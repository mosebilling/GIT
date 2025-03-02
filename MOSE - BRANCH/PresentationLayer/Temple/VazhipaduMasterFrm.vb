
Public Class VazhipaduMasterFrm
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
    Private Chgamt As Boolean
    Private ChgAddr As Boolean
    Private _vtable As DataTable
    Private rNo As Integer
    Private _fcTable As DataTable
    Private _slsManTable As DataTable
    Private activecontrolname As String
    Private accid As Long

    'const declarations for opening balance
    Private Const ConstRef = 0
    Private Const ConstTrdate = 1
    Private Const ConstDtype = 2
    Private Const ConstAmount = 3
    Private Const ConstDesc = 4
    Private Const ConstFCName = 5
    Private Const ConstFCAmount = 6
    Private Const ConstVessel = 7
    Private Const ConstFCRate = 8
    Private Const ConstSlsman = 9
    Private Const ConstDueDate = 10
    Private Const ConstTrid = 11
    Private Const ConstFCDec = 12
    Private Const ConstUnqNo = 13
    '****************

    Private Const ConstAlias = 0
    Private Const ConstDescription = 1
    Private Const Constopb = 2
    Private Const Constclosing = 3
    Private Const ConstPhone = 4
    Private Const ConstContactName = 5
    Private Const constEMail = 6
    Private Const constTrdLcno = 7
    Private Const constTrdDate = 8
    Private Const ConstAccounNo = 9
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
    Dim chngprg As Boolean

    Private Sub ldAccounts(ByVal AccNo As Long)
        Dim dtTable As DataTable
        SetControlsInDetailTab()
        'dtTable = _objcmnbLayer._fldDatatable("SELECT * FROM AccMast LEFT JOIN AccMastAddr ON AccMast.AccountNo=AccMastAddr.AccountNo LEFT JOIN JobTb ON AccMast.AccountNo=JobTb.custcode WHERE AccMast.AccountNo = " & AccNo)
        _ObjAcc.AccountNo = AccNo
        Dim ds As DataSet
        ds = _ObjAcc.returnAccountDetailsWithJob
        dtTable = ds.Tables(0)
        isModi = True
        txtRec0.Tag = dtTable(0)("AccountNo")
        txtRec0.Text = Trim("" & dtTable(0)("Alias"))
        txtRec1.Text = Trim("" & dtTable(0)("AccDescr"))
        txtmalayalam.Text = Trim("" & dtTable(0)("nameMalayalam"))
        cmbtimes.Text = Trim("" & dtTable(0)("VazhipaduTimes"))
        txtnada.Text = Trim("" & dtTable(0)("VazhipaduNada"))
        txtrate.Text = Format(Val("" & dtTable(0)("VazhipaduRate")), numFormat)
        txtT.Text = Format(Val("" & dtTable(0)("vazhipadurateThirumeni")), numFormat)
        txtK.Text = Format(Val("" & dtTable(0)("vazhipadurateKazhakam")), numFormat)
        If Val("" & dtTable(0)("isTempleNonVazhipaduItem")) > 0 Then
            chkvazhipadu.Checked = True
        Else
            chkvazhipadu.Checked = False
        End If
        accid = dtTable(0)("accid")
        txtnada.ReadOnly = False
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
    Private Function validateEntry() As Boolean


        Dim dt As DataTable
        Dim _currw As Integer


        validateEntry = True
        Dim i As Integer
        Dim j As Integer
        Dim col As Integer
        With grdVoucher
            For i = 0 To .Rows.Count - 1
                _currw = i
                If (iscust) Then
                    dt = _objcmnbLayer._fldDatatable("SELECT UnqNo,LinkNo,Reference FROM AccTrDet where Reference='" & .Item(0, i).Value & "' And AccountNo=" & Val(accid))
                Else
                    dt = _objcmnbLayer._fldDatatable("SELECT UnqNo,LinkNo,Reference FROM AccTrDet where Reference='" & .Item(0, i).Value & "' And AccountNo=" & Val(accid))
                End If

                If chkDate(.Item(ConstTrdate, i).Value) Then
                    If DateValue(.Item(ConstTrdate, i).Value) >= DateFrom Then GoTo invalidDt
                    .CurrentCell = .Item(ConstTrdate, i)
                    .Select()
                    .BeginEdit(True)
                Else
                    GoTo invalidDt
                End If
                If IsDBNull(.Item(ConstAmount, i).Value) Then .Item(ConstAmount, i).Value = 0
                If CDbl(.Item(ConstAmount, i).Value) = 0 Then GoTo invalidAmount
                Dim _qry = From job In dt.AsEnumerable() Where job![Reference] = grdVoucher.Item(ConstRef, i).Value And job![LinkNo] <> Val(grdVoucher.Item(ConstTrid, i).Value & "") Select New With _
                       {.Name = job!UnqNo}
                If Not IsDBNull(_qry) Then
                    On Error Resume Next
                    If _qry.Count > 0 Then GoTo invalidRef
                End If
                For j = 0 To .Rows.Count - 1
                    If j <> i Then
                        If Trim(.Item(ConstRef, i).Value & "") = Trim(.Item(ConstRef, j).Value & "") Then i = j : GoTo invalidRef
                    End If
                Next
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
invalidAmount:
        MsgBox("Invalid Amount", MsgBoxStyle.Critical)
        col = ConstAmount
        GoTo ext
invalidRef:
        MsgBox("Invalid / Duplicate Reference", MsgBoxStyle.Critical)
        col = ConstRef
ext:
        validateEntry = False
        With grdVoucher
            .Select()
            .CurrentCell = .Item(col, i)
            activecontrolname = "grdVoucher"
        End With


    End Function
    Private Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        If Val(btnaddnew.Tag) = 0 And Val(btnmodify.Tag) = 0 Then
            MsgBox("This user do not have rights to update", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If Not chkDuplicate() Then Exit Sub
        If txtRec0.Text = "" Then
            If iscust Then
                MsgBox("Vazhipadu Customer Code", MsgBoxStyle.Critical)
            Else
                MsgBox("Invalid Vazhipadu Code", MsgBoxStyle.Critical)
            End If

            txtRec0.Focus()
            Exit Sub
        End If
        If txtRec1.Text = "" Then
            If iscust Then
                MsgBox("Invalid Vazhipadu Name", MsgBoxStyle.Critical)
            Else
                MsgBox("Invalid Vazhipadu Name", MsgBoxStyle.Critical)
            End If
            txtRec1.Focus()
            Exit Sub
        End If
        If isModi Then
           ModiAcc()
        Else
            'Dim AccMastSrch As DataTable
            'AccMastSrch = _objcmnbLayer._fldDatatable("Select * from Accmast ")
            If cmbAccGroup.SelectedIndex < 0 Or cmbAccGroup.Text = "" Then
                MsgBox("Choose a valid Account Group !!", vbExclamation)
                cmbAccGroup.Focus()
                Exit Sub
            End If
            saveAcc
        End If
        MsgBox("Record Updated Successfully", MsgBoxStyle.Information)
        btnaddnew_Click(btnaddnew, New System.EventArgs)
        ldVazhipadu()
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
        If Val(numopnBal.Text) = 0 Then numopnBal.Text = 0
        If Val(txtlimit.Text) = 0 Then txtlimit.Text = 0
        If Val(txtrate.Text) = 0 Then txtrate.Text = 0
        If Val(txtT.Text) = 0 Then txtT.Text = 0
        If Val(txtK.Text) = 0 Then txtK.Text = 0
        _objcmnbLayer._saveDatawithOutParm("UPDATE AccMast SET Alias = '" & MkDbSrchStr(txtRec0.Text) & "', AccDescr = '" & _
                                           MkDbSrchStr(txtRec1.Text) & "', S1AccId = '" & Val(cmbAccGroup.Items(cmbAccGroup.SelectedIndex).thedata()) & "'," & _
                                           " VazhipaduRate = " & CDbl(txtrate.Text) & "," & _
                                           " VazhipaduTimes = '" & cmbtimes.Text & "'," & _
                                           " VazhipaduNada = '" & txtnada.Text & "'," & _
                                           " nameMalayalam = N'" & txtmalayalam.Text & "'," & _
                                           " vazhipadurateThirumeni = " & CDbl(txtT.Text) & "," & _
                                           " vazhipadurateKazhakam = " & CDbl(txtK.Text) & "," & _
                                           " isTempleNonVazhipaduItem=" & IIf(chkvazhipadu.Checked, 1, 0) & _
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
        Chgamt = True
        numopnBal.Text = Format(0, numFormat)
        Chgamt = False
        cmbtimes.Text = defaultState
        chkvazhipadu.Checked = False
        'dtpdate.CtlText = dtEmpty
        txtlcno.Text = ""
        ChgByPrg = False
    End Sub

    Private Function saveAcc() As Boolean
        saveAcc = True
        If numopnBal.Text = "" Then numopnBal.Text = 0
        If Not isModi Then
            GenerateNext(cmbAccGroup.Text, cmbAccGroup.Items(cmbAccGroup.SelectedIndex).thedata)
            If Val(numopnBal.Text) = 0 Then numopnBal.Text = 0
            If Val(txtlimit.Text) = 0 Then txtlimit.Text = 0
            If Val(txtrate.Text) = 0 Then txtrate.Text = 0
            If Val(txtT.Text) = 0 Then txtT.Text = 0
            If Val(txtK.Text) = 0 Then txtK.Text = 0
            _objcmnbLayer._saveDatawithOutParm("INSERT INTO AccMast (AccountNo, Alias, AccDescr, S1AccId,VazhipaduRate,VazhipaduTimes,VazhipaduNada,nameMalayalam,vazhipadurateThirumeni,vazhipadurateKazhakam,isTempleNonVazhipaduItem) VALUES (" & _
                                               Val(txtRec0.Tag) & ", '" & MkDbSrchStr(Trim(txtRec0.Text)) & "', '" & _
                                               MkDbSrchStr(Trim(txtRec1.Text)) & "', " & cmbAccGroup.Items(cmbAccGroup.SelectedIndex).thedata() & "," & _
                                               CDbl(txtrate.Text) & ",'" & cmbtimes.Text & "','" & txtnada.Text & "',N'" & txtmalayalam.Text & "'," & _
                                               CDbl(txtT.Text) & "," & CDbl(txtK.Text) & "," & IIf(chkvazhipadu.Checked, 1, 0) & ")")
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
    Private Sub ldState()
        Dim dt As DataTable = _objcmnbLayer._fldDatatable("SELECT statecode FROM StateMasterTb", False)
        cmbtimes.Items.Clear()
        cmbtimes.Items.Add("")
        Dim i As Integer
        For i = 0 To dt.Rows.Count - 1
            cmbtimes.Items.Add(dt(i)("statecode"))
        Next
    End Sub
    Private Sub CreateAccNew_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddtoCombo(cmbAccGroup, "SELECT Descr, S1AccId FROM S1AccHd " & IIf(Condition = "", "", "WHERE " & Condition) & " ORDER BY Descr", False, True)
        If Not userType Then
            btnaddnew.Tag = 1
            btnmodify.Tag = 1
            btnremove.Tag = 1
        Else
            btnaddnew.Tag = IIf(getRight(137, CurrentUser), 1, 0)
            btnmodify.Tag = IIf(getRight(138, CurrentUser), 1, 0)
            btnremove.Tag = IIf(getRight(139, CurrentUser), 1, 0)
        End If
        ldVazhipadu()
        '_fcTable = _objcmnbLayer._fldDatatable("SELECT CurrencyCode,CurrencyRate,[Decimal Places] FROM CurrencyTb")
        '_slsManTable = _objcmnbLayer._fldDatatable("SELECT SManCode FROM SalesmanTb")
        'ldState()
        'cmbstate.Text = defaultState
        'If iscust Then
        '    ldCustomer()
        '    Label3.Text = "Cust Code"
        '    Label4.Text = "Cust Name"
        '    lblname.Text = "CUSTOMER MASTER"
        '    If Not userType Then
        '        btnaddnew.Tag = 1
        '        btnmodify.Tag = 1
        '        btnremove.Tag = 1
        '    Else
        '        btnaddnew.Tag = IIf(getRight(10, CurrentUser), 1, 0)
        '        btnmodify.Tag = IIf(getRight(11, CurrentUser), 1, 0)
        '        btnremove.Tag = IIf(getRight(12, CurrentUser), 1, 0)
        '    End If
        'Else
        '    ldSupplier()
        '    'Panel3.Visible = True
        '    'grppayment.Visible = True
        '    Label3.Text = "Supp Code"
        '    Label4.Text = "Supp Name"
        '    lblname.Text = "SUPPLIER MASTER"
        '    If userType = 0 Or userType = 2 Then
        '        btnaddnew.Tag = 1
        '        btnmodify.Tag = 1
        '        btnremove.Tag = 1
        '    Else
        '        btnaddnew.Tag = IIf(getRight(6, CurrentUser), 1, 0)
        '        btnmodify.Tag = IIf(getRight(7, CurrentUser), 1, 0)
        '        btnremove.Tag = IIf(getRight(8, CurrentUser), 1, 0)
        '    End If
        'End If

        cmdOk.Enabled = False
        cmbAccGroup.SelectedIndex = 0

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
            Dim dt As Date
            dt = DateValue("01/01/1950")
            If txtnada.Text <> "" Then
                _objcmnbLayer._saveDatawithOutParm("INSERT INTO AccMastAddr (AccountNo,Address1,Address2,Address3,Address4,Phone,Fax,ContactName,EMail,Website," & _
                                                                "TrdLcno,TrdDate,GSTIN) VALUES(" & _
                                                                   Val(accid) & ",'" & _
                                                                   MkDbSrchStr(txtnada.Text) & "','" & _
                                                                   MkDbSrchStr(txtAddr2.Text) & "','" & _
                                                                   MkDbSrchStr(txtAddr2.Text) & "','" & _
                                                                   MkDbSrchStr(txtAddr3.Text) & "','" & _
                                                                   MkDbSrchStr(txtphone.Text) & "','" & _
                                                                   MkDbSrchStr(txtFax.Text) & "','" & _
                                                                   MkDbSrchStr(txtcontactname.Text) & "','" & _
                                                                   MkDbSrchStr(txtemail.Text) & "','" & _
                                                                   MkDbSrchStr(txtwebsite.Text) & "','" & _
                                                                   MkDbSrchStr(txtlcno.Text) & "','" & _
                                                                   Format(DateValue(dt), "yyyy/MM/dd") & "','" & _
                                                                   MkDbSrchStr(txtgstin.Text) & "')")
            End If
            updateOB()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub btnaddnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddnew.Click
        If Val(btnaddnew.Tag) = 0 Then
            MsgBox("This user do not have permission to add new entry", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If btnaddnew.Text = "&Add New" Then
            btnaddnew.Text = "Undo"
            btnadd.Enabled = True
            Panel1.Enabled = True
            numopnBal.Enabled = True
            btnmodify.Enabled = False
            cmdOk.Enabled = False
            grdVoucher.Rows.Clear()
            accid = 0
            isModi = False
            SetControlsInDetailTab()
            txtRec1.Focus()
            If Not isModi Then
                If cmbAccGroup.Items.Count = 0 Then Exit Sub
                txtRec0.Text = GenerateNext((cmbAccGroup.Text), cmbAccGroup.Items(cmbAccGroup.SelectedIndex).thedata)
            Else
                txtRec1.Focus()
            End If
        Else
            SetControlsInDetailTab()
            txtRec0.Focus()
            btnmodify.Enabled = False
            cmdOk.Enabled = False
            btnaddnew.Text = "&Add New"
            Panel1.Enabled = False
            numopnBal.Enabled = False
            btnremove.Enabled = False
            grdVoucher.Rows.Clear()
            btnadd.Enabled = False
            If isModi Then
                TabControl1.SelectedIndex = 1
            End If
            isModi = False
        End If

    End Sub

    Private Sub btnmodify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnmodify.Click
        If Val(btnmodify.Tag) = 0 Then
            MsgBox("This user do not have permission to modify", MsgBoxStyle.Exclamation)
            btnadd.Enabled = False
            Exit Sub
        End If
        If btnaddnew.Text = "&Add New" Then
            btnaddnew.Text = "Undo"
            Panel1.Enabled = True
            btnadd.Enabled = True
            btnmodify.Enabled = False
            btnremove.Enabled = True
            isModi = True
            'txtAddr0.ReadOnly = False
        Else
            btnaddnew_Click(btnaddnew, New System.EventArgs)
        End If
        'txtRec0.Focus()

    End Sub

    Private Sub txtRec0_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRec0.KeyPress
        NumericTextOnKeypress(sender, e, ChgByPrg, "#")
    End Sub


    Private Sub txtRec0_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRec1.TextChanged, txtRec0.TextChanged, txtAddr3.TextChanged, txtAddr2.TextChanged, txtnada.TextChanged, txtlcno.TextChanged, txtcontactname.TextChanged, txtwebsite.TextChanged, txtemail.TextChanged, txtFax.TextChanged, txtphone.TextChanged, txtgstin.TextChanged
        If ChgByPrg Then Exit Sub
        cmdOk.Enabled = True
    End Sub

    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        cmdOk.Enabled = True
    End Sub


    Private Sub cmbAccGroup_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAccGroup.SelectedValueChanged
        ChgByPrg = True
        If Not isModi Then
            txtRec0.Text = GenerateNext((cmbAccGroup.Text), cmbAccGroup.Items(cmbAccGroup.SelectedIndex).thedata)
        Else
        End If
        ChgByPrg = False
    End Sub

    Private Sub txtRec0_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtwebsite.KeyDown, txtRec1.KeyDown, txtRec0.KeyDown, txtphone.KeyDown, txtlcno.KeyDown, txtFax.KeyDown, txtemail.KeyDown, txtcontactname.KeyDown, txtAddr3.KeyDown, txtAddr2.KeyDown, txtnada.KeyDown, txtgstin.KeyDown, txtmalayalam.KeyDown, txtnada.KeyDown
        Dim myctl As TextBox
        myctl = sender

        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")

        End If
    End Sub

    Private Sub Label8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label8.Click

    End Sub

    Private Sub cldrdate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cldrdate.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub cldrdate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cldrdate.ValueChanged
        'dtpdate.CtlText = Format(cldrdate.Value, DtFormat)
        cmdOk.Enabled = True
    End Sub

    Private Sub dtpdate_Change(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If ChgByPrg Then Exit Sub
        cmdOk.Enabled = True
    End Sub

    Private Sub dtpdate_KeyDownEvent(ByVal sender As Object, ByVal e As AxMSMask.MaskEdBoxEvents_KeyDownEvent)
        If e.keyCode = Keys.Enter Then
            numopnBal.Focus()
        End If
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    
    Private Sub ldVazhipadu()
        _vtable = _objcmnbLayer._fldDatatable("SELECT Alias,AccDescr,VazhipaduRate,VazhipaduTimes,VazhipaduNada,AccMast.AccountNo FROM AccMast " & _
                                                "left Join BalanceQr On BalanceQr.AccountNo=AccMAst.accid " & _
                                              "LEFT JOIN AccMastAddr ON AccMast.accid=AccMastAddr.AccountNo left join S1AccHd on AccMast.S1AccId=S1AccHd.S1AccId " & IIf(Condition = "", "", "WHERE " & Condition) & " ORDER BY Descr")
        grdItem.DataSource = _vtable
        SetGridCustomer()
        setComboGrid()
    End Sub


    Private Sub SetGridSupplier()
        With grdItem
            SetGridProperty(grdItem)

            .Columns(ConstAlias).HeaderText = "Sup. code"
            .Columns(ConstAlias).Width = 100
            .Columns(ConstAlias).Frozen = True

            .Columns(ConstDescription).HeaderText = "Description"
            .Columns(ConstDescription).Width = 250

            .Columns(ConstPhone).HeaderText = "Phone"
            .Columns(ConstPhone).Width = 80

            .Columns(ConstContactName).HeaderText = "ContactName"
            .Columns(ConstContactName).Width = 150

            .Columns(constEMail).HeaderText = "EMail"
            .Columns(constEMail).Width = 150

            .Columns(constTrdLcno).HeaderText = "Trd. Lcno"
            .Columns(constTrdLcno).Width = 100
            .Columns(constTrdLcno).Visible = False

            .Columns(constTrdDate).HeaderText = "Trd. Lc Date"
            .Columns(constTrdDate).Width = 90
            .Columns(constTrdDate).Visible = False

            .Columns(Constopb).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(Constopb).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(Constclosing).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(Constclosing).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


            .Columns(ConstAccounNo).Visible = False

        End With
    End Sub

    Private Sub SetGridCustomer()
        With grdItem
            SetGridProperty(grdItem)

            .Columns("Alias").HeaderText = "Code"
            .Columns("Alias").Width = 100
            .Columns("Alias").Frozen = True

            .Columns("AccDescr").HeaderText = "Description"
            .Columns("AccDescr").Width = 250

            .Columns("VazhipaduNada").HeaderText = "Nada"
            .Columns("VazhipaduNada").Width = 100

            .Columns("VazhipaduTimes").HeaderText = "Times"
            .Columns("VazhipaduTimes").Width = 100


            .Columns("AccountNo").Visible = False

            .Columns("VazhipaduRate").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("VazhipaduRate").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("VazhipaduRate").HeaderText = "Rate"

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
        lblclosing.Text = "0.00"
        Dim rate As Double
        rate = Val(grdItem.Item("VazhipaduRate", grdItem.CurrentCell.RowIndex).Value)
        txtrate.Text = Format(rate, numFormat)
        ldAccounts(AccNo)
        btnmodify_Click(btnmodify, New System.EventArgs)
        TabControl1.SelectedIndex = 0
    End Sub

    Private Sub grdItem_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItem.CellContentClick

    End Sub

    Private Sub TabControl1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.Click
        If TabControl1.SelectedIndex = 1 And btnaddnew.Text = "Undo" Then
            MsgBox("Do Undo/Update before moving to Enquiry", MsgBoxStyle.Exclamation)
            TabControl1.SelectedIndex = 0
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
        If dt.Rows.Count > 0 Then
            MsgBox("Supplier Code #" & txtRec0.Text & " having transactions, You can't remove record", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If MsgBox("You are going to remove the Vazhipadu # " & txtRec0.Text & " # Permenantly! Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
            Exit Sub
        End If
        _objcmnbLayer._saveDatawithOutParm("DELETE FROM AccMast WHERE AccountNo=" & Val(txtRec0.Tag))
        _objcmnbLayer._saveDatawithOutParm("DELETE FROM AccMastAddr WHERE AccountNo=" & Val(txtRec0.Tag))
        'ldSupplier()
        TabControl1.SelectedIndex = 1
        btnmodify_Click(btnmodify, New System.EventArgs)
    End Sub

    Private Sub dtpjdate_KeyDownEvent(ByVal sender As Object, ByVal e As AxMSMask.MaskEdBoxEvents_KeyDownEvent)
        If e.keyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub


    Private Sub dtpjdate_Change(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If ChgByPrg Then Exit Sub
        'cmdOk.Enabled = True
    End Sub


    Private Sub btncost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim fCost As New ItemCostFrm
        fCost.isSupp = False
        fCost.lblName.Text = "Cost Settings For Supplier :" & txtRec1.Text
        fCost.lblName.Tag = Val(txtRec0.Tag)
        fCost.ShowDialog()
    End Sub

    Private Sub btnAcSrch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAcSrch.Click
        rNo = RowOnGrid(rNo, grdVoucher, cmbAcOrder.SelectedIndex, txtAcSearch.Text)
        grdVoucher.ClearSelection()
        grdVoucher.Rows(rNo).Selected = True
        grdVoucher.FirstDisplayedScrollingRowIndex = rNo
        rNo = +1
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
            .ColumnCount = 14
            .ColumnHeadersHeight = 25
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke
            .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular)
            .RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.0!)

            .Columns(ConstRef).HeaderText = "INV/Ref"
            .Columns(ConstRef).Width = Me.Width * 7.5 / 100   '100
            .Columns(ConstRef).SortMode = DataGridViewColumnSortMode.NotSortable

            Dim col As New CalendarColumn()
            .Columns.RemoveAt(ConstTrdate)
            'col.DataPropertyName = "ChqDate"
            .Columns.Insert(ConstTrdate, col)
            .Columns(ConstTrdate).HeaderText = "Tr. Date"

            .Columns(ConstDtype).HeaderText = "Type"

            .Columns(ConstDtype).SortMode = DataGridViewColumnSortMode.NotSortable
            Dim cmb As New DataGridViewComboBoxColumn
            cmb.Items.Add("")
            cmb.Items.Add("Dr")
            cmb.Items.Add("Cr")
            cmb.HeaderText = "Type"
            cmb.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
            .Columns.RemoveAt(ConstDtype)
            .Columns.Insert(ConstDtype, cmb)
            .Columns(ConstDtype).Width = 50

            .Columns(ConstAmount).HeaderText = "Amount"
            .Columns(ConstAmount).Width = Me.Width * 12 / 100   '100
            .Columns(ConstAmount).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstAmount).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstAmount).DefaultCellStyle.Format = "N" & NoOfDecimal

            '.Columns(ConstVessel).HeaderText = "Vessel"
            '.Columns(ConstVessel).Width = 100
            '.Columns(ConstVessel).DisplayIndex = 8
            '.Columns(ConstVessel).SortMode = DataGridViewColumnSortMode.NotSortable
            'cmb = New DataGridViewComboBoxColumn
            'cmb.Items.Add("")
            'For i = 0 To _vesselTable.Rows.Count - 1
            '    cmb.Items.Add(_vesselTable(i)("VesselId"))
            'Next
            'cmb.HeaderText = "Vessel"
            'cmb.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
            '.Columns.RemoveAt(ConstVessel)
            '.Columns.Insert(ConstVessel, cmb)
            .Columns(ConstVessel).Visible = False

            .Columns(ConstDesc).HeaderText = "Description"
            .Columns(ConstDesc).Width = 300 'Me.Width * 10 / 100   '100
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
            cmb.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
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

            .Columns(ConstSlsman).HeaderText = "Sales Man"
            .Columns(ConstSlsman).Width = Me.Width * 5 / 100   '100
            .Columns(ConstSlsman).SortMode = DataGridViewColumnSortMode.NotSortable
            cmb = New DataGridViewComboBoxColumn
            cmb.Items.Add("")
            For i = 0 To _slsManTable.Rows.Count - 1
                cmb.Items.Add(_slsManTable(i)("SManCode"))
            Next
            cmb.HeaderText = "Sales Man"
            cmb.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
            .Columns.RemoveAt(ConstSlsman)
            .Columns.Insert(ConstSlsman, cmb)

            col = New CalendarColumn()
            .Columns.RemoveAt(ConstDueDate)
            col.DataPropertyName = "DueDate"
            .Columns.Insert(ConstDueDate, col)
            .Columns(ConstDueDate).HeaderText = "Due Date"

            .Columns(ConstTrid).HeaderText = "Trid"
            .Columns(ConstTrid).Visible = False
            .Columns(ConstFCDec).HeaderText = "Trid"
            .Columns(ConstFCDec).Visible = False
            .Columns(ConstUnqNo).HeaderText = "UnqNo"
            .Columns(ConstUnqNo).Visible = False

        End With
        setComboGridOPB()
    End Sub
    Private Sub setComboGridOPB()
        cmbAcOrder.Items.Clear()
        Dim i As Integer = 0
        For i = 0 To grdVoucher.ColumnCount - 1
            cmbAcOrder.Items.Add(grdVoucher.Columns(i).HeaderText)
        Next
        If cmbAcOrder.Items.Count > 0 Then
            cmbAcOrder.SelectedIndex = 0
        End If

        'If cmbSearch.Items.Count >= cmbShowIndex Then cmbSearch.SelectedIndex = cmbShowIndex
    End Sub
    Private Sub ldOpeningBalance()
        Dim dt As DataTable
        SetGridHead()
        dt = _objcmnbLayer._fldDatatable("SELECT * from AccTrCmn LEFT JOIN AccTrDet ON AccTrCmn.LinkNo=AccTrDet.LinkNo WHERE JVType='OB' AND AccountNo=" & accid)
        grdVoucher.Rows.Clear()
        If dt.Rows.Count > 0 Then
            Panel4.Visible = True
            numopnBal.Enabled = False
        Else
            Panel4.Visible = False
            numopnBal.Enabled = True
        End If
        For i = 0 To dt.Rows.Count - 1
            With grdVoucher
                .Rows.Add()
                .Item(ConstFCDec, i).Value = Val(setDecimal(Trim(dt(i)("CurrencyCode") & "")))
                .Item(ConstRef, i).Value = dt(i)("Reference")
                .Item(ConstTrdate, i).Value = dt(i)("JVDate")
                .Item(ConstDtype, i).Value = IIf(dt(i)("DealAmt") > 0, "Dr", "Cr")
                .Item(ConstAmount, i).Value = dt(i)("DealAmt") * IIf(dt(i)("DealAmt") > 0, 1, -1)

                .Item(ConstDesc, i).Value = dt(i)("EntryRef")
                .Item(ConstFCName, i).Value = dt(i)("CurrencyCode")
                .Item(ConstFCAmount, i).Value = dt(i)("FCAmt") * IIf(dt(i)("DealAmt") > 0, 1, -1)
                .Item(ConstFCRate, i).Value = dt(i)("CurrRate")
                .Item(ConstVessel, i).Value = Trim(dt(i)("VesselId") & "")
                .Item(ConstSlsman, i).Value = dt(i)("SMan")
                .Item(ConstDueDate, i).Value = dt(i)("DueDate")
                .Item(ConstTrid, i).Value = dt(i)("LinkNo")
                .Item(ConstUnqNo, i).Value = dt(i)("UnqNo")
            End With
        Next
        If dt.Rows.Count > 0 Then
            calCulate()
        End If
    End Sub

    Private Function setDecimal(ByVal Cur As String) As Integer
        Try
            Dim _qry = From job In _fcTable.AsEnumerable() Where job!CurrencyCode = Cur Select New With _
                                  {.Name = job("Decimal Places")}
            For Each itm In _qry
                Return itm.Name
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

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
            ' .Item(ConstTrdate, .Rows.Count - 1).Value = "  /  /    "
            .Item(ConstDueDate, .Rows.Count - 1).Value = Format(DateValue("01/01/2010"), "dd/MM/yyyy")
            .Item(ConstFCRate, .Rows.Count - 1).Value = Format(1, "0.00")
            .Item(ConstDtype, .Rows.Count - 1).Value = IIf(isCust, "Dr", "Cr")
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
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            Const WM_KEYDOWN As Integer = &H100
            Const WM_KEYUP As Integer = &H101
            'msg.ToString()
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

    Private Sub grdVoucher_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellEndEdit
        Dim col As Integer = e.ColumnIndex
        If col = ConstAmount Or col = ConstFCAmount Then
            grdVoucher.Item(col, e.RowIndex).Value = Format(Convert.ToDecimal(grdVoucher.Item(col, e.RowIndex).Value), "#,##0" & IIf(NoOfDecimal = 0, "", "." & Strings.StrDup(NoOfDecimal, "0")))
        End If
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
        If ChgByPrg Then Exit Sub
        cmdOk.Enabled = True
        Try
            Dim col As Integer
            col = grdVoucher.CurrentCell.ColumnIndex
            Dim MyCtrl As TextBox = sender
            If chgbyprg Then Exit Sub
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
            End If

        Catch ex As Exception

        End Try
    End Sub


    Private Sub btnrem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrem.Click
        removeRow()
    End Sub
    Private Sub removeRow()
        If MsgBox("Do You Want Remove The Row", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        Dim Linkno As Long
        Dim dt As DataTable
        With grdVoucher
            If .CurrentRow Is Nothing Then Exit Sub
            dt = _objcmnbLayer._fldDatatable("Select Linkno from acctrdet where UnqNo=" & Val(.Item(ConstUnqNo, .CurrentRow.Index).Value))
            If dt.Rows.Count > 0 Then
                Linkno = Val(dt(0)("Linkno") & "")
            End If
            _objcmnbLayer._saveDatawithOutParm("DELETE FROM AccTrDet WHERE UnqNo=" & Val(.Item(ConstUnqNo, .CurrentRow.Index).Value))
            dt = _objcmnbLayer._fldDatatable("Select Linkno from AccTrDet WHERE Linkno=" & Linkno)
            If dt.Rows.Count = 0 Then
                _objcmnbLayer._saveDatawithOutParm("DELETE FROM AccTrCmn WHERE Linkno=" & Linkno)
            End If
        End With
        ldOpeningBalance()
    End Sub
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
                _ObjAcc.SMan = .Item(ConstSlsman, _row).Value
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
            .UnqNo = Val(grdVoucher.Item(ConstUnqNo, _row).Value)
            .BankCode = ""
            .LPONo = ""
            Dim dtDue As Date = IIf(chkDate(grdVoucher.Item(ConstDueDate, _row).Value), grdVoucher.Item(ConstDueDate, _row).Value, DateValue(grdVoucher.Item(ConstTrdate, _row).Value))
            .DocDate = Date.Now
            .SuppInvDate = Date.Now
            .DueDate = dtDue
            .VesselId = ""
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


    Private Sub grdVoucher_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.SelectionChanged
        Try
            If grdVoucher.Rows.Count = 0 Then Exit Sub
            BeginEdit()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub numopnBal_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles numopnBal.KeyDown
        If e.KeyCode = Keys.Return Then cmdOk.Focus()
    End Sub

    Private Sub txtlimit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtlimit.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtduedays_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtrate.KeyDown
        If e.KeyCode = Keys.Return Then
            numopnBal.Focus()
        End If
    End Sub

    Private Sub txtpT_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtpT.KeyDown, txtT.KeyDown, txtK.KeyDown, txtrate.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtpK_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtpK.KeyDown
        If e.KeyCode = Keys.Return Then
            cmdOk.Focus()
        End If
    End Sub

    Private Sub numopnBal_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles numopnBal.KeyPress, txtpT.KeyPress, txtrate.KeyPress, txtT.KeyPress, txtpK.KeyPress, txtK.KeyPress
        numCtrl = sender
        NumericTextOnKeypress(numCtrl, e, ChgByPrg, numFormat)
    End Sub

    Private Sub numopnBal_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numopnBal.TextChanged
        If Chgamt Then Exit Sub
        cmdOk.Enabled = True
    End Sub

    Private Sub txtlimit_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtlimit.TextChanged, txtrate.TextChanged
        If Chgamt Then Exit Sub
        cmdOk.Enabled = True
    End Sub

    Private Sub cmbstate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbtimes.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub cmbstate_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbtimes.SelectedIndexChanged
        If ChgByPrg Then Exit Sub
        cmdOk.Enabled = True
    End Sub

    Private Sub txtpT_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpT.TextChanged
        If Chgamt Then Exit Sub
        Chgamt = True
        If Val(txtrate.Text) > 0 Then
            txtT.Text = Format(CDbl(txtrate.Text) * Val(txtpT.Text) / 100, numFormat)
        End If
        Chgamt = False
        cmdOk.Enabled = True
    End Sub

    Private Sub txtT_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtT.TextChanged
        If Chgamt Then Exit Sub
        Chgamt = True
        If Val(txtrate.Text) > 0 And Val(txtT.Text) > 0 Then
            txtpT.Text = Format(CDbl(txtT.Text) * 100 / CDbl(txtrate.Text), numFormat)
        End If
        Chgamt = False
        cmdOk.Enabled = True
    End Sub

    Private Sub txtpK_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtpK.TextChanged
        If Chgamt Then Exit Sub
        Chgamt = True
        If Val(txtrate.Text) > 0 Then
            txtK.Text = Format(CDbl(txtrate.Text) * Val(txtpK.Text) / 100, numFormat)
        End If
        Chgamt = False
        cmdOk.Enabled = True
    End Sub

    Private Sub txtK_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtK.TextChanged
        If Chgamt Then Exit Sub
        Chgamt = True
        If Val(txtrate.Text) > 0 And Val(txtK.Text) > 0 Then
            txtpK.Text = Format(CDbl(txtK.Text) * 100 / CDbl(txtrate.Text), numFormat)
        End If
        Chgamt = False
        cmdOk.Enabled = True
    End Sub

End Class