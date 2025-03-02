
Public Class WSCardSalesInvoice

#Region "Public Variables"
    Public isModi As Boolean
    Public isCust As Boolean
#End Region
#Region "Local Variables"
    Private flnumformat As String = "#,##0.000"
    Private chgbyprg As Boolean
    Private chgbyprgN As Boolean
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
    Private isImport As Boolean
    Private lnumFormat As String
    Private dtTax As DataTable
    Private chgUprice As Boolean
    Private chgNumByPgm As Boolean
    Private dtTb As DataTable
    Private dtItemInfo As DataTable
    Private optid As Integer



    'numeric text
    Private idx As Integer
    Private str1 As String
    Private str2 As String
    Private str3 As String
    Private SelStart As Integer
    Private numCtrl As TextBox

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
    Private ActBr As String
    Private isprotected As Boolean
    Private dtMultipleDebits As DataTable
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
    Private WithEvents fCashCust As CreateCashCustomerFrm
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
#Region "GridConstantVariables"
    Private Const ConstSlNo = 0
    Private Const ConstItemCode = 1
    Private Const ConstBarcode = 2 'HSN Code
    Private Const ConstDescr = 3
    Private Const ConstCardtype = 4 'WARRENTY COLUMN
    Private Const ConstCardNumber = 5
    Private Const ConstQty = 6
    Private Const ConstUPrice = 7
    Private Const ConstDisP = 8
    Private Const ConstDisAmt = 9
    Private Const constItmTot = 10
    Private Const ConstTaxP = 11
    Private Const ConstTaxAmt = 12
    Private Const ConstLTotal = 13
    Private Const ConstUnitOthCost = 14
    Private Const ConstNUPrice = 15
    Private Const ConstActualOthCost = 16
    Private Const ConstMthd = 17
    Private Const ConstUnitVal = 18
    Private Const ConstDiscOther = 19
    Private Const ConstJob = 20
    Private Const ConstJobCostAc = 21
    Private Const ConstBcodeOrICode = 22
    Private Const ConstImpLnId = 23
    Private Const ConstImpDocId = 24
    Private Const ConstActualPrice = 25
    Private Const ConstJobAcID = 26
    Private Const ConstPMult = 27
    Private Const ConstPFraction = 28
    Private Const ConstItemID = 29
    Private Const ConstBaseID = 30
    Private Const ConstLrow = 31
    Private Const ConstId = 32
    Private Const ConstqtyChg = 33
    Private Const ConstCGSTP = 34
    Private Const ConstCGSTAmt = 35
    Private Const ConstSGSTP = 36
    Private Const ConstSGSTAmt = 37
    Private Const ConstIGSTP = 38
    Private Const ConstIGSTAmt = 39
    Private Const ConstIsSerial = 40
    Private Const ConstIsManufacturingItem = 41
    Private Const ConstB = 42
    Private Const ConstUnit = 43
    Private Const Constsman = 44
    Private Const ConstStartReading = 45
    Private Const ConstSerialNo = 46
    Private Const ConstMeterCode = 47
    Private Const ConstWarrentyExpiry = 48
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
            InvList = _objcmnbLayer._fldDatatable("SELECT TrId FROM ItmInvCmnTb WHERE  TrId = " & FromTrId)
        Else
            InvList = _objcmnbLayer._fldDatatable("SELECT TrId FROM ItmInvCmnTb WHERE TrType = 'DIS' AND InvNo = " & Val(numVchrNo.Text) & " AND PreFix = '" & MkDbSrchStr(Trim(txtprefix.Text), True) & "'")
        End If
        If InvList.Rows.Count > 0 Then
            If Not isImport Then
                loadedTrId = InvList(0)("TrId")
                InvList = Nothing
                ldPostedInv()
                isModi = True
            Else
                isImport = False
                PasteFrom(InvList(0)("TrId"))
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
        getItemInfo(0)
        Dim ItmInvCmnTb As DataTable
        Dim DocAssgnTb As DataTable
        Dim sRs As DataTable
        'Dim Discount As Double
        Dim UPerPack As Double
        Dim Protect As Boolean
        Dim CrossBr As Boolean
        ItmInvCmnTb = _objcmnbLayer._fldDatatable("SELECT ItmInvCmnTb.*,jobname FROM ItmInvCmnTb LEFT JOIN JobTb ON ItmInvCmnTb.[Job Code]=JobTb.jobcode WHERE TrId = " & loadedTrId & " AND TrType = 'DIS'")
        chgbyprg = True
        ActBr = ""
        If ItmInvCmnTb.Rows.Count = 0 Then GoTo Quit
        getProtectUntil()
        cmbVoucherTp.Text = getVoucherName(Val(ItmInvCmnTb(0)("InvTypeNo") & ""))
        cldrdate.Value = Format(ItmInvCmnTb(0)("TrDate"), DtFormat) ' "MM/dd/yyyy")
        clrDuedate.Value = Format(ItmInvCmnTb(0)("DueDate"), DtFormat)
        txtprefix.Text = ItmInvCmnTb(0)("PreFix")
        txtPPrefix.Text = txtprefix.Text
        txtprefix.Tag = ItmInvCmnTb(0)("PreFix")
        numVchrNo.Text = ItmInvCmnTb(0)("InvNo") 'Val(Mid(!InvNo, 3)) '!InvNo '
        numVchrNo.Tag = ItmInvCmnTb(0)("InvNo")
        numPrintVchr.Text = ItmInvCmnTb(0)("InvNo")  'CMNREC(12).FormattedText
        txtJob.Text = Trim("" & ItmInvCmnTb(0)("Job Code"))
        txtjobname.Text = Trim(ItmInvCmnTb(0)("jobname") & "")
        cmbsalesman.Text = Trim(ItmInvCmnTb(0)("SlsManId") & "")
        txtCashCustomer.Text = Trim(ItmInvCmnTb(0)("CashCustName") & "")
        txtCashCustomer.Tag = Val(ItmInvCmnTb(0)("CashCustid") & "")
        optid = Val(ItmInvCmnTb(0)("optid") & "")
        If optid = 1 Then
            lblhead.Text = "Card Renew"
        Else
            lblhead.Text = "Card Sales"
        End If
        cmbfc.Text = ItmInvCmnTb(0)("FC")
        If Not IsDBNull(ItmInvCmnTb(0)("Discount1")) Then
            txtsmanP.Text = Format(CDbl(ItmInvCmnTb(0)("Discount1")), lnumFormat)
        Else
            txtsmanP.Text = Format(0, lnumFormat)
        End If
        If Not IsDBNull(ItmInvCmnTb(0)("rndoff")) Then
            If Val(ItmInvCmnTb(0)("rndoff")) > 0 Then
                cmbsign.Text = "+"
            Else
                cmbsign.Text = "-"
                ItmInvCmnTb(0)("rndoff") = ItmInvCmnTb(0)("rndoff") * -1
            End If
            txtroundOff.Text = Format(CDbl(ItmInvCmnTb(0)("rndoff")), lnumFormat)
        Else
            txtroundOff.Text = Format(0, lnumFormat)
        End If
        setCustomer(ItmInvCmnTb(0)("CSCode"))
        txtcustAddress.Text = Trim(ItmInvCmnTb(0)("OthrCust") & "")
        txtfcrt.Text = Format(ItmInvCmnTb(0)("FcRate"), lnumFormat)
        FCRt = ItmInvCmnTb(0)("FcRate")
        sRs = _objcmnbLayer._fldDatatable("SELECT * FROM AccMast WHERE accid = " & ItmInvCmnTb(0)("PSAcc"))

        If sRs.Rows.Count > 0 Then
            txtPurchAlias.Tag = ItmInvCmnTb(0)("PSAcc")
            txtPurchAlias.Text = sRs(0)("Alias")
            txtPurchaseName.Text = sRs(0)("AccDescr")
        ElseIf txtPurchAlias.Text <> "" Then
            sRs = _objcmnbLayer._fldDatatable("SELECT * FROM AccMast WHERE alias = '" & txtPurchAlias.Text & "'")
            If sRs.Rows.Count > 0 Then
                txtPurchAlias.Tag = sRs(0)("accid")
                txtPurchAlias.Text = sRs(0)("Alias")
                txtPurchaseName.Text = sRs(0)("AccDescr")
            End If
        End If

        txtReference.Text = Trim("" & ItmInvCmnTb(0)("LPO"))
        txtDescr.Text = Trim("" & ItmInvCmnTb(0)("TrDescription"))
        chgNumByPgm = True
        numDisc.Text = Format(ItmInvCmnTb(0)("Discount") / FCRt, lnumFormat)
        chgNumByPgm = False
        '-----------------------for Other Info ------------------------
        'If Not fOthInf Is Nothing Then FillOthInf()
        'If Not IsDBNull(ItmInvCmnTb(0)("SuppInvDate")) Then dtSuppDate.CtlText = DateValue(ItmInvCmnTb(0)("SuppInvDate"))
        On Error GoTo 0
        LddImpDocs = ""
        CTVol = IIf(IsDBNull(ItmInvCmnTb(0)("InvTtlVol")), 0, ItmInvCmnTb(0)("InvTtlVol"))
        CTWt = IIf(IsDBNull(ItmInvCmnTb(0)("InvTtlWt")), 0, ItmInvCmnTb(0)("InvTtlWt"))
        CTQty = IIf(IsDBNull(ItmInvCmnTb(0)("InvTtlQty")), 0, ItmInvCmnTb(0)("InvTtlQty"))
        OthCost = Format(Val(ItmInvCmnTb(0)("OthCost")), lnumFormat)
        Protect = (ItmInvCmnTb(0)("TrDate") <= ProtectUntil)
        If sRs.Rows.Count > 0 Then sRs.Clear()
        sRs = _objcmnbLayer._fldDatatable("SELECT ItmInvTrTb.*, [Item Code],Warrentyname,isSerialNo,fcode,isnull(FraCount,0)FraCount,ISNULL(cardtypename,'')cardtypename FROM ItmInvTrTb LEFT JOIN InvItm ON InvItm.ItemId = ItmInvTrTb.ItemId " & _
                                          " LEFT JOIN InvItmPropertiesTb ON InvItmPropertiesTb.Itemid=invitm.itemid " & _
                                          "LEFT JOIN WarrentyMastertb ON WarrentyMastertb.Wid=ItmInvTrTb.WarrentyId " & _
                                          "LEFT JOIN FuelMeterReadingTb ON FuelMeterReadingTb.fmeterid=ItmInvTrTb.FuleMeterid " & _
                                          "LEFT JOIN UnitsTb ON UnitsTb.Units=ItmInvTrTb.Unit " & _
                                          "LEFT JOIN CardtypemasterTb ON CardtypemasterTb.cardtypeid=ItmInvTrTb.disccardid " & _
                                          "WHERE TrId = " & loadedTrId & " ORDER BY SlNo")
        grdVoucher.RowCount = 0
        If _objcmnbLayer.dtSerialNo.Rows.Count > 0 Then _objcmnbLayer.dtSerialNo.Rows.Clear()
        With grdVoucher
            .Rows.Clear()
            If sRs.Rows.Count > 0 Then
                For i = 0 To sRs.Rows.Count - 1
                    grdVoucher.Rows.Add(1)
                    'grdVoucher.Item(ConstSlNo, i).Value = IIf(sRs(i)("MsgId") = 1, "M", IIf(sRs(i)("MsgId") = 2, "L", ""))
                    UPerPack = IIf(sRs(i)("PFraction") = 0, 1, sRs(i)("PFraction"))
                    grdVoucher.Item(ConstSlNo, i).Value = IIf(Val(sRs(i)("ItemId")) = 0, "M", "")
                    If grdVoucher.Item(ConstSlNo, i).Value <> "M" Then
                        UPerPack = IIf(sRs(i)("PFraction") = 0 Or IsDBNull(sRs(i)("PFraction")), 1, sRs(i)("PFraction"))
                        grdVoucher.Item(ConstItemCode, i).Value = Trim("" & sRs(i)("Item Code"))
                        grdVoucher.Item(ConstItemID, i).Value = sRs(i)("ItemId")
                        grdVoucher.Item(ConstPFraction, i).Value = sRs(i)("FraCount")
                        grdVoucher.Item(ConstPMult, i).Value = UPerPack 'IIf(IsNull(sRs!PFraCount), "2", sRs!PFraCount)
                    Else
                        grdVoucher.Item(ConstPFraction, i).Value = "2"
                        grdVoucher.Item(ConstPMult, i).Value = "1"
                        grdVoucher.Item(ConstItemID, i).Value = 0
                    End If
                    grdVoucher.Item(ConstDescr, i).Value = IIf(IsDBNull(sRs(i)("IDescription")), "", sRs(i)("IDescription"))
                    grdVoucher.Item(ConstB, i).Value = IIf(sRs(i)("Method") & "" = "", "B", Trim(sRs(i)("Method") & ""))
                    grdVoucher.Item(ConstUnit, i).Value = Trim("" & sRs(i)("Unit"))
                    If Val(sRs(i)("Taxp") & "") = 0 Then sRs(i)("Taxp") = 0
                    grdVoucher.Item(ConstTaxP, i).Value = Format(sRs(i)("Taxp"), lnumFormat)
                    If Val(sRs(i)("taxamt") & "") = 0 Then sRs(i)("taxamt") = 0
                    grdVoucher.Item(ConstTaxAmt, i).Value = Format(sRs(i)("taxamt") / FCRt, lnumFormat)
                    grdVoucher.Item(ConstCardtype, i).Value = Trim("" & sRs(i)("Warrentyname")) ' !Colour
                    grdVoucher.Item(ConstQty, i).Value = Format(sRs(i)("TrQty") / UPerPack, "#,##0" & IIf(Val(grdVoucher.Item(ConstPFraction, i).Value) = 0, "", "." & Strings.StrDup(CByte(grdVoucher.Item(ConstPFraction, i).Value), "0")))
                    grdVoucher.Item(ConstUPrice, i).Value = Format(sRs(i)("UnitCost") * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & lnumFormat)
                    grdVoucher.Item(ConstActualPrice, i).Value = sRs(i)("UnitCost") * UPerPack / FCRt
                    grdVoucher.Item(ConstLTotal, i).Value = Format((sRs(i)("TrQty") * sRs(i)("UnitCost")) * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & lnumFormat)
                    grdVoucher.Item(ConstUnitOthCost, i).Value = Format(sRs(i)("UnitOthCost") * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & lnumFormat)
                    grdVoucher.Item(ConstActualOthCost, i).Value = sRs(i)("UnitOthCost") * UPerPack / FCRt
                    grdVoucher.Item(ConstCardNumber, i).Value = sRs(i)("SerialNo")
                    'grdVoucher.Item(ConstMthd, i).Value = sRs(i)("Method")
                    'grdVoucher.Item(ConstUnitVal, i).Value = sRs(i)("UnitValue") * UPerPack / FCRt
                    'grdVoucher.Item(ConstDiscOther, i).Value = sRs(i)("DiscOther") * UPerPack / FCRt
                    grdVoucher.Item(ConstDiscOther, i).Value = sRs(i)("UnitDiscount") * UPerPack / FCRt
                    If Val(sRs(i)("DisP") & "") = 0 Then sRs(i)("DisP") = 0
                    grdVoucher.Item(ConstDisP, i).Value = Format(sRs(i)("DisP"), numFormat)
                    If Val(sRs(i)("ItemDiscount") & "") = 0 Then sRs(i)("ItemDiscount") = 0
                    grdVoucher.Item(ConstDisAmt, i).Value = Format(sRs(i)("ItemDiscount") * UPerPack / FCRt, lnumFormat)
                    grdVoucher.Item(ConstImpDocId, i).Value = sRs(i)("disccardid")
                    grdVoucher.Item(ConstCardtype, i).Value = sRs(i)("cardtypename")
                    'grdVoucher.Item(ConstImpLnId, i).Value = sRs(i)("ImpDocLnNo")


                    grdVoucher.Item(ConstBarcode, i).Value = sRs(i)("HSNCode")
                    grdVoucher.Item(ConstCGSTP, i).Value = sRs(i)("CSGTP")
                    grdVoucher.Item(ConstCGSTAmt, i).Value = sRs(i)("CGSTAMT")

                    grdVoucher.Item(ConstSGSTP, i).Value = sRs(i)("SGSTP")
                    grdVoucher.Item(ConstSGSTAmt, i).Value = sRs(i)("SGSTAmt")

                    grdVoucher.Item(ConstIGSTP, i).Value = sRs(i)("IGSTP")
                    grdVoucher.Item(ConstIGSTAmt, i).Value = sRs(i)("IGSTAmt")
                    If enableFuleBankInvoice Then
                        grdVoucher.Item(ConstMeterCode, i).Value = Trim(sRs(i)("fcode") & "")
                        If Val(sRs(i)("StartingReading") & "") = 0 Then sRs(i)("StartingReading") = 0
                        If Val(sRs(i)("EndingReading") & "") = 0 Then sRs(i)("EndingReading") = 0
                        grdVoucher.Item(ConstStartReading, i).Value = Format(sRs(i)("StartingReading"), "#,##0" & IIf(Val(grdVoucher.Item(ConstPFraction, i).Value) = 0, "", "." & Strings.StrDup(CByte(grdVoucher.Item(ConstPFraction, i).Value), "0")))
                        grdVoucher.Item(ConstCardNumber, i).Value = Format(sRs(i)("EndingReading"), "#,##0" & IIf(Val(grdVoucher.Item(ConstPFraction, i).Value) = 0, "", "." & Strings.StrDup(CByte(grdVoucher.Item(ConstPFraction, i).Value), "0")))
                    End If

                    If Not IsDBNull(sRs(i)("isSerialNo")) Then
                        grdVoucher.Item(ConstIsSerial, i).Value = IIf(sRs(i)("isSerialNo"), 1, 0)
                    Else
                        grdVoucher.Item(ConstIsSerial, i).Value = 0
                    End If

                    grdVoucher.Item(ConstId, i).Value = sRs(i)("id")
                    If Not IsDBNull(sRs(i)("WarrentyExpDate")) Then
                        If DateValue(sRs(i)("WarrentyExpDate")) > DateValue("01/01/1950") Then
                            grdVoucher.Item(ConstWarrentyExpiry, i).Value = sRs(i)("WarrentyExpDate")
                        End If
                    End If
                    grdVoucher.Item(Constsman, i).Value = Trim(sRs(i)("Smancode") & "")
                    If .Item(ConstSerialNo, i).Value <> "" Then
                        AddTodtSerialNo(.Item(ConstSerialNo, i).Value, Val(.Item(ConstItemID, .CurrentRow.Index).Value), .CurrentRow.Index, DateValue(.Item(ConstWarrentyExpiry, .CurrentRow.Index).Value), Val(.Item(ConstId, .CurrentRow.Index).Value))
                    End If
                Next

            End If
        End With
        If sRs.Rows.Count > 0 Then sRs.Clear()
        If enableFuleBankInvoice Then loadSalesMultipleDebits(loadedTrId)
        calculate()
        reArrangeNo()

        If Val(lblbalance.Text) > 0 Then
            lblbalance.Text = Format(CDbl(lblbalance.Text) - CDbl(lblNetAmt.Text), numFormat)
        End If
        If Val(lblInvoices.Text) > 0 Then
            lblInvoices.Text = Format(CDbl(lblInvoices.Text) - 1, numFormat)
        End If

        chgNumByPgm = True
        txtdp.Text = Format((CDbl(numDisc.Text) * 100) / CDbl(lblTotAmt.Text), lnumFormat)
        chgNumByPgm = False
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
    Private Sub PasteFrom(ByVal Trid As Long)
        Dim i As Integer
        Dim sRs As DataTable
        Dim UPerPack As Double
        sRs = _objcmnbLayer._fldDatatable("SELECT ItmInvTrTb.*, [Item Code],Warrentyname FROM ItmInvTrTb LEFT JOIN InvItm ON InvItm.ItemId = ItmInvTrTb.ItemId " & _
                                          "LEFT JOIN WarrentyMastertb ON WarrentyMastertb.Wid=ItmInvTrTb.WarrentyId WHERE TrId = " & Trid & " ORDER BY SlNo")
        grdVoucher.RowCount = 0
        With grdVoucher
            .Rows.Clear()
            If sRs.Rows.Count > 0 Then
                For i = 0 To sRs.Rows.Count - 1
                    grdVoucher.Rows.Add(1)
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
                    grdVoucher.Item(ConstCardtype, i).Value = Trim("" & sRs(i)("Warrentyname")) ' !Colour
                    grdVoucher.Item(ConstQty, i).Value = Format(sRs(i)("TrQty") / UPerPack, "#,##0" & IIf(Val(grdVoucher.Item(ConstPFraction, i).Value) = 0, "", "." & Strings.StrDup(CByte(grdVoucher.Item(ConstPFraction, i).Value), "0")))
                    grdVoucher.Item(ConstUPrice, i).Value = Format(sRs(i)("UnitCost") * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & lnumFormat)
                    grdVoucher.Item(ConstActualPrice, i).Value = sRs(i)("UnitCost") * UPerPack / FCRt
                    grdVoucher.Item(ConstLTotal, i).Value = Format((sRs(i)("TrQty") * sRs(i)("UnitCost")) * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & lnumFormat)
                    grdVoucher.Item(ConstUnitOthCost, i).Value = Format(sRs(i)("UnitOthCost") * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & lnumFormat)
                    grdVoucher.Item(ConstActualOthCost, i).Value = sRs(i)("UnitOthCost") * UPerPack / FCRt
                    grdVoucher.Item(ConstSerialNo, i).Value = sRs(i)("SerialNo")
                    grdVoucher.Item(ConstDisAmt, i).Value = sRs(i)("UnitDiscount") * UPerPack / FCRt
                    grdVoucher.Item(ConstId, i).Value = 0
                    grdVoucher.Item(ConstWarrentyExpiry, i).Value = DateAdd(DateInterval.Year, 1, DateValue(Date.Now))

                Next
                reArrangeNo()
            End If
        End With
        If sRs.Rows.Count > 0 Then sRs.Clear()
        calculate()
        numVchrNo.Focus()
        chgbyprg = False
        ChgId = True
        chgPost = True
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
            Dim vrPrefix As String = ""
            Dim vrVoucherNo As Long
            Dim vrAccountNo1 As Long
            Dim vrAccountNo2 As Long
            With cmbVoucherTp
                If .Items.Count > 0 Then
                    If .SelectedIndex < 0 Then .SelectedIndex = 0
                    cmbVoucherTp.Tag = getvrsId(cmbVoucherTp.Text, 12)
                    getVrsDet(Val(cmbVoucherTp.Tag), "DIS", vrPrefix, vrVoucherNo, vrAccountNo1, vrAccountNo2)
                Else
                    getVrsDet(0, "DIS", vrPrefix, vrVoucherNo, vrAccountNo1, vrAccountNo2)
                End If

            End With
            If Not isModi Then
                numVchrNo.Text = vrVoucherNo
                txtprefix.Text = vrPrefix
            End If
            If Val(txtSuppName.Tag) = 0 Then
                txtSuppAlias.Tag = vrAccountNo2
            End If
            If Val(txtPurchaseName.Tag) = 0 Then
                txtPurchAlias.Tag = vrAccountNo1
            End If
            Dim dtAcc As DataTable
            dtAcc = _objcmnbLayer._fldDatatable("SELECT Alias,AccDescr,GrpSetOn,GrpSetOn,accid FROM AccMast " & _
                                                "LEFT JOIN S1AccHd ON AccMast.S1AccId=S1AccHd.S1AccId WHERE accid=" & vrAccountNo1)
            If dtAcc.Rows.Count > 0 Then
                txtPurchaseName.Text = dtAcc(0)("AccDescr")
                txtPurchAlias.Text = dtAcc(0)("Alias")
                txtPurchAlias.Tag = vrAccountNo1
            Else
                txtPurchaseName.Text = ""
                txtPurchAlias.Text = ""
                txtPurchAlias.Tag = ""
            End If
            dtAcc = _objcmnbLayer._fldDatatable("SELECT Alias,AccDescr,GrpSetOn,GrpSetOn,accid FROM AccMast " & _
                                               "LEFT JOIN S1AccHd ON AccMast.S1AccId=S1AccHd.S1AccId WHERE accid=" & vrAccountNo2)
            If dtAcc.Rows.Count > 0 Then
                txtSuppName.Text = dtAcc(0)("AccDescr")
                txtSuppAlias.Text = dtAcc(0)("Alias")
                txtSuppAlias.Tag = vrAccountNo2
                setCustomer(Val(txtSuppAlias.Tag))
            Else
                txtSuppName.Text = ""
                txtSuppName.Text = ""
                txtSuppAlias.Tag = ""
            End If
            txtReference.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub txtReference_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDescr.KeyDown, txtSuppName.KeyDown, txtSuppAlias.KeyDown, txtReference.KeyDown, txtjobname.KeyDown, txtJob.KeyDown, txtCashCustomer.KeyDown
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
            ElseIf MyCtrl.Name = "txtCashCustomer" Then
                txtDescr.Focus()
            Else
                SendKeys.Send("{TAB}")
            End If
            If Not fMList Is Nothing Then fMList.Visible = False
        ElseIf e.KeyCode = Keys.F2 Then
            If Not fMList Is Nothing Then fMList.Visible = False
            Select Case MyCtrl.Name
                Case "txtSuppAlias", "txtSuppName"
                    _srchTxtId = IIf(MyCtrl.Name = "txtSuppAlias", 1, 2)
                    ldSelect(1)
                Case "txtJob"
                    _srchTxtId = 3
                    ldSelect(3)
                Case "txtCashCustomer"
                    fCashCust = New CreateCashCustomerFrm
                    fCashCust.ShowDialog()
                    fCashCust = Nothing
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
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
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


    Private Sub txtLocation_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtJob.TextChanged, txtSuppName.TextChanged, txtSuppAlias.TextChanged
        If chgbyprg = True Then Exit Sub
        Dim myCtrl As Control = sender
        Select Case myCtrl.Name
            Case "txtSuppAlias"
                _srchTxtId = 1
            Case "txtSuppName"
                _srchTxtId = 2
            Case "txtJob"
                _srchTxtId = 3
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
            Dim x As Integer = Me.Width - fMList.Width - 100
            Dim y As Integer = Me.Height - fMList.Height - 100
            PopupLoc = New Point(x, y)
            If fMList.Visible = False Then
                Select Case _srchTxtId
                    Case 1, 2
                        SetFmlist(fMList, 3)
                    Case 3 'job 
                        SetFmlist(fMList, 8)
                End Select
                fMList.loc = PopupLoc
                fMList.Show(fMainForm)
                fMList.Visible = True
            End If
        End If
        Select Case _srchTxtId
            Case 1   'Customer Code
                fMList.SearchIndex = 1
                fMList.SearchIndexDescr = 0
                fMList_doFocus()
                fMList.Search(txtSuppAlias.Text)
                txtSuppName.Text = fMList.AssignList(txtSuppAlias, lstKey, chgbyprg)
            Case 2   'Customer name
                fMList.SearchIndex = 0
                fMList.SearchIndexDescr = 1
                fMList_doFocus()
                fMList.Search(txtSuppName.Text)
                txtSuppAlias.Text = fMList.AssignList(txtSuppName, lstKey, chgbyprg)
            Case 3   'job
                fMList.SearchIndex = 0
                'fMList.SearchIndexDescr = 0
                fMList_doFocus()
                fMList.Search(txtJob.Text)
                fMList.AssignList(txtJob, lstKey, chgbyprg)
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
                txtSuppAlias.Text = ItmFlds(1)
                txtSuppName.Text = ItmFlds(0)
                txtSuppAlias.Tag = ItmFlds(3)
            Case 3
                txtJob.Text = ItmFlds(0)
        End Select
        chgbyprg = False
    End Sub
    Private Sub SetGridHead()
        chgbyprg = True
        Dim i As Integer
        With grdVoucher
            With grdVoucher

                SetEntryGridProperty(grdVoucher)
                .ColumnCount = 49

                .Columns(ConstSlNo).HeaderText = "SlNo"
                .Columns(ConstSlNo).Width = 40
                '.Columns(ConstSlNo).ReadOnly = False
                .Columns(ConstSlNo).SortMode = DataGridViewColumnSortMode.NotSortable
                '.Columns(ConstSlNo).Resizable = DataGridViewTriState.False
                .Columns(ConstSlNo).Frozen = True
                .Columns(ConstSlNo).DefaultCellStyle.Format = "N0"
                .Columns(ConstSlNo).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(ConstSlNo).ReadOnly = True
                .Columns(ConstSlNo).DefaultCellStyle.BackColor = Color.AliceBlue


                .Columns(ConstBarcode).HeaderText = "HSN Code"
                .Columns(ConstBarcode).Width = 100
                .Columns(ConstBarcode).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(ConstBarcode).ReadOnly = True
                .Columns(ConstBarcode).Visible = EnableGST

                .Columns(ConstItemCode).HeaderText = "ItemCode"
                .Columns(ConstItemCode).Width = 100
                .Columns(ConstItemCode).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(ConstItemCode).ReadOnly = False

                .Columns(ConstDescr).HeaderText = "Description"
                '.Columns(ConstDescr).Width = Me.Width * 45 / 100
                .Columns(ConstDescr).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(ConstDescr).ReadOnly = False
                .Columns(ConstDescr).Width = 150


                Dim cmb As New DataGridViewComboBoxColumn
                'cmb.Items.Add("")
                Dim dt As DataTable
                dt = _objcmnbLayer._fldDatatable("SELECT cardtypename FROM CardtypemasterTb")
                cmb.Items.Add("")
                For i = 0 To dt.Rows.Count - 1
                    cmb.Items.Add(Trim(dt(i)(0)))
                Next
                cmb.HeaderText = "Card Type"
                cmb.DataPropertyName = "cardtypename"
                cmb.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
                cmb.DefaultCellStyle.Font = New System.Drawing.Font("Arial Black", 12.0!, System.Drawing.FontStyle.Bold)
                .Columns.RemoveAt(ConstCardtype)
                .Columns.Insert(ConstCardtype, cmb)
                .Columns(ConstCardtype).HeaderText = "Card Type"
                .Columns(ConstCardtype).Width = 100
                .Columns(ConstCardtype).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(ConstCardtype).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(ConstCardtype).ReadOnly = False ' Not AllowLocationItemwiseOnInventory
                .Columns(ConstCardtype).Visible = True


                .Columns(ConstCardNumber).HeaderText = "Card Number"
                .Columns(ConstCardNumber).Width = 150
                .Columns(ConstCardNumber).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(ConstCardNumber).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Columns(ConstCardNumber).ReadOnly = False



                .Columns(ConstQty).HeaderText = "Qty"
                .Columns(ConstQty).Width = 50
                .Columns(ConstQty).SortMode = DataGridViewColumnSortMode.NotSortable
                '.Columns(ConstQty).Resizable = DataGridViewTriState.False
                .Columns(ConstQty).DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns(ConstQty).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(ConstQty).ReadOnly = True

                .Columns(ConstUPrice).HeaderText = "Unit Price"
                .Columns(ConstUPrice).Width = 70
                .Columns(ConstUPrice).SortMode = DataGridViewColumnSortMode.NotSortable
                '.Columns(ConstUPrice).Resizable = DataGridViewTriState.False
                '.Columns(ConstUPrice).ValueType=
                .Columns(ConstUPrice).DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns(ConstUPrice).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(ConstUPrice).ReadOnly = False

                .Columns(constItmTot).HeaderText = "Item Total"
                '.Columns(ConstDisAmt).Width = Me.Width * 7 / 100 '70
                .Columns(constItmTot).SortMode = DataGridViewColumnSortMode.NotSortable
                '.Columns(ConstDisAmt).Resizable = DataGridViewTriState.False
                .Columns(constItmTot).DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns(constItmTot).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(constItmTot).ReadOnly = True
                .Columns(constItmTot).Visible = False


                .Columns(ConstTaxP).HeaderText = IIf(EnableGST, "GST%", "Tax%")
                .Columns(ConstTaxP).Width = 50
                '.Columns(ConstTaxP).Width = Me.Width * 6 / 100 '60
                .Columns(ConstTaxP).SortMode = DataGridViewColumnSortMode.NotSortable
                '.Columns(ConstTaxP).Resizable = DataGridViewTriState.False
                .Columns(ConstTaxP).DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns(ConstTaxP).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(ConstTaxP).ReadOnly = EnableGST
                .Columns(ConstTaxP).Visible = False
                'lblTax.Visible = ShowTaxOnInventory

                .Columns(ConstTaxAmt).HeaderText = IIf(EnableGST, "GST Amt", "Tax Amt")
                '.Columns(ConstTaxAmt).Width = Me.Width * 7 / 100 '70
                .Columns(ConstTaxAmt).SortMode = DataGridViewColumnSortMode.NotSortable
                '.Columns(ConstTaxAmt).Resizable = DataGridViewTriState.False
                .Columns(ConstTaxAmt).DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns(ConstTaxAmt).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(ConstTaxAmt).ReadOnly = True
                .Columns(ConstTaxAmt).Visible = False

                .Columns(ConstLTotal).HeaderText = "Line Total"
                .Columns(ConstLTotal).Width = 80
                .Columns(ConstLTotal).SortMode = DataGridViewColumnSortMode.NotSortable
                '.Columns(ConstLTotal).Resizable = DataGridViewTriState.False
                .Columns(ConstLTotal).DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns(ConstLTotal).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(ConstLTotal).ReadOnly = True
                .Columns(ConstLTotal).DefaultCellStyle.BackColor = Color.GreenYellow

                .Columns(ConstUnitOthCost).HeaderText = "Unit Oth Cost"
                '.Columns(ConstUnitOthCost).Width = Me.Width * 9 / 100 '80
                .Columns(ConstUnitOthCost).SortMode = DataGridViewColumnSortMode.NotSortable
                '.Columns(ConstLTotal).Resizable = DataGridViewTriState.False
                .Columns(ConstUnitOthCost).DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns(ConstUnitOthCost).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(ConstUnitOthCost).ReadOnly = True
                .Columns(ConstUnitOthCost).Visible = False

                .Columns(ConstNUPrice).HeaderText = "Net Unit Price"
                '.Columns(ConstNUPrice).Width = Me.Width * 8 / 100 '70
                .Columns(ConstNUPrice).ReadOnly = True
                .Columns(ConstNUPrice).SortMode = DataGridViewColumnSortMode.NotSortable
                '.Columns(ConstNUPrice).Resizable = DataGridViewTriState.False
                .Columns(ConstNUPrice).DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns(ConstNUPrice).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(ConstNUPrice).Visible = False

                .Columns(ConstActualOthCost).HeaderText = "Unit Actual Oth Cost"
                .Columns(ConstActualOthCost).Visible = False
                .Columns(ConstActualOthCost).ReadOnly = True
                .Columns(ConstActualOthCost).DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns(ConstActualOthCost).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                .Columns(ConstMthd).HeaderText = "Method"
                .Columns(ConstMthd).Visible = False
                .Columns(ConstMthd).ReadOnly = True

                .Columns(ConstUnitVal).HeaderText = "uVal"
                .Columns(ConstUnitVal).Visible = False
                .Columns(ConstUnitVal).ReadOnly = True

                .Columns(ConstDiscOther).HeaderText = "DisOther"
                .Columns(ConstDiscOther).Visible = False
                .Columns(ConstDiscOther).ReadOnly = True

                .Columns(ConstJob).HeaderText = "Job"
                .Columns(ConstJob).Visible = False
                .Columns(ConstJob).ReadOnly = True

                .Columns(ConstJobCostAc).HeaderText = "JobCostAcc"
                .Columns(ConstJobCostAc).Visible = False
                .Columns(ConstJobCostAc).ReadOnly = True

                .Columns(ConstBcodeOrICode).HeaderText = "Barcode/ItmCode"
                .Columns(ConstBcodeOrICode).Visible = False
                .Columns(ConstBcodeOrICode).ReadOnly = True

                .Columns(ConstImpLnId).HeaderText = "ImprtdDocLnNo"
                .Columns(ConstImpLnId).Visible = False
                .Columns(ConstImpLnId).ReadOnly = True

                .Columns(ConstImpDocId).HeaderText = "ImprtdDocID"
                .Columns(ConstImpDocId).Visible = False
                .Columns(ConstImpDocId).ReadOnly = True

                .Columns(ConstActualPrice).HeaderText = "Actual Price"
                .Columns(ConstActualPrice).Visible = False
                .Columns(ConstActualPrice).ReadOnly = True

                .Columns(ConstJobAcID).HeaderText = "JobAccID"
                .Columns(ConstJobAcID).Visible = False
                .Columns(ConstJobAcID).ReadOnly = True

                .Columns(ConstPFraction).HeaderText = "pFraction"
                .Columns(ConstPFraction).Visible = False
                .Columns(ConstPFraction).ReadOnly = True


                .Columns(ConstPMult).HeaderText = "Mult"
                .Columns(ConstPMult).Visible = False
                .Columns(ConstPMult).ReadOnly = True

                .Columns(ConstItemID).HeaderText = "ItemID"
                .Columns(ConstItemID).Visible = False
                .Columns(ConstItemID).ReadOnly = True

                .Columns(ConstBaseID).HeaderText = "BaseID"
                .Columns(ConstBaseID).Visible = False
                .Columns(ConstBaseID).ReadOnly = True

                .Columns(ConstLrow).HeaderText = "Lrow"
                .Columns(ConstLrow).Visible = False
                .Columns(ConstLrow).ReadOnly = True

                .Columns(ConstId).HeaderText = "id"
                .Columns(ConstId).Visible = False
                .Columns(ConstId).ReadOnly = True
                .Columns(ConstqtyChg).Visible = False

                .Columns(ConstCGSTP).HeaderText = "CGST %"
                .Columns(ConstCGSTP).Width = 50
                .Columns(ConstCGSTP).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(ConstCGSTP).DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns(ConstCGSTP).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(ConstCGSTP).ReadOnly = True
                .Columns(ConstCGSTP).Visible = False

                .Columns(ConstCGSTAmt).HeaderText = "CGST Amt"
                .Columns(ConstCGSTAmt).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(ConstCGSTAmt).DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns(ConstCGSTAmt).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(ConstCGSTAmt).ReadOnly = True
                .Columns(ConstCGSTAmt).Visible = False

                .Columns(ConstSGSTP).HeaderText = "SGST %"
                .Columns(ConstSGSTP).Width = 50
                .Columns(ConstSGSTP).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(ConstSGSTP).DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns(ConstSGSTP).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(ConstSGSTP).ReadOnly = True
                .Columns(ConstSGSTP).Visible = False

                .Columns(ConstSGSTAmt).HeaderText = "SGST Amt"
                .Columns(ConstSGSTAmt).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(ConstSGSTAmt).DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns(ConstSGSTAmt).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(ConstSGSTAmt).ReadOnly = True
                .Columns(ConstSGSTAmt).Visible = False

                .Columns(ConstIGSTP).HeaderText = "IGST %"
                .Columns(ConstIGSTP).Width = 50
                .Columns(ConstIGSTP).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(ConstIGSTP).DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns(ConstIGSTP).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(ConstIGSTP).ReadOnly = True
                .Columns(ConstIGSTP).Visible = False

                .Columns(ConstIGSTAmt).HeaderText = "IGST Amt"
                .Columns(ConstIGSTAmt).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(ConstIGSTAmt).DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns(ConstIGSTAmt).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(ConstIGSTAmt).ReadOnly = True
                .Columns(ConstIGSTAmt).Visible = False
                .Columns(ConstIsSerial).Visible = False
                .Columns(ConstIsManufacturingItem).Visible = False

                .Columns(ConstB).Visible = False
                .Columns(ConstUnit).Visible = False
                .Columns(Constsman).Visible = False
                .Columns(ConstStartReading).Visible = False
                .Columns(ConstWarrentyExpiry).Visible = False
                .Columns(ConstMeterCode).Visible = False
                .Columns(ConstDisP).Visible = False
                .Columns(ConstDisAmt).Visible = False
                .Columns(ConstSerialNo).Visible = False


            End With

            For i = 1 To ConstUPrice
                cmbcolms.Items.Add(.Columns(i).HeaderText)
            Next
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

            .Rows.Add(1)
            i = .RowCount - 1
            .Item(ConstSlNo, i).Value = i + 1
            .Item(ConstBarcode, i).Value = ""
            .Item(ConstItemCode, i).Value = ""
            .Item(ConstDescr, i).Value = ""
            .Item(ConstUnit, i).Value = ""
            .Item(ConstQty, i).Value = 1
            .Item(ConstUPrice, i).Value = Format(0, lnumFormat)
            .Item(ConstDisP, i).Value = Format(0, lnumFormat)
            .Item(ConstDisAmt, i).Value = Format(0, lnumFormat)
            .Item(ConstTaxP, i).Value = Format(0, lnumFormat)
            .Item(ConstTaxAmt, i).Value = Format(0, lnumFormat)
            .Item(ConstLTotal, i).Value = Format(0, lnumFormat) '(.Item(ConstQty, i).Value * DR!unitPrice) - (.Item(ConstQty, i).Value * Val(DR!DiscountVal)) + (.Item(ConstQty, i).Value * Val(DR!TaxVal))
            .Item(ConstNUPrice, i).Value = Format(0, lnumFormat) 'Val(DR!unitPrice) - Val(DR!DiscountVal) + Val(DR!TaxVal)
            .Item(ConstItemID, i).Value = 0
            .Item(ConstBaseID, i).Value = 0
            .Item(ConstSerialNo, i).Value = ""
            .Item(ConstPMult, i).Value = "1"
            .Item(ConstPFraction, i).Value = "0"
            .Item(ConstImpDocId, i).Value = ""
            .Item(ConstImpLnId, i).Value = ""
            .Item(ConstLrow, i).Value = i
            'MsgBox(.Item(ConstUPrice, i).Value)
            checkWSCarditem(i)
            .CurrentCell = .Item(ConstItemCode, i)
            Valid(i, ConstItemCode)
            
            chgItm = False
        End With
        calculate()
        reArrangeNo()
        'ChgByPrg = False

    End Sub
    Private Sub calculate(Optional ByVal bFrmCalcOthCost As Boolean = False)
        Dim totQty As Double
        Dim totItm As Double
        Dim totTax As Double
        Dim totLnDis As Double
        Dim totAmt As Double
        Dim i As Integer
        calOthCost()
        If numDisc.Text = "" Then
            numDisc.Text = Format(0, lnumFormat)
        End If
        If Not dtTax Is Nothing Then
            For j = 0 To dtTax.Rows.Count - 1
                dtTax(j)("Amount") = 0
            Next
        End If


        With grdVoucher
            For i = 0 To .Rows.Count - 1
                If Val(.Item(ConstItemID, i).Value) = 0 Then GoTo nxt
                .Item(ConstSlNo, i).Value = i + 1
                totQty = totQty + IIf(Val(.Item(ConstQty, i).Value) > 0, Val(.Item(ConstQty, i).Value), 0)
                If Val(.Item(ConstTaxP, i).Value & "") = 0 Then
                    .Item(ConstTaxP, i).Value = 0
                End If
                If Val(.Item(ConstActualPrice, i).Value & "") = 0 Then
                    .Item(ConstActualPrice, i).Value = 0
                End If
                If Val(.Item(ConstQty, i).Value & "") = 0 Then
                    .Item(ConstQty, i).Value = 0
                End If
                If EnableGST Then
                    If Val(.Item(ConstSGSTAmt, i).Value & "") = 0 Then .Item(ConstSGSTAmt, i).Value = 0
                    If Val(.Item(ConstCGSTAmt, i).Value & "") = 0 Then .Item(ConstCGSTAmt, i).Value = 0
                    .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstCGSTAmt, i).Value) + CDbl(.Item(ConstSGSTAmt, i).Value), lnumFormat)
                Else
                    .Item(ConstTaxAmt, i).Value = Format(((CDbl(.Item(ConstActualPrice, i).Value) * .Item(ConstTaxP, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value), lnumFormat)
                End If
                'If chkTaxbill.Checked = False Then
                '    .Item(ConstTaxAmt, i).Value = Format(0, lnumFormat)
                '    .Item(ConstTaxP, i).Value = Format(0, lnumFormat)
                'End If
                totTax = totTax + .Item(ConstTaxAmt, i).Value
                totLnDis = totLnDis + .Item(ConstDisAmt, i).Value
                'totAmt = totAmt + CDbl(.Item(ConstActualPrice, i).Value) * CDbl(.Item(ConstQty, i).Value)
                totAmt = totAmt + (.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value) + CDbl(.Item(ConstTaxAmt, i).Value)
                totItm = totItm + (.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value)
                .Item(constItmTot, i).Value = Format((.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value), lnumFormat)
                .Item(ConstLTotal, i).Value = Format((.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value) + CDbl(.Item(ConstTaxAmt, i).Value), lnumFormat)
                .Item(ConstNUPrice, i).Value = CDbl(.Item(ConstActualPrice, i).Value) - (CDbl(.Item(ConstDisAmt, i).Value) / CDbl(.Item(ConstQty, i).Value)) + (CDbl(.Item(ConstTaxAmt, i).Value) / CDbl(.Item(ConstQty, i).Value)) - Val(.Item(ConstDiscOther, i).Value)
                If ShowTaxOnInventory Then
                    For j = 0 To dtTax.Rows.Count - 1
                        If Val(.Item(ConstTaxP, i).Value) = dtTax(j)("vat") Then
                            dtTax(j)("Amount") = CDbl(dtTax(j)("Amount")) + CDbl(grdVoucher.Item(ConstTaxAmt, i).Value)
                        End If
                    Next
                ElseIf EnableGST Then
                    CalculateGST()
                End If

nxt:
            Next
            calOthCost()
            lblTotAmt.Text = Format(totItm, lnumFormat)
            lblNetAmt.Text = Format(totAmt - CDbl(numDisc.Text), lnumFormat)
            If Val(txtroundOff.Text) > 0 Then
                lblNetAmt.Text = Format(CDbl(lblNetAmt.Text) - (IIf(cmbsign.SelectedIndex = 1, 1, -1) * CDbl(txtroundOff.Text)), lnumFormat)
            End If
            lbltax.Text = Format(totTax, lnumFormat)
            totAmt = totAmt - CDbl(numDisc.Text)
            chgAmt = False
            If Val(txtdp.Text) > 0 And Val(lblTotAmt.Text) > 0 And Val(numDisc.Text) = 0 Then
                numDisc.Text = Format((CDbl(lblTotAmt.Text) * CDbl(txtdp.Text)) / 100, lnumFormat)
            End If
            If Val(lblNetAmt.Text) > 0 And Val(txtsmanP.Text) > 0 Then
                lblSCAmt.Text = Format((CDbl(lblNetAmt.Text) * CDbl(txtsmanP.Text)) / 100, lnumFormat)
            End If
        End With
    End Sub

    Private Sub setOthCost()

    End Sub

    Private Function calOthCost(Optional ByVal bfrmSetOthrCost As Boolean = False) As Double

        If Not bfrmSetOthrCost Then setOthCost()
        Dim i As Integer
        Dim tOthVal As Double
        Dim tBAmt As Double
        Dim tBDAmt As Double
        Dim tDAmt As Double
        Dim actualPrice As Double
        With grdVoucher
            For i = 0 To .Rows.Count - 1 '- 1
                If IsDBNull(.Item(ConstQty, i).Value) Then .Item(ConstQty, i).Value = 0
                If CStr(.Item(ConstQty, i).Value) = "" Then .Item(ConstQty, i).Value = 0
                If Val(.Item(ConstMthd, i).Value) <> 0 Then
                    tOthVal = tOthVal + Val(.Item(ConstActualOthCost, i).Value) * CDbl(.Item(ConstQty, i).Value)
                Else
                    tBAmt = tBAmt + CDbl(.Item(constItmTot, i).Value)
                End If
                'If Val(.Item(i, 20)) <> 0 Then
                '   tDAmt = tDAmt + Val(.Item(ConstDiscOther, i).Value) * CDbl(.Item(ConstQty, i).Value)
                'Else
                tBDAmt = tBDAmt + CDbl(.Item(constItmTot, i).Value)
                'End If
            Next i
            tOthVal = OthCost / FCRt - tOthVal
            If numDisc.Text = "" Then numDisc.Text = 0
            tDAmt = CDbl(numDisc.Text) - tDAmt
            For i = 0 To .Rows.Count - 1 '- 1
                If Val(.Item(ConstMthd, i).Value) = 0 Then
                    ' '' '' '' ''If tBAmt = 0 Then
                    ' '' '' '' ''    .Item(ConstActualOthCost, i).Value = 0
                    ' '' '' '' ''Else
                    ' '' '' '' ''    .Item(ConstActualOthCost, i).Value = tOthVal * Val(.Item(ConstActualPrice, i).Value) / tBAmt
                    ' '' '' '' ''End If
                    .Item(ConstUnitOthCost, i).Value = Format(Val(.Item(ConstActualOthCost, i).Value), "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ' & lnumFormat)
                End If
                If Val(.Item(ConstQty, i).Value) > 0 Then
                    actualPrice = Val(.Item(ConstActualPrice, i).Value) - (CDbl(.Item(ConstDisAmt, i).Value) / Val(.Item(ConstQty, i).Value))
                End If
                If tBDAmt = 0 Then
                    .Item(ConstDiscOther, i).Value = 0
                Else
                    .Item(ConstDiscOther, i).Value = (tDAmt * actualPrice) / tBDAmt
                End If

                .Item(ConstNUPrice, i).Value = Format(Val(.Item(ConstActualPrice, i).Value) + Val(.Item(ConstActualOthCost, i).Value) - Val(.Item(ConstDiscOther, i).Value) - Val(.Item(ConstDisAmt, i).Value), "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ' & lnumFormat)
                calOthCost = calOthCost + (CDbl(.Item(ConstQty, i).Value) * (Val(.Item(ConstActualOthCost, i).Value) - Val(.Item(ConstDiscOther, i).Value))) - Val(.Item(ConstDisAmt, i).Value)
            Next i
        End With
        If lblTotAmt.Text = "" Then lblTotAmt.Text = Format(0, lnumFormat)
        If lblNetAmt.Text = "" Then lblNetAmt.Text = Format(0, lnumFormat)
        If numDisc.Text = "" Then numDisc.Text = Format(0, lnumFormat)
        'lblNetAmt.Text = Format(CDbl(lblTotAmt.Text) - CDbl(numDisc.Text), lnumFormat)
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
        chgbyprg = True
        With grdVoucher
            If Val(grdVoucher.Item(0, grdVoucher.CurrentCell.RowIndex).Value) = 0 And e.ColumnIndex <> 3 Then
                grdVoucher.CurrentCell.ReadOnly = True
            ElseIf e.ColumnIndex <> ConstSlNo And e.ColumnIndex <> constItmTot And e.ColumnIndex <> ConstUnit And e.ColumnIndex <> ConstLTotal And e.ColumnIndex <> ConstBarcode And e.ColumnIndex <> ConstTaxAmt And e.ColumnIndex <> ConstStartReading Then
                If e.ColumnIndex = ConstTaxP And EnableGST Then
                    grdVoucher.CurrentCell.ReadOnly = True
                ElseIf e.ColumnIndex = ConstQty And grdVoucher.Item(ConstMeterCode, grdVoucher.CurrentCell.RowIndex).Value <> "" Then
                    grdVoucher.CurrentCell.ReadOnly = True
                ElseIf e.ColumnIndex = ConstCardtype And isModi = True And optid = 1 Then
                    grdVoucher.CurrentCell.ReadOnly = True
                ElseIf e.ColumnIndex = ConstCardNumber And isModi = True And optid = 1 Then
                    grdVoucher.CurrentCell.ReadOnly = True
                Else
                    grdVoucher.CurrentCell.ReadOnly = False
                End If
            
            Else
                grdVoucher.CurrentCell.ReadOnly = True
            End If
        End With
        grdBeginEdit()
    End Sub

    Private Sub grdVoucher_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellValidated
        Valid(e.RowIndex, e.ColumnIndex)
    End Sub
    Private Sub Valid(ByVal RowIndex As Integer, ByVal ColIndex As Integer)
        Dim dt As DataTable
        With grdVoucher
            Select Case ColIndex
                Case ConstBarcode, ConstItemCode
                    If Not chgItm Then Exit Sub
                    If Trim(.Item(ColIndex, RowIndex).Value) = "" Then Exit Sub
                    Dim dtItms As DataTable
                    Dim found As Boolean
                    dtItms = getItmDtls(3, SrchText, True)
                    If dtItms.Rows.Count > 0 Then
                        found = True
                        AddDetails(dtItms)
                    End If
                    chgItm = False
                    If Not found Then
                        .Item(ConstBarcode, RowIndex).Value = ""
                        .Item(ConstItemCode, RowIndex).Value = ""
                        .Item(ConstBaseID, RowIndex).Value = ""
                        .Item(ConstItemID, RowIndex).Value = ""
                        .Item(ConstUnit, RowIndex).Value = ""
                        .Item(ConstSerialNo, RowIndex).Value = ""
                        .Item(ConstPMult, RowIndex).Value = "1"
                        .Item(ConstPFraction, RowIndex).Value = "2"
                        .Item(ConstImpDocId, RowIndex).Value = ""
                        .Item(ConstImpLnId, RowIndex).Value = ""
                        chgItm = False
                    End If
                Case ConstMeterCode
                    If enableFuleBankInvoice Then
                        Dim ir As Integer
                        For ir = 0 To .RowCount - 1
                            If .Item(ConstMeterCode, RowIndex).Value = .Item(ConstMeterCode, ir).Value And ir <> RowIndex Then
                                MsgBox("Meter Code already selected", MsgBoxStyle.Exclamation)
                                .Item(ConstMeterCode, RowIndex).Value = ""
                                Exit Sub
                            End If
                        Next
                        dt = _objcmnbLayer._fldDatatable("Select isnull(fending,0) fending,isnull(fstarting,0) fstarting from FuelMeterReadingTb where fcode='" & .Item(ConstMeterCode, RowIndex).Value & "'")
                        If dt.Rows.Count > 0 Then
                            .Item(ConstStartReading, RowIndex).Value = Format(IIf(CDbl(dt(0)("fending")) = 0, CDbl(dt(0)("fstarting")), CDbl(dt(0)("fending"))), flnumformat)
                        End If
                    End If
                Case ConstQty
                    If chgAmt Then
                        If AllowUnitDiscountEntryOnInventory Then
                            .Item(ConstDisAmt, RowIndex).Value = Format(((CDbl(.Item(ConstActualPrice, RowIndex).Value) * Val(.Item(ConstDisP, RowIndex).Value)) / 100) * CDbl(.Item(ConstQty, RowIndex).Value), lnumFormat)
                        End If
                        If EnableGST Then getGSTDetails(Trim(.Item(ConstBarcode, RowIndex).Value & ""), RowIndex, True)
                        calculate()
                    End If
                Case ConstUPrice
                    If chgAmt Then
                        .Item(ConstActualPrice, RowIndex).Value = CDbl(.Item(ConstUPrice, RowIndex).Value)
                        If AllowUnitDiscountEntryOnInventory Then
                            .Item(ConstDisAmt, RowIndex).Value = Format(((CDbl(.Item(ConstActualPrice, RowIndex).Value) * Val(.Item(ConstDisP, RowIndex).Value)) / 100) * CDbl(.Item(ConstQty, RowIndex).Value), lnumFormat)
                        End If
                        If EnableGST Then getGSTDetails(Trim(.Item(ConstBarcode, RowIndex).Value & ""), RowIndex, True)
                        calculate()
                    End If
                Case ConstDisP
                    If chgAmt Then
                        .Item(ConstDisAmt, RowIndex).Value = Format(((CDbl(.Item(ConstActualPrice, RowIndex).Value) * Val(.Item(ConstDisP, RowIndex).Value)) / 100) * CDbl(.Item(ConstQty, RowIndex).Value), lnumFormat)
                        If EnableGST Then getGSTDetails(Trim(.Item(ConstBarcode, RowIndex).Value & ""), RowIndex, True)
                        calculate()
                    End If
                Case ConstDisAmt
                    If chgAmt Then
                        Dim unitDiscount As Double
                        unitDiscount = CDbl(.Item(ConstDisAmt, RowIndex).Value) / CDbl(.Item(ConstQty, RowIndex).Value)
                        .Item(ConstDisP, RowIndex).Value = Format((unitDiscount * 100) / CDbl(.Item(ConstActualPrice, RowIndex).Value), lnumFormat)
                        If EnableGST Then getGSTDetails(Trim(.Item(ConstBarcode, RowIndex).Value & ""), RowIndex, True)
                        calculate()
                    End If
                Case ConstTaxAmt
                    If chgAmt Then
                        calculate()
                    End If
                Case ConstTaxP
                    If chgAmt Then
                        calculate()
                    End If
                Case ConstCardtype
                    dt = _objcmnbLayer._fldDatatable("SELECT isnull(Amount,0) Amount," & _
                                    "GSTTb.HSNCode,cardtypeid FROM " & _
                                    "CardtypemasterTb LEFT JOIN GSTTb ON GSTTb.gstid=CardtypemasterTb.gstid " & _
                                    "where cardtypename='" & Trim(.Item(ConstCardtype, RowIndex).Value & "") & "'")
                    If dt.Rows.Count > 0 Then
                        .Item(ConstUPrice, RowIndex).Value = Format(dt(0)("Amount"), numFormat)
                        .Item(ConstActualPrice, RowIndex).Value = dt(0)("Amount")
                        .Item(ConstBarcode, RowIndex).Value = dt(0)("HSNCode")
                        .Item(ConstImpDocId, RowIndex).Value = dt(0)("cardtypeid")
                        If EnableGST Then getGSTDetails(Trim(.Item(ConstBarcode, RowIndex).Value & ""), RowIndex, False)
                    End If
                    calculate()
                Case Else
            End Select
        End With
    End Sub
    Private Sub AddDetails(ByVal DR As DataTable)
        Dim i As Integer
        Dim PMult As Double
        With grdVoucher
            chgbyprg = True
            PMult = 1
            i = .CurrentRow.Index ' .RowCount - 1
            .Item(ConstSlNo, i).Value = i + 1
            .Item(ConstItemCode, i).Value = IIf(IsDBNull(DR(0)("Item Code")), Trim(DR(0)("Item Code") & ""), DR(0)("Item Code"))
            .Item(ConstDescr, i).Value = DR(0)("Description")
            If EnableGST Then
                .Item(ConstBarcode, i).Value = DR(0)("HSNCode") 'hsncode
            Else
                .Item(ConstBarcode, i).Value = ""
            End If
            .Item(ConstQty, i).Value = Format(1, lnumFormat) 'IIf(IsReturn, -1, 1)
            .Item(ConstItemID, i).Value = DR(0)("ItemId")
            .Item(ConstPMult, i).Value = 1 'IIf(IsNull(sRs(0)("PFraCount), "2", sRs(0)("PFraCount)

            getItemInfo(DR(0)("ItemId"))
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
                If chkws.Checked Then
                    .Item(ConstActualPrice, i).Value = DR(0)("UnitPriceWS")
                Else
                    .Item(ConstActualPrice, i).Value = DR(0)("UnitPrice")
                End If
                .Item(ConstUPrice, i).Value = Format(CDbl(.Item(ConstActualPrice, i).Value), lnumFormat)
            End If
            If Not IsDBNull(DR(0)("isSerialNo")) Then
                .Item(ConstIsSerial, i).Value = IIf(DR(0)("isSerialNo"), 1, 0)
            Else
                .Item(ConstIsSerial, i).Value = 0
            End If
            'If Val(DR(0)("vat") & "") = 0 Then DR(0)("vat") = 0
            '.Item(ConstTaxP, i).Value = Format(IIf(IsDBNull(DR(0)("vat")), 0, CDbl(DR(0)("vat"))), lnumFormat)
            '.Item(ConstTaxAmt, i).Value = ((CDbl(.Item(ConstActualPrice, i).Value) * .Item(ConstTaxP, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value)
            If ShowTaxOnInventory Then
                If Val(DR(0)("vat") & "") = 0 Then
                    DR(0)("vat") = 0
                End If
                .Item(ConstTaxP, i).Value = Format(IIf(IsDBNull(DR(0)("vat")), 0, CDbl(DR(0)("vat"))), lnumFormat)
                .Item(ConstTaxAmt, i).Value = ((CDbl(.Item(ConstActualPrice, i).Value) * .Item(ConstTaxP, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value)
            ElseIf EnableGST Then
                getGSTDetails(Trim(.Item(ConstBarcode, i).Value & ""), i, False)
            End If
            .Item(ConstLTotal, i).Value = Format(.Item(ConstQty, i).Value * CDbl(.Item(ConstUPrice, i).Value), lnumFormat)
            .Item(ConstNUPrice, i).Value = Format(CDbl(.Item(ConstUPrice, i).Value), lnumFormat)
            .Item(ConstQty, i).Value = Format(CDbl(.Item(ConstQty, i).Value), "#,##0" & IIf(Val(.Item(ConstPFraction, i).Value) = 0, "", "." & Strings.StrDup(CByte(.Item(ConstPFraction, i).Value), "0")))
            .Item(ConstImpDocId, i).Value = ""
            .Item(ConstImpLnId, i).Value = ""
            chgAmt = True
            chgItm = False
            .ClearSelection()
        End With
        calculate()
        chgbyprg = False
    End Sub
    Private Sub CalculateGST()
        Exit Sub
        Dim i As Integer
        'Dim dtHSN As DataTable
        'Dim dtrow As DataRow
        'Dim slno As Integer
        dtTax.Rows.Clear()
        Dim dtHSN As DataTable
        Dim dtrow As DataRow
        Dim slno As Integer = 0
        With grdVoucher
            Dim _qurey As EnumerableRowCollection(Of DataRow)
            For i = 0 To .RowCount - 1
                slno = 0
                _qurey = From data In dtGST.AsEnumerable() Where data("HSNCode") = Trim(.Item(ConstBarcode, i).Value & "") Select data
                If _qurey.Count > 0 Then
                    dtHSN = _qurey.CopyToDataTable
                    If Val(lblstatecode.Tag) = 0 Then
                        'adding CSGT Amount****
                        Dim a = (From data In dtTax.AsEnumerable() Where data("ACid") = dtHSN(0)("CGSTCAc") Select data("slno"))
                        For Each itm In a
                            Try
                                slno = itm
                            Catch ex As Exception
                                MsgBox(ex.Message)
                            End Try
                        Next
                        If slno = 0 Then
                            dtrow = dtTax.NewRow
                            dtrow("slno") = dtTax.Rows.Count + 1
                            dtrow("AccountName") = dtHSN(0)("CGSTCAname")
                            dtrow("ACid") = dtHSN(0)("CGSTCAc")
                            dtrow("Amount") = CDbl(.Item(ConstCGSTAmt, i).Value)
                            dtTax.Rows.Add(dtrow)
                        Else
                            dtTax(slno - 1)("Amount") = CDbl(dtTax(slno - 1)("Amount")) + CDbl(.Item(ConstCGSTAmt, i).Value)
                        End If
                        'adding SSGT Amount****
                        a = (From data In dtTax.AsEnumerable() Where data("ACid") = dtHSN(0)("SGSTCAc") Select data("slno"))
                        slno = 0
                        For Each itm In a
                            Try
                                slno = itm
                            Catch ex As Exception
                                MsgBox(ex.Message)
                            End Try

                        Next
                        If slno = 0 Then
                            dtrow = dtTax.NewRow
                            dtrow("slno") = dtTax.Rows.Count + 1
                            dtrow("AccountName") = dtHSN(0)("SGSTCAname")
                            dtrow("ACid") = dtHSN(0)("SGSTCAc")
                            dtrow("Amount") = CDbl(.Item(ConstSGSTAmt, i).Value)
                            dtTax.Rows.Add(dtrow)
                        Else
                            dtTax(slno - 1)("Amount") = CDbl(dtTax(slno - 1)("Amount")) + CDbl(.Item(ConstSGSTAmt, i).Value)
                        End If
                    Else
                        'adding ISGT Amount****
                        Dim a = (From data In dtTax.AsEnumerable() Where data("ACid") = dtHSN(0)("IGSTCAc") Select data("slno"))
                        slno = 0
                        For Each itm In a
                            slno = itm
                        Next
                        If slno = 0 Then
                            dtrow = dtTax.NewRow
                            dtrow("slno") = dtTax.Rows.Count + 1
                            dtrow("AccountName") = dtHSN(0)("IGSTCAname")
                            dtrow("ACid") = dtHSN(0)("IGSTCAc")
                            dtrow("Amount") = CDbl(.Item(ConstIGSTAmt, i).Value)
                            dtTax.Rows.Add(dtrow)
                        Else
                            dtTax(slno - 1)("Amount") = CDbl(dtTax(slno - 1)("Amount")) + CDbl(.Item(ConstIGSTAmt, i).Value)
                        End If
                    End If
                End If
            Next
        End With
    End Sub
    Private Sub getGSTDetails(ByVal hsncode As String, ByVal i As Integer, ByVal calculatefromGrid As Boolean)
        Exit Sub
        Dim dt As DataTable
        With grdVoucher
            If Not calculatefromGrid Then
                dt = _objcmnbLayer._fldDatatable("SELECT * FROM GSTTb WHERE HSNCODE='" & hsncode & "'")
                If dt.Rows.Count > 0 Then
                    .Item(ConstCGSTP, i).Value = Format(IIf(IsDBNull(dt(0)("CGST")), 0, CDbl(dt(0)("CGST"))), lnumFormat)
                    .Item(ConstSGSTP, i).Value = Format(IIf(IsDBNull(dt(0)("SGST")), 0, CDbl(dt(0)("SGST"))), lnumFormat)
                    .Item(ConstIGSTP, i).Value = Format(IIf(IsDBNull(dt(0)("IGST")), 0, CDbl(dt(0)("IGST"))), lnumFormat)
                Else
                    .Item(ConstCGSTP, i).Value = 0
                    .Item(ConstSGSTP, i).Value = 0
                    .Item(ConstIGSTP, i).Value = 0
                End If
            End If
            Dim actualPrice As Double
            actualPrice = (CDbl(.Item(ConstActualPrice, i).Value) * CDbl(.Item(ConstQty, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value)
            .Item(ConstCGSTAmt, i).Value = (actualPrice * .Item(ConstCGSTP, i).Value) / 100
            .Item(ConstSGSTAmt, i).Value = (actualPrice * .Item(ConstSGSTP, i).Value) / 100
            .Item(ConstIGSTAmt, i).Value = (actualPrice * .Item(ConstIGSTP, i).Value) / 100
            If Val(lblstatecode.Tag) = 1 Then
                .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstIGSTAmt, i).Value), lnumFormat)
                .Item(ConstTaxP, i).Value = .Item(ConstIGSTP, i).Value
            Else
                .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstCGSTAmt, i).Value) + CDbl(.Item(ConstSGSTAmt, i).Value), lnumFormat)
                .Item(ConstTaxP, i).Value = CDbl(.Item(ConstCGSTP, i).Value) + CDbl(.Item(ConstSGSTP, i).Value)
            End If
        End With
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
                Case ConstQty, ConstTaxP, ConstDisAmt, ConstDisP
                    chgAmt = True
                Case ConstLTotal
                    If Val(grdVoucher.Item(ConstQty, i).Value) > 0 Then
                        If Val(grdVoucher.Item(ConstDisAmt, i).Value) = 0 And CDbl(grdVoucher.Item(ConstTaxAmt, i).Value) = 0 Then 'Not AllowUnitDiscountEntryOnInventory And Not ShowTaxOnInventory And
                            chgbyprg = True
                            grdVoucher.Item(ConstUPrice, i).Value = Format(CDbl(grdVoucher.Item(ConstLTotal, i).Value) / grdVoucher.Item(ConstQty, i).Value, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) 'IIf(IsReturn, -1, 1)
                            grdVoucher.Item(ConstActualPrice, i).Value = CDbl(grdVoucher.Item(ConstLTotal, i).Value) / grdVoucher.Item(ConstQty, i).Value
                            calculate()
                            chgbyprg = False
                        End If
                    End If
                    chgAmt = True
                Case ConstDisAmt
                    chgAmt = True
                Case ConstUPrice
                    chgUprice = True
                    chgAmt = True
                    calculateTaxFromUnitPrice(e.RowIndex)
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
                    If grdVoucher.Item(ConstSerialNo, grdVoucher.CurrentRow.Index).Value <> "" Then
                        e.Handled = True
                    End If
                End If
            ElseIf col = ConstSerialNo Then
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
                _srchTxtId = 1
                chgbyprg = True
                strGridSrchString = MyCtrl.Text
                ShowPanel()
                chgItm = True
                chgbyprg = False
                grdVoucher.Item(ConstqtyChg, grdVoucher.CurrentRow.Index).Value = "Chg"
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
    Private Sub ShowPanel()
        If Not _srchOnce Then
            chgbyprg = True
            Dim PopupLoc As Point
            Dim x As Integer = Me.Width - plsrch.Width - 100
            Dim y As Integer = Me.Height - plsrch.Height - 100
            PopupLoc = New Point(x, y)
            If plsrch.Visible = False Then
                plsrch.Location = PopupLoc
                plsrch.Visible = True
            End If
        End If
        SearchProductPanel(grdSrch, "[Item Code]", strGridSrchString, True)
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
                If SrchText = "" And grdVoucher.CurrentCell.ColumnIndex = ConstItemCode Then
                    GoTo nxt
                End If
                If FindNextCell(grdVoucher, grdVoucher.CurrentCell.RowIndex, grdVoucher.CurrentCell.ColumnIndex + 1) Or enableNextlineonItemcode Then
                    AddRow()
                End If
nxt:
                plsrch.Visible = False
                grdBeginEdit()

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
            ElseIf e.KeyCode = Keys.F2 Then
                If plsrch.Visible Then plsrch.Visible = False
                If grdVoucher.RowCount = 0 Then Exit Sub
                Select Case grdVoucher.CurrentCell.ColumnIndex
                    Case ConstItemCode
                        If Not fMList Is Nothing Then fMList.Visible = False
                        If plsrch.Visible Then plsrch.Visible = False
                        fProductEnquiry = New ItmEnqry
                        fProductEnquiry.ShowDialog()
                    Case ConstSerialNo
                        'If Not fSerialno Is Nothing Then fSerialno.Close() : fSerialno = Nothing
                        'fSerialno = New AddSerialnoFrm
                        'fSerialno.txtserialno.Tag = Val(grdVoucher.Item(ConstItemID, grdVoucher.CurrentRow.Index).Value)
                        'fSerialno.isSales = True
                        'fSerialno.cldrdate.Value = DateAdd(DateInterval.Year, 1, cldrdate.Value)
                        'fSerialno.ShowDialog()
                        showSerialNoFrom()
                    Case ConstTaxAmt, ConstTaxP
                        If EnableGST Then
                            tbgst.Visible = True
                            showItemGst(True, grdVoucher.CurrentRow.Index)
                        End If
                End Select
            ElseIf e.KeyCode = Keys.Escape Then
                plsrch.Visible = False
            ElseIf e.KeyCode = Keys.F3 Then
                AddRow()
            ElseIf e.KeyCode = Keys.F4 Then
                RemoveRow()
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
                    .isSales = True
                    .detId = Val(grdVoucher.Item(ConstId, grdVoucher.CurrentRow.Index).Value)
                    .rowIndex = grdVoucher.CurrentCell.RowIndex + 1
                    .ShowDialog()
                End With

            End If
        End If
    End Sub
    Private Sub showItemGst(ByVal isfocus As Boolean, ByVal i As Integer)
        With grdVoucher
            'Dim i As Integer = grdVoucher.CurrentRow.Index
            If Val(.Item(ConstActualPrice, i).Value) = 0 Then .Item(ConstActualPrice, i).Value = 0
            If Val(.Item(ConstQty, i).Value) = 0 Then .Item(ConstQty, i).Value = 0
            txtCgst.Tag = CDbl(.Item(ConstActualPrice, i).Value) * CDbl(.Item(ConstQty, i).Value)
            chgbyprgN = True
            If Val(lblstatecode.Tag) > 0 Then
                txtIgst.Enabled = True
                txtIgstAmt.Enabled = True
                txtIgst.Text = Format(CDbl(.Item(ConstIGSTP, i).Value), lnumformat)
                txtIgstAmt.Text = Format(CDbl(.Item(ConstIGSTAmt, i).Value), lnumformat)

                txtCgst.Text = Format(0, lnumformat)
                txtCgstAmt.Text = Format(0, lnumformat)
                txtSgst.Text = Format(0, lnumformat)
                txtSgstAmt.Text = Format(0, lnumformat)
                txtCgst.Enabled = False
                txtCgstAmt.Enabled = False
                txtSgst.Enabled = False
                txtSgstAmt.Enabled = False
            Else
                txtCgst.Enabled = True
                txtCgstAmt.Enabled = True
                txtSgst.Enabled = True
                txtSgstAmt.Enabled = True
                txtCgst.Text = Format(CDbl(.Item(ConstCGSTP, i).Value), lnumformat)
                txtCgstAmt.Text = Format(CDbl(.Item(ConstCGSTAmt, i).Value), lnumformat)
                txtSgst.Text = Format(CDbl(.Item(ConstSGSTP, i).Value), lnumformat)
                txtSgstAmt.Text = Format(CDbl(.Item(ConstSGSTAmt, i).Value), lnumformat)

                txtIgst.Text = Format(0, lnumformat)


                txtIgstAmt.Text = Format(0, lnumformat)
                txtIgst.Enabled = False
                txtIgstAmt.Enabled = False
            End If

        End With
        chgbyprgN = False
        If isfocus Then txtCgst.Focus()
    End Sub
    Private Sub setGstToGrdFromTabC()
        Dim i As Integer = grdVoucher.CurrentRow.Index
        With grdVoucher
            If Val(lblstatecode.Tag) > 0 Then
                .Item(ConstIGSTP, i).Value = txtIgst.Text
                .Item(ConstIGSTAmt, i).Value = txtIgstAmt.Text
            Else
                .Item(ConstCGSTP, i).Value = txtCgst.Text
                .Item(ConstCGSTAmt, i).Value = txtCgstAmt.Text
                .Item(ConstSGSTP, i).Value = txtSgst.Text
                .Item(ConstSGSTAmt, i).Value = txtSgstAmt.Text
            End If
            If Val(lblstatecode.Tag) = 1 Then
                .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstIGSTAmt, i).Value), lnumformat)
                .Item(ConstTaxP, i).Value = .Item(ConstIGSTP, i).Value
            Else
                .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstCGSTAmt, i).Value) + CDbl(.Item(ConstSGSTAmt, i).Value), lnumformat)
                .Item(ConstTaxP, i).Value = CDbl(.Item(ConstCGSTP, i).Value) + CDbl(.Item(ConstSGSTP, i).Value)
            End If
        End With
        txtIgst.Text = 0
        txtIgstAmt.Text = 0
        txtCgst.Text = 0
        txtCgstAmt.Text = 0
        txtSgst.Text = 0
        txtSgstAmt.Text = 0
        txtCgst.Tag = 0
        tbgst.Visible = False
        calculate()
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
        If tbgst.Visible Then
            showItemGst(False, e.RowIndex)
        End If
        If Not grdVoucher.CurrentRow Is Nothing Then
            setItemInfo(Val(grdVoucher.Item(ConstItemID, e.RowIndex).Value))
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
                getItemInfo(0)
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
        lnumFormat = numFormat
        grdVoucher.CurrentCell = grdVoucher.Item(1, 0)
        _objcmnbLayer.dtSerialNo.Rows.Clear()
        getItemInfo(0)
        activecontrolname = ""
        'lstRow = 0
        chgbyprg = True
        txtReference.Text = ""
        txtDescr.Text = ""
        txtSuppAlias.Text = ""
        txtSuppAlias.Tag = ""
        txtSuppName.Text = ""
        txtcustAddress.Text = ""
        txtJob.Text = ""
        txtJob.Tag = ""
        lblhead.Text = "Card Sales"
        chgDoc = False
        'fMainForm.lblUser.Text = CurrentUser
        'fMainForm.lblModiUser.Text = ""
        OthCost = 0
        chgNumByPgm = True
        txtCashCustomer.Text = ""
        numDisc.Text = Format(0, lnumFormat)
        txtdp.Text = Format(0, lnumFormat)
        chgNumByPgm = False
        lblSCAmt.Text = Format(0, lnumFormat)
        txtsmanP.Text = Format(0, lnumFormat)
        cmbfc.Text = ""
        txtfcrt.Text = Format(0, lnumFormat)
        txtroundOff.Text = Format(0, lnumFormat)
        OthCost = 0
        CTVol = 0
        CTWt = 0
        CTQty = 0
        loadedTrId = 0
        LddImpDocs = ""
        optid = 0

        If dtItemInfo.Rows.Count > 0 Then dtItemInfo.Rows.Clear()
        'setImport(True)
        calculate()
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
        numVchrNo.Focus()
        setCustomer()
        chgbyprg = False
        lbladd1.Text = ""
        lbladd2.Text = ""
        lbladd3.Text = ""
        lbladd4.Text = ""
        lbladd5.Text = ""
        lbladd6.Text = ""
        lbladd7.Text = ""
        lbltrdate.Text = ""
        txtIgst.Text = 0
        txtIgstAmt.Text = 0
        txtCgst.Text = 0
        txtCgstAmt.Text = 0
        txtSgst.Text = 0
        txtSgstAmt.Text = 0
        txtCgst.Tag = 0
        tbgst.Visible = False
        lblbalance.Text = Format(0, numFormat)
        lbllimit.Text = Format(0, numFormat)
        lblInvoices.Text = Format(0, numFormat)
        If enableFuleBankInvoice Then loadSalesMultipleDebits(0)
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
            If userType Then
                btnupdate.Tag = IIf(getRight(162, CurrentUser), 1, 0)
                btndelete.Tag = IIf(getRight(163, CurrentUser), 1, 0)
            Else
                btnupdate.Tag = 1
                btndelete.Tag = 1
            End If

        Else
            AddNewClick()

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
        If chgAmt Then calculate()
        ActBr = ""
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
        'If txtReference.Text = "" Then
        '    MsgBox("Invalid Reference", MsgBoxStyle.Exclamation)
        '    txtReference.Focus()
        '    Exit Sub
        'End If
        If DateValue(cldrdate.Value) <= ProtectUntil Then
            MsgBox("Voucher Date comes within Protected Date Range !!", MsgBoxStyle.Information)
            cldrdate.Focus()
            Exit Sub
        End If
        If enableFuleBankInvoice Then
            If dtMultipleDebits.Rows.Count = 0 Then
                MsgBox("Enter Multiple Debit Account !!", vbExclamation)
                txtSuppName.Focus()
                'txtSuppAlias.Focus()
                Exit Sub
            End If
            Dim Mamt As Double
            Mamt = getSumOfDataTable(dtMultipleDebits, "accAmt")
            If Mamt <> CDbl(lblNetAmt.Text) Then
                MsgBox("Multiple Debit Amount and Invoice Amount are Mismatch!", vbExclamation)
                txtDescr.Focus()
                Exit Sub
            End If
        Else
            _vAcMaster = _objcmnbLayer._fldDatatable(StrAccMastSrch & " WHERE Alias = '" & txtSuppAlias.Text & "' ORDER BY AccDescr")
            If _vAcMaster.Rows.Count = 0 Then
                MsgBox("Enter a valid  Customer Account !!", vbExclamation)
                txtSuppName.Focus()
                'txtSuppAlias.Focus()
                Exit Sub
            Else
                txtSuppAlias.Tag = _vAcMaster(0)("accid")
            End If
        End If

        If Val(txtPurchAlias.Tag) = 0 Then
            MsgBox("Invalid Prepaid Income!", MsgBoxStyle.Exclamation)
            cmbVoucherTp.Focus()
            Exit Sub
        End If
        If blockInvoicing() Then Exit Sub
        'If Not isModi Then
        '    If chekDuplicate() Then Exit Sub
        'End If
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT Trid from ItmInvcmntb where cscode=" & Val(txtSuppAlias.Tag) & " and TrRefNo='" & txtReference.Text & "' and Trid<>" & loadedTrId)
        If dt.Rows.Count > 0 Then
            MsgBox("Reference already exist!", MsgBoxStyle.Exclamation)
            txtReference.Focus()
            Exit Sub
        End If
        'If CheckRestrictImport() = False Then Exit Sub
        If Not chkGridvalue() Then Exit Sub
        If CDbl(lblNetAmt.Text) < 0 Then
            MsgBox("Net Amount below Zero is not allowed !!!?", vbExclamation)
            MyActiveControl = numDisc
            Exit Sub
        End If
        If MsgBox("Verification is succeeded. Do you like to file it ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = vbYes Then
            saveTrans()
        End If
    End Sub

    '    Private Sub doStockUpdate(ByVal InvNo As Long, ByVal DocId As Long)
    '        Dim dtInvCmnTb As DataTable
    '        'Dim dtsRs As DataTable
    '        Dim TrId As Long
    '        Dim i As Integer
    '        Dim PPerU As Single
    '        Dim Rfrsh As Boolean
    '        dtInvCmnTb = _objcmnbLayer._fldDatatable("SELECT * FROM ItmInvCmnTb WHERE TrType = '" & IIf(isCust, "DC", "DS") & "' AND InvNo = " & InvNo)
    '        If dtInvCmnTb.Rows.Count > 0 Then
    '            Rfrsh = True
    '            _objcmnbLayer._saveDatawithOutParm("DELETE FROM ItmInvTrTb WHERE TrId = " & dtInvCmnTb(0)("TrId"))
    '            TrId = dtInvCmnTb(0)("TrId")
    '        Else
    '            TrId = 0
    '        End If
    '        'If chkStkUpdtd.Checked = False Then GoTo Ter
    '        setInvCmnValue(TrId)
    '        TrId = Val(_objInv._saveCmn())
    '        With grdVoucher
    '            For i = 0 To .Rows.Count - 1 '- 1
    '                If Val(.Item(ConstItemID, i).Value) > 0 Then
    '                    If CDbl(.Item(ConstQty, i).Value) <> 0 Or .Item(ConstSlNo, i).Value.ToString = "M" Then
    '                        PPerU = Val(.Item(ConstPMult, i).Value) 'Switch(.TextMatrix(i, 4) = "B", 1, .TextMatrix(i, 4) = "P1", CSng(grdVoucher.TextMatrix(i, 18)), .TextMatrix(i, 4) = "P2", CSng(grdVoucher.TextMatrix(i, 19)))
    '                        PPerU = IIf(PPerU = 0, 1, PPerU)
    '                        setInvDetValue(TrId, PPerU, i)
    '                        _objInv._saveDetails()
    '                        '-------------------------------------------------------------------------------------------------------------------------
    '                    End If
    '                End If

    '            Next i
    '        End With
    'Ter:
    '    End Sub
    Private Sub savetest()
        Dim con As New SqlClient.SqlConnection
        con.ConnectionString = "Server=VINVIS-PC\sql2014;uid=sa;pwd=mosesft;database=VISHNUTEST"
        con.Open()
        Using copy As New SqlClient.SqlBulkCopy(con)
            copy.ColumnMappings.Add("trid", "trid")
            copy.ColumnMappings.Add("itemid", "itemid")
            copy.ColumnMappings.Add("id", "id")
            copy.DestinationTableName = "ItmInvTrTb"
            copy.WriteToServer(dtTb)
        End Using
    End Sub
    Private Sub saveCardmaster(ByVal cardtypeid As Integer, ByVal cardnumber As String)
        Dim dt As DataTable
        If cardnumber = "" Then Exit Sub
        dt = _objcmnbLayer._fldDatatable("Select cardid from CardmasterTb where cardnumber='" & Trim(MkDbSrchStr(cardnumber)) & "'")
        If dt.Rows.Count = 0 Then
            _objcmnbLayer._saveDatawithOutParm("Insert into CardmasterTb (cardtypeid,customerid,cardnumber) values(" & _
                                               cardtypeid & "," & Val(txtCashCustomer.Tag) & ",'" & cardnumber & "')")
        End If

    End Sub

    Private Sub saveTrans()
        Dim TrId As Long
        Dim i As Integer
        Dim DiscAcc As Long
        Dim PPerU As Single
        Dim TDrAmt As Double
        Dim dateChanged As Boolean
        Dim qtychanged As Boolean
        Dim dtTable As DataTable
        If Val(txtfcrt.Text) = 0 Then txtfcrt.Text = 0
        FCRt = CDbl(txtfcrt.Text)
        If FCRt = 0 Then
            FCRt = 1
        End If
        clsreader()
        clsCnnection()
        If Not isModi Then
chkagain:
            If Not CheckNoExists(txtprefix.Text, Val(numVchrNo.Text), "DIS", "Inventory") Then
                If MsgBox("Document No alreary exist! Do you want to fill next?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Exit Sub
                Else
                    numVchrNo.Text = Val(numVchrNo.Text) + 1
                    GoTo chkagain
                End If
            End If
        End If
        dtTable = _objcmnbLayer._fldDatatable("SELECT accid FROM AccMast WHERE AccSetId Like '%03%'")
        If dtTable.Rows.Count > 0 Then DiscAcc = dtTable(0)("accid")
        calculate()
        'If dtTable.Rows.Count > 0 Then dtTable.Clear()
        If isModi Then
            dtTable = _objcmnbLayer._fldDatatable("SELECT TOP 1 * FROM ItmInvCmnTb WHERE TrId = " & loadedTrId)
            If dtTable.Rows.Count = 0 Then
                MsgBox("The loaded voucher not found.  It may removed externally.", vbExclamation)
                Exit Sub
            End If
            _objcmnbLayer._saveDatawithOutParm("Update ItmInvTrTb set setRemove=1 WHERE trid=" & loadedTrId)
            TrId = loadedTrId
        End If
        setInvCmnValue(TrId)
        TrId = Val(_objInv._saveCmn())
        _objcmnbLayer._saveDatawithOutParm("UPDATE ItmInvCmnTb SET CashCustName='" & IIf(txtCashCustomer.Text = "", txtSuppName.Text, txtCashCustomer.Text) & "',CashCustid=" & Val(txtCashCustomer.Tag) & " WHERE TRID=" & TrId)

        'to check whether date has been changed or not
        'if changed there should be calculeted cost average for all items
        dtTable = _objcmnbLayer._fldDatatable("SELECT TOP 1 * FROM ItmInvCmnTb LEFT JOIN VoucherTypeNoTb ON ItmInvCmnTb.TypeNo=VoucherTypeNoTb.vrno " & _
                                                      "WHERE InvType='OUT' AND Trdate >='" & Format(DateValue(cldrdate.Value), "yyyy/MM/dd") & "'")
        If dtTable.Rows.Count > 0 Then
            dateChanged = True
        Else
            dateChanged = False
        End If
        ReDim JobAcc(0)
        JobAcc(0).Acc = Val(txtPurchAlias.Tag)
        JobAcc(0).Job = txtJob.Text
        Dim amt As Double
        If Val(txtroundOff.Text) > 0 Then
            amt = CDbl(lblTotAmt.Text) - (IIf(cmbsign.SelectedIndex = 1, 1, -1) * CDbl(txtroundOff.Text))
        Else
            amt = CDbl(lblTotAmt.Text)
        End If
        JobAcc(0).Amt = IIf(DiscAcc = 0, CDbl(lblNetAmt.Text) - CDbl(lbltax.Text), amt)
        Dim itemidsdatatable As New DataTable
        itemidsdatatable.Columns.Add(New DataColumn("Itemid", GetType(Long)))
        With grdVoucher
            For i = 0 To .Rows.Count - 1 '- 1
                If CDbl(.Item(ConstQty, i).Value) <> 0 Or .Item(ConstSlNo, i).Value.ToString = "M" Then
                    PPerU = Val(.Item(ConstPMult, i).Value) 'Switch(.TextMatrix(i, 4) = "B", 1, .TextMatrix(i, 4) = "P1", CSng(grdVoucher.TextMatrix(i, 18)), .TextMatrix(i, 4) = "P2", CSng(grdVoucher.TextMatrix(i, 19)))
                    PPerU = IIf(PPerU = 0, 1, PPerU)
                    TDrAmt = TDrAmt + CDbl(.Item(ConstQty, i).Value) * (CDbl(.Item(ConstActualPrice, i).Value) - IIf(DiscAcc = 0, CDbl(.Item(ConstDiscOther, i).Value), 0))
                    TDrAmt = TDrAmt - CDbl(.Item(ConstDisAmt, i).Value)
                    setInvDetValue(TrId, PPerU, i)
                    _objInv._saveDetails()
                    saveCardmaster(Val(.Item(ConstImpDocId, i).Value), .Item(ConstCardNumber, i).Value)
                    If UCase(.Item(ConstqtyChg, i).Value) = "CHG" Then
                        qtychanged = True
                    Else
                        qtychanged = False
                    End If
                    If (dateChanged Or (qtychanged And Val(.Item(ConstId, i).Value) > 0)) And enableRealtimeCosting Then
                        _objInv.ItemId = Val(.Item(ConstItemID, i).Value)
                        _objInv.TrDate = DateValue(cldrdate.Value)
                        _objInv.setcostAverageOnModification(UsrBr)
                    End If
                End If
                If enableFuleBankInvoice Then
                    _objcmnbLayer.updateMeterReadingQty(Trim(.Item(ConstMeterCode, i).Value & ""))
                End If
            Next
            'savetest()
            'MsgBox("")
            'Exit Sub

            If isModi Then
                _objcmnbLayer._saveDatawithOutParm("UPDATE SerialNoTb SET SalesId=0 WHERE SerialNo IN (SELECT SerialNo FROM ItmInvTrTb where setRemove=1 and trid=" & loadedTrId & ")")
                _objcmnbLayer._saveDatawithOutParm("delete from WarrentyTrTb where SerialNo IN (SELECT SerialNo FROM ItmInvTrTb where setRemove=1 and trid=" & loadedTrId & ")")
                '_objcmnbLayer._saveDatawithOutParm("delete from SerialNoTb where ISNULL(PurchaseId,0)=0 and ISNULL(SalesId,0)=0")
                _objcmnbLayer._saveDatawithOutParm("delete from SerialNoTb where ISNULL(LastINTrId,0)=0 and ISNULL(LastOutTrid,0)=0")
                _objInv.deleteInventoryRelatedItemDetails(loadedTrId)
                itemidsdatatable = _objcmnbLayer._fldDatatable("select itemid from ItmInvTrTb where setRemove=1 and trid=" & loadedTrId)
                _objcmnbLayer._saveDatawithOutParm("delete from ItmInvTrTb where setRemove=1 and trid=" & loadedTrId)
                If itemidsdatatable.Rows.Count > 0 And enableRealtimeCosting Then
                    For i = 0 To itemidsdatatable.Rows.Count - 1
                        _objInv.ItemId = itemidsdatatable(i)("Itemid")
                        _objInv.TrDate = DateValue(cldrdate.Value)
                        _objInv.setcostAverageOnModification(UsrBr)
                    Next
                End If
            End If
        End With
        UpdateAccounts(TrId, TDrAmt, DiscAcc)
        If isModi = False Then
            numPrintVchr.Text = numVchrNo.Text
            txtPPrefix.Text = txtprefix.Text
            numVchrNo.Tag = Val(numVchrNo.Text) 'Trim(CMNREC(12).FormattedText)
            txtprefix.Tag = txtprefix.Text
            txtprefix.Text = SetNextVrNo(numVchrNo, Val(cmbVoucherTp.Tag), "DIS", "TrType = 'DIS' AND InvNo = ", False, True, True)
        End If
        Dim isprint As Boolean
        If enablePrintOnSave And Not isModi Then
            If MsgBox("Do you want print?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                isprint = True
            Else
                isprint = False
            End If
        End If
        ChgId = False
        chgPost = False
        If isModi Then
            btnModify_Click(btnModify, New System.EventArgs)
        Else
            AddNewClick()
        End If
        MsgBox("Sales Invoice is succesfully posted with Vr. # " & numPrintVchr.Text & ".", MsgBoxStyle.Information)
        numVchrNo.Tag = ""
        If isprint Then
            PrepareRpt("DIS", True)
        End If
    End Sub

    Private Sub UpdateAccounts(ByVal TrId As Long, ByVal TDrAmt As Double, ByVal DiscAcc As Integer)
        Dim LinkNo As Long
        Dim dtTable As DataTable
        If isModi And TrId > 0 Then
            dtTable = _objcmnbLayer._fldDatatable("SELECT LinkNo FROM AccTrCmn WHERE lnkno  = " & TrId)
            If dtTable.Rows.Count > 0 Then
                LinkNo = dtTable(0)("LinkNo")
                _objcmnbLayer._saveDatawithOutParm("DELETE FROM AccTrDet WHERE LinkNo=" & LinkNo)
            End If
        End If

        setAcctrCmnValue(TrId, LinkNo)
        LinkNo = 0
        LinkNo = Val(_objTr.SaveAccTrCmn())

        Dim EntRef As String = Strings.Left(Trim(txtDescr.Text) & IIf(Trim(txtDescr.Text) = "", "", " / ") & txtPurchaseName.Text, 249)
        Dim dlAmt As Double = (TDrAmt - IIf(DiscAcc > 0, CDbl(numDisc.Text), 0)) * FCRt

        'Tax Entry Credit
        Dim i As Integer = 0
        If EnableGST Then
            For i = 0 To dtTax.Rows.Count - 1
                If Val(dtTax(i)("ACid")) > 0 And Val(dtTax(i)("Amount")) > 0 Then
                    Dim TxAmount As Double = CDbl(dtTax(i)("Amount"))
                    dlAmt = dlAmt + (TxAmount * FCRt)
                    setAcctrDetValue(LinkNo, Val(dtTax(i)("ACid")), Trim(txtReference.Text), dtTax(i)("AccountName") & " Tax Collected", TxAmount * -1 * FCRt, Strings.Left(txtJob.Text, 50), "", 0, 0, "", "", Val(txtSuppAlias.Tag), Val(txtSuppAlias.Tag) & txtReference.Text, cmbfc.Text, FCRt)
                    _objTr.saveAccTrans()
                End If
            Next
        ElseIf ShowTaxOnInventory Then
            For i = 0 To dtTax.Rows.Count - 1
                If Val(dtTax(i)("collectionAC")) > 0 And Val(dtTax(i)("Amount")) > 0 Then
                    Dim TxAmount As Double = CDbl(dtTax(i)("Amount"))
                    dlAmt = dlAmt + (TxAmount * FCRt)
                    setAcctrDetValue(LinkNo, Val(dtTax(i)("collectionAC")), Trim(txtReference.Text), dtTax(i)("Vatcode") & " Tax Collected", TxAmount * -1 * FCRt, Strings.Left(txtJob.Text, 50), "", 0, 0, "", "", Val(txtSuppAlias.Tag), Val(txtSuppAlias.Tag) & txtReference.Text, cmbfc.Text, FCRt)
                    _objTr.saveAccTrans()
                End If
            Next
        End If
        'Debit Entry or prepaid income
        For j = 0 To JobAcc.Count - 1
            setAcctrDetValue(LinkNo, j)
            _objTr.saveAccTrans()
        Next

        'Credit Entry or customer entry
        If enableFuleBankInvoice Then
            If dtMultipleDebits.Rows.Count > 0 Then
                'Multiple Entry
                For j = 0 To dtMultipleDebits.Rows.Count - 1
                    setAcctrDetValue(LinkNo, Val(dtMultipleDebits(j)("accid")), Trim(txtReference.Text), EntRef, CDbl(dtMultipleDebits(j)("accAmt")), "", Strings.Left(txtJob.Text, 50), 0, 0, "", _
                         "", Val(txtSuppAlias.Tag), Val(txtSuppAlias.Tag) & txtReference.Text, cmbfc.Text, FCRt)
                    _objTr.saveAccTrans()
                Next
            Else
                GoTo deft
            End If
        Else
deft:
            If Val(txtroundOff.Text) > 0 Then
                dlAmt = dlAmt - (IIf(cmbsign.SelectedIndex = 1, 1, -1) * CDbl(txtroundOff.Text))
            End If

            setAcctrDetValue(LinkNo, Val(txtSuppAlias.Tag), Trim(txtReference.Text), EntRef, dlAmt * -1, "", Strings.Left(txtJob.Text, 50), 0, 0, "", _
                             "", Val(txtSuppAlias.Tag), Val(txtSuppAlias.Tag) & txtReference.Text, cmbfc.Text, FCRt)
            _objTr.saveAccTrans()
        End If


        'DiscountEntry
        If (CDbl(numDisc.Text)) > 0 And DiscAcc > 0 Then
            dlAmt = CDbl(numDisc.Text) * FCRt
            setAcctrDetValue(LinkNo, DiscAcc, Trim(txtReference.Text), Trim(txtDescr.Text), dlAmt, "", "", 2, 0, "", _
                            "", Val(txtSuppAlias.Tag), DiscAcc & txtReference.Text, cmbfc.Text, FCRt)
            _objTr.saveAccTrans()
        End If
        If enableFuleBankInvoice Then saveMultipleDebits(TrId)
        updateStockTransaction(TrId, LinkNo)
        updateClosingBalanceForInvoice(TrId)
    End Sub

    Private Sub updateStockTransaction(ByVal trid As Long, ByVal LinkNo As Long)
        If Not enableCostAccounting Then Exit Sub
        Dim stockAc As Long
        Dim costOfSalesAc As Long
        Dim dt As DataTable
        Dim costAmt As Double
        stockAc = getConstantAccounts(1)
        costOfSalesAc = getConstantAccounts(9)
        If stockAc = 0 Or costOfSalesAc = 0 Then Exit Sub
        dt = _objcmnbLayer._fldDatatable("select sum(CostAvg*TrQty) costAmt from ItmInvTrTb  where trid=" & trid)
        If dt.Rows.Count > 0 Then
            costAmt = dt(0)("costAmt")
        End If
        If costAmt <> 0 Then
            'debit entry [cost of sales]
            Dim entryref As String = "COST OF SALES FOR INVOICE : " & txtSuppName.Text & " # " & Trim(txtprefix.Text) & numVchrNo.Text
            setAcctrDetValue(LinkNo, costOfSalesAc, Trim(txtReference.Text), entryref, costAmt, "", "", 3, 0, "", _
                           "", Val(txtSuppAlias.Tag), costOfSalesAc & txtReference.Text, "", FCRt)
            _objTr.saveAccTrans()
            'UpdtClosBal(costOfSalesAc, costAmt)
            'credit entry [stock in hand]
            costAmt = costAmt * -1
            setAcctrDetValue(LinkNo, stockAc, Trim(txtReference.Text), entryref, costAmt, "", "", 3, 0, "", _
                           "", Val(txtSuppAlias.Tag), stockAc & txtReference.Text, "", FCRt)
            _objTr.saveAccTrans()
            'UpdtClosBal(stockAc, costAmt)
        End If
    End Sub
    Private Sub setInvCmnValue(ByVal InvTrid As Long)
        With _objInv
            Dt = DateValue(Date.Now)
            .TrId = InvTrid
            .TrDate = DateValue(cldrdate.Value)
            .TrType = "DIS"
            .DocLstTxt = ""
            .InvTypeNo = Val(cmbVoucherTp.Tag)
            .SlsManId = cmbsalesman.Text
            .Prefix = Trim(txtprefix.Text)
            .InvNo = Val(numVchrNo.Text)
            .TrRefNo = Trim(txtprefix.Text) & IIf(txtprefix.Text = "", "", "/") & numVchrNo.Text ' Trim(txtReference.Text)
            .CSCode = Val(txtSuppAlias.Tag)
            .PSAcc = Val(txtPurchAlias.Tag)
            .JobCode = txtJob.Text
            .ImpDoc = 0
            .UserId = CurrentUser
            .IsFC = (cmbfc.Text <> "")
            .FCRate = FCRt
            .NFraction = NDec
            .FC = cmbfc.Text
            .Discount = CDbl(numDisc.Text) * FCRt
            .TrDescription = Trim(txtDescr.Text)
            .TypeNo = getVouchernumber("DIS")
            .EnaJob = False
            .DocDefLoc = ""
            .BrId = ActBr
            .OthCost = OthCost
            If Val(txtsmanP.Text) = 0 Then txtsmanP.Text = 0
            .Discount1 = CDbl(txtsmanP.Text)
            .NetAmt = CDbl(lblNetAmt.Text) * FCRt
            .LPO = txtReference.Text
            'Dim dt As Date = getServerDate()
            .CrtDt = Dt
            .DelDate = Dt
            .DueDate = DateValue(clrDuedate.Value)
            .DocDate = Dt
            .SuppInvDate = Dt
            .TermsId = ""
            .MchName = MACHINENAME
            .isModi = isModi
            .lpoclass = ""
            .rndoff = txtroundOff.Text * IIf(cmbsign.SelectedIndex = 1, -1, 1)
            'If TaxType is 1 then invoice is interstate invoice otherwise intrastate invoice
            .TaxType = Val(lblstatecode.Tag)
            .OthrCust = txtcustAddress.Text

        End With

    End Sub
    Private Sub setInvDetValue(ByVal Invid As Long, ByVal PPerU As Single, ByVal i As Integer)
        With grdVoucher

            _objInv.dtTrId = Invid
            _objInv.ItemId = IIf(.Item(ConstSlNo, i).Value.ToString <> "M", Val(.Item(ConstItemID, i).Value), 0)
            _objInv.Unit = .Item(ConstUnit, i).Value
            _objInv.TrQty = CDbl(.Item(ConstQty, i).Value) * PPerU
            _objInv.UnitCost = CDbl(.Item(ConstActualPrice, i).Value) * FCRt / PPerU 'CDbl(.TextMatrix(i, 10)) / PPerU

            _objInv.taxP = CDbl(grdVoucher.Item(ConstTaxP, i).Value)
            _objInv.taxAmt = (CDbl(grdVoucher.Item(ConstTaxAmt, i).Value) * FCRt) / CDbl(PPerU)
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
            _objInv.TrTypeNo = getVouchernumber("DIS")
            _objInv.TrDateNo = getDateNo(CDate(cldrdate.Value))
            _objInv.TrType = "DIS"
            _objInv.id = Val(.Item(ConstId, i).Value)
            _objInv.WarrentyName = .Item(ConstCardtype, i).Value
            _objInv.SerialNo = Trim(.Item(ConstCardNumber, i).Value & "")
            If IsDate(.Item(ConstWarrentyExpiry, i).Value) Then
                _objInv.WarrentyExpDate = DateValue(.Item(ConstWarrentyExpiry, i).Value)
            Else
                _objInv.WarrentyExpDate = DateValue("01/01/1950")
            End If
            _objInv.HSNCode = Trim(.Item(ConstBarcode, i).Value & "")
            If Val(.Item(ConstIGSTP, i).Value & "") = 0 Then .Item(ConstIGSTP, i).Value = 0
            _objInv.IGSTP = CDbl(.Item(ConstIGSTP, i).Value)
            If Val(.Item(ConstIGSTAmt, i).Value & "") = 0 Then .Item(ConstIGSTAmt, i).Value = 0
            _objInv.IGSTAmt = CDbl(.Item(ConstIGSTAmt, i).Value)

            If Val(.Item(ConstCGSTP, i).Value & "") = 0 Then .Item(ConstCGSTP, i).Value = 0
            _objInv.CSGTP = CDbl(.Item(ConstCGSTP, i).Value)
            If Val(.Item(ConstCGSTAmt, i).Value & "") = 0 Then .Item(ConstCGSTAmt, i).Value = 0
            _objInv.CGSTAMT = CDbl(.Item(ConstCGSTAmt, i).Value)
            If Val(.Item(ConstSGSTP, i).Value & "") = 0 Then .Item(ConstSGSTP, i).Value = 0
            _objInv.SGSTP = CDbl(.Item(ConstSGSTP, i).Value)
            If Val(.Item(ConstSGSTAmt, i).Value & "") = 0 Then .Item(ConstSGSTAmt, i).Value = 0
            _objInv.SGSTAmt = CDbl(.Item(ConstSGSTAmt, i).Value)
            _objInv.Smancode = Trim(.Item(Constsman, i).Value & "")
            If Val(.Item(ConstStartReading, i).Value) = 0 Then .Item(ConstStartReading, i).Value = 0
            If Val(.Item(ConstCardNumber, i).Value) = 0 Then .Item(ConstCardNumber, i).Value = 0
            _objInv.StartingReading = CDbl(.Item(ConstStartReading, i).Value)
            _objInv.EndingReading = CDbl(.Item(ConstCardNumber, i).Value)
            _objInv.MeterCode = Trim(.Item(ConstMeterCode, i).Value & "")
            _objInv.disccardid = Val(.Item(ConstImpDocId, i).Value)
            If _objInv.ItemId = 0 Then
                _objInv.TrQty = 1
                '_objInv.UnitCost = 1
                '_objInv.taxP = 1
                '_objInv.taxAmt = 1
                '_objInv.UnitDiscount = 0
            End If
            'addtodtTb(Invid, .Item(ConstItemID, i).Value, .Item(ConstId, i).Value)

        End With
    End Sub
    Private Sub addtodtTb(ByVal trid As Long, ByVal itemid As Long, ByVal id As Long)
        Dim dtrow As DataRow
        dtrow = dtTb.NewRow
        dtrow("trid") = trid
        dtrow("itemid") = itemid
        dtrow("id") = id
        dtTb.Rows.Add(dtrow)
    End Sub
    Private Sub setAcctrCmnValue(ByVal InvTrid As Long, ByVal LinkNo As Long)
        _objTr.JVType = "DIS"
        _objTr.JVDate = DateValue(cldrdate.Value)
        _objTr.PreFix = Trim(txtprefix.Text)
        _objTr.JVNum = Val(numVchrNo.Text)
        _objTr.JVTypeNo = getVouchernumber("DIS")
        _objTr.UserId = Trim(CurrentUser & "")
        _objTr.MchName = MACHINENAME
        _objTr.CrtDtTm = Dt
        _objTr.TypeNo = getVouchernumber("DIS")
        '_objTr.VrDescr = Trim(txtpayee.Text)
        _objTr.IsModi = IIf(LinkNo > 0, 1, 0)
        _objTr.LinkNo = InvTrid
    End Sub
    Private Sub setAcctrDetValue(ByVal lnkNo As Long, ByVal jbIndex As Integer)
        With _objTr
            .trLinkNo = lnkNo
            .AccountNo = JobAcc(jbIndex).Acc
            .Reference = Trim(txtprefix.Text) & IIf(txtprefix.Text = "", "", "/") & numVchrNo.Text
            .EntryRef = Strings.Left(Trim(txtDescr.Text) & IIf(JobAcc(jbIndex).Job = "", "", " / " & JobAcc(jbIndex).Job) & " / " & txtSuppName.Text, 249)
            .DealAmt = JobAcc(jbIndex).Amt * FCRt
            .FCAmt = JobAcc(jbIndex).Amt * FCRt
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
            .DueDate = DateValue(clrDuedate.Value)
            .DocDate = Dt
            .SuppInvDate = Dt
        End With
    End Sub
    Private Sub setAcctrDetValue(ByVal lnkNo As Long, ByVal AccountNo As Long, ByVal Reference As String, ByVal EntryRef As String, ByVal DealAmt As Double, ByVal JobCode As String, ByVal JobStr As String, _
                                  ByVal TrInf As Short, ByVal OthCost As Short, ByVal LPO As String, ByVal TermsId As String, ByVal CustAcc As Integer, ByVal AccWithRef As String, _
                                  ByVal CurrencyCode As String, ByVal CurrRate As Double)
        With _objTr
            .trLinkNo = lnkNo
            .AccountNo = AccountNo
            .Reference = Trim(txtprefix.Text) & IIf(txtprefix.Text = "", "", "/") & numVchrNo.Text
            .EntryRef = EntryRef
            .DealAmt = DealAmt
            .JobCode = JobCode
            .JobStr = JobStr
            .CurrRate = CurrRate
            .CurrencyCode = CurrencyCode ' IIf(chkFC.Checked = True, txtFC.Text, "")
            .TrInf = TrInf
            .OthCost = OthCost
            .TermsId = TermsId
            .CustAcc = CustAcc
            .AccWithRef = AccWithRef
            .LPONo = LPO
            Dim dtLPO As Date = IIf(chkDate(cldrdate.Value), cldrdate.Value, DateValue(cldrdate.Value))
            Dim dtDue As Date = DateValue(clrDuedate.Value)
            Dim dtSup As Date = DateValue(cldrdate.Value)
            .DocDate = dtLPO
            .SuppInvDate = dtSup
            .DueDate = dtDue
        End With
    End Sub

    Private Sub ldSman()
        AddtoCombo(cmbsalesman, "SELECT SManCode FROM SalesmanTb", True, False)
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

    Private Sub returnFcrt()
        Dim dt As DataTable = _objcmnbLayer._fldDatatable("Select CurrencyRate,[Decimal Places] from CurrencyTb where CurrencyCode='" & cmbfc.Text & "'", False)
        If (dt.Rows.Count > 0) Then
            NDec = dt(0)("Decimal Places")
            lnumFormat = "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))
            txtfcrt.Text = Format(CDbl(dt(0)("CurrencyRate")), lnumFormat)
        Else
            txtfcrt.Text = Format(0, numFormat)
            NDec = 2
        End If
    End Sub
    Private Function blockInvoicing() As Boolean
        If getRight(89, CurrentUser) And Val(lbllimit.Text) > 0 Then
            If CDbl(lblbalance.Text) + CDbl(lblNetAmt.Text) > CDbl(lbllimit.Text) Then
                MsgBox("Credit Limit Exeeds! You cannot post the invoice", MsgBoxStyle.Exclamation)
                Return True
            End If
        End If
        If getRight(91, CurrentUser) Then
            If CDbl(lblInvoices.Text) > 0 Then
                MsgBox("Due Invoices Found! You cannot post the invoice", MsgBoxStyle.Exclamation)
                Return True
            End If
        End If
    End Function

    Private Sub SalesInvoice_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
        If Not fSelect Is Nothing Then fSelect.Close() : fSelect = Nothing
        If Not fProductEnquiry Is Nothing Then fProductEnquiry.Close() : fProductEnquiry = Nothing
        If Not fSlctDoc Is Nothing Then fSlctDoc.Close() : fSlctDoc = Nothing
        If Not fSerialno Is Nothing Then fSerialno.Close() : fSerialno = Nothing
    End Sub

    Private Sub SalesOrderFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            StrAccMastSrch = "SELECT AccDescr, Alias, ClosingBal As Balance, OpnBal, AccountNo, S1AccHd.S1AccId, AccSetId, GrpSetOn,accid FROM" & _
                            " AccMast INNER JOIN S1AccHd ON S1AccHd.S1AccId = AccMast.S1AccId "
            'btnNext_Click(btnNext, New System.EventArgs())
            'dtTax = _objcmnbLayer._fldDatatable("SELECT vatcode,convert(decimal,0) Amount,collectionAC,paymentAC,vat From VatMasterTb", False)
            If ShowTaxOnInventory Then
                dtTax = _objcmnbLayer._fldDatatable("SELECT vatcode,convert(money, 0) Amount,collectionAC,paymentAC,vat From VatMasterTb", False)
            ElseIf EnableGST Then
                CreateTaxTable(dtTax)
            End If
            dtTb = _objcmnbLayer._fldDatatable("SELECT trid,itemid,id FROM ItmInvTrTb WHERE TRID=0")
            _objcmnbLayer.dtSerialNo = createdtSerialNo()
            SetGridHead()
            crtSubVrs(cmbVoucherTp, 12, True)
            lnumFormat = numFormat
            FCRt = 1
            OthCost = 0
            chgbyprg = True
            Me.Text = "Store Receipt "
            cldrdate.Value = Format(Date.Now, DtFormat)
            NDec = NoOfDecimal
            loadedTrId = 0
            chgbyprg = False
            ldSman()
            LodCurrency()

            'cmbVrType_SelectedIndexChanged(sender, e)
            'numVchrNo.Select()
            ChgId = False
            If isModi Then
                btnModify_Click(btnModify, New System.EventArgs)
                If userType Then
                    btnupdate.Tag = IIf(getRight(162, CurrentUser), 1, 0)
                    btndelete.Tag = IIf(getRight(163, CurrentUser), 1, 0)
                Else
                    btnupdate.Tag = 1
                    btndelete.Tag = 1
                End If

                btndelete.Text = "Delete"
            Else
                AddNewClick()
                If userType Then
                    btnupdate.Tag = IIf(getRight(161, CurrentUser), 1, 0)
                Else
                    btnupdate.Tag = 1
                End If
                btndelete.Text = "Clear"
                btndelete.Tag = 1
            End If
            Timer1.Enabled = True
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
        dtTable = _objcmnbLayer._fldDatatable("SELECT * FROM ItmInvCmnTb WHERE TrType = 'DIS' AND InvNo = " & Val(numVchrNo.Text) & " AND PreFix = '" & MkDbSrchStr(Trim(txtprefix.Text), True) & "'")
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
            'Dim dtExstSrlno As DataTable
            Dim itemidStr As String
            Dim r As Integer
            itemidStr = ""
            For r = 0 To grdVoucher.RowCount - 1
                itemidStr = itemidStr & IIf(itemidStr = "", "", ",") & Val(.Item(ConstItemID, r).Value)
            Next
            'dtSrlNo = _objcmnbLayer._fldDatatable("SELECT SerialNo,itemid FROM ItmInvTrTb Left Join ItmInvCmnTb on ItmInvTrTb.Trid=ItmInvCmnTb.trid WHERE TrType='IS' AND ItmInvCmnTb.Trid<>" & loadedTrId)
            'dtExstSrlno = _objcmnbLayer._fldDatatable("SELECT SerialNo,ItemId FROM serialnotb ")
            'Dim _qurey As EnumerableRowCollection(Of DataRow)

            For r = 0 To .RowCount - 1 '- 1
                If .Item(ConstCardNumber, r).Value = "" Then
                    .Rows(r).Selected = True
                    .CurrentCell = .Item(ConstQty, r)
                    MsgBox("Card Number cannot be Blank !!", vbExclamation)
                    .FirstDisplayedScrollingRowIndex = r
                    GoTo Ter
                End If
                If .Item(ConstCardtype, r).Value = "" Then
                    .Rows(r).Selected = True
                    .CurrentCell = .Item(ConstCardtype, r)
                    MsgBox("Card Type cannot be Blank !!", vbExclamation)
                    .FirstDisplayedScrollingRowIndex = r
                    GoTo Ter
                End If
                If .Item(ConstCardNumber, r).Value <> "" Then
                    Dim dt As DataTable
                    dt = _objcmnbLayer._fldDatatable("SELECT id FROM ItmInvTrTb LEFT JOIN ItmInvCmnTb ON ItmInvCmnTb.Trid=ItmInvTrTb.Trid WHERE ItmInvCmnTb.trid<>" & loadedTrId & " AND ISNULL(optID,0)<>1 AND SerialNo='" & Trim(.Item(ConstCardNumber, r).Value & "") & "'")
                    If dt.Rows.Count > 0 Then
                        If optid = 0 Then
                            .Rows(r).Selected = True
                            .CurrentCell = .Item(ConstCardNumber, r)
                            MsgBox("Card Number already exist! Now you can renew the card", vbExclamation)
                            .FirstDisplayedScrollingRowIndex = r
                            GoTo Ter
                        End If
                    Else
                        If optid = 1 Then
                            .Rows(r).Selected = True
                            .CurrentCell = .Item(ConstCardNumber, r)
                            MsgBox("Card Number Not exist", vbExclamation)
                            .FirstDisplayedScrollingRowIndex = r
                            GoTo Ter
                        End If
                    End If
                End If
                If Val(.Item(ConstQty, r).Value) = 0 Then .Item(ConstQty, r).Value = 0
                If Trim(.Item(ConstBarcode, r).Value & "") = "" And CDbl(.Item(ConstQty, r).Value) = 0 Or .Item(ConstSlNo, r).Value.ToString = "M" Then
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
                If chkremovealert.Checked = 0 Then
                    If CDbl(.Item(ConstUPrice, r).Value) = 0 Then
                        .Rows(r).Selected = True
                        .CurrentCell = .Item(ConstUPrice, r)
                        If MsgBox("Entered Price/Unit of Item [" & .Item(ConstItemCode, r).Value & "] is zero !!  Proceed ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo) = vbNo Then
                            .FirstDisplayedScrollingRowIndex() = r
                            GoTo Ter
                        End If
                    End If
                End If

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

    Private Sub fProductEnquiry_getSelected(ByVal ItemcODE As String, ByVal isacc As Integer) Handles fProductEnquiry.getSelected
        chgbyprg = True
        fProductEnquiry.Close()
        SrchText = ItemcODE
        grdVoucher.Item(ConstItemCode, grdVoucher.CurrentRow.Index).Value = ItemcODE
        chgItm = True
        Valid(grdVoucher.CurrentRow.Index, ConstItemCode)
        grdVoucher.CurrentCell = grdVoucher.Item(3, grdVoucher.CurrentRow.Index)
        grdBeginEdit()
        chgbyprg = False
    End Sub

    Private Sub txtroundOff_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        txtReference.Focus()
    End Sub

    Private Sub txtdp_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtdp.KeyDown
        If e.KeyCode = Keys.Return Then btnupdate.Focus()
    End Sub

    Private Sub numDisc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles numDisc.KeyDown
        If e.KeyCode = Keys.Return Then
            btnupdate.Focus()
        End If
    End Sub

    Private Sub numDisc_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles numDisc.KeyPress, txtdp.KeyPress, txtsmanP.KeyPress, txtfcrt.KeyPress, txtroundOff.KeyPress
        NumericTextOnKeypress(sender, e, chgbyprg, lnumFormat)
    End Sub

    Private Sub numDisc_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numDisc.TextChanged, txtroundOff.TextChanged
        If Not chgNumByPgm And Val(lblTotAmt.Text) > 0 Then
            chgNumByPgm = True
            'If Val(numDisc.Text) = 0 Then
            '    numDisc.Text = Format(0, lnumFormat)
            'End If
            txtdp.Text = Format((CDbl(numDisc.Text) * 100) / CDbl(lblTotAmt.Text), lnumFormat)

            chgPost = True
            calculate(False)
        End If
        chgNumByPgm = False
    End Sub

    Private Sub numDisc_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles numDisc.Validated
        calculate()
    End Sub

    Private Sub grdSrch_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSrch.CellDoubleClick
        activecontrolname = "grdVoucher"
        doSelect(2)
        chgbyprg = True
        grdVoucher.CurrentCell = grdVoucher.Item(1, grdVoucher.CurrentRow.Index)
        chgItm = True
        Valid(grdVoucher.CurrentCell.RowIndex, grdVoucher.CurrentCell.ColumnIndex)
        chgbyprg = False
        grdBeginEdit()
        plsrch.Visible = False
    End Sub


    Private Sub txtSuppAlias_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSuppAlias.Validated, txtSuppName.Validated
        If txtSuppAlias.Text = "" Then Exit Sub
        setCustomer()
    End Sub
    Private Sub setCustomer(Optional ByVal accid As Long = 0)
        Dim dt As DataTable
        Dim condition As String
        If accid > 0 Then
            condition = "where accid=" & accid
        Else
            condition = "where Alias='" & txtSuppAlias.Text & "'"
        End If
        dt = _objcmnbLayer._fldDatatable("SELECT AccMast.accid,Alias,AccDescr,Address1,Address2,Address3,Address4," & _
                                        "Phone,Fax,EMail,PT1,PT2,PT3,PT4,TrdLcno,TrdDate,CreditLimit,DueDays,SlsmanId,CurrencyCode,CountryCode,GSTIN,Remarks,isnull(custid,0)custid " & _
                                        " from AccMast LEFT JOIN AccMastAddr ON AccMast.accid=AccMastAddr.AccountNo " & _
                                        "LEFT JOIN (select customeraccount,custid from  CashCustomerTb) CashCustomerTb ON AccMast.accid=CashCustomerTb.customeraccount " & condition)
        If dt.Rows.Count > 0 Then
            txtSuppAlias.Tag = dt(0)("accid")
            If accid > 0 Then
                txtSuppAlias.Text = Trim("" & dt(0)("Alias"))
                txtSuppName.Text = Trim("" & dt(0)("AccDescr"))
            End If
            txtCashCustomer.Tag = dt(0)("custid")
            txtCashCustomer.Text = Trim("" & dt(0)("AccDescr"))
            txtcustAddress.Text = Trim(dt(0)("Address1") & "")
            If Trim(dt(0)("Address2") & "") <> "" Then
                txtcustAddress.Text = txtcustAddress.Text & vbCrLf & Trim(dt(0)("Address2") & "")
            End If
            If Trim(dt(0)("Address3") & "") <> "" Then
                txtcustAddress.Text = txtcustAddress.Text & vbCrLf & Trim(dt(0)("Address3") & "")
            End If
            If Trim(dt(0)("Address4") & "") <> "" Then
                txtcustAddress.Text = txtcustAddress.Text & vbCrLf & Trim(dt(0)("Address4") & "")
                'txtcustAddress.Text = txtcustAddress.Text & "  GSTIN: " & Trim(dt(0)("GSTIN") & "")
            End If
            'If Trim(dt(0)("GSTIN") & "") <> "" Then
            '    txtcustAddress.Text = txtcustAddress.Text & vbCrLf & "  GSTIN: " & Trim(dt(0)("GSTIN") & "")
            'End If
            If Trim(dt(0)("Phone") & "") <> "" Then
                txtcustAddress.Text = txtcustAddress.Text & vbCrLf & "Ph: " & Trim(dt(0)("Phone") & "")
            End If

            If accid = 0 Then
                cmbfc.Text = Trim(dt(0)("CurrencyCode") & "")
                cmbsalesman.Text = Trim(dt(0)("SlsmanId") & "")
            End If
            lblstatecode.Text = ("State Code : " & dt(0)("CountryCode"))
            If ("" & dt(0)("CountryCode")) = "" Then
                lblstatecode.Tag = ""
            Else
                If Trim(dt(0)("CountryCode") & "") <> Trim(stateCode & "") Then
                    lblstatecode.Tag = 1
                Else
                    lblstatecode.Tag = ""
                End If

            End If
            If Val(dt(0)("CreditLimit") & "") = 0 Then
                dt(0)("CreditLimit") = 0
            End If
            txtremarks.Text = Trim(dt(0)("Remarks") & "")
            lbllimit.Text = Format(Val(dt(0)("CreditLimit")), lnumFormat)
            Dim iNBal As Double = getAccBal(Val(txtSuppAlias.Tag))
            lblbalance.Text = Strings.Format(iNBal, lnumFormat)
            If IsDBNull(dt(0)("DueDays")) Then
                dt(0)("DueDays") = 0
            End If
            If Val(dt(0)("DueDays") & "") > 0 Then
                iNBal = getAccAegBal(Val(txtSuppAlias.Tag), DateValue(DateTime.Now), Val(dt(0)("DueDays")))
                lblInvoices.Text = Strings.Format(iNBal, lnumFormat)
                clrDuedate.Value = DateAdd(DateInterval.Day, Val(dt(0)("DueDays")), cldrdate.Value)
            End If
            If EnableGST Then CalculateGST()
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
            lblInvoices.Text = Format(0, lnumFormat)
            lblbalance.Text = Format(0, lnumFormat)
            lbllimit.Text = Format(0, lnumFormat)
        End If

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
            .strType = "DIS"
            .Text = "Select Sales Invoice"
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

    Private Sub txtDescr_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDescr.TextChanged, txtCashCustomer.TextChanged
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
            fRptFormat.RptType = "DIS"
            fRptFormat.ShowDialog()
            fRptFormat = Nothing
        Else
            PrepareRpt("DIS")
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
        ClearControls()
        isModi = False
        'btnNext_Click(btnNext, New System.EventArgs)
        enableCtrls(False)
        btnModify.Text = "&Modify"
        btndelete.Text = "Clear"
        isModi = False
        btnSlct.Visible = False
        numVchrNo.ReadOnly = True
        txtReference.Select()
        If userType Then
            btnupdate.Tag = IIf(getRight(45, CurrentUser), 1, 0)
        Else
            btnupdate.Tag = 1
        End If
        btndelete.Text = "Clear"
        btndelete.Tag = 1
        cmbVoucherTp_SelectedIndexChanged(cmbVoucherTp, New System.EventArgs)
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
        If MsgBox("You are going to REMOVE the Sales Invoice # " & numVchrNo.Text & Chr(13) & "Are you sure ?", vbOKCancel + vbQuestion + vbDefaultButton2) = vbOK Then

            Dim itemidsdatatable As New DataTable
            Dim trdate As Date
            itemidsdatatable = _objcmnbLayer._fldDatatable("SELECT Itemid,TrDate,SerialNo from ItmInvTrTb LEFT JOIN ItmInvCmnTb ON ItmInvTrTb.Trid=ItmInvCmnTb.Trid  WHERE ItmInvTrTb.TrId =" & loadedTrId)
            If itemidsdatatable.Rows.Count > 0 Then
                trdate = DateValue(itemidsdatatable(0)("TrDate"))
            End If
            _objInv.TrId = loadedTrId
            _objInv.TrType = "OUT"
            _objInv.deleteInventoryTransactions()
            For i = 0 To itemidsdatatable.Rows.Count - 1
                _objcmnbLayer._saveDatawithOutParm("UPDATE SerialNoTb SET SalesId=0 WHERE SerialNo='" & itemidsdatatable(i)("SerialNo") & "'")
                _objcmnbLayer._saveDatawithOutParm("delete from SerialNoTb where ISNULL(PurchaseId,0)=0 and ISNULL(SalesId,0)=0")
                _objcmnbLayer._saveDatawithOutParm("delete from WarrentyTrTb where SerialNo='" & itemidsdatatable(i)("SerialNo") & "'")

                _objInv.ItemId = itemidsdatatable(i)("Itemid")
                _objInv.TrDate = DateValue(itemidsdatatable(i)("TrDate"))
                _objInv.setcostAverageOnModification(UsrBr)
            Next
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
        dt = _objcmnbLayer._fldDatatable("SELECT Alias,AccDescr,AccountNo FROM AccMast WHERE AccountNO=" & AccountNo)
        If dt.Rows.Count > 0 Then
            txtSuppAlias.Text = dt(0)("Alias")
            txtSuppName.Text = dt(0)("AccDescr")
            txtSuppAlias.Tag = dt(0)("AccountNo")
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
        Me.Close()
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



    Private Sub fSelect_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fSelect.FormClosed
        fSelect = Nothing
    End Sub

    Private Sub ctmControl_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

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


    Private Sub txtroundOff_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
        calculate()
    End Sub

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        AddRow()
        grdVoucher.CurrentCell = grdVoucher.Item(ConstCardtype, grdVoucher.CurrentRow.Index)
        chgbyprg = True
        grdVoucher.BeginEdit(True)
        chgbyprg = False
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
        With grdVoucher
            If e.ColumnIndex = ConstSlNo Then
                .Item(ConstSlNo, .CurrentCell.RowIndex).Value = IIf(Val(.Item(ConstSlNo, .CurrentCell.RowIndex).Value) = 0, "", "M")
                reArrangeNo()
            ElseIf e.ColumnIndex = ConstUnit Then
                If .Item(ConstB, .CurrentCell.RowIndex).Value = "B" Then
                    .Item(ConstB, .CurrentCell.RowIndex).Value = "P1"
                    dt = _objcmnbLayer._fldDatatable("SELECT P1Unit U,P1Fra F,LastPurchCost FROM InvItm WHERE Itemid=" & Val(.Item(ConstItemID, .CurrentCell.RowIndex).Value))
                ElseIf .Item(ConstB, .CurrentCell.RowIndex).Value = "P1" Then
                    .Item(ConstB, .CurrentCell.RowIndex).Value = "P2"
                    dt = _objcmnbLayer._fldDatatable("SELECT P2Unit U,P2Fra F,LastPurchCost FROM InvItm WHERE Itemid=" & Val(.Item(ConstItemID, .CurrentCell.RowIndex).Value))
                ElseIf .Item(ConstB, .CurrentCell.RowIndex).Value = "P2" Then
u:
                    .Item(ConstB, .CurrentCell.RowIndex).Value = "B"
                    dt = _objcmnbLayer._fldDatatable("SELECT Unit U,1 F,LastPurchCost FROM InvItm WHERE Itemid=" & Val(.Item(ConstItemID, .CurrentCell.RowIndex).Value))
                End If
                If dt Is Nothing Then GoTo u
                If dt.Rows.Count > 0 Then
                    .Item(ConstUnit, .CurrentCell.RowIndex).Value = dt(0)("U")
                    .Item(ConstPMult, .CurrentCell.RowIndex).Value = dt(0)("F")
                    Dim cost As Double
                    cost = getPurchAmt(dt(0)("LastPurchCost"), Val(txtSuppAlias.Tag), Val(.Item(ConstItemID, .CurrentCell.RowIndex).Value))
                    cost = cost * Val(.Item(ConstPMult, .CurrentCell.RowIndex).Value)
                    .Item(ConstActualPrice, .CurrentCell.RowIndex).Value = cost
                    .Item(ConstUPrice, .CurrentCell.RowIndex).Value = Format(cost, lnumFormat)
                    calculate()
                End If

            End If
        End With
    End Sub


    Private Sub TabPage1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub grdVoucher_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellEndEdit
        Dim col As Integer = e.ColumnIndex
        Dim ndec1 As Integer
        If col = ConstQty Or col = ConstUPrice Or col = ConstDisAmt Or col = ConstLTotal Or col = ConstTaxP Or col = ConstDisP Then
            If col = ConstQty Or col = ConstCardNumber Then
                ndec1 = grdVoucher.Item(ConstPFraction, grdVoucher.CurrentRow.Index).Value
            Else
                ndec1 = NDec
            End If
            If Val(grdVoucher.Item(col, e.RowIndex).Value) = 0 Then grdVoucher.Item(col, e.RowIndex).Value = 0
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

    Private Sub cldrdate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cldrdate.KeyDown
        If e.KeyCode = Keys.Return Then
            txtSuppName.Focus()
        End If

    End Sub

    Private Sub cldrdate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cldrdate.ValueChanged
        chgPost = True
    End Sub

    Private Sub btnimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnimport.Click
        isImport = True

        fSlctDoc = New SelectInvTr
        With fSlctDoc
            .strType = "IP"
            .Text = "Select Purchase Invoice"
            .ShowDialog()
        End With
        If Not fSlctDoc Is Nothing Then fSlctDoc = Nothing
    End Sub

    Private Sub grdSrch_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSrch.CellContentClick

    End Sub

    Private Sub cmbVoucherTp_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVoucherTp.SelectedIndexChanged
        If chgbyprg = True Then Exit Sub
        chgbyprg = True
        NextNumber()
        chgbyprg = False
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        If EnableWarranty = False Then resizeGridColumn(grdVoucher, ConstDescr)
    End Sub

    Private Sub SalesInvoice_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Timer1.Enabled = True
    End Sub




    Private Sub cmbsalesman_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbsalesman.SelectedIndexChanged
        If Not chgbyprg Then
            returnSalesmanAccount(True)
            chgPost = True
        End If
    End Sub

    Private Sub returnSalesmanAccount(Optional ByVal clearIfblank As Boolean = True)
        Dim dt As DataTable = _objcmnbLayer._fldDatatable("Select accountno,comP,DisP from SalesmanTb where SManCode='" & cmbsalesman.Text & "'", False)
        lblSCAmt.Text = "0.00"
        If (dt.Rows.Count > 0) Then
            If IsDBNull(dt(0)("comP")) Then
                dt(0)("comP") = 0
            End If
            If IsDBNull(dt(0)("DisP")) Then
                dt(0)("DisP") = 0
            End If
            cmbsalesman.Tag = dt(0)("accountno")
            chgbyprg = True
            txtsmanP.Text = Format(CDbl(dt(0)("comP")), lnumFormat)
            txtdp.Text = Format(CDbl(dt(0)("DisP")), lnumFormat)
            If Val(txtdp.Text) = 0 Then txtdp.Text = 0
            If Val(lblTotAmt.Text) = 0 Then lblTotAmt.Text = 0
            numDisc.Text = Format((CDbl(lblTotAmt.Text) * CDbl(txtdp.Text)) / 100, lnumFormat)
            chgbyprg = False
        ElseIf clearIfblank Then
            cmbsalesman.Tag = 0
            txtsmanP.Text = Format(0, lnumFormat)
            txtdp.Text = Format(0, lnumFormat)
            numDisc.Text = Format(0, lnumFormat)
            lblSCAmt.Text = "0.00"
        End If
        calculate(False)
    End Sub

    Private Sub cmbfc_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbfc.SelectedIndexChanged
        returnFcrt()
    End Sub

    Private Sub txtfcrt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtfcrt.KeyDown
        If e.KeyCode = Keys.Return Then
            txtDescr.Focus()
        End If
    End Sub

    Private Sub txtfcrt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtfcrt.TextChanged

    End Sub

    Private Sub txtfcrt_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtfcrt.Validated
        FCRt = txtfcrt.Text
    End Sub

    Private Sub calculateTaxFromUnitPrice(ByVal i As Integer)
        If chgUprice And chkcal.Checked Then
            chgbyprg = True
            With grdVoucher
                .Item(ConstActualPrice, i).Value = (CDbl(.Item(ConstUPrice, i).Value) * 100) / (CDbl(.Item(ConstTaxP, i).Value) + 100)
                .Item(ConstUPrice, i).Value = Format(.Item(ConstActualPrice, i).Value, lnumFormat)
            End With
        End If
        chgUprice = False
        chgbyprg = False
    End Sub


    Private Sub txtdp_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtdp.Validated
        calOthCost()
    End Sub


    Private Sub txtdp_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtdp.TextChanged
        If Not chgNumByPgm And Val(lblTotAmt.Text) > 0 Then
            chgNumByPgm = True
            If Val(txtdp.Text) = 0 Then txtdp.Text = 0
            numDisc.Text = Format((CDbl(lblTotAmt.Text) * CDbl(txtdp.Text)) / 100, lnumFormat)
            chgNumByPgm = False
            chgPost = True
            calculate(False)
        End If
    End Sub

    Private Sub btntax_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btntax.Click
        ShowTax.grdVoucher.DataSource = dtTax
        ShowTax.ShowDialog()
    End Sub

    Private Sub numPrintVchr_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numPrintVchr.TextChanged

    End Sub

    Private Sub btncancelgst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancelgst.Click
        tbgst.Visible = False
    End Sub

    Private Sub btnAddgst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddgst.Click
        setGstToGrdFromTabC()
    End Sub

    Private Sub txtCgst_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCgst.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtCgst_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCgst.KeyPress, txtSgst.KeyPress, txtIgst.KeyPress
        NumericTextOnKeypress(sender, e, chgbyprg, lnumFormat)
    End Sub

    Private Sub txtCgst_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCgst.TextChanged
        If chgbyprgN Then Exit Sub
        If Val(txtCgst.Tag) > 0 Then
            If Val(txtCgst.Text) = 0 Then txtCgst.Text = 0
            txtCgstAmt.Text = Format((CDbl(txtCgst.Tag) * CDbl(txtCgst.Text)) / 100, lnumFormat)
        End If
    End Sub

    Private Sub txtSgst_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSgst.TextChanged
        If chgbyprgN Then Exit Sub
        If Val(txtCgst.Tag) > 0 Then
            If Val(txtSgst.Text) = 0 Then txtSgst.Text = 0
            txtSgstAmt.Text = Format((CDbl(txtCgst.Tag) * CDbl(txtSgst.Text)) / 100, lnumFormat)
        End If
    End Sub

    Private Sub txtIgst_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIgst.TextChanged
        If chgbyprgN Then Exit Sub
        If Val(txtCgst.Tag) > 0 Then
            If Val(txtIgst.Text) = 0 Then txtIgst.Text = 0
            txtIgstAmt.Text = Format((CDbl(txtCgst.Tag) * CDbl(txtIgst.Text)) / 100, lnumFormat)
        End If
    End Sub

    Private Sub cmbsign_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbsign.SelectedIndexChanged
        calculate()
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

    Private Sub btnfind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnfind.Click
        Dim rindex As Integer
        Dim getValue As String
        getValue = SearchSequenceFromGrid(grdVoucher, cmbcolms.SelectedIndex + 1, txtSeq.Text, Val(btnfind.Tag) + 1)
        With grdVoucher
            If getValue <> "N" Then
                rindex = Val(getValue)
                .ClearSelection()

                .CurrentCell = .Item(cmbcolms.SelectedIndex + 1, rindex)
                .Rows(rindex).Selected = True
                .FirstDisplayedScrollingRowIndex = rindex
                btnfind.Tag = .CurrentRow.Index
            Else
                btnfind.Tag = -1
            End If
        End With
    End Sub
    Private Sub setItemInfo(ByVal itemid As Long)
        If dtItemInfo Is Nothing Then Exit Sub
        If dtItemInfo.Rows.Count = 0 And itemid > 0 Then
            getItemInfo(itemid)
            Exit Sub
        End If
        Dim bDatatable As DataTable
        Dim _qurey As EnumerableRowCollection(Of DataRow)
        _qurey = (From data In dtItemInfo.AsEnumerable() Where data("itemid") = itemid Select data)
        If _qurey.Count > 0 Then
            bDatatable = _qurey.CopyToDataTable()
        Else
            bDatatable = dtItemInfo.Clone
            If itemid > 0 Then
                getItemInfo(itemid)
                Exit Sub
            End If
        End If
        grdItemInfo.DataSource = bDatatable
        SetGridItemInfo()
    End Sub
    Private Sub getItemInfo(ByVal itemid As Long)
        Dim dt As DataTable
        If dtItemInfo Is Nothing Then
            dtItemInfo = _objcmnbLayer._fldDatatable("SELECT MRP,UnitPrice Price,((isnull(IGST,1)*UnitPrice)/100)+UnitPrice [Tax Price]," & _
                                                 "UnitPriceWS WSP,CostAvg [C Avg],LastPurchCost LPC,QIH,itemid FROM INVITM " & _
                                                  " LEFT JOIN GSTTb ON GSTTb.HSNCode=InvItm.HSNCode" & _
                                                 " where itemid=" & itemid)
            grdItemInfo.DataSource = dtItemInfo
            SetGridItemInfo()
        Else
            If itemid = 0 And dtItemInfo.Rows.Count > 0 Then dtItemInfo.Rows.Clear() : grdItemInfo.DataSource = dtItemInfo
            Dim _qurey As EnumerableRowCollection(Of DataRow)
            _qurey = (From data In dtItemInfo.AsEnumerable() Where data("itemid") = itemid Select data)
            If _qurey.Count = 0 Then
                dt = _objcmnbLayer._fldDatatable("SELECT MRP,UnitPrice Price,((isnull(IGST,1)*UnitPrice)/100)+UnitPrice [Tax Price]," & _
                                                 "UnitPriceWS WSP,CostAvg [C Avg],LastPurchCost LPC,QIH,itemid FROM INVITM " & _
                                                  " LEFT JOIN GSTTb ON GSTTb.HSNCode=InvItm.HSNCode" & _
                                                 " where itemid=" & itemid)
                If dt.Rows.Count > 0 Then
                    Dim dtRow As DataRow
                    dtRow = dtItemInfo.NewRow
                    dtRow("MRP") = dt(0)("MRP")
                    dtRow("Price") = dt(0)("Price")
                    dtRow("WSP") = dt(0)("WSP")
                    dtRow("C Avg") = dt(0)("C Avg")
                    dtRow("LPC") = dt(0)("LPC")
                    dtRow("QIH") = dt(0)("QIH")
                    dtRow("itemid") = dt(0)("itemid")
                    dtRow("Tax Price") = dt(0)("Tax Price")
                    dtItemInfo.Rows.Add(dtRow)
                Else
                    grdItemInfo.DataSource = dtItemInfo
                    SetGridItemInfo()
                End If

            End If
            setItemInfo(itemid)
        End If


    End Sub
    Private Sub SetGridItemInfo()
        SetGridProperty(grdItemInfo)
        With grdItemInfo
            .Columns("Price").Width = 70
            .Columns("Price").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("Price").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Price").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns("MRP").Width = 70
            .Columns("MRP").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("MRP").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("MRP").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns("WSP").Width = 70
            .Columns("WSP").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("WSP").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("WSP").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns("C Avg").Width = 70
            .Columns("C Avg").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("C Avg").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("C Avg").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns("Tax Price").Width = 70
            .Columns("Tax Price").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("Tax Price").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Tax Price").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


            .Columns("LPC").Width = 70
            .Columns("LPC").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("LPC").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("LPC").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns("QIH").Width = 70
            .Columns("QIH").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("QIH").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("QIH").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("itemid").Visible = False
        End With
    End Sub


    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        If enableDuplicateBill Then
            Dim frm As New PrintCopiesFrm
            frm.ShowDialog()
            If Val(frm.btnAddgst.Tag) = 0 Then Exit Sub
            Dim dt As DataTable
            dt = _objcmnbLayer._fldDatatable("SELECT case when Printed='true' then 1 else 0 end p from ItmInvCmnTb where  trtype='DIS'  and  InvNo=" & Val(numPrintVchr.Text) & " and  PreFix='" & txtPPrefix.Text & "'")
            If dt.Rows.Count > 0 Then
                If dt(0)(0) = 0 Then
                    PrepareRpt("DIS", True)
                End If
            End If
            If frm.chkduplicate.Checked Then
                PrepareRpt("DIS", True, 1)
            End If
            If frm.chktriplicate.Checked Then
                PrepareRpt("DIS", True, 2)
            End If
        Else
            PrepareRpt("DIS", True)
        End If

    End Sub

    Private Sub fCashCust_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fCashCust.FormClosed
        fCashCust = Nothing
    End Sub

    Private Sub fCashCust_selectcust(ByVal custname As String, ByVal custaddress As String, ByVal Cashcustid As Long, ByVal cardnumber As String, ByVal phone As String, ByVal gstn As String) Handles fCashCust.selectcust
        txtCashCustomer.Text = custname
        txtcustAddress.Text = custaddress
        txtCashCustomer.Tag = Cashcustid
    End Sub
    Private Sub loadSalesMultipleDebits(ByVal trid As Long)
        dtMultipleDebits = _objcmnbLayer._fldDatatable("SELECT Alias,AccDescr,accAmt,accid,dbtid FROM SalesMultipleDebitsTb " & _
                                                       "LEFT JOIN AccMast ON AccMast.accid=SalesMultipleDebitsTb.dbaccid where dbtrid=" & trid)
        _objcmnbLayer._saveDatawithOutParm("UPDATE SalesMultipleDebitsTb SET setremove=1 " & _
                                                  " WHERE dbtrid=" & trid)

    End Sub
    Private Sub saveMultipleDebits(ByVal trid As Long)
        Dim i As Integer
        For i = 0 To dtMultipleDebits.Rows.Count - 1
            If Val(dtMultipleDebits(i)("dbtid")) = 0 Then
                _objcmnbLayer._saveDatawithOutParm("INSERT INTO SalesMultipleDebitsTb (dbtrid,dbaccid,accAmt) VALUES" & _
                                                   "(" & trid & "," & Val(dtMultipleDebits(i)("accid")) & "," & CDbl(dtMultipleDebits(i)("accAmt")) & ")")
            Else
                _objcmnbLayer._saveDatawithOutParm("UPDATE SalesMultipleDebitsTb SET setremove=0 " & _
                                                   " WHERE dbtid=" & dtMultipleDebits(i)("dbtid"))
            End If
        Next
        _objcmnbLayer._saveDatawithOutParm("DELETE FROM SalesMultipleDebitsTb " & _
                                                 " WHERE setremove=1 AND dbtrid=" & trid)
    End Sub

    Private Sub btnmultipleDebit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Val(lblNetAmt.Text) = 0 Then
            MsgBox("Invoice Amount should be greater than Zero", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Dim fMultipleDebit As New MultipledebitsOnSalesFrm
        With fMultipleDebit
            .dt = dtMultipleDebits
            .lblinvoiceAmt.Text = "Invoice Amt. " & Format(CDbl(lblNetAmt.Text), numFormat)
            .lblinvoiceAmt.Tag = CDbl(lblNetAmt.Text)
            .ShowDialog()
            If Val(.btnupdate.Tag) > 0 Then
                dtMultipleDebits = .dt
            End If
        End With
    End Sub
    Private Sub checkWSCarditem(ByVal i As Integer)
        Dim dt As DataTable
chkagain:
        dt = _objcmnbLayer._fldDatatable("SELECT Itemid,Description,[Item Code] from invitm where [Item Code] ='WSCard'")
        If dt.Rows.Count > 0 Then
            With grdVoucher
                .Item(ConstItemCode, i).Value = dt(0)("Item Code")
                SrchText = dt(0)("Item Code")
                .Item(ConstDescr, i).Value = dt(0)("Description")
                .Item(ConstItemID, i).Value = dt(0)("Itemid")
            End With

        Else
            _objcmnbLayer._saveDatawithOutParm("Insert into invitm([Item Code],Description,itemCategory,donotchange) values('WSCard','Discount Card Item (WS)','Service',1)")
            GoTo chkagain
        End If
    End Sub

    Private Sub grdVoucher_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles grdVoucher.DataError

    End Sub
End Class