Public Class BranchFrm
    Private _objcmnbLayer As New clsCommon_BL
    Private _objBr As New clsBranch
    Private Sub BtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        Me.Close()
    End Sub

    Private Sub txtname_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtname.KeyDown, txtaddr1.KeyDown, txtaddr2.KeyDown, txtaddr3.KeyDown, _
                                                                                                                txtaddr4.KeyDown, txtphone.KeyDown, _
                                                                                                                txtlocation.KeyDown, txtbcode.KeyDown
        Dim ctrl As TextBox = sender
        If e.KeyCode = Keys.Return Then
            If ctrl.Name = "txtphone" Then
                txtlocation.Focus()
            Else
                SendKeys.Send("{TAB}")
            End If
        End If
    End Sub

    Private Sub BranchFrm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtbcode.Focus()
    End Sub

    Private Sub BranchFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loadAccountHeads()
        loadBranch()
    End Sub
    Private Sub loadAccountHeads()
        Dim ObjLocationlist As New List(Of String)
        ObjLocationlist = toLoadAutoFillListItems("LocCode", "LocationTb")
        toAssignDownListToText(txtlocation, ObjLocationlist)

        'SELECT AccMast.AccDescr As [AccountName],AccMast.Alias,BranchId,accid FROM AccMast WHERE AccMast.S1AccId Between 7000 And 7999
    End Sub
    Private Sub txtstock_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ctrl As TextBox = sender
        Dim dt As DataTable
        If ctrl.Text = "" Then Exit Sub
        dt = _objcmnbLayer._fldDatatable("SELECT Accid,AccDescr from AccMast where AccDescr='" & ctrl.Text & "'")
        If dt.Rows.Count > 0 Then
            ctrl.Text = dt(0)("AccDescr")
            ctrl.Tag = dt(0)("Accid")
        Else
            ctrl.Text = ""
            ctrl.Tag = ""
        End If

    End Sub
    Private Sub txtlocation_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtlocation.Validated
        If txtlocation.Text = "" Then Exit Sub
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT LocCode from LocationTb where LocCode='" & txtlocation.Text & "'")
        If dt.Rows.Count > 0 Then
            txtlocation.Text = dt(0)("LocCode")
        Else
            txtlocation.Text = ""
        End If
    End Sub
    Private Sub saveBranch()
        If txtbcode.Text = "" Then
            MsgBox("Invalid Code", MsgBoxStyle.Exclamation)
            txtbcode.Focus()
            Exit Sub
        End If
        If txtname.Text = "" Then
            MsgBox("Invalid Name", MsgBoxStyle.Exclamation)
            txtbcode.Focus()
            Exit Sub
        End If
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT BranchId from BranchTb where BranchId<>" & Val(txtbcode.Tag) & " and  Branchcode='" & txtbcode.Text & "'")
        If dt.Rows.Count > 0 Then
            MsgBox("Branch Code already exist", MsgBoxStyle.Exclamation)
            txtbcode.Focus()
            Exit Sub
        End If
        If txtlocation.Text = "" Then
            dt = _objcmnbLayer._fldDatatable("SELECT LocCode from LocationTb where LocCode='" & txtbcode.Text & "'")
            If dt.Rows.Count = 0 Then
                _objcmnbLayer._fldDatatable("Insert into LocationTb(LocCode,LocName) values('" & MkDbSrchStr(txtbcode.Text) & "','" & MkDbSrchStr(txtname.Text) & "')")
                txtlocation.Text = txtbcode.Text
            End If
        Else
            dt = _objcmnbLayer._fldDatatable("SELECT LocCode from LocationTb where LocCode='" & txtlocation.Text & "'")
            If dt.Rows.Count = 0 Then
                MsgBox("Invalid Location", MsgBoxStyle.Exclamation)
                txtlocation.Focus()
                Exit Sub
            End If
        End If
        
        With _objBr
            .BranchId = Val(txtbcode.Tag)
            .Branchcode = txtbcode.Text
            .BranchName = txtname.Text
            .CostOfSlHd = 0
            .StockHd = 0
            .CostDiff = 0
            .IsDefault = chkdefault.Checked
            .BrAdd1 = txtaddr1.Text
            .BrAdd2 = txtaddr2.Text
            .BrAdd3 = txtaddr3.Text
            .BrAdd4 = txtaddr4.Text
            .BrPhone = txtphone.Text
            .BrLocation = txtlocation.Text
            .saveBranch()
        End With
        MsgBox("Updated", MsgBoxStyle.Information)
        ClearControl()
        loadBranch()
    End Sub
    Private Sub loadBranch()
        Dim ListTb As DataTable
        ListTb = _objcmnbLayer._fldDatatable("Select Branchcode,BranchName,BrLocation from BranchTb")
        With lstContent
            .Items.Clear()
            If ListTb.Rows.Count > 0 Then
                For i = 0 To ListTb.Rows.Count - 1
                    .Items.Add(ListTb(i)("Branchcode"))
                    If .Items.Item(i).SubItems.Count > 1 Then
                        .Items.Item(i).SubItems(1).Text = .Items.Add(ListTb(i)("BranchName"))
                    Else
                        .Items.Item(i).SubItems.Insert(1, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, ListTb(i)("BranchName")))
                    End If
                    If .Items.Item(i).SubItems.Count > 2 Then
                        .Items.Item(i).SubItems(2).Text = ListTb(i)("BrLocation")
                    Else
                        .Items.Item(i).SubItems.Insert(2, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, ListTb(i)("BrLocation")))
                    End If
                Next
            End If
        End With
    End Sub

    Private Sub lstContent_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstContent.DoubleClick
        Dim dt As DataTable
        With _objBr
            .Branchcode = lstContent.SelectedItems(0).SubItems(0).Text
            dt = .loadBranch
        End With
        If dt.Rows.Count > 0 Then
            txtbcode.Text = dt(0)("Branchcode")
            txtbcode.Tag = dt(0)("BranchId")
            txtname.Text = dt(0)("BranchName")
            chkdefault.Checked = Val(dt(0)("IsDefault"))
            txtaddr1.Text = Trim(dt(0)("BrAdd1") & "")
            txtaddr2.Text = Trim(dt(0)("BrAdd2") & "")
            txtaddr3.Text = Trim(dt(0)("BrAdd3") & "")
            txtaddr4.Text = Trim(dt(0)("BrAdd4") & "")
            txtphone.Text = Trim(dt(0)("BrPhone") & "")
            txtlocation.Text = Trim(dt(0)("BrLocation") & "")
        End If
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        saveBranch()
    End Sub
    Private Sub ClearControl()
        txtbcode.Text = ""
        txtbcode.Tag = ""
        txtname.Text = ""
        chkdefault.Checked = False
        txtaddr1.Text = ""
        txtaddr2.Text = ""
        txtaddr3.Text = ""
        txtaddr4.Text = ""
        txtphone.Text = ""
        txtlocation.Text = ""
    End Sub
End Class