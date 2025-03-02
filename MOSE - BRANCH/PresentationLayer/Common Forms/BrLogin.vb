
Public Class BrLogin
    Dim _objCmnblayer As New clsCommon_BL
    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub

    Private Sub Login_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtUsername.Focus()
    End Sub
    Sub subUsername()
        Try

            'Dim dtUser As DataTable

            If txtUsername.Text = "" Then
                MsgBox("Invalid User name", MsgBoxStyle.Critical)
                txtUsername.Focus()
                Exit Sub
            End If
            'dtUser = _objCmnblayer._fldDatatable("SELECT * FROM UserTb WHERE UserId='" & txtUsername.Text & "'")

            'If dtUser.Rows.Count > 0 Then
            '    If Me.ActiveControl.Name = "txtUsername" Then
            '        txtUsername.Tag = dtUser(0)("id")
            '        txtUsername.Text = UCase(dtUser(0)("username"))
            '        dtPhotopath = dtUser(0)("documentPath")
            '        txtpassword.Focus()
            '        Exit Sub
            '    Else
            '        If UCase(txtpassword.Text) <> "VINVISGRP" Then 'PROGRAMMAR PASSWORD
            '            If UCase(txtpassword.Text) <> UCase(dtUser(0)("Password")) Then
            '                MsgBox("Invalid Password", MsgBoxStyle.Critical)
            '                txtpassword.Focus()
            '                Exit Sub
            '            End If
            '            If cmbbranch.Text = "" Then
            '                If cmbbranch.Items.Count > 0 Then
            '                    If MsgBox("Are You Sure to Login Without Branch", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
            '                        cmbbranch.Focus()
            '                        Exit Sub
            '                    End If
            '                End If
            '            End If
            '        End If
            '    End If

            '    CurrentUser = txtUsername.Text
            '    dtPhotopath = Trim(dtUser(0)("documentPath") & "")
            '    userType = IIf(dtUser(0)("MasterYN") = True, 0, 1)
            '    UsrBr = cmbbranch.Text
            '    'Usrlocation = Trim(dtUser(0)("DefLoc") & "")
            'Else
            '    If UCase(txtpassword.Text) <> "VINVISGRP" Then 'PROGRAMMAR PASSWORD
            '        MsgBox("User does not exist", MsgBoxStyle.Critical)
            '        txtUsername.Focus()
            '        Exit Sub
            '    End If
            'End If
            If UCase(txtpassword.Text) <> "VINVISGRP" Then 'PROGRAMMAR PASSWORD
                If UCase(txtpassword.Text) <> UCase(txtpassword.Tag) Then
                    MsgBox("Invalid Password", MsgBoxStyle.Critical)
                    txtpassword.Focus()
                    Exit Sub
                End If
                If cmbbranch.Text = "" Then
                    If cmbbranch.Items.Count > 0 Then
                        If MsgBox("Are You Sure to Login Without Branch", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                            cmbbranch.Focus()
                            Exit Sub
                        End If
                    End If
                End If
            End If
            UsrBr = cmbbranch.Text

            If UCase(txtpassword.Text) <> "VINVISGRP" Then
                CurrentUser = txtUsername.Text
            Else
                CurrentUser = "PROGRAMMAR"
            End If

            fMainForm.Width = Screen.PrimaryScreen.WorkingArea.Width
            fMainForm.Height = Screen.PrimaryScreen.WorkingArea.Height
            fMainForm.Show()
            'RaiseEvent CloseLogin()
            Me.Close()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try

    End Sub

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        Dim frm As New KeyForm
        Dim dt As DataTable
        Dim expired As Boolean
        Dim filepath As String = Application.StartupPath & "\dataSet.dll"
        If System.IO.File.Exists(filepath) Then
            expired = True
            GoTo skp
        End If
        loadLocation()
        'MsgBox(Dloc)
        dt = _objCmnblayer._fldDatatable("SELECT Cntdt,Dt FROM CompanyTb")
        If dt.Rows.Count > 0 Then
            If Val(dt(0)(0) & "") <> 80 And Val(dt(0)(0) & "") <> 81 And Val(dt(0)(0) & "") <> 87 Then
                expired = False
                If UCase(txtpassword.Text) <> "VINVISGRP" Then
                    _objCmnblayer._saveDatawithOutParm("Update CompanyTb set Dt=getdate(),Cntdt=87")
                End If
                GoTo skp
            End If
            If Val(dt(0)(0) & "") = 87 Then
                If DateValue(Date.Now) > DateAdd(DateInterval.Day, 15, DateValue(dt(0)(1))) Then
                    expired = True
                    GoTo skp
                Else
                    GoTo skp
                End If
            End If
            If Val(dt(0)(0) & "") = 80 Then
                If DateValue(Date.Now) > DateAdd(DateInterval.Day, 30, DateValue(dt(0)(1))) Then
                    expired = True
                    GoTo skp
                Else
                    GoTo skp
                End If
            End If
            If Val(dt(0)(0) & "") = 81 Then
                expired = False
            Else
                expired = True
            End If
skp:
        End If
        If expired And UCase(txtpassword.Text) <> "VINVISGRP" Then
            If System.IO.File.Exists(filepath) Then
                MsgBox("You are using demo verion of this product! Please contact your vendor to continue with registration" & vbCrLf & "Thank you for using!", MsgBoxStyle.Exclamation)
            Else
                MsgBox("You are using demo verion of this product! Please contact your vendor to continue with registration" & vbCrLf & "Thank you for using!", MsgBoxStyle.Exclamation)
                System.IO.File.Create(filepath).Dispose()
            End If
            If UCase(txtpassword.Text) = "VINVISGRP" Then
                CurrentUser = "PROGRAMMER"
            Else
                CurrentUser = ""
            End If
            frm.ShowDialog()
            Exit Sub
        Else
            If System.IO.File.Exists(filepath) Then
                System.IO.File.Delete(filepath)
            End If
        End If
        subUsername()
    End Sub

    Private Sub txtUsername_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtUsername.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
            'ldBranch()
        End If
    End Sub

    Private Sub cmbBranch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbbranch.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtpassword_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtpassword.KeyDown
        If e.KeyCode = Keys.Return Then
            OK_Click(OK, New System.EventArgs)
        End If
    End Sub
    Private Sub ldBranch(ByVal dt As DataTable)
        'dt = _objCmnblayer._fldDatatable("SELECT Branchcode FROM UsrBr LEFT JOIN BranchTb ON UsrBr.BrId=BranchTb.Branchcode WHERE UsrId=" & Val(txtUsername.Tag))
        cmbbranch.Items.Clear()
        cmbbranch.Items.Add("")
        For i = 0 To dt.Rows.Count - 1
            cmbbranch.Items.Add(dt(i)(0))
        Next
    End Sub

    Private Sub Login_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Label3.Text = "Version : " & My.Application.Info.Version.ToString
    End Sub

    Private Sub txtUsername_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUsername.Validated
        If txtUsername.Text = "" Then Exit Sub
        Try
            Dim dtUser As DataSet
            'cmbBranch.Items.Clear()
            Dim qry As String
            qry = "SELECT * FROM UserTb WHERE UserId='" & txtUsername.Text & "'"
            qry = qry & " SELECT Branchcode FROM UsrBr " & _
            "LEFT JOIN BranchTb ON UsrBr.BrId=BranchTb.Branchcode " & _
            "LEFT JOIN UserTb ON UsrBr.UsrId=UserTb.id " & _
            "WHERE UserId='" & txtUsername.Text & "'"
            qry = qry & " SELECT * FROM Rights LEFT JOIN UserTb ON Rights.UId=UserTb.Id where UserId='" & txtUsername.Text & "'"

            dtUser = _objCmnblayer._ldDataset(qry, False)
            If dtUser.Tables(0).Rows.Count > 0 Then
                txtUsername.Text = UCase(txtUsername.Text)
                txtUsername.Tag = dtUser.Tables(0)(0)("id")
                txtpassword.Tag = dtUser.Tables(0)(0)("Password")
                ldBranch(dtUser.Tables(1))
                dtrights = dtUser.Tables(2)
                dtPhotopath = Trim(dtUser.Tables(0)(0)("documentPath") & "")
                'DPath = dtPhotopath
                userType = IIf(dtUser.Tables(0)(0)("MasterYN") = True, 0, 1)
                If Not IsDBNull(dtUser.Tables(0)(0)("LstOpnBr")) Then
                    cmbbranch.Text = dtUser.Tables(0)(0)("LstOpnBr")
                Else
                    cmbbranch.Text = Trim(dtUser.Tables(0)(0)("DefLoc") & "")
                End If
                userDiscount = Val(dtUser.Tables(0)(0)("maxsalesDiscPercentage") & "")
            Else
                MsgBox("Invalid Username", MsgBoxStyle.Critical)
                txtUsername.Focus()
                txtpassword.Tag = ""
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub loadLocation()
        Dim ds As DataSet
        Dim str As String = "Select BrLocation,BranchId from BranchTb where Branchcode='" & cmbbranch.Text & "' "
        str = str & "Select Branchcode,BranchId from BranchTb where isnull(isdefault,0)=1"
        ds = _objCmnblayer._ldDataset(str, False)
        If ds.Tables(0).Rows.Count > 0 Then
            Dloc = Trim(ds.Tables(0)(0)("BrLocation") & "")
            BranchId = Val(ds.Tables(0)(0)("BranchId") & "")
        End If
        If ds.Tables(1).Rows.Count > 0 Then
            Dbranch = Trim(ds.Tables(1)(0)("Branchcode") & "")
            DBranchId = Val(ds.Tables(1)(0)("BranchId") & "")
        End If
    End Sub
End Class