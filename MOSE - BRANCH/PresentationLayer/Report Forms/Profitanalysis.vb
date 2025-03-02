Public Class Profitanalysis
#Region "Class Objects"
    Private _objInv As New clsInvoice
#End Region
#Region "Local Variables"
    Private dtTable As DataTable
    Private RptdtTable As DataTable
#End Region
#Region "Form Objects"
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fRptFormat As RptFormatfrm
#End Region
    Private Sub Profitanalysis_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cldrStartDate.Value = Format(DateAdd(DateInterval.Day, firstDateFromToday * -1, DateValue(Date.Now)), DtFormat)
        ldGrid()
        Timer1.Enabled = True
    End Sub
    Private Sub ldGrid()
        dtTable = _objInv.returnProfitanalysisGrid(Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd"), Format(DateValue(cldrEnddate.Value), "yyyy/MM/dd"), 1).Tables(0)
        grdvoucher.DataSource = dtTable
        SetGridHead()
        gridTotal(dtTable)
    End Sub
    Private Sub gridTotal(ByVal RptdtTable As DataTable)
        If RptdtTable.Rows.Count = 0 Then Exit Sub
        Dim invoiceAmt As Double
        Dim totalSale As Double
        Dim totalDiscount As Double
        Dim netsale As Double
        Dim salescost As Double
        Dim profit As Double

        Dim amt As String
        amt = Trim(RptdtTable.Compute("SUM([Inv Amt])", "") & "")
        If Val(amt) > 0 Then
            invoiceAmt = Convert.ToDouble(amt)
        End If
        amt = Trim(RptdtTable.Compute("SUM([Total Sale])", "") & "")
        If Val(amt) > 0 Then
            totalSale = Convert.ToDouble(amt)
        End If
        amt = Trim(RptdtTable.Compute("SUM(Discount)", "") & "")
        If Val(amt) > 0 Then
            totalDiscount = Convert.ToDouble(amt)
        End If
        amt = Trim(RptdtTable.Compute("SUM([Net sale])", "") & "")
        If Val(amt) > 0 Then
            netsale = Convert.ToDouble(amt)
        End If
        amt = Trim(RptdtTable.Compute("SUM([Sales Cost])", "") & "")
        If Val(amt) > 0 Then
            salescost = Convert.ToDouble(amt)
        End If
        profit = netsale - salescost
        lblinvoiceamt.Text = Format(CDbl(invoiceAmt), numFormat)
        lbltotalsales.Text = Format(CDbl(totalSale), numFormat)
        lbldiscount.Text = Format(totalDiscount, numFormat)
        lblnetsales.Text = Format(netsale, numFormat)
        lblprofit.Text = Format(profit, numFormat)
        lblprofitPercentage.Text = Format(profit * 100 / IIf(netsale = 0, 100, netsale), numFormat)
        ''crSum = Convert.ToDouble(RptdtTable.Compute("SUM(Amount)", "Dtype='Cr'"))
        'lblTlDebit.Text = Format(drSum, numFormat)
        'lblTlCredit.Text = Format(crSum, numFormat)
        'lblDiff.Text = Format(CDbl(lblTlDebit.Text) - CDbl(lblTlCredit.Text), numFormat)
    End Sub

    Private Sub SetGridHead()
        With grdvoucher
            SetGridProperty(grdvoucher)
            .Columns("PreFix").Width = 75
            .Columns("InvNo").Width = 75
            .Columns("InvNo").HeaderText = "Inv No"
            .Columns("Trdate").Width = 75
            .Columns("Customer Name").Width = 200
            .Columns("Customer Name").HeaderText = "Customer Name"
            .Columns("SlsManId").Width = 100
            .Columns("SlsManId").HeaderText = "Sales Man"

            .Columns("Inv Amt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Inv Amt").Width = 100
            .Columns("Inv Amt").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Inv Amt").HeaderText = "Inv Amt"


            .Columns("Total Sale").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Total Sale").Width = 100
            .Columns("Total Sale").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Total Sale").HeaderText = "Total Sale"

            .Columns("Discount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Discount").Width = 100
            .Columns("Discount").DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns("Net Sale").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Net Sale").Width = 100
            .Columns("Net Sale").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Net Sale").HeaderText = "Net Sale"

            .Columns("Sales Cost").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Sales Cost").Width = 100
            .Columns("Sales Cost").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Sales Cost").HeaderText = "Sales Cost"

            .Columns("Profit").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Profit").Width = 75
            .Columns("Profit").DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns("Profit%").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Profit%").Width = 65
            .Columns("Profit%").DefaultCellStyle.Format = "N" & NoOfDecimal

            '.Columns("returnprice").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '.Columns("returnprice").Width = 100
            '.Columns("returnprice").DefaultCellStyle.Format = "N" & NoOfDecimal
            '.Columns("returnprice").HeaderText = "Return Price"
            '.Columns("returnprice").Visible = enableprofitanalysiswithreturn

            '.Columns("returncost").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '.Columns("returncost").Width = 100
            '.Columns("returncost").DefaultCellStyle.Format = "N" & NoOfDecimal
            '.Columns("returncost").HeaderText = "Return Cost"
            '.Columns("returncost").Visible = enableprofitanalysiswithreturn


            .Columns("DateFrom").Visible = False
            .Columns("DateTo").Visible = False
            .Columns("lnk").Visible = False
            Dim fc As String
            Dim i As Integer
            For i = 0 To .Columns.Count - 1
                fc = Mid(.Columns(i).HeaderText, 1, 1)
                .Columns(i).HeaderText = UCase(fc) & Mid(.Columns(i).HeaderText, 2)
            Next
        End With
        setComboGrid(grdvoucher)
    End Sub
    Private Sub setComboGrid(ByVal grd As DataGridView)
        cmbcolms.Items.Clear()
        Dim i As Integer = 0
        With grd
            For i = 0 To grd.ColumnCount - 1
                cmbcolms.Items.Add(.Columns(i).HeaderText)
            Next
            If cmbcolms.Items.Count > 0 Then cmbcolms.SelectedIndex = 1
        End With
    End Sub
    Private Sub setColwidth()
        If grdvoucher.Columns.Count = 0 Then Exit Sub
        Dim colwidth As Integer
        Dim colwidth1 As Integer
        Dim i As Integer
        For i = 4 To grdvoucher.ColumnCount - 1
            If grdvoucher.Columns(i).Visible = True Then
                colwidth = colwidth + grdvoucher.Columns(i).Width
            End If
        Next
        For i = 0 To 2
            If grdvoucher.Columns(i).Visible = True Then
                colwidth1 = colwidth1 + grdvoucher.Columns(i).Width
            End If
        Next
        colwidth = colwidth + colwidth1
        grdvoucher.Columns("TrDescription").Width = grdvoucher.Width - colwidth - 130
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        setColwidth()
    End Sub

    Private Sub Profitanalysis_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        'Timer1.Enabled = True
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        ldGrid()
    End Sub

    Private Sub txtSeq_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeq.TextChanged
        grdvoucher.DataSource = SearchGrid(dtTable, Trim(txtSeq.Text), cmbcolms.SelectedIndex, Not chkSearch.Checked)
        RptdtTable = SearchGrid(dtTable, Trim(txtSeq.Text), cmbcolms.SelectedIndex, Not chkSearch.Checked)
        gridTotal(RptdtTable)

    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Dim RptType As String = ""
        Dim RptCaption As String = ""
        If rdoInvoicewise.Checked Then
            RptType = "INP"
        ElseIf rdoItemwise.Checked Then
            RptType = "ITP"
        End If
        fRptFormat = New RptFormatfrm
        fRptFormat.RptType = RptType
        fRptFormat.ShowDialog()
        fRptFormat = Nothing
    End Sub

    Private Sub frm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles frm.FormClosed
        frm = Nothing
    End Sub

    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        PrepareReport(RptFlName, RptCaption, forPrint)
    End Sub
    Public Sub PrepareReport(ByVal FileName As String, ByVal RptCaption As String, ByVal forpint As Boolean)
        Dim ds As New DataSet
        If Not frm Is Nothing Then
            frm.Close()
            frm = New ReprtviewNEWfrm
        Else
            frm = New ReprtviewNEWfrm
        End If
        Dim tp As Integer
        Select Case True
            Case rdoInvoicewise.Checked
                tp = 1
                If RptdtTable Is Nothing Then
                    ds = _objInv.returnProfitanalysisGrid(Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd"), Format(DateValue(cldrEnddate.Value), "yyyy/MM/dd"), tp)
                Else
                    ds.Tables.Add(RptdtTable)
                End If
                RptdtTable = Nothing
            Case rdoItemwise.Checked
                tp = 2
                ds = _objInv.returnProfitanalysisGrid(Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd"), Format(DateValue(cldrEnddate.Value), "yyyy/MM/dd"), tp)
        End Select
        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = Me.MdiParent
        frm.Text = "Voucher Wise Report " & RptCaption
        frm.Show()
    End Sub

    Private Sub lblinvoiceamt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblinvoiceamt.Click

    End Sub
End Class