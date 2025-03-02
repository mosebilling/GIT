Public Class GarrageJobDeliveryFrm
    Private _objcmnbLayer As New clsCommon_BL
    Private Sub GarrageJobDeliveryFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        If MsgBox("Do you want update Delivery?",MsgBoxStyle.Question+MsgBoxStyle.YesNo)=MsgBoxResult.No then Exit Sub
        _objcmnbLayer._saveDatawithOutParm("Update GarrageTb set " & _
                                           "deliverydate='" & Format(dtpdate.Value, "yyyy/MMM/dd hh:mm tt") & "'," & _
                                           "deliveredBy='" & txtdeliveredBy.Text & "'," & _
                                           "Status=2," & _
                                           "receivedBy='" & txtreceivedBy.Text & "' " & _
                                           "where grid=" & Val(lblcar.Tag))
        MsgBox("Delivery Updated", MsgBoxStyle.Information)
        Me.Close()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class