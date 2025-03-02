Public Class ShowAlert
    Public dtShowAlert As DataTable
    Public AlertType As Integer
    Private _objcmnbLayer As New clsCommon_BL
    Private dtRptTable As DataTable
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fRptFormat As RptFormatfrm
    Private Sub btnothcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnothcancel.Click
        Me.Close()
    End Sub
    Private Sub showAlert()
        dtShowAlert = _objcmnbLayer.returnShowAlert(AlertType, DateValue(dtpfrom.Value), DateValue(dtpto.Value), 1)
        grdVoucher.DataSource = dtShowAlert
        setGridHead()
    End Sub
    Public Sub setGridHead()
        With grdVoucher
            SetGridProperty(grdVoucher)
            Select Case AlertType
                Case 0
                    .Columns("SupWarrentyDt").HeaderText = "Expiry Date"
                    .Columns("SupWarrentyDt").Width = 90
                    .Columns("Pur Date").Width = 75
                    .Columns("Pur No").Width = 75
                    .Columns("Validity").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns("Validity").Width = 75
                    .Columns("Supp. Name").Width = 150
                    .Columns("SerialNo").Width = 75
                    .Columns("lnk").Visible = False
                    .Columns("dtfrom").Visible = False
                    .Columns("dtto").Visible = False
                    .Columns("alertType").Visible = False
                    .Columns("isdatewise").Visible = False
                    resizeGridColumn(grdVoucher, 2)
                Case 1, 2
                    .Columns("DealAmt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns("DealAmt").DefaultCellStyle.Format = "N" & NoOfDecimal
                    .Columns("DealAmt").HeaderText = "Amount"
                    .Columns("DealAmt").Width = 100
                    .Columns("ChqNo").Width = 75
                    .Columns("ChqDate").Width = 75
                    .Columns("BankCode").HeaderText = "Bank"
                    .Columns("BankCode").Width = 50
                    .Columns("JVDate").Width = 75
                    .Columns("JVType").Width = 50
                    .Columns("Inv No").Width = 75
                    .Columns("Validity").Width = 75
                    resizeGridColumn(grdVoucher, 4)
                Case 3, 4, 5, 6
                    .Columns("empcode").HeaderText = "EMP Code"
                    .Columns("empcode").Width = 100
                    .Columns("empname").HeaderText = "EMP Name"
                    .Columns("empname").Width = 150
                    .Columns("Validity").Width = 75
                    resizeGridColumn(grdVoucher, 1)
            End Select
        End With
        setComboGrid()
    End Sub
    Private Sub setComboGrid()
        Dim i As Integer = 0
        For i = 0 To grdVoucher.Columns.Count - 1
            cmbcolms.Items.Add(grdVoucher.Columns(i).HeaderText)
        Next
        If cmbcolms.Items.Count > 0 Then cmbcolms.SelectedIndex = 0
    End Sub

    Private Sub ShowAlert_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblcap.Text = ""
        Timer1.Enabled = True
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        lblcap.Text = Me.Text
        resizeGridColumn(grdVoucher, 2)
    End Sub

    Private Sub btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnload.Click
        showAlert()
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        dtRptTable = SearchGrid(dtShowAlert, Trim(txtSearch.Text), cmbcolms.SelectedIndex)
        grdVoucher.DataSource = dtRptTable
    End Sub

    Private Sub btnpreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpreview.Click
        Dim RptType As String = ""
        Select Case AlertType
            Case 0
                RptType = "ALS"
        End Select
        fRptFormat = New RptFormatfrm
        fRptFormat.RptType = RptType
        fRptFormat.ShowDialog()
        fRptFormat = Nothing
    End Sub

    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        If Not frm Is Nothing Then
            frm.Close()
            frm = New ReprtviewNEWfrm
        Else
            frm = New ReprtviewNEWfrm
        End If
        Dim ds As New DataSet
        Dim dt As DataTable
        If dtRptTable Is Nothing Then
            dt = dtShowAlert
            dtRptTable = dt.Copy
        End If
        ds.Tables.Add(dtRptTable)
        dtRptTable = Nothing
        frm.SetReport(ds, RptFlName, 0, False)
        frm.MdiParent = Me.MdiParent
        frm.Text = "Alert Report " & RptCaption
        frm.Show()
    End Sub


    Private Sub chkdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkdate.Click
        pldate.Enabled = chkdate.Checked
    End Sub

    Private Sub grdVoucher_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellContentClick

    End Sub

    Private Sub chkdontshow_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkdontshow.CheckedChanged
        If chkdontshow.Checked Then
            _objcmnbLayer._saveDatawithOutParm("update UserTb set lastalertdate='" & Format(DateValue(Date.Now), "yyyy/MM/dd") & "' where UserId='" & CurrentUser & "'")
        Else
            _objcmnbLayer._saveDatawithOutParm("update UserTb set lastalertdate=null where UserId='" & CurrentUser & "'")
        End If
    End Sub
End Class