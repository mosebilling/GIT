Public Class LodgeSettingsFrm
    Private _objcmnbLayer As New clsCommon_BL
    Private chgbyprg As Boolean
    Private Sub LodgeSettingsFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loadHsn()
        loadLodgesettings()
    End Sub
    Private Sub loadHsn()
        Dim dt As DataTable
        Dim i As Integer
        dt = _objcmnbLayer._fldDatatable("SELECT HSNCode FROM GSTTb")
        cmbtaxhsn.Items.Clear()
        cmbnontaxhsn.Items.Clear()
        cmbnontaxhsn.Items.Add("")
        cmbtaxhsn.Items.Add("")
        cmbhsn1.Items.Add("")
        For i = 0 To dt.Rows.Count - 1
            cmbtaxhsn.Items.Add(dt(i)("HSNCode"))
            cmbnontaxhsn.Items.Add(dt(i)("HSNCode"))
            cmbhsn1.Items.Add(dt(i)("HSNCode"))
        Next
    End Sub
    Private Sub loadLodgesettings()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("select * from LodgeSettingsTb")
        If dt.Rows.Count > 0 Then
            If Val(dt(0)("taxableAmount") & "") = 0 Then dt(0)("taxableAmount") = 0
            txtamount.Text = Format(CDbl(dt(0)("taxableAmount")), numFormat)
            cmbtaxhsn.Text = Trim(dt(0)("taxableHsnCode") & "")
            cmbnontaxhsn.Text = Trim(dt(0)("taxablenonHsnCode") & "")
            If Val(dt(0)("taxableAmount1") & "") = 0 Then dt(0)("taxableAmount1") = 0
            txttaxable1.Text = Format(CDbl(dt(0)("taxableAmount1")), numFormat)
            cmbhsn1.Text = Trim(dt(0)("taxableHsnCode1") & "")
            txtamount.Tag = dt(0)("id")
        End If
    End Sub
    Private Sub saveLodgeSettings()
        If Val(txtamount.Tag) > 0 Then
            _objcmnbLayer._saveDatawithOutParm("Update LodgeSettingsTb set taxableAmount=" & CDbl(txtamount.Text) & _
                                               ",taxableHsnCode='" & cmbtaxhsn.Text & "',taxablenonHsnCode='" & cmbnontaxhsn.Text & "'" & _
                                               ",taxableHsnCode1='" & cmbtaxhsn.Text & "',taxableAmount1=" & CDbl(txttaxable1.Text))
        Else
            _objcmnbLayer._saveDatawithOutParm("Insert into LodgeSettingsTb(taxableAmount,taxableHsnCode,taxablenonHsnCode,taxableAmount1,taxableHsnCode1) values(" & _
                                               CDbl(txtamount.Text) & ",'" & cmbtaxhsn.Text & "','" & cmbnontaxhsn.Text & "'," & CDbl(txttaxable1.Text) & ",'" & cmbhsn1.Text & "')")
        End If
        MsgBox("Updated", MsgBoxStyle.Information)
        loadLodgesettings()
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        saveLodgeSettings()
    End Sub

    Private Sub BtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        Me.Close()
    End Sub

    Private Sub txtamount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtamount.KeyPress
        NumericTextOnKeypress(txtamount, e, chgbyprg, numFormat)
    End Sub

    Private Sub txttaxable1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txttaxable1.KeyPress
        NumericTextOnKeypress(sender, e, chgbyprg, numFormat)
    End Sub

End Class