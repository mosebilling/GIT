Public Class CheckOutFrm
    Private _objJob As New clsJob
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        If MsgBox("Are you sure to checkout this Room?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        _objJob.ldgroomid = Val(lblroom.Tag)
        _objJob.checkoutDateTime = dtcheckout.Value
        _objJob.checkOutLodge()
        Me.Close()
    End Sub
End Class