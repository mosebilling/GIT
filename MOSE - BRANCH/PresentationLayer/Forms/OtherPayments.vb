
Public Class OtherPayments
#Region "Constant Variables"
    Private Const ConstSlno = 0
    Private Const ConstRefNo = 2
    Private Const ConstAccountName = 1
    Private Const ConstDescription = 3
    Private Const ConstCheque = 4
    Private Const ConstChequeDt = 5
    Private Const ConstBank = 6
    Private Const ConstAmount = 7
    Private Const ConstExpAcc = 8
    Private Const ConstCrAcc = 9
    Private Const ConstAccSetId = 10

    'LIST GRID CONSTANT variables
    Private Const ConstInvNo = 0
    Private Const ConstTrdate = 1
    Private Const ConstInvAmount = 2
    Private Const ConstPaidTo = 3
    Private Const ConstCustId = 4
    Private Const ConstLinkNo = 5

#End Region
#Region "Local Variables"
    Private chgbyprg As Boolean
    Private activecontrolname As String
    Public isModi As Boolean
    Private NDec As Integer = 2
    Private continueVar As String
    Private loadedTrId As Long
    Private chgPost As Boolean
    Private dtTable As DataTable
    Private _srchTxtId As Integer
    Private _srchOnce As Boolean
    Private MyActiveControl As New Object
    Private RptdtTable As DataTable
    Private lstKey As Keys
#End Region
#Region "Class Object Declerations"
    Private _objcmnbLayer As New clsCommon_BL
    Private _objTr As New clsAccountTransaction
#End Region
#Region "Form Object Declerations"

    Private WithEvents fRptFormat As RptFormatfrm
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fMList As Mlistfrm
#End Region

    Private Sub OtherPayments_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        dtpdate.Focus()
    End Sub

    Private Sub OtherPayments_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetGridHead()
        ldExpense()
        NextJvNumber()
        If Not userType Then
            btndelete.Tag = 1
        Else
            btndelete.Tag = IIf(getRight(82, CurrentUser), 1, 0)
        End If
    End Sub
    Private Sub NextJvNumber()
        Dim vrPrefix As String = ""
        Dim vrVoucherNo As Long
        Dim vrAccountNo1 As Long
        Dim vrAccountNo2 As Long
        getVrsDet(0, "PV No", vrPrefix, vrVoucherNo, vrAccountNo1, vrAccountNo2)
        numVchrNo.Text = vrVoucherNo

    End Sub
    Sub SetGridHead()
        Try
            With grdVoucher
                SetEntryGridProperty(grdVoucher)

                .ColumnCount = 11

                .Columns(ConstSlno).HeaderText = "Sl.No"
                .Columns(ConstSlno).Width = 50
                .Columns(ConstSlno).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(ConstSlno).ReadOnly = True

                .Columns(ConstRefNo).HeaderText = "Ref.No"
                .Columns(ConstRefNo).Width = 75
                .Columns(ConstRefNo).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(ConstRefNo).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                .Columns(ConstAccountName).HeaderText = "Payment Mode"
                .Columns(ConstAccountName).Width = 100
                .Columns(ConstAccountName).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(ConstAccountName).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Columns(ConstAccountName).ReadOnly = True

                .Columns(ConstDescription).HeaderText = "Description"
                .Columns(ConstDescription).Width = Me.Width * 35 / 100   '100
                .Columns(ConstDescription).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(ConstDescription).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                .Columns(ConstCheque).HeaderText = "Chq No"
                .Columns(ConstCheque).Width = Me.Width * 10 / 100   '100
                .Columns(ConstCheque).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(ConstCheque).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                .Columns(ConstChequeDt).HeaderText = "Cheq Date"
                .Columns(ConstChequeDt).Width = 75
                .Columns(ConstChequeDt).SortMode = DataGridViewColumnSortMode.NotSortable
                Dim col As New CalendarColumn()
                .Columns.RemoveAt(ConstChequeDt)
                col.DataPropertyName = "ChqDate"
                .Columns.Insert(ConstChequeDt, col)

                .Columns(ConstChequeDt).HeaderText = "Cheq Date"

                .Columns(ConstBank).HeaderText = "No"
                .Columns(ConstBank).Visible = False

                .Columns(ConstAmount).HeaderText = "Amount"
                .Columns(ConstAmount).Width = 100
                .Columns(ConstAmount).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(ConstAmount).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(ConstAmount).DefaultCellStyle.Format = "N" & NoOfDecimal

                .Columns(ConstExpAcc).HeaderText = "No"
                .Columns(ConstExpAcc).Visible = False

                .Columns(ConstCrAcc).HeaderText = "CrNo"
                .Columns(ConstCrAcc).Visible = False

                .Columns(ConstAccSetId).HeaderText = "CrNo"
                .Columns(ConstAccSetId).Visible = False
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub
    Private Sub AddNewClick()
        AddRow()
        'grdVoucher.CurrentCell = grdVoucher.Item(ConstDescription, grdVoucher.CurrentRow.Index)
        btnModify.Text = "&Modify"
    End Sub
    Private Sub AddRow()
        If Val(txtexpense.Tag) = 0 Then
            MsgBox("Expense Account should be set in system parameter", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        With grdVoucher
            activecontrolname = "grdVoucher"
            .Rows.Add(1)
            .CurrentCell = .Item(ConstRefNo, .RowCount - 1)
            chgbyprg = True
            .BeginEdit(True)
            chgbyprg = False
        End With
        reArrangeNo()
    End Sub
    Private Sub RemoveRow()
        If grdVoucher.RowCount > 0 Then
            If MsgBox("You Are Going to Remove the Row." & vbCrLf & "Are You Sure ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then
                Exit Sub
            End If
            With grdVoucher
                .Rows.RemoveAt(.CurrentRow.Index)
                .ClearSelection()
            End With
            reArrangeNo()
        End If

    End Sub
    Private Sub reArrangeNo()
        Dim r As Integer

        chgbyprg = True
        With grdVoucher
            For r = 0 To .Rows.Count - 1 '- 1
                .Item(ConstSlno, r).Value = r + 1
            Next r
        End With
        chgbyprg = False
    End Sub

    'Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
    '    AddNewClick()
    'End Sub

    Private Sub btnrem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrem.Click
        RemoveRow()
    End Sub

    Private Sub btnexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexit.Click
        Me.Close()
    End Sub

    Private Sub grdVoucher_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles grdVoucher.CellBeginEdit
        chgPost = True
        btnupdate.Enabled = True
    End Sub

    Private Sub grdVoucher_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellClick
        If e.ColumnIndex = 0 Or e.RowIndex < 0 Then Exit Sub
        chgbyprg = True
        grdVoucher.BeginEdit(True)
        chgbyprg = False

    End Sub

    Private Sub grdVoucher_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellContentClick

    End Sub

    Private Sub grdVoucher_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellEndEdit
        Dim col As Integer = e.ColumnIndex
        Dim fmt As String = "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))
        If col = ConstAmount Then

            grdVoucher.Item(col, e.RowIndex).Value = Format(Convert.ToDecimal(grdVoucher.Item(col, e.RowIndex).Value), fmt)
        End If
    End Sub

    Private Sub grdVoucher_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellEnter
        If e.ColumnIndex > 0 Then
            chgbyprg = True
            grdVoucher.BeginEdit(True)
            chgbyprg = False
        End If
    End Sub

    Private Sub grdVoucher_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellValidated
        assignTotal()
        chgPost = True
    End Sub

    Private Sub grdVoucher_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdVoucher.EditingControlShowing
        Dim Col As Integer
        Col = grdVoucher.CurrentCell.ColumnIndex
        If Col = ConstAmount Then
            Dim tb As TextBox = CType(e.Control, TextBox)
            RemoveHandler tb.KeyPress, AddressOf Textbox_KeyPress
            AddHandler tb.KeyPress, AddressOf Textbox_KeyPress
        End If
    End Sub
    Private Sub Textbox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            Dim col As Integer
            col = grdVoucher.CurrentCell.ColumnIndex
            If col = ConstAmount Then
                If Not IsNumeric(e.KeyChar) And e.KeyChar <> "." And e.KeyChar <> "-" And Not e.KeyChar = Convert.ToChar(Keys.Back) Then e.Handled = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub grdVoucher_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.GotFocus
        'If grdVoucher.RowCount = 0 Then
        '    AddRow()
        'End If
        activecontrolname = "grdVoucher"
    End Sub

    Private Sub grdVoucher_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdVoucher.KeyDown
        If e.KeyCode = Keys.Enter Then
            If grdVoucher.RowCount = 0 Then Exit Sub
            If FindNextCell(grdVoucher, grdVoucher.CurrentCell.RowIndex, grdVoucher.CurrentCell.ColumnIndex + 1) Then
                AddRow()
                BankPdcCash(continueVar, grdVoucher) ' AddRow()
            End If
nxt:
            chgbyprg = True
            If grdVoucher.CurrentCell.ColumnIndex > 0 Then grdVoucher.BeginEdit(True)
            chgbyprg = False
        ElseIf e.KeyCode = Keys.F3 Then
            AddRow()
            BankPdcCash(btnPdc.Tag, grdVoucher)
        ElseIf e.KeyCode = Keys.F4 Then
            If grdVoucher.RowCount = 0 Then Exit Sub
            RemoveRow()
        End If
    End Sub

    Private Sub grdVoucher_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.Leave
        activecontrolname = ""
    End Sub

    Private Sub grdVoucher_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.RowEnter
        If chgbyprg Then Exit Sub
    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            Const WM_KEYDOWN As Integer = &H100
            Const WM_KEYUP As Integer = &H101
            'msg.ToString()
            If activecontrolname = "grdVoucher" Then
                If msg.Msg = WM_KEYDOWN Or msg.Msg = WM_KEYUP Then
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

    Private Sub btnCash_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCash.Click
        AddRow()
        With grdVoucher
            If .RowCount = 0 Then Exit Sub
            .CurrentCell = .Item(ConstRefNo, .Rows.Count - 1)
        End With
        BankPdcCash(btnCash.Tag, grdVoucher)
    End Sub
    Private Sub BankPdcCash(ByVal AccSetId As String, ByVal grd As DataGridView)
        If btnupdate.Enabled = False Then Exit Sub
        Dim Setid As String = ""
        With grd
            If .CurrentRow Is Nothing Then
                If .RowCount > 0 Then .CurrentCell = .Item(ConstRefNo, .Rows.Count - 1)
            End If
            Dim Dtbank As DataTable
            If AccSetId = "Bank" Then
                Setid = "09"
            ElseIf AccSetId = "P.D.C.(I)" Then
                Setid = "10"
            ElseIf AccSetId = "Cash" Then
                Setid = "08"
            ElseIf AccSetId = "Advance" Then
                Setid = "21"
            End If
            continueVar = AccSetId
            Dim sRow As Integer
            Dtbank = _objcmnbLayer._fldDatatable("select AccountNo,Alias,S1AccId,AccDescr,isnull(AccSetId,'') AccSetId from AccMast  where AccSetId like '%" & Setid & "%'")
            sRow = grd.CurrentRow.Index
            If Dtbank.Rows.Count > 0 Then
                With grdVoucher
                    .Item(ConstAccountName, sRow).Value = Dtbank(0)("AccDescr")
                    .Item(ConstCrAcc, sRow).Value = Dtbank(0)("AccountNo")
                    .Item(ConstAccSetId, sRow).Value = AccSetId
                    .Item(ConstExpAcc, sRow).Value = Val(txtexpense.Tag)
                End With
            End If
            If AccSetId = "Bank" And Val(txtBank.Tag) > 0 Then
                .Item(ConstAccountName, sRow).Value = txtBank.Text
                .Item(ConstCrAcc, sRow).Value = Val(txtBank.Tag)
            End If
            Dim read As Boolean = IIf(Trim(AccSetId) = "P.D.C.(I)" Or Trim(AccSetId) = "Bank", False, True)
            .Rows(sRow).Cells(ConstCheque).ReadOnly = read
            .Rows(sRow).Cells(ConstChequeDt).ReadOnly = read
            '.Rows(sRow).Cells(BankCode).ReadOnly = read
            '.Rows(sRow).Cells(PDCStatus).ReadOnly = read

            '.Select()
            reArrangeNo()
            .CurrentCell = .Item(ConstRefNo, sRow)
            activecontrolname = grd.Name
            chgbyprg = True
            .BeginEdit(True)
            chgbyprg = False
        End With
    End Sub

    Private Sub btnBank_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBank.Click
        AddRow()
        With grdVoucher
            .CurrentCell = .Item(ConstRefNo, .Rows.Count - 1)
        End With
        BankPdcCash(btnBank.Tag, grdVoucher)
    End Sub

    Private Sub btnPdc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPdc.Click
        AddRow()
        With grdVoucher
            .CurrentCell = .Item(ConstRefNo, .Rows.Count - 1)
        End With
        BankPdcCash(btnPdc.Tag, grdVoucher)
    End Sub
    Private Sub ldExpense()
        Dim Dtbank As DataTable
        Dtbank = _objcmnbLayer._fldDatatable("select AccountNo,Alias,S1AccId,AccDescr,isnull(AccSetId,'') AccSetId from dbo.AccMast where AccSetId like '%02%'")
        If Dtbank.Rows.Count > 0 Then
            txtexpense.Text = Dtbank(0)("AccDescr")
            txtexpense.Tag = Dtbank(0)("AccountNo")
        End If
    End Sub

    Private Sub numVchrNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles numVchrNo.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub assignTotal()
        Dim i As Integer
        Dim ttl As Double
        With grdVoucher
            For i = 0 To .RowCount - 1
                If CDbl(.Item(ConstAmount, i).Value) <> 0 Then
                    If .Item(ConstAccSetId, i).Value = "Advance" Then
                        ttl = ttl + (CDbl(.Item(ConstAmount, i).Value) * -1)
                    Else
                        ttl = ttl + CDbl(.Item(ConstAmount, i).Value)
                    End If
                End If
            Next
        End With
        lblTlDebit.Text = Format(ttl, numFormat)
    End Sub

    Private Sub grdVoucher_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.Validated
        assignTotal()
        chgPost = True
    End Sub
    Private Sub MakeClear()
        chgbyprg = True
        txtremarks.Text = ""
        txtBank.Text = ""
        txtpaidto.Text = ""
        txtBank.Tag = ""
        loadedTrId = 0
        dtpdate.Value = DateValue(Date.Now)
        If grdVoucher.RowCount > 0 Then grdVoucher.Rows.Clear()
        assignTotal()
        chgbyprg = False
    End Sub
    

    Private Sub ldRec()
        Dim dtAccTrCmn As DataTable
        Dim dtAccTr As DataTable
        Dim AccTrCmn As String
        Dim i As Integer
        Dim r As Integer
        Dim chqStatus As Boolean
        Dim chqStatusDt As DataTable
        chgbyprg = True
        AccTrCmn = "SELECT JVTypeNo,JVType,JVNum,PreFix,JVDate,UserId,LstModiUsrId,LinkNo,TYPENO,AccDescr,CmnBank,PaidTo,VrDescr FROM AccTrCmn LEFT JOIN AccMast ON AccTrCmn.CmnBank=Accmast.accountno WHERE LinkNo=" & Val(numVchrNo.Tag)
        grdVoucher.Rows.Clear()
        isModi = True
        dtAccTrCmn = _objcmnbLayer._fldDatatable(AccTrCmn)
        If dtAccTrCmn.Rows.Count > 0 Then
            loadedTrId = numVchrNo.Tag
            dtpdate.Value = DateValue(dtAccTrCmn(0)("JVDate"))
            numVchrNo.Text = dtAccTrCmn(0)("JVnum")
            txtBank.Text = Trim(dtAccTrCmn(0)("AccDescr") & "")
            txtBank.Tag = dtAccTrCmn(0)("CmnBank")
            txtpaidto.Text = Trim(dtAccTrCmn(0)("PaidTo") & "")
            txtremarks.Text = Trim(dtAccTrCmn(0)("VrDescr") & "")
            numPrintVchr.Text = dtAccTrCmn(0)("JVnum")
            dtAccTr = _objcmnbLayer._fldDatatable("SELECT * FROM AccTrDet LEFT JOIN AccMast ON AccTrDet.Accountno=Accmast.accountno WHERE LinkNO=" & loadedTrId & " ORDER BY UnqNo")
            With grdVoucher
                For i = 0 To dtAccTr.Rows.Count - 1
                    If CDbl(dtAccTr(i)("DealAmt")) < 0 Then
                        chqStatusDt = _objcmnbLayer._fldDatatable("select LinkNo dbcbnos,JVNum dbcbJvno,JVDate trfrDate  " & _
                                                      "from AccTrCmn where Linkno=" & Val(dtAccTr(0)("DBCBNo") & ""))
                        If chqStatusDt.Rows.Count > 0 Then
                            MsgBox("PDC Already Transfered! you can not modify/delete", MsgBoxStyle.Exclamation)
                            chqStatus = True
                        End If
                        grdVoucher.Rows.Add()
                        r = grdVoucher.Rows.Count - 1
                        .Item(ConstRefNo, r).Value = dtAccTr(i)("Reference")
                        .Item(ConstDescription, r).Value = dtAccTr(i)("EntryRef")
                        .Item(ConstAccountName, r).Value = dtAccTr(i)("AccDescr")
                        .Item(ConstCrAcc, r).Value = dtAccTr(i)("Accountno")
                        .Item(ConstCheque, r).Value = Trim(dtAccTr(i)("ChqNo") & "")
                        .Rows(r).Cells(ConstCheque).ReadOnly = True
                        .Rows(r).Cells(ConstChequeDt).ReadOnly = True
                        If dtAccTr(i)("TrInf") = 9 Then
                            .Item(ConstAccSetId, r).Value = "Bank"
                        ElseIf dtAccTr(i)("TrInf") = 10 Then
                            .Item(ConstAccSetId, r).Value = "P.D.C.(I)"
                        ElseIf dtAccTr(i)("TrInf") = 8 Then
                            .Item(ConstAccSetId, r).Value = "Cash"
                        ElseIf dtAccTr(i)("TrInf") = 21 Then
                            .Item(ConstAccSetId, r).Value = "Advance"
                        End If
                        If Not IsDBNull(dtAccTr(i)("ChqDate")) Then
                            If DateValue(dtAccTr(i)("ChqDate")) > DateValue("01/01/1950") Then
                                .Item(ConstChequeDt, r).Value = dtAccTr(i)("ChqDate")
                                .Rows(r).Cells(ConstCheque).ReadOnly = False
                                .Rows(r).Cells(ConstChequeDt).ReadOnly = False
                            End If
                        End If
                        .Item(ConstAmount, r).Value = Format(CDbl(dtAccTr(i)("DealAmt")) * -1, numFormat)
                    End If
                Next
            End With
            assignTotal()
            reArrangeNo()
            btnModify.Enabled = True
            btndelete.Enabled = True
        Else
            MsgBox("Payment Voucher Not Found", MsgBoxStyle.Exclamation)
        End If
        chgbyprg = False
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        btnupdate.Focus()
        If Not userType Then
            btnupdate.Tag = 1
        Else
            If isModi Then
                btnupdate.Tag = IIf(getRight(81, CurrentUser), 1, 0)
            Else
                btnupdate.Tag = IIf(getRight(80, CurrentUser), 1, 0)
            End If
        End If
        
        btnupdate.Focus()
        If Val(btnupdate.Tag) = 0 Then
            MsgBox("This user do not have permission to Update", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Verify()
    End Sub
    Private Sub Verify()
        If isModi Then
            'numVchrNo.Text = numVchrNo.Tag
            If chgPost = False Then
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
        
        If grdVoucher.RowCount = 0 Then
            MsgBox("Transactions not found!", MsgBoxStyle.Exclamation)
            Exit Sub
        Else
            Dim i As Integer
            For i = 0 To grdVoucher.Rows.Count - 1
                With grdVoucher
                    If .Item(ConstAccSetId, i).Value = "P.D.C.(I)" Then
                        If .Item(ConstCheque, i).Value = "" Then
                            MsgBox("Invalid Cheque No", MsgBoxStyle.Exclamation)
                            .CurrentCell = .Item(ConstCheque, i)
                            Exit Sub
                        End If
                        If Not IsDate(.Item(ConstChequeDt, i).Value) Then
                            MsgBox("Invalid Cheque Date !!", vbExclamation)
                            .CurrentCell = .Item(ConstChequeDt, i)
                            Exit Sub
                        End If
                    End If
                    If .Item(ConstAmount, i).Value = "" Then .Item(ConstAmount, i).Value = Format(0, numFormat)
                    If CDbl(.Item(ConstAmount, i).Value) = 0 Then
                        MsgBox("Invalid Amount", MsgBoxStyle.Exclamation)
                        .CurrentCell = .Item(ConstCheque, i)
                        Exit Sub
                    End If
                End With
            Next
        End If
        If MsgBox("Verification Succeded, Do you want to File it?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            If Not saveTrans() Then Exit Sub
            numPrintVchr.Text = numVchrNo.Text
            makeClear()
            If Not isModi Then
                SetNextVrNo(numVchrNo, 0, "PV No", "JvType = 'PV' AND JvNum = ", False, True, True)
            Else
                isModi = False
                btnModify_Click(btnModify, New System.EventArgs)
            End If
            MsgBox("Payment Voucher # " & numPrintVchr.Text & " Saved Successfully", MsgBoxStyle.Information)
        Else
            dtpdate.Focus()
        End If

    End Sub
    Private Function saveTrans() As Boolean
        saveTrans = True
        If Not isModi Then
chkagain:
            If Not CheckNoExists("", Val(numVchrNo.Text), "PV", "Accounts") Then
                If MsgBox("Payment No alreary exist! Do you want to fill next?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    numVchrNo.Text = Val(numVchrNo.Text) + 1
                    GoTo chkagain
                Else
                    saveTrans = False
                    Exit Function
                End If
            End If
        End If
        Dim LinkNo As Long
        setAcctrCmnValue()
        LinkNo = Val(_objTr.SaveAccTrCmn())
        'Update bankaccout in acccmn table
        '_objcmnbLayer._saveDatawithOutParm("UPDATE AccTrCmn SET CmnBank=" & Val(txtBank.Tag) & ",PaidTo='" & txtpaidto.Text & "' WHERE LinkNo=" & LinkNo)
        _objcmnbLayer._saveDatawithOutParm("DELETE FROM AccTrDet WHERE LinkNo=" & LinkNo)
        'Debit Entry
        setAcctrDetValue(LinkNo, Val(txtexpense.Tag), "ON A/C", txtremarks.Text, CDbl(lblTlDebit.Text), "", DateValue("01/01/1950"), 0)
        _objTr.saveAccTrans()
        Dim Setid As Integer
        Dim i As Integer
        'Dim crAc As Long
        With grdVoucher
            For i = 0 To .RowCount - 1
                If Trim(.Item(ConstDescription, i).Value & "") = "" Or Val(.Item(ConstAmount, i).Value & "") = 0 Then GoTo nxt
                If .Item(ConstAccSetId, i).Value = "Bank" Then
                    Setid = "9"
                ElseIf .Item(ConstAccSetId, i).Value = "P.D.C.(I)" Then
                    Setid = "10"
                ElseIf .Item(ConstAccSetId, i).Value = "Cash" Then
                    Setid = "8"
                ElseIf .Item(ConstAccSetId, i).Value = "Advance" Then
                    Setid = "21"
                End If
                'Credit Entry
                Dim dt As Date
                If IsDate(.Item(ConstChequeDt, i).Value) Then
                    dt = DateValue(.Item(ConstChequeDt, i).Value)
                Else
                    dt = DateValue("01/01/1950")
                End If
                setAcctrDetValue(LinkNo, IIf(Setid = "21" Or Val(txtBank.Tag) = 0, (.Item(ConstCrAcc, i).Value), Val(txtBank.Tag)), Trim(.Item(ConstRefNo, i).Value & ""), .Item(ConstDescription, i).Value, CDbl(.Item(ConstAmount, i).Value) * -1, .Item(ConstCheque, i).Value, dt, Setid)

                _objTr.saveAccTrans()
nxt:
            Next
        End With
    End Function
    Private Sub setAcctrCmnValue()
        _objTr.JVType = "PV"
        _objTr.JVDate = DateValue(dtpdate.Value)
        _objTr.PreFix = ""
        _objTr.JVNum = Val(numVchrNo.Text)
        _objTr.JVTypeNo = 102
        _objTr.UserId = CurrentUser
        _objTr.MchName = MACHINENAME
        _objTr.CrtDtTm = DateValue(Date.Now)
        _objTr.TypeNo = 0
        _objTr.VrDescr = Trim(txtremarks.Text)
        '_objTr.OthInf = txtpaidto.Text
        _objTr.IsModi = IIf(loadedTrId > 0, 2, 0)
        _objTr.LinkNo = loadedTrId
    End Sub
    Private Sub setAcctrDetValue(ByVal lnkNo As Long, ByVal AccountNo As Long, ByVal reference As String, ByVal EntryRef As String, ByVal Amount As Double, ByVal ChqNo As String, ByVal ChqDate As Date, ByVal AccsetId As Integer)
        With _objTr
            .trLinkNo = lnkNo
            .AccountNo = AccountNo
            .Reference = Trim(reference & "")
            .EntryRef = Trim(EntryRef & "")
            .DealAmt = Amount
            .FCAmt = Amount
            .JobCode = ""
            .JobStr = ""
            .CurrRate = 1
            .CurrencyCode = ""
            .TrInf = AccsetId
            .OthCost = 0
            .TermsId = ""
            .CustAcc = ConstExpAcc
            .AccWithRef = ""
            .DueDate = DateValue(Date.Now)
            .DocDate = DateValue(Date.Now)
            .SuppInvDate = DateValue(Date.Now)
            .ChqDate = ChqDate
            .ChqNo = ChqNo
            .LPONo = ""
        End With
    End Sub

    Private Sub btnModify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModify.Click
        If btnModify.Text = "&Clear" Then
            MakeClear()
            numVchrNo.Focus()
            NextJvNumber()
        Else
            If isModi Then
                TabControl1.SelectedIndex = 1
                btnModify.Enabled = False
                btnupdate.Enabled = False
                btndelete.Enabled = False
            End If
            MakeClear()

            btnModify.Text = "&Clear"
            isModi = False
        End If
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        If TabControl1.SelectedIndex = 0 Then
            If chkFormat.Checked Then
                fRptFormat = New RptFormatfrm
                fRptFormat.RptType = "PVO"
                fRptFormat.ShowDialog()
                fRptFormat = Nothing
            Else
                PrepareRpt("PVO")
            End If
        Else
            fRptFormat = New RptFormatfrm
            fRptFormat.RptType = IIf(chkchq.Checked, "PVOCL", "PVOL")
            fRptFormat.ShowDialog()
            fRptFormat = Nothing
        End If

    End Sub
    Private Sub PrepareRpt(ByVal RptType As String, Optional ByVal Forprint As Boolean = False, Optional ByVal isF As Boolean = False)
        If Val(numVchrNo.Text) = 0 And Not isF Then
            MsgBox("Enter a valid Voucher Number!!", vbInformation)
            numVchrNo.Focus()
            Exit Sub
        End If
        Dim RptName As String
        Dim RptCaption As String = ""
        RptName = getRptDefFlName(RptType, RptCaption)
        If Trim(RptName) <> "" Then
            LoadReport(RptName, RptCaption, Forprint)
        End If
    End Sub
    Private Sub LoadReport(ByVal FileName As String, ByVal RptCaption As String, Optional ByVal ToPrint As Boolean = False)
        If Not frm Is Nothing Then
            frm.Close()
            frm = New ReprtviewNEWfrm
        Else
            frm = New ReprtviewNEWfrm
        End If
        Dim ds As New DataSet
        If TabControl1.SelectedIndex = 0 Then
            _objTr.PreFix = ""
            _objTr.JVNum = Val(numPrintVchr.Text)
            _objTr.JVType = "PVO"
            ds = _objTr.ldInvoice("rturnAccountVoucherForPrint")
        Else
            If RptdtTable Is Nothing Then
                With _objTr
                    .ptype = IIf(chkchq.Checked, 3, 2)
                    .DateFrom = DateValue(dtpstart.Value)
                    .DateTo = DateValue(dtpto.Value)
                    If chkchq.Checked And txtSeq.Text <> "" Then
                        RptdtTable = .returnPaymentDetails.Tables(0)
                        RptdtTable = SearchGrid(RptdtTable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
                        ds.Tables.Add(RptdtTable)
                    Else
                        ds = .returnPaymentDetails()
                    End If

                End With
            Else
                ds.Tables.Add(RptdtTable)
            End If
        End If
        RptdtTable = Nothing
        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = Me.MdiParent
        frm.Text = RptCaption
        frm.Show()
    End Sub

    Private Sub dtpdate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtpdate.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub


    Private Sub btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnload.Click
        ldGrid()
    End Sub
    Private Sub ldGrid()
        'Dim strQry As String
        'strQry = " SELECT convert(varchar(12), PreFix + case when rTrim(PreFix) = '' then '' else '-' end + convert(varchar(12),JVNum) ) as [Inv No],convert(varchar(11),JVDate,106) as [Tr Date],abs(DealAmt) [Amount], Alias,AccDescr,EntryRef Description,JVType,AccTrCmn.LinkNo FROM (AccTrDet" & _
        '" INNER JOIN AccTrCmn ON AccTrCmn.LinkNo = AccTrDet.LinkNo LEFT JOIN AccMast ON AccTrDet.AccountNo = AccMast.AccountNo)  WHERE JVTypeNo=102 AND JVType = 'PV ' AND DealAmt<0 and JVDate>='" & Format(DateValue(dtpstart.Value), "yyyy/MM/dd") & "' and JVDate<='" & Format(DateValue(dtpto.Value), "yyyy/MM/dd") & "'  ORDER BY Jvdate,Jvnum"
        'strQry = " SELECT convert(varchar(12), PreFix + case when rTrim(PreFix) = '' then '' else '-' end + convert(varchar(12),JVNum) ) as [Inv No],convert(varchar(11)," & _
        '            " JVDate,106) as [Tr Date],TtlAmt [Amount],PaidTo, VrDescr Description,AccTrCmn.LinkNo FROM " & _
        '            " AccTrCmn LEFT JOIN (SELECT SUM(case when DealAmt>0 then DealAmt else 0 END) TtlAmt,LinkNo From AccTrDet GROUP BY LinkNo) Amt ON AccTrCmn.LinkNo = Amt.LinkNo " & _
        '            "WHERE JVTypeNo=102 AND JVType = 'PV' AND JVDate>='" & Format(DateValue(dtpstart.Value), "yyyy/MM/dd") & "' and JVDate<='" & Format(DateValue(dtpto.Value), "yyyy/MM/dd") & "'  ORDER BY Jvdate,Jvnum"
        With _objTr
            .ptype = 2
            .DateFrom = DateValue(dtpstart.Value)
            .DateTo = DateValue(dtpto.Value)
            dtTable = .returnPaymentDetails.Tables(0)
        End With
        'dtTable = _objcmnbLayer._fldDatatable(strQry)
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


            .Columns(ConstInvNo).HeaderText = "PV No"
            .Columns(ConstInvNo).Width = 75
            '.Columns(ConstInvNo).SortMode = DataGridViewColumnSortMode.Automatic

            .Columns(ConstTrdate).HeaderText = "Tr Date"
            .Columns(ConstTrdate).Width = 100
            .Columns(ConstTrdate).SortMode = DataGridViewColumnSortMode.Automatic
            .Columns(ConstTrdate).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

            .Columns(ConstInvAmount).HeaderText = "Amount"
            .Columns(ConstInvAmount).Width = 100
            .Columns(ConstInvAmount).SortMode = DataGridViewColumnSortMode.Automatic
            .Columns(ConstInvAmount).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstInvAmount).DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns(ConstPaidTo).HeaderText = "Paid To"
            .Columns(ConstPaidTo).Width = 200
            .Columns(ConstPaidTo).SortMode = DataGridViewColumnSortMode.Automatic
            .Columns(ConstPaidTo).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(ConstPaidTo).Visible = True

            .Columns(ConstCustId).HeaderText = "Description"
            .Columns(ConstCustId).Width = 500
            .Columns(ConstCustId).SortMode = DataGridViewColumnSortMode.Automatic
            .Columns(ConstCustId).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(ConstCustId).Visible = True

            .Columns(ConstLinkNo).HeaderText = "Link No"
            .Columns(ConstLinkNo).Width = 250 '100
            .Columns(ConstLinkNo).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstLinkNo).Visible = False

            .Columns("DateFrom").Visible = False
            .Columns("DateTo").Visible = False
            .Columns("Lnk").Visible = False

        End With
    End Sub

    Private Sub txtSeq_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeq.TextChanged
        dvData.DataSource = SearchGrid(dtTable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
        If Not chkchq.Checked Then RptdtTable = SearchGrid(dtTable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
    End Sub

    Private Sub TabControl1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.Click
        If btnModify.Text = "Undo" And TabControl1.SelectedIndex = 1 Then
            MsgBox("Do Undo/Update before moving to Enquiry", MsgBoxStyle.Exclamation)
            TabControl1.SelectedIndex = 0
            Exit Sub
        End If
        If TabControl1.SelectedIndex = 1 And grdVoucher.RowCount = 0 Then
            ldGrid()
        End If
        If TabControl1.SelectedIndex = 1 Then
            btnModify.Enabled = False
            btndelete.Enabled = False
            btnupdate.Enabled = False
        Else
            btnModify.Enabled = True
            If Not isModi Then NextJvNumber()
            'btnupdate.Enabled = True
        End If
    End Sub

    Private Sub txtremarks_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtremarks.TextChanged
        If chgbyprg Then Exit Sub
        chgPost = True
    End Sub


    Private Sub dvData_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dvData.CellDoubleClick
        btnModify_Click(btnModify, New System.EventArgs)
        numVchrNo.Tag = dvData.Item(ConstLinkNo, dvData.CurrentRow.Index).Value
        ldRec()
        btnModify.Text = "Undo"
        isModi = True
        TabControl1.SelectedIndex = 0
    End Sub

    Private Sub txtBank_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBank.KeyDown, txtpaidto.KeyDown
        lstKey = e.KeyCode
        Dim myctrl As Object = sender
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Up Or e.KeyCode = Keys.PageUp Then
            If fMList.Visible Then
                fMList.MoveUp(myctrl.Text)
                Exit Sub
            End If
        ElseIf e.KeyCode = Keys.Down Or e.KeyCode = Keys.PageDown Then
            If Not fMList Is Nothing Then
                If fMList.Visible Then
                    fMList.MoveDown(myctrl.Text)
                    Exit Sub
                End If
            End If
        End If
    End Sub

    Private Sub txtpaidto_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpaidto.TextChanged, txtBank.TextChanged
        If chgbyprg = True Then Exit Sub
        Dim myCtrl As Control = sender
        Select Case myCtrl.Name
            Case "txtpaidto"
                _srchTxtId = 1
                _srchOnce = False
                ShowFmlist(sender)
            Case "txtBank"
                _srchTxtId = 2
                _srchOnce = False
                ShowFmlist(sender)
        End Select
        chgPost = True
        btnupdate.Enabled = True
    End Sub
    Private Sub ShowFmlist(ByVal myCtrl As Control)
        If chgbyprg Then Exit Sub
        MyActiveControl = myCtrl
        If Not _srchOnce Then
            chgbyprg = True
            If fMList Is Nothing Then
                fMList = New Mlistfrm
            End If
            Dim PopupLoc As Point
            Dim x As Integer = Me.Width - fMList.Width - 100
            Dim y As Integer = Me.Height - fMList.Height - 100
            PopupLoc = New Point(x, y)
            If fMList.Visible = False Then
                Select Case _srchTxtId
                    Case 1
                        SetFmlist(fMList, 19)
                    Case 2
                        SetFmlist(fMList, 15)
                End Select
                fMList.loc = PopupLoc
                fMList.Show(fMainForm)
                fMList.Visible = True
            End If
        End If
        Select Case _srchTxtId
            Case 1   'Piad to
                'fMList.isSingle = True
                fMList.SearchIndex = 0
                fMList.SearchIndexDescr = 0
                fMList_doFocus()
                fMList.Search(txtpaidto.Text)
                fMList.AssignList(txtpaidto, lstKey, chgbyprg)
            Case 2   'Bank 
                fMList.isSingle = False
                fMList.SearchIndex = 0
                fMList.SearchIndexDescr = 1
                fMList_doFocus()
                fMList.Search(txtBank.Text)
                fMList.AssignList(txtBank, lstKey, chgbyprg)
        End Select
        _srchOnce = True
        chgbyprg = False
    End Sub

    Private Sub fMList_doClose() Handles fMList.doClose
        fMList = Nothing
    End Sub

    Private Sub fMList_doFocus() Handles fMList.doFocus
        If TypeOf (MyActiveControl) Is TextBox Then
            MyActiveControl.focus()
        End If
    End Sub

    Private Sub fMList_doSelect(ByVal ItmFlds() As String) Handles fMList.doSelect
        chgbyprg = True
        Select Case _srchTxtId
            Case 1
                txtpaidto.Text = ItmFlds(0)
            Case 2
                txtBank.Text = ItmFlds(0)
                txtBank.Tag = ItmFlds(7)
        End Select
        chgbyprg = False
    End Sub

    Private Sub txtBank_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBank.Validated, txtpaidto.Validated
        If fMList Is Nothing Then Exit Sub
        If fMList.Visible Then
            fMList.Close()
            fMList = Nothing
        End If
        Dim myctrl As TextBox = sender
        Dim dt As DataTable
        If myctrl.Name = "txtBank" Then
            dt = _objcmnbLayer._fldDatatable("SELECT AccountNo FROM AccMast WHERE AccDescr='" & MkDbSrchStr(txtBank.Text) & "'")
            If dt.Rows.Count > 0 Then
                txtBank.Tag = dt(0)(0)
            Else
                txtBank.Tag = 0
            End If
        End If
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        If loadedTrId = 0 Or isModi = False Then
            MsgBox("Select valid voucher for remove", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If Val(btndelete.Tag) = 0 Then
            MsgBox("This user do not have permission to Remove", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If MsgBox("You are going to remove the Payment # " & numVchrNo.Text & " # Permenantly! Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
            Exit Sub
        End If
        '_objcmnbLayer._saveDatawithOutParm("DELETE FROM AccTrCmn WHERE LinkNo=" & loadedTrId)
        '_objcmnbLayer._saveDatawithOutParm("DELETE FROM AccTrDet WHERE LinkNo=" & loadedTrId)
        _objTr.deleteAccountTransaction(loadedTrId)
        btnModify_Click(btnModify, New System.EventArgs)
    End Sub

    Private Sub dtpdate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpdate.ValueChanged
        If chgbyprg Then Exit Sub
        chgPost = True
        btnupdate.Enabled = True
    End Sub

    Private Sub btnAdvance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdvance.Click
        AddRow()
        With grdVoucher
            .CurrentCell = .Item(ConstRefNo, .Rows.Count - 1)
        End With
        BankPdcCash(btnAdvance.Tag, grdVoucher)
    End Sub

    Private Sub btnedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnedit.Click
        If dvData.RowCount = 0 Then Exit Sub
        btnModify_Click(btnModify, New System.EventArgs)
        numVchrNo.Tag = dvData.Item(ConstLinkNo, dvData.CurrentRow.Index).Value
        ldRec()
        btnModify.Text = "Undo"
        isModi = True
        TabControl1.SelectedIndex = 0
    End Sub

    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        LoadReport(RptFlName, RptCaption, forPrint)
    End Sub
    
End Class