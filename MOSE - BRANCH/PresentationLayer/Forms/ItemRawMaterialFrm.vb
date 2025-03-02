

Public Class ItemRawMaterialFrm
    Private Const RawItemCode = 0
    Private Const RawTrDescr = 1
    Private Const RawUnit = 2
    Private Const RawQtyInHand = 3
    Private Const RawQtyUsed = 4
    Private Const RawUnitcost = 5
    Private Const RawPffraction = 6
    Private Const RawItemid = 7
    Private Const RawMinSlno = 8
    Private Const RawMid = 9
    Private chgbyprg As Boolean
    Public dtRawmeaterial As DataTable
    Private Sub ItemRawMaterialFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
    Public Sub viewItems()
        grdmanufacturing.DataSource = dtRawmeaterial
        setRawGrid()
    End Sub

    Private Sub setRawGrid()
        chgbyprg = True
        SetGridProperty(grdmanufacturing)
        With grdmanufacturing
            .Columns(RawItemCode).HeaderText = "Item Code"
            .Columns(RawItemCode).Width = 100

            .Columns(RawTrDescr).HeaderText = "Description"
            .Columns(RawTrDescr).Width = 200
            .Columns(RawTrDescr).DefaultCellStyle.BackColor = Color.LightSteelBlue

            .Columns(RawUnit).HeaderText = "Unit"
            .Columns(RawUnit).Width = 50
            .Columns(RawUnit).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(RawUnit).ReadOnly = True

            .Columns(RawQtyInHand).HeaderText = "QIH"
            .Columns(RawQtyInHand).Width = 50
            .Columns(RawQtyInHand).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(RawQtyInHand).DefaultCellStyle.BackColor = Color.LightYellow
            .Columns(RawQtyInHand).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(RawQtyUsed).HeaderText = "QIH Used"
            .Columns(RawQtyUsed).Width = 100
            .Columns(RawQtyUsed).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(RawQtyUsed).DefaultCellStyle.BackColor = Color.LightYellow
            .Columns(RawQtyUsed).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(RawUnitcost).HeaderText = "Cost Avg"
            .Columns(RawUnitcost).Width = 100
            .Columns(RawUnitcost).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(RawUnitcost).DefaultCellStyle.BackColor = Color.LightYellow
            .Columns(RawUnitcost).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(RawMinSlno).Visible = False
            .Columns(RawItemid).Visible = False
            .Columns(RawPffraction).Visible = False
            .Columns(RawMid).Visible = False
        End With
        chgbyprg = False
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
End Class