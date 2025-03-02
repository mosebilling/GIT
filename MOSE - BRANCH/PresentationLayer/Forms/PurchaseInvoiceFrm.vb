Imports System.IO

Public Class PurchaseInvoiceFrm

#Region "Public Variables"
    Public isModi As Boolean
    Public isCust As Boolean
    Public chgbyprg As Boolean
#End Region
#Region "Local Variables"
    Private vtype As String

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
    Private dtItemInfo As DataTable
    Private defaultOutputOnImport As Integer
    Private defaultInputOnImport As Integer
    Private isNotSupplierAccount As Boolean
    Private DiscAcc As Long
    Private TrTypeNo As Integer
    Private isImport As Boolean
    Private FrTrId As Long
    Private cngmrp As Boolean
    Private IsLoadFromExternal As Boolean
    Private dtMultipleDebits As DataTable
    Private dtSetoffTable As DataTable
    Private diableNegativeSale As Boolean
    Private disableBelowcost As Boolean
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
    Private dtloadFromExcell As DataTable
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
    Private WithEvents fTransferFromexel As TransferiItemsFromExcel
    Private WithEvents fDolist As DOListFrm
    Private WithEvents fshowlocationqty As ShowLocationQtyFrm
    Private WithEvents fwait As WaitMessageFrm
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
            End If
            SendKeys.Send("{TAB}")
        End If
    End Sub
    Public Sub CheckNLoad(Optional ByVal FromTrId As Long = 0)

        Dim InvList As DataTable
        If FromTrId <> 0 Then
            InvList = _objcmnbLayer._fldDatatable("SELECT TrId FROM ItmInvCmnTb WHERE " & IIf(UsrBr = "", "", " Brid='" & UsrBr & "' AND") & " TrId = " & FromTrId)
        Else
            InvList = _objcmnbLayer._fldDatatable("SELECT TrId FROM ItmInvCmnTb WHERE " & IIf(UsrBr = "", "", " Brid='" & UsrBr & "' AND") & " TrType = 'IP' AND InvNo = " & Val(numVchrNo.Text) & " AND PreFix = '" & MkDbSrchStr(Trim(txtprefix.Text), True) & "'")
        End If
        If InvList.Rows.Count > 0 Then
            If isImport Then
                FrTrId = InvList(0)("TrId")
                loadWaite(3)
                isImport = False
            Else
                loadedTrId = InvList(0)("TrId")
                InvList = Nothing
                isModi = True
                loadWaite(2)
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
        Dim itemquery As String = "SELECT ItmInvTrTb.*, [Item Code],Warrentyname,isSerialNo,FraCount," & _
                                          "isnull(itemCategory,'')itemCategory,paymentAC,vat,rgcess,rgcaccount,additionalcess FROM ItmInvTrTb LEFT JOIN InvItm ON InvItm.ItemId = ItmInvTrTb.ItemId " & _
                                          " LEFT JOIN InvItmPropertiesTb ON InvItmPropertiesTb.Itemid=invitm.itemid " & _
                                          "LEFT JOIN WarrentyMastertb ON WarrentyMastertb.Wid=ItmInvTrTb.WarrentyId " & _
                                          "LEFT JOIN UnitsTb ON UnitsTb.Units=ItmInvTrTb.Unit " & _
                                          "LEFT JOIN VatMasterTb ON InvItm.vatId=VatMasterTb.vatId " & _
                                          "LEFT JOIN (select vatcode rgccode,vat rgcess, paymentAC rgcaccount,vatid rgcid from  VatMasterTb)regularcess ON InvItm.regularcessid=regularcess.rgcid " & _
                                          "WHERE TrId = " & loadedTrId & " ORDER BY SlNo"

        'ItmInvCmnTb = _objcmnbLayer._fldDatatable("SELECT ItmInvCmnTb.*,jobname,[Voucher Name],Ctgry FROM ItmInvCmnTb " & _
        '                                          "LEFT JOIN PreFixTb ON PreFixTb.id=ItmInvCmnTb.invtypeno " & _
        '                                          "LEFT JOIN JobTb ON ItmInvCmnTb.[Job Code]=JobTb.jobcode WHERE TrId = " & loadedTrId & " AND TrType = 'IP'")
        Dim dtset As DataSet = _objcmnbLayer._ldDataset("SELECT ItmInvCmnTb.*,jobname,[Voucher Name],Ctgry,isnull(SalesmanTb.accountno,0) Smanacc,Alias,AccDescr,isnull(linkno,0)linkno " & _
                                                  "FROM ItmInvCmnTb " & _
                                                  "LEFT JOIN PreFixTb ON PreFixTb.id=ItmInvCmnTb.invtypeno " & _
                                                  "LEFT JOIN JobTb ON ItmInvCmnTb.[Job Code]=JobTb.jobcode " & _
                                                  "LEFT JOIN SalesmanTb ON SalesmanTb.SManCode=ItmInvCmnTb.SlsManId " & _
                                                  "LEFT JOIN AccMast ON AccMast.accid=ItmInvCmnTb.PSAcc " & _
                                                  "LEFT JOIN AccTrCmn on ItmInvCmnTb.trid=AccTrCmn.lnkno " & _
                                                  "WHERE TrId = " & loadedTrId & itemquery, False)
        chgbyprg = True
        ItmInvCmnTb = dtset.Tables(0)
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
        'cmbVoucherTp.Text = getVoucherName(Val(ItmInvCmnTb(0)("InvTypeNo") & ""))
        cmbVoucherTp.Tag = Val(ItmInvCmnTb(0)("invtypeno") & "")
        cmbVoucherTp.Text = Trim(ItmInvCmnTb(0)("Voucher Name") & "")
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
        cldrdate.Value = Format(ItmInvCmnTb(0)("TrDate"), DtFormat) ' "MM/dd/yyyy")
        clrDuedate.Value = Format(ItmInvCmnTb(0)("DueDate"), DtFormat)
        txtprefix.Text = ItmInvCmnTb(0)("PreFix")
        txtprefix.Tag = ItmInvCmnTb(0)("PreFix")
        numVchrNo.Text = ItmInvCmnTb(0)("InvNo") 'Val(Mid(!InvNo, 3)) '!InvNo '
        numVchrNo.Tag = ItmInvCmnTb(0)("InvNo")
        numPrintVchr.Text = ItmInvCmnTb(0)("InvNo")  'CMNREC(12).FormattedText
        txtPPrefix.Text = ItmInvCmnTb(0)("PreFix")
        txtJob.Text = Trim("" & ItmInvCmnTb(0)("Job Code"))
        txtjobname.Text = Trim(ItmInvCmnTb(0)("jobname") & "")
        cmbfc.Text = Trim(ItmInvCmnTb(0)("FC") & "")
        txtfcrt.Text = Format(ItmInvCmnTb(0)("FCRate"), lnumformat)
        FCRt = ItmInvCmnTb(0)("FCRate")
        txtDOLst.Text = Trim("" & ItmInvCmnTb(0)("DocLstTxt"))
        chkimport.Checked = Val(ItmInvCmnTb(0)("isImportOrExport") & "")
        cmbbycustoms.SelectedIndex = Val(ItmInvCmnTb(0)("isthroughcustoms") & "")
        If IsDBNull(ItmInvCmnTb(0)("isTaxInvoice")) Then ItmInvCmnTb(0)("isTaxInvoice") = 0
        If Not IsDBNull(ItmInvCmnTb(0)("taxwithoutLineDiscount")) Then
            chktaxwithoutLinediscount.Checked = ItmInvCmnTb(0)("taxwithoutLineDiscount")
        End If
        cmblocation.Text = Trim(ItmInvCmnTb(0)("DocDefLoc") & "")
        chktaxInv.Checked = ItmInvCmnTb(0)("isTaxInvoice")
        txtSuppAlias.Tag = ItmInvCmnTb(0)("CSCode")
        If chgbyprg = False Then chgbyprg = True
        setSupplier(ItmInvCmnTb(0)("CSCode"))
        chgbyprg = True
        txtCashSuppName.Text = Trim(ItmInvCmnTb(0)("CashCustName") & "")
        txtPurchaseName.Tag = ItmInvCmnTb(0)("PSAcc")
        txtPurchAlias.Text = Trim(ItmInvCmnTb(0)("Alias") & "")
        txtPurchaseName.Text = Trim(ItmInvCmnTb(0)("AccDescr") & "")

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
        PasteFrom(loadedTrId, dtset.Tables(1))
        showOtherCost(False)
        If enableMultipleDebitInInvoice Then loadSalesMultipleDebits(loadedTrId)
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
        chgbyprg = False
        chgbyprgN = False
        ChgId = False
        chgPost = False
        If EnableBarcode Then btnbarcode.Visible = True
        GoTo Quit
Err:
        If MsgBox(Err.Description & Chr(13) & "Retry?", vbRetryCancel + vbQuestion, "Warning During Opening Data File.") = vbRetry Then Resume
Quit:
        chgbyprg = False
    End Sub
    Private Sub loadSalesMultipleDebits(ByVal trid As Long)
        dtMultipleDebits = _objcmnbLayer._fldDatatable("SELECT Alias,AccDescr,accAmt,reference,accid,dbtid FROM SalesMultipleDebitsTb " & _
                                                       "LEFT JOIN AccMast ON AccMast.accid=SalesMultipleDebitsTb.dbaccid where dbtrid=" & trid)
        If trid > 0 Then
            _objcmnbLayer._saveDatawithOutParm("UPDATE SalesMultipleDebitsTb SET setremove=1 " & _
                                          " WHERE dbtrid=" & trid)
        End If
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
            chgbyprg = True
            With cmbVoucherTp
                If .Items.Count > 0 Then
                    If .SelectedIndex < 0 Then .SelectedIndex = 0
                    cmbVoucherTp.Tag = getvrsId(cmbVoucherTp.Text, 6)
                    getVrsDet(Val(cmbVoucherTp.Tag), "IP", vrPrefix, vrVoucherNo, vrAccountNo1, vrAccountNo2, , , vtype)
                Else
                    getVrsDet(0, "IP", vrPrefix, vrVoucherNo, vrAccountNo1, vrAccountNo2)
                End If

            End With
            If Not isModi Then
                numVchrNo.Text = vrVoucherNo
                txtprefix.Text = vrPrefix
            End If

            
            If Not IsLoadFromExternal Then
                If Val(txtSuppName.Tag) = 0 Then
                    txtSuppName.Tag = vrAccountNo2
                    txtSuppAlias.Tag = vrAccountNo2
                End If
            Else
                vrAccountNo2 = IIf(Val(txtSuppAlias.Tag & "") > 0, txtSuppAlias.Tag, vrAccountNo2)
            End If
            If Val(txtPurchaseName.Tag) = 0 Then
                txtPurchaseName.Tag = vrAccountNo1
            End If
            'Dim dtAcc As DataTable
            'dtAcc = _objcmnbLayer._fldDatatable("SELECT Alias,AccDescr,GrpSetOn,GrpSetOn,accid FROM AccMast " & _
            '                                    "LEFT JOIN S1AccHd ON AccMast.S1AccId=S1AccHd.S1AccId WHERE accid=" & vrAccountNo1)

            Dim qry As String = "SELECT Alias,AccDescr,GrpSetOn,GrpSetOn,accid FROM AccMast " & _
            "LEFT JOIN S1AccHd ON AccMast.S1AccId=S1AccHd.S1AccId WHERE accid=" & vrAccountNo1
            qry = qry & " SELECT Alias,AccDescr,GrpSetOn,GrpSetOn,accid FROM AccMast " & _
                      "LEFT JOIN S1AccHd ON AccMast.S1AccId=S1AccHd.S1AccId WHERE accid=" & vrAccountNo2

            Dim ds As DataSet
            ds = _objcmnbLayer._ldDataset(qry, False)

            Dim dtTable As DataTable
            dtTable = ds.Tables(0)

            'Dim _qurey As EnumerableRowCollection(Of DataRow)
            '_qurey = From data In dtAcc.AsEnumerable() Where data("accid") = vrAccountNo1 Select data
            'If _qurey.Count > 0 Then
            '    dtTable = _qurey.CopyToDataTable()
            'Else
            '    dtTable = dtAcc.Clone
            'End If

            If dtTable.Rows.Count > 0 Then
                txtPurchaseName.Text = dtTable(0)("AccDescr")
                txtPurchAlias.Text = dtTable(0)("Alias")
                txtPurchaseName.Tag = vrAccountNo1

            Else
                txtPurchaseName.Text = ""
                txtPurchAlias.Text = ""
                txtPurchaseName.Tag = ""
            End If

            'dtAcc = _objcmnbLayer._fldDatatable("SELECT Alias,AccDescr,GrpSetOn,GrpSetOn,accid FROM AccMast " & _
            '                                   "LEFT JOIN S1AccHd ON AccMast.S1AccId=S1AccHd.S1AccId WHERE accid=" & vrAccountNo2)

            'dtTable.Rows.Clear()
            '_qurey = From data In dtAcc.AsEnumerable() Where data("accid") = vrAccountNo2 Select data
            'If _qurey.Count > 0 Then
            '    dtTable = _qurey.CopyToDataTable()
            'Else
            '    dtTable = dtAcc.Clone
            'End If
            dtTable.Rows.Clear()
            dtTable = ds.Tables(1)
            If dtTable.Rows.Count > 0 Then
                txtSuppName.Text = dtTable(0)("AccDescr")
                txtSuppAlias.Text = dtTable(0)("Alias")
                txtSuppAlias.Tag = vrAccountNo2
                setSupplier(vrAccountNo2)
            Else
                txtSuppName.Text = ""
                txtSuppAlias.Text = ""
                txtSuppAlias.Tag = 0
                txtCashSuppName.Text = ""
            End If
            txtReference.Focus()
            chgbyprg = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub txtReference_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDescr.KeyDown, txtSuppName.KeyDown, txtSuppAlias.KeyDown, txtReference.KeyDown, txtjobname.KeyDown, txtJob.KeyDown, txtcredit.KeyDown, txtCashSuppName.KeyDown
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
            Dim x As Integer = Width - fMList.Width - 100
            Dim y As Integer = Height - fMList.Height - 100
            PopupLoc = New Point(x, y)
            If fMList.Visible = False Then
                Select Case _srchTxtId - 1
                    Case 0, 1
                        SetFmlist(fMList, 23)
                    Case 2 'job 
                        SetFmlist(fMList, 8)
                    Case 3
                        SetFmlist(fMList, 13, 0)
                    Case 4
                        SetFmlist(fMList, 33, 0) 'cash supplier
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
                fMList.Search(txtCashSuppName.Text)
                txtSuppAlias.Text = fMList.AssignList(txtCashSuppName, lstKey, chgbyprg)
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
                fMList.SearchIndex = 1
                fMList.SearchIndexDescr = 2
                fMList_doFocus()
                fMList.Search(txtCashSuppName.Text, , , , True)
                txtCashSuppName.Tag = fMList.AssignList(txtCashSuppName, lstKey, chgbyprg, False)
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
                txtSuppName.Text = ItmFlds(0)
                txtCashSuppName.Text = ItmFlds(0)
            Case 2
                txtJob.Text = ItmFlds(0)
            Case 3
                txtcredit.Text = ItmFlds(0)
                txtcredit.Tag = ItmFlds(3)
            Case 4
                txtSuppName.Text = ItmFlds(0)
                txtSuppName.Tag = ItmFlds(2)
                txtCashSuppName.Text = ItmFlds(0)
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
            .Columns(Constsman).Visible = False
            .Columns(ConstMRP).Visible = enableMRPInStockIn
            .Columns(ConstSP1).Visible = enableSP1InStockIn
            .Columns(ConstSP2).Visible = enableSP2InStockIn
            .Columns(ConstSP3).Visible = enableSP3InStockIn
            If Not EnableGST And Not enableGCC Then
                .Columns(ConstLTotal).ReadOnly = False
            End If
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
        'If enableItemAutoPopulate Then
        '    fProductEnquiry = New ItmEnqry
        '    fProductEnquiry.ShowDialog()
        'End If
        'calculate()
        'ChgByPrg = False

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
                    If enableFloodCess Then
                        'lnTax = lnTax + ((actualPrice * .Item(ConstFloodcess, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value)
                        'lnTax = lnTax + CDbl(.Item(ConstFloodCessAmt, i).Value)
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
            totTax = IIf(chkimport.Checked, 0, totTax)
            totAmt = totAmt + totTax + totCess
            lblTotAmt.Text = Format(totItm, lnumFormat)
            lblNetAmt.Text = Format(totAmt - CDbl(numDisc.Text), lnumFormat)

            If Val(txtroundOff.Text) > 0 Then
                lblNetAmt.Text = Format(CDbl(lblNetAmt.Text) - (IIf(cmbsign.SelectedIndex = 1, 1, -1) * CDbl(txtroundOff.Text)), lnumFormat)
            End If
            lbltax.Text = Format(totTax, lnumformat)
            lblcess.Text = Format(totCess, lnumFormat)
            totAmt = totAmt - CDbl(numDisc.Text)
            lbltaxable.Text = Format(totTaxableAmt, lnumFormat)
            chgAmt = False
            chgbyprg = False
        End With
    End Sub
    'Private Sub calcualteLineTotal(ByVal RowIndex As Integer)
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
    '        If EnableGST Then
    '            getGSTDetails(Trim(.Item(ConstBarcode, i).Value & ""), i, True)
    '            If Val(.Item(ConstSGSTAmt, i).Value & "") = 0 Then .Item(ConstSGSTAmt, i).Value = 0
    '            If Val(.Item(ConstCGSTAmt, i).Value & "") = 0 Then .Item(ConstCGSTAmt, i).Value = 0
    '            .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstCGSTAmt, i).Value) + CDbl(.Item(ConstSGSTAmt, i).Value), lnumFormat)
    '        ElseIf enableGCC Or ShowTaxOnInventory Then
    '            Dim actualPrice As Double
    '            Dim discountOther As Double
    '            discountOther = CDbl(.Item(ConstDiscOther, i).Value) * CDbl(.Item(ConstQty, i).Value)
    '            If chktaxwithoutLinediscount.Checked Then
    '                actualPrice = (CDbl(.Item(ConstActualPrice, i).Value) * CDbl(.Item(ConstQty, i).Value)) - discountOther
    '            Else
    '                actualPrice = (CDbl(.Item(ConstActualPrice, i).Value) * CDbl(.Item(ConstQty, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value) - discountOther
    '            End If
    '            'actualPrice = (CDbl(.Item(ConstActualPrice, i).Value) * CDbl(.Item(ConstQty, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value) - discountOther
    '            actualPrice = Format(actualPrice, lnumformat)
    '            .Item(ConstIGSTAmt, i).Value = ((actualPrice * .Item(ConstIGSTP, i).Value) / 100)
    '            .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstIGSTAmt, i).Value), lnumformat)
    '        End If
    '        'If enablecess And Val(lblgstn.Tag & "") = 0 And cessdate <= DateValue(cldrdate.Value) And chktaxInv.Checked Then
    '        '    .Item(ConstcessAmt, i).Value = Format((((CDbl(.Item(ConstActualPrice, i).Value) - CDbl(.Item(ConstDiscOther, i).Value)) * .Item(Constcess, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value), lnumFormat)
    '        'End If
    '        If chkTaxbill.Checked = False Then
    '            .Item(ConstTaxAmt, i).Value = Format(0, lnumformat)
    '            .Item(ConstTaxP, i).Value = Format(0, lnumformat)
    '            .Item(ConstcessAmt, i).Value = 0
    '        End If

    '        .Item(constItmTot, i).Value = Format((.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value), lnumFormat)
    '        .Item(ConstLTotal, i).Value = Format((.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value) + CDbl(.Item(ConstTaxAmt, i).Value), lnumFormat)
    '        .Item(ConstNUPrice, i).Value = CDbl(.Item(ConstActualPrice, i).Value) - (CDbl(.Item(ConstDisAmt, i).Value) / CDbl(.Item(ConstQty, i).Value)) + (CDbl(.Item(ConstTaxAmt, i).Value) / CDbl(.Item(ConstQty, i).Value)) + (CDbl(.Item(ConstcessAmt, i).Value) / CDbl(.Item(ConstQty, i).Value)) - Val(.Item(ConstDiscOther, i).Value)
    '    End With

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
    '        Dim totTaxableAmt As Double
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
    '            lnTax = 0
    '            For i = 0 To .Rows.Count - 1
    '                If (calculateLineTotal And Val(numDisc.Text) > 0) Or chgDiscount Then
    '                    calcualteLineTotal(i)
    '                End If
    '                If Val(.Item(ConstItemID, i).Value) = 0 Then GoTo nxt
    '                totQty = totQty + IIf(Val(.Item(ConstQty, i).Value) > 0, Val(.Item(ConstQty, i).Value), 0)
    '                If chktaxInv.Checked = False Then
    '                    .Item(ConstTaxAmt, i).Value = Format(0, lnumformat)
    '                    .Item(ConstTaxP, i).Value = Format(0, lnumformat)
    '                    .Item(ConstcessAmt, i).Value = 0
    '                Else
    '                    .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstIGSTAmt, i).Value), lnumformat)
    '                    .Item(ConstTaxP, i).Value = Format(CDbl(.Item(ConstIGSTP, i).Value), lnumformat)
    '                    lnTax = CDbl(.Item(ConstIGSTAmt, i).Value)
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
    '                totAmt = totAmt + (.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value)
    '                totItm = totItm + (.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value)

    '                lnttl = (CDbl(.Item(ConstQty, i).Value) * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value)
    '                lnttl = lnttl + lnTax
    '                chgbyprg = True
    '                .Item(ConstLTotal, i).Value = Format(lnttl, lnumformat)
    '                chgbyprg = False
    '                If Val(.Item(ConstTaxAmt, i).Value) > 0 Then
    '                    totTaxableAmt = totTaxableAmt + ((CDbl(.Item(ConstActualPrice, i).Value) - CDbl(.Item(ConstDiscOther, i).Value)) * CDbl(.Item(ConstQty, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value)
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
    '            lbltax.Text = Format(totTax, lnumformat)
    '            totAmt = totAmt - CDbl(numDisc.Text)
    '            lbltaxable.Text = Format(totTaxableAmt, lnumformat)
    '            chgAmt = False
    '        End With
    '    End Sub

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
                    btnupdate.Focus()
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
        If Not grdVoucher.CurrentRow Is Nothing Then
            If e.RowIndex < 0 Then Exit Sub
            If grdVoucher.Item(e.ColumnIndex, e.RowIndex).ReadOnly = False Then grdBeginEdit()
        End If

    End Sub

    Private Sub grdVoucher_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellValidated
        If chgbyprg Then Exit Sub
        Valid(e.RowIndex, e.ColumnIndex)
        SrchText = ""
    End Sub
    Private Sub Valid(ByVal RowIndex As Integer, ByVal ColIndex As Integer)
        With grdVoucher
            Select Case ColIndex
                Case ConstBarcode, ConstItemCode
                    If Not chgItm Then Exit Sub
                    If Trim(.Item(ColIndex, RowIndex).Value) = "" And SrchText <> "" Then .Item(ColIndex, RowIndex).Value = SrchText
                    Dim dtItms As DataTable
                    Dim found As Boolean
                    dtItms = ItmValidation(3, SrchText, True, "IP", Val(txtSuppAlias.Tag)) 'getItmDtls(3, SrchText, True)
                    If dtItms.Rows.Count > 0 Then
                        found = True
                        AddDetails(dtItms, RowIndex)
                    End If
                    SrchText = ""
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
                Case ConstWoodQty, ConstWoodDiscQty
                    If chgAmt Then
                        calculateWoodDiscountQty(RowIndex)
                        calculate(, True)
                    End If
                Case ConstQty

                    If chgAmt Then
                        If AllowUnitDiscountEntryOnInventory Then
                            .Item(ConstDisAmt, RowIndex).Value = Format(((CDbl(.Item(ConstActualPrice, RowIndex).Value) * Val(.Item(ConstDisP, RowIndex).Value)) / 100) * CDbl(.Item(ConstQty, RowIndex).Value), lnumformat)
                        End If
                        calOthCost()
                        If EnableGST Then getGSTDetails(Trim(.Item(ConstBarcode, RowIndex).Value & ""), RowIndex, True)
                        calculate(, True)
                    End If

                Case ConstUPrice
                    If chgAmt Then
                        If Format(.Item(ConstActualPrice, RowIndex).Value, lnumformat) <> Format(CDbl(.Item(ConstUPrice, RowIndex).Value), lnumformat) Then
                            .Item(ConstActualPrice, RowIndex).Value = CDbl(.Item(ConstUPrice, RowIndex).Value)
                        End If

                        If AllowUnitDiscountEntryOnInventory Then
                            .Item(ConstDisAmt, RowIndex).Value = Format(((CDbl(.Item(ConstActualPrice, RowIndex).Value) * Val(.Item(ConstDisP, RowIndex).Value)) / 100) * CDbl(.Item(ConstQty, RowIndex).Value), lnumformat)
                        End If
                        If EnableGST Then getGSTDetails(Trim(.Item(ConstBarcode, RowIndex).Value & ""), RowIndex, True)
                        calculate(, True)
                    End If
                Case ConstLTotal
                    If chgAmt Then
                        If Not enableGCC And Not EnableGST And Val(.Item(ConstDisAmt, RowIndex).Value) = 0 Then
                            If Val(.Item(ConstLTotal, RowIndex).Value) = 0 Then .Item(ConstLTotal, RowIndex).Value = 0
                            If Val(.Item(ConstQty, RowIndex).Value) = 0 Then .Item(ConstQty, RowIndex).Value = 0
                            .Item(ConstActualPrice, RowIndex).Value = CDbl(.Item(ConstLTotal, RowIndex).Value) / CDbl(.Item(ConstQty, RowIndex).Value)
                            If Format(.Item(ConstActualPrice, RowIndex).Value, lnumformat) <> Format(CDbl(.Item(ConstUPrice, RowIndex).Value), lnumformat) Then
                                .Item(ConstUPrice, RowIndex).Value = CDbl(.Item(ConstActualPrice, RowIndex).Value)
                            End If
                            calculate(, True)
                        End If
                    End If

                Case ConstDisP
                    If chgAmt Then
                        .Item(ConstDisAmt, RowIndex).Value = Format(((CDbl(.Item(ConstActualPrice, RowIndex).Value) * Val(.Item(ConstDisP, RowIndex).Value)) / 100) * CDbl(.Item(ConstQty, RowIndex).Value), lnumformat)
                        If EnableGST Then getGSTDetails(Trim(.Item(ConstBarcode, RowIndex).Value & ""), RowIndex, True)
                        calculate(, True)
                    End If
                Case ConstDisAmt
                    If chgAmt Then
                        Dim unitDiscount As Double
                        unitDiscount = CDbl(.Item(ConstDisAmt, RowIndex).Value) / CDbl(.Item(ConstQty, RowIndex).Value)
                        .Item(ConstDisP, RowIndex).Value = Format((unitDiscount * 100) / CDbl(.Item(ConstActualPrice, RowIndex).Value), lnumformat)
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
                        .Item(ConstDonotAllowsaveItem, RowIndex).Value = 0
                        If Trim(.Item(ConstSerialNo, RowIndex).Value & "") <> "" Then
                            dt = _objcmnbLayer._fldDatatable("select trid from ItmInvTrTb where itemid=" & Val(.Item(ConstItemID, RowIndex).Value) & " and SerialNo='" & .Item(ConstSerialNo, RowIndex).Value & "'")
                            If dt.Rows.Count > 0 Then
                                MsgBox("Entered Batch number already exist", MsgBoxStyle.Exclamation)
                                .Item(ConstDonotAllowsaveItem, RowIndex).Value = 1
                            End If
                        End If
                    End If
                Case ConstMRP
                    If setSalespriceFromMRPinPruchase And cngmrp Then
                        Dim actualprice As Double = CDbl(.Item(ConstMRP, RowIndex).Value)
                        Dim ttax As Double = CDbl(.Item(ConstTaxP, RowIndex).Value) + CDbl(.Item(Constcess, RowIndex).Value) + CDbl(.Item(ConstRegcess, RowIndex).Value)
                        actualprice = (actualprice * 100) / (ttax + 100)
                        .Item(ConstSP1, RowIndex).Value = Format(actualprice, lnumformat)
                    End If
                    cngmrp = False
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
        Dim cnt As Integer
        If dtGST Is Nothing Then Exit Sub
        If dtTax Is Nothing Then Exit Sub
        If Not dtTax Is Nothing Then dtTax.Rows.Clear()
        Dim taxamt As Double
        If enableGCC Then GoTo addtax
        If enablecess Or (enableFloodCess And cessenddate >= DateValue(cldrdate.Value)) Then
addtax:
            Dim dt As DataTable = _objcmnbLayer._fldDatatable("SELECT  vatcode,convert(money, 0) Amount,paymentAC,vat,AccDescr From VatMasterTb Left join accmast on VatMasterTb.paymentAC=AccMast.accid", False)
            For i = 0 To dt.Rows.Count - 1
                dtrow = dtTax.NewRow
                dtrow("slno") = dtTax.Rows.Count + 1
                dtrow("AccountName") = dt(i)("AccDescr")
                dtrow("ACid") = dt(i)("paymentAC")
                dtrow("Amount") = 0
                dtTax.Rows.Add(dtrow)
            Next
        End If
        With grdVoucher
            If EnableGST Then
                cnt = .RowCount - 1
                Dim _qurey As EnumerableRowCollection(Of DataRow)
                For i = 0 To cnt
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
                    'If enableFloodCess Then
                    '    Dim b = (From data In dtTax.AsEnumerable() Where data("ACid") = Val(.Item(ConstcessAc, i).Value & "") Select data("slno"))
                    '    slno = 0
                    '    For Each itm In b
                    '        slno = itm
                    '    Next
                    '    If slno > 0 Then
                    '        dtTax(slno - 1)("Amount") = CDbl(dtTax(slno - 1)("Amount")) + CDbl(.Item(ConstFloodCessAmt, i).Value)
                    '    End If
                    'End If
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
            Else
                For i = 0 To .RowCount - 1
                    If Val(.Item(ConstcessAc, i).Value & "") = 0 Then .Item(ConstcessAc, i).Value = 0
                    Dim b = (From data In dtTax.AsEnumerable() Where data("ACid") = Val(.Item(ConstcessAc, i).Value & "") Select data("slno"))
                    slno = 0
                    For Each itm In b
                        slno = itm
                    Next
                    If slno > 0 Then
                        dtTax(slno - 1)("Amount") = CDbl(dtTax(slno - 1)("Amount")) + CDbl(.Item(ConstIGSTAmt, i).Value)
                    End If
                Next
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

        End With

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
                    .Item(ConstIGSTP, i).Value = Format(IIf(IsDBNull(dt(0)("IGST")), 0, CDbl(dt(0)("IGST"))), lnumformat)
                Else
                    .Item(ConstCGSTP, i).Value = Format(0, lnumformat)
                    .Item(ConstSGSTP, i).Value = Format(0, lnumformat)
                    .Item(ConstIGSTP, i).Value = Format(0, lnumformat)
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
            actualPrice = Format(actualPrice, lnumformat)
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

    Private Sub AddDetails(ByVal DR As DataTable, ByVal rowindex As Integer)
        Dim i As Integer
        Dim PMult As Double
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
            If Val(.Item(ConstQty, i).Value) = 0 Then
                .Item(ConstQty, i).Value = Format(1, lnumformat) 'IIf(IsReturn, -1, 1)
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
            .Item(ConstB, .CurrentCell.RowIndex).Value = "B"
            .Item(ConstPFraction, i).Value = IIf(IsDBNull(DR(0)("FraCount")), "2", DR(0)("FraCount"))
            .Item(ConstUnit, i).Value = DR(0)("Unit")
            .Item(ConstWarrentyExpiry, i).Value = Format(DateAdd(DateInterval.Year, 1, DateValue(cldrdate.Value)), DtFormat)

            If CDbl(.Item(ConstUPrice, i).Value) = 0 Or chgItm Then
                If getRight(200, CurrentUser) Then
                    .Item(ConstActualPrice, i).Value = DR(0)("lastPrice")
                Else
                    .Item(ConstActualPrice, i).Value = 0
                End If
                'getPurchAmt(DR(0)("CostAvg"), Val(txtSuppAlias.Tag), Val(.Item(ConstItemID, .CurrentCell.RowIndex).Value))
                .Item(ConstUPrice, i).Value = Format(CDbl(.Item(ConstActualPrice, i).Value), lnumformat)
            End If
            .Item(ConstLTotal, i).Value = Format(.Item(ConstQty, i).Value * CDbl(.Item(ConstUPrice, i).Value), lnumformat)
            .Item(ConstNUPrice, i).Value = Format(CDbl(.Item(ConstUPrice, i).Value), lnumformat)
            If ShowTaxOnInventory Then
                If Val(DR(0)("vat") & "") = 0 Then
                    DR(0)("vat") = 0
                End If
                .Item(ConstTaxP, i).Value = Format(IIf(IsDBNull(DR(0)("vat")), 0, CDbl(DR(0)("vat"))), lnumformat)
                .Item(ConstTaxAmt, i).Value = ((CDbl(.Item(ConstActualPrice, i).Value) * .Item(ConstTaxP, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value)
                .Item(ConstIGSTP, i).Value = IIf(IsDBNull(DR(0)("vat")), 0, CDbl(DR(0)("vat")))
                .Item(ConstcessAc, i).Value = IIf(IsDBNull(DR(0)("paymentAC")), 0, Val(DR(0)("paymentAC")))
            ElseIf EnableGST Then
                getGSTDetails(Trim(.Item(ConstBarcode, i).Value & ""), i, False)
            End If
            Dim cessAmt As Double
            If enablecess Then
                .Item(ConstRegcess, i).Value = Format(IIf(IsDBNull(DR(0)("rgcess")), 0, CDbl(DR(0)("rgcess"))), lnumformat)
                .Item(ConstRegcessAc, i).Value = IIf(IsDBNull(DR(0)("rgcpeymentacc")), 0, Val(DR(0)("rgcpeymentacc")))
                If Val(DR(0)("additionalcess") & "") = 0 Then DR(0)("additionalcess") = 0
                .Item(ConstAdditionalcess, i).Value = IIf(IsDBNull(DR(0)("additionalcess")), 0, Val(DR(0)("additionalcess")))
                cessAmt = ((CDbl(.Item(ConstActualPrice, i).Value) * .Item(ConstRegcessAc, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value)
                If Val(.Item(ConstAdditionalcess, i).Value) = 0 Then .Item(ConstAdditionalcess, i).Value = 0
                cessAmt = cessAmt + CDbl(.Item(ConstAdditionalcess, i).Value)
            End If
            If enableFloodCess And cessenddate >= DateValue(cldrdate.Value) Then
                .Item(Constcess, i).Value = Format(IIf(IsDBNull(DR(0)("vat")), 0, CDbl(DR(0)("vat"))), lnumformat)
                .Item(ConstcessAc, i).Value = IIf(IsDBNull(DR(0)("paymentAC")), 0, Val(DR(0)("paymentAC")))
                cessAmt = cessAmt + (((CDbl(.Item(ConstActualPrice, i).Value) * .Item(Constcess, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value))
                .Item(ConstcessAmt, i).Value = Format(cessAmt, lnumformat)
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
                Case ConstQty, ConstTaxP, ConstDisAmt, ConstDisP, ConstWoodDiscQty, ConstWoodQty
                    '.Item(ConstLTotal, i).Value = Format((.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - (.Item(ConstQty, i).Value * .Item(ConstDisAmt, i).Value) + (.Item(ConstQty, i).Value * .Item(ConstTaxAmt, i).Value), lnumformat)
                    '.Item(ConstNUPrice, i).Value = CDbl(.Item(ConstActualPrice, i).Value) - .Item(ConstDisAmt, i).Value + .Item(ConstTaxAmt, i).Value
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
                Case ConstUPrice, ConstSP1, ConstSP2, ConstSP3
                    chgUprice = True
                    chgAmt = True
                    calculateTaxFromUnitPrice(e.RowIndex, e.ColumnIndex)
                Case ConstMRP
                    cngmrp = True
            End Select
        End With
    End Sub

    Private Sub calculateTaxFromUnitPrice(ByVal i As Integer, ByVal cindex As Integer)
        chgbyprg = True
        Dim actualprice As Double
        Dim ttax As Double
        With grdVoucher
            Select Case cindex
                Case ConstUPrice
                    If chgAmt And chkcal.Checked Then
                        actualprice = CDbl(.Item(ConstUPrice, i).Value) - CDbl(.Item(ConstAdditionalcess, i).Value)
                        ttax = CDbl(.Item(ConstTaxP, i).Value) + CDbl(.Item(Constcess, i).Value) + CDbl(.Item(ConstRegcess, i).Value)
                        .Item(ConstActualPrice, i).Value = (actualprice * 100) / (ttax + 100)
                        .Item(ConstUPrice, i).Value = Format(.Item(ConstActualPrice, i).Value, lnumformat)
                    End If
                   
                Case Else
                    If chgAmt And chkcaltaxForsalesprice.Checked Then
                        actualprice = CDbl(.Item(cindex, i).Value) - CDbl(.Item(ConstAdditionalcess, i).Value)
                        ttax = CDbl(.Item(ConstTaxP, i).Value) + CDbl(.Item(Constcess, i).Value) + CDbl(.Item(ConstRegcess, i).Value)
                        .Item(cindex, i).Value = (actualprice * 100) / (ttax + 100)
                        .Item(cindex, i).Value = Format(.Item(cindex, i).Value, lnumformat)
                    End If
                   
            End Select
        End With
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
                If enableItemAutoPopulate Then GoTo ext
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
                If enableItemAutoPopulate Then GoTo ext
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
ext:
    End Sub
    Private Sub ShowPanel()
        If Not _srchOnce Then
            chgbyprg = True
            Dim PopupLoc As Point
            Dim x As Integer '= Width - plsrch.Width - 100
            Dim y As Integer '= Height - plsrch.Height - 100
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
        If grdVoucher.Rows.Count = 0 Then Exit Sub
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
                Dim batchflag As Boolean
                If grdVoucher.CurrentCell.ColumnIndex = ConstSerialNo Then
                    batchflag = True
                End If
                If FindNextCell(grdVoucher, grdVoucher.CurrentCell.RowIndex, grdVoucher.CurrentCell.ColumnIndex + 1) Then
                    AddRow()
                End If
                If batchflag And Val(grdVoucher.Item(ConstDonotAllowsaveItem, grdVoucher.CurrentCell.RowIndex).Value) = 1 Then
                    grdVoucher.Item(ConstSerialNo, grdVoucher.CurrentCell.RowIndex).Value = ""
                    grdVoucher.CurrentCell = grdVoucher.Item(ConstSerialNo, grdVoucher.CurrentCell.RowIndex)
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
                        If plsrch.Visible Then plsrch.Visible = False
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
        lnumformat = numFormat
        chgbyprg = True
        'MsgBox(grdVoucher.Rows.Count)
        If Not grdVoucher Is Nothing Then
            If grdVoucher.RowCount > 0 Then
                grdVoucher.Rows.Clear()
            End If
        End If

        'grdVoucher.CurrentCell = grdVoucher.Item(1, 0)
        activecontrolname = ""
        txtReference.Text = ""
        txtDescr.Text = ""
        txtSuppAlias.Text = ""
        txtSuppAlias.Tag = ""
        txtSuppName.Text = ""
        txtJob.Text = ""
        btnOthrOk.Tag = ""
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
        getItemInfo(0)
        If dtItemInfo.Rows.Count > 0 Then dtItemInfo.Rows.Clear()
        ReDim LPos(0)
        numVchrNo.ReadOnly = True
        txtjobname.Text = ""
        chgPost = False
        chktaxwithoutLinediscount.Checked = False

        numVchrNo.Focus()
        lbladd1.Text = ""
        lbladd2.Text = ""
        lbladd3.Text = ""
        lbladd4.Text = ""
        lbladd5.Text = ""
        lbladd6.Text = ""
        lbladd7.Text = ""
        lbltrdate.Text = ""
        'If dtMultipleDebits.Rows.Count > 0 Then dtMultipleDebits.Rows.Clear()
        If Not dtMultipleDebits Is Nothing Then
            If dtMultipleDebits.Rows.Count > 0 Then dtMultipleDebits.Rows.Clear()
        End If
        grdOtherCost.Rows.Clear()
        lblOthCost.Text = Format(0, numFormat)
        cmbfc.Text = ""
        txtfcrt.Text = Format(1, numFormat)
        clearOtherCostFileds()
        chgbyprg = True
        txtDOLst.Text = ""
        calculate()
        isNotSupplierAccount = False
        If cmblocation.Text = "" Then
            cmblocation.Text = Dloc
        End If
        chgbyprg = False
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
            'btnbarcode.Visible = False
            If userType Then
                btnupdate.Tag = IIf(getRight(42, CurrentUser), 1, 0)
                btndelete.Tag = IIf(getRight(43, CurrentUser), 1, 0)
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
        calculate()
        CalculateGST()
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
            MsgBox("Enter a valid " & IIf(isCust, " Customer ", " Supplier ") & " Account !!", vbExclamation)
            txtSuppAlias.Focus()
            'txtSuppAlias.Focus()
            Exit Sub
        Else
            txtSuppAlias.Tag = _vAcMaster(0)("accid")
        End If
        If blockInvoicing() Then Exit Sub
        'If Not isModi Then
        '    If chekDuplicate() Then Exit Sub
        'End If
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT Trid from ItmInvcmntb where TrType='IP' AND cscode=" & Val(txtSuppAlias.Tag) & " and TrRefNo='" & txtReference.Text & "' and Trid<>" & loadedTrId)
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
        If enableBranch And UsrBr = "" Then
            MsgBox("Transaction cannot be saved without Branch! Please login with Branch", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If enableMultipleDebitAutoPopulate And vtype = "Credit" And isModi = False And enableMultipleDebitInInvoice Then
            If Not showMultipleDebits() Then Exit Sub
            saveTrans()
        Else
            If MsgBox("Verification is succeeded. Do you like to file it ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = vbYes Then
                'saveTrans()
                loadWaite(1)
            End If
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
        
        setInvCmnValue(TrId, 0)
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

    Private Sub saveTrans()
        Dim TrId As Long
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
            If Not CheckNoExists(txtprefix.Text, Val(numVchrNo.Text), "IP", "Inventory") Then
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
        'DiscAcc = getConstantAccounts(2)
        'calculate()
        'If dtTable.Rows.Count > 0 Then dtTable.Clear()
        If isModi Then
            dtTable = _objcmnbLayer._fldDatatable("SELECT TOP 1 * FROM ItmInvCmnTb WHERE TrId = " & loadedTrId)
            If dtTable.Rows.Count = 0 Then
                MsgBox("The loaded voucher not found.  It may removed externally.", vbExclamation)
                Exit Sub
            End If
            TrId = loadedTrId
        End If
        Me.Cursor = Cursors.WaitCursor
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
        'Dim dtinv As DataTable
        'dtinv = _objcmnbLayer._fldDatatable("Select InvNo from ItmInvCmnTb where trid=" & TrId)
        'If dtinv.Rows.Count > 0 Then
        '    If Val(numVchrNo.Text) <> Val(dtinv(0)("InvNo")) Then
        '        numVchrNo.Text = dtinv(0)("InvNo")
        '    End If
        'End If

        '_objcmnbLayer._saveDatawithOutParm("UPDATE ItmInvCmnTb SET taxwithoutLineDiscount=" & IIf(chktaxwithoutLinediscount.Checked, 1, 0) & _
        '                                   ", CashCustName='" & MkDbSrchStr(IIf(txtCashSuppName.Text = "", txtSuppName.Text, txtCashSuppName.Text)) & "' WHERE TRID=" & TrId)
        'to check whether date has been changed or not
        'if changed there should be calculeted cost average for all items
        'dtTable = _objcmnbLayer._fldDatatable("SELECT TOP 1 * FROM ItmInvCmnTb LEFT JOIN VoucherTypeNoTb ON ItmInvCmnTb.TypeNo=VoucherTypeNoTb.vrno " & _
        '                                      "WHERE InvType='OUT' AND Trdate >'" & Format(DateValue(cldrdate.Value), "yyyy/MM/dd") & "'")
        'If dtTable.Rows.Count > 0 Then
        '    dateChanged = True
        'Else
        '    dateChanged = False
        'End If

        'ReDim JobAcc(0)
        'JobAcc(0).Acc = Val(txtPurchaseName.Tag)
        'JobAcc(0).Job = txtJob.Text
        'Dim amt As Double

        'If setTaxAsIncomeExpense Then
        '    If Val(txtroundOff.Text) > 0 Then
        '        amt = (CDbl(lblTotAmt.Text) + CDbl(lbltax.Text)) - (IIf(cmbsign.SelectedIndex = 1, 1, -1) * CDbl(txtroundOff.Text))
        '    Else
        '        amt = (CDbl(lblTotAmt.Text) + CDbl(lbltax.Text))
        '    End If
        '    JobAcc(0).Amt = IIf(DiscAcc = 0, CDbl(lblNetAmt.Text), amt)
        'Else
        '    If Val(txtroundOff.Text) > 0 Then
        '        amt = CDbl(lblTotAmt.Text) - (IIf(cmbsign.SelectedIndex = 1, 1, -1) * CDbl(txtroundOff.Text))
        '    Else
        '        amt = CDbl(lblTotAmt.Text)
        '    End If
        '    JobAcc(0).Amt = IIf(DiscAcc = 0, CDbl(lblNetAmt.Text) - CDbl(lbltax.Text) - CDbl(lblcess.Text), amt)
        'End If

        Dim itemidsdatatable As New DataTable
        itemidsdatatable.Columns.Add(New DataColumn("Itemid", GetType(Long)))
        TDrAmt = saveInvTr(TrId)

        'With grdVoucher
        '    For i = 0 To .Rows.Count - 1 '- 1
        '        If CDbl(.Item(ConstQty, i).Value) <> 0 Or .Item(ConstSlNo, i).Value.ToString = "M" Then
        '            PPerU = Val(.Item(ConstPMult, i).Value) 'Switch(.TextMatrix(i, 4) = "B", 1, .TextMatrix(i, 4) = "P1", CSng(grdVoucher.TextMatrix(i, 18)), .TextMatrix(i, 4) = "P2", CSng(grdVoucher.TextMatrix(i, 19)))
        '            PPerU = IIf(PPerU = 0, 1, PPerU)
        '            TDrAmt = TDrAmt + CDbl(.Item(ConstQty, i).Value) * (CDbl(.Item(ConstActualPrice, i).Value) - IIf(DiscAcc = 0, CDbl(.Item(ConstDiscOther, i).Value), 0))
        '            TDrAmt = TDrAmt - CDbl(.Item(ConstDisAmt, i).Value)
        '            'If setTaxAsIncomeExpense Then
        '            '    TDrAmt = TDrAmt + CDbl(.Item(ConstTaxAmt, i).Value)
        '            'End If
        '            setInvDetValue(TrId, PPerU, i)
        '            _objInv._saveDetails()
        '            If UCase(.Item(ConstqtyChg, i).Value) = "CHG" Then
        '                qtychanged = True
        '            Else
        '                qtychanged = False
        '            End If
        '            'If (dateChanged Or (qtychanged And Val(.Item(ConstId, i).Value) > 0)) And enableRealtimeCosting And Val(.Item(ConstItemID, i).Value) > 0 Then
        '            '    _objInv.ItemId = Val(.Item(ConstItemID, i).Value)
        '            '    _objInv.TrDate = DateValue(cldrdate.Value)
        '            '    _objInv.setcostAverageOnModification(UsrBr)
        '            'End If
        '        End If
        '        'status("", "", i + 1, .Rows.Count)
        '    Next
        If isModi Then
            _objInv.deleteInventoryRelatedItemDetails(loadedTrId)
        End If
        UpdateAccounts(TrId, TDrAmt, DiscAcc)
        'If Trim(LddImpDocs) <> "" Then RfrshPrssdQty(LddImpDocs)
        If isModi = False Then
            numPrintVchr.Text = numVchrNo.Text
            numVchrNo.Tag = Val(numVchrNo.Text) 'Trim(CMNREC(12).FormattedText)
            txtPPrefix.Text = txtprefix.Text
            txtprefix.Tag = txtprefix.Text
            'txtprefix.Text = SetNextVrNo(numVchrNo, Val(cmbVoucherTp.Tag), "IP", "TrType = 'IP' AND InvNo = ", False, True, True)
        End If
        ChgId = False
        chgPost = False
        If isModi Then
            btnModify_Click(btnModify, New System.EventArgs)
        Else
            AddNewClick()
            cmbVoucherTp_SelectedIndexChanged(cmbVoucherTp, New System.EventArgs)
        End If
        Me.Cursor = Cursors.Default
        If Not fwait Is Nothing Then fwait.Close() : fwait = Nothing
        MsgBox("Purchase Invoice is succesfully posted with Vr. # " & numPrintVchr.Text & ".", MsgBoxStyle.Information)
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
        If dtAccTb Is Nothing Then
            dtAccTb = _objcmnbLayer._fldDatatable("Select top 1 LinkNo,AccountNo,DueDate,Reference,EntryRef,DealAmt,FCAmt,CurrencyCode,CurrRate, " & _
                                                  "JobCode,PDCAcc,BankCode,LPONo,OthCost,TrInf,TermsId,CustAcc,AccWithRef,ChqDate,ChqNo,SuppInvDate," & _
                                                  "vatid,isvatEntry,UnqNo from AccTrDet")
        End If
        If dtAccTb.Rows.Count > 0 Then
            dtAccTb.Rows.Clear()
        End If
        'tax
        Dim defaultInput As Integer
        Dim defaultOutputOnImport As Integer
        Dim defaultInputOnImport As Integer
        Dim defaultOutput As Long
        If enableGCC And chkimport.Checked Then
            getdefaultTaxAccounts(defaultInput, defaultOutput, defaultInputOnImport, defaultOutputOnImport)
        End If
        setAcctrCmnValue(TrId, LinkNo)
        'LinkNo = 0
        'LinkNo = Val(_objTr.SaveAccTrCmn())
        'Debit Entry
        'For j = 0 To JobAcc.Count - 1
        '    setAcctrDetValue(LinkNo, j)
        '    _objTr.saveAccTrans()
        'Next

        Dim EntRef As String = Strings.Left(Trim(txtDescr.Text) & IIf(Trim(txtDescr.Text) = "", "", " / ") & cmbVoucherTp.Text & " PURCHASE (" & txtPurchaseName.Text & ")", 249)
        Dim Dramt As Double = (TDrAmt - IIf(DiscAcc > 0, CDbl(numDisc.Text), 0)) * FCRt
        Dim ttlTxAmount As Double
        Dim TxAmount As Double

        'Tax Entry Debit
        Dim i As Integer = 0
        If chktaxInv.Checked Then
            If chkimport.Checked And enableGCC Then
                For i = 0 To dtTax.Rows.Count - 1
                    If Val(dtTax(i)("ACid")) > 0 And Val(dtTax(i)("Amount")) > 0 Then
                        'TxAmount = TxAmount + CDbl(dtTax(i)("Amount"))
                        ttlTxAmount = Format(ttlTxAmount + CDbl(dtTax(i)("Amount")), numFormat)
                    End If
                Next
                'Debit Import on Input
                If defaultInputOnImport > 0 And ttlTxAmount > 0 Then
                    setAcctrDetValue(LinkNo, defaultInputOnImport, Trim(txtReference.Text), " Tax Input On Import", ttlTxAmount * FCRt, Strings.Left(txtJob.Text, 50), "", 0, 0, "", "", Val(txtSuppAlias.Tag), Val(txtSuppAlias.Tag) & txtReference.Text, cmbfc.Text, FCRt)
                    '_objTr.saveAccTrans()

                End If
                'Credit Import on Output
                If defaultOutputOnImport > 0 And ttlTxAmount > 0 Then
                    setAcctrDetValue(LinkNo, defaultOutputOnImport, Trim(txtReference.Text), " Tax Output On Import", ttlTxAmount * FCRt * -1, Strings.Left(txtJob.Text, 50), "", 0, 0, "", "", Val(txtSuppAlias.Tag), Val(txtSuppAlias.Tag) & txtReference.Text, cmbfc.Text, FCRt)
                    '_objTr.saveAccTrans()
                End If


            Else
                If Not dtTax Is Nothing Then
                    For i = 0 To dtTax.Rows.Count - 1
                        If Val(dtTax(i)("ACid")) > 0 And Val(dtTax(i)("Amount")) > 0 Then
                            TxAmount = Format(CDbl(dtTax(i)("Amount")), numFormat)
                            ttlTxAmount = Format(ttlTxAmount + TxAmount, numFormat)
                        End If
                    Next
                End If
                
            End If

        End If
        'Debit Entry
        Dramt = CDbl(lblTotAmt.Text)
        Dramt = IIf(DiscAcc = 0, CDbl(lblNetAmt.Text) - CDbl(lbltax.Text) - CDbl(lblcess.Text), Dramt)
        Dim total As Double
        If chkimport.Checked = False Then
            total = Dramt + ttlTxAmount
        Else
            total = Dramt
        End If


        total = CDbl(lblNetAmt.Text) - total
        Dramt = Dramt + total
        Dim DrTrRef As String = Strings.Left(Trim(txtDescr.Text) & IIf(txtDescr.Text = "", " Purchase /", "/ Purchase / ") & txtSuppName.Text, 249)
        setAcctrDetValue(LinkNo, Val(txtPurchaseName.Tag), Trim(txtReference.Text), DrTrRef, Dramt * FCRt, txtJob.Text, "", 0, 0, "", _
                             "", Val(txtSuppAlias.Tag), "", cmbfc.Text, FCRt)
        If chktaxInv.Checked And Not chkimport.Checked And Not dtTax Is Nothing Then
            For i = 0 To dtTax.Rows.Count - 1
                If Val(dtTax(i)("ACid")) > 0 And Val(dtTax(i)("Amount")) > 0 Then
                    TxAmount = Math.Round(CDbl(dtTax(i)("Amount")), NoOfDecimal)
                    setAcctrDetValue(LinkNo, Val(dtTax(i)("ACid")), Trim(txtReference.Text), dtTax(i)("AccountName") & " Tax Paid", TxAmount * FCRt, Strings.Left(txtJob.Text, 50), "", 0, 0, "", "", Val(txtSuppAlias.Tag), Val(txtSuppAlias.Tag) & txtReference.Text, cmbfc.Text, FCRt)
                    '_objTr.saveAccTrans()
                End If
            Next
        End If

        'Credit Entry
        If Val(txtroundOff.Text) > 0 Then
            Dramt = Dramt - (IIf(cmbsign.SelectedIndex = 1, 1, -1) * CDbl(txtroundOff.Text))
        End If
        Dramt = CDbl(lblNetAmt.Text) * -1

        setAcctrDetValue(LinkNo, Val(txtSuppAlias.Tag), Trim(txtReference.Text), EntRef, CDbl(lblNetAmt.Text) * -1, "", Strings.Left(txtJob.Tag, 50), 0, 0, "", _
                         "", Val(txtSuppAlias.Tag), Val(txtSuppAlias.Tag) & txtReference.Text, cmbfc.Text, FCRt)
        '_objTr.saveAccTrans()
        'DiscountEntry
        If (CDbl(numDisc.Text)) > 0 And DiscAcc > 0 Then
            Dramt = -1 * (CDbl(numDisc.Text)) * FCRt
            setAcctrDetValue(LinkNo, DiscAcc, Trim(txtReference.Text), Trim(txtDescr.Text), Dramt, "", "", 2, 0, "", _
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
                        If txtCashSuppName.Text = "" Then
                            customername = txtSuppName.Text
                        Else
                            customername = txtCashSuppName.Text
                        End If
                        Dim collectionRef As String = ""
                        For j = 0 To dtMultipleDebits.Rows.Count - 1
                            If CDbl(dtMultipleDebits(j)("accAmt")) < 0 Or enableMultipleDebitAsCreditCollection Then
                                collectionRef = "Paid On " & Trim(txtprefix.Text) & IIf(txtprefix.Text = "", "", "/") & numVchrNo.Text & "[" & txtCashSuppName.Text & _
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
        LinkNo = 0
        UpdateOtherCost(LinkNo)
        _objTr.SaveAccTrWithDt(dtAccTb)
        '
        'updateClosingBalanceForInvoice(TrId)
    End Sub
    Private Sub setInvCmnValue(ByVal InvTrid As Long, ByVal paidamt As Double)
        With _objInv
            Dt = DateValue(Date.Now)
            .TrId = InvTrid
            .TrDate = DateValue(cldrdate.Value)
            .TrType = "IP"
            .DocLstTxt = txtDOLst.Text
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
            .TypeNo = TrTypeNo ' getVouchernumber("IP")
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
            .DueDate = DateValue(clrDuedate.Value)
            .DocDate = Dt
            .SuppInvDate = Dt
            .TermsId = ""
            .MchName = MACHINENAME
            .isModi = isModi
            .lpoclass = ""
            If Val(txtroundOff.Text) = 0 Then txtroundOff.Text = 0
            .rndoff = CDbl(txtroundOff.Text) * IIf(cmbsign.SelectedIndex = 1, -1, 1)
            .TaxType = Val(lblstatecode.Tag)
            .InvTypeNo = Val(cmbVoucherTp.Tag)
            .isTaxInvoice = chktaxInv.Checked
            .isImportOrExport = IIf(chkimport.Checked, 1, 0)
            .isthroughcustoms = cmbbycustoms.SelectedIndex
            .taxwithoutLineDiscount = IIf(chktaxwithoutLinediscount.Checked, 1, 0)
            .CashCustName = MkDbSrchStr(IIf(txtCashSuppName.Text = "", txtSuppName.Text, txtCashSuppName.Text))
            .tenderd = paidamt
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
                If .Item(ConstSlNo, i).Value.ToString <> "M" And Val(.Item(ConstItemID, i).Value) = 0 Then GoTo nxt1
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
            Next
nxt1:
        End With
        _objInv.savebulktoInvTr(dtInvTb)
        Return TDrAmt
    End Function
    Private Sub setInvDetValue(ByVal Invid As Long, ByVal PPerU As Single, ByVal i As Integer)
        With grdVoucher
            _objInv.dtTrId = Invid
            _objInv.ItemId = IIf(.Item(ConstSlNo, i).Value.ToString <> "L", Val(.Item(ConstItemID, i).Value), 0)
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

            If Val(.Item(ConstDisAmt, i).Value & "") = 0 Then .Item(ConstDisAmt, i).Value = 0
            _objInv.ItemDiscount = CDbl(.Item(ConstDisAmt, i).Value) * FCRt / PPerU
            If Val(.Item(ConstDisP, i).Value) = 0 Then .Item(ConstDisP, i).Value = 0
            _objInv.DisP = CDbl(.Item(ConstDisP, i).Value) * FCRt / PPerU

            If Not IsDBNull(.Item(ConstDescr, i).Value) Then
                _objInv.IDescription = IIf(.Item(ConstSlNo, i).Value.ToString = "M", .Item(ConstDescr, i).Value, Trim(.Item(ConstDescr, i).Value))
            End If
            _objInv.SlNo = i + 1
            _objInv.TrTypeNo = getVouchernumber("IP")
            _objInv.TrDateNo = getDateNo(CDate(cldrdate.Value))
            _objInv.TrType = "IP"
            _objInv.id = Val(.Item(ConstId, i).Value)
            _objInv.WarrentyName = .Item(ConstLocation, i).Value
            If Trim(.Item(ConstSerialNo, i).Value & "") = "" And enableBatchwiseTr Then
                _objInv.SerialNo = "IP" & txtprefix.Text & numVchrNo.Text
            Else
                _objInv.SerialNo = Trim(.Item(ConstSerialNo, i).Value & "")
            End If


            _objInv.HSNCode = Trim(.Item(ConstBarcode, i).Value & "")
            If Val(.Item(ConstCGSTP, i).Value & "") = 0 Then .Item(ConstCGSTP, i).Value = 0
            _objInv.CSGTP = CDbl(.Item(ConstCGSTP, i).Value)
            If Val(.Item(ConstSGSTP, i).Value & "") = 0 Then .Item(ConstSGSTP, i).Value = 0
            _objInv.SGSTP = CDbl(.Item(ConstSGSTP, i).Value)
            If Val(.Item(ConstIGSTP, i).Value & "") = 0 Then .Item(ConstIGSTP, i).Value = 0
            _objInv.IGSTP = CDbl(.Item(ConstIGSTP, i).Value)

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
            _objInv.impDocid = Val(.Item(ConstImpDocId, i).Value & "")
            _objInv.impDocSlno = Val(.Item(ConstImpLnId, i).Value & "")
        End With
    End Sub
    Private Sub setAcctrCmnValue(ByVal InvTrid As Long, ByVal LinkNo As Long)
        _objTr.JVType = "IP"
        _objTr.JVDate = DateValue(cldrdate.Value)
        _objTr.PreFix = Trim(txtprefix.Text)
        _objTr.JVNum = Val(numVchrNo.Text)
        _objTr.JVTypeNo = TrTypeNo 'getVouchernumber("IP")
        _objTr.UserId = Trim(CurrentUser & "")
        _objTr.MchName = MACHINENAME
        _objTr.CrtDtTm = Dt
        _objTr.TypeNo = TrTypeNo ' getVouchernumber("IP")
        '_objTr.VrDescr = Trim(txtpayee.Text)
        _objTr.IsModi = IIf(LinkNo > 0, 1, 0)
        _objTr.LinkNo = InvTrid
        If Val(lbltaxablecost.Text) = 0 Then lbltaxablecost.Text = 0
        If Val(lbltaxable.Text) = 0 Then lbltaxable.Text = 0
        If Val(lbltax.Text) = 0 Then lbltax.Text = 0
        If Val(lblothercosttax.Text) = 0 Then lblothercosttax.Text = 0
        _objTr.taxablevalue = CDbl(lbltaxable.Text) + CDbl(lbltaxablecost.Text)
        _objTr.taxvalue = CDbl(lbltax.Text) + CDbl(lblothercosttax.Text)
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
            .DueDate = clrDuedate.Value
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
        dtrow("Reference") = Trim(txtReference.Text)
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
        'Dim dt As DataTable = _objcmnbLayer._fldDatatable("SELECT CurrencyCode FROM CurrencyTb", False)

        cmbfc.Items.Clear()
        cmbfc.Items.Add("")
        Dim i As Integer
        For i = 0 To dtcurrentyTb.Rows.Count - 1
            cmbfc.Items.Add(dtcurrentyTb(i)("CurrencyCode"))
        Next
    End Sub

    Private Sub PurchaseInvoiceFrm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
        If Not fSelect Is Nothing Then fSelect.Close() : fSelect = Nothing
        If Not fProductEnquiry Is Nothing Then fProductEnquiry.Close() : fProductEnquiry = Nothing
        If Not fSlctDoc Is Nothing Then fSlctDoc.Close() : fSlctDoc = Nothing
        If Not fSerialno Is Nothing Then fSerialno.Close() : fSerialno = Nothing
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
        dtTable = _objcmnbLayer._fldDatatable("SELECT * FROM ItmInvCmnTb WHERE TrType = 'IP' AND InvNo = " & Val(numVchrNo.Text) & " AND PreFix = '" & MkDbSrchStr(Trim(txtprefix.Text), True) & "'")
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
            'dtSrlNo = _objcmnbLayer._fldDatatable("SELECT SerialNo,itemid FROM ItmInvTrTb Left Join ItmInvCmnTb on ItmInvTrTb.Trid=ItmInvCmnTb.trid WHERE TrType='IP' AND ItmInvCmnTb.Trid<>" & loadedTrId)
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
        grdVoucher.CurrentCell = grdVoucher.Item(ConstDescr, grdVoucher.CurrentRow.Index)
        grdBeginEdit()
        chgbyprg = False
    End Sub

    Private Sub numDisc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles numDisc.Click
        numDisc.SelectAll()
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
        NumericTextOnKeypress(sender, e, chgbyprgN, lnumformat)
    End Sub

    Private Sub numDisc_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numDisc.TextChanged
        If chgbyprgN Then Exit Sub
        'btnUpdate.Enabled = True
        'chgAmt = True
        'doCommandStat(True)
        'If Val(numDisc.Text) > 0 Then
        '    lblNetAmt.Text = Format(CDbl(lblTotAmt.Text) - CDbl(numDisc.Text), "0.00")
        'End If
        chgPost = True
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
        setSupplier()
    End Sub
    Private Sub setSupplier(Optional ByVal accid As Long = 0)
        Dim dt As DataTable
        If txtSuppAlias.Text = "" And accid = 0 Then GoTo els
        Dim condition As String
        If accid > 0 Then
            condition = "where accid=" & accid
        Else
            condition = "where Alias='" & txtSuppAlias.Text & "'"
        End If
        dt = _objcmnbLayer._fldDatatable("SELECT AccMast.accid,Alias,AccDescr,Address1,Address2,Address3,Address4," & _
                                        "Phone,Fax,EMail,PT1,PT2,PT3,PT4,TrdLcno,TrdDate,CreditLimit,DueDays,SlsmanId," & _
                                        "CurrencyCode,CountryCode,GSTIN,Remarks,GrpSetOn " & _
                                        " from AccMast LEFT JOIN AccMastAddr ON AccMast.accid=AccMastAddr.AccountNo " & _
                                        "LEFT JOIN S1AccHd ON AccMast.S1AccId=S1AccHd.S1AccId " & _
                                        condition)
        If dt.Rows.Count > 0 Then
            txtSuppAlias.Tag = dt(0)("accid")
            lbladd1.Text = Trim("" & dt(0)("Address1"))
            lbladd2.Text = Trim("" & dt(0)("Address2"))
            lbladd3.Text = Trim("" & dt(0)("Address3"))
            lbladd4.Text = Trim("" & dt(0)("Address4"))
            lbladd5.Text = Trim("" & dt(0)("Phone"))
            lbladd6.Text = Trim("" & dt(0)("Email"))
            If UCase(Trim("" & dt(0)("GrpSetOn"))) <> "SUPPLIER" Then
                isNotSupplierAccount = True
            Else
                isNotSupplierAccount = False
            End If
            txtCashSuppName.Text = Trim("" & dt(0)("AccDescr"))
            If enableGCC Then
                lblstatecode.Text = "Emirate : "
            Else
                lblstatecode.Text = "State Code : "
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
            chgbyprg = True
            txtSuppAlias.Text = Trim("" & dt(0)("Alias"))
            txtSuppName.Text = Trim("" & dt(0)("AccDescr"))
            chgbyprg = False
            'If accid > 0 Then
            '    txtSuppAlias.Text = Trim("" & dt(0)("Alias"))
            '    txtSuppName.Text = Trim("" & dt(0)("AccDescr"))
            'End If
            If EnableGST Then CalculateGST()
            If Val(dt(0)("CreditLimit") & "") = 0 Then
                dt(0)("CreditLimit") = 0
            End If
            lbllimit.Text = Format(Val(dt(0)("CreditLimit")), lnumformat)
            Dim iNBal As Double = getAccBal(Val(txtSuppAlias.Tag))
            lblbalance.Text = Strings.Format(iNBal, lnumformat)
            If IsDBNull(dt(0)("DueDays")) Then
                dt(0)("DueDays") = 0
            End If
            If Val(dt(0)("DueDays") & "") > 0 Then
                iNBal = getAccAegBal(Val(txtSuppAlias.Tag), DateValue(DateTime.Now), Val(dt(0)("DueDays")))
                lblInvoices.Text = Strings.Format(iNBal, lnumformat)
                clrDuedate.Value = DateAdd(DateInterval.Day, Val(dt(0)("DueDays")), cldrdate.Value)
            Else
                lblInvoices.Text = Format(0, numFormat)
                clrDuedate.Value = DateValue(Date.Now)
            End If
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
            txtCashSuppName.Text = ""
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
            .strType = "IP"
            .Text = "Select Purchase Invoice"
            .ShowDialog()
        End With
        If Not fSlctDoc Is Nothing Then fSlctDoc = Nothing
    End Sub

    Private Sub fSlctDoc_closetr() Handles fSlctDoc.closetr
        AddNewClick()
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
       
        'btnUpdate.Enabled = Val(btnUpdate.Tag) > 0
        chgPost = True
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
            fRptFormat.RptType = "IP"
            fRptFormat.ShowDialog()
            fRptFormat = Nothing
        Else
            PrepareRpt("IP")
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

            'Dim itemidsdatatable As New DataTable
            'Dim trdate As Date
            'itemidsdatatable = _objcmnbLayer._fldDatatable("SELECT Itemid,TrDate,SerialNo from ItmInvTrTb " & _
            '"Right Join ItmInvCmnTb ON ItmInvTrTb.Trid=ItmInvCmnTb.Trid  WHERE ItmInvCmnTb.TrId =" & loadedTrId)
            _objInv.TrId = loadedTrId
            _objInv.TrType = "IN"
            _objInv.deleteInventoryTransactions()

            'If itemidsdatatable.Rows.Count > 0 Then
            '    trdate = DateValue(itemidsdatatable(0)("TrDate"))

            '    For i = 0 To itemidsdatatable.Rows.Count - 1
            '        _objcmnbLayer._saveDatawithOutParm("UPDATE SerialNoTb SET PurchaseId=0 WHERE SerialNo='" & itemidsdatatable(i)("SerialNo") & "'")
            '        _objcmnbLayer._saveDatawithOutParm("delete from SerialNoTb where ISNULL(PurchaseId,0)=0 and ISNULL(SalesId,0)=0")

            '        _objInv.ItemId = Val(itemidsdatatable(i)("Itemid") & "")
            '        _objInv.TrDate = DateValue(itemidsdatatable(i)("TrDate"))
            '        If _objInv.ItemId > 0 Then
            '            _objInv.setcostAverageOnModification(UsrBr)
            '        End If

            '    Next
            'End If
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
        _objInv.TrType = "IP"
        Dim ds As DataSet = _objInv.ldInvoice("rturnInventoryDetailsForInvoicePrint")
        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = MdiParent
        frm.Text = RptCaption
        frm.Show()
    End Sub


    Private Sub txtroundOff_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
        calculate()
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
                If .Item(ConstB, .CurrentCell.RowIndex).Value = "B" Then
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

    Private Sub grdVoucher_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellEndEdit
        Dim col As Integer = e.ColumnIndex
        Dim ndec1 As Integer
        If col = ConstQty Or col = ConstUPrice Or col = ConstDisAmt Or col = ConstLTotal _
        Or col = ConstTaxP Or col = ConstDisP Or col = ConstWoodDiscQty Or col = ConstWoodQty _
        Or col = ConstMRP Or col = ConstSP1 Or col = ConstSP2 Or col = ConstSP3 Or col = ConstFocQty Then
            If col = ConstQty Or col = ConstWoodDiscQty Or col = ConstWoodQty Or col = ConstFocQty Then
                ndec1 = grdVoucher.Item(ConstPFraction, grdVoucher.CurrentRow.Index).Value
            Else
                ndec1 = NoOfDecimal
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
            txtSuppName.Focus()
        End If

    End Sub

    Private Sub cldrdate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cldrdate.ValueChanged
        chgPost = True
    End Sub

    Private Sub cmbVoucherTp_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVoucherTp.SelectedIndexChanged
        If chgbyprg Then Exit Sub
        isNotSupplierAccount = False
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
        Dim r As Integer
        With grdVoucher
            If .RowCount > 0 Then
                If Val(.Item(ConstItemID, .RowCount - 1).Value) > 0 Then
                    AddRow()
                    r = .RowCount - 1
                End If
            Else
                AddRow()
                r = .RowCount - 1
            End If
        End With

        SrchText = ItemCode
        r = grdVoucher.RowCount - 1
        grdVoucher.Item(ConstItemCode, r).Value = ItemCode
        chgItm = True
        Valid(r, ConstItemCode)
        Timer3.Enabled = True
        'FindNextCell(grdVoucher, r, ConstUnit)
        'grdVoucher.CurrentCell = grdVoucher.Item(ConstQty, r)
        'grdBeginEdit()
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
        If enableGCC = False And EnableGST = False Then Exit Sub
        CalculateGST()
        ShowTax.grdVoucher.DataSource = dtTax
        ShowTax.ShowDialog()
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
        txtdebit.Text = txtPurchaseName.Text
        txtdebit.Tag = Val(txtPurchaseName.Tag)
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
                                          "WHERE JVType = 'IP' AND JVNum = " & Val(numVchrNo.Text) & " AND PreFix = '" & MkDbSrchStr(Trim(txtprefix.Text)) & "') AND OthCost > 0  ORDER BY OthCost, DealAmt DESC")
        Else
            sRs = _objcmnbLayer._fldDatatable("SELECT AccTrDet.*, AccDescr, Alias, BranchId FROM AccTrDet " & _
                                          "LEFT JOIN AccMast ON AccMast.accid = AccTrDet.AccountNo " & _
                                          "WHERE  LinkNo IN (SELECT LinkNo FROM AccTrCmn WHERE JVType = 'IP' AND JVNum = " & Val(numVchrNo.Text) & " AND PreFix = '" & MkDbSrchStr(Trim(txtprefix.Text)) & "') AND OthCost > 0  ORDER BY OthCost, DealAmt DESC")
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
            lbltaxablecost.Text = Format(totothercostTaxable, lnumformat)
        Else
            lblothercosttax.Text = Format(0, lnumformat)
            lbltaxablecost.Text = Format(0, lnumformat)
            lblOthCost.Text = Format(0, lnumformat)
        End If



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

    Private Sub AddRowOthCost()

        Dim num As Integer
        With grdOtherCost
            If Val(.Tag) = 0 Then
                .Rows.Add(1)
                num = .RowCount - 1
            Else
                num = Val(.Tag) - 1
            End If
            If Not dtTax Is Nothing Then
                If dtTax.Rows.Count = 0 And enableGCC Then CalculateGST()
            End If

            .Item(CostDbName, num).Value = txtdebit.Text
            .Item(CostAmount, num).Value = Format(CDbl(numOtherAmt.Text), lnumformat)
            .Item(CostReference, num).Value = txtOthrRef.Text
            If txtOthrDescription.Text = "" Then
                .Item(CostDescr, num).Value = txtcredit.Text & " ON PURCHASE " & "/" & numVchrNo.Text
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
    Private Sub ldVat()
        Dim dt As DataTable
        Dim i As Integer
        dt = _objcmnbLayer._fldDatatable("SELECT vatcode FROM VatMasterTb")
        cmbtax.Items.Clear()
        cmbtax.Items.Add("")
        For i = 0 To dt.Rows.Count - 1
            cmbtax.Items.Add(dt(i)("vatcode"))
        Next
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
            txtotherTax.Tag = Val(grdOtherCost.Item(CostvatOther, rowIndex).Value)
            cmbtax.Text = grdOtherCost.Item(Costvatcode, rowIndex).Value
            cmbtax.Tag = grdOtherCost.Item(CostvatAcc, rowIndex).Value
            btnadd.Tag = grdOtherCost.Item(Costvatcode, rowIndex).Value
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
        If grdVoucher.Item(e.ColumnIndex, e.RowIndex).ReadOnly = False Then grdBeginEdit()
    End Sub

    Private Sub grdVoucher_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellContentClick

    End Sub

    Private Sub cmbsign_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbsign.KeyDown

    End Sub

    Private Sub cmbsign_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbsign.SelectedIndexChanged
        calculate()
    End Sub

    Private Sub btnfind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnfind.Click
        If grdVoucher.RowCount = 0 Then Exit Sub
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
    Private Sub calculateWoodDiscountQty(ByVal rIndex As Integer)
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
    End Sub

    Private Sub btntransferexcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btntransferexcel.Click
        If Not fTransferFromexel Is Nothing Then fTransferFromexel = Nothing
        fTransferFromexel = New TransferiItemsFromExcel
        With fTransferFromexel
            .isFromPurchase = True
            .ShowDialog()
        End With
        loadFromExcell()
    End Sub

    Private Sub fTransferFromexel_loadtoPurchase(ByVal dt As System.Data.DataTable) Handles fTransferFromexel.loadtoPurchase
        If Not dtloadFromExcell Is Nothing Then
            If dtloadFromExcell.Rows.Count > 0 Then dtloadFromExcell.Rows.Clear()
        End If
        dtloadFromExcell = dt
    End Sub
    Private Sub loadFromExcell()
        Try
            Dim i As Integer
            chgbyprg = True
            Dim unitDiscount As Double

            For i = 0 To dtloadFromExcell.Rows.Count - 1
                unitDiscount = 0
                With grdVoucher
                    If Trim(dtloadFromExcell(i)("Item Name") & "") = "" Then GoTo nxt
                    .Rows.Add()
                    'Dim a As Integer = .Item(ConstQty, 2).Value
                    .Item(ConstItemCode, i).Value = Trim(dtloadFromExcell(i)("Item Code") & "")
                    chgItm = False
                    .CurrentCell = .Item(ConstItemCode, i)
                    chgItm = True
                    SrchText = Trim(dtloadFromExcell(i)("Item Code") & "")
                    Valid(i, ConstItemCode)
                    chgItm = False
                    .Item(ConstSlNo, i).Value = i + 1
                    .Item(ConstDescr, i).Value = Trim(dtloadFromExcell(i)("Item Name") & "")
                    If Val(dtloadFromExcell(i)("Purchase Cost") & "") = 0 Then dtloadFromExcell(i)("Purchase Cost") = 0
                    If Val(dtloadFromExcell(i)("qty") & "") = 0 Then dtloadFromExcell(i)("qty") = 0
                    If Val(dtloadFromExcell(i)("TotalItemDiscount") & "") = 0 Then dtloadFromExcell(i)("TotalItemDiscount") = 0
                    If Val(dtloadFromExcell(i)("ItemUnitDiscount") & "") = 0 Then dtloadFromExcell(i)("ItemUnitDiscount") = 0
                    If dtloadFromExcell(i)("ItemUnitDiscount") = 0 And dtloadFromExcell(i)("TotalItemDiscount") > 0 And dtloadFromExcell(i)("qty") > 0 Then
                        unitDiscount = dtloadFromExcell(i)("TotalItemDiscount")
                    Else
                        unitDiscount = dtloadFromExcell(i)("ItemUnitDiscount") * dtloadFromExcell(i)("qty")
                    End If

                    .Item(ConstActualPrice, i).Value = CDbl(dtloadFromExcell(i)("Purchase Cost"))
                    .Item(ConstUPrice, i).Value = Format(CDbl(dtloadFromExcell(i)("Purchase Cost")), numFormat)
                    .Item(ConstQty, i).Value = Format(CDbl(dtloadFromExcell(i)("qty")), numFormat)
                    If unitDiscount > 0 Then
                        .Item(ConstDisAmt, i).Value = Format(CDbl(unitDiscount), numFormat)
                        unitDiscount = unitDiscount / CDbl(.Item(ConstQty, i).Value)
                        .Item(ConstDisP, i).Value = Format((unitDiscount * 100) / CDbl(.Item(ConstActualPrice, i).Value), lnumformat)
                    End If
                    calcualteLineTotal(i)
                End With
nxt:
            Next
            calculate()
            'With grdVoucher
            '    activecontrolname = "grdVoucher"
            '    .CurrentCell = .Item(ConstItemCode, i)
            '    .BeginEdit(True)
            'End With
            chgbyprg = False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub
    Private Sub PrintfromBarTender(ByVal isfromGrid As Boolean)
        Dim fl As System.IO.StreamWriter
        Dim query As String
        Dim i As Integer
        Dim dt As DataTable
        Dim isTaxPrice As Boolean
        Dim unitprice As Double
        Dim formatname As String = ""
        Dim trqty As Double
        isTaxPrice = isPrintTaxPrice()
        If bartenderpath = "" Then
            bartenderpath = DPath & "\barcode.txt"
        End If
        If Not FileExists(bartenderpath) Then
            File.Copy(Application.StartupPath & "\barcode.txt", bartenderpath)
        End If
        fl = My.Computer.FileSystem.OpenTextFileWriter(bartenderpath, False)
        query = "ProductName,Code,Rate,MRP,PDate,Edate,Nos"
        If isfromGrid Then
            dt = _objcmnbLayer._fldDatatable("SELECT *,(((isnull(IGST,0)+ISNULL(vat,0))*UnitPrice)/100)+UnitPrice [Tax Price] from " & _
                                         "(Select [Item Code],Description,UnitPrice,mrp," & _
                                         " IGST,vat FROM InvItm" & _
                                         " LEFT JOIN GSTTb ON GSTTb.HSNCode=InvItm.HSNCode " & _
                                         " LEFT JOIN VatMasterTb ON VatMasterTb.vatid=InvItm.vatid " & _
                                         "where itemid=" & Val(grdVoucher.Item(ConstItemID, grdVoucher.CurrentRow.Index).Value) & ")tr")
            If dt.Rows.Count > 0 Then
                With grdVoucher
                    Dim r As Integer = .CurrentRow.Index
                    Dim taxp As Double = .Item(ConstTaxP, r).Value
                    If Val(.Item(ConstSP1, r).Value) = 0 Then .Item(ConstSP1, r).Value = 0
                    Dim taxprice As Double = CDbl(.Item(ConstSP1, r).Value)
                    If Val(.Item(ConstMRP, r).Value) = 0 Then .Item(ConstMRP, r).Value = 0
                    Dim mrp As Double = CDbl(.Item(ConstMRP, r).Value)
                    If mrp = 0 Then
                        mrp = CDbl(dt(i)("MRP"))
                    End If
                    Dim manufacturingdate As Date
                    Dim WarrentyExpDate As Date
                    trqty = CDbl(.Item(ConstQty, r).Value)
                    If Trim(.Item(ConstManufacturingdate, r).Value & "") <> "" Then
                        If DateValue(.Item(ConstManufacturingdate, r).Value) > DateValue("01/01/1950") Then
                            manufacturingdate = .Item(ConstManufacturingdate, r).Value
                        End If
                    End If
                    If Trim(.Item(ConstWarrentyExpiry, r).Value & "") <> "" Then
                        If DateValue(.Item(ConstWarrentyExpiry, r).Value) > DateValue("01/01/1950") Then
                            WarrentyExpDate = .Item(ConstWarrentyExpiry, r).Value
                        End If
                    End If
                   
                    If isTaxPrice Then
                        If taxprice > 0 Then
                            unitprice = taxprice + ((taxprice * taxp) / 100)
                        Else
                            unitprice = dt(0)("Tax Price")
                        End If
                    Else
                        If taxprice > 0 Then
                            unitprice = taxprice
                        Else
                            unitprice = dt(0)("UnitPrice")
                        End If
                    End If
                    Dim frm As New SelectBarcodeFormat
                    frm.txtcopy.Text = trqty
                    frm.ShowDialog()
                    formatname = frm.cmbformat.Tag
                    trqty = Val(frm.txtcopy.Text)
                    Dim nformat As String = "0" & IIf(NoOfDecimal = 0, "", "." & StrDup(NoOfDecimal, "0"))
                    query = query & vbCrLf & dt(i)("Description") & "," & dt(i)("Item Code") & "," & _
                    Format(CDbl(unitprice), nformat) & "," & Format(CDbl(mrp), nformat) & "," & _
                    Format(manufacturingdate, "dd/MM/yyyy") & "," & Format(WarrentyExpDate, "dd/MM/yyyy") & "," & trqty
                End With
               
            End If

        Else
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
                    'dt = _objcmnbLayer._fldDatatable("SELECT [Item Code],Description,UnitPrice," & _
                    '                                 "(((isnull(IGST,0)+ISNULL(vat,0))*UnitPrice)/100)+UnitPrice [Tax Price],mrp, " & _
                    '                                 " WarrentyExpDate,manufacturingdate  FROM InvItm" & _
                    '                                 " LEFT JOIN GSTTb ON GSTTb.HSNCode=InvItm.HSNCode " & _
                    '                                 " LEFT JOIN VatMasterTb ON VatMasterTb.vatid=InvItm.vatid " & _
                    '                                 "where itemid=" & Val(.Item(ConstItemID, i).Value))
                    If dt.Rows.Count > 0 Then
                        If isTaxPrice Then
                            unitprice = dt(0)("Tax Price")
                        Else
                            unitprice = dt(0)("UnitPrice")
                        End If
                        query = query & vbCrLf & dt(i)("Description") & "," & dt(i)("Item Code") & "," & _
                        CDbl(unitprice) & "," & CDbl(dt(i)("MRP")) & "," & _
                        Format(dt(i)("manufacturingdate"), "dd/MM/yyyy") & "," & Format(dt(i)("WarrentyExpDate"), "dd/MM/yyyy") & "," & Val(dt(i)("trqty"))
                        'query = query & vbCrLf & .Item(ConstDescr, i).Value & "," & .Item(ConstItemCode, i).Value & "," & Format(CDbl(unitprice), numFormat) & "," & Val(.Item(ConstQty, i).Value)
                    End If
                End With
            Next
        End If
        fl.WriteLine(query)
        fl.Close()
        Dim appstart As String = Application.StartupPath & "\BarTender\bartend.exe"
        If FileExists(appstart) = False Then
            MsgBox("Bar Code Application not found! Please contact vendor", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        dt = _objcmnbLayer._fldDatatable("SELECT barid FROM BarcodeFormatTb")
        If Not isfromGrid Then
            If dt.Rows.Count > 1 Then
                Dim frm As New SelectBarcodeFormat
                frm.ShowDialog()
                formatname = frm.cmbformat.Tag
            Else
                dt = _objcmnbLayer._fldDatatable("SELECT path FROM BarcodeFormatTb")
                If dt.Rows.Count > 0 Then
                    formatname = dt(0)("path")
                End If
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
        PrintfromBarTender(chksinglebarcode.Checked)
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
        Dim branchstr As String = ""
        If UsrBr <> "" Then
            branchstr = " LEFT JOIN (SELECT locQIH,itemid litemid,lastcost from LocOpnQtyTb " & _
                        "left join LocationTb on LocationTb.LocationID=LocOpnQtyTb.LocationID " & _
                        "where LocCode='" & Dloc & "')LocationTb on InvItm.itemid=LocationTb.litemid "
        End If
        If dtItemInfo Is Nothing Then
            dtItemInfo = _objcmnbLayer._fldDatatable("Select Rack,QIH, MRP,Price,[Tax Price],WSP,[C Avg],[Cost+Tax]," & _
                                                     "case when isnull(LPC,0)=0 then opcost else LPC end LPC,itemid from (SELECT Rack," & IIf(UsrBr = "", "QIH", "isnull(locQIH,0)") & " QIH, MRP,UnitPrice Price,(((isnull(IGST,1)+ISNULL(vat,0))*UnitPrice)/100)+UnitPrice [Tax Price]," & _
                                                     "UnitPriceWS WSP,CostAvg [C Avg]," & _
                                                     "(((isnull(IGST,0)+ISNULL(vat,0))*CostAvg)/100)+CostAvg [Cost+Tax]," & _
                                                     IIf(UsrBr = "", "LastPurchCost", "isnull(lastcost,0)") & " LPC,itemid,opcost FROM INVITM " & _
                                                      " LEFT JOIN GSTTb ON GSTTb.HSNCode=InvItm.HSNCode" & _
                                                      " LEFT JOIN VatMasterTb ON VatMasterTb.vatid=InvItm.vatid " & branchstr & _
                                                     " where itemid=" & itemid & ") tr")




            grdItemInfo.DataSource = dtItemInfo
            SetGridItemInfo()
        Else
            If itemid = 0 And dtItemInfo.Rows.Count > 0 Then dtItemInfo.Rows.Clear() : grdItemInfo.DataSource = dtItemInfo
            Dim _qurey As EnumerableRowCollection(Of DataRow)
            _qurey = (From data In dtItemInfo.AsEnumerable() Where data("itemid") = itemid Select data)
            If _qurey.Count = 0 Then
                dt = _objcmnbLayer._fldDatatable("Select Rack,QIH, MRP,Price,[Tax Price],WSP,[C Avg],[Cost+Tax]," & _
                                                 "case when isnull(LPC,0)=0 then opcost else LPC end LPC,itemid from (SELECT Rack," & IIf(UsrBr = "", "QIH", "isnull(locQIH,0)") & " QIH,MRP,UnitPrice Price,(((isnull(IGST,0)+ISNULL(vat,0))*UnitPrice)/100)+UnitPrice [Tax Price]," & _
                                                 "UnitPriceWS WSP,CostAvg [C Avg],((isnull(IGST,0)*CostAvg)/100)+CostAvg [Cost+Tax]," & _
                                                 IIf(UsrBr = "", "LastPurchCost", "isnull(lastcost,0)") & " LPC,itemid,opcost FROM INVITM " & _
                                                  " LEFT JOIN GSTTb ON GSTTb.HSNCode=InvItm.HSNCode" & _
                                                  " LEFT JOIN VatMasterTb ON VatMasterTb.vatid=InvItm.vatid " & branchstr & _
                                                 " where itemid=" & itemid & ") tr")
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

    Private Sub chkTaxbill_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chktaxInv.CheckedChanged
        calculate()
    End Sub


    Private Sub chktaxwithoutLinediscount_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chktaxwithoutLinediscount.CheckedChanged
        If chgbyprg Then Exit Sub
        calculate(, True)
    End Sub
    Private Sub PasteFrom(ByVal trid As Long, Optional ByVal sRs As DataTable = Nothing)
        Dim i As Integer
        Dim UPerPack As Double
        'sRs = _objcmnbLayer._fldDatatable("SELECT ItmInvTrTb.*, [Item Code],Warrentyname,isSerialNo,paymentAC FROM ItmInvTrTb LEFT JOIN InvItm ON InvItm.ItemId = ItmInvTrTb.ItemId " & _
        '                                  " LEFT JOIN InvItmPropertiesTb ON InvItmPropertiesTb.Itemid=invitm.itemid " & _
        '                                  "LEFT JOIN WarrentyMastertb ON WarrentyMastertb.Wid=ItmInvTrTb.WarrentyId " & _
        '                                   "LEFT JOIN VatMasterTb ON InvItm.vatId=VatMasterTb.vatId " & _
        '                                  "WHERE TrId = " & trid & " ORDER BY SlNo")

        If sRs Is Nothing Then
            sRs = _objcmnbLayer._fldDatatable("SELECT ItmInvTrTb.*, [Item Code],Warrentyname,isSerialNo,FraCount," & _
                                          "isnull(itemCategory,'')itemCategory,paymentAC,vat,rgcess,rgcaccount,additionalcess FROM ItmInvTrTb LEFT JOIN InvItm ON InvItm.ItemId = ItmInvTrTb.ItemId " & _
                                          " LEFT JOIN InvItmPropertiesTb ON InvItmPropertiesTb.Itemid=invitm.itemid " & _
                                          "LEFT JOIN WarrentyMastertb ON WarrentyMastertb.Wid=ItmInvTrTb.WarrentyId " & _
                                          "LEFT JOIN UnitsTb ON UnitsTb.Units=ItmInvTrTb.Unit " & _
                                          "LEFT JOIN VatMasterTb ON InvItm.vatId=VatMasterTb.vatId " & _
                                          "LEFT JOIN (select vatcode rgccode,vat rgcess, paymentAC rgcaccount,vatid rgcid from  VatMasterTb)regularcess ON InvItm.regularcessid=regularcess.rgcid " & _
                                          "WHERE TrId = " & trid & " ORDER BY SlNo")
        End If

        
        grdVoucher.RowCount = 0
        If _objcmnbLayer.dtSerialNo.Rows.Count > 0 Then _objcmnbLayer.dtSerialNo.Rows.Clear()
        Dim tNumformat As String
        chgbyprg = True
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
                    grdVoucher.Item(ConstLocation, i).Value = Trim("" & sRs(i)("Warrentyname")) ' !Colour
                    If Val(grdVoucher.Item(ConstPFraction, i).Value & "") = 0 Then grdVoucher.Item(ConstPFraction, i).Value = 0
                    grdVoucher.Item(ConstQty, i).Value = Format(sRs(i)("TrQty") / UPerPack, "#,##0" & IIf(Val(grdVoucher.Item(ConstPFraction, i).Value) = 0, "", "." & Strings.StrDup(CByte(grdVoucher.Item(ConstPFraction, i).Value), "0")))
                    If Val(sRs(i)("Focqty") & "") = 0 Then sRs(i)("Focqty") = 0
                    grdVoucher.Item(ConstFocQty, i).Value = Format(sRs(i)("Focqty") / UPerPack, "#,##0" & IIf(Val(grdVoucher.Item(ConstPFraction, i).Value) = 0, "", "." & Strings.StrDup(CByte(grdVoucher.Item(ConstPFraction, i).Value), "0")))
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


                    If Val(sRs(i)("taxAmt") & "") = 0 Then
                        sRs(i)("taxAmt") = 0
                    End If
                    grdVoucher.Item(ConstTaxAmt, i).Value = sRs(i)("taxAmt") / FCRt
                    grdVoucher.Item(ConstTaxP, i).Value = sRs(i)("taxP")

                    If Val(sRs(i)("vat") & "") = 0 Then sRs(i)("vat") = 0
                    If Val(sRs(i)("rgcess") & "") = 0 Then sRs(i)("rgcess") = 0
                    If Not enableGCC Then
                        grdVoucher.Item(Constcess, i).Value = Format(sRs(i)("vat"), lnumformat)
                        grdVoucher.Item(ConstRegcess, i).Value = Format(sRs(i)("rgcess"), lnumformat)
                    Else
                        grdVoucher.Item(Constcess, i).Value = 0
                        grdVoucher.Item(ConstRegcess, i).Value = 0
                    End If

                    If Val(sRs(i)("CessAmt") & "") = 0 Then sRs(i)("CessAmt") = 0
                    grdVoucher.Item(ConstcessAmt, i).Value = Format(sRs(i)("CessAmt") / FCRt, lnumformat)
                    If Val(sRs(i)("regularcessAmt") & "") = 0 Then sRs(i)("regularcessAmt") = 0
                    grdVoucher.Item(ConstregularCessAmt, i).Value = Format(sRs(i)("regularcessAmt") / FCRt, lnumformat)
                    If Val(sRs(i)("FloodcessAmt") & "") = 0 Then sRs(i)("FloodcessAmt") = 0
                    grdVoucher.Item(ConstFloodCessAmt, i).Value = Format(sRs(i)("FloodcessAmt") / FCRt, lnumformat)
                    If Val(sRs(i)("additionalcess") & "") = 0 Then sRs(i)("additionalcess") = 0
                    grdVoucher.Item(ConstAdditionalcess, i).Value = sRs(i)("additionalcess") / FCRt
                    If Val(sRs(i)("paymentAC") & "") = 0 Then sRs(i)("paymentAC") = 0
                    grdVoucher.Item(ConstcessAc, i).Value = Val(sRs(i)("paymentAC"))
                    If Val(sRs(i)("rgcaccount") & "") = 0 Then sRs(i)("rgcaccount") = 0
                    grdVoucher.Item(ConstRegcessAc, i).Value = Val(sRs(i)("rgcaccount"))


                    .Item(constItmTot, i).Value = Format((.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value), lnumformat)
                    'grdVoucher.Item(ConstMthd, i).Value = sRs(i)("Method")
                    'grdVoucher.Item(ConstUnitVal, i).Value = sRs(i)("UnitValue") * UPerPack / FCRt
                    'grdVoucher.Item(ConstDiscOther, i).Value = sRs(i)("DiscOther") * UPerPack / FCRt
                    grdVoucher.Item(ConstDiscOther, i).Value = sRs(i)("UnitDiscount") * UPerPack / FCRt
                    grdVoucher.Item(ConstImpDocId, i).Value = Val(sRs(i)("impDocid") & "")
                    grdVoucher.Item(ConstImpLnId, i).Value = Val(sRs(i)("impDocSlno") & "")
                    grdVoucher.Item(ConstId, i).Value = IIf(loadedTrId > 0, sRs(i)("id"), 0)
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
                    If Val(sRs(i)("SP1") & "") = 0 Then sRs(i)("SP1") = 0
                    grdVoucher.Item(ConstSP1, i).Value = Format(sRs(i)("SP1"), numFormat)
                    If Val(sRs(i)("SP2") & "") = 0 Then sRs(i)("SP2") = 0
                    grdVoucher.Item(ConstSP2, i).Value = Format(sRs(i)("SP2"), numFormat)
                    If Val(sRs(i)("SP3") & "") = 0 Then sRs(i)("SP3") = 0
                    grdVoucher.Item(ConstSP3, i).Value = Format(sRs(i)("SP3"), numFormat)
                    If Val(sRs(i)("MRP") & "") = 0 Then sRs(i)("MRP") = 0
                    grdVoucher.Item(ConstMRP, i).Value = Format(sRs(i)("MRP"), numFormat)
                    If enableWoodSale Then
                        tNumformat = "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))
                        grdVoucher.Item(ConstWoodQty, i).Value = Format(sRs(i)("WoodNetQty") / UPerPack, tNumformat)
                        grdVoucher.Item(ConstWoodDiscQty, i).Value = Format(sRs(i)("WoodDiscountQty") / UPerPack, tNumformat)
                    End If
                    'If enableGCC Then
                    '    If Val(sRs(i)("paymentAC") & "") = 0 Then sRs(i)("paymentAC") = 0
                    '    grdVoucher.Item(ConstcessAc, i).Value = Val(sRs(i)("paymentAC"))
                    'End If


                    calcualteLineTotal(i)
                Next
                reArrangeNo()
            End If
        End With
        If sRs.Rows.Count > 0 Then sRs.Clear()
    End Sub

    Private Sub txtroundOff_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtroundOff.TextChanged
        calculate()
    End Sub

    Private Sub PurchaseInvoiceFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            StrAccMastSrch = "SELECT AccDescr, Alias, ClosingBal As Balance, OpnBal, accid, S1AccHd.S1AccId, AccSetId, GrpSetOn FROM" & _
                            " AccMast INNER JOIN S1AccHd ON S1AccHd.S1AccId = AccMast.S1AccId "
            lnumformat = numFormat
            'btnNext_Click(btnNext, New System.EventArgs())
            loadInventoryFormLoadMasters(False, 6, "IP", 2, DiscAcc, TrTypeNo)

            If ShowTaxOnInventory Then
                'dtTax = _objcmnbLayer._fldDatatable("SELECT vatcode,convert(money, 0) Amount,collectionAC,paymentAC,vat From VatMasterTb", False)
                CreateTaxTable(dtTax)
                chktaxInv.Checked = True
            ElseIf EnableGST Then
                CreateTaxTable(dtTax)
                chktaxInv.Checked = True
            Else
                chktaxInv.Checked = False
                chktaxInv.Visible = False
            End If
            If enableMultipleDebitInInvoice Then
                'loadSalesMultipleDebits(0)
                createMultipleDebitTb(dtMultipleDebits)
                btnmultipleDebit.Visible = True
            End If
            _objcmnbLayer.dtSerialNo = createdtSerialNo()
            SetGridHead()
            setOtherCostHead()
            chgbyprg = True
            crtSubVrs(cmbVoucherTp, 6, True)
            FCRt = 1
            OthCost = 0

            'calculate()
            Text = "Purchase Invoice "
            cldrdate.Value = Format(Date.Now, DtFormat)
            LodCurrency()
            NDec = NoOfDecimal
            loadedTrId = 0
            chgbyprg = False
            cmbDos.SelectedIndex = 0
            AddDttoCombo(cmblocation, dtlocationTb, True, False)
            cmblocation.Text = Dloc
            'AddtoCombo(cmblocation, "SELECT LocCode FROM LocationTb", True, False)
            'cmbVrType_SelectedIndexChanged(sender, e)
            'numVchrNo.Select()
            ChgId = False
            If isModi Then
                btnModify_Click(btnModify, New System.EventArgs)
                If userType Then
                    btnupdate.Tag = IIf(getRight(42, CurrentUser), 1, 0)
                    btndelete.Tag = IIf(getRight(43, CurrentUser), 1, 0)
                Else
                    btnupdate.Tag = 1
                    btndelete.Tag = 1
                End If

                btndelete.Text = "Delete"
            Else
                'AddNewClick()
                NextNumber()
                If userType Then
                    btnupdate.Tag = IIf(getRight(41, CurrentUser), 1, 0)
                Else
                    btnupdate.Tag = 1
                End If
                btndelete.Text = "Clear"
                btndelete.Tag = 1
            End If
            Timer1.Enabled = True
            If enableGCC Then
                chkimport.Visible = True
                pltax.Visible = True
                grpotherCostTax.Visible = True
                'ldVat()
            End If
            calculate()
            chkcaltaxForsalesprice.Checked = calcluatetaxFromSpriceInIP
            btnbarcode.Visible = EnableBarcode
            chksinglebarcode.Visible = EnableBarcode
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub chkimport_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkimport.CheckedChanged
        cmbbycustoms.Visible = chkimport.Checked
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

    Private Sub txtdebit_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtdebit.TextChanged

    End Sub

    Private Sub txtOthrRef_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOthrRef.TextChanged

    End Sub

    Private Sub numOtherAmt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numOtherAmt.TextChanged

    End Sub

    Private Sub btnshow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnshow.Click
        If Val(txtSuppAlias.Tag) = 0 Then
            MsgBox("Invalid Supplier", MsgBoxStyle.Exclamation)
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
    Private Function ImportDOs(ByVal Docids As String, Optional ByVal trasferAllItems As Boolean = False) As Boolean

        Dim i As Integer
        Dim sRs As DataTable
        Dim UPerPack As Double
        Dim tNumformat As String
        If trasferAllItems Then
            sRs = _objcmnbLayer._fldDatatable("SELECT * from (Select DocTranTb.*, [Item Code],isSerialNo,FraCount,isnull(itemCategory,'')itemCategory,collectionAC,vat,MRP,withtax, " & _
                                              "isnull(TPQtyInv,0)TPQtyInv,isnull(TPQtyDoc,0)TPQtyDoc,Qty-(isnull(TPQtyInv,0)+isnull(TPQtyDoc,0)) balanceQty,Dno FROM DocTranTb LEFT JOIN DocCmnTb ON DocCmnTb.DocId=DocTranTb.DocId " & _
                                              "LEFT JOIN InvItm ON InvItm.ItemId = DocTranTb.ItemId " & _
                                              "LEFT JOIN InvItmPropertiesTb ON InvItmPropertiesTb.Itemid=invitm.itemid  " & _
                                              "LEFT JOIN UnitsTb ON UnitsTb.Units=DocTranTb.Unit " & _
                                              "LEFT JOIN VatMasterTb ON InvItm.vatId=VatMasterTb.vatId " & _
                                              "LEFT JOIN (SELECT impDocSlno, Sum(TrQty/PFraction) As TPQtyInv,Sum(UnitCost * (TrQty/PFraction)) PIAmt FROM ItmInvTrTb  GROUP BY impDocSlno) As PIQ ON PIQ.impDocSlno = DocTranTb.id " & _
                                              "LEFT JOIN (SELECT ImpDocLnNo, Sum(Qty/PFraction) As TPQtyDoc,Sum(CostPUnit * (Qty/PFraction)) As PDAmt FROM DocTranTb  GROUP BY ImpDocLnNo) As PIQD ON PIQD.ImpDocLnNo = DocTranTb.id " & _
                                              "WHERE DocTranTb.docid IN( " & Docids & "))tr where balanceQty>0 ORDER BY SlNo")
        End If



        grdVoucher.RowCount = 0
        Dim dno As Integer = 0
        If _objcmnbLayer.dtSerialNo.Rows.Count > 0 Then _objcmnbLayer.dtSerialNo.Rows.Clear()
        txtDOLst.Text = ""
        chgbyprg = True
        With grdVoucher
            .Rows.Clear()
            If sRs.Rows.Count > 0 Then
                For i = 0 To sRs.Rows.Count - 1
                    If i = 0 Then
                        If IsDBNull(sRs(i)("withtax")) Then sRs(i)("withtax") = 0
                        chktaxInv.Checked = sRs(i)("withtax")
                    End If
                    If dno <> sRs(i)("DNO") Then
                        txtDOLst.Text = IIf(txtDOLst.Text = "", "", ",") & sRs(i)("DNO")
                        dno = sRs(i)("DNO")
                    End If
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
                    grdVoucher.Item(ConstDescr, i).Value = IIf(IsDBNull(sRs(i)("TrDetail")), "", sRs(i)("TrDetail"))
                    grdVoucher.Item(ConstB, i).Value = IIf(sRs(i)("Mthd") & "" = "", "B", Trim(sRs(i)("Mthd") & ""))
                    grdVoucher.Item(ConstUnit, i).Value = Trim("" & sRs(i)("Unit"))
                    If Val(sRs(i)("Taxp") & "") = 0 Then sRs(i)("Taxp") = 0
                    grdVoucher.Item(ConstTaxP, i).Value = Format(sRs(i)("Taxp"), lnumFormat)
                    If Val(sRs(i)("taxamt") & "") = 0 Then sRs(i)("taxamt") = 0
                    grdVoucher.Item(ConstTaxAmt, i).Value = Format(sRs(i)("taxamt") / FCRt, lnumFormat)

                    If Val(sRs(i)("vat") & "") = 0 Then sRs(i)("vat") = 0
                    If Not enableGCC Then
                        grdVoucher.Item(Constcess, i).Value = Format(sRs(i)("vat"), lnumFormat)
                    Else
                        grdVoucher.Item(Constcess, i).Value = 0
                    End If

                    If Val(sRs(i)("CessAmt") & "") = 0 Then sRs(i)("CessAmt") = 0
                    grdVoucher.Item(ConstcessAmt, i).Value = Format(sRs(i)("CessAmt") / FCRt, lnumFormat)
                    If Val(sRs(i)("collectionAC") & "") = 0 Then sRs(i)("collectionAC") = 0
                    grdVoucher.Item(ConstcessAc, i).Value = Val(sRs(i)("collectionAC"))

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
                    If Val(sRs(i)("DisP") & "") = 0 Then sRs(i)("DisP") = 0
                    grdVoucher.Item(ConstDisP, i).Value = Format(sRs(i)("DisP"), numFormat)
                    If Val(sRs(i)("ItemDiscount") & "") = 0 Then sRs(i)("ItemDiscount") = 0
                    grdVoucher.Item(ConstDisAmt, i).Value = Format(sRs(i)("ItemDiscount") * UPerPack / FCRt, lnumFormat)
                    grdVoucher.Item(ConstImpDocId, i).Value = sRs(i)("Docid")
                    grdVoucher.Item(ConstImpLnId, i).Value = sRs(i)("id")

                    grdVoucher.Item(ConstBarcode, i).Value = sRs(i)("HSNCode")
                    grdVoucher.Item(ConstCGSTP, i).Value = sRs(i)("CSGTP")
                    grdVoucher.Item(ConstCGSTAmt, i).Value = sRs(i)("CGSTAMT")

                    grdVoucher.Item(ConstSGSTP, i).Value = sRs(i)("SGSTP")
                    grdVoucher.Item(ConstSGSTAmt, i).Value = sRs(i)("SGSTAmt")

                    grdVoucher.Item(ConstIGSTP, i).Value = sRs(i)("IGSTP")
                    grdVoucher.Item(ConstIGSTAmt, i).Value = sRs(i)("IGSTAmt")

                    grdVoucher.Item(ConstIsSerial, i).Value = 0
                    grdVoucher.Item(ConstId, i).Value = 0

                    If Val(sRs(i)("MRP") & "") = 0 Then sRs(i)("MRP") = 0
                    .Item(ConstMRP, i).Value = _objInv.MRP = CDbl(sRs(i)("MRP"))
                    .Item(ConstBatchCost, i).Value = 0
                    calcualteLineTotal(i)
                Next
            Else
                MsgBox("Document's Entry or Balance Quantity not found under the entered Documents.", MsgBoxStyle.Exclamation)
            End If
        End With
        If sRs.Rows.Count > 0 Then sRs.Clear()
        calculate()
        reArrangeNo()
        numVchrNo.Focus()
        chgbyprg = False
        ChgId = True
        chgPost = True

    End Function

    Private Sub txtCashSuppName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCashSuppName.TextChanged
        If chgbyprg = True Then Exit Sub
        If isNotSupplierAccount Then
            _srchTxtId = 5
        Else
            _srchTxtId = 2
        End If
        _srchOnce = False
        ShowFmlist(sender)
    End Sub

    Private Sub txtCashSuppName_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCashSuppName.Validated
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
        If txtCashSuppName.Text = "" Then Exit Sub
        If vtype = "Credit" Then
            setSupplier()
            txtDescr.Focus()
        Else
            Dim dt As DataTable
            dt = _objcmnbLayer._fldDatatable("Select * from CashCustomerTb where isnull(issupp,0)=1 and custid=" & Val(txtCashSuppName.Tag))
            If dt.Rows.Count > 0 Then
                chgbyprg = True
                txtCashSuppName.Text = UCase(Trim(dt(0)("CustName") & ""))
                chgbyprg = False
            End If
        End If
        chgbyprg = True
        txtCashSuppName.Text = UCase(txtCashSuppName.Text)
        chgbyprg = False
    End Sub

    Private Sub txtDOLst_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDOLst.TextChanged

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
                PasteFrom(FrTrId)
                calculate()
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

    Private Sub grdVoucher_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles grdVoucher.DataError
        MsgBox(e.Exception.ToString)
    End Sub


    Private Sub chkcaltaxForsalesprice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkcaltaxForsalesprice.Click
        _objcmnbLayer._saveDatawithOutParm("Update CompanyTb set calcluatetaxFromSpriceInIP =" & IIf(chkcaltaxForsalesprice.Checked, 1, 0))
        calcluatetaxFromSpriceInIP = chkcaltaxForsalesprice.Checked
    End Sub
    Public Sub returnFromJob(ByVal jobcode As String, ByVal jobid As Integer, ByVal plateno As String, ByVal supplierid As Integer, ByVal cartype As String)
        chgbyprg = True
        txtJob.Text = jobcode
        txtJob.Tag = jobid
        'txtjobname.Text = jobname
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select Alias,AccDescr from AccMast where AccId=" & supplierid)
        txtSuppAlias.Text = dt(0)("Alias")
        txtSuppName.Text = dt(0)("AccDescr")
        txtCashSuppName.Text = dt(0)("AccDescr")
        txtSuppAlias.Tag = supplierid
        chgbyprg = True
        Dim PreFixTb As DataTable = _objcmnbLayer._fldDatatable("SELECT  top 1 * FROM PreFixTb WHERE vrTypeNo = 6 and ctgry=3", False)
        If PreFixTb.Rows.Count > 0 Then
            cmbVoucherTp.Text = PreFixTb(0)("Voucher Name")
        End If
createNewItem:
        Dim dR As DataTable = ItmValidation(1, plateno, True, "IP", Val(txtSuppAlias.Tag))
        If dR.Rows.Count > 0 Then
            grdVoucher.Rows.Add(1)
            grdVoucher.CurrentCell = grdVoucher.Item(0, (grdVoucher.RowCount - 1))
            AddDetails(dR, grdVoucher.RowCount - 1)
            grdVoucher.Item(ConstQty, (grdVoucher.RowCount - 1)).Value = Strings.Format(1, numFormat)
            grdVoucher.Item(ConstUPrice, (grdVoucher.RowCount - 1)).Value = Strings.Format(0, numFormat)
            grdVoucher.Item(ConstActualPrice, (grdVoucher.RowCount - 1)).Value = 0
            grdVoucher.Item(constItmTot, (grdVoucher.RowCount - 1)).Value = 0
            calcualteLineTotal(grdVoucher.RowCount - 1)
        Else
            createInstantItem(plateno, cartype, "", 0, "Stock")
            GoTo createNewItem
        End If
        IsLoadFromExternal = True
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
            .trtype = "IP"
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

    Private Sub ldtimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ldtimer.Tick
        If chgbyprg Then Exit Sub
        ldtimer.Enabled = False
        chgbyprg = True
        ShowPanel()
        chgItm = True
        chgbyprg = False
        grdVoucher.Item(ConstqtyChg, grdVoucher.CurrentRow.Index).Value = "Chg"
    End Sub

    Private Sub btnnewitem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnewitem.Click
        loadCreateItem()
    End Sub
    Private Sub loadCreateItem()
        If fproductMast Is Nothing Then
            fproductMast = New ItemMastFrm
            'fproductMast.MdiParent = fMainForm
            fproductMast.IsFromEnqry = True
            fproductMast.WindowState = FormWindowState.Normal
            fproductMast.StartPosition = FormStartPosition.Manual

            fproductMast.Top = Me.Top + 10
            fproductMast.Left = (Me.Width / 2) - (fproductMast.Width / 2)
            fproductMast.ShowDialog()
        Else
            fproductMast.ShowDialog()

        End If
    End Sub

    Private Sub lblSgst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblSgst.Click

    End Sub

    Private Sub Timer3_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        Timer3.Enabled = False
        grdVoucher.CurrentCell = grdVoucher.Item(ConstQty, grdVoucher.RowCount - 1)
        grdBeginEdit()
    End Sub
    Public Sub ldjbname()
        chgbyprg = True
        txtjobname.Text = (_objcmnbLayer.isValidEntry(txtJob.Text, 3))
        chgbyprg = False
    End Sub

    Private Sub cmbimporttr_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbimporttr.SelectedIndexChanged

    End Sub
End Class