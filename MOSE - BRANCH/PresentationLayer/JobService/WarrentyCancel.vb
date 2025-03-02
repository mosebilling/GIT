Public Class WarrentyCancel
    Private _objcmnbLayer As clsCommon_BL
    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        If MsgBox("Do you want cancel warrenty?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        _objcmnbLayer = New clsCommon_BL
        _objcmnbLayer._saveDatawithOutParm("Update SerialNoTb set Warrenty=2,CancelDate='" & Format(DateValue(cldrdate.Value), "yyyy/MMM/dd") & "' where SerialNo='" & cldrdate.Tag & "'")
        MsgBox("Warrenty Cancelled", MsgBoxStyle.Information)
        Me.Close()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class