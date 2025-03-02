Public Class WebsettingsFrm
    Private _objcmn As New clsCommon_BL
    Private WithEvents ftransfer As TransferToWebFrm
    Private Sub saveSettings()
        Dim dt As DataTable
        dt = _objcmn._fldDatatable("Select * from WebserverTb")
        If dt.Rows.Count > 0 Then
            _objcmn._saveDatawithOutParm("Update WebserverTb set " & _
                                         "webserver='" & txtserver.Text & "'," & _
                                         "username='" & txtuser.Text & "'," & _
                                         "password='" & txtpassword.Text & "'," & _
                                         "dbname='" & txtdb.Text & "'," & _
                                         "webIntegrationid=" & webIntegrationid)
        Else
            _objcmn._saveDatawithOutParm("Insert into WebserverTb values(" & _
                                         "'" & txtserver.Text & "'," & _
                                         "'" & txtuser.Text & "'," & _
                                         "'" & txtpassword.Text & "'," & _
                                         "'" & txtdb.Text & "'," & _
                                         webIntegrationid & ")")
        End If
        MsgBox("Updated", MsgBoxStyle.Information)
        getWebSettings()
    End Sub

    Private Sub btnexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexit.Click
        Me.Close()
    End Sub

    Private Sub WebsettingsFrm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtpassword.Text = ""
        If webserver <> "" Then txtserver.Text = webserver
        If webusername <> "" Then txtuser.Text = webusername
        If webpassword <> "" Then txtpassword.Text = webpassword
        lblwebintegrationid.Text = "Web Integration ID: " & webIntegrationid

        txtdb.Text = webdbname
    End Sub

    Private Sub txtserver_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtserver.KeyDown, txtdb.KeyDown, txtpassword.KeyDown, txtuser.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtserver_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtserver.TextChanged

    End Sub

    Private Sub btnproduct_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnproduct.Click
        ftransfer = New TransferToWebFrm
        ftransfer.isremove = chkremove.Checked
        ftransfer.typeofTransfer = 0
        ftransfer.Show(fMainForm)
    End Sub

    Private Sub ftransfer_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles ftransfer.FormClosed
        ftransfer = Nothing
    End Sub

    Private Sub btnproductlevel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnproductlevel.Click
        ftransfer = New TransferToWebFrm
        ftransfer.isremove = chkremove.Checked
        ftransfer.typeofTransfer = 2
        ftransfer.Show(fMainForm)
    End Sub

    Private Sub btngst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btngst.Click
        ftransfer = New TransferToWebFrm
        ftransfer.isremove = chkremove.Checked
        ftransfer.typeofTransfer = 3
        ftransfer.Show(fMainForm)
    End Sub

    Private Sub btntest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btntest.Click
        Dim _objweb As New webDatalayer
        Me.Cursor = Cursors.WaitCursor
        webusername = txtuser.Text
        webserver = txtserver.Text
        webpassword = txtpassword.Text
        webdbname = txtdb.Text
        If _objweb.testconnection() Then
            MsgBox("Test connection successful", MsgBoxStyle.Information)
            Dim dt As DataTable
            Dim _objcmnbLayer As New clsCommon_BL
            dt = _objcmnbLayer._fldDatatable("SELECT CompName FROM CompanyTb")
            Dim CompanyName As String = ""
            If dt.Rows.Count > 0 Then
                CompanyName = dt(0)(0)
            End If
            If webIntegrationid = 0 Then
                webIntegrationid = _objweb.saveDataToOnlineExecuteScalar("insert into companysettings(companyname) values('" & CompanyName & "') select Scope_identity()")
            End If
            lblwebintegrationid.Text = "Web Integration ID: " & webIntegrationid
            saveSettings()
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        _objcmn._saveDatawithOutParm("delete from WebserverTb")
        webusername = ""
        webserver = ""
        webpassword = ""
        webdbname = ""
        webIntegrationid = 0
        txtserver.Text = ""
        txtuser.Text = ""
        txtpassword.Text = ""
        lblwebintegrationid.Text = "Web Integration ID: "
    End Sub
End Class