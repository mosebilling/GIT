Public Class CardTypeMasterFrm
    Private chgbyprg As Boolean
    Private _objcmn As clsCommon_BL
    Private activecontrolname As String
    Private WithEvents fProductEnquiry As ItmEnqry

    '//
    Private Const ConstItemCode = 0
    Private Const ConstDescr = 1
    Private Const ConstDper = 2
    Private Const constitemid = 3

    Private Const Constpackage = 0
    Private Const ConstCount = 1
    Private Const Constpid = 2

    Private Sub txtservices_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtservices.KeyDown, txtamount.KeyDown, txtCode.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub
    Private Sub txtservices_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtservices.KeyPress
        NumericTextOnKeypress(txtservices, e, chgbyprg, "#,##0")
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        If txtCode.Text = "" Then MsgBox("Invalid Card type", MsgBoxStyle.Exclamation) : txtCode.Focus() : Exit Sub
        If Val(txtservices.Text) = 0 Then MsgBox("Invalid Services", MsgBoxStyle.Exclamation) : txtservices.Focus() : Exit Sub
        If Val(txtamount.Text) = 0 Then MsgBox("Invalid Amount", MsgBoxStyle.Exclamation) : txtamount.Focus() : Exit Sub
        _objcmn = New clsCommon_BL
        If Val(txtCode.Tag) > 0 Then
            _objcmn._saveDatawithOutParm("Update CardtypemasterTb set gstid=" & cmbgst.Tag & ", cardtypename='" & txtCode.Text & "', services=" & Val(txtservices.Text) & ", Amount=" & CDbl(txtamount.Text) & " where cardtypeid=" & Val(txtCode.Tag))
        Else
            _objcmn._saveDatawithOutParm("Insert into CardtypemasterTb(cardtypename,services,Amount,gstid) values('" & txtCode.Text & "'," & CDbl(txtservices.Text) & "," & CDbl(txtamount.Text) & "," & Val(cmbgst.Tag) & ")")
        End If
        savecarditems()
        MsgBox("Card Type Updated", MsgBoxStyle.Information)
        clearContrls()
        loadcardtype()
    End Sub
    Private Sub clearContrls()
        txtCode.Text = ""
        txtCode.Tag = ""
        txtservices.Text = ""
        txtamount.Text = Format(0, numFormat)
        grdVoucher.Rows.Clear()
        grdpackage.Rows.Clear()
        cmbgst.Text = ""
        cmbgst.Tag = ""
        lblgstper.Text = "0%"
    End Sub
    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        If Val(txtCode.Tag) = 0 Then Exit Sub
        _objcmn = New clsCommon_BL
        Dim dt As DataTable
        dt = _objcmn._fldDatatable("Select carid from CarMasterTb where cardtypeid=" & Val(txtCode.Tag))
        If dt.Rows.Count > 0 Then
            MsgBox("Card type found in carmaster! cannot remove current card type")
            Exit Sub
        End If
        If MsgBox("Do you want remove current card type?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.No Then Exit Sub
        _objcmn._saveDatawithOutParm("Delete from CardtypemasterTb where cardtypeid=" & Val(txtCode.Tag))
        _objcmn._saveDatawithOutParm("Delete from CardItemTb where cardtypeid=" & Val(txtCode.Tag))
        clearContrls
        loadcardtype()
    End Sub
    Private Sub loadcardtype()
        Dim dt As DataTable
        _objcmn = New clsCommon_BL
        dt = _objcmn._fldDatatable("Select * from CardtypemasterTb LEFT JOIN GSTTb ON GSTTb.gstid=CardtypemasterTb.gstid")
        With lstContent
            .Items.Clear()
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    .Items.Add(dt(i)("cardtypename"))
                    If .Items.Item(i).SubItems.Count > 1 Then
                        .Items.Item(i).SubItems(1).Text = .Items.Add(dt(i)("services"))
                    Else
                        .Items.Item(i).SubItems.Insert(1, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, dt(i)("services")))
                    End If
                    If .Items.Item(i).SubItems.Count > 2 Then
                        .Items.Item(i).SubItems(2).Text = .Items.Add(dt(i)("Amount"))
                    Else
                        If Val(dt(i)("Amount") & "") = 0 Then dt(i)("Amount") = 0
                        .Items.Item(i).SubItems.Insert(2, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, Format(dt(i)("Amount"), numFormat)))
                    End If
                    If .Items.Item(i).SubItems.Count > 3 Then
                        .Items.Item(i).SubItems(3).Text = .Items.Add(dt(i)("HSNCode"))
                    Else
                        .Items.Item(i).SubItems.Insert(3, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, Trim(dt(i)("HSNCode") & "")))
                    End If
                    If .Items.Item(i).SubItems.Count > 4 Then
                        .Items.Item(i).SubItems(4).Text = .Items.Add(dt(i)("cardtypeid"))
                    Else
                        .Items.Item(i).SubItems.Insert(4, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, dt(i)("cardtypeid")))
                    End If
                   
                Next
            End If
        End With

    End Sub

    Private Sub CardTypeMasterFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loadcardtype()
        setGridHead()
        setPackageGridHead()
        loadhsncode()
        Timer1.Enabled = True
    End Sub

    Private Sub lstContent_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstContent.DoubleClick
        txtCode.Text = lstContent.SelectedItems(0).SubItems(0).Text
        txtCode.Tag = lstContent.SelectedItems(0).SubItems(4).Text
        cmbgst.Text = lstContent.SelectedItems(0).SubItems(3).Text
        cmbgst_SelectedIndexChanged(cmbgst, New System.EventArgs)
        txtservices.Text = Val(lstContent.SelectedItems(0).SubItems(1).Text)
        txtamount.Text = Format(CDbl(lstContent.SelectedItems(0).SubItems(2).Text), numFormat)
        loadcarditems()
        loadFreePackages()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            Const WM_KEYDOWN As Integer = &H100
            Const WM_KEYUP As Integer = &H101
            msg.ToString()
            If msg.Msg = WM_KEYDOWN Or msg.Msg = WM_KEYUP Then
                If msg.WParam.ToInt32() = CInt(Keys.F8) Then
                
                ElseIf activecontrolname = "grdVoucher" Then
                    If msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up) Or msg.WParam.ToInt32() = CInt(Keys.Enter) Or msg.WParam.ToInt32() = CInt(Keys.F2) Or msg.WParam.ToInt32() = CInt(Keys.F3) Or msg.WParam.ToInt32() = CInt(Keys.F4) Then
                        grdVoucher_KeyDown(Nothing, New KeyEventArgs(keyData))
                        Return True
                    End If
                ElseIf activecontrolname = "grdpackage" Then
                    If msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up) Or msg.WParam.ToInt32() = CInt(Keys.Enter) Or msg.WParam.ToInt32() = CInt(Keys.F2) Or msg.WParam.ToInt32() = CInt(Keys.F3) Or msg.WParam.ToInt32() = CInt(Keys.F4) Then
                        grdpackage_KeyDown(Nothing, New KeyEventArgs(keyData))
                        Return True
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function

    Private Sub grdVoucher_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellClick
        If grdVoucher.RowCount = 0 Or grdVoucher.CurrentCell.ColumnIndex = ConstDescr Then Exit Sub
        grdVoucher.BeginEdit(True)
    End Sub

    Private Sub grdVoucher_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellEndEdit
        Dim col As Integer = e.ColumnIndex
        Dim ndec1 As Integer

        Dim grdval As Double
        grdval = Val(grdVoucher.Item(col, e.RowIndex).Value & "")
        grdval = grdval - Fix(grdval)
        If grdval > 0 Then
            ndec1 = 2
        Else
            ndec1 = 0
        End If
        If col = ConstDper Then
            If Val(grdVoucher.Item(col, e.RowIndex).Value) = 0 Then grdVoucher.Item(col, e.RowIndex).Value = 0
            grdVoucher.Item(col, e.RowIndex).Value = Format(Convert.ToDecimal(grdVoucher.Item(col, e.RowIndex).Value), "#,##0" & IIf(ndec1 = 0, "", "." & Strings.StrDup(ndec1, "0")))
        End If
    End Sub

    Private Sub grdVoucher_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdVoucher.EditingControlShowing
        Dim col As Integer
        If Col = ConstDper Then
            Dim tb As TextBox = CType(e.Control, TextBox)
            RemoveHandler tb.KeyPress, AddressOf Textbox_KeyPress
            AddHandler tb.KeyPress, AddressOf Textbox_KeyPress
        End If
    End Sub
    Private Sub Textbox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            Dim col As Integer
            If activecontrolname = "grdVoucher" Then
                col = grdVoucher.CurrentCell.ColumnIndex
                If col = ConstDper Then
                    If Not IsNumeric(e.KeyChar) And e.KeyChar <> "." And Not e.KeyChar = Convert.ToChar(Keys.Back) Then e.Handled = True
                End If
            Else
                col = grdpackage.CurrentCell.ColumnIndex
                If col = ConstCount Then
                    If Not IsNumeric(e.KeyChar) And e.KeyChar <> "." And Not e.KeyChar = Convert.ToChar(Keys.Back) Then e.Handled = True
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


    Private Sub grdVoucher_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.GotFocus
        If grdVoucher.RowCount = 0 Then
            AddRow()
        End If
        activecontrolname = "grdVoucher"
    End Sub

    Private Sub grdVoucher_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdVoucher.KeyDown
        Try
            If e.KeyCode = Keys.F2 Then
                fProductEnquiry = New ItmEnqry
                fProductEnquiry.ShowDialog()
            ElseIf e.KeyCode = Keys.F3 Then
                AddRow()
            ElseIf e.KeyCode = Keys.F4 Then
                removeRow()
            ElseIf e.KeyCode = Keys.Enter Then
                If FindNextCell(grdVoucher, grdVoucher.CurrentCell.RowIndex, grdVoucher.CurrentCell.ColumnIndex + 1) Then
                    AddRow()
                End If
                grdVoucher.BeginEdit(True)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub AddRow()
        Dim i As Integer
        With grdVoucher
            activecontrolname = "grdVoucher"
            .Rows.Add(1)
            i = .RowCount - 1
            .CurrentCell = .Item(0, i)
        End With
        grdVoucher.BeginEdit(True)
    End Sub
    Private Sub AddPackageRow()
        Dim i As Integer
        With grdpackage
            activecontrolname = "grdpackage"
            .Rows.Add(1)
            i = .RowCount - 1
            .CurrentCell = .Item(0, i)
        End With
        grdpackage.BeginEdit(True)
    End Sub
    Private Sub setRow()
        Try
            Dim i As Integer
            With grdVoucher
                activecontrolname = "grdVoucher"
                i = .CurrentRow.Index
                .Columns(ConstDper).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .CurrentCell = .Item(ConstDper, i)

                .BeginEdit(True)
                .Columns(ConstDper).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                '.CurrentCell = .Item(1, i)
                '.BeginEdit(True)
            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        

    End Sub
    Private Sub removeRow()
        If MsgBox("Do you want remove the row?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        With grdVoucher
            .Rows.RemoveAt(.CurrentRow.Index)
            .ClearSelection()
        End With
    End Sub
    Private Sub removePackageRow()
        If MsgBox("Do you want remove the row?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        With grdpackage
            .Rows.RemoveAt(.CurrentRow.Index)
            .ClearSelection()
        End With
    End Sub
    Private Sub fProductEnquiry_getSelected(ByVal ItemcODE As String, ByVal isacc As Integer) Handles fProductEnquiry.getSelected
        chgbyprg = True
        fProductEnquiry.Close()
        grdVoucher.Item(ConstItemCode, grdVoucher.CurrentRow.Index).Value = ItemcODE
        getitemdetails(ItemcODE, grdVoucher.CurrentRow.Index)
        grdVoucher.ClearSelection()
        chgbyprg = False
        'activecontrolname = "grdVoucher"

        'grdVoucher.CurrentCell = grdVoucher.Item(0, grdVoucher.CurrentRow.Index)
        'grdVoucher.BeginEdit(True)
        'grdVoucher.Focus()
        'If grdVoucher.Item(constitemid, grdVoucher.RowCount - 1).Value = 0 Then
        '    grdVoucher.CurrentCell = grdVoucher.Item(ConstItemCode, grdVoucher.RowCount - 1)
        'End If
        setRow()
    End Sub
    Private Sub getitemdetails(ByVal itemcode As String, ByVal i As Integer)
        Dim dt As DataTable
        _objcmn = New clsCommon_BL
        dt = _objcmn._fldDatatable("Select itemid,Description from invitm where [Item Code]='" & itemcode & "'")
        If dt.Rows.Count > 0 Then
            grdVoucher.Item(ConstDescr, i).Value = dt(0)("Description")
            grdVoucher.Item(constitemid, i).Value = dt(0)("itemid")
            grdVoucher.Item(ConstDper, i).Value = Format(100, "#,##0")
        End If
    End Sub
    Private Sub setGridHead()

        Try
            chgbyprg = True
            With grdVoucher
                SetEntryGridProperty(grdVoucher)
                .ColumnCount = 4

                .Columns(ConstItemCode).HeaderText = "Item Code"
                .Columns(ConstItemCode).Width = 100 '100
                .Columns(ConstItemCode).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(ConstItemCode).DefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.5!, System.Drawing.FontStyle.Regular)

                .Columns(ConstDescr).HeaderText = "Item Name"
                .Columns(ConstDescr).Width = 200
                .Columns(ConstDescr).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(ConstDescr).DefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.5!, System.Drawing.FontStyle.Regular)
                '.Columns(ConstDescr).ReadOnly = True

                .Columns(ConstDper).HeaderText = "Disc%"
                .Columns(ConstDper).Width = 70
                .Columns(ConstDper).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(ConstDper).DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns(ConstDper).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                .Columns(constitemid).Visible = False

            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        chgbyprg = False
    End Sub
    Private Sub setPackageGridHead()

        Try
            chgbyprg = True
            With grdpackage
                SetEntryGridProperty(grdpackage)
                .ColumnCount = 3
                Dim cmb As New DataGridViewComboBoxColumn
                'cmb.Items.Add("")
                Dim dt As DataTable
                dt = _objcmn._fldDatatable("SELECT cardtypename FROM CardtypemasterTb")
                cmb.Items.Add("")
                For i = 0 To dt.Rows.Count - 1
                    cmb.Items.Add(Trim(dt(i)(0)))
                Next
                cmb.HeaderText = "Card Type"
                cmb.DataPropertyName = "cardtypename"
                cmb.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
                cmb.DefaultCellStyle.Font = New System.Drawing.Font("Arial Black", 12.0!, System.Drawing.FontStyle.Bold)
                .Columns.RemoveAt(Constpackage)
                .Columns.Insert(Constpackage, cmb)
                .Columns(Constpackage).HeaderText = "Package Name"
                .Columns(Constpackage).Width = 100
                .Columns(Constpackage).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(Constpackage).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Columns(Constpackage).ReadOnly = False ' Not AllowLocationItemwiseOnInventory
                .Columns(Constpackage).Visible = True

                .Columns(ConstCount).HeaderText = "Count"
                .Columns(ConstCount).Width = 70
                .Columns(ConstCount).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(ConstCount).DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns(ConstCount).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                .Columns(Constpid).Visible = False

            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        chgbyprg = False
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        resizeGridColumn(grdVoucher, 1)
        resizeGridColumn(grdpackage, 0)
    End Sub
    Private Sub savecarditems()
        Dim dt As DataTable
        _objcmn = New clsCommon_BL
        Dim i As Integer
        Dim ctypeid As Integer
        If Val(txtCode.Tag) = 0 Then
            dt = _objcmn._fldDatatable("Select cardtypeid from  CardtypemasterTb where cardtypename='" & txtCode.Text & "'")
            If dt.Rows.Count > 0 Then
                ctypeid = dt(0)("cardtypeid")
            End If
        Else
            ctypeid = Val(txtCode.Tag)
        End If
        _objcmn._saveDatawithOutParm("Delete from CardItemTb where cardtypeid=" & ctypeid)
        With grdVoucher
            For i = 0 To .RowCount - 1
                If Val(.Item(constitemid, i).Value) > 0 Then
                    _objcmn._saveDatawithOutParm("Insert into CardItemTb(itemid,discpercentage,cardtypeid) values(" & Val(.Item(constitemid, i).Value) & "," & Val(.Item(ConstDper, i).Value) & "," & ctypeid & ")")
                End If
            Next
        End With
        saveFreePackage(ctypeid)
    End Sub
    Private Sub saveFreePackage(ByVal ctypeid As Integer)
        _objcmn._saveDatawithOutParm("Delete from FreePackageTb where pid=" & ctypeid)
        With grdpackage
            For i = 0 To .RowCount - 1
                If Val(.Item(Constpid, i).Value) > 0 Then
                    _objcmn._saveDatawithOutParm("Insert into FreePackageTb(pid,premiumpackage,serviceCount) values(" & ctypeid & "," & Val(.Item(Constpid, i).Value) & "," & Val(.Item(ConstCount, i).Value) & ")")
                End If
            Next
        End With
    End Sub
    Private Sub loadcarditems()
        Dim dt As DataTable
        _objcmn = New clsCommon_BL
        dt = _objcmn._fldDatatable("Select [Item Code],Description,discpercentage,CardItemTb.itemid from CardItemTb LEFT JOIN InvItm On CardItemTb.itemid=invitm.itemid where cardtypeid=" & Val(txtCode.Tag))
        Dim i As Integer
        Dim ndec1 As String
        Dim grdval As Double
        grdVoucher.Rows.Clear()
        For i = 0 To dt.Rows.Count - 1
            With grdVoucher
                .Rows.Add()
                .Item(ConstItemCode, i).Value = dt(i)("Item Code")
                .Item(ConstDescr, i).Value = dt(i)("Description")
                grdval = Val(dt(i)("discpercentage"))
                grdval = grdval - Fix(grdval)
                If grdval > 0 Then
                    ndec1 = "#,##0.00"
                Else
                    ndec1 = "#,##0"
                End If
                .Item(ConstDper, i).Value = Format(dt(i)("discpercentage"), ndec1)
                .Item(constitemid, i).Value = dt(i)("itemid")
            End With
        Next
    End Sub
    Private Sub loadFreePackages()
        Dim dt As DataTable
        _objcmn = New clsCommon_BL
        dt = _objcmn._fldDatatable("Select premiumpackage,serviceCount,cardtypename from FreePackageTb " & _
                                   "LEFT JOIN CardtypemasterTb On FreePackageTb.premiumpackage=CardtypemasterTb.cardtypeid where isnull(cardtypename,'')<>'' and pid=" & Val(txtCode.Tag))
        Dim i As Integer
        Dim ndec1 As String
        Dim grdval As Double
        grdpackage.Rows.Clear()
        For i = 0 To dt.Rows.Count - 1
            With grdpackage
                .Rows.Add()
                .Item(Constpackage, i).Value = Trim(dt(i)("cardtypename") & "")
                .Item(Constpid, i).Value = dt(i)("premiumpackage")
                grdval = Val(dt(i)("serviceCount"))
                grdval = grdval - Fix(grdval)
                If grdval > 0 Then
                    ndec1 = "#,##0.00"
                Else
                    ndec1 = "#,##0"
                End If
                .Item(ConstCount, i).Value = Format(dt(i)("serviceCount"), ndec1)
            End With
        Next
    End Sub
    Private Sub grdVoucher_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.Leave
        activecontrolname = ""
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        clearContrls()
    End Sub

    Private Sub txtamount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtamount.KeyPress
        NumericTextOnKeypress(txtamount, e, chgbyprg, numFormat)
    End Sub
    Private Sub loadhsncode()
        Dim dt As DataTable
        dt = _objcmn._fldDatatable("SELECT HSNCode FROM GSTTb")
        cmbgst.Items.Clear()
        cmbgst.Items.Add("")
        Dim i As Integer
        For i = 0 To dt.Rows.Count - 1
            cmbgst.Items.Add(dt(i)("HSNCode"))
        Next
    End Sub

    Private Sub cmbgst_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbgst.SelectedIndexChanged
        Dim dt As DataTable
        dt = _objcmn._fldDatatable("SELECT gstid,IGST FROM GSTTb where HSNCode='" & cmbgst.Text & "'")
        If dt.Rows.Count > 0 Then
            cmbgst.Tag = dt(0)("gstid")
            lblgstper.Text = dt(0)("IGST") & "%"
        Else
            cmbgst.Tag = 0
            lblgstper.Text = "0%"
        End If

    End Sub


    Private Sub grdpackage_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdpackage.CellClick
        If grdpackage.RowCount = 0 Then Exit Sub
        grdpackage.BeginEdit(True)
    End Sub

    Private Sub grdpackage_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdpackage.CellEndEdit
        Dim col As Integer = e.ColumnIndex
        Dim ndec1 As Integer

        Dim grdval As Double
        grdval = Val(grdpackage.Item(col, e.RowIndex).Value)
        grdval = grdval - Fix(grdval)
        If grdval > 0 Then
            ndec1 = 2
        Else
            ndec1 = 0
        End If
        If col = ConstDper Then
            If Val(grdpackage.Item(col, e.RowIndex).Value) = 0 Then grdpackage.Item(col, e.RowIndex).Value = 0
            grdpackage.Item(col, e.RowIndex).Value = Format(Convert.ToDecimal(grdpackage.Item(col, e.RowIndex).Value), "#,##0" & IIf(ndec1 = 0, "", "." & Strings.StrDup(ndec1, "0")))
        End If
    End Sub

    Private Sub grdpackage_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdpackage.CellValidated
        Dim pname As String = grdpackage.Item(Constpackage, e.RowIndex).Value
        grdpackage.Item(Constpid, e.RowIndex).Value = getPackageId(pname)
    End Sub
    Private Function getPackageId(ByVal pname As String) As Integer
        Dim dt As DataTable
        Dim pid As Integer
        dt = _objcmn._fldDatatable("SELECT cardtypeid FROM CardtypemasterTb where cardtypename='" & pname & "'")
        If dt.Rows.Count > 0 Then
            pid = dt(0)(0)
        End If
        Return pid
    End Function

    Private Sub grdpackage_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdpackage.EditingControlShowing
        Dim col As Integer
        If col = ConstDper Then
            Dim tb As TextBox = CType(e.Control, TextBox)
            RemoveHandler tb.KeyPress, AddressOf Textbox_KeyPress
            AddHandler tb.KeyPress, AddressOf Textbox_KeyPress
        End If
    End Sub

    Private Sub grdpackage_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdpackage.GotFocus
        If grdpackage.RowCount = 0 Then
            AddPackageRow()
        End If
        activecontrolname = "grdpackage"
    End Sub

    Private Sub grdpackage_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdpackage.KeyDown
        Try
            If e.KeyCode = Keys.F3 Then
                AddPackageRow()
            ElseIf e.KeyCode = Keys.F4 Then
                removePackageRow()
            ElseIf e.KeyCode = Keys.Enter Then
                If FindNextCell(grdpackage, grdpackage.CurrentCell.RowIndex, grdpackage.CurrentCell.ColumnIndex + 1) Then
                    AddPackageRow()
                End If
                grdpackage.BeginEdit(True)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub grdpackage_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdpackage.Leave
        activecontrolname = ""
    End Sub
End Class