
Public Class LevelMasterFrm
    'object declarations
    Private _objcmnbLayer As New clsCommon_BL

    Private Sub LevelMasterFrm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtlevel.Focus()
    End Sub

    Private Sub LevelMasterFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ldLevel()
        getNextOrderNumber()
    End Sub


    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        Dim dt As DataTable
        If TabControl1.SelectedIndex = 0 Then
            If txtlevel.Text = "" Then
                MsgBox("Level name canot be blank", MsgBoxStyle.Exclamation)
                txtlevel.Focus()
                Exit Sub

            End If
            dt = _objcmnbLayer._fldDatatable("SELECT LCode FROM LevelTb where LName='" & txtlevel.Text & "' and LCode<>" & Val(txtlevel.Tag))
            If dt.Rows.Count > 0 Then
                MsgBox("Level Name already exist", MsgBoxStyle.Critical)
                txtlevel.Focus()
                Exit Sub
            End If
            dt = _objcmnbLayer._fldDatatable("SELECT LCode FROM LevelTb where lorder=" & Val(txtorder.Text) & " and LCode<>" & Val(txtlevel.Tag))
            If dt.Rows.Count > 0 Then
                MsgBox("Level Order already exist", MsgBoxStyle.Critical)
                getNextOrderNumber()
                txtorder.Focus()
                Exit Sub
            End If
            If Val(txtlevel.Tag) > 0 Then
                _objcmnbLayer._saveDatawithOutParm("UPDATE LevelTb SET LName='" & txtlevel.Text & "',lorder=" & Val(txtorder.Text) & " WHERE LCode=" & Val(txtlevel.Tag))
            Else
                _objcmnbLayer._saveDatawithOutParm("Insert into LevelTb (LName,lorder) values('" & txtlevel.Text & "'," & Val(txtorder.Text) & ")")
            End If
            MsgBox("Level Created Successfully", MsgBoxStyle.Information)
            ldLevel()
            getNextOrderNumber()
            txtlevel.Text = ""
            txtlevel.Tag = ""
            txtorder.Text = ""
            txtlevel.Focus()
        Else
            dt = _objcmnbLayer._fldDatatable("SELECT LCode FROM LevelTb where LName='" & cmblevel.Text & "'")
            If dt.Rows.Count > 0 Then
                cmblevel.Tag = dt(0)("LCode")
            End If
            dt = _objcmnbLayer._fldDatatable("SELECT UnqGrpId FROM GrpItmTb where GrpItmCode='" & txtGcode.Text & "' AND UnqGrpId<>" & Val(txtGcode.Tag))
            If dt.Rows.Count > 0 Then
                MsgBox("Group Code already exist", MsgBoxStyle.Critical)
                txtGcode.Focus()
                Exit Sub
            End If
            dt = _objcmnbLayer._fldDatatable("SELECT UnqGrpId FROM GrpItmTb where GrpName='" & txtGname.Text & "' AND UnqGrpId<>" & Val(txtGcode.Tag))
            If dt.Rows.Count > 0 Then
                MsgBox("Group Name already exist", MsgBoxStyle.Critical)
                txtGname.Focus()
                Exit Sub
            End If
            If Val(cmblevel.Tag) = 0 Then MsgBox("Select one Level Name", MsgBoxStyle.Exclamation) : cmblevel.Focus() : Exit Sub
            If Val(txtGcode.Tag) > 0 Then
                _objcmnbLayer._saveDatawithOutParm("UPDATE GrpItmTb SET GrpItmCode='" & txtGcode.Text & "',LCode=" & Val(cmblevel.Tag) & ",GrpName='" & txtGname.Text & "'  WHERE UnqGrpId=" & Val(txtGcode.Tag))
            Else
                _objcmnbLayer._saveDatawithOutParm("Insert into GrpItmTb (GrpItmCode,LCode,GrpName) values('" & txtGcode.Text & "'," & Val(cmblevel.Tag) & ",'" & txtGname.Text & "')")
            End If
            MsgBox("Group Created Successfully", MsgBoxStyle.Information)
            ldGroup()
            txtGcode.Text = ""
            cmblevel.Tag = ""
            txtGcode.Tag = ""
            txtGname.Text = ""
            txtGcode.Focus()
        End If
    End Sub
    Private Sub ldLevel()
        Dim dtLevel As DataTable
        Dim i As Integer
        dtLevel = _objcmnbLayer._fldDatatable("SELECT LCode,LName,lorder Lorder FROM LevelTb")
        If dtLevel.Rows.Count > 0 Then
            With lstlevel
                .Items.Clear()
                cmblevel.Items.Clear()
                For i = 0 To dtLevel.Rows.Count - 1
                    .Items.Add(dtLevel(i)("LName"))
                    .Items.Item(i).SubItems.Add("")
                    .Items.Item(i).SubItems(1).Text = dtLevel(i)("LCode")
                    .Items.Item(i).SubItems.Add("")
                    .Items.Item(i).SubItems(2).Text = Val(dtLevel(i)("lorder") & "")
                    cmblevel.Items.Add(dtLevel(i)("LName"))
                Next
            End With
        End If
    End Sub
    Private Sub getNextOrderNumber()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select max(lorder) lordernumber from LevelTb")
        Dim lstvalue As Integer
        If dt.Rows.Count > 0 Then
            lstvalue = Val(dt(0)("lordernumber") & "")
        End If
        txtorder.Text = lstvalue + 1
    End Sub
    Private Sub ldGroup()
        Dim dt As DataTable
        Dim i As Integer
        dt = _objcmnbLayer._fldDatatable("SELECT GrpItmCode,GrpName,UnqGrpId FROM GrpItmTb INNER JOIN LevelTb ON GrpItmTb.LCode=LevelTb.LCode where LName='" & cmblevel.Text & "'")
        lstgroup.Items.Clear()
        If dt.Rows.Count > 0 Then
            With lstgroup
                For i = 0 To dt.Rows.Count - 1
                    .Items.Add(dt(i)("GrpItmCode"))
                    .Items.Item(i).SubItems.Add("")
                    .Items.Item(i).SubItems(1).Text = dt(i)("GrpName")
                    .Items.Item(i).SubItems.Add("")
                    .Items.Item(i).SubItems(2).Text = dt(i)("UnqGrpId")
                Next
            End With
        End If
    End Sub

    Private Sub btnexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexit.Click
        Me.Close()
    End Sub

    Private Sub txtlevel_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtlevel.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnadd.Focus()
        End If
    End Sub

    Private Sub txtlevel_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtlevel.TextChanged

    End Sub

    Private Sub TabControl1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.Click
        If TabControl1.SelectedIndex = 0 Then
            txtlevel.Focus()
        Else
            txtGcode.Focus()
            If cmblevel.Items.Count > 0 Then cmblevel.SelectedIndex = 0
            ldGroup()

        End If
    End Sub

    Private Sub lstlevel_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstlevel.DoubleClick
        If userType Then
            If Not getRight(19, CurrentUser) Then
                MsgBox("This user do not have rights to Modify", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
        End If

        txtlevel.Text = lstlevel.SelectedItems.Item(0).Text
        txtlevel.Tag = Val(lstlevel.SelectedItems.Item(0).SubItems(1).Text)
        txtorder.Text = Val(lstlevel.SelectedItems.Item(0).SubItems(2).Text)
    End Sub

    Private Sub lstlevel_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstlevel.SelectedIndexChanged

    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        If TabControl1.SelectedIndex = 0 Then
            txtlevel.Text = ""
            txtlevel.Tag = ""
            txtlevel.Focus()
        Else
            txtGcode.Text = ""
            cmblevel.Tag = ""
            txtGcode.Tag = ""
            txtGname.Text = ""
            txtGcode.Focus()
        End If

    End Sub

    Private Sub btnremove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnremove.Click
        If userType Then
            If Not getRight(20, CurrentUser) Then
                MsgBox("This user do not have rights to Remove", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
        End If

        If TabControl1.SelectedIndex = 0 Then
            If MsgBox("Do you want to remove the Level code #" & lstlevel.SelectedItems.Item(0).Text, MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Dim dt As DataTable
                dt = _objcmnbLayer._fldDatatable("SELECT UnqGrpId FROM GrpItmTb WHERE LCode=" & Val(lstlevel.SelectedItems.Item(0).SubItems(1).Text))
                If dt.Rows.Count > 0 Then
                    MsgBox("Level Contain Group items! You can't remove this Level", MsgBoxStyle.Critical)
                    Exit Sub
                End If
                If Val(txtlevel.Tag) > 0 Then
                    MsgBox("Invalide attempt to remove Level Master! Save data first", MsgBoxStyle.Critical)
                    Exit Sub
                End If
                _objcmnbLayer._saveDatawithOutParm("DELETE FROM LevelTb where LCode=" & Val(lstlevel.SelectedItems.Item(0).SubItems(1).Text))
                ldLevel()
            End If
            txtlevel.Focus()
        Else
            If MsgBox("Do you want to remove the Group code # " & lstgroup.SelectedItems.Item(0).Text, MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                If Val(txtGcode.Tag) > 0 Then
                    MsgBox("Invalide attempt to remove Group Master! Save data first", MsgBoxStyle.Critical)
                    Exit Sub
                End If
                _objcmnbLayer._saveDatawithOutParm("DELETE FROM GrpItmTb where UnqGrpId=" & Val(lstgroup.SelectedItems.Item(0).SubItems(2).Text))
                ldGroup()
            End If
            txtGcode.Focus()
        End If
    End Sub

    Private Sub lstgroup_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstgroup.DoubleClick
        If userType Then
            If Not getRight(19, CurrentUser) Then
                MsgBox("This user do not have rights to Modify", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
        End If

        With lstgroup.SelectedItems.Item(0)
            txtGcode.Text = .Text
            txtGcode.Tag = .SubItems(2).Text
            txtGname.Text = .SubItems(1).Text
        End With
    End Sub


    Private Sub cmblevel_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmblevel.SelectedIndexChanged

    End Sub

    Private Sub cmblevel_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmblevel.SelectedValueChanged
        ldGroup()
    End Sub

   
    Private Sub txtGcode_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtGname.KeyDown, txtGcode.KeyDown, cmblevel.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtorder_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtorder.KeyPress
        NumericTextOnKeypress(txtorder, e, False, "0")
    End Sub

    Private Sub txtorder_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtorder.TextChanged

    End Sub
End Class