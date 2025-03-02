Public Class PaymentBookingFrm
    Private _objcmnbLayer As New clsCommon_BL
    Private _objpayroll As New clsPayroll
    Private _dtTable As DataTable
    Private chgbyprg As Boolean
    Private activecontrolname As String
    Private SrchText As String
    Private _objTr As New clsAccountTransaction
    Private WithEvents fExpense As ExpensePayments
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fRptFormat As RptFormatfrm
#Region "Constant Variables"
    Private Const ConstgSlNo = 0
    Private Const ConstgTag = 1
    Private Const ConstgEmpCode = 2
    Private Const ConstgEmpname = 3
    Private Const Constgsalarycategory = 4
    Private Const Constgadvance = 5
    Private Const Constgabsent = 6
    Private Const Constgholyday = 7
    Private Const ConstgUnits = 8
    Private Const ConstgRate = 9
    Private Const Constgallowance1 = 10
    Private Const Constgallowance2 = 11
    Private Const Constgworktotal = 12
    Private Const Constgdeduction = 13
    Private Const Constgdeduction1 = 14
    Private Const Constgdeduction2 = 15
    Private Const ConstgTotal = 16
    Private Const ConstgPaid = 17
    Private Const ConstgBal = 18
    Private Const ConstgempAcc = 19
    Private Const ConstgempAccid = 20
    Private Const ConstgEmpId = 21
    Private Const ConstgDetid = 22
    Private Const ConstgSheetDetid = 23
#End Region
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    'Private Sub PaymentBookingFrm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
    '    Timer1.Enabled = True
    'End Sub

    Private Sub PaymentBookingFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetGridHead()
        Dim dt As DataTable
        Dim qry As String
        If UsrBr = "" Then
            qry = "SELECT accid,Accdescr FROM AccMast WHERE AccSetId Like '%23%'"
        Else
            qry = " SELECT AccMast.accid,Accdescr FROM BranchAccSet left join AccMast on AccMast.accid=BranchAccSet.accid WHERE branchcode='" & UsrBr & "' and setno=" & 23
        End If

        dt = _objcmnbLayer._fldDatatable(qry)
        If dt.Rows.Count > 0 Then
            txtaccount.Tag = dt(0)("accid")
            txtaccount.Text = dt(0)("Accdescr")
        End If
        Dim dtdate As Date
        dtdate = DateValue("01/" & Month(dtpfrom.Value) & "/" & Year(dtpfrom.Value))
        dtpfrom.Value = dtdate
        dtdate = DateAdd(DateInterval.Month, 1, dtdate)
        dtpto.Value = DateAdd(DateInterval.Day, -1, dtdate)
        'Timer1.Enabled = True
    End Sub
    Private Sub SetGridHead()
        chgbyprg = True

        With grdVoucher

            SetEntryGridProperty(grdVoucher)
            .ColumnCount = 24

            .Columns(ConstgSlNo).HeaderText = "SlNo"
            .Columns(ConstgSlNo).Width = 40
            .Columns(ConstgSlNo).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstgSlNo).Frozen = True
            .Columns(ConstgSlNo).DefaultCellStyle.Format = "N0"
            .Columns(ConstgSlNo).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstgSlNo).ReadOnly = True
            .Columns(ConstgSlNo).DefaultCellStyle.BackColor = Color.AliceBlue

            .Columns(ConstgTag).HeaderText = "Tag"
            .Columns(ConstgTag).Width = 30
            .Columns(ConstgTag).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstgTag).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(ConstgTag).Visible = False

            .Columns(ConstgEmpCode).HeaderText = "Code"
            .Columns(ConstgEmpCode).Width = 100
            .Columns(ConstgEmpCode).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstgEmpCode).ReadOnly = True
            .Columns(ConstgEmpCode).Visible = False

            .Columns(ConstgEmpname).HeaderText = "Employee Name"
            .Columns(ConstgEmpname).Width = 200
            .Columns(ConstgEmpname).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstgEmpname).ReadOnly = True

            .Columns(Constgsalarycategory).HeaderText = "Category"
            .Columns(Constgsalarycategory).Width = 70
            .Columns(Constgsalarycategory).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(Constgsalarycategory).ReadOnly = True

            .Columns(Constgadvance).HeaderText = "Advance"
            .Columns(Constgadvance).Width = 75
            .Columns(Constgadvance).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(Constgadvance).ReadOnly = True
            .Columns(Constgadvance).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(Constgadvance).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(Constgabsent).HeaderText = "Absent"
            .Columns(Constgabsent).Width = 50
            .Columns(Constgabsent).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(Constgabsent).DefaultCellStyle.Format = "N" & 0
            .Columns(Constgabsent).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


            .Columns(ConstgUnits).HeaderText = "Units"
            .Columns(ConstgUnits).Width = 50
            .Columns(ConstgUnits).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstgUnits).DefaultCellStyle.Format = "N" & 0
            .Columns(ConstgUnits).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(ConstgRate).HeaderText = "Rate"
            .Columns(ConstgRate).Width = 60
            .Columns(ConstgRate).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstgRate).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstgRate).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstgRate).ReadOnly = True

            .Columns(Constgholyday).HeaderText = "H-Day"
            .Columns(Constgholyday).Width = 50
            .Columns(Constgholyday).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(Constgholyday).DefaultCellStyle.Format = "N" & 0
            .Columns(Constgholyday).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(Constgworktotal).HeaderText = "Total"
            .Columns(Constgworktotal).Width = 80
            .Columns(Constgworktotal).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(Constgworktotal).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(Constgworktotal).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(Constgworktotal).ReadOnly = True

            .Columns(Constgallowance1).HeaderText = "Allowance"
            .Columns(Constgallowance1).Width = 80
            .Columns(Constgallowance1).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(Constgallowance1).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(Constgallowance1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '.Columns(Constgallowance1).ReadOnly = True

            .Columns(Constgallowance2).HeaderText = "Commission"
            .Columns(Constgallowance2).Width = 80
            .Columns(Constgallowance2).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(Constgallowance2).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(Constgallowance2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(Constgdeduction).HeaderText = "Deduction"
            .Columns(Constgdeduction).Width = 80
            .Columns(Constgdeduction).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(Constgdeduction).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(Constgdeduction).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(Constgdeduction1).HeaderText = "Deduction 1"
            .Columns(Constgdeduction1).Width = 80
            .Columns(Constgdeduction1).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(Constgdeduction1).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(Constgdeduction1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(Constgdeduction2).HeaderText = "Deduction 2"
            .Columns(Constgdeduction2).Width = 80
            .Columns(Constgdeduction2).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(Constgdeduction2).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(Constgdeduction2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '.Columns(Constgdeduction).ReadOnly = True

            .Columns(ConstgTotal).HeaderText = "Net Amt"
            .Columns(ConstgTotal).Width = 80
            .Columns(ConstgTotal).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstgTotal).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstgTotal).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstgTotal).ReadOnly = True

            .Columns(ConstgPaid).HeaderText = "Paid"
            .Columns(ConstgPaid).Width = 80
            .Columns(ConstgPaid).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstgPaid).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstgPaid).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstgPaid).ReadOnly = True

            .Columns(ConstgBal).HeaderText = "Balance"
            .Columns(ConstgBal).Width = 80
            .Columns(ConstgBal).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstgBal).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstgBal).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstgBal).ReadOnly = True


            .Columns(ConstgempAcc).HeaderText = "Acc-Head"
            .Columns(ConstgempAcc).Width = 80
            .Columns(ConstgempAcc).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstgempAcc).ReadOnly = True

            .Columns(ConstgEmpId).Visible = False
            .Columns(ConstgempAccid).Visible = False
            .Columns(ConstgDetid).Visible = False
            .Columns(ConstgSheetDetid).Visible = False
        End With
        chgbyprg = False
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'Timer1.Enabled = False
        'If Val(dtpbookingdate.Tag) = 0 Then
        '    resizeGridColumn(grdVoucher, ConstgEmpname)
        'End If

    End Sub

    Private Sub grdVoucher_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellClick
        chgbyprg = True
        If e.RowIndex < 0 Then Exit Sub
        If grdVoucher.Item(ConstgTag, e.RowIndex).Value = "Y" Then grdVoucher.CurrentCell.ReadOnly = True
        Select Case e.ColumnIndex
            Case ConstgUnits, Constgallowance1, Constgallowance2, Constgdeduction, Constgholyday, Constgabsent, Constgdeduction1, Constgdeduction2
                If e.ColumnIndex = ConstgUnits Then
                    If e.ColumnIndex = rdosalary.Checked Then
                        grdVoucher.CurrentCell.ReadOnly = True
                    ElseIf (rdodaily.Checked Or rdounit.Checked) And chkloadfromdailysheet.Checked Then
                        grdVoucher.CurrentCell.ReadOnly = True
                    Else
                        grdVoucher.CurrentCell.ReadOnly = False
                    End If
                Else
                    grdVoucher.CurrentCell.ReadOnly = False
                End If
            Case Else
                grdVoucher.CurrentCell.ReadOnly = True
                Select Case e.ColumnIndex
                    Case ConstgTag
                        grdVoucher.Item(e.ColumnIndex, e.RowIndex).Value = IIf(grdVoucher.Item(e.ColumnIndex, e.RowIndex).Value = "Y", "", "Y")
                        chkselectall.Checked = False
                        calculatePayment()
                End Select
        End Select
        Dim i As Integer
        Dim foundEntry As Boolean
        For i = 0 To grdVoucher.RowCount - 1
            If grdVoucher.Item(ConstgTag, i).Value = "Y" And Val(lblnetAmt.Text) > 0 Then
                foundEntry = True
                Exit For
            End If
        Next
         btnpayment.Enabled =foundEntry
        chgbyprg = False
    End Sub

    Private Sub grdVoucher_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellEndEdit
        Dim col As Integer = e.ColumnIndex
        Dim ndec1 As Integer = 2
        chgbyprg = True
        If col = ConstgUnits Or col = Constgallowance1 Or col = Constgallowance2 _
        Or col = Constgdeduction Or col = Constgholyday Or col = Constgabsent Or col = Constgdeduction1 Or col = Constgdeduction2 Then
            If Val(grdVoucher.Item(col, e.RowIndex).Value) = 0 Then grdVoucher.Item(col, e.RowIndex).Value = 0
            If col = Constgholyday Or col = ConstgUnits Or col = Constgabsent Then ndec1 = 0
            grdVoucher.Item(col, e.RowIndex).Value = Format(Convert.ToDecimal(grdVoucher.Item(col, e.RowIndex).Value), "#,##0" & IIf(ndec1 = 0, "", "." & Strings.StrDup(ndec1, "0")))
        End If
        chgbyprg = False
    End Sub

    Private Sub grdVoucher_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellEnter
        If grdVoucher.Item(ConstgTag, e.RowIndex).Value = "Y" Then grdVoucher.CurrentCell.ReadOnly = True : Exit Sub
        If e.ColumnIndex = ConstgUnits Or e.ColumnIndex = Constgallowance1 _
        Or e.ColumnIndex = Constgallowance2 Or e.ColumnIndex = Constgdeduction _
        Or e.ColumnIndex = Constgdeduction1 Or e.ColumnIndex = Constgdeduction2 _
        Or e.ColumnIndex = Constgholyday Or e.ColumnIndex = Constgabsent Then
            If e.ColumnIndex = ConstgUnits Then
                If rdosalary.Checked Then
                    grdVoucher.CurrentCell.ReadOnly = True
                    Exit Sub
                ElseIf (rdodaily.Checked Or rdounit.Checked) And chkloadfromdailysheet.Checked Then
                    grdVoucher.CurrentCell.ReadOnly = True
                    Exit Sub
                Else
                    grdVoucher.CurrentCell.ReadOnly = False
                End If
            Else
                grdVoucher.CurrentCell.ReadOnly = False
            End If
            grdBeginEdit()
        End If
    End Sub
    Private Sub grdBeginEdit()
        chgbyprg = True
        grdVoucher.BeginEdit(True)
        chgbyprg = False
    End Sub

    Private Sub grdVoucher_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellValidated
        Select Case e.ColumnIndex
            Case ConstgUnits, Constgallowance1, Constgdeduction, Constgholyday, Constgabsent, Constgallowance2, Constgdeduction1, Constgdeduction2
                'If Val(grdVoucher.Item(e.ColumnIndex, e.RowIndex).Value) > 0 Then
                '    grdVoucher.Item(ConstgTag, e.RowIndex).Value = "Y"
                'End If
                If e.ColumnIndex = Constgabsent Or e.ColumnIndex = Constgholyday Then
                    If rdosalary.Checked Then
                        grdVoucher.Item(ConstgUnits, e.RowIndex).Value = Val(cldrdate.Tag) - (Val(grdVoucher.Item(Constgabsent, e.RowIndex).Value) + Val(grdVoucher.Item(Constgholyday, e.RowIndex).Value))
                    End If
                End If
                calculate()
                btnupdateAccounts.Enabled = False
        End Select


    End Sub

    Private Sub grdVoucher_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdVoucher.EditingControlShowing
        Dim Col As Integer = grdVoucher.CurrentCell.ColumnIndex
        If Col = Constgallowance1 Or Col = Constgallowance2 _
        Or Col = Constgdeduction Or Col = ConstgUnits Or Col = Constgholyday Or Col = Constgabsent _
         Or Col = Constgdeduction1 Or Col = Constgdeduction2 Then
            Dim tb As TextBox = CType(e.Control, TextBox)
            RemoveHandler tb.KeyPress, AddressOf Textbox_KeyPress
            AddHandler tb.KeyPress, AddressOf Textbox_KeyPress
        End If
    End Sub
    

    Private Sub grdVoucher_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.GotFocus
        activecontrolname = "grdVoucher"
    End Sub

    Private Sub grdVoucher_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdVoucher.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If grdVoucher.RowCount = 0 Then Exit Sub
                
                FindNextCell(grdVoucher, grdVoucher.CurrentCell.RowIndex, grdVoucher.CurrentCell.ColumnIndex + 1)
                'If grdVoucher.CurrentRow.Index = grdVoucher.RowCount - 1 Then
                '    grdVoucher.CurrentCell = grdVoucher.Item(ConstgRate, grdVoucher.CurrentRow.Index)
                'End If
nxt:

                grdBeginEdit()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub grdVoucher_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.Leave
        activecontrolname = ""
    End Sub
    Private Sub loadEmployees()
        Dim condition As String = ""
        cldrdate.Tag = ""
        Dim dtFrom As Date
        Dim dtTo As Date
        Dim salarycategory As Integer
        Dim tp As Integer
        Dim dt As DataTable
        chkselectall.Checked = False
        If Val(dtpbookingdate.Tag) > 0 Then
            _dtTable = _objpayroll.returnPayrollPaymentBooking(Val(dtpbookingdate.Tag), tp).Tables(0)
            dtFrom = DateValue("01/" & Month(cldrdate.Value) & "/" & Year(cldrdate.Value))
            dtTo = DateAdd(DateInterval.Month, 1, dtFrom)
            dtTo = DateAdd(DateInterval.Day, -1, dtTo)
            'lbldatefrom.Text = dtFrom
            'lbldateto.Text = dtTo
            cldrdate.Tag = DateDiff(DateInterval.Day, dtFrom, dtTo) + 1
        Else
            lblstatus.Text = "New"
            If rdodaily.Checked Then
                salarycategory = 0
                dtFrom = DateValue(cldrdate.Value)
                dtTo = DateValue(dtptodate.Value)
            ElseIf rdounit.Checked Then
                salarycategory = 1
                dtFrom = DateValue(cldrdate.Value)
                dtTo = DateValue(dtptodate.Value)
            Else
                salarycategory = 2
                dtFrom = DateValue("01/" & Month(cldrdate.Value) & "/" & Year(cldrdate.Value))
                dtTo = DateAdd(DateInterval.Month, 1, dtFrom)
                dtTo = DateAdd(DateInterval.Day, -1, dtTo)
                cldrdate.Tag = DateDiff(DateInterval.Day, dtFrom, dtTo) + 1
            End If
            'If salarycategory = 2 Then
            '    dt = _objcmnbLayer._fldDatatable("Select top 1 paymentid from PayrollPaymentCmnTb " & _
            '                     "where datefrom>='" & Format(dtFrom, "yyyy/MM/dd") & "' and dateto>='" & Format(dtTo, "yyyy/MM/dd") & "'")
            'Else
            '    dt = _objcmnbLayer._fldDatatable("Select top 1 paymentid from PayrollPaymentCmnTb " & _
            '                     "where paymentcategory=" & salarycategory & " and  dateto>='" & Format(DateValue(cldrdate.Value), "yyyy/MM/dd") & "'")
            'End If

            'If dt.Rows.Count > 0 Then
            '    If Val(dt(0)("paymentid")) > 0 Then
            '        'MsgBox("Payment already done for selected period", MsgBoxStyle.Exclamation)
            '        salarycategory = 3 'select blank list
            '    End If
            'End If
            _dtTable = _objpayroll.returnEmployeeForPaymentBooking(dtFrom, dtTo, salarycategory, tp).Tables(0)
            Dim _qurey As EnumerableRowCollection(Of DataRow)
            If chkloadfromdailysheet.Checked Then
                _qurey = From data In _dtTable.AsEnumerable() Where data("sheetunits") > 0 Select data
                If _qurey.Count > 0 Then
                    _dtTable = _qurey.CopyToDataTable()
                Else
                    _dtTable = _dtTable.Clone
                End If
            End If
        End If
        If grdVoucher.RowCount > 0 Then grdVoucher.Rows.Clear()
       
        If _dtTable.Rows.Count > 0 Then
            Dim i As Integer
            With grdVoucher
                For i = 0 To _dtTable.Rows.Count - 1
                    .Rows.Add("")
                    .Item(ConstgSlNo, i).Value = i + 1
                    .Item(ConstgEmpCode, i).Value = _dtTable(i)("empcode")
                    .Item(ConstgEmpname, i).Value = _dtTable(i)("empname")
                    .Item(Constgabsent, i).Value = _dtTable(i)("absentdays")
                    .Item(Constgholyday, i).Value = _dtTable(i)("holydays")
                    If (chkloadfromdailysheet.Checked Or Val(dtpbookingdate.Tag) > 0) Then
                        .Item(ConstgUnits, i).Value = Val(_dtTable(i)("sheetunits"))
                        If Val(_dtTable(i)("allowance2") & "") = 0 Then _dtTable(i)("allowance2") = 0
                        .Item(Constgallowance2, i).Value = Format(CDbl(_dtTable(i)("allowance2")), numFormat)
                        'If rdosalary.Checked Or Val(dtpbookingdate.Tag) > 0 Then

                        'Else
                        '    .Item(Constgallowance2, i).Value = 0
                        'End If
                    Else
                        .Item(ConstgUnits, i).Value = 0
                        .Item(Constgallowance2, i).Value = 0
                    End If
                    If Val(_dtTable(i)("salarycategory") & "") = 0 Then _dtTable(i)("salarycategory") = 0
                    If Val(_dtTable(i)("dailyPay") & "") = 0 Then _dtTable(i)("dailyPay") = 0

                    If chkloadfromdailysheet.Checked Then
                        If _dtTable(i)("salarycategory") = 2 Then
                            .Item(Constgsalarycategory, i).Value = "Salary"
                            If Val(.Item(ConstgUnits, i).Value) > 0 And Val(dtpbookingdate.Tag) = 0 Then
                                .Item(Constgabsent, i).Value = Format(Val(cldrdate.Tag) - CDbl(.Item(ConstgUnits, i).Value), "0")
                            ElseIf Val(.Item(ConstgUnits, i).Value) = 0 Then
                                .Item(ConstgUnits, i).Value = Format(Val(cldrdate.Tag), "0")
                            End If
                            .Item(ConstgRate, i).Value = Val(cldrdate.Tag) * CDbl(_dtTable(i)("unitRate"))
                            .Item(ConstgRate, i).Value = Format(CDbl(.Item(ConstgRate, i).Value), numFormat)
                        ElseIf _dtTable(i)("salarycategory") = 1 Then
                            .Item(Constgsalarycategory, i).Value = "Unitwise"
                            .Item(ConstgRate, i).Value = Format(CDbl(_dtTable(i)("unitRate")), numFormat)
                        Else
                            .Item(Constgsalarycategory, i).Value = "Daily Wages"
                            .Item(ConstgRate, i).Value = Format(CDbl(_dtTable(i)("unitRate")), numFormat)
                        End If
                    Else
                        If _dtTable(i)("salarycategory") = 2 Then
                            .Item(Constgsalarycategory, i).Value = "Salary"
                            If Val(.Item(ConstgUnits, i).Value) > 0 And Val(dtpbookingdate.Tag) = 0 Then
                                .Item(Constgabsent, i).Value = Format(Val(cldrdate.Tag) - CDbl(.Item(ConstgUnits, i).Value), "0")
                            ElseIf Val(.Item(ConstgUnits, i).Value) = 0 Then
                                .Item(ConstgUnits, i).Value = Format(Val(cldrdate.Tag), "0")
                            End If
                            .Item(ConstgRate, i).Value = Format(CDbl(_dtTable(i)("monthlyPay")), numFormat)
                        ElseIf _dtTable(i)("salarycategory") = 1 Then
                            .Item(Constgsalarycategory, i).Value = "Unitwise"
                            .Item(ConstgRate, i).Value = Format(CDbl(_dtTable(i)("dailyPay")), numFormat)
                        Else
                            .Item(Constgsalarycategory, i).Value = "Daily Wages"
                            .Item(ConstgRate, i).Value = Format(CDbl(_dtTable(i)("dailyPay")), numFormat)
                        End If
                    End If
                    .Item(Constgadvance, i).Value = Format(CDbl(_dtTable(i)("advance")), numFormat)
                    .Item(Constgallowance1, i).Value = Format(CDbl(_dtTable(i)("allowance")), numFormat)
                    '.Item(Constgallowance2, i).Value = Format(CDbl(_dtTable(i)("allowance2")), numFormat)
                    .Item(Constgdeduction, i).Value = Format(CDbl(_dtTable(i)("deduction")), numFormat)
                    .Item(Constgdeduction1, i).Value = Format(CDbl(_dtTable(i)("deduction1")), numFormat)
                    .Item(Constgdeduction2, i).Value = Format(CDbl(_dtTable(i)("deduction2")), numFormat)
                    .Item(ConstgempAcc, i).Value = _dtTable(i)("AccDescr")
                    .Item(ConstgempAccid, i).Value = _dtTable(i)("accid")
                    .Item(ConstgEmpId, i).Value = _dtTable(i)("empid")
                    .Item(ConstgDetid, i).Value = _dtTable(i)("detId")
                    If Val(dtpbookingdate.Tag) = 0 Then
                        'If Not IsDBNull(_dtTable(i)("fsheetdate")) And chkloadfromdailysheet.Checked Then
                        '    lbldatefrom.Text = _dtTable(i)("fsheetdate")
                        '    If Not rdosalary.Checked Then lbldateto.Text = _dtTable(i)("lsheetdate")
                        'End If
                        .Item(ConstgPaid, i).Value = Format(0, numFormat)
                    Else
                        .Item(ConstgPaid, i).Value = Format(CDbl(_dtTable(i)("paidAmt")), numFormat)
                    End If
                Next
            End With
        End If
        If Val(dtpbookingdate.Tag) > 0 Then
            btndelete.Enabled = True
            btnpreview.Enabled = True
            grdVoucher.Columns(ConstgTag).Visible = True
            grdVoucher.Columns(ConstgPaid).Visible = True
            grdVoucher.Columns(ConstgBal).Visible = True
            chkpayslip.Visible = True
            If rdosalary.Checked Then
                grdVoucher.Columns(Constgabsent).Visible = True
                grdVoucher.Columns(Constgholyday).Visible = True
                'grdVoucher.Columns(ConstgUnits).Visible = False
                grdVoucher.Columns(ConstgRate).HeaderText = "Salary"
                grdVoucher.Columns(ConstgRate).Width = 100
                grdVoucher.Columns(ConstgEmpname).Frozen = True
            Else
                grdVoucher.Columns(Constgabsent).Visible = False
                grdVoucher.Columns(Constgholyday).Visible = False
                grdVoucher.Columns(ConstgUnits).Visible = True
                grdVoucher.Columns(ConstgRate).HeaderText = "Rate"
                grdVoucher.Columns(ConstgRate).Width = 60
                grdVoucher.Columns(ConstgEmpname).Frozen = False
                'resizeGridColumn(grdVoucher, ConstgEmpname)
            End If
            If rdounit.Checked Then
                grdVoucher.Columns(ConstgUnits).HeaderText = "Units"
            Else
                grdVoucher.Columns(ConstgUnits).HeaderText = "Days"
            End If
        Else
            btndelete.Enabled = False
            btnpreview.Enabled = False
            grdVoucher.Columns(ConstgTag).Visible = False
            grdVoucher.Columns(ConstgPaid).Visible = False
            grdVoucher.Columns(ConstgBal).Visible = False
            chkpayslip.Visible = False
            'resizeGridColumn(grdVoucher, ConstgEmpname)
        End If
        BtnUpdate.Enabled = True
        calculate()
        calculatePayment()
    End Sub
    Private Sub calculate()
        Dim i As Integer
        Dim ttl As Double
        Dim ttlWork As Double
        Dim ttlUnits As Double
        Dim wtotal As Double
        For i = 0 To grdVoucher.RowCount - 1
            With grdVoucher
                If Val(.Item(ConstgUnits, i).Value) = 0 Then .Item(ConstgUnits, i).Value = 0
                If Val(.Item(ConstgRate, i).Value) = 0 Then .Item(ConstgRate, i).Value = 0
                ttlUnits = Val(.Item(ConstgUnits, i).Value) + Val(.Item(Constgholyday, i).Value)
                If .Item(Constgsalarycategory, i).Value = "Salary" Then
                    wtotal = ttlUnits * (CDbl(.Item(ConstgRate, i).Value) / Val(cldrdate.Tag))
                Else
                    wtotal = ttlUnits * CDbl(.Item(ConstgRate, i).Value)
                End If
                wtotal = wtotal + CDbl(.Item(Constgallowance1, i).Value) + CDbl(.Item(Constgallowance2, i).Value)
                .Item(Constgworktotal, i).Value = Format(wtotal, numFormat)
                If Val(.Item(Constgdeduction, i).Value) = 0 Then .Item(Constgdeduction, i).Value = 0
                If Val(.Item(Constgdeduction1, i).Value) = 0 Then .Item(Constgdeduction1, i).Value = 0
                If Val(.Item(Constgdeduction2, i).Value) = 0 Then .Item(Constgdeduction2, i).Value = 0
                wtotal = wtotal - CDbl(.Item(Constgdeduction, i).Value) - CDbl(.Item(Constgdeduction1, i).Value) - CDbl(.Item(Constgdeduction2, i).Value)
                .Item(ConstgTotal, i).Value = Format(wtotal, numFormat)
                ttl = CDbl(.Item(ConstgTotal, i).Value) + ttl
                ttlWork = ttlWork + CDbl(.Item(Constgworktotal, i).Value)
                .Item(ConstgBal, i).Value = Format(CDbl(.Item(ConstgTotal, i).Value) - CDbl(.Item(ConstgPaid, i).Value) - CDbl(.Item(Constgadvance, i).Value), numFormat)
            End With
        Next
        lblworkAmt.Text = Format(ttl, numFormat)
        lblworkAmt.Tag = Format(ttlWork, numFormat)
    End Sub
    Private Sub calculatePayment()
        Dim i As Integer
        Dim ttl As Double
        For i = 0 To grdVoucher.RowCount - 1
            With grdVoucher
                If .Item(ConstgTag, i).Value = "Y" Then
                    ttl = CDbl(.Item(ConstgBal, i).Value) + ttl
                End If
            End With
        Next
        lblnetAmt.Text = Format(ttl, numFormat)
    End Sub

    Private Sub btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnload.Click
        loadEmployees()
    End Sub


    Private Sub rdodaily_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdodaily.Click, rdosalary.Click, rdounit.Click

        If rdounit.Checked Then
            grdVoucher.Columns(ConstgUnits).HeaderText = "Units"
        Else
            grdVoucher.Columns(ConstgUnits).HeaderText = "Days"
        End If
        If rdosalary.Checked Then
            grdVoucher.Columns(ConstgRate).HeaderText = "Salary"
            grdVoucher.Columns(ConstgRate).Width = 100
            grdVoucher.Columns(Constgabsent).Visible = True
            grdVoucher.Columns(Constgholyday).Visible = True
            chkloadfromdailysheet.Text = "Load from Attendance"
            cldrdate.CustomFormat = "MMM/yyyy"
            Label6.Text = "Month"
            dtptodate.Enabled = False
        Else
            grdVoucher.Columns(ConstgRate).HeaderText = "Rate"
            grdVoucher.Columns(ConstgRate).Width = 60
            grdVoucher.Columns(Constgabsent).Visible = False
            grdVoucher.Columns(Constgholyday).Visible = False
            chkloadfromdailysheet.Text = "Load from Worksheet"
            cldrdate.CustomFormat = "dd/MM/yyyy"
            Label6.Text = "Date From"
            dtptodate.Enabled = True
        End If
        'resizeGridColumn(grdVoucher, ConstgEmpname)
        loadEmployees()
    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            Const WM_KEYDOWN As Integer = &H100
            Const WM_KEYUP As Integer = &H101
            'msg.ToString()
            If msg.Msg = WM_KEYDOWN Or msg.Msg = WM_KEYUP Then
                If msg.WParam.ToInt32() = CInt(Keys.F8) Then
                    'UpdateClick()
                ElseIf msg.WParam.ToInt32() = CInt(Keys.F5) Then
                    'ClearClick()
                ElseIf activecontrolname = "grdVoucher" Then
                    If msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up) Or msg.WParam.ToInt32() = CInt(Keys.Enter) Or msg.WParam.ToInt32() = CInt(Keys.F2) Or msg.WParam.ToInt32() = CInt(Keys.F3) Or msg.WParam.ToInt32() = CInt(Keys.F4) Or msg.WParam.ToInt32() = CInt(Keys.F6) Then
                        If (msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up)) Then GoTo ctn
                        grdVoucher_KeyDown(Nothing, New KeyEventArgs(keyData))
                        Return True
                    End If
                End If
            End If
ctn:
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function
    Private Sub Textbox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            Dim col As Integer
            col = grdVoucher.CurrentCell.ColumnIndex
            If col = ConstgUnits Or col = Constgallowance1 Or col = Constgdeduction Or col = Constgholyday Or col = Constgabsent Then
                If Not IsNumeric(e.KeyChar) And e.KeyChar <> "." And Not e.KeyChar = Convert.ToChar(Keys.Back) Then e.Handled = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub BtnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUpdate.Click
        If grdVoucher.RowCount = 0 Then
            MsgBox("Record not found", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If Val(lblworkAmt.Text) = 0 Then
            MsgBox("Payment not prepared", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Dim paymentcategory As Integer
        If rdodaily.Checked Then
            paymentcategory = 0
        ElseIf rdounit.Checked Then
            paymentcategory = 1
        Else
            paymentcategory = 2
        End If
        Dim paymentid As Long
        With _objpayroll
            .paymentid = Val(dtpbookingdate.Tag)
            If paymentcategory = 2 Then
                Dim dtdate As Date
                dtdate = DateValue("01/" & Month(cldrdate.Value) & "/" & Year(cldrdate.Value))
                .datefrom = dtdate
                dtdate = DateAdd(DateInterval.Month, 1, dtdate)
                dtdate = DateAdd(DateInterval.Day, -1, dtdate)
                .dateto = dtdate
            Else
                .datefrom = DateValue(cldrdate.Value)
                .dateto = DateValue(dtptodate.Value)
            End If
            .bookingdate = DateValue(dtpbookingdate.Value)
            .isloadedFromWorksheet = chkloadfromdailysheet.Checked
            .paymentcategory = paymentcategory
            .totalAmt = CDbl(lblworkAmt.Text)
            paymentid = Val(.savePayrollPaymentCmn())

        End With
        Dim i As Integer
        Dim empnames As String = ""
        With grdVoucher
            For i = 0 To .RowCount - 1
                If CDbl(.Item(Constgworktotal, i).Value) > 0 Then
                    _objpayroll.paymentdetid = Val(.Item(ConstgDetid, i).Value)
                    _objpayroll.paymentid = paymentid
                    _objpayroll.empid = Val(.Item(ConstgEmpId, i).Value)
                    If Val(.Item(Constgadvance, i).Value) = 0 Then .Item(Constgadvance, i).Value = 0
                    _objpayroll.advance = CDbl(.Item(Constgadvance, i).Value)
                    _objpayroll.absent = Val(.Item(Constgabsent, i).Value)
                    _objpayroll.holyday = Val(.Item(Constgholyday, i).Value)
                    _objpayroll.units = CDbl(.Item(ConstgUnits, i).Value)
                    If Val(.Item(Constgallowance1, i).Value) = 0 Then .Item(Constgallowance1, i).Value = 0
                    _objpayroll.allowance = CDbl(.Item(Constgallowance1, i).Value)
                    If Val(.Item(Constgdeduction, i).Value) = 0 Then .Item(Constgdeduction, i).Value = 0
                    _objpayroll.deduction = CDbl(.Item(Constgdeduction, i).Value)

                    If Val(.Item(Constgdeduction1, i).Value) = 0 Then .Item(Constgdeduction1, i).Value = 0
                    _objpayroll.deduction1 = CDbl(.Item(Constgdeduction1, i).Value)
                    If Val(.Item(Constgdeduction2, i).Value) = 0 Then .Item(Constgdeduction2, i).Value = 0
                    _objpayroll.deduction2 = CDbl(.Item(Constgdeduction2, i).Value)

                    If Val(.Item(ConstgTotal, i).Value) = 0 Then .Item(ConstgTotal, i).Value = 0
                    _objpayroll.netAmt = CDbl(.Item(ConstgTotal, i).Value)
                    _objpayroll.allowance2 = CDbl(.Item(Constgallowance2, i).Value)
                    If rdosalary.Checked Then
                        _objpayroll.unitRate = CDbl(.Item(ConstgRate, i).Value) / Val(cldrdate.Tag)
                    Else
                        _objpayroll.unitRate = CDbl(.Item(ConstgRate, i).Value)
                    End If
                    _objpayroll.savePayrollPaymentdet()
                    empnames = empnames & IIf(empnames = "", "", "/") & .Item(ConstgEmpname, i).Value
                End If
                If chkloadfromdailysheet.Checked Then
                    _objcmnbLayer._saveDatawithOutParm("Update PayrollWorksheetTb set transferid=" & paymentid & _
                                                              " where sheetempid=" & Val(.Item(ConstgEmpId, i).Value) & " and isnull(transferid,0)=0 and sheetcategory=" & paymentcategory & _
                                                              " and sheetdate>='" & Format(DateValue(cldrdate.Value), "yyyy/MM/dd") & _
                                                              "' and sheetdate<='" & Format(DateValue(dtptodate.Value), "yyyy/MM/dd") & "'")
                End If
            Next
        End With
        _objcmnbLayer._saveDatawithOutParm("Update PayrollPaymentCmnTb set txtdescription='" & Mid(empnames, 1, 500) & "' where paymentid=" & paymentid)
        MsgBox("Updated", MsgBoxStyle.Information)
        loadSaved(paymentid)
        btnupdateAccounts.Enabled = True
    End Sub
    Private Sub loadPaymentList()
        Dim condition As String
        If chkbookingdate.Checked Then
            condition = " where bookingdate>='" & Format(DateValue(dtpfrom.Value), "yyyy/MM/dd") & "' and bookingdate<='" & Format(DateValue(dtpto.Value), "yyyy/MM/dd") & "'"
        Else
            condition = " where datefrom>='" & Format(DateValue(dtpfrom.Value), "yyyy/MM/dd") & "' and dateto<='" & Format(DateValue(dtpto.Value), "yyyy/MM/dd") & "'"
        End If
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select datefrom [Date From],dateto [Date To],bookingdate [Booking Date]," & _
                                         "case when paymentcategory=0 then 'Wages' when paymentcategory=1 then 'Unitwise' when paymentcategory=2 then 'Salary' end [Category]," & _
                                         "totalAmt Amount,JVNum,txtdescription,paymentid,isloadedFromWorksheet from PayrollPaymentCmnTb left join acctrcmn on PayrollPaymentCmnTb.JvLinkNo=acctrcmn.linkno " & condition & " order by paymentid desc")
        grdlist.DataSource = dt
        SetGridProperty(grdlist)
        With grdlist
            .Columns("Booking Date").Width = 110
            .Columns("paymentid").Visible = False
            .Columns("isloadedFromWorksheet").Visible = False
            .Columns("Amount").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("Amount").DefaultCellStyle.Format = "N" & 2
            .Columns("Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        resizeGridColumn(grdlist, 6)
    End Sub

    Private Sub TabControl1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.Click
        If TabControl1.SelectedIndex = 1 Then
            loadPaymentList()
        End If
    End Sub

    Private Sub btnloadbooking_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnloadbooking.Click
        loadPaymentList()
    End Sub

    Private Sub grdlist_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdlist.DoubleClick
        If grdlist.RowCount = 0 Then Exit Sub
        loadSaved(Val(grdlist.Item("paymentid", grdlist.CurrentRow.Index).Value))
    End Sub
    Private Sub loadSaved(ByVal paymentid As Long)
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select datefrom,dateto,bookingdate," & _
                                         "paymentcategory,isnull(JVNum,0)JVNum,paymentid,isloadedFromWorksheet,isnull(linkno,0) JvLinkNo " & _
                                         "from PayrollPaymentCmnTb left join acctrcmn on PayrollPaymentCmnTb.JvLinkNo=acctrcmn.linkno WHERE paymentid=" & paymentid)
        If dt.Rows.Count > 0 Then
            dtpbookingdate.Tag = Val(dt(0)("paymentid"))
            cldrdate.Value = DateValue(dt(0)("datefrom"))
            dtptodate.Value = DateValue(dt(0)("dateto"))
            dtpbookingdate.Value = DateValue(dt(0)("bookingdate"))
            If dt(0)("isloadedFromWorksheet") = "True" Then
                chkloadfromdailysheet.Checked = True
            Else
                chkloadfromdailysheet.Checked = False
            End If
            cldrdate.Enabled = False
            dtptodate.Enabled = False
            txtjv.Text = dt(0)("JVNum")
            txtjv.Tag = dt(0)("JvLinkNo")
            lblstatus.Text = "SAVED"
            If dt(0)("paymentcategory") = 0 Then
                rdodaily.Checked = True
                rdounit.Enabled = False
                rdosalary.Enabled = False
                rdodaily.Enabled = True
                cldrdate.CustomFormat = "dd/MM/yyyy"
            ElseIf dt(0)("paymentcategory") = 1 Then
                rdounit.Checked = True
                rdounit.Enabled = True
                rdodaily.Enabled = False
                rdosalary.Enabled = False
                cldrdate.CustomFormat = "dd/MM/yyyy"
            Else
                rdounit.Enabled = False
                rdosalary.Enabled = True
                rdodaily.Enabled = False
                rdosalary.Checked = True
                cldrdate.CustomFormat = "MMM/yyyy"
            End If
        End If
        btnload.Enabled = False
        btndelete.Enabled = True
        btnclear.Enabled = True
        chkselectall.Visible = True
        loadEmployees()
        TabControl1.SelectedIndex = 0
        btnupdateAccounts.Enabled = True
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        clearControls()
    End Sub
    Private Sub clearControls()
        cldrdate.Value = DateValue(Date.Now)
        dtpbookingdate.Value = DateValue(Date.Now)
        dtpbookingdate.Tag = ""
        grdVoucher.Rows.Clear()
        btndelete.Enabled = False
        btnload.Enabled = True
        cldrdate.Enabled = True
        dtptodate.Enabled = True
        lblstatus.Text = "NEW"
        chkselectall.Visible = False
        rdodaily.Enabled = True
        rdounit.Enabled = True
        rdosalary.Enabled = True
        chkloadfromdailysheet.Checked = False
        loadEmployees()
        lblnetAmt.Text = "0.00"
        lblworkAmt.Text = "0.00"
        lblnetAmt.Tag = "0"
        txtjv.Text = ""
        txtjv.Tag = 0
        btnupdateAccounts.Enabled = False
        btnpayment.Enabled = False
    End Sub


    Private Sub chkloadfromdailysheet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkloadfromdailysheet.Click
        loadEmployees()
    End Sub

    Private Sub btnupdateAccounts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdateAccounts.Click
        If Val(txtjv.Tag) > 0 Then
            If MsgBox("Payment booking already saved for selected period" & vbCrLf & "Do you want to change it?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        End If
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select linkno from acctrcmn where payrollpaymentid=" & Val(dtpbookingdate.Tag))
        If dt.Rows.Count > 0 Then
            If MsgBox("Payment already done for selected period" & vbCrLf & "Do you want to change it?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        End If
        If txtaccount.Text = "" Then
            MsgBox("Salary Account not found", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If Val(lblworkAmt.Text) = 0 Then
            MsgBox("Valid entries not found", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Dim i As Integer
        With grdVoucher
            If Val(grdVoucher.Item(ConstgempAccid, i).Value & "") = 0 Then
                MsgBox("Invalid staff account for " & grdVoucher.Item(ConstgEmpname, i).Value, MsgBoxStyle.Exclamation)
                Exit Sub
            End If
        End With
        If Val(txtjv.Tag) = 0 Then
            dt = _objcmnbLayer._fldDatatable("SELECT InvNO from InvNos where InvType='JV'")
            If dt.Rows.Count > 0 Then
                txtjv.Text = dt(0)("InvNO")
            End If
chkagain:
            If Not CheckNoExists("", Val(txtjv.Text), "JV", "Accounts") Then
                txtjv.Text = Val(txtjv.Text) + 1
                GoTo chkagain
            End If
        End If
        Dim LinkNo As Long
        setAcctrCmnValue()
        LinkNo = Val(_objTr.SaveAccTrCmn())
        Dim entryRef As String
        If rdosalary.Checked Then
            entryRef = "Salary Booking for " & cldrdate.Text
        Else
            entryRef = "Payment booking for date from " & cldrdate.Value & " to " & dtptodate.Value
        End If
        Dim netamount As Double
        With grdVoucher
            For i = 0 To .RowCount - 1
                If Val(grdVoucher.Item(ConstgempAccid, i).Value & "") > 0 Then
                    netamount = netamount + CDbl(grdVoucher.Item(Constgworktotal, i).Value)
                End If
            Next

        End With
        _objcmnbLayer._saveDatawithOutParm("DELETE FROM AccTrDet WHERE LINKNO=" & LinkNo)
        setAcctrDetValue(LinkNo, Val(txtaccount.Tag), entryRef, netamount)
        With grdVoucher
            For i = 0 To .RowCount - 1
                If Val(grdVoucher.Item(Constgworktotal, i).Value & "") = 0 Or Val(grdVoucher.Item(ConstgempAccid, i).Value & "") = 0 Then GoTo nxt
                setAcctrDetValue(LinkNo, Val(grdVoucher.Item(ConstgempAccid, i).Value), entryRef, CDbl(grdVoucher.Item(Constgworktotal, i).Value) * -1)
nxt:
                'If Val(grdVoucher.Item(Constgdeduction, i).Value & "") = 0 Or Val(grdVoucher.Item(ConstgempAccid, i).Value & "") = 0 Then GoTo nxt1
                'entryRef = "Other Deduction"
                'setAcctrDetValue(LinkNo, Val(grdVoucher.Item(ConstgempAccid, i).Value), entryRef, CDbl(grdVoucher.Item(Constgdeduction, i).Value) * -1)
nxt1:
            Next
        End With
        _objcmnbLayer._saveDatawithOutParm("update PayrollPaymentCmnTb set JvLinkNo=" & LinkNo & "  WHERE paymentid=" & Val(dtpbookingdate.Tag))
        SetNextVrNo(txtjv, 0, "JV", "JvType = 'JV' AND JvNum = ", True, True, True)
        MsgBox("Updated", MsgBoxStyle.Information)
        loadSaved(Val(dtpbookingdate.Tag))
    End Sub

    Private Sub setAcctrCmnValue()
        _objTr.JVType = "JV"
        _objTr.JVDate = DateValue(dtpbookingdate.Value)
        _objTr.PreFix = ""
        _objTr.JVNum = Val(txtjv.Text)
        _objTr.JVTypeNo = getVouchernumber("JV")
        _objTr.UserId = CurrentUser
        _objTr.MchName = MACHINENAME
        _objTr.CrtDtTm = DateValue(Date.Now)
        _objTr.TypeNo = 0 ' id number from prefixtb
        _objTr.VrDescr = ""
        _objTr.IsModi = IIf(Val(txtjv.Tag) > 0, 2, 0)
        _objTr.LinkNo = Val(txtjv.Tag)
        _objTr.isLinkNo = True
    End Sub
    Private Sub setAcctrDetValue(ByVal lnkNo As Long, ByVal AccountNo As Long, ByVal EntryRef As String, ByVal DealAmt As Double)
        With _objTr
            .trLinkNo = lnkNo
            .AccountNo = AccountNo
            .Reference = "ON/AC"
            .EntryRef = EntryRef
            .DealAmt = DealAmt
            .FCAmt = DealAmt
            .JobCode = ""
            .JobStr = ""
            .CurrRate = 1
            .CurrencyCode = ""
            .TrInf = 0
            .OthCost = 0
            .TermsId = ""
            .CustAcc = 0
            .AccWithRef = 0
            .UnqNo = 0
            .BankCode = ""
            .ChqNo = ""
            .PDCAcc = 0
            .LPONo = ""
            'Dim dtDue As Date = IIf(chkDate(grdVoucher.Item(ConstDueDate, _row).Value), grdVoucher.Item(ConstDueDate, _row).Value, DateValue(grdVoucher.Item(ConstTrdate, _row).Value))
            .DocDate = Date.Now
            .SuppInvDate = Date.Now
            .DueDate = Date.Now
            .VesselId = ""
            .saveAccTrans()
        End With
    End Sub

    Private Sub grdVoucher_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellContentClick

    End Sub

    Private Sub btnpayment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpayment.Click
        Dim entryRef As String
        If fExpense Is Nothing Then
            fExpense = New ExpensePayments
            fExpense.MdiParent = fMainForm
            Dim netamount As Double
            With fExpense.grdVoucher
                fExpense.Show()
                Dim i As Integer
                If rdosalary.Checked Then
                    entryRef = "Salary payment for the month " & cldrdate.Text
                Else
                    entryRef = "Wages payment for the perriod from " & cldrdate.Value & " To " & dtptodate.Value
                End If
                fExpense.lblpayroll.Text = entryRef
                fExpense.lblpayroll.Tag = Val(dtpbookingdate.Tag)
                fExpense.lblpayroll.Visible = True
                For i = 0 To grdVoucher.RowCount - 1
                    If grdVoucher.Item(ConstgTag, i).Value = "Y" Then
                        If Val(grdVoucher.Item(ConstgempAccid, i).Value & "") > 0 Then
                            fExpense.chgbyprg = True
                            .Rows.Add(1)
                            .CurrentCell = .Item(0, .RowCount - 1)
                            .Item(4, .RowCount - 1).Value = "Dr"
                            .Item(3, .RowCount - 1).Value = entryRef
                            .Item(16, .RowCount - 1).Value = Val(grdVoucher.Item(ConstgempAccid, i).Value & "")
                            .Item(5, .RowCount - 1).Value = CDbl(grdVoucher.Item(ConstgBal, i).Value)
                            netamount = netamount + CDbl(grdVoucher.Item(ConstgBal, i).Value)
                            fExpense.ValidForPayrollPayment(.RowCount - 1)
                            fExpense.chgbyprg = False
                        End If
                    End If
                Next
            End With
            With fExpense.grdpayment
                .Item(5, .RowCount - 1).Value = netamount
                .Item(3, .RowCount - 1).Value = entryRef
            End With
            fExpense.assaignTotal()
        Else
            fExpense.Focus()
        End If
    End Sub

    Private Sub fExpense_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fExpense.FormClosed
        Timer1.Enabled = True
        loadEmployees()
        fExpense = Nothing
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        Dim dt As DataTable
        Dim JVlinkno As String = ""
        dt = _objcmnbLayer._fldDatatable("select isnull(JvLinkNo,0)JvLinkNo from PayrollPaymentCmnTb where paymentid=" & Val(dtpbookingdate.Tag))
        If dt.Rows.Count > 0 Then
            If dt(0)("JvLinkNo") > 0 Then
                If MsgBox("Found Payment Booking JV" & vbCrLf & "Do you want to remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                JVlinkno = dt(0)("JvLinkNo")
            End If
        End If
        dt = _objcmnbLayer._fldDatatable("select isnull(linkno,0)linkno from AccTrCmn where payrollpaymentid=" & Val(dtpbookingdate.Tag))
        Dim i As Integer
        If dt.Rows.Count > 0 Then
            If dt(0)("linkno") > 0 Then
                If MsgBox("Found Payment Voucher" & vbCrLf & "Do you want to remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                For i = 0 To dt.Rows.Count - 1
                    If JVlinkno = "" Then
                        JVlinkno = dt(0)("linkno")
                    Else
                        JVlinkno = JVlinkno & "," & dt(0)("linkno")
                    End If
                Next
            End If
        End If
        If JVlinkno <> "" Then
            _objcmnbLayer._saveDatawithOutParm("DELETE FROM AccTrCmn WHERE LINKNO IN(" & JVlinkno & ")")
            _objcmnbLayer._saveDatawithOutParm("DELETE FROM AccTrDet WHERE LINKNO IN(" & JVlinkno & ")")
        End If
        _objcmnbLayer._saveDatawithOutParm("DELETE FROM PayrollPaymentCmnTb WHERE paymentid=" & Val(dtpbookingdate.Tag))
        _objcmnbLayer._saveDatawithOutParm("DELETE FROM PayrollPaymentDetTb WHERE paymentid=" & Val(dtpbookingdate.Tag))
        _objcmnbLayer._saveDatawithOutParm("update  PayrollWorksheetTb set transferid=0 WHERE transferid=" & Val(dtpbookingdate.Tag))
        clearControls()
    End Sub

    Private Sub btnpreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpreview.Click
        Dim RptType As String = ""
        If chkpayslip.Checked Then
            If rdodaily.Checked Or rdounit.Checked Then
                RptType = "PSLW"
            Else
                RptType = "PSLS"
            End If
        Else
            If rdodaily.Checked Then
                RptType = "PWG"
            ElseIf rdounit.Checked Then
                RptType = "PWU"
            Else
                RptType = "PSAL"
            End If
        End If
        fRptFormat = New RptFormatfrm
        fRptFormat.RptType = RptType
        fRptFormat.ShowDialog()
        fRptFormat = Nothing
    End Sub
    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        PrepareReport(RptFlName, RptCaption, forPrint)
    End Sub
    Public Sub PrepareReport(ByVal FileName As String, ByVal RptCaption As String, ByVal forpint As Boolean)
        If Not frm Is Nothing Then
            frm.Close()
            frm = New ReprtviewNEWfrm
        Else
            frm = New ReprtviewNEWfrm
        End If
        Dim ds As New DataSet
        'If chkpayslip.Checked Then

        'End If
        If Not chkpayslip.Checked Then
            ds = _objpayroll.returnPayrollPaymentBooking(Val(dtpbookingdate.Tag), 0)
        Else
            If chkselectall.Checked Then
                ds = _objpayroll.returnPayrollPaymentBooking(Val(dtpbookingdate.Tag), 0)
                GoTo nxt

            End If
            Dim i As Integer
            Dim empids As String = ""
            Dim bDatatable As DataTable
            bDatatable = _objpayroll.returnPayrollPaymentBooking(Val(dtpbookingdate.Tag), 0).Tables(0)
            For i = 0 To grdVoucher.RowCount - 1
                If grdVoucher.Item(ConstgTag, i).Value = "Y" Then
                    bDatatable(i)("tag") = "Y"
                Else
                    bDatatable(i)("tag") = ""
                End If
            Next
            Dim _qurey As EnumerableRowCollection(Of DataRow)
            _qurey = From data In bDatatable.AsEnumerable() Where data("tag") = "Y" Select data
            If _qurey.Count > 0 Then
                bDatatable = _qurey.CopyToDataTable()
            Else
                bDatatable = bDatatable.Clone
            End If
            ds.Tables.Add(bDatatable)
        End If
nxt:
        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = fMainForm
        frm.Text = RptCaption
        frm.Show()
    End Sub

    Private Sub chkselectall_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkselectall.Click
        Dim i As Integer
        For i = 0 To grdVoucher.RowCount - 1
            If chkselectall.Checked Then
                grdVoucher.Item(ConstgTag, i).Value = "Y"
                btnpayment.Enabled = True
            Else
                grdVoucher.Item(ConstgTag, i).Value = ""
                btnpayment.Enabled = False
            End If
        Next
        calculatePayment()
    End Sub

    Private Sub PaymentBookingFrm_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Timer1.Enabled = True
    End Sub
End Class