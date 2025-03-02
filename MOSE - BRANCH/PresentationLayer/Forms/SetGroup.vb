
Public Class SetGroup
#Region "Local Variables"
    Private chgbyprg As Boolean
#End Region
#Region "Public Variables"
    Public ItemId As Long
#End Region
#Region "Object Variables"
    Private _objcmnbLayer As New clsCommon_BL
#End Region
#Region "Publice Events"
    Public Event updateGroup()
#End Region
    Private Sub SetGroup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetLevelGrid()
        ldLevel()
    End Sub
    Private Sub SetLevelGrid()
        'Dim dtLevel As DataTable
        'Dim dtGrp As DataTable
        chgbyprg = True
        Dim headert(2) As String
        headert(0) = "GrpItmCode"
        headert(1) = "Description"
        headert(2) = "UnqGrpId"
        With grdLevel
            .ReadOnly = False
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = True
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = True
            .AutoResizeColumns()
            .StandardTab = False
            '.Location = New Point(1, 1)
            .ColumnCount = 1
            '.Width = tbPanelLevel.Width - 2
            '.Height = tbPanelLevel.Height - 2
            .ColumnHeadersHeight = 100
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersDefaultCellStyle.BackColor = Color.Olive
            .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("arial", 8.5!, FontStyle.Regular)

            .Columns(0).HeaderText = "Level"
            .Columns(0).Width = 150
            .Columns(0).ReadOnly = True

            Dim cmb As New DataGridViewComboBoxColumn
            cmb.HeaderText = "Item Group"
            cmb.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
            cmb.DropDownWidth = 150
            '
            .Columns.Add(cmb)
            .Columns(1).ReadOnly = False
            .Columns(1).Width = 160
            .Columns.Item(1).ReadOnly = False
            .Columns.Item(1).Width = 160

            .Columns.Add("id", "id")
            .Columns.Item(2).Visible = False

            '.Columns(1).ReadOnly = True
            'ldLevel()
        End With

        chgbyprg = False
    End Sub
    Private Sub ldLevel()
        Dim LevelTb As DataTable
        Dim LevelGrpTb As DataTable
        Dim dtGroup As DataTable
        Dim cmb As New DataGridViewComboBoxCell
        Dim itmlevel As New DataTable
        'grdLevel.Rows.Clear()
        LevelTb = _objcmnbLayer._fldDatatable("SELECT LName,LCode from LevelTb Order by LCode")
        LevelGrpTb = _objcmnbLayer._fldDatatable("SELECT LevelTb.LCode, LName, GrpItmCode, UnqGrpId FROM LevelTb LEFT JOIN " & _
          "(SELECT GrpItmCode, LCode, UnqGrpId FROM GrpItmTb) Q ON Q.LCode = LevelTb.LCode ORDER BY LevelTb.LCode,GrpItmCode")
        chgbyprg = True
        Dim found As Boolean
        With grdLevel
            .Rows.Clear()
            Dim i As Integer
            '.RowCount = 0
            If LevelTb.Rows.Count > 0 Then
                For i = 0 To LevelTb.Rows.Count - 1
                    If LevelTb.Rows.Count > .RowCount Then .Rows.Add()
                    .Item(0, i).Value = LevelTb(i)("LName")
                    cmb = .Rows(i).Cells(1)
                    If cmb.Items.Count = 0 Then
                        cmb.Items.Clear()
                        cmb.Items.Add("")
                        dtGroup = SearchGrid(LevelGrpTb, LevelTb(i)("LName"), 1)
                        found = False
                        For j = 0 To dtGroup.Rows.Count - 1
                            cmb.Items.Add(dtGroup(j)("GrpItmCode"))
                            If itmlevel.Rows.Count > 0 Then
                                If Trim(itmlevel(0)(0) & "") = Trim(dtGroup(j)("GrpItmCode") & "") Then found = True
                            End If
                        Next
                    End If
                Next
            End If
            .Refresh()
        End With
        ldItemLevel()
        chgbyprg = False
    End Sub

    Private Sub BtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub

    Private Sub btnsetGroup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsetGroup.Click
        Dim dt As DataTable = _objcmnbLayer._fldDatatable("select top 1 UnqGrpId FROM GrpItmTb where 1=0", False)
        Dim num2 As Integer = (grdLevel.Rows.Count - 1)
        Dim i As Integer = 0
        Do While (i <= num2)
            dt = _objcmnbLayer._fldDatatable("select UnqGrpId FROM GrpItmTb where GrpItmCode = '" & grdLevel.Item(1, i).Value & "'", False)
            If (dt.Rows.Count > 0) Then
                grdLevel.Item(2, i).Value = dt(0)("UnqGrpId")
            End If
            i += 1
        Loop
        RaiseEvent updateGroup()
        Me.Close()
    End Sub

    Private Sub grdLevel_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdLevel.CellClick
        If e.ColumnIndex = 0 Then Exit Sub
        grdLevel.BeginEdit(True)
    End Sub

    Private Sub grdLevel_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdLevel.CellContentClick

    End Sub

    Private Sub grdLevel_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles grdLevel.DataError

    End Sub

    Private Sub grdLevel_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdLevel.EditingControlShowing
        grdLevel.Tag = "chg"
    End Sub
    Private Sub ldItemLevel()
        Dim i As Integer
        Dim itmlevel As DataTable
        Dim _vDtable = _objcmnbLayer._fldDatatable("SELECT * FROM InvItm WHERE Itemid=" & ItemId)
        With grdLevel
            For i = 0 To .RowCount - 1
                If Not _vDtable Is Nothing Then
                    If _vDtable.Rows.Count > 0 Then
                        itmlevel = _objcmnbLayer._fldDatatable("SELECT GrpItmCode,UnqGrpId FROM GrpItmTb WHERE UnqGrpId=" & Val(_vDtable(0)("Level" & i + 1) & ""))
                        If itmlevel.Rows.Count > 0 Then
                            .Item(1, i).Value = itmlevel(0)(0)
                        End If
                    End If
                End If
            Next
        End With

    End Sub
End Class