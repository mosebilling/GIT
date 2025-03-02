Public Class SalesPurchaseReturnItemsFrm
    Private _objcmnbLayer As New clsCommon_BL
    Private SelectPos As Integer
    Public Event transfer(ByVal dt As DataTable)
    Private dt As DataTable
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Close()
    End Sub

    Public Sub ldItems(ByVal trid As Long)
        Dim strprocessed As String
        strprocessed = "left join (select impDocSlno,SlsPurchRetId,sum(trqty)srqty FROM ItmInvTrTb " & _
                        "left join ItmInvCmnTb on ItmInvCmnTb.trid=ItmInvTrTb.trid where trtype='SR' Group by impDocSlno,SlsPurchRetId)sr " & _
                        "on ItmInvTrTb.id=sr.impDocSlno "
        Dim str As String
        str = "Select * from (SELECT '' Tag, [Item Code],IDescription,SerialNo,WarrentyExpDate,ItmInvTrTb.Unit," & _
                                         "TrQty/pfraction TrQty,isnull(srqty,0)SRQTY, taxamt/FcRate taxamt,UnitCost,((UnitCost*TrQty)+taxamt)/FcRate Total,id,SlNo FROM ItmInvTrTb " & _
                                         "left join ItmInvCmnTb on ItmInvCmnTb.trid=ItmInvTrTb.trid " & _
                                         "LEFT JOIN InvItm ON InvItm.ItemId = ItmInvTrTb.ItemId " & strprocessed & _
                                         " WHERE ItmInvTrTb.TrId = " & trid & " and ItmInvTrTb.itemid>0)tr where TrQty-SRQTY>0 ORDER BY SlNo"
        dt = _objcmnbLayer._fldDatatable(str)
        grdVoucher.DataSource = dt
        SetGridHead()
    End Sub
    Private Sub SetGridHead()
        SetGridProperty(grdVoucher)
        With grdVoucher
            .Columns("Tag").Width = 50
            .Columns("Tag").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Item Code").Width = 100
            .Columns("IDescription").Width = 570
            .Columns("Unit").Width = 50
            .Columns("TrQty").Width = 100
            .Columns("TrQty").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("TrQty").DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns("SRQTY").Width = 100
            .Columns("SRQTY").HeaderText = "Returned"
            .Columns("SRQTY").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("SRQTY").DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns("taxamt").Width = 100
            .Columns("taxamt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("taxamt").Visible = False

            .Columns("WarrentyExpDate").HeaderText = "Exp Date"
            .Columns("WarrentyExpDate").Width = 75
            .Columns("WarrentyExpDate").Visible = enableSerialnumber
            .Columns("SerialNo").Visible = enableSerialnumber

            .Columns("UnitCost").Width = 100
            .Columns("UnitCost").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("UnitCost").Visible = False
            .Columns("Total").Width = 100
            .Columns("Total").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Total").Visible = False
            .Columns("id").Visible = False
            .Columns("slno").Visible = False
        End With
        resizeGridColumn(grdVoucher, 2)
        setComboGrid(grdVoucher)
    End Sub
    Private Sub setComboGrid(ByVal grd As DataGridView)
        cmbcolms.Items.Clear()
        Dim i As Integer = 0
        With grd
            For i = 0 To grd.ColumnCount - 1
                cmbcolms.Items.Add(.Columns(i).HeaderText)
            Next
            If cmbcolms.Items.Count > 0 Then cmbcolms.SelectedIndex = 1
        End With
    End Sub

    Private Sub getRowForSpecifiedText()
        If Trim(txtSearch.Text) = "" Then Exit Sub
        Dim TAG1 As New Boolean
        Dim str As String
        Dim colIndex As Integer
        colIndex = cmbcolms.SelectedIndex
        With grdVoucher
            For i = SelectPos + 1 To .RowCount - 1
                If Not IsDBNull(.Item(colIndex, i).Value) Then
                    str = UCase(.Item(colIndex, i).Value)
                    If str.Contains(UCase(txtSearch.Text)) Then
                        .ClearSelection()
                        SelectPos = i
                        .FirstDisplayedScrollingRowIndex = SelectPos
                        .CurrentCell = .Item(colIndex, i)
                        .Rows(i).Selected = True
                        TAG1 = True
                        Exit For
                    End If
                End If
            Next
        End With
        If TAG1 = False Then
            MsgBox("  Finished   ", MsgBoxStyle.Information, "Validation")
            SelectPos = 0
        End If
    End Sub

    Private Sub btnsearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsearch.Click
        getRowForSpecifiedText()
    End Sub

    Private Sub grdVoucher_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellClick
        With grdVoucher
            If e.ColumnIndex = 0 Then
                .Item(0, .CurrentRow.Index).Value = IIf(.Item(0, .CurrentRow.Index).Value = "", "Y", "")
            End If
        End With
    End Sub

    Private Sub btnselect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnselect.Click
        Dim _qurey As EnumerableRowCollection(Of DataRow)
        Dim _vDtable As New DataTable
        _qurey = From data In dt.AsEnumerable() Where data(0) = "Y" Select data
        If _qurey.Count > 0 Then
            _vDtable = _qurey.CopyToDataTable
        End If
        RaiseEvent transfer(_vDtable)
        Me.Close()
    End Sub

    Private Sub SalesPurchaseReturnItemsFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub grdVoucher_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellContentClick

    End Sub

    Private Sub chkselectall_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkselectall.CheckedChanged
        With grdVoucher
            For i = 0 To .Rows.Count - 1
                .Item(0, i).Value = IIf(chkselectall.Checked, "Y", "")
            Next
        End With
    End Sub
End Class