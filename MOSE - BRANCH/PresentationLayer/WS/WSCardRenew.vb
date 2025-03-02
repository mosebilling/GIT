Public Class WSCardRenew
    Private _objcmnbLayer As New clsCommon_BL
    Private _objInv As New clsInvoice
    Private _objTr As New clsAccountTransaction
    Public isModi As Boolean
    Private CGSTCAc As Integer
    Private SGSTCAc As Integer
    Public Event printdocs(ByVal trid As Long)
    Private WithEvents fCashCust As CreateCashCustomerFrm
    Private WithEvents frm As New ReprtviewNEWfrm
    Private itemname As String
    Private chgbypgm As Boolean
    Private WithEvents fRptFormat As RptFormatfrm

    Private Sub generatenewNumber()
        Dim PreFixTb As DataTable
        PreFixTb = _objcmnbLayer._fldDatatable("SELECT * FROM InvNos WHERE InvType='DIS'", False)
        If PreFixTb.Rows.Count > 0 Then
            numVchrNo.Text = Val(PreFixTb.Rows(0)("InvNo"))
        Else
            numVchrNo.Text = 1
        End If
        txtprefix.Text = Trim(PreFixTb.Rows(0)("Prefix") & "")
    End Sub
    Private Sub clearControl()
        txtReference.Text = ""
        cldrdate.Value = Date.Now
        txtcustomer.Text = ""
        txtaddress.Text = ""

        txtledger.Text = ""
        txtledger.Tag = ""
        txtcustomer.Tag = ""
        cmbcard.Text = ""
        lblcardtype.Text = ""
        lblcardtype.Tag = ""
        lblRservice.Text = ""
        lbllastplatenumber.Text = ""
        lbllastservicedate.Text = ""
        lblservice.Text = ""
        lblhsncode.Text = ""
        grdVoucher.DataSource = Nothing
        txtAmount.Text = ""
        txtdiscount.Text = Format(0, numFormat)
        lblgstper.Text = "0%"
        lblgstper.Tag = ""
        numVchrNo.Tag = ""
        txtPPrefix.Text = ""
        numPrintVchr.Text = ""
        txtgst.Text = Format(0, numFormat)
        txtnetamount.Text = Format(0, numFormat)
        getdefaultaccounts()
        generatenewNumber()
    End Sub
    Private Sub addNew()
        clearControl()
        generatenewNumber()
    End Sub
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub loadcardtype()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT cardtypename FROM CardtypemasterTb")
        cmbcard.Items.Clear()
        cmbcard.Items.Add("")
        Dim i As Integer
        For i = 0 To dt.Rows.Count - 1
            cmbcard.Items.Add(dt(i)("cardtypename"))
        Next
    End Sub
    Private Sub loadcustomerAccount()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT AccDescr,customeraccount FROM CashCustomerTb " & _
                                         "LEFT JOIN AccMast ON AccMast.accid=CashCustomerTb.customeraccount " & _
                                         " where custid=" & Val(txtcustomer.Tag))
        If dt.Rows.Count > 0 Then
            txtledger.Text = Trim(dt(0)("AccDescr") & "")
            txtledger.Tag = dt(0)("customeraccount")
        End If

    End Sub
    Private Sub getdefaultaccounts()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT AccDescr,Accid from AccMast where AccSetId like '%12%'")
        If dt.Rows.Count > 0 Then
            txtcredit.Text = dt(0)("AccDescr")
            txtcredit.Tag = dt(0)("Accid")
        End If
        dt = _objcmnbLayer._fldDatatable("SELECT AccDescr,Accid from AccMast where AccSetId like '%11%'")
        If dt.Rows.Count > 0 Then
            txtdebit.Text = dt(0)("AccDescr")
            txtdebit.Tag = dt(0)("Accid")
        End If
    End Sub
    Private Sub loadcarddetails()
        Dim dt As DataTable
        Dim _objvehicle As New clsVechicle
        dt = _objvehicle.returnWSCardHistory(2, cmbcard.Text, 0, 0)
        If dt.Rows.Count > 0 Then
            lblRservice.Text = dt(0)("totalrenewal") - dt(0)("totalservices")
            lbllastplatenumber.Text = Trim(dt(0)("platenumber") & "")
            If Not IsDBNull(dt(0)("trdate")) Then
                lbllastservicedate.Text = DateValue(dt(0)("trdate"))
            Else
                lbllastservicedate.Text = ""
            End If
        End If
    End Sub
    Private Sub cardtypedetails()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT CardtypemasterTb.cardtypeid,cardtypename, services,isnull(Amount,0) Amount," & _
                                         "GSTTb.HSNCode,GSTName,isnull(CGST,0) CGST,isnull(SGST,0) SGST,isnull(SGSTCAc,0)SGSTCAc,isnull(CGSTCAc,0)CGSTCAc FROM " & _
                                         "CardtypemasterTb LEFT JOIN GSTTb ON GSTTb.gstid=CardtypemasterTb.gstid " & _
                                         "LEFT JOIN CardmasterTb ON CardmasterTb.cardtypeid=CardtypemasterTb.cardtypeid " & _
                                         "where cardnumber='" & cmbcard.Text & "'")
        Dim gstper As Double
        If dt.Rows.Count > 0 Then
            lblservice.Text = dt(0)("services")
            txtAmount.Text = Format(dt(0)("Amount"), numFormat)
            gstper = dt(0)("CGST") + dt(0)("SGST")
            lblgstper.Text = gstper & " %"
            lblgstper.Tag = gstper
            SGSTCAc = dt(0)("SGSTCAc")
            CGSTCAc = dt(0)("CGSTCAc")
            lblcardtype.Tag = dt(0)("cardtypeid")
            lblcardtype.Text = dt(0)("cardtypename")
            lblhsncode.Text = Trim(dt(0)("HSNCode") & "")
        End If
        calculate()
        dt = _objcmnbLayer._fldDatatable("SELECT Description [Item Name],discpercentage [Dis%] FROM " & _
                                         "CardItemTb LEFT JOIN InvItm ON CardItemTb.itemid=invitm.itemid where cardtypeid='" & lblcardtype.Tag & "'")
        grdVoucher.DataSource = dt
        With grdVoucher
            SetGridProperty(grdVoucher)

            resizeGridColumn(grdVoucher, 0)
        End With
    End Sub

    Private Sub cmbcardtype_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbcard.SelectedIndexChanged
        cardtypedetails()
        loadcarddetails()
    End Sub
    Private Sub saveTrans()
        Dim TrId As Long
chkagain:
        If Not isModi Then
            If Not CheckNoExists(txtprefix.Text, Val(numVchrNo.Text), "DIS", "Inventory") Then
                If MsgBox("Document No alreary exist! Do you want to fill next?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Exit Sub
                Else
                    numVchrNo.Text = Val(numVchrNo.Text) + 1
                    GoTo chkagain
                End If
            End If
        Else
            TrId = Val(numVchrNo.Tag)
        End If
        Dim itemid As Long
        itemid = checkWSCarditem()
        setInvCmnValue(TrId)
        TrId = Val(_objInv._saveCmn())
        numVchrNo.Tag = TrId
        _objcmnbLayer._saveDatawithOutParm("UPDATE ItmInvCmnTb SET CashCustName='" & txtcustomer.Text & "',CashCustid=" & Val(txtcustomer.Tag) & ",optID=1 WHERE TRID=" & TrId)
        setInvDetValue(TrId, 1, itemid)
        _objInv._saveDetails()
        'If enableRealtimeCosting Then
        '    _objInv.ItemId = itemid
        '    _objInv.TrDate = DateValue(cldrdate.Value)
        '    _objInv.setcostAverageOnModification(UsrBr)
        'End If
        updateaccounts(TrId)
    End Sub
    Private Sub updateaccounts(ByVal trid As Long)
        Dim LinkNo As Long
        Dim dtTable As DataTable
        If isModi And TrId > 0 Then
            dtTable = _objcmnbLayer._fldDatatable("SELECT LinkNo FROM AccTrCmn WHERE lnkno  = " & TrId)
            If dtTable.Rows.Count > 0 Then
                LinkNo = dtTable(0)("LinkNo")
                _objcmnbLayer._saveDatawithOutParm("DELETE FROM AccTrDet WHERE LinkNo=" & LinkNo)
            End If
        End If
        setAcctrCmnValue(trid, LinkNo)
        LinkNo = 0
        LinkNo = Val(_objTr.SaveAccTrCmn())

        'debit entry
        setAcctrDetValue(LinkNo, Val(txtdebit.Tag), CDbl(txtAmount.Text), "Discount Card Sale")
        _objTr.saveAccTrans()
        If EnableGST And Val(txtgst.Text) > 0 And CGSTCAc > 0 And SGSTCAc > 0 Then
            'tax entry
            setAcctrDetValue(LinkNo, CGSTCAc, CDbl(txtgst.Text) / 2, "Tax Collected from " & txtcustomer.Text)
            _objTr.saveAccTrans()
            setAcctrDetValue(LinkNo, SGSTCAc, CDbl(txtgst.Text) / 2, "Tax Collected from " & txtcustomer.Text)
            _objTr.saveAccTrans()
        End If
        'credit entry
        setAcctrDetValue(LinkNo, Val(txtledger.Tag), (CDbl(txtAmount.Text) + CDbl(txtgst.Text)) * -1, "Discount Card Sale")
        _objTr.saveAccTrans()
        updateClosingBalanceForInvoice(trid)
    End Sub
    Private Sub setAcctrCmnValue(ByVal InvTrid As Long, ByVal LinkNo As Long)
        _objTr.JVType = "DIS"
        _objTr.JVDate = DateValue(cldrdate.Value)
        _objTr.PreFix = Trim(txtprefix.Text)
        _objTr.JVNum = Val(numVchrNo.Text)
        _objTr.JVTypeNo = getVouchernumber("DIS")
        _objTr.UserId = Trim(CurrentUser & "")
        _objTr.MchName = MACHINENAME
        _objTr.CrtDtTm = Date.Now
        _objTr.TypeNo = getVouchernumber("DIS")
        _objTr.IsModi = IIf(LinkNo > 0, 1, 0)
        _objTr.LinkNo = InvTrid
    End Sub
    Private Sub setAcctrDetValue(ByVal lnkNo As Long, ByVal AccountNo As Integer, ByVal amount As Double, ByVal EntryRef As String)
        With _objTr
            .trLinkNo = lnkNo
            .AccountNo = AccountNo
            .Reference = Trim(txtprefix.Text) & IIf(txtprefix.Text = "", "", "/") & numVchrNo.Text
            .EntryRef = EntryRef
            .DealAmt = amount
            .JobCode = ""
            .JobStr = ""
            .CurrRate = 1
            .CurrencyCode = "" ' IIf(chkFC.Checked = True, txtFC.Text, "")
            .TrInf = 0
            .OthCost = 0
            .TermsId = 0
            .CustAcc = 0
            .AccWithRef = ""
            .LPONo = txtReference.Text
            Dim dtLPO As Date = IIf(chkDate(cldrdate.Value), cldrdate.Value, DateValue(cldrdate.Value))
            Dim dtDue As Date = DateValue(Date.Now)
            Dim dtSup As Date = DateValue(cldrdate.Value)
            .DocDate = dtLPO
            .SuppInvDate = dtSup
            .DueDate = dtDue
        End With
    End Sub
    Private Function checkWSCarditem() As Long
        Dim dt As DataTable
chkagain:
        dt = _objcmnbLayer._fldDatatable("SELECT Itemid,Description from invitm where [Item Code] ='WSCard'")
        If dt.Rows.Count > 0 Then
            itemname = dt(0)("Description")
            Return Val(dt(0)("itemid"))
        Else
            _objcmnbLayer._saveDatawithOutParm("Insert into invitm([Item Code],Description,itemCategory,donotchange) values('WSCard','Discount Card Item (WS)','Service',1)")
            GoTo chkagain
        End If
        Return 0
    End Function
    Private Sub setInvCmnValue(ByVal InvTrid As Long)
        With _objInv
            .TrId = InvTrid
            .TrDate = DateValue(cldrdate.Value)
            .TrType = "DIS"
            .DocLstTxt = ""
            .InvTypeNo = 0
            .SlsManId = ""
            .Prefix = Trim(txtprefix.Text)
            .InvNo = Val(numVchrNo.Text)
            .TrRefNo = Trim(txtprefix.Text) & IIf(txtprefix.Text = "", "", "/") & numVchrNo.Text ' Trim(txtReference.Text)
            .CSCode = Val(txtledger.Tag)
            .PSAcc = Val(txtcredit.Tag)
            .JobCode = ""
            .ImpDoc = 0
            .UserId = CurrentUser
            .IsFC = False
            .FCRate = 1
            .NFraction = 2
            .FC = ""
            .Discount = CDbl(txtdiscount.Text)
            .TrDescription = ""
            .TypeNo = getVouchernumber("DIS")
            .EnaJob = False
            .DocDefLoc = ""
            .BrId = ""
            .OthCost = 0
            .Discount1 = 0
            .NetAmt = CDbl(txtAmount.Text)
            .LPO = txtReference.Text
            Dim dt As Date = getServerDate()
            .CrtDt = Dt
            .DelDate = Dt
            .DueDate = DateValue(Date.Now)
            .DocDate = Dt
            .SuppInvDate = Dt
            .TermsId = ""
            .MchName = MACHINENAME
            .isModi = isModi
            .lpoclass = ""
            .rndoff = 0
            'If TaxType is 1 then invoice is interstate invoice otherwise intrastate invoice
            .TaxType = 0
            .OthrCust = txtaddress.Text
            _objInv.disccardid = Val(lblcardtype.Tag)
        End With

    End Sub
    Private Sub setInvDetValue(ByVal Invid As Long, ByVal PPerU As Single, ByVal itemid As Integer)
        Dim FCRt As Integer
        FCRt = 1
        _objInv.dtTrId = Invid
        _objInv.ItemId = itemid
        _objInv.Unit = ""
        _objInv.TrQty = 1
        _objInv.UnitCost = CDbl(txtAmount.Text)
        _objInv.taxP = Val(lblgstper.Tag)
        _objInv.taxAmt = CDbl(txtgst.Text)
        _objInv.PFraction = PPerU
        _objInv.UnitOthCost = 0
        _objInv.Method = 1
        _objInv.UnitDiscount = CDbl(txtdiscount.Text)
        _objInv.ItemDiscount = 0
        _objInv.DisP = 0
        _objInv.IDescription = itemname
        _objInv.SlNo = 1
        _objInv.TrTypeNo = getVouchernumber("DIS")
        _objInv.TrDateNo = getDateNo(CDate(cldrdate.Value))
        _objInv.TrType = "DIS"
        _objInv.id = 0
        _objInv.WarrentyName = ""
        _objInv.SerialNo = cmbcard.Text
        _objInv.WarrentyExpDate = DateValue("01/01/1950")
        _objInv.HSNCode = lblhsncode.Text
        _objInv.IGSTP = Val(lblgstper.Tag)
        _objInv.IGSTAmt = CDbl(txtgst.Text)
        _objInv.CSGTP = Val(lblgstper.Tag) / 2
        _objInv.CGSTAMT = CDbl(txtgst.Text) / 2
        _objInv.SGSTP = Val(lblgstper.Tag) / 2
        _objInv.SGSTAmt = CDbl(txtgst.Text) / 2
        _objInv.Smancode = ""
        _objInv.StartingReading = 0
        _objInv.EndingReading = 0
        _objInv.MeterCode = 0
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        If cmbcard.Text = "" Then
            MsgBox("Invalid Card number", MsgBoxStyle.Exclamation)
            cmbcard.Focus()
            Exit Sub
        End If
        If txtdebit.Text = "" Then
            MsgBox("Invalid Debit A/C! Please set it in company settings", MsgBoxStyle.Exclamation)
            txtdebit.Focus()
            Exit Sub
        End If
        'If txtcredit.Text = "" Then
        '    MsgBox("Invalid Credit A/C! Please set it in company settings ", MsgBoxStyle.Exclamation)
        '    txtcredit.Focus()
        '    Exit Sub
        'End If
        If txtledger.Text = "" Then
            MsgBox("Invalid Customer Ledger A/C", MsgBoxStyle.Exclamation)
            txtledger.Focus()
            Exit Sub
        End If
        saveTrans()
        MsgBox("Card Updated", MsgBoxStyle.Information)
        txtPPrefix.Text = txtprefix.Text
        numPrintVchr.Text = numVchrNo.Text
        SetNextVrNo(numVchrNo, 0, "DIS", "TrType = 'DIS' AND InvNo = ", False, True, True)
        If MsgBox("Do you want to print?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
            PrepareRpt("DIS", True)
        End If
        btnupdate.Enabled = False
        cmbcard.Enabled = False
        'txtcardnumber.Enabled = False
        chkcredit.Enabled = False
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If MsgBox("Do you want to remove current sales invoice", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        _objInv.TrId = Val(numVchrNo.Tag)
        _objInv.TrType = "OUT"
        _objInv.deleteInventoryTransactions()
        MsgBox("Invoice Deleted", MsgBoxStyle.Information)
        Me.Close()
    End Sub

    Private Sub WSCardRenew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        addNew()
        'loadcustomerANDcardetails()
        loadcardtype()
        getdefaultaccounts()
    End Sub

    Private Sub txtcustomer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtcustomer.KeyDown
        If e.KeyCode = Keys.F2 Then
            fCashCust = New CreateCashCustomerFrm
            fCashCust.ShowDialog()
            fCashCust = Nothing
        End If
        
    End Sub
    Private Sub fCashCust_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fCashCust.FormClosed
        fCashCust = Nothing
    End Sub

    Private Sub fCashCust_selectcust(ByVal custname As String, ByVal custaddress As String, ByVal Cashcustid As Long, ByVal cardnumber As String, ByVal phone As String, ByVal gstn As String) Handles fCashCust.selectcust
        txtcustomer.Text = custname
        txtaddress.Text = custaddress
        txtcustomer.Tag = Cashcustid
        loadcustomercards()
        cmbcard.Text = cardnumber
        loadcustomerAccount()
    End Sub
    Private Sub loadcustomercards()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT cardnumber from CardmasterTb where customerid=" & Val(txtcustomer.Tag))
        Dim i As Integer
        cmbcard.Items.Clear()
        For i = 0 To dt.Rows.Count - 1
            cmbcard.Items.Add(dt(i)("cardnumber"))
        Next
    End Sub

    Private Sub txtdiscount_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtdiscount.KeyDown
        If e.KeyCode = Keys.Return Then
            btnupdate.Focus()
        End If
    End Sub

    Private Sub txtdiscount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtdiscount.KeyPress
        NumericTextOnKeypress(txtdiscount, e, chgbypgm, numFormat)
    End Sub

    Private Sub txtdiscount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtdiscount.TextChanged
        calculate()
    End Sub
    Private Sub calculate()
        If Val(txtdiscount.Text) = 0 Then txtdiscount.Text = 0
        If Val(txtAmount.Text) = 0 Then txtAmount.Text = 0
        'txtgst.Text = Format((CDbl(txtAmount.Text) - CDbl(txtdiscount.Text)) * Val(lblgstper.Tag) / 100, numFormat)
        txtgst.Text = 0
        txtnetamount.Text = Format(CDbl(txtAmount.Text) - CDbl(txtdiscount.Text) + CDbl(txtgst.Text), numFormat)
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        clearControl()
    End Sub
    Private Sub PrepareRpt(ByVal RptType As String, Optional ByVal Forprint As Boolean = False, Optional ByVal pno As Integer = 0)

        If Val(numVchrNo.Text) = 0 Then
            MsgBox("Enter a valid Voucher Number !!", vbInformation)
            numVchrNo.Focus()
            Exit Sub
        End If
        Dim RptName As String
        Dim RptCaption As String = ""
        RptName = getRptDefFlName(RptType, RptCaption)
        If Trim(RptName) <> "" Then
            LoadReport(RptName, RptCaption, Forprint, pno)
        End If
    End Sub
    Private Sub LoadReport(ByVal FileName As String, ByVal RptCaption As String, Optional ByVal ToPrint As Boolean = False, Optional ByVal pno As Integer = 0)
        If Not frm Is Nothing Then
            frm.Close()
            frm = New ReprtviewNEWfrm
        Else
            frm = New ReprtviewNEWfrm
        End If
        _objInv.Prefix = txtPPrefix.Text
        _objInv.InvNo = Val(numPrintVchr.Text)
        _objInv.TrType = "DIS"

        Dim ds As DataSet
        ds = _objInv.ldInvoice("rturnInventoryDetailsForInvoicePrint", pno)
        If ToPrint Then
            _objcmnbLayer._saveDatawithOutParm("Update ItmInvCmnTb set Printed=1 where trid=" & Val(ds.Tables(0)(0)("trid")))
        End If
        frm.SetReport(ds, FileName, 0, False, , ToPrint)
        frm.MdiParent = Me.MdiParent
        frm.Text = RptCaption
        If Not ToPrint Then frm.Show()
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        fRptFormat = New RptFormatfrm
        fRptFormat.RptType = "DIS"
        fRptFormat.btnView.Text = "Print"
        fRptFormat.ShowDialog()
        fRptFormat = Nothing
    End Sub

    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        LoadReport(RptFlName, RptCaption, True)
    End Sub

    Private Sub txtcustomer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcustomer.TextChanged

    End Sub
End Class