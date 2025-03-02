Imports System.IO

Public Class StichingServiceMasterFrm
    Dim _objcmnbLayer As New clsCommon_BL
    Dim _objItmMast As New clsItemMast_BL
    Private changeBypgm As Boolean
    Private chgbyprgAmt As Boolean
    Private _vtable As DataTable
    Private activecontrolname As String
    Private ChgID As Boolean
    Private Const constMeasurement = 0
    Private Const constsecondname = 1
    Private Const constid = 2

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnaddline_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddline.Click
        addRow()
    End Sub
    Private Sub setValue()
        _objItmMast.ItemId = Val(txtCode.Tag)
        _objItmMast.ItemCode = Trim(txtCode.Text)
        _objItmMast.Descr = Trim(txtTrDescr.Text)
        _objItmMast.Unit = ""
        _objItmMast.hsncode = txthsncode.Text
        _objItmMast.Model = ""
        _objItmMast.Make = 0
        _objItmMast.WSalesPrice = 0
        _objItmMast.Category = "Service"
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
    End Sub

    Private Sub StichingServiceMasterFrm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtCode.Focus()
    End Sub

    Private Sub StichingServiceMasterFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ldHSN()
        clearControls()
        loadService()
        setGridHeadMeasurement()
        If userType = 0 Or userType = 2 Then
            btnRemove.Tag = 1
            BtnUpdate.Tag = 1
        Else
            btnRemove.Tag = IIf(getRight(198, CurrentUser), 1, 0)
            BtnUpdate.Tag = IIf(getRight(196, CurrentUser), 1, 0)
        End If
        cmbcategory.SelectedIndex = 0
        'Me.Top = fMainForm.Top + fMainForm.Panel1.Top + fMainForm.Panel1.Height + fMainForm.ToolStrip1.Height + fMainForm.MenuStrip.Height + fMainForm.MenuStrip1.Height + 10
        'Me.Left = Screen.PrimaryScreen.WorkingArea.Width - Me.Width - 10
        'Me.Height = fMainForm.Height - Me.Top - 40
    End Sub
    Private Sub ldHSN()
        Dim ObjLocationlist As New List(Of String)
        ObjLocationlist = toLoadAutoFillListItems("HSNCode", "GSTTb")
        Call toAssignDownListToText(txthsncode, ObjLocationlist) '
    End Sub

    Private Sub txtCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCode.KeyDown, txthsncode.KeyDown, txtpriceWtax.KeyDown, txtTrDescr.KeyDown, NumSalesPrice.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtpriceWtax_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtpriceWtax.KeyPress
        NumericTextOnKeypress(txtpriceWtax, e, chgbyprgAmt, numFormat)
    End Sub

    Private Sub NumSalesPrice_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles NumSalesPrice.Validated
        calculateTaxFromUnitPrice(True)
    End Sub
    Private Sub calculateTaxFromUnitPrice(ByVal fromUnitpice As Boolean)
        changeBypgm = True
        If Val(txtpriceWtax.Tag & "") = 0 Then txtpriceWtax.Tag = 0
        If Val(txtpriceWtax.Text & "") = 0 Then txtpriceWtax.Text = 0
        If Val(NumSalesPrice.Text) = 0 Then NumSalesPrice.Text = 0
        Dim tax As Double
        tax = CDbl(txtpriceWtax.Tag)
        If Not fromUnitpice Then
            NumSalesPrice.Text = (CDbl(txtpriceWtax.Text) * 100) / (tax + 100)
            NumSalesPrice.Text = Format(CDbl(NumSalesPrice.Text), numFormat)
        Else
            txtpriceWtax.Text = Format(CDbl(NumSalesPrice.Text) + ((CDbl(NumSalesPrice.Text) * tax) / 100), numFormat)
        End If
        lblgstamt.Text = "GST : " & Format((CDbl(NumSalesPrice.Text) * Val(txtpriceWtax.Tag)) / 100, numFormat)
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

    Private Sub txtpriceWtax_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpriceWtax.TextChanged

    End Sub

    Private Sub BtnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUpdate.Click
        'If Val(BtnUpdate.Tag) = 0 Then
        '    MsgBox("This user do not have rights to update", MsgBoxStyle.Exclamation)
        '    Exit Sub
        'End If
        If chkFields() Then
            setValue()
            txtCode.Tag = _objItmMast._saveItemMast()

            If Val(txtCode.Tag) > 0 Then
                saveMeasurement()
            End If
            saveImage()
            MsgBox("Item Saved Successfully", MsgBoxStyle.Information)
            clearControls()
            loadService()
        End If
    End Sub
    Private Sub saveImage()
        dtPhotopath = DPath & "Photos"
        If picImage.Tag <> "" Then
            Dim findExt As String
            If Directory.Exists(dtPhotopath) = False Then
                Directory.CreateDirectory(dtPhotopath)
            End If
            Try
                findExt = picImage.Tag
                findExt = Mid(findExt, findExt.LastIndexOf(".") + 2)
                Dim filename As String
                Dim filepath As String
                filename = "ITM-" & Val(txtCode.Tag) & "-" & 1 & "." & findExt
                filepath = dtPhotopath & "\" & filename
                If FileExists(filepath) Then
                    File.Delete(filepath)
                End If
                If FileExists(picImage.Tag) Then
                    File.Copy(picImage.Tag, filepath)
                End If
                _objcmnbLayer._saveDatawithOutParm("UPDATE INVITM SET Image1='" & filename & "' where itemid=" & Val(txtCode.Tag))

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub
    Private Sub saveMeasurement()
        Dim i As Integer
        With grdmeasurement
            '_objcmnbLayer._saveDatawithOutParm("update StichingMeasurementTb set setremove=1 where mitemid=" & Val(txtCode.Tag))
            For i = 0 To .Rows.Count - 1
                If .Item(constMeasurement, i).Value <> "" Then
                    If Val(.Item(constid, i).Value) = 0 Then
                        _objcmnbLayer._saveDatawithOutParm("Insert into StichingMeasurementTb(measurementname,mitemid,secondname) values('" & .Item(constMeasurement, i).Value & "'," & Val(txtCode.Tag) & ",N'" & .Item(constsecondname, i).Value & "')")
                    Else
                        _objcmnbLayer._saveDatawithOutParm("update StichingMeasurementTb set measurementname='" & .Item(constMeasurement, i).Value & "',secondname=N'" & .Item(constsecondname, i).Value & "',mitemid=" & Val(txtCode.Tag) & " where measurementid=" & Val(.Item(constid, i).Value))
                    End If
                End If
            Next
            '_objcmnbLayer._saveDatawithOutParm("delete from StichingMeasurementTb where setremove=1 and mitemid=" & Val(txtCode.Tag))
        End With
    End Sub
    Private Sub loadService()
        Try
            Dim condition As String = ""
            If cmbcategory.SelectedIndex = 0 Then
                condition = " and isnull(ishide,0)=0"
            ElseIf cmbcategory.SelectedIndex = 1 Then
                condition = " and isnull(ishide,0)=1"
            End If
            'condition = ""
            _vtable = _objcmnbLayer._fldDatatable("SELECT [Item Code] [Service Code],Description [Service Name],UnitPrice Price,(((isnull(IGST,0)+ISNULL(vat,0))*UnitPrice)/100)+UnitPrice [Tax Price]," & _
                                             "itemid FROM InvItm" & _
                                             " LEFT JOIN VatMasterTb ON VatMasterTb.vatid=InvItm.vatid " & _
                                             " LEFT JOIN GSTTb ON GSTTb.HSNCode=InvItm.HSNCode where itemCategory='service' " & condition)
            grdItem.DataSource = _vtable
            Dim i As Integer
            cmbcopyfrom.Items.Clear()
            For i = 0 To _vtable.Rows.Count - 1
                cmbcopyfrom.Items.Add(_vtable(i)("Service Name"))
            Next
            SetGridHead()
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
            .Columns("Price").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Price").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Tax Price").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Tax Price").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        setComboGrid()
        resizeGridColumn(grdItem, 1)
    End Sub
    Private Function chkFields() As Boolean
        Dim _vdtTable As DataTable
        If Trim(txtCode.Text) = "" Then
            MsgBox("Service Code Cannot be Blank !", MsgBoxStyle.Exclamation)
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
            MsgBox("Service Name Cannot be Blank !", MsgBoxStyle.Exclamation)
            txtTrDescr.Focus()
            GoTo err
        End If

        _vdtTable = _objcmnbLayer._fldDatatable("Select ItemId from InvItm Where [Item Code] ='" & MkDbSrchStr(txtCode.Text) & "' and ItemId <>" & Val(txtCode.Tag))
        If _vdtTable.Rows.Count > 0 Then
            MsgBox("Service Code Already Exist !", MsgBoxStyle.Exclamation)
            txtCode.Focus()
            GoTo err
        End If
        chkFields = True
        Exit Function
Err:
        chkFields = False
    End Function
    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        clearControls()
    End Sub
    Private Sub clearControls()
        changeBypgm = True
        txtCode.Text = ""
        txtCode.Tag = ""
        txtTrDescr.Text = ""
        txtpriceWtax.Text = Format(0, numFormat)
        NumSalesPrice.Text = Format(0, numFormat)
        txtpriceWtax.Tag = 0
        txthsncode.Text = ""
        txthsncode.Tag = ""
        lblgstp.Text = "GST : "
        lblgstamt.Text = "GST Amt : "
        changeBypgm = False
        btnRemove.Enabled = False
        BtnUpdate.Enabled = False
        If userType Then
            BtnUpdate.Tag = IIf(getRight(196, CurrentUser), 1, 0)
        Else
            BtnUpdate.Tag = 1
        End If
        txtCode.Focus()
        If Not grdmeasurement Is Nothing Then
            If grdmeasurement.Rows.Count > 0 Then
                grdmeasurement.Rows.Clear()
            End If
        End If
        picImage.Image = Nothing
        txtimgpath.Text = ""
        txtCode.Text = GenerateNext("")
        'loadmeasurement()
    End Sub
    Private Function GenerateNext(ByVal strCode As String) As String
        Dim N As Double
        Dim NewCode As String
        Dim i As Byte
        Dim f As Byte
        Dim tmp As String
        If strCode = "" Then
            Dim dt As DataTable = _objcmnbLayer._fldDatatable("select top 1 [item code] from invitm where itemcategory='service' order by itemid desc ")
            If dt.Rows.Count > 0 Then
                strCode = dt(0)("item code")
            End If
        End If
        If strCode = "" Then
            strCode = "SER000"
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
    Private Sub setGridHeadMeasurement()
        With grdmeasurement
            SetGridEditProperty(grdmeasurement)
            .ColumnHeadersDefaultCellStyle.BackColor = Color.Red
            .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
            .ColumnCount = 3
            .Columns(constMeasurement).HeaderText = "Measurement"
            .Columns(constMeasurement).Width = (grdmeasurement.Width / 2) - 15
            .Columns(constsecondname).HeaderText = "Second Name"
            .Columns(constsecondname).Width = (grdmeasurement.Width / 2) - 15
            .Columns(constid).HeaderText = "id"
            .Columns(constid).Visible = False
            'resizeGridColumn(grdmeasurement, 0)
        End With
    End Sub
    Private Sub addRow()
        With grdmeasurement
            .Rows.Add()
            activecontrolname = "grdmeasurement"
            .Item(constMeasurement, .Rows.Count - 1).Value = ""
            .Item(constsecondname, .Rows.Count - 1).Value = ""
            .Item(constid, .Rows.Count - 1).Value = 0
            .CurrentCell = .Item(constMeasurement, .Rows.Count - 1)
            BtnUpdate.Enabled = True
            grdBeginEdit()
        End With
    End Sub

    Private Sub btnremline_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnremline.Click
        If grdmeasurement.CurrentRow Is Nothing Then Exit Sub
        If MsgBox("Do you want to remove row?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        With grdmeasurement
            Dim dt As DataTable
            dt = _objcmnbLayer._fldDatatable("select measuermentid from StichingJobMeasurementTb where measuermentid=" & Val(grdmeasurement.Item(constid, .CurrentRow.Index).Value))
            If dt.Rows.Count > 0 Then
                MsgBox("Job found for this measurement! you cannot remove", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            _objcmnbLayer._saveDatawithOutParm("delete from StichingMeasurementTb where measurementid=" & Val(grdmeasurement.Item(constid, .CurrentRow.Index).Value))
            .Rows.RemoveAt(.CurrentRow.Index)
        End With
    End Sub

    Private Sub grdItem_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItem.CellContentClick

    End Sub

    Private Sub grdItem_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdItem.DoubleClick
        If grdItem.CurrentRow Is Nothing Then Exit Sub
        txtCode.Tag = grdItem.Item("itemid", grdItem.CurrentRow.Index).Value
        loadToEdit()
    End Sub
    Private Sub loadToEdit()
        Try
            Dim dt As DataTable
            dt = _objcmnbLayer._fldDatatable("SELECT [Item Code] ,Description,UnitPrice,InvItm.HSNCode,isnull(IGST,0) GST," & _
                                             "(((isnull(IGST,0))*UnitPrice)/100)+UnitPrice [Tax Price],Model,Make ,UnitPriceWS," & _
                                             "itemid,isnull(ishide,0)ishide  FROM InvItm" & _
                                             " LEFT JOIN VatMasterTb ON VatMasterTb.vatid=InvItm.vatid " & _
                                             " LEFT JOIN GSTTb ON GSTTb.HSNCode=InvItm.HSNCode where itemid=" & Val(txtCode.Tag))
            If dt.Rows.Count > 0 Then
                changeBypgm = True
                txtCode.Text = dt(0)("Item Code")
                txtTrDescr.Text = dt(0)("Description")
                txthsncode.Text = dt(0)("HSNCode")
                NumSalesPrice.Text = Format(Val(dt(0)("UnitPrice")), numFormat)
                txtpriceWtax.Text = Format(Val(dt(0)("Tax Price")), numFormat)
                txtpriceWtax.Tag = dt(0)("GST")
                lblgstp.Text = "GST : " & Val(txtpriceWtax.Tag) & "%"
                If Not IsDBNull(dt(0)("ishide")) Then
                    chkhide.Checked = dt(0)("ishide")
                End If
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
            dt = _objcmnbLayer._fldDatatable("select measurementname,measurementid,secondname from StichingMeasurementTb where mitemid=" & Val(txtCode.Tag))
            Dim i As Integer
            If grdmeasurement.Rows.Count > 0 Then grdmeasurement.Rows.Clear()
            For i = 0 To dt.Rows.Count - 1
                With grdmeasurement
                    .Rows.Add()
                    .Item(constMeasurement, .Rows.Count - 1).Value = Trim(dt(i)("measurementname") & "")
                    .Item(constsecondname, .Rows.Count - 1).Value = Trim(dt(i)("secondname") & "")
                    .Item(constid, .Rows.Count - 1).Value = dt(i)("measurementid")
                End With
            Next
            ldImage()
            txtCode.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Public Sub ldImage()
        dtPhotopath = DPath & "Photos"
        Dim objImage As Image
        picImage.Image = Nothing
        Dim dtImage As DataTable = _objcmnbLayer._fldDatatable("SELECT image1 FROM InvItm WHERE itemid=" & Val(txtCode.Tag))
        If dtImage.Rows.Count > 0 Then
            picImage.Tag = ""
            txtimgpath.Text = ""
            If Trim(dtImage(0)("image1") & "") = "" Then Exit Sub
            picImage.Tag = dtPhotopath & "\" & dtImage(0)("image1")
            txtimgpath.Text = picImage.Tag
            If File.Exists(picImage.Tag) Then
                Try
                    Using str As Stream = File.OpenRead(picImage.Tag)
                        objImage = Image.FromStream(str)
                    End Using
                    'picImage.Width = 603
                    'picImage.Height = 379
                    'picImage.Left = 64
                    'picImage.Top = 3
                    picImage.Image = objImage
                Catch ex As Exception
                    'bm = New Bitmap(Application.StartupPath & "\DOC.PNG")
                    'picImage.Image = bm
                    'picImage.Width = 80
                    'picImage.Height = 68
                    'picImage.Left = 299
                    'picImage.Top = 147
                End Try
            End If
        End If
    End Sub

    Private Sub btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnload.Click
        loadService()
    End Sub

    Private Sub txtSeq_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeq.TextChanged
        grdItem.DataSource = SearchGrid(_vtable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            Const WM_KEYDOWN As Integer = &H100
            Const WM_KEYUP As Integer = &H101
            'msg.ToString()
            If msg.Msg = WM_KEYDOWN Or msg.Msg = WM_KEYUP Then
                If activecontrolname = "grdmeasurement" Then
                    If msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up) Or msg.WParam.ToInt32() = CInt(Keys.Enter) Or msg.WParam.ToInt32() = CInt(Keys.F2) Or msg.WParam.ToInt32() = CInt(Keys.F3) Or msg.WParam.ToInt32() = CInt(Keys.F4) Or msg.WParam.ToInt32() = CInt(Keys.F6) Then
                        grdmeasurement_KeyDown(Nothing, New KeyEventArgs(keyData))
                        Return True
                    End If
                End If
            End If
ctn:
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub grdmeasurement_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdmeasurement.CellClick
        grdBeginEdit()
    End Sub

    Private Sub grdmeasurement_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdmeasurement.CellEnter
        grdBeginEdit()
    End Sub

    Private Sub grdmeasurement_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdmeasurement.GotFocus
        activecontrolname = "grdmeasurement"
        grdBeginEdit()
    End Sub

    Private Sub grdmeasurement_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdmeasurement.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If FindNextCell(grdmeasurement, grdmeasurement.CurrentCell.RowIndex, grdmeasurement.CurrentCell.ColumnIndex + 1) Then
                    addRow()
                End If
                grdBeginEdit()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdmeasurement_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdmeasurement.Leave
        activecontrolname = ""
    End Sub
    Private Sub grdBeginEdit()
        If grdmeasurement.RowCount = 0 Then Exit Sub
        changeBypgm = True
        grdmeasurement.BeginEdit(True)
        changeBypgm = False
    End Sub

    Private Sub txtpriceWtax_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtpriceWtax.Validated
        calculateTaxFromUnitPrice(False)
    End Sub

    Private Sub NumSalesPrice_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumSalesPrice.TextChanged

    End Sub

    Private Sub NumSalesPrice_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles NumSalesPrice.KeyPress
        NumericTextOnKeypress(NumSalesPrice, e, chgbyprgAmt, numFormat)
    End Sub

    Private Sub txtCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged, txthsncode.TextChanged, txtpriceWtax.TextChanged, txtTrDescr.TextChanged
        If chgbyprgAmt Then Exit Sub
        If changeBypgm Then Exit Sub
        BtnUpdate.Enabled = True
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        With DlgOpen
            .Filter = "All Picture Files|*.bmp;*.dib;*.gif;*.wmf;*.emf;*.jpg;*.ico;*.cur|" & _
                     "Bitmaps(*.bmp,*.dib)|*.bmp;*.dib|" & _
                     "Gif Images(*.gif)|*.gif|" & _
                     "JPEG Images(*.jpg)|*.jpg|" & _
                     "Matafiles(*.wmf,*.emf)|*.wmf;*.emf|" & _
                     "Icons(*.ico,*.cur)|*.ico;*.cur|" & _
                     "All Files(*.*)|*.*"
            .Title = "Select an Image file"
            .FileName = ""
            .ShowDialog()
            If .FileName <> "" Then
                Err.Clear()
                On Error Resume Next
                Dim bm As New Bitmap(.FileName)
                picImage.Image = bm
                If Err.Number Then
                    MsgBox(Err.Description)
                Else
                    picImage.Tag = .FileName
                    txtimgpath.Text = .FileName
                    BtnUpdate.Enabled = True
                    ChgID = True
                End If
            End If
            ChDir(Application.StartupPath)
        End With
    End Sub

    Private Sub cmbcopyfrom_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbcopyfrom.SelectedIndexChanged
        loadmeasurement()
    End Sub
    Private Sub loadmeasurement()
        If cmbcopyfrom.Text = "" Then Exit Sub
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("select measurementname,measurementid,secondname from StichingMeasurementTb " & _
                                         "left join invitm on StichingMeasurementTb.mitemid=invitm.itemid where [Description]='" & cmbcopyfrom.Text & "'")
        Dim i As Integer
        If grdmeasurement.Rows.Count > 0 Then grdmeasurement.Rows.Clear()
        For i = 0 To dt.Rows.Count - 1
            With grdmeasurement
                .Rows.Add()
                .Item(constMeasurement, .Rows.Count - 1).Value = dt(i)("measurementname")
                .Item(constsecondname, .Rows.Count - 1).Value = dt(i)("secondname")
                .Item(constid, .Rows.Count - 1).Value = ""
            End With
        Next
        txtCode.Focus()
    End Sub

    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Dim dttable As DataTable = _objcmnbLayer._fldDatatable("SELECT ITEMID FROM ITMINVTRTB WHERE itemid =" & Val(txtCode.Tag) & _
                                              " UNION ALL SELECT ITEMID FROM JobitemTb WHERE itemid =" & Val(txtCode.Tag))
        If dttable.Rows.Count > 0 Then
            MsgBox("Transaction Exist !", MsgBoxStyle.Exclamation, "Cannot Delete !")
            Exit Sub
        End If
        If MsgBox("Do you want remove item?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            _objcmnbLayer._saveDatawithOutParm("DELETE FROM INVITM WHERE Itemid=" & Val(txtCode.Tag) & "DELETE FROM StichingMeasurementTb WHERE mitemid=" & Val(txtCode.Tag))
        End If
        loadService()
        clearControls()
    End Sub


    Private Sub chkhide_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkhide.Click
        If Val(txtCode.Tag) = 0 Then
            MsgBox("Invalid Item", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If chkhide.Checked Then
            If MsgBox("Do you want to hide this item?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        Else
            If MsgBox("Do you want to visible this item?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        End If
        _objcmnbLayer._saveDatawithOutParm("Update invitm set ishide=" & IIf(chkhide.Checked, 1, 0) & " where itemid=" & Val(txtCode.Tag))
        clearControls()
        loadService()
        MsgBox("Updated", MsgBoxStyle.Information)
    End Sub

    Private Sub cmbcategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbcategory.SelectedIndexChanged
        loadService()
    End Sub
End Class