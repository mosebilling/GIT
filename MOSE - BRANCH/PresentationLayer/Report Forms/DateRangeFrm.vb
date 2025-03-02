Public Class DateRangeFrm

    Private Sub btnapply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnapply.Click
        btnapply.Tag = 1
        Me.Close()
    End Sub

    Private Sub btnexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexit.Click
        btnapply.Tag = 0
        Me.Close()
    End Sub
End Class