Imports DataAccessLayer
Public Class LoanBook
    Private _objReport As New clsReport_BL

    Private Sub txtInvNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtInvNo.KeyDown
        If e.KeyCode = Keys.Return Then
            loadLoan()
        End If

    End Sub
    Private Sub loadLoan()
        Dim dt As DataTable
        Dim ds As DataSet
        If txtInvNo.Text = "" Then Exit Sub
        ds = _objReport.LoadLoanBook(txtInvNo.Text)
        dt = ds.Tables(0)
        If (dt.Rows.Count > 0) Then
            txtInvNo.Text = dt(0)("TrRefNo")
            lbldate.Text = dt(0)("TrDate")
            lblcustomer.Text = dt(0)("CashCustName")
            lblphone.Text = dt(0)("customerPhone")
            lblloanamt.Text = Format(CDbl(dt(0)("Loanamnt")), numFormat)
            lbltunur.Text = Trim(dt(0)("tunername") & "")
            lblinstallment.Text = Format(CDbl(dt(0)("amount")), numFormat)
            lblperiod.Text = "FROM " & dt(0)("datefrom") & " TO " & dt(0)("dateto")
            lbltunurenddate.Text = dt(0)("dateto")
            lblinterest.Text = Format(CDbl(dt(0)("interst")), numFormat)
            lblloanwithinterest.Text = Format(dt(0)("NetAmt"), numFormat)
            If Not IsDBNull(dt(0)("JvDate")) Then
                lbllastinstallment.Text = dt(0)("JvDate")
            Else
                lbllastinstallment.Text = ""
            End If
            If Not IsDBNull(dt(0)("Latefee")) Then
                lbltotallatefee.Text = Format(CDbl(dt(0)("Latefee")), numFormat)
            Else
                lbltotallatefee.Text = Format(0, numFormat)
            End If
            If Not IsDBNull(dt(0)("totalPaid")) Then
                lblpaid.Text = Format(CDbl(dt(0)("totalPaid")), numFormat)
            Else
                lblpaid.Text = Format(0, numFormat)
            End If
            If Val(lblloanwithinterest.Text) = 0 Then lblloanwithinterest.Text = 0
            If Val(lbltotallatefee.Text) = 0 Then lbltotallatefee.Text = 0
            lblgross.Text = Format(CDbl(lblloanwithinterest.Text) + CDbl(lbltotallatefee.Text), numFormat)
            If Val(lblpaid.Text) = 0 Then lblpaid.Text = 0
            lbloutstanding.Text = Format(CDbl(lblgross.Text) - CDbl(lblpaid.Text), numFormat)
        End If
        grdVoucher.DataSource = ds.Tables(1)
        grdpayment.DataSource = ds.Tables(2)
        grdrestructure.DataSource = ds.Tables(4)
        dt = ds.Tables(3)
        If dt.Rows.Count > 0 Then
            lbloverduedays.Text = Val(dt(0)("overdue") & "")
            If Val(dt(0)("interestamt") & "") = 0 Then dt(0)("interestamt") = 0
            lbltotalsettlementinterest.Text = Format(CDbl(dt(0)("interestamt")), numFormat)
            If Val(dt(0)("restructurefee") & "") = 0 Then dt(0)("restructurefee") = 0
            lblrestructurefee.Text = Format(CDbl(dt(0)("restructurefee")), numFormat)
            If Val(lbloutstanding.Text) = 0 Then lbloutstanding.Text = 0
            lblsettlementAmt.Text = Format(CDbl(lbloutstanding.Text) + CDbl(lblrestructurefee.Text), numFormat)
            If Not IsDBNull(dt(0)("lastjvdate")) Then
                lbllastrestructuredate.Text = dt(0)("lastjvdate")
            Else
                lbllastrestructuredate.Text = ""
            End If
            lbllastrestructureJV.Text = Trim(dt(0)("lastjv") & "")
            lbllastrestructureJV.Tag = Trim(dt(0)("lastjvid") & "")
        Else
            lbloverduedays.Text = ""
            lbltotalsettlementinterest.Text = ""
            lblrestructurefee.Text = ""
            lblsettlementAmt.Text = ""
            lbllastrestructuredate.Text = ""
            lbllastrestructureJV.Text = ""
            lbllastrestructureJV.Tag = ""
        End If
        SetGridHead()
        SetGridHeadPayment()
        SetGridHeadRestructure()
    End Sub
    Private Sub makeclear()

        lbldate.Text = ""
        lblcustomer.Text = ""
        lblphone.Text = ""
        lblloanamt.Text = ""
        lbltunur.Text = ""
        lblinstallment.Text = ""
        lblperiod.Text = ""
        lbltunurenddate.Text = ""
        lblinterest.Text = ""
        lblloanwithinterest.Text = ""
        lbllastinstallment.Text = ""
        lbltotallatefee.Text = ""
        lblpaid.Text = ""
        lblloanwithinterest.Text = ""
        lbltotallatefee.Text = ""
        lblgross.Text = ""
        lblpaid.Text = ""
        lbloutstanding.Text = ""
        lbloverduedays.Text = ""
        lbltotalsettlementinterest.Text = ""
        lblrestructurefee.Text = ""
        lblsettlementAmt.Text = ""
        lbllastrestructuredate.Text = ""
        lbllastrestructureJV.Text = ""
        lbllastrestructureJV.Tag = ""
        grdpayment.Rows.Clear()
        grdVoucher.Rows.Clear()
        grdrestructure.Rows.Clear()
    End Sub
    Private Sub SetGridHead()
        With grdVoucher
            SetGridProperty(grdVoucher)

            .Columns("Rownum").HeaderText = "No"
            .Columns("Rownum").Width = 40

            .Columns("installmentdate").HeaderText = "Date"
            .Columns("installmentdate").Width = 100

            .Columns("amount").HeaderText = "Installment"
            .Columns("amount").Width = 100
            .Columns("amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("amount").DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns("firstemi").HeaderText = "Late Fee1"
            .Columns("firstemi").Width = 100
            .Columns("firstemi").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("firstemi").DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns("secondemi").HeaderText = "Late Fee2"
            .Columns("secondemi").Width = 100
            .Columns("secondemi").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("secondemi").DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns("rvamt").HeaderText = "RV Amount"
            .Columns("rvamt").Width = 100
            .Columns("rvamt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("rvamt").DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns("totaldue").HeaderText = "Total Due"
            .Columns("totaldue").Width = 100
            .Columns("totaldue").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("totaldue").DefaultCellStyle.Format = "N" & NoOfDecimal

            resizeGridColumn(grdVoucher, 1)
        End With
    End Sub
    Private Sub SetGridHeadPayment()
        With grdpayment
            SetGridProperty(grdpayment)

            .Columns("JVDate").HeaderText = "Date"
            .Columns("JVDate").Width = 100

            .Columns("jvnum").HeaderText = "RV No"
            .Columns("jvnum").Width = 100

            .Columns("AccDescr").HeaderText = "Payment By"
            .Columns("AccDescr").Width = 100

            .Columns("rvamt").HeaderText = "Amount"
            .Columns("rvamt").Width = 100
            .Columns("rvamt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("rvamt").DefaultCellStyle.Format = "N" & NoOfDecimal

            resizeGridColumn(grdpayment, 2)
        End With
    End Sub
    Private Sub SetGridHeadRestructure()
        With grdrestructure
            SetGridProperty(grdrestructure)

            .Columns("restructuredate").HeaderText = "Date"
            .Columns("restructuredate").Width = 100

            .Columns("jvnum").HeaderText = "JV No"
            .Columns("jvnum").Width = 100

            .Columns("outstanding").HeaderText = "Outstanding"
            .Columns("outstanding").Width = 100
            .Columns("outstanding").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("outstanding").DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns("interest").HeaderText = "Interest"
            .Columns("interest").Width = 100
            .Columns("interest").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("interest").DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns("restructurefee").HeaderText = "Restructure Amt"
            .Columns("restructurefee").Width = 100
            .Columns("restructurefee").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("restructurefee").DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns("Total").HeaderText = "Total"
            .Columns("Total").Width = 100
            .Columns("Total").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Total").DefaultCellStyle.Format = "N" & NoOfDecimal

            resizeGridColumn(grdrestructure, 2)
        End With
    End Sub
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub LoanBook_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Label18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label18.Click

    End Sub

    Private Sub btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnload.Click
        loadLoan()
    End Sub

    Private Sub TabControl2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl2.Click
        If TabControl2.SelectedIndex = 1 Then
            resizeGridColumn(grdpayment, 2)
        ElseIf TabControl2.SelectedIndex = 2 Then
            resizeGridColumn(grdrestructure, 2)
        End If
    End Sub
End Class