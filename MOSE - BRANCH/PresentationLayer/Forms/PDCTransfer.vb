Public Class PDCTransfer
#Region "Class Objet declarations"
    Private _objcmnbLayer As New clsCommon_BL
    Private _objTr As New clsAccountTransaction
#End Region
#Region "Local Variables"
    Private dtTable As DataTable
    Private chgbyprg As Boolean
    Private activecontrolname As String
#End Region
#Region "Constant Variables"
    Private Const constAmount = 0
    Private Const constChq = 1
    Private Const constChqDt = 2
    Private Const constbank = 3
    Private Const constT = 4
    Private Const constref = 5
    Private Const constDCBank = 6
    Private Const constCustomer = 7
    Private Const constPDC = 8
    Private Const constJvdt = 9
    Private Const constjvtp = 10
    Private Const constInvno = 11
    Private Const constJVRef = 12
    Private Const constUniqno = 13
    Private Const constCustAcc = 14
    Private Const constPDCAcc = 15
    Private Const constlinkno = 16
    Private Const constBankid = 17
#End Region
#Region "Form Objects"
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fRptFormat As RptFormatfrm
    Private WithEvents fSelect As Selectfrm
#End Region
    Private Sub PDCTransfer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loadGrid()
        Timer1.Enabled = True
    End Sub
    Public Sub loadGrid()
        With _objTr
            .DateFrom = cldrStartDate.Value
            .DateTo = cldrEnddate.Value
            If rdoAll.Checked Then
                .ptype = 0
            ElseIf rdochqdate.Checked Then
                .ptype = 1
            ElseIf rdotransaction.Checked Then
                .ptype = 2
            Else
                .ptype = 3
            End If
            dtTable = .returnPDCTransfer(IIf(optPdc.Checked, 1, 0), IIf(rdoclearList.Checked, 1, 0)).Tables(0)
            grdvoucher.DataSource = dtTable
            SetHead()
            If rdoclearList.Checked Then
                btntransfer.Text = "Unod Tr."
                Panel1.Visible = False
            Else
                btntransfer.Text = "Transfer"
                Panel1.Visible = True
            End If
            Dim amt As String
            amt = Trim(dtTable.Compute("SUM(DealAmt)", "") & "")
            If Val(amt) > 0 Then
                lblTlDebit.Text = Format(CDbl(amt), numFormat)
            End If

        End With
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
                    If msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up) Or msg.WParam.ToInt32() = CInt(Keys.Enter) Or msg.WParam.ToInt32() = CInt(Keys.F2) Or msg.WParam.ToInt32() = CInt(Keys.F3) Or msg.WParam.ToInt32() = CInt(Keys.F4) Then
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
    Private Sub setAcctrCmnValue(ByVal vrType As String, ByVal vrNo As Integer, ByVal pdcid As Long)
        _objTr.JVType = vrType
        _objTr.JVDate = DateValue(dtptrdate.Value)
        _objTr.PreFix = ""
        _objTr.JVNum = vrNo
        _objTr.JVTypeNo = getVouchernumber(vrType)
        _objTr.UserId = CurrentUser
        _objTr.MchName = MACHINENAME
        _objTr.CrtDtTm = DateValue(Date.Now)
        _objTr.TypeNo = 0 ' id number from prefixtb
        _objTr.VrDescr = "PDC Transfer"
        _objTr.IsModi = 0
        _objTr.LinkNo = 0
        _objTr.isdeleteTr = 1
        _objTr.pdcid = pdcid
    End Sub
    Private Sub setAcctrDetValue(ByVal lnkNo As Long, ByVal _row As Integer, ByVal Accountno As Integer, ByVal Dtype As Integer)
        Dim fcrt As Double
        Dim dtrow As DataRow
        dtrow = dtAccTb.NewRow
        dtrow("LinkNo") = lnkNo
        dtrow("AccountNo") = Accountno
        dtrow("Reference") = Trim(grdvoucher.Item(constref, _row).Value & "")
        dtrow("EntryRef") = "PDC Transfer Chq: " & Trim(grdvoucher.Item(constChq, _row).Value & "") & " Party: " & Trim(grdvoucher.Item(constCustomer, _row).Value & "")
        dtrow("DealAmt") = CDbl(grdvoucher.Item(constAmount, _row).Value) * IIf(Dtype = 1, 1, -1)
        If Val(grdvoucher.Item("PdcRate", _row).Value & "") = 0 Then grdvoucher.Item("PdcRate", _row).Value = 1
        fcrt = CDbl(grdvoucher.Item("PdcRate", _row).Value)
        dtrow("FCAmt") = dtrow("DealAmt") * fcrt
        dtrow("CurrencyCode") = Trim(grdvoucher.Item("PDcCur", _row).Value & "")
        dtrow("CurrRate") = fcrt
        dtrow("TrInf") = 0
        dtrow("PDCAcc") = Val(grdvoucher.Item(constPDCAcc, _row).Value & "")
        dtrow("CustAcc") = Val(grdvoucher.Item(constCustAcc, _row).Value & "")
        If chkDate(grdvoucher.Item(constChqDt, _row).Value) Then
            dtrow("ChqDate") = DateValue(grdvoucher.Item(constChqDt, _row).Value)
        End If
        dtrow("ChqNo") = grdvoucher.Item(constChq, _row).Value
        dtrow("BankCode") = grdvoucher.Item(constbank, _row).Value
        dtrow("UnqNo") = 0
        dtAccTb.Rows.Add(dtrow)
        Dim j As Integer
        Dim dataType As String
        For j = 0 To dtAccTb.Columns.Count - 1
            dataType = dtAccTb.Columns(j).DataType.Name
            If Trim(dtAccTb(dtAccTb.Rows.Count - 1)(j) & "") = "" Then
                Select Case dataType
                    Case "String"
                        dtAccTb(dtAccTb.Rows.Count - 1)(j) = ""
                    Case "Int64", "Int32", "Double", "Decimal", "Boolean"
                        dtAccTb(dtAccTb.Rows.Count - 1)(j) = 0
                End Select
            End If
nxt:
        Next



        'With _objTr

        '    .trLinkNo = lnkNo
        '    .AccountNo = Accountno
        '    .Reference = Trim(grdvoucher.Item(constref, _row).Value & "")
        '    .EntryRef = "PDC Transfer Chq: " & Trim(grdvoucher.Item(constChq, _row).Value & "") & " Party: " & Trim(grdvoucher.Item(constCustomer, _row).Value & "")
        '    .DealAmt = CDbl(grdvoucher.Item(constAmount, _row).Value) * IIf(Dtype = 1, 1, -1)
        '    If Val(grdvoucher.Item("PdcRate", _row).Value & "") = 0 Then grdvoucher.Item("PdcRate", _row).Value = 1
        '    fcrt = CDbl(grdvoucher.Item("PdcRate", _row).Value)
        '    .FCAmt = .DealAmt * fcrt
        '    .JobCode = ""
        '    .JobStr = ""
        '    .CurrRate = fcrt 'CDbl(grdvoucher.Item(ConstFCRate, _row).Value)
        '    .CurrencyCode = Trim(grdvoucher.Item("PDcCur", _row).Value & "")
        '    .TrInf = 0
        '    .OthCost = 0
        '    .TermsId = ""
        '    .CustAcc = 0
        '    .AccWithRef = 0
        '    .BankCode = grdvoucher.Item(constbank, _row).Value
        '    .ChqNo = grdvoucher.Item(constChq, _row).Value
        '    If chkDate(grdvoucher.Item(constChqDt, _row).Value) Then
        '        .ChqDate = DateValue(grdvoucher.Item(constChqDt, _row).Value)
        '    End If
        '    .PDCAcc = Val(grdvoucher.Item(constCustAcc, _row).Value & "")
        '    .LPONo = ""
        '    .DocDate = Date.Now
        '    .SuppInvDate = Date.Now
        '    .DueDate = Date.Now
        '    .VesselId = ""
        'End With
    End Sub
    Private Sub SetHead()
        With grdVoucher
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = True
            .EditMode = DataGridViewEditMode.EditProgrammatically
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersDefaultCellStyle.BackColor = Color.Olive
            .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular)
            .RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 9.0!)
            If rdoclearList.Checked Then
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            Else
                .SelectionMode = DataGridViewSelectionMode.CellSelect
            End If
      
            .Columns(constAmount).HeaderText = "BC Amount"
            .Columns(constAmount).Width = Me.Width * 7.5 / 100   '100
            .Columns(constAmount).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(constAmount).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(constAmount).ReadOnly = True

            .Columns(Constchq).HeaderText = "Cheq#"
            .Columns(Constchq).Width = Me.Width * 7.5 / 100   '100
            .Columns(Constchq).ReadOnly = True

            .Columns(constChqDt).Width = 75
            .Columns(constChqDt).HeaderText = "C Date"
            .Columns(constChqDt).ReadOnly = True

            .Columns(constbank).HeaderText = "Bank"
            .Columns(constbank).Width = 35
            .Columns(constbank).ReadOnly = True

            .Columns(constT).HeaderText = IIf(rdoclearList.Checked, "UndoTr?", "Transfer?")
            .Columns(constT).Width = 70
            .Columns(constT).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(constT).ReadOnly = True
            .Columns(constT).DefaultCellStyle.BackColor = Color.LightYellow
            .Columns(constT).Visible = Not rdoclearList.Checked

            .Columns(constref).HeaderText = "Trn Ref "
            .Columns(constref).Width = 75
            .Columns(constref).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(constref).ReadOnly = False
            .Columns(constref).Visible = Not rdoclearList.Checked

            .Columns(constDCBank).HeaderText = "Bank A/c." & IIf(optpdcIs.Checked, "(Cr)", "(Dr)")
            .Columns(constDCBank).Width = Me.Width * 10 / 100   '100
            .Columns(constDCBank).SortMode = DataGridViewColumnSortMode.NotSortable
            '   .Columns(constbankName).DefaultCellStyle.BackColor = Color.LightYellow
            If optpdcIs.Checked Then
                .Columns(constCustomer).HeaderText = "Supp A/c Head"
            Else
                .Columns(constCustomer).HeaderText = "Cust A/c Head"
            End If
            .Columns(constCustomer).Width = Me.Width * 10 / 100   '100
            .Columns(constCustomer).ReadOnly = True

            .Columns(constPDC).HeaderText = "PDC A/c." & IIf(optpdcIs.Checked, "(Dr)", "(Cr)")
            .Columns(constpdc).Width = Me.Width * 10 / 100   '100
            .Columns(constpdc).ReadOnly = True

            .Columns(constJvdt).Width = Me.Width * 6 / 100   '100
            .Columns(constJvdt).HeaderText = "V Date"
            .Columns(constJvdt).Width = 75
            .Columns(constJvdt).ReadOnly = True

            .Columns(constjvtp).HeaderText = "Type"
            .Columns(constjvtp).Width = 50
            .Columns(constjvtp).ReadOnly = True
            .Columns(constjvtp).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns(constInvno).HeaderText = "VrNum"
            .Columns(constInvno).Width = 85
            .Columns(constInvno).ReadOnly = True

            .Columns(constJVRef).HeaderText = IIf(Not optpdcIs.Checked, "DB No", "CB No")
            .Columns(constJVRef).Width = 75
            .Columns(constJVRef).ReadOnly = True
            .Columns(constJVRef).Visible = rdoclearList.Checked

            .Columns(constUniqno).HeaderText = "Unique No"
            .Columns(constUniqno).Width = Me.Width * 5 / 100   '100
            .Columns(constUniqno).Visible = False

            .Columns(constCustAcc).HeaderText = "CustID"
            .Columns(constCustAcc).Width = Me.Width * 5 / 100   '100
            .Columns(constCustAcc).Visible = False

            .Columns(constBankid).HeaderText = "BankAcc"
            .Columns(constBankid).Width = Me.Width * 5 / 100   '100
            .Columns(constBankid).Visible = False

            .Columns(constPDCAcc).HeaderText = "AccountNo"
            .Columns(constPDCAcc).Width = Me.Width * 5 / 100   '100
            .Columns(constPDCAcc).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(constPDCAcc).Visible = False

            .Columns(ConstLinkno).HeaderText = "Linkno"
            .Columns(ConstLinkno).Width = Me.Width * 5 / 100   '100
            .Columns(ConstLinkno).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(constlinkno).Visible = False
            .Columns("lnk").Visible = False
            .Columns("datefrom").Visible = False
            .Columns("dateto").Visible = False
            .Columns("pdcType").Visible = False
            .Columns("pdcstatus").Visible = False
            .Columns("rptType").Visible = False
            .Columns("PDcCur").Visible = False
            .Columns("PdcRate").Visible = False
            .Columns("PdcDec").Visible = False
            .Columns("dbcbnos").Visible = False

            .Columns("trfrDate").Visible = rdoclearList.Checked
            .Columns("trfrDate").Width = 75
            .Columns("trfrDate").HeaderText = "Trfr-Date"
            .Columns("trfrDate").ReadOnly = True
            Dim i As Integer
            Dim TbCmb As New DataTable
            For i = 1 To constJVRef
                cmbcolms.Items.Add(.Columns(i).HeaderText)
            Next
            cmbcolms.SelectedIndex = 0
        End With
        resizeGridColumn(grdvoucher, constDCBank)
    End Sub
    Private Sub BeginEdit()
        chgbyprg = True
        With grdVoucher
            If .RowCount = 0 Then Exit Sub
            activecontrolname = "grdVoucher"
            .BeginEdit(True)

        End With
        chgbyprg = False
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        loadGrid()
    End Sub

    Private Sub optPdc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optPdc.CheckedChanged

    End Sub

    Private Sub optPdc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optPdc.Click, optpdcIs.Click
        loadGrid()
    End Sub

    Private Sub PDCTransfer_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Timer1.Enabled = True
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        resizeGridColumn(grdvoucher, constDCBank)
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Dim RptType As String = ""
        Dim RptCaption As String = ""
        RptType = "PDL"
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
        With _objTr
            .DateFrom = cldrStartDate.Value
            .DateTo = cldrEnddate.Value
            If rdoAll.Checked Then
                .ptype = 0
            ElseIf rdochqdate.Checked Then
                .ptype = 1
            Else
                .ptype = 2
            End If
            ds = .returnPDCTransfer(IIf(optPdc.Checked, 1, 0), IIf(rdoclearList.Checked, 1, 0))
        End With
        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = fMainForm
        frm.Text = IIf(RptCaption = "", "PDC Reports", RptCaption)
        frm.Show()
    End Sub

    Private Sub grdvoucher_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdvoucher.CellClick
        If e.ColumnIndex = constref Then
            BeginEdit()
        End If
    End Sub

    Private Sub grdvoucher_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdvoucher.CellDoubleClick
        With grdvoucher
            If e.ColumnIndex = constT Then
                .Item(constT, .CurrentRow.Index).Value = IIf(.Item(constT, .CurrentRow.Index).Value = "Yes", "No", "Yes")
            End If
        End With
    End Sub

    Private Sub btntransfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btntransfer.Click
        If btntransfer.Text = "Transfer" Then
            If MsgBox("Do you want to Transfer the current Entry?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.No Then Exit Sub
            saveTransaction()
        Else
            If MsgBox("Do you want to Undo the current Transfer?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.No Then Exit Sub
            undoTransfer()
        End If
        loadGrid()
    End Sub
    Private Sub undoTransfer()
        Dim i As Integer
        i = grdvoucher.CurrentCell.RowIndex
        Dim linkno As Long
        linkno = Val(grdvoucher.Item("dbcbnos", i).Value)
        _objTr.deleteAccountTransaction(linkno)
        _objcmnbLayer._saveDatawithOutParm("update AccTrDet set DBCBNo=0 WHERE UnqNo=" & Val(grdvoucher(constUniqno, i).Value))
    End Sub
    Private Sub saveTransaction()
        Dim VchrNo As Integer
        Dim VoucherTp As String = IIf(optPdc.Checked, "DB", "CB")
        Dim accno As Integer
        Dim vrInvoice As String = ""
        Dim vrPrefix As String = ""
        Dim a, b As String
        Try
            a = ""
            b = ""
            getVrsDet(0, VoucherTp, "", vrInvoice, a, b)
            VchrNo = Val(vrInvoice)
            Dim LinkNo As Long
            Dim i As Integer
            Dim found As Boolean
            With grdvoucher
                For i = 0 To .RowCount - 1
                    If Val(grdvoucher.Item(constAmount, i).Value & "") = 0 Then GoTo nxt
                    If Val(grdvoucher.Item(constBankid, i).Value & "") = 0 Then GoTo nxt
                    If Val(grdvoucher.Item(constPDCAcc, i).Value & "") = 0 Then GoTo nxt
                    If UCase(grdvoucher.Item(constT, i).Value) = UCase("No") Then GoTo nxt
chkagain:
                    If Not CheckNoExists("", Val(VchrNo), VoucherTp, "Accounts") Then
                        VchrNo = VchrNo + 1
                        GoTo chkagain
                    End If
                    found = True
                    setAcctrCmnValue(VoucherTp, VchrNo, Val(grdvoucher(constUniqno, i).Value))
                    If dtAccTb.Rows.Count > 0 Then dtAccTb.Rows.Clear()
                    'LinkNo = Val(_objTr.SaveAccTrCmn())
                    'PDC R
                    If optPdc.Checked Then
                        'Debit Account
                        accno = Val(grdvoucher.Item(constBankid, i).Value & "")
                        setAcctrDetValue(LinkNo, i, accno, 1)
                        '_objTr.saveAccTrans()

                        'Credit Account
                        accno = Val(grdvoucher.Item(constPDCAcc, i).Value & "")
                        setAcctrDetValue(LinkNo, i, accno, 0)
                        '_objTr.saveAccTrans()
                    Else ' PDC ISSUED
                        'Debit Account
                        accno = Val(grdvoucher.Item(constPDCAcc, i).Value & "")
                        setAcctrDetValue(LinkNo, i, accno, 1)
                        '_objTr.saveAccTrans()

                        'Credit Account
                        accno = Val(grdvoucher.Item(constBankid, i).Value & "")
                        setAcctrDetValue(LinkNo, i, accno, 0)
                        '_objTr.saveAccTrans()
                    End If
                    _objTr.SaveAccTrWithDt(dtAccTb)
                    '_objcmnbLayer._saveDatawithOutParm("update AccTrDet set DBCBNo=" & LinkNo & " WHERE UnqNo=" & Val(grdvoucher(constUniqno, i).Value))
                    VchrNo = VchrNo + 1
nxt:
                Next
            End With
            If Not found Then MsgBox("Vouchers not found to transfer", MsgBoxStyle.Exclamation) : Exit Sub
            Dim numVchrNo As New TextBox
            numVchrNo.Text = VchrNo
            SetNextVrNo(numVchrNo, 0, VoucherTp, "JvType = '" & VoucherTp & "' AND JvNum = ", True, True, True)
            MsgBox(VoucherTp & " Saved Successfully", MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox("Proc: saveTransaction" & vbCrLf & ex.Message, , "Postinng Falid")
        End Try
    End Sub
    Private Sub ldSelect(ByVal BVal As Single, Optional ByVal isOther As Boolean = False)
        fSelect = New Selectfrm
        If isOther Then
            getGLSQL(fSelect, BVal)
        Else
            fSelect.Location = New Point(650, 150)
            fSelect.StartPosition = FormStartPosition.CenterScreen
            SetForm(fSelect, BVal)
        End If
        fSelect.Show()
    End Sub
    Private Sub getGLSQL(ByVal frm As Selectfrm, ByVal col As Byte)
        Dim getGLSQLstr As String = ""
        Try
            getGLSQLstr = "SELECT AccMast.AccDescr As [Account Name],AccMast.Alias,AccMast.accid FROM " & _
                                   "S1AccHd INNER JOIN AccMast ON S1AccHd.S1AccId = AccMast.S1AccId " & _
                                   "LEFT JOIN CurrencyTb ON AccMast.CurrencyCode = CurrencyTb.CurrencyCode "
            Select Case col
                Case 1
                    getGLSQLstr = getGLSQLstr & " WHERE AccMast.S1AccId Between 1000 And 1999"
                Case 6, 8
                    getGLSQLstr = getGLSQLstr & " WHERE S1AccHd.GrpSetOn='Bank'"
                Case 7
                    getGLSQLstr = getGLSQLstr & " WHERE S1AccHd.GrpSetOn='P.D.C.(R)'"
                Case 6
                    getGLSQLstr = AssignAccSQLStr(3)

                Case 5
                    getGLSQLstr = getGLSQLstr & " WHERE S1AccHd.GrpSetOn='Cash'"
                    'Case 2
                    '    getGLSQLstr = getGLSQLstr '& " WHERE AccMast.S1AccId Between 2000 And 2999"
                Case 2, 3, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18
                    getGLSQLstr = AssignAccSQLStr(8)
                Case 4
                    getGLSQLstr = AssignAccSQLStr(5)

            End Select
            frm.Location = New Point(650, 150)
            frm.pnlRadios.Visible = False
            frm.Width = 516
            frm.strMyQry = getGLSQLstr
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub grdvoucher_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdvoucher.GotFocus
        activecontrolname = "grdVoucher"
    End Sub
    Private Sub grdvoucher_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdvoucher.KeyDown
        If e.KeyCode = Keys.F2 Then
            ldSelect(6, True)
        End If
    End Sub

    Private Sub fSelect_doSelect(ByVal KeyId As String, ByVal strFld1 As String, ByVal strFld2 As String) Handles fSelect.doSelect
        chgbyprg = True
         If activecontrolname = "grdVoucher" Then
            With grdvoucher
                .Item(constBankid, .CurrentRow.Index).Value = KeyId
                .Item(.CurrentCell.ColumnIndex, .CurrentRow.Index).Value = strFld1
            End With
        End If
        chgbyprg = False
    End Sub

    Private Sub grdvoucher_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdvoucher.Leave
        activecontrolname = ""
    End Sub


    Private Sub rdopdclist_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdopdclist.Click, rdoclearList.Click
        rdotrdate.Visible = Not rdopdclist.Checked
        loadGrid()
    End Sub

 
    Private Sub btnfind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnfind.Click

    End Sub
End Class