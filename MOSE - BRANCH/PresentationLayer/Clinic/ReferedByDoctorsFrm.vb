Public Class ReferedByDoctorsFrm
    Private _objcmnbLayer As New clsCommon_BL
    Public Event returnname(ByVal doctorname As String)
    Public isreturnname As Boolean
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub ReferedByDoctorsFrm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtname.Focus()
    End Sub

    Private Sub ReferedByDoctorsFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not userType Then
            btnupdate.Tag = 1
            btndelete.Tag = 1
        Else
            btnupdate.Tag = IIf(getRight(233, CurrentUser), 1, 0)
            btndelete.Tag = IIf(getRight(234, CurrentUser), 1, 0)
        End If
        loadreferences()
    End Sub
    Private Sub loadreferences()
        Try
            Dim ListTb As DataTable
            ListTb = _objcmnbLayer._fldDatatable("SELECT refDoctorname,refDocHospitalname, refPhone phone,refdocid FROM RefernceDoctorTb")
            With lstContent
                .Items.Clear()
                If ListTb.Rows.Count > 0 Then
                    For i = 0 To ListTb.Rows.Count - 1
                        .Items.Add(ListTb(i)("refDoctorname"))
                        If .Items.Item(i).SubItems.Count > 1 Then
                            .Items.Item(i).SubItems(1).Text = .Items.Add(ListTb(i)("refDocHospitalname"))
                        Else
                            .Items.Item(i).SubItems.Insert(1, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, ListTb(i)("refDocHospitalname")))
                        End If

                        If .Items.Item(i).SubItems.Count > 2 Then
                            .Items.Item(i).SubItems(2).Text = .Items.Add(ListTb(i)("phone"))
                        Else
                            .Items.Item(i).SubItems.Insert(2, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, ListTb(i)("phone")))
                        End If

                        If .Items.Item(i).SubItems.Count > 3 Then
                            .Items.Item(i).SubItems(3).Text = .Items.Add(ListTb(i)("refdocid"))
                        Else
                            .Items.Item(i).SubItems.Insert(3, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, ListTb(i)("refdocid")))
                        End If
                    Next
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        End Try

    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        If Val(btnupdate.Tag) = 0 Then
            MsgBox("This user do not have rights to update", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If txtname.Text = "" Then
            MsgBox("Invalid Name!", MsgBoxStyle.Exclamation)
            txtname.Focus()
            Exit Sub
        End If
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("select refdocid from  RefernceDoctorTb where refDoctorname='" & txtname.Text & "' AND refdocid<>" & Val(txtname.Tag))
        If dt.Rows.Count > 0 Then
            MsgBox("Name already exist", MsgBoxStyle.Exclamation)
            txtname.Focus()
            Exit Sub
        End If

        If Val(txtname.Tag) > 0 Then
            _objcmnbLayer._saveDatawithOutParm("Update RefernceDoctorTb set refDoctorname ='" & txtname.Text & "', refDocHospitalname='" & txthospital.Text & _
                                              "',refPhone='" & txtphone.Text & "' where refdocid=" & Val(txtname.Tag))
        Else
            _objcmnbLayer._saveDatawithOutParm("Insert into RefernceDoctorTb (refDoctorname,refDocHospitalname,refPhone) values('" & _
            txtname.Text & "','" & txthospital.Text & "','" & txtphone.Text & "')")
        End If
        If isreturnname Then
            MsgBox("Updated", MsgBoxStyle.Information)
            Me.Close()
            Exit Sub
        End If
        makeclear()
        loadreferences()
        MsgBox("Updated", MsgBoxStyle.Information)
    End Sub
    Private Sub makeclear()
        txtname.Text = ""
        txtname.Tag = ""
        txthospital.Text = ""
        txtphone.Text = ""
        txtname.Focus()
    End Sub

    Private Sub txtname_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtname.KeyDown, txthospital.KeyDown, txtphone.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub lstContent_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstContent.DoubleClick
        txtname.Text = lstContent.SelectedItems(0).SubItems(0).Text
        txthospital.Text = lstContent.SelectedItems(0).SubItems(1).Text
        txtphone.Text = lstContent.SelectedItems(0).SubItems(2).Text
        txtname.Tag = lstContent.SelectedItems(0).SubItems(3).Text
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        If Val(btndelete.Tag) = 0 Then
            MsgBox("This user do not have rights to delete", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If MsgBox("Are you sure to delete the record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        _objcmnbLayer._saveDatawithOutParm("DELETE FROM RefernceDoctorTb WHERE refdocid=" & Val(txtname.Tag))
        makeclear()
        loadreferences()
    End Sub
End Class