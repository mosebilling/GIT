Public Class About

    Private Sub About_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Label1.Text = "Billing && Service Tracking Solution || Version " & vrVersion
        'Label3.Text = "Version : " & vrVersion
        Label3.Text = "Version : " & My.Application.Info.Version.ToString
    End Sub
End Class