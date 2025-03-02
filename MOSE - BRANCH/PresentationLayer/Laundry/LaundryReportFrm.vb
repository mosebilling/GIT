
Public Class LaundryReportFrm
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fRptFormat As RptFormatfrm
    Private WithEvents fProductEnquiry As ItmEnqry
    Private WithEvents fInvoice As MFSalesInvoice
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
        If cmbSearch.Items.Count > 1 Then cmbSearch.SelectedIndex = 4
        txtSearch.Focus()
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
        If chkenableDate.Checked Then
            Select Case ldType
                Case "SO", "DOC", "QTI", "MTN"
                    strWhere = " Ddate>='" & Format(CDate(cldrStartDate.Value), "yyyy-MM-dd") & "' and  Ddate<='" & Format(CDate(cldrEnddate.Value), "yyyy-MM-dd") & "'"
            End Select
        Else
            strWhere = " Ddate<='" & Format(CDate(DateTo), "yyyy-MM-dd") & "'"
        End If

        If chkPending.Checked Then
            strWhere = strWhere & " AND isnull(Totamt,0) -(ISNULL(PIAmt,0)+isnull(PDAmt,0))>0 "
        End If
        If strWhere <> "" Then
            getWhereInv = " and " & strWhere
        Else
            getWhereInv = ""
        End If

        'End If
    End Function
    Public Sub fillGrid(ByVal optInv As Single)
        chgbyprg = True

        If ldType = "QTI" Then
            lblName.Text = "Customer Quotation Report"
        ElseIf ldType = "SO" Then
            lblName.Text = "Sales Performa Invoice"
        ElseIf ldType = "PO" Then
            lblName.Text = "Purchase Order Report"
        ElseIf ldType = "DOC" Then
            lblName.Text = "Customer Delivery Report"
        ElseIf ldType = "MTN" Then
            lblName.Text = "Material Transfer Note"
        End If

        txtSearch.Text = ""
        If chkenableDate.Checked Then
            If IsDate(cldrStartDate.Value) And IsDate(cldrEnddate.Value) Then
                If CDate(cldrStartDate.Value) > CDate(cldrEnddate.Value) Then GoTo Invaliddate
            Else
Invaliddate:
                MsgBox("Invalid Date", MsgBoxStyle.Information)
                Exit Sub
            End If
        End If

        Dim shPending As String
        Dim shpendingFlds As String
        If chkPending.Checked Then
            '
            shPending = "LEFT JOIN (SELECT impDocid, Sum(UnitCost * (TrQty/PFraction)) PIAmt FROM ItmInvTrTb  GROUP BY impDocid) As PIQ ON PIQ.impDocid = DocCmnTb.DocId " & _
                        "LEFT JOIN (SELECT impDocid,Sum(CostPUnit * (Qty/PFraction)) As PDAmt FROM DocTranTb  GROUP BY impDocid) As PIQD ON PIQD.impDocid = DocCmnTb.DocId "

        Else
            shPending = ""
        End If
        shpendingFlds = "isnull(Totamt,0) [Doc Value],(ISNULL(PIAmt,0)+isnull(PDAmt,0)) Processed, isnull(Totamt,0) -(ISNULL(PIAmt,0)+isnull(PDAmt,0)) Balance,"
        Select Case ldType
            Case "SO"
                strsql = "Select convert(bit,0) as Tag,DNo [Doc No] , Ddate [Doc.Date] ," & _
                        "AccDescr [Customer Name],Phone,Reference [Ref. No],Comment  [Tr. Description],InvAmt [Amount]," & IIf(chkPending.Checked, shpendingFlds, "") & _
                        "UserId [Created By],Docid,CSCode " & _
                       " from ( select  DNo ,DDate ,InvAmt+rndoff InvAmt,Discount,AccDescr ,DocCmnTb.Reference,Comment,Alias ,DocCmnTb.UserId,DocCmnTb.jobcode,DocCmnTb.DocId,jobname,fjname,DocDefLoc,fcode,FromLoc " & _
                       IIf(chkPending.Checked, ",PIAmt,PDAmt,Totamt ", "") & ",CSCode,Phone from DocCmnTb" & _
                       " LEFT JOIN JobTb ON JobTb.jobcode=DocCmnTb.jobcode " & _
                       " LEFT JOIN (select jobcode fcode,jobname fjname from JobTb)fjb ON fjb.fcode=DocCmnTb.FromJob" & _
                         " LEFT JOIN (SELECT DocId,SUM(((Qty/PFraction)*CostPUnit)+taxAmt+CessAmt) InvAmt," & _
                         "SUM(((Qty/PFraction)*CostPUnit)) Totamt FROM DocTranTb GROUP BY DocId) Tr ON  DocCmnTb.Docid=Tr.Docid " & shPending & _
                         " left join Accmast on DocCmnTb.CSCode=Accmast.accid " & _
                         " left join AccMastAddr on AccMastAddr.AccountNo=Accmast.accid " & _
                         "where DocCmnTb.DocType='" & ldType & "'" & getWhereInv(True) & " ) as qq  order by Ddate ,Dno"

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
            Select Case ldType
                Case "SO"
                    .Columns("Customer Name").Width = 250
                    .Columns("Created By").Width = 100
                    .Columns("Doc No").Width = 80
                    .Columns("Doc.Date").Width = 70
                    .Columns("Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns("Amount").DefaultCellStyle.Format = "N" & NoOfDecimal
                    If chkPending.Checked Then
                        .Columns("Doc Value").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                        .Columns("Doc Value").DefaultCellStyle.Format = "N" & NoOfDecimal
                        .Columns("Processed").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                        .Columns("Processed").DefaultCellStyle.Format = "N" & NoOfDecimal
                        .Columns("Balance").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                        .Columns("Balance").DefaultCellStyle.Format = "N" & NoOfDecimal
                    End If
                    .Columns("Docid").Visible = False
                    .Columns(0).Visible = False
                    resizeGridColumn(grdvoucher, 6)
            End Select

        End With
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Dim RptType As String = ldType
        Dim RptCaption As String = ""
        If rdosummary.Checked Then
            RptType = ldType & "S"
        Else
            RptType = ldType & "D"
        End If
        If chkPending.Checked Then
            RptType = RptType & "P"
        End If
        If chkFormat.Checked Then
            fRptFormat = New RptFormatfrm
            fRptFormat.RptType = RptType
            fRptFormat.ShowDialog()
            fRptFormat = Nothing
        Else
            Dim RptName As String
            RptName = getRptDefFlName(RptType, RptCaption)
            If RptName <> "" Then
                PrepareReport(RptName, RptCaption, False)
            End If
        End If
        'fRptFormat = New RptFormatfrm
        'fRptFormat.RptType = RptType
        'fRptFormat.ShowDialog()
        'fRptFormat = Nothing
    End Sub
    Private Function getwhere() As String
        Dim txtcondition As String
        Dim condition As String
        condition = " WHERE [Doc.Date]>='" & Format(CDate(cldrStartDate.Value), "yyyy/MM/dd") & "' and [Doc.Date]<='" & Format(CDate(cldrEnddate.Value), "yyyy/MM/dd") & "' AND DocType='" & ldType & "'"
        If cmbSearch.Text = "Customer Name" Or cmbSearch.Text = "Supplier Name" Then
            txtcondition = "AccDescr"
        Else
            txtcondition = cmbSearch.Text
        End If
        If Val(txtitem.Tag) > 0 Then
            condition = condition & " AND itemid=" & Val(txtitem.Tag)
        End If
        If chkPending.Checked Then
            condition = condition & " AND isnull(Balance,0)>0 "
        End If
        If txtSearch.Text <> "" Then
            If chkSearch.Checked Then
                condition = condition & " AND [" & txtcondition & "] like '" & txtSearch.Text & "%'"
            Else
                condition = condition & " AND [" & txtcondition & "] like '%" & txtSearch.Text & "%'"
            End If
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
        Dim AccDate1 = Format(CDate(cldrStartDate.Value), "yyyy/MM/dd")
        Dim AccDate2 = Format(CDate(cldrEnddate.Value), "yyyy/MM/dd")
        Dim dateFlds As String = "'" & Format(CDate(cldrStartDate.Value), "dd-MM-yyyy") & "' DateFrom ,'" & Format(CDate(cldrEnddate.Value), "dd-MM-yyyy") & "' DateTo"
        Dim ds As DataSet = Nothing
        Dim tp As Integer
        If chkPending.Checked Then
            tp = 1
        Else
            tp = 0
        End If
        If rdosummary.Checked Then
            ds = _objReport.returnDetailsForReport(condition, dateFlds, "returnDocDetailsForReport", tp, "")
        ElseIf rdoitemwise.Checked Then
            ds = _objReport.returnDetailsForReport(condition, dateFlds, "returnDOCItemDetailsForReport", tp, "")
        End If
        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = fMainForm
        frm.Text = RptCaption
        frm.Show()
    End Sub


    Private Sub InvReportFrm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtSearch.Focus()
    End Sub

    Private Sub InvReportFrm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not fRptFormat Is Nothing Then fRptFormat.Close() : fRptFormat = Nothing
        If Not frm Is Nothing Then frm.Close() : frm = Nothing
        'fMainForm.plHome.Visible = True
    End Sub

    Private Sub InvReportFrm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cldrStartDate.Value = Format(DateValue(Date.Now), DtFormat)
        cldrEnddate.Value = Format(Date.Now, DtFormat)
        btnLoad_Click(btnLoad, New System.EventArgs)
        Me.Text = "ORDER LIST"
        Timer1.Enabled = True
    End Sub

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

    Private Sub btnedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnedit.Click
        If grdvoucher.RowCount = 0 Then Exit Sub
        If grdvoucher.CurrentRow Is Nothing Then Exit Sub
        fMainForm.LoadSOLaundry(Val(grdvoucher.Item("Docid", grdvoucher.CurrentRow.Index).Value), True)
    End Sub

    Private Sub grdvoucher_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdvoucher.DoubleClick
        btnedit_Click(btnedit, New System.EventArgs)
    End Sub
    Private Sub ldDocumentItems(ByVal rIndex As Integer)
        Try
            Dim qryTp As Integer
            If chkPending.Checked Then
                Select Case ldType
                    Case "QTI", "SO", "DOC"
                        qryTp = 1
                End Select
            Else
                Select Case ldType
                    Case "QTI", "SO", "DOC", "MTN"
                        qryTp = 0
                End Select
            End If
            itmDatatable = _objReport.returnItemsToListForm("returnDocumentItemsToListForm", Val(grdvoucher.Item("Docid", rIndex).Value), qryTp).Tables(0)
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
            .Columns("Item Code").Width = 100
            .Columns("TrDetail").Width = 200
            .Columns("Unit").Width = 50
            .Columns("Qty").Width = 75
            .Columns("Qty").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Qty").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("CostPUnit").Width = 100
            .Columns("CostPUnit").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("CostPUnit").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Tax Amount").Width = 100
            .Columns("Tax Amount").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Tax Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            If chkPending.Checked Then
                .Columns("Processed").Visible = True
                .Columns("Balance").Visible = True
                .Columns("Processed").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("Processed").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("Balance").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("Balance").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End If
            .Columns("LineTotal").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("LineTotal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("itemid").Visible = False
            Dim i As Integer
            If cmbitmOrder.Items.Count = 0 Then
                For i = 0 To .ColumnCount - 2
                    cmbitmOrder.Items.Add(.Columns(i).HeaderText)
                Next
            End If
            If Val(.Tag & "") = 0 Then
                Timer1.Enabled = True
                .Tag = 1
            Else
                resizeGridColumn(grdItemView, 1)
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

    Private Sub rdosummary_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdosummary.Click, rdoitemwise.Click
        plitem.Visible = False
        txtitem.Text = ""
        txtitem.Tag = ""
        If rdosummary.Checked Then
            plitem.Visible = False
        ElseIf rdoitemwise.Checked Then
            plitem.Visible = True
        End If
    End Sub
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        Select Case ldType
            Case "QTI", "SO", "DOC", "PO"
                resizeGridColumn(grdvoucher, 6)
                resizeGridColumn(grdItemView, 1)
            Case "MTN"
                resizeGridColumn(grdvoucher, 5)
                resizeGridColumn(grdItemView, 1)
        End Select
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
    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        fMainForm.LoadSOLaundry(, True)
    End Sub

    Private Sub btninvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btninvoice.Click
        If (Not fInvoice Is Nothing) Then
            fInvoice = Nothing
        End If
        fInvoice = New MFSalesInvoice
        fInvoice.MdiParent = fMainForm
        fInvoice.Show()
        Dim docid As Long
        Dim cscode As Integer
        If Not grdvoucher.CurrentRow Is Nothing Then
            docid = Val(grdvoucher.Item("Docid", grdvoucher.CurrentRow.Index).Value)
            cscode = Val(grdvoucher.Item("cscode", grdvoucher.CurrentRow.Index).Value)
        End If
        fInvoice.loadfromLaundry(docid, cscode)
    End Sub
    Private Sub chkenableDate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkenableDate.Click
        GroupBox1.Enabled = chkenableDate.Checked
    End Sub

    Private Sub grdvoucher_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdvoucher.CellContentClick

    End Sub
End Class