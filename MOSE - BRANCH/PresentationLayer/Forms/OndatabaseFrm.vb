

Public Class UnitMasterFrm
    Public chkSQL As String
    Public chkSQLdup As String
    Public rptSQL As String
    Public RptType As String
    Public KeyFld As Integer = 0
    Public strSQL As String
    Public StrCaption As String
    Public numCols As String
    Dim _objcmnbLayer As New clsCommon_BL

    'Dim _objcmnPrts As New pcl_Cmn
    Public Event Unload()
    Public Event DoExtnlAction(ByVal id As String)
    Public Event SetGridHead()
    Dim ds As DataSet
    Private _NewRw As Boolean
    Private chageByPgm As Boolean

    Dim _hasChanges As Boolean
    Dim activecontrolname As String
    Private Sub setDataset()
        Try
            ds = _objcmnbLayer._ldDataset(strSQL, False)
            dvData.DataSource = ds
            chageByPgm = True
            dvData.DataMember = "table"
            chageByPgm = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub LoadGrid()
        setDataset()
        Select Case StrCaption
            Case "Unit Master"
                With dvData
                    chageByPgm = True
                    .Columns(2).HeaderText = "Fraction"
                    .Columns(2).Width = 70
                    .Columns(2).DefaultCellStyle.Format = "N0"
                    .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    chageByPgm = False
                End With
            Case "Carrier"
                With dvData
                    .Columns(0).HeaderText = "Carrier Name"
                    .Columns(0).Width = 150
                    .Columns(1).Visible = False
                End With
                chageByPgm = True

                chageByPgm = False
        End Select

    End Sub

    Private Sub OndatabaseFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmdUpdate.Enabled = False
        'Me.Text = StrCaption
        Call _setFormPropertiesBasedOnNature()
        LoadGrid()
        SetEntryGridProperty(dvData)
        If userType Then
            cmdAddnew.Tag = IIf(getRight(14, CurrentUser), 1, 0)
            cmdRemove.Tag = IIf(getRight(16, CurrentUser), 1, 0)
            cmdUpdate.Tag = IIf(getRight(15, CurrentUser), 1, 0)
        Else
            cmdAddnew.Tag = 1
            cmdAddnew.Tag = 1
            cmdUpdate.Tag = 1
        End If
        Select Case StrCaption
            Case "Unit Master"
                With dvData
                    chageByPgm = True
                    .Columns(2).HeaderText = "Fraction"
                    .Columns(2).Width = 70
                    .Columns(2).DefaultCellStyle.Format = "N0"
                    .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns(3).DefaultCellStyle.Format = "N0"
                    .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    chageByPgm = False
                End With
            Case "Carrier"
                With dvData
                    .Columns(0).HeaderText = "Carrier Name"
                    .Columns(0).Width = 150
                    .Columns(1).Visible = False
                End With
                chageByPgm = True

                chageByPgm = False
        End Select
        If Val(cmdUpdate.Tag) = 1 Then cmdUpdate.Enabled = True
    End Sub
    Private Sub _setFormPropertiesBasedOnNature()
        '-----------------Created By Ashok On 12/10/2013------------------
        ' This Method is used to select /set form properties and SQL quries 
        ' Based On menu selection. Because This Form is used for meny entries 
        ' (ie; Unit master , Branch, Sales MAster...etc...  Based on the menu
        ' selection, The Grid should fill / Update data from carrasponding table
        ' based on users action. For This Here we  set the sql queries also.
        '-------------------------------------------------------------------

        ' The variable "StrCaption" get the value from Menu items click
        Select Case StrCaption

            Case "Unit Master"
                chkSQL = "Select Unit From InvItm Where unit = '"
                strSQL = "Select Units,Description,FraCount,POSCount,IsDefault from UnitsTb Order By IsDefault,Units"
                'strSQL = "stp_FET_UniteDetails"
            Case "B"
                chkSQL = "0"
                strSQL = "B"
            Case "C"
                chkSQL = "B"
                strSQL = "B"
            Case "Sales Route"
                chkSQL = "Select areacode From AccMast Where AreaCode = '"
                strSQL = "Select Areacode,Areaname  from AreaTb Order By AreaCode"
            Case "Carrier"
                chkSQL = "Select carrierid From CarrierTrTb Where AreaCode = '"
                strSQL = "Select carriername,carrierid  from CarrierTb Order By carriername"
        End Select

    End Sub

    Private Sub cmdRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemove.Click
        If Val(cmdRemove.Tag) = 0 Then
            MsgBox("This user do not have permission to Remove", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Try
            With dvData
                If .RowCount > 0 Then
                    If MsgBox("You Are Going to Remove the Row." & vbCrLf & "Are You Sure ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then
                        Exit Sub
                    End If
                    .Rows.RemoveAt(.CurrentRow.Index)
                    _objcmnbLayer.__saveDataset(strSQL, ds)
                    .ClearSelection()
                End If
            End With
        Catch ex As Exception
            MsgBox(Err.Description, MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub cmdUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdate.Click
        Try
            If _hasChanges Then
                _objcmnbLayer.__saveDataset(strSQL, ds)
                LoadGrid()
            End If
            _hasChanges = False
            'cmdUpdate.Enabled = False
            'cmdUpdate.Tag = IIf(getRight(15, CurrentUser), 1, 0)
            If Val(cmdUpdate.Tag) = 1 Then cmdUpdate.Enabled = True
        Catch ex As Exception
            MsgBox(ex.Message & vbCrLf & " Invalid Data or Duplication Found", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub dvData_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dvData.CellEnter
        If chageByPgm Then Exit Sub
        dvData.BeginEdit(True)
    End Sub

    Private Sub dvData_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dvData.GotFocus
        activecontrolname = "dvData"
    End Sub

    'Private Sub dvData_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
    '    _hasChanges = True
    '    cmdUpdate.Enabled = True
    'End Sub

    Private Sub dvData_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dvData.KeyDown
        With dvData
            Dim intEmptyColumnId As Integer = 0
            If e.KeyCode = Keys.Return Then
                Dim RowIndex As Integer = .CurrentCell.RowIndex
                Dim ColIndex As Integer = .CurrentCell.ColumnIndex
                Dim row = .Rows(RowIndex)
                'Code Added By Ashok -------------v
                intEmptyColumnId = clsCommonFunctions_BL.isGridMandatoryFieldsFilled_Two(RowIndex, ColIndex, 0, 1, dvData)
                If intEmptyColumnId > 0 Then
                    dvData.Select()
                    dvData.CurrentCell = dvData.Item(intEmptyColumnId, RowIndex)
                End If
                '----------- ---------------------^
                e.SuppressKeyPress = True
                FindNextCell(dvData, RowIndex, ColIndex + 1)
                .BeginEdit(True)
            ElseIf e.KeyCode = Keys.F4 Then
                cmdRemove_Click(cmdRemove, New System.EventArgs)
            End If
        End With

    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            Const WM_KEYDOWN As Integer = &H100
            Const WM_KEYUP As Integer = &H101
            msg.ToString()
            If msg.Msg = WM_KEYDOWN Or msg.Msg = WM_KEYUP Then
                If activecontrolname = "dvData" Then
                    If msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up) Or msg.WParam.ToInt32() = CInt(Keys.Enter) Or msg.WParam.ToInt32() = CInt(Keys.F2) Or msg.WParam.ToInt32() = CInt(Keys.F3) Or msg.WParam.ToInt32() = CInt(Keys.F4) Then
                        dvData_KeyDown(Nothing, New KeyEventArgs(keyData))
                        Return True
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function


    'Public Sub BeginEdit()
    '    With dvData
    '        If .RowCount = 0 Then Exit Sub
    '        Dim colIndex As Integer
    '        Dim roIndex As Integer
    '        colIndex = .CurrentCell.ColumnIndex
    '        roIndex = .CurrentCell.RowIndex
    '        activecontrolname = "dvData"
    '        .BeginEdit(True)
    '    End With
    'End Sub



    Private Sub dvData_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dvData.CellValueChanged
        If chageByPgm Then Exit Sub
        _hasChanges = True
        If Val(cmdUpdate.Tag) > 0 Then
            cmdUpdate.Enabled = True
        End If

    End Sub

    Private Sub cmdAddnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddnew.Click
        If Val(cmdAddnew.Tag) = 0 Then
            MsgBox("This user do not have permission to Create New", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        cmdUpdate.Tag = 1
        Call AddNewGridRow()

    End Sub

    Private Sub AddNewGridRow()
        Dim _newDataRow As DataRow
        Dim _RowCount As Integer = dvData.RowCount
        _newDataRow = ds.Tables(0).NewRow()
        ds.Tables(0).Rows.Add(_newDataRow)

        'dvData.Rows.Add(1)

        '_newRow = True


        dvData.CurrentCell = dvData.Item(0, _RowCount)
        'dvData.Rows(_RowCount + 1).Visible = False
        dvData.BeginEdit(True)
        'dvData.Rows.RemoveAt(_RowCount - 1)

    End Sub

    Private Sub dvData_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dvData.CellContentClick

    End Sub
End Class