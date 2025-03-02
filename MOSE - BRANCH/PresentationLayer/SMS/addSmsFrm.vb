Public Class addSmsFrm
    Private _objweb As New webDatalayer
    Dim _objcmnbLayer As New clsCommon_BL
    Private chgbyprg As Boolean

    Private Sub addSmsFrm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT isnull(smsActiveId,0)smsActiveId,smsAPIKey FROM CompanyTb")
        If dt.Rows.Count > 0 Then
            txtkey.Text = dt(0)(0)
            txtapi.Text = Trim(dt(0)("smsAPIKey") & "")
        End If
        If Val(txtkey.Text) > 0 Then
            btnadd.Enabled = False
            Dim smsBal As Integer
            _objweb.isSms = True
            dt = _objweb.returnDatatable("select isnull(smscount,0)smscount,isnull(smssent,0)smssent,isnull(status,0)status from SMSCntTb where id=" & Val(txtkey.Text))
            If dt.Rows.Count > 0 Then
                smsBal = dt(0)(0) - dt(0)(1)
            End If
            chgbyprg = True
            txtcount.Text = smsBal
            chgbyprg = False
        End If
    End Sub

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        If Val(txtkey.Text) > 0 Then GoTo updateAPIOnly
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT CompName,smsAPIKey FROM CompanyTb")
        Dim CompanyName As String = ""
        If dt.Rows.Count > 0 Then
            CompanyName = dt(0)(0)
            If txtapi.Text = "" Then txtapi.Text = Trim(dt(0)(1) & "")
        End If
        Dim qry As String = "INSERT INTO SMSCntTb(companyname,smscount,status)VALUES('" & CompanyName & "'," & Val(txtcount.Text) & ",1)  select Scope_identity()"
        _objweb.isSms = True
        Dim id As String = _objweb.saveDataToOnlineExecuteScalar(qry)
        _objcmnbLayer._saveDatawithOutParm("update CompanyTb set smsAPIKey='" & txtapi.Text & "', smsActiveId=" & Val(id))
        If webserver <> "" And webpassword <> "" And webIntegrationid > 0 Then
            _objweb.saveDataToOnline("update companysettings set smsDbId=" & Val(id) & " where cid=" & webIntegrationid)
        End If
        txtkey.Text = id
        btnadd.Enabled = False
updateAPIOnly:
        _objweb.saveDataToOnline("update SMSCntTb set smscount=" & txtcount.Text & " where Id=" & Val(txtkey.Text))
        _objcmnbLayer._saveDatawithOutParm("update CompanyTb set smsAPIKey='" & txtapi.Text & "'")
        btnadd.Enabled = False
    End Sub

    Private Sub txtapi_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtapi.TextChanged
        btnadd.Enabled = True
    End Sub

    Private Sub txtcount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcount.KeyPress
        NumericTextOnKeypress(txtcount, e, chgbyprg, "0")
    End Sub

    Private Sub txtcount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcount.TextChanged
        If chgbyprg = True Then Exit Sub
        btnadd.Enabled = True
    End Sub
End Class