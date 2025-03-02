Public Class JournalVoucher
#Region "Constant Declaration"
    'const declarations
    Private Const ConstAlias = 0
    Private Const ConstName = 1
    Private Const ConstReference = 2
    Private Const ConstDescr = 3
    Private Const ConstDtype = 4
    Private Const ConstAmount = 5
    Private Const ConstFCName = 6
    Private Const ConstFCRate = 7
    Private Const ConstFCAmount = 8
    Private Const ConstJob = 9
    Private Const Constchq = 10
    Private Const ConstChqdate = 11
    Private Const ConstBank = 12
    Private Const ConstPdcCustAc = 13
    Private Const ConstLPO = 14
    Private Const ConstLpoDate = 15
    Private Const ConstPdcCustAcno = 16
    Private Const ConstFCDec = 17
    Private Const ConstAccountNo = 18
    Private Const ConstBrId = 19
    Private Const ConstGrpSetOn = 20
    Private Const ConstUnq = 21
    Private Const Constinsmntid = 22
    'LIST GRID CONSTANT variables
    Private Const ConstInvNo = 0
    Private Const ConstTrdate = 1
    Private Const ConstInvAmount = 2
    Private Const ConstCustId = 3
    Private Const ConstCustname = 4
    Private Const ConstTrRef = 5
    Private Const Consttype = 6
    Private Const ConstLinkNo = 7
#End Region
#Region "Class Objet declarations"
    Private _objcmnbLayer As New clsCommon_BL
    Private _objTr As New clsAccountTransaction
#End Region
#Region "Public variables"
    Public isModi As Boolean
#End Region
#Region "Local Variable"
    Private activecontrolname As String
    Private chgbyprg As Boolean
    Private strGridSrchString As String
    Private _srchIndexId As Integer
    Private _srchTxtId As Integer
    Private SrchText As String
    Private _NewRw As Boolean
    Private ChgId As Boolean
    Private _srchOnce As Boolean
    Private loadedTrId As Long
    Private dtTable As DataTable
    Private RptdtTable As DataTable
#End Region
#Region "Form Object declarations"
    Private WithEvents fSelect As Selectfrm
    Private WithEvents fRptFormat As RptFormatfrm
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fwait As WaitMessageFrm
#End Region
    Sub SetGridHead()
        With grdVoucher
            SetEntryGridProperty(grdVoucher)
            .ColumnCount = 22
            

            .Columns(ConstAlias).HeaderText = "Alias"
            .Columns(ConstAlias).Width = 100
            .Columns(ConstAlias).SortMode = DataGridViewColumnSortMode.NotSortable

            .Columns(ConstName).HeaderText = "Account Name"
            .Columns(ConstName).Width = 200
            .Columns(ConstName).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstName).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

            .Columns(ConstReference).HeaderText = "Reference"
            .Columns(ConstReference).Width = 100
            .Columns(ConstReference).SortMode = DataGridViewColumnSortMode.NotSortable
            CType(.Columns(ConstReference), DataGridViewTextBoxColumn).MaxInputLength = 25

            .Columns(ConstDescr).HeaderText = "Description"
            .Columns(ConstDescr).Width = 100
            .Columns(ConstDescr).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstDescr).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            CType(.Columns(ConstDescr), DataGridViewTextBoxColumn).MaxInputLength = 250

            .Columns(ConstDtype).HeaderText = "Type"
            .Columns(ConstDtype).SortMode = DataGridViewColumnSortMode.NotSortable
            Dim cmb As New DataGridViewComboBoxColumn
            'cmb.Items.Add("")
            cmb.Items.Add("Dr")
            cmb.Items.Add("Cr")
            cmb.HeaderText = "Type"
            cmb.DataPropertyName = "Type"
            cmb.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
            .Columns.RemoveAt(ConstDtype)
            .Columns.Insert(ConstDtype, cmb)
            .Columns(ConstDtype).Width = 40

            .Columns(ConstAmount).HeaderText = "Amount"
            .Columns(ConstAmount).Width = 100
            .Columns(ConstAmount).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstAmount).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstAmount).DefaultCellStyle.Format = "N" & NoOfDecimal


            .Columns(ConstFCName).HeaderText = "FC"
            .Columns(ConstFCName).Width = 50
            .Columns(ConstFCName).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstFCName).Visible = False

            .Columns(ConstFCRate).HeaderText = "FC Rate"
            .Columns(ConstFCRate).Width = 150
            .Columns(ConstFCRate).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstFCRate).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstFCRate).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstFCRate).Visible = False

            .Columns(ConstFCAmount).HeaderText = "FC Amount"
            .Columns(ConstFCAmount).Width = 150
            .Columns(ConstFCAmount).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstFCAmount).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstFCAmount).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstFCAmount).Visible = False


            .Columns(ConstJob).HeaderText = "Job"
            .Columns(ConstJob).Width = 100
            .Columns(ConstJob).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstJob).Visible = False

            .Columns(Constchq).HeaderText = "Cheq#"
            .Columns(Constchq).Width = 100
            .Columns(Constchq).SortMode = DataGridViewColumnSortMode.NotSortable

            Dim col As New CalendarColumn()
            .Columns.RemoveAt(ConstChqdate)
            col.DataPropertyName = "ChqDate"
            .Columns.Insert(ConstChqdate, col)
            .Columns(ConstChqdate).HeaderText = "Cheq Date"

            cmb = New DataGridViewComboBoxColumn
            cmb.Items.Add("")
            Dim dtbankDatatable As DataTable
            dtbankDatatable = _objcmnbLayer._fldDatatable("Select Bankcode from BankTb")
            For i = 0 To dtbankDatatable.Rows.Count - 1
                cmb.Items.Add(dtbankDatatable(i)(0))
            Next
            cmb.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
            cmb.HeaderText = "Bank"
            cmb.DataPropertyName = "BankCode"
            .Columns.RemoveAt(ConstBank)
            .Columns.Insert(ConstBank, cmb)
            .Columns(ConstBank).Width = 45

            .Columns(ConstPdcCustAc).HeaderText = "PDC CustId"
            .Columns(ConstPdcCustAc).Width = 150
            .Columns(ConstPdcCustAc).SortMode = DataGridViewColumnSortMode.NotSortable

           

            .Columns(ConstLPO).HeaderText = "LPO"
            .Columns(ConstLPO).Width = Me.Width * 5 / 100   '100
            .Columns(ConstLPO).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstLPO).Visible = False

           
            col = New CalendarColumn()
            .Columns.RemoveAt(ConstLpoDate)
            col.DataPropertyName = "DocDate"
            .Columns.Insert(ConstLpoDate, col)
            .Columns(ConstLpoDate).HeaderText = "LOP Date"
            .Columns(ConstLpoDate).Visible = False

            .Columns(ConstPdcCustAcno).HeaderText = "PDC ACCOUNT NO"
            .Columns(ConstPdcCustAcno).Width = Me.Width * 5 / 100   '100
            .Columns(ConstPdcCustAcno).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstPdcCustAcno).Visible = False

            .Columns(ConstFCDec).HeaderText = "Decimal"
            .Columns(ConstFCDec).Width = Me.Width * 5 / 100   '100
            .Columns(ConstFCDec).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstFCDec).Visible = False

            .Columns(ConstAccountNo).HeaderText = "AccountNO"
            .Columns(ConstAccountNo).Width = Me.Width * 5 / 100   '100
            .Columns(ConstAccountNo).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstAccountNo).Visible = False

            .Columns(ConstBrId).HeaderText = "ConstBrId"
            .Columns(ConstBrId).Width = Me.Width * 5 / 100   '100
            .Columns(ConstBrId).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstBrId).Visible = False

            .Columns(ConstGrpSetOn).HeaderText = "GrpSetOn"
            .Columns(ConstGrpSetOn).Visible = False

            .Columns(ConstUnq).HeaderText = "Unq"
            .Columns(ConstUnq).Visible = False
        End With

    End Sub
   

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        addrow()
    End Sub
    Private Sub addrow()

        Dim i As Integer
        With grdVoucher
            .Rows.Add(1)
            assaignTotal()
            i = .RowCount - 1
            If CDbl(lbldiff.Text) > 0 Then
                .Item(ConstDtype, i).Value = "Cr"
            Else
                .Item(ConstDtype, i).Value = "Dr"
            End If
            .Item(ConstFCRate, i).Value = 1
            .Item(ConstAmount, i).Value = Format(IIf(CDbl(lbldiff.Text) > 0, 1, -1) * CDbl(lbldiff.Text), "#,##" & numFormat)
            .Item(ConstFCAmount, i).Value = Format(IIf(CDbl(lbldiff.Text) > 0, 1, -1) * CDbl(lbldiff.Text), "#,##" & numFormat)
            .Item(ConstGrpSetOn, i).Value = ""
            If grdVoucher.Rows.Count >= 2 Then
                .Item(ConstDescr, i).Value = .Item(ConstDescr, i - 1).Value
                .Item(ConstReference, i).Value = .Item(ConstReference, i - 1).Value
            End If
            .CurrentCell = .Item(0, .Rows.Count - 1)
            activecontrolname = "grdVoucher"
            BeginEdit()
        End With
    End Sub
    Public Sub BeginEdit()
        chgbyprg = True
        With grdVoucher
            If .RowCount = 0 Then Exit Sub
            activecontrolname = "grdVoucher"
            .BeginEdit(True)
        End With
        chgbyprg = False
    End Sub
    Sub assaignTotal()
        Try
            Dim i As Integer
            Dim ttlDebit As Double
            Dim ttlCredit As Double
            Dim currentAmt As Double
            If SrchText = "" Then SrchText = 0
            With grdVoucher
                For i = 0 To grdVoucher.Rows.Count - 1
                    If i = .CurrentRow.Index Then
                        currentAmt = CDbl(SrchText)
                    Else
                        currentAmt = 0
                    End If
                    If IsDBNull(.Item(ConstAmount, i).Value) Then .Item(ConstAmount, i).Value = 0
                    If .Item(ConstDtype, i).Value = "Dr" Then
                        ttlDebit = ttlDebit + IIf(CDbl(.Item(ConstAmount, i).Value) = 0, currentAmt, CDbl(.Item(ConstAmount, i).Value))
                    Else
                        ttlCredit = ttlCredit + IIf(CDbl(.Item(ConstAmount, i).Value) = 0, currentAmt, CDbl(.Item(ConstAmount, i).Value))
                    End If

                Next
                SrchText = ""
                currentAmt = 0
                lblTlDebit.Text = Format(ttlDebit, numFormat)
                lblcredit.Text = Format(ttlCredit, numFormat)
                lbldiff.Text = Format(CDbl(lblTlDebit.Text) - CDbl(lblcredit.Text), numFormat)
            End With
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub
    Public Sub CheckNLoad(ByVal keyid As Long)

    End Sub

    Private Sub JournalVoucher_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        dtpdate.Focus()
    End Sub

    Private Sub JournalVoucher_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetGridHead()
        cmbVoucherTp.SelectedIndex = 0
        Timer1.Enabled = True
        If isModi Then

        End If
        If userType = 0 Or userType = 2 Then
            btnupdate.Tag = 1
            btnrem.Tag = 1
        Else
            btnupdate.Tag = IIf(getRight(58, CurrentUser), 1, 0)
            btndelete.Tag = IIf(getRight(60, CurrentUser), 1, 0)
        End If
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        If grdVoucher.Columns.Count = 0 Then Exit Sub
        Dim colwidth As Integer
        Dim i As Integer
        For i = ConstDtype To ConstGrpSetOn
            If grdVoucher.Columns(i).Visible = True Then
                colwidth = colwidth + grdVoucher.Columns(i).Width
            End If
        Next
        colwidth = colwidth + grdVoucher.Columns(ConstAlias).Width + grdVoucher.Columns(ConstName).Width
        grdVoucher.Columns(ConstDescr).Width = grdVoucher.Width - colwidth - 130

    End Sub

    Private Sub JournalVoucher_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Timer1.Enabled = True
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
                        If (msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up)) And plsrch.Visible = False Then GoTo ctn
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
        BeginEdit()
    End Sub

    Private Sub grdVoucher_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellEndEdit
        Dim col As Integer = e.ColumnIndex
        Dim fmt As String
        Dim ndcf As Integer
        If col = ConstAmount Then
            fmt = "#,##0" & IIf(NoOfDecimal = 0, "", "." & Strings.StrDup(NoOfDecimal, "0"))
            grdVoucher.Item(col, e.RowIndex).Value = Format(Convert.ToDecimal(grdVoucher.Item(col, e.RowIndex).Value), fmt)
        ElseIf e.ColumnIndex = ConstFCRate Or e.ColumnIndex = ConstFCAmount Then
            If IsDBNull(grdVoucher.Item(ConstFCDec, e.RowIndex).Value) Then grdVoucher.Item(ConstFCDec, e.RowIndex).Value = NoOfDecimal
            ndcf = Val(grdVoucher.Item(ConstFCDec, e.RowIndex).Value)
            fmt = "#,##0" & IIf(ndcf = 0, "", "." & Strings.StrDup(ndcf, "0"))
            grdVoucher.Item(col, e.RowIndex).Value = Format(Convert.ToDecimal(grdVoucher.Item(col, e.RowIndex).Value), fmt)
        End If
       
    End Sub

    'Private Sub grdVoucher_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdVoucher.CellFormatting
    '    If grdVoucher.Rows.Count = 0 Then Exit Sub
    '    If e.ColumnIndex = ConstFCRate Or e.ColumnIndex = ConstFCAmount Then
    '        If IsDBNull(grdVoucher.Item(ConstFCDec, e.RowIndex).Value) Then grdVoucher.Item(ConstFCDec, e.RowIndex).Value = 2
    '        e.Value = String.Format("{0:F" & Val(grdVoucher.Item(ConstFCDec, e.RowIndex).Value) & "}", Val(e.Value))
    '    ElseIf e.ColumnIndex = ConstAmount Then
    '        e.Value = String.Format("{0:F" & Val(NoOfDecimal) & "}", Val(e.Value))
    '    End If
    'End Sub

    Private Sub grdVoucher_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellValidated
        If chgbyprg Then Exit Sub
        Valid(e.RowIndex, e.ColumnIndex)
        SrchText = ""
    End Sub
    Private Sub Valid(ByVal RowIndex As Integer, ByVal ColIndex As Integer)
        Dim chkDatatable As DataTable
        chgbyprg = True
        With grdVoucher
            Select Case ColIndex
                Case ConstAlias, ConstName
                    If SrchText = "" And Not IsDBNull(grdVoucher.Item(ConstAlias, grdVoucher.CurrentCell.RowIndex).Value) Then
                        SrchText = grdVoucher.Item(ConstAlias, .CurrentCell.RowIndex).Value
                    End If
                    chkDatatable = EntriesValidation(SrchText, 1)
                    If IsDBNull(.Item(ConstAccountNo, .CurrentCell.RowIndex).Value) Then .Item(ConstAccountNo, .CurrentCell.RowIndex).Value = 0
                    If chkDatatable.Rows.Count > 0 And Val(.Item(ConstAccountNo, .CurrentCell.RowIndex).Value) > 0 Then
                        .Item(ConstAlias, .CurrentCell.RowIndex).Value = SrchText
                        .Item(ConstAlias, .CurrentCell.RowIndex).Value = chkDatatable(0)("Alias")
                        .Item(ConstName, .CurrentCell.RowIndex).Value = chkDatatable(0)("AccDescr")
                        .Item(ConstAccountNo, .CurrentCell.RowIndex).Value = chkDatatable(0)("accid")
                        .Item(ConstBrId, .CurrentCell.RowIndex).Value = chkDatatable(0)("BranchId")
                        .Item(ConstGrpSetOn, .CurrentCell.RowIndex).Value = GrpSetOn(chkDatatable(0)("AccountNo"))
                    Else
                        .Item(ConstAlias, .CurrentCell.RowIndex).Value = ""
                        .Item(ConstName, .CurrentCell.RowIndex).Value = ""
                        .Item(ConstAccountNo, .CurrentCell.RowIndex).Value = 0
                        .Item(ConstBrId, .CurrentCell.RowIndex).Value = ""
                        .Item(ConstGrpSetOn, .CurrentCell.RowIndex).Value = ""
                    End If

                    '-------
                    If UCase(Trim(.Item(ConstGrpSetOn, .CurrentCell.RowIndex).Value)) = "BANK" Or UCase(Trim(.Item(ConstGrpSetOn, .CurrentCell.RowIndex).Value)) = "P.D.C.(I)" Or UCase(Trim(.Item(ConstGrpSetOn, .CurrentCell.RowIndex).Value)) = "P.D.C.(R)" Then
                        .Rows(.CurrentCell.RowIndex).Cells(Constchq).ReadOnly = False
                        .Rows(.CurrentCell.RowIndex).Cells(ConstChqdate).ReadOnly = False
                        .Rows(.CurrentCell.RowIndex).Cells(ConstBank).ReadOnly = False
                        .Rows(.CurrentCell.RowIndex).Cells(ConstPdcCustAc).ReadOnly = False
                    Else
                        .Rows(.CurrentCell.RowIndex).Cells(Constchq).ReadOnly = True
                        .Rows(.CurrentCell.RowIndex).Cells(ConstChqdate).ReadOnly = True
                        .Rows(.CurrentCell.RowIndex).Cells(ConstBank).ReadOnly = True
                        .Rows(.CurrentCell.RowIndex).Cells(ConstPdcCustAc).ReadOnly = True
                    End If
                Case ConstFCName
                    If SrchText = "" And Not IsDBNull(.Item(ConstFCName, .CurrentCell.RowIndex).Value) Then
                        SrchText = grdVoucher.Item(ConstFCName, .CurrentCell.RowIndex).Value
                    End If
                    chkDatatable = EntriesValidation(SrchText, 2)
                    If chkDatatable.Rows.Count > 0 Then
                        .Item(ConstFCName, .CurrentCell.RowIndex).Value = SrchText
                    Else
                        .Item(ConstFCName, .CurrentCell.RowIndex).Value = ""
                        .Item(ConstFCRate, .CurrentCell.RowIndex).Value = 1
                        .Item(ConstFCDec, .CurrentCell.RowIndex).Value = 2
                        If Not IsDBNull(.Item(ConstFCAmount, .CurrentRow.Index).Value) And Not IsDBNull(.Item(ConstFCRate, .CurrentRow.Index).Value) Then
                            .Item(ConstAmount, .CurrentRow.Index).Value = CDbl(.Item(ConstFCAmount, .CurrentRow.Index).Value) * IIf(CDbl(.Item(ConstFCRate, .CurrentCell.RowIndex).Value) > 0, CDbl(.Item(ConstFCRate, .CurrentCell.RowIndex).Value), 1)
                        End If
                    End If
                Case ConstJob
                    If SrchText = "" And Not IsDBNull(.Item(ConstJob, grdVoucher.CurrentCell.RowIndex).Value) Then
                        SrchText = grdVoucher.Item(ConstJob, .CurrentCell.RowIndex).Value
                    End If
                    chkDatatable = EntriesValidation(SrchText, 5)
                    If chkDatatable.Rows.Count > 0 Then
                        .Item(ConstJob, .CurrentCell.RowIndex).Value = SrchText
                    Else
                        .Item(ConstJob, .CurrentCell.RowIndex).Value = ""
                    End If
                Case ConstPdcCustAc
                    If SrchText = "" And Not IsDBNull(.Item(ConstPdcCustAc, grdVoucher.CurrentCell.RowIndex).Value) Then
                        SrchText = grdVoucher.Item(ConstPdcCustAc, .CurrentCell.RowIndex).Value
                    End If
                    chkDatatable = EntriesValidation(SrchText, 8)
                    If chkDatatable.Rows.Count > 0 Then
                        .Item(ConstPdcCustAc, .CurrentCell.RowIndex).Value = SrchText
                        .Item(ConstPdcCustAcno, .CurrentCell.RowIndex).Value = chkDatatable(0)("accid")
                    Else
                        .Item(ConstPdcCustAc, .CurrentCell.RowIndex).Value = ""
                        .Item(ConstPdcCustAcno, .CurrentCell.RowIndex).Value = 0
                    End If
                    'Case ConstDescr
                    '    If Not IsDBNull(.Item(ConstDescr, .CurrentCell.RowIndex).Value) And Not .Item(ConstDescr, .CurrentCell.RowIndex).Value = Nothing Then
                    '        .Item(ConstDescr, .CurrentCell.RowIndex).Value = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(.Item(ConstDescr, .CurrentCell.RowIndex).Value)
                    '    End If

                    'Case ConstReference
                    '    If Not IsDBNull(.Item(ConstReference, .CurrentCell.RowIndex).Value) And Not .Item(ConstReference, .CurrentCell.RowIndex).Value = Nothing Then
                    '        .Item(ConstReference, .CurrentCell.RowIndex).Value = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(grdVoucher.Item(ConstReference, grdVoucher.CurrentCell.RowIndex).Value)
                    '    End If

            End Select
            Select Case ColIndex
                Case ConstAmount, ConstFCAmount, ConstFCName, ConstFCRate, ConstDtype
                    assaignTotal()
            End Select
        End With
        chgbyprg = False
    End Sub

    Private Sub grdVoucher_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdVoucher.EditingControlShowing
        Dim Col As Integer
        Col = grdVoucher.CurrentCell.ColumnIndex
        If Col = ConstAlias Or Col = ConstFCName Or Col = ConstName Or Col = ConstJob Or Col = ConstPdcCustAc Or Col = ConstAmount Or Col = ConstFCAmount Or Col = ConstFCRate Then
            Dim tb As TextBox = CType(e.Control, TextBox)
            RemoveHandler tb.TextChanged, AddressOf Textbox_TextChanged
            AddHandler tb.TextChanged, AddressOf Textbox_TextChanged
        End If
        If Col = ConstAmount Or Col = ConstFCAmount Or Col = ConstFCRate Then
            Dim tb As TextBox = CType(e.Control, TextBox)
            RemoveHandler tb.KeyPress, AddressOf Textbox_KeyPress
            AddHandler tb.KeyPress, AddressOf Textbox_KeyPress
        End If
    End Sub
    Private Sub Textbox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            Dim col As Integer
            col = grdVoucher.CurrentCell.ColumnIndex
            If col = ConstAmount Or col = ConstFCAmount Or col = ConstFCRate Then
                If Not IsNumeric(e.KeyChar) And e.KeyChar <> "." And Not e.KeyChar = Convert.ToChar(Keys.Back) Then e.Handled = True
            ElseIf col = ConstPdcCustAc Then
                e.Handled = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub Textbox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If chgbyprg Then Exit Sub 'sh
        Try
            Dim col As Integer
            col = grdVoucher.CurrentCell.ColumnIndex
            Dim MyCtrl As TextBox = sender
            If col = ConstAlias Or ConstDescr Then strGridSrchString = MyCtrl.Text
            If chgbyprg Then Exit Sub
            ChgId = True
            _srchOnce = False
            If col = ConstAlias Then
                _srchTxtId = 1
                _srchIndexId = 1
                chgbyprg = True
                strGridSrchString = MyCtrl.Text
                Showfmlist()
                chgbyprg = False
            ElseIf col = ConstName Then
                _srchTxtId = 2
                _srchIndexId = 0
                chgbyprg = True
                strGridSrchString = MyCtrl.Text
                Showfmlist()
                chgbyprg = False
            ElseIf col = ConstFCName Then
                _srchTxtId = 3
                _srchIndexId = 0
                chgbyprg = True
                strGridSrchString = MyCtrl.Text
                Showfmlist()
                chgbyprg = False
            ElseIf col = ConstJob Then
                _srchTxtId = 4
                _srchIndexId = 0
                chgbyprg = True
                strGridSrchString = MyCtrl.Text
                Showfmlist()
                chgbyprg = False
            ElseIf col = ConstAmount Then
                With grdVoucher
                    If .Rows.Count > 0 Then
                        If Not IsDBNull(.Item(ConstFCRate, .CurrentRow.Index).Value) And MyCtrl.Text <> "" Then
                            ' .Item(ConstAmount, .CurrentRow.Index).Value = MyCtrl.Text 'CDbl(MyCtrl.Text) / IIf(CDbl(.Item(ConstFCRate, .CurrentRow.Index).Value) > 0, CDbl(.Item(ConstFCRate, .CurrentRow.Index).Value), 1)
                            .Item(ConstFCAmount, .CurrentRow.Index).Value = CDbl(MyCtrl.Text) / IIf(CDbl(.Item(ConstFCRate, .CurrentRow.Index).Value) > 0, CDbl(.Item(ConstFCRate, .CurrentRow.Index).Value), 1)
                        Else
                            If MyCtrl.Text <> "" Then .Item(ConstFCAmount, .CurrentRow.Index).Value = CDbl(MyCtrl.Text)
                        End If
                    End If
                End With
                SrchText = MyCtrl.Text
            ElseIf col = ConstFCAmount Then
                With grdVoucher
                    If .Rows.Count > 0 Then
                        If Not IsDBNull(.Item(ConstFCRate, .CurrentRow.Index).Value) And MyCtrl.Text <> "" Then
                            .Item(ConstAmount, .CurrentRow.Index).Value = CDbl(MyCtrl.Text) * IIf(CDbl(.Item(ConstFCRate, .CurrentRow.Index).Value) > 0, CDbl(.Item(ConstFCRate, .CurrentRow.Index).Value), 1)
                        Else
                            If MyCtrl.Text <> "" Then .Item(ConstAmount, .CurrentRow.Index).Value = CDbl(MyCtrl.Text)
                        End If
                    End If
                End With
            ElseIf col = ConstFCRate Then
                With grdVoucher
                    If .Rows.Count > 0 Then
                        If Not IsDBNull(.Item(ConstFCAmount, .CurrentRow.Index).Value) And MyCtrl.Text <> "" And MyCtrl.Text <> "." Then
                            .Item(ConstAmount, .CurrentRow.Index).Value = CDbl(.Item(ConstFCAmount, .CurrentRow.Index).Value) * IIf(CDbl(MyCtrl.Text) > 0, CDbl(MyCtrl.Text), 1)
                        End If
                    End If
                End With
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub Showfmlist()
        If Not _srchOnce Then
            chgbyprg = True
            Dim PopupLoc As Point
            Dim x As Integer = Me.Width - plsrch.Width - 100
            Dim y As Integer = Me.Height - plsrch.Height - 100
            PopupLoc = New Point(x, y)
            If plsrch.Visible = False Then
                Select Case _srchTxtId
                    Case 1, 2 ' account name
                        SetPanel(grdSrch, 13, 3, 0, 250)
                    Case 3 'fc
                        SetPanel(grdSrch, 7, 2, 0, 200)
                    Case 4 'job
                        SetPanel(grdSrch, 8, 2, 1, 200)
                End Select
                plsrch.Location = PopupLoc
                plsrch.Visible = True
            End If
        End If
        Select Case _srchTxtId
            Case 1   'Customer code
                SearchPanel(grdSrch, strGridSrchString, 1)
                doSelect(2)
            Case 2   'Customer name
                SearchPanel(grdSrch, strGridSrchString, 0)
                doSelect(1)
            Case 3   'FC
                SearchPanel(grdSrch, strGridSrchString, 0)
                doSelect(2)
            Case 4   'Job
                SearchPanel(grdSrch, strGridSrchString, 0)
                doSelect(2)
        End Select
        _srchOnce = True
        chgbyprg = False
    End Sub

    Private Sub grdVoucher_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.Enter
        Try
            If grdVoucher.Rows.Count = 0 Then Exit Sub
            BeginEdit()
            If plsrch.Visible Then plsrch.Visible = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub grdVoucher_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.GotFocus
        If chgbyprg Then Exit Sub
        activecontrolname = "grdVoucher"
    End Sub


    Private Sub grdVoucher_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdVoucher.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If grdVoucher.RowCount = 0 Then Exit Sub
                With grdVoucher
                    If FindNextCell(grdVoucher, grdVoucher.CurrentCell.RowIndex, grdVoucher.CurrentCell.ColumnIndex + 1) Then
                        addrow()
                    End If
                    If grdVoucher.CurrentRow.Index = 0 Then grdVoucher.Tag = grdVoucher.Item(ConstDtype, grdVoucher.CurrentCell.RowIndex).Value
                    plsrch.Visible = False
                    BeginEdit()
                End With
            ElseIf e.KeyCode = Keys.Up Or e.KeyCode = Keys.PageUp Then
                If grdVoucher.RowCount = 0 Then Exit Sub
                If plsrch.Visible Then
                    doSelect(0)
                    Exit Sub
                End If
            ElseIf e.KeyCode = Keys.Down Or e.KeyCode = Keys.PageDown Then
                If grdVoucher.RowCount = 0 Then Exit Sub
                If plsrch.Visible Then
                    doSelect(1)
                    Exit Sub
                End If
            ElseIf e.KeyCode = Keys.F2 Then
                If grdVoucher.RowCount = 0 Then Exit Sub
                Select Case grdVoucher.CurrentCell.ColumnIndex
                    Case ConstName, ConstAlias
                        ldSelect(2)
                    Case ConstPdcCustAc
                        ldSelect(1)
                End Select
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ldSelect(ByVal BVal As Single)
        fSelect = New Selectfrm
        SetForm(fSelect, BVal)
        fSelect.ShowDialog()
    End Sub
    Private Sub doSelect(ByVal Mup As Integer)
        Try
            chgbyprg = True
            Dim ItmFlds() As String
            If plsrch.Visible = False Then Exit Sub
            If Mup = 0 Then 'UP
                ItmFlds = MoveUpPl(grdSrch, _srchIndexId, strGridSrchString)
            ElseIf Mup = 1 Then 'Down
                ItmFlds = MoveDownPl(grdSrch, _srchIndexId, strGridSrchString)
            Else
                ItmFlds = SelectItmPanel(grdSrch)
            End If
            If strGridSrchString = "" And Mup = 2 Then SrchText = "" : GoTo Nxt
            Select Case _srchTxtId
                Case 1, 2 'alias,account name

                    grdVoucher.Item(ConstAlias, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(1) IsNot Nothing, ItmFlds(1), "")
                    SrchText = IIf(ItmFlds(1) IsNot Nothing, ItmFlds(1), strGridSrchString)
                    Dim s As String = IIf(ItmFlds(1) IsNot Nothing, ItmFlds(1), "")
                    grdVoucher.Item(ConstName, grdVoucher.CurrentCell.RowIndex).Value = ItmFlds(0)
                    grdVoucher.Item(ConstAccountNo, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(3) IsNot Nothing, ItmFlds(3), 0)
                Case 3 'FC
                    grdVoucher.Item(ConstFCName, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(0) IsNot Nothing, ItmFlds(0), "")
                    SrchText = IIf(ItmFlds(0) IsNot Nothing, ItmFlds(0), strGridSrchString)
                    grdVoucher.Item(ConstFCRate, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(1) IsNot Nothing, ItmFlds(1), 0)
                    grdVoucher.Item(ConstFCDec, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(4) IsNot Nothing, ItmFlds(4), 2)
                    With grdVoucher
                        If Not IsDBNull(.Item(ConstFCAmount, .CurrentRow.Index).Value) And Not IsDBNull(.Item(ConstFCRate, .CurrentRow.Index).Value) Then
                            .Item(ConstAmount, .CurrentRow.Index).Value = CDbl(.Item(ConstFCAmount, .CurrentRow.Index).Value) * IIf(CDbl(.Item(ConstFCRate, .CurrentCell.RowIndex).Value) > 0, CDbl(.Item(ConstFCRate, .CurrentCell.RowIndex).Value), 1)
                        End If
                    End With
                Case 4 'Job
                    grdVoucher.Item(ConstJob, grdVoucher.CurrentCell.RowIndex).Value = ItmFlds(0)
                    SrchText = IIf(ItmFlds(0) IsNot Nothing, ItmFlds(0), strGridSrchString)
                Case 5 'Pdc cust name
                    grdVoucher.Item(ConstPdcCustAc, grdVoucher.CurrentCell.RowIndex).Value = ItmFlds(1)
                    SrchText = IIf(ItmFlds(1) IsNot Nothing, ItmFlds(1), strGridSrchString)
                    grdVoucher.Item(ConstPdcCustAcno, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(3) IsNot Nothing, ItmFlds(3), 0)
            End Select
nxt:
            chgbyprg = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub grdVoucher_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.Leave
        activecontrolname = ""
    End Sub

    Private Sub grdVoucher_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.SelectionChanged
        If _NewRw Then _NewRw = False : Exit Sub
        Try
            If grdVoucher.Rows.Count = 0 Then Exit Sub
            BeginEdit()
            If plsrch.Visible Then plsrch.Visible = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub fSelect_doSelect(ByVal KeyId As String, ByVal strFld1 As String, ByVal strFld2 As String) Handles fSelect.doSelect
        chgbyprg = True
        With grdVoucher
            Select Case .CurrentCell.ColumnIndex
                Case ConstAlias, ConstName
                    .Item(ConstAlias, .CurrentRow.Index).Value = strFld2
                    .Item(ConstName, .CurrentRow.Index).Value = strFld1
                    .Item(ConstAccountNo, .CurrentRow.Index).Value = KeyId
                    .Item(ConstGrpSetOn, .CurrentRow.Index).Value = GrpSetOn(KeyId)
                Case ConstPdcCustAc
                    grdVoucher.Item(ConstPdcCustAc, grdVoucher.CurrentCell.RowIndex).Value = strFld1
                    grdVoucher.Item(ConstPdcCustAcno, grdVoucher.CurrentCell.RowIndex).Value = KeyId
            End Select
            '-----
            If UCase(Trim(.Item(ConstGrpSetOn, .CurrentCell.RowIndex).Value)) = "BANK" Or UCase(Trim(.Item(ConstGrpSetOn, .CurrentCell.RowIndex).Value)) = "P.D.C.(I)" Or UCase(Trim(.Item(ConstGrpSetOn, .CurrentCell.RowIndex).Value)) = "P.D.C.(R)" Then
                .Rows(.CurrentCell.RowIndex).Cells(Constchq).ReadOnly = False
                .Rows(.CurrentCell.RowIndex).Cells(ConstChqdate).ReadOnly = False
                .Rows(.CurrentCell.RowIndex).Cells(ConstBank).ReadOnly = False
            Else
                .Rows(.CurrentCell.RowIndex).Cells(Constchq).ReadOnly = True
                .Rows(.CurrentCell.RowIndex).Cells(ConstChqdate).ReadOnly = True
                .Rows(.CurrentCell.RowIndex).Cells(ConstBank).ReadOnly = True
            End If
            FindNextCell(grdVoucher, .CurrentCell.RowIndex, .CurrentCell.ColumnIndex + 1)
            BeginEdit()
        End With
        chgbyprg = False
    End Sub

    Private Sub fSelect_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fSelect.FormClosed
        fSelect = Nothing
    End Sub

    Private Sub cmbVoucherTp_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVoucherTp.SelectedIndexChanged
        If TabControl1.SelectedIndex = 1 Then
            ldGrid()
        Else
            dtAcc = Nothing
            dtInvNos = Nothing
            PreFixTb = Nothing
            If Mid(cmbVoucherTp.Text, 1, 2) = "PI" Or Mid(cmbVoucherTp.Text, 1, 2) = "SI" Then
                crtSubVrs(cmbprefix, IIf(Mid(cmbVoucherTp.Text, 1, 2) = "PI", 7, 5), True)
                cmbprefix.Visible = True
            Else
                cmbprefix.Visible = False
                cmbprefix.Tag = ""
            End If
            nextVoucher()
        End If

    End Sub
    Private Sub nextVoucher()
        Try
            Dim vrInvoice As String = ""
            Dim vrPrefix As String = ""
            Dim a, b As String
            a = ""
            b = ""
            If Val(cmbprefix.Tag) = 0 And Mid(cmbVoucherTp.Text, 1, 2) = "PI" Then
                cmbprefix.Tag = getvrsId(cmbprefix.Text, 7)
            End If
            If Val(cmbprefix.Tag) = 0 And Mid(cmbVoucherTp.Text, 1, 2) = "SI" Then
                cmbprefix.Tag = getvrsId(cmbprefix.Text, 5)
            End If
            getVrsDet(Val(cmbprefix.Tag), Mid(cmbVoucherTp.Text, 1, 2), vrPrefix, vrInvoice, a, b)

            chgbyprg = True
            makeClear()
            If Val(a) > 0 Then
                addAccount(Val(a), "Dr")
            End If
            If Val(b) > 0 Then
                addAccount(Val(b), "Cr")
            End If
            numVchrNo.Text = Val(vrInvoice)
            txtprefix.Text = Trim(vrPrefix)

            chgbyprg = False
            dtpdate.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub addAccount(ByVal accountno As Long, ByVal Dtype As String)
        Dim i As Integer
        With grdVoucher
            .Rows.Add(1)
            Dim dtAcc As DataTable
            dtAcc = _objcmnbLayer._fldDatatable("SELECT Alias,AccDescr,GrpSetOn,GrpSetOn,accid FROM AccMast " & _
                                                "LEFT JOIN S1AccHd ON AccMast.S1AccId=S1AccHd.S1AccId WHERE accid=" & Val(accountno))
            If dtAcc.Rows.Count > 0 Then
                i = .RowCount - 1
                .Item(ConstDtype, i).Value = Dtype
                .Item(ConstFCRate, i).Value = 1
                .Item(ConstAlias, i).Value = dtAcc(0)("Alias")
                .Item(ConstName, i).Value = dtAcc(0)("AccDescr")
                .Item(ConstAccountNo, i).Value = dtAcc(0)("accid")
                .Item(ConstAmount, i).Value = Format(IIf(CDbl(lbldiff.Text) > 0, 1, -1) * CDbl(lbldiff.Text), "#,##" & numFormat)
                .Item(ConstFCAmount, i).Value = Format(IIf(CDbl(lbldiff.Text) > 0, 1, -1) * CDbl(lbldiff.Text), "#,##" & numFormat)
                .Item(ConstGrpSetOn, i).Value = ""
                If grdVoucher.Rows.Count >= 2 Then
                    .Item(ConstDescr, i).Value = .Item(ConstDescr, i - 1).Value
                    .Item(ConstReference, i).Value = .Item(ConstReference, i - 1).Value
                End If
                .CurrentCell = .Item(ConstAmount, 0)
                activecontrolname = "grdVoucher"
                BeginEdit()
            End If
        End With
    End Sub
    Private Sub makeClear()
        grdVoucher.Rows.Clear()
        lblcredit.Text = Format(0, numFormat)
        lblTlDebit.Text = Format(0, numFormat)
        lbldiff.Text = Format(0, numFormat)
        txtprintprefix.Text = ""
        numprintvrno.Text = ""
        txtDescription.Text = ""
        btndelete.Enabled = False
        loadedTrId = 0
        btnnew.Text = "Clear"
        chgbyprg = False
    End Sub

    Private Sub dtpdate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtpdate.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub dtpdate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpdate.ValueChanged

    End Sub

    Private Sub txtDescription_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDescription.KeyDown
        If e.KeyCode = Keys.Return Then
            addrow()
        End If
    End Sub

    Private Sub btnrem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrem.Click
        If Not grdVoucher.RowCount > 0 Then Exit Sub
        If grdVoucher.RowCount > 0 Then
            If MsgBox("You Are Going to Remove the Row." & vbCrLf & "Are You Sure ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then
                Exit Sub
            End If
            With grdVoucher
                .Rows.RemoveAt(.CurrentRow.Index)
                .ClearSelection()
            End With
            assaignTotal()
        End If

    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        nextVoucher()
        If btnnew.Text = "Undo" Then
            TabControl1.SelectedIndex = 1
        End If
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        If btnupdate.Enabled = False Then Exit Sub
        If Not userType Then
            btnupdate.Tag = 1
        Else
            If btnnew.Text = "Undo" Then
                btnupdate.Tag = IIf(getRight(59, CurrentUser), 1, 0)
            Else
                btnupdate.Tag = IIf(getRight(58, CurrentUser), 1, 0)
            End If
        End If
        If Val(btnupdate.Tag) = 0 Then
            MsgBox("This user do not have permission to Update", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        assaignTotal()
        If plsrch.Visible Then plsrch.Visible = False
        If Not fSelect Is Nothing Then fSelect.Close() : fSelect = Nothing
        verify()
    End Sub
    Private Sub verify()
        If btnnew.Text = "Undo" Then
            If ChgId = False Then
                MsgBox("Changes not found !!", vbExclamation)
                numVchrNo.Focus()
                Exit Sub
            Else
                If loadedTrId = 0 Then
                    MsgBox("Voucher not yet loaded !!", vbExclamation)
                    numVchrNo.Focus()
                    Exit Sub
                End If
            End If
        End If
        If Val(numVchrNo.Text) = 0 Then
            MsgBox("Invalid Voucher Number", MsgBoxStyle.Exclamation)
            numVchrNo.Focus()
            Exit Sub
        End If
        If chkDate((dtpdate.Value)) Then
            If CDate(dtpdate.Value) < DateFrom Or CDate(dtpdate.Value) > DateTo Then
                MsgBox("Invalid Voucher Date !!", MsgBoxStyle.Exclamation)
                dtpdate.Focus()
                Exit Sub
            End If
        End If
        If DateValue(dtpdate.Value) <= ProtectUntil Then
            MsgBox("Voucher Date comes within Protected Date Range !", MsgBoxStyle.Exclamation)
            dtpdate.Focus()
            Exit Sub
        End If
        If CDbl(lbldiff.Text) <> 0 Or (CDbl(lblcredit.Text) = 0 And CDbl(lblTlDebit.Text) = 0) Then
            MsgBox("Amount is not balanced or empty entry.", MsgBoxStyle.Exclamation)
            With grdVoucher
                .Select()
                .CurrentCell = .Item(ConstAmount, .RowCount - 1)
            End With
            Exit Sub
        End If
        'If enableBranch And UsrBr = "" Then
        '    MsgBox("Transaction cannot be saved without Branch! Please login with Branch", MsgBoxStyle.Exclamation)
        '    Exit Sub
        'End If
        If Not isGridValid() Then Exit Sub
        Dim i As Integer
        For i = 0 To grdVoucher.RowCount - 1
            If UCase(grdVoucher.Item(ConstDtype, i).Value) = "CR" Then
                If Not checkReconciliation(grdVoucher, ConstAccountNo, ConstAmount, ConstName, i) Then Exit Sub
            End If
        Next
        If MsgBox("Verification is succeeded. Do you like to file it ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        'saveTransaction()
        loadWaite(2)
    End Sub
    Private Sub saveTransaction()
        Try
chkagain:
            If btnnew.Text <> "Undo" Then
                If Not CheckNoExists(txtprefix.Text, Val(numVchrNo.Text), Mid(cmbVoucherTp.Text, 1, 2), "Accounts") Then
                    If MsgBox("Voucher Number alreary exist! Do you want to fill next number?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        numVchrNo.Text = Val(numVchrNo.Text) + 1
                        GoTo chkagain
                    Else
                        numVchrNo.Focus()
                        Exit Sub
                    End If
                End If
            End If
            If dtAccTb.Rows.Count > 0 Then dtAccTb.Rows.Clear()
            Dim LinkNo As Long
            setAcctrCmnValue()
            'LinkNo = Val(_objTr.SaveAccTrCmn())
            '_objcmnbLayer._saveDatawithOutParm("Update AccTrDet set setremove=1 where LinkNo=" & LinkNo)
            Dim i As Integer
            With grdVoucher
                For i = 0 To .RowCount - 1
                    If Val(grdVoucher.Item(ConstAmount, i).Value & "") = 0 Or Val(grdVoucher.Item(ConstAccountNo, i).Value & "") = 0 Then GoTo nxt
                    setAcctrDetValue(LinkNo, i)
                    '_objTr.saveAccTrans()
nxt:
                Next
            End With
            '_objcmnbLayer._saveDatawithOutParm("Delete from AccTrDet  where setremove=1 and LinkNo=" & LinkNo)
            _objTr.SaveAccTrWithDt(dtAccTb)
            numprintvrno.Tag = numVchrNo.Text
            txtprintprefix.Tag = txtprefix.Text
            If btnnew.Text <> "Undo" Then
                SetNextVrNo(numVchrNo, 0, Mid(cmbVoucherTp.Text, 1, 2), "JvType = '" & Mid(cmbVoucherTp.Text, 1, 2) & "' AND JvNum = ", True, True, True)
            Else
                nextVoucher()
            End If
            makeClear()
            numprintvrno.Text = numprintvrno.Tag
            txtprintprefix.Text = txtprintprefix.Tag
            If Not fwait Is Nothing Then fwait.Close() : fwait = Nothing
            MsgBox(Mid(cmbVoucherTp.Text, 5) & " # " & numprintvrno.Text & " Saved Successfully", MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox("Proc: saveTransaction" & vbCrLf & ex.Message, , "Postinng Falid")
        End Try
    End Sub
    Private Sub setAcctrCmnValue()
        _objTr.JVType = Mid(cmbVoucherTp.Text, 1, 2)
        _objTr.JVDate = DateValue(dtpdate.Value)
        _objTr.PreFix = txtprefix.Text
        _objTr.JVNum = Val(numVchrNo.Text)
        If Mid(cmbVoucherTp.Text, 1, 2) = "PV" Then
            _objTr.JVTypeNo = getVouchernumber("PVO")
        ElseIf Mid(cmbVoucherTp.Text, 1, 2) = "RV" Then
            _objTr.JVTypeNo = getVouchernumber("RVO")
        Else
            _objTr.JVTypeNo = getVouchernumber(Mid(cmbVoucherTp.Text, 1, 2))
        End If
        _objTr.UserId = CurrentUser
        _objTr.MchName = MACHINENAME
        _objTr.CrtDtTm = DateValue(Date.Now)
        _objTr.TypeNo = 0 ' id number from prefixtb
        _objTr.VrDescr = Trim(txtDescription.Text)
        _objTr.IsModi = IIf(loadedTrId > 0, 2, 0)
        _objTr.LinkNo = loadedTrId
        _objTr.isLinkNo = True
    End Sub
    Private Sub setAcctrDetValue(ByVal lnkNo As Long, ByVal _row As Integer)
        Dim dtrow As DataRow
        dtrow = dtAccTb.NewRow
        dtrow("LinkNo") = lnkNo
        dtrow("AccountNo") = Trim(grdVoucher.Item(ConstAccountNo, _row).Value & "")
        dtrow("Reference") = Trim(grdVoucher.Item(ConstReference, _row).Value & "")
        dtrow("EntryRef") = Trim(grdVoucher.Item(ConstDescr, _row).Value & "")
        dtrow("DealAmt") = CDbl(grdVoucher.Item(ConstAmount, _row).Value) * IIf(grdVoucher.Item(ConstDtype, _row).Value = "Dr", 1, -1)
        dtrow("FCAmt") = CDbl(grdVoucher.Item(ConstFCAmount, _row).Value) * IIf(grdVoucher.Item(ConstDtype, _row).Value = "Dr", 1, -1)
        dtrow("CurrencyCode") = Trim(grdVoucher.Item(ConstFCName, _row).Value & "")
        dtrow("CurrRate") = CDbl(grdVoucher.Item(ConstFCRate, _row).Value)
        dtrow("TrInf") = 0
        dtrow("PDCAcc") = Val(grdVoucher.Item(ConstPdcCustAcno, _row).Value & "")
        If chkDate(grdVoucher.Item(ConstChqdate, _row).Value) Then
            dtrow("ChqDate") = DateValue(grdVoucher.Item(ConstChqdate, _row).Value)
        End If
        dtrow("ChqNo") = grdVoucher.Item(Constchq, _row).Value
        dtrow("BankCode") = grdVoucher.Item(ConstBank, _row).Value
        dtrow("UnqNo") = Val(grdVoucher.Item(ConstUnq, _row).Value)
        dtAccTb.Rows.Add(dtrow)
        Dim j As Integer
        Dim dtype As String
        For j = 0 To dtAccTb.Columns.Count - 1
            dtype = dtAccTb.Columns(j).DataType.Name
            If Trim(dtAccTb(dtAccTb.Rows.Count - 1)(j) & "") = "" Then
                Select Case dtype
                    Case "String"
                        dtAccTb(dtAccTb.Rows.Count - 1)(j) = ""
                    Case "Int64", "Int32", "Double", "Decimal", "Boolean"
                        dtAccTb(dtAccTb.Rows.Count - 1)(j) = 0
                End Select
            End If
nxt:
        Next
    End Sub
    Private Function isGridValid() As Boolean
        Dim i As Integer
        Dim grpset As String
        Dim SqlQuery As String
        Dim _vAcctrdatatable As DataTable
        With grdVoucher
            If .Rows.Count > 0 Then
                For i = 0 To .Rows.Count - 1
                    grpset = .Item(ConstGrpSetOn, i).Value
                    If grpset = "P.D.C.(I)" Then
                        If .Item(ConstDtype, i).Value <> "Cr" Then
                            MsgBox("P.D.C Issued can not be Debited !", MsgBoxStyle.Exclamation)
                            .CurrentCell = .Item(ConstDtype, i).Value
                            .BeginEdit(True)
                            Return False
                        End If
                        If .Item(Constchq, i).Value = "" Then
                            MsgBox("Cheque Number Missing !", MsgBoxStyle.Exclamation)
                            .CurrentCell = .Item(Constchq, i).Value
                            .BeginEdit(True)
                            Return False
                        End If
                        If .Item(ConstBank, i).Value = "" Then
                            MsgBox("Bank Code Missing !", MsgBoxStyle.Exclamation)
                            .CurrentCell = .Item(ConstBank, i).Value
                            .BeginEdit(True)
                            Return False
                        End If
                        SqlQuery = tocheckDuplicatePDCEntryStr(Val(grdVoucher.Item(ConstPdcCustAcno, i).Value & ""), MkDbSrchStr(Trim(grdVoucher.Item(Constchq, i).Value & "")), MkDbSrchStr(Trim(grdVoucher.Item(ConstBank, i).Value & "")), "D")
                        _vAcctrdatatable = _objcmnbLayer._fldDatatable(SqlQuery)
                        If _vAcctrdatatable.Rows.Count > 0 Then
                            MsgBox("Duplicate PDC entry details found !", MsgBoxStyle.Exclamation)
                            grdVoucher.Select()
                            grdVoucher.CurrentCell = grdVoucher.Item(Constchq, i)
                            Return False
                        End If
                    End If
                    If grpset = "P.D.C.(R)" Then
                        If .Item(ConstDtype, i).Value <> "Dr" Then
                            MsgBox("P.D.C Recieved can not be Credited !", MsgBoxStyle.Exclamation)
                            .CurrentCell = .Item(ConstDtype, i).Value
                            .BeginEdit(True)
                            Return False
                        End If
                        If .Item(Constchq, i).Value = "" Then
                            MsgBox("Cheque Number Missing !", MsgBoxStyle.Exclamation)
                            .CurrentCell = .Item(Constchq, i).Value
                            .BeginEdit(True)
                            Return False
                        End If
                        If .Item(ConstBank, i).Value = "" Then
                            MsgBox("Bank Code Missing !", MsgBoxStyle.Exclamation)
                            .CurrentCell = .Item(ConstBank, i).Value
                            .BeginEdit(True)
                            Return False
                        End If
                        SqlQuery = tocheckDuplicatePDCEntryStr(Val(grdVoucher.Item(ConstPdcCustAcno, i).Value & ""), MkDbSrchStr(Trim(grdVoucher.Item(Constchq, i).Value & "")), MkDbSrchStr(Trim(grdVoucher.Item(ConstBank, i).Value & "")), "C")
                        _vAcctrdatatable = _objcmnbLayer._fldDatatable(SqlQuery)
                        If _vAcctrdatatable.Rows.Count > 0 Then
                            MsgBox("Duplicate PDC entry details found !", MsgBoxStyle.Exclamation)
                            grdVoucher.Select()
                            grdVoucher.CurrentCell = grdVoucher.Item(Constchq, i)
                            Return False
                        End If
                    End If
                    If grpset = "P.D.C.(R)" Or grpset = "P.D.C.(I)" Then
                        Dim r As Integer
                        For r = i + 1 To grdVoucher.RowCount - 1
                            If grdVoucher.Item(ConstPdcCustAcno, r).Value = grdVoucher.Item(ConstPdcCustAcno, i).Value And UCase(Trim(grdVoucher.Item(Constchq, r).Value)) = UCase(Trim(grdVoucher.Item(Constchq, i).Value)) And UCase(grdVoucher.Item(ConstBank, r).Value) = UCase(grdVoucher.Item(ConstBank, i).Value) Then
                                MsgBox("Duplicate PDC entry details found in the same voucher !", MsgBoxStyle.Exclamation)
                                grdVoucher.Select()
                                grdVoucher.CurrentCell = grdVoucher.Item(Constchq, r)
                                Return False
                            End If
                        Next
                    End If

                Next
            End If
        End With
        Return True
    End Function

    Private Sub picCloseProd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picCloseProd.Click
        plsrch.Visible = False
    End Sub

    Private Sub grdSrch_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSrch.CellDoubleClick
        activecontrolname = "grdVoucher"
        strGridSrchString = grdSrch.Item(0, grdSrch.CurrentRow.Index).Value
        doSelect(2)
        chgbyprg = True
        grdVoucher.CurrentCell = grdVoucher.Item(1, grdVoucher.CurrentRow.Index)
        Valid(grdVoucher.CurrentCell.RowIndex, grdVoucher.CurrentCell.ColumnIndex)
        chgbyprg = False
        grdVoucher.BeginEdit(True)
        plsrch.Visible = False
    End Sub
    Private Sub ldGrid()
        With _objTr
            .ptype = 6
            .DateFrom = DateValue(dtpstart.Value)
            .DateTo = DateValue(dtpto.Value)
            If Mid(cmbVoucherTp.Text, 1, 2) = "PV" Then
                .JVType = "PVO"
            ElseIf Mid(cmbVoucherTp.Text, 1, 2) = "RV" Then
                .JVType = "RVO"
            Else
                .JVType = Mid(cmbVoucherTp.Text, 1, 2)
            End If

            dtTable = .returnPaymentDetails.Tables(0)
        End With
        dvData.DataSource = dtTable
        SetmodiGrid()
        setComboGrid()
    End Sub
    Private Sub setComboGrid()
        Dim i As Integer = 0
        cmbOrder.Items.Clear()
        For i = 0 To dvData.ColumnCount - 1 'IIf(dvData.ColumnCount - 1 >= cmbShowIndex, cmbShowIndex, dvData.ColumnCount - 1)
            cmbOrder.Items.Add(dvData.Columns(i).HeaderText)
        Next
        cmbOrder.SelectedIndex = 0
    End Sub

    Sub SetmodiGrid()
        With dvData
            SetGridProperty(dvData)


            .Columns(ConstInvNo).HeaderText = Mid(cmbVoucherTp.Text, 1, 2) & " No"
            .Columns(ConstInvNo).Width = 75
            '.Columns(ConstInvNo).SortMode = DataGridViewColumnSortMode.Automatic

            .Columns(ConstTrdate).HeaderText = "Tr Date"
            .Columns(ConstTrdate).Width = 90
            .Columns(ConstTrdate).SortMode = DataGridViewColumnSortMode.Automatic
            .Columns(ConstTrdate).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

            .Columns(ConstInvAmount).HeaderText = "Amount"
            .Columns(ConstInvAmount).Width = 120
            .Columns(ConstInvAmount).SortMode = DataGridViewColumnSortMode.Automatic
            .Columns(ConstInvAmount).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstInvAmount).DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns(ConstCustId).HeaderText = "Alias"
            .Columns(ConstCustId).Width = 75
            .Columns(ConstCustId).SortMode = DataGridViewColumnSortMode.Automatic
            .Columns(ConstCustId).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(ConstCustId).Visible = False

            .Columns(ConstCustname).HeaderText = "Account Name"
            .Columns(ConstCustname).Width = 300
            .Columns(ConstCustname).SortMode = DataGridViewColumnSortMode.Automatic


            .Columns(ConstTrRef).HeaderText = "Description"
            .Columns(ConstTrRef).Width = 300
            .Columns(ConstTrRef).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstTrRef).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

            .Columns(Consttype).HeaderText = "Type"
            .Columns(Consttype).Width = 7.5 '100
            .Columns(Consttype).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(Consttype).Visible = False

            .Columns(ConstLinkNo).HeaderText = "Link No"
            .Columns(ConstLinkNo).Width = 250 '100
            .Columns(ConstLinkNo).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstLinkNo).Visible = False

            .Columns("ChqDate").HeaderText = "Cheque Date"
            .Columns("ChqDate").Width = 100 '100
            '.Columns("ChqDate").SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns("ChqDate").Visible = False

            .Columns("ChqNo").HeaderText = "Cheque#"
            .Columns("ChqNo").Width = 75 '100
            '.Columns("ChqNo").SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns("ChqNo").Visible = False


            .Columns("DateFrom").Visible = False
            .Columns("DateTo").Visible = False
            .Columns("Lnk").Visible = False

            Dim colwidth As Integer
            Dim i As Integer
            For i = Consttype To .Columns.Count - 1
                If .Columns(i).Visible = True Then
                    colwidth = colwidth + .Columns(i).Width
                End If
            Next
            colwidth = colwidth + 485
            .Columns(ConstTrRef).Width = .Width - colwidth - 130

        End With
    End Sub

    Private Sub TabControl1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.Click
        If btnnew.Text = "Undo" And TabControl1.SelectedIndex = 1 Then
            MsgBox("Do Undo/Update before moving to Enquiry", MsgBoxStyle.Exclamation)
            TabControl1.SelectedIndex = 0
            Exit Sub
        End If
        If TabControl1.SelectedIndex = 1 And grdVoucher.RowCount = 0 Then
            loadWaite(1)
        End If
        If TabControl1.SelectedIndex = 1 Then
            btnnew.Enabled = False
            btndelete.Enabled = False
            btnupdate.Enabled = False

        Else
            If btnnew.Text <> "Undo" Then
                cmbVoucherTp_SelectedIndexChanged(cmbVoucherTp, New EventArgs)
                btnnew.Enabled = True
                btndelete.Enabled = True
                btnupdate.Enabled = True

            End If
        End If
    End Sub

    Private Sub btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnload.Click
        ldGrid()
    End Sub

    Private Sub txtSeq_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeq.TextChanged
        dvData.DataSource = SearchGrid(dtTable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
        RptdtTable = SearchGrid(dtTable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
    End Sub

    Private Sub btnedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnedit.Click
        loadWaite(3)
    End Sub
    Private Sub editRecord()
        If dvData.RowCount = 0 Then Exit Sub
        makeClear()
        numVchrNo.Text = ""
        loadedTrId = dvData.Item(ConstLinkNo, dvData.CurrentRow.Index).Value
        ldRec()
        btnnew.Text = "Undo"
        btnnew.Enabled = True
        isModi = True
        dtpdate.Focus()
        assaignTotal()
        If Val(btnupdate.Tag) = 0 Then
            MsgBox("This user do not have permission to modify", MsgBoxStyle.Exclamation)
        End If
        TabControl1.SelectedIndex = 0
    End Sub
    Private Sub ldRec()
        Dim dtAccTrCmn As DataTable
        Dim dtAccTr As DataTable
        Dim AccTrCmn As String
        Dim i As Integer
        Dim r As Integer
        chgbyprg = True
        AccTrCmn = "SELECT * FROM AccTrCmn WHERE LinkNo=" & loadedTrId
        grdVoucher.Rows.Clear()
        dtAccTrCmn = _objcmnbLayer._fldDatatable(AccTrCmn)
        If dtAccTrCmn.Rows.Count > 0 Then
            loadedTrId = dtAccTrCmn(0)("LinkNo")
            dtpdate.Value = DateValue(dtAccTrCmn(0)("JVDate"))
            chgbyprg = True
            numVchrNo.Text = dtAccTrCmn(0)("JVnum")
            numprintvrno.Text = dtAccTrCmn(0)("JVnum")
            txtprefix.Text = dtAccTrCmn(0)("Prefix")
            dtAccTr = _objcmnbLayer._fldDatatable(getAccTrDetbyloadedTrId(loadedTrId))
            For i = 0 To dtAccTr.Rows.Count - 1
                With grdVoucher
                    .Rows.Add()
                    r = .Rows.Count - 1
                    .Item(ConstAlias, r).Value = dtAccTr(i)("Alias")
                    .Item(ConstName, r).Value = dtAccTr(i)("AccDescr")
                    .Item(ConstReference, r).Value = dtAccTr(i)("Reference")
                    .Item(ConstDescr, r).Value = dtAccTr(i)("EntryRef")
                    If CDbl(dtAccTr(i)("DealAmt")) > 0 Then
                        .Item(ConstDtype, r).Value = "Dr"
                        .Item(ConstAmount, r).Value = Format(CDbl(dtAccTr(i)("DealAmt")), numFormat)
                    Else
                        .Item(ConstDtype, r).Value = "Cr"
                        .Item(ConstAmount, r).Value = Format(CDbl(dtAccTr(i)("DealAmt")) * -1, numFormat)
                    End If
                    .Item(ConstFCName, r).Value = dtAccTr(i)("CurrencyCode")
                    .Item(ConstFCRate, r).Value = dtAccTr(i)("CurrRate")
                    .Item(ConstFCAmount, r).Value = dtAccTr(i)("FCAmt")
                    .Item(ConstFCDec, r).Value = 2

                    .Item(Constchq, r).Value = dtAccTr(i)("ChqNo")
                    If Trim(dtAccTr(i)("ChqNo") & "") <> "" And Not IsDBNull(dtAccTr(i)("ChqDate")) Then
                        .Item(ConstChqdate, r).Value = Format(DateValue(dtAccTr(i)("ChqDate")), DtFormat)
                    End If

                    .Item(ConstBank, r).Value = dtAccTr(i)("BankCode")
                    .Item(ConstPdcCustAc, r).Value = dtAccTr(i)("pdcname")
                    .Item(ConstPdcCustAcno, r).Value = dtAccTr(i)("pdcid")
                    .Item(ConstUnq, r).Value = dtAccTr(i)("UnqNo")
                    .Item(ConstGrpSetOn, r).Value = dtAccTr(i)("GrpSetOn")
                    .Item(ConstAccountNo, r).Value = dtAccTr(i)("accountno")

                    '-------
                    If UCase(Trim(.Item(ConstGrpSetOn, r).Value & "")) = "BANK" Or UCase(Trim(.Item(ConstGrpSetOn, r).Value & "")) = "P.D.C.(I)" Or UCase(Trim(.Item(ConstGrpSetOn, r).Value & "")) = "P.D.C.(R)" Then
                        .Rows(r).Cells(Constchq).ReadOnly = False
                        .Rows(r).Cells(ConstChqdate).ReadOnly = False
                        .Rows(r).Cells(ConstBank).ReadOnly = False
                        .Rows(r).Cells(ConstPdcCustAc).ReadOnly = False
                    Else
                        .Rows(r).Cells(Constchq).ReadOnly = True
                        .Rows(r).Cells(ConstChqdate).ReadOnly = True
                        .Rows(r).Cells(ConstBank).ReadOnly = True
                        .Rows(r).Cells(ConstPdcCustAc).ReadOnly = True
                    End If
                End With
                
            Next
            FindNextCell(grdVoucher, grdVoucher.CurrentCell.RowIndex, grdVoucher.CurrentCell.ColumnIndex + 1)
            activecontrolname = ""
            btndelete.Enabled = True
            btnupdate.Enabled = True
        Else
            MsgBox("Payment Voucher Not Found", MsgBoxStyle.Exclamation)
        End If
        chgbyprg = False
    End Sub

    Private Sub dvData_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dvData.DoubleClick
        loadWaite(3)
    End Sub

    Private Sub txtDescription_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDescription.TextChanged
        If chgbyprg Then Exit Sub
        ChgId = True
        btnupdate.Enabled = True
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        If TabControl1.SelectedIndex = 0 Then
            fRptFormat = New RptFormatfrm
            fRptFormat.RptType = Mid(cmbVoucherTp.Text, 1, 2)
            fRptFormat.ShowDialog()
            fRptFormat = Nothing
        Else
            fRptFormat = New RptFormatfrm
            fRptFormat.RptType = "JVL"
            fRptFormat.ShowDialog()
            fRptFormat = Nothing
        End If
    End Sub

    Private Sub fRptFormat_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fRptFormat.FormClosed
        fRptFormat = Nothing
    End Sub

    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        LoadReport(RptFlName, RptCaption, forPrint)
    End Sub
    Private Sub LoadReport(ByVal FileName As String, ByVal RptCaption As String, Optional ByVal ToPrint As Boolean = False)
        If Not frm Is Nothing Then
            frm.Close()
            frm = New ReprtviewNEWfrm
        Else
            frm = New ReprtviewNEWfrm
        End If
        Dim ds As New DataSet
        _objTr = New clsAccountTransaction
        If TabControl1.SelectedIndex = 0 Then
            _objTr.PreFix = txtprintprefix.Text
            _objTr.JVNum = Val(numprintvrno.Text)
            If Mid(cmbVoucherTp.Text, 1, 2) = "PV" Then
                _objTr.TypeNo = 2
            ElseIf Mid(cmbVoucherTp.Text, 1, 2) = "RV" Then
                _objTr.TypeNo = 3
            Else
                _objTr.TypeNo = 1
            End If
            _objTr.JVType = Mid(cmbVoucherTp.Text, 1, 2)
            ds = _objTr.ldInvoice("rturnAccountVoucherForPrint")
        Else
            If RptdtTable Is Nothing Then
                With _objTr
                    .ptype = 1
                    .DateFrom = DateValue(dtpstart.Value)
                    .DateTo = DateValue(dtpto.Value)
                    .JVType = Mid(cmbVoucherTp.Text, 1, 2)
                    ds = .returnPaymentDetails()
                End With
            Else
                ds.Tables.Add(RptdtTable)
            End If
        End If
        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = Me.MdiParent
        frm.Text = IIf(RptCaption & "" = "", "VINVIS", RptCaption)
        frm.Show()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        If Not userType Then
            btndelete.Tag = 1
        End If
        If Val(btndelete.Tag) = 0 Then
            MsgBox("This user do not have permission to Remove", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If loadedTrId = 0 Then
            MsgBox("Select valid voucher for remove", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If Val(btndelete.Tag) = 0 Then
            MsgBox("This user do not have permission to remove", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If MsgBox("You are going to remove the Payment # " & numVchrNo.Text & " # Permenantly! Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
            Exit Sub
        End If
        '_objcmnbLayer._saveDatawithOutParm("DELETE FROM AccTrCmn WHERE LinkNo=" & loadedTrId)
        '_objcmnbLayer._saveDatawithOutParm("DELETE FROM AccTrDet WHERE LinkNo=" & loadedTrId)
        _objTr.deleteAccountTransaction(loadedTrId)
        makeClear()
        nextVoucher()
    End Sub

    Private Sub grdVoucher_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellContentClick

    End Sub

    Private Sub dvData_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dvData.CellContentClick

    End Sub

    Private Sub numVchrNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles numVchrNo.KeyPress
        NumericTextOnKeypress(numVchrNo, e, chgbyprg, "0")
    End Sub

    Private Sub numVchrNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numVchrNo.TextChanged

    End Sub

    Private Sub cmbprefix_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbprefix.SelectedIndexChanged
        cmbprefix.Tag = getvrsId(cmbprefix.Text, 7)
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
                ldGrid()
            Case 2
                saveTransaction()
            Case 3
                editRecord()
        End Select
        If Not fwait Is Nothing Then
            fwait.Close()
            fwait = Nothing
        End If
    End Sub

End Class