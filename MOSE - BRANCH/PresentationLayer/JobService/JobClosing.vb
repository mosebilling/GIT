Public Class JobClosing
    Private _objcmnbLayer As clsCommon_BL
    Public isCloseItem As Boolean
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        If isCloseItem Then
            If MsgBox("Do you want close this Job?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            _objcmnbLayer = New clsCommon_BL
            _objcmnbLayer._saveDatawithOutParm("UPDATE JobItemtb SET isClosed =1," & _
                                                    " closedDate='" & Format(DateValue(cldrdate.Value), "yyyy/MMM/dd") & "' WHERE jbitmId=" & Val(cldrdate.Tag))
        Else
            'If MsgBox("Do you want close this job?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            _objcmnbLayer = New clsCommon_BL
            Dim qry As String
            qry = "Update JobTb set [Status]=1,JobCloseDate='" & Format(cldrdate.Value, "yyyy/MMM/dd hh:mm:ss tt") & "',Jdeliverydate='" & Format(cldrdate.Value, "yyyy/MMM/dd hh:mm:ss tt") & "' where Jobid=" & Val(lbljjob.Tag)
            qry = qry & " UPDATE JobItemtb SET isClosed =1," & _
                  " closedDate='" & Format(cldrdate.Value, "yyyy/MMM/dd") & "' WHERE jbid=" & Val(lbljjob.Tag)
            _objcmnbLayer._saveDatawithOutParm(qry)
        End If
        MsgBox("Job Closed", MsgBoxStyle.Information)
        Me.Close()
    End Sub
End Class