
Public Class Mlistfrm

    Public strMyQry As String
    Public strMyCaption As String
    Public SearchIndex As Integer
    Public SearchIndexDescr As Integer
    Public SearchText As String
    Public loc As Point
    Public isSingle As Boolean
    Public isItemdata As Boolean
    Dim _vdatatable As DataTable
    Dim _objcmnbLayer As New clsCommon_BL
    Public resizecolum As Integer
    Public istemple As Boolean
    Private ItemserarchGlobal As Boolean
    Public donotresize As Boolean

    '///Events
    Public Event doClose()
    Public Event SetMyGridHead()
    Public Event doFocus()
    Public Event doSelect(ByVal ItmFlds() As String)

    Private Sub Mlistfrm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Me.Location = loc
    End Sub


    Private Sub Mlistfrm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RaiseEvent doClose()
    End Sub

    Private Sub Mlistfrm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Hide()
        End If
    End Sub
    Private Sub ReturnItem()
        Me.Hide()
        SelectItm()
    End Sub
    Private Sub Mlistfrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        RaiseEvent doFocus()
    End Sub
    Public Sub FillGrid(Optional ByVal ispositem As Boolean = False)
        dvData.DataSource = Nothing
        If ispositem Then
            If _vInvItmtable.Rows.Count = 0 Or isnewItemcreated Then
                _vdatatable = SearchProductPanel(dvData, "Item Code", "", True, , , , True)
                dvData.DataSource = _vdatatable
            Else
                _vdatatable = _vInvItmtable
                dvData.DataSource = _vdatatable
            End If

        Else
            If strMyQry = "" Then GoTo SetHeadOnly
            _vdatatable = _objcmnbLayer._fldDatatable(strMyQry)
            dvData.DataSource = _vdatatable
        End If

SetHeadOnly:
        SetGridHead()
    End Sub
    Private Sub SetGridHead()
        Me.Text = strMyCaption
        With dvData
            .ReadOnly = True
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = True
            '.AutoResizeColumn(0)
            .ColumnHeadersHeight = 100
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke
            .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular)
            .RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 9.0!)
            If isSingle Then
                .Columns(0).Width = dvData.Width - 50
                If .Columns.Count > 1 Then
                    .Columns(.Columns.Count - 1).Visible = False
                End If
            End If
            If isItemdata Then

                .Columns(2).Width = 70
                .Columns(2).DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(1).Width = dvData.Width - .Columns(1).Width - 50
                If .Columns.Count > 3 Then
                    .Columns(3).Visible = False
                End If
            End If
            'If resizecolum > 0 Or (resizecolum = 0 And dvData.RowCount > 0 And dvData.ColumnCount = 1) Then

            'End If
            'If Not isItemdata Then
            '    If istemple Then
            '        resizeGridColumn(dvData, 0)
            '    Else
            '        resizeGridColumn(dvData, resizecolum)
            '    End If
            'End If
            If istemple Then
                resizeGridColumn(dvData, 0)
            Else
                resizeGridColumn(dvData, resizecolum)
            End If
            Timer1.Enabled = True
        End With
    End Sub
    Public Function MoveDown(ByVal DefTxt As String) As String
        MoveDown = DefTxt
        Dim r As Integer
        With dvData
            If .RowCount < 1 Then Exit Function
            If .CurrentRow.Index = .RowCount - 1 Then GoTo Slct
            r = .CurrentRow.Index
            If .CurrentRow.Index < .RowCount - 1 Then
                r = r + 1
                .Rows(r).Selected = True
                .CurrentCell = .Item(SearchIndex, r)
                .FirstDisplayedScrollingRowIndex() = r
            Else
                Exit Function
            End If
Slct:
            SelectItm()
            MoveDown = Trim(.Item(SearchIndex, r).Value & "") '  .SelectedItem.Text
        End With
    End Function

    Public Function MoveUp(ByVal DefTxt As String) As String
        MoveUp = DefTxt
        Dim r As Integer
        With dvData
            If .RowCount < 1 Then Exit Function
            If .CurrentRow.Index < 0 Then Exit Function
            r = .CurrentRow.Index
            If r = 0 And DefTxt = .Item(SearchIndex, r).Value Then Exit Function
            If r <> 0 Then
                r = r - 1
                If Not r < 0 Then
                    .Rows(r).Selected = True
                    .CurrentCell = .Item(SearchIndex, r)
                    .FirstDisplayedScrollingRowIndex() = r
                    SelectItm()
                    MoveUp = .Item(SearchIndex, r).Value
                End If
            End If
        End With

    End Function
    Private Sub SelectItm()
        With dvData
            If .RowCount < 1 Then Exit Sub
            Dim Items() As String
            Dim i As Byte
            ReDim Items(.Columns.Count - 1)
            For i = 0 To .Columns.Count - 1
                If IsDBNull(.Item(i, .CurrentRow.Index).Value) Then
                    Items(i) = ""
                Else
                    Items(i) = .Item(i, .CurrentRow.Index).Value  ' .Columns(i).Text '  
                End If
            Next i
            RaiseEvent doSelect(Items) 'ItemSelect(Items)
            ReDim Items(0)
        End With
    End Sub
    Public Function AssignList(ByRef Txt As Object, ByVal lstKey As Keys, Optional ByVal ChgByPrg As Boolean = False, Optional ByVal bFromDGV As Boolean = False) As String
        Dim s As String
        Dim l As Byte
        Dim r As Integer
        Dim preChgVal As Boolean
        AssignList = ""
        preChgVal = ChgByPrg
        If bFromDGV Then
            If Trim(Txt.value) = "" Then Exit Function
            With dvData
                If .RowCount = 0 Then Exit Function
                r = .CurrentRow.Index
                If UCase(.Item(SearchIndex, r).Value) Like UCase(Txt.value) & "*" Then
                    s = .Item(SearchIndex, r).Value
                    If lstKey = Keys.Back Or lstKey = Keys.Delete Then
                        If UCase(s) <> UCase(Txt.value) Then Exit Function
                    End If
                    l = Len(Txt.value)
                    ChgByPrg = True
                    Txt.value = Txt.value & Mid(s, l + 1)
                    Txt.SelectionStart = l
                    Txt.SelectionLength = Len(Txt.value) - l
                    AssignList = .Item(SearchIndexDescr, r).Value
                    ChgByPrg = preChgVal
                    Exit Function
                End If
            End With
        Else
            If Trim(Txt.Text) = "" Then Exit Function
            With dvData
                If .RowCount = 0 Then
                    Txt.tag = ""
                    ChgByPrg = preChgVal
                    Exit Function
                End If
                'r = .CurrentRow.Index
                If ItemserarchGlobal Then
                    Txt.tag = .Item(SearchIndex, r).Value
                    ChgByPrg = preChgVal
                    Exit Function
                Else
                    If UCase(.Item(SearchIndex, r).Value) Like UCase(Txt.Text) & "*" Then
                        s = .Item(SearchIndex, r).Value
                        If lstKey = Keys.Back Or lstKey = Keys.Delete Then
                            If UCase(s) <> UCase(Txt.Text) Then Exit Function
                        End If
                        l = Len(Txt.Text)
                        ChgByPrg = True
                        Txt.Text = Txt.Text & Mid(s, l + 1)
                        Txt.SelectionStart = l
                        Txt.SelectionLength = Len(Txt.Text) - l
                        AssignList = .Item(SearchIndexDescr, r).Value.ToString  ' .Columns(1).Text ' .SelectedItem.SubItems(1)
                        ChgByPrg = preChgVal 'False
                        Exit Function
                    End If
                End If

            End With
        End If
    End Function
    Public Function returnid(ByRef Txt As Object) As Long
        If Trim(Txt.text) = "" Then Exit Function
        With dvData
            If .RowCount = 0 Then Exit Function
            Dim r As Integer = .CurrentRow.Index
            Txt.tag = .Item(SearchIndexDescr, r).Value
        End With
    End Function
    Public Sub Search(ByVal txtSearch As String, Optional ByVal isglobalItemserarch As Boolean = False, Optional ByVal searchStartWith As Boolean = False, Optional ByVal iscashcustomer As Boolean = False, Optional ByVal isSUPP As Boolean = False)
        If _vdatatable Is Nothing Then Exit Sub
        Dim bDatatable As DataTable

        If Trim(txtSearch) <> "" Then
            If _vdatatable.Rows.Count = 0 Then Exit Sub
            Dim _qurey As EnumerableRowCollection(Of DataRow)
            ItemserarchGlobal = isglobalItemserarch
            If isglobalItemserarch Then
                If searchStartWith Then
                    _qurey = From data In _vdatatable.AsEnumerable() Where data("Code").ToString.StartsWith(Trim(txtSearch), StringComparison.OrdinalIgnoreCase) Or data("Description").ToString.StartsWith(Trim(txtSearch), StringComparison.OrdinalIgnoreCase) Select data
                Else
                    _qurey = From data In _vdatatable.AsEnumerable() Where UCase(data("Code")).ToString.Contains(Trim(UCase(txtSearch))) Or UCase(data("Description")).ToString.Contains(Trim(UCase(txtSearch))) Select data
                End If
            ElseIf iscashcustomer Then
                If searchStartWith Then
                    _qurey = From data In _vdatatable.AsEnumerable() Where data("Phone Number").ToString.StartsWith(Trim(txtSearch), StringComparison.OrdinalIgnoreCase) Or data("Customer Name").ToString.StartsWith(Trim(txtSearch), StringComparison.OrdinalIgnoreCase) Select data
                Else
                    _qurey = From data In _vdatatable.AsEnumerable() Where UCase(data("Phone Number")).ToString.Contains(Trim(UCase(txtSearch))) Or UCase(data("Customer Name")).ToString.Contains(Trim(UCase(txtSearch))) Select data
                End If
            ElseIf isSUPP Then
                If searchStartWith Then
                    _qurey = From data In _vdatatable.AsEnumerable() Where data("Phone Number").ToString.StartsWith(Trim(txtSearch), StringComparison.OrdinalIgnoreCase) Or data("Supplier Name").ToString.StartsWith(Trim(txtSearch), StringComparison.OrdinalIgnoreCase) Select data
                Else
                    _qurey = From data In _vdatatable.AsEnumerable() Where UCase(data("Phone Number")).ToString.Contains(Trim(UCase(txtSearch))) Or UCase(data("Supplier Name")).ToString.Contains(Trim(UCase(txtSearch))) Select data
                End If
            Else
                If searchStartWith Then
                    _qurey = From data In _vdatatable.AsEnumerable() Where data(SearchIndex).ToString.StartsWith(Trim(txtSearch), StringComparison.OrdinalIgnoreCase) Select data
                Else
                    _qurey = From data In _vdatatable.AsEnumerable() Where UCase(data(SearchIndex)).ToString.Contains(Trim(UCase(txtSearch))) Select data
                End If
            End If
            If _qurey.Count > 0 Then
                bDatatable = _qurey.CopyToDataTable()
                dvData.DataSource = bDatatable
            Else
                bDatatable = _vdatatable.Clone
            End If
        Else
            bDatatable = _vdatatable
        End If
        dvData.DataSource = bDatatable
    End Sub
    Public Sub Search(ByVal txtSearch As String, ByVal fld As String, Optional ByVal isglobalItemserarch As Boolean = False)
        Dim bDatatable As DataTable
        ItemserarchGlobal = isglobalItemserarch
        If txtSearch = "" Then
            bDatatable = _objcmnbLayer._fldDatatable(strMyQry & " WHERE ItemId=0")
        Else
            If isglobalItemserarch Then
                bDatatable = _objcmnbLayer._fldDatatable(strMyQry & " WHERE code like '" & txtSearch & "%' or Description like '" & txtSearch & "%'")
            Else
                bDatatable = _objcmnbLayer._fldDatatable(strMyQry & " WHERE " & fld & " like '" & txtSearch & "%'")
            End If

        End If
        dvData.DataSource = bDatatable
        SetGridHead()
    End Sub

    Private Sub dvData_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dvData.CellDoubleClick
        ReturnItem()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        If istemple Then
            resizeGridColumn(dvData, 0)
        Else
            resizeGridColumn(dvData, resizecolum)
        End If
    End Sub

    Private Sub dvData_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dvData.CellContentClick

    End Sub
End Class