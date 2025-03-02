Public Class StockmovementFrm
    Private chgbyprg As Boolean
    Private _gridItems As DataTable
    Private dtTransaction As DataTable
    Private _objcmnbLayer As clsCommon_BL
    Private _report As New clsReport_BL
    Private supRindex As Integer


    Private Const ItemCode As Integer = 0
    Private Const TrDescr As Integer = 1
    Private Const Unit As Integer = 2
    Private Const QtyInHand As Integer = 3
    Private Const CostAverage As Integer = 4
    Private Const intReceivedQty As Integer = 5
    Private Const intIssuedQty As Integer = 6
    Private Const priceAvg As Integer = 7
    Private Const profit As Integer = 9
    Private Const totalPrice As Integer = 8
    Private Const ConstItemId As Integer = 10

    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fRptFormat As RptFormatfrm
    Private WithEvents fwait As WaitMessageFrm
    Private Sub PricelistFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        chgbyprg = True
        cldrStartDate.Value = Format(DateAdd(DateInterval.Day, firstDateFromToday * -1, DateValue(Date.Now)), DtFormat)
        loadWaite(1)
        'lditemdetails(0)
        chgbyprg = False
        Timer1.Enabled = True
    End Sub
    Private Sub lditemdetails(ByVal supRindex As Integer)
        Try
            Dim condition As String = ""
            Dim dtcondition As String = ""
            Dim CScode As String = ""
            Dim tp As Integer
            Dim reportwise As String = ""
            If chksuppllierwise.Checked Then
                If grdSup.RowCount = 0 Then Exit Sub
                tp = 2
                condition = IIf(chkallsupp.Checked, "", "Where cscode=" & Val(grdSup.Item("AccountNo", supRindex).Value))
                CScode = Val(grdSup.Item("AccountNo", supRindex).Value)
            End If
            condition = condition & IIf(condition = "", " WHERE ", " AND ") & " isnull(ishide,0)=0 "
            If rdoqty.Checked Then
                condition = condition & " Order by IssdQty"
                reportwise = "1"
            ElseIf rdovalue.Checked Then
                condition = condition & " Order by totalPrice"
                reportwise = "2"
            ElseIf rdoprofit.Checked Then
                condition = condition & " Order by Profit"
                reportwise = "3"
            End If
            If chkdate.Checked Then
                dtcondition = " where trdate>='" & Format(DateValue(cldrStartDate.Value), "yyyy/MMM/dd") & "'"
                dtcondition = dtcondition & " and trdate<='" & Format(DateValue(dtpto.Value), "yyyy/MMM/dd") & "'"
                tp = 1
                If chksuppllierwise.Checked Then tp = 3
            End If
            condition = condition & IIf(rdofastmove.Checked, " DESC", "")
            dtcondition = dtcondition & IIf(dtcondition = "" And UsrBr <> "", " where ", IIf(UsrBr <> "", " and ", "")) & IIf(UsrBr = "", "", " brid='" & UsrBr & "'")
            _gridItems = _report.returnStockMovement(tp, condition, dtcondition, CScode, DateValue(cldrStartDate.Value), DateValue(dtpto.Value), reportwise, IIf(chkdate.Checked, "Y", "N")).Tables(0)
            grdItem.DataSource = _gridItems
            SetGridHead()
            setComboGrid()
            btnApply.Enabled = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        End Try

    End Sub
    Private Sub setComboGrid()
        cmbOrder.Items.Clear()
        Dim i As Integer = 0
        Dim cnt As Integer = (grdItem.ColumnCount - 2)
        For i = 0 To cnt
            cmbOrder.Items.Add(grdItem.Columns.Item(i).HeaderText)
        Next
        If (cmbOrder.Items.Count > 0) Then
            cmbOrder.SelectedIndex = 0
        End If
    End Sub
    Private Sub SetGridHead()
        With grdItem
            SetGridProperty(grdItem)
            .ColumnHeadersHeight = 200
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke
            .ColumnHeadersDefaultCellStyle.Font = New Font("Arial", 9.0!)
            .Columns.Item(ItemCode).HeaderText = "Item Code"
            .Columns.Item(ItemCode).Width = 150
            .Columns.Item(TrDescr).HeaderText = "Description"
            .Columns.Item(TrDescr).Width = 600
            .Columns.Item(TrDescr).DefaultCellStyle.BackColor = Color.LightSteelBlue
            .Columns.Item(Unit).HeaderText = "Unit"
            .Columns.Item(Unit).Width = 50
            .Columns.Item(Unit).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns.Item(QtyInHand).HeaderText = "QIH"
            .Columns.Item(QtyInHand).Width = 90
            .Columns.Item(QtyInHand).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns.Item(QtyInHand).DefaultCellStyle.BackColor = Color.LightGreen
            .Columns.Item(QtyInHand).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns.Item(CostAverage).HeaderText = "CostAvg"
            .Columns.Item(CostAverage).Width = 90
            .Columns.Item(CostAverage).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns.Item(CostAverage).DefaultCellStyle.BackColor = Color.LightPink
            .Columns.Item(CostAverage).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns.Item(intReceivedQty).HeaderText = "Rec. Qty"
            .Columns.Item(intReceivedQty).Width = 90
            .Columns.Item(intReceivedQty).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns.Item(intReceivedQty).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns.Item(intIssuedQty).HeaderText = "Issu. Qty"
            .Columns.Item(intIssuedQty).Width = 90
            .Columns.Item(intIssuedQty).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns.Item(intIssuedQty).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns.Item(priceAvg).HeaderText = "Price Avg."
            .Columns.Item(priceAvg).Width = 90
            .Columns.Item(priceAvg).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns.Item(priceAvg).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns.Item(totalPrice).HeaderText = "Total Value"
            .Columns.Item(totalPrice).Width = 90
            .Columns.Item(totalPrice).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Item(totalPrice).DefaultCellStyle.BackColor = Color.LightGreen
            .Columns.Item(totalPrice).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns.Item(totalPrice).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns.Item(profit).HeaderText = "Profit"
            .Columns.Item(profit).Width = 90
            .Columns.Item(profit).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Item(profit).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns.Item(profit).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns.Item(ConstItemId).HeaderText = "ItemId"
            .Columns.Item(ConstItemId).Visible = False
            If chksuppllierwise.Checked Then
                .Columns.Item("CSCode").Visible = False
                .Columns.Item("AccDescr").Visible = False

            End If
            .Columns.Item("lnk").Visible = False
            .Columns.Item("dtfrom").Visible = False
            .Columns.Item("dtto").Visible = False
            .Columns.Item("reportWise").Visible = False
            .Columns.Item("Isuptodate").Visible = False
           
        End With
        resizeGridColumn(grdItem, TrDescr)
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        resizeGridColumn(grdItem, TrDescr)
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub grdItem_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItem.CellEnter
       

    End Sub


    Private Sub ldSupplier(ByVal currentindex As Integer)
        'Dim currentindex As Integer

        Dim table As DataTable
        Dim tp As Integer
        If chksuppllierwise.Checked And Not chkallsupp.Checked Then
            table = _report.returnItemCostbySupplier(0, 1).Tables.Item(0)
        Else
            If grdItem.RowCount = 0 Then Exit Sub
            tp = 0
            'If Not grdItem.CurrentRow Is Nothing Then
            '    currentindex = grdItem.CurrentRow.Index
            'Else
            '    currentindex = 0
            'End If
            table = _report.returnItemCostbySupplier(Val(grdItem.Item(ConstItemId, currentindex).Value), 0).Tables.Item(0)
        End If
        grdSup.DataSource = Nothing
        grdSup.DataSource = table
        If Not dtTransaction Is Nothing Then
            dtTransaction.Rows.Clear()
        End If
        SetGridSupplier()
        'setComboGridSup()
    End Sub
    Private Sub SetGridSupplier()
        With grdSup
            SetGridProperty(grdSup)
            .Columns.Item("Alias").HeaderText = "Sup. code"
            .Columns.Item("Alias").Width = 100
            .Columns.Item("Alias").Frozen = True
            .Columns.Item("AccDescr").HeaderText = "Description"
            .Columns.Item("AccDescr").Width = 330
            If Not chksuppllierwise.Checked Then
                .Columns.Item("LastTr").HeaderText = "Last Inv."
                .Columns.Item("LastTr").Width = 100
                .Columns.Item("Itemprice").HeaderText = "Last Price"
                .Columns.Item("Itemprice").Width = &H55
                .Columns.Item("Itemprice").DefaultCellStyle.Format = "N" & 2
                .Columns.Item("Itemprice").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns.Item("Itemprice").DefaultCellStyle.BackColor = Color.LightGreen
            End If

            .Columns.Item("AccountNo").Visible = False
        End With
        resizeGridColumn(grdSup, 1)
    End Sub
    Private Sub setComboGridSup()
        cmbOrder.Items.Clear()
        Dim i As Integer = 0
        Dim cnt As Integer = (grdSup.ColumnCount - 2)
        For i = 0 To cnt
            cmbOrder.Items.Add(grdSup.Columns.Item(i).HeaderText)
        Next
        If (cmbOrder.Items.Count > 0) Then
            cmbOrder.SelectedIndex = 0
        End If
    End Sub

    Private Sub returnItemwiseTransactionList(ByVal Itmcurrentindex As Integer, ByVal supCurrentindex As Integer)
        Try
            'Dim Itmcurrentindex As Integer
            'Dim supCurrentindex As Integer
            If grdItem.RowCount = 0 Then Exit Sub
            If grdSup.RowCount = 0 Then Exit Sub
            'If Not grdItem.CurrentRow Is Nothing Then
            '    Itmcurrentindex = grdItem.CurrentRow.Index
            'Else
            '    Itmcurrentindex = 0
            'End If
            'If Not grdSup.CurrentRow Is Nothing Then
            '    supCurrentindex = grdSup.CurrentRow.Index
            'Else
            '    supCurrentindex = 0
            'End If
            Dim tp As Integer
            If chksuppllierwise.Checked Then
                tp = 1
            Else
                tp = 0
            End If
            dtTransaction = _report.returnItemwiseTransactionList(Val(grdItem.Item(ConstItemId, Itmcurrentindex).Value), Val(grdSup.Item("Accountno", supCurrentindex).Value), DateValue(cldrStartDate.Value), DateValue(dtpto.Value), "IP", tp).Tables.Item(0)
            grdTransactions.DataSource = dtTransaction
            SetGridHeadTransaction()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub SetGridHeadTransaction()
        With grdTransactions
            SetGridProperty(grdTransactions)
            .ColumnHeadersHeight = 200
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke
            .ColumnHeadersDefaultCellStyle.Font = New Font("Arial", 9.0!)
            .Columns.Item("Trdate").HeaderText = "Tr. Date"
            .Columns.Item("Trdate").Width = &H55
            .Columns.Item("InvNo").HeaderText = "Inv No."
            .Columns.Item("InvNo").Width = 80
            .Columns.Item("InvNo").DefaultCellStyle.BackColor = Color.LightSteelBlue
            .Columns.Item("TrRefNo").HeaderText = "Reference"
            .Columns.Item("TrRefNo").Width = 90
            .Columns.Item("TrDescription").HeaderText = "Description"
            .Columns.Item("TrDescription").Width = 150
            .Columns.Item("DocLstTxt").HeaderText = "LPO"
            .Columns.Item("DocLstTxt").Width = &H4B
            .Columns.Item("Jbcode").HeaderText = "Job Code"
            .Columns.Item("Jbcode").Width = 100
            .Columns.Item("TrQty").HeaderText = "Tr Qty"
            .Columns.Item("TrQty").Width = 90
            .Columns.Item("TrQty").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns.Item("TrQty").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns.Item("TrQty").DefaultCellStyle.BackColor = Color.LightGreen
            .Columns.Item("UnitCost").HeaderText = "Unit Cost"
            .Columns.Item("UnitCost").Width = 100
            .Columns.Item("UnitCost").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns.Item("UnitCost").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns.Item("UnitCost").Frozen = True
            .Columns.Item("Itemid").Visible = False
        End With


    End Sub

    Private Sub txtSeq_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeq.TextChanged
        grdItem.DataSource = SearchGrid(_gridItems, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
    End Sub

    Private Sub txtsupsearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtsupsearch.TextChanged
        grdSup.DataSource = SearchGrid(_gridItems, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
    End Sub

    Private Sub btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnload.Click

        If Not grdSup.CurrentRow Is Nothing Then
            supRindex = grdSup.CurrentRow.Index
        End If
        loadWaite(1)
        supRindex = 0
        'lditemdetails()
    End Sub

    Private Sub chkdate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkdate.CheckedChanged
        pldate.Enabled = chkdate.Checked
        btnApply.Enabled = False
    End Sub

    Private Sub chksuppllierwise_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chksuppllierwise.CheckedChanged
        If chgbyprg Then Exit Sub
        If chksuppllierwise.Checked Then
            grdItem.DataSource = Nothing
            chkdate.Checked = True
            loadWaite(2)
            'ldSupplier(0)
        Else
            chkdate.Checked = False
            loadWaite(1)
            'lditemdetails(0)

        End If
    End Sub

    Private Sub grdSup_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSup.RowEnter
        If Not chgbyprg Then
            If chksuppllierwise.Checked And Not chkallsupp.Checked Then
                'lditemdetails(e.RowIndex)
                supRindex = e.RowIndex
                loadWaite(1)
                supRindex = 0
            End If
            Dim cRow As Integer
            If grdItem.CurrentRow Is Nothing Then
                cRow = 0
            Else
                cRow = grdItem.CurrentRow.Index
            End If
            returnItemwiseTransactionList(cRow, e.RowIndex)
        End If
    End Sub

    Private Sub grdItem_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItem.RowEnter
        Try
            If Not chgbyprg Then
                If Not chksuppllierwise.Checked Or (chksuppllierwise.Checked And chkallsupp.Checked) Then
                    supRindex = e.RowIndex
                    loadWaite(2)
                    supRindex = 0
                End If
                Dim cRow As Integer
                If grdSup.CurrentRow Is Nothing Then
                    cRow = 0
                Else
                    cRow = grdSup.CurrentRow.Index
                End If
                returnItemwiseTransactionList(e.RowIndex, cRow)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Dim RptType As String = ""
        Dim RptCaption As String = ""
        If chksuppllierwise.Checked Then
            RptType = "STMSUP"
        Else
            RptType = "STM"
        End If
        fRptFormat = New RptFormatfrm
        fRptFormat.RptType = RptType
        fRptFormat.ShowDialog()
        fRptFormat = Nothing
    End Sub
    Public Sub PrepareReport(ByVal FileName As String, ByVal RptCaption As String, ByVal forpint As Boolean)
        If Not frm Is Nothing Then
            frm.Close()
            frm = New ReprtviewNEWfrm
        Else
            frm = New ReprtviewNEWfrm
        End If
        Dim ds As New DataSet
        Dim dtRpt = returnToReportTable(_gridItems)
        ds.Tables.Add(dtRpt)
        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = fMainForm
        frm.Text = RptCaption
        frm.Show()
    End Sub

    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        PrepareReport(RptFlName, RptCaption, forPrint)
    End Sub

    Private Sub rdofastmove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdofastmove.Click, rdofastmove.Click, rdoprofit.Click, rdoqty.Click, rdovalue.Click, rdoslow.Click
        btnApply.Enabled = False
    End Sub
    Private Sub frm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles frm.FormClosed
        frm = Nothing
    End Sub

    Private Sub chkallsupp_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkallsupp.CheckedChanged
        chksuppllierwise.Enabled = Not chkallsupp.Checked
        chgbyprg = True
        chksuppllierwise.Checked = True
        chgbyprg = False
        If chkallsupp.Checked Then
            loadWaite(1)
        Else
            loadWaite(2)
        End If

    End Sub
    Private Sub fwait_triggerEvent() Handles fwait.triggerEvent
        Select Case fwait.triggerType
            Case 1
                lditemdetails(0)
            Case 2
                ldSupplier(0)
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
End Class