Public Class SelectItemBatchFrm
    Public itms As String()
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
        btnadd.Tag = 0
    End Sub
    Public Sub loadBatch()
        searchProductBatch(grdSrch, txtname.Text, Val(txtname.Tag), True, True)
        itms = SelectItmPanel(grdSrch)
    End Sub

    Private Sub SelectItemBatchFrm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtname.Focus()
    End Sub

    Private Sub SelectItemBatchFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
    Public Sub MoveUp()
        Dim r As Integer
        With grdSrch
            If .RowCount < 1 Then Exit Sub
            If .CurrentRow Is Nothing Then GoTo Slct

            If .CurrentRow.Index <= 0 Then GoTo Slct
            r = .CurrentRow.Index
            If r <> 0 Then
                r = r - 1
                If Not r < 0 Then
                    .Rows(r).Selected = True
                    .CurrentCell = .Item(1, r)
                    .FirstDisplayedScrollingRowIndex() = r
                End If
            End If
        End With
Slct:
        itms = SelectItmPanel(grdSrch)
    End Sub
    Public Sub MoveDown()
        On Error Resume Next
        Dim r As Integer
        With grdSrch
            If .RowCount < 1 Then Exit Sub
            If .CurrentRow Is Nothing Then GoTo Slct
            If .CurrentRow.Index = .RowCount - 1 Then GoTo Slct
            r = .CurrentRow.Index
            If .CurrentRow.Index < .RowCount - 1 Then
                r = r + 1
                .Rows(r).Selected = True
                .CurrentCell = .Item(1, r)
                .FirstDisplayedScrollingRowIndex() = r
            Else
                Exit Sub
            End If
Slct:
            itms = SelectItmPanel(grdSrch)
        End With
    End Sub

    Private Sub txtname_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtname.KeyDown
        Select Case e.KeyCode
            Case Keys.Up, Keys.PageUp
                MoveUp()
            Case Keys.Down, Keys.PageDown
                MoveDown()
            Case Keys.Return
                itms = SelectItmPanel(grdSrch)
                Me.Close()
                btnadd.Tag = 1
        End Select
    End Sub

    Private Sub txtname_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtname.TextChanged
        Dim sindex As Integer
        If rdobatch.Checked Then
            sindex = 2
        ElseIf rdomrp.Checked Then
            sindex = 5
        Else
            sindex = 6
        End If
        searchProductBatch(grdSrch, txtname.Text, Val(txtname.Tag), False, True, sindex)
    End Sub

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        Me.Close()
        btnadd.Tag = 1
    End Sub

    Private Sub grdSrch_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSrch.CellContentClick

    End Sub

    Private Sub grdSrch_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSrch.DoubleClick
        itms = SelectItmPanel(grdSrch)
        Me.Close()
        btnadd.Tag = 1
    End Sub

    Private Sub grdSrch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdSrch.KeyDown
        If e.KeyCode = Keys.Return Then
            itms = SelectItmPanel(grdSrch)
            Me.Close()
            btnadd.Tag = 1
        End If
    End Sub
End Class