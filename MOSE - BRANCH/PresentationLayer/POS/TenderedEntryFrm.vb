
Public Class TenderedEntryFrm
    Public Event updatePos(ByVal cashAcc As Long, ByVal cashAmt As Double, ByVal cardAcc As Long, ByVal cardAmt As Double, _
                           ByVal cardnumber As String, ByVal creditAcc As Long, ByVal creditAmt As Double, _
                           ByVal customername As String, ByVal tendAmt As Double, ByVal chgBalanceAmt As Double, _
                           ByVal customerphone As String, ByVal srno As String, ByVal sramt As Double, ByVal vname As String)
    Public Event closeWait()
    Private _objInv As New clsInvoice
    Private WithEvents fCrtAcc As CreateAccNew
    Private WithEvents fSelect As Selectfrm
    Private WithEvents fMList As Mlistfrm
    Private _objcmnbLayer As New clsCommon_BL
    Private _srchTxtId As Byte
    Private chgbyprg As Boolean
    Private chgbyprgN As Boolean
    Private _srchOnce As Boolean
    Private MyActiveControl As New Object
    Private lstKey As Keys


    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        If btnupdate.Text = "NEW" Then
            Me.Close()
        Else
            updateInv()
        End If
    End Sub
    Private Sub updateInv()
        Dim cashAmt As Double
        Dim cardAmt As Double
        Dim changeAmt As Double
        Dim accountname As String = ""
        If getRight(89, CurrentUser) And Val(lbllimit.Text) > 0 Then
            If CDbl(lblbalance.Text) + CDbl(lblNetAmt.Text) > CDbl(lbllimit.Text) Then
                MsgBox("Credit Limit Exeeds! You cannot post the invoice", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
        End If
        If Val(txtcardamount.Text) = 0 Then
            cardAmt = 0
        Else
            cardAmt = CDbl(txtcardamount.Text)
            accountname = txtPCard.Text
        End If
        If Val(lblchange.Text) = 0 Then
            changeAmt = 0
        Else
            changeAmt = CDbl(lblchange.Text)
        End If
        If changeAmt > 0 Then
            cashAmt = CDbl(lblNetAmt.Text) - CDbl(txtsramt.Text) - cardAmt
        Else
            cashAmt = CDbl(txttendered.Text)
        End If
        If cashAmt > 0 Then
            accountname = accountname & IIf(accountname = "", "", ",") & txtPcash.Text
        End If
        changeAmt = changeAmt + CDbl(txtsramt.Text)
        If cardAmt <= 0 And cashAmt <= 0 And changeAmt > 0 Then
            MsgBox("Without Payment POS Invoice cannot be saved", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If changeAmt < 0 And Val(txtcustomer.Tag) = 0 Then
            MsgBox("Amount is not balanced or empty entry." & vbCrLf & "Please set balance amount as credit to customer", MsgBoxStyle.Exclamation)
            If chkcredit.Checked Then
                txtcustomer.Focus()
            Else
                txttendered.Focus()
            End If

            Exit Sub
        End If
        If changeAmt < 0 Then
            accountname = accountname & IIf(accountname = "", "", ",") & txtcustomer.Text
        End If
        If Val(txtsramt.Text) = 0 Then txtsramt.Text = 0
        RaiseEvent updatePos(Val(txtPcash.Tag), cashAmt, Val(txtPCard.Tag), cardAmt, txtcardnumber.Text, _
                             Val(txtcustomer.Tag), changeAmt * -1, accountname, CDbl(txttendered.Text), _
                             CDbl(lblchange.Text), Trim(txtcustAddress.Tag & ""), Trim(txtsrno.Text), CDbl(txtsramt.Text), lblvouchername.Text)
       
    End Sub
    Private Sub fCrtAcc_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fCrtAcc.FormClosed
        fCrtAcc = Nothing
    End Sub

    Private Sub fCrtAcc_OpenAccMaster(ByRef AccountNo As Long) Handles fCrtAcc.OpenAccMaster
        txtcustomer.Tag = AccountNo
        loadCustomerDet()
    End Sub
    Private Sub loadCustomerDet()
        Dim dt As DataTable
        _objcmnbLayer = New clsCommon_BL
        dt = _objcmnbLayer._fldDatatable("SELECT accid,Alias,AccDescr,Address1,Address2,Address3,Address4,Phone,Fax,EMail,PT1," & _
                                         "PT2,PT3,PT4,TrdLcno,TrdDate,ContactName,CountryCode,isnull(CreditLimit,0)CreditLimit from AccMast " & _
                                         "LEFT JOIN AccMastAddr ON AccMast.ACCID=AccMastAddr.accountno where ACCID='" & Val(txtcustomer.Tag) & "'")
        chgbyprg = True
        If dt.Rows.Count > 0 Then
            txtcustomer.Text = dt(0)("AccDescr")
            txtcustomer.Tag = dt(0)("accid")
            Dim iNBal As Double = getAccBal(Val(txtcustomer.Tag))
            lblbalance.Text = Format(iNBal, numFormat)
            lblcbalance.Text = Format(iNBal + (CDbl(lblchange.Text) * -1), numFormat)
            If Trim(dt(0)("Address1") & "") <> "" Then
                txtcustAddress.Text = dt(0)("Address1") & vbCrLf
            End If
            If Trim(dt(0)("Address2") & "") <> "" Then
                txtcustAddress.Text = txtcustAddress.Text & dt(0)("Address2") & vbCrLf
            End If
            If Trim(dt(0)("Address3") & "") <> "" Then
                txtcustAddress.Text = txtcustAddress.Text & dt(0)("Address3") & vbCrLf
            End If
            If Trim(dt(0)("Phone") & "") <> "" Then
                txtcustAddress.Text = txtcustAddress.Text & "Phone : " & dt(0)("Phone")
                txtcustAddress.Tag = dt(0)("Phone")
            End If
            lbllimit.Text = Format(Val(dt(0)("CreditLimit")), numFormat)
        Else
            txtcustomer.Text = ""
        End If
        chgbyprg = False
    End Sub

    Private Sub chkcredit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkcredit.CheckedChanged
        If Val(lblchange.Text) >= 0 Then
            MsgBox("You cannot set zero or less than zero balance as credit", MsgBoxStyle.Exclamation)
            txttendered.Focus()
            Exit Sub
        End If
        grpCredit.Enabled = chkcredit.Checked
        txtcustomer.Focus()
    End Sub
    Private Sub QuickCust(Optional ByVal Grp As String = "Customer")
        fCrtAcc = New CreateAccNew
        With fCrtAcc
            .Condition = "GrpSetOn In ('" & Grp & "')"
            If Grp = "Customer" Then
                .iscust = True
            Else
                .iscust = False
            End If
            .bOnlyOne = True

            .ShowDialog()
        End With
    End Sub

    Private Sub btnnewcustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnewcustomer.Click
        QuickCust()
    End Sub

    Private Sub txtPCard_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPCard.KeyDown, txtcustomer.KeyDown
        Dim MyCtrl As TextBox = sender
        lstKey = e.KeyCode
        If e.KeyCode = Keys.F2 Then
            If Not fMList Is Nothing Then fMList.Visible = False
            Select Case MyCtrl.Name
                Case "txtcustomer"
                    _srchTxtId = 1
                    ldSelect(1)
                Case "txtPCard"
                    _srchTxtId = 2
                    ldSelect(15)
            End Select
        ElseIf e.KeyCode = Keys.Return Then
            If Not fMList Is Nothing Then fMList.Visible = False
            Select Case MyCtrl.Name
                Case "txtcustomer"
                    btnupdate.Focus()
                Case "txtPCard"
                    txtcardamount.Focus()
            End Select
        ElseIf e.KeyCode = Keys.Up Or e.KeyCode = Keys.PageUp Then
            If Not fMList Is Nothing Then
                If fMList.Visible Then
                    fMList.MoveUp(MyCtrl.Text)
                    Exit Sub
                End If
            End If

        ElseIf e.KeyCode = Keys.Down Or e.KeyCode = Keys.PageDown Then
            If Not fMList Is Nothing Then
                If fMList.Visible Then
                    fMList.MoveDown(MyCtrl.Text)
                    Exit Sub
                End If
            End If
        End If
    End Sub
    Private Sub ldSelect(ByVal BVal As Single)
        fSelect = New Selectfrm
        'nSelect = BVal
        SetForm(fSelect, BVal)
        If BVal = 1 Or BVal = 0 Then
            fSelect.Width = 742
            fSelect.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
        Else
            fSelect.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedToolWindow
            fSelect.Width = 425
        End If
        fSelect.Show()
    End Sub
    Private Sub fSelect_doSelect(ByVal KeyId As String, ByVal strFld1 As String, ByVal strFld2 As String) Handles fSelect.doSelect
        If _srchTxtId = 1 Then
            txtcustomer.Text = strFld1
            txtcustomer.Tag = KeyId
            loadCustomerDet()
            btnupdate.Focus()
        ElseIf _srchTxtId = 2 Then
            txtPCard.Text = strFld1
            txtPCard.Tag = KeyId
            txtcardamount.Focus()
        End If
    End Sub
    Private Sub fSelect_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fSelect.FormClosed
        fSelect = Nothing
    End Sub
    Private Sub calculate()
        chgbyprg = True
        Dim cardamt As Double
        Dim tendered As Double
        If Val(txtcardamount.Text) = 0 Then
            cardamt = 0
        Else
            cardamt = CDbl(txtcardamount.Text)
        End If
        If Val(txttendered.Text) = 0 Then
            tendered = 0
        Else
            tendered = CDbl(txttendered.Text)
        End If
        Dim ttl As Double
        ttl = cardamt + tendered
        Dim netamt As Double
        If Val(txtsramt.Text) = 0 Then txtsramt.Text = Format(0, numFormat)
        netamt = CDbl(lblNetAmt.Text) - CDbl(txtsramt.Text)
        If netamt > tendered Then
            lblcash.Text = Format(tendered, numFormat)
        Else
            lblcash.Text = Format(netamt - cardamt, numFormat)
        End If
        If Val(lblbalance.Text) = 0 Then lblbalance.Text = 0
        lblchange.Text = Format(ttl - netamt, numFormat)
        lblcbalance.Text = Format(CDbl(lblbalance.Text) + (CDbl(lblchange.Text) * -1), numFormat)
        chgbyprg = False
    End Sub

    Private Sub txttendered_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txttendered.KeyPress
        NumericTextOnKeypress(sender, e, chgbyprgN, numFormat)
    End Sub
   
    Private Sub txttendered_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txttendered.TextChanged
        If chgbyprg Then Exit Sub
        calculate()
    End Sub

    Private Sub txtcardamount_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtcardamount.KeyDown
        If e.KeyCode = Keys.Return Then
            txtcardnumber.Focus()
        End If
    End Sub

    Private Sub txtcardamount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcardamount.KeyPress
        NumericTextOnKeypress(sender, e, chgbyprgN, numFormat)
    End Sub

    Private Sub txtcardamount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcardamount.TextChanged
        If chgbyprg Then Exit Sub
        Dim cardamt As Double
        Dim invAmt As Double
        If Val(txtcardamount.Text) = 0 Then
            cardamt = 0
        Else
            cardamt = CDbl(txtcardamount.Text)
        End If
        If Val(lblNetAmt.Text) = 0 Then
            invAmt = 0
        Else
            invAmt = CDbl(lblNetAmt.Text)
        End If
        txttendered.Text = Format(invAmt - cardamt, numFormat)
        calculate()
    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            Const WM_KEYDOWN As Integer = &H100
            Const WM_KEYUP As Integer = &H101
            'msg.ToString()
            If msg.Msg = WM_KEYDOWN Or msg.Msg = WM_KEYUP Then
                If msg.WParam.ToInt32() = CInt(Keys.F5) Or msg.WParam.ToInt32() = CInt(Keys.F4) Then
                    If msg.WParam.ToInt32() = CInt(Keys.F4) Then
                        'UPI
                        loadUPICard(True)
                    Else
                        'CARD
                        loadUPICard(False)
                    End If
                    txtcardnumber.Focus()
                    txtcardamount.Text = Format(CDbl(lblNetAmt.Text), numFormat)
                    txttendered.Text = Format(0, numFormat)
                ElseIf msg.WParam.ToInt32() = CInt(Keys.F6) Then
                    txttendered.Focus()

                ElseIf msg.WParam.ToInt32() = CInt(Keys.F7) Then
                    If Val(lblchange.Text) >= 0 Then
                        MsgBox("You cannot set zero or less than zero balance as credit", MsgBoxStyle.Exclamation)
                        txttendered.Focus()
                        Exit Function
                    End If
                    chkcredit.Checked = True
                    txtcustomer.Focus()
                ElseIf msg.WParam.ToInt32() = CInt(Keys.F8) Then
                    updateInv()
                ElseIf msg.WParam.ToInt32() = CInt(Keys.Escape) Then
                    Me.Close()
                End If
            End If
ctn:
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub btnsetcash_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsetcash.Click
        txttendered.Focus()
        txttendered.Text = Format(CDbl(lblNetAmt.Text) - CDbl(txtsramt.Text), numFormat)
        chgbyprg = True
        txtcardamount.Text = Format(0, numFormat)
        chgbyprg = False
        calculate()
    End Sub

    Private Sub btnsetcard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsetcard.Click
        loadUPICard(False)
        txtcardamount.Focus()
        txtcardamount.Text = Format(CDbl(lblNetAmt.Text) - CDbl(txtsramt.Text), numFormat)
        'chgbyprg = True
        txttendered.Text = Format(0, numFormat)
        getvouchertype(2)
        'chgbyprg = False
    End Sub


    Private Sub txtcardnumber_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtcardnumber.KeyDown
        If e.KeyCode = Keys.Return Then
            btnupdate.Focus()
        End If
    End Sub
    Private Sub loadDebits()
        Dim dtTable As DataTable
        Dim ds As DataSet
        Dim qry As String
        qry = "SELECT accid,AccDescr FROM AccMast WHERE AccSetId Like '%22%'"
        qry = qry & " SELECT accid,AccDescr FROM AccMast WHERE AccSetId Like '%11%'"
        ds = _objcmnbLayer._ldDataset(qry, False)
        dtTable = ds.Tables(0)
        If dtTable.Rows.Count > 0 Then
            txtPCard.Tag = dtTable(0)("accid")
            txtPCard.Text = dtTable(0)("AccDescr")
        End If
        dtTable = Nothing
        dtTable = ds.Tables(1)
        If dtTable.Rows.Count > 0 Then
            txtPcash.Tag = dtTable(0)("accid")
            txtPcash.Text = dtTable(0)("AccDescr")
        End If
    End Sub

    Private Sub TenderedEntryFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loadDebits()
        Timer1.Enabled = True
        RaiseEvent closeWait()
        chkdupprint.Checked = duplicatebillinPOS
    End Sub
  
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        If Val(Timer1.Tag) = 0 Then
            txttendered.Focus()
        ElseIf Val(Timer1.Tag) = 1 Then
            txtcardnumber.Focus()
            txtcardamount.Text = CDbl(lblNetAmt.Text)
            txttendered.Text = Format(0, numFormat)
        ElseIf Val(Timer1.Tag) = 2 Then
            txttendered.Text = Format(0, numFormat)
            txtcustomer.Focus()
            chkcredit.Checked = True
        End If
    End Sub

    Private Sub chkdupprint_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkdupprint.CheckedChanged
        _objcmnbLayer._saveDatawithOutParm("update CompanyTb set duplicatebillinPOS=" & IIf(chkdupprint.Checked, 1, 0))
        duplicatebillinPOS = chkdupprint.Checked
    End Sub

    Private Sub txtprefix_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Return Then
            txtsrno.Focus()
        End If
    End Sub

    Private Sub txtsrno_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtsrno.KeyDown
        If e.KeyCode = Keys.Return Then
            txttendered.Focus()
        End If
    End Sub
    Private Sub txtsrno_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtsrno.Validated
        If txtsrno.Text <> "" Then
            Dim dt As DataTable
            dt = _objcmnbLayer._fldDatatable("Select trid from ItmInvCmnTb where deductedSRNO='" & txtsrno.Text & "'")
            If dt.Rows.Count > 0 Then
                MsgBox("Sales Returned Adjusted", MsgBoxStyle.Exclamation)
                txtsrno.Text = ""
                Exit Sub
            End If

            dt = _objcmnbLayer._fldDatatable("Select netamt from ItmInvCmnTb " & _
                                             "where trtype='SR' and prefix+case when isnull(prefix,'')='' then '' else '/' end +convert(varchar,invno)='" & txtsrno.Text & "'")
            If dt.Rows.Count > 0 Then
                txtsramt.Text = Format(CDbl(dt(0)("netamt")), numFormat)
            Else
                txtsramt.Text = Format(0, numFormat)
            End If
        Else
            txtsramt.Text = Format(0, numFormat)
        End If
        txttendered.Text = Format(CDbl(lblNetAmt.Text) - CDbl(txtsramt.Text), numFormat)
    End Sub

    Private Sub txtsrno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtsrno.TextChanged

    End Sub

    Private Sub btnupi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupi.Click
        loadUPICard(True)
        txtcardamount.Focus()
        txtcardamount.Text = Format(CDbl(lblNetAmt.Text) - CDbl(txtsramt.Text), numFormat)
        'chgbyprg = True
        txttendered.Text = Format(0, numFormat)
        getvouchertype(6)
        'chgbyprg = False
    End Sub
    Private Sub loadUPICard(ByVal isupi As Boolean)
        Dim dtTable As DataTable
        If isupi Then
            dtTable = _objcmnbLayer._fldDatatable("SELECT accid,AccDescr FROM AccMast WHERE AccSetId Like '%29%'")
        Else
            dtTable = _objcmnbLayer._fldDatatable("SELECT accid,AccDescr FROM AccMast WHERE AccSetId Like '%22%'")
        End If

        If dtTable.Rows.Count > 0 Then
            txtPCard.Tag = dtTable(0)("accid")
            txtPCard.Text = dtTable(0)("AccDescr")
        Else
            If isupi Then
                MsgBox("Set UPI Payment account in Settings", MsgBoxStyle.Exclamation)
            Else
                MsgBox("Set CARD Payment account in Settings", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub
    Private Sub getvouchertype(ByVal tp As Integer)
        Dim dt As DataTable
        If tp = 3 Then
            'CREDIT
            dt = _objcmnbLayer._fldDatatable("SELECT  [VOUCHER NAME] FROM PreFixTb WHERE CTGRY=3 and vrtypeno=4", False)
            If dt.Rows.Count > 0 Then
                lblvouchername.Text = dt(0)(0)
            End If
        ElseIf tp = 1 Then
            'CASH
            dt = _objcmnbLayer._fldDatatable("SELECT  [VOUCHER NAME] FROM PreFixTb WHERE CTGRY=1 and vrtypeno=4", False)
            If dt.Rows.Count > 0 Then
                lblvouchername.Text = dt(0)(0)
            End If
        ElseIf tp = 2 Then
            'CARD
            dt = _objcmnbLayer._fldDatatable("SELECT  [VOUCHER NAME] FROM PreFixTb WHERE CTGRY=2 and vrtypeno=4", False)
            If dt.Rows.Count > 0 Then
                lblvouchername.Text = dt(0)(0)
            End If
        ElseIf tp = 6 Then
            'UPI
            dt = _objcmnbLayer._fldDatatable("SELECT  [VOUCHER NAME] FROM PreFixTb WHERE CTGRY=6 and vrtypeno=4", False)
            If dt.Rows.Count > 0 Then
                lblvouchername.Text = dt(0)(0)
            End If
        End If

    End Sub

    Private Sub chkcredit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkcredit.Click
        If chkcredit.Checked Then
            getvouchertype(3)
        Else
            getvouchertype(1)
        End If
    End Sub

    Private Sub txtcustomer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcustomer.TextChanged
        If chgbyprg = True Then Exit Sub
        Dim myCtrl As Control = sender
        _srchTxtId = 2
        _srchOnce = False
        ShowFmlist(sender)
    End Sub
    Private Sub ShowFmlist(ByVal myCtrl As Control, Optional ByVal isFromTexbox As Boolean = False)
        If chgbyprg Then Exit Sub
        MyActiveControl = myCtrl
        Dim alreadyOpened As Boolean
        If Not _srchOnce Then
            chgbyprg = True
            If fMList Is Nothing Then
                fMList = New Mlistfrm
            Else
                alreadyOpened = True
            End If
            Dim PopupLoc As Point
            Dim x As Integer
            Dim y As Integer
            x = Me.Width - (fMList.Width / 2)
            y = Me.Height - 50
            PopupLoc = New Point(x, y)
            If fMList.Visible = False Then
                Select Case _srchTxtId
                    Case 1, 2
                        SetFmlist(fMList, 3)
                End Select
                fMList.loc = PopupLoc
                fMList.Show(fMainForm)
                fMList.Visible = True
            End If
        End If
        Select Case _srchTxtId
            Case 2   'Customer name
                fMList.SearchIndex = 0
                fMList.SearchIndexDescr = 4
                fMList_doFocus()
                fMList.Search(txtcustomer.Text)
                txtcustomer.Tag = fMList.AssignList(txtcustomer, lstKey, chgbyprg)
        End Select
        _srchOnce = True
        chgbyprg = False
    End Sub
    Private Sub fMList_doClose() Handles fMList.doClose
        fMList = Nothing
    End Sub

    Private Sub fMList_doFocus() Handles fMList.doFocus
        If TypeOf (MyActiveControl) Is TextBox Then
            MyActiveControl.focus()
        End If
    End Sub

    Private Sub fMList_doSelect(ByVal ItmFlds() As String) Handles fMList.doSelect
        chgbyprg = True
        Select Case _srchTxtId
            Case 1, 2
                txtcustomer.Text = ItmFlds(0)
        End Select
        chgbyprg = False
    End Sub

    Private Sub txtcustomer_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcustomer.Validated
        loadCustomerDet()
    End Sub
End Class