﻿Public Class JobList
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
    Private Sub JobList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loadTech()
        Label2.Visible = True
        Panel2.Visible = False
        plclose.Visible = False
        If rptCategory = 6 Then
            txtsearch.Visible = True
            txtsearch.ReadOnly = True
            cmbtech.Visible = False
            Panel2.Visible = True
        ElseIf rptCategory = 7 Then
            txtsearch.Visible = True
            cmbtech.Visible = False
            Panel2.Visible = True
            txtsearch.ReadOnly = False
        ElseIf rptCategory = 8 Then
            txtsearch.Visible = False
            cmbtech.Visible = True
            Panel2.Visible = True
        ElseIf rptCategory = 9 Then
            txtsearch.Visible = True
            txtsearch.ReadOnly = False
            cmbtech.Visible = False
            Panel2.Visible = True
        ElseIf (rptCategory > 10) Then
            txtsearch.Visible = True
            txtsearch.ReadOnly = False
            cmbtech.Visible = False
            Panel2.Visible = False
            Panel3.Visible = False
            btnDelivery.Enabled = False
            btncloseJob.Enabled = False
            plDelivery.Visible = True
            plDelivery.Left = Panel2.Left

        Else
            txtsearch.Visible = False
            cmbtech.Visible = False
            Label2.Visible = False
            Panel2.Visible = False

            Select Case rptCategory
                Case 2
                    rdoall.Checked = True
                Case 4
                    rdoactive.Checked = True
                Case 3, 5
                    If rptCategory = 3 Then
                        rdocloseddate.Checked = True
                    ElseIf 5 Then
                        rdojobdate.Checked = True
                    End If
                    rdoclosed.Checked = True
                    plclose.Visible = True
            End Select
        End If
        
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
        If enableJobMaster Then
            btncloseJob.Visible = False
            btnEdit.Visible = False
            btnDelivery.Visible = False
        End If
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
                fMList.Search(txtsearch.Text)
                fMList.AssignList(txtsearch, lstKey, chgbyprg)
            Case 2   'Item name
                fMList.SearchIndex = 1
                fMList.SearchIndexDescr = 0
                fMList_doFocus()
                fMList.Search(txtsearch.Text)
                fMList.AssignList(txtsearch, lstKey, chgbyprg)
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
                txtsearch.Text = ItmFlds(0)
                txtsearch.Tag = ItmFlds(3)
            Case 2
                txtsearch.Text = ItmFlds(1)
                txtsearch.Tag = ItmFlds(2)
        End Select
        chgbyprg = False
    End Sub
    Private Sub loadTech()
        Dim dt As DataTable
        _objcmnbLayer = New clsCommon_BL
        dt = _objcmnbLayer._fldDatatable("Select SManCode from SalesmanTb")
        cmbtech.Items.Clear()
        Dim i As Integer
        For i = 0 To dt.Rows.Count - 1
            cmbtech.Items.Add(dt(i)(0))
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

            .Columns("Jobcode").HeaderText = "Job Code"
            .Columns("jobdate").HeaderText = "Job Date"
            .Columns("Jobname").HeaderText = "Job Name"
            .Columns("JobDescription").HeaderText = "Job Description"
            .Columns("AccDescr").HeaderText = "Customer Name"
            .Columns("ContactName").HeaderText = "Contact Name"
            .Columns("EstimatedDate").HeaderText = "Estimated Date"
            .Columns("EstimatedAmt").HeaderText = "Estimated Amount"
            .Columns("JobValue").HeaderText = "Job Value"
            .Columns("Userid").HeaderText = "Created By"
            .Columns("CrdtDate").HeaderText = "Created Date"
            .Columns("EstimatedAmt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("EstimatedAmt").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("JobValue").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("JobValue").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Datefrom").Visible = False
            .Columns("Dateto").Visible = False
            .Columns("lnk").Visible = False
            .Columns("jobid").Visible = False
            .Columns("ServiceCost").Visible = False
            .Columns("ItemCost").Visible = False
            .Columns("JobCloseDate").Visible = False
            .Columns("JobDescription").Width = 200
            .Columns("AccDescr").Width = 200
            .Columns("ContactName").Width = 200
            .Columns("EstimatedDate").Width = 150
            .Columns("EstimatedAmt").Width = 150
            .Columns("CrdtDate").Width = 150
            .Columns("Jobname").Frozen = True
            .Columns("Closed").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Closed").HeaderText = "Closed"

            If (Me.rptCategory = 10) Then
                .Columns.Item("EstimatedDate").Visible = False
                .Columns.Item("EstimatedAmt").Visible = False
                .Columns.Item("ContactName").Visible = False
                .Columns.Item("jobid").Visible = False
                .Columns.Item("JobDescription").Visible = False
                .Columns.Item("Phone").Visible = False
                .Columns.Item("ItemCost").Visible = False
                .Columns.Item("ServiceCost").Visible = False
                .Columns.Item("JobCloseDate").Visible = False
                .Columns.Item("Profit").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns.Item("Profit").DefaultCellStyle.Format = ("N" & NoOfDecimal)
                .Columns.Item("Profit %").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns.Item("Profit %").DefaultCellStyle.Format = ("N" & NoOfDecimal)
                .Columns.Item("LabourCost").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns.Item("LabourCost").DefaultCellStyle.Format = ("N" & NoOfDecimal)
                .Columns.Item("Item Cost").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns.Item("Item Cost").DefaultCellStyle.Format = ("N" & NoOfDecimal)
                .Columns.Item("TotCost").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns.Item("TotCost").DefaultCellStyle.Format = ("N" & NoOfDecimal)
                .Columns.Item("SManName").HeaderText = "Technician"
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
            .custcode = Val(txtsearch.Tag)
            .IMEI = IIf(rptCategory = 8, cmbtech.Text, txtsearch.Text)
            'rptCategory= 1-estimated datewise, 2-Job datewise list,3-Job datewise Closed,4-Job datewise Opened,5-Closed datewise Closed
            If rdoclosed.Checked Then
                If rdojobdate.Checked Then
                    rptCategory = 3
                Else
                    rptCategory = 5
                End If
            End If
            If rptCategory = 3 Or rptCategory = 5 Then
                .Closed = "YES"
            End If
            If rdoactive.Checked And Not chkdate.Checked Then
                rptCategory = 27
            End If
            
            .Tp = IIf(Not enableJobMaster And rptCategory = 9, 26, rptCategory)
            _vtable = .returnJob.Tables(0)
        End With
        grdItem.DataSource = Nothing
        grdItem.DataSource = _vtable
        'For active setgridhead
        If rptCategory = 27 Then rptCategory = 4
        If rptCategory > 10 Then
            setGridForDelivery()
        Else
            SetGrid()
        End If

    End Sub

    Private Sub btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnload.Click
        ldJobdetails()
        setComboGrid()
        _dtRptTable = Nothing
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
        If cmbOrder.Items.Count > 0 Then cmbOrder.SelectedIndex = 5
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        If enableJobMaster Then Exit Sub
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
        fMainForm.loadJob(Val(grdItem.Item("jobid", grdItem.CurrentRow.Index).Value))
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

    Private Sub txtsearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtsearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnload.Focus()
        ElseIf e.KeyCode = Keys.Up Or e.KeyCode = Keys.PageUp Then
            If fMList.Visible Then
                fMList.MoveUp(txtsearch.Text)
                Exit Sub
            End If
        ElseIf e.KeyCode = Keys.Down Or e.KeyCode = Keys.PageDown Then
            If Not fMList Is Nothing Then
                If fMList.Visible Then
                    fMList.MoveDown(txtsearch.Text)
                    Exit Sub
                End If
            End If
        ElseIf e.KeyCode = Keys.F2 Then
            If rptCategory = 6 Then
                _srchTxtId = 2
                ldSelect(1)
            End If
        End If
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

    Private Sub txtsearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtsearch.TextChanged
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

    Private Sub txtsearch_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtsearch.Validated
        If chgbyprg Then Exit Sub
        If txtsearch.Text = "" Then Exit Sub
        Dim dt As DataTable
        _objcmnbLayer = New clsCommon_BL
        If rptCategory = 6 Then
            dt = _objcmnbLayer._fldDatatable("SELECT AccMast.AccountNo from AccMast where AccDescr='" & txtsearch.Text & "'")
            If dt.Rows.Count > 0 Then
                txtsearch.Tag = dt(0)("AccountNo")
            Else
                txtsearch.Text = ""
            End If
        ElseIf rptCategory = 9 Then
            dt = _objcmnbLayer._fldDatatable("SELECT Itemid from invitm where Description='" & txtsearch.Text & "'")
            If dt.Rows.Count > 0 Then
                txtsearch.Tag = dt(0)("Itemid")
            Else
                txtsearch.Text = ""
            End If
        End If
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Dim RptType As String = ""
        Select Case rptCategory
            Case 2, 4
                RptType = "JBS1" ' Job datewise
            Case 1
                RptType = "JBS2" 'estimated datewise 
            Case 3
                RptType = "JBS3" 'Job datewise Closed
            Case 5
                RptType = "JBS5"
            Case 6
                RptType = "JBS6"
            Case 7
                RptType = "JBS7"
            Case 8
                RptType = "JBS8"
            Case 9
                RptType = "JBS9"
            Case 10
                RptType = "JBS10"
            Case 12, 13, 14
                RptType = "JBS11"
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
                .custcode = Val(txtsearch.Tag)
                .IMEI = IIf(rptCategory = 8, cmbtech.Text, txtsearch.Text)
                'rptCategory= 1-estimated datewise, 2-Job datewise list,3-Job datewise Closed,4-Job datewise Opened,5-Closed datewise Closed
                .Tp = rptCategory
                ds = .returnJob
            End With
        Else
            ds.Tables.Add(_dtRptTable)
        End If
        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = fMainForm
        frm.Text = RptCaption
        frm.Show()
    End Sub

    Private Sub fSelect_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fSelect.FormClosed
        fSelect = Nothing
    End Sub
    Private Sub fSelect_doSelect(ByVal KeyId As String, ByVal strFld1 As String, ByVal strFld2 As String) Handles fSelect.doSelect
        chgbyprg = True
        If _srchTxtId = 2 Then
            txtsearch.Text = strFld1
            txtsearch.Tag = KeyId
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


    Private Sub rdoall_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoall.Click, rdoactive.Click, rdoclosed.Click
        Dim myctrl As New RadioButton
        myctrl = sender
        plclose.Visible = False
        chkdate.Enabled = False
        chkdate.Checked = True
        Select Case myctrl.Name
            Case "rdoall"
                rptCategory = 2
            Case "rdoactive"
                rptCategory = 4
                chkdate.Checked = False
                chkdate.Enabled = True
            Case "rdoclosed"
                If rdojobdate.Checked Then
                    rptCategory = 3
                ElseIf rdocloseddate.Checked Then
                    rptCategory = 5
                    plclose.Visible = True
                End If
        End Select
        ldJobdetails()
        setComboGrid()
        SetGrid()
    End Sub


    Private Sub rdodelivereditems_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdodelivereditems.Click, rdonotdelivered.Click, rdoReceivedItems.Click
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

    Private Sub grdItem_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdItem.DoubleClick
        If grdItem.RowCount = 0 Then Exit Sub
        btnEdit_Click(btnEdit, New System.EventArgs)
    End Sub

    Private Sub grdItem_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItem.RowEnter
        If rptCategory <= 10 Then
            If grdItem.Item("Closed", e.RowIndex).Value = "Yes" Then
                btncloseJob.Enabled = False
            Else
                btncloseJob.Enabled = True
            End If
        End If
    End Sub


    Private Sub rdojobdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdojobdate.Click
        If rdojobdate.Checked Then
            rptCategory = 5
        Else
            rptCategory = 3
        End If
    End Sub

    Private Sub rdoclosed_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoclosed.CheckedChanged

    End Sub
End Class