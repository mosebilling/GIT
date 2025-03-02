
Public Class Selectfrm
    Public Event SetMyGridHead()
    Public Event doSelect(ByVal KeyId As String, ByVal strFld1 As String, ByVal strFld2 As String)
    Public strMyQry As String
    Public strMyCaption As String = ""
    Public cmbShowIndex As Integer
    Public bAddnew As Boolean = True
    Public bModify As Boolean = True
    Public BVal As Integer
    Private tmpMyQry As String
    'Private WithEvents fCrtAcc As CreateAccNew
    Dim _objcmnbLayer As New clsCommon_BL
    Dim _vdatatable As DataTable

    Private Sub Selectfrm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtSearch.Focus()
    End Sub

    Private Sub Selectfrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FillGrid()
        setComboGrid()
        'Me.Text = strMyCaption
        txtSearch.Select()
    End Sub
    Private Sub FillGrid()
        With dvData
            If strMyQry = "" Then GoTo SetHeadOnly
            tmpMyQry = strMyQry
            _vdatatable = _objcmnbLayer._fldDatatable(tmpMyQry)
            dvData.DataSource = _vdatatable
        End With
SetHeadOnly:
        'RaiseEvent SetMyGridHead()
        SetGridHead()
        txtSearch.Focus()
    End Sub
    Private Sub SetGridHead()
        With dvData
            .ReadOnly = True
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = True
            '.AllowUserToOrderColumns = True
            .AutoResizeColumn(1)
            '.ColumnCount = 8
            .ColumnHeadersHeight = 100
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersDefaultCellStyle.BackColor = Color.Olive
            .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular)
            .RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.0!)
            Select Case BVal
                Case 3
                    .Columns(0).Width = 150
                    .Columns(1).Width = 200

                    Me.Width = 425
                Case 1
                    .Columns(0).Width = 350
                    .Columns(1).Width = 120
                    .Columns(2).Width = 120
                Case Else
                    .Columns(0).Width = 350
                    .Columns(1).Width = 120
                    .Columns(2).Visible = False
            End Select
            .Columns(.Columns.Count - 1).Visible = False

        End With
    End Sub
    Private Sub setComboGrid()
        Dim i As Integer = 0
        With dvData
            For i = 0 To IIf(.ColumnCount - 1 >= cmbShowIndex, cmbShowIndex, .ColumnCount - 1)
                cmbSearch.Items.Add(.Columns(i).HeaderText)
            Next
        End With
        cmbSearch.SelectedIndex = 0
    End Sub


    Private Sub rbCustomer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbCustomer.Click, rbSupplier.Click, rbCustSupp.Click, rbPDCIssued.Click, rbPDCRcvd.Click, rbAllAc.Click, rbCashBank.Click, rbSales.Click, rbPurchase.Click, rbExpence.Click, rdostaff.Click
        Select Case True
            Case rbCustomer.Checked And rbCustomer.Enabled
                strMyQry = AssignAccSQLStr(0, "", 2)
            Case rbSupplier.Checked And rbSupplier.Enabled
                strMyQry = AssignAccSQLStr(1, "", 2)
            Case rbCustSupp.Checked And rbCustSupp.Enabled
                strMyQry = AssignAccSQLStr(2, "", 2)
            Case rbSales.Checked And rbSales.Enabled
                strMyQry = AssignAccSQLStr(3, "", 2)
            Case rbPurchase.Checked And rbPurchase.Enabled
                strMyQry = AssignAccSQLStr(4, "", 2)
            Case rbExpence.Checked And rbExpence.Enabled
                strMyQry = AssignAccSQLStr(5, "", 2)
            Case rbCashBank.Checked And rbCashBank.Enabled
                strMyQry = AssignAccSQLStr(6, "", 2)
            Case rbAllAc.Checked And rbAllAc.Enabled
                strMyQry = AssignAccSQLStr(8, "", 2)
            Case rbPDCRcvd.Checked And rbPDCRcvd.Enabled
                strMyQry = AssignAccSQLStr(9, "", 2)
            Case rbPDCIssued.Checked And rbPDCIssued.Enabled
                strMyQry = AssignAccSQLStr(10, "", 2)
            Case rdostaff.Checked And rdostaff.Enabled
                strMyQry = AssignAccSQLStr(21, "", 2)
        End Select
        FillGrid()
    End Sub

    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        Me.Hide()
        With dvData
            If .Rows.Count = 0 Then Exit Sub
            If CStr(.CurrentRow.Cells(.Columns.Count - 1).Value) <> "" Then
                RaiseEvent doSelect(.CurrentRow.Cells(.Columns.Count - 1).Value.ToString, .CurrentRow.Cells(0).Value.ToString, .CurrentRow.Cells(1).Value.ToString)
            End If
        End With
    End Sub
    Public Sub MoveDown()
        On Error Resume Next
        Dim r As Integer
        With dvData
            If .RowCount < 1 Then Exit Sub
            If dvData.CurrentRow.Index = .RowCount - 1 Then GoTo Slct
            r = dvData.CurrentRow.Index
            If .CurrentRow.Index < .RowCount - 1 Then
                r = r + 1
                .Rows(r).Selected = True
                .CurrentCell = .Item(cmbSearch.SelectedIndex, r)
                .FirstDisplayedScrollingRowIndex() = r
            Else
                Exit Sub
            End If
Slct:
            'MsgBox(dvData.CurrentRow.Index)
        End With
    End Sub

    Public Sub MoveUp()
        Dim r As Integer
        With dvData
            If .RowCount < 1 Then Exit Sub
            If .CurrentRow.Index < 0 Then Exit Sub
            r = .CurrentRow.Index
            If r <> 0 Then
                r = r - 1
                If Not r < 0 Then
                    .Rows(r).Selected = True
                    .CurrentCell = .Item(cmbSearch.SelectedIndex, r)
                    .FirstDisplayedScrollingRowIndex() = r
                End If
            End If
        End With
    End Sub

    Private Sub txtSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
        Select Case e.KeyCode
            Case Keys.Up, Keys.PageUp
                MoveUp()
            Case Keys.Down, Keys.PageDown
                MoveDown()
            Case Keys.Return
                btnSelect_Click(sender, e)
        End Select
    End Sub
    Private Sub SearchGrid()
        If Trim(txtSearch.Text) <> "" Then
            If _vdatatable.Rows.Count = 0 Then Exit Sub
            Dim _qurey As EnumerableRowCollection(Of DataRow)
            If chkSearch.Checked = False Then
                _qurey = From data In _vdatatable.AsEnumerable() Where data(cmbSearch.SelectedIndex).ToString.ToUpper.Contains(UCase(txtSearch.Text)) Select data
            Else
                _qurey = From data In _vdatatable.AsEnumerable() Where data(cmbSearch.SelectedIndex).ToString.StartsWith(txtSearch.Text, StringComparison.OrdinalIgnoreCase) Select data
            End If
            If _qurey.Count > 0 Then
                Dim bDatatable As DataTable = _qurey.CopyToDataTable()
                dvData.DataSource = bDatatable
            End If
        Else
            dvData.DataSource = _vdatatable
        End If
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        SearchGrid()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        FillGrid()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        SearchGrid()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub dvData_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dvData.CellDoubleClick
        btnSelect_Click(btnSelect, New System.EventArgs)
    End Sub

    Private Sub dvData_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dvData.KeyDown
        btnSelect_Click(btnSelect, New System.EventArgs)
    End Sub

    Private Sub rbCustomer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbCustomer.CheckedChanged

    End Sub

    Private Sub rbCustSupp_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbCustSupp.CheckedChanged

    End Sub

    Private Sub rbExpence_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbExpence.CheckedChanged

    End Sub
End Class