Public Class PatientFollowupFrm
    Private dttable As DataTable
    Private _objcmnbLayer As New clsCommon_BL
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub ldVisits()
        dttable = _objcmnbLayer._fldDatatable("SELECT calldate [Date],Alias [OP Number],AccDescr [Patient Name],Address1+Address2 Address,Phone,Pupose Purpose,salesman Doctor," & _
                                         "recallid from CustomerFollowupTb " & _
                                         "Left join Accmast on CustomerFollowupTb.customerid=Accmast.accid " & _
                                         "Left join AccMastAddr on CustomerFollowupTb.customerid=AccMastAddr.accountno " & _
                                         " where isnull(isclosed,0)=0 AND " & _
                                         "calldate>='" & Format(DateValue(dtpfrom.Value), "yyyy/MM/dd") & "' AND calldate<='" & Format(DateValue(dtpto.Value), "yyyy/MM/dd") & "' ORDER BY calldate")
        grdlist.DataSource = dttable
        SetGridHeadList()
        resizeGridColumn(grdlist, 5)
    End Sub
    Sub SetGridHeadList()
        With grdlist
            SetGridProperty(grdlist)
            .Columns("Date").Width = 100
            .Columns("OP Number").Width = 100
            .Columns("Address").Width = 120
            .Columns("Patient Name").Width = 150
            .Columns("Doctor").Width = 100
            .Columns("purpose").Width = 150
            .Columns("recallid").Visible = False
        End With
    End Sub

    Private Sub ldclosedVisits()
        dttable = _objcmnbLayer._fldDatatable("SELECT calldate [Date],Alias [OP Number],AccDescr [Patient Name]," & _
                                              "Address1+Address2 Address,Phone,Pupose Purpose,salesman Doctor,closeddate [Closed Date],comment," & _
                                             "recallid from CustomerFollowupTb " & _
                                             "Left join Accmast on CustomerFollowupTb.customerid=Accmast.accid " & _
                                             "Left join AccMastAddr on CustomerFollowupTb.customerid=AccMastAddr.accountno " & _
                                             " where isnull(isclosed,0)=1 AND " & _
                                             "closeddate>='" & Format(DateValue(dtpdate1.Value), "yyyy/MM/dd") & "' AND closeddate<='" & Format(DateValue(dtpdate2.Value), "yyyy/MM/dd") & "' ORDER BY closeddate Desc")
        grdclosedlist.DataSource = dttable
        SetGridClosedHeadList()
        resizeGridColumn(grdclosedlist, 6)
    End Sub
    Sub SetGridClosedHeadList()
        With grdclosedlist
            SetGridProperty(grdclosedlist)
            .Columns("Date").Width = 100
            .Columns("OP Number").Width = 100
            .Columns("Patient Name").Width = 150
            .Columns("Address").Width = 100
            .Columns("Doctor").Width = 100
            .Columns("purpose").Width = 250
            .Columns("comment").Width = 250
            .Columns("recallid").Visible = False
        End With
        setComboGrid()
    End Sub
    Private Sub setComboGrid()
        cmbOrder.Items.Clear()
        Dim i As Integer = 0
        For i = 0 To grdlist.ColumnCount - 2
            cmbOrder.Items.Add(grdlist.Columns(i).HeaderText)
        Next
        If cmbOrder.Items.Count > 0 Then cmbOrder.SelectedIndex = 1
    End Sub

    Private Sub TabControl1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.Click
        If TabControl1.SelectedIndex = 1 Then
            ldclosedVisits()
        End If
    End Sub

    Private Sub btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnload.Click
        ldclosedVisits()
    End Sub

    Private Sub btnloadcalls_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnloadcalls.Click
        ldVisits()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        resizeGridColumn(grdlist, 5)
    End Sub

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        Dim frm As New AddFollowupFrm
        frm.ShowDialog()
        ldVisits()
    End Sub

    Private Sub PatientFollowupFrm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ldVisits()
        Timer1.Enabled = True
    End Sub

    Private Sub grdlist_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdlist.DoubleClick
        If grdlist.RowCount = 0 Then Exit Sub
        Dim frm As New AddFollowupFrm
        frm.dtpcalldate.Tag = Val(grdlist.Item("recallid", grdlist.CurrentRow.Index).Value)
        frm.ShowDialog()
        ldclosedVisits()
        ldVisits()
    End Sub

    Private Sub grdclosedlist_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdclosedlist.DoubleClick
        If grdclosedlist.RowCount = 0 Then Exit Sub
        Dim frm As New AddFollowupFrm
        frm.dtpcalldate.Tag = Val(grdclosedlist.Item("recallid", grdclosedlist.CurrentRow.Index).Value)
        frm.ShowDialog()
        ldVisits()
    End Sub

    Private Sub txtSeq_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSeq.TextChanged
        grdclosedlist.DataSource = SearchGrid(dttable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
    End Sub

    Private Sub cmbOrder_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbOrder.SelectedIndexChanged
        txtSeq.Focus()
    End Sub

    Private Sub grdlist_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdlist.CellContentClick

    End Sub
End Class