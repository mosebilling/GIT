
Public Class Reconciliation
    Private _objcmnbLayer As New clsCommon_BL
    Private _clsStatementReport As New clsAccountTransaction
    Private WithEvents fRptFormat As RptFormatfrm
    Private WithEvents frm As New ReprtviewNEWfrm
    Private _dtProjection As New DataTable

#Region "Const Variables"
    Private Const ConstJVType = 0
    Private Const ConstJVNum = 1
    Private Const ConstJVDate = 2
    Private Const ConstReference = 3
    Private Const ConstDescription = 4
    Private Const ConstChqNo = 5
    Private Const ConstChqDate = 6
    Private Const ConstDebit = 7
    Private Const ConstCredit = 8
    Private Const Constbalance = 9
    Private Const ConstAccountNo = 10
    Private Const ConstGrpSetOn = 11
    Private Const ConstLnkNo = 12
    Private Const ConstAccdescr = 13
    Private Const ConstFromDate = 14
    Private Const ConstToDate = 15
    Private Const ConstTypeOrd = 16
    Private Const ConstProOrder = 17

#End Region

    Private Sub ProjectionReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        rdUncleared.Checked = True
        LoadDefaultDate()
        Loadgrid()
    End Sub
    Public Sub GetBankCode()
        Dim _dtBankCode As New DataTable

        Dim qry As String = "select AcBankCode,(isnull(OpeningBal,0)+isnull(Acm.AccOpn ,0)) OpnBal,isnull(debit,0)debit,isnull(credit,0)credit from Accmast left join" & _
                            " (SELECT SUM(Case when DealAmt>0 then DealAmt else 0 end) debit,SUM(Case when DealAmt>0 then 0 else DealAmt*-1 end) Credit," & _
                            "AccountNo from acctrdet left Join AcctrCmn On AccTrCmn . LinkNo = AcctrDet.LinkNo where JVDate>='" & Format(DateValue(cldrdateFrom.Value), "yyyy/MM/dd") & _
                            "' and JVDate<='" & Format(DateValue(clrDateTo.Value), "yyyy/MM/dd") & "' and JVTYpe<>'OB' group by accountno) TR " & _
                            "ON tr.accountno=Accmast.accid left Join  ( select OPdebit-OpCredit OpeningBal,AccountNo from (select Sum(case when dealAmt>0 then DealAmt else 0 end) Opdebit," & _
                            "SUM(Case when DealAmt>0 then 0 else DealAmt*-1 end) OPCredit,AccountNo from acctrdet left Join AcctrCmn On AccTrCmn . LinkNo = AcctrDet.LinkNo " & _
                            "where JVDate<'" & Format(DateValue(cldrdateFrom.Value), "yyyy/MM/dd") & "' and JVTYpe<>'OB' group by accountno)A)Op On AccMast.accid=OP.AccountNo " & _
                            "left join (select OpnBal AccOpn,accid  from AccMast)Acm On AccMast.accid =Acm.accid  where Accmast.accid=" & Val(txtbankname.Tag)


        _dtBankCode = _objcmnbLayer._fldDatatable(qry)
        If (_dtBankCode.Rows.Count > 0) Then
            txtBankCode.Text = Trim(_dtBankCode(0)(0) & "")
            lbldebit.Text = Format(_dtBankCode(0)("debit"), numFormat)
            lblcredit.Text = Format(_dtBankCode(0)("credit"), numFormat)
            ' If CDbl(_dtBankCode(0)("OpnBal")) >= 0 Then
            lblOpening.Text = Format(CDbl(_dtBankCode(0)("OpnBal")), numFormat) '  Format(CDbl(_dtBankCode(0)("debit")) +
            'Else
            '    lblcredit.Text = Format(CDbl(_dtBankCode(0)("credit")) + CDbl(_dtBankCode(0)("OpnBal") * -1), numFormat)
            'End If
        End If
        lblbalance.Text = 0
        lblbalance.Text = Format((CDbl(lblOpening.Text) + CDbl(lbldebit.Text)) - CDbl(lblcredit.Text), numFormat)
    End Sub
    Private Sub BalanceTotal()
        lblAdd.Text = Format(0, numFormat)
        lblLess.Text = Format(0, numFormat)
        With grdVoucher
            For i = 0 To .Rows.Count - 1
                If .Item(0, i).Value <> "Y" Then
                    lblAdd.Text = CDbl(lblAdd.Text) + CDbl(.Item("Credit", i).Value)
                    lblLess.Text = CDbl(lblLess.Text) + CDbl(.Item("Debit", i).Value)
                End If

            Next
            lblAdd.Text = Format(CDbl(lblAdd.Text), numFormat)
            lblLess.Text = Format(CDbl(lblLess.Text), numFormat)
        End With
        If CDbl(lblbalance.Text) >= 0 Then
            'Dim debit As Double = CDbl(lbldebit.Text) - CDbl(lblLess.Text)
            lblBankAmount.Text = Format(CDbl(lblbalance.Text) - CDbl(lblLess.Text) + CDbl(lblAdd.Text), numFormat)
            lblDbText.Text = "Total Debit (Add)"
            lblCBText.Text = "Total Credit (Less)"
        Else
            lblBankAmount.Text = Format(CDbl(lblbalance.Text) - CDbl(lblAdd.Text) + CDbl(lblLess.Text), numFormat)
            lblDbText.Text = "Total Debit (Less)"
            lblCBText.Text = "Total Credit (Add)"
        End If

    End Sub
    Private Sub SetCumulativeBalance()
        If (grdVoucher.Rows.Count > 0) Then
            Dim Balance_Total As Double = 0
            For i = 0 To grdVoucher.Rows.Count - 1
                If (i - 1 >= 0) Then
                    Balance_Total = Val(grdVoucher.Item("Balance", i - 1).Value) + Val(grdVoucher.Item("Balance", i).Value)
                Else
                    Balance_Total = Val(grdVoucher.Item("Balance", i).Value)
                End If
                grdVoucher.Item("Balance", i).Value = Balance_Total
            Next
        End If
    End Sub
    Private Sub SetGrid_Head()

        With grdVoucher

            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = True
            .ColumnHeadersHeight = 100
            .RowTemplate.Height = 18
            ' .ColumnHeadersHeight = 200
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersDefaultCellStyle.BackColor = Color.Olive
            .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Arial", 9.0!, FontStyle.Bold)

            .Columns("Tag").HeaderText = "Tag"
            .Columns("Tag").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Tag").Width = 30
            .Columns("Tag").Visible = True
            .Columns("Tag").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("Tag").ReadOnly = True

            .Columns("ClearedDate").HeaderText = "Cleared Date"
            .Columns("ClearedDate").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("ClearedDate").Width = 100
            .Columns("ClearedDate").Visible = True
            .Columns("ClearedDate").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("ClearedDate").ReadOnly = False

            .Columns("JVNum").HeaderText = "JV Num"
            .Columns("JVNum").Width = 58
            .Columns("JVNum").Visible = True
            .Columns("JVNum").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("JVNum").ReadOnly = True

            .Columns("reference").HeaderText = "Reference"
            .Columns("reference").Width = 110
            .Columns("reference").Visible = True
            .Columns("reference").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("reference").ReadOnly = True

            .Columns("JVDate").HeaderText = "Vchr Date"
            .Columns("JVDate").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("JVDate").Width = 70
            .Columns("JVDate").Visible = True
            .Columns("JVDate").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("JVDate").ReadOnly = True

            .Columns("Debit").HeaderText = "Debit"
            .Columns("Debit").Width = 95
            .Columns("Debit").Visible = True
            .Columns("Debit").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Debit").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Debit").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("Debit").ReadOnly = True

            .Columns("Credit").HeaderText = "Credit"
            .Columns("Credit").Width = 95
            .Columns("Credit").Visible = True
            .Columns("Credit").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Credit").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Credit").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("Credit").ReadOnly = True

            .Columns("JVTYpe").HeaderText = "Type"
            .Columns("JVTYpe").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("JVTYpe").Width = 50
            .Columns("JVTYpe").Visible = True
            .Columns("JVTYpe").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("JVTYpe").ReadOnly = True

            .Columns("Description").HeaderText = "Description"
            .Columns("Description").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Description").Width = 200
            .Columns("Description").Visible = True
            .Columns("Description").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("Description").ReadOnly = True

            .Columns("ChqNo").HeaderText = "ChqNo"
            .Columns("ChqNo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("ChqNo").Width = 95
            .Columns("ChqNo").Visible = True
            .Columns("ChqNo").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("ChqNo").ReadOnly = True

            .Columns("ChqDate").HeaderText = "Chq Date"
            .Columns("ChqDate").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("ChqDate").Width = 70
            .Columns("ChqDate").Visible = True
            .Columns("ChqDate").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("ChqDate").ReadOnly = True

            .Columns("BankCode").HeaderText = "Bank"
            .Columns("BankCode").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("BankCode").Width = 50
            .Columns("BankCode").Visible = True
            .Columns("BankCode").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("BankCode").ReadOnly = True

            .Columns("unqno").Visible = False
            .Columns("unqno").ReadOnly = True

            setComboGrid()
        End With



    End Sub
    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        LoadProjectionReport(RptFlName, RptCaption)
    End Sub
    Private Sub Loadgrid()
        _clsStatementReport = New clsAccountTransaction

        Dim _FromDate As DateTime
        Dim _ToDate As DateTime
        Dim _BnkCode As String
        Dim _Type As Integer
        Dim _accountNo As Integer

        _accountNo = txtbankname.Tag
        If rdUncleared.Checked Then
            _Type = 0
        ElseIf rdCleared.Checked Then
            _Type = 1
        ElseIf rdAll.Checked Then
            _Type = 2
        Else
            _Type = 2
        End If
        _FromDate = DateValue(cldrdateFrom.Value)
        _ToDate = DateValue(clrDateTo.Value)
        _BnkCode = txtBankCode.Text

        _dtProjection = _clsStatementReport.returnReconciliationGrid(_FromDate, _ToDate, _BnkCode, _accountNo, _Type)
        grdVoucher.DataSource = _dtProjection
        SetGrid_Head()
        chkall.Checked = False
        'With grdVoucher
        '    For i = 0 To .Rows.Count - 1
        '        If .Item(ConstGrpSetOn, i).Value = "P.D.C.(I)" Then
        '            .Rows(i).DefaultCellStyle.BackColor = Color.AntiqueWhite
        '        End If
        '        If .Item(ConstGrpSetOn, i).Value = "P.D.C.(R) " Or .Item(ConstGrpSetOn, i).Value = "P.D.C.(R)" Then
        '            .Rows(i).DefaultCellStyle.BackColor = Color.LightCyan
        '        End If
        '        If .Item(ConstGrpSetOn, i).Value = "Bank" Or .Item(ConstGrpSetOn, i).Value = "Bank " Then
        '            .Rows(i).DefaultCellStyle.BackColor = Color.LavenderBlush
        '        End If
        '    Next


        'End With
        'BalanceTotal()
        'SetCumulativeBalance()
        BalanceTotal()
    End Sub
    Private Sub LoadDefaultDate()

        'Dim _dtDates As New DataTable
        '_dtDates = _objcmnbLayer._fldDatatable("Select AccPeriodFrm , AccPeriodTo from SysTb")
        'If _dtDates.Rows.Count > 0 Then
        '    ' cldrdateFrom.value = _dtDates(0)("AccPeriodFrm")
        '    'clrDateTo.value = DateTime.Now.Date

        'End If

    End Sub
    Private Sub LoadProjectionReport(ByVal FileName As String, ByVal RptCaption As String, Optional ByVal ToPrint As Boolean = False, Optional ByVal isFirst As Boolean = False)
        Try

            _clsStatementReport = New clsAccountTransaction
            Dim _dtProjectionReport As New DataTable
            Dim _FromDate As DateTime
            Dim _ToDate As DateTime
            Dim _BnkCode As String
            Dim _accountNo As Integer
            Dim _ds As New DataSet

            If Not frm Is Nothing Then 'shine
                frm.Close()
                frm = New ReprtviewNEWfrm
            Else
                frm = New ReprtviewNEWfrm
            End If
            _accountNo = txtbankname.Tag

            _FromDate = DateValue(cldrdateFrom.Value)
            _ToDate = DateValue(clrDateTo.Value)
            _BnkCode = txtBankCode.Text
            _ds = _clsStatementReport.returnReconciliationReport(_FromDate, _ToDate, _BnkCode, _accountNo)
            frm.MdiParent = fMainForm
            frm.SetReport(_ds, FileName, 0, False)
            frm.Show()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub setComboGrid()
        cmbSearch.Items.Clear()
        Dim i As Integer = 0
        For i = 0 To grdVoucher.ColumnCount - 1
            cmbSearch.Items.Add(grdVoucher.Columns(i).HeaderText)

        Next
        If cmbSearch.Items.Count > 0 Then
            cmbSearch.SelectedIndex = 1
        End If

        'If cmbSearch.Items.Count >= cmbShowIndex Then cmbSearch.SelectedIndex = cmbShowIndex
    End Sub
    Private Sub btn_Exit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Exit.Click
        Me.Close()
    End Sub
    Private Sub Update_ClearedDate()
        Dim _ClearedDate As DateTime
        Dim _UniqueNo As Integer
        _clsStatementReport = New clsAccountTransaction
        With grdVoucher
            If .Rows.Count > 0 Then
                For i = 0 To .Rows.Count - 1
                    _ClearedDate = DateValue(.Item("ClearedDate", i).Value)
                    _UniqueNo = Val(.Item("unqno", i).Value)
                    _clsStatementReport.updateClearedDate(_ClearedDate, _UniqueNo)
                Next
            End If
        End With
    End Sub
    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Try
            Update_ClearedDate()
        Catch ex As Exception

        End Try
        fRptFormat = New RptFormatfrm
        fRptFormat.RptType = "RCO"
        fRptFormat.ShowDialog()
        fRptFormat = Nothing
    End Sub

    Private Sub txtbtnEmpReportTextSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtProjectionSearch.TextChanged
        grdVoucher.DataSource = SearchGrid(_dtProjection, Trim(txtProjectionSearch.Text), cmbSearch.SelectedIndex, False)
        With grdVoucher
            'For i = 0 To .Rows.Count - 1
            '    If grdVoucher.Item(ConstGrpSetOn, i).Value = "P.D.C.(I)" Then
            '        .Rows(i).DefaultCellStyle.BackColor = Color.AntiqueWhite
            '    End If
            '    If .Item(ConstGrpSetOn, i).Value = "P.D.C.(R) " Or .Item(ConstGrpSetOn, i).Value = "P.D.C.(R)" Then
            '        .Rows(i).DefaultCellStyle.BackColor = Color.LightCyan
            '    End If
            '    If .Item(ConstGrpSetOn, i).Value = "Bank" Or .Item(ConstGrpSetOn, i).Value = "Bank " Then
            '        .Rows(i).DefaultCellStyle.BackColor = Color.LavenderBlush
            '    End If
            'Next


        End With
        ' SetCumulativeBalance()
    End Sub

    Private Sub btnRefresh1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh1.Click
        'If Not IsDate(cldrdateFrom.value) Then

        'End If

        Loadgrid()
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        Dim i As Integer
        For i = 0 To grdVoucher.RowCount - 1
            _objcmnbLayer._saveDatawithOutParm("UPDATE AcctrDet SET ClearedDate='" & Format(DateValue(grdVoucher.Item(1, i).Value), "yyyy/MM/dd") & "',RecntnTag=" & _
                                               IIf(grdVoucher.Item(0, i).Value = "Y", 1, 0) & " WHERE UnqNo=" & _
                                               Val(grdVoucher.Item("UnqNo", i).Value))
        Next
        MsgBox("Reconciliation Updated successfully", MsgBoxStyle.Information)
    End Sub

    Private Sub grdVoucher_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellClick
        If e.ColumnIndex = 0 Then
            If grdVoucher.Item(0, grdVoucher.CurrentRow.Index).Value = "Y" Then
                grdVoucher.Item(0, grdVoucher.CurrentRow.Index).Value = ""
            Else
                grdVoucher.Item(0, grdVoucher.CurrentRow.Index).Value = "Y"
            End If
        End If
        BalanceTotal()
    End Sub

    Private Sub chkall_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkall.CheckedChanged
        Dim i As Integer
        For i = 0 To grdVoucher.RowCount - 1
            If chkall.Checked Then
                grdVoucher.Item(0, i).Value = "Y"
            Else
                grdVoucher.Item(0, i).Value = ""
            End If
        Next
    End Sub

    Private Sub grdVoucher_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellContentClick

    End Sub

    Private Sub grdVoucher_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellEnter
        grdVoucher.BeginEdit(True)
    End Sub

    Private Sub rdUncleared_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdUncleared.CheckedChanged
        If rdUncleared.Checked Then
            Loadgrid()

        End If
    End Sub

    Private Sub rdCleared_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdCleared.CheckedChanged
        If rdCleared.Checked Then
            Loadgrid()

        End If
    End Sub

    Private Sub rdAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdAll.CheckedChanged
        If rdAll.Checked Then
            Loadgrid()

        End If
    End Sub

    Private Sub chkPrtDlg_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPrtDlg.CheckedChanged

    End Sub
End Class