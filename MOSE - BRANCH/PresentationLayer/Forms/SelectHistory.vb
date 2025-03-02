
Public Class SelectHistory
    Private tmpMyQry As String
    Private strMyQry As String
    Private strMyCaption As String = ""
    Private cmbShowIndex As Integer
    Private dtTable As DataTable
    Public serialno As String

    Public strType As String
    Public Itemid As Long
    Public ItemCode As String
    Public PartyId As Long
    Public jobcode As String
    'object declarations
    Dim _objcmnbLayer As New clsCommon_BL
    Dim _objItmMast As New clsItemMast_BL


    'const declaration
    Const ConstInvNo = 0
    Const ConstTrDate = 1
    Const ConstTrRefNo = 2
    Const ConstLPO = 3
    Const ConstAccDescr = 4
    Const ConstItemCode = 5
    Const ConstIDescription = 6
    Const ConstTrQty = 7
    Const ConstUnitCost = 8
    Const ConstItemid = 9
    Const ConstTrid = 10
    Private Sub SelectHistory_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If serialno <> "" Then
            setSerialNumber()
            setComboGrid()
            Panel1.Visible = False
            Me.Text = strMyCaption
        Else
            setEnquiryLoad()
        End If
        txtSearch.Select()
    End Sub
    Public Sub setSerialNumber()
        dtTable = _objcmnbLayer.returnAvailableSerialNumber(3, serialno, "")
        grdVoucher.DataSource = dtTable
        SetGridSerialTr()
    End Sub
    Private Sub SetGridSerialTr()
        SetGridProperty(grdVoucher)
        With grdVoucher
            .Columns("TrId").Visible = False
            .Columns("Item Name").Width = 150
            .Columns("Inv No").Width = 75
            .Columns("T type").Width = 75
            .Columns("Party Name").Width = 250
            .Columns("Item Name").Width = 250
        End With
    End Sub
    Public Sub setEnquiryLoad()
        fillDataGrid()
        setComboGrid()
        Me.Text = strMyCaption
    End Sub
    Private Sub setComboGrid()
        Dim i As Integer = 0
        For i = 0 To IIf(grdVoucher.ColumnCount - 1 >= cmbShowIndex, cmbShowIndex, grdVoucher.ColumnCount - 1)
            cmbSearch.Items.Add(grdVoucher.Columns(i).HeaderText)
        Next
        If cmbSearch.Items.Count > 0 Then cmbSearch.SelectedIndex = 0
    End Sub
    Private Sub fillDataGrid()
        Select Case strType
            Case "IP", "IS"
                strMyCaption = "Inventory " & IIf(strType = "IS", "Sales", "Purchase") & " History of [ " & ItemCode & " ]"
                cmbShowIndex = 14
            Case "SR", "PR"
                strMyCaption = "Inventory " & IIf(strType = "SR", "Sales Return ", "Purchase Return ") & " History of [ " & ItemCode & " ]"
                cmbShowIndex = 14
            Case "DOC", "DOS"
                strMyCaption = "Inventory " & IIf(strType = "DOC", "Delivery Order Customer ", "Delivery Order Supplier ") & " History of [ " & ItemCode & " ]"

                cmbShowIndex = 14
            Case "ENQ"

            Case "PHI", "PHO"

            Case "PO", "SO"
                strMyCaption = "Inventory " & IIf(strType = "PO", "Purchase Order ", "Sales Order ") & " History of [ " & ItemCode & " ]"
                cmbShowIndex = 14

            Case "QTI", "QTR"
                strMyCaption = "Inventory " & IIf(strType = "QTR", "Quotations Received", "Quotations Issued ") & " History of [ " & ItemCode & " ]"
                cmbShowIndex = 14

            Case "TI", "TO"
                strMyCaption = "Inventory " & IIf(strType = "TI", "Transaction IN", "Transaction OUT ") & " History of [ " & ItemCode & " ]"
                cmbShowIndex = 14
            Case "MI"
                strMyCaption = "Inventory Production IN History of [ " & ItemCode & " ]"
                cmbShowIndex = 14
            Case "MO"
                strMyCaption = "Inventory Production Out History of [ " & ItemCode & " ]"
                cmbShowIndex = 14
            Case "JT"
                strMyCaption = "Item Transactions"
                cmbShowIndex = 6
            Case Else
                Exit Sub
        End Select
        With _objItmMast
            _objItmMast.ItemId = Itemid
            .CustomerOrSupplierId = PartyId
            .InventoryType = strType
            .DateFrom = Format(dt_From.Value, DtFormat)
            .DateTo = dt_To.Value
            .Branch = UsrBr
        End With

        grdVoucher.DataSource = Nothing
        Try
            Dim tp As Integer
            If strType = "PO" Then
                tp = 1
            ElseIf strType = "JT" Then 'Job item transactions
                tp = 2
            End If
            If tp = 0 And PartyId > 0 Then
                tp = 4
            End If
            dtTable = _objItmMast.returnTransactionHistoryForItem(tp, jobcode)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        grdVoucher.DataSource = dtTable
        SetGridHead()
    End Sub

    Private Sub SetGridHead()
        With grdVoucher
            .ReadOnly = True
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = True
            .ColumnHeadersHeight = 100
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersDefaultCellStyle.BackColor = Color.Olive
            .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
            .RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 9.0!)
            Select Case strType
                Case "IS", "IP", "SR", "PR"
                    .Columns("InvNo").HeaderText = "InvNo"
                    .Columns("TrDate").HeaderText = "TrDate"
                    .Columns("TrRefNo").HeaderText = "TrRefNo"
                    .Columns("LPO").HeaderText = "LPO"
                    .Columns("AccDescr").HeaderText = "Party Name"
                    .Columns("itemcode").HeaderText = "Item Code"
                    .Columns("IDescription").HeaderText = "IDescription"
                    .Columns("TrQty").HeaderText = "TrQty"
                    .Columns("TrQty").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns("UnitCost").HeaderText = "UnitCost"
                    .Columns("UnitCost").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns("UnitCost").DefaultCellStyle.Format = "N" & 2
                    .Columns("ItemID").Visible = False
                    .Columns("TrId").Visible = False
                Case "PO"
                    .Columns("DNo").HeaderText = "Doc No"
                    .Columns("DDate").HeaderText = "Doc Date"
                    '.Columns("TrRefNo").HeaderText = "TrRefNo"
                    .Columns("AccDescr").HeaderText = "Party Name"
                    .Columns("AccDescr").Width = 150
                    .Columns("itemcode").HeaderText = "Item Code"
                    .Columns("TrDetail").HeaderText = "Item Name"
                    .Columns("TrDetail").Width = 150
                    .Columns("Qty").HeaderText = "Qty"
                    .Columns("Qty").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns("UnitCost").HeaderText = "UnitCost"
                    .Columns("UnitCost").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns("UnitCost").DefaultCellStyle.Format = "N" & 2
                    .Columns("ItemID").Visible = False
                    .Columns("DocId").Visible = False
                Case "JT"
                    .Columns("TrId").Visible = False
                    .Columns("InQty").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns("InQty").DefaultCellStyle.Format = "N" & 2
                    .Columns("OutQty").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns("OutQty").DefaultCellStyle.Format = "N" & 2
                    .Columns("AccDescr").HeaderText = "Party Name"
                    resizeGridColumn(grdVoucher, 3)
            End Select
        End With
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        grdVoucher.DataSource = SearchGrid(dtTable, Trim(txtSearch.Text), cmbSearch.SelectedIndex)
    End Sub

    Private Sub grdVoucher_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellDoubleClick
        Me.Hide()
        If strType = "IS" Then 'IS
            fMainForm.LoadIS(Val(grdVoucher.Item(ConstTrid, grdVoucher.CurrentRow.Index).Value))
        ElseIf strType = "IP" Then 'IP
            fMainForm.LoadIP(Val(grdVoucher.Item(grdVoucher.ColumnCount - 1, grdVoucher.CurrentRow.Index).Value))
        ElseIf strType = "SR" Then 'SR
            fMainForm.LoadSR(Val(grdVoucher.Item(grdVoucher.ColumnCount - 1, grdVoucher.CurrentRow.Index).Value))
        ElseIf strType = "PR" Then 'PR
            fMainForm.LoadPR(Val(grdVoucher.Item(grdVoucher.ColumnCount - 1, grdVoucher.CurrentRow.Index).Value))
        ElseIf strType = "PO" Then 'PUrchase Order
            'fMainForm.LoadPO(Val(grdVoucher.Item(grdVoucher.ColumnCount - 1, grdVoucher.CurrentRow.Index).Value))
            'ElseIf strType = "SO" Then 'Sales Order
            '    fMainForm.ldSOEdit(Val(grdVoucher.Item(grdVoucher.ColumnCount - 1, grdVoucher.CurrentRow.Index).Value))
            'ElseIf strType = "QTI" Then 'Quotations Issued
            '    fMainForm.LoadQTIEdt(Val(grdVoucher.Item(grdVoucher.ColumnCount - 1, grdVoucher.CurrentRow.Index).Value))
            'ElseIf strType = "QTR" Then 'Quotations Received
            '    fMainForm.toLoadQuotationReceived(Val(grdVoucher.Item(grdVoucher.ColumnCount - 1, grdVoucher.CurrentRow.Index).Value))
            'ElseIf strType = "TI" Then 'Transaction IN
            '    fMainForm.toLoadStockTransferIn(Val(grdVoucher.Item(grdVoucher.ColumnCount - 1, grdVoucher.CurrentRow.Index).Value))
            'ElseIf strType = "TO" Then 'Transaction OUT
            '    fMainForm.toLoadStockTransferOut(Val(grdVoucher.Item(grdVoucher.ColumnCount - 1, grdVoucher.CurrentRow.Index).Value))
        Else
            Exit Sub
        End If

    End Sub

    Private Sub grdVoucher_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdVoucher.ColumnHeaderMouseClick
        If e.ColumnIndex > 0 And cmbSearch.Items.Count > e.ColumnIndex Then
            cmbSearch.SelectedIndex = e.ColumnIndex
        End If
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        setEnquiryLoad()
        txtSearch.Select()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub grdVoucher_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellContentClick

    End Sub

    Private Sub SelectHistory_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
     

    End Sub
End Class