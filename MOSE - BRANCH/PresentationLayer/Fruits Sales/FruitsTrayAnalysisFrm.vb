Public Class FruitsTrayAnalysisFrm
    Private dtTable As DataTable
    Private _rptTable As DataTable
    Private _objreport As New clsReport_BL
    Private _objcommpnlayer As New clsCommon_BL
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fRptFormat As RptFormatfrm
    Private Sub btnexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexit.Click
        Me.Close()
    End Sub
    Private Sub loadCustomers()
        Dim dt As DataTable
        Dim tp As Integer
        If rdocustomer.Checked Then
            tp = 0
        Else
            tp = 3
        End If
        dt = _objreport.loadFruitsTrayOutstanding(DateValue(cldrStartDate.Value), DateValue(dtpto.Value), tp, 0, 0).Tables(0)
        grdcustomers.DataSource = dt
        setGridHead()

    End Sub
    Private Sub setGridHead()
        With grdcustomers
            SetGridProperty(grdcustomers)
            If rdocustomer.Checked Then
                .Columns("AccDescr").HeaderText = "Customer Name"
                .Columns("bal").HeaderText = "Balance"
                .Columns("bal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("bal").Width = 100
                .Columns("bal").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("accid").Visible = False
            End If
        End With
        setComboGrid()
    End Sub
    Private Sub setComboGrid()
        cmbOrder.Items.Clear()
        Dim i As Integer = 0
        Dim cnt As Integer = (grdcustomers.ColumnCount - 2)
        For i = 0 To cnt
            cmbOrder.Items.Add(grdcustomers.Columns.Item(i).HeaderText)
        Next
        If (cmbOrder.Items.Count > 0) Then
            cmbOrder.SelectedIndex = 0
        End If
    End Sub
    Private Sub loadTray(ByVal custid As Long)
        Dim dt As DataTable
        Dim tp As Integer
        If rdocustomer.Checked Then
            tp = 1
        Else
            tp = 4
        End If
        grdtray.DataSource = Nothing
        dt = _objreport.loadFruitsTrayOutstanding(DateValue(cldrStartDate.Value), DateValue(dtpto.Value), tp, custid, 0).Tables(0)
        grdtray.DataSource = dt
        With grdtray
            SetGridProperty(grdtray)
            .Columns("AccDescr").Visible = False
            .Columns("carriername").HeaderText = "Carrier Name"

            .Columns("QtyOut").HeaderText = IIf(tp = 1, "Issued", "Returned")
            .Columns("QtyOut").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("QtyOut").Width = 60
            .Columns("QtyOut").DefaultCellStyle.Format = "N0"

            .Columns("qtyin").HeaderText = IIf(tp = 1, "Returned", "Received")
            .Columns("qtyin").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("qtyin").Width = 60
            .Columns("qtyin").DefaultCellStyle.Format = "N0"

            .Columns("bal").HeaderText = "Balance"
            .Columns("bal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("bal").Width = 60
            .Columns("bal").DefaultCellStyle.Format = "N0"
            .Columns("carrierid").Visible = False
            resizeGridColumn(grdtray, 1)
        End With
        grdtransaction.DataSource = Nothing
        loadAdditionalTr(custid)
    End Sub
    Private Sub loadAdditionalTr(ByVal custid As Long)
        Dim dt As DataTable
        dt = _objcommpnlayer._fldDatatable("Select trtype,trdate,carcmnid from CarrierTrCmnTb where " & _
                                           "trdate>='" & Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd") & "'" & _
                                           "and trdate<='" & Format(DateValue(dtpto.Value), "yyyy/MM/dd") & "'" & _
                                           " and customerid=" & custid)
        grdadditional.DataSource = dt
        With grdadditional
            SetGridProperty(grdadditional)
            .Columns("trtype").HeaderText = "Type"
            .Columns("trtype").Width = 75
            .Columns("trdate").HeaderText = "Date"
            .Columns("carcmnid").Visible = False
            resizeGridColumn(grdadditional, 1)
        End With
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        loadCustomers()
        resizeGridColumn(grdcustomers, 0)
    End Sub

    Private Sub FruitsTrayAnalysisFrm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Timer1.Enabled = True
    End Sub

    Private Sub grdcustomers_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdcustomers.RowEnter
        Dim custid As Long
        custid = grdcustomers.Item("accid", e.RowIndex).Value
        loadTray(custid)
    End Sub
    Private Sub loadTransactions(ByVal custid As Long, ByVal carrierid As Long)
        Dim dt As DataTable
        Dim tp As Integer
        If rdocustomer.Checked Then
            tp = 2
        Else
            tp = 5
        End If
        dt = _objreport.loadFruitsTrayOutstanding(DateValue(cldrStartDate.Value), DateValue(dtpto.Value), tp, custid, carrierid).Tables(0)
        grdtransaction.DataSource = Nothing
        grdtransaction.DataSource = dt
        With grdtransaction
            SetGridProperty(grdtransaction)
            .Columns("TrRefNo").HeaderText = "Inv No"
            .Columns("trdate").HeaderText = "Date"
            .Columns("trdate").Width = 75
            .Columns("AccDescr").HeaderText = IIf(tp = 2, "Customer Name", "Supplier Name")
            .Columns("AccDescr").Width = 150
            .Columns("trRemarks").HeaderText = "Remarks"
            .Columns("trRemarks").Width = 150

            .Columns("QtyOut").HeaderText = IIf(tp = 2, "Issued", "Returned")
            .Columns("QtyOut").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("QtyOut").Width = 60
            .Columns("QtyOut").DefaultCellStyle.Format = "N0"

            .Columns("qtyin").HeaderText = IIf(tp = 2, "Returned", "Received")
            .Columns("qtyin").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("qtyin").Width = 60
            .Columns("qtyin").DefaultCellStyle.Format = "N0"
            .Columns("trid").Visible = False
            resizeGridColumn(grdtransaction, 3)
        End With
    End Sub

    Private Sub grdtray_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdtray.CellClick
        Dim custid As Long
        Dim carrierid As Long
        If grdcustomers.CurrentRow Is Nothing Then Exit Sub
        custid = grdcustomers.Item("accid", grdcustomers.CurrentRow.Index).Value
        carrierid = grdtray.Item("carrierid", e.RowIndex).Value
        loadTransactions(custid, carrierid)
    End Sub

    Private Sub rdocustomer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdocustomer.Click, rdosupplier.Click
        loadCustomers()
    End Sub

    Private Sub btnreturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreturn.Click
        If grdcustomers.CurrentRow Is Nothing Then Exit Sub
        Dim frm As New AddTrayOrReturnTrayFrm
        With frm
            .trtype = "RT"
            .loadedTrId = 0
            .accType = IIf(rdocustomer.Checked, 0, 1)
            .lblcustomer.Text = grdcustomers.Item(0, grdcustomers.CurrentRow.Index).Value
            .supplierid = grdcustomers.Item(2, grdcustomers.CurrentRow.Index).Value
            .loadCarriers()
            .ShowDialog()
            loadTray(grdcustomers.Item(2, grdcustomers.CurrentRow.Index).Value)
        End With
    End Sub

    Private Sub btnob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnob.Click
        If grdcustomers.CurrentRow Is Nothing Then Exit Sub
        Dim frm As New AddTrayOrReturnTrayFrm
        With frm
            .trtype = "OB"
            .loadedTrId = 0
            .accType = IIf(rdocustomer.Checked, 0, 1)
            .lblcustomer.Text = grdcustomers.Item(0, grdcustomers.CurrentRow.Index).Value
            .supplierid = grdcustomers.Item(2, grdcustomers.CurrentRow.Index).Value
            .loadCarriers()
            .ShowDialog()
            loadTray(grdcustomers.Item(2, grdcustomers.CurrentRow.Index).Value)
            Dim i As Integer
            Dim ttl As Integer
            For i = 0 To grdtray.RowCount - 1
                ttl = Val(grdtray.Item("bal", i).Value) + ttl
            Next
            grdcustomers.Item(1, grdcustomers.CurrentRow.Index).Value = ttl
        End With
    End Sub

    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        loadCustomers()
    End Sub

    Private Sub grdadditional_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdadditional.DoubleClick
        If grdcustomers.CurrentRow Is Nothing Then Exit Sub
        If grdadditional.RowCount = 0 Then Exit Sub
        If grdadditional.CurrentRow Is Nothing Then Exit Sub
        Dim frm As New AddTrayOrReturnTrayFrm
        With frm
            .trtype = grdadditional.Item(0, grdadditional.CurrentRow.Index).Value
            .loadedTrId = Val(grdadditional.Item(2, grdadditional.CurrentRow.Index).Value)
            .accType = IIf(rdocustomer.Checked, 0, 1)
            .lblcustomer.Text = grdcustomers.Item(0, grdcustomers.CurrentRow.Index).Value
            .supplierid = grdcustomers.Item(2, grdcustomers.CurrentRow.Index).Value
            .loadCarriers()
            .ShowDialog()
            loadTray(grdcustomers.Item(2, grdcustomers.CurrentRow.Index).Value)
            Dim i As Integer
            Dim ttl As Integer
            For i = 0 To grdtray.RowCount - 1
                ttl = Val(grdtray.Item("bal", i).Value) + ttl
            Next
            grdcustomers.Item(1, grdcustomers.CurrentRow.Index).Value = ttl
        End With
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Dim RptType As String = ""
        Dim RptCaption As String = ""
        RptType = "TOU"
        If chkFormat.Checked Then
            fRptFormat = New RptFormatfrm
            fRptFormat.RptType = RptType
            fRptFormat.ShowDialog()
            fRptFormat = Nothing
        Else
            Dim RptName As String
            RptName = getRptDefFlName(RptType, RptCaption)
            If RptName <> "" Then
                PrepareReport(RptName, "Outstanding", False)
            End If
        End If
    End Sub
    Public Sub PrepareReport(ByVal FileName As String, ByVal RptCaption As String, ByVal forpint As Boolean)
        If Not frm Is Nothing Then
            frm.Close()
            frm = New ReprtviewNEWfrm
        Else
            frm = New ReprtviewNEWfrm
        End If
        Dim ds As New DataSet
        'If Not _rptTable Is Nothing Then
        '    ds.Tables.Add(_rptTable)
        'Else
        '    Dim dtRpt = dtTable.Copy
        '    ds.Tables.Add(dtRpt)
        'End If
        Dim tp As Integer
        If rdocustomer.Checked Then
            tp = 6
        Else
            tp = 7
        End If
        ds = _objreport.loadFruitsTrayOutstanding(DateValue(cldrStartDate.Value), DateValue(dtpto.Value), tp, 0, 0)
        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = fMainForm
        frm.Text = RptCaption
        frm.Show()
    End Sub

    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        PrepareReport(RptFlName, "Outstanding", False)
    End Sub
    Private Sub fRptFormat_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fRptFormat.FormClosed
        fRptFormat = Nothing
    End Sub

    Private Sub btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnload.Click
        Dim rindex As Integer
        If Not grdcustomers.CurrentRow Is Nothing Then
            rindex = grdcustomers.CurrentRow.Index
        Else
            Exit Sub
        End If
        Dim custid As Long
        Dim carrierid As Long
        custid = grdcustomers.Item("accid", rindex).Value
        loadAdditionalTr(custid)
        carrierid = grdtray.Item("carrierid", 0).Value
        loadTransactions(custid, carrierid)
    End Sub

    Private Sub txtSeq_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeq.TextChanged
        grdcustomers.DataSource = SearchGrid(dtTable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
    End Sub
End Class