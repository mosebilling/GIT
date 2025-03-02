Public Class SendShortSMSFrm
    Private _objTr As New clsSMS
    Private _objcmnbLayer As New clsCommon_BL
    Private _objweb As New webDatalayer
    Private Sub SendShortSMSFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        getSMSKey()
        getSMSCount()
    End Sub
    Private Sub getSMSKey()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("select isnull(smsActiveId,0) from CompanyTb")
        If dt.Rows.Count > 0 Then
            btnsend.Tag = dt(0)(0)
        End If
    End Sub
    Private Sub getSMSCount()
        Dim dt As DataTable
        _objweb.isSms = True
        dt = _objweb.returnDatatable("select isnull(smscount,0)smscount,isnull(smssent,0)smssent,isnull(status,0)status from SMSCntTb where id=" & Val(btnsend.Tag))
        Dim smsBal As Integer
        If dt.Rows.Count > 0 Then
            smsBal = dt(0)(0) - dt(0)(1)
            If dt(0)(2) = "True" Then
                lblstaus.Text = "Active"
                lblstaus.ForeColor = Color.Green
                btnsend.Enabled = True
            Else
                GoTo els
            End If
        Else
els:
            lblstaus.Text = "Inactive"
            lblstaus.ForeColor = Color.Red
            btnsend.Enabled = False
        End If
        lblsmsremaining.Text = "Remaining : " & smsBal
        lblsmsremaining.Tag = smsBal
    End Sub

    Private Sub btnsend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsend.Click
        If txtphone.Text = "" Or txtphone.Text.Length <> 10 Or Val(txtphone.Text) = 0 Then
            MsgBox("Invalid Phone Number", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If txtcontent.Text = "" Then
            MsgBox("Message not Found", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Me.Cursor = Cursors.WaitCursor
        Dim result As String = sendSMS(txtphone.Text)
        Me.Cursor = Cursors.Default
        If Trim(result & "") <> "failure" And Trim(result & "") <> "" Then
            saveSmsTransaction(txtphone.Text, txtparty.Text)
            _objweb.saveDataToOnline("Update SMSCntTb set smssent=isnull(smssent,0)+1 where id=" & Val(btnsend.Tag))
        End If
    End Sub
    Private Function sendSMS(ByVal smsNumber As String) As String
        Dim sms As New sendSMS
        'smsNumber = "919526794529,918156932691"
        Dim result As String = sms.sendSMSFn(smsNumber, txtcontent.Text)
        result = Mid(result, result.IndexOf("status"))
        result = Mid(result, result.IndexOf(":") + 3)
        result = Mid(result, result.IndexOf(":") + 2)
        result = Mid(result, 1, result.IndexOf(""""))
        Return result
    End Function
    Private Sub saveSmsTransaction(ByVal smsNumber As String, ByVal receivername As String)
        _objcmnbLayer._saveDatawithOutParm("INSERT INTO smsTransactionTb (smsNumber,formatid,smsDateTime,receivername) VALUES(" & _
                                           "'" & smsNumber & "',0,GETDATE(),'" & receivername & "')")

    End Sub

    Private Sub txtcontent_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcontent.TextChanged
        lblcharector.Text = "Charector Remaining : " & 120 - Len(txtcontent.Text) & " / 120"
    End Sub
End Class