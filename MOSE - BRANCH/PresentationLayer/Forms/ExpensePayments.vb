
Public Class ExpensePayments
#Region "Local Variable Declerations"
    Public isModi As Boolean
    Private activecontrolname As String
    Public chgbyprg As Boolean
    Public editlinkno As Long

    Private NDec As Integer
    Private _srchTxtId As Integer
    Private _srchOnce As Boolean
    Private chgPost As Boolean
    Private loadedTrId As Long
    Private MyActiveControl As New Object
    Private lstKey As Keys
    Private dtTable As DataTable
    Private RptdtTable As DataTable
    Private SrchText As String
    Private _NewRw As Boolean
    Private strGridSrchString As String
    Private _srchIndexId As Integer
    Private ChgId As Boolean
    Private dtSetoffTable As New DataTable
    Private lnumformat As String
#End Region
#Region "Constant Declerations"

    'const declarations
    Private Const ConstAlias = 0
    Private Const ConstName = 1
    Private Const ConstReference = 2
    Private Const ConstDescr = 3
    Private Const ConstDtype = 4
    Private Const Constvatper = 5
    Private Const ConstFCName = 6
    Private Const ConstFCRate = 7
    Private Const ConstFCAmount = 8
    Private Const ConstJob = 9
    Private Const ConstPAmount = 10
    Private Const ConstvatAmt = 11
    Private Const Constchq = 12
    Private Const ConstChqdate = 13
    Private Const ConstBank = 14
    Private Const ConstPdcCustAc = 15
    Private Const ConstPdcCustAcno = 16
    Private Const ConstFCDec = 17
    Private Const ConstAccountNo = 18
    Private Const ConstBrId = 19
    Private Const ConstGrpSetOn = 20
    Private Const ConstUnq = 21

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
#Region "Form Object Declarations"
    Private WithEvents fMList As Mlistfrm
    Private WithEvents fSelect As Selectfrm
    Private WithEvents fRptFormat As RptFormatfrm
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fPpayment As PreviousPaymentFrm
    Private WithEvents fwait As WaitMessageFrm
#End Region
#Region "Class Object Declerations"
    Private _objcmnbLayer As New clsCommon_BL
    Private _objTr As New clsAccountTransaction
#End Region
#Region "Numeric Field"
    Private idx As Integer
    Private numCtrl As TextBox
    Private SelStart As Integer
    Private str1 As String
    Private str2 As String
    Private str3 As String
#End Region

    Private Sub ExpensePayments_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        dtpdate.Focus()
    End Sub


    Private Sub btnexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexit.Click
        If MsgBox("Do you want to exit?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        Me.Close()
    End Sub

    Private Sub AddRow()
        With grdpayment
            activecontrolname = "grdpayment"
            .Rows.Add(1)
            .CurrentCell = .Item(ConstDescr, .RowCount - 1)
            .Item(ConstDtype, .RowCount - 1).Value = "Cr"
            chgbyprg = True
            .BeginEdit(True)
            chgbyprg = False
        End With
    End Sub
    Private Sub AddExpRow()
        With grdVoucher
            activecontrolname = "grdVoucher"
            .Rows.Add(1)
            .CurrentCell = .Item(ConstAlias, .RowCount - 1)
            .Item(ConstDtype, .RowCount - 1).Value = "Dr"
            chgbyprg = True
            .BeginEdit(True)
            chgbyprg = False
        End With
    End Sub
    Private Sub RemoveRow()
        If grdpayment.RowCount > 0 Then
            If MsgBox("You Are Going to Remove the Row." & vbCrLf & "Are You Sure ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then
                Exit Sub
            End If
            With grdpayment
                .Rows.RemoveAt(.CurrentRow.Index)
                .ClearSelection()
            End With
        End If
        assaignTotal()
        btnupdate.Enabled = True
    End Sub
    Private Sub RemoveExpRow()
        If grdVoucher.RowCount > 0 Then
            If MsgBox("You Are Going to Remove the Row." & vbCrLf & "Are You Sure ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then
                Exit Sub
            End If
            With grdVoucher
                .Rows.RemoveAt(.CurrentRow.Index)
                .ClearSelection()
            End With
        End If
        assaignTotal()
        btnupdate.Enabled = True
    End Sub


    Private Sub btnrem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrem.Click
        RemoveRow()
    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            Const WM_KEYDOWN As Integer = &H100
            Const WM_KEYUP As Integer = &H101
            'msg.ToString()
            If msg.Msg = WM_KEYDOWN Or msg.Msg = WM_KEYUP Then
                If msg.WParam.ToInt32() = CInt(Keys.F8) Then
                    UpdateClick()
                ElseIf activecontrolname = "grdVoucher" Then
                    If msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up) Or msg.WParam.ToInt32() = CInt(Keys.Enter) Or msg.WParam.ToInt32() = CInt(Keys.F2) Or msg.WParam.ToInt32() = CInt(Keys.F3) Or msg.WParam.ToInt32() = CInt(Keys.F4) Then
                        grdVoucher_KeyDown(Nothing, New KeyEventArgs(keyData))
                        Return True
                    End If
                ElseIf activecontrolname = "grdpayment" Then
                    If msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up) Or msg.WParam.ToInt32() = CInt(Keys.Enter) Or msg.WParam.ToInt32() = CInt(Keys.F2) Or msg.WParam.ToInt32() = CInt(Keys.F3) Or msg.WParam.ToInt32() = CInt(Keys.F4) Then
                        grdpayment_KeyDown(Nothing, New KeyEventArgs(keyData))
                        Return True
                    End If
                End If
                End If

ctn:
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub grdVoucher_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles grdVoucher.CellBeginEdit
        chgPost = True
        btnupdate.Enabled = True
    End Sub

    Private Sub grdVoucher_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellClick
        BeginEditExp()
    End Sub
    Public Sub BeginEditExp()
        chgbyprg = True
        With grdVoucher

            If .RowCount = 0 Then Exit Sub
            activecontrolname = "grdVoucher"
            .BeginEdit(True)

        End With
        chgbyprg = False
    End Sub
    

    Private Sub grdVoucher_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellEndEdit
        Dim col As Integer = e.ColumnIndex
        Dim fmt As String = "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))
        If col = ConstPAmount Then
            grdVoucher.Item(col, e.RowIndex).Value = Format(Convert.ToDecimal(grdVoucher.Item(col, e.RowIndex).Value), fmt)
        End If
    End Sub

    Private Sub grdVoucher_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellValidated
        If chgbyprg Then Exit Sub
        Valid(e.RowIndex, e.ColumnIndex, grdVoucher)
        SrchText = ""
    End Sub

    Private Sub grdVoucher_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdVoucher.EditingControlShowing
        Dim Col As Integer
        Col = grdVoucher.CurrentCell.ColumnIndex
        If Col = ConstAlias Or Col = ConstFCName Or Col = ConstName Or Col = ConstJob Or Col = ConstPdcCustAc Or _
        Col = ConstPAmount Or Col = ConstFCAmount Or Col = ConstFCRate Then
            Dim tb As TextBox = CType(e.Control, TextBox)
            RemoveHandler tb.TextChanged, AddressOf Textbox_TextChanged
            AddHandler tb.TextChanged, AddressOf Textbox_TextChanged
        End If
        If Col = ConstPAmount Or Col = ConstFCAmount Or Col = ConstFCRate Or Col = Constvatper Then
            Dim tb As TextBox = CType(e.Control, TextBox)
            RemoveHandler tb.KeyPress, AddressOf Textbox_KeyPress
            AddHandler tb.KeyPress, AddressOf Textbox_KeyPress
        End If
    End Sub

    Private Sub grdVoucher_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.Enter
        Try
            If grdVoucher.Rows.Count = 0 Then Exit Sub
            BeginEditExp()
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
                        AddExpRow()
                    End If
                    'If grdVoucher.CurrentRow.Index = 0 Then grdVoucher.Tag = grdVoucher.Item(ConstDtype, grdVoucher.CurrentCell.RowIndex).Value
                    plsrch.Visible = False
                    BeginEditExp()
                End With
            ElseIf e.KeyCode = Keys.Up Or e.KeyCode = Keys.PageUp Then
                If grdVoucher.RowCount = 0 Then Exit Sub
                If plsrch.Visible Then
                    doSelect(0, grdVoucher)
                    Exit Sub
                End If
            ElseIf e.KeyCode = Keys.Down Or e.KeyCode = Keys.PageDown Then
                If grdVoucher.RowCount = 0 Then Exit Sub
                If plsrch.Visible Then
                    doSelect(1, grdVoucher)
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

    Private Sub grdVoucher_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.Leave
        activecontrolname = ""
    End Sub

    Private Sub grdVoucher_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.RowEnter
        If chgbyprg Then Exit Sub
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
            Dim y As Integer = Me.Height - fMList.Height + 50
            PopupLoc = New Point(x, y)
            If fMList.Visible = False Then
                Select Case _srchTxtId
                    Case 1, 2
                        SetFmlist(fMList, 3)
                    Case 6 'job 
                        SetFmlist(fMList, 8)
                End Select
                fMList.loc = PopupLoc
                fMList.Show(fMainForm)
                fMList.Visible = True
            End If
        End If
        Select Case _srchTxtId
            Case 6   'job
                fMList.SearchIndex = 0
                'fMList.SearchIndexDescr = 0
                fMList_doFocus()
                fMList.Search(txtjobcode.Text)
                fMList.AssignList(txtjobcode, lstKey, chgbyprg)
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
            Case 6
                txtjobcode.Text = ItmFlds(0)
            Case 2
                txtBank.Text = ItmFlds(0)
                txtBank.Tag = ItmFlds(3)
        End Select
        chgbyprg = False
    End Sub

    Private Sub cmbVoucherTp_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVoucherTp.SelectedIndexChanged
        Try
            Dim vrPrefix As String = ""
            Dim vrVoucherNo As Long
            Dim vrAccountNo1 As Long
            Dim vrAccountNo2 As Long
            With cmbVoucherTp
                If .Items.Count > 0 Then
                    If .SelectedIndex < 0 Then .SelectedIndex = 0
                    cmbVoucherTp.Tag = getvrsId(cmbVoucherTp.Text, 1)
                    getVrsDet(Val(cmbVoucherTp.Tag), "PV", vrPrefix, vrVoucherNo, vrAccountNo1, vrAccountNo2)
                Else
                    getVrsDet(0, "PV", vrPrefix, vrVoucherNo, vrAccountNo1, vrAccountNo2)
                End If

            End With
            If Not isModi Then
                numVchrNo.Text = vrVoucherNo
                txtPreFix.Text = vrPrefix
            End If

            'Dim dtTable As DataTable
            'Dim _qurey As EnumerableRowCollection(Of DataRow)
            '_qurey = From data In dtAcc.AsEnumerable() Where data("accid") = vrAccountNo1 Select data
            'If _qurey.Count > 0 Then
            '    dtTable = _qurey.CopyToDataTable()
            'Else
            '    dtTable = dtAcc.Clone
            'End If
            Dim dtTable As DataTable
            dtTable = _objcmnbLayer._fldDatatable("SELECT Alias,AccDescr,GrpSetOn,GrpSetOn,accid FROM AccMast " & _
                                                "LEFT JOIN S1AccHd ON AccMast.S1AccId=S1AccHd.S1AccId WHERE accid=" & vrAccountNo1)


            Dim i As Integer
            'assaignTotal()
            If dtTable.Rows.Count > 0 Then
                With grdpayment
                    chgbyprg = True
                    If Not grdpayment.RowCount > 0 Then
                        .Rows.Add(1)
                    End If
                    i = .CurrentRow.Index
                    .Item(ConstDtype, i).Value = "Cr"
                    .Item(ConstAccountNo, i).Value = Val(dtTable(0)("accid"))
                    lblaccountbalance.Text = 0 ' getAccBal(Val(dtAcc(0)("accid")))
                    lblaccountbalance.Text = dtTable(0)("AccDescr") & ": " & Format(CDbl(lblaccountbalance.Text), numFormat)
                    .Item(ConstName, i).Value = dtTable(0)("AccDescr")
                    .Item(ConstAlias, i).Value = dtTable(0)("Alias")
                    .Item(ConstFCRate, i).Value = 1
                    .Item(ConstGrpSetOn, i).Value = Trim(dtTable(0)("GrpSetOn"))

                    If UCase(Trim(dtTable(0)("GrpSetOn"))) = "BANK" Or UCase(Trim(dtTable(0)("GrpSetOn"))) = "P.D.C.(I)" Then
                        .Item(ConstChqdate, .CurrentRow.Index).Value = Format(DateValue(Date.Now), "dd/MM/yyyy")
                        .Rows(.CurrentCell.RowIndex).Cells(Constchq).ReadOnly = False
                        .Rows(.CurrentCell.RowIndex).Cells(ConstChqdate).ReadOnly = False
                        .Rows(.CurrentCell.RowIndex).Cells(ConstBank).ReadOnly = False
                        .Rows(.CurrentCell.RowIndex).Cells(ConstPdcCustAc).ReadOnly = False
                    Else
                        .Item(Constchq, .CurrentRow.Index).Value = ""
                        .Item(ConstBank, .CurrentRow.Index).Value = ""
                        .Item(ConstChqdate, .CurrentRow.Index).Value = System.DBNull.Value
                        .Rows(.CurrentCell.RowIndex).Cells(Constchq).ReadOnly = True
                        .Rows(.CurrentCell.RowIndex).Cells(ConstChqdate).ReadOnly = True
                        .Rows(.CurrentCell.RowIndex).Cells(ConstBank).ReadOnly = True
                        .Rows(.CurrentCell.RowIndex).Cells(ConstPdcCustAc).ReadOnly = True
                    End If
                    .Select()
                    .CurrentCell = .Item(ConstReference, i)
                    grdpayment.BeginEdit(True)
                    chgbyprg = False
                End With
            Else
                MsgBox("Voucher Settings Not Found", MsgBoxStyle.Critical)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        UpdateClick()
    End Sub
    Private Sub UpdateClick()
        If Not userType Then
            btnupdate.Tag = 1
        Else
            If isModi Then
                btnupdate.Tag = IIf(getRight(67, CurrentUser), 1, 0)
            Else
                btnupdate.Tag = IIf(getRight(66, CurrentUser), 1, 0)
            End If
        End If
        assaignTotal()
        btnupdate.Focus()
        If Val(btnupdate.Tag) = 0 Then
            MsgBox("This user do not have permission to Update", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Verify()
    End Sub

    Private Sub numVchrNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles numVchrNo.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
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
        If Val(numVchrNo.Text) = 0 Then
            MsgBox("Invalid Voucher Number", MsgBoxStyle.Exclamation)
            numVchrNo.Focus()
            Exit Sub
        End If
        If CDate(dtpdate.Value) < DateFrom Or CDate(dtpdate.Value) > DateTo Then
            MsgBox("Invalid Voucher Date !!", MsgBoxStyle.Exclamation)
            dtpdate.Focus()
            Exit Sub
        End If
        If DateValue(dtpdate.Value) <= ProtectUntil Then
            MsgBox("Voucher Date comes within Protected Date Range !", MsgBoxStyle.Exclamation)
            dtpdate.Focus()
            Exit Sub
        End If
        If CDbl(lbldiff.Text) <> 0 Or (CDbl(lblcredit.Text) = 0 And CDbl(lblTlDebit.Text) = 0) Then
            MsgBox("Amount is not balanced or empty entry.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If Not isGridValidCredit() Then Exit Sub
        If Not isGridValidDebit() Then Exit Sub
        Dim i As Integer
        For i = 0 To grdpayment.RowCount - 1
            If UCase(grdpayment.Item(ConstDtype, i).Value) = "CR" Then
                If Not checkReconciliation(grdpayment, ConstAccountNo, ConstPAmount, ConstName, i) Then Exit Sub
            End If
        Next
        'If enableBranch And UsrBr = "" Then
        '    MsgBox("Transaction cannot be saved without Branch! Please login with Branch", MsgBoxStyle.Exclamation)
        '    Exit Sub
        'End If
        If MsgBox("Verification Succeded, Do you want to File it?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            'If Not saveTrans() Then Exit Sub
            'numPrintVchr.Text = numVchrNo.Text
            'makeClear()
            loadWaite(2)
        Else
            dtpdate.Focus()
        End If

    End Sub
    Private Function isGridValidDebit() As Boolean
        Dim i As Integer
        Dim grpset As String
        With grdVoucher
            If .Rows.Count > 0 Then
                For i = 0 To .Rows.Count - 1
                    If Val(.Item(ConstAccountNo, i).Value) = 0 Then
                        MsgBox("Invalid Expense!", MsgBoxStyle.Exclamation)
                        .CurrentCell = .Item(ConstDescr, i)
                        .BeginEdit(True)
                        Return False
                    End If
                    If Val(.Item(ConstPAmount, i).Value) = 0 Then
                        MsgBox("Invalid Amount!", MsgBoxStyle.Exclamation)
                        .CurrentCell = .Item(ConstPAmount, i)
                        .BeginEdit(True)
                        Return False
                    End If
                    grpset = .Item(ConstGrpSetOn, i).Value
                    If grpset = "P.D.C.(I)" Or grpset = "P.D.C.(R)" Then
                        MsgBox("P.D.C can not be Debited !", MsgBoxStyle.Exclamation)
                        .CurrentCell = .Item(ConstDtype, i)
                        .BeginEdit(True)
                        Return False
                    End If
                Next
            End If
        End With
        Return True
    End Function
    Private Function isGridValidCredit() As Boolean
        Dim i As Integer
        Dim grpset As String
        Dim SqlQuery As String
        Dim _vAcctrdatatable As DataTable
        With grdpayment
            If .Rows.Count > 0 Then
                For i = 0 To .Rows.Count - 1
                    If Val(.Item(ConstAccountNo, i).Value) = 0 Then
                        MsgBox("Invalid Account!", MsgBoxStyle.Exclamation)
                        .CurrentCell = .Item(ConstDescr, i)
                        .BeginEdit(True)
                        Return False
                    End If
                    If Val(.Item(ConstPAmount, i).Value) = 0 Then
                        MsgBox("Invalid Amount!", MsgBoxStyle.Exclamation)
                        .CurrentCell = .Item(ConstPAmount, i)
                        .BeginEdit(True)
                        Return False
                    End If
                    grpset = .Item(ConstGrpSetOn, i).Value
                    If grpset = "P.D.C.(I)" Then
                        If .Item(ConstDtype, i).Value <> "Cr" Then
                            MsgBox("P.D.C Issued can not be Debited !", MsgBoxStyle.Exclamation)
                            .CurrentCell = .Item(ConstDtype, i)
                            .BeginEdit(True)
                            Return False
                        End If
                        If .Item(Constchq, i).Value = "" Then
                            MsgBox("Cheque Number Missing !", MsgBoxStyle.Exclamation)
                            .CurrentCell = .Item(Constchq, i)
                            .BeginEdit(True)
                            Return False
                        End If
                        If .Item(ConstBank, i).Value = "" Then
                            MsgBox("Bank Code Missing !", MsgBoxStyle.Exclamation)
                            .CurrentCell = .Item(ConstBank, i)
                            .BeginEdit(True)
                            Return False
                        End If
                        SqlQuery = tocheckDuplicatePDCEntryStr(Val(grdpayment.Item(ConstPdcCustAcno, i).Value & ""), MkDbSrchStr(Trim(grdpayment.Item(Constchq, i).Value & "")), MkDbSrchStr(Trim(grdpayment.Item(ConstBank, i).Value & "")), "D")
                        _vAcctrdatatable = _objcmnbLayer._fldDatatable(SqlQuery)
                        If _vAcctrdatatable.Rows.Count > 0 Then
                            MsgBox("Duplicate PDC entry details found !", MsgBoxStyle.Exclamation)
                            grdpayment.Select()
                            grdpayment.CurrentCell = grdpayment.Item(Constchq, i)
                            Return False
                        End If
                    End If
                    If grpset = "P.D.C.(I)" Then
                        Dim r As Integer
                        For r = i + 1 To grdpayment.RowCount - 1
                            If grdpayment.Item(ConstPdcCustAcno, r).Value = grdpayment.Item(ConstPdcCustAcno, i).Value And UCase(Trim(grdpayment.Item(Constchq, r).Value)) = UCase(Trim(grdpayment.Item(Constchq, i).Value)) And UCase(grdpayment.Item(ConstBank, r).Value) = UCase(grdpayment.Item(ConstBank, i).Value) Then
                                MsgBox("Duplicate PDC entry details found in the same voucher !", MsgBoxStyle.Exclamation)
                                grdpayment.Select()
                                grdpayment.CurrentCell = grdpayment.Item(Constchq, r)
                                Return False
                            End If
                        Next
                    End If
                Next
            End If
        End With
        Return True
    End Function
    Private Sub makeClear(Optional ByVal skipcmbselectChange As Boolean = False)
        lnumformat = numFormat
        chgbyprg = True
        txtjobcode.Text = ""
        lbljobname.Text = ""
        txtchequeno.Text = ""
        txtchequeno.Tag = ""
        txtBank.Text = ""
        loadedTrId = 0
        txtBank.Tag = ""
        cmbfc.Text = ""
        cmbdeliveredBy.Text = ""
        txtfcrt.Text = Format(1, lnumformat)
        dtpchequedate.Value = DateValue(Date.Now)
        dtpdate.Value = DateValue(Date.Now)
        chgbyprg = True
        grdpayment.Rows.Clear()
        grdVoucher.Rows.Clear()
        chgbyprg = False
        dtpdate.Focus()
        assaignTotal()
        'If Not skipcmbselectChange Then
        '    cmbVoucherTp.SelectedIndex = 0
        '    'cmbVoucherTp_SelectedIndexChanged(cmbVoucherTp, New System.EventArgs)
        'End If
        If Not skipcmbselectChange Then cmbVoucherTp_SelectedIndexChanged(cmbVoucherTp, New System.EventArgs)
        lblpayroll.Text = ""
        lblpayroll.Tag = ""
        lblpayroll.Visible = False
    End Sub
    Private Function saveTrans() As Boolean
        saveTrans = True
        If Not isModi Then
chkagain:
            If Not CheckNoExists(txtPreFix.Text, Val(numVchrNo.Text), "PV", "Accounts", numVchrNo) Then
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
        'LinkNo = Val(_objTr.SaveAccTrCmn())
        If dtAccTb.Rows.Count > 0 Then dtAccTb.Rows.Clear()
        '_objcmnbLayer._saveDatawithOutParm("Update AccTrDet set setremove=1 where LinkNo=" & LinkNo)
        'If Val(lblpayroll.Tag) > 0 Then
        '    _objcmnbLayer._saveDatawithOutParm("Update AccTrCmn set payrollpaymentid=" & Val(lblpayroll.Tag) & " where LinkNo=" & LinkNo)
        'Else
        '    If cmbdeliveredBy.Text <> "" Then
        '        _objcmnbLayer._saveDatawithOutParm("Update AccTrCmn set collectedOrDeliveredBy='" & cmbdeliveredBy.Text & "' where LinkNo=" & LinkNo)
        '    End If
        'End If
        Dim i As Integer
        Dim paymentaccount As Integer
        paymentaccount = Val(grdpayment.Item(ConstAccountNo, 0).Value)
        'Debit Entry
        With grdVoucher
            For i = 0 To .RowCount - 1
                If Val(.Item(ConstPAmount, i).Value & "") = 0 Then GoTo nxt
                If Val(txtfcrt.Text) = 0 Then txtfcrt.Text = 1
                .Item(ConstFCRate, i).Value = CDbl(txtfcrt.Text)
                .Item(ConstFCName, i).Value = cmbfc.Text
                setAcctrDetValue(LinkNo, i, grdVoucher, 0, paymentaccount)
                '_objTr.saveAccTrans()
nxt:
            Next
        End With
        'Credit Entry
        With grdpayment
            For i = 0 To .RowCount - 1
                If Val(.Item(ConstPAmount, i).Value & "") = 0 Then GoTo nxt1
                If Val(txtfcrt.Text) = 0 Then txtfcrt.Text = 1
                .Item(ConstFCRate, i).Value = CDbl(txtfcrt.Text)
                .Item(ConstFCName, i).Value = cmbfc.Text
                setAcctrDetValue(LinkNo, i, grdpayment, 1)
                '_objTr.saveAccTrans()
nxt1:
            Next
        End With
        If CDbl(lblvat.Text) > 0 Then
            Dim qry As String
            Dim cAcc As Integer
            If UsrBr = "" Then
                qry = "SELECT accid FROM AccMast WHERE AccSetId Like '%25%'"
            Else
                qry = " SELECT accid FROM BranchAccSet WHERE branchcode='" & UsrBr & "' and setno=" & 25
            End If
            dtTable = _objcmnbLayer._fldDatatable(qry)
            If dtTable.Rows.Count > 0 Then
                cAcc = dtTable(0)("accid")
            End If
            If cAcc > 0 Then
                setAcctrDetValue(LinkNo, cAcc, "ON/AC", "Input vat on Payment " & txtPreFix.Text & IIf(txtPreFix.Text = "", "", "/") & numVchrNo.Text, _
                                 CDbl(lblvat.Text), "", DateValue(Date.Now), 0, 0, 1)
            End If

        End If
        _objTr.SaveAccTrWithDt(dtAccTb)
        '_objcmnbLayer._saveDatawithOutParm("Delete from AccTrDet  where setremove=1 and LinkNo=" & LinkNo)
        numPrintVchr.Text = numVchrNo.Text
        txtprintprefix.Text = txtPreFix.Text
        
        If Not isModi Then
            'SetNextVrNo(numVchrNo, Val(cmbVoucherTp.Tag), "PV", "JvType = 'PV' AND JvNum = ", True, True, True)
        Else
            btnModify_Click(btnModify, New System.EventArgs)
        End If

        If Not fwait Is Nothing Then fwait.Close() : fwait = Nothing
        MsgBox("Payment Voucher # " & numPrintVchr.Text & " Saved Successfully", MsgBoxStyle.Information)
        If Val(lblpayroll.Tag) > 0 Then Me.Close() : Exit Function
        makeClear()
    End Function
    Private Sub setAcctrCmnValue()
        _objTr.JVType = "PV"
        _objTr.JVDate = DateValue(dtpdate.Value)
        _objTr.PreFix = txtPreFix.Text
        _objTr.JVNum = Val(numVchrNo.Text)
        _objTr.JVTypeNo = getVouchernumber("PVO")
        _objTr.UserId = CurrentUser
        _objTr.MchName = MACHINENAME
        _objTr.CrtDtTm = DateValue(Date.Now)
        _objTr.TypeNo = cmbVoucherTp.Tag
        _objTr.VrDescr = ""
        _objTr.IsModi = IIf(loadedTrId > 0, 2, 0)
        _objTr.LinkNo = loadedTrId
        _objTr.isLinkNo = True
        _objTr.payrollpaymentid = Val(lblpayroll.Tag)
        _objTr.collectedOrDeliveredBy = cmbdeliveredBy.Text
        _objTr.taxablevalue = CDbl(lbltaxable.Text)
        _objTr.taxvalue = CDbl(lblvat.Text)
    End Sub
    Private Sub setAcctrDetValue(ByVal lnkNo As Long, ByVal _row As Integer, ByVal grd As DataGridView, ByVal trtype As Integer, Optional ByVal CustAcc As Integer = 0)
        Dim dtrow As DataRow
        dtrow = dtAccTb.NewRow
        dtrow("LinkNo") = lnkNo
        dtrow("AccountNo") = Val(grd.Item(ConstAccountNo, _row).Value & "")
        dtrow("Reference") = Trim(grd.Item(ConstReference, _row).Value & "")
        If dtrow("Reference") = "" Then dtrow("Reference") = "ON/AC"
        dtrow("EntryRef") = Trim(grd.Item(ConstDescr, _row).Value & "")
        If IsDBNull(grd.Item(ConstFCAmount, _row).Value) Then
            dtrow("FCAmt") = dtrow("DealAmt")
        Else
            dtrow("FCAmt") = CDbl(grd.Item(ConstFCAmount, _row).Value) * IIf(grd.Item(ConstDtype, _row).Value = "Dr", 1, -1)
        End If
        dtrow("JobCode") = txtjobcode.Text
        If IsDBNull(grd.Item(ConstFCRate, _row).Value) Then
            grd.Item(ConstFCRate, _row).Value = 1
        End If
        dtrow("CurrRate") = CDbl(grd.Item(ConstFCRate, _row).Value)
        dtrow("CurrencyCode") = Trim(grd.Item(ConstFCName, _row).Value & "")
        dtrow("DealAmt") = CDbl(grd.Item(ConstPAmount, _row).Value) * IIf(grd.Item(ConstDtype, _row).Value = "Dr", 1, -1)
        dtrow("DealAmt") = dtrow("DealAmt") * dtrow("CurrRate")

        dtrow("TrInf") = 0
        dtrow("CustAcc") = 0
        dtrow("UnqNo") = Val(grd.Item(ConstUnq, _row).Value)
        dtrow("BankCode") = grd.Item(ConstBank, _row).Value
        dtrow("ChqNo") = grd.Item(Constchq, _row).Value
        'set vatid as vat percentage
        If IsDBNull(grd.Item(Constvatper, _row).Value) Then
            dtrow("vatid") = dtrow("vatid")
        Else
            dtrow("vatid") = CDbl(grd.Item(Constvatper, _row).Value)
        End If
        
        If Not IsDBNull(grd.Item(ConstChqdate, _row).Value) Then
            If chkDate(grd.Item(ConstChqdate, _row).Value) Then
                dtrow("ChqDate") = DateValue(grd.Item(ConstChqdate, _row).Value)
            Else
                dtrow("ChqDate") = DateValue("01/01/1950")
            End If
        End If
        dtrow("PDCAcc") = 0
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
    Private Sub setAcctrDetValue(ByVal lnkNo As Long, ByVal AccountNo As Long, ByVal reference As String, _
                             ByVal EntryRef As String, ByVal Amount As Double, ByVal ChqNo As String, _
                             ByVal ChqDate As Date, ByVal AccsetId As Integer, ByVal setoffCount As Integer, ByVal isvatEntry As Integer)
        Dim FCRT As Double
        FCRT = CDbl(txtfcrt.Text)
        If FCRT = 0 Then FCRT = 1

        Dim dtrow As DataRow
        dtrow = dtAccTb.NewRow
        dtrow("LinkNo") = lnkNo
        dtrow("AccountNo") = AccountNo
        dtrow("Reference") = Trim(reference & "")
        dtrow("EntryRef") = Trim(EntryRef & "")
        dtrow("DealAmt") = Amount * FCRT
        dtrow("FCAmt") = Amount
        dtrow("CurrencyCode") = cmbfc.Text
        dtrow("CurrRate") = FCRT
        dtrow("TrInf") = 0
        dtrow("PDCAcc") = 0
        dtrow("ChqDate") = ChqDate
        dtrow("ChqNo") = ChqNo
        dtrow("BankCode") = ""
        dtrow("UnqNo") = 0
        dtrow("setoffCount") = setoffCount
        dtrow("isvatEntry") = isvatEntry
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
    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        If TabControl1.SelectedIndex = 0 Then
            If chkFormat.Checked Then
                fRptFormat = New RptFormatfrm
                fRptFormat.RptType = "PV"
                fRptFormat.ShowDialog()
                fRptFormat = Nothing
            Else
                PrepareRpt("PV")
            End If
        Else
            fRptFormat = New RptFormatfrm
            fRptFormat.RptType = "PVL"
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
            _objTr.PreFix = txtprintprefix.Text
            _objTr.JVNum = Val(numPrintVchr.Text)
            _objTr.JVType = "PV"
            _objTr.TypeNo = 2
            ds = _objTr.ldInvoice("rturnAccountVoucherForPrint")
        Else
            If RptdtTable Is Nothing Then
                With _objTr
                    .ptype = 5
                    .DateFrom = DateValue(dtpstart.Value)
                    .DateTo = DateValue(dtpto.Value)
                    ds = .returnPaymentDetails()
                End With
            Else
                ds.Tables.Add(RptdtTable)
            End If
        End If
        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = Me.MdiParent
        frm.Text = RptCaption
        frm.Show()
    End Sub

    Private Sub btnModify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModify.Click
        If btnModify.Text = "&Clear" Then
            makeClear(True)
            cmbVoucherTp_SelectedIndexChanged(cmbVoucherTp, New EventArgs)
            numVchrNo.Focus()
        Else
            If isModi Then
                TabControl1.SelectedIndex = 1
                'btnpayment.Enabled = False
                btnModify.Enabled = False
                btnupdate.Enabled = False
                btndelete.Enabled = False
            End If
            makeClear(True)
            cmbVoucherTp_SelectedIndexChanged(cmbVoucherTp, New EventArgs)
            btnModify.Text = "&Clear"
            isModi = False
        End If
    End Sub

    Private Sub dtpdate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtpdate.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub



    Private Sub dtpdate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpdate.ValueChanged
        If chgbyprg Then Exit Sub
        chgPost = True
        btnupdate.Enabled = True
    End Sub

    Private Sub dtpchequedate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If chgbyprg Then Exit Sub
        chgPost = True
    End Sub
    Private Sub ldGrid()
        With _objTr
            .ptype = 8
            .DateFrom = DateValue(dtpstart.Value)
            .DateTo = DateValue(dtpto.Value)
            .JVType = "PVO"
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


            .Columns(ConstInvNo).HeaderText = "PV No"
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

            .Columns(ConstCustId).HeaderText = "Supp.Code"
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
            .Columns("ChqDate").Width = 90 '100
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

    Private Sub txtSeq_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeq.TextChanged
        dvData.DataSource = SearchGrid(dtTable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
        RptdtTable = SearchGrid(dtTable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
    End Sub

    Private Sub btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnload.Click
        loadWaite(1)
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        If Val(btndelete.Tag) = 0 Then
            MsgBox("This user do not have permission to Remove", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If loadedTrId = 0 Or isModi = False Then
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
        _objcmnbLayer._saveDatawithOutParm("DELETE FROM AccTrCmn WHERE LinkNo=" & loadedTrId)
        _objcmnbLayer._saveDatawithOutParm("DELETE FROM AccTrDet WHERE LinkNo=" & loadedTrId)
        btnModify_Click(btnModify, New System.EventArgs)
    End Sub

    Private Sub TabControl1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.Click
        If btnModify.Text = "Undo" And TabControl1.SelectedIndex = 1 Then
            MsgBox("Do Undo/Update before moving to Enquiry", MsgBoxStyle.Exclamation)
            TabControl1.SelectedIndex = 0
            Exit Sub
        End If
        If TabControl1.SelectedIndex = 1 And dvData.RowCount = 0 Then
            loadWaite(1)
        End If
        If TabControl1.SelectedIndex = 1 Then
            btnModify.Enabled = False
            btndelete.Enabled = False
            btnupdate.Enabled = False
            'btnpayment.Enabled = False
        Else
            btnModify.Enabled = True
            If Not isModi Then
                cmbVoucherTp_SelectedIndexChanged(cmbVoucherTp, New EventArgs)
            End If
            'btnpayment.Enabled = True
            'btnupdate.Enabled = True
        End If
    End Sub

    
    Private Sub Textbox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            Dim col As Integer
            col = grdVoucher.CurrentCell.ColumnIndex
            If col = ConstPAmount Then
                If Not IsNumeric(e.KeyChar) And e.KeyChar <> "." And Not e.KeyChar = Convert.ToChar(Keys.Back) Then e.Handled = True
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
            ElseIf col = ConstPAmount Then
                With grdVoucher
                    If .Rows.Count > 0 Then
                        If Not IsDBNull(.Item(ConstFCRate, .CurrentRow.Index).Value) And MyCtrl.Text <> "" Then
                            ' .Item(ConstPAmount, .CurrentRow.Index).Value = MyCtrl.Text 'CDbl(MyCtrl.Text) / IIf(CDbl(.Item(ConstFCRate, .CurrentRow.Index).Value) > 0, CDbl(.Item(ConstFCRate, .CurrentRow.Index).Value), 1)
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
                            .Item(ConstPAmount, .CurrentRow.Index).Value = CDbl(MyCtrl.Text) * IIf(CDbl(.Item(ConstFCRate, .CurrentRow.Index).Value) > 0, CDbl(.Item(ConstFCRate, .CurrentRow.Index).Value), 1)
                        Else
                            If MyCtrl.Text <> "" Then .Item(ConstPAmount, .CurrentRow.Index).Value = CDbl(MyCtrl.Text)
                        End If
                    End If
                End With
            ElseIf col = ConstFCRate Then
                With grdVoucher
                    If .Rows.Count > 0 Then
                        If Not IsDBNull(.Item(ConstFCAmount, .CurrentRow.Index).Value) And MyCtrl.Text <> "" And MyCtrl.Text <> "." Then
                            .Item(ConstPAmount, .CurrentRow.Index).Value = CDbl(.Item(ConstFCAmount, .CurrentRow.Index).Value) * IIf(CDbl(MyCtrl.Text) > 0, CDbl(MyCtrl.Text), 1)
                        End If
                    End If
                End With
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


    Private Sub txtBank_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBank.Validated
        If Trim(txtchequeno.Tag) = "Bank" Then
            If Val(txtBank.Tag) = 0 Then
                MsgBox("Invalid Bank", MsgBoxStyle.Exclamation)
                txtBank.Focus()
            Else
                txtCr.Text = txtBank.Text
                txtCr.Tag = txtBank.Tag
            End If
        End If
    End Sub
    Public Sub editRecord(Optional ByVal linknoFrm As Long = 0)
        If dvData.RowCount = 0 And linknoFrm = 0 Then Exit Sub
        makeClear(True)
        numVchrNo.Text = ""
        If linknoFrm = 0 Then
            loadedTrId = dvData.Item(ConstLinkNo, dvData.CurrentRow.Index).Value
        Else
            loadedTrId = linknoFrm
        End If
        ldRec()
        btnModify.Text = "Undo"
        isModi = True
        dtpdate.Focus()
        assaignTotal()
        If Val(btnupdate.Tag) = 0 Then
            MsgBox("This user do not have permission to modify", MsgBoxStyle.Exclamation)
        End If
        TabControl1.SelectedIndex = 0
    End Sub
    
    Private Sub ldRec()
        Dim qry As String
        Dim dtAccTrCmn As DataTable
        Dim drqry As String
        drqry = getAccTrDetbyloadedTrId(loadedTrId, " AND DealAmt<0 and isnull(isvatEntry,0)=0", True)
        drqry = "SELECT *,CHQ.dbcbnos trrfd FROM (" & drqry & ")TR left join (select LinkNo dbcbnos,JVNum dbcbJvno,JVDate trfrDate  " & _
                                                  "from AccTrCmn) CHQ ON TR.DBCBNo=CHQ.dbcbnos "
        qry = "SELECT * FROM AccTrCmn WHERE LinkNo=" & loadedTrId
        qry = qry & " " & getAccTrDetbyloadedTrId(loadedTrId, " AND DealAmt>0 and isnull(isvatEntry,0)=0")
        qry = qry & " " & drqry
        Dim dtset As DataSet = _objcmnbLayer._ldDataset(qry, False)
        dtAccTrCmn = dtset.Tables(0)
        If dtAccTrCmn.Rows.Count > 0 Then
            loadedTrId = dtAccTrCmn(0)("LinkNo")
            dtpdate.Value = DateValue(dtAccTrCmn(0)("JVDate"))
            chgbyprg = True
            numVchrNo.Text = dtAccTrCmn(0)("JVnum")
            numPrintVchr.Text = dtAccTrCmn(0)("JVnum")
            txtPreFix.Text = dtAccTrCmn(0)("Prefix")
            txtprintprefix.Text = dtAccTrCmn(0)("Prefix")
            cmbdeliveredBy.Text = Trim(dtAccTrCmn(0)("collectedOrDeliveredBy") & "")

            btndelete.Enabled = True
            btnupdate.Enabled = True
            btnModify.Enabled = True
            loadDebit(dtset.Tables(2))
            loadCredit(dtset.Tables(1))

            activecontrolname = ""
           
            'btnpayment.Enabled = True
        Else
            MsgBox("Payment Voucher Not Found", MsgBoxStyle.Exclamation)
        End If
        chgbyprg = False

    End Sub
    Private Sub loadCredit(ByVal dtAccTr As DataTable)
        Dim i As Integer
        Dim r As Integer
        chgbyprg = True
        grdVoucher.Rows.Clear()
        For i = 0 To dtAccTr.Rows.Count - 1
            With grdVoucher
                .Rows.Add()
                r = .Rows.Count - 1
                
                .Item(ConstAlias, r).Value = dtAccTr(i)("Alias")
                .Item(ConstName, r).Value = dtAccTr(i)("AccDescr")
                .Item(ConstReference, r).Value = dtAccTr(i)("Reference")
                .Item(ConstDescr, r).Value = dtAccTr(i)("EntryRef")
                cmbfc.Text = dtAccTr(i)("CurrencyCode")
                txtfcrt.Text = Format(dtAccTr(i)("CurrRate"), lnumformat)

                If CDbl(dtAccTr(i)("DealAmt")) > 0 Then
                    .Item(ConstDtype, r).Value = "Dr"
                    .Item(ConstPAmount, r).Value = Format(CDbl(dtAccTr(i)("DealAmt")) / CDbl(dtAccTr(i)("CurrRate")), lnumformat)
                Else
                    .Item(ConstDtype, r).Value = "Cr"
                    .Item(ConstPAmount, r).Value = Format((CDbl(dtAccTr(i)("DealAmt")) / CDbl(dtAccTr(i)("CurrRate"))) * -1, lnumformat)
                End If
                .Item(ConstFCName, r).Value = dtAccTr(i)("CurrencyCode")
                .Item(ConstFCRate, r).Value = dtAccTr(i)("CurrRate")
                .Item(ConstFCAmount, r).Value = dtAccTr(i)("FCAmt")
                .Item(ConstFCDec, r).Value = 2

                .Item(Constchq, r).Value = dtAccTr(i)("ChqNo")
                If Trim(dtAccTr(i)("ChqNo") & "") <> "" And Not IsDBNull(dtAccTr(i)("ChqDate")) Then
                    .Item(ConstChqdate, r).Value = Format(DateValue(dtAccTr(i)("ChqDate")), DtFormat)
                End If
                .Item(ConstJob, r).Value = dtAccTr(i)("JobCode")
                .Item(ConstBank, r).Value = dtAccTr(i)("BankCode")
                .Item(ConstPdcCustAc, r).Value = dtAccTr(i)("pdcname")
                .Item(ConstPdcCustAcno, r).Value = dtAccTr(i)("pdcid")
                .Item(ConstUnq, r).Value = dtAccTr(i)("UnqNo")
                .Item(ConstGrpSetOn, r).Value = dtAccTr(i)("GrpSetOn")
                .Item(ConstAccountNo, r).Value = dtAccTr(i)("accountno")
                .Item(Constvatper, r).Value = Format(CDbl(dtAccTr(i)("vatid")), lnumformat)
                .Item(ConstvatAmt, r).Value = CDbl(.Item(ConstPAmount, r).Value) * Val(dtAccTr(i)("vatid")) / 100
            End With
        Next
        FindNextCell(grdVoucher, grdVoucher.CurrentCell.RowIndex, grdVoucher.CurrentCell.ColumnIndex + 1)

    End Sub
    Private Sub loadDebit(ByVal dtAccTr As DataTable)
        Dim i As Integer
        Dim r As Integer
        chgbyprg = True
        Dim chqStatus As Boolean
        grdpayment.Rows.Clear()
        Dim GrpSetOn As String
        For i = 0 To dtAccTr.Rows.Count - 1
            With grdpayment
                .Rows.Add()
                r = .Rows.Count - 1
                If Val(dtAccTr(i)("trrfd") & "") > 0 Then
                    MsgBox("PDC Already Transfered! you can not modify/delete", MsgBoxStyle.Exclamation)
                    chqStatus = True
                End If
                .Item(ConstAlias, r).Value = dtAccTr(i)("Alias")
                .Item(ConstName, r).Value = dtAccTr(i)("AccDescr")
                .Item(ConstReference, r).Value = dtAccTr(i)("Reference")
                .Item(ConstDescr, r).Value = dtAccTr(i)("EntryRef")
                cmbfc.Text = dtAccTr(i)("CurrencyCode")
                txtfcrt.Text = Format(dtAccTr(i)("CurrRate"), lnumformat)

                If CDbl(dtAccTr(i)("DealAmt")) > 0 Then
                    .Item(ConstDtype, r).Value = "Dr"
                    .Item(ConstPAmount, r).Value = Format(CDbl(dtAccTr(i)("DealAmt")) / CDbl(dtAccTr(i)("CurrRate")), lnumformat)
                Else
                    .Item(ConstDtype, r).Value = "Cr"
                    .Item(ConstPAmount, r).Value = Format((CDbl(dtAccTr(i)("DealAmt")) / CDbl(dtAccTr(i)("CurrRate"))) * -1, lnumformat)
                End If
                .Item(ConstFCName, r).Value = dtAccTr(i)("CurrencyCode")
                .Item(ConstFCRate, r).Value = dtAccTr(i)("CurrRate")
                .Item(ConstFCAmount, r).Value = dtAccTr(i)("FCAmt")
                .Item(ConstFCDec, r).Value = 2

                .Item(Constchq, r).Value = dtAccTr(i)("ChqNo")
                If Trim(dtAccTr(i)("ChqNo") & "") <> "" And Not IsDBNull(dtAccTr(i)("ChqDate")) Then
                    .Item(ConstChqdate, r).Value = Format(DateValue(dtAccTr(i)("ChqDate")), DtFormat)
                End If
                .Item(ConstJob, r).Value = dtAccTr(i)("JobCode")
                .Item(ConstBank, r).Value = dtAccTr(i)("BankCode")
                .Item(ConstPdcCustAc, r).Value = dtAccTr(i)("pdcname")
                .Item(ConstPdcCustAcno, r).Value = dtAccTr(i)("pdcid")
                .Item(ConstUnq, r).Value = dtAccTr(i)("UnqNo")
                .Item(ConstGrpSetOn, r).Value = dtAccTr(i)("GrpSetOn")
                .Item(ConstAccountNo, r).Value = dtAccTr(i)("accountno")

                GrpSetOn = Trim(.Item(ConstGrpSetOn, r).Value & "")
                If UCase(GrpSetOn) = "BANK" Or UCase(GrpSetOn) = "P.D.C.(I)" Or UCase(GrpSetOn) = "P.D.C.(R)" Then
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
        FindNextCell(grdpayment, grdpayment.CurrentCell.RowIndex, grdpayment.CurrentCell.ColumnIndex + 1)
        activecontrolname = ""
        If chqStatus Then
            btndelete.Enabled = False
            btnupdate.Enabled = False
        Else
            btndelete.Enabled = True
            btnupdate.Enabled = True
        End If
        chgbyprg = False
    End Sub

    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        LoadReport(RptFlName, RptCaption, forPrint)
    End Sub
    
    Sub SetGridHeadPayment()
        With grdpayment
            SetEntryGridProperty(grdpayment)
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
            .Columns(ConstDtype).Width = 40
            .Columns(ConstDtype).ReadOnly = True

            .Columns(ConstPAmount).HeaderText = "Amount"
            .Columns(ConstPAmount).Width = 100
            .Columns(ConstPAmount).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstPAmount).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstPAmount).DefaultCellStyle.Format = "N" & NoOfDecimal


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

            Dim cmb As New DataGridViewComboBoxColumn
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
            .Columns(ConstPdcCustAc).Visible = False

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
            .Columns(ConstvatAmt).Visible = False
            .Columns(Constvatper).Visible = False
        End With

    End Sub
    Sub SetGridHeadExpense()
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
            .Columns(ConstDtype).Width = 40
            .Columns(ConstDtype).ReadOnly = True

            .Columns(ConstPAmount).HeaderText = "Amount"
            .Columns(ConstPAmount).Width = 100
            .Columns(ConstPAmount).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstPAmount).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstPAmount).DefaultCellStyle.Format = "N" & NoOfDecimal


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
            .Columns(ConstJob).Visible = True

            .Columns(Constvatper).HeaderText = "Vat"
            .Columns(Constvatper).Width = 75
            .Columns(Constvatper).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(Constvatper).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(Constvatper).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(Constvatper).Visible = enableGCC

            .Columns(ConstvatAmt).HeaderText = "Vat Amt"
            .Columns(ConstvatAmt).Width = 100
            .Columns(ConstvatAmt).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstvatAmt).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstvatAmt).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstvatAmt).Visible = enableGCC
            .Columns(ConstvatAmt).ReadOnly = True

            .Columns(Constchq).HeaderText = "Cheq#"
            .Columns(Constchq).Width = 100
            .Columns(Constchq).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(Constchq).Visible = False

            Dim col As New CalendarColumn()
            .Columns.RemoveAt(ConstChqdate)
            col.DataPropertyName = "ChqDate"
            .Columns.Insert(ConstChqdate, col)
            .Columns(ConstChqdate).HeaderText = "Cheq Date"
            '.Columns(ConstChqdate).Visible = False

            Dim cmb As New DataGridViewComboBoxColumn
            cmb = New DataGridViewComboBoxColumn
            cmb.Items.Add("")
            'Dim dtbankDatatable As DataTable
            'dtbankDatatable = _objcmnbLayer._fldDatatable("Select Bankcode from BankTb")
            'For i = 0 To dtbankDatatable.Rows.Count - 1
            '    cmb.Items.Add(dtbankDatatable(i)(0))
            'Next
            cmb.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
            cmb.HeaderText = "Bank"
            cmb.DataPropertyName = "BankCode"
            .Columns.RemoveAt(ConstBank)
            .Columns.Insert(ConstBank, cmb)
            '.Columns(ConstBank).Visible = False

            .Columns(ConstPdcCustAc).HeaderText = "PDC CustId"
            .Columns(ConstPdcCustAc).Width = 150
            .Columns(ConstPdcCustAc).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstPdcCustAc).Visible = False

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

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        'setColwidth()
        resizeGridColumn(grdVoucher, ConstDescr)
        resizeGridColumn(grdpayment, ConstDescr)
    End Sub
    Private Sub setColwidth()
        If grdpayment.Columns.Count = 0 Then Exit Sub
        Dim colwidth As Integer
        Dim i As Integer
        For i = ConstDtype To ConstGrpSetOn
            If grdpayment.Columns(i).Visible = True Then
                colwidth = colwidth + grdpayment.Columns(i).Width
            End If
        Next
        colwidth = colwidth + grdpayment.Columns(ConstAlias).Width + grdpayment.Columns(ConstName).Width
        grdpayment.Columns(ConstDescr).Width = grdpayment.Width - colwidth - 130
    End Sub

    Private Sub ExpensePayments_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
    End Sub
    Private Sub loadMasters()
        Dim qry As String
        Dim prefixacc As String = " select Alias,AccDescr,GrpSetOn,accid from (select ANo,VrTypeNo from PreFixTb group by VrTypeNo,ANo)PreFixTb inner join (SELECT Alias,AccDescr,GrpSetOn,accid FROM AccMast " & _
                                                "LEFT JOIN S1AccHd ON AccMast.S1AccId=S1AccHd.S1AccId) acc1 on PreFixTb.ANo=acc1.accid WHERE VrTypeNo = 1"

        prefixacc = prefixacc & " Union all select Alias,AccDescr,GrpSetOn,accid from (select ANo2,VrTypeNo from PreFixTb group by VrTypeNo,ANo2)PreFixTb inner join (SELECT Alias,AccDescr,GrpSetOn,accid FROM AccMast " & _
                                                "LEFT JOIN S1AccHd ON AccMast.S1AccId=S1AccHd.S1AccId) acc2 on PreFixTb.ANo2=acc2.accid WHERE VrTypeNo = 1"

        Dim userInvnos As String = IIf(UsrBr = "", " SELECT * FROM InvNos WHERE InvType='PV'", " SELECT * FROM InvNosBrTb WHERE Brcode='" & UsrBr & "' AND InvType='PV'")


        qry = "SELECT SManCode FROM SalesmanTb"
        qry = qry & " SELECT CurrencyCode FROM CurrencyTb"
        qry = qry & "  SELECT * FROM PreFixTb WHERE VrTypeNo = 1" & IIf(UsrBr = "", "", " AND BrId In ('', '" & UsrBr & "')") & " Order by ordNo"
        qry = qry & prefixacc & userInvnos

        Dim dtset As DataSet
        dtset = _objcmnbLayer._ldDataset(qry, False)
        AddDttoCombo(cmbdeliveredBy, dtset.Tables(0), True)
        LodCurrency(dtset.Tables(1))
        PreFixTb = dtset.Tables(2)
        dtAcc = dtset.Tables(3)
        dtInvNos = dtset.Tables(4)
    End Sub
    Private Sub ExpensePayments_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetGridHeadPayment()
        SetGridHeadExpense()
        lnumformat = numFormat
        loadMasters()
        chgbyprg = True
        crtSubVrs(cmbVoucherTp, 1, True)
        CreateSetoffTable(dtSetoffTable)
        chgbyprg = False
        NDec = NoOfDecimal
        If Not userType Then
            btndelete.Tag = 1
            btnupdate.Tag = 1
        Else
            btndelete.Tag = IIf(getRight(68, CurrentUser), 1, 0)
            btnupdate.Tag = IIf(getRight(67, CurrentUser), 1, 0)
        End If
        Timer1.Enabled = True
        If enableGCC = False Then
            lblvat.Visible = False
            Label17.Visible = False

        End If
        If editlinkno > 0 Then
            Timer2.Enabled = True
        End If
    End Sub


    Private Sub grdpayment_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdpayment.CellClick
        BeginEdit()
    End Sub
    Public Sub BeginEdit()
        chgbyprg = True
        With grdpayment
            If .RowCount = 0 Then Exit Sub
            activecontrolname = "grdpayment"
            .BeginEdit(True)

        End With
        chgbyprg = False
    End Sub

    Private Sub grdpayment_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdpayment.CellEndEdit
        Dim col As Integer = e.ColumnIndex
        Dim fmt As String
        'Dim ndcf As Integer
        fmt = "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))
        If col = ConstPAmount Then
            grdpayment.Item(col, e.RowIndex).Value = Format(Convert.ToDecimal(grdpayment.Item(col, e.RowIndex).Value), fmt)
        End If
    End Sub

    Private Sub grdpayment_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdpayment.CellValidated
        If chgbyprg Then Exit Sub
        Valid(e.RowIndex, e.ColumnIndex, grdpayment)
        SrchText = ""
    End Sub
    Private Sub Valid(ByVal RowIndex As Integer, ByVal ColIndex As Integer, ByVal grd As DataGridView)
        If chgbyprg Then Exit Sub
        Dim chkDatatable As DataTable
        chgbyprg = True
        With grd
            Select Case ColIndex
                Case ConstAlias, ConstName
                    If SrchText = "" And Not IsDBNull(grd.Item(ConstAlias, grd.CurrentCell.RowIndex).Value) Then
                        SrchText = grd.Item(ConstAlias, grd.CurrentCell.RowIndex).Value
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
                        SrchText = grd.Item(ConstFCName, .CurrentCell.RowIndex).Value
                    End If
                    chkDatatable = EntriesValidation(SrchText, 2)
                    If chkDatatable.Rows.Count > 0 Then
                        .Item(ConstFCName, .CurrentCell.RowIndex).Value = SrchText
                    Else
                        .Item(ConstFCName, .CurrentCell.RowIndex).Value = ""
                        .Item(ConstFCRate, .CurrentCell.RowIndex).Value = 1
                        .Item(ConstFCDec, .CurrentCell.RowIndex).Value = 2
                        If Not IsDBNull(.Item(ConstFCAmount, .CurrentRow.Index).Value) And Not IsDBNull(.Item(ConstFCRate, .CurrentRow.Index).Value) Then
                            .Item(ConstPAmount, .CurrentRow.Index).Value = CDbl(.Item(ConstFCAmount, .CurrentRow.Index).Value) * IIf(CDbl(.Item(ConstFCRate, .CurrentCell.RowIndex).Value) > 0, CDbl(.Item(ConstFCRate, .CurrentCell.RowIndex).Value), 1)
                        End If
                    End If
                Case ConstJob
                    If SrchText = "" And Not IsDBNull(.Item(ConstJob, grd.CurrentCell.RowIndex).Value) Then
                        SrchText = grd.Item(ConstJob, .CurrentCell.RowIndex).Value
                    End If
                    chkDatatable = EntriesValidation(SrchText, 5)
                    If chkDatatable.Rows.Count > 0 Then
                        .Item(ConstJob, .CurrentCell.RowIndex).Value = SrchText
                    Else
                        .Item(ConstJob, .CurrentCell.RowIndex).Value = ""
                    End If
                Case ConstPdcCustAc
                    If SrchText = "" And Not IsDBNull(.Item(ConstPdcCustAc, grd.CurrentCell.RowIndex).Value) Then
                        SrchText = grd.Item(ConstPdcCustAc, .CurrentCell.RowIndex).Value
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
                    '        .Item(ConstReference, .CurrentCell.RowIndex).Value = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(grd.Item(ConstReference, grd.CurrentCell.RowIndex).Value)
                    '    End If
                Case ConstPAmount, Constvatper
                    If Val(.Item(Constvatper, .CurrentCell.RowIndex).Value) > 0 And Val(.Item(ConstPAmount, .CurrentCell.RowIndex).Value) > 0 Then
                        If chkcal.Checked Then
                            Dim aprice As Double = CDbl(.Item(ConstPAmount, .CurrentCell.RowIndex).Value)
                            Dim vp As Double = CDbl(.Item(Constvatper, .CurrentCell.RowIndex).Value)
                            aprice = aprice - ((aprice * vp) / (100 + vp))
                            SrchText = aprice
                            .Item(ConstPAmount, .CurrentCell.RowIndex).Value = Format(aprice, numFormat)
                        End If
                        .Item(ConstvatAmt, .CurrentCell.RowIndex).Value = Format(CDbl(.Item(ConstPAmount, .CurrentCell.RowIndex).Value) * CDbl(.Item(Constvatper, .CurrentCell.RowIndex).Value) / 100, lnumformat)
                    End If
            End Select
            Select Case ColIndex
                Case ConstPAmount, ConstFCAmount, ConstFCName, ConstFCRate, ConstDtype, Constvatper
                    assaignTotal()
            End Select
        End With
        chgbyprg = False
    End Sub
    Public Sub assaignTotal()
        Try
            Dim i As Integer
            Dim ttlDebit As Double
            Dim ttlCredit As Double
            Dim currentAmt As Double
            Dim taxable As Double
            Dim taxamt As Double
            If SrchText = "" Then SrchText = 0
            With grdpayment
                For i = 0 To grdpayment.Rows.Count - 1
                    If activecontrolname = "grdpayment" Then
                        If i = .CurrentRow.Index Then
                            currentAmt = CDbl(SrchText)
                        Else
                            currentAmt = 0
                        End If
                    End If
                    If IsDBNull(.Item(ConstPAmount, i).Value) Then .Item(ConstPAmount, i).Value = 0
                    If .Item(ConstDtype, i).Value = "Dr" Then
                        ttlDebit = ttlDebit + IIf(CDbl(.Item(ConstPAmount, i).Value) <> currentAmt And currentAmt > 0, currentAmt, CDbl(.Item(ConstPAmount, i).Value))
                    Else
                        ttlCredit = ttlCredit + IIf(CDbl(.Item(ConstPAmount, i).Value) <> currentAmt And currentAmt > 0, currentAmt, CDbl(.Item(ConstPAmount, i).Value))
                    End If

                Next
            End With
            With grdVoucher
                For i = 0 To .Rows.Count - 1
                    currentAmt = 0
                    If activecontrolname = "grdVoucher" Then
                        If i = .CurrentRow.Index Then
                            currentAmt = CDbl(SrchText)
                        Else
                            currentAmt = 0
                        End If
                    End If
                    If IsDBNull(.Item(ConstPAmount, i).Value) Then .Item(ConstPAmount, i).Value = 0
                    If Val(.Item(Constvatper, i).Value) > 0 Then
                        taxable = taxable + +IIf(CDbl(.Item(ConstPAmount, i).Value) <> currentAmt And currentAmt > 0, currentAmt, CDbl(.Item(ConstPAmount, i).Value))
                        taxamt = taxamt + Val(.Item(ConstvatAmt, i).Value)
                    End If
                    If .Item(ConstDtype, i).Value = "Dr" Then
                        ttlDebit = ttlDebit + IIf(CDbl(.Item(ConstPAmount, i).Value) <> currentAmt And currentAmt > 0, currentAmt, CDbl(.Item(ConstPAmount, i).Value))
                    Else
                        ttlCredit = ttlCredit + IIf(CDbl(.Item(ConstPAmount, i).Value) <> currentAmt And currentAmt > 0, currentAmt, CDbl(.Item(ConstPAmount, i).Value))
                    End If

                Next
            End With

            SrchText = ""
            currentAmt = 0
            lblTlDebit.Text = Format(ttlDebit, lnumformat)
            lbltaxable.Text = Format(taxable, lnumformat)
            lblvat.Text = Format(taxamt, lnumformat)
            lblcredit.Text = Format(ttlCredit, lnumformat)
            lbldiff.Text = Format((CDbl(lblTlDebit.Text) + CDbl(lblvat.Text)) - CDbl(lblcredit.Text), lnumformat)
            chgbyprg = False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub grdpayment_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdpayment.EditingControlShowing
        Dim Col As Integer
        Col = grdpayment.CurrentCell.ColumnIndex
        If Col = ConstAlias Or Col = ConstFCName Or Col = ConstName Or Col = ConstJob Or Col = ConstPdcCustAc Or Col = ConstPAmount Or Col = ConstFCAmount Or Col = ConstFCRate Then
            Dim tb As TextBox = CType(e.Control, TextBox)
            RemoveHandler tb.TextChanged, AddressOf Textbox_TextChangedPayment
            AddHandler tb.TextChanged, AddressOf Textbox_TextChangedPayment
        End If
        If Col = ConstPAmount Or Col = ConstFCAmount Or Col = ConstFCRate Then
            Dim tb As TextBox = CType(e.Control, TextBox)
            RemoveHandler tb.KeyPress, AddressOf Textbox_KeyPressPayment
            AddHandler tb.KeyPress, AddressOf Textbox_KeyPressPayment
        End If
    End Sub
    Private Sub Textbox_KeyPressPayment(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            Dim col As Integer
            col = grdpayment.CurrentCell.ColumnIndex
            If col = ConstPAmount Or col = ConstFCAmount Or col = ConstFCRate Then
                If Not IsNumeric(e.KeyChar) And e.KeyChar <> "." And Not e.KeyChar = Convert.ToChar(Keys.Back) Then e.Handled = True
            ElseIf col = ConstPdcCustAc Then
                e.Handled = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub Textbox_TextChangedPayment(ByVal sender As Object, ByVal e As System.EventArgs)
        If chgbyprg Then Exit Sub 'sh
        Try
            Dim col As Integer
            col = grdpayment.CurrentCell.ColumnIndex
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
                ShowFmlist()
                chgbyprg = False
            ElseIf col = ConstName Then
                _srchTxtId = 2
                _srchIndexId = 0
                chgbyprg = True
                strGridSrchString = MyCtrl.Text
                ShowFmlist()
                chgbyprg = False
            ElseIf col = ConstFCName Then
                _srchTxtId = 3
                _srchIndexId = 0
                chgbyprg = True
                strGridSrchString = MyCtrl.Text
                ShowFmlist()
                chgbyprg = False
            ElseIf col = ConstJob Then
                _srchTxtId = 4
                _srchIndexId = 0
                chgbyprg = True
                strGridSrchString = MyCtrl.Text
                ShowFmlist()
                chgbyprg = False
            ElseIf col = ConstPAmount Then
                With grdpayment
                    If .Rows.Count > 0 Then
                        If Not IsDBNull(.Item(ConstFCRate, .CurrentRow.Index).Value) And MyCtrl.Text <> "" Then
                            ' .Item(ConstPAmount, .CurrentRow.Index).Value = MyCtrl.Text 'CDbl(MyCtrl.Text) / IIf(CDbl(.Item(ConstFCRate, .CurrentRow.Index).Value) > 0, CDbl(.Item(ConstFCRate, .CurrentRow.Index).Value), 1)
                            .Item(ConstFCAmount, .CurrentRow.Index).Value = CDbl(MyCtrl.Text) / IIf(CDbl(.Item(ConstFCRate, .CurrentRow.Index).Value) > 0, CDbl(.Item(ConstFCRate, .CurrentRow.Index).Value), 1)
                        Else
                            If MyCtrl.Text <> "" Then .Item(ConstFCAmount, .CurrentRow.Index).Value = CDbl(MyCtrl.Text)
                        End If
                    End If
                End With
                SrchText = MyCtrl.Text
            ElseIf col = ConstFCAmount Then
                With grdpayment
                    If .Rows.Count > 0 Then
                        If Not IsDBNull(.Item(ConstFCRate, .CurrentRow.Index).Value) And MyCtrl.Text <> "" Then
                            .Item(ConstPAmount, .CurrentRow.Index).Value = CDbl(MyCtrl.Text) * IIf(CDbl(.Item(ConstFCRate, .CurrentRow.Index).Value) > 0, CDbl(.Item(ConstFCRate, .CurrentRow.Index).Value), 1)
                        Else
                            If MyCtrl.Text <> "" Then .Item(ConstPAmount, .CurrentRow.Index).Value = CDbl(MyCtrl.Text)
                        End If
                    End If
                End With
            ElseIf col = ConstFCRate Then
                With grdpayment
                    If .Rows.Count > 0 Then
                        If Not IsDBNull(.Item(ConstFCAmount, .CurrentRow.Index).Value) And MyCtrl.Text <> "" And MyCtrl.Text <> "." Then
                            .Item(ConstPAmount, .CurrentRow.Index).Value = CDbl(.Item(ConstFCAmount, .CurrentRow.Index).Value) * IIf(CDbl(MyCtrl.Text) > 0, CDbl(MyCtrl.Text), 1)
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
            Dim x As Integer = Me.Width - plsrch.Width - (100 + IIf(_srchTxtId = 4, 100, 0))
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
                doSelect(2, IIf(activecontrolname = "grdpayment", grdpayment, grdVoucher))
            Case 2   'Customer name
                SearchPanel(grdSrch, strGridSrchString, 0)
                doSelect(1, IIf(activecontrolname = "grdpayment", grdpayment, grdVoucher))
            Case 3   'FC
                SearchPanel(grdSrch, strGridSrchString, 0)
                doSelect(2, IIf(activecontrolname = "grdpayment", grdpayment, grdVoucher))
            Case 4   'Job
                SearchPanel(grdSrch, strGridSrchString, 0)
                doSelect(2, IIf(activecontrolname = "grdpayment", grdpayment, grdVoucher))
        End Select
        _srchOnce = True
        chgbyprg = False
    End Sub

    Private Sub grdpayment_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdpayment.Enter
        Try
            If grdpayment.Rows.Count = 0 Then Exit Sub
            BeginEdit()
            If plsrch.Visible Then plsrch.Visible = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub grdpayment_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdpayment.GotFocus
        If chgbyprg Then Exit Sub
        activecontrolname = "grdpayment"
    End Sub

    Private Sub grdpayment_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdpayment.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If grdpayment.RowCount = 0 Then Exit Sub
                With grdpayment
                    If FindNextCell(grdpayment, grdpayment.CurrentCell.RowIndex, grdpayment.CurrentCell.ColumnIndex + 1) Then
                        If grdpayment.RowCount = 1 Then
                            If grdVoucher.RowCount = 0 Then
                                AddExpRow()
                                grdVoucher.CurrentCell = grdVoucher.Item(ConstName, 0)
                                assaignTotal()
                                grdVoucher.Item(ConstDescr, 0).Value = .Item(ConstDescr, .CurrentCell.RowIndex).Value
                                grdVoucher.Item(ConstPAmount, 0).Value = CDbl(lblcredit.Text)
                                assaignTotal()
                            Else
                                SrchText = ""
                                activecontrolname = "grdVoucher"
                                If grdVoucher.Item(ConstAlias, grdVoucher.RowCount - 1).Value = "" Then
                                    grdVoucher.CurrentCell = grdVoucher.Item(ConstName, grdVoucher.RowCount - 1)
                                Else
                                    grdVoucher.CurrentCell = grdVoucher.Item(ConstPAmount, grdVoucher.RowCount - 1)
                                End If
                            End If
                            'chgbyprg = True
                            grdVoucher.BeginEdit(True)
                            chgbyprg = False
                            Exit Sub
                        End If
                        .CurrentCell = .Item(ConstAlias, .CurrentRow.Index)
                    End If
                    plsrch.Visible = False
                    BeginEdit()
                End With
            ElseIf e.KeyCode = Keys.Up Or e.KeyCode = Keys.PageUp Then
                If grdpayment.RowCount = 0 Then Exit Sub
                If plsrch.Visible Then
                    doSelect(0, grdpayment)
                    Exit Sub
                End If
            ElseIf e.KeyCode = Keys.Down Or e.KeyCode = Keys.PageDown Then
                If grdpayment.RowCount = 0 Then Exit Sub
                If plsrch.Visible Then
                    doSelect(1, grdpayment)
                    Exit Sub
                End If
            ElseIf e.KeyCode = Keys.F2 Then
                If grdpayment.RowCount = 0 Then Exit Sub
                Select Case grdpayment.CurrentCell.ColumnIndex
                    Case ConstName, ConstAlias
                        ldSelect(2)
                    Case ConstPdcCustAc
                        ldSelect(1)
                End Select
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ldSelect(ByVal BVal As Single)
        fSelect = New Selectfrm
        SetForm(fSelect, BVal)
        fSelect.ShowDialog()
    End Sub
    Private Sub doSelect(ByVal Mup As Integer, ByVal grd As DataGridView)
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

                    grd.Item(ConstAlias, grd.CurrentCell.RowIndex).Value = IIf(ItmFlds(1) IsNot Nothing, ItmFlds(1), "")
                    SrchText = IIf(ItmFlds(1) IsNot Nothing, ItmFlds(1), strGridSrchString)
                    Dim s As String = IIf(ItmFlds(1) IsNot Nothing, ItmFlds(1), "")
                    grd.Item(ConstName, grd.CurrentCell.RowIndex).Value = ItmFlds(0)
                    grd.Item(ConstAccountNo, grd.CurrentCell.RowIndex).Value = IIf(ItmFlds(3) IsNot Nothing, ItmFlds(3), 0)
                Case 3 'FC
                    grd.Item(ConstFCName, grd.CurrentCell.RowIndex).Value = IIf(ItmFlds(0) IsNot Nothing, ItmFlds(0), "")
                    SrchText = IIf(ItmFlds(0) IsNot Nothing, ItmFlds(0), strGridSrchString)
                    grd.Item(ConstFCRate, grd.CurrentCell.RowIndex).Value = IIf(ItmFlds(1) IsNot Nothing, ItmFlds(1), 0)
                    grd.Item(ConstFCDec, grd.CurrentCell.RowIndex).Value = IIf(ItmFlds(4) IsNot Nothing, ItmFlds(4), 2)
                    With grd
                        If Not IsDBNull(.Item(ConstFCAmount, .CurrentRow.Index).Value) And Not IsDBNull(.Item(ConstFCRate, .CurrentRow.Index).Value) Then
                            .Item(ConstPAmount, .CurrentRow.Index).Value = CDbl(.Item(ConstFCAmount, .CurrentRow.Index).Value) * IIf(CDbl(.Item(ConstFCRate, .CurrentCell.RowIndex).Value) > 0, CDbl(.Item(ConstFCRate, .CurrentCell.RowIndex).Value), 1)
                        End If
                    End With
                Case 4 'Job
                    grd.Item(ConstJob, grd.CurrentCell.RowIndex).Value = ItmFlds(0)
                    SrchText = IIf(ItmFlds(0) IsNot Nothing, ItmFlds(0), strGridSrchString)
                Case 5 'Pdc cust name
                    grd.Item(ConstPdcCustAc, grd.CurrentCell.RowIndex).Value = ItmFlds(1)
                    SrchText = IIf(ItmFlds(1) IsNot Nothing, ItmFlds(1), strGridSrchString)
                    grd.Item(ConstPdcCustAcno, grd.CurrentCell.RowIndex).Value = IIf(ItmFlds(3) IsNot Nothing, ItmFlds(3), 0)
            End Select
nxt:
            chgbyprg = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub grdpayment_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdpayment.Leave
        activecontrolname = ""
    End Sub

    Private Sub grdpayment_LocationChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdpayment.LocationChanged

    End Sub

    Private Sub grdpayment_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdpayment.SelectionChanged
        If chgbyprg Then Exit Sub
        If _NewRw Then _NewRw = False : Exit Sub
        Try
            If grdpayment.Rows.Count = 0 Then Exit Sub
            BeginEdit()
            If plsrch.Visible Then plsrch.Visible = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
   



    Private Sub fSelect_doSelect(ByVal KeyId As String, ByVal strFld1 As String, ByVal strFld2 As String) Handles fSelect.doSelect
        chgbyprg = True
        With grdpayment
            Select Case .CurrentCell.ColumnIndex
                Case ConstAlias, ConstName
                    .Item(ConstAlias, .CurrentRow.Index).Value = strFld2
                    .Item(ConstName, .CurrentRow.Index).Value = strFld1
                    .Item(ConstAccountNo, .CurrentRow.Index).Value = KeyId
                    .Item(ConstGrpSetOn, .CurrentRow.Index).Value = GrpSetOn(KeyId)
                    FindNextCell(grdVoucher, .CurrentCell.RowIndex, .CurrentCell.ColumnIndex + 1)
                    BeginEdit()
                Case ConstPdcCustAc
                    .Item(ConstPdcCustAc, .CurrentCell.RowIndex).Value = strFld1
                    .Item(ConstPdcCustAcno, .CurrentCell.RowIndex).Value = KeyId
                    'txtSuppName.Focus()
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

        End With
        chgbyprg = False
    End Sub

    Private Sub fSelect_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fSelect.FormClosed
        fSelect = Nothing
    End Sub

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        AddRow()
    End Sub
    Private Sub LodCurrency(ByVal dt As DataTable)
        'Dim dt As DataTable = _objcmnbLayer._fldDatatable(, False)
        cmbfc.Items.Clear()
        cmbfc.Items.Add("")
        Dim i As Integer
        For i = 0 To dt.Rows.Count - 1
            cmbfc.Items.Add(dt(i)("CurrencyCode"))
        Next
    End Sub

    Private Sub cmbfc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbfc.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub cmbfc_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbfc.SelectedIndexChanged
        returnFcrt()
    End Sub
    Private Sub returnFcrt()
        If grdpayment.Columns.Count = 0 Then Exit Sub
        Dim dt As DataTable = _objcmnbLayer._fldDatatable("Select CurrencyRate,[Decimal Places] from CurrencyTb where CurrencyCode='" & cmbfc.Text & "'", False)
        If (dt.Rows.Count > 0) Then
            NDec = dt(0)("Decimal Places")
            lnumformat = "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))
            txtfcrt.Text = Format(CDbl(dt(0)("CurrencyRate")), lnumformat)
        Else
            txtfcrt.Text = Format(0, lnumformat)
            NDec = 2
        End If
        grdpayment.Columns(ConstPAmount).DefaultCellStyle.Format = "N" & NDec
    End Sub

    Private Sub txtfcrt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtfcrt.KeyDown
        If e.KeyCode = Keys.Return Then
            With grdpayment
                If .Rows.Count > 0 Then
                    .CurrentCell = .Item(ConstReference, 0)
                    .BeginEdit(True)
                End If
            End With
           
        End If
    End Sub

    Private Sub txtfcrt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtfcrt.KeyPress
        On Error Resume Next
        numCtrl = sender
        chgbyprg = True
        SelStart = numCtrl.SelectionStart
        If IsNumeric(e.KeyChar) Or e.KeyChar = "." Then
            If numCtrl.SelectionLength > 0 Then
                numCtrl.Text = Mid(numCtrl.Text, 1, SelStart) & Mid(numCtrl.Text, SelStart + numCtrl.SelectionLength + 1)
            End If
            idx = numCtrl.Text.IndexOf(".")
            If e.KeyChar <> "." Then
                If SelStart > idx Then
                    numCtrl.Text = Mid(numCtrl.Text, 1, SelStart) & Val(e.KeyChar) & Mid(numCtrl.Text, SelStart + 2)
                Else
                    numCtrl.Text = Mid(numCtrl.Text, 1, SelStart) & Val(e.KeyChar) & Mid(numCtrl.Text, SelStart + 1)
                End If
            End If
            idx = numCtrl.Text.IndexOf(".")
            If idx > 0 Then
                str1 = CDbl(Mid(numCtrl.Text, 1, idx))
                str2 = Mid(numCtrl.Text, idx + 1)
            End If
            If Len(Trim(str1)) > 10 And Len(numCtrl.Text) > 0 Then
                If Mid(Mid(Mid(numCtrl.Text, 1, idx), Len(Trim(str1)) + 1 - 10), 1, 1) = "," Then
                    str3 = Mid(Mid(numCtrl.Text, 1, idx), Len(Trim(str1)) + 2 - 10)
                End If
                numCtrl.Text = Mid(str1, Len(Trim(str1)) + 1 - 10) & str2
                SelStart = SelStart - 2
            Else
                str3 = ""
            End If
            idx = numCtrl.Text.IndexOf(".")
            If idx > 0 Then
                str1 = Mid(numCtrl.Text, 1, idx)
            Else
                str1 = numCtrl.Text
            End If
            numCtrl.Text = CDbl(numCtrl.Text)
            numCtrl.Text = Format(Val(numCtrl.Text), lnumformat)
            idx = numCtrl.Text.IndexOf(".")
            If idx > 0 Then
                str2 = Mid(numCtrl.Text, 1, idx)
            Else
                str2 = numCtrl.Text
            End If
            numCtrl.SelectionStart = SelStart + Len(str2) - IIf(str3 = "", Len(str1), Len(str3)) + 1
            'we assaigned formatted value to textbox so we not need it write it on again
            e.Handled = True
        Else
            If CInt(AscW(e.KeyChar)) = 8 Or CInt(AscW(e.KeyChar)) = 22 Then
                If CInt(AscW(e.KeyChar)) = 22 Then
                    If Not IsNumeric(Clipboard.GetText) Then
                        e.Handled = True
                    End If
                Else
                    e.Handled = False
                End If
            Else
                e.Handled = True
            End If
        End If
        chgbyprg = False
    End Sub


    Private Sub ExpensePayments_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Timer1.Enabled = True
    End Sub

    Private Sub btnaddexpense_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddexpense.Click
        AddExpRow()
    End Sub


    Private Sub picCloseProd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picCloseProd.Click
        plsrch.Visible = False
    End Sub

    Private Sub btnedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnedit.Click
        loadWaite(3)
    End Sub

    Private Sub dvData_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dvData.DoubleClick
        loadWaite(3)
    End Sub

    Private Sub txtjobcode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtjobcode.KeyDown
        lstKey = e.KeyCode
        If e.KeyCode = Keys.Return Then
            If grdVoucher.RowCount > 0 Then
                grdVoucher.CurrentCell = grdVoucher.Item(ConstAlias, 0)
                grdVoucher.Focus()
            Else
                btnaddexpense.Focus()
            End If
            If Not fMList Is Nothing Then fMList.Visible = False
        ElseIf e.KeyCode = Keys.Up Or e.KeyCode = Keys.PageUp Then
            If fMList.Visible Then
                fMList.MoveUp(txtjobcode.Text)
                Exit Sub
            End If
        ElseIf e.KeyCode = Keys.Down Or e.KeyCode = Keys.PageDown Then
            If Not fMList Is Nothing Then
                If fMList.Visible Then
                    fMList.MoveDown(txtjobcode.Text)
                    Exit Sub
                End If
            End If
        End If
    End Sub

    Private Sub txtjobcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtjobcode.TextChanged
        If chgbyprg = True Then Exit Sub
        _srchOnce = False
        _srchTxtId = 6
        ShowFmlist(sender)
    End Sub

    Private Sub txtjobcode_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtjobcode.Validated
        If _srchTxtId = 0 Then Exit Sub
        ldjbname
    End Sub
    Public Sub ldjbname()
        chgbyprg = True
        lbljobname.Text = (_objcmnbLayer.isValidEntry(txtjobcode.Text, 3))
        chgbyprg = False
    End Sub

    Private Sub dvData_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dvData.CellContentClick

    End Sub

    Private Sub btnremoveexpense_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnremoveexpense.Click
        RemoveExpRow()
    End Sub
    Public Sub ValidForPayrollPayment(ByVal r As Integer)
        Dim chkDatatable As DataTable
        chgbyprg = True
        With grdVoucher
            chkDatatable = EntriesValidation(Val(.Item(ConstAccountNo, r).Value), 9)
            If IsDBNull(.Item(ConstAccountNo, r).Value) Then .Item(ConstAccountNo, r).Value = 0
            If chkDatatable.Rows.Count > 0 And Val(.Item(ConstAccountNo, r).Value) > 0 Then
                '.Item(ConstAlias, r - 1).Value = 0
                .Item(ConstAlias, r).Value = chkDatatable(0)("Alias")
                .Item(ConstName, r).Value = chkDatatable(0)("AccDescr")
                .Item(ConstAccountNo, r).Value = chkDatatable(0)("accid")
                .Item(ConstBrId, r).Value = chkDatatable(0)("BranchId")
                .Item(ConstGrpSetOn, r).Value = GrpSetOn(chkDatatable(0)("AccountNo"))
            Else
                .Item(ConstAlias, r).Value = ""
                .Item(ConstName, r).Value = ""
                .Item(ConstAccountNo, r).Value = 0
                .Item(ConstBrId, r).Value = ""
                .Item(ConstGrpSetOn, r).Value = ""
            End If
            .Rows(r).Cells(Constchq).ReadOnly = True
            .Rows(r).Cells(ConstChqdate).ReadOnly = True
            .Rows(r).Cells(ConstBank).ReadOnly = True
            .Rows(r).Cells(ConstPdcCustAc).ReadOnly = True
        End With
        chgbyprg = False
    End Sub

    Private Sub numVchrNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles numVchrNo.KeyPress
        NumericTextOnKeypress(numVchrNo, e, chgbyprg, "0")
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
                saveTrans()
            Case 3
                editRecord()
        End Select
        If Not fwait Is Nothing Then
            fwait.Close()
            fwait = Nothing
        End If
    End Sub

    Private Sub lbljobname_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbljobname.Click

    End Sub

    Private Sub TabPage1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage1.Click

    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Timer2.Enabled = False
        editRecord(editlinkno)
    End Sub

    Private Sub grdVoucher_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellContentClick

    End Sub
End Class