
Public Class POSInvoice

#Region "Public Variables"
    Public isModi As Boolean
    Public isCust As Boolean
    Public POScounter As String
#End Region
#Region "Local Variables"
    Private keytime As DateTime
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
    Private strCustomername As String
    Private strcustomerphone As String
    Private chgsearch As Boolean
    Private diableNegativeSale As Boolean
    Private dtRawMaterial As DataTable
    Private giftvoucherSalesValue, giftvoucherPointPerValue, giftvoucherPointValue As Integer
    Private DiscAcc As Long
    Private TrTypeNo As Integer
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
    Private chggrd As Boolean

    Private LcashAcc As Long
    Private LcashAmt As Double
    Private LcardAcc As Long
    Private LcardAmt As Double
    Private Lcardnumber As String
    Private LcreditAcc As Long
    Private LcreditName As String
    Private LcreditAmt As Double
    Private LtendAmt As Double
    Private LchangeAmt As Double
    Private cessdate As Date
    Private isShowItems As Boolean
    Private discountPointsCollected As Integer
    Private posType As Integer
    Private dtpoints As DataTable
    Private chgdiscount As Boolean
#End Region
#Region "constants"
    Private Const constitemname = 0
    Private Const constpsman = 1
    Private Const constpoints = 2
    Private Const constpTrid = 3
    Private Const constpDetId = 4
    Private Const constpItemid = 5
    Private Const constpRowIndex = 6
#End Region

#Region "Class Objet declarations"
    Private _objcmnbLayer As New clsCommon_BL
    Private _objTr As New clsAccountTransaction
    Private _objInv As New clsInvoice
    Private _objItmMast As New clsItemMast_BL
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
    Private WithEvents fTendered As TenderedEntryFrm
    Private WithEvents fQuickItem As QuickItem
    Private WithEvents fSR As New SalesReturnInvoice
    Private WithEvents fCashCust As CreateCashCustomerFrm
    Private WithEvents fwait As WaitMessageFrm
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
            InvList = _objcmnbLayer._fldDatatable("SELECT TrId FROM ItmInvCmnTb WHERE  TrId = " & FromTrId)
        Else
            InvList = _objcmnbLayer._fldDatatable("SELECT TrId FROM ItmInvCmnTb WHERE TrType = 'IS' AND InvNo = " & Val(numVchrNo.Text) & " AND PreFix = '" & MkDbSrchStr(Trim(txtprefix.Text), True) & "'")
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
        Dim dtset As DataSet
        'ItmInvCmnTb = _objcmnbLayer._fldDatatable("SELECT ItmInvCmnTb.*,jobname FROM ItmInvCmnTb LEFT JOIN JobTb ON ItmInvCmnTb.[Job Code]=JobTb.jobcode WHERE TrId = " & loadedTrId & " AND TrType = 'IS'")
        Dim itemquery As String = " SELECT ItmInvTrTb.*, [Item Code],Warrentyname,isSerialNo,fcode,FraCount," & _
                                          "isnull(itemCategory,'')itemCategory,collectionAC,vat,rgcess,rgcaccount,additionalcess FROM ItmInvTrTb LEFT JOIN InvItm ON InvItm.ItemId = ItmInvTrTb.ItemId " & _
                                          " LEFT JOIN InvItmPropertiesTb ON InvItmPropertiesTb.Itemid=invitm.itemid " & _
                                          "LEFT JOIN WarrentyMastertb ON WarrentyMastertb.Wid=ItmInvTrTb.WarrentyId " & _
                                          "LEFT JOIN FuelMeterReadingTb ON FuelMeterReadingTb.fmeterid=ItmInvTrTb.FuleMeterid " & _
                                          "LEFT JOIN UnitsTb ON UnitsTb.Units=ItmInvTrTb.Unit " & _
                                          "LEFT JOIN VatMasterTb ON InvItm.vatId=VatMasterTb.vatId " & _
                                          "LEFT JOIN (select vatcode rgccode,vat rgcess, collectionAC rgcaccount,vatid rgcid from  VatMasterTb)regularcess ON InvItm.regularcessid=regularcess.rgcid " & _
                                          "WHERE TrId = " & loadedTrId & " ORDER BY SlNo"

        dtset = _objcmnbLayer._ldDataset("SELECT ItmInvCmnTb.*,jobname,[Voucher Name],Ctgry,isnull(SalesmanTb.accountno,0) Smanacc," & _
                                         "Alias,AccDescr,isnull(linkno,0)linkno,rvprefix,rvno " & _
                                                  "FROM ItmInvCmnTb " & _
                                                  "LEFT JOIN PreFixTb ON PreFixTb.id=ItmInvCmnTb.invtypeno " & _
                                                  "LEFT JOIN JobTb ON ItmInvCmnTb.[Job Code]=JobTb.jobcode " & _
                                                  "LEFT JOIN SalesmanTb ON SalesmanTb.SManCode=ItmInvCmnTb.SlsManId " & _
                                                  "LEFT JOIN AccMast ON AccMast.accid=ItmInvCmnTb.PSAcc " & _
                                                  "LEFT JOIN AccTrCmn on ItmInvCmnTb.trid=AccTrCmn.lnkno " & _
                                                  "LEFT JOIN (select PreFix rvprefix, JVNum rvno, linkno rvlinkno from AcctrCmn) rv on ItmInvCmnTb.ISRVID=rv.rvlinkno " & _
                                                  "WHERE TrId = " & loadedTrId & " AND TrType = 'IS'" & itemquery, False)
        ItmInvCmnTb = dtset.Tables(0)

        chgbyprg = True
        ActBr = ""
        If ItmInvCmnTb.Rows.Count = 0 Then GoTo Quit
        getProtectUntil()
        cmbVoucherTp.Text = getVoucherName(Val(ItmInvCmnTb(0)("InvTypeNo") & ""))
        If Val(cmbholdedInvs.Tag) = 1 Then
            cldrdate.Value = Format(DateValue(Date.Now), DtFormat) ' "MM/dd/yyyy")
            lbldate.Text = Format(DateValue(Date.Now), DtFormat) ' "MM/dd/yyyy")
        Else
            cldrdate.Value = Format(ItmInvCmnTb(0)("TrDate"), DtFormat) ' "MM/dd/yyyy")
            lbldate.Text = Format(ItmInvCmnTb(0)("TrDate"), DtFormat) ' "MM/dd/yyyy")
        End If
        cmbholdedInvs.Tag = 0
        'If Not IsDBNull(ItmInvCmnTb(0)("TrDate")) Then
        '    cldrdate.Value = Format(ItmInvCmnTb(0)("TrDate"), DtFormat) ' "MM/dd/yyyy")
        '    lbldate.Text = Format(ItmInvCmnTb(0)("TrDate"), DtFormat) ' "MM/dd/yyyy")
        'End If

        'clrDuedate.Value = Format(ItmInvCmnTb(0)("DueDate"), DtFormat)
        txtprefix.Text = Trim(ItmInvCmnTb(0)("PreFix") & "")
        txtPPrefix.Text = txtprefix.Text
        txtprefix.Tag = ItmInvCmnTb(0)("PreFix")
        numVchrNo.Text = ItmInvCmnTb(0)("InvNo") 'Val(Mid(!InvNo, 3)) '!InvNo '
        numVchrNo.Tag = ItmInvCmnTb(0)("InvNo")
        numPrintVchr.Text = ItmInvCmnTb(0)("InvNo")  'CMNREC(12).FormattedText
        txtJob.Text = Trim("" & ItmInvCmnTb(0)("Job Code"))
        txtjobname.Text = Trim(ItmInvCmnTb(0)("jobname") & "")
        cmbsalesman.Text = Trim(ItmInvCmnTb(0)("SlsManId") & "")
        txtsalesman.Text = Trim(ItmInvCmnTb(0)("SlsManId") & "")
        txtCashCustomer.Text = Trim(ItmInvCmnTb(0)("CashCustName") & "")
        txtphone.Text = Trim(ItmInvCmnTb(0)("customerPhone") & "")
        cmbfc.Text = Trim(ItmInvCmnTb(0)("FC") & "")
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
        If Val(ItmInvCmnTb(0)("CSCode") & "") > 0 Then
            setCustomer(ItmInvCmnTb(0)("CSCode"))
        End If
        txtsramt.Text = Format(Val(ItmInvCmnTb(0)("SRDeduction") & ""), numFormat)
        txtsrno.Text = Trim(ItmInvCmnTb(0)("deductedSRNO") & "")
        txtcustAddress.Text = Trim(ItmInvCmnTb(0)("OthrCust") & "")
        txtfcrt.Text = Format(Val(ItmInvCmnTb(0)("FcRate") & ""), lnumFormat)
        FCRt = Val(ItmInvCmnTb(0)("FcRate") & "")
        If FCRt = 0 Then FCRt = 1
        txtPurchAlias.Tag = Val(ItmInvCmnTb(0)("PSAcc") & "")
        txtPurchAlias.Text = Trim(ItmInvCmnTb(0)("Alias") & "")
        txtPurchaseName.Text = Trim(ItmInvCmnTb(0)("AccDescr") & "")

        '        If Val(ItmInvCmnTb(0)("PSAcc") & "") > 0 Then
        '            sRs = _objcmnbLayer._fldDatatable("SELECT * FROM AccMast WHERE accid = " & Val(ItmInvCmnTb(0)("PSAcc") & ""))
        '        End If
        '        If Not sRs Is Nothing Then
        '            If sRs.Rows.Count > 0 Then
        '                txtPurchAlias.Tag = ItmInvCmnTb(0)("PSAcc")
        '                txtPurchAlias.Text = sRs(0)("Alias")
        '                txtPurchaseName.Text = sRs(0)("AccDescr")
        '            ElseIf txtPurchAlias.Text <> "" Then
        'slsacc:
        '                sRs = _objcmnbLayer._fldDatatable("SELECT * FROM AccMast WHERE alias = '" & txtPurchAlias.Text & "'")
        '                If sRs.Rows.Count > 0 Then
        '                    txtPurchAlias.Tag = sRs(0)("accid")
        '                    txtPurchAlias.Text = sRs(0)("Alias")
        '                    txtPurchaseName.Text = sRs(0)("AccDescr")
        '                End If
        '            End If
        '        Else
        '            GoTo slsacc
        '        End If


        txtReference.Text = Trim("" & ItmInvCmnTb(0)("LPO"))
        txtDescr.Text = Trim("" & ItmInvCmnTb(0)("TrDescription"))
        chgNumByPgm = True
        numDisc.Text = Format(Val(ItmInvCmnTb(0)("Discount") & "") / FCRt, lnumFormat)
        chgNumByPgm = False
        '-----------------------for Other Info ------------------------
        'If Not fOthInf Is Nothing Then FillOthInf()
        'If Not IsDBNull(ItmInvCmnTb(0)("SuppInvDate")) Then dtSuppDate.CtlText = DateValue(ItmInvCmnTb(0)("SuppInvDate"))
        On Error GoTo 0
        LddImpDocs = ""
        CTVol = IIf(IsDBNull(ItmInvCmnTb(0)("InvTtlVol")), 0, ItmInvCmnTb(0)("InvTtlVol"))
        CTWt = IIf(IsDBNull(ItmInvCmnTb(0)("InvTtlWt")), 0, ItmInvCmnTb(0)("InvTtlWt"))
        CTQty = IIf(IsDBNull(ItmInvCmnTb(0)("InvTtlQty")), 0, ItmInvCmnTb(0)("InvTtlQty"))
        OthCost = Format(Val(ItmInvCmnTb(0)("OthCost") & ""), lnumFormat)
        If Not IsDBNull(ItmInvCmnTb(0)("TrDate")) Then
            Protect = (ItmInvCmnTb(0)("TrDate") <= ProtectUntil)
        End If
        sRs = dtset.Tables(1)
        'If Not sRs Is Nothing Then
        '    If sRs.Rows.Count > 0 Then sRs.Clear()
        'End If

        'sRs = _objcmnbLayer._fldDatatable("SELECT ItmInvTrTb.*, [Item Code],Warrentyname,isSerialNo,fcode,FraCount,itemCategory,collectionAC,vat FROM ItmInvTrTb LEFT JOIN InvItm ON InvItm.ItemId = ItmInvTrTb.ItemId " & _
        '                           " LEFT JOIN InvItmPropertiesTb ON InvItmPropertiesTb.Itemid=invitm.itemid " & _
        '                           "LEFT JOIN WarrentyMastertb ON WarrentyMastertb.Wid=ItmInvTrTb.WarrentyId " & _
        '                           "LEFT JOIN FuelMeterReadingTb ON FuelMeterReadingTb.fmeterid=ItmInvTrTb.FuleMeterid " & _
        '                           "LEFT JOIN UnitsTb ON UnitsTb.Units=ItmInvTrTb.Unit " & _
        '                           "LEFT JOIN VatMasterTb ON InvItm.vatId=VatMasterTb.vatId " & _
        '                           "WHERE TrId = " & loadedTrId & " ORDER BY SlNo")
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
                        grdVoucher.Item(ConstPFraction, i).Value = "2"
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
                    grdVoucher.Item(ConstLocation, i).Value = Trim("" & sRs(i)("Warrentyname")) ' !Colour
                    grdVoucher.Item(ConstQty, i).Value = Format(sRs(i)("TrQty") / UPerPack, "#,##0" & IIf(Val(grdVoucher.Item(ConstPFraction, i).Value) = 0, "", "." & Strings.StrDup(CByte(grdVoucher.Item(ConstPFraction, i).Value), "0")))
                    grdVoucher.Item(ConstUPrice, i).Value = Format(sRs(i)("UnitCost") * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & lnumFormat)
                    grdVoucher.Item(ConstMRP, i).Value = Format(sRs(i)("MRP") * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0")))
                    grdVoucher.Item(ConstActualPrice, i).Value = sRs(i)("UnitCost") * UPerPack / FCRt
                    grdVoucher.Item(ConstLTotal, i).Value = Format((sRs(i)("TrQty") * sRs(i)("UnitCost")) * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & lnumFormat)
                    grdVoucher.Item(ConstUnitOthCost, i).Value = Format(sRs(i)("UnitOthCost") * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & lnumFormat)
                    grdVoucher.Item(ConstActualOthCost, i).Value = sRs(i)("UnitOthCost") * UPerPack / FCRt
                    grdVoucher.Item(ConstSerialNo, i).Value = sRs(i)("SerialNo")
                    'grdVoucher.Item(ConstMthd, i).Value = sRs(i)("Method")
                    'grdVoucher.Item(ConstUnitVal, i).Value = sRs(i)("UnitValue") * UPerPack / FCRt
                    'grdVoucher.Item(ConstDiscOther, i).Value = sRs(i)("DiscOther") * UPerPack / FCRt
                    grdVoucher.Item(ConstDiscOther, i).Value = sRs(i)("UnitDiscount") * UPerPack / FCRt
                    If Val(sRs(i)("DisP") & "") = 0 Then sRs(i)("DisP") = 0
                    grdVoucher.Item(ConstDisP, i).Value = Format(sRs(i)("DisP"), numFormat)
                    If Val(sRs(i)("ItemDiscount") & "") = 0 Then sRs(i)("ItemDiscount") = 0
                    grdVoucher.Item(ConstDisAmt, i).Value = Format(sRs(i)("ItemDiscount") * UPerPack / FCRt, lnumFormat)
                    grdVoucher.Item(Constsman, i).Value = Trim(sRs(i)("Smancode") & "")
                    grdVoucher.Item(ConstWoodQty, i).Value = Val(sRs(i)("WoodNetQty") & "")


                    grdVoucher.Item(ConstBarcode, i).Value = sRs(i)("HSNCode")
                    grdVoucher.Item(ConstCGSTP, i).Value = sRs(i)("CSGTP")
                    grdVoucher.Item(ConstCGSTAmt, i).Value = sRs(i)("CGSTAMT")

                    grdVoucher.Item(ConstSGSTP, i).Value = sRs(i)("SGSTP")
                    grdVoucher.Item(ConstSGSTAmt, i).Value = sRs(i)("SGSTAmt")

                    grdVoucher.Item(ConstIGSTP, i).Value = sRs(i)("IGSTP")
                    grdVoucher.Item(ConstIGSTAmt, i).Value = sRs(i)("IGSTAmt")

                    If Val(sRs(i)("vat") & "") = 0 Then sRs(i)("vat") = 0
                    grdVoucher.Item(Constcess, i).Value = Format(sRs(i)("vat"), lnumFormat)
                    If Val(sRs(i)("CessAmt") & "") = 0 Then sRs(i)("CessAmt") = 0
                    grdVoucher.Item(ConstcessAmt, i).Value = Format(sRs(i)("CessAmt") / FCRt, lnumFormat)
                    If Val(sRs(i)("collectionAC") & "") = 0 Then sRs(i)("collectionAC") = 0
                    grdVoucher.Item(ConstcessAc, i).Value = Val(sRs(i)("collectionAC"))

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

                    'If .Item(ConstSerialNo, i).Value <> "" Then
                    '    AddTodtSerialNo(.Item(ConstSerialNo, i).Value, Val(.Item(ConstItemID, .CurrentRow.Index).Value), .CurrentRow.Index, DateValue(.Item(ConstWarrentyExpiry, .CurrentRow.Index).Value), Val(.Item(ConstId, .CurrentRow.Index).Value))
                    'End If
                    calcualteLineTotal(i)
                Next
                If enableSerialnumber Then
                    _objcmnbLayer.dtSerialNo = _objcmnbLayer._fldDatatable("Select ItmSerialNo,Trid,DetId,Itemid,RowIndex-1 RowIndex,Wdate from SerialNoTrTb where trid=" & loadedTrId)
                End If
                'If enableMultiplePointsOnLineItem Then
                '    loadMultipleServicePoints(loadedTrId)
                'End If
            End If
        End With
        If sRs.Rows.Count > 0 Then sRs.Clear()
        'chkautoroundOff.Checked = False
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
        chgItm = False
        If userType Then
            btnPreview.Enabled = IIf(getRight(261, CurrentUser), False, True)
            btnprint.Enabled = IIf(getRight(261, CurrentUser), False, True)
        Else
            btnPreview.Enabled = True
            btnprint.Enabled = True
        End If
        If userType Then
            btnupdate.Tag = IIf(getRight(46, CurrentUser), 1, 0)
            btndelete.Tag = IIf(getRight(47, CurrentUser), 1, 0)
            diableNegativeSale = getRight(171, CurrentUser)
        Else
            btnupdate.Tag = 1
            btndelete.Tag = 1
        End If
        GoTo Quit
Err:
        If MsgBox(Err.Description & Chr(13) & "Retry?", vbRetryCancel + vbQuestion, "Warning During Opening Data File.") = vbRetry Then Resume
Quit:
        chgbyprg = False
        chkautoroundOff.Checked = enableAutoRoundOff
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
                    grdVoucher.Item(ConstLocation, i).Value = Trim("" & sRs(i)("Warrentyname")) ' !Colour
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
                    calcualteLineTotal(i)
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
        'Try
        '    Dim dtInv As DataTable
        '    _objcmnbLayer = New clsCommon_BL
        '    '//next number to Admission
        '    dtInv = _objcmnbLayer._fldDatatable("SELECT Prefix,InvNo FROM InvNos WHERE InvType='IS'")
        '    If dtInv.Rows.Count > 0 Then
        '        numVchrNo.Text = Val(dtInv(0)("InvNo"))
        '    Else
        '        numVchrNo.Text = 1
        '    End If
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
        Try
            Dim vrPrefix As String = ""
            Dim vrVoucherNo As Long
            Dim vrAccountNo1 As Long
            Dim vrAccountNo2 As Long
            Dim vrisb2b As Integer
            Dim vrisTax As Integer

            With cmbVoucherTp
                If .Items.Count > 0 Then
                    If .SelectedIndex < 0 Then .SelectedIndex = 0
                    cmbVoucherTp.Tag = getvrsId(cmbVoucherTp.Text, 4)
                    getVrsDet(Val(cmbVoucherTp.Tag), "IS", vrPrefix, vrVoucherNo, vrAccountNo1, vrAccountNo2)
                Else
                    getVrsDet(0, "IS", vrPrefix, vrVoucherNo, vrAccountNo1, vrAccountNo2)
                End If

            End With
            If Not isModi Or Val(numVchrNo.Text) = 0 Then
                numVchrNo.Text = vrVoucherNo
                txtprefix.Text = vrPrefix
            End If
            If Val(txtSuppName.Tag) = 0 Then
                txtSuppAlias.Tag = vrAccountNo2
            End If
            If Val(txtPurchaseName.Tag) = 0 Then
                txtPurchAlias.Tag = vrAccountNo1
            End If
            If withNonTaxBill Then
                If vrisTax > 0 Then
                    chktaxInv.Checked = True
                Else
                    chktaxInv.Checked = False
                End If
            End If
            'Dim dtAcc As DataTable
            'dtAcc = _objcmnbLayer._fldDatatable("SELECT Alias,AccDescr,GrpSetOn,GrpSetOn,accid FROM AccMast " & _
            '                                    "LEFT JOIN S1AccHd ON AccMast.S1AccId=S1AccHd.S1AccId WHERE accid=" & vrAccountNo1)
            'If dtAcc.Rows.Count > 0 Then
            '    txtPurchaseName.Text = dtAcc(0)("AccDescr")
            '    txtPurchAlias.Text = dtAcc(0)("Alias")
            '    txtPurchAlias.Tag = vrAccountNo1
            'Else
            '    txtPurchaseName.Text = ""
            '    txtPurchAlias.Text = ""
            '    txtPurchAlias.Tag = ""
            'End If
            'chgbyprg = True
            'dtAcc = _objcmnbLayer._fldDatatable("SELECT Alias,AccDescr,GrpSetOn,GrpSetOn,accid FROM AccMast " & _
            '                                   "LEFT JOIN S1AccHd ON AccMast.S1AccId=S1AccHd.S1AccId WHERE accid=" & vrAccountNo2)
            'If dtAcc.Rows.Count > 0 Then
            '    txtSuppName.Text = dtAcc(0)("AccDescr")
            '    txtSuppAlias.Text = dtAcc(0)("Alias")
            '    txtSuppAlias.Tag = vrAccountNo2
            '    setCustomer(Val(txtSuppAlias.Tag))
            'Else
            '    txtSuppName.Text = ""
            '    txtSuppName.Text = ""
            '    txtSuppAlias.Tag = ""
            'End If
            Dim qry As String = "SELECT Alias,AccDescr,GrpSetOn,GrpSetOn,accid FROM AccMast " & _
            "LEFT JOIN S1AccHd ON AccMast.S1AccId=S1AccHd.S1AccId WHERE accid=" & vrAccountNo1
            qry = qry & " SELECT Alias,AccDescr,GrpSetOn,GrpSetOn,accid FROM AccMast " & _
                      "LEFT JOIN S1AccHd ON AccMast.S1AccId=S1AccHd.S1AccId WHERE accid=" & vrAccountNo2
            Dim ds As DataSet
            ds = _objcmnbLayer._ldDataset(qry, False)

            Dim dtTable As DataTable
            dtTable = ds.Tables(0)
            If dtTable.Rows.Count > 0 Then
                txtPurchaseName.Text = dtTable(0)("AccDescr")
                txtPurchAlias.Text = dtTable(0)("Alias")
                txtPurchAlias.Tag = vrAccountNo1
            Else
                txtPurchaseName.Text = ""
                txtPurchAlias.Text = ""
                txtPurchAlias.Tag = ""
            End If
            dtTable.Rows.Clear()
            dtTable = ds.Tables(1)
            If dtTable.Rows.Count > 0 Then
                txtSuppName.Text = dtTable(0)("AccDescr")
                txtSuppAlias.Text = dtTable(0)("Alias")
                txtSuppAlias.Tag = vrAccountNo2
                setCustomer(Val(txtSuppAlias.Tag))
            Else
                txtSuppName.Text = ""
                txtSuppName.Text = ""
                txtSuppAlias.Tag = ""
            End If
            txtReference.Focus()
            txtSuppName.ReadOnly = True
            chgbyprg = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub txtCashCustomer_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCashCustomer.GotFocus

    End Sub

    Private Sub txtReference_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDescr.KeyDown, txtSuppName.KeyDown, _
                                                                                                                    txtSuppAlias.KeyDown, txtReference.KeyDown, _
                                                                                                                    txtjobname.KeyDown, txtJob.KeyDown, txtphone.KeyDown, _
                                                                                                                    txtCashCustomer.KeyDown, txtgiftvoucher.KeyDown, txtsalesman.KeyDown
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
                txtitemcode.Focus()
            ElseIf MyCtrl.Name = "txtgiftvoucher" Or MyCtrl.Name = "txtsalesman" Then
                txtitemcode.Focus()
            Else
                SendKeys.Send("{TAB}")
            End If
            If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
        ElseIf e.KeyCode = Keys.F2 Then
            If Not fMList Is Nothing Then fMList.Visible = False
            Select Case MyCtrl.Name
                Case "txtSuppAlias", "txtSuppName"
                    _srchTxtId = IIf(MyCtrl.Name = "txtSuppAlias", 1, 2)
                    ldSelect(1)
                Case "txtJob"
                    _srchTxtId = 3
                    ldSelect(3)
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
        chgPost = True
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
            If Not isFromTexbox Then
                x = Me.Width - fMList.Width - 100
                y = Me.Height - fMList.Height - 100
            Else
                If Not alreadyOpened Then
                    fMList.Width = fMList.Width + 100
                    fMList.resizecolum = 1
                End If
                x = Me.Left + 100
                y = Me.Top + 250
            End If
            PopupLoc = New Point(x, y)
            If fMList.Visible = False Then
                Select Case _srchTxtId
                    Case 1, 2
                        SetFmlist(fMList, 3)
                    Case 3 'job 
                        SetFmlist(fMList, 8)
                    Case 4 'item master 
                        SetFmlist(fMList, 1)
                    Case 5, 6 'cash customer 
                        SetFmlist(fMList, 30)
                    Case 7 'Gift Voucher 
                        SetFmlist(fMList, 38)
                    Case 8 'salesman 
                        SetFmlist(fMList, 12)
                End Select
                fMList.loc = PopupLoc
                fMList.Show(fMainForm)
                fMList.Visible = True
            End If
        End If
        Select Case _srchTxtId
            Case 1   'Customer Phone
                fMList.SearchIndex = 2
                fMList.SearchIndexDescr = 1
                fMList_doFocus()
                fMList.Search(txtphone.Text)
                txtSuppAlias.Text = fMList.AssignList(txtphone, lstKey, chgbyprg)
            Case 2   'Customer name
                fMList.SearchIndex = 0
                fMList.SearchIndexDescr = 1
                fMList_doFocus()
                fMList.Search(txtCashCustomer.Text)
                txtSuppAlias.Text = fMList.AssignList(txtCashCustomer, lstKey, chgbyprg)
            Case 3   'job
                fMList.SearchIndex = 0
                'fMList.SearchIndexDescr = 0
                fMList_doFocus()
                fMList.Search(txtJob.Text)
                fMList.AssignList(txtJob, lstKey, chgbyprg)
            Case 4   'Item Master
                fMList.SearchIndex = 0
                fMList_doFocus()
                'fMList.Search(txtitemcode.Text, True, chkSearch.Checked)
                fMList.Search(txtitemcode.Text, "", True)
                fMList.AssignList(txtitemcode, lstKey, chgbyprg)
                txtitemcode.SelectionStart = Len(txtitemcode.Text)
            Case 5   'Cash customer phone 
                fMList.SearchIndex = 1
                fMList.SearchIndexDescr = 0
                fMList_doFocus()
                fMList.Search(txtphone.Text)
                fMList.AssignList(txtphone, lstKey, chgbyprg)
            Case 6   'Cash customer name
                fMList.SearchIndex = 0
                fMList.SearchIndexDescr = 1
                fMList_doFocus()
                fMList.Search(txtCashCustomer.Text)
                fMList.AssignList(txtCashCustomer, lstKey, chgbyprg)
            Case 7   'Gift Voucher
                fMList.SearchIndex = 0
                fMList.SearchIndexDescr = 1
                fMList_doFocus()
                fMList.Search(txtgiftvoucher.Text)
                fMList.AssignList(txtgiftvoucher, lstKey, chgbyprg)
            Case 8   'salesman
                fMList.SearchIndex = 1
                fMList.SearchIndexDescr = 1
                fMList_doFocus()
                fMList.Search(txtsalesman.Text)
                txtsalesman.Tag = fMList.AssignList(txtsalesman, lstKey, chgbyprg)
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
                txtCashCustomer.Text = ItmFlds(0)
                txtSuppAlias.Text = ItmFlds(1)
                txtphone.Text = ItmFlds(2)
                txtphone.Tag = ItmFlds(3)
            Case 3
                txtJob.Text = ItmFlds(0)
            Case 4
                txtitemcode.Text = ItmFlds(0)
                txtitemcode.Tag = ItmFlds(0)
            Case 5, 6
                txtphone.Text = ItmFlds(1)
                txtphone.Tag = ItmFlds(2)
                txtCashCustomer.Text = ItmFlds(0)
            Case 7
                txtphone.Tag = ItmFlds(2)
                txtgiftvoucher.Text = ItmFlds(0)
            Case 8
                txtsalesman.Tag = ItmFlds(1)
                txtsalesman.Text = ItmFlds(0)
        End Select
        chgbyprg = False
    End Sub
    Private Sub SetGridHead()
        chgbyprg = True
        SetGridHeadEntryProperty(grdVoucher)
        With grdVoucher
            .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular)
            .RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.0!)
            Dim i As Integer
            For i = 1 To ConstUPrice
                cmbcolms.Items.Add(.Columns(i).HeaderText)
            Next
            '.Columns(ConstBarcode).Width = 120
            .Columns(ConstItemCode).ReadOnly = True
            .Columns(ConstDescr).ReadOnly = True
            .Columns(ConstMRP).Visible = enableMRPinSales
            .Columns(ConstBarcode).Visible = False
            .Columns(ConstSerialNo).Visible = enableSerialnumber
            .Columns(ConstManufacturingdate).Visible = False
            .Columns(ConstWarrentyExpiry).Visible = enableExpiryDateInPOS
            .Columns(ConstFocQty).Visible = False
            '.Columns(ConstDisAmt).Visible = False
            '.Columns(ConstDisP).Visible = False
            .Columns(Constsman).Visible = enableItemwiseSalesman
            'If enableMultipleBarcodeOnItem Then
            '    .Columns(ConstSerialNo).HeaderText = "Barcode"
            '    .Columns(ConstSerialNo).ReadOnly = True
            '    .Columns(ConstSerialNo).Visible = True
            'End If
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

    Private Sub AddRow(Optional ByVal tocheck As Boolean = False, Optional ByVal qty As Double = 0)
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
            .Item(ConstQty, i).Value = qty
            .Item(ConstUPrice, i).Value = Format(0, lnumFormat)
            .Item(ConstDisP, i).Value = Format(0, lnumFormat)
            .Item(ConstDisAmt, i).Value = Format(0, lnumFormat)
            .Item(ConstTaxP, i).Value = Format(0, lnumFormat)
            .Item(ConstTaxAmt, i).Value = Format(0, lnumFormat)
            .Item(ConstLTotal, i).Value = Format(0, lnumFormat) '(.Item(ConstQty, i).Value * DR!unitPrice) - (.Item(ConstQty, i).Value * Val(DR!DiscountVal)) + (.Item(ConstQty, i).Value * Val(DR!TaxVal))
            .Item(ConstNUPrice, i).Value = Format(0, lnumFormat) 'Val(DR!unitPrice) - Val(DR!DiscountVal) + Val(DR!TaxVal)
            .Item(ConstItemID, i).Value = 0
            .Item(ConstImpJobChildTbID, i).Value = 0
            .Item(ConstSerialNo, i).Value = ""
            .Item(ConstPMult, i).Value = "1"
            .Item(ConstPFraction, i).Value = "2"
            .Item(ConstImpDocId, i).Value = ""
            .Item(ConstImpLnId, i).Value = ""
            '.Item(ConstqtyChg, i).Value = "Chg"
            .Item(ConstLrow, i).Value = i + 1
            '.Item(ConstActualPrice, i).Value = 0 ' DR!unitPrice
            '.ClearSelection()
            '.Select()
            '.Rows(i).Selected = True
            chgbyprg = True
            .CurrentCell = .Item(ConstItemCode, i)

            .BeginEdit(True)
            chgbyprg = False
            chgItm = False
        End With
        'calculate()
        reArrangeNo()
        'ChgByPrg = False

    End Sub
    Private Sub calculate(Optional ByVal bFrmCalcOthCost As Boolean = False, Optional ByVal calculateLineTotal As Boolean = False, Optional ByVal chgDiscount As Boolean = False, Optional ByVal blockAutoRoundOff As Boolean = False)
        Dim totQty As Double
        Dim totItm As Double
        Dim totTax As Double
        Dim totLnDis As Double
        Dim totAmt As Double
        Dim totCess As Double
        Dim lnTax As Double
        Dim lnttl As Double
        Dim totTaxableAmt As Double
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
        Dim discountOther As Double
        Dim actualPrice As Double
        With grdVoucher
            For i = 0 To .Rows.Count - 1
                lnTax = 0
                discountOther = 0
                actualPrice = 0
                discountOther = CDbl(.Item(ConstDiscOther, i).Value)
                actualPrice = CDbl(.Item(ConstActualPrice, i).Value) - discountOther - (CDbl(.Item(ConstDisAmt, i).Value) / CDbl(.Item(ConstQty, i).Value))
                If (calculateLineTotal And Val(numDisc.Text) > 0) Or chgDiscount Then
                    calcualteLineTotal(i)
                End If
                If Val(.Item(ConstItemID, i).Value) = 0 Then GoTo nxt
                If Val(.Item(ConstUnitCount, i).Value) = 1 Then
                    totQty = totQty + 1
                Else
                    totQty = totQty + IIf(Val(.Item(ConstQty, i).Value) > 0, Val(.Item(ConstQty, i).Value), 0)
                End If
                If chktaxInv.Checked = False Then
                    .Item(ConstTaxAmt, i).Value = Format(0, lnumFormat)
                    .Item(ConstTaxP, i).Value = Format(0, lnumFormat)
                    .Item(ConstcessAmt, i).Value = 0
                Else
                    .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstIGSTAmt, i).Value), lnumFormat)
                    .Item(ConstTaxP, i).Value = Format(CDbl(.Item(ConstIGSTP, i).Value), lnumFormat)
                    If enablecess Then
                        lnTax = CDbl(.Item(ConstregularCessAmt, i).Value)
                    End If
                    If enableFloodCess And cessdate <= DateValue(cldrdate.Value) And cessenddate >= DateValue(cldrdate.Value) Then
                        lnTax = lnTax + CDbl(.Item(ConstFloodCessAmt, i).Value)
                    End If
                    .Item(ConstcessAmt, i).Value = Format(lnTax, lnumFormat)
                    lnTax = lnTax + CDbl(.Item(ConstIGSTAmt, i).Value)
                End If
                If EnableGST Then
                    If chktaxInv.Checked Then
                        totTax = totTax + CDbl(.Item(ConstCGSTAmt, i).Value) + CDbl(.Item(ConstSGSTAmt, i).Value)
                    End If
                Else
                    If chktaxInv.Checked Then
                        totTax = totTax + CDbl(.Item(ConstIGSTAmt, i).Value)
                    End If
                End If

                totLnDis = totLnDis + .Item(ConstDisAmt, i).Value
                totAmt = totAmt + (CDbl(.Item(ConstQty, i).Value) * (CDbl(.Item(ConstActualPrice, i).Value) - IIf(enableAdjustDiscountOnTaxTotal, CDbl(.Item(ConstDiscOther, i).Value), 0))) - CDbl(.Item(ConstDisAmt, i).Value)
                totItm = totItm + (CDbl(.Item(ConstQty, i).Value) * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value)
                If (enablecess Or (enableFloodCess And cessdate <= DateValue(cldrdate.Value) And cessenddate >= DateValue(cldrdate.Value))) And chktaxInv.Checked Then
                    totCess = totCess + CDbl(.Item(ConstcessAmt, i).Value)
                End If
                lnttl = (CDbl(.Item(ConstQty, i).Value) * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value)
                lnttl = lnttl + lnTax
                chgbyprg = True
                .Item(ConstLTotal, i).Value = Format(lnttl, lnumFormat)
                chgbyprg = False
                If Val(.Item(ConstTaxAmt, i).Value) > 0 Then
                    totTaxableAmt = totTaxableAmt + ((CDbl(.Item(ConstActualPrice, i).Value) - CDbl(.Item(ConstDiscOther, i).Value)) * CDbl(.Item(ConstQty, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value)
                End If
nxt:
            Next
            calOthCost()

            totAmt = totAmt + totTax + totCess
            lblTotAmt.Text = Format(totItm, lnumFormat)
            If enableAdjustDiscountOnTaxTotal Then
                lblNetAmt.Text = Format(totAmt, lnumFormat)
            Else
                lblNetAmt.Text = Format(totAmt - CDbl(numDisc.Text), lnumFormat)
            End If

            If chkautoroundOff.Checked Then
                If Not blockAutoRoundOff Then
                    chgNumByPgm = True
                    Dim retrnAmt As Double
                    cmbsign.SelectedIndex = getroundoffAMT(lblNetAmt.Text, retrnAmt)
                    txtroundOff.Text = Format(retrnAmt, lnumFormat)
                    chgNumByPgm = False
                End If
            End If

            If Val(txtroundOff.Text) > 0 Then
                lblNetAmt.Text = Format(CDbl(lblNetAmt.Text) - (IIf(cmbsign.SelectedIndex = 1, 1, -1) * CDbl(txtroundOff.Text)), lnumFormat)
            End If
            If Val(txtsmanP.Text) > 0 Then
                lblSCAmt.Text = Format(CDbl(lblNetAmt.Text) * CDbl(txtsmanP.Text) / 100, lnumFormat)
            End If
            lbltax.Text = Format(totTax, lnumFormat)
            lblcess.Text = Format(totCess, lnumFormat)
            totAmt = totAmt - CDbl(numDisc.Text)
            lbltaxable.Text = Format(totTaxableAmt, lnumFormat)
            lblqty.Text = totQty
            'lbltotalwithOC.Text = Format(CDbl(lblNetAmt.Text) + CDbl(lblOthCost.Text), lnumFormat)
            chgAmt = False
            chgbyprg = False
        End With
    End Sub
    Private Sub calcualteLineTotal(ByVal RowIndex As Integer)
        chgbyprg = True
        If RowIndex < 0 Then Exit Sub
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
            If Val(.Item(ConstRegcess, i).Value & "") = 0 Then
                .Item(ConstRegcess, i).Value = 0
            End If
            If Val(.Item(ConstAdditionalcess, i).Value & "") = 0 Then
                .Item(ConstAdditionalcess, i).Value = 0
            End If
            If Val(.Item(ConstActualPrice, i).Value & "") = 0 Then
                .Item(ConstActualPrice, i).Value = 0
            End If
            If Val(.Item(ConstQty, i).Value & "") = 0 Then
                .Item(ConstQty, i).Value = 0
            End If
            Dim gstamt As Double
            Dim cessTtl As Double
            Dim actualPrice As Double
            Dim discountOther As Double
            discountOther = CDbl(.Item(ConstDiscOther, i).Value) * CDbl(.Item(ConstQty, i).Value)
            actualPrice = (CDbl(.Item(ConstActualPrice, i).Value) * CDbl(.Item(ConstQty, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value) - discountOther
            If EnableGST Then
                getGSTDetails(Trim(.Item(ConstBarcode, i).Value & ""), i, True)
                If Val(.Item(ConstSGSTAmt, i).Value & "") = 0 Then .Item(ConstSGSTAmt, i).Value = 0
                If Val(.Item(ConstCGSTAmt, i).Value & "") = 0 Then .Item(ConstCGSTAmt, i).Value = 0
                gstamt = CDbl(.Item(ConstCGSTAmt, i).Value) + CDbl(.Item(ConstSGSTAmt, i).Value)
                .Item(ConstTaxAmt, i).Value = Format(gstamt, lnumFormat)
            ElseIf enableGCC Or ShowTaxOnInventory Then
                actualPrice = Format(actualPrice, lnumFormat)
                .Item(ConstIGSTAmt, i).Value = ((actualPrice * .Item(ConstIGSTP, i).Value) / 100)
                .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstIGSTAmt, i).Value), lnumFormat)
            End If
            If enablecess Then
                cessTtl = (actualPrice * CDbl(.Item(ConstRegcess, i).Value) / 100)
                cessTtl = cessTtl + (CDbl(.Item(ConstAdditionalcess, i).Value) * CDbl(.Item(ConstQty, i).Value))
                .Item(ConstregularCessAmt, i).Value = cessTtl
            End If
            If enableFloodCess And cessdate <= DateValue(cldrdate.Value) And cessenddate >= DateValue(cldrdate.Value) Then
                .Item(ConstFloodCessAmt, i).Value = (actualPrice * CDbl(.Item(Constcess, i).Value)) / 100
                cessTtl = cessTtl + CDbl(.Item(ConstFloodCessAmt, i).Value)

            End If
            .Item(ConstcessAmt, i).Value = Format(cessTtl, lnumFormat)

            If chktaxInv.Checked = False Then
                .Item(ConstTaxAmt, i).Value = Format(0, lnumFormat)
                .Item(ConstTaxP, i).Value = Format(0, lnumFormat)
                .Item(ConstcessAmt, i).Value = 0
            End If

            .Item(constItmTot, i).Value = Format((.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value), lnumFormat)
            Dim ttl As Double
            ttl = (.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value) + gstamt + cessTtl
            .Item(ConstLTotal, i).Value = Format(ttl, lnumFormat)
            .Item(ConstNUPrice, i).Value = CDbl(.Item(ConstActualPrice, i).Value) - (CDbl(.Item(ConstDisAmt, i).Value) / CDbl(.Item(ConstQty, i).Value)) + (CDbl(.Item(ConstTaxAmt, i).Value) / CDbl(.Item(ConstQty, i).Value)) + (CDbl(.Item(ConstcessAmt, i).Value) / CDbl(.Item(ConstQty, i).Value)) - Val(.Item(ConstDiscOther, i).Value)
        End With
        chgbyprg = False
    End Sub
    'Private Sub calcualteLineTotal(ByVal RowIndex As Integer)
    '    chgbyprg = True
    '    If grdVoucher.RowCount = 0 Then Exit Sub
    '    With grdVoucher
    '        Dim i As Integer
    '        i = RowIndex
    '        If Val(.Item(ConstItemID, i).Value) = 0 Then Exit Sub
    '        .Item(ConstSlNo, i).Value = i + 1
    '        If Val(.Item(ConstTaxP, i).Value & "") = 0 Then
    '            .Item(ConstTaxP, i).Value = 0
    '        End If
    '        If Val(.Item(Constcess, i).Value & "") = 0 Then
    '            .Item(Constcess, i).Value = 0
    '        End If
    '        If Val(.Item(ConstActualPrice, i).Value & "") = 0 Then
    '            .Item(ConstActualPrice, i).Value = 0
    '        End If
    '        If Val(.Item(ConstQty, i).Value & "") = 0 Then
    '            .Item(ConstQty, i).Value = 0
    '        End If
    '        Dim gstamt As Double
    '        Dim cessTtl As Double
    '        If EnableGST Then
    '            getGSTDetails(Trim(.Item(ConstBarcode, i).Value & ""), i, True)
    '            If Val(.Item(ConstSGSTAmt, i).Value & "") = 0 Then .Item(ConstSGSTAmt, i).Value = 0
    '            If Val(.Item(ConstCGSTAmt, i).Value & "") = 0 Then .Item(ConstCGSTAmt, i).Value = 0
    '            gstamt = CDbl(.Item(ConstCGSTAmt, i).Value) + CDbl(.Item(ConstSGSTAmt, i).Value)
    '            .Item(ConstTaxAmt, i).Value = Format(gstamt, lnumFormat)
    '        Else
    '            .Item(ConstTaxAmt, i).Value = Format(((CDbl(.Item(ConstActualPrice, i).Value) * .Item(ConstTaxP, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value), lnumFormat)
    '        End If
    '        If enablecess And cessdate <= DateValue(cldrdate.Value) Then
    '            cessTtl = (((CDbl(.Item(ConstActualPrice, i).Value) - CDbl(.Item(ConstDiscOther, i).Value)) * .Item(Constcess, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value)
    '            .Item(ConstcessAmt, i).Value = Format(cessTtl, lnumFormat)
    '        End If

    '        .Item(constItmTot, i).Value = Format((.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value), lnumFormat)
    '        Dim ttl As Double
    '        ttl = (.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value) + gstamt + cessTtl
    '        .Item(ConstLTotal, i).Value = Format(ttl, lnumFormat)
    '        .Item(ConstNUPrice, i).Value = CDbl(.Item(ConstActualPrice, i).Value) - (CDbl(.Item(ConstDisAmt, i).Value) / CDbl(.Item(ConstQty, i).Value)) + (CDbl(.Item(ConstTaxAmt, i).Value) / CDbl(.Item(ConstQty, i).Value)) + (CDbl(.Item(ConstcessAmt, i).Value) / CDbl(.Item(ConstQty, i).Value)) - Val(.Item(ConstDiscOther, i).Value)
    '    End With
    '    chgbyprg = False
    'End Sub
    Private Sub setCreditOrCashPrice()
        Dim i As Integer
        Dim dt As DataTable
        Dim itemids As String = ""
        Dim unitPrice As Double
        Dim dtsub As DataTable
        chgbyprg = True
        With grdVoucher
            'For i = 0 To .RowCount - 1
            '    itemids = itemids & IIf(itemids = "", "", ",") & Val(.Item(ConstItemID, i).Value)
            'Next
            If enableBatchwiseTr Then
                dt = _objcmnbLayer._fldDatatable("select unitprice,secondprice,invitm.itemid,bno from " & _
                                                 "(select case when isnull(SP1,0)=0 then unitprice else SP1 end unitprice," & _
                                                 "case when isnull(SP2,0)=0 then secondprice else SP2 end secondprice," & _
                                                 "ItmInvTrTb.itemid,SerialNo bno from ItmInvCmnTb " & _
                                                 "left join ItmInvTrTb on ItmInvCmnTb.trid=ItmInvTrTb.trid " & _
                                                 "left join VoucherTypeNoTb on VoucherTypeNoTb.vrno=ItmInvCmnTb.typeno " & _
                                                  "left join invitm on invitm.itemid=ItmInvTrTb.itemid " & _
                                                 "where invtype='IN') invitm left join " & _
                                             "itminvtrtb on invitm.itemid=itminvtrtb.itemid where trid=" & loadedTrId)
                If dt.Rows.Count = 0 Then
                    GoTo itm
                End If
            Else
itm:
                dt = _objcmnbLayer._fldDatatable("Select unitprice,secondprice,invitm.itemid,'' bno from invitm left join " & _
                                             "itminvtrtb on invitm.itemid=itminvtrtb.itemid where trid=" & loadedTrId)
            End If


            For i = 0 To .RowCount - 1
                Dim prices = From data In dt _
                 Where data("itemid") = Val(.Item(ConstItemID, i).Value) And data("bno") = Trim(.Item(ConstSerialNo, i).Value & "") _
                 Select New With {.unitprice = data.Item("unitprice"), .secondprice = data.Item("secondprice")}
                unitPrice = 0
                If prices.Count = 0 Then
                    dtsub = _objcmnbLayer._fldDatatable("Select unitprice,secondprice,invitm.itemid,'' bno from invitm where itemid=" & Val(.Item(ConstItemID, i).Value))
                    If chkcredit.Checked Then
                        unitPrice = dtsub(0)("secondprice")
                        If unitPrice = 0 Then
                            unitPrice = dtsub(0)("unitprice")
                        End If
                    Else
                        unitPrice = dtsub(0)("unitprice")
                    End If
                    GoTo nxt
                End If
                For Each r In prices
                    If IsDBNull(r.unitprice) Then
                        r.unitprice = 0
                    End If
                    If IsDBNull(r.secondprice) Then
                        r.secondprice = 0
                    End If
                    If chkcredit.Checked Then
                        unitPrice = r.secondprice
                        If unitPrice = 0 Then
                            unitPrice = r.unitprice
                        End If
                    Else
                        unitPrice = r.unitprice
                    End If
                Next
nxt:
                chgbyprg = True
                .Item(ConstActualPrice, i).Value = unitPrice
                .Item(ConstUPrice, i).Value = Format(unitPrice, numFormat)
                .Item(ConstqtyChg, i).Value = "CHG"
                calcualteLineTotal(i)
                chgbyprg = False
            Next
        End With
        calculate()
        chgbyprg = False
    End Sub
    '    Private Sub calculate(Optional ByVal bFrmCalcOthCost As Boolean = False, Optional ByVal calculateLineTotal As Boolean = False, Optional ByVal chgDiscount As Boolean = False, Optional ByVal blockAutoRoundOff As Boolean = False)
    '        Dim totQty As Double
    '        Dim totItm As Double
    '        Dim totTax As Double
    '        Dim totLnDis As Double
    '        Dim totAmt As Double
    '        Dim totCess As Double
    '        Dim i As Integer
    '        calOthCost()
    '        Dim gindex As Integer
    '        If grdVoucher.CurrentCell Is Nothing Then
    '            gindex = grdVoucher.RowCount - 1
    '        Else
    '            gindex = grdVoucher.CurrentCell.RowIndex
    '        End If
    '        If calculateLineTotal And Val(numDisc.Text) = 0 Then
    '            calcualteLineTotal(gindex)
    '        End If

    '        If numDisc.Text = "" Then
    '            numDisc.Text = Format(0, lnumFormat)
    '        End If
    '        With grdVoucher
    '            For i = 0 To .Rows.Count - 1
    '                If (calculateLineTotal And Val(numDisc.Text) > 0) Or chgDiscount Then
    '                    calcualteLineTotal(i)
    '                End If
    '                If Val(.Item(ConstItemID, i).Value) = 0 Then GoTo nxt
    '                totQty = totQty + IIf(Val(.Item(ConstQty, i).Value) > 0, Val(.Item(ConstQty, i).Value), 0)
    '                .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstIGSTAmt, i).Value), lnumFormat)
    '                .Item(ConstTaxP, i).Value = Format(CDbl(.Item(ConstIGSTP, i).Value), lnumFormat)
    '                If enablecess And cessdate <= DateValue(cldrdate.Value) Then
    '                    .Item(ConstcessAmt, i).Value = Format((((CDbl(.Item(ConstActualPrice, i).Value) - CDbl(.Item(ConstDiscOther, i).Value)) * .Item(Constcess, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value), lnumFormat)
    '                End If
    '                totTax = totTax + CDbl(.Item(ConstCGSTAmt, i).Value) + CDbl(.Item(ConstSGSTAmt, i).Value)
    '                totLnDis = totLnDis + .Item(ConstDisAmt, i).Value
    '                totAmt = totAmt + (.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value)
    '                totItm = totItm + (.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value)
    '                If enablecess And cessdate <= DateValue(cldrdate.Value) Then
    '                    totCess = totCess + CDbl(.Item(ConstcessAmt, i).Value)
    '                End If
    '                If chgDiscount Or chggrd Then
    '                    .Item(ConstqtyChg, i).Value = "CHG"
    '                End If
    'nxt:
    '            Next
    '            calOthCost()
    '            totAmt = totAmt + totTax + totCess
    '            lblTotAmt.Text = Format(totItm, lnumFormat)
    '            lblNetAmt.Text = Format(totAmt - CDbl(numDisc.Text), lnumFormat)
    '            If chkautoroundOff.Checked Then
    '                If Not blockAutoRoundOff Then
    '                    chgNumByPgm = True
    '                    Dim retrnAmt As Double
    '                    cmbsign.SelectedIndex = getroundoffAMT(lblNetAmt.Text, retrnAmt)
    '                    txtroundOff.Text = Format(retrnAmt, lnumFormat)
    '                    chgNumByPgm = False
    '                End If
    '            End If

    '            If Val(txtroundOff.Text) > 0 Then
    '                lblNetAmt.Text = Format(CDbl(lblNetAmt.Text) - (IIf(cmbsign.SelectedIndex = 1, 1, -1) * CDbl(txtroundOff.Text)), lnumFormat)
    '            End If
    '            lbltax.Text = Format(totTax, lnumFormat)
    '            lblcess.Text = Format(totCess, lnumFormat)
    '            totAmt = totAmt - CDbl(numDisc.Text)
    '            chgAmt = False
    '            chggrd = False
    '        End With
    '    End Sub
    '    Private Sub calcualteLineTotal(ByVal RowIndex As Integer)
    '        With grdVoucher
    '            Dim i As Integer
    '            'If .CurrentCell Is Nothing Then
    '            '    i = .RowCount - 1
    '            'Else
    '            '    i = .CurrentCell.RowIndex
    '            'End If
    '            i = RowIndex
    '            If Val(.Item(ConstItemID, i).Value) = 0 Then Exit Sub
    '            .Item(ConstSlNo, i).Value = i + 1
    '            If Val(.Item(ConstTaxP, i).Value & "") = 0 Then
    '                .Item(ConstTaxP, i).Value = 0
    '            End If
    '            If Val(.Item(Constcess, i).Value & "") = 0 Then
    '                .Item(Constcess, i).Value = 0
    '            End If
    '            If Val(.Item(ConstActualPrice, i).Value & "") = 0 Then
    '                .Item(ConstActualPrice, i).Value = 0
    '            End If
    '            If Val(.Item(ConstQty, i).Value & "") = 0 Then
    '                .Item(ConstQty, i).Value = 0
    '            End If
    '            If EnableGST Then
    '                getGSTDetails(Trim(.Item(ConstBarcode, i).Value & ""), i, True)
    '                If Val(.Item(ConstSGSTAmt, i).Value & "") = 0 Then .Item(ConstSGSTAmt, i).Value = 0
    '                If Val(.Item(ConstCGSTAmt, i).Value & "") = 0 Then .Item(ConstCGSTAmt, i).Value = 0
    '                .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstCGSTAmt, i).Value) + CDbl(.Item(ConstSGSTAmt, i).Value), lnumFormat)
    '            Else
    '                .Item(ConstTaxAmt, i).Value = Format(((CDbl(.Item(ConstActualPrice, i).Value) * .Item(ConstTaxP, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value), lnumFormat)
    '            End If
    '            If enablecess And cessdate <= DateValue(cldrdate.Value) Then
    '                .Item(ConstcessAmt, i).Value = Format(((CDbl(.Item(ConstActualPrice, i).Value) * .Item(Constcess, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value), lnumFormat)
    '            End If

    '            .Item(constItmTot, i).Value = Format((.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value), lnumFormat)
    '            .Item(ConstLTotal, i).Value = Format((.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value) + CDbl(.Item(ConstTaxAmt, i).Value), lnumFormat)
    '            .Item(ConstNUPrice, i).Value = CDbl(.Item(ConstActualPrice, i).Value) - (CDbl(.Item(ConstDisAmt, i).Value) / CDbl(.Item(ConstQty, i).Value)) + (CDbl(.Item(ConstTaxAmt, i).Value) / CDbl(.Item(ConstQty, i).Value)) + (CDbl(.Item(ConstcessAmt, i).Value) / CDbl(.Item(ConstQty, i).Value)) - Val(.Item(ConstDiscOther, i).Value)
    '        End With

    '    End Sub
    '    Private Sub calculate(Optional ByVal bFrmCalcOthCost As Boolean = False, Optional ByVal calculateLineTotal As Boolean = False)
    '        Dim totQty As Double
    '        Dim totItm As Double
    '        Dim totTax As Double
    '        Dim totLnDis As Double
    '        Dim totAmt As Double
    '        Dim totCess As Double
    '        Dim i As Integer
    '        calOthCost()
    '        Dim gindex As Integer
    '        If grdVoucher.CurrentCell Is Nothing Then
    '            gindex = grdVoucher.RowCount - 1
    '        Else
    '            gindex = grdVoucher.CurrentCell.RowIndex
    '        End If
    '        If calculateLineTotal Then calcualteLineTotal(gindex)
    '        If numDisc.Text = "" Then
    '            numDisc.Text = Format(0, lnumformat)
    '        End If
    '        With grdVoucher
    '            For i = 0 To .Rows.Count - 1
    '                If Val(.Item(ConstItemID, i).Value) = 0 Then GoTo nxt
    '                totQty = totQty + IIf(Val(.Item(ConstQty, i).Value) > 0, Val(.Item(ConstQty, i).Value), 0)
    '                totTax = totTax + CDbl(.Item(ConstCGSTAmt, i).Value) + CDbl(.Item(ConstSGSTAmt, i).Value)
    '                totLnDis = totLnDis + .Item(ConstDisAmt, i).Value
    '                totAmt = totAmt + (.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value)
    '                totItm = totItm + (.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value)
    '                If enablecess And cessdate <= DateValue(cldrdate.Value) Then
    '                    totCess = totCess + CDbl(.Item(ConstcessAmt, i).Value)
    '                End If
    'nxt:
    '            Next
    '            calOthCost()
    '            totAmt = totAmt + totTax + totCess
    '            lblTotAmt.Text = Format(totItm, lnumformat)
    '            lblNetAmt.Text = Format(totAmt - CDbl(numDisc.Text), lnumformat)
    '            If Val(txtroundOff.Text) > 0 Then
    '                lblNetAmt.Text = Format(CDbl(lblNetAmt.Text) - (IIf(cmbsign.SelectedIndex = 1, 1, -1) * CDbl(txtroundOff.Text)), lnumformat)
    '            End If
    '            lbltax.Text = Format(totTax, lnumFormat)
    '            lblcess.Text = Format(totCess, lnumFormat)
    '            totAmt = totAmt - CDbl(numDisc.Text)
    '            chgAmt = False
    '        End With
    '    End Sub


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
                If Val(.Item(ConstMthd, i).Value & "") <> 0 Then
                    tOthVal = tOthVal + Val(.Item(ConstActualOthCost, i).Value) * CDbl(.Item(ConstQty, i).Value)
                Else
                    tBAmt = tBAmt + (Val(.Item(ConstActualPrice, i).Value) * CDbl(.Item(ConstQty, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value) 'CDbl(.Item(constItmTot, i).Value)
                End If
                'If Val(.Item(i, 20)) <> 0 Then
                '   tDAmt = tDAmt + Val(.Item(ConstDiscOther, i).Value) * CDbl(.Item(ConstQty, i).Value)
                'Else
                tBDAmt = tBDAmt + (Val(.Item(ConstActualPrice, i).Value) * CDbl(.Item(ConstQty, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value)
                'End If
            Next i
            tOthVal = OthCost / FCRt - tOthVal
            If numDisc.Text = "" Then numDisc.Text = 0
            tDAmt = CDbl(numDisc.Text)
            Dim discamt As Double
            For i = 0 To .Rows.Count - 1 '- 1
                If Val(.Item(ConstDisAmt, i).Value) <> 0 Then
                    discamt = Val(.Item(ConstDisAmt, i).Value) / CDbl(.Item(ConstQty, i).Value)
                Else
                    discamt = 0
                End If
                If Val(.Item(ConstMthd, i).Value) = 0 Then
                    If tBAmt = 0 Then
                        .Item(ConstActualOthCost, i).Value = 0
                    Else
                        If Val(grdVoucher.Item(ConstActualPrice, i).Value) = 0 Then grdVoucher.Item(ConstActualPrice, i).Value = 0
                        .Item(ConstActualOthCost, i).Value = tOthVal * (CDbl(grdVoucher.Item(ConstActualPrice, i).Value) - discamt) / tBAmt
                    End If
                    grdVoucher.Item(ConstUnitOthCost, i).Value = Format(Val(.Item(ConstActualOthCost, i).Value), "#,##0" & IIf(NoOfDecimal = 0, "", "." & StrDup(NoOfDecimal, "0"))) ' & NumFormat)'shine
                End If
                actualPrice = Val(.Item(ConstActualPrice, i).Value) - discamt
                If tBDAmt = 0 Then
                    .Item(ConstDiscOther, i).Value = 0
                Else
                    If enableAdjustDiscountOnTaxTotal Then
                        If tDAmt = 0 Then GoTo els
                        Dim discountOther As Double
                        discountOther = (tDAmt * actualPrice) / tBDAmt
                        Dim discountWithoutTax As Double
                        Dim ttax As Double
                        ttax = CDbl(.Item(ConstTaxP, i).Value) + CDbl(.Item(ConstRegcess, i).Value)
                        discountWithoutTax = (discountOther * 100) / (ttax + 100)
                        .Item(ConstDiscOther, i).Value = discountWithoutTax
                    Else
els:
                        .Item(ConstDiscOther, i).Value = (tDAmt * actualPrice) / tBDAmt
                    End If
                    .Item(ConstqtyChg, i).Value = "CHG"
                End If
                numDisc.Tag = Val(numDisc.Tag) + (Val(.Item(ConstDiscOther, i).Value) * CDbl(.Item(ConstQty, i).Value))
                .Item(ConstNUPrice, i).Value = Format(Val(.Item(ConstActualPrice, i).Value) + Val(.Item(ConstActualOthCost, i).Value) - Val(.Item(ConstDiscOther, i).Value) - Val(.Item(ConstDisAmt, i).Value), "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ' & lnumFormat)
                'If Val(.Item(ConstActualOthCost, i).Value)
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
                    'If chkcredit.Checked And enableCreditPrice Then
                    '    UpdateClick(True, 2)
                    'Else
                    '    UpdateClick(True)
                    'End If
                    posType = 0
                    If chgdiscount = True Then
                        calculate(, True, True)
                    Else
                        calculate()
                    End If

                    loadWaite(3)
                ElseIf msg.WParam.ToInt32() = CInt(Keys.F1) Then
                    txtitemcode.Focus()
                    'chgbyprg = True
                    'txtitemcode.Text = ""
                    'txtitemcode.Tag = ""
                    'chgbyprg = False
                    'If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
                ElseIf msg.WParam.ToInt32() = CInt(Keys.F3) Then
                    numDisc.Focus()
                ElseIf msg.WParam.ToInt32() = CInt(Keys.F6) Then
                    'If chkcredit.Checked And enableCreditPrice Then
                    '    UpdateClick(True, 2)
                    'Else
                    '    UpdateClick(True, 1)
                    'End If
                    posType = 1
                    loadWaite(3)
                ElseIf msg.WParam.ToInt32() = CInt(Keys.F7) Then
                    If enableCreditPrice Then chkcredit.Checked = True
                    'UpdateClick(True, 2)
                    posType = 2
                    loadWaite(3)
                ElseIf msg.WParam.ToInt32() = CInt(Keys.F5) Then
                    holdInvoice()
                ElseIf msg.WParam.ToInt32() = CInt(Keys.F9) Then
                    If grdVoucher.RowCount > 0 Then
                        Dim eindex As Integer
                        If grdVoucher.CurrentRow Is Nothing Then
                            eindex = 0
                        Else
                            eindex = grdVoucher.CurrentRow.Index
                        End If
                        openQuickItem(Val(grdVoucher.Item(ConstItemID, eindex).Value))
                    End If
                ElseIf msg.WParam.ToInt32() = CInt(Keys.F4) Then
                    If activecontrolname = "grdVoucher" Then
                        RemoveRow(grdVoucher.CurrentRow.Index)
                    Else
                        RemoveRow(grdVoucher.RowCount - 1)
                    End If
                ElseIf msg.WParam.ToInt32() = CInt(Keys.F2) Then
                    With grdVoucher
                        'If .RowCount > 0 And .CurrentCell.ColumnIndex = ConstMRP Then
                        '    Dim r As Integer
                        '    r = .RowCount - 1
                        '    .Rows(r).Selected = True
                        '    .CurrentCell = .Item(ConstMRP, r)
                        '    .FirstDisplayedScrollingRowIndex = r
                        '    .BeginEdit(True)
                        'Else
                        '    grdVoucher_KeyDown(Nothing, New KeyEventArgs(keyData))
                        'End If
                        grdVoucher_KeyDown(Nothing, New KeyEventArgs(keyData))
                        activecontrolname = "grdVoucher"
                    End With
                ElseIf msg.WParam.ToInt32() = CInt(Keys.F11) Then
                    With grdVoucher
                        If .RowCount > 0 Then
                            Dim r As Integer
                            r = .RowCount - 1
                            .Rows(r).Selected = True
                            .CurrentCell = .Item(ConstQty, r)
                            .FirstDisplayedScrollingRowIndex = r
                            .BeginEdit(True)
                            activecontrolname = "grdVoucher"
                        End If
                    End With
                ElseIf msg.WParam.ToInt32() = CInt(Keys.F12) Then
                    With grdVoucher
                        If Not disablePriceEditInPos Then
                            If .RowCount > 0 Then
                                Dim r As Integer
                                r = .RowCount - 1
                                .Rows(r).Selected = True
                                .CurrentCell = .Item(ConstUPrice, r)
                                .FirstDisplayedScrollingRowIndex = r
                                .BeginEdit(True)
                                activecontrolname = "grdVoucher"
                            End If
                        Else
                            .CurrentCell = .Item(ConstUPrice, .RowCount - 1)
                            .CurrentCell.ReadOnly = True
                            txtitemcode.Focus()
                        End If

                    End With
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
                    If Not fTendered Is Nothing Then fTendered.Close() : fTendered = Nothing
                    If Not fSR Is Nothing Then fSR.Close() : fSR = Nothing
                    txtitemcode.SelectionStart = 0
                    txtitemcode.SelectionLength = Len(txtitemcode.Text)
                    chgbyprg = True
                    txtitemcode.Text = ""
                    txtitemcode.Tag = ""
                    chgbyprg = False

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
            ElseIf e.ColumnIndex <> ConstItemCode And e.ColumnIndex <> ConstDescr And e.ColumnIndex <> ConstSlNo And e.ColumnIndex <> constItmTot And e.ColumnIndex <> ConstUnit And e.ColumnIndex <> ConstLTotal And e.ColumnIndex <> ConstBarcode And e.ColumnIndex <> ConstTaxAmt Then
                If e.ColumnIndex = ConstTaxP And EnableGST Then
                    grdVoucher.CurrentCell.ReadOnly = True
                ElseIf e.ColumnIndex = ConstUPrice And disablePriceEditInPos Then
                    grdVoucher.CurrentCell.ReadOnly = True
                Else
                    grdVoucher.CurrentCell.ReadOnly = False
                End If
            Else
                grdVoucher.CurrentCell.ReadOnly = True
            End If
        End With
        grdBeginEdit()
        chgbyprg = False
    End Sub

    Private Sub grdVoucher_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellValidated
        If chgbyprg Then Exit Sub
        Valid(e.RowIndex, e.ColumnIndex, txtitemcode.Text)
    End Sub
    Private Sub Valid(ByVal RowIndex As Integer, ByVal ColIndex As Integer, ByVal searchby As String)
        With grdVoucher
            Select Case ColIndex
                Case ConstBarcode, ConstItemCode
                    If Not chgItm Then Exit Sub
                    If searchby = "" Then
                        If txtitemcode.Tag <> "" Then .Item(ColIndex, RowIndex).Value = txtitemcode.Tag : SrchText = txtitemcode.Tag
                    Else
                        .Item(ColIndex, RowIndex).Value = searchby
                    End If
                    If Trim(.Item(ColIndex, RowIndex).Value) = "" Then
                        Exit Sub
                    Else
                        SrchText = Trim(.Item(ColIndex, RowIndex).Value)
                    End If

                    Dim dtItms As DataTable
                    Dim found As Boolean
                    dtItms = getItmDtls(3, SrchText, True)
                    If dtItms.Rows.Count > 0 Then
                        found = True
                        AddDetails(dtItms, RowIndex)
                    End If
                    chgItm = False
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
                    If diableNegativeSale Then
                        Dim qtyqry As String = "SELECT isnull(AsOnQty,0)+isnull(opQty,0) AsOnQty,Itemid,Description,isnull(wmcalculation,0)wmcalculation FROM InvItm left join " & _
                                                "(SELECT SUM(CASE WHEN InvType in ('IN') then 1 else -1 end  * isnull(trqty,0)) AsOnQty ,Itemid TItemid from " & _
                                                "(SELECT InvType,trqty,TrDateNo,Itemid FROM ItmInvTrtb LEFT JOIN VoucherTypeNoTb ON ItmInvTrTb.TrTypeNo=VoucherTypeNoTb.vrno " & _
                                                "LEFT JOIN ItmInvCmnTb ON  ItmInvTrTb.TRID=ItmInvCmnTb.TRID  WHERE isnull(invStatus,0)=0) tr  group by Itemid) tr ON INVITM.Itemid=tr.TItemid " & _
                                                "where itemid=" & Val(.Item(ConstItemID, RowIndex).Value)

                        Dim dtqty As DataTable = _objcmnbLayer._fldDatatable(qtyqry)
                        If dtqty.Rows.Count > 0 Then
                            Dim qty As Double = getgridqty(.Item(ConstItemCode, RowIndex).Value, RowIndex)
                            If Val(dtqty(0)("AsOnQty")) - qty < Val(.Item(ConstQty, RowIndex).Value) Then
                                MsgBox("Quantity Exceeds", MsgBoxStyle.Exclamation)
                                chgbyprg = True
                                .Item(ConstQty, RowIndex).Value = Val(dtqty(0)("AsOnQty"))
                                chgbyprg = False
                            End If
                        End If

                    End If
                    If chgAmt Then
                        If AllowUnitDiscountEntryOnInventory Then
                            .Item(ConstDisAmt, RowIndex).Value = Format(((CDbl(.Item(ConstActualPrice, RowIndex).Value) * Val(.Item(ConstDisP, RowIndex).Value)) / 100) * CDbl(.Item(ConstQty, RowIndex).Value), lnumFormat)
                        End If
                        If EnableGST Then getGSTDetails(Trim(.Item(ConstBarcode, RowIndex).Value & ""), RowIndex, True)
                        calculate(, True)
                    End If
                    'If EnableRawmaterialUpdateInSales Then setRawmaterialQty(RowIndex)
                Case ConstUPrice
                    If chgAmt Then
                        If Not chgUprice And Not chkcal.Checked Then
                            .Item(ConstActualPrice, RowIndex).Value = CDbl(.Item(ConstUPrice, RowIndex).Value)
                        End If
                        If AllowUnitDiscountEntryOnInventory Then
                            .Item(ConstDisAmt, RowIndex).Value = Format(((CDbl(.Item(ConstActualPrice, RowIndex).Value) * Val(.Item(ConstDisP, RowIndex).Value)) / 100) * CDbl(.Item(ConstQty, RowIndex).Value), lnumFormat)
                        End If
                        If EnableGST Then getGSTDetails(Trim(.Item(ConstBarcode, RowIndex).Value & ""), RowIndex, True)
                        calculate(, True)
                    End If
                Case ConstDisP
                    If chgAmt Then
                        .Item(ConstDisAmt, RowIndex).Value = Format(((CDbl(.Item(ConstActualPrice, RowIndex).Value) * Val(.Item(ConstDisP, RowIndex).Value)) / 100) * CDbl(.Item(ConstQty, RowIndex).Value), lnumFormat)
                        If EnableGST Then getGSTDetails(Trim(.Item(ConstBarcode, RowIndex).Value & ""), RowIndex, True)
                        calculate(, True)
                    End If
                Case ConstDisAmt
                    If chgAmt Then
                        Dim unitDiscount As Double
                        unitDiscount = CDbl(.Item(ConstDisAmt, RowIndex).Value) / CDbl(.Item(ConstQty, RowIndex).Value)
                        .Item(ConstDisP, RowIndex).Value = Format((unitDiscount * 100) / CDbl(.Item(ConstActualPrice, RowIndex).Value), lnumFormat)
                        If EnableGST Then getGSTDetails(Trim(.Item(ConstBarcode, RowIndex).Value & ""), RowIndex, True)
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
                Case ConstMRP
                    If enableBatchwiseTr Then
                        Dim dt As DataTable
                        If Val(SrchText) <> 0 Then .Item(ConstMRP, RowIndex).Value = Format(CDbl(SrchText), numFormat)
                        If Val(.Item(ConstMRP, RowIndex).Value & "") <> 0 Then
                            dt = _objcmnbLayer._fldDatatable("select trid from ItmInvTrTb where itemid=" & Val(.Item(ConstItemID, RowIndex).Value) & " and SerialNo='" & .Item(ConstSerialNo, RowIndex).Value & "'")
                            If dt.Rows.Count = 0 Then
                                If MsgBox("Entered Batch number not found! Do you want to continue?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                                    .Item(ConstDonotAllowsaveItem, RowIndex).Value = 1
                                Else
                                    .Item(ConstDonotAllowsaveItem, RowIndex).Value = 0
                                End If

                            End If
                        End If
                    End If
                Case Else
            End Select
        End With
    End Sub
    Private Sub AddDetails(ByVal DR As DataTable, Optional ByVal currentRow As Integer = 0)
        Dim i As Integer
        Dim PMult As Double
        With grdVoucher
            chgbyprg = True
            PMult = 1
            If currentRow > 0 Then
                i = currentRow
            Else
                i = .CurrentRow.Index
            End If
            ' .RowCount - 1
            .Item(ConstSlNo, i).Value = i + 1
            .Item(ConstItemCode, i).Value = IIf(IsDBNull(DR(0)("Item Code")), Trim(DR(0)("Item Code") & ""), DR(0)("Item Code"))
            .Item(ConstDescr, i).Value = DR(0)("Description")
            If EnableGST Then
                .Item(ConstBarcode, i).Value = DR(0)("HSNCode") 'hsncode
            Else
                .Item(ConstBarcode, i).Value = ""
            End If
            If Val(.Item(ConstQty, i).Value) = 0 Then
                .Item(ConstQty, i).Value = Format(1, lnumFormat) 'IIf(IsReturn, -1, 1)
            End If
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
            Dim dtunit As DataTable
            'Select Case DR(0)("Packing")
            '    Case "B"

            '    Case "P1"
            '        .Item(ConstB, .CurrentCell.RowIndex).Value = "P1"
            '        .Item(ConstPMult, i).Value = IIf(IsDBNull(DR(0)("P1Fra")), "2", DR(0)("P1Fra"))
            '        .Item(ConstUnit, i).Value = DR(0)("P1Unit")
            '        dtunit = _objcmnbLayer._fldDatatable("Select FraCount from UnitsTb where Units='" & DR(0)("P1Unit") & "'")
            '        If dtunit.Rows.Count > 0 Then
            '            .Item(ConstPFraction, i).Value = IIf(IsDBNull(dtunit(0)("FraCount")), "2", dtunit(0)("FraCount"))
            '        End If
            '    Case "P2"
            '        .Item(ConstB, .CurrentCell.RowIndex).Value = "P2"
            '        .Item(ConstPMult, i).Value = IIf(IsDBNull(DR(0)("P2Fra")), "2", DR(0)("P2Fra"))
            '        .Item(ConstUnit, i).Value = DR(0)("P2Unit")
            '        dtunit = _objcmnbLayer._fldDatatable("Select FraCount from UnitsTb where Units='" & DR(0)("P2Unit") & "'")
            '        If dtunit.Rows.Count > 0 Then
            '            .Item(ConstPFraction, i).Value = IIf(IsDBNull(dtunit(0)("FraCount")), "2", dtunit(0)("FraCount"))
            '        End If
            'End Select
            .Item(ConstB, .CurrentCell.RowIndex).Value = "B"
            .Item(ConstPMult, i).Value = 1
            .Item(ConstUnit, i).Value = DR(0)("Unit")
            .Item(ConstPFraction, i).Value = IIf(IsDBNull(DR(0)("FraCount")), "2", DR(0)("FraCount"))
            .Item(ConstWarrentyExpiry, i).Value = Format(DateAdd(DateInterval.Year, 1, DateValue(cldrdate.Value)), DtFormat)
            'If enableMultipleBarcodeOnItem Then
            '    .Item(ConstSerialNo, i).Value = txtitemcode.Text
            'End If
            If CDbl(.Item(ConstUPrice, i).Value) = 0 Or chgItm Then
                If chkws.Checked Then
                    .Item(ConstActualPrice, i).Value = Val(DR(0)("UnitPriceWS") & "")
                ElseIf chkcredit.Checked Then
                    .Item(ConstActualPrice, i).Value = Val(DR(0)("secondprice") & "")
                Else
                    .Item(ConstActualPrice, i).Value = Val(DR(0)("UnitPrice") & "")
                End If
                .Item(ConstUPrice, i).Value = Format(CDbl(.Item(ConstActualPrice, i).Value), lnumFormat)

            End If
            If Val(DR(0)("MRP") & "") = 0 Then
                DR(0)("MRP") = 0
            End If
            .Item(ConstMRP, i).Value = DR(0)("MRP")
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
                .Item(ConstIGSTP, i).Value = IIf(IsDBNull(DR(0)("vat")), 0, CDbl(DR(0)("vat")))
            ElseIf EnableGST Then
                getGSTDetails(Trim(.Item(ConstBarcode, i).Value & ""), i, False)
            End If
            'If enablecess And cessdate <= DateValue(cldrdate.Value) Then
            '    .Item(Constcess, i).Value = Format(IIf(IsDBNull(DR(0)("vat")), 0, CDbl(DR(0)("vat"))), lnumFormat)
            '    .Item(ConstcessAc, i).Value = IIf(IsDBNull(DR(0)("collectionAC")), 0, Val(DR(0)("collectionAC")))
            '    .Item(ConstcessAmt, i).Value = Format(((CDbl(.Item(ConstActualPrice, i).Value) * .Item(Constcess, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value), lnumFormat)
            'End If
            Dim cessAmt As Double
            If enablecess Then
                .Item(ConstRegcess, i).Value = Format(IIf(IsDBNull(DR(0)("rgcess")), 0, CDbl(DR(0)("rgcess"))), lnumFormat)
                .Item(ConstRegcessAc, i).Value = IIf(IsDBNull(DR(0)("rgccollectionac")), 0, Val(DR(0)("rgccollectionac")))
                If Val(DR(0)("additionalcess") & "") = 0 Then DR(0)("additionalcess") = 0
                .Item(ConstAdditionalcess, i).Value = IIf(IsDBNull(DR(0)("additionalcess")), 0, Val(DR(0)("additionalcess")))
                cessAmt = ((CDbl(.Item(ConstActualPrice, i).Value) * .Item(ConstRegcessAc, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value)
                If Val(.Item(ConstAdditionalcess, i).Value) = 0 Then .Item(ConstAdditionalcess, i).Value = 0
                cessAmt = cessAmt + CDbl(.Item(ConstAdditionalcess, i).Value)
            End If
            If enableFloodCess And cessdate <= DateValue(cldrdate.Value) Then
                .Item(Constcess, i).Value = Format(IIf(IsDBNull(DR(0)("vat")), 0, CDbl(DR(0)("vat"))), lnumFormat)
                .Item(ConstcessAc, i).Value = IIf(IsDBNull(DR(0)("collectionAC")), 0, Val(DR(0)("collectionAC")))
                cessAmt = cessAmt + (((CDbl(.Item(ConstActualPrice, i).Value) * .Item(Constcess, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value))
                .Item(ConstcessAmt, i).Value = Format(cessAmt, lnumFormat)
            End If
            If Val(DR(0)("CostAvg") & "") = 0 Then
                DR(0)("CostAvg") = 0
            End If
            .Item(ConstBatchCost, i).Value = DR(0)("CostAvg")
            .Item(ConstLTotal, i).Value = Format(.Item(ConstQty, i).Value * CDbl(.Item(ConstUPrice, i).Value), lnumFormat)
            .Item(ConstNUPrice, i).Value = Format(CDbl(.Item(ConstUPrice, i).Value), lnumFormat)
            .Item(ConstQty, i).Value = Format(CDbl(.Item(ConstQty, i).Value), "#,##0" & IIf(Val(.Item(ConstPFraction, i).Value) = 0, "", "." & Strings.StrDup(CByte(.Item(ConstPFraction, i).Value), "0")))
            .Item(ConstImpDocId, i).Value = ""
            .Item(ConstImpLnId, i).Value = ""
            .Item(ConstWoodQty, i).Value = Val(DR(0)("salesPontOncount") & "")
            chgAmt = True
            chgItm = False
            'chgUprice = True
            'calculateTaxFromUnitPrice(i)
            .ClearSelection()
            'If EnableRawmaterialUpdateInSales Then setRawmaterial(Val(.Item(ConstItemID, i).Value), i)
        End With
        calculate(, True)
        chgbyprg = False
    End Sub
    'Private Sub CalculateGST(Optional ByVal isAddcess As Boolean = False)
    '    Dim i As Integer
    '    Dim dtHSN As DataTable
    '    Dim dtrow As DataRow
    '    Dim slno As Integer

    '    dtTax.Rows.Clear()
    '    If isAddcess Then
    '        If enablecess And cessdate <= DateValue(cldrdate.Value) Then
    '            Dim dt As DataTable = _objcmnbLayer._fldDatatable("SELECT vatcode,convert(money, 0) Amount,collectionAC,vat,AccDescr From VatMasterTb Left join accmast on VatMasterTb.collectionAC=AccMast.accid", False)
    '            For i = 0 To dt.Rows.Count - 1
    '                dtrow = dtTax.NewRow
    '                dtrow("slno") = dtTax.Rows.Count + 1
    '                dtrow("AccountName") = dt(0)("AccDescr")
    '                dtrow("ACid") = dt(0)("collectionAC")
    '                dtrow("Amount") = 0
    '                dtTax.Rows.Add(dtrow)
    '            Next
    '        End If
    '    End If
    '    With grdVoucher
    '        Dim _qurey As EnumerableRowCollection(Of DataRow)
    '        For i = 0 To .RowCount - 1
    '            slno = 0
    '            _qurey = From data In dtGST.AsEnumerable() Where data("HSNCode") = Trim(.Item(ConstBarcode, i).Value & "") Select data
    '            If _qurey.Count > 0 Then
    '                dtHSN = _qurey.CopyToDataTable
    '                If Val(lblstatecode.Tag) = 0 Then
    '                    'adding CSGT Amount****
    '                    Dim a = (From data In dtTax.AsEnumerable() Where data("ACid") = dtHSN(0)("CGSTCAc") Select data("slno"))
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
    '                        dtrow("AccountName") = dtHSN(0)("CGSTCAname")
    '                        dtrow("ACid") = dtHSN(0)("CGSTCAc")
    '                        dtrow("Amount") = CDbl(.Item(ConstCGSTAmt, i).Value)
    '                        dtTax.Rows.Add(dtrow)
    '                    Else
    '                        dtTax(slno - 1)("Amount") = CDbl(dtTax(slno - 1)("Amount")) + CDbl(.Item(ConstCGSTAmt, i).Value)
    '                    End If
    '                    'adding SSGT Amount****
    '                    a = (From data In dtTax.AsEnumerable() Where data("ACid") = dtHSN(0)("SGSTCAc") Select data("slno"))
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
    '                        dtrow("AccountName") = dtHSN(0)("SGSTCAname")
    '                        dtrow("ACid") = dtHSN(0)("SGSTCAc")
    '                        dtrow("Amount") = CDbl(.Item(ConstSGSTAmt, i).Value)
    '                        dtTax.Rows.Add(dtrow)
    '                    Else
    '                        dtTax(slno - 1)("Amount") = CDbl(dtTax(slno - 1)("Amount")) + CDbl(.Item(ConstSGSTAmt, i).Value)
    '                    End If
    '                Else
    '                    'adding ISGT Amount****
    '                    Dim a = (From data In dtTax.AsEnumerable() Where data("ACid") = dtHSN(0)("IGSTCAc") Select data("slno"))
    '                    slno = 0
    '                    For Each itm In a
    '                        slno = itm
    '                    Next
    '                    If slno = 0 Then
    '                        dtrow = dtTax.NewRow
    '                        dtrow("slno") = dtTax.Rows.Count + 1
    '                        dtrow("AccountName") = dtHSN(0)("IGSTCAname")
    '                        dtrow("ACid") = dtHSN(0)("IGSTCAc")
    '                        dtrow("Amount") = CDbl(.Item(ConstIGSTAmt, i).Value)
    '                        dtTax.Rows.Add(dtrow)
    '                    Else
    '                        dtTax(slno - 1)("Amount") = CDbl(dtTax(slno - 1)("Amount")) + CDbl(.Item(ConstIGSTAmt, i).Value)
    '                    End If
    '                End If

    '            End If
    '            If enablecess Then
    '                Dim b = (From data In dtTax.AsEnumerable() Where data("ACid") = Val(.Item(ConstcessAc, i).Value & "") Select data("slno"))
    '                slno = 0
    '                For Each itm In b
    '                    slno = itm
    '                Next
    '                If slno > 0 Then
    '                    dtTax(slno - 1)("Amount") = CDbl(dtTax(slno - 1)("Amount")) + CDbl(.Item(ConstcessAmt, i).Value)
    '                End If
    '            End If

    '        Next
    '    End With

    'End Sub
    Private Sub CalculateGST(Optional ByVal isAddcess As Boolean = False)
        Dim i As Integer
        Dim dtHSN As DataTable
        Dim dtrow As DataRow
        Dim slno As Integer
        If dtTax Is Nothing Then Exit Sub
        If dtTax.Rows.Count > 0 Then
            dtTax.Rows.Clear()
        End If
        If enableGCC Then GoTo addtax
        If isAddcess Then
            If enablecess Or (enableFloodCess And cessdate <= DateValue(cldrdate.Value) And cessenddate >= DateValue(cldrdate.Value)) Then
addtax:
                Dim dt As DataTable = _objcmnbLayer._fldDatatable("SELECT  vatcode,convert(money, 0) Amount,collectionAC,vat,AccDescr From VatMasterTb Left join accmast on VatMasterTb.collectionAC=AccMast.accid", False)
                For i = 0 To dt.Rows.Count - 1
                    dtrow = dtTax.NewRow
                    dtrow("slno") = dtTax.Rows.Count + 1
                    dtrow("AccountName") = dt(i)("AccDescr")
                    dtrow("ACid") = dt(i)("collectionAC")
                    dtrow("Amount") = 0
                    dtTax.Rows.Add(dtrow)
                Next
            End If
        End If
        If EnableGST Then
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
                    If enableFloodCess And cessdate <= DateValue(cldrdate.Value) And cessenddate >= DateValue(cldrdate.Value) Then
                        Dim b = (From data In dtTax.AsEnumerable() Where data("ACid") = Val(.Item(ConstcessAc, i).Value & "") Select data("slno"))
                        slno = 0
                        For Each itm In b
                            slno = itm
                        Next
                        If slno > 0 Then
                            dtTax(slno - 1)("Amount") = CDbl(dtTax(slno - 1)("Amount")) + CDbl(.Item(ConstFloodCessAmt, i).Value)
                        End If
                    End If
                    If enablecess Then
                        Dim b = (From data In dtTax.AsEnumerable() Where data("ACid") = Val(.Item(ConstRegcessAc, i).Value & "") Select data("slno"))
                        slno = 0
                        For Each itm In b
                            slno = itm
                        Next
                        If slno > 0 Then
                            dtTax(slno - 1)("Amount") = CDbl(dtTax(slno - 1)("Amount")) + CDbl(.Item(ConstregularCessAmt, i).Value)
                        End If
                    End If
                Next
            End With
        ElseIf enableGCC Then
            Dim taxamt As Double
            With grdVoucher
                For i = 0 To .RowCount - 1
                    Dim b = (From data In dtTax.AsEnumerable() Where data("ACid") = Val(.Item(ConstcessAc, i).Value & "") Select data("slno"))
                    slno = 0
                    For Each itm In b
                        slno = itm
                    Next
                    If slno > 0 Then
                        dtTax(slno - 1)("Amount") = CDbl(dtTax(slno - 1)("Amount")) + CDbl(.Item(ConstTaxAmt, i).Value)
                    End If
                Next

            End With
        End If


    End Sub
    Private Sub getGSTDetails(ByVal hsncode As String, ByVal i As Integer, ByVal calculatefromGrid As Boolean)
        Dim dt As DataTable
        With grdVoucher
            If Not calculatefromGrid Then
                dt = _objcmnbLayer._fldDatatable("SELECT * FROM GSTTb WHERE HSNCODE='" & hsncode & "'")
                If dt.Rows.Count > 0 Then
                    .Item(ConstCGSTP, i).Value = Format(IIf(IsDBNull(dt(0)("CGST")), 0, CDbl(dt(0)("CGST"))), lnumFormat)
                    .Item(ConstSGSTP, i).Value = Format(IIf(IsDBNull(dt(0)("SGST")), 0, CDbl(dt(0)("SGST"))), lnumFormat)
                    .Item(ConstIGSTP, i).Value = Format(IIf(IsDBNull(dt(0)("IGST")), 0, CDbl(dt(0)("IGST"))), lnumFormat)
                Else
                    .Item(ConstCGSTP, i).Value = Format(0, lnumFormat)
                    .Item(ConstSGSTP, i).Value = Format(0, lnumFormat)
                    .Item(ConstIGSTP, i).Value = Format(0, lnumFormat)
                End If
            End If
            Dim actualPrice As Double
            Dim discountOther As Double
            discountOther = CDbl(.Item(ConstDiscOther, i).Value) * CDbl(.Item(ConstQty, i).Value)
            actualPrice = (CDbl(.Item(ConstActualPrice, i).Value) * CDbl(.Item(ConstQty, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value) - discountOther
            'actualPrice = Format(actualPrice, lnumFormat)
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
                            calculate(, True)
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
                    Dim actualprice As Double
                    Dim ttax As Double
                    Dim modoff As Double
                    Dim disablecess As Boolean
                    If cessenddate < DateValue(cldrdate.Value) Then
                        disablecess = True
                    End If
                    actualprice = CDbl(.Item(e.ColumnIndex, e.RowIndex).Value)
                    'ttax = CDbl(.Item(ConstTaxP, e.RowIndex).Value) + IIf(disablecess, 0, CDbl(.Item(Constcess, e.RowIndex).Value)) + CDbl(.Item(ConstRegcess, e.RowIndex).Value)
                    'actualprice = CDbl(.Item(ConstSP3, e.RowIndex).Value)
                    ttax = CDbl(.Item(ConstTaxP, e.RowIndex).Value) + IIf(disablecess, 0, CDbl(.Item(Constcess, e.RowIndex).Value)) + CDbl(.Item(ConstRegcess, e.RowIndex).Value)
                    actualprice = Format(actualprice + ((actualprice * ttax) / 100), numFormat)
                    modoff = Format(.Item(ConstMRP, e.RowIndex).Value - actualprice, numFormat)
                    If modoff < 0 Then modoff = modoff * -1
                    If modoff <= 0.01 Then actualprice = .Item(ConstMRP, e.RowIndex).Value
                    If chkmrp.Checked = False Then
                        If Val(.Item(ConstMRP, e.RowIndex).Value) > 0 And CDbl(.Item(ConstMRP, e.RowIndex).Value) < actualprice Then
                            MsgBox("Sales price should not be greater than MRP", MsgBoxStyle.Exclamation)
                            .Item(ConstUPrice, e.RowIndex).Value = 0
                            .Item(ConstActualPrice, e.RowIndex).Value = CDbl(.Item(ConstUPrice, e.RowIndex).Value)
                            calculateTaxFromUnitPrice(e.RowIndex)
                        End If
                    End If

            End Select
        End With
    End Sub
    Private Sub grdVoucher_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdVoucher.EditingControlShowing
        Dim Col As Integer
        Col = grdVoucher.CurrentCell.ColumnIndex
        If Col = ConstItemCode Or Col = ConstQty Or Col = ConstUPrice Or Col = ConstMRP Then
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
            ElseIf col = ConstQty Or col = ConstUPrice Then
                grdVoucher.Item(ConstqtyChg, grdVoucher.CurrentRow.Index).Value = "Chg"
            ElseIf col = ConstMRP Then
                grdVoucher.Item(ConstqtyChg, grdVoucher.CurrentRow.Index).Value = "Chg"
                _srchTxtId = 3
                _srchIndexId = 5
                chgbyprg = True
                strGridSrchString = MyCtrl.Text
                ShowPanel()
                chgbyprg = False
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
                plsrch.Width = 500
            Else
                plsrch.Height = 300
                plsrch.Width = 700
                'x = Me.Width - plsrch.Width - 100
                'y = Me.Height - plsrch.Height - 100
            End If
            If grdVoucher.CurrentCell.ColumnIndex = ConstMRP Then
                x = grdVoucher.Left + grdVoucher.Width - 400
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
            searchProductBatch(grdSrch, strGridSrchString, Val(grdVoucher.Item(ConstItemID, grdVoucher.CurrentRow.Index).Value), Val(grdVoucher.Item(ConstId, grdVoucher.CurrentRow.Index).Value), isrefreshBatchData, True)
            If grdSrch.RowCount > 0 And strGridSrchString = "" Then
                strGridSrchString = grdSrch.Item(5, 0).Value
            End If
        Else
            SearchProductPanel(grdSrch, "[Item Code]", strGridSrchString, True)
            If grdSrch.RowCount > 0 And strGridSrchString = "" Then
                strGridSrchString = grdSrch.Item(0, 0).Value
            End If
        End If
        If grdSrch.RowCount > 0 Then
            doSelect(2)
        End If

        _srchOnce = True
        chgbyprg = False
    End Sub
    'Private Sub ShowPanel()
    '    If Not _srchOnce Then
    '        chgbyprg = True
    '        Dim PopupLoc As Point
    '        Dim x As Integer = Me.Width - plsrch.Width - 100
    '        Dim y As Integer = Me.Height - plsrch.Height - 100
    '        PopupLoc = New Point(x, y)
    '        If plsrch.Visible = False Then
    '            plsrch.Location = PopupLoc
    '            plsrch.Visible = True
    '        End If
    '    End If
    '    SearchProductPanel(grdSrch, "[Item Code]", strGridSrchString, True)
    '    doSelect(2)
    '    _srchOnce = True
    '    chgbyprg = False
    'End Sub

    Private Sub grdVoucher_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.GotFocus
        If grdVoucher.RowCount = 0 Then
            AddRow()
        End If
        activecontrolname = "grdVoucher"
    End Sub
    Private Sub grdBeginEdit()
        If grdVoucher.CurrentCell.ColumnIndex = ConstUPrice And disablePriceEditInPos Then
            grdVoucher.CurrentCell.ReadOnly = True
        End If
        If Not grdVoucher.CurrentCell.ReadOnly Then
            chgbyprg = True
            grdVoucher.BeginEdit(True)
            activecontrolname = "grdVoucher"
            chgbyprg = False
        End If
    End Sub
    Private Sub openQuickItem(ByVal itemid As Long)
        fQuickItem = New QuickItem
        With fQuickItem
            .IsModi = IIf(itemid > 0, True, False)
            .ItemId = itemid
            If itemid > 0 Then
                .Timer1.Tag = 1
            End If
            .ShowDialog()
        End With
    End Sub
    Private Sub grdVoucher_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdVoucher.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If grdVoucher.RowCount = 0 Then Exit Sub
                If SrchText = "" And grdVoucher.CurrentCell.ColumnIndex = ConstItemCode Then
                    GoTo nxt
                End If
                If disablePriceEditInPos And grdVoucher.CurrentCell.ColumnIndex = ConstQty Then
                    txtitemcode.Focus()
                    Exit Sub
                End If
                If FindNextCell(grdVoucher, grdVoucher.CurrentCell.RowIndex, grdVoucher.CurrentCell.ColumnIndex + 1) Then
                    'AddRow()
                    txtitemcode.Focus()
                    Exit Sub
                End If
nxt:
                plsrch.Visible = False
                grdBeginEdit()

            ElseIf e.KeyCode = Keys.Up Or e.KeyCode = Keys.PageUp Then
                If grdVoucher.RowCount = 0 Then Exit Sub
                If plsrch.Visible Then
                    doSelect(0)
                    Exit Sub
                Else
                    'Dim i As Integer = grdVoucher.CurrentRow.Index
                    grdBeginEdit()
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
                        showSerialNoForm()
                    Case ConstTaxAmt, ConstTaxP
                        If EnableGST Then
                            tbgst.Visible = True
                            showItemGst(True, grdVoucher.CurrentRow.Index)
                        End If
                    Case ConstMRP
                        If enableBatchwiseTr Then
                            Dim frm As New SelectItemBatchFrm
                            Dim foundbatch As Integer
                            Dim ItmFlds() As String
                            ReDim ItmFlds(1)
                            frm.txtname.Tag = Val(grdVoucher.Item(ConstItemID, grdVoucher.CurrentRow.Index).Value)
                            'dtProductBatch = dt
                            frm.loadBatch()
                            frm.ShowDialog()
                            foundbatch = Val(frm.btnadd.Tag)
                            ItmFlds = frm.itms
                            If foundbatch = 1 Then
                                chgbyprg = True
                                grdVoucher.Item(ConstSerialNo, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(2) IsNot Nothing, ItmFlds(2), "")
                                'grdVoucher.Item(ConstBarcode, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(1) IsNot Nothing, ItmFlds(1), "")
                                grdVoucher.Item(ConstManufacturingdate, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(7) IsNot Nothing, ItmFlds(7), "")
                                grdVoucher.Item(ConstWarrentyExpiry, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(8) IsNot Nothing, ItmFlds(8), "")
                                grdVoucher.Item(ConstBatchQty, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(1) IsNot Nothing, ItmFlds(1), "")
                                If Val(ItmFlds(3)) = 0 Then ItmFlds(3) = 0
                                grdVoucher.Item(ConstMRP, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(3) IsNot Nothing, Format(CDbl(ItmFlds(3)), numFormat), "")
                                grdVoucher.Item(ConstBatchCost, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(9) IsNot Nothing, ItmFlds(9), "")
                                If chkcredit.Checked Then
                                    grdVoucher.Item(ConstActualPrice, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(5) IsNot Nothing, ItmFlds(5), "")
                                Else
                                    grdVoucher.Item(ConstActualPrice, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(4) IsNot Nothing, ItmFlds(4), "")
                                End If
                                If Val(grdVoucher.Item(ConstActualPrice, grdVoucher.CurrentCell.RowIndex).Value) = 0 Then grdVoucher.Item(ConstActualPrice, grdVoucher.CurrentCell.RowIndex).Value = 0
                                grdVoucher.Item(ConstUPrice, grdVoucher.CurrentCell.RowIndex).Value = Format(CDbl(grdVoucher.Item(ConstActualPrice, grdVoucher.CurrentCell.RowIndex).Value), numFormat)
                                chgbyprg = False
                                calculate(, True)
                            End If

                        End If
                End Select
            ElseIf e.KeyCode = Keys.Escape Then
                plsrch.Visible = False
                'ElseIf e.KeyCode = Keys.F3 Then
                '    AddRow()
            ElseIf e.KeyCode = Keys.F4 Then
                'If grdVoucher.RowCount = 0 Then Exit Sub
                'grdVoucher.Rows.RemoveAt(grdVoucher.CurrentCell.RowIndex)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub showSerialNoForm()
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
                    .detId = Val(grdVoucher.Item(ConstId, grdVoucher.CurrentRow.Index).Value)
                    .rowIndex = grdVoucher.CurrentCell.RowIndex + 1
                    '.dtCurrentItems = getSerialNumberFromDT(grdVoucher.CurrentCell.RowIndex, _objcmnbLayer.dtSerialNo)
                    '.isOut = True
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
                txtIgst.Text = Format(CDbl(.Item(ConstIGSTP, i).Value), lnumFormat)
                txtIgstAmt.Text = Format(CDbl(.Item(ConstIGSTAmt, i).Value), lnumFormat)

                txtCgst.Text = Format(0, lnumFormat)
                txtCgstAmt.Text = Format(0, lnumFormat)
                txtSgst.Text = Format(0, lnumFormat)
                txtSgstAmt.Text = Format(0, lnumFormat)
                txtCgst.Enabled = False
                txtCgstAmt.Enabled = False
                txtSgst.Enabled = False
                txtSgstAmt.Enabled = False
            Else
                txtCgst.Enabled = True
                txtCgstAmt.Enabled = True
                txtSgst.Enabled = True
                txtSgstAmt.Enabled = True
                txtCgst.Text = Format(CDbl(.Item(ConstCGSTP, i).Value), lnumFormat)
                txtCgstAmt.Text = Format(CDbl(.Item(ConstCGSTAmt, i).Value), lnumFormat)
                txtSgst.Text = Format(CDbl(.Item(ConstSGSTP, i).Value), lnumFormat)
                txtSgstAmt.Text = Format(CDbl(.Item(ConstSGSTAmt, i).Value), lnumFormat)

                txtIgst.Text = Format(0, lnumFormat)


                txtIgstAmt.Text = Format(0, lnumFormat)
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
                .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstIGSTAmt, i).Value), lnumFormat)
                .Item(ConstTaxP, i).Value = .Item(ConstIGSTP, i).Value
            Else
                .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstCGSTAmt, i).Value) + CDbl(.Item(ConstSGSTAmt, i).Value), lnumFormat)
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
        calculate(, True)
    End Sub
    Private Sub doSelect(ByVal Mup As Integer, Optional ByVal isfromgrid As Boolean = False)
        Try
            chgbyprg = True
            Dim ItmFlds() As String
            If plsrch.Visible = False And isfromgrid = False Then Exit Sub
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
                    'grdVoucher.Item(ConstSerialNo, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(2) IsNot Nothing, ItmFlds(2), "")
                    ''grdVoucher.Item(ConstBarcode, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(1) IsNot Nothing, ItmFlds(1), "")
                    'grdVoucher.Item(ConstManufacturingdate, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(3) IsNot Nothing, ItmFlds(3), "")
                    'grdVoucher.Item(ConstWarrentyExpiry, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(4) IsNot Nothing, ItmFlds(4), "")
                    'grdVoucher.Item(ConstBatchQty, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(1) IsNot Nothing, ItmFlds(1), "")
                    'If Val(ItmFlds(5)) = 0 Then ItmFlds(5) = 0
                    'If Val(ItmFlds(6)) = 0 Then ItmFlds(6) = 0
                    'If Val(ItmFlds(9)) = 0 Then ItmFlds(9) = 0
                    'grdVoucher.Item(ConstMRP, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(5) IsNot Nothing, Format(CDbl(ItmFlds(5)), numFormat), Format(0, numFormat))
                    'grdVoucher.Item(ConstBatchCost, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(9) IsNot Nothing, ItmFlds(9), 0)
                    'grdVoucher.Item(ConstActualPrice, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(6) IsNot Nothing, ItmFlds(6), 0)
                    'grdVoucher.Item(ConstUPrice, grdVoucher.CurrentCell.RowIndex).Value = Format(CDbl(grdVoucher.Item(ConstActualPrice, grdVoucher.CurrentCell.RowIndex).Value), numFormat)
                    'SrchText = IIf(ItmFlds(5) IsNot Nothing, ItmFlds(5), strGridSrchString)
                    'calculate(, True)
                    If Trim(ItmFlds(2) & "") <> "" Then
                        grdVoucher.Item(ConstBatchQty, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(1) IsNot Nothing, ItmFlds(1), "")
                        grdVoucher.Item(ConstSerialNo, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(2) IsNot Nothing, ItmFlds(2), "")
                        grdVoucher.Item(ConstMRP, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(3) IsNot Nothing, ItmFlds(3), "")
                        If Not chkcredit.Checked Then
                            grdVoucher.Item(ConstActualPrice, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(4) IsNot Nothing, ItmFlds(4), "")
                        Else
                            grdVoucher.Item(ConstActualPrice, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(6) IsNot Nothing, ItmFlds(6), "")
                        End If
                        If Val(grdVoucher.Item(ConstActualPrice, grdVoucher.CurrentCell.RowIndex).Value) = 0 Then grdVoucher.Item(ConstActualPrice, grdVoucher.CurrentCell.RowIndex).Value = 0
                        grdVoucher.Item(ConstUPrice, grdVoucher.CurrentCell.RowIndex).Value = Format(CDbl(grdVoucher.Item(ConstActualPrice, grdVoucher.CurrentCell.RowIndex).Value), lnumFormat)
                        grdVoucher.Item(ConstManufacturingdate, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(7) IsNot Nothing, ItmFlds(7), "")
                        grdVoucher.Item(ConstWarrentyExpiry, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(8) IsNot Nothing, ItmFlds(8), "")
                        grdVoucher.Item(ConstBatchCost, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(9) IsNot Nothing, ItmFlds(9), "")
                        'grdVoucher.Item(ConstMRP, grdVoucher.CurrentCell.RowIndex).Value = Format(CDbl(grdVoucher.Item(ConstMRP, grdVoucher.CurrentCell.RowIndex).Value), lnumFormat)
                        SrchText = IIf(ItmFlds(2) IsNot Nothing, ItmFlds(2), strGridSrchString)
                        calculate(, True)
                    End If
                Case 10
                    txtitemcode.Text = IIf(ItmFlds(0) IsNot Nothing, ItmFlds(0), "")
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
        'If tbrawMaterial.Visible And EnableRawmaterialUpdateInSales Then getRawMaterials(e.RowIndex)

    End Sub

    Private Sub RemoveRow(ByVal rIndex As Integer)
        If grdVoucher.RowCount > 0 Then
            If MsgBox("You Are Going to Remove the Row." & vbCrLf & "Are You Sure ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then
                Exit Sub
            End If
            With grdVoucher
                'Dim cindex As Integer = .CurrentRow.Index
                'deleteDtSerialNo(_objcmnbLayer.dtSerialNo, grdVoucher.Item(ConstSerialNo, grdVoucher.CurrentRow.Index).Value, Val(.Item(ConstItemID, .CurrentRow.Index).Value))
                'deleteDtSerialNos(grdVoucher.CurrentRow.Index, False, _objcmnbLayer.dtSerialNo)
                If Val(btnhold.Tag) = 1 Then
                    _objcmnbLayer._saveDatawithOutParm("DELETE FROM ItmInvTrTb WHERE id=" & Val(.Item(ConstId, rIndex).Value))
                Else
                    _objcmnbLayer._saveDatawithOutParm("update ItmInvTrTb set setremove=1 WHERE id=" & Val(.Item(ConstId, rIndex).Value))
                End If
                .Rows.RemoveAt(rIndex)
                .ClearSelection()
                '.Rows(.CurrentRow.Index).Selected = True
                getItemInfo(0)
                calculate(, IIf(Val(numDisc.Text) > 0, True, False))
            End With
            reArrangeNo()
        End If
        'If enableMultiplePointsOnLineItem Then deleteMultipleServicePoints(rIndex)
        'If EnableRawmaterialUpdateInSales Then DeleteRawmaterial(rIndex, False)
        chggrd = True
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
        txtCashCustomer.Text = ""
        txtcustAddress.Text = ""
        txtcustemail.Text = ""
        txtphone.Text = ""
        txtgiftvoucher.Text = ""
        txtsalesman.Text = ""
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
        lblinvcount.Text = ""
        cldrdate.Value = DateValue(Date.Now)
        lbldate.Text = DateValue(Date.Now)
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
        cmbholdedInvs.Tag = 0
        btnhold.Tag = 0
        lblbalance.Text = Format(0, numFormat)
        lbllimit.Text = Format(0, numFormat)
        lblInvoices.Text = Format(0, numFormat)
        chkautoroundOff.Checked = enableAutoRoundOff
        chkcredit.Checked = False
        rdocashcustomer.Checked = True

        lblearned.Text = ""
        lblredeemed.Text = ""
        lblpointbalance.Text = ""
        lblpointValue.Text = ""
        lblpointbalance.Tag = ""
        numDisc.ReadOnly = False
        txtdp.ReadOnly = False
        btnredeem.Text = "Redeem Points"
        txtsramt.Text = Format(0, numFormat)
        txtsrno.Text = ""
        chgdiscount = False
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
            If userType Then
                btnupdate.Tag = IIf(getRight(46, CurrentUser), 1, 0)
                btndelete.Tag = IIf(getRight(47, CurrentUser), 1, 0)
            Else
                btnupdate.Tag = 1
                btndelete.Tag = 1
            End If

        Else
            AddNewClick()

        End If
    End Sub



    Private Function Verify() As Boolean
        Dim _vAcMaster As DataTable
        clsreader()
        clsCnnection()
        If isModi Then
            numVchrNo.Text = numVchrNo.Tag
            If loadedTrId = 0 Then
                MsgBox("Voucher not yet loaded !!", vbExclamation)
                numVchrNo.Focus()
                Return False
            End If
        End If
        calculate()
        If EnableGST Then CalculateGST(True)
        ActBr = ""
        If Val(numVchrNo.Text) < 1 Then
            MsgBox("Invalid Voucher Number !!", vbExclamation)
            MyActiveControl = numVchrNo
            Return False
        End If
        If Not IsDate(cldrdate.Value) Then
            MsgBox("Invalid Voucher Date !!", vbExclamation)
            cldrdate.Focus()
            Return False
        End If
        'If txtReference.Text = "" Then
        '    MsgBox("Invalid Reference", MsgBoxStyle.Exclamation)
        '    txtReference.Focus()
        '    Exit Sub
        'End If
        If DateValue(cldrdate.Value) <= getProtectUntil() Then
            MsgBox("Voucher Date comes within Protected Date Range !!", MsgBoxStyle.Information)
            cldrdate.Focus()
            Return False
        End If
        _vAcMaster = _objcmnbLayer._fldDatatable(StrAccMastSrch & " WHERE Alias = '" & txtSuppAlias.Text & "' ORDER BY AccDescr")
        If _vAcMaster.Rows.Count = 0 Then
            MsgBox("Enter a valid  Customer Account !!", vbExclamation)
            txtSuppName.Focus()
            'txtSuppAlias.Focus()
            Return False
        Else
            txtSuppAlias.Tag = _vAcMaster(0)("accid")
        End If
        If Val(txtPurchAlias.Tag) = 0 Then
            MsgBox("Sales A/C could not found!", MsgBoxStyle.Exclamation)
            cmbVoucherTp.Focus()
            Return False
        End If
        If blockInvoicing() Then Return False
        If Not isModi Then
            If chekDuplicate() Then Return False
        End If
        'If CheckRestrictImport() = False Then Exit Sub
        If Not chkGridvalue() Then Return False
        If CDbl(lblNetAmt.Text) < 0 Then
            MsgBox("Net Amount below Zero is not allowed !!!?", vbExclamation)
            MyActiveControl = numDisc
            Return False
        End If
        If txtphone.Text = "" And enablePhoneNumberMandatory Then
            MsgBox("Customer phone number is mandatory!", MsgBoxStyle.Exclamation)
            txtphone.Focus()
            Exit Function
        End If
        Return True
    End Function

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
    Private Sub savePos(ByVal itemrow As Integer)
        If loadedTrId = 0 Then

            'loadedTrId = Val(_objInv._saveCmn())
            Dim dt As DataTable
            dt = _objcmnbLayer._fldDatatable("declare @tempInvNo int SELECT @tempInvNo=max(tempInvNo) FROM ItmInvCmnTb where  TrType='IS' and Counter ='" & POScounter & "'" & _
                                             " insert into ItmInvCmnTb (tempInvNo,IsPOS,InvNo,invStatus,Counter,trtype,TrDate,DueDate) values (isnull(@tempInvNo,0)+1,1,0,1,'" & _
                                             POScounter & "','IS',GETDATE(),GETDATE()) select scope_identity() maxno")
            If dt.Rows.Count > 0 Then
                loadedTrId = dt(0)(0)
            End If
        End If
        saveInvTr(loadedTrId)
        'Dim iddet As Long
        'setInvDetValue(loadedTrId, 1, itemrow)
        'iddet = _objInv._savePOSDetails(True)
        'grdVoucher.Item(ConstId, itemrow).Value = iddet
        'If EnableRawmaterialUpdateInSales Then
        '    '_objcmnbLayer._saveDatawithOutParm("delete from ItmInvTrTb where " & _
        '    '                             "ISNULL(isRawMaterial,0)=1 and slno=" & itemrow + 1)
        '    For i = 0 To dtRawMaterial.Rows.Count - 1
        '        setInvRawValue(loadedTrId, 1, i)
        '        _objInv._saveDetails()
        '    Next
        'End If
    End Sub

    Private Sub saveTrans()
        Dim TrId As Long
        Dim i As Integer
        Dim DiscAcc As Long
        Dim SRAcc As Long
        Dim PPerU As Single
        Dim TDrAmt As Double
        'Dim dateChanged As Boolean
        'Dim qtychanged As Boolean
        Dim dtTable As DataTable
        If Val(txtfcrt.Text) = 0 Then txtfcrt.Text = 0
        FCRt = CDbl(txtfcrt.Text)
        If FCRt = 0 Then
            FCRt = 1
        End If
        clsreader()
        clsCnnection()


        'setVoucherType()
        savecashcustomer()
        If Not isModi Then
chkagain:
            If Not CheckNoExists(txtprefix.Text, Val(numVchrNo.Text), "IS", "Inventory") Then
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
        dtTable = _objcmnbLayer._fldDatatable("SELECT accid FROM AccMast WHERE AccSetId Like '%28%'")
        If dtTable.Rows.Count > 0 Then SRAcc = dtTable(0)("accid")
        'calculate()
        'If dtTable.Rows.Count > 0 Then dtTable.Clear()
        If loadedTrId > 0 Then
            dtTable = _objcmnbLayer._fldDatatable("SELECT TOP 1 * FROM ItmInvCmnTb WHERE TrId = " & loadedTrId)
            If dtTable.Rows.Count = 0 Then
                MsgBox("The loaded voucher not found.  It may removed externally.", vbExclamation)
                Exit Sub
            End If
            '_objcmnbLayer._saveDatawithOutParm("Update ItmInvTrTb set setRemove=1 WHERE trid=" & loadedTrId)
            TrId = loadedTrId
        End If
        setInvCmnValue(TrId, False)
        TrId = Val(_objInv._saveCmn())
        strCustomername = IIf(txtCashCustomer.Text = "", strCustomername, txtCashCustomer.Text)
        Dim cscode As Long
        If LcashAcc = SRAcc Then
            LcashAmt = LcashAmt + CDbl(txtsramt.Text)
            SRAcc = 0
        End If
        If LcreditAcc > 0 And LcreditAmt > 0 Then
            cscode = LcreditAcc
        End If
        If LcardAmt > 0 And LcardAcc > 0 Then
            cscode = LcardAcc
        End If
        Dim points As Double
        If giftvoucherSalesValue > 0 Then
            points = CDbl(lblNetAmt.Text) / giftvoucherSalesValue
            discountPointsCollected = points - (points - Math.Truncate(points))
        Else
            discountPointsCollected = 0
        End If

        discountPointsCollected = discountPointsCollected * giftvoucherPointPerValue
        _objcmnbLayer._saveDatawithOutParm("UPDATE ItmInvCmnTb SET IsPOS=1," & IIf(cscode > 0, "CSCode=" & cscode & ",", "") & " invStatus=0," & _
                                           "tempInvNo=0,CashCustName='" & strCustomername & "'," & _
                                           "cashAmount=" & LcashAmt & "," & _
                                           "CardAmount=" & LcardAmt & "," & _
                                           "discountPointsCollected=" & discountPointsCollected & "," & _
                                           "redeemPoints=" & Val(lblpointbalance.Tag) & "," & _
                                           "tenderd=" & LtendAmt & "," & _
                                           "poschange=" & LchangeAmt & "," & _
                                           "customerPhone='" & txtphone.Text & "'," & _
                                           "giftno='" & txtgiftvoucher.Text & "'," & _
                                           "isTaxInvoice=" & IIf(chktaxInv.Checked, 1, 0) & "," & _
                                           "CashCustid=" & Val(txtphone.Tag) & "," & _
                                           "SRDeduction=" & CDbl(txtsramt.Text) & "," & _
                                           "deductedSRNO='" & Trim(txtsrno.Text & "'") & "," & _
                                           "Counter='" & (POScounter & "'") & _
                                           " WHERE TRID=" & TrId)
        saveMultipleDebits(TrId)
        'saveMultipleServicePoints(TrId)
        'to check whether date has been changed or not
        'if changed there should be calculeted cost average for all items
        'dtTable = _objcmnbLayer._fldDatatable("SELECT TOP 1 * FROM ItmInvCmnTb LEFT JOIN VoucherTypeNoTb ON ItmInvCmnTb.TypeNo=VoucherTypeNoTb.vrno " & _
        '                                              "WHERE InvType='OUT' AND Trdate >='" & Format(DateValue(cldrdate.Value), "yyyy/MM/dd") & "'")
        'If dtTable.Rows.Count > 0 Then
        '    dateChanged = True
        'Else
        '    dateChanged = False
        'End If
        ReDim JobAcc(0)
        JobAcc(0).Acc = Val(txtPurchAlias.Tag)
        JobAcc(0).Job = txtJob.Text
        Dim amt As Double
        If Val(txtroundOff.Text) > 0 Then
            amt = CDbl(lblTotAmt.Text) - (IIf(cmbsign.SelectedIndex = 1, 1, -1) * CDbl(txtroundOff.Text))
        Else
            amt = CDbl(lblTotAmt.Text)
        End If
        amt = IIf(DiscAcc = 0, CDbl(lblNetAmt.Text) - CDbl(lbltax.Text) - CDbl(lblcess.Text), amt)
        'amt = IIf(SRAcc = 0, CDbl(lblNetAmt.Text) - CDbl(lbltax.Text) - CDbl(lblcess.Text) - CDbl(txtsramt.Text), amt + CDbl(txtsramt.Text))
        JobAcc(0).Amt = amt ' IIf(DiscAcc = 0, CDbl(lblNetAmt.Text) - CDbl(lbltax.Text) - CDbl(lblcess.Text), amt)
        Dim itemidsdatatable As New DataTable
        'SRAcc = 0
        'itemidsdatatable.Columns.Add(New DataColumn("Itemid", GetType(Long)))
        TDrAmt = saveInvTr(TrId)
        UpdateAccounts(TrId, TDrAmt, DiscAcc, SRAcc)
        If Not fwait Is Nothing Then fwait.Close() : fwait = Nothing
        'If EnableRawmaterialUpdateInSales Then
        '    _objcmnbLayer._saveDatawithOutParm("delete from ItmInvTrTb where " & _
        '                                 "ISNULL(isRawMaterial,0)=1 and trid=" & loadedTrId)
        '    For i = 0 To dtRawMaterial.Rows.Count - 1
        '        setInvRawValue(TrId, 1, i)
        '        _objInv._savePOSDetails()
        '    Next
        'End If
        If isModi = False Or Val(cmbholdedInvs.Text) > 0 Then
            numPrintVchr.Text = numVchrNo.Text
            txtPPrefix.Text = txtprefix.Text
            numVchrNo.Tag = Val(numVchrNo.Text) 'Trim(CMNREC(12).FormattedText)
            txtprefix.Tag = txtprefix.Text
            txtprefix.Text = SetNextVrNo(numVchrNo, Val(cmbVoucherTp.Tag), "IS", "TrType = 'IS' AND InvNo = ", False, True, True)
        End If
        ChgId = False
        chgPost = False
        AddNewClick()
        getHoldInvoices()
        If enablePrintOnSave Then
            If MsgBox("Invoice Updated Successfully, " & vbCrLf & "Do you want print?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                PrepareRpt("ISPOS", True)
                If duplicatebillinPOS Then
                    PrepareRpt("ISPOS", True)
                End If
            End If

        End If

        'MsgBox("Sales Invoice is succesfully posted with Vr. # " & numPrintVchr.Text & ".", MsgBoxStyle.Information)
        txtitemcode.Focus()
        numVchrNo.Tag = ""
    End Sub

    Private Sub UpdateAccounts(ByVal TrId As Long, ByVal TDrAmt As Double, ByVal DiscAcc As Integer, ByVal sracc As Integer)
        Dim LinkNo As Long
        Dim Refference As String
        Refference = Trim(txtprefix.Text) & IIf(txtprefix.Text = "", "", "/") & numVchrNo.Text
        Dim dtTable As DataTable
        If isModi And TrId > 0 Then
            dtTable = _objcmnbLayer._fldDatatable("SELECT LinkNo FROM AccTrCmn WHERE lnkno  = " & TrId)
            If dtTable.Rows.Count > 0 Then
                LinkNo = dtTable(0)("LinkNo")
                _objcmnbLayer._saveDatawithOutParm("DELETE FROM AccTrDet WHERE LinkNo=" & LinkNo)
            End If
        End If

        setAcctrCmnValue(TrId, LinkNo)
        'LinkNo = 0
        'LinkNo = Val(_objTr.SaveAccTrCmn())
        If dtAccTb.Rows.Count > 0 Then
            dtAccTb.Rows.Clear()
        End If
        Dim EntRef As String = Strings.Left(Trim(txtDescr.Text) & IIf(Trim(txtDescr.Text) = "", "", " / ") & txtPurchaseName.Text, 249)
        Dim dlAmt As Double = (TDrAmt - IIf(DiscAcc > 0, CDbl(numDisc.Text), 0)) * FCRt
        Dim ttlTxAmount As Double
        'Tax Entry Credit
        Dim i As Integer = 0
        If EnableGST Then
            For i = 0 To dtTax.Rows.Count - 1
                If Val(dtTax(i)("ACid")) > 0 And Val(dtTax(i)("Amount")) > 0 Then
                    Dim TxAmount As Double = CDbl(dtTax(i)("Amount"))
                    dlAmt = dlAmt + (TxAmount * FCRt)
                    ttlTxAmount = Format(CDbl(ttlTxAmount) + TxAmount, numFormat)
                    setAcctrDetValue(LinkNo, Val(dtTax(i)("ACid")), Trim(Refference), dtTax(i)("AccountName") & " Tax Collected", TxAmount * -1 * FCRt, Strings.Left(txtJob.Text, 50), "", 0, 0, "", "", Val(txtSuppAlias.Tag), Val(txtSuppAlias.Tag) & Refference, cmbfc.Text, FCRt)
                    '_objTr.saveAccTrans()
                End If
            Next
        ElseIf ShowTaxOnInventory Then
            For i = 0 To dtTax.Rows.Count - 1
                If Val(dtTax(i)("collectionAC")) > 0 And Val(dtTax(i)("Amount")) > 0 Then
                    Dim TxAmount As Double = CDbl(dtTax(i)("Amount"))
                    dlAmt = dlAmt + (TxAmount * FCRt)
                    ttlTxAmount = Format(CDbl(ttlTxAmount) + TxAmount, numFormat)
                    setAcctrDetValue(LinkNo, Val(dtTax(i)("collectionAC")), Trim(Refference), dtTax(i)("Vatcode") & " Tax Collected", TxAmount * -1 * FCRt, Strings.Left(txtJob.Text, 50), "", 0, 0, "", "", Val(txtSuppAlias.Tag), Val(txtSuppAlias.Tag) & Refference, cmbfc.Text, FCRt)
                    '_objTr.saveAccTrans()
                End If
            Next
        End If
        'Debit Entry
        If Val(txtroundOff.Text) > 0 Then
            dlAmt = dlAmt - (IIf(cmbsign.SelectedIndex = 1, 1, -1) * CDbl(txtroundOff.Text))
        End If
        If LcashAcc > 0 And LcashAmt > 0 Then
            setAcctrDetValue(LinkNo, Val(LcashAcc), Trim(Refference), EntRef, LcashAmt, "", Strings.Left(txtJob.Text, 50), 0, 0, "", _
                        "", Val(txtSuppAlias.Tag), Val(txtSuppAlias.Tag) & Refference, cmbfc.Text, FCRt)
            '_objTr.saveAccTrans()
        End If
        If LcardAcc > 0 And LcardAmt > 0 Then
            EntRef = "Amount Received with  CARD NUMBER: " & Lcardnumber
            setAcctrDetValue(LinkNo, Val(LcardAcc), Trim(Refference), EntRef, LcardAmt, "", Strings.Left(txtJob.Text, 50), 0, 0, "", _
                        "", Val(txtSuppAlias.Tag), Val(txtSuppAlias.Tag) & Refference, cmbfc.Text, FCRt)
            '_objTr.saveAccTrans()
        End If
        If LcreditAcc > 0 And LcreditAmt <> 0 Then
            If LcreditAmt < 0 Then
                EntRef = "Collection "
            End If
            setAcctrDetValue(LinkNo, Val(LcreditAcc), Trim(Refference), EntRef, LcreditAmt, "", Strings.Left(txtJob.Text, 50), 0, 0, "", _
                        "", Val(txtSuppAlias.Tag), Val(txtSuppAlias.Tag) & Refference, cmbfc.Text, FCRt)
            '_objTr.saveAccTrans()
            If LcreditAmt < 0 Then
                setAcctrDetValue(LinkNo, Val(LcashAcc), Trim(Refference), "Collection Received from " & strCustomername, LcreditAmt * -1, "", Strings.Left(txtJob.Text, 50), 0, 0, "", _
                        "", Val(txtSuppAlias.Tag), Val(txtSuppAlias.Tag) & Refference, cmbfc.Text, FCRt)
                '_objTr.saveAccTrans()
            End If
        End If


        'Credit Entry
        'For j = 0 To JobAcc.Count - 1
        '    setAcctrDetValue(LinkNo, j)
        '    '_objTr.saveAccTrans()
        'Next
        Dim cramt As Double
        cramt = CDbl(lblTotAmt.Text)
        cramt = IIf(DiscAcc = 0, CDbl(lblNetAmt.Text) - CDbl(lbltax.Text) - CDbl(lblcess.Text), cramt)
        Dim total As Double = cramt + ttlTxAmount
        total = CDbl(lblNetAmt.Text) - total
        cramt = (cramt + total) * FCRt
        Dim reference As String = Trim(txtprefix.Text) & IIf(txtprefix.Text = "", "", "/") & numVchrNo.Text
        Dim crTrRef As String = Strings.Left(Trim(txtDescr.Text) & IIf(txtDescr.Text = "", " Sales /", "/ Sales / ") & txtSuppName.Text, 249)
        setAcctrDetValue(0, Val(txtPurchAlias.Tag), reference, crTrRef, cramt * -1, txtJob.Text, "", 0, 0, "", _
                             "", Val(txtSuppAlias.Tag), "", cmbfc.Text, FCRt)
        'DiscountEntry
        If (CDbl(numDisc.Text)) > 0 And DiscAcc > 0 Then
            dlAmt = CDbl(numDisc.Text) * FCRt
            setAcctrDetValue(LinkNo, DiscAcc, Trim(Refference), Trim(txtDescr.Text), dlAmt, "", "", 2, 0, "", _
                            "", Val(txtSuppAlias.Tag), DiscAcc & Refference, cmbfc.Text, FCRt)
            '_objTr.saveAccTrans()
        End If
        'SR Deduction
        If (CDbl(txtsramt.Text)) > 0 And sracc > 0 Then
            dlAmt = CDbl(txtsramt.Text) * FCRt
            setAcctrDetValue(LinkNo, sracc, Trim(Refference), "Sales Return Deduction from " & txtsrno.Text, dlAmt, "", "", 2, 0, "", _
                            "", Val(txtSuppAlias.Tag), sracc & Refference, cmbfc.Text, FCRt)
            '_objTr.saveAccTrans()
        End If
        _objTr.SaveAccTrWithDt(dtAccTb)

        'updateStockTransaction(TrId, LinkNo)
        'updateClosingBalanceForInvoice(TrId)
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
            costAmt = Val(dt(0)("costAmt") & "")
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
    Private Sub setInvCmnValue(ByVal InvTrid As Long, ByVal istempInv As Boolean)
        With _objInv
            Dt = DateValue(cldrdate.Value)
            .TrId = InvTrid
            .TrDate = Dt
            .TrType = "IS"
            .DocLstTxt = ""
            .InvTypeNo = Val(cmbVoucherTp.Tag)
            .SlsManId = cmbsalesman.Text
            .Prefix = Trim(txtprefix.Text)
            If istempInv Then
                .InvNo = 0
            Else
                .InvNo = Val(numVchrNo.Text)
            End If
            .TrRefNo = Trim(txtprefix.Text) & IIf(txtprefix.Text = "", "", "/") & numVchrNo.Text ' Trim(txtReference.Text)
            .CSCode = IIf(LcreditAcc = 0, Val(txtSuppAlias.Tag), LcreditAcc)
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
            .TypeNo = getVouchernumber("IS")
            .EnaJob = False
            .DocDefLoc = Dloc
            .SlsManId = txtsalesman.Text
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
            .TaxType = 0
            .OthrCust = txtcustAddress.Text
            .isTaxInvoice = EnableGST
            .custid = Val(txtphone.Tag)
        End With

    End Sub
    Private Sub setInvRawValue(ByVal Invid As Long, ByVal PPerU As Single, ByVal i As Integer)
        _objInv.dtTrId = Invid
        _objInv.ItemId = dtRawMaterial(i)("ItemId")
        _objInv.Unit = dtRawMaterial(i)("Unit")
        _objInv.taxP = 0
        _objInv.taxAmt = 0
        _objInv.TrQty = dtRawMaterial(i)("qty")
        _objInv.UnitCost = dtRawMaterial(i)("unitprice")
        _objInv.PFraction = 1 ' dtRawMaterial(i)("PFraction")

        _objInv.UnitOthCost = 0
        _objInv.Method = 1
        _objInv.UnitDiscount = 0
        _objInv.ItemDiscount = 0
        _objInv.DisP = 0
        _objInv.IDescription = dtRawMaterial(i)("itemname")

        _objInv.SlNo = Val(dtRawMaterial(i)("slno") + 1)
        _objInv.TrTypeNo = getVouchernumber("IS")
        _objInv.TrDateNo = getDateNo(CDate(cldrdate.Value))
        _objInv.TrType = "IS"
        _objInv.id = 0
        _objInv.WarrentyName = ""
        _objInv.SerialNo = ""
        _objInv.HSNCode = ""
        _objInv.CSGTP = 0
        _objInv.CGSTAMT = 0
        _objInv.SGSTP = 0 ' CDbl(.Item(ConstSGSTP, i).Value)
        _objInv.SGSTAmt = 0 '0 CDbl(.Item(ConstSGSTAmt, i).Value)
        _objInv.IGSTP = 0 'CDbl(.Item(ConstIGSTP, i).Value)
        _objInv.IGSTAmt = 0 ' CDbl(.Item(ConstIGSTAmt, i).Value)
        _objInv.WarrentyExpDate = DateValue("01/01/1950")
        _objInv.MINslno = 0
        '_objInv.isRawMaterial = 1
        _objInv.itemcost = 0
    End Sub
    Private Sub setInvDetValue(ByVal Invid As Long, ByVal PPerU As Single, ByVal i As Integer)
        With grdVoucher
            PPerU = Val(.Item(ConstPMult, i).Value) 'Switch(.TextMatrix(i, 4) = "B", 1, .TextMatrix(i, 4) = "P1", CSng(grdVoucher.TextMatrix(i, 18)), .TextMatrix(i, 4) = "P2", CSng(grdVoucher.TextMatrix(i, 19)))
            PPerU = IIf(PPerU = 0, 1, PPerU)

            _objInv.dtTrId = Invid
            _objInv.ItemId = IIf(.Item(ConstSlNo, i).Value.ToString <> "M", Val(.Item(ConstItemID, i).Value), 0)
            _objInv.Unit = .Item(ConstUnit, i).Value
            _objInv.TrQty = CDbl(.Item(ConstQty, i).Value) * PPerU
            _objInv.UnitCost = CDbl(.Item(ConstActualPrice, i).Value) * FCRt / PPerU 'CDbl(.TextMatrix(i, 10)) / PPerU
            _objInv.MRP = CDbl(.Item(ConstMRP, i).Value) * FCRt / PPerU
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
            _objInv.TrTypeNo = getVouchernumber("IS")
            _objInv.TrDateNo = getDateNo(CDate(cldrdate.Value))
            _objInv.TrType = "IS"
            _objInv.id = Val(.Item(ConstId, i).Value)
            _objInv.WarrentyName = .Item(ConstLocation, i).Value
            _objInv.SerialNo = Trim(.Item(ConstSerialNo, i).Value & "")
            If IsDate(.Item(ConstWarrentyExpiry, i).Value) Then
                _objInv.WarrentyExpDate = DateValue(.Item(ConstWarrentyExpiry, i).Value)
            Else
                _objInv.WarrentyExpDate = DateValue("01/01/1950")
            End If
            _objInv.HSNCode = Trim(.Item(ConstBarcode, i).Value & "")
            If Val(.Item(ConstIGSTP, i).Value & "") = 0 Then .Item(ConstIGSTP, i).Value = 0
            _objInv.IGSTP = CDbl(.Item(ConstIGSTP, i).Value)
            If Val(.Item(ConstCGSTP, i).Value & "") = 0 Then .Item(ConstCGSTP, i).Value = 0
            _objInv.CSGTP = CDbl(.Item(ConstCGSTP, i).Value)
            If Val(.Item(ConstSGSTP, i).Value & "") = 0 Then .Item(ConstSGSTP, i).Value = 0
            _objInv.SGSTP = CDbl(.Item(ConstSGSTP, i).Value)
            If chktaxInv.Checked Then
                If Val(.Item(ConstCGSTAmt, i).Value & "") = 0 Then .Item(ConstCGSTAmt, i).Value = 0
                If Val(.Item(ConstSGSTAmt, i).Value & "") = 0 Then .Item(ConstSGSTAmt, i).Value = 0
                If Val(.Item(ConstIGSTAmt, i).Value & "") = 0 Then .Item(ConstIGSTAmt, i).Value = 0
                If Val(.Item(ConstcessAmt, i).Value & "") = 0 Then .Item(ConstcessAmt, i).Value = 0
                If Val(.Item(ConstregularCessAmt, i).Value & "") = 0 Then .Item(ConstregularCessAmt, i).Value = 0
                If Val(.Item(ConstAdditionalcess, i).Value & "") = 0 Then .Item(ConstAdditionalcess, i).Value = 0
            Else
                .Item(ConstCGSTAmt, i).Value = 0
                .Item(ConstSGSTAmt, i).Value = 0
                .Item(ConstIGSTAmt, i).Value = 0
                .Item(ConstregularCessAmt, i).Value = 0
                .Item(ConstFloodCessAmt, i).Value = 0
                .Item(ConstAdditionalcess, i).Value = 0
                .Item(ConstcessAmt, i).Value = 0
            End If

            _objInv.IGSTAmt = CDbl(.Item(ConstIGSTAmt, i).Value) * FCRt
            _objInv.CGSTAMT = CDbl(.Item(ConstCGSTAmt, i).Value) * FCRt
            _objInv.SGSTAmt = CDbl(.Item(ConstSGSTAmt, i).Value) * FCRt
            _objInv.regularcessAmt = (CDbl(.Item(ConstregularCessAmt, i).Value) * FCRt)
            _objInv.FloodcessAmt = (CDbl(.Item(ConstFloodCessAmt, i).Value) * FCRt)
            _objInv.AdditionalcessAmt = ((CDbl(.Item(ConstAdditionalcess, i).Value) * CDbl(.Item(ConstQty, i).Value)) * FCRt)
            _objInv.CessAmt = (CDbl(grdVoucher.Item(ConstcessAmt, i).Value) * FCRt)
            _objInv.Smancode = Trim(.Item(Constsman, i).Value & "")

            'If Val(.Item(ConstIGSTP, i).Value & "") = 0 Then .Item(ConstIGSTP, i).Value = 0
            '_objInv.IGSTP = CDbl(.Item(ConstIGSTP, i).Value)
            'If Val(.Item(ConstIGSTAmt, i).Value & "") = 0 Then .Item(ConstIGSTAmt, i).Value = 0
            '_objInv.IGSTAmt = CDbl(.Item(ConstIGSTAmt, i).Value)

            'If Val(.Item(ConstCGSTP, i).Value & "") = 0 Then .Item(ConstCGSTP, i).Value = 0
            '_objInv.CSGTP = CDbl(.Item(ConstCGSTP, i).Value)
            'If Val(.Item(ConstCGSTAmt, i).Value & "") = 0 Then .Item(ConstCGSTAmt, i).Value = 0
            '_objInv.CGSTAMT = CDbl(.Item(ConstCGSTAmt, i).Value)
            'If Val(.Item(ConstSGSTP, i).Value & "") = 0 Then .Item(ConstSGSTP, i).Value = 0
            '_objInv.SGSTP = CDbl(.Item(ConstSGSTP, i).Value)
            'If Val(.Item(ConstSGSTAmt, i).Value & "") = 0 Then .Item(ConstSGSTAmt, i).Value = 0
            '_objInv.SGSTAmt = CDbl(.Item(ConstSGSTAmt, i).Value)
            '_objInv.CessAmt = (CDbl(grdVoucher.Item(ConstcessAmt, i).Value) * FCRt) / CDbl(PPerU)


            If _objInv.ItemId = 0 Then
                _objInv.TrQty = 1
                '_objInv.UnitCost = 1
                '_objInv.taxP = 1
                '_objInv.taxAmt = 1
                '_objInv.UnitDiscount = 0
            End If
            If Val(.Item(ConstBatchCost, i).Value & "") = 0 Then .Item(ConstBatchCost, i).Value = 0
            _objInv.itemcost = CDbl(.Item(ConstBatchCost, i).Value)
            '_objInv.isRawMaterial = 0
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
        _objTr.JVType = "IS"
        _objTr.JVDate = DateValue(cldrdate.Value)
        _objTr.PreFix = Trim(txtprefix.Text)
        _objTr.JVNum = Val(numVchrNo.Text)
        _objTr.JVTypeNo = getVouchernumber("IS")
        _objTr.UserId = Trim(CurrentUser & "")
        _objTr.MchName = MACHINENAME
        _objTr.CrtDtTm = Dt
        _objTr.TypeNo = getVouchernumber("IS")
        '_objTr.VrDescr = Trim(txtpayee.Text)
        _objTr.IsModi = IIf(LinkNo > 0, 1, 0)
        _objTr.LinkNo = InvTrid
        _objTr.taxablevalue = CDbl(lbltaxable.Text)
        _objTr.taxvalue = CDbl(lbltax.Text)
    End Sub
    Private Sub setAcctrDetValue(ByVal lnkNo As Long, ByVal jbIndex As Integer)
        With _objTr
            .trLinkNo = lnkNo
            .AccountNo = JobAcc(jbIndex).Acc
            .Reference = Trim(txtprefix.Text) & IIf(txtprefix.Text = "", "", "/") & numVchrNo.Text
            .EntryRef = Strings.Left(Trim(txtDescr.Text) & IIf(JobAcc(jbIndex).Job = "", "", " / " & JobAcc(jbIndex).Job) & " / " & txtSuppName.Text, 249)
            .DealAmt = -1 * JobAcc(jbIndex).Amt * FCRt
            .FCAmt = -1 * JobAcc(jbIndex).Amt * FCRt
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
    'Private Sub setAcctrDetValue(ByVal lnkNo As Long, ByVal AccountNo As Long, ByVal Reference As String, ByVal EntryRef As String, ByVal DealAmt As Double, ByVal JobCode As String, ByVal JobStr As String, _
    '                              ByVal TrInf As Short, ByVal OthCost As Short, ByVal LPO As String, ByVal TermsId As String, ByVal CustAcc As Integer, ByVal AccWithRef As String, _
    '                              ByVal CurrencyCode As String, ByVal CurrRate As Double)
    '    With _objTr
    '        .trLinkNo = lnkNo
    '        .AccountNo = AccountNo
    '        .Reference = Trim(txtprefix.Text) & IIf(txtprefix.Text = "", "", "/") & numVchrNo.Text
    '        .EntryRef = EntryRef
    '        .DealAmt = DealAmt
    '        .JobCode = JobCode
    '        .JobStr = JobStr
    '        .CurrRate = CurrRate
    '        .CurrencyCode = CurrencyCode ' IIf(chkFC.Checked = True, txtFC.Text, "")
    '        .TrInf = TrInf
    '        .OthCost = OthCost
    '        .TermsId = TermsId
    '        .CustAcc = CustAcc
    '        .AccWithRef = AccWithRef
    '        .LPONo = LPO
    '        Dim dtLPO As Date = IIf(chkDate(cldrdate.Value), cldrdate.Value, DateValue(cldrdate.Value))
    '        Dim dtDue As Date = DateValue(clrDuedate.Value)
    '        Dim dtSup As Date = DateValue(cldrdate.Value)
    '        .DocDate = dtLPO
    '        .SuppInvDate = dtSup
    '        .DueDate = dtDue
    '    End With
    'End Sub
    Private Sub setAcctrDetValue(ByVal lnkNo As Long, ByVal AccountNo As Long, ByVal Reference As String, ByVal EntryRef As String, ByVal DealAmt As Double, ByVal JobCode As String, ByVal JobStr As String, _
                              ByVal TrInf As Short, ByVal OthCost As Short, ByVal LPO As String, ByVal TermsId As String, ByVal CustAcc As Integer, ByVal AccWithRef As String, _
                              ByVal CurrencyCode As String, ByVal CurrRate As Double, Optional ByVal vatcode As String = "", Optional ByVal UnqNo As Long = 0)

        Dim dtrow As DataRow
        Dim dtLPO As Date = IIf(chkDate(cldrdate.Value), cldrdate.Value, DateValue(cldrdate.Value))
        Dim dtDue As Date = DateValue(clrDuedate.Value)
        Dim dtSup As Date = DateValue(cldrdate.Value)
        dtrow = dtAccTb.NewRow
        dtrow("LinkNo") = lnkNo
        dtrow("AccountNo") = AccountNo
        dtrow("Reference") = Reference ' Trim(txtprefix.Text) & IIf(txtprefix.Text = "", "", "/") & numVchrNo.Text
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
        dtrow("UnqNo") = UnqNo

        'dtrow("vatcode") = vatcode
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

    Private Sub ldSman()
        'AddtoCombo(cmbsalesman, "SELECT SManCode FROM SalesmanTb", True, False)
        AddDttoCombo(cmbsalesman, dtsalesman, True, False)
    End Sub

    Private Sub LodCurrency()
        'Dim dt As DataTable = _objcmnbLayer._fldDatatable("SELECT CurrencyCode FROM CurrencyTb", False)

        cmbfc.Items.Clear()
        cmbfc.Items.Add("")
        Dim i As Integer
        For i = 0 To dtcurrentyTb.Rows.Count - 1
            cmbfc.Items.Add(dtcurrentyTb(i)("CurrencyCode"))
        Next
    End Sub
    'Private Sub LodCurrency()
    '    Dim dt As DataTable = _objcmnbLayer._fldDatatable("SELECT CurrencyCode FROM CurrencyTb", False)
    '    cmbfc.Items.Clear()
    '    cmbfc.Items.Add("")
    '    Dim i As Integer
    '    For i = 0 To dt.Rows.Count - 1
    '        cmbfc.Items.Add(dt(i)("CurrencyCode"))
    '    Next
    'End Sub

    Private Sub returnFcrt()
        Dim dt As DataTable = _objcmnbLayer._fldDatatable("Select CurrencyRate,[Decimal Places] from CurrencyTb where CurrencyCode='" & cmbfc.Text & "'", False)
        If (dt.Rows.Count > 0) Then
            NDec = dt(0)("Decimal Places")
            lnumFormat = "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))
            txtfcrt.Text = Format(CDbl(dt(0)("CurrencyRate")), lnumFormat)
        Else
            txtfcrt.Text = Format(0, numFormat)
            NDec = NoOfDecimal
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

    Private Sub POSInvoice_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtitemcode.Focus()

    End Sub

    Private Sub SalesInvoice_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
        If Not fSelect Is Nothing Then fSelect.Close() : fSelect = Nothing
        If Not fProductEnquiry Is Nothing Then fProductEnquiry.Close() : fProductEnquiry = Nothing
        If Not fSlctDoc Is Nothing Then fSlctDoc.Close() : fSlctDoc = Nothing
        If Not fSerialno Is Nothing Then fSerialno.Close() : fSerialno = Nothing
        If Not fTendered Is Nothing Then fTendered.Close() : fTendered = Nothing
    End Sub

    Private Sub SalesOrderFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Timer2.Enabled = True
        chkcal.Checked = calcluatetaxFrompriceInv
        chkmrp.Checked = False
        loadlastInvoicenumber()
        btnupdate.Tag = 1
        btndelete.Tag = 1
        'lblcompany.Text = ""
    End Sub
    Private Sub frmload()
        Try
            'Exit Sub
            loadInventoryFormLoadMasters(True, 4, "IS", 3, DiscAcc, TrTypeNo)
            StrAccMastSrch = "SELECT AccDescr, Alias, ClosingBal As Balance, OpnBal, AccountNo, S1AccHd.S1AccId, AccSetId, GrpSetOn,accid FROM" & _
                            " AccMast INNER JOIN S1AccHd ON S1AccHd.S1AccId = AccMast.S1AccId "
            'btnNext_Click(btnNext, New System.EventArgs())
            'dtTax = _objcmnbLayer._fldDatatable("SELECT vatcode,convert(decimal,0) Amount,collectionAC,paymentAC,vat From VatMasterTb", False)
            chksearchitem.Visible = False
            'chksearchitem.Checked = Not enableMultipleBarcodeOnItem
            btnbarcode.Visible = False
            btnshift.Visible = False
            If ShowTaxOnInventory Or EnableGST Then
                CreateTaxTable(dtTax)
                chktaxInv.Checked = True
                If withNonTaxBill Then
                    chktaxInv.Checked = False
                End If
            End If
            If userType Then
                diableNegativeSale = getRight(171, CurrentUser)
            Else
                diableNegativeSale = False
            End If
            'If EnableRawmaterialUpdateInSales Then
            '    dtRawMaterial = New DataTable
            'CreateRawMaterialTable(dtRawMaterial)
            '    chkrawmaterial.Visible = True
            '    SetGridHeadRawMaterials()
            'End If
            'dtTb = _objcmnbLayer._fldDatatable("SELECT trid,itemid,id FROM ItmInvTrTb WHERE TRID=0")
            _objcmnbLayer.dtSerialNo = createdtSerialNo()
            dtpoints = createMultiplePointsOnSales()
            SetGridHead()
            crtSubVrs(cmbVoucherTp, 4, True)
            lnumFormat = numFormat
            FCRt = 1
            OthCost = 0
            chgbyprg = True
            Me.Text = "POS"
            cldrdate.Value = Format(Date.Now, DtFormat)
            NDec = NoOfDecimal
            loadedTrId = 0
            chgbyprg = False
            ldSman()
            LodCurrency()
            lbldate.Text = DateValue(Date.Now)
            'cmbVrType_SelectedIndexChanged(sender, e)
            'numVchrNo.Select()
            getHoldInvoices()
            ChgId = False
            If isModi Then
                btnModify_Click(btnModify, New System.EventArgs)
                If userType Then
                    btnupdate.Tag = IIf(getRight(46, CurrentUser), 1, 0)
                    btndelete.Tag = IIf(getRight(47, CurrentUser), 1, 0)
                Else
                    btnupdate.Tag = 1
                    btndelete.Tag = 1
                End If
            Else
                AddNewClick()
                If userType Then
                    btnupdate.Tag = IIf(getRight(45, CurrentUser), 1, 0)
                Else
                    btnupdate.Tag = 1
                End If
                'btndelete.Text = "Clear"
                btndelete.Tag = 1
            End If
            Timer1.Enabled = True
            cessdate = getCessDate()
            chkautoroundOff.Checked = enableAutoRoundOff
            chkcredit.Text = IIf(enableCreditPrice, "Credit Price", "Second Price")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub setVoucherType()
        Dim PreFixTb As DataTable
        Dim category As Integer
        If LcashAcc > 0 And LcashAmt > 0 Then
            category = 1
        End If
        If LcardAcc > 0 And LcardAmt > 0 Then
            category = 2
        End If
        If LcreditAcc > 0 And LcreditAmt <> 0 Then
            category = 3
        End If
        PreFixTb = _objcmnbLayer._fldDatatable("SELECT * FROM PreFixTb WHERE VrTypeNo=4 and Ctgry=" & category & IIf(UsrBr = "", "", " AND BrId In ('', '" & UsrBr & "')") & " Order by ordNo")
        If PreFixTb.Rows.Count > 0 Then
            cmbVoucherTp.Text = PreFixTb(0)("Voucher Name")
            cmbVoucherTp.Tag = PreFixTb(0)("Id")
        End If
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
        dtTable = _objcmnbLayer._fldDatatable("SELECT * FROM ItmInvCmnTb WHERE invStatus=0 and TrType = 'IS' AND InvNo = " & Val(numVchrNo.Text) & " AND PreFix = '" & MkDbSrchStr(Trim(txtprefix.Text), True) & "'")
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
                'If .Item(ConstIsSerial, r).Value = 1 And .Item(ConstSerialNo, r).Value = "" Then
                '    .Rows(r).Selected = True
                '    .CurrentCell = .Item(ConstQty, r)
                '    MsgBox("Serial Number cannot be Blank !!", vbExclamation)
                '    .FirstDisplayedScrollingRowIndex = r
                '    GoTo Ter
                'End If
                'If .Item(ConstSerialNo, r).Value <> "" Then
                '    _qurey = From data In dtExstSrlno.AsEnumerable() Where data(0) = .Item(ConstSerialNo, r).Value And data(1) = .Item(ConstItemID, r).Value Select data
                '    If _qurey.Count = 0 And enableSerialnumber Then
                '        .Rows(r).Selected = True
                '        .CurrentCell = .Item(ConstSerialNo, r)
                '        MsgBox("Serial Number not found!", vbExclamation)
                '        .FirstDisplayedScrollingRowIndex = r
                '        .BeginEdit(True)
                '        GoTo Ter
                '    Else
                '        _qurey = Nothing
                '    End If
                '    _qurey = Nothing
                '    _qurey = From data In dtSrlNo.AsEnumerable() Where data(0) = .Item(ConstSerialNo, r).Value Select data
                '    If _qurey.Count > 0 And enableSerialnumber Then
                '        .Rows(r).Selected = True
                '        .CurrentCell = .Item(ConstSerialNo, r)
                '        MsgBox("Serial Number already Sold !!", vbExclamation)
                '        .FirstDisplayedScrollingRowIndex = r
                '        .BeginEdit(True)
                '        GoTo Ter
                '    End If
                'End If
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
                If chkremovealert.Checked = 0 And enableMultiplePointsOnLineItem = False Then
                    If CDbl(.Item(ConstUPrice, r).Value) = 0 Then
                        .Rows(r).Selected = True
                        .CurrentCell = .Item(ConstUPrice, r)
                        MsgBox("Entered Price/Unit of Item [" & .Item(ConstItemCode, r).Value & "] is zero !", MsgBoxStyle.Exclamation)
                        .FirstDisplayedScrollingRowIndex() = r
                        GoTo Ter
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
        Valid(grdVoucher.CurrentRow.Index, ConstItemCode, txtitemcode.Text)
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
            txtitemcode.Focus()
        End If
    End Sub

    Private Sub numDisc_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles numDisc.KeyPress, txtdp.KeyPress, txtsmanP.KeyPress, txtfcrt.KeyPress, txtroundOff.KeyPress
        NumericTextOnKeypress(sender, e, chgbyprg, lnumFormat)
    End Sub

    Private Sub numDisc_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numDisc.TextChanged
        If Not chgNumByPgm And Val(lblTotAmt.Text) > 0 Then
            chgNumByPgm = True
            'If Val(numDisc.Text) = 0 Then
            '    numDisc.Text = Format(0, lnumFormat)
            'End If
            If Val(numDisc.Text) = 0 Then
                txtdp.Text = Format(0, numFormat)
            Else
                txtdp.Text = Format((CDbl(numDisc.Text) * 100) / CDbl(lblTotAmt.Text), lnumFormat)
            End If

            chgdiscount = True
            chgPost = True
        End If
        chgNumByPgm = False

    End Sub

    Private Sub numDisc_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles numDisc.Validated
        calculate(, True, True)
    End Sub

    Private Sub grdSrch_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSrch.CellDoubleClick
        activecontrolname = "grdVoucher"
        doSelect(2)
        chgbyprg = True
        grdVoucher.CurrentCell = grdVoucher.Item(1, grdVoucher.CurrentRow.Index)
        chgItm = True
        Valid(grdVoucher.CurrentCell.RowIndex, grdVoucher.CurrentCell.ColumnIndex, txtitemcode.Text)
        chgbyprg = False
        grdBeginEdit()
        plsrch.Visible = False
    End Sub


    Private Sub txtSuppAlias_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSuppAlias.Validated, txtSuppName.Validated
        If txtSuppAlias.Text = "" Then Exit Sub
        setCustomer()
    End Sub
    Private Sub setCustomer(Optional ByVal accid As Long = 0, Optional ByVal giftno As String = "")
        Dim dt As DataTable
        If txtSuppAlias.Text = "" And accid = 0 Then GoTo els
        If accid > 0 Then
            dt = _objcmnbLayer._fldDatatable("SELECT AccMast.accid,Alias,AccDescr,Address1,Address2,Address3,Address4," & _
                                         "Phone,Fax,EMail,PT1,PT2,PT3,PT4,TrdLcno,TrdDate,CreditLimit,DueDays,SlsmanId," & _
                                         "CurrencyCode,CountryCode,GSTIN,GrpSetOn,GiftVrNo " & _
                                         " from AccMast LEFT JOIN AccMastAddr ON AccMast.accid=AccMastAddr.AccountNo " & _
                                         "LEFT JOIN S1AccHd ON S1AccHd.S1AccId=AccMast.S1AccId " & _
                                         "LEFT JOIN (SELECT GiftVrNo,customeraccount FROM CashCustomerTb)CashCustomerTb ON CashCustomerTb.customeraccount=AccMast.Accid " & _
                                         "where accid=" & accid)
        ElseIf giftno <> "" Then
            dt = _objcmnbLayer._fldDatatable("SELECT AccMast.accid,Alias,AccDescr,Address1,Address2,Address3,Address4," & _
                                         "Phone,Fax,EMail,PT1,PT2,PT3,PT4,TrdLcno,TrdDate,CreditLimit,DueDays,SlsmanId," & _
                                         "CurrencyCode,CountryCode,GSTIN,GrpSetOn,GiftVrNo " & _
                                         " from AccMast LEFT JOIN AccMastAddr ON AccMast.accid=AccMastAddr.AccountNo " & _
                                         "LEFT JOIN S1AccHd ON S1AccHd.S1AccId=AccMast.S1AccId " & _
                                         "LEFT JOIN (SELECT GiftVrNo,customeraccount,custid FROM CashCustomerTb)CashCustomerTb ON CashCustomerTb.customeraccount=AccMast.Accid " & _
                                         "where GiftVrNo=" & giftno)
        Else
            dt = _objcmnbLayer._fldDatatable("SELECT AccMast.accid,Alias,AccDescr,Address1,Address2,Address3,Address4," & _
                                         "Phone,Fax,EMail,PT1,PT2,PT3,PT4,TrdLcno,TrdDate,CreditLimit,DueDays,SlsmanId," & _
                                         "CurrencyCode,CountryCode,GSTIN,GrpSetOn,GiftVrNo " & _
                                         " from AccMast LEFT JOIN AccMastAddr ON AccMast.accid=AccMastAddr.AccountNo " & _
                                         "LEFT JOIN S1AccHd ON S1AccHd.S1AccId=AccMast.S1AccId " & _
                                         "LEFT JOIN (SELECT GiftVrNo,customeraccount FROM CashCustomerTb)CashCustomerTb ON CashCustomerTb.customeraccount=AccMast.Accid " & _
                                         "where Alias='" & txtSuppAlias.Text & "'")
        End If
        chgbyprg = True
        If dt.Rows.Count > 0 Then
            If Trim("" & dt(0)("GrpSetOn")) <> "Cash" Then
                txtphone.Tag = dt(0)("accid")
                txtphone.Text = Trim("" & dt(0)("Phone"))
                txtCashCustomer.Text = Trim("" & dt(0)("AccDescr"))
                txtgiftvoucher.Text = Trim(dt(0)("GiftVrNo") & "")
                txtcustAddress.Text = Trim(dt(0)("Address1") & "")
                If Trim(dt(0)("Address2") & "") <> "" Then
                    txtcustAddress.Text = txtcustAddress.Text & vbCrLf & Trim(dt(0)("Address2") & "")
                End If
                If Trim(dt(0)("Address3") & "") <> "" Then
                    txtcustAddress.Text = txtcustAddress.Text & vbCrLf & Trim(dt(0)("Address3") & "")
                End If

                If Val(dt(0)("CreditLimit") & "") = 0 Then
                    dt(0)("CreditLimit") = 0
                End If
                lbllimit.Text = Format(Val(dt(0)("CreditLimit")), lnumFormat)
                If rdocreditcustomer.Checked Then
                    Dim iNBal As Double = getAccBal(Val(txtphone.Tag))
                    lblbalance.Text = Strings.Format(iNBal, lnumFormat)
                Else
                    lblbalance.Text = Strings.Format(0, lnumFormat)
                End If
            End If
            getGiftCardPoints()
        Else
els:
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
            txtphone.Text = ""
            txtCashCustomer.Text = ""
            txtgiftvoucher.Text = ""
            lblinvcount.Text = "Invoices : 0"
            lblearned.Text = ""
            lblredeemed.Text = ""
            lblpointbalance.Text = ""
            lblpointValue.Text = ""
            lblpointbalance.Tag = ""
            lblInvoices.Text = Format(0, lnumFormat)
            lblbalance.Text = Format(0, lnumFormat)
            lbllimit.Text = Format(0, lnumFormat)
        End If
        chgbyprg = False
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
            .strType = "IS"
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
            fRptFormat.RptType = "ISPOS"
            fRptFormat.ShowDialog()
            fRptFormat = Nothing
        Else
            PrepareRpt("ISPOS")
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
        btnPreview.Enabled = True
        btnprint.Enabled = True
        isModi = False
        'btnNext_Click(btnNext, New System.EventArgs)
        enableCtrls(False)
        btnModify.Text = "&Modify"
        'btndelete.Text = "Clear"
        isModi = False
        btnSlct.Visible = False
        numVchrNo.ReadOnly = True
        txtReference.Select()
        If userType Then
            btnupdate.Tag = IIf(getRight(45, CurrentUser), 1, 0)
        Else
            btnupdate.Tag = 1
        End If
        btndelete.Text = "Delete Holded"

        btndelete.Tag = 1
        Dim found As Boolean = setDefaultVoucher()
        If Not found Then
            If cmbVoucherTp.SelectedIndex > 0 Then
                cmbVoucherTp.SelectedIndex = 0
            Else
                cmbVoucherTp_SelectedIndexChanged(cmbVoucherTp, New System.EventArgs)
            End If
        End If
        'btnNext.Visible = True
    End Sub
    Private Function setDefaultVoucher() As Boolean
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT [Voucher Name] FROM UserTb " & _
                                        "left join PreFixTb on UserTb.defaultISVoucher=PreFixTb.id " & _
                                        "WHERE UserId='" & CurrentUser & "' and isnull([Voucher Name],'')<>''")
        Dim index As Integer = cmbVoucherTp.SelectedIndex
        Dim found As Boolean
        If dt.Rows.Count > 0 Then
            cmbVoucherTp.Text = dt(0)("Voucher Name")
            found = True
        Else
            found = False
        End If
        If cmbVoucherTp.SelectedIndex = index And found Then
            cmbVoucherTp_SelectedIndexChanged(cmbVoucherTp, New System.EventArgs)
        End If
        Return found
    End Function

    Private Sub ClearClick()
        If MsgBox("Do You Want Clear Etry", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        ClearControls()
    End Sub

    Private Sub DeleteClick(Optional ByVal isdeleteFromExit As Boolean = False)
        If loadedTrId = 0 Then Exit Sub
        If Not isdeleteFromExit Then
            If MsgBox("Do you want to remove Holded Invoice?", vbYesNo + vbQuestion + vbDefaultButton2) = MsgBoxResult.No Then Exit Sub
        End If
        _objInv.TrId = loadedTrId
        _objInv.TrType = "OUT"
        _objInv.deleteInventoryTransactions()
        ChgId = False
        AddNewClick()
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
        If chgPost And loadedTrId = 0 Then
            If MsgBox("Changes Found ! if you do not hold the invoice it will be removed ! " & vbCrLf & "Are you sure to exit?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            If loadedTrId > 0 Then DeleteClick(True)
        End If
        Me.Close()
    End Sub
    Private Sub saveTenderedAmt(ByVal Tendered As Double, ByVal tChange As Double, ByVal custid As Long, ByVal debitacc As Long, ByVal trid As Long)
        _objInv = New clsInvoice
        With _objInv
            .TenderedAmt = Tendered
            .ChangeAmt = tChange
            .TeTrid = trid
            .Tid = 0
            .saveTenderedAmt()

        End With
    End Sub
    Private Sub showTendered()
        If Not fTendered Is Nothing Then fTendered.Close() : fTendered = Nothing
        fTendered = New TenderedEntryFrm
        With fTendered
            '.lblgrossTotal.Text = Format(CDbl(lblNetAmt.Text) + CDbl(numDisc.Text), lnumFormat)
            .txtsrno.Text = txtsrno.Text
            If Val(txtsramt.Text) = 0 Then txtsramt.Text = Format(0, numFormat)
            .txtsramt.Text = Format(CDbl(txtsramt.Text), lnumFormat)
            .lblNetAmt.Text = Format(CDbl(lblNetAmt.Text), lnumFormat)
            .txttendered.Text = Format(CDbl(lblNetAmt.Text) - CDbl(txtsramt.Text), lnumFormat)
            .lblchange.Text = Format(0, lnumFormat)
            .lblredeem.Text = Format(CDbl(numDisc.Text), numFormat)
            .lblearned.Text = lblearned.Text
            .lblredeemed.Text = lblredeemed.Text
            .lblpointbalance.Text = lblpointbalance.Text
            .lblpointValue.Text = lblpointValue.Text
            .lblvouchername.Text = cmbVoucherTp.Text
            .lblqty.Text = Val(lblqty.Text)
            .Timer1.Tag = posType
            If rdocreditcustomer.Checked Then
                .txtcustAddress.Text = txtcustAddress.Text
                .txtcustomer.Text = txtCashCustomer.Text
                .txtcustomer.Tag = Val(txtphone.Tag)
                .lblbalance.Text = Format(CDbl(lblbalance.Text), numFormat)
                .lblcbalance.Text = Format(CDbl(lblbalance.Text) + (CDbl(.lblchange.Text) * -1), numFormat)
                .lbllimit.Text = Format(lbllimit.Text, numFormat)
            End If
            'If Not fwait Is Nothing Then fwait.Close() : fwait = Nothing
            .ShowDialog()

            txtitemcode.Focus()
        End With
    End Sub
    Private Sub UpdateClick(Optional ByVal ispos As Boolean = False, Optional ByVal posType As Integer = 0)
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
        If Val(btnupdate.Tag) = 0 Then
            MsgBox("This user do not have permission to Update", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If CDbl(lblNetAmt.Text) = 0 Then
            MsgBox("Invoice amount should be greater than zero", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If ispos Then
            If enablePhoneNumberMandatory And txtphone.Text = "" Then
                Dim frm As New POSCustomerFrm
                frm.ShowDialog()
                With frm
                    chgbyprg = True
                    txtCashCustomer.Text = .txtCashCustomer.Text
                    txtphone.Text = .txtphone.Text
                    chgbyprg = False
                    If txtphone.Text <> "" Then
                        phoneValidate()
                    End If
                End With
                frm = Nothing
            End If
            If Verify() Then
                showTendered()
                Exit Sub
            Else
                Exit Sub
            End If
        End If
        'If Val(btnupdate.Tag) = 0 Then
        '    MsgBox("This user do not have permission to Update", MsgBoxStyle.Exclamation)
        '    Exit Sub
        'End If
        'If MsgBox("Verification is succeeded. Do you like to file it ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = vbYes Then

        'End If
        'saveTrans()
        loadWaite(1)
        plsrch.Visible = False
        With fTendered
            .btnupdate.Text = "NEW"
            .btnupdate.Focus()
            .btnupdate.BackColor = Color.Green
        End With
        txtitemcode.Focus()
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
        _objInv.TrType = "IS"

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

    Private Sub cmnGridbutton_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncalc.Click
        System.Diagnostics.Process.Start("Calc")
        'AddRow()
        'grdVoucher.CurrentCell = grdVoucher.Item(ConstItemCode, grdVoucher.CurrentRow.Index)
        'doCommandStat(True)
        'chgPost = True
    End Sub

    Private Sub btnrem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrem.Click
        If grdVoucher.CurrentRow Is Nothing Then Exit Sub
        RemoveRow(grdVoucher.CurrentRow.Index)
        chgPost = True
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        posType = 0
        loadWaite(3)
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        If btndelete.Text = "Clear" Then
            If Val(btnhold.Tag) = 1 Then GoTo del
            AddNewClick()
        Else
del:
            DeleteClick()
            txtitemcode.Focus()
            getHoldInvoices()
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
                    dt = _objcmnbLayer._fldDatatable("SELECT P1Unit U,P1Fra F,UnitPrice,isnull(P1Price1,0) pprice,UnitPriceWS,additionalcess,secondPrice,PMRP1 PMRP,InvItm.ItemId FROM " & _
                                                     "InvItm " & _
                                                     " left join InvItmPackingTb on InvItmPackingTb.itemid=invitm.itemid " & _
                                                     "WHERE InvItm.Itemid=" & Val(.Item(ConstItemID, .CurrentCell.RowIndex).Value))
                ElseIf .Item(ConstB, .CurrentCell.RowIndex).Value = "P1" Then
                    .Item(ConstB, .CurrentCell.RowIndex).Value = "P2"
                    dt = _objcmnbLayer._fldDatatable("SELECT P2Unit U,P2Fra F,UnitPrice,isnull(P2Price1,0) pprice,UnitPriceWS,additionalcess,secondPrice,PMRP2 PMRP ,InvItm.ItemId FROM " & _
                                                     "InvItm " & _
                                                     " left join InvItmPackingTb on InvItmPackingTb.itemid=invitm.itemid " & _
                                                     "WHERE InvItm.Itemid=" & Val(.Item(ConstItemID, .CurrentCell.RowIndex).Value))
                ElseIf .Item(ConstB, .CurrentCell.RowIndex).Value = "P2" Then
u:
                    .Item(ConstB, .CurrentCell.RowIndex).Value = "B"
                    dt = _objcmnbLayer._fldDatatable("SELECT Unit U,1 F,UnitPrice,0 pprice,UnitPriceWS,additionalcess,secondPrice,MRP PMRP,ItemId FROM InvItm WHERE Itemid=" & Val(.Item(ConstItemID, .CurrentCell.RowIndex).Value))
                End If
                If dt Is Nothing Then GoTo u

                If dt.Rows.Count > 0 Then
                    chgbyprg = True
                    .Item(ConstUnit, .CurrentCell.RowIndex).Value = dt(0)("U")
                    If Val(dt(0)("PMRP") & "") = 0 Then dt(0)("PMRP") = 0
                    .Item(ConstMRP, .CurrentCell.RowIndex).Value = dt(0)("PMRP")
                    .Item(ConstPMult, .CurrentCell.RowIndex).Value = dt(0)("F")
                    Dim cost As Double
                    Dim addcess As Double
                    addcess = Val(dt(0)("additionalcess") & "") * Val(.Item(ConstPMult, .CurrentCell.RowIndex).Value & "")
                    cost = getLastSalesAmt(Val(txtSuppAlias.Tag), dt(0)("ItemId"), True, "IS", CDbl(dt(0)("UnitPrice")))
                    If Val(.Item(ConstPMult, .CurrentCell.RowIndex).Value & "") = 0 Then
                        .Item(ConstPMult, .CurrentCell.RowIndex).Value = 0
                    End If
                    If (.Item(ConstB, .CurrentCell.RowIndex).Value <> "B" And Val(dt(0)("pprice")) = 0) Or .Item(ConstB, .CurrentCell.RowIndex).Value = "B" Then
                        cost = cost * Val(.Item(ConstPMult, .CurrentCell.RowIndex).Value)
                        .Item(ConstActualPrice, .CurrentCell.RowIndex).Value = cost
                        .Item(ConstUPrice, .CurrentCell.RowIndex).Value = Format(cost, lnumFormat)
                    Else
                        cost = Val(dt(0)("pprice"))
                        .Item(ConstActualPrice, .CurrentCell.RowIndex).Value = cost
                        .Item(ConstUPrice, .CurrentCell.RowIndex).Value = Format(cost, lnumFormat)
                    End If
                    .Item(ConstAdditionalcess, .CurrentCell.RowIndex).Value = addcess
                    'Dim a As Double = .Item(ConstUPrice, .CurrentCell.RowIndex).Value
                    chgbyprg = False
                    calculate(, True)
                End If
            End If
        End With
    End Sub


    Private Sub TabPage1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub grdVoucher_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellEndEdit
        Dim col As Integer = e.ColumnIndex
        Dim ndec1 As Integer
        If col = ConstQty Or col = ConstMRP Or col = ConstUPrice Or col = ConstDisAmt Or col = ConstLTotal Or col = ConstTaxP Or col = ConstDisP Then
            If col = ConstQty Then
                ndec1 = grdVoucher.Item(ConstPFraction, grdVoucher.CurrentRow.Index).Value
            Else
                ndec1 = NoOfDecimal
            End If
            If Val(grdVoucher.Item(col, e.RowIndex).Value) = 0 Then grdVoucher.Item(col, e.RowIndex).Value = 0
            grdVoucher.Item(col, e.RowIndex).Value = Format(Convert.ToDecimal(grdVoucher.Item(col, e.RowIndex).Value), "#,##0" & IIf(ndec1 = 0, "", "." & Strings.StrDup(ndec1, "0")))
        End If
    End Sub


    Private Sub fSerialno_AddSerialNos() Handles fSerialno.AddSerialNos
        Try
            If grdVoucher.CurrentRow Is Nothing Then Exit Sub
            Dim serialCount As Integer
            With fSerialno.grdVoucher
                Dim strSerialno As String = ""
                serialCount = .RowCount
                Dim rowid As Integer
                rowid = grdVoucher.CurrentRow.Index
                If _objcmnbLayer.dtSerialNo.Rows.Count > 0 Then
                    Dim _qurey As EnumerableRowCollection(Of DataRow)
                    _qurey = From data In _objcmnbLayer.dtSerialNo.AsEnumerable() Where data("RowIndex") <> rowid Select data
                    If _qurey.Count > 0 Then
                        _objcmnbLayer.dtSerialNo = _qurey.CopyToDataTable()
                    Else
                        _objcmnbLayer.dtSerialNo.Rows.Clear()
                    End If
                End If
                Dim i As Integer
                For i = 0 To serialCount - 1
                    AddTodtSerialNo(.Item(0, i).Value, Val(grdVoucher.Item(ConstItemID, rowid).Value), rowid, DateValue(grdVoucher.Item(ConstWarrentyExpiry, rowid).Value), Val(grdVoucher.Item(ConstId, rowid).Value))
                    strSerialno = strSerialno & IIf(strSerialno = "", "", ",") & .Item(0, i).Value
                Next
                grdVoucher.Item(ConstSerialNo, rowid).Value = strSerialno
                grdVoucher.Item(ConstQty, rowid).Value = serialCount

            End With
            chgPost = True
            'MsgBox(grdVoucher.Item(ConstItemCode, grdVoucher.RowCount - 1).Value)
            calculate()
            reArrangeNo()
            fSerialno.Close()
            fSerialno = Nothing
            Timer3.Enabled = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
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
        If grdVoucher.ColumnCount = 0 Then Exit Sub
        Timer1.Enabled = False
        If grdVoucher.ColumnCount = 0 Then Exit Sub

        If EnableWarranty = False And Me.Width > 1200 Then resizeGridColumn(grdVoucher, ConstDescr)
        If Me.Width <= 1200 Or grdVoucher.Columns(ConstDescr).Width <= 25 Then
            grdVoucher.Columns(ConstDescr).Width = 150
        End If

        'If Me.Width <= 1200 Then
        '    With grdVoucher
        '        .Columns(ConstDescr).Width = 250
        '        .Columns(ConstBarcode).Visible = False
        '        .Columns(constItmTot).Visible = False
        '        .Columns(ConstTaxAmt).Visible = False
        '    End With
        'Else
        '    With grdVoucher

        '        .Columns(constItmTot).Visible = True
        '        .Columns(ConstTaxAmt).Visible = True
        '    End With
        'End If
        'If EnableWarranty = False Then resizeGridColumn(grdVoucher, ConstDescr)
        txtitemcode.Focus()
    End Sub

    Private Sub SalesInvoice_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Timer1.Enabled = True
        lblcompany.Left = (Me.Width / 2) - (lblcompany.Width / 2)
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

    Private Sub txtfcrt_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtfcrt.Validated
        FCRt = txtfcrt.Text
    End Sub

    Private Sub calculateTaxFromUnitPrice(ByVal i As Integer)
        Dim disablecess As Boolean
        If cessenddate < DateValue(cldrdate.Value) Then
            disablecess = True
        End If
        If chgUprice And chkcal.Checked Then
            chgbyprg = True
            With grdVoucher
                .Item(ConstActualPrice, i).Value = (CDbl(.Item(ConstUPrice, i).Value) * 100) / (CDbl(.Item(ConstTaxP, i).Value) + IIf(disablecess, 0, CDbl(.Item(Constcess, i).Value)) + 100)
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
        If chgbyprg Then Exit Sub
        If chgNumByPgm Then Exit Sub
        chkautoroundOff.Checked = False
        calculate(, , , True)
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
            .Columns("C Avg").Visible = getRight(200, CurrentUser)

            .Columns("Tax Price").Width = 70
            .Columns("Tax Price").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("Tax Price").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Tax Price").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


            .Columns("LPC").Width = 70
            .Columns("LPC").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("LPC").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("LPC").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("LPC").Visible = getRight(200, CurrentUser)

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
            dt = _objcmnbLayer._fldDatatable("SELECT case when Printed='true' then 1 else 0 end p from ItmInvCmnTb where  trtype='IS'  and  InvNo=" & Val(numPrintVchr.Text) & " and  PreFix='" & txtPPrefix.Text & "'")
            If dt.Rows.Count > 0 Then
                If dt(0)(0) = 0 Then
                    PrepareRpt("ISPOS", True)
                End If
            End If
            If frm.chkduplicate.Checked Then
                PrepareRpt("ISPOS", True, 1)
            End If
            If frm.chktriplicate.Checked Then
                PrepareRpt("ISPOS", True, 2)
            End If
        Else
            PrepareRpt("ISPOS", True)
        End If

    End Sub

    Private Sub txtitemcode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtitemcode.GotFocus
        txtitemcode.BackColor = Color.FromArgb(255, 192, 192)
        grdVoucher.ClearSelection()
    End Sub

    Private Sub txtitemcode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtitemcode.KeyDown
        'Dim blanktime As DateTime
        'Dim keytimecount As Integer
        'If keytime = blanktime Then
        '    MsgBox(keytime)
        '    keytimecount = 1
        'End If
        'keytimecount = DateDiff(DateInterval.Minute, keytime, Date.Now)
        'keytime = Date.Now
        'If e.KeyCode <> 13 Then
        '    txtDescr.Text = txtDescr.Text & " " & Date.Now
        'End If
        '49 48 52 56 49 97 8
        lstKey = e.KeyCode
        If e.KeyCode <> Keys.Escape Then
            isShowItems = True
        Else
            isShowItems = False
        End If
        If e.KeyCode = Keys.Return Then

            loadWaite(2)
        ElseIf e.KeyCode = Keys.Up Or e.KeyCode = Keys.PageUp Then
            isShowItems = False
            If Not fMList Is Nothing Then
                If fMList.Visible Then
                    fMList.MoveUp(txtitemcode.Text)
                    txtitemcode.SelectionStart = Len(txtitemcode.Text) + 1
                    'txtitemcode.SelectionLength = Len(txtitemcode.Text)
                    'txtitemcode.SelectAll()
                    Exit Sub
                End If
            End If

        ElseIf e.KeyCode = Keys.Down Or e.KeyCode = Keys.PageDown Then
            isShowItems = False
            If Not fMList Is Nothing Then
                If fMList.Visible Then
                    fMList.MoveDown(txtitemcode.Text)
                    'txtitemcode.SelectionStart = Len(txtitemcode.Text)
                    'txtitemcode.SelectionLength = Len(txtitemcode.Text)
                    Exit Sub
                End If
            End If
        End If
    End Sub
    Private Sub itemCodeKeyEnter()
        isShowItems = False
        If Not fMList Is Nothing Then fMList.Visible = False
        Dim dt As DataTable = Nothing
        Dim itemid As Long
        Dim qty As Double = 1
        Dim removebatchwise As Boolean
        '#qtyItemcode
        '#00001itemcode
        Dim isscannedFromWM As Boolean = False
        If Mid(txtitemcode.Text, 1, 1) = "#" Then
            Dim str As String = Mid(txtitemcode.Text, 7)
            qty = Val(Mid(txtitemcode.Text, 2, 5))
            chgbyprg = True
            txtitemcode.Text = str
            chgbyprg = False
            isscannedFromWM = True
        End If
        If Trim(txtitemcode.Tag & "") = "" Then
            txtitemcode.Tag = txtitemcode.Text
        End If
        Dim qtyqry As String
        Dim skipLoad As Boolean
        If diableNegativeSale Then
            qtyqry = "SELECT isnull(AsOnQty,0)+isnull(opQty,0) AsOnQty,Itemid,Description,isnull(wmcalculation,0)wmcalculation,removebatchwise FROM InvItm left join " & _
                                           "(SELECT SUM(CASE WHEN InvType in ('IN') then 1 else -1 end  * isnull(trqty,0)) AsOnQty ,Itemid TItemid from " & _
                                           "(SELECT InvType,trqty,TrDateNo,Itemid FROM ItmInvTrtb LEFT JOIN VoucherTypeNoTb ON ItmInvTrTb.TrTypeNo=VoucherTypeNoTb.vrno " & _
                                           "LEFT JOIN ItmInvCmnTb ON  ItmInvTrTb.TRID=ItmInvCmnTb.TRID  WHERE isnull(invStatus,0)=0) tr  group by Itemid) tr ON INVITM.Itemid=tr.TItemid " & _
                                           "where [item code]='" & txtitemcode.Tag & "'"
            'ElseIf enableMultipleBarcodeOnItem Then
            '    If chksearchitem.Checked Then
            '        qtyqry = "SELECT InvItm.Itemid,[item code] itemcode, Description,isnull(wmcalculation,0)wmcalculation,isnull(barcode,'')barcode,removebatchwise FROM InvItm " & _
            '             "LEFT JOIN BarcodeMultipleTb ON BarcodeMultipleTb.itemid=InvItm.itemid " & _
            '             "where [item code]='" & txtitemcode.Tag & "' and isnull(isTrid,0)=0"
            '    Else
            '        qtyqry = "SELECT InvItm.Itemid,[item code] itemcode, Description,isnull(wmcalculation,0)wmcalculation,barcode,removebatchwise FROM InvItm " & _
            '            "LEFT JOIN BarcodeMultipleTb ON BarcodeMultipleTb.itemid=InvItm.itemid " & _
            '            "where barcode='" & txtitemcode.Text & "' and isnull(isTrid,0)=0"
            '    End If

            '    dt = _objcmnbLayer._fldDatatable(qtyqry)
            '    If dt.Rows.Count > 0 Then
            '        txtitemcode.Tag = dt(0)("itemcode")
            '        chgbyprg = True
            '        txtitemcode.Text = dt(0)("barcode")
            '        chgbyprg = False
            '        skipLoad = True
            '    End If
        Else
itemload:
            qtyqry = "SELECT Itemid,Description,isnull(wmcalculation,0)wmcalculation,removebatchwise FROM InvItm " & _
                                            "where [item code]='" & txtitemcode.Tag & "'"
            skipLoad = False
        End If
        If skipLoad = False Then dt = _objcmnbLayer._fldDatatable(qtyqry)
        skipLoad = False
        If dt.Rows.Count = 0 Then
            MsgBox("Item not found", MsgBoxStyle.Exclamation)
            chgbyprg = True
            txtitemcode.Text = ""
            chgbyprg = False
            Exit Sub
        Else
            If diableNegativeSale Then
                Dim Gqty As Double = getgridqty(txtitemcode.Tag, grdVoucher.RowCount - 2)
                If (Val(dt(0)("AsOnQty")) - Gqty) <= 0 Then
                    MsgBox("Quantity Exceeds", MsgBoxStyle.Exclamation)
                    chgbyprg = True
                    txtitemcode.Text = ""
                    chgbyprg = False
                    Exit Sub
                End If
            End If
            itemid = dt(0)("Itemid")
            If Not IsDBNull(dt(0)("removebatchwise")) Then
                removebatchwise = dt(0)("removebatchwise")
            End If

            If Val(dt(0)("wmcalculation")) > 0 And isscannedFromWM Then
                qty = qty / Val(dt(0)("wmcalculation"))
            End If
        End If
        chgbyprg = True
        AddRow(, qty)
        chgItm = True
        Valid(grdVoucher.RowCount - 1, 1, "")
        Dim foundbatch As Integer
        Dim ItmFlds() As String
        ReDim ItmFlds(1)
        If enableBatchwiseTr Then
            If Not removebatchwise Then
                dt = _objItmMast.returnBatchItems(itemid, 0)
                If dt.Rows.Count > 1 Then
                    Dim frm As New SelectItemBatchFrm
                    frm.txtname.Tag = itemid
                    'dtProductBatch = dt
                    frm.loadBatch()
                    frm.ShowDialog()
                    foundbatch = Val(frm.btnadd.Tag)
                    ItmFlds = frm.itms
                ElseIf dt.Rows.Count > 0 Then
                    dt = _objItmMast.returnBatchItems(itemid, 0)
                    ReDim ItmFlds(dt.Columns.Count)
                    If dt.Rows.Count > 0 Then
                        For i = 0 To dt.Columns.Count - 1
                            If IsDBNull(dt(0)(i)) Then
                                ItmFlds(i) = ""
                            Else
                                ItmFlds(i) = dt(0)(i)  ' .Columns(i).Text '  
                            End If
                        Next i
                        foundbatch = 1
                    End If

                Else
                    foundbatch = 0
                End If
            Else
                foundbatch = 0
            End If

        End If
        If foundbatch = 1 Then
            chgbyprg = True
            grdVoucher.Item(ConstSerialNo, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(2) IsNot Nothing, ItmFlds(2), "")
            'grdVoucher.Item(ConstBarcode, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(1) IsNot Nothing, ItmFlds(1), "")
            grdVoucher.Item(ConstManufacturingdate, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(7) IsNot Nothing, ItmFlds(7), "")
            grdVoucher.Item(ConstWarrentyExpiry, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(8) IsNot Nothing, ItmFlds(8), "")
            grdVoucher.Item(ConstBatchQty, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(1) IsNot Nothing, ItmFlds(1), "")
            If Val(ItmFlds(3)) = 0 Then ItmFlds(3) = 0
            grdVoucher.Item(ConstMRP, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(3) IsNot Nothing, Format(CDbl(ItmFlds(3)), numFormat), "")
            grdVoucher.Item(ConstBatchCost, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(9) IsNot Nothing, ItmFlds(9), "")
            If chkcredit.Checked Then
                grdVoucher.Item(ConstActualPrice, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(5) IsNot Nothing, ItmFlds(5), "")
            Else
                grdVoucher.Item(ConstActualPrice, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(4) IsNot Nothing, ItmFlds(4), "")
            End If
            If Val(grdVoucher.Item(ConstActualPrice, grdVoucher.CurrentCell.RowIndex).Value) = 0 Then grdVoucher.Item(ConstActualPrice, grdVoucher.CurrentCell.RowIndex).Value = 0
            grdVoucher.Item(ConstUPrice, grdVoucher.CurrentCell.RowIndex).Value = Format(CDbl(grdVoucher.Item(ConstActualPrice, grdVoucher.CurrentCell.RowIndex).Value), numFormat)
            chgbyprg = False
            calculate(, True)
        Else

        End If
        'If enableFocusOnQTYinPOS Then
        '    With grdVoucher
        '        .CurrentCell = .Item(ConstQty, .RowCount - 1)
        '        activecontrolname = "grdVoucher"
        '        .BeginEdit(True)
        '    End With
        'ElseIf enableItemwiseSalesman Then
        '    With grdVoucher
        '        .CurrentCell = .Item(Constsman, .RowCount - 1)
        '        activecontrolname = "grdVoucher"
        '        .BeginEdit(True)
        '    End With
        'Else
        '    txtitemcode.Focus()
        '    activecontrolname = ""
        'End If
        If enableMultiplePointsOnLineItem Then loadRawMertialForPoints(itemid, grdVoucher.Rows.Count - 1, True)
        chgbyprg = True
        txtitemcode.Text = ""
        txtitemcode.Tag = ""
        SrchText = ""
        chgbyprg = False
        'savePos(grdVoucher.RowCount - 1)
    End Sub
    Private Function getgridqty(ByVal itemcode As String, ByVal index As Integer) As Double
        Dim i As Integer
        Dim qty As Double
        With grdVoucher
            For i = 0 To .RowCount - 1
                If .Item(ConstItemCode, i).Value = itemcode And i <> index Then
                    qty = qty + Val(.Item(ConstQty, i).Value)
                End If
            Next
        End With
        Return qty
    End Function

    Private Sub txtitemcode_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtitemcode.Leave
        txtitemcode.BackColor = Color.LightGray
    End Sub

    Private Sub txtitemcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtitemcode.TextChanged
        Timer4.Enabled = False
        Timer4.Enabled = True
    End Sub

    Private Sub fTendered_closeWait() Handles fTendered.closeWait
        If Not fwait Is Nothing Then fwait.Hide()
    End Sub

    Private Sub fTendered_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fTendered.FormClosed
        If Not fTendered Is Nothing Then
            fTendered.Close()
            fTendered = Nothing
        End If
    End Sub


    Private Sub btnhold_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnhold.Click
        loadWaite(4)
        chgPost = True
    End Sub
    Private Sub holdInvoice()
        If MsgBox("Do you want to hold the invoice?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        Dim PPerU As Single
        'With grdVoucher
        '    For i = 0 To .Rows.Count - 1 '- 1
        '        If (CDbl(.Item(ConstQty, i).Value) <> 0 Or .Item(ConstSlNo, i).Value.ToString = "M") And UCase(.Item(ConstqtyChg, i).Value) = "CHG" Then
        '            PPerU = Val(.Item(ConstPMult, i).Value) 'Switch(.TextMatrix(i, 4) = "B", 1, .TextMatrix(i, 4) = "P1", CSng(grdVoucher.TextMatrix(i, 18)), .TextMatrix(i, 4) = "P2", CSng(grdVoucher.TextMatrix(i, 19)))
        '            PPerU = IIf(PPerU = 0, 1, PPerU)
        '            setInvDetValue(loadedTrId, PPerU, i)
        '            _objInv._savePOSDetails(True)
        '        End If
        '    Next
        'End With
        'If _objcmnbLayer.dtSerialNo.Rows.Count > 0 Then
        '    saveSerialNumbers(loadedTrId, _objcmnbLayer.dtSerialNo)
        'End If
        savePos(0)
        AddNewClick()
        getHoldInvoices()
        txtitemcode.Focus()
    End Sub


    Private Sub cmbholdedInvs_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbholdedInvs.SelectedIndexChanged
        If chgbyprg Then Exit Sub
        loadWaite(5)
    End Sub
    Private Sub loadHoldInvoice()
        If cmbholdedInvs.Text = "" Then
            AddNewClick()
            Exit Sub
        End If
        'numVchrNo.Text = cmbholdedInvs.Text
        Dim dtHold As DataTable
        dtHold = _objcmnbLayer._fldDatatable("SELECT trid FROM ItmInvCmnTb WHERE isnull(invStatus,0)=1 and Counter='" & POScounter & "' and tempInvNo=" & Val(cmbholdedInvs.Text))
        Dim tid As Long
        If dtHold.Rows.Count > 0 Then
            tid = dtHold(0)("trid")
            txtprefix.Text = ""
            cmbholdedInvs.Tag = 1
            CheckNLoad(tid)
        End If
        NextNumber()
        numVchrNo.Tag = numVchrNo.Text
        btnupdate.Tag = 1
        txtitemcode.Focus()
    End Sub
    Private Sub getHoldInvoices()
        Dim dtHold As DataTable
        dtHold = _objcmnbLayer._fldDatatable("SELECT INVNO,tempInvNo,trid FROM ItmInvCmnTb WHERE isnull(invStatus,0)=1 and Counter='" & POScounter & "' and trtype='IS'")
        Dim i As Integer
        cmbholdedInvs.Items.Clear()
        cmbholdedInvs.Items.Add("")
        For i = 0 To dtHold.Rows.Count - 1
            cmbholdedInvs.Items.Add(dtHold(i)("tempInvNo"))
        Next
    End Sub

    Private Sub btncard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncard.Click
        'If chkcredit.Checked And enableCreditPrice Then
        '    UpdateClick(True, 2)
        'Else
        '    UpdateClick(True, 1)
        'End If
        posType = 1
        loadWaite(3)
    End Sub

    Private Sub btncredit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncredit.Click
        If enableCreditPrice Then chkcredit.Checked = True
        'UpdateClick(True, 2)
        posType = 2
        loadWaite(3)
    End Sub



    Private Sub fQuickItem_PassData(ByVal ItemCode As String, ByVal ismode As Boolean) Handles fQuickItem.PassData
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT Description,UnitPrice,isnull(mrp,0) MRP FROM INVITM where [Item Code]='" & ItemCode & "'")
        If dt.Rows.Count > 0 Then
            With grdVoucher
                Dim i As Integer = .CurrentRow.Index
                If ismode Then
                    .Item(ConstActualPrice, i).Value = dt(0)("UnitPrice")
                    .Item(ConstUPrice, i).Value = Format(CDbl(dt(0)("UnitPrice")), numFormat)
                    .Item(ConstMRP, i).Value = dt(0)("Mrp")
                End If
                Dim _qurey As EnumerableRowCollection(Of DataRow)
                Dim itemid As Long = Val(.Item(ConstItemID, i).Value)
                _qurey = (From data In dtItemInfo.AsEnumerable() Where data("itemid") <> itemid Select data)
                If _qurey.Count > 0 Then
                    dtItemInfo = _qurey.CopyToDataTable()
                Else
                    dtItemInfo = dtItemInfo.Clone
                End If
                getItemInfo(Val(.Item(ConstItemID, i).Value))
            End With
        End If
        calculate(, True)
    End Sub

    Private Sub grdVoucher_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellEnter
        'If e.ColumnIndex = ConstMRP And enableBatchwiseTr Then
        '    _srchTxtId = 3
        '    _srchIndexId = 5
        '    _srchOnce = False
        '    strGridSrchString = ""
        '    chgbyprg = True
        '    If Val(grdVoucher.Item(ConstMRP, grdVoucher.CurrentRow.Index).Value) <> 0 Then
        '        strGridSrchString = grdVoucher.Item(ConstMRP, grdVoucher.CurrentRow.Index).Value
        '    Else
        '        strGridSrchString = ""
        '    End If
        '    ShowPanel(True)
        '    chgbyprg = False
        'End If

        grdBeginEdit()
    End Sub

    Private Sub btnreturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreturn.Click
        fSR = New SalesReturnInvoice
        With fSR
            .Width = 1201
            .Height = 532
            .WindowState = FormWindowState.Normal
            .StartPosition = FormStartPosition.CenterScreen
            .isAddRow = True
            .isfromPos = True
            .txtReference.Text = "CR"
            .btnlocqty.Visible = False
            .btnmultipleDebit.Visible = False
            '.Panel4.Visible = True
            .ShowDialog()
            txtitemcode.Focus()
        End With
    End Sub

    Private Sub fSR_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fSR.FormClosed
        fSR = Nothing
    End Sub

    Private Sub txtroundOff_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtroundOff.TextChanged
        If chgNumByPgm Then Exit Sub
        chkautoroundOff.Checked = False
        calculate(, , , True)
    End Sub

    Private Sub chkautoroundOff_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkautoroundOff.CheckedChanged
        If chgbyprg Then Exit Sub
        calculate()
    End Sub

    Private Sub txtphone_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtphone.TextChanged, txtCashCustomer.TextChanged, txtgiftvoucher.TextChanged
        If chgbyprg = True Then Exit Sub
        Dim myCtrl As Control = sender
        chgsearch = True
        If rdocashcustomer.Checked Then
            Select Case myCtrl.Name
                Case "txtphone"
                    _srchTxtId = 5
                Case "txtCashCustomer"
                    _srchTxtId = 6
                Case "txtgiftvoucher"
                    _srchTxtId = 7
            End Select
        Else
            Select Case myCtrl.Name
                Case "txtphone"
                    _srchTxtId = 1
                Case "txtCashCustomer"
                    _srchTxtId = 2
                Case "txtgiftvoucher"
                    _srchTxtId = 7
            End Select
        End If

        _srchOnce = False
        ShowFmlist(sender)
        'btnUpdate.Enabled = Val(btnUpdate.Tag) > 0
        chgPost = True
    End Sub

    Private Sub txtphone_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtphone.Validated, txtCashCustomer.Validated
        phoneValidate()
    End Sub
    Private Sub phoneValidate(Optional ByVal giftno As String = "")
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
        Dim dt As DataTable
        If Not chgsearch Then Exit Sub
        chgbyprg = True
        If rdocashcustomer.Checked Then
            If txtphone.Text = "" Then
                If txtCashCustomer.Text <> "" Then
                    dt = _objcmnbLayer._fldDatatable("Select custid,CustName,Add1,Add2,Phone,email,salesPoints,GiftVrNo,Memberid,isnull(cnt,0)cnt " & _
                                            "from CashCustomerTb " & _
                                            "left join (select count(trid) cnt,CashCustid from ItmInvCmnTb where isnull(invStatus,0)=0 group by CashCustid) tr on tr.CashCustid=CashCustomerTb.custid " & _
                                            " where CustName='" & txtCashCustomer.Text & "'")
                ElseIf giftno <> "" Then
                    dt = _objcmnbLayer._fldDatatable("Select custid,CustName,Add1,Add2,Phone,email,salesPoints,GiftVrNo,Memberid,isnull(cnt,0)cnt " & _
                                                 "from CashCustomerTb " & _
                                                 "left join (select count(trid) cnt,CashCustid from ItmInvCmnTb where isnull(invStatus,0)=0 group by CashCustid) tr on tr.CashCustid=CashCustomerTb.custid " & _
                                                 "where GiftVrNo='" & giftno & "'")
                Else
                    dt = _objcmnbLayer._fldDatatable("Select custid,CustName,Add1,Add2,Phone,email,salesPoints,GiftVrNo,Memberid,isnull(cnt,0)cnt " & _
                                            "from CashCustomerTb " & _
                                            "left join (select count(trid) cnt,CashCustid from ItmInvCmnTb where isnull(invStatus,0)=0 group by CashCustid) tr on tr.CashCustid=CashCustomerTb.custid " & _
                                            "where custid=" & Val(txtphone.Tag))
                End If
            Else
                dt = _objcmnbLayer._fldDatatable("Select custid,CustName,Add1,Add2,Phone,email,salesPoints,GiftVrNo,Memberid,isnull(cnt,0)cnt " & _
                                             "from CashCustomerTb " & _
                                             "left join (select count(trid) cnt,CashCustid from ItmInvCmnTb where isnull(invStatus,0)=0 group by CashCustid) tr on tr.CashCustid=CashCustomerTb.custid " & _
                                             "where Phone='" & txtphone.Text & "'")
            End If
            If dt.Rows.Count > 0 Then
                txtphone.Text = dt(0)("Phone")
                txtCashCustomer.Text = dt(0)("CustName")
                txtcustAddress.Text = dt(0)("Add1")
                txtcustemail.Text = dt(0)("email")
                txtgiftvoucher.Text = Trim(dt(0)("GiftVrNo") & "")
                txtphone.Tag = dt(0)("custid")
                lblinvcount.Text = "Invoices : " & dt(0)("cnt")
                lblinvcount.Visible = True
                getGiftCardPoints()
            Else
                txtgiftvoucher.Text = ""
                lblinvcount.Text = "Invoices : 0"
                lblearned.Text = ""
                lblredeemed.Text = ""
                lblpointbalance.Text = ""
                lblpointValue.Text = ""

                fCashCust = New CreateCashCustomerFrm
                fCashCust.isaddnew = True
                fCashCust.txtphone.Text = txtphone.Text
                fCashCust.ShowDialog()
                fCashCust = Nothing
                txtitemcode.Focus()
            End If
        Else
            setCustomer()
        End If
        chgbyprg = False
        chgsearch = False

    End Sub
    Private Sub getGiftCardPoints()
        Dim dt As DataTable
        Dim dtset As DataSet
        Dim str As String
        If txtgiftvoucher.Text = "" Then Exit Sub
        str = "select count(trid) cnt,sum(NetAmt) TotalSales," & _
                                         "sum(isnull(discountPointsCollected,0))discountPointsCollected,sum(isnull(redeemPoints,0))redeemPoints,giftno " & _
                                         "from ItmInvCmnTb where isnull(invStatus,0)=0 and giftno='" & txtgiftvoucher.Text & "' group by giftno"
        str = str & " Select giftvoucherSalesValue,giftvoucherPointPerValue,giftvoucherPointValue from CompanyDefaultSettingsTb"
        dtset = _objcmnbLayer._ldDataset(str, False)
        dt = dtset.Tables(1)
        If dt.Rows.Count > 0 Then
            giftvoucherSalesValue = Val(dt(0)("giftvoucherSalesValue") & "")
            giftvoucherPointPerValue = Val(dt(0)("giftvoucherPointPerValue") & "")
            giftvoucherPointValue = Val(dt(0)("giftvoucherPointValue") & "")
        End If
        dt = Nothing
        dt = dtset.Tables(0)
        If dt.Rows.Count > 0 Then
            lblinvcount.Text = "Invoices : " & Val(dt(0)("cnt") & "") & " [ " & Format(Val(dt(0)("TotalSales") & ""), numFormat) & " ]"
            lblearned.Text = Val(dt(0)("discountPointsCollected") & "")
            lblredeemed.Text = Val(dt(0)("redeemPoints") & "")
            lblpointbalance.Text = Val(dt(0)("discountPointsCollected") & "") - Val(dt(0)("redeemPoints") & "")
            lblpointValue.Text = Format(Val(lblpointbalance.Text) * giftvoucherPointValue, numFormat)
        End If
        lblpointbalance.Tag = ""
    End Sub

    Private Sub txtcustemail_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtcustemail.KeyDown
        If e.KeyCode = Keys.Return Then
            btnupdate.Focus()
        End If

    End Sub
    Private Sub savecashcustomer()
        If txtCashCustomer.Text <> "" Then
            If Val(txtphone.Tag) > 0 Then
                _objcmnbLayer._saveDatawithOutParm("update CashCustomerTb set CustName='" & txtCashCustomer.Text & "',Add1='" & _
                                                   txtcustAddress.Text & "',Phone='" & txtphone.Text & "',email='" & txtcustemail.Text & "' where custid=" & Val(txtphone.Tag))
            Else
                _objcmnbLayer._saveDatawithOutParm("Insert into CashCustomerTb(CustName,Add1,Phone,email) values('" & _
                                                   txtCashCustomer.Text & "','" & txtcustAddress.Text & "','" & txtphone.Text & "','" & txtcustemail.Text & "')")
                Dim dt As DataTable
                dt = _objcmnbLayer._fldDatatable("Select custid,CustName,Add1,Add2,Phone,email,salesPoints,GiftVrNo,Memberid " & _
                                             "from CashCustomerTb where Phone='" & txtphone.Text & "'")
                If dt.Rows.Count > 0 Then
                    txtphone.Tag = dt(0)("custid")
                End If
            End If
        End If

    End Sub


    Private Sub chkcredit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkcredit.CheckedChanged
        setCreditOrCashPrice()
        Dim PreFixTb As DataTable
        If chkcredit.Checked And enableCreditPrice Then
            PreFixTb = _objcmnbLayer._fldDatatable("SELECT  [VOUCHER NAME] FROM PreFixTb WHERE CTGRY=3 and vrtypeno=4", False)
            If PreFixTb.Rows.Count > 0 Then
                cmbVoucherTp.Text = PreFixTb(0)(0)
            End If
        Else
            PreFixTb = _objcmnbLayer._fldDatatable("SELECT  [VOUCHER NAME] FROM PreFixTb WHERE CTGRY=1 and vrtypeno=4", False)
            If PreFixTb.Rows.Count > 0 Then
                cmbVoucherTp.Text = PreFixTb(0)(0)
            End If
        End If

    End Sub

    Private Sub chkSearch_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSearch.CheckedChanged
        txtitemcode.Focus()
    End Sub

    Private Sub rdocashcustomer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdocashcustomer.Click, rdocreditcustomer.Click
        btnaddcustomer.Visible = rdocashcustomer.Checked
        Label32.Visible = rdocreditcustomer.Checked
        lblbalance.Visible = rdocreditcustomer.Checked
        lblinvcount.Text = ""
        lblinvcount.Visible = True
        txtphone.Focus()
        chgbyprg = True
        txtCashCustomer.Text = ""
        txtphone.Text = ""
        txtphone.Tag = ""
        txtcustAddress.Text = ""
        txtcustemail.Text = ""
        lblbalance.Text = "0.00"
        txtgiftvoucher.Text = ""
        chgbyprg = False
    End Sub

    Private Sub btnaddcustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddcustomer.Click
        fCashCust = New CreateCashCustomerFrm
        'fCashCust.isaddnew = True
        fCashCust.ShowDialog()
        fCashCust = Nothing
        txtitemcode.Focus()
    End Sub
    Private Sub fCashCust_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fCashCust.FormClosed
        fCashCust = Nothing
    End Sub

    Private Sub fCashCust_selectcust(ByVal custname As String, ByVal custaddress As String, ByVal Cashcustid As Long, ByVal cardnumber As String, ByVal phone As String, ByVal gstn As String) Handles fCashCust.selectcust
        chgbyprg = True
        txtCashCustomer.Text = custname
        txtcustAddress.Text = custaddress
        txtgiftvoucher.Text = cardnumber
        txtphone.Tag = Cashcustid
        txtphone.Text = phone
        chgbyprg = False
    End Sub


    Private Sub fTendered_updatePos(ByVal cashAcc As Long, ByVal cashAmt As Double, ByVal cardAcc As Long, ByVal cardAmt As Double, _
                                    ByVal cardnumber As String, ByVal creditAcc As Long, ByVal creditAmt As Double, _
                                    ByVal customername As String, ByVal tendAmt As Double, ByVal chgBalanceAmt As Double, _
                                    ByVal customerphone As String, ByVal srno As String, ByVal sramt As Double, ByVal vname As String) Handles fTendered.updatePos
        LcashAcc = cashAcc
        LcashAmt = cashAmt
        LcardAcc = cardAcc
        LcardAmt = cardAmt
        Lcardnumber = cardnumber
        LcreditAcc = creditAcc
        LcreditAmt = creditAmt
        LtendAmt = tendAmt
        LchangeAmt = chgBalanceAmt
        strCustomername = customername
        txtSuppAlias.Tag = cashAcc
        txtsrno.Text = srno
        txtsramt.Text = Format(sramt, lnumFormat)
        cmbVoucherTp.Text = vname
        If customerphone <> "" Then
            chgbyprg = True
            txtphone.Text = customerphone
            chgbyprg = False
        End If
        chgPost = True
        UpdateClick()
        LcashAcc = 0
        LcashAmt = 0
        LcardAcc = 0
        LcardAmt = 0
        Lcardnumber = 0
        LcreditAcc = 0
        LcreditAmt = 0
    End Sub
    Private Sub saveMultipleDebits(ByVal trid As Long)
        _objcmnbLayer._saveDatawithOutParm("DELETE FROM SalesMultipleDebitsTb " & _
                                                 " WHERE setremove=1 AND dbtrid=" & trid)

        Dim query As String = ""
        Dim nofs As Integer
        If LcashAmt > 0 Then nofs = 1
        If LcardAmt > 0 Then nofs = nofs + 1
        If LcreditAmt > 0 Then nofs = nofs + 1
        If nofs = 1 Then GoTo ext
        Dim ref As String
        ref = txtprefix.Text & IIf(txtprefix.Text = "", "", "/") & numVchrNo.Text
        If txtReference.Text <> "" Then
            ref = txtReference.Text
        End If
        If LcashAcc > 0 And LcashAmt > 0 Then
            query = "INSERT INTO SalesMultipleDebitsTb (dbtrid,dbaccid,accAmt,reference) VALUES" & _
                                                   "(" & trid & "," & LcashAcc & "," & LcashAmt & ",'" & Trim(ref & "") & "')"
        End If
        If LcardAcc > 0 And LcardAmt > 0 Then
            query = query & "  INSERT INTO SalesMultipleDebitsTb (dbtrid,dbaccid,accAmt,reference) VALUES" & _
                                                      "(" & trid & "," & LcardAcc & "," & LcardAmt & ",'" & Trim(ref & "") & "')"
        End If

        If LcreditAcc > 0 And LcreditAmt > 0 Then
            query = query & "  INSERT INTO SalesMultipleDebitsTb (dbtrid,dbaccid,accAmt,reference) VALUES" & _
                                                    "(" & trid & "," & LcreditAcc & "," & LcreditAmt & ",'" & Trim(ref & "") & "')"
        End If
        If query <> "" Then
            _objcmnbLayer._saveDatawithOutParm(query)
        End If

ext:
    End Sub

    Private Sub chktaxInv_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chktaxInv.CheckedChanged
        If chgbyprg Then Exit Sub
        calculate()
    End Sub

    Private Sub txtitemcode_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtitemcode.Validated
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
    End Sub

    Private Sub btnminimize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnminimize.Click
        btnminimize.Cursor = Cursors.WaitCursor
        Me.WindowState = FormWindowState.Minimized
        btnminimize.Cursor = Cursors.Arrow
    End Sub

    Private Sub Timer2_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Timer2.Enabled = False
        'If enableShift Then
        '    If shiftId = 0 Then
        '        Dim dt As DataTable
        '        dt = _objcmnbLayer._fldDatatable("select isnull(shiftId,0)shiftId from UserTb")
        '        If dt.Rows.Count > 0 Then
        '            shiftId = dt(0)("shiftId")
        '        End If
        '    End If
        '    If shiftid = 0 Then
        '        If Not openShift() Then
        '            Me.Close()
        '            Exit Sub
        '        End If
        '    End If

        'End If

        loadWaite(0)
    End Sub

    Private Sub fMList_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fMList.FormClosed
        fMList = Nothing
    End Sub

    Private Sub btnbarcode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnbarcode.Click
        'Dim frm As New BarcodeTrackingFrm
        'frm.ShowDialog()
        'frm = Nothing
    End Sub

    Private Sub Label30_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label30.Click

    End Sub

    Private Sub btnshift_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnshift.Click
        If Not openShift() Then Me.Close()
    End Sub
    Private Function openShift() As Boolean
        'Dim frm As New POSShiftFrm
        'If shiftid = 0 Then
        '    Dim dt As DataTable
        '    dt = _objcmnbLayer._fldDatatable("select UserTb.shiftId from UserTb " & _
        '                                     "left join PosShiftTb on PosShiftTb.shiftid=UserTb.shiftid where convert(date,shdatetime,103)='" & Format(DateValue(Date.Now), "yyyy/MM/dd") & "'")
        '    If dt.Rows.Count > 0 Then
        '        shiftid = dt(0)("shiftId")
        '    End If
        'End If
        'With frm
        '    .companyid = 1
        '    .branchid = 0
        '    .dtpopen.Tag = shiftid
        '    .counter = POScounter
        '    .ShowDialog()
        '    'If Val(.btnOK.Tag & "") = 0 Then
        '    '    'lblloginDetails.Text = "Logged User : " & CurrentUser & " Counter : " & POScounter & " Shift : [ " & .txtshift.Text & " ] " & .dtpopen.Value
        '    '    shiftId = Val(.dtpopen.Tag)
        '    'ElseIf Val(.btnOK.Tag & "") = 1 Then
        '    '    'lblloginDetails.Text = "Logged User : " & CurrentUser & " Counter : " & POScounter & " Shift : "
        '    '    shiftId = 0
        '    'End If
        '    If Val(.btnOK.Tag & "") = 1 Then
        '        Return False
        '    End If
        'End With
        'Return True
    End Function

    Private Sub chkcal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkcal.CheckedChanged

    End Sub

    Private Sub chkcal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkcal.Click
        _objcmnbLayer._saveDatawithOutParm("Update CompanyTb set calcluatetaxFrompriceInv =" & IIf(chkcal.Checked, 1, 0))
        calcluatetaxFrompriceInv = chkcal.Checked
    End Sub

    Private Sub chkrawmaterial_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkrawmaterial.Click
        tbrawMaterial.Visible = chkrawmaterial.Checked
        resizeGridColumn(grdrawmaterials, ConstDescr)
        Dim rowid As Integer
        If grdVoucher.CurrentRow Is Nothing Then
            rowid = 0
        Else
            rowid = grdVoucher.CurrentRow.Index
        End If
        getRawMaterials(rowid)
    End Sub
    Private Sub setRawmaterialQty(ByVal rIndex As Integer)
        Dim i As Integer
        Dim totalcost As Double
        Dim cost As Double
        With grdVoucher
            For i = 0 To dtRawMaterial.Rows.Count - 1
                If dtRawMaterial(i)("slno") = rIndex Then
                    dtRawMaterial(i)("qty") = dtRawMaterial(i)("Rqty") * CDbl(.Item(ConstQty, rIndex).Value)
                    cost = dtRawMaterial(i)("qty") * dtRawMaterial(i)("unitprice")
                    totalcost = totalcost + cost
                End If
            Next
        End With
        If tbrawMaterial.Visible Then getRawMaterials(rIndex)
    End Sub
    Private Sub DeleteRawmaterial(ByVal cindex As Integer, ByVal donotsetIndex As Boolean)
        Dim iterateIndex As Integer = 0
        Dim newDataTable As DataTable = dtRawMaterial.Copy
        For Each row As DataRow In newDataTable.Rows
            If row("slno") = cindex Then
                'row.Delete()
                dtRawMaterial.Rows.RemoveAt(iterateIndex)
            Else
                iterateIndex = iterateIndex + 1
            End If
        Next
        If Not donotsetIndex Then
            Dim i As Integer
            For i = 0 To dtRawMaterial.Rows.Count - 1
                If dtRawMaterial(i)("slno") > cindex Then
                    dtRawMaterial(i)("slno") = dtRawMaterial(i)("slno") - 1
                End If
            Next
        End If

    End Sub
    Private Sub setRawmaterial(ByVal PreitemId As Long, ByVal rowid As Integer)
        Dim dt As DataTable
        DeleteRawmaterial(rowid, True)
        Dim _objItmMast As clsItemMast_BL = New clsItemMast_BL
        dt = _objItmMast.returnRawMaterial(Val(PreitemId))
        Dim totalcost As Double
        Dim cost As Double
        Dim dtrow As DataRow
        For i = 0 To dt.Rows.Count - 1
            dtrow = dtRawMaterial.NewRow
            dtrow("slno") = rowid
            dtrow("itemcode") = dt(i)("Item Code")
            dtrow("itemname") = dt(i)("Description")
            dtrow("unit") = dt(i)("Unit")
            dtrow("Rqty") = dt(i)("Rawqty")
            dtrow("qty") = dt(i)("Rawqty")
            dtrow("unitprice") = dt(i)("CostAvg")
            dtrow("PFraction") = dt(i)("FraCount")
            dtrow("itemid") = dt(i)("Ritemid")
            cost = dt(i)("Rawqty") * dt(i)("CostAvg")
            totalcost = totalcost + cost
            dtRawMaterial.Rows.Add(dtrow)
        Next
        If tbrawMaterial.Visible Then getRawMaterials(rowid)
        grdVoucher.Item(ConstBatchCost, rowid).Value = totalcost
    End Sub
    Private Sub getRawMaterials(ByVal rowid As Integer)
        Dim dt As DataTable
        Dim totalcost As Double
        Dim cost As Double
        If dtRawMaterial.Rows.Count = 0 Then dt = dtRawMaterial.Clone : GoTo nxt
        Dim _qurey As EnumerableRowCollection(Of DataRow)
        _qurey = From data In dtRawMaterial.AsEnumerable() Where data("slno") = rowid Select data
        If _qurey.Count > 0 Then
            dt = _qurey.CopyToDataTable()
        Else
            dt = dtRawMaterial.Clone
        End If
        grdrawmaterials.Rows.Clear()
        Dim qty As Double
        lblrawcost.Text = Format(0, numFormat)
        qty = CDbl(grdVoucher.Item(ConstQty, rowid).Value)
        For i = 0 To dt.Rows.Count - 1
            With grdrawmaterials
                .Rows.Add()
                .Item(ConstSlNo, i).Value = i + 1
                .Item(ConstItemCode, i).Value = dt(i)("itemcode")
                .Item(ConstDescr, i).Value = dt(i)("itemname")
                .Item(ConstUnit, i).Value = dt(i)("Unit")
                .Item(ConstUPrice, i).Value = Format(dt(i)("unitprice"), numFormat)
                .Item(ConstActualPrice, i).Value = dt(i)("unitprice")
                .Item(ConstPFraction, i).Value = dt(i)("PFraction")
                If Val(.Item(ConstPFraction, i).Value & "") = 0 Then .Item(ConstPFraction, i).Value = 0
                .Item(ConstQty, i).Value = dt(i)("qty")
                If Val(grdVoucher.Item(ConstQty, rowid).Value) > 0 Then
                    .Item(constItmTot, i).Value = dt(i)("qty") / CDbl(grdVoucher.Item(ConstQty, rowid).Value)
                Else
                    .Item(constItmTot, i).Value = dt(i)("Rqty")
                End If
                .Item(ConstLTotal, i).Value = Format(dt(i)("qty") * dt(i)("unitprice"), numFormat)
                .Item(ConstQty, i).Value = Format(CDbl(.Item(ConstQty, i).Value), "#,##0" & _
                                                  IIf(Val(.Item(ConstPFraction, i).Value & "") = 0, "", "." & Strings.StrDup(CByte(.Item(ConstPFraction, i).Value), "0"))) ' 
                .Item(ConstItemID, i).Value = dt(i)("itemid")
                cost = .Item(ConstQty, i).Value * .Item(ConstUPrice, i).Value
                totalcost = totalcost + cost
            End With
        Next
        lblrawcost.Text = Format(totalcost, numFormat)
        Exit Sub
nxt:
        grdrawmaterials.Rows.Clear()
    End Sub
    Private Sub SetGridHeadRawMaterials()
        chgbyprg = True
        SetGridHeadEntryProperty(grdrawmaterials)
        With grdrawmaterials
            .Columns(Constsman).Visible = False
            .Columns(ConstMeterCode).Visible = False
            .Columns(ConstStartReading).Visible = False
            .Columns(ConstEndReading).Visible = False
            .Columns(ConstWarrentyExpiry).Visible = False
            .Columns(ConstManufacturingdate).Visible = False
            .Columns(ConstTotalSales).Visible = False
            .Columns(ConstWoodQty).Visible = False
            .Columns(ConstWoodDiscQty).Visible = False
            .Columns(ConstBarcode).Visible = False
            .Columns(ConstTaxP).Visible = False
            .Columns(ConstTaxAmt).Visible = False
            .Columns(ConstDisAmt).Visible = False
            .Columns(ConstDisP).Visible = False
            .Columns(ConstSerialNo).Visible = False
            .Columns(ConstMeterCode).Visible = False
            '.Columns(constItmTot).Visible = False
            .Columns(constItmTot).HeaderText = "Basic Qty"

        End With
        chgbyprg = False
    End Sub

    Private Sub grdVoucher_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellContentClick

    End Sub

    Private Sub btnRMOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRMOk.Click
        tbrawMaterial.Visible = False
        chkrawmaterial.Checked = False
    End Sub

    Private Sub Timer3_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        Timer3.Enabled = False
        FindNextCell(grdVoucher, grdVoucher.CurrentRow.Index, grdVoucher.CurrentCell.ColumnIndex + 1)
        grdBeginEdit()
    End Sub

    Private Sub grdVoucher_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.CurrentCellDirtyStateChanged

    End Sub

    Private Sub Timer4_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer4.Tick
        'If Val(Timer4.Tag) = 0 Then Exit Sub
        Timer4.Enabled = False
        If Not isShowItems Then Exit Sub
        If chgbyprg = True Then Exit Sub
        _srchTxtId = 4
        _srchOnce = False
        ShowFmlist(txtitemcode, True)
        chgPost = True
        activecontrolname = ""
        isShowItems = False
    End Sub

    Private Sub txtgiftvoucher_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtgiftvoucher.Validated
        If rdocashcustomer.Checked Then
            phoneValidate(txtgiftvoucher.Text)
        Else
            setCustomer(, txtgiftvoucher.Text)
        End If
    End Sub

    Private Sub btnredeem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnredeem.Click
        If btnredeem.Text = "Undo Redeem" Then
            If MsgBox("Do you want to Undo redeem?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.No Then Exit Sub
            numDisc.Text = Format(0, numFormat)
            numDisc.ReadOnly = False
            txtdp.ReadOnly = False
            btnredeem.Text = "Redeem Points"
        Else
            If MsgBox("Do you want to redeem points?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.No Then Exit Sub
            btnredeem.Text = "Undo Redeem"
            lblpointbalance.Tag = Val(lblpointbalance.Text)
            If Val(lblpointValue.Text) > 0 Then
                numDisc.Text = Format(CDbl(lblpointValue.Text), numFormat)
                numDisc.ReadOnly = True
                txtdp.ReadOnly = True
            Else
                numDisc.Text = Format(0, numFormat)
                numDisc.ReadOnly = False
                txtdp.ReadOnly = False
            End If
        End If

        calculate(, True, True)
    End Sub
    Private Sub loadlastInvoicenumber()
        Dim dt As DataTable = _objcmnbLayer._fldDatatable("SELECT prefix,invno FROM ItmInvCmnTb WHERE trid= " & _
                                                          "(select max(trid) from ItmInvCmnTb where  trtype='IS')")

        If dt.Rows.Count > 0 Then
            txtPPrefix.Text = Trim(dt(0)("Prefix") & "")
            numPrintVchr.Text = Trim(dt(0)("invno") & "")
        End If

    End Sub

    'Private Sub fSR_returnSR(ByVal invno As String, ByVal amt As Double) Handles fSR.returnSR
    '    txtsrno.Text = invno
    '    txtsramt.Text = Format(amt, lnumFormat)
    'End Sub

    Private Sub chkmrp_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkmrp.CheckedChanged

    End Sub

    Private Sub chkmrp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkmrp.Click
        _objcmnbLayer._saveDatawithOutParm("update companytb set disableMRPChecking=" & IIf(chkmrp.Checked, 1, 0))
        'disableMRPChecking = chkmrp.Checked
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
        Dim slno As Integer
        With grdVoucher
            For i = 0 To .Rows.Count - 1
                If .Item(ConstSlNo, i).Value.ToString <> "M" And Val(.Item(ConstItemID, i).Value) = 0 Then GoTo skip
                PPerU = Val(.Item(ConstPMult, i).Value) 'Switch(.TextMatrix(i, 4) = "B", 1, .TextMatrix(i, 4) = "P1", CSng(grdVoucher.TextMatrix(i, 18)), .TextMatrix(i, 4) = "P2", CSng(grdVoucher.TextMatrix(i, 19)))
                PPerU = IIf(PPerU = 0, 1, PPerU)

                dtrow = dtInvTb.NewRow
                dtrow("TrId") = Invid
                dtrow("ItemId") = IIf(.Item(ConstSlNo, i).Value.ToString <> "M", Val(.Item(ConstItemID, i).Value), 0)
                dtrow("Unit") = .Item(ConstUnit, i).Value
                dtrow("TrQty") = CDbl(.Item(ConstQty, i).Value) * PPerU
                dtrow("Focqty") = CDbl(.Item(ConstFocQty, i).Value) * PPerU
                dtrow("UnitCost") = CDbl(.Item(ConstActualPrice, i).Value) * FCRt / PPerU 'CDbl(.TextMatrix(i, 10)) / PPerU

                TDrAmt = TDrAmt + CDbl(.Item(ConstQty, i).Value) * (CDbl(.Item(ConstActualPrice, i).Value) - IIf(DiscAcc = 0, CDbl(.Item(ConstDiscOther, i).Value), 0))
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
                slno = slno + 1
                dtrow("SlNo") = slno
                dtrow("TrTypeNo") = TrTypeNo ' getVouchernumber("IS")
                dtrow("TrDateNo") = getDateNo(CDate(cldrdate.Value))
                dtrow("id") = Val(.Item(ConstId, i).Value)
                'dtrow("WarrentyName") = .Item(ConstLocation, i).Value
                dtrow("SerialNo") = Trim(.Item(ConstSerialNo, i).Value & "")
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
                If Val(.Item(ConstWoodQty, i).Value) = 0 Then .Item(ConstWoodQty, i).Value = 0
                dtrow("WoodNetQty") = CDbl(.Item(ConstWoodQty, i).Value)
                If chktaxInv.Checked Then
                    If Val(.Item(ConstCGSTAmt, i).Value & "") = 0 Then .Item(ConstCGSTAmt, i).Value = 0
                    If Val(.Item(ConstSGSTAmt, i).Value & "") = 0 Then .Item(ConstSGSTAmt, i).Value = 0
                    If Val(.Item(ConstIGSTAmt, i).Value & "") = 0 Then .Item(ConstIGSTAmt, i).Value = 0
                    If Val(.Item(ConstcessAmt, i).Value & "") = 0 Then .Item(ConstcessAmt, i).Value = 0
                    If Val(.Item(ConstregularCessAmt, i).Value & "") = 0 Then .Item(ConstregularCessAmt, i).Value = 0
                    If Val(.Item(ConstAdditionalcess, i).Value & "") = 0 Then .Item(ConstAdditionalcess, i).Value = 0
                Else
                    .Item(ConstCGSTAmt, i).Value = 0
                    .Item(ConstSGSTAmt, i).Value = 0
                    .Item(ConstIGSTAmt, i).Value = 0
                    .Item(ConstregularCessAmt, i).Value = 0
                    .Item(ConstFloodCessAmt, i).Value = 0
                    .Item(ConstAdditionalcess, i).Value = 0
                    .Item(ConstcessAmt, i).Value = 0
                End If
                dtrow("IGSTAmt") = CDbl(.Item(ConstIGSTAmt, i).Value) * FCRt
                dtrow("CGSTAMT") = CDbl(.Item(ConstCGSTAmt, i).Value) * FCRt
                dtrow("SGSTAmt") = CDbl(.Item(ConstSGSTAmt, i).Value) * FCRt
                dtrow("regularcessAmt") = (CDbl(.Item(ConstregularCessAmt, i).Value) * FCRt)
                'If Val(lblgstn.Tag & "") = 0 Then
                '    dtrow("FloodcessAmt") = (CDbl(.Item(ConstFloodCessAmt, i).Value) * FCRt)
                'Else
                '    dtrow("FloodcessAmt") = 0
                'End If

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
                If Val(.Item(ConstBatchCost, i).Value & "") = 0 Then .Item(ConstBatchCost, i).Value = 0
                dtrow("CostAvg") = CDbl(.Item(ConstBatchCost, i).Value)

                If Val(dtrow("ItemId")) = 0 Then
                    dtrow("TrQty") = 1
                    dtrow("UnitCost") = 0
                    dtrow("taxP") = 0
                    dtrow("taxAmt") = 0
                    dtrow("UnitDiscount") = 0
                End If
                dtInvTb.Rows.Add(dtrow)
                If Val(.Item(ConstImpJobChildTbID, i).Value) > 0 Then
                    ImpJobChildTbIDs = ImpJobChildTbIDs & IIf(ImpJobChildTbIDs = "", "", ",") & Val(.Item(ConstImpJobChildTbID, i).Value)
                End If
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
skip:
            Next
        End With
        _objInv.savebulktoInvTr(dtInvTb)
        Return TDrAmt
    End Function

    Private Sub fwait_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fwait.FormClosed
        fwait = Nothing
    End Sub

    Private Sub fwait_triggerEvent() Handles fwait.triggerEvent
        Select Case fwait.triggerType
            Case 0
                'If _vInvItmtable.Rows.Count = 0 Or isnewItemcreated Then
                '    SearchProductPanel(grdVoucher, "Item Code", "", True, , , , True)
                'End If
                frmload()
            Case 1

                'Me.Cursor = Cursors.WaitCursor
                saveTrans()
                'If chkcredit.Checked And enableCreditPrice Then
                '    UpdateClick(True, 2)
                'Else
                '    UpdateClick(True)
                'End If

                'Me.Cursor = Cursors.Default
            Case 2
                itemCodeKeyEnter()

            Case 3
                'showTendered()
                UpdateClick(True)
                txtitemcode.Focus()
            Case 4
                holdInvoice()
            Case 5
                loadHoldInvoice()
        End Select
        If Not fwait Is Nothing Then fwait.Close() : fwait = Nothing
    End Sub
    Private Sub loadWaite(ByVal triggerType As Integer)

        If Not fwait Is Nothing Then fwait.Close() : fwait = Nothing
        fwait = New WaitMessageFrm
        With fwait
            .triggerType = triggerType
            '.Visible = True
            .ShowDialog()
        End With
        If triggerType = 2 Then
            Timer5.Enabled = True
        Else
            txtitemcode.Focus()
        End If

    End Sub

    Private Sub Timer5_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer5.Tick
        Timer5.Enabled = False
        If grdVoucher.RowCount = 0 Then Exit Sub
        If enableFocusOnQTYinPOS Then
            With grdVoucher
                .CurrentCell = .Item(ConstQty, .RowCount - 1)
                activecontrolname = "grdVoucher"
                .BeginEdit(True)
            End With
        ElseIf enableItemwiseSalesman Then
            With grdVoucher
                .CurrentCell = .Item(Constsman, .RowCount - 1)
                activecontrolname = "grdVoucher"
                .BeginEdit(True)
            End With
        Else
            txtitemcode.Focus()
            activecontrolname = ""
        End If
    End Sub
    Private Sub fSR_returnSR(ByVal invno As String, ByVal amt As Double) Handles fSR.returnSR
        txtsrno.Text = invno
        txtsramt.Text = Format(amt, lnumFormat)
    End Sub

    Private Sub btnprevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprevious.Click
        If loadedTrId > 0 And Val(btnhold.Tag) > 0 Then
            MsgBox("Hold current invoice before move", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        getPreInvoice()
        btndelete.Text = "Clear"
    End Sub
    Private Sub getNextInvoice()
        Dim dt As DataTable
        If loadedTrId > 0 Then
            dt = _objcmnbLayer._fldDatatable("select top 1 trid from ItmInvCmnTb where isnull(invStatus,0)=0 and trtype='IS' AND Counter='" & POScounter & "' " & _
                                             "AND trid>" & loadedTrId)
            If dt.Rows.Count > 0 Then
                loadedTrId = Val(dt(0)("trid") & "")
            Else
                MsgBox("Not Found", MsgBoxStyle.Exclamation)
                Exit Sub
            End If

            CheckNLoad(loadedTrId)
        Else
            MsgBox("Not Found", MsgBoxStyle.Exclamation)
        End If
    End Sub
    Private Sub getPreInvoice()
        Dim dt As DataTable
        If loadedTrId > 0 Then
            dt = _objcmnbLayer._fldDatatable("select max(trid) trid from ItmInvCmnTb where isnull(invStatus,0)=0 and trtype='IS' AND Counter='" & POScounter & "' " & _
                                            "AND trid<" & loadedTrId)
        Else
            dt = _objcmnbLayer._fldDatatable("select max(trid) trid from ItmInvCmnTb where isnull(invStatus,0)=0 and trtype='IS' AND Counter='" & POScounter & "'")
        End If
        If dt.Rows.Count > 0 Then
            If Val(dt(0)("trid") & "") > 0 Then
                loadedTrId = Val(dt(0)("trid") & "")
            Else
                MsgBox("Not Found", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
        Else
            MsgBox("Not Found", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If loadedTrId = 0 Then
            MsgBox("Not Found", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        CheckNLoad(loadedTrId)
    End Sub

    Private Sub btnnextInv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnextInv.Click
        getNextInvoice()
        btndelete.Text = "Clear"
    End Sub
    Private Sub SetGridHeadPoints()
        chgbyprg = True
        SetGridEditProperty(grdpoints)
        With grdpoints
            .ColumnCount = 7

            .Columns(constitemname).ReadOnly = True
            .Columns(constpDetId).Visible = False
            .Columns(constpTrid).Visible = False
            .Columns(constpItemid).Visible = False
            .Columns(constpRowIndex).Visible = False

            Dim cmb As New DataGridViewComboBoxColumn
            cmb.Items.Add("")
            For i = 0 To dtsalesman.Rows.Count - 1
                cmb.Items.Add(Trim(dtsalesman(i)(0)))
            Next
            cmb.HeaderText = "Service By"
            cmb.DataPropertyName = "SManCode"
            cmb.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
            cmb.DefaultCellStyle.Font = New System.Drawing.Font("Arial Black", 12.0!, System.Drawing.FontStyle.Bold)
            .Columns.RemoveAt(constpsman)
            .Columns.Insert(constpsman, cmb)
            .Columns(constpsman).HeaderText = "Serviced By"
            .Columns(constpsman).Width = 100
            .Columns(constpsman).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(constpsman).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns(constpoints).HeaderText = "Poins"

            resizeGridColumn(grdpoints, 0)
        End With
        chgbyprg = False
    End Sub
    Private Sub loadRawMertialForPoints(ByVal itemid As Long, ByVal rindex As Integer, ByVal isnew As Boolean)
        Dim bDatatable As DataTable
        bDatatable = _objcmnbLayer._fldDatatable("select [Item Code],salesPontOncount from ItemRawMeterialTb left " & _
                                                    "join InvItm on ItemRawMeterialTb.Ritemid =InvItm.ItemId where ItemRawMeterialTb.itemid=" & itemid)
        Dim i As Integer
        For i = 0 To bDatatable.Rows.Count - 1
            AddRow(, 1)
            chgItm = True
            Valid(grdVoucher.RowCount - 1, 1, bDatatable(i)("Item Code"))
            grdVoucher.Item(ConstWoodQty, grdVoucher.RowCount - 1).Value = bDatatable(i)("salesPontOncount")
        Next
        
        'SetGridHeadPoints()
        'Dim bDatatable As DataTable
        'If isnew Then
        '    bDatatable = _objcmnbLayer._fldDatatable("select Description,salesPontOncount from ItemRawMeterialTb left " & _
        '                                             "join InvItm on ItemRawMeterialTb.Ritemid =InvItm.ItemId where ItemRawMeterialTb.itemid=" & itemid)
        '    With grdpoints
        '        .Rows.Clear()
        '        Dim i As Integer
        '        For i = 0 To bDatatable.Rows.Count - 1
        '            .Rows.Add()
        '            .Item(constitemname, i).Value = bDatatable(i)("Description")
        '            .Item(constpsman, i).Value = ""
        '            .Item(constpoints, i).Value = Val(bDatatable(i)("salesPontOncount") & "")
        '            .Item(constpTrid, i).Value = loadedTrId
        '            .Item(constpDetId, i).Value = 0
        '            .Item(constpItemid, i).Value = itemid
        '            .Item(constpRowIndex, i).Value = rindex
        '        Next
        '    End With
        'Else
        '    If dtpoints.Rows.Count = 0 Then bDatatable = dtpoints.Clone : GoTo nxt
        '    Dim _qurey As EnumerableRowCollection(Of DataRow)
        '    _qurey = From data In dtpoints.AsEnumerable() Where data("RowIndex") = rindex + 1 Select data
        '    If _qurey.Count > 0 Then
        '        bDatatable = _qurey.CopyToDataTable()
        '    Else
        '        bDatatable = dtpoints.Clone
        '    End If
        '    With grdpoints
        '        .Rows.Clear()
        '        Dim i As Integer
        '        For i = 0 To bDatatable.Rows.Count - 1
        '            .Rows.Add()
        '            .Item(constitemname, rindex).Value = bDatatable(i)("itemname")
        '            .Item(constpsman, rindex).Value = bDatatable(i)("sman")
        '            .Item(constpoints, rindex).Value = Val(bDatatable(i)("points") & "")
        '            .Item(constpTrid, rindex).Value = loadedTrId
        '            .Item(constpDetId, rindex).Value = Val(bDatatable(i)("DetId") & "")
        '            .Item(constpItemid, rindex).Value = itemid
        '            .Item(constpRowIndex, rindex).Value = rindex
        '        Next
        '    End With
        'End If
        'tbpoints.Visible = True
        'activecontrolname = "grdpoints"
        'chgbyprg = True
        'With grdpoints
        '    .CurrentCell = .Item(constpsman, 0)
        '    .BeginEdit(True)
        'End With
        'chgbyprg = False
nxt:
    End Sub
    Private Sub saveMultipleServicePoints(ByVal trid As Long)
        Dim i As Integer
        Dim qry As String = "declare @detid bigint "
        For i = 0 To dtpoints.Rows.Count - 1
            qry = qry + " set @detid=0 select @detid=id from itminvtrtb where trid=" & trid & " and slno=" & Val(dtpoints(i)("RowIndex")) + 1 & " insert into SalesServicePointsTb(serviceby,trid,detid,itemid,rowindex,ponts) values('" & dtpoints(i)("sman") & "'," & _
            trid & ",@detid," & dtpoints(i)("itemid") & "," & dtpoints(i)("RowIndex") & "," & dtpoints(i)("RowIndex") & ")"
        Next
        _objcmnbLayer._saveDatawithOutParm(qry)
    End Sub
    Private Sub loadMultipleServicePoints(ByVal trid As Long)
        Dim dt As DataTable
        Dim qry As String
        qry = "select SalesServicePointsTb.*,Description from SalesServicePointsTb left join InvItm on SalesServicePointsTb.itemid=InvItm.ItemId where trid=" & trid
        dt = _objcmnbLayer._fldDatatable(qry)
        Dim i As Integer
        Dim drow As DataRow
        For i = 0 To dt.Rows.Count - 1
            drow = dtpoints.NewRow
            drow("itemname") = dt(i)("Description")
            drow("sman") = dt(i)("serviceby")
            drow("points") = dt(i)("ponts")
            drow("Trid") = trid
            drow("DetId") = dt(i)("detid")
            drow("Itemid") = dt(i)("itemid")
            drow("RowIndex") = dt(i)("rowindex")
        Next
    End Sub
    Private Sub deleteMultipleServicePoints(ByVal rindex As Integer)
        If dtpoints.Rows.Count = 0 Then dtpoints = dtpoints.Clone : GoTo nxt
        Dim _qurey As EnumerableRowCollection(Of DataRow)
        _qurey = From data In dtpoints.AsEnumerable() Where data("RowIndex") <> rindex + 1 Select data
        If _qurey.Count > 0 Then
            dtpoints = _qurey.CopyToDataTable()
        Else
            dtpoints = dtpoints.Clone
        End If
        Dim i As Integer
        For i = 0 To dtpoints.Rows.Count - 1
            If dtpoints(i)("rowindex") > rindex + 1 Then
                dtpoints(i)("rowindex") = dtpoints(i)("rowindex") - 1
            End If
        Next
nxt:
    End Sub
    Private Sub begineditPoints()
        chgbyprg = True
        grdpoints.BeginEdit(True)
        chgbyprg = False
    End Sub

    Private Sub grdpoints_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdpoints.CellClick
        begineditPoints()
    End Sub



    Private Sub txtsalesman_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtsalesman.TextChanged
        If chgbyprg = True Then Exit Sub
        _srchTxtId = 8
        _srchOnce = False
        ShowFmlist(sender)

        chgPost = True
    End Sub

    Private Sub txtsalesman_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtsalesman.Validated
        If Not fMList Is Nothing Then
            fMList.Close()
            fMList = Nothing
        End If
        chgbyprg = True
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("select SManName,SManCode,0 enablekot from SalesmanTb where SManCode='" & txtsalesman.Text & "'")
        If dt.Rows.Count > 0 Then
            txtsalesman.Tag = IIf(dt(0)("enableKOT"), 1, 0)
            txtsalesman.Text = dt(0)("SManCode")
            txtitemcode.Focus()
        Else
            txtsalesman.Tag = ""
            txtsalesman.Text = ""
        End If
        chgbyprg = False
    End Sub

    Private Sub picwhatsapp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picwhatsapp.Click
        Dim f As New WhatsaapFrm
        f.txtphone.Text = txtphone.Text
        f.txtparty.Text = txtCashCustomer.Text
        f.TopMost = True
        f.txtjobcode.Text = ""
        'f.txtamount.Text = lbloutstanding.Text
        f.txtreceived.Text = 0
        f.txtreceived.Visible = False
        f.chkoutstanding.Checked = True
        f.chkoutstanding.Enabled = False
        f.Label6.Visible = False
        f.Jobtype = "Admission No"
        f.ShowDialog()
    End Sub
End Class