Public Class UAETaxReport
    Private dtData As DataTable
#Region "Form Objects"
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fRptFormat As RptFormatfrm
    Private WithEvents fwait As WaitMessageFrm
#End Region
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub getreturnValue()
        Dim _objinvoice As New clsInvoice
        'dtpfrom.Value = DateFrom
        dtData = _objinvoice.returnUAEVatReport(DateValue(dtpfrom.Value), DateValue(dtpto.Value))
        calculatesum()
        grdvoucher.DataSource = dtData
        SetGridHead()

    End Sub

    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        loadWaite(1)
    End Sub

    Private Sub UAETaxReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        getreturnValue()
        Timer1.Enabled = True
    End Sub
    Private Sub SetGridHead()
        'Dim i As Integer
        With grdvoucher
            SetGridProperty(grdvoucher)

            .RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("ARIAL", 9.0!, FontStyle.Bold)
            .Columns("Descriptontext").HeaderText = "Description"
            .Columns("Descriptontext").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("taxablevalue").HeaderText = "Amount (AED)"
            .Columns("taxablevalue").Width = 150
            .Columns("taxablevalue").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("taxablevalue").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("taxablevalue").SortMode = DataGridViewColumnSortMode.NotSortable

            .Columns("taxvalue").HeaderText = "Vat Amount (AED)"
            .Columns("taxvalue").Width = 150
            .Columns("taxvalue").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("taxvalue").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("taxvalue").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("tp").Visible = False
            .Columns("lnk").Visible = False
            .Columns("datefrom").Visible = False
            .Columns("dateto").Visible = False

            .Rows.Item(0).DefaultCellStyle.BackColor = Color.SteelBlue
            .Rows.Item(6).DefaultCellStyle.BackColor = Color.Brown
            .Rows.Item(0).DefaultCellStyle.ForeColor = Color.White
            .Rows.Item(6).DefaultCellStyle.ForeColor = Color.White
            .Rows.Item(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Rows.Item(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            '.Item("taxablevalue", 0).Value = ""
            '.Item("taxvalue", 0).Value = ""
        End With
        resizeGridColumn(grdvoucher, 0)
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        SetGridHead()
        resizeGridColumn(grdvoucher, 0)
    End Sub
    Private Sub calculatesum()
        Dim i As Integer
        Dim ttlTaxable As Double
        Dim ttlTax As Double
        For i = 1 To dtData.Rows.Count - 1
            If dtData(i)("tp") = 1 Then
                ttlTaxable = ttlTaxable + CDbl(dtData(i)("taxablevalue"))
                ttlTax = ttlTax + CDbl(dtData(i)("taxvalue"))
            Else
                Exit For
            End If
        Next
        dtData(0)("taxablevalue") = ttlTaxable
        dtData(0)("taxvalue") = ttlTax
        lbltaxable.Text = Format(ttlTax, numFormat)
        ttlTaxable = 0
        ttlTax = 0
        For i = 7 To 8
            ttlTaxable = ttlTaxable + CDbl(dtData(i)("taxablevalue"))
            ttlTax = ttlTax + CDbl(dtData(i)("taxvalue"))
        Next
        dtData(6)("taxablevalue") = ttlTaxable
        dtData(6)("taxvalue") = ttlTax
        lblrecoverable.Text = Format(ttlTax, numFormat)
        lblnetvat.Text = Format(CDbl(lbltaxable.Text) - CDbl(lblrecoverable.Text), numFormat)
        'grdvoucher.Item("taxablevalue", 6).Value = ttlTaxable
        'grdvoucher.Item("taxvalue", 6).Value = ttlTax
    End Sub

    Private Sub btnpreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpreview.Click
        Dim RptType As String = ""
        RptType = "VAT"
        fRptFormat = New RptFormatfrm
        fRptFormat.RptType = RptType
        fRptFormat.ShowDialog()
        fRptFormat = Nothing
    End Sub

    Private Sub frm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles frm.FormClosed
        frm = Nothing
    End Sub

    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        PrepareReport(RptFlName, RptCaption, forPrint)
    End Sub
    Public Sub PrepareReport(ByVal FileName As String, ByVal RptCaption As String, ByVal forpint As Boolean)
        Dim ds As New DataSet
        If Not frm Is Nothing Then
            frm.Close()
            frm = New ReprtviewNEWfrm
        Else
            frm = New ReprtviewNEWfrm
        End If
        Dim dt As New DataTable
        Dim _qurey As EnumerableRowCollection(Of DataRow)
        _qurey = From data In dtData.AsEnumerable() Where data("lnk") = 1 Select data
        If _qurey.Count > 0 Then
            dt = _qurey.CopyToDataTable
        End If
        ds.Tables.Add(dt)
        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = Me.MdiParent
        frm.Text = "Tax Report " & RptCaption
        frm.Show()
    End Sub
    Private Sub fwait_triggerEvent() Handles fwait.triggerEvent
        Select Case fwait.triggerType
            Case 1
                getreturnValue()
        End Select
        If Not fwait Is Nothing Then
            fwait.Close()
            fwait = Nothing
        End If

    End Sub
    Private Sub loadWaite(ByVal triggerType As Integer)
        If Not fwait Is Nothing Then fwait.Close() : fwait = Nothing
        fwait = New WaitMessageFrm
        With fwait
            .triggerType = triggerType
            .Show()
        End With
    End Sub
End Class