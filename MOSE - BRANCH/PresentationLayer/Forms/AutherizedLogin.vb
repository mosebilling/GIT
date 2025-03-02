
Public Class AutherizedLogin

    Private _objCmnblayer As New clsCommon_BL
    Public usrType As Integer
    Public isremove As Boolean
    Public Event CloseLogin(ByVal usrType As Integer, ByVal usrId As Integer)
    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        subUsername()
    End Sub
    Sub subUsername()
        Try
            Dim dtUser As DataTable
            If txtUsername.Text = "" Then
                MsgBox("Invalid User name", MsgBoxStyle.Critical)
                txtUsername.Focus()
                Exit Sub
            End If
            dtUser = _objCmnblayer._fldDatatable("SELECT * FROM UserTb WHERE UserId='" & txtUsername.Text & "'")
            If dtUser.Rows.Count > 0 Then
                If Me.ActiveControl.Name = "txtUsername" Then
                    txtUsername.Tag = dtUser(0)("id")
                    txtUsername.Text = UCase(dtUser(0)("username"))
                    txtpassword.Focus()
                    Exit Sub
                Else
                    If UCase(txtpassword.Text) <> "VINVISGRP" Then 'PROGRAMMAR PASSWORD
                        If UCase(txtpassword.Text) <> UCase(dtUser(0)("Password")) Then
                            MsgBox("Invalid Password", MsgBoxStyle.Critical)
                            txtpassword.Focus()
                            Exit Sub
                        End If
                    End If
                End If
                If UCase(txtpassword.Text) <> "VINVISGRP" Then
                    usrType = IIf(dtUser(0)("MasterYN") = True, 1, 0)
                Else
                    usrType = 2
                End If
            Else
                If UCase(txtpassword.Text) <> "VINVISGRP" Then 'PROGRAMMAR PASSWORD
                    MsgBox("User does not exist", MsgBoxStyle.Critical)
                    txtUsername.Focus()
                    Exit Sub
                Else
                    usrType = 2
                End If
            End If
            RaiseEvent CloseLogin(usrType, Val(txtUsername.Tag))
            Me.Close()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try

    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        btncancel.Tag = 1
        RaiseEvent CloseLogin(usrType, Val(txtUsername.Tag))
        Me.Close()
    End Sub

    Private Sub txtUsername_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtUsername.KeyDown, txtpassword.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub
End Class