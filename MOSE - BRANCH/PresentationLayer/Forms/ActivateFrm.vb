Imports MoseActivationDll
Public Class ActivateFrm
    Private _objcmnbLayer As New clsCommon_BL
    Private objactivation As New activationdll
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        End
    End Sub

    Private Sub ActivateFrm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtagent.Focus()
    End Sub

    Private Sub ActivateFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select CompName,LICENSEKEY,Tel1 PHONE,EMAIL,Addr1,Addr2,Addr3,Addr4 from companyTb ")
        If dt.Rows.Count > 0 Then
            txtcompany.Text = dt(0)("CompName")
            txtemail.Text = dt(0)("EMAIL")
            txtphone.Text = dt(0)("PHONE")
            txtaddress.Text = Trim(dt(0)("Addr1") & ",") & Trim(dt(0)("Addr2") & "") & IIf(Trim(dt(0)("Addr2") & "") = "", "", ",") & Trim(dt(0)("Addr3") & "") & IIf(Trim(dt(0)("Addr3") & "") = "", "", ",") & Trim(dt(0)("Addr4") & "")
        End If
        txtcomputer.Text = Environment.MachineName
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        If txtcompany.Text = "" Then
            MsgBox("Invalid Company name", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If txtcomputer.Text = "" Then
            MsgBox("Invalid Computer name", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If txtagent.Text = "" Then
            MsgBox("Invalid Agent Code", MsgBoxStyle.Exclamation)
            txtagent.Focus()
            Exit Sub
        End If
        If txtproductcode.Text = "" Then
            MsgBox("Invalid Product Code", MsgBoxStyle.Exclamation)
            txtproductcode.Focus()
            Exit Sub
        End If
        Dim version As String = My.Application.Info.Version.ToString
        version = version.Replace(".", "")
        Dim IdentityKEY As String = ""
        Dim result As Integer = objactivation.validateProductKey(txtproductcode.Text, txtagent.Text, MkDbSrchStr(txtcompany.Text), txtcomputer.Text, txtphone.Text, txtemail.Text, version, IdentityKEY, MkDbSrchStr(txtaddress.Text))
        If result = 0 Then
            MsgBox("Activation Failed", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If result = 2 Then
            MsgBox("User Limit Exceeds", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Dim objen As New encriptTex
        IdentityKEY = objen.Encrypt(IdentityKEY)
        objactivation.saveActivation(IdentityKEY, MkDbSrchStr(txtcompany.Text), txtcomputer.Text, "", txtproductcode.Text)
        MsgBox("Activation successfully completed", MsgBoxStyle.Information)
        Me.Close()
    End Sub
End Class