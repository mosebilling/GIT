
Public Class FinanceSalesInvoice

#Region "Public Variables"
    Public isModi As Boolean
    Public isCust As Boolean
    Public isJobCategory As Integer '1 -membership job
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
    Private dtItemInfo As DataTable
    Private isNotCustomerAccount As Boolean
    Private creditCustomerACC As Long
    Private dtInvItm As DataTable
    Private DiscAcc As Long
    Private TrTypeNo As Integer
    Private FrTrId As Long
    Private IsPOSInv As Boolean


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
    Private dtMultipleDebits As DataTable
    Private dtSetoffTable As DataTable
    Private diableNegativeSale As Boolean
    Private disableBelowcost As Boolean
    Private cessdate As Date
    Private exitFromValidProc As Boolean
    Private vtype As String = ""

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
    Private WithEvents fPpayment As PreviousPaymentFrm
    Private WithEvents fproductMast As ItemMastFrm
    Private WithEvents fDolist As DOListFrm
    Private WithEvents fHistory As New SelectHistory
    Private WithEvents fshowlocationqty As ShowLocationQtyFrm
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
#Region "Const Variables"
    Private Const CostSlNo = 0
    Private Const CostDbName = 1
    Private Const CostCrName = 2
    Private Const CostReference = 3
    Private Const CostAmount = 4
    Private Const CostvatOther = 5
    Private Const CostDescr = 6
    Private Const CostFCAmount = 7
    Private Const CostFCName = 8
    Private Const CostFCRate = 9
    Private Const CostDrAcc = 10
    Private Const CostCrAcc = 11
    Private Const CostvatAcc = 12
    Private Const Costvatcode = 13
#End Region
    Delegate Sub GenericDelegate(ByVal Mname As String, ByVal RecName As String, ByVal rec As Integer, ByVal count As Integer)

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
            InvList = _objcmnbLayer._fldDatatable("SELECT TrId FROM ItmInvCmnTb WHERE " & IIf(UsrBr = "", "", " Brid='" & UsrBr & "' AND") & " TrId = " & FromTrId)
        Else
            InvList = _objcmnbLayer._fldDatatable("SELECT TrId FROM ItmInvCmnTb WHERE " & IIf(UsrBr = "", "", " Brid='" & UsrBr & "' AND") & " TrType = 'IS' AND InvNo = " & Val(numVchrNo.Text) & " AND PreFix = '" & MkDbSrchStr(Trim(txtprefix.Text), True) & "'")
        End If
        If InvList.Rows.Count > 0 Then
            If Not isImport Then
                loadedTrId = InvList(0)("TrId")
                InvList = Nothing
                loadWaite(2)
                isModi = True
            Else
                isImport = True
                FrTrId = InvList(0)("TrId")
                loadWaite(3)
                'isImport = False
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
        'getItemInfo(0)
        If Not dtItemInfo Is Nothing Then
            dtItemInfo.Rows.Clear()
            grdItemInfo.DataSource = dtItemInfo
        End If
        btnupdate.Enabled = True
        Dim ItmInvCmnTb As DataTable
        Dim DocAssgnTb As DataTable
        Dim sRs As DataTable
        'Dim Discount As Double
        Dim UPerPack As Double
        Dim Protect As Boolean
        Dim CrossBr As Boolean
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

        dtset = _objcmnbLayer._ldDataset("SELECT ItmInvCmnTb.*,tunername,NoOfMnth,jobname,[Voucher Name],Ctgry,isnull(SalesmanTb.accountno,0) Smanacc," & _
                                         "Alias,AccDescr,isnull(linkno,0)linkno,rvprefix,rvno " & _
                                                  "FROM ItmInvCmnTb " & _
                                                  "LEFT JOIN PreFixTb ON PreFixTb.id=ItmInvCmnTb.invtypeno " & _
                                                  "LEFT JOIN JobTb ON ItmInvCmnTb.[Job Code]=JobTb.jobcode " & _
                                                  "LEFT JOIN SalesmanTb ON SalesmanTb.SManCode=ItmInvCmnTb.SlsManId " & _
                                                  "LEFT JOIN AccMast ON AccMast.accid=ItmInvCmnTb.PSAcc " & _
                                                  "LEFT JOIN AccTrCmn on ItmInvCmnTb.trid=AccTrCmn.lnkno " & _
                                                  "LEFT JOIN TunurTb ON TunurTb.tunurid=itminvcmntb.turnurid " & _
                                                  "LEFT JOIN (select PreFix rvprefix, JVNum rvno, linkno rvlinkno from AcctrCmn) rv on ItmInvCmnTb.ISRVID=rv.rvlinkno " & _
                                                  "WHERE TrId = " & loadedTrId & " AND TrType = 'IS'" & itemquery, False)
        ItmInvCmnTb = dtset.Tables(0)
        chgbyprg = True
        chgNumByPgm = True
        If ItmInvCmnTb.Rows.Count = 0 Then GoTo Quit
        'getProtectUntil()
        'cmbVoucherTp.Text = getVoucherName(Val(ItmInvCmnTb(0)("InvTypeNo") & ""))
        'Dim PreFixTb As DataTable = _objcmnbLayer._fldDatatable("SELECT  * FROM PreFixTb WHERE Id = " & Val(ItmInvCmnTb(0)("invtypeno") & ""), False)
        Dim NoOfMnth As Integer
        NoOfMnth = Val(ItmInvCmnTb(0)("NoOfMnth") & "")
        cldrdate.Value = Format(ItmInvCmnTb(0)("TrDate"), DtFormat) ' "MM/dd/yyyy")
        clrDuedate.Value = Format(ItmInvCmnTb(0)("DueDate"), DtFormat)
        txtprefix.Text = Trim(ItmInvCmnTb(0)("PreFix") & "")
        txtPPrefix.Text = txtprefix.Text
        txtprefix.Tag = ItmInvCmnTb(0)("PreFix")
        numVchrNo.Text = ItmInvCmnTb(0)("InvNo") 'Val(Mid(!InvNo, 3)) '!InvNo '
        numVchrNo.Tag = ItmInvCmnTb(0)("InvNo")
        numPrintVchr.Text = ItmInvCmnTb(0)("InvNo")  'CMNREC(12).FormattedText
        txtJob.Text = Trim("" & ItmInvCmnTb(0)("Job Code"))
        txtDOLst.Text = Trim("" & ItmInvCmnTb(0)("DocLstTxt"))
        txtjobname.Text = Trim(ItmInvCmnTb(0)("jobname") & "")
        cmbsalesman.Text = Trim(ItmInvCmnTb(0)("SlsManId") & "")
        cmbsalesman.Tag = Val(ItmInvCmnTb(0)("Smanacc") & "")
        cmbtunur.Text = Trim(ItmInvCmnTb(0)("tunername") & "")
        chkexport.Checked = Val(ItmInvCmnTb(0)("isImportOrExport") & "")
        cmblocation.Text = Trim(ItmInvCmnTb(0)("DocDefLoc") & "")
        txtphone.Text = Trim(ItmInvCmnTb(0)("customerPhone") & "")
        txtrvno.Text = Trim(ItmInvCmnTb(0)("rvprefix") & "") & Val(ItmInvCmnTb(0)("rvno") & "")
        If Val(ItmInvCmnTb(0)("rvno") & "") = 0 Then
            txtrvno.Tag = 0
        Else
            txtrvno.Tag = Val(ItmInvCmnTb(0)("ISRVID") & "")
        End If
        If Val(ItmInvCmnTb(0)("ccustid") & "") = 0 Then
            creditCustomerACC = Val(ItmInvCmnTb(0)("CSCode" & ""))
        Else
            creditCustomerACC = Val(ItmInvCmnTb(0)("ccustid" & ""))
        End If
        txtCashCustomer.Enabled = True
        If Val(ItmInvCmnTb(0)("priceType") & "") = 0 Then
            chkretailprice.Checked = True
            chkws.Checked = False
            chksecondprice.Checked = False
        ElseIf Val(ItmInvCmnTb(0)("priceType") & "") = 1 Then
            chkws.Checked = True
            chkretailprice.Checked = False
            chksecondprice.Checked = False
        Else
            chkws.Checked = False
            chkretailprice.Checked = False
            chksecondprice.Checked = True
        End If
        If Trim(ItmInvCmnTb(0)("cap1") & "") <> "" Then
            txtcap1.Text = Trim(ItmInvCmnTb(0)("cap1") & "")
        End If
        If Trim(ItmInvCmnTb(0)("cap2") & "") <> "" Then
            txtcap1.Text = Trim(ItmInvCmnTb(0)("cap2") & "")
        End If
        If Trim(ItmInvCmnTb(0)("cap3") & "") <> "" Then
            txtcap1.Text = Trim(ItmInvCmnTb(0)("cap3") & "")
        End If
        If Trim(ItmInvCmnTb(0)("cap4") & "") <> "" Then
            txtcap1.Text = Trim(ItmInvCmnTb(0)("cap4") & "")
        End If
        If Trim(ItmInvCmnTb(0)("cap5") & "") <> "" Then
            txtcap1.Text = Trim(ItmInvCmnTb(0)("cap5") & "")
        End If
        txtinfo1.Text = Trim(ItmInvCmnTb(0)("info1") & "")
        txtinfo2.Text = Trim(ItmInvCmnTb(0)("info2") & "")
        txtinfo3.Text = Trim(ItmInvCmnTb(0)("info3") & "")
        txtinfo4.Text = Trim(ItmInvCmnTb(0)("info4") & "")
        txtinfo5.Text = Trim(ItmInvCmnTb(0)("info5") & "")

        'Dim dt As DataTable = _objcmnbLayer._fldDatatable("Select isnull(accountno,0)accountno from SalesmanTb where SManCode='" & cmbsalesman.Text & "'", False)
        'If dt.Rows.Count > 0 Then

        'End If

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
        If IsDBNull(ItmInvCmnTb(0)("isTaxInvoice")) Then ItmInvCmnTb(0)("isTaxInvoice") = 0
        chktaxInv.Checked = IIf(ItmInvCmnTb(0)("isTaxInvoice") = "True", 1, 0)
        If Not IsDBNull(ItmInvCmnTb(0)("taxwithoutLineDiscount")) Then
            chktaxwithoutLinediscount.Checked = ItmInvCmnTb(0)("taxwithoutLineDiscount")
        End If
        cmbdeliveredBy.Text = Trim(ItmInvCmnTb(0)("deliveredBy") & "")
        If Not IsDBNull(ItmInvCmnTb(0)("IsPOS")) Then
            IsPOSInv = ItmInvCmnTb(0)("IsPOS")
            'If EnableGST And IsPOS Then chktaxInv.Checked = True
        End If
        txtdp.Text = 0
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
        Dim varlinkno As Long = ItmInvCmnTb(0)("linkno")
        setCustomer(ItmInvCmnTb(0)("CSCode"), True, Trim(ItmInvCmnTb(0)("GSTN") & ""), , varlinkno)
        If Not IsDBNull(ItmInvCmnTb(0)("isB2B")) Then
            chkb2b.Checked = ItmInvCmnTb(0)("isB2B")
        Else
            chkb2b.Checked = False
        End If
        chgbyprg = True
        txtCashCustomer.Text = Trim(ItmInvCmnTb(0)("CashCustName") & "")
        txtCashCustomer.Tag = Val(ItmInvCmnTb(0)("CashCustid") & "")
        txtcustAddress.Text = Trim(ItmInvCmnTb(0)("OthrCust") & "")
        txtfcrt.Text = Format(ItmInvCmnTb(0)("FcRate"), lnumFormat)
        FCRt = ItmInvCmnTb(0)("FcRate")
        'sRs = _objcmnbLayer._fldDatatable("SELECT * FROM AccMast WHERE accid = " & ItmInvCmnTb(0)("PSAcc"))
        txtPurchAlias.Tag = ItmInvCmnTb(0)("PSAcc")
        txtPurchAlias.Text = ItmInvCmnTb(0)("Alias")
        txtPurchaseName.Text = ItmInvCmnTb(0)("AccDescr")
        'If sRs.Rows.Count > 0 Then

        'ElseIf txtPurchAlias.Text <> "" Then
        '    sRs = _objcmnbLayer._fldDatatable("SELECT * FROM AccMast WHERE alias = '" & txtPurchAlias.Text & "'")
        '    If sRs.Rows.Count > 0 Then
        '        txtPurchAlias.Tag = sRs(0)("accid")
        '        txtPurchAlias.Text = sRs(0)("Alias")
        '        txtPurchaseName.Text = sRs(0)("AccDescr")
        '    End If
        'End If

        txtReference.Text = Trim("" & ItmInvCmnTb(0)("LPO"))
        txtDescr.Text = Trim("" & ItmInvCmnTb(0)("TrDescription"))
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
        OthCost = Format(Val(ItmInvCmnTb(0)("OthCost") & ""), lnumFormat)
        Protect = (ItmInvCmnTb(0)("TrDate") <= ProtectUntil)
        'load grid
        PasteFrom(loadedTrId, dtset.Tables(1))
        showOtherCost(False)
        If enableMultipleDebitInInvoice Then loadSalesMultipleDebits(loadedTrId)

        'If Val(lblbalance.Text) > 0 Then
        '    lblbalance.Text = Format(CDbl(lblbalance.Text) - CDbl(lblNetAmt.Text), numFormat)
        'End If
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
        'If IsPOS Then
        '    MsgBox("You can't Post modifications in POS Invoice", MsgBoxStyle.Exclamation)
        '    btnupdate.Enabled = False
        '    If userType Then
        '        Dim isremovepos As Integer = IIf(getRight(174, CurrentUser), 1, 0)
        '        If isremovepos = 0 Then
        '            btndelete.Enabled = False
        '        End If
        '    End If

        'End If
        'For i = 0 To 10 - grdVoucher.RowCount
        '    AddRow(True)
        'Next
        numVchrNo.Focus()
        chgbyprg = False
        ChgId = False
        chgPost = False
        btnRv.Visible = True
        GoTo Quit
Err:
        If MsgBox(Err.Description & Chr(13) & "Retry?", vbRetryCancel + vbQuestion, "Warning During Opening Data File.") = vbRetry Then Resume
Quit:
        chgbyprg = False
    End Sub
    Private Sub PasteFrom(ByVal Trid As Long, Optional ByVal sRs As DataTable = Nothing)
        Dim i As Integer
        'Dim sRs As DataTable
        Dim UPerPack As Double
        Dim tNumformat As String
        If sRs Is Nothing Then
            sRs = _objcmnbLayer._fldDatatable("SELECT ItmInvTrTb.*, [Item Code],Warrentyname,isSerialNo,fcode,FraCount," & _
                                              "isnull(itemCategory,'')itemCategory,collectionAC,vat,rgcess,rgcaccount,additionalcess FROM ItmInvTrTb LEFT JOIN InvItm ON InvItm.ItemId = ItmInvTrTb.ItemId " & _
                                              " LEFT JOIN InvItmPropertiesTb ON InvItmPropertiesTb.Itemid=invitm.itemid " & _
                                              "LEFT JOIN WarrentyMastertb ON WarrentyMastertb.Wid=ItmInvTrTb.WarrentyId " & _
                                              "LEFT JOIN FuelMeterReadingTb ON FuelMeterReadingTb.fmeterid=ItmInvTrTb.FuleMeterid " & _
                                              "LEFT JOIN UnitsTb ON UnitsTb.Units=ItmInvTrTb.Unit " & _
                                              "LEFT JOIN VatMasterTb ON InvItm.vatId=VatMasterTb.vatId " & _
                                              "LEFT JOIN (select vatcode rgccode,vat rgcess, collectionAC rgcaccount,vatid rgcid from  VatMasterTb)regularcess ON InvItm.regularcessid=regularcess.rgcid " & _
                                              "WHERE TrId = " & Trid & " ORDER BY SlNo")
        End If

        grdVoucher.RowCount = 0
        If _objcmnbLayer.dtSerialNo.Rows.Count > 0 Then _objcmnbLayer.dtSerialNo.Rows.Clear()
        chgbyprg = True
        Dim importdocid As Long
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
                    If Val(lblgstn.Tag & "") = 0 Then
                        grdVoucher.Item(ConstFloodCessAmt, i).Value = sRs(i)("FloodcessAmt") / FCRt ' Format(sRs(i)("FloodcessAmt") / FCRt, lnumFormat)
                    Else
                        grdVoucher.Item(ConstFloodCessAmt, i).Value = 0
                    End If
                    If Val(sRs(i)("additionalcess") & "") = 0 Then sRs(i)("additionalcess") = 0
                    grdVoucher.Item(ConstAdditionalcess, i).Value = sRs(i)("additionalcess") / FCRt
                    If Val(sRs(i)("collectionAC") & "") = 0 Then sRs(i)("collectionAC") = 0
                    grdVoucher.Item(ConstcessAc, i).Value = Val(sRs(i)("collectionAC"))
                    If Val(sRs(i)("rgcaccount") & "") = 0 Then sRs(i)("rgcaccount") = 0
                    grdVoucher.Item(ConstRegcessAc, i).Value = Val(sRs(i)("rgcaccount"))

                    grdVoucher.Item(ConstLocation, i).Value = Trim("" & sRs(i)("Warrentyname")) ' !Colour
                    grdVoucher.Item(ConstQty, i).Value = Format(sRs(i)("TrQty") / UPerPack, "#,##0" & IIf(Val(grdVoucher.Item(ConstPFraction, i).Value & "") = 0, "", "." & Strings.StrDup(CByte(Val(grdVoucher.Item(ConstPFraction, i).Value & "")), "0")))
                    If Val(sRs(i)("Focqty") & "") = 0 Then sRs(i)("Focqty") = 0
                    If Val(grdVoucher.Item(ConstPFraction, i).Value & "") = 0 Then grdVoucher.Item(ConstPFraction, i).Value = 0
                    grdVoucher.Item(ConstFocQty, i).Value = Format(sRs(i)("Focqty") / UPerPack, "#,##0" & IIf(Val(grdVoucher.Item(ConstPFraction, i).Value & "") = 0, "", "." & Strings.StrDup(CByte(grdVoucher.Item(ConstPFraction, i).Value & ""), "0")))

                    grdVoucher.Item(ConstUPrice, i).Value = Format(sRs(i)("UnitCost") * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & lnumFormat)
                    grdVoucher.Item(ConstActualPrice, i).Value = sRs(i)("UnitCost") * UPerPack / FCRt
                    tNumformat = "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))
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
                    .Item(constItmTot, i).Value = Format((.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value), lnumFormat)

                    grdVoucher.Item(ConstImpDocId, i).Value = Val(sRs(i)("impDocid") & "")
                    grdVoucher.Item(ConstImpLnId, i).Value = Val(sRs(i)("impDocSlno") & "")
                    importdocid = Val(sRs(i)("impDocid") & "")

                    grdVoucher.Item(ConstBarcode, i).Value = sRs(i)("HSNCode")
                    grdVoucher.Item(ConstCGSTP, i).Value = sRs(i)("CSGTP")
                    grdVoucher.Item(ConstCGSTAmt, i).Value = sRs(i)("CGSTAMT") / FCRt

                    grdVoucher.Item(ConstSGSTP, i).Value = sRs(i)("SGSTP")
                    grdVoucher.Item(ConstSGSTAmt, i).Value = sRs(i)("SGSTAmt") / FCRt

                    grdVoucher.Item(ConstIGSTP, i).Value = sRs(i)("IGSTP")
                    grdVoucher.Item(ConstIGSTAmt, i).Value = sRs(i)("IGSTAmt") / FCRt
                    If enableWoodSale Then
                        grdVoucher.Item(ConstWoodQty, i).Value = Format(sRs(i)("WoodNetQty") / UPerPack, tNumformat)
                        grdVoucher.Item(ConstWoodDiscQty, i).Value = Format(sRs(i)("WoodDiscountQty") / UPerPack, tNumformat)
                    End If


                    If enableFuleBankInvoice Then
                        grdVoucher.Item(ConstMeterCode, i).Value = Trim(sRs(i)("fcode") & "")
                        If Val(sRs(i)("StartingReading") & "") = 0 Then sRs(i)("StartingReading") = 0
                        If Val(sRs(i)("EndingReading") & "") = 0 Then sRs(i)("EndingReading") = 0
                        grdVoucher.Item(ConstStartReading, i).Value = Format(sRs(i)("StartingReading"), "#,##0" & IIf(Val(grdVoucher.Item(ConstPFraction, i).Value) = 0, "", "." & Strings.StrDup(CByte(grdVoucher.Item(ConstPFraction, i).Value), "0")))
                        grdVoucher.Item(ConstEndReading, i).Value = Format(sRs(i)("EndingReading"), "#,##0" & IIf(Val(grdVoucher.Item(ConstPFraction, i).Value) = 0, "", "." & Strings.StrDup(CByte(grdVoucher.Item(ConstPFraction, i).Value), "0")))
                    End If

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
                    .Item(ConstMRP, i).Value = CDbl(sRs(i)("MRP"))
                    If Val(sRs(i)("costavg") & "") = 0 Then sRs(i)("costavg") = 0
                    .Item(ConstBatchCost, i).Value = CDbl(sRs(i)("costavg"))

                    calcualteLineTotal(i)
                Next

            End If
        End With
        If sRs.Rows.Count > 0 Then sRs.Clear()
        If importdocid > 0 Then
            sRs = _objcmnbLayer._fldDatatable("Select doctype from DocCmnTb where docid=" & importdocid)
            If sRs.Rows.Count > 0 Then
                cmbDos.Text = sRs(0)("doctype")
            End If
        End If
        calculate(, , , True)
        reArrangeNo()
        numVchrNo.Focus()
        chgbyprg = False
        ChgId = True
        chgPost = True
        isImport = False
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

            With cmbVoucherTp
                If .Items.Count > 0 Then
                    If .SelectedIndex < 0 Then .SelectedIndex = 0
                    cmbVoucherTp.Tag = getvrsId(cmbVoucherTp.Text, 4)
                    getVrsDet(Val(cmbVoucherTp.Tag), "IS", vrPrefix, vrVoucherNo, vrAccountNo1, vrAccountNo2, , , vtype)
                Else
                    getVrsDet(0, "IS", vrPrefix, vrVoucherNo, vrAccountNo1, vrAccountNo2)
                End If

            End With
            If Not isModi Then
                numVchrNo.Text = vrVoucherNo
                txtprefix.Text = vrPrefix
            End If
            If Val(txtSuppName.Tag) = 0 Then
                txtSuppAlias.Tag = vrAccountNo2
                txtSuppName.Enabled = False
            End If
            If Val(txtPurchaseName.Tag) = 0 Then
                txtPurchAlias.Tag = vrAccountNo1
            End If
            txtSuppName.Enabled = False
            'If vrAccountNo2 > 0 Then
            '    txtSuppName.Enabled = False
            'Else
            '    txtSuppName.Enabled = True
            'End If

            'dtAcc = _objcmnbLayer._fldDatatable("SELECT Alias,AccDescr,GrpSetOn,GrpSetOn,accid FROM AccMast " & _
            '"LEFT JOIN S1AccHd ON AccMast.S1AccId=S1AccHd.S1AccId WHERE accid=" & vrAccountNo1)
            Dim qry As String = "SELECT Alias,AccDescr,GrpSetOn,GrpSetOn,accid FROM AccMast " & _
            "LEFT JOIN S1AccHd ON AccMast.S1AccId=S1AccHd.S1AccId WHERE accid=" & vrAccountNo1
            qry = qry & " SELECT Alias,AccDescr,GrpSetOn,GrpSetOn,accid FROM AccMast " & _
                      "LEFT JOIN S1AccHd ON AccMast.S1AccId=S1AccHd.S1AccId WHERE accid=" & vrAccountNo2

            Dim ds As DataSet
            ds = _objcmnbLayer._ldDataset(qry, False)

            Dim dtTable As DataTable
            dtTable = ds.Tables(0)
            'Dim _qurey As EnumerableRowCollection(Of DataRow)
            '_qurey = From data In ds.Tables(0).AsEnumerable() Where data("accid") = vrAccountNo1 Select data
            'If _qurey.Count > 0 Then
            '    dtTable = _qurey.CopyToDataTable()
            'Else
            '    dtTable = dtAcc.Clone
            'End If

            If dtTable.Rows.Count > 0 Then
                txtPurchaseName.Text = dtTable(0)("AccDescr")
                txtPurchAlias.Text = dtTable(0)("Alias")
                txtPurchAlias.Tag = vrAccountNo1
            Else
                txtPurchaseName.Text = ""
                txtPurchAlias.Text = ""
                txtPurchAlias.Tag = ""
            End If
            'If vrAccountNo2 = 0 And creditCustomerACC > 0 Then
            '    vrAccountNo2 = creditCustomerACC
            'End If
            If vrAccountNo2 = 0 And vtype = "Credit" And txtCashCustomer.Text <> "" And Val(txtCashCustomer.Tag) > 0 Then
                Dim dt As DataTable = _objcmnbLayer._fldDatatable("Select * from CashCustomerTb where custid=" & Val(txtCashCustomer.Tag))
                If dt.Rows.Count > 0 Then
                    vrAccountNo2 = Val(dt(0)("customeraccount") & "")
                Else
                    MsgBox("This Customer is not a valid Credit customer", MsgBoxStyle.Exclamation)
                End If
            End If
            dtTable.Rows.Clear()
            dtTable = ds.Tables(1)
            '_qurey = From data In dtAcc.AsEnumerable() Where data("accid") = vrAccountNo2 Select data
            'If _qurey.Count > 0 Then
            '    dtTable = _qurey.CopyToDataTable()
            'Else
            '    dtTable = dtAcc.Clone
            'End If

            If dtTable.Rows.Count > 0 Then
                txtSuppName.Text = dtTable(0)("AccDescr")
                txtSuppAlias.Text = dtTable(0)("Alias")
                txtSuppAlias.Tag = vrAccountNo2
                'If vtype <> "Credit" Then setCustomer(Val(txtSuppAlias.Tag), True)
                chgbyprg = True
                If txtCashCustomer.Text = "" Then txtCashCustomer.Text = txtSuppName.Text
                chgbyprg = False
            Else
                txtSuppName.Text = ""
                txtSuppName.Text = ""
                txtSuppAlias.Tag = ""
                txtCashCustomer.Text = ""
            End If
            'txtReference.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub txtReference_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDescr.KeyDown, txtSuppName.KeyDown, txtSuppAlias.KeyDown, _
                                                                                                                    txtReference.KeyDown, txtjobname.KeyDown, txtJob.KeyDown, _
                                                                                                                    txtCashCustomer.KeyDown, txtcredit.KeyDown
        lstKey = e.KeyCode
        Dim MyCtrl As TextBox = sender
        If e.KeyCode = Keys.Return Then

            If MyCtrl.Name = "txtDescr" Then
                If enablePhoneNumberMandatory And txtphone.Text = "" Then
                    txtphone.Focus()
                Else
                    If grdVoucher.Rows.Count > 0 Then
                        activecontrolname = "grdVoucher"
                        grdVoucher.CurrentCell = grdVoucher.Item(1, grdVoucher.CurrentRow.Index)
                        grdBeginEdit()
                    Else
                        AddRow()
                    End If
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
getcust:
                    _srchTxtId = IIf(MyCtrl.Name = "txtSuppAlias", 1, 2)
                    ldSelect(1)
                Case "txtJob"
                    _srchTxtId = 3
                    ldSelect(3)
                Case "txtCashCustomer"
                    If vtype = "Cash" Then
                        fCashCust = New CreateCashCustomerFrm
                        fCashCust.ShowDialog()
                        fCashCust = Nothing
                    Else
                        GoTo getcust
                    End If

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



    Private Sub txtLocation_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtJob.TextChanged, txtSuppName.TextChanged, txtSuppAlias.TextChanged, txtcredit.TextChanged
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
                    Case 4
                        SetFmlist(fMList, 13, 0)
                    Case 5
                        SetFmlist(fMList, 30, 0) 'cash customer
                End Select
                fMList.loc = PopupLoc
                fMList.Show(fMainForm)
                fMList.Visible = True
            End If
        End If
        fMList.dvData.BackgroundColor = Color.White
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
                fMList.Search(txtCashCustomer.Text)
                txtSuppAlias.Text = fMList.AssignList(txtCashCustomer, lstKey, chgbyprg)
            Case 3   'job
                fMList.SearchIndex = 0
                'fMList.SearchIndexDescr = 0
                fMList_doFocus()
                fMList.Search(txtJob.Text)
                fMList.AssignList(txtJob, lstKey, chgbyprg)
            Case 4
                fMList.SearchIndex = 0
                fMList_doFocus()
                fMList.Search(txtcredit.Text)
                fMList.AssignList(txtcredit, lstKey, chgbyprg, False)
            Case 5
                fMList.SearchIndex = 0
                fMList.SearchIndexDescr = 2
                fMList.dvData.BackgroundColor = Color.LightGreen
                fMList_doFocus()
                fMList.Search(txtCashCustomer.Text)
                txtCashCustomer.Tag = fMList.AssignList(txtCashCustomer, lstKey, chgbyprg, False)
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
                txtCashCustomer.Text = ItmFlds(0)
            Case 3
                txtJob.Text = ItmFlds(0)
            Case 4
                txtcredit.Text = ItmFlds(0)
                txtcredit.Tag = ItmFlds(3)
            Case 5
                txtCashCustomer.Text = ItmFlds(0)
                txtCashCustomer.Tag = ItmFlds(2)
        End Select
        chgbyprg = False
    End Sub
    Private Sub SetGridHead()
        chgbyprg = True
        SetGridHeadEntryProperty(grdVoucher)
        With grdVoucher
            .Columns(Constsman).Visible = enableItemwiseSalesman
            .Columns(ConstMeterCode).Visible = enableFuleBankInvoice
            .Columns(ConstStartReading).Visible = enableFuleBankInvoice
            .Columns(ConstEndReading).Visible = enableFuleBankInvoice
            '.Columns(ConstManufacturingdate).Visible = False
            If enableFuleBankInvoice Then
                .Columns(ConstQty).Width = 100
                .Columns(ConstQty).ReadOnly = True
            ElseIf enableWoodSale Then
                .Columns(ConstQty).ReadOnly = True
            End If
            If enableBatchwiseTr Then
                .Columns(ConstWarrentyExpiry).ReadOnly = True
                .Columns(ConstManufacturingdate).ReadOnly = True
            End If
            .Columns(ConstTaxP).HeaderText = "%"
            .Columns(ConstTaxAmt).HeaderText = "Inst. Amt"
            Dim i As Integer
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
            txtCashCustomer.Text = strFld1
            txtCashCustomer.Tag = 0
        ElseIf _srchTxtId = 3 Then
            txtJob.Text = strFld1
        End If
        chgbyprg = False
    End Sub

    Private Sub AddRow(Optional ByVal tocheck As Boolean = False)
        Dim i As Integer
        'ChgByPrg = True

        If grdVoucher.RowCount > 0 Then
            If Val(grdVoucher.Item(ConstItemID, grdVoucher.RowCount - 1).Value) = 0 And grdVoucher.Item(ConstItemCode, grdVoucher.RowCount - 1).Value = "" And Val(grdVoucher.Item(ConstSlNo, grdVoucher.RowCount - 1).Value) <> 0 Then
                grdVoucher.CurrentCell = grdVoucher.Item(ConstItemCode, grdVoucher.RowCount - 1)
                Exit Sub
            End If

        End If
        With grdVoucher
            activecontrolname = "grdVoucher"
            i = .RowCount '- 1
            .Rows.Add(1)
            'chgbyprg = True
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
            '
            'exitFromValidProc = True
            If Not grdVoucher.CurrentRow Is Nothing Then
                If Val(grdVoucher.Item(ConstItemID, grdVoucher.CurrentRow.Index).Value) > 0 Then chgItm = False
            End If
            .CurrentCell = .Item(ConstItemCode, i)
            'exitFromValidProc = False
            SrchText = "" ' .Item(ConstItemCode, i).Value
            chgbyprg = True
            .BeginEdit(True)
            chgbyprg = False
            chgItm = False
        End With

        'calculate()
        reArrangeNo()
        'ChgByPrg = False
        ChgId = True
        'If enableItemAutoPopulate Then
        '    fProductEnquiry = New ItmEnqry
        '    fProductEnquiry.ShowDialog()
        'End If
        'If dtInvItm Is Nothing Then
        '    dtInvItm = returnAllItem()
        'End If
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
            If enableFloodCess And Val(lblgstn.Tag & "") = 0 And cessenddate >= DateValue(cldrdate.Value) Then
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
    Public Sub calculate(Optional ByVal bFrmCalcOthCost As Boolean = False, Optional ByVal calculateLineTotal As Boolean = False, Optional ByVal chgDiscount As Boolean = False, Optional ByVal blockAutoRoundOff As Boolean = False)
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
                .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstIGSTAmt, i).Value), lnumFormat)
                .Item(ConstTaxP, i).Value = Format(CDbl(.Item(ConstIGSTP, i).Value), lnumFormat)
                totTax = totTax + CDbl(.Item(ConstTaxAmt, i).Value)
                'If chktaxInv.Checked = False Then
                '    .Item(ConstTaxAmt, i).Value = Format(0, lnumFormat)
                '    .Item(ConstTaxP, i).Value = Format(0, lnumFormat)
                '    .Item(ConstcessAmt, i).Value = 0
                'Else
                '    .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstIGSTAmt, i).Value), lnumFormat)
                '    .Item(ConstTaxP, i).Value = Format(CDbl(.Item(ConstIGSTP, i).Value), lnumFormat)
                '    If enablecess Then
                '        lnTax = CDbl(.Item(ConstregularCessAmt, i).Value)
                '        'lnTax = ((actualPrice * .Item(Constcess, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value)
                '        'lnTax = lnTax + (CDbl(.Item(ConstAdditionalcess, i).Value) * CDbl(.Item(ConstQty, i).Value))
                '    End If
                '    If enableFloodCess And Val(lblgstn.Tag & "") = 0 And cessenddate >= DateValue(cldrdate.Value) Then
                '        'lnTax = lnTax + ((actualPrice * .Item(ConstFloodcess, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value)
                '        lnTax = lnTax + CDbl(.Item(ConstFloodCessAmt, i).Value)
                '    End If
                '    .Item(ConstcessAmt, i).Value = lnTax ' Format(lnTax, lnumFormat)
                '    lnTax = lnTax + CDbl(.Item(ConstIGSTAmt, i).Value)
                'End If
                'If EnableGST Then
                '    If chktaxInv.Checked Then
                '        totTax = totTax + CDbl(.Item(ConstCGSTAmt, i).Value) + CDbl(.Item(ConstSGSTAmt, i).Value)
                '    End If
                'Else
                '    If chktaxInv.Checked Then

                '    End If
                'End If

                totLnDis = totLnDis + .Item(ConstDisAmt, i).Value
                totAmt = totAmt + (CDbl(.Item(ConstQty, i).Value) * (CDbl(.Item(ConstActualPrice, i).Value) - IIf(enableAdjustDiscountOnTaxTotal, CDbl(.Item(ConstDiscOther, i).Value), 0))) - CDbl(.Item(ConstDisAmt, i).Value)
                totItm = totItm + (CDbl(.Item(ConstQty, i).Value) * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value)
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
            If Val(txtsmanP.Text) > 0 Then
                txtscamt.Text = Format(CDbl(lblNetAmt.Text) * CDbl(txtsmanP.Text) / 100, lnumFormat)
            End If
            lbltax.Text = Format(totTax, lnumFormat)
            lblcess.Text = Format(totCess, lnumFormat)
            totAmt = totAmt - CDbl(numDisc.Text)
            lbltaxable.Text = Format(totTaxableAmt, lnumFormat)
            lbltotalwithOC.Text = Format(CDbl(lblNetAmt.Text) + CDbl(lblOthCost.Text), lnumFormat)
            chgAmt = False
            chgbyprg = False
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
                    End If
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
                    UpdateClick()
                ElseIf msg.WParam.ToInt32() = CInt(Keys.F5) Then
                    ClearClick()
                ElseIf msg.WParam.ToInt32() = CInt(Keys.F3) Then
                    grdVoucher_KeyDown(Nothing, New KeyEventArgs(keyData))
                ElseIf activecontrolname = "grdVoucher" Then
                    If msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up) Or msg.WParam.ToInt32() = CInt(Keys.Enter) Or msg.WParam.ToInt32() = CInt(Keys.F2) Or msg.WParam.ToInt32() = CInt(Keys.F3) Or msg.WParam.ToInt32() = CInt(Keys.F4) Or msg.WParam.ToInt32() = CInt(Keys.F6) Then
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
        If e.RowIndex < 0 Then Exit Sub
        chgbyprg = True
        With grdVoucher
            If .Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.LightGray Then
                If e.ColumnIndex <> ConstDescr Or e.ColumnIndex <> ConstUPrice Then
                    grdVoucher.CurrentCell.ReadOnly = True
                    Exit Sub
                End If
            End If
            If Val(grdVoucher.Item(0, grdVoucher.CurrentCell.RowIndex).Value) = 0 And e.ColumnIndex <> 3 Then
                grdVoucher.CurrentCell.ReadOnly = True
            ElseIf e.ColumnIndex <> ConstSlNo And e.ColumnIndex <> constItmTot And e.ColumnIndex <> ConstUnit And e.ColumnIndex <> ConstLTotal And e.ColumnIndex <> ConstBarcode And e.ColumnIndex <> ConstTaxAmt And e.ColumnIndex <> ConstStartReading And e.ColumnIndex <> ConstcessAmt Then
                If e.ColumnIndex = ConstTaxP And EnableGST Then
                    grdVoucher.CurrentCell.ReadOnly = True
                ElseIf e.ColumnIndex = ConstQty And grdVoucher.Item(ConstMeterCode, grdVoucher.CurrentCell.RowIndex).Value <> "" Then
                    grdVoucher.CurrentCell.ReadOnly = True
                ElseIf e.ColumnIndex = ConstQty And enableWoodSale Then
                    grdVoucher.CurrentCell.ReadOnly = True
                ElseIf e.ColumnIndex = ConstWarrentyExpiry And enableBatchwiseTr Then
                    grdVoucher.CurrentCell.ReadOnly = True
                ElseIf e.ColumnIndex = ConstManufacturingdate Then
                    grdVoucher.CurrentCell.ReadOnly = True
                ElseIf e.ColumnIndex = ConstDescr And disableEditProdectDescription Then
                    grdVoucher.CurrentCell.ReadOnly = True
                Else
                    grdVoucher.CurrentCell.ReadOnly = False
                End If
            ElseIf e.ColumnIndex = ConstSlNo And enableBatchwiseTr Then
                grdVoucher.CurrentCell.ReadOnly = False

            Else
                grdVoucher.CurrentCell.ReadOnly = True
            End If
        End With
        grdBeginEdit()
    End Sub

    Private Sub grdVoucher_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellValidated
        'If chgbyprg Then Exit Sub
        Try
            'MsgBox(grdVoucher.Item(ConstDonotAllowsaveItem, 0).Value)
            Valid(e.RowIndex, e.ColumnIndex)
            SrchText = ""
            'MsgBox("satus : " & grdVoucher.Item(ConstDonotAllowsaveItem, 0).Value)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try

    End Sub
    Private Sub Valid(ByVal RowIndex As Integer, ByVal ColIndex As Integer)
        If exitFromValidProc Then Exit Sub
        With grdVoucher

            Select Case ColIndex
                Case ConstItemCode
                    If Not chgItm Then Exit Sub
                    If Trim(.Item(ColIndex, RowIndex).Value) = "" And SrchText <> "" Then .Item(ColIndex, RowIndex).Value = SrchText
                    If Trim(.Item(ColIndex, RowIndex).Value) <> "" And SrchText = "" Then SrchText = .Item(ColIndex, RowIndex).Value

                    Dim found As Boolean
                    Dim dtItms As DataTable
                    'dtItms = getItmDtls(3, SrchText, True)
                    dtItms = ItmValidation(3, SrchText, True, "IS", Val(txtSuppAlias.Tag))
                    If dtItms.Rows.Count > 0 Then
                        found = True
                        AddDetails(dtItms)
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
                        Dim dt As DataTable
                        dt = _objcmnbLayer._fldDatatable("Select isnull(fending,0) fending,isnull(fstarting,0) fstarting from FuelMeterReadingTb where fcode='" & .Item(ConstMeterCode, RowIndex).Value & "'")
                        If dt.Rows.Count > 0 Then
                            .Item(ConstStartReading, RowIndex).Value = Format(IIf(CDbl(dt(0)("fending")) = 0, CDbl(dt(0)("fstarting")), CDbl(dt(0)("fending"))), flnumformat)
                        End If
                    End If
                Case ConstEndReading
                    If Val(.Item(ConstEndReading, RowIndex).Value) = 0 Then .Item(ConstEndReading, RowIndex).Value = 0
                    If CDbl(.Item(ConstStartReading, RowIndex).Value) > CDbl(.Item(ConstEndReading, RowIndex).Value) Then
                        MsgBox("End Reading should be greater than Start Reading", MsgBoxStyle.Exclamation)
                        .Item(ConstEndReading, RowIndex).Value = CDbl(.Item(ConstStartReading, RowIndex).Value)
                    End If
                    .Item(ConstQty, RowIndex).Value = Format(CDbl(.Item(ConstEndReading, RowIndex).Value) - CDbl(.Item(ConstStartReading, RowIndex).Value), "#,##0" & IIf(Val(.Item(ConstPFraction, RowIndex).Value) = 0, "", "." & Strings.StrDup(CByte(.Item(ConstPFraction, RowIndex).Value), "0")))
                    calculate(, True)
                Case ConstWoodQty, ConstWoodDiscQty
                    If chgAmt Then
                        calculateWoodDiscountQty(RowIndex)
                        calculate(, True)
                    End If
                Case ConstQty

                    checkItemQty(RowIndex, True, False)
                    .Item(ConstBatchCost, RowIndex).Value = 0
                    If enableBatchwiseTr And Trim(.Item(ConstSerialNo, RowIndex).Value & "") <> "" Then checkBatchQty(RowIndex)
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
                        checkItemQty(RowIndex, False, True)
                        If Format(.Item(ConstActualPrice, RowIndex).Value, lnumFormat) <> Format(CDbl(.Item(ConstUPrice, RowIndex).Value), lnumFormat) Then
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
                Case ConstSerialNo
                    If enableBatchwiseTr Then
                        Dim dt As DataTable
                        If SrchText <> "" Then .Item(ConstSerialNo, RowIndex).Value = SrchText
                        If Trim(.Item(ConstSerialNo, RowIndex).Value & "") <> "" Then
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
        chgAmt = False
    End Sub
    Private Sub checkBatchQty(ByVal i As Integer)
        Dim bqty As Double
        Dim grdqty As Double
        Dim r As Integer
        With grdVoucher
            If .Item(ConstDonotAllowsaveItem, i).Value = 1 Then Exit Sub
            bqty = _objInv.returnBatchQty(.Item(ConstItemID, i).Value, .Item(ConstSerialNo, i).Value)
            For r = 0 To .RowCount - 1
                If r <> i And .Item(ConstItemID, r).Value = .Item(ConstItemID, i).Value And .Item(ConstSerialNo, r).Value = .Item(ConstSerialNo, i).Value Then
                    grdqty = grdqty + Val(.Item(ConstQty, r).Value)
                End If
            Next
            bqty = bqty - grdqty
            If .Item(ConstQty, i).Value > bqty Then
                If diableNegativeSale Then
                    MsgBox("Entered qty exceeds quantity in Batch!" & vbCrLf & " You have been protected from -ve quantity entry" & vbCrLf & "Available quantity : " & bqty, MsgBoxStyle.Exclamation)
                    .Item(ConstDonotAllowsaveItem, i).Value = 1
                Else
                    If MsgBox("Entered qty exceeds quantity in Batch!" & vbCrLf & " Do you want to continue?" & vbCrLf & "Available quantity : " & bqty, MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton3) = MsgBoxResult.No Then
                        .Item(ConstDonotAllowsaveItem, i).Value = 1
                        .Item(ConstQty, r).Value = 0
                    Else
                        .Item(ConstDonotAllowsaveItem, i).Value = 0
                    End If
                End If
            Else
                .Item(ConstDonotAllowsaveItem, i).Value = 0
            End If
        End With

    End Sub
    Private Sub checkItemQty(ByVal i As Integer, ByVal checkqty As Boolean, ByVal checkcost As Boolean)
        'If Not enableNegativeQtyAlert Then Exit Sub
        Dim qty As Double
        Dim itemexceeds As Integer
        With grdVoucher
            Dim dtQty As DataTable
            Dim unitprice As Double
            Dim locqtyQTY As String
            If UsrBr = "" Then
                dtQty = _objcmnbLayer._fldDatatable("SELECT isnull(AsOnQty,0)+isnull(opQty,0) AsOnQty,CostAvg,LastPurchCost Cost FROM InvItm left join " & _
                                                    "(SELECT SUM(CASE WHEN InvType in ('IN') then 1 else -1 end  * isnull(trqty,0)) AsOnQty ,Itemid TItemid from " & _
                                                    "(SELECT InvType,trqty,TrDateNo,Itemid FROM ItmInvTrtb LEFT JOIN VoucherTypeNoTb ON ItmInvTrTb.TrTypeNo=VoucherTypeNoTb.vrno " & _
                                                    "LEFT JOIN ItmInvCmnTb ON  ItmInvTrTb.TRID=ItmInvCmnTb.TRID  WHERE ItmInvTrTb.TRID<>" & loadedTrId & " AND  isnull(invStatus,0)=0) tr  group by Itemid) tr ON INVITM.Itemid=tr.TItemid where itemid=" & Val(.Item(ConstItemID, i).Value))
            Else
                Dim str As String = " select isnull(Locqty,0)+isnull(opnQty,0)AsOnQty,isnull(lastcost,isnull(locationCost,isnull(LastPurchCost,0))) Cost from invitm " & _
                "left join  (SELECT SUM(CASE WHEN InvType in ('IN') then 1 else -1 end  * isnull(qty,0)) Locqty ,Itemid ,FromLoc loc,sum(isnull(opnQty,0))opnQty,sum(isnull(lastcost,0)) lastcost,sum(locationCost)locationCost from " & _
                "(SELECT 'IN' InvType,isnull(LocOpnQtyTb.qty,0)qty,0 TrDateNo,Itemid,LocCode FromLoc,Qty opnQty,lastcost,locationCost FROM  LocOpnQtyTb " & _
                "LEFT JOIN LocationTb ON LocationTb.locationid=LocOpnQtyTb.locationid " & _
                "UNION ALL " & _
                "SELECT 'OUT' InvType,case when doctype='DOC' AND 0='0' then 0 else qty end qty,0 TrDateNo,Itemid,FromLoc,0,0,0 " & _
                "FROM DocTranTb LEFT JOIN DocCmnTb ON  DocTranTb.DocId=DocCmnTb.DocId where doctype in ('DOC','MTN') " & _
                "UNION ALL " & _
               "SELECT 'IN' InvType,case when doctype='DOC' AND 0='0' then 0 else qty end qty,0 TrDateNo,Itemid,DocDefLoc,0,0,0 FROM DocTranTb " & _
               "LEFT JOIN DocCmnTb ON  DocTranTb.DocId=DocCmnTb.DocId where doctype in ('DOS','MTN') " & _
               " UNION ALL " & _
               "SELECT InvType,trqty+isnull(focqty,0) trqty,TrDateNo,Itemid,DocDefLoc,0,0,0 FROM ItmInvTrtb LEFT JOIN VoucherTypeNoTb ON ItmInvTrTb.TrTypeNo=VoucherTypeNoTb.vrno " & _
               "LEFT JOIN ItmInvCmnTb ON  ItmInvTrTb.TRID=ItmInvCmnTb.TRID  WHERE isnull(invStatus,0)=0 and ItmInvTrTb.TRID<>" & loadedTrId & _
               ")Loc where isnull(FromLoc,'')='" & UsrBr & "'  group by Itemid,FromLoc)tr " & _
               "on invitm.itemid=tr.itemid	where invitm.Itemid=" & Val(.Item(ConstItemID, i).Value)
                dtQty = _objcmnbLayer._fldDatatable(str)
            End If
            If Val(.Item(ConstQty, i).Value) > 0 Then
                qty = CDbl(.Item(ConstQty, i).Value)
            Else
                qty = 0
            End If
            If Val(.Item(ConstUPrice, i).Value) > 0 Then
                unitprice = CDbl(.Item(ConstUPrice, i).Value)
            Else
                unitprice = 0
            End If
            If dtQty.Rows.Count > 0 Then
                If dtQty(0)("AsOnQty") - qty < 0 And enableNegativeQtyAlert And checkqty Then
                    If diableNegativeSale Then
                        MsgBox("Entered qty exceeds quantity in hand!" & vbCrLf & " You have been protected from -ve quantity entry" & vbCrLf & "Available quantity : " & dtQty(0)("AsOnQty"), MsgBoxStyle.Exclamation)
                        .Item(ConstDonotAllowsaveItem, i).Value = 1
                        itemexceeds = 1
                    Else
                        If MsgBox("Entered qty exceeds quantity in hand! Do you want continue?" & vbCrLf & "Available quantity : " & dtQty(0)("AsOnQty"), MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                            .Item(ConstDonotAllowsaveItem, i).Value = 1
                            itemexceeds = 1
                        Else
                            .Item(ConstDonotAllowsaveItem, i).Value = 0
                        End If
                    End If

                Else
                    .Item(ConstDonotAllowsaveItem, i).Value = Val(.Item(ConstDonotAllowsaveItem, i).Value)
                End If

                If dtQty(0)("Cost") > unitprice And EnableAlertBelowcost And checkcost Then
                    If disableBelowcost Then
                        MsgBox("Entered price is below cost!" & vbCrLf & " You have been protected from below cost entry", MsgBoxStyle.Exclamation)
                        .Item(ConstDonotAllowsaveItem, i).Value = 2
                    Else
                        If MsgBox("Entered price is below cost! Do you want to continue?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                            .Item(ConstDonotAllowsaveItem, i).Value = 2
                        Else
                            .Item(ConstDonotAllowsaveItem, i).Value = itemexceeds
                        End If
                    End If
                Else
                    .Item(ConstDonotAllowsaveItem, i).Value = itemexceeds
                End If
            End If
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
            '.Item(ConstDisP, i).Value = Format(getCustomerDiscount(Val(.Item(ConstItemID, i).Value)), numFormat)
            Dim bcost As Double
            Dim itemcost As Double
            If Not enableBatchwiseTr Then
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
                        If Val(DR(0)("locationLastCost") & "") = 0 Then
                            bcost = itemcost
                        Else
                            bcost = DR(0)("locationLastCost")
                        End If
                    Else
                        bcost = DR(0)("locationCost")
                    End If
                End If
                If bcost > 0 Then
                    itemcost = bcost
                End If
            End If
            .Item(ConstBatchCost, i).Value = 0
            .Item(ConstDisP, i).Value = Format(CDbl(DR(0)("discount")), numFormat)
            If CDbl(.Item(ConstUPrice, i).Value) = 0 Or chgItm Then
                If chkws.Checked Then
                    .Item(ConstActualPrice, i).Value = DR(0)("UnitPriceWS")
                ElseIf chksecondprice.Checked Then
                    .Item(ConstActualPrice, i).Value = DR(0)("secondPrice")
                ElseIf enableFuleBankInvoice Then
                    .Item(ConstActualPrice, i).Value = getLastSalesAmt(0, DR(0)("ItemId"), False, "IS")
                ElseIf chkretailprice.Checked Then
                    If Val(DR(0)("UnitPrice") & "") = 0 Then DR(0)("UnitPrice") = 0
                    If isNotCustomerAccount Then
                        .Item(ConstActualPrice, i).Value = 0
                    Else
                        .Item(ConstActualPrice, i).Value = IIf(enablefetchLastPrice, DR(0)("lastPrice"), DR(0)("UnitPrice")) ' getLastSalesAmt(Val(txtSuppAlias.Tag), DR(0)("ItemId"), True, "IS", CDbl(DR(0)("UnitPrice")))
                    End If
                    If Val(.Item(ConstActualPrice, i).Value) = 0 Then
                        .Item(ConstActualPrice, i).Value = DR(0)("UnitPrice")
                    End If
                Else
                    .Item(ConstActualPrice, i).Value = 0
                    .Item(ConstUPrice, i).Value = Format(CDbl(.Item(ConstActualPrice, i).Value), lnumFormat)
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
            .Item(ConstDisAmt, i).Value = Format(((CDbl(.Item(ConstActualPrice, i).Value) * Val(.Item(ConstDisP, i).Value)) / 100) * CDbl(.Item(ConstQty, i).Value), lnumFormat)
            .Item(ConstLTotal, i).Value = Format(.Item(ConstQty, i).Value * CDbl(.Item(ConstUPrice, i).Value), lnumFormat)
            .Item(ConstNUPrice, i).Value = Format(CDbl(.Item(ConstUPrice, i).Value), lnumFormat)
            .Item(ConstQty, i).Value = Format(CDbl(.Item(ConstQty, i).Value), "#,##0" & IIf(Val(.Item(ConstPFraction, i).Value) = 0, "", "." & Strings.StrDup(CByte(.Item(ConstPFraction, i).Value), "0")))
            .Item(ConstImpDocId, i).Value = ""
            .Item(ConstImpLnId, i).Value = ""
            chgAmt = True
            chgAmt = True
            chgItm = False
            .ClearSelection()
            checkItemQty(i, True, IIf(userType, True, False))
            'If diableNegativeSale Then
            '    .CurrentCell = .Item(ConstQty, i)
            'End If

        End With
        calculate(, True)
        Dim a As Integer = grdVoucher.Item(ConstDonotAllowsaveItem, i).Value
        chgbyprg = False
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
            If enablecess Or (enableFloodCess And Val(lblgstn.Tag & "") = 0 And cessenddate >= DateValue(cldrdate.Value)) Then
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
                    If enableFloodCess And Val(lblgstn.Tag & "") = 0 And cessenddate >= DateValue(cldrdate.Value) Then
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
            For i = 0 To grdOtherCost.RowCount - 1
                Dim b = (From data In dtTax.AsEnumerable() Where data("ACid") = Val(grdOtherCost.Item(CostvatAcc, i).Value & "") Select data("slno"))
                slno = 0
                For Each itm In b
                    slno = itm
                Next
                If slno > 0 Then
                    taxamt = CDbl(grdOtherCost.Item(CostAmount, i).Value) * CDbl(grdOtherCost.Item(CostvatOther, i).Value) / 100
                    dtTax(slno - 1)("Amount") = CDbl(dtTax(slno - 1)("Amount")) + taxamt
                End If
            Next
        End If


    End Sub
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
                    chgAmt = True
                Case ConstLTotal
                    'If Val(grdVoucher.Item(ConstQty, i).Value) > 0 Then
                    '    If Val(grdVoucher.Item(ConstDisAmt, i).Value) = 0 And CDbl(grdVoucher.Item(ConstTaxAmt, i).Value) = 0 Then 'Not AllowUnitDiscountEntryOnInventory And Not ShowTaxOnInventory And
                    '        chgbyprg = True
                    '        grdVoucher.Item(ConstUPrice, i).Value = Format(CDbl(grdVoucher.Item(ConstLTotal, i).Value) / grdVoucher.Item(ConstQty, i).Value, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) 'IIf(IsReturn, -1, 1)
                    '        grdVoucher.Item(ConstActualPrice, i).Value = CDbl(grdVoucher.Item(ConstLTotal, i).Value) / grdVoucher.Item(ConstQty, i).Value
                    '        calculate(, True)
                    '        chgbyprg = False
                    '    End If
                    'End If
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
                _srchTxtId = 1
                '_srchIndexId = 1
                chgbyprg = True
                strGridSrchString = MyCtrl.Text
                ShowPanel()
                chgItm = True
                chgbyprg = False
                grdVoucher.Item(ConstqtyChg, grdVoucher.CurrentRow.Index).Value = "Chg"
            ElseIf col = ConstBarcode Then
                _srchTxtId = 2
                '_srchIndexId = 2
                chgbyprg = True
                strGridSrchString = MyCtrl.Text
                ShowPanel()
                chgbyprg = False
            ElseIf col = ConstSerialNo Then
                _srchTxtId = 3
                '_srchIndexId = 3
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
            x = grdVoucher.Left + grdVoucher.Width - plsrch.Width
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
        If chgbyprg Then Exit Sub
        If isModi And loadedTrId = 0 Then Exit Sub
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
                'MsgBox("Keydown : " & grdVoucher.Item(ConstDonotAllowsaveItem, 0).Value)
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
                Dim qtyFlag As Boolean
                Dim batchflag As Boolean
                If grdVoucher.CurrentCell.ColumnIndex = ConstQty Then
                    qtyFlag = True
                ElseIf grdVoucher.CurrentCell.ColumnIndex = ConstSerialNo Then
                    batchflag = True
                End If
                'If batchflag Then
                '    grdVoucher.Item(ConstSerialNo, grdVoucher.CurrentRow.Index).Value = SrchText
                'End If
                'grdVoucher.Item(ConstDonotAllowsaveItem, grdVoucher.CurrentCell.RowIndex).Value = 0

                Dim nextlineColum As Integer
                Select Case ISNextline
                    Case 1
                        nextlineColum = ConstItemCode
                    Case 2
                        nextlineColum = ConstDescr
                    Case 3
                        nextlineColum = ConstQty
                    Case Else
                        nextlineColum = 0
                End Select
                If nextlineColum = grdVoucher.CurrentCell.ColumnIndex Then
                    AddRow()
                Else
                    If FindNextCell(grdVoucher, grdVoucher.CurrentCell.RowIndex, grdVoucher.CurrentCell.ColumnIndex + 1) Then
                        AddRow()
                    End If
                End If


                'If FindNextCell(grdVoucher, grdVoucher.CurrentCell.RowIndex, grdVoucher.CurrentCell.ColumnIndex + 1) Or enableNextlineonItemcode Then
                '    AddRow()
                'End If
                If qtyFlag And Val(grdVoucher.Item(ConstDonotAllowsaveItem, grdVoucher.CurrentCell.RowIndex).Value) = 1 Then
                    grdVoucher.CurrentCell = grdVoucher.Item(ConstQty, grdVoucher.CurrentCell.RowIndex)
                End If

                If batchflag And Val(grdVoucher.Item(ConstDonotAllowsaveItem, grdVoucher.CurrentCell.RowIndex).Value) = 1 Then
                    exitFromValidProc = True
                    grdVoucher.CurrentCell = grdVoucher.Item(ConstSerialNo, grdVoucher.CurrentCell.RowIndex)
                    exitFromValidProc = False
                End If
nxt:

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
            ElseIf e.KeyCode = Keys.F6 Then
                modifyItem(Val(grdVoucher.Item(ConstItemID, grdVoucher.CurrentRow.Index).Value))
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
        calculate(, True)
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
                    'grdVoucher.Item(ConstSerialNo, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(2) IsNot Nothing, ItmFlds(2), "")
                    ''grdVoucher.Item(ConstBarcode, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(1) IsNot Nothing, ItmFlds(1), "")
                    'grdVoucher.Item(ConstManufacturingdate, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(3) IsNot Nothing, ItmFlds(3), "")
                    'grdVoucher.Item(ConstWarrentyExpiry, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(4) IsNot Nothing, ItmFlds(4), "")
                    'grdVoucher.Item(ConstBatchQty, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(1) IsNot Nothing, ItmFlds(1), "")
                    'grdVoucher.Item(ConstMRP, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(6) IsNot Nothing, ItmFlds(6), "")
                    'grdVoucher.Item(ConstBatchCost, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(5) IsNot Nothing, ItmFlds(5), "")
                    'grdVoucher.Item(ConstActualPrice, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(7) IsNot Nothing, ItmFlds(7), "")
                    'If Val(grdVoucher.Item(ConstActualPrice, grdVoucher.CurrentCell.RowIndex).Value) = 0 Then grdVoucher.Item(ConstActualPrice, grdVoucher.CurrentCell.RowIndex).Value = 0
                    'grdVoucher.Item(ConstUPrice, grdVoucher.CurrentCell.RowIndex).Value = Format(CDbl(grdVoucher.Item(ConstActualPrice, grdVoucher.CurrentCell.RowIndex).Value), numFormat)
                    'SrchText = IIf(ItmFlds(2) IsNot Nothing, ItmFlds(2), strGridSrchString)
                    'calculate(, True)
                    If Trim(ItmFlds(2) & "") <> "" Then
                        grdVoucher.Item(ConstBatchQty, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(1) IsNot Nothing, ItmFlds(1), "")
                        grdVoucher.Item(ConstSerialNo, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(2) IsNot Nothing, ItmFlds(2), "")
                        grdVoucher.Item(ConstMRP, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(3) IsNot Nothing, ItmFlds(3), "")
                        If chkretailprice.Checked Then
                            grdVoucher.Item(ConstActualPrice, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(4) IsNot Nothing, ItmFlds(4), "")
                        ElseIf chkws.Checked Then
                            grdVoucher.Item(ConstActualPrice, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(5) IsNot Nothing, ItmFlds(5), "")
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
            If Not fshowlocationqty Is Nothing Then
                If fshowlocationqty.Visible = True Then
                    showlocationwise(e.RowIndex)
                End If
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
                getItemInfo(0)
                calculate(, True)
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
        chgbyprg = True
        grdVoucher.RowCount = 0
        'For i = 0 To 10
        '    AddRow(True)
        'Next
        lnumFormat = numFormat
        If Not dtSetoffTable Is Nothing Then dtSetoffTable.Rows.Clear()
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
        cmbdeliveredBy.Text = ""
        btnOthrOk.Tag = ""
        chktaxwithoutLinediscount.Checked = False
        creditCustomerACC = 0
        chgDoc = False
        'fMainForm.lblUser.Text = CurrentUser
        'fMainForm.lblModiUser.Text = ""
        OthCost = 0
        chgNumByPgm = True
        chgbyprg = True
        txtCashCustomer.Text = ""
        numDisc.Text = Format(0, lnumFormat)
        txtdp.Text = Format(0, lnumFormat)

        txtscamt.Text = Format(0, lnumFormat)
        txtsmanP.Text = Format(0, lnumFormat)
        cmbfc.Text = ""
        txtfcrt.Text = Format(0, lnumFormat)
        chgNumByPgm = True
        txtroundOff.Text = Format(0, lnumFormat)
        chgNumByPgm = False
        cmbsalesman.Text = ""
        lbltaxable.Text = 0
        If dtMultipleDebits.Rows.Count > 0 Then dtMultipleDebits.Rows.Clear()
        OthCost = 0
        CTVol = 0
        CTWt = 0
        CTQty = 0
        loadedTrId = 0
        LddImpDocs = ""
        isNotCustomerAccount = False
        If cmblocation.Text = "" Then
            cmblocation.Text = Dloc
        End If

        If dtItemInfo.Rows.Count > 0 Then dtItemInfo.Rows.Clear()
        'setImport(True)
        calculate()
        chgbyprg = True
        doCommandStat(False)
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
        txtDOLst.Text = ""
        lblbalance.Text = Format(0, numFormat)
        lbllimit.Text = Format(0, numFormat)
        lblInvoices.Text = Format(0, numFormat)
        If enableFuleBankInvoice Then loadSalesMultipleDebits(0)
        If enableGCC Then
            lblgstn.Text = "TIN: "
        Else
            lblgstn.Text = "GSTN:"
        End If

        lblgstn.Tag = 0
        btnupdate.Enabled = True
        btndelete.Enabled = True
        clrDuedate.Value = Format(Date.Now, DtFormat)
        cldrdate.Value = Format(Date.Now, DtFormat)
        btnRv.Visible = False
        grdOtherCost.Rows.Clear()
        lblOthCost.Text = Format(0, numFormat)
        clearOtherCostFileds()
        setPrice()
        chgbyprg = True
        chkautoroundOff.Checked = enableAutoRoundOff
        chgbyprg = False
        txtphone.Text = ""
        loadAddInfoCap()
        txtinfo1.Text = ""
        txtinfo2.Text = ""
        txtinfo3.Text = ""
        txtinfo4.Text = ""
        txtinfo5.Text = ""
        txtrvno.Text = ""
        txtrvno.Tag = ""
        isModi = False
        'add_Click()
        'grdVoucher.Select()
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
            If chgPost Then
                Select Case MsgBox("Changes found !!  Do you want hold changes ?", vbYesNoCancel + vbQuestion)
                    Case vbYes
                        'Hold procedure
                    Case vbNo
                    Case Else
                        Exit Sub
                End Select
            End If

            ClearControls()
            numVchrNo.ReadOnly = False
            numVchrNo.Focus()
            btnNext.Visible = False
            btnSlct.Visible = True
            btnModify.Text = "&Undo"
            btndelete.Text = "Delete"
            isModi = True
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
        'If txtReference.Text = "" Then
        '    MsgBox("Invalid Reference", MsgBoxStyle.Exclamation)
        '    txtReference.Focus()
        '    Exit Sub
        'End If
        If DateValue(cldrdate.Value) <= ProtectUntil Then
            MsgBox("Voucher Date comes within Protected Date Range !!", MsgBoxStyle.Exclamation)
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
            If txtCashCustomer.Text = "" Then
                MsgBox("Enter a Customer name !!", vbExclamation)
                txtCashCustomer.Focus()
                Exit Sub
            End If
            _vAcMaster = _objcmnbLayer._fldDatatable(StrAccMastSrch & " WHERE Alias = '" & txtSuppAlias.Text & "' ORDER BY AccDescr")
            If _vAcMaster.Rows.Count = 0 Then
                MsgBox("Debit A/C could not found!", MsgBoxStyle.Exclamation)
                txtSuppName.Focus()
                'txtSuppAlias.Focus()
                Exit Sub
            Else
                txtSuppAlias.Tag = _vAcMaster(0)("accid")
            End If
        End If

        If Val(txtPurchAlias.Tag) = 0 Then
            MsgBox("Sales A/C could not found!", MsgBoxStyle.Exclamation)
            cmbVoucherTp.Focus()
            Exit Sub
        End If
        If txtphone.Text = "" And enablePhoneNumberMandatory Then
            MsgBox("Customer phone number is mandatory!", MsgBoxStyle.Exclamation)
            txtphone.Focus()
            Exit Sub
        End If
        If blockInvoicing() Then Exit Sub
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
        If CDbl(lblNetAmt.Text) < 0 Then
            MsgBox("Net Amount below Zero is not allowed !!!?", vbExclamation)
            MyActiveControl = numDisc
            Exit Sub
        End If
        If enableBranch And UsrBr = "" Then
            MsgBox("Transaction cannot be saved without Branch! Please login with Branch", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If enableMultipleDebitAutoPopulate And vtype = "Credit" And isModi = False Then
            If Not showMultipleDebits() Then Exit Sub
            saveTrans()
        Else
            If MsgBox("Verification is succeeded. Do you like to file it ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = vbYes Then
                'saveTrans()
                loadWaite(1)
            End If
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

    Private Sub saveTrans()
        Me.Cursor = Cursors.WaitCursor
        Dim TrId As Long
        Dim i As Integer
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
            If Not CheckNoExists(txtprefix.Text, Val(numVchrNo.Text), "IS", "Inventory") Then
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
        Dim paidamt As Double
        If enableMultipleDebitInInvoice Then
            For i = 0 To dtMultipleDebits.Rows.Count - 1
                If Val(dtMultipleDebits(i)("accAmt") & "") > 0 Then
                    paidamt = paidamt + CDbl(dtMultipleDebits(i)("accAmt"))
                End If
            Next
        End If
        setInvCmnValue(TrId, paidamt)
        TrId = Val(_objInv._saveCmn())
        Save_instalment(TrId)
        '_objInv.closeDlayerConnection()

        Dim itemidsdatatable As New DataTable
        itemidsdatatable.Columns.Add(New DataColumn("Itemid", GetType(Long)))
        TDrAmt = saveInvTr(TrId)
        Dim strqry As String = ""
        'If isModi Then
        '    _objInv.deleteInventoryRelatedItemDetails(loadedTrId)
        'End If
        UpdateAccounts(TrId, TDrAmt, DiscAcc)

        Me.Cursor = Cursors.Default
        If isModi = False Then
            numPrintVchr.Text = numVchrNo.Text
            txtPPrefix.Text = txtprefix.Text
            numVchrNo.Tag = Val(numVchrNo.Text) 'Trim(CMNREC(12).FormattedText)
            txtprefix.Tag = txtprefix.Text
            'txtprefix.Text = SetNextVrNo(numVchrNo, Val(cmbVoucherTp.Tag), "IS", "TrType = 'IS' AND InvNo = ", False, True, True)
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
        If Not fwait Is Nothing Then fwait.Close() : fwait = Nothing
        MsgBox("Sales Invoice is succesfully posted with Vr. # " & numPrintVchr.Text & ".", MsgBoxStyle.Information)
        numVchrNo.Tag = ""
        chgPost = False
        If isprint Then
            PrepareRpt("IS", True)
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
        'If dtAccTb Is Nothing Then
        '    dtAccTb = _objcmnbLayer._fldDatatable()
        'End If
        If dtAccTb.Rows.Count > 0 Then
            dtAccTb.Rows.Clear()
        End If

        Dim EntRef As String = Strings.Left(Trim(txtDescr.Text) & IIf(Trim(txtDescr.Text) = "", "", " / ") & cmbVoucherTp.Text & " SALES (" & txtPurchaseName.Text & ")", 249)
        Dim dlAmt As Double '= (TDrAmt - IIf(DiscAcc > 0, CDbl(numDisc.Text), 0)) * FCRt
        Dim reference As String = Trim(txtprefix.Text) & IIf(txtprefix.Text = "", "", "/") & numVchrNo.Text
        'Tax Entry Credit
        Dim i As Integer = 0
        Dim ttlTxAmount As Double
        Dim TxAmount As Double
        If chktaxInv.Checked Then
            If dtTax Is Nothing Then
                GoTo nxt
            End If
            For i = 0 To dtTax.Rows.Count - 1
                If Val(dtTax(i)("ACid")) > 0 And Val(dtTax(i)("Amount")) > 0 Then
                    TxAmount = Format(CDbl(dtTax(i)("Amount")), numFormat)
                    ttlTxAmount = Format(CDbl(ttlTxAmount) + TxAmount, numFormat)

                    setAcctrDetValue(0, Val(dtTax(i)("ACid")), reference, dtTax(i)("AccountName") & " Tax Collected", TxAmount * -1 * FCRt, Strings.Left(txtJob.Text, 50), "", 0, 0, "", "", Val(txtSuppAlias.Tag), Val(txtSuppAlias.Tag) & txtReference.Text, cmbfc.Text, FCRt)
                    '_objTr.saveAccTrans()
                End If
            Next
nxt:
        End If
        dlAmt = CDbl(lblNetAmt.Text)
        'Debit Entry
        Dim mdebit As Double
        Dim customername As String
        If Not enableMultipleDebitAsCreditCollection Then
            'collection udjustment as sales transaction without RV
            If Not dtMultipleDebits Is Nothing Then
                If enableMultipleDebitInInvoice And dtMultipleDebits.Rows.Count > 0 Then
                    If dtMultipleDebits.Rows.Count > 0 Then
                        'Multiple Entry
                        If txtCashCustomer.Text = "" Then
                            customername = txtSuppName.Text
                        Else
                            customername = txtCashCustomer.Text
                        End If
                        Dim collectionRef As String = ""
                        For j = 0 To dtMultipleDebits.Rows.Count - 1
                            If CDbl(dtMultipleDebits(j)("accAmt")) < 0 Or enableMultipleDebitAsCreditCollection Then
                                collectionRef = "Collection Received On " & Trim(txtprefix.Text) & IIf(txtprefix.Text = "", "", "/") & numVchrNo.Text
                            Else
                                collectionRef = EntRef & IIf(dtMultipleDebits(j)("reference") <> "", " " & txtCashCustomer.Text & " Ref: " & dtMultipleDebits(j)("reference"), "")
                            End If
                            setAcctrDetValue(LinkNo, Val(dtMultipleDebits(j)("accid") & ""), dtMultipleDebits(j)("reference"), collectionRef, CDbl(dtMultipleDebits(j)("accAmt")) * FCRt, "", Strings.Left(txtJob.Text, 50), 0, 0, "", _
                                 "", Val(txtSuppAlias.Tag), Val(txtSuppAlias.Tag) & dtMultipleDebits(j)("reference"), cmbfc.Text, FCRt)
                            '_objTr.saveAccTrans()
                            mdebit = mdebit + CDbl(dtMultipleDebits(j)("accAmt"))
                        Next
                    End If
                End If
            End If
        End If

        If Val(txtroundOff.Text) > 0 Then
            dlAmt = Math.Round(dlAmt, NoOfDecimal)
            If Val(dlAmt) <> CDbl(lblNetAmt.Text) Then
                'dlAmt = dlAmt - (IIf(cmbsign.SelectedIndex = 1, 1, -1) * CDbl(txtroundOff.Text))
                dlAmt = CDbl(lblNetAmt.Text)
            End If
        End If
        If mdebit = 0 Then
            setAcctrDetValue(0, Val(txtSuppAlias.Tag), reference, EntRef, dlAmt * FCRt, Strings.Left(txtJob.Text, 50), Strings.Left(txtJob.Text, 50), 0, 0, "", _
                 "", Val(txtSuppAlias.Tag), Val(txtSuppAlias.Tag) & txtReference.Text, cmbfc.Text, FCRt)
        End If

        '_objTr.saveAccTrans()

        If Val(txtscamt.Text) > 0 And Val(cmbsalesman.Tag & "") > 0 Then
            'sales man commission entry
            Dim cAcc As Integer = 0
            Dim qry As String
            If UsrBr = "" Then
                qry = "SELECT accid FROM AccMast WHERE AccSetId Like '%19%'"
            Else
                qry = " SELECT accid FROM BranchAccSet WHERE branchcode='" & UsrBr & "' and setno=" & 19
            End If
            dtTable = _objcmnbLayer._fldDatatable(qry)
            If dtTable.Rows.Count > 0 Then
                cAcc = dtTable(0)("accid")
            End If
            If cAcc = 0 Then GoTo extc
            'debit
            setAcctrDetValue(0, Val(cAcc), reference, "Commission to " & cmbsalesman.Text & " For the Invoice " & txtprefix.Text & IIf(txtprefix.Text = "", "", "/") & Val(numVchrNo.Text), CDbl(txtscamt.Text) * FCRt, "", "", 0, 0, "", _
                             "", Val(cmbsalesman.Tag), "", cmbfc.Text, FCRt)
            '_objTr.saveAccTrans()
            'credit
            setAcctrDetValue(0, Val(cmbsalesman.Tag), reference, "Commission " & "For the Invoice " & txtprefix.Text & IIf(txtprefix.Text = "", "", "/") & Val(numVchrNo.Text), (CDbl(txtscamt.Text) * -1) * FCRt, "", "", 0, 0, "", _
                             "", Val(cmbsalesman.Tag), "", cmbfc.Text, FCRt)
            '_objTr.saveAccTrans()
extc:
        End If
        'Credit Entry
        Dim cramt As Double
        'If Val(txtroundOff.Text) > 0 Then
        '    cramt = CDbl(lblTotAmt.Text) - (IIf(cmbsign.SelectedIndex = 1, 1, -1) * CDbl(txtroundOff.Text))
        'Else

        'End If
        cramt = CDbl(lblTotAmt.Text)
        cramt = IIf(DiscAcc = 0, CDbl(lblNetAmt.Text) - CDbl(lbltax.Text) - CDbl(lblcess.Text), cramt)
        Dim total As Double = cramt + ttlTxAmount
        total = CDbl(lblNetAmt.Text) - total
        cramt = (cramt + total) * FCRt
        Dim crTrRef As String = Strings.Left(Trim(txtDescr.Text) & IIf(txtDescr.Text = "", " Sales /", "/ Sales / ") & txtSuppName.Text, 249)
        setAcctrDetValue(0, Val(txtPurchAlias.Tag), reference, crTrRef, cramt * -1, txtJob.Text, "", 0, 0, "", _
                             "", Val(txtSuppAlias.Tag), "", cmbfc.Text, FCRt)


        'DiscountEntry
        If (CDbl(numDisc.Text)) > 0 And DiscAcc > 0 Then
            dlAmt = CDbl(numDisc.Text) * FCRt
            setAcctrDetValue(0, DiscAcc, reference, Trim(txtDescr.Text), dlAmt, "", "", 2, 0, "", _
                            "", Val(txtSuppAlias.Tag), DiscAcc & txtReference.Text, cmbfc.Text, FCRt)
            '_objTr.saveAccTrans()
        End If
        If enableMultipleDebitInInvoice Then saveMultipleDebits(TrId)

        UpdateOtherCost(LinkNo)
        updateStockTransaction(TrId, LinkNo)
        'updateClosingBalanceForInvoice(TrId)
        LinkNo = 0
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


        _objTr.SaveAccTrWithDt(dtAccTb)

        dtAccTb.Rows.Clear()
        If enableMultipleDebitAsCreditCollection Then
            If Not dtMultipleDebits Is Nothing Then
                If enableMultipleDebitInInvoice And dtMultipleDebits.Rows.Count > 0 Then
                    setAcctrCmnValueRV(TrId, LinkNo)
                    If dtMultipleDebits.Rows.Count > 0 Then
                        'Multiple Entry
                        If txtCashCustomer.Text = "" Then
                            customername = txtSuppName.Text
                        Else
                            customername = txtCashCustomer.Text
                        End If
                        Dim collectionRef As String = ""
                        For j = 0 To dtMultipleDebits.Rows.Count - 1
                            If CDbl(dtMultipleDebits(j)("accAmt")) < 0 Or enableMultipleDebitAsCreditCollection Then
                                collectionRef = "Collection Received On " & Trim(txtprefix.Text) & IIf(txtprefix.Text = "", "", "/") & numVchrNo.Text
                            Else
                                collectionRef = EntRef & IIf(dtMultipleDebits(j)("reference") <> "", " " & txtCashCustomer.Text & " Ref: " & dtMultipleDebits(j)("reference"), "")
                            End If
                            setAcctrDetValue(LinkNo, Val(dtMultipleDebits(j)("accid")), dtMultipleDebits(j)("reference"), collectionRef, CDbl(dtMultipleDebits(j)("accAmt")) * FCRt, "", Strings.Left(txtJob.Text, 50), 0, 0, "", _
                                 "", Val(txtSuppAlias.Tag), Val(txtSuppAlias.Tag) & dtMultipleDebits(j)("reference"), cmbfc.Text, FCRt)
                            '_objTr.saveAccTrans()
                            mdebit = mdebit + CDbl(dtMultipleDebits(j)("accAmt"))
                        Next
                    End If
                    _objTr.SaveAccTrWithDt(dtAccTb)
                End If
                If dtMultipleDebits.Rows.Count = 0 Then
                    _objcmnbLayer._saveDatawithOutParm("delete from AccTrCmn where LinkNo=" & Val(txtrvno.Tag) & _
                                                       " delete from AccTrDet where LinkNo=" & Val(txtrvno.Tag) & _
                                                       " Update ItmInvCmnTb set ISRVID=0 where trid=" & TrId)
                End If
            End If
        End If

        _objTr.closeDlayerConnection()
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
                                        "", Val(txtSuppAlias.Tag), Val(.Item(CostDrAcc, i).Value) & Trim(.Item(CostReference, i).Value), .Item(CostFCName, i).Value, IIf(.Item(CostFCName, i).Value = "", 1, CDbl(.Item(CostFCRate, i).Value)), .Item(Costvatcode, i).Value)
                        '_objTr.saveAccTrans()

                        'Credit
                        setAcctrDetValue(LinkNo, Val(.Item(CostCrAcc, i).Value), Trim(.Item(CostReference, i).Value), Trim(.Item(CostDescr, i).Value), CDbl(.Item(CostAmount, i).Value) * -1 * FCRt, txtJob.Text, "", 1, c, "", _
                                         "", Val(txtSuppAlias.Tag), Val(.Item(CostCrAcc, i).Value) & Trim(.Item(CostReference, i).Value), .Item(CostFCName, i).Value, IIf(.Item(CostFCName, i).Value = "", 1, CDbl(.Item(CostFCRate, i).Value)))
                        '_objTr.saveAccTrans()
                    End If
                Next
            End With
        End If
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
            '_objTr.saveAccTrans()
            'UpdtClosBal(costOfSalesAc, costAmt)
            'credit entry [stock in hand]
            costAmt = costAmt * -1
            setAcctrDetValue(LinkNo, stockAc, Trim(txtReference.Text), entryref, costAmt, "", "", 3, 0, "", _
                           "", Val(txtSuppAlias.Tag), stockAc & txtReference.Text, "", FCRt)
            '_objTr.saveAccTrans()
            'UpdtClosBal(stockAc, costAmt)
        End If
    End Sub
    Private Sub setInvCmnValue(ByVal InvTrid As Long, ByVal paidamt As Double)
        With _objInv
            Dt = DateValue(Date.Now)
            .TrId = InvTrid
            .TrDate = DateValue(cldrdate.Value)
            .TrType = "IS"
            .DocLstTxt = txtDOLst.Text
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
            .TypeNo = TrTypeNo ' getVouchernumber("IS")
            .EnaJob = False
            .DocDefLoc = IIf(cmblocation.Text = "", Dloc, cmblocation.Text)
            .BrId = UsrBr
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
            'If TaxType is 0 then invoice is interstate invoice otherwise intrastate invoice
            .TaxType = Val(lblstatecode.Tag)
            .OthrCust = txtcustAddress.Text
            .DocLstTxt = txtDOLst.Text
            .isTaxInvoice = chktaxInv.Checked
            .isImportOrExport = IIf(chkexport.Checked, 1, 0)
            .cap1 = txtcap1.Text
            .cap2 = txtcap2.Text
            .cap3 = txtcap3.Text
            .cap4 = txtcap4.Text
            .cap5 = txtcap5.Text
            .info1 = txtinfo1.Text
            .info2 = txtinfo2.Text
            .info3 = txtinfo3.Text
            .info4 = txtinfo4.Text
            .info5 = txtinfo5.Text
            .taxwithoutLineDiscount = IIf(chktaxwithoutLinediscount.Checked, 1, 0)
            .CashCustName = UCase(MkDbSrchStr(IIf(txtCashCustomer.Text = "", txtSuppName.Text, txtCashCustomer.Text)))
            .deliveredBy = cmbdeliveredBy.Text
            .customerPhone = txtphone.Text
            .tenderd = paidamt
            .ccustid = creditCustomerACC
            .CashCustid = Val(txtCashCustomer.Tag)
            .isB2B = IIf(chkb2b.Checked, 1, 0)
            .tunername = cmbtunur.Text
            
            Dim ptype As Integer
            If chksecondprice.Checked Then
                ptype = 2
            ElseIf chkws.Checked Then
                ptype = 1
            Else
                ptype = 0
            End If
            .priceType = ptype
            .GSTN = ""
            If enableGCC Then
                If Trim(Mid(lblgstn.Text, Len("TIN:") + 1)) <> "Nill" Then
                    .GSTN = Trim(Mid(lblgstn.Text, Len("TIN:") + 1))
                End If
            Else
                If Trim(Mid(lblgstn.Text, Len("GSTN:") + 1)) <> "Nill" Then
                    .GSTN = Trim(Mid(lblgstn.Text, Len("GSTN:") + 1))
                End If

            End If
        End With

    End Sub
    Private Sub setInvDetValue(ByVal Invid As Long, ByVal PPerU As Single, ByVal i As Integer)
        With grdVoucher

            _objInv.dtTrId = Invid
            _objInv.ItemId = IIf(.Item(ConstSlNo, i).Value.ToString <> "M", Val(.Item(ConstItemID, i).Value), 0)
            _objInv.Unit = .Item(ConstUnit, i).Value
            _objInv.TrQty = CDbl(.Item(ConstQty, i).Value) * PPerU
            _objInv.focqty = CDbl(.Item(ConstFocQty, i).Value) * PPerU
            _objInv.UnitCost = CDbl(.Item(ConstActualPrice, i).Value) * FCRt / PPerU 'CDbl(.TextMatrix(i, 10)) / PPerU

            _objInv.taxP = CDbl(grdVoucher.Item(ConstTaxP, i).Value)
            _objInv.taxAmt = (CDbl(grdVoucher.Item(ConstTaxAmt, i).Value) * FCRt)
            _objInv.PFraction = PPerU
            If Val(.Item(ConstUnitOthCost, i).Value) = 0 Then .Item(ConstUnitOthCost, i).Value = 0
            _objInv.UnitOthCost = 0 ' CDbl(.Item(ConstUnitOthCost, i).Value) * FCRt / PPerU
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
            _objInv.TrTypeNo = TrTypeNo ' getVouchernumber("IS")
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
            If Val(lblgstn.Tag & "") = 0 Then
                _objInv.FloodcessAmt = (CDbl(.Item(ConstFloodCessAmt, i).Value) * FCRt)
            Else
                _objInv.FloodcessAmt = 0
            End If

            _objInv.AdditionalcessAmt = ((CDbl(.Item(ConstAdditionalcess, i).Value) * CDbl(.Item(ConstQty, i).Value)) * FCRt)
            _objInv.CessAmt = (CDbl(grdVoucher.Item(ConstcessAmt, i).Value) * FCRt)

            _objInv.Smancode = Trim(.Item(Constsman, i).Value & "")
            If Val(.Item(ConstStartReading, i).Value) = 0 Then .Item(ConstStartReading, i).Value = 0
            If Val(.Item(ConstEndReading, i).Value) = 0 Then .Item(ConstEndReading, i).Value = 0
            _objInv.StartingReading = CDbl(.Item(ConstStartReading, i).Value)
            _objInv.EndingReading = CDbl(.Item(ConstEndReading, i).Value)
            _objInv.MeterCode = Trim(.Item(ConstMeterCode, i).Value & "")
            _objInv.impDocid = Val(.Item(ConstImpDocId, i).Value & "")
            _objInv.impDocSlno = Val(.Item(ConstImpLnId, i).Value & "")
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
            If Val(.Item(ConstBatchCost, i).Value & "") = 0 Then .Item(ConstBatchCost, i).Value = 0
            _objInv.itemcost = CDbl(.Item(ConstBatchCost, i).Value)

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
        If isJobCategory = 1 And ImpJobChildTbIDs <> "" Then
            _objcmnbLayer._saveDatawithOutParm("Update MembershipRenewalTb set InvoiceTrid=" & Invid & " where renewid in(" & ImpJobChildTbIDs & ")")
        End If
        Return TDrAmt
    End Function
    Private Sub addtodtTb(ByVal trid As Long, ByVal itemid As Long, ByVal id As Long)
        Dim dtrow As DataRow
        dtrow = dtInvTb.NewRow
        dtrow("trid") = trid
        dtrow("itemid") = itemid
        dtrow("id") = id
        dtInvTb.Rows.Add(dtrow)
    End Sub
    Private Sub setAcctrCmnValue(ByVal InvTrid As Long, ByVal LinkNo As Long)
        _objTr.JVType = "IS"
        _objTr.JVDate = DateValue(cldrdate.Value)
        _objTr.PreFix = Trim(txtprefix.Text)
        _objTr.JVNum = Val(numVchrNo.Text)
        _objTr.JVTypeNo = TrTypeNo ' getVouchernumber("IS")
        _objTr.UserId = Trim(CurrentUser & "")
        _objTr.MchName = MACHINENAME
        _objTr.CrtDtTm = Dt
        _objTr.TypeNo = TrTypeNo ' getVouchernumber("IS")
        '_objTr.VrDescr = Trim(txtpayee.Text)
        _objTr.IsModi = IIf(LinkNo > 0, 1, 0)
        _objTr.LinkNo = InvTrid
        _objTr.taxablevalue = IIf(chkexport.Checked, CDbl(lblNetAmt.Text), CDbl(lbltaxable.Text))
        _objTr.taxvalue = CDbl(lbltax.Text)
        _objTr.isLinkNo = False
    End Sub
    Private Sub setAcctrCmnValueRV(ByVal InvTrid As Long, ByVal LinkNo As Long)
        _objTr.JVType = "ISRV"
        _objTr.JVDate = DateValue(cldrdate.Value)
        _objTr.PreFix = "ISRV"
        _objTr.JVNum = 0
        _objTr.JVTypeNo = getVouchernumber("RVO")
        _objTr.UserId = Trim(CurrentUser & "")
        _objTr.MchName = MACHINENAME
        _objTr.CrtDtTm = Dt
        _objTr.TypeNo = TrTypeNo ' getVouchernumber("IS")
        '_objTr.VrDescr = Trim(txtpayee.Text)
        _objTr.IsModi = IIf(Val(txtrvno.Tag) > 0, 2, 0)
        _objTr.LinkNo = IIf(Val(txtrvno.Tag) > 0, Val(txtrvno.Tag), InvTrid)
        _objTr.taxablevalue = IIf(chkexport.Checked, CDbl(lblNetAmt.Text), CDbl(lbltaxable.Text))
        _objTr.taxvalue = CDbl(lbltax.Text)
        _objTr.isdeleteTr = 1
        _objTr.isLinkNo = True
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
            'txtJob.Tag = Val(txtJob.Tag) & IIf(Val(txtJob.Tag) = "" Or JobAcc(jbIndex).Job = "", "", ",") & JobAcc(jbIndex).Job
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
        'AddtoCombo(cmbdeliveredBy, "SELECT SManCode FROM SalesmanTb", True, False)
        AddDttoCombo(cmbsalesman, dtsalesman, True, False)
        AddDttoCombo(cmbdeliveredBy, dtsalesman, True, False)
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
    'Private Sub loadUserDiscount()
    '    Dim dt As DataTable
    '    dt = _objcmnbLayer._fldDatatable("SELECT maxsalesDiscPercentage FROM UserTb WHERE UserId='" & CurrentUser & "'")
    '    If dt.Rows.Count > 0 Then
    '        userDiscount = Val(dt(0)("maxsalesDiscPercentage") & "")
    '    End If
    'End Sub

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

    Private Sub SalesInvoice_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        cmbVoucherTp.Focus()
    End Sub

    Private Sub SalesInvoice_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
        If Not fSelect Is Nothing Then fSelect.Close() : fSelect = Nothing
        If Not fProductEnquiry Is Nothing Then fProductEnquiry.Close() : fProductEnquiry = Nothing
        If Not fSlctDoc Is Nothing Then fSlctDoc.Close() : fSlctDoc = Nothing
        If Not fSerialno Is Nothing Then fSerialno.Close() : fSerialno = Nothing
    End Sub

    Private Sub SalesInvoice_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
    End Sub

    Private Sub SalesOrderFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor
        'Timer1.Enabled = True
        formLoad()
ext:
        Me.Cursor = Cursors.Default
    End Sub
    Private Sub setPrice()
        chkb2b.Checked = enableB2BAsDefault
        chkretailprice.Checked = False
        chksecondprice.Checked = False
        chkws.Checked = False
        If chkb2b.Checked = False Then
            If priceInSales = 1 Or priceInSales = 0 Then
                chkretailprice.Checked = True
            ElseIf priceInSales = 2 Then
                chksecondprice.Checked = True
            ElseIf priceInSales = 3 Then
                chkws.Checked = True
            End If
        Else
            chkws.Checked = True
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
        dtTable = _objcmnbLayer._fldDatatable("SELECT * FROM ItmInvCmnTb WHERE TrType = 'IS' AND InvNo = " & Val(numVchrNo.Text) & " AND PreFix = '" & MkDbSrchStr(Trim(txtprefix.Text), True) & "'")
        If dtTable.Rows.Count > 0 Then
            If MsgBox("Entered Voucher number already exists. Fill next ?", vbQuestion + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                If Not varNextFoundBool And Val(numVchrNo.Text) = 0 Then
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
        Dim itemidStr As String
        'Dim dt As DataTable
        With grdVoucher
            MyActiveControl = grdVoucher
            'Dim dtSrlNo As DataTable
            'Dim dtExstSrlno As DataTable

            Dim r As Integer
            itemidStr = ""
            'For r = 0 To grdVoucher.RowCount - 1
            '    itemidStr = itemidStr & IIf(itemidStr = "", "", ",") & Val(.Item(ConstItemID, r).Value)
            'Next
            'dtSrlNo = _objcmnbLayer._fldDatatable("SELECT SerialNo,itemid FROM ItmInvTrTb Left Join ItmInvCmnTb on ItmInvTrTb.Trid=ItmInvCmnTb.trid WHERE TrType='IS' AND ItmInvCmnTb.Trid<>" & loadedTrId)
            'dtExstSrlno = _objcmnbLayer._fldDatatable("SELECT SerialNo,ItemId FROM serialnotb ")
            'Dim _qurey As EnumerableRowCollection(Of DataRow)

            For r = 0 To .RowCount - 1 '- 1
                If .Item(ConstIsSerial, r).Value = 1 And .Item(ConstSerialNo, r).Value = "" And enableSerialnumber Then
                    .Rows(r).Selected = True
                    .CurrentCell = .Item(ConstQty, r)
                    MsgBox("Serial Number cannot be Blank !!", vbExclamation)
                    .FirstDisplayedScrollingRowIndex = r
                    GoTo Ter
                End If
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
                If CDbl(.Item(ConstDonotAllowsaveItem, r).Value) = 1 Then
                    .Rows(r).Selected = True
                    .CurrentCell = .Item(ConstQty, r)
                    MsgBox("Entered Quantity Exceeds!" & vbCrLf & " You have been protected from -ve quantity entry", vbExclamation)
                    .FirstDisplayedScrollingRowIndex = r
                    GoTo Ter
                End If
                If CDbl(.Item(ConstDonotAllowsaveItem, r).Value) = 2 Then
                    .Rows(r).Selected = True
                    .CurrentCell = .Item(ConstQty, r)
                    MsgBox("Entered Price is below cost!" & vbCrLf & " You have been protected from below cost entry", vbExclamation)
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
                itemidStr = itemidStr & IIf(itemidStr = "", "", ",") & Val(.Item(ConstItemID, r).Value)
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
        '
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

        If ItemcODE <> "" Then
            Valid(grdVoucher.CurrentRow.Index, ConstItemCode)
            'If Not diableNegativeSale Then

            'End If
            grdVoucher.CurrentCell = grdVoucher.Item(ConstDescr, grdVoucher.CurrentRow.Index)
        Else
            grdVoucher.CurrentCell = grdVoucher.Item(ConstItemCode, grdVoucher.CurrentRow.Index)
        End If

        grdBeginEdit()
        chgbyprg = False
    End Sub

    Private Sub txtroundOff_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtroundOff.KeyDown
        If e.KeyCode = Keys.Return Then
            btnupdate.Focus()
        End If
    End Sub

    Private Sub txtdp_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtdp.KeyDown
        If e.KeyCode = Keys.Return Then btnupdate.Focus()
    End Sub

    Private Sub numDisc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles numDisc.Click
        numDisc.SelectAll()
    End Sub

    Private Sub numDisc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles numDisc.KeyDown
        If e.KeyCode = Keys.Return Then
            btnupdate.Focus()
        End If
    End Sub

    Private Sub numDisc_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles numDisc.KeyPress, txtdp.KeyPress, txtsmanP.KeyPress, txtfcrt.KeyPress, txtscamt.KeyPress
        NumericTextOnKeypress(sender, e, chgNumByPgm, lnumFormat)
    End Sub

    Private Sub numDisc_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numDisc.TextChanged
        If Not chgNumByPgm And Val(lblTotAmt.Text) > 0 Then
            chgNumByPgm = True
            If Val(numDisc.Text) = 0 Then
                numDisc.Text = Format(0, lnumFormat)
            End If

            txtdp.Text = Format((CDbl(numDisc.Text) * 100) / CDbl(lblTotAmt.Text), lnumFormat)

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
        If grdVoucher.CurrentCell.ColumnIndex = ConstItemCode Then
            grdVoucher.CurrentCell = grdVoucher.Item(1, grdVoucher.CurrentRow.Index)
            chgItm = True
            Valid(grdVoucher.CurrentCell.RowIndex, grdVoucher.CurrentCell.ColumnIndex)
            grdBeginEdit()
        ElseIf grdVoucher.CurrentCell.ColumnIndex = ConstSerialNo Then
            grdVoucher.CurrentCell = grdVoucher.Item(ConstSerialNo, grdVoucher.CurrentRow.Index)
            'Valid(grdVoucher.CurrentCell.RowIndex, grdVoucher.CurrentCell.ColumnIndex)
            'grdBeginEdit()
        End If

        plsrch.Visible = False
        chgbyprg = False
    End Sub


    Private Sub txtSuppAlias_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSuppAlias.Validated, txtSuppName.Validated
        If txtSuppAlias.Text = "" Then Exit Sub
        setCustomer()
    End Sub
    Private Sub setCustomer(Optional ByVal accid As Long = 0, Optional ByVal skipCalculate As Boolean = False, Optional ByVal GSTN As String = "", Optional ByVal skipcustomerdiscount As Boolean = True, Optional ByVal varlinkno As Long = 0)
        Dim dt As DataTable
        Dim condition As String
        If txtSuppAlias.Text = "" And accid = 0 Then GoTo els
        If accid > 0 Then
            condition = "where accid=" & accid
        Else
            condition = "where Alias='" & txtSuppAlias.Text & "'"
        End If
        Dim balquery As String = "SELECT  from AccMast  where accid=" & accid
        balquery = "LEFT JOIN (Select sum(DealAmt) bal,accountno from AccTrDet " & _
                                                          "left join AccTrCmn on AccTrCmn.linkno=AccTrDet.linkno where jvtype<>'OB' and " & _
                                                          IIf(varlinkno = 0, "AccTrCmn.linkno<>" & varlinkno, "AccTrCmn.linkno<" & varlinkno) & _
                                                          " group by accountno) Tr On Accmast.accid=tr.accountno "


        dt = _objcmnbLayer._fldDatatable("SELECT AccMast.accid,Alias,AccDescr,Address1,Address2,Address3,Address4," & _
                                        "Phone,Fax,EMail,PT1,PT2,PT3,PT4,TrdLcno,TrdDate,CreditLimit,DueDays,SlsmanId," & _
                                        "CurrencyCode,CountryCode,GSTIN,Remarks,GrpSetOn,isnull(OpnBal,0)+isnull(bal,0) bal,custid " & _
                                        " from AccMast LEFT JOIN AccMastAddr ON AccMast.accid=AccMastAddr.AccountNo " & _
                                        "LEFT JOIN S1AccHd ON AccMast.S1AccId=S1AccHd.S1AccId " & _
                                        "LEFT JOIN (select custid,customeraccount from CashCustomerTb) CashCustomerTb ON customeraccount=ACCMAST.ACCID " & balquery & _
                                        condition)
        If dt.Rows.Count > 0 Then
            txtSuppAlias.Tag = dt(0)("accid")
            chgbyprg = True
            txtSuppAlias.Text = Trim("" & dt(0)("Alias"))
            txtSuppName.Text = Trim("" & dt(0)("AccDescr"))
            txtCashCustomer.Tag = Val(dt(0)("custid") & "")
            If UCase(Trim("" & dt(0)("GrpSetOn"))) <> "CUSTOMER" Then
                isNotCustomerAccount = True
            Else
                isNotCustomerAccount = False
            End If

            chgbyprg = False

            txtcustAddress.Text = Trim(dt(0)("Address1") & "")
            If Trim(dt(0)("Address2") & "") <> "" Then
                txtcustAddress.Text = txtcustAddress.Text & vbCrLf & Trim(dt(0)("Address2") & "")
            End If
            If Trim(dt(0)("Address3") & "") <> "" Then
                txtcustAddress.Text = txtcustAddress.Text & vbCrLf & Trim(dt(0)("Address3") & "")
            End If
            If Trim(dt(0)("Address4") & "") <> "" Then
                txtcustAddress.Text = txtcustAddress.Text & vbCrLf & Trim(dt(0)("Address4") & "")
            End If

            If enableGCC Then
                If Trim(dt(0)("TrdLcno") & "") <> "" Then
                    lblgstn.Text = "TIN: " & Trim(dt(0)("TrdLcno") & "")
                    chktaxInv.Checked = True
                    lblgstn.Visible = True
                Else
                    'chktaxInv.Checked = False
                    lblgstn.Text = "TIN: Nill"
                    lblgstn.Tag = 0
                End If
                lblstatecode.Text = "Emirate : "
            Else
                If Trim(dt(0)("GSTIN") & "") <> "" Or GSTN <> "" Then
                    lblgstn.Text = "GSTN: " & IIf(GSTN = "", Trim(dt(0)("GSTIN") & ""), GSTN)
                    chktaxInv.Checked = True
                    lblgstn.Tag = 1
                    lblgstn.Visible = True
                    chkb2b.Checked = True
                Else
                    'chktaxInv.Checked = False
                    lblgstn.Text = "GSTN: Nill"
                    lblgstn.Tag = 0
                    chkb2b.Checked = False
                End If
                lblstatecode.Text = "Sate Code : "
            End If
            If Trim(dt(0)("Phone") & "") <> "" Then
                txtcustAddress.Text = txtcustAddress.Text & vbCrLf & "Ph: " & Trim(dt(0)("Phone") & "")
                If txtphone.Text = "" Then txtphone.Text = Trim(dt(0)("Phone") & "")
            End If

            If accid = 0 Then
                cmbfc.Text = Trim(dt(0)("CurrencyCode") & "")
                cmbsalesman.Text = Trim(dt(0)("SlsmanId") & "")
                cmbdeliveredBy.Text = Trim(dt(0)("SlsmanId") & "")
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
            If Val(dt(0)("CreditLimit") & "") = 0 Then
                dt(0)("CreditLimit") = 0
            End If
            txtremarks.Text = Trim(dt(0)("Remarks") & "")
            If Not isNotCustomerAccount Then
                lbllimit.Text = Format(Val(dt(0)("CreditLimit")), lnumFormat)
                Dim iNBal As Double = Format(Val(dt(0)("bal")), lnumFormat) ' getAccBal(Val(txtSuppAlias.Tag), varlinkno)
                lblbalance.Text = Strings.Format(iNBal, lnumFormat)
                If IsDBNull(dt(0)("DueDays")) Then
                    dt(0)("DueDays") = 0
                End If
                If Val(dt(0)("DueDays") & "") > 0 Then
                    iNBal = getAccAegBal(Val(txtSuppAlias.Tag), DateValue(DateTime.Now), Val(dt(0)("DueDays")))
                    lblInvoices.Text = Strings.Format(iNBal, lnumFormat)
                    clrDuedate.Value = DateAdd(DateInterval.Day, Val(dt(0)("DueDays")), cldrdate.Value)
                Else
                    lblInvoices.Text = Format(0, numFormat)
                End If
            Else
                lbllimit.Text = Format(0, lnumFormat)
                lblbalance.Text = Format(0, lnumFormat)
                lblInvoices.Text = "0"
            End If
            'If EnableGST Then CalculateGST()
            If skipcustomerdiscount = False Then
                Dim i As Integer
                Dim discount As Double
                With grdVoucher
                    For i = 0 To .RowCount - 1
                        If enablecess And Val(lblgstn.Tag & "") = 0 And cessdate <= DateValue(cldrdate.Value) Then
                            .Item(ConstcessAmt, i).Value = Format(((CDbl(.Item(ConstActualPrice, i).Value) * .Item(Constcess, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value), lnumFormat)
                        Else
                            .Item(ConstcessAmt, i).Value = 0
                        End If
                        discount = getCustomerDiscount(Val(.Item(ConstItemID, i).Value))
                        If discount > 0 Then
                            .Item(ConstDisP, i).Value = Format(discount, numFormat)
                            .Item(ConstDisAmt, i).Value = Format(((CDbl(.Item(ConstActualPrice, i).Value) * Val(.Item(ConstDisP, i).Value)) / 100) * CDbl(.Item(ConstQty, i).Value), lnumFormat)
                        End If
                        calcualteLineTotal(i)
                    Next
                    If Not skipCalculate Then calculate()
                End With
            End If

        Else
els:
            chgbyprg = True
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
            txtCashCustomer.Text = ""
            chgbyprg = False
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

    Private Sub txtphone_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtphone.KeyDown
        If e.KeyCode = Keys.Return Then
            If enablePhoneNumberMandatory And txtphone.Text = "" Then
                MsgBox("Phone number is mandatory", MsgBoxStyle.Exclamation)
                txtphone.Focus()
                Exit Sub
            End If
            If grdVoucher.Rows.Count > 0 Then
                activecontrolname = "grdVoucher"
                grdVoucher.CurrentCell = grdVoucher.Item(1, grdVoucher.CurrentRow.Index)
                grdBeginEdit()
            Else
                AddRow()
            End If
        End If

    End Sub

    Private Sub txtDescr_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDescr.TextChanged, txtphone.TextChanged
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
            fRptFormat.RptType = "ISFI"
            fRptFormat.ShowDialog()
            fRptFormat = Nothing
        Else
            PrepareRpt("ISFI")
        End If
        'PrepareRpt("PO")
    End Sub

    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        LoadReport(RptFlName, RptCaption)
    End Sub

    Private Sub AddNewClick()
        If chgbyprg Then Exit Sub
        If chgPost Then
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
        btnupdate.Enabled = True
        btndelete.Enabled = True
        If cmbVoucherTp.SelectedIndex > 0 Then
            cmbVoucherTp.SelectedIndex = 0
        Else
            cmbVoucherTp_SelectedIndexChanged(cmbVoucherTp, New System.EventArgs)
        End If

        '
        'btnNext.Visible = True
    End Sub

    Private Sub ClearClick()
        If btndelete.Enabled = False Then Exit Sub
        If MsgBox("Do You Want Clear Etry", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        ClearControls()
        cmbVoucherTp_SelectedIndexChanged(cmbVoucherTp, New EventArgs)
    End Sub

    Private Sub DeleteClick()
        If btndelete.Enabled = False Then Exit Sub
        If Val(btndelete.Tag) = 0 Then
            MsgBox("This user do not have permission to Remove", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If MsgBox("You are going to REMOVE the Sales Invoice # " & numVchrNo.Text & Chr(13) & "Are you sure ?", vbOKCancel + vbQuestion + vbDefaultButton2) = vbOK Then

            Dim itemidsdatatable As New DataTable
            'Dim trdate As Date
            'itemidsdatatable = _objcmnbLayer._fldDatatable("SELECT Itemid,TrDate,SerialNo from ItmInvTrTb LEFT JOIN ItmInvCmnTb ON ItmInvTrTb.Trid=ItmInvCmnTb.Trid  WHERE ItmInvTrTb.TrId =" & loadedTrId)
            'If itemidsdatatable.Rows.Count > 0 Then
            '    trdate = DateValue(itemidsdatatable(0)("TrDate"))
            'End If
            _objInv.TrId = loadedTrId
            _objInv.TrType = "OUT"
            _objInv.deleteInventoryTransactions()
            'For i = 0 To itemidsdatatable.Rows.Count - 1
            '    _objcmnbLayer._saveDatawithOutParm("UPDATE SerialNoTb SET SalesId=0 WHERE SerialNo='" & itemidsdatatable(i)("SerialNo") & "'")
            '    _objcmnbLayer._saveDatawithOutParm("delete from SerialNoTb where ISNULL(PurchaseId,0)=0 and ISNULL(SalesId,0)=0")
            '    _objcmnbLayer._saveDatawithOutParm("delete from WarrentyTrTb where SerialNo='" & itemidsdatatable(i)("SerialNo") & "'")

            '    _objInv.ItemId = itemidsdatatable(i)("Itemid")
            '    _objInv.TrDate = DateValue(itemidsdatatable(i)("TrDate"))
            '    _objInv.setcostAverageOnModification(UsrBr)
            'Next
            btnModify_Click(btnModify, New System.EventArgs)
        End If
        chgPost = False
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
            txtCashCustomer.Text = dt(0)("AccDescr")
            txtCashCustomer.Tag = 0
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
        If btnupdate.Enabled = False Then Exit Sub
        If chktaxInv.Visible = False Then chktaxInv.Checked = False
        If Val(btnupdate.Tag) = 0 Then
            MsgBox("This user do not have permission to Update", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If Val(txtPurchAlias.Tag) = 0 And Val(txtSuppAlias.Tag) = 0 Then
            MsgBox("Invalid Account Settings", MsgBoxStyle.Exclamation)
            cmbVoucherTp.Focus()
            Exit Sub
        End If
        If userDiscount > 0 Then
            If Val(txtdp.Text) > userDiscount Then
                MsgBox("This user do not have permission to Allow discount more than " & userDiscount & "%", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
        End If
        plsrch.Visible = False

        Verify()
        chgPost = False
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
            'dtMultipleDebits.Rows.Clear()
            .lblinvoiceAmt.Text = "Invoice Amt. " & Format(CDbl(lblNetAmt.Text), numFormat)
            .lblinvoiceAmt.Tag = CDbl(lblNetAmt.Text)
            .txtreference.Text = Trim(txtprefix.Text) & IIf(txtprefix.Text = "", "", "/") & numVchrNo.Text
            .customeraccid = Val(txtSuppAlias.Tag)
            .customeralias = txtSuppAlias.Text
            .customername = txtSuppName.Text
            .lnumformat = lnumFormat
            .skipCrEntry = IsPOSInv
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


    Private Sub fSelect_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fSelect.FormClosed
        fSelect = Nothing
    End Sub

    Private Sub ctmControl_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub PrepareRpt(ByVal RptType As String, Optional ByVal Forprint As Boolean = False, Optional ByVal pno As Integer = 0)
        If chgPost Then
            MsgBox("Changes Found! Update the voucher", MsgBoxStyle.Exclamation)
            btnupdate.Focus()
            Exit Sub
        End If
        If Val(numVchrNo.Text) = 0 Then
            MsgBox("Enter a valid Voucher Number !!", vbInformation)
            numVchrNo.Focus()
            Exit Sub
        End If
        Dim RptName As String
        Dim RptCaption As String = ""
        Dim printername As String = ""
        RptName = getRptDefFlName(RptType, RptCaption, printername)
        If Trim(RptName) <> "" Then
            LoadReport(RptName, RptCaption, Forprint, pno, printername)
        End If
    End Sub

    Private Sub LoadReport(ByVal FileName As String, ByVal RptCaption As String, Optional ByVal ToPrint As Boolean = False, Optional ByVal pno As Integer = 0, Optional ByVal printername As String = "")
        If Not frm Is Nothing Then
            frm.Close()
            frm = New ReprtviewNEWfrm
        Else
            frm = New ReprtviewNEWfrm
        End If
        _objInv.Prefix = txtPPrefix.Text
        _objInv.InvNo = Val(numPrintVchr.Text)
        _objInv.TrType = "IS"
        If _objInv.InvNo = 0 Then Exit Sub
        Dim ds As DataSet
        ds = _objInv.ldInvoice("rturnfinanceInvoiceForPrint", pno)
        If ToPrint Then
            _objcmnbLayer._saveDatawithOutParm("Update ItmInvCmnTb set Printed=1 where trid=" & Val(ds.Tables(0)(0)("trid")))
        End If
        frm.SetReport(ds, FileName, 0, False, , ToPrint)
        frm.btnprint.Tag = Val(ds.Tables(0)(0)("trid"))
        frm.MdiParent = Me.MdiParent
        frm.Text = RptCaption
        If Not ToPrint Then frm.Show()
    End Sub

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        add_Click()
    End Sub
    Private Sub add_Click()
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
        With grdVoucher
            If e.ColumnIndex = ConstSlNo Then
                .Item(ConstSlNo, .CurrentCell.RowIndex).Value = IIf(Val(.Item(ConstSlNo, .CurrentCell.RowIndex).Value) = 0, "", "M")
                reArrangeNo()
            ElseIf e.ColumnIndex = ConstUnit Then
                If .Item(ConstB, .CurrentCell.RowIndex).Value = "B" Then
                    .Item(ConstB, .CurrentCell.RowIndex).Value = "P1"
                    dt = _objcmnbLayer._fldDatatable("SELECT P1Unit U,P1Fra F,UnitPrice,UnitPriceWS,additionalcess,secondPrice,ItemId FROM InvItm WHERE Itemid=" & Val(.Item(ConstItemID, .CurrentCell.RowIndex).Value))
                ElseIf .Item(ConstB, .CurrentCell.RowIndex).Value = "P1" Then
                    .Item(ConstB, .CurrentCell.RowIndex).Value = "P2"
                    dt = _objcmnbLayer._fldDatatable("SELECT P2Unit U,P2Fra F,UnitPrice,UnitPriceWS,additionalcess,secondPrice,ItemId FROM InvItm WHERE Itemid=" & Val(.Item(ConstItemID, .CurrentCell.RowIndex).Value))
                ElseIf .Item(ConstB, .CurrentCell.RowIndex).Value = "P2" Then
u:
                    .Item(ConstB, .CurrentCell.RowIndex).Value = "B"
                    dt = _objcmnbLayer._fldDatatable("SELECT Unit U,1 F,UnitPrice,UnitPriceWS,additionalcess,secondPrice,ItemId FROM InvItm WHERE Itemid=" & Val(.Item(ConstItemID, .CurrentCell.RowIndex).Value))
                End If
                If dt Is Nothing Then GoTo u
                If dt.Rows.Count > 0 Then
                    .Item(ConstUnit, .CurrentCell.RowIndex).Value = dt(0)("U")
                    .Item(ConstPMult, .CurrentCell.RowIndex).Value = dt(0)("F")
                    Dim cost As Double
                    Dim addcess As Double
                    addcess = Val(dt(0)("additionalcess") & "") * Val(.Item(ConstPMult, .CurrentCell.RowIndex).Value & "")
                    cost = getLastSalesAmt(Val(txtSuppAlias.Tag), dt(0)("ItemId"), True, "IS", CDbl(dt(0)("UnitPrice")))
                    If Val(.Item(ConstPMult, .CurrentCell.RowIndex).Value & "") = 0 Then
                        .Item(ConstPMult, .CurrentCell.RowIndex).Value = 0
                    End If
                    cost = cost * Val(.Item(ConstPMult, .CurrentCell.RowIndex).Value)
                    .Item(ConstActualPrice, .CurrentCell.RowIndex).Value = cost
                    .Item(ConstUPrice, .CurrentCell.RowIndex).Value = Format(cost, lnumFormat)
                    .Item(ConstAdditionalcess, .CurrentCell.RowIndex).Value = addcess
                    calculate(, True)
                End If

            End If
        End With
    End Sub


    Private Sub TabPage1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub grdVoucher_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellEndEdit
        If chgbyprg Then Exit Sub
        Dim col As Integer = e.ColumnIndex
        Dim ndec1 As Integer
        chgbyprg = True

        If col = ConstQty Or col = ConstUPrice Or col = ConstDisAmt Or col = ConstLTotal Or col = ConstTaxP Or col = ConstDisP Or col = ConstEndReading Or col = ConstWoodDiscQty Or col = ConstWoodQty Then
            If col = ConstQty Or col = ConstEndReading Or col = ConstWoodDiscQty Or col = ConstWoodQty Then
                ndec1 = Val(grdVoucher.Item(ConstPFraction, grdVoucher.CurrentRow.Index).Value & "")
            Else
                ndec1 = NoOfDecimal
            End If
            If Val(grdVoucher.Item(col, e.RowIndex).Value) = 0 Then grdVoucher.Item(col, e.RowIndex).Value = 0
            grdVoucher.Item(col, e.RowIndex).Value = Format(Convert.ToDecimal(grdVoucher.Item(col, e.RowIndex).Value), "#,##0" & IIf(ndec1 = 0, "", "." & Strings.StrDup(ndec1, "0")))
        End If
        chgbyprg = False
        'MsgBox(grdVoucher.Item(ConstDonotAllowsaveItem, 0).Value)
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
                calcualteLineTotal(grdVoucher.Rows.Count - 1)
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
            If txtSuppName.Enabled Then
                txtSuppName.Focus()
            Else
                txtCashCustomer.Focus()
            End If

        End If

    End Sub

    Private Sub cldrdate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cldrdate.ValueChanged
        chgPost = True
    End Sub

    Private Sub btnimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnimport.Click
        isImport = True
        fSlctDoc = New SelectInvTr
        With fSlctDoc
            .strType = cmbimporttr.Text
            .Text = "Select Transaction"
            .ShowDialog()
        End With
        If Not fSlctDoc Is Nothing Then fSlctDoc = Nothing
    End Sub

    Private Sub grdSrch_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSrch.CellContentClick

    End Sub

    Private Sub cmbVoucherTp_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbVoucherTp.KeyDown
        If e.KeyCode = Keys.Return Then
            If txtReference.Text = "" Then
                txtReference.Focus()
            Else
                txtCashCustomer.Focus()
            End If
        End If
    End Sub

    Private Sub cmbVoucherTp_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVoucherTp.SelectedIndexChanged
        If chgbyprg = True Then Exit Sub
        chgbyprg = True
        isNotCustomerAccount = False
        NextNumber()
        If grdVoucher.RowCount > 0 Then
            calculate(, True, True)
        End If
        If cmbVoucherTp.Text <> "" Then
            txtCashCustomer.Enabled = True
        Else
            txtCashCustomer.Enabled = False
        End If
        chgbyprg = False
    End Sub
    Private Sub formLoad()
        Try
            loadInventoryFormLoadMasters(True, 4, "IS", 3, DiscAcc, TrTypeNo)
            StrAccMastSrch = "SELECT AccDescr, Alias, ClosingBal As Balance, OpnBal, AccountNo, S1AccHd.S1AccId, AccSetId, GrpSetOn,accid FROM" & _
                            " AccMast INNER JOIN S1AccHd ON S1AccHd.S1AccId = AccMast.S1AccId "
            txtdp.Enabled = Not enableAdjustDiscountOnTaxTotal
            If ShowTaxOnInventory Or EnableGST Then
                CreateTaxTable(dtTax)
                chktaxInv.Checked = True
                If withNonTaxBill Then
                    chktaxInv.Checked = False
                End If
            Else
                chktaxInv.Checked = False
                chktaxInv.Visible = False
                lblgstn.Visible = False
            End If
            'MsgBox("2")
            If enableMultipleDebitInInvoice Then
                'loadSalesMultipleDebits(0)
                createMultipleDebitTb(dtMultipleDebits)
                btnmultipleDebit.Visible = True
            End If

            If enableDeliverywiseOutstanding Then
                cmbdeliveredBy.Visible = True
                lbldelivery.Visible = True
            Else
                cmbdeliveredBy.Visible = False
                lbldelivery.Visible = False
            End If

            SetGridHead()
            setOtherCostHead()
            loadAddInfoCap()
            'MsgBox("4")
            'crtSubVrs(cmbVoucherTp, 4, True)
            lnumFormat = numFormat
            FCRt = 1
            OthCost = 0
            chgbyprg = True
            cldrdate.Value = Format(Date.Now, DtFormat)
            NDec = NoOfDecimal
            loadedTrId = 0

            crtSubVrs(cmbVoucherTp, 4, True)

            chgbyprg = False
            'GoTo ext
            ldSman()
            LodCurrency()
            'loadUserDiscount()
            'AddtoCombo(cmblocation, "SELECT LocCode FROM LocationTb", True, False)
            AddDttoCombo(cmblocation, dtlocationTb, True, False)
            _objcmnbLayer.dtSerialNo = createdtSerialNo()

            ChgId = False
            chgPost = False
            If userType Then
                disableBelowcost = getRight(246, CurrentUser)
                diableNegativeSale = getRight(171, CurrentUser)
            End If

            If isModi Then
                btnModify_Click(btnModify, New System.EventArgs)
                If userType Then
                    btnupdate.Tag = IIf(getRight(46, CurrentUser), 1, 0)
                    btndelete.Tag = IIf(getRight(47, CurrentUser), 1, 0)
                    diableNegativeSale = getRight(171, CurrentUser)

                Else
                    btnupdate.Tag = 1
                    btndelete.Tag = 1
                End If

                btndelete.Text = "Delete"
            Else
nw:
                AddNewClick()
                If userType Then
                    btnupdate.Tag = IIf(getRight(45, CurrentUser), 1, 0)
                Else
                    btnupdate.Tag = 1
                End If
                btndelete.Text = "Clear"
                btndelete.Tag = 1
            End If

            Timer1.Enabled = True
            cessdate = getCessDate()
            chkautoroundOff.Checked = enableAutoRoundOff
            cmbimporttr.SelectedIndex = 0
            cmbvoucher.SelectedIndex = 0
            If enableGCC Then
                lblgstn.Text = "TIN : "
                lblstatecode.Text = "Emirate : "
                chkexport.Visible = True
                Label20.Text = "VAT Total"
                Label31.Visible = False
                lblcess.Visible = False
                chktaxInv.Visible = True
            End If
            cmbDos.SelectedIndex = 0
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        chkcal.Checked = calcluatetaxFrompriceInv
        setPrice()
        chkb2b.Checked = enableB2BAsDefault
        loadTurnur()
    End Sub
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        'formLoad()
        If grdVoucher.ColumnCount = 0 Then Exit Sub
        If EnableWarranty = False And Me.Width > 1200 Then resizeGridColumn(grdVoucher, ConstDescr)
        If Me.Width <= 1200 Or grdVoucher.Columns(ConstDescr).Width <= 25 Then
            grdVoucher.Columns(ConstDescr).Width = 150
        End If
    End Sub

    Private Sub SalesInvoice_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Timer1.Enabled = True
    End Sub

    Private Sub cmbsalesman_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbsalesman.Click
        If isnewSalesmanAdded Then
            ldSman()
            isnewSalesmanAdded = False
        End If
    End Sub

    Private Sub cmbsalesman_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbsalesman.SelectedIndexChanged
        If Not chgbyprg Then
            returnSalesmanAccount(True)
            chgPost = True
        End If
    End Sub
    Private Sub loadTurnur()
       
        AddtoCombo(cmbtunur, "Select tunername from TunurTb", False, False)
    End Sub

    Private Sub returnSalesmanAccount(Optional ByVal clearIfblank As Boolean = True)
        Dim dt As DataTable = _objcmnbLayer._fldDatatable("Select accountno,comP,DisP from SalesmanTb where SManCode='" & cmbsalesman.Text & "'", False)
        txtscamt.Text = "0.00"
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
            If Val(dt(0)("DisP")) > 0 And Val(txtdp.Text) = 0 Then
                txtdp.Text = Format(CDbl(dt(0)("DisP")), lnumFormat)
                If Val(txtdp.Text) = 0 Then txtdp.Text = 0
                If Val(lblTotAmt.Text) = 0 Then lblTotAmt.Text = 0
                numDisc.Text = Format((CDbl(lblTotAmt.Text) * CDbl(txtdp.Text)) / 100, lnumFormat)
            End If

            chgbyprg = False
        ElseIf clearIfblank Then
            cmbsalesman.Tag = 0
            txtsmanP.Text = Format(0, lnumFormat)
            txtdp.Text = Format(0, lnumFormat)
            numDisc.Text = Format(0, lnumFormat)
            txtscamt.Text = "0.00"
        End If
        calculate(False)
    End Sub

    Private Sub cmbfc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbfc.Click
        If isnewCurrencyAdded Then
            LodCurrency()
            isnewCurrencyAdded = False
        End If
    End Sub

    Private Sub cmbfc_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbfc.SelectedIndexChanged
        'If chgbyprg Then Exit Sub
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


    Private Sub txtdp_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtdp.Validated
        If Val(lblTotAmt.Text) > 0 Then
            chgNumByPgm = True
            If Val(txtdp.Text) = 0 Then txtdp.Text = 0
            numDisc.Text = Format((CDbl(lblTotAmt.Text) * CDbl(txtdp.Text)) / 100, lnumFormat)
            chgNumByPgm = False
            chgPost = True
            calculate(, True, True)
        End If
        'calOthCost()
    End Sub

    Private Sub btntax_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btntax.Click
        If enableGCC = False And EnableGST = False Then Exit Sub
        CalculateGST(True)
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
            If .RowCount = 0 Then Exit Sub
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
        If dtItemInfo Is Nothing And itemid > 0 Then
            getItemInfo(itemid)
            Exit Sub
        End If
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
        Dim branchstr As String = ""
        If UsrBr <> "" Then
            branchstr = " LEFT JOIN (SELECT locQIH,itemid litemid,lastcost,locationCost from LocOpnQtyTb " & _
                        "left join LocationTb on LocationTb.LocationID=LocOpnQtyTb.LocationID " & _
                        "where LocCode='" & Dloc & "')LocationTb on InvItm.itemid=LocationTb.litemid "
        End If
        Dim str As String
        Dim costavg As String = IIf(UsrBr = "", "CostAvg", "isnull(locationCost,0)")
        str = "Select Rack,QIH, MRP,Price,[Tax Price],WSP,[C Avg],[Cost+Tax]," & _
                                                     "case when isnull(LPC,0)=0 then opcost else LPC end LPC,itemid from (SELECT Rack," & IIf(UsrBr = "", "QIH", "isnull(locQIH,0)") & _
                                                     " QIH, MRP,UnitPrice Price,(((isnull(IGST,1)+ISNULL(vat,0))*UnitPrice)/100)+UnitPrice [Tax Price]," & _
                                                     "UnitPriceWS WSP," & costavg & " [C Avg]," & _
                                                     "(((isnull(IGST,0)+ISNULL(vat,0))*" & costavg & ")/100)+" & costavg & " [Cost+Tax]," & _
                                                     IIf(UsrBr = "", "LastPurchCost", "isnull(lastcost,0)") & " LPC," & _
                                                     "itemid,opcost FROM INVITM " & _
                                                      " LEFT JOIN GSTTb ON GSTTb.HSNCode=InvItm.HSNCode" & _
                                                      " LEFT JOIN VatMasterTb ON VatMasterTb.vatid=InvItm.vatid " & branchstr & _
                                                     " where itemid=" & itemid & ") tr"
        If dtItemInfo Is Nothing Then
getinfo:
            dtItemInfo = _objcmnbLayer._fldDatatable(str)
            grdItemInfo.DataSource = dtItemInfo
            SetGridItemInfo()
        Else
            'If dtItemInfo.Rows.Count = 0 And itemid > 0 Then GoTo getinfo
            If itemid = 0 And dtItemInfo.Rows.Count > 0 Then dtItemInfo.Rows.Clear() : grdItemInfo.DataSource = dtItemInfo
            Dim _qurey As EnumerableRowCollection(Of DataRow)
            _qurey = (From data In dtItemInfo.AsEnumerable() Where data("itemid") = itemid Select data)
            If _qurey.Count = 0 Then
                dt = _objcmnbLayer._fldDatatable(str)
                If dt.Rows.Count > 0 Then
                    Dim dtRow As DataRow
                    dtRow = dtItemInfo.NewRow
                    dtRow("MRP") = dt(0)("MRP")
                    dtRow("Price") = dt(0)("Price")
                    dtRow("WSP") = dt(0)("WSP")
                    dtRow("C Avg") = dt(0)("C Avg")
                    dtRow("Cost+Tax") = dt(0)("Cost+Tax")
                    dtRow("LPC") = dt(0)("LPC")
                    dtRow("QIH") = dt(0)("QIH")
                    dtRow("itemid") = dt(0)("itemid")
                    dtRow("Tax Price") = dt(0)("Tax Price")
                    dtRow("Rack") = dt(0)("Rack")
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
            .Columns("Rack").Width = 70
            .Columns("Rack").SortMode = DataGridViewColumnSortMode.NotSortable

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

            .Columns("Cost+Tax").Width = 70
            .Columns("Cost+Tax").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("Cost+Tax").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Cost+Tax").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Cost+Tax").Visible = getRight(200, CurrentUser)

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
                    PrepareRpt("ISFI", True)
                End If
            End If
            If frm.chkduplicate.Checked Then
                PrepareRpt("ISFI", True, 1)
            End If
            If frm.chktriplicate.Checked Then
                PrepareRpt("ISFI", True, 2)
            End If
        Else
            PrepareRpt("ISFI", True)
        End If

    End Sub

    Private Sub fCashCust_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fCashCust.FormClosed
        fCashCust = Nothing
    End Sub


    Private Sub loadSalesMultipleDebits(ByVal trid As Long)
        dtMultipleDebits = _objcmnbLayer._fldDatatable("SELECT Alias,AccDescr,accAmt,reference,accid,dbtid FROM SalesMultipleDebitsTb " & _
                                                       "LEFT JOIN AccMast ON AccMast.accid=SalesMultipleDebitsTb.dbaccid where dbtrid=" & trid)
        If trid > 0 Then
            _objcmnbLayer._saveDatawithOutParm("UPDATE SalesMultipleDebitsTb SET setremove=1 " & _
                                          " WHERE dbtrid=" & trid)
        End If
    End Sub

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

    Private Sub btnmultipleDebit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnmultipleDebit.Click
        showMultipleDebits()
    End Sub

    Private Sub btnRv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRv.Click
        If Not fPpayment Is Nothing Then
            fPpayment.Close()
            fPpayment = Nothing
        End If
        fPpayment = New PreviousPaymentFrm
        With fPpayment
            .AccountNo = Val(txtSuppAlias.Tag)
            .dtpstart.Value = DateFrom
            .dtpto.Value = DateTo
            .jvtype = "RV"
            .reference = Trim(txtprefix.Text) & IIf(txtprefix.Text = "", "", "/") & numVchrNo.Text
            .ldGrid(10)
            .ShowDialog()
        End With
        fPpayment = Nothing
    End Sub
    Public Function ImportDOs(ByVal Docids As String, Optional ByVal trasferAllItems As Boolean = False, Optional ByVal fetchcustomer As Boolean = False) As Boolean

        Dim i As Integer
        Dim sRs As DataTable
        Dim UPerPack As Double
        Dim tNumformat As String
        If trasferAllItems Then
            sRs = _objcmnbLayer._fldDatatable("SELECT * from (Select DocTranTb.*, [Item Code],isSerialNo,FraCount,isnull(itemCategory,'')itemCategory," & _
                                              "collectionAC,vat,MRP,withtax, " & _
                                              "isnull(TPQtyInv,0)TPQtyInv,isnull(TPQtyDoc,0)TPQtyDoc,Qty-(isnull(TPQtyInv,0)+isnull(TPQtyDoc,0)) balanceQty,Dno,rgcess,additionalcess," & _
                                              "Discount,rndoff FROM DocTranTb LEFT JOIN DocCmnTb ON DocCmnTb.DocId=DocTranTb.DocId " & _
                                              "LEFT JOIN InvItm ON InvItm.ItemId = DocTranTb.ItemId " & _
                                              "LEFT JOIN InvItmPropertiesTb ON InvItmPropertiesTb.Itemid=invitm.itemid  " & _
                                              "LEFT JOIN UnitsTb ON UnitsTb.Units=DocTranTb.Unit " & _
                                              "LEFT JOIN VatMasterTb ON InvItm.vatId=VatMasterTb.vatId " & _
                                              "LEFT JOIN (select vatcode rgccode,vat rgcess, collectionAC rgcaccount,vatid rgcid from  VatMasterTb)regularcess ON InvItm.regularcessid=regularcess.rgcid " & _
                                              "LEFT JOIN (SELECT impDocSlno, Sum(TrQty/PFraction) As TPQtyInv,Sum(UnitCost * (TrQty/PFraction)) PIAmt FROM ItmInvTrTb  GROUP BY impDocSlno) As PIQ ON PIQ.impDocSlno = DocTranTb.id " & _
                                              "LEFT JOIN (SELECT ImpDocLnNo, Sum(Qty/PFraction) As TPQtyDoc,Sum(CostPUnit * (Qty/PFraction)) As PDAmt FROM DocTranTb  GROUP BY ImpDocLnNo) As PIQD ON PIQD.ImpDocLnNo = DocTranTb.id " & _
                                              "WHERE DocTranTb.docid IN( " & Docids & "))tr where balanceQty>0 ORDER BY SlNo")
        End If

        If fetchcustomer Then
            Dim dt As DataTable
            dt = _objcmnbLayer._fldDatatable("SELECT [Voucher Name] FROM PreFixTb WHERE VrTypeNo = 4 AND Ctgry=3" & IIf(UsrBr = "", "", " AND BrId In ('', '" & UsrBr & "')") & " Order by ordNo")
            If dt.Rows.Count > 0 Then
                cmbVoucherTp.Text = dt(0)("Voucher Name")
            End If
            dt = Nothing
            dt = _objcmnbLayer._fldDatatable("Select DocType,cscode,Ddate from DocCmnTb where docid=" & Val(Docids))
            If dt.Rows.Count > 0 Then
                setCustomer(dt(0)("cscode"))
                chgbyprg = True
                txtCashCustomer.Text = txtSuppName.Text
                chgbyprg = False
                cmbDos.Text = dt(0)("DocType")
                dtpdocdate.Value = dt(0)("Ddate")
                Dim ddiff As Integer
                ddiff = DateDiff(DateInterval.Day, DateValue(cldrdate.Value), DateValue(dtpdocdate.Value))
                txtDescr.Text = "Issue Date : " & dt(0)("Ddate") & " (Days : " & ddiff & ")"
            End If
        End If
        grdVoucher.RowCount = 0
        Dim dno As Integer = 0
        If _objcmnbLayer.dtSerialNo.Rows.Count > 0 Then _objcmnbLayer.dtSerialNo.Rows.Clear()
        chgbyprg = True
        txtDOLst.Text = ""
        Dim discount As Double
        With grdVoucher
            .Rows.Clear()
            If sRs.Rows.Count > 0 Then
                For i = 0 To sRs.Rows.Count - 1
                    If i = 0 Then
                        If IsDBNull(sRs(i)("withtax")) Then sRs(i)("withtax") = 0
                        chktaxInv.Checked = sRs(i)("withtax")
                        'If IsDBNull(sRs(i)("Discount")) Then sRs(i)("Discount") = 0
                        'numDisc.Text = Format(CDbl(sRs(i)("Discount")), numFormat)
                        'If IsDBNull(sRs(i)("rndoff")) Then sRs(i)("rndoff") = 0
                        'If Val(sRs(i)("rndoff")) > 0 Then
                        '    cmbsign.SelectedIndex = 0
                        '    txtroundOff.Text = Format(CDbl(sRs(i)("rndoff")), numFormat)
                        'Else
                        '    cmbsign.SelectedIndex = 1
                        '    txtroundOff.Text = Format(CDbl(sRs(i)("rndoff")) * -1, numFormat)
                        'End If
                    End If
                    If dno <> sRs(i)("DNO") Then
                        txtDOLst.Text = txtDOLst.Text & IIf(txtDOLst.Text = "", "", ",") & sRs(i)("DNO")
                        dno = sRs(i)("DNO")
                    End If
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
                    grdVoucher.Item(ConstDescr, i).Value = IIf(IsDBNull(sRs(i)("TrDetail")), "", sRs(i)("TrDetail"))
                    grdVoucher.Item(ConstB, i).Value = IIf(sRs(i)("Mthd") & "" = "", "B", Trim(sRs(i)("Mthd") & ""))
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
                    grdVoucher.Item(ConstAdditionalcess, i).Value = sRs(i)("additionalcess") / FCRt

                    grdVoucher.Item(ConstLocation, i).Value = ""
                    grdVoucher.Item(ConstQty, i).Value = Format(sRs(i)("balanceQty") / UPerPack, "#,##0" & IIf(Val(grdVoucher.Item(ConstPFraction, i).Value & "") = 0, "", "." & Strings.StrDup(CByte(Val(grdVoucher.Item(ConstPFraction, i).Value & "")), "0")))
                    grdVoucher.Item(ConstUPrice, i).Value = Format(sRs(i)("CostPUnit") * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & lnumFormat)
                    grdVoucher.Item(ConstActualPrice, i).Value = sRs(i)("CostPUnit") * UPerPack / FCRt
                    tNumformat = "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))
                    grdVoucher.Item(ConstLTotal, i).Value = Format((sRs(i)("balanceQty") * sRs(i)("CostPUnit")) * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & lnumFormat)
                    grdVoucher.Item(ConstUnitOthCost, i).Value = 0
                    grdVoucher.Item(ConstActualOthCost, i).Value = 0
                    grdVoucher.Item(ConstSerialNo, i).Value = ""
                    .Item(constItmTot, i).Value = Format((.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value), lnumFormat)
                    If Val(sRs(i)("UnitDiscount") & "") = 0 Then sRs(i)("UnitDiscount") = 0
                    grdVoucher.Item(ConstDiscOther, i).Value = sRs(i)("UnitDiscount") * UPerPack / FCRt

                    discount = discount + (CDbl(grdVoucher.Item(ConstDiscOther, i).Value) * CDbl(grdVoucher.Item(ConstQty, i).Value))

                    If Val(sRs(i)("DisP") & "") = 0 Then sRs(i)("DisP") = 0
                    grdVoucher.Item(ConstDisP, i).Value = Format(sRs(i)("DisP"), numFormat)
                    If Val(sRs(i)("ItemDiscount") & "") = 0 Then sRs(i)("ItemDiscount") = 0
                    grdVoucher.Item(ConstDisAmt, i).Value = Format(sRs(i)("ItemDiscount") * UPerPack / FCRt, lnumFormat)
                    grdVoucher.Item(ConstImpDocId, i).Value = sRs(i)("Docid")
                    grdVoucher.Item(ConstImpLnId, i).Value = sRs(i)("id")

                    grdVoucher.Item(ConstBarcode, i).Value = sRs(i)("HSNCode")
                    grdVoucher.Item(ConstCGSTP, i).Value = sRs(i)("CSGTP")
                    grdVoucher.Item(ConstCGSTAmt, i).Value = sRs(i)("CGSTAMT") / FCRt

                    grdVoucher.Item(ConstSGSTP, i).Value = sRs(i)("SGSTP")
                    grdVoucher.Item(ConstSGSTAmt, i).Value = sRs(i)("SGSTAmt") / FCRt

                    grdVoucher.Item(ConstIGSTP, i).Value = sRs(i)("IGSTP")
                    grdVoucher.Item(ConstIGSTAmt, i).Value = sRs(i)("IGSTAmt") / FCRt

                    grdVoucher.Item(ConstIsSerial, i).Value = 0
                    grdVoucher.Item(ConstId, i).Value = 0

                    If Val(sRs(i)("MRP") & "") = 0 Then sRs(i)("MRP") = 0
                    .Item(ConstMRP, i).Value = CDbl(sRs(i)("MRP"))
                    .Item(ConstBatchCost, i).Value = 0
                    calcualteLineTotal(i)
                Next
            Else
                MsgBox("Document's Entry or Balance Quantity not found under the entered Documents.", MsgBoxStyle.Exclamation)
            End If
        End With
        If sRs.Rows.Count > 0 Then sRs.Clear()
        numDisc.Text = Format(discount, numFormat)
        calculate()

        reArrangeNo()
        numVchrNo.Focus()
        chgbyprg = False
        ChgId = True
        chgPost = True

    End Function
    Private Sub setImport(ByVal EnaImport As Boolean)
        txtSuppAlias.Enabled = EnaImport
        txtSuppName.Enabled = EnaImport
        txtDOLst.Enabled = EnaImport
        btnshow.Enabled = EnaImport
        cmbDos.Enabled = EnaImport
        If EnaImport Then
            btnImport.Tag = ""
        Else
            btnImport.Tag = "Imported"
        End If
    End Sub
    Private Sub calculateWoodDiscountQty(ByVal rIndex As Integer)
        chgbyprg = True
        Dim netQty As Double
        Dim dQty As Double
        Dim tnumformat As String
        Dim ndec1 As Integer
        Dim PPerU As Integer
        With grdVoucher
            PPerU = Val(.Item(ConstPMult, rIndex).Value)
            PPerU = IIf(PPerU = 0, 1, PPerU)
            ndec1 = .Item(ConstPFraction, rIndex).Value
            tnumformat = "#,##0" & IIf(ndec1 = 0, "", "." & Strings.StrDup(ndec1, "0"))
            If Val(.Item(ConstWoodQty, rIndex).Value) = 0 Then .Item(ConstWoodQty, rIndex).Value = 0
            If Val(.Item(ConstWoodDiscQty, rIndex).Value) = 0 Then .Item(ConstWoodDiscQty, rIndex).Value = 0
            netQty = CDbl(.Item(ConstWoodQty, rIndex).Value) * PPerU
            dQty = CDbl(.Item(ConstWoodDiscQty, rIndex).Value)
            If .CurrentCell.ColumnIndex = ConstWoodQty Then
                dQty = getWoodDiscount(Val(grdVoucher.Item(ConstItemID, rIndex).Value), netQty)
                .Item(ConstWoodDiscQty, rIndex).Value = Format(dQty, tnumformat)
            End If
            .Item(ConstQty, rIndex).Value = Format((netQty - dQty) / PPerU, tnumformat)
        End With
        chgbyprg = False
    End Sub
    Public Sub returnFromLodge(ByVal jbid As Long)
        Dim strsql As String
        strsql = "SELECT Jobid,Jobcode,Jobname,alias,AccDescr,custcode " & _
                 "From JobTb LEFT JOIN AccMast on JobTb.custcode=Accmast.accid where Jobid=" & jbid
        Dim dtJob As DataTable = _objcmnbLayer._fldDatatable(strsql)

        If (dtJob.Rows.Count > 0) Then
            chgbyprg = True
            txtJob.Text = dtJob(0)("jobcode")
            txtJob.Tag = Val(dtJob(0)("Jobid"))
            txtjobname.Text = dtJob(0)("jobname")
            txtSuppAlias.Text = dtJob(0)("Alias")
            txtSuppName.Text = dtJob(0)("AccDescr")
            txtSuppName.Tag = dtJob(0)("custcode")
            setCustomer()
            chgbyprg = False
        End If
    End Sub
    Public Sub returnLodgeServices(ByVal roomid As Long, ByVal isserviceAmt As Boolean, Optional ByVal donotCalculate As Boolean = False)
        Dim strsql As String
        Dim dtJob As DataTable
        Dim i As Integer
        Dim ldgServiceId As Long
        With grdVoucher
            If isserviceAmt Then
                strsql = "SELECT sum(LodgeServiceTb.UnitPrice*qty) ServiceAmt,ldgroomid " & _
                "FROM LodgeServiceTb " & _
                "LEFT JOIN (SELECT impDocSlno FROM JobInvTrTb )JBTR ON LodgeServiceTb.ldgServiceId= JBTR.impDocSlno " & _
                "where isnull(impDocSlno,0)=0 and ldgroomid =" & roomid & " group by ldgroomid"
                dtJob = _objcmnbLayer._fldDatatable(strsql)
                Dim serviceAmt As Double
                If dtJob.Rows.Count > 0 Then
                    serviceAmt = dtJob(0)("ServiceAmt")
                    ldgServiceId = dtJob(0)("ldgroomid")
                End If

createNewItem:
                Dim dR As DataTable = getItmDtls(3, "SERVICE", True)
                If (dR.Rows.Count > 0) Then
                    grdVoucher.Rows.Add(1)
                    grdVoucher.CurrentCell = grdVoucher.Item(0, (grdVoucher.RowCount - 1))
                    AddDetails(dR)
                    grdVoucher.Item(ConstQty, (grdVoucher.RowCount - 1)).Value = Strings.Format(1, numFormat)
                    grdVoucher.Item(ConstUPrice, (grdVoucher.RowCount - 1)).Value = Format(serviceAmt, numFormat)
                    grdVoucher.Item(ConstActualPrice, (grdVoucher.RowCount - 1)).Value = serviceAmt
                    grdVoucher.Item(constItmTot, (grdVoucher.RowCount - 1)).Value = serviceAmt
                    .Item(ConstImpDocId, grdVoucher.RowCount - 1).Value = ldgServiceId
                    getGSTDetails(Trim(.Item(ConstBarcode, i).Value & ""), grdVoucher.RowCount - 1, True)
                Else
                    createInstantItem("SERVICE", "SERVICE ITEM", "", 0, "Service")
                    GoTo createNewItem
                End If
            Else
                strsql = "SELECT [item code],INVITM.HSNCode,Description,Unit,LodgeServiceTb.UnitPrice,ISNULL(CGST,0)CGST,isnull(SGST,0)SGST,isnull(IGST,0)IGST,ItemId,qty,ldgroomid,ldgServiceId " & _
                "FROM INVITM " & _
                "LEFT JOIN LodgeServiceTb ON LodgeServiceTb.ldgServiceItemid=INVITM.Itemid " & _
                "LEFT JOIN (SELECT impDocSlno FROM JobInvTrTb )JBTR ON LodgeServiceTb.ldgServiceId= JBTR.impDocSlno " & _
                "LEFT JOIN GSTTb ON GSTTb.HSNCode=InvItm.HSNCode " & _
                "where isnull(impDocSlno,0)=0 and ldgroomid in(" & roomid & ")"
                dtJob = _objcmnbLayer._fldDatatable(strsql)
                Dim num3 As Integer = (dtJob.Rows.Count - 1)
                Dim num As Integer
                i = 0
                Do While (i <= num3)
                    .Rows.Add(1)
                    num = (.RowCount - 1)
                    .Item(ConstSlNo, num).Value = (num + 1)
                    .Item(ConstItemCode, num).Value = dtJob(i)("item code")
                    .Item(ConstBarcode, num).Value = dtJob(i)("HSNCode")
                    .Item(ConstDescr, num).Value = dtJob(i)("Description")
                    .Item(ConstB, num).Value = "B"
                    .Item(ConstUnit, num).Value = dtJob(i)("Unit")
                    .Item(ConstQty, num).Value = Format(dtJob(i)("qty"), numFormat)
                    .Item(ConstUPrice, num).Value = Format(CDbl(dtJob(i)("UnitPrice")), numFormat)
                    .Item(ConstActualPrice, num).Value = dtJob(i)("UnitPrice")
                    .Item(constItmTot, num).Value = Format(1 * CDbl(dtJob(i)("UnitPrice")), numFormat)
                    .Item(ConstItemID, num).Value = Val(dtJob(i)("ItemId"))
                    .Item(ConstId, num).Value = 0
                    If EnableGST Then
                        .Item(ConstTaxP, num).Value = Format(CDbl(dtJob(i)("IGST")), numFormat)
                        .Item(ConstCGSTP, num).Value = dtJob(i)("CGST")
                        .Item(ConstSGSTP, num).Value = dtJob(i)("SGST")
                        .Item(ConstIGSTP, num).Value = CDbl(dtJob(i)("CGST")) + CDbl(dtJob(i)("SGST"))
                        getGSTDetails(Trim(.Item(ConstBarcode, i).Value & ""), num, True)
                    End If
                    .Item(ConstPFraction, num).Value = 2
                    .Item(ConstPMult, num).Value = 1
                    .Item(ConstImpDocId, num).Value = dtJob(i)("ldgroomid")
                    .Item(ConstImpLnId, grdVoucher.RowCount - 1).Value = dtJob(i)("ldgServiceId")
                    i += 1
                Loop
            End If
        End With
        If Not donotCalculate Then
            reArrangeNo()
            calculate(False)
        End If
    End Sub
    Public Sub returnLodgeRoomAndServices(ByVal roomid As Long, ByVal isserviceAmt As Boolean, ByVal isIncludeService As Boolean)
        Dim strsql As String
        strsql = "SELECT [item code],case when isnull(LodgeRoomTb.HSNCode,'')='' then INVITM.HSNCode else isnull(LodgeRoomTb.HSNCode,'') end HSNCode,Description,Unit,rent UnitPrice,ISNULL(gst,0)/2 CGST,isnull(gst,0)/2 SGST,isnull(gst,0)IGST,ItemId," & _
        "datediff(d,checkinDateTime,checkoutDateTime) qty,ldgroomid,checkinDateTime,checkoutDateTime,cess,collectionAC FROM INVITM " & _
        "LEFT JOIN LodgeRoomTb ON LodgeRoomTb.roomItemid=INVITM.Itemid " & _
        " LEFT JOIN VatMasterTb ON VatMasterTb.vatid=InvItm.vatid " & _
        "LEFT JOIN (SELECT impDocid FROM JobInvTrTb )JBTR ON LodgeRoomTb.ldgroomid= JBTR.impDocid " & _
        "LEFT JOIN GSTTb ON GSTTb.HSNCode=InvItm.HSNCode " & _
        "where isnull(impDocid,0)=0 and ldgroomid =" & roomid
        Dim checkinTime As Date
        Dim checkouttime As Date
        Dim checkindate As Date
        Dim checkoutdate As Date
        'Dim roomrent As Double
        Dim rentduration As Integer
        Dim dtJob As DataTable = _objcmnbLayer._fldDatatable(strsql)
        SetGridHead()
        'grdVoucher.Rows.Clear()
        Dim num3 As Integer = (dtJob.Rows.Count - 1)
        Dim num As Integer
        Dim i As Integer = 0
        'If CDbl(lbltax.Text) = 0 Then chktaxInv.Checked = False
        With grdVoucher
            Do While (i <= num3)
                .Rows.Add(1)
                num = (.RowCount - 1)
                .Item(ConstSlNo, num).Value = (num + 1)
                .Item(ConstItemCode, num).Value = dtJob(i)("item code")
                .Item(ConstBarcode, num).Value = dtJob(i)("HSNCode")
                .Item(ConstDescr, num).Value = dtJob(i)("Description")
                .Item(ConstB, num).Value = "B"
                .Item(ConstUnit, num).Value = dtJob(i)("Unit")
                checkindate = DateValue(dtJob(i)("checkinDateTime"))
                checkinTime = TimeValue(dtJob(i)("checkinDateTime"))
                If Trim(dtJob(i)("checkoutDateTime") & "") = "" Then
                    checkoutdate = DateValue(Date.Now)
                    checkouttime = TimeValue(Date.Now)
                Else
                    checkoutdate = DateValue(dtJob(i)("checkoutDateTime"))
                    checkouttime = TimeValue(dtJob(i)("checkoutDateTime"))
                End If
                rentduration = DateDiff(DateInterval.Day, checkindate, checkoutdate) + 1
                If checkouttime <= checkinTime And rentduration > 1 Then
                    rentduration = rentduration - 1
                End If

                .Item(ConstQty, num).Value = Format(rentduration, numFormat)
                .Item(ConstUPrice, num).Value = Format(CDbl(dtJob(i)("UnitPrice")), numFormat)
                .Item(ConstActualPrice, num).Value = dtJob(i)("UnitPrice")
                .Item(constItmTot, num).Value = Format(1 * CDbl(dtJob(i)("UnitPrice")), numFormat)
                .Item(ConstItemID, num).Value = Val(dtJob(i)("ItemId"))
                .Item(ConstId, num).Value = 0
                If EnableGST And chktaxInv.Checked Then
                    .Item(ConstTaxP, num).Value = Format(CDbl(dtJob(i)("IGST")), numFormat)
                    .Item(ConstCGSTP, num).Value = dtJob(i)("CGST")
                    .Item(ConstSGSTP, num).Value = dtJob(i)("SGST")
                    .Item(ConstIGSTP, num).Value = CDbl(dtJob(i)("CGST")) + CDbl(dtJob(i)("SGST"))
                    getGSTDetails(Trim(.Item(ConstBarcode, i).Value & ""), num, True)
                End If
                If enableFloodCess And Val(lblgstn.Tag & "") = 0 And cessenddate >= DateValue(cldrdate.Value) Then
                    .Item(Constcess, num).Value = Format(IIf(IsDBNull(dtJob(0)("cess")), 0, CDbl(dtJob(0)("cess"))), lnumFormat)
                    .Item(ConstcessAc, num).Value = IIf(IsDBNull(dtJob(0)("collectionAC")), 0, Val(dtJob(0)("collectionAC") & ""))
                    .Item(ConstcessAmt, num).Value = (((CDbl(.Item(ConstActualPrice, num).Value) * .Item(Constcess, num).Value) / 100) * CDbl(.Item(ConstQty, num).Value))
                    .Item(ConstcessAmt, num).Value = Format(CDbl(.Item(ConstcessAmt, num).Value), lnumFormat)
                End If
                .Item(ConstPFraction, num).Value = 2
                .Item(ConstPMult, num).Value = 1
                .Item(ConstImpDocId, num).Value = dtJob(i)("ldgroomid")
                .Rows(num).DefaultCellStyle.BackColor = Color.LightGray
                .Rows(num).ReadOnly = True
                calcualteLineTotal(num)
                .Rows.Add(1)
                num = (.RowCount - 1)
                .Item(ConstSlNo, num).Value = "M"
                .Item(ConstItemCode, num).Value = ""
                .Item(ConstDescr, num).Value = "Check in : " & Format(dtJob(i)("checkinDateTime"), "dd/MM/yyyy hh:mm:ss tt") & " Check Out : " & Format(dtJob(i)("checkoutDateTime"), "dd/MM/yyyy hh:mm:ss tt")
                .Item(ConstPFraction, num).Value = 2
                .Item(ConstPMult, num).Value = 1
                .Item(ConstUnit, num).Value = ""
                '.Rows(num).DefaultCellStyle.BackColor = Color.LightGray
                i += 1

            Loop
            If isIncludeService Then
                returnLodgeServices(roomid, isserviceAmt, True)
            End If

        End With

        reArrangeNo()
        'MsgBox(grdVoucher.Item(Constcess, 2).Value)
        'calculate(, True)

    End Sub
    Public Sub returnLodgeMemberShip(ByVal jobid As Long, ByVal itemids As String)
        Dim strsql As String
        strsql = "SELECT [item code],INVITM.HSNCode,Description,Unit,price UnitPrice,ISNULL(CGST,0)  CGST,isnull(SGST,0)  SGST,isnull(IGST,0)IGST,INVITM.ItemId," & _
        "1 qty,vat cess,collectionAC,custcode,jobcode,renewid FROM INVITM " & _
        "inner JOIN MembershipRenewalTb ON MembershipRenewalTb.itemid=INVITM.Itemid " & _
        "LEFT JOIN JobTb ON JobTb.Jobid=MembershipRenewalTb.jobid " & _
        "LEFT JOIN VatMasterTb ON VatMasterTb.vatid=InvItm.vatid " & _
        "LEFT JOIN GSTTb ON GSTTb.HSNCode=InvItm.HSNCode " & _
        "where MembershipRenewalTb.renewid in (" & itemids & ") and JobTb.jobid=" & jobid

        Dim dtJob As DataTable = _objcmnbLayer._fldDatatable(strsql)
        SetGridHead()
        Dim dtcount As Integer = (dtJob.Rows.Count - 1)
        Dim rIndex As Integer
        Dim i As Integer = 0
        chgbyprg = True
        chkcal.Checked = True
        With grdVoucher
            Do While (i <= dtcount)
                .Rows.Add(1)
                rIndex = (.RowCount - 1)
                If rIndex = 0 Then
                    chgbyprg = True
                    txtJob.Text = dtJob(i)("jobcode")
                    setCustomer(dtJob(i)("custcode"))
                End If
                .Item(ConstSlNo, rIndex).Value = (rIndex + 1)
                .Item(ConstItemCode, rIndex).Value = dtJob(i)("item code")
                .Item(ConstBarcode, rIndex).Value = dtJob(i)("HSNCode")
                .Item(ConstDescr, rIndex).Value = dtJob(i)("Description")
                .Item(ConstB, rIndex).Value = "B"
                .Item(ConstUnit, rIndex).Value = ""
                .Item(ConstQty, rIndex).Value = Format(1, numFormat)
                .Item(ConstUPrice, rIndex).Value = Format(CDbl(dtJob(i)("UnitPrice")), numFormat)
                .Item(ConstActualPrice, rIndex).Value = dtJob(i)("UnitPrice")
                .Item(constItmTot, rIndex).Value = Format(1 * CDbl(.Item(ConstActualPrice, rIndex).Value), numFormat)
                .Item(ConstItemID, rIndex).Value = Val(dtJob(i)("ItemId"))
                .Item(ConstId, rIndex).Value = 0
                If EnableGST And chktaxInv.Checked Then
                    .Item(ConstTaxP, rIndex).Value = Format(CDbl(dtJob(i)("IGST")), numFormat)
                    .Item(ConstCGSTP, rIndex).Value = dtJob(i)("CGST")
                    .Item(ConstSGSTP, rIndex).Value = dtJob(i)("SGST")
                    .Item(ConstIGSTP, rIndex).Value = CDbl(dtJob(i)("CGST")) + CDbl(dtJob(i)("SGST"))
                    getGSTDetails(Trim(.Item(ConstBarcode, i).Value & ""), rIndex, True)
                End If
                If enableFloodCess And Val(lblgstn.Tag & "") = 0 And cessenddate >= DateValue(cldrdate.Value) Then
                    If Val(dtJob(0)("cess") & "") = 0 Then dtJob(0)("cess") = 0
                    .Item(Constcess, i).Value = Format(IIf(IsDBNull(dtJob(0)("cess")), 0, CDbl(dtJob(0)("cess"))), lnumFormat)
                    .Item(ConstcessAc, i).Value = IIf(IsDBNull(dtJob(0)("collectionAC")), 0, Val(dtJob(0)("collectionAC") & ""))
                    .Item(ConstcessAmt, i).Value = (((CDbl(.Item(ConstActualPrice, i).Value) * .Item(Constcess, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value))
                    .Item(ConstcessAmt, i).Value = Format(CDbl(.Item(ConstcessAmt, i).Value), lnumFormat)
                End If
                .Item(ConstPFraction, rIndex).Value = 2
                .Item(ConstPMult, rIndex).Value = 1
                .Item(ConstImpJobChildTbID, rIndex).Value = dtJob(i)("renewid")
                i += 1
                '.Rows(rIndex).DefaultCellStyle.BackColor = Color.LightGray
                chgUprice = True
                calculateTaxFromUnitPrice(rIndex)
                calcualteLineTotal(rIndex)
            Loop
        End With
        reArrangeNo()
        calculate(, True)
        chgbyprg = False
    End Sub

    Private Sub grdVoucher_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellEnter
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
    End Sub

    Private Sub chktaxInv_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chktaxInv.CheckedChanged
        If chgbyprg Then Exit Sub
        calculate()
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
    Private Sub modifyItem(ByVal itemid As Long)
        If itemid = 0 Then Exit Sub
        If Not fproductMast Is Nothing Then fproductMast.Close() : fproductMast = Nothing
        fproductMast = New ItemMastFrm
        fproductMast.MdiParent = fMainForm
        fproductMast.txtCode.Tag = itemid
        fproductMast.Show()
        fproductMast.loadFromTransactions()
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

    Private Sub chktaxwithoutLinediscount_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chktaxwithoutLinediscount.CheckedChanged
        If chgbyprg Then Exit Sub
        calculate(, True)
    End Sub

    Private Sub cmbdeliveredBy_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbdeliveredBy.SelectedIndexChanged
        If chgbyprg Then Exit Sub
        chgPost = True
    End Sub


    Private Sub txtroundOff_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtroundOff.KeyPress
        chkautoroundOff.Checked = False
        NumericTextOnKeypress(sender, e, chgNumByPgm, lnumFormat)
    End Sub
    Private Sub txtroundOff_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtroundOff.TextChanged
        If chgNumByPgm Then Exit Sub
        chkautoroundOff.Checked = False
        calculate(, , , True)
    End Sub

    Private Sub chkexport_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkexport.CheckedChanged
        chktaxInv.Checked = Not chkexport.Checked
        chktaxInv.Enabled = Not chkexport.Checked
    End Sub

    Private Sub btnshow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnshow.Click
        If Val(txtSuppAlias.Tag) = 0 Then
            MsgBox("Invalid Customer", MsgBoxStyle.Exclamation)
            txtSuppName.Focus()
            Exit Sub
        End If
        If Not fDolist Is Nothing Then fDolist.Close() : fDolist = Nothing
        fDolist = New DOListFrm
        With fDolist
            .txtSuppName.Text = txtSuppName.Text
            .txtSuppName.Tag = txtSuppAlias.Tag
            .lbldoc.Text = cmbDos.Text
            .loadDocs()
            .ShowDialog()
        End With
    End Sub

    Private Sub fDolist_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fDolist.FormClosed
        fDolist = Nothing
    End Sub

    Private Sub fDolist_transferItems(ByVal docids As String, ByVal isallitem As Boolean) Handles fDolist.transferItems
        ImportDOs(docids, isallitem)
    End Sub
    Private Sub setHistory(ByVal ItemId As Long, ByVal strCode As String, Optional ByVal PartyId As Long = 0, Optional ByVal strType As String = "")
        If fHistory Is Nothing Then
            fHistory = New SelectHistory
        End If
        With fHistory
            If strType <> "" Then
                .strType = strType
            Else
                Exit Sub
            End If
            .Itemid = ItemId
            .ItemCode = strCode
            .PartyId = PartyId
            .dt_From.Value = DateFrom
            .jobcode = ""
            If fHistory.Visible Then
                .setEnquiryLoad()
            Else
                .Show()
                .Top = Me.Top + Screen.PrimaryScreen.WorkingArea.Height - .Height + 20
            End If
        End With
    End Sub

    Private Sub btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnload.Click
        With grdVoucher
            If .CurrentRow Is Nothing Then Exit Sub
            setHistory(Val(.Item(ConstItemID, .CurrentRow.Index).Value), .Item(ConstItemCode, .CurrentRow.Index).Value.ToString, Val(txtSuppAlias.Tag), cmbvoucher.Text)

        End With
    End Sub
    Private Sub fHistory_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fHistory.FormClosed
        fHistory = Nothing
    End Sub

    Private Sub txtOthrRef_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtOthrRef.KeyDown, txtOthrDescription.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub numOtherAmt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles numOtherAmt.KeyDown
        If e.KeyCode = Keys.Return Then
            btnothadd.Focus()
        End If
    End Sub

    Private Sub numOtherAmt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles numOtherAmt.KeyPress
        NumericTextOnKeypress(sender, e, chgbyprgN, lnumFormat)
    End Sub
    Private Sub calculateothercostTax()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT vat,paymentAC,vatcode FROM VatMasterTb where vatcode='" & cmbtax.Text & "'")
        If dt.Rows.Count > 0 Then
            txtotherTax.Text = Format(CDbl(numOtherAmt.Text) * CDbl(dt(0)("vat")) / 100, numFormat)
            cmbtax.Tag = dt(0)("paymentAC")
            txtotherTax.Tag = dt(0)("vat")
            btnadd.Tag = dt(0)("vatcode")
        Else
            txtotherTax.Text = "0.00"
            cmbtax.Tag = ""
            btnadd.Tag = ""
            txtotherTax.Tag = ""
        End If
    End Sub

    Private Sub cmbtax_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbtax.SelectedIndexChanged
        calculateothercostTax()
    End Sub
    Private Sub setOthercostTax()
        Dim i As Integer
        Dim taxamt As Double
        Dim totaltax As Double
        Dim totothercostTaxable As Double
        With grdOtherCost
            For i = 0 To grdOtherCost.RowCount - 1
                For j = 0 To dtTax.Rows.Count - 1
                    If Val(.Item(CostvatAcc, i).Value) = dtTax(j)("acid") Then
                        taxamt = CDbl(.Item(CostAmount, i).Value) * CDbl(.Item(CostvatOther, i).Value) / 100
                        totothercostTaxable = totothercostTaxable + CDbl(.Item(CostAmount, i).Value)
                        dtTax(j)("Amount") = CDbl(dtTax(j)("Amount")) + taxamt
                        Exit For
                    End If
                Next
                totaltax = totaltax + taxamt
            Next
        End With
        lblothercosttax.Text = Format(totaltax, lnumformat)
        lbltaxablecost.Text = Format(totothercostTaxable, lnumformat)
    End Sub

    Private Sub btnothercost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnothercost.Click
        tbothercost.Visible = Not tbothercost.Visible
        If tbothercost.Visible Then
            txtcredit.Focus()
            clearOtherCostFileds()
            If btnOthrOk.Tag <> "chg" Then
                showOtherCost(False)
            End If
        End If
        calculate(False)
    End Sub

    Private Sub clearOtherCostFileds()
        chgbyprg = True
        txtdebit.Text = txtSuppName.Text
        txtdebit.Tag = Val(txtSuppAlias.Tag)
        txtcredit.Text = ""
        txtOthrDescription.Text = ""
        txtOthrRef.Text = ""
        numOtherAmt.Text = Format(0, lnumformat)
        txtcredit.Tag = ""
        txtotherTax.Tag = ""
        cmbtax.Tag = ""
        txtotherTax.Text = ""
        cmbtax.Text = ""
        btnadd.Tag = ""
        chgbyprg = False
    End Sub
    Private Sub showOtherCost(ByVal CrossBr As Boolean)
        Dim sRs As DataTable
        Dim i As Integer
        Dim rw As Integer
        Dim taxamt As Double
        Dim totaltax As Double
        Dim totothercostTaxable As Double
        grdOtherCost.Rows.Clear()
        If enableGCC Then
            sRs = _objcmnbLayer._fldDatatable("SELECT AccTrDet.*, AccDescr, Alias, BranchId,isnull(vatcode,'')vatcode,isnull(vat,0)vat,isnull(paymentAC,0)paymentAC FROM AccTrDet " & _
                                          "LEFT JOIN AccMast ON AccMast.accid = AccTrDet.AccountNo " & _
                                          "LEFT JOIN VatMasterTb ON VatMasterTb.vatid = AccTrDet.vatid " & _
                                          "WHERE  LinkNo IN (SELECT LinkNo FROM AccTrCmn " & _
                                          "WHERE JVType = 'IS' AND JVNum = " & Val(numVchrNo.Text) & " AND PreFix = '" & MkDbSrchStr(Trim(txtprefix.Text)) & "') AND OthCost > 0  ORDER BY OthCost, DealAmt DESC")
        Else
            sRs = _objcmnbLayer._fldDatatable("SELECT AccTrDet.*, AccDescr, Alias, BranchId FROM AccTrDet " & _
                                          "LEFT JOIN AccMast ON AccMast.accid = AccTrDet.AccountNo " & _
                                          "WHERE  LinkNo IN (SELECT LinkNo FROM AccTrCmn WHERE JVType = 'IS' AND JVNum = " & Val(numVchrNo.Text) & " AND PreFix = '" & MkDbSrchStr(Trim(txtprefix.Text)) & "') AND OthCost > 0  ORDER BY OthCost, DealAmt DESC")
        End If

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
                        If enableGCC Then
                            .Item(CostvatOther, i).Value = Format(Val(sRs(rw)("vat") & ""), lnumformat)
                            .Item(Costvatcode, i).Value = Trim("" & sRs(rw)("vatcode"))
                            .Item(CostvatAcc, i).Value = Val(sRs(rw)("paymentAC") & "")
                            If Val(sRs(rw)("vat") & "") > 0 Then
                                taxamt = CDbl(.Item(CostAmount, i).Value) * CDbl(.Item(CostvatOther, i).Value) / 100
                                totaltax = totaltax + taxamt
                                totothercostTaxable = totothercostTaxable + CDbl(.Item(CostAmount, i).Value)
                            End If
                        End If
                        ttlOthCost = ttlOthCost + sRs(rw)("DealAmt")

                    Else
                        .Item(CostCrName, i).Value = Trim("" & sRs(rw)("AccDescr"))
                        .Item(CostCrAcc, i).Value = sRs(rw)("AccountNo")
                    End If
                Next
            End With
            lblOthCost.Text = Format(ttlOthCost, lnumformat)
            OthCost = ttlOthCost
            lblothercosttax.Text = Format(totaltax, lnumformat)
            lbltaxablecost.Text = Format(totothercostTaxable, lnumFormat)

        Else
            lblothercosttax.Text = Format(0, lnumformat)
            lbltaxablecost.Text = Format(0, lnumformat)
            lblOthCost.Text = Format(0, lnumformat)
        End If

        lbltotalwithOC.Text = Format(CDbl(lblNetAmt.Text) + CDbl(lblOthCost.Text), lnumFormat)

    End Sub



    Private Sub setOtherCostHead()
        With grdOtherCost

            SetGridProperty(grdOtherCost)

            .ColumnCount = 14
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

            .Columns(CostvatOther).HeaderText = "Vat%"
            .Columns(CostvatOther).Width = 75
            .Columns(CostvatOther).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(CostvatOther).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(CostvatOther).ReadOnly = True
            .Columns(CostvatOther).Visible = enableGCC

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
            .Columns(CostvatAcc).Visible = False
            .Columns(Costvatcode).Visible = False


        End With
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
            txtOthrRef.Focus()
        ElseIf Val(numOtherAmt.Text) = 0 Then
            MsgBox("Invalid Amount", MsgBoxStyle.Exclamation, Nothing)
            numOtherAmt.Focus()
        Else
            AddRowOthCost()
            clearOtherCostFileds()
            txtcredit.Focus()
        End If
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
    Private Sub AddRowOthCost()

        Dim num As Integer
        With grdOtherCost
            If Val(.Tag) = 0 Then
                .Rows.Add(1)
                num = .RowCount - 1
            Else
                num = Val(.Tag) - 1
            End If
            If dtTax.Rows.Count = 0 And enableGCC Then CalculateGST()
            .Item(CostDbName, num).Value = txtdebit.Text
            .Item(CostAmount, num).Value = Format(CDbl(numOtherAmt.Text), lnumformat)
            .Item(CostReference, num).Value = txtOthrRef.Text
            If txtOthrDescription.Text = "" Then
                .Item(CostDescr, num).Value = txtcredit.Text & " ON SALES " & "/" & numVchrNo.Text
            Else
                .Item(CostDescr, num).Value = txtOthrDescription.Text
            End If
            .Item(CostCrName, num).Value = txtcredit.Text
            .Item(CostFCAmount, num).Value = Format(CDbl(numOtherAmt.Text), lnumformat)
            .Item(CostFCName, num).Value = cmbfc.Text
            .Item(CostFCRate, num).Value = FCRt
            .Item(CostDrAcc, num).Value = Val(txtdebit.Tag)
            .Item(CostCrAcc, num).Value = Val(txtcredit.Tag)
            .Item(CostvatOther, num).Value = Val(txtotherTax.Tag)
            .Item(CostvatAcc, num).Value = Val(cmbtax.Tag)
            .Item(Costvatcode, num).Value = Trim(btnadd.Tag & "")
            .Tag = 0


        End With
        OthrreArrangeNo()
        calculate(False)
        If enableGCC Then setOthercostTax()
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

    Private Sub btnOthrOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOthrOk.Click
        tbothercost.Visible = False
        btnOthrOk.Tag = "chg"
        calculate(False)
    End Sub

    Private Sub btnothcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnothcancel.Click
        tbothercost.Visible = False
        btnOthrOk.Tag = ""
    End Sub

    Private Sub fCashCust_selectcust(ByVal custname As String, ByVal custaddress As String, ByVal Cashcustid As Long, ByVal cardnumber As String, ByVal phone As String, ByVal gstn As String) Handles fCashCust.selectcust
        chgbyprg = True
        txtCashCustomer.Text = custname
        txtcustAddress.Text = custaddress
        txtCashCustomer.Tag = Cashcustid
        If gstn <> "" Then
            lblgstn.Text = "GSTN: " & gstn
            chktaxInv.Checked = True
            lblgstn.Tag = 1
            lblgstn.Visible = True
            chkb2b.Checked = True
        End If
        chgbyprg = False
    End Sub

    Private Sub chkcal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkcal.Click
        _objcmnbLayer._saveDatawithOutParm("Update CompanyTb set calcluatetaxFrompriceInv =" & IIf(chkcal.Checked, 1, 0))
        calcluatetaxFrompriceInv = chkcal.Checked
    End Sub

    Private Sub chkautoroundOff_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkautoroundOff.CheckedChanged
        If chgbyprg Then Exit Sub
        calculate()
    End Sub

    Private Sub txtroundOff_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtroundOff.Validated
        calculate(, , , True)
    End Sub

    Private Sub txtCashCustomer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCashCustomer.TextChanged
        If chgbyprg = True Then Exit Sub
        If vtype <> "Credit" Then
            _srchTxtId = 5
        Else
            _srchTxtId = 2
        End If
        _srchOnce = False
        ShowFmlist(sender)
        'btnUpdate.Enabled = Val(btnUpdate.Tag) > 0
        chgPost = True
    End Sub

    Private Sub txtCashCustomer_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCashCustomer.Validated
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
        If txtCashCustomer.Text = "" Then Exit Sub
        If vtype = "Credit" Then
            setCustomer(, , , False)
            txtDescr.Focus()
        Else
            setCashCustomer()
        End If
        chgbyprg = True
        txtCashCustomer.Text = UCase(txtCashCustomer.Text)
        chgbyprg = False
    End Sub
    Private Sub setCashCustomer(Optional ByVal accid As Integer = 0)
        Dim dt As DataTable
        If accid > 0 Then
            dt = _objcmnbLayer._fldDatatable("Select * from CashCustomerTb where customeraccount=" & accid)
        Else
            dt = _objcmnbLayer._fldDatatable("Select * from CashCustomerTb where custid=" & Val(txtCashCustomer.Tag))
        End If

        If dt.Rows.Count > 0 Then
            chgbyprg = True
            txtcustAddress.Text = Trim(dt(0)("Add1") & "")
            txtCashCustomer.Text = Trim(dt(0)("CustName") & "")
            chgbyprg = False
            If Trim(dt(0)("Add2") & "") <> "" Then
                txtcustAddress.Text = txtcustAddress.Text & vbCrLf & Trim(dt(0)("Add2") & "")
            End If
            If Trim(dt(0)("Add3") & "") <> "" Then
                txtcustAddress.Text = txtcustAddress.Text & vbCrLf & Trim(dt(0)("Add3") & "")
            End If
            If Trim(dt(0)("Phone") & "") <> "" Then
                txtcustAddress.Text = txtcustAddress.Text & vbCrLf & Trim(dt(0)("Phone") & "")
                'txtcustAddress.Text = txtcustAddress.Text & "  GSTIN: " & Trim(dt(0)("GSTIN") & "")
                txtphone.Text = Trim(dt(0)("Phone") & "")
            End If
            If Trim(dt(0)("GSTN") & "") <> "" Then
                lblgstn.Text = "GSTN: " & Trim(dt(0)("GSTN") & "")
                chktaxInv.Checked = True
                lblgstn.Tag = 1
                lblgstn.Visible = True
                chkb2b.Checked = True
            Else
                lblgstn.Text = "GSTN: Nill"
                lblgstn.Tag = 0
                chkb2b.Checked = False
            End If
        End If
    End Sub
    Private Sub chkretailprice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkretailprice.Click, chksecondprice.Click, chkws.Click
        Dim ctrl As CheckBox
        ctrl = sender
        If ctrl.Checked Then
            Select Case ctrl.Name
                Case "chkretailprice"
                    chkws.Checked = False
                    chksecondprice.Checked = False
                Case "chksecondprice"
                    chkws.Checked = False
                    chkretailprice.Checked = False
                Case Else
                    chksecondprice.Checked = False
                    chkretailprice.Checked = False
            End Select
        End If
        fetchPrice()
    End Sub



    Private Sub txtcap1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtcap1.KeyDown, txtcap2.KeyDown, txtcap3.KeyDown, txtcap4.KeyDown, txtcap5.KeyDown, _
                                                                                                                txtinfo1.KeyDown, txtinfo2.KeyDown, txtinfo3.KeyDown, txtinfo4.KeyDown, txtinfo5.KeyDown
        If e.KeyCode = Keys.Return Then
            Dim ctrl As TextBox = sender
            If ctrl.Name = "txtinfo5" Then
                txtDescr.Focus()
            Else
                SendKeys.Send("{TAB}")
            End If

        End If
    End Sub

    Private Sub txtcap1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcap1.TextChanged, txtcap2.TextChanged, txtcap3.TextChanged, txtcap4.TextChanged, txtcap5.TextChanged, _
                                                                                                        txtinfo1.TextChanged, txtinfo2.TextChanged, txtinfo3.TextChanged, txtinfo4.TextChanged, txtinfo5.TextChanged
        If chgbyprg Then Exit Sub
        chgPost = True
    End Sub

    Private Sub TabControl2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl2.Click
        If TabControl2.SelectedIndex = 2 Then
            txtcap1.Focus()
        End If
    End Sub
    Private Sub loadAddInfoCap()
        'Dim dt As DataTable
        chgbyprg = True
        'dt = _objcmnbLayer._fldDatatable("Select * from AdditionalInfoCaptionTb")
        If CaptionTb.Rows.Count > 0 Then
            txtcap1.Text = CaptionTb(0)("cap1")
            txtcap2.Text = CaptionTb(0)("cap2")
            txtcap3.Text = CaptionTb(0)("cap3")
            txtcap4.Text = CaptionTb(0)("cap4")
            txtcap5.Text = CaptionTb(0)("cap5")
        Else
            txtcap1.Tag = 0
            txtcap1.Text = ""
            txtcap2.Text = ""
            txtcap3.Text = ""
            txtcap4.Text = ""
            txtcap5.Text = ""
        End If
        chgbyprg = False
    End Sub

    Private Sub chkb2b_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkb2b.Click
        If LinkB2BWithWSPrice Then
            If chkb2b.Checked Then
                chkws.Checked = True
                chkretailprice.Checked = False
                chksecondprice.Checked = False
            Else
                chkws.Checked = False
                chkretailprice.Checked = True
                chksecondprice.Checked = False
            End If
        End If


    End Sub

    Private Sub btnsetpricedefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsetpricedefault.Click
        Dim pricetext As String
        If chkretailprice.Checked Then
            pricetext = "Retail"
        ElseIf chksecondprice.Checked Then
            pricetext = "Dealer"
        Else
            pricetext = "Wholesale"
        End If
        If MsgBox("Do you want to set " & pricetext & " price as default?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            If chkretailprice.Checked Then
                priceInSales = 1
            ElseIf chksecondprice.Checked Then
                priceInSales = 2
            Else
                priceInSales = 3
            End If
        End If
        _objcmnbLayer._saveDatawithOutParm("Update CompanyTb set priceInSales =" & priceInSales)
    End Sub
    Private Sub fetchPrice()
        Dim itemids As String = ""
        Dim itemid As Long
        Dim i As Integer
        For i = 0 To grdVoucher.RowCount - 1
            With grdVoucher
                itemids = itemids & IIf(itemids = "", "", ",") & Val(.Item(ConstItemID, i).Value)
            End With
        Next
        If itemids = "" Then Exit Sub
        Dim dt As DataTable
        Dim bDatatable As DataTable
        Dim _qurey As EnumerableRowCollection(Of DataRow)
        dt = _objcmnbLayer._fldDatatable("Select UnitPrice,UnitPriceWS,secondPrice,itemid from invitm where itemid in (" & itemids & ")")
        For i = 0 To grdVoucher.RowCount - 1
            With grdVoucher
                itemid = Val(.Item(ConstItemID, i).Value)
                _qurey = From data In dt.AsEnumerable() Where data("itemid") = itemid Select data
                If _qurey.Count > 0 Then
                    bDatatable = _qurey.CopyToDataTable()
                Else
                    bDatatable = dt.Clone
                End If
                If bDatatable.Rows.Count > 0 Then
                    If chkretailprice.Checked Then
                        .Item(ConstActualPrice, i).Value = bDatatable(0)("UnitPrice")
                    ElseIf chksecondprice.Checked Then
                        .Item(ConstActualPrice, i).Value = bDatatable(0)("secondPrice")
                    Else
                        .Item(ConstActualPrice, i).Value = bDatatable(0)("UnitPriceWS")
                    End If
                End If
                .Item(ConstUPrice, i).Value = Format(CDbl(.Item(ConstActualPrice, i).Value), lnumFormat)
            End With
            calcualteLineTotal(i)
        Next
        calculate()
    End Sub
    Public Sub loadfromClinic(ByVal accid As Integer, ByVal refrence As String)
        setCustomer(accid)
    End Sub
    Public Sub loadfromLaundry(ByVal docid As Long, ByVal accid As Integer)
        cmbVoucherTp.SelectedIndex = 1
        setCashCustomer(accid)
        ImportDOs(docid, True)
    End Sub
    Private Function getCustomerDiscount(ByVal itemid As Integer) As Double
        Dim dt As DataTable
        Dim accid As Integer
        If creditCustomerACC > 0 And creditCustomerACC <> Val(txtSuppAlias.Tag) Then
            accid = creditCustomerACC
        Else
            accid = Val(txtSuppAlias.Tag)
        End If
        dt = _objcmnbLayer._fldDatatable("Select discount from CustomerWisePriceDiscountTb where customerid=" & Val(txtSuppAlias.Tag) & " AND Itemid=" & itemid)
        If dt.Rows.Count > 0 Then
            If Not IsDBNull(dt(0)("discount")) Then
                Return CDbl(dt(0)("discount"))
            End If
        End If
        Return 0
    End Function
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

    Private Sub btnlocqty_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnlocqty.Click
        If grdVoucher.CurrentRow Is Nothing Then Exit Sub
        showlocationwise(grdVoucher.CurrentRow.Index)
    End Sub

    Private Sub cmblocation_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmblocation.Click
        If islocationAdded Then
            AddDttoCombo(cmblocation, dtlocationTb, True, False)
            islocationAdded = False
        End If
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

    'Private Sub Worker_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles Worker.DoWork
    '    refreshItemTable("item code", "", False, False, True, "")
    'End Sub

    Private Sub txtsmanP_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtsmanP.TextChanged

    End Sub

    Private Sub txtsmanP_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtsmanP.Validated
        If Val(txtsmanP.Text) > 0 Then
            txtscamt.Text = Format(CDbl(lblNetAmt.Text) * CDbl(txtsmanP.Text) / 100, lnumFormat)
        End If
    End Sub

    Private Sub txtscamt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtscamt.KeyDown, txtsmanP.KeyDown
        If e.KeyCode = Keys.Return Then
            btnupdate.Focus()
        End If
    End Sub
    Private Sub txtscamt_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtscamt.Validated
        If Val(txtscamt.Text) > 0 Then
            txtsmanP.Text = Format(CDbl(txtscamt.Text) * 100 / CDbl(lblNetAmt.Text), lnumFormat)
        End If
    End Sub

    Private Sub grdOtherCost_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdOtherCost.CellContentClick

    End Sub

    Private Sub Panel5_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel5.Paint

    End Sub

    Private Sub frm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles frm.Load

    End Sub

    Private Sub pnlCmn_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlCmn.Paint

    End Sub

    Private Sub cmbtunur_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbtunur.SelectedIndexChanged
        Dim dt As DataTable = _objcmnbLayer._fldDatatable("Select interst  from TunurTb where tunername='" & cmbtunur.Text & "'", False)
        If dt.Rows.Count > 0 Then
            cmbtunur.Tag = Val(dt(0)("interst"))

        End If
        Dim i As Integer
        For i = 0 To grdVoucher.RowCount - 1
            With grdVoucher
                .Item(ConstIGSTP, i).Value = Val(cmbtunur.Tag)
                .Item(ConstTaxP, i).Value = Val(cmbtunur.Tag)
            End With
            calcualteLineTotal(i)
        Next
        calculate()
    End Sub

    Private Sub cmblocation_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmblocation.SelectedIndexChanged

    End Sub
    Private Sub Save_instalment(ByVal trid As Long)
        Dim m As Integer
        Dim y As Integer
        Dim NoOfMnth As Integer
        Dim financeEMIDate As Integer
        Dim emidate As Date
        Dim netamt As Double

        Dim dt As DataTable = _objcmnbLayer._fldDatatable("Select NoOfMnth  from TunurTb where tunername='" & cmbtunur.Text & "'", False)
        If dt.Rows.Count > 0 Then
            NoOfMnth = Val(dt(0)("NoOfMnth"))
         

        End If
        Dim dtSup As Date = DateValue(cldrdate.Value)
        m = Month(dtSup)
        y = Year(dtSup)

        Dim cs As DataTable = _objcmnbLayer._fldDatatable("Select financeEMIDate  from CompanyDefaultSettingsTb ", False)
        If cs.Rows.Count > 0 Then
            financeEMIDate = Val(cs(0)("financeEMIDate") & "")
            If Val(financeEMIDate) > 0 Then
                emidate = DateValue(financeEMIDate & "/" & m & "/" & y)
            End If

        End If
       


        netamt = CDbl(lblNetAmt.Text)
        Dim amount As Double
        amount = netamt / NoOfMnth

        _objcmnbLayer._saveDatawithOutParm("DELETE FROM InstallmentTb WHERE TrId = " & trid)
        For i = 1 To NoOfMnth
            emidate = DateAdd(DateInterval.Month, 1, emidate)

            _objcmnbLayer._saveDatawithOutParm("Insert into InstallmentTb(trid,installmentdate,amount) values('" & _
                                                      trid & "','" & Format(emidate, "yyyy/MM/dd") & "','" & amount & "')")

        Next
       
    End Sub

    Private Sub lblNetAmt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblNetAmt.Click

    End Sub
End Class