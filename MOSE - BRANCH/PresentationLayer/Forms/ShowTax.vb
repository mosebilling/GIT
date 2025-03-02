Public Class ShowTax

    Private Sub ShowTax_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If grdVoucher.Columns.Count = 0 Then Exit Sub
        SetGridProperty(grdVoucher)
        With grdVoucher
            'If EnableGST Then

            'Else
            '    .Columns.Item(2).Visible = False
            '    .Columns.Item(3).Visible = False
            '    .Columns.Item(4).Visible = False
            '    .Columns.Item(0).HeaderText = "Code"
            '    .Columns.Item(1).DefaultCellStyle.Format = "N0" & NoOfDecimal
            '    .Columns.Item(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            'End If
            .Columns.Item(0).Visible = False
            .Columns.Item(3).Visible = False
            .Columns.Item(2).DefaultCellStyle.Format = "N0" & NoOfDecimal
            .Columns.Item(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End With

    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Me.Close()
    End Sub
End Class