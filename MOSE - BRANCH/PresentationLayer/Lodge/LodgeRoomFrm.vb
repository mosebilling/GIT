Public Class LodgeRoomFrm
    'object variable
    Dim _objcmnbLayer As New clsCommon_BL
    Dim _objItmMast As New clsItemMast_BL
    Private changeBypgm As Boolean
    Private chgbyprgAmt As Boolean
    Private WithEvents fcheckin As LodgeCheckInFrm
    Private _vtable As DataTable
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub LodgeRoomFrm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtCode.Focus()
    End Sub
    Private Sub ldVat()
        Dim dt As DataTable
        Dim i As Integer
        dt = _objcmnbLayer._fldDatatable("SELECT vatcode FROM VatMasterTb")
        cmbtax.Items.Clear()
        cmbtax.Items.Add("")
        For i = 0 To dt.Rows.Count - 1
            cmbtax.Items.Add(dt(i)("vatcode"))
        Next
    End Sub
    Private Sub LodgeRoomFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'loadRooms()
        ldHSN()
        ldVat()
        If userType = 0 Or userType = 2 Then
            btnRemove.Tag = 1
            BtnUpdate.Tag = 1
        Else
            btnRemove.Tag = IIf(getRight(198, CurrentUser), 1, 0)
            BtnUpdate.Tag = IIf(getRight(196, CurrentUser), 1, 0)
        End If
    End Sub
    Private Sub ldHSN()
        Dim ObjLocationlist As New List(Of String)
        ObjLocationlist = toLoadAutoFillListItems("HSNCode", "GSTTb")
        Call toAssignDownListToText(txthsncode, ObjLocationlist) '
    End Sub
    Private Function chkFields() As Boolean
        Dim _vdtTable As DataTable
        If Trim(txtCode.Text) = "" Then
            MsgBox("'Room Number' Cannot be Blank !", MsgBoxStyle.Exclamation)
            txtCode.Focus()
            GoTo err
        End If
        If Val(txtCode.Tag) > 0 Then
            _vdtTable = _objcmnbLayer._fldDatatable("Select ItemId from InvItm Where ItemId= " & Val(txtCode.Tag))
            If _vdtTable.Rows.Count = 0 Then
                MsgBox("Selected Base Item Not Found !", MsgBoxStyle.Exclamation)
                GoTo err
            End If
        End If
        If txtTrDescr.Text = "" Then
            MsgBox("'Description' Cannot be Blank !", MsgBoxStyle.Exclamation)
            txtTrDescr.Focus()
            GoTo err
        End If
        If cmbtype.Text = "" Then
            MsgBox("'Room Type' Cannot be Blank !", MsgBoxStyle.Exclamation)
            cmbtype.Focus()
            GoTo err
        End If
        _vdtTable = _objcmnbLayer._fldDatatable("Select ItemId from InvItm Where [Item Code] ='" & MkDbSrchStr(txtCode.Text) & "' and ItemId <>" & Val(txtCode.Tag))
        If _vdtTable.Rows.Count > 0 Then
            MsgBox("Room Number Already Exist !", MsgBoxStyle.Exclamation)
            txtCode.Focus()
            GoTo err
        End If
        chkFields = True
        Exit Function
Err:
        chkFields = False
    End Function
    Private Sub setValue()
        _objItmMast.ItemId = Val(txtCode.Tag)
        _objItmMast.ItemCode = Trim(txtCode.Text)
        _objItmMast.Descr = Trim(txtTrDescr.Text)
        _objItmMast.Unit = ""
        _objItmMast.hsncode = txthsncode.Text
        _objItmMast.Model = cmbtype.Text
        _objItmMast.Make = IIf(rdoac.Checked, 1, 0)
        If Val(txtwithoutac.Text) = 0 Then txtwithoutac.Text = 0
        If rdoac.Checked Then
            _objItmMast.WSalesPrice = CDbl(txtwithoutac.Text)
        Else
            _objItmMast.WSalesPrice = 0
        End If
        _objItmMast.Category = "room"
        If Val(NumSalesPrice.Text) = 0 Then NumSalesPrice.Text = 0
        _objItmMast.salesPrice = CDbl(NumSalesPrice.Text)

        If Val(txtCode.Tag) > 0 Then
            _objItmMast.LstModiBy = CurrentUser
            _objItmMast.LstModiDt = Date.Now
            _objItmMast.CreatedDt = Date.Now
            _objItmMast.Createdby = Trim(CurrentUser)
        Else
            _objItmMast.LstModiBy = ""
            _objItmMast.Createdby = Trim(CurrentUser)
            _objItmMast.CreatedDt = Date.Now
            _objItmMast.LstModiDt = Date.Now
        End If
        _objItmMast.Ismodi = (Val(txtCode.Tag) > 0)
        _objItmMast.vat = cmbtax.Text
    End Sub

    Private Sub BtnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUpdate.Click
        If Val(BtnUpdate.Tag) = 0 Then
            MsgBox("This user do not have rights to update", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If chkFields() Then
            setValue()
            _objItmMast._saveItemMast()
            MsgBox("Item Created Successfully", MsgBoxStyle.Information)
            clearControls()
            loadRooms()
        End If
    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            Const WM_KEYDOWN As Integer = &H100
            Const WM_KEYUP As Integer = &H101
            'msg.ToString()
            If msg.Msg = WM_KEYDOWN Or msg.Msg = WM_KEYUP Then
                If msg.WParam.ToInt32() = CInt(Keys.F8) Then
                    BtnUpdate_Click(BtnUpdate, New System.EventArgs)
                End If
            End If
ctn:
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function
    Private Sub loadRooms()
        Try
            _vtable = _objcmnbLayer._fldDatatable("SELECT [Item Code] [Room Number],Description,UnitPrice,(((isnull(IGST,0)+ISNULL(vat,0))*UnitPrice)/100)+UnitPrice [Tax Price]," & _
                                             "Model [Room Type],case when isnull(Make,0)=1 then 'AC' Else 'Non AC' END [Ac/NonAc]," & _
                                             "case when isnull(roomLockStatus,0)=0 then case when isnull(isActive,0) =0 then 'Vacent' Else 'Engaged' end else " & _
                                             "case when isnull(roomLockStatus,0)=1 then 'Cleaning' Else 'Not Available' end end RoomStatus," & _
                                             "checkoutEstimateDateTime [Checkout Est. Date],Remarks,itemid FROM InvItm" & _
                                              " LEFT JOIN VatMasterTb ON VatMasterTb.vatid=InvItm.vatid " & _
                                             " LEFT JOIN (Select sum(roomstatus)isActive,roomItemid from LodgeRoomTb left join LodgeTb on LodgeTb.jobid=LodgeRoomTb.roomLdgid where Dtype='CHI' group by roomItemid ) roomTb ON InvItm.Itemid=roomTb.roomItemid " & _
                                             " LEFT JOIN (Select checkoutEstimateDateTime,roomItemid from LodgeRoomTb " & _
                                             " where isnull(roomstatus,0)=1) activeroomTb ON InvItm.Itemid=activeroomTb.roomItemid " & _
                                             " LEFT JOIN GSTTb ON GSTTb.HSNCode=InvItm.HSNCode where itemCategory='room'")
            grdItem.DataSource = _vtable
            SetGridHead()
            Dim i As Integer
            Dim dtTypes As DataTable
            dtTypes = _objcmnbLayer._fldDatatable("select Model from invitm group by Model")
            cmbtype.Items.Clear()
            For i = 0 To dtTypes.Rows.Count - 1
                If dtTypes(i)("Model") & "" <> "" Then
                    cmbtype.Items.Add(dtTypes(i)("Model"))
                End If
            Next
            For i = 0 To grdItem.RowCount - 1
                With grdItem
                    If .Item("RoomStatus", i).Value = "Cleaning" Then
                        .Rows(i).DefaultCellStyle.BackColor = Color.GreenYellow
                    ElseIf .Item("RoomStatus", i).Value = "Not Available" Then
                        .Rows(i).DefaultCellStyle.BackColor = Color.Red
                    ElseIf .Item("RoomStatus", i).Value = "Engaged" Then
                        .Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
                    End If
                End With
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub setComboGrid()
        cmbOrder.Items.Clear()
        Dim i As Integer = 0
        For i = 0 To grdItem.ColumnCount - 2
            cmbOrder.Items.Add(grdItem.Columns(i).HeaderText)
        Next
        If cmbOrder.Items.Count > 0 Then cmbOrder.SelectedIndex = 0
    End Sub
    Private Sub SetGridHead()
        With grdItem
            SetGridProperty(grdItem)
            .Columns("itemid").Visible = False
            .Columns("UnitPrice").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("UnitPrice").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Tax Price").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Tax Price").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Checkout Est. Date").Width = 100
        End With
        setComboGrid()
        resizeGridColumn(grdItem, 1)
    End Sub

    Private Sub txtCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCode.KeyDown, cmbtype.KeyDown, NumSalesPrice.KeyDown, txthsncode.KeyDown, txtpricetaxwithoutac.KeyDown, txtwithoutac.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub NumSalesPrice_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles NumSalesPrice.KeyPress
        NumericTextOnKeypress(NumSalesPrice, e, chgbyprgAmt, numFormat)
    End Sub

    Private Sub txthsncode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txthsncode.TextChanged

    End Sub

    Private Sub txtCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged, txthsncode.TextChanged, txtTrDescr.TextChanged
        If changeBypgm Then Exit Sub
        BtnUpdate.Enabled = True
    End Sub

    Private Sub cmbtype_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbtype.SelectedIndexChanged
        If changeBypgm Then Exit Sub
        BtnUpdate.Enabled = True
    End Sub
    Private Sub loadRoomToEdit()
        Try
            Dim dt As DataTable
            dt = _objcmnbLayer._fldDatatable("SELECT [Item Code] ,Description,UnitPrice,InvItm.HSNCode,isnull(IGST,0) GST," & _
                                             "(((isnull(IGST,0)+ISNULL(vat,0))*UnitPrice)/100)+UnitPrice [Tax Price],Model,Make ,UnitPriceWS," & _
                                             "itemid," & _
                                             "case when isnull(roomLockStatus,0)=0 then case when isnull(isActive,0) =0 then 'Vacant' Else 'Engaged' end else " & _
                                             "case when isnull(roomLockStatus,0)=1 then 'Cleaning' Else 'Not Available' end end RoomStatus," & _
                                             "vattb.vatcode,remarks FROM InvItm" & _
                                             " LEFT JOIN VatMasterTb ON VatMasterTb.vatid=InvItm.vatid " & _
                                             " LEFT JOIN (SELECT vatcode,vatid FROM VatMasterTb)vattb ON vattb.vatid=InvItm.vatid " & _
                                             " LEFT JOIN (Select sum(roomstatus)isActive,roomItemid from LodgeRoomTb left join LodgeTb on LodgeTb.jobid=LodgeRoomTb.roomLdgid where Dtype='CHI' group by roomItemid ) roomTb ON InvItm.Itemid=roomTb.roomItemid " & _
                                             " LEFT JOIN GSTTb ON GSTTb.HSNCode=InvItm.HSNCode where itemid=" & Val(txtCode.Tag))
            If dt.Rows.Count > 0 Then
                changeBypgm = True
                txtCode.Text = dt(0)("Item Code")
                txtTrDescr.Text = dt(0)("Description")
                txthsncode.Text = dt(0)("HSNCode")
                cmbtype.Text = dt(0)("Model")
                lblremarks.Text = Trim(dt(0)("Remarks") & "")
                If dt(0)("RoomStatus") = "Vacant" Then
                    rdoready.Checked = True
                    lblstatus.ForeColor = Color.Green
                ElseIf dt(0)("RoomStatus") = "Cleaning" Then
                    rdocleaning.Checked = True
                    lblstatus.ForeColor = Color.GreenYellow
                ElseIf dt(0)("RoomStatus") = "Not Available" Then
                    rdonotavailable.Checked = True
                    lblstatus.ForeColor = Color.Red
                Else
                    lblstatus.ForeColor = Color.Red
                End If
                lblstatus.Text = dt(0)("RoomStatus")
                NumSalesPrice.Text = Format(Val(dt(0)("UnitPrice")), numFormat)
                txtwithoutac.Text = Format(Val(dt(0)("UnitPriceWS")), numFormat)
                If Val(dt(0)("Make") & "") = 1 Then
                    rdoac.Checked = True
                    plwithoutac.Visible = True
                Else
                    rdononac.Checked = True
                    plwithoutac.Visible = False
                End If
                txtpriceWtax.Text = Format(Val(dt(0)("Tax Price")), numFormat)
                txtpriceWtax.Tag = dt(0)("GST")
                cmbtax.Text = Trim(dt(0)("vatcode") & "")
                lblgstp.Text = "GST : " & Val(txtpriceWtax.Tag) & "%"
                changeBypgm = False
            End If
            txtCode.Focus()
            btnRemove.Enabled = True
            calculateTaxFromUnitPrice(True)
            roomHistory()
            If userType Then
                BtnUpdate.Tag = IIf(getRight(197, CurrentUser), 1, 0)
            Else
                BtnUpdate.Tag = 1
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub grdItem_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItem.CellContentClick

    End Sub

    Private Sub grdItem_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdItem.DoubleClick
        If grdItem.CurrentRow Is Nothing Then Exit Sub
        txtCode.Tag = grdItem.Item("itemid", grdItem.CurrentRow.Index).Value
        loadRoomToEdit()
        TabControl1.SelectedIndex = 0
        txtCode.Focus()
    End Sub
    Private Sub roomHistory()
        Dim strsql As String
        strsql = "select jobcode [Code],AccDescr Customer,Phone,convert(varchar,checkinDateTime,100) [Check In], " & _
                "checkoutEstimateDateTime [Check In Est.],convert(varchar,checkoutDateTime,100)[Check Out]," & _
                "case when isnull(roomstatus,0) =0 then 'Closed' when isnull(roomstatus,0) =2 then 'Booked' else 'Active' end [Room Status]," & _
                "roomItemid,ldgroomid,Jobid from JobTb " & _
                "left join AccMast on JobTb.custcode=AccMast.AccId " & _
                "left join AccMastAddr on AccMast.accid=AccMastAddr.AccountNo " & _
                "left join LodgeRoomTb on JobTb.Jobid=LodgeRoomTb.roomLdgid where roomItemid=" & Val(txtCode.Tag) & _
                "order by roomItemid, ldgroomid desc"
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable(strsql)
        grdroomhistory.DataSource = dt
        SetGridHistroryHead()
    End Sub
    Private Sub SetGridHistroryHead()
        With grdroomhistory
            SetGridProperty(grdroomhistory)
            .Columns("roomItemid").Visible = False
            .Columns("ldgroomid").Visible = False
            .Columns("Jobid").Visible = False
            .Columns("Check In").Width = 120
            .Columns("Check In Est.").Width = 120
            .Columns("Check Out").Width = 120
        End With
        resizeGridColumn(grdroomhistory, 1)
    End Sub

    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        If Val(btnRemove.Tag) = 0 Then
            MsgBox("This user do not have permission to remove", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Dim dt As DataTable = _objcmnbLayer._fldDatatable("SELECT itemid FROM ITMINVTRTB WHERE itemid =" & Val(txtCode.Tag) & " UNION ALL SELECT roomItemid FROM LodgeRoomTb WHERE roomItemid =" & Val(txtCode.Tag))
        If dt.Rows.Count > 0 Then
            MsgBox("Transaction Exist !", MsgBoxStyle.Exclamation, "Cannot Delete !")
            Exit Sub
        End If
        If MsgBox("Are You Sure to Remove The Item!", MsgBoxStyle.YesNo + MsgBoxStyle.Information) = MsgBoxResult.No Then Exit Sub
        _objcmnbLayer._saveDatawithOutParm("DELETE FROM INVITM WHERE itemid=" & Val(txtCode.Tag))
        loadRooms()
        clearControls()
    End Sub

    Private Sub txthsncode_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txthsncode.Validated
        If changeBypgm Then Exit Sub
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT IGST FROM GSTTB WHERE HSNCode='" & txthsncode.Text & "'")
        If dt.Rows.Count > 0 Then
            txtpriceWtax.Tag = Format(dt(0)("IGST"), numFormat)
            lblgstp.Text = "GST : " & Val(txtpriceWtax.Tag) & "%"
            If Val(NumSalesPrice.Text) > 0 Then
                calculateTaxFromUnitPrice(True)
            ElseIf Val(txtpriceWtax.Text) > 0 Then
                calculateTaxFromUnitPrice(False)
            End If
        Else
            txtpriceWtax.Tag = 0
            lblgstp.Text = ""
        End If
    End Sub
    Private Sub calculateTaxFromUnitPrice(ByVal fromUnitpice As Boolean)
        changeBypgm = True
        If Val(txtpriceWtax.Tag & "") = 0 Then txtpriceWtax.Tag = 0
        If Val(txtpriceWtax.Text & "") = 0 Then txtpriceWtax.Text = 0
        If Val(txtpricetaxwithoutac.Text & "") = 0 Then txtpricetaxwithoutac.Text = 0
        If Val(txtpricetaxwithoutac.Text & "") = 0 Then txtpricetaxwithoutac.Text = 0
        If Val(txtwithoutac.Text & "") = 0 Then txtwithoutac.Text = 0
        If Val(NumSalesPrice.Text) = 0 Then NumSalesPrice.Text = 0
        Dim tax As Double
        If Val(cmbtax.Tag) = 0 Then cmbtax.Tag = 0
        tax = CDbl(txtpriceWtax.Tag) + CDbl(cmbtax.Tag)
        If Not fromUnitpice Then
            NumSalesPrice.Text = (CDbl(txtpriceWtax.Text) * 100) / (tax + 100)
            NumSalesPrice.Text = Format(CDbl(NumSalesPrice.Text), numFormat)
            txtwithoutac.Text = (CDbl(txtpricetaxwithoutac.Text) * 100) / (tax + 100)
            txtwithoutac.Text = Format(CDbl(txtwithoutac.Text), numFormat)
        Else
            txtpriceWtax.Text = Format(CDbl(NumSalesPrice.Text) + ((CDbl(NumSalesPrice.Text) * tax) / 100), numFormat)
            txtpricetaxwithoutac.Text = Format(CDbl(txtwithoutac.Text) + ((CDbl(txtwithoutac.Text) * tax) / 100), numFormat)
        End If
        lblgstamt.Text = "GST : " & Format((CDbl(NumSalesPrice.Text) * Val(txtpriceWtax.Tag)) / 100, numFormat) & vbCrLf
        lblgstamt.Text = lblgstamt.Text & "KFC : " & Format((CDbl(NumSalesPrice.Text) * Val(cmbtax.Tag)) / 100, numFormat)
        lblnonacGst.Text = "GST : " & Format((CDbl(txtwithoutac.Text) * Val(txtpriceWtax.Tag)) / 100, numFormat) & vbCrLf
        lblnonacGst.Text = lblnonacGst.Text & "KFC : " & Format((CDbl(txtwithoutac.Text) * Val(cmbtax.Tag)) / 100, numFormat)
        changeBypgm = False
    End Sub

    Private Sub txtpriceWtax_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtpriceWtax.KeyPress
        NumericTextOnKeypress(txtpriceWtax, e, chgbyprgAmt, numFormat)
    End Sub

    Private Sub txtpriceWtax_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtpriceWtax.Validated
        If Val(txtpriceWtax.Text) > 0 Then calculateTaxFromUnitPrice(False)
    End Sub
    Private Sub clearControls()
        changeBypgm = True
        txtCode.Text = ""
        txtCode.Tag = ""
        txtTrDescr.Text = ""
        txtpriceWtax.Text = Format(0, numFormat)
        NumSalesPrice.Text = Format(0, numFormat)
        txtwithoutac.Text = Format(0, numFormat)
        txtpriceWtax.Tag = 0
        txtpricetaxwithoutac.Text = Format(0, numFormat)
        cmbtype.Text = ""
        rdononac.Checked = True
        txthsncode.Text = ""
        txthsncode.Tag = ""
        lblgstp.Text = "GST : "
        lblgstamt.Text = "GST Amt : "
        changeBypgm = False
        btnRemove.Enabled = False
        BtnUpdate.Enabled = False
        cmbtax.Text = ""
        lblstatus.Text = ""
        rdocleaning.Checked = False
        rdoready.Checked = False
        rdonotavailable.Checked = False
        roomHistory()
        If userType Then
            BtnUpdate.Tag = IIf(getRight(196, CurrentUser), 1, 0)
        Else
            BtnUpdate.Tag = 1
        End If
        txtCode.Focus()
        lblremarks.Text = ""
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        clearControls()
    End Sub


    Private Sub rdononac_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdononac.Click, rdoac.Click
        If rdoac.Checked Then
            plwithoutac.Visible = True
        Else
            plwithoutac.Visible = False
        End If
    End Sub


    Private Sub txtwithoutac_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtwithoutac.KeyPress
        NumericTextOnKeypress(txtwithoutac, e, chgbyprgAmt, numFormat)
    End Sub


    Private Sub txtwithoutac_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtwithoutac.TextChanged
        If changeBypgm Then Exit Sub
        BtnUpdate.Enabled = True
        calculateTaxFromUnitPrice(True)
    End Sub

    Private Sub TabControl1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.Click
        If TabControl1.SelectedIndex = 1 Then
            If grdItem.RowCount = 0 Then loadRooms()
            resizeGridColumn(grdItem, 1)
        End If
    End Sub
    Private Sub grdroomhistory_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdroomhistory.DoubleClick
        fcheckin = New LodgeCheckInFrm
        With LodgeCheckInFrm
            .txtjobcode.Tag = Val(grdroomhistory.Item("jobid", grdroomhistory.CurrentRow.Index).Value)
            .MdiParent = fMainForm
            .Show()
            .loadLodgeForEdit()
        End With
    End Sub

    Private Sub txtSeq_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeq.TextChanged
        grdItem.DataSource = SearchGrid(_vtable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
    End Sub

    Private Sub NumSalesPrice_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumSalesPrice.TextChanged
        If changeBypgm Then Exit Sub
        BtnUpdate.Enabled = True
        calculateTaxFromUnitPrice(True)
    End Sub

    Private Sub cmbtax_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbtax.SelectedIndexChanged
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select isnull(vat,0)vat from VatMasterTb where vatcode='" & cmbtax.Text & "'")
        If dt.Rows.Count > 0 Then
            cmbtax.Tag = dt(0)("vat")
        Else
            cmbtax.Tag = 0
        End If
        If Val(NumSalesPrice.Text) > 0 Then calculateTaxFromUnitPrice(True)
    End Sub

    Private Sub txtpriceWtax_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpriceWtax.TextChanged
        If changeBypgm Then Exit Sub
        BtnUpdate.Enabled = True
        calculateTaxFromUnitPrice(False)
    End Sub

    Private Sub txtpricetaxwithoutac_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtpricetaxwithoutac.KeyPress
        NumericTextOnKeypress(txtpricetaxwithoutac, e, chgbyprgAmt, numFormat)
    End Sub

    Private Sub txtpricetaxwithoutac_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpricetaxwithoutac.TextChanged
        If changeBypgm Then Exit Sub
        BtnUpdate.Enabled = True
        calculateTaxFromUnitPrice(False)

    End Sub

    Private Sub rdocleaning_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdocleaning.Click, rdoready.Click, rdonotavailable.Click
        If lblstatus.Text = "Engaged" Then
            MsgBox("Cannot change status for Engaged room", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Dim ctrl As RadioButton = sender
        If MsgBox("Do you want to change the status of the room?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then GoTo ext
        If rdocleaning.Checked Then
            _objcmnbLayer._saveDatawithOutParm("update InvItm SET roomLockStatus=1,remarks='' where itemid=" & Val(txtCode.Tag))
        ElseIf rdonotavailable.Checked Then
            Dim remark As String = InputBox("Add Remark for Room not available: ")
            _objcmnbLayer._saveDatawithOutParm("update InvItm SET roomLockStatus=2,remarks='" & remark & "' where itemid=" & Val(txtCode.Tag))
        Else
            _objcmnbLayer._saveDatawithOutParm("update InvItm SET roomLockStatus=0,remarks='' where itemid=" & Val(txtCode.Tag))
        End If
        loadRoomToEdit()
        MsgBox("Done", MsgBoxStyle.Information)
        Exit Sub
ext:
        ctrl.Checked = False
    End Sub

    Private Sub rdocleaning_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdocleaning.CheckedChanged

    End Sub

    Private Sub btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnload.Click
        loadRooms()
    End Sub
    Private Sub rdonotavailable_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdonotavailable.CheckedChanged

    End Sub
End Class