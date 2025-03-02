
Public Class SalesReturnInvoice

#Region "Public Variables"
    Public isModi As Boolean
    Public isCust As Boolean
    Public isAddRow As Boolean
    Public isfromPos As Boolean
#End Region
#Region "Local Variables"
    Private vtype As String
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
    Private isImport As Boolean
    Private IsReturn As Boolean
    Private lnumFormat As String
    Private dtTax As DataTable
    Private DiscAcc As Long
    Private TrTypeNo As Integer
    Private FrTrId As Long
    Private dtMultipleDebits As DataTable
    Private dtSetoffTable As DataTable

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
    Private isprotected As Boolean
    Private cessdate As Date
    Private chgNumByPgm As Boolean
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
    Private WithEvents fSelectRtnItem As SalesPurchaseReturnItemsFrm
    Private WithEvents fproductMast As ItemMastFrm
    Private WithEvents fwait As WaitMessageFrm
    Private WithEvents fshowlocationqty As ShowLocationQtyFrm
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
    Private Const CostDbAlias = 1
    Private Const CostDbName = 2
    Private Const CostAmount = 3
    Private Const CostReference = 4
    Private Const CostDescr = 5
    Private Const CostCrAlias = 6
    Private Const CostCrName = 7
    Private Const CostFCAmount = 8
    Private Const CostFCName = 9
    Private Const CostFCRate = 10
    Private Const CostDrAcc = 11
    Private Const CostCrAcc = 12
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
    Public Event returnSR(ByVal invno As String, ByVal amt As Double)
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
            InvList = _objcmnbLayer._fldDatatable("SELECT TrId FROM ItmInvCmnTb WHERE  " & IIf(UsrBr = "", "", " Brid='" & UsrBr & "' AND") & " TrId = " & FromTrId)
        Else
            InvList = _objcmnbLayer._fldDatatable("SELECT TrId FROM ItmInvCmnTb WHERE " & IIf(UsrBr = "", "", " Brid='" & UsrBr & "' AND") & " TrType = 'SR' AND InvNo = " & Val(numVchrNo.Text) & " AND PreFix = '" & MkDbSrchStr(Trim(txtprefix.Text), True) & "'")
        End If
        If InvList.Rows.Count > 0 Then
            If Not isImport Then
                loadedTrId = InvList(0)("TrId")
                InvList = Nothing
                loadWaite(2)
                isModi = True
            Else
                isImport = False
                'PasteFrom(InvList(0)("TrId"))
                FrTrId = InvList(0)("TrId")
                loadWaite(3)
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
        Dim ItmInvCmnTb As DataTable
        Dim DocAssgnTb As DataTable
        Dim sRs As DataTable
        'Dim Discount As Double
        Dim UPerPack As Double
        Dim Protect As Boolean
        Dim CrossBr As Boolean
        Dim dtIO As DataTable
        Dim dtset As DataSet
        Dim itemquery As String = " SELECT ItmInvTrTb.*, [Item Code],Warrentyname,isSerialNo,fcode,FraCount," & _
                                          "isnull(itemCategory,'')itemCategory,collectionAC,vat,rgcess,rgcaccount,additionalcess FROM ItmInvTrTb LEFT JOIN InvItm ON InvItm.ItemId = ItmInvTrTb.ItemId " & _
                                          " LEFT JOIN InvItmPropertiesTb ON InvItmPropertiesTb.Itemid=invitm.itemid " & _
                                          "LEFT JOIN WarrentyMastertb ON WarrentyMastertb.Wid=ItmInvTrTb.WarrentyId " & _
                                          "LEFT JOIN FuelMeterReadingTb ON FuelMeterReadingTb.fmeterid=ItmInvTrTb.FuleMeterid " & _
                                          "LEFT JOIN UnitsTb ON UnitsTb.Units=ItmInvTrTb.Unit " & _
                                          "LEFT JOIN VatMasterTb ON InvItm.vatId=VatMasterTb.vatId " & _
                                          "LEFT JOIN (select vatcode rgccode,vat rgcess, collectionAC rgcaccount,vatid rgcid from  VatMasterTb)regularcess ON InvItm.regularcessid=regularcess.rgcid " & _
                                          "WHERE TrId = " & loadedTrId & " ORDER BY SlNo"

        dtset = _objcmnbLayer._ldDataset("SELECT ItmInvCmnTb.*,jobname,[Voucher Name],Ctgry,isnull(SalesmanTb.accountno,0) Smanacc,Alias,AccDescr,isnull(linkno,0)linkno " & _
                                                  "FROM ItmInvCmnTb " & _
                                                  "LEFT JOIN PreFixTb ON PreFixTb.id=ItmInvCmnTb.invtypeno " & _
                                                  "LEFT JOIN JobTb ON ItmInvCmnTb.[Job Code]=JobTb.jobcode " & _
                                                  "LEFT JOIN SalesmanTb ON SalesmanTb.SManCode=ItmInvCmnTb.SlsManId " & _
                                                  "LEFT JOIN AccMast ON AccMast.accid=ItmInvCmnTb.PSAcc " & _
                                                  "LEFT JOIN AccTrCmn on ItmInvCmnTb.trid=AccTrCmn.lnkno " & _
                                                  "WHERE TrId = " & loadedTrId & itemquery, False)

        ItmInvCmnTb = dtset.Tables(0) ' _objcmnbLayer._fldDatatable("SELECT ItmInvCmnTb.*,jobname FROM ItmInvCmnTb LEFT JOIN JobTb ON ItmInvCmnTb.[Job Code]=JobTb.jobcode WHERE TrId = " & loadedTrId & " AND TrType = 'SR'")
        chgbyprg = True
        If ItmInvCmnTb.Rows.Count = 0 Then GoTo Quit
        'getProtectUntil()
        dtIO = _objcmnbLayer._fldDatatable("SELECT prefix,invno,trid from ItmInvCmnTb where trid=" & ItmInvCmnTb(0)("SlsPurchRetId"))
        If dtIO.Rows.Count > 0 Then
            txtImpPrefix.Text = Trim(dtIO(0)("prefix") & "")
            txtDOLst.Text = Val(dtIO(0)("invno") & "")
            txtDOLst.Tag = Val(dtIO(0)("trid") & "")
            txtSuppName.Enabled = False
        Else
            txtSuppName.Enabled = True
        End If
        cmbVoucherTp.Tag = Val(ItmInvCmnTb(0)("invtypeno") & "")
        cmbVoucherTp.Text = ItmInvCmnTb(0)("Voucher Name")
        Select Case Val(ItmInvCmnTb(0)("Ctgry") & "")
            Case 0
                vtype = ""
            Case 1
                vtype = "Cash"
            Case 2
                vtype = "Card"
            Case 3
                vtype = "Credit"
            Case 4
                vtype = "Gift"
            Case 5
                vtype = "Disc"
        End Select
        'cmbVoucherTp.Text = getVoucherName(Val(ItmInvCmnTb(0)("InvTypeNo") & ""))

        cldrdate.Value = Format(ItmInvCmnTb(0)("TrDate"), DtFormat) ' "MM/dd/yyyy")
        txtprefix.Text = ItmInvCmnTb(0)("PreFix")
        txtprefix.Tag = ItmInvCmnTb(0)("PreFix")
        numVchrNo.Text = ItmInvCmnTb(0)("InvNo") 'Val(Mid(!InvNo, 3)) '!InvNo '
        numVchrNo.Tag = ItmInvCmnTb(0)("InvNo")
        numPrintVchr.Text = ItmInvCmnTb(0)("InvNo")  'CMNREC(12).FormattedText
        txtPPrefix.Text = ItmInvCmnTb(0)("PreFix")
        txtJob.Text = Trim("" & ItmInvCmnTb(0)("Job Code"))
        txtjobname.Text = Trim(ItmInvCmnTb(0)("jobname") & "")
        cmbfc.Text = ItmInvCmnTb(0)("FC")
        txtfcrt.Text = Format(ItmInvCmnTb(0)("FcRate"), lnumFormat)
        FCRt = ItmInvCmnTb(0)("FcRate")
        setCustomer(ItmInvCmnTb(0)("CSCode"))
        cmbsalesman.Text = Trim(ItmInvCmnTb(0)("SlsManId") & "")
        If IsDBNull(ItmInvCmnTb(0)("isTaxInvoice")) Then ItmInvCmnTb(0)("isTaxInvoice") = 0
        chktaxInv.Checked = IIf(ItmInvCmnTb(0)("isTaxInvoice") = "True", 1, 0)
        cmbdeliveredBy.Text = Trim(ItmInvCmnTb(0)("deliveredBy") & "")
        cmblocation.Text = Trim(ItmInvCmnTb(0)("DocDefLoc") & "")
        If Not IsDBNull(ItmInvCmnTb(0)("taxwithoutLineDiscount")) Then
            chktaxwithoutLinediscount.Checked = ItmInvCmnTb(0)("taxwithoutLineDiscount")
        End If
        chkexport.Checked = Val(ItmInvCmnTb(0)("isImportOrExport") & "")
        'sRs = _objcmnbLayer._fldDatatable("SELECT * FROM AccMast WHERE accid = " & ItmInvCmnTb(0)("PSAcc"))
        txtPurchAlias.Tag = ItmInvCmnTb(0)("PSAcc")
        txtPurchAlias.Text = ItmInvCmnTb(0)("Alias")
        txtPurchaseName.Text = ItmInvCmnTb(0)("AccDescr")
        
        txtReference.Text = Trim("" & ItmInvCmnTb(0)("TrRefNo"))
        txtDescr.Text = Trim("" & ItmInvCmnTb(0)("TrDescription"))
        numDisc.Text = Format(ItmInvCmnTb(0)("Discount") / FCRt, lnumFormat)
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
        'If sRs.Rows.Count > 0 Then sRs.Clear()
        'sRs = _objcmnbLayer._fldDatatable("SELECT ItmInvTrTb.*, [Item Code],Warrentyname,isSerialNo,fcode,FraCount," & _
        '                                  "isnull(itemCategory,'')itemCategory,collectionAC,vat,rgcess,rgcaccount,additionalcess FROM ItmInvTrTb LEFT JOIN InvItm ON InvItm.ItemId = ItmInvTrTb.ItemId " & _
        '                                  " LEFT JOIN InvItmPropertiesTb ON InvItmPropertiesTb.Itemid=invitm.itemid " & _
        '                                  "LEFT JOIN WarrentyMastertb ON WarrentyMastertb.Wid=ItmInvTrTb.WarrentyId " & _
        '                                  "LEFT JOIN FuelMeterReadingTb ON FuelMeterReadingTb.fmeterid=ItmInvTrTb.FuleMeterid " & _
        '                                  "LEFT JOIN UnitsTb ON UnitsTb.Units=ItmInvTrTb.Unit " & _
        '                                  "LEFT JOIN VatMasterTb ON InvItm.vatId=VatMasterTb.vatId " & _
        '                                  "LEFT JOIN (select vatcode rgccode,vat rgcess, collectionAC rgcaccount,vatid rgcid from  VatMasterTb)regularcess ON InvItm.regularcessid=regularcess.rgcid " & _
        '                                  "WHERE TrId = " & loadedTrId & " ORDER BY SlNo")
        sRs = dtset.Tables(1)
        grdVoucher.RowCount = 0
        If _objcmnbLayer.dtSerialNo.Rows.Count > 0 Then _objcmnbLayer.dtSerialNo.Rows.Clear()
        With grdVoucher
            .Rows.Clear()
            If sRs.Rows.Count > 0 Then
                For i = 0 To sRs.Rows.Count - 1
                    grdVoucher.Rows.Add(1)
                    chgbyprg = True
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

                    If Val(sRs(i)("vat") & "") = 0 Then sRs(i)("vat") = 0
                    If Val(sRs(i)("rgcess") & "") = 0 Then sRs(i)("rgcess") = 0
                    If Not enableGCC Then
                        grdVoucher.Item(Constcess, i).Value = Format(sRs(i)("vat"), lnumFormat)
                        grdVoucher.Item(ConstRegcess, i).Value = Format(sRs(i)("rgcess"), lnumFormat)
                    Else
                        grdVoucher.Item(Constcess, i).Value = 0
                        grdVoucher.Item(ConstRegcess, i).Value = 0
                        sRs(i)("IGSTP") = sRs(i)("vat")
                    End If

                    If Val(sRs(i)("CessAmt") & "") = 0 Then sRs(i)("CessAmt") = 0
                    grdVoucher.Item(ConstcessAmt, i).Value = Format(sRs(i)("CessAmt") / FCRt, lnumFormat)
                    If Val(sRs(i)("regularcessAmt") & "") = 0 Then sRs(i)("regularcessAmt") = 0
                    grdVoucher.Item(ConstregularCessAmt, i).Value = Format(sRs(i)("regularcessAmt") / FCRt, lnumFormat)
                    If Val(sRs(i)("FloodcessAmt") & "") = 0 Then sRs(i)("FloodcessAmt") = 0
                    grdVoucher.Item(ConstFloodCessAmt, i).Value = Format(sRs(i)("FloodcessAmt") / FCRt, lnumFormat)
                    If Val(sRs(i)("additionalcess") & "") = 0 Then sRs(i)("additionalcess") = 0
                    grdVoucher.Item(ConstAdditionalcess, i).Value = sRs(i)("additionalcess") / FCRt
                    If Val(sRs(i)("collectionAC") & "") = 0 Then sRs(i)("collectionAC") = 0
                    grdVoucher.Item(ConstcessAc, i).Value = Val(sRs(i)("collectionAC"))
                    If Val(sRs(i)("rgcaccount") & "") = 0 Then sRs(i)("rgcaccount") = 0
                    grdVoucher.Item(ConstRegcessAc, i).Value = Val(sRs(i)("rgcaccount"))

                    grdVoucher.Item(ConstLocation, i).Value = Trim("" & sRs(i)("Warrentyname")) ' !Colour
                    grdVoucher.Item(ConstQty, i).Value = Format(sRs(i)("TrQty") / UPerPack, "#,##0" & IIf(Val(grdVoucher.Item(ConstPFraction, i).Value & "") = 0, "", "." & Strings.StrDup(CByte(Val(grdVoucher.Item(ConstPFraction, i).Value & "")), "0")))
                    grdVoucher.Item(ConstUPrice, i).Value = Format(sRs(i)("UnitCost") * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & lnumFormat)
                    grdVoucher.Item(ConstActualPrice, i).Value = sRs(i)("UnitCost") * UPerPack / FCRt
                    grdVoucher.Item(ConstLTotal, i).Value = Format((sRs(i)("TrQty") * sRs(i)("UnitCost")) * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & lnumFormat)
                    grdVoucher.Item(ConstUnitOthCost, i).Value = Format(sRs(i)("UnitOthCost") * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & lnumFormat)
                    grdVoucher.Item(ConstActualOthCost, i).Value = sRs(i)("UnitOthCost") * UPerPack / FCRt
                    grdVoucher.Item(ConstSerialNo, i).Value = sRs(i)("SerialNo")
                    .Item(constItmTot, i).Value = Format((.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value), lnumFormat)
                    'grdVoucher.Item(ConstMthd, i).Value = sRs(i)("Method")
                    'grdVoucher.Item(ConstUnitVal, i).Value = sRs(i)("UnitValue") * UPerPack / FCRt
                    'grdVoucher.Item(ConstDiscOther, i).Value = sRs(i)("DiscOther") * UPerPack / FCRt
                    grdVoucher.Item(ConstDiscOther, i).Value = sRs(i)("UnitDiscount") * UPerPack / FCRt
                    If Val(sRs(i)("DisP") & "") = 0 Then sRs(i)("DisP") = 0
                    grdVoucher.Item(ConstDisP, i).Value = Format(sRs(i)("DisP"), numFormat)
                    If Val(sRs(i)("ItemDiscount") & "") = 0 Then sRs(i)("ItemDiscount") = 0
                    grdVoucher.Item(ConstDisAmt, i).Value = Format(sRs(i)("ItemDiscount") * UPerPack / FCRt, lnumFormat)
                    grdVoucher.Item(ConstImpDocId, i).Value = Val(sRs(i)("impDocid") & "")
                    grdVoucher.Item(ConstImpLnId, i).Value = Val(sRs(i)("impDocSlno") & "")

                    grdVoucher.Item(ConstBarcode, i).Value = sRs(i)("HSNCode")
                    grdVoucher.Item(ConstCGSTP, i).Value = sRs(i)("CSGTP")
                    grdVoucher.Item(ConstCGSTAmt, i).Value = sRs(i)("CGSTAMT") / FCRt

                    grdVoucher.Item(ConstSGSTP, i).Value = sRs(i)("SGSTP")
                    grdVoucher.Item(ConstSGSTAmt, i).Value = sRs(i)("SGSTAmt") / FCRt

                    grdVoucher.Item(ConstIGSTP, i).Value = sRs(i)("IGSTP")
                    grdVoucher.Item(ConstIGSTAmt, i).Value = sRs(i)("IGSTAmt") / FCRt

                    If Not IsDBNull(sRs(i)("isSerialNo")) Then
                        grdVoucher.Item(ConstIsSerial, i).Value = IIf(sRs(i)("isSerialNo"), 1, 0)
                    Else
                        grdVoucher.Item(ConstIsSerial, i).Value = 0
                    End If
                    If Not isImport Then
                        grdVoucher.Item(ConstId, i).Value = sRs(i)("id")
                    Else
                        grdVoucher.Item(ConstId, i).Value = 0
                    End If
                    If Not IsDBNull(sRs(i)("WarrentyExpDate")) Then
                        If DateValue(sRs(i)("WarrentyExpDate")) > DateValue("01/01/1950") Then
                            grdVoucher.Item(ConstWarrentyExpiry, i).Value = sRs(i)("WarrentyExpDate")
                        End If
                    End If
                    grdVoucher.Item(Constsman, i).Value = Trim(sRs(i)("Smancode") & "")
                    If .Item(ConstSerialNo, i).Value <> "" And enableSerialnumber Then
                        AddTodtSerialNo(.Item(ConstSerialNo, i).Value, Val(.Item(ConstItemID, .CurrentRow.Index).Value), .CurrentRow.Index, DateValue(.Item(ConstWarrentyExpiry, .CurrentRow.Index).Value), Val(.Item(ConstId, .CurrentRow.Index).Value))
                    End If
                    If Trim(sRs(i)("itemCategory") & "") = "room" Then
                        .Rows(i).DefaultCellStyle.BackColor = Color.LightGray
                        '.Rows(i).DefaultCellStyle.ForeColor = Color.White
                    End If
                    If Not IsDBNull(sRs(i)("manufacturingdate")) Then
                        If DateValue(sRs(i)("manufacturingdate")) > DateValue("01/01/1950") Then
                            grdVoucher.Item(ConstManufacturingdate, i).Value = sRs(i)("manufacturingdate")
                        End If
                    End If
                    If Val(sRs(i)("MRP") & "") = 0 Then sRs(i)("MRP") = 0
                    .Item(ConstMRP, i).Value = _objInv.MRP = CDbl(sRs(i)("MRP"))
                    If Val(sRs(i)("costavg") & "") = 0 Then sRs(i)("costavg") = 0
                    .Item(ConstBatchCost, i).Value = _objInv.MRP = CDbl(sRs(i)("costavg"))
                    calcualteLineTotal(i)
                Next
            End If
        End With
        If sRs.Rows.Count > 0 Then sRs.Clear()
        If enableMultipleDebitInInvoice Then loadSalesMultipleDebits(loadedTrId)
        calculate()
        reArrangeNo()
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
        Dim dtInvCmn As DataTable

        dtInvCmn = _objcmnbLayer._fldDatatable("SELECT * FROM ItmInvCmnTb left join (select SlsPurchRetId returnid from ItmInvCmnTb where trtype='SR') SR " & _
                                               "ON ItmInvCmnTb.Trid=sr.returnid" & _
                                               " Where trid=" & Trid)
        If dtInvCmn.Rows.Count > 0 Then
            chgbyprg = Trid
            cmbfc.Text = dtInvCmn(0)("FC")
            txtfcrt.Text = Format(dtInvCmn(0)("FcRate"), lnumFormat)
            FCRt = dtInvCmn(0)("FcRate")
            txtImpPrefix.Text = dtInvCmn(0)("PreFix")
            txtDOLst.Text = dtInvCmn(0)("InvNo")
            txtDOLst.Tag = dtInvCmn(0)("trid")
            txtReference.Text = Trim(txtImpPrefix.Text) & IIf(txtImpPrefix.Text = "", "", "/") & txtDOLst.Text
            setCustomer(dtInvCmn(0)("CSCode"))
            If Val(dtInvCmn(0)("Discount") & "") = 0 Then dtInvCmn(0)("Discount") = 0
            numDisc.Text = Format(CDbl(dtInvCmn(0)("Discount")), lnumFormat)
            If cmbsalesman.Text = "" Then
                cmbsalesman.Text = Trim(dtInvCmn(0)("SlsManId") & "")
            End If
            chgbyprg = False
            If IsReturn Then
                If Val(dtInvCmn(0)("returnid") & "") > 0 Then
                    If MsgBox("Sales Return already found! Do you want to make sales return agian?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                End If
                txtSuppName.Enabled = False
                If fSelectRtnItem Is Nothing Then
                    fSelectRtnItem = New SalesPurchaseReturnItemsFrm
                End If
                fSelectRtnItem.ldItems(Trid)
                fSelectRtnItem.ShowDialog()
                fSelectRtnItem = Nothing
                Exit Sub
            End If
        End If

        sRs = _objcmnbLayer._fldDatatable("SELECT ItmInvTrTb.*, [Item Code],Warrentyname,isSerialNo,fcode,FraCount," & _
                                   "isnull(itemCategory,'')itemCategory,collectionAC,vat,rgcess,rgcaccount,additionalcess FROM ItmInvTrTb LEFT JOIN InvItm ON InvItm.ItemId = ItmInvTrTb.ItemId " & _
                                   " LEFT JOIN InvItmPropertiesTb ON InvItmPropertiesTb.Itemid=invitm.itemid " & _
                                   "LEFT JOIN WarrentyMastertb ON WarrentyMastertb.Wid=ItmInvTrTb.WarrentyId " & _
                                   "LEFT JOIN FuelMeterReadingTb ON FuelMeterReadingTb.fmeterid=ItmInvTrTb.FuleMeterid " & _
                                   "LEFT JOIN UnitsTb ON UnitsTb.Units=ItmInvTrTb.Unit " & _
                                   "LEFT JOIN VatMasterTb ON InvItm.vatId=VatMasterTb.vatId " & _
                                   "LEFT JOIN (select vatcode rgccode,vat rgcess, collectionAC rgcaccount,vatid rgcid from  VatMasterTb)regularcess ON InvItm.regularcessid=regularcess.rgcid " & _
                                   "WHERE TrId = " & loadedTrId & " ORDER BY SlNo")
        grdVoucher.RowCount = 0
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

                    If Val(sRs(i)("vat") & "") = 0 Then sRs(i)("vat") = 0
                    If Val(sRs(i)("rgcess") & "") = 0 Then sRs(i)("rgcess") = 0
                    If Not enableGCC Then
                        grdVoucher.Item(Constcess, i).Value = Format(sRs(i)("vat"), lnumFormat)
                        grdVoucher.Item(ConstRegcess, i).Value = Format(sRs(i)("rgcess"), lnumFormat)
                    Else
                        grdVoucher.Item(Constcess, i).Value = 0
                        grdVoucher.Item(ConstRegcess, i).Value = 0
                    End If

                    If Val(sRs(i)("CessAmt") & "") = 0 Then sRs(i)("CessAmt") = 0
                    grdVoucher.Item(ConstcessAmt, i).Value = Format(sRs(i)("CessAmt") / FCRt, lnumFormat)
                    If Val(sRs(i)("regularcessAmt") & "") = 0 Then sRs(i)("regularcessAmt") = 0
                    grdVoucher.Item(ConstregularCessAmt, i).Value = Format(sRs(i)("regularcessAmt") / FCRt, lnumFormat)
                    If Val(sRs(i)("FloodcessAmt") & "") = 0 Then sRs(i)("FloodcessAmt") = 0
                    grdVoucher.Item(ConstFloodCessAmt, i).Value = Format(sRs(i)("FloodcessAmt") / FCRt, lnumFormat)
                    If Val(sRs(i)("additionalcess") & "") = 0 Then sRs(i)("additionalcess") = 0
                    grdVoucher.Item(ConstAdditionalcess, i).Value = sRs(i)("additionalcess")
                    If Val(sRs(i)("collectionAC") & "") = 0 Then sRs(i)("collectionAC") = 0
                    grdVoucher.Item(ConstcessAc, i).Value = Val(sRs(i)("collectionAC"))
                    If Val(sRs(i)("rgcaccount") & "") = 0 Then sRs(i)("rgcaccount") = 0
                    grdVoucher.Item(ConstRegcessAc, i).Value = Val(sRs(i)("rgcaccount"))

                    grdVoucher.Item(ConstLocation, i).Value = Trim("" & sRs(i)("Warrentyname")) ' !Colour
                    grdVoucher.Item(ConstQty, i).Value = Format(sRs(i)("TrQty") / UPerPack, "#,##0" & IIf(Val(grdVoucher.Item(ConstPFraction, i).Value & "") = 0, "", "." & Strings.StrDup(CByte(Val(grdVoucher.Item(ConstPFraction, i).Value & "")), "0")))
                    If Val(sRs(i)("Focqty") & "") = 0 Then sRs(i)("Focqty") = 0
                    grdVoucher.Item(ConstFocQty, i).Value = Format(sRs(i)("Focqty") / UPerPack, "#,##0" & IIf(Val(grdVoucher.Item(ConstPFraction, i).Value) = 0, "", "." & Strings.StrDup(CByte(grdVoucher.Item(ConstPFraction, i).Value), "0")))

                    grdVoucher.Item(ConstUPrice, i).Value = Format(sRs(i)("UnitCost") * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & lnumFormat)
                    grdVoucher.Item(ConstActualPrice, i).Value = sRs(i)("UnitCost") * UPerPack / FCRt
                    grdVoucher.Item(ConstLTotal, i).Value = Format((sRs(i)("TrQty") * sRs(i)("UnitCost")) * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & lnumFormat)
                    grdVoucher.Item(ConstUnitOthCost, i).Value = Format(sRs(i)("UnitOthCost") * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & lnumFormat)
                    grdVoucher.Item(ConstActualOthCost, i).Value = sRs(i)("UnitOthCost") * UPerPack / FCRt
                    grdVoucher.Item(ConstSerialNo, i).Value = sRs(i)("SerialNo")
                    .Item(constItmTot, i).Value = Format((.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value), lnumFormat)
                    'grdVoucher.Item(ConstMthd, i).Value = sRs(i)("Method")
                    'grdVoucher.Item(ConstUnitVal, i).Value = sRs(i)("UnitValue") * UPerPack / FCRt
                    'grdVoucher.Item(ConstDiscOther, i).Value = sRs(i)("DiscOther") * UPerPack / FCRt
                    grdVoucher.Item(ConstDiscOther, i).Value = sRs(i)("UnitDiscount") * UPerPack / FCRt
                    If Val(sRs(i)("DisP") & "") = 0 Then sRs(i)("DisP") = 0
                    grdVoucher.Item(ConstDisP, i).Value = Format(sRs(i)("DisP"), numFormat)
                    If Val(sRs(i)("ItemDiscount") & "") = 0 Then sRs(i)("ItemDiscount") = 0
                    grdVoucher.Item(ConstDisAmt, i).Value = Format(sRs(i)("ItemDiscount") * UPerPack / FCRt, lnumFormat)
                    grdVoucher.Item(ConstImpDocId, i).Value = Val(sRs(i)("impDocid") & "")
                    grdVoucher.Item(ConstImpLnId, i).Value = Val(sRs(i)("impDocSlno") & "")

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
                   grdVoucher.Item(ConstId, i).Value = 0
                    If Not IsDBNull(sRs(i)("WarrentyExpDate")) Then
                        If DateValue(sRs(i)("WarrentyExpDate")) > DateValue("01/01/1950") Then
                            grdVoucher.Item(ConstWarrentyExpiry, i).Value = sRs(i)("WarrentyExpDate")
                        End If
                    End If
                    grdVoucher.Item(Constsman, i).Value = Trim(sRs(i)("Smancode") & "")
                    If .Item(ConstSerialNo, i).Value <> "" And enableSerialnumber Then
                        AddTodtSerialNo(.Item(ConstSerialNo, i).Value, Val(.Item(ConstItemID, i).Value), i, DateValue(.Item(ConstWarrentyExpiry, i).Value), Val(.Item(ConstId, i).Value))
                    End If
                    If Trim(sRs(i)("itemCategory") & "") = "room" Then
                        .Rows(i).DefaultCellStyle.BackColor = Color.LightGray
                        '.Rows(i).DefaultCellStyle.ForeColor = Color.White
                    End If
                    If Not IsDBNull(sRs(i)("manufacturingdate")) Then
                        If DateValue(sRs(i)("manufacturingdate")) > DateValue("01/01/1950") Then
                            grdVoucher.Item(ConstManufacturingdate, i).Value = sRs(i)("manufacturingdate")
                        End If
                    End If
                    If Val(sRs(i)("MRP") & "") = 0 Then sRs(i)("MRP") = 0
                    .Item(ConstMRP, i).Value = _objInv.MRP = CDbl(sRs(i)("MRP"))
                    If Val(sRs(i)("costavg") & "") = 0 Then sRs(i)("costavg") = 0
                    .Item(ConstBatchCost, i).Value = _objInv.MRP = CDbl(sRs(i)("costavg"))
                    calcualteLineTotal(i)
                Next
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
        '    dtInv = _objcmnbLayer._fldDatatable("SELECT Prefix,InvNo FROM InvNos WHERE InvType='SR'")
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
            With cmbVoucherTp
                If .Items.Count > 0 Then
                    If .SelectedIndex < 0 Then .SelectedIndex = 0
                    cmbVoucherTp.Tag = getvrsId(cmbVoucherTp.Text, 8)
                    getVrsDet(Val(cmbVoucherTp.Tag), "SR", vrPrefix, vrVoucherNo, vrAccountNo1, vrAccountNo2, , , vtype)
                Else
                    getVrsDet(0, "SR", vrPrefix, vrVoucherNo, vrAccountNo1, vrAccountNo2)
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
            'Dim dtAcc As DataTable
            chgbyprg = True
            Dim dtTable As DataTable
            Dim qry As String = "SELECT Alias,AccDescr,GrpSetOn,GrpSetOn,accid FROM AccMast " & _
            "LEFT JOIN S1AccHd ON AccMast.S1AccId=S1AccHd.S1AccId WHERE accid=" & vrAccountNo1
            qry = qry & " SELECT Alias,AccDescr,GrpSetOn,GrpSetOn,accid FROM AccMast " & _
                      "LEFT JOIN S1AccHd ON AccMast.S1AccId=S1AccHd.S1AccId WHERE accid=" & vrAccountNo2

            Dim ds As DataSet
            ds = _objcmnbLayer._ldDataset(qry, False)
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
            'dtAcc = _objcmnbLayer._fldDatatable("SELECT Alias,AccDescr,GrpSetOn,GrpSetOn,accid FROM AccMast " & _
            '                                    "LEFT JOIN S1AccHd ON AccMast.S1AccId=S1AccHd.S1AccId WHERE accid=" & vrAccountNo1)
            'If dtAcc.Rows.Count > 0 Then
            '    txtPurchaseName.Text = dtAcc(0)("AccDescr")
            '    txtPurchAlias.Text = dtAcc(0)("Alias")
            '    txtPurchAlias.Tag = vrAccountNo1
            'End If
            'dtAcc = _objcmnbLayer._fldDatatable("SELECT Alias,AccDescr,GrpSetOn,GrpSetOn,accid FROM AccMast " & _
            '                                   "LEFT JOIN S1AccHd ON AccMast.S1AccId=S1AccHd.S1AccId WHERE accid=" & vrAccountNo2)
            'If dtAcc.Rows.Count > 0 Then
            '    txtSuppName.Text = dtAcc(0)("AccDescr")
            '    txtSuppName.Text = dtAcc(0)("Alias")
            '    txtSuppAlias.Tag = vrAccountNo2
            '    setCustomer(vrAccountNo2)
            'End If
            dtTable.Rows.Clear()
            dtTable = ds.Tables(1)
            If dtTable.Rows.Count > 0 Then
                txtSuppName.Text = dtTable(0)("AccDescr")
                txtSuppAlias.Text = dtTable(0)("Alias")
                txtSuppAlias.Tag = vrAccountNo2
            Else
                txtSuppName.Text = ""
                txtSuppName.Text = ""
                txtSuppAlias.Tag = ""
            End If
            chgbyprg = False
            txtReference.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub txtReference_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDescr.KeyDown, txtSuppName.KeyDown, txtSuppAlias.KeyDown, txtReference.KeyDown, txtjobname.KeyDown, txtJob.KeyDown
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
                    ldSelect(2)
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
                        SetFmlist(fMList, 13)
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
        SetGridHeadEntryProperty(grdVoucher)
        With grdVoucher
            '.Columns(ConstDescr).Width = 350
            '.Columns(ConstDisAmt).Width = Me.Width * 7 / 100 '70
            '.Columns(ConstTaxP).Width = Me.Width * 6 / 100 '60
            '.Columns(ConstTaxAmt).Width = Me.Width * 7 / 100 '70
            '.Columns(ConstUnitOthCost).Width = Me.Width * 9 / 100 '80
            '.Columns(ConstNUPrice).Width = Me.Width * 8 / 100 '70
            '.Columns(ConstTaxP).ReadOnly = False
            .Columns(ConstMRP).Visible = enableMRPInStockIn
            .Columns(ConstSP1).Visible = enableSP1InStockIn
            .Columns(ConstSP2).Visible = enableSP2InStockIn
            .Columns(ConstSP3).Visible = enableSP3InStockIn
            .Columns(ConstUPrice).ReadOnly = disablePriceEditInPos
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
            .Item(ConstQty, i).Value = Format(0, lnumFormat)
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
        'calculate()
        'ChgByPrg = False
        'If enableItemAutoPopulate Then
        '    fProductEnquiry = New ItmEnqry
        '    fProductEnquiry.ShowDialog()
        'End If
        doCommandStat(True)
        chgPost = True
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
            If chktaxwithoutLinediscount.Checked Then
                actualPrice = (CDbl(.Item(ConstActualPrice, i).Value) * CDbl(.Item(ConstQty, i).Value)) - discountOther
            Else
                actualPrice = (CDbl(.Item(ConstActualPrice, i).Value) * CDbl(.Item(ConstQty, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value) - discountOther
            End If
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
            If enableFloodCess And cessenddate >= DateValue(cldrdate.Value) Then
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
                totQty = totQty + IIf(Val(.Item(ConstQty, i).Value) > 0, Val(.Item(ConstQty, i).Value), 0)
                If chktaxInv.Checked = False Then
                    .Item(ConstTaxAmt, i).Value = Format(0, lnumFormat)
                    .Item(ConstTaxP, i).Value = Format(0, lnumFormat)
                    .Item(ConstcessAmt, i).Value = 0
                    '.Item(ConstCGSTAmt, i).Value = Format(0, lnumFormat)
                    '.Item(ConstSGSTAmt, i).Value = Format(0, lnumFormat)
                    '.Item(ConstIGSTAmt, i).Value = Format(0, lnumFormat)
                Else
                    .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstIGSTAmt, i).Value), lnumFormat)
                    .Item(ConstTaxP, i).Value = Format(CDbl(.Item(ConstIGSTP, i).Value), lnumFormat)
                    If enablecess Then
                        lnTax = CDbl(.Item(ConstregularCessAmt, i).Value)
                        'lnTax = ((actualPrice * .Item(Constcess, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value)
                        'lnTax = lnTax + (CDbl(.Item(ConstAdditionalcess, i).Value) * CDbl(.Item(ConstQty, i).Value))
                    End If
                    If enableFloodCess And Val(lblgstn.Tag & "") = 0 And cessenddate >= DateValue(cldrdate.Value) Then
                        'lnTax = lnTax + ((actualPrice * .Item(ConstFloodcess, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value)
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
                'totAmt = totAmt + (CDbl(.Item(ConstQty, i).Value) * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value)
                'totItm = totItm + (CDbl(.Item(ConstQty, i).Value) * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value)
                If (enablecess Or (enableFloodCess And cessenddate >= DateValue(cldrdate.Value))) And chktaxInv.Checked Then
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
            'lblNetAmt.Text = Format(totAmt - CDbl(numDisc.Text), lnumFormat)
            If chkautoroundOff.Checked Then
                If Not blockAutoRoundOff And CDbl(lblNetAmt.Text) > 0 Then
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

            lbltax.Text = Format(totTax, lnumFormat)
            lblcess.Text = Format(totCess, lnumFormat)
            totAmt = totAmt - CDbl(numDisc.Text)
            lbltaxable.Text = Format(totTaxableAmt, lnumFormat)
            chgAmt = False
            chgbyprg = False
        End With
    End Sub
    'Private Sub calcualteLineTotal(ByVal RowIndex As Integer)
    '    chgbyprg = True
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
    '        ElseIf enableGCC Or ShowTaxOnInventory Then
    '            Dim actualPrice As Double
    '            Dim discountOther As Double
    '            discountOther = CDbl(.Item(ConstDiscOther, i).Value) * CDbl(.Item(ConstQty, i).Value)
    '            actualPrice = (CDbl(.Item(ConstActualPrice, i).Value) * CDbl(.Item(ConstQty, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value) - discountOther
    '            actualPrice = Format(actualPrice, lnumFormat)
    '            .Item(ConstIGSTAmt, i).Value = ((actualPrice * .Item(ConstIGSTP, i).Value) / 100)
    '            .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstIGSTAmt, i).Value), lnumFormat)
    '        End If
    '        If enablecess And Val(lblgstn.Tag & "") = 0 And cessdate <= DateValue(cldrdate.Value) And chktaxInv.Checked Then
    '            cessTtl = (((CDbl(.Item(ConstActualPrice, i).Value) - CDbl(.Item(ConstDiscOther, i).Value)) * .Item(Constcess, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value)
    '            .Item(ConstcessAmt, i).Value = Format(cessTtl, lnumFormat)
    '        End If
    '        If chktaxInv.Checked = False Then
    '            .Item(ConstTaxAmt, i).Value = Format(0, lnumFormat)
    '            .Item(ConstTaxP, i).Value = Format(0, lnumFormat)
    '            .Item(ConstcessAmt, i).Value = 0
    '        End If

    '        .Item(constItmTot, i).Value = Format((.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value), lnumFormat)
    '        Dim ttl As Double
    '        ttl = (.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value) + gstamt + cessTtl
    '        .Item(ConstLTotal, i).Value = Format(ttl, lnumFormat)
    '        .Item(ConstNUPrice, i).Value = CDbl(.Item(ConstActualPrice, i).Value) - (CDbl(.Item(ConstDisAmt, i).Value) / CDbl(.Item(ConstQty, i).Value)) + (CDbl(.Item(ConstTaxAmt, i).Value) / CDbl(.Item(ConstQty, i).Value)) + (CDbl(.Item(ConstcessAmt, i).Value) / CDbl(.Item(ConstQty, i).Value)) - Val(.Item(ConstDiscOther, i).Value)
    '    End With
    '    chgbyprg = False
    'End Sub
    '    Private Sub calculate(Optional ByVal bFrmCalcOthCost As Boolean = False, Optional ByVal calculateLineTotal As Boolean = False, Optional ByVal chgDiscount As Boolean = False)
    '        Dim totQty As Double
    '        Dim totItm As Double
    '        Dim totTax As Double
    '        Dim totLnDis As Double
    '        Dim totAmt As Double
    '        Dim totCess As Double
    '        Dim lnTax As Double
    '        Dim lnttl As Double
    '        Dim i As Integer
    '        Dim totTaxableAmt As Double
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
    '                lnTax = 0
    '                If (calculateLineTotal And Val(numDisc.Text) > 0) Or chgDiscount Then
    '                    calcualteLineTotal(i)
    '                End If
    '                If Val(.Item(ConstItemID, i).Value) = 0 Then GoTo nxt
    '                totQty = totQty + IIf(Val(.Item(ConstQty, i).Value) > 0, Val(.Item(ConstQty, i).Value), 0)
    '                If chktaxInv.Checked = False Then
    '                    .Item(ConstTaxAmt, i).Value = Format(0, lnumFormat)
    '                    .Item(ConstTaxP, i).Value = Format(0, lnumFormat)
    '                    .Item(ConstcessAmt, i).Value = 0
    '                    '.Item(ConstCGSTAmt, i).Value = Format(0, lnumFormat)
    '                    '.Item(ConstSGSTAmt, i).Value = Format(0, lnumFormat)
    '                    '.Item(ConstIGSTAmt, i).Value = Format(0, lnumFormat)
    '                Else
    '                    .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstIGSTAmt, i).Value), lnumFormat)
    '                    .Item(ConstTaxP, i).Value = Format(CDbl(.Item(ConstIGSTP, i).Value), lnumFormat)
    '                    If enablecess And Val(lblgstn.Tag & "") = 0 And cessdate <= DateValue(cldrdate.Value) Then
    '                        lnTax = (((CDbl(.Item(ConstActualPrice, i).Value) - CDbl(.Item(ConstDiscOther, i).Value)) * .Item(Constcess, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value)
    '                        .Item(ConstcessAmt, i).Value = Format(lnTax, lnumFormat)
    '                    End If
    '                    lnTax = lnTax + CDbl(.Item(ConstIGSTAmt, i).Value)
    '                End If
    '                If EnableGST Then
    '                    If chktaxInv.Checked Then
    '                        totTax = totTax + CDbl(.Item(ConstCGSTAmt, i).Value) + CDbl(.Item(ConstSGSTAmt, i).Value)
    '                    End If
    '                Else
    '                    If chktaxInv.Checked Then
    '                        totTax = totTax + CDbl(.Item(ConstIGSTAmt, i).Value)
    '                    End If
    '                End If
    '                totLnDis = totLnDis + .Item(ConstDisAmt, i).Value
    '                totAmt = totAmt + (CDbl(.Item(ConstQty, i).Value) * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value)
    '                totItm = totItm + (CDbl(.Item(ConstQty, i).Value) * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value)
    '                If enablecess And Val(lblgstn.Tag & "") = 0 And cessdate <= DateValue(cldrdate.Value) And chktaxInv.Checked Then
    '                    totCess = totCess + CDbl(.Item(ConstcessAmt, i).Value)
    '                End If
    '                lnttl = (CDbl(.Item(ConstQty, i).Value) * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value)
    '                lnttl = lnttl + lnTax
    '                chgbyprg = True
    '                .Item(ConstLTotal, i).Value = Format(lnttl, lnumFormat)
    '                chgbyprg = False
    '                If Val(.Item(ConstTaxAmt, i).Value) > 0 Then
    '                    totTaxableAmt = totTaxableAmt + ((CDbl(.Item(ConstActualPrice, i).Value) - CDbl(.Item(ConstDiscOther, i).Value)) * CDbl(.Item(ConstQty, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value)
    '                End If
    'nxt:
    '            Next
    '            calOthCost()

    '            totAmt = totAmt + totTax + totCess
    '            lblTotAmt.Text = Format(totItm, lnumFormat)
    '            lblNetAmt.Text = Format(totAmt - CDbl(numDisc.Text), lnumFormat)
    '            lbltax.Text = Format(totTax, lnumFormat)
    '            lblcess.Text = Format(totCess, lnumFormat)
    '            totAmt = totAmt - CDbl(numDisc.Text)
    '            lbltaxable.Text = Format(totTaxableAmt, lnumFormat)
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
                    discamt = CDbl(.Item(ConstDisAmt, i).Value) / CDbl(.Item(ConstQty, i).Value)
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
                        '.Item(ConstDiscOther, i).Value = (tDAmt * actualPrice) / tBDAmt
                    End If
                End If
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
                    UpdateClick()
                ElseIf msg.WParam.ToInt32() = CInt(Keys.F5) Then
                    ClearClick()
                ElseIf msg.WParam.ToInt32() = CInt(Keys.Escape) Then
                    If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
                    If Not fSelect Is Nothing Then fSelect.Close() : fSelect = Nothing
                    If Not fProductEnquiry Is Nothing Then fProductEnquiry.Close() : fProductEnquiry = Nothing
                    If Me.WindowState = FormWindowState.Normal Then
                        Me.Close()
                    End If
                ElseIf activecontrolname = "grdVoucher" Then
                    If msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up) Or msg.WParam.ToInt32() = CInt(Keys.Enter) Or msg.WParam.ToInt32() = CInt(Keys.F2) Or msg.WParam.ToInt32() = CInt(Keys.F3) Or msg.WParam.ToInt32() = CInt(Keys.F4) Then
                        If (msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up)) And plsrch.Visible = False Then GoTo ctn
                        grdVoucher_KeyDown(Nothing, New KeyEventArgs(keyData))
                        Return True
                    End If

                End If
            End If
ctn:
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub grdVoucher_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellClick
        If grdVoucher.RowCount = 0 Then Exit Sub
        If grdVoucher.CurrentRow Is Nothing Then Exit Sub
        If e.RowIndex < 0 Then Exit Sub
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
                    dtItms = ItmValidation(3, SrchText, True, "SR", Val(txtSuppAlias.Tag))
                    If dtItms.Rows.Count > 0 Then
                        found = True
                        AddDetails(dtItms)
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
                        If AllowUnitDiscountEntryOnInventory Then
                            .Item(ConstDisAmt, RowIndex).Value = Format(((CDbl(.Item(ConstActualPrice, RowIndex).Value) * Val(.Item(ConstDisP, RowIndex).Value)) / 100) * CDbl(.Item(ConstQty, RowIndex).Value), lnumFormat)
                        End If
                        calOthCost()
                        If EnableGST Then getGSTDetails(Trim(.Item(ConstBarcode, RowIndex).Value & ""), RowIndex, True)

                    End If
                    calculate(, True)
                Case ConstUPrice
                    If chgAmt Then
                        .Item(ConstActualPrice, RowIndex).Value = .Item(ConstUPrice, RowIndex).Value
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
                        Dim tx As Double
                        If Val(.Item(ConstTaxP, RowIndex).Value) = 0 Then .Item(ConstTaxP, RowIndex).Value = 0
                        tx = CDbl(.Item(ConstTaxP, RowIndex).Value) / 2
                        .Item(ConstCGSTP, RowIndex).Value = Format(tx, lnumFormat)
                        .Item(ConstSGSTP, RowIndex).Value = Format(tx, lnumFormat)
                        .Item(ConstIGSTP, RowIndex).Value = Format(CDbl(.Item(ConstCGSTP, RowIndex).Value), lnumFormat)
                        If EnableGST Then getGSTDetails(Trim(.Item(ConstBarcode, RowIndex).Value & ""), RowIndex, True)
                        calculate(, True)
                    End If
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
            Dim itemcost As Double
            If Val(DR(0)("CostAvg") & "") = 0 Then
                If Val(DR(0)("LastPurchCost") & "") = 0 Then
                    itemcost = DR(0)("opcost")
                Else
                    itemcost = DR(0)("LastPurchCost")
                End If
            Else
                itemcost = DR(0)("CostAvg")
            End If
            If UsrBr <> "" Then
                If Val(DR(0)("locationCost")) = 0 Then
                    If Val(DR(0)("locationLastCost") & "") > 0 Then
                        itemcost = DR(0)("locationLastCost")
                    End If
                Else
                    itemcost = DR(0)("locationCost")
                End If
            End If
            If itemcost = 0 Then
                itemcost = DR(0)("UnitPrice")
            End If
            If CDbl(.Item(ConstUPrice, i).Value) = 0 Or chgItm Then
                .Item(ConstActualPrice, i).Value = DR(0)("UnitPrice")
                .Item(ConstUPrice, i).Value = Format(CDbl(.Item(ConstActualPrice, i).Value), lnumFormat)
            End If
            If Not IsDBNull(DR(0)("isSerialNo")) Then
                .Item(ConstIsSerial, i).Value = IIf(DR(0)("isSerialNo"), 1, 0)
            Else
                .Item(ConstIsSerial, i).Value = 0
            End If
            If ShowTaxOnInventory Then
                If Val(DR(0)("vat") & "") = 0 Then
                    DR(0)("vat") = 0
                End If
                .Item(ConstTaxP, i).Value = Format(IIf(IsDBNull(DR(0)("vat")), 0, CDbl(DR(0)("vat"))), lnumFormat)
                .Item(ConstTaxAmt, i).Value = ((CDbl(.Item(ConstActualPrice, i).Value) * .Item(ConstTaxP, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value)
                .Item(ConstIGSTP, i).Value = IIf(IsDBNull(DR(0)("vat")), 0, CDbl(DR(0)("vat")))
                .Item(ConstcessAc, i).Value = IIf(IsDBNull(DR(0)("collectionAC")), 0, Val(DR(0)("collectionAC")))
            ElseIf EnableGST Then
                getGSTDetails(Trim(.Item(ConstBarcode, i).Value & ""), i, False)
            End If
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
            If enableFloodCess And Val(lblgstn.Tag & "") = 0 And cessenddate >= DateValue(cldrdate.Value) Then
                .Item(Constcess, i).Value = Format(IIf(IsDBNull(DR(0)("vat")), 0, CDbl(DR(0)("vat"))), lnumFormat)
                .Item(ConstcessAc, i).Value = IIf(IsDBNull(DR(0)("collectionAC")), 0, Val(DR(0)("collectionAC")))
                cessAmt = cessAmt + (((CDbl(.Item(ConstActualPrice, i).Value) * .Item(Constcess, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value))
                .Item(ConstcessAmt, i).Value = Format(cessAmt, lnumFormat)
            End If
            .Item(ConstBatchCost, i).Value = itemcost
            .Item(ConstLTotal, i).Value = Format(.Item(ConstQty, i).Value * CDbl(.Item(ConstUPrice, i).Value), lnumFormat)
            .Item(ConstNUPrice, i).Value = Format(CDbl(.Item(ConstUPrice, i).Value), lnumFormat)
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
    'Private Sub AddDetails(ByVal DR As DataTable)
    '    Dim i As Integer
    '    Dim PMult As Double
    '    With grdVoucher
    '        chgbyprg = True
    '        PMult = 1
    '        i = .CurrentRow.Index ' .RowCount - 1
    '        .Item(ConstSlNo, i).Value = i + 1
    '        .Item(ConstItemCode, i).Value = IIf(IsDBNull(DR(0)("Item Code")), Trim(DR(0)("Item Code") & ""), DR(0)("Item Code"))
    '        .Item(ConstDescr, i).Value = DR(0)("Description")
    '        .Item(ConstQty, i).Value = Format(1, lnumFormat) 'IIf(IsReturn, -1, 1)
    '        .Item(ConstItemID, i).Value = DR(0)("ItemId")
    '        .Item(ConstPMult, i).Value = 1 'IIf(IsNull(sRs(0)("PFraCount), "2", sRs(0)("PFraCount)
    '        If EnableGST Then
    '            .Item(ConstBarcode, i).Value = DR(0)("HSNCode") 'hsncode
    '        Else
    '            .Item(ConstBarcode, i).Value = ""
    '        End If
    '        If DR(0)("ItemCategory") = "Comment" Then
    '            onceChgFld = (CStr(.Item(ConstSlNo, i).Value) <> "M")
    '            .Item(ConstSlNo, i).Value = "M"
    '            .Item(ConstB, i).Value = 0
    '            .Item(ConstUnit, i).Value = ""
    '            .Item(ConstSerialNo, i).Value = ""
    '            .Item(ConstPMult, i).Value = "1"
    '            .Item(ConstPFraction, i).Value = "2"
    '            .Item(ConstImpDocId, i).Value = ""
    '            .Item(ConstImpLnId, i).Value = ""
    '        Else
    '            onceChgFld = (CStr(.Item(ConstSlNo, i).Value) = "M" Or CStr(.Item(ConstSlNo, i).Value) = "L")
    '            If onceChgFld Then
    '                .Item(ConstSlNo, i).Value = ""
    '                .Item(ConstBarcode, i).Value = ""
    '                .Item(ConstItemCode, i).Value = ""
    '            End If
    '        End If
    '        .Item(ConstB, .CurrentCell.RowIndex).Value = "B"
    '        .Item(ConstPFraction, i).Value = IIf(IsDBNull(DR(0)("FraCount")), "2", DR(0)("FraCount"))
    '        .Item(ConstUnit, i).Value = DR(0)("Unit")
    '        .Item(ConstWarrentyExpiry, i).Value = Format(DateAdd(DateInterval.Year, 1, DateValue(cldrdate.Value)), DtFormat)
    '        If CDbl(.Item(ConstUPrice, i).Value) = 0 Or chgItm Then
    '            '.Item(ConstActualPrice, i).Value = getPurchAmt(DR(0)("CostAvg"), Val(txtSuppAlias.Tag), Val(.Item(ConstItemID, .CurrentCell.RowIndex).Value))
    '            .Item(ConstActualPrice, i).Value = DR(0)("UnitPrice")
    '            .Item(ConstUPrice, i).Value = Format(CDbl(.Item(ConstActualPrice, i).Value), lnumFormat)
    '        End If
    '        If Not IsDBNull(DR(0)("isSerialNo")) Then
    '            .Item(ConstIsSerial, i).Value = IIf(DR(0)("isSerialNo"), 1, 0)
    '        Else
    '            .Item(ConstIsSerial, i).Value = 0
    '        End If
    '        'If Val(DR(0)("vat") & "") = 0 Then
    '        '    DR(0)("vat") = 0

    '        'End If
    '        '.Item(ConstTaxP, i).Value = Format(IIf(IsDBNull(DR(0)("vat")), 0, CDbl(DR(0)("vat"))), lnumFormat)
    '        '.Item(ConstTaxAmt, i).Value = ((CDbl(.Item(ConstActualPrice, i).Value) * .Item(ConstTaxP, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value)
    '        If ShowTaxOnInventory Then
    '            If Val(DR(0)("vat") & "") = 0 Then
    '                DR(0)("vat") = 0
    '            End If
    '            .Item(ConstTaxP, i).Value = Format(IIf(IsDBNull(DR(0)("vat")), 0, CDbl(DR(0)("vat"))), lnumFormat)
    '            .Item(ConstTaxAmt, i).Value = ((CDbl(.Item(ConstActualPrice, i).Value) * .Item(ConstTaxP, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value)
    '            .Item(ConstIGSTP, i).Value = IIf(IsDBNull(DR(0)("vat")), 0, CDbl(DR(0)("vat")))
    '            .Item(ConstcessAc, i).Value = IIf(IsDBNull(DR(0)("collectionAC")), 0, Val(DR(0)("collectionAC")))
    '        ElseIf EnableGST Then
    '            getGSTDetails(Trim(.Item(ConstBarcode, i).Value & ""), i, False)
    '        End If
    '        If enablecess And Val(lblgstn.Tag & "") = 0 And cessdate <= DateValue(cldrdate.Value) Then
    '            .Item(Constcess, i).Value = Format(IIf(IsDBNull(DR(0)("vat")), 0, CDbl(DR(0)("vat"))), lnumFormat)
    '            .Item(ConstcessAc, i).Value = IIf(IsDBNull(DR(0)("collectionAC")), 0, Val(DR(0)("collectionAC")))
    '            .Item(ConstcessAmt, i).Value = Format(((CDbl(.Item(ConstActualPrice, i).Value) * .Item(Constcess, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value), lnumFormat)
    '        End If
    '        .Item(ConstLTotal, i).Value = Format(.Item(ConstQty, i).Value * CDbl(.Item(ConstUPrice, i).Value), lnumFormat)
    '        .Item(ConstNUPrice, i).Value = Format(CDbl(.Item(ConstUPrice, i).Value), lnumFormat)

    '        .Item(ConstQty, i).Value = Format(CDbl(.Item(ConstQty, i).Value), "#,##0" & IIf(Val(.Item(ConstPFraction, i).Value) = 0, "", "." & Strings.StrDup(CByte(.Item(ConstPFraction, i).Value), "0")))
    '        .Item(ConstImpDocId, i).Value = ""
    '        .Item(ConstImpLnId, i).Value = ""
    '        chgAmt = True
    '        chgItm = False
    '        .ClearSelection()
    '    End With
    '    calculate(, True)
    '    chgbyprg = False
    'End Sub

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
                Case ConstQty, ConstTaxP, ConstDisAmt, ConstDisP, ConstWoodDiscQty, ConstWoodQty
                    '.Item(ConstLTotal, i).Value = Format((.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - (.Item(ConstQty, i).Value * .Item(ConstDisAmt, i).Value) + (.Item(ConstQty, i).Value * .Item(ConstTaxAmt, i).Value), lnumFormat)
                    '.Item(ConstNUPrice, i).Value = CDbl(.Item(ConstActualPrice, i).Value) - .Item(ConstDisAmt, i).Value + .Item(ConstTaxAmt, i).Value
                    chgAmt = True
                Case ConstLTotal
                    If Val(grdVoucher.Item(ConstQty, i).Value) > 0 Then
                        If Val(grdVoucher.Item(ConstDisAmt, i).Value) = 0 And CDbl(grdVoucher.Item(ConstTaxAmt, i).Value) = 0 Then 'Not AllowUnitDiscountEntryOnInventory And Not ShowTaxOnInventory And
                            chgbyprg = True
                            grdVoucher.Item(ConstUPrice, i).Value = Format(CDbl(grdVoucher.Item(ConstLTotal, i).Value) / CDbl(grdVoucher.Item(ConstQty, i).Value), lnumFormat)
                            grdVoucher.Item(ConstActualPrice, i).Value = CDbl(grdVoucher.Item(ConstLTotal, i).Value) / CDbl(grdVoucher.Item(ConstQty, i).Value)
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
                    If grdVoucher.Item(ConstSerialNo, grdVoucher.CurrentRow.Index).Value <> "" And enableSerialnumber Then
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
                chgbyprg = True
                grdVoucher.Item(ConstqtyChg, grdVoucher.CurrentRow.Index).Value = "Chg"
                chgbyprg = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub ShowPanel()
        If Not _srchOnce Then
            chgbyprg = True
            Dim PopupLoc As Point
            Dim x As Integer '= Me.Width - plsrch.Width - 100
            Dim y As Integer '= Me.Height - plsrch.Height - 100
            'PopupLoc = New Point(x, y)
            
            plsrch.Height = 300
            plsrch.Width = 700
            x = grdVoucher.Left + grdVoucher.Width - plsrch.Width
            y = grdVoucher.Top
            PopupLoc = New Point(x, y)
            If plsrch.Visible = False Then
                plsrch.Location = PopupLoc
                plsrch.Visible = True
            End If
        End If
        SearchProductPanel(grdSrch, "Item Code", strGridSrchString, True)
        If grdSrch.RowCount > 0 And strGridSrchString = "" Then
            strGridSrchString = grdSrch.Item(0, 0).Value
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
            ElseIf e.KeyCode = Keys.F1 Then
                grdBeginEdit()
            ElseIf e.KeyCode = Keys.F2 Then
                If plsrch.Visible Then plsrch.Visible = False
                If grdVoucher.RowCount = 0 Then Exit Sub
                Select Case grdVoucher.CurrentCell.ColumnIndex
                    Case ConstItemCode
                        If Not fMList Is Nothing Then fMList.Visible = False
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
    Private Sub doSelect(ByVal Mup As Integer)
        Try
            chgbyprg = True
            Dim ItmFlds() As String
            If grdSrch.RowCount = 0 Then Exit Sub
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
        txtImpPrefix.Text = ""
        txtImpPrefix.Tag = ""
        txtSuppName.Enabled = True
        txtJob.Text = ""
        txtJob.Tag = ""
        txtDOLst.Text = ""
        txtDOLst.Tag = ""
        cmbdeliveredBy.Text = ""
        chgDoc = False
        chktaxwithoutLinediscount.Checked = False
        'fMainForm.lblUser.Text = CurrentUser
        'fMainForm.lblModiUser.Text = ""
        OthCost = 0
        numDisc.Text = Format(0, lnumFormat)
        cmbsalesman.Text = ""
        OthCost = 0
        CTVol = 0
        CTWt = 0
        CTQty = 0
        loadedTrId = 0
        LddImpDocs = ""
        txtroundOff.Text = Format(0, lnumFormat)
        cmbsign.SelectedIndex = 0
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
        chgbyprg = False
        chgUprice = False
        numVchrNo.Focus()
        lbladd1.Text = ""
        lbladd2.Text = ""
        lbladd3.Text = ""
        lbladd4.Text = ""
        lbladd5.Text = ""
        lbladd6.Text = ""
        lbladd7.Text = ""
        lbltrdate.Text = ""
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
            If userType Then
                btnupdate.Tag = IIf(getRight(80, CurrentUser), 1, 0)
                btndelete.Tag = IIf(getRight(81, CurrentUser), 1, 0)
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
        calculate()
        CalculateGST(True)
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
            MsgBox("Invalid Reference", MsgBoxStyle.Exclamation)
            txtReference.Focus()
            Exit Sub
        End If
        If DateValue(cldrdate.Value) <= ProtectUntil Then
            MsgBox("Voucher Date comes within Protected Date Range !!", MsgBoxStyle.Exclamation)
            cldrdate.Focus()
            Exit Sub
        End If
        _vAcMaster = _objcmnbLayer._fldDatatable(StrAccMastSrch & " WHERE Alias = '" & txtSuppAlias.Text & "' ORDER BY AccDescr")
        If _vAcMaster.Rows.Count = 0 Then
            MsgBox("Enter a valid  Customer Account !!", vbExclamation)
            txtSuppName.Focus()
            'txtSuppAlias.Focus()
            Exit Sub
        Else
            txtSuppAlias.Tag = _vAcMaster(0)("accid")
        End If
        If Val(txtPurchAlias.Tag) = 0 Then
            MsgBox("Purchase A/C could not found!", MsgBoxStyle.Exclamation)
            cmbVoucherTp.Focus()
            Exit Sub
        End If
        'If Not isModi Then
        '    If chekDuplicate() Then Exit Sub
        'End If
        'Dim dt As DataTable
        'dt = _objcmnbLayer._fldDatatable("SELECT Trid from ItmInvcmntb where cscode=" & Val(txtSuppAlias.Tag) & " and TrRefNo='" & txtReference.Text & "' and Trid<>" & loadedTrId)
        'If dt.Rows.Count > 0 Then
        '    MsgBox("Reference already exist!", MsgBoxStyle.Exclamation)
        '    txtReference.Focus()
        '    Exit Sub
        'End If
        'If CheckRestrictImport() = False Then Exit Sub
        If Not chkGridvalue() Then Exit Sub
        If enableBranch And UsrBr = "" Then
            MsgBox("Transaction cannot be saved without Branch! Please login with Branch", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If CDbl(lblNetAmt.Text) < 0 Then
            MsgBox("Net Amount below Zero is not allowed !!!?", vbExclamation)
            MyActiveControl = numDisc
            Exit Sub
        End If
        If MsgBox("Verification is succeeded. Do you like to file it ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = vbYes Then
            'Dim frm As New WaitMessageFrm
            'frm.Show()
            'If Not fwait Is Nothing Then fwait.Close() : fwait = Nothing
            'fwait = New WaitMessageFrm
            'fwait.Show()
            'saveTrans()
            'loadWaite(1)
            If enableMultipleDebitAutoPopulate And (vtype = "Credit" Or vtype = "") And isModi = False And enableMultipleDebitInInvoice And isfromPos = False Then
                If Not showMultipleDebits() Then Exit Sub
                saveTrans()
            Else
                'If MsgBox("Verification is succeeded. Do you like to file it ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = vbYes Then
                '    'saveTrans()

                'End If
                loadWaite(1)
            End If

        End If
    End Sub


    Private Sub saveTrans()
       
        Dim TrId As Long
        Dim TDrAmt As Double
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
            If Not CheckNoExists(txtprefix.Text, Val(numVchrNo.Text), "SR", "Inventory") Then
                If MsgBox("Document No alreary exist! Do you want to fill next?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Exit Sub
                Else
                    numVchrNo.Text = Val(numVchrNo.Text) + 1
                    GoTo chkagain
                End If
            End If
        End If
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
        Dim itemidsdatatable As New DataTable
        itemidsdatatable.Columns.Add(New DataColumn("Itemid", GetType(Long)))
        TDrAmt = saveInvTr(TrId)
        Dim strqry As String = ""
        If isModi Then
            _objInv.deleteInventoryRelatedItemDetails(loadedTrId)
        End If
        'Dim dtr As DataRow
        UpdateAccounts(TrId, TDrAmt, DiscAcc)
        If isModi = False Then
            numPrintVchr.Text = numVchrNo.Text
            numVchrNo.Tag = Val(numVchrNo.Text) 'Trim(CMNREC(12).FormattedText)
            txtprefix.Tag = txtprefix.Text
            txtPPrefix.Text = txtprefix.Text
            'txtprefix.Text = SetNextVrNo(numVchrNo, Val(cmbVoucherTp.Tag), "SR", "TrType = 'SR' AND InvNo = ", False, True, True)
        End If
        ChgId = False
        chgPost = False
        If isModi Then
            btnModify_Click(btnModify, New System.EventArgs)
        Else
            If isfromPos Then
                If MsgBox("Do you want to set SR Amount as deduction?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Dim ref As String
                    ref = txtprefix.Text & IIf(txtprefix.Text = "", "", "/") & numVchrNo.Text
                    RaiseEvent returnSR(ref, CDbl(lblNetAmt.Text))
                    Me.Close()
                End If
            End If
            
            AddNewClick()
        End If
        If Not fwait Is Nothing Then fwait.Close()
        MsgBox("Sales Return is succesfully posted with Vr. # " & numPrintVchr.Text & ".", MsgBoxStyle.Information)
        numVchrNo.Tag = ""
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

        LinkNo = 0
        'Debit Entry
        Dim Dramt As Double
        Dim ttlTxAmount As Double
        Dim TxAmount As Double
        Dim i As Integer = 0
        Dramt = (TDrAmt - IIf(DiscAcc > 0, CDbl(numDisc.Text), 0)) * FCRt
        Dim crTrRef As String = Strings.Left(Trim(txtDescr.Text) & IIf(txtDescr.Text = "", " Sales /", "/ Sales / ") & txtSuppName.Text, 249)
        If Not dtTax Is Nothing Then
            For i = 0 To dtTax.Rows.Count - 1
                If Val(dtTax(i)("ACid")) > 0 And Val(dtTax(i)("Amount")) > 0 Then
                    TxAmount = Format(CDbl(dtTax(i)("Amount")), numFormat)
                    ttlTxAmount = Format(ttlTxAmount + TxAmount, numFormat)
                End If
            Next
        End If
        
        Dramt = CDbl(lblTotAmt.Text)
        Dramt = IIf(DiscAcc = 0, CDbl(lblNetAmt.Text) - CDbl(lbltax.Text) - CDbl(lblcess.Text), Dramt)
        Dim total As Double = Dramt + ttlTxAmount
        total = CDbl(lblNetAmt.Text) - total
        Dramt = Dramt + total
        setAcctrDetValue(LinkNo, Val(txtPurchAlias.Tag), Trim(txtReference.Text), crTrRef, Dramt * FCRt, txtJob.Text, "", 0, 0, "", _
                             "", Val(txtSuppAlias.Tag), "", cmbfc.Text, FCRt)

        'Tax Entry Debit

        If chktaxInv.Checked Then
            For i = 0 To dtTax.Rows.Count - 1
                If Val(dtTax(i)("ACid")) > 0 And Val(dtTax(i)("Amount")) > 0 Then
                    TxAmount = Math.Round(CDbl(dtTax(i)("Amount")), NoOfDecimal)
                    setAcctrDetValue(LinkNo, Val(dtTax(i)("ACid")), Trim(txtReference.Text), dtTax(i)("AccountName") & " Tax Paid", TxAmount * FCRt, Strings.Left(txtJob.Text, 50), "", 0, 0, "", "", Val(txtSuppAlias.Tag), Val(txtSuppAlias.Tag) & txtReference.Text, cmbfc.Text, FCRt)
                    '_objTr.saveAccTrans()
                End If
            Next
        End If
        'Credit Entry
        Dim EntRef As String = Strings.Left(Trim(txtDescr.Text) & IIf(Trim(txtDescr.Text) = "", "", " / ") & txtPurchaseName.Text, 249)
        Dim dlAmt As Double = CDbl(lblNetAmt.Text) * FCRt
        dlAmt = dlAmt * -1
        setAcctrDetValue(LinkNo, Val(txtSuppAlias.Tag), Trim(txtReference.Text), EntRef, dlAmt, "", Strings.Left(txtJob.Tag, 50), 0, 0, "", _
                         "", Val(txtSuppAlias.Tag), Val(txtSuppAlias.Tag) & txtReference.Text, cmbfc.Text, FCRt)
        '_objTr.saveAccTrans()


        'DiscountEntry
        'DiscAcc = getConstantAccounts(3)
        If (CDbl(numDisc.Text)) > 0 And DiscAcc > 0 Then
            dlAmt = -1 * (CDbl(numDisc.Text)) * FCRt
            setAcctrDetValue(LinkNo, DiscAcc, Trim(txtReference.Text), Trim(txtDescr.Text), dlAmt, "", "", 2, 0, "", _
                            "", Val(txtSuppAlias.Tag), DiscAcc & txtReference.Text, cmbfc.Text, FCRt)
            '_objTr.saveAccTrans()
        End If
        Dim customername As String
        Dim mcredit As Double
        If Not enableMultipleDebitAsCreditCollection Then
            'collection udjustment as sales transaction without RV
            If Not dtMultipleDebits Is Nothing Then
                If enableMultipleDebitInInvoice And dtMultipleDebits.Rows.Count > 0 Then
                    If dtMultipleDebits.Rows.Count > 0 Then
                        'Multiple Entry
                        If txtSuppName.Text = "" Then
                            customername = txtSuppName.Text
                        Else
                            customername = txtSuppName.Text
                        End If
                        Dim collectionRef As String = ""
                        For j = 0 To dtMultipleDebits.Rows.Count - 1
                            If CDbl(dtMultipleDebits(j)("accAmt")) < 0 Or enableMultipleDebitAsCreditCollection Then
                                collectionRef = "Paid On " & Trim(txtprefix.Text) & IIf(txtprefix.Text = "", "", "/") & numVchrNo.Text & "[" & txtSuppName.Text & _
                                " Ref: " & dtMultipleDebits(j)("reference") & "]"
                            Else
                                collectionRef = "PAID On " & Trim(txtprefix.Text) & IIf(txtprefix.Text = "", "", "/") & numVchrNo.Text & "[Ref: " & dtMultipleDebits(j)("reference") & "]"
                            End If
                            setAcctrDetValue(LinkNo, Val(dtMultipleDebits(j)("accid") & ""), dtMultipleDebits(j)("reference"), collectionRef, CDbl(dtMultipleDebits(j)("accAmt")) * FCRt, "", Strings.Left(txtJob.Text, 50), 0, 0, "", _
                                 "", Val(txtSuppAlias.Tag), Val(txtSuppAlias.Tag) & dtMultipleDebits(j)("reference"), cmbfc.Text, FCRt)
                            '_objTr.saveAccTrans()
                            mcredit = mcredit + CDbl(dtMultipleDebits(j)("accAmt"))
                        Next
                    End If
                End If
            End If
        End If
        If enableMultipleDebitInInvoice Then saveMultipleDebits(TrId)

        Dim reference As String = Trim(txtprefix.Text) & IIf(txtprefix.Text = "", "", "/") & numVchrNo.Text
        If Not dtSetoffTable Is Nothing Then
            If enableMultipleDebitInInvoice And dtSetoffTable.Rows.Count > 0 Then
                Dim _qurey As EnumerableRowCollection(Of DataRow)
                _qurey = From data In dtSetoffTable.AsEnumerable() Where data("Tag") = "Y" Select data
                If _qurey.Count > 0 Then
                    dtSetoffTable = _qurey.CopyToDataTable()
                Else
                    dtSetoffTable = dtAcc.Clone
                End If
                Dim balance As Double
                For i = 0 To dtSetoffTable.Rows.Count - 1
                    balance = CDbl(dtSetoffTable(i)("DealAmt")) - CDbl(dtSetoffTable(i)("Assign"))
                    If balance > 0 Then
                        setAcctrDetValue(Val(dtSetoffTable(i)("linkno")), Val(dtSetoffTable(i)("accountno")), "ON/AC", Trim(dtSetoffTable(i)("EntryRef")), balance * -1, "", "", 0, 0, "", _
                               "", Val(txtSuppAlias.Tag), "", dtSetoffTable(i)("CurrencyCode"), dtSetoffTable(i)("CurrRate"), , Val(dtSetoffTable(i)("UnqNo")))

                        setAcctrDetValue(Val(dtSetoffTable(i)("linkno")), Val(dtSetoffTable(i)("accountno")), reference, Trim(dtSetoffTable(i)("EntryRef")), CDbl(dtSetoffTable(i)("Assign")) * -1, "", "", 0, 0, "", _
                               "", Val(txtSuppAlias.Tag), "", dtSetoffTable(i)("CurrencyCode"), dtSetoffTable(i)("CurrRate"))
                    Else
                        setAcctrDetValue(Val(dtSetoffTable(i)("linkno")), Val(dtSetoffTable(i)("accountno")), reference, Trim(dtSetoffTable(i)("EntryRef")), CDbl(dtSetoffTable(i)("Assign")) * -1, "", "", 0, 0, "", _
                               "", Val(txtSuppAlias.Tag), "", dtSetoffTable(i)("CurrencyCode"), dtSetoffTable(i)("CurrRate"), , Val(dtSetoffTable(i)("UnqNo")))
                    End If
                Next
            End If
        End If
        updateStockTransaction(TrId, LinkNo)
        _objTr.SaveAccTrWithDt(dtAccTb)

        'updateClosingBalanceForInvoice(TrId)
    End Sub
    Private Sub setInvCmnValue(ByVal InvTrid As Long)
        With _objInv
            Dt = DateValue(Date.Now)
            .TrId = InvTrid
            .TrDate = DateValue(cldrdate.Value)
            .TrType = "SR"
            .DocLstTxt = ""
            .Prefix = Trim(txtprefix.Text)
            .InvNo = Val(numVchrNo.Text)
            .TrRefNo = Trim(txtReference.Text)
            .CSCode = Val(txtSuppAlias.Tag)
            .PSAcc = Val(txtPurchAlias.Tag)
            .JobCode = txtJob.Text
            .ImpDoc = 0
            .UserId = CurrentUser
            .IsFC = 0
            .FCRate = FCRt
            .NFraction = NDec
            .FC = cmbfc.Text
            .Discount = CDbl(numDisc.Text) * FCRt
            .TrDescription = Trim(txtDescr.Text)
            .TypeNo = TrTypeNo ' getVouchernumber("SR")
            .EnaJob = False
            .DocDefLoc = IIf(cmblocation.Text = "", Dloc, cmblocation.Text)
            .BrId = UsrBr
            .OthCost = OthCost
            .Discount1 = 0
            .NetAmt = CDbl(lblNetAmt.Text) * FCRt
            .LPO = ""
            .RetInvIds = Val(txtDOLst.Tag)
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
            .TaxType = Val(lblstatecode.Tag)
            .InvTypeNo = Val(cmbVoucherTp.Tag)
            .SlsManId = cmbsalesman.Text
            .isTaxInvoice = chktaxInv.Checked
            .isImportOrExport = IIf(chkexport.Checked, 1, 0)
            .taxwithoutLineDiscount = IIf(chktaxwithoutLinediscount.Checked, 1, 0)
            .deliveredBy = cmbdeliveredBy.Text
            If Val(txtroundOff.Text) = 0 Then txtroundOff.Text = Format(0, numFormat)
            .rndoff = txtroundOff.Text * IIf(cmbsign.SelectedIndex = 1, -1, 1)
        End With

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
                dtrow("SlNo") = i + 1
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
                If Val(lblgstn.Tag & "") = 0 Then
                    dtrow("FloodcessAmt") = (CDbl(.Item(ConstFloodCessAmt, i).Value) * FCRt)
                Else
                    dtrow("FloodcessAmt") = 0
                End If

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
skip:
            Next

        End With
        _objInv.savebulktoInvTr(dtInvTb)
        Return TDrAmt
    End Function
    Private Sub setInvDetValue(ByVal Invid As Long, ByVal PPerU As Single, ByVal i As Integer)
        With grdVoucher

            _objInv.dtTrId = Invid
            _objInv.ItemId = IIf(.Item(ConstSlNo, i).Value.ToString <> "M", Val(.Item(ConstItemID, i).Value), 0)
            _objInv.Unit = .Item(ConstUnit, i).Value
            _objInv.taxP = CDbl(.Item(ConstTaxP, i).Value)
            _objInv.taxAmt = CDbl(.Item(ConstTaxAmt, i).Value) * FCRt
            _objInv.TrQty = CDbl(.Item(ConstQty, i).Value) * PPerU
            _objInv.focqty = CDbl(.Item(ConstFocQty, i).Value) * PPerU

            _objInv.UnitCost = CDbl(.Item(ConstActualPrice, i).Value) * FCRt / PPerU 'CDbl(.TextMatrix(i, 10)) / PPerU
            _objInv.PFraction = PPerU
            If Val(.Item(ConstUnitOthCost, i).Value) = 0 Then .Item(ConstUnitOthCost, i).Value = 0
            _objInv.UnitOthCost = CDbl(.Item(ConstUnitOthCost, i).Value) * FCRt / PPerU
            _objInv.Method = .Item(ConstB, i).Value
            _objInv.UnitDiscount = Val(.Item(ConstDiscOther, i).Value) * FCRt / PPerU

            '***********
            If Val(.Item(ConstDisAmt, i).Value & "") = 0 Then .Item(ConstDisAmt, i).Value = 0
            _objInv.ItemDiscount = CDbl(.Item(ConstDisAmt, i).Value) * FCRt / PPerU
            If Val(.Item(ConstDisP, i).Value) = 0 Then .Item(ConstDisP, i).Value = 0
            _objInv.DisP = CDbl(.Item(ConstDisP, i).Value) * FCRt / PPerU
            '***********

            If Not IsDBNull(.Item(ConstDescr, i).Value) Then
                _objInv.IDescription = IIf(.Item(ConstSlNo, i).Value.ToString = "M", .Item(ConstDescr, i).Value, Trim(.Item(ConstDescr, i).Value))
            End If
            _objInv.SlNo = i + 1
            _objInv.TrTypeNo = getVouchernumber("SR")
            _objInv.TrDateNo = getDateNo(CDate(cldrdate.Value))
            _objInv.TrType = "SR"
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
                If Val(.Item(ConstcessAmt, i).Value & "") = 0 Then .Item(ConstcessAmt, i).Value = 0
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
        _objTr.JVType = "SR"
        _objTr.JVDate = DateValue(cldrdate.Value)
        _objTr.PreFix = Trim(txtprefix.Text)
        _objTr.JVNum = Val(numVchrNo.Text)
        _objTr.JVTypeNo = TrTypeNo 'getVouchernumber("SR")
        _objTr.UserId = Trim(CurrentUser & "")
        _objTr.MchName = MACHINENAME
        _objTr.CrtDtTm = Dt
        _objTr.TypeNo = 0 'PREFIXTB ID
        '_objTr.VrDescr = Trim(txtpayee.Text)
        _objTr.IsModi = IIf(LinkNo > 0, 1, 0)
        _objTr.LinkNo = InvTrid
        _objTr.taxablevalue = IIf(chkexport.Checked, CDbl(lblNetAmt.Text), CDbl(lbltaxable.Text))
        _objTr.taxvalue = CDbl(lbltax.Text)
    End Sub
    Private Sub setAcctrDetValue(ByVal lnkNo As Long, ByVal jbIndex As Integer)
        With _objTr
            .trLinkNo = lnkNo
            .AccountNo = JobAcc(jbIndex).Acc
            .Reference = Trim(txtReference.Text)
            .EntryRef = Strings.Left(Trim(txtDescr.Text) & IIf(JobAcc(jbIndex).Job = "", "", " / " & JobAcc(jbIndex).Job) & " / " & txtSuppName.Text, 249)
            .DealAmt = JobAcc(jbIndex).Amt * FCRt
            .FCAmt = JobAcc(jbIndex).Amt * FCRt
            .JobCode = JobAcc(jbIndex).Job
            .JobStr = ""
            .CurrRate = FCRt
            .CurrencyCode = ""
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
                                  ByVal CurrencyCode As String, ByVal CurrRate As Double, Optional ByVal vatcode As String = "", Optional ByVal UnqNo As Long = 0)
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

    Private Sub SalesReturnInvoice_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
        If Not fSelect Is Nothing Then fSelect.Close() : fSelect = Nothing
        If Not fProductEnquiry Is Nothing Then fProductEnquiry.Close() : fProductEnquiry = Nothing
        If Not fSlctDoc Is Nothing Then fSlctDoc.Close() : fSlctDoc = Nothing
        If Not fSerialno Is Nothing Then fSerialno.Close() : fSerialno = Nothing
    End Sub

    Private Sub SalesOrderFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            loadInventoryFormLoadMasters(True, 8, "SR", 3, DiscAcc, TrTypeNo)
            StrAccMastSrch = "SELECT AccDescr, Alias, ClosingBal As Balance, OpnBal, AccountNo, S1AccHd.S1AccId, AccSetId, GrpSetOn,accid FROM" & _
                            " AccMast INNER JOIN S1AccHd ON S1AccHd.S1AccId = AccMast.S1AccId "
            'btnNext_Click(btnNext, New System.EventArgs())
            lnumFormat = numFormat
            FCRt = 1
            SetGridHead()
            chkautoroundOff.Checked = enableAutoRoundOff
            'txtdp.Enabled = Not enableAdjustDiscountOnTaxTotal
            If ShowTaxOnInventory Then
                CreateTaxTable(dtTax)
                chktaxInv.Checked = True
            ElseIf EnableGST Then
                CreateTaxTable(dtTax)
                chktaxInv.Checked = True
                If withNonTaxBill Then
                    chktaxInv.Checked = False
                End If
            Else
                chktaxInv.Checked = False
                chktaxInv.Visible = False
            End If
            If enableDeliverywiseOutstanding Then
                cmbdeliveredBy.Visible = True
                lbldelivery.Visible = True
                'AddtoCombo(cmbdeliveredBy, "SELECT SManCode FROM SalesmanTb", True, False)
                AddDttoCombo(cmbdeliveredBy, dtsalesman, True, False)
            Else
                cmbdeliveredBy.Visible = False
                lbldelivery.Visible = False
            End If
            If enableMultipleDebitInInvoice Then
                'loadSalesMultipleDebits(0)
                createMultipleDebitTb(dtMultipleDebits)
                btnmultipleDebit.Visible = Not isfromPos
            End If
            crtSubVrs(cmbVoucherTp, 8, True)
            'dtTax = _objcmnbLayer._fldDatatable("SELECT vatcode,convert(decimal,0) Amount,collectionAC,paymentAC,vat From VatMasterTb", False)
            'AddtoCombo(cmbsalesman, "SELECT SManCode FROM SalesmanTb", True, False)
            'AddtoCombo(cmblocation, "SELECT LocCode FROM LocationTb", True, False)
            AddDttoCombo(cmbsalesman, dtsalesman, True, False)
            AddDttoCombo(cmblocation, dtlocationTb, True, False)
            _objcmnbLayer.dtSerialNo = createdtSerialNo()
            OthCost = 0
            chgbyprg = True
            LodCurrency()
            'calculate()
            Me.Text = "Sales Return "
            cldrdate.Value = Format(Date.Now, DtFormat)
            NDec = NoOfDecimal
            loadedTrId = 0
            chgbyprg = False
            'cmbVrType_SelectedIndexChanged(sender, e)
            'numVchrNo.Select()
            ChgId = False
            If isModi Then
                btnModify_Click(btnModify, New System.EventArgs)
                If userType Then
                    btnupdate.Tag = IIf(getRight(80, CurrentUser), 1, 0)
                    btndelete.Tag = IIf(getRight(81, CurrentUser), 1, 0)
                Else
                    btnupdate.Tag = 1
                    btndelete.Tag = 1
                End If

                btndelete.Text = "Delete"
            Else
                'AddNewClick()
                If userType Then
                    btnupdate.Tag = IIf(getRight(79, CurrentUser), 1, 0)
                Else
                    btnupdate.Tag = 1
                End If
                btndelete.Text = "Clear"
                btndelete.Tag = 1
            End If
            Timer1.Enabled = True
            cessdate = getCessDate()
            If enableGCC Then
                lblgstn.Text = "TIN : "
                lblstatecode.Text = "Emirate : "
                chkexport.Visible = True
                chktaxInv.Visible = True
            End If
            chkcal.Checked = calcluatetaxFrompriceInv
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
        dtTable = _objcmnbLayer._fldDatatable("SELECT * FROM ItmInvCmnTb WHERE TrType = 'SR' AND InvNo = " & Val(numVchrNo.Text) & " AND PreFix = '" & MkDbSrchStr(Trim(txtprefix.Text), True) & "'")
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
            'dtSrlNo = _objcmnbLayer._fldDatatable("SELECT SerialNo,itemid FROM ItmInvTrTb Left Join ItmInvCmnTb on ItmInvTrTb.Trid=ItmInvCmnTb.trid WHERE TrType='SR' AND ItmInvCmnTb.Trid<>" & loadedTrId)
            'dtExstSrlno = _objcmnbLayer._fldDatatable("SELECT SerialNo,ItemId FROM serialnotb ")
            'Dim _qurey As EnumerableRowCollection(Of DataRow)

            For r = 0 To .RowCount - 1 '- 1
                If Not enableSerialnumber And .Item(ConstIsSerial, r).Value = 1 Then .Item(ConstIsSerial, r).Value = 0
                If .Item(ConstIsSerial, r).Value = 1 And .Item(ConstSerialNo, r).Value = "" And enableSerialnumber Then
                    .Rows(r).Selected = True
                    .CurrentCell = .Item(ConstQty, r)
                    MsgBox("Serial Number cannot be Blank !!", vbExclamation)
                    .FirstDisplayedScrollingRowIndex = r
                    GoTo Ter
                End If
                'If .Item(ConstSerialNo, r).Value <> "" Then
                '    _qurey = From data In dtExstSrlno.AsEnumerable() Where data(0) = .Item(ConstSerialNo, r).Value And data(1) = .Item(ConstItemID, r).Value Select data
                '    If _qurey.Count = 0 Then
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
                '    If _qurey.Count > 0 Then
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

    Private Sub fProductEnquiry_CreateItem() Handles fProductEnquiry.CreateItem
        If fproductMast Is Nothing Then
            fproductMast = New ItemMastFrm
            fproductMast.MdiParent = fMainForm
            fproductMast.IsFromEnqry = True

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
        grdVoucher.CurrentCell = grdVoucher.Item(3, grdVoucher.CurrentRow.Index)
        grdBeginEdit()
        chgbyprg = False
    End Sub


    Private Sub txtroundOff_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        txtReference.Focus()
    End Sub

    Private Sub numDisc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles numDisc.KeyDown
        If e.KeyCode = Keys.Return Then
            btnupdate.Focus()
        End If
    End Sub


    Private Sub numDisc_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles numDisc.KeyPress, txtfcrt.KeyPress
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
            numCtrl.Text = Format(Val(numCtrl.Text), lnumFormat)
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
        Valid(grdVoucher.CurrentCell.RowIndex, grdVoucher.CurrentCell.ColumnIndex)
        chgbyprg = False
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

    Private Sub LodCurrency()
        'Dim dt As DataTable = _objcmnbLayer._fldDatatable("SELECT CurrencyCode FROM CurrencyTb", False)

        cmbfc.Items.Clear()
        cmbfc.Items.Add("")
        Dim i As Integer
        For i = 0 To dtcurrentyTb.Rows.Count - 1
            cmbfc.Items.Add(dtcurrentyTb(i)("CurrencyCode"))
        Next
    End Sub

    Private Sub returnFcrt()
        Dim dt As DataTable = _objcmnbLayer._fldDatatable("Select CurrencyRate,[Decimal Places] from CurrencyTb where CurrencyCode='" & cmbfc.Text & "'", False)
        If (dt.Rows.Count > 0) Then
            NDec = dt(0)("Decimal Places")
            lnumFormat = "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))
            txtfcrt.Text = Format(CDbl(dt(0)("CurrencyRate")), lnumFormat)
        Else
            txtfcrt.Text = Format(0, lnumFormat)
            NDec = NoOfDecimal
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
        IsReturn = False
        With fSlctDoc
            .strType = "SR"
            .Text = "Select Purchase Invoice"
            .ShowDialog()
        End With
        If Not fSlctDoc Is Nothing Then fSlctDoc = Nothing
    End Sub

    Private Sub fSlctDoc_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fSlctDoc.FormClosed
        fSlctDoc = Nothing
    End Sub
    Private Sub fSlctDoc_closetr() Handles fSlctDoc.closetr
        AddNewClick()
    End Sub


    Private Sub fSlctDoc_selectTr(ByVal trid As Long, ByVal TrType As String) Handles fSlctDoc.selectTr
        If IsReturn Then
            PasteFrom(trid)
        Else
            CheckNLoad(trid)
        End If
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
            fRptFormat.RptType = "SR"
            fRptFormat.ShowDialog()
            fRptFormat = Nothing
        Else
            PrepareRpt("SR")
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
        NextNumber()
        'btnNext_Click(btnNext, New System.EventArgs)
        enableCtrls(False)
        btnModify.Text = "&Modify"
        btndelete.Text = "Clear"
        isModi = False
        btnSlct.Visible = False
        numVchrNo.ReadOnly = True
        txtReference.Select()
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

            Dim itemidsdatatable As New DataTable
            Dim trdate As Date
            itemidsdatatable = _objcmnbLayer._fldDatatable("SELECT Itemid,TrDate,SerialNo from ItmInvTrTb LEFT JOIN ItmInvCmnTb ON ItmInvTrTb.Trid=ItmInvCmnTb.Trid  WHERE ItmInvTrTb.TrId =" & loadedTrId)
            trdate = DateValue(itemidsdatatable(0)("TrDate"))
            _objInv.TrId = loadedTrId
            _objInv.TrType = "OUT"
            _objInv.deleteInventoryTransactions()
            For i = 0 To itemidsdatatable.Rows.Count - 1
                '_objcmnbLayer._saveDatawithOutParm("UPDATE SerialNoTb SET PurchaseId=0 WHERE SerialNo='" & itemidsdatatable(i)("SerialNo") & "'")
                '_objcmnbLayer._saveDatawithOutParm("delete from SerialNoTb where ISNULL(PurchaseId,0)=0 and ISNULL(SalesId,0)=0")
                '_objcmnbLayer._saveDatawithOutParm("delete from WarrentyTrTb where SerialNo='" & itemidsdatatable(i)("SerialNo") & "'")

                '_objInv.ItemId = itemidsdatatable(i)("Itemid")
                '_objInv.TrDate = DateValue(itemidsdatatable(i)("TrDate"))
                '_objInv.setcostAverageOnModification(UsrBr)
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
        dt = _objcmnbLayer._fldDatatable("SELECT Alias,AccDescr,ACCID FROM AccMast WHERE AccountNO=" & AccountNo)
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
        setCustomer()
        chgbyprg = False
    End Sub
    Private Sub setCustomer(Optional ByVal accid As Long = 0)
        Dim dt As DataTable
        If accid > 0 Then
            dt = _objcmnbLayer._fldDatatable("SELECT AccMast.accid,Alias,AccDescr,Address1,Address2,Address3,Address4," & _
                                         "Phone,Fax,EMail,PT1,PT2,PT3,PT4,TrdLcno,TrdDate,CreditLimit,DueDays,SlsmanId,CurrencyCode,CountryCode,GSTIN " & _
                                         " from AccMast LEFT JOIN AccMastAddr ON AccMast.accid=AccMastAddr.AccountNo where accid=" & accid)
        Else
            dt = _objcmnbLayer._fldDatatable("SELECT AccMast.accid,Alias,AccDescr,Address1,Address2,Address3,Address4," & _
                                         "Phone,Fax,EMail,PT1,PT2,PT3,PT4,TrdLcno,TrdDate,CreditLimit,DueDays,SlsmanId,CurrencyCode,CountryCode,GSTIN " & _
                                         " from AccMast LEFT JOIN AccMastAddr ON AccMast.accid=AccMastAddr.AccountNo where Alias='" & txtSuppAlias.Text & "'")
        End If

        If dt.Rows.Count > 0 Then
            txtSuppAlias.Tag = dt(0)("accid")
            If accid > 0 Then
                txtSuppAlias.Text = Trim("" & dt(0)("Alias"))
                txtSuppName.Text = Trim("" & dt(0)("AccDescr"))
            End If
            If enableGCC Then
                If Trim(dt(0)("TrdLcno") & "") <> "" Then
                    lblgstn.Text = "TIN: " & Trim(dt(0)("TrdLcno") & "")
                    chktaxInv.Checked = True
                Else
                    'chktaxInv.Checked = False
                    lblgstn.Text = "TIN: Nill"
                    lblgstn.Tag = 0
                End If
                lblstatecode.Text = "Emirate : "

            Else
                If Trim(dt(0)("GSTIN") & "") <> "" Then
                    lblgstn.Text = "GSTN: " & Trim(dt(0)("GSTIN") & "")
                    chktaxInv.Checked = True
                    lblgstn.Tag = 1
                Else
                    'chktaxInv.Checked = False
                    lblgstn.Text = "GSTN: Nill"
                    lblgstn.Tag = 0
                End If
                lblstatecode.Text = "Sate Code : "
            End If
            lblstatecode.Text = lblstatecode.Text & dt(0)("CountryCode")
            If ("" & dt(0)("CountryCode")) = "" Then
                lblstatecode.Tag = ""
            Else
                If Trim(dt(0)("CountryCode") & "") <> Trim(stateCode & "") Then
                    lblstatecode.Tag = 1
                Else
                    lblstatecode.Tag = ""
                End If
            End If
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
            If enablecess Or (enableFloodCess And Val(lblgstn.Tag & "") = 0 And cessdate <= DateValue(cldrdate.Value) And cessenddate >= DateValue(cldrdate.Value)) Then
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
                    If enableFloodCess And cessenddate >= DateValue(cldrdate.Value) Then
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
    '    Private Sub CalculateGST(Optional ByVal isAddcess As Boolean = False)
    '        Dim i As Integer
    '        Dim dtHSN As DataTable
    '        Dim dtrow As DataRow
    '        Dim slno As Integer
    '        If dtTax Is Nothing Then Exit Sub
    '        If dtTax.Rows.Count > 0 Then
    '            dtTax.Rows.Clear()
    '        End If
    '        If enableGCC Then GoTo addtax
    '        If isAddcess Then
    '            If (enablecess And Val(lblgstn.Tag & "") = 0 And cessdate <= DateValue(cldrdate.Value)) Then
    'addtax:
    '                Dim dt As DataTable = _objcmnbLayer._fldDatatable("SELECT  vatcode,convert(money, 0) Amount,collectionAC,vat,AccDescr From VatMasterTb Left join accmast on VatMasterTb.collectionAC=AccMast.accid", False)
    '                For i = 0 To dt.Rows.Count - 1
    '                    dtrow = dtTax.NewRow
    '                    dtrow("slno") = dtTax.Rows.Count + 1
    '                    dtrow("AccountName") = dt(0)("AccDescr")
    '                    dtrow("ACid") = dt(0)("collectionAC")
    '                    dtrow("Amount") = 0
    '                    dtTax.Rows.Add(dtrow)
    '                Next
    '            End If
    '        End If
    '        If EnableGST Then
    '            With grdVoucher
    '                Dim _qurey As EnumerableRowCollection(Of DataRow)
    '                For i = 0 To .RowCount - 1
    '                    slno = 0
    '                    _qurey = From data In dtGST.AsEnumerable() Where data("HSNCode") = Trim(.Item(ConstBarcode, i).Value & "") Select data
    '                    If _qurey.Count > 0 Then
    '                        dtHSN = _qurey.CopyToDataTable
    '                        If Val(lblstatecode.Tag) = 0 Then
    '                            'adding CSGT Amount****
    '                            Dim a = (From data In dtTax.AsEnumerable() Where data("ACid") = dtHSN(0)("CGSTCAc") Select data("slno"))
    '                            For Each itm In a
    '                                Try
    '                                    slno = itm
    '                                Catch ex As Exception
    '                                    MsgBox(ex.Message)
    '                                End Try
    '                            Next
    '                            If slno = 0 Then
    '                                dtrow = dtTax.NewRow
    '                                dtrow("slno") = dtTax.Rows.Count + 1
    '                                dtrow("AccountName") = dtHSN(0)("CGSTCAname")
    '                                dtrow("ACid") = dtHSN(0)("CGSTCAc")
    '                                dtrow("Amount") = CDbl(.Item(ConstCGSTAmt, i).Value)
    '                                dtTax.Rows.Add(dtrow)
    '                            Else
    '                                dtTax(slno - 1)("Amount") = CDbl(dtTax(slno - 1)("Amount")) + CDbl(.Item(ConstCGSTAmt, i).Value)
    '                            End If
    '                            'adding SSGT Amount****
    '                            a = (From data In dtTax.AsEnumerable() Where data("ACid") = dtHSN(0)("SGSTCAc") Select data("slno"))
    '                            slno = 0
    '                            For Each itm In a
    '                                Try
    '                                    slno = itm
    '                                Catch ex As Exception
    '                                    MsgBox(ex.Message)
    '                                End Try

    '                            Next
    '                            If slno = 0 Then
    '                                dtrow = dtTax.NewRow
    '                                dtrow("slno") = dtTax.Rows.Count + 1
    '                                dtrow("AccountName") = dtHSN(0)("SGSTCAname")
    '                                dtrow("ACid") = dtHSN(0)("SGSTCAc")
    '                                dtrow("Amount") = CDbl(.Item(ConstSGSTAmt, i).Value)
    '                                dtTax.Rows.Add(dtrow)
    '                            Else
    '                                dtTax(slno - 1)("Amount") = CDbl(dtTax(slno - 1)("Amount")) + CDbl(.Item(ConstSGSTAmt, i).Value)
    '                            End If
    '                        Else
    '                            'adding ISGT Amount****
    '                            Dim a = (From data In dtTax.AsEnumerable() Where data("ACid") = dtHSN(0)("IGSTCAc") Select data("slno"))
    '                            slno = 0
    '                            For Each itm In a
    '                                slno = itm
    '                            Next
    '                            If slno = 0 Then
    '                                dtrow = dtTax.NewRow
    '                                dtrow("slno") = dtTax.Rows.Count + 1
    '                                dtrow("AccountName") = dtHSN(0)("IGSTCAname")
    '                                dtrow("ACid") = dtHSN(0)("IGSTCAc")
    '                                dtrow("Amount") = CDbl(.Item(ConstIGSTAmt, i).Value)
    '                                dtTax.Rows.Add(dtrow)
    '                            Else
    '                                dtTax(slno - 1)("Amount") = CDbl(dtTax(slno - 1)("Amount")) + CDbl(.Item(ConstIGSTAmt, i).Value)
    '                            End If
    '                        End If

    '                    End If
    '                    If enablecess Then
    '                        Dim b = (From data In dtTax.AsEnumerable() Where data("ACid") = Val(.Item(ConstcessAc, i).Value & "") Select data("slno"))
    '                        slno = 0
    '                        For Each itm In b
    '                            slno = itm
    '                        Next
    '                        If slno > 0 Then
    '                            dtTax(slno - 1)("Amount") = CDbl(dtTax(slno - 1)("Amount")) + CDbl(.Item(ConstcessAmt, i).Value)
    '                        End If
    '                    End If

    '                Next
    '            End With
    '        ElseIf enableGCC Then
    '            With grdVoucher
    '                For i = 0 To .RowCount - 1
    '                    Dim b = (From data In dtTax.AsEnumerable() Where data("ACid") = Val(.Item(ConstcessAc, i).Value & "") Select data("slno"))
    '                    slno = 0
    '                    For Each itm In b
    '                        slno = itm
    '                    Next
    '                    If slno > 0 Then
    '                        dtTax(slno - 1)("Amount") = CDbl(dtTax(slno - 1)("Amount")) + CDbl(.Item(ConstTaxAmt, i).Value)
    '                    End If
    '                Next

    '            End With
    '        End If


    '    End Sub
    Private Sub getGSTDetails(ByVal hsncode As String, ByVal i As Integer, ByVal calculatefromGrid As Boolean)
        Dim dt As DataTable
        With grdVoucher
            If Not calculatefromGrid Then
                'dt = _objcmnbLayer._fldDatatable("SELECT * FROM GSTTb WHERE HSNCODE='" & hsncode & "'")
                dt = returnHsnCodeDet(hsncode)
                If dt.Rows.Count > 0 Then
                    .Item(ConstCGSTP, i).Value = Format(IIf(IsDBNull(dt(0)("CGST")), 0, CDbl(dt(0)("CGST"))), lnumformat)
                    .Item(ConstSGSTP, i).Value = Format(IIf(IsDBNull(dt(0)("SGST")), 0, CDbl(dt(0)("SGST"))), lnumformat)
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
            If chktaxwithoutLinediscount.Checked Then
                actualPrice = (CDbl(.Item(ConstActualPrice, i).Value) * CDbl(.Item(ConstQty, i).Value)) - discountOther
            Else
                actualPrice = (CDbl(.Item(ConstActualPrice, i).Value) * CDbl(.Item(ConstQty, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value) - discountOther
            End If
            'actualPrice = (CDbl(.Item(ConstActualPrice, i).Value) * CDbl(.Item(ConstQty, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value) - discountOther
            actualPrice = Format(actualPrice, lnumFormat)
            .Item(ConstCGSTAmt, i).Value = (actualPrice * .Item(ConstCGSTP, i).Value) / 100
            .Item(ConstSGSTAmt, i).Value = (actualPrice * .Item(ConstSGSTP, i).Value) / 100
            .Item(ConstIGSTAmt, i).Value = (actualPrice * .Item(ConstIGSTP, i).Value) / 100
            If Val(lblstatecode.Tag) = 1 Then
                .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstIGSTAmt, i).Value), lnumformat)
                .Item(ConstTaxP, i).Value = .Item(ConstIGSTP, i).Value
            Else
                .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstCGSTAmt, i).Value) + CDbl(.Item(ConstSGSTAmt, i).Value), lnumformat)
                .Item(ConstTaxP, i).Value = CDbl(.Item(ConstCGSTP, i).Value) + CDbl(.Item(ConstSGSTP, i).Value)
            End If
        End With
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
        _objInv.TrType = "SR"
        Dim ds As DataSet = _objInv.ldInvoice("rturnInventoryDetailsForInvoicePrint")
        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = Me.MdiParent
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
        IsReturn = False
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
                    dt = _objcmnbLayer._fldDatatable("SELECT P1Unit U,P1Fra F,UnitPrice,additionalcess FROM InvItm WHERE Itemid=" & Val(.Item(ConstItemID, .CurrentCell.RowIndex).Value))
                ElseIf .Item(ConstB, .CurrentCell.RowIndex).Value = "P1" Then
                    .Item(ConstB, .CurrentCell.RowIndex).Value = "P2"
                    dt = _objcmnbLayer._fldDatatable("SELECT P2Unit U,P2Fra F,UnitPrice,additionalcess FROM InvItm WHERE Itemid=" & Val(.Item(ConstItemID, .CurrentCell.RowIndex).Value))
                ElseIf .Item(ConstB, .CurrentCell.RowIndex).Value = "P2" Then
u:
                    .Item(ConstB, .CurrentCell.RowIndex).Value = "B"
                    dt = _objcmnbLayer._fldDatatable("SELECT Unit U,1 F,UnitPrice,additionalcess FROM InvItm WHERE Itemid=" & Val(.Item(ConstItemID, .CurrentCell.RowIndex).Value))
                End If
                If dt Is Nothing Then GoTo u
                If dt.Rows.Count > 0 Then
                    .Item(ConstUnit, .CurrentCell.RowIndex).Value = dt(0)("U")
                    .Item(ConstPMult, .CurrentCell.RowIndex).Value = dt(0)("F")
                    Dim cost As Double
                    Dim addcess As Double
                    addcess = Val(dt(0)("additionalcess") & "") * Val(.Item(ConstPMult, .CurrentCell.RowIndex).Value & "")
                    cost = dt(0)("UnitPrice")
                    cost = cost * Val(.Item(ConstPMult, .CurrentCell.RowIndex).Value)
                    .Item(ConstActualPrice, .CurrentCell.RowIndex).Value = cost
                    .Item(ConstUPrice, .CurrentCell.RowIndex).Value = Format(cost, lnumFormat)
                    .Item(ConstAdditionalcess, .CurrentCell.RowIndex).Value = addcess
                    calculate(, True)
                End If
            End With
        End If
    End Sub

    Private Sub grdVoucher_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellEndEdit
        Dim col As Integer = e.ColumnIndex
        Dim ndec1 As Integer
        If col = ConstQty Or col = ConstUPrice Or col = ConstDisAmt Or col = ConstLTotal Or col = ConstTaxP Then
            If col = ConstQty Then
                If Val(grdVoucher.Item(ConstPFraction, grdVoucher.CurrentRow.Index).Value & "") = 0 Then grdVoucher.Item(ConstPFraction, grdVoucher.CurrentRow.Index).Value = NoOfDecimal
                ndec1 = grdVoucher.Item(ConstPFraction, grdVoucher.CurrentRow.Index).Value
            Else
                ndec1 = NoOfDecimal
            End If
            grdVoucher.Item(col, e.RowIndex).Value = Format(Convert.ToDecimal(grdVoucher.Item(col, e.RowIndex).Value), "#,##0" & IIf(ndec1 = 0, "", "." & Strings.StrDup(ndec1, "0")))
        End If
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
                calcualteLineTotal(grdVoucher.Rows.Count - 1)
            Next
            grdVoucher.CurrentCell = grdVoucher.Item(ConstUPrice, grdVoucher.Rows.Count - 1)
            'chgbyprg = True
            'grdVoucher.BeginEdit(True)
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
        NextNumber()
    End Sub

    Private Sub btnshow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnshow.Click
        fSlctDoc = New SelectInvTr
        IsReturn = True
        With fSlctDoc
            .strType = "IS"
            .Text = "Sales Invoices"
            .ShowDialog()
        End With
        If Not fSlctDoc Is Nothing Then fSlctDoc = Nothing
    End Sub

    Private Sub txtDOLst_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDOLst.KeyDown
        If e.KeyCode = Keys.Return Then
            If Val(txtDOLst.Text) = 0 Then Exit Sub
            If MsgBox("Do you want to import all items from this invoice?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.No Then Exit Sub
            Dim dt As DataTable
            dt = _objcmnbLayer._fldDatatable("select trid from ItmInvCmnTb where prefix='" & txtImpPrefix.Text & "' and invno=" & Val(txtDOLst.Text))
            If dt.Rows.Count > 0 Then
                PasteFrom(dt(0)("trid"))
            End If
        End If
    End Sub

    Private Sub txtDOLst_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDOLst.Validated
        If Val(txtDOLst.Text) = 0 Then Exit Sub
        txtReference.Text = Trim(txtImpPrefix.Text) & IIf(txtImpPrefix.Text = "", "", "/") & txtDOLst.Text
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
            Dim entryref As String = "SALES RETURN INVOICE : " & txtSuppName.Text & " # " & Trim(txtprefix.Text) & numVchrNo.Text
            setAcctrDetValue(LinkNo, stockAc, Trim(txtReference.Text), entryref, costAmt, "", "", 3, 0, "", _
                           "", Val(txtSuppAlias.Tag), costOfSalesAc & txtReference.Text, "", FCRt)
            '_objTr.saveAccTrans()
            'UpdtClosBal(costOfSalesAc, costAmt)
            'credit entry [stock in hand]
            costAmt = costAmt * -1
            setAcctrDetValue(LinkNo, costOfSalesAc, Trim(txtReference.Text), entryref, costAmt, "", "", 3, 0, "", _
                           "", Val(txtSuppAlias.Tag), stockAc & txtReference.Text, "", FCRt)
            '_objTr.saveAccTrans()
            'UpdtClosBal(stockAc, costAmt)
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'Timer1.Enabled = False
        'If EnableWarranty = False Then resizeGridColumn(grdVoucher, ConstDescr)
        'If isAddRow Then
        '    AddRow()
        'End If
        Timer1.Enabled = False
        If grdVoucher.ColumnCount = 0 Then Exit Sub
        If EnableWarranty = False And Me.Width > 1200 Then resizeGridColumn(grdVoucher, ConstDescr)
        If Me.Width <= 1200 Or grdVoucher.Columns(ConstDescr).Width <= 25 Then
            grdVoucher.Columns(ConstDescr).Width = 150
        End If
        If isAddRow Then
            AddRow()
        End If
    End Sub

    Private Sub SalesReturnInvoice_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Timer1.Enabled = True
        'Label2.Text = Me.Width & ", " & Me.Height
    End Sub

    Private Sub cmbfc_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbfc.SelectedIndexChanged
        returnFcrt()
    End Sub

    Private Sub txtfcrt_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtfcrt.Validated
        FCRt = txtfcrt.Text
    End Sub

  
    Private Sub fSelectRtnItem_transfer(ByVal dt As System.Data.DataTable) Handles fSelectRtnItem.transfer
        Dim i As Integer
        Dim ids As String = ""
        Dim dtInv As DataTable
        Dim UPerPack As Double
        grdVoucher.Rows.Clear()
        For i = 0 To dt.Rows.Count - 1
            ids = ids & IIf(ids = "", "", ",") & dt(i)("id")
        Next
        If ids = "" Then Exit Sub
        If ids <> "" Then ids = "(" & ids & ")"
        dtInv = _objcmnbLayer._fldDatatable("SELECT ItmInvTrTb.*, [Item Code],Warrentyname,isSerialNo,fcode,FraCount," & _
                                         "isnull(itemCategory,'')itemCategory,collectionAC,vat,rgcess,rgcaccount,additionalcess FROM ItmInvTrTb LEFT JOIN InvItm ON InvItm.ItemId = ItmInvTrTb.ItemId " & _
                                         " LEFT JOIN InvItmPropertiesTb ON InvItmPropertiesTb.Itemid=invitm.itemid " & _
                                         "LEFT JOIN WarrentyMastertb ON WarrentyMastertb.Wid=ItmInvTrTb.WarrentyId " & _
                                         "LEFT JOIN FuelMeterReadingTb ON FuelMeterReadingTb.fmeterid=ItmInvTrTb.FuleMeterid " & _
                                         "LEFT JOIN UnitsTb ON UnitsTb.Units=ItmInvTrTb.Unit " & _
                                         "LEFT JOIN VatMasterTb ON InvItm.vatId=VatMasterTb.vatId " & _
                                         "LEFT JOIN (select vatcode rgccode,vat rgcess, collectionAC rgcaccount,vatid rgcid from  VatMasterTb)regularcess ON InvItm.regularcessid=regularcess.rgcid " & _
                                         "WHERE ItmInvTrTb.id in " & ids & " ORDER BY SlNo")
        For i = 0 To dtInv.Rows.Count - 1
            With grdVoucher
                .Rows.Add(1)
                chgbyprg = True
                UPerPack = IIf(dtInv(i)("PFraction") = 0, 1, dtInv(i)("PFraction"))
                .Item(ConstSlNo, i).Value = IIf(Val(dtInv(i)("ItemId")) = 0, "M", "")
                If .Item(ConstSlNo, i).Value <> "M" Then
                    UPerPack = IIf(dtInv(i)("PFraction") = 0 Or IsDBNull(dtInv(i)("PFraction")), 1, dtInv(i)("PFraction"))
                    .Item(ConstItemCode, i).Value = Trim("" & dtInv(i)("Item Code"))
                    .Item(ConstItemID, i).Value = dtInv(i)("ItemId")
                    .Item(ConstPFraction, i).Value = Val(dtInv(i)("FraCount") & "")
                    .Item(ConstPMult, i).Value = UPerPack 'IIf(IsNull(dtInv!PFraCount), "2", dtInv!PFraCount)
                Else
                    .Item(ConstPFraction, i).Value = "2"
                    .Item(ConstPMult, i).Value = "1"
                    .Item(ConstItemID, i).Value = 0
                End If
                .Item(ConstDescr, i).Value = IIf(IsDBNull(dtInv(i)("IDescription")), "", dtInv(i)("IDescription"))
                .Item(ConstB, i).Value = IIf(dtInv(i)("Method") & "" = "", "B", Trim(dtInv(i)("Method") & ""))
                .Item(ConstUnit, i).Value = Trim("" & dtInv(i)("Unit"))
                If Val(dtInv(i)("Taxp") & "") = 0 Then dtInv(i)("Taxp") = 0
                .Item(ConstTaxP, i).Value = Format(dtInv(i)("Taxp"), lnumFormat)
                If Val(dtInv(i)("taxamt") & "") = 0 Then dtInv(i)("taxamt") = 0
                .Item(ConstTaxAmt, i).Value = Format(dtInv(i)("taxamt") / FCRt, lnumFormat)

                If Val(dtInv(i)("vat") & "") = 0 Then dtInv(i)("vat") = 0
                If Val(dtInv(i)("rgcess") & "") = 0 Then dtInv(i)("rgcess") = 0
                If Not enableGCC Then
                    .Item(Constcess, i).Value = Format(dtInv(i)("vat"), lnumFormat)
                    .Item(ConstRegcess, i).Value = Format(dtInv(i)("rgcess"), lnumFormat)
                Else
                    .Item(Constcess, i).Value = 0
                    .Item(ConstRegcess, i).Value = 0
                End If

                If Val(dtInv(i)("CessAmt") & "") = 0 Then dtInv(i)("CessAmt") = 0
                .Item(ConstcessAmt, i).Value = Format(dtInv(i)("CessAmt") / FCRt, lnumFormat)
                If Val(dtInv(i)("regularcessAmt") & "") = 0 Then dtInv(i)("regularcessAmt") = 0
                .Item(ConstregularCessAmt, i).Value = Format(dtInv(i)("regularcessAmt") / FCRt, lnumFormat)
                If Val(dtInv(i)("FloodcessAmt") & "") = 0 Then dtInv(i)("FloodcessAmt") = 0
                .Item(ConstFloodCessAmt, i).Value = Format(dtInv(i)("FloodcessAmt") / FCRt, lnumFormat)
                If Val(dtInv(i)("additionalcess") & "") = 0 Then dtInv(i)("additionalcess") = 0
                .Item(ConstAdditionalcess, i).Value = dtInv(i)("additionalcess") / FCRt
                If Val(dtInv(i)("collectionAC") & "") = 0 Then dtInv(i)("collectionAC") = 0
                .Item(ConstcessAc, i).Value = Val(dtInv(i)("collectionAC"))
                If Val(dtInv(i)("rgcaccount") & "") = 0 Then dtInv(i)("rgcaccount") = 0
                .Item(ConstRegcessAc, i).Value = Val(dtInv(i)("rgcaccount"))

                .Item(ConstLocation, i).Value = Trim("" & dtInv(i)("Warrentyname")) ' !Colour
                .Item(ConstQty, i).Value = Format(dtInv(i)("TrQty") / UPerPack, "#,##0" & IIf(Val(.Item(ConstPFraction, i).Value & "") = 0, "", "." & Strings.StrDup(CByte(Val(.Item(ConstPFraction, i).Value & "")), "0")))
                If Val(dtInv(i)("Focqty") & "") = 0 Then dtInv(i)("Focqty") = 0
                grdVoucher.Item(ConstFocQty, i).Value = Format(dtInv(i)("Focqty") / UPerPack, "#,##0" & IIf(Val(grdVoucher.Item(ConstPFraction, i).Value & "") = 0, "", "." & Strings.StrDup(CByte(grdVoucher.Item(ConstPFraction, i).Value), "0")))

                .Item(ConstUPrice, i).Value = Format(dtInv(i)("UnitCost") * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & lnumFormat)
                .Item(ConstActualPrice, i).Value = dtInv(i)("UnitCost") * UPerPack / FCRt
                .Item(ConstLTotal, i).Value = Format((dtInv(i)("TrQty") * dtInv(i)("UnitCost")) * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & lnumFormat)
                .Item(ConstUnitOthCost, i).Value = Format(dtInv(i)("UnitOthCost") * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & lnumFormat)
                .Item(ConstActualOthCost, i).Value = dtInv(i)("UnitOthCost") * UPerPack / FCRt
                .Item(ConstSerialNo, i).Value = dtInv(i)("SerialNo")
                .Item(constItmTot, i).Value = Format((.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value), lnumFormat)
                ' .Item(ConstMthd,0).Value = dtInv(i)("Method")
                ' .Item(ConstUnitVal,0).Value = dtInv(i)("UnitValue") * UPerPack / FCRt
                ' .Item(ConstDiscOther,0).Value = dtInv(i)("DiscOther") * UPerPack / FCRt
                .Item(ConstDiscOther, i).Value = dtInv(i)("UnitDiscount") * UPerPack / FCRt
                If Val(dtInv(i)("DisP") & "") = 0 Then dtInv(i)("DisP") = 0
                .Item(ConstDisP, i).Value = Format(dtInv(i)("DisP"), numFormat)
                If Val(dtInv(i)("ItemDiscount") & "") = 0 Then dtInv(i)("ItemDiscount") = 0
                .Item(ConstDisAmt, i).Value = Format(dtInv(i)("ItemDiscount") * UPerPack / FCRt, lnumFormat)
                .Item(ConstImpDocId, i).Value = 0
                .Item(ConstImpLnId, i).Value = dtInv(i)("id")

                .Item(ConstBarcode, i).Value = dtInv(i)("HSNCode")
                .Item(ConstCGSTP, i).Value = dtInv(i)("CSGTP")
                .Item(ConstCGSTAmt, i).Value = dtInv(i)("CGSTAMT") / FCRt

                .Item(ConstSGSTP, i).Value = dtInv(i)("SGSTP")
                .Item(ConstSGSTAmt, i).Value = dtInv(i)("SGSTAmt") / FCRt

                .Item(ConstIGSTP, i).Value = dtInv(i)("IGSTP")
                .Item(ConstIGSTAmt, i).Value = dtInv(i)("IGSTAmt") / FCRt

                If Not IsDBNull(dtInv(i)("isSerialNo")) Then
                    .Item(ConstIsSerial, i).Value = IIf(dtInv(i)("isSerialNo"), 1, 0)
                Else
                    .Item(ConstIsSerial, i).Value = 0
                End If
                .Item(ConstId, i).Value = 0
                If Not IsDBNull(dtInv(i)("WarrentyExpDate")) Then
                    If DateValue(dtInv(i)("WarrentyExpDate")) > DateValue("01/01/1950") Then
                        .Item(ConstWarrentyExpiry, i).Value = dtInv(i)("WarrentyExpDate")
                    End If
                End If
                .Item(Constsman, i).Value = Trim(dtInv(i)("Smancode") & "")
                If .Item(ConstSerialNo, i).Value <> "" And enableSerialnumber Then
                    AddTodtSerialNo(.Item(ConstSerialNo, i).Value, Val(.Item(ConstItemID, i).Value), i, DateValue(.Item(ConstWarrentyExpiry, i).Value), Val(.Item(ConstId, i).Value))
                End If
                If Trim(dtInv(i)("itemCategory") & "") = "room" Then
                    .Rows(i).DefaultCellStyle.BackColor = Color.LightGray
                    '.Rows(i).DefaultCellStyle.ForeColor = Color.White
                End If
                If Not IsDBNull(dtInv(i)("manufacturingdate")) Then
                    If DateValue(dtInv(i)("manufacturingdate")) > DateValue("01/01/1950") Then
                        .Item(ConstManufacturingdate, i).Value = dtInv(i)("manufacturingdate")
                    End If
                End If
                If Val(dtInv(i)("MRP") & "") = 0 Then dtInv(i)("MRP") = 0
                .Item(ConstMRP, i).Value = CDbl(dtInv(i)("MRP"))
                If Val(dtInv(i)("costavg") & "") = 0 Then dtInv(i)("costavg") = 0
                .Item(ConstBatchCost, i).Value = CDbl(dtInv(i)("costavg"))
            End With
            'If dtInv.Rows.Count > 0 Then dtInv.Clear()
            calcualteLineTotal(i)
        Next
        reArrangeNo()
        calculate()
        chgbyprg = False
    End Sub

    Private Sub btntax_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btntax.Click
        If enableGCC = False And EnableGST = False Then Exit Sub
        CalculateGST(True)
        ShowTax.grdVoucher.DataSource = dtTax
        ShowTax.ShowDialog()
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
                    .isReturn = True
                    .detId = Val(grdVoucher.Item(ConstId, grdVoucher.CurrentRow.Index).Value)
                    .rowIndex = grdVoucher.CurrentCell.RowIndex + 1
                    .ShowDialog()
                End With

            End If
        End If
    End Sub

    Private Sub chktaxwithoutLinediscount_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chktaxwithoutLinediscount.CheckedChanged
        If chgbyprg Then Exit Sub
        calculate(, True)
    End Sub

    Private Sub chktaxInv_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chktaxInv.CheckedChanged
        calculate()
    End Sub

    Private Sub cmbdeliveredBy_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbdeliveredBy.SelectedIndexChanged
        If chgbyprg Then Exit Sub
        chgPost = True
    End Sub

    Private Sub chkexport_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkexport.CheckedChanged
        chktaxInv.Checked = Not chkexport.Checked
        chktaxInv.Enabled = Not chkexport.Checked
    End Sub

    Private Sub grdVoucher_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellEnter
        If grdVoucher.Item(e.ColumnIndex, e.RowIndex).ReadOnly = False Then grdBeginEdit()
    End Sub
    Private Sub fwait_triggerEvent() Handles fwait.triggerEvent
        Select Case fwait.triggerType
            Case 1
                saveTrans()
            Case 2
                ldPostedInv()
            Case 3
                PasteFrom(FrTrId)
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

    Private Sub btnmultipleDebit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnmultipleDebit.Click
        showMultipleDebits()
    End Sub
    Private Function showMultipleDebits() As Boolean
        If Val(lblNetAmt.Text) = 0 Then
            MsgBox("Invoice Amount should be greater than Zero", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        Dim fMultipleDebit As New MultipledebitsOnSalesFrm
        With fMultipleDebit
            .txtreference.Text = ""
            .dt = dtMultipleDebits
            .trtype = "SR"
            'dtMultipleDebits.Rows.Clear()
            .lblinvoiceAmt.Text = "Invoice Amt. " & Format(CDbl(lblNetAmt.Text), numFormat)
            .lblinvoiceAmt.Tag = CDbl(lblNetAmt.Text)
            .txtreference.Text = Trim(txtprefix.Text) & IIf(txtprefix.Text = "", "", "/") & numVchrNo.Text
            .customeraccid = Val(txtSuppAlias.Tag)
            .customeralias = txtSuppAlias.Text
            .customername = txtSuppName.Text
            .lnumformat = lnumFormat
            .skipCrEntry = False
            .ShowDialog()
            If Val(.btnupdate.Tag) > 0 Then
                dtMultipleDebits = .dt
                If Not dtSetoffTable Is Nothing Then
                    If dtSetoffTable.Rows.Count > 0 Then dtSetoffTable.Rows.Clear()
                End If
                dtSetoffTable = .dtSetoffTable
                chgPost = True
                Return True
            Else
                Return False
            End If
        End With
    End Function
    Private Sub saveMultipleDebits(ByVal trid As Long)
        Dim i As Integer
        For i = 0 To dtMultipleDebits.Rows.Count - 1
            If Val(dtMultipleDebits(i)("dbtid")) = 0 Then
                _objcmnbLayer._saveDatawithOutParm("INSERT INTO SalesMultipleDebitsTb (dbtrid,dbaccid,accAmt,reference) VALUES" & _
                                                   "(" & trid & "," & Val(dtMultipleDebits(i)("accid")) & "," & CDbl(dtMultipleDebits(i)("accAmt")) & ",'" & Trim(dtMultipleDebits(i)("reference") & "") & "')")
            Else
                _objcmnbLayer._saveDatawithOutParm("UPDATE SalesMultipleDebitsTb SET setremove=0 " & _
                                                   " WHERE dbtid=" & dtMultipleDebits(i)("dbtid"))
            End If
        Next
        _objcmnbLayer._saveDatawithOutParm("DELETE FROM SalesMultipleDebitsTb " & _
                                                 " WHERE setremove=1 AND dbtrid=" & trid)
    End Sub
    Private Sub loadSalesMultipleDebits(ByVal trid As Long)
        dtMultipleDebits = _objcmnbLayer._fldDatatable("SELECT Alias,AccDescr,accAmt,reference,accid,dbtid FROM SalesMultipleDebitsTb " & _
                                                       "LEFT JOIN AccMast ON AccMast.accid=SalesMultipleDebitsTb.dbaccid where dbtrid=" & trid)
        If trid > 0 Then
            _objcmnbLayer._saveDatawithOutParm("UPDATE SalesMultipleDebitsTb SET setremove=1 " & _
                                          " WHERE dbtrid=" & trid)
        End If
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

    Private Sub cmbsign_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If chgbyprg Then Exit Sub
        If chgNumByPgm Then Exit Sub
        chkautoroundOff.Checked = False
        calculate(, , , True)
    End Sub

    Private Sub txtroundOff_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtroundOff.TextChanged
        If chgNumByPgm Then Exit Sub
        chkautoroundOff.Checked = False
        calculate(, , , True)
    End Sub
    Private Sub calculateTaxFromUnitPrice(ByVal i As Integer)
        If chgUprice And chkcal.Checked Then
            chgbyprg = True
            Dim actualprice As Double
            Dim ttax As Double
            With grdVoucher
                actualprice = CDbl(.Item(ConstUPrice, i).Value) - CDbl(.Item(ConstAdditionalcess, i).Value)
                ttax = CDbl(.Item(ConstTaxP, i).Value) + CDbl(.Item(Constcess, i).Value) + CDbl(.Item(ConstRegcess, i).Value)
                .Item(ConstActualPrice, i).Value = (actualprice * 100) / (ttax + 100)
                .Item(ConstUPrice, i).Value = Format(.Item(ConstActualPrice, i).Value, lnumFormat)
            End With
        End If
        chgUprice = False
        chgbyprg = False
    End Sub


    Private Sub chkcal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkcal.Click
        _objcmnbLayer._saveDatawithOutParm("Update CompanyTb set calcluatetaxFrompriceInv =" & IIf(chkcal.Checked, 1, 0))
        calcluatetaxFrompriceInv = chkcal.Checked
    End Sub
End Class