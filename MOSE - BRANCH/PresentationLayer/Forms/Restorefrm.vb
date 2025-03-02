Imports System.IO
Imports System.Xml
Public Class Restorefrm
    Dim _objcmnbLayer As New clsCommon_BL
    Private Sub btnexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexit.Click
        Me.Close()
    End Sub

    Private Sub cmdRestore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRestore.Click
        Try
            If txtFirleFrom.Text = "" Then MsgBox("The selected path is invalid", MsgBoxStyle.Critical)
            Dim con As String
            con = readXml()
            con = Replace(con, "database=" & MyDatabase, "database=master")
            Dim sqlconnection As New System.Data.SqlClient.SqlConnection(con)
            Dim str As String
            str = "ALTER DATABASE " & MyDatabase & " SET SINGLE_USER With ROLLBACK IMMEDIATE  restore database " & MyDatabase & " from disk='" & txtFirleFrom.Text & "' with replace " & _
                    "ALTER DATABASE " & MyDatabase & " SET Multi_User"
            sqlconnection.Open()
            Dim cmd As New System.Data.SqlClient.SqlCommand(str, sqlconnection)
            Me.Cursor = Cursors.WaitCursor
            cmd.ExecuteNonQuery()
            Me.Cursor = Cursors.Default
            MsgBox("Database restore is successfully completed, It will close the application", MsgBoxStyle.Information)
            End
        Catch ex As Exception
            MsgBox(ex.Message)
            _objcmnbLayer._saveDatawithOutParm("ALTER DATABASE " & MyDatabase & " SET MULTI_USER With ROLLBACK IMMEDIATE")
        End Try
    End Sub
    

    Private Sub cmdFileFrom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFileFrom.Click
        Try
            Dim dlgOpenFile As New OpenFileDialog()
            If dlgOpenFile.ShowDialog() = Windows.Forms.DialogResult.OK Then
                With dlgOpenFile
                    txtFirleFrom.Text = Mid(.FileName, InStrRev(.FileName, "\") + 1)
                End With
                txtFirleFrom.Text = dlgOpenFile.FileName
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
End Class