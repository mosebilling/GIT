Public Class StateCodeCreation
#Region "Class Objects"
    Private _objcmnbLayer As New clsCommon_BL
#End Region
    Private Sub BankCodeCreation_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtCode.Focus()
    End Sub

    Private Sub BankCodeCreation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If enableGCC Then
            lblcap.Text = "Emirate Master"
        End If
        ldStatecode()
    End Sub
    Private Sub ldStatecode()
        Dim ListTb As DataTable
        ListTb = _objcmnbLayer._fldDatatable("Select * from StateMasterTb")
        With lstContent
            .Items.Clear()
            If ListTb.Rows.Count > 0 Then
                For i = 0 To ListTb.Rows.Count - 1
                    .Items.Add(ListTb(i)("statecode"))
                    If .Items.Item(i).SubItems.Count > 1 Then
                        .Items.Item(i).SubItems(1).Text = .Items.Add(ListTb(i)("statename"))
                    Else
                        .Items.Item(i).SubItems.Insert(1, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, ListTb(i)("statename")))
                    End If
                Next
            End If
        End With
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        If txtCode.Text = "" Then
            MsgBox("Invalid State Code!", MsgBoxStyle.Exclamation)
            txtCode.Focus()
            Exit Sub
        End If
        Dim dt As DataTable
        _objcmnbLayer = New clsCommon_BL
        dt = _objcmnbLayer._fldDatatable("Select statecode from StateMasterTb where statecode='" & Trim(txtCode.Text) & "'")
        If dt.Rows.Count > 0 Then
            If dt(0)("statecode") <> txtCode.Tag Then
                MsgBox("State Code already exist", MsgBoxStyle.Exclamation)
                txtCode.Focus()
                Exit Sub
            End If
        End If
        If txtCode.Tag <> "" Then
            _objcmnbLayer._saveDatawithOutParm("UPDATE StateMasterTb SET statecode='" & Trim(MkDbSrchStr(txtCode.Text)) & "',statename='" & MkDbSrchStr(txtname.Text) & "' WHERE statecode='" & MkDbSrchStr(txtCode.Tag) & "'")
            _objcmnbLayer._saveDatawithOutParm("UPDATE AccMast SET CountryCode='" & Trim(MkDbSrchStr(txtCode.Text)) & "' WHERE CountryCode='" & MkDbSrchStr(txtCode.Tag) & "'")
            _objcmnbLayer._saveDatawithOutParm("UPDATE CompanyTb SET statecode='" & Trim(MkDbSrchStr(txtCode.Text)) & "' WHERE statecode='" & MkDbSrchStr(txtCode.Tag) & "'")
        Else
            _objcmnbLayer._saveDatawithOutParm("INSERT INTO StateMasterTb (statecode,statename)VALUES ('" & Trim(MkDbSrchStr(txtCode.Text)) & "','" & MkDbSrchStr(txtname.Text) & "')")
        End If
        MsgBox("Updated", MsgBoxStyle.Information)
        txtCode.Text = ""
        txtCode.Tag = ""
        txtname.Text = ""
        Dim sRs As DataTable
        sRs = _objcmnbLayer._fldDatatable("SELECT * FROM CompanyTb")
        If sRs.Rows.Count > 0 Then
            stateCode = Trim(sRs(0)("statecode") & "")
        End If
        ldStatecode()
    End Sub

    Private Sub lstContent_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstContent.DoubleClick
        txtCode.Text = lstContent.SelectedItems(0).SubItems(0).Text
        txtCode.Tag = lstContent.SelectedItems(0).SubItems(0).Text
        txtname.Text = lstContent.SelectedItems(0).SubItems(1).Text
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        If lstContent.SelectedItems.Count = 0 Then Exit Sub
        If MsgBox("Do you want to remove " & lstContent.SelectedItems(0).SubItems(1).Text, MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.No Then Exit Sub
        _objcmnbLayer = New clsCommon_BL
        _objcmnbLayer._saveDatawithOutParm("DELETE FROM StateMasterTb WHERE statecode='" & txtCode.Tag & "'")
        ldStatecode()
        txtCode.Text = ""
        txtCode.Tag = ""
        txtname.Text = ""
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub txtCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCode.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged

    End Sub
End Class