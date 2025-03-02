Public Class DaycloseReportFrm
    Private _objcmnbLayer As New clsCommon_BL
    Private _objReport As New clsReport_BL
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fRptFormat As RptFormatfrm
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnFP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFP.Click
        cldrStartDate.Value = Format(DateFrom, DtFormat)
    End Sub

    Private Sub btnTo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTo.Click
        cldrStartDate.Value = Format(DateTo, DtFormat)
    End Sub
    '09273175999

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        loadDayClose()
    End Sub
    Private Sub loadDayClose()
        Dim dt As DataTable
        dt = _objReport.returnDayCloseReport(DateValue(cldrStartDate.Value), DateValue(dtpto.Value), IIf(chkremoveob.Checked, 2, 0)).Tables(0)
        If chkremoveob.Checked Then
            If dt.Rows.Count = 0 Then Exit Sub
            Dim _qurey As EnumerableRowCollection(Of DataRow)
            _qurey = From data In dt.AsEnumerable() Where data("trtype") <> "ob" Select data
            If _qurey.Count > 0 Then
                dt = _qurey.CopyToDataTable()
            Else
                dt = dt.Clone
            End If
        End If
        Dim i As Integer
        For i = 0 To dt.Rows.Count - 1
            dt(i)("accdescr") = UCase(dt(i)("accdescr"))
        Next
        grdvoucher.DataSource = dt
        setGridHead()
        Timer1.Enabled = True
    End Sub
    Private Sub setGridHead()
        SetGridProperty(grdvoucher)
        With grdvoucher
            .RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("ARIAL", 9.0!, FontStyle.Bold)

            .Columns("Ctry").HeaderText = "Type"
            .Columns("Ctry").Width = 150
            .Columns("Ctry").Visible = False

            .Columns("AccDescr").HeaderText = "Description"
            .Columns("AccDescr").Width = 100
            .Columns("AccDescr").ReadOnly = True
            .Columns("AccDescr").SortMode = DataGridViewColumnSortMode.NotSortable

            .Columns("Debit").HeaderText = "Debit"
            .Columns("Debit").Width = 100
            .Columns("Debit").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Debit").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Debit").ReadOnly = True
            .Columns("Debit").SortMode = DataGridViewColumnSortMode.NotSortable


            .Columns("Credit").HeaderText = "Credit"
            .Columns("Credit").Width = 100
            .Columns("Credit").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Credit").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Credit").ReadOnly = True
            .Columns("Credit").SortMode = DataGridViewColumnSortMode.NotSortable


            .Columns("datefrom").Visible = False
            .Columns("dateto").Visible = False
            .Columns("lnk").Visible = False
            .Columns("Ctryh").Visible = False
            .Columns("tp").Visible = False
            .Columns("trtype").Visible = False
        End With
    End Sub

    Private Sub DaycloseReportFrm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        loadDayClose()
        'Timer1.Enabled = True
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        Dim i As Integer
        With grdvoucher
            For i = 0 To .RowCount - 1
                Dim a As String = .Item("Ctryh", i).Value
                If .Item("Ctryh", i).Value = "h" Then
                    '.Rows.Item(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    '.Columns("AccDescr").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Rows.Item(i).DefaultCellStyle.BackColor = Color.LightGray
                    .Rows.Item(i).DefaultCellStyle.ForeColor = Color.Black
                    If .Item("Ctryh", i).Value = "h" And (.Item("tp", i).Value = 2 Or .Item("tp", i).Value = 3) Then
                        .Rows.Item(i).DefaultCellStyle.BackColor = Color.SkyBlue
                        .Rows.Item(i).DefaultCellStyle.ForeColor = Color.White
                    End If
                    'If Trim(.Item(1, i).Value) = "OPENING" Then
                    '    .Rows.Item(i).DefaultCellStyle.BackColor = Color.SteelBlue
                    '    '.Rows.Item(i).DefaultCellStyle.ForeColor = Color.White
                    'ElseIf Trim(.Item(1, i).Value) = "CLOSING" Then
                    '    .Rows.Item(i).DefaultCellStyle.BackColor = Color.Green
                    '    '.Rows.Item(i).DefaultCellStyle.ForeColor = Color.White
                    'ElseIf .Item("Ctryh", i).Value = "h" And (.Item("tp", i).Value = 2 Or .Item("tp", i).Value = 3) Then
                    '    .Rows.Item(i).DefaultCellStyle.BackColor = Color.Orange
                    '    '.Rows.Item(i).DefaultCellStyle.ForeColor = Color.Black
                    'Else
                    '    .Rows.Item(i).DefaultCellStyle.BackColor = Color.Gray
                    '    '.Rows.Item(i).DefaultCellStyle.ForeColor = Color.Black
                    'End If
                    '.Rows.Item(i).DefaultCellStyle.ForeColor = Color.White
                End If
            Next
            .Columns("Debit").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        End With
       
        resizeGridColumn(grdvoucher, 1)
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Dim RptType As String = ""
        Dim RptCaption As String = ""
        RptType = "DCR"
        If chkFormat.Checked Then
            fRptFormat = New RptFormatfrm
            fRptFormat.RptType = RptType
            fRptFormat.ShowDialog()
            fRptFormat = Nothing
        Else
            Dim RptName As String
            RptName = getRptDefFlName(RptType, RptCaption)
            If RptName <> "" Then
                PrepareReport(RptName, "Day Closing Report", False)
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
        'If Not _rptTable Is Nothing Then
        '    ds.Tables.Add(_rptTable)
        'Else
        '    Dim dtRpt = dtTable.Copy
        '    ds.Tables.Add(dtRpt)
        'End If
        ds = _objReport.returnDayCloseReport(DateValue(cldrStartDate.Value), DateValue(dtpto.Value), IIf(chkremoveob.Checked, 3, 1))
        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = fMainForm
        frm.Text = RptCaption
        frm.Show()
    End Sub

    Private Sub fRptFormat_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fRptFormat.FormClosed
        fRptFormat = Nothing
    End Sub

    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        PrepareReport(RptFlName, "Day Closing Report", False)
    End Sub
End Class