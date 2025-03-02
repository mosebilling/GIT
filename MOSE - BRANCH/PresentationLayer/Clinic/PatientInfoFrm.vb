
Public Class PatientInfoFrm
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
    Private SrchText As String
    Private chgByPrgNum As Boolean
    Public Event Unload()
    Public Event OpenAccMaster(ByRef AccountNo As Long)
    Private _objcmnbLayer As New clsCommon_BL
    Private _ObjAcc As New clsAccountTransaction
    Dim chngprg As Boolean
    Private _objTr As New clsAccountTransaction
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fRptFormat As RptFormatfrm
    Private WithEvents fInvoice As SalesInvoice
    Private WithEvents fPrebooking As PreBookingAppointmentFrm
    Private WithEvents fvisitnote As VisitNoteFrm

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
        isModi = True
        txtRec0.Tag = dtTable(0)("AccountNo")
        txtRec0.Text = Trim("" & dtTable(0)("Alias"))
        txtRec1.Text = Trim("" & dtTable(0)("AccDescr"))
        cmbsalesman.Text = Trim("" & dtTable(0)("SlsmanId"))

        accid = dtTable(0)("accid")
        Chgamt = True
        numopnBal.Text = Format(CDbl(dtTable(0)("OpnBal")), numFormat)
        If Val(dtTable(0)("CreditLimit") & "") = 0 Then dtTable(0)("CreditLimit") = 0
        If Val(dtTable(0)("DueDays") & "") = 0 Then dtTable(0)("DueDays") = 0
        txtlimit.Text = Format(CDbl(dtTable(0)("CreditLimit")), numFormat)
        txtduedays.Text = Format(CDbl(dtTable(0)("DueDays")), numFormat)
        cmbstate.Text = Trim(dtTable(0)("CountryCode") & "")
        chkregistered.Checked = IIf(Val(dtTable(0)("isTaxRegistered") & "") = 1, True, False)
        Chgamt = False
        'dtTable = _objcmnbLayer._fldDatatable("SELECT * FROM AccMastAddr WHERE AccountNo = " & AccNo)
        If dtTable.Rows.Count > 0 Then
            txtAddr0.Text = Trim("" & dtTable(0)("Address1"))
            txtAddr1.Text = Trim("" & dtTable(0)("Address2"))
            txtAddr2.Text = Trim("" & dtTable(0)("Address3"))
            txtAddr3.Text = Trim("" & dtTable(0)("Address4"))
            txtphone.Text = Trim("" & dtTable(0)("Phone"))
            txtage.Text = Val(dtTable(0)("customerage") & "")
            txtFax.Text = Trim("" & dtTable(0)("Fax"))
            txtemail.Text = Trim("" & dtTable(0)("EMail"))
            txtwebsite.Text = Trim("" & dtTable(0)("Website"))
            txtcontactname.Text = Trim("" & dtTable(0)("ContactName"))
            txtlcno.Text = Trim("" & dtTable(0)("TrdLcno"))
            txtgstin.Text = Trim("" & dtTable(0)("GSTIN"))
            txtremarks.Text = Trim("" & dtTable(0)("Remarks"))
            txtphoneAltr.Text = Trim(dtTable(0)("PhoneAltr") & "")
            If Val(dtTable(0)("gender") & "") = 0 Then
                rdomale.Checked = True
            Else
                rdofemale.Checked = True
            End If
            If Not IsDBNull(dtTable(0)("dateofbirth")) Then
                dtpdateofbirth.Value = DateValue(dtTable(0)("dateofbirth"))
            End If
            'If Not IsDBNull(dtTable(0)("TrdDate")) Then
            '    If DateValue(dtTable(0)("TrdDate")) > DateValue("01/01/1950") Then
            '        dtpdate.CtlText = Format(DateValue(dtTable(0)("TrdDate")), DtFormat)
            '    Else
            '        dtpdate.CtlText = dtEmpty
            '    End If
            'End If
        End If
        dtTable = ds.Tables(1)
        ldVisitHistory()
        ChgByPrg = False
        txtAddr0.ReadOnly = False
        ChgAddr = False
        btnremove.Enabled = True
        txtRec1.Focus()
        TabControl2.SelectedIndex = 1
        loadStatements()
        resizeGridColumn(grdVoucher, 5)
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
        updateClick()
    End Sub
    Private Sub updateClick()
        If cmdOk.Enabled = False Then Exit Sub
        If Val(btnaddnew.Tag) = 0 And Val(btnmodify.Tag) = 0 Then
            MsgBox("This user do not have rights to update", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If Trim(txtRec0.Text) = "" Then
            If iscust Then
                MsgBox("Invalid Customer Code", MsgBoxStyle.Critical)
            Else
                MsgBox("Invalid Supplier Code", MsgBoxStyle.Critical)
            End If

            txtRec0.Focus()
            Exit Sub
        End If
        If Trim(txtRec1.Text) = "" Then
            If iscust Then
                MsgBox("Invalid Customer Name", MsgBoxStyle.Critical)
            Else
                MsgBox("Invalid Supplier Name", MsgBoxStyle.Critical)
            End If
            txtRec1.Focus()
            Exit Sub
        End If
        If Not chkDuplicate() Then Exit Sub
        If chkregistered.Checked And txtgstin.Text = "" Then
            If iscust Then
                MsgBox("Invalid GSTIN for registered Customer", MsgBoxStyle.Critical)
            Else
                MsgBox("Invalid GSTIN for registered Supplier", MsgBoxStyle.Critical)
            End If
            txtgstin.Focus()
            Exit Sub
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
            Exit Sub
        End If
        btnaddnew_Click(btnaddnew, New System.EventArgs)
        If iscust Then
            ldCustomer()
        Else
            ldSupplier()
        End If
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
        If Val(txtduedays.Text) = 0 Then txtduedays.Text = 0
        _objcmnbLayer._saveDatawithOutParm("UPDATE AccMast SET Alias = '" & MkDbSrchStr(txtRec0.Text) & "', AccDescr = '" & _
                                           MkDbSrchStr(txtRec1.Text) & "', S1AccId = '" & Val(cmbAccGroup.Items(cmbAccGroup.SelectedIndex).thedata()) & "'," & _
                                           " BranchId = '" & MkDbSrchStr(cmbBr.Text) & "', " & _
                                           " OpnBal = " & CDbl(numopnBal.Text) & ", " & _
                                           " CreditLimit = " & CDbl(txtlimit.Text) & ", " & _
                                           " DueDays = " & CDbl(txtduedays.Text) & "," & _
                                           " CountryCode = '" & MkDbSrchStr(cmbstate.Text) & "'," & _
                                           " isTaxRegistered = " & IIf(chkregistered.Checked, 1, 0) & "," & _
                                           " placesupply = " & cmbplaceofsupply.SelectedIndex & "," & _
                                           " SlsmanId='" & cmbsalesman.Text & "'" & _
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
                If Control.name = "chkcashcustomer" And enableCarWash Then
                    Control.checked = True
                    Control.enabled = False
                Else
                    Control.checked = False
                    Control.tag = ""
                End If

            End If
        Next
        Chgamt = True
        chkcashcustomer.Checked = True
        numopnBal.Text = Format(0, numFormat)
        dtpdateofbirth.Value = DateValue(Date.Now)
        Chgamt = False
        cmbstate.Text = defaultState
        cmbsalesman.Text = ""
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
            If Val(txtduedays.Text) = 0 Then txtduedays.Text = 0
            _objcmnbLayer._saveDatawithOutParm("INSERT INTO AccMast (AccountNo, Alias, AccDescr, S1AccId,OpnBal,CreditLimit,DueDays,CountryCode,isTaxRegistered,placesupply,SlsmanId) VALUES (" & _
                                               Val(txtRec0.Tag) & ", '" & MkDbSrchStr(Trim(txtRec0.Text)) & "', '" & _
                                               MkDbSrchStr(Trim(txtRec1.Text)) & "', " & cmbAccGroup.Items(cmbAccGroup.SelectedIndex).thedata() & "," & _
                                               CDbl(numopnBal.Text) & "," & CDbl(txtlimit.Text) & "," & CDbl(txtduedays.Text) & ",'" & cmbstate.Text & "'," & _
                                               IIf(chkregistered.Checked, 1, 0) & "," & cmbplaceofsupply.SelectedIndex & ",'" & cmbsalesman.Text & "')")
        End If
    End Function
    Private Function GenerateNext(ByVal Grpname As String, ByVal newVal As Integer) As String
        Dim N As Double
        Dim NewCode As String = ""
        GenerateNext = ""
        Dim i As Byte
        Dim f As Byte
        Dim tmp As String
        Dim strCode As String = ""
        If strCode = "" Then
            strCode = Strings.Left(Grpname, 4)
        End If
        Dim _vdatatableAcc As DataTable
        _vdatatableAcc = _objcmnbLayer._fldDatatable("SELECT MAX(AccountNo)AccountNo FROM AccMast WHERE S1AccId =" & newVal)
        If _vdatatableAcc.Rows.Count > 0 Then
            txtRec0.Tag = _vdatatableAcc(0)("AccountNo")
            _vdatatableAcc = _objcmnbLayer._fldDatatable("SELECT alias FROM AccMast WHERE accountno =" & Val(txtRec0.Tag & ""))
            If _vdatatableAcc.Rows.Count > 0 Then
                strCode = _vdatatableAcc(0)("alias")
            End If
        End If
        For i = 1 To 11
            If IsNumeric(Mid(strCode, Len(strCode) - i + 1, 1)) = False Then Exit For
            tmp = Val(Mid(strCode, Len(strCode) - i + 1))
            If Val(tmp) <> 0 Then
                N = Val(tmp)
            Else
                If N <> 0 Then Exit For
            End If
            If i = Len(strCode) Then i = i + 1 : Exit For
        Next i
        If Val(txtRec0.Tag & "") = 0 Then
            txtRec0.Tag = Val(newVal & "0000")
        End If
        If Val(txtRec0.Tag) >= Val(newVal & "9999") Then MsgBox("Maximum number of Ledgers reached in this Group.", MsgBoxStyle.Critical) : Exit Function
        txtRec0.Tag = Val(txtRec0.Tag) + 1
        _vdatatableAcc = _objcmnbLayer._fldDatatable("SELECT * FROM AccMast")
        i = i - 1
        f = i
        If i <= 0 Then
            i = 3
        End If
        tmp = ""
        NewCode = ""
        Try
            Do Until False
                N = N + 1
                tmp = Mid(strCode, 1, IIf(Len(strCode) <> 0, Len(strCode) - f, 0)) & Format(N, StrDup(i, "0"))
                Dim _qry = From job In _vdatatableAcc.AsEnumerable() Where job![Alias] = tmp Select New With _
                       {.Name = job!AccountNo}
                If _qry.Count = 0 Then
                    NewCode = Mid(strCode, 1, IIf(Len(strCode) <> 0, Len(strCode) - f, 0)) & Format(N, StrDup(i, "0")) 'Strings.Left(Grpname, 4) & Format(N, StrDup(4, "0"))
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

    Private Sub ldState()
        Dim dt As DataTable = _objcmnbLayer._fldDatatable("SELECT statecode FROM StateMasterTb", False)
        cmbstate.Items.Clear()
        cmbstate.Items.Add("")
        Dim i As Integer
        For i = 0 To dt.Rows.Count - 1
            cmbstate.Items.Add(dt(i)("statecode"))
        Next
    End Sub

    Private Sub saveAddress()
        Try
            If isModi And Val(txtAddr1.Tag) = 1 Then
                _objcmnbLayer._saveDatawithOutParm("DELETE FROM AccMastAddr Where AccountNo=" & Val(accid))
            End If
            Dim AccMast As DataTable
            AccMast = _objcmnbLayer._fldDatatable("SELECT AccId FROM AccMast WHERE AccountNo=" & Val(txtRec0.Tag))
            If AccMast.Rows.Count > 0 Then
                accid = AccMast(0)("AccId")
            End If
            Dim dt As Date
            dt = DateValue("01/01/1950")
            Dim gender As Integer
            If rdomale.Checked Then
                gender = 0
            Else
                gender = 1
            End If
            If Val(txtAddr1.Tag) = 1 Then
                _objcmnbLayer._saveDatawithOutParm("INSERT INTO AccMastAddr (AccountNo,Address1,Address2,Address3,Address4,Phone,Fax,ContactName,EMail,Website," & _
                                                                "TrdLcno,TrdDate,GSTIN,Remarks,PhoneAltr,gender,dateofbirth,customerage) VALUES(" & _
                                                                   Val(accid) & ",'" & _
                                                                   MkDbSrchStr(txtAddr0.Text) & "','" & _
                                                                   MkDbSrchStr(txtAddr1.Text) & "','" & _
                                                                   MkDbSrchStr(txtAddr2.Text) & "','" & _
                                                                   MkDbSrchStr(txtAddr3.Text) & "','" & _
                                                                   MkDbSrchStr(txtphone.Text) & "','" & _
                                                                   MkDbSrchStr(txtFax.Text) & "','" & _
                                                                   MkDbSrchStr(txtcontactname.Text) & "','" & _
                                                                   MkDbSrchStr(txtemail.Text) & "','" & _
                                                                   MkDbSrchStr(txtwebsite.Text) & "','" & _
                                                                   MkDbSrchStr(txtlcno.Text) & "','" & _
                                                                   Format(DateValue(dt), "yyyy/MM/dd") & "','" & _
                                                                   MkDbSrchStr(txtgstin.Text) & "','" & _
                                                                   MkDbSrchStr(txtremarks.Text) & "','" & _
                                                                   MkDbSrchStr(txtphoneAltr.Text) & "'," & _
                                                                   gender & ",'" & _
                                                                   Format(DateValue(dtpdateofbirth.Value), "yyyy/MM/dd") & "'," & Val(txtage.Text) & ")")
            End If
            If chkcashcustomer.Checked Then saveCustomer(accid) 'cash customer
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub saveCustomer(ByVal accid As Integer)
        Dim _objInv As New clsInvoice
        Dim custid As Integer
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select custid  from CashCustomerTb where customeraccount=" & accid)
        If dt.Rows.Count > 0 Then
            custid = dt(0)("custid")
        End If
        With _objInv
            .custid = custid
            .CustName = txtRec1.Text
            .Add1 = txtAddr0.Text
            .Add2 = txtAddr1.Text
            .Add3 = txtAddr2.Text
            .Phone = txtphone.Text
            .email = txtemail.Text
            .Memberid = ""
            .GiftVrNo = ""
            .Remarks = txtremarks.Text
            .customeraccount = accid
            .issupp = Not iscust
            .GSTN = txtgstin.Text
            .saveCashCustomer()
        End With
    End Sub

    Private Sub btnaddnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddnew.Click

        addnewButtonClick()
    End Sub
    Private Sub addnewButtonClick()
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
            'grdVoucher.Rows.Clear()
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
            accid = 0
            ldVisitHistory()
            btnmodify.Enabled = False
            cmdOk.Enabled = False
            btnaddnew.Text = "&Add New"
            Panel1.Enabled = False
            numopnBal.Enabled = False
            btnremove.Enabled = False
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


    Private Sub txtRec0_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRec1.TextChanged, txtRec0.TextChanged, txtAddr3.TextChanged, txtAddr2.TextChanged, txtAddr1.TextChanged, txtAddr0.TextChanged, txtlcno.TextChanged, txtcontactname.TextChanged, txtwebsite.TextChanged, txtemail.TextChanged, txtFax.TextChanged, txtphone.TextChanged, txtgstin.TextChanged, txtremarks.TextChanged, txtphoneAltr.TextChanged
        If ChgByPrg Then Exit Sub
        cmdOk.Enabled = True
        Dim myctrl As TextBox
        myctrl = sender
        If myctrl.Name <> "txtRec0" Or myctrl.Name <> "txtRec1" Then
            txtAddr1.Tag = 1
        End If

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

    Private Sub txtRec0_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtwebsite.KeyDown, txtRec1.KeyDown, txtRec0.KeyDown, txtphone.KeyDown, txtlcno.KeyDown, txtFax.KeyDown, txtemail.KeyDown, txtcontactname.KeyDown, txtAddr3.KeyDown, txtAddr2.KeyDown, txtAddr1.KeyDown, txtAddr0.KeyDown, txtgstin.KeyDown, txtphoneAltr.KeyDown, _
    txtage.KeyDown
        Dim myctl As TextBox
        myctl = sender

        If e.KeyCode = Keys.Enter Then
            If myctl.Name = "txtremarks" Then
                rdomale.Focus()
            Else
                SendKeys.Send("{TAB}")
            End If

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
    Private Sub ldSupplier()
        _vtable = _objcmnbLayer._fldDatatable("SELECT Alias,AccDescr,isnull(Bal,0)+OpnBal [Closing Bal],OpnBal [Opening Bal],Phone,ContactName,EMail,TrdLcno,case when TrdDate<='1950/01/01' then null else TrdDate end TrdDate,AccMast.AccountNo FROM AccMast " & _
                                               "left Join BalanceQr On BalanceQr.AccountNo=AccMAst.accid " & _
                                              "LEFT JOIN AccMastAddr ON AccMast.accid=AccMastAddr.AccountNo left join S1AccHd on AccMast.S1AccId=S1AccHd.S1AccId " & IIf(Condition = "", "", "WHERE " & Condition) & " ORDER BY Descr")
        grdItem.DataSource = _vtable
        SetGridSupplier()
        setComboGrid()
    End Sub
    Private Sub ldCustomer()
        _vtable = _objcmnbLayer._fldDatatable("SELECT Alias [OP Number],AccDescr [Patient Name],isnull(Bal,0)+OpnBal [Closing Bal],OpnBal [Opening Bal],Phone,ContactName,EMail,AccMast.AccountNo FROM AccMast " & _
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

            .Columns("OP Number").HeaderText = "OP Number"
            .Columns("OP Number").Width = 100
            .Columns("OP Number").Frozen = True

            .Columns("Patient Name").HeaderText = "Patient Name"
            .Columns("Patient Name").Width = 250

            .Columns("Phone").HeaderText = "Phone"
            .Columns("Phone").Width = 80

            .Columns("ContactName").HeaderText = "ContactName"
            .Columns("ContactName").Width = 150

            .Columns("EMail").HeaderText = "EMail"
            .Columns("EMail").Width = 150

            .Columns("AccountNo").Visible = False

            .Columns("Closing Bal").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Closing Bal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns("Opening Bal").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Opening Bal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


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
        Dim clsbal As Double
        clsbal = Val(grdItem.Item("Closing Bal", grdItem.CurrentCell.RowIndex).Value)
        lblclosing.Text = Format(clsbal, numFormat)
        ldAccounts(AccNo)
        btnmodify_Click(btnmodify, New System.EventArgs)
        TabControl1.SelectedIndex = 0
        TabControl2.SelectedIndex = 0
    End Sub

    Private Sub grdItem_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItem.CellContentClick

    End Sub

    Private Sub TabControl1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.Click
        If TabControl1.SelectedIndex = 1 And btnaddnew.Text = "Undo" Then
            MsgBox("Do Undo/Update before moving to Enquiry", MsgBoxStyle.Exclamation)
            TabControl1.SelectedIndex = 0
            Exit Sub
        End If
        ldCustomer()
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
            MsgBox("Code #" & txtRec0.Text & " having transactions, You can't remove record", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If MsgBox("You are going to remove the Patient # " & txtRec0.Text & " # Permenantly! Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
            Exit Sub
        End If
        _objcmnbLayer._saveDatawithOutParm("DELETE FROM AccMast WHERE AccountNo=" & Val(txtRec0.Tag))
        _objcmnbLayer._saveDatawithOutParm("DELETE FROM AccMastAddr WHERE AccountNo=" & Val(txtRec0.Tag))
        _objcmnbLayer._saveDatawithOutParm("DELETE FROM ClinicVistTb WHERE customerid=" & accid)
        ldSupplier()
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

    Sub SetGridHead()
        With grdVoucher
            SetGridProperty(grdVoucher)
            .Columns("Type").Width = 50
            .Columns("No").Width = 50
            .Columns("Date").Width = 100
            .Columns("Doctor").Width = 100
            .Columns("Comment").Width = 150
            .Columns("Observation").Width = 150
            .Columns("Treatement").Width = 250
            .Columns("visitid").Visible = False
            .Columns("visitnoteid").Visible = False
        End With
        resizeGridColumn(grdVoucher, 4)
    End Sub
    Sub SetGridHeadVisitnote(ByVal ismedicine As Boolean)
        With grdvisitnote
            SetGridProperty(grdvisitnote)
            If Not ismedicine Then
                .Columns("Date").Width = 100
                .Columns("Doctor").Width = 100
                .Columns("Comment").Width = 150
                .Columns("Observation").Width = 150
                .Columns("cvn_visitnoteid").Visible = False
                resizeGridColumn(grdvisitnote, 4)
            Else
                .Columns("vnm_medicinename").Width = 200
                .Columns("vnm_medicinename").HeaderText = "Medicine"
                .Columns("vnm_qty").Width = 50
                .Columns("vnm_qty").HeaderText = "QTY"
                .Columns("vnm_qty").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                .Columns("vnm_noofdays").Width = 50
                .Columns("vnm_noofdays").HeaderText = "Days"
                .Columns("vnm_noofdays").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("vnm_use").Width = 150
                .Columns("vnm_use").HeaderText = "Use of Medicine"
                resizeGridColumn(grdvisitnote, 3)
            End If
           
        End With
    End Sub
    Private Sub ldVisitHistory()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT visittype [Type], visitno [No],visitdate [Date],cvn_doctor Doctor," & _
                                         "cvn_notecomment Comment,cvn_visitnoteobservation Observation,cvn_treatmentnote Treatement,visitid,isnull(max_cvn_visitnoteid,0)visitnoteid from ClinicVistTb " & _
                                         "LEFT JOIN (Select max(cvn_visitnoteid) max_cvn_visitnoteid,cvn_visitid from ClinicVisitNoteTb group by cvn_visitid)lastvisitnote ON " & _
                                         "ClinicVistTb.visitid=lastvisitnote.cvn_visitid " & _
                                         "LEFT JOIN ClinicVisitNoteTb ON lastvisitnote.max_cvn_visitnoteid=ClinicVisitNoteTb.cvn_visitnoteid " & _
                                         "where customerid=" & accid & " ORDER BY visitdate Desc")
        grdVoucher.DataSource = dt
        SetGridHead()
        Dim i As Integer
        For i = 0 To grdVoucher.RowCount - 1
            With grdVoucher
                If Trim(.Item("Type", i).Value & "") = "IP" Then
                    .Rows.Item(i).DefaultCellStyle.BackColor = Color.LightGray
                End If
            End With
        Next

    End Sub
    Private Sub ldVisitNoteHistory(ByVal visitid As Long)
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT  cvn_notedate [Date],cvn_doctor Doctor," & _
                                         "cvn_notecomment Comment,cvn_visitnoteobservation Observation,cvn_treatmentnote Treatement,cvn_visitnoteid from" & _
                                         " ClinicVisitNoteTb where cvn_visitid=" & visitid & " ORDER BY cvn_notedate Desc")
        grdvisitnote.DataSource = Nothing
        grdvisitnote.DataSource = dt
        grdvisitnote.Tag = "IP"
        SetGridHeadVisitnote(False)
    End Sub
    Private Sub ldVisitNoteMedicine(ByVal visitnoteid As Long)
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT  vnm_medicinename,vnm_qty,vnm_noofdays,vnm_use from" & _
                                         " ClinicVisitMedicineTb left join invitm on invitm.itemid=ClinicVisitMedicineTb.vnm_itemid where vnm_visitnoteid=" & visitnoteid)
        grdvisitnote.DataSource = Nothing
        grdvisitnote.DataSource = dt
        grdvisitnote.Tag = ""
        SetGridHeadVisitnote(True)
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
        If accid = 0 Then
            MsgBox("Please select Patient before add visit history", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        AddVisitNote()
        '
    End Sub
    Private Sub AddVisitNote()
        If fvisitnote Is Nothing Then fvisitnote = New VisitNoteFrm
        With fvisitnote
            .lblvisitnumber.Text = "Visit Number: 0"
            .txtCashCustomer.Text = txtRec1.Text
            .txtCashCustomer.Tag = accid
            .visitid = 0
            .cmbdoctor.Text = cmbsalesman.Text
            '.MdiParent = fMainForm
            .ShowDialog()
        End With
    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            If (msg.WParam.ToInt32() = CInt(Keys.F3)) Then
                AddVisitNote()
            ElseIf (msg.WParam.ToInt32() = CInt(Keys.F8)) Then
                updateClick()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function
    Private Sub btnrem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrem.Click
        removeRow()
    End Sub
    Private Sub removeRow()
        If MsgBox("Do You Want Remove The Row", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        With grdVoucher
            If .CurrentRow Is Nothing Then Exit Sub
            _objcmnbLayer._saveDatawithOutParm("DELETE FROM ClinicVistTb WHERE visitid=" & Val(.Item("visitid", .CurrentRow.Index).Value))
        End With
        ldVisitHistory()
    End Sub
    Private Sub numopnBal_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles numopnBal.KeyDown
        If e.KeyCode = Keys.Return Then txtremarks.Focus()
    End Sub

    Private Sub txtlimit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtlimit.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtduedays_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtduedays.KeyDown
        If e.KeyCode = Keys.Return Then
            numopnBal.Focus()
        End If
    End Sub

    Private Sub numopnBal_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles numopnBal.KeyPress, txtlimit.KeyPress, txtduedays.KeyPress
        NumericTextOnKeypress(sender, e, chgByPrgNum, numFormat)
    End Sub

    Private Sub numopnBal_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numopnBal.TextChanged
        If Chgamt Then Exit Sub
        cmdOk.Enabled = True
    End Sub

    Private Sub txtlimit_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtlimit.TextChanged, txtduedays.TextChanged
        If Chgamt Then Exit Sub
        cmdOk.Enabled = True
    End Sub

    Private Sub cmbstate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbstate.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub cmbstate_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbstate.SelectedIndexChanged
        If ChgByPrg Then Exit Sub
        cmdOk.Enabled = True
        txtAddr1.Tag = 1
    End Sub
    Private Sub cmbplaceofsupply_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbplaceofsupply.SelectedIndexChanged
        txtAddr1.Tag = 1
    End Sub

    Private Sub cmbsalesman_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbsalesman.SelectedIndexChanged
        If chgByPrgNum Then Exit Sub
        cmdOk.Enabled = True
    End Sub

    Private Sub PatientInfoFrm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If bOnlyOne Then
            txtRec1.Focus()
        Else
            btnaddnew.Focus()
        End If

    End Sub

    Private Sub PatientInfoFrm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddtoCombo(cmbAccGroup, "SELECT Descr, S1AccId FROM S1AccHd " & IIf(Condition = "", "", "WHERE " & Condition) & " ORDER BY Descr", False, True)
        _fcTable = _objcmnbLayer._fldDatatable("SELECT CurrencyCode,CurrencyRate,[Decimal Places] FROM CurrencyTb")
        _slsManTable = _objcmnbLayer._fldDatatable("SELECT SManCode FROM SalesmanTb WHERE isnull(isdoctor,0)=1")
        Dim i As Integer
        cmbsalesman.Items.Add("")
        For i = 0 To _slsManTable.Rows.Count - 1
            cmbsalesman.Items.Add(_slsManTable(i)("SManCode"))
        Next
        ldState()
        cmbstate.Text = defaultState
        If enableGCC Then
            plgst.Visible = False
            plvat.Visible = True
            Label16.Visible = False
            Label19.Visible = True
            cmbplaceofsupply.Visible = True
            Label15.Text = "Emirate"
        End If
        If iscust Then
            ldCustomer()
            Label3.Text = "OP Number"
            Label4.Text = "Patient Name"
            lblname.Text = "PATIENT MASTER"
            If Not userType Then
                btnaddnew.Tag = 1
                btnmodify.Tag = 1
                btnremove.Tag = 1
            Else
                btnaddnew.Tag = IIf(getRight(236, CurrentUser), 1, 0)
                btnmodify.Tag = IIf(getRight(237, CurrentUser), 1, 0)
                btnremove.Tag = IIf(getRight(238, CurrentUser), 1, 0)
            End If
            If enableCarWash Then
                'chkcashcustomer.Visible = True
                chkcashcustomer.Checked = True
                chkcashcustomer.Enabled = False
            End If
            chkcashcustomer.Text = "Create Cash Master"
        Else
            ldSupplier()
            'Panel3.Visible = True
            'grppayment.Visible = True
            chkcashcustomer.Text = "Create Cash Supplier"
            Label3.Text = "Supp Code"
            Label4.Text = "Supp Name"
            lblname.Text = "SUPPLIER MASTER"
            If userType = 0 Or userType = 2 Then
                btnaddnew.Tag = 1
                btnmodify.Tag = 1
                btnremove.Tag = 1
            Else
                btnaddnew.Tag = IIf(getRight(6, CurrentUser), 1, 0)
                btnmodify.Tag = IIf(getRight(7, CurrentUser), 1, 0)
                btnremove.Tag = IIf(getRight(8, CurrentUser), 1, 0)
            End If
        End If

        cmdOk.Enabled = False
        cmbAccGroup.SelectedIndex = 0
        If bOnlyOne Then
            addnewButtonClick()
        End If
    End Sub

    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        ldVisitHistory()
        resizeGridColumn(grdVoucher, 5)
    End Sub

    Private Sub loadStatements()
        Dim dt As DataTable
        dt = _objTr.returnStatementReport(cldrStartDate.Value, cldrEnddate.Value, accid, 0, 0, "customer", 0).Tables(0)
        grdstatement.DataSource = Nothing
        grdstatement.DataSource = dt
        SetGridHeadStatement()
        gridTotal(dt)
    End Sub
    Sub SetGridHeadStatement()
        Dim resizecolumn As Integer
        With grdstatement
            SetGridProperty(grdstatement)
            Dim i As Integer
            For i = 0 To grdstatement.Columns.Count - 1
                Select Case UCase(.Columns(i).HeaderText)
                    Case UCase("JVTYpe")
                        .Columns(i).Width = 50
                        .Columns(i).HeaderText = "Type"
                    Case UCase("JVNum")
                        .Columns(i).Width = 100
                        .Columns(i).HeaderText = "JV No"
                    Case UCase("JVDate")
                        .Columns(i).Width = 75
                        .Columns(i).HeaderText = "Date"
                    Case UCase("Debit")
                        .Columns(i).Width = 100
                        .Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                        .Columns(i).DefaultCellStyle.Format = "N" & NoOfDecimal
                        .Columns(i).DefaultCellStyle.BackColor = Color.LightPink
                        .Columns(i).HeaderText = "Debit"
                    Case UCase("Credit")
                        .Columns(i).Width = 100
                        .Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                        .Columns(i).DefaultCellStyle.Format = "N" & NoOfDecimal
                        .Columns(i).DefaultCellStyle.BackColor = Color.LightGreen
                        .Columns(i).HeaderText = "Credit"
                    Case UCase("Reference")
                        .Columns(i).Width = 100
                        .Columns(i).HeaderText = "Reference"
                    Case UCase("Description")
                        .Columns(i).Width = 50
                        resizecolumn = i
                        .Columns(i).HeaderText = "Description"
                    Case Else
                        .Columns(i).Visible = False
                End Select
            Next
        End With
        resizeGridColumn(grdstatement, resizecolumn)

    End Sub

    Private Sub btnstatementRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnstatementRefresh.Click
        loadStatements()
    End Sub

    Private Sub TabControl2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl2.Click
        If TabControl2.SelectedIndex = 1 Then
            loadStatements()
        End If
    End Sub
    Private Sub gridTotal(ByVal RptdtTable As DataTable)
        If RptdtTable.Rows.Count = 0 Then Exit Sub
        Dim drSum As Double
        Dim crSum As Double
        Dim amt As String
        amt = Trim(RptdtTable.Compute("SUM(Debit)", "") & "")
        If Val(amt) > 0 Then
            drSum = Convert.ToDouble(amt)
        End If
        amt = Trim(RptdtTable.Compute("SUM(Credit)", "") & "")
        If Val(amt) > 0 Then
            crSum = Convert.ToDouble(amt)
        End If
        'crSum = Convert.ToDouble(RptdtTable.Compute("SUM(Amount)", "Dtype='Cr'"))
        lblTlDebit.Text = Format(drSum, numFormat)
        lblTlCredit.Text = Format(crSum, numFormat)
        lblDiff.Text = Format(CDbl(lblTlDebit.Text) - CDbl(lblTlCredit.Text), numFormat)
    End Sub

    Private Sub btnpreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpreview.Click
        Dim RptType As String = ""
        RptType = "PAL"
        If chkFormat.Checked Then
            fRptFormat = New RptFormatfrm
            fRptFormat.RptType = RptType
            fRptFormat.ShowDialog()
            fRptFormat = Nothing
        Else
            Dim RptName As String
            RptName = getRptDefFlName(RptType, "")
            If RptName <> "" Then
                PrepareReport(RptName, "", False)
            End If
        End If
    End Sub
    Public Sub PrepareReport(ByVal FileName As String, ByVal RptCaption As String, ByVal forpint As Boolean)
        If Not frm Is Nothing Then
            frm.Close()
            frm = New ReprtviewNEWfrm
        Else
            frm = New ReprtviewNEWfrm
        End If
        Dim tp As Integer
        Dim ds As New DataSet
        ds = _objTr.returnStatementReport(cldrStartDate.Value, cldrEnddate.Value, accid, 6, 0, "customer", 0)
        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = fMainForm
        frm.Text = IIf(RptCaption = "", "Reports", RptCaption)
        frm.Show()
    End Sub

    Private Sub btninvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btninvoice.Click
        Dim AccMast As DataTable
        Dim accid As Integer
        AccMast = _objcmnbLayer._fldDatatable("SELECT AccId FROM AccMast WHERE AccountNo=" & Val(txtRec0.Tag))
        If AccMast.Rows.Count > 0 Then
            accid = AccMast(0)("AccId")
        End If

        If (Not fInvoice Is Nothing) Then
            fInvoice = Nothing
        End If
        fInvoice = New SalesInvoice
        fInvoice.MdiParent = fMainForm
        fInvoice.Show()
        fInvoice.loadfromClinic(accid, "")
    End Sub

    Private Sub fInvoice_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fInvoice.FormClosed
        fInvoice = Nothing
    End Sub

    Private Sub btnRV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRV.Click
        Dim accid As Integer
        Dim AccMast As DataTable
        AccMast = _objcmnbLayer._fldDatatable("SELECT AccId FROM AccMast WHERE AccountNo=" & Val(txtRec0.Tag))
        If AccMast.Rows.Count > 0 Then
            accid = AccMast(0)("AccId")
        End If
        If cmbreceipttype.SelectedIndex = 0 Then
            fMainForm.LoadRVO(0, txtRec0.Text, accid)
        ElseIf cmbreceipttype.SelectedIndex = 1 Then
            fMainForm.LoadRV(0, txtRec1.Text)
        End If
    End Sub

    Private Sub rdomale_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdomale.Click, rdofemale.Click
        txtAddr1.Tag = 1
    End Sub

    Private Sub dtpdateofbirth_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpdateofbirth.ValueChanged
        txtAddr1.Tag = 1
        txtage.Text = DateDiff(DateInterval.Year, DateValue(dtpdateofbirth.Value), DateValue(Date.Now))
    End Sub

    Private Sub btnprebooking_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprebooking.Click
        If Not fPrebooking Is Nothing Then fPrebooking.Close() : fPrebooking = Nothing
        fPrebooking = New PreBookingAppointmentFrm
        fPrebooking.ShowDialog()
        'Dim frm As New PreBookingAppointmentFrm
        'frm.ShowDialog()
    End Sub

    Private Sub fPrebooking_doPrebooking(ByVal strday As Integer, ByVal count As Integer, ByVal doctorname As String, ByVal bookingtime As DateTime) Handles fPrebooking.doPrebooking
        Dim datefld As Date = DateValue(Date.Now)
        If strday = 0 Then
            strday = 6
        Else
            strday = strday - datefld.DayOfWeek
            If strday = 0 Then strday = 7
        End If
        Dim AccMast As DataTable
        AccMast = _objcmnbLayer._fldDatatable("SELECT AccId FROM AccMast WHERE AccountNo=" & Val(txtRec0.Tag))
        If AccMast.Rows.Count > 0 Then
            accid = AccMast(0)("AccId")
        End If
        datefld = DateAdd(DateInterval.Day, strday, datefld)
        Dim i As Integer
        Dim Str As String
        Dim VchrNo As Integer
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("select max(bookingno) maxno from ClinicVisitBookingTb")
        If dt.Rows.Count > 0 Then
            Dim maxno As Integer = Val(dt(0)("maxno") & "")
            VchrNo = maxno + 1
        Else
            VchrNo = 1
        End If
        For i = 1 To count
            Str = "Insert into ClinicVisitBookingTb(bookingno,bookingdate,bookingFor,customerid,doctor,comment,bookingtype,bokkingtime,phone," & _
                    "add1,add2,add3,patientName,referenceBy) values(" & _
                    VchrNo & ",'" & Format(DateValue(Date.Now), "yyyy/MM/dd") & "','" & Format(DateValue(datefld), "yyyy/MM/dd") & "'," & _
                    accid & ",'" & doctorname & "','" & txtremarks.Text & "','OP','" & _
                    bookingtime & "','" & txtphone.Text & "','" & _
                    txtAddr0.Text & "','" & txtAddr1.Text & "','" & txtAddr2.Text & "','" & txtRec1.Text & "','')"
            _objcmnbLayer._saveDatawithOutParm(Str)
            VchrNo = VchrNo + 1
            datefld = DateAdd(DateInterval.Day, 7, datefld)
        Next
        MsgBox("Updated", MsgBoxStyle.Information)
    End Sub

    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        PrepareReport(RptFlName, RptCaption, False)
    End Sub

    Private Sub btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnload.Click
        ldCustomer()
    End Sub

    Private Sub fvisitnote_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fvisitnote.FormClosed
        fvisitnote = Nothing
        ldVisitHistory()
    End Sub

    Private Sub grdVoucher_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.DoubleClick
        If Trim(grdVoucher.Item("Type", grdVoucher.CurrentRow.Index).Value & "") <> "IP" Then
            If fvisitnote Is Nothing Then fvisitnote = New VisitNoteFrm
            With fvisitnote
                .visitnoteid = Val(grdVoucher.Item("visitnoteid", grdVoucher.CurrentRow.Index).Value)
                .ShowDialog()
            End With
        End If
        
    End Sub

    Private Sub grdVoucher_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.RowEnter
        If Trim(grdVoucher.Item("Type", e.RowIndex).Value & "") = "IP" Then
            'IN PATIENT VISIT
            ldVisitNoteHistory(grdVoucher.Item("visitid", e.RowIndex).Value)
        Else
            'OP OR OE - OUT PATIENT OR OPENDING ENTRY
            ldVisitNoteMedicine(grdVoucher.Item("visitnoteid", e.RowIndex).Value)
        End If

    End Sub

    Private Sub grdvisitnote_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdvisitnote.DoubleClick
        If grdvisitnote.Tag = "IP" Then
            If fvisitnote Is Nothing Then fvisitnote = New VisitNoteFrm
            Dim rindex As Integer
            With fvisitnote
                '.lblvisitnumber.Text = "Visit Number: 0"
                '.txtCashCustomer.Text = txtRec1.Text
                '.txtCashCustomer.Tag = accid
                rindex = grdVoucher.CurrentRow.Index
                .visitnoteid = Val(grdvisitnote.Item("cvn_visitnoteid", grdvisitnote.CurrentRow.Index).Value)
                '.MdiParent = fMainForm
                .ShowDialog()
                grdVoucher.CurrentCell = grdVoucher.Item(0, rindex)
            End With
        End If
        
    End Sub

    Private Sub grdVoucher_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellContentClick

    End Sub
End Class