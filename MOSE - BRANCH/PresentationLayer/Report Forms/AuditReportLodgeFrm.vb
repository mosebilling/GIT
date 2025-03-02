Public Class AuditReportLodgeFrm
    Private _objJob As New clsJob
    Private _objcmnbLayer As New clsCommon_BL
    Private _dtTable As DataTable
    Private _dtRptTable As DataTable
    Private WithEvents fcheckin As LodgeCheckInFrm
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fRptFormat As RptFormatfrm
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub loadCheckInDetails()
        _objJob.DateFrom = DateValue(cldrStartDate.Value)
        _objJob.DateTo = DateValue(cldrEnddate.Value)
        _dtTable = _objJob.returnLodgeCheckInlistForAudit.Tables(0)
        If rdoall.Checked = False Then
            filter()
        End If
        grdvoucher.DataSource = _dtTable
        gridTotal(_dtTable)
        SetGrid()
    End Sub
    Private Sub SetGrid()
        With grdvoucher
            If .ColumnCount = 0 Then Exit Sub
            SetGridProperty(grdvoucher)
            .Columns("Jobcode").HeaderText = "Code"
            .Columns("jobdate").HeaderText = "Doc Date"
            .Columns("ldgdescription").HeaderText = "Description"
            .Columns("AccDescr").HeaderText = "Customer Name"
            .Columns("Userid").HeaderText = "Created By"
            .Columns("CrdtDate").HeaderText = "Created Date"
            .Columns("closed").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Invoice Amt").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Invoice Amt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Received").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Received").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Balance").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Balance").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Datefrom").Visible = False
            .Columns("Dateto").Visible = False
            .Columns("lnk").Visible = False
            .Columns("jobid").Visible = False
            .Columns("DTYPE").Visible = False
            .Columns("ldgdescription").Width = 200
            .Columns("AccDescr").Width = 200
            .Columns("jobdate").Width = 80
            .Columns("CrdtDate").Width = 150
            .Columns("Jobcode").Width = 100
            setComboGrid()
            'resizeGridColumn(grdItem, 5)
        End With
    End Sub
    Private Sub setComboGrid()
        cmbOrder.Items.Clear()
        Dim i As Integer = 0
        For i = 0 To grdvoucher.ColumnCount - 2
            cmbOrder.Items.Add(grdvoucher.Columns(i).HeaderText)
        Next
        If cmbOrder.Items.Count > 0 Then cmbOrder.SelectedIndex = 0
    End Sub

    Private Sub AuditReportLodgeFrm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        loadCheckInDetails()
    End Sub

    Private Sub btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnload.Click
        loadCheckInDetails()
    End Sub
    Private Sub loadRooms(ByVal roomLdgid As Long, ByVal jobcode As String)
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT [Item Code] Room,Description,checkinDateTime [Check In],checkoutDateTime [Check Out]," & _
                                         "case when isnull(roomstatus,0)=0 then 'Closed' else 'Active' end [Room Status]," & _
                                         "case when isnull(invitemid,0)=0 then 'NO' else 'YES' end [Invoiced?], " & _
                                         "((isnull(rent,0)*(isnull(gst,0)+isnull(cess,0)))/100)+rent TaxPrice,roomLdgid jobid FROM LodgeRoomTb " & _
                                         "INNER JOIN INVITM ON LodgeRoomTb.roomItemid=invitm.itemid " & _
                                         "LEFT JOIN GSTTb ON GSTTb.HSNCode=InvItm.HSNCode " & _
                                         "left join (select ItemId invitemid from ItmInvCmnTb " & _
                                         "left join ItmInvTrTb on ItmInvCmnTb.trid=ItmInvTrTb.TrId " & _
                                         "where [Job Code]='" & jobcode & "' and itemid>0) tr on  LodgeRoomTb.roomItemid=tr.invitemid " & _
                                         "where roomLdgid=" & roomLdgid)
        grdroom.DataSource = dt
        SetGridRoom()
    End Sub
    Private Sub SetGridRoom()
        With grdroom
            If .ColumnCount = 0 Then Exit Sub
            SetGridProperty(grdroom)
            .Columns("Room Status").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Invoiced?").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("TaxPrice").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("TaxPrice").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Check In").DefaultCellStyle.Format = "dd/MM/yyyy hh:mm tt"
            .Columns("Check Out").DefaultCellStyle.Format = "dd/MM/yyyy hh:mm tt"
            .Columns("Check In").Width = 150
            .Columns("Check Out").Width = 150
            .Columns("jobid").Visible = False
        End With
        resizeGridColumn(grdroom, 1)
    End Sub

    Private Sub grdvoucher_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdvoucher.DoubleClick
        With grdvoucher
            If .Rows.Count = 0 Then Exit Sub
            If .CurrentRow Is Nothing Then Exit Sub
            If fcheckin Is Nothing Then fcheckin = New LodgeCheckInFrm
            fcheckin.MdiParent = fMainForm
            fcheckin.Show()
            fcheckin.txtjobcode.Tag = Val(.Item("jobid", .CurrentRow.Index).Value)
            fcheckin.loadLodgeForEdit()
        End With

    End Sub

    Private Sub grdvoucher_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdvoucher.RowEnter
        With grdvoucher
            loadRooms(.Item("jobid", e.RowIndex).Value, .Item("Jobcode", e.RowIndex).Value)
        End With

    End Sub

    Private Sub txtSeq_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSeq.TextChanged
        grdvoucher.DataSource = SearchGrid(_dtTable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, True)
        _dtRptTable = SearchGrid(_dtTable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, True)
        gridTotal(_dtRptTable)
    End Sub

    Private Sub fcheckin_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fcheckin.FormClosed
        fcheckin = Nothing
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Dim RptType As String = ""
        Dim RptCaption As String = ""
        RptType = "LDGAU"
        If chkFormat.Checked Then
            fRptFormat = New RptFormatfrm
            fRptFormat.RptType = RptType
            fRptFormat.ShowDialog()
            fRptFormat = Nothing
        Else
            PrepareReport(RptType)
        End If
    End Sub
    Public Sub PrepareReport(ByVal RptType As String)
        Dim RptName As String
        Dim RptCaption As String = ""
        Dim printername As String = ""
        RptName = getRptDefFlName(RptType, RptCaption, printername)
        If Trim(RptName) <> "" Then
            loadReport(RptName, RptCaption, False)
        End If
    End Sub
    Private Sub loadReport(ByVal FileName As String, ByVal RptCaption As String, ByVal forpint As Boolean)
        If Not frm Is Nothing Then
            frm.Close()
            frm = New ReprtviewNEWfrm
        Else
            frm = New ReprtviewNEWfrm
        End If
        Dim ds As New DataSet
        If _dtRptTable Is Nothing Then
            _objJob.DateFrom = DateValue(cldrStartDate.Value)
            _objJob.DateTo = DateValue(cldrEnddate.Value)
            ds = _objJob.returnLodgeCheckInlistForAudit
        Else
            ds.Tables.Add(_dtRptTable)
        End If
        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = fMainForm
        frm.Text = RptCaption
        frm.Show()
    End Sub
    Private Function filter() As DataTable
        If _dtTable.Rows.Count = 0 Then GoTo nxt
        Dim _qurey As EnumerableRowCollection(Of DataRow)
        If rdonotinvoiced.Checked Then
            _qurey = From data In _dtTable.AsEnumerable() Where CDbl(data("Invoice Amt")) = 0 Select data
        Else
            _qurey = From data In _dtTable.AsEnumerable() Where CDbl(data("balance")) > 0 Select data
        End If
        If _qurey.Count > 0 Then
            _dtTable = _qurey.CopyToDataTable()
        Else
            _dtTable = _dtTable.Clone
        End If
nxt:
        Return _dtTable
    End Function

    Private Sub rdoall_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoall.Click, rdonotinvoiced.Click, rdonotreceived.Click
        loadCheckInDetails()
    End Sub
    Private Sub gridTotal(ByVal RptdtTable As DataTable)
        If RptdtTable.Rows.Count = 0 Then Exit Sub
        Dim drSum As Double
        Dim crSum As Double
        Dim amt As String
        amt = Trim(RptdtTable.Compute("SUM([Invoice Amt])", "") & "")
        If Val(amt) > 0 Then
            drSum = Convert.ToDouble(amt)
        End If
        amt = Trim(RptdtTable.Compute("SUM(Received)", "") & "")
        If Val(amt) > 0 Then
            crSum = Convert.ToDouble(amt)
        End If
        lblTlDebit.Text = Format(drSum, numFormat)
        lblcredit.Text = Format(crSum, numFormat)
        lbldiff.Text = Format(CDbl(lblTlDebit.Text) - CDbl(lblcredit.Text), numFormat)
    End Sub
End Class