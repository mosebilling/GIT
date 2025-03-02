Imports System.IO
Public Class DocumentView
    Public KeyId As String
    Public moduleid As Integer '1-Accounts Voucher,2-Account Master,3-Item master,4-Employee Master,5-Hospitality,6-Membership,7-Usedcar
    'object declarations
    Dim _objcmnbLayer As New clsCommon_BL
    Private dtImage As DataTable
    Private _vRecPosition As Integer
    Private dtPhotopath As String
    Public isDoc As Boolean
    Private bm As Bitmap
    Private isSelectedDoc As Boolean
    Private pbThambNail() As PictureBox
    Private pbCount As Integer
    Public itemid As Integer
    'Dim objImage As Image

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        Dim bm As Bitmap
        With fMainForm.DlgOpen
            If isDoc Then
                .Title = "Select a file"
                .Filter = "All Files|*.bmp;*.dib;*.gif;*.wmf;*.emf;*.jpg;*.ico;*.cur|" & _
                                     "Bitmaps(*.bmp,*.dib)|*.bmp;*.dib|" & _
                                     "Gif Images(*.gif)|*.gif|" & _
                                     "JPEG Images(*.jpg)|*.jpg|" & _
                                     "Matafiles(*.wmf,*.emf)|*.wmf;*.emf|" & _
                                     "Icons(*.ico,*.cur)|*.ico;*.cur|" & _
                                     "All Files(*.*)|*.*"
            Else
                .Title = "Select an Image file"
                .Filter = "All Files|*.bmp;*.dib;*.gif;*.wmf;*.emf;*.jpg;*.ico;*.cur|" & _
                                     "Bitmaps(*.bmp,*.dib)|*.bmp;*.dib|" & _
                                     "Gif Images(*.gif)|*.gif|" & _
                                     "JPEG Images(*.jpg)|*.jpg|" & _
                                     "Matafiles(*.wmf,*.emf)|*.wmf;*.emf|" & _
                                     "Icons(*.ico,*.cur)|*.ico;*.cur"
            End If
            .FileName = ""
            Dim varXml As String
            .ShowDialog()
            If .FileName <> "" Then
                Try
                    If isDoc = True Then
                        'bm = New Bitmap(Application.StartupPath & IO.Path.GetFileNameWithoutExtension(.FileName))
                        'bm = New Bitmap(Application.StartupPath & "/Doc.png")
                        varXml = Mid(.FileName, .FileName.LastIndexOf("\") + 2)
                        varXml = Mid(varXml, varXml.IndexOf("."))
                        picImage.Image = returnBitMapForDoc(varXml)

                        'bm = New Bitmap(.FileName)
                        'picImage.Image = bm
                        isSelectedDoc = True

                    Else
                        bm = New Bitmap(.FileName)
                        picImage.Image = bm
                        isSelectedDoc = False
                    End If

                Catch ex As Exception
                    bm = New Bitmap(Application.StartupPath & "\Doc.png")
                    picImage.Image = bm
                    isSelectedDoc = True
                End Try
                picImage.Tag = .FileName
                txtpath.Text = Mid(.FileName, .FileName.LastIndexOf("\") + 2)
                If Len(txtpath.Text) > 50 Then
                    MsgBox("File name Should be short less than 50 charectors", MsgBoxStyle.Critical)
                    Exit Sub
                End If
                If isDoc Then
                    saveDoc()
                Else
                    If itemid > 0 Then
                        saveItemimage()
                    Else
                        saveImage()
                    End If

                End If
            End If
        End With
        ldImage()
    End Sub
    Private Sub saveItemimage()
        Dim findExt As String
        If Directory.Exists(dtPhotopath) = False Then
            Directory.CreateDirectory(dtPhotopath)
        End If
        Try
            findExt = picImage.Tag
            findExt = Mid(findExt, findExt.LastIndexOf(".") + 2)
            Dim filename As String
            Dim filepath As String
            filename = KeyId & "-" & 1 & "." & findExt
            filepath = dtPhotopath & "\" & filename
            If FileExists(filepath) Then
                File.Delete(filepath)
            End If
            File.Copy(picImage.Tag, filepath)
            _objcmnbLayer._saveDatawithOutParm("UPDATE INVITM SET Image1='" & filename & "' where itemid=" & itemid)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub saveImage()
        Dim dt As DataTable
        Dim id As Integer
        Dim findExt As String
        _objcmnbLayer._saveDatawithOutParm("INSERT INTO ImgTb(KeyId,DocName,DocImg,ModuleId)values(" & _
                                           "'" & KeyId & "','" & txtpath.Text & "',''," & moduleid & ")")
        dt = _objcmnbLayer._fldDatatable("SELECT max(id)id FROM ImgTb")
        If dt.Rows.Count > 0 Then
            id = dt(0)(0)
        End If
        If Directory.Exists(dtPhotopath) = False Then
            Directory.CreateDirectory(dtPhotopath)
        End If
        Try
            findExt = picImage.Tag
            findExt = Mid(findExt, findExt.LastIndexOf(".") + 2)
            File.Copy(picImage.Tag, dtPhotopath & "\" & KeyId & "-" & id & "." & findExt)
            dtImage = _objcmnbLayer._fldDatatable("SELECT * FROM ImgTb WHERE KeyId='" & KeyId & "'")
            _vRecPosition = dtImage.Rows.Count - 1
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub saveDoc()
        Dim dt As DataTable
        Dim id As Integer
        Dim findExt As String
        _objcmnbLayer._saveDatawithOutParm("INSERT INTO DocAttachmentTb(KeyId,DocName,DocImg,ModuleId,Description)values(" & _
                                           "'" & KeyId & "','" & txtpath.Text & "','" & txtpath.Text & "'," & moduleid & ",'" & txtimgDesc.Text & "')")
        dt = _objcmnbLayer._fldDatatable("SELECT max(id)id FROM DocAttachmentTb")
        If dt.Rows.Count > 0 Then
            id = dt(0)(0)
        End If
        If Directory.Exists(dtPhotopath) = False Then
            Directory.CreateDirectory(dtPhotopath)
        End If
        Try
            findExt = picImage.Tag
            findExt = Mid(findExt, findExt.LastIndexOf(".") + 2)
            File.Copy(picImage.Tag, dtPhotopath & "\" & KeyId & "-" & id & "." & findExt)
            dtImage = _objcmnbLayer._fldDatatable("SELECT * FROM DocAttachmentTb WHERE KeyId='" & KeyId & "'")
            _vRecPosition = dtImage.Rows.Count - 1
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
   
    Private Sub DocumentView_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim dtpath As DataTable
        'dtpath = _objcmnbLayer._fldDatatable("SELECT DataPath FROM SysTb ")
        'If dtpath.Rows.Count > 0 Then
        '    If Not IsDBNull(dtpath(0)("DataPath")) Then
        '        dtPhotopath = dtpath(0)("DataPath") & "Photos"
        '    End If
        'End If
    End Sub
    Public Sub ldImage()
        dtPhotopath = DPath & "Photos"
        If isDoc Then
            dtImage = _objcmnbLayer._fldDatatable("SELECT * FROM DocAttachmentTb WHERE KeyId='" & KeyId & "'")
        Else
            dtImage = _objcmnbLayer._fldDatatable("SELECT image1 FROM InvItm WHERE itemid=" & itemid)
        End If

        If _vRecPosition < 0 Then _vRecPosition = 0
        ShowCurrentRecord(_vRecPosition)
        'loadThumbNailsOfAllPics()
    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click, btnPre.Click, btnFirst.Click, btnLast.Click
        Dim btn As Control = sender
        Select Case btn.Tag
            Case 1
                _vRecPosition = 0
            Case 2
                If _vRecPosition > 0 Then
                    _vRecPosition = _vRecPosition - 1
                End If
            Case 3
                If _vRecPosition < dtImage.Rows.Count - 1 Then
                    _vRecPosition = _vRecPosition + 1
                End If
            Case 4
                If _vRecPosition <> dtImage.Rows.Count - 1 Then
                    _vRecPosition = dtImage.Rows.Count - 1
                End If
        End Select
        ShowCurrentRecord(_vRecPosition)
    End Sub
    Private Sub ShowItemimage(ByVal intRecPosition As Integer)
        Dim objImage As Image
        Dim Ppath As String
        If dtImage.Rows.Count > 0 Then
            Ppath = Trim(dtImage(0)("image1") & "")
            If Ppath = "" Then GoTo els
            Ppath = dtPhotopath & "\" & Ppath
            Dim varXml As String
            If File.Exists(Ppath) Then
                Try
                    'bm = New Bitmap(Ppath)
                    'picImage.Image = bm

                    Using str As Stream = File.OpenRead(Ppath)
                        objImage = Image.FromStream(str)
                    End Using
                    picImage.Width = 603
                    picImage.Height = 379
                    picImage.Left = 64
                    picImage.Top = 3
                    picImage.Image = objImage


                Catch ex As Exception
                    'bm = New Bitmap(Application.StartupPath & "\DOC.PNG")
                    'picImage.Image = bm
                    picImage.Width = 80
                    picImage.Height = 68
                    picImage.Left = 299
                    picImage.Top = 147
                    varXml = Mid(Ppath, Ppath.LastIndexOf("\") + 2)
                    varXml = Mid(varXml, varXml.IndexOf("."))
                    picImage.Image = returnBitMapForDoc(varXml)
                End Try
                picImage.Tag = Ppath
            End If
        Else
els:
            txtpath.Text = ""
            txtpath.Tag = ""
            picImage.Tag = ""
            picImage.Image = Nothing
        End If
    End Sub
    Private Sub ShowCurrentRecord(ByVal intRecPosition As Integer)
        Dim objImage As Image
        Dim Ppath As String
        picImage.Image = Nothing
        If itemid > 0 Then ShowItemimage(0) : Exit Sub
        If dtImage.Rows.Count > 0 Then
            Ppath = dtImage(intRecPosition)("DocImg")
            txtpath.Text = dtImage(intRecPosition)("DocName")
            txtimgDesc.Text = dtImage(intRecPosition)("Description")
            txtpath.Tag = dtImage(intRecPosition)("id")
            Ppath = dtPhotopath & "\" & KeyId & "-" & Val(txtpath.Tag) & "." & Mid(txtpath.Text, txtpath.Text.LastIndexOf(".") + 2)
            Dim varXml As String
            If File.Exists(Ppath) Then
                Try
                    'bm = New Bitmap(Ppath)
                    'picImage.Image = bm

                    Using str As Stream = File.OpenRead(Ppath)
                        objImage = Image.FromStream(str)
                    End Using
                    picImage.Width = 603
                    picImage.Height = 379
                    picImage.Left = 64
                    picImage.Top = 3
                    picImage.Image = objImage


                Catch ex As Exception
                    'bm = New Bitmap(Application.StartupPath & "\DOC.PNG")
                    'picImage.Image = bm
                    picImage.Width = 80
                    picImage.Height = 68
                    picImage.Left = 299
                    picImage.Top = 147
                    varXml = Mid(Ppath, Ppath.LastIndexOf("\") + 2)
                    varXml = Mid(varXml, varXml.IndexOf("."))
                    picImage.Image = returnBitMapForDoc(varXml)
                End Try
                picImage.Tag = Ppath
            End If
        Else
            txtpath.Text = ""
            txtpath.Tag = ""
            picImage.Tag = ""
            picImage.Image = Nothing
        End If
    End Sub
  
    Sub loadThumbNailsOfAllPics()

        Dim objImage As Image

        Dim Ppath As String
        Dim strImageName As String


        'For Each ctl As Control In Me.Panel1

        '    If (TypeOf ctl Is PictureBox) Then
        '        Dim picBox As PictureBox = DirectCast(ctl, PictureBox)
        '        picBox.Image.Dispose()
        '    End If
        'Next ctl

        'Using str As Stream = File.OpenRead(Fileloc)
        '    objImage = Image.FromStream(str)
        'End Using
        'PictureBox.Image = objImage

        'For Each ctl As Control In Me.Panel1.Controls
        '    If (TypeOf ctl Is PictureBox) Then
        '        Dim picBox As PictureBox = DirectCast(ctl, PictureBox)
        '        picBox.Image.Dispose()
        '    End If
        'Next ctl

        Dim Array_Size As Integer = dtImage.Rows.Count
        'pbThambNail.Initialize.

        'If Not pbThambNail Is Nothing Then
        '    pbCount = pbThambNail.Count
        'End If
        If Array_Size > pbCount Then
            ReDim Preserve pbThambNail(Array_Size)
        Else
            Dim i As Integer
            For i = Array_Size To pbCount - 1
                pbThambNail(i).Visible = False
            Next
        End If

        'For i = 0 To Array_Size
        pbCount = Array_Size
        For i = 0 To dtImage.Rows.Count - 1
            'pbThambNail(i).Dispose()
            If pbThambNail(i) Is Nothing Then
                pbThambNail(i) = New PictureBox
            End If
            If i = 0 Then pbThambNail(i).Top = 3
            pbThambNail(i).Visible = True
            strImageName = dtImage(i)("DocName")

            Ppath = dtImage(i)("DocImg")

            'pbThambNail(i).Text = "Radio Button " + i.ToString
            'pbThambNail(i).Tag = dtImage(i)("id")
            pbThambNail(i).Tag = i

            pbThambNail(i).Top = 53 * i
            pbThambNail(i).SizeMode = PictureBoxSizeMode.StretchImage

            Ppath = dtPhotopath & "\" & KeyId & "-" & dtImage(i)("id") & "." & Mid(strImageName, strImageName.LastIndexOf(".") + 2)
            Dim varXml As String
            If File.Exists(Ppath) Then
                Try
                    'picImage.Dispose()
                    'bm.Dispose()
                    'bm = New Bitmap(Ppath)

                    Using str As Stream = File.OpenRead(Ppath)
                        objImage = Image.FromStream(str)
                    End Using

                    pbThambNail(i).Image = objImage
                    pbThambNail(i).Refresh()
                    'pbThambNail(i).Image = bm

                Catch ex As Exception
                    varXml = Mid(Ppath, Ppath.LastIndexOf("\") + 2)
                    varXml = Mid(varXml, varXml.IndexOf(".") + 1)
                    pbThambNail(i).Image = returnBitMapForDoc(varXml)
                End Try
                'pbThambNail(i).Tag = Ppath
            End If
            'Else
            '    txtpath.Text = ""
            '    txtpath.Tag = ""
            '    picImage.Tag = ""
            '    picImage.Image = Nothing
            'End If
            AddHandler pbThambNail(i).Click, AddressOf doClickAction
            Me.Panel1.Controls.Add(pbThambNail(i))
            Me.Panel1.Controls.AddRange(pbThambNail)
        Next
    End Sub
    Protected Sub doClickAction(ByVal sender As Object, ByVal e As EventArgs)
        Dim thisPictureBox As PictureBox
        thisPictureBox = sender
        'MsgBox(thisPictureBox.Tag)
        ShowCurrentRecord(thisPictureBox.Tag)

        'thisPictureBox.BackColor = Color.DarkBlue
        'thisPictureBox.Text = "clicked"
    End Sub

    Private Sub ShowCurrentRecord_Old()
        Dim Ppath As String

        If dtImage.Rows.Count > 0 Then
            Ppath = dtImage(_vRecPosition)("DocImg")
            txtpath.Text = dtImage(_vRecPosition)("DocName")
            txtpath.Tag = dtImage(_vRecPosition)("id")
            Ppath = dtPhotopath & "\" & KeyId & "-" & Val(txtpath.Tag) & "." & Mid(txtpath.Text, txtpath.Text.LastIndexOf(".") + 2)
            If File.Exists(Ppath) Then
                Try
                    'picImage.Dispose()
                    'bm.Dispose()
                    bm = New Bitmap(Ppath)
                    picImage.Image = bm
                Catch ex As Exception
                    bm = New Bitmap(Application.StartupPath & "\pic.bmp")
                    picImage.Image = bm
                End Try
                picImage.Tag = Ppath
                'bm.Dispose()
            End If
        Else
            txtpath.Text = ""
            txtpath.Tag = ""
            picImage.Tag = ""
            picImage.Image = Nothing
        End If
    End Sub

    Sub OepenFile(ByVal strPathAndFile As String)
        Try
            Dim p As New System.Diagnostics.Process
            Dim s As New System.Diagnostics.ProcessStartInfo(strPathAndFile)
            s.UseShellExecute = True
            s.WindowStyle = ProcessWindowStyle.Normal
            p.StartInfo = s
            p.Start()
        Catch ex As Exception
            MessageBox.Show("File '" & strPathAndFile & "' couldnt be found!", "www.interloper.nl", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnopen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnopen.Click
        OepenFile(picImage.Tag)
    End Sub

    Private Sub btnremove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnremove.Click
        Try
            'txtpath.Tag = dtImage(_vRecPosition)(0)
            Dim Ppath As String
            Ppath = dtPhotopath & "\" & KeyId & "-" & Val(txtpath.Tag) & "." & Mid(txtpath.Text, txtpath.Text.LastIndexOf(".") + 2)
            'If Not picImage.Image Is Nothing Then
            '    picImage.Image.Dispose()
            '    picImage.Image = Nothing
            '    picImage.Refresh()
            'End If
            'bm.Dispose()
            'bm = Nothing

            'pbThambNail(_vRecPosition).Image.Dispose()
            'picImage.Image.Dispose()

            'pbThambNail(_vRecPosition).Image = Nothing
            'picImage.Image = Nothing
            If File.Exists(Ppath) Then
                File.Delete(Ppath)
            End If


            'If dtImage.Rows.Count > 0 Then
            '    dtImage.Rows.RemoveAt(_vRecPosition)
            '    If _vRecPosition > dtImage.Rows.Count - 1 Then
            '        _vRecPosition = _vRecPosition - 1
            '        ShowCurrentRecord(_vRecPosition)
            '    ElseIf _vRecPosition < dtImage.Rows.Count - 1 Then
            '        _vRecPosition = _vRecPosition + 1
            '        ShowCurrentRecord(_vRecPosition)
            '    Else
            '        '_vRecPosition = 0
            '        ShowCurrentRecord(_vRecPosition)
            '    End If
            'End If

            If isDoc Then
                _objcmnbLayer._saveDatawithOutParm("DELETE FROM DocAttachmentTb WHERE ID=" & Val(txtpath.Tag))
            Else
                '_objcmnbLayer._saveDatawithOutParm("DELETE FROM ImgTb WHERE ID=" & Val(txtpath.Tag))
                _objcmnbLayer._saveDatawithOutParm("UPDATE INVITM SET Image1='' where itemid=" & itemid)
            End If
            _vRecPosition = _vRecPosition - 1

            Call ldImage()


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try


    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub picImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picImage.Click

    End Sub

    Private Sub picImage_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picImage.MouseWheel
        If e.Delta <> 0 Then
            If e.Delta <= 0 Then
                If picImage.Width < 500 Then Exit Sub 'minimum 500?
            Else
                If picImage.Width > 2000 Then Exit Sub 'maximum 2000?
            End If

            picImage.Width += CInt(picImage.Width * e.Delta / 1000)
            picImage.Height += CInt(picImage.Height * e.Delta / 1000)
        End If
    End Sub

    Private Sub btndecsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndecsave.Click
        _objcmnbLayer._saveDatawithOutParm("UPDATE DocAttachmentTb  SET Description='" & txtimgDesc.Text & "'  WHERE ID=" & Val(txtpath.Tag))
        MsgBox("Updated", MsgBoxStyle.Information)
    End Sub
End Class