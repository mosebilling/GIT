Public Class AddRoomsFrm
    Private chgbyprg As Boolean
    Public ItemId As Long
    Public selectedRooms As String
    Private gst As Double
    Private cess As Double
    Private hsncode As String
    Public rent As Double
    Private checkoutEstimateDateTime As Date
    Dim dtTable As DataTable
    Dim srchTxtEdtd As Boolean
    Dim srchTxtId As Single
    Dim srchOnce As Boolean
    Dim _objcmnbLayer As New clsCommon_BL
    Private chgbyprgAmt As Boolean
    Public isgstCustomer As Boolean
    Public isBooking As Boolean
    Public jobid As Long
    Public Event addOrEditRoom(ByVal itemid As Long, ByVal roomDescr As String, ByVal chkdate As Date, _
                               ByVal estDays As Integer, ByVal rindex As Integer, ByVal category As String, _
                               ByVal rent As Double, ByVal gst As Double, ByVal cess As Double, ByVal hsncode As String)
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub txtestNumber_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtestNumber.Validated
        If Val(txtestNumber.Text) = 0 Then lblestimated.Text = DateValue(dtpcheckin.Value) : Exit Sub
        lblestimated.Text = DateAdd(DateInterval.Day, Val(txtestNumber.Text), DateValue(dtpcheckin.Value))
    End Sub

    Private Sub AddRoomsFrm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtSearch.Focus()
    End Sub

    Private Sub AddRoomsFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        chgbyprg = True
        FillGrid()
        If ItemId > 0 Then
            btnupdate.Text = "Edit"
            If rdoac.Tag = "AC" Then
                rdoac.Checked = True
            Else
                rdononac.Checked = True
            End If
        Else
            txtestNumber.Text = 1
            If Val(txtestNumber.Text) = 0 Then lblestimated.Text = "" : Exit Sub
            lblestimated.Text = DateAdd(DateInterval.Day, Val(txtestNumber.Text), DateValue(dtpcheckin.Value))
        End If
    End Sub
    Private Sub FillGrid(Optional ByVal isfromclick As Boolean = False)
        'grdPack.VirtualMode = True
        Dim strQry As String
        grdPack.DataSource = Nothing
        strQry = "SELECT * FROM (SELECT case when isnull(roomLockStatus,0)=0 then case when isnull(isActive,0) =0 then 'Vacant' Else 'Engaged' end else " & _
        "case when isnull(roomLockStatus,0)=1 then 'Cleaning' Else 'Not Available' end end RoomStatus," & _
                "[Item Code] [Room],Model [Room Type],Description,case when isnull(Make,0)=1 then 'AC' Else 'Non AC' END [Ac/NonAc],UnitPrice Rent,UnitPriceWS [Non A/C],(((isnull(IGST,0)+ISNULL(vat,0))*UnitPrice)/100)+UnitPrice [Tax Price]," & _
                "(((isnull(IGST,0)+ISNULL(vat,0))*UnitPriceWS)/100)+UnitPriceWS [Tax Non A/C]," & _
                 "itemid ,case when " & ItemId & "=itemid then 0 else 1 end ord,ISNULL(vat,0) cess,isnull(IGST,0) Gst FROM InvItm" & _
                 " LEFT JOIN VatMasterTb ON VatMasterTb.vatid=InvItm.vatid " & _
                 " LEFT JOIN (Select sum(roomstatus)isActive,roomItemid from LodgeRoomTb where isnull(roomstatus,0)<2 group by roomItemid ) roomTb ON InvItm.Itemid=roomTb.roomItemid " & _
                 " LEFT JOIN GSTTb ON GSTTb.HSNCode=InvItm.HSNCode where itemCategory='room' " & selectedRooms & " ) Itms Order by ord,RoomStatus DESC"
        dtTable = _objcmnbLayer._fldDatatable(strQry)
        Dim dtTemp As DataTable
        If (rdovacent.Checked And ItemId = 0) Or (isfromclick And rdovacent.Checked) Then
            'dtTemp = SearchGrid(dtTable, "Vacant", 0)
            dtTemp = Roomfilter()
        Else
            dtTemp = dtTable
        End If
        grdPack.DataSource = dtTemp
        For i = 0 To grdPack.RowCount - 1
            With grdPack
                If .Item("RoomStatus", i).Value = "Cleaning" Then
                    .Rows(i).DefaultCellStyle.BackColor = Color.GreenYellow
                ElseIf .Item("RoomStatus", i).Value = "Not Available" Then
                    .Rows(i).DefaultCellStyle.BackColor = Color.Red
                ElseIf .Item("RoomStatus", i).Value = "Engaged" Then
                    .Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
                End If
            End With
        Next
        If grdPack.RowCount > 0 Then
            loadRoom(0)
        End If
        SetGridHead()
    End Sub
    Private Function Roomfilter() As DataTable
        Dim bDatatable As DataTable
        If dtTable.Rows.Count = 0 Then bDatatable = dtTable.Clone : GoTo nxt
        Dim _qurey As EnumerableRowCollection(Of DataRow)
        _qurey = From data In dtTable.AsEnumerable() Where data(0).ToString.ToUpper.Contains(UCase("Vacant")) Or data(0).ToString.ToUpper.Contains(UCase("Cleaning")) Select data
        If _qurey.Count > 0 Then
            bDatatable = _qurey.CopyToDataTable()
        Else
            bDatatable = dtTable.Clone
        End If
nxt:
        Return bDatatable
    End Function
    Private Sub loadRoom(ByVal rindex As Integer)
        chgbyprg = True
        gst = 0
        cess = 0
        If grdPack.Rows.Count > 0 Then
            lblroom.Text = "Room Number : " & grdPack.Item("Room", rindex).Value
            lblDetails.Text = "Room Type: " & grdPack.Item("Room Type", rindex).Value & vbCrLf
            lblDetails.Text = lblDetails.Text & "Description: " & grdPack.Item("Description", rindex).Value & vbCrLf
            'lblDetails.Text = lblDetails.Text & " Estimated Date : " & grdPack.Item("checkoutEstimateDateTime", rindex).Value
            'If Trim(grdPack.Item("checkoutEstimateDateTime", rindex).Value & "") <> "" Then
            '    checkoutEstimateDateTime = DateValue(grdPack.Item("checkoutEstimateDateTime", rindex).Value)
            'End If

            lblcategory.Text = grdPack.Item("Ac/NonAc", rindex).Value & " Room"

            lblrent.Text = Format(CDbl(grdPack.Item("Rent", rindex).Value), numFormat)
            lbltaxprice.Text = Format(CDbl(grdPack.Item("Tax Price", rindex).Value), numFormat)
            lblnonacrent.Text = Format(CDbl(grdPack.Item("Non A/C", rindex).Value), numFormat)
            If rent = 0 Then
                NumSalesPrice.Text = Format(CDbl(grdPack.Item("Rent", rindex).Value), numFormat)
                'txtpriceWtax.Text = Format(CDbl(grdPack.Item("Tax Price", rindex).Value), numFormat)
            Else
                NumSalesPrice.Text = Format(rent, numFormat)

            End If

            lblnotacTaxprice.Text = Format(CDbl(grdPack.Item("Tax Non A/C", rindex).Value), numFormat)
            If grdPack.Item("Ac/NonAc", rindex).Value = "AC" Then
                plnonac.Visible = True
                rdoac.Visible = True
            Else
                plnonac.Visible = False
                rdoac.Visible = False
            End If
            '
            gst = getGst(True)
            If gst < 0 Then
                gst = Val(grdPack.Item("Gst", rindex).Value)
                lbltax.Text = "GST : " & grdPack.Item("Gst", rindex).Value & "%, Cess : " & grdPack.Item("cess", rindex).Value & "%"
            Else
                lbltax.Text = "GST : " & gst & "%, Cess : " & cess & "%"
            End If
            cess = Val(grdPack.Item("cess", rindex).Value)
            'lbltax.Tag = Val(grdPack.Item("Gst", rindex).Value) + IIf(isgstCustomer, 0, Val(grdPack.Item("cess", rindex).Value))
            cess = IIf(isgstCustomer Or gst = 0, 0, cess)
            lbltax.Tag = gst + cess
            lblDetails.Tag = Val(grdPack.Item("itemid", rindex).Value)
            lblstatus.Text = grdPack.Item("RoomStatus", rindex).Value
            If grdPack.Item("Ac/NonAc", rindex).Value = "AC" Then
                rdoac.Checked = True
            Else
                rdononac.Checked = True
            End If
            calculateTaxFromUnitPrice(True)

        End If
        chgbyprg = False
    End Sub
    Private Function getGst(ByVal fromUnitpice As Boolean) As Double
        Dim dt As DataTable
        Dim gst As Double
        Dim salesPrice As Double
        If fromUnitpice Then
            salesPrice = CDbl(NumSalesPrice.Text)
        Else
            salesPrice = CDbl(txtpriceWtax.Text)
        End If
        dt = _objcmnbLayer._fldDatatable("select * from LodgeSettingsTb")
        If dt.Rows.Count > 0 Then
            If CDbl(salesPrice) < dt(0)("taxableAmount") Then
                dt = _objcmnbLayer._fldDatatable("select IGST,hsncode from GSTTb where hsncode='" & dt(0)("taxablenonHsnCode") & "'")
            ElseIf CDbl(salesPrice) < dt(0)("taxableAmount1") Then
                dt = _objcmnbLayer._fldDatatable("select IGST,hsncode from GSTTb where hsncode='" & dt(0)("taxableHsnCode") & "'")
            Else
                dt = _objcmnbLayer._fldDatatable("select IGST,hsncode from GSTTb where hsncode='" & dt(0)("taxableHsnCode1") & "'")
            End If
            If dt.Rows.Count > 0 Then
                gst = dt(0)("IGST")
                hsncode = dt(0)("hsncode")
            Else
                GoTo els
            End If
        Else
els:
            gst = -1
        End If
        Return gst
    End Function
    Private Sub SetGridHead()
        With grdPack
            SetGridProperty(grdPack)

            .Columns("Room Type").Width = 200
            .Columns("Room").Width = 70
            .Columns("Description").Width = 250
            '.Columns("Ac/NonAc").Frozen = True

            .Columns("Rent").Width = 70
            .Columns("Rent").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Rent").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns("Non A/C").Width = 100
            .Columns("Non A/C").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Non A/C").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns("Tax Price").Width = 100
            .Columns("Tax Price").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Tax Price").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns("Tax Non A/C").Width = 100
            .Columns("Tax Non A/C").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Tax Non A/C").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            '.Columns(ItemId).HeaderText = "ItemId"
            .Columns("itemid").Visible = False
            .Columns("ord").Visible = False
            .Columns("cess").Visible = True
            .Columns("gst").Visible = False
            '.Columns("checkoutEstimateDateTime").HeaderText = "Est. Date"

            cmbSearch.Items.Clear()
            Dim i As Integer = 0
            For i = 0 To grdPack.ColumnCount - 1
                cmbSearch.Items.Add(grdPack.Columns(i).HeaderText)
            Next
            'resizeGridColumn(grdPack, TrDescr)
            cmbSearch.SelectedIndex = 1
        End With
    End Sub

    Private Sub txtSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
        Select Case e.KeyCode
            Case Keys.Up, Keys.PageUp
                MoveUp()
            Case Keys.Down, Keys.PageDown
                MoveDown()
            Case Keys.Return
                dtpcheckin.Focus()
        End Select
    End Sub
    Public Sub MoveUp()
        Dim r As Integer
        With grdPack
            If .RowCount < 1 Then Exit Sub
            If .CurrentRow.Index < 0 Then Exit Sub
            r = .CurrentRow.Index
            If r <> 0 Then
                r = r - 1
                If Not r < 0 Then
                    .Rows(r).Selected = True
                    .CurrentCell = .Item(cmbSearch.SelectedIndex, r)
                    .FirstDisplayedScrollingRowIndex() = r
                    loadRoom(r)
                End If
            End If
        End With
    End Sub
    Public Sub MoveDown()
        On Error Resume Next
        Dim r As Integer
        With grdPack
            If .RowCount < 1 Then Exit Sub
            If .CurrentRow.Index = .RowCount - 1 Then GoTo Slct
            r = .CurrentRow.Index
            If .CurrentRow.Index < .RowCount - 1 Then
                r = r + 1
                .Rows(r).Selected = True
                .CurrentCell = .Item(cmbSearch.SelectedIndex, r)
                .FirstDisplayedScrollingRowIndex() = r
                loadRoom(r)
            Else
                Exit Sub
            End If
Slct:
            'MsgBox(dvData.CurrentRow.Index)
        End With
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        grdPack.DataSource = SearchGrid(dtTable, Trim(txtSearch.Text), 1, Not chkSearch.Checked)
        loadRoom(0)
    End Sub

    Private Sub dtpcheckin_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtpcheckin.KeyDown, txtestNumber.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        addRoom()
    End Sub
    Private Sub addRoom()
        If Val(txtestNumber.Text) = 0 Then
            MsgBox("Invalid Days", MsgBoxStyle.Exclamation)
            txtestNumber.Focus()
            Exit Sub
        End If
        If lblstatus.Text = "Engaged" And ItemId <> Val(lblDetails.Tag) Then
            MsgBox("Room not available", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If lblstatus.Text = "Cleaning" And ItemId <> Val(lblDetails.Tag) Then
            MsgBox("Room not available", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If lblstatus.Text = "Not Available" And ItemId <> Val(lblDetails.Tag) Then
            MsgBox("Room not available", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select  checkoutEstimateDateTime,roomItemid  from LodgeRoomTb " & _
                                         "where isnull(roomstatus,0)>0 and roomItemid=" & Val(lblDetails.Tag) & " AND roomLdgid<>" & jobid & _
                                         " AND convert(date,checkinDateTime)<='" & Format(DateValue(dtpcheckin.Value), "yyyy/MM/dd") & "'" & _
                                         " AND convert(date,checkoutEstimateDateTime)>='" & Format(DateValue(dtpcheckin.Value), "yyyy/MM/dd") & "'" & _
                                         " UNION ALL " & _
                                         "Select  checkoutEstimateDateTime,roomItemid  from LodgeRoomTb " & _
                                         "where isnull(roomstatus,0)>0 and roomItemid=" & Val(lblDetails.Tag) & " AND roomLdgid<>" & jobid & _
                                         " AND convert(date,checkinDateTime)<='" & Format(DateValue(lblestimated.Text), "yyyy/MM/dd") & "'" & _
                                         " AND convert(date,checkoutEstimateDateTime)>='" & Format(DateValue(lblestimated.Text), "yyyy/MM/dd") & "'" & _
                                         " UNION ALL " & _
                                         "Select  checkoutEstimateDateTime,roomItemid  from LodgeRoomTb " & _
                                         "where isnull(roomstatus,0)>0 and roomItemid=" & Val(lblDetails.Tag) & " AND roomLdgid<>" & jobid & _
                                         " AND convert(date,checkinDateTime)>='" & Format(DateValue(dtpcheckin.Value), "yyyy/MM/dd") & "'" & _
                                         " AND convert(date,checkoutEstimateDateTime)<='" & Format(DateValue(lblestimated.Text), "yyyy/MM/dd") & "'")
        If dt.Rows.Count > 0 Then
            MsgBox("Room not available upto " & dt(dt.Rows.Count - 1)("checkoutEstimateDateTime"), MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        RaiseEvent addOrEditRoom(Val(lblDetails.Tag), txtdescription.Text, dtpcheckin.Value, Val(txtestNumber.Text), _
                                 Val(btnupdate.Tag), IIf(rdoac.Checked, "AC", "Non AC"), CDbl(NumSalesPrice.Text), gst, cess, hsncode)
    End Sub

    Private Sub txtestNumber_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtestNumber.KeyPress
        NumericTextOnKeypress(txtestNumber, e, chgbyprg, "0")
    End Sub

    Private Sub grdPack_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdPack.DoubleClick
        addRoom()
    End Sub

    Private Sub grdPack_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPack.RowEnter
        loadRoom(e.RowIndex)
    End Sub

    Private Sub rdovacent_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdovacent.CheckedChanged

    End Sub

    Private Sub rdovacent_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdovacent.Click, rdoall.Click
        FillGrid(True)
    End Sub

    Private Sub txtdescription_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtdescription.KeyDown
        If e.KeyCode = Keys.Return Then
            If txtdescription.Text <> "" Then
                If Mid(txtdescription.Text, Len(txtdescription.Text) - 1) = vbCrLf Then
                    NumSalesPrice.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub NumSalesPrice_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles NumSalesPrice.KeyPress
        NumericTextOnKeypress(sender, e, chgbyprgAmt, numFormat)
    End Sub
    Private Sub calculateTaxFromUnitPrice(ByVal fromUnitpice As Boolean, Optional ByVal ischange As Boolean = False)

        If grdPack.CurrentRow Is Nothing Then Exit Sub
        chgbyprg = True
        If Val(txtpriceWtax.Text & "") = 0 Then txtpriceWtax.Text = 0
        If Val(NumSalesPrice.Text) = 0 Then NumSalesPrice.Text = 0
        Dim tax As Double
        If ischange Then
            gst = getGst(fromUnitpice)
            If gst < 0 Then
                gst = Val(grdPack.Item("Gst", grdPack.CurrentRow.Index).Value)
            End If
        End If
        cess = Val(grdPack.Item("cess", grdPack.CurrentRow.Index).Value)
        cess = IIf(isgstCustomer Or gst = 0, 0, cess)
        lbltax.Text = "GST : " & gst & "%, Cess : " & cess & "%"
        lbltax.Tag = gst + cess
        tax = Val(lbltax.Tag)
        If Not fromUnitpice Then
            NumSalesPrice.Text = (CDbl(txtpriceWtax.Text) * 100) / (tax + 100)
            NumSalesPrice.Text = Format(CDbl(NumSalesPrice.Text), numFormat)
        Else
            txtpriceWtax.Text = Format(CDbl(NumSalesPrice.Text) + ((CDbl(NumSalesPrice.Text) * tax) / 100), numFormat)
        End If
        lblgstamt.Text = "GST : " & Format((CDbl(NumSalesPrice.Text) * Val(gst)) / 100, numFormat) & vbCrLf
        lblgstamt.Text = lblgstamt.Text & "KFC : " & Format((CDbl(NumSalesPrice.Text) * Val(cess)) / 100, numFormat)
        chgbyprg = False
    End Sub

    Private Sub NumSalesPrice_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumSalesPrice.TextChanged
        'If chgbyprgAmt Then Exit Sub
        If chgbyprg Then Exit Sub
        calculateTaxFromUnitPrice(True, True)
    End Sub

    Private Sub txtpriceWtax_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtpriceWtax.KeyPress
        NumericTextOnKeypress(sender, e, chgbyprgAmt, numFormat)
    End Sub

    Private Sub txtpriceWtax_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpriceWtax.TextChanged
        'If chgbyprgAmt Then Exit Sub
        If chgbyprg Then Exit Sub
        calculateTaxFromUnitPrice(False, True)
    End Sub

    Private Sub dtpcheckin_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpcheckin.ValueChanged
        If Val(txtestNumber.Text) = 0 Then lblestimated.Text = "" : Exit Sub
        lblestimated.Text = DateAdd(DateInterval.Day, Val(txtestNumber.Text), DateValue(dtpcheckin.Value))
    End Sub

   

    Private Sub rdononac_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdononac.Click, rdoac.Click
        If rdoac.Visible Then
            If rdoac.Checked Then
                NumSalesPrice.Text = Format(CDbl(lblrent.Text), numFormat)
            Else
                NumSalesPrice.Text = Format(CDbl(lblnonacrent.Text), numFormat)
            End If
        Else
            NumSalesPrice.Text = Format(CDbl(lblrent.Text), numFormat)
        End If

    End Sub
End Class