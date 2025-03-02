Imports System.Net
Imports System.IO

Public Class ItemImageFrm

    Private Sub btnexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexit.Click
        Me.Close()
    End Sub

    Private Sub btnbrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnbrowse.Click
        With DlgOpen
            .Filter = "All Picture Files|*.bmp;*.dib;*.gif;*.wmf;*.emf;*.jpg;*.ico;*.cur|" & _
                     "Bitmaps(*.bmp,*.dib)|*.bmp;*.dib|" & _
                     "Gif Images(*.gif)|*.gif|" & _
                     "JPEG Images(*.jpg)|*.jpg|" & _
                     "Matafiles(*.wmf,*.emf)|*.wmf;*.emf|" & _
                     "Icons(*.ico,*.cur)|*.ico;*.cur|" & _
                     "All Files(*.*)|*.*"
            .Title = "Select an Image file"
            .FileName = ""
            .ShowDialog()
            If .FileName <> "" Then
                Err.Clear()
                On Error Resume Next
                Dim bm As New Bitmap(.FileName)
                picImge.SizeMode = PictureBoxSizeMode.StretchImage
                picImge.Image = bm
                If Err.Number Then
                    MsgBox(Err.Description)
                Else
                    picImge.Tag = .FileName
                    'lblpicpath.Text = .FileName
                    btnupdate.Enabled = True
                End If
            End If
            ChDir(Application.StartupPath)
        End With
    End Sub
    Private Sub saveImage()
        If picImge.Tag <> "" Then
            'On Error Resume Next
            If Directory.Exists(DPath & "\Photos") = False Then
                Directory.CreateDirectory(DPath & "\Photos")
            End If
            Dim imagename As String = "ITM-" & Val(lblitem.Tag) & ".png"
            If DPath <> "" Then
                imagename = DPath & "Photos\" & imagename
                If FileExists(imagename) Then
                    System.IO.File.Delete(imagename)
                End If
                FileCopy(picImge.Tag, imagename)
                If Err.Number Then
                    If MsgBox(Err.Description, vbRetryCancel + vbExclamation) = vbRetry Then Exit Sub
                End If
            End If
            uploadtoServer(Val(lblitem.Tag))
        End If
    End Sub
    Private Sub loadimage()
        Try
            If ftpurl <> "" And ftpusername <> "" And ftppassword <> "" Then
                btnupload.Enabled = False
                picImge.SizeMode = PictureBoxSizeMode.CenterImage
                picImge.ImageLocation = Application.StartupPath & "\loader.gif"
                'LdPic(picImge, Application.StartupPath & "\loader.gif", Me)
                Dim fname As String = ftpurl & "/ITM-" & Val(lblitem.Tag) & ".png"
                Dim MyWebClient As New System.Net.WebClient
                AddHandler MyWebClient.DownloadDataCompleted, AddressOf DownloadDataCompleted
                MyWebClient.Credentials = New NetworkCredential(ftpusername, ftppassword)
                MyWebClient.DownloadDataAsync(New Uri(fname))
            Else
                GoTo loadlocalimage
            End If
        Catch ex As Exception
            GoTo loadlocalimage
        End Try
        Exit Sub
loadlocalimage:
        LdPic(picImge, DPath & "Photos\ITM-" & Val(lblitem.Tag) & ".png", Me)
        lblpicpath.Text = "ITM-" & Val(lblitem.Tag) & ".png"
        lblpicpath.Tag = DPath & "Photos\ITM-" & Val(lblitem.Tag) & ".png"
        If FileExists(lblpicpath.Tag) Then
            btnupload.Enabled = True
        End If
        btnupdate.Enabled = True
        btnbrowse.Enabled = True

    End Sub
    Private Sub uploadtoServer(ByVal itemid As Long)
        If ftpurl <> "" And ftpusername <> "" And ftppassword <> "" Then
            Dim imagename As String = DPath & "Photos\ITM-" & itemid & ".png"
            If FileExists(imagename) Then
                If MsgBox("Do you want update image to webserver?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.No Then Exit Sub
                btnupdate.Enabled = False
                btnexit.Enabled = False
                btnupload.Enabled = False
                btnbrowse.Enabled = False

                Worker.RunWorkerAsync()
                Worker.WorkerSupportsCancellation = True
            End If
        Else
            MsgBox("Image Updated", MsgBoxStyle.Information)
            picloader.Visible = False
            Me.Close()
        End If
    End Sub
    Private Sub UploadAsyc(ByVal itemid As Long)
        Try
            Dim imagename As String = DPath & "Photos\ITM-" & itemid & ".png"
            Dim request As FtpWebRequest = DirectCast(WebRequest.Create(New Uri(ftpurl & "/ITM-" & itemid & ".png")), System.Net.FtpWebRequest)
            request.Method = WebRequestMethods.Ftp.UploadFile
            request.Credentials = New NetworkCredential(ftpusername, ftppassword)
            request.UseBinary = True
            request.UsePassive = True

            Dim buffer(1023) As Byte
            Dim bytesIn As Long = 1
            Dim totalBytesIn As Long = 0

            Dim filepath As System.IO.FileInfo = New System.IO.FileInfo(imagename)
            Dim ftpstream As System.IO.FileStream = filepath.OpenRead()
            Dim flLength As Long = ftpstream.Length
            Dim reqfile As System.IO.Stream = request.GetRequestStream()

            Do Until bytesIn < 1
                bytesIn = ftpstream.Read(buffer, 0, 1024)
                If bytesIn > 0 Then
                    reqfile.Write(buffer, 0, bytesIn)
                    totalBytesIn += bytesIn
                End If
            Loop
            reqfile.Close()
            ftpstream.Close()
        Catch ex As Exception
            MsgBox(ex.Message & vbCrLf & "Please Try again!", MsgBoxStyle.Critical)
        End Try
        
    End Sub
    Sub DownloadDataCompleted(ByVal sender As Object, ByVal e As DownloadDataCompletedEventArgs)
        If e.Cancelled = False AndAlso e.Error Is Nothing Then
            picImge.SizeMode = PictureBoxSizeMode.StretchImage
            picImge.Image = New Bitmap(New IO.MemoryStream(e.Result))
            lblpicpath.Text = "ITM-" & Val(lblitem.Tag) & ".png"
            lblpicpath.Tag = DPath & "Photos\ITM-" & Val(lblitem.Tag) & ".png"
        Else
            picImge.Image = Nothing
            picImge.ImageLocation = ""
            LdPic(picImge, DPath & "Photos\ITM-" & Val(lblitem.Tag) & ".png", Me, True)
            lblpicpath.Text = "ITM-" & Val(lblitem.Tag) & ".png"
            lblpicpath.Tag = DPath & "Photos\ITM-" & Val(lblitem.Tag) & ".png"
            If FileExists(lblpicpath.Tag) Then
                btnupload.Enabled = True
            End If
           
        End If
        btnupdate.Enabled = True
        btnbrowse.Enabled = True
    End Sub

    Private Sub ItemImageFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        picloader.SizeMode = PictureBoxSizeMode.StretchImage
        picloader.ImageLocation = Application.StartupPath & "\loader.gif"
        loadimage()
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        picloader.Visible = True
        saveImage()
    End Sub

    Private Sub btnupload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupload.Click
        btnupdate.Enabled = False
        btnexit.Enabled = False
        btnupload.Enabled = False
        picloader.Visible = True
        btnbrowse.Enabled = True

        Worker.RunWorkerAsync()
        Worker.WorkerSupportsCancellation = True
    End Sub

    Private Sub Worker_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles Worker.DoWork
        UploadAsyc(Val(lblitem.Tag))
    End Sub

    Private Sub Worker_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Worker.RunWorkerCompleted
        picloader.Visible = False
        btnupdate.Enabled = True
        btnexit.Enabled = True
        btnupload.Enabled = True
        MsgBox("Image Updated", MsgBoxStyle.Information)
        Me.Close()
    End Sub
End Class