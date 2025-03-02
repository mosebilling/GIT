Imports System.IO

Public Class ManufacturingFrm

#Region "Public Variables"
    Public isModi As Boolean
    Public isCust As Boolean
#End Region
#Region "Local Variables"
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
    Private lnumformat As String
    Private dtTax As DataTable
    Private chgUprice As Boolean



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
    Private dtrawmaterial As DataTable
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
#End Region
#Region "Const Variables"
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
        isModi = True
        Dim InvList As DataTable
        If FromTrId <> 0 Then
            InvList = _objcmnbLayer._fldDatatable("SELECT TrId FROM ItmInvCmnTb WHERE TrType = 'MI' AND TrId = " & FromTrId)
        Else
            InvList = _objcmnbLayer._fldDatatable("SELECT TrId FROM ItmInvCmnTb WHERE TrType = 'MI' AND InvNo = " & Val(numVchrNo.Text) & " AND PreFix = '" & MkDbSrchStr(Trim(txtprefix.Text), True) & "'")
        End If
        If InvList.Rows.Count > 0 Then
            loadedTrId = InvList(0)("TrId")
            InvList = Nothing
            ldPostedInv()
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
        Dim sRs As New DataTable
        'Dim Discount As Double
        Dim UPerPack As Double
        Dim Protect As Boolean
        Dim CrossBr As Boolean
        ItmInvCmnTb = _objcmnbLayer._fldDatatable("SELECT ItmInvCmnTb.*,jobname FROM ItmInvCmnTb LEFT JOIN JobTb ON ItmInvCmnTb.[Job Code]=JobTb.jobcode WHERE TrId = " & loadedTrId & " AND TrType = 'MI'")
        chgbyprg = True
        ActBr = ""
        If ItmInvCmnTb.Rows.Count = 0 Then GoTo Quit
        'getProtectUntil()
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
        chgbyprgN = True
        cmbVoucherTp.Text = getVoucherName(Val(ItmInvCmnTb(0)("InvTypeNo") & ""))
        cldrdate.Value = Format(ItmInvCmnTb(0)("TrDate"), DtFormat) ' "MM/dd/yyyy")
        clrDuedate.Value = Format(ItmInvCmnTb(0)("DueDate"), DtFormat)
        txtprefix.Text = ItmInvCmnTb(0)("PreFix")
        'txtprefix.Tag = ItmInvCmnTb(0)("PreFix")
        numVchrNo.Text = ItmInvCmnTb(0)("InvNo") 'Val(Mid(!InvNo, 3)) '!InvNo '
        numVchrNo.Tag = ItmInvCmnTb(0)("InvNo")
        txtPPrefix.Text = ItmInvCmnTb(0)("PreFix")
        numPrintVchr.Text = ItmInvCmnTb(0)("InvNo")  'CMNREC(12).FormattedText
        txtJob.Text = Trim("" & ItmInvCmnTb(0)("Job Code"))
        txtjobname.Text = Trim(ItmInvCmnTb(0)("jobname") & "")
        cmbfc.Text = Trim(ItmInvCmnTb(0)("FC") & "")
        txtfcrt.Text = Format(ItmInvCmnTb(0)("FCRate"), lnumformat)
        FCRt = ItmInvCmnTb(0)("FCRate")
        'sRs = _objcmnbLayer._fldDatatable("SELECT AccMast.accid,Alias,AccDescr,Address1,Address2,Address3,Address4,Phone,Fax,EMail,PT1,PT2,PT3,PT4,TrdLcno,TrdDate from AccMast LEFT JOIN AccMastAddr ON AccMast.AccountNo=AccMastAddr.AccountNo WHERE AccMast.accid = " & ItmInvCmnTb(0)("CSCode"))

        txtCashSuppName.Text = Trim(ItmInvCmnTb(0)("CashCustName") & "")
        'If sRs.Rows.Count > 0 Then
        '    If chgbyprg = False Then chgbyprg = True
        '    txtMIN.Tag = sRs(0)("accid")
        '    txtMIN.Text = sRs(0)("AccDescr")

        '    'setSupplier(sRs(0)("accid"))
        'End If
        'If sRs.Rows.Count > 0 Then sRs.Clear()
        'sRs = _objcmnbLayer._fldDatatable("SELECT * FROM AccMast WHERE accid = " & ItmInvCmnTb(0)("PSAcc"))

        'If sRs.Rows.Count > 0 Then
        '    txtStockAlias.Tag = ItmInvCmnTb(0)("PSAcc")
        '    txtStockAlias.Text = sRs(0)("Alias")
        '    'txtPurchaseName.Text = sRs(0)("AccDescr")
        'End If
        txtReference.Text = Trim("" & ItmInvCmnTb(0)("TrRefNo"))
        txtDescr.Text = Trim("" & ItmInvCmnTb(0)("TrDescription"))
        numDisc.Text = Format(ItmInvCmnTb(0)("Discount") / FCRt, lnumformat)
        If Not IsDBNull(ItmInvCmnTb(0)("rndoff")) Then
            If Val(ItmInvCmnTb(0)("rndoff")) > 0 Then
                cmbsign.Text = "+"
            Else
                cmbsign.Text = "-"
                ItmInvCmnTb(0)("rndoff") = ItmInvCmnTb(0)("rndoff") * -1
            End If
            txtroundOff.Text = Format(CDbl(ItmInvCmnTb(0)("rndoff")), lnumformat)
        Else
            txtroundOff.Text = Format(0, lnumformat)
        End If
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
        If sRs.Rows.Count > 0 Then sRs.Clear()
        sRs = _objcmnbLayer._fldDatatable("SELECT ItmInvTrTb.*, [Item Code],Warrentyname,isSerialNo FROM ItmInvTrTb LEFT JOIN InvItm ON InvItm.ItemId = ItmInvTrTb.ItemId " & _
                                          " LEFT JOIN InvItmPropertiesTb ON InvItmPropertiesTb.Itemid=invitm.itemid " & _
                                          "LEFT JOIN WarrentyMastertb ON WarrentyMastertb.Wid=ItmInvTrTb.WarrentyId WHERE TrId = " & loadedTrId & " ORDER BY SlNo")
        grdVoucher.RowCount = 0
        If _objcmnbLayer.dtSerialNo.Rows.Count > 0 Then _objcmnbLayer.dtSerialNo.Rows.Clear()
        getRawmaterial(0, loadedTrId)
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
                    'grdVoucher.Item(ConstMthd, i).Value = sRs(i)("Method")
                    'grdVoucher.Item(ConstUnitVal, i).Value = sRs(i)("UnitValue") * UPerPack / FCRt
                    'grdVoucher.Item(ConstDiscOther, i).Value = sRs(i)("DiscOther") * UPerPack / FCRt
                    grdVoucher.Item(ConstDiscOther, i).Value = sRs(i)("UnitDiscount") * UPerPack / FCRt
                    'grdVoucher.Item(ConstImpDocId, i).Value = sRs(i)("ImpDocId")
                    'grdVoucher.Item(ConstImpLnId, i).Value = sRs(i)("ImpDocLnNo")
                    grdVoucher.Item(ConstId, i).Value = sRs(i)("id")
                    If Not IsDBNull(sRs(i)("WarrentyExpDate")) Then
                        If DateValue(sRs(i)("WarrentyExpDate")) > DateValue("01/01/1950") Then
                            grdVoucher.Item(ConstWarrentyExpiry, i).Value = sRs(i)("WarrentyExpDate")
                        End If
                    End If
                    If .Item(ConstSerialNo, i).Value <> "" Then
                        AddTodtSerialNo(.Item(ConstSerialNo, i).Value, Val(.Item(ConstItemID, .CurrentRow.Index).Value), .CurrentRow.Index, DateValue(.Item(ConstWarrentyExpiry, .CurrentRow.Index).Value), Val(.Item(ConstId, .CurrentRow.Index).Value))
                    End If
                Next
                reArrangeNo()
                'calculateRawmaterials(i)
            End If
        End With

        'MsgBox(grdVoucher.Item(ConstActualPrice, 0).Value)
        If sRs.Rows.Count > 0 Then sRs.Clear()
        showOtherCost(False)
        calculate()
        If Val(lblbalance.Text) > 0 Then
            lblbalance.Text = Format(CDbl(lblbalance.Text) - CDbl(lblNetAmt.Text), numFormat)
        End If
        If Val(lblInvoices.Text) > 0 Then
            lblInvoices.Text = Format(CDbl(lblInvoices.Text) - 1, numFormat)
        End If

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
        btnbarcode.Visible = True
        chgbyprg = False
        chgbyprgN = False
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
        'Try
        '    Dim dtInv As DataTable
        '    _objcmnbLayer = New clsCommon_BL
        '    '//next number to Admission
        '    dtInv = _objcmnbLayer._fldDatatable("SELECT Prefix,InvNo FROM InvNos WHERE InvType='MI'")
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
            chgbyprg = True
            With cmbVoucherTp
                If .Items.Count > 0 Then
                    If .SelectedIndex < 0 Then .SelectedIndex = 0
                    cmbVoucherTp.Tag = getvrsId(cmbVoucherTp.Text, 11)
                    getVrsDet(Val(cmbVoucherTp.Tag), "MI", vrPrefix, vrVoucherNo, vrAccountNo1, vrAccountNo2)
                Else
                    getVrsDet(0, "MI", vrPrefix, vrVoucherNo, vrAccountNo1, vrAccountNo2)
                End If

            End With
            If Not isModi Then
                numVchrNo.Text = vrVoucherNo
                txtprefix.Text = vrPrefix
            End If
            If Val(txtMIN.Tag) = 0 Then
                txtMIN.Tag = vrAccountNo2
            End If
            If Val(txtMout.Tag) = 0 Then
                txtMout.Tag = vrAccountNo1
            End If
            Dim dtAcc As DataTable
            dtAcc = _objcmnbLayer._fldDatatable("SELECT Alias,AccDescr,GrpSetOn,GrpSetOn,accid FROM AccMast " & _
                                                "LEFT JOIN S1AccHd ON AccMast.S1AccId=S1AccHd.S1AccId WHERE accid=" & vrAccountNo1)
            If dtAcc.Rows.Count > 0 Then
                txtMIN.Text = dtAcc(0)("AccDescr")
                txtMIN.Tag = vrAccountNo1

            Else
                txtMIN.Text = ""
                txtMIN.Tag = ""
            End If
            dtAcc = _objcmnbLayer._fldDatatable("SELECT Alias,AccDescr,GrpSetOn,GrpSetOn,accid FROM AccMast " & _
                                               "LEFT JOIN S1AccHd ON AccMast.S1AccId=S1AccHd.S1AccId WHERE accid=" & vrAccountNo2)
            If dtAcc.Rows.Count > 0 Then
                txtMout.Text = dtAcc(0)("AccDescr")
                txtMout.Tag = vrAccountNo2
            Else
                txtMout.Text = ""
                txtMout.Tag = ""
            End If
            txtReference.Focus()
            chgbyprg = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub txtReference_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDescr.KeyDown, txtStock.KeyDown, txtMoutAlias.KeyDown, txtReference.KeyDown, txtjobname.KeyDown, txtJob.KeyDown, txtcredit.KeyDown, txtCashSuppName.KeyDown
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
            ElseIf MyCtrl.Name = "txtCashSuppName" Then
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
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
        fSelect = New Selectfrm
        'nSelect = BVal
        SetForm(fSelect, BVal)
        If BVal = 1 Or BVal = 0 Then
            fSelect.Width = 742
            fSelect.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
        Else
            fSelect.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedToolWindow
            fSelect.Width = 425
        End If
        fSelect.Show()
    End Sub


    Private Sub txtLocation_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtJob.TextChanged, txtStock.TextChanged, txtMoutAlias.TextChanged, txtcredit.TextChanged
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
            Dim x As Integer = Width - fMList.Width - 100
            Dim y As Integer = Height - fMList.Height - 100
            PopupLoc = New Point(x, y)
            If fMList.Visible = False Then
                Select Case _srchTxtId - 1
                    Case 0, 1
                        SetFmlist(fMList, 20)
                    Case 2 'job 
                        SetFmlist(fMList, 8)
                    Case 3
                        SetFmlist(fMList, 13, 0)

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
                fMList.Search(txtMoutAlias.Text)
                txtStock.Text = fMList.AssignList(txtMoutAlias, lstKey, chgbyprg)
            Case 1   'Customer name
                fMList.SearchIndex = 0
                fMList.SearchIndexDescr = 1
                fMList_doFocus()
                fMList.Search(txtStock.Text)
                txtMoutAlias.Text = fMList.AssignList(txtStock, lstKey, chgbyprg)
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
                txtMoutAlias.Text = ItmFlds(1)
                txtStock.Text = ItmFlds(0)
                txtMoutAlias.Tag = ItmFlds(3)
            Case 2
                txtJob.Text = ItmFlds(0)
            Case 3
                txtcredit.Text = ItmFlds(0)
                txtcredit.Tag = ItmFlds(3)
        End Select
        chgbyprg = False
    End Sub
    Private Sub SetGridHead()
        chgbyprg = True
        SetGridHeadEntryProperty(grdVoucher)
        With grdVoucher
            '.Columns(ConstDescr).Width = 350
            '.Columns(ConstDisAmt).Width = Width * 7 / 100 '70
            ''.Columns(ConstTaxP).Width =  Width * 6 / 100 '60
            '.Columns(ConstTaxAmt).Width = Width * 7 / 100 '70
            '.Columns(ConstUnitOthCost).Width = Width * 9 / 100 '80
            '.Columns(ConstNUPrice).Width = Width * 8 / 100 '70
            .Columns(ConstMRP).Visible = enableMRPInStockIn
            .Columns(ConstSP1).Visible = enableSP1InStockIn
            .Columns(ConstSP2).Visible = enableSP2InStockIn
            .Columns(ConstSP3).Visible = enableSP3InStockIn
            Dim i As Integer
            For i = 1 To ConstUPrice
                cmbcolms.Items.Add(.Columns(i).HeaderText)
            Next
            .Columns(ConstTaxAmt).Visible = False
            .Columns(ConstTaxP).Visible = False
        End With

        chgbyprg = False
    End Sub


    Private Sub fSelect_doSelect(ByVal KeyId As String, ByVal strFld1 As String, ByVal strFld2 As String) Handles fSelect.doSelect
        chgbyprg = True
        If _srchTxtId = 1 Or _srchTxtId = 2 Then
            txtMoutAlias.Text = strFld2
            txtStock.Text = strFld1
            txtMoutAlias.Tag = KeyId
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
            .CurrentCell = .Item(ConstItemCode, i)
            chgbyprg = True
            .BeginEdit(True)
            chgbyprg = False
            chgItm = False
        End With
        calculate()
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
            numDisc.Text = Format(0, lnumformat)
        End If
        If Not dtTax Is Nothing Then
            For j = 0 To dtTax.Rows.Count - 1
                dtTax(j)("Amount") = 0
            Next
        End If
        With grdVoucher
            For i = 0 To .Rows.Count - 1
                If Val(.Item(ConstItemID, i).Value) = 0 Then GoTo nxt
                '.Item(ConstSlNo, i).Value = i + 1
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
                    .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstCGSTAmt, i).Value) + CDbl(.Item(ConstSGSTAmt, i).Value), lnumformat)
                Else
                    .Item(ConstTaxAmt, i).Value = Format(((CDbl(.Item(ConstActualPrice, i).Value) * .Item(ConstTaxP, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value), lnumformat)
                End If
                If chkTaxbill.Checked = False Then
                    .Item(ConstTaxAmt, i).Value = Format(0, lnumformat)
                    .Item(ConstTaxP, i).Value = Format(0, lnumformat)
                End If
                totTax = totTax + .Item(ConstTaxAmt, i).Value
                totLnDis = totLnDis + .Item(ConstDisAmt, i).Value
                'totAmt = totAmt + CDbl(.Item(ConstActualPrice, i).Value) * CDbl(.Item(ConstQty, i).Value)
                totAmt = totAmt + (.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value) + CDbl(.Item(ConstTaxAmt, i).Value)
                totItm = totItm + (.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value)
                .Item(constItmTot, i).Value = Format((.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value), lnumformat)
                .Item(ConstLTotal, i).Value = Format((.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value) + CDbl(.Item(ConstTaxAmt, i).Value), lnumformat)
                .Item(ConstNUPrice, i).Value = CDbl(.Item(ConstActualPrice, i).Value) - (CDbl(.Item(ConstDisAmt, i).Value) / CDbl(.Item(ConstQty, i).Value)) + (CDbl(.Item(ConstTaxAmt, i).Value) / CDbl(.Item(ConstQty, i).Value)) - Val(.Item(ConstDiscOther, i).Value)
                If ShowTaxOnInventory Then
                    For j = 0 To dtTax.Rows.Count - 1
                        If Val(.Item(ConstTaxP, i).Value) = dtTax(j)("vat") Then
                            dtTax(j)("Amount") = CDbl(dtTax(j)("Amount")) + CDbl(grdVoucher.Item(ConstTaxAmt, i).Value)
                        End If
                    Next
                ElseIf EnableGST Then
                    'CalculateGST()
                End If

nxt:
            Next
            calOthCost()

            lblTotAmt.Text = Format(totItm, lnumformat)
            lblNetAmt.Text = Format(totAmt + CDbl(lblOthCost.Text), lnumformat)
            If Val(txtroundOff.Text) > 0 Then
                lblNetAmt.Text = Format(CDbl(lblNetAmt.Text) - (IIf(cmbsign.SelectedIndex = 1, 1, -1) * CDbl(txtroundOff.Text)), lnumformat)
            End If
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
        Dim actualPrice As Double
        With grdVoucher
            For i = 0 To .Rows.Count - 1 '- 1
                If IsDBNull(.Item(ConstQty, i).Value) Then .Item(ConstQty, i).Value = 0
                If CStr(.Item(ConstQty, i).Value) = "" Then .Item(ConstQty, i).Value = 0
                If Val(.Item(ConstMthd, i).Value & "") <> 0 Then
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
    'Private Function calOthCost(Optional ByVal bfrmSetOthrCost As Boolean = False) As Double
    '    If Not bfrmSetOthrCost Then setOthCost()
    '    Dim i As Integer
    '    Dim tOthVal As Double
    '    Dim tBAmt As Double
    '    Dim tBDAmt As Double
    '    Dim tDAmt As Double
    '    Dim DiscountOtherAmt As Double
    '    With grdVoucher
    '        For i = 0 To .Rows.Count - 1 '- 1
    '            If IsDBNull(.Item(ConstQty, i).Value) Then .Item(ConstQty, i).Value = 0
    '            If CStr(.Item(ConstQty, i).Value) = "" Then .Item(ConstQty, i).Value = 0
    '            If Val(.Item(ConstMthd, i).Value & "") <> 0 Then
    '                tOthVal = tOthVal + CDbl(.Item(ConstActualOthCost, i).Value) * CDbl(.Item(ConstQty, i).Value)
    '            Else
    '                tBAmt = tBAmt + CDbl(.Item(constItmTot, i).Value)
    '            End If
    '            If Val(.Item(ConstDisAmt, i).Value) <> 0 Then
    '                tDAmt = tDAmt + CDbl(.Item(ConstDisAmt, i).Value) ' * CDbl(.Item(ConstQty, i).Value)
    '            End If
    '            'If Val(.Item(ConstDiscOther, i).Value) <> 0 Then
    '            '    DiscountOtherAmt = DiscountOtherAmt + Val(.Item(ConstDiscOther, i).Value) * CDbl(.Item(ConstQty, i).Value)
    '            'End If
    '            tBDAmt = tBDAmt + CDbl(.Item(ConstLTotal, i).Value)
    '            'End If
    '        Next i
    '        tOthVal = OthCost / FCRt - tOthVal
    '        chgbyprg = True
    '        If numDisc.Text = "" Then numDisc.Text = 0
    '        chgbyprg = False
    '        DiscountOtherAmt = CDbl(numDisc.Text)
    '        'tBAmt = tBAmt - CDbl(txtDis.Text)
    '        For i = 0 To .Rows.Count - 1 '- 1
    '            If Val(.Item(ConstDisAmt, i).Value) <> 0 Then
    '                tDAmt = Val(.Item(ConstDisAmt, i).Value) / CDbl(.Item(ConstQty, i).Value)
    '            Else
    '                tDAmt = 0
    '            End If
    '            If Val(.Item(ConstMthd, i).Value) = 0 Then
    '                If tBAmt = 0 Then
    '                    .Item(ConstActualOthCost, i).Value = 0
    '                Else
    '                    If Val(grdVoucher.Item(ConstActualPrice, i).Value) = 0 Then grdVoucher.Item(ConstActualPrice, i).Value = 0
    '                    .Item(ConstActualOthCost, i).Value = tOthVal * (CDbl(grdVoucher.Item(ConstActualPrice, i).Value) - tDAmt) / tBAmt
    '                End If
    '                grdVoucher.Item(ConstUnitOthCost, i).Value = Format(Val(.Item(ConstActualOthCost, i).Value), "#,##0" & IIf(NoOfDecimal = 0, "", "." & StrDup(NoOfDecimal, "0"))) ' & NumFormat)'shine
    '            End If
    '            'If Val(.Item(const i, 20)) = 0 Then

    '            If DiscountOtherAmt = 0 Then
    '                .Item(ConstDiscOther, i).Value = 0
    '            Else
    '                .Item(ConstDiscOther, i).Value = DiscountOtherAmt * (CDbl(grdVoucher.Item(ConstActualPrice, i).Value) - tDAmt) / tBDAmt
    '            End If
    '            'End If
    '            If Val(.Item(ConstActualPrice, i).Value) = 0 Then .Item(ConstActualPrice, i).Value = 0
    '            grdVoucher.Item(ConstNUPrice, i).Value = Format(CDbl(.Item(ConstActualPrice, i).Value) + CDbl(.Item(ConstActualOthCost, i).Value) - CDbl(.Item(ConstDiscOther, i).Value) - tDAmt, "#,##0" & IIf(NoOfDecimal = 0, "", "." & StrDup(NoOfDecimal, "0"))) ' & NumFormat)
    '            If Val(.Item(ConstQty, i).Value) = 0 Then .Item(ConstQty, i).Value = 0
    '            calOthCost = calOthCost + CDbl(.Item(ConstQty, i).Value) * (Val(.Item(ConstActualOthCost, i).Value) - Val(.Item(ConstDiscOther, i).Value) - tDAmt)
    '            'calOthCost = calOthCost + (CDbl(.Item(ConstQty, i).Value) * (Val(.Item(ConstActualOthCost, i).Value) - Val(.Item(ConstDiscOther, i).Value))) - Val(.Item(ConstDisAmt, i).Value)

    '        Next i
    '    End With
    '    If lblTotAmt.Text = "" Then
    '        lblTotAmt.Text = Format(0, lnumformat)
    '    End If
    '    If (lblNetAmt.Text = "") Then
    '        lblNetAmt.Text = Format(0, lnumformat)
    '    End If
    '    If (numDisc.Text = "") Then
    '        numDisc.Text = Format(0, lnumformat)
    '    End If
    '    If (lbltax.Text = "") Then
    '        lbltax.Text = Format(0, lnumformat)
    '    End If
    '    lblNetAmt.Text = Format(((CDbl(lblTotAmt.Text) + CDbl(lbltax.Text)) - CDbl(numDisc.Text)), lnumformat)
    'End Function
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
                    If tbgst.Visible Then tbgst.Visible = False
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
        grdBeginEdit()
    End Sub

    Private Sub grdVoucher_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellValidated
        Valid(e.RowIndex, e.ColumnIndex)
    End Sub
    Private Sub Valid(ByVal RowIndex As Integer, ByVal ColIndex As Integer)
        With grdVoucher
            Select Case ColIndex
                Case ConstBarcode, ConstItemCode
                    If Not chgItm Then Exit Sub
                    If Trim(.Item(ColIndex, RowIndex).Value) = "" And SrchText <> "" Then .Item(ColIndex, RowIndex).Value = SrchText
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
                            .Item(ConstDisAmt, RowIndex).Value = Format(((CDbl(.Item(ConstActualPrice, RowIndex).Value) * Val(.Item(ConstDisP, RowIndex).Value)) / 100) * CDbl(.Item(ConstQty, RowIndex).Value), lnumformat)
                        End If
                        'If EnableGST Then getGSTDetails(Trim(.Item(ConstBarcode, RowIndex).Value & ""), RowIndex, True)
                        calculate()
                        calculateRawmaterials(RowIndex)
                    End If

                Case ConstUPrice
                    If chgAmt Then
                        .Item(ConstActualPrice, RowIndex).Value = .Item(ConstUPrice, RowIndex).Value
                        If AllowUnitDiscountEntryOnInventory Then
                            .Item(ConstDisAmt, RowIndex).Value = Format(((CDbl(.Item(ConstActualPrice, RowIndex).Value) * Val(.Item(ConstDisP, RowIndex).Value)) / 100) * CDbl(.Item(ConstQty, RowIndex).Value), lnumformat)
                        End If
                        'If EnableGST Then getGSTDetails(Trim(.Item(ConstBarcode, RowIndex).Value & ""), RowIndex, True)
                        calculate()
                    End If
                Case ConstDisP
                    If chgAmt Then
                        .Item(ConstDisAmt, RowIndex).Value = Format(((CDbl(.Item(ConstActualPrice, RowIndex).Value) * Val(.Item(ConstDisP, RowIndex).Value)) / 100) * CDbl(.Item(ConstQty, RowIndex).Value), lnumformat)
                        'If EnableGST Then getGSTDetails(Trim(.Item(ConstBarcode, RowIndex).Value & ""), RowIndex, True)
                        calculate()
                    End If
                Case ConstDisAmt
                    If chgAmt Then
                        Dim unitDiscount As Double
                        unitDiscount = CDbl(.Item(ConstDisAmt, RowIndex).Value) / CDbl(.Item(ConstQty, RowIndex).Value)
                        .Item(ConstDisP, RowIndex).Value = Format((unitDiscount * 100) / CDbl(.Item(ConstActualPrice, RowIndex).Value), lnumformat)
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
                Case Else
            End Select
        End With
        chgAmt = False
    End Sub
    Private Sub CalculateGST()
        Dim i As Integer
        Dim dtHSN As DataTable
        Dim dtrow As DataRow
        Dim slno As Integer
        dtTax.Rows.Clear()
        With grdVoucher
            Dim _qurey As EnumerableRowCollection(Of DataRow)
            For i = 0 To .RowCount - 1
                slno = 0
                _qurey = From data In dtGST.AsEnumerable() Where data("HSNCode") = Trim(.Item(ConstBarcode, i).Value & "") Select data
                If _qurey.Count > 0 Then
                    dtHSN = _qurey.CopyToDataTable
                    If Val(lblstatecode.Tag) = 0 Then
                        'adding CSGT Amount****
                        Dim a = (From data In dtTax.AsEnumerable() Where data("ACid") = dtHSN(0)("CGSTPAc") Select data("slno"))
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
                            dtrow("AccountName") = dtHSN(0)("CGSTPAname")
                            dtrow("ACid") = dtHSN(0)("CGSTPAc")
                            dtrow("Amount") = CDbl(.Item(ConstCGSTAmt, i).Value)
                            dtTax.Rows.Add(dtrow)
                        Else
                            dtTax(slno - 1)("Amount") = CDbl(dtTax(slno - 1)("Amount")) + CDbl(.Item(ConstCGSTAmt, i).Value)
                        End If
                        'adding SSGT Amount****
                        a = (From data In dtTax.AsEnumerable() Where data("ACid") = dtHSN(0)("SGSTPAc") Select data("slno"))
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
                            dtrow("AccountName") = dtHSN(0)("SGSTPAname")
                            dtrow("ACid") = dtHSN(0)("SGSTPAc")
                            dtrow("Amount") = CDbl(.Item(ConstSGSTAmt, i).Value)
                            dtTax.Rows.Add(dtrow)
                        Else
                            dtTax(slno - 1)("Amount") = CDbl(dtTax(slno - 1)("Amount")) + CDbl(.Item(ConstSGSTAmt, i).Value)
                        End If
                    Else
                        'adding ISGT Amount****
                        Dim a = (From data In dtTax.AsEnumerable() Where data("ACid") = dtHSN(0)("IGSTPAc") Select data("slno"))
                        slno = 0
                        For Each itm In a
                            slno = itm
                        Next
                        If slno = 0 Then
                            dtrow = dtTax.NewRow
                            dtrow("slno") = dtTax.Rows.Count + 1
                            dtrow("AccountName") = dtHSN(0)("IGSTPAname")
                            dtrow("ACid") = dtHSN(0)("IGSTPAc")
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
        Dim dt As DataTable
        With grdVoucher
            If Not calculatefromGrid Then
                dt = _objcmnbLayer._fldDatatable("SELECT * FROM GSTTb WHERE HSNCODE='" & hsncode & "'")
                If dt.Rows.Count > 0 Then
                    .Item(ConstCGSTP, i).Value = Format(IIf(IsDBNull(dt(0)("CGST")), 0, CDbl(dt(0)("CGST"))), lnumformat)
                    .Item(ConstSGSTP, i).Value = Format(IIf(IsDBNull(dt(0)("SGST")), 0, CDbl(dt(0)("SGST"))), lnumformat)
                    .Item(ConstIGSTP, i).Value = Format(IIf(IsDBNull(dt(0)("IGST")), 0, CDbl(dt(0)("IGST"))), lnumformat)
                Else
                    .Item(ConstCGSTP, i).Value = Format(0, lnumformat)
                    .Item(ConstSGSTP, i).Value = Format(0, lnumformat)
                    .Item(ConstIGSTP, i).Value = Format(0, lnumformat)
                End If
            End If
            Dim actualPrice As Double
            actualPrice = (CDbl(.Item(ConstActualPrice, i).Value) * CDbl(.Item(ConstQty, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value)
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
            .Item(ConstMRP, i).Value = DR(0)("MRP")
            .Item(ConstSP1, i).Value = DR(0)("UnitPrice")
            .Item(ConstSP2, i).Value = DR(0)("secondPrice")
            .Item(ConstSP3, i).Value = DR(0)("UnitPriceWS")
            .Item(ConstWarrentyExpiry, i).Value = Format(DateAdd(DateInterval.Year, 1, DateValue(cldrdate.Value)), DtFormat)
            'If CDbl(.Item(ConstUPrice, i).Value) = 0 Or chgItm Then
            '    .Item(ConstActualPrice, i).Value = getPurchAmt(DR(0)("CostAvg"), Val(txtMoutAlias.Tag), Val(.Item(ConstItemID, .CurrentCell.RowIndex).Value))
            '    .Item(ConstUPrice, i).Value = Format(CDbl(.Item(ConstActualPrice, i).Value), lnumformat)
            'End If
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
            If Not IsDBNull(DR(0)("ismanufacturing")) Then
                .Item(ConstIsManufacturingItem, i).Value = IIf(DR(0)("ismanufacturing"), 1, 0)
            Else
                .Item(ConstIsManufacturingItem, i).Value = 0
            End If
            If Val(.Item(ConstIsManufacturingItem, i).Value) = 1 Then
                getRawmaterial(i, 0)
            End If
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
    'Private Sub AddDetails(ByVal DR As DataTable)
    '    Dim i As Integer
    '    Dim PMult As Double
    '    With grdVoucher
    '        chgbyprg = True
    '        PMult = 1
    '        'PMult = IIf(IsDBNull(DR(0)("VUp) Or DR(0)("VUp = 0, 1, DR(0)("VUp) / IIf(IsDBNull(DR(0)("VDown) Or DR(0)("VDown = 0, 1, DR(0)("VDown)
    '        i = .CurrentRow.Index ' .RowCount - 1
    '        '.Rows.Add(1)
    '        .Item(ConstSlNo, i).Value = i + 1
    '        'UPerPack = 1
    '        'UPerPack = IIf(DR("PFraction") = 0 Or IsDBNull(DR("PFraction")) Or (DR("ItemId") = DR("BaseId")), 1, DR("PFraction"))
    '        '.Item(ConstBarcode, i).Value = IIf(IsDBNull(DR("BarCode")), Trim(DR("BarCode") & ""), DR("BarCode"))
    '        .Item(ConstItemCode, i).Value = IIf(IsDBNull(DR(0)("Item Code")), Trim(DR(0)("Item Code") & ""), DR(0)("Item Code"))
    '        .Item(ConstDescr, i).Value = DR(0)("Description")
    '        .Item(ConstQty, i).Value = Format(1, lnumformat) 'IIf(IsReturn, -1, 1)
    '        .Item(ConstItemID, i).Value = DR(0)("ItemId")
    '        .Item(ConstPMult, i).Value = 2 'IIf(IsNull(sRs(0)("PFraCount), "2", sRs(0)("PFraCount)
    '        '.Item(ConstUPrice, i).Value = Format(DR(0)("unitPrice, lnumformat)
    '        If DR(0)("ItemCategory") = "Comment" Then
    '            onceChgFld = (CStr(.Item(ConstSlNo, i).Value) <> "M")
    '            .Item(ConstSlNo, i).Value = "M"
    '            .Item(ConstB, i).Value = 0
    '            .Item(ConstUnit, i).Value = ""
    '            .Item(ConstPack, i).Value = ""
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

    '        .Item(ConstPFraction, i).Value = IIf(IsDBNull(DR(0)("FraCount")), "2", DR(0)("FraCount"))
    '        .Item(ConstUnit, i).Value = DR(0)("Unit")
    '        .Item(ConstMtrPqty, i).Value = Format(1, "#,##0.00")
    '        If CDbl(.Item(ConstUPrice, i).Value) = 0 Then
    '            .Item(ConstActualPrice, i).Value = getPurchAmt(IIf(isCust, DR(0)("unitPrice"), 0), Val(txtSuppAlias.Tag), Val(.Item(ConstItemID, .CurrentCell.RowIndex).Value))
    '            .Item(ConstUPrice, i).Value = Format(CDbl(.Item(ConstActualPrice, i).Value), "#,###.00")
    '        End If
    '        .Item(ConstLTotal, i).Value = Format(.Item(ConstQty, i).Value * CDbl(.Item(ConstUPrice, i).Value), lnumformat)
    '        .Item(ConstNUPrice, i).Value = Format(CDbl(.Item(ConstUPrice, i).Value), lnumformat)

    '        .Item(ConstQty, i).Value = Format(CDbl(.Item(ConstQty, i).Value), "#,##0" & IIf(Val(.Item(ConstPFraction, i).Value) = 0, "", "." & Strings.StrDup(CByte(.Item(ConstPFraction, i).Value), "0")))
    '        .Item(ConstImpDocId, i).Value = ""
    '        .Item(ConstImpLnId, i).Value = ""
    '        chgAmt = True
    '        chgItm = False
    '        .ClearSelection()
    '        '.Rows(i).Selected = True
    '        '.CurrentCell = .Item(ConstItemCode, i)
    '        '.FirstDisplayedScrollingRowIndex() = i
    '    End With
    '    calculate()
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
                Case ConstQty, ConstTaxP, ConstDisAmt, ConstDisP
                    '.Item(ConstLTotal, i).Value = Format((.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - (.Item(ConstQty, i).Value * .Item(ConstDisAmt, i).Value) + (.Item(ConstQty, i).Value * .Item(ConstTaxAmt, i).Value), lnumformat)
                    '.Item(ConstNUPrice, i).Value = CDbl(.Item(ConstActualPrice, i).Value) - .Item(ConstDisAmt, i).Value + .Item(ConstTaxAmt, i).Value
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
                Case ConstSP1, ConstSP2, ConstSP3
                    chgUprice = True
                    chgAmt = True
                    calculateTaxFromUnitPrice(e.RowIndex, e.ColumnIndex)
            End Select
        End With
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
                    dt = _objcmnbLayer._fldDatatable("SELECT isnull(vat,0)vat FROM InvItm" & _
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
                grdVoucher.Item(ConstqtyChg, grdVoucher.CurrentRow.Index).Value = "Chg"
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
    Private Sub ShowPanel()
        If Not _srchOnce Then
            chgbyprg = True
            Dim PopupLoc As Point
            Dim x As Integer = Width - plsrch.Width - 100
            Dim y As Integer = Height - plsrch.Height - 100
            PopupLoc = New Point(x, y)
            If plsrch.Visible = False Then
                plsrch.Location = PopupLoc
                plsrch.Visible = True
            End If
        End If
        SearchProductPanel(grdSrch, "[Item Code]", strGridSrchString, True, True)
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
                If SrchText = "" And grdVoucher.CurrentCell.ColumnIndex = ConstItemCode Then
                    GoTo nxt
                End If
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
            ElseIf e.KeyCode = Keys.F2 Then
                If plsrch.Visible Then plsrch.Visible = False
                If grdVoucher.RowCount = 0 Then Exit Sub
                Select Case grdVoucher.CurrentCell.ColumnIndex
                    Case ConstItemCode
                        If Not fMList Is Nothing Then fMList.Visible = False
                        If plsrch.Visible Then plsrch.Visible = False
                        fProductEnquiry = New ItmEnqry
                        fProductEnquiry.isproduction = True
                        fProductEnquiry.ShowDialog()
                    Case ConstSerialNo
                        'If Not fSerialno Is Nothing Then fSerialno.Close() : fSerialno = Nothing
                        'fSerialno = New AddSerialnoFrm
                        'fSerialno.txtserialno.Tag = Val(grdVoucher.Item(ConstItemID, grdVoucher.CurrentRow.Index).Value)
                        'fSerialno.cldrdate.Value = DateAdd(DateInterval.Year, 1, cldrdate.Value)
                        'fSerialno.ShowDialog()
                        showSerialNoFrom()
                    Case ConstTaxAmt, ConstTaxP
                        If EnableGST Then
                            tbgst.Visible = True
                            showItemGst(True, grdVoucher.CurrentRow.Index)
                        End If
                    Case ConstQty
                        showRawmaterials(grdVoucher.CurrentRow.Index)
                End Select
            ElseIf e.KeyCode = Keys.Escape Then
                plsrch.Visible = False
            ElseIf e.KeyCode = Keys.F3 Then
                AddRow()
            ElseIf e.KeyCode = Keys.F4 Then
                'If grdVoucher.RowCount = 0 Then Exit Sub
                'If MsgBox("Do you want to remove the row?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.No Then Exit Sub
                'grdVoucher.Rows.RemoveAt(grdVoucher.CurrentCell.RowIndex)
                RemoveRow()
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
    End Sub

    Private Sub RemoveRow()
        If grdVoucher.RowCount > 0 Then
            If MsgBox("You Are Going to Remove the Row." & vbCrLf & "Are You Sure ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then
                Exit Sub
            End If
            With grdVoucher
                deleteDtSerialNo(_objcmnbLayer.dtSerialNo, grdVoucher.Item(ConstSerialNo, grdVoucher.CurrentRow.Index).Value, Val(.Item(ConstItemID, .CurrentRow.Index).Value))
                removeRawmaterial(.CurrentRow.Index)
                .Rows.RemoveAt(.CurrentRow.Index)
                .ClearSelection()
                '.Rows(.CurrentRow.Index).Selected = True
                'MsgBox(.Item(ConstSlNo, 0).Value)
                reArrangeNo()
                calculate()
            End With

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
                    rearrangeRawmaterial(Val(.Item(ConstSlNo, r).Value), i)
                    .Item(ConstSlNo, r).Value = i
                End If
            Next r
        End With
        chgbyprg = False
    End Sub
    Private Sub rearrangeRawmaterial(ByVal slno As Integer, ByVal newslno As Integer)
        Dim i As Integer
        Dim found As Boolean
        For i = 0 To dtrawmaterial.Rows.Count - 1
            If dtrawmaterial(i)("MINslno") = slno Then
                found = True
                dtrawmaterial(i)("MINslno") = newslno
            ElseIf found = True Then
                Exit For
            End If
        Next
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
        lnumformat = numFormat
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
        txtMoutAlias.Text = ""
        txtMoutAlias.Tag = ""
        txtStock.Text = ""
        txtJob.Text = ""
        txtJob.Tag = ""
        chgDoc = False
        'fMainForm.lblUser.Text = CurrentUser
        'fMainForm.lblModiUser.Text = ""
        OthCost = 0
        numDisc.Text = Format(0, numFormat)
        OthCost = 0
        CTVol = 0
        CTWt = 0
        CTQty = 0
        txtCashSuppName.Text = ""
        loadedTrId = 0
        lblbalance.Text = Format(0, numFormat)
        lbllimit.Text = Format(0, numFormat)
        lblInvoices.Text = Format(0, numFormat)
        _objcmnbLayer.dtSerialNo.Rows.Clear()
        txtroundOff.Text = Format(0, lnumformat)
        LddImpDocs = ""
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
        getOtherAccounts()
        dtrawmaterial.Rows.Clear()
        On Error GoTo 0
    End Sub
    Private Sub enableCtrls(ByVal Eyn As Boolean)
        grdVoucher.ReadOnly = Eyn
        chgbyprg = True
        txtReference.ReadOnly = Eyn
        txtDescr.ReadOnly = Eyn
        txtMoutAlias.ReadOnly = Eyn
        'txtStock.ReadOnly = Eyn
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
                btnupdate.Tag = IIf(getRight(154, CurrentUser), 1, 0)
                btndelete.Tag = IIf(getRight(155, CurrentUser), 1, 0)
            Else
                btnupdate.Tag = 1
                btndelete.Tag = 1
            End If

        Else
            AddNewClick()
            cmbVoucherTp_SelectedIndexChanged(cmbVoucherTp, New System.EventArgs)

        End If
    End Sub
    Private Function blockInvoicing() As Boolean
        If getRight(90, CurrentUser) And Val(lbllimit.Text) > 0 Then
            If CDbl(lblbalance.Text) + CDbl(lblNetAmt.Text) > CDbl(lbllimit.Text) Then
                MsgBox("Credit Limit Exeeds! You cannot post the invoice", MsgBoxStyle.Exclamation)
                Return True
            End If
        End If
        If getRight(92, CurrentUser) Then
            If CDbl(lblInvoices.Text) > 0 Then
                MsgBox("Due Invoices Found! You cannot post the invoice", MsgBoxStyle.Exclamation)
                Return True
            End If
        End If
    End Function


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
        If txtReference.Text = "" Then
            MsgBox("Invalid Reference", MsgBoxStyle.Exclamation)
            txtReference.Focus()
            Exit Sub
        End If
        If DateValue(cldrdate.Value) <= ProtectUntil Then
            MsgBox("Voucher Date comes within Protected Date Range !!", MsgBoxStyle.Information)
            cldrdate.Focus()
            Exit Sub
        End If
        _vAcMaster = _objcmnbLayer._fldDatatable(StrAccMastSrch & " WHERE accid = " & Val(txtMIN.Tag) & " ORDER BY AccDescr")
        If _vAcMaster.Rows.Count = 0 Then
            MsgBox("Enter a valid MIN Account !!", vbExclamation)
            txtMoutAlias.Focus()
            'txtSuppAlias.Focus()
            Exit Sub
        Else
            txtMIN.Tag = _vAcMaster(0)("accid")
        End If
        If blockInvoicing() Then Exit Sub
        'If Not isModi Then
        '    If chekDuplicate() Then Exit Sub
        'End If
        'Dim dt As DataTable
        'dt = _objcmnbLayer._fldDatatable("SELECT Trid from ItmInvcmntb where TrType='MI' AND cscode=" & Val(txtMoutAlias.Tag) & " and TrRefNo='" & txtReference.Text & "' and Trid<>" & loadedTrId)
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
            'If Not BkProcess And StkUpdtd Then UpdtQty(dtInvCmnTb(0)("TrId"), True, IIf(isCust, "IS", "IP"))
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
                        'If IIf(cmbDos.Items.Count = 0 And txtDOLst.Visible And txtDOLst.Text <> "", IIf(chkStkUpdtd.Checked = True, False, Val(.Item(ConstImpDocId, i).Value) <> 0), True) And .Item(ConstSlNo, i).Value.ToString <> "L" Then UpdtQty(TrId, False, IIf(isCust, "IS", "IP"), Val(.Item(ConstActualPrice, i).Value) * FCRt / PPerU, IIf(.Item(ConstSlNo, i).Value.ToString <> "L", Val(.Item(ConstBaseID, i).Value), 0), CDbl(.Item(ConstQty, i).Value) * PPerU, CDbl(.Item(ConstMtrPqty, i).Value))
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
    Private Sub saveMout(ByVal MINTrid As Long)
        Dim dtTable As DataTable
        Dim Trid As Long
        Dim TDrAmt As Double
        If isModi Then
            dtTable = _objcmnbLayer._fldDatatable("SELECT trid FROM ItmInvCmnTb WHERE MINTrid = " & loadedTrId)
            If dtTable.Rows.Count > 0 Then
                Trid = dtTable(0)("trid")
            End If
            _objcmnbLayer._saveDatawithOutParm("delete from ItmInvTrTb WHERE trid=" & Trid)
        End If
        _objInv = New clsInvoice()
        setMOUTCmnValue(Trid, _objInv)
        Trid = Val(_objInv._saveCmn())
        _objcmnbLayer._saveDatawithOutParm("Update ItmInvCmnTb set MINTrid=" & MINTrid & " WHERE trid=" & Trid)
        For i = 0 To dtrawmaterial.Rows.Count - 1 '- 1
            If Val(dtrawmaterial(i)("ItemId")) <> 0 Then
                'If i = 7 Then
                '    MsgBox("")
                'End If
                TDrAmt = TDrAmt + CDbl(dtrawmaterial(i)("qtyused")) * CDbl(dtrawmaterial(i)("UnitCost"))
                setInvDetMOUTValue(Trid, 1, i)
                _objInv._saveDetails()
            End If
        Next
        UpdateAccountsMout(Trid, TDrAmt, 0)
    End Sub

    Private Sub saveTrans()
        Me.Cursor = Cursors.WaitCursor
        Dim TrId As Long
        Dim i As Integer
        Dim DiscAcc As Long
        Dim PPerU As Single
        Dim TDrAmt As Double
        Dim dateChanged As Boolean
        Dim qtychanged As Boolean
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
            If Not CheckNoExists(txtprefix.Text, Val(numVchrNo.Text), "MI", "Inventory") Then
                If MsgBox("Document No alreary exist! Do you want to fill next?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Exit Sub
                Else
                    numVchrNo.Text = Val(numVchrNo.Text) + 1
                    GoTo chkagain
                End If
            End If
        End If
        dtTable = _objcmnbLayer._fldDatatable("SELECT accid FROM AccMast WHERE AccSetId Like '%02%'")
        If dtTable.Rows.Count > 0 Then DiscAcc = dtTable(0)("accid")
        calculate()
        If dtTable.Rows.Count > 0 Then dtTable.Clear()
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
        _objcmnbLayer._saveDatawithOutParm("UPDATE ItmInvCmnTb SET CashCustName='" & IIf(txtCashSuppName.Text = "", txtStock.Text, txtCashSuppName.Text) & "' WHERE TRID=" & TrId)
        'to check whether date has been changed or not
        'if changed there should be calculeted cost average for all items
        dtTable = _objcmnbLayer._fldDatatable("SELECT TOP 1 * FROM ItmInvCmnTb LEFT JOIN VoucherTypeNoTb ON ItmInvCmnTb.TypeNo=VoucherTypeNoTb.vrno " & _
                                              "WHERE InvType='OUT' AND Trdate >'" & Format(DateValue(cldrdate.Value), "yyyy/MM/dd") & "'")
        If dtTable.Rows.Count > 0 Then
            dateChanged = True
        Else
            dateChanged = False
        End If

        ReDim JobAcc(0)
        JobAcc(0).Acc = Val(txtStock.Tag)
        JobAcc(0).Job = txtJob.Text
        Dim amt As Double

        If setTaxAsIncomeExpense Then
            If Val(txtroundOff.Text) > 0 Then
                amt = (CDbl(lblTotAmt.Text) + CDbl(lbltax.Text)) - (IIf(cmbsign.SelectedIndex = 1, 1, -1) * CDbl(txtroundOff.Text))
            End If
            JobAcc(0).Amt = IIf(DiscAcc = 0, CDbl(lblNetAmt.Text), amt)
        Else
            If Val(txtroundOff.Text) > 0 Then
                amt = CDbl(lblTotAmt.Text) - (IIf(cmbsign.SelectedIndex = 1, 1, -1) * CDbl(txtroundOff.Text))
            Else
                amt = CDbl(lblTotAmt.Text)
            End If
            JobAcc(0).Amt = amt
        End If

        Dim itemidsdatatable As New DataTable
        itemidsdatatable.Columns.Add(New DataColumn("Itemid", GetType(Long)))
        With grdVoucher
            For i = 0 To .Rows.Count - 1 '- 1
                If CDbl(.Item(ConstQty, i).Value) <> 0 Or .Item(ConstSlNo, i).Value.ToString = "M" Then
                    PPerU = Val(.Item(ConstPMult, i).Value) 'Switch(.TextMatrix(i, 4) = "B", 1, .TextMatrix(i, 4) = "P1", CSng(grdVoucher.TextMatrix(i, 18)), .TextMatrix(i, 4) = "P2", CSng(grdVoucher.TextMatrix(i, 19)))
                    PPerU = IIf(PPerU = 0, 1, PPerU)
                    TDrAmt = TDrAmt + CDbl(.Item(ConstQty, i).Value) * (CDbl(.Item(ConstActualPrice, i).Value) - IIf(DiscAcc = 0, CDbl(.Item(ConstDiscOther, i).Value), 0))
                    TDrAmt = TDrAmt - CDbl(.Item(ConstDisAmt, i).Value)
                    'If setTaxAsIncomeExpense Then
                    '    TDrAmt = TDrAmt + CDbl(.Item(ConstTaxAmt, i).Value)
                    'End If
                    setInvDetValue(TrId, PPerU, i)
                    _objInv._saveDetails()
                    If UCase(.Item(ConstqtyChg, i).Value) = "CHG" Then
                        qtychanged = True
                    Else
                        qtychanged = False
                    End If
                    'If (dateChanged Or (qtychanged And Val(.Item(ConstId, i).Value) > 0)) And enableRealtimeCosting And Val(.Item(ConstItemID, i).Value) > 0 Then
                    '    _objInv.ItemId = Val(.Item(ConstItemID, i).Value)
                    '    _objInv.TrDate = DateValue(cldrdate.Value)
                    '    _objInv.setcostAverageOnModification(UsrBr)
                    'End If
                End If
            Next
            If isModi Then
                _objcmnbLayer._saveDatawithOutParm("UPDATE SerialNoTb SET PurchaseId=0 WHERE SerialNo IN (SELECT SerialNo FROM ItmInvTrTb where setRemove=1 and trid=" & loadedTrId & ")")
                '_objcmnbLayer._saveDatawithOutParm("delete from SerialNoTb where ISNULL(PurchaseId,0)=0 and ISNULL(SalesId,0)=0")
                _objcmnbLayer._saveDatawithOutParm("delete from SerialNoTb where ISNULL(LastINTrId,0)=0 and ISNULL(LastOutTrid,0)=0")
                _objInv.deleteInventoryRelatedItemDetails(loadedTrId)
                itemidsdatatable = _objcmnbLayer._fldDatatable("select itemid from ItmInvTrTb where setRemove=1 and trid=" & loadedTrId)
                _objcmnbLayer._saveDatawithOutParm("delete from ItmInvTrTb where setRemove=1 and trid=" & loadedTrId)
                If itemidsdatatable.Rows.Count > 0 And enableRealtimeCosting And Not enableBatchwiseTr Then
                    For i = 0 To itemidsdatatable.Rows.Count - 1
                        _objInv.ItemId = itemidsdatatable(i)("Itemid")
                        _objInv.TrDate = DateValue(cldrdate.Value)
                        _objInv.setcostAverageOnModification(UsrBr)
                    Next
                End If
            End If
        End With
        UpdateAccounts(TrId, TDrAmt, DiscAcc)
        If Not enableRestuarent Then saveMout(TrId)
        'If Trim(LddImpDocs) <> "" Then RfrshPrssdQty(LddImpDocs)
        If isModi = False Then
            numPrintVchr.Text = numVchrNo.Text
            numVchrNo.Tag = Val(numVchrNo.Text) 'Trim(CMNREC(12).FormattedText)
            txtPPrefix.Text = txtprefix.Text
            txtprefix.Tag = txtprefix.Text
            txtprefix.Text = SetNextVrNo(numVchrNo, Val(cmbVoucherTp.Tag), "MI", "TrType = 'MI' AND InvNo = ", False, True, True)
        End If
        ChgId = False
        chgPost = False
        If isModi Then
            btnModify_Click(btnModify, New System.EventArgs)
        Else
            AddNewClick()
            cmbVoucherTp_SelectedIndexChanged(cmbVoucherTp, New System.EventArgs)
        End If
        MsgBox("Purchase Invoice is succesfully posted with Vr. # " & numPrintVchr.Text & ".", MsgBoxStyle.Information)
        numVchrNo.Tag = ""
        Me.Cursor = Cursors.Default
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

        setAcctrCmnValue(TrId, LinkNo, "MI")
        LinkNo = 0
        LinkNo = Val(_objTr.SaveAccTrCmn())
        'Debit Entry
        For j = 0 To JobAcc.Count - 1
            setAcctrDetValue(LinkNo, j)
            _objTr.saveAccTrans()
        Next

        Dim EntRef As String = Strings.Left("Manufacturing Stock IN", 249)
        Dim dlAmt As Double = TDrAmt * FCRt

        'Tax Entry Debit
        'Dim i As Integer = 0
        'If EnableGST Then
        '    For i = 0 To dtTax.Rows.Count - 1
        '        If Val(dtTax(i)("ACid")) > 0 And Val(dtTax(i)("Amount")) > 0 Then
        '            Dim TxAmount As Double = CDbl(dtTax(i)("Amount"))
        '            dlAmt = dlAmt + (TxAmount * FCRt)
        '            setAcctrDetValue(LinkNo, Val(dtTax(i)("ACid")), Trim(txtReference.Text), dtTax(i)("AccountName") & " Tax Paid", TxAmount * FCRt, Strings.Left(txtJob.Text, 50), "", 0, 0, "", "", Val(txtMoutAlias.Tag), Val(txtMoutAlias.Tag) & txtReference.Text, cmbfc.Text, FCRt)
        '            _objTr.saveAccTrans()
        '        End If
        '    Next
        'ElseIf ShowTaxOnInventory Then
        '    For i = 0 To dtTax.Rows.Count - 1
        '        If Val(dtTax(i)("paymentAC")) > 0 And Val(dtTax(i)("Amount")) > 0 Then
        '            Dim TxAmount As Double = CDbl(dtTax(i)("Amount"))
        '            dlAmt = dlAmt + (TxAmount * FCRt)
        '            setAcctrDetValue(LinkNo, Val(dtTax(i)("paymentAC")), Trim(txtReference.Text), dtTax(i)("Vatcode") & " Tax Paid", TxAmount * FCRt, Strings.Left(txtJob.Text, 50), "", 0, 0, "", "", Val(txtMoutAlias.Tag), Val(txtMoutAlias.Tag) & txtReference.Text, cmbfc.Text, FCRt)
        '            _objTr.saveAccTrans()
        '        End If
        '    Next
        'End If


        'Credit Entry
        If Val(txtroundOff.Text) > 0 Then
            dlAmt = dlAmt - (IIf(cmbsign.SelectedIndex = 1, 1, -1) * CDbl(txtroundOff.Text))
        End If
        dlAmt = dlAmt * -1

        setAcctrDetValue(LinkNo, Val(txtMIN.Tag), Trim(txtReference.Text), EntRef, dlAmt, "", Strings.Left(txtJob.Tag, 50), 0, 0, "", _
                         "", Val(txtMIN.Tag), Val(txtMIN.Tag) & txtReference.Text, cmbfc.Text, FCRt)
        _objTr.saveAccTrans()

        UpdateOtherCost(LinkNo)
        updateClosingBalanceForInvoice(TrId)
    End Sub
    Private Sub UpdateAccountsMout(ByVal TrId As Long, ByVal TDrAmt As Double, ByVal DiscAcc As Integer)
        Dim LinkNo As Long
        Dim dtTable As DataTable
        If isModi And TrId > 0 Then
            dtTable = _objcmnbLayer._fldDatatable("SELECT LinkNo FROM AccTrCmn WHERE LnkNo = " & TrId)
            If dtTable.Rows.Count > 0 Then
                LinkNo = dtTable(0)("LinkNo")
                _objcmnbLayer._saveDatawithOutParm("DELETE FROM AccTrDet WHERE LinkNo=" & LinkNo)
            End If
        End If

        setAcctrCmnValue(TrId, LinkNo, "MO")
        LinkNo = 0
        LinkNo = Val(_objTr.SaveAccTrCmn())
        'Debit Entry
        setAcctrDetValue(LinkNo, Val(txtMout.Tag), Trim(txtReference.Text), "Manufacturing Stock Out", TDrAmt, Strings.Left(txtJob.Text, 50), "", 0, 0, "", "", Val(txtMout.Tag), Val(txtMout.Tag) & txtReference.Text, cmbfc.Text, FCRt)
        _objTr.saveAccTrans()
        'Credit Entry
        setAcctrDetValue(LinkNo, Val(txtStock.Tag), Trim(txtReference.Text), "Manufacturing Stock Out", TDrAmt * -1, Strings.Left(txtJob.Text, 50), "", 0, 0, "", "", Val(txtStock.Tag), Val(txtStock.Tag) & txtReference.Text, cmbfc.Text, FCRt)
        _objTr.saveAccTrans()

        updateClosingBalanceForInvoice(TrId)
    End Sub
    Private Sub setMOUTCmnValue(ByVal InvTrid As Long, ByVal _objInv As clsInvoice)
        With _objInv
            Dt = DateValue(Date.Now)
            .TrId = InvTrid
            .TrDate = DateValue(cldrdate.Value)
            .TrType = "MO"
            .DocLstTxt = ""
            .Prefix = Trim(txtprefix.Text)
            .InvNo = Val(numVchrNo.Text)
            .TrRefNo = Trim(txtReference.Text)
            .CSCode = Val(txtMout.Tag)
            .PSAcc = Val(txtStock.Tag)
            .JobCode = txtJob.Text
            .ImpDoc = 0
            .UserId = CurrentUser
            .IsFC = 0
            .FCRate = FCRt
            .NFraction = NDec
            .FC = cmbfc.Text
            .Discount = 0 'CDbl(numDisc.Text) * FCRt
            .TrDescription = Trim(txtDescr.Text)
            .TypeNo = getVouchernumber("MO")
            .EnaJob = False
            .DocDefLoc = ""
            .BrId = ActBr
            .OthCost = 0 'CDbl(lblOthCost.Text)
            .Discount1 = 0
            .NetAmt = 0 'CDbl(lblNetAmt.Text) * FCRt
            .LPO = ""
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
            .rndoff = 0 ' txtroundOff.Text * IIf(cmbsign.SelectedIndex = 1, -1, 1)
            .TaxType = 0 'Val(lblstatecode.Tag)
            .InvTypeNo = 0 'Val(cmbVoucherTp.Tag)
        End With

    End Sub
    Private Sub setInvCmnValue(ByVal InvTrid As Long)
        With _objInv
            Dt = DateValue(Date.Now)
            .TrId = InvTrid
            .TrDate = DateValue(cldrdate.Value)
            .TrType = "MI"
            .DocLstTxt = ""
            .Prefix = Trim(txtprefix.Text)
            .InvNo = Val(numVchrNo.Text)
            .TrRefNo = Trim(txtReference.Text)
            .CSCode = Val(txtMIN.Tag)
            .PSAcc = Val(txtStock.Tag)
            .JobCode = txtJob.Text
            .ImpDoc = 0
            .UserId = CurrentUser
            .IsFC = 0
            .FCRate = FCRt
            .NFraction = NDec
            .FC = cmbfc.Text
            .Discount = 0 'CDbl(numDisc.Text) * FCRt
            .TrDescription = Trim(txtDescr.Text)
            .TypeNo = getVouchernumber("MI")
            .EnaJob = False
            .DocDefLoc = ""
            .BrId = ActBr
            .OthCost = CDbl(lblOthCost.Text)
            .Discount1 = 0
            .NetAmt = CDbl(lblNetAmt.Text) * FCRt
            .LPO = ""
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
            .TaxType = 0 'Val(lblstatecode.Tag)
            .InvTypeNo = Val(cmbVoucherTp.Tag)
        End With

    End Sub
    Private Sub setInvDetValue(ByVal Invid As Long, ByVal PPerU As Single, ByVal i As Integer)

        With grdVoucher
            _objInv.dtTrId = Invid
            _objInv.ItemId = IIf(.Item(ConstSlNo, i).Value.ToString <> "L", Val(.Item(ConstItemID, i).Value), 0)
            _objInv.Unit = .Item(ConstUnit, i).Value
            _objInv.taxP = CDbl(.Item(ConstTaxP, i).Value)
            _objInv.taxAmt = CDbl(.Item(ConstTaxAmt, i).Value) * FCRt / PPerU
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
            _objInv.TrTypeNo = getVouchernumber("MI")
            _objInv.TrDateNo = getDateNo(CDate(cldrdate.Value))
            _objInv.TrType = "MI"
            _objInv.id = Val(.Item(ConstId, i).Value)
            _objInv.WarrentyName = .Item(ConstLocation, i).Value
            If Trim(.Item(ConstSerialNo, i).Value & "") = "" And enableBatchwiseTr Then
                _objInv.SerialNo = "MI" & txtprefix.Text & numVchrNo.Text
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
    Private Sub setInvDetMOUTValue(ByVal Invid As Long, ByVal PPerU As Single, ByVal i As Integer)

        With grdVoucher
            _objInv.dtTrId = Invid
            _objInv.ItemId = dtrawmaterial(i)("ItemId")
            _objInv.Unit = dtrawmaterial(i)("Unit")
            _objInv.taxP = 0
            _objInv.taxAmt = 0
            _objInv.TrQty = dtrawmaterial(i)("qtyused")
            _objInv.UnitCost = dtrawmaterial(i)("UnitCost")
            _objInv.PFraction = PPerU

            _objInv.UnitOthCost = 0
            _objInv.Method = 1
            _objInv.UnitDiscount = 0
            _objInv.ItemDiscount = 0
            _objInv.DisP = 0
            _objInv.IDescription = dtrawmaterial(i)("itemname")

            _objInv.SlNo = i + 1
            _objInv.TrTypeNo = getVouchernumber("MO")
            _objInv.TrDateNo = getDateNo(CDate(cldrdate.Value))
            _objInv.TrType = "MO"
            If isModi Then
                _objInv.id = Val(dtrawmaterial(i)("id"))
            Else
                _objInv.id = 0
            End If
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
            _objInv.MINslno = dtrawmaterial(i)("MINslno")
        End With
    End Sub
    Private Sub setAcctrCmnValue(ByVal InvTrid As Long, ByVal LinkNo As Long, ByVal Trtype As String)
        _objTr.JVType = Trtype
        _objTr.JVDate = DateValue(cldrdate.Value)
        _objTr.PreFix = Trim(txtprefix.Text)
        _objTr.JVNum = Val(numVchrNo.Text)
        _objTr.JVTypeNo = getVouchernumber(Trtype)
        _objTr.UserId = Trim(CurrentUser & "")
        _objTr.MchName = MACHINENAME
        _objTr.CrtDtTm = Dt
        _objTr.TypeNo = getVouchernumber(Trtype)
        '_objTr.VrDescr = Trim(txtpayee.Text)
        _objTr.IsModi = IIf(LinkNo > 0, 1, 0)
        _objTr.LinkNo = InvTrid
    End Sub
    Private Sub setAcctrDetValue(ByVal lnkNo As Long, ByVal jbIndex As Integer)
        With _objTr
            .trLinkNo = lnkNo
            .AccountNo = JobAcc(jbIndex).Acc
            .Reference = Trim(txtReference.Text)
            .EntryRef = Strings.Left("Manufacturing Stock IN", 249)
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
            .CustAcc = Val(txtMoutAlias.Tag)
            .AccWithRef = JobAcc(jbIndex).Acc & txtReference.Text
            .DueDate = clrDuedate.Value
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
            .Reference = Reference
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
            Dim dtDue As Date = clrDuedate.Value
            Dim dtSup As Date = DateValue(cldrdate.Value)
            .DocDate = dtLPO
            .SuppInvDate = dtSup
            .DueDate = dtDue
        End With
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

    Private Sub PurchaseInvoiceFrm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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
            'btnNext_Click(btnNext, New System.EventArgs())
            If ShowTaxOnInventory Then
                dtTax = _objcmnbLayer._fldDatatable("SELECT vatcode,convert(money, 0) Amount,collectionAC,paymentAC,vat From VatMasterTb", False)
            ElseIf EnableGST Then
                CreateTaxTable(dtTax)
            End If
            _objcmnbLayer.dtSerialNo = createdtSerialNo()
            SetGridHead()
            setOtherCostHead()
            crtSubVrs(cmbVoucherTp, 11, True)
            getOtherAccounts()
            If Val(txtStock.Tag) = 0 Then
                MsgBox("Not found default stock account! set stock account in default settings before save this voucher", MsgBoxStyle.Exclamation)
            End If
            FCRt = 1
            OthCost = 0
            chgbyprg = True
            createRawmaterialTable()
            'calculate()
            Text = "Store Receipt "
            cldrdate.Value = Format(Date.Now, DtFormat)
            LodCurrency()
            NDec = NoOfDecimal
            loadedTrId = 0
            chgbyprg = False
            'cmbVrType_SelectedIndexChanged(sender, e)
            'numVchrNo.Select()
            ChgId = False
            If isModi Then
                btnModify_Click(btnModify, New System.EventArgs)
                If userType Then
                    btnupdate.Tag = IIf(getRight(154, CurrentUser), 1, 0)
                    btndelete.Tag = IIf(getRight(155, CurrentUser), 1, 0)
                Else
                    btnupdate.Tag = 1
                    btndelete.Tag = 1
                End If
                btndelete.Text = "Delete"
            Else
                AddNewClick()
                NextNumber()
                If userType Then
                    btnupdate.Tag = IIf(getRight(153, CurrentUser), 1, 0)
                Else
                    btnupdate.Tag = 1
                End If
                btndelete.Text = "Clear"
                btndelete.Tag = 1
            End If
            Timer1.Enabled = True
            If enableRestuarent Then btnmenuitems.Visible = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function JobAssianable(ByVal dtMasteer As DataTable) As Boolean
        JobAssianable = True
        If dtMasteer(0)("JobAssgble") Then
            If dtMasteer(0)("ForJobYN") Then
                If txtJob.Text = "" Then
                    MsgBox("A/c. [" & txtStock.Text & "] is refered to Job. Entry should need a value !", MsgBoxStyle.Information)
                    txtStock.Focus()
                    JobAssianable = False
                    Exit Function
                End If
            End If
        Else
            If txtJob.Text <> "" Then
                If MsgBox("A/c. [" & txtStock.Text & "] is not Job Assignable." & Chr(13) & Chr(10) & "But found some Job Entry and it will remove.  Are you sure !?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    txtMoutAlias.Focus()
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
        dtTable = _objcmnbLayer._fldDatatable("SELECT * FROM ItmInvCmnTb WHERE TrType = 'MI' AND InvNo = " & Val(numVchrNo.Text) & " AND PreFix = '" & MkDbSrchStr(Trim(txtprefix.Text), True) & "'")
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
            'dtSrlNo = _objcmnbLayer._fldDatatable("SELECT SerialNo,itemid FROM ItmInvTrTb Left Join ItmInvCmnTb on ItmInvTrTb.Trid=ItmInvCmnTb.trid WHERE TrType='MI' AND ItmInvCmnTb.Trid<>" & loadedTrId)
            'Dim _qurey As EnumerableRowCollection(Of DataRow)

            For r = 0 To .RowCount - 1 '- 1
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
                If .Item(ConstIsSerial, r).Value = 1 And .Item(ConstSerialNo, r).Value = "" Then
                    .Rows(r).Selected = True
                    .CurrentCell = .Item(ConstQty, r)
                    MsgBox("Serial Number cannot be Blank !!", vbExclamation)
                    .FirstDisplayedScrollingRowIndex = r
                    GoTo Ter
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
                'If CDbl(.Item(ConstQty, r).Value) = 0 Then
                '    .Rows(r).Selected = True
                '    .CurrentCell = .Item(ConstQty, r)
                '    MsgBox("Zero Quantity !!", vbExclamation)
                '    .FirstDisplayedScrollingRowIndex = r
                '    GoTo Ter
                'End If
                If Not enableRestuarent Then
                    If CDbl(.Item(ConstUPrice, r).Value) = 0 Then
                        .Rows(r).Selected = True
                        .CurrentCell = .Item(ConstUPrice, r)
                        If MsgBox("Entered Price/Unit of Item [" & .Item(ConstItemCode, r).Value & "] is zero !!  Proceed ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo) = vbNo Then
                            .FirstDisplayedScrollingRowIndex() = r
                            GoTo Ter
                        End If
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


    Private Sub numDisc_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles numDisc.KeyPress, txtfcrt.KeyPress, numOtherAmt.KeyPress, txtroundOff.KeyPress
        NumericTextOnKeypress(sender, e, chgbyprg, lnumformat)
    End Sub

    Private Sub numDisc_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numDisc.TextChanged, txtroundOff.TextChanged
        If chgbyprgN Then Exit Sub
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


    Private Sub txtSuppAlias_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMoutAlias.Validated, txtStock.Validated
        If txtMoutAlias.Text = "" Then Exit Sub
        setSupplier()
    End Sub
    Private Sub setSupplier(Optional ByVal accid As Long = 0)
        Dim dt As DataTable
        If accid > 0 Then
            dt = _objcmnbLayer._fldDatatable("SELECT AccMast.accid,Alias,AccDescr,Address1,Address2,Address3,Address4," & _
                                         "Phone,Fax,EMail,PT1,PT2,PT3,PT4,TrdLcno,TrdDate,CreditLimit,DueDays,SlsmanId,CurrencyCode,CountryCode " & _
                                         " from AccMast LEFT JOIN AccMastAddr ON AccMast.accid=AccMastAddr.AccountNo where accid=" & accid)
        Else
            dt = _objcmnbLayer._fldDatatable("SELECT AccMast.accid,Alias,AccDescr,Address1,Address2,Address3,Address4," & _
                                         "Phone,Fax,EMail,PT1,PT2,PT3,PT4,TrdLcno,TrdDate,CreditLimit,DueDays,SlsmanId,CurrencyCode,CountryCode " & _
                                         " from AccMast LEFT JOIN AccMastAddr ON AccMast.accid=AccMastAddr.AccountNo where Alias='" & txtMoutAlias.Text & "'")
        End If

        If dt.Rows.Count > 0 Then
            txtMoutAlias.Tag = dt(0)("accid")
            lbladd1.Text = Trim("" & dt(0)("Address1"))
            lbladd2.Text = Trim("" & dt(0)("Address2"))
            lbladd3.Text = Trim("" & dt(0)("Address3"))
            lbladd4.Text = Trim("" & dt(0)("Address4"))
            lbladd5.Text = Trim("" & dt(0)("Phone"))
            lbladd6.Text = Trim("" & dt(0)("Email"))
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
            If accid > 0 Then
                txtMoutAlias.Text = Trim("" & dt(0)("Alias"))
                txtStock.Text = Trim("" & dt(0)("AccDescr"))
            End If
            If EnableGST Then CalculateGST()
            If Val(dt(0)("CreditLimit") & "") = 0 Then
                dt(0)("CreditLimit") = 0
            End If
            lbllimit.Text = Format(Val(dt(0)("CreditLimit")), lnumformat)
            Dim iNBal As Double = getAccBal(Val(txtMoutAlias.Tag))
            lblbalance.Text = Strings.Format(iNBal, lnumformat)
            If IsDBNull(dt(0)("DueDays")) Then
                dt(0)("DueDays") = 0
            End If
            If Val(dt(0)("DueDays") & "") > 0 Then
                iNBal = getAccAegBal(Val(txtMoutAlias.Tag), DateValue(DateTime.Now), Val(dt(0)("DueDays")))
                lblInvoices.Text = Strings.Format(iNBal, lnumformat)
                clrDuedate.Value = DateAdd(DateInterval.Day, Val(dt(0)("DueDays")), cldrdate.Value)
            End If
        Else
            txtMoutAlias.Text = ""
            txtStock.Text = ""
            lbladd1.Text = ""
            lbladd2.Text = ""
            lbladd3.Text = ""
            lbladd4.Text = ""
            lbladd5.Text = ""
            lbladd6.Text = ""
            lbladd7.Text = ""
            lbltrdate.Text = ""
            lblInvoices.Text = Format(0, lnumformat)
            lblbalance.Text = Format(0, lnumformat)
            lbllimit.Text = Format(0, lnumformat)
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
            .strType = "MI"
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

    Private Sub txtDescr_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDescr.TextChanged, txtCashSuppName.TextChanged
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
            fRptFormat.RptType = "MI"
            fRptFormat.ShowDialog()
            fRptFormat = Nothing
        Else
            PrepareRpt("MI")
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

            Dim itemidsdatatable As New DataTable
            Dim trdate As Date

            itemidsdatatable = _objcmnbLayer._fldDatatable("SELECT Itemid,TrDate,SerialNo from ItmInvTrTb LEFT JOIN ItmInvCmnTb ON ItmInvTrTb.Trid=ItmInvCmnTb.Trid  WHERE ItmInvTrTb.TrId =" & loadedTrId)
            If itemidsdatatable.Rows.Count > 0 Then
                trdate = DateValue(itemidsdatatable(0)("TrDate"))

                _objInv.TrId = loadedTrId
                _objInv.TrType = "IN"
                _objInv.deleteInventoryTransactions()
                For i = 0 To itemidsdatatable.Rows.Count - 1
                    _objcmnbLayer._saveDatawithOutParm("UPDATE SerialNoTb SET PurchaseId=0 WHERE SerialNo='" & itemidsdatatable(i)("SerialNo") & "'")
                    _objcmnbLayer._saveDatawithOutParm("delete from SerialNoTb where ISNULL(PurchaseId,0)=0 and ISNULL(SalesId,0)=0")

                    _objInv.ItemId = itemidsdatatable(i)("Itemid")
                    _objInv.TrDate = DateValue(itemidsdatatable(i)("TrDate"))
                    _objInv.setcostAverageOnModification(UsrBr)
                Next
            End If
            deleteOutTr(loadedTrId)
            btnModify_Click(btnModify, New System.EventArgs)
        End If

    End Sub
    Private Sub deleteOutTr(ByVal trid As Long)
        Dim itemidsdatatable As New DataTable
        Dim trdate As Date
        Dim outTrid As Long
        itemidsdatatable = _objcmnbLayer._fldDatatable("SELECT Itemid,TrDate,SerialNo,ItmInvCmnTb.trid from ItmInvTrTb LEFT JOIN ItmInvCmnTb ON ItmInvTrTb.Trid=ItmInvCmnTb.Trid  WHERE MINTrid =" & trid)
        If itemidsdatatable.Rows.Count > 0 Then
            trdate = DateValue(itemidsdatatable(0)("TrDate"))
            outTrid = itemidsdatatable(0)("Trid")
            _objInv.TrId = outTrid
            _objInv.TrType = "OUT"
            _objInv.deleteInventoryTransactions()
            For i = 0 To itemidsdatatable.Rows.Count - 1
                _objInv.ItemId = itemidsdatatable(i)("Itemid")
                _objInv.TrDate = DateValue(itemidsdatatable(i)("TrDate"))
                _objInv.setcostAverageOnModification(UsrBr)
            Next
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
            txtMoutAlias.Text = dt(0)("Alias")
            txtStock.Text = dt(0)("AccDescr")
            txtMoutAlias.Tag = dt(0)("accid")
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
        _objInv.TrType = "MI"
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
        AddRow()
        grdVoucher.CurrentCell = grdVoucher.Item(ConstItemCode, grdVoucher.CurrentRow.Index)
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
                    cost = getPurchAmt(dt(0)("LastPurchCost"), Val(txtMoutAlias.Tag), Val(.Item(ConstItemID, .CurrentCell.RowIndex).Value))
                    cost = cost * Val(.Item(ConstPMult, .CurrentCell.RowIndex).Value)
                    .Item(ConstActualPrice, .CurrentCell.RowIndex).Value = cost
                    .Item(ConstUPrice, .CurrentCell.RowIndex).Value = Format(cost, lnumformat)
                    calculate()
                End If
            End With
        End If
    End Sub


    Private Sub TabPage1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub grdVoucher_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellEndEdit
        Dim col As Integer = e.ColumnIndex
        Dim ndec1 As Integer
        If col = ConstQty Or col = ConstUPrice Or col = ConstDisAmt Or col = ConstLTotal Or col = ConstTaxP Or col = ConstDisP _
        Or col = ConstMRP Or col = ConstSP1 Or col = ConstSP2 Or col = ConstSP3 Then
            If col = ConstQty Then
                ndec1 = grdVoucher.Item(ConstPFraction, grdVoucher.CurrentRow.Index).Value
            Else
                ndec1 = NDec
            End If
            If Val(grdVoucher.Item(col, e.RowIndex).Value) = 0 Then grdVoucher.Item(col, e.RowIndex).Value = 0
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
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub cldrdate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cldrdate.KeyDown
        If e.KeyCode = Keys.Return Then
            txtStock.Focus()
        End If

    End Sub

    Private Sub cldrdate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cldrdate.ValueChanged
        chgPost = True
    End Sub

    Private Sub cmbVoucherTp_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVoucherTp.SelectedIndexChanged
        NextNumber()
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
            NDec = 2
        End If
    End Sub

    Private Sub txtfcrt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtfcrt.TextChanged

    End Sub

    Private Sub txtfcrt_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtfcrt.Validated
        FCRt = txtfcrt.Text
    End Sub

    Private Sub btntax_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btntax.Click
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
        txtdebit.Text = txtStock.Text
        txtdebit.Tag = Val(txtStock.Tag)
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
        sRs = _objcmnbLayer._fldDatatable("SELECT AccTrDet.*, AccDescr, Alias, BranchId FROM AccTrDet LEFT JOIN AccMast ON AccMast.accid = AccTrDet.AccountNo WHERE  LinkNo IN (SELECT LinkNo FROM AccTrCmn WHERE JVType = 'MI' AND JVNum = " & Val(numVchrNo.Text) & " AND PreFix = '" & MkDbSrchStr(Trim(txtprefix.Text)) & "') AND OthCost > 0  ORDER BY OthCost, DealAmt DESC")
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

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub grdOtherCost_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdOtherCost.CellContentClick

    End Sub

    Private Sub btnothadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnothadd.Click
        If Val(txtdebit.Tag) = 0 Then
            MsgBox("Invalid Debit", MsgBoxStyle.Exclamation, Nothing)
            txtMIN.Focus()
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
                                        "", Val(txtMoutAlias.Tag), Val(.Item(CostDrAcc, i).Value) & Trim(.Item(CostReference, i).Value), .Item(CostFCName, i).Value, IIf(.Item(CostFCName, i).Value = "", 1, CDbl(.Item(CostFCRate, i).Value)))
                        _objTr.saveAccTrans()

                        'Credit
                        setAcctrDetValue(LinkNo, Val(.Item(CostCrAcc, i).Value), Trim(.Item(CostReference, i).Value), Trim(.Item(CostDescr, i).Value), CDbl(.Item(CostAmount, i).Value) * -1 * FCRt, txtJob.Text, "", 1, c, "", _
                                         "", Val(txtMoutAlias.Tag), Val(.Item(CostCrAcc, i).Value) & Trim(.Item(CostReference, i).Value), .Item(CostFCName, i).Value, IIf(.Item(CostFCName, i).Value = "", 1, CDbl(.Item(CostFCRate, i).Value)))
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


    Private Sub btncancelgst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancelgst.Click
        tbgst.Visible = False
    End Sub

    Private Sub txtCgst_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCgst.KeyDown, txtSgst.KeyDown, txtIgst.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtCgst_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCgst.KeyPress, txtSgst.KeyPress, txtIgst.KeyPress
        NumericTextOnKeypress(sender, e, chgbyprg, lnumformat)
    End Sub

    Private Sub txtCgst_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCgst.TextChanged
        If chgbyprgN Then Exit Sub
        If Val(txtCgst.Tag) > 0 Then
            If Val(txtCgst.Text) = 0 Then txtCgst.Text = 0
            txtCgstAmt.Text = Format((CDbl(txtCgst.Tag) * CDbl(txtCgst.Text)) / 100, lnumformat)
        End If
    End Sub

    Private Sub txtSgst_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSgst.TextChanged
        If chgbyprgN Then Exit Sub
        If Val(txtCgst.Tag) > 0 Then
            If Val(txtSgst.Text) = 0 Then txtSgst.Text = 0
            txtSgstAmt.Text = Format((CDbl(txtCgst.Tag) * CDbl(txtSgst.Text)) / 100, lnumformat)
        End If
    End Sub

    Private Sub txtIgst_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIgst.TextChanged
        If chgbyprgN Then Exit Sub
        If Val(txtCgst.Tag) > 0 Then
            If Val(txtIgst.Text) = 0 Then txtIgst.Text = 0
            txtIgstAmt.Text = Format((CDbl(txtCgst.Tag) * CDbl(txtIgst.Text)) / 100, lnumformat)
        End If
    End Sub

    Private Sub btnAddgst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddgst.Click
        setGstToGrdFromTabC()
    End Sub

    Private Sub grdVoucher_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.Click
        If chgbyprg Then Exit Sub
        If grdVoucher.RowCount = 0 Then Exit Sub
        If tbgst.Visible Then
            showItemGst(False, grdVoucher.CurrentRow.Index)
        End If
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
                    .detId = Val(grdVoucher.Item(ConstId, grdVoucher.CurrentRow.Index).Value)
                    .rowIndex = grdVoucher.CurrentCell.RowIndex + 1
                    .ShowDialog()
                End With

            End If
        End If
    End Sub

    Private Sub grdVoucher_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellEnter
        'showSerialNoFrom()
    End Sub

    Private Sub grdVoucher_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellContentClick

    End Sub

    Private Sub cmbsign_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbsign.SelectedIndexChanged
        calculate()
    End Sub

    Private Sub btnfind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnfind.Click
        Dim rindex As Integer
        Dim getValue As String
        getValue = SearchSequenceFromGrid(grdVoucher, ConstDescr, txtSeq.Text, Val(btnfind.Tag) + 1)
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
    Private Sub getOtherAccounts()
        chgbyprg = True
        Dim dtTable As DataTable
        dtTable = _objcmnbLayer._fldDatatable("SELECT accid,AccDescr,Alias FROM AccMast WHERE AccSetId Like '%01%'")
        If dtTable.Rows.Count > 0 Then
            txtStock.Text = dtTable(0)("AccDescr")
            txtStock.Tag = dtTable(0)("accid")
        End If
        chgbyprg = False
    End Sub
    Private Sub createRawmaterialTable()
        dtrawmaterial = _objcmnbLayer._fldDatatable("Select '' itemcode,'' itemname, Unit,TrQty,0.000 qtyused,UnitCost,PFraction,ItemId,id,MINslno from ItmInvTrTb where 1=2")
    End Sub
    Private Sub getRawmaterial(ByVal rowIndex As Integer, ByVal trid As Long)
        Dim dt As DataTable
        Dim _objitem As New clsItemMast_BL
        'trid = 0

        If trid > 0 Then
            If dtrawmaterial.Rows.Count > 0 Then dtrawmaterial.Clear()
            dt = _objitem.returnRawmaterialFromTransaction(trid)
        Else
            dt = _objitem.returnRawMaterial(Val(grdVoucher.Item(ConstItemID, rowIndex).Value))
        End If

        Dim dtrow As DataRow
        Dim i As Integer
        Dim ttlcost As Double
        For i = 0 To dt.Rows.Count - 1
            If Val(dt(i)("Ritemid") & "") = 0 Then GoTo nxt
            dtrow = dtrawmaterial.NewRow
            dtrow("Itemcode") = dt(i)("Item Code")
            dtrow("itemname") = dt(i)("Description")
            dtrow("Unit") = dt(i)("Unit")
            dtrow("TrQty") = dt(i)("Rawqty")
            If trid > 0 Then
                dtrow("qtyused") = dt(i)("TrQty")
                dtrow("MINslno") = dt(i)("SlNo")
            Else
                dtrow("qtyused") = dt(i)("Rawqty")
                dtrow("MINslno") = rowIndex + 1
            End If
            dtrow("UnitCost") = dt(i)("CostAvg")
            dtrow("PFraction") = dt(i)("FraCount")
            dtrow("ItemId") = dt(i)("Ritemid")

            If trid > 0 Then
                dtrow("id") = 0
                'dtrow("id") = dt(i)("rawmetid")
            Else
                dtrow("id") = 0
                If IsDBNull(dt(i)("Rawqty")) Then dt(i)("Rawqty") = 0
                If IsDBNull(dt(i)("CostAvg")) Then dt(i)("CostAvg") = 0
                ttlcost = ttlcost + (CDbl(dt(i)("CostAvg")) * CDbl(dt(i)("Rawqty")))
                grdVoucher.Item(ConstActualPrice, rowIndex).Value = ttlcost
                grdVoucher.Item(ConstUPrice, rowIndex).Value = Format(ttlcost, lnumformat)
            End If
            dtrawmaterial.Rows.Add(dtrow)
nxt:
        Next
        'If EnableGST Then getGSTDetails(Trim(grdVoucher.Item(ConstBarcode, rowIndex).Value & ""), rowIndex, True)
        'If trid = 0 Then calculate()
    End Sub
    Private Sub calculateRawmaterials(ByVal rowindex As Integer)
        Dim found As Boolean
        For i = 0 To dtrawmaterial.Rows.Count - 1
            If rowindex + 1 = dtrawmaterial(i)("MINslno") Then
                found = True
                dtrawmaterial(i)("qtyused") = dtrawmaterial(i)("TrQty") * CDbl(grdVoucher.Item(ConstQty, rowindex).Value)
            ElseIf found = True Then
                Exit For
            End If
        Next
    End Sub
    Private Sub showRawmaterials(ByVal rowindex As Integer)
        Dim bDatatable As DataTable
        Dim _qurey = From data In dtrawmaterial.AsEnumerable() Where data("MINslno") = rowindex + 1 Select data
        If _qurey.Count > 0 Then
            bDatatable = _qurey.CopyToDataTable()
        Else
            bDatatable = dtrawmaterial.Clone
        End If
        Dim frm As New ItemRawMaterialFrm
        frm.dtRawmeaterial = bDatatable
        frm.viewItems()
        frm.ShowDialog()
    End Sub
    Private Sub removeRawmaterial(ByVal rowindex As Integer)
        Dim _qurey = From data In dtrawmaterial.AsEnumerable() Where data("MINslno") <> rowindex + 1 Select data
        If _qurey.Count > 0 Then
            dtrawmaterial = _qurey.CopyToDataTable()
        Else
            dtrawmaterial.Rows.Clear()
        End If
    End Sub

    Private Sub btnmenuitems_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnmenuitems.Click
        loadMenuItems()
    End Sub
    Private Sub loadMenuItems()
        Dim dt As DataTable
        Dim dtItms As DataTable
        Dim strQry As String
        dt = _objcmnbLayer._fldDatatable("SELECT InvItm.itemid FROM InvItm " & _
                                         "LEFT JOIN InvItmPropertiesTb ON InvItmPropertiesTb.Itemid=invitm.itemid " & _
                                         "  WHERE itemCategory='Menu Item' and isnull(ismanufacturing,0)<>0")
        Dim i As Integer
        grdVoucher.Rows.Clear()
        For i = 0 To dt.Rows.Count - 1
            With grdVoucher
                strQry = "SELECT InvItm.*, FraCount,vat,isSerialNo,isDuealSerialNo,ismanufacturing FROM InvItm LEFT JOIN UnitsTb ON UnitsTb.Units = InvItm.Unit" & _
                " LEFT JOIN VatMasterTb ON InvItm.vatid=VatMasterTb.vatid " & _
                "LEFT JOIN InvItmPropertiesTb ON InvItmPropertiesTb.Itemid=invitm.itemid " & _
                " WHERE InvItm.itemid=" & dt(i)("itemid")
                dtItms = _objcmnbLayer._fldDatatable(strQry)
                If dtItms.Rows.Count > 0 Then
                    .Rows.Add()
                    .CurrentCell = .Item(ConstItemCode, i)
                    AddDetails(dtItms)
                    .Item(ConstQty, i).Value = Format(0, numFormat)
                End If

            End With
        Next
        calculate()
    End Sub
    Private Sub PrintfromBarTender()
        Dim fl As System.IO.StreamWriter
        Dim query As String
        Dim i As Integer
        Dim dt As DataTable
        Dim isTaxPrice As Boolean
        Dim unitprice As Double
        isTaxPrice = isPrintTaxPrice()
        If bartenderpath = "" Then
            bartenderpath = DPath & "\barcode.txt"
        End If
        If Not FileExists(bartenderpath) Then
            File.Copy(Application.StartupPath & "\barcode.txt", Application.StartupPath & "\barcode.txt")
        End If
        fl = My.Computer.FileSystem.OpenTextFileWriter(bartenderpath, False)
        query = "ProductName,Code,Rate,MRP,PDate,Edate,Nos"
        dt = _objcmnbLayer._fldDatatable("SELECT *,(((isnull(IGST,0)+ISNULL(vat,0))*UnitPrice)/100)+UnitPrice [Tax Price] from " & _
                                         "(Select [Item Code],Description,case when isnull(sp1,0)=0 then UnitPrice else sp1 end UnitPrice," & _
                                         "case when isnull(ItmInvTrTb.mrp,0)=0 then InvItm.mrp else ItmInvTrTb.mrp end mrp," & _
                                         "WarrentyExpDate,manufacturingdate,IGST,vat,trqty FROM InvItm" & _
                                         " LEFT JOIN GSTTb ON GSTTb.HSNCode=InvItm.HSNCode " & _
                                         " LEFT JOIN VatMasterTb ON VatMasterTb.vatid=InvItm.vatid " & _
                                         " LEFT JOIN ItmInvTrTb ON InvItm.itemid=ItmInvTrTb.itemid " & _
                                         "where trid=" & loadedTrId & ")tr")
        For i = 0 To grdVoucher.RowCount - 1
            With grdVoucher
                If dt.Rows.Count > 0 Then
                    If isTaxPrice Then
                        unitprice = dt(0)("Tax Price")
                    Else
                        unitprice = dt(0)("UnitPrice")
                    End If
                    query = query & vbCrLf & dt(i)("Description") & "," & dt(i)("Item Code") & "," & _
                    Format(CDbl(unitprice), numFormat) & "," & Format(CDbl(dt(i)("MRP")), numFormat) & "," & _
                    Format(dt(i)("manufacturingdate"), "dd/MM/yyyy") & "," & Format(dt(i)("WarrentyExpDate"), "dd/MM/yyyy") & "," & Val(dt(i)("trqty"))
                    'query = query & vbCrLf & .Item(ConstDescr, i).Value & "," & .Item(ConstItemCode, i).Value & "," & Format(CDbl(unitprice), numFormat) & "," & Val(.Item(ConstQty, i).Value)
                End If
            End With
        Next
        fl.WriteLine(query)
        fl.Close()
        Dim appstart As String = Application.StartupPath & "\BarTender\bartend.exe"
        If FileExists(appstart) = False Then
            MsgBox("Bar Code Application not found! Please contact vendor", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        dt = _objcmnbLayer._fldDatatable("SELECT barid FROM BarcodeFormatTb")
        Dim formatname As String = ""
        If dt.Rows.Count > 1 Then
            Dim frm As New SelectBarcodeFormat
            frm.ShowDialog()
            formatname = frm.cmbformat.Tag
        Else
            dt = _objcmnbLayer._fldDatatable("SELECT path FROM BarcodeFormatTb WHERE isdefalult=1")
            If dt.Rows.Count > 0 Then
                formatname = dt(0)("path")
            End If
        End If
        If formatname = "" Then
            MsgBox("Format file not found", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Dim psi As New ProcessStartInfo(appstart, formatname)
        Process.Start(psi)

    End Sub

    Private Sub btnbarcode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnbarcode.Click
        If grdVoucher.RowCount = 0 Then Exit Sub
        PrintfromBarTender()
    End Sub
End Class