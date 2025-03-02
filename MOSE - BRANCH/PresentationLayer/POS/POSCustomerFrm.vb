Public Class POSCustomerFrm
    Private _objcmnbLayer As New clsCommon_BL

    Private Sub txtphone_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtphone.KeyDown
        If e.KeyCode = Keys.Return Then
            txtCashCustomer.Focus()
        End If
    End Sub

    Private Sub txtphone_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtphone.Validated
        Dim dt As DataTable = _objcmnbLayer._fldDatatable("Select custid,CustName,Add1,Add2,Phone,email,salesPoints,GiftVrNo,Memberid " & _
                                             "from CashCustomerTb " & _
                                             "where Phone='" & txtphone.Text & "'")
        If dt.Rows.Count > 0 Then
            txtphone.Text = dt(0)("Phone")
            txtCashCustomer.Text = dt(0)("CustName")
            txtcustAddress.Text = dt(0)("Add1")
            txtcustemail.Text = dt(0)("email")
            txtphone.Tag = dt(0)("custid")
           
        End If
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub

    Private Sub txtCashCustomer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCashCustomer.KeyDown
        If e.KeyCode = Keys.Return Then
            btnOK.Focus()
        End If
    End Sub
End Class