
Public Class LaundryOrderFrm

#Region "Public Variables"
    Public isModi As Boolean
    Public isCust As Boolean
    Public islaundry As Boolean
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
    Private isGarrage As Boolean = False



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
    Private diableNegativeSale As Boolean
    Private cessdate As Date
    Private exitFromValidProc As Boolean
#End Region
#Region "Class Objet declarations"
    Private _objcmnbLayer As New clsCommon_BL
    Private _objTr As New clsAccountTransaction
    Private _objInv As New clsInvoice
    Private _objDoc As clsDocCmn
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
    Private WithEvents fImportdcos As ViewImportedDocsFrm
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
            InvList = _objcmnbLayer._fldDatatable("SELECT Docid FROM DocCmnTb  WHERE  Docid = " & FromTrId)
        Else
            InvList = _objcmnbLayer._fldDatatable("SELECT Docid FROM DocCmnTb  WHERE Doctype = 'SO' AND DNO = " & Val(numVchrNo.Text) & _
                                                  " AND isnull(Prefix,'')='" & txtprefix.Text & "'")
        End If
        If InvList.Rows.Count > 0 Then
            loadedTrId = InvList(0)("Docid")
            InvList = Nothing
            ldPostedInv()
            isModi = True
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
        Dim sRs As DataTable
        'Dim Discount As Double
        Dim UPerPack As Double
        Dim Protect As Boolean
        Dim CrossBr As Boolean
        ItmInvCmnTb = _objcmnbLayer._fldDatatable("SELECT DocCmnTb.*,jobname,fname,AccDescr,alias,cartype,platenumber FROM DocCmnTb " & _
                                                  "LEFT JOIN JobTb ON DocCmnTb.jobcode=JobTb.jobcode " & _
                                                  "LEFT JOIN garragetb ON DocCmnTb.jobcode=garragetb.jobcode " & _
                                                  "LEFT JOIN CarMasterTb ON CarMasterTb.carid=garragetb.carid " & _
                                                  "LEFT JOIN AccMast ON DocCmnTb.CSCode=AccMast.Accid " & _
                                                  "LEFT JOIN (select jobcode fcode,jobname fname FROM JobTb) jbfrom ON DocCmnTb.FromJob=jbfrom.fcode " & _
                                                  "WHERE DocId = " & loadedTrId & " AND DocType = 'SO'")
        chgbyprg = True
        If ItmInvCmnTb.Rows.Count = 0 Then GoTo Quit
        getProtectUntil()

        cldrdate.Value = Format(ItmInvCmnTb(0)("DDate"), DtFormat) ' "MM/dd/yyyy")
        numVchrNo.Text = ItmInvCmnTb(0)("DNo") 'Val(Mid(!InvNo, 3)) '!InvNo '
        numVchrNo.Tag = ItmInvCmnTb(0)("DNo")
        txtprefix.Text = Trim(ItmInvCmnTb(0)("Prefix") & "")
        txtPPrefix.Text = Trim(ItmInvCmnTb(0)("Prefix") & "")
        numPrintVchr.Text = ItmInvCmnTb(0)("DNo")  'CMNREC(12).FormattedText
        txtSuppName.Text = Trim(ItmInvCmnTb(0)("AccDescr") & "")
        txtSuppAlias.Text = Trim(ItmInvCmnTb(0)("alias") & "")
        txtSuppName.Tag = ItmInvCmnTb(0)("CSCode")
        txtJob.Text = Trim("" & ItmInvCmnTb(0)("jobcode"))
        txtAttn.Text = Trim("" & ItmInvCmnTb(0)("Attn"))
        txtsubject.Text = Trim("" & ItmInvCmnTb(0)("Subject"))
        txtDOLst.Text = Trim("" & ItmInvCmnTb(0)("DocLstTxt"))
        numDisc.Text = Val(ItmInvCmnTb(0)("Discount") & "")
        numDisc.Text = Format(CDbl(numDisc.Text), numFormat)
        txtDescr.Text = Trim("" & ItmInvCmnTb(0)("Comment"))
        If Not IsDBNull(ItmInvCmnTb(0)("isGarrage")) Then
            isGarrage = ItmInvCmnTb(0)("isGarrage")
        End If
        If isGarrage Then
            txtjobname.Text = ItmInvCmnTb(0)("platenumber") & " / " & ItmInvCmnTb(0)("cartype")
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

        If IsDBNull(ItmInvCmnTb(0)("withTax")) Then ItmInvCmnTb(0)("withTax") = 0
        chktaxInv.Checked = IIf(ItmInvCmnTb(0)("withTax") = "True", 1, 0)
        Protect = (ItmInvCmnTb(0)("DDate") <= ProtectUntil)
        sRs = _objcmnbLayer._fldDatatable("SELECT DocTranTb.*, [Item Code],vat,foundImport,rgcess,additionalcess FROM DocTranTb " & _
                                          "LEFT JOIN InvItm ON InvItm.ItemId = DocTranTb.ItemId  " & _
                                          "LEFT JOIN VatMasterTb ON InvItm.vatId=VatMasterTb.vatId " & _
                                          "LEFT JOIN (SELECT foundImport FROM (SELECT impDocSlno foundImport FROM ItmInvTrTb " & _
                                          "UNION ALL SELECT ImpDocLnNo FROM DocTranTb)tr  GROUP BY foundImport) As PIQ ON PIQ.foundImport = DocTranTb.id " & _
                                          "LEFT JOIN (select vatcode rgccode,vat rgcess, collectionAC rgcaccount,vatid rgcid from  VatMasterTb)regularcess ON InvItm.regularcessid=regularcess.rgcid " & _
                                          "WHERE Docid = " & loadedTrId & " ORDER BY SlNo")
        grdVoucher.RowCount = 0
        chgbyprg = True
        Dim importdocid As Long
        btnimport.Visible = False
        btndelete.Enabled = True
        With grdVoucher
            .Rows.Clear()
            If sRs.Rows.Count > 0 Then
                For i = 0 To sRs.Rows.Count - 1
                    grdVoucher.Rows.Add(1)
                    chgbyprg = True
                    UPerPack = IIf(sRs(i)("PFraction") = 0, 1, sRs(i)("PFraction"))
                    grdVoucher.Item(ConstSlNo, i).Value = IIf(Val(sRs(i)("ItemId")) = 0, "M", "")
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
                        grdVoucher.Item(ConstItemID, i).Value = 0
                    End If
                    grdVoucher.Item(ConstDescr, i).Value = IIf(IsDBNull(sRs(i)("Trdetail")), "", sRs(i)("Trdetail"))
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


                    grdVoucher.Item(ConstQty, i).Value = Format(sRs(i)("Qty") / UPerPack, "#,##0" & IIf(Val(grdVoucher.Item(ConstPFraction, i).Value) = 0, "", "." & Strings.StrDup(CByte(grdVoucher.Item(ConstPFraction, i).Value), "0")))
                    grdVoucher.Item(ConstUPrice, i).Value = Format(sRs(i)("CostPUnit") * UPerPack, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & NumFormat)
                    grdVoucher.Item(ConstActualPrice, i).Value = sRs(i)("CostPUnit") * UPerPack
                    grdVoucher.Item(constItmTot, i).Value = Format((sRs(i)("Qty") * sRs(i)("CostPUnit")) * UPerPack, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & NumFormat)
                    grdVoucher.Item(ConstUnitOthCost, i).Value = 0
                    grdVoucher.Item(ConstActualOthCost, i).Value = 0
                    If Val(sRs(i)("UnitDiscount") & "") = 0 Then sRs(i)("UnitDiscount") = 0
                    grdVoucher.Item(ConstDiscOther, i).Value = Format(sRs(i)("UnitDiscount") * UPerPack / FCRt, numFormat)
                    grdVoucher.Item(ConstId, i).Value = sRs(i)("id")

                    If Val(sRs(i)("DisP") & "") = 0 Then sRs(i)("DisP") = 0
                    grdVoucher.Item(ConstDisP, i).Value = Format(sRs(i)("DisP"), numFormat)
                    If Val(sRs(i)("ItemDiscount") & "") = 0 Then sRs(i)("ItemDiscount") = 0
                    grdVoucher.Item(ConstDisAmt, i).Value = Format(sRs(i)("ItemDiscount") * UPerPack / FCRt, lnumFormat)
                    grdVoucher.Item(ConstImpDocId, i).Value = sRs(i)("impDocid")
                    grdVoucher.Item(ConstImpLnId, i).Value = sRs(i)("ImpDocLnNo")
                    importdocid = sRs(i)("impDocid")
                    grdVoucher.Item(ConstBarcode, i).Value = sRs(i)("HSNCode")
                    grdVoucher.Item(ConstCGSTP, i).Value = sRs(i)("CSGTP")
                    grdVoucher.Item(ConstCGSTAmt, i).Value = sRs(i)("CGSTAMT") / FCRt
                    grdVoucher.Item(ConstSGSTP, i).Value = sRs(i)("SGSTP")
                    grdVoucher.Item(ConstSGSTAmt, i).Value = sRs(i)("SGSTAmt") / FCRt
                    grdVoucher.Item(ConstIGSTP, i).Value = sRs(i)("IGSTP")
                    grdVoucher.Item(ConstIGSTAmt, i).Value = sRs(i)("IGSTAmt") / FCRt
                    If Val(sRs(i)("foundImport") & "") > 0 Then
                        grdVoucher.Item(ConstDonotAllowsaveItem, i).Value = 1
                        btnimport.Visible = True
                        btndelete.Enabled = False
                    End If
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
        calculate()
        reArrangeNo()
        If Protect Then
            MsgBox("Voucher comes under Protected Range.  You can't Post modifications.", vbInformation)
        ElseIf CrossBr Then
            MsgBox("Found multi-branches or branches other than you loged.  Can't Post modifications.", vbInformation)
        Else
            'btnUpdate.Enabled = (Val(btnUpdate.Tag) > 0)
            'btnRemoveRec.Enabled = (Val(btnRemoveRec.Tag) > 0)
        End If
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
        Dim tNumformat As String
        sRs = _objcmnbLayer._fldDatatable("SELECT ItmInvTrTb.*, [Item Code],Warrentyname,collectionAC,vat FROM ItmInvTrTb LEFT JOIN InvItm ON InvItm.ItemId = ItmInvTrTb.ItemId " & _
                                          "LEFT JOIN WarrentyMastertb ON WarrentyMastertb.Wid=ItmInvTrTb.WarrentyId " & _
                                          "LEFT JOIN VatMasterTb ON InvItm.vatId=VatMasterTb.vatId " & _
                                          "WHERE TrId = " & Trid & " ORDER BY SlNo")
        grdVoucher.RowCount = 0
        With grdVoucher
            .Rows.Clear()
            If sRs.Rows.Count > 0 Then
                For i = 0 To sRs.Rows.Count - 1
                    grdVoucher.Rows.Add(1)
                    If grdVoucher.Item(ConstSlNo, i).Value <> "L" Then
                        UPerPack = 1
                        grdVoucher.Item(ConstItemCode, i).Value = Trim("" & sRs(i)("Item Code"))
                        grdVoucher.Item(ConstItemID, i).Value = sRs(i)("ItemId")
                        grdVoucher.Item(ConstPFraction, i).Value = "2"
                        grdVoucher.Item(ConstPMult, i).Value = UPerPack 'IIf(IsNull(sRs!PFraCount), "2", sRs!PFraCount)
                    Else
                        grdVoucher.Item(ConstPFraction, i).Value = "2"
                        grdVoucher.Item(ConstPMult, i).Value = "1"
                        grdVoucher.Item(ConstItemID, i).Value = sRs(i)("LMsgNo")
                    End If
                    grdVoucher.Item(ConstDescr, i).Value = IIf(IsDBNull(sRs(i)("remarks")), "", sRs(i)("remarks"))
                    grdVoucher.Item(ConstB, i).Value = "B"
                    grdVoucher.Item(ConstLocation, i).Value = Trim("" & sRs(i)("Warrentyname")) ' !Colour
                    grdVoucher.Item(ConstQty, i).Value = Format(sRs(i)("TrQty") / UPerPack, "#,##0" & IIf(Val(grdVoucher.Item(ConstPFraction, i).Value) = 0, "", "." & Strings.StrDup(CByte(grdVoucher.Item(ConstPFraction, i).Value), "0")))
                    grdVoucher.Item(ConstUPrice, i).Value = Format(sRs(i)("UnitCost") * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & lnumFormat)
                    grdVoucher.Item(ConstActualPrice, i).Value = sRs(i)("UnitCost") * UPerPack / FCRt
                    grdVoucher.Item(ConstLTotal, i).Value = Format((sRs(i)("TrQty") * sRs(i)("UnitCost")) * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & lnumFormat)
                    grdVoucher.Item(ConstUnitOthCost, i).Value = Format(sRs(i)("UnitOthCost") * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & lnumFormat)
                    grdVoucher.Item(ConstActualOthCost, i).Value = sRs(i)("UnitOthCost") * UPerPack / FCRt
                    grdVoucher.Item(ConstSerialNo, i).Value = sRs(i)("SerialNo")
                    grdVoucher.Item(ConstDiscOther, i).Value = sRs(i)("UnitDiscount") * UPerPack / FCRt
                    grdVoucher.Item(ConstId, i).Value = 0
                    grdVoucher.Item(ConstWarrentyExpiry, i).Value = DateAdd(DateInterval.Year, 1, DateValue(Date.Now))

                    tNumformat = "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))
                    grdVoucher.Item(ConstWoodQty, i).Value = Format(sRs(i)("WoodNetQty") / UPerPack, tNumformat)
                    grdVoucher.Item(ConstWoodDiscQty, i).Value = Format(sRs(i)("WoodDiscountQty") / UPerPack, tNumformat)

                    If Val(sRs(i)("vat") & "") = 0 Then sRs(i)("vat") = 0
                    grdVoucher.Item(Constcess, i).Value = Format(sRs(i)("vat"), lnumFormat)
                    If Val(sRs(i)("CessAmt") & "") = 0 Then sRs(i)("CessAmt") = 0
                    grdVoucher.Item(ConstcessAmt, i).Value = Format(sRs(i)("CessAmt") / FCRt, lnumFormat)
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
        Dim vrPrefix As String = ""
        Dim vrVoucherNo As Long
        Dim vrAccountNo1 As Long
        Dim vrAccountNo2 As Long
        getVrsDet(0, "SO", vrPrefix, vrVoucherNo, vrAccountNo1, vrAccountNo2)
        If Not isModi Then
            numVchrNo.Text = vrVoucherNo
            txtprefix.Text = vrPrefix
        End If
    End Sub

    Private Sub txtReference_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSuppName.KeyDown, txtSuppAlias.KeyDown, txtReference.KeyDown, txtjobname.KeyDown, txtJob.KeyDown, txtAttn.KeyDown, txtsubject.KeyDown
        lstKey = e.KeyCode
        Dim MyCtrl As TextBox = sender
        If e.KeyCode = Keys.Return Then

            If MyCtrl.Name = "txtSuppName" Then
                If grdVoucher.Rows.Count > 0 Then
                    activecontrolname = "grdVoucher"
                    grdVoucher.CurrentCell = grdVoucher.Item(1, grdVoucher.CurrentRow.Index)
                    grdBeginEdit()
                Else
                    AddRow()
                End If
                'ElseIf MyCtrl.Name = "txtCashCustomer" Then
                '    txtDescr.Focus()
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
            .Columns(Constsman).Visible = False
            .Columns(ConstMeterCode).Visible = False
            .Columns(ConstStartReading).Visible = False
            .Columns(ConstEndReading).Visible = False
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
            .Columns(ConstMRP).Visible = enableMRPinDocument
            .Columns(ConstMRP).ReadOnly = True
            .Columns(ConstDisP).Visible = False
            .Columns(ConstDisAmt).Visible = False
            .Columns(constItmTot).Visible = enableTaxinDocument
            .Columns(ConstTaxP).Visible = enableTaxinDocument
            .Columns(ConstTaxAmt).Visible = enableTaxinDocument
            .Columns(ConstcessAmt).Visible = False

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

            reArrangeNo()
            diableColums()
            If Not grdVoucher.CurrentRow Is Nothing Then
                If Val(grdVoucher.Item(ConstItemID, grdVoucher.CurrentRow.Index).Value) > 0 Then chgItm = False
            End If
            If Val(.Item(ConstSlNo, i).Value) = 0 Then
                .CurrentCell = .Item(ConstDescr, i)
            Else
                .Columns(ConstItemCode).ReadOnly = False
                .CurrentCell = .Item(ConstItemCode, i)
            End If
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
            If EnableGST Then
                getGSTDetails(Trim(.Item(ConstBarcode, i).Value & ""), i, True)
                If Val(.Item(ConstSGSTAmt, i).Value & "") = 0 Then .Item(ConstSGSTAmt, i).Value = 0
                If Val(.Item(ConstCGSTAmt, i).Value & "") = 0 Then .Item(ConstCGSTAmt, i).Value = 0
                .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstCGSTAmt, i).Value) + CDbl(.Item(ConstSGSTAmt, i).Value), lnumFormat)
            ElseIf enableGCC Or ShowTaxOnInventory Then
                Dim actualPrice As Double
                Dim discountOther As Double
                discountOther = CDbl(.Item(ConstDiscOther, i).Value) * CDbl(.Item(ConstQty, i).Value)
                actualPrice = (CDbl(.Item(ConstActualPrice, i).Value) * CDbl(.Item(ConstQty, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value) - discountOther
                actualPrice = Format(actualPrice, lnumFormat)
                .Item(ConstIGSTAmt, i).Value = ((actualPrice * .Item(ConstIGSTP, i).Value) / 100)
                .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstIGSTAmt, i).Value), lnumFormat)
            End If
            If enablecess And Val(lblgstn.Tag & "") = 0 And cessdate <= DateValue(cldrdate.Value) And chktaxInv.Checked Then
                .Item(ConstcessAmt, i).Value = Format((((CDbl(.Item(ConstActualPrice, i).Value) - CDbl(.Item(ConstDiscOther, i).Value)) * .Item(Constcess, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value), lnumFormat)
            End If
            If chktaxInv.Checked = False Then
                .Item(ConstTaxAmt, i).Value = Format(0, lnumFormat)
                .Item(ConstTaxP, i).Value = Format(0, lnumFormat)
                .Item(ConstcessAmt, i).Value = 0
            End If

            .Item(constItmTot, i).Value = Format((.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value), lnumFormat)
            .Item(ConstLTotal, i).Value = Format((.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value) + CDbl(.Item(ConstTaxAmt, i).Value), lnumFormat)
            .Item(ConstNUPrice, i).Value = CDbl(.Item(ConstActualPrice, i).Value) - (CDbl(.Item(ConstDisAmt, i).Value) / CDbl(.Item(ConstQty, i).Value)) + (CDbl(.Item(ConstTaxAmt, i).Value) / CDbl(.Item(ConstQty, i).Value)) + (CDbl(.Item(ConstcessAmt, i).Value) / CDbl(.Item(ConstQty, i).Value)) - Val(.Item(ConstDiscOther, i).Value)
        End With

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
        Dim ttlprofit As Double
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
        Dim profit As Double
        With grdVoucher
            For i = 0 To .Rows.Count - 1
                lnTax = 0
                discountOther = 0
                actualPrice = 0
                profit = 0
                profit = CDbl(.Item(ConstActualPrice, i).Value) - CDbl(.Item(ConstBatchCost, i).Value)
                .Item(ConstLineProfit, i).Value = Format(profit * CDbl(.Item(ConstQty, i).Value), lnumFormat)
                ttlprofit = ttlprofit + CDbl(.Item(ConstLineProfit, i).Value)
                discountOther = CDbl(.Item(ConstDiscOther, i).Value)
                actualPrice = CDbl(.Item(ConstActualPrice, i).Value) - discountOther - (CDbl(.Item(ConstDisAmt, i).Value) / CDbl(.Item(ConstQty, i).Value))
                If (calculateLineTotal And Val(numDisc.Text) > 0) Or chgDiscount Then
                    calcualteLineTotal(i)
                End If
                If Val(.Item(ConstItemID, i).Value) = 0 Then GoTo nxt
                totQty = totQty + IIf(CDbl(.Item(ConstQty, i).Value) > 0, CDbl(.Item(ConstQty, i).Value), 0)
                If chktaxInv.Checked = False Then
                    .Item(ConstTaxAmt, i).Value = Format(0, lnumFormat)
                    .Item(ConstTaxP, i).Value = Format(0, lnumFormat)
                    .Item(ConstcessAmt, i).Value = 0
                Else
                    .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstIGSTAmt, i).Value), lnumFormat)
                    .Item(ConstTaxP, i).Value = Format(CDbl(.Item(ConstIGSTP, i).Value), lnumFormat)
                    If enablecess Then
                        lnTax = CDbl(.Item(ConstregularCessAmt, i).Value)
                        'lnTax = ((actualPrice * .Item(Constcess, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value)
                        'lnTax = lnTax + (CDbl(.Item(ConstAdditionalcess, i).Value) * CDbl(.Item(ConstQty, i).Value))
                    End If
                    If enableFloodCess And Val(lblgstn.Tag & "") = 0 And cessdate <= DateValue(cldrdate.Value) Then
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
                totAmt = totAmt + (CDbl(.Item(ConstQty, i).Value) * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value)
                totItm = totItm + (CDbl(.Item(ConstQty, i).Value) * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value)
                If (enablecess Or enableFloodCess) And chktaxInv.Checked Then
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
            lblNetAmt.Text = Format(totAmt - CDbl(numDisc.Text), lnumFormat)

            If chkautoroundOff.Checked Then
                If Not blockAutoRoundOff Then
                    chgNumByPgm = True
                    chgbyprg = True
                    Dim retrnAmt As Double
                    cmbsign.SelectedIndex = getroundoffAMT(lblNetAmt.Text, retrnAmt)
                    txtroundOff.Text = Format(retrnAmt, lnumFormat)
                    chgNumByPgm = False
                    chgbyprg = True
                End If
            End If

            If Val(txtroundOff.Text) > 0 Then
                lblNetAmt.Text = Format(CDbl(lblNetAmt.Text) - (IIf(cmbsign.SelectedIndex = 1, 1, -1) * CDbl(txtroundOff.Text)), lnumFormat)
            End If
            lbltax.Text = Format(totTax, lnumFormat)
            lblcess.Text = Format(totCess, lnumFormat)
            totAmt = totAmt - CDbl(numDisc.Text)
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
                    .Item(ConstDiscOther, i).Value = (tDAmt * actualPrice) / tBDAmt
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
                    If msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up) Or msg.WParam.ToInt32() = CInt(Keys.Enter) Or msg.WParam.ToInt32() = CInt(Keys.F2) Or msg.WParam.ToInt32() = CInt(Keys.F3) Or msg.WParam.ToInt32() = CInt(Keys.F4) Or msg.WParam.ToInt32() = CInt(Keys.F6) Or msg.WParam.ToInt32() = CInt(Keys.F1) Then
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
            If Val(.Item(ConstDonotAllowsaveItem, .CurrentRow.Index).Value) = 1 Then
                If e.ColumnIndex < 3 Then
                    grdVoucher.CurrentCell.ReadOnly = True
                    GoTo ext
                End If
            End If
            If Val(grdVoucher.Item(0, grdVoucher.CurrentCell.RowIndex).Value) = 0 And e.ColumnIndex <> 3 Then
                grdVoucher.CurrentCell.ReadOnly = True
            ElseIf e.ColumnIndex <> ConstSlNo And e.ColumnIndex <> constItmTot And e.ColumnIndex <> ConstUnit And e.ColumnIndex <> ConstLTotal And e.ColumnIndex <> ConstBarcode And e.ColumnIndex <> ConstTaxAmt And e.ColumnIndex <> ConstStartReading And e.ColumnIndex <> ConstMRP Then
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
ext:
        chgbyprg = False
    End Sub

    Private Sub grdVoucher_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellValidated
        Try
            Valid(e.RowIndex, e.ColumnIndex)
            SrchText = ""
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try

    End Sub
    Private Sub Valid(ByVal RowIndex As Integer, ByVal ColIndex As Integer)
        If exitFromValidProc Then Exit Sub
        With grdVoucher
            Select Case ColIndex
                Case ConstBarcode, ConstItemCode
                    If Not chgItm Then Exit Sub
                    If Trim(.Item(ColIndex, RowIndex).Value) = "" And SrchText <> "" Then .Item(ColIndex, RowIndex).Value = SrchText
                    If Trim(.Item(ColIndex, RowIndex).Value) <> "" And SrchText = "" Then SrchText = .Item(ColIndex, RowIndex).Value
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
                Case Else
            End Select
        End With
        chgAmt = False
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
                ElseIf chksecondprice.Checked Then
                    .Item(ConstActualPrice, i).Value = DR(0)("secondPrice")
                Else
                    .Item(ConstActualPrice, i).Value = DR(0)("UnitPrice")
                End If
                .Item(ConstUPrice, i).Value = Format(CDbl(.Item(ConstActualPrice, i).Value), lnumFormat)
            End If

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
            If enablecess And Val(lblgstn.Tag & "") = 0 And cessdate <= DateValue(cldrdate.Value) Then
                .Item(Constcess, i).Value = Format(IIf(IsDBNull(DR(0)("vat")), 0, CDbl(DR(0)("vat"))), lnumFormat)
                .Item(ConstcessAc, i).Value = IIf(IsDBNull(DR(0)("collectionAC")), 0, Val(DR(0)("collectionAC")))
                .Item(ConstcessAmt, i).Value = Format(((CDbl(.Item(ConstActualPrice, i).Value) * .Item(Constcess, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value), lnumFormat)
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
        calculate(, True)
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
            actualPrice = (CDbl(.Item(ConstActualPrice, i).Value) * CDbl(.Item(ConstQty, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value) - discountOther
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
                plsrch.Width = 450
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
            SearchProductPanel(grdSrch, "[Item Code]", strGridSrchString, True)
            If grdSrch.RowCount > 0 And strGridSrchString = "" Then
                strGridSrchString = grdSrch.Item(0, 0).Value
            End If
        End If
        doSelect(2)
        _srchOnce = True
        chgbyprg = False
    End Sub

    Private Sub grdVoucher_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.GotFocus
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
    Private Sub grdVoucher_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdVoucher.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If grdVoucher.RowCount = 0 Then Exit Sub

                If Trim(SrchText) = "" And grdVoucher.CurrentCell.ColumnIndex = ConstItemCode And Val(grdVoucher.Item(ConstSlNo, grdVoucher.CurrentCell.RowIndex).Value) > 0 Then
                    SrchText = grdVoucher.Item(ConstItemCode, grdVoucher.CurrentRow.Index).Value
                    If SrchText = "" Then
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
                diableColums()
                If FindNextCell(grdVoucher, grdVoucher.CurrentCell.RowIndex, grdVoucher.CurrentCell.ColumnIndex + 1) Then
                    AddRow()
                End If
                plsrch.Visible = False
nxt:
                chgbyprg = True
                grdBeginEdit()
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
                If plsrch.Visible Then plsrch.Visible = False
                If grdVoucher.RowCount = 0 Then Exit Sub
                Select Case grdVoucher.CurrentCell.ColumnIndex
                    Case ConstItemCode
                        If Not fMList Is Nothing Then fMList.Visible = False
                        If plsrch.Visible Then plsrch.Visible = False
                        fProductEnquiry = New ItmEnqry
                        fProductEnquiry.ShowDialog()

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
                    grdVoucher.Item(ConstSerialNo, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(2) IsNot Nothing, ItmFlds(2), "")
                    'grdVoucher.Item(ConstBarcode, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(1) IsNot Nothing, ItmFlds(1), "")
                    grdVoucher.Item(ConstManufacturingdate, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(3) IsNot Nothing, ItmFlds(3), "")
                    grdVoucher.Item(ConstWarrentyExpiry, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(4) IsNot Nothing, ItmFlds(4), "")
                    grdVoucher.Item(ConstBatchQty, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(2) IsNot Nothing, ItmFlds(2), "")
                    SrchText = IIf(ItmFlds(2) IsNot Nothing, ItmFlds(2), strGridSrchString)
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
            If Not fImportdcos Is Nothing Then
                If fImportdcos.Visible Then
                    loadImpDocs(e.RowIndex)
                End If
            End If
        End If

    End Sub

    Private Sub RemoveRow()
        If grdVoucher.RowCount > 0 Then
            If grdVoucher.CurrentRow Is Nothing Then Exit Sub
            If grdVoucher.Item(ConstDonotAllowsaveItem, grdVoucher.CurrentRow.Index).Value = 1 Then
                MsgBox("Selected product attached with other document! You cannot remove", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
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
        'txtDescr.Text = ""
        txtSuppAlias.Text = ""
        txtSuppAlias.Tag = ""
        txtSuppName.Text = ""
        txtcustAddress.Text = ""
        txtJob.Text = ""
        txtJob.Tag = ""
        chgDoc = False
        'fMainForm.lblUser.Text = CurrentUser
        'fMainForm.lblModiUser.Text = ""
        OthCost = 0
        chgNumByPgm = True
        numDisc.Text = Format(0, lnumFormat)
        txtdp.Text = Format(0, lnumFormat)
        chgNumByPgm = True
        cmbfc.Text = ""
        txtfcrt.Text = Format(0, lnumFormat)
        txtroundOff.Text = Format(0, lnumFormat)
        cmbsalesman.Text = ""
        OthCost = 0
        CTVol = 0
        CTWt = 0
        CTQty = 0
        loadedTrId = 0
        LddImpDocs = ""
        txtDescr.Text = ""
        If dtItemInfo.Rows.Count > 0 Then dtItemInfo.Rows.Clear()
        'setImport(True)
        calculate()
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
        txtDOLst.Text = ""
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
        If enableGCC Then
            lblgstn.Text = "TIN: "
        Else
            lblgstn.Text = "GSTN:"
        End If
        lblgstn.Tag = 0
        btnupdate.Enabled = True
        btndelete.Enabled = True
        cldrdate.Value = Format(Date.Now, DtFormat)

        chgNumByPgm = False
        'add_Click()
        'grdVoucher.Select()
        On Error GoTo 0
    End Sub
    Private Sub enableCtrls(ByVal Eyn As Boolean)
        grdVoucher.ReadOnly = Eyn
        chgbyprg = True
        txtReference.ReadOnly = Eyn
        'txtDescr.ReadOnly = Eyn
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
            isModi = True
            ClearControls()
            numVchrNo.ReadOnly = False
            numVchrNo.Focus()
            btnNext.Visible = False
            btnSlct.Visible = True
            btnModify.Text = "&Undo"
            btndelete.Text = "Delete"
            If userType Then
                btnupdate.Tag = IIf(getRight(209, CurrentUser), 1, 0)
                btndelete.Tag = IIf(getRight(210, CurrentUser), 1, 0)
            Else
                btnupdate.Tag = 1
                btndelete.Tag = 1
            End If

        Else
            AddNewClick()
            NextNumber()
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
        'CalculateGST(True)
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
        If DateValue(cldrdate.Value) <= ProtectUntil Then
            MsgBox("Voucher Date comes within Protected Date Range !!", MsgBoxStyle.Information)
            cldrdate.Focus()
            Exit Sub
        End If
        _vAcMaster = _objcmnbLayer._fldDatatable(StrAccMastSrch & " WHERE Alias = '" & txtSuppAlias.Text & "' ORDER BY AccDescr")
        If _vAcMaster.Rows.Count = 0 Then
            MsgBox("Enter a valid  Customer Account !!", vbExclamation)
            txtSuppName.Focus()
            Exit Sub
        Else
            txtSuppAlias.Tag = _vAcMaster(0)("accid")
        End If
        'If Not isModi Then
        '    If chekDuplicate() Then Exit Sub
        'End If
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


    Private Sub saveTrans()
        Dim docid As Long
        If Not isModi Then
chkagain:
            If Not CheckNoExists(txtprefix.Text, Val(numVchrNo.Text), "SO", "Document") Then
                If MsgBox("Document No alreary exist! Do you want to fill next?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Exit Sub
                Else
                    numVchrNo.Text = Val(numVchrNo.Text) + 1
                    GoTo chkagain
                End If
            End If
        Else
            _objcmnbLayer._saveDatawithOutParm("Update DocTranTb set setRemove=1 WHERE DocId=" & loadedTrId)
        End If
        setCmnValue()
        docid = _objDoc._saveCmn()
        Dim i As Integer
        Dim cnt As Integer = grdVoucher.RowCount - 1
        Dim PPerU As Integer
        For i = 0 To cnt
            With grdVoucher
                If Val(.Item(ConstItemID, i).Value) > 0 Or Val(.Item(ConstSlNo, i).Value) = 0 Then
                    PPerU = Val(.Item(ConstPMult, i).Value)
                    setDetValue(docid, PPerU, i)
                    _objDoc._saveDetails()
                End If

            End With
        Next
        If isModi Then
            _objcmnbLayer._saveDatawithOutParm("delete from DocTranTb where setRemove=1 and DocId=" & loadedTrId)
        End If
        numVchrNo.Tag = numVchrNo.Text
        If Not isModi Then
            numPrintVchr.Text = Trim(numVchrNo.Text)
            txtPPrefix.Text = txtprefix.Text
            numVchrNo.Tag = numVchrNo.Text
            txtprefix.Text = SetNextVrNo(numVchrNo, 0, "SO", "DocType = 'SO' AND DNO = ", False, False, True, 0, txtprefix.Text)
        End If
        ChgId = False
        chgPost = False
        AddNewClick()
        MsgBox("Sales Order is succesfully posted with SO. # " & numPrintVchr.Text & ".", MsgBoxStyle.Information)
        numVchrNo.Tag = ""
    End Sub
    Private Sub setDetValue(ByVal Invid As Long, ByVal PPerU As Single, ByVal i As Integer)
        With grdVoucher
            _objDoc = New clsDocCmn
            _objDoc.DocMId = Invid
            _objDoc.ItemId = IIf(.Item(ConstSlNo, i).Value.ToString <> "L", Val(.Item(ConstItemID, i).Value), 0)
            _objDoc.Unit = .Item(ConstUnit, i).Value
            _objDoc.Qty = CDbl(.Item(ConstQty, i).Value) * PPerU
            _objDoc.CostPUnit = CDbl(.Item(ConstActualPrice, i).Value) / PPerU
            _objDoc.PFraction = PPerU
            _objDoc.Mthd = .Item(ConstB, i).Value
            If Not IsDBNull(.Item(ConstDescr, i).Value) Then
                _objDoc.TrDetail = IIf(.Item(ConstSlNo, i).Value.ToString = "M", .Item(ConstDescr, i).Value, Trim(.Item(ConstDescr, i).Value))
            End If
            _objDoc.SlNo = i + 1
            _objDoc.id = Val(.Item(ConstId, i).Value)

            _objDoc.taxP = CDbl(grdVoucher.Item(ConstTaxP, i).Value)
            _objDoc.taxAmt = (CDbl(grdVoucher.Item(ConstTaxAmt, i).Value) * FCRt)
            If Val(.Item(ConstDisAmt, i).Value & "") = 0 Then .Item(ConstDisAmt, i).Value = 0
            _objDoc.ItemDiscount = CDbl(.Item(ConstDisAmt, i).Value) * FCRt / PPerU

            If Val(.Item(ConstDiscOther, i).Value & "") = 0 Then .Item(ConstDiscOther, i).Value = 0
            _objDoc.UnitDiscount = CDbl(.Item(ConstDiscOther, i).Value) * FCRt / PPerU

            If Val(.Item(ConstDisP, i).Value) = 0 Then .Item(ConstDisP, i).Value = 0
            _objDoc.DisP = CDbl(.Item(ConstDisP, i).Value) * FCRt / PPerU
            _objDoc.HSNCode = Trim(.Item(ConstBarcode, i).Value & "")

            If Val(.Item(ConstIGSTP, i).Value & "") = 0 Then .Item(ConstIGSTP, i).Value = 0
            _objDoc.IGSTP = CDbl(.Item(ConstIGSTP, i).Value)
            If Val(.Item(ConstCGSTP, i).Value & "") = 0 Then .Item(ConstCGSTP, i).Value = 0
            _objDoc.CSGTP = CDbl(.Item(ConstCGSTP, i).Value)
            If Val(.Item(ConstSGSTP, i).Value & "") = 0 Then .Item(ConstSGSTP, i).Value = 0
            _objDoc.SGSTP = CDbl(.Item(ConstSGSTP, i).Value)
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
            _objDoc.IGSTAmt = CDbl(.Item(ConstIGSTAmt, i).Value) * FCRt
            _objDoc.CGSTAMT = CDbl(.Item(ConstCGSTAmt, i).Value) * FCRt
            _objDoc.SGSTAmt = CDbl(.Item(ConstSGSTAmt, i).Value) * FCRt
            _objDoc.regularcessAmt = (CDbl(.Item(ConstregularCessAmt, i).Value) * FCRt)
            _objDoc.FloodcessAmt = (CDbl(.Item(ConstFloodCessAmt, i).Value) * FCRt)
            _objDoc.AdditionalcessAmt = ((CDbl(.Item(ConstAdditionalcess, i).Value) * CDbl(.Item(ConstQty, i).Value)) * FCRt)
            _objDoc.CessAmt = (CDbl(grdVoucher.Item(ConstcessAmt, i).Value) * FCRt)

            _objDoc.ImpDocId = Val(.Item(ConstImpDocId, i).Value & "")
            _objDoc.ImpDocLnNo = Val(.Item(ConstImpLnId, i).Value & "")
        End With
    End Sub

    Private Function chekDuplicate() As Boolean
        Dim dtTable As DataTable
        Dim varNextFoundBool As Boolean
ChkAgain:
        dtTable = _objcmnbLayer._fldDatatable("SELECT * FROM DocCmnTb WHERE DocType = 'SO' AND DNo = " & Val(numVchrNo.Text))
        If dtTable.Rows.Count > 0 Then
            If MsgBox("Entered Voucher number already exists. Fill next ?", vbQuestion + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                If Not varNextFoundBool Then
                    NextNumber()
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
            For r = 0 To .RowCount - 1 '- 1
                If Val(.Item(ConstQty, r).Value) = 0 Then .Item(ConstQty, r).Value = 0
                If Trim(.Item(ConstItemCode, r).Value) = "" And CDbl(.Item(ConstQty, r).Value) = 0 Or .Item(ConstSlNo, r).Value.ToString = "M" Then
                    GoTo NextR
                End If
                Exsts = True
                If CDbl(.Item(ConstQty, r).Value) = 0 Then
                    .Rows(r).Selected = True
                    .CurrentCell = .Item(ConstQty, r)
                    MsgBox("Zero Quantity !!", vbExclamation)
                    .FirstDisplayedScrollingRowIndex = r
                    GoTo Ter
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
    Private Sub setCmnValue()
        _objDoc = New clsDocCmn
        With _objDoc
            .DocType = "SO"
            .Reference = Strings.Trim(txtReference.Text)
            .UserId = CurrentUser
            .MchName = MACHINENAME
            .Comment = Trim(txtDescr.Text & "")
            .DDate = cldrdate.Value
            .JobCode = txtJob.Text
            .FromJob = ""
            .FromLoc = ""
            .DocDefLoc = ""
            .FCRate = 1
            .CSCode = Val(txtSuppAlias.Tag)
            .Prefix = txtprefix.Text
            .DNo = Val(numVchrNo.Text)
            .DocAmt = CDbl(lblNetAmt.Text)
            .DocId = loadedTrId
            .isModi = isModi
            .SlManId = cmbsalesman.Tag
            .TermsId = ""
            .BrId = ""
            .DeptId = ""
            .Attn = txtAttn.Text
            .Subject = txtsubject.Text
            .withTax = chktaxInv.Checked
            .TaxType = Val(lblstatecode.Tag) 'if 1 then its IGST bill or CGST AND SGST Bill
            .rndoff = Val(txtroundOff.Text) * IIf(cmbsign.SelectedIndex = 1, -1, 1)
            .DocLstTxt = txtDOLst.Text
            .Discount = CDbl(numDisc.Text)
            .NetAmt = CDbl(lblNetAmt.Text)
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

    Private Sub CustomerQuotation_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
        If Not fSelect Is Nothing Then fSelect.Close() : fSelect = Nothing
        If Not fProductEnquiry Is Nothing Then fProductEnquiry.Close() : fProductEnquiry = Nothing
        If Not fSlctDoc Is Nothing Then fSlctDoc.Close() : fSlctDoc = Nothing
        If Not fSerialno Is Nothing Then fSerialno.Close() : fSerialno = Nothing
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
        If Not diableNegativeSale Then
            grdVoucher.CurrentCell = grdVoucher.Item(3, grdVoucher.CurrentRow.Index)
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

    Private Sub numDisc_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles numDisc.KeyPress, txtdp.KeyPress, txtfcrt.KeyPress, txtroundOff.KeyPress
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
    Private Sub setCustomer(Optional ByVal accid As Long = 0)
        Dim dt As DataTable
        Dim condition As String
        If accid > 0 Then
            condition = "where accid=" & accid
        Else
            condition = "where Alias='" & txtSuppAlias.Text & "'"
        End If
        dt = _objcmnbLayer._fldDatatable("SELECT AccMast.accid,Alias,AccDescr,Address1,Address2,Address3,Address4," & _
                                        "Phone,Fax,EMail,PT1,PT2,PT3,PT4,TrdLcno,TrdDate,CreditLimit,DueDays,SlsmanId,CurrencyCode,CountryCode,GSTIN,Remarks " & _
                                        " from AccMast LEFT JOIN AccMastAddr ON AccMast.accid=AccMastAddr.AccountNo " & condition)
        If dt.Rows.Count > 0 Then
            txtSuppAlias.Tag = dt(0)("accid")
            chgbyprg = True
            txtSuppAlias.Text = Trim("" & dt(0)("Alias"))
            txtSuppName.Text = Trim("" & dt(0)("AccDescr"))
            chgbyprg = False
            'If accid > 0 Then

            'End If
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
            End If
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
            lbllimit.Text = Format(Val(dt(0)("CreditLimit")), lnumFormat)
            Dim iNBal As Double = getAccBal(Val(txtSuppAlias.Tag))
            lblbalance.Text = Strings.Format(iNBal, lnumFormat)
            If IsDBNull(dt(0)("DueDays")) Then
                dt(0)("DueDays") = 0
            End If
            If Val(dt(0)("DueDays") & "") > 0 Then
                iNBal = getAccAegBal(Val(txtSuppAlias.Tag), DateValue(DateTime.Now), Val(dt(0)("DueDays")))
                lblInvoices.Text = Strings.Format(iNBal, lnumFormat)
            End If
            'If EnableGST Then CalculateGST()
            Dim i As Integer
            With grdVoucher
                For i = 0 To .RowCount - 1
                    If enablecess And Val(lblgstn.Tag & "") = 0 And cessdate <= DateValue(cldrdate.Value) Then
                        .Item(ConstcessAmt, i).Value = Format(((CDbl(.Item(ConstActualPrice, i).Value) * .Item(Constcess, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value), lnumFormat)
                    Else
                        .Item(ConstcessAmt, i).Value = 0
                    End If
                Next
                calculate()
            End With

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

    Private Sub btnSlct_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSlct.Click
        fSlctDoc = New SelectInvTr
        With fSlctDoc
            .strType = "SO"
            .Text = "Select Customer Quotation"
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


    Private Sub txtReference_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtReference.TextChanged
        chgPost = True
        'btnUpdate.Enabled = Val(btnUpdate.Tag) > 0
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        If chkFormat.Checked Then
            fRptFormat = New RptFormatfrm
            fRptFormat.RptType = "SO"
            fRptFormat.ShowDialog()
            fRptFormat = Nothing
        Else
            PrepareRpt("SO")
        End If
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
        btnupdate.Enabled = True
        btndelete.Enabled = True
        txtDescr.Text = getTerms()
        'btnNext.Visible = True
    End Sub

    Private Sub ClearClick()
        If btndelete.Enabled = False Then Exit Sub
        If MsgBox("Do You Want Clear Etry", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        ClearControls()
    End Sub

    Private Sub DeleteClick()
        If btndelete.Enabled = False Then Exit Sub
        If Val(btndelete.Tag) = 0 Then
            MsgBox("This user do not have permission to Remove", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If MsgBox("You are going to REMOVE the Sales Invoice # " & numVchrNo.Text & Chr(13) & "Are you sure ?", vbOKCancel + vbQuestion + vbDefaultButton2) = vbOK Then
            _objcmnbLayer._saveDatawithOutParm("delete from DocCmnTb where DocId=" & loadedTrId)
            _objcmnbLayer._saveDatawithOutParm("delete from DocTranTb where DocId=" & loadedTrId)
            btnModify_Click(btnModify, New System.EventArgs)
        End If

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
        _objDoc = New clsDocCmn
        _objDoc.Prefix = txtPPrefix.Text
        _objDoc.DNo = Val(numPrintVchr.Text)
        _objDoc.DocType = "SO"
        If _objDoc.DNo = 0 Then Exit Sub
        Dim ds As DataSet
        ds = _objDoc.ldDoc("rturnDocumentDetailsForDocumentPrint", pno)
        frm.SetReport(ds, FileName, 0, False, , ToPrint)
        frm.MdiParent = Me.MdiParent
        frm.Text = RptCaption
        If Not ToPrint Then frm.Show()
    End Sub

    Private Sub txtroundOff_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtroundOff.Validated
        calculate()
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
        grdVoucher.CurrentCell = grdVoucher.Item(ConstItemCode, grdVoucher.CurrentRow.Index)
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

    Private Sub grdVoucher_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellEndEdit
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
                txtAttn.Focus()
            End If

        End If

    End Sub

    Private Sub cldrdate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cldrdate.ValueChanged
        chgPost = True
    End Sub

    Private Sub btnimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnimport.Click
        Dim rindex As Integer = 0
        If Not grdVoucher.CurrentRow.Index = Nothing Then
            rindex = grdVoucher.CurrentRow.Index
        End If
        loadImpDocs(rindex)
    End Sub
    Private Sub loadImpDocs(ByVal rindex As Integer)
        If fImportdcos Is Nothing Then
            fImportdcos = New ViewImportedDocsFrm
        End If
        With fImportdcos
            If .Visible = False Then
                .Show()
                .Top = Me.Top + Screen.PrimaryScreen.WorkingArea.Height - .Height + 20
            End If
            .txtitemname.Text = grdVoucher.Item(ConstDescr, rindex).Value
            .txtitemname.Tag = grdVoucher.Item(ConstId, rindex).Value
            .lbldoc.Text = numVchrNo.Text
            .lbldoc.Tag = loadedTrId
            If .lbldoc.ForeColor = Color.Black Then
                .lbldoc.ForeColor = Color.Red
            Else
                .lbldoc.ForeColor = Color.Black
            End If
            .loadImportedDocs()
        End With
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        If EnableWarranty = False And Me.Width > 1200 Then resizeGridColumn(grdVoucher, ConstDescr)
        If Me.Width <= 1200 Then
            grdVoucher.Columns(ConstDescr).Width = 250
        End If
    End Sub

    Private Sub CustomerQuotation_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
    End Sub

    Private Sub cmbfc_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbfc.SelectedIndexChanged
        returnFcrt()
    End Sub

    Private Sub txtfcrt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtfcrt.KeyDown
        If e.KeyCode = Keys.Return Then
            btnadd.Focus()
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
                .Item(ConstActualPrice, i).Value = (CDbl(.Item(ConstUPrice, i).Value) * 100) / (CDbl(.Item(ConstTaxP, i).Value) + CDbl(.Item(Constcess, i).Value) + 100)
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
        CalculateGST()
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
            dtItemInfo = _objcmnbLayer._fldDatatable("SELECT Rack,QIH, MRP,UnitPrice Price,(((isnull(IGST,1)+ISNULL(vat,0))*UnitPrice)/100)+UnitPrice [Tax Price]," & _
                                                     "UnitPriceWS WSP,CostAvg [C Avg]," & _
                                                     "(((isnull(IGST,0)+ISNULL(vat,0))*CostAvg)/100)+CostAvg [Cost+Tax]," & _
                                                     "LastPurchCost LPC,itemid FROM INVITM " & _
                                                      " LEFT JOIN GSTTb ON GSTTb.HSNCode=InvItm.HSNCode" & _
                                                      " LEFT JOIN VatMasterTb ON VatMasterTb.vatid=InvItm.vatid " & _
                                                     " where itemid=" & itemid)




            grdItemInfo.DataSource = dtItemInfo
            SetGridItemInfo()
        Else
            If itemid = 0 And dtItemInfo.Rows.Count > 0 Then dtItemInfo.Rows.Clear() : grdItemInfo.DataSource = dtItemInfo
            Dim _qurey As EnumerableRowCollection(Of DataRow)
            _qurey = (From data In dtItemInfo.AsEnumerable() Where data("itemid") = itemid Select data)
            If _qurey.Count = 0 Then
                dt = _objcmnbLayer._fldDatatable("SELECT Rack,QIH,MRP,UnitPrice Price,(((isnull(IGST,0)+ISNULL(vat,0))*UnitPrice)/100)+UnitPrice [Tax Price]," & _
                                                 "UnitPriceWS WSP,CostAvg [C Avg],((isnull(IGST,0)*CostAvg)/100)+CostAvg [Cost+Tax]," & _
                                                 "LastPurchCost LPC,itemid FROM INVITM " & _
                                                  " LEFT JOIN GSTTb ON GSTTb.HSNCode=InvItm.HSNCode" & _
                                                  " LEFT JOIN VatMasterTb ON VatMasterTb.vatid=InvItm.vatid " & _
                                                 " where itemid=" & itemid)
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

            .Columns("Cost+Tax").Width = 70
            .Columns("Cost+Tax").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("Cost+Tax").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Cost+Tax").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

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
        PrepareRpt("SO", True)
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


    Private Sub txtroundOff_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtroundOff.TextChanged
        If chgNumByPgm Then Exit Sub
        calculate()
    End Sub

    Private Sub CustomerQuotation_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Timer1.Enabled = True
    End Sub
    Public Sub loadFroJob(ByVal customerid As Long, ByVal jobcode As String, ByVal jobname As String, ByVal isgarragejb As Boolean)
        isGarrage = isgarragejb
        chgbyprg = True
        txtjobname.Text = jobname
        txtJob.Text = jobcode
        chgbyprg = False
        setCustomer(customerid)
    End Sub

    Private Sub btnshow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnshow.Click
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
    Private Function ImportDOs(ByVal Docids As String, Optional ByVal trasferAllItems As Boolean = False) As Boolean

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
                        If IsDBNull(sRs(i)("rndoff")) Then sRs(i)("rndoff") = 0
                        If Val(sRs(i)("rndoff")) > 0 Then
                            cmbsign.SelectedIndex = 0
                            txtroundOff.Text = Format(CDbl(sRs(i)("rndoff")), numFormat)
                        Else
                            cmbsign.SelectedIndex = 1
                            txtroundOff.Text = Format(CDbl(sRs(i)("rndoff")) * -1, numFormat)
                        End If
                    End If
                    If dno <> sRs(i)("DNO") Then
                        txtDOLst.Text = IIf(txtDOLst.Text = "", "", ",") & sRs(i)("DNO")
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

    Private Sub fImportdcos_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fImportdcos.FormClosed
        fImportdcos = Nothing
    End Sub


    Private Sub txtDescr_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDescr.TextChanged
        chgPost = True
    End Sub

    Private Sub txtcustAddress_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcustAddress.TextChanged
        chgPost = True
    End Sub

    Private Sub LaundryOrderFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            StrAccMastSrch = "SELECT AccDescr, Alias, ClosingBal As Balance, OpnBal, AccountNo, S1AccHd.S1AccId, AccSetId, GrpSetOn,accid FROM" & _
                            " AccMast INNER JOIN S1AccHd ON S1AccHd.S1AccId = AccMast.S1AccId "
            'btnNext_Click(btnNext, New System.EventArgs())
            'dtTax = _objcmnbLayer._fldDatatable("SELECT vatcode,convert(decimal,0) Amount,collectionAC,paymentAC,vat From VatMasterTb", False)
            chgbyprg = True
            If ShowTaxOnInventory Then
                dtTax = _objcmnbLayer._fldDatatable("SELECT vatcode,convert(money, 0) Amount,collectionAC,paymentAC,vat From VatMasterTb", False)
            ElseIf EnableGST Then
                CreateTaxTable(dtTax)
                chktaxInv.Checked = True
                If withNonTaxBill Then
                    chktaxInv.Checked = False
                End If
            Else
                chktaxInv.Checked = False
            End If

            chgbyprg = True
            dtTb = _objcmnbLayer._fldDatatable("SELECT trid,itemid,id FROM ItmInvTrTb WHERE TRID=0")
            _objcmnbLayer.dtSerialNo = createdtSerialNo()
            SetGridHead()
            NextNumber()
            lnumFormat = numFormat
            FCRt = 1
            OthCost = 0

            cldrdate.Value = Format(Date.Now, DtFormat)
            NDec = NoOfDecimal
            loadedTrId = 0
            chgbyprg = False
            ldSman()
            LodCurrency()
            If enableGCC Then
                lblgstn.Text = "TIN : "
                lblstatecode.Text = "Emirate : "
                Label20.Text = "VAT Total"
            End If
            'cmbVrType_SelectedIndexChanged(sender, e)
            'numVchrNo.Select()
            ChgId = False
            chgPost = False
            If isModi Then
                btnModify_Click(btnModify, New System.EventArgs)
                If userType Then
                    btnupdate.Tag = IIf(getRight(209, CurrentUser), 1, 0)
                    btndelete.Tag = IIf(getRight(210, CurrentUser), 1, 0)
                Else
                    btnupdate.Tag = 1
                    btndelete.Tag = 1
                End If

                btndelete.Text = "Delete"
            Else
                AddNewClick()
                If userType Then
                    btnupdate.Tag = IIf(getRight(208, CurrentUser), 1, 0)
                Else
                    btnupdate.Tag = 1
                End If
                btndelete.Text = "Clear"
                btndelete.Tag = 1
            End If
            Timer1.Enabled = True
            cessdate = getCessDate()
            chkautoroundOff.Checked = enableAutoRoundOff
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnaddcust_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddcust.Click
        QuickCust("Customer")
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

    Private Sub fCrtAcc_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fCrtAcc.FormClosed
        fCrtAcc = Nothing
    End Sub

    Private Sub fCrtAcc_OpenAccMaster(ByRef AccountNo As Long) Handles fCrtAcc.OpenAccMaster
        txtSuppAlias.Tag = AccountNo
        setCustomer(AccountNo)
    End Sub
End Class