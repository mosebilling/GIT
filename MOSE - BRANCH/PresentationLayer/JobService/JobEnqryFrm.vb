Public Class JobEnqryFrm
    Private _objJob As clsJob
    Private _vtable As DataTable
    Public Event sendJobid(ByVal jobid As Long)
    Private Sub JobEnqryFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ldJobdetails()
    End Sub
    Private Sub ldJobdetails()

        _objJob = New clsJob
        With _objJob
            .Jobid = 0
            .DateFrom = DateValue(Date.Now)
            .DateTo = DateValue(Date.Now)
            .custcode = 0
            .Tp = 25
            _vtable = .returnJob.Tables(0)
        End With
        grdItem.DataSource = Nothing
        grdItem.DataSource = _vtable
        SetGrid()

    End Sub
    Private Sub SetGrid()
        With grdItem
            If .ColumnCount = 0 Then Exit Sub
            SetGridProperty(grdItem)

            .Columns("Jobcode").HeaderText = "Job Code"
            .Columns("jobdate").HeaderText = "Job Date"
            .Columns("Jobname").HeaderText = "Job Name"
            .Columns("JobDescription").HeaderText = "Job Description"
            .Columns("AccDescr").HeaderText = "Customer Name"
            .Columns("ContactName").HeaderText = "Contact Name"
            .Columns("EstimatedDate").HeaderText = "Estimated Date"
            .Columns("EstimatedAmt").HeaderText = "Estimated Amount"
            .Columns("JobValue").HeaderText = "Job Value"
            .Columns("Userid").HeaderText = "Created By"
            .Columns("CrdtDate").HeaderText = "Created Date"
            .Columns("EstimatedAmt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("EstimatedAmt").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("JobValue").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("JobValue").DefaultCellStyle.Format = "N" & NoOfDecimal
            '.Columns("Datefrom").Visible = False
            '.Columns("Dateto").Visible = False
            '.Columns("lnk").Visible = False
            .Columns("jobid").Visible = False
            .Columns("ServiceCost").Visible = False
            .Columns("ItemCost").Visible = False
            .Columns("JobCloseDate").Visible = False
            .Columns("JobDescription").Width = 200
            .Columns("AccDescr").Width = 200
            .Columns("ContactName").Width = 200
            .Columns("EstimatedDate").Width = 150
            .Columns("EstimatedAmt").Width = 150
            .Columns("CrdtDate").Width = 150
            .Columns("Jobname").Frozen = True

            cmbOrder.Items.Clear()
            Dim i As Integer = 0
            For i = 0 To grdItem.ColumnCount - 2
                cmbOrder.Items.Add(grdItem.Columns(i).HeaderText)
            Next
            If cmbOrder.Items.Count > 0 Then cmbOrder.SelectedIndex = 4
        End With
    End Sub

    Private Sub txtSeq_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeq.TextChanged
        grdItem.DataSource = SearchGrid(_vtable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
    End Sub

    Private Sub grdItem_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdItem.DoubleClick
        RaiseEvent sendJobid(Val(grdItem.Item("jobid", grdItem.CurrentRow.Index).Value))
        Me.Close()
    End Sub
End Class