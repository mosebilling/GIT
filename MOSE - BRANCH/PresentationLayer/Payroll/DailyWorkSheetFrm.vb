Public Class DailyWorkSheetFrm
    Private _objcmnbLayer As New clsCommon_BL
    Private _objpayroll As New clsPayroll
    Private _dtTable As DataTable
    Private chgbyprg As Boolean
    Private activecontrolname As String
    Private SrchText As String
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fRptFormat As RptFormatfrm
#Region "Constant Variables"
    Private Const ConstgSlNo = 0
    Private Const ConstgTag = 1
    Private Const ConstgEmpCode = 2
    Private Const ConstgEmpname = 3
    Private Const ConstgEmpType = 4
    Private Const ConstgDesignation = 5
    Private Const ConstgUnits = 6
    Private Const ConstgRate = 7
    Private Const ConstgTotal = 8
    Private Const Constgsalarycategory = 9
    Private Const ConstgEmpId = 10
    Private Const ConstgDetid = 11
#End Region
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub rdodaily_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdodaily.Click, rdounit.Click, rdowages.Click
        If rdodaily.Checked Or rdowages.Checked Then
            chkselectall.Visible = True
        Else
            chkselectall.Visible = False
        End If
        grdVoucher.Rows.Clear()
        SetGridHead()
        Timer1.Enabled = True
        loadEmployees()
        getlastdate()
    End Sub

    Private Sub DailyWorkSheetFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetGridHead()
        lblstatus.Text = ""
        Timer1.Enabled = True
        getlastdate()
    End Sub
    Private Sub getlastdate()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select max(sheetdate)sheetdate from PayrollWorksheetTb where sheetcategory=" & IIf(rdodaily.Checked, 0, 1))
        If dt.Rows.Count > 0 Then
            lbllastdate.Text = "Last Date: " & dt(0)(0)
        Else
            lbllastdate.Text = "Last Date:"
        End If
    End Sub
    Private Sub loadEmployees()
        Dim condition As String
        cldrdate.Tag = ""
        Dim sheetcategory As Integer
        If rdodaily.Checked Then
            condition = " AND isnull(salarycategory,0)=2"
            'If chkwages.Checked Or chksalary.Checked Then
            '    condition = " AND "
            '    If chkwages.Checked Then
            '        condition = condition & "( isnull(salarycategory,0)=0"
            '    End If
            '    If chksalary.Checked Then
            '        condition = condition & IIf(chkwages.Checked, " OR ", "(") & " isnull(salarycategory,0)=2"
            '    End If
            '    condition = condition & ")"
            'Else
            '    MsgBox("Category not found", MsgBoxStyle.Exclamation)
            '    Exit Sub
            'End If
            sheetcategory = 2
        ElseIf rdowages.Checked Then
            condition = " AND isnull(salarycategory,0)=0"
            sheetcategory = 0
        Else
            condition = " AND isnull(salarycategory,0)=1"
            sheetcategory = 1
        End If
        lblstatus.Text = "Not Saved"
        lblstatus.ForeColor = Color.Red
        _dtTable = _objcmnbLayer._fldDatatable("Select sheetid,isnull(paymentid,0)transferid from PayrollWorksheetTb " & _
                                               "left join PayrollPaymentCmnTb on PayrollPaymentCmnTb.paymentid=PayrollWorksheetTb.transferid where sheetdate='" & Format(DateValue(cldrdate.Value), "yyyy/MM/dd") & "' AND sheetcategory=" & sheetcategory)
        If _dtTable.Rows.Count > 0 Then
            cldrdate.Tag = Val(_dtTable(0)("sheetid"))
            If Val(_dtTable(0)("transferid")) = 0 Then
                _objcmnbLayer._saveDatawithOutParm("update PayrollWorksheetDetTb set setRemove=1 where sheetid=" & Val(cldrdate.Tag))
                lblstatus.Text = "Not Transfered"
                lblstatus.ForeColor = Color.Red
                BtnUpdate.Enabled = True
            Else
                MsgBox("Sheet already transfered! You cannot edit/remove", MsgBoxStyle.Exclamation)
                BtnUpdate.Enabled = False
                lblstatus.Text = "Transfered"
                lblstatus.ForeColor = Color.Green
            End If

        Else
            cldrdate.Tag = 0
            Dim dt As DataTable
            dt = _objcmnbLayer._fldDatatable("select paymentid from PayrollPaymentCmnTb where datefrom<='" & Format(DateValue(cldrdate.Value), "yyyy/MM/dd") & "' and dateto>='" & Format(DateValue(cldrdate.Value), "yyyy/MM/dd") & "'")
            If dt.Rows.Count > 0 Then
                MsgBox("Payment booking already created for selected date! Cannot Prepared", MsgBoxStyle.Exclamation)
                'GoTo ext
                BtnUpdate.Enabled = False
            Else
                BtnUpdate.Enabled = True
            End If
        End If
        _dtTable = _objcmnbLayer._fldDatatable("Select  empcode,empname,case when isnull(emptype,0)=0 then 'Labour' else 'Office Staff' end emptype," & _
                                               "DesignationName [Designation]," & _
                                               "dailyPay,monthlyPay,salarycategory,EmpMasterTb.empid,isnull(sheetunits,0)sheetunits,isnull(detId,0)detId," & _
                                               "case when isnull(sheetunits,0)=0 then '' else 'Y' end absentOrPresent,'" & DateValue(cldrdate.Value) & "' sheetdate,1 lnk" & _
                                               "  from EmpMasterTb " & _
                                              "left join DesignationTb on EmpMasterTb.DesignationId=DesignationTb.DesignationId " & _
                                              "left join(Select case when sheetcategory=1 then isnull(sheetunits,0) else 1 end sheetunits,empid,detId from PayrollWorksheetDetTb " & _
                                              "left join PayrollWorksheetTb on PayrollWorksheetTb.sheetid=PayrollWorksheetDetTb.sheetid " & _
                                              "where PayrollWorksheetDetTb.sheetid=" & Val(cldrdate.Tag) & ")PayrollWorksheetDetTb on EmpMasterTb.empid=PayrollWorksheetDetTb.empid " & _
                                              "where (isnull(empstatus,0)=0 Or isnull(sheetunits,0)>0) " & condition)
        grdVoucher.Rows.Clear()
        If _dtTable.Rows.Count > 0 Then
            Dim i As Integer
            With grdVoucher
                For i = 0 To _dtTable.Rows.Count - 1
                    .Rows.Add("")
                    .Item(ConstgSlNo, i).Value = i + 1
                    .Item(ConstgEmpCode, i).Value = _dtTable(i)("empcode")
                    .Item(ConstgEmpname, i).Value = _dtTable(i)("empname")
                    .Item(ConstgEmpType, i).Value = _dtTable(i)("emptype")
                    .Item(ConstgDesignation, i).Value = Trim(_dtTable(i)("Designation") & "")
                    .Item(ConstgUnits, i).Value = Format(CDbl(_dtTable(i)("sheetunits")), numFormat)
                    If Val(.Item(ConstgUnits, i).Value) > 0 Then
                        .Item(ConstgTag, i).Value = "Y"
                    End If
                    If _dtTable(i)("salarycategory") = 2 Then
                        If Val(_dtTable(i)("monthlyPay") & "") = 0 Then _dtTable(i)("monthlyPay") = 0
                        .Item(ConstgRate, i).Value = Format(CDbl(_dtTable(i)("monthlyPay")) / 30, numFormat)
                    Else
                        If Val(_dtTable(i)("dailyPay") & "") = 0 Then _dtTable(i)("dailyPay") = 0
                        .Item(ConstgRate, i).Value = Format(CDbl(_dtTable(i)("dailyPay")), numFormat)
                    End If
                    .Item(ConstgTotal, i).Value = Format(CDbl(.Item(ConstgRate, i).Value) * CDbl(.Item(ConstgUnits, i).Value), numFormat)
                    .Item(ConstgEmpId, i).Value = _dtTable(i)("empid")
                    .Item(Constgsalarycategory, i).Value = _dtTable(i)("salarycategory")
                    .Item(ConstgDetid, i).Value = _dtTable(i)("detId")
                Next
            End With
        End If
        If Val(cldrdate.Tag) > 0 Then
            btndelete.Enabled = True
        Else
            btndelete.Enabled = False
        End If
        btnpreview.Enabled = True
ext:
        calculate()
    End Sub
    Private Sub SetGridHead()
        chgbyprg = True

        With grdVoucher

            SetEntryGridProperty(grdVoucher)
            .ColumnCount = 12

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
            .Columns(ConstgTag).ReadOnly = True

            .Columns(ConstgEmpCode).HeaderText = "Code"
            .Columns(ConstgEmpCode).Width = 100
            .Columns(ConstgEmpCode).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstgEmpCode).ReadOnly = True

            .Columns(ConstgEmpname).HeaderText = "Employee Name"
            .Columns(ConstgEmpname).Width = 100
            .Columns(ConstgEmpname).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstgEmpname).ReadOnly = True

            .Columns(ConstgEmpType).HeaderText = "Type"
            .Columns(ConstgEmpType).Width = 75
            .Columns(ConstgEmpType).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstgEmpType).ReadOnly = True

            .Columns(ConstgDesignation).HeaderText = "Designation"
            .Columns(ConstgDesignation).Width = 130
            .Columns(ConstgDesignation).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstgDesignation).ReadOnly = True

            .Columns(ConstgUnits).HeaderText = "Units"
            .Columns(ConstgUnits).Width = 75
            .Columns(ConstgUnits).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstgUnits).Visible = rdounit.Checked
            .Columns(ConstgUnits).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstgUnits).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(ConstgRate).HeaderText = "Rate"
            .Columns(ConstgRate).Width = 60
            .Columns(ConstgRate).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstgRate).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstgRate).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstgRate).ReadOnly = True

            .Columns(ConstgTotal).HeaderText = "Total"
            .Columns(ConstgTotal).Width = 100
            .Columns(ConstgTotal).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstgTotal).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstgTotal).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstgTotal).ReadOnly = True

            .Columns(ConstgEmpId).Visible = False
            .Columns(Constgsalarycategory).Visible = False
            .Columns(ConstgDetid).Visible = False

            Dim i As Integer
            cmbcolms.Items.Clear()
            For i = 1 To ConstgDesignation
                cmbcolms.Items.Add(.Columns(i).HeaderText)
            Next
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
        Timer1.Enabled = False
        resizeGridColumn(grdVoucher, ConstgEmpname)
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
        If e.ColumnIndex <> ConstgUnits Then
            grdVoucher.CurrentCell.ReadOnly = True
            Select Case e.ColumnIndex
                Case ConstgTag
                    grdVoucher.Item(e.ColumnIndex, e.RowIndex).Value = IIf(grdVoucher.Item(e.ColumnIndex, e.RowIndex).Value = "Y", "", "Y")
                    If grdVoucher.Item(e.ColumnIndex, e.RowIndex).Value = "Y" Then
                        If grdVoucher.Item(Constgsalarycategory, e.RowIndex).Value = 1 Then
                            grdVoucher.Item(ConstgUnits, e.RowIndex).Value = 0
                        Else
                            grdVoucher.Item(ConstgUnits, e.RowIndex).Value = 1
                        End If
                    Else
                        grdVoucher.Item(ConstgUnits, e.RowIndex).Value = 0
                    End If
                    chkselectall.Checked = False
                    calculate()
            End Select
        Else
            If grdVoucher.Item(Constgsalarycategory, e.RowIndex).Value = 1 Then
                grdVoucher.CurrentCell.ReadOnly = False
            Else
                grdVoucher.CurrentCell.ReadOnly = True
            End If
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
        If e.ColumnIndex = ConstgUnits And rdounit.Checked Then
            grdBeginEdit()
        End If
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
        For i = 0 To grdVoucher.RowCount - 1
            With grdVoucher
                If Val(.Item(ConstgUnits, i).Value) = 0 Then .Item(ConstgUnits, i).Value = 0
                If Val(.Item(ConstgRate, i).Value) = 0 Then .Item(ConstgRate, i).Value = 0
                .Item(ConstgTotal, i).Value = Format(CDbl(.Item(ConstgUnits, i).Value) * CDbl(.Item(ConstgRate, i).Value), numFormat)
                ttl = CDbl(.Item(ConstgTotal, i).Value) + ttl
            End With
        Next
        lblNetAmt.Text = Format(ttl, numFormat)
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
            If Val(cldrdate.Tag) > 0 Then
                If MsgBox("Data found for the seleted date! " & vbCrLf & "If you continue with blank worsheet, saved data will be removed" & vbCrLf & "Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Exit Sub
                Else
                    _objcmnbLayer._saveDatawithOutParm("delete from PayrollWorksheetTb where sheetid=" & Val(cldrdate.Tag))
                    GoTo lst
                End If
            Else
                MsgBox("Valid entries not found", MsgBoxStyle.Exclamation)
                Exit Sub
            End If

        End If
        Dim sheetid As Long
        _objcmnbLayer._saveDatawithOutParm("update PayrollWorksheetDetTb set setRemove=1 where sheetid=" & Val(cldrdate.Tag))
        With _objpayroll
            .sheetid = Val(cldrdate.Tag)
            .sheetdate = Format(DateValue(cldrdate.Value), "yyyy/MM/dd")
            .sheetdateto = Format(DateValue(cldrdate.Value), "yyyy/MM/dd")
            If rdodaily.Checked Then
                .sheetcategory = 0
            ElseIf rdowages.Checked Then
                .sheetcategory = 1
            Else
                .sheetcategory = 2
            End If
            .sheetTotal = CDbl(lblNetAmt.Text)
            sheetid = .savePayrollWorksheet()
        End With
        Dim i As Integer
        With grdVoucher
            For i = 0 To .RowCount - 1
                If .Item(ConstgTag, i).Value = "Y" Then
                    _objpayroll.detId = Val(.Item(ConstgDetid, i).Value)
                    _objpayroll.sheetid = sheetid
                    _objpayroll.empid = Val(.Item(ConstgEmpId, i).Value)
                    _objpayroll.sheetunits = CDbl(.Item(ConstgUnits, i).Value)
                    _objpayroll.savePayrollWorksheetDet()
                End If
            Next
        End With
lst:
        _objcmnbLayer._saveDatawithOutParm("delete from PayrollWorksheetDetTb where setRemove=1 and sheetid=" & Val(cldrdate.Tag))
        cldrdate.Tag = ""
        MsgBox("Worksheet updated", MsgBoxStyle.Information)
        getlastdate()
        loadEmployees()
    End Sub

    Private Sub cldrdate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cldrdate.ValueChanged
        grdVoucher.Rows.Clear()
        cldrdate.Tag = ""
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        If Val(cldrdate.Tag) = 0 Then Exit Sub
        If MsgBox("Do you want delete worksheet for selected date?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

        _objcmnbLayer._saveDatawithOutParm("delete from PayrollWorksheetDetTb where sheetid=" & Val(cldrdate.Tag))
        _objcmnbLayer._saveDatawithOutParm("delete from PayrollWorksheetTb where sheetid=" & Val(cldrdate.Tag))
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
                grdVoucher.Item(ConstgUnits, i).Value = 1
            Else
                grdVoucher.Item(ConstgTag, i).Value = ""
                grdVoucher.Item(ConstgUnits, i).Value = 0
            End If
        Next
        calculate()
    End Sub


    Private Sub btnpreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpreview.Click
        Dim RptType As String = ""
        If rdodaily.Checked Then
            RptType = "PATT"
        Else
            RptType = "PWSH"

        End If

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
        Dim condition As String
        If rdodaily.Checked Then
            condition = " AND isnull(salarycategory,0)=2"
            sheetcategory = 2
        ElseIf rdowages.Checked Then
            condition = " AND isnull(salarycategory,0)=0"
            sheetcategory = 0
        Else
            condition = " AND isnull(salarycategory,0)=1"
            sheetcategory = 1
        End If
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select  empcode,empname,case when isnull(emptype,0)=0 then 'Labour' else 'Office Staff' end emptype," & _
                                               "DesignationName [Designation]," & _
                                               "case when salarycategory=2 then isnull(monthlyPay,0)/30 else dailyPay end dailyPay,monthlyPay,salarycategory,EmpMasterTb.empid,isnull(sheetunits,0)sheetunits,isnull(detId,0)detId," & _
                                               "case when isnull(sheetunits,0)=0 then '' else 'Y' end absentOrPresent,'" & DateValue(cldrdate.Value) & "' sheetdate,1 lnk" & _
                                               "  from EmpMasterTb " & _
                                              "left join DesignationTb on EmpMasterTb.DesignationId=DesignationTb.DesignationId " & _
                                              "left join(Select case when sheetcategory=1 then isnull(sheetunits,0) else 1 end sheetunits,empid,detId from PayrollWorksheetDetTb left join PayrollWorksheetTb on PayrollWorksheetTb.sheetid=PayrollWorksheetDetTb.sheetid " & _
                                              "where sheetdate='" & Format(DateValue(cldrdate.Value), "yyyy/MM/dd") & "' AND sheetcategory=" & sheetcategory & ")PayrollWorksheetDetTb on EmpMasterTb.empid=PayrollWorksheetDetTb.empid " & _
                                              "where (isnull(empstatus,0)=0 Or isnull(sheetunits,0)>0) " & condition)
        ds.Tables.Add(dt)
        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = fMainForm
        frm.Text = RptCaption
        frm.Show()
    End Sub

    Private Sub chkselectall_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkselectall.CheckedChanged

    End Sub

    Private Sub rdodaily_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdodaily.CheckedChanged

    End Sub

    Private Sub grdVoucher_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellContentClick

    End Sub
End Class