
Public Class ContractMemberShipFrm
#Region "Class Objects"
    Private _objJob As New clsJob
    Private _objcmnbLayer As New clsCommon_BL
    Private _objInv As New clsInvoice
    Private _objTr As New clsAccountTransaction
    Private _objReport As New clsReport_BL
#End Region
#Region "Form Objects"
    Private WithEvents fDoc As DocumentView
    Private WithEvents fSelect As Selectfrm
    Private WithEvents fCrtAcc As CreateAccNew
    Private WithEvents fProductEnquiry As ItmEnqry
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fRptFormat As RptFormatfrm
    Private WithEvents fMList As Mlistfrm
    Private WithEvents fInvoice As MFSalesInvoice
#End Region
#Region "Private variables"
    Private chgbyprg As Boolean
    Private SrchText As String
    Private textIndex As Integer

    Private _srchOnce As Boolean
    Private _srchIndexId As Byte
    Private _srchTxtId As Byte

    Private strGridSrchString As String
    Private onceChgFld As Boolean
    Private MyActiveControl As New Object
    Private NDec As Integer = NoOfDecimal
    Private chgPost As Boolean

    Private activecontrolname As String
    Private _vtable As DataTable
    Private _dtRptTable As DataTable
    Private chgItm As Boolean
    Private lstKey As Integer
    Private bm As Bitmap
#End Region
#Region "Constant Variables"
    Private Const ConstStartDate = 4
    Private Const ConstEndDate = 5
    Private Const ConstPrice = 6 'tax price
    Private Const ConstIsclosed = 7
    Private Const ConstTag = 8
    Private Const ConstInvoiceNumber = 9
    Private Const Constrenewid = 10
    Private Const ConstItemid = 11
    Private Const constDays = 12
#End Region
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub ContractMemberShipFrm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtcustomer.Focus()
    End Sub

    Private Sub ContractMemberShipFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AddNew()
        setGridHead()
        ldJobdetails()
        If userType Then
            btnupdate.Tag = IIf(getRight(225, CurrentUser), 1, 0)
        Else
            btnupdate.Tag = 1
        End If
        Timer1.Enabled = True
        cmbidentityproof.SelectedIndex = 0
    End Sub
    Private Sub AddNew()
        chgbyprg = True
        txtcode.Text = GenerateNext("")
        chgbyprg = False
    End Sub
    Private Function GenerateNext(ByVal strCode As String) As String
        Dim N As Double
        Dim NewCode As String
        Dim i As Byte
        Dim f As Byte
        Dim tmp As String
        Dim dt As DataTable
        If strCode = "" Then
            dt = _objcmnbLayer._fldDatatable("SELECT top 1 jobcode from JobTb " & _
                                             " order by JobTb.Jobid desc")
            If dt.Rows.Count > 0 Then
                strCode = dt(0)(0)
            End If
        End If
        If strCode = "" Then
            strCode = "ADM"
        End If
        Dim dr As DataTable
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
                dr = _objcmnbLayer._fldDatatable("SELECT jobcode from JobTb WHERE jobcode = '" & tmp & "'")
                If dr.Rows.Count = 0 Then
                    NewCode = Mid(strCode, 1, IIf(Len(strCode) <> 0, Len(strCode) - f, 0)) & Format(N, StrDup(i, "0"))
                    NewCode = Mid(NewCode, 1, 30)
                    Exit Do
                End If
            Loop
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return NewCode
    End Function

    Private Sub btnaddcustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddcustomer.Click
        QuickCust("Customer")
    End Sub
    Private Sub fCrtAcc_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fCrtAcc.FormClosed
        fCrtAcc = Nothing
    End Sub

    Private Sub fCrtAcc_OpenAccMaster(ByRef AccountNo As Long) Handles fCrtAcc.OpenAccMaster
        txtcustomer.Tag = AccountNo
        loadCustomerDet(AccountNo)
    End Sub
    Private Sub QuickCust(Optional ByVal Grp As String = "Customer")
        fCrtAcc = New CreateAccNew
        With fCrtAcc
            .Condition = "GrpSetOn In ('" & Grp & "')"
            If Grp = "Customer" Then
                .iscust = True
            Else
                .iscust = False
            End If
            .bOnlyOne = True
            .ShowDialog()
        End With
        txtage.Focus()
    End Sub

    Private Sub txtcustomer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtcustomer.KeyDown
        lstKey = e.KeyCode
        If e.KeyCode = Keys.Return Then
            txtage.Focus()
        End If
    End Sub

    Private Sub txtcustomer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcustomer.TextChanged
        If chgbyprg = True Then Exit Sub
        Dim myCtrl As Control = sender
        Select Case myCtrl.Name
            Case "txtcustomer"
                _srchTxtId = 1
        End Select
        txtcustomer.Tag = ""
        _srchOnce = False
        ShowFmlist(sender)
        chgPost = True
        btnupdate.Enabled = True
    End Sub
    Private Sub ShowFmlist(ByVal myCtrl As Control)
        If chgbyprg Then Exit Sub
        MyActiveControl = myCtrl
        If Not _srchOnce Then
            chgbyprg = True
            If fMList Is Nothing Then
                fMList = New Mlistfrm
            End If
            Dim PopupLoc As Point
            Dim x As Integer
            Dim y As Integer
            If _srchTxtId = 4 Then
                x = Me.Left + 480
                y = Me.Top + 465
            ElseIf _srchTxtId = 2 Then
                x = Me.Left + 100
                y = Me.Top + 300
            Else
                x = Me.Left + 480
                y = Me.Top + 320
            End If

            PopupLoc = New Point(x, y)
            If fMList.Visible = False Then
                Select Case _srchTxtId
                    Case 1
                        SetFmlist(fMList, 3)
                End Select
                If _srchTxtId = 4 Then
                    fMList.Height = fMList.Height - 50
                End If
                fMList.loc = PopupLoc
                fMList.Show(fMainForm)
                fMList.Visible = True
            End If
        End If
        Select Case _srchTxtId
            Case 1   'Customer name
                fMList.SearchIndex = 0
                fMList.SearchIndexDescr = 1
                fMList_doFocus()
                fMList.Search(txtcustomer.Text)
                fMList.AssignList(txtcustomer, lstKey, chgbyprg)
        End Select
        _srchOnce = True
        chgbyprg = False
    End Sub
    Private Sub fMList_doClose() Handles fMList.doClose
        fMList = Nothing
    End Sub

    Private Sub fMList_doFocus() Handles fMList.doFocus
        If TypeOf (MyActiveControl) Is TextBox Then
            MyActiveControl.focus()
        End If
    End Sub

    Private Sub fMList_doSelect(ByVal ItmFlds() As String) Handles fMList.doSelect
        chgbyprg = True
        Select Case _srchTxtId
            Case 1
                txtcustomer.Text = ItmFlds(0)
                txtcustomer.Tag = ItmFlds(3)
        End Select
        chgbyprg = False
    End Sub

    Private Sub txtage_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtage.KeyDown, cmbidentityproof.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtidentityProofNumber_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtidentityProofNumber.KeyDown
        If e.KeyCode = Keys.Return Then
            btnadd.Focus()
        End If
    End Sub
    Private Sub setGridHead()
        SetGridEditProperty(grdpackage)
        With grdpackage
            .ColumnCount = 13
            .ColumnHeadersHeight = 100
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersDefaultCellStyle.BackColor = Color.Olive
            .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.5!, System.Drawing.FontStyle.Regular)
            .RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.0!)
            '.ReadOnly = True

            .Columns(ConstSlNo).HeaderText = "SlNo"
            .Columns(ConstSlNo).Width = 40
            .Columns(ConstSlNo).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstSlNo).Frozen = True
            .Columns(ConstSlNo).DefaultCellStyle.Format = "N0"
            .Columns(ConstSlNo).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstSlNo).ReadOnly = True

            .Columns(ConstItemCode).HeaderText = "Package"
            .Columns(ConstItemCode).Width = 75
            .Columns(ConstItemCode).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(ConstItemCode).ReadOnly = False

            .Columns(ConstBarcode).HeaderText = "HSN Code"
            .Columns(ConstBarcode).Width = 100
            .Columns(ConstBarcode).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstBarcode).ReadOnly = True
            .Columns(ConstBarcode).Visible = False

            .Columns(ConstDescr).HeaderText = "Description"
            .Columns(ConstDescr).Width = 220
            .Columns(ConstDescr).SortMode = DataGridViewColumnSortMode.NotSortable

            Dim col As New CalendarColumn()
            .Columns(ConstStartDate).Width = 50
            .Columns(ConstStartDate).SortMode = DataGridViewColumnSortMode.NotSortable
            col = New CalendarColumn()
            .Columns.RemoveAt(ConstStartDate)
            col.DataPropertyName = "Startdate"
            .Columns.Insert(ConstStartDate, col)
            .Columns(ConstStartDate).HeaderText = "Start Date"
            .Columns(ConstStartDate).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns(ConstEndDate).Width = 50
            .Columns(ConstEndDate).SortMode = DataGridViewColumnSortMode.NotSortable
            col = New CalendarColumn()
            .Columns.RemoveAt(ConstEndDate)
            col.DataPropertyName = "Startdate"
            .Columns.Insert(ConstEndDate, col)
            .Columns(ConstEndDate).HeaderText = "End Date"
            .Columns(ConstEndDate).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns(ConstPrice).HeaderText = "Price"
            .Columns(ConstPrice).Width = 70
            .Columns(ConstPrice).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstPrice).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstPrice).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstPrice).DefaultCellStyle.BackColor = Color.GreenYellow
            .Columns(ConstPrice).ReadOnly = False

            .Columns(ConstIsclosed).HeaderText = "Is Closed?"
            .Columns(ConstIsclosed).ReadOnly = True
            .Columns(ConstIsclosed).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns(ConstTag).HeaderText = "Tag"
            .Columns(ConstTag).ReadOnly = True
            .Columns(ConstTag).Width = 50
            .Columns(ConstTag).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns(ConstInvoiceNumber).HeaderText = "Inovice No"
            .Columns(ConstInvoiceNumber).ReadOnly = True
            .Columns(Constrenewid).Visible = False
            .Columns(ConstItemid).Visible = False
            .Columns(constDays).Visible = False
        End With
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        resizeGridColumn(grdpackage, ConstDescr)
    End Sub

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        AddRow()
    End Sub
    Private Sub AddRow()
        Dim i As Integer
        With grdpackage
            activecontrolname = "grdpackage"
            i = .RowCount '- 1
            .Rows.Add(1)
            .Item(ConstSlNo, i).Value = i + 1
            .Item(ConstBarcode, i).Value = ""
            .Item(ConstItemCode, i).Value = ""
            .Item(ConstStartDate, i).Value = DateValue(cldrdate.Value)
            .Item(ConstDescr, i).Value = ""
            .CurrentCell = .Item(ConstItemCode, i)
            chgbyprg = True
            .BeginEdit(True)
            chgbyprg = False
            chgItm = False
        End With
    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            Const WM_KEYDOWN As Integer = &H100
            Const WM_KEYUP As Integer = &H101
            'msg.ToString()
            If msg.Msg = WM_KEYDOWN Or msg.Msg = WM_KEYUP Then
                If msg.WParam.ToInt32() = CInt(Keys.F8) Then
                    'UpdateClick()
                ElseIf msg.WParam.ToInt32() = CInt(Keys.F3) Then
                    grdpackage_KeyDown(Nothing, New KeyEventArgs(keyData))
                ElseIf activecontrolname = "grdpackage" Then
                    If msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up) Or msg.WParam.ToInt32() = CInt(Keys.Enter) Or msg.WParam.ToInt32() = CInt(Keys.F2) Or msg.WParam.ToInt32() = CInt(Keys.F3) Or msg.WParam.ToInt32() = CInt(Keys.F4) Or msg.WParam.ToInt32() = CInt(Keys.F6) Then
                        If (msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up)) And plsrch.Visible = False Then GoTo ctn
                        grdpackage_KeyDown(Nothing, New KeyEventArgs(keyData))
                        Return True
                    End If
                ElseIf msg.WParam.ToInt32() = CInt(Keys.Escape) Then
                    If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
                    If Not fSelect Is Nothing Then fSelect.Close() : fSelect = Nothing
                    If Not fProductEnquiry Is Nothing Then fProductEnquiry.Close() : fProductEnquiry = Nothing

                End If
            End If
ctn:
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function
    Private Sub grdBeginEdit()
        chgbyprg = True
        grdpackage.BeginEdit(True)
        chgbyprg = False
    End Sub

    Private Sub grdpackage_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdpackage.CellClick
        If e.ColumnIndex = ConstTag Then
            With grdpackage
                If .Item(ConstInvoiceNumber, e.RowIndex).Value <> "" Then
                    MsgBox("Already invoiced", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If
                .Item(e.ColumnIndex, e.RowIndex).Value = IIf(.Item(e.ColumnIndex, e.RowIndex).Value = "Y", "", "Y")
            End With
        Else
            grdBeginEdit()
        End If

    End Sub


    Private Sub grdpackage_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdpackage.CellEndEdit
        If chgbyprg Then Exit Sub
        Dim col As Integer = e.ColumnIndex
        chgbyprg = True
        If col = ConstPrice Then
            If Val(grdpackage.Item(col, e.RowIndex).Value) = 0 Then grdpackage.Item(col, e.RowIndex).Value = 0
            grdpackage.Item(col, e.RowIndex).Value = Format(Convert.ToDecimal(grdpackage.Item(col, e.RowIndex).Value), "#,##0" & _
                                                            IIf(NoOfDecimal = 0, "", "." & Strings.StrDup(NoOfDecimal, "0")))
        End If
        chgbyprg = False
    End Sub

    Private Sub grdpackage_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdpackage.CellEnter
        grdBeginEdit()
    End Sub

    Private Sub grdpackage_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdpackage.CellValidated
        Try
            Valid(e.RowIndex, e.ColumnIndex)
            SrchText = ""
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub grdpackage_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdpackage.CellValueChanged
        If chgbyprg Then Exit Sub
        'ChgByPrg = True
        With grdpackage
            Dim i As Integer = e.RowIndex
            'btnUpdate.Enabled = True
            chgPost = True
            Select Case e.ColumnIndex
                Case ConstItemCode
                    chgItm = True
            End Select
        End With
    End Sub

    Private Sub grdpackage_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdpackage.EditingControlShowing
        Dim Col As Integer
        Col = grdpackage.CurrentCell.ColumnIndex
        If Col = ConstItemCode Then
            Dim tb As TextBox = CType(e.Control, TextBox)
            RemoveHandler tb.TextChanged, AddressOf Textbox_TextChanged
            AddHandler tb.TextChanged, AddressOf Textbox_TextChanged
        End If
        If Col = ConstItemCode Then
            Dim tb As TextBox = CType(e.Control, TextBox)
            RemoveHandler tb.KeyPress, AddressOf Textbox_KeyPress
            AddHandler tb.KeyPress, AddressOf Textbox_KeyPress
        End If
    End Sub
    Private Sub Textbox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            Dim col As Integer
            col = grdpackage.CurrentCell.ColumnIndex
            If col = ConstPrice Then
                If Not IsNumeric(e.KeyChar) And e.KeyChar <> "." And Not e.KeyChar = Convert.ToChar(Keys.Back) Then e.Handled = True
                If col = ConstQty Then
                    If grdpackage.Item(ConstSerialNo, grdpackage.CurrentRow.Index).Value <> "" And Not enableBatchwiseTr Then
                        e.Handled = True
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub Textbox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Try
            Dim col As Integer
            col = grdpackage.CurrentCell.ColumnIndex
            Dim MyCtrl As TextBox = sender
            If col = ConstItemCode Or ConstDescr Then strGridSrchString = MyCtrl.Text
            If chgbyprg Then Exit Sub
            _srchOnce = False
            If col = ConstItemCode Then
                _srchTxtId = 1
                '_srchIndexId = 1
                chgbyprg = True
                strGridSrchString = MyCtrl.Text
                ShowPanel()
                chgItm = True
                chgbyprg = False
                'grdpackage.Item(ConstqtyChg, grdpackage.CurrentRow.Index).Value = "Chg"
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub ShowPanel(Optional ByVal isrefreshBatchData As Boolean = False)
        If Not _srchOnce Then
            chgbyprg = True
            Dim PopupLoc As Point
            Dim x As Integer
            Dim y As Integer
            If _srchTxtId = 3 Then
                plsrch.Height = 246
                plsrch.Width = 600
            Else

                plsrch.Height = 300
                plsrch.Width = 700
                'x = Me.Width - plsrch.Width - 100
                'y = Me.Height - plsrch.Height - 100
            End If
            x = grdpackage.Left + grdpackage.Width - plsrch.Width
            y = grdpackage.Top
            PopupLoc = New Point(x, y)
            If plsrch.Visible = False Then
                plsrch.Location = PopupLoc
                plsrch.Visible = True
            End If
        End If
        SearchProductPanel(grdSrch, "[Item Code]", strGridSrchString, True, , , "package")
        If grdSrch.RowCount > 0 And strGridSrchString = "" Then
            strGridSrchString = grdSrch.Item(0, 0).Value
        End If

        doSelect(2)
        _srchOnce = True
        chgbyprg = False
    End Sub
    Private Sub doSelect(ByVal Mup As Integer)
        Try
            chgbyprg = True
            Dim ItmFlds() As String
            If plsrch.Visible = False Then Exit Sub
            If Mup = 0 Then 'UP
                ItmFlds = MoveUpPl(grdSrch, _srchIndexId, strGridSrchString)
            ElseIf Mup = 1 Then 'Down
                ItmFlds = MoveDownPl(grdSrch, _srchIndexId, strGridSrchString)
            Else
                ItmFlds = SelectItmPanel(grdSrch)
            End If
            If strGridSrchString = "" And Mup = 2 Then SrchText = "" : GoTo Nxt

            Select Case _srchTxtId
                Case 1
                    grdpackage.Item(ConstItemCode, grdpackage.CurrentCell.RowIndex).Value = IIf(ItmFlds(0) IsNot Nothing, ItmFlds(0), "")
                    'grdpackage.Item(ConstBarcode, grdpackage.CurrentCell.RowIndex).Value = IIf(ItmFlds(1) IsNot Nothing, ItmFlds(1), "")
                    grdpackage.Item(ConstDescr, grdpackage.CurrentCell.RowIndex).Value = IIf(ItmFlds(2) IsNot Nothing, ItmFlds(1), "")
                    SrchText = IIf(ItmFlds(0) IsNot Nothing, ItmFlds(0), strGridSrchString)
            End Select
nxt:
            chgbyprg = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub grdpackage_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdpackage.GotFocus
        If chgbyprg Then Exit Sub
        If grdpackage.RowCount = 0 Then
            AddRow()
        End If
        activecontrolname = "grdpackage"
    End Sub

    Private Sub grdpackage_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdpackage.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If grdpackage.RowCount = 0 Then Exit Sub
                plsrch.Visible = False
                If Trim(SrchText) = "" And grdpackage.CurrentCell.ColumnIndex = ConstItemCode Then
                    SrchText = grdpackage.Item(ConstItemCode, grdpackage.CurrentRow.Index).Value
                    If Trim(SrchText) = "" Then
                        If enableItemAutoPopulate And Val(grdpackage.Item(ConstSlNo, grdpackage.CurrentRow.Index).Value) > 0 Then
                            fProductEnquiry = New ItmEnqry
                            fProductEnquiry.ShowDialog()
                        End If
                        If SrchText = "" Then GoTo nxt
                    Else
                        GoTo cntu
                    End If
                End If
cntu:
                If FindNextCell(grdpackage, grdpackage.CurrentCell.RowIndex, grdpackage.CurrentCell.ColumnIndex + 1) Then
                    AddRow()
                End If
nxt:

                grdBeginEdit()

            ElseIf e.KeyCode = Keys.Up Or e.KeyCode = Keys.PageUp Then
                If grdpackage.RowCount = 0 Then Exit Sub
                If plsrch.Visible Then
                    doSelect(0)
                    Exit Sub
                End If
            ElseIf e.KeyCode = Keys.Down Or e.KeyCode = Keys.PageDown Then
                If grdpackage.RowCount = 0 Then Exit Sub
                If plsrch.Visible Then
                    doSelect(1)
                    Exit Sub
                End If
            ElseIf e.KeyCode = Keys.F1 Then
                grdBeginEdit()
            ElseIf e.KeyCode = Keys.F2 Then
                If plsrch.Visible Then plsrch.Visible = False
                If grdpackage.RowCount = 0 Then Exit Sub
                Select Case grdpackage.CurrentCell.ColumnIndex
                    Case ConstItemCode
                        If Not fMList Is Nothing Then fMList.Visible = False
                        If plsrch.Visible Then plsrch.Visible = False
                        fProductEnquiry = New ItmEnqry
                        fProductEnquiry.ShowDialog()
                End Select
            ElseIf e.KeyCode = Keys.Escape Then
                plsrch.Visible = False
            ElseIf e.KeyCode = Keys.F3 Then
                AddRow()
            ElseIf e.KeyCode = Keys.F4 Then
                RemoveRow()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub RemoveRow()
        If grdpackage.RowCount > 0 Then
            If MsgBox("You Are Going to Remove the Row." & vbCrLf & "Are You Sure ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then
                Exit Sub
            End If
            With grdpackage
                deleteDtSerialNo(_objcmnbLayer.dtSerialNo, grdpackage.Item(ConstSerialNo, grdpackage.CurrentRow.Index).Value, Val(.Item(ConstItemid, .CurrentRow.Index).Value))
                .Rows.RemoveAt(.CurrentRow.Index)
                .ClearSelection()
            End With
            reArrangeNo()
        End If

    End Sub
    Private Sub reArrangeNo()
        Dim r As Integer
        Dim i As Integer
        chgbyprg = True
        i = 0
        With grdpackage
            For r = 0 To .Rows.Count - 1 '- 1
                If CStr(.Item(ConstSlNo, r).Value) <> "M" And CStr(.Item(ConstSlNo, r).Value) <> "L" Then
                    i = i + 1
                    .Item(ConstSlNo, r).Value = i
                End If
            Next r
        End With
        chgbyprg = False
    End Sub
    Private Sub Valid(ByVal RowIndex As Integer, ByVal ColIndex As Integer)
        With grdpackage
            Select Case ColIndex
                Case ConstItemCode
                    If Not chgItm Then Exit Sub
                    If Trim(.Item(ColIndex, RowIndex).Value) = "" And SrchText <> "" Then .Item(ColIndex, RowIndex).Value = SrchText
                    If Trim(.Item(ColIndex, RowIndex).Value) <> "" And SrchText = "" Then SrchText = .Item(ColIndex, RowIndex).Value
                    Dim dtItms As DataTable
                    Dim found As Boolean
                    dtItms = getItmDtls(3, SrchText, True)
                    If dtItms.Rows.Count > 0 Then
                        found = True
                        AddDetails(dtItms)
                    End If
                    chgItm = False
                    If Not found Then
                        .Item(ConstBarcode, RowIndex).Value = ""
                        .Item(ConstItemCode, RowIndex).Value = ""
                        .Item(ConstItemid, RowIndex).Value = ""
                        chgItm = False
                    End If
                Case Else
            End Select
        End With
    End Sub
    Private Sub AddDetails(ByVal DR As DataTable)
        Dim i As Integer
        Dim PMult As Double
        chgbyprg = True
        With grdpackage
            chgbyprg = True
            PMult = 1
            i = .CurrentRow.Index ' .RowCount - 1
            .Item(ConstSlNo, i).Value = i + 1
            .Item(ConstItemCode, i).Value = IIf(IsDBNull(DR(0)("Item Code")), Trim(DR(0)("Item Code") & ""), DR(0)("Item Code"))
            .Item(ConstDescr, i).Value = DR(0)("Description")
            .Item(ConstItemid, i).Value = DR(0)("ItemId")
            .Item(constDays, i).Value = Val(DR(0)("MinQty") & "")
            If IsDate(.Item(ConstStartDate, i).Value) Then
                .Item(ConstEndDate, i).Value = DateAdd(DateInterval.Day, Val(DR(0)("MinQty") & ""), DateValue(.Item(ConstStartDate, i).Value))
            End If
            Dim Tax As Double
            Tax = Val(DR(0)("IGST") & "") + Val(DR(0)("vat") & "")
            .Item(ConstPrice, i).Value = Val(DR(0)("UnitPrice")) + (Val(DR(0)("UnitPrice")) * Tax / 100)
            chgItm = False
            .ClearSelection()
        End With
        chgbyprg = False
    End Sub

    Private Sub grdpackage_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdpackage.Leave
        activecontrolname = ""
    End Sub

    Private Sub picCloseProd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picCloseProd.Click
        plsrch.Visible = False
    End Sub

    Private Sub grdSrch_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSrch.DoubleClick
        activecontrolname = "grdpackage"
        doSelect(2)
        chgbyprg = True
        If grdpackage.CurrentCell.ColumnIndex = ConstItemCode Then
            grdpackage.CurrentCell = grdpackage.Item(1, grdpackage.CurrentRow.Index)
            chgItm = True
            Valid(grdpackage.CurrentCell.RowIndex, grdpackage.CurrentCell.ColumnIndex)
            grdBeginEdit()
        End If
        plsrch.Visible = False
        chgbyprg = False
    End Sub

    Private Sub btnundo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnundo.Click
        clearControl()
    End Sub
    Private Sub clearControl()
        Dim bm1 As Bitmap
        bm1 = New Bitmap(Application.StartupPath & "/attachment.png")
        btnattach.BackgroundImage = bm1
        chgbyprg = True
        txtcode.Text = GenerateNext(txtcode.Text)
        txtcustomer.Text = ""
        txtcustomer.Tag = ""
        txtAddr0.Text = ""
        txtage.Text = ""
        rdomale.Checked = True
        cmbidentityproof.Text = ""
        txtidentityProofNumber.Text = ""
        lblinvamt.Text = Format(0, numFormat)
        lblRv.Text = Format(0, numFormat)
        lblbalance.Text = Format(0, numFormat)
        'lblrvtotal.Text = Format(0, numFormat)
        grdreceipt.DataSource = Nothing
        grdinvlist.DataSource = Nothing
        grdpackage.Rows.Clear()
        btnundo.Text = "Clear"
        txtcustomer.Focus()
        chgbyprg = False
        picMember.BackgroundImage = Nothing
        btnrefresh.Visible = False
        txtcode.Tag = ""
        btnattach.Enabled = False
        lblcreatedinfo.Text = ""
    End Sub
    Private Sub saveJob()
        With _objJob
            .Jobid = Val(txtcode.Tag)
            .jobcode = txtcode.Text
            .jobdate = DateValue(cldrdate.Value)
            .jobname = ""
            .custcode = Val(txtcustomer.Tag)
            .EstimatedDate = DateValue(Date.Now)
            .SIID = 0
            .RvId = 0
            .Userid = CurrentUser
            txtcode.Tag = .saveJob()
        End With
        saveMembership()
        If grdpackage.RowCount > 0 Then
            savePackage()
        End If

    End Sub
    Private Sub saveMembership()
        Dim FileName As String = ""
        If picMember.Tag <> "" Then
            FileName = "ADM - " & txtcode.Text & ".jpg"
        End If
        With _objJob
            .Jobid = Val(txtcode.Tag)
            .age = Val(txtage.Text)
            .gender = IIf(rdomale.Checked, 0, 1)
            .identityProof = cmbidentityproof.Text
            .identityProofNumber = txtidentityProofNumber.Text
            .imagename = FileName
            .SaveMembership()
        End With
        If picMember.Tag <> "" Then
            'On Error Resume Next
            If DPath = "" Then Exit Sub
            FileName = "Photos\" & FileName
            If DPath & FileName = picMember.Tag Then Exit Sub
            If Not DirExist(DPath) Then
                MsgBox("Invalid Data Path", MsgBoxStyle.Exclamation)
                Exit Sub
            End If

            If FileExists(DPath & FileName) Then
                bm = Nothing
                System.IO.File.Delete(DPath & FileName)
            End If

            FileCopy(picMember.Tag, DPath & FileName)
            If Err.Number Then
                If MsgBox(Err.Description, vbRetryCancel + vbExclamation) = vbRetry Then Resume
            End If
        End If
    End Sub
    Private Sub savePackage()
        Dim i As Integer
        _objcmnbLayer._saveDatawithOutParm("update MembershipRenewalTb set setremove=1 where jobid=" & Val(txtcode.Tag))
        For i = 0 To grdpackage.Rows.Count - 1
            With grdpackage
                If Val(.Item(ConstItemid, i).Value) > 0 And .Item(ConstDescr, i).Value <> "" Then
                    _objJob.renewid = Val(.Item(Constrenewid, i).Value)
                    _objJob.Jobid = Val(txtcode.Tag)
                    _objJob.Itemid = Val(.Item(ConstItemid, i).Value)
                    _objJob.Startdate = DateValue(.Item(ConstStartDate, i).Value)
                    _objJob.EstimatedDate = DateValue(.Item(ConstEndDate, i).Value)
                    If Val(.Item(ConstPrice, i).Value) = 0 Then .Item(ConstPrice, i).Value = 0
                    _objJob.unitprice = Format(CDbl(.Item(ConstPrice, i).Value), numFormat)
                    _objJob.SaveMembershipRenewalTb()
                End If
            End With
        Next
        _objcmnbLayer._saveDatawithOutParm("delete from MembershipRenewalTb where isnull(setremove,0)=1 and jobid=" & Val(txtcode.Tag))
    End Sub
    Private Sub ldJobdetails()
        Try
            _objJob = New clsJob
            With _objJob
                .Jobid = 0
                .DateFrom = DateValue(cldrStartDate.Value)
                .DateTo = DateValue(cldrEnddate.Value)
                .custcode = 0
                '.Dtype = "CHI"
                If rdoactive.Checked Then
                    .Tp = 0
                ElseIf rdoExpirylist.Checked Then
                    .Tp = 1
                ElseIf rdoall.Checked Then
                    .Tp = 2
                ElseIf rdoexpired.Checked Then
                    .Tp = 4
                ElseIf rdoduelist.Checked Then
                    .Tp = 5
                End If
                _vtable = .returnMemberShip.Tables(0)
            End With
            grdList.DataSource = _vtable
            SetGrid()
            Dim i As Integer
            For i = 0 To grdList.RowCount - 1
                With grdList
                    If .Item("Expired", i).Value = "YES" Then
                        .Rows(i).DefaultCellStyle.BackColor = Color.LightCoral
                    End If

                End With
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub SetGrid()
        With grdList
            If .ColumnCount = 0 Then Exit Sub
            SetGridProperty(grdList)
            .Columns("SLNO").Width = 50
            .Columns("Jobcode").HeaderText = "Code"
            .Columns("jobdate").HeaderText = "Date"
            .Columns("AccDescr").HeaderText = "Customer Name"
            .Columns("Userid").HeaderText = "Created By"
            .Columns("CrdtDate").HeaderText = "Created Date"
            .Columns("Datefrom").Visible = False
            .Columns("Dateto").Visible = False
            .Columns("lnk").Visible = False
            .Columns("jobid").Visible = False
            .Columns("AccDescr").Width = 150
            .Columns("AccDescr").DefaultCellStyle.BackColor = Color.YellowGreen
            .Columns("jobdate").Width = 100
            .Columns("CrdtDate").Width = 100
            .Columns("TP").Visible = False
            .Columns("CrdtDate").Visible = False
            .Columns("Userid").Visible = False
            If rdoduelist.Checked Then
                .Columns.Item("NetAmt").DefaultCellStyle.Format = ("N" & NoOfDecimal)
                .Columns.Item("NetAmt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns.Item("NetAmt").HeaderText = "Invoice Amt"
                .Columns.Item("NetAmt").Width = 100

                .Columns.Item("RVAmount").DefaultCellStyle.Format = ("N" & NoOfDecimal)
                .Columns.Item("RVAmount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns.Item("RVAmount").HeaderText = "RV Amt"
                .Columns.Item("RVAmount").Width = 100

                .Columns.Item("Balance").DefaultCellStyle.Format = ("N" & NoOfDecimal)
                .Columns.Item("Balance").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns.Item("Balance").HeaderText = "Balance"
                .Columns.Item("Balance").Width = 100
                resizeGridColumn(grdList, 2)
            Else
                .Columns("Description").HeaderText = "Package Name"
                .Columns("startDate").HeaderText = "Start Date"
                .Columns("enddate").HeaderText = "End Date"
                .Columns("startDate").Width = 100
                .Columns("enddate").Width = 100
                .Columns("enddate").DefaultCellStyle.BackColor = Color.LightCoral
                .Columns("Description").Width = 200
                .Columns("closed").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                resizeGridColumn(grdList, 4)
            End If
            setComboGrid()

        End With
    End Sub
    Private Sub setComboGrid()
        cmbOrder.Items.Clear()
        Dim i As Integer = 0
        For i = 0 To grdList.ColumnCount - 2
            cmbOrder.Items.Add(grdList.Columns(i).HeaderText)
        Next
        If cmbOrder.Items.Count > 0 Then cmbOrder.SelectedIndex = 3
    End Sub

    Private Sub TabControl2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl2.Click
        If btnundo.Text = "Undo" Then
            MsgBox("Do Undo/Update before moving to List", MsgBoxStyle.Exclamation)
            TabControl2.SelectedIndex = 0
            Exit Sub
        End If
        If TabControl2.SelectedIndex = 1 Then
            resizeGridColumn(grdList, 4)
            Label27.Enabled = False
            txtprintjob.Enabled = False
            txtSeq.Focus()
        Else
            Label27.Enabled = True
            txtprintjob.Enabled = True

        End If
    End Sub

    Private Sub rdoactive_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoactive.CheckedChanged

    End Sub

    Private Sub rdoactive_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoactive.Click, rdoall.Click, rdoExpirylist.Click, rdoexpired.Click, rdoduelist.Click
        If rdoExpirylist.Checked Then
            pldate.Enabled = True
        Else
            pldate.Enabled = False
        End If
        ldJobdetails()
    End Sub

    Private Sub btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnload.Click
        ldJobdetails()
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        Try
            verify()
        Catch ex As Exception
            MsgBox(ex.Message & vbCrLf & "Try Again!", MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub verify()
        If Val(btnupdate.Tag) = 0 Then
            MsgBox("This user do not have permission to Update", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If btnupdate.Enabled = False Then Exit Sub
        Dim dt As DataTable
        _objcmnbLayer = New clsCommon_BL
        dt = _objcmnbLayer._fldDatatable("Select Jobid from JobTb where jobcode ='" & txtcode.Text & "' and jobid<>" & Val(txtcode.Tag))
        If dt.Rows.Count > 0 Then
            MsgBox("Job code already exist", MsgBoxStyle.Exclamation)
            txtcode.Focus()
            Exit Sub
        End If
        If Val(txtcustomer.Tag) = 0 Then
            MsgBox("Invalid Customer", MsgBoxStyle.Exclamation)
            txtcustomer.Focus()
            Exit Sub
        End If
        If grdpackage.Rows.Count = 0 Then
            MsgBox("Packages not Found", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If MsgBox("Verification is succeeded. Do you like to file it?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        saveJob()
        txtprintjob.Text = txtcode.Text
        MsgBox("Membership saved successfully", MsgBoxStyle.Information)
        clearControl()
        AddNew()
        btnundo.Text = "Clear"
    End Sub

    Private Sub txtcustomer_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcustomer.Validated
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
        If txtcustomer.Text = "" Then Exit Sub
        loadCustomerDet(Val(txtcustomer.Tag))
    End Sub
    Private Sub loadCustomerDet(ByVal accid As Integer)
        Dim dt As DataTable
        _objcmnbLayer = New clsCommon_BL
        chgbyprg = True
        dt = _objcmnbLayer._fldDatatable("SELECT accid,Alias,AccDescr,Address1,Address2,Address3,Address4,Phone,Fax,EMail,PT1,PT2,PT3,PT4," & _
                                          "TrdLcno,TrdDate,ContactName,GSTIN from AccMast LEFT JOIN AccMastAddr ON AccMast.ACCID=AccMastAddr.accountno " & _
                                          IIf(accid > 0, "where accid=" & accid, "where AccDescr='" & txtcustomer.Text & "'"))
        If dt.Rows.Count > 0 Then
            txtcustomer.Text = dt(0)("AccDescr")
            txtcustomer.Tag = dt(0)("accid")
            txtAddr0.Text = dt(0)("Address1") & vbCrLf & dt(0)("Address2") & vbCrLf & dt(0)("Address3") & vbCrLf & "Phone : " & dt(0)("Phone") & vbCrLf & dt(0)("ContactName")
            btnattach.Enabled = True
        Else
            txtcustomer.Text = ""
            btnattach.Enabled = False
        End If
        chgbyprg = False
    End Sub

    Private Sub grdList_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdList.DoubleClick
        If grdList.Rows.Count = 0 Then Exit Sub
        If grdList.CurrentRow Is Nothing Then Exit Sub
        txtcode.Tag = Val(grdList.Item("jobid", grdList.CurrentRow.Index).Value)
        loadMembershipForEdit()
        TabControl2.SelectedIndex = 0
        txtcustomer.Focus()
        btnundo.Text = "Undo"
    End Sub
    Private Sub loadMembershipForEdit()
        chgbyprg = True
        Try
            Dim dt As DataTable
            Dim ds As DataSet
            _objJob = New clsJob
            With _objJob
                .Jobid = Val(txtcode.Tag)
                .DateFrom = DateValue(cldrStartDate.Value)
                .DateTo = DateValue(cldrEnddate.Value)
                .Tp = 3
                ds = .returnMemberShip
            End With
            dt = ds.Tables(0)
            If dt.Rows.Count > 0 Then
                txtcode.Text = dt(0)("Jobcode")
                txtprintjob.Text = dt(0)("Jobcode")
                cldrdate.Value = DateValue(dt(0)("jobdate"))
                txtcode.Tag = dt(0)("jobid")
                txtcustomer.Tag = dt(0)("custcode")
                loadCustomerDet(Val(txtcustomer.Tag))
                txtage.Text = dt(0)("age")
                If Val(dt(0)("gender")) = 0 Then
                    rdomale.Checked = True
                Else
                    rdofemale.Checked = True
                End If
                cmbidentityproof.Text = dt(0)("identityProof")
                txtidentityProofNumber.Text = dt(0)("identityProofNumber")
                Dim FileName As String = DPath & "Photos\" & dt(0)("imagename")
                If FileName <> "" Then
                    Err.Clear()
                    If FileExists(FileName) Then
                        If Not bm Is Nothing Then bm = Nothing
                        bm = New Bitmap(FileName)
                        picMember.BackgroundImage = bm
                        If Err.Number Then
                            MsgBox(Err.Description)
                        Else
                            picMember.Tag = FileName
                            btnupdate.Enabled = True
                        End If
                        'bm.Dispose()
                    End If

                End If
                If Not IsDBNull(dt(0)("CrdtDate")) Then
                    lblcreatedinfo.Text = "Created on: " & dt(0)("CrdtDate")
                End If
                If Not IsDBNull(dt(0)("ModiDate")) Then
                    lblcreatedinfo.Text = lblcreatedinfo.Text & vbCrLf & "Modified on: " & dt(0)("ModiDate")
                End If
            Else
                Exit Sub
            End If
            dt = ds.Tables(1)
            loadPackage(dt)
            grdhistory.DataSource = ds.Tables(2)
            SetGridProperty(grdhistory)
            With grdhistory
                .Columns("price").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("price").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns.Item("itemid").Visible = False
                .Columns.Item("renewid").Visible = False
                resizeGridColumn(grdhistory, 1)
            End With
            fillGrid()
            'calculateJobValue(3)
            Label27.Enabled = True
            txtprintjob.Enabled = True
            Dim KeyId As String = "ADMDOC-" & txtcode.Text
            Dim dtImage As DataTable
            dtImage = _objcmnbLayer._fldDatatable("SELECT * FROM DocAttachmentTb WHERE KeyId='" & KeyId & "'")
            Dim bm1 As Bitmap
            If dtImage.Rows.Count > 0 Then
                bm1 = New Bitmap(Application.StartupPath & "/attached.png")
                btnattach.BackgroundImage = bm1
            Else
                bm1 = New Bitmap(Application.StartupPath & "/attachment.png")
                btnattach.BackgroundImage = bm1
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
        btndelete.Enabled = True
        If userType Then
            btnupdate.Tag = IIf(getRight(226, CurrentUser), 1, 0)
            btndelete.Tag = IIf(getRight(227, CurrentUser), 1, 0)
        Else
            btnupdate.Tag = 1
            btndelete.Tag = 1
        End If
        chgbyprg = False
        txtcustomer.Focus()
        btnrefresh.Visible = True
        btnattach.Enabled = True
    End Sub
    Private Sub loadPackage(ByVal dt As DataTable)
        With grdpackage
            .Rows.Clear()
            For i = 0 To dt.Rows.Count - 1
                .Rows.Add()
                i = .Rows.Count - 1
                .Item(ConstSlNo, i).Value = i + 1
                .Item(ConstItemCode, i).Value = dt(i)("Item Code")
                .Item(ConstDescr, i).Value = dt(i)("Description")
                .Item(ConstStartDate, i).Value = dt(i)("startDate")
                .Item(ConstEndDate, i).Value = dt(i)("enddate")
                If Val(dt(i)("price") & "") = 0 Then dt(i)("price") = 0
                .Item(ConstPrice, i).Value = Format(CDbl(dt(i)("price")), numFormat)
                .Item(ConstIsclosed, i).Value = dt(i)("Closed")
                .Item(ConstInvoiceNumber, i).Value = Trim(dt(i)("InvNo") & "")
                .Item(constDays, i).Value = dt(i)("minqty")
                .Item(ConstItemid, i).Value = dt(i)("itemid")
                .Item(Constrenewid, i).Value = dt(i)("renewid")
                If DateValue(.Item(ConstEndDate, i).Value) < DateValue(Date.Now) Then
                    .Rows(i).DefaultCellStyle.BackColor = Color.LightCoral
                ElseIf .Item(ConstInvoiceNumber, i).Value = "" Then
                    .Rows(i).DefaultCellStyle.BackColor = Color.LightSkyBlue
                End If

            Next
        End With
        reArrangeNo()
    End Sub
    Public Sub fillGrid()
        Dim num2 As Double
        Dim strSql As String = ("Select  prefix + case when prefix=''  then '' else '-' end + convert(varchar(12), invNo ) as [Inv No] , TrDate [Tr.Date] ,netamt [Amount]," & _
                                "Alias [Cust. Code],AccDescr [Customer Name],TrRefNo [Ref. No]," & _
                                "TrDescription  [Tr. Description],[Job Code],UserId [Created By],TrId from " & _
                                "( select  prefix, invNo ,TrDate ,netamt,Discount,AccDescr ,TrRefNo,TrDescription,Alias ,ItmInvCmnTb.UserId,[Job Code],ItmInvCmnTb.TrId from " & _
                                "ItmInvCmnTb LEFT JOIN (SELECT Trid,SUM((TrQty*(UnitCost+UnitOthCost))+taxamt)InvAmt FROM ItmInvTrTb GROUP BY Trid) Tr " & _
                                "ON  ItmInvCmnTb.Trid=Tr.Trid left join Accmast on ItmInvCmnTb.CSCode=Accmast.accid where ItmInvCmnTb.trtype='IS' and [Job Code] ='" & txtcode.Text & "') qq  order by TrDate ,InvNo")

        strSql = strSql & vbCrLf & "SELECT JVType [Type], prefix+ case when prefix=''  then '' else '-' end + convert(varchar(12), JVNum) [RV No],JVDate [RV Date],DealAmt*-1 Amount,Reference,Accdescr [Paid By],dbtr.ChqNo [Chq No],dbtr.ChqDate [Chq Date],dbtr.BankCode [Bank Code],JVTypeNo,AccTrCmn.Linkno From AccTrCmn " & _
                    "LEFT JOIN AccTrDet ON AccTrDet.Linkno=AccTrCmn.Linkno " & _
                    "LEFT JOIN (SELECT Linkno,Accdescr,ChqNo,ChqDate,BankCode FROM AccTrDet " & _
                    "LEFT JOIN AccMast On AccTrDet.accountno=AccMast.Accid where DealAmt>0) dbtr ON AccTrDet.Linkno=dbtr.Linkno " & _
                    "where JobCode ='" & txtcode.Text & "' and JobCode<>'' and JVType='RV' and DealAmt<0"

        strSql = strSql & vbCrLf & "select sum(netamt) netamt,sum(isnull(received,0))received from ItmInvCmnTb " & _
                "left join (select sum(dealamt*-1)received,Reference from AccTrDet group by Reference)AccTrDet on AccTrDet.Reference=ItmInvCmnTb.trrefno " & _
                "where trtype='IS' and [Job Code] ='" & txtcode.Text & "' and isnull(netamt,0)-isnull(received,0)>0"

        grdinvlist.DataSource = Nothing
        _objcmnbLayer = New clsCommon_BL
        Dim ds As DataSet = _objcmnbLayer._ldDataset(strSql, False)
        grdinvlist.DataSource = ds.Tables(0)
        Dim num3 As Integer = (ds.Tables(0).Rows.Count - 1)
        Dim i As Integer = 0
        Do While (i <= num3)
            num2 = num2 + ds.Tables(0)(i)("Amount")
            i += 1
        Loop
        grdreceipt.DataSource = Nothing
        grdreceipt.DataSource = ds.Tables(1)
        SetGridHeadInv()
        Dim totalRv As Double
        With grdreceipt
            For i = 0 To .RowCount - 1
                If .Item("type", i).Value = "RV" Then
                    totalRv = totalRv + CDbl(.Item("Amount", i).Value)
                End If
            Next
        End With
        lblinvoicetotal.Text = Format(num2, numFormat)
        lblrvtotal.Text = Format(totalRv, numFormat)
        If ds.Tables(2).Rows.Count > 0 Then
            lblinvamt.Text = Format(Val(ds.Tables(2)(0)("netamt") & ""), numFormat)
            lblRv.Text = Format(Val(ds.Tables(2)(0)("received") & ""), numFormat)
            lblbalance.Text = Format(Val(ds.Tables(2)(0)("netamt") & "") - Val(ds.Tables(2)(0)("received") & ""), numFormat)
        End If



        chgbyprg = False
    End Sub

    Private Sub SetGridHeadInv()

        If grdinvlist.ColumnCount = 0 Then Exit Sub
        SetGridProperty(grdinvlist)
        With grdinvlist
            .Columns.Item((.ColumnCount - 1)).Visible = False
            .Columns.Item("Inv No").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns.Item("Inv No").Width = &H4B
            .Columns.Item("Customer Name").Width = 200
            .Columns.Item("Tr. Description").Width = 200
            .Columns.Item("Amount").DefaultCellStyle.Format = ("N" & NoOfDecimal)
            '.Columns.Item("Amount").Frozen = True
            .Columns.Item("Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .RowTemplate.DefaultCellStyle.ForeColor = Color.Black
        End With
        If grdreceipt.ColumnCount = 0 Then Exit Sub
        SetGridProperty(grdreceipt)
        With grdreceipt
            .RowTemplate.DefaultCellStyle.ForeColor = Color.Black
            .Columns.Item((.ColumnCount - 1)).Visible = False
            .Columns.Item("RV No").Width = &H4B
            .Columns.Item("type").Visible = False
            .Columns.Item("Paid By").Width = 150
            .Columns.Item("Bank Code").Width = 100
            .Columns.Item("Amount").DefaultCellStyle.Format = ("N" & NoOfDecimal)
            '.Columns.Item("Amount").Frozen = True
            .Columns.Item("Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns.Item("JVTypeNo").Visible = False
        End With
    End Sub

    Private Sub txtcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcode.TextChanged, txtage.TextChanged, txtidentityProofNumber.TextChanged
        If chgbyprg Then Exit Sub
        btnupdate.Enabled = True
    End Sub

    Private Sub rdomale_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdomale.Click, rdofemale.Click
        btnupdate.Enabled = True
    End Sub

    Private Sub cmbidentityproof_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbidentityproof.SelectedIndexChanged
        If chgbyprg Then Exit Sub
        btnupdate.Enabled = True
    End Sub

    Private Sub btnaddreceipt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddreceipt.Click
        If txtcustomer.Text = "" Then
            MsgBox("Invalid Customer", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        fMainForm.LoadRV(0, txtcustomer.Text)
    End Sub


    Private Sub grdreceipt_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdreceipt.DoubleClick
        If grdreceipt.RowCount = 0 Then Exit Sub
        fMainForm.LoadRV(Val(grdreceipt.Item("linkno", grdreceipt.CurrentRow.Index).Value), txtcustomer.Text)
        btnaddreceipt.Tag = 1
    End Sub

    Private Sub btncreateinvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncreateinvoice.Click
        If (Not fInvoice Is Nothing) Then
            fInvoice = Nothing
        End If
        fInvoice = New MFSalesInvoice
        fInvoice.MdiParent = fMainForm
        Dim i As Integer
        Dim ids As String = ""
        For i = 0 To grdpackage.Rows.Count - 1
            If grdpackage.Item(ConstTag, i).Value = "Y" Then
                ids = ids & IIf(ids = "", "", ",") & Val(grdpackage.Item(Constrenewid, i).Value)
            End If
        Next
        If ids = "" Then
            MsgBox("Select Package from the list", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        fInvoice.Show()
        fInvoice.isJobCategory = 1
        fInvoice.returnLodgeMemberShip(Val(txtcode.Tag), ids)
    End Sub

    Private Sub fInvoice_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fInvoice.FormClosed
        fInvoice = Nothing
    End Sub


    Private Sub grdinvlist_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdinvlist.DoubleClick
        If grdinvlist.Rows.Count = 0 Then Exit Sub
        fMainForm.LoadIS(Val(grdinvlist.Item("Trid", grdinvlist.CurrentRow.Index).Value))
    End Sub

    Private Sub btnclosejob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclosejob.Click
        If grdpackage.Rows.Count = 0 Then Exit Sub
        If grdpackage.CurrentRow Is Nothing Then Exit Sub
        If MsgBox("Do you want to close selected package?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        _objcmnbLayer._saveDatawithOutParm("Update MembershipRenewalTb set isclosed=1 where renewid=" & Val(grdpackage.Item(Constrenewid, grdpackage.CurrentRow.Index).Value))
        loadMembershipForEdit()
    End Sub

    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        loadMembershipForEdit()
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Dim RptType As String = ""
        Dim RptCaption As String = ""
        If TabControl2.SelectedIndex = 1 Then
            If rdoduelist.Checked Then
                RptType = "MDU"
            Else
                RptType = "MLST"
            End If

        ElseIf TabControl2.SelectedIndex = 0 Then
            If txtprintjob.Text = "" Then
                MsgBox("Invalid Membership Code", MsgBoxStyle.Information)
                Exit Sub
            End If
            RptType = "MCD"
        End If
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
        If TabControl2.SelectedIndex = 0 Then
            ds = _objReport.rerturnMembershipCardForPrint(txtprintjob.Text)
        Else
            If _dtRptTable Is Nothing Then
                With _objJob
                    If rdoactive.Checked Then
                        .Tp = 0
                    ElseIf rdoExpirylist.Checked Then
                        .Tp = 1
                    ElseIf rdoall.Checked Then
                        .Tp = 2
                    ElseIf rdoexpired.Checked Then
                        .Tp = 4
                    ElseIf rdoduelist.Checked Then
                        .Tp = 5
                    End If
                    ds = .returnMemberShip
                End With
            Else
                ds.Tables.Add(_dtRptTable)
            End If
        End If
        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = fMainForm
        frm.Text = RptCaption
        frm.Show()
    End Sub

    Private Sub txtSeq_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSeq.TextChanged
        grdList.DataSource = SearchGrid(_vtable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
        _dtRptTable = SearchGrid(_vtable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
    End Sub

    Private Sub grdList_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdList.CellContentClick

    End Sub

    Private Sub btnbrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnbrowse.Click
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
                picMember.Image = Nothing
                Err.Clear()
                On Error Resume Next
                If Not bm Is Nothing Then
                    bm.Dispose()
                End If
                bm = New Bitmap(.FileName)
                picMember.BackgroundImage = bm
                If Err.Number Then
                    MsgBox(Err.Description)
                Else
                    picMember.Tag = .FileName
                    btnupdate.Enabled = True
                End If
            End If
            ChDir(Application.StartupPath)
        End With
    End Sub


    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        If Val(btndelete.Tag) = 0 Then
            MsgBox("This user do not have permission to Remove", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If grdinvlist.RowCount > 0 Then
            MsgBox("Invoice found! cannot remove the document", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If grdreceipt.RowCount > 0 Then
            MsgBox("RV found! cannot remove the document", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If MsgBox("You are going remove the document! are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        _objcmnbLayer._saveDatawithOutParm("DELETE FROM MembershipRenewalTb where jobid=" & Val(txtcode.Tag))
        _objcmnbLayer._saveDatawithOutParm("DELETE FROM MembershipDb where jobid=" & Val(txtcode.Tag))
        _objcmnbLayer._saveDatawithOutParm("DELETE FROM JobTb where jobid=" & Val(txtcode.Tag))
        btnundo.Text = "Clear"
        AddNew()
        clearControl()
    End Sub

    Private Sub btnrem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrem.Click
        If grdpackage.Rows.Count = 0 Then Exit Sub
        If grdpackage.CurrentRow Is Nothing Then Exit Sub
        If grdpackage.Item(ConstInvoiceNumber, grdpackage.CurrentRow.Index).Value <> "" Then
            MsgBox("Already invoiced", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If MsgBox("Do you want remove the row?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        grdpackage.Rows.RemoveAt(grdpackage.CurrentRow.Index)
        chgPost = True
        btnupdate.Enabled = True
    End Sub

    Private Sub btnattach_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnattach.Click
        Try
            If Not DirExist(DPath) Then
                MsgBox("Invalid Data Path", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            Dim jobid As Integer
            If Not fDoc Is Nothing Then fDoc = Nothing
            jobid = Val(txtcode.Tag)
            fDoc = New DocumentView
            If txtcustomer.Text = "" Then
                MsgBox("Invalid Customer", MsgBoxStyle.Exclamation)
                txtcustomer.Focus()
                Exit Sub
            End If
            If txtcode.Text <> "" And txtcustomer.Text <> "" Then
                fDoc.KeyId = "ADMDOC-" & txtcode.Text
                fDoc.moduleid = 6
                fDoc.isDoc = True
                fDoc.itemid = 0
                fDoc.ldImage()
                fDoc.ShowDialog()
            Else
                fDoc.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnremoveimage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnremoveimage.Click
        picMember.BackgroundImage = Nothing
        If Not bm Is Nothing Then bm.Dispose()
        picMember.Tag = ""
    End Sub

    Private Sub cldrdate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cldrdate.KeyDown
        If e.KeyCode = Keys.Return Then
            txtcustomer.Focus()
        End If
    End Sub

    Private Sub btnundoclosing_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnundoclosing.Click
        If grdhistory.Rows.Count = 0 Then Exit Sub
        If grdhistory.CurrentRow Is Nothing Then Exit Sub
        If MsgBox("Do you want to Undo closed package?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        _objcmnbLayer._saveDatawithOutParm("Update MembershipRenewalTb set isclosed=0 where renewid=" & Val(grdhistory.Item("renewid", grdhistory.CurrentRow.Index).Value))
        loadMembershipForEdit()
    End Sub

    Private Sub cmbOrder_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbOrder.SelectedIndexChanged
        txtSeq.Focus()
    End Sub
End Class