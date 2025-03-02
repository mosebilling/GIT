Imports System.IO
Imports System.Net

Public Class Backupfrm
    Dim _objcmnbLayer As New clsCommon_BL
    Private Sub cmdbackup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdbackup.Click
        Dim pth As String
        pth = Backup(txtFileto.Text)
        If pth = "" Then txtFileto.Focus() : Exit Sub
        MsgBox("Data backup successfully completed to " & pth, MsgBoxStyle.Information)
        _objcmnbLayer._saveDatawithOutParm("update CompanyTb set LastBkdDt=getdate()")
        Me.Close()
    End Sub

    Private Sub cmdfileto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdfileto.Click
        FolderBrowserDialog1.ShowDialog()
        If FolderBrowserDialog1.SelectedPath <> "" Then
            txtFileto.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub
    Function Backup(ByVal Filepath As String) As String
        cmdbackup.Cursor = Cursors.WaitCursor
        Dim Filename As String
        If txtFileto.Text = "" Then
            MsgBox("Select Valid Path", MsgBoxStyle.Exclamation)
            cmdbackup.Cursor = Cursors.Arrow
            Return ""
        End If
        ' = DPath & "\Backups"
        If Directory.Exists(Filepath) = False Then
            Directory.CreateDirectory(Filepath)
        End If
        '
        If CurrentUser = "PROGRAMMAR" Then
            Filename = Filepath & "\data.bak"
        Else
            Filename = Filepath & "\MoseDataFile-" & MyDatabase & "-" & Format(DateValue(Date.Now), "dd-MMM-yyyy") & "[" & Format(TimeValue(Date.Now), "h-mm-ss tt") & "]" & ".bak"
        End If
        'Dim cmd As New SqlCommand("backup database " & database & " to disk='" & Filename & "'", cmn.Connection)
        _objcmnbLayer._saveDatawithOutParm("backup database " & MyDatabase & " to disk='" & Filename & "'")
        cmdbackup.Cursor = Cursors.Arrow
        Return Filename
    End Function

    Private Sub btnexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexit.Click
        Me.Close()
    End Sub

    Private Sub Backupfrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim bpath As String
        If CurrentUser = "PROGRAMMAR" Then
            bpath = Application.StartupPath
        Else
            If DPath = "" Then
                bpath = Application.StartupPath & "\BACKUP"
            Else
                bpath = DPath
                If Mid(bpath, Len(bpath)) <> "\" Then
                    bpath = bpath & "\"
                End If
                bpath = bpath & "BACKUP"
            End If

        End If
        
        txtFileto.Text = bpath
    End Sub
End Class