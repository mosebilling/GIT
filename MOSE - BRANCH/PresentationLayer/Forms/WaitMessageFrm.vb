Public Class WaitMessageFrm
    Public Event triggerEvent()
    Public triggerType As Integer
    Private Sub WaitMessageFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label8.Refresh()
        Timer1.Enabled = True
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        RaiseEvent triggerEvent()
    End Sub
End Class