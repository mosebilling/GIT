Public Class SelectCardPackageFrm
    Private _objcmn As clsCommon_BL
    Private cardnum As String
    Private _objcw As New clsCarWash
    Public Event selectPackage(ByVal packageid As Integer, ByVal packagename As String, ByVal remainingServices As Integer)
    Private Sub SelectCardPackageFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
    Public Sub loadcardPackage(ByVal cardnumber As String)
        Dim ds As DataSet
        ds = _objcw.returnCardPackages(cardnumber)
        cardnum = cardnumber
        grdpackage.DataSource = ds.Tables(0)
        grdfreepackage.DataSource = ds.Tables(1)
        SetGridProperty(grdfreepackage)
        SetGridProperty(grdpackage)
        setGrid()
    End Sub
    Private Sub loadAllPackage()
        Dim dt As DataTable
        _objcmn = New clsCommon_BL
        dt = _objcmn._fldDatatable("Select cardtypename,0 RemainingServices,Amount,cardtypeid from CardtypemasterTb")
        grdpackage.DataSource = dt
        grdfreepackage.DataSource = Nothing
        SetGridProperty(grdpackage)
        setGrid()
    End Sub
    Private Sub setGrid()
        If grdfreepackage.ColumnCount > 0 Then
            With grdfreepackage
                .Columns("cardtypename").HeaderText = "Package"
                .Columns("cardtypename").Width = 40

                .Columns("RemainingServices").HeaderText = "Remaining Services"
                .Columns("RemainingServices").Width = 150
                .Columns("RemainingServices").DefaultCellStyle.Format = "N0"
                .Columns("RemainingServices").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                .Columns("Amount").HeaderText = "Amount"
                .Columns("Amount").Width = 100
                .Columns("Amount").DefaultCellStyle.Format = "N0"
                .Columns("Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                .Columns("cardtypeid").Visible = False
            End With
            resizeGridColumn(grdfreepackage, 0)
        End If
        If grdpackage.ColumnCount > 0 Then
            With grdpackage
                .Columns("cardtypename").HeaderText = "Package"
                .Columns("cardtypename").Width = 40

                .Columns("RemainingServices").HeaderText = "Remaining Services"
                .Columns("RemainingServices").Width = 150
                .Columns("RemainingServices").DefaultCellStyle.Format = "N0"
                .Columns("RemainingServices").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                .Columns("Amount").HeaderText = "Amount"
                .Columns("Amount").Width = 100
                .Columns("Amount").DefaultCellStyle.Format = "N0"
                .Columns("Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("cardtypeid").Visible = False
            End With
            resizeGridColumn(grdpackage, 0)
        End If
        


    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub grdpackage_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdpackage.DoubleClick
        If grdpackage.RowCount = 0 Then Exit Sub
        With grdpackage
            RaiseEvent selectPackage(Val(.Item("cardtypeid", .CurrentRow.Index).Value), .Item("cardtypename", .CurrentRow.Index).Value, Val(.Item("RemainingServices", .CurrentRow.Index).Value))
        End With

    End Sub

    Private Sub grdfreepackage_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdfreepackage.DoubleClick
        If grdfreepackage.RowCount = 0 Then Exit Sub
        With grdfreepackage
            RaiseEvent selectPackage(Val(.Item("cardtypeid", .CurrentRow.Index).Value), .Item("cardtypename", .CurrentRow.Index).Value, Val(.Item("RemainingServices", .CurrentRow.Index).Value))
        End With
    End Sub


    Private Sub rdocardpackage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdocardpackage.Click, rdoallpackage.Click
        If rdoallpackage.Checked Then
            loadAllPackage()
        Else
            loadcardPackage(cardnum)
        End If
    End Sub
End Class