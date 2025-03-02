Public Class TunurMaster
#Region "Class Objects"
    Private _objcmnbLayer As New clsCommon_BL
#End Region
    Private Sub TunurMaster_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtCode.Focus()
    End Sub

    Private Sub TunurMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ldTunercode()
    End Sub
    
    Private Sub ldTunercode()
        Dim ListTb As DataTable
        ListTb = _objcmnbLayer._fldDatatable("Select tunername,interst,isnull(NoOfMnth,0)NoOfMnth from TunurTb")
        With lstContent
            .Items.Clear()
            If ListTb.Rows.Count > 0 Then
                For i = 0 To ListTb.Rows.Count - 1
                    .Items.Add(ListTb(i)("tunername"))
                    If .Items.Item(i).SubItems.Count > 1 Then
                        .Items.Item(i).SubItems(1).Text = .Items.Add(ListTb(i)("interst"))

                    Else
                        .Items.Item(i).SubItems.Insert(1, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, ListTb(i)("interst")))

                    End If
                    If .Items.Item(i).SubItems.Count > 2 Then
                        .Items.Item(i).SubItems(2).Text = .Items.Add(ListTb(i)("NoOfMnth"))

                    Else
                        .Items.Item(i).SubItems.Insert(2, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, ListTb(i)("NoOfMnth")))

                    End If
                Next
            End If
        End With
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        If txtCode.Text = "" Then
            MsgBox("Invalid Tuner Code!", MsgBoxStyle.Exclamation)
            txtCode.Focus()
            Exit Sub
        End If
        Dim dt As DataTable
        _objcmnbLayer = New clsCommon_BL
        dt = _objcmnbLayer._fldDatatable("Select tunername from TunurTb where tunername='" & txtCode.Text & "'")
        If dt.Rows.Count > 0 Then
            If dt(0)("tunername") <> txtCode.Tag Then
                MsgBox("Tunur already exist", MsgBoxStyle.Exclamation)
                txtCode.Focus()
                Exit Sub
            End If
        End If
        If txtCode.Tag <> "" Then
            _objcmnbLayer._saveDatawithOutParm("UPDATE TunurTb SET tunername='" & MkDbSrchStr(txtCode.Text) & "',interst='" & MkDbSrchStr(txtinterest.Text) & "',NoOfMnth='" & MkDbSrchStr(txtNoOfMonth.Text) & "' WHERE tunername='" & MkDbSrchStr(txtCode.Tag) & "'")
        Else
            _objcmnbLayer._saveDatawithOutParm("INSERT INTO TunurTb (tunername,interst,NoOfMnth)VALUES ('" & MkDbSrchStr(txtCode.Text) & "','" & MkDbSrchStr(txtinterest.Text) & "','" & MkDbSrchStr(txtNoOfMonth.Text) & "')")
        End If
        txtCode.Text = ""
        txtCode.Tag = ""
        txtinterest.Text = ""
        txtNoOfMonth.Text = ""
        ldTunercode()
    End Sub

    Private Sub lstContent_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstContent.DoubleClick
        txtCode.Text = lstContent.SelectedItems(0).SubItems(0).Text
        txtCode.Tag = lstContent.SelectedItems(0).SubItems(0).Text
        txtinterest.Text = lstContent.SelectedItems(0).SubItems(1).Text
        txtNoOfMonth.Text = lstContent.SelectedItems(0).SubItems(2).Text
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        If lstContent.SelectedItems.Count = 0 Then Exit Sub
        If MsgBox("Do you want to remove " & lstContent.SelectedItems(0).SubItems(1).Text, MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.No Then Exit Sub
        _objcmnbLayer = New clsCommon_BL
        _objcmnbLayer._saveDatawithOutParm("DELETE FROM TunurTb WHERE tunername='" & txtCode.Tag & "'")
        ldTunercode()
        txtCode.Text = ""
        txtCode.Tag = ""
        txtinterest.Text = ""
        txtNoOfMonth.Text = ""
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

    Private Sub lblcap_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblcap.Click

    End Sub

    Private Sub Panel7_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel7.Paint

    End Sub

    Private Sub lstContent_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstContent.SelectedIndexChanged

    End Sub
End Class