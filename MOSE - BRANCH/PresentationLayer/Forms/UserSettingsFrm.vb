Imports System.IO
Public Class UserSettingsFrm
#Region "Class Object Declaration"
    Dim _objcmnbLayer As New clsCommon_BL
#End Region
#Region "Form Object Declaration"
    Private WithEvents fLogin As AutherizedLogin
#End Region
#Region "Local Variables"
    Private Userid As Integer
    Private ProtectedDate As Date
    Private chgpgm As Boolean
    Private type As Integer 'for to specify remove permissions before save the same 'Using in savePermission proc.'
    Private dt_CopyUserDetails As DataTable
    Private ismodi As Boolean
#End Region
    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Dispose()
    End Sub

    Private Sub cmdCreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCreate.Click
        Try
            If nullValueChecking() = True Then Exit Sub
            If txtUsername.Text.Length > 10 Then
                MsgBox("User name should lessthan 10 charectors", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            If txtpassword.Text.Length > 10 Then
                MsgBox("Password should lessthan 10 charectors", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            saveUser()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Sub saveUser()
        If txtpassword.Text <> txtretype.Text Then MsgBox("Password confirmation was failed", MsgBoxStyle.Exclamation) : Exit Sub
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT id FROM UserTb WHERE UserId='" & txtUsername.Text & "'")
        If dt.Rows.Count > 0 Then
            If Userid <> dt(0)("id") Then
                MsgBox("User name already exist", MsgBoxStyle.Critical)
                Exit Sub
            End If
        End If

        Try
            If Userid = 0 Then
                _objcmnbLayer._saveDatawithOutParm("INSERT INTO UserTb (UserId,Password,MasterYN,DefLoc,LstOpnBr,documentPath,usercode,isreception,userCounter,maxsalesDiscPercentage) VALUES ('" & txtUsername.Text & "','" & _
                                                   txtpassword.Text & "'," & IIf(rdomaster.Checked = True, "'True'", "'False'") & ",'" & _
                                                   cmbLocation.Text & "','" & cmbbranch.Text & "','" & txtDocumentPath.Text & "','" & txtusercode.Text & "'," & IIf(chkReception.Checked, 1, 0) & ",'" & cmbcounter.Text & "'," & Val(txtsalesdiscount.Text) & ")")

                dt = _objcmnbLayer._fldDatatable("SELECT id FROM UserTb WHERE UserId='" & txtUsername.Text & "'")
                If dt.Rows.Count > 0 Then
                    Userid = dt(0)("id")
                End If
            Else
                _objcmnbLayer._saveDatawithOutParm("UPDATE UserTb SET UserId='" & txtUsername.Text & "',Password='" & txtpassword.Text & "',MasterYN=" & IIf(rdomaster.Checked = True, "'True'", "'False'") & ",DefLoc='" & cmbLocation.Text & "',LstOpnBr='" & _
                                                   cmbbranch.Text & "',documentPath='" & txtDocumentPath.Text & "',usercode='" & txtusercode.Text & "',isreception=" & IIf(chkReception.Checked, 1, 0) & ",userCounter='" & cmbcounter.Text & "',maxsalesDiscPercentage=" & Val(txtsalesdiscount.Text) & " WHERE id=" & Userid)

            End If
            _objcmnbLayer._saveDatawithOutParm("DELETE FROM UsrBr WHERE UsrId=" & Userid)
            For i = 0 To chklst.Items.Count - 1
                With chklst
                    If .GetItemChecked(i) Then
                        _objcmnbLayer._saveDatawithOutParm("INSERT INTO UsrBr (UsrId,BrId) Values(" & Userid & ",'" & chklst.Items(i).ToString & "')")
                    End If
                End With
            Next
            MsgBox("User has been created sucessfully", MsgBoxStyle.Information)
            ClearValues()
        Catch ex As SqlClient.SqlException
            If ex.Number = 2627 Then
                MsgBox("User Name Already Exist", MsgBoxStyle.Critical)
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Function nullValueChecking() As Boolean
        'If txtFullname.Text = "" Then MsgBox("Please fill up Full name!", MsgBoxStyle.Exclamation) : nullValueChecking = True : txtFullname.Focus() : Exit Function
        If txtUsername.Text = "" Then MsgBox("Please fill up User name!", MsgBoxStyle.Exclamation) : nullValueChecking = True : txtUsername.Focus() : Exit Function
        If txtpassword.Text = "" Then MsgBox("Please fill up Password!", MsgBoxStyle.Exclamation) : nullValueChecking = True : txtpassword.Focus() : Exit Function
        If txtretype.Text = "" Then MsgBox("Please confirm your Password!", MsgBoxStyle.Exclamation) : nullValueChecking = True : txtretype.Focus() : Exit Function
    End Function
    Sub ClearValues()
        'txtFullname.Clear()
        txtpassword.Clear()
        txtUsername.Clear()
        txtretype.Clear()
        txtDocumentPath.Clear()
        cmbbranch.Text = ""
        Userid = 0
        txtpassword.Enabled = True
        txtretype.Enabled = True
        selectAllUserbyType()
        'cmdCreate.Text = "Create"
        GroupBox1.Enabled = False
        btnBrowseDocFolder.Enabled = False
        txtDocumentPath.Enabled = False
        cmdmodi.Enabled = True
        cmdremove.Enabled = False
        cmdCreate.Enabled = False
        cmdadd.Enabled = True
        cmdadd.Text = "&Add"
        Panel3.Enabled = True
        cmdmodi.Text = "&Modify"
        cmbcounter.Text = ""
        txtsalesdiscount.Text = "0.00"
        For j = 0 To chklst.Items.Count - 1
            chklst.SetItemChecked(j, False)
        Next
        cmbLocation.Text = ""
        txtusercode.Text = ""
        chkReception.Checked = False
    End Sub
    Sub selectAllUser()
        Dim dtUser As DataTable
        Try
            dtUser = _objcmnbLayer._fldDatatable("SELECT * FROM UserTb where masterYN='False'")
            cmbuser.Items.Clear()
            For i = 0 To dtUser.Rows.Count - 1
                cmbuser.Items.Add(dtUser(i)("UserId"))
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub selectAllUserbyType()
        Dim dtUser As DataTable
        Try
            dtUser = _objcmnbLayer._fldDatatable("SELECT * FROM UserTb where masterYN='" & IIf(cmbType.SelectedIndex = 0, "True", "False") & "'")
            lstUser.Items.Clear()
            'cmbuser.Items.Clear()
            For i = 0 To dtUser.Rows.Count - 1
                lstUser.Items.Add(dtUser(i)("UserId"))
                'cmbuser.Items.Add(dtUser(i)("UserId"))
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub UserSettingsFrm_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        'If networkErr = True Then
        '    MsgBox("Server connection was faild, you must restart the software", MsgBoxStyle.Exclamation)
        '    End
        'End If
    End Sub

    Private Sub UserSettingsFrm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        'If networkErr = True Then
        '    MsgBox("Server connection was faild, you must restart the software", MsgBoxStyle.Exclamation)
        '    End
        'End If
    End Sub

    Private Sub UserSettingsFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call ClearValues()
        If UCase(password) = "VINVISGRP" Then
            cmdprotect.Visible = True
        Else
            cmdprotect.Visible = False
        End If
        ldBranch()
        'ldLocation()
        Permission()

        cmbType.SelectedIndex = 1
    End Sub
    Sub selectedUser()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT * FROM UserTb WHERE UserId='" & lstUser.SelectedItem & "'")
        If TabControl1.SelectedIndex = 0 Then
            If dt.Rows.Count > 0 Then
                txtUsername.Text = dt(0)("UserId")
                txtpassword.Text = dt(0)("password")
                cmbLocation.Text = Trim(dt(0)("DefLoc") & "")
                cmbbranch.Text = Trim(dt(0)("LstOpnBr") & "")
                txtDocumentPath.Text = Trim(dt(0)("documentPath") & "")
                txtusercode.Text = Trim(dt(0)("usercode") & "")
                chkReception.Checked = IIf(Val(dt(0)("isreception") & "") = 0, False, True)
                cmbcounter.Text = Trim(dt(0)("userCounter") & "")
                txtsalesdiscount.Text = Format(Val(dt(0)("maxsalesDiscPercentage") & ""), numFormat)
                Userid = dt(0)("id")
                If dt(0)("MasterYN") = True Then
                    'cmbType.SelectedIndex = 0
                    rdomaster.Checked = True
                Else
                    rdouser.Checked = True
                End If
            Else
                txtUsername.Text = ""
                txtpassword.Text = ""
                cmbLocation.Text = ""
                cmbbranch.Text = ""
                txtusercode.Text = ""
                chkReception.Checked = False
            End If
            setBranch()
            dt.Dispose()
            txtretype.Text = txtpassword.Text
        Else
            txtUsername.Text = ""
            txtpassword.Text = ""
            cmbLocation.Text = ""
            cmbbranch.Text = ""
            txtusercode.Text = ""
            chkReception.Checked = False
        End If

    End Sub


    Private Sub cmdremove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdremove.Click
        If Not fLogin Is Nothing Then fLogin.Close() : fLogin = Nothing
        fLogin = New AutherizedLogin
        fLogin.isremove = True
        fLogin.ShowDialog()
        If Userid > 0 Then
            If MsgBox("Are you sure to delete the user from the list ! if you are click on the Yes it will be remved permenently", MsgBoxStyle.Information + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                _objcmnbLayer._saveDatawithOutParm("DELETE FROM UserTb WHERE id=" & Userid)
                MsgBox("Item removed from list", MsgBoxStyle.Information)
            End If
        Else
            MsgBox("Invalid User", MsgBoxStyle.Critical)
        End If
        Call ClearValues()
    End Sub
    Sub LoadPermission(ByVal type As Boolean)
        TvPermission.Nodes.Clear()
        Dim dt As DataTable
        Dim dtTree As New DataTable
        If type = False Then
            dt = _objcmnbLayer._fldDatatable("select * from Rights INNER JOIN RightNode ON Rights.id=RightNode.NodeId where UId=" & Userid & " order by parentid,ordno")
        Else
            dt = _objcmnbLayer._fldDatatable("select * from RightNode order by parentid,ordno")
        End If
        If dtTree.Columns.Count = 0 Then
            dtTree.Columns.Add("ID", GetType(Integer))
            dtTree.Columns.Add("Name", GetType(String))
            dtTree.Columns.Add("IDParent", GetType(Integer))
        End If
        For i = 0 To dt.Rows.Count - 1
            dtTree.Rows.Add(dt(i)("NodeId"), dt(i)("Description"), IIf(dt(i)("ParentId") = 0, DBNull.Value, dt(i)("ParentId")))
        Next
        AddNodes(TvPermission.Nodes, dtTree.Select("ISNULL(IDParent, -1) = -1"), dtTree)
        'TvPermission.ExpandAll()
        dtTree.Clear()
    End Sub


    Public Function savePermission(ByVal _nodeCollection As TreeNodeCollection) As TreeNode
        Dim tmpNode As TreeNode
        Dim T As New TreeNode
        For Each _child As TreeNode In _nodeCollection
            If _child.Checked = True Then
                _SaveOrRemoveRights(cmbuser.Text, _child.Text, type)
                type = 2
            End If
            tmpNode = savePermission(_child.Nodes)
        Next
        Return Nothing
    End Function

    Private Sub cmdApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdApply.Click
        type = 1
        lblstatus.Visible = True
        lblstatus.Text = "Permission Updating.. Please wait!"
        lblstatus.Refresh()
        Me.Cursor = Cursors.WaitCursor
        Dim UserId As Integer
        UserId = _selecetUserid(cmbuser.Text)
        If type = 1 Then
            _objcmnbLayer._saveDatawithOutParm("Delete from Rights where UId=" & UserId)
        End If
        savePermission(TvPermission.Nodes)
        type = 1
        Me.Cursor = Cursors.Arrow
        lblstatus.Text = ""
        MsgBox("Prmissions have been set for the user", MsgBoxStyle.Information)
    End Sub
    Public Function CheckNode(ByVal _nodeCollection As TreeNodeCollection, ByVal name As String) As TreeNode
        Dim tmpNode As TreeNode
        Dim T As New TreeNode
        For Each _child As TreeNode In _nodeCollection
            If _child.Text = name Then
                _child.Checked = True
                Return Nothing
            End If
            tmpNode = CheckNode(_child.Nodes, name)
        Next
        Return Nothing
    End Function

    Private Sub TvPermission_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TvPermission.AfterCheck
        If chgpgm = True Then Exit Sub
        Dim currentNode As TreeNode
        For Each currentNode In e.Node.Nodes
            currentNode.Checked = e.Node.Checked
        Next
    End Sub

    Private Sub cmdadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdadd.Click
        ClearValues()
        If cmdadd.Text = "&Add" Then
            GroupBox1.Enabled = True
            Panel3.Enabled = False
            cmdremove.Enabled = False
            cmdCreate.Enabled = True
            cmdadd.Text = "Un&do"
            ismodi = False
            rdouser.Checked = True
            If Not fLogin Is Nothing Then fLogin.Close() : fLogin = Nothing
            fLogin = New AutherizedLogin
            fLogin.ShowDialog()
            txtUsername.Focus()
        Else
            GroupBox1.Enabled = False
            cmdremove.Enabled = False
            cmdCreate.Enabled = False
            cmdadd.Text = "&Add"
            Panel3.Enabled = True
        End If

    End Sub

    Private Sub cmdmodi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdmodi.Click
        If cmdmodi.Text = "&Modify" Then
            If Userid = 0 Then MsgBox("Select User Name!", MsgBoxStyle.Exclamation) : Exit Sub
            GroupBox1.Enabled = True
            txtDocumentPath.Enabled = True
            btnBrowseDocFolder.Enabled = True
            Panel3.Enabled = True
            cmdadd.Enabled = False
            cmdCreate.Enabled = True
            cmdmodi.Text = "Un&do"
            ismodi = True
            If Not fLogin Is Nothing Then fLogin.Close() : fLogin = Nothing
            fLogin = New AutherizedLogin
            fLogin.ShowDialog()
        Else
            GroupBox1.Enabled = False
            cmdadd.Enabled = True
            cmdremove.Enabled = False
            cmdCreate.Enabled = False
            cmdmodi.Text = "&Modify"
            ismodi = False
        End If
    End Sub

    Private Sub txtUsername_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtUsername.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtpassword_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtpassword.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtretype_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtretype.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub cmbType_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbType.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Public Function _selecetUserid(ByVal username As String) As Integer
        Try
            Dim dt As DataTable
            dt = _objcmnbLayer._fldDatatable("SELECT id FROM UserTb WHERE UserId='" & username & "'")
            If dt.Rows.Count > 0 Then
                Return dt(0)("id")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function
    Public Function _RightsSelectByCodition(ByVal Username As String, ByVal grp As Integer, ByVal type As Integer) As DataTable
        _RightsSelectByCodition = Nothing
        Try
            Dim UserId As Integer
            UserId = _selecetUserid(Username)
            Return _objcmnbLayer._fldDatatable("select * from Rights_tb INNER JOIN RightNode ON Rights.NodeId=RightNode.NodeId where UId=" & UserId & "  order by NodeId")

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function
    Sub Permission()
        'Dim drPer As DataTable
        'If UCase(password) = "SEFWANAPFT" Or userType = 1 Then Exit Sub
        'drPer = _RightsSelectByCodition(CurrentUser, 2, 3)
        'GroupBox1.Enabled = False
        'cmdadd.Enabled = False
        'cmdmodi.Enabled = False
        'cmdremove.Enabled = False
        'cmdCreate.Enabled = False
        'Panel2.Enabled = False
        'Panel3.Enabled = False
        'Panel4.Enabled = False
        'For i = 0 To drPer.Rows.Count - 1
        '    With drPer
        '        If drPer(i)("perid") = 18 Then
        '            cmdadd.Enabled = True
        '            cmdmodi.Enabled = True
        '            cmdremove.Enabled = True
        '            cmdCreate.Enabled = True
        '        ElseIf drPer(i)("perid") = 19 Then
        '            Panel2.Enabled = True
        '            'ElseIf drPer(i)("perid") = 32 Then
        '            '    Panel3.Enabled = True
        '        End If
        '    End With
        'Next
    End Sub
    Private Sub _SaveOrRemoveRights(ByVal username As String, ByVal perName As String, ByVal type As Integer)
        Try
            Dim dt As DataTable
            dt = _objcmnbLayer._fldDatatable("select * from RightNode where Description='" & perName & "'")
            Dim Rid As Integer
            Dim ismenu As Boolean
            If dt.Rows.Count > 0 Then
                Rid = dt(0)("NodeId")
                If IsDBNull(dt(0)("IsMenu")) Then
                    ismenu = False
                Else
                    ismenu = dt(0)("IsMenu")
                End If
            End If
            Dim UserId As Integer
            UserId = _selecetUserid(cmbuser.Text)
            _objcmnbLayer._saveDatawithOutParm("INSERT INTO Rights values(" & UserId & " ," & Rid & ",'" & ismenu & "')")
            dt.Dispose()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ldBranch()
        Try
            Dim dtTemp As New DataTable
            chklst.Items.Clear()
            cmbbranch.Items.Clear()
            cmbbranch.Items.Add("")
            dtTemp = _objcmnbLayer._fldDatatable("SELECT Branchcode FROM BranchTb")
            With chklst
                For i = 0 To dtTemp.Rows.Count - 1
                    .Items.Add(dtTemp(i)(0))
                    cmbbranch.Items.Add(dtTemp(i)(0))
                Next
            End With
            dtTemp.Dispose()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ldLocation()
        Try
            Dim dtTemp As New DataTable
            cmbLocation.Items.Clear()
            dtTemp = _objcmnbLayer._fldDatatable("SELECT LocationId FROM LocationTb")
            cmbLocation.Items.Add("")
            With cmbLocation
                For i = 0 To dtTemp.Rows.Count - 1
                    .Items.Add(dtTemp(i)(0))
                Next
            End With
            dtTemp.Dispose()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub setBranch()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT * FROM UsrBr WHERE UsrId=" & Userid)
        For j = 0 To chklst.Items.Count - 1
            chklst.SetItemChecked(j, False)
        Next
        For i = 0 To dt.Rows.Count - 1
            For j = 0 To chklst.Items.Count - 1
                If dt(i)(1) = chklst.Items(j).ToString Then
                    chklst.SetItemChecked(j, True)
                    Exit For
                End If
            Next
        Next
        dt.Dispose()
    End Sub



    Private Sub fLogin_CloseLogin(ByVal usrType As Integer, ByVal usrId As Integer) Handles fLogin.CloseLogin
        If fLogin.isremove Then
            If Val(fLogin.btncancel.Tag) = 1 Then
                GroupBox1.Enabled = False
                cmdremove.Enabled = False
                cmdCreate.Enabled = False
                cmdadd.Text = "&Add"
                Userid = 0
                Panel3.Enabled = True
                ClearValues()
                Exit Sub
            End If
            If usrType = 0 And cmbType.SelectedIndex = 1 Then
msg:
                MsgBox("Administrator can only create or modify user master", MsgBoxStyle.Exclamation)
                If ismodi Then
                    GroupBox1.Enabled = False
                    cmdadd.Enabled = True
                    cmdremove.Enabled = False
                    cmdCreate.Enabled = False
                    cmdmodi.Text = "&Modify"
                    ismodi = False
                Else
                    GroupBox1.Enabled = False
                    cmdremove.Enabled = False
                    cmdCreate.Enabled = False
                    cmdadd.Text = "&Add"
                    Panel3.Enabled = True
                End If
                ClearValues()
                Exit Sub
            ElseIf usrType <> 2 And cmbType.SelectedIndex = 0 Then
                GoTo msg
            Else
                cmdremove.Tag = 1
            End If
        Else
            cmdremove.Tag = ""
            If Val(fLogin.btncancel.Tag) = 1 Then
                If ismodi Then
                    GroupBox1.Enabled = False
                    cmdadd.Enabled = True
                    cmdremove.Enabled = False
                    cmdCreate.Enabled = False
                    cmdmodi.Text = "&Modify"
                    ismodi = False
                Else
                    GroupBox1.Enabled = False
                    cmdremove.Enabled = False
                    cmdCreate.Enabled = False
                    cmdadd.Text = "&Add"
                    Panel3.Enabled = True
                End If
                ClearValues()
                Exit Sub
            End If
            If usrType = 0 Then
                MsgBox("Administrator can only create or modify user master", MsgBoxStyle.Exclamation)
                If ismodi Then
                    GroupBox1.Enabled = False
                    cmdadd.Enabled = True
                    cmdremove.Enabled = False
                    cmdCreate.Enabled = False
                    cmdmodi.Text = "&Modify"
                    ismodi = False
                Else
                    GroupBox1.Enabled = False
                    cmdremove.Enabled = False
                    cmdCreate.Enabled = False
                    cmdadd.Text = "&Add"
                    Panel3.Enabled = True
                End If
                ClearValues()
                Exit Sub
            ElseIf usrType = 1 Then
                If cmbType.SelectedIndex = 0 Then
                    MsgBox("Vendor can only create or modify Administrator User!", MsgBoxStyle.Exclamation)
                    If ismodi Then
                        GroupBox1.Enabled = False
                        cmdadd.Enabled = True
                        cmdremove.Enabled = False
                        cmdCreate.Enabled = False
                        cmdmodi.Text = "&Modify"
                        ismodi = False
                    Else
                        GroupBox1.Enabled = False
                        cmdremove.Enabled = False
                        cmdCreate.Enabled = False
                        cmdadd.Text = "&Add"
                        Panel3.Enabled = True
                        ClearValues()
                    End If
                    Exit Sub
                End If
                rdomaster.Enabled = False
                rdomaster.Checked = False
                rdouser.Checked = True
            Else
                rdomaster.Enabled = True
            End If
            'rdouser.Checked = True
            txtUsername.Focus()
        End If

    End Sub

    Private Sub fLogin_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fLogin.FormClosed
        fLogin = Nothing
    End Sub

    Private Sub cmbType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbType.SelectedIndexChanged
        selectAllUserbyType()
    End Sub

    Private Sub lstUser_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstUser.SelectedIndexChanged
        selectedUser()
        'Dim dt As DataTable
        'LoadPermission(True)
        'dt = _objcmnbLayer._fldDatatable("select * from Rights INNER JOIN UserTb ON Rights.Uid = UserTb.Id INNER JOIN RightNode ON Rights.NodeId=RightNode.NodeId where UserId='" & lbluser.Text & "' order by Parentid,ordno")
        'chgpgm = True
        'For i = 0 To dt.Rows.Count - 1
        '    CheckNode(TvPermission.Nodes, dt(i)("Description"))
        'Next
        'dt.Dispose()
        chgpgm = False
        cmdremove.Enabled = True
    End Sub

    Private Sub txtretype_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtretype.TextChanged

    End Sub

    Private Sub btnBrowseDocFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseDocFolder.Click
        Try
            Dim MyFolderBrowser As New System.Windows.Forms.FolderBrowserDialog

            ' Descriptive text displayed above the tree view control in the dialog box
            MyFolderBrowser.Description = "Select the Folder"

            'Sets the root folder where the browsing starts from
            'MyFolderBrowser.RootFolder = Environment.SpecialFolder.MyDocuments
            'MyFolderBrowser.RootFolder = Environment.SpecialFolder.ApplicationData

            ' Do not show the button for new folder
            MyFolderBrowser.ShowNewFolderButton = False

            Dim dlgResult As DialogResult = MyFolderBrowser.ShowDialog()

            If dlgResult = Windows.Forms.DialogResult.OK Then
                txtDocumentPath.Text = MyFolderBrowser.SelectedPath
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy.Click
        Dim UserId As Integer

        If (dt_CopyUserDetails Is Nothing) Then
        Else

            dt_CopyUserDetails.Rows.Clear()
        End If
        UserId = _selecetUserid(cmbuser.Text)
        Get_User_Rights(UserId)
    End Sub
    Private Sub Get_User_Rights(ByVal Us_Id As Integer)
        dt_CopyUserDetails = _objcmnbLayer._fldDatatable("Select * from RIGHTS where UId=" & Us_Id & "")
    End Sub

    Private Sub btnpaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpaste.Click

        Dim Node_Ide As Integer
        Dim _rightyn As Integer

        Userid = _selecetUserid(cmbuser.Text)

        _objcmnbLayer._saveDatawithOutParm("Delete from Rights where UId=" & Userid)

        If (dt_CopyUserDetails.Rows.Count > 0) Then
            For i = 0 To dt_CopyUserDetails.Rows.Count - 1
                Node_Ide = Val(dt_CopyUserDetails.Rows(i)(1))
                _rightyn = Val(dt_CopyUserDetails.Rows(i)(2))
                _objcmnbLayer._saveDatawithOutParm("INSERT INTO Rights values(" & Userid & " ," & Node_Ide & "," & _rightyn & ")")
            Next
            dt_CopyUserDetails.Rows.Clear() ' 
            MsgBox("Prmissions have been set for the user", MsgBoxStyle.Information)
            If (lstUser.SelectedItems.Count > 0) Then
                Permission_Settings()
            Else

            End If

        End If
    End Sub
    Private Sub Permission_Settings()
        Dim dt As DataTable
        LoadPermission(True)
        dt = _objcmnbLayer._fldDatatable("select * from Rights INNER JOIN UserTb ON Rights.Uid = UserTb.Id INNER JOIN RightNode ON Rights.NodeId=RightNode.NodeId where UserId='" & cmbuser.Text & "' order by Parentid,ordno")
        chgpgm = True
        For i = 0 To dt.Rows.Count - 1
            CheckNode(TvPermission.Nodes, dt(i)("Description"))
        Next
        dt.Dispose()
        chgpgm = False
        cmdremove.Enabled = True
    End Sub

    Private Sub TabControl1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.Click
        If TabControl1.SelectedIndex = 1 Then
            selectAllUser()
        End If
    End Sub

    Private Sub cmbuser_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbuser.SelectedIndexChanged
        Permission_Settings()
    End Sub

    Private Sub txtsalesdiscount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtsalesdiscount.KeyPress
        NumericTextOnKeypress(txtsalesdiscount, e, chgpgm, numFormat)
    End Sub

End Class