Public Class RVCollectionListFrm
    Private _objcommonlayer As New clsCommon_BL
    Private _objreport As New clsReport_BL
    Private dttable As DataTable
    Private rpttable As DataTable
    Private WithEvents fPpayment As CollectionRVComparisonFrm
    Private WithEvents fRptFormat As RptFormatfrm
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fwait As WaitMessageFrm
    Private Sub RVCollectionListFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Timer1.Enabled = True
        cmbtype.Tag = 1
        cmbtype.SelectedIndex = 0
        cmbtype.Tag = 0

    End Sub
    Private Sub loadGrid()
        Dim tp As Integer
        If cmbtype.Text = "Hidden" Then
            tp = 1
        Else
            tp = 0
        End If
        dttable = _objreport.returnRVCollectionList(DateValue(dtpdate.Value), tp).Tables(0)
        If chkpending.Checked Then
            Dim dt As DataTable = filterPending()
            dvData.DataSource = dt
        Else
            dvData.DataSource = dttable
        End If
        setGridHead()
        setComboGrid()
        gridTotal(dttable)
        changecolor()
        btnPreview.Enabled = True
    End Sub
    Private Sub changecolor()
        Dim i As Integer
        For i = 0 To dvData.RowCount - 1
            With dvData
                If Val(.Item("todayRV", i).Value & "") = 0 Then .Item("todayRV", i).Value = 0
                If Val(.Item("installment", i).Value & "") = 0 Then .Item("installment", i).Value = 0
                If CDbl(.Item("todayRV", i).Value) = 0 Then
                    .Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
                ElseIf CDbl(.Item("todayRV", i).Value) < CDbl(.Item("installment", i).Value) Then
                    .Rows(i).DefaultCellStyle.BackColor = Color.LightGreen
                ElseIf CDbl(.Item("todayRV", i).Value) <> CDbl(.Item("todayOnlineCollection", i).Value) Then
                    .Rows(i).DefaultCellStyle.BackColor = Color.LightYellow
                End If
            End With
        Next
    End Sub
    Private Sub setGridHead()
        With dvData
            SetGridProperty(dvData)
            .Columns("invprefix").HeaderText = "Inv No"
            .Columns("invprefix").Width = 75
            .Columns("trdate").HeaderText = "Date"
            .Columns("trdate").Width = 75
            .Columns("accountname").HeaderText = "Customer Name"
            .Columns("accountname").Width = 150

            .Columns("InvAmt").HeaderText = "Invoiced"
            .Columns("InvAmt").Width = 100
            .Columns("InvAmt").SortMode = DataGridViewColumnSortMode.Automatic
            .Columns("InvAmt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("InvAmt").DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns("RVAmt").HeaderText = "Received"
            .Columns("RVAmt").Width = 100
            .Columns("RVAmt").SortMode = DataGridViewColumnSortMode.Automatic
            .Columns("RVAmt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("RVAmt").DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns("balance").HeaderText = "Balance"
            .Columns("balance").Width = 100
            .Columns("balance").SortMode = DataGridViewColumnSortMode.Automatic
            .Columns("balance").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("balance").DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns("installment").HeaderText = "Installment"
            .Columns("installment").Width = 100
            .Columns("installment").SortMode = DataGridViewColumnSortMode.Automatic
            .Columns("installment").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("installment").DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns("todayRV").HeaderText = "Today RV"
            .Columns("todayRV").Width = 100
            .Columns("todayRV").SortMode = DataGridViewColumnSortMode.Automatic
            .Columns("todayRV").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("todayRV").DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns("cashamt").HeaderText = "RV Cash"
            .Columns("cashamt").Width = 100
            .Columns("cashamt").SortMode = DataGridViewColumnSortMode.Automatic
            .Columns("cashamt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("cashamt").DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns("otherAmt").HeaderText = "RV Online"
            .Columns("otherAmt").Width = 100
            .Columns("otherAmt").SortMode = DataGridViewColumnSortMode.Automatic
            .Columns("otherAmt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("otherAmt").DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns("collectionBal").HeaderText = "Today Bal."
            .Columns("collectionBal").Width = 100
            .Columns("collectionBal").SortMode = DataGridViewColumnSortMode.Automatic
            .Columns("collectionBal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("collectionBal").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("collectionBal").DefaultCellStyle.BackColor = Color.Cyan

            .Columns("todayOnlineCollection").HeaderText = "Collection"
            .Columns("todayOnlineCollection").Width = 100
            .Columns("todayOnlineCollection").SortMode = DataGridViewColumnSortMode.Automatic
            .Columns("todayOnlineCollection").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("todayOnlineCollection").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("todayOnlineCollection").DefaultCellStyle.BackColor = Color.LightGreen

            .Columns("cashCollection").HeaderText = "Cash"
            .Columns("cashCollection").Width = 100
            .Columns("cashCollection").SortMode = DataGridViewColumnSortMode.Automatic
            .Columns("cashCollection").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("cashCollection").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("cashCollection").DefaultCellStyle.BackColor = Color.LightGreen

            .Columns("otherCollection").HeaderText = "Online"
            .Columns("otherCollection").Width = 100
            .Columns("otherCollection").SortMode = DataGridViewColumnSortMode.Automatic
            .Columns("otherCollection").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("otherCollection").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("otherCollection").DefaultCellStyle.BackColor = Color.LightGreen


            '.Columns("totalonlinecollection").HeaderText = "Total Collected"
            '.Columns("totalonlinecollection").Width = 120
            '.Columns("totalonlinecollection").SortMode = DataGridViewColumnSortMode.Automatic
            '.Columns("totalonlinecollection").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '.Columns("totalonlinecollection").DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns("creditCount").HeaderText = "Count"
            .Columns("creditCount").Width = 50
            .Columns("creditCount").SortMode = DataGridViewColumnSortMode.Automatic
            .Columns("creditCount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("creditCount").DefaultCellStyle.Format = "N0"


            .Columns("trid").Visible = False
            .Columns("AccId").Visible = False
            .Columns("gno").Visible = False

            .Columns("cdate").Visible = False
            .Columns("lnk").Visible = False
        End With
        'resizeGridColumn(dvData, 2)
    End Sub
    Private Sub setComboGrid()
        Dim i As Integer = 0
        cmbOrder.Items.Clear()
        For i = 0 To dvData.ColumnCount - 1 'IIf(dvData.ColumnCount - 1 >= cmbShowIndex, cmbShowIndex, dvData.ColumnCount - 1)
            cmbOrder.Items.Add(dvData.Columns(i).HeaderText)
        Next
        cmbOrder.SelectedIndex = 2
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        loadGrid()

    End Sub
    Private Sub gridTotal(ByVal dt As DataTable)
        lbltotalInvoice.Text = Format(0, numFormat)
        lblreceived.Text = Format(0, numFormat)
        lbltotalbalance.Text = Format(0, numFormat)
        lblinstallment.Text = Format(0, numFormat)
        lblcollection.Text = Format(0, numFormat)
        lblbalancecollection.Text = Format(0, numFormat)

        Dim amt As String
        Dim dblAmount As Double
        amt = Trim(dt.Compute("SUM(InvAmt)", "") & "")
        If Val(amt) > 0 Then
            dblAmount = Convert.ToDouble(amt)
            lbltotalInvoice.Text = Format(dblAmount, numFormat)
        End If
        amt = ""
        dblAmount = 0
        amt = Trim(dt.Compute("SUM(RVAmt)", "") & "")
        If Val(amt) > 0 Then
            dblAmount = Convert.ToDouble(amt)
            lblreceived.Text = Format(dblAmount, numFormat)
        End If
        amt = ""
        dblAmount = 0
        amt = Trim(dt.Compute("SUM(balance)", "") & "")
        If Val(amt) > 0 Then
            dblAmount = Convert.ToDouble(amt)
            lbltotalbalance.Text = Format(dblAmount, numFormat)
        End If
        amt = ""
        dblAmount = 0
        amt = Trim(dt.Compute("SUM(installment)", "") & "")
        If Val(amt) > 0 Then
            dblAmount = Convert.ToDouble(amt)
            lblinstallment.Text = Format(dblAmount, numFormat)
        End If
        amt = ""
        dblAmount = 0
        amt = Trim(dt.Compute("SUM(todayRV)", "") & "")
        If Val(amt) > 0 Then
            dblAmount = Convert.ToDouble(amt)
            lblcollection.Text = Format(dblAmount, numFormat)
        End If
        amt = ""
        dblAmount = 0
        amt = Trim(dt.Compute("SUM(collectionBal)", "") & "")
        If Val(amt) > 0 Then
            dblAmount = Convert.ToDouble(amt)
            lblbalancecollection.Text = Format(dblAmount, numFormat)
        End If
        amt = ""
        dblAmount = 0
        amt = Trim(dt.Compute("SUM(todayOnlineCollection)", "") & "")
        If Val(amt) > 0 Then
            dblAmount = Convert.ToDouble(amt)
            lbltodaycollection.Text = Format(dblAmount, numFormat)
        End If
    End Sub

    Private Sub btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnload.Click
        loadWaite(1)
    End Sub
    Private Sub loadWaite(ByVal triggerType As Integer)
        If Not fwait Is Nothing Then fwait.Close() : fwait = Nothing
        fwait = New WaitMessageFrm
        With fwait
            .triggerType = triggerType
            .Show()
        End With

    End Sub

    Private Sub fwait_triggerEvent() Handles fwait.triggerEvent
        Select Case fwait.triggerType
            Case 1
                loadGrid()
        End Select
        If Not fwait Is Nothing Then
            fwait.Close()
            fwait = Nothing
        End If
    End Sub
    Private Function filterPending() As DataTable
        If dttable Is Nothing Then Return Nothing
        Dim _qurey As EnumerableRowCollection(Of DataRow)
        _qurey = From data In dttable.AsEnumerable() Where Val(data("collectionBal")) > 0 Select data
        If _qurey.Count > 0 Then
            rpttable = _qurey.CopyToDataTable()
        Else
            rpttable = dttable.Clone
        End If
        Return rpttable
    End Function

    Private Sub chkpending_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkpending.Click
        If chkpending.Checked Then
            filterPending()
            gridTotal(rpttable)
        Else
            dvData.DataSource = dttable
            gridTotal(dttable)
        End If
        changecolor()

    End Sub

    Private Sub btnexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexit.Click
        Me.Close()
    End Sub

    Private Sub btncomparison_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncomparison.Click
        If dvData.RowCount = 0 Then Exit Sub
        If dvData.CurrentRow Is Nothing Then Exit Sub

        loadComparison(dvData.CurrentRow.Index)
    End Sub
    Private Sub loadComparison(ByVal rindex As Integer)
        Dim AccountNo As Long
        Dim firstdate As Date
        Dim reference As String
       
        With dvData
            reference = .Item("invprefix", rindex).Value
            firstdate = DateValue(.Item("trdate", rindex).Value)
            AccountNo = Val(.Item("AccId", rindex).Value)
        End With
        If fPpayment Is Nothing Then
            fPpayment = New CollectionRVComparisonFrm
        End If

        With fPpayment
            .AccountNo = AccountNo
            .dateFrom = DateFrom
            .reference = reference
            .ldGrid(12)
            .Show()
            .Top = Me.Top + Screen.PrimaryScreen.WorkingArea.Height - .Height + 20
            .Left = Screen.PrimaryScreen.WorkingArea.Width - .Width - (.Width / 2)
        End With
    End Sub

    Private Sub fPpayment_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fPpayment.FormClosed
        fPpayment = Nothing
    End Sub

    Private Sub dvData_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dvData.RowEnter
        If fPpayment Is Nothing Then Exit Sub
        If fPpayment.Visible = False Then Exit Sub
        loadComparison(e.RowIndex)
    End Sub

    
    Private Sub btnrv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrv.Click
        If dvData.CurrentRow Is Nothing Then Exit Sub
        Dim i As Integer
        i = dvData.CurrentRow.Index
        fMainForm.LoadRV(0, dvData.Item("accountname", i).Value)
    End Sub

    Private Sub btnhide_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnhide.Click
        If dvData.CurrentRow Is Nothing Then Exit Sub
        Dim i As Integer
        i = dvData.CurrentRow.Index
        Dim trid As Long = Val(dvData.Item("trid", i).Value)
        If btnhide.Text = "Hide" Then
            If MsgBox("Do you want hide from List", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            _objcommonlayer._fldDatatable("update ItmInvCmnTb set ishideFromRVCollection=1 where trid=" & trid)
        Else
            If MsgBox("Do you want Undo", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            _objcommonlayer._fldDatatable("update ItmInvCmnTb set ishideFromRVCollection=0 where trid=" & trid)
        End If
        MsgBox("Done", MsgBoxStyle.Information)
        loadGrid()
    End Sub

    Private Sub cmbtype_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbtype.SelectedIndexChanged
        If Val(cmbtype.Tag) = 1 Then Exit Sub
        loadGrid()
        If btnhide.Text = "Hide" Then
            btnhide.Text = "Undo Hide"
        Else
            btnhide.Text = "Hide"
        End If
    End Sub

    Private Sub chkpending_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkpending.CheckedChanged

    End Sub

    Private Sub txtSeq_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeq.TextChanged
        dvData.DataSource = SearchGrid(dttable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
        rpttable = SearchGrid(dttable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
        changecolor()
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        If chkFormat.Checked Then
            fRptFormat = New RptFormatfrm
            fRptFormat.RptType = "RVCL"
            fRptFormat.ShowDialog()
            fRptFormat = Nothing
        Else
            PrepareRpt("RVCL")
        End If
    End Sub

    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        LoadReport(RptFlName, RptCaption, forPrint)
    End Sub
    Private Sub PrepareRpt(ByVal RptType As String, Optional ByVal Forprint As Boolean = False, Optional ByVal isF As Boolean = False)
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
        Dim ds As New DataSet
        If rpttable Is Nothing Then
            Dim tp As Integer
            If cmbtype.Text = "Hidden" Then
                tp = 1
            Else
                tp = 0
            End If
            ds = _objreport.returnRVCollectionList(DateValue(dtpdate.Value), tp)
            If chkpending.Checked Then
                filterPending()
                GoTo rpt
            End If
        Else
rpt:
            ds.Tables.Add(rpttable)
        End If
        frm.SetReport(ds, FileName, 0, False, , ToPrint)
        frm.MdiParent = Me.MdiParent
        frm.Text = RptCaption
        If Not ToPrint Then frm.Show()
    End Sub

    Private Sub lblcap_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblcap.Click

    End Sub
End Class