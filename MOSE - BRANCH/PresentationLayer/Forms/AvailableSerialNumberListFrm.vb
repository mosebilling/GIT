Public Class AvailableSerialNumberListFrm
    Private _objcmnbLayer As New clsCommon_BL
    Private dtTable As DataTable
    Private dtRptTable As DataTable
    Private WithEvents fHistory As New SelectHistory
    Private Sub AvailableSerialNumberListFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        returnAvailableSerialNumber()
        Timer1.Enabled = True
    End Sub
    Private Sub setComboGrid()
        Dim i As Integer = 0
        For i = 0 To grdVoucher.Columns.Count - 1
            cmbcolms.Items.Add(grdVoucher.Columns(i).HeaderText)
        Next
        If cmbcolms.Items.Count > 0 Then cmbcolms.SelectedIndex = 0
    End Sub
    Private Sub btnexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexit.Click
        Me.Close()
    End Sub
    Private Sub returnAvailableSerialNumber()
        Dim rtp As Integer
        Select Case True
            Case rdoAvailable.Checked
                rtp = 0
            Case rdosold.Checked
                rtp = 1
            Case rdoboth.Checked
                rtp = 2
        End Select
        dtTable = _objcmnbLayer.returnAvailableSerialNumber(rtp, "", "")
        grdVoucher.DataSource = dtTable
        setGridHead()
    End Sub
    Private Sub setGridHead()
        SetGridProperty(grdVoucher)
        With grdVoucher
            .Columns("SerialNo").Width = 220
            .Columns("Item Code").Width = 85
            .Columns("IN No").Width = 85
            .Columns("OUT No").Width = 85
            .Columns("Item Name").Width = 200
            .Columns("Supplier Name").Width = 220
            .Columns("Customer Name").Width = 220
            .Columns("OUT Date").Width = 85
            .Columns("OUT Date").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("IN Date").Width = 85
            .Columns("IN Date").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("ItemId").Visible = False
            .Columns("cnt").Visible = False

        End With
        setComboGrid()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        resizeGridColumn(grdVoucher, 2)
    End Sub

    Private Sub AvailableSerialNumberListFrm_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        resizeGridColumn(grdVoucher, 2)
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        dtRptTable = SearchGrid(dtTable, Trim(txtSearch.Text), cmbcolms.SelectedIndex)
        grdVoucher.DataSource = dtRptTable
    End Sub


    Private Sub rdoAvailable_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoAvailable.Click, rdosold.Click, rdoboth.Click
        returnAvailableSerialNumber()
    End Sub
    Private Sub setHistory()
        If fHistory Is Nothing Then
            fHistory = New SelectHistory
        End If
        With fHistory
            .serialno = grdVoucher.Item("Serialno", grdVoucher.CurrentRow.Index).Value
            If fHistory.Visible Then
                .setSerialNumber()
            Else
                .Show()
                .Top = Me.Top + Screen.PrimaryScreen.WorkingArea.Height - .Height + 20
            End If
        End With
    End Sub

    Private Sub btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnload.Click
        If grdVoucher.RowCount = 0 Then Exit Sub
        setHistory()
    End Sub

    Private Sub fHistory_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fHistory.FormClosed
        fHistory = Nothing
    End Sub

    Private Sub grdVoucher_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellClick
        If fHistory Is Nothing Then Exit Sub
        If fHistory.Visible Then setHistory()
    End Sub

  
End Class