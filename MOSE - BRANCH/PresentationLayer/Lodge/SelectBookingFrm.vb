Public Class SelectBookingFrm
    Private _objcmnbLayer As New clsCommon_BL
    Private _objJob As New clsJob
    Private _vtable As DataTable
    Public Event transferBooking(ByVal jobid As Long, ByVal roomids As String)
    Private jobid As Long
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub ldJobdetails()
        Try
            _objJob = New clsJob
            With _objJob
                .Jobid = 0
                .DateFrom = DateValue(cldrStartDate.Value)
                .DateTo = DateValue(cldrEnddate.Value)
                .custcode = 0
                .Dtype = "BKN"
                .Tp = 0
                _vtable = .returnLodge.Tables(0)
            End With
            'grdItem.DataSource = Nothing
            grdItem.DataSource = _vtable
            SetGrid()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub SetGrid()
        With grdItem
            If .ColumnCount = 0 Then Exit Sub
            SetGridProperty(grdItem)
            .Columns("Jobcode").HeaderText = "Code"
            .Columns("jobdate").HeaderText = "Booking Date"
            .Columns("ldgdescription").HeaderText = "Description"
            .Columns("AccDescr").HeaderText = "Customer Name"
            .Columns("Userid").HeaderText = "Created By"
            .Columns("CrdtDate").HeaderText = "Created Date"
            .Columns("closed").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            '.Columns("JobValue").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Datefrom").Visible = False
            .Columns("Dateto").Visible = False
            .Columns("lnk").Visible = False
            .Columns("jobid").Visible = False
            '.Columns("ServiceCost").Visible = False
            '.Columns("ItemCost").Visible = False
            '.Columns("JobCloseDate").Visible = False
            .Columns("ldgdescription").Width = 200
            .Columns("AccDescr").Width = 200
            .Columns("jobdate").Width = 150
            .Columns("CrdtDate").Width = 150
            setComboGrid()
            resizeGridColumn(grdItem, 5)
        End With
    End Sub
    Private Sub setComboGrid()
        cmbOrder.Items.Clear()
        Dim i As Integer = 0
        For i = 0 To grdItem.ColumnCount - 2
            cmbOrder.Items.Add(grdItem.Columns(i).HeaderText)
        Next
        If cmbOrder.Items.Count > 0 Then cmbOrder.SelectedIndex = 0
    End Sub

    Private Sub txtSeq_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeq.TextChanged
        grdItem.DataSource = SearchGrid(_vtable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, False)
    End Sub
    Private Sub Loadrooms(ByVal roomLdgid As Long)
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT case when isnull(tmpldgroomid,0)=0 then 'Y' else '' end Tag,[Item Code],Description," & _
                                         "checkinDateTime [Booking on]," & _
                                         "((isnull(rent,0)*(isnull(gst,0)+isnull(cess,0)))/100)+rent TaxPrice,ldgroomid" & _
                                         ",case when isnull(roomstatus,0)=0 then 'Yes' else 'No' end [Transfered]" & _
                                         ",case when isnull(tmpldgroomid,0)=0 then 'Vacent' else 'Engaged' end [Status]," & _
                                         "isnull(tmpldgroomid,0) ischecked FROM LodgeRoomTb " & _
                                         "INNER JOIN INVITM ON LodgeRoomTb.roomItemid=invitm.itemid " & _
                                         "LEFT JOIN GSTTb ON GSTTb.HSNCode=InvItm.HSNCode " & _
                                        "left join (select ldgroomid tmpldgroomid,roomItemid checkedRoomID from " & _
                                        "LodgeRoomTb where roomstatus=1) checkedIn on " & _
                                        "InvItm.itemid=checkedIn.checkedRoomID where roomLdgid=" & roomLdgid)
        grdroom.DataSource = dt
        SetGridProperty(grdroom)
        resizeGridColumn(grdroom, 2)
        jobid = roomLdgid
        grdroom.Columns("ldgroomid").Visible = False
        grdroom.Columns("ischecked").Visible = False
        grdroom.Columns("tag").Width = 50
        grdroom.Columns("tag").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        grdroom.Columns("TaxPrice").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grdroom.Columns("TaxPrice").DefaultCellStyle.Format = "N" & NoOfDecimal
    End Sub

    Private Sub SelectBookingFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ldJobdetails()
    End Sub

    Private Sub btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnload.Click
        ldJobdetails()
    End Sub

    Private Sub grdItem_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItem.RowEnter
        Loadrooms(Val(grdItem.Item("jobid", e.RowIndex).Value))
    End Sub

    Private Sub grdroom_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdroom.CellClick
        With grdroom
            If e.ColumnIndex = 0 Then
                If Val(.Item("ischecked", e.RowIndex).Value) > 0 Then
                    MsgBox("Room already checked In", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If
                .Item(e.ColumnIndex, e.RowIndex).Value = IIf(.Item(e.ColumnIndex, e.RowIndex).Value = "Y", "", "Y")
            End If
        End With
    End Sub

    Private Sub btntransfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btntransfer.Click
        Dim i As Integer
        Dim roomids As String = ""
        For i = 0 To grdroom.RowCount - 1
            If grdroom.Item("tag", i).Value = "Y" Then
                roomids = roomids & IIf(roomids = "", "", ",") & grdroom.Item("ldgroomid", i).Value
            End If
        Next
        If roomids = "" Then
            MsgBox("Room not found", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        RaiseEvent transferBooking(jobid, roomids)
        Me.Close()
    End Sub
End Class