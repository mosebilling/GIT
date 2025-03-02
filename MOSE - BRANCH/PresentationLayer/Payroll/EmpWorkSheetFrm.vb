Public Class EmpworksheetFrm
    Private _objcmnbLayer As New clsCommon_BL
    Private _objpayroll As New clsPayroll
    Private _dtTable As DataTable
    Private _dtcommissionslab As DataTable
    Private chgbyprg As Boolean
    Private activecontrolname As String
    Private SrchText As String
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fRptFormat As RptFormatfrm
    Private dtcommission As DataTable
    Private dtsales As DataTable
#Region "Constant Variables"
    Private Const ConstgTag = 0
    Private Const ConstDate = 1
    Private Const ConstgUnits = 2
    Private Const ConstgRate = 3
    Private Const Constgsales = 4
    Private Const ConstgCommission = 5
    Private Const ConstgTotal = 6
    Private Const ConstgDetid = 7
    Private Const Constgsheetid = 8
#End Region
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Private Sub getlastdate()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select max(sheetdate)sheetdate from PayrollWorksheetTb " & _
                                         "LEFT JOIN PayrollWorksheetDetTb ON PayrollWorksheetDetTb.sheetid=PayrollWorksheetTb.sheetid" & _
                                         " where empid=" & Val(lblemployee.Tag))
        If dt.Rows.Count > 0 Then
            lbllastdate.Text = "Last Date: " & dt(0)(0)
        Else
            lbllastdate.Text = "Last Date:"
        End If
    End Sub
    Private Function getsheetdata(ByVal dtdate As Date) As DataTable
        Dim bDatatable As DataTable

        If _dtTable.Rows.Count = 0 Then bDatatable = _dtTable.Clone : GoTo nxt
        Dim _qurey As EnumerableRowCollection(Of DataRow)
        _qurey = From data In _dtTable.AsEnumerable() Where Convert.ToDateTime(data("sheetdate")) = dtdate Select data
        If _qurey.Count > 0 Then
            bDatatable = _qurey.CopyToDataTable()
        Else
            bDatatable = _dtTable.Clone
        End If
nxt:
        Return bDatatable
    End Function
    Private Function getsalesmansalesdata(ByVal dtdate As Date) As Double
        Dim bDatatable As DataTable
        If dtsales.Rows.Count = 0 Then bDatatable = dtsales.Clone : GoTo nxt
        Dim _qurey As EnumerableRowCollection(Of DataRow)
        _qurey = From data In dtsales.AsEnumerable() Where Convert.ToDateTime(data("trdate")) = dtdate Select data
        If _qurey.Count > 0 Then
            bDatatable = _qurey.CopyToDataTable()
        Else
            bDatatable = _dtTable.Clone
        End If
nxt:
        If bDatatable.Rows.Count > 0 Then
            Return CDbl(bDatatable(0)("NetAmt"))
        Else
            Return 0
        End If

    End Function
    Public Function getcommission(ByVal amt As Double) As Double
        Dim bDatatable As DataTable
        If dtcommission.Rows.Count = 0 Then bDatatable = dtcommission.Clone : GoTo nxt
        Dim _qurey As EnumerableRowCollection(Of DataRow)
        _qurey = From data In dtcommission.AsEnumerable() Where Convert.ToDouble(data("targetamt")) <= amt Select data
        If _qurey.Count > 0 Then
            bDatatable = _qurey.CopyToDataTable()
        Else
            bDatatable = _dtTable.Clone
        End If
nxt:
        If bDatatable.Rows.Count > 0 Then
            Return CDbl(bDatatable(0)("commissionamt"))
        Else
            Return Val(lblsaleman.Tag)
        End If
    End Function
    Private Sub loadDates()
        cldrdateFrom.Tag = ""
        _dtTable = _objcmnbLayer._fldDatatable("Select sheetdate,sheetunits,detId,isnull(unitRate,0)unitRate,isnull(unitTotal,0)unitTotal,PayrollWorksheetTb.sheetid  from PayrollWorksheetTb " & _
                                               "left join PayrollWorksheetDetTb on PayrollWorksheetTb.sheetid=PayrollWorksheetDetTb.sheetid " & _
                                               "where sheetdate>='" & Format(DateValue(cldrdateFrom.Value), "yyyy/MM/dd") & "' AND sheetdate<='" & Format(DateValue(dtpdateTo.Value), "yyyy/MM/dd") & "'" & _
                                               "and empid=" & Val(lblemployee.Tag))
        Dim dtcount As Integer
        dtcount = DateDiff(DateInterval.Day, DateValue(cldrdateFrom.Value), DateValue(dtpdateTo.Value))
        Dim ddate As Date = DateValue(cldrdateFrom.Value)
        Dim i As Integer
        Dim dtsheetdata As DataTable
        grdVoucher.Rows.Clear()
        Dim salesamount As Double
        Dim commission As Double
        For i = 0 To dtcount
            With grdVoucher
                .Rows.Add("")
                .Item(ConstDate, i).Value = Format(DateValue(ddate), DtFormat)
                dtsheetdata = getsheetdata(ddate)
                If dtsheetdata.Rows.Count > 0 Then
                    .Item(ConstgTag, i).Value = "Y"
                    If Val(lblsalarytype.Tag) = 1 Then
                        .Item(ConstgUnits, i).Value = Format(dtsheetdata(0)("sheetunits"), numFormat)
                        .Item(ConstgRate, i).Value = Format(dtsheetdata(0)("unitRate"), numFormat)
                        lblrate.Tag = dtsheetdata(0)("unitRate")
                        lblrate.Text = "Rate : " & Format(CDbl(dtsheetdata(0)("unitRate")), numFormat)
                    Else
                        .Item(Constgsales, i).Value = Format(dtsheetdata(0)("sheetunits"), numFormat)
                        .Item(ConstgCommission, i).Value = Format(dtsheetdata(0)("unitTotal"), numFormat)
                        If Val(lblsalarytype.Tag) = 2 Then
                            lblrate.Tag = DateDiff(DateInterval.Day, DateValue(cldrdateFrom.Value), DateValue(dtpdateTo.Value))
                            lblrate.Tag = dtsheetdata(0)("unitRate") * (lblrate.Tag + 1)
                            lblrate.Text = "Salary : " & Format(CDbl(lblrate.Tag), numFormat)
                            lblrate.Tag = dtsheetdata(0)("unitRate")
                        Else
                            lblrate.Tag = dtsheetdata(0)("unitRate")
                            lblrate.Text = "Wages : " & Format(CDbl(dtsheetdata(0)("unitRate")), numFormat)
                        End If
                    End If
                        .Item(ConstgDetid, i).Value = dtsheetdata(0)("detId")
                        .Item(Constgsheetid, i).Value = dtsheetdata(0)("sheetid")
                        .Item(ConstgTotal, i).Value = Format(dtsheetdata(0)("unitTotal"), numFormat)
                Else
                        If chkloadsalescommission.Checked Then
                            If Not dtsales Is Nothing Then
                                salesamount = getsalesmansalesdata(ddate)
                            End If
                            If Not dtcommission Is Nothing Or salesamount > 0 Then
                                commission = getcommission(salesamount)
                            End If
                        Else
                            salesamount = 0
                            commission = 0
                        End If
                        .Item(Constgsales, i).Value = Format(salesamount, numFormat)
                        .Item(ConstgCommission, i).Value = Format(commission, numFormat)
                        'If Val(lblsalarytype.Tag) = 2 Then
                        '    .Item(ConstgUnits, i).Value = Format(salesamount, numFormat)
                        '    .Item(ConstgRate, i).Value = Format(commission, numFormat)
                        'Else
                        '    .Item(ConstgUnits, i).Value = 0
                        '    .Item(ConstgRate, i).Value = 0 ' Format(Val(lblrate.Tag), numFormat)
                        'End If
                        .Item(ConstgDetid, i).Value = 0
                        If Not dtcommission Is Nothing Then
                            If dtcommission.Rows.Count > 0 Then
                                .Item(ConstgTotal, i).Value = Format(commission, numFormat)
                            Else
                                .Item(ConstgTotal, i).Value = Format(salesamount * commission / 100, numFormat)
                            End If
                        End If
                        .Item(ConstgTotal, i).Value = CDbl(.Item(ConstgTotal, i).Value) + CDbl(.Item(ConstgUnits, i).Value) * CDbl(.Item(ConstgRate, i).Value)
                        .Item(ConstgTotal, i).Value = Format(CDbl(.Item(ConstgTotal, i).Value), numFormat)
                        If CDbl(.Item(ConstgUnits, i).Value) > 0 Then
                            .Item(ConstgTag, i).Value = "Y"
                        End If
                End If
            End With
            ddate = DateAdd(DateInterval.Day, 1, DateValue(ddate))
        Next
        If Val(cldrdateFrom.Tag) > 0 Then
            btndelete.Enabled = True
        Else
            btndelete.Enabled = False
        End If
        btnpreview.Enabled = True
ext:
        calculate()
    End Sub
    Private Sub loadEmployees()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select  SManCode,comP,EmpMasterTb.empcode,empname," & _
                                         "case when isnull(emptype,0)=0 then 'Labour' else 'Office Staff' end emptype," & _
                                       "DesignationName," & _
                                       "dailyPay,monthlyPay,salarycategory,EmpMasterTb.empid" & _
                                       "  from EmpMasterTb " & _
                                        "left join SalesmanTb on EmpMasterTb.empcode=SalesmanTb.empcode " & _
                                      "left join DesignationTb on EmpMasterTb.DesignationId=DesignationTb.DesignationId " & _
                                      "where empname='" & cmbemployee.Text & "'")
        If dt.Rows.Count > 0 Then
            lblemployee.Text = dt(0)("empname")
            lblemployee.Tag = dt(0)("empid")
            lbldesignation.Text = "Designation: " & Trim(dt(0)("DesignationName") & "")
            lblsaleman.Text = "S.Man: " & Trim(dt(0)("SManCode") & "")
            lblsaleman.Tag = Val(dt(0)("comP") & "")
            If Trim(dt(0)("SManCode") & "") <> "" Then
                dtcommission = _objcmnbLayer._fldDatatable("Select SalesmanCommissionSlabTb.targetamt,commissionamt,ispercentage from SalesmanCommissionSlabTb " & _
                                                           "left join SalesmanTb on SalesmanCommissionSlabTb.salesmanid=SalesmanTb.salesmanid " & _
                                                           " where SManCode='" & Trim(dt(0)("SManCode") & "' order by targetamt desc"))
                dtsales = _objcmnbLayer._fldDatatable("Select trdate,sum(NetAmt)NetAmt from ItmInvCmnTb where SlsManId='" & Trim(dt(0)("SManCode") & "") & "'" & _
                                                      " and trdate>='" & Format(DateValue(cldrdateFrom.Value), "yyyy/MM/dd") & "' and trdate<='" & Format(DateValue(dtpdateTo.Value), "yyyy/MM/dd") & "'" & _
                                                      "group by trdate")
            Else
                dtcommission = Nothing
                dtsales = Nothing
            End If
            Dim salarycategory As String
            cldrdateFrom.CustomFormat = "dd/MM/yyyy"
            Label1.Text = "From"
            dtpdateTo.Enabled = True
            Panel1.Visible = True
            If dt(0)("salarycategory") = 0 Then
                salarycategory = "Wages"

                lblrate.Text = "Wages : " & Format(CDbl(dt(0)("dailyPay")), numFormat)
                lblrate.Tag = dt(0)("dailyPay")
                grdVoucher.Columns(ConstgRate).Visible = False
                grdVoucher.Columns(ConstgUnits).Visible = False
                grdVoucher.Columns(Constgsales).Visible = True
                grdVoucher.Columns(ConstgCommission).Visible = True
            ElseIf dt(0)("salarycategory") = 1 Then
                salarycategory = "Unitwise"
                lblrate.Text = "Rate : " & Format(CDbl(dt(0)("dailyPay")), numFormat)
                lblrate.Tag = dt(0)("dailyPay")
                grdVoucher.Columns(ConstgUnits).Visible = True
                'grdVoucher.Columns(ConstgRate).Visible = True
                grdVoucher.Columns(Constgsales).Visible = False
                grdVoucher.Columns(ConstgCommission).Visible = False
                Panel1.Visible = False
            Else
                salarycategory = "Salary"
                grdVoucher.Columns(ConstgUnits).Visible = False
                grdVoucher.Columns(ConstgRate).Visible = False
                grdVoucher.Columns(Constgsales).Visible = True
                grdVoucher.Columns(ConstgCommission).Visible = True

                lblrate.Text = "Salary : " & Format(CDbl(dt(0)("monthlyPay")), numFormat)
                cldrdateFrom.CustomFormat = "MMM/yyyy"
                Label1.Text = "Month"
                dtpdateTo.Enabled = False
                cldrdateFrom.Value = DateValue("01/" & Month(cldrdateFrom.Value) & "/" & Year(cldrdateFrom.Value))
                Dim ldt As Date
                ldt = DateAdd(DateInterval.Month, 1, DateValue(cldrdateFrom.Value))
                ldt = DateValue("01/" & Month(ldt) & "/" & Year(ldt))
                ldt = DateAdd(DateInterval.Day, -1, DateValue(ldt))
                dtpdateTo.Value = ldt
                lblrate.Tag = DateDiff(DateInterval.Day, DateValue(cldrdateFrom.Value), DateValue(dtpdateTo.Value))
                lblrate.Tag = dt(0)("monthlyPay") / (lblrate.Tag + 1)
            End If
            lblsalarytype.Text = "Salary Type : " & salarycategory
            lblsalarytype.Tag = dt(0)("salarycategory")
        Else
            clear()
        End If
        loadDates()
        getlastdate()
    End Sub
    Private Sub clear()
        lblemployee.Text = ""
        lblemployee.Tag = ""
        lbldesignation.Text = ""
        lblsalarytype.Text = ""
        lblsalarytype.Tag = ""
    End Sub
    Private Sub SetGridHead()
        chgbyprg = True
        With grdVoucher

            SetEntryGridProperty(grdVoucher)
            .ColumnCount = 9

            .Columns(ConstgTag).HeaderText = "Tag"
            .Columns(ConstgTag).Width = 30
            .Columns(ConstgTag).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstgTag).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(ConstgTag).ReadOnly = True

            .Columns(ConstDate).HeaderText = "Date"
            .Columns(ConstDate).Width = 150
            .Columns(ConstDate).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstDate).ReadOnly = True

            .Columns(ConstgUnits).HeaderText = "Units"
            .Columns(ConstgUnits).Width = 150
            .Columns(ConstgUnits).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(ConstgUnits).Visible = rdounit.Checked
            .Columns(ConstgUnits).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstgUnits).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(ConstgRate).HeaderText = "Rate"
            .Columns(ConstgRate).Width = 100
            .Columns(ConstgRate).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstgRate).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstgRate).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstgRate).Visible = False

            .Columns(Constgsales).HeaderText = "Total Sales"
            .Columns(Constgsales).Width = 100
            .Columns(Constgsales).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(Constgsales).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(Constgsales).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(Constgsales).ReadOnly = True

            .Columns(ConstgCommission).HeaderText = "Commission"
            .Columns(ConstgCommission).Width = 100
            .Columns(ConstgCommission).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstgCommission).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstgCommission).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstgCommission).ReadOnly = True

            .Columns(ConstgTotal).HeaderText = "Total"
            .Columns(ConstgTotal).Width = 150
            .Columns(ConstgTotal).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstgTotal).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstgTotal).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstgTotal).ReadOnly = True
            .Columns(ConstgDetid).Visible = False
            .Columns(Constgsheetid).Visible = False
        End With
        chgbyprg = False
    End Sub

    Private Sub btnfind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnfind.Click
        Dim rindex As Integer
        Dim getValue As String
        getValue = SearchSequenceFromGrid(grdVoucher, cmbcolms.SelectedIndex + 1, txtSeq.Text, Val(btnfind.Tag) + 1)
        With grdVoucher
            If .RowCount = 0 Then Exit Sub
            If getValue <> "N" Then
                rindex = Val(getValue)
                .ClearSelection()

                .CurrentCell = .Item(cmbcolms.SelectedIndex + 1, rindex)
                .Rows(rindex).Selected = True
                .FirstDisplayedScrollingRowIndex = rindex
                btnfind.Tag = .CurrentRow.Index
            Else
                btnfind.Tag = -1
            End If
        End With
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'Timer1.Enabled = False
        'resizeGridColumn(grdVoucher, ConstgEmpname)
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

    Private Sub grdVoucher_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellClick
        chgbyprg = True
        If e.RowIndex < 0 Then Exit Sub
        If e.ColumnIndex <> ConstgUnits Then
            grdVoucher.CurrentCell.ReadOnly = True
            Select Case e.ColumnIndex
                Case ConstgTag
                    grdVoucher.Item(e.ColumnIndex, e.RowIndex).Value = IIf(grdVoucher.Item(e.ColumnIndex, e.RowIndex).Value = "Y", "", "Y")
                    'If grdVoucher.Item(e.ColumnIndex, e.RowIndex).Value = "Y" Then
                    '    If Val(lblsalarytype.Tag) = 0 Then
                    '        grdVoucher.Item(ConstgUnits, e.RowIndex).Value = 1
                    '    End If
                    'Else
                    '    grdVoucher.Item(ConstgUnits, e.RowIndex).Value = 0
                    'End If
                    chkselectall.Checked = False
                    calculate()
            End Select
        
        End If
        chgbyprg = False
    End Sub

    Private Sub grdVoucher_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellEndEdit
        Dim col As Integer = e.ColumnIndex
        Dim ndec1 As Integer = 2
        chgbyprg = True
        If col = ConstgUnits Then
            If Val(grdVoucher.Item(col, e.RowIndex).Value) = 0 Then grdVoucher.Item(col, e.RowIndex).Value = 0
            grdVoucher.Item(col, e.RowIndex).Value = Format(Convert.ToDecimal(grdVoucher.Item(col, e.RowIndex).Value), "#,##0" & IIf(ndec1 = 0, "", "." & Strings.StrDup(ndec1, "0")))
        End If
        chgbyprg = False
    End Sub

    Private Sub grdVoucher_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellEnter
        'If e.ColumnIndex = ConstgUnits And rdounit.Checked Then
        grdBeginEdit()

    End Sub

    Private Sub grdVoucher_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellValidated
        If e.ColumnIndex = ConstgUnits Then
            If Val(grdVoucher.Item(e.ColumnIndex, e.RowIndex).Value) > 0 Then
                grdVoucher.Item(ConstgTag, e.RowIndex).Value = "Y"
            End If
            calculate()
        End If

    End Sub

    Private Sub grdVoucher_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.GotFocus
        activecontrolname = "grdVoucher"
    End Sub

    Private Sub grdVoucher_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdVoucher.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If grdVoucher.RowCount = 0 Then Exit Sub
                If grdVoucher.CurrentRow.Index < grdVoucher.RowCount - 1 Then
                    grdVoucher.CurrentCell = grdVoucher.Item(ConstgUnits, grdVoucher.CurrentRow.Index + 1)
                Else
                    grdVoucher.CurrentCell = grdVoucher.Item(ConstgRate, grdVoucher.CurrentRow.Index)
                End If
                'FindNextCell(grdVoucher, grdVoucher.CurrentCell.RowIndex, grdVoucher.CurrentCell.ColumnIndex + 1)
nxt:

                grdBeginEdit()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub grdBeginEdit()
        chgbyprg = True
        grdVoucher.BeginEdit(True)
        chgbyprg = False
    End Sub

    Private Sub grdVoucher_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.Leave
        activecontrolname = ""
    End Sub
    Private Sub calculate()
        Dim i As Integer
        Dim ttl As Double
        lblpay.Text = 0
        For i = 0 To grdVoucher.RowCount - 1
            With grdVoucher
                If .Item(ConstgTag, i).Value = "Y" Then
                    If Val(lblsalarytype.Tag) = 1 Then
                        If Val(.Item(ConstgUnits, i).Value) = 0 Then .Item(ConstgUnits, i).Value = 0
                        If Val(.Item(ConstgRate, i).Value) = 0 Then .Item(ConstgRate, i).Value = 0
                        .Item(ConstgTotal, i).Value = Format(CDbl(.Item(ConstgUnits, i).Value) * CDbl(lblrate.Tag), numFormat)
                        ttl = CDbl(.Item(ConstgTotal, i).Value) + ttl
                    Else
                        If Val(.Item(ConstgTotal, i).Value) = 0 Then .Item(ConstgTotal, i).Value = 0
                        ttl = CDbl(.Item(ConstgTotal, i).Value) + ttl
                        lblpay.Text = CDbl(lblpay.Text) + CDbl(lblrate.Tag)
                    End If
                End If
            End With
        Next
        If Val(lblsalarytype.Tag) = 1 Then
            lblNetAmt.Text = Format(ttl, numFormat)
            lblcommission.Text = 0
            lblpay.Text = 0
        Else
            lblNetAmt.Text = Format(ttl + CDbl(lblpay.Text), numFormat)
            lblcommission.Text = Format(ttl, numFormat)
            lblpay.Text = Format(CDbl(lblpay.Text), numFormat)
        End If
    End Sub

    Private Sub btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnload.Click
        'Dim dt As DataTable
        'dt = _objcmnbLayer._fldDatatable("select paymentid from PayrollPaymentCmnTb where datefrom<='" & Format(DateValue(cldrdate.Value), "yyyy/MM/dd") & "' and dateto>='" & Format(DateValue(cldrdate.Value), "yyyy/MM/dd") & "'")
        'If dt.Rows.Count > 0 Then
        '    MsgBox("Payment booking already created for selected date! Cannot Prepared", MsgBoxStyle.Exclamation)
        '    Exit Sub
        'End If

        loadEmployees()
    End Sub


    Private Sub BtnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUpdate.Click
        If grdVoucher.RowCount = 0 Then
            MsgBox("Records not found", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If Val(lblNetAmt.Text) = 0 Then
            If Val(cldrdateFrom.Tag) > 0 Then
                If MsgBox("Data found for the seleted date! " & vbCrLf & "If you continue with blank worsheet, saved data will be removed" & vbCrLf & "Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Exit Sub
                Else
                    _objcmnbLayer._saveDatawithOutParm("delete from PayrollWorksheetTb where sheetid=" & Val(cldrdateFrom.Tag))
                    GoTo lst
                End If
            Else
                'MsgBox("Valid entries not found", MsgBoxStyle.Exclamation)
                'Exit Sub
            End If

        End If
        Dim sheetid As Long
        Dim i As Integer
        Dim dt As DataTable
        With grdVoucher
            For i = 0 To .RowCount - 1
                If .Item(ConstgTag, i).Value = "Y" Then
                    dt = _objcmnbLayer._fldDatatable("select sheetid from PayrollWorksheetTb where sheetempid=" & Val(lblemployee.Tag) & " and sheetcategory=" & Val(lblsalarytype.Tag) & _
                                                     " and sheetdate='" & Format(DateValue(.Item(ConstDate, i).Value), "yyyy/MM/dd") & "'")
                    sheetid = 0
                    If dt.Rows.Count > 0 Then
                        sheetid = dt(0)("sheetid")
                    Else
                        _objpayroll.sheetid = 0
                        _objpayroll.sheetdate = Format(DateValue(.Item(ConstDate, i).Value), "yyyy/MM/dd")
                        _objpayroll.sheetcategory = Val(lblsalarytype.Tag)
                        sheetid = _objpayroll.savePayrollWorksheet()
                        _objcmnbLayer._saveDatawithOutParm("update PayrollWorksheetTb set sheetempid=" & Val(lblemployee.Tag) & " where sheetid=" & sheetid)
                    End If
                    _objpayroll.detId = Val(.Item(ConstgDetid, i).Value)
                    _objpayroll.sheetid = sheetid
                    _objpayroll.empid = Val(lblemployee.Tag)
                    If Val(lblsalarytype.Tag) = 1 Then
                        _objpayroll.sheetunits = CDbl(.Item(ConstgUnits, i).Value)
                    Else
                        _objpayroll.sheetunits = CDbl(.Item(Constgsales, i).Value)
                    End If
                    _objpayroll.unitRate = CDbl(lblrate.Tag)
                    _objpayroll.unitTotal = CDbl(.Item(ConstgTotal, i).Value)
                    _objpayroll.savePayrollWorksheetDet()
                Else
                    _objcmnbLayer._saveDatawithOutParm("delete from PayrollWorksheetDetTb where detId=" & Val(.Item(ConstgDetid, i).Value))
                End If
            Next
        End With
lst:
        cldrdateFrom.Tag = ""
        MsgBox("Worksheet updated", MsgBoxStyle.Information)
        getlastdate()
        loadEmployees()
    End Sub

    Private Sub cldrdate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cldrdateFrom.ValueChanged
        grdVoucher.Rows.Clear()
        cldrdateFrom.Tag = ""
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        If Val(cldrdateFrom.Tag) = 0 Then Exit Sub
        If MsgBox("Do you want delete worksheet for selected date?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

        _objcmnbLayer._saveDatawithOutParm("delete from PayrollWorksheetDetTb where sheetid=" & Val(cldrdateFrom.Tag))
        _objcmnbLayer._saveDatawithOutParm("delete from PayrollWorksheetTb where sheetid=" & Val(cldrdateFrom.Tag))
        loadEmployees()
        getlastdate()
    End Sub

    Private Sub chkwages_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        loadEmployees()
    End Sub

    Private Sub chkselectall_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkselectall.Click
        Dim i As Integer
        For i = 0 To grdVoucher.RowCount - 1
            If chkselectall.Checked Then
                grdVoucher.Item(ConstgTag, i).Value = "Y"
                If Val(lblsalarytype.Tag) = 0 Then
                    grdVoucher.Item(ConstgUnits, i).Value = 1
                End If
            Else
                grdVoucher.Item(ConstgTag, i).Value = ""
                grdVoucher.Item(ConstgUnits, i).Value = 0
            End If
        Next
        calculate()
    End Sub


    Private Sub btnpreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpreview.Click
        Dim RptType As String = ""
        'If rdodaily.Checked Then
        '    RptType = "PATT"
        'Else
        '    RptType = "PWSH"

        'End If
        RptType = "PATE"
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
        Dim sheetcategory As Integer
        Dim condition As String = " AND EmpMasterTb.empid=" & Val(lblemployee.Tag)
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select  empcode,empname,case when isnull(emptype,0)=0 then 'Labour' else 'Office Staff' end emptype," & _
                                               "DesignationName [Designation]," & _
                                               "case when salarycategory=2 then isnull(monthlyPay,0)/30 else dailyPay end dailyPay,monthlyPay,salarycategory,EmpMasterTb.empid,isnull(sheetunits,0)sheetunits,isnull(detId,0)detId," & _
                                               "case when isnull(sheetunits,0)=0 then '' else 'Y' end absentOrPresent, sheetdate,1 lnk,unitrate,unittotal" & _
                                               "  from EmpMasterTb " & _
                                              "left join DesignationTb on EmpMasterTb.DesignationId=DesignationTb.DesignationId " & _
                                              "left join(Select case when sheetcategory=1 or isnull(sheetunits,0)>0 then isnull(sheetunits,0) else 1 end sheetunits,empid,detId,sheetdate,isnull(unitrate,0)unitrate,isnull(unittotal,0)unittotal from PayrollWorksheetDetTb left join PayrollWorksheetTb on PayrollWorksheetTb.sheetid=PayrollWorksheetDetTb.sheetid " & _
                                              "where sheetdate>='" & Format(DateValue(cldrdateFrom.Value), "yyyy/MM/dd") & "' AND sheetdate<='" & Format(DateValue(dtpdateTo.Value), "yyyy/MM/dd") & _
                                              "')PayrollWorksheetDetTb on EmpMasterTb.empid=PayrollWorksheetDetTb.empid " & _
                                              "where (isnull(empstatus,0)=0 Or isnull(sheetunits,0)>0) " & condition)
        ds.Tables.Add(dt)
        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = fMainForm
        frm.Text = "Payroll report"
        frm.Show()
    End Sub

    Private Sub EmpworksheetFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetGridHead()
        loadEmps()
        cmbemployee.SelectedIndex = 0
    End Sub
    Private Sub loadEmps()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select  empname from EmpMasterTb")
        cmbemployee.Items.Clear()
        Dim i As Integer
        For i = 0 To dt.Rows.Count - 1
            cmbemployee.Items.Add(dt(i)("empname"))
        Next
    End Sub

    Private Sub cmbemployee_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbemployee.SelectedIndexChanged
        loadEmployees()
    End Sub

    Private Sub chkselectall_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkselectall.CheckedChanged

    End Sub

    Private Sub grdVoucher_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellContentClick

    End Sub

    Private Sub chkloadsalescommission_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkloadsalescommission.CheckedChanged

    End Sub
End Class