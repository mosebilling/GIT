Public Class usedcar10
    Private chgbyprg As Boolean
    Private chcar As Boolean
    Private carid As Long
    Private SrchText As String
    Private _srchIndexId As Byte
    Private _srchTxtId As Integer
    Private _srchOnce As Boolean
    Private MyActiveControl As New Object
    Private lstKey As Integer
    Private isset As Boolean
    Private dtTable As DataTable
    Private dtTableJob As DataTable
    Private rptTable As DataTable
    Private Const ConstInvNo = ConstBatchQty + 1
#Region "Class Objects"
    Private _objJob As clsJob
    Private _objcmnbLayer As New clsCommon_BL
    Private _objInv As New clsInvoice
    Private _objTr As New clsAccountTransaction
    Private _objvh As New clsVechicle
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


    Private Sub btnsearchPlateno_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsearchPlateno.Click
        If btnsearchPlateno.Text = "Clear" Then
            If Val(txtjobcode.Tag) > 0 Then
                If MsgBox("Job opened! Do you want clear data?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                undo()
            End If
            clearCarDetails()
        End If
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
        'jobhistory()
    End Sub
    'Private Sub jobhistory()
    '    Dim dt As DataTable
    '    dt = _objcmnbLayer._fldDatatable("select jobcode [Job Code],jobdate [Date],SManName [Technician],grid from GarrageTb " & _
    '                                     "left join CarMasterTb on CarMasterTb.carid=GarrageTb.carid " & _
    '                                     "left join SalesmanTb on SalesmanTb.salesmanid=GarrageTb.technician " & _
    '                                     "where platenumber='" & txtplateno.Text & "' order by jobdate desc")
    '    dgvjobhistory.DataSource = dt
    '    SetGridProperty(dgvjobhistory)
    '    If dgvjobhistory.ColumnCount > 0 Then

    '    End If
    '    With dgvjobhistory
    '        .Columns("Job Code").Width = 80
    '        .Columns("Date").Width = 75
    '        .Columns(.Columns.Count - 1).Visible = False
    '    End With
    '    resizeGridColumn(dgvjobhistory, 2)
    'End Sub
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

        lblJobvalue.Text = Format(0, numFormat)
        lblitmcost.Text = Format(0, numFormat)
        lbljobcost.Text = Format(0, numFormat)
        rdo14.Checked = True
        txtkm.Text = ""
        txtother.Text = ""
        txttotalItemAmt.Text = Format(0, numFormat)

        grdcomplaints.Rows.Clear()
        numsto.Text = ""
        numsto.Tag = ""
        txtjobcode.Tag = ""
        lblstatus.Text = ""
        lblstatus.Tag = ""
        txtmat.Text = ""
        txtmudflap.Text = ""
        txtwheelcup.Text = ""
        txtspeekers.Text = ""
        txtscost.Text = Format(0, numFormat)
        If Val(txtplateno.Tag) = 0 Then
            txtcustomer.Text = ""
            txtcustomer.Tag = ""
            txtaddress.Text = ""
        End If

        fillGrid()
        chgbyprg = False
        'ischgItm = False
        Dim i As Integer
        For i = 0 To chAccessories.Items.Count - 1
            chAccessories.SetItemChecked(i, False)
        Next
        btnsearchPlateno.Enabled = True
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
        'gbinvoice.Enabled = Not EnDs
        'btnupdate.Enabled = Not EnDs
        txtmat.ReadOnly = EnDs
        txtmudflap.ReadOnly = EnDs
        txtwheelcup.ReadOnly = EnDs
        txtspeekers.ReadOnly = EnDs
    End Sub
    Public Sub fillGrid()

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

    Private Sub txtcustomer_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcustomer.Validated, txttechnician.Validated
        If txtcustomer.Text = "" Then
            If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
            Exit Sub
        End If
        loadCustomerDetails(0)
        chgbyprg = False
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
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

        Else
            txtcustomer.Text = ""
        End If
        chgbyprg = False
        txttechnician.Focus()
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

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
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
    Private Sub GarrageJobCardFrm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtplateno.Focus()
    End Sub
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


            isset = True
        End With
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





    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class