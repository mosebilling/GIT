
Public Class ChangeSecurityFrm
    Dim blayer As New clsCommon_BL
    Public Event CloseChanceSecurityFrm()
    Private Sub cmdchange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdchange.Click
        If MsgBox("You are going to change your Password! are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        saveUser()
        Me.Dispose()
    End Sub
    Sub saveUser()
        If txtNewPass.Text <> txtretype.Text Then MsgBox("Password confirmation was failed", MsgBoxStyle.Exclamation) : Exit Sub
        Try
            blayer._saveDatawithOutParm("UPDATE UserTb SET Password='" & MkDbSrchStr(txtNewPass.Text) & "' WHERE UserId='" & CurrentUser & "'")
            MsgBox("Your password has been changed successfully", MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub txtNewPass_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNewPass.KeyDown, txtretype.KeyDown
        If e.KeyCode = Keys.Return Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub txtretype_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtretype.KeyDown
        If e.KeyCode = Keys.Return Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub
End Class