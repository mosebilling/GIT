Public Class MaterialConusumptionComparisonFrm
    Private _objrest As clsrestaurent
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fRptFormat As RptFormatfrm
    Private Sub MaterialConusumptionComparisonFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loadGrid()
        Timer1.Enabled = True
    End Sub
    Private Sub loadGrid()
        Try
            Dim dt As DataTable
            _objrest = New clsrestaurent
            dt = _objrest.returnProductionMaterialConsumption(DateValue(cldrStartDate.Value))
            grdvoucher.DataSource = dt
            With grdvoucher
                SetGridProperty(grdvoucher)
                .Columns("lnk").Visible = False
                .Columns("Unit").Width = 50
                .Columns("Opening").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("Opening").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("Out Qty").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("Out Qty").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("Consumption").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("Consumption").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("Balance").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("Balance").DefaultCellStyle.Format = "N" & NoOfDecimal
                resizeGridColumn(grdvoucher, 1)
            End With
            _objrest = Nothing
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        loadGrid()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        resizeGridColumn(grdvoucher, 1)
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        fRptFormat = New RptFormatfrm
        fRptFormat.RptType = "MCC"
        fRptFormat.ShowDialog()
        fRptFormat = Nothing
    End Sub

    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        PrepareReport(RptFlName, RptCaption, forPrint)
    End Sub
    Public Sub PrepareReport(ByVal FileName As String, ByVal RptCaption As String, ByVal forpint As Boolean)
        If Not frm Is Nothing Then
            frm.Close()
            frm = New ReprtviewNEWfrm
        Else
            frm = New ReprtviewNEWfrm
        End If
        Dim ds As New DataSet
        _objrest = New clsrestaurent
        ds = _objrest.returnProductionMaterialConsumptionDS(DateValue(cldrStartDate.Value))
        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = fMainForm
        frm.Text = RptCaption
        frm.Show()
    End Sub
End Class