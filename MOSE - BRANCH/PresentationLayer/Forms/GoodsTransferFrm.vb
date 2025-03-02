Public Class GoodsTransferFrm
#Region "Public Variables"
    Public isModi As Boolean
    Public isCust As Boolean
#End Region
#Region "Private Variables"
    Private chgbyprg As Boolean
    Private _srchTxtId As Integer
    Private _srchOnce As Boolean
    Private MyActiveControl As New Object
    Private lstKey As Integer
    Private activecontrolname As String
    Private chgItm As Boolean
    Private SrchText As String
    Private NDec As Integer
    Private chgAmt As Boolean
    Private chgPost As Boolean
    Private strGridSrchString As String
    Private _srchIndexId As Integer
    Private ChgId As Boolean
    Private loadedTrId As Long
#End Region
#Region "Form Objects"
    Private WithEvents fMList As Mlistfrm
    Private WithEvents fRptFormat As RptFormatfrm
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fSlctDoc As SelectInvTr
    Private WithEvents fProductEnquiry As ItmEnqry
#End Region
#Region "Class Object"
    Private _objcmnbLayer As New clsCommon_BL
    Private _objDoc As clsDocCmn
    Private _objInv As clsInvoice
#End Region

    Private Sub GoodsTransferFrm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
        If Not fRptFormat Is Nothing Then fRptFormat.Close() : fRptFormat = Nothing
        If Not frm Is Nothing Then frm.Close() : frm = Nothing
        If Not fSlctDoc Is Nothing Then fSlctDoc.Close() : fSlctDoc = Nothing
        If Not fProductEnquiry Is Nothing Then fProductEnquiry.Close() : fProductEnquiry = Nothing
    End Sub
    Private Sub SalesOrderFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetGridHead()
        NextNumber()
        ldLoc()
        If isModi Then
            btnModify_Click(btnModify, New System.EventArgs)
            If userType Then
                btnupdate.Tag = IIf(getRight(124, CurrentUser), 1, 0)
                btndelete.Tag = IIf(getRight(125, CurrentUser), 1, 0)
            Else
                btnupdate.Tag = 1
                btndelete.Tag = 1
            End If

            btndelete.Text = "Delete"
        Else
            AddNewClick()
            If userType Then
                btnupdate.Tag = IIf(getRight(123, CurrentUser), 1, 0)
            Else
                btnupdate.Tag = 1
            End If
            btndelete.Text = "Clear"
            btndelete.Tag = 1
        End If
        Timer1.Enabled = True
    End Sub
    Private Sub SetGridHead()
        chgbyprg = True
        SetGridHeadEntryProperty(grdVoucher)
        With grdVoucher
            .Columns(ConstTaxAmt).Visible = False
            .Columns(ConstTaxP).Visible = False
            .Columns(ConstUPrice).Visible = False
            .Columns(ConstLTotal).Visible = False
            .Columns(constItmTot).Visible = False
            .Columns(ConstcessAmt).Visible = False
            .Columns(ConstBarcode).Visible = False
            .Columns(ConstDisAmt).Visible = False
            .Columns(ConstDisP).Visible = False
            .Columns(ConstFocQty).Visible = False
            .Columns(ConstQty).Width = 100
        End With
        chgbyprg = False
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        resizeGridColumn(grdVoucher, ConstDescr)
    End Sub

    Private Sub SalesOrderFrm_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Timer1.Enabled = True
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub NextNumber()
        Dim vrPrefix As String = ""
        Dim vrVoucherNo As Long
        Dim vrAccountNo1 As Long
        Dim vrAccountNo2 As Long
        getVrsDet(0, "MTN", vrPrefix, vrVoucherNo, vrAccountNo1, vrAccountNo2)
        If Not isModi Then
            numVchrNo.Text = vrVoucherNo
            'txtprefix.Text = vrPrefix
        End If
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
            Dim x As Integer = Me.Width - fMList.Width
            Dim y As Integer = Me.Height - fMList.Height - 10
            PopupLoc = New Point(x, y)
            If fMList.Visible = False Then

                Select Case _srchTxtId
                    Case 5, 4
                        SetFmlist(fMList, 8)
                End Select
                fMList.loc = PopupLoc
                fMList.Show(fMainForm)
                fMList.Visible = True
            End If
        End If
        Select Case _srchTxtId
            Case 4   'Job

                fMList.SearchIndex = 0
                fMList.SearchIndexDescr = 1
                fMList_doFocus()
                fMList.Search(txtjobfrom.Text)
                fMList.AssignList(txtjobfrom, lstKey, chgbyprg)
            Case 5 'Job for modification

                fMList.SearchIndex = 0
                fMList.SearchIndexDescr = 1
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
            Case 4
                txtjobfrom.Text = ItmFlds(0)
            Case 5
                txtJob.Text = ItmFlds(0)
        End Select
        chgbyprg = False
    End Sub

    Private Sub txtJob_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtJob.KeyDown, txtjobfrom.KeyDown
        lstKey = e.KeyCode
        Dim myctrl As TextBox
        myctrl = sender
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
        If myctrl.Name = "txtjobfrom" Or myctrl.Name = "txtJob" Then
            If e.KeyCode = Keys.Up Or e.KeyCode = Keys.PageUp Then
                If Not fMList Is Nothing Then
                    If fMList.Visible Then
                        fMList.MoveUp(myctrl.Text)
                        Exit Sub
                    End If
                End If
            ElseIf e.KeyCode = Keys.Down Or e.KeyCode = Keys.PageDown Then
                If Not fMList Is Nothing Then
                    If fMList.Visible Then
                        fMList.MoveDown(myctrl.Text)
                        Exit Sub
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub txtjobfrom_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtjobfrom.TextChanged, txtJob.TextChanged
        If chgbyprg = True Then Exit Sub
        Dim myCtrl As Control = sender
        Select Case myCtrl.Name
            Case "txtjobfrom"
                _srchTxtId = 4
            Case "txtJob"
               _srchTxtId = 5
        End Select
        _srchOnce = False
        ShowFmlist(sender)
    End Sub

    Private Sub txtJob_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtJob.Validated
        If chgbyprg Then Exit Sub
        _objcmnbLayer = New clsCommon_BL
        Dim dt As DataTable
        _objcmnbLayer = New clsCommon_BL
        lbllocto.Text = Dloc
        txtjobname.Text = ""
        dt = _objcmnbLayer._fldDatatable("SELECT jobcode,jobname,custcode,AccDescr from jobtb Left join accmast on jobtb.custcode=accmast.accid  where jobcode='" & txtJob.Text & "'")
        If dt.Rows.Count > 0 Then
            txtJob.Text = dt(0)("jobcode")
            txtjobname.Text = dt(0)("jobname")
            lbllocto.Text = JLoc
            txtcustomer.Text = dt(0)("AccDescr")
            txtcustomer.Tag = dt(0)("custcode")
        Else
            txtJob.Text = ""
            lbllocto.Text = Dloc
            txtcustomer.Text = ""
            txtcustomer.Tag = ""
        End If
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
    End Sub

    Private Sub txtjobfrom_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtjobfrom.Validated
        If chgbyprg Then Exit Sub
        _objcmnbLayer = New clsCommon_BL
        Dim dt As DataTable
        _objcmnbLayer = New clsCommon_BL
        txtjobnamefrom.Text = ""
        dt = _objcmnbLayer._fldDatatable("SELECT * from jobtb  where jobcode='" & txtjobfrom.Text & "'")
        lblLocFrom.Text = Dloc
        If dt.Rows.Count > 0 Then
            txtjobfrom.Text = dt(0)("jobcode")
            txtjobnamefrom.Text = dt(0)("jobname")
            lblLocFrom.Text = JLoc
        Else
            txtjobfrom.Text = ""
            lblLocFrom.Text = Dloc
        End If
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
    End Sub
    Private Sub ldLoc()
        lblLocFrom.Text = Dloc
        lbllocto.Text = Dloc
        loadLocation()
        cmblocationFrom.Text = Dloc
        cmblocationTo.Text = Dloc
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
            If Not grdVoucher.CurrentRow Is Nothing Then
                If Val(grdVoucher.Item(ConstItemID, grdVoucher.CurrentRow.Index).Value) > 0 Then chgItm = False
            End If
            SrchText = ""
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
        Dim totTax As Double = 0
        Dim totLnDis As Double
        Dim totAmt As Double
        Dim i As Integer
        'Dim j As Integer

        With grdVoucher
            For i = 0 To .Rows.Count - 1
                If Val(.Item(ConstItemID, i).Value) = 0 Then GoTo nxt
                .Item(ConstSlNo, i).Value = i + 1
                totQty = totQty + IIf(Val(.Item(ConstQty, i).Value) > 0, Val(.Item(ConstQty, i).Value), 0)
                .Item(ConstTaxAmt, i).Value = ((CDbl(.Item(ConstActualPrice, i).Value) * CDbl(.Item(ConstTaxP, i).Value)) / 100)
                totLnDis = totLnDis + .Item(ConstDisAmt, i).Value
                'totAmt = totAmt + CDbl(.Item(ConstActualPrice, i).Value) * CDbl(.Item(ConstQty, i).Value)
                totAmt = totAmt + (.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - (.Item(ConstQty, i).Value * .Item(ConstDisAmt, i).Value)
                totTax = totTax + CDbl(.Item(ConstTaxAmt, i).Value)
                .Item(ConstLTotal, i).Value = Format((.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - (.Item(ConstQty, i).Value * .Item(ConstDisAmt, i).Value) + (.Item(ConstQty, i).Value * .Item(ConstTaxAmt, i).Value), numFormat)
                .Item(ConstNUPrice, i).Value = CDbl(.Item(ConstActualPrice, i).Value) - .Item(ConstDisAmt, i).Value + .Item(ConstTaxAmt, i).Value - Val(.Item(ConstDiscOther, i).Value)

nxt:
            Next
            lblNetAmt.Text = Format(totAmt, numFormat)
            'totAmt = totAmt - CDbl(numDisc.Text)
            'chgAmt = False
        End With
    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            Const WM_KEYDOWN As Integer = &H100
            Const WM_KEYUP As Integer = &H101
            'msg.ToString()
            If msg.Msg = WM_KEYDOWN Or msg.Msg = WM_KEYUP Then
                If msg.WParam.ToInt32() = CInt(Keys.F8) Then
                    'UpdateClick()
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
                    'If Not fSelect Is Nothing Then fSelect.Close() : fSelect = Nothing
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
        grdVoucher.BeginEdit(True)
        chgbyprg = False
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
                    cost = getPurchAmt(dt(0)("LastPurchCost"), 0, Val(.Item(ConstItemID, .CurrentCell.RowIndex).Value))
                    cost = cost * Val(.Item(ConstPMult, .CurrentCell.RowIndex).Value)
                    .Item(ConstActualPrice, .CurrentCell.RowIndex).Value = cost
                    .Item(ConstUPrice, .CurrentCell.RowIndex).Value = Format(cost, numFormat)
                    calculate()
                End If
            End With
        End If
    End Sub

    Private Sub grdVoucher_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellEndEdit
        Dim col As Integer = e.ColumnIndex
        Dim ndec1 As Integer
        If col = ConstQty Or col = ConstUPrice Or col = ConstDisAmt Or col = ConstLTotal Then
            If col = ConstQty Then
                ndec1 = grdVoucher.Item(ConstPFraction, grdVoucher.CurrentRow.Index).Value
            Else
                ndec1 = NDec
            End If
            grdVoucher.Item(col, e.RowIndex).Value = Format(Convert.ToDecimal(grdVoucher.Item(col, e.RowIndex).Value), "#,##0" & IIf(ndec1 = 0, "", "." & Strings.StrDup(ndec1, "0")))
        End If
    End Sub

    Private Sub grdVoucher_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellValidated
        Valid(e.RowIndex, e.ColumnIndex)
        SrchText = ""
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
                Case ConstQty, ConstUPrice, ConstTaxAmt, ConstTaxP
                    '.Item(ConstLTotal, i).Value = Format((.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - (.Item(ConstQty, i).Value * .Item(ConstDisAmt, i).Value) + (.Item(ConstQty, i).Value * .Item(ConstTaxAmt, i).Value), NumFormat)
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
                Case ConstDisAmt
                    chgAmt = True
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
        If Col = ConstQty Then
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
        SearchProductPanel(grdSrch, "Item Code", strGridSrchString, True)
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
            ElseIf e.KeyCode = Keys.F2 Then
                If grdVoucher.RowCount = 0 Then Exit Sub
                Select Case grdVoucher.CurrentCell.ColumnIndex
                    Case ConstItemCode
                        If Not fMList Is Nothing Then fMList.Visible = False
                        fProductEnquiry = New ItmEnqry
                        fProductEnquiry.ShowDialog()
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

    Private Sub picCloseProd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picCloseProd.Click
        plsrch.Visible = False
    End Sub
    Private Sub Valid(ByVal RowIndex As Integer, ByVal ColIndex As Integer)
        With grdVoucher
            Select Case ColIndex
                Case ConstBarcode, ConstItemCode
                    If Not chgItm Then Exit Sub
                    If Trim(.Item(ColIndex, RowIndex).Value) = "" Then Exit Sub
                    Dim dtItms As DataTable
                    Dim found As Boolean
                    'dtItms = getItmDtls(3, SrchText, True)
                    dtItms = ItmValidation(3, SrchText, True, "", 0)
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
                        calculate()
                    End If

                Case ConstUPrice
                    If chgAmt Then
                        .Item(ConstActualPrice, RowIndex).Value = CDbl(.Item(ConstUPrice, RowIndex).Value)
                        calculate()
                    End If
                Case ConstDisAmt
                    If chgAmt Then
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
            .Item(ConstQty, i).Value = Format(1, numFormat) 'IIf(IsReturn, -1, 1)
            .Item(ConstTaxP, i).Value = Format(Val(DR(0)("vat") & ""), numFormat)
            .Item(ConstItemID, i).Value = DR(0)("ItemId")
            .Item(ConstPMult, i).Value = 1 'IIf(IsNull(sRs(0)("PFraCount), "2", sRs(0)("PFraCount)
            If DR(0)("ItemCategory") = "Comment" Then
                'onceChgFld = (CStr(.Item(ConstSlNo, i).Value) <> "M")
                .Item(ConstSlNo, i).Value = "M"
                .Item(ConstB, i).Value = 0
                .Item(ConstUnit, i).Value = ""
                .Item(ConstSerialNo, i).Value = ""
                .Item(ConstPMult, i).Value = "1"
                .Item(ConstPFraction, i).Value = "2"
                .Item(ConstImpDocId, i).Value = ""
                .Item(ConstImpLnId, i).Value = ""
            Else
                'onceChgFld = (CStr(.Item(ConstSlNo, i).Value) = "M" Or CStr(.Item(ConstSlNo, i).Value) = "L")
                'If onceChgFld Then
                '    .Item(ConstSlNo, i).Value = ""
                '    .Item(ConstBarcode, i).Value = ""
                '    .Item(ConstItemCode, i).Value = ""
                'End If
            End If
            .Item(ConstB, .CurrentCell.RowIndex).Value = "B"
            .Item(ConstPFraction, i).Value = IIf(IsDBNull(DR(0)("FraCount")), "2", DR(0)("FraCount"))
            .Item(ConstUnit, i).Value = DR(0)("Unit")
            .Item(ConstWarrentyExpiry, i).Value = Format(DateAdd(DateInterval.Year, 1, DateValue(cldrdate.Value)), DtFormat)
            If CDbl(.Item(ConstUPrice, i).Value) = 0 Or chgItm Then
                .Item(ConstActualPrice, i).Value = DR(0)("UnitPrice") 'getPurchAmt(DR(0)("CostAvg"), Val(txtSuppAlias.Tag), Val(.Item(ConstItemID, .CurrentCell.RowIndex).Value))
                .Item(ConstUPrice, i).Value = Format(CDbl(.Item(ConstActualPrice, i).Value), "#,###.00")
            End If
            .Item(ConstLTotal, i).Value = Format(.Item(ConstQty, i).Value * CDbl(.Item(ConstUPrice, i).Value), numFormat)
            .Item(ConstNUPrice, i).Value = Format(CDbl(.Item(ConstUPrice, i).Value), numFormat)

            .Item(ConstQty, i).Value = Format(CDbl(.Item(ConstQty, i).Value), "#,##0" & IIf(Val(.Item(ConstPFraction, i).Value) = 0, "", "." & Strings.StrDup(CByte(.Item(ConstPFraction, i).Value), "0")))
            .Item(ConstImpDocId, i).Value = ""
            .Item(ConstImpLnId, i).Value = ""
            'chgAmt = True
            chgItm = False
            .ClearSelection()
        End With
        calculate()
        chgbyprg = False
    End Sub

    Private Sub grdVoucher_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.Leave
        activecontrolname = ""
    End Sub

    Private Sub grdVoucher_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.RowEnter
        If chgbyprg Then Exit Sub
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

    Private Sub btnrem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrem.Click
        RemoveRow()
        chgPost = True
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
       
        grdVoucher.RowCount = 0
        'grdVoucher.CurrentCell = grdVoucher.Item(1, 0)
        activecontrolname = ""
        'lstRow = 0
        chgbyprg = True
        txtReference.Text = ""
        txtDescr.Text = ""
        txtJob.Text = ""
        txtJob.Tag = ""
        txtjobfrom.Text = ""
        txtjobfrom.Tag = ""
        txtjobname.Text = ""
        txtjobnamefrom.Text = ""
        calculate()
        numVchrNo.ReadOnly = True
        numVchrNo.Tag = ""
        loadedTrId = 0
        isModi = False
        txtjobname.Text = ""
        chgPost = False
        chgbyprg = False
        lblLocFrom.Text = Dloc
        lbllocto.Text = Dloc
        txtcustomer.Text = ""
        txtcustomer.Tag = ""
        cmblocationFrom.Text = Dloc
        cmblocationTo.Text = Dloc
        numVchrNo.Focus()
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
            ClearControls()
            isModi = True
            numVchrNo.ReadOnly = False
            numVchrNo.Focus()
            btnSlct.Visible = True
            btnModify.Text = "&Undo"
            btndelete.Text = "Delete"
            If userType Then
                btnupdate.Tag = IIf(getRight(124, CurrentUser), 1, 0)
                btndelete.Tag = IIf(getRight(125, CurrentUser), 1, 0)
            Else
                btnupdate.Tag = 1
                btndelete.Tag = 1
            End If

        Else
            AddNewClick()

        End If
    End Sub
    Private Sub AddNewClick()
        If chgbyprg Then Exit Sub
        'If ChgId Then
        '    Select Case MsgBox("Changes found !!  Do you want hold changes ?", vbYesNoCancel + vbQuestion)
        '        Case vbYes
        '            'Hold procedure
        '        Case vbNo
        '        Case Else
        '            Exit Sub
        '    End Select
        'End If
        ClearControls()
        isModi = False
        btnModify.Text = "&Modify"
        btndelete.Text = "Clear"
        isModi = False
        btnSlct.Visible = False
        numVchrNo.ReadOnly = True
        txtReference.Select()
        NextNumber()
    End Sub
    Private Sub ClearClick()
        If MsgBox("Do You Want Clear Etry", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        ClearControls()
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        If chkFormat.Checked Then
            fRptFormat = New RptFormatfrm
            fRptFormat.RptType = "MTN"
            fRptFormat.ShowDialog()
            fRptFormat = Nothing
        Else
            PrepareRpt("MTN")
        End If
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
        _objDoc = New clsDocCmn
        _objDoc.Prefix = ""
        _objDoc.DNo = Val(numPrintVchr.Text)
        _objDoc.DocType = "MTN"
        If _objDoc.DNo = 0 Then Exit Sub
        Dim ds As DataSet
        ds = _objDoc.ldDoc("rturnDocumentDetailsForDocumentPrint", 0)
        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = Me.MdiParent
        frm.Text = RptCaption
        frm.Show()
    End Sub
    Private Sub doCommandStat(ByVal stat As Boolean)
        If chgbyprg Then Exit Sub
        ChgId = stat
        chgPost = stat
    End Sub

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        AddRow()
        grdVoucher.CurrentCell = grdVoucher.Item(ConstItemCode, grdVoucher.CurrentRow.Index)
        doCommandStat(True)
        chgPost = True
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

    Private Sub numVchrNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles numVchrNo.KeyDown, txtReference.KeyDown, cldrdate.KeyDown, txtDescr.KeyDown
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
            InvList = _objcmnbLayer._fldDatatable("SELECT Docid FROM DocCmnTb  WHERE Doctype = 'MTN' AND DNO = " & Val(numVchrNo.Text))
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
        ItmInvCmnTb = _objcmnbLayer._fldDatatable("SELECT DocCmnTb.*,jobname,fname,AccDescr FROM DocCmnTb " & _
                                                  "LEFT JOIN JobTb ON DocCmnTb.jobcode=JobTb.jobcode " & _
                                                  "LEFT JOIN AccMast ON DocCmnTb.CSCode=AccMast.Accid " & _
                                                  "LEFT JOIN (select jobcode fcode,jobname fname FROM JobTb) jbfrom ON DocCmnTb.FromJob=jbfrom.fcode " & _
                                                  "WHERE DocId = " & loadedTrId & " AND DocType = 'MTN'")
        chgbyprg = True
        If ItmInvCmnTb.Rows.Count = 0 Then GoTo Quit
        'getProtectUntil()

        cldrdate.Value = Format(ItmInvCmnTb(0)("DDate"), DtFormat) ' "MM/dd/yyyy")
        numVchrNo.Text = ItmInvCmnTb(0)("DNo") 'Val(Mid(!InvNo, 3)) '!InvNo '
        numVchrNo.Tag = ItmInvCmnTb(0)("DNo")
        numPrintVchr.Text = ItmInvCmnTb(0)("DNo")  'CMNREC(12).FormattedText
        txtcustomer.Text = Trim(ItmInvCmnTb(0)("AccDescr") & "")
        txtcustomer.Tag = ItmInvCmnTb(0)("CSCode")
        txtJob.Text = Trim("" & ItmInvCmnTb(0)("jobcode"))
        If txtJob.Text = "" Then
            lbllocto.Text = Dloc
        Else
            lbllocto.Text = JLoc
        End If
        txtjobname.Text = Trim(ItmInvCmnTb(0)("jobname") & "")
        txtjobfrom.Text = Trim("" & ItmInvCmnTb(0)("FromJob"))
        txtjobnamefrom.Text = Trim(ItmInvCmnTb(0)("fname") & "")

        cmblocationFrom.Text = Trim("" & ItmInvCmnTb(0)("FromLoc"))
        cmblocationTo.Text = Trim(ItmInvCmnTb(0)("DocDefLoc") & "")

        txtDescr.Text = Trim("" & ItmInvCmnTb(0)("Comment"))
        Protect = (ItmInvCmnTb(0)("DDate") <= ProtectUntil)
        sRs = _objcmnbLayer._fldDatatable("SELECT DocTranTb.*, [Item Code] FROM DocTranTb LEFT JOIN InvItm ON InvItm.ItemId = DocTranTb.ItemId  WHERE Docid = " & loadedTrId & " ORDER BY SlNo")
        grdVoucher.RowCount = 0
        With grdVoucher
            .Rows.Clear()
            If sRs.Rows.Count > 0 Then
                For i = 0 To sRs.Rows.Count - 1
                    grdVoucher.Rows.Add(1)
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
                        grdVoucher.Item(ConstItemID, i).Value = 0
                    End If
                    grdVoucher.Item(ConstDescr, i).Value = IIf(IsDBNull(sRs(i)("Trdetail")), "", sRs(i)("Trdetail"))
                    grdVoucher.Item(ConstB, i).Value = IIf(sRs(i)("Mthd") & "" = "", "B", Trim(sRs(i)("Mthd") & ""))
                    grdVoucher.Item(ConstUnit, i).Value = Trim("" & sRs(i)("Unit"))
                    grdVoucher.Item(ConstTaxP, i).Value = 0
                    grdVoucher.Item(ConstTaxAmt, i).Value = 0
                    grdVoucher.Item(ConstQty, i).Value = Format(sRs(i)("Qty") / UPerPack, "#,##0" & IIf(Val(grdVoucher.Item(ConstPFraction, i).Value) = 0, "", "." & Strings.StrDup(CByte(grdVoucher.Item(ConstPFraction, i).Value), "0")))
                    grdVoucher.Item(ConstUPrice, i).Value = Format(sRs(i)("CostPUnit") * UPerPack, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & NumFormat)
                    grdVoucher.Item(ConstActualPrice, i).Value = sRs(i)("CostPUnit") * UPerPack
                    grdVoucher.Item(constItmTot, i).Value = Format((sRs(i)("Qty") * sRs(i)("CostPUnit")) * UPerPack, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & NumFormat)
                    grdVoucher.Item(ConstUnitOthCost, i).Value = 0
                    grdVoucher.Item(ConstActualOthCost, i).Value = 0
                    grdVoucher.Item(ConstDiscOther, i).Value = 0
                    grdVoucher.Item(ConstId, i).Value = sRs(i)("id")

                Next
                reArrangeNo()
            End If
        End With
        If sRs.Rows.Count > 0 Then sRs.Clear()
        calculate()
        If Protect Then
            MsgBox("Voucher comes under Protected Range.  You can't Post modifications.", vbInformation)
            'varProtectedByRights = True
        ElseIf CrossBr Then
            MsgBox("Found multi-branches or branches other than you loged.  Can't Post modifications.", vbInformation)
            'varProtectedByRights = True
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

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        UpdateClick()
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
    Private Sub Verify()
        clsreader()
        clsCnnection()
        If isModi Then
            numVchrNo.Text = numVchrNo.Tag
            If chgPost = False Then
                'MsgBox("Changes not found !!", vbExclamation)
                'numVchrNo.Focus()
                'Exit Sub
            Else
                If loadedTrId = 0 Then
                    MsgBox("Voucher not yet loaded !!", vbExclamation)
                    numVchrNo.Focus()
                    Exit Sub
                End If
            End If
        End If
        If chgAmt Then calculate()
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
        'If Not isModi Then
        '    If chekDuplicate() Then Exit Sub
        'End If
        If Not chkGridvalue() Then Exit Sub
        If MsgBox("Verification is succeeded. Do you like to file it ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = vbYes Then
            saveTrans()
        End If
    End Sub
    Private Sub saveTrans()
        Dim docid As Long
        If Not isModi Then
chkagain:
            If Not CheckNoExists("", Val(numVchrNo.Text), "MTN", "Document") Then
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
        setDetValueTr()
        docid = _objDoc._saveCmn()
        'Dim i As Integer
        'Dim cnt As Integer = grdVoucher.RowCount - 1
        'Dim PPerU As Integer
        'For i = 0 To cnt
        '    With grdVoucher
        '        If Val(.Item(ConstItemID, i).Value) > 0 Or Val(.Item(ConstSlNo, i).Value) = 0 Then
        '            PPerU = Val(.Item(ConstPMult, i).Value)
        '            setDetValue(docid, PPerU, i)
        '            _objDoc._saveDetails()
        '        End If

        '    End With
        'Next
        If isModi Then
            _objcmnbLayer._saveDatawithOutParm("delete from DocTranTb where setRemove=1 and DocId=" & loadedTrId)
        End If
        numVchrNo.Tag = numVchrNo.Text
        If Not isModi Then
            numPrintVchr.Text = Trim(numVchrNo.Text)
            numVchrNo.Tag = numVchrNo.Text
            SetNextVrNo(numVchrNo, 0, "MTN", "DocType = 'MTN' AND DNO = ", True, False, True, 0)
        End If
        ChgId = False
        chgPost = False
        AddNewClick()
        MsgBox("Material Transfer is succesfully posted with Vr. # " & numPrintVchr.Text & ".", MsgBoxStyle.Information)
        numVchrNo.Tag = ""
    End Sub
    Private Sub setDetValueTr()
        If dtDocTb Is Nothing Then
            dtDocTb = _objcmnbLayer._fldDatatable("Select top 1 * from DocTranTb")
        End If
        Dim dtrow As DataRow
        dtDocTb.Rows.Clear()
        Dim i As Integer
        Dim PPerU As Single
        Dim ImpJobChildTbIDs As String = ""
        Dim slno As Integer
        With grdVoucher
            For i = 0 To .Rows.Count - 1
                If .Item(ConstSlNo, i).Value.ToString <> "M" And Val(.Item(ConstItemID, i).Value) = 0 Then GoTo skip
                PPerU = Val(.Item(ConstPMult, i).Value) 'Switch(.TextMatrix(i, 4) = "B", 1, .TextMatrix(i, 4) = "P1", CSng(grdVoucher.TextMatrix(i, 18)), .TextMatrix(i, 4) = "P2", CSng(grdVoucher.TextMatrix(i, 19)))
                PPerU = IIf(PPerU = 0, 1, PPerU)

                dtrow = dtDocTb.NewRow
                dtrow("ItemId") = IIf(.Item(ConstSlNo, i).Value.ToString <> "M", Val(.Item(ConstItemID, i).Value), 0)
                dtrow("Unit") = .Item(ConstUnit, i).Value
                If Not IsDBNull(.Item(ConstDescr, i).Value) Then
                    dtrow("TrDetail") = IIf(.Item(ConstSlNo, i).Value.ToString = "M", .Item(ConstDescr, i).Value, Trim(.Item(ConstDescr, i).Value))
                End If
                dtrow("Qty") = CDbl(.Item(ConstQty, i).Value) * PPerU
                dtrow("CostPUnit") = 0 'CDbl(.Item(ConstActualPrice, i).Value) * FCRt * PPerU 'CDbl(.TextMatrix(i, 10)) / PPerU
                slno = slno + 1
                dtrow("SlNo") = slno
                dtrow("PFraction") = PPerU
                dtrow("Mthd") = .Item(ConstB, i).Value
                dtrow("id") = Val(.Item(ConstId, i).Value)
                dtrow("lineCost") = 0
                If Val(dtrow("ItemId")) = 0 Then
                    dtrow("Qty") = 1
                    dtrow("CostPUnit") = 0
                    dtrow("taxP") = 0
                    dtrow("taxAmt") = 0
                    dtrow("UnitDiscount") = 0
                End If
                dtDocTb.Rows.Add(dtrow)
                Dim j As Integer
                Dim dtype As String
                For j = 0 To dtDocTb.Columns.Count - 1
                    If dtDocTb.Columns(j).ColumnName = "id" Then GoTo nxt
                    dtype = dtDocTb.Columns(j).DataType.Name
                    If Trim(dtDocTb(i)(j) & "") = "" Then
                        Select Case dtype
                            Case "String"
                                dtDocTb(i)(j) = ""
                            Case "Int64", "Int32", "Double", "Decimal", "Boolean"
                                dtDocTb(i)(j) = 0
                        End Select
                    End If
nxt:
                Next
skip:
            Next
        End With

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
        End With
    End Sub

    Private Function chekDuplicate() As Boolean
        Dim dtTable As DataTable
        Dim varNextFoundBool As Boolean
ChkAgain:
        dtTable = _objcmnbLayer._fldDatatable("SELECT * FROM DocCmnTb WHERE DocType = 'MTN' AND DNo = " & Val(numVchrNo.Text))
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
            .DocType = "MTN"
            .Reference = Strings.Trim(txtReference.Text)
            .UserId = CurrentUser
            .MchName = MACHINENAME
            .Comment = Trim(txtDescr.Text & "")
            .DDate = DateValue(cldrdate.Value)
            .JobCode = txtJob.Text
            .FromJob = txtjobfrom.Text
            .FromLoc = cmblocationFrom.Text
            .DocDefLoc = cmblocationTo.Text
            .CSCode = Val(txtcustomer.Tag)
            .Prefix = ""
            .DNo = Val(numVchrNo.Text)
            .DocAmt = 0 'CSng(Conversions.ToDouble(lblNetAmt.Text))
            .DocId = loadedTrId
            .isModi = isModi
            .SlManId = ""
            .TermsId = ""
            .BrId = ""
            .DeptId = ""
            .DueDt = DateValue(cldrdate.Value)
        End With
        
    End Sub

    Private Sub btnSlct_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSlct.Click
        fSlctDoc = New SelectInvTr
        With fSlctDoc
            .strType = "MTN"
            .Text = "Material Transfer Note"
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
    Private Sub loadLocation()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select LocCode from LocationTb")
        cmblocationFrom.Items.Clear()
        cmblocationTo.Items.Clear()
        Dim i As Integer
        For i = 0 To dt.Rows.Count - 1
            cmblocationFrom.Items.Add(dt(i)(0))
            cmblocationTo.Items.Add(dt(i)(0))
        Next
    End Sub
End Class