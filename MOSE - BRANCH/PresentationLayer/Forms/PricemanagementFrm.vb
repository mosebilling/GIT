Public Class PricemanagementFrm

#Region "Local Variables"
    Private dtItem As DataTable
    Private chgbyprg As Boolean
    Private SelectRow As Integer
    Private activecontrolname As String
    Private dtcmn As DataTable
    Private chgamt As Boolean
#End Region
#Region "Numeric Text"
    'numeric text
    Dim idx As Integer
    Dim str1 As String
    Dim str2 As String
    Dim str3 As String
    Dim SelStart As Integer
    Dim numCtrl As TextBox

#End Region
#Region "Class Objects"
    Private _objItemDetailsBL As New clsItemMast_BL
    Private _objcmnbLayer As New clsCommon_BL
#End Region
#Region "Form Objects"
    Private WithEvents fGrp As SetGroup
    Private WithEvents ftransfer As TransferToWebFrm
    Private WithEvents fDoc As DocumentView
    Private WithEvents fwait As WaitMessageFrm
    Private WithEvents fshowlocationqty As ShowLocationQtyFrm
#End Region
#Region "Constant Variables"
    Private Const ConstTag = 0
    Private Const ConstItemCode = 1
    Private Const ConstTrDescr = 2
    Private Const ConstUnit = 3
    Private Const ConstHSNCode = 4
    Private Const Constvatcode = 5
    Private Const Constrgccode = 6
    Private Const Constadditionalcess = 7
    Private Const ConstRack = 8
    Private Const Constwebname = 9
    Private Const ConstCostAvg = 10
    Private Const ConstopQty = 11
    Private Const Constopcost = 12
    Private Const ConstUnitPrice = 13
    Private Const Constsecondprice = 14
    Private Const ConstTaxPrice = 15
    Private Const ConstMrp = 16
    Private Const Constwsprice = 17
    Private Const ConstMinSalesPrice = 18
    Private Const ConstItemid = 19
    Private Const Constvatper = 20
    Private Const Constcess = 21

#End Region
    Private Sub PricemanagementFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
       
        Timer1.Enabled = True
        lblstatus.Text = ""
        If enableWebIntegration Then
            btndescription.Visible = True
            btnonline.Visible = True
        Else
            btndescription.Visible = False
            btnonline.Visible = False
        End If
        If EnableBarcode Then btnbarcode.Visible = True
    End Sub
    Private Sub _fillDataGrid()
        Try
            dtcmn = _objItemDetailsBL.returnItemdetailsForPricemanagement(IIf(chksort.Checked, 1, 0))
            dtItem = dtcmn
            chgbyprg = True
            grdPack.DataSource = dtItem
            chgbyprg = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub SetGridHead()
        Try
            SetEntryGridProperty(grdPack)
            With grdPack
                chgbyprg = True
                .Columns.Item(ConstTag).HeaderText = "Tag"
                .Columns.Item(ConstTag).Width = 40
                .Columns.Item(ConstTag).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns.Item(ConstTag).ReadOnly = True
                .Columns.Item(ConstItemCode).HeaderText = "Item Code"
                .Columns.Item(ConstItemCode).Width = 100
                .Columns.Item(ConstItemCode).Frozen = True
                .Columns.Item(ConstItemCode).ReadOnly = True
                .Columns.Item(Constwebname).HeaderText = "Web Name"
                .Columns.Item(ConstTrDescr).Width = 200

                Dim dt As DataTable = _objcmnbLayer._fldDatatable("select Units from dbo.UnitsTb", False)
                Dim dataGridViewColumn As New DataGridViewComboBoxColumn
                dataGridViewColumn.Items.Add("")
                Dim num2 As Integer = (dt.Rows.Count - 1)
                Dim i As Integer = 0
                Do While (i <= num2)
                    dataGridViewColumn.Items.Add(dt(i)(0))
                    i += 1
                Loop
                dataGridViewColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
                dataGridViewColumn.HeaderText = "Unit"
                dataGridViewColumn.DataPropertyName = "Unit"
                .Columns.RemoveAt(ConstUnit)
                .Columns.Insert(ConstUnit, dataGridViewColumn)
                .Columns.Item(ConstUnit).Width = 50
                .Columns.Item(ConstUnit).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                chgbyprg = True


                dt = _objcmnbLayer._fldDatatable("select HSNCode from GSTTb", False)
                dataGridViewColumn = New DataGridViewComboBoxColumn
                dataGridViewColumn.Items.Add("")
                num2 = (dt.Rows.Count - 1)
                i = 0
                Do While (i <= num2)
                    dataGridViewColumn.Items.Add(dt(i)(0))
                    i += 1
                Loop
                dataGridViewColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
                dataGridViewColumn.HeaderText = "HSN Code"
                dataGridViewColumn.DataPropertyName = "HSNCode"
                .Columns.RemoveAt(ConstHSNCode)
                .Columns.Insert(ConstHSNCode, dataGridViewColumn)
                .Columns.Item(ConstHSNCode).Width = 100
                .Columns.Item(ConstHSNCode).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


                dt = _objcmnbLayer._fldDatatable("select vatcode from VatMasterTb", False)
                dataGridViewColumn = New DataGridViewComboBoxColumn
                dataGridViewColumn.Items.Add("")
                num2 = (dt.Rows.Count - 1)
                i = 0
                Do While (i <= num2)
                    dataGridViewColumn.Items.Add(dt(i)(0))
                    i += 1
                Loop
                dataGridViewColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
                If enableGCC Then
                    dataGridViewColumn.HeaderText = "Vat"
                Else
                    dataGridViewColumn.HeaderText = "Flood Cess"
                End If

                dataGridViewColumn.DataPropertyName = "vatcode"
                .Columns.RemoveAt(Constvatcode)
                .Columns.Insert(Constvatcode, dataGridViewColumn)
                .Columns.Item(Constvatcode).Width = 100
                .Columns.Item(Constvatcode).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                dataGridViewColumn = New DataGridViewComboBoxColumn
                dataGridViewColumn.Items.Add("")
                num2 = (dt.Rows.Count - 1)
                i = 0
                Do While (i <= num2)
                    dataGridViewColumn.Items.Add(dt(i)(0))
                    i += 1
                Loop
                dataGridViewColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
                dataGridViewColumn.HeaderText = "Cess"
                dataGridViewColumn.DataPropertyName = "rgccode"
                .Columns.RemoveAt(Constrgccode)
                .Columns.Insert(Constrgccode, dataGridViewColumn)
                .Columns.Item(Constrgccode).Width = 100
                .Columns.Item(Constrgccode).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns.Item(Constrgccode).Visible = Not enableGCC


                .Columns.Item(Constadditionalcess).HeaderText = "Add. Cess"
                .Columns.Item(Constadditionalcess).Width = 100
                .Columns.Item(Constadditionalcess).DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns.Item(Constadditionalcess).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns.Item(Constadditionalcess).Visible = Not enableGCC


                .Columns.Item(ConstCostAvg).HeaderText = "Cost Avg."
                .Columns.Item(ConstCostAvg).Width = 100
                .Columns.Item(ConstCostAvg).DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns.Item(ConstCostAvg).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns.Item(ConstCostAvg).ReadOnly = True

                .Columns.Item(Constopcost).HeaderText = "OpnCost"
                .Columns.Item(Constopcost).Width = &H4B
                .Columns.Item(Constopcost).DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns.Item(Constopcost).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                .Columns.Item(ConstMrp).HeaderText = "MRP"
                .Columns.Item(ConstMrp).Width = &H4B
                .Columns.Item(ConstMrp).DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns.Item(ConstMrp).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                .Columns.Item(ConstopQty).HeaderText = "OpnQty "
                .Columns.Item(ConstopQty).Width = &H4B
                .Columns.Item(ConstopQty).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns.Item(ConstopQty).DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns.Item(ConstopQty).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                .Columns.Item(ConstUnitPrice).HeaderText = "S.Price"
                .Columns.Item(ConstUnitPrice).Width = &H4B
                .Columns.Item(ConstUnitPrice).DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns.Item(ConstUnitPrice).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                .Columns.Item(ConstTaxPrice).HeaderText = "Tax Price"
                .Columns.Item(ConstTaxPrice).Width = 90
                .Columns.Item(ConstTaxPrice).DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns.Item(ConstTaxPrice).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                '.Columns.Item(ConstTaxPrice).ReadOnly = True

                .Columns.Item(Constsecondprice).HeaderText = "S. Price2"
                .Columns.Item(Constsecondprice).Width = 90
                .Columns.Item(Constsecondprice).DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns.Item(Constsecondprice).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                .Columns.Item(Constwsprice).HeaderText = "WS.Price"
                .Columns.Item(Constwsprice).Width = 90
                .Columns.Item(Constwsprice).DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns.Item(Constwsprice).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                .Columns.Item(ConstMinSalesPrice).HeaderText = "Minimum Price"
                .Columns.Item(ConstMinSalesPrice).Width = 90
                .Columns.Item(ConstMinSalesPrice).DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns.Item(ConstMinSalesPrice).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns.Item(ConstMinSalesPrice).Visible = False

                .Columns.Item(ConstItemid).HeaderText = "ItemId"
                .Columns.Item(ConstItemid).Visible = False
                .Columns.Item(Constvatper).Visible = False
                .Columns.Item(Constcess).Visible = False
                chgbyprg = False
                setComboGrid()
            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub setComboGrid()
        chgbyprg = True
        cmbOrder.Items.Clear()
        Dim i As Integer = 0
        For i = 0 To grdPack.ColumnCount - 1
            cmbOrder.Items.Add(grdPack.Columns(i).HeaderText)
        Next
        If cmbOrder.Items.Count > 0 Then cmbOrder.SelectedIndex = 2
        chgbyprg = False
    End Sub


    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        loadWaite(1)
    End Sub

    Private Sub PricemanagementFrm_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        'Timer1.Enabled = True
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        Dim num2 As Integer = grdPack.RowCount - 1
        Dim i As Integer = 0
        Do While (i <= num2)
            grdPack.Item(0, i).Value = ""
            i += 1
        Loop

    End Sub

    Private Sub btnexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexit.Click
        Me.Close()
    End Sub

    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        _fillDataGrid()
    End Sub

    Private Sub SearchItemPrice()
        Dim flag As New Boolean
        If (grdPack.Rows.Count <= SelectRow) Then
            MsgBox("No Item Found", MsgBoxStyle.Critical, Nothing)
            SelectRow = 0
        Else
            Dim num2 As Integer = (grdPack.Rows.Count - 1)
            Dim i As Integer = (SelectRow + 1)
            Do While (i <= num2)
                If (i = 10) Then
                    MsgBox("", MsgBoxStyle.ApplicationModal, Nothing)
                End If
                If (cmbOrder.Text = "Unit") Then
                    If UCase(grdPack.Item(ConstUnit, i).Value.ToString).Contains(UCase(txtSeq.Text)) Then
                        SelectRow = i
                        flag = True
                        grdPack.FirstDisplayedScrollingRowIndex = SelectRow
                        grdPack.Rows.Item(SelectRow).Selected = True
                        grdPack.CurrentCell = grdPack.Item(1, SelectRow)
                        grdPack.SelectionMode = DataGridViewSelectionMode.FullRowSelect
                        Exit Do
                    End If
                ElseIf UCase(grdPack.Item(cmbOrder.SelectedIndex, i).Value.ToString).Contains(UCase(txtSeq.Text)) Then
                    SelectRow = i
                    flag = True
                    grdPack.FirstDisplayedScrollingRowIndex = SelectRow
                    grdPack.Rows.Item(SelectRow).Selected = True
                    grdPack.SelectionMode = DataGridViewSelectionMode.FullRowSelect
                    grdPack.CurrentCell = grdPack.Item(1, SelectRow)
                    Exit Do
                End If
                i += 1
            Loop
            If Not flag Then
                MsgBox("No Item Found", MsgBoxStyle.ApplicationModal, Nothing)
                SelectRow = 0
            End If
        End If
    End Sub


    Private Sub btnsearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsearch.Click
        SearchItemPrice()
    End Sub

    Private Function calculateTaxFromUnitPrice(ByVal price As Double, ByVal i As Integer) As Double
        If chkcal.Checked Then
            Dim taxp As Double
            Dim additionaltax As Double
            With grdPack
                taxp = CDbl(.Item(Constvatper, i).Value) + CDbl(.Item(Constcess, i).Value)
                additionaltax = CDbl(.Item(Constadditionalcess, i).Value)
            End With
            price = price - additionaltax
            Return ((price * 100) / (taxp + 100))
        Else
            Return price
        End If
    End Function

    Private Sub btnset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnset.Click
        Try
            lblstatus.Left = btnsearch.Left + btnsearch.Width + 50
            lblstatus.Top = btnsearch.Top
            Dim priceclm As Integer
            If rdoprice.Checked Or rdomrpdisc.Checked Then
                priceclm = ConstUnitPrice
            ElseIf rdowprice.Checked Then
                priceclm = ConstMrp
            End If
            lblstatus.Visible = True
            lblstatus.Text = "Price Percentage Updating.. Please wait!"
            lblstatus.Refresh()
            Cursor = Cursors.WaitCursor
            Dim i As Integer
            Dim price As Double
            If rdomrpdisc.Checked Then
                If chktag.Checked Then
                    For i = 0 To grdPack.RowCount - 1
                        If grdPack.Item(0, i).Value = "Y" Then
                            If Val(grdPack.Item(ConstMrp, i).Value & "") = 0 Then grdPack.Item(ConstMrp, i).Value = Format(0, numFormat)
                            price = CDbl(grdPack.Item(ConstMrp, i).Value) - ((CDbl(grdPack.Item(ConstMrp, i).Value) * CDbl(txtper.Text)) / 100)
                            If Val(grdPack.Item(Constvatper, i).Value & "") = 0 Then grdPack.Item(Constvatper, i).Value = 0
                            price = calculateTaxFromUnitPrice(price, i)
                            dtItem(i)(priceclm) = Format(price, numFormat)
                            dtItem(i)(ConstTaxPrice) = Format(price + ((price * CDbl(grdPack.Item(Constvatper, i).Value)) / 100), numFormat)
                        End If
                    Next
                Else
                    For i = 0 To grdPack.RowCount - 1
                        If Val(grdPack.Item(ConstMrp, i).Value & "") = 0 Then grdPack.Item(ConstMrp, i).Value = Format(0, numFormat)
                        price = CDbl(grdPack.Item(ConstMrp, i).Value) - ((CDbl(grdPack.Item(ConstMrp, i).Value) * CDbl(txtper.Text)) / 100)
                        If Val(grdPack.Item(Constvatper, i).Value & "") = 0 Then grdPack.Item(Constvatper, i).Value = 0
                        price = calculateTaxFromUnitPrice(price, i)
                        dtItem(i)(priceclm) = Format(price, numFormat)
                        dtItem(i)(ConstTaxPrice) = Format(price + ((price * CDbl(grdPack.Item(Constvatper, i).Value)) / 100), numFormat)
                    Next
                End If
            ElseIf rdowprice.Checked Then
                Dim num5 As Integer
                If rdoFromMRP.Checked Then
                    num5 = ConstMrp
                Else
                    num5 = ConstUnitPrice
                End If
                If chktag.Checked Then
                    For i = 0 To grdPack.RowCount - 1
                        If grdPack.Item(0, i).Value = "Y" Then
                            price = CDbl(grdPack.Item(num5, i).Value) - ((CDbl(grdPack.Item(num5, i).Value) * CDbl(txtper.Text)) / 100)
                            dtItem(i)(Constwsprice) = Format(price, numFormat)
                        End If

                    Next
                Else
                    For i = 0 To grdPack.RowCount - 1
                        price = CDbl(grdPack.Item(num5, i).Value) - ((CDbl(grdPack.Item(num5, i).Value) * CDbl(txtper.Text)) / 100)
                        dtItem(i)(Constwsprice) = Format(price, numFormat)
                        dtItem(i)(0) = "Y"
                    Next
                End If

            Else
                If chktag.Checked Then
                    For i = 0 To grdPack.RowCount - 1
                        If grdPack.Item(0, i).Value = "Y" Then
                            price = CDbl(grdPack.Item(ConstCostAvg, i).Value) + ((CDbl(grdPack.Item(ConstCostAvg, i).Value) * CDbl(txtper.Text)) / 100)
                            If Val(grdPack.Item(Constvatper, i).Value & "") = 0 Then grdPack.Item(Constvatper, i).Value = 0
                            If price = 0 And chkcal.Checked Then
                                price = CDbl(grdPack.Item(ConstUnitPrice, i).Value)
                            End If
                            price = calculateTaxFromUnitPrice(price, i)
                            'dtItem(num)(num2) = Format(price, numFormat)
                            dtItem(i)(ConstTaxPrice) = Format(price + ((price * CDbl(grdPack.Item(Constvatper, i).Value)) / 100), numFormat)
                            dtItem(i)(ConstUnitPrice) = Format(price, numFormat)
                        End If
                    Next
                Else
                    For i = 0 To grdPack.RowCount - 1
                        price = CDbl(grdPack.Item(ConstCostAvg, i).Value) + ((CDbl(grdPack.Item(ConstCostAvg, i).Value) * CDbl(txtper.Text)) / 100)
                        If Val(grdPack.Item(Constvatper, i).Value & "") = 0 Then grdPack.Item(Constvatper, i).Value = 0
                        If price = 0 And chkcal.Checked Then
                            price = CDbl(grdPack.Item(ConstUnitPrice, i).Value)
                        End If
                        price = calculateTaxFromUnitPrice(price, i)
                        'dtItem(num)(num2) = Format(price, numFormat)
                        dtItem(i)(ConstTaxPrice) = Format(price + ((price * CDbl(grdPack.Item(Constvatper, i).Value)) / 100), numFormat)
                        dtItem(i)(ConstUnitPrice) = Format(price, numFormat)
                        dtItem(i)(0) = "Y"
                    Next
                End If
            End If
            lblstatus.Text = ""
            Cursor = Cursors.Arrow
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        End Try


    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        loadWaite(2)

    End Sub
    Private Sub updatePrice()
        Try
            lblstatus.Left = btnsearch.Left + btnsearch.Width + 50
            lblstatus.Top = btnsearch.Top
            lblstatus.Visible = True
            lblstatus.Text = "Price Management Updating.. Please wait!"
            lblstatus.Refresh()
            Cursor = Cursors.WaitCursor
            Dim dtUpdate As New DataTable
            Dim _qurey As EnumerableRowCollection(Of DataRow)
            _qurey = From data In dtItem.AsEnumerable() Where data(0) = "Y" Select data
            If _qurey.Count > 0 Then
                dtUpdate = _qurey.CopyToDataTable
            End If
            Dim i As Integer
            If Not dtUpdate Is Nothing Then
                For i = 0 To dtUpdate.Rows.Count - 1
                    setValue(i, dtUpdate)
                    _objItemDetailsBL.updatePricemanagement()
                Next
                _fillDataGrid()
                Me.Cursor = Cursors.Default
                lblstatus.Text = ""
                MsgBox("Item Pricemanagement successfully saved", MsgBoxStyle.Information)
            Else
                Me.Cursor = Cursors.Default
                lblstatus.Text = ""
                MsgBox("Nothing found to save", MsgBoxStyle.Exclamation)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Private Sub setValue(ByVal i As Integer, ByVal dtUpdate As DataTable)
        With _objItemDetailsBL
            .ItemId = Val(dtUpdate(i)(ConstItemid))
            .ItemCode = dtUpdate(i)(ConstItemCode)
            .Descr = dtUpdate(i)(ConstTrDescr)
            .Unit = dtUpdate(i)(ConstDescr)
            If Val(dtUpdate(i)(ConstopQty) & "") = 0 Then dtUpdate(i)(ConstopQty) = 0
            .opnQty = CDbl(dtUpdate(i)(ConstopQty))
            If Val(dtUpdate(i)(Constopcost) & "") = 0 Then dtUpdate(i)(Constopcost) = 0
            .OpnCost = CDbl(dtUpdate(i)(Constopcost))
            If Val(dtUpdate(i)(ConstUnitPrice) & "") = 0 Then dtUpdate(i)(ConstUnitPrice) = 0
            .salesPrice = CDbl(dtUpdate(i)(ConstUnitPrice))
            If Val(dtUpdate(i)(Constwsprice)) = 0 Then dtUpdate(i)(Constwsprice) = 0
            .WSalesPrice = CDbl(dtUpdate(i)(Constwsprice))
            If Val(dtUpdate(i)(ConstMinSalesPrice) & "") = 0 Then dtUpdate(i)(ConstMinSalesPrice) = 0
            .MinSalesPrice = CDbl(dtUpdate(i)(ConstMinSalesPrice))
            If Val(dtUpdate(i)(ConstMrp) & "") = 0 Then dtUpdate(i)(ConstMrp) = 0
            .MRP = CDbl(dtUpdate(i)(ConstMrp))
            .hsncode = Trim(dtUpdate(i)(ConstHSNCode) & "")
            .webname = Trim(dtUpdate(i)(Constwebname) & "")
            .vat = Trim(dtUpdate(i)(Constvatcode) & "")
            .Rack = Trim(dtUpdate(i)(ConstRack) & "")
            If Val(dtUpdate(i)(Constsecondprice) & "") = 0 Then dtUpdate(i)(Constsecondprice) = 0
            .secondPrice = CDbl(dtUpdate(i)(Constsecondprice))
            If Val(dtUpdate(i)(Constadditionalcess) & "") = 0 Then dtUpdate(i)(Constadditionalcess) = 0
            .additionalcess = CDbl(dtUpdate(i)(Constadditionalcess))
            .regularcess = Trim(dtUpdate(i)(Constrgccode) & "")
        End With
    End Sub



    Private Sub grdPack_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPack.CellClick
        Dim intColumnIndex As Integer = 0
        checkOrUncheckTag(grdPack, e, intColumnIndex)
        grdPack.SelectionMode = DataGridViewSelectionMode.CellSelect
        chgbyprg = True
        grdPack.BeginEdit(True)
        chgbyprg = False

    End Sub

    Private Sub grdPack_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPack.CellEndEdit
        Dim columnIndex As Integer = e.ColumnIndex
        If (columnIndex = 5) Then
        End If

    End Sub

    Private Sub grdPack_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPack.CellEnter
        chgbyprg = True
        grdPack.BeginEdit(True)
        chgbyprg = False
    End Sub

    Private Sub grdPack_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPack.CellValidated
        calculateLineTotal(e.RowIndex, e.ColumnIndex)
        Dim val As String = Trim(grdPack.Item(e.ColumnIndex, e.RowIndex).Value & "")
        Dim rownumber As Integer = grdPack.Item("rownumber", e.RowIndex).Value
        dtcmn(rownumber - 1)(e.ColumnIndex) = val
        val = Trim(grdPack.Item("tag", e.RowIndex).Value & "")
        dtcmn(rownumber - 1)("tag") = val
    End Sub
    Private Sub calculateLineTotal(ByVal num As Integer, ByVal clIndex As Integer)
        Try
            Dim price As Double
            Dim tax As Double
            Dim additionalcess As Double
            Dim dt As DataTable
            chgbyprg = True
            Dim disablefc As Boolean
            If cessenddate <= DateValue(Date.Now) Then
                disablefc = True
            End If
            If Not chgamt Then Exit Sub
            If clIndex = ConstUnitPrice Or clIndex = ConstHSNCode Or clIndex = Constvatcode Or clIndex = Constrgccode Or clIndex = Constadditionalcess Or clIndex = Constsecondprice Or clIndex = Constwsprice Then
                With grdPack
                    If Val(.Item(Constadditionalcess, num).Value & "") = 0 Then .Item(Constadditionalcess, num).Value = 0
                    additionalcess = CDbl(.Item(Constadditionalcess, num).Value)
                    If Val(.Item(Constvatper, num).Value & "") = 0 Then .Item(Constvatper, num).Value = 0
                    dt = _objcmnbLayer._fldDatatable("SELECT SUM(isnull(VAT,0)) VAT FROM(Select " & IIf(disablefc, "0", " isnull(vat,0)") & " vat from VatMasterTb where vatcode='" & .Item(Constvatcode, num).Value & "'" & _
                                                     "UNION ALL Select isnull(vat,0)vat from VatMasterTb where vatcode='" & .Item(Constrgccode, num).Value & "') TR")
                    If dt.Rows.Count > 0 Then
                        tax = Val(dt(0)("vat") & "")
                    Else
                        tax = 0
                    End If
                    .Item(Constcess, num).Value = tax
                    tax = tax + CDbl(.Item(Constvatper, num).Value)
                    Select Case clIndex
                        Case ConstUnitPrice
                            price = .Item(ConstUnitPrice, num).Value
                        Case Constsecondprice
                            price = .Item(Constsecondprice, num).Value
                        Case Constwsprice
                            price = .Item(Constwsprice, num).Value
                        Case Else
                            price = .Item(ConstUnitPrice, num).Value
                    End Select
                    price = calculateTaxFromUnitPrice(price, num)
                    Select Case clIndex
                        Case ConstUnitPrice
uprice:
                            .Item(ConstUnitPrice, num).Value = price
                            If Val(.Item(Constvatper, num).Value) = 0 Then
                                .Item(ConstTaxPrice, num).Value = price 'Format(price, numFormat)
                            Else
                                .Item(ConstTaxPrice, num).Value = price + additionalcess + ((price * tax) / 100) 'Format(, numFormat)
                            End If
                        Case Constsecondprice
                            .Item(Constsecondprice, num).Value = price
                        Case Constwsprice
                            .Item(Constwsprice, num).Value = price
                        Case Else
                            GoTo uprice
                    End Select


                    'dtItem(num)("Tax Price")
                End With
            End If
            chgbyprg = False
            chgamt = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub grdPack_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPack.CellValueChanged
        If chgbyprg Then Exit Sub
        If Not chgbyprg And e.ColumnIndex > 1 Then
            grdPack.Item(0, grdPack.CurrentRow.Index).Value = "Y"
        End If
        chgbyprg = True
        If e.RowIndex >= 0 Then
            Select Case e.ColumnIndex
                Case ConstHSNCode
                    Dim dt As DataTable
                    dt = _objcmnbLayer._fldDatatable("select IGST from GSTTb where hsncode='" & dtItem(e.RowIndex)(ConstHSNCode) & "'", False)
                    If dt.Rows.Count > 0 Then

                        grdPack.Item(Constvatper, grdPack.CurrentRow.Index).Value = dt(0)("IGST")

                    Else
                        grdPack.Item(Constvatper, grdPack.CurrentRow.Index).Value = 0
                    End If
                    chgamt = True
                Case ConstUnitPrice, Constsecondprice, Constwsprice, Constadditionalcess, Constrgccode, Constvatcode
                    chgamt = True
            End Select

        End If
        chgbyprg = False
    End Sub

    Private Sub grdPack_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles grdPack.DataError

    End Sub

    Private Sub grdPack_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdPack.Enter
        activecontrolname = "grdPack"

    End Sub

    Private Sub grdPack_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdPack.GotFocus
        activecontrolname = "grdPack"
    End Sub

    Private Sub grdPack_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdPack.KeyDown
        Try
            If (e.KeyCode = Keys.Enter) Then
                If (grdPack.RowCount = 0) Then
                    Return
                End If
                If chkenter.Checked Then
                    If (grdPack.CurrentRow.Index < (grdPack.RowCount - 1)) Then
                        grdPack.CurrentCell = grdPack.Item(grdPack.CurrentCell.ColumnIndex, (grdPack.CurrentCell.RowIndex + 1))
                    End If
                Else
                    FindNextCell(grdPack, grdPack.CurrentCell.RowIndex, (grdPack.CurrentCell.ColumnIndex + 1))
                End If
                chgbyprg = True
                grdPack.BeginEdit(True)
                chgbyprg = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub grdPack_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdPack.Leave
        activecontrolname = ""

    End Sub

    Private Sub txtper_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtper.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            btnset.Focus()
        End If

    End Sub

    Private Sub txtper_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtper.KeyPress
        On Error Resume Next
        numCtrl = sender
        If numCtrl.ReadOnly Then Exit Sub
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
            numCtrl.Text = Format(Val(numCtrl.Text), numFormat)
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
        btnupdate.Enabled = True
        chgbyprg = False
    End Sub

    Private Sub txtSeq_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSeq.KeyDown
        If e.KeyCode = Keys.Return Then
            SearchItemPrice()
        End If
    End Sub

    Private Sub txtSeq_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeq.TextChanged
        'SelectRow = 0
        dtItem = SearchGrid(dtcmn, txtSeq.Text, cmbOrder.SelectedIndex, True)
        grdPack.DataSource = dtItem
        grdPack.Tag = ""
    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            Const WM_KEYDOWN As Integer = &H100
            Const WM_KEYUP As Integer = &H101
            'msg.ToString()
            If msg.Msg = WM_KEYDOWN Or msg.Msg = WM_KEYUP Then
                If activecontrolname = "grdPack" Then
                    If msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up) Or msg.WParam.ToInt32() = CInt(Keys.Enter) Or msg.WParam.ToInt32() = CInt(Keys.F2) Or msg.WParam.ToInt32() = CInt(Keys.F3) Or msg.WParam.ToInt32() = CInt(Keys.F4) Then
                        grdPack_KeyDown(Nothing, New KeyEventArgs(keyData))
                        Return True
                    End If
                End If
            End If
ctn:
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub btnlevel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnlevel.Click
        If grdPack.RowCount = 0 Then Exit Sub
        lblstatus.Left = btnsearch.Left + btnsearch.Width + 50
        lblstatus.Top = btnsearch.Top
        If (Not fGrp Is Nothing) Then
            fGrp.Close()
            fGrp = Nothing
        End If
        fGrp = New SetGroup
        Dim point As New Point(Me.Width / 2, Me.Height - fGrp.Height)
        fGrp.Location = point
        fGrp.ItemId = Val(grdPack.Item("itemid", grdPack.CurrentRow.Index).Value)
        fGrp.ShowDialog()
        fGrp = Nothing

    End Sub

    Private Sub fGrp_updateGroup() Handles fGrp.updateGroup
        Dim i As Integer
        lblstatus.Visible = True
        lblstatus.Text = "Item Level Updating.. Please wait!"
        lblstatus.Refresh()
        Cursor = Cursors.WaitCursor
        Dim found As Boolean
        For i = 0 To grdPack.RowCount - 1
            If grdPack.Item("Tag", i).Value = "Y" Then
                found = True
                saveGroup(Val(grdPack.Item(ConstItemid, i).Value), fGrp.grdLevel)
            End If
        Next
        Cursor = Cursors.Default
        lblstatus.Text = ""
        If found = False Then
            MsgBox("Select item for set level", MsgBoxStyle.Exclamation)
        Else
            MsgBox("Item Level Updated", MsgBoxStyle.Information, Nothing)
        End If
    End Sub
    Private Sub saveGroup(ByVal varitemid As Long, ByVal grd As DataGridView)
        Dim left As String = ""
        If grd.Tag <> "" Then
            Dim num2 As Integer = (grd.Rows.Count - 1)
            Dim i As Integer = 0
            Do While (i <= num2)
                If Val(grd.Item(2, i).Value) > 0 Then
                    left = left & IIf(left = "", "", ",") & "Level" & i + 1 & "=" & grd.Item(2, i).Value
                End If
                i += 1
            Loop
            _objcmnbLayer._saveDatawithOutParm("Update invitm set " & left & ",ischange=1 WHERE ItemId= " & varitemid)
            '_objcmnbLayer._saveDatawithOutParm("Update invitm set ischange=1 WHERE ItemId= " & varitemid)
        End If

    End Sub


    Private Sub rdowprice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdowprice.Click, rdoprice.Click, rdomrpdisc.Click
        plWS.Visible = rdowprice.Checked
    End Sub

    Private Sub btndescription_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndescription.Click
        Dim frm As New AddDescriptionFrm
        frm.txtshort.Tag = Val(grdPack.Item(ConstItemid, grdPack.CurrentRow.Index).Value)
        frm.getDescriptions()
        frm.ShowDialog()
    End Sub

    Private Sub btnonline_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnonline.Click
        ftransfer = New TransferToWebFrm
        ftransfer.typeofTransfer = 1
        ftransfer.dtTable = dtItem
        ftransfer.lblwithimage.Text = IIf(chkupdateimage.Checked, "Updating with Image", "")
        ftransfer.lblwithimage.Tag = IIf(chkupdateimage.Checked, 1, 0)
        ftransfer.Show(fMainForm)
    End Sub

    Private Sub ftransfer_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles ftransfer.FormClosed
        ftransfer = Nothing
    End Sub

    Private Sub grdPack_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPack.CellContentClick

    End Sub

    Private Sub btnimage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnimage.Click
        If grdPack.RowCount = 0 Then Exit Sub
        Dim frm As New ItemImageFrm
        With frm
            .lblitem.Tag = Val(grdPack.Item(ConstItemid, grdPack.CurrentRow.Index).Value)
            .lblitem.Text = grdPack.Item(ConstTrDescr, grdPack.CurrentRow.Index).Value & "[" & grdPack.Item(ConstItemCode, grdPack.CurrentRow.Index).Value & "]"
            .ShowDialog()
        End With
        frm = Nothing
        'Try
        '    Dim ItemId As Integer
        '    If Not fDoc Is Nothing Then fDoc = Nothing
        '    ItemId = Val(grdPack.Item(ConstItemid, grdPack.CurrentRow.Index).Value)
        '    fDoc = New DocumentView
        '    If Val(ItemId) Then
        '        fDoc.KeyId = "ITM-" & Val(ItemId)
        '        fDoc.moduleid = 3
        '        fDoc.isDoc = False
        '        fDoc.itemid = ItemId
        '        fDoc.ldImage()
        '        fDoc.ShowDialog()
        '    Else
        '        fDoc.Close()
        '    End If
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub

    Private Sub fDoc_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fDoc.FormClosed
        fDoc = Nothing
    End Sub

    Private Sub chkcal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkcal.CheckedChanged

    End Sub

    Private Sub btnbarcode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnbarcode.Click
        If grdPack.RowCount = 0 Then Exit Sub
        Dim frm As New BarCodeFrm
        Dim i As Integer
        Dim itemCode As String = ""
        Dim itemname As String = ""
        Dim qty As Integer = 0
        Dim itemid As Long
        Dim price As Double
        Dim dt As DataTable
        If Not grdPack.CurrentRow Is Nothing Then
            i = grdPack.CurrentRow.Index
        End If
        itemid = Val(grdPack.Item(ConstItemid, grdPack.CurrentRow.Index).Value)
        dt = _objcmnbLayer._fldDatatable("SELECT [Item Code],Description," & _
                                                 "(((isnull(IGST,1)+ISNULL(vat,0))*UnitPrice)/100)+UnitPrice [Tax Price],mrp " & _
                                                 " FROM InvItm" & _
                                                 " LEFT JOIN GSTTb ON GSTTb.HSNCode=InvItm.HSNCode " & _
                                                 " LEFT JOIN VatMasterTb ON VatMasterTb.vatid=InvItm.vatid " & _
                                                 "where itemid=" & itemid)
        If dt.Rows.Count > 0 Then
            itemCode = dt(0)("Item Code")
            itemname = dt(0)("Description")
            price = dt(0)("Tax Price")
        End If
        With frm
            .lblbarcode.Text = itemCode
            .lblitem.Text = itemname
            .lblprice.Text = Format(price, numFormat)
            .txtqty.Text = 1
            .ShowDialog()
        End With
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        If grdPack.RowCount = 0 Then
            MsgBox("Items not found", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If grdPack.CurrentRow Is Nothing Then
            MsgBox("Please select atleast one item", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        deleteItem(Val(grdPack.Item(ConstItemid, grdPack.CurrentRow.Index).Value))
    End Sub
    Private Sub deleteItem(ByVal PreitemId As Integer)
        If userType = 0 Or userType = 2 Then
            btndelete.Tag = 1
        Else
            btndelete.Tag = IIf(getRight(3, CurrentUser), 1, 0)
        End If
        If Val(btndelete.Tag) = 0 Then
            MsgBox("This user do not have permission to remove", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Dim dttable As DataTable
        dttable = _objcmnbLayer._fldDatatable("SELECT itemid FROM INVITM WHERE opQty >0 AND itemid =" & Val(PreitemId))
        If dttable.Rows.Count > 0 Then
            MsgBox("Opening QTY Exist !", MsgBoxStyle.Information, "Cannot Delete !")
            Exit Sub
        End If
        dttable.Clear()
        dttable = _objcmnbLayer._fldDatatable("SELECT ITEMID FROM (SELECT ITEMID FROM ITMINVTRTB WHERE itemid =" & Val(PreitemId) & _
                                              " UNION ALL SELECT ITEMID FROM JobitemTb WHERE itemid =" & Val(PreitemId) & _
                                              " UNION ALL SELECT ITEMID FROM DocTranTb WHERE itemid =" & Val(PreitemId) & _
                                              " UNION ALL SELECT ITEMID FROM JobInvTrTb WHERE itemid =" & Val(PreitemId) & ") TR GROUP BY ITEMID")
        If dttable.Rows.Count > 0 Then
            MsgBox("Transaction Exist !", MsgBoxStyle.Information, "Cannot Delete !")
        Else
            If MsgBox("Are You Sure to Remove The Item!", MsgBoxStyle.YesNo + MsgBoxStyle.Information) = MsgBoxResult.No Then Exit Sub
            _objcmnbLayer._saveDatawithOutParm("DELETE FROM INVITM WHERE itemid=" & Val(PreitemId))
            _objcmnbLayer._saveDatawithOutParm("DELETE FROM ItemRawMeterialTb WHERE itemid=" & Val(PreitemId))
            _objcmnbLayer._saveDatawithOutParm("DELETE FROM InvItmPropertiesTb WHERE itemid=" & Val(PreitemId))
            _objcmnbLayer._saveDatawithOutParm("delete from BatchTb where Itemid=" & Val(PreitemId) & " and batchTrid=0")
        End If
        _fillDataGrid()
    End Sub

    Private Sub btntaxtoprice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btntaxtoprice.Click
        Dim i As Integer
        Dim price As Double
        chkcal.Checked = True
        With grdPack
            For i = 0 To .RowCount - 1
                price = Format(.Item(ConstTaxPrice, i).Value, numFormat)
                If Val(.Item(Constvatper, i).Value & "") = 0 Then .Item(Constvatper, i).Value = 0
                price = calculateTaxFromUnitPrice(price, i)
                .Item(ConstUnitPrice, i).Value = price
            Next

        End With
        chkcal.Checked = False
        MsgBox("Done", MsgBoxStyle.Information)
    End Sub

    Private Sub btnloaddecimal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnloaddecimal.Click
        Dim rindex As Integer
        If rdop1.Checked Then
            rindex = ConstUnitPrice
        ElseIf rdop2.Checked Then
            rindex = Constsecondprice
        ElseIf rdop3.Checked Then
            rindex = ConstMrp
        ElseIf rdop4.Checked Then
            rindex = ConstTaxPrice
        End If
        'MsgBox(dtcmn(0)(rindex))
        'If Val(Mid(dtcmn(0)(rindex).ToString(), dtcmn(0)(rindex).ToString.IndexOf(".") + 1)) > 0 Then
        '    MsgBox("done")
        'End If
        dtItem = SearchGrid(dtcmn, "", rindex, , True)
        grdPack.DataSource = dtItem
        grdPack.Tag = ""
    End Sub
    Private Sub loadWaite(ByVal triggerType As Integer)
        If Not fwait Is Nothing Then fwait.Close() : fwait = Nothing
        fwait = New WaitMessageFrm
        With fwait
            .triggerType = triggerType
            .Show()
        End With

    End Sub

    Private Sub fwait_triggerEvent() Handles fwait.triggerEvent
        Select Case fwait.triggerType
            Case 1
                _fillDataGrid()
                SetGridHead()
                'resizeGridColumn(grdPack, 2)
            Case 2
                updatePrice()
        End Select
        If Not fwait Is Nothing Then
            fwait.Close()
            fwait = Nothing
        End If
    End Sub
    Private Sub showlocationwise(ByVal rowindex As Integer)
        If chgbyprg Then Exit Sub
        If grdPack.CurrentRow Is Nothing Then Exit Sub
        If fshowlocationqty Is Nothing Then fshowlocationqty = New ShowLocationQtyFrm
        With fshowlocationqty
            '.MdiParent = fMainForm
            .loadLOCQty(Val(grdPack.Item("Itemid", rowindex).Value))
            .Show()
            .Top = Me.Top + Screen.PrimaryScreen.WorkingArea.Height - .Height - 40
            .Left = Me.Left + Screen.PrimaryScreen.WorkingArea.Width - .Width - 10
        End With
    End Sub

End Class