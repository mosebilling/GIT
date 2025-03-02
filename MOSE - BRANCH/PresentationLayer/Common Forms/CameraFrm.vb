Imports AForge
Imports AForge.Video
Imports AForge.Video.DirectShow
Public Class CameraFrm
    Dim camera As VideoCaptureDevice
    Dim bmp As Bitmap
    Public Event sendimage()

    Private Sub CameraFrm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If camera Is Nothing Then Exit Sub
        On Error Resume Next
        camera.Stop()
    End Sub
    Private Sub CameraFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim cameras As VideoCaptureDeviceForm = New VideoCaptureDeviceForm
        If cameras.ShowDialog() = Windows.Forms.DialogResult.OK Then
            camera = cameras.VideoDevice
            AddHandler camera.NewFrame, New NewFrameEventHandler(AddressOf Captured)
            camera.Start()
        End If
    End Sub
    Private Sub Captured(ByVal sender As Object, ByVal eventArgs As NewFrameEventArgs)
        bmp = DirectCast(eventArgs.Frame.Clone(), Bitmap)
        picimage.Image = DirectCast(eventArgs.Frame.Clone(), Bitmap)
    End Sub

    Private Sub btnexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexit.Click
        btncapture.Tag = 0
        Me.Close()

    End Sub

    Private Sub btncapture_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncapture.Click
        btncapture.Tag = 1
        RaiseEvent sendimage()
        camera.Stop()
        Me.Close()
    End Sub
End Class