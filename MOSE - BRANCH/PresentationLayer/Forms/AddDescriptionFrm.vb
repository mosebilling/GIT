Public Class AddDescriptionFrm
    Private _objcmn As New clsCommon_BL
    Private Sub btnexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexit.Click
        Me.Close()
    End Sub

    Private Sub BtnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUpdate.Click
        _objcmn._saveDatawithOutParm("Update invitm set shortDescr='" & txtshort.Text & "', longDescr='" & txtlong.Text & "' where itemid=" & Val(txtshort.Tag))
        MsgBox("Text Updated", MsgBoxStyle.Information)
        Me.Close()
    End Sub
    Public Sub getDescriptions()
        Dim dt As DataTable
        dt = _objcmn._fldDatatable("Select shortDescr,longDescr from invitm where itemid=" & Val(txtshort.Tag))
        If dt.Rows.Count > 0 Then
            txtshort.Text = Trim(dt(0)("shortDescr") & "")
            txtlong.Text = Trim(dt(0)("longDescr") & "")
        End If
    End Sub
End Class