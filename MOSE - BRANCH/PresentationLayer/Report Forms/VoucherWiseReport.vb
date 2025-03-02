Public Class VoucherWiseReport
#Region "Class Objects"
    Private _objcmnbLayer As New clsCommon_BL
    Private _objtr As New clsAccountTransaction

#End Region
#Region "Local Variables"
    Private dtTable As DataTable
    Private RptdtTable As DataTable
#End Region
#Region "Form Objects"
    Private WithEvents fRptFormat As RptFormatfrm
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fwait As WaitMessageFrm
#End Region


    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub ldGrid()
        Dim dsVrs As DataSet
        With _objtr
            .JVType = Trim(Mid(cmbVoucherTp.Text, 1, 3))
            .DateFrom = DateValue(cldrStartDate.Value)
            .DateTo = DateValue(cldrEnddate.Value)
            If rdovoucher.Checked Then
                If chkTally.Checked Then
                    If Trim(Mid(cmbVoucherTp.Text, 1, 3)) = "All" Then
                        .ptype = 5
                    ElseIf Trim(Mid(cmbVoucherTp.Text, 1, 3)) = "MA" Then
                        .ptype = 6
                    Else
                        .ptype = 2
                    End If
                Else
                    If Trim(Mid(cmbVoucherTp.Text, 1, 3)) = "All" Then
                        .ptype = 4
                    ElseIf Trim(Mid(cmbVoucherTp.Text, 1, 3)) = "MA" Then
                        .ptype = 6
                    Else
                        .ptype = 1
                    End If
                End If
            ElseIf rdoExpense.Checked Then
                .ptype = 3
            End If
        End With
        dsVrs = _objtr.returnVoucherReport
        dtTable = dsVrs.Tables(0)
        grdvoucher.DataSource = dtTable
        SetGridHead()
        setComboGrid()
        'Calculate(dsVrs.Tables(1))
        gridTotal(dsVrs.Tables(0))
    End Sub
    Private Sub gridTotal(ByVal RptdtTable As DataTable)
        lblTlDebit.Text = Format(0, numFormat)
        lblTlCredit.Text = Format(0, numFormat)
        lblDiff.Text = Format(0, numFormat)
        If RptdtTable.Rows.Count = 0 Then Exit Sub
        Dim drSum As Double
        Dim crSum As Double
        Dim amt As String
        amt = Trim(RptdtTable.Compute("SUM(Amount)", "Dtype='Dr'") & "")
        If Val(amt) > 0 Then
            drSum = Convert.ToDouble(amt)
        End If
        amt = Trim(RptdtTable.Compute("SUM(Amount)", "Dtype='Cr'") & "")
        If Val(amt) > 0 Then
            crSum = Convert.ToDouble(amt)
        End If

        'crSum = Convert.ToDouble(RptdtTable.Compute("SUM(Amount)", "Dtype='Cr'"))
        lblTlDebit.Text = Format(drSum, numFormat)
        lblTlCredit.Text = Format(crSum, numFormat)
        lblDiff.Text = Format(CDbl(lblTlDebit.Text) - CDbl(lblTlCredit.Text), numFormat)
    End Sub
    Private Function ldForReport() As DataTable
        With _objtr
            .JVType = Mid(cmbVoucherTp.Text, 1, 3)
            .DateFrom = DateValue(cldrStartDate.Value)
            .DateTo = DateValue(cldrEnddate.Value)
            If rdovoucher.Checked Then
                If chkTally.Checked Then
                    If Mid(cmbVoucherTp.Text, 1, 3) = "All" Then
                        .ptype = 5
                    Else
                        .ptype = 2
                    End If
                Else
                    If Mid(cmbVoucherTp.Text, 1, 3) = "All" Then
                        .ptype = 4
                    Else
                        .ptype = 1
                    End If
                End If
            ElseIf rdoExpense.Checked Then
                .ptype = 3
            End If
        End With
        Return _objtr.returnVoucherReport.Tables(0)
    End Function
    Private Sub Calculate(ByVal srs As DataTable)
        Try

            If srs.Rows.Count = 0 Then
                lblTlCredit.Text = Format(0, "#,##0" & IIf(NoOfDecimal = 0, "", "." & Strings.StrDup(NoOfDecimal, "0")))
                lblTlDebit.Text = Format(0, "#,##0" & IIf(NoOfDecimal = 0, "", "." & Strings.StrDup(NoOfDecimal, "0")))
                lblDiff.Text = Format(0, "#,##0" & IIf(NoOfDecimal = 0, "", "." & Strings.StrDup(NoOfDecimal, "0")))
            Else
                If srs.Rows.Count = 0 Then
                    lblTlCredit.Text = Format(0, "#,##0" & IIf(NoOfDecimal = 0, "", "." & Strings.StrDup(NoOfDecimal, "0")))
                    lblTlDebit.Text = Format(0, "#,##0" & IIf(NoOfDecimal = 0, "", "." & Strings.StrDup(NoOfDecimal, "0")))
                Else
                    If IsDBNull(srs(0)("Credit")) Then
                        lblTlCredit.Text = Format(0, "#,##0" & IIf(NoOfDecimal = 0, "", "." & Strings.StrDup(NoOfDecimal, "0")))
                    Else
                        lblTlCredit.Text = Format(srs(0)("Credit"), "#,##0" & IIf(NoOfDecimal = 0, "", "." & Strings.StrDup(NoOfDecimal, "0")))
                    End If
                    If IsDBNull(srs(0)("Debit")) Then
                        lblTlDebit.Text = Format(0, "#,##0" & IIf(NoOfDecimal = 0, "", "." & Strings.StrDup(NoOfDecimal, "0")))
                    Else
                        lblTlDebit.Text = Format(srs(0)("Debit"), "#,##0" & IIf(NoOfDecimal = 0, "", "." & Strings.StrDup(NoOfDecimal, "0")))
                    End If
                End If
            End If
            lblDiff.Text = Format(CDbl(lblTlDebit.Text) - CDbl(lblTlCredit.Text), "#,##0" & IIf(NoOfDecimal = 0, "", "." & Strings.StrDup(NoOfDecimal, "0")))
            'btnLoad.Enabled = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub SetGridHead()
        'If Me.IsInitializing Then Exit Sub
        With grdvoucher

            SetGridProperty(grdvoucher)

            .Columns("VchrDate").HeaderText = "JVDate"
            .Columns("VchrDate").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns("JVType").HeaderText = "JVType"
            .Columns("JVType").Width = (Me.Width / 2) * 9 / 100   '100
            .Columns("JVType").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns("Branch").HeaderText = "PreFix"
            .Columns("Branch").Width = 50
            .Columns("Branch").Visible = False

            .Columns("Reference").HeaderText = "Reference"
            .Columns("Reference").Width = 80

            .Columns("PreFix").HeaderText = "PreFix"
            .Columns("PreFix").Width = 50

            .Columns("VchrNo").HeaderText = "JVNum"
            .Columns("VchrNo").Width = 50
            .Columns("VchrNo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns("DType").HeaderText = "DType"
            .Columns("DType").Width = (Me.Width / 2) * 8 / 100   '100
            .Columns("DType").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns("Amount").HeaderText = "Amount"
            .Columns("Amount").Width = (Me.Width / 2) * 18 / 100 '4
            .Columns("Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Amount").DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns("Alias").HeaderText = "Alias"
            .Columns("Alias").Width = (Me.Width / 2) * 12 / 100 '40

            .Columns("AccDescr").HeaderText = "AccDescr"
            .Columns("AccDescr").Width = (Me.Width / 2) * 30 / 100 '50

            .Columns("Description").HeaderText = "EntryRef"
            .Columns("Description").Width = (Me.Width / 2) * 30 / 100 '70

            .Columns("JobCode").HeaderText = "JobCode"
            .Columns("JobCode").Width = (Me.Width / 2) * 14 / 100 '70
            .Columns("JobCode").Visible = False


            .Columns("ChqNo").HeaderText = "ChqNo"
            .Columns("ChqNo").Width = (Me.Width / 2) * 14 / 100 '70

            .Columns("ChqDate").HeaderText = "ChqDate"
            .Columns("ChqDate").Width = (Me.Width / 2) * 14 / 100 '70
            .Columns("ChqDate").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns("Bank").HeaderText = "Bank"
            .Columns("Bank").Width = (Me.Width / 2) * 14 / 100 '70

            .Columns("UserId").HeaderText = "UserId"
            .Columns("UserId").Width = (Me.Width / 2) * 14 / 100 '7

            .Columns("unqNo").HeaderText = "unqNo"
            .Columns("unqNo").Visible = False
            .Columns("JVNum").Visible = False
            .Columns("JVdate1").Visible = False
            .Columns("JVdate2").Visible = False
            .Columns("Linkno").Visible = False
            .Columns("lnk").Visible = False
            .Columns("vrdescr").Visible = False


        End With
    End Sub
    Private Sub setComboGrid()
        cmbcolms.Items.Clear()
        Dim i As Integer = 0
        For i = 0 To grdvoucher.ColumnCount - 1
            cmbcolms.Items.Add(grdvoucher.Columns(i).HeaderText)
        Next
        If cmbcolms.Items.Count > 0 Then cmbcolms.SelectedIndex = 0
    End Sub

    Private Sub txtSeq_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        grdvoucher.DataSource = SearchGrid(dtTable, Trim(txtSearch.Text), cmbcolms.SelectedIndex, Not chkSearch.Checked)
        RptdtTable = SearchGrid(dtTable, Trim(txtSearch.Text), cmbcolms.SelectedIndex, Not chkSearch.Checked)
        gridTotal(RptdtTable)
    End Sub

    Private Sub VoucherWiseReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'cldrStartDate.Value = Format(DateAdd(DateInterval.Day, -30, DateValue(Date.Now)), DtFormat)
        If enableTemple Then
            cmbVoucherTp.Items.Add("TIS - Temple Sales Invoice")
        End If
        cmbVoucherTp.Items.Add("All Vouchers")
        If Not rdoExpense.Checked Then
            cmbVoucherTp.SelectedIndex = 0
            'ldGrid()
            loadWaite(1)
        End If
        If CurrentUser = "PROGRAMMAR" Then
            btndelete.Visible = True
        Else
            btndelete.Visible = False
        End If
    End Sub

    Private Sub cmbVoucherTp_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVoucherTp.SelectedIndexChanged
        'ldGrid()
        loadWaite(1)
    End Sub

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        'ldGrid()
        loadWaite(1)
    End Sub

    Private Sub chkTally_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTally.CheckedChanged
        'ldGrid()
        loadWaite(1)
    End Sub


    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        fRptFormat = New RptFormatfrm
        If rdoExpense.Checked Then
            fRptFormat.RptType = "ER"
        Else
            fRptFormat.RptType = "VL"
        End If
        fRptFormat.ShowDialog()
        fRptFormat = Nothing
    End Sub

    Private Sub fRptFormat_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fRptFormat.FormClosed
        fRptFormat = Nothing
    End Sub

    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        If Not frm Is Nothing Then
            frm.Close()
            frm = New ReprtviewNEWfrm
        Else
            frm = New ReprtviewNEWfrm
        End If
        Dim ds As New DataSet
        Dim dt As DataTable
        If RptdtTable Is Nothing Then
            dt = ldForReport()
            RptdtTable = dt.Copy
            'ds.Tables.Add(dt)
        Else
            'ds.Tables.Add(RptdtTable)
        End If
        ds.Tables.Add(RptdtTable)
        RptdtTable = Nothing
        frm.SetReport(ds, RptFlName, 0, False)
        frm.MdiParent = Me.MdiParent
        frm.Text = "Voucher Wise Report " & RptCaption
        frm.Show()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        If grdvoucher.RowCount = 0 Then Exit Sub
        If MsgBox("Do you want to Remove the current Transfer?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.No Then Exit Sub
        Dim linkno As Long
        Dim i As Integer
        i = grdvoucher.CurrentCell.RowIndex
        linkno = Val(grdvoucher.Item("Linkno", i).Value)
        _objtr.deleteAccountTransaction(linkno)
        'ldGrid()
        loadWaite(1)
    End Sub

    Private Sub rdoExpense_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoExpense.Click, rdovoucher.Click
        Dim myctrl As RadioButton
        myctrl = sender
        Select Case myctrl.Name
            Case "rdoExpense"
                plvoucher.Visible = False
            Case "rdovoucher"
                plvoucher.Visible = True
        End Select
        loadWaite(1)
        If rdoExpense.Checked Then
            Label3.Visible = False
            Label5.Visible = False
            lblDiff.Visible = False
            lblTlCredit.Visible = False
            Label2.Text = "Total"
            'rdoExpense.Tag = "ld"
        Else
            Label3.Visible = True
            Label5.Visible = True
            lblDiff.Visible = True
            lblTlCredit.Visible = True
            Label2.Text = "Debit"
        End If
    End Sub
    Private Sub fwait_triggerEvent() Handles fwait.triggerEvent
        Select Case fwait.triggerType
            Case 1
                ldGrid()
        End Select
        If Not fwait Is Nothing Then
            fwait.Close()
            fwait = Nothing
        End If

    End Sub
    Private Sub loadWaite(ByVal triggerType As Integer)
        If Not fwait Is Nothing Then fwait.Close() : fwait = Nothing
        fwait = New WaitMessageFrm
        With fwait
            .triggerType = triggerType
            .Show()
        End With
    End Sub
End Class