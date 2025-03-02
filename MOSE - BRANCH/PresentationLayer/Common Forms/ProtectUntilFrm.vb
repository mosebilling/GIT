Public Class ProtectUntilFrm
    Dim _objcmnbLayer As New clsCommon_BL
    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        _objcmnbLayer._saveDatawithOutParm("Update CompanyTb set ProtectUntil='" & Format(DateValue(dtpdate.Value), "yyyy/MM/dd") & "'")
        MsgBox("Updated", MsgBoxStyle.Information)
        ProtectUntil = getProtectUntil()
    End Sub

    Private Sub ProtectUntilFrm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dtpdate.Value = DateValue(ProtectUntil)
    End Sub
End Class