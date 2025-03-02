
Public Class AssignJobItemsFrm
#Region "Class Objects"
    Private _objJob As clsJob
    Private _objcmnbLayer As New clsCommon_BL
#End Region

    Public Sub ldPendigJobItems(ByVal jobid As Long)
        _objJob = New clsJob
        Dim _vtable As DataTable
        With _objJob
            .Jobid = jobid
            txtjobcode.Tag = jobid
            .DateFrom = DateValue(Date.Now)
            .DateTo = DateValue(Date.Now)
            .custcode = 0
            .Tp = 24
            _vtable = .returnJob.Tables(0)
        End With
        If _vtable.Rows.Count > 0 Then
            txtjobcode.Text = _vtable(0)("Jobcode")
            txtcustomer.Text = _vtable(0)("AccDescr")
            dtpdate.Value = Format(DateValue(_vtable(0)("jobdate")), DtFormat)
        End If
        grdItem.DataSource = Nothing
        grdItem.DataSource = _vtable
        SetGrid()
    End Sub
    Private Sub setGrid()
        With grdItem
            If .ColumnCount = 0 Then Exit Sub
            SetGridProperty(grdItem)

            .Columns("Jobcode").Visible = False
            .Columns("Jobname").Visible = False
            .Columns("AccDescr").Visible = False
            .Columns("jobdate").Visible = False
            .Columns("jbDescription").HeaderText = "Job Description"
            .Columns("jbDescription").Width = 200
            .Columns("itmDescription").HeaderText = "Item Description"
            .Columns("itmDescription").Width = 200
            .Columns("Qty").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Qty").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("qty").Width = 50
            .Columns("Tag").Width = 50
            .Columns("Tag").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("EstimatedDate").Width = 100
            .Columns("jbitmId").Visible = False
            .Columns("EstimatedDate").HeaderText = "Est. Dt."

            'setComboGrid()
        End With
    End Sub

    Private Sub btnassign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnassign.Click
        Dim i As Integer
        For i = 0 To grdItem.Rows.Count - 1
            With grdItem
                If grdItem.Item(0, i).Value = "Y" Then
                    _objcmnbLayer._saveDatawithOutParm("UPDATE JobItemtb SET attendedBy='" & CurrentUser & "'," & _
                                                       " attendDate='" & Format(DateValue(Date.Now), "yyyy/MM/dd") & "' WHERE jbitmId=" & Val(grdItem.Item("jbitmId", i).Value))
                End If
            End With
        Next
        ldPendigJobItems(Val(txtjobcode.Tag))
    End Sub

    Private Sub grdItem_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItem.CellDoubleClick
        If e.ColumnIndex = 0 Then
            grdItem.Item(e.ColumnIndex, e.RowIndex).Value = IIf(grdItem.Item(e.ColumnIndex, e.RowIndex).Value = "Y", "", "Y")
        End If
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub chkselectall_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkselectall.CheckedChanged

    End Sub

    Private Sub chkselectall_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkselectall.Click
        Dim i As Integer
        For i = 0 To grdItem.Rows.Count - 1
            grdItem.Item(0, i).Value = IIf(chkselectall.Checked, "Y", "")
        Next
    End Sub

    Private Sub AssignJobItemsFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class