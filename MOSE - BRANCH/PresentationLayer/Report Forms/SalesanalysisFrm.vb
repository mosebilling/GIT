Public Class SalesanalysisFrm
    Private _report As New clsReport_BL
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
    Private chgbyprg As Boolean
    Private _gridItems As DataTable
    Private _rptTable As DataTable
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fRptFormat As RptFormatfrm
    Private WithEvents fwait As WaitMessageFrm
    Private _objReport As New clsReport_BL
    Private Sub SalesanalysisFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmboptions.SelectedIndex = 0
        cldrStartDate.Value = Format(DateAdd(DateInterval.Day, firstDateFromToday * -1, DateValue(Date.Now)), DtFormat)
        'ldCustomerdetails()
        Timer1.Enabled = True
    End Sub
    Public Function returnOutstanding(ByVal _vDtable As DataTable)
        Dim bDatatable As DataTable
        If _vDtable.Rows.Count = 0 Then bDatatable = _vDtable.Clone : GoTo nxt
        Dim _qurey As EnumerableRowCollection(Of DataRow)
        _qurey = From data In _vDtable.AsEnumerable() Where data("balance") > 0 Select data
        If _qurey.Count > 0 Then
            bDatatable = _qurey.CopyToDataTable()
        Else
            bDatatable = _vDtable.Clone
        End If
nxt:
        Return bDatatable
    End Function
    Private Sub ldCustomerdetails()
        Try
            Dim tp As Integer
            If cmboptions.SelectedIndex = 0 Then
                tp = 0
            Else
                tp = cmboptions.SelectedIndex - 1
            End If
            grdItem.DataSource = Nothing
            _gridItems = _report.returnCustomerwiseSalesAnalysis(DateValue(cldrStartDate.Value), DateValue(dtpto.Value), tp)
            If chkoutstanding.Checked Then
                _gridItems = returnOutstanding(_gridItems)
            End If
            grdItem.DataSource = _gridItems
            If cmboptions.SelectedIndex = 0 Then
                SetGridHeadCustomer()
            ElseIf cmboptions.SelectedIndex = 2 Then
                SetGridHeadInvoice()
            Else
                SetGridHeadSummary()
            End If
            setComboGrid()
            btnApply.Enabled = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Private Sub SetGridHeadSummary()
        With grdItem
            SetGridProperty(grdItem)
            Select Case cmboptions.SelectedIndex
                Case 3
                    .Columns.Item("filterby").HeaderText = "Date"
                    .Columns.Item("TrDate").Visible = False
                Case 4
                    .Columns.Item("filterby").HeaderText = "Month"
                    .Columns.Item("TrDate").Visible = False
                Case 5
                    .Columns.Item("filterby").HeaderText = "Year"
                    .Columns.Item("TrDate").Visible = False
            End Select
            .Columns.Item("TrDate").Width = 100

            .Columns.Item("casshsales").HeaderText = "Cash"
            .Columns.Item("casshsales").Width = 100
            .Columns.Item("casshsales").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns.Item("casshsales").DefaultCellStyle.BackColor = Color.LightGreen
            .Columns.Item("casshsales").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns.Item("creditsales").HeaderText = "Credit"
            .Columns.Item("creditsales").Width = 100
            .Columns.Item("creditsales").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns.Item("creditsales").DefaultCellStyle.BackColor = Color.LightGreen
            .Columns.Item("creditsales").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns.Item("banksales").HeaderText = "Bank"
            .Columns.Item("banksales").Width = 100
            .Columns.Item("banksales").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns.Item("banksales").DefaultCellStyle.BackColor = Color.LightGreen
            .Columns.Item("banksales").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns.Item("Total").HeaderText = "Total"
            .Columns.Item("Total").Width = 100
            .Columns.Item("Total").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns.Item("Total").DefaultCellStyle.BackColor = Color.LightGreen
            .Columns.Item("Total").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns.Item("collected").HeaderText = "Collected"
            .Columns.Item("collected").Width = 100
            .Columns.Item("collected").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns.Item("collected").DefaultCellStyle.BackColor = Color.LightGreen
            .Columns.Item("collected").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns.Item("lnk").Visible = False
            .Columns.Item("LDATE").Visible = False
            resizeGridColumn(grdItem, 0)
        End With
    End Sub
    Private Sub SetGridHeadInvoice()
        With grdItem
            SetGridProperty(grdItem)
            .Columns.Item("TrDate").HeaderText = "Date"
            .Columns.Item("TrDate").Width = 100

            .Columns.Item("customer").HeaderText = "Customer Name"
            .Columns.Item("customer").Width = 150

            .Columns.Item("customerPhone").HeaderText = "Phone"

            .Columns.Item("InvNo").HeaderText = "InvNo"
            .Columns.Item("InvNo").Width = 100

            .Columns.Item("Total").HeaderText = "Net Total"
            .Columns.Item("Total").Width = 100
            .Columns.Item("Total").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns.Item("Total").DefaultCellStyle.BackColor = Color.LightGreen
            .Columns.Item("Total").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns.Item("collected").HeaderText = "Collected"
            .Columns.Item("collected").Width = 100
            .Columns.Item("collected").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns.Item("collected").DefaultCellStyle.BackColor = Color.LightGreen
            .Columns.Item("collected").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns.Item("Balance").HeaderText = "Balance"
            .Columns.Item("Balance").Width = 100
            .Columns.Item("Balance").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns.Item("Balance").DefaultCellStyle.BackColor = Color.LightGreen
            .Columns.Item("Balance").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns.Item("lnk").Visible = False
            .Columns.Item("datefrom").Visible = False
            .Columns.Item("dateto").Visible = False
            .Columns.Item("trid").Visible = False
            resizeGridColumn(grdItem, 2)
        End With
    End Sub
    Private Sub SetGridHeadCustomer()
        With grdItem
            SetGridProperty(grdItem)
            .Columns.Item("customer").HeaderText = "Customer Name"
            .Columns.Item("customer").Width = 150

            .Columns.Item("Add1").HeaderText = "Address"
            .Columns.Item("Add1").Width = 150

            .Columns.Item("phone").HeaderText = "Phone"
            .Columns.Item("phone").Width = 100

            .Columns.Item("ContactName").HeaderText = "Contact Name"
            .Columns.Item("ContactName").Width = 150

            .Columns.Item("Opbal").HeaderText = "Opening"
            .Columns.Item("Opbal").Width = 100
            .Columns.Item("Opbal").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns.Item("Opbal").DefaultCellStyle.BackColor = Color.LightGreen
            .Columns.Item("Opbal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns.Item("NetTot").HeaderText = "Net Total"
            .Columns.Item("NetTot").Width = 100
            .Columns.Item("NetTot").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns.Item("NetTot").DefaultCellStyle.BackColor = Color.LightGreen
            .Columns.Item("NetTot").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns.Item("collected").HeaderText = "Collected"
            .Columns.Item("collected").Width = 100
            .Columns.Item("collected").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns.Item("collected").DefaultCellStyle.BackColor = Color.LightGreen
            .Columns.Item("collected").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns.Item("Balance").HeaderText = "Balance"
            .Columns.Item("Balance").Width = 100
            .Columns.Item("Balance").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns.Item("Balance").DefaultCellStyle.BackColor = Color.LightGreen
            .Columns.Item("Balance").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


            .Columns.Item("accid").Visible = False
            .Columns.Item("CashCustid").Visible = False
            .Columns.Item("lnk").Visible = False
            .Columns.Item("datefrom").Visible = False
            .Columns.Item("dateto").Visible = False
            resizeGridColumn(grdItem, 0)
        End With
    End Sub
    Private Sub lditemdetails()
        Try
            Dim condition As String = ""
            Dim dtcondition As String = ""
            Dim CScode As String = ""
            Dim tp As Integer
            Dim reportwise As String = ""
            If chksolditem.Checked Then
                condition = "where totalPrice>0 "
            End If
            condition = condition & " Order by totalPrice desc"
            reportwise = "2"
            dtcondition = " where trdate>='" & Format(DateValue(cldrStartDate.Value), "yyyy/MMM/dd") & "'"
            dtcondition = dtcondition & " and trdate<='" & Format(DateValue(dtpto.Value), "yyyy/MMM/dd") & "'"
            dtcondition = dtcondition & IIf(UsrBr = "", "", " and brid='" & UsrBr & "'")
            tp = 4
            _gridItems = _report.returnStockMovement(tp, condition, dtcondition, CScode, DateValue(cldrStartDate.Value), DateValue(dtpto.Value), reportwise, "N").Tables(0)
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
            '.Columns.Item(intReceivedQty).HeaderText = "Rec. Qty"
            '.Columns.Item(intReceivedQty).Width = 90
            '.Columns.Item(intReceivedQty).DefaultCellStyle.Format = "N" & NoOfDecimal
            '.Columns.Item(intReceivedQty).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns.Item(intIssuedQty).HeaderText = "Sales Qty"
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
            
            .Columns.Item("lnk").Visible = False
            .Columns.Item("dtfrom").Visible = False
            .Columns.Item("dtto").Visible = False
            .Columns.Item("reportWise").Visible = False
            .Columns.Item("Isuptodate").Visible = False

        End With
        resizeGridColumn(grdItem, TrDescr)
    End Sub

    Private Sub cmboptions_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmboptions.SelectedIndexChanged
        If cmboptions.SelectedIndex = 1 Then
            chksolditem.Visible = True
            loadWaite(1)
        Else
            chksolditem.Visible = False
            chksolditem.Checked = False
            chkoutstanding.Visible = False
            chkoutstanding.Checked = False
            If cmboptions.SelectedIndex = 2 Then
                chkoutstanding.Visible = True
                chkoutstanding.Top = chksolditem.Top
            End If
            loadWaite(2)
        End If
    End Sub

    Private Sub grdItem_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItem.RowEnter
        Try
            If Not chgbyprg Then
                If cmboptions.SelectedIndex = 2 Then
                    ldDocumentItems(e.RowIndex)
                Else
                    returnItemwiseTransactionList(e.RowIndex)
                End If
                
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub
    Private Sub returnItemwiseTransactionList(ByVal rindex As Integer)
        Try
            Dim dtTransaction As DataTable
            If grdItem.RowCount = 0 Then Exit Sub
            Dim tp As Integer
            Dim accid As Integer
            Dim itemid As Long
            Dim datefrom As Date = DateValue(cldrStartDate.Value)
            Dim dateto As Date = DateValue(dtpto.Value)
            If cmboptions.SelectedIndex = 0 Then
                If Val(grdItem.Item("CashCustid", rindex).Value & "") > 0 Then
                    tp = 4
                    accid = Val(grdItem.Item("CashCustid", rindex).Value)
                Else
                    tp = 2
                    accid = Val(grdItem.Item("accid", rindex).Value)
                End If
            ElseIf cmboptions.SelectedIndex = 1 Then
                tp = 3
                itemid = Val(grdItem.Item("itemid", rindex).Value)
            ElseIf cmboptions.SelectedIndex = 3 Then
                With grdItem
                    datefrom = DateValue(.Item("filterby", rindex).Value)
                    dateto = datefrom
                End With
                tp = 5
            ElseIf cmboptions.SelectedIndex = 4 Or cmboptions.SelectedIndex = 5 Then
                With grdItem
                    datefrom = DateValue(.Item("trdate", rindex).Value)
                    dateto = DateValue(.Item("LDATE", rindex).Value)
                End With
                tp = 5
            Else
                Exit Sub
            End If
            grdTransactions.DataSource = Nothing
            dtTransaction = _report.returnItemwiseTransactionList(itemid, accid, datefrom, dateto, "IS", tp).Tables.Item(0)
            grdTransactions.DataSource = dtTransaction
            SetGridHeadTransaction()
            If cmboptions.SelectedIndex = 1 Then
                resizeGridColumn(grdTransactions, 7)
            Else
                resizeGridColumn(grdTransactions, 4)
            End If

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
            If cmboptions.SelectedIndex = 1 Then
                .Columns.Item("TrQty").HeaderText = "Tr Qty"
                .Columns.Item("TrQty").Width = 90
                .Columns.Item("CustName").HeaderText = "Customer"
                .Columns.Item("CustName").Width = 250
                .Columns.Item("TrQty").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns.Item("TrQty").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns.Item("TrQty").DefaultCellStyle.BackColor = Color.LightGreen
                .Columns.Item("UnitCost").HeaderText = "Unit Cost"
                .Columns.Item("UnitCost").Width = 100
                .Columns.Item("UnitCost").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns.Item("UnitCost").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns.Item("UnitCost").Frozen = True
                .Columns.Item("Itemid").Visible = False
            ElseIf cmboptions.SelectedIndex > 2 Then
                .Columns.Item("trid").Visible = False
            End If
            .Columns.Item("total").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns.Item("total").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns.Item("total").DefaultCellStyle.BackColor = Color.LightGreen
            
        End With


    End Sub

    Private Sub btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnload.Click
        If cmboptions.SelectedIndex = 1 Then
            loadWaite(1)
        Else
            loadWaite(2)
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        resizeGridColumn(grdItem, 0)
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub txtSeq_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeq.TextChanged
        grdItem.DataSource = SearchGrid(_gridItems, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
        _rptTable = SearchGrid(_gridItems, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Dim RptType As String = ""
        Dim RptCaption As String = ""
        If cmboptions.SelectedIndex = 0 Then
            RptType = "SACUS"
        ElseIf cmboptions.SelectedIndex = 1 Then
            RptType = "SAITM"
        ElseIf cmboptions.SelectedIndex = 3 Then
            RptType = "DSS"
        ElseIf cmboptions.SelectedIndex = 4 Then
            RptType = "MSS"
        ElseIf cmboptions.SelectedIndex = 5 Then
            RptType = "YSS"
        Else
            RptType = "SAINV"
        End If
        If chkFormat.Checked Then
            fRptFormat = New RptFormatfrm
            fRptFormat.RptType = RptType
            fRptFormat.ShowDialog()
            fRptFormat = Nothing
        Else
            Dim RptName As String
            RptName = getRptDefFlName(RptType, RptCaption)
            If RptName <> "" Then
                PrepareReport(RptName, "Sales Analysis", False)
            End If
        End If

        'fRptFormat = New RptFormatfrm
        'fRptFormat.RptType = RptType
        'fRptFormat.ShowDialog()
        'fRptFormat = Nothing
    End Sub
    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        PrepareReport(RptFlName, RptCaption, forPrint)
    End Sub

    Private Sub frm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles frm.FormClosed
        frm = Nothing
    End Sub
    Public Sub PrepareReport(ByVal FileName As String, ByVal RptCaption As String, ByVal forpint As Boolean)
        If Not frm Is Nothing Then
            frm.Close()
            frm = New ReprtviewNEWfrm
        Else
            frm = New ReprtviewNEWfrm
        End If
        Dim ds As New DataSet
        If Not _rptTable Is Nothing Then
            ds.Tables.Add(_rptTable)
        Else
            Dim dtRpt = returnToReportTable(_gridItems)
            ds.Tables.Add(dtRpt)
        End If
        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = fMainForm
        frm.Text = RptCaption
        frm.Show()
    End Sub

    Private Sub chksolditem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chksolditem.Click
        'lditemdetails()
        loadWaite(1)
    End Sub
    Private Sub ldDocumentItems(ByVal rIndex As Integer)
        Dim itmDatatable As DataTable
        Dim qryTp As Integer
        qryTp = 1
        grdTransactions.DataSource = Nothing
        Try
            itmDatatable = _objReport.returnItemsToListForm("returnInventoryItemsToListForm", Val(grdItem.Item(grdItem.ColumnCount - 1, rIndex).Value), qryTp).Tables(0)
            grdTransactions.DataSource = itmDatatable
            setItemGridHead()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub setItemGridHead()
        With grdTransactions
            SetGridProperty(grdTransactions)
            .Columns("Item Code").Width = 100
            .Columns("IDescription").Width = 200
            .Columns("Unit").Width = 50
            .Columns("TrQty").Width = 70
            .Columns("TrQty").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("TrQty").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("UnitCost").Width = 100
            .Columns("UnitCost").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("UnitCost").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("LineTotal").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("LineTotal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Taxp").Width = 100
            .Columns("Taxp").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Taxp").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("TaxAmt").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("TaxAmt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Cess").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Cess").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("FOC").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("FOC").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("FOC").Visible = enableFOCQty
            .Columns("FOC").Width = 70
            .Columns("SerialNo").Visible = enableSerialnumber Or enableBatchwiseTr
            If enablecess Then
                .Columns("Cess").Visible = True
            Else
                .Columns("Cess").Visible = False
            End If
            .Columns("itemid").Visible = False
            resizeGridColumn(grdTransactions, 1)
        End With
    End Sub
    Private Sub fwait_triggerEvent() Handles fwait.triggerEvent
        Select Case fwait.triggerType
            Case 1
                lditemdetails()
            Case 2, 3, 4, 5
                ldCustomerdetails()
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