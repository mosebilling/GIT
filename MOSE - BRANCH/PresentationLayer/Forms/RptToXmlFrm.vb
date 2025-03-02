Imports System.IO
Public Class RptToXmlFrm

    Private Sub RptToXmlFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtpath.Text = Application.StartupPath
        txtpath.Text = Replace(txtpath.Text, "\bin\Debug", "\Reports")
        getFiles()
        lblfilename.Text = ""
    End Sub
    Private Sub getFiles()
        Dim strFileSize As String = ""
        Dim di As New IO.DirectoryInfo(txtpath.Text)
        Dim aryFi As IO.FileInfo() = di.GetFiles("*.rpt")
        Dim fi As IO.FileInfo
        Dim filename As String
        lblfilename.Tag = aryFi.Count
        For Each fi In aryFi
            filename = fi.Name
            chkfiles.Items.Add(filename)
        Next
    End Sub
    Private Sub Convert(ByVal dpath As String, ByVal spath As String, ByVal isxml As Boolean)
        Dim filename As String
        Dim oFileInfo As FileInfo
        Dim i As Integer
        Dim dstPath As String
        Dim sourcePath As String
        For Each itm In chkfiles.CheckedItems
            filename = itm.ToString
            sourcePath = spath & "\" & filename
            filename = Mid(filename, filename.LastIndexOf("\") + 2)
            filename = Mid(filename, 1, filename.IndexOf("."))
            filename = filename & ".xml"
            lblfilename.Refresh()
            lblfilename.Text = filename
            lblfilename.Refresh()
            dstPath = dpath & "\" & filename
            If FileExists(dstPath) Then
                oFileInfo = New FileInfo(dstPath)
                If (oFileInfo.Attributes And FileAttributes.ReadOnly) > 0 Then
                    oFileInfo.Attributes = oFileInfo.Attributes Xor FileAttributes.ReadOnly
                End If
                File.Delete(dstPath)
            End If
            File.Copy(sourcePath, dstPath, True)
            i = i + 1
        Next
        MsgBox("Files " & i & "/" & Val(lblfilename.Tag) & " Converted", MsgBoxStyle.Information)
    End Sub
    Private Sub Convert()
        'MsgBox(aryFi.Count)
        'Exit Sub
        'strFileSize = (Math.Round(fi.Length / 1024)).ToString()
        'Console.WriteLine("File Name: {0}", fi.Name)
        'Console.WriteLine("File Full Name: {0}", fi.FullName)
        'Console.WriteLine("File Size (KB): {0}", strFileSize)
        'Console.WriteLine("File Extension: {0}", fi.Extension)
        'Console.WriteLine("Last Accessed: {0}", fi.LastAccessTime)
        'Console.WriteLine("Read Only: {0}", (fi.Attributes.ReadOnly = True).ToString)
        Dim i As Integer
        Dim strFileSize As String = ""
        Dim di As New IO.DirectoryInfo(txtpath.Text)
        Dim aryFi As IO.FileInfo() = di.GetFiles("*.rpt")
        Dim fi As IO.FileInfo
        Dim filename As String
        Dim dPath As String
        Dim oFileInfo As FileInfo
        lblfilename.Tag = aryFi.Count
        For Each fi In aryFi

            filename = fi.Name
            filename = Mid(filename, filename.LastIndexOf("\") + 2)
            filename = Mid(filename, 1, filename.IndexOf("."))
            filename = filename & ".xml"
            lblfilename.Refresh()
            lblfilename.Text = filename
            lblfilename.Refresh()
            dPath = Application.StartupPath & "\" & filename
            If FileExists(dPath) Then
                oFileInfo = New FileInfo(dPath)
                If (oFileInfo.Attributes And FileAttributes.ReadOnly) > 0 Then
                    oFileInfo.Attributes = oFileInfo.Attributes Xor FileAttributes.ReadOnly
                End If
                File.Delete(dPath)
            End If
            File.Copy(fi.FullName, dPath, True)
            i = i + 1
        Next
        MsgBox("Files " & i & "/" & Val(lblfilename.Tag) & " Converted", MsgBoxStyle.Information)
    End Sub

    Private Sub ConvertToRpt()
        
        Dim i As Integer
        Dim strFileSize As String = ""
        Dim di As New IO.DirectoryInfo(txtpath.Text)
        Dim aryFi As IO.FileInfo() = di.GetFiles("*.xml")
        Dim fi As IO.FileInfo
        Dim filename As String
        Dim dPath As String
        Dim oFileInfo As FileInfo
        lblfilename.Tag = aryFi.Count
        For Each fi In aryFi

            filename = fi.Name
            filename = Mid(filename, filename.LastIndexOf("\") + 2)
            filename = Mid(filename, 1, filename.IndexOf("."))
            filename = filename & ".rpt"
            lblfilename.Refresh()
            lblfilename.Text = filename
            lblfilename.Refresh()
            dPath = txtpath.Text & "\Reports\" & filename
            If FileExists(dPath) Then
                oFileInfo = New FileInfo(dPath)
                If (oFileInfo.Attributes And FileAttributes.ReadOnly) > 0 Then
                    oFileInfo.Attributes = oFileInfo.Attributes Xor FileAttributes.ReadOnly
                End If
                File.Delete(dPath)
            End If
            File.Copy(fi.FullName, dPath, True)
            i = i + 1
        Next
        MsgBox("Files " & i & "/" & Val(lblfilename.Tag) & " Converted", MsgBoxStyle.Information)
    End Sub
    Private Sub btnbrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnbrowse.Click
        opfSelectFile.Title = "Please Select a File"
        opfSelectFile.InitialDirectory = "c:"
        opfSelectFile.ShowDialog()
        getFiles()
    End Sub

    Private Sub opfSelectFile_FileOk(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles opfSelectFile.FileOk
        txtpath.Text = opfSelectFile.FileName
    End Sub

    Private Sub btnexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexit.Click
        Me.Close()
    End Sub

    Private Sub btnconvert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnconvert.Click
        If Directory.Exists(txtpath.Text) Then
            If chkxmltorpt.Checked Then
                ConvertToRpt()
            Else
                Convert(Application.StartupPath, txtpath.Text, True)
            End If
        Else
            MsgBox("Path Does not Exist", MsgBoxStyle.Exclamation)
        End If
        Me.Close()
    End Sub

    Private Sub chkeselectall_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkeselectall.Click
        Dim i As Integer
        If chkeselectall.Checked Then
            For i = 0 To chkfiles.Items.Count - 1
                chkfiles.SetItemChecked(i, True)
            Next
        Else
            For i = 0 To chkfiles.Items.Count - 1
                chkfiles.SetItemChecked(i, False)
            Next
        End If
    End Sub
End Class