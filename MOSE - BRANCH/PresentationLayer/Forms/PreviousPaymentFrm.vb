
Public Class PreviousPaymentFrm
    Private _objTr As New clsAccountTransaction
    Dim _vdatatable As DataTable
    Public AccountNo As Long
    Public accountname As String
    Public jvtype As String
    Dim RptdtTable As DataTable
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fRptFormat As RptFormatfrm
    Private reportType As Integer
    Public reference As String
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Public Sub ldGrid(ByVal ptype As Integer)
        reportType = ptype
        With _objTr
            .ptype = ptype
            .DateFrom = DateValue(dtpstart.Value)
            .DateTo = DateValue(dtpto.Value)
            .AccountNo = AccountNo
            .JVType = jvtype
            .Reference = reference
            _vdatatable = .returnPaymentDetails.Tables(0)
        End With
        dvData.DataSource = _vdatatable
        SetmodiGrid()
        setComboGrid()
        calculate()
    End Sub
    Private Sub setComboGrid()
        Dim i As Integer = 0
        cmbSearch.Items.Clear()
        For i = 0 To dvData.ColumnCount - 1 'IIf(dvData.ColumnCount - 1 >= cmbShowIndex, cmbShowIndex, dvData.ColumnCount - 1)
            cmbSearch.Items.Add(dvData.Columns(i).HeaderText)
        Next
        cmbSearch.SelectedIndex = 0
    End Sub

    Sub SetmodiGrid()
        With dvData
            SetGridProperty(dvData)

            SetGridProperty(dvData)
            .Columns("Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Amount").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Description").Width = 350
            .Columns("DateFrom").Visible = False
            .Columns("DateTo").Visible = False
            .Columns("Lnk").Visible = False
            .Columns("AccountNo").Visible = False
            .Columns("AccDescr").Visible = False
            .Columns("Linkno").Visible = False
            Dim i As Integer
            For i = 0 To .ColumnCount - 1
                Dim htfs As String = Mid(.Columns(i).HeaderText, 1, 1)
                Dim htls As String = Mid(.Columns(i).HeaderText, 2)
                .Columns(i).HeaderText = UCase(htfs) & htls
            Next
        End With
    End Sub

    Private Sub btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnload.Click
        ldGrid(reportType)
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        dvData.DataSource = SearchGrid(_vdatatable, Trim(txtSearch.Text), cmbSearch.SelectedIndex, Not chkSearch.Checked)
        RptdtTable = SearchGrid(_vdatatable, Trim(txtSearch.Text), cmbSearch.SelectedIndex, Not chkSearch.Checked)
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If chkFormat.Checked Then
            fRptFormat = New RptFormatfrm
            fRptFormat.RptType = "PPC"
            fRptFormat.ShowDialog()
            fRptFormat = Nothing
        Else
            Dim RptName As String
            RptName = getRptDefFlName("PPC", "Previous Payment History")
            If RptName <> "" Then
                LoadReport(RptName, "Previous Payment History", False)
            End If
        End If
    End Sub

    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
         LoadReport(RptFlName, RptCaption, forPrint)
    End Sub
    Private Sub PrepareRpt(ByVal RptType As String, Optional ByVal Forprint As Boolean = False, Optional ByVal isF As Boolean = False)
        Dim RptName As String
        Dim RptCaption As String = ""
        RptName = getRptDefFlName(RptType, RptCaption)
        If Trim(RptName) <> "" Then
            LoadReport(RptName, RptCaption, Forprint)
        End If
    End Sub
    Private Sub LoadReport(ByVal FileName As String, ByVal RptCaption As String, Optional ByVal ToPrint As Boolean = False)
        If Not frm Is Nothing Then
            frm.Close()
            frm = New ReprtviewNEWfrm
        Else
            frm = New ReprtviewNEWfrm
        End If
        Dim ds As New DataSet
        If RptdtTable Is Nothing Then
            With _objTr
                .ptype = reportType
                .DateFrom = DateValue(dtpstart.Value)
                .DateTo = DateValue(dtpto.Value)
                .AccountNo = AccountNo
                ds = .returnPaymentDetails
            End With
        Else
            ds.Tables.Add(RptdtTable)
        End If
        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = fMainForm
        frm.Text = RptCaption
        frm.TopMost = True
        Me.Hide()
        frm.Show()
    End Sub


    Private Sub dvData_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dvData.DoubleClick
        If dvData.RowCount = 0 Then Exit Sub
        If jvtype = "RV" Then
            fMainForm.LoadRV(dvData.Item("Linkno", dvData.CurrentRow.Index).Value, accountname)
        ElseIf jvtype = "PV" Then
            fMainForm.LoadPV(dvData.Item("Linkno", dvData.CurrentRow.Index).Value, accountname)
        End If
        Me.Close()
    End Sub

    Private Sub PreviousPaymentFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If jvtype = "RV" Then
            Label1.Text = "Rceipt History"
        End If
    End Sub
    Private Sub calculate()
        Dim amount As Double
        Dim ttl As Double
        Dim i As Integer
        For i = 0 To dvData.Rows.Count - 1
            amount = CDbl(dvData.Item("Amount", i).Value)
            ttl = ttl + amount
        Next
        lbltotal.Text = Format(ttl, numFormat)
    End Sub
End Class