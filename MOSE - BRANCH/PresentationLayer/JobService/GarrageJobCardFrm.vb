Public Class GarrageJobCardFrm
    Private chgbyprg As Boolean
    Private _srchTxtId As Integer
    Private _srchOnce As Boolean
    Private MyActiveControl As New Object
    Private lstKey As Integer
    Private activecontrolname As String
    Private isset As Boolean
    Private PSAcc As Long
    Private ischgItm As Boolean
    Private SrchText As String
    Private _srchIndexId As Byte
    Private strGridSrchString As String
    Private chgItm As Boolean
    Private cessdate As Date
    Private chgAmt As Boolean
    Private chgUprice As Boolean
    Private NDec As Integer = 2
    Private FCRt As Double = 1
    Private PPerU As Single
    Private ismodi As Boolean
    Private chcar As Boolean
    Private carid As Long
    Private dtTable As DataTable
    Private dtTableJob As DataTable
    Private rptTable As DataTable
    Private Const ConstInvNo = ConstBatchQty + 1


#Region "NumericText"
    'numeric text
    Private idx As Integer
    Private str1 As String
    Private str2 As String
    Private str3 As String
    Private SelStart As Integer
    Private numCtrl As TextBox
#End Region
#Region "Form Objects"
    Private WithEvents fMList As Mlistfrm
    Private WithEvents fRptFormat As RptFormatfrm
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fDelivery As New JobDelivery
    Private WithEvents fInvoice As JobSalesInvoice
    Private WithEvents fCrtAcc As CreateAccNew
    Private WithEvents fProductEnquiry As ItmEnqry
    Private WithEvents fselectJob As JobEnqryFrm
    Private WithEvents festmate As CustomerQuotation
    Private WithEvents fproductMast As ItemMastFrm
#End Region
#Region "Class Objects"
    Private _objJob As clsJob
    Private _objcmnbLayer As New clsCommon_BL
    Private _objInv As New clsInvoice
    Private _objTr As New clsAccountTransaction
    Private _objvh As New clsVechicle
#End Region
#Region "ComplaintsConstants"
    Private Const ConstComplaints = 1
    Private Const ConstRemark = 2
    Private Const ConstisCompleted = 3
    Private Const ConstComplaintsId = 4
#End Region
    Private Sub GarrageJobCardFrm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtplateno.Focus()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub


    Private Sub txtjobcode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtjobcode.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtEstAmt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtEstAmt.KeyDown
        If e.KeyCode = Keys.Return Then
            dtpestimatedDt.Focus()
        End If
    End Sub

    Private Sub dtpestimatedDt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtpestimatedDt.KeyDown
        If e.KeyCode = Keys.Return Then
            btnupdate.Focus()
        End If
    End Sub

        Private Sub txtcustomer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtcustomer.KeyDown, txttechnician.KeyDown, txtplateno.KeyDown
        lstKey = e.KeyCode
        Dim myctrl As TextBox
        myctrl = sender
        If e.KeyCode = Keys.Return Then
            If myctrl.Name = "txtcustomer" Then
                txttechnician.Focus()
            Else
                    SendKeys.Send("{TAB}")
            End If
            Exit Sub
        End If
        If myctrl.Name = "txtcustomer" Or myctrl.Name = "txttechnician" Or myctrl.Name = "txtplateno" Then
            If e.KeyCode = Keys.Up Or e.KeyCode = Keys.PageUp Then
                If fMList.Visible Then
                    fMList.MoveUp(myctrl.Text)
                    Exit Sub
                End If
            ElseIf e.KeyCode = Keys.Down Or e.KeyCode = Keys.PageDown Then
                If Not fMList Is Nothing Then
                    If fMList.Visible Then
                        fMList.MoveDown(myctrl.Text)
                        Exit Sub
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub AddNew()
        txtjobcode.Text = GenerateNext(txtjobcode.Text)
        txtcustomer.Focus()
    End Sub
    Private Function GenerateNext(ByVal strCode As String) As String
        Dim N As Double
        Dim NewCode As String
        Dim i As Byte
        Dim f As Byte
        Dim tmp As String
        Dim dt As DataTable
        _objcmnbLayer = New clsCommon_BL
        If strCode = "" Then
            dt = _objcmnbLayer._fldDatatable("SELECT top 1 jobcode from GarrageTb order by grid desc")
            If dt.Rows.Count > 0 Then
                strCode = dt(0)(0)
            End If
        End If
        If strCode = "" Then
            strCode = "JB"
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
                dr = _objcmnbLayer._fldDatatable("SELECT jobcode from GarrageTb WHERE jobcode = '" & tmp & "'")
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

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        If btnnew.Text = "New" Then
            If txtplateno.Text = "" Then
                MsgBox("Invalid Car Registration number", MsgBoxStyle.Exclamation)
                txtplateno.Focus()
                Exit Sub
            End If
            btnnew.Text = "Clear"
            AddNew()
            makeClear()
            enableDisableControl(False)
        
        Else
            If btnnew.Text = "Undo" Then
                If MsgBox("You are going to undo the modify! Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Exit Sub
                End If
            End If
            undo()
        End If

    End Sub
    Private Sub enableModify()
        Dim updt As Integer
        If userType Then
            updt = IIf(getRight(182, CurrentUser), 1, 0)
        Else
            updt = 1
        End If

        If updt = 0 Then
            MsgBox("This user do not have permission to Modify", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If lblstatus.Text = "Delivered" Then
            MsgBox("You cannot modify delivered job", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        btnnew.Text = "Undo"
        txtcustomer.Focus()
        btnsearchPlateno.Enabled = False
        enableDisableControl(False)
    End Sub
    Private Sub undo()
        btnnew.Text = "New"
        makeClear()
        enableDisableControl(True)
        txtjobcode.Text = ""
        txtjobcode.Tag = ""
    End Sub
    Private Sub makeClear()
        chgbyprg = True
        txtDescription.Text = ""
        txtEstAmt.Text = Format(0, numFormat)
        If Val(btnnew.Tag) = 0 Then
            txtcustomer.Text = ""
            txtcustomer.Tag = ""
            txtaddress.Text = ""
        End If
        If Val(txtplateno.Tag) = 0 Then btnnew.Tag = 0
        txttechnician.Text = ""
        txtserviceCharge.Text = Format(0, numFormat)
        lblitemvalue.Text = Format(0, numFormat)
        lblJobvalue.Text = Format(0, numFormat)
        lblitmcost.Text = Format(0, numFormat)
        lbljobcost.Text = Format(0, numFormat)
        rdo14.Checked = True
        txtkm.Text = ""
        txtother.Text = ""
        txttotalItemAmt.Text = Format(0, numFormat)
        grdVoucher.Rows.Clear()
        grdcomplaints.Rows.Clear()
        numsto.Text = ""
        numsto.Tag = ""
        txtjobcode.Tag = ""
        lblstatus.Text = ""
        lblstatus.Tag = ""
        txtmat.Text = ""
        txtmudflap.Text = ""
        txtwheelcup.Text= ""
        txtspeekers.Text = ""
        txtscost.Text = Format(0, numFormat)
        If Val(txtplateno.Tag) = 0 Then
            txtcustomer.Text = ""
            txtcustomer.Tag = ""
            txtaddress.Text = ""
        End If
        lblbalance.Text = ""
        fillGrid()
        chgbyprg = False
        ischgItm = False
        Dim i As Integer
        For i = 0 To chAccessories.Items.Count - 1
            chAccessories.SetItemChecked(i, False)
        Next
        btnsearchPlateno.Enabled = True
    End Sub
    Private Sub clearCarDetails()
        chgbyprg = True
        txtplateno.Text = ""
        txtplateno.Tag = ""
        txtmodel.Text = ""
        txtchasisno.Text = ""
        txtengineno.Text = ""
        txtlastKM.Text = ""
        btnsearchPlateno.Text = "Search"
        txtcarname.Text = ""
        txtplateno.Enabled = True
        txtplateno.Focus()
        carid = 0
        txtcustomer.Text = ""
        txtcustomer.Tag = ""
        txtaddress.Text = ""
        chgbyprg = False
        chcar = False
        jobhistory()
    End Sub

    Private Sub txtcustomer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txttechnician.TextChanged, txtcustomer.TextChanged, txtplateno.TextChanged
        If chgbyprg = True Then Exit Sub
        Dim myCtrl As Control = sender
        Select Case myCtrl.Name
            Case "txtcustomer"
                _srchTxtId = 1
            Case "txtplateno"
                _srchTxtId = 2
            Case "txttechnician"
                _srchTxtId = 4
        End Select
        _srchOnce = False
        ShowFmlist(sender)
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
                    Case 2
                        SetFmlist(fMList, 29)
                    Case 3
                        SetFmlist(fMList, 2)
                    Case 4
                        SetFmlist(fMList, 12)
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
            Case 2   'Car name
                fMList.SearchIndex = 0
                fMList.SearchIndexDescr = 1
                fMList_doFocus()
                fMList.Search(txtplateno.Text)
                fMList.AssignList(txtplateno, lstKey, chgbyprg)
            Case 4   'Technician
                fMList.SearchIndex = 0
                fMList.SearchIndexDescr = 1
                fMList_doFocus()
                fMList.Search(txttechnician.Text)
                fMList.AssignList(txttechnician, lstKey, chgbyprg)
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
            Case 2
                txtplateno.Text = ItmFlds(0)
                'txtplateno.Tag = ItmFlds(2)
            Case 4
                txttechnician.Text = ItmFlds(0)
        End Select
        chgbyprg = False
    End Sub

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        QuickCust(True, "Customer")
    End Sub
    Public Sub QuickCust(Optional ByRef bOnlyOne As Boolean = False, Optional ByVal Grp As String = "Customer")
        fCrtAcc = New CreateAccNew
        With fCrtAcc
            .Condition = "GrpSetOn In ('" & Grp & "')"
            If Grp = "Customer" Then
                .iscust = True
            Else
                .iscust = False
            End If
            .bOnlyOne = bOnlyOne
            .ShowDialog()
            txttechnician.Focus()
        End With
    End Sub

    Private Sub fCrtAcc_OpenAccMaster(ByRef AccountNo As Long) Handles fCrtAcc.OpenAccMaster
        loadCustomerDetails(AccountNo)
    End Sub
    Private Sub loadCustomerDetails(ByVal accid As Long)
        Dim dt As DataTable
        chgbyprg = True
        _objcmnbLayer = New clsCommon_BL
        dt = _objcmnbLayer._fldDatatable("SELECT accid,Alias,AccDescr,Address1,Address2,Address3,Address4,Phone,Fax,EMail,PT1,PT2,PT3,PT4," & _
                                         "TrdLcno,TrdDate,ContactName from AccMast LEFT JOIN AccMastAddr ON AccMast.ACCID=AccMastAddr.accountno " & _
                                         IIf(accid > 0, "where accid=" & accid, "where AccDescr='" & txtcustomer.Text & "'"))
        If dt.Rows.Count > 0 Then
            txtcustomer.Tag = dt(0)("accid")
            txtcustomer.Text = dt(0)("AccDescr")
            txtaddress.Text = dt(0)("Address1") & vbCrLf & dt(0)("Address2") & vbCrLf & dt(0)("Address3") & vbCrLf & dt(0)("Phone") & vbCrLf & dt(0)("ContactName")
            Dim iNBal As Double = getAccBal(Val(txtcustomer.Tag))
            lblbalance.Text = "Outstanding : " & Strings.Format(iNBal, numFormat)
        Else
            txtcustomer.Text = ""
        End If
        chgbyprg = False
        txttechnician.Focus()
    End Sub
    Private Sub enableDisableControl(ByVal EnDs As Boolean)
        For Each Control In gbjob.Controls
            If TypeOf (Control) Is TextBox Then
                Control.readonly = EnDs
            End If
            If TypeOf (Control) Is DateTimePicker Then
                Control.enabled = Not EnDs
            End If
        Next
        txtaddress.ReadOnly = True
        txtEstAmt.ReadOnly = EnDs
        txtother.ReadOnly = EnDs
        Panel1.Enabled = Not EnDs
        txtkm.ReadOnly = EnDs
        chAccessories.Enabled = Not EnDs
        dtpestimatedDt.Enabled = Not EnDs
        txttechnician.ReadOnly = EnDs
        txtDescription.ReadOnly = EnDs
        'txtserviceCharge.ReadOnly = EnDs
        txtscost.ReadOnly = EnDs
        btnaddcomplaints.Enabled = Not EnDs
        btnrem.Enabled = Not EnDs
        btndelete.Enabled = Not EnDs
        btnjobdelivery.Enabled = Not EnDs
        btnclosejob.Visible = False
        gbinvoice.Enabled = Not EnDs
        btnupdate.Enabled = Not EnDs
        txtmat.ReadOnly = EnDs
        txtmudflap.ReadOnly = EnDs
        txtwheelcup.ReadOnly = EnDs
        txtspeekers.ReadOnly = EnDs
    End Sub

    Private Sub TabControl3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl3.Click
        If btnnew.Text = "New" Then
            MsgBox("Invalid Job", MsgBoxStyle.Exclamation)
            TabControl3.SelectedIndex = 0
            Exit Sub
        End If
    End Sub

    Private Sub GarrageJobCardFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        lblbalance.Text = ""
        enableDisableControl(True)
        setComplaintsGrid()
        Timer1.Enabled = True
        If userType Then
            btnupdate.Tag = IIf(getRight(181, CurrentUser), 1, 0)
            btndelete.Tag = IIf(getRight(182, CurrentUser), 1, 0)
        Else
            btnupdate.Tag = 1
            btndelete.Tag = 1
        End If
        loadCarDetails()
        loadJoblist()
        SetGridHead()

    End Sub
    Private Sub SetGridHead()
        chgbyprg = True
        With grdVoucher


            SetGridHeadEntryProperty(grdVoucher)
            .Columns(ConstInvNo).HeaderText = "Inv No"
            .Columns(ConstInvNo).Width = 100
            .Columns(ConstInvNo).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstInvNo).ReadOnly = True

            .Columns(Constsman).Visible = False
            .Columns(ConstMeterCode).Visible = False
            .Columns(ConstStartReading).Visible = False
            .Columns(ConstEndReading).Visible = False
            .Columns(ConstSerialNo).Visible = False
            .Columns(ConstWarrentyExpiry).Visible = False
        End With
        chgbyprg = False
    End Sub

    Private Sub txtcustomer_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcustomer.Validated
        If txtcustomer.Text = "" Then
            If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
            Exit Sub
        End If
        loadCustomerDetails(0)
        chgbyprg = False
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
    End Sub
    Private Sub jobhistory()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("select jobcode [Job Code],jobdate [Date],SManName [Technician],grid from GarrageTb " & _
                                         "left join CarMasterTb on CarMasterTb.carid=GarrageTb.carid " & _
                                         "left join SalesmanTb on SalesmanTb.salesmanid=GarrageTb.technician " & _
                                         "where platenumber='" & txtplateno.Text & "' order by jobdate desc")
        dgvjobhistory.DataSource = dt
        SetGridProperty(dgvjobhistory)
        With dgvjobhistory
            .Columns("Job Code").Width = 80
            .Columns("Date").Width = 75
            .Columns(.Columns.Count - 1).Visible = False
        End With
        resizeGridColumn(dgvjobhistory, 2)
    End Sub
    Private Sub txttechnician_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txttechnician.Validated
        If chgbyprg Then Exit Sub
        If txttechnician.Text = "" Then
            If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
            Exit Sub
        End If
        Dim dt As DataTable
        _objcmnbLayer = New clsCommon_BL
        dt = _objcmnbLayer._fldDatatable("SELECT SManCode,SManName from SalesmanTb  where SManName='" & txttechnician.Text & "'")
        chgbyprg = True
        If dt.Rows.Count > 0 Then
            txttechnician.Tag = dt(0)("SManCode")
            txttechnician.Text = dt(0)("SManName")
        Else
            txttechnician.Text = ""
        End If
        chgbyprg = False
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
    End Sub
    Private Sub setComplaintsGrid()
        With grdcomplaints
            SetEntryGridProperty(grdcomplaints)

            .ColumnHeadersHeight = 100
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersDefaultCellStyle.BackColor = Color.Olive
            .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.5!, System.Drawing.FontStyle.Regular)
            .RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.0!)
            .ColumnCount = 5

            .Columns(ConstSlNo).HeaderText = "SlNo"
            .Columns(ConstSlNo).Width = 40
            .Columns(ConstSlNo).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstSlNo).Frozen = True
            .Columns(ConstSlNo).DefaultCellStyle.Format = "N0"
            .Columns(ConstSlNo).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstSlNo).ReadOnly = True

            .Columns(ConstComplaints).HeaderText = "Customer Complaints"
            .Columns(ConstComplaints).Width = 220
            .Columns(ConstComplaints).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstComplaints).ReadOnly = False
            .Columns(ConstComplaints).Frozen = True

            .Columns(ConstRemark).HeaderText = "Remark"
            .Columns(ConstRemark).Width = 150
            .Columns(ConstRemark).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstRemark).ReadOnly = False

            .Columns(ConstisCompleted).HeaderText = "Is Completed"
            .Columns(ConstisCompleted).Width = 100
            .Columns(ConstisCompleted).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstisCompleted).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(ConstisCompleted).ReadOnly = True
            .Columns(ConstComplaintsId).Visible = False
            isset = True
        End With
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        resizeGridColumn(grdcomplaints, ConstComplaints)
        'resizeGridColumn(grdVoucher, ConstDescr)
    End Sub
    Private Sub addComplaints()
        Dim i As Integer
        If Not btnaddcomplaints.Enabled Then Exit Sub
        With grdcomplaints
            activecontrolname = "grdcomplaints"
            i = .RowCount '- 1
            .Rows.Add(1)
            .Item(ConstSlNo, i).Value = i + 1
            .Item(ConstComplaints, i).Value = ""
            .Item(ConstRemark, i).Value = ""
            .CurrentCell = .Item(1, i)
            chgbyprg = True
            .BeginEdit(True)
            chgbyprg = False
        End With
    End Sub

    Private Sub btnaddcomplaints_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddcomplaints.Click
        addComplaints()
    End Sub
    Private Sub RemoveRowComplaints()
        If Not btnrem.Enabled Then Exit Sub
        If grdcomplaints.RowCount > 0 Then
            If MsgBox("You Are Going to Remove the Row." & vbCrLf & "Are You Sure ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then
                Exit Sub
            End If
            With grdcomplaints
                .Rows.RemoveAt(.CurrentRow.Index)
                .ClearSelection()
            End With
        End If
        rearrangeComplaintsNo()
    End Sub
    Private Sub rearrangeComplaintsNo()
        Dim i As Integer
        For i = 0 To grdcomplaints.RowCount - 1
            grdcomplaints.Item(ConstSlNo, i).Value = i + 1
        Next
    End Sub

    Private Sub btnremove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnremove.Click
        RemoveRowComplaints()
    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            Const WM_KEYDOWN As Integer = &H100
            Const WM_KEYUP As Integer = &H101

            'msg.ToString()
            If msg.Msg = WM_KEYDOWN Or msg.Msg = WM_KEYUP Then
                If TabControl3.SelectedIndex = 0 And TabControl1.SelectedIndex = 0 And msg.WParam.ToInt32() = CInt(Keys.F3) Then
                    activecontrolname = "grdcomplaints"
                End If
                If TabControl1.SelectedIndex = 0 And msg.WParam.ToInt32() = CInt(Keys.F1) Then
                    activecontrolname = ""
                    If btnnew.Text = "New" Then
                        MsgBox("Invalid Job", MsgBoxStyle.Exclamation)
                        TabControl3.SelectedIndex = 0
                        Exit Function
                    End If
                    If TabControl3.SelectedIndex = 0 Then
                        TabControl3.SelectedIndex = 1
                        txtkm.Focus()
                    ElseIf TabControl3.SelectedIndex = 1 Then
                        TabControl3.SelectedIndex = 2
                        txtEstAmt.Focus()
                    Else
                        TabControl3.SelectedIndex = 0
                    End If
                End If
                If msg.WParam.ToInt32() = CInt(Keys.F8) Then
                    updateClick()
                ElseIf activecontrolname = "grdcomplaints" Then
                    If msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up) Or msg.WParam.ToInt32() = CInt(Keys.Enter) Or msg.WParam.ToInt32() = CInt(Keys.F2) Or msg.WParam.ToInt32() = CInt(Keys.F3) Or msg.WParam.ToInt32() = CInt(Keys.F4) Then
                        grdcomplaints_KeyDown(Nothing, New KeyEventArgs(keyData))
                        Return True
                    End If
                ElseIf activecontrolname = "grdVoucher" Then
                    If msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up) Or msg.WParam.ToInt32() = CInt(Keys.Enter) Or msg.WParam.ToInt32() = CInt(Keys.F2) Or msg.WParam.ToInt32() = CInt(Keys.F3) Or msg.WParam.ToInt32() = CInt(Keys.F4) Or msg.WParam.ToInt32() = CInt(Keys.F6) Then
                        If (msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up)) And plsrch.Visible = False Then GoTo ctn
                        grdVoucher_KeyDown(Nothing, New KeyEventArgs(keyData))
                        Return True
                    End If
                End If
            End If
ctn:
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub grdcomplaints_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdcomplaints.CellClick
        If Not btnupdate.Enabled Then Exit Sub
        If e.ColumnIndex = 3 Then
            grdcomplaints.Item(e.ColumnIndex, e.RowIndex).Value = IIf(grdcomplaints.Item(e.ColumnIndex, e.RowIndex).Value = "Y", "", "Y")
        End If
        chgbyprg = True
        grdcomplaints.BeginEdit(True)
        chgbyprg = False
    End Sub

    Private Sub grdcomplaints_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdcomplaints.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If FindNextCell(grdcomplaints, grdcomplaints.CurrentCell.RowIndex, grdcomplaints.CurrentCell.ColumnIndex + 1) Then
                    addComplaints()
                End If
                chgbyprg = True
                grdcomplaints.BeginEdit(True)
                chgbyprg = False
            ElseIf e.KeyCode = Keys.F3 Then
                addComplaints()
            ElseIf e.KeyCode = Keys.F4 Then
                If grdcomplaints.RowCount = 0 Then Exit Sub
                RemoveRowComplaints()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub grdVoucher_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellClick
        chgbyprg = True
        With grdVoucher
            If Val(grdVoucher.Item(0, grdVoucher.CurrentCell.RowIndex).Value) = 0 And e.ColumnIndex <> 3 Then
                grdVoucher.CurrentCell.ReadOnly = True
            ElseIf Trim(grdVoucher.Item(ConstInvNo, grdVoucher.CurrentCell.RowIndex).Value) <> "" Then
                grdVoucher.CurrentCell.ReadOnly = True
            ElseIf e.ColumnIndex <> ConstSlNo And e.ColumnIndex <> constItmTot And e.ColumnIndex <> ConstUnit And e.ColumnIndex <> ConstLTotal And e.ColumnIndex <> ConstBarcode And e.ColumnIndex <> ConstTaxAmt And e.ColumnIndex <> ConstStartReading And e.ColumnIndex <> ConstcessAmt Then
                If e.ColumnIndex = ConstTaxP And EnableGST Then
                    grdVoucher.CurrentCell.ReadOnly = True
                ElseIf e.ColumnIndex = ConstQty And grdVoucher.Item(ConstMeterCode, grdVoucher.CurrentCell.RowIndex).Value <> "" Then
                    grdVoucher.CurrentCell.ReadOnly = True
                ElseIf e.ColumnIndex = ConstQty And enableWoodSale Then
                    grdVoucher.CurrentCell.ReadOnly = True
                ElseIf e.ColumnIndex = ConstWarrentyExpiry And enableBatchwiseTr Then
                    grdVoucher.CurrentCell.ReadOnly = True
                ElseIf e.ColumnIndex = ConstManufacturingdate Then
                    grdVoucher.CurrentCell.ReadOnly = True
                ElseIf e.ColumnIndex = ConstInvNo Then
                    grdVoucher.CurrentCell.ReadOnly = True
                Else
                    grdVoucher.CurrentCell.ReadOnly = False
                End If
            ElseIf e.ColumnIndex = ConstSlNo And enableBatchwiseTr Then
                grdVoucher.CurrentCell.ReadOnly = False
            Else
                grdVoucher.CurrentCell.ReadOnly = True
            End If
        End With
        grdBeginEdit()
    End Sub
    Private Sub grdBeginEdit()
        chgbyprg = True
        grdVoucher.BeginEdit(True)
        chgbyprg = False
    End Sub
    Private Sub reArrangeNo()
        Dim r As Integer
        Dim i As Integer
        chgbyprg = True
        i = 0
        With grdVoucher
            For r = 0 To .Rows.Count - 1 '- 1
                If CStr(.Item(ConstSlNo, r).Value) <> "M" And CStr(.Item(ConstSlNo, r).Value) <> "L" Then
                    i = i + 1
                    .Item(ConstSlNo, r).Value = i
                End If
            Next r
        End With
        chgbyprg = False
    End Sub
    Private Sub grdVoucher_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellDoubleClick
        Dim dt As DataTable = Nothing
        With grdVoucher
            If e.ColumnIndex = ConstSlNo Then
                .Item(ConstSlNo, .CurrentCell.RowIndex).Value = IIf(Val(.Item(ConstSlNo, .CurrentCell.RowIndex).Value) = 0, "", "M")
                reArrangeNo()
            ElseIf e.ColumnIndex = ConstUnit Then
                If .Item(ConstB, .CurrentCell.RowIndex).Value = "B" Then
                    .Item(ConstB, .CurrentCell.RowIndex).Value = "P1"
                    dt = _objcmnbLayer._fldDatatable("SELECT P1Unit U,P1Fra F,LastPurchCost FROM InvItm WHERE Itemid=" & Val(.Item(ConstItemID, .CurrentCell.RowIndex).Value))
                ElseIf .Item(ConstB, .CurrentCell.RowIndex).Value = "P1" Then
                    .Item(ConstB, .CurrentCell.RowIndex).Value = "P2"
                    dt = _objcmnbLayer._fldDatatable("SELECT P2Unit U,P2Fra F,LastPurchCost FROM InvItm WHERE Itemid=" & Val(.Item(ConstItemID, .CurrentCell.RowIndex).Value))
                ElseIf .Item(ConstB, .CurrentCell.RowIndex).Value = "P2" Then
u:
                    .Item(ConstB, .CurrentCell.RowIndex).Value = "B"
                    dt = _objcmnbLayer._fldDatatable("SELECT Unit U,1 F,LastPurchCost FROM InvItm WHERE Itemid=" & Val(.Item(ConstItemID, .CurrentCell.RowIndex).Value))
                End If
                If dt Is Nothing Then GoTo u
                If dt.Rows.Count > 0 Then
                    .Item(ConstUnit, .CurrentCell.RowIndex).Value = dt(0)("U")
                    .Item(ConstPMult, .CurrentCell.RowIndex).Value = dt(0)("F")
                    Dim cost As Double
                    cost = getPurchAmt(dt(0)("LastPurchCost"), 0, Val(.Item(ConstItemID, .CurrentCell.RowIndex).Value))
                    If Val(.Item(ConstPMult, .CurrentCell.RowIndex).Value & "") = 0 Then
                        .Item(ConstPMult, .CurrentCell.RowIndex).Value = 0
                    End If
                    cost = cost * Val(.Item(ConstPMult, .CurrentCell.RowIndex).Value)
                    .Item(ConstActualPrice, .CurrentCell.RowIndex).Value = cost
                    .Item(ConstUPrice, .CurrentCell.RowIndex).Value = Format(cost, numFormat)
                    calculate(, True)
                End If

            End If
        End With
    End Sub

    Private Sub grdVoucher_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellEndEdit
        Dim col As Integer = e.ColumnIndex
        Dim ndec1 As Integer
        chgbyprg = True
        If col = ConstQty Or col = ConstUPrice Or col = ConstDisAmt Or col = ConstLTotal Or col = ConstTaxP Or col = ConstDisP Or col = ConstEndReading Or col = ConstWoodDiscQty Or col = ConstWoodQty Then
            If col = ConstQty Or col = ConstEndReading Or col = ConstWoodDiscQty Or col = ConstWoodQty Then
                ndec1 = Val(grdVoucher.Item(ConstPFraction, grdVoucher.CurrentRow.Index).Value & "")
            Else
                ndec1 = NoOfDecimal
            End If
            If Val(grdVoucher.Item(col, e.RowIndex).Value) = 0 Then grdVoucher.Item(col, e.RowIndex).Value = 0
            grdVoucher.Item(col, e.RowIndex).Value = Format(Convert.ToDecimal(grdVoucher.Item(col, e.RowIndex).Value), "#,##0" & IIf(ndec1 = 0, "", "." & Strings.StrDup(ndec1, "0")))
        End If
        chgbyprg = False
    End Sub

    Private Sub grdVoucher_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellValidated
        Try
            Valid(e.RowIndex, e.ColumnIndex)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub grdVoucher_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellValueChanged
        If chgbyprg Then Exit Sub
        'ChgByPrg = True
        With grdVoucher
            Dim i As Integer = e.RowIndex
            'btnUpdate.Enabled = True
            Select Case e.ColumnIndex
                Case ConstItemCode, ConstBarcode
                    chgItm = True
                Case ConstQty, ConstTaxP, ConstDisAmt, ConstDisP, ConstWoodDiscQty, ConstWoodQty
                    chgAmt = True
                Case ConstLTotal
                    If Val(grdVoucher.Item(ConstQty, i).Value) > 0 Then
                        If Val(grdVoucher.Item(ConstDisAmt, i).Value) = 0 And CDbl(grdVoucher.Item(ConstTaxAmt, i).Value) = 0 Then 'Not AllowUnitDiscountEntryOnInventory And Not ShowTaxOnInventory And
                            chgbyprg = True
                            grdVoucher.Item(ConstUPrice, i).Value = Format(CDbl(grdVoucher.Item(ConstLTotal, i).Value) / grdVoucher.Item(ConstQty, i).Value, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) 'IIf(IsReturn, -1, 1)
                            grdVoucher.Item(ConstActualPrice, i).Value = CDbl(grdVoucher.Item(ConstLTotal, i).Value) / grdVoucher.Item(ConstQty, i).Value
                            calculate(, True)
                            chgbyprg = False
                        End If
                    End If
                    chgAmt = True
                Case ConstDisAmt
                    chgAmt = True
                Case ConstUPrice
                    chgUprice = True
                    chgAmt = True
                    calculateTaxFromUnitPrice(e.RowIndex)
            End Select
        End With
    End Sub

    Private Sub grdVoucher_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdVoucher.EditingControlShowing
        Dim Col As Integer
        Col = grdVoucher.CurrentCell.ColumnIndex
        If Col = ConstItemCode Or Col = ConstQty Then
            Dim tb As TextBox = CType(e.Control, TextBox)
            RemoveHandler tb.TextChanged, AddressOf Textbox_TextChanged
            AddHandler tb.TextChanged, AddressOf Textbox_TextChanged
        End If
        If Col = ConstItemCode Or Col = ConstSerialNo Or Col = ConstQty Then
            Dim tb As TextBox = CType(e.Control, TextBox)
            RemoveHandler tb.KeyPress, AddressOf Textbox_KeyPress
            AddHandler tb.KeyPress, AddressOf Textbox_KeyPress
        End If
    End Sub

    Private Sub grdVoucher_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.GotFocus
        activecontrolname = "grdVoucher"
    End Sub

    Private Sub grdVoucher_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdVoucher.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If grdVoucher.RowCount = 0 Then Exit Sub

                If SrchText = "" And grdVoucher.CurrentCell.ColumnIndex = ConstItemCode Then
                    SrchText = grdVoucher.Item(ConstItemCode, grdVoucher.CurrentRow.Index).Value
                    If SrchText = "" Then GoTo nxt
                End If
                diableColums()
                If FindNextCell(grdVoucher, grdVoucher.CurrentCell.RowIndex, grdVoucher.CurrentCell.ColumnIndex + 1) Then
                    AddRow()
                End If
                plsrch.Visible = False
nxt:
                chgbyprg = True
                grdBeginEdit()
                chgbyprg = False

            ElseIf e.KeyCode = Keys.Up Or e.KeyCode = Keys.PageUp Then
                If grdVoucher.RowCount = 0 Then Exit Sub
                If plsrch.Visible Then
                    doSelect(0)
                    Exit Sub
                End If
            ElseIf e.KeyCode = Keys.Down Or e.KeyCode = Keys.PageDown Then
                If grdVoucher.RowCount = 0 Then Exit Sub
                If plsrch.Visible Then
                    doSelect(1)
                    Exit Sub
                End If
            ElseIf e.KeyCode = Keys.F1 Then
                grdBeginEdit()
            ElseIf e.KeyCode = Keys.F2 Then
                If plsrch.Visible Then plsrch.Visible = False
                If grdVoucher.RowCount = 0 Then Exit Sub
                Select Case grdVoucher.CurrentCell.ColumnIndex
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
            ElseIf e.KeyCode = Keys.F6 Then
                'modifyItem(Val(grdVoucher.Item(ConstItemID, grdVoucher.CurrentRow.Index).Value))
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub grdcomplaints_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdcomplaints.GotFocus
        activecontrolname = "grdcomplaints"
    End Sub

    Private Sub grdcomplaints_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdcomplaints.Leave
        activecontrolname = ""
    End Sub

    Private Sub btnsearchPlateno_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsearchPlateno.Click
        If btnsearchPlateno.Text = "Clear" Then
            If Val(txtjobcode.Tag) > 0 Then
                If MsgBox("Job opened! Do you want clear data?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                undo()
            End If
            clearCarDetails()
        End If
    End Sub
    Private Sub txtplateno_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtplateno.Validated
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
        searchCar()


    End Sub
    Private Sub searchCar()
        chgbyprg = True
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("select platenumber,cartype,regyear,chaisenumber,engineNo,CarMasterTb.carid,isnull(lastKm,0) lastKm,customerid from " & _
                                         "CarMasterTb LEFT Join (select max(kilometer) lastKm,carid from GarrageTb group by carid)km on CarMasterTb.carid=km.carid " & _
                                         "where platenumber='" & txtplateno.Text & "'")
        If dt.Rows.Count > 0 Then
            txtplateno.Text = Trim(dt(0)("platenumber") & "")
            txtplateno.Tag = dt(0)("carid")
            txtcarname.Text = Trim(dt(0)("cartype") & "")
            txtmodel.Text = Trim(dt(0)("regyear") & "")
            txtchasisno.Text = Trim(dt(0)("chaisenumber") & "")
            txtengineno.Text = Trim(dt(0)("engineNo") & "")
            txtlastKM.Text = Trim(dt(0)("lastKm") & "")
            loadCustomerDetails(dt(0)("customerid"))
            btnnew.Tag = 1
            jobhistory()
            btnsearchPlateno.Text = "Clear"
            txtplateno.Enabled = False
            btneditplatenumber.Enabled = True
        Else
            If btneditplatenumber.Enabled = False And carid > 0 Then
                txtplateno.Tag = carid
                carid = 0
                _objcmnbLayer._saveDatawithOutParm("update CarMasterTb set platenumber='" & txtplateno.Text & "' where carid=" & Val(txtplateno.Tag))
                txtplateno.Enabled = False
                btneditplatenumber.Enabled = True
                GoTo ext
            End If
            txtplateno.Enabled = True
            txtplateno.Tag = ""
            txtlastKM.Text = ""
            txtcarname.Text = ""
            txtmodel.Text = ""
            txtchasisno.Text = ""
            txtengineno.Text = ""
            txtlastKM.Text = ""
            btneditplatenumber.Enabled = False
        End If
ext:
        chgbyprg = False
    End Sub

    Private Sub txtcarname_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtcarname.KeyDown, txtmodel.KeyDown, txtchasisno.KeyDown, txtengineno.KeyDown
        Dim myctrl As TextBox
        myctrl = sender
        If e.KeyCode = Keys.Return Then
            If myctrl.Name = "txtengineno" Then
                btnnew.Focus()
            Else
                SendKeys.Send("{TAB}")
            End If
        End If
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        updateClick()
    End Sub
    Private Sub updateClick()
        If Val(btnupdate.Tag) = 0 Then
            MsgBox("This user do not have permission to Update", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        verify()
    End Sub
    Private Sub verify()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select grid from GarrageTb where jobcode ='" & txtjobcode.Text & "' and grid<>" & Val(txtjobcode.Tag))
        If dt.Rows.Count > 0 Then
            MsgBox("Job code already exist", MsgBoxStyle.Exclamation)
            txtjobcode.Focus()
            Exit Sub
        End If
        If Val(txtcustomer.Tag) = 0 Then
            MsgBox("Invalid Customer", MsgBoxStyle.Exclamation)
            txtcustomer.Focus()
            Exit Sub
        End If
        If txtplateno.Text = "" Then
            MsgBox("Invalid Car Registration Number", MsgBoxStyle.Exclamation)
            txtplateno.Focus()
            Exit Sub
        End If
        If MsgBox("Verification is succeeded. Do you like to file it?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        If chcar Then saveCar()
        saveJob()
        searchCar()
        txtprintjob.Text = txtjobcode.Text
        MsgBox("Job Card saved successfully", MsgBoxStyle.Information)
        makeClear()
        txtjobcode.Text = ""
        txtjobcode.Tag = ""
        enableDisableControl(True)
        btnnew.Text = "New"
        txtplateno.Focus()
        chcar = False
    End Sub
    Private Sub saveCar()
        With _objvh
            .carid = Val(txtplateno.Tag)
            .platenumber = txtplateno.Text
            .cartype = txtcarname.Text
            .regyear = txtmodel.Text
            .chassisnumber = txtchasisno.Text
            .engineNo = txtengineno.Text
            .customerid = Val(txtcustomer.Tag)
            txtplateno.Tag = .savecarmaster()
        End With

    End Sub
    Private Sub saveJob()
        _objJob = New clsJob
        With _objJob
            .Jobid = Val(txtjobcode.Tag)
            .jobcode = txtjobcode.Text
            .jobdate = DateValue(dtpdate.Value)
            .JobDescription = txtDescription.Text
            .custcode = Val(txtcustomer.Tag)
            .EstimatedDate = dtpestimatedDt.Value
            If Val(txtEstAmt.Text) = 0 Then txtEstAmt.Text = 0
            .EstimatedAmt = 0 ' CDbl(txtEstAmt.Text)
            .Technician = txttechnician.Tag
            If Val(txtserviceCharge.Text) = 0 Then txtserviceCharge.Text = 0
            .ServiceCost = CDbl(txtserviceCharge.Text)
            If Val(lblitemvalue.Text) = 0 Then lblitemvalue.Text = 0
            .Userid = CurrentUser
            If Val(txtscost.Text) = 0 Then txtscost.Text = 0
            .LabourCost = CDbl(txtscost.Text)
            Dim fuelStatus As Integer
            If rdo14.Checked Then
                fuelStatus = 0
            ElseIf rdo12.Checked Then
                fuelStatus = 1
            ElseIf rdo34.Checked Then
                fuelStatus = 2
            Else
                fuelStatus = 3
            End If
            .fuelStatus = fuelStatus
            .kilometer = txtkm.Text
            .accessoriesRemark = txtother.Text
            .carid = Val(txtplateno.Tag)
            .noofMats = Val(txtmat.Text)
            .noofMudFlap = Val(txtmudflap.Text)
            .noofwheelcap = Val(txtwheelcup.Text)
            .noofSpeekers = Val(txtspeekers.Text)
            txtjobcode.Tag = .saveGarrageJob()
        End With
        Dim i As Integer
        With grdcomplaints
            _objcmnbLayer._saveDatawithOutParm("update GarrageDemandedRepairTb set setremove=1 where  grid=" & Val(txtjobcode.Tag))
            For i = 0 To .RowCount - 1
                _objJob.repairsId = Val(.Item(ConstComplaintsId, i).Value)
                _objJob.Jobid = Val(txtjobcode.Tag)
                _objJob.complaints = .Item(ConstComplaints, i).Value
                _objJob.instrucations = .Item(ConstRemark, i).Value
                _objJob.completed = IIf(.Item(ConstisCompleted, i).Value = "Y", 1, 0)
                _objJob.saveGarrageDemandedRepairTb()
            Next
            _objcmnbLayer._saveDatawithOutParm("delete from GarrageDemandedRepairTb  where setremove=1 and grid=" & Val(txtjobcode.Tag))
        End With
        saveAccessories()
    End Sub
    Public Sub saveAccessories()
        Dim i As Integer
        Dim servicebook As Integer
        Dim sparewheel As Integer
        Dim jackandHandle As Integer
        Dim toolkit As Integer
        Dim mats As Integer
        Dim mudflap As Integer
        Dim wheelcups As Integer
        Dim stereo As Integer
        Dim freshner As Integer
        Dim speaker As Integer
        Dim clock As Integer
        Dim cassetsandcds As Integer
        Dim mirror As Integer
        Dim battery As Integer
        Dim tyre As Integer
        Dim stepneytyre As Integer
        Dim seatcover As Integer

        If chAccessories.GetItemChecked(0) = True Then
            servicebook = 1
        End If
        If chAccessories.GetItemChecked(1) = True Then
            sparewheel = 1
        End If
        If chAccessories.GetItemChecked(2) = True Then
            jackandHandle = 1
        End If
        If chAccessories.GetItemChecked(3) = True Then
            toolkit = 1
        End If
        If chAccessories.GetItemChecked(4) = True Then
            mats = 1
        End If
        If chAccessories.GetItemChecked(5) = True Then
            mudflap = 1
        End If
        If chAccessories.GetItemChecked(6) = True Then
            wheelcups = 1
        End If
        If chAccessories.GetItemChecked(7) = True Then
            stereo = 1
        End If

        If chAccessories.GetItemChecked(8) = True Then
            freshner = 1
        End If
        If chAccessories.GetItemChecked(9) = True Then
            speaker = 1
        End If
        If chAccessories.GetItemChecked(10) = True Then
            clock = 1
        End If
        If chAccessories.GetItemChecked(11) = True Then
            cassetsandcds = 1
        End If
        If chAccessories.GetItemChecked(12) = True Then
            mirror = 1
        End If
        If chAccessories.GetItemChecked(13) = True Then
            battery = 1
        End If
        If chAccessories.GetItemChecked(14) = True Then
            tyre = 1
        End If
        If chAccessories.GetItemChecked(15) = True Then
            stepneytyre = 1
        End If
        If chAccessories.GetItemChecked(16) = True Then
            seatcover = 1
        End If
        _objcmnbLayer._saveDatawithOutParm("delete from GarrageAccessoriesTb where grdid=" & Val(txtjobcode.Tag))
        _objcmnbLayer._saveDatawithOutParm("Insert into GarrageAccessoriesTb values (" & Val(txtjobcode.Tag) & "," & _
                                               servicebook & "," & _
                                               sparewheel & "," & _
                                               jackandHandle & "," & _
                                               toolkit & "," & _
                                               mats & "," & _
                                               mudflap & "," & _
                                               wheelcups & "," & _
                                               stereo & "," & _
                                               freshner & "," & _
                                               speaker & "," & _
                                               clock & "," & _
                                               cassetsandcds & "," & _
                                               mirror & "," & _
                                               battery & "," & _
                                               tyre & "," & _
                                               stepneytyre & "," & _
                                               seatcover & _
                                               ")")

    End Sub
    Private Sub ldRec()
        Dim dt As DataTable
        'dt = _objcmnbLayer._fldDatatable("select GarrageTb.*,SManCode,SManName,invno,trid  from GarrageTb " & _
        '                                 "left join SalesmanTb on SalesmanTb.salesmanid=GarrageTb.technician " & _
        '                                 "left join (select invno,[Job Code],trid from ItmInvCmnTb where trtype='STO') tr on tr.[Job Code]=GarrageTb.jobcode " & _
        '                                 "left join (select sum((TrQty*(UnitCost-UnitDiscount))taxAmt) serviceAmt from JobInvTrTb left join invitm where itemcategory='Service') tr on tr.[Job Code]=GarrageTb.jobcode " & _
        '                                 "where grid=" & Val(txtjobcode.Tag))
        _objJob = New clsJob
        chgbyprg = True
        dt = _objJob.returnGarrageJob(Val(txtjobcode.Tag))
        If dt.Rows.Count > 0 Then
            txtjobcode.Text = dt(0)("jobcode")
            txtprintjob.Text = dt(0)("jobcode")
            dtpdate.Value = Format(dt(0)("jobdate"), DtFormat)
            loadCustomerDetails(dt(0)("customerid"))
            chgbyprg = True
            txttechnician.Text = Trim(dt(0)("SManName") & "")
            txttechnician.Tag = Trim(dt(0)("SManCode") & "")
            txtDescription.Text = Trim(dt(0)("remarks") & "")
            txtmat.Text = Trim(dt(0)("noofMats") & "")
            txtmudflap.Text = Trim(dt(0)("noofMudFlap") & "")
            txtwheelcup.Text = Trim(dt(0)("noofwheelcap") & "")
            txtspeekers.Text = Trim(dt(0)("noofSpeekers") & "")
            If Not IsDBNull(dt(0)("EstimatedDate")) Then
                dtpestimatedDt.Value = dt(0)("EstimatedDate")
            End If
            txtEstAmt.Text = Format(Val(dt(0)("estimateAmt") & ""), numFormat)
            lbldelivery.Text = "Nil"
            lbldeliveredby.Text = "Delivered By : "
            lblreceivedby.Text = "Received By : "
            btnjobdelivery.Text = "Delivery"
            If Val(dt(0)("Status") & "") < 2 Then
                lblstatus.Text = "Active"
                btnclosejob.Visible = True
            Else
                lblstatus.Text = "Delivered"
                btnjobdelivery.Text = "Undo Delivery"
                If Not IsDBNull(dt(0)("deliverydate")) Then
                    lbldelivery.Text = dt(0)("deliverydate")
                Else
                    lbldelivery.Text = "Nil"
                End If
                lbldeliveredby.Text = "Delivered By : " & Trim(dt(0)("deliveredBy") & "")
                lblreceivedby.Text = "Received By : " & Trim(dt(0)("receivedBy") & "")
                btnclosejob.Visible = False
            End If
            lblstatus.Tag = Val(dt(0)("Status") & "")
            txtscost.Text = Format(Val(dt(0)("ServiceCost") & ""), numFormat)
            txtserviceCharge.Text = Format(Val(dt(0)("serviceAmt") & ""), numFormat)
            lblitmcost.Text = Format(Val(dt(0)("totalCost") & ""), numFormat)
            
            If Val(dt(0)("fuelStatus")) = 0 Then
                rdo14.Checked = True
            ElseIf Val(dt(0)("fuelStatus")) = 1 Then
                rdo12.Checked = True
            ElseIf Val(dt(0)("fuelStatus")) = 2 Then
                rdo34.Checked = True
            Else
                rdofull.Checked = True
            End If
            txtkm.Text = Trim(dt(0)("kilometer") & "")
            txtother.Text = Trim(dt(0)("accessoriesRemark") & "")
            numsto.Text = Val(dt(0)("invno") & "")
            numsto.Tag = Val(dt(0)("trid") & "")
        Else
            MsgBox("Invalid Job", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If Val(txtjobcode.Tag) > 0 Then
            dt = _objcmnbLayer._fldDatatable("Select * from GarrageDemandedRepairTb where grid=" & Val(txtjobcode.Tag))
            Dim i As Integer
            grdcomplaints.Rows.Clear()
            For i = 0 To dt.Rows.Count - 1
                With grdcomplaints
                    .Rows.Add()
                    .Item(ConstSlNo, i).Value = i + 1
                    .Item(ConstComplaints, i).Value = dt(i)("complaints")
                    .Item(ConstRemark, i).Value = dt(i)("instrucations")
                    .Item(ConstisCompleted, i).Value = IIf(Val(dt(i)("completed")) = 0, "", "Y")
                    .Item(ConstComplaintsId, i).Value = dt(i)("repairsId")
                End With
            Next
            dt = _objcmnbLayer._fldDatatable("Select * from GarrageAccessoriesTb where grdid=" & Val(txtjobcode.Tag))
            If dt.Rows.Count > 0 Then
                For i = 1 To dt.Columns.Count - 1
                    chAccessories.SetItemChecked(i - 1, False)
                    If Val(dt(0)(i) & "") = 1 Then
                        chAccessories.SetItemChecked(i - 1, True)
                    End If
                Next
            End If

        End If
        loadItems(Val(numsto.Tag))
        loadEstimates()
        fillGrid()
        If Val(lblstatus.Tag) = 1 Then
            lblstatus.Text = "Closed"
            btnclosejob.Text = "Undo Job close"
        Else
            btnclosejob.Text = "Job Close"
        End If
        txtcustomer.Focus()
        btnnew.Text = "Modify"
        plPrint.Visible = True
        btnjobdelivery.Enabled = True
        gbinvoice.Enabled = True
        btndelete.Enabled = True
        chgbyprg = True
    End Sub
    Private Sub loadItems(ByVal loadedTrId As Long)
        Dim sRs As DataTable
        Dim UPerPack As Integer
        _objcmnbLayer = New clsCommon_BL
        sRs = _objcmnbLayer._fldDatatable("SELECT ItmInvTrTb.*, [Item Code],Warrentyname,isSerialNo,fcode,FraCount,isnull(itemCategory,'')itemCategory,collectionAC,vat,isTaxInvoice, " & _
                                          "JobInvCmnTb.prefix + case when isnull(JobInvCmnTb.prefix,'')='' then '' else '/' end  + isnull(convert(varchar,JobInvCmnTb.InvNo),'') [Job InvNo]  " & _
                                          "FROM ItmInvTrTb LEFT JOIN InvItm ON InvItm.ItemId = ItmInvTrTb.ItemId " & _
                                          "LEFT JOIN ItmInvCmnTb on ItmInvCmnTb.trid=ItmInvTrTb.trid " & _
                                          " LEFT JOIN InvItmPropertiesTb ON InvItmPropertiesTb.Itemid=invitm.itemid " & _
                                          "LEFT JOIN WarrentyMastertb ON WarrentyMastertb.Wid=ItmInvTrTb.WarrentyId " & _
                                          "LEFT JOIN FuelMeterReadingTb ON FuelMeterReadingTb.fmeterid=ItmInvTrTb.FuleMeterid " & _
                                          "LEFT JOIN UnitsTb ON UnitsTb.Units=ItmInvTrTb.Unit " & _
                                          "LEFT JOIN VatMasterTb ON InvItm.vatId=VatMasterTb.vatId " & _
                                          "LEFT JOIN JobInvCmnTb ON JobInvCmnTb.trid=ItmInvTrTb.jobInvTrid " & _
                                          "WHERE ItmInvTrTb.TrId = " & loadedTrId & " ORDER BY SlNo")
        grdVoucher.RowCount = 0
        Dim tNumformat As String
        With grdVoucher
            .Rows.Clear()
            If sRs.Rows.Count > 0 Then
                For i = 0 To sRs.Rows.Count - 1
                    grdVoucher.Rows.Add(1)
                    If i = 0 Then
                        If IsDBNull(sRs(i)("isTaxInvoice")) Then sRs(i)("isTaxInvoice") = 0
                        chktaxInv.Checked = IIf(sRs(i)("isTaxInvoice") = "True", 1, 0)
                    End If
                    'grdVoucher.Item(ConstSlNo, i).Value = IIf(sRs(i)("MsgId") = 1, "M", IIf(sRs(i)("MsgId") = 2, "L", ""))
                    UPerPack = IIf(sRs(i)("PFraction") = 0, 1, sRs(i)("PFraction"))
                    grdVoucher.Item(ConstSlNo, i).Value = IIf(Val(sRs(i)("ItemId")) = 0, "M", "")
                    If grdVoucher.Item(ConstSlNo, i).Value <> "M" Then
                        UPerPack = IIf(sRs(i)("PFraction") = 0 Or IsDBNull(sRs(i)("PFraction")), 1, sRs(i)("PFraction"))
                        grdVoucher.Item(ConstItemCode, i).Value = Trim("" & sRs(i)("Item Code"))
                        grdVoucher.Item(ConstItemID, i).Value = sRs(i)("ItemId")
                        grdVoucher.Item(ConstPFraction, i).Value = sRs(i)("FraCount")
                        grdVoucher.Item(ConstPMult, i).Value = UPerPack 'IIf(IsNull(sRs!PFraCount), "2", sRs!PFraCount)
                    Else
                        grdVoucher.Item(ConstPFraction, i).Value = "2"
                        grdVoucher.Item(ConstPMult, i).Value = "1"
                        grdVoucher.Item(ConstItemID, i).Value = 0
                    End If
                    grdVoucher.Item(ConstDescr, i).Value = IIf(IsDBNull(sRs(i)("IDescription")), "", sRs(i)("IDescription"))
                    grdVoucher.Item(ConstB, i).Value = IIf(sRs(i)("Method") & "" = "", "B", Trim(sRs(i)("Method") & ""))
                    grdVoucher.Item(ConstUnit, i).Value = Trim("" & sRs(i)("Unit"))
                    If Val(sRs(i)("Taxp") & "") = 0 Then sRs(i)("Taxp") = 0
                    grdVoucher.Item(ConstTaxP, i).Value = Format(sRs(i)("Taxp"), numFormat)
                    If Val(sRs(i)("taxamt") & "") = 0 Then sRs(i)("taxamt") = 0
                    grdVoucher.Item(ConstTaxAmt, i).Value = Format(sRs(i)("taxamt") / FCRt, numFormat)

                    If Val(sRs(i)("vat") & "") = 0 Then sRs(i)("vat") = 0
                    grdVoucher.Item(Constcess, i).Value = Format(sRs(i)("vat"), numFormat)
                    If Val(sRs(i)("CessAmt") & "") = 0 Then sRs(i)("CessAmt") = 0
                    grdVoucher.Item(ConstcessAmt, i).Value = Format(sRs(i)("CessAmt") / FCRt, numFormat)
                    If Val(sRs(i)("collectionAC") & "") = 0 Then sRs(i)("collectionAC") = 0
                    grdVoucher.Item(ConstcessAc, i).Value = Val(sRs(i)("collectionAC"))

                    grdVoucher.Item(ConstLocation, i).Value = Trim("" & sRs(i)("Warrentyname")) ' !Colour
                    grdVoucher.Item(ConstQty, i).Value = Format(sRs(i)("TrQty") / UPerPack, "#,##0" & IIf(Val(grdVoucher.Item(ConstPFraction, i).Value & "") = 0, "", "." & Strings.StrDup(CByte(Val(grdVoucher.Item(ConstPFraction, i).Value & "")), "0")))
                    grdVoucher.Item(ConstUPrice, i).Value = Format(sRs(i)("UnitCost") * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & numformat)
                    grdVoucher.Item(ConstActualPrice, i).Value = sRs(i)("UnitCost") * UPerPack / FCRt
                    tNumformat = "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))
                    grdVoucher.Item(ConstLTotal, i).Value = Format((sRs(i)("TrQty") * sRs(i)("UnitCost")) * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & numformat)
                    grdVoucher.Item(ConstUnitOthCost, i).Value = Format(sRs(i)("UnitOthCost") * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & numformat)
                    grdVoucher.Item(ConstActualOthCost, i).Value = sRs(i)("UnitOthCost") * UPerPack / FCRt
                    grdVoucher.Item(ConstSerialNo, i).Value = sRs(i)("SerialNo")
                    .Item(constItmTot, i).Value = Format((.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value), numFormat)
                    'grdVoucher.Item(ConstMthd, i).Value = sRs(i)("Method")
                    'grdVoucher.Item(ConstUnitVal, i).Value = sRs(i)("UnitValue") * UPerPack / FCRt
                    'grdVoucher.Item(ConstDiscOther, i).Value = sRs(i)("DiscOther") * UPerPack / FCRt
                    grdVoucher.Item(ConstDiscOther, i).Value = sRs(i)("UnitDiscount") * UPerPack / FCRt
                    If Val(sRs(i)("DisP") & "") = 0 Then sRs(i)("DisP") = 0
                    grdVoucher.Item(ConstDisP, i).Value = Format(sRs(i)("DisP"), numFormat)
                    If Val(sRs(i)("ItemDiscount") & "") = 0 Then sRs(i)("ItemDiscount") = 0
                    grdVoucher.Item(ConstDisAmt, i).Value = Format(sRs(i)("ItemDiscount") * UPerPack / FCRt, numFormat)
                    grdVoucher.Item(ConstImpDocId, i).Value = sRs(i)("impDocid")
                    grdVoucher.Item(ConstImpLnId, i).Value = sRs(i)("impDocSlno")


                    grdVoucher.Item(ConstBarcode, i).Value = sRs(i)("HSNCode")
                    grdVoucher.Item(ConstCGSTP, i).Value = sRs(i)("CSGTP")
                    grdVoucher.Item(ConstCGSTAmt, i).Value = sRs(i)("CGSTAMT")

                    grdVoucher.Item(ConstSGSTP, i).Value = sRs(i)("SGSTP")
                    grdVoucher.Item(ConstSGSTAmt, i).Value = sRs(i)("SGSTAmt")

                    grdVoucher.Item(ConstIGSTP, i).Value = sRs(i)("IGSTP")
                    grdVoucher.Item(ConstIGSTAmt, i).Value = sRs(i)("IGSTAmt")

                    grdVoucher.Item(ConstInvNo, i).Value = Trim(sRs(i)("Job InvNo") & "")

                    grdVoucher.Item(ConstIsSerial, i).Value = 0
                    grdVoucher.Item(ConstId, i).Value = sRs(i)("id")
                    calcualteLineTotal(i)
                Next

            End If
        End With
        If sRs.Rows.Count > 0 Then sRs.Clear()
        calculate()
        reArrangeNo()
        btnrem.Enabled = True
        chgItm = False
    End Sub

    Private Sub dgvjobhistory_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvjobhistory.DoubleClick
        If btnnew.Text = "Clear" Or btnnew.Text = "Undo" Then
            MsgBox("Please undo or clear editing mode", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If dgvjobhistory.Rows.Count = 0 Then Exit Sub
        txtjobcode.Tag = Val(dgvjobhistory.Item("grid", dgvjobhistory.CurrentRow.Index).Value)
        ldRec()
        enableModify()
    End Sub


    Private Sub grdVoucher_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.Leave
        activecontrolname = ""
    End Sub
    Private Sub calculate(Optional ByVal bFrmCalcOthCost As Boolean = False, Optional ByVal calculateLineTotal As Boolean = False, Optional ByVal chgDiscount As Boolean = False)
        Dim totQty As Double
        Dim totItm As Double
        Dim totTax As Double
        Dim totLnDis As Double
        Dim totAmt As Double
        Dim totCess As Double
        Dim i As Integer
        Dim gindex As Integer
        Dim lnTax As Double
        Dim lnttl As Double
        If grdVoucher.CurrentCell Is Nothing Then
            gindex = grdVoucher.RowCount - 1
        Else
            gindex = grdVoucher.CurrentCell.RowIndex
        End If
        If calculateLineTotal Then
            calcualteLineTotal(gindex)
        End If

        With grdVoucher
            For i = 0 To .Rows.Count - 1
                lnTax = 0
                If Val(.Item(ConstItemID, i).Value) = 0 Then GoTo nxt
                totQty = totQty + IIf(Val(.Item(ConstQty, i).Value) > 0, Val(.Item(ConstQty, i).Value), 0)
                If chktaxInv.Checked = False Then
                    .Item(ConstTaxAmt, i).Value = Format(0, numFormat)
                    .Item(ConstTaxP, i).Value = Format(0, numFormat)
                    .Item(ConstcessAmt, i).Value = 0
                Else
                    .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstIGSTAmt, i).Value), numFormat)
                    .Item(ConstTaxP, i).Value = Format(CDbl(.Item(ConstIGSTP, i).Value), numFormat)
                    If enablecess And cessdate <= DateValue(dtpdate.Value) Then
                        lnTax = (((CDbl(.Item(ConstActualPrice, i).Value) - CDbl(.Item(ConstDiscOther, i).Value)) * .Item(Constcess, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value)
                        .Item(ConstcessAmt, i).Value = Format(lnTax, numFormat)
                    End If
                    lnTax = lnTax + CDbl(.Item(ConstIGSTAmt, i).Value)
                End If
                If chktaxInv.Checked Then
                    totTax = totTax + CDbl(.Item(ConstCGSTAmt, i).Value) + CDbl(.Item(ConstSGSTAmt, i).Value)
                End If
                totLnDis = totLnDis + .Item(ConstDisAmt, i).Value
                totAmt = totAmt + (CDbl(.Item(ConstQty, i).Value) * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value)
                totItm = totItm + (CDbl(.Item(ConstQty, i).Value) * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value)
                If enablecess And cessdate <= DateValue(dtpdate.Value) And chktaxInv.Checked Then
                    totCess = totCess + CDbl(.Item(ConstcessAmt, i).Value)
                End If
                lnttl = (CDbl(.Item(ConstQty, i).Value) * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value)
                lnttl = lnttl + lnTax
                chgbyprg = True
                .Item(ConstLTotal, i).Value = Format(lnttl, numFormat)
                chgbyprg = False
nxt:
            Next
            totAmt = totAmt + totTax + totCess
            lblTotAmt.Text = Format(totItm, numFormat)
            txttotalItemAmt.Text = Format(totAmt, numFormat)
            lblitemvalue.Text = Format(totAmt, numFormat)
            lbltax.Text = Format(totTax, numFormat)
            lblcess.Text = Format(totCess, numFormat)
        End With
        calculateJobvalue()
    End Sub
    Private Sub calcualteLineTotal(ByVal RowIndex As Integer)
        chgbyprg = True
        With grdVoucher
            Dim i As Integer
            i = RowIndex
            If Val(.Item(ConstItemID, i).Value) = 0 Then Exit Sub
            .Item(ConstSlNo, i).Value = i + 1
            If Val(.Item(ConstTaxP, i).Value & "") = 0 Then
                .Item(ConstTaxP, i).Value = 0
            End If
            If Val(.Item(Constcess, i).Value & "") = 0 Then
                .Item(Constcess, i).Value = 0
            End If
            If Val(.Item(ConstActualPrice, i).Value & "") = 0 Then
                .Item(ConstActualPrice, i).Value = 0
            End If
            If Val(.Item(ConstQty, i).Value & "") = 0 Then
                .Item(ConstQty, i).Value = 0
            End If
            Dim gstamt As Double
            Dim cessTtl As Double
            If EnableGST Then
                getGSTDetails(Trim(.Item(ConstBarcode, i).Value & ""), i, True)
                If Val(.Item(ConstSGSTAmt, i).Value & "") = 0 Then .Item(ConstSGSTAmt, i).Value = 0
                If Val(.Item(ConstCGSTAmt, i).Value & "") = 0 Then .Item(ConstCGSTAmt, i).Value = 0
                gstamt = CDbl(.Item(ConstCGSTAmt, i).Value) + CDbl(.Item(ConstSGSTAmt, i).Value)
                .Item(ConstTaxAmt, i).Value = Format(gstamt, numFormat)
            Else
                .Item(ConstTaxAmt, i).Value = Format(((CDbl(.Item(ConstActualPrice, i).Value) * .Item(ConstTaxP, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value), numFormat)
            End If
            If enablecess And cessdate <= DateValue(dtpdate.Value) And chktaxInv.Checked Then
                cessTtl = (((CDbl(.Item(ConstActualPrice, i).Value) - CDbl(.Item(ConstDiscOther, i).Value)) * .Item(Constcess, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value)
                .Item(ConstcessAmt, i).Value = Format(cessTtl, numFormat)
            End If
            If chktaxInv.Checked = False Then
                .Item(ConstTaxAmt, i).Value = Format(0, numFormat)
                .Item(ConstTaxP, i).Value = Format(0, numFormat)
                .Item(ConstcessAmt, i).Value = 0
            End If

            .Item(constItmTot, i).Value = Format((.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value), numFormat)
            Dim ttl As Double
            ttl = (.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value) + gstamt + cessTtl
            .Item(ConstLTotal, i).Value = Format(ttl, numFormat)
            .Item(ConstNUPrice, i).Value = CDbl(.Item(ConstActualPrice, i).Value) - (CDbl(.Item(ConstDisAmt, i).Value) / CDbl(.Item(ConstQty, i).Value)) + (CDbl(.Item(ConstTaxAmt, i).Value) / CDbl(.Item(ConstQty, i).Value)) + (CDbl(.Item(ConstcessAmt, i).Value) / CDbl(.Item(ConstQty, i).Value)) - Val(.Item(ConstDiscOther, i).Value)
        End With
        chgbyprg = False
    End Sub
    Private Sub calculateJobvalue()
        If Val(txtserviceCharge.Text) = 0 Then txtserviceCharge.Text = 0
        If Val(lblitemvalue.Text) = 0 Then lblitemvalue.Text = 0
        lblJobvalue.Text = Format(CDbl(txtserviceCharge.Text) + CDbl(lblitemvalue.Text), numFormat)

        If Val(txtscost.Text) = 0 Then txtscost.Text = 0
        If Val(lblitmcost.Text) = 0 Then lblitmcost.Text = 0
        lbljobcost.Text = Format(CDbl(txtscost.Text) + CDbl(lblitmcost.Text), numFormat)
    End Sub
    Private Sub Valid(ByVal RowIndex As Integer, ByVal ColIndex As Integer)
        With grdVoucher
            Select Case ColIndex
                Case ConstBarcode, ConstItemCode
                    If Not chgItm Then Exit Sub
                    If Trim(.Item(ColIndex, RowIndex).Value) = "" Then Exit Sub
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
                        .Item(ConstImpJobChildTbID, RowIndex).Value = ""
                        .Item(ConstItemID, RowIndex).Value = ""
                        .Item(ConstUnit, RowIndex).Value = ""
                        .Item(ConstSerialNo, RowIndex).Value = ""
                        .Item(ConstPMult, RowIndex).Value = "1"
                        .Item(ConstPFraction, RowIndex).Value = "2"
                        .Item(ConstImpDocId, RowIndex).Value = ""
                        .Item(ConstImpLnId, RowIndex).Value = ""
                        chgItm = False
                    End If

                Case ConstQty
                    If chgAmt Then
                        If AllowUnitDiscountEntryOnInventory Then
                            .Item(ConstDisAmt, RowIndex).Value = Format(((CDbl(.Item(ConstActualPrice, RowIndex).Value) * Val(.Item(ConstDisP, RowIndex).Value)) / 100) * CDbl(.Item(ConstQty, RowIndex).Value), numFormat)
                        End If
                        If EnableGST Then getGSTDetails(Trim(.Item(ConstBarcode, RowIndex).Value & ""), RowIndex, True)

                    End If
                    calculate(, True)
                Case ConstUPrice
                    If chgAmt Then
                        If Format(.Item(ConstActualPrice, RowIndex).Value, numFormat) <> Format(CDbl(.Item(ConstUPrice, RowIndex).Value), numFormat) Then
                            .Item(ConstActualPrice, RowIndex).Value = CDbl(.Item(ConstUPrice, RowIndex).Value)
                        End If
                        If AllowUnitDiscountEntryOnInventory Then
                            .Item(ConstDisAmt, RowIndex).Value = Format(((CDbl(.Item(ConstActualPrice, RowIndex).Value) * Val(.Item(ConstDisP, RowIndex).Value)) / 100) * CDbl(.Item(ConstQty, RowIndex).Value), numFormat)
                        End If
                        If EnableGST Then getGSTDetails(Trim(.Item(ConstBarcode, RowIndex).Value & ""), RowIndex, True)
                        calculate(, True)
                    End If
                Case ConstDisP
                    If chgAmt Then
                        .Item(ConstDisAmt, RowIndex).Value = Format(((CDbl(.Item(ConstActualPrice, RowIndex).Value) * Val(.Item(ConstDisP, RowIndex).Value)) / 100) * CDbl(.Item(ConstQty, RowIndex).Value), numFormat)
                        If EnableGST Then getGSTDetails(Trim(.Item(ConstBarcode, RowIndex).Value & ""), RowIndex, True)
                        calculate(, True)
                    End If
                Case ConstDisAmt
                    If chgAmt Then
                        Dim unitDiscount As Double
                        unitDiscount = CDbl(.Item(ConstDisAmt, RowIndex).Value) / CDbl(.Item(ConstQty, RowIndex).Value)
                        .Item(ConstDisP, RowIndex).Value = Format((unitDiscount * 100) / CDbl(.Item(ConstActualPrice, RowIndex).Value), numFormat)
                        If EnableGST Then getGSTDetails(Trim(.Item(ConstBarcode, RowIndex).Value & ""), RowIndex, True)
                        calculate(, True)
                    End If
                Case ConstTaxAmt
                    If chgAmt Then
                        calculate(, True)
                    End If
                Case ConstTaxP
                    If chgAmt Then
                        calculate(, True)
                    End If
                Case Else
            End Select
        End With
        chgAmt = False
    End Sub
    Private Sub calculateTaxFromUnitPrice(ByVal i As Integer)
        If chgUprice And chkcal.Checked Then
            chgbyprg = True
            With grdVoucher
                .Item(ConstActualPrice, i).Value = (CDbl(.Item(ConstUPrice, i).Value) * 100) / (CDbl(.Item(ConstTaxP, i).Value) + CDbl(.Item(Constcess, i).Value) + 100)
                .Item(ConstUPrice, i).Value = Format(.Item(ConstActualPrice, i).Value, numFormat)
            End With
        End If
        chgUprice = False
        chgbyprg = False
    End Sub
    Private Sub Textbox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            Dim col As Integer
            col = grdVoucher.CurrentCell.ColumnIndex
            If col = ConstQty Or col = ConstNUPrice Or col = ConstLTotal Then
                If Not IsNumeric(e.KeyChar) And e.KeyChar <> "." And Not e.KeyChar = Convert.ToChar(Keys.Back) Then e.Handled = True
                If col = ConstQty Then
                    If grdVoucher.Item(ConstSerialNo, grdVoucher.CurrentRow.Index).Value <> "" And Not enableBatchwiseTr Then
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
            col = grdVoucher.CurrentCell.ColumnIndex
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
                grdVoucher.Item(ConstqtyChg, grdVoucher.CurrentRow.Index).Value = "Chg"
            ElseIf col = ConstBarcode Then
                _srchTxtId = 2
                '_srchIndexId = 2
                chgbyprg = True
                strGridSrchString = MyCtrl.Text
                ShowPanel()
                chgbyprg = False
                'ElseIf col = ConstSerialNo Then
                '    _srchTxtId = 3
                '    '_srchIndexId = 3
                '    chgbyprg = True
                '    strGridSrchString = MyCtrl.Text
                '    ShowPanel()
                '    chgbyprg = False
            ElseIf col = ConstQty Then
                grdVoucher.Item(ConstqtyChg, grdVoucher.CurrentRow.Index).Value = "Chg"
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
                plsrch.Width = 450
            Else

                plsrch.Height = 300
                plsrch.Width = 700
                'x = Me.Width - plsrch.Width - 100
                'y = Me.Height - plsrch.Height - 100
            End If
            x = grdVoucher.Left + grdVoucher.Width - plsrch.Width
            y = grdVoucher.Top
            PopupLoc = New Point(x, y)
            If plsrch.Visible = False Then
                plsrch.Location = PopupLoc
                plsrch.Visible = True
            End If
        End If
        If _srchTxtId = 3 And enableBatchwiseTr Then
            searchProductBatch(grdSrch, strGridSrchString, Val(grdVoucher.Item(ConstItemID, grdVoucher.CurrentRow.Index).Value), isrefreshBatchData)
        Else
            SearchProductPanel(grdSrch, "[Item Code]", strGridSrchString, True)
        End If
        If grdSrch.RowCount > 0 And grdVoucher.CurrentCell.ColumnIndex = ConstSerialNo And enableBatchwiseTr And strGridSrchString = "" Then
            strGridSrchString = grdSrch.Item(2, 0).Value
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
                    grdVoucher.Item(ConstItemCode, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(0) IsNot Nothing, ItmFlds(0), "")
                    'grdVoucher.Item(ConstBarcode, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(1) IsNot Nothing, ItmFlds(1), "")
                    grdVoucher.Item(ConstDescr, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(2) IsNot Nothing, ItmFlds(1), "")
                    SrchText = IIf(ItmFlds(0) IsNot Nothing, ItmFlds(0), strGridSrchString)
                Case 3
                    grdVoucher.Item(ConstSerialNo, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(2) IsNot Nothing, ItmFlds(2), "")
                    'grdVoucher.Item(ConstBarcode, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(1) IsNot Nothing, ItmFlds(1), "")
                    grdVoucher.Item(ConstManufacturingdate, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(3) IsNot Nothing, ItmFlds(3), "")
                    grdVoucher.Item(ConstWarrentyExpiry, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(4) IsNot Nothing, ItmFlds(4), "")
                    grdVoucher.Item(ConstBatchQty, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(2) IsNot Nothing, ItmFlds(2), "")
                    SrchText = IIf(ItmFlds(2) IsNot Nothing, ItmFlds(2), strGridSrchString)
            End Select
nxt:
            chgbyprg = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub getGSTDetails(ByVal hsncode As String, ByVal i As Integer, ByVal calculatefromGrid As Boolean)
        Dim dt As DataTable
        With grdVoucher
            If Not calculatefromGrid Then
                dt = _objcmnbLayer._fldDatatable("SELECT * FROM GSTTb WHERE HSNCODE='" & hsncode & "'")
                If dt.Rows.Count > 0 Then
                    .Item(ConstCGSTP, i).Value = Format(IIf(IsDBNull(dt(0)("CGST")), 0, CDbl(dt(0)("CGST"))), numFormat)
                    .Item(ConstSGSTP, i).Value = Format(IIf(IsDBNull(dt(0)("SGST")), 0, CDbl(dt(0)("SGST"))), numFormat)
                    .Item(ConstIGSTP, i).Value = Format(IIf(IsDBNull(dt(0)("IGST")), 0, CDbl(dt(0)("IGST"))), numFormat)
                Else
                    .Item(ConstCGSTP, i).Value = Format(0, numFormat)
                    .Item(ConstSGSTP, i).Value = Format(0, numFormat)
                    .Item(ConstIGSTP, i).Value = Format(0, numFormat)
                End If
            End If
            Dim actualPrice As Double
            Dim discountOther As Double
            discountOther = CDbl(.Item(ConstDiscOther, i).Value) * CDbl(.Item(ConstQty, i).Value)
            actualPrice = (CDbl(.Item(ConstActualPrice, i).Value) * CDbl(.Item(ConstQty, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value) - discountOther
            'actualPrice = (CDbl(.Item(ConstActualPrice, i).Value) * CDbl(.Item(ConstQty, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value) - discountOther
            actualPrice = Format(actualPrice, numFormat)
            .Item(ConstCGSTAmt, i).Value = (actualPrice * .Item(ConstCGSTP, i).Value) / 100
            .Item(ConstSGSTAmt, i).Value = (actualPrice * .Item(ConstSGSTP, i).Value) / 100
            .Item(ConstIGSTAmt, i).Value = (actualPrice * .Item(ConstIGSTP, i).Value) / 100
            .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstCGSTAmt, i).Value) + CDbl(.Item(ConstSGSTAmt, i).Value), numFormat)
            .Item(ConstTaxP, i).Value = CDbl(.Item(ConstCGSTP, i).Value) + CDbl(.Item(ConstSGSTP, i).Value)
        End With
    End Sub
    Public Sub AddRow(Optional ByVal tocheck As Boolean = False)
        If lblstatus.Text = "Delivered" Then
            MsgBox("You cannot Add/Edit Items in delivered job", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Dim i As Integer
        With grdVoucher
            activecontrolname = "grdVoucher"
            i = .RowCount '- 1
            .Rows.Add(1)
            .Item(ConstSlNo, i).Value = i + 1
            .Item(ConstBarcode, i).Value = ""
            .Item(ConstItemCode, i).Value = ""
            .Item(ConstDescr, i).Value = ""
            .Item(ConstUnit, i).Value = ""
            .Item(ConstQty, i).Value = Format(0, numFormat)
            .Item(ConstUPrice, i).Value = Format(0, numFormat)
            .Item(ConstDisP, i).Value = Format(0, numFormat)
            .Item(ConstDisAmt, i).Value = Format(0, numFormat)
            .Item(ConstTaxP, i).Value = Format(0, numFormat)
            .Item(ConstTaxAmt, i).Value = Format(0, numFormat)
            .Item(ConstLTotal, i).Value = Format(0, numFormat) '(.Item(ConstQty, i).Value * DR!unitPrice) - (.Item(ConstQty, i).Value * Val(DR!DiscountVal)) + (.Item(ConstQty, i).Value * Val(DR!TaxVal))
            .Item(ConstNUPrice, i).Value = Format(0, numFormat) 'Val(DR!unitPrice) - Val(DR!DiscountVal) + Val(DR!TaxVal)
            .Item(ConstItemID, i).Value = 0
            .Item(ConstImpJobChildTbID, i).Value = 0
            .Item(ConstSerialNo, i).Value = ""
            .Item(ConstPMult, i).Value = "1"
            .Item(ConstPFraction, i).Value = "2"
            .Item(ConstImpDocId, i).Value = ""
            .Item(ConstImpLnId, i).Value = ""
            .Item(ConstLrow, i).Value = i + 1
            .CurrentCell = .Item(ConstItemCode, i)
            SrchText = "" ' .Item(ConstItemCode, i).Value
            chgbyprg = True
            .BeginEdit(True)
            chgbyprg = False
            chgItm = False
        End With
        calculate()
    End Sub
    Private Sub RemoveRow()
        If lblstatus.Text = "Delivered" Then
            MsgBox("You cannot Add/Edit Items in delivered job", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If grdVoucher.RowCount > 0 Then
            If MsgBox("You Are Going to Remove the Row." & vbCrLf & "Are You Sure ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then
                Exit Sub
            End If
            With grdVoucher
                If Trim(.Item(ConstInvNo, .CurrentRow.Index).Value & "") <> "" Then
                    MsgBox("You cannot remove invoiced item", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If
                .Rows.RemoveAt(.CurrentRow.Index)
                .ClearSelection()
                '.Rows(.CurrentRow.Index).Selected = True
                calculate()
            End With
            reArrangeNo()
        End If
        ischgItm = True
    End Sub
    Private Sub AddDetails(ByVal DR As DataTable)
        Dim i As Integer
        Dim PMult As Double
        With grdVoucher
            chgbyprg = True
            PMult = 1
            i = .CurrentRow.Index ' .RowCount - 1
            .Item(ConstSlNo, i).Value = i + 1
            .Item(ConstItemCode, i).Value = IIf(IsDBNull(DR(0)("Item Code")), Trim(DR(0)("Item Code") & ""), DR(0)("Item Code"))
            .Item(ConstDescr, i).Value = DR(0)("Description")
            If EnableGST Then
                .Item(ConstBarcode, i).Value = DR(0)("HSNCode") 'hsncode
            Else
                .Item(ConstBarcode, i).Value = ""
            End If
            .Item(ConstQty, i).Value = Format(1, numFormat) 'IIf(IsReturn, -1, 1)
            .Item(ConstItemID, i).Value = DR(0)("ItemId")
            .Item(ConstPMult, i).Value = 1 'IIf(IsNull(sRs(0)("PFraCount), "2", sRs(0)("PFraCount)


            .Item(ConstB, .CurrentCell.RowIndex).Value = "B"
            .Item(ConstPFraction, i).Value = IIf(IsDBNull(DR(0)("FraCount")), "2", DR(0)("FraCount"))
            .Item(ConstUnit, i).Value = DR(0)("Unit")
            .Item(ConstWarrentyExpiry, i).Value = Format(DateAdd(DateInterval.Year, 1, DateValue(dtpdate.Value)), DtFormat)
            If CDbl(.Item(ConstUPrice, i).Value) = 0 Or chgItm Then
                If chkws.Checked Then
                    .Item(ConstActualPrice, i).Value = DR(0)("UnitPriceWS")
                Else
                    .Item(ConstActualPrice, i).Value = DR(0)("UnitPrice")
                End If
                .Item(ConstUPrice, i).Value = Format(CDbl(.Item(ConstActualPrice, i).Value), numFormat)
            End If
            If Not IsDBNull(DR(0)("isSerialNo")) Then
                .Item(ConstIsSerial, i).Value = IIf(DR(0)("isSerialNo"), 1, 0)
            Else
                .Item(ConstIsSerial, i).Value = 0
            End If
            'If Val(DR(0)("vat") & "") = 0 Then DR(0)("vat") = 0
            '.Item(ConstTaxP, i).Value = Format(IIf(IsDBNull(DR(0)("vat")), 0, CDbl(DR(0)("vat"))), numformat)
            '.Item(ConstTaxAmt, i).Value = ((CDbl(.Item(ConstActualPrice, i).Value) * .Item(ConstTaxP, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value)
            If ShowTaxOnInventory Then
                If Val(DR(0)("vat") & "") = 0 Then
                    DR(0)("vat") = 0
                End If
                .Item(ConstTaxP, i).Value = Format(IIf(IsDBNull(DR(0)("vat")), 0, CDbl(DR(0)("vat"))), numFormat)
                .Item(ConstTaxAmt, i).Value = ((CDbl(.Item(ConstActualPrice, i).Value) * .Item(ConstTaxP, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value)
            ElseIf EnableGST Then
                getGSTDetails(Trim(.Item(ConstBarcode, i).Value & ""), i, False)
            End If
            If enablecess And cessdate <= DateValue(dtpdate.Value) Then
                .Item(Constcess, i).Value = Format(IIf(IsDBNull(DR(0)("vat")), 0, CDbl(DR(0)("vat"))), numFormat)
                .Item(ConstcessAc, i).Value = IIf(IsDBNull(DR(0)("collectionAC")), 0, Val(DR(0)("collectionAC")))
                .Item(ConstcessAmt, i).Value = Format(((CDbl(.Item(ConstActualPrice, i).Value) * .Item(Constcess, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value), numFormat)
            End If
            .Item(ConstLTotal, i).Value = Format(.Item(ConstQty, i).Value * CDbl(.Item(ConstUPrice, i).Value), numFormat)
            .Item(ConstNUPrice, i).Value = Format(CDbl(.Item(ConstUPrice, i).Value), numFormat)
            .Item(ConstQty, i).Value = Format(CDbl(.Item(ConstQty, i).Value), "#,##0" & IIf(Val(.Item(ConstPFraction, i).Value) = 0, "", "." & Strings.StrDup(CByte(.Item(ConstPFraction, i).Value), "0")))
            .Item(ConstImpDocId, i).Value = ""
            .Item(ConstImpLnId, i).Value = ""
            chgAmt = True
            chgItm = False
            .ClearSelection()
            'checkItemQty(i)
            'If diableNegativeSale Then
            '    .CurrentCell = .Item(ConstQty, i)
            'End If

        End With
        calculate(, True)
        chgbyprg = False
    End Sub

    Private Sub btnitmAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnitmAdd.Click
        If Val(lblstatus.Tag) > 0 Then
            MsgBox("Job already closed! You cannot add", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        AddRow()
    End Sub

    Private Sub btnrem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrem.Click
        If Val(lblstatus.Tag) > 0 Then
            MsgBox("Job already closed! You cannot remove", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        RemoveRow()
    End Sub
    Private Sub saveJobItems(Optional ByVal isSTOOnly As Boolean = False)
        If lblstatus.Text = "Delivered" Then
            MsgBox("You cannot Add/Edit Items in delivered job", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        _objJob = New clsJob
        Dim itemidsdatatable As DataTable
        Dim dtTable As DataTable
        Dim dateChanged As Boolean
        Dim i As Integer
        dtTable = _objcmnbLayer._fldDatatable("SELECT TOP 1 * FROM ItmInvCmnTb LEFT JOIN VoucherTypeNoTb ON ItmInvCmnTb.TypeNo=VoucherTypeNoTb.vrno " & _
                                                      "WHERE InvType='OUT' AND Trdate >='" & Format(DateValue(dtpdate.Value), "yyyy/MM/dd") & "'")
        If dtTable.Rows.Count > 0 Then
            dateChanged = True
        Else
            dateChanged = False
        End If
        If Val(numsto.Tag) > 0 Then
            ismodi = True
            _objcmnbLayer._saveDatawithOutParm("Update JobitemTb set setRemove=1 WHERE jbid=" & Val(txtjobcode.Tag))
            _objcmnbLayer._saveDatawithOutParm("Update ItmInvTrTb set setRemove=1 WHERE trid=" & Val(numsto.Tag))
        End If
        saveInventory()
        With grdVoucher
            For i = 0 To .Rows.Count - 1 '- 1
                If CDbl(.Item(ConstQty, i).Value) <> 0 Or .Item(ConstSlNo, i).Value.ToString = "M" Then
                    PPerU = Val(.Item(ConstPMult, i).Value) 'Switch(.TextMatrix(i, 4) = "B", 1, .TextMatrix(i, 4) = "P1", CSng(grdVoucher.TextMatrix(i, 18)), .TextMatrix(i, 4) = "P2", CSng(grdVoucher.TextMatrix(i, 19)))
                    PPerU = IIf(PPerU = 0, 1, PPerU)
                    setInvDetValue(Val(numsto.Tag), PPerU, i)
                    _objInv._saveDetails()
                    If dateChanged And enableRealtimeCosting Then
                        _objInv.ItemId = Val(.Item(ConstItemID, i).Value)
                        _objInv.TrDate = DateValue(dtpdate.Value)
                        _objInv.setcostAverageOnModification(UsrBr)
                    End If
                End If
            Next
        End With
        If ismodi Then
            itemidsdatatable = _objcmnbLayer._fldDatatable("select itemid from ItmInvTrTb where setRemove=1 and trid=" & Val(numsto.Tag))
            _objcmnbLayer._saveDatawithOutParm("delete from ItmInvTrTb where setRemove=1 and trid=" & Val(numsto.Tag))
            If itemidsdatatable.Rows.Count > 0 And enableRealtimeCosting Then
                For i = 0 To itemidsdatatable.Rows.Count - 1
                    _objInv.ItemId = itemidsdatatable(i)("Itemid")
                    _objInv.TrDate = DateValue(dtpdate.Value)
                    _objInv.setcostAverageOnModification(UsrBr)
                Next
            End If
        End If
        _objcmnbLayer._saveDatawithOutParm("update GarrageTb set itemValue=" & CDbl(txttotalItemAmt.Text) & " where grid=" & Val(txtjobcode.Tag))

        updateStockTransaction()
        ischgItm = False
        If isSTOOnly Then
            MsgBox("Stock Updated", MsgBoxStyle.Information)
            loadItems(Val(numsto.Tag))
        End If
    End Sub
    Private Sub updateStockTransaction()
        If Not enableCostAccounting Then Exit Sub
        Dim stockAc As Long
        Dim costOfSalesAc As Long
        Dim dt As DataTable
        Dim costAmt As Double
        Dim dtTable As DataTable
        Dim LinkNo As Long
        stockAc = getConstantAccounts(1)
        costOfSalesAc = getConstantAccounts(9)
        If stockAc = 0 Or costOfSalesAc = 0 Then Exit Sub
        If ismodi And Val(numsto.Tag) > 0 Then
            dtTable = _objcmnbLayer._fldDatatable("SELECT LinkNo FROM AccTrCmn WHERE LnkNo = " & Val(numsto.Tag))
            If dtTable.Rows.Count > 0 Then
                LinkNo = dtTable(0)("LinkNo")
                _objcmnbLayer._saveDatawithOutParm("DELETE FROM AccTrDet WHERE LinkNo=" & LinkNo)
            End If
        End If
        setAcctrCmnValue(Val(numsto.Tag), LinkNo)
        LinkNo = 0
        LinkNo = Val(_objTr.SaveAccTrCmn())
        dt = _objcmnbLayer._fldDatatable("select sum(CostAvg*TrQty) costAmt from ItmInvTrTb  where trid=" & Val(numsto.Tag))
        If dt.Rows.Count > 0 Then
            costAmt = dt(0)("costAmt")
        End If
        If costAmt <> 0 Then
            'debit entry [cost of sales]
            Dim entryref As String = "SERVICE STOCK OUT : JOB#" & txtjobcode.Text
            setAcctrDetValue(LinkNo, costOfSalesAc, Val(numsto.Text), entryref, costAmt, "", "", 3, 0, "", _
                           "", stockAc, costOfSalesAc & Val(numsto.Text), "", 1)
            _objTr.saveAccTrans()
            UpdtClosBal(costOfSalesAc, costAmt)
            'credit entry [stock in hand]
            costAmt = costAmt * -1
            setAcctrDetValue(LinkNo, stockAc, numsto.Text, entryref, costAmt, "", "", 3, 0, "", _
                           "", costOfSalesAc, stockAc & numsto.Text, "", 1)
            _objTr.saveAccTrans()
            UpdtClosBal(stockAc, costAmt)
        End If
    End Sub
    Private Sub setAcctrCmnValue(ByVal InvTrid As Long, ByVal LinkNo As Long)
        _objTr.JVType = "STO"
        _objTr.JVDate = DateValue(dtpdate.Value)
        _objTr.PreFix = ""
        _objTr.JVNum = Val(numsto.Text)
        _objTr.JVTypeNo = getVouchernumber("STO")
        _objTr.UserId = Trim(CurrentUser & "")
        _objTr.MchName = MACHINENAME
        _objTr.CrtDtTm = DateValue(Date.Now)
        _objTr.TypeNo = getVouchernumber("STO")
        '_objTr.VrDescr = Trim(txtpayee.Text)
        _objTr.IsModi = IIf(LinkNo > 0, 1, 0)
        _objTr.LinkNo = InvTrid
    End Sub
    Private Sub setAcctrDetValue(ByVal lnkNo As Long, ByVal AccountNo As Long, ByVal Reference As String, ByVal EntryRef As String, ByVal DealAmt As Double, ByVal JobCode As String, ByVal JobStr As String, _
                                  ByVal TrInf As Short, ByVal OthCost As Short, ByVal LPO As String, ByVal TermsId As String, ByVal CustAcc As Integer, ByVal AccWithRef As String, _
                                  ByVal CurrencyCode As String, ByVal CurrRate As Double)
        With _objTr
            .trLinkNo = lnkNo
            .AccountNo = AccountNo
            .Reference = numsto.Text
            .EntryRef = EntryRef
            .DealAmt = DealAmt
            .JobCode = JobCode
            .JobStr = JobStr
            .CurrRate = CurrRate
            .CurrencyCode = CurrencyCode ' IIf(chkFC.Checked = True, txtFC.Text, "")
            .TrInf = TrInf
            .OthCost = OthCost
            .TermsId = TermsId
            .CustAcc = CustAcc
            .AccWithRef = AccWithRef
            .LPONo = LPO
            Dim dtLPO As Date = IIf(chkDate(dtpdate.Value), dtpdate.Value, DateValue(dtpdate.Value))
            .DocDate = dtLPO
            .SuppInvDate = dtLPO
            .DueDate = dtLPO
        End With
    End Sub
    Private Sub saveInventory()

        If Val(numsto.Text & "") = 0 Then
            Dim PreFixTb As DataTable
            PreFixTb = _objcmnbLayer._fldDatatable("SELECT * FROM InvNos WHERE InvType='STO'", False)
            If PreFixTb.Rows.Count > 0 Then
                numsto.Text = Val(PreFixTb.Rows(0)("InvNo"))
            Else
                numsto.Text = 1
            End If
        End If
chkagain:
        If Val(numsto.Tag) = 0 Then
            If Not CheckNoExists("", Val(numsto.Text), "STO", "Inventory") Then
                numsto.Text = Val(numsto.Text) + 1
                GoTo chkagain
            End If
        End If
        setInvCmnValue()
        _objcmnbLayer._saveDatawithOutParm("Update InvItm Set IssdQty=IssdQty-tr.tQty,QIH=QIH+tr.tQty from " & _
                                           "(SELECT Itemid,TrQty tQty from ItmInvTrTb where trid=" & Val(numsto.Tag) & ") tr" & _
                                           "  Where InvItm.ItemId=tr.Itemid")
        '_objcmnbLayer._saveDatawithOutParm("delete from ItmInvTrTb where trid=" & Val(numsto.Tag))
        _objcmnbLayer._saveDatawithOutParm("UPDATE InvNos SET Prefix = '',InvNo=" & Val(numsto.Text) + 1 & " WHERE InvType='STO'")
    End Sub
    Private Sub setInvCmnValue()
        Dim Dt As Date
        With _objInv
            Dt = DateValue(Date.Now)
            .TrId = Val(numsto.Tag & "")
            .TrDate = DateValue(dtpdate.Value)
            .TrType = "STO"
            .DocLstTxt = ""
            .Prefix = ""
            .InvNo = Val(numsto.Text)
            .TrRefNo = numsto.Text ' Trim(txtReference.Text)
            .CSCode = Val(txtcustomer.Tag)
            .PSAcc = PSAcc
            .JobCode = txtjobcode.Text
            .ImpDoc = 0
            .UserId = CurrentUser
            .IsFC = 0
            .FCRate = 1
            .NFraction = 2
            .FC = ""
            .Discount = 0
            .TrDescription = "SERVICE STOCK OUT TRANSACTION"
            .TypeNo = getVouchernumber("STO")
            .EnaJob = False
            .DocDefLoc = ""
            .BrId = ""
            .OthCost = 0
            .Discount1 = 0
            .NetAmt = CDbl(txttotalItemAmt.Text)
            .LPO = ""
            'Dim dt As Date = getServerDate()
            .CrtDt = Dt
            .DelDate = Dt
            .DueDate = Dt
            .DocDate = Dt
            .SuppInvDate = Dt
            .TermsId = ""
            .MchName = MACHINENAME
            .isModi = IIf(Val(numsto.Tag & "") > 0, True, False)
            .lpoclass = ""
            .rndoff = 0
            .isTaxInvoice = chktaxInv.Checked
            numsto.Tag = Val(_objInv._saveCmn())
        End With
    End Sub
    Private Sub setInvDetValue(ByVal Invid As Long, ByVal PPerU As Single, ByVal i As Integer)
        With grdVoucher

            _objInv.dtTrId = Invid
            _objInv.ItemId = IIf(.Item(ConstSlNo, i).Value.ToString <> "M", Val(.Item(ConstItemID, i).Value), 0)
            _objInv.Unit = .Item(ConstUnit, i).Value
            _objInv.TrQty = CDbl(.Item(ConstQty, i).Value) * PPerU
            _objInv.UnitCost = CDbl(.Item(ConstActualPrice, i).Value) * FCRt / PPerU 'CDbl(.TextMatrix(i, 10)) / PPerU

            _objInv.taxP = CDbl(grdVoucher.Item(ConstTaxP, i).Value)
            _objInv.taxAmt = (CDbl(grdVoucher.Item(ConstTaxAmt, i).Value) * FCRt) / CDbl(PPerU)
            _objInv.PFraction = PPerU
            If Val(.Item(ConstUnitOthCost, i).Value) = 0 Then .Item(ConstUnitOthCost, i).Value = 0
            _objInv.UnitOthCost = CDbl(.Item(ConstUnitOthCost, i).Value) * FCRt / PPerU
            _objInv.Method = .Item(ConstB, i).Value
            _objInv.UnitDiscount = Val(.Item(ConstDiscOther, i).Value) * FCRt / PPerU
            If Val(.Item(ConstDisAmt, i).Value & "") = 0 Then .Item(ConstDisAmt, i).Value = 0
            _objInv.ItemDiscount = CDbl(.Item(ConstDisAmt, i).Value) * FCRt / PPerU
            If Val(.Item(ConstDisP, i).Value) = 0 Then .Item(ConstDisP, i).Value = 0
            _objInv.DisP = CDbl(.Item(ConstDisP, i).Value) * FCRt / PPerU

            If Not IsDBNull(.Item(ConstDescr, i).Value) Then
                _objInv.IDescription = IIf(.Item(ConstSlNo, i).Value.ToString = "M", .Item(ConstDescr, i).Value, Trim(.Item(ConstDescr, i).Value))
            End If
            _objInv.SlNo = i + 1
            _objInv.TrTypeNo = getVouchernumber("IS")
            _objInv.TrDateNo = getDateNo(CDate(dtpdate.Value))
            _objInv.TrType = "IS"
            _objInv.id = Val(.Item(ConstId, i).Value)
            _objInv.WarrentyName = .Item(ConstLocation, i).Value
            _objInv.SerialNo = Trim(.Item(ConstSerialNo, i).Value & "")
            If IsDate(.Item(ConstWarrentyExpiry, i).Value) Then
                _objInv.WarrentyExpDate = DateValue(.Item(ConstWarrentyExpiry, i).Value)
            Else
                _objInv.WarrentyExpDate = DateValue("01/01/1950")
            End If
            _objInv.HSNCode = Trim(.Item(ConstBarcode, i).Value & "")
            If Val(.Item(ConstIGSTP, i).Value & "") = 0 Then .Item(ConstIGSTP, i).Value = 0
            _objInv.IGSTP = CDbl(.Item(ConstIGSTP, i).Value)
            If Val(.Item(ConstIGSTAmt, i).Value & "") = 0 Then .Item(ConstIGSTAmt, i).Value = 0
            _objInv.IGSTAmt = CDbl(.Item(ConstIGSTAmt, i).Value)

            If Val(.Item(ConstCGSTP, i).Value & "") = 0 Then .Item(ConstCGSTP, i).Value = 0
            _objInv.CSGTP = CDbl(.Item(ConstCGSTP, i).Value)
            If Val(.Item(ConstCGSTAmt, i).Value & "") = 0 Then .Item(ConstCGSTAmt, i).Value = 0
            _objInv.CGSTAMT = CDbl(.Item(ConstCGSTAmt, i).Value)
            If Val(.Item(ConstSGSTP, i).Value & "") = 0 Then .Item(ConstSGSTP, i).Value = 0
            _objInv.SGSTP = CDbl(.Item(ConstSGSTP, i).Value)
            If Val(.Item(ConstSGSTAmt, i).Value & "") = 0 Then .Item(ConstSGSTAmt, i).Value = 0
            _objInv.SGSTAmt = CDbl(.Item(ConstSGSTAmt, i).Value)
            _objInv.Smancode = Trim(.Item(Constsman, i).Value & "")
            If Val(.Item(ConstStartReading, i).Value) = 0 Then .Item(ConstStartReading, i).Value = 0
            If Val(.Item(ConstEndReading, i).Value) = 0 Then .Item(ConstEndReading, i).Value = 0
            _objInv.StartingReading = CDbl(.Item(ConstStartReading, i).Value)
            _objInv.EndingReading = CDbl(.Item(ConstEndReading, i).Value)
            _objInv.MeterCode = Trim(.Item(ConstMeterCode, i).Value & "")
            _objInv.impDocid = Val(.Item(ConstImpDocId, i).Value & "")
            _objInv.impDocSlno = Val(.Item(ConstImpLnId, i).Value & "")
            If Val(.Item(ConstWoodQty, i).Value) = 0 Then .Item(ConstWoodQty, i).Value = 0
            If Val(.Item(ConstWoodDiscQty, i).Value) = 0 Then .Item(ConstWoodDiscQty, i).Value = 0
            _objInv.WoodNetQty = CDbl(.Item(ConstWoodQty, i).Value) * PPerU
            _objInv.WoodDiscountQty = CDbl(.Item(ConstWoodDiscQty, i).Value) * PPerU
            If Val(.Item(ConstcessAmt, i).Value & "") = 0 Then .Item(ConstcessAmt, i).Value = 0
            _objInv.CessAmt = (CDbl(grdVoucher.Item(ConstcessAmt, i).Value) * FCRt) / CDbl(PPerU)
            If IsDate(.Item(ConstManufacturingdate, i).Value) Then
                _objInv.manufacturingdate = DateValue(.Item(ConstManufacturingdate, i).Value)
            Else
                _objInv.manufacturingdate = DateValue("01/01/1950")
            End If
            If _objInv.ItemId = 0 Then
                _objInv.TrQty = 1
                '_objInv.UnitCost = 1
                '_objInv.taxP = 1
                '_objInv.taxAmt = 1
                '_objInv.UnitDiscount = 0
            End If
            'addtodtTb(Invid, .Item(ConstItemID, i).Value, .Item(ConstId, i).Value)

        End With
    End Sub

    Private Sub btnsto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsto.Click
        If Val(lblstatus.Tag) > 0 Then
            MsgBox("Job already closed! You cannot update stock", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        saveJobItems(True)
    End Sub

    Private Sub TabControl1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.Click
        If Val(txtjobcode.Tag) = 0 And TabControl1.SelectedIndex > 0 Then
            MsgBox("Invalid Job", MsgBoxStyle.Exclamation)
            TabControl1.SelectedIndex = 0
            Exit Sub
        End If
        If TabControl1.SelectedIndex = 1 Then
            If Not EnableGST Then resizeGridColumn(grdVoucher, ConstDescr)
        End If
    End Sub

    Private Sub btninvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btninvoice.Click
        If lblstatus.Text = "Delivered" Then
            MsgBox("You cannot invoice against delivered job", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If userType Then
            btninvoice.Tag = IIf(getRight(86, CurrentUser), 1, 0)
        Else
            btninvoice.Tag = 1
        End If
        If Val(btninvoice.Tag) = 0 Then
            MsgBox("This user do not have permission to Create New Invoice", MsgBoxStyle.Exclamation, Nothing)
        ElseIf Val(txtjobcode.Tag) = 0 Then
            MsgBox("Invalid Job", MsgBoxStyle.Exclamation, Nothing)
        Else
            If grdinvList.RowCount > 0 Then
                If MsgBox("Invoice Found against this job, Do you want to create new Invoice?", MsgBoxStyle.Question & MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            End If
            If (Not fInvoice Is Nothing) Then
                fInvoice = Nothing
            End If
            fInvoice = New JobSalesInvoice
            fInvoice.MdiParent = fMainForm
            fInvoice.Show()
            fInvoice.returnFromJob(Val(txtjobcode.Tag), IIf(chkwS.Checked, 0, 1), rdoserviceinv.Checked, Val(numsto.Tag), True)
        End If
    End Sub

    Public Sub fillGrid()
        Dim num2 As Double
        Dim strSql As String = ("Select  convert(varchar(12), case when prefix=''  then '' else '-' end +  invNo ) as [Inv No] , TrDate [Tr.Date] ," & _
                                "Alias [Cust. Code],AccDescr [Customer Name],(InvAmt-Discount) [Amount],TrRefNo [Ref. No],TrDescription  [Tr. Description]," & _
                                "jobcode,UserId [Created By],TrId from ( select  prefix, invNo ,TrDate ,InvAmt,Discount,AccDescr ,TrRefNo,TrDescription,Alias ," & _
                                "JobInvCmntb.UserId,JOBCODE,JobInvCmntb.TrId from JOBInvCmntb LEFT JOIN JobTb ON JobInvCmntb.jobid=JobTb.jobid  " & _
                                "LEFT JOIN (SELECT Trid,SUM((TrQty*(UnitCost+UnitOthCost))+taxamt)InvAmt FROM JobInvTrTb GROUP BY Trid) Tr ON  JobInvCmntb.Trid=Tr.Trid " & _
                                "left join Accmast on JobInvCmnTb.custid=Accmast.accid where JobInvCmntb.trtype='JIS' and JobInvCmntb.jobid=" & Val(txtjobcode.Tag) & ") as qq  order by TrDate ,InvNo")
        grdinvList.DataSource = Nothing
        _objcmnbLayer = New clsCommon_BL
        Dim source As DataTable = Me._objcmnbLayer._fldDatatable(strSql, False)
        grdinvList.DataSource = source
        SetGridHeadInv()
        chgbyprg = False
        Dim num3 As Integer = (source.Rows.Count - 1)
        Dim i As Integer = 0
        Do While (i <= num3)
            num2 = num2 + source(i)("Amount")
            i += 1
        Loop
        lblinvamt.Text = Format(num2, numFormat)
    End Sub

    Private Sub SetGridHeadInv()

        SetGridProperty(grdinvList)
        With grdinvList
            .Columns.Item((.ColumnCount - 1)).Visible = False
            .Columns.Item("Inv No").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns.Item("Inv No").Width = &H4B
            .Columns.Item("Customer Name").Width = 200
            .Columns.Item("Tr. Description").Width = 200
            .Columns.Item("Amount").DefaultCellStyle.Format = ("N" & NoOfDecimal)
            .Columns.Item("Amount").Frozen = True
            .Columns.Item("Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End With
    End Sub

    Private Sub grdinvList_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdinvList.DoubleClick
        If grdinvList.RowCount = 0 Then Exit Sub
        fMainForm.LoadJIS(Val(grdinvList.Item(grdinvList.ColumnCount - 1, grdinvList.CurrentRow.Index).Value))
    End Sub
    Private Sub loadCarDetails()
        dtTable = _objvh.returncarmaster(1).Tables(0)
        dgvCarmaster.DataSource = dtTable
        SetGridProperty(dgvCarmaster)
        With dgvCarmaster
            .Columns("Vehicle Name").Width = 150
            .Columns("Reg Number").Width = 100
            .Columns("Chasis Number").Width = 150
            .Columns("Engine Number").Width = 150
            .Columns("Phone").Width = 100
            .Columns("Last Job Code").Width = 150
            .Columns("Job Date").Width = 100
            .Columns("carid").Visible = False
            .Columns("lnk").Visible = False
            Dim i As Integer
            cmbOrder.Items.Clear()
            For i = 0 To .Columns.Count - 1
                cmbOrder.Items.Add(.Columns(i).HeaderText)
            Next
            cmbOrder.SelectedIndex = 1
        End With
    End Sub

    Private Sub btneditplatenumber_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btneditplatenumber.Click
        If MsgBox("Do you want change Reg.Number?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then carid = 0 : Exit Sub
        txtplateno.Enabled = True
        btneditplatenumber.Enabled = False
        carid = txtplateno.Tag
        txtplateno.Focus()
    End Sub

    Private Sub txtcarname_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcarname.TextChanged, txtmodel.TextChanged, txtchasisno.TextChanged, txtengineno.TextChanged
        If chgbyprg Then Exit Sub
        chcar = True
    End Sub

    Private Sub TabControl2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl2.Click
        If TabControl2.SelectedIndex = 1 Then
            resizeGridColumn(dgvCarmaster, 5)
            txtSeq.Focus()
            plPrint.Visible = False
        ElseIf TabControl2.SelectedIndex = 2 Then
            'resizeGridColumn(dgvjoblist, 2)
            txtsearchjob.Focus()
            plPrint.Visible = False
        Else
            plPrint.Visible = True
        End If
    End Sub

    Private Sub btncarRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncarRefresh.Click
        loadCarDetails()
        resizeGridColumn(dgvCarmaster, 5)
    End Sub

    Private Sub dgvCarmaster_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvCarmaster.DoubleClick
        If dgvCarmaster.RowCount = 0 Then Exit Sub
        chgbyprg = True
        txtplateno.Text = dgvCarmaster.Item("Reg Number", dgvCarmaster.CurrentRow.Index).Value
        chgbyprg = False
        searchCar()
        TabControl2.SelectedIndex = 0
    End Sub

    Private Sub txtSeq_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeq.TextChanged
        dgvCarmaster.DataSource = SearchGrid(dtTable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
        rptTable = SearchGrid(dtTable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
    End Sub

    Private Sub cmbOrder_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbOrder.SelectedIndexChanged
        txtSeq.Focus()
    End Sub
    Private Sub chkSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSearch.Click
        txtSeq.Focus()
    End Sub

    Private Sub btncreateEstimate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncreateEstimate.Click
        If Val(txtjobcode.Tag) = 0 Then
            MsgBox("Invalid Job", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If Val(lblstatus.Tag) > 0 Then
            MsgBox("Job already closed", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If (Not festmate Is Nothing) Then
            festmate = Nothing
        End If
        festmate = New CustomerQuotation
        festmate.MdiParent = fMainForm
        festmate.txtJob.ReadOnly = True
        festmate.Show()
        festmate.loadFroJob(Val(txtcustomer.Tag), txtjobcode.Text, txtplateno.Text & " / " & txtcarname.Text, True)
    End Sub

    Private Sub festmate_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles festmate.FormClosed
        festmate = Nothing
    End Sub
    Private Sub loadEstimates()
        Dim dt As DataTable
        Dim qry As String = "Select DNo [Doc No] , Ddate [Doc.Date] , AccDescr [Customer Name],InvAmt [Amount],Reference [Ref. No],UserId [Created By],Docid " & _
                        " from ( select  DNo ,DDate ,InvAmt- Discount InvAmt,AccDescr ,Reference,UserId,DocCmnTb.DocId from DocCmnTb" & _
                          " LEFT JOIN (SELECT DocId,SUM((Qty*CostPUnit)+isnull(taxAmt,0)+isnull(CessAmt,0))InvAmt FROM DocTranTb GROUP BY DocId) Tr ON  DocCmnTb.Docid=Tr.Docid" & _
                          " left join Accmast on DocCmnTb.CSCode=Accmast.accid where DocCmnTb.DocType='QTI' AND jobcode='" & txtjobcode.Text & "') as qq  order by Ddate ,Dno"

        dt = _objcmnbLayer._fldDatatable(qry)
        dgvestimateList.DataSource = dt
        SetGridProperty(dgvestimateList)
        With dgvestimateList
            .Columns.Item("Doc No").Width = 75
            .Columns.Item("Doc.Date").Width = 75
            .Columns.Item("Customer Name").Width = 200
            .Columns.Item("Amount").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns.Item("Docid").Visible = False
        End With
    End Sub

    Private Sub btnestimaterefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnestimaterefresh.Click
        loadEstimates()
    End Sub

    Private Sub dgvestimateList_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvestimateList.DoubleClick
        If dgvestimateList.RowCount = 0 Then Exit Sub
        Dim docid As Long
        docid = Val(dgvestimateList.Item("docid", dgvestimateList.CurrentRow.Index).Value)
        fMainForm.LoadQTI(docid)
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Dim RptType As String
        If TabControl2.SelectedIndex = 0 Then
            RptType = "GJB"
        ElseIf TabControl2.SelectedIndex = 1 Then
            RptType = "GCL"
        Else
            RptType = "GJPL"
        End If
        If chkjobhistory.Checked Then
            RptType = "GJBH"
        End If
        If chkFormat.Checked Then
            fRptFormat = New RptFormatfrm
            fRptFormat.RptType = RptType
            fRptFormat.ShowDialog()
        Else
            PrepareRpt(RptType)
        End If
    End Sub
    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        LoadReport(RptFlName, RptCaption)
    End Sub
    Private Sub fRptFormat_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fRptFormat.FormClosed
        fRptFormat = Nothing
    End Sub
    Private Sub PrepareRpt(ByVal RptType As String, Optional ByVal Forprint As Boolean = False)

        If txtprintjob.Text = "" And TabControl2.SelectedIndex = 0 And chkjobhistory.Checked = False Then
            MsgBox("Enter a valid Job Code !!", vbInformation)
            txtprintjob.Focus()
            Exit Sub
        End If
        Dim RptName As String
        Dim RptCaption As String = ""
        RptName = getRptDefFlName(RptType, RptCaption)
        If Trim(RptName) <> "" Then
            LoadReport(RptName, RptCaption, Forprint)
        End If
    End Sub
    Private Sub LoadReport(ByVal FileName As String, ByVal RptCaption As String, Optional ByVal ToPrint As Boolean = False)
        If Not frm Is Nothing Then
            frm.Close()
            frm = New ReprtviewNEWfrm
        Else
            frm = New ReprtviewNEWfrm
        End If
        _objJob = New clsJob
        _objJob.jobcode = txtprintjob.Text
        Dim ds As New DataSet
        
        If TabControl2.SelectedIndex = 0 Then
            If chkjobhistory.Checked Then
                ds = _objvh.returnCarjobHistory(Val(txtplateno.Tag))
                GoTo ext
            End If
            ds = _objJob.returnJobForInvPrint(2)
        ElseIf TabControl2.SelectedIndex = 1 Then
            If chkjobhistory.Checked Then
                Dim rindex As Integer
                If Not dgvCarmaster.CurrentRow Is Nothing Then
                    rindex = dgvCarmaster.CurrentRow.Index
                End If
                ds = _objvh.returnCarjobHistory(Val(dgvCarmaster.Item("carid", rindex).Value))
                GoTo ext
            End If
            If rptTable Is Nothing Then
                ds = _objvh.returncarmaster(1)
            Else
                ds.Tables.Add(rptTable)
            End If
        Else
            If rptTable Is Nothing Then
                ds = returnJobList()
            Else
                ds.Tables.Add(rptTable)
            End If
        End If
ext:
        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = fMainForm
        frm.Text = RptCaption
        frm.Show()
        rptTable = Nothing

    End Sub

    Private Sub frm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles frm.FormClosed
        frm = Nothing
    End Sub
    Private Function returnJobList() As DataSet
        Dim status As Integer
        Dim tp As Integer
        If rdoactive.Checked Then
            status = 0
        ElseIf rdoclosed.Checked Then
            status = 1
        ElseIf rdodelivered.Checked Then
            status = 2
        End If
        If rdoAll.Checked Then
            tp = 0
        Else
            If rdopdeliverydate.Checked Then
                tp = 2
            Else
                tp = 1
            End If
        End If
        Return _objvh.retunGarrageJobList(tp, DateValue(dtpFrom.Value), DateValue(dtpTo.Value), status)
    End Function
    Private Sub loadJoblist()
        dtTableJob = returnJobList.Tables(0)
        dgvjoblist.DataSource = dtTableJob
        SetGridProperty(dgvjoblist)
        With dgvjoblist
            .Columns("Job Code").Width = 100
            .Columns("Job Date").Width = 80
            .Columns("Customer Name").Width = 200
            .Columns("Phone").Width = 100
            .Columns("Technician").Width = 100
            .Columns("Reg Number").Width = 100
            .Columns("Reg Number").Frozen = True
            .Columns("Chasis Number").Width = 150
            .Columns("Engine Number").Width = 150
            .Columns("Model").Width = 150
            .Columns("Vehicle Name").Width = 200
            .Columns("Estimated Date").HeaderText = "Est. Date"
            .Columns("Estimated Date").Width = 85
            .Columns("Estimated Amt").HeaderText = "Est. Amt."
            .Columns("Estimated Amt").Width = 100
            .Columns.Item("Estimated Amt").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Estimated Amt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Job Status").Width = 85
            .Columns("Delivery Date").Width = 110
            .Columns("Service Cost").Width = 110
            .Columns.Item("Service Cost").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Service Cost").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Item Cost").Width = 100
            .Columns.Item("Item Cost").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Item Cost").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns("total cost").Width = 100
            .Columns.Item("total cost").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("total cost").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns("Item Value").Width = 100
            .Columns.Item("Item Value").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Item Value").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns("Service Amt").Width = 100
            .Columns.Item("Service Amt").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Service Amt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns("Gross Income").Width = 110
            .Columns.Item("Gross Income").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Gross Income").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


            .Columns("Inv Amount").Width = 100
            .Columns.Item("Inv Amount").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Inv Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Profit").Width = 100
            .Columns.Item("Profit").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Profit").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns("Profit%").Width = 100
            .Columns.Item("Profit%").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Profit%").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns("lnk").Visible = False
            .Columns("datefrom").Visible = False
            .Columns("dateto").Visible = False
            .Columns("grid").Visible = False
            Dim i As Integer
            cmbcolumsjob.Items.Clear()
            For i = 0 To .Columns.Count - 1
                cmbcolumsjob.Items.Add(.Columns(i).HeaderText)
            Next
            cmbcolumsjob.SelectedIndex = 0
        End With
    End Sub

    Private Sub TabPage5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage5.Click

    End Sub

    Private Sub btnrefreshjob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefreshjob.Click
        loadJoblist()
    End Sub

    Private Sub dgvjoblist_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvjoblist.DoubleClick
        If btnnew.Text = "Clear" Or btnnew.Text = "Undo" Then
            MsgBox("Please undo or clear editing mode", MsgBoxStyle.Exclamation)
            TabControl2.SelectedIndex = 0
            Exit Sub
        End If
        chgbyprg = True
        txtjobcode.Tag = Val(dgvjoblist.Item("grid", dgvjoblist.CurrentRow.Index).Value)
        txtplateno.Text = Trim(dgvjoblist.Item("Reg Number", dgvjoblist.CurrentRow.Index).Value & "")
        chgbyprg = False
        searchCar()
        ldRec()
        TabControl2.SelectedIndex = 0
        txtplateno.Focus()
    End Sub
    Private Sub diableColums()
        Dim i As Integer
        For i = 1 To grdVoucher.Columns.Count - 1
            With grdVoucher
                If .Columns(i).Visible = True Then
                    If Trim(.Item(ConstInvNo, .CurrentCell.RowIndex).Value & "") <> "" Then
                        .Columns(i).ReadOnly = True
                    Else
                        If i <> ConstSlNo And i <> constItmTot And i <> ConstUnit And i <> ConstLTotal And i <> ConstBarcode And i <> ConstTaxAmt And i <> ConstStartReading And i <> ConstcessAmt Then
                            .Columns(i).ReadOnly = False
                        End If
                    End If
                End If
            End With
        Next
    End Sub

    Private Sub fInvoice_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fInvoice.FormClosed
        fInvoice = Nothing
        ldRec()
    End Sub

    Private Sub btnjobdelivery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnjobdelivery.Click
        If btnjobdelivery.Text <> "Delivery" Then
            If MsgBox("Do you want to undo Delivery", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            _objcmnbLayer._saveDatawithOutParm("Update GarrageTb set " & _
                                          "deliveredBy=''," & _
                                          "Status=0," & _
                                          "receivedBy='' " & _
                                          "where grid=" & Val(txtjobcode.Tag))
            MsgBox("Updated", MsgBoxStyle.Information)
        Else
            Dim frm As New GarrageJobDeliveryFrm
            With frm
                .lblcar.Text = txtplateno.Text & " / " & txtjobcode.Text
                .lblcar.Tag = Val(txtjobcode.Tag)
                .ShowDialog()
            End With
        End If

        ldRec()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        If Val(btndelete.Tag) = 0 Then
            MsgBox("This user do not have permission to Remove", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If grdinvList.RowCount > 0 Then
            MsgBox("You cannot delete invoiced job", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If MsgBox("You are going to REMOVE the JOB # " & txtjobcode.Text & Chr(13) & "Are you sure ?", vbOKCancel + vbQuestion + vbDefaultButton2) = vbOK Then

            _objcmnbLayer._saveDatawithOutParm("delete from GarrageTb where grid=" & Val(txtjobcode.Tag) & _
                                               " delete from GarrageAccessoriesTb where grdid=" & Val(txtjobcode.Tag) & _
                                               " delete from GarrageDemandedRepairTb where grid=" & Val(txtjobcode.Tag))
            _objInv.TrId = Val(numsto.Tag)
            _objInv.TrType = "OUT"
            _objInv.deleteInventoryTransactions()
            undo()
            searchCar()
        End If
    End Sub

    Private Sub txtsearchjob_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtsearchjob.TextChanged
        dgvjoblist.DataSource = SearchGrid(dtTableJob, Trim(txtsearchjob.Text), cmbcolumsjob.SelectedIndex, Not chkstartonlyjob.Checked)
        rptTable = SearchGrid(dtTableJob, Trim(txtsearchjob.Text), cmbcolumsjob.SelectedIndex, Not chkstartonlyjob.Checked)
    End Sub
    Private Sub fProductEnquiry_CreateItem() Handles fProductEnquiry.CreateItem
        If fproductMast Is Nothing Then
            fproductMast = New ItemMastFrm
            fproductMast.MdiParent = fMainForm
            'fproductMast.Top =  Top + 500
            fproductMast.Show()
        Else
            fproductMast.Focus()

        End If
    End Sub
    Private Sub fProductEnquiry_getSelected(ByVal ItemcODE As String, ByVal isacc As Integer) Handles fProductEnquiry.getSelected
        chgbyprg = True
        fProductEnquiry.Close()
        SrchText = ItemcODE
        grdVoucher.Item(ConstItemCode, grdVoucher.CurrentRow.Index).Value = ItemcODE
        chgItm = True
        Valid(grdVoucher.CurrentRow.Index, ConstItemCode)
        'If Not diableNegativeSale Then
        '    
        'End If
        grdVoucher.CurrentCell = grdVoucher.Item(3, grdVoucher.CurrentRow.Index)
        grdBeginEdit()
        chgbyprg = False
    End Sub

    Private Sub btnclosejob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclosejob.Click
        If btnclosejob.Text = "Job Close" Then
            If MsgBox("Do you want close the job?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            _objcmnbLayer._saveDatawithOutParm("Update GarrageTb set Status=1,closingdate='" & Format(DateValue(Date.Now), "yyyy/MMM/dd") & "' where grid=" & Val(txtjobcode.Tag))
        Else
            If Val(lblstatus.Tag) > 1 Then
                MsgBox("Job already delivered! you cannot undo job close", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            If MsgBox("Do you want undo job close?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            _objcmnbLayer._saveDatawithOutParm("Update GarrageTb set Status=0 where grid=" & Val(txtjobcode.Tag))
        End If
        ldRec()
    End Sub

    Private Sub picCloseProd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picCloseProd.Click
        plsrch.Visible = False
    End Sub

    Private Sub grdSrch_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSrch.CellDoubleClick
        activecontrolname = "grdVoucher"
        doSelect(2)
        chgbyprg = True
        If grdVoucher.CurrentCell.ColumnIndex = ConstItemCode Then
            grdVoucher.CurrentCell = grdVoucher.Item(1, grdVoucher.CurrentRow.Index)
            chgItm = True
            Valid(grdVoucher.CurrentCell.RowIndex, grdVoucher.CurrentCell.ColumnIndex)
            grdBeginEdit()
        ElseIf grdVoucher.CurrentCell.ColumnIndex = ConstSerialNo Then
            grdVoucher.CurrentCell = grdVoucher.Item(ConstSerialNo, grdVoucher.CurrentRow.Index)
            'Valid(grdVoucher.CurrentCell.RowIndex, grdVoucher.CurrentCell.ColumnIndex)
            'grdBeginEdit()
        End If

        plsrch.Visible = False
        chgbyprg = False
    End Sub

    Private Sub txtkm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtkm.KeyDown, txtmat.KeyDown, txtmudflap.KeyDown, txtwheelcup.KeyDown, txtspeekers.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")

        End If
    End Sub

    Private Sub txtscost_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtscost.KeyPress, txtmat.KeyPress, txtmudflap.KeyPress, txtwheelcup.KeyPress, txtspeekers.KeyPress

        NumericTextOnKeypress(sender, e, chgbyprg, "0")

    End Sub

    Private Sub txtscost_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtscost.TextChanged

    End Sub

    Private Sub btnadd_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles btnadd.KeyPress

    End Sub

    Private Sub dgvjobhistory_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvjobhistory.CellContentClick

    End Sub

    Private Sub txtkm_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtkm.TextChanged

    End Sub

    Private Sub txtjobcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtjobcode.TextChanged

    End Sub

    Private Sub TabPage2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage2.Click

    End Sub

    Private Sub dgvCarmaster_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvCarmaster.CellContentClick

    End Sub

    Private Sub chkwS_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkwS.CheckedChanged

    End Sub

    Private Sub grdinvList_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdinvList.CellContentClick

    End Sub
End Class