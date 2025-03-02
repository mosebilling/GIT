Public Class ContractJobList
#Region "Class Objects"
    Private _objJob As clsJob
    Private _objcmnbLayer As clsCommon_BL
#End Region
#Region "Private variables"
    Private _vtable As DataTable
    Private _dtRptTable As DataTable
    Private chgbyprg As Boolean
    Private _srchTxtId As Integer
    Private _srchOnce As Boolean
    Private MyActiveControl As New Object
    Private lstKey As Integer
#End Region
#Region "Form Objects"
    Private WithEvents fMList As Mlistfrm
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fRptFormat As RptFormatfrm
    Private WithEvents fSelect As Selectfrm
    Private WithEvents fDelivery As JobDelivery
#End Region
#Region "Public variables"
    Public rptCategory As Integer
#End Region

    Private Sub ShowFmlist(ByVal myCtrl As Control)
        If chgbyprg Then Exit Sub
        MyActiveControl = myCtrl
        If Not _srchOnce Then
            chgbyprg = True
            If fMList Is Nothing Then
                fMList = New Mlistfrm
            End If
            Dim PopupLoc As Point
            Dim x As Integer = Me.Width - fMList.Width
            Dim y As Integer = Me.Height - fMList.Height - 10
            PopupLoc = New Point(x, y)
            If fMList.Visible = False Then
                Select Case _srchTxtId
                    Case 1
                        SetFmlist(fMList, 3)
                    Case 2
                        SetFmlist(fMList, 2)
                End Select
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
                'fMList.Search(txtsearch.Text)
                'fMList.AssignList(txtsearch, lstKey, chgbyprg)
            Case 2   'Item name
                fMList.SearchIndex = 1
                fMList.SearchIndexDescr = 0
                fMList_doFocus()
                'fMList.Search(txtsearch.Text)
                'fMList.AssignList(txtsearch, lstKey, chgbyprg)
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
            'Case 1
            '    txtsearch.Text = ItmFlds(0)
            '    txtsearch.Tag = ItmFlds(3)
            'Case 2
            '    txtsearch.Text = ItmFlds(1)
            '    txtsearch.Tag = ItmFlds(2)
        End Select
        chgbyprg = False
    End Sub
    Private Sub loadTech()
        Dim dt As DataTable
        _objcmnbLayer = New clsCommon_BL
        dt = _objcmnbLayer._fldDatatable("Select SManCode from SalesmanTb")
        'cmbtech.Items.Clear()
        Dim i As Integer
        For i = 0 To dt.Rows.Count - 1
            'cmbtech.Items.Add(dt(i)(0))
        Next
    End Sub
    Private Sub setGridForDelivery()
        If grdItem.ColumnCount <> 0 Then
            SetGridProperty(grdItem)
            With grdItem
                .Columns.Item("Jobcode").HeaderText = "Job Code"
                .Columns.Item("jobdate").HeaderText = "Job Date"
                .Columns.Item("Jobname").HeaderText = "Job Name"
                .Columns.Item("AccDescr").HeaderText = "Customer Name"
                .Columns.Item("ContactName").HeaderText = "Contact Name"
                .Columns.Item("Delivered").Frozen = True
                .Columns.Item("Closed").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns.Item("Delivered").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns.Item("jobid").Visible = False
                .Columns.Item("AccDescr").Width = 200
                .Columns.Item("ContactName").Width = 200
                .Columns.Item("DateFrom").Visible = False
                .Columns.Item("DateTo").Visible = False
                .Columns.Item("lnk").Visible = False
                .Columns.Item("tp").Visible = False
            End With
            setComboGrid()
        End If
    End Sub



    Private Sub SetGrid()
        With grdItem
            If .ColumnCount = 0 Then Exit Sub
            SetGridProperty(grdItem)



            If (rptCategory = 19) Then
                .Columns.Item("EstimatedDate").Visible = False
                .Columns.Item("EstimatedAmt").Visible = False
                .Columns.Item("ContactName").Visible = False
                .Columns.Item("jobid").Visible = False
                .Columns.Item("JobDescription").Visible = False
                .Columns.Item("Phone").Visible = False
                .Columns.Item("ItemCost").Visible = False
                .Columns.Item("Other Expense").Visible = True
                .Columns.Item("JobCloseDate").Visible = False
                .Columns.Item("Other Expense").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns.Item("Other Expense").DefaultCellStyle.Format = ("N" & NoOfDecimal)
                .Columns.Item("Profit").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns.Item("Profit").DefaultCellStyle.Format = ("N" & NoOfDecimal)
                .Columns.Item("Profit %").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns.Item("Profit %").DefaultCellStyle.Format = ("N" & NoOfDecimal)
                .Columns.Item("LabourCost").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns.Item("LabourCost").DefaultCellStyle.Format = ("N" & NoOfDecimal)
                .Columns.Item("JobValue").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns.Item("JobValue").DefaultCellStyle.Format = ("N" & NoOfDecimal)
                .Columns.Item("LabourCost").Visible = False
                .Columns.Item("Item Cost").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns.Item("Item Cost").DefaultCellStyle.Format = ("N" & NoOfDecimal)
                .Columns.Item("TotCost").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns.Item("TotCost").DefaultCellStyle.Format = ("N" & NoOfDecimal)
                .Columns.Item("SManName").Visible = False
                .Columns("Datefrom").Visible = False
                .Columns("Dateto").Visible = False
                .Columns("lnk").Visible = False
                .Columns.Item("Other Expense").Width = 120
            Else
                .Columns("Jobcode").HeaderText = "Job Code"
                .Columns("jobtype").HeaderText = "Job Type"
                .Columns("jobdate").HeaderText = "Job Date"
                .Columns("Pjobcode").HeaderText = "P.Job Code"
                .Columns("Jobname").HeaderText = "Job Name"
                .Columns("JobDescription").HeaderText = "Job Description"
                .Columns("AccDescr").HeaderText = "Customer Name"
                .Columns("ContactName").HeaderText = "Contact Name"
                .Columns("EstimatedDate").HeaderText = "Est Date"
                .Columns("EstimatedAmt").HeaderText = "Est Amount"
                .Columns("JobValue").HeaderText = "Job Value"
                .Columns("Userid").HeaderText = "Created By"
                .Columns("CrdtDate").HeaderText = "Created Date"
                .Columns("JobCloseDate").HeaderText = "Cls Date"
                .Columns("EstimatedAmt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("EstimatedAmt").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("JobValue").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("JobValue").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("QtnAmt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("QtnAmt").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("Closed").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("ItemCost").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("ItemCost").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("ServiceCost").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("ServiceCost").DefaultCellStyle.Format = "N" & NoOfDecimal

                .Columns("Datefrom").Visible = False
                .Columns("Dateto").Visible = False
                .Columns("lnk").Visible = False
                .Columns("jobid").Visible = False
                .Columns("ServiceCost").Visible = True
                .Columns("ItemCost").Visible = True
                .Columns("JobCloseDate").Visible = True

                .Columns("jobtype").Width = 78
                .Columns("JobDescription").Width = 300
                .Columns("AccDescr").Width = 200
                .Columns("ContactName").Width = 200
                .Columns("EstimatedDate").Width = 80
                .Columns("JobCloseDate").Width = 80
                .Columns("jobdate").Width = 80
                .Columns("Closed").Width = 45
                .Columns("EstimatedAmt").Width = 150
                .Columns("CrdtDate").Width = 150
                .Columns("Jobname").Width = 200
                .Columns("Jobname").Frozen = True
                .Columns("parentid").Visible = False
            End If
            setComboGrid()

        End With
    End Sub
    Private Sub ldJobdetails()

        _objJob = New clsJob
        With _objJob
            .Jobid = 0
            .DateFrom = DateValue(cldrStartDate.Value)
            .DateTo = DateValue(cldrEnddate.Value)
            .custcode = 0 'Val(txtsearch.Tag)
            .Tp = rptCategory
            _vtable = .returnJob.Tables(0)
            Dim _qurey As EnumerableRowCollection(Of DataRow)
            If rdoparentjob.Checked Then
                _qurey = From data In _vtable.AsEnumerable() Where data("parentid") > 0 Select data
            Else
                _qurey = From data In _vtable.AsEnumerable() Where data("parentid") = 0 Select data
            End If
            If _qurey.Count > 0 Then
                _vtable = _qurey.CopyToDataTable()
            End If
        End With
        grdItem.DataSource = Nothing
        grdItem.DataSource = _vtable
        SetGrid()

    End Sub

    Private Sub btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnload.Click
        ldJobdetails()
        setComboGrid()
    End Sub

    Private Sub txtSeq_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeq.TextChanged
        grdItem.DataSource = SearchGrid(_vtable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
        _dtRptTable = SearchGrid(_vtable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
    End Sub
    Private Sub setComboGrid()
        cmbOrder.Items.Clear()
        Dim i As Integer = 0
        For i = 0 To grdItem.ColumnCount - 2
            cmbOrder.Items.Add(grdItem.Columns(i).HeaderText)
        Next
        If cmbOrder.Items.Count > 0 Then cmbOrder.SelectedIndex = 0
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        If Val(btnEdit.Tag) = 0 Then
            MsgBox("This user do not have permission to Edit", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Dim fjob As New ServiceJob
        If grdItem.RowCount = 0 Then
            Exit Sub
        End If
        If Val(grdItem.Item("jobid", grdItem.CurrentRow.Index).Value) = 0 Then
            Exit Sub
        End If
        fMainForm.ldECJob(Val(grdItem.Item("jobid", grdItem.CurrentRow.Index).Value))
    End Sub

    Private Sub btncloseJob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncloseJob.Click
        If Val(btncloseJob.Tag) = 0 Then
            MsgBox("This user do not have permission to Close Job", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Dim frm As New JobClosing
        If grdItem.RowCount = 0 Then Exit Sub
        frm.cldrdate.Tag = Val(grdItem.Item("jobid", grdItem.CurrentRow.Index).Value)
        frm.lbljjob.Text = "Job Code : " & grdItem.Item("Jobcode", grdItem.CurrentRow.Index).Value
        frm.ShowDialog()
    End Sub

    
    Private Sub ldSelect(ByVal BVal As Single)
        fSelect = New Selectfrm
        'nSelect = BVal
        SetForm(fSelect, BVal)
        If BVal = 1 Or BVal = 0 Then
            fSelect.Width = 894
            fSelect.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
        Else
            fSelect.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedToolWindow
            fSelect.Width = 425
        End If
        fSelect.Show()
    End Sub

    Private Sub txtsearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If chgbyprg = True Then Exit Sub
        If rptCategory < 6 Or rptCategory = 7 Then Exit Sub
        Select Case rptCategory
            Case 6
                _srchTxtId = 1
            Case 9
                _srchTxtId = 2
        End Select
        _srchOnce = False
        ShowFmlist(sender)
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Dim RptType As String = ""
        Select Case rptCategory
            Case 15
                RptType = "JCS1" 'All Job datewise
            Case 16
                RptType = "JCS2" 'Active job datewise 
            Case 17, 18
                RptType = "JCS3" 'Closed Job/closed datewise
            Case 19
                RptType = "JBC10" ' Job profit analysis
        End Select
        fRptFormat = New RptFormatfrm
        fRptFormat.RptType = RptType
        fRptFormat.ShowDialog()
        fRptFormat = Nothing
    End Sub
    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        PrepareReport(RptFlName, "", forPrint)
    End Sub
    Public Sub PrepareReport(ByVal FileName As String, ByVal RptCaption As String, ByVal forpint As Boolean)
        If Not frm Is Nothing Then
            frm.Close()
            frm = New ReprtviewNEWfrm
        Else
            frm = New ReprtviewNEWfrm
        End If
        Dim ds As New DataSet
        If _dtRptTable Is Nothing Then
            With _objJob
                .Jobid = 0
                .DateFrom = DateValue(cldrStartDate.Value)
                .DateTo = DateValue(cldrEnddate.Value)
                .custcode = 0 ' Val(txtsearch.Tag)
                .IMEI = 0 'IIf(rptCategory = 8, cmbtech.Text, txtsearch.Text)
                'rptCategory= 1-estimated datewise, 2-Job datewise list,3-Job datewise Closed,4-Job datewise Opened,5-Closed datewise Closed
                .Tp = rptCategory
                ds = .returnJob
            End With
        Else
            ds.Tables.Add(_dtRptTable)
        End If
        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = fMainForm
        frm.Text = "Job Summary"
        frm.Show()
    End Sub

    Private Sub fSelect_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fSelect.FormClosed
        fSelect = Nothing
    End Sub
    Private Sub fSelect_doSelect(ByVal KeyId As String, ByVal strFld1 As String, ByVal strFld2 As String) Handles fSelect.doSelect
        chgbyprg = True
        If _srchTxtId = 2 Then
            'txtsearch.Text = strFld1
            'txtsearch.Tag = KeyId
        End If
        chgbyprg = False
    End Sub

    Private Sub btnDelivery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelivery.Click
        If Val(btnDelivery.Tag) = 0 Then
            MsgBox("This user do not have permission to Delivery Entry", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If Not fDelivery Is Nothing Then
            fDelivery = Nothing
        End If
        fDelivery = New JobDelivery
        Dim jbDescription As String = ("Job Code:" & grdItem.Item("Jobcode", grdItem.CurrentRow.Index).Value & "\Job Name: " & grdItem.Item("Jobname", grdItem.CurrentRow.Index).Value)
        fDelivery.ldRec(Val(grdItem.Item("Jobid", grdItem.CurrentRow.Index).Value), jbDescription)
        fDelivery.ShowDialog()
    End Sub


    Private Sub rdoall_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoall.Click, rdoactive.Click, rdoclosed.Click, rdoparentjob.Click, rdosubprojects.Click
        Dim myctrl As New RadioButton
        myctrl = sender
        plclose.Visible = False
        Select Case myctrl.Name
            Case "rdoall"
                rptCategory = 15
            Case "rdoactive"
                rptCategory = 16
            Case "rdoclosed"
                If rdocloseddate.Checked Then
                    rptCategory = 18
                ElseIf rdojobdate.Checked Then
                    rptCategory = 17
                    plclose.Visible = True
                End If
            Case "rdosubprojects", "rdoparentjob"
                If rdoactive.Checked Then
                    rptCategory = 16
                ElseIf rdoclosed.Checked Then
                    If rdocloseddate.Checked Then
                        rptCategory = 18
                    ElseIf rdojobdate.Checked Then
                        rptCategory = 17
                        plclose.Visible = True
                    End If
                End If
        End Select
        ldJobdetails()
        setComboGrid()
        SetGrid()
    End Sub


    Private Sub rdodelivereditems_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim button As RadioButton
        button = sender
        If button.Checked Then
            Select Case button.Name
                Case "rdonotdelivered"
                    rptCategory = 12
                    Exit Select
                Case "rdodelivereditems"
                    rptCategory = 13
                    Exit Select
                Case "rdoReceivedItems"
                    rptCategory = 14
                    Exit Select
            End Select
            btnload_Click(btnload, New EventArgs)
        End If
    End Sub

    Private Sub ContractJobList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        loadTech()
        plclose.Visible = False
        Select Case rptCategory
            Case 15
                rdoactive.Checked = True
            Case 17, 18
                If rptCategory = 18 Then
                    rdocloseddate.Checked = True
                ElseIf 17 Then
                    rdojobdate.Checked = True
                End If
                rdoclosed.Checked = True
                plclose.Visible = True
        End Select
        cldrStartDate.Value = DateValue(DateFrom)
        ldJobdetails()
        If userType Then
            btnEdit.Tag = IIf(getRight(30, CurrentUser), 1, 0)
            btnDelivery.Tag = IIf(getRight(33, CurrentUser), 1, 0)
            btncloseJob.Tag = IIf(getRight(54, CurrentUser), 1, 0)
        Else
            btnEdit.Tag = 1
            btnDelivery.Tag = 1
            btncloseJob.Tag = 1
        End If
    End Sub

    Private Sub grdItem_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdItem.DoubleClick
        If grdItem.RowCount = 0 Then Exit Sub
        btnEdit_Click(btnEdit, New System.EventArgs)
    End Sub

    Private Sub grdItem_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItem.CellContentClick

    End Sub

    Private Sub rdoactive_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoactive.CheckedChanged

    End Sub

    Private Sub rdoparentjob_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoparentjob.CheckedChanged

    End Sub
End Class