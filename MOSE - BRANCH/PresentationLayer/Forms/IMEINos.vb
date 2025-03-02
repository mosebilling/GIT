Public Class IMEINos
#Region "Class objects"
    Private _objcmnbLayer As New clsCommon_BL
    Private _objReport As clsReport_BL
#End Region
#Region "Private variables"
    Private dtImeinos As DataTable
    Private dtReport As DataTable
#End Region
#Region "Public variables"
    Public SearchType As Integer
#End Region
#Region "Form Objects"
    Private WithEvents fRptFormat As RptFormatfrm
    Private WithEvents frm As New ReprtviewNEWfrm
#End Region
    Private Sub IMEINos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loadImeinos()
    End Sub
    Private Sub loadImeinos()
        _objReport = New clsReport_BL
        With _objReport
            If SearchType = 0 Then
                lblName.Text = "IMEI NO [SUPPLIER WARRENTY EXPIRY LIST]"
                dtImeinos = .returnIMEINos(DateValue(cldrStartDate.Value), DateValue(dtpto.Value), 0).Tables(0)
            ElseIf SearchType = 1 Then
                lblName.Text = "IMEI NO [CUSTOMER WARRENTY EXPIRY LIST]"
                dtImeinos = .returnIMEINos(DateValue(cldrStartDate.Value), DateValue(dtpto.Value), 1).Tables(0)
            ElseIf SearchType = 2 Then
                lblName.Text = "IMEI NO [WARRENTY CANCELLED LIST]"
                dtImeinos = .returnIMEINos(DateValue(cldrStartDate.Value), DateValue(dtpto.Value), 2).Tables(0)
            ElseIf SearchType = 3 Then
                lblName.Text = "IMEI NO [WARRENTY LIST]"
                cldrStartDate.Enabled = False
                dtpto.Enabled = False
                dtImeinos = .returnIMEINos(DateValue(cldrStartDate.Value), DateValue(dtpto.Value), 3).Tables(0)
            End If
        End With
        grdvoucher.DataSource = dtImeinos
        SetGridHead()
    End Sub
    Private Sub SetGridHead()
        SetGridProperty(grdvoucher)
        With grdvoucher
            .Columns("SerialNo").HeaderText = "Serial No"
            .Columns("Description").HeaderText = "Item Name"
            .Columns("SupName").HeaderText = "Supplier Name"
            .Columns("PurDate").HeaderText = "Purcahse Date"
            .Columns("CustomeName").HeaderText = "Customer Name"
            .Columns("SaleDate").HeaderText = "Sales Date"
            .Columns("CustExDate").HeaderText = "Customer Expiry Date"
            .Columns("SupExDt").HeaderText = "Supplier Expiry Date"
            .Columns("WarrentyName").HeaderText = "Warrenty Name"
            .Columns("WarrentyStatus").HeaderText = "Warrenty Status"

            .Columns("SerialNo").Width = 150
            .Columns("Description").Width = 250
            .Columns("SupName").Width = 200
            .Columns("PurDate").Width = 100
            .Columns("CustomeName").Width = 200
            .Columns("SaleDate").Width = 100


            .Columns("Datefrom").Visible = False
            .Columns("Dateto").Visible = False
            .Columns("lnk").Visible = False
            .Columns("trid").Visible = False
        End With
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        loadImeinos()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Dim RptType As String = ""
        Select Case SearchType
            Case 0
                RptType = "IME1" 'supplier warrenty expired list
            Case 1
                RptType = "IME2" 'Customer warrenty expired list
            Case 2
                RptType = "IME3" 'warrenty cancelled list
            Case 3
                RptType = "IME4" 'warrenty list
        End Select
        fRptFormat = New RptFormatfrm
        fRptFormat.RptType = RptType
        fRptFormat.ShowDialog()
        fRptFormat = Nothing
    End Sub

    Private Sub fRptFormat_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fRptFormat.FormClosed
        fRptFormat = Nothing
    End Sub
    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        PrepareReport(RptFlName, "", forPrint)
    End Sub
    Public Sub PrepareReport(ByVal FileName As String, ByVal RptCaption As String, ByVal forpint As Boolean)
        If Not frm Is Nothing Then
            frm.Close()
            frm = New ReprtviewNEWfrm
        Else
            frm = New ReprtviewNEWfrm
        End If
        Dim ds As New DataSet
        If dtReport Is Nothing Then
            With _objReport
                ds = .returnIMEINos(DateValue(cldrStartDate.Value), DateValue(dtpto.Value), SearchType)
            End With
        Else
            ds.Tables.Add(dtReport)
        End If
        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = Me.MdiParent
        frm.Text = RptCaption
        frm.Show()
    End Sub

    Private Sub frm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles frm.FormClosed
        frm = Nothing
    End Sub
End Class