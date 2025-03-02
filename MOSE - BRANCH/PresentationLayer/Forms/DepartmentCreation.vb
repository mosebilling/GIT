Public Class DepartmentCreation
#Region "Class Objects"
    Private _objcmnbLayer As New clsCommon_BL
#End Region
    Public isClose As Boolean


    Private Sub ldDepartment()
        Dim ListTb As DataTable
        ListTb = _objcmnbLayer._fldDatatable("Select * from DepartmentTb")
        With lstContent
            .Items.Clear()
            If ListTb.Rows.Count > 0 Then
                For i = 0 To ListTb.Rows.Count - 1
                    .Items.Add(ListTb(i)("departmentname"))
                    If .Items.Item(i).SubItems.Count > 1 Then
                        .Items.Item(i).SubItems(1).Text = .Items.Add(ListTb(i)("departmentid"))
                    Else
                        .Items.Item(i).SubItems.Insert(1, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, ListTb(i)("departmentid")))
                    End If
                Next
            End If
        End With
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        saveRec()
    End Sub
    Private Sub saveRec()
        If txtname.Text = "" Then
            MsgBox("Invalid Designation!", MsgBoxStyle.Exclamation)
            txtname.Focus()
            Exit Sub
        End If
        Dim dt As DataTable
        _objcmnbLayer = New clsCommon_BL
        dt = _objcmnbLayer._fldDatatable("Select departmentname,departmentid from DepartmentTb where departmentname='" & Trim(txtname.Text) & "'")
        If dt.Rows.Count > 0 Then
            If dt(0)("departmentid") <> Val(txtname.Tag) Then
                MsgBox("Department already exist", MsgBoxStyle.Exclamation)
                txtname.Focus()
                Exit Sub
            End If
        End If
        If txtname.Tag <> "" Then
            _objcmnbLayer._saveDatawithOutParm("UPDATE DepartmentTb SET departmentname='" & Trim(MkDbSrchStr(txtname.Text)) & "' WHERE departmentid='" & Val(txtname.Tag) & "'")
        Else
            _objcmnbLayer._saveDatawithOutParm("INSERT INTO DepartmentTb (departmentname)VALUES ('" & Trim(MkDbSrchStr(txtname.Text)) & "')")
        End If
        MsgBox("Updated", MsgBoxStyle.Information)
        txtname.Text = ""
        txtname.Tag = ""
        txtname.Text = ""
        If isClose Then
            Me.Close()
            Exit Sub
        End If
        ldDepartment()
    End Sub

    Private Sub lstContent_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstContent.DoubleClick
        txtname.Text = lstContent.SelectedItems(0).SubItems(0).Text
        txtname.Tag = lstContent.SelectedItems(0).SubItems(0).Text
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        If lstContent.SelectedItems.Count = 0 Then Exit Sub
        If MsgBox("Do you want to remove " & lstContent.SelectedItems(0).SubItems(0).Text, MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.No Then Exit Sub
        _objcmnbLayer = New clsCommon_BL
        _objcmnbLayer._saveDatawithOutParm("DELETE FROM DepartmentTb WHERE departmentid=" & Val(txtname.Tag) & "'")
        ldDepartment()
        txtname.Text = ""
        txtname.Tag = ""
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub txtCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub DepartmentCreation_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtname.Focus()
    End Sub

    Private Sub DepartmentCreation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ldDepartment()
    End Sub

    Private Sub txtname_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtname.KeyDown
        If e.KeyCode = Keys.Return Then
            saveRec()
        End If
    End Sub

End Class