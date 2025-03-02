
Public Class WebInvReport
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fRptFormat As RptFormatfrm
    Private WithEvents fProductEnquiry As ItmEnqry
    Private WithEvents ftransfer As TransferToWebFrm
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
            Case "SOW"
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

        lblName.Text = "Sales Order Report from Online Store"

        txtSearch.Text = ""
        If IsDate(cldrStartDate.Value) And IsDate(cldrEnddate.Value) Then
            If CDate(cldrStartDate.Value) > CDate(cldrEnddate.Value) Then GoTo Invaliddate
        Else
Invaliddate:
            MsgBox("Invalid Date", MsgBoxStyle.Information)
            Exit Sub
        End If
        Select Case ldType
            Case "SOW"
                strsql = "Select convert(bit,0) as Tag,DNo [Doc No] , Ddate [Doc.Date] ,CustName [Customer Name]," & _
                        "(InvAmt-isnull(Discount,0))+isnull(rndoff,0) [Amount],Reference [Ref. No],Comment  [Tr. Description],[Job Code],UserId [Created By],Docid " & _
                        " from ( select   DNo ,DDate ,InvAmt,Discount,CustName ,Reference,Comment,UserId,jobcode [Job Code],DocCmnTb.DocId,rndoff,impDocid from DocCmnTb" & _
                          " LEFT JOIN (SELECT DocId,SUM((Qty/PFraction)*CostPUnit)InvAmt FROM DocTranTb GROUP BY DocId) Tr ON  DocCmnTb.Docid=Tr.Docid " & _
                          " left join CashCustomerTb on DocCmnTb.CSCode=CashCustomerTb.custid " & _
                          "left join (select impDocid from ItmInvTrTb) invTr on DocCmnTb.DocId=invTr.impDocid " & _
                          "where DocCmnTb.DocType='" & ldType & "' and isnull(impDocid,0)=0 " & getWhereInv(True) & " ) as qq  order by Ddate ,Dno"
        End Select
        grdvoucher.DataSource = Nothing
        grdItemView.DataSource = Nothing
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
            .Columns(0).Visible = False
            .Columns("Customer Name").Width = 250
            .Columns.Item("Tr. Description").Width = 300
            .Columns.Item("Tr. Description").Visible = False
            .Columns("amount").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns.Item("Docid").Visible = False
            'End If
        End With
        resizeGridColumn(grdvoucher, 3)
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Dim RptType As String = ldType
        Dim RptCaption As String = ""
        If rdosummary.Checked Then
            RptType = ldType & "S"
        Else
            RptType = ldType & "D"
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
                txtcondition = "CustName"
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
        If cmbsalesman.Text <> "" Then
            If chkserviceby.Checked Then
                condition = condition & " AND Smancode='" & cmbsalesman.Text & "'"
            End If
        End If
        If Val(txtitem.Tag) > 0 Then
            condition = condition & " AND itemid=" & Val(txtitem.Tag)
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
        If isDoc Then
            If rdosummary.Checked Then
                ds = _objReport.returnDetailsForReport(condition, dateFlds, "returnDocDetailsForReport", 0)
            ElseIf rdoitemwise.Checked Then
                ds = _objReport.returnDetailsForReport(condition, dateFlds, "returnDOCItemDetailsForReport", 0)
            End If
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
        ldsalesman()
        isDoc = True
        ldType = "SOW"
        btnLoad_Click(btnLoad, New System.EventArgs)
        Timer1.Enabled = True
        chgbyprg = False
        chkserviceby.Visible = False
        cmbsalesman.Visible = False
    End Sub
    Private Sub ldsalesman()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT SManCode FROM SalesmanTb")
        cmbsalesman.Items.Clear()
        cmbsalesman.Items.Add("")
        Dim i As Integer
        For i = 0 To dt.Rows.Count - 1
            cmbsalesman.Items.Add(dt(i)("SManCode"))
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
    Private Sub ldDocumentItems(ByVal rIndex As Integer)
        Try
            If chkPending.Checked Then
            Else
                Dim qryTp As Integer
                qryTp = 3
                itmDatatable = _objReport.returnItemsToListForm("returnInventoryItemsToListForm", Val(grdvoucher.Item(grdvoucher.ColumnCount - 1, rIndex).Value), qryTp).Tables(0)
                grdItemView.DataSource = itmDatatable
                setItemGridHead()
            End If

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
            .Columns(1).Width = 200
            .Columns(3).Width = 50
            .Columns(4).Width = 75
            .Columns(4).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(5).Width = 100
            .Columns(5).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(6).Width = 100
            .Columns(6).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("LineTotal").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("LineTotal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(7).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '.Columns(7).Visible = False
            .Columns("itemid").Visible = False
            .Columns("Taxp").Visible = False
            .Columns("TaxAmt").Visible = False
            '.Columns("LineTotal").Visible = False
            .Columns("TrQty").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("TrQty").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            resizeGridColumn(grdItemView, 1)
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

    Private Sub rdosummary_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdosummary.Click, rdoitemwise.Click
        plitem.Visible = False
        txtitem.Text = ""
        txtitem.Tag = ""
        chkItemtotal.Visible = False
        chkserviceby.Visible = False
        cmbsalesman.Visible = False
    End Sub

    Private Sub chkItemtotal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkItemtotal.Click
        If ldType = "TIS" Then Exit Sub
        If chgbyprg Then Exit Sub
        chgbyprg = True
        If chkSearch.Visible Then chkserviceby.Checked = False
        chgbyprg = False
    End Sub

    Private Sub chkserviceby_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkserviceby.CheckedChanged
        If ldType = "TIS" Then Exit Sub
        If chgbyprg Then Exit Sub
        chgbyprg = True
        chkItemtotal.Checked = False
        chgbyprg = False
    End Sub


    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        resizeGridColumn(grdvoucher, 3)
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
            If ldType = "TIS" Then
                If dt(0)("isacc") = 0 Then
                    chkItemtotal.Checked = False
                    chkItemtotal.Enabled = False
                    chkserviceby.Checked = True
                    chkserviceby.Enabled = True
                Else
                    chkItemtotal.Checked = True
                    chkItemtotal.Enabled = True
                    chkserviceby.Checked = False
                    chkserviceby.Enabled = False
                End If
            End If
        End If
        fProductEnquiry.Close()
        'txtitem.Text = ItemcODE

    End Sub

    Private Sub WebInvReport_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Timer1.Enabled = True
    End Sub

    Private Sub btnexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexport.Click
        ftransfer = New TransferToWebFrm
        ftransfer.typeofTransfer = 4
        ftransfer.Show(fMainForm)
    End Sub

    Private Sub ftransfer_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles ftransfer.FormClosed
        ftransfer = Nothing
    End Sub

    Private Sub grdvoucher_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdvoucher.CellContentClick

    End Sub

    Private Sub btnimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnimport.Click
        If grdvoucher.CurrentRow Is Nothing Then
            MsgBox("Please Select Document", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Dim dt As DataTable
        Dim docid As Long
        docid = Val(grdvoucher.Item("Docid", grdvoucher.CurrentRow.Index).Value)
        dt = _objcmnbLayer._fldDatatable("Select docid from DocCmnTb left join (select impDocid from ItmInvTrTb) invTr on DocCmnTb.DocId=invTr.impDocid where  isnull(impDocid,0)=0 and docid=" & docid)
        If dt.Rows.Count > 0 Then
            fMainForm.LoadISDoc(docid)
        Else
            MsgBox("Document already imported", MsgBoxStyle.Exclamation)
        End If

    End Sub
End Class