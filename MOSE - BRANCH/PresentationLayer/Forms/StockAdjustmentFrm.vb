﻿
Public Class StockAdjustmentFrm

#Region "Public Variables"
    Public isModi As Boolean
    Public isCust As Boolean
#End Region
#Region "Local Variables"
    Private chgbyprg As Boolean
    Private Dt As Date
    Private istext As Boolean
    Private ChgId As Boolean
    Private textIndex As Integer
    Private _srchOnce As Boolean
    Private _srchTxtId As Byte
    Private _srchIndexId As Byte
    Private strGridSrchString As String
    Private LddTmpSave As Long
    Private chgItm As Boolean
    Private chgAmt As Boolean
    Private activecontrolname As String
    Private SrchText As String
    Private CTVol As Double
    Private CTWt As Double
    Private CTQty As Double
    Private loadedTrId As Long
    Private StrAccMastSrch As String
    Private Locked As Boolean
    Private varProtectedByRights As Boolean
    Private lnumformat As String
    Private dtTax As DataTable
    Private TrTypeNo As Integer
    Private FrTrId As Long


    'numeric text
    Private idx As Integer
    Private str1 As String
    Private str2 As String
    Private str3 As String
    Private SelStart As Integer
    Private numCtrl As TextBox
    Private isImport As Boolean

    Private nSelect As Integer
    Private LddImpDocs As String
    Private OthCost As Double
    Private FCRt As Double
    Private chgCurr As Boolean
    Private NDec As Byte
    Private chgDoc As Boolean
    Private indexHead As Short

    Private MyActiveControl As New Object
    Private lstKey As Keys
    Private chgPost As Boolean
    Private onceChgFld As Boolean
    Private isprotected As Boolean
    Private chgUprice As Boolean
#End Region
#Region "Class Objet declarations"
    Private _objcmnbLayer As New clsCommon_BL
    Private _objTr As New clsAccountTransaction
    Private _objInv As New clsInvoice
#End Region
#Region "Form Object Declaration"
    Private WithEvents fMList As Mlistfrm
    Private WithEvents fSelect As Selectfrm
    Private WithEvents fProductEnquiry As ItmEnqry
    Private WithEvents fSlctDoc As SelectInvTr
    Private WithEvents fRptFormat As RptFormatfrm
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fCrtAcc As CreateAccNew
    Private WithEvents fSerialno As AddSerialnoFrm
    Private WithEvents fproductMast As ItemMastFrm
    Private WithEvents fshowlocationqty As ShowLocationQtyFrm
    Private WithEvents fwait As WaitMessageFrm
#End Region
#Region "Const Variables"
    'item details
    Private Const ItmSlNo = 0
    Private Const ItmCode = 1
    Private Const ItmQIH = 2
    Private Const ItmUnitPrice = 3
    Private Const ItmCost = 4
    Private Const ItmLPC = 5

    Private Const CostSlNo = 0
    Private Const CostDbName = 1
    Private Const CostCrName = 2
    Private Const CostReference = 3
    Private Const CostAmount = 4
    Private Const CostDescr = 5
    Private Const CostFCAmount = 6
    Private Const CostFCName = 7
    Private Const CostFCRate = 8
    Private Const CostDrAcc = 9
    Private Const CostCrAcc = 10
#End Region
#Region "Structure Variables"
    Private Structure LPOInf
        Dim LPO As Long
        Dim LPODate As Date
        Dim Amount As Double
    End Structure
    Private Structure JobAccTp
        Dim Amt As Double
        Dim Job As String
        Dim Acc As Long
    End Structure
    Private LPos() As LPOInf
    Private JobAcc() As JobAccTp
#End Region

    Private Sub numVchrNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles numVchrNo.KeyDown
        If e.KeyCode = Keys.Enter Then
            If isModi Then
                CheckNLoad()
            Else
                SendKeys.Send("{TAB}")
            End If
        End If
    End Sub
    Public Sub CheckNLoad(Optional ByVal FromTrId As Long = 0)
        Dim InvList As DataTable
        If FromTrId <> 0 Then
            InvList = _objcmnbLayer._fldDatatable("SELECT TrId FROM ItmInvCmnTb WHERE TrId = " & FromTrId)
        Else
            InvList = _objcmnbLayer._fldDatatable("SELECT TrId FROM ItmInvCmnTb WHERE " & IIf(UsrBr = "", "", " Brid='" & UsrBr & "' AND") & " TrType = '" & cmbVoucherTp.Text & "' AND InvNo = " & Val(numVchrNo.Text) & " AND PreFix = '" & MkDbSrchStr(Trim(txtprefix.Text), True) & "'")
        End If
        If InvList.Rows.Count > 0 Then
            If Not isImport Then
                loadedTrId = InvList(0)("TrId")
                InvList = Nothing
                loadWaite(2)
                isModi = True
            Else
                'PasteFrom(InvList(0)("TrId"))
                FrTrId = InvList(0)("TrId")
                loadWaite(3)
                isImport = False
            End If
        Else
            MsgBox("Voucher with # [" & numVchrNo.Text & "] not exits !!", vbInformation)
            numVchrNo.Focus()
        End If
        'If InvList.State Then InvList.Close()
    End Sub

    Private Sub ldPostedInv()
        Dim i As Integer
        If ChgId And loadedTrId <> 0 Then
            If MsgBox("Changes found on loaded Voucher.  Continue with loading ?", vbQuestion + vbOKCancel) = vbCancel Then
                Exit Sub
                numVchrNo.Focus()
            End If
        End If
        isImport = False
        Dim ItmInvCmnTb As DataTable
        Dim DocAssgnTb As DataTable
        Dim sRs As DataTable
        'Dim Discount As Double
        Dim UPerPack As Double
        Dim Protect As Boolean
        Dim CrossBr As Boolean
        ItmInvCmnTb = _objcmnbLayer._fldDatatable("SELECT ItmInvCmnTb.*,jobname FROM ItmInvCmnTb LEFT JOIN JobTb ON ItmInvCmnTb.[Job Code]=JobTb.jobcode WHERE TrId = " & loadedTrId & " AND TrType = '" & cmbVoucherTp.Text & "'")
        chgbyprg = True
        If ItmInvCmnTb.Rows.Count = 0 Then GoTo Quit
        getProtectUntil()
        'If IsDBNull(ItmInvCmnTb(0)("ImpDoc")) Then
        '    cmbDos.SelectedIndex = -1
        'Else
        '    cmbDos.SelectedIndex = ItmInvCmnTb(0)("ImpDoc") - 1
        'End If
        'If cmbVrType.Items.Count > 0 Then
        '    'cmbVrType.SelectedIndex = 0
        '    cmbVrType.Text = getvrsName(ItmInvCmnTb(0)("TypeNo"))
        '    'For i = 0 To cmbVrType.Items.Count - 1
        '    '    If cmbVrType.Items(i).thedata = ItmInvCmnTb(0)("TypeNo") Then
        '    '        cmbVrType.SelectedIndex = i
        '    '        Exit For
        '    '    End If
        '    'Next i
        'End If

        'DocAssgnTb = _objcmnbLayer._fldDatatable("SELECT DNo, Q1.DocId FROM (SELECT DocId FROM DocAssgnTb WHERE IsPrchOrSls  = 1 AND TrId = " & loadedTrId & "  GROUP BY DocId) As Q1 LEFT JOIN DocCmnTb ON DocCmnTb.DocId = Q1.DocId ORDER BY DNo")
        cldrdate.Value = Format(ItmInvCmnTb(0)("TrDate"), DtFormat) ' "MM/dd/yyyy")
        txtprefix.Text = ItmInvCmnTb(0)("PreFix")
        txtPPrefix.Text = ItmInvCmnTb(0)("PreFix")
        txtprefix.Tag = ItmInvCmnTb(0)("PreFix")
        numVchrNo.Text = ItmInvCmnTb(0)("InvNo") 'Val(Mid(!InvNo, 3)) '!InvNo '
        numVchrNo.Tag = ItmInvCmnTb(0)("InvNo")
        numPrintVchr.Text = ItmInvCmnTb(0)("InvNo")  'CMNREC(12).FormattedText
        txtJob.Text = Trim("" & ItmInvCmnTb(0)("Job Code"))
        txtjobname.Text = Trim(ItmInvCmnTb(0)("jobname") & "")
        cmbfc.Text = Trim(ItmInvCmnTb(0)("FC") & "")
        txtfcrt.Text = Format(ItmInvCmnTb(0)("FCRate"), lnumformat)
        FCRt = ItmInvCmnTb(0)("FCRate")
        If IsDBNull(ItmInvCmnTb(0)("isTaxInvoice")) Then ItmInvCmnTb(0)("isTaxInvoice") = 0
        cmblocation.Text = Trim(ItmInvCmnTb(0)("DocDefLoc") & "")
        chkTaxbill.Checked = ItmInvCmnTb(0)("isTaxInvoice")
        sRs = _objcmnbLayer._fldDatatable("SELECT AccMast.accid,Alias,AccDescr,Address1,Address2,Address3,Address4,Phone,Fax,EMail,PT1,PT2,PT3,PT4,TrdLcno,TrdDate from AccMast LEFT JOIN AccMastAddr ON AccMast.AccountNo=AccMastAddr.AccountNo WHERE AccMast.accid = " & ItmInvCmnTb(0)("CSCode"))
        chgbyprg = True
        If sRs.Rows.Count > 0 Then
            txtSuppAlias.Tag = sRs(0)("accid")
            txtSuppAlias.Text = Trim("" & sRs(0)("Alias"))
            txtSuppName.Text = Trim("" & sRs(0)("AccDescr"))
            lbladd1.Text = Trim(sRs(0)("Address1") & "")
            lbladd2.Text = Trim(sRs(0)("Address2") & "")
            lbladd3.Text = Trim(sRs(0)("Address3") & "")
            lbladd4.Text = Trim(sRs(0)("Address4") & "")
            lbladd5.Text = Trim(sRs(0)("Phone") & "")
            lbladd6.Text = Trim(sRs(0)("EMail") & "")
            lbladd7.Text = Trim(sRs(0)("TrdLcno") & "")
            lbltrdate.Text = Trim(sRs(0)("TrdDate") & "")
        End If
        If sRs.Rows.Count > 0 Then sRs.Clear()
        sRs = _objcmnbLayer._fldDatatable("SELECT * FROM AccMast WHERE accid = " & ItmInvCmnTb(0)("PSAcc"))

        If sRs.Rows.Count > 0 Then
            txtPurchaseName.Tag = ItmInvCmnTb(0)("PSAcc")
            txtPurchAlias.Text = sRs(0)("Alias")
            txtPurchaseName.Text = sRs(0)("AccDescr")
        End If
        txtReference.Text = Trim("" & ItmInvCmnTb(0)("TrRefNo"))
        txtDescr.Text = Trim("" & ItmInvCmnTb(0)("TrDescription"))
        numDisc.Text = Format(ItmInvCmnTb(0)("Discount") / FCRt, lnumformat)
        '-----------------------for Other Info ------------------------
        'If Not fOthInf Is Nothing Then FillOthInf()
        'If Not IsDBNull(ItmInvCmnTb(0)("SuppInvDate")) Then dtSuppDate.CtlText = DateValue(ItmInvCmnTb(0)("SuppInvDate"))
        On Error GoTo 0
        LddImpDocs = ""
        CTVol = IIf(IsDBNull(ItmInvCmnTb(0)("InvTtlVol")), 0, ItmInvCmnTb(0)("InvTtlVol"))
        CTWt = IIf(IsDBNull(ItmInvCmnTb(0)("InvTtlWt")), 0, ItmInvCmnTb(0)("InvTtlWt"))
        CTQty = IIf(IsDBNull(ItmInvCmnTb(0)("InvTtlQty")), 0, ItmInvCmnTb(0)("InvTtlQty"))
        OthCost = Format(Val(ItmInvCmnTb(0)("OthCost")), lnumformat)
        lblOthCost.Text = Format(OthCost, lnumformat)
        Protect = (ItmInvCmnTb(0)("TrDate") <= ProtectUntil)
        PasteFrom(loadedTrId)
        showOtherCost(False)
        calculate()
        If Protect Then
            MsgBox("Voucher comes under Protected Range.  You can't Post modifications.", vbInformation)
            varProtectedByRights = True
        ElseIf CrossBr Then
            MsgBox("Found multi-branches or branches other than you loged.  Can't Post modifications.", vbInformation)
            varProtectedByRights = True
        Else
            'btnUpdate.Enabled = (Val(btnUpdate.Tag) > 0)
            'btnRemoveRec.Enabled = (Val(btnRemoveRec.Tag) > 0)
        End If
        'For i = 0 To 10 - grdVoucher.RowCount
        '    AddRow(True)
        'Next
        numVchrNo.Focus()
        chgbyprg = False
        ChgId = False
        chgPost = False
        GoTo Quit
Err:
        If MsgBox(Err.Description & Chr(13) & "Retry?", vbRetryCancel + vbQuestion, "Warning During Opening Data File.") = vbRetry Then Resume
Quit:
        chgbyprg = False
    End Sub

    Private Sub numVchrNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles numVchrNo.KeyPress
        If Not IsNumeric(e.KeyChar) And Not e.KeyChar = Convert.ToChar(Keys.Back) Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        If isModi Then
        Else
            NextNumber()
        End If
    End Sub
    Private Sub NextNumber()

        Try
            chgbyprg = True
            ldDefault()
            txtReference.Focus()
            chgbyprg = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ldDefault()
        Dim table As DataTable
        chgbyprg = True
        txtPurchaseName.Tag = 0
        txtPurchaseName.Text = ""
        txtSuppAlias.Tag = 0
        txtSuppAlias.Text = ""
        txtSuppName.Text = ""
        Dim userquery As String
        Dim dtset As DataSet
        Dim qry As String
        qry = " SELECT WarrentyName FROM WarrentyMasterTb"
        qry = qry & " SELECT SManCode FROM SalesmanTb"
        qry = qry & " SELECT CurrencyCode FROM CurrencyTb"
        qry = qry & " SELECT LocCode FROM LocationTb"
        
        If (cmbVoucherTp.Text = "TI") Then
            txtPurchaseName.Tag = getConstantAccounts(1)
            txtSuppAlias.Tag = getConstantAccounts(14)
        Else
            txtSuppAlias.Tag = getConstantAccounts(1)
            txtPurchaseName.Tag = getConstantAccounts(15)
        End If
        userquery = IIf(UsrBr = "", " SELECT * FROM InvNos WHERE InvType='" & cmbVoucherTp.Text & "'", _
                         " SELECT * FROM InvNosBrTb WHERE Brcode='" & UsrBr & "' AND InvType='" & cmbVoucherTp.Text & "'")
        userquery = userquery & " SELECT accid,Accdescr,alias from accmast where accid=" & Val(txtPurchaseName.Tag)
        userquery = userquery & " SELECT accid,Accdescr,alias from accmast where accid=" & Val(txtSuppAlias.Tag)
        userquery = userquery & " SELECT * FROM VoucherTypeNoTb WHERE VoucherType='" & cmbVoucherTp.Text & "'"
        userquery = userquery & qry

        dtset = _objcmnbLayer._ldDataset(userquery, False)
        table = dtset.Tables(0)
        If Not isModi Then
            If table.Rows.Count > 0 Then
                numVchrNo.Text = Val(table(0)("InvNo"))
                txtprefix.Text = Trim(table(0)("Prefix") & "")
            End If
        End If
        table = Nothing
        table = dtset.Tables(1)
        If (table.Rows.Count > 0) Then
            If table.Rows.Count > 0 Then
                txtPurchaseName.Tag = Val(table(0)("accid"))
                txtPurchaseName.Text = table(0)("Accdescr")
            End If
        End If
        table = Nothing
        table = dtset.Tables(2)
        If (table.Rows.Count > 0) Then
            txtSuppAlias.Text = table(0)("alias")
            txtSuppAlias.Tag = Val(table(0)("accid"))
            txtSuppName.Text = table(0)("Accdescr")
        End If
        If dtset.Tables(3).Rows.Count > 0 Then
            TrTypeNo = Val(dtset.Tables(3)(0)(0) & "")
        End If
        If EnableWarranty Then
            dtwarrenty = dtset.Tables(4)
        End If
        dtsalesman = dtset.Tables(5)

        dtcurrentyTb = dtset.Tables(6)
        dtlocationTb = dtset.Tables(7)

        chgbyprg = False
    End Sub




    Private Sub txtReference_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDescr.KeyDown, txtSuppName.KeyDown, txtSuppAlias.KeyDown, txtReference.KeyDown, txtjobname.KeyDown, _
                                                                                                                   txtJob.KeyDown, txtcredit.KeyDown, txtPurchaseName.KeyDown
        lstKey = e.KeyCode
        Dim MyCtrl As TextBox = sender
        If e.KeyCode = Keys.Return Then

            If MyCtrl.Name = "txtDescr" Then
                If grdVoucher.Rows.Count > 0 Then
                    activecontrolname = "grdVoucher"
                    grdVoucher.CurrentCell = grdVoucher.Item(1, grdVoucher.CurrentRow.Index)
                    grdBeginEdit()
                Else
                    AddRow()
                End If
            Else
                SendKeys.Send("{TAB}")
            End If
            If Not fMList Is Nothing Then fMList.Visible = False
        ElseIf e.KeyCode = Keys.F2 Then
            If Not fMList Is Nothing Then fMList.Visible = False
            Select Case MyCtrl.Name
                Case "txtSuppAlias", "txtSuppName"
                    _srchTxtId = IIf(MyCtrl.Name = "txtSuppAlias", 1, 2)
                    ldSelect(0)
                Case "txtJob"
                    _srchTxtId = 3
                    ldSelect(3)
            End Select
        ElseIf e.KeyCode = Keys.Up Or e.KeyCode = Keys.PageUp Then
            If fMList.Visible Then
                fMList.MoveUp(MyCtrl.Text)
                Exit Sub
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
            fSelect.Width = 491
            fSelect.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
        Else
            fSelect.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedToolWindow
            fSelect.Width = 425
        End If
        fSelect.Show()
    End Sub


    Private Sub txtLocation_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtJob.TextChanged, txtSuppName.TextChanged, txtSuppAlias.TextChanged, txtcredit.TextChanged, txtPurchaseName.TextChanged
        If chgbyprg = True Then Exit Sub
        Dim myCtrl As Control = sender
        Select Case myCtrl.Name
            Case "txtSuppAlias"
                _srchTxtId = 1
            Case "txtSuppName"
                _srchTxtId = 2
            Case "txtJob"
                _srchTxtId = 3
            Case "txtcredit"
                _srchTxtId = 4
            Case "txtPurchaseName"
                _srchTxtId = 5
        End Select
        _srchOnce = False
        ShowFmlist(sender)
        'btnUpdate.Enabled = Val(btnUpdate.Tag) > 0
        chgPost = True
    End Sub
    Private Sub ShowFmlist(ByVal myCtrl As Control)
        If chgbyprg Then Exit Sub
        MyActiveControl = myCtrl
        If Not _srchOnce Then
            chgbyprg = True
            If fMList Is Nothing Then
                fMList = New Mlistfrm
            End If
            Dim PopupLoc As Point
            Dim x As Integer = Width - fMList.Width - 100
            Dim y As Integer = Height - fMList.Height - 100
            PopupLoc = New Point(x, y)
            If fMList.Visible = False Then
                Select Case _srchTxtId - 1
                    Case 0, 1
                        SetFmlist(fMList, 20)
                    Case 2
                        SetFmlist(fMList, 8)
                    Case 3 'job 
                        SetFmlist(fMList, 13, 0)
                    Case 4
                        SetFmlist(fMList, 36)
                End Select
                fMList.loc = PopupLoc
                fMList.Show(fMainForm)
                fMList.Visible = True
            End If
        End If
        Select Case _srchTxtId - 1
            Case 0  'Customer Code
                fMList.SearchIndex = 1
                fMList.SearchIndexDescr = 0
                fMList_doFocus()
                fMList.Search(txtSuppAlias.Text)
                txtSuppName.Text = fMList.AssignList(txtSuppAlias, lstKey, chgbyprg)
            Case 1   'Customer name
                fMList.SearchIndex = 0
                fMList.SearchIndexDescr = 1
                fMList_doFocus()
                fMList.Search(txtSuppName.Text)
                txtSuppAlias.Text = fMList.AssignList(txtSuppName, lstKey, chgbyprg)
            Case 2   'job
                fMList.SearchIndex = 0
                'fMList.SearchIndexDescr = 0
                fMList_doFocus()
                fMList.Search(txtJob.Text)
                fMList.AssignList(txtJob, lstKey, chgbyprg)
            Case 3
                fMList.SearchIndex = 0
                fMList_doFocus()
                fMList.Search(txtcredit.Text)
                fMList.AssignList(txtcredit, lstKey, chgbyprg, False)
            Case 4
                fMList.SearchIndex = 0
                fMList.SearchIndexDescr = 1
                fMList_doFocus()
                fMList.Search(txtPurchaseName.Text)
                txtPurchAlias.Text = fMList.AssignList(txtPurchaseName, lstKey, chgbyprg, False)
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
        Select Case _srchTxtId - 1
            Case 0, 1
                txtSuppAlias.Text = ItmFlds(1)
                txtSuppName.Text = ItmFlds(0)
                txtSuppAlias.Tag = ItmFlds(3)
            Case 2
                txtJob.Text = ItmFlds(0)
            Case 3
                txtcredit.Text = ItmFlds(0)
                txtcredit.Tag = ItmFlds(3)
            Case 4
                txtPurchAlias.Text = ItmFlds(1)
                txtPurchaseName.Text = ItmFlds(0)
                txtPurchaseName.Tag = ItmFlds(3)
        End Select
        chgbyprg = False
    End Sub
    Private Sub SetGridHead()
        chgbyprg = True
        SetGridHeadEntryProperty(grdVoucher)
        With grdVoucher
            '.Columns(ConstDescr).Width = 350
            '.Columns(ConstDisAmt).Width = Width * 7 / 100 '70
            '.Columns(ConstTaxP).Visible = False
            '.Columns(ConstTaxAmt).Visible = False
            '.Columns(ConstDisAmt).Visible = False
            '.Columns(ConstDisP).Visible = False
            '.Columns(ConstBarcode).Visible = False
            '.Columns(ConstUnitOthCost).Width = Width * 9 / 100 '80
            '.Columns(ConstNUPrice).Width = Width * 8 / 100 '70
            If cmbVoucherTp.Text = "TO" Then
                If enableBatchwiseTr Then
                    .Columns(ConstWarrentyExpiry).ReadOnly = True
                    .Columns(ConstManufacturingdate).Visible = False
                End If
            Else
                .Columns(ConstMRP).Visible = enableMRPInStockIn
                .Columns(ConstSP1).Visible = enableSP1InStockIn
                .Columns(ConstSP2).Visible = enableSP2InStockIn
                .Columns(ConstSP3).Visible = enableSP3InStockIn
            End If
            .Columns(ConstTaxP).Visible = False
            .Columns(ConstTaxAmt).Visible = False
            .Columns(ConstcessAmt).Visible = False
        End With
        chgbyprg = False
    End Sub


    Private Sub fSelect_doSelect(ByVal KeyId As String, ByVal strFld1 As String, ByVal strFld2 As String) Handles fSelect.doSelect
        chgbyprg = True
        If _srchTxtId = 1 Or _srchTxtId = 2 Then
            txtSuppAlias.Text = strFld2
            txtSuppName.Text = strFld1
            txtSuppAlias.Tag = KeyId
        ElseIf _srchTxtId = 3 Then
            txtJob.Text = strFld1
        End If
        chgbyprg = False
    End Sub

    Private Sub AddRow(Optional ByVal tocheck As Boolean = False)
        Dim i As Integer
        'ChgByPrg = True
        If grdVoucher.RowCount > 0 And Not tocheck Then
            If Val(grdVoucher.Item(ConstItemID, i).Value) = 0 Then Exit Sub
        End If
        With grdVoucher
            activecontrolname = "grdVoucher"
            i = .RowCount '- 1
            .Rows.Add(1)
            .Item(ConstSlNo, i).Value = i + 1
            .Item(ConstBarcode, i).Value = ""
            .Item(ConstItemCode, i).Value = ""
            .Item(ConstDescr, i).Value = ""
            .Item(ConstUnit, i).Value = ""
            .Item(ConstQty, i).Value = Format(0, lnumformat)
            .Item(ConstUPrice, i).Value = Format(0, lnumformat)
            .Item(ConstDisP, i).Value = Format(0, lnumformat)
            .Item(ConstDisAmt, i).Value = Format(0, lnumformat)
            .Item(ConstTaxP, i).Value = Format(0, lnumformat)
            .Item(ConstTaxAmt, i).Value = Format(0, lnumformat)
            .Item(ConstLTotal, i).Value = Format(0, lnumformat) '(.Item(ConstQty, i).Value * DR!unitPrice) - (.Item(ConstQty, i).Value * Val(DR!DiscountVal)) + (.Item(ConstQty, i).Value * Val(DR!TaxVal))
            .Item(ConstNUPrice, i).Value = Format(0, lnumformat) 'Val(DR!unitPrice) - Val(DR!DiscountVal) + Val(DR!TaxVal)
            .Item(ConstItemID, i).Value = 0
            .Item(ConstImpJobChildTbID, i).Value = 0
            .Item(ConstSerialNo, i).Value = ""
            .Item(ConstPMult, i).Value = "1"
            .Item(ConstPFraction, i).Value = "2"
            .Item(ConstImpDocId, i).Value = ""
            .Item(ConstImpLnId, i).Value = ""
            .Item(ConstLrow, i).Value = i + 1
            '.Item(ConstActualPrice, i).Value = 0 ' DR!unitPrice
            '.ClearSelection()
            '.Select()
            '.Rows(i).Selected = True
            If Not grdVoucher.CurrentRow Is Nothing Then
                If Val(grdVoucher.Item(ConstItemID, grdVoucher.CurrentRow.Index).Value) > 0 Then chgItm = False
            End If
            .CurrentCell = .Item(ConstItemCode, i)
            chgbyprg = True
            .BeginEdit(True)
            chgbyprg = False
            chgItm = False
        End With
        calculate()
        'If enableItemAutoPopulate Then
        '    fProductEnquiry = New ItmEnqry
        '    fProductEnquiry.ShowDialog()
        'End If
        'ChgByPrg = False

    End Sub
    Private Sub calcualteLineTotal(ByVal RowIndex As Integer)
        With grdVoucher
            Dim i As Integer
            i = RowIndex
            If Val(.Item(ConstItemID, i).Value) = 0 Then Exit Sub
            .Item(ConstSlNo, i).Value = i + 1
            If Val(.Item(ConstTaxP, i).Value & "") = 0 Then
                .Item(ConstTaxP, i).Value = 0
            End If
            If Val(.Item(Constcess, i).Value & "") = 0 Then
                .Item(Constcess, i).Value = 0
            End If
            If Val(.Item(ConstActualPrice, i).Value & "") = 0 Then
                .Item(ConstActualPrice, i).Value = 0
            End If
            If Val(.Item(ConstQty, i).Value & "") = 0 Then
                .Item(ConstQty, i).Value = 0
            End If
            'If EnableGST Then
            '    getGSTDetails(Trim(.Item(ConstBarcode, i).Value & ""), i, True)
            '    If Val(.Item(ConstSGSTAmt, i).Value & "") = 0 Then .Item(ConstSGSTAmt, i).Value = 0
            '    If Val(.Item(ConstCGSTAmt, i).Value & "") = 0 Then .Item(ConstCGSTAmt, i).Value = 0
            '    .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstCGSTAmt, i).Value) + CDbl(.Item(ConstSGSTAmt, i).Value), lnumFormat)
            'Else
            '    .Item(ConstTaxAmt, i).Value = Format(((CDbl(.Item(ConstActualPrice, i).Value) * .Item(ConstTaxP, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value), lnumFormat)
            'End If
            'If enablecess And Val(lblgstn.Tag & "") = 0 And cessdate <= DateValue(cldrdate.Value) And chktaxInv.Checked Then
            '    .Item(ConstcessAmt, i).Value = Format((((CDbl(.Item(ConstActualPrice, i).Value) - CDbl(.Item(ConstDiscOther, i).Value)) * .Item(Constcess, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value), lnumFormat)
            'End If
            'If chkTaxbill.Checked = False Then
            '    .Item(ConstTaxAmt, i).Value = Format(0, lnumformat)
            '    .Item(ConstTaxP, i).Value = Format(0, lnumformat)
            '    .Item(ConstcessAmt, i).Value = 0
            'End If

            .Item(constItmTot, i).Value = Format((.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value), lnumFormat)
            .Item(ConstLTotal, i).Value = Format((.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value) + CDbl(.Item(ConstTaxAmt, i).Value), lnumFormat)
            .Item(ConstNUPrice, i).Value = CDbl(.Item(ConstActualPrice, i).Value) - (CDbl(.Item(ConstDisAmt, i).Value) / CDbl(.Item(ConstQty, i).Value)) + (CDbl(.Item(ConstTaxAmt, i).Value) / CDbl(.Item(ConstQty, i).Value)) + (CDbl(.Item(ConstcessAmt, i).Value) / CDbl(.Item(ConstQty, i).Value)) - Val(.Item(ConstDiscOther, i).Value)
        End With

    End Sub
    Private Sub calculate(Optional ByVal bFrmCalcOthCost As Boolean = False, Optional ByVal calculateLineTotal As Boolean = False, Optional ByVal chgDiscount As Boolean = False)
        Dim totQty As Double
        Dim totItm As Double
        Dim totTax As Double
        Dim totLnDis As Double
        Dim totAmt As Double
        Dim totCess As Double
        Dim lnTax As Double
        Dim lnttl As Double
        Dim i As Integer
        calOthCost()
        Dim gindex As Integer
        If grdVoucher.CurrentCell Is Nothing Then
            gindex = grdVoucher.RowCount - 1
        Else
            gindex = grdVoucher.CurrentCell.RowIndex
        End If
        If calculateLineTotal And Val(numDisc.Text) = 0 Then
            calcualteLineTotal(gindex)
        End If

        If numDisc.Text = "" Then
            numDisc.Text = Format(0, lnumFormat)
        End If
        With grdVoucher
            lnTax = 0
            For i = 0 To .Rows.Count - 1
                If (calculateLineTotal And Val(numDisc.Text) > 0) Or chgDiscount Then
                    calcualteLineTotal(i)
                End If
                If Val(.Item(ConstItemID, i).Value) = 0 Then GoTo nxt
                totQty = totQty + IIf(Val(.Item(ConstQty, i).Value) > 0, Val(.Item(ConstQty, i).Value), 0)
                'If chkTaxbill.Checked = False Then
                .Item(ConstTaxAmt, i).Value = Format(0, lnumformat)
                .Item(ConstTaxP, i).Value = Format(0, lnumformat)
                .Item(ConstcessAmt, i).Value = 0
                'Else
                '    .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstIGSTAmt, i).Value), lnumformat)
                '    .Item(ConstTaxP, i).Value = Format(CDbl(.Item(ConstIGSTP, i).Value), lnumformat)
                '    lnTax = CDbl(.Item(ConstIGSTAmt, i).Value)
                'End If
                'If chkTaxbill.Checked Then
                '    totTax = totTax + CDbl(.Item(ConstCGSTAmt, i).Value) + CDbl(.Item(ConstSGSTAmt, i).Value)
                'End If
                totLnDis = totLnDis + .Item(ConstDisAmt, i).Value
                totAmt = totAmt + (.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value)
                totItm = totItm + (.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value)

                lnttl = (CDbl(.Item(ConstQty, i).Value) * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value)
                lnttl = lnttl + lnTax
                chgbyprg = True
                .Item(ConstLTotal, i).Value = Format(lnttl, lnumformat)
                chgbyprg = False
nxt:
            Next
            calOthCost()
            totAmt = totAmt + totTax + totCess
            lblTotAmt.Text = Format(totItm, lnumformat)
            lblNetAmt.Text = Format(totAmt - CDbl(numDisc.Text), lnumformat)
            lbltax.Text = Format(totTax, lnumformat)
            totAmt = totAmt - CDbl(numDisc.Text)
            chgAmt = False
        End With
    End Sub
    Private Sub setOthCost()
        Dim i As Integer
        Dim totOthCost As Double
        Dim totOthcostFc As Double
        With grdOtherCost
            For i = 0 To grdOtherCost.Rows.Count - 1
                If Val(.Item(CostAmount, i).Value) = 0 Then .Item(CostAmount, i).Value = 0
                totOthCost = totOthCost + CDbl(.Item(CostAmount, i).Value)
                If Val(.Item(CostFCRate, i).Value) = 0 Then .Item(CostFCRate, i).Value = 1
                totOthcostFc = totOthcostFc + (CDbl(.Item(CostAmount, i).Value) * CDbl(.Item(CostFCRate, i).Value))
            Next
        End With
        lblOthCost.Text = Format(totOthCost, lnumformat)
        lblOthCost.Tag = totOthcostFc
        lblOthCost.Text = Format(totOthCost, lnumformat)
        lblOthCost.Tag = totOthcostFc
        OthCost = totOthcostFc

    End Sub

    Private Function calOthCost(Optional ByVal bfrmSetOthrCost As Boolean = False) As Double
        If Not bfrmSetOthrCost Then setOthCost()
        Dim i As Integer
        Dim tOthVal As Double
        Dim tBAmt As Double
        Dim tBDAmt As Double
        Dim tDAmt As Double
        Dim DiscountOtherAmt As Double
        With grdVoucher
            For i = 0 To .Rows.Count - 1 '- 1
                If IsDBNull(.Item(ConstQty, i).Value) Then .Item(ConstQty, i).Value = 0
                If CStr(.Item(ConstQty, i).Value) = "" Then .Item(ConstQty, i).Value = 0
                If Val(.Item(ConstMthd, i).Value & "") <> 0 Then
                    tOthVal = tOthVal + CDbl(.Item(ConstActualOthCost, i).Value) * CDbl(.Item(ConstQty, i).Value)
                Else
                    tBAmt = tBAmt + CDbl(.Item(ConstLTotal, i).Value)
                End If
                If Val(.Item(ConstDisAmt, i).Value) <> 0 Then
                    tDAmt = tDAmt + CDbl(.Item(ConstDisAmt, i).Value) * CDbl(.Item(ConstQty, i).Value)
                End If
                'If Val(.Item(ConstDiscOther, i).Value) <> 0 Then
                '    DiscountOtherAmt = DiscountOtherAmt + Val(.Item(ConstDiscOther, i).Value) * CDbl(.Item(ConstQty, i).Value)
                'End If
                tBDAmt = tBDAmt + CDbl(.Item(ConstLTotal, i).Value)
                'End If
            Next i
            tOthVal = OthCost / FCRt - tOthVal
            chgbyprg = True
            If numDisc.Text = "" Then numDisc.Text = 0
            chgbyprg = False
            DiscountOtherAmt = CDbl(numDisc.Text)
            'tBAmt = tBAmt - CDbl(txtDis.Text)
            For i = 0 To .Rows.Count - 1 '- 1
                If Val(.Item(ConstDisAmt, i).Value) <> 0 Then
                    tDAmt = Val(.Item(ConstDisAmt, i).Value) '* CDbl(.Item(ConstQty, i).Value)
                Else
                    tDAmt = 0
                End If
                If Val(.Item(ConstMthd, i).Value) = 0 Then
                    If tBAmt = 0 Then
                        .Item(ConstActualOthCost, i).Value = 0
                    Else
                        If Val(grdVoucher.Item(ConstActualPrice, i).Value) = 0 Then grdVoucher.Item(ConstActualPrice, i).Value = 0
                        .Item(ConstActualOthCost, i).Value = tOthVal * (CDbl(grdVoucher.Item(ConstActualPrice, i).Value) - tDAmt) / tBAmt
                    End If
                    grdVoucher.Item(ConstUnitOthCost, i).Value = Format(Val(.Item(ConstActualOthCost, i).Value), "#,##0" & IIf(NoOfDecimal = 0, "", "." & StrDup(NoOfDecimal, "0"))) ' & NumFormat)'shine
                End If
                'If Val(.Item(const i, 20)) = 0 Then

                If DiscountOtherAmt = 0 Then
                    .Item(ConstDiscOther, i).Value = 0
                Else
                    .Item(ConstDiscOther, i).Value = DiscountOtherAmt * (CDbl(grdVoucher.Item(ConstActualPrice, i).Value) - CDbl(grdVoucher.Item(ConstDisAmt, i).Value)) / tBDAmt
                End If
                'End If
                If Val(.Item(ConstActualPrice, i).Value) = 0 Then .Item(ConstActualPrice, i).Value = 0
                grdVoucher.Item(ConstNUPrice, i).Value = Format(CDbl(.Item(ConstActualPrice, i).Value) + CDbl(.Item(ConstActualOthCost, i).Value) - CDbl(.Item(ConstDiscOther, i).Value) - Val(.Item(ConstDisAmt, i).Value), "#,##0" & IIf(NoOfDecimal = 0, "", "." & StrDup(NoOfDecimal, "0"))) ' & NumFormat)
                If Val(.Item(ConstQty, i).Value) = 0 Then .Item(ConstQty, i).Value = 0
                calOthCost = calOthCost + CDbl(.Item(ConstQty, i).Value) * (Val(.Item(ConstActualOthCost, i).Value) - Val(.Item(ConstDiscOther, i).Value) - Val(.Item(ConstDisAmt, i).Value))
            Next i
        End With
        If lblTotAmt.Text = "" Then
            lblTotAmt.Text = Format(0, lnumformat)
        End If
        If (lblNetAmt.Text = "") Then
            lblNetAmt.Text = Format(0, lnumformat)
        End If
        If (numDisc.Text = "") Then
            numDisc.Text = Format(0, lnumformat)
        End If
        If (lbltax.Text = "") Then
            lbltax.Text = Format(0, lnumformat)
        End If
        lblNetAmt.Text = Format(((CDbl(lblTotAmt.Text) + CDbl(lbltax.Text)) - CDbl(numDisc.Text)), lnumformat)
    End Function
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            Const WM_KEYDOWN As Integer = &H100
            Const WM_KEYUP As Integer = &H101
            'msg.ToString()
            If msg.Msg = WM_KEYDOWN Or msg.Msg = WM_KEYUP Then
                If msg.WParam.ToInt32() = CInt(Keys.F8) Then
                    UpdateClick()
                ElseIf msg.WParam.ToInt32() = CInt(Keys.F5) Then
                    ClearClick()
                ElseIf activecontrolname = "grdVoucher" Then
                    If msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up) Or msg.WParam.ToInt32() = CInt(Keys.Enter) Or msg.WParam.ToInt32() = CInt(Keys.F2) Or msg.WParam.ToInt32() = CInt(Keys.F3) Or msg.WParam.ToInt32() = CInt(Keys.F4) Then
                        If (msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up)) And plsrch.Visible = False Then GoTo ctn
                        grdVoucher_KeyDown(Nothing, New KeyEventArgs(keyData))
                        Return True
                    End If
                ElseIf msg.WParam.ToInt32() = CInt(Keys.Escape) Then
                    If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
                    If Not fSelect Is Nothing Then fSelect.Close() : fSelect = Nothing
                    If Not fProductEnquiry Is Nothing Then fProductEnquiry.Close() : fProductEnquiry = Nothing

                End If
            End If
ctn:
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub grdVoucher_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellClick
        If cmbVoucherTp.Text = "TO" Then
            If e.ColumnIndex = ConstWarrentyExpiry And enableBatchwiseTr Then
                grdVoucher.CurrentCell.ReadOnly = True
            End If
        End If
        If grdVoucher.Item(e.ColumnIndex, e.RowIndex).ReadOnly = False Then grdBeginEdit()
    End Sub

    Private Sub grdVoucher_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellValidated
        Valid(e.RowIndex, e.ColumnIndex)
        SrchText = ""
    End Sub
    Private Sub Valid(ByVal RowIndex As Integer, ByVal ColIndex As Integer)
        With grdVoucher
            Select Case ColIndex
                Case ConstBarcode, ConstItemCode
                    If Not chgItm Then Exit Sub
                    If Trim(.Item(ColIndex, RowIndex).Value) = "" And SrchText <> "" Then .Item(ColIndex, RowIndex).Value = SrchText
                    If Trim(.Item(ColIndex, RowIndex).Value) <> "" And SrchText = "" Then SrchText = .Item(ColIndex, RowIndex).Value
                    Dim dtItms As DataTable
                    Dim found As Boolean
                    'dtItms = getItmDtls(3, SrchText, True)
                    dtItms = ItmValidation(3, SrchText, True, cmbVoucherTp.Text, Val(txtSuppAlias.Tag))
                    If dtItms.Rows.Count > 0 Then
                        found = True
                        AddDetails(dtItms, RowIndex)
                    End If
                    chgItm = False
                    SrchText = ""
                    If Not found Then
                        .Item(ConstBarcode, RowIndex).Value = ""
                        .Item(ConstItemCode, RowIndex).Value = ""
                        .Item(ConstImpJobChildTbID, RowIndex).Value = ""
                        .Item(ConstItemID, RowIndex).Value = ""
                        .Item(ConstUnit, RowIndex).Value = ""
                        .Item(ConstSerialNo, RowIndex).Value = ""
                        .Item(ConstPMult, RowIndex).Value = "1"
                        .Item(ConstPFraction, RowIndex).Value = "2"
                        .Item(ConstImpDocId, RowIndex).Value = ""
                        .Item(ConstImpLnId, RowIndex).Value = ""
                        chgItm = False
                    End If
                Case ConstQty

                    If chgAmt Then
                        calculate(, True)
                    End If

                Case ConstUPrice
                    If chgAmt Then
                        .Item(ConstActualPrice, RowIndex).Value = .Item(ConstUPrice, RowIndex).Value
                        calculate(, True)
                    End If
                Case ConstDisAmt
                    If chgAmt Then
                        calculate(, True)
                    End If
                Case ConstTaxAmt
                    If chgAmt Then
                        calculate(, True)
                    End If
                Case ConstTaxP
                    If chgAmt Then
                        calculate(, True)
                    End If
                Case Else
            End Select
        End With
    End Sub
    'Private Sub CalculateGST()
    '    Dim i As Integer
    '    Dim dtHSN As DataTable
    '    Dim dtrow As DataRow
    '    Dim slno As Integer
    '    Dim cnt As Integer
    '    Exit Sub
    '    If Not dtTax Is Nothing Then dtTax.Rows.Clear()
    '    With grdVoucher
    '        cnt = .RowCount - 1
    '        Dim _qurey As EnumerableRowCollection(Of DataRow)
    '        For i = 0 To cnt
    '            slno = 0
    '            _qurey = From data In dtGST.AsEnumerable() Where data("HSNCode") = Trim(.Item(ConstBarcode, i).Value & "") Select data
    '            If _qurey.Count > 0 Then
    '                dtHSN = _qurey.CopyToDataTable
    '                If Val(lblstatecode.Tag) = 0 Then
    '                    'adding CSGT Amount****
    '                    Dim a = (From data In dtTax.AsEnumerable() Where data("ACid") = dtHSN(0)("CGSTPAc") Select data("slno"))
    '                    For Each itm In a
    '                        Try
    '                            slno = itm
    '                        Catch ex As Exception
    '                            MsgBox(ex.Message)
    '                        End Try
    '                    Next
    '                    If slno = 0 Then
    '                        dtrow = dtTax.NewRow
    '                        dtrow("slno") = dtTax.Rows.Count + 1
    '                        dtrow("AccountName") = dtHSN(0)("CGSTPAname")
    '                        dtrow("ACid") = dtHSN(0)("CGSTPAc")
    '                        dtrow("Amount") = CDbl(.Item(ConstCGSTAmt, i).Value)
    '                        dtTax.Rows.Add(dtrow)
    '                    Else
    '                        dtTax(slno - 1)("Amount") = CDbl(dtTax(slno - 1)("Amount")) + CDbl(.Item(ConstCGSTAmt, i).Value)
    '                    End If
    '                    'adding SSGT Amount****
    '                    a = (From data In dtTax.AsEnumerable() Where data("ACid") = dtHSN(0)("SGSTPAc") Select data("slno"))
    '                    slno = 0
    '                    For Each itm In a
    '                        Try
    '                            slno = itm
    '                        Catch ex As Exception
    '                            MsgBox(ex.Message)
    '                        End Try

    '                    Next
    '                    If slno = 0 Then
    '                        dtrow = dtTax.NewRow
    '                        dtrow("slno") = dtTax.Rows.Count + 1
    '                        dtrow("AccountName") = dtHSN(0)("SGSTPAname")
    '                        dtrow("ACid") = dtHSN(0)("SGSTPAc")
    '                        dtrow("Amount") = CDbl(.Item(ConstSGSTAmt, i).Value)
    '                        dtTax.Rows.Add(dtrow)
    '                    Else
    '                        dtTax(slno - 1)("Amount") = CDbl(dtTax(slno - 1)("Amount")) + CDbl(.Item(ConstSGSTAmt, i).Value)
    '                    End If
    '                Else
    '                    'adding ISGT Amount****
    '                    Dim a = (From data In dtTax.AsEnumerable() Where data("ACid") = dtHSN(0)("IGSTPAc") Select data("slno"))
    '                    slno = 0
    '                    For Each itm In a
    '                        slno = itm
    '                    Next
    '                    If slno = 0 Then
    '                        dtrow = dtTax.NewRow
    '                        dtrow("slno") = dtTax.Rows.Count + 1
    '                        dtrow("AccountName") = dtHSN(0)("IGSTPAname")
    '                        dtrow("ACid") = dtHSN(0)("IGSTPAc")
    '                        dtrow("Amount") = CDbl(.Item(ConstIGSTAmt, i).Value)
    '                        dtTax.Rows.Add(dtrow)
    '                    Else
    '                        dtTax(slno - 1)("Amount") = CDbl(dtTax(slno - 1)("Amount")) + CDbl(.Item(ConstIGSTAmt, i).Value)
    '                    End If
    '                End If
    '            End If
    '        Next
    '    End With
    'End Sub
    'Private Sub getGSTDetails(ByVal hsncode As String, ByVal i As Integer, ByVal calculatefromGrid As Boolean)
    '    Dim dt As DataTable
    '    With grdVoucher
    '        If Not calculatefromGrid Then
    '            dt = _objcmnbLayer._fldDatatable("SELECT * FROM GSTTb WHERE HSNCODE='" & hsncode & "'")
    '            If dt.Rows.Count > 0 Then
    '                .Item(ConstCGSTP, i).Value = Format(IIf(IsDBNull(dt(0)("CGST")), 0, CDbl(dt(0)("CGST"))), lnumformat)
    '                .Item(ConstSGSTP, i).Value = Format(IIf(IsDBNull(dt(0)("SGST")), 0, CDbl(dt(0)("SGST"))), lnumformat)
    '                .Item(ConstIGSTP, i).Value = Format(IIf(IsDBNull(dt(0)("IGST")), 0, CDbl(dt(0)("IGST"))), lnumformat)
    '            End If
    '        End If
    '        Dim actualPrice As Double
    '        Dim discountOther As Double
    '        discountOther = CDbl(.Item(ConstDiscOther, i).Value) * CDbl(.Item(ConstQty, i).Value)
    '        actualPrice = (CDbl(.Item(ConstActualPrice, i).Value) * CDbl(.Item(ConstQty, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value) - discountOther
    '        actualPrice = Format(actualPrice, lnumformat)
    '        .Item(ConstCGSTAmt, i).Value = (actualPrice * .Item(ConstCGSTP, i).Value) / 100
    '        .Item(ConstSGSTAmt, i).Value = (actualPrice * .Item(ConstSGSTP, i).Value) / 100
    '        .Item(ConstIGSTAmt, i).Value = (actualPrice * .Item(ConstIGSTP, i).Value) / 100
    '        If Val(lblstatecode.Tag) = 1 Then
    '            .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstIGSTAmt, i).Value), lnumformat)
    '            .Item(ConstTaxP, i).Value = .Item(ConstIGSTP, i).Value
    '        Else
    '            .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstCGSTAmt, i).Value) + CDbl(.Item(ConstSGSTAmt, i).Value), lnumformat)
    '            .Item(ConstTaxP, i).Value = CDbl(.Item(ConstCGSTP, i).Value) + CDbl(.Item(ConstSGSTP, i).Value)
    '        End If
    '    End With
    'End Sub

    Private Sub AddDetails(ByVal DR As DataTable, ByVal rowindex As Integer)
        Dim i As Integer
        Dim PMult As Double
        Dim lcost As Double
        With grdVoucher
            chgbyprg = True
            PMult = 1
            i = rowindex '.CurrentRow.Index ' .RowCount - 1
            .Item(ConstSlNo, i).Value = i + 1
            .Item(ConstItemCode, i).Value = IIf(IsDBNull(DR(0)("Item Code")), Trim(DR(0)("Item Code") & ""), DR(0)("Item Code"))
            .Item(ConstDescr, i).Value = DR(0)("Description")
            If EnableGST Then
                .Item(ConstBarcode, i).Value = DR(0)("HSNCode") 'hsncode
            Else
                .Item(ConstBarcode, i).Value = ""
            End If

            .Item(ConstQty, i).Value = Format(1, lnumformat) 'IIf(IsReturn, -1, 1)
            .Item(ConstItemID, i).Value = DR(0)("ItemId")
            .Item(ConstPMult, i).Value = 1 'IIf(IsNull(sRs(0)("PFraCount), "2", sRs(0)("PFraCount)

            If DR(0)("ItemCategory") = "Comment" Then
                onceChgFld = (CStr(.Item(ConstSlNo, i).Value) <> "M")
                .Item(ConstSlNo, i).Value = "M"
                .Item(ConstB, i).Value = 0
                .Item(ConstUnit, i).Value = ""
                .Item(ConstSerialNo, i).Value = ""
                .Item(ConstPMult, i).Value = "1"
                .Item(ConstPFraction, i).Value = "2"
                .Item(ConstImpDocId, i).Value = ""
                .Item(ConstImpLnId, i).Value = ""
            Else
                onceChgFld = (CStr(.Item(ConstSlNo, i).Value) = "M" Or CStr(.Item(ConstSlNo, i).Value) = "L")
                If onceChgFld Then
                    .Item(ConstSlNo, i).Value = ""
                    .Item(ConstBarcode, i).Value = ""
                    .Item(ConstItemCode, i).Value = ""
                End If
            End If
            .Item(ConstB, .CurrentCell.RowIndex).Value = "B"
            .Item(ConstPFraction, i).Value = IIf(IsDBNull(DR(0)("FraCount")), "2", DR(0)("FraCount"))
            .Item(ConstUnit, i).Value = DR(0)("Unit")
            .Item(ConstWarrentyExpiry, i).Value = Format(DateAdd(DateInterval.Year, 1, DateValue(cldrdate.Value)), DtFormat)
            If CDbl(.Item(ConstUPrice, i).Value) = 0 Or chgItm Then

                If Val(DR(0)("LastPurchCost")) > 0 Then
                    lcost = DR(0)("LastPurchCost")
                Else
                    lcost = Val(DR(0)("opcost") & "")
                End If
                .Item(ConstActualPrice, i).Value = lcost 'getPurchAmt(lcost, 0, Val(.Item(ConstItemID, .CurrentCell.RowIndex).Value))
                .Item(ConstUPrice, i).Value = Format(CDbl(.Item(ConstActualPrice, i).Value), lnumformat)
            End If
            If cmbVoucherTp.Text = "TO" Then
                .Item(ConstBatchCost, i).Value = lcost
            End If
            .Item(ConstLTotal, i).Value = Format(.Item(ConstQty, i).Value * CDbl(.Item(ConstUPrice, i).Value), lnumformat)
            .Item(ConstNUPrice, i).Value = Format(CDbl(.Item(ConstUPrice, i).Value), lnumformat)
            If ShowTaxOnInventory Then
                If Val(DR(0)("vat") & "") = 0 Then
                    DR(0)("vat") = 0
                End If
                .Item(ConstTaxP, i).Value = Format(IIf(IsDBNull(DR(0)("vat")), 0, CDbl(DR(0)("vat"))), lnumformat)
                .Item(ConstTaxAmt, i).Value = ((CDbl(.Item(ConstActualPrice, i).Value) * .Item(ConstTaxP, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value)
            ElseIf EnableGST Then
                'getGSTDetails(Trim(.Item(ConstBarcode, i).Value & ""), i, False)
            End If
            If Not IsDBNull(DR(0)("isSerialNo")) Then
                .Item(ConstIsSerial, i).Value = IIf(DR(0)("isSerialNo"), 1, 0)
            Else
                .Item(ConstIsSerial, i).Value = 0
            End If

            .Item(ConstQty, i).Value = Format(CDbl(.Item(ConstQty, i).Value), "#,##0" & IIf(Val(.Item(ConstPFraction, i).Value) = 0, "", "." & Strings.StrDup(CByte(.Item(ConstPFraction, i).Value), "0")))
            .Item(ConstImpDocId, i).Value = ""
            .Item(ConstImpLnId, i).Value = ""
            chgAmt = True
            chgItm = False
            .ClearSelection()
        End With
        calculate(, True)
        chgbyprg = False
    End Sub


    Private Sub grdVoucher_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellValueChanged
        If chgbyprg Then Exit Sub
        'ChgByPrg = True
        With grdVoucher
            Dim i As Integer = e.RowIndex
            'btnUpdate.Enabled = True
            chgPost = True
            Select Case e.ColumnIndex
                Case ConstItemCode, ConstBarcode
                    chgItm = True
                Case ConstQty, ConstUPrice
                    '.Item(ConstLTotal, i).Value = Format((.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - (.Item(ConstQty, i).Value * .Item(ConstDisAmt, i).Value) + (.Item(ConstQty, i).Value * .Item(ConstTaxAmt, i).Value), lnumformat)
                    '.Item(ConstNUPrice, i).Value = CDbl(.Item(ConstActualPrice, i).Value) - .Item(ConstDisAmt, i).Value + .Item(ConstTaxAmt, i).Value
                    chgAmt = True
                Case ConstLTotal
                    If Val(grdVoucher.Item(ConstQty, i).Value) > 0 Then
                        If Val(grdVoucher.Item(ConstDisAmt, i).Value) = 0 And CDbl(grdVoucher.Item(ConstTaxAmt, i).Value) = 0 Then 'Not AllowUnitDiscountEntryOnInventory And Not ShowTaxOnInventory And
                            chgbyprg = True
                            grdVoucher.Item(ConstUPrice, i).Value = Format(CDbl(grdVoucher.Item(ConstLTotal, i).Value) / grdVoucher.Item(ConstQty, i).Value, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) 'IIf(IsReturn, -1, 1)
                            grdVoucher.Item(ConstActualPrice, i).Value = CDbl(grdVoucher.Item(ConstLTotal, i).Value) / grdVoucher.Item(ConstQty, i).Value
                            calculate(, True)
                            chgbyprg = False
                        End If
                    End If
                    chgAmt = True
                Case ConstDisAmt
                    chgAmt = True
                Case ConstSP1, ConstSP2, ConstSP3
                    chgUprice = True
                    calculateTaxFromUnitPrice(e.RowIndex, e.ColumnIndex)
            End Select
        End With
    End Sub
    Private Sub grdVoucher_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdVoucher.EditingControlShowing
        Dim Col As Integer
        Col = grdVoucher.CurrentCell.ColumnIndex
        If Col = ConstItemCode Or Col = ConstQty Then
            Dim tb As TextBox = CType(e.Control, TextBox)
            RemoveHandler tb.TextChanged, AddressOf Textbox_TextChanged
            AddHandler tb.TextChanged, AddressOf Textbox_TextChanged
        End If
        If Col = ConstItemCode Or Col = ConstSerialNo Or Col = ConstQty Then
            Dim tb As TextBox = CType(e.Control, TextBox)
            RemoveHandler tb.KeyPress, AddressOf Textbox_KeyPress
            AddHandler tb.KeyPress, AddressOf Textbox_KeyPress
        End If
    End Sub
    Private Sub Textbox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            Dim col As Integer
            col = grdVoucher.CurrentCell.ColumnIndex
            If col = ConstQty Or col = ConstNUPrice Or col = ConstLTotal Then
                If Not IsNumeric(e.KeyChar) And e.KeyChar <> "." And Not e.KeyChar = Convert.ToChar(Keys.Back) Then e.Handled = True
                If col = ConstQty Then
                    If grdVoucher.Item(ConstSerialNo, grdVoucher.CurrentRow.Index).Value <> "" And Not enableBatchwiseTr Then
                        e.Handled = True
                    End If
                End If
            ElseIf col = ConstSerialNo And Not enableBatchwiseTr Then
                e.Handled = True
            ElseIf col = ConstItemCode And enableItemAutoPopulate Then
                e.Handled = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub Textbox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim col As Integer
            col = grdVoucher.CurrentCell.ColumnIndex
            Dim MyCtrl As TextBox = sender
            If col = ConstItemCode Or ConstDescr Then strGridSrchString = MyCtrl.Text
            If chgbyprg Then Exit Sub
            _srchOnce = False
            If col = ConstItemCode Then
                '_srchTxtId = 1
                'chgbyprg = True
                'strGridSrchString = MyCtrl.Text
                'ShowPanel()
                'chgItm = True
                'grdVoucher.Item(ConstqtyChg, grdVoucher.CurrentRow.Index).Value = "Chg"
                'chgbyprg = False
                chgbyprg = True
                ldtimer.Enabled = False
                ldtimer.Enabled = True
                strGridSrchString = MyCtrl.Text
                _srchTxtId = 1
                chgbyprg = False
            ElseIf col = ConstBarcode Then
                _srchTxtId = 2
                chgbyprg = True
                strGridSrchString = MyCtrl.Text
                ShowPanel()
                chgbyprg = False
            ElseIf col = ConstQty Then
                grdVoucher.Item(ConstqtyChg, grdVoucher.CurrentRow.Index).Value = "Chg"
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub ShowPanel(Optional ByVal isrefreshBatchData As Boolean = False)
        If Not _srchOnce Then
            chgbyprg = True
            Dim PopupLoc As Point
            Dim x As Integer
            Dim y As Integer
            If _srchTxtId = 3 Then
                plsrch.Height = 246
                plsrch.Width = 600
            Else

                plsrch.Height = 300
                plsrch.Width = 700
                'x = Me.Width - plsrch.Width - 100
                'y = Me.Height - plsrch.Height - 100
            End If
            If cmbVoucherTp.Text = "TO" And grdVoucher.CurrentCell.ColumnIndex = ConstSerialNo Then
                x = grdVoucher.Left
            Else
                x = grdVoucher.Left + grdVoucher.Width - plsrch.Width
            End If
            y = grdVoucher.Top
            PopupLoc = New Point(x, y)
            If plsrch.Visible = False Then
                plsrch.Location = PopupLoc
                plsrch.Visible = True
            End If
        End If

        If _srchTxtId = 3 And enableBatchwiseTr Then
            searchProductBatch(grdSrch, strGridSrchString, Val(grdVoucher.Item(ConstItemID, grdVoucher.CurrentRow.Index).Value), isrefreshBatchData)
            If grdSrch.RowCount > 0 And strGridSrchString = "" Then
                strGridSrchString = grdSrch.Item(2, 0).Value
            End If
        Else
            SearchProductPanel(grdSrch, "Item Code", strGridSrchString, True)
            If grdSrch.RowCount > 0 And strGridSrchString = "" Then
                strGridSrchString = grdSrch.Item(0, 0).Value
            End If
        End If
        doSelect(2)
        _srchOnce = True
        chgbyprg = False
    End Sub

    Private Sub grdVoucher_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.GotFocus
        If grdVoucher.RowCount = 0 Then
            AddRow()
        End If
        activecontrolname = "grdVoucher"
    End Sub
    Private Sub grdBeginEdit()
        chgbyprg = True
        grdVoucher.BeginEdit(True)
        chgbyprg = False
    End Sub

    Private Sub grdVoucher_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdVoucher.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If grdVoucher.RowCount = 0 Then Exit Sub
                plsrch.Visible = False
                If Trim(SrchText) = "" And grdVoucher.CurrentCell.ColumnIndex = ConstItemCode Then
                    SrchText = grdVoucher.Item(ConstItemCode, grdVoucher.CurrentRow.Index).Value
                    If Trim(SrchText) = "" Then
                        If enableItemAutoPopulate And Val(grdVoucher.Item(ConstSlNo, grdVoucher.CurrentRow.Index).Value) > 0 Then
                            fProductEnquiry = New ItmEnqry
                            fProductEnquiry.ShowDialog()
                        End If
                        If SrchText = "" Then GoTo nxt
                    Else
                        GoTo cntu
                    End If
                End If
cntu:
                If FindNextCell(grdVoucher, grdVoucher.CurrentCell.RowIndex, grdVoucher.CurrentCell.ColumnIndex + 1) Then
                    AddRow()
                End If
nxt:

              grdBeginEdit

            ElseIf e.KeyCode = Keys.Up Or e.KeyCode = Keys.PageUp Then
                If grdVoucher.RowCount = 0 Then Exit Sub
                If plsrch.Visible Then
                    doSelect(0)
                    Exit Sub
                End If
            ElseIf e.KeyCode = Keys.Down Or e.KeyCode = Keys.PageDown Then
                If grdVoucher.RowCount = 0 Then Exit Sub
                If plsrch.Visible Then
                    doSelect(1)
                    Exit Sub
                End If
            ElseIf e.KeyCode = Keys.F1 Then
                grdBeginEdit()
            ElseIf e.KeyCode = Keys.F2 Then
                If plsrch.Visible Then plsrch.Visible = False
                If grdVoucher.RowCount = 0 Then Exit Sub
                Select Case grdVoucher.CurrentCell.ColumnIndex
                    Case ConstItemCode
                        If Not fMList Is Nothing Then fMList.Visible = False
                        fProductEnquiry = New ItmEnqry
                        fProductEnquiry.isFromPurchase = True
                        fProductEnquiry.ShowDialog()
                    Case ConstSerialNo
                        'If Not fSerialno Is Nothing Then fSerialno.Close() : fSerialno = Nothing
                        'fSerialno = New AddSerialnoFrm
                        'fSerialno.txtserialno.Tag = Val(grdVoucher.Item(ConstItemID, grdVoucher.CurrentRow.Index).Value)
                        'fSerialno.cldrdate.Value = DateAdd(DateInterval.Year, 1, cldrdate.Value)
                        'fSerialno.ShowDialog()
                        showSerialNoFrom()
                End Select
            ElseIf e.KeyCode = Keys.Escape Then
                plsrch.Visible = False
            ElseIf e.KeyCode = Keys.F3 Then
                AddRow()
            ElseIf e.KeyCode = Keys.F4 Then
                If grdVoucher.RowCount = 0 Then Exit Sub
                grdVoucher.Rows.RemoveAt(grdVoucher.CurrentCell.RowIndex)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub showSerialNoFrom()
        If grdVoucher.CurrentCell.ColumnIndex = ConstSerialNo Then
            Dim dt As DataTable
            dt = _objcmnbLayer._fldDatatable("select itemid from InvItmPropertiesTb where isnull(isSerialNo,0)<>0 and itemid=" & Val(grdVoucher.Item(ConstItemID, grdVoucher.CurrentRow.Index).Value))
            If dt.Rows.Count > 0 Then
                If Not fSerialno Is Nothing Then fSerialno.Close() : fSerialno = Nothing
                fSerialno = New AddSerialnoFrm
                With fSerialno
                    ._objbLayer = _objcmnbLayer
                    .txtserialno.Tag = Val(grdVoucher.Item(ConstItemID, grdVoucher.CurrentRow.Index).Value)
                    .cldrdate.Value = DateAdd(DateInterval.Year, 1, cldrdate.Value)
                    .Trid = loadedTrId
                    .isSales = IIf(cmbVoucherTp.Text = "TI", False, True)
                    .detId = Val(grdVoucher.Item(ConstId, grdVoucher.CurrentRow.Index).Value)
                    .rowIndex = grdVoucher.CurrentCell.RowIndex + 1
                    .ShowDialog()
                End With

            End If
        End If
    End Sub
    Private Sub doSelect(ByVal Mup As Integer)
        Try
            chgbyprg = True
            Dim ItmFlds() As String
            If plsrch.Visible = False Then Exit Sub
            If Mup = 0 Then 'UP
                ItmFlds = MoveUpPl(grdSrch, _srchIndexId, strGridSrchString)
            ElseIf Mup = 1 Then 'Down
                ItmFlds = MoveDownPl(grdSrch, _srchIndexId, strGridSrchString)
            Else
                ItmFlds = SelectItmPanel(grdSrch)
            End If
            If strGridSrchString = "" And Mup = 2 Then SrchText = "" : GoTo Nxt

            Select Case _srchTxtId
                Case 1
                    grdVoucher.Item(ConstItemCode, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(0) IsNot Nothing, ItmFlds(0), "")
                    'grdVoucher.Item(ConstBarcode, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(1) IsNot Nothing, ItmFlds(1), "")
                    grdVoucher.Item(ConstDescr, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(2) IsNot Nothing, ItmFlds(1), "")
                    SrchText = IIf(ItmFlds(0) IsNot Nothing, ItmFlds(0), strGridSrchString)
                Case 3
                    If cmbVoucherTp.Text = "TO" Then
                        grdVoucher.Item(ConstSerialNo, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(2) IsNot Nothing, ItmFlds(2), "")
                        'grdVoucher.Item(ConstBarcode, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(1) IsNot Nothing, ItmFlds(1), "")
                        grdVoucher.Item(ConstManufacturingdate, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(3) IsNot Nothing, ItmFlds(3), "")
                        grdVoucher.Item(ConstWarrentyExpiry, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(4) IsNot Nothing, ItmFlds(4), "")
                        grdVoucher.Item(ConstBatchQty, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(1) IsNot Nothing, ItmFlds(1), "")
                        grdVoucher.Item(ConstMRP, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(5) IsNot Nothing, ItmFlds(5), "")
                        grdVoucher.Item(ConstBatchCost, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(9) IsNot Nothing, ItmFlds(9), "")
                        grdVoucher.Item(ConstActualPrice, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(9) IsNot Nothing, ItmFlds(9), "")
                        grdVoucher.Item(ConstUPrice, grdVoucher.CurrentCell.RowIndex).Value = Format(CDbl(grdVoucher.Item(ConstActualPrice, grdVoucher.CurrentCell.RowIndex).Value), numFormat)
                        SrchText = IIf(ItmFlds(2) IsNot Nothing, ItmFlds(2), strGridSrchString)
                        calculate(, True)
                    End If

            End Select
nxt:
            chgbyprg = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub grdVoucher_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.Leave
        activecontrolname = ""
    End Sub

    Private Sub grdVoucher_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.RowEnter
        If chgbyprg Then Exit Sub
        If Not fshowlocationqty Is Nothing Then
            If fshowlocationqty.Visible = True Then
                showlocationwise(e.RowIndex)
            End If
        End If
       
    End Sub

    Private Sub RemoveRow()
        If grdVoucher.RowCount > 0 Then
            If MsgBox("You Are Going to Remove the Row." & vbCrLf & "Are You Sure ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then
                Exit Sub
            End If
            With grdVoucher
                deleteDtSerialNo(_objcmnbLayer.dtSerialNo, grdVoucher.Item(ConstSerialNo, grdVoucher.CurrentRow.Index).Value, Val(.Item(ConstItemID, .CurrentRow.Index).Value))
                .Rows.RemoveAt(.CurrentRow.Index)
                .ClearSelection()
                '.Rows(.CurrentRow.Index).Selected = True
                calculate()
            End With
            reArrangeNo()
        End If

    End Sub
    Private Sub reArrangeNo()
        Dim r As Integer
        Dim i As Integer
        chgbyprg = True
        i = 0
        With grdVoucher
            For r = 0 To .Rows.Count - 1 '- 1
                If CStr(.Item(ConstSlNo, r).Value) <> "M" And CStr(.Item(ConstSlNo, r).Value) <> "L" Then
                    i = i + 1
                    .Item(ConstSlNo, r).Value = i
                End If
            Next r
        End With
        chgbyprg = False
    End Sub
    Private Sub doCommandStat(ByVal stat As Boolean)
        If chgbyprg Then Exit Sub
        ChgId = stat
        chgPost = stat
        '----------------------do later for temp entry --------------------
        'cmdHold.Enabled = stat
        'cmdSave.Enabled = stat
        '------------------------------------------------------------------
    End Sub


    Private Sub ClearControls()
        If ChgId Then
            'If chkAuto.Checked = True And isModi = False And cmdShow.Enabled Then SaveTmpDb()
        End If
        On Error Resume Next
        'fMainForm.lblUser.Text = ""
        'fMainForm.lblModiUser.Text = ""
        'pnlOthCost.Visible = False
        Locked = False
        LddTmpSave = 0
        Dim i As Integer
        grdVoucher.RowCount = 0
        'For i = 0 To 10
        '    AddRow(True)
        'Next
        grdVoucher.CurrentCell = grdVoucher.Item(1, 0)
        activecontrolname = ""
        'lstRow = 0
        chgbyprg = True
        txtReference.Text = ""
        txtDescr.Text = ""
        txtSuppAlias.Text = ""
        txtSuppAlias.Tag = ""
        txtSuppName.Text = ""
        txtJob.Text = ""
        txtJob.Tag = ""
        chgDoc = False
        'fMainForm.lblUser.Text = CurrentUser
        'fMainForm.lblModiUser.Text = ""
        OthCost = 0
        numDisc.Text = Format(0, lnumformat)
        OthCost = 0
        CTVol = 0
        CTWt = 0
        CTQty = 0
        loadedTrId = 0
        LddImpDocs = ""
        FrTrId = 0
        isImport = False
        'setImport(True)

        'setEmptyDoList()
        'btnAddRec.Enabled = True
        'btnAddRec.Tag = ""
        'btnRemoveRec.Enabled = True
        'btnInsert.Enabled = True
        doCommandStat(False)
        'cmdLdSvd.Enabled = False
        'cmbSvdInv.Tag = ""
        'btnUpdate.Enabled = False
        ReDim LPos(0)
        numVchrNo.ReadOnly = True
        txtjobname.Text = ""
        chgPost = False
        chgbyprg = False
        numVchrNo.Focus()
        lbladd1.Text = ""
        lbladd2.Text = ""
        lbladd3.Text = ""
        lbladd4.Text = ""
        lbladd5.Text = ""
        lbladd6.Text = ""
        lbladd7.Text = ""
        lbltrdate.Text = ""

        grdOtherCost.Rows.Clear()
        lblOthCost.Text = Format(0, numFormat)
        cmbfc.Text = ""
        txtfcrt.Text = Format(1, numFormat)
        clearOtherCostFileds()
        calculate()
        If cmblocation.Text = "" Then
            cmblocation.Text = Dloc
        End If
        On Error GoTo 0
    End Sub
    Private Sub enableCtrls(ByVal Eyn As Boolean)
        grdVoucher.ReadOnly = Eyn
        chgbyprg = True
        txtReference.ReadOnly = Eyn
        txtDescr.ReadOnly = Eyn
        txtSuppAlias.ReadOnly = Eyn
        txtSuppName.ReadOnly = Eyn
        txtJob.ReadOnly = Eyn
        numDisc.ReadOnly = Eyn
        chgbyprg = False
    End Sub

    Private Sub btnModify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModify.Click
        If btnModify.Text = "&Modify" Then
            If chgbyprg Then Exit Sub
            If ChgId Then
                Select Case MsgBox("Changes found !!  Do you want hold changes ?", vbYesNoCancel + vbQuestion)
                    Case vbYes
                        'Hold procedure
                    Case vbNo
                    Case Else
                        Exit Sub
                End Select
            End If
            isModi = True
            ClearControls()
            numVchrNo.ReadOnly = False
            numVchrNo.Focus()
            btnNext.Visible = False
            btnSlct.Visible = True
            btnModify.Text = "&Undo"
            btndelete.Text = "Delete"
            'cmbVoucherTp_SelectedIndexChanged(cmbVoucherTp, New System.EventArgs)
            cmbVoucherTp.Enabled = False
        Else
            AddNewClick()
            cmbVoucherTp_SelectedIndexChanged(cmbVoucherTp, New System.EventArgs)

        End If
    End Sub



    Private Sub Verify()
        Dim _vAcMaster As DataTable
        clsreader()
        clsCnnection()
        If isModi Then
            numVchrNo.Text = numVchrNo.Tag
            If chgPost = False Then
                MsgBox("Changes not found !!", vbExclamation)
                numVchrNo.Focus()
                Exit Sub
            Else
                If loadedTrId = 0 Then
                    MsgBox("Voucher not yet loaded !!", vbExclamation)
                    numVchrNo.Focus()
                    Exit Sub
                End If
            End If
        End If
        calculate()
        'CalculateGST()
        If Val(numVchrNo.Text) < 1 Then
            MsgBox("Invalid Voucher Number !!", vbExclamation)
            MyActiveControl = numVchrNo
            Exit Sub
        End If
        If Not IsDate(cldrdate.Value) Then
            MsgBox("Invalid Voucher Date !!", vbExclamation)
            cldrdate.Focus()
            Exit Sub
        End If
        If txtReference.Text = "" Then
            'MsgBox("Invalid Reference", MsgBoxStyle.Exclamation)
            'txtReference.Focus()
            'Exit Sub
            txtReference.Text = txtprefix.Text & Val(numVchrNo.Text)
        End If
        If DateValue(cldrdate.Value) <= ProtectUntil Then
            MsgBox("Voucher Date comes within Protected Date Range !!", MsgBoxStyle.Exclamation)
            cldrdate.Focus()
            Exit Sub
        End If
        _vAcMaster = _objcmnbLayer._fldDatatable(StrAccMastSrch & " WHERE Alias = '" & txtSuppAlias.Text & "' ORDER BY AccDescr")
        If _vAcMaster.Rows.Count = 0 Then
            MsgBox("Enter a valid  Account !!", vbExclamation)
            txtSuppAlias.Focus()
            'txtSuppAlias.Focus()
            Exit Sub
        Else
            txtSuppAlias.Tag = _vAcMaster(0)("accid")
        End If
        'If Not isModi Then
        '    If chekDuplicate() Then Exit Sub
        'End If
        'Dim dt As DataTable
        'dt = _objcmnbLayer._fldDatatable("SELECT Trid from ItmInvcmntb where TrType='" & cmbVoucherTp.Text & "' AND cscode=" & Val(txtSuppAlias.Tag) & " and TrRefNo='" & txtReference.Text & "' and Trid<>" & loadedTrId)
        'If dt.Rows.Count > 0 Then
        '    MsgBox("Reference already exist!", MsgBoxStyle.Exclamation)
        '    txtReference.Focus()
        '    Exit Sub
        'End If
        'If CheckRestrictImport() = False Then Exit Sub
        If Not chkGridvalue() Then Exit Sub
        If CDbl(lblNetAmt.Text) < 0 Then
            MsgBox("Net Amount below Zero is not allowed !!!?", vbExclamation)
            MyActiveControl = numDisc
            Exit Sub
        End If
        If enableBranch And UsrBr = "" Then
            MsgBox("Transaction cannot be saved without Branch! Please login with Branch", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If enableBranch Then
            If chkforlocation.Checked And cmbtolocation.Text = "" Then
                MsgBox("Please select location before transfer Stock", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
        End If
        If MsgBox("Verification is succeeded. Do you like to file it ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = vbYes Then
            'saveTrans()
            loadWaite(1)
        End If
    End Sub

    Private Sub doStockUpdate(ByVal InvNo As Long, ByVal DocId As Long)
        Dim dtInvCmnTb As DataTable
        'Dim dtsRs As DataTable
        Dim TrId As Long
        Dim i As Integer
        Dim PPerU As Single
        Dim Rfrsh As Boolean
        dtInvCmnTb = _objcmnbLayer._fldDatatable("SELECT * FROM ItmInvCmnTb WHERE TrType = '" & IIf(isCust, "DC", "DS") & "' AND InvNo = " & InvNo)
        If dtInvCmnTb.Rows.Count > 0 Then
            Rfrsh = True
            '---------------------------------------------------------------------------
            'If Not BkProcess And StkUpdtd Then UpdtQty(dtInvCmnTb(0)("TrId"), True, IIf(isCust, "IS", cmbVoucherTp.Text))
            '---------------------------------------------------------------------------
            _objcmnbLayer._saveDatawithOutParm("DELETE FROM ItmInvTrTb WHERE TrId = " & dtInvCmnTb(0)("TrId"))
            TrId = dtInvCmnTb(0)("TrId")
            'If chkStkUpdtd.Checked = True Then
            '    'ItmInvCmnTb.Edit()
            'Else
            '    _objcmnbLayer._saveDatawithOutParm("DELETE FROM ItmInvCmnTb WHERE TrId = " & dtInvCmnTb(0)("TrId"))
            'End If
        Else
            'If chkStkUpdtd.Checked = True Then
            '    Rfrsh = True
            'End If
            TrId = 0
        End If
        'If chkStkUpdtd.Checked = False Then GoTo Ter
        setInvCmnValue(TrId)
        TrId = Val(_objInv._saveCmn())
        With grdVoucher
            For i = 0 To .Rows.Count - 1 '- 1
                If Val(.Item(ConstItemID, i).Value) > 0 Then
                    If CDbl(.Item(ConstQty, i).Value) <> 0 Or .Item(ConstSlNo, i).Value.ToString = "M" Then
                        PPerU = Val(.Item(ConstPMult, i).Value) 'Switch(.TextMatrix(i, 4) = "B", 1, .TextMatrix(i, 4) = "P1", CSng(grdVoucher.TextMatrix(i, 18)), .TextMatrix(i, 4) = "P2", CSng(grdVoucher.TextMatrix(i, 19)))
                        PPerU = IIf(PPerU = 0, 1, PPerU)
                        setInvDetValue(TrId, PPerU, i)
                        _objInv._saveDetails()
                        '-------------------------------------------------------------------------
                        'If IIf(cmbDos.Items.Count = 0 And txtDOLst.Visible And txtDOLst.Text <> "", IIf(chkStkUpdtd.Checked = True, False, Val(.Item(ConstImpDocId, i).Value) <> 0), True) And .Item(ConstSlNo, i).Value.ToString <> "L" Then UpdtQty(TrId, False, IIf(isCust, "IS", cmbVoucherTp.Text), Val(.Item(ConstActualPrice, i).Value) * FCRt / PPerU, IIf(.Item(ConstSlNo, i).Value.ToString <> "L", Val(.Item(ConstBaseID, i).Value), 0), CDbl(.Item(ConstQty, i).Value) * PPerU, CDbl(.Item(ConstMtrPqty, i).Value))
                        '-------------------------------------------------------------------------
                        'RfrshSingle(Val(.Item(ConstBaseID, i).Value))
                        'UpdtLastSP(Val(txtSuppAlias.Tag), IIf(.Item(ConstSlNo, i).Value.ToString <> "L", Val(.Item(ConstBaseID, i).Value), 0), IIf(.Item(ConstSlNo, i).Value.ToString <> "L", Val(.Item(ConstItemID, i).Value), 0), 16, Trim(.Item(ConstDescr, i).Value), Val(.Item(ConstActualPrice, i).Value) * FCRt, 0, PPerU, PPerU, CDate(dtVrDate.Value))
                        '--------------------------------------do later if Item Master Requires PurchInfo-----------------------------------
                        'If isModi = False And Trim(.TextMatrix(i, 13)) <> "" Then
                        '    conMyADODB.Execute("UPDATE BaseItmDet SET LstPurchInf3 = LstPurchInf2, LstPurchInf2 = LstPurchInf1, LstPurchInf1 = '" & Left(MkDbSrchStr(Trim(.TextMatrix(i, 13)), True) & " (" & cmnRec(8).FormattedText & ")", 40) & "' WHERE BaseItemId = " & Val(.TextMatrix(i, .Cols - 1)))
                        'End If
                        '-------------------------------------------------------------------------------------------------------------------------
                    End If
                End If

            Next i
        End With
Ter:
        'If ItmsStr <> "" Then
        '    If Right(ItmsStr, 1) = "," Then ItmsStr = Mid(ItmsStr, 2, Len(ItmsStr) - 2)
        '    'RefreshQty ItmsStr
        '    'If RfrshAPorS And Rfrsh = True Then doRefreshCost False, ItmsStr, True
        '    WorkingDb.Execute("INSERT INTO RfrshRequestTb (IsCostAcc, ItemStr, Method, shortDtFmt, TrTypeNo, TrId, RfrshAPorS) VALUES (" & EnaCostAcc & ", '" & _
        '       ItmsStr & "'," & CostMthd + 1 & ", '" & shortDtFmt & "', " & IIf(isCust, 10, 1) & "," & IIf(isModi, 0, TrId) & "," & RfrshAPorS & ")")
        'End If
    End Sub

    Private Sub saveTrans()
        Dim TrId As Long
        Dim DiscAcc As Long
        Dim TDrAmt As Double
        Dim dtTable As DataTable
        clsreader()
        clsCnnection()
        If Val(txtfcrt.Text) = 0 Then txtfcrt.Text = 0
        FCRt = CDbl(txtfcrt.Text)
        If FCRt = 0 Then
            FCRt = 1
        End If
        If Not isModi Then
chkagain:
            If Not CheckNoExists(txtprefix.Text, Val(numVchrNo.Text), cmbVoucherTp.Text, "Inventory") Then
                If MsgBox("Document No alreary exist! Do you want to fill next?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Exit Sub
                Else
                    numVchrNo.Text = Val(numVchrNo.Text) + 1
                    GoTo chkagain
                End If
            End If
        End If
        'dtTable = _objcmnbLayer._fldDatatable("SELECT accid FROM AccMast WHERE AccSetId Like '%02%'")
        'If dtTable.Rows.Count > 0 Then DiscAcc = dtTable(0)("accid")
        'If dtTable.Rows.Count > 0 Then dtTable.Clear()
        If isModi Then
            dtTable = _objcmnbLayer._fldDatatable("SELECT TOP 1 * FROM ItmInvCmnTb WHERE TrId = " & loadedTrId)
            If dtTable.Rows.Count = 0 Then
                MsgBox("The loaded voucher not found.  It may removed externally.", vbExclamation)
                Exit Sub
            End If
            TrId = loadedTrId
        End If
        setInvCmnValue(TrId)
        TrId = Val(_objInv._saveCmn())
        If FrTrId > 0 Then
            If cmbVoucherTp.Text = "TI" Then
                _objcmnbLayer._saveDatawithOutParm("update ItmInvCmnTb set locTransferInTOTI=" & IIf(chkforlocation.Checked, 1, 0) & ", isimportedtoTI=" & TrId & " where trid=" & FrTrId)
            Else
                _objcmnbLayer._saveDatawithOutParm("update ItmInvCmnTb set locTransferInTOTI=" & IIf(chkforlocation.Checked, 1, 0) & ",isimportedtoTI=" & FrTrId & " where trid=" & TrId)
            End If
        Else
            _objcmnbLayer._saveDatawithOutParm("update ItmInvCmnTb set locTransferInTOTI=" & IIf(chkforlocation.Checked, 1, 0) & ",DefStkOutBr='" & cmbtolocation.Text & "' where trid=" & TrId)
        End If
        Dim itemidsdatatable As New DataTable
        itemidsdatatable.Columns.Add(New DataColumn("Itemid", GetType(Long)))
        TDrAmt = saveInvTr(TrId)

        'If isModi Then
        '    _objInv.deleteInventoryRelatedItemDetails(loadedTrId)
        'End If
        UpdateAccounts(TrId, TDrAmt, DiscAcc)
        'If Trim(LddImpDocs) <> "" Then RfrshPrssdQty(LddImpDocs)
        If isModi = False Then
            numPrintVchr.Text = numVchrNo.Text
            numVchrNo.Tag = Val(numVchrNo.Text) 'Trim(CMNREC(12).FormattedText)
            txtPPrefix.Text = txtprefix.Text
            txtprefix.Tag = txtprefix.Text
            txtprefix.Text = SetNextVrNo(numVchrNo, Val(cmbVoucherTp.Tag), cmbVoucherTp.Text, "TrType = '" & cmbVoucherTp.Text & "' AND InvNo = ", False, True, True)
        End If
        ChgId = False
        chgPost = False
        If isModi Then
            btnModify_Click(btnModify, New System.EventArgs)
        Else
            AddNewClick()
            cmbVoucherTp_SelectedIndexChanged(cmbVoucherTp, New System.EventArgs)
        End If
        If Not fwait Is Nothing Then fwait.Close() : fwait = Nothing
        MsgBox("Voucher succesfully posted with Vr. # " & numPrintVchr.Text & ".", MsgBoxStyle.Information)
        numVchrNo.Tag = ""
    End Sub
    Private Function saveInvTr(ByVal Invid As Long) As Double
        If dtInvTb Is Nothing Then
            dtInvTb = _objcmnbLayer._fldDatatable("Select top 1 * from ItmInvTrTb")
        End If
        Dim dtrow As DataRow
        dtInvTb.Rows.Clear()
        Dim i As Integer
        Dim PPerU As Single
        Dim TDrAmt As Double
        Dim ImpJobChildTbIDs As String = ""
        With grdVoucher
            For i = 0 To .Rows.Count - 1
                If .Item(ConstSlNo, i).Value.ToString <> "M" And Val(.Item(ConstItemID, i).Value) = 0 Then GoTo nxtM
                PPerU = Val(.Item(ConstPMult, i).Value) 'Switch(.TextMatrix(i, 4) = "B", 1, .TextMatrix(i, 4) = "P1", CSng(grdVoucher.TextMatrix(i, 18)), .TextMatrix(i, 4) = "P2", CSng(grdVoucher.TextMatrix(i, 19)))
                PPerU = IIf(PPerU = 0, 1, PPerU)

                dtrow = dtInvTb.NewRow
                dtrow("TrId") = Invid
                dtrow("ItemId") = IIf(.Item(ConstSlNo, i).Value.ToString <> "M", Val(.Item(ConstItemID, i).Value), 0)
                dtrow("Unit") = .Item(ConstUnit, i).Value
                dtrow("TrQty") = CDbl(.Item(ConstQty, i).Value) * PPerU
                dtrow("Focqty") = CDbl(.Item(ConstFocQty, i).Value) * PPerU
                dtrow("UnitCost") = CDbl(.Item(ConstActualPrice, i).Value) * FCRt / PPerU 'CDbl(.TextMatrix(i, 10)) / PPerU

                TDrAmt = TDrAmt + CDbl(.Item(ConstQty, i).Value) * (CDbl(.Item(ConstActualPrice, i).Value))
                TDrAmt = TDrAmt - CDbl(.Item(ConstDisAmt, i).Value)

                dtrow("taxP") = CDbl(grdVoucher.Item(ConstTaxP, i).Value)
                dtrow("taxAmt") = (CDbl(grdVoucher.Item(ConstTaxAmt, i).Value) * FCRt)
                dtrow("PFraction") = PPerU
                If Val(.Item(ConstUnitOthCost, i).Value) = 0 Then .Item(ConstUnitOthCost, i).Value = 0
                dtrow("UnitOthCost") = CDbl(.Item(ConstUnitOthCost, i).Value) * FCRt / PPerU
                dtrow("Method") = .Item(ConstB, i).Value
                dtrow("UnitDiscount") = Val(.Item(ConstDiscOther, i).Value) * FCRt / PPerU
                If Val(.Item(ConstDisAmt, i).Value & "") = 0 Then .Item(ConstDisAmt, i).Value = 0
                dtrow("ItemDiscount") = CDbl(.Item(ConstDisAmt, i).Value) * FCRt / PPerU
                If Val(.Item(ConstDisP, i).Value) = 0 Then .Item(ConstDisP, i).Value = 0
                dtrow("DisP") = CDbl(.Item(ConstDisP, i).Value) * FCRt / PPerU

                If Not IsDBNull(.Item(ConstDescr, i).Value) Then
                    dtrow("IDescription") = IIf(.Item(ConstSlNo, i).Value.ToString = "M", .Item(ConstDescr, i).Value, Trim(.Item(ConstDescr, i).Value))
                End If
                dtrow("SlNo") = i + 1
                dtrow("TrTypeNo") = TrTypeNo ' getVouchernumber("IS")
                dtrow("TrDateNo") = getDateNo(CDate(cldrdate.Value))
                dtrow("id") = Val(.Item(ConstId, i).Value)
                'dtrow("WarrentyName") = .Item(ConstLocation, i).Value
                If Trim(.Item(ConstSerialNo, i).Value & "") = "" And enableBatchwiseTr Then
                    dtrow("SerialNo") = "IP" & txtprefix.Text & numVchrNo.Text
                Else
                    dtrow("SerialNo") = Trim(.Item(ConstSerialNo, i).Value & "")
                End If

                If IsDate(.Item(ConstWarrentyExpiry, i).Value) Then
                    dtrow("WarrentyExpDate") = DateValue(.Item(ConstWarrentyExpiry, i).Value)
                Else
                    dtrow("WarrentyExpDate") = DateValue("01/01/1950")
                End If
                dtrow("HSNCode") = Trim(.Item(ConstBarcode, i).Value & "")
                If Val(.Item(ConstCGSTP, i).Value & "") = 0 Then .Item(ConstCGSTP, i).Value = 0
                dtrow("CSGTP") = CDbl(.Item(ConstCGSTP, i).Value)
                If Val(.Item(ConstSGSTP, i).Value & "") = 0 Then .Item(ConstSGSTP, i).Value = 0
                dtrow("SGSTP") = CDbl(.Item(ConstSGSTP, i).Value)
                If Val(.Item(ConstIGSTP, i).Value & "") = 0 Then .Item(ConstIGSTP, i).Value = 0
                dtrow("IGSTP") = CDbl(.Item(ConstIGSTP, i).Value)
                .Item(ConstCGSTAmt, i).Value = 0
                .Item(ConstSGSTAmt, i).Value = 0
                .Item(ConstIGSTAmt, i).Value = 0
                .Item(ConstregularCessAmt, i).Value = 0
                .Item(ConstFloodCessAmt, i).Value = 0
                .Item(ConstAdditionalcess, i).Value = 0
                .Item(ConstcessAmt, i).Value = 0
                dtrow("IGSTAmt") = CDbl(.Item(ConstIGSTAmt, i).Value) * FCRt
                dtrow("CGSTAMT") = CDbl(.Item(ConstCGSTAmt, i).Value) * FCRt
                dtrow("SGSTAmt") = CDbl(.Item(ConstSGSTAmt, i).Value) * FCRt
                dtrow("regularcessAmt") = (CDbl(.Item(ConstregularCessAmt, i).Value) * FCRt)
                dtrow("FloodcessAmt") = 0

                dtrow("AdditionalcessAmt") = ((CDbl(.Item(ConstAdditionalcess, i).Value) * CDbl(.Item(ConstQty, i).Value)) * FCRt)
                dtrow("CessAmt") = (CDbl(grdVoucher.Item(ConstcessAmt, i).Value) * FCRt)
                dtrow("Smancode") = Trim(.Item(Constsman, i).Value & "")
                dtrow("impDocid") = Val(.Item(ConstImpDocId, i).Value & "")
                dtrow("impDocSlno") = Val(.Item(ConstImpLnId, i).Value & "")

                If IsDate(.Item(ConstManufacturingdate, i).Value) Then
                    dtrow("manufacturingdate") = DateValue(.Item(ConstManufacturingdate, i).Value)
                Else
                    dtrow("manufacturingdate") = DateValue("01/01/1950")
                End If
                If Val(.Item(ConstMRP, i).Value & "") = 0 Then .Item(ConstMRP, i).Value = 0
                dtrow("MRP") = CDbl(.Item(ConstMRP, i).Value)
                If Val(.Item(ConstSP1, i).Value & "") = 0 Then .Item(ConstSP1, i).Value = 0
                dtrow("SP1") = CDbl(.Item(ConstSP1, i).Value)
                If Val(.Item(ConstSP2, i).Value & "") = 0 Then .Item(ConstSP2, i).Value = 0
                dtrow("SP2") = CDbl(.Item(ConstSP2, i).Value)
                If Val(.Item(ConstSP3, i).Value & "") = 0 Then .Item(ConstSP3, i).Value = 0
                dtrow("SP3") = CDbl(.Item(ConstSP3, i).Value)
                If Val(.Item(ConstBatchCost, i).Value & "") = 0 Then .Item(ConstBatchCost, i).Value = 0
                dtrow("CostAvg") = CDbl(.Item(ConstBatchCost, i).Value)
                If Val(dtrow("ItemId")) = 0 Then
                    dtrow("TrQty") = 1
                    dtrow("UnitCost") = 1
                    dtrow("taxP") = 1
                    dtrow("taxAmt") = 1
                    dtrow("UnitDiscount") = 0
                End If
                dtInvTb.Rows.Add(dtrow)
                Dim j As Integer
                Dim dtype As String
                For j = 0 To dtInvTb.Columns.Count - 1
                    If dtInvTb.Columns(j).ColumnName = "id" Then GoTo nxt
                    dtype = dtInvTb.Columns(j).DataType.Name
                    If Trim(dtInvTb(i)(j) & "") = "" Then
                        Select Case dtype
                            Case "String"
                                dtInvTb(i)(j) = ""
                            Case "Int64", "Int32", "Double", "Decimal", "Boolean"
                                dtInvTb(i)(j) = 0
                        End Select
                    End If
nxt:
                Next
nxtM:
            Next
        End With
        _objInv.savebulktoInvTr(dtInvTb)
        Return TDrAmt
    End Function
    Private Sub updateStockTransaction(ByVal trid As Long, ByVal LinkNo As Long)
        If Not enableCostAccounting Then Exit Sub
        Dim stockAc As Long
        Dim costDiffAc As Long
        Dim dt As DataTable
        Dim costAmt As Double
        stockAc = getConstantAccounts(1)
        costDiffAc = getConstantAccounts(10)
        If stockAc = 0 Or costDiffAc = 0 Then Exit Sub
        dt = _objcmnbLayer._fldDatatable("select sum((CostAvg-(UnitCost-isnull(UnitDiscount,0)))*TrQty) costAmt from ItmInvTrTb  where trid=" & trid)
        If dt.Rows.Count > 0 Then
            costAmt = dt(0)("costAmt")
        End If

        If costAmt <> 0 Then
            'debit entry [cost Difference]
            setAcctrDetValue(LinkNo, costDiffAc, Trim(txtReference.Text), Trim(txtDescr.Text), costAmt, "", "", 3, 0, "", _
                           "", Val(txtSuppAlias.Tag), costDiffAc & txtReference.Text, "", FCRt)
            _objTr.saveAccTrans()
            'UpdtClosBal(costDiffAc, costAmt)
            'credit entry [stock in hand]
            costAmt = costAmt * -1
            setAcctrDetValue(LinkNo, stockAc, Trim(txtReference.Text), "COST DIFFERENCE FROM-" & Trim(txtReference.Text), costAmt, "", "", 3, 0, "", _
                           "", Val(txtSuppAlias.Tag), stockAc & txtReference.Text, "", FCRt)
            _objTr.saveAccTrans()
            'UpdtClosBal(stockAc, costAmt)
        End If
    End Sub
    Private Sub UpdateAccounts(ByVal TrId As Long, ByVal TDrAmt As Double, ByVal DiscAcc As Integer)
        Dim LinkNo As Long
        Dim dtTable As DataTable
        If isModi And TrId > 0 Then
            dtTable = _objcmnbLayer._fldDatatable("SELECT LinkNo FROM AccTrCmn WHERE LnkNo = " & TrId)
            If dtTable.Rows.Count > 0 Then
                LinkNo = dtTable(0)("LinkNo")
                _objcmnbLayer._saveDatawithOutParm("DELETE FROM AccTrDet WHERE LinkNo=" & LinkNo)
            End If
        End If

        setAcctrCmnValue(TrId, LinkNo)
        If dtAccTb Is Nothing Then
            dtAccTb = _objcmnbLayer._fldDatatable("Select top 1 LinkNo,AccountNo,DueDate,Reference,EntryRef,DealAmt,FCAmt,CurrencyCode,CurrRate, " & _
                                                  "JobCode,PDCAcc,BankCode,LPONo,OthCost,TrInf,TermsId,CustAcc,AccWithRef,ChqDate,ChqNo,SuppInvDate," & _
                                                  "vatid,isvatEntry,UnqNo from AccTrDet")
        End If
        If dtAccTb.Rows.Count > 0 Then
            dtAccTb.Rows.Clear()
        End If
        Dim EntRef As String = Strings.Left(Trim(txtDescr.Text) & IIf(txtDescr.Text = "", " / ", "") & "Stock " & cmbVoucherTp.Text & " /" & txtSuppName.Text, 249)

        setAcctrDetValue(LinkNo, Val(txtPurchaseName.Tag), Trim(txtReference.Text), EntRef, CDbl(lblNetAmt.Text) * FCRt, txtJob.Text, "", 0, 0, "", _
                             "", Val(txtSuppAlias.Tag), "", cmbfc.Text, FCRt)


        EntRef = Strings.Left(Trim(txtDescr.Text) & IIf(Trim(txtDescr.Text) = "", "", " / ") & txtPurchaseName.Text, 249)
        Dim dlAmt As Double = CDbl(lblNetAmt.Text) * FCRt
        'Credit Entry
        dlAmt = dlAmt * -1
        setAcctrDetValue(LinkNo, Val(txtSuppAlias.Tag), Trim(txtReference.Text), EntRef, dlAmt, "", Strings.Left(txtJob.Tag, 50), 0, 0, "", _
                         "", Val(txtPurchaseName.Tag), Val(txtSuppAlias.Tag) & txtReference.Text, "", FCRt)
        '_objTr.saveAccTrans()
        'updateStockTransaction(TrId, LinkNo)
        'updateClosingBalanceForInvoice(TrId)
        _objTr.SaveAccTrWithDt(dtAccTb)
    End Sub
    Private Sub setInvCmnValue(ByVal InvTrid As Long)
        With _objInv
            Dt = DateValue(Date.Now)
            .TrId = InvTrid
            .TrDate = DateValue(cldrdate.Value)
            .TrType = cmbVoucherTp.Text
            .DocLstTxt = ""
            .Prefix = Trim(txtprefix.Text)
            .InvNo = Val(numVchrNo.Text)
            .TrRefNo = Trim(txtReference.Text)
            .CSCode = Val(txtSuppAlias.Tag)
            .PSAcc = Val(txtPurchaseName.Tag)
            .JobCode = txtJob.Text
            .ImpDoc = 0
            .UserId = CurrentUser
            .IsFC = 0
            .FCRate = FCRt
            .NFraction = NDec
            .FC = cmbfc.Text
            .Discount = CDbl(numDisc.Text) * FCRt
            .TrDescription = Trim(txtDescr.Text)
            .TypeNo = TrTypeNo ' getVouchernumber(cmbVoucherTp.Text)
            .EnaJob = False
            .DocDefLoc = IIf(cmblocation.Text = "", Dloc, cmblocation.Text)
            .BrId = UsrBr
            .OthCost = CDbl(lblOthCost.Text)
            .Discount1 = 0
            .NetAmt = CDbl(lblNetAmt.Text) * FCRt
            .LPO = ""
            'Dim dt As Date = getServerDate()
            .CrtDt = Dt
            .DelDate = Dt
            .DueDate = Dt
            .DocDate = Dt
            .SuppInvDate = Dt
            .TermsId = ""
            .MchName = MACHINENAME
            .isModi = isModi
            .lpoclass = ""
            .rndoff = 0
            .isTaxInvoice = chkTaxbill.Checked
        End With

    End Sub
    Private Sub setInvDetValue(ByVal Invid As Long, ByVal PPerU As Single, ByVal i As Integer)
        With grdVoucher
            _objInv.dtTrId = Invid
            _objInv.ItemId = IIf(.Item(ConstSlNo, i).Value.ToString <> "L", Val(.Item(ConstItemID, i).Value), 0)
            _objInv.Unit = .Item(ConstUnit, i).Value
            _objInv.taxP = CDbl(.Item(ConstTaxP, i).Value)
            _objInv.taxAmt = CDbl(.Item(ConstTaxAmt, i).Value) * FCRt
            _objInv.TrQty = CDbl(.Item(ConstQty, i).Value) * PPerU
            _objInv.UnitCost = CDbl(.Item(ConstActualPrice, i).Value) * FCRt / PPerU 'CDbl(.TextMatrix(i, 10)) / PPerU
            _objInv.PFraction = PPerU
            If Val(.Item(ConstUnitOthCost, i).Value) = 0 Then .Item(ConstUnitOthCost, i).Value = 0
            _objInv.UnitOthCost = CDbl(.Item(ConstUnitOthCost, i).Value) * FCRt / PPerU
            _objInv.Method = .Item(ConstB, i).Value
            _objInv.UnitDiscount = Val(.Item(ConstDiscOther, i).Value) * FCRt / PPerU

            If Val(.Item(ConstDisAmt, i).Value & "") = 0 Then .Item(ConstDisAmt, i).Value = 0
            _objInv.ItemDiscount = CDbl(.Item(ConstDisAmt, i).Value) * FCRt / PPerU
            If Val(.Item(ConstDisP, i).Value) = 0 Then .Item(ConstDisP, i).Value = 0
            _objInv.DisP = CDbl(.Item(ConstDisP, i).Value) * FCRt / PPerU

            If Not IsDBNull(.Item(ConstDescr, i).Value) Then
                _objInv.IDescription = IIf(.Item(ConstSlNo, i).Value.ToString = "M", .Item(ConstDescr, i).Value, Trim(.Item(ConstDescr, i).Value))
            End If
            _objInv.SlNo = i + 1
            _objInv.TrTypeNo = getVouchernumber(cmbVoucherTp.Text)
            _objInv.TrDateNo = getDateNo(CDate(cldrdate.Value))
            _objInv.TrType = cmbVoucherTp.Text
            _objInv.id = Val(.Item(ConstId, i).Value)
            _objInv.WarrentyName = .Item(ConstLocation, i).Value
            If Trim(.Item(ConstSerialNo, i).Value & "") = "" And enableBatchwiseTr Then
                _objInv.SerialNo = "TI" & txtprefix.Text & numVchrNo.Text
            Else
                _objInv.SerialNo = Trim(.Item(ConstSerialNo, i).Value & "")
            End If
            _objInv.HSNCode = Trim(.Item(ConstBarcode, i).Value & "")
            If Val(.Item(ConstCGSTP, i).Value & "") = 0 Then .Item(ConstCGSTP, i).Value = 0
            _objInv.CSGTP = CDbl(.Item(ConstCGSTP, i).Value)
            If Val(.Item(ConstCGSTAmt, i).Value & "") = 0 Then .Item(ConstCGSTAmt, i).Value = 0
            _objInv.CGSTAMT = CDbl(.Item(ConstCGSTAmt, i).Value)
            If Val(.Item(ConstSGSTP, i).Value & "") = 0 Then .Item(ConstSGSTP, i).Value = 0
            _objInv.SGSTP = CDbl(.Item(ConstSGSTP, i).Value)
            If Val(.Item(ConstSGSTAmt, i).Value & "") = 0 Then .Item(ConstSGSTAmt, i).Value = 0
            _objInv.SGSTAmt = CDbl(.Item(ConstSGSTAmt, i).Value)
            If Val(.Item(ConstIGSTP, i).Value & "") = 0 Then .Item(ConstIGSTP, i).Value = 0
            _objInv.IGSTP = CDbl(.Item(ConstIGSTP, i).Value)
            If Val(.Item(ConstIGSTAmt, i).Value & "") = 0 Then .Item(ConstIGSTAmt, i).Value = 0
            _objInv.IGSTAmt = CDbl(.Item(ConstIGSTAmt, i).Value)

            If IsDate(.Item(ConstWarrentyExpiry, i).Value) Then
                _objInv.WarrentyExpDate = DateValue(.Item(ConstWarrentyExpiry, i).Value)
            Else
                _objInv.WarrentyExpDate = DateValue("01/01/1950")
            End If
            If Val(.Item(ConstWoodQty, i).Value) = 0 Then .Item(ConstWoodQty, i).Value = 0
            If Val(.Item(ConstWoodDiscQty, i).Value) = 0 Then .Item(ConstWoodDiscQty, i).Value = 0
            _objInv.WoodNetQty = CDbl(.Item(ConstWoodQty, i).Value) * PPerU
            _objInv.WoodDiscountQty = CDbl(.Item(ConstWoodDiscQty, i).Value) * PPerU
            If IsDate(.Item(ConstManufacturingdate, i).Value) Then
                _objInv.manufacturingdate = DateValue(.Item(ConstManufacturingdate, i).Value)
            Else
                _objInv.manufacturingdate = DateValue("01/01/1950")
            End If
            If Val(.Item(ConstMRP, i).Value & "") = 0 Then .Item(ConstMRP, i).Value = 0
            _objInv.MRP = CDbl(.Item(ConstMRP, i).Value)
            If Val(.Item(ConstSP1, i).Value & "") = 0 Then .Item(ConstSP1, i).Value = 0
            _objInv.SP1 = CDbl(.Item(ConstSP1, i).Value)
            If Val(.Item(ConstSP2, i).Value & "") = 0 Then .Item(ConstSP2, i).Value = 0
            _objInv.SP2 = CDbl(.Item(ConstSP2, i).Value)
            If Val(.Item(ConstSP3, i).Value & "") = 0 Then .Item(ConstSP3, i).Value = 0
            _objInv.SP3 = CDbl(.Item(ConstSP3, i).Value)

        End With
    End Sub
    
    Private Sub setAcctrCmnValue(ByVal InvTrid As Long, ByVal LinkNo As Long)
        _objTr.JVType = cmbVoucherTp.Text
        _objTr.JVDate = DateValue(cldrdate.Value)
        _objTr.PreFix = Trim(txtprefix.Text)
        _objTr.JVNum = Val(numVchrNo.Text)
        _objTr.JVTypeNo = TrTypeNo ' getVouchernumber(cmbVoucherTp.Text)
        _objTr.UserId = Trim(CurrentUser & "")
        _objTr.MchName = MACHINENAME
        _objTr.CrtDtTm = Dt
        _objTr.TypeNo = 4
        '_objTr.VrDescr = Trim(txtpayee.Text)
        _objTr.IsModi = IIf(LinkNo > 0, 1, 0)
        _objTr.LinkNo = InvTrid
    End Sub
    Private Sub setAcctrDetValue(ByVal lnkNo As Long, ByVal jbIndex As Integer)
        With _objTr
            .trLinkNo = lnkNo
            .AccountNo = JobAcc(jbIndex).Acc
            .Reference = Trim(txtReference.Text)
            .EntryRef = Strings.Left(Trim(txtDescr.Text) & IIf(JobAcc(jbIndex).Job = "", "", " / " & JobAcc(jbIndex).Job) & " / " & txtSuppName.Text, 249)
            .DealAmt = JobAcc(jbIndex).Amt * FCRt
            .FCAmt = JobAcc(jbIndex).Amt
            .JobCode = JobAcc(jbIndex).Job
            .JobStr = ""
            .CurrRate = FCRt
            .CurrencyCode = cmbfc.Text
            txtJob.Tag = txtJob.Tag & IIf(txtJob.Tag = "" Or JobAcc(jbIndex).Job = "", "", ",") & JobAcc(jbIndex).Job
            .TrInf = 0
            .OthCost = 0
            .TermsId = ""
            .CustAcc = Val(txtSuppAlias.Tag)
            .AccWithRef = JobAcc(jbIndex).Acc & txtReference.Text
            .DueDate = Dt
            .DocDate = Dt
            .SuppInvDate = Dt
        End With
    End Sub
    Private Sub setAcctrDetValue(ByVal lnkNo As Long, ByVal AccountNo As Long, ByVal Reference As String, ByVal EntryRef As String, ByVal DealAmt As Double, ByVal JobCode As String, ByVal JobStr As String, _
                                  ByVal TrInf As Short, ByVal OthCost As Short, ByVal LPO As String, ByVal TermsId As String, ByVal CustAcc As Integer, ByVal AccWithRef As String, _
                                  ByVal CurrencyCode As String, ByVal CurrRate As Double)
        Dim dtrow As DataRow
        Dim dtLPO As Date = IIf(chkDate(cldrdate.Value), cldrdate.Value, DateValue(cldrdate.Value))
        Dim dtSup As Date = DateValue(cldrdate.Value)
        dtrow = dtAccTb.NewRow
        dtrow("LinkNo") = lnkNo
        dtrow("AccountNo") = AccountNo
        dtrow("Reference") = txtReference.Text
        'dtrow("DueDate") = Format(DateValue(cldrdate.Value), "yyyy/MM/dd")
        dtrow("EntryRef") = EntryRef
        dtrow("DealAmt") = DealAmt
        dtrow("FCAmt") = DealAmt * CurrRate
        dtrow("CurrencyCode") = CurrencyCode
        dtrow("CurrRate") = CurrRate
        dtrow("TrInf") = TrInf
        dtrow("OthCost") = OthCost
        dtrow("TermsId") = TermsId
        dtrow("CustAcc") = CustAcc
        dtrow("AccWithRef") = AccWithRef
        dtrow("LPONo") = LPO
        'dtrow("SuppInvDate") = Format(DateValue(cldrdate.Value), "yyyy/MM/dd")
        dtAccTb.Rows.Add(dtrow)
        Dim j As Integer
        Dim dtype As String
        For j = 0 To dtAccTb.Columns.Count - 1
            dtype = dtAccTb.Columns(j).DataType.Name
            If Trim(dtAccTb(dtAccTb.Rows.Count - 1)(j) & "") = "" Then
                Select Case dtype
                    Case "String"
                        dtAccTb(dtAccTb.Rows.Count - 1)(j) = ""
                    Case "Int64", "Int32", "Double", "Decimal", "Boolean"
                        dtAccTb(dtAccTb.Rows.Count - 1)(j) = 0
                End Select
            End If
nxt:
        Next
    End Sub

    Private Sub LodCurrency()
        Dim dt As DataTable = _objcmnbLayer._fldDatatable("SELECT CurrencyCode FROM CurrencyTb", False)
        cmbfc.Items.Clear()
        cmbfc.Items.Add("")
        Dim i As Integer
        For i = 0 To dt.Rows.Count - 1
            cmbfc.Items.Add(dt(i)("CurrencyCode"))
        Next
    End Sub

    Private Sub StockAdjustmentFrm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
        If Not fSelect Is Nothing Then fSelect.Close() : fSelect = Nothing
        If Not fProductEnquiry Is Nothing Then fProductEnquiry.Close() : fProductEnquiry = Nothing
        If Not fSlctDoc Is Nothing Then fSlctDoc.Close() : fSlctDoc = Nothing
        If Not fSerialno Is Nothing Then fSerialno.Close() : fSerialno = Nothing
    End Sub

    Private Sub SalesOrderFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            StrAccMastSrch = "SELECT AccDescr, Alias, ClosingBal As Balance, OpnBal, accid, S1AccHd.S1AccId, AccSetId, GrpSetOn FROM" & _
                            " AccMast INNER JOIN S1AccHd ON S1AccHd.S1AccId = AccMast.S1AccId "
            lnumformat = numFormat
            'loadStockAdjFormLoadMasters()
            
            'loadInventoryFormLoadMasters(False, 0, "IP", 2, DiscAcc, TrTypeNo)
            
            'AddtoCombo(cmblocation, "SELECT LocCode FROM LocationTb", True, False)
            TabControl1.Visible = enableBranch

            _objcmnbLayer.dtSerialNo = createdtSerialNo()
            SetGridHead()
            setOtherCostHead()
            FCRt = 1
            OthCost = 0
            chgbyprg = True
            'calculate()
            Text = "Stock Adjustment "
            cldrdate.Value = Format(Date.Now, DtFormat)
            'LodCurrency()
            NDec = NoOfDecimal
            loadedTrId = 0
            chgbyprg = False
            'cmbVrType_SelectedIndexChanged(sender, e)
            'numVchrNo.Select()
            ChgId = False
            cmbVoucherTp.Items.Clear()
            If userType Then
                If getRight(93, CurrentUser) Then
                    cmbVoucherTp.Items.Add("TI")
                End If
                If getRight(97, CurrentUser) Then
                    cmbVoucherTp.Items.Add("TO")
                End If
            Else
                cmbVoucherTp.Items.Add("TI")
                cmbVoucherTp.Items.Add("TO")
            End If
            If isModi Then
                btnModify_Click(btnModify, New System.EventArgs)
                If userType Then
                    If cmbVoucherTp.Text = "TI" Then
                        btnupdate.Tag = IIf(getRight(95, CurrentUser), 1, 0)
                        btndelete.Tag = IIf(getRight(96, CurrentUser), 1, 0)
                    Else
                        btnupdate.Tag = IIf(getRight(99, CurrentUser), 1, 0)
                        btndelete.Tag = IIf(getRight(100, CurrentUser), 1, 0)
                    End If

                Else
                    btnupdate.Tag = 1
                    btndelete.Tag = 1
                End If
                cmbVoucherTp.SelectedIndex = 0
                btndelete.Text = "Delete"
            Else
                AddNewClick()
                cmbVoucherTp.SelectedIndex = 0
                'NextNumber()
                If userType Then
                    If cmbVoucherTp.Text = "TI" Then
                        btnupdate.Tag = IIf(getRight(94, CurrentUser), 1, 0)
                    Else
                        btnupdate.Tag = IIf(getRight(98, CurrentUser), 1, 0)
                    End If

                Else
                    btnupdate.Tag = 1
                End If
                btndelete.Text = "Clear"
                btndelete.Tag = 1
            End If
            Timer1.Enabled = True
            AddDttoCombo(cmblocation, dtlocationTb, False)
            AddDttoCombo(cmbtolocation, dtlocationTb, False)
            AddDttoCombo(cmbfc, dtcurrentyTb, False)
            cmblocation.Text = Dloc
            Dim dt As DataTable
            dt = _objcmnbLayer._fldDatatable("select isnull(setlocationtransferInSTADJ,0)setlocationtransferInSTADJ from CompanyTb")
            If dt.Rows.Count > 0 Then
                If dt(0)("setlocationtransferInSTADJ") Then
                    chkforlocation.Checked = True
                Else
                    chkforlocation.Checked = False
                End If
            End If
            'If cmbVoucherTp.Text = "TI" Then

            '    cmbtolocation.Visible = False
            'End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function JobAssianable(ByVal dtMasteer As DataTable) As Boolean
        JobAssianable = True
        If dtMasteer(0)("JobAssgble") Then
            If dtMasteer(0)("ForJobYN") Then
                If txtJob.Text = "" Then
                    MsgBox("A/c. [" & txtSuppName.Text & "] is refered to Job. Entry should need a value !", MsgBoxStyle.Information)
                    txtSuppName.Focus()
                    JobAssianable = False
                    Exit Function
                End If
            End If
        Else
            If txtJob.Text <> "" Then
                If MsgBox("A/c. [" & txtSuppName.Text & "] is not Job Assignable." & Chr(13) & Chr(10) & "But found some Job Entry and it will remove.  Are you sure !?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    txtSuppAlias.Focus()
                    JobAssianable = False
                    Exit Function
                Else
                    chgbyprg = True
                    txtJob.Text = ""
                    chgbyprg = False
                End If
            End If
        End If
    End Function
    Private Function chekDuplicate() As Boolean
        Dim dtTable As DataTable
        Dim varNextFoundBool As Boolean
ChkAgain:
        dtTable = _objcmnbLayer._fldDatatable("SELECT * FROM ItmInvCmnTb WHERE TrType = '" & cmbVoucherTp.Text & "' AND InvNo = " & Val(numVchrNo.Text) & " AND PreFix = '" & MkDbSrchStr(Trim(txtprefix.Text), True) & "'")
        If dtTable.Rows.Count > 0 Then
            If MsgBox("Entered Voucher number already exists. Fill next ?", vbQuestion + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                If Not varNextFoundBool Then
                    btnNext_Click(btnNext, New System.EventArgs())
                    varNextFoundBool = True
                Else
                    numVchrNo.Text = Val(numVchrNo.Text) + 1
                End If
                GoTo ChkAgain
            Else
                Return True
            End If
        End If
        varNextFoundBool = False
    End Function
    Private Function chkGridvalue() As Boolean
        Dim Exsts As Boolean
        chkGridvalue = True
        With grdVoucher
            MyActiveControl = grdVoucher
            'Dim dtSrlNo As DataTable
            Dim itemidStr As String
            Dim r As Integer
            itemidStr = ""
            For r = 0 To grdVoucher.RowCount - 1
                itemidStr = itemidStr & IIf(itemidStr = "", "", ",") & Val(.Item(ConstItemID, r).Value)
            Next
            'dtSrlNo = _objcmnbLayer._fldDatatable("SELECT SerialNo,itemid FROM ItmInvTrTb Left Join ItmInvCmnTb on ItmInvTrTb.Trid=ItmInvCmnTb.trid WHERE TrType='" & cmbVoucherTp.Text & "' AND ItmInvCmnTb.Trid<>" & loadedTrId)
            'Dim _qurey As EnumerableRowCollection(Of DataRow)

            For r = 0 To .RowCount - 1 '- 1
                If .Item(ConstIsSerial, r).Value = 1 And .Item(ConstSerialNo, r).Value = "" Then
                    .Rows(r).Selected = True
                    .CurrentCell = .Item(ConstQty, r)
                    MsgBox("Serial Number cannot be Blank !!", vbExclamation)
                    .FirstDisplayedScrollingRowIndex = r
                    GoTo Ter
                End If
                'If .Item(ConstSerialNo, r).Value <> "" Then
                '    _qurey = From data In dtSrlNo.AsEnumerable() Where data(0) = .Item(ConstSerialNo, r).Value Select data
                '    If _qurey.Count > 0 Then
                '        .Rows(r).Selected = True
                '        .CurrentCell = .Item(ConstSerialNo, r)
                '        MsgBox("Serial No already Exist !!", vbExclamation)
                '        .FirstDisplayedScrollingRowIndex = r
                '        .BeginEdit(True)
                '        GoTo Ter
                '    End If
                'End If
                If Val(.Item(ConstQty, r).Value) = 0 Then .Item(ConstQty, r).Value = 0
                If Trim(.Item(ConstBarcode, r).Value) = "" And CDbl(.Item(ConstQty, r).Value) = 0 Or .Item(ConstSlNo, r).Value.ToString = "M" Then
                    GoTo NextR
                End If
                Exsts = True
                If Val(.Item(ConstItemID, r).Value) = 0 Then
                    'dr = getItmDtls(1, Trim(.Item(ConstItemCode, r).Value))
                    'While dr.Read
                    '    itmFound = True
                    '    .Item(ConstItemID, r).Value = dr!ItemId
                    '    .Item(ConstBaseID, r).Value = dr!BaseId
                    'End While
                    'If Not itmFound Then
                    '    .Rows(r).Selected = True
                    '    .CurrentCell = .Item(ConstItemCode, r)
                    '    MsgBox("Item Code [" & .Item(ConstItemCode, r).Value & "] not exists !!", vbExclamation)
                    '    .FirstDisplayedScrollingRowIndex() = r
                    '    GoTo ter
                    'End If
                End If
                If CDbl(.Item(ConstQty, r).Value) = 0 Then
                    .Rows(r).Selected = True
                    .CurrentCell = .Item(ConstQty, r)
                    MsgBox("Zero Quantity !!", vbExclamation)
                    .FirstDisplayedScrollingRowIndex = r
                    GoTo Ter
                End If
                If CDbl(.Item(ConstUPrice, r).Value) = 0 Then
                    .Rows(r).Selected = True
                    .CurrentCell = .Item(ConstUPrice, r)
                    If MsgBox("Entered Price/Unit of Item [" & .Item(ConstItemCode, r).Value & "] is zero !!  Proceed ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo) = vbNo Then
                        .FirstDisplayedScrollingRowIndex() = r
                        GoTo Ter
                    End If
                End If
                If Not DontWarnAny Then
                    'sRs.Open("SELECT Amt As UnitCost FROM LastPSInfTb WHERE ItemId = " & Val(.Item(ConstBaseID, r).Value) & " and PartyNo =" & Val(txtSuppAlias.Tag), conMyADODB, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                    'If Not (sRs.EOF And sRs.BOF) Then
                    '    If (.Item(ConstActualPrice, r).Value) < IIf(IsDBNull(sRs.Fields("UnitCost").Value), 0, sRs.Fields("UnitCost").Value) * Val(.Item(ConstPMult, r).Value) / FCRt Then
                    '        .Rows(r).Selected = True
                    '        .CurrentCell = .Item(ConstUPrice, r)
                    '        If msgbox("The entered Sales Price is Below of Last Selling Price from the Customer [" & txtSuppName.Text & "] for the Item [" & .Item(ConstItemCode, r).Value & "] !!  Proceed ?", vbQuestion, 2, 0, vbYesNo, vbDefaultButton1) = vbNo Then
                    '            .FirstDisplayedScrollingRowIndex() = r
                    '            GoTo Ter
                    '        End If
                    '        GoTo SkpNxt
                    '    End If
                    'End If
                    'itmFound = False
                    'dt = getItmDtls(1, Trim(.Item(ConstItemCode, r).Value), True)
                    'If dt.Rows.Count > 0 Then

                    'End If

                    'While dr.Read
                    '    If isCust Then
                    '        If Val(.Item(ConstQty, r).Value) > IIf(IsDBNull(dr!QtyinHand), 0, dr!QtyinHand * Val(.Item(ConstPFraction, r).Value)) Then
                    '            .Rows(r).Selected = True
                    '            .CurrentCell = .Item(ConstQty, r)
                    '            If MsgBox("The entered Quantity of Item [" & .Item(ConstItemCode, r).Value & "] Exceeding Stock In Hand !!  Proceed ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    '                .FirstDisplayedScrollingRowIndex() = r
                    '                GoTo Ter
                    '            End If
                    '        End If
                    '    End If
                    'If (.Item(ConstActualPrice, r).Value) < IIf(IsDBNull(sRs.Fields("CostAverage").Value), 0, sRs.Fields("CostAverage").Value) * Val(.Item(ConstPMult, r).Value) / FCRt Then
                    '    .Rows(r).Selected = True
                    '    .CurrentCell = .Item(ConstUPrice, r)
                    '    If msgbox("The entered Sales Price is Below of CostAverage for the Item [" & .Item(ConstItemCode, r).Value & "] !!  Proceed ?", vbQuestion, 2, 0, vbYesNo, vbDefaultButton1) = vbNo Then
                    '        .FirstDisplayedScrollingRowIndex() = r
                    '        GoTo Ter
                    '    End If
                    'End If
                    'GoTo SkpNxt
                    'End While
                End If
SkpNxt:
                '***************** Accounting
                'If chkEnaJob.Checked = True Then
                '    If Val(.Item(ConstJobAcID, r).Value) <> 0 Then
                '        If sRs.State Then sRs.Close()
                '        sRs.Open("SELECT * FROM AccMast WHERE AccountNo = " & Val(.Item(ConstJobAcID, r).Value), conMyADODB, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                '        If sRs.EOF And sRs.BOF Then
                '            .Rows(r).Selected = True
                '            .CurrentCell = .Item(ConstJobCostAc, r)
                '            msgbox("Enter a valid Sales Account !!", vbExclamation, , 10, True)
                '            .FirstDisplayedScrollingRowIndex() = r
                '            GoTo Ter
                '        End If
                '        If Not VerifyBr(ActBr, sRs) Then
                '            .Rows(r).Selected = True
                '            .CurrentCell = .Item(ConstJobCostAc, r)
                '            msgbox("Conflicting Branches !!", vbExclamation, , 10, True)
                '            .FirstDisplayedScrollingRowIndex() = r
                '            GoTo Ter
                '        End If
                '        If ActBr = "" Then
                '            ActBr = IIf(sRs.Fields("BranchId").Value <> "", sRs.Fields("BranchId").Value, ActBr)
                '        End If
                '        If sRs.Fields("JobAssgble").Value Then
                '            If sRs.Fields("ForJobYN").Value Then
                '                If Trim(.Item(ConstJobCostAc, r).Value) = "" Then
                '                    .Rows(r).Selected = True
                '                    .CurrentCell = .Item(ConstJobCostAc, r)
                '                    msgbox("A/c. [" & .Item(ConstJobCostAc, r).Value & "] is refered to Job. Entry should need a value !", vbInformation, , 0, True)
                '                    .FirstDisplayedScrollingRowIndex() = r
                '                    GoTo Ter
                '                End If
                '            End If
                '        Else
                '            If Trim(.Item(ConstJob, r).Value) <> "" Then
                '                .Rows(r).Selected = True
                '                .CurrentCell = .Item(ConstJob, r)
                '                If msgbox("A/c. [" & .Item(ConstJobCostAc, r).Value & "] is not Job Assignable." & Chr(13) & Chr(10) & "But found some Job Entry and it will remove.  Are you sure !?", vbQuestion, 2, , , vbYesNo) = vbNo Then
                '                    .FirstDisplayedScrollingRowIndex() = r
                '                    GoTo Ter
                '                End If
                '                .Item(ConstJob, r).Value = ""
                '            End If
                '        End If
                '    Else
                '        .Rows(r).Selected = True
                '        .CurrentCell = .Item(ConstJobCostAc, r)
                '        msgbox("Job Cost Account not specified !!  Can not Proceed.", vbExclamation, , 10, True)
                '        .FirstDisplayedScrollingRowIndex() = r
                '        GoTo Ter
                '    End If
                'End If
NextR:
            Next r
        End With
        If Exsts = False Then
            MsgBox("Valid Transaction Entries not exists !!", MsgBoxStyle.Exclamation)
            txtReference.Focus()
            GoTo Ter
        End If
        clsreader()
        clsCnnection()
        Exit Function
ter:
        Return False
    End Function

    Private Sub cldrdate_KeyDownEvent(ByVal sender As Object, ByVal e As AxMSMask.MaskEdBoxEvents_KeyDownEvent)
        If e.keyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub picCloseProd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picCloseProd.Click
        plsrch.Visible = False
    End Sub

    Private Sub fProductEnquiry_CreateItem() Handles fProductEnquiry.CreateItem
        If fproductMast Is Nothing Then
            fproductMast = New ItemMastFrm
            fproductMast.MdiParent = fMainForm
            'fproductMast.Top =  Top + 500
            fproductMast.Show()
        Else
            fproductMast.Focus()

        End If
    End Sub

    Private Sub fProductEnquiry_getSelected(ByVal ItemcODE As String, ByVal isacc As Integer) Handles fProductEnquiry.getSelected
        chgbyprg = True
        fProductEnquiry.Close()
        SrchText = ItemcODE
        grdVoucher.Item(ConstItemCode, grdVoucher.CurrentRow.Index).Value = ItemcODE
        chgItm = True
        Valid(grdVoucher.CurrentRow.Index, ConstItemCode)
        grdVoucher.CurrentCell = grdVoucher.Item(ConstDescr, grdVoucher.CurrentRow.Index)
        grdBeginEdit()
        chgbyprg = False
    End Sub

    Private Sub numDisc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles numDisc.KeyDown
        If e.KeyCode = Keys.Return Then
            btnupdate.Focus()
        End If
    End Sub

    Private Sub txtroundOff_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        txtReference.Focus()
    End Sub

    Private Sub numOtherAmt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles numOtherAmt.KeyDown
        If e.KeyCode = Keys.Return Then
            btnothadd.Focus()
        End If
    End Sub

    Private Sub txtfcrt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtfcrt.KeyDown
        If e.KeyCode = Keys.Return Then
            txtDescr.Focus()
        End If
    End Sub


    Private Sub numDisc_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles numDisc.KeyPress, txtfcrt.KeyPress, numOtherAmt.KeyPress
        On Error Resume Next
        numCtrl = sender
        chgbyprg = True
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
            numCtrl.Text = Format(Val(numCtrl.Text), lnumformat)
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
        chgbyprg = False
    End Sub

    Private Sub numDisc_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numDisc.TextChanged
        'If chgbyprg Then Exit Sub
        'btnUpdate.Enabled = True
        'chgAmt = True
        'doCommandStat(True)
        'If Val(numDisc.Text) > 0 Then
        '    lblNetAmt.Text = Format(CDbl(lblTotAmt.Text) - CDbl(numDisc.Text), "0.00")
        'End If
        calculate()
        chgPost = True
    End Sub

    Private Sub numDisc_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles numDisc.Validated
        calOthCost()
    End Sub

    Private Sub grdSrch_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSrch.CellDoubleClick
        If grdVoucher.CurrentCell.ColumnIndex = ConstItemCode Then
            activecontrolname = "grdVoucher"
            doSelect(2)
            chgbyprg = True
            grdVoucher.CurrentCell = grdVoucher.Item(1, grdVoucher.CurrentRow.Index)
            chgItm = True
            Valid(grdVoucher.CurrentCell.RowIndex, grdVoucher.CurrentCell.ColumnIndex)
            chgbyprg = False
        ElseIf grdVoucher.CurrentCell.ColumnIndex = ConstSerialNo Then
            grdVoucher.CurrentCell = grdVoucher.Item(ConstSerialNo, grdVoucher.CurrentRow.Index)
        End If
        grdBeginEdit()
        plsrch.Visible = False
    End Sub


    Private Sub txtSuppAlias_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSuppAlias.Validated, txtSuppName.Validated
        If txtSuppAlias.Text = "" Then Exit Sub
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT AccMast.accid,Alias,AccDescr,Address1,Address2,Address3,Address4,Phone,Fax,EMail,PT1,PT2,PT3,PT4,TrdLcno,TrdDate from AccMast LEFT JOIN AccMastAddr ON AccMast.AccountNo=AccMastAddr.AccountNo where Alias='" & txtSuppAlias.Text & "'")
        If dt.Rows.Count > 0 Then
            txtSuppAlias.Tag = dt(0)("accid")
            lbladd1.Text = Trim("" & dt(0)("Address1"))
            lbladd2.Text = Trim("" & dt(0)("Address2"))
            lbladd3.Text = Trim("" & dt(0)("Address3"))
            lbladd4.Text = Trim("" & dt(0)("Address4"))
            lbladd5.Text = Trim("" & dt(0)("Phone"))
            lbladd6.Text = Trim("" & dt(0)("Email"))
            'lbladd7.Text = Trim("" & dt(0)("TrdLcno"))
            'If chkDate(dt(0)("TrdDate")) Then
            '    If DateValue(dt(0)("TrdDate")) > DateValue("01/01/1950") Then
            '        lbltrdate.Text = dt(0)("TrdDate")
            '    End If
            'End If

        Else
            txtSuppAlias.Text = ""
            txtSuppName.Text = ""
            lbladd1.Text = ""
            lbladd2.Text = ""
            lbladd3.Text = ""
            lbladd4.Text = ""
            lbladd5.Text = ""
            lbladd6.Text = ""
            lbladd7.Text = ""
            lbltrdate.Text = ""
        End If
    End Sub
    Private Sub setCustomer()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT ")
    End Sub
    Private Sub txtFooter_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        chgPost = True
    End Sub

    Private Sub numFcRt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Return Then
            AddRow()
        End If
    End Sub



    Private Sub btnSlct_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSlct.Click
        fSlctDoc = New SelectInvTr
        With fSlctDoc
            .strType = cmbVoucherTp.Text
            .Text = "Select Purchase Invoice"
            .ShowDialog()
        End With
        If Not fSlctDoc Is Nothing Then fSlctDoc = Nothing
    End Sub

    Private Sub fSlctDoc_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fSlctDoc.FormClosed
        fSlctDoc = Nothing
    End Sub

    Private Sub fSlctDoc_selectTr(ByVal trid As Long, ByVal TrType As String) Handles fSlctDoc.selectTr
        CheckNLoad(trid)
        fSlctDoc.Close()
        fSlctDoc = Nothing
    End Sub

    Private Sub txtDescr_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDescr.TextChanged
        chgPost = True
        'btnUpdate.Enabled = Val(btnUpdate.Tag) > 0
    End Sub

    Private Sub txtReference_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtReference.TextChanged
        chgPost = True
        'btnUpdate.Enabled = Val(btnUpdate.Tag) > 0
    End Sub

    Private Sub cldrdate_Change(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'btnUpdate.Enabled = True
        chgPost = True
    End Sub
    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        If chkFormat.Checked Then
            fRptFormat = New RptFormatfrm
            fRptFormat.RptType = cmbVoucherTp.Text
            fRptFormat.ShowDialog()
            fRptFormat = Nothing
        Else
            PrepareRpt(cmbVoucherTp.Text)
        End If
        'PrepareRpt("PO")
    End Sub

    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        LoadReport(RptFlName, RptCaption)
    End Sub


    Private Sub AddNewClick()
        If chgbyprg Then Exit Sub
        If ChgId Then
            Select Case MsgBox("Changes found !!  Do you want hold changes ?", vbYesNoCancel + vbQuestion)
                Case vbYes
                    'Hold procedure
                Case vbNo
                Case Else
                    Exit Sub
            End Select
        End If
        cmbVoucherTp.Enabled = True
        ClearControls()
        isModi = False
        enableCtrls(False)
        btnModify.Text = "&Modify"
        btndelete.Text = "Clear"
        isModi = False
        btnSlct.Visible = False
        numVchrNo.ReadOnly = True
        txtReference.Select()
        If userType Then
            btnupdate.Tag = IIf(getRight(41, CurrentUser), 1, 0)
        Else
            btnupdate.Tag = 1
        End If
        btndelete.Text = "Clear"
        btndelete.Tag = 1
        'btnNext.Visible = True
    End Sub

    Private Sub ClearClick()
        If MsgBox("Do You Want Clear Etry", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        ClearControls()
    End Sub

    Private Sub DeleteClick()
        If Val(btndelete.Tag) = 0 Then
            MsgBox("This user do not have permission to Remove", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If MsgBox("You are going to REMOVE the Purchase Invoice # " & numVchrNo.Text & Chr(13) & "Are you sure ?", vbOKCancel + vbQuestion + vbDefaultButton2) = vbOK Then
            'UpdtQty(loadedTrId, True, cmbVoucherTp.Text)
            '_objcmnbLayer._saveDatawithOutParm("DELETE  FROM ItmInvTrTb WHERE TrId =" & loadedTrId)
            '_objcmnbLayer._saveDatawithOutParm("DELETE  FROM DocAssgnTb WHERE IsPrchOrSls = 'True' AND TrId = " & loadedTrId)
            '_objcmnbLayer._saveDatawithOutParm("DELETE  FROM ItmInvCmnTb WHERE TrId = " & loadedTrId)

            'Dim dt As DataTable
            'Dim LNK As Long
            'dt = _objcmnbLayer._fldDatatable("SELECT LinkNo FROM AccTrCmn WHERE LnkNo=" & loadedTrId)
            'If dt.Rows.Count > 0 Then
            '    ModifyClosBal(cmbVoucherTp.Text, Trim(txtprefix.Text), Val(numVchrNo.Text))
            '    LNK = dt(0)(0)
            '    _objcmnbLayer._saveDatawithOutParm("DELETE  FROM AccTrCmn WHERE LinkNo=" & LNK)
            '    _objcmnbLayer._saveDatawithOutParm("DELETE  FROM AccTrDet WHERE LinkNo=" & LNK)
            'End If

            'Dim itemidsdatatable As New DataTable
            'Dim trdate As Date
            'itemidsdatatable = _objcmnbLayer._fldDatatable("SELECT Itemid,TrDate,SerialNo from ItmInvTrTb LEFT JOIN ItmInvCmnTb ON ItmInvTrTb.Trid=ItmInvCmnTb.Trid  WHERE ItmInvTrTb.TrId =" & loadedTrId)
            'If itemidsdatatable.Rows.Count > 0 Then
            '    trdate = DateValue(itemidsdatatable(0)("TrDate"))
            'End If
            _objInv.TrId = loadedTrId
            _objInv.TrType = IIf(cmbVoucherTp.Text = "TI", "IN", "OUT")
            _objInv.deleteInventoryTransactions()
            'For i = 0 To itemidsdatatable.Rows.Count - 1
            '    _objcmnbLayer._saveDatawithOutParm("UPDATE SerialNoTb SET PurchaseId=0 WHERE SerialNo='" & itemidsdatatable(i)("SerialNo") & "'")
            '    _objcmnbLayer._saveDatawithOutParm("delete from SerialNoTb where ISNULL(PurchaseId,0)=0 and ISNULL(SalesId,0)=0")

            '    _objInv.ItemId = itemidsdatatable(i)("Itemid")
            '    _objInv.TrDate = DateValue(itemidsdatatable(i)("TrDate"))
            '    _objInv.setcostAverageOnModification(UsrBr)
            'Next

            btnModify_Click(btnModify, New System.EventArgs)
        End If

    End Sub

    Private Sub cmdCrtSC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        fCrtAcc = New CreateAccNew
        With fCrtAcc
            If isCust Then
                .Condition = "GrpSetOn In ('Customer')"
            Else
                .Condition = "GrpSetOn In ('Supplier')"
            End If
            .bOnlyOne = True
            .ShowDialog()
        End With
        fCrtAcc = Nothing
    End Sub

    Private Sub fCrtAcc_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fCrtAcc.FormClosed
        fCrtAcc = Nothing
    End Sub
    Private Sub fCrtAcc_OpenAccMaster(ByRef AccountNo As Long) Handles fCrtAcc.OpenAccMaster
        Dim dt As DataTable
        chgbyprg = True
        dt = _objcmnbLayer._fldDatatable("SELECT Alias,AccDescr,accid FROM AccMast WHERE accountno=" & AccountNo)
        If dt.Rows.Count > 0 Then
            txtSuppAlias.Text = dt(0)("Alias")
            txtSuppName.Text = dt(0)("AccDescr")
            txtSuppAlias.Tag = dt(0)("accid")
        End If
        chgbyprg = False
        txtDescr.Focus()
    End Sub



    Private Sub txtJob_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtJob.Validated
        If _srchTxtId = 0 Then Exit Sub
        chgbyprg = True
        txtjobname.Text = (_objcmnbLayer.isValidEntry(txtJob.Text, _srchTxtId))
        'ldCustomer()
        chgbyprg = False
    End Sub





    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        If chgPost Then
            If MsgBox("Changes Found ! Do You want to Exit?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        End If
        Close()
    End Sub

    Private Sub grdVoucher_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellContentClick

    End Sub

    Private Sub UpdateClick()
        If Val(btnupdate.Tag) = 0 Then
            MsgBox("This user do not have permission to Update", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        plsrch.Visible = False
        Verify()
        chgPost = False
    End Sub

    Private Sub InsertClick()
        If grdVoucher.RowCount < 1 Then Exit Sub
        chgbyprg = True
        With grdVoucher
            Dim i As Integer
            '.AddItem.Row()
            .Rows.Insert(.CurrentRow.Index, 1)
            i = .CurrentRow.Index
            .Item(ConstB, i).Value = 1
            'If cmdImport.Tag = "Imported" Then .Item(ConstSlNo, i).Value = "M"
            '.Item(ConstWarrentyExpiry, i).Value = "0.00"
            .Item(ConstQty, i).Value = "0.00"
            .Item(ConstUPrice, i).Value = lnumformat
            .Item(ConstLTotal, i).Value = lnumformat
            .Item(ConstUnitOthCost, i).Value = lnumformat
            '.Item(Const, i).Value = lnumformat
            '.item(.Row, 18) = 1
            .Item(ConstPMult, i).Value = 1
            .Item(ConstJob, i).Value = txtJob.Text
            '.Item(ConstJobCostAc, i).Value = txtPurchaseName.Text
            '.Item(ConstJobAcID, i).Value = Val(txtPurchAlias.Tag)
            .Item(ConstPFraction, i).Value = 2
            reArrangeNo()
            .Rows(i - 1).Selected = True
            .CurrentCell = .Item(ConstItemCode, i - 1)
            .FirstDisplayedScrollingRowIndex = i - 1
        End With
        doCommandStat(True)
        chgItm = True
        chgbyprg = False
        chgPost = True
        grdVoucher.Select()
    End Sub

    Private Sub fSelect_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fSelect.FormClosed
        fSelect = Nothing
    End Sub

    Private Sub ctmControl_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub PrepareRpt(ByVal RptType As String, Optional ByVal Forprint As Boolean = False)

        If Val(numVchrNo.Text) = 0 Then
            MsgBox("Enter a valid Voucher Number !!", vbInformation)
            numVchrNo.Focus()
            Exit Sub
        End If
        Dim RptName As String
        Dim RptCaption As String = ""
        RptName = getRptDefFlName(RptType, RptCaption)
        If Trim(RptName) <> "" Then
            LoadReport(RptName, RptCaption, Forprint)
        End If
    End Sub
    Private Sub LoadReport(ByVal FileName As String, ByVal RptCaption As String, Optional ByVal ToPrint As Boolean = False)
        If Not frm Is Nothing Then
            frm.Close()
            frm = New ReprtviewNEWfrm
        Else
            frm = New ReprtviewNEWfrm
        End If
        _objInv.Prefix = txtPPrefix.Text
        _objInv.InvNo = Val(numPrintVchr.Text)
        _objInv.TrType = cmbVoucherTp.Text
        Dim ds As DataSet = _objInv.ldInvoice("rturnInventoryDetailsForInvoicePrint")
        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = MdiParent
        frm.Text = RptCaption
        frm.Show()
    End Sub


    Private Sub txtroundOff_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
        calculate()
    End Sub

    Private Sub cmnGridbutton_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        If isModi And loadedTrId = 0 Then
            MsgBox("Voucher has not been loaded", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        AddRow()
        'grdVoucher.CurrentCell = grdVoucher.Item(ConstItemCode, grdVoucher.CurrentRow.Index)
        doCommandStat(True)
        chgPost = True
    End Sub

    Private Sub btnrem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrem.Click
        RemoveRow()
        chgPost = True
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        UpdateClick()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        If btndelete.Text = "Clear" Then
            ClearClick()
        Else
            DeleteClick()
        End If

    End Sub


    Private Sub grdVoucher_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellDoubleClick
        Dim dt As DataTable = Nothing
        If e.ColumnIndex = ConstUnit Then
            With grdVoucher
                If e.ColumnIndex = ConstSlNo Then
                    .Item(ConstSlNo, .CurrentCell.RowIndex).Value = IIf(Val(.Item(ConstSlNo, .CurrentCell.RowIndex).Value) = 0, "", "M")
                    reArrangeNo()
                ElseIf .Item(ConstB, .CurrentCell.RowIndex).Value = "B" Then
                    .Item(ConstB, .CurrentCell.RowIndex).Value = "P1"
                    dt = _objcmnbLayer._fldDatatable("SELECT P1Unit U,P1Fra F,LastPurchCost,additionalcess FROM InvItm WHERE Itemid=" & Val(.Item(ConstItemID, .CurrentCell.RowIndex).Value))
                ElseIf .Item(ConstB, .CurrentCell.RowIndex).Value = "P1" Then
                    .Item(ConstB, .CurrentCell.RowIndex).Value = "P2"
                    dt = _objcmnbLayer._fldDatatable("SELECT P2Unit U,P2Fra F,LastPurchCost,additionalcess FROM InvItm WHERE Itemid=" & Val(.Item(ConstItemID, .CurrentCell.RowIndex).Value))
                ElseIf .Item(ConstB, .CurrentCell.RowIndex).Value = "P2" Then
u:
                    .Item(ConstB, .CurrentCell.RowIndex).Value = "B"
                    dt = _objcmnbLayer._fldDatatable("SELECT Unit U,1 F,LastPurchCost,additionalcess FROM InvItm WHERE Itemid=" & Val(.Item(ConstItemID, .CurrentCell.RowIndex).Value))
                End If
                If dt Is Nothing Then GoTo u
                If dt.Rows.Count > 0 Then
                    .Item(ConstUnit, .CurrentCell.RowIndex).Value = dt(0)("U")
                    .Item(ConstPMult, .CurrentCell.RowIndex).Value = dt(0)("F")
                    Dim cost As Double
                    Dim addcess As Double
                    addcess = Val(dt(0)("additionalcess") & "") * Val(.Item(ConstPMult, .CurrentCell.RowIndex).Value & "")
                    cost = getPurchAmt(dt(0)("LastPurchCost"), Val(txtSuppAlias.Tag), Val(.Item(ConstItemID, .CurrentCell.RowIndex).Value))
                    cost = cost * Val(.Item(ConstPMult, .CurrentCell.RowIndex).Value)
                    .Item(ConstActualPrice, .CurrentCell.RowIndex).Value = cost
                    .Item(ConstUPrice, .CurrentCell.RowIndex).Value = Format(cost, lnumformat)
                    .Item(ConstAdditionalcess, .CurrentCell.RowIndex).Value = addcess
                    calculate(, True)
                End If
            End With
        End If
    End Sub


    Private Sub TabPage1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub grdVoucher_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellEndEdit
        Dim col As Integer = e.ColumnIndex
        Dim ndec1 As Integer
        If col = ConstQty Or col = ConstUPrice Or col = ConstDisAmt Or col = ConstLTotal Or col = ConstTaxP Or col = ConstMRP Or col = ConstSP1 Or col = ConstSP2 Or col = ConstSP3 Then
            If col = ConstQty Then
                ndec1 = grdVoucher.Item(ConstPFraction, grdVoucher.CurrentRow.Index).Value
            Else
                ndec1 = NoOfDecimal
            End If
            grdVoucher.Item(col, e.RowIndex).Value = Format(Convert.ToDecimal(grdVoucher.Item(col, e.RowIndex).Value), "#,##0" & IIf(ndec1 = 0, "", "." & Strings.StrDup(ndec1, "0")))
        End If
    End Sub

    Private Sub fSerialno_AddSerialNos() Handles fSerialno.AddSerialNos
        Dim serialCount As Integer
        With fSerialno.grdVoucher
            serialCount = .RowCount
            Dim i As Integer
            Dim j As Integer
            Dim lstRow As Integer
            lstRow = grdVoucher.CurrentRow.Index
            chgbyprg = True
            For i = 0 To serialCount - 1
                If i = 0 And grdVoucher.Item(ConstSerialNo, lstRow).Value = "" Then
                    grdVoucher.Item(ConstSerialNo, lstRow).Value = .Item(0, i).Value
                    grdVoucher.Item(ConstWarrentyExpiry, lstRow).Value = .Item(1, i).Value
                    grdVoucher.Item(ConstQty, lstRow).Value = Format(1, numFormat)
                Else
                    grdVoucher.Rows.Add(1)
                    For j = 0 To grdVoucher.ColumnCount - 1
                        grdVoucher.Item(j, grdVoucher.Rows.Count - 1).Value = grdVoucher.Item(j, lstRow).Value
                    Next
                    grdVoucher.Item(ConstSerialNo, grdVoucher.Rows.Count - 1).Value = .Item(0, i).Value
                    grdVoucher.Item(ConstWarrentyExpiry, grdVoucher.Rows.Count - 1).Value = .Item(1, i).Value
                    grdVoucher.Item(ConstId, grdVoucher.Rows.Count - 1).Value = 0
                End If
                AddTodtSerialNo(.Item(0, i).Value, Val(grdVoucher.Item(ConstItemID, grdVoucher.CurrentRow.Index).Value), grdVoucher.CurrentRow.Index, DateValue(grdVoucher.Item(ConstWarrentyExpiry, grdVoucher.CurrentRow.Index).Value), Val(grdVoucher.Item(ConstId, grdVoucher.CurrentRow.Index).Value))

            Next
            grdVoucher.CurrentCell = grdVoucher.Item(ConstUPrice, grdVoucher.Rows.Count - 1)
            'chgbyprg = True
            'grdBeginEdit
            'chgbyprg = False
        End With
        chgbyprg = False
        chgPost = True
        'MsgBox(grdVoucher.Item(ConstItemCode, grdVoucher.RowCount - 1).Value)
        calculate()
        reArrangeNo()
        fSerialno.Close()
        fSerialno = Nothing
    End Sub
    Private Sub AddTodtSerialNo(ByVal serialno As String, ByVal itemid As Long, ByVal rowindex As Integer, ByVal datefld As Date, ByVal detid As Long)
        Dim dtrow As DataRow
        dtrow = _objcmnbLayer.dtSerialNo.NewRow
        dtrow("ItmSerialNo") = serialno
        dtrow("Wdate") = Format(datefld, DtFormat)
        dtrow("Trid") = loadedTrId
        dtrow("Detid") = detid
        dtrow("itemid") = itemid
        dtrow("RowIndex") = rowindex
        dtrow("dtTbIndex") = _objcmnbLayer.dtSerialNo.Rows.Count
        _objcmnbLayer.dtSerialNo.Rows.Add(dtrow)
    End Sub

    Private Sub cldrdate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cldrdate.KeyDown
        If e.KeyCode = Keys.Return Then
            txtSuppName.Focus()
        End If

    End Sub

    Private Sub cldrdate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cldrdate.ValueChanged
        chgPost = True
    End Sub

    Private Sub cmbVoucherTp_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVoucherTp.SelectedIndexChanged
        If grdVoucher.RowCount > 0 Then
            If MsgBox("Changes found! Do you want to change Transaction Type?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            grdVoucher.Rows.Clear()
        End If
        SetGridHead()
        If isModi Then
            If userType Then
                If cmbVoucherTp.Text = "TI" Then
                    btnupdate.Tag = IIf(getRight(95, CurrentUser), 1, 0)
                    btndelete.Tag = IIf(getRight(96, CurrentUser), 1, 0)
                Else
                    btnupdate.Tag = IIf(getRight(99, CurrentUser), 1, 0)
                    btndelete.Tag = IIf(getRight(100, CurrentUser), 1, 0)
                End If

            Else
                btnupdate.Tag = 1
                btndelete.Tag = 1
            End If

            btndelete.Text = "Delete"
        Else
            NextNumber()
            If cmbVoucherTp.Text = "TO" Then
                grdVoucher.Columns(ConstManufacturingdate).Visible = False
                grdVoucher.Columns(ConstWarrentyExpiry).Visible = False
            End If
        End If
        resizeGridColumn(grdVoucher, ConstDescr)
        If cmbVoucherTp.Text = "TI" Then
            cmbimporttr.Items.Clear()
            cmbimporttr.Items.Add("TO")
            cmbimporttr.Text = "TO"
            txtPurchaseName.ReadOnly = True
            txtSuppName.ReadOnly = False
        Else
            cmbimporttr.Items.Clear()
            cmbimporttr.Items.Add("TI")
            cmbimporttr.Text = "TI"
            txtPurchaseName.ReadOnly = False
            txtSuppName.ReadOnly = True
        End If
    End Sub

    Private Sub Label5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label5.Click

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        If grdVoucher.ColumnCount = 0 Then Exit Sub
        If EnableWarranty = False And Me.Width > 1200 Then resizeGridColumn(grdVoucher, ConstDescr)
        If Me.Width <= 1200 Or grdVoucher.Columns(ConstDescr).Width <= 25 Then
            grdVoucher.Columns(ConstDescr).Width = 150
        End If
    End Sub


    Private Sub numVchrNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numVchrNo.TextChanged

    End Sub

    Private Sub grdSrch_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSrch.CellContentClick

    End Sub

    Private Sub Label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label4.Click

    End Sub

    Private Sub fproductMast_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fproductMast.FormClosed
        fproductMast = Nothing
    End Sub

    Private Sub fproductMast_updateItemCode(ByVal ItemCode As String) Handles fproductMast.updateItemCode
        chgbyprg = True
        fproductMast.Close()
        SrchText = ItemCode
        grdVoucher.Item(ConstItemCode, grdVoucher.CurrentRow.Index).Value = ItemCode
        chgItm = True
        Valid(grdVoucher.CurrentRow.Index, ConstItemCode)
        grdVoucher.CurrentCell = grdVoucher.Item(ConstDescr, grdVoucher.CurrentRow.Index)
        grdBeginEdit()
        chgbyprg = False
    End Sub


    Private Sub cmbfc_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbfc.SelectedIndexChanged
        returnFcrt()
    End Sub
    Private Sub returnFcrt()
        Dim dt As DataTable = _objcmnbLayer._fldDatatable("Select CurrencyRate,[Decimal Places] from CurrencyTb where CurrencyCode='" & cmbfc.Text & "'", False)
        If (dt.Rows.Count > 0) Then
            NDec = dt(0)("Decimal Places")
            lnumformat = "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))
            txtfcrt.Text = Format(CDbl(dt(0)("CurrencyRate")), lnumformat)
        Else
            txtfcrt.Text = Format(0, numFormat)
            NDec = NoOfDecimal
        End If
    End Sub

    Private Sub txtfcrt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtfcrt.TextChanged

    End Sub

    Private Sub txtfcrt_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtfcrt.Validated
        FCRt = txtfcrt.Text
    End Sub

    Private Sub btntax_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btntax.Click
        'CalculateGST()
        Exit Sub
        ShowTax.grdVoucher.DataSource = dtTax
        ShowTax.ShowDialog()
    End Sub

    Private Sub btnothercost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnothercost.Click
        tbothercost.Visible = Not tbothercost.Visible
        If tbothercost.Visible Then
            txtcredit.Focus()
            clearOtherCostFileds()
            If btnOthrOk.Tag = "chg" Then
                showOtherCost(False)
            End If
        End If
        calculate(False)
    End Sub


    Private Sub clearOtherCostFileds()
        chgbyprg = True
        txtdebit.Text = txtPurchaseName.Text
        txtdebit.Tag = Val(txtPurchaseName.Tag)
        txtcredit.Text = ""
        txtOthrDescription.Text = ""
        txtOthrRef.Text = ""
        numOtherAmt.Text = Format(0, lnumformat)
        txtcredit.Tag = ""
        chgbyprg = False
    End Sub
    Private Sub showOtherCost(ByVal CrossBr As Boolean)
        Dim sRs As DataTable
        Dim i As Integer
        Dim rw As Integer
        sRs = _objcmnbLayer._fldDatatable("SELECT AccTrDet.*, AccDescr, Alias, BranchId FROM AccTrDet LEFT JOIN AccMast ON AccMast.accid = AccTrDet.AccountNo WHERE  LinkNo IN (SELECT LinkNo FROM AccTrCmn WHERE JVType = '" & cmbVoucherTp.Text & "' AND JVNum = " & Val(numVchrNo.Text) & " AND PreFix = '" & MkDbSrchStr(Trim(txtprefix.Text)) & "') AND OthCost > 0  ORDER BY OthCost, DealAmt DESC")
        If sRs.Rows.Count > 0 Then
            Dim ttlOthCost As Double
            With grdOtherCost
                .RowCount = 0
                For rw = 0 To sRs.Rows.Count - 1
                    If sRs(rw)("OthCost") > .RowCount Then .Rows.Add(1) '""

                    If sRs(rw)("DealAmt") > 0 Then
                        i = .RowCount - 1
                        .Item(CostDbName, i).Value = Trim("" & sRs(rw)("AccDescr"))
                        .Item(CostFCName, i).Value = Trim("" & sRs(rw)("CurrencyCode")) ', "", AccTranTb!CurrencyCode)
                        .Item(CostDrAcc, i).Value = sRs(rw)("AccountNo")
                        .Item(CostAmount, i).Value = Format(sRs(rw)("DealAmt") / FCRt, lnumformat)
                        .Item(CostReference, i).Value = Trim("" & sRs(rw)("Reference"))
                        .Item(CostDescr, i).Value = Trim("" & sRs(rw)("EntryRef"))
                        .Item(CostFCAmount, i).Value = Format(sRs(rw)("DealAmt"), lnumformat)
                        .Item(CostFCRate, i).Value = Format(IIf(sRs(rw)("CurrRate") = 0 Or IsDBNull(sRs(rw)("CurrRate")), 1, sRs(rw)("CurrRate")), lnumformat)
                        ttlOthCost = ttlOthCost + sRs(rw)("DealAmt")
                    Else
                        .Item(CostCrName, i).Value = Trim("" & sRs(rw)("AccDescr"))
                        .Item(CostCrAcc, i).Value = sRs(rw)("AccountNo")
                    End If
                Next
            End With
            lblOthCost.Text = Format(ttlOthCost, lnumformat)
            OthCost = ttlOthCost
        Else
            'pnlOthCost.Visible = False
        End If
    End Sub


    Private Sub setOtherCostHead()
        With grdOtherCost

            SetGridProperty(grdOtherCost)

            .ColumnCount = 11
            .Columns(CostSlNo).HeaderText = "SlNo"
            .Columns(CostSlNo).Visible = False
            .Columns(CostSlNo).ReadOnly = True
            .Columns(CostSlNo).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(CostSlNo).Frozen = True
            .Columns(CostSlNo).DefaultCellStyle.Format = "N0"
            .Columns(CostSlNo).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(CostDbName).HeaderText = "Debit Name"
            .Columns(CostDbName).Width = Width * 10 / 100   '100
            .Columns(CostDbName).SortMode = DataGridViewColumnSortMode.NotSortable

            .Columns(CostAmount).HeaderText = "Amount"
            .Columns(CostAmount).Width = Width * 7.5 / 100   '100
            .Columns(CostAmount).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(CostAmount).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(CostAmount).DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns(CostReference).HeaderText = "Reference"
            .Columns(CostReference).Width = 85
            .Columns(CostReference).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(CostReference).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '.Columns(CostReference).DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns(CostDescr).HeaderText = "Description"
            .Columns(CostDescr).Width = Width * 10 / 100   '100
            .Columns(CostDescr).SortMode = DataGridViewColumnSortMode.NotSortable


            .Columns(CostCrName).HeaderText = "Credit Name"
            .Columns(CostCrName).Width = Width * 10 / 100   '100
            .Columns(CostCrName).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(CostReference).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '.Columns(CostReference).DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns(CostFCAmount).HeaderText = "FC Amount"
            .Columns(CostFCAmount).Width = Width * 10 / 100   '100
            .Columns(CostFCAmount).ReadOnly = False
            .Columns(CostFCAmount).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(CostFCAmount).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(CostFCAmount).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(CostFCAmount).Visible = False

            .Columns(CostFCName).HeaderText = "FC"
            .Columns(CostFCName).Width = 50
            .Columns(CostFCName).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(CostFCName).Visible = False
            '.Columns(CostReference).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '.Columns(CostReference).DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns(CostFCRate).HeaderText = "FC Rate"
            .Columns(CostFCRate).Width = 75
            .Columns(CostFCRate).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(CostFCRate).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(CostFCRate).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(CostFCRate).Visible = False

            .Columns(CostDrAcc).HeaderText = "DrAcc"
            .Columns(CostDrAcc).Visible = False
            .Columns(CostDrAcc).ReadOnly = True
            .Columns(CostDrAcc).SortMode = DataGridViewColumnSortMode.NotSortable

            .Columns(CostCrAcc).HeaderText = "CrAcc"
            .Columns(CostCrAcc).Visible = False
            .Columns(CostCrAcc).ReadOnly = True
            .Columns(CostCrAcc).SortMode = DataGridViewColumnSortMode.NotSortable


        End With
    End Sub

    Private Sub numOtherAmt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numOtherAmt.TextChanged

    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub grdOtherCost_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdOtherCost.CellContentClick

    End Sub

    Private Sub btnothadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnothadd.Click
        If Val(txtdebit.Tag) = 0 Then
            MsgBox("Invalid Debit", MsgBoxStyle.Exclamation, Nothing)
            txtPurchaseName.Focus()
        ElseIf Val(txtcredit.Tag) = 0 Then
            MsgBox("Invalid Credit", MsgBoxStyle.Exclamation, Nothing)
            txtcredit.Focus()
        ElseIf txtOthrRef.Text = "" Then
            MsgBox("Invalid Reference", MsgBoxStyle.Exclamation, Nothing)
        ElseIf Val(numOtherAmt.Text) = 0 Then
            MsgBox("Invalid Amount", MsgBoxStyle.Exclamation, Nothing)
        Else
            AddRowOthCost()
            clearOtherCostFileds()
            txtcredit.Focus()
        End If

    End Sub

    Private Sub AddRowOthCost()

        Dim num As Integer
        With grdOtherCost
            If Val(.Tag) = 0 Then
                .Rows.Add(1)
                num = .RowCount - 1
            Else
                num = Val(.Tag) - 1
            End If
            .Item(CostDbName, num).Value = txtdebit.Text
            .Item(CostAmount, num).Value = Format(CDbl(numOtherAmt.Text), lnumformat)
            .Item(CostReference, num).Value = txtOthrRef.Text
            .Item(CostDescr, num).Value = txtOthrDescription.Text
            .Item(CostCrName, num).Value = txtcredit.Text
            .Item(CostFCAmount, num).Value = Format(CDbl(numOtherAmt.Text), lnumformat)
            .Item(CostFCName, num).Value = cmbfc.Text
            .Item(CostFCRate, num).Value = FCRt
            .Item(CostDrAcc, num).Value = Val(txtdebit.Tag)
            .Item(CostCrAcc, num).Value = Val(txtcredit.Tag)
            .Tag = 0
        End With
        OthrreArrangeNo()
        calculate(False)
    End Sub
    Private Sub OthrreArrangeNo()
        chgbyprg = True
        Dim num As Integer = 0
        Dim num3 As Integer = (grdOtherCost.Rows.Count - 1)
        Dim i As Integer = 0
        Do While (i <= num3)
            num += 1
            grdOtherCost.Item(0, i).Value = num
            i += 1
        Loop
        chgbyprg = False
    End Sub

    Private Sub btnothRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnothRemove.Click
        If grdOtherCost.RowCount > 0 Then
            If MsgBox("You Are Going to Remove the Row. Are You Sure ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.No Then Exit Sub
            grdOtherCost.Rows.RemoveAt(grdOtherCost.CurrentRow.Index)
            grdOtherCost.ClearSelection()
            OthrreArrangeNo()
            calculate(False)
        End If

    End Sub

    Private Sub btnOthrOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOthrOk.Click
        tbothercost.Visible = False
        btnOthrOk.Tag = "chg"
        calculate(False)

    End Sub

    Private Sub btnothcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnothcancel.Click
        tbothercost.Visible = False
        btnOthrOk.Tag = ""
    End Sub

    Private Sub txtOthrRef_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtOthrRef.KeyDown, txtOthrDescription.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub


    Private Sub txtcredit_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcredit.Validated
        Dim dt As DataTable = _objcmnbLayer._fldDatatable(("SELECT accid,AccDescr from AccMast where AccDescr='" & txtcredit.Text & "'"), False)
        If dt.Rows.Count > 0 Then
            chgbyprg = True
            txtcredit.Text = dt(0)("AccDescr")
            txtcredit.Tag = Val(dt(0)("accid"))
            chgbyprg = False
        End If

    End Sub
    Private Sub UpdateOtherCost(ByVal LinkNo As Long)
        Dim c As Byte
        If Val(lblOthCost.Text) > 0 Then
            With grdOtherCost
                For i = 0 To .Rows.Count - 1
                    If CDbl(.Item(CostAmount, i).Value) <> 0 Then
                        c = c + 1
                        .Item(CostFCRate, i).Value = FCRt
                        .Item(CostFCName, i).Value = cmbfc.Text
                        'Debit
                        setAcctrDetValue(LinkNo, Val(.Item(CostDrAcc, i).Value), Trim(.Item(CostReference, i).Value), Trim(.Item(CostDescr, i).Value), CDbl(.Item(CostAmount, i).Value) * FCRt, txtJob.Text, "", 1, c, "", _
                                        "", Val(txtSuppAlias.Tag), Val(.Item(CostDrAcc, i).Value) & Trim(.Item(CostReference, i).Value), .Item(CostFCName, i).Value, IIf(.Item(CostFCName, i).Value = "", 1, CDbl(.Item(CostFCRate, i).Value)))
                        _objTr.saveAccTrans()

                        'Credit
                        setAcctrDetValue(LinkNo, Val(.Item(CostCrAcc, i).Value), Trim(.Item(CostReference, i).Value), Trim(.Item(CostDescr, i).Value), CDbl(.Item(CostAmount, i).Value) * -1 * FCRt, txtJob.Text, "", 1, c, "", _
                                         "", Val(txtSuppAlias.Tag), Val(.Item(CostCrAcc, i).Value) & Trim(.Item(CostReference, i).Value), .Item(CostFCName, i).Value, IIf(.Item(CostFCName, i).Value = "", 1, CDbl(.Item(CostFCRate, i).Value)))
                        _objTr.saveAccTrans()
                    End If
                Next
            End With
        End If
    End Sub

    Private Sub PurchaseInvoiceFrm_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Timer1.Enabled = True
    End Sub

    Private Sub grdOtherCost_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdOtherCost.DoubleClick
        If grdOtherCost.RowCount <> 0 Then
            Dim rowIndex As Integer = grdOtherCost.CurrentCell.RowIndex
            chgbyprg = True
            txtdebit.Text = grdOtherCost.Item(CostDbName, rowIndex).Value
            numOtherAmt.Text = Format(CDbl(grdOtherCost.Item(CostAmount, rowIndex).Value), lnumformat)
            txtOthrRef.Text = grdOtherCost.Item(CostReference, rowIndex).Value
            txtOthrDescription.Text = grdOtherCost.Item(CostDescr, rowIndex).Value
            txtcredit.Text = grdOtherCost.Item(CostCrName, rowIndex).Value
            txtdebit.Tag = Val(grdOtherCost.Item(CostDrAcc, rowIndex).Value)
            txtcredit.Tag = Val(grdOtherCost.Item(CostCrAcc, rowIndex).Value)
            grdOtherCost.Tag = (rowIndex + 1)
            chgbyprg = False
        End If

    End Sub

    Private Sub chkTaxbill_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTaxbill.CheckedChanged
        calculate()
    End Sub
    Private Sub PasteFrom(ByVal trid As Long)
        Dim sRs As DataTable
        Dim i As Integer
        Dim UPerPack As Double
        sRs = _objcmnbLayer._fldDatatable("SELECT ItmInvTrTb.*, [Item Code],Warrentyname,isSerialNo,PreFix,invno,trdate FROM ItmInvTrTb LEFT JOIN InvItm ON InvItm.ItemId = ItmInvTrTb.ItemId " & _
                                          " LEFT JOIN InvItmPropertiesTb ON InvItmPropertiesTb.Itemid=invitm.itemid " & _
                                           " LEFT JOIN ItmInvCmnTb ON ItmInvCmnTb.trid=ItmInvTrTb.trid " & _
                                          "LEFT JOIN WarrentyMastertb ON WarrentyMastertb.Wid=ItmInvTrTb.WarrentyId WHERE ItmInvTrTb.TrId = " & trid & " ORDER BY SlNo")
        grdVoucher.RowCount = 0
        If _objcmnbLayer.dtSerialNo.Rows.Count > 0 Then _objcmnbLayer.dtSerialNo.Rows.Clear()
        Dim tNumformat As String
        With grdVoucher
            .Rows.Clear()
            If sRs.Rows.Count > 0 Then
                For i = 0 To sRs.Rows.Count - 1
                    If i = 0 And isImport Then
                        txtReference.Text = sRs(i)("PreFix") & sRs(i)("invno")
                        cldrdate.Value = DateValue(sRs(i)("trdate"))
                    End If
                    grdVoucher.Rows.Add(1)
                    chgbyprg = True
                    'grdVoucher.Item(ConstSlNo, i).Value = IIf(sRs(i)("MsgId") = 1, "M", IIf(sRs(i)("MsgId") = 2, "L", ""))
                    UPerPack = IIf(sRs(i)("PFraction") = 0, 1, sRs(i)("PFraction"))
                    'grdVoucher.Item(ConstSlNo, i).Value = IIf(sRs(i)("MsgId") = 1, "M", IIf(sRs(i)("MsgId") = 2, "L", ""))
                    If grdVoucher.Item(ConstSlNo, i).Value <> "L" Then
                        UPerPack = IIf(sRs(i)("PFraction") = 0 Or IsDBNull(sRs(i)("PFraction")), 1, sRs(i)("PFraction"))
                        grdVoucher.Item(ConstItemCode, i).Value = Trim("" & sRs(i)("Item Code"))
                        grdVoucher.Item(ConstItemID, i).Value = sRs(i)("ItemId")
                        grdVoucher.Item(ConstPFraction, i).Value = "2"
                        grdVoucher.Item(ConstPMult, i).Value = UPerPack 'IIf(IsNull(sRs!PFraCount), "2", sRs!PFraCount)
                    Else
                        grdVoucher.Item(ConstPFraction, i).Value = "2"
                        grdVoucher.Item(ConstPMult, i).Value = "1"
                        grdVoucher.Item(ConstItemID, i).Value = sRs(i)("LMsgNo")
                    End If
                    grdVoucher.Item(ConstDescr, i).Value = IIf(IsDBNull(sRs(i)("IDescription")), "", sRs(i)("IDescription"))
                    grdVoucher.Item(ConstB, i).Value = IIf(sRs(i)("Method") & "" = "", "B", Trim(sRs(i)("Method") & ""))
                    grdVoucher.Item(ConstUnit, i).Value = Trim("" & sRs(i)("Unit"))
                    grdVoucher.Item(ConstLocation, i).Value = Trim("" & sRs(i)("Warrentyname")) ' !Colour
                    grdVoucher.Item(ConstQty, i).Value = Format(sRs(i)("TrQty") / UPerPack, "#,##0" & IIf(Val(grdVoucher.Item(ConstPFraction, i).Value) = 0, "", "." & Strings.StrDup(CByte(grdVoucher.Item(ConstPFraction, i).Value), "0")))
                    grdVoucher.Item(ConstUPrice, i).Value = Format(sRs(i)("UnitCost") * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & lnumformat)
                    grdVoucher.Item(ConstActualPrice, i).Value = sRs(i)("UnitCost") * UPerPack / FCRt
                    grdVoucher.Item(ConstLTotal, i).Value = Format((sRs(i)("TrQty") * sRs(i)("UnitCost")) * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & lnumformat)
                    grdVoucher.Item(ConstUnitOthCost, i).Value = Format(sRs(i)("UnitOthCost") * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & lnumformat)
                    grdVoucher.Item(ConstActualOthCost, i).Value = sRs(i)("UnitOthCost") * UPerPack / FCRt
                    grdVoucher.Item(ConstSerialNo, i).Value = sRs(i)("SerialNo")
                    If Val(sRs(i)("DisP") & "") = 0 Then sRs(i)("DisP") = 0
                    grdVoucher.Item(ConstDisP, i).Value = Format(sRs(i)("DisP"), numFormat)
                    If Val(sRs(i)("ItemDiscount") & "") = 0 Then sRs(i)("ItemDiscount") = 0
                    grdVoucher.Item(ConstDisAmt, i).Value = Format(sRs(i)("ItemDiscount") * UPerPack / FCRt, lnumformat)

                    grdVoucher.Item(ConstBarcode, i).Value = sRs(i)("HSNCode")
                    grdVoucher.Item(ConstCGSTP, i).Value = sRs(i)("CSGTP")
                    grdVoucher.Item(ConstCGSTAmt, i).Value = sRs(i)("CGSTAMT")

                    grdVoucher.Item(ConstSGSTP, i).Value = sRs(i)("SGSTP")
                    grdVoucher.Item(ConstSGSTAmt, i).Value = sRs(i)("SGSTAmt")

                    grdVoucher.Item(ConstIGSTP, i).Value = sRs(i)("IGSTP")
                    grdVoucher.Item(ConstIGSTAmt, i).Value = sRs(i)("IGSTAmt")
                    If Not IsDBNull(sRs(i)("isSerialNo")) Then
                        grdVoucher.Item(ConstIsSerial, i).Value = IIf(sRs(i)("isSerialNo"), 1, 0)
                    Else
                        grdVoucher.Item(ConstIsSerial, i).Value = 0
                    End If


                    If Val(sRs(i)("taxAmt") & "") = 0 Then
                        sRs(i)("taxAmt") = 0
                    End If
                    grdVoucher.Item(ConstTaxAmt, i).Value = sRs(i)("taxAmt") * UPerPack / FCRt
                    grdVoucher.Item(ConstTaxP, i).Value = sRs(i)("taxP")
                    .Item(constItmTot, i).Value = Format((.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value), lnumformat)
                    'grdVoucher.Item(ConstMthd, i).Value = sRs(i)("Method")
                    'grdVoucher.Item(ConstUnitVal, i).Value = sRs(i)("UnitValue") * UPerPack / FCRt
                    'grdVoucher.Item(ConstDiscOther, i).Value = sRs(i)("DiscOther") * UPerPack / FCRt
                    grdVoucher.Item(ConstDiscOther, i).Value = sRs(i)("UnitDiscount") * UPerPack / FCRt
                    'grdVoucher.Item(ConstImpDocId, i).Value = sRs(i)("ImpDocId")
                    'grdVoucher.Item(ConstImpLnId, i).Value = sRs(i)("ImpDocLnNo")
                    grdVoucher.Item(ConstId, i).Value = IIf(isImport, 0, sRs(i)("id"))
                    If Not IsDBNull(sRs(i)("WarrentyExpDate")) Then
                        If DateValue(sRs(i)("WarrentyExpDate")) > DateValue("01/01/1950") Then
                            grdVoucher.Item(ConstWarrentyExpiry, i).Value = sRs(i)("WarrentyExpDate")
                        End If
                    End If
                    If Not IsDBNull(sRs(i)("manufacturingdate")) Then
                        If DateValue(sRs(i)("manufacturingdate")) > DateValue("01/01/1950") Then
                            grdVoucher.Item(ConstManufacturingdate, i).Value = sRs(i)("manufacturingdate")
                        End If
                    End If
                    If .Item(ConstSerialNo, i).Value <> "" And enableSerialnumber Then
                        AddTodtSerialNo(.Item(ConstSerialNo, i).Value, Val(.Item(ConstItemID, i).Value), i, DateValue(.Item(ConstWarrentyExpiry, i).Value), Val(.Item(ConstId, i).Value))
                    End If
                    If enableWoodSale Then
                        tNumformat = "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))
                        grdVoucher.Item(ConstWoodQty, i).Value = Format(sRs(i)("WoodNetQty") / UPerPack, tNumformat)
                        grdVoucher.Item(ConstWoodDiscQty, i).Value = Format(sRs(i)("WoodDiscountQty") / UPerPack, tNumformat)
                    End If
                    If Val(sRs(i)("SP1") & "") = 0 Then sRs(i)("SP1") = 0
                    grdVoucher.Item(ConstSP1, i).Value = Format(sRs(i)("SP1"), numFormat)
                    If Val(sRs(i)("SP2") & "") = 0 Then sRs(i)("SP2") = 0
                    grdVoucher.Item(ConstSP2, i).Value = Format(sRs(i)("SP2"), numFormat)
                    If Val(sRs(i)("SP3") & "") = 0 Then sRs(i)("SP3") = 0
                    grdVoucher.Item(ConstSP3, i).Value = Format(sRs(i)("SP3"), numFormat)
                    If Val(sRs(i)("MRP") & "") = 0 Then sRs(i)("MRP") = 0
                    grdVoucher.Item(ConstMRP, i).Value = Format(sRs(i)("MRP"), numFormat)
                    calcualteLineTotal(i)

                Next
                reArrangeNo()
            End If
        End With
        calculate()
        If sRs.Rows.Count > 0 Then sRs.Clear()
    End Sub

    Private Sub grdVoucher_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellEnter
        If cmbVoucherTp.Text = "TO" Then
            If e.ColumnIndex = ConstSerialNo And enableBatchwiseTr Then
                _srchTxtId = 3
                _srchOnce = False
                strGridSrchString = ""
                chgbyprg = True
                If grdVoucher.Item(ConstSerialNo, grdVoucher.CurrentRow.Index).Value <> "" Then
                    strGridSrchString = grdVoucher.Item(ConstSerialNo, grdVoucher.CurrentRow.Index).Value
                Else
                    strGridSrchString = ""
                End If
                ShowPanel(True)
                chgbyprg = False
            Else
                If grdVoucher.Item(e.ColumnIndex, e.RowIndex).ReadOnly = False Then grdBeginEdit()
            End If
        Else
            If grdVoucher.Item(e.ColumnIndex, e.RowIndex).ReadOnly = False Then grdBeginEdit()
        End If

    End Sub
    Private Sub calculateTaxFromUnitPrice(ByVal i As Integer, ByVal cindex As Integer)
        If chgUprice And chkcal.Checked Then
            chgbyprg = True
            Dim taxper As Double
            With grdVoucher
                Dim dt As DataTable
                dt = _objcmnbLayer._fldDatatable("SELECT * FROM GSTTb WHERE HSNCODE='" & Trim(.Item(ConstBarcode, i).Value & "") & "'")
                If dt.Rows.Count > 0 Then
                    taxper = CDbl(dt(0)("CGST")) + CDbl(dt(0)("SGST"))
                End If
                If enablecess Then
                    dt = _objcmnbLayer._fldDatatable("SELECT isnull(vat,0)vat FROM InvItm " & _
                                                " LEFT JOIN VatMasterTb ON InvItm.vatid=VatMasterTb.vatid " & _
                                                "WHERE itemid=" & Val(.Item(ConstItemID, i).Value & ""))
                    If dt.Rows.Count > 0 Then
                        taxper = taxper + CDbl(dt(0)("vat"))
                    End If
                End If
                .Item(cindex, i).Value = (CDbl(.Item(cindex, i).Value) * 100) / (taxper + 100)
                .Item(cindex, i).Value = Format(.Item(cindex, i).Value, lnumformat)
            End With
        End If
        chgUprice = False
        chgbyprg = False
    End Sub
    Public Sub loadStockAdjFormLoadMasters()
        Dim qry As String
        qry = "SELECT WarrentyName FROM WarrentyMasterTb"
        qry = qry & " SELECT SManCode FROM SalesmanTb"
        qry = qry & " SELECT CurrencyCode FROM CurrencyTb"
        qry = qry & " SELECT LocCode FROM LocationTb"
        Dim dtset As DataSet
        dtset = _objcmnbLayer._ldDataset(qry, False)
        If EnableWarranty Then
            dtwarrenty = dtset.Tables(0)
        End If
        dtsalesman = dtset.Tables(1)

        dtcurrentyTb = dtset.Tables(2)
        dtlocationTb = dtset.Tables(3)
        
    End Sub

    Private Sub btnimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnimport.Click
        isImport = True
        fSlctDoc = New SelectInvTr
        With fSlctDoc
            .strType = cmbimporttr.Text
            .Text = "Select Transaction"
            .cmbbranch.Visible = True
            .branch = cmbtolocation.Text
            .cmbbranch.Tag = 1
            .Label1.Visible = True
            .isForLocationTransfer = chkforlocation.Checked
            .ShowDialog()
        End With
        If Not fSlctDoc Is Nothing Then fSlctDoc = Nothing
    End Sub

    Private Sub btnlocqty_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnlocqty.Click
        If grdVoucher.CurrentRow Is Nothing Then Exit Sub
        showlocationwise(grdVoucher.CurrentRow.Index)
    End Sub
    Private Sub showlocationwise(ByVal rowindex As Integer)
        If chgbyprg Then Exit Sub
        Dim litemid As Long
        litemid = Val(grdVoucher.Item(ConstItemID, rowindex).Value)
        If fshowlocationqty Is Nothing Then fshowlocationqty = New ShowLocationQtyFrm
        With fshowlocationqty
            .loadLOCQty(litemid)
            If .Visible = False Then
                .Show()
                .Top = Me.Top + Screen.PrimaryScreen.WorkingArea.Height - .Height - 40
                .Left = Me.Left + Screen.PrimaryScreen.WorkingArea.Width - .Width - 10
            End If
        End With
    End Sub
    Private Sub fshowlocationqty_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fshowlocationqty.FormClosed
        fshowlocationqty = Nothing
    End Sub
    Private Sub fwait_triggerEvent() Handles fwait.triggerEvent
        Select Case fwait.triggerType
            Case 1
                saveTrans()
            Case 2
                ldPostedInv()
            Case 3
                isImport = True
                PasteFrom(FrTrId)
                isImport = False
        End Select
        If Not fwait Is Nothing Then
            fwait.Close()
            fwait = Nothing
        End If

    End Sub
    Private Sub loadWaite(ByVal triggerType As Integer)
        If Not fwait Is Nothing Then fwait.Close() : fwait = Nothing
        fwait = New WaitMessageFrm
        With fwait
            .triggerType = triggerType
            .Show()
        End With
    End Sub

    Private Sub txtPurchaseName_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPurchaseName.Validated
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
        chgbyprg = True
        Dim myctrl As TextBox
        myctrl = sender
        myctrl.Text = UCase(myctrl.Text)
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT AccMast.accid,Alias,AccDescr from AccMast where Alias='" & txtPurchAlias.Text & "'")
        If dt.Rows.Count > 0 Then
            txtPurchaseName.Tag = dt(0)("accid")
            txtDescr.Focus()
        Else
            txtPurchaseName.Tag = 0
            txtPurchaseName.Text = ""
            txtPurchAlias.Text = ""
            txtPurchaseName.Focus()
        End If

        chgbyprg = False
    End Sub

    Private Sub chkforlocation_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkforlocation.Click
        _objcmnbLayer._saveDatawithOutParm("Update CompanyTb set setlocationtransferInSTADJ=" & IIf(chkforlocation.Checked, 1, 0))
    End Sub

    Private Sub ldtimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ldtimer.Tick
        If chgbyprg Then Exit Sub
        ldtimer.Enabled = False
        chgbyprg = True
        ShowPanel()
        chgItm = True
        chgbyprg = False
        grdVoucher.Item(ConstqtyChg, grdVoucher.CurrentRow.Index).Value = "Chg"
    End Sub

    Private Sub chkcal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkcal.CheckedChanged

    End Sub
End Class