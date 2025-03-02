Public Class MembershipItemsFrm
    Dim _objcmnbLayer As New clsCommon_BL
    Dim _objItmMast As New clsItemMast_BL
    Private _dlayer As New Dlayer
    Private changeBypgm As Boolean
    Private chgbyprgAmt As Boolean
    Private WithEvents fcheckin As LodgeCheckInFrm
    Private _vtable As DataTable
    Private Sub MembershipItemsFrm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtname.Focus()
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
    Private Sub ldHSN()
        Dim ObjLocationlist As New List(Of String)
        ObjLocationlist = toLoadAutoFillListItems("HSNCode", "GSTTb")
        Call toAssignDownListToText(txthsncode, ObjLocationlist) '
    End Sub
    Private Function chkFields() As Boolean
        Dim _vdtTable As DataTable
        If Trim(txtCode.Text) = "" Then
            MsgBox("Package Cannot be Blank !", MsgBoxStyle.Exclamation)
            txtCode.Focus()
            GoTo err
        End If
        If Val(txtCode.Tag) > 0 Then
            _vdtTable = _objcmnbLayer._fldDatatable("Select ItemId from InvItm Where ItemId= " & Val(txtCode.Tag))
            If _vdtTable.Rows.Count = 0 Then
                MsgBox("Selected Package Not Found !", MsgBoxStyle.Exclamation)
                GoTo err
            End If
        End If
        If txtname.Text = "" Then
            MsgBox("'Package' Cannot be Blank !", MsgBoxStyle.Exclamation)
            txtname.Focus()
            GoTo err
        End If
        _vdtTable = _objcmnbLayer._fldDatatable("Select ItemId from InvItm Where [Item Code] ='" & _
                                                MkDbSrchStr(txtCode.Text) & "' and ItemId <>" & Val(txtCode.Tag))
        If _vdtTable.Rows.Count > 0 Then
            MsgBox("Package Already Exist !", MsgBoxStyle.Exclamation)
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
        _objItmMast.Descr = Trim(txtname.Text)
        _objItmMast.Unit = ""
        _objItmMast.hsncode = txthsncode.Text
        _objItmMast.Make = ""
        _objItmMast.Category = "package"
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
    Private Sub MembershipItemsFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtCode.Text = GenerateNext("")
        loadPackage()
        ldVat()
        Dim ObjLocationlist As New List(Of String)
        ObjLocationlist = toLoadAutoFillListItems("HSNCode", "GSTTb")
        Call toAssignDownListToText(txthsncode, ObjLocationlist) '
        If userType = 0 Or userType = 2 Then
            btnRemove.Tag = 1
            BtnUpdate.Tag = 1
        Else
            btnRemove.Tag = IIf(getRight(198, CurrentUser), 1, 0)
            BtnUpdate.Tag = IIf(getRight(196, CurrentUser), 1, 0)
        End If
    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            Const WM_KEYDOWN As Integer = &H100
            Const WM_KEYUP As Integer = &H101
            'msg.ToString()
            If msg.Msg = WM_KEYDOWN Or msg.Msg = WM_KEYUP Then
                If msg.WParam.ToInt32() = CInt(Keys.F8) Then
                    'UpdateClick()
                End If
            End If
ctn:
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

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
    Private Sub calculateTaxFromUnitPrice(ByVal fromUnitpice As Boolean)
        changeBypgm = True
        If Val(txtpriceWtax.Tag & "") = 0 Then txtpriceWtax.Tag = 0
        If Val(txtpriceWtax.Text & "") = 0 Then txtpriceWtax.Text = 0
        If Val(NumSalesPrice.Text) = 0 Then NumSalesPrice.Text = 0
        Dim tax As Double
        If Val(cmbtax.Tag) = 0 Then cmbtax.Tag = 0
        tax = CDbl(txtpriceWtax.Tag) + CDbl(cmbtax.Tag)
        If Not fromUnitpice Then
            NumSalesPrice.Text = (CDbl(txtpriceWtax.Text) * 100) / (tax + 100)
            NumSalesPrice.Text = Format(CDbl(NumSalesPrice.Text), numFormat)
        Else
            txtpriceWtax.Text = Format(CDbl(NumSalesPrice.Text) + ((CDbl(NumSalesPrice.Text) * tax) / 100), numFormat)
        End If
        lblgstamt.Text = "GST : " & Format((CDbl(NumSalesPrice.Text) * Val(txtpriceWtax.Tag)) / 100, numFormat) & vbCrLf
        lblgstamt.Text = lblgstamt.Text & "KFC : " & Format((CDbl(NumSalesPrice.Text) * Val(cmbtax.Tag)) / 100, numFormat)
        lblgstamt.Visible = True
        changeBypgm = False
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

    Private Sub NumSalesPrice_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles NumSalesPrice.KeyDown, txtdays.KeyDown, txtpriceWtax.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub NumSalesPrice_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles NumSalesPrice.KeyPress
        NumericTextOnKeypress(NumSalesPrice, e, chgbyprgAmt, numFormat)
    End Sub

    Private Sub NumSalesPrice_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumSalesPrice.TextChanged
        If changeBypgm Then Exit Sub
        BtnUpdate.Enabled = True
        calculateTaxFromUnitPrice(True)
    End Sub

    Private Sub txtpriceWtax_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtpriceWtax.KeyPress
        NumericTextOnKeypress(txtpriceWtax, e, chgbyprgAmt, numFormat)
    End Sub

    Private Sub txtpriceWtax_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpriceWtax.TextChanged
        If changeBypgm Then Exit Sub
        BtnUpdate.Enabled = True
        calculateTaxFromUnitPrice(False)
    End Sub

    Private Sub txtdays_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtdays.KeyPress
        NumericTextOnKeypress(txtdays, e, chgbyprgAmt, "0")
    End Sub
    Private Function GenerateNext(ByVal strCode As String) As String
        Dim N As Double
        Dim NewCode As String
        Dim i As Byte
        Dim f As Byte
        Dim tmp As String
        If strCode = "" Then
            strCode = _objItmMast.returnLastItemCode
        End If
        If strCode = "" Then
            strCode = "PKG"
        End If
        Dim dr As IDataReader = Nothing
        If strCode = "" Then Return strCode
        For i = 1 To 11
            If IsNumeric(Mid(strCode, Len(strCode) - i + 1, 1)) = False Then Exit For
            tmp = Val(Mid(strCode, Len(strCode) - i + 1))
            If Val(tmp) <> 0 Then
                N = Val(tmp)
            Else
                If N <> 0 Then Exit For
            End If
            If i = Len(strCode) Then i = i + 1 : Exit For
        Next i
        i = i - 1
        f = i
        If i <= 0 Then
            'i = txtCode.MaxLength - Len(strCode)
            i = 3
        End If
        tmp = ""
        NewCode = ""
        Try
            Do Until False
                N = N + 1
                tmp = Mid(strCode, 1, IIf(Len(strCode) <> 0, Len(strCode) - f, 0)) & Format(N, StrDup(i, "0"))
                dr = _objItmMast.ldmst("SELECT [Item Code] FROM InvItm WHERE [Item Code] = '" & tmp & "'")
                If dr.Read = False Then
                    NewCode = Mid(strCode, 1, IIf(Len(strCode) <> 0, Len(strCode) - f, 0)) & Format(N, StrDup(i, "0"))
                    NewCode = Mid(NewCode, 1, 30)
                    Exit Do
                End If
            Loop
            If Not dr Is Nothing Then dr.Close()
            _objItmMast.clsreader()
            _objItmMast.clsCnnection()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return NewCode
    End Function

    Private Sub BtnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUpdate.Click
        If Val(BtnUpdate.Tag) = 0 Then
            MsgBox("This user do not have rights to update", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If chkFields() Then
            setValue()
            Dim itemid As Long = _objItmMast._saveItemMast()
            _dlayer.savewithoutparam("update invitm set courseduration=" & Val(txtdays.Text) & ",units=" & Val(txtclasess.Text) & " where itemid=" & itemid)
            MsgBox("Package Created Successfully", MsgBoxStyle.Information)
            clearControls()
            loadPackage()
        End If
    End Sub
    Private Sub clearControls()
        changeBypgm = True
        txtCode.Text = ""
        txtCode.Tag = ""
        txtname.Text = ""
        txtpriceWtax.Text = Format(0, numFormat)
        NumSalesPrice.Text = Format(0, numFormat)
        txtpriceWtax.Tag = 0
        txthsncode.Text = ""
        txthsncode.Tag = ""
        lblgstp.Text = "GST : "
        changeBypgm = False
        btnRemove.Enabled = False
        BtnUpdate.Enabled = False
        cmbtax.Text = ""
        txtdays.Text = 0
        txtclasess.Text = 0
        If userType Then
            BtnUpdate.Tag = IIf(getRight(196, CurrentUser), 1, 0)
        Else
            BtnUpdate.Tag = 1
        End If
    End Sub
    Private Sub loadPackage()
        Try
            _vtable = _objcmnbLayer._fldDatatable("SELECT [Item Code] [Code],Description,UnitPrice,(((isnull(IGST,0)+ISNULL(vat,0))*UnitPrice)/100)+UnitPrice [Tax Price],isnull(courseduration,0) Days,isnull(units,0) Clasess," & _
                                             "itemid FROM InvItm" & _
                                              " LEFT JOIN VatMasterTb ON VatMasterTb.vatid=InvItm.vatid " & _
                                             " LEFT JOIN GSTTb ON GSTTb.HSNCode=InvItm.HSNCode where itemCategory='package'")
            grdItem.DataSource = _vtable
            SetGridHead()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub SetGridHead()
        With grdItem
            SetGridProperty(grdItem)
            .Columns("itemid").Visible = False
            .Columns("Description").Width = 150
            .Columns("Description").HeaderText = "Package"
            .Columns("UnitPrice").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("UnitPrice").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Tax Price").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Tax Price").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns("Days").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Days").Width = 75
            .Columns("Clasess").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Clasess").Width = 75
        End With
        setComboGrid()
        'resizeGridColumn(grdItem, 1)
    End Sub
    Private Sub setComboGrid()
        cmbOrder.Items.Clear()
        Dim i As Integer = 0
        For i = 0 To grdItem.ColumnCount - 2
            cmbOrder.Items.Add(grdItem.Columns(i).HeaderText)
        Next
        If cmbOrder.Items.Count > 0 Then cmbOrder.SelectedIndex = 0
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        clearControls()
    End Sub

    Private Sub btnexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexit.Click
        Me.Close()
    End Sub

    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        If Val(btnRemove.Tag) = 0 Then
            MsgBox("This user do not have permission to remove", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Dim dt As DataTable = _objcmnbLayer._fldDatatable("SELECT itemid FROM ITMINVTRTB WHERE itemid =" & Val(txtCode.Tag) & _
                                                          " UNION ALL SELECT itemid FROM MembershipRenewalTb WHERE itemid =" & Val(txtCode.Tag))
        If dt.Rows.Count > 0 Then
            MsgBox("Transaction Exist !", MsgBoxStyle.Exclamation, "Cannot Delete !")
            Exit Sub
        End If
        If MsgBox("Are You Sure to Remove The Item!", MsgBoxStyle.YesNo + MsgBoxStyle.Information) = MsgBoxResult.No Then Exit Sub
        _objcmnbLayer._saveDatawithOutParm("DELETE FROM INVITM WHERE itemid=" & Val(txtCode.Tag))
        loadPackage()
        clearControls()
    End Sub

    Private Sub txtCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCode.KeyDown, txtname.KeyDown, txthsncode.KeyDown, cmbtax.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub


    Private Sub grdItem_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdItem.DoubleClick
        If grdItem.CurrentRow Is Nothing Then Exit Sub
        txtCode.Tag = grdItem.Item("itemid", grdItem.CurrentRow.Index).Value
        loadPackageToEdit()
        txtCode.Focus()
    End Sub
    Private Sub loadPackageToEdit()
        Try
            Dim dt As DataTable
            dt = _objcmnbLayer._fldDatatable("SELECT [Item Code] ,Description,UnitPrice,InvItm.HSNCode,isnull(IGST,0) GST," & _
                                             "(((isnull(IGST,0)+ISNULL(vat,0))*UnitPrice)/100)+UnitPrice [Tax Price],Model,Make ,UnitPriceWS," & _
                                             "itemid,case when isnull(isActive,0) =0 then 'Vacent' Else 'Engaged' end RoomStatus,vattb.vatcode,isnull(courseduration,0)courseduration,isnull(units,0)units FROM InvItm" & _
                                              " LEFT JOIN VatMasterTb ON VatMasterTb.vatid=InvItm.vatid " & _
                                             " LEFT JOIN (SELECT vatcode,vatid FROM VatMasterTb)vattb ON vattb.vatid=InvItm.vatid " & _
                                             " LEFT JOIN (Select sum(roomstatus)isActive,roomItemid from LodgeRoomTb group by roomItemid ) roomTb ON InvItm.Itemid=roomTb.roomItemid " & _
                                             " LEFT JOIN GSTTb ON GSTTb.HSNCode=InvItm.HSNCode where itemid=" & Val(txtCode.Tag))
            If dt.Rows.Count > 0 Then
                changeBypgm = True
                txtCode.Text = dt(0)("Item Code")
                txtname.Text = dt(0)("Description")
                txthsncode.Text = dt(0)("HSNCode")
                NumSalesPrice.Text = Format(Val(dt(0)("UnitPrice")), numFormat)
                txtpriceWtax.Text = Format(Val(dt(0)("Tax Price")), numFormat)
                txtpriceWtax.Tag = dt(0)("GST")
                txtdays.Text = Val(dt(0)("courseduration") & "")
                cmbtax.Text = Trim(dt(0)("vatcode") & "")
                lblgstp.Text = "GST : " & Val(txtpriceWtax.Tag) & "%"
                txtclasess.Text = Val(dt(0)("units") & "")
                changeBypgm = False
            End If
            txtCode.Focus()
            btnRemove.Enabled = True
            calculateTaxFromUnitPrice(True)
            If userType Then
                BtnUpdate.Tag = IIf(getRight(197, CurrentUser), 1, 0)
            Else
                BtnUpdate.Tag = 1
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub txtSeq_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeq.TextChanged
        grdItem.DataSource = SearchGrid(_vtable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
    End Sub

    Private Sub txtCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged, txtCode.TextChanged, _
    txthsncode.TextChanged, txtdays.TextChanged
        If changeBypgm Then Exit Sub
        BtnUpdate.Enabled = True
    End Sub
End Class