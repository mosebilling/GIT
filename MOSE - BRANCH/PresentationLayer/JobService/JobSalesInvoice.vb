
Public Class JobSalesInvoice

#Region "Public Variables"
    Public isModi As Boolean
    Public isCust As Boolean
    Public Event refhreshList()
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
    Private isImport As Boolean
    Private dtTax As DataTable
    Private isgarrageInv As Boolean

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
    Private cessdate As Date
    Private chgUprice As Boolean
    Private chgNumByPgm As Boolean
    Private stoTrId As Long
    Private jobstatus As Integer
    Private tempJobCustid As Integer
#End Region
#Region "Class Objet declarations"
    Private _objcmnbLayer As New clsCommon_BL
    Private _objTr As New clsAccountTransaction
    Private _objInv As New clsInvoice
    Private _objJob As clsJob
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
#End Region
#Region "Const Variables"
    'Private Const ConstSlNo = 0
    'Private Const ConstBarcode = 2
    'Private Const ConstItemCode = 1
    'Private Const ConstDescr = 3
    'Private Const ConstB = 4
    'Private Const ConstUnit = 5
    'Private Const ConstLocation = 6 ' Warrenty
    'Private Const ConstSerialNo = 7
    'Private Const ConstWarrentyExpiry = 8
    'Private Const ConstQty = 9
    'Private Const ConstUPrice = 10
    'Private Const ConstDisP = 11
    'Private Const ConstDisAmt = 12
    'Private Const constItmTot = 13
    'Private Const ConstTaxP = 14
    'Private Const ConstTaxAmt = 15
    'Private Const ConstLTotal = 16
    'Private Const ConstUnitOthCost = 17
    'Private Const ConstNUPrice = 18
    'Private Const ConstActualOthCost = 19
    'Private Const ConstMthd = 12
    'Private Const ConstUnitVal = 21
    'Private Const ConstDiscOther = 22
    'Private Const ConstJob = 23
    'Private Const ConstJobCostAc = 24
    'Private Const ConstBcodeOrICode = 25
    'Private Const ConstImpLnId = 26
    'Private Const ConstImpDocId = 27
    'Private Const ConstActualPrice = 28
    'Private Const ConstJobAcID = 29
    'Private Const ConstPMult = 30
    'Private Const ConstPFraction = 31
    'Private Const ConstItemID = 32
    'Private Const ConstBaseID = 33
    'Private Const ConstLrow = 34
    'Private Const ConstId = 35
    'Private Const ConstqtyChg = 36
    'Private Const ConstCGSTP = 37
    'Private Const ConstCGSTAmt = 38
    'Private Const ConstSGSTP = 39
    'Private Const ConstSGSTAmt = 40
    'Private Const ConstIGSTP = 41
    'Private Const ConstIGSTAmt = 42

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
            InvList = _objcmnbLayer._fldDatatable("SELECT TrId FROM jobInvCmnTb  WHERE  TrId = " & FromTrId)
        Else
            InvList = _objcmnbLayer._fldDatatable("SELECT TrId FROM jobInvCmnTb  WHERE TrType = 'JIS' AND InvNo = " & Val(numVchrNo.Text) & " AND PreFix = '" & MkDbSrchStr(Trim(txtprefix.Text), True) & "'")
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
        Dim ItmInvCmnTb As DataTable
        Dim DocAssgnTb As DataTable
        Dim sRs As DataTable
        'Dim Discount As Double
        Dim UPerPack As Double
        Dim Protect As Boolean
        Dim CrossBr As Boolean
        ItmInvCmnTb = _objcmnbLayer._fldDatatable("SELECT jobInvCmnTb.*,case when isnull(isGarrage,0)=0 then JobTb.jobcode else GarrageTb.jobcode end jobcode,jobname, " & _
                                                  "platenumber,cartype,regyear,isnull(GarrageTb.status,0)status FROM jobInvCmnTb " & _
                                                  "LEFT JOIN JobTb ON jobInvCmnTb.jobid=JobTb.jobid " & _
                                                   "LEFT JOIN GarrageTb ON jobInvCmnTb.jobid=GarrageTb.grid " & _
                                                   "left join CarMasterTb on CarMasterTb.carid=GarrageTb.carid " & _
                                                  "WHERE TrId = " & loadedTrId & " AND TrType = 'JIS'")
        chgbyprg = True
        ActBr = ""
        If ItmInvCmnTb.Rows.Count = 0 Then GoTo Quit
        getProtectUntil()
        jobstatus = 0

        cldrdate.Value = Format(ItmInvCmnTb(0)("TrDate"), DtFormat) ' "MM/dd/yyyy")
        txtprefix.Text = ItmInvCmnTb(0)("PreFix")
        If Not IsDBNull(ItmInvCmnTb(0)("isGarrage")) Then
            isgarrageInv = ItmInvCmnTb(0)("isGarrage")
        End If
        If Trim(ItmInvCmnTb(0)("vhclDetails") & "") = "" Then
            txtvehicle.Text = "Reg.No. :" & Trim(ItmInvCmnTb(0)("platenumber") & "") & vbCrLf
            txtvehicle.Text = txtvehicle.Text & "Vh.Name :" & Trim(ItmInvCmnTb(0)("cartype") & "") & vbCrLf
            txtvehicle.Text = txtvehicle.Text & "Model :" & Trim(ItmInvCmnTb(0)("regyear") & "") & vbCrLf
        Else
            txtvehicle.Text = ItmInvCmnTb(0)("vhclDetails")
        End If
        cmbVoucherTp.Text = getVoucherName(Val(ItmInvCmnTb(0)("InvTypeNo") & ""))
        txtprefix.Tag = ItmInvCmnTb(0)("PreFix")
        txtPPrefix.Text = ItmInvCmnTb(0)("PreFix")
        numVchrNo.Text = ItmInvCmnTb(0)("InvNo") 'Val(Mid(!InvNo, 3)) '!InvNo '
        numVchrNo.Tag = ItmInvCmnTb(0)("InvNo")
        numPrintVchr.Text = ItmInvCmnTb(0)("InvNo")  'CMNREC(12).FormattedText
        txtJob.Text = Trim("" & ItmInvCmnTb(0)("jobcode"))
        txtjobname.Text = Trim(ItmInvCmnTb(0)("jobname") & "")
        If IsDBNull(ItmInvCmnTb(0)("isTaxInv")) Then ItmInvCmnTb(0)("isTaxInv") = 0
        chktaxInv.Checked = IIf(ItmInvCmnTb(0)("isTaxInv") = "True", 1, 0)
        jobstatus = ItmInvCmnTb(0)("status")
        sRs = _objcmnbLayer._fldDatatable("SELECT AccMast.accid,Alias,AccDescr,Address1,Address2,Address3,Address4,Phone,Fax,EMail,PT1,PT2,PT3,PT4,TrdLcno,TrdDate from AccMast LEFT JOIN AccMastAddr ON AccMast.accid=AccMastAddr.AccountNo WHERE AccMast.accid = " & ItmInvCmnTb(0)("custid"))

        If sRs.Rows.Count > 0 Then
            txtSuppAlias.Tag = sRs(0)("accid")
            txtSuppAlias.Text = Trim("" & sRs(0)("Alias"))
            txtSuppName.Text = Trim("" & sRs(0)("AccDescr"))
            'txtcustAddress.Text = Trim(sRs(0)("Address1") & "")
            'txtcustAddress.Text = txtcustAddress.Text & vbCrLf & Trim(sRs(0)("Address2") & "")
            'txtcustAddress.Text = txtcustAddress.Text & vbCrLf & Trim(sRs(0)("Address3") & "")
            'txtcustAddress.Text = txtcustAddress.Text & vbCrLf & Trim(sRs(0)("Address4") & "")
            'txtcustAddress.Text = txtcustAddress.Text & vbCrLf & "Ph: " & Trim(sRs(0)("Phone") & "")
            'txtcustAddress.Text = txtcustAddress.Text & vbCrLf & "Email: " & Trim(sRs(0)("EMail") & "")
        End If
        txtcustAddress.Text = Trim(ItmInvCmnTb(0)("OthrCust") & "")
        If sRs.Rows.Count > 0 Then sRs.Clear()
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

        'txtReference.Text = Trim("" & ItmInvCmnTb(0)("LPO"))
        txtDescr.Text = Trim("" & ItmInvCmnTb(0)("TrDescription"))
        numDisc.Text = Format(ItmInvCmnTb(0)("Discount") / FCRt, numFormat)
        '-----------------------for Other Info ------------------------
        ''If Not fOthInf Is Nothing Then FillOthInf()
        'If Not IsDBNull(ItmInvCmnTb(0)("SuppInvDate")) Then dtSuppDate.CtlText = DateValue(ItmInvCmnTb(0)("SuppInvDate"))
        On Error GoTo 0
        LddImpDocs = ""
        'CTVol = IIf(IsDBNull(ItmInvCmnTb(0)("InvTtlVol")), 0, ItmInvCmnTb(0)("InvTtlVol"))
        'CTWt = IIf(IsDBNull(ItmInvCmnTb(0)("InvTtlWt")), 0, ItmInvCmnTb(0)("InvTtlWt"))
        'CTQty = IIf(IsDBNull(ItmInvCmnTb(0)("InvTtlQty")), 0, ItmInvCmnTb(0)("InvTtlQty"))
        'OthCost = Format(Val(ItmInvCmnTb(0)("OthCost")), numFormat)
        Protect = (ItmInvCmnTb(0)("TrDate") <= ProtectUntil)
        If sRs.Rows.Count > 0 Then sRs.Clear()
        sRs = _objcmnbLayer._fldDatatable("SELECT JOBInvTrTb.*, [Item Code],itemCategory,collectionAC,vat FROM JOBInvTrTb " & _
                                          "LEFT JOIN InvItm ON InvItm.ItemId = JOBInvTrTb.ItemId" & _
                                           " LEFT JOIN VatMasterTb ON InvItm.vatId=VatMasterTb.vatId " & _
                                          "  WHERE TrId = " & loadedTrId & " ORDER BY SlNo")
        grdVoucher.RowCount = 0
        With grdVoucher
            .Rows.Clear()
            If sRs.Rows.Count > 0 Then
                For i = 0 To sRs.Rows.Count - 1
                    grdVoucher.Rows.Add(1)
                    UPerPack = IIf(sRs(i)("PFraction") = 0, 1, sRs(i)("PFraction"))
                    grdVoucher.Item(ConstSlNo, i).Value = IIf(Val(sRs(i)("ItemId")) = 0, "M", "")
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
                    grdVoucher.Item(ConstTaxP, i).Value = Format(sRs(i)("Taxp"), numFormat)
                    grdVoucher.Item(ConstTaxAmt, i).Value = Format(sRs(i)("taxamt") / FCRt, numFormat)
                    grdVoucher.Item(ConstQty, i).Value = Format(sRs(i)("TrQty") / UPerPack, "#,##0" & IIf(Val(grdVoucher.Item(ConstPFraction, i).Value) = 0, "", "." & Strings.StrDup(CByte(grdVoucher.Item(ConstPFraction, i).Value), "0")))
                    grdVoucher.Item(ConstUPrice, i).Value = Format(sRs(i)("UnitCost") * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & NumFormat)
                    grdVoucher.Item(ConstActualPrice, i).Value = sRs(i)("UnitCost") * UPerPack / FCRt
                    grdVoucher.Item(constItmTot, i).Value = Format((sRs(i)("TrQty") * sRs(i)("UnitCost")) * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & NumFormat)
                    grdVoucher.Item(ConstUnitOthCost, i).Value = Format(sRs(i)("UnitOthCost") * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & NumFormat)
                    grdVoucher.Item(ConstActualOthCost, i).Value = sRs(i)("UnitOthCost") * UPerPack / FCRt
                    grdVoucher.Item(ConstDiscOther, i).Value = sRs(i)("UnitDiscount") * UPerPack / FCRt
                    grdVoucher.Item(ConstId, i).Value = sRs(i)("id")

                    grdVoucher.Item(ConstBarcode, i).Value = sRs(i)("HSNCode")
                    grdVoucher.Item(ConstCGSTP, i).Value = sRs(i)("CSGTP")
                    grdVoucher.Item(ConstCGSTAmt, i).Value = sRs(i)("CGSTAMT")

                    grdVoucher.Item(ConstSGSTP, i).Value = sRs(i)("SGSTP")
                    grdVoucher.Item(ConstSGSTAmt, i).Value = sRs(i)("SGSTAmt")

                    grdVoucher.Item(ConstIGSTP, i).Value = sRs(i)("IGSTP")
                    grdVoucher.Item(ConstIGSTAmt, i).Value = sRs(i)("IGSTAmt")
                    grdVoucher.Item(ConstImpDocId, i).Value = sRs(i)("impDocid")
                    grdVoucher.Item(ConstImpLnId, i).Value = sRs(i)("impDocSlno")

                    If Val(sRs(i)("vat") & "") = 0 Then sRs(i)("vat") = 0
                    If Not enableGCC Then
                        grdVoucher.Item(Constcess, i).Value = Format(sRs(i)("vat"), numFormat)
                    Else
                        grdVoucher.Item(Constcess, i).Value = 0
                    End If
                    If Val(sRs(i)("CessAmt") & "") = 0 Then sRs(i)("CessAmt") = 0
                    grdVoucher.Item(ConstcessAmt, i).Value = Format(sRs(i)("CessAmt") / FCRt, numFormat)
                    If Val(sRs(i)("collectionAC") & "") = 0 Then sRs(i)("collectionAC") = 0
                    grdVoucher.Item(ConstcessAc, i).Value = Val(sRs(i)("collectionAC"))

                    If Trim(sRs(i)("itemCategory") & "") = "room" Then
                        .Rows(i).DefaultCellStyle.BackColor = Color.Gray
                        .Rows(i).DefaultCellStyle.ForeColor = Color.White
                    End If
                    If isgarrageInv And Trim(sRs(i)("itemCategory") & "") = "Stock" Then
                        grdVoucher.Item(ConstDonotAllowsaveItem, i).Value = 1
                    End If
                    calcualteLineTotal(i)
                Next
                reArrangeNo()
            End If
        End With
        If sRs.Rows.Count > 0 Then sRs.Clear()
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
        chgItm = False
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
        Dim tNumformat As String
        stoTrId = Trid
        chgbyprg = True
        sRs = _objcmnbLayer._fldDatatable("SELECT ItmInvTrTb.*, [Item Code],Warrentyname,isSerialNo,fcode,FraCount,isnull(itemCategory,'')itemCategory,collectionAC,vat,isTaxInvoice " & _
                                          "FROM ItmInvTrTb LEFT JOIN InvItm ON InvItm.ItemId = ItmInvTrTb.ItemId " & _
                                          " LEFT JOIN InvItmPropertiesTb ON InvItmPropertiesTb.Itemid=invitm.itemid " & _
                                           "LEFT JOIN ItmInvCmnTb on ItmInvCmnTb.trid=ItmInvTrTb.trid " & _
                                          "LEFT JOIN WarrentyMastertb ON WarrentyMastertb.Wid=ItmInvTrTb.WarrentyId " & _
                                          "LEFT JOIN FuelMeterReadingTb ON FuelMeterReadingTb.fmeterid=ItmInvTrTb.FuleMeterid " & _
                                          "LEFT JOIN UnitsTb ON UnitsTb.Units=ItmInvTrTb.Unit " & _
                                          "LEFT JOIN VatMasterTb ON InvItm.vatId=VatMasterTb.vatId " & _
                                          "WHERE ItmInvTrTb.TrId = " & Trid & " and isnull(jobInvTrid,0)=0 ORDER BY SlNo")
        grdVoucher.RowCount = 0
        With grdVoucher
            .Rows.Clear()
            If sRs.Rows.Count > 0 Then
                For i = 0 To sRs.Rows.Count - 1
                    grdVoucher.Rows.Add(1)
                    If i = 0 Then
                        If IsDBNull(sRs(i)("isTaxInvoice")) Then sRs(i)("isTaxInvoice") = 0
                        chktaxInv.Checked = IIf(sRs(i)("isTaxInvoice") = "True", 1, 0)
                    End If
                    If grdVoucher.Item(ConstSlNo, i).Value <> "L" Then
                        UPerPack = 1
                        grdVoucher.Item(ConstItemCode, i).Value = Trim("" & sRs(i)("Item Code"))
                        grdVoucher.Item(ConstItemID, i).Value = sRs(i)("ItemId")
                        grdVoucher.Item(ConstPFraction, i).Value = "2"
                        grdVoucher.Item(ConstPMult, i).Value = UPerPack 'IIf(IsNull(sRs!PFraCount), "2", sRs!PFraCount)
                        grdVoucher.Item(ConstDonotAllowsaveItem, i).Value = 1
                    Else
                        grdVoucher.Item(ConstPFraction, i).Value = "2"
                        grdVoucher.Item(ConstPMult, i).Value = "1"
                        grdVoucher.Item(ConstItemID, i).Value = sRs(i)("LMsgNo")
                    End If
                    grdVoucher.Item(ConstDescr, i).Value = IIf(IsDBNull(sRs(i)("IDescription")), "", sRs(i)("IDescription"))
                    grdVoucher.Item(ConstB, i).Value = "B"
                    grdVoucher.Item(ConstUnit, i).Value = sRs(i)("unit")
                    grdVoucher.Item(ConstLocation, i).Value = Trim("" & sRs(i)("Warrentyname")) ' !Colour
                    grdVoucher.Item(ConstQty, i).Value = Format(sRs(i)("TrQty") / UPerPack, "#,##0" & IIf(Val(grdVoucher.Item(ConstPFraction, i).Value) = 0, "", "." & Strings.StrDup(CByte(grdVoucher.Item(ConstPFraction, i).Value), "0")))
                    grdVoucher.Item(ConstUPrice, i).Value = Format(sRs(i)("UnitCost") * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & numformat)
                    grdVoucher.Item(ConstActualPrice, i).Value = sRs(i)("UnitCost") * UPerPack / FCRt
                    grdVoucher.Item(ConstLTotal, i).Value = Format((sRs(i)("TrQty") * sRs(i)("UnitCost")) * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & numformat)
                    grdVoucher.Item(ConstUnitOthCost, i).Value = Format(sRs(i)("UnitOthCost") * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & numformat)
                    grdVoucher.Item(ConstActualOthCost, i).Value = sRs(i)("UnitOthCost") * UPerPack / FCRt
                    grdVoucher.Item(ConstSerialNo, i).Value = sRs(i)("SerialNo")
                    grdVoucher.Item(ConstDiscOther, i).Value = sRs(i)("UnitDiscount") * UPerPack / FCRt
                    grdVoucher.Item(ConstId, i).Value = 0

                    grdVoucher.Item(ConstBarcode, i).Value = sRs(i)("HSNCode")
                    grdVoucher.Item(ConstCGSTP, i).Value = sRs(i)("CSGTP")
                    grdVoucher.Item(ConstCGSTAmt, i).Value = sRs(i)("CGSTAMT")

                    grdVoucher.Item(ConstSGSTP, i).Value = sRs(i)("SGSTP")
                    grdVoucher.Item(ConstSGSTAmt, i).Value = sRs(i)("SGSTAmt")

                    grdVoucher.Item(ConstIGSTP, i).Value = sRs(i)("IGSTP")
                    grdVoucher.Item(ConstIGSTAmt, i).Value = sRs(i)("IGSTAmt")
                    grdVoucher.Item(ConstImpDocId, i).Value = sRs(i)("impDocid")
                    grdVoucher.Item(ConstImpLnId, i).Value = sRs(i)("impDocSlno")

                    grdVoucher.Item(ConstWarrentyExpiry, i).Value = DateAdd(DateInterval.Year, 1, DateValue(Date.Now))

                    tNumformat = "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))
                    grdVoucher.Item(ConstWoodQty, i).Value = Format(sRs(i)("WoodNetQty") / UPerPack, tNumformat)
                    grdVoucher.Item(ConstWoodDiscQty, i).Value = Format(sRs(i)("WoodDiscountQty") / UPerPack, tNumformat)

                    If Val(sRs(i)("vat") & "") = 0 Then sRs(i)("vat") = 0
                    grdVoucher.Item(Constcess, i).Value = Format(sRs(i)("vat"), numFormat)
                    If Val(sRs(i)("CessAmt") & "") = 0 Then sRs(i)("CessAmt") = 0
                    grdVoucher.Item(ConstcessAmt, i).Value = Format(sRs(i)("CessAmt") / FCRt, numFormat)
                    If Val(sRs(i)("collectionAC") & "") = 0 Then sRs(i)("collectionAC") = 0
                    grdVoucher.Item(ConstcessAc, i).Value = Val(sRs(i)("collectionAC"))

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
        chgItm = False
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
                    cmbVoucherTp.Tag = getvrsId(cmbVoucherTp.Text, 3)
                    getVrsDet(Val(cmbVoucherTp.Tag), "JIS", vrPrefix, vrVoucherNo, vrAccountNo1, vrAccountNo2)
                Else
                    getVrsDet(0, "JIS", vrPrefix, vrVoucherNo, vrAccountNo1, vrAccountNo2)
                End If

            End With
            chgbyprg = True
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
            ElseIf tempJobCustid > 0 Then
                dtAcc = _objcmnbLayer._fldDatatable("SELECT Alias,AccDescr,GrpSetOn,GrpSetOn,accid FROM AccMast " & _
                                               "LEFT JOIN S1AccHd ON AccMast.S1AccId=S1AccHd.S1AccId WHERE accid=" & tempJobCustid)
                If dtAcc.Rows.Count > 0 Then
                    txtSuppName.Text = dtAcc(0)("AccDescr")
                    txtSuppAlias.Text = dtAcc(0)("Alias")
                    txtSuppAlias.Tag = tempJobCustid
                Else
                    GoTo els
                End If
            Else
els:
                txtSuppName.Text = ""
                txtSuppAlias.Text = ""
                txtSuppAlias.Tag = ""
            End If
                txtReference.Focus()
                chgbyprg = False
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
                    chgbyprg = True
                    grdVoucher.BeginEdit(True)
                    chgbyprg = False
                Else
                    AddRow()
                End If
            Else
                SendKeys.Send("{TAB}")
            End If
            If Not fMList Is Nothing Then fMList.Visible = False
        ElseIf e.KeyCode = Keys.F2 Then
            Select Case MyCtrl.Name
                Case "txtSuppAlias", "txtSuppName"
                    _srchTxtId = IIf(MyCtrl.Name = "txtSuppAlias", 1, 2)
                    ldSelect(1)
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
        SetGridHeadEntryProperty(grdVoucher)
        With grdVoucher
            .Columns(ConstDescr).Width = 350
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
        i = grdVoucher.RowCount - 1
        If grdVoucher.RowCount > 0 And Not tocheck Then
            If Val(grdVoucher.Item(ConstItemID, i).Value) = 0 And grdVoucher.Item(ConstItemCode, i).Value = "" And Val(grdVoucher.Item(ConstSlNo, i).Value) <> 0 Then Exit Sub
        End If
        With grdVoucher
            activecontrolname = "grdVoucher"
            i = .RowCount '- 1
            .Rows.Add(1)
            If i > 0 Then
                If Val(.Item(ConstSlNo, i - 1).Value) = 0 Then
                    .Item(ConstSlNo, i).Value = "M"
                Else
                    .Item(ConstSlNo, i).Value = i + 1
                End If
            End If
            .Item(ConstBarcode, i).Value = ""
            .Item(ConstItemCode, i).Value = ""
            .Item(ConstDescr, i).Value = ""
            .Item(ConstUnit, i).Value = ""
            .Item(ConstQty, i).Value = Format(0, numFormat)
            .Item(ConstUPrice, i).Value = Format(0, numFormat)
            .Item(ConstDisP, i).Value = Format(0, numFormat)
            .Item(ConstDisAmt, i).Value = Format(0, numFormat)
            .Item(ConstTaxP, i).Value = Format(0, numFormat)
            .Item(ConstTaxAmt, i).Value = Format(0, numFormat)
            .Item(ConstLTotal, i).Value = Format(0, numFormat) '(.Item(ConstQty, i).Value * DR!unitPrice) - (.Item(ConstQty, i).Value * Val(DR!DiscountVal)) + (.Item(ConstQty, i).Value * Val(DR!TaxVal))
            .Item(ConstNUPrice, i).Value = Format(0, numFormat) 'Val(DR!unitPrice) - Val(DR!DiscountVal) + Val(DR!TaxVal)
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
            reArrangeNo()
            diableColums()
            If Val(.Item(ConstSlNo, i).Value) = 0 Then
                .CurrentCell = .Item(ConstDescr, i)
            Else
                .Columns(ConstItemCode).ReadOnly = False
                .CurrentCell = .Item(ConstItemCode, i)
            End If

            'chgbyprg = True
            '.BeginEdit(True)
            'chgbyprg = False
            grdBeginEdit()
            chgItm = False
        End With
        calculate()

        'ChgByPrg = False

    End Sub
    Private Sub calcualteLineTotal(ByVal RowIndex As Integer)
        chgbyprg = True
        With grdVoucher
            Dim i As Integer
            i = RowIndex
            If Val(.Item(ConstItemID, i).Value) = 0 Then Exit Sub
            '.Item(ConstSlNo, i).Value = i + 1
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
            Dim gstamt As Double
            Dim cessTtl As Double
            If EnableGST Then
                getGSTDetails(Trim(.Item(ConstBarcode, i).Value & ""), i, True)
                If Val(.Item(ConstSGSTAmt, i).Value & "") = 0 Then .Item(ConstSGSTAmt, i).Value = 0
                If Val(.Item(ConstCGSTAmt, i).Value & "") = 0 Then .Item(ConstCGSTAmt, i).Value = 0
                gstamt = CDbl(.Item(ConstCGSTAmt, i).Value) + CDbl(.Item(ConstSGSTAmt, i).Value)
                .Item(ConstTaxAmt, i).Value = Format(gstamt, numFormat)
            ElseIf enableGCC Or ShowTaxOnInventory Then
                Dim actualPrice As Double
                Dim discountOther As Double
                discountOther = CDbl(.Item(ConstDiscOther, i).Value) * CDbl(.Item(ConstQty, i).Value)
                actualPrice = (CDbl(.Item(ConstActualPrice, i).Value) * CDbl(.Item(ConstQty, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value) - discountOther
                actualPrice = Format(actualPrice, numFormat)
                .Item(ConstIGSTAmt, i).Value = ((actualPrice * .Item(ConstIGSTP, i).Value) / 100)
                .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstIGSTAmt, i).Value), numFormat)
            End If
            If enablecess And Val(lblgstn.Tag & "") = 0 And cessdate <= DateValue(cldrdate.Value) And chktaxInv.Checked Then
                cessTtl = (((CDbl(.Item(ConstActualPrice, i).Value) - CDbl(.Item(ConstDiscOther, i).Value)) * .Item(Constcess, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value)
                .Item(ConstcessAmt, i).Value = Format(cessTtl, numFormat)
            End If
            If chktaxInv.Checked = False Then
                .Item(ConstTaxAmt, i).Value = Format(0, numFormat)
                .Item(ConstTaxP, i).Value = Format(0, numFormat)
                .Item(ConstcessAmt, i).Value = 0
            End If

            .Item(constItmTot, i).Value = Format((.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value), numFormat)
            Dim ttl As Double
            ttl = (.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value) + gstamt + cessTtl
            .Item(ConstLTotal, i).Value = Format(ttl, numFormat)
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
            numDisc.Text = Format(0, numFormat)
        End If
        With grdVoucher
            For i = 0 To .Rows.Count - 1
                lnTax = 0
                If (calculateLineTotal And Val(numDisc.Text) > 0) Or chgDiscount Then
                    calcualteLineTotal(i)
                End If
                If Val(.Item(ConstItemID, i).Value) = 0 Then GoTo nxt
                totQty = totQty + IIf(Val(.Item(ConstQty, i).Value) > 0, Val(.Item(ConstQty, i).Value), 0)
                If chktaxInv.Checked = False Then
                    .Item(ConstTaxAmt, i).Value = Format(0, numFormat)
                    .Item(ConstTaxP, i).Value = Format(0, numFormat)
                    .Item(ConstcessAmt, i).Value = 0
                    '.Item(ConstCGSTAmt, i).Value = Format(0, numformat)
                    '.Item(ConstSGSTAmt, i).Value = Format(0, numformat)
                    '.Item(ConstIGSTAmt, i).Value = Format(0, numformat)
                Else
                    .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstIGSTAmt, i).Value), numFormat)
                    .Item(ConstTaxP, i).Value = Format(CDbl(.Item(ConstIGSTP, i).Value), numFormat)
                    If enablecess And Val(lblgstn.Tag & "") = 0 And cessdate <= DateValue(cldrdate.Value) Then
                        lnTax = (((CDbl(.Item(ConstActualPrice, i).Value) - CDbl(.Item(ConstDiscOther, i).Value)) * .Item(Constcess, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value)
                        .Item(ConstcessAmt, i).Value = Format(lnTax, numFormat)
                    End If
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
                totAmt = totAmt + (CDbl(.Item(ConstQty, i).Value) * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value)
                totItm = totItm + (CDbl(.Item(ConstQty, i).Value) * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value)
                If enablecess And Val(lblgstn.Tag & "") = 0 And cessdate <= DateValue(cldrdate.Value) And chktaxInv.Checked Then
                    totCess = totCess + CDbl(.Item(ConstcessAmt, i).Value)
                End If
                lnttl = (CDbl(.Item(ConstQty, i).Value) * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value)
                lnttl = lnttl + lnTax
                chgbyprg = True
                .Item(ConstLTotal, i).Value = Format(lnttl, numFormat)
                chgbyprg = False
                If Val(.Item(ConstTaxAmt, i).Value) > 0 Then
                    totTaxableAmt = totTaxableAmt + ((CDbl(.Item(ConstActualPrice, i).Value) - CDbl(.Item(ConstDiscOther, i).Value)) * CDbl(.Item(ConstQty, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value)
                End If
nxt:
            Next
            calOthCost()

            totAmt = totAmt + totTax + totCess
            lblTotAmt.Text = Format(totItm, numFormat)
            lblNetAmt.Text = Format(totAmt - CDbl(numDisc.Text), numFormat)

            If chkautoroundOff.Checked Then
                If Not blockAutoRoundOff Then
                    chgNumByPgm = True
                    Dim retrnAmt As Double
                    cmbsign.SelectedIndex = getroundoffAMT(lblNetAmt.Text, retrnAmt)
                    txtroundOff.Text = Format(retrnAmt, numFormat)
                    chgNumByPgm = False
                End If
            End If

            If Val(txtroundOff.Text) > 0 Then
                lblNetAmt.Text = Format(CDbl(lblNetAmt.Text) - (IIf(cmbsign.SelectedIndex = 1, 1, -1) * CDbl(txtroundOff.Text)), numFormat)
            End If
            lbltax.Text = Format(totTax, numFormat)
            lblcess.Text = Format(totCess, numFormat)
            totAmt = totAmt - CDbl(numDisc.Text)
            lbltaxable.Text = Format(totTaxableAmt, numFormat)
            chgAmt = False
            chgbyprg = False
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
                    .Item(ConstUnitOthCost, i).Value = Format(Val(.Item(ConstActualOthCost, i).Value), "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ' & NumFormat)
                End If

                If tBDAmt = 0 Then
                    .Item(ConstDiscOther, i).Value = 0
                Else
                    .Item(ConstDiscOther, i).Value = tDAmt * Val(.Item(ConstActualPrice, i).Value) / tBDAmt
                End If
                .Item(ConstNUPrice, i).Value = Format(Val(.Item(ConstActualPrice, i).Value) + Val(.Item(ConstActualOthCost, i).Value) - Val(.Item(ConstDiscOther, i).Value) - Val(.Item(ConstDisAmt, i).Value), "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ' & NumFormat)
                calOthCost = calOthCost + CDbl(.Item(ConstQty, i).Value) * (Val(.Item(ConstActualOthCost, i).Value) - Val(.Item(ConstDiscOther, i).Value) - CDbl(.Item(ConstDisAmt, i).Value))
            Next i
        End With
        If lblTotAmt.Text = "" Then lblTotAmt.Text = Format(0, numFormat)
        If lblNetAmt.Text = "" Then lblNetAmt.Text = Format(0, numFormat)
        If numDisc.Text = "" Then numDisc.Text = Format(0, numFormat)
        lblNetAmt.Text = Format(CDbl(lblTotAmt.Text) - CDbl(numDisc.Text), numFormat)
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
                    If msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up) Or msg.WParam.ToInt32() = CInt(Keys.Enter) Or msg.WParam.ToInt32() = CInt(Keys.F2) Or msg.WParam.ToInt32() = CInt(Keys.F3) Or msg.WParam.ToInt32() = CInt(Keys.F4) Or msg.WParam.ToInt32() = CInt(Keys.F1) Then
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
            ElseIf grdVoucher.Item(ConstDonotAllowsaveItem, grdVoucher.CurrentCell.RowIndex).Value = 1 And e.ColumnIndex <> 3 Then
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
    Private Sub grdBeginEdit()
        chgbyprg = True
        grdVoucher.BeginEdit(True)
        chgbyprg = False
    End Sub

    Private Sub grdVoucher_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellValidated
        Valid(e.RowIndex, e.ColumnIndex)
    End Sub
    Private Sub Valid(ByVal RowIndex As Integer, ByVal ColIndex As Integer)
        With grdVoucher
            Select Case ColIndex
                Case ConstBarcode, ConstItemCode
                    If Not chgItm Then Exit Sub
                    If Trim(.Item(ColIndex, RowIndex).Value) = "" Then Exit Sub
                    Dim dtItms As DataTable
                    Dim found As Boolean
                    If .Item(ColIndex, RowIndex).Value <> "" And SrchText = "" Then SrchText = .Item(ColIndex, RowIndex).Value
                    dtItms = getItmDtls(3, SrchText, True)
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
                Case ConstQty
                    If chgAmt Then
                        If EnableGST Then getGSTDetails(Trim(.Item(ConstBarcode, RowIndex).Value & ""), RowIndex, True)
                    End If
                    calculate(, True)
                Case ConstUPrice
                    If chgAmt Then
                        If Format(.Item(ConstActualPrice, RowIndex).Value, numFormat) <> Format(CDbl(.Item(ConstUPrice, RowIndex).Value), numFormat) Then
                            .Item(ConstActualPrice, RowIndex).Value = CDbl(.Item(ConstUPrice, RowIndex).Value)
                        End If
                        If AllowUnitDiscountEntryOnInventory Then
                            .Item(ConstDisAmt, RowIndex).Value = Format(((CDbl(.Item(ConstActualPrice, RowIndex).Value) * Val(.Item(ConstDisP, RowIndex).Value)) / 100) * CDbl(.Item(ConstQty, RowIndex).Value), numFormat)
                        End If
                        If EnableGST Then getGSTDetails(Trim(.Item(ConstBarcode, RowIndex).Value & ""), RowIndex, True)
                        calculate(, True)
                    End If
                Case ConstDisP
                    If chgAmt Then
                        .Item(ConstDisAmt, RowIndex).Value = Format(((CDbl(.Item(ConstActualPrice, RowIndex).Value) * Val(.Item(ConstDisP, RowIndex).Value)) / 100) * CDbl(.Item(ConstQty, RowIndex).Value), numFormat)
                        If EnableGST Then getGSTDetails(Trim(.Item(ConstBarcode, RowIndex).Value & ""), RowIndex, True)
                        calculate(, True)
                    End If
                Case ConstDisAmt
                    If chgAmt Then
                        Dim unitDiscount As Double
                        unitDiscount = CDbl(.Item(ConstDisAmt, RowIndex).Value) / CDbl(.Item(ConstQty, RowIndex).Value)
                        .Item(ConstDisP, RowIndex).Value = Format((unitDiscount * 100) / CDbl(.Item(ConstActualPrice, RowIndex).Value), numFormat)
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
            '.Item(ConstSlNo, i).Value = i + 1
            .Item(ConstItemCode, i).Value = IIf(IsDBNull(DR(0)("Item Code")), Trim(DR(0)("Item Code") & ""), DR(0)("Item Code"))
            .Item(ConstDescr, i).Value = DR(0)("Description")
            .Item(ConstQty, i).Value = Format(1, numFormat) 'IIf(IsReturn, -1, 1)
            .Item(ConstTaxP, i).Value = Format(Val(DR(0)("vat") & ""), numFormat)
            .Item(ConstItemID, i).Value = DR(0)("ItemId")
            .Item(ConstPMult, i).Value = 1 'IIf(IsNull(sRs(0)("PFraCount), "2", sRs(0)("PFraCount)
            If EnableGST Then
                .Item(ConstBarcode, i).Value = DR(0)("HSNCode") 'hsncode
            Else
                .Item(ConstBarcode, i).Value = ""
            End If
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
                .Item(ConstActualPrice, i).Value = DR(0)("UnitPrice") 'getPurchAmt(DR(0)("CostAvg"), Val(txtSuppAlias.Tag), Val(.Item(ConstItemID, .CurrentCell.RowIndex).Value))
                .Item(ConstUPrice, i).Value = Format(CDbl(.Item(ConstActualPrice, i).Value), "#,###.00")
            End If
            If ShowTaxOnInventory Then
                If Val(DR(0)("vat") & "") = 0 Then
                    DR(0)("vat") = 0
                End If
                .Item(ConstTaxP, i).Value = Format(IIf(IsDBNull(DR(0)("vat")), 0, CDbl(DR(0)("vat"))), numFormat)
                .Item(ConstTaxAmt, i).Value = ((CDbl(.Item(ConstActualPrice, i).Value) * .Item(ConstTaxP, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value)
                .Item(ConstIGSTP, i).Value = IIf(IsDBNull(DR(0)("vat")), 0, CDbl(DR(0)("vat")))
                .Item(ConstcessAc, i).Value = IIf(IsDBNull(DR(0)("collectionAC")), 0, Val(DR(0)("collectionAC")))
            ElseIf EnableGST Then
                getGSTDetails(Trim(.Item(ConstBarcode, i).Value & ""), i, False)
            End If
            .Item(ConstLTotal, i).Value = Format(.Item(ConstQty, i).Value * CDbl(.Item(ConstUPrice, i).Value), numFormat)
            .Item(ConstNUPrice, i).Value = Format(CDbl(.Item(ConstUPrice, i).Value), numFormat)

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
                Case ConstQty, ConstTaxAmt, ConstTaxP
                    '.Item(ConstLTotal, i).Value = Format((.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - (.Item(ConstQty, i).Value * .Item(ConstDisAmt, i).Value) + (.Item(ConstQty, i).Value * .Item(ConstTaxAmt, i).Value), NumFormat)
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
                Case ConstUPrice
                    chgUprice = True
                    chgAmt = True
                    calculateTaxFromUnitPrice(e.RowIndex)
            End Select
        End With
    End Sub
    Private Sub calculateTaxFromUnitPrice(ByVal i As Integer)
        If chgUprice And chkcal.Checked Then
            chgbyprg = True
            With grdVoucher
                .Item(ConstActualPrice, i).Value = (CDbl(.Item(ConstUPrice, i).Value) * 100) / (CDbl(.Item(ConstTaxP, i).Value) + CDbl(.Item(Constcess, i).Value) + 100)
                .Item(ConstUPrice, i).Value = Format(.Item(ConstActualPrice, i).Value, numFormat)
            End With
        End If
        chgUprice = False
        chgbyprg = False
    End Sub
    Private Sub grdVoucher_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdVoucher.EditingControlShowing
        Dim Col As Integer
        Col = grdVoucher.CurrentCell.ColumnIndex
        If Col = ConstItemCode Or Col = ConstQty Then
            Dim tb As TextBox = CType(e.Control, TextBox)
            RemoveHandler tb.TextChanged, AddressOf Textbox_TextChanged
            AddHandler tb.TextChanged, AddressOf Textbox_TextChanged
        End If
        If Col = ConstItemCode Then
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

    Private Sub grdVoucher_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdVoucher.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If grdVoucher.RowCount = 0 Then Exit Sub
                If SrchText = "" And grdVoucher.CurrentCell.ColumnIndex = ConstItemCode And Val(grdVoucher.Item(ConstSlNo, grdVoucher.CurrentCell.RowIndex).Value) > 0 Then
                    GoTo nxt
                End If
                diableColums()

                If FindNextCell(grdVoucher, grdVoucher.CurrentCell.RowIndex, grdVoucher.CurrentCell.ColumnIndex + 1) Then
                    AddRow()
                End If
nxt:
                plsrch.Visible = False
                chgbyprg = True
                grdVoucher.BeginEdit(True)
                chgbyprg = False

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
                With grdVoucher
                    .Item(ConstSlNo, .CurrentCell.RowIndex).Value = IIf(Val(.Item(ConstSlNo, .CurrentCell.RowIndex).Value) = 0, "", "M")
                    reArrangeNo()
                    If Val(.Item(ConstSlNo, .CurrentCell.RowIndex).Value) = 0 Then
                        .CurrentCell = .Item(ConstDescr, .CurrentCell.RowIndex)
                    Else
                        .Columns(ConstItemCode).ReadOnly = False
                        .CurrentCell = .Item(ConstItemCode, .CurrentCell.RowIndex)
                    End If

                    grdBeginEdit()
                End With
            ElseIf e.KeyCode = Keys.F2 Then
                If grdVoucher.RowCount = 0 Then Exit Sub
                Select Case grdVoucher.CurrentCell.ColumnIndex
                    Case ConstItemCode
                        If Not fMList Is Nothing Then fMList.Visible = False
                        fProductEnquiry = New ItmEnqry
                        fProductEnquiry.ShowDialog()
                    Case ConstSerialNo
                        If Not fSerialno Is Nothing Then fSerialno.Close() : fSerialno = Nothing
                        fSerialno = New AddSerialnoFrm
                        fSerialno.txtserialno.Tag = Val(grdVoucher.Item(ConstItemID, grdVoucher.CurrentRow.Index).Value)
                        fSerialno.isSales = True
                        fSerialno.cldrdate.Value = DateAdd(DateInterval.Year, 1, cldrdate.Value)
                        fSerialno.ShowDialog()
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
    End Sub

    Private Sub RemoveRow()
        If grdVoucher.RowCount > 0 Then
            If MsgBox("You Are Going to Remove the Row." & vbCrLf & "Are You Sure ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then
                Exit Sub
            End If
            With grdVoucher
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
        txtcustAddress.Text = ""
        chgDoc = False
        'fMainForm.lblUser.Text = CurrentUser
        'fMainForm.lblModiUser.Text = ""
        OthCost = 0
        numDisc.Text = Format(0, numFormat)
        OthCost = 0
        CTVol = 0
        CTWt = 0
        CTQty = 0
        loadedTrId = 0
        LddImpDocs = ""
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
        numVchrNo.Focus()
        lbladd1.Text = ""
        lbladd2.Text = ""
        lbladd3.Text = ""
        lbladd4.Text = ""
        lbladd5.Text = ""
        lbladd6.Text = ""
        lbladd7.Text = ""
        lbltrdate.Text = ""
        txtvehicle.Text = ""
        tempJobCustid = 0
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
                btnupdate.Tag = IIf(getRight(36, CurrentUser), 1, 0)
                btndelete.Tag = IIf(getRight(37, CurrentUser), 1, 0)
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
            MsgBox("Sales A/C could not found!", MsgBoxStyle.Exclamation)
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

    Private Sub saveTrans()
        Dim TrId As Long
        Dim i As Integer
        Dim DiscAcc As Long
        Dim PPerU As Single
        Dim TDrAmt As Double
        Dim dtTable As DataTable
        clsreader()
        clsCnnection()
        If Not isModi Then
chkagain:
            If Not CheckNoExists(txtprefix.Text, Val(numVchrNo.Text), "JIS", "JBInvoice") Then
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
            dtTable = _objcmnbLayer._fldDatatable("SELECT TOP 1 * FROM JobInvCmnTb  WHERE TrId = " & loadedTrId)
            If dtTable.Rows.Count = 0 Then
                MsgBox("The loaded voucher not found.  It may removed externally.", vbExclamation)
                Exit Sub
            End If
            _objcmnbLayer._saveDatawithOutParm("Update JobInvTrTb set setRemove=1 WHERE trid=" & loadedTrId)
            TrId = loadedTrId
        End If
        setInvCmnValue(TrId)
        TrId = Val(_objInv.saveJobsalesInvoice())
        _objcmnbLayer._saveDatawithOutParm("Update JobInvCmnTb set NetAmt=" & CDbl(lblNetAmt.Text) & " WHERE trid=" & TrId)
        ReDim JobAcc(0)
        JobAcc(0).Acc = Val(txtPurchAlias.Tag)
        JobAcc(0).Job = txtJob.Text
        JobAcc(0).Amt = IIf(DiscAcc = 0, CDbl(lblNetAmt.Text) - CDbl(lbltax.Text), lblTotAmt.Text)
        Dim gjstockInvoice As Boolean
        With grdVoucher
            For i = 0 To .Rows.Count - 1 '- 1
                If CDbl(.Item(ConstQty, i).Value) <> 0 Or .Item(ConstSlNo, i).Value.ToString = "M" Then
                    PPerU = Val(.Item(ConstPMult, i).Value) 'Switch(.TextMatrix(i, 4) = "B", 1, .TextMatrix(i, 4) = "P1", CSng(grdVoucher.TextMatrix(i, 18)), .TextMatrix(i, 4) = "P2", CSng(grdVoucher.TextMatrix(i, 19)))
                    PPerU = IIf(PPerU = 0, 1, PPerU)
                    TDrAmt = TDrAmt + CDbl(.Item(ConstQty, i).Value) * (CDbl(.Item(ConstActualPrice, i).Value) - IIf(DiscAcc = 0, CDbl(.Item(ConstDiscOther, i).Value) + CDbl(.Item(ConstDisAmt, i).Value), 0))
                    setInvDetValue(TrId, PPerU, i)
                    _objInv.savejobInvTrTb()
                    If Val(.Item(ConstDonotAllowsaveItem, i).Value & "") = 1 Then
                        gjstockInvoice = True
                    End If
                End If
            Next
            If isModi Then
                _objcmnbLayer._saveDatawithOutParm("delete from JobInvTrTb where setRemove=1 and trid=" & loadedTrId)
            End If
        End With
        UpdateAccounts(TrId, TDrAmt, DiscAcc)
        If Not isModi Then
            If Not isgarrageInv Then
                _objcmnbLayer._saveDatawithOutParm("Update JobTb set Status=1,JobCloseDate='" & Format(DateValue(cldrdate.Value), "yyyy/MMM/dd") & "' where Jobid=" & Val(txtJob.Tag))
            Else
                '_objcmnbLayer._saveDatawithOutParm("Update GarrageTb set Status=1,closingdate='" & Format(DateValue(cldrdate.Value), "yyyy/MMM/dd") & "' where grid=" & Val(txtJob.Tag))
            End If
            If gjstockInvoice Then
                _objcmnbLayer._saveDatawithOutParm("Update ItmInvTrTb set jobInvTrid=" & TrId & " where isnull(jobInvTrid,0)=0 and trid=" & stoTrId)
            End If

        End If

        If isModi = False Then
            numPrintVchr.Text = numVchrNo.Text
            txtPPrefix.Text = txtprefix.Text
            numVchrNo.Tag = Val(numVchrNo.Text) 'Trim(CMNREC(12).FormattedText)
            txtprefix.Tag = txtprefix.Text
            txtprefix.Text = SetNextVrNo(numVchrNo, 0, "JIS", "TrType = 'JIS' AND InvNo = ", False, True, True, 1)
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
    End Sub
    Private Sub UpdateAccounts(ByVal TrId As Long, ByVal TDrAmt As Double, ByVal DiscAcc As Integer)
        Dim LinkNo As Long
        Dim dtTable As DataTable
        If isModi And TrId > 0 Then
            dtTable = _objcmnbLayer._fldDatatable("SELECT LinkNo FROM AccTrCmn WHERE jbInvid  = " & TrId)
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
        If chktaxInv.Checked Then
            For i = 0 To dtTax.Rows.Count - 1
                If Val(dtTax(i)("ACid")) > 0 And Val(dtTax(i)("Amount")) > 0 Then
                    Dim TxAmount As Double = CDbl(dtTax(i)("Amount"))
                    dlAmt = dlAmt + (TxAmount * FCRt)
                    setAcctrDetValue(LinkNo, Val(dtTax(i)("ACid")), Trim(txtReference.Text), dtTax(i)("AccountName") & " Tax Collected", TxAmount * -1 * FCRt, Strings.Left(txtJob.Text, 50), "", 0, 0, "", "", Val(txtSuppAlias.Tag), Val(txtSuppAlias.Tag) & txtReference.Text, "", FCRt)
                    _objTr.saveAccTrans()
                End If
            Next
        End If
        'If EnableGST Then
        '    For i = 0 To dtTax.Rows.Count - 1
        '        If Val(dtTax(i)("ACid")) > 0 And Val(dtTax(i)("Amount")) > 0 Then
        '            Dim TxAmount As Double = CDbl(dtTax(i)("Amount"))
        '            dlAmt = dlAmt + (TxAmount * FCRt)
        '            setAcctrDetValue(LinkNo, Val(dtTax(i)("ACid")), Trim(txtReference.Text), dtTax(i)("AccountName") & " Tax Collected", TxAmount * -1 * FCRt, Strings.Left(txtJob.Text, 50), "", 0, 0, "", "", Val(txtSuppAlias.Tag), Val(txtSuppAlias.Tag) & txtReference.Text, "", FCRt)
        '            _objTr.saveAccTrans()
        '        End If
        '    Next
        'ElseIf ShowTaxOnInventory Then
        '    For i = 0 To dtTax.Rows.Count - 1
        '        If Val(dtTax(i)("collectionAC")) > 0 And Val(dtTax(i)("Amount")) > 0 Then
        '            Dim TxAmount As Double = CDbl(dtTax(i)("Amount"))
        '            dlAmt = dlAmt + (TxAmount * FCRt)
        '            setAcctrDetValue(LinkNo, Val(dtTax(i)("collectionAC")), Trim(txtReference.Text), dtTax(i)("Vatcode") & " Tax Collected", TxAmount * -1 * FCRt, Strings.Left(txtJob.Text, 50), "", 0, 0, "", "", Val(txtSuppAlias.Tag), Val(txtSuppAlias.Tag) & txtReference.Text, "", FCRt)
        '            _objTr.saveAccTrans()
        '        End If
        '    Next
        'End If
        'Debit Entry
        setAcctrDetValue(LinkNo, Val(txtSuppAlias.Tag), Trim(txtReference.Text), EntRef, dlAmt, txtJob.Text, Strings.Left(txtJob.Text, 50), 0, 0, "", _
                         "", Val(txtSuppAlias.Tag), Val(txtSuppAlias.Tag) & txtReference.Text, "", FCRt)
        _objTr.saveAccTrans()

        'Credit Entry
        For j = 0 To JobAcc.Count - 1
            setAcctrDetValue(LinkNo, j)
            _objTr.saveAccTrans()
        Next

        'DiscountEntry
        If (CDbl(numDisc.Text)) > 0 And DiscAcc > 0 Then
            dlAmt = (CDbl(numDisc.Text)) * FCRt
            setAcctrDetValue(LinkNo, DiscAcc, Trim(txtReference.Text), Trim(txtDescr.Text), dlAmt, "", "", 2, 0, "", _
                            "", Val(txtSuppAlias.Tag), DiscAcc & txtReference.Text, "", FCRt)
            _objTr.saveAccTrans()
        End If
        updateClosingBalanceForInvoice(TrId)
    End Sub

    Private Sub setInvCmnValue(ByVal InvTrid As Long)
        With _objInv
            Dt = DateValue(Date.Now)
            .TrId = InvTrid
            .TrDate = DateValue(cldrdate.Value)
            .TrType = IIf(isgarrageInv, "GJIS", "JIS")
            .Prefix = Trim(txtprefix.Text) ' IIf(Trim(txtprefix.Text) = "", "JIS", Trim(txtprefix.Text))
            .InvNo = Val(numVchrNo.Text)
            .TrRefNo = txtprefix.Text & IIf(txtprefix.Text = "", "JIS", "/") & numVchrNo.Text
            .CSCode = Val(txtSuppAlias.Tag)
            .PSAcc = Val(txtPurchAlias.Tag)
            .JobCode = txtJob.Text
            .UserId = CurrentUser
            .Discount = CDbl(numDisc.Text) * FCRt
            .TrDescription = Trim(txtDescr.Text)
            .MchName = MACHINENAME
            .OthrCust = txtcustAddress.Text
            .TaxType = Val(lblstatecode.Tag)
            .isTaxInvoice = chktaxInv.Checked
            .vhclDetails = txtvehicle.Text
            .InvTypeNo = Val(cmbVoucherTp.Tag)
        End With

    End Sub
    Private Sub setInvDetValue(ByVal Invid As Long, ByVal PPerU As Single, ByVal i As Integer)
        With grdVoucher

            _objInv.dtTrId = Invid
            _objInv.ItemId = IIf(.Item(ConstSlNo, i).Value.ToString <> "L", Val(.Item(ConstItemID, i).Value), 0)
            _objInv.Unit = .Item(ConstUnit, i).Value
            _objInv.TrQty = CDbl(.Item(ConstQty, i).Value) * PPerU
            _objInv.UnitCost = CDbl(.Item(ConstActualPrice, i).Value) * FCRt / PPerU 'CDbl(.TextMatrix(i, 10)) / PPerU
            _objInv.taxP = CDbl(grdVoucher.Item(ConstTaxP, i).Value)
            _objInv.taxAmt = (CDbl(grdVoucher.Item(ConstTaxAmt, i).Value) * Me.FCRt) / CDbl(PPerU)
            _objInv.PFraction = PPerU
            If Val(.Item(ConstUnitOthCost, i).Value) = 0 Then .Item(ConstUnitOthCost, i).Value = 0
            _objInv.UnitOthCost = CDbl(.Item(ConstUnitOthCost, i).Value) * FCRt / PPerU

            _objInv.Method = .Item(ConstB, i).Value
            _objInv.UnitDiscount = Val(.Item(ConstDiscOther, i).Value) * FCRt / PPerU
            If Not IsDBNull(.Item(ConstDescr, i).Value) Then
                _objInv.IDescription = IIf(.Item(ConstSlNo, i).Value.ToString = "M", .Item(ConstDescr, i).Value, Trim(.Item(ConstDescr, i).Value))
            End If
            _objInv.SlNo = i + 1
            _objInv.TrTypeNo = getVouchernumber("JIS")
            _objInv.TrDateNo = getDateNo(CDate(cldrdate.Value))
            _objInv.TrType = "JIS"
            _objInv.id = Val(.Item(ConstId, i).Value)
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
            _objInv.impDocid = Val(.Item(ConstImpDocId, i).Value)
            _objInv.impDocSlno = Val(.Item(ConstImpLnId, i).Value & "")
            If Val(.Item(ConstcessAmt, i).Value & "") = 0 Then .Item(ConstcessAmt, i).Value = 0
            _objInv.CessAmt = (CDbl(grdVoucher.Item(ConstcessAmt, i).Value) * FCRt) / CDbl(PPerU)
        End With
    End Sub
    Private Sub setAcctrCmnValue(ByVal InvTrid As Long, ByVal LinkNo As Long)
        _objTr.JVType = "JIS"
        _objTr.JVDate = DateValue(cldrdate.Value)
        _objTr.PreFix = Trim(txtprefix.Text)
        _objTr.JVNum = Val(numVchrNo.Text)
        _objTr.JVTypeNo = getVouchernumber("JIS")
        _objTr.UserId = Trim(CurrentUser & "")
        _objTr.MchName = MACHINENAME
        _objTr.CrtDtTm = Dt
        _objTr.TypeNo = getVouchernumber("JIS")
        '_objTr.VrDescr = Trim(txtpayee.Text)
        _objTr.IsModi = IIf(LinkNo > 0, 3, 0)
        _objTr.LinkNo = InvTrid
        _objTr.taxablevalue = CDbl(lbltaxable.Text)
        _objTr.taxvalue = CDbl(lbltax.Text)
    End Sub
    Private Sub setAcctrDetValue(ByVal lnkNo As Long, ByVal jbIndex As Integer)
        With _objTr
            .trLinkNo = lnkNo
            .AccountNo = JobAcc(jbIndex).Acc
            .Reference = Trim(txtprefix.Text) & IIf(txtprefix.Text = "", "JIS", "/") & numVchrNo.Text
            .EntryRef = Strings.Left(Trim(txtDescr.Text) & IIf(JobAcc(jbIndex).Job = "", "", " / " & JobAcc(jbIndex).Job) & " / " & txtSuppName.Text, 249)
            .DealAmt = -1 * JobAcc(jbIndex).Amt * FCRt
            .FCAmt = -1 * JobAcc(jbIndex).Amt
            .JobCode = JobAcc(jbIndex).Job
            .JobStr = ""
            .CurrRate = FCRt
            .CurrencyCode = ""
            'txtJob.Tag = txtJob.Text & IIf(txtJob.Text = "" Or JobAcc(jbIndex).Job = "", "", ",") & JobAcc(jbIndex).Job
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
        With _objTr
            .trLinkNo = lnkNo
            .AccountNo = AccountNo
            .Reference = Trim(txtprefix.Text) & IIf(txtprefix.Text = "", "JIS", "/") & numVchrNo.Text
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
            Dim dtDue As Date = IIf(chkDate(cldrdate.Value), cldrdate.Value, DateValue(cldrdate.Value))
            Dim dtSup As Date = DateValue(cldrdate.Value)
            .DocDate = dtLPO
            .SuppInvDate = dtSup
            .DueDate = dtDue
        End With
    End Sub

    Private Sub JobSalesInvoice_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtReference.Focus()
    End Sub

    Private Sub JobSalesInvoice_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RaiseEvent refhreshList()
    End Sub

    Private Sub SalesOrderFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            StrAccMastSrch = "SELECT AccDescr, Alias, ClosingBal As Balance, OpnBal, AccountNo, S1AccHd.S1AccId, AccSetId, GrpSetOn,accid FROM" & _
                            " AccMast INNER JOIN S1AccHd ON S1AccHd.S1AccId = AccMast.S1AccId "
            'btnNext_Click(btnNext, New System.EventArgs())
            'dtTax = _objcmnbLayer._fldDatatable("SELECT vatcode,convert(money, 0) Amount,collectionAC,paymentAC,vat From VatMasterTb", False)
            If ShowTaxOnInventory Then
                CreateTaxTable(dtTax)
                chktaxInv.Checked = True
                If withNonTaxBill Then
                    chktaxInv.Checked = False
                End If
            ElseIf EnableGST Then
                CreateTaxTable(dtTax)
                chktaxInv.Checked = True
                If withNonTaxBill Then
                    chktaxInv.Checked = False
                End If
            End If
            SetGridHead()

            FCRt = 1
            OthCost = 0
            chgbyprg = True
            calculate()
            Me.Text = "Job Sales Invoice"
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
                    btnupdate.Tag = IIf(getRight(87, CurrentUser), 1, 0)
                    btndelete.Tag = IIf(getRight(88, CurrentUser), 1, 0)
                Else
                    btnupdate.Tag = 1
                    btndelete.Tag = 1
                End If

                btndelete.Text = "Delete"
            Else
                AddNewClick()
                If userType Then
                    btnupdate.Tag = IIf(getRight(86, CurrentUser), 1, 0)
                Else
                    btnupdate.Tag = 1
                End If
                btndelete.Text = "Clear"
                btndelete.Tag = 1

            End If
            crtSubVrs(cmbVoucherTp, 3, True)
            Timer1.Enabled = True
            cessdate = getCessDate()
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
        dtTable = _objcmnbLayer._fldDatatable("SELECT * FROM ItmInvCmnTb WHERE TrType = 'IS' AND InvNo = " & Val(numVchrNo.Text) & " AND PreFix = '" & MkDbSrchStr(Trim(txtprefix.Text), True) & "'")
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
            Dim dtSrlNo As DataTable
            Dim dtExstSrlno As DataTable
            Dim itemidStr As String
            Dim r As Integer
            itemidStr = ""
            For r = 0 To grdVoucher.RowCount - 1
                itemidStr = itemidStr & IIf(itemidStr = "", "", ",") & Val(.Item(ConstItemID, r).Value)
            Next
            dtSrlNo = _objcmnbLayer._fldDatatable("SELECT SerialNo,itemid FROM ItmInvTrTb Left Join ItmInvCmnTb on ItmInvTrTb.Trid=ItmInvCmnTb.trid WHERE TrType='IS' AND ItmInvCmnTb.Trid<>" & loadedTrId)
            dtExstSrlno = _objcmnbLayer._fldDatatable("SELECT SerialNo,ItemId FROM serialnotb ")
            Dim _qurey As EnumerableRowCollection(Of DataRow)

            For r = 0 To .RowCount - 1 '- 1
                If .Item(ConstSerialNo, r).Value <> "" Then
                    _qurey = From data In dtExstSrlno.AsEnumerable() Where data(0) = .Item(ConstSerialNo, r).Value And data(1) = .Item(ConstItemID, r).Value Select data
                    If _qurey.Count = 0 And enableSerialnumber Then
                        .Rows(r).Selected = True
                        .CurrentCell = .Item(ConstSerialNo, r)
                        MsgBox("Serial Number not found!", vbExclamation)
                        .FirstDisplayedScrollingRowIndex = r
                        .BeginEdit(True)
                        GoTo Ter
                    Else
                        _qurey = Nothing
                    End If
                    _qurey = Nothing
                    _qurey = From data In dtSrlNo.AsEnumerable() Where data(0) = .Item(ConstSerialNo, r).Value Select data
                    If _qurey.Count > 0 And enableSerialnumber Then
                        .Rows(r).Selected = True
                        .CurrentCell = .Item(ConstSerialNo, r)
                        MsgBox("Serial Number already Sold !!", vbExclamation)
                        .FirstDisplayedScrollingRowIndex = r
                        .BeginEdit(True)
                        GoTo Ter
                    End If
                End If
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
        grdVoucher.BeginEdit(True)
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


    Private Sub numDisc_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles numDisc.KeyPress
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
        chgbyprg = False
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
        grdVoucher.BeginEdit(True)
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
    Private Sub setCustomer(Optional ByVal accid As Long = 0, Optional ByVal donotsetCustomernameFromJob As Boolean = False)
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
            If Not donotsetCustomernameFromJob Then
                txtSuppAlias.Tag = dt(0)("accid")
                If accid > 0 Then
                    txtSuppAlias.Text = Trim("" & dt(0)("Alias"))
                    txtSuppName.Text = Trim("" & dt(0)("AccDescr"))
                End If
            Else
                txtDescr.Text = Trim("" & dt(0)("AccDescr"))
            End If
           
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
            If Trim(dt(0)("Phone") & "") <> "" Then
                txtcustAddress.Text = txtcustAddress.Text & vbCrLf & "Ph: " & Trim(dt(0)("Phone") & "")
            End If
            If Trim(dt(0)("GSTIN") & "") <> "" Then
                txtcustAddress.Text = txtcustAddress.Text & vbCrLf & "GSTIN: " & Trim(dt(0)("GSTIN") & "")
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
            If (enablecess And Val(lblgstn.Tag & "") = 0 And cessdate <= DateValue(cldrdate.Value)) Then
addtax:
                Dim dt As DataTable = _objcmnbLayer._fldDatatable("SELECT  vatcode,convert(money, 0) Amount,collectionAC,vat,AccDescr From VatMasterTb Left join accmast on VatMasterTb.collectionAC=AccMast.accid", False)
                For i = 0 To dt.Rows.Count - 1
                    dtrow = dtTax.NewRow
                    dtrow("slno") = dtTax.Rows.Count + 1
                    dtrow("AccountName") = dt(0)("AccDescr")
                    dtrow("ACid") = dt(0)("collectionAC")
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
                    If enablecess Then
                        Dim b = (From data In dtTax.AsEnumerable() Where data("ACid") = Val(.Item(ConstcessAc, i).Value & "") Select data("slno"))
                        slno = 0
                        For Each itm In b
                            slno = itm
                        Next
                        If slno > 0 Then
                            dtTax(slno - 1)("Amount") = CDbl(dtTax(slno - 1)("Amount")) + CDbl(.Item(ConstcessAmt, i).Value)
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
    Private Sub getGSTDetails(ByVal hsncode As String, ByVal i As Integer, ByVal calculatefromGrid As Boolean)
        Dim dt As DataTable
        With grdVoucher
            If Not calculatefromGrid Then
                dt = _objcmnbLayer._fldDatatable("SELECT * FROM GSTTb WHERE HSNCODE='" & hsncode & "'")
                If dt.Rows.Count > 0 Then
                    .Item(ConstCGSTP, i).Value = Format(IIf(IsDBNull(dt(0)("CGST")), 0, CDbl(dt(0)("CGST"))), numFormat)
                    .Item(ConstSGSTP, i).Value = Format(IIf(IsDBNull(dt(0)("SGST")), 0, CDbl(dt(0)("SGST"))), numFormat)
                    .Item(ConstIGSTP, i).Value = Format(IIf(IsDBNull(dt(0)("IGST")), 0, CDbl(dt(0)("IGST"))), numFormat)
                Else
                    .Item(ConstCGSTP, i).Value = Format(0, numFormat)
                    .Item(ConstSGSTP, i).Value = Format(0, numFormat)
                    .Item(ConstIGSTP, i).Value = Format(0, numFormat)
                End If
            End If
            Dim actualPrice As Double
            Dim discountOther As Double
            discountOther = CDbl(.Item(ConstDiscOther, i).Value) * CDbl(.Item(ConstQty, i).Value)
            actualPrice = (CDbl(.Item(ConstActualPrice, i).Value) * CDbl(.Item(ConstQty, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value) - discountOther
            'actualPrice = (CDbl(.Item(ConstActualPrice, i).Value) * CDbl(.Item(ConstQty, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value) - discountOther
            actualPrice = Format(actualPrice, numFormat)
            .Item(ConstCGSTAmt, i).Value = (actualPrice * .Item(ConstCGSTP, i).Value) / 100
            .Item(ConstSGSTAmt, i).Value = (actualPrice * .Item(ConstSGSTP, i).Value) / 100
            .Item(ConstIGSTAmt, i).Value = (actualPrice * .Item(ConstIGSTP, i).Value) / 100
            If Val(lblstatecode.Tag) = 1 Then
                .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstIGSTAmt, i).Value), numFormat)
                .Item(ConstTaxP, i).Value = .Item(ConstIGSTP, i).Value
            Else
                .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstCGSTAmt, i).Value) + CDbl(.Item(ConstSGSTAmt, i).Value), numFormat)
                .Item(ConstTaxP, i).Value = CDbl(.Item(ConstCGSTP, i).Value) + CDbl(.Item(ConstSGSTP, i).Value)
            End If
        End With
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
            .strType = "JIS"
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
            fRptFormat.RptType = "JIS"
            fRptFormat.ShowDialog()
            fRptFormat = Nothing
        Else
            PrepareRpt("JIS")
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

            Dim linkno As Long
            Dim dt As DataTable = _objcmnbLayer._fldDatatable("SELECT LinkNo FROM AccTrCmn WHERE jbInvid = " & loadedTrId, False)
            If (dt.Rows.Count > 0) Then
                linkno = dt(0)("LinkNo")
                dt = _objcmnbLayer._fldDatatable("SELECT Accountno FROM AccTrDet WHERE LinkNo=" & linkno, False)
                _objcmnbLayer._saveDatawithOutParm("DELETE FROM AccTrDet WHERE LinkNo=" & linkno)
            End If
            _objcmnbLayer._saveDatawithOutParm("DELETE FROM AccTrCmn WHERE LinkNo=" & linkno)
            _objcmnbLayer._saveDatawithOutParm("DELETE FROM JobInvCmnTb WHERE trid=" & loadedTrId)
            _objcmnbLayer._saveDatawithOutParm("DELETE FROM JobInvTrTb WHERE trid=" & loadedTrId)
            _objcmnbLayer._saveDatawithOutParm("update ItmInvTrTb set  jobInvTrid=0 WHERE jobInvTrid=" & loadedTrId)
            Dim i As Integer = 0
            For i = 0 To dt.Rows.Count - 1
                _objcmnbLayer.updateClosingBalance(dt(i)("Accountno"))
            Next
            Try
                _objcmnbLayer.updatetoLog("JOB SALES", "D", DateTime.Now, "JIS-" & txtprefix.Text & numVchrNo.Text)
            Catch exception1 As Exception
               MsgBox("Cannot update log details! Please contact vendor", MsgBoxStyle.Exclamation, Nothing)

            End Try

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
        If jobstatus = 2 Then
            MsgBox("You cannot modify delivered job invoice", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
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
        _objInv.Prefix = ""
        _objInv.InvNo = Val(numPrintVchr.Text)
        _objInv.TrType = "JIS"
        Dim ds As DataSet = _objInv.ldInvoice("rturnJobInvoiceDetailsForInvoicePrint")
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
        AddRow()
        'grdVoucher.CurrentCell = grdVoucher.Item(ConstItemCode, grdVoucher.CurrentRow.Index)
        doCommandStat(True)
        chgPost = True
        reArrangeNo()
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
    Private Sub diableColums()
        Dim i As Integer
        For i = 1 To grdVoucher.Columns.Count - 1
            With grdVoucher
                If .Columns(i).Visible = True Then
                    If Val(.Item(ConstSlNo, .CurrentCell.RowIndex).Value) = 0 And i <> 3 Then
                        .Columns(i).ReadOnly = True
                    ElseIf Val(.Item(ConstDonotAllowsaveItem, .CurrentCell.RowIndex).Value) = 1 And i <> 3 Then
                        .Columns(i).ReadOnly = True
                    Else
                        If i <> ConstSlNo And i <> constItmTot And i <> ConstUnit And i <> ConstLTotal And i <> ConstBarcode And i <> ConstTaxAmt And i <> ConstStartReading And i <> ConstcessAmt Then
                            .Columns(i).ReadOnly = False
                        End If
                    End If
                End If
            End With
        Next
    End Sub

    Private Sub grdVoucher_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellDoubleClick
        Dim dt As DataTable = Nothing
        With grdVoucher
            If e.ColumnIndex = ConstSlNo Then
                .Item(ConstSlNo, .CurrentCell.RowIndex).Value = IIf(Val(.Item(ConstSlNo, .CurrentCell.RowIndex).Value) = 0, "", "M")
                reArrangeNo()
                'diableColums()
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
                    .Item(ConstUPrice, .CurrentCell.RowIndex).Value = Format(cost, numFormat)
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
        If col = ConstQty Or col = ConstUPrice Or col = ConstDisAmt Or col = ConstLTotal Then
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
                Else
                    grdVoucher.Rows.Add(1)
                    For j = 0 To grdVoucher.ColumnCount - 1
                        grdVoucher.Item(j, grdVoucher.Rows.Count - 1).Value = grdVoucher.Item(j, lstRow).Value
                    Next
                    grdVoucher.Item(ConstSerialNo, grdVoucher.Rows.Count - 1).Value = .Item(0, i).Value
                    grdVoucher.Item(ConstWarrentyExpiry, grdVoucher.Rows.Count - 1).Value = .Item(1, i).Value
                    grdVoucher.Item(ConstId, grdVoucher.Rows.Count - 1).Value = 0
                End If
            Next
            grdVoucher.CurrentCell = grdVoucher.Item(ConstUPrice, grdVoucher.Rows.Count - 1)
            'chgbyprg = True
            'grdVoucher.BeginEdit(True)
            'chgbyprg = False
        End With
        chgbyprg = False
        chgPost = True
        'MsgBox(grdVoucher.Item(ConstItemCode, grdVoucher.RowCount - 1).Value)
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


    Private Sub grdSrch_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSrch.CellContentClick

    End Sub

    Private Sub cmbVoucherTp_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVoucherTp.SelectedIndexChanged
        If chgbyprg Then Exit Sub
        NextNumber()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        resizeGridColumn(grdVoucher, ConstDescr)
    End Sub

    Private Sub SalesInvoice_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Timer1.Enabled = True
    End Sub
    Public Sub returnFromJob(ByVal jbid As Long, ByVal serviceAmt As Double, ByVal isOnlyService As Boolean, ByVal trid As Long, Optional ByVal isGarrage As Boolean = False)
        If Not isGarrage Then
            _objJob = New clsJob
            _objJob.Jobid = jbid
            _objJob.DateFrom = DateValue(Date.Now)
            _objJob.DateTo = DateValue(Date.Now)
            _objJob.Tp = 0
            Dim dsJob As DataSet = _objJob.returnJob
            If (dsJob.Tables(0).Rows.Count > 0) Then
                chgbyprg = True
                txtJob.Text = dsJob.Tables(0)(0)("jobcode")
                txtJob.Tag = Val(dsJob.Tables(0)(0)("Jobid"))
                txtjobname.Text = dsJob.Tables(0)(0)("jobname")
                txtSuppAlias.Text = dsJob.Tables(0)(0)("Alias")
                txtSuppName.Text = dsJob.Tables(0)(0)("AccDescr")
                txtSuppName.Tag = dsJob.Tables(0)(0)("custcode")
                setCustomer()
                chgbyprg = False
            End If
        Else
            Dim dt As DataTable
            dt = _objcmnbLayer._fldDatatable("select GarrageTb.*,SManCode,SManName,cartype,platenumber,regyear from GarrageTb " & _
                                             "left join SalesmanTb on SalesmanTb.salesmanid=GarrageTb.technician " & _
                                             "left join CarMasterTb on CarMasterTb.carid=GarrageTb.carid " & _
                                             "where grid=" & jbid)
            chgbyprg = True
            txtJob.Text = dt(0)("jobcode")
            txtJob.Tag = jbid
            txtvehicle.Text = "Reg.No. :" & dt(0)("platenumber") & vbCrLf
            txtvehicle.Text = txtvehicle.Text & "Name :" & dt(0)("cartype") & vbCrLf
            txtvehicle.Text = txtvehicle.Text & "Model :" & dt(0)("regyear") & vbCrLf
            txtvehicle.Text = txtvehicle.Text & "Last KM :" & dt(0)("kilometer")
            setCustomer(Val(dt(0)("customerid")), True)
            chgbyprg = False
            tempJobCustid = Val(dt(0)("customerid"))
        End If
        isgarrageInv = isGarrage
        SetGridHead()
        grdVoucher.Rows.Clear()
        If Not isOnlyService Then
            PasteFrom(trid)
        End If
createNewItem:
        If (serviceAmt > 0) Then
            Dim dR As DataTable = getItmDtls(3, "JOB", True)
            If (dR.Rows.Count > 0) Then
                grdVoucher.Rows.Add(1)
                grdVoucher.CurrentCell = grdVoucher.Item(0, (grdVoucher.RowCount - 1))
                AddDetails(dR)
                grdVoucher.Item(ConstQty, (grdVoucher.RowCount - 1)).Value = Strings.Format(1, numFormat)
                grdVoucher.Item(ConstUPrice, (grdVoucher.RowCount - 1)).Value = Strings.Format(serviceAmt, numFormat)
                grdVoucher.Item(ConstActualPrice, (grdVoucher.RowCount - 1)).Value = serviceAmt
                'grdVoucher.Item(constItmTot, (grdVoucher.RowCount - 1)).Value = serviceAmt
                calcualteLineTotal(grdVoucher.RowCount - 1)
            Else
                createInstantItem("JOB", "SERVICE ITEM", "", 0, "Service")
                GoTo createNewItem
            End If
        End If
        reArrangeNo()
        calculate()
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

        lblhead.Text = "Invoice"
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
        strsql = "SELECT [item code],INVITM.HSNCode,Description,Unit,UnitPrice,ISNULL(CGST,0)CGST,isnull(SGST,0)SGST,isnull(IGST,0)IGST,ItemId," & _
        "datediff(d,checkinDateTime,checkoutDateTime) qty,ldgroomid,checkinDateTime,checkoutDateTime FROM INVITM " & _
        "LEFT JOIN LodgeRoomTb ON LodgeRoomTb.roomItemid=INVITM.Itemid " & _
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
                i += 1
                .Rows(num).DefaultCellStyle.BackColor = Color.Gray
                .Rows(num).DefaultCellStyle.ForeColor = Color.White
            Loop
            If isIncludeService Then
                returnLodgeServices(roomid, isserviceAmt, True)
            End If
        End With

        reArrangeNo()
        calculate(False)
    End Sub
    

    Private Sub btntax_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btntax.Click
        ShowTax.grdVoucher.DataSource = dtTax
        ShowTax.ShowDialog()
    End Sub

    Private Sub txtcustAddress_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcustAddress.TextChanged
        chgPost = True
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
                .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstIGSTAmt, i).Value), numFormat)
                .Item(ConstTaxP, i).Value = .Item(ConstIGSTP, i).Value
            Else
                .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstCGSTAmt, i).Value) + CDbl(.Item(ConstSGSTAmt, i).Value), numFormat)
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

    Private Sub btnAddgst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddgst.Click
        setGstToGrdFromTabC()
    End Sub

    Private Sub txtCgst_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCgst.KeyDown, txtSgst.KeyDown, txtIgst.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub btncancelgst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancelgst.Click
        tbgst.Visible = False
    End Sub

    Private Sub cmbsign_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbsign.SelectedIndexChanged
        calculate()
    End Sub

    Private Sub chktaxInv_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chktaxInv.Click
        calculate()
    End Sub

    Private Sub txtroundOff_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtroundOff.KeyPress
        NumericTextOnKeypress(sender, e, chgNumByPgm, numFormat)
    End Sub

    Private Sub txtroundOff_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtroundOff.TextChanged
        calculate()
    End Sub

    Private Sub txtroundOff_Validated1(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtroundOff.Validated
        calculate()
    End Sub

    Private Sub chktaxInv_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chktaxInv.CheckedChanged

    End Sub

    Private Sub numDisc_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numDisc.TextChanged

    End Sub

    Private Sub grdVoucher_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellEnter

    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        PrepareRpt("JIS", True)
    End Sub
End Class