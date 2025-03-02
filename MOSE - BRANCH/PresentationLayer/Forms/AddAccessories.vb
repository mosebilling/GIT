Public Class AddAccessories
#Region "Class Objects"
    Private _objAccessories As clsJob
    Private _objcmnbLayer As clsCommon_BL
#End Region
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub ldAccessories()
        Dim ListTb As DataTable
        _objcmnbLayer = New clsCommon_BL
        ListTb = _objcmnbLayer._fldDatatable("SELECT * FROM AccessoriesTb")
        With lstContent
            .Items.Clear()
            If ListTb.Rows.Count > 0 Then
                For i = 0 To ListTb.Rows.Count - 1
                    .Items.Add(ListTb(i)("AccessoriesName"))
                    If .Items.Item(i).SubItems.Count > 1 Then
                        .Items.Item(i).SubItems(1).Text = .Items.Add(ListTb(i)("AccId"))
                    Else
                        .Items.Item(i).SubItems.Insert(1, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, ListTb(i)("AccId")))
                    End If
                Next
            End If
        End With
    End Sub

    Private Sub lstContent_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstContent.DoubleClick
        txtname.Tag = Val(lstContent.SelectedItems(0).SubItems(1).Text)
        txtname.Text = lstContent.SelectedItems(0).Text
        txtname.Focus()
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        If txtname.Text = "" Then
            MsgBox("Invalid Name", MsgBoxStyle.Exclamation)
            txtname.Focus()
            Exit Sub
        End If
        _objAccessories = New clsJob
        _objAccessories.AccessoriesName = txtname.Text
        _objAccessories.Alid = Val(txtname.Tag)
        _objAccessories.saveAccessoriesTb()
        txtname.Text = ""
        txtname.Tag = ""
        ldAccessories()
        txtname.Focus()
    End Sub

    Private Sub AddAccessories_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtname.Focus()
    End Sub

    Private Sub AddAccessories_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ldAccessories()
    End Sub

    Private Sub txtname_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtname.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnupdate_Click(btnupdate, New System.EventArgs)
        End If
    End Sub

    Private Sub txtname_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtname.TextChanged

    End Sub
End Class