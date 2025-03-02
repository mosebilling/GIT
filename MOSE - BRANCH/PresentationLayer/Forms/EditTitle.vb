Public Class EditTitle
    Private _objcmnbLayer As New clsCommon_BL
   
    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("select MAccId from MAccHd where Descr='" & txtname.Text & "'")
        If dt.Rows.Count > 0 Then
            If Val(txtname.Tag) <> Val(dt(0)("MAccId") & "") Then
                MsgBox("Duplicate Found", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
        End If
        _objcmnbLayer._saveDatawithOutParm("UPDATE MAccHd SET Descr='" & txtname.Text & "' WHERE MAccId=" & Val(txtname.Tag))
        Me.Close()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT S1AccId FROM S1AccHd  WHERE MAccId=" & Val(txtname.Tag))
        If dt.Rows.Count > 0 Then
            MsgBox("Group Found under this Title! you cannot remove the title", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If MsgBox("Do you want to remove the title " & txtname.Text, MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.No Then Exit Sub
        _objcmnbLayer._saveDatawithOutParm("DELETE from MAccHd WHERE MAccId=" & Val(txtname.Tag))
        Me.Close()
    End Sub
End Class