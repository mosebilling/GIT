
Public Class FuelInvReportFrm
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fRptFormat As RptFormatfrm
    Private WithEvents fProductEnquiry As ItmEnqry
    Dim strsql As String
    Public ldType As String
    Public opt As Single
    Public isDoc As Boolean
    'WithEvents bsCustomers As New BindingSource
    Private cmbShowIndex As Integer
    Private chgbyprg As Boolean
    Private forSingle As Boolean
    Private dtTable As DataTable
    Dim MyCtrl As RadioButton
    Dim strSearch As String
    'object declarations
    Dim _objcmnbLayer As New clsCommon_BL
    Private itmDatatable As DataTable
    Private _objReport As New clsReport_BL

    Private Sub setComboGrid()
        chgbyprg = True
        Dim i As Integer = 0
        cmbSearch.Items.Clear()
        For i = 0 To grdvoucher.ColumnCount - 1
            cmbSearch.Items.Add(grdvoucher.Columns(i).HeaderText)

        Next
        If cmbSearch.Items.Count > 1 Then cmbSearch.SelectedIndex = 1
        'If cmbSearch.Items.Count >= cmbShowIndex Then cmbSearch.SelectedIndex = cmbShowIndex
        chgbyprg = False
    End Sub
    Private Function getWhereInv(Optional ByVal forGrid As Boolean = False) As String
        Dim strWhere As String = ""
        If forGrid Then GoTo forGrid
        If forSingle Then
            Dim SelectedTrID As Long
            SelectedTrID = Val(grdvoucher.Item(grdvoucher.ColumnCount - 1, grdvoucher.CurrentRow.Index).Value)
            getWhereInv = " and ItmInvCmntb.TrId= " & SelectedTrID
            Exit Function
        End If
forGrid:
        Select Case ldType
            Case "IP", "IS", "SR", "PR", "JIS", "TIS"
                strWhere = " Trdate>='" & Format(CDate(cldrStartDate.Value), "yyyy-MM-dd") & "' and  Trdate<='" & Format(CDate(cldrEnddate.Value), "yyyy-MM-dd") & "' "
            Case "PO", "MTN"
                strWhere = " Ddate>='" & Format(CDate(cldrStartDate.Value), "yyyy-MM-dd") & "' and  Ddate<='" & Format(CDate(cldrEnddate.Value), "yyyy-MM-dd") & "'"
        End Select
        If strWhere <> "" Then
            getWhereInv = " and " & strWhere
        Else
            getWhereInv = ""
        End If

        'End If
    End Function
    Public Sub fillGrid(ByVal optInv As Single)
        chgbyprg = True
        If ldType = "IS" Then
            lblName.Text = "Sales Invoice Report"
        End If

        txtSearch.Text = ""
        If IsDate(cldrStartDate.Value) And IsDate(cldrEnddate.Value) Then
            If CDate(cldrStartDate.Value) > CDate(cldrEnddate.Value) Then GoTo Invaliddate
        Else
Invaliddate:
            MsgBox("Invalid Date", MsgBoxStyle.Information)
            Exit Sub
        End If
        Select Case ldType
            Case "IS", "SR"
                strsql = "Select convert(bit,0) as Tag, convert(varchar(12), case when prefix=''  then '' else '-' end +  invNo ) as [Inv No] , TrDate [Tr.Date] ,Alias [Cust. Code]," & _
                        "CASE WHEN ISNULL(CashCustName,'')='' THEN AccDescr ELSE CashCustName END [Customer Name],(InvAmt-Discount)+rndoff [Amount],taxAmt [Tax Amount],(InvAmt-Discount)+rndoff+taxAmt [Net Total], OthCost [Sales Exp.],TrRefNo [Ref. No],TrDescription  [Tr. Description],[Job Code],LPO,UserId [Created By],TrId from ( select  prefix, invNo ,TrDate ,InvAmt,Discount,Discount1,OthCost,AccDescr ,TrRefNo,TrDescription,Alias ,UserId,[Job Code],ItmInvCmntb.Termsid,LPO,ItmInvCmntb.TrId,rndoff,taxAmt,CashCustName from ItmInvCmntb" & _
                         " LEFT JOIN (SELECT Trid,SUM((TrQty*(UnitCost+UnitOthCost))-ItemDiscount)InvAmt,sum(taxAmt) taxAmt FROM ItmInvTrTb GROUP BY Trid) Tr ON  ItmInvCmntb.Trid=Tr.Trid" & _
                         " INNER JOIN ( SELECT Trid Ftrid from ItmInvTrTb where isnull(FuleMeterid,0)>0 group by Trid) Ftr ON Ftr.Ftrid=ItmInvCmntb.Trid " & _
                         " left join Accmast on ItmInvCmnTb.CSCode=Accmast.accid where ItmInvCmntb.trtype='" & ldType & "'" & getWhereInv(True) & " ) as qq  order by TrDate ,InvNo"
                cmbShowIndex = 14
            Case "IP", "PR"
                strsql = "Select convert(bit,0) as Tag, convert(varchar(12), case when prefix=''  then '' else '-' end +  invNo ) as [Inv No] ,TrDate [Tr.Date] ,Alias [Sup. Code]," & _
                 "CASE WHEN ISNULL(CashCustName,'')='' THEN AccDescr ELSE CashCustName END as [Supplier Name],(InvAmt-Discount)+rndoff [Amount],taxAmt [Tax Amount],(InvAmt-Discount)+rndoff+taxAmt [Net Total],OthCost [Other Exp.],TrRefNo [Ref. No],TrDescription [Tr. Description],[Job Code],LPO,UserId [Created By],TrId from ( select  prefix, invNo ,TrDate ,InvAmt,Discount,Discount1,OthCost,AccDescr,TrRefNo,TrDescription,Alias ,UserId,[Job Code],ItmInvCmntb.Termsid,LPO,ItmInvCmntb.TrId,rndoff,taxAmt,CashCustName from ItmInvCmntb " & _
                         " LEFT JOIN (SELECT Trid,SUM((TrQty*(UnitCost+UnitOthCost))-ItemDiscount) InvAmt,sum(taxAmt) taxAmt FROM ItmInvTrTb GROUP BY Trid) Tr ON  ItmInvCmntb.Trid=Tr.Trid" & _
                         " left join Accmast on ItmInvCmnTb.CSCode=Accmast.accid where ItmInvCmntb.trtype='" & ldType & "'" & getWhereInv(True) & " ) as qq  order by TrDate ,InvNo"
                cmbShowIndex = 14
        End Select
        grdvoucher.DataSource = Nothing
        If strsql = "" Then
            cmbSearch.Items.Clear()
            GoTo SetHeadOnly
        End If
        dtTable = _objcmnbLayer._fldDatatable(strsql)
        grdvoucher.DataSource = dtTable
        setComboGrid()
SetHeadOnly:
        SetGridHead()
        chgbyprg = False
    End Sub
    Private Sub SetGridHead()
        For i = 0 To cmbSearch.Items.Count - 1
            With grdvoucher
                grdvoucher.Columns(i).ReadOnly = True
                .Columns(i).HeaderText = cmbSearch.Items(i).ToString
            End With
            grdvoucher.Columns(0).ReadOnly = False
        Next

        With grdvoucher
            SetGridProperty(grdvoucher)
            If ldType <> "MTN" And ldType <> "TIS" Then
                .Columns(0).Width = 50
                .Columns(.ColumnCount - 1).Visible = False
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Columns(1).Width = 75
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Columns(5).DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns(5).Frozen = True
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(6).DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(7).DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(8).DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End If
            .Columns(4).Width = 200
            .Columns("Tr. Description").Width = 300
        End With
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Dim RptType As String = ldType
        Dim RptCaption As String = ""
        If rdosummary.Checked Then
            RptType = ldType & "S"
            If chkdaybookSum.Checked Then
                RptType = "DBS"
            End If
        ElseIf rdopricelist.Checked Then
            RptType = "FPL"
        Else
            RptType = ldType & "SDF"
            If chkmeterwise.Checked Then
                RptType = "ISDM"
            End If
        End If
        fRptFormat = New RptFormatfrm
        fRptFormat.RptType = RptType
        fRptFormat.ShowDialog()
        fRptFormat = Nothing
    End Sub
    Private Function getwhere() As String
        Dim txtcondition As String
        Dim condition As String
        If Not isDoc Then
            condition = " WHERE Trdate>='" & Format(CDate(cldrStartDate.Value), "yyyy/MM/dd") & "' and Trdate<='" & Format(CDate(cldrEnddate.Value), "yyyy/MM/dd") & IIf(ldType = "TIS", "'", "' AND TrType='" & ldType & "'")
            Select Case ldType
                Case "TIS"
                    txtcondition = cmbSearch.Text
                Case Else
                    If cmbSearch.Text = "Customer Name" Then
                        txtcondition = "Supplier Name"
                    ElseIf cmbSearch.Text = "Cust. Code" Then
                        txtcondition = "Sup. Code"
                    Else
                        txtcondition = cmbSearch.Text
                    End If
            End Select
        Else
            condition = " WHERE [Doc.Date]>='" & Format(CDate(cldrStartDate.Value), "yyyy/MM/dd") & "' and [Doc.Date]<='" & Format(CDate(cldrEnddate.Value), "yyyy/MM/dd") & "' AND DocType='" & ldType & "'"

            If cmbSearch.Text = "Customer Name" Or cmbSearch.Text = "Supplier Name" Then
                txtcondition = "AccDescr"
            Else
                txtcondition = cmbSearch.Text
            End If

        End If
        If txtSearch.Text <> "" Then
            If chkSearch.Checked Then
                condition = condition & " AND [" & txtcondition & "] like '" & txtSearch.Text & "%'"
            Else
                condition = condition & " AND [" & txtcondition & "] like '%" & txtSearch.Text & "%'"
            End If
        End If
        If cmbMeter.Text <> "" Then
            If chkmeterwise.Checked Then
                condition = condition & " AND fcode='" & cmbMeter.Text & "'"
            End If
        End If
        If Val(txtitem.Tag) > 0 Then
            condition = condition & " AND itemid=" & Val(txtitem.Tag)
            'ElseIf txtitem.Tag <> "" Then
            '    condition = condition & " AND " & txtitem.Tag
        End If
        Return condition
    End Function
    Public Sub PrepareReport(ByVal FileName As String, ByVal RptCaption As String, ByVal forpint As Boolean)
        If Not frm Is Nothing Then
            frm.Close()
            frm = New ReprtviewNEWfrm
        Else
            frm = New ReprtviewNEWfrm
        End If
        Dim condition As String = getwhere()
        Dim dateFlds As String = "'" & Format(CDate(cldrStartDate.Value), "dd-MM-yyyy") & "' DateFrom ,'" & Format(CDate(cldrEnddate.Value), "dd-MM-yyyy") & "' DateTo"
        Dim ds As DataSet = Nothing

        Dim rptType As Integer
        rptType = 5
        If rdosummary.Checked Then
            If chkdaybookSum.Checked Then
                ds = _objReport.returnDaybook(0, CDate(cldrStartDate.Value), CDate(cldrEnddate.Value))
            Else
                ds = _objReport.returnDetailsForReport(condition, dateFlds, "returnInventoryDetailsForReport", rptType)
            End If
        ElseIf rdoitemwise.Checked Or rdopricelist.Checked Then
            ds = _objReport.returnDetailsForReport(condition, dateFlds, "returnInventoryItemwiseDetailsForReport", rptType)
        End If

        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = fMainForm
        frm.Text = RptCaption
        frm.Show()
    End Sub


    Private Sub InvReportFrm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not fRptFormat Is Nothing Then fRptFormat.Close() : fRptFormat = Nothing
        If Not frm Is Nothing Then frm.Close() : frm = Nothing
        'fMainForm.plHome.Visible = True
    End Sub

    Private Sub InvReportFrm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cldrStartDate.Value = Format(DateValue(Date.Now), DtFormat)
        cldrEnddate.Value = Format(Date.Now, DtFormat)
        'setUpGrid()
        ldBranch()
        ldFuelMeter()
        btnLoad_Click(btnLoad, New System.EventArgs)
        If ldType <> "IS" Then
            cmbMeter.Visible = False
            chkmeterwise.Visible = False
        End If
        Timer1.Enabled = True
        chgbyprg = False
    End Sub
    Private Sub ldFuelMeter()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT fcode FROM FuelMeterReadingTb")
        cmbMeter.Items.Clear()
        cmbMeter.Items.Add("")
        Dim i As Integer
        For i = 0 To dt.Rows.Count - 1
            cmbMeter.Items.Add(dt(i)("fcode"))
        Next
    End Sub
    'Private Sub setUpGrid()
    '    With grdvoucher
    '        '.ReadOnly = True
    '        .ColumnHeadersVisible = True
    '        .RowHeadersVisible = False
    '        .SelectionMode = DataGridViewSelectionMode.FullRowSelect
    '        .AllowUserToAddRows = False
    '        .AllowUserToDeleteRows = False
    '        .AllowUserToResizeRows = False
    '        .AllowUserToResizeColumns = True
    '        .ScrollBars = ScrollBars.Both
    '        .ColumnHeadersHeight = 250
    '        .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    '        .ColumnHeadersDefaultCellStyle.BackColor = Color.Olive
    '        .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.5!, System.Drawing.FontStyle.Regular)
    '        .RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.5!)
    '    End With
    'End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        If chgbyprg Then Exit Sub
        grdvoucher.DataSource = SearchGrid(dtTable, Trim(txtSearch.Text), cmbSearch.SelectedIndex, Not chkSearch.Checked)
        SetGridHead()
    End Sub

    Private Sub cmbSearch_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSearch.SelectedIndexChanged
        If chgbyprg Then Exit Sub
        txtSearch.Text = ""
        SetGridHead()
        txtSearch.Focus()
    End Sub

    Private Sub rdIS_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Me.IsInitializing Then Exit Sub
        If chgbyprg Then Exit Sub
        Dim MyCtrl As RadioButton = sender
        If MyCtrl.Checked = True Then
            opt = Val(MyCtrl.Tag)
            fillGrid(Val(MyCtrl.Tag))
        End If
    End Sub

    Private Sub chkDate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Me.IsInitializing Then Exit Sub
        fillGrid(opt)
    End Sub

    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        PrepareReport(RptFlName, RptCaption, forPrint)
    End Sub

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        fillGrid(opt)
    End Sub

    Private Sub ftnFP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        cldrStartDate.Value = Format(CDate(DateFrom), DtFormat)
        cldrEnddate.Value = Format(CDate(DateTo), DtFormat)
        btnLoad_Click(sender, e)
    End Sub

    Private Sub btnToday_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        cldrStartDate.Value = Format(CDate(Today), DtFormat)
        cldrEnddate.Value = Format(CDate(Today), DtFormat)
        btnLoad_Click(sender, e)
    End Sub




    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub ldBranch()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT * FROM BranchTb")
        cmbbranch.Items.Clear()
        cmbbranch.Items.Add("")
        Dim i As Integer
        For i = 0 To dt.Rows.Count - 1
            cmbbranch.Items.Add(dt(i)("BranchId"))
        Next
    End Sub


    Private Sub btnedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnedit.Click
        If ldType = "IS" Then 'IS
            fMainForm.LoadIS(Val(grdvoucher.Item(grdvoucher.ColumnCount - 1, grdvoucher.CurrentRow.Index).Value))
        ElseIf ldType = "IP" Then 'IP
            fMainForm.LoadIP(Val(grdvoucher.Item(grdvoucher.ColumnCount - 1, grdvoucher.CurrentRow.Index).Value))
        ElseIf ldType = "SR" Then 'SR
            fMainForm.LoadSR(Val(grdvoucher.Item(grdvoucher.ColumnCount - 1, grdvoucher.CurrentRow.Index).Value))
        ElseIf ldType = "PR" Then 'PR
            fMainForm.LoadPR(Val(grdvoucher.Item(grdvoucher.ColumnCount - 1, grdvoucher.CurrentRow.Index).Value))
        ElseIf ldType = "JIS" Then 'PR
            fMainForm.LoadJIS(Val(grdvoucher.Item(grdvoucher.ColumnCount - 1, grdvoucher.CurrentRow.Index).Value))
        ElseIf ldType = "MTN" Then 'MTN
            fMainForm.LoadGIN(Val(grdvoucher.Item(grdvoucher.ColumnCount - 1, grdvoucher.CurrentRow.Index).Value))
        Else
            Exit Sub
        End If
    End Sub

    Private Sub grdvoucher_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdvoucher.DoubleClick
        btnedit_Click(btnedit, New System.EventArgs)
    End Sub
    Private Sub ldDocumentItems(ByVal rIndex As Integer)
        Try
            itmDatatable = _objReport.returnFuelItemsToListForm("returnInventoryItemsToListForm", Val(grdvoucher.Item(grdvoucher.ColumnCount - 1, rIndex).Value), 5).Tables(0)
            grdItemView.DataSource = itmDatatable
            setItemGridHead()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub grdvoucher_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdvoucher.RowEnter
        ldDocumentItems(e.RowIndex)
    End Sub
    Private Sub setItemGridHead()
        With grdItemView
            SetGridProperty(grdItemView)
            .Columns(0).Width = 100
            .Columns(1).Width = 100
            .Columns(3).Width = 50
            .Columns("Meter Code").Width = 100
            .Columns(4).Width = 100
            .Columns(4).DefaultCellStyle.Format = "N" & 3
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(5).Width = 100
            .Columns(5).DefaultCellStyle.Format = "N" & 3
            .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(6).Width = 100
            .Columns(6).DefaultCellStyle.Format = "N" & 3
            .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(7).Width = 75
            .Columns(7).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(8).Width = 100
            .Columns(8).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(9).Width = 100
            .Columns(9).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("LineTotal").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("LineTotal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '.Columns(7).Visible = False
            .Columns("itemid").Visible = False
            'If ldType = "MTN" Then
            '    .Columns("UnitCost").Visible = False
            '    .Columns("Taxp").Visible = False
            '    .Columns("TaxAmt").Visible = False
            '    .Columns("LineTotal").Visible = False
            '    .Columns("TrQty").DefaultCellStyle.Format = "N" & NoOfDecimal
            '    .Columns("TrQty").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '    resizeGridColumn(grdItemView, 1)
            'ElseIf ldType = "TIS" Then
            '    .Columns("detid").Visible = False
            '    If .Tag = 0 Then
            '        Timer1.Enabled = True
            '        .Tag = 1
            '    Else
            '        resizeGridColumn(grdItemView, 1)
            '    End If
            'End If
            Dim i As Integer
            If cmbitmOrder.Items.Count = 0 Then
                For i = 0 To .ColumnCount - 2
                    cmbitmOrder.Items.Add(.Columns(i).HeaderText)
                Next
            End If

        End With
    End Sub

    Private Sub txtitemSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtitemSearch.TextChanged
        grdItemView.DataSource = SearchGrid(itmDatatable, Trim(txtitemSearch.Text), cmbitmOrder.SelectedIndex, Not chkitemSearchOnly.Checked)
        setItemGridHead()
    End Sub

    Private Sub cldrStartDate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cldrStartDate.KeyDown
        If e.KeyCode = Keys.Return Then
            cldrEnddate.Focus()
        End If
    End Sub
    Private Sub cldrEnddate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cldrEnddate.KeyDown
        If e.KeyCode = Keys.Return Then
            btnLoad_Click(btnLoad, New System.EventArgs)
        End If
    End Sub

    Private Sub rdosummary_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdosummary.Click, rdoitemwise.Click, rdopricelist.Click
        plitem.Visible = False
        txtitem.Text = ""
        txtitem.Tag = ""
        'chkItemtotal.Visible = False
        chkmeterwise.Visible = False
        cmbMeter.Visible = False
        chkdaybookSum.Visible = False
        If rdosummary.Checked Then
            'chkmeterwise.Text = "Sales Man wise"
            'cmbMeter.Visible = fals
            'If ldType = "IS" Then chkmeterwise.Visible = True
            chkdaybookSum.Visible = True
        ElseIf rdoitemwise.Checked Then
            chkmeterwise.Visible = True
            cmbMeter.Visible = True
            chkmeterwise.Visible = True
            plitem.Visible = True
        ElseIf rdopricelist.Checked Then
            plitem.Visible = True
        End If
    End Sub

    Private Sub chkItemtotal_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If ldType = "TIS" Then Exit Sub
        If chgbyprg Then Exit Sub
        chgbyprg = True
        If chkSearch.Visible Then chkmeterwise.Checked = False
        chgbyprg = False
    End Sub



    Private Sub txtitem_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtitem.KeyDown
        If e.KeyCode = Keys.F2 Then
            fProductEnquiry = New ItmEnqry
            fProductEnquiry.isVazhipadusales = IIf(ldType = "TIS", True, False)
            fProductEnquiry.ShowDialog()
        ElseIf e.KeyCode = Keys.Delete Then
            txtitem.Text = ""
            txtitem.Tag = ""
        End If
    End Sub


    Private Sub fProductEnquiry_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fProductEnquiry.FormClosed
        fProductEnquiry = Nothing
    End Sub

    Private Sub fProductEnquiry_getSelected(ByVal ItemcODE As String, ByVal isacc As Integer) Handles fProductEnquiry.getSelected
        Dim dt As DataTable
        Dim strMyQry As String
        If ldType = "TIS" Then
            strMyQry = "SELECT * FROM (SELECT [Item Code], InvItm.Description,ItemId,0 isacc FROM InvItm UNION ALL " & _
                                "SELECT Alias,AccDescr,AccId,1 FROM AccMast left join S1AccHd on S1AccHd.S1AccId=AccMast.S1AccId where GrpSetOn='Vazhipadu') Itm " & _
                                "where [Item Code] ='" & ItemcODE & "'"
        Else
            strMyQry = "SELECT [Item Code], InvItm.Description,ItemId FROM InvItm " & _
                                "where [Item Code] ='" & ItemcODE & "'"
        End If

        dt = _objcmnbLayer._fldDatatable(strMyQry)
        If dt.Rows.Count > 0 Then
            txtitem.Text = dt(0)("Description")
            txtitem.Tag = dt(0)("ItemId")
        End If
        fProductEnquiry.Close()
        'txtitem.Text = ItemcODE

    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub rdoitemwise_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoitemwise.CheckedChanged

    End Sub

    Private Sub rdosummary_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdosummary.CheckedChanged

    End Sub
End Class