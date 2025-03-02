Public Class PrintCopiesFrm

    Private Sub btnAddgst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddgst.Click
        btnAddgst.Tag = 1
        Me.Close()
    End Sub

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        btnAddgst.Tag = 0
        Me.Close()
    End Sub
End Class