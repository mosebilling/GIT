Public Class SelectClinicBookingFrm
    Public currentdate As Date
    Private dttable As DataTable
    Private _objcmnbLayer As New clsCommon_BL
    Public Event retrunBooking(ByVal bookingid As Long)
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub SelectClinicBookingFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ldBookings()
    End Sub
    Private Sub ldBookings()
        Dim condition As String
        condition = "bookingFor>='" & Format(DateValue(currentdate), "yyyy/MM/dd") & "' AND bookingFor<='" & Format(DateValue(currentdate), "yyyy/MM/dd") & "' ORDER BY bokkingtime "
        dttable = _objcmnbLayer._fldDatatable("SELECT bookingno [No],bookingFor [Date],ltrim(right (convert(varchar,bokkingtime,100),7)) BookTime,isnull(Alias,'') [OP Number],patientName [Patient Name]," & _
                                              "add1+add2 Address,ClinicVisitBookingTb.Phone,ClinicVisitBookingTb.Doctor,referenceBy Reference," & _
                                              "ClinicVisitBookingTb.bookingid from ClinicVisitBookingTb " & _
                                             "Left join Accmast on ClinicVisitBookingTb.customerid=Accmast.accid " & _
                                             "Left join AccMastAddr on ClinicVisitBookingTb.customerid=AccMastAddr.accountno " & _
                                             "Left join ClinicVistTb on ClinicVisitBookingTb.bookingid=ClinicVistTb.bookingid " & _
                                             " where isnull(visitid,0)=0 AND " & condition)
        grdlist.DataSource = dttable
        SetGridHeadList()
        resizeGridColumn(grdlist, 4)
    End Sub
    Sub SetGridHeadList()
        With grdlist
            SetGridProperty(grdlist)
            .Columns("No").Width = 50
            .Columns("Date").Width = 50
            .Columns("Patient Name").Width = 100
            '.Columns("Booking For").Width = 100
            .Columns("OP Number").Width = 100
            .Columns("Address").Width = 100
            .Columns("Doctor").Width = 100
            .Columns("Reference").Width = 100
            .Columns("bookingid").Visible = False
        End With
        setComboGrid()
    End Sub
    Private Sub setComboGrid()
        cmbOrder.Items.Clear()
        Dim i As Integer = 0
        For i = 0 To grdlist.ColumnCount - 2
            cmbOrder.Items.Add(grdlist.Columns(i).HeaderText)
        Next
        If cmbOrder.Items.Count > 0 Then cmbOrder.SelectedIndex = 3
    End Sub

    Private Sub txtSeq_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSeq.KeyDown
        If e.KeyCode = Keys.Return Then returnBookingid()
    End Sub

    Private Sub txtSeq_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSeq.TextChanged
        grdlist.DataSource = SearchGrid(dttable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
    End Sub
    Private Sub grdlist_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdlist.DoubleClick
        returnBookingid()
    End Sub
    Private Sub returnBookingid()
        If grdlist.RowCount = 0 Then Exit Sub
        If grdlist.CurrentRow Is Nothing Then Exit Sub
        RaiseEvent retrunBooking(Val(grdlist.Item("bookingid", grdlist.CurrentRow.Index).Value))
        Me.Close()
    End Sub
End Class