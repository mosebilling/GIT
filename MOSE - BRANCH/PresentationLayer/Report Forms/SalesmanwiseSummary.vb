
Public Class SalesmanwiseSummaryfrm
    Private _objReport As New clsReport_BL
    Private dtTable As DataTable
    Private RptdtTable As DataTable
#Region "Form Objects"
    Private WithEvents fRptFormat As RptFormatfrm
    Private WithEvents frm As New ReprtviewNEWfrm
#End Region
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub SalesmanwiseSummaryfrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AddtoCombo(cmbsalesman, "SELECT SManCode FROM SalesmanTb", True, False)

    End Sub
    Private Sub ldrec()
        Dim tp As Integer
        If rdosummary.Checked Then
            If cmbsalesman.Text = "" Then
                tp = 3
            Else
                tp = 1
            End If
        Else
            If cmbsalesman.Text = "" Then
                tp = 2
            Else
                tp = 0
            End If
        End If
        dtTable = _objReport.returnsalesmanwisesummary(cmbsalesman.Text, DateValue(cldrStartDate.Value), DateValue(cldrEnddate.Value), tp).Tables(0)
        grdvoucher.DataSource = Nothing
        If chkonlycash.Checked Then
            Dim _qurey As EnumerableRowCollection(Of DataRow)
            _qurey = From data In dtTable.AsEnumerable() Where UCase(Trim(data("GrpSetOn") & "")) = "CASH" Select data
            If _qurey.Count > 0 Then
                dtTable = _qurey.CopyToDataTable()
            Else
                dtTable = dtTable.Clone
            End If
        End If
        grdvoucher.DataSource = dtTable
        setGridHead()
        gridTotal(dtTable)
    End Sub
    Private Sub setGridHead()
        With grdvoucher
            SetGridProperty(grdvoucher)
            .Columns("datefrom").Visible = False
            .Columns("dateto").Visible = False
            '
            .Columns("lnk").Visible = False
            .Columns("Sales").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Sales").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Received").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Received").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Expense").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Expense").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("GrpSetOn").HeaderText = "Mode"
            .Columns("sramt").HeaderText = "SR Amt"
            .Columns("jvdate").HeaderText = "Date"
            If cmbsalesman.Text <> "" Then
                .Columns("SlsManId").Visible = False
            Else
                .Columns("SlsManId").HeaderText = "Sales Man"
            End If
            If rdosummary.Checked Then
                .Columns("category").Visible = False
                .Columns("TRTYPE").HeaderText = "Description"
                resizeGridColumn(grdvoucher, 0)
            Else
                .Columns("trdescr").HeaderText = "Description"
                .Columns("TRTYPE").HeaderText = "Type"
                .Columns("prefix").HeaderText = "Prefix"
                .Columns("prefix").Width = 50
                .Columns("JVNum").Width = 75
                .Columns("accid").Visible = False
                .Columns("category").Visible = False
                .Columns("TRCODE").Visible = False
                .Columns("trByAcc").Visible = False
                resizeGridColumn(grdvoucher, 7)
            End If
            Dim fc As String
            Dim i As Integer
            For i = 0 To .Columns.Count - 1
                fc = Mid(.Columns(i).HeaderText, 1, 1)
                .Columns(i).HeaderText = UCase(fc) & Mid(.Columns(i).HeaderText, 2)
            Next
        End With
        setComboGrid()
    End Sub
    Private Sub setComboGrid()
        cmbcolms.Items.Clear()
        Dim i As Integer = 0
        For i = 0 To grdvoucher.ColumnCount - 1
            cmbcolms.Items.Add(grdvoucher.Columns(i).HeaderText)
        Next
        If cmbcolms.Items.Count > 0 Then cmbcolms.SelectedIndex = 0
    End Sub

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        ldrec()
    End Sub
    Private Sub gridTotal(ByVal RptdtTable As DataTable)
        If RptdtTable.Rows.Count = 0 Then Exit Sub
        Dim drSum As Double
        Dim crSum As Double
        Dim amt As String
        Dim salestotal As Double
        Dim salesreturn As Double
        amt = Trim(RptdtTable.Compute("SUM(Received)", "") & "")
        If Val(amt) > 0 Then
            drSum = Convert.ToDouble(amt)
        End If
        amt = Trim(RptdtTable.Compute("SUM(Expense)", "") & "")
        If Val(amt) > 0 Then
            crSum = Convert.ToDouble(amt)
        End If
        amt = Trim(RptdtTable.Compute("SUM(Sales)", "") & "")
        If Val(amt) > 0 Then
            salestotal = Convert.ToDouble(amt)
        End If
        amt = Trim(RptdtTable.Compute("SUM(SRAmt)", "") & "")
        If Val(amt) > 0 Then
            salesreturn = Convert.ToDouble(amt)
        End If
        lbltotalsr.Text = Format(salesreturn, numFormat)
        salesreturn = 0
        amt = Trim(RptdtTable.Compute("SUM(SRAmt)", "GrpSetOn='cash'") & "")
        If Val(amt) > 0 Then
            salesreturn = Convert.ToDouble(amt)
        End If
        lblTlDebit.Text = Format(drSum, numFormat)
        lblcredit.Text = Format(crSum, numFormat)
        lblsales.Text = Format(salestotal, numFormat)
        lblsr.Text = Format(salesreturn, numFormat)
        lbldiff.Text = Format(CDbl(lblTlDebit.Text) - CDbl(lblcredit.Text) - CDbl(lblsr.Text), numFormat)
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        grdvoucher.DataSource = SearchGrid(dtTable, Trim(txtSearch.Text), cmbcolms.SelectedIndex)
        RptdtTable = SearchGrid(dtTable, Trim(txtSearch.Text), cmbcolms.SelectedIndex)
        gridTotal(RptdtTable)
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        If chkFormat.Checked Then
            fRptFormat = New RptFormatfrm
            fRptFormat.RptType = IIf(rdosummary.Checked, "SMRS", "SMRSD")
            fRptFormat.ShowDialog()
            fRptFormat = Nothing
        Else
            Dim RptName As String
            RptName = getRptDefFlName(IIf(rdosummary.Checked, "SMRS", "SMRSD"), "Salesman Route Report")
            If RptName <> "" Then
                PrepareReport(RptName, "Salesman Route Report", False)
            End If
        End If
    End Sub
    Public Sub PrepareReport(ByVal FileName As String, ByVal RptCaption As String, ByVal forpint As Boolean)
        If Not frm Is Nothing Then
            frm.Close()
            frm = New ReprtviewNEWfrm
        Else
            frm = New ReprtviewNEWfrm
        End If
        Dim ds As New DataSet
        Dim dt As DataTable
        If RptdtTable Is Nothing Then
            Dim tp As Integer
            If rdosummary.Checked Then
                If cmbsalesman.Text = "" Then
                    tp = 3
                Else
                    tp = 1
                End If
            Else
                If cmbsalesman.Text = "" Then
                    tp = 2
                Else
                    tp = 0
                End If
            End If
            dt = _objReport.returnsalesmanwisesummary(cmbsalesman.Text, DateValue(cldrStartDate.Value), DateValue(cldrEnddate.Value), tp).Tables(0)
            RptdtTable = dt.Copy
            If chkonlycash.Checked Then
                Dim _qurey As EnumerableRowCollection(Of DataRow)
                _qurey = From data In RptdtTable.AsEnumerable() Where UCase(data("GrpSetOn")) = "CASH" Select data
                If _qurey.Count > 0 Then
                    RptdtTable = _qurey.CopyToDataTable()
                Else
                    RptdtTable = dtTable.Clone
                End If
            End If
        End If
        ds.Tables.Add(RptdtTable)
        RptdtTable = Nothing
        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = Me.MdiParent
        frm.Text = "Salesman Route Summary Report " & RptCaption
        frm.Show()
    End Sub

    Private Sub fRptFormat_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fRptFormat.FormClosed
        fRptFormat = Nothing
    End Sub

    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        PrepareReport(RptFlName, RptCaption, forPrint)
    End Sub
End Class