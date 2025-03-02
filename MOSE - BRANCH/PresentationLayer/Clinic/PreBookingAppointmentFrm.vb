Public Class PreBookingAppointmentFrm
    Private _objcmnbLayer As New clsCommon_BL
    Private chgbyprg As Boolean
    Public Event doPrebooking(ByVal strday As Integer, ByVal count As Integer, ByVal doctorname As String, ByVal bookingtime As DateTime)
    Private Sub PreBookingAppointmentFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim _slsManTable As DataTable
        _slsManTable = _objcmnbLayer._fldDatatable("SELECT SManCode FROM SalesmanTb WHERE isnull(isdoctor,0)=1")
        Dim i As Integer
        For i = 0 To _slsManTable.Rows.Count - 1
            cmbsalesman.Items.Add(_slsManTable(i)("SManCode"))
        Next
        cmbsalesman.SelectedIndex = 0
        cmbday.SelectedIndex = 0
    End Sub

    Private Sub btnbook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnbook.Click
        RaiseEvent doPrebooking(cmbday.SelectedIndex, Val(txtno.Text), cmbsalesman.Text, dtptime.Value)
    End Sub
    Private Sub txtno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtno.KeyPress
        NumericTextOnKeypress(txtno, e, chgbyprg, "0")
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
End Class