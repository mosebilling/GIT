Public Class MilkCustomerPriceFrm
    Public dt As DataTable
    Public pricegroup As Integer
    Public customername As String
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Public Sub loaditems()
        grdother.DataSource = dt
        SetGridProperty(grdother)
        With grdother
            .Columns(0).HeaderText = "Item Code"
            .Columns(0).Width = 70
            .Columns(0).ReadOnly = True

            .Columns(1).HeaderText = "Price"
            .Columns(1).Width = 100
            .Columns(1).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(1).ReadOnly = True
        End With
        resizeGridColumn(grdother, 0)
        resizeGridColumn(grdother, 1)
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        loaditems()

    End Sub

    Private Sub MilkCustomerPriceFrm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Timer1.Enabled = True
    End Sub
End Class