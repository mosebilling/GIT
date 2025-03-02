
Public Class JobDelivery
#Region "Class Objects"
    Private _objJob As clsJob
    Private _objcmnbLayer As clsCommon_BL
#End Region


    Public Sub ldRec(ByVal jbid As Long, ByVal jbDescription As String)
        _objJob = New clsJob
        lbljob.Text = jbDescription
        lbljob.Tag = jbid
        _objJob.Jobid = jbid
        _objJob.DateFrom = DateValue(Now)
        _objJob.DateTo = DateValue(Now)
        _objJob.Tp = 11
        Dim dt As DataTable
        dt = _objJob.returnJob.Tables(0)
        grdImei.DataSource = dt
        setGridHead()
        _objJob = Nothing
    End Sub

    Private Sub setGridHead()
        SetGridProperty(grdImei)
        With grdImei
            .Columns.Item((grdImei.ColumnCount - 1)).Visible = False
            .Columns.Item("jobimeiId").Visible = False
            .Columns.Item("Tag").Width = 50
            .Columns.Item("Tag").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns.Item("Delivered").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        End With
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        _objcmnbLayer = New clsCommon_BL
        Dim num2 As Integer = (grdImei.RowCount - 1)
        Dim i As Integer = 0
        Do While (i <= num2)
            If grdImei.Item("Tag", i).Value = "Y" And grdImei.Item("Edt", i).Value = "Y" Then
                _objcmnbLayer._saveDatawithOutParm("UPDATE JobImeiListTb SET Delivered=1,DeliveredDt='" & Format(DateValue(dtpdate.Value), "yyyy/MM/dd") & "',DeliveredTo='" & txtreceivedby.Text & "' where jobimeiId=" & Val(grdImei.Item("jobimeiId", i).Value))
            ElseIf grdImei.Item("Tag", i).Value <> "Y" And grdImei.Item("Edt", i).Value = "Y" Then
                _objcmnbLayer._saveDatawithOutParm("UPDATE JobImeiListTb SET Delivered=0,DeliveredTo='' where jobimeiId=" & Val(grdImei.Item("jobimeiId", i).Value))
            End If
            i += 1
        Loop
        MsgBox("Record Updated", MsgBoxStyle.Information, Nothing)
        ldRec(Val(lbljob.Tag), lbljob.Text)
        txtreceivedby.Text = ""
    End Sub

    Private Sub grdImei_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdImei.CellClick
        If e.ColumnIndex = 1 Then
            grdImei.Item("Tag", grdImei.CurrentRow.Index).Value = IIf(grdImei.Item("Tag", grdImei.CurrentRow.Index).Value = "Y", "", "Y")
            grdImei.Item("Edt", grdImei.CurrentRow.Index).Value = "Y"
        End If
    End Sub

End Class