
Public Class CreateAcc
    Public Success As Boolean
    Public bOnlyOne As Boolean
    Public AccNo As Integer
    Public GrpNo As Short
    Public Condition As String
    Public IsContract As Boolean
    Public DeptId As String
    Public GeneratAccNIncm As Boolean
    Public AccGrpId As Integer
    Public IncmGrpId As Integer
    Public isModi As Boolean
    Public iscust As Boolean

    Private ChgAlias As Boolean
    Private ChgDescr As Boolean
    Private ChgByPrg As Boolean
    Private chgByPrgNum As Boolean
    Private ChgAddr As Boolean
    Private _vtable As DataTable
    Private _BankTable As DataTable
    Private _fcTable As DataTable
    Private rNo As Integer
    Private activecontrolname As String
    Private isIssued As Boolean
    Private accid As Long
    Private grpset As String
    Private chgByPgm As Boolean

    '--const for opb
    Private Const ConstChq = 0
    Private Const ConstChqdate = 1
    Private Const constBank = 2
    Private Const ConstDtype = 3
    Private Const ConstAmount = 4
    Private Const ConstParty = 5
    Private Const ConstRef = 6
    Private Const ConstTrdate = 7
    Private Const ConstDesc = 8
    Private Const ConstFCName = 9
    Private Const ConstFCAmount = 10
    Private Const ConstFCRate = 11
    Private Const ConstTransfer = 12
    Private Const ConstCustHead = 13
    Private Const ConstTrid = 14
    Private Const ConstFCDec = 15
    Private _isdatechanged As Boolean = False
    Private Const ConstPAmt = 16
    Private Const ConstUnq = 17
    ' ------------------

    Private Const ConstAlias = 0
    Private Const ConstDescription = 1
    Private Const ConstPhone = 4
    Private Const ConstContactName = 5
    Private Const constEMail = 6
    Private Const ConstAccounNo = 7
    'numeric text
    Dim idx As Integer
    Dim str1 As String
    Dim str2 As String
    Dim str3 As String
    Dim SelStart As Integer
    Dim numCtrl As TextBox
    Private nSelect As Integer

    Public Event Unload()
    Public Event OpenAccMaster(ByRef AccountNo As Long)
    Private _objcmnbLayer As New clsCommon_BL
    Private _ObjAcc As New clsAccountTransaction
    Private WithEvents fSelect As Selectfrm
    Dim chngprg As Boolean

    Private Sub ldAccounts(ByVal AccNo As Long)
        Dim dtTable As DataTable
        SetControlsInDetailTab()
        'dtTable = _objcmnbLayer._fldDatatable("SELECT * FROM AccMast LEFT JOIN AccMastAddr ON AccMast.AccountNo=AccMastAddr.AccountNo LEFT JOIN JobTb ON AccMast.AccountNo=JobTb.custcode WHERE AccMast.AccountNo = " & AccNo)
        _ObjAcc.AccountNo = AccNo
        Dim ds As DataSet
        ds = _ObjAcc.returnAccountDetailsWithJob
        dtTable = ds.Tables(0)
        ChgByPrg = True
        If dtTable.Rows.Count > 0 Then
            cmbBr.Text = Trim("" & dtTable(0)("BranchId"))
        Else
            cmbBr.Text = ""
        End If
        txtRec0.Tag = dtTable(0)("AccountNo")
        txtRec0.Text = Trim("" & dtTable(0)("Alias"))
        txtRec1.Text = Trim("" & dtTable(0)("AccDescr"))
        accid = dtTable(0)("accid")
        numopnBal.Text = Format(CDbl(dtTable(0)("OpnBal")), numFormat)
        numopnBal.Enabled = True
        'dtTable = _objcmnbLayer._fldDatatable("SELECT * FROM AccMastAddr WHERE AccountNo = " & AccNo)
        If dtTable.Rows.Count > 0 Then
            txtAddr0.Text = Trim("" & dtTable(0)("Address1"))
            txtAddr1.Text = Trim("" & dtTable(0)("Address2"))
            txtAddr2.Text = Trim("" & dtTable(0)("Address3"))
            txtAddr3.Text = Trim("" & dtTable(0)("Address4"))
            txtphone.Text = Trim("" & dtTable(0)("Phone"))
            txtFax.Text = Trim("" & dtTable(0)("Fax"))
            txtemail.Text = Trim("" & dtTable(0)("EMail"))
            txtwebsite.Text = Trim("" & dtTable(0)("Website"))
            txtcontactname.Text = Trim("" & dtTable(0)("ContactName"))
            txtmalayalam.Text = Trim("" & dtTable(0)("nameMalayalam"))
            cmbBr.Text = Trim("" & dtTable(0)("BranchId"))
            If Val(dtTable(0)("isreconcil") & "") = 0 Then
                chkreconcil.Checked = False
            Else
                chkreconcil.Checked = True
            End If
            ldOpeningBalance()
        End If
        dtTable = ds.Tables(1)
        ChgByPrg = False
        txtAddr0.ReadOnly = False
        ChgAddr = False
        btnremove.Enabled = True
        txtRec1.Focus()
    End Sub


    Private Sub locateCmb()
        Dim i As Short
        With cmbAccGroup
            For i = 0 To .Items.Count - 1
                'If GrpNo = VB6.GetItemData(cmbAccGroup, i) Then
                If GrpNo = cmbAccGroup.Items(i).thedata Then
                    .SelectedIndex = i
                    Exit Sub
                End If
            Next i
            If .Items.Count > 0 Then .SelectedIndex = 0
        End With
    End Sub

    Private Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        If Val(btnaddnew.Tag) = 0 And Val(btnmodify.Tag) = 0 Then
            MsgBox("This user do not have rights to update", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If Not chkDuplicate() Then Exit Sub
        If txtRec0.Text = "" Then
            If iscust Then
                MsgBox("Invalid Customer Code", MsgBoxStyle.Critical)
            Else
                MsgBox("Invalid Supplier Code", MsgBoxStyle.Critical)
            End If

            txtRec0.Focus()
            Exit Sub
        End If
        If txtRec1.Text = "" Then
            If iscust Then
                MsgBox("Invalid Customer Name", MsgBoxStyle.Critical)
            Else
                MsgBox("Invalid Supplier Name", MsgBoxStyle.Critical)
            End If
            txtRec1.Focus()
            Exit Sub
        End If
        If grdVoucher.RowCount > 0 Then
            If Not validateEntry() Then Exit Sub
        End If
        If isModi Then
            If ModiAcc() Then
                saveAddress()
            Else
                Exit Sub
            End If
        Else
            'Dim AccMastSrch As DataTable
            'AccMastSrch = _objcmnbLayer._fldDatatable("Select * from Accmast ")
            If cmbAccGroup.SelectedIndex < 0 Or cmbAccGroup.Text = "" Then
                MsgBox("Choose a valid Account Group !!", vbExclamation)
                cmbAccGroup.Focus()
                Exit Sub
            End If
            With cmbBr
                If .Items.Count > 0 Then
                    If .Text = "" And .Items(0) <> "" Then
                        MsgBox("Choose a valid Branch !!", vbExclamation)
                        Exit Sub
                    End If
                End If
            End With
            If saveAcc() Then
                saveAddress()
                '_objcmnbLayer._fldDatatable("SELECT max(AccountNo) from AccMast ")
            Else
                Exit Sub
            End If
        End If
        MsgBox("Record Updated Successfully", MsgBoxStyle.Information)
        If bOnlyOne Then
            RaiseEvent OpenAccMaster(accid)
            Me.Close()
        End If
        btnaddnew_Click(btnaddnew, New System.EventArgs)
        If isModi Then
            TabControl1.SelectedIndex = 1
        End If
    End Sub
    Private Function ModiAcc() As Boolean
        ModiAcc = True
        With cmbBr
            If .Items.Count > 0 Then
                If .Text = "" And .Items(0) <> "" Then
                    MsgBox("Choose a valid Branch !!", vbExclamation)
                    Exit Function
                End If
            End If
        End With
        If numopnBal.Text = "" Then numopnBal.Text = 0
        _objcmnbLayer._saveDatawithOutParm("UPDATE AccMast SET Alias = '" & MkDbSrchStr(txtRec0.Text) & "', AccDescr = '" & _
                                           MkDbSrchStr(txtRec1.Text) & "', S1AccId = '" & Val(cmbAccGroup.Items(cmbAccGroup.SelectedIndex).thedata()) & "'," & _
                                           " BranchId = '" & MkDbSrchStr(cmbBr.Text) & "',BKGCode='" & MkDbSrchStr(txtbankcode.Text) & "'," & _
                                           "OpnBal=" & CDbl(numopnBal.Text) & "," & _
                                           "nameMalayalam = N'" & txtmalayalam.Text & "'," & _
                                           "isreconcil=" & IIf(chkreconcil.Checked, 1, 0) & _
                                           " WHERE accid = " & accid)

    End Function


    Private Function chkDuplicate() As Boolean
        Dim dt As DataTable
        chkDuplicate = True
        dt = _objcmnbLayer._fldDatatable("SELECT * FROM AccMast ")
        Dim _qry = From job In dt.AsEnumerable() Where job![Alias] = txtRec0.Text And job!AccountNo <> AccNo Select New With _
                        {.Name = job!AccountNo}
        If _qry.Count > 0 Then
            MsgBox("Dupicate Alias Found!", MsgBoxStyle.Critical)
            chkDuplicate = False
            txtRec0.Focus()
            Exit Function
        End If
        _qry = From job In dt.AsEnumerable() Where job!AccDescr = txtRec1.Text And job!AccountNo <> AccNo Select New With _
                        {.Name = job!AccountNo}
        If _qry.Count > 0 Then
            MsgBox("Dupicate Name Found!", MsgBoxStyle.Critical)
            chkDuplicate = False
            txtRec1.Focus()
            Exit Function
        End If
    End Function


    Private Sub SetControlsInDetailTab()
        chgbyprg = True
        For Each Control In Panel1.Controls
            If TypeOf (Control) Is TextBox Then
                Control.text = ""
                Control.tag = ""
            End If
            If TypeOf (Control) Is CheckBox Then
                Control.checked = False
                Control.tag = ""
            End If
        Next
        grdVoucher.Rows.Clear()
        numopnBal.Text = Format(0, numFormat)
        chgbyprg = False
    End Sub

    Private Function saveAcc() As Boolean
        saveAcc = True
        If Not isModi Then
            GenerateNext(cmbAccGroup.Text, cmbAccGroup.Items(cmbAccGroup.SelectedIndex).thedata)
            If numopnBal.Text = "" Then numopnBal.Text = 0
            _objcmnbLayer._saveDatawithOutParm("INSERT INTO AccMast (AccountNo, Alias, AccDescr, S1AccId,OpnBal,BKGCode,nameMalayalam,BranchId,isreconcil) VALUES (" & _
                                               Val(txtRec0.Tag) & ", '" & MkDbSrchStr(Trim(txtRec0.Text)) & "', '" & MkDbSrchStr(Trim(txtRec1.Text)) & "', " & _
                                               cmbAccGroup.Items(cmbAccGroup.SelectedIndex).thedata() & "," & CDbl(numopnBal.Text) & ",'" & _
                                               MkDbSrchStr(txtbankcode.Text) & "',N'" & txtmalayalam.Text & "','" & cmbBr.Text & "'," & IIf(chkreconcil.Checked, 1, 0) & ")")
        End If
    End Function
    Private Function GenerateNext(ByVal Grpname As String, ByVal newVal As Integer) As String
        Dim N As Double
        Dim NewCode As String = ""
        GenerateNext = ""
        Dim tmp As String
        Dim _vdatatableAcc As DataTable
        _vdatatableAcc = _objcmnbLayer._fldDatatable("SELECT MAX(AccountNo)AccountNo FROM AccMast WHERE S1AccId =" & newVal)
        If _vdatatableAcc.Rows.Count > 0 Then
            txtRec0.Tag = _vdatatableAcc(0)("AccountNo")
        End If
        If Val(txtRec0.Tag & "") = 0 Then
            txtRec0.Tag = Val(newVal & "0000")
        End If
        If Val(txtRec0.Tag) >= Val(newVal & "9999") Then MsgBox("Maximum number of Ledgers reached in this Group.", MsgBoxStyle.Critical) : Exit Function
        txtRec0.Tag = Val(txtRec0.Tag) + 1
        _vdatatableAcc = _objcmnbLayer._fldDatatable("SELECT * FROM AccMast")
        Try
            Do Until False
                N = N + 1
                tmp = Strings.Left(Grpname, 4) & Format(N, StrDup(4, "0"))
                Dim _qry = From job In _vdatatableAcc.AsEnumerable() Where job![Alias] = tmp Select New With _
                       {.Name = job!AccountNo}
                If _qry.Count = 0 Then
                    NewCode = Strings.Left(Grpname, 4) & Format(N, StrDup(4, "0"))
                    NewCode = Mid(NewCode, 1, 30)
                    Exit Do
                End If
                For Each itm In _qry
                    If Val(itm.Name) = 0 Then
                        NewCode = Strings.Left(Grpname, 4) & Format(N, StrDup(4, "0"))
                        NewCode = Mid(NewCode, 1, 30)
                        Exit Do
                    End If
                Next
            Loop
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return NewCode
    End Function

    Private Sub CreateAccNew_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        btnaddnew.Focus()
    End Sub
    Sub SetGridHead()
        With grdVoucher
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = True
            .EditMode = DataGridViewEditMode.EditProgrammatically
            .ColumnCount = 18
            .ColumnHeadersHeight = 25
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke
            .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
            .RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.0!)

            .Columns(ConstChq).HeaderText = "Chq. #"
            .Columns(ConstChq).Width = Me.Width * 7.5 / 100   '100
            .Columns(ConstChq).SortMode = DataGridViewColumnSortMode.NotSortable

            Dim col As New CalendarColumn()
            .Columns.RemoveAt(ConstChqdate)
            'col.DataPropertyName = "ChqDate"
            .Columns.Insert(ConstChqdate, col)
            .Columns(ConstChqdate).HeaderText = "Chq. Date"

            .Columns(constBank).HeaderText = "Bank"
            .Columns(constBank).Width = 50
            .Columns(constBank).SortMode = DataGridViewColumnSortMode.NotSortable
            Dim cmb As New DataGridViewComboBoxColumn
            ' If (ismodi = False) Then

            cmb.Items.Add("")
            For i = 0 To _BankTable.Rows.Count - 1
                cmb.Items.Add(_BankTable(i)("BankCode"))
            Next

            cmb.HeaderText = "Bank"
            cmb.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox
            .Columns.RemoveAt(constBank)
            .Columns.Insert(constBank, cmb)
            ' End If
            .Columns(ConstDtype).HeaderText = "Type"
            .Columns(ConstDtype).Width = 50
            .Columns(ConstDtype).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstDtype).ReadOnly = True

            .Columns(ConstAmount).HeaderText = "Amount"
            .Columns(ConstAmount).Width = Me.Width * 7.5 / 100   '100
            .Columns(ConstAmount).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstAmount).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstAmount).DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns(ConstParty).HeaderText = "Party Name"
            .Columns(ConstParty).Width = Me.Width * 10 / 100   '100
            .Columns(ConstParty).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstParty).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

            .Columns(ConstRef).HeaderText = "INV/Ref"
            .Columns(ConstRef).Width = Me.Width * 7.5 / 100   '100
            .Columns(ConstRef).SortMode = DataGridViewColumnSortMode.NotSortable

            col = New CalendarColumn()
            .Columns.RemoveAt(ConstTrdate)
            'col.DataPropertyName = "ChqDate"
            .Columns.Insert(ConstTrdate, col)
            .Columns(ConstTrdate).HeaderText = "Tr. Date"

            .Columns(ConstDesc).HeaderText = "Description"
            .Columns(ConstDesc).Width = Me.Width * 10 / 100   '100
            .Columns(ConstDesc).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstDesc).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

            .Columns(ConstFCName).HeaderText = "FC"
            .Columns(ConstFCName).Width = 50
            .Columns(ConstFCName).SortMode = DataGridViewColumnSortMode.NotSortable
            cmb = New DataGridViewComboBoxColumn
            cmb.Items.Add("")
            For i = 0 To _fcTable.Rows.Count - 1
                cmb.Items.Add(_fcTable(i)("CurrencyCode"))
            Next
            cmb.HeaderText = "FC"
            cmb.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton

            .Columns.RemoveAt(ConstFCName)
            .Columns.Insert(ConstFCName, cmb)

            .Columns(ConstFCAmount).HeaderText = "FC Amount"
            .Columns(ConstFCAmount).Width = Me.Width * 7.5 / 100   '100
            .Columns(ConstFCAmount).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstFCAmount).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstFCAmount).DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns(ConstFCRate).HeaderText = "FC Rate"
            .Columns(ConstFCRate).Width = Me.Width * 7.5 / 100   '100
            .Columns(ConstFCRate).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstFCRate).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstFCRate).DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns(ConstTransfer).HeaderText = "Transfer ?"
            .Columns(ConstTransfer).Width = Me.Width * 7.5 / 100   '100
            .Columns(ConstTransfer).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstTransfer).ReadOnly = True

            .Columns(ConstCustHead).HeaderText = "CustId"
            .Columns(ConstCustHead).Visible = False
            .Columns(ConstTrid).HeaderText = "Trid"
            .Columns(ConstTrid).Visible = False
            .Columns(ConstFCDec).HeaderText = "FCDec"
            .Columns(ConstFCDec).Visible = False
            .Columns(ConstPAmt).HeaderText = "PAmt"
            .Columns(ConstPAmt).Visible = False
            .Columns(ConstUnq).HeaderText = "UnqNo"
            .Columns(ConstUnq).Visible = False
        End With
    End Sub
    Private Sub CreateAccNew_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        onLoadDefault()
        If Not userType Then
            btnaddnew.Tag = 1
            btnmodify.Tag = 1
            btnremove.Tag = 1
        Else
            btnaddnew.Tag = IIf(getRight(26, CurrentUser), 1, 0)
            btnmodify.Tag = IIf(getRight(27, CurrentUser), 1, 0)
            btnremove.Tag = IIf(getRight(28, CurrentUser), 1, 0)
        End If
        cmdOk.Enabled = False
        If Condition = "" Then cmbAccGroup.SelectedIndex = 0
        cmbAccGroup.Enabled = True
        lblCap8.Visible = enableBranch
        cmbBr.Visible = enableBranch
    End Sub
    Private Sub saveAddress()
        Try
            If isModi Then
                _objcmnbLayer._saveDatawithOutParm("DELETE FROM AccMastAddr Where AccountNo=" & Val(accid))
            End If
            Dim AccMast As DataTable
            AccMast = _objcmnbLayer._fldDatatable("SELECT AccId FROM AccMast WHERE AccountNo=" & Val(txtRec0.Tag))
            If AccMast.Rows.Count > 0 Then
                accid = AccMast(0)("AccId")
            End If
            If txtAddr0.Text <> "" Or txtphone.Text <> "" Or txtemail.Text <> "" Then
                _objcmnbLayer._saveDatawithOutParm("INSERT INTO AccMastAddr (AccountNo,Address1,Address2,Address3,Address4,Phone,Fax,ContactName,EMail,Website)" & _
                                                      " VALUES(" & _
                                                         Val(accid) & ",'" & _
                                                         MkDbSrchStr(txtAddr0.Text) & "','" & _
                                                         MkDbSrchStr(txtAddr1.Text) & "','" & _
                                                         MkDbSrchStr(txtAddr2.Text) & "','" & _
                                                         MkDbSrchStr(txtAddr3.Text) & "','" & _
                                                         MkDbSrchStr(txtphone.Text) & "','" & _
                                                         MkDbSrchStr(txtFax.Text) & "','" & _
                                                         MkDbSrchStr(txtcontactname.Text) & "','" & _
                                                         MkDbSrchStr(txtemail.Text) & "','" & _
                                                         MkDbSrchStr(txtwebsite.Text) & "')")
            End If

            updateOB()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub addNew()
        btnaddnew.Text = "Undo"
        Panel1.Enabled = True
        btnadd.Enabled = (btnadd.Tag = "T")
        numopnBal.Enabled = True

        btnmodify.Enabled = False
        cmdOk.Enabled = False
        isModi = False
        SetControlsInDetailTab()
        txtRec1.Focus()
        If Not isModi Then
            If cmbAccGroup.Items.Count = 0 Then Exit Sub
            txtRec0.Text = GenerateNext((cmbAccGroup.Text), cmbAccGroup.Items(cmbAccGroup.SelectedIndex).thedata)
        Else
            txtRec1.Focus()
        End If
    End Sub

    Private Sub btnaddnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddnew.Click
        If Val(btnaddnew.Tag) = 0 Then
            MsgBox("This user do not have permission to add new entry", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If btnaddnew.Text = "&Add New" Then
            addNew()
        Else
            SetControlsInDetailTab()
            txtRec0.Focus()
            btnmodify.Enabled = False
            cmdOk.Enabled = False
            btnadd.Enabled = False
            numopnBal.Enabled = False
            btnaddnew.Text = "&Add New"
            Panel1.Enabled = False
            btnremove.Enabled = False
            Panel4.Visible = False
            If isModi Then
                TabControl1.SelectedIndex = 1
            End If
            isModi = False
            cmbAccGroup.Enabled = True
        End If
        grdVoucher.Rows.Clear()
    End Sub

    Private Sub btnmodify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnmodify.Click
        If Val(btnmodify.Tag) = 0 Then
            MsgBox("This user do not have permission to modify", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If btnaddnew.Text = "&Add New" Then
            btnaddnew.Text = "Undo"
            Panel1.Enabled = True
            btnadd.Enabled = (btnadd.Tag = "T")
            btnmodify.Enabled = False
            btnremove.Enabled = True
            isModi = True
            'txtAddr0.ReadOnly = False
        Else
            btnaddnew_Click(btnaddnew, New System.EventArgs)
        End If
        'txtRec0.Focus()
    End Sub


    Private Sub txtRec0_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRec1.TextChanged, txtRec0.TextChanged, txtAddr3.TextChanged, txtAddr2.TextChanged, txtAddr1.TextChanged, txtAddr0.TextChanged, txtcontactname.TextChanged, txtwebsite.TextChanged, txtemail.TextChanged, txtFax.TextChanged, txtbankcode.TextChanged
        If ChgByPrg Then Exit Sub
        cmdOk.Enabled = True
    End Sub

    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        cmdOk.Enabled = True
    End Sub


    Private Sub cmbAccGroup_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAccGroup.SelectedValueChanged
        ChgByPrg = True
        If Not isModi Then
            If cmbAccGroup.Text <> "" Then
                txtRec0.Text = GenerateNext((cmbAccGroup.Text), cmbAccGroup.Items(cmbAccGroup.SelectedIndex).thedata)
            End If
        Else
        End If
        ChgByPrg = False
    End Sub

    Private Sub txtRec0_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtwebsite.KeyDown, txtRec1.KeyDown, txtRec0.KeyDown, txtphone.KeyDown, txtFax.KeyDown, txtemail.KeyDown, txtcontactname.KeyDown, txtAddr3.KeyDown, txtAddr2.KeyDown, txtAddr1.KeyDown, txtAddr0.KeyDown, txtbankcode.KeyDown, txtmalayalam.KeyDown
        Dim myctl As TextBox
        myctl = sender
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub Label8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub cldrdate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub



    Private Sub dtpdate_Change(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If ChgByPrg Then Exit Sub
        cmdOk.Enabled = True
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Private Sub ldAccount()
        Dim brcodition As String
        brcodition = IIf(UsrBr = "", "", " AND (BranchId='" & UsrBr & "' Or isnull(BranchId,'')='') ")

        _vtable = _objcmnbLayer._fldDatatable("SELECT Alias,AccDescr,isnull(Bal,0)+OpnBal [Closing Bal],OpnBal [Opening Bal],Phone,ContactName,EMail,BranchId,AccMast.AccountNo FROM AccMast " & _
                                              "left Join BalanceQr On BalanceQr.AccountNo=AccMAst.accid " & _
                                              "LEFT JOIN AccMastAddr ON AccMast.AccountNo=AccMastAddr.AccountNo " & _
                                              "left join S1AccHd on AccMast.S1AccId=S1AccHd.S1AccId WHERE Descr In ('" & cmbAccGroup.Text & "') " & brcodition & " ORDER BY Descr")
        grdItem.DataSource = _vtable
        SetGridSupplier()
        setComboGrid()
    End Sub
    Private Sub onLoadDefault()
        Dim ds As DataSet
        ds = _ldMasterDataset(0)
        If enableBranch Then
            Dim i As Integer
            cmbBr.Items.Add("")
            For i = 0 To ds.Tables(0).Rows.Count - 1
                cmbBr.Items.Add(ds.Tables(0)(i)("Branchcode"))
            Next
            If cmbBr.Items.Count > 0 Then
                cmbBr.Text = UsrBr
                If cmbBr.Text = "" Then cmbBr.SelectedIndex = 0
            End If
        End If
        _fcTable = ds.Tables(2)
        _BankTable = ds.Tables(4)
        ldTitle()
    End Sub

    Private Sub SetGridSupplier()
        With grdItem
            SetGridProperty(grdItem)

            .Columns("Alias").HeaderText = "Cust. code"
            .Columns("Alias").Width = 100
            .Columns("Alias").Frozen = True

            .Columns("AccDescr").HeaderText = "Description"
            .Columns("AccDescr").Width = 250

            .Columns("Phone").HeaderText = "Phone"
            .Columns("Phone").Width = 80

            .Columns("ContactName").HeaderText = "ContactName"
            .Columns("ContactName").Width = 150

            .Columns("EMail").HeaderText = "EMail"
            .Columns("EMail").Width = 150

            .Columns("AccountNo").Visible = False
            .Columns("BranchId").Visible = enableBranch
            .Columns("BranchId").HeaderText = "Branch"

            .Columns("Closing Bal").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Closing Bal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns("Opening Bal").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Opening Bal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


        End With
    End Sub

    Private Sub SetGridCustomer()
        With grdItem
            SetGridProperty(grdItem)

            .Columns("Alias").HeaderText = "Cust. code"
            .Columns("Alias").Width = 100
            .Columns("Alias").Frozen = True

            .Columns("AccDescr").HeaderText = "Description"
            .Columns("AccDescr").Width = 250

            .Columns("Phone").HeaderText = "Phone"
            .Columns("Phone").Width = 80

            .Columns("ContactName").HeaderText = "ContactName"
            .Columns("ContactName").Width = 150

            .Columns("EMail").HeaderText = "EMail"
            .Columns("EMail").Width = 150

            .Columns("AccountNo").Visible = False

        End With
    End Sub

    Private Sub setComboGrid()
        ChgByPrg = True
        cmbOrder.Items.Clear()
        Dim i As Integer = 0
        For i = 0 To grdItem.ColumnCount - 2
            cmbOrder.Items.Add(grdItem.Columns(i).HeaderText)
        Next
        If cmbOrder.Items.Count > 0 Then cmbOrder.SelectedIndex = 0
        ChgByPrg = False
    End Sub

    Private Sub txtSeq_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeq.TextChanged
        grdItem.DataSource = SearchGrid(_vtable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
    End Sub

    Private Sub grdItem_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdItem.DoubleClick
        AccNo = Val(grdItem.Item("AccountNo", grdItem.CurrentCell.RowIndex).Value)
        ldAccounts(AccNo)
        btnadd.Visible = True
        Dim clsbal As Double
        clsbal = Val(grdItem.Item("Closing Bal", grdItem.CurrentCell.RowIndex).Value)
        lblclosing.Text = "0.00"
        lblclosing.Text = Format(clsbal, numFormat)
        btnmodify_Click(btnmodify, New System.EventArgs)
        TabControl1.SelectedIndex = 0
        cmbAccGroup.Enabled = False
    End Sub

    Private Sub grdItem_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItem.CellContentClick

    End Sub

    Private Sub TabControl1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.Click
        If TabControl1.SelectedIndex = 1 And btnaddnew.Text = "Undo" Then
            MsgBox("Do Undo/Update before moving to Enquiry", MsgBoxStyle.Exclamation)
            TabControl1.SelectedIndex = 0
        Else
            ldAccount()
        End If
    End Sub

    Private Sub btnremove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnremove.Click
        If Val(btnremove.Tag) = 0 Then
            MsgBox("This user do not have permission to remove", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT TrId FROM ItmInvCmnTb WHERE CSCode=" & accid & _
                                         " UNION ALL SELECT Jobid FROM JobTb WHERE custcode=" & accid & _
                                         " UNION ALL SELECT Linkno FROM AccTrDet WHERE AccountNo=" & accid)
        If MsgBox("You are going to remove the Account # " & txtRec0.Text & " # Permenantly! Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
            Exit Sub
        End If
        If dt.Rows.Count > 0 Then
            MsgBox("Account Code #" & txtRec0.Text & " having transactions, You can't remove record", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        _objcmnbLayer._saveDatawithOutParm("DELETE FROM AccMast WHERE AccountNo=" & Val(txtRec0.Tag))
        _objcmnbLayer._saveDatawithOutParm("DELETE FROM AccMastAddr WHERE AccountNo=" & Val(txtRec0.Tag))
        ldAccount()
        TabControl1.SelectedIndex = 1
        btnmodify_Click(btnmodify, New System.EventArgs)
    End Sub

    Private Sub dtpjdate_KeyDownEvent(ByVal sender As Object, ByVal e As AxMSMask.MaskEdBoxEvents_KeyDownEvent)
        If e.keyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtvalue_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        numCtrl = sender
        ChgByPrg = True
        SelStart = numCtrl.SelectionStart
        If IsNumeric(e.KeyChar) Or e.KeyChar = "." Then
            If numCtrl.SelectionLength > 0 Then
                numCtrl.Text = Mid(numCtrl.Text, 1, SelStart) & Mid(numCtrl.Text, SelStart + numCtrl.SelectionLength + 1)
            End If
            idx = numCtrl.Text.IndexOf(".")
            If e.KeyChar <> "." Then
                If SelStart > idx Then
                    numCtrl.Text = Mid(numCtrl.Text, 1, SelStart) & Val(e.KeyChar) & Mid(numCtrl.Text, SelStart + 2)
                Else
                    numCtrl.Text = Mid(numCtrl.Text, 1, SelStart) & Val(e.KeyChar) & Mid(numCtrl.Text, SelStart + 1)
                End If
            End If
            idx = numCtrl.Text.IndexOf(".")
            If idx > 0 Then
                str1 = CDbl(Mid(numCtrl.Text, 1, idx))
                str2 = Mid(numCtrl.Text, idx + 1)
            End If
            If Len(Trim(str1)) > 10 And Len(numCtrl.Text) > 0 Then
                If Mid(Mid(Mid(numCtrl.Text, 1, idx), Len(Trim(str1)) + 1 - 10), 1, 1) = "," Then
                    str3 = Mid(Mid(numCtrl.Text, 1, idx), Len(Trim(str1)) + 2 - 10)
                End If
                numCtrl.Text = Mid(str1, Len(Trim(str1)) + 1 - 10) & str2
                SelStart = SelStart - 2
            Else
                str3 = ""
            End If
            idx = numCtrl.Text.IndexOf(".")
            If idx > 0 Then
                str1 = Mid(numCtrl.Text, 1, idx)
            Else
                str1 = numCtrl.Text
            End If
            numCtrl.Text = CDbl(numCtrl.Text)
            numCtrl.Text = Format(Val(numCtrl.Text), "#,##0.00")
            idx = numCtrl.Text.IndexOf(".")
            If idx > 0 Then
                str2 = Mid(numCtrl.Text, 1, idx)
            Else
                str2 = numCtrl.Text
            End If
            numCtrl.SelectionStart = SelStart + Len(str2) - IIf(str3 = "", Len(str1), Len(str3)) + 1
            'we assaigned formatted value to textbox so we not need it write it on again
            e.Handled = True
        Else
            If CInt(AscW(e.KeyChar)) = 8 Or CInt(AscW(e.KeyChar)) = 22 Then
                If CInt(AscW(e.KeyChar)) = 22 Then
                    If Not IsNumeric(Clipboard.GetText) Then
                        e.Handled = True
                    End If
                Else
                    e.Handled = False
                End If
            Else
                e.Handled = True
            End If
        End If
        ChgByPrg = False
    End Sub

    Private Sub dtpjdate_Change(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If ChgByPrg Then Exit Sub
        'cmdOk.Enabled = True
    End Sub

    Private Sub cmbAccGroup_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAccGroup.SelectedIndexChanged

        If TabControl1.SelectedIndex = 1 Then
            ldAccount()
        End If
        getGroupDet()
        txtbankcode.Enabled = False
        lblbankcode.Enabled = False
        'btnadd.Visible = False
        'numopnBal.Enabled = True
        If UCase(grpset) = "P.D.C.(R)" Or UCase(grpset) = "P.D.C.(I)" Then
            'btnadd.Visible = True
            numopnBal.Enabled = False
            btnadd.Tag = "T"
        ElseIf UCase(grpset) = "BANK" Then
            txtbankcode.Enabled = True
            lblbankcode.Enabled = True
        Else
            'btnadd.Visible = False
            btnadd.Tag = ""
        End If
    End Sub
    Private Sub getGroupDet()
        Dim dt As DataTable
        grpset = ""
        dt = _objcmnbLayer._fldDatatable("SELECT GrpSetOn FROM S1AccHd WHERE Descr='" & cmbAccGroup.Text & "'")
        If dt.Rows.Count > 0 Then
            grpset = dt(0)("GrpSetOn")
        End If
    End Sub
    Private Sub numopnBal_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles numopnBal.KeyPress
        NumericTextOnKeypress(numopnBal, e, chgByPrgNum, numFormat)
        'numCtrl = sender
        'ChgByPrg = True
        'SelStart = numCtrl.SelectionStart
        'If IsNumeric(e.KeyChar) Or e.KeyChar = "." Then
        '    If numCtrl.SelectionLength > 0 Then
        '        numCtrl.Text = Mid(numCtrl.Text, 1, SelStart) & Mid(numCtrl.Text, SelStart + numCtrl.SelectionLength + 1)
        '    End If
        '    idx = numCtrl.Text.IndexOf(".")
        '    If e.KeyChar <> "." Then
        '        If SelStart > idx Then
        '            numCtrl.Text = Mid(numCtrl.Text, 1, SelStart) & Val(e.KeyChar) & Mid(numCtrl.Text, SelStart + 2)
        '        Else
        '            numCtrl.Text = Mid(numCtrl.Text, 1, SelStart) & Val(e.KeyChar) & Mid(numCtrl.Text, SelStart + 1)
        '        End If
        '    End If
        '    idx = numCtrl.Text.IndexOf(".")
        '    If idx > 0 Then
        '        str1 = CDbl(Mid(numCtrl.Text, 1, idx))
        '        str2 = Mid(numCtrl.Text, idx + 1)
        '    End If
        '    If Len(Trim(str1)) > 10 And Len(numCtrl.Text) > 0 Then
        '        If Mid(Mid(Mid(numCtrl.Text, 1, idx), Len(Trim(str1)) + 1 - 10), 1, 1) = "," Then
        '            str3 = Mid(Mid(numCtrl.Text, 1, idx), Len(Trim(str1)) + 2 - 10)
        '        End If
        '        numCtrl.Text = Mid(str1, Len(Trim(str1)) + 1 - 10) & str2
        '        SelStart = SelStart - 2
        '    Else
        '        str3 = ""
        '    End If
        '    idx = numCtrl.Text.IndexOf(".")
        '    If idx > 0 Then
        '        str1 = Mid(numCtrl.Text, 1, idx)
        '    Else
        '        str1 = numCtrl.Text
        '    End If
        '    numCtrl.Text = CDbl(numCtrl.Text)
        '    numCtrl.Text = Format(Val(numCtrl.Text), numFormat)
        '    idx = numCtrl.Text.IndexOf(".")
        '    If idx > 0 Then
        '        str2 = Mid(numCtrl.Text, 1, idx)
        '    Else
        '        str2 = numCtrl.Text
        '    End If
        '    numCtrl.SelectionStart = SelStart + Len(str2) - IIf(str3 = "", Len(str1), Len(str3)) + 1
        '    'we assaigned formatted value to textbox so we not need it write it on again
        '    e.Handled = True
        'Else
        '    If CInt(AscW(e.KeyChar)) = 8 Or CInt(AscW(e.KeyChar)) = 22 Or CInt(AscW(e.KeyChar)) = 45 Then
        '        If CInt(AscW(e.KeyChar)) = 22 Then
        '            If Not IsNumeric(Clipboard.GetText) Then
        '                e.Handled = True
        '            End If
        '        Else
        '            e.Handled = False
        '        End If
        '    Else
        '        e.Handled = True
        '    End If
        'End If
        'ChgByPrg = False
    End Sub

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        If Panel4.Visible = True And grdVoucher.RowCount = 0 Then
            Panel4.Visible = False
            numopnBal.Enabled = True
        ElseIf Panel4.Visible = False Then
            If txtRec1.Text = "" Then
                MsgBox("Please enter Account Ledger Details", MsgBoxStyle.Exclamation)
                txtRec1.Focus()
                Exit Sub
            End If
            Panel4.Visible = True
            numopnBal.Enabled = False
            If grdVoucher.RowCount = 0 Then SetGridHead()
        End If

    End Sub

    Private Sub btnAcSrch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAcSrch.Click
        rNo = RowOnGrid(rNo, grdVoucher, cmbAcOrder.SelectedIndex, txtAcSearch.Text)
        grdVoucher.ClearSelection()
        grdVoucher.Rows(rNo).Selected = True
        grdVoucher.FirstDisplayedScrollingRowIndex = rNo
        rNo = +1
    End Sub

    Private Sub btnitmAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnitmAdd.Click
        If DateFrom = DateValue("01/01/1950") Then
            MsgBox("Missing Financial Period! Opening balance cannot update", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        AddRow()
    End Sub
    Private Sub AddRow()
        grdVoucher.Rows.Add(1)
        With grdVoucher
            .Item(ConstFCRate, .Rows.Count - 1).Value = Format(1, "0.00")
            .Item(ConstRef, .Rows.Count - 1).Value = "ON A/C"
            .Item(ConstDtype, .Rows.Count - 1).Value = IIf(isIssued, "Cr", "Dr")
            .Item(ConstTrdate, .Rows.Count - 1).Value = DateAdd(DateInterval.Day, -1, DateValue(DateFrom))
            .Select()
            .CurrentCell = .Item(0, .Rows.Count - 1)
            activecontrolname = "grdVoucher"
            BeginEdit()
        End With
    End Sub
    Public Sub BeginEdit()
        ChgByPrg = True
        With grdVoucher
            If .RowCount = 0 Then Exit Sub
            activecontrolname = "grdVoucher"
            .BeginEdit(True)
        End With
        ChgByPrg = False
    End Sub

    Private Sub btnrem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrem.Click
        removeRow()
    End Sub
    Private Sub removeRow()
        If grdVoucher.Item(ConstTransfer, grdVoucher.CurrentRow.Index).Value = "YES" Then
            MsgBox("You Can not Delete! This is Already Transferred")
            Exit Sub
        End If
        If MsgBox("Do You Want Remove The Row", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        Dim Linkno As Long
        Dim dt As DataTable
        With grdVoucher
            If .CurrentRow Is Nothing Then Exit Sub
            dt = _objcmnbLayer._fldDatatable("Select Linkno from acctrdet where UnqNo=" & Val(.Item(ConstUnq, .CurrentRow.Index).Value))
            If dt.Rows.Count > 0 Then
                Linkno = Val(dt(0)("Linkno") & "")
            End If
            _objcmnbLayer._saveDatawithOutParm("DELETE FROM AccTrDet WHERE UnqNo=" & Val(.Item(ConstUnq, .CurrentRow.Index).Value))
            dt = _objcmnbLayer._fldDatatable("Select Linkno from AccTrDet WHERE Linkno=" & Linkno)
            If dt.Rows.Count = 0 Then
                _objcmnbLayer._saveDatawithOutParm("DELETE FROM AccTrCmn WHERE Linkno=" & Linkno)
            End If
        End With
        ldOpeningBalance()
    End Sub
    Private Function validateEntry() As Boolean
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT UnqNo,LinkNo,Reference FROM AccTrDet")
        validateEntry = True
        Dim i As Integer
        Dim col As Integer
        Dim _currw As Integer
        With grdVoucher
            For i = 0 To .Rows.Count - 1
                _currw = i
                If Not chkDate(.Item(ConstChqdate, i).Value) Or .Item(ConstChq, i).Value = "" Or .Item(constBank, i).Value = "" Then
                    If ((Trim(.Item(constBank, i).Value = ""))) Then
                        Dim _chkbankCode As String
                        _chkbankCode = .Item(constBank, i).Value
                        GoTo invalidbankCode
                    Else
                        GoTo invalidPDC
                    End If

                End If
                If chkDate(.Item(ConstTrdate, i).Value) Then
                    If DateValue(.Item(ConstTrdate, i).Value) >= DateFrom Then GoTo invalidDt
                Else
                    GoTo invalidDt
                End If

                If ((.Item(ConstAmount, i).Value) = 0) Then

                    GoTo invalidAmount
                End If

                If ((Trim(.Item(ConstParty, i).Value & "") = "")) Then

                    GoTo invalidParty
                ElseIf IsDBNull(.Item(ConstParty, i).Value) Then
                    GoTo invalidParty
                Else

                End If

                If IsDBNull(.Item(ConstAmount, i).Value) Then .Item(ConstAmount, i).Value = 0
            Next
        End With
        Exit Function
invalidDt:
        MsgBox("Invalid Transaction Date, should be prior to Accounting Period", MsgBoxStyle.Critical)
        col = ConstTrdate
        grdVoucher.CurrentCell = grdVoucher.Item(ConstTrdate, _currw)

        grdVoucher.BeginEdit(True)
        grdVoucher.Select()
        GoTo ext
invalidbankCode:
        MsgBox("Invalid Bank Code", MsgBoxStyle.Critical)
        col = constBank
        Dim cmb As New DataGridViewComboBoxColumn
        GoTo ext
invalidAmount:
        MsgBox("Amount is found empty", MsgBoxStyle.Critical)
        col = ConstAmount
        grdVoucher.CurrentCell = grdVoucher.Item(ConstAmount, _currw)
        grdVoucher.BeginEdit(True)
        grdVoucher.CurrentCell.Style.BackColor = Color.Yellow


        GoTo ext
invalidPDC:
        MsgBox("Invalid PDC Entry", MsgBoxStyle.Critical)
        col = ConstChq
        grdVoucher.CurrentCell = grdVoucher.Item(ConstChq, _currw)

        grdVoucher.CurrentCell.Style.BackColor = Color.Yellow

        grdVoucher.BeginEdit(True)
        GoTo ext
invalidParty:
        MsgBox("Invalid Party Account", MsgBoxStyle.Critical)
        col = ConstParty
        grdVoucher.CurrentCell = grdVoucher.Item(ConstParty, _currw)

        grdVoucher.CurrentCell.Style.BackColor = Color.Yellow

        grdVoucher.BeginEdit(True)
        GoTo ext
invalidRef:
        MsgBox("Invalid / Duplicate Reference", MsgBoxStyle.Critical)
        col = ConstRef
        grdVoucher.CurrentCell = grdVoucher.Item(ConstRef, _currw)

        grdVoucher.CurrentCell.Style.BackColor = Color.Yellow

        grdVoucher.BeginEdit(True)
ext:
        validateEntry = False
        With grdVoucher
            .Select()
            .CurrentCell = .Item(col, i)
            activecontrolname = "grdVoucher"
        End With
    End Function
    Private Sub saveTrans(ByVal _row As Integer)
        Try
            Dim LinkNo As String = 0
            Dim UnqNo As Integer = 0
            Dim FCRt As Double
            With grdVoucher
                _ObjAcc.JVType = "OB"
                _ObjAcc.JVDate = DateValue(.Item(ConstTrdate, _row).Value)
                _ObjAcc.PreFix = ""
                _ObjAcc.JVNum = 0
                _ObjAcc.CrtDtTm = getServerDate()
                _ObjAcc.JVTypeNo = 0
                _ObjAcc.SMan = ""
                If Val(.Item(ConstTrid, _row).Value) = 0 Then
                    _ObjAcc.UserId = CurrentUser
                Else
                    _ObjAcc.UserId = CurrentUser
                End If
                _ObjAcc.VrDescr = "OPENING BALANCE"
                _ObjAcc.isLinkNo = True
                _ObjAcc.IsModi = IIf(Val(.Item(ConstTrid, _row).Value) > 0, 1, 0)
                _ObjAcc.LinkNo = Val(.Item(ConstTrid, _row).Value)

                LinkNo = _ObjAcc.SaveAccTrCmn()  ' New Code Added By Ashok
                If IsDBNull(.Item(ConstAmount, _row).Value) Then .Item(ConstAmount, _row).Value = 0
                If IsDBNull(.Item(ConstFCRate, _row).Value) Then .Item(ConstFCRate, _row).Value = 0
                If IsDBNull(.Item(ConstFCAmount, _row).Value) Then .Item(ConstFCAmount, _row).Value = 0
                FCRt = CDbl(.Item(ConstFCRate, _row).Value)
                setAcctrDetValue(LinkNo, Val(accid), _row)
                _ObjAcc.saveAccTrans()

            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub setAcctrDetValue(ByVal lnkNo As Long, ByVal AccountNo As Long, ByVal _row As Integer)
        With _ObjAcc
            .trLinkNo = lnkNo
            .AccountNo = AccountNo
            .Reference = Trim(grdVoucher.Item(ConstRef, _row).Value & "")
            .EntryRef = Trim(grdVoucher.Item(ConstDesc, _row).Value & "")
            .DealAmt = CDbl(grdVoucher.Item(ConstAmount, _row).Value) * IIf(grdVoucher.Item(ConstDtype, _row).Value = "Dr", 1, -1)
            .FCAmt = CDbl(grdVoucher.Item(ConstFCAmount, _row).Value) * IIf(grdVoucher.Item(ConstDtype, _row).Value = "Dr", 1, -1)
            .JobCode = ""
            .JobStr = ""
            .CurrRate = CDbl(grdVoucher.Item(ConstFCRate, _row).Value)
            .CurrencyCode = Trim(grdVoucher.Item(ConstFCName, _row).Value & "")
            .TrInf = 0
            .OthCost = 0
            .TermsId = ""
            .CustAcc = 0
            .AccWithRef = 0
            .UnqNo = Val(grdVoucher.Item(ConstUnq, _row).Value)
            .BankCode = grdVoucher.Item(constBank, _row).Value
            .ChqNo = grdVoucher.Item(ConstChq, _row).Value
            If chkDate(grdVoucher.Item(ConstChqdate, _row).Value) Then
                .ChqDate = DateValue(grdVoucher.Item(ConstChqdate, _row).Value)
            End If
            .PDCAcc = Val(grdVoucher.Item(ConstCustHead, _row).Value)
            .LPONo = ""
            'Dim dtDue As Date = IIf(chkDate(grdVoucher.Item(ConstDueDate, _row).Value), grdVoucher.Item(ConstDueDate, _row).Value, DateValue(grdVoucher.Item(ConstTrdate, _row).Value))
            .DocDate = Date.Now
            .SuppInvDate = Date.Now
            .DueDate = Date.Now
            .VesselId = ""
        End With
    End Sub
    Private Sub updateOB()
        Dim i As Integer
        For i = 0 To grdVoucher.RowCount - 1
            saveTrans(i)
        Next
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT acctrcmn.Linkno from acctrcmn left join acctrdet on acctrcmn.linkno=acctrdet.linkno where jvtype='OB' AND accountno=" & accid)
        If dt.Rows.Count > 0 Then
            _objcmnbLayer._saveDatawithOutParm("update accmast set isobdet=1,opnbal=" & CDbl(numopnBal.Text) & " where accid=" & accid)
        End If
    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            Const WM_KEYDOWN As Integer = &H100
            Const WM_KEYUP As Integer = &H101
            msg.ToString()
            If msg.Msg = WM_KEYDOWN Or msg.Msg = WM_KEYUP Then
                If activecontrolname = "grdVoucher" Then
                    If msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up) Or msg.WParam.ToInt32() = CInt(Keys.Enter) Or msg.WParam.ToInt32() = CInt(Keys.F2) Or msg.WParam.ToInt32() = CInt(Keys.F3) Or msg.WParam.ToInt32() = CInt(Keys.F4) Then
                        grdVoucher_KeyDown(Nothing, New KeyEventArgs(keyData))
                        Return True
                    End If
                End If
            End If
            If (msg.WParam.ToInt32() = CInt(Keys.F3)) Then
                AddRow()

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function

    Private Sub grdVoucher_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellClick

    End Sub

    Private Sub grdVoucher_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdVoucher.CellFormatting
        If grdVoucher.Rows.Count = 0 Then Exit Sub
        If e.ColumnIndex = ConstFCRate Or e.ColumnIndex = ConstFCAmount Then
            If IsDBNull(grdVoucher.Item(ConstFCDec, e.RowIndex).Value) Then grdVoucher.Item(ConstFCDec, e.RowIndex).Value = 2
            e.Value = String.Format("{0:F" & Val(grdVoucher.Item(ConstFCDec, e.RowIndex).Value) & "}", Val(e.Value))
        ElseIf e.ColumnIndex = ConstAmount Then
            e.Value = String.Format("{0:F" & Val(NoOfDecimal) & "}", Val(e.Value))
        End If
    End Sub

    Private Sub grdVoucher_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles grdVoucher.DataError

    End Sub

    Private Sub grdVoucher_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdVoucher.EditingControlShowing
        Dim Col As Integer
        Col = grdVoucher.CurrentCell.ColumnIndex
        If Col = ConstAmount Or Col = ConstFCAmount Or Col = ConstFCRate Then
            Dim tb As TextBox = CType(e.Control, TextBox)
            RemoveHandler tb.TextChanged, AddressOf Textbox_TextChanged
            AddHandler tb.TextChanged, AddressOf Textbox_TextChanged

            RemoveHandler tb.KeyPress, AddressOf Textbox_KeyPress
            AddHandler tb.KeyPress, AddressOf Textbox_KeyPress
        End If
    End Sub
    Private Sub Textbox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            Dim col As Integer
            col = grdVoucher.CurrentCell.ColumnIndex
            If col = ConstAmount Or col = ConstFCAmount Or col = ConstFCRate Then
                If Not IsNumeric(e.KeyChar) And e.KeyChar <> "." And Not e.KeyChar = Convert.ToChar(Keys.Back) Then e.Handled = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub Textbox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ChgByPrg Then Exit Sub
            Dim col As Integer
            col = grdVoucher.CurrentCell.ColumnIndex
            Dim MyCtrl As TextBox = sender
            cmdOk.Enabled = True
            If col = ConstAmount Then
                With grdVoucher
                    If .Rows.Count > 0 Then
                        If Not IsDBNull(.Item(ConstFCRate, .CurrentRow.Index).Value) And MyCtrl.Text <> "" Then
                            .Item(ConstFCAmount, .CurrentRow.Index).Value = CDbl(MyCtrl.Text) / IIf(CDbl(.Item(ConstFCRate, .CurrentRow.Index).Value) > 0, CDbl(.Item(ConstFCRate, .CurrentRow.Index).Value), 1)
                        Else
                            If MyCtrl.Text <> "" Then .Item(ConstFCAmount, .CurrentRow.Index).Value = CDbl(MyCtrl.Text)
                        End If
                    End If
                End With
            ElseIf col = ConstFCAmount Then
                With grdVoucher
                    If .Rows.Count > 0 Then
                        If Not IsDBNull(.Item(ConstFCRate, .CurrentRow.Index).Value) And MyCtrl.Text <> "" Then
                            .Item(ConstAmount, .CurrentRow.Index).Value = CDbl(MyCtrl.Text) * IIf(CDbl(.Item(ConstFCRate, .CurrentRow.Index).Value) > 0, CDbl(.Item(ConstFCRate, .CurrentRow.Index).Value), 1)
                        Else
                            If MyCtrl.Text <> "" Then .Item(ConstAmount, .CurrentRow.Index).Value = CDbl(MyCtrl.Text)
                        End If
                    End If
                End With
            ElseIf col = ConstFCRate Then
                With grdVoucher
                    If .Rows.Count > 0 Then
                        If Not IsDBNull(.Item(ConstFCAmount, .CurrentRow.Index).Value) And MyCtrl.Text <> "" And MyCtrl.Text <> "." Then
                            .Item(ConstAmount, .CurrentRow.Index).Value = CDbl(.Item(ConstFCAmount, .CurrentRow.Index).Value) * IIf(CDbl(MyCtrl.Text) > 0, CDbl(MyCtrl.Text), 1)
                        End If
                    End If
                End With
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub grdVoucher_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdVoucher.KeyDown
        Try

            If e.KeyCode = Keys.Enter Then
                If grdVoucher.RowCount = 0 Then Exit Sub
                With grdVoucher
                    FindNextCell(grdVoucher, grdVoucher.CurrentCell.RowIndex, grdVoucher.CurrentCell.ColumnIndex + 1)
                    BeginEdit()
                End With
            ElseIf e.KeyCode = Keys.F2 Then
                If grdVoucher.RowCount = 0 Then Exit Sub
                Select Case grdVoucher.CurrentCell.ColumnIndex
                    Case ConstParty
                        ldSelect(2)
                End Select
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub ldSelect(ByVal BVal As Single)
        fSelect = New Selectfrm
        SetForm(fSelect, BVal)
        fSelect.Show()
    End Sub

    Private Sub fSelect_doSelect(ByVal KeyId As String, ByVal strFld1 As String, ByVal strFld2 As String) Handles fSelect.doSelect
        ChgByPrg = True
        With grdVoucher
            Select Case .CurrentCell.ColumnIndex
                Case ConstParty
                    .Item(ConstParty, .CurrentRow.Index).Value = strFld1
                    .Item(ConstCustHead, .CurrentRow.Index).Value = KeyId
            End Select
            FindNextCell(grdVoucher, .CurrentCell.RowIndex, .CurrentCell.ColumnIndex + 1)
            BeginEdit()
        End With
        ChgByPrg = False
    End Sub

    Private Sub fSelect_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fSelect.FormClosed
        fSelect = Nothing
    End Sub

    Private Sub CreateAcc_Unload() Handles Me.Unload
        If Not fSelect Is Nothing Then fSelect.Close() : fSelect = Nothing
    End Sub

    Private Sub grdVoucher_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellContentClick

    End Sub

    Private Sub grdVoucher_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellValidated
        If ChgByPrg Then Exit Sub
        Valid(e.RowIndex, e.ColumnIndex)
    End Sub
    Private Sub Valid(ByVal RowIndex As Integer, ByVal ColIndex As Integer)
        Dim chkDatatable As DataTable
        Dim SrchText As String = ""
        With grdVoucher
            Select Case ColIndex
                Case ConstFCName
                    If SrchText = "" And Not IsDBNull(.Item(ConstFCName, .CurrentCell.RowIndex).Value) Then
                        SrchText = grdVoucher.Item(ConstFCName, .CurrentCell.RowIndex).Value
                    End If
                    chkDatatable = EntriesValidation(SrchText, 2)
                    If chkDatatable.Rows.Count > 0 Then
                        .Item(ConstFCName, .CurrentCell.RowIndex).Value = SrchText
                        .Item(ConstFCRate, .CurrentCell.RowIndex).Value = Format(chkDatatable(0)("CurrencyRate"), "0.00")
                        .Item(ConstFCDec, .CurrentCell.RowIndex).Value = chkDatatable(0)("Decimal Places")
                    Else
                        .Item(ConstFCName, .CurrentCell.RowIndex).Value = ""
                        .Item(ConstFCRate, .CurrentCell.RowIndex).Value = Format(1, "0.00")
                        .Item(ConstFCDec, .CurrentCell.RowIndex).Value = 2
                        If Not IsDBNull(.Item(ConstFCAmount, .CurrentRow.Index).Value) And Not IsDBNull(.Item(ConstFCRate, .CurrentRow.Index).Value) Then
                            .Item(ConstAmount, .CurrentRow.Index).Value = CDbl(.Item(ConstFCAmount, .CurrentRow.Index).Value) * IIf(CDbl(.Item(ConstFCRate, .CurrentCell.RowIndex).Value) > 0, CDbl(.Item(ConstFCRate, .CurrentCell.RowIndex).Value), 1)
                        End If
                    End If
            End Select
            Select Case ColIndex
                Case ConstFCName, ConstFCAmount, ConstFCRate, ConstAmount
                    calCulate()
            End Select
        End With
    End Sub
    Private Sub calCulate()
        Dim ttl As Double
        With grdVoucher
            For i = 0 To .RowCount - 1
                ttl = ttl + CDbl(.Item(ConstAmount, i).Value) * IIf(.Item(ConstDtype, i).Value = "Dr", 1, -1)
            Next
        End With
        numopnBal.Text = Format(ttl, numFormat)
    End Sub
    Private Sub ldOpeningBalance()
        Try
            Dim dt As New DataTable
            dt = _ObjAcc.returnOpeningbalanceInPDC(accid)
            SetGridHead()
            grdVoucher.Rows.Clear()
            For i = 0 To dt.Rows.Count - 1
                With grdVoucher
                    .Rows.Add()
                    .Item(ConstFCDec, i).Value = dt(i)("Decimal Places")
                    .Item(ConstChq, i).Value = Trim(dt(i)("ChqNo") & "")
                    .Item(ConstChqdate, i).Value = dt(i)("ChqDate")

                    .Item(constBank, i).Value = Trim(dt(i)("BankCode") & "")
                    .Item(ConstDtype, i).Value = IIf(dt(i)("DealAmt") > 0, "Dr", "Cr")
                    .Item(ConstAmount, i).Value = dt(i)("DealAmt") * IIf(dt(i)("DealAmt") > 0, 1, -1)
                    .Item(ConstParty, i).Value = Trim(dt(i)("Alias") & "")
                    .Item(ConstRef, i).Value = Trim(dt(i)("Reference") & "")
                    .Item(ConstTrdate, i).Value = dt(i)("JVDate")
                    .Item(ConstDesc, i).Value = dt(i)("EntryRef")
                    .Item(ConstFCName, i).Value = Trim(dt(i)("CurrencyCode") & "")
                    If Not IsDBNull(dt(i)("FCAmt")) Then
                        .Item(ConstFCAmount, i).Value = dt(i)("FCAmt") * IIf(dt(i)("DealAmt") > 0, 1, -1)
                    Else
                        .Item(ConstFCAmount, i).Value = dt(i)("DealAmt")
                    End If
                    If Not IsDBNull(dt(i)("CurrRate")) Then
                        .Item(ConstFCRate, i).Value = dt(i)("CurrRate")
                    Else
                        dt(i)("CurrRate") = 1
                    End If

                    .Item(ConstTransfer, i).Value = IIf(Trim(dt(i)("PDCStatus") & "") = "Y", "YES", "NO")
                    If Not IsDBNull(dt(i)("PDCAcc")) Then
                        .Item(ConstCustHead, i).Value = dt(i)("PDCAcc")
                    Else
                        .Item(ConstCustHead, i).Value = 0
                    End If
                    .Item(ConstTrid, i).Value = dt(i)("LinkNo")
                    .Item(ConstPAmt, i).Value = .Item(ConstAmount, i).Value
                    .Item(ConstUnq, i).Value = dt(i)("UnqNo")
                    If Trim(dt(i)("PDCStatus") & "") = "Y" Then
                        .Rows(i).ReadOnly = True
                        .Rows(i).DefaultCellStyle.BackColor = Color.LightCyan
                    End If
                End With
            Next
            If dt.Rows.Count > 0 Then
                Panel4.Visible = True
                numopnBal.Enabled = False
                calCulate()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub grdVoucher_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.DoubleClick
        If grdVoucher.Rows.Count > 0 Then grdVoucher.BeginEdit(True)
    End Sub

    Private Sub grdVoucher_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.SelectionChanged
        Try
            If grdVoucher.Rows.Count = 0 Then Exit Sub
            BeginEdit()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub grdVoucher_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellEndEdit
        cmdOk.Enabled = True
    End Sub

    Private Sub numopnBal_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numopnBal.TextChanged
        If ChgByPrg Then Exit Sub
        cmdOk.Enabled = True
    End Sub
    Private Sub ldTitle()
        Dim dtTable As DataTable
        dtTable = _objcmnbLayer._fldDatatable("select * from (SELECT 'Nature'=case " & _
                                                      "when MAccId>=1000 and MAccId<2000 then 'Assets'" & _
                                                      "when MAccId>=2000 and MAccId<3000 then 'Liability'" & _
                                                      "when MAccId>=3000 and MAccId<4000 then 'Equity'" & _
                                                      "when MAccId>=4000 and MAccId<5000 then 'Income'" & _
                                                      "when MAccId>=5000 and MAccId<6000 then 'Indirect Income'" & _
                                                      "when MAccId>=6000 and MAccId<7000 then 'Exp.Direct'" & _
                                                      "when MAccId>=7000 and MAccId<8000 then 'Exp.Indirect'" & _
                                                      "End ,Descr,MAccId FROM MAccHd) tr")
        Dim i As Integer
        cmbtitle.Items.Clear()
        For i = 0 To dtTable.Rows.Count - 1
            cmbtitle.Items.Add(dtTable(i)("Descr"))
        Next
        If cmbtitle.Items.Count > 0 Then
            cmbtitle.SelectedIndex = 0
        End If
        If Condition <> "" Then
            dtTable = _objcmnbLayer._fldDatatable("SELECT MAccHd.MAccId,MAccHd.Descr,S1AccHd.Descr s1Descr FROM S1AccHd left join MAccHd on S1AccHd.MAccId=MAccHd.MAccId WHERE " & Condition & " ORDER BY MAccHd.Descr")
            If dtTable.Rows.Count > 0 Then
                'chgByPgm = True
                cmbtitle.Text = dtTable(0)("Descr")
                cmbAccGroup.Text = dtTable(0)("s1Descr")
                'chgByPgm = False
            End If
        End If
    End Sub

    Private Sub cmbtitle_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbtitle.SelectedIndexChanged
        'If chgByPgm Then Exit Sub
        ldGroup()
    End Sub
    Private Sub ldGroup()
        Dim dtTable As DataTable
        cmbtitle.Tag = ""
        dtTable = _objcmnbLayer._fldDatatable("select MAccId from MAccHd where Descr='" & cmbtitle.Text & "'")
        If dtTable.Rows.Count > 0 Then
            cmbtitle.Tag = dtTable(0)("MAccId")
            AddtoCombo(cmbAccGroup, "SELECT Descr, S1AccId FROM S1AccHd WHERE GrpSetOn NOT In ('Customer','Supplier') and MAccId=" & Val(cmbtitle.Tag) & " ORDER BY Descr", False, True)
        End If
        If cmbAccGroup.Items.Count > 0 Then cmbAccGroup.SelectedIndex = 0
    End Sub

    Private Sub txtmalayalam_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtmalayalam.TextChanged
        If ChgByPrg Then Exit Sub
        cmdOk.Enabled = True
    End Sub

    Private Sub txtRec1_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRec1.Validated
        txtRec1.Text = UCase(txtRec1.Text)
    End Sub

End Class