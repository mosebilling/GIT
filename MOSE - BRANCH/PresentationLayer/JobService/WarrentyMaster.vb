Public Class WarrentyMaster
#Region "Class Objects"
    Private _objcmnbLayer As New clsCommon_BL
#End Region
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub txtname_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtname.KeyDown

    End Sub

    Private Sub txtname_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtname.TextChanged

    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        If txtname.Text = "" Then
            MsgBox("Invalid Warranty Name!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("select Wid from  WarrentyMasterTb where WarrentyName='" & txtname.Text & "'")
        If dt.Rows.Count > 0 Then
            If dt(0)("Wid") <> Val(txtname.Tag) Then
                MsgBox("Warrenty Name already exist", MsgBoxStyle.Exclamation)
                txtname.Focus()
                Exit Sub
            End If
        End If
        If Val(txtname.Tag) > 0 Then
            _objcmnbLayer._saveDatawithOutParm("Update WarrentyMasterTb set WarrentyName='" & txtname.Text & "' where Wid=" & Val(txtname.Tag))
        Else
            _objcmnbLayer._saveDatawithOutParm("Insert into WarrentyMasterTb (WarrentyName) values('" & txtname.Text & "')")
        End If
        ldWarrenty()
        txtname.Text = ""
        txtname.Tag = 0
        txtname.Focus()
    End Sub
    Private Sub ldWarrenty()
        Dim ListTb As DataTable
        ListTb = _objcmnbLayer._fldDatatable("SELECT WarrentyName,Wid FROM WarrentyMasterTb")
        dtwarrenty = ListTb
        iswarrentyAdded = True
        With lstContent
            .Items.Clear()
            If ListTb.Rows.Count > 0 Then
                For i = 0 To ListTb.Rows.Count - 1
                    .Items.Add(ListTb(i)("WarrentyName"))
                    If .Items.Item(i).SubItems.Count > 1 Then
                        .Items.Item(i).SubItems(1).Text = .Items.Add(ListTb(i)("Wid"))
                    Else
                        .Items.Item(i).SubItems.Insert(1, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, ListTb(i)("Wid")))
                    End If
                Next
            End If
        End With
    End Sub

    Private Sub lstContent_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstContent.DoubleClick
        txtname.Tag = Val(lstContent.SelectedItems(0).SubItems(1).Text)
        txtname.Text = lstContent.SelectedItems(0).SubItems(0).Text
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        If MsgBox("Do you want to remove this warranty?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        If lstContent.SelectedItems.Count = 0 Then Exit Sub
        _objcmnbLayer._saveDatawithOutParm("DELETE FROM WarrentyMasterTb where Wid=" & Val(lstContent.SelectedItems(0).SubItems(1).Text))
        txtname.Text = ""
        txtname.Tag = 0
        ldWarrenty()
    End Sub

    Private Sub WarrentyMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ldWarrenty()
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        txtname.Text = ""
        txtname.Tag = 0
        ldWarrenty()
    End Sub

    Private Sub lstContent_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstContent.SelectedIndexChanged

    End Sub
End Class