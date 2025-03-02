
Public Class SupplierPayments
#Region "Local Variable Declerations"
    Public isModi As Boolean
    Private activecontrolname As String
    Public chgbyprg As Boolean
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
    Private chgsup As Boolean
#End Region
#Region "Constant Declerations"

    'const declarations
    Private Const ConstAlias = 0
    Private Const ConstName = 1
    Private Const ConstReference = 2
    Private Const ConstDescr = 3
    Private Const ConstDtype = 4
    Private Const ConstPAmount = 5
    Private Const ConstFCName = 6
    Private Const ConstFCRate = 7
    Private Const ConstFCAmount = 8
    Private Const ConstJob = 9
    Private Const Constchq = 10
    Private Const ConstChqdate = 11
    Private Const ConstBank = 12
    Private Const ConstPdcCustAc = 13
    Private Const ConstPdcCustAcno = 14
    Private Const ConstFCDec = 15
    Private Const ConstAccountNo = 16
    Private Const ConstBrId = 17
    Private Const ConstGrpSetOn = 18
    Private Const ConstUnq = 19

    'LIST GRID CONSTANT variables
    Private Const ConstInvNo = 0
    Private Const ConstTrdate = 1
    Private Const ConstInvAmount = 2
    Private Const ConstCustId = 3
    Private Const ConstCustname = 4
    Private Const ConstTrRef = 5
    Private Const Consttype = 6
    Private Const ConstLinkNo = 7

    'Tr datagrid
    Private Const _vJVDate = 0
    Private Const _vRef = 1
    Private Const _vTag = 2
    Private Const _vType = 3
    Private Const _vAssign = 4
    Private Const _vBalance = 5
    Private Const _vInvAmt = 6
    Private Const _installment = 7
    Private Const _vSetOffAmt = 8
    Private Const _vFCamt = 9
    Private Const _vCurrencyCode = 10
    Private Const _vRate = 11
    Private Const _vEntryRef = 12
    Private Const _vFcdec = 13
    Private Const _vLpo = 15
    Private Const _vJobcode = 17
    Private Const _SetoffCount = 18
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

    Private Sub SupplierPayments_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        dtpdate.Focus()
    End Sub
    Private Sub loadMasters()
        Dim qry As String
        Dim branch As String = IIf(UsrBr = "", Dbranch, UsrBr)
        Dim prefixacc As String = " select Alias,AccDescr,GrpSetOn,accid from (select ANo,VrTypeNo from PreFixTb group by VrTypeNo,ANo)PreFixTb inner join (SELECT Alias,AccDescr,GrpSetOn,accid FROM AccMast " & _
                                                "LEFT JOIN S1AccHd ON AccMast.S1AccId=S1AccHd.S1AccId) acc1 on PreFixTb.ANo=acc1.accid WHERE VrTypeNo = 1"

        prefixacc = prefixacc & " Union all select Alias,AccDescr,GrpSetOn,accid from (select ANo2,VrTypeNo from PreFixTb group by VrTypeNo,ANo2)PreFixTb inner join (SELECT Alias,AccDescr,GrpSetOn,accid FROM AccMast " & _
                                                "LEFT JOIN S1AccHd ON AccMast.S1AccId=S1AccHd.S1AccId) acc2 on PreFixTb.ANo2=acc2.accid WHERE VrTypeNo = 1"

        Dim userInvnos As String = IIf(branch = "", " SELECT * FROM InvNos WHERE InvType='PV'", " SELECT * FROM InvNosBrTb WHERE Brcode='" & branch & "' AND InvType='PV'")


        qry = "SELECT CurrencyCode FROM CurrencyTb"
        qry = qry & "  SELECT * FROM PreFixTb WHERE VrTypeNo = 1" & IIf(branch = "", "", " AND BrId In ('', '" & branch & "')") & " Order by ordNo"
        qry = qry & prefixacc & userInvnos

        Dim dtset As DataSet
        dtset = _objcmnbLayer._ldDataset(qry, False)
        LodCurrency(dtset.Tables(0))
        PreFixTb = dtset.Tables(1)
        dtAcc = dtset.Tables(2)
        dtInvNos = dtset.Tables(3)
    End Sub
    Private Sub SupplierPayments_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetGridHeadPayment()
        lnumformat = numFormat
        chgbyprg = True
        loadMasters()
        crtSubVrs(cmbVoucherTp, 1, True)
        CreateSetoffTable(dtSetoffTable)
        chgbyprg = False
        If Not userType Then
            btndelete.Tag = 1
        Else
            btndelete.Tag = IIf(getRight(68, CurrentUser), 1, 0)
            chkonAc.Visible = IIf(getRight(170, CurrentUser), 0, 1)
        End If
        Timer1.Enabled = True
        NDec = NoOfDecimal
    End Sub

    Private Sub btnexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexit.Click
        Me.Close()
    End Sub
  
    'Private Sub AddNewClick()
    '    AddRow()
    '    btnModify.Text = "&Modify"
    '    btnupdate.Enabled = True
    'End Sub
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
    Private Sub RemoveRow()
        If grdVoucher.RowCount > 0 Then
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
    

    Private Sub btnrem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrem.Click
        RemoveRow()
    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            Const WM_KEYDOWN As Integer = &H100
            Const WM_KEYUP As Integer = &H101
            'msg.ToString()
            If msg.Msg = WM_KEYDOWN Or msg.Msg = WM_KEYUP Then
                If activecontrolname = "grdVoucher" Then
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
    Private Sub FillAmt(ByVal grd As DataGridView)
        With grd
            Dim assignAmt As Double
            If .RowCount = 0 Then Exit Sub
            Dim balanceamt As Double = .Item(_vBalance, .CurrentCell.RowIndex).Value
            If (CDbl(lblDiff.Text) < 0 And CDbl(.Item(_vBalance, .CurrentCell.RowIndex).Value) < 0) Or _
                (CDbl(lblDiff.Text) > 0 And CDbl(.Item(_vBalance, .CurrentCell.RowIndex).Value) > 0) Then
                If Math.Abs(CDbl(lblDiff.Text)) >= Math.Abs(CDbl(.Item(_vBalance, .CurrentCell.RowIndex).Value)) Then
                    assignAmt = Math.Abs(CDbl(.Item(_vBalance, .CurrentCell.RowIndex).Value))
                    .Item(_vAssign, .CurrentCell.RowIndex).Value = Format(assignAmt, "0.00")
                Else
                    .Item(_vAssign, .CurrentCell.RowIndex).Value = Format(Math.Abs(CDbl(lblDiff.Text)), "0.00")
                End If
                assaignTotal()
            End If
            If IsDBNull(grdVoucher.Item(_vAssign, grdVoucher.CurrentCell.RowIndex).Value) Then grdVoucher.Item(_vAssign, grdVoucher.CurrentCell.RowIndex).Value = 0
            .Item(_vTag, .CurrentCell.RowIndex).Value = IIf(.Item(_vTag, .CurrentCell.RowIndex).Value = "Y", "", "Y")
        End With
    End Sub
    Private Sub grdVoucher_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellClick
        'If e.ColumnIndex = ConstAmount Or e.ColumnIndex = ConstFCAmount Then grdVoucherBeginEdit()
        If grdVoucher.RowCount = 0 Then Exit Sub
        If e.ColumnIndex = _vTag Then
            If IsDBNull(grdVoucher.Item(_vTag, e.RowIndex).Value) Then grdVoucher.Item(_vTag, e.RowIndex).Value = ""
            If grdVoucher.Item(_vTag, e.RowIndex).Value = "Y" Then
                grdVoucher.Item(_vTag, e.RowIndex).Value = ""
                grdVoucher.Item(_vAssign, e.RowIndex).Value = Format(0, "0.00")
            Else
                FillAmt(grdVoucher)
                If CDbl(grdVoucher.Item(_vAssign, grdVoucher.CurrentCell.RowIndex).Value) = 0 Then assignFull(grdVoucher)
            End If
            With grdVoucher
                If .Rows.Count > 0 Then
                    If Not IsDBNull(.Item(_vRate, .CurrentRow.Index).Value) Then
                        If Val(.Item(_vAssign, .CurrentRow.Index).Value & "") = 0 Then .Item(_vAssign, .CurrentRow.Index).Value = 0
                        .Item(_vFCamt, .CurrentRow.Index).Value = CDbl(.Item(_vAssign, .CurrentRow.Index).Value) / IIf(CDbl(.Item(_vRate, .CurrentRow.Index).Value) > 0, CDbl(.Item(_vRate, .CurrentRow.Index).Value), 1)
                    Else
                        .Item(_vFCamt, .CurrentRow.Index).Value = .Item(_vAssign, .CurrentRow.Index).Value
                    End If
                End If
            End With
        ElseIf e.ColumnIndex = _vAssign Then
            chgbyprg = True
            grdVoucher.BeginEdit(True)
            chgbyprg = False
        End If
        assaignTotal()
    End Sub
    Private Sub assignFull(ByVal grd As DataGridView)
        With grd
            .Item(_vTag, .CurrentCell.RowIndex).Value = "Y"
            .Item(_vAssign, .CurrentCell.RowIndex).Value = Math.Abs(.Item(_vBalance, .CurrentCell.RowIndex).Value) 'Format(Math.Abs(CDbl(.Item(_vInvAmt, .CurrentCell.RowIndex).Value)), "#,##" & lnumFormat)
            'assaignTotal()
        End With
    End Sub

    Private Sub grdVoucher_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellEndEdit
        Dim col As Integer = e.ColumnIndex
        Dim fmt As String = "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))
        If col = ConstPAmount Then
            grdVoucher.Item(col, e.RowIndex).Value = Format(Convert.ToDecimal(grdVoucher.Item(col, e.RowIndex).Value), fmt)
        End If
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
                'AddRow()
            End If
nxt:
            chgbyprg = True
            grdVoucher.BeginEdit(True)
            chgbyprg = False
        ElseIf e.KeyCode = Keys.F3 Then
            AddRow()
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

    Private Sub txtSuppName_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSuppName.KeyDown, txtDescription.KeyDown, numVchrNo.KeyDown, dtpdate.KeyDown
        lstKey = e.KeyCode
        Dim myctrl As Object = sender
        If e.KeyCode = Keys.Enter Then
                If myctrl.name = "dtpdate" Then
                grdpayment.CurrentCell = grdpayment.Item(ConstReference, 0)
                grdpayment.BeginEdit(True)
            Else
                SendKeys.Send("{TAB}")
            End If

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

    Private Sub txtSuppName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSuppName.TextChanged, txtDescription.TextChanged
        If chgbyprg = True Then Exit Sub
        Dim myCtrl As Control = sender
        Select Case myCtrl.Name
            Case "txtSuppName"
                _srchTxtId = 1
                _srchOnce = False
                ShowFmlist(sender)
                chgsup = True
            Case "txtBank"
                _srchTxtId = 2
                _srchOnce = False
                ShowFmlist(sender)
        End Select
        btnupdate.Enabled = True
        chgPost = True
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
                        SetFmlist(fMList, 3)
                    Case 2
                        SetFmlist(fMList, 15)
                End Select
                fMList.loc = PopupLoc
                fMList.Show(fMainForm)
                fMList.Visible = True
            End If
        End If
        Select Case _srchTxtId
            Case 1   'Supplier
                fMList.SearchIndex = 0
                fMList.SearchIndexDescr = 1
                fMList_doFocus()
                fMList.Search(txtSuppName.Text)
                fMList.AssignList(txtSuppName, lstKey, chgbyprg)
            Case 2   'Bank 
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
                txtSuppName.Text = ItmFlds(0)
                txtSuppName.Tag = ItmFlds(3)

            Case 2
                txtBank.Text = ItmFlds(0)
                txtBank.Tag = ItmFlds(3)
        End Select
        chgbyprg = False
    End Sub

    Private Sub txtSuppName_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSuppName.Validated
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
        'If fMList.Visible Then
        '    fMList.Close()
        '    fMList = Nothing
        'End If
        Dim myctrl As TextBox = sender
        Dim dt As DataTable
        If myctrl.Name = "txtSuppName" Then
            If chgsup Then
                dt = _objcmnbLayer._fldDatatable("SELECT accid,AccDescr FROM AccMast WHERE AccDescr='" & MkDbSrchStr(txtSuppName.Text) & "'")
                If dt.Rows.Count > 0 Then
                    chgbyprg = True
                    txtSuppName.Tag = dt(0)(0)
                    txtSuppName.Text = dt(0)(1)

                    ldTrans()
                    Dim i As Integer
                    For i = 0 To grdpayment.Rows.Count - 1
                        grdpayment.Item(ConstDescr, i).Value = "Amount Paid to " & dt(0)(1)
                    Next
                    chgbyprg = False
                Else
                    txtSuppName.Tag = 0
                End If
            End If
            chgsup = False
        ElseIf myctrl.Name = "txtBank" Then
            dt = _objcmnbLayer._fldDatatable("SELECT AccountNo FROM AccMast WHERE AccDescr='" & MkDbSrchStr(txtBank.Text) & "'")
            If dt.Rows.Count > 0 Then
                txtBank.Tag = dt(0)(0)
            Else
                txtBank.Tag = 0
            End If
        End If

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
            If Val(txtSuppName.Tag) = 0 Then
                txtSuppName.Tag = vrAccountNo2
            End If
            Dim dtAcc As DataTable
            dtAcc = _objcmnbLayer._fldDatatable("SELECT Alias,AccDescr,GrpSetOn,GrpSetOn,accid FROM AccMast " & _
                                                "LEFT JOIN S1AccHd ON AccMast.S1AccId=S1AccHd.S1AccId WHERE accid=" & vrAccountNo1)
            Dim i As Integer
            'assaignTotal()
            If dtAcc.Rows.Count > 0 Then
                With grdpayment
                    chgbyprg = True
                    If Not grdpayment.RowCount > 0 Then
                        .Rows.Add(1)
                    End If
                    i = .CurrentRow.Index
                    .Item(ConstDtype, i).Value = "Cr"
                    .Item(ConstAccountNo, i).Value = Val(dtAcc(0)("accid"))
                    lblaccountbalance.Text = getAccBal(Val(dtAcc(0)("accid")))
                    lblaccountbalance.Text = dtAcc(0)("AccDescr") & ": " & Format(CDbl(lblaccountbalance.Text), numFormat)
                    .Item(ConstName, i).Value = dtAcc(0)("AccDescr")
                    .Item(ConstAlias, i).Value = dtAcc(0)("Alias")
                    .Item(ConstFCRate, i).Value = 1
                    .Item(ConstGrpSetOn, i).Value = Trim(dtAcc(0)("GrpSetOn"))

                    If UCase(Trim(dtAcc(0)("GrpSetOn"))) = "BANK" Or UCase(Trim(dtAcc(0)("GrpSetOn"))) = "P.D.C.(I)" Then
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

    Private Sub numVchrNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles numVchrNo.KeyPress
        NumericTextOnKeypress(numVchrNo, e, chgbyprg, "0")
    End Sub

    Private Sub numVchrNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numVchrNo.TextChanged

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
        If Val(txtSuppName.Tag) = 0 Then
            MsgBox("Invalid Supplier Name", MsgBoxStyle.Exclamation)
            txtSuppName.Focus()
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
        If (CDbl(lbldiff.Text) <> 0 And Not chkonAc.Checked) Or (CDbl(lblcredit.Text) = 0 And CDbl(lblTlDebit.Text) = 0) Or Val(lbldiff.Text) > 0 Then
            MsgBox("Amount is not balanced or empty entry.", MsgBoxStyle.Exclamation)
            txtSuppName.Focus()
            Exit Sub
        End If
        If Not isGridValid() Then Exit Sub
        Dim i As Integer
        For i = 0 To grdpayment.RowCount - 1
            If UCase(grdpayment.Item(ConstDtype, i).Value) = "CR" Then
                If Not checkReconciliation(grdpayment, ConstAccountNo, ConstPAmount, ConstName, i) Then Exit Sub
            End If
        Next
        If enableBranch And UsrBr = "" And Dbranch = "" Then
            MsgBox("Transaction cannot be saved without Branch! Please login with Branch", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If MsgBox("Verification Succeded, Do you want to File it?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            'If Not saveTrans() Then Exit Sub
            'numPrintVchr.Text = numVchrNo.Text
            'makeClear()
            loadWaite(2)

        Else
            dtpdate.Focus()
        End If

    End Sub
    Private Function isGridValid() As Boolean
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
                            If Val(grdpayment.Item(ConstPdcCustAcno, r).Value & "") = Val(grdpayment.Item(ConstPdcCustAcno, i).Value & "") And UCase(Trim(grdpayment.Item(Constchq, r).Value)) = UCase(Trim(grdpayment.Item(Constchq, i).Value)) And UCase(grdpayment.Item(ConstBank, r).Value) = UCase(grdpayment.Item(ConstBank, i).Value) Then
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
        txtSuppName.Text = ""
        txtSuppName.Tag = ""
        txtDescription.Text = ""
        txtchequeno.Text = ""
        txtchequeno.Tag = ""
        txtBank.Text = ""
        loadedTrId = 0
        txtBank.Tag = ""
        cmbfc.Text = ""
        txtfcrt.Text = Format(1, lnumformat)
        dtpchequedate.Value = DateValue(Date.Now)
        dtpdate.Value = DateValue(Date.Now)
        dtSetoffTable.Rows.Clear()
        grdpayment.Rows.Clear()
        If Not skipcmbselectChange Then cmbVoucherTp_SelectedIndexChanged(cmbVoucherTp, New System.EventArgs)
        chgbyprg = False
        dtpdate.Focus()
        assaignTotal()
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
        If dtAccTb.Rows.Count > 0 Then dtAccTb.Rows.Clear()
        'LinkNo = Val(_objTr.SaveAccTrCmn())
        '_objcmnbLayer._saveDatawithOutParm("DELETE FROM AccTrDet WHERE dealamt>0 and LinkNo=" & LinkNo)
        '_objcmnbLayer._saveDatawithOutParm("DELETE FROM AccTrDet WHERE LinkNo=" & LinkNo)
        Dim i As Integer
        Dim paymentaccount As Integer
        paymentaccount = Val(grdpayment.Item(ConstAccountNo, 0).Value)
        Dim refs As String = ""
        Dim entryref As String = "AMOUNT PAID BY " & grdpayment.Item(ConstName, 0).Value & IIf(grdpayment.Item(ConstReference, 0).Value = "", "", "( Ref: " & grdpayment.Item(ConstReference, 0).Value & ")")
        With grdVoucher
            For i = 0 To .RowCount - 1
                If Val(.Item(_vAssign, i).Value & "") = 0 Or Trim(.Item(_vTag, i).Value & "") = "" Then GoTo nxt
                setAcctrDetValue(LinkNo, i, grdVoucher, 0, paymentaccount, entryref & " / " & txtDescription.Text)
                refs = refs & IIf(refs = "", "", ",") & .Item(_vRef, i).Value
                '_objTr.saveAccTrans()
nxt:
            Next
        End With
        entryref = "Amount Paid to " & txtSuppName.Text
        With grdpayment
            For i = 0 To .RowCount - 1
                If Val(.Item(ConstPAmount, i).Value & "") = 0 Then GoTo nxt1
                If Val(txtfcrt.Text) = 0 Then txtfcrt.Text = 1
                .Item(ConstFCRate, i).Value = CDbl(txtfcrt.Text)
                .Item(ConstFCName, i).Value = cmbfc.Text
                setAcctrDetValue(LinkNo, i, grdpayment, 1, Val(txtSuppName.Tag), entryref & " References : " & refs)
                '_objTr.saveAccTrans()
nxt1:
            Next
        End With
        If chkonAc.Checked Then
            setAcctrDetValue(LinkNo, Val(txtSuppName.Tag), "ON/AC", "ADVANCE PAYMENT", CDbl(lbldiff.Text) * -1, "", DateValue("01/01/1950"), 0)
            '_objTr.saveAccTrans()
        End If

        _objTr.SaveAccTrWithDt(dtAccTb)
        numPrintVchr.Text = numVchrNo.Text
        txtprintprefix.Text = txtPreFix.Text

        If Not isModi Then
            SetNextVrNo(numVchrNo, Val(cmbVoucherTp.Tag), "PV", "JvType = 'PV' AND JvNum = ", True, True, True)
        Else
            btnModify_Click(btnModify, New System.EventArgs)
        End If
        makeClear()
        If Not fwait Is Nothing Then fwait.Close() : fwait = Nothing
        MsgBox("Payment Voucher # " & numPrintVchr.Text & " Saved Successfully", MsgBoxStyle.Information)
        'numPrintVchr.Text = numPrintVchr.Tag
        'txtprintprefix.Text = txtprintprefix.Tag
    End Function
    Private Sub setAcctrCmnValue()
        _objTr.JVType = "PV"
        _objTr.JVDate = DateValue(dtpdate.Value)
        _objTr.PreFix = ""
        _objTr.JVNum = Val(numVchrNo.Text)
        _objTr.JVTypeNo = getVouchernumber("PV")
        _objTr.UserId = CurrentUser
        _objTr.MchName = MACHINENAME
        _objTr.CrtDtTm = DateValue(Date.Now)
        _objTr.TypeNo = cmbVoucherTp.Tag
        _objTr.VrDescr = IIf(txtDescription.Text = "", grdpayment.Item(ConstDescr, 0).Value, Trim(txtDescription.Text))
        _objTr.IsModi = IIf(loadedTrId > 0, 2, 0)
        _objTr.LinkNo = loadedTrId
        _objTr.isLinkNo = True
        _objTr.isdeleteTr = 1
    End Sub
    Private Sub setAcctrDetValue(ByVal lnkNo As Long, ByVal _row As Integer, ByVal grd As DataGridView, ByVal trtype As Integer, ByVal paymentaccount As Integer, ByVal EntryRef As String)

        Dim dtrow As DataRow
        dtrow = dtAccTb.NewRow
        dtrow("LinkNo") = lnkNo
        dtrow("AccountNo") = IIf(trtype = 0, Val(txtSuppName.Tag), Trim(grd.Item(ConstAccountNo, _row).Value & ""))
        dtrow("Reference") = Trim(grd.Item(IIf(trtype = 0, _vRef, ConstReference), _row).Value & "")
        dtrow("EntryRef") = EntryRef
        If IsDBNull(grd.Item(IIf(trtype = 0, _vFCamt, ConstFCAmount), _row).Value) Then
            dtrow("FCAmt") = dtrow("DealAmt")
        Else
            dtrow("FCAmt") = CDbl(grd.Item(IIf(trtype = 0, _vFCamt, ConstFCAmount), _row).Value) * IIf(grd.Item(IIf(trtype = 0, _vType, ConstDtype), _row).Value = "Dr", 1, -1)
        End If
        dtrow("JobCode") = ""

        If IsDBNull(grd.Item(IIf(trtype = 0, _vRate, ConstFCRate), _row).Value) Then
            grd.Item(IIf(trtype = 0, _vRate, ConstFCRate), _row).Value = 1
        End If
        dtrow("CurrRate") = CDbl(grd.Item(IIf(trtype = 0, _vRate, ConstFCRate), _row).Value)
        dtrow("CurrencyCode") = Trim(grd.Item(IIf(trtype = 0, _vCurrencyCode, ConstFCName), _row).Value & "")
        dtrow("DealAmt") = CDbl(grd.Item(IIf(trtype = 0, _vAssign, ConstPAmount), _row).Value) * IIf(grd.Item(IIf(trtype = 0, _vType, ConstDtype), _row).Value = "Dr", 1, -1)
        dtrow("DealAmt") = dtrow("DealAmt") * dtrow("CurrRate")

        dtrow("TrInf") = 0
        If trtype = 0 Then
            dtrow("UnqNo") = 0
            If chkDate(grdpayment.Item(ConstChqdate, 0).Value & "") Then
                dtrow("ChqDate") = DateValue(grdpayment.Item(ConstChqdate, 0).Value)
            End If
            dtrow("ChqNo") = ""
            dtrow("BankCode") = ""
            dtrow("CustAcc") = 0
            dtrow("PDCAcc") = paymentaccount
        Else
            dtrow("UnqNo") = 0 = 0 'Val(grd.Item(ConstUnq, _row).Value)
            dtrow("BankCode") = grd.Item(ConstBank, _row).Value
            dtrow("ChqNo") = grd.Item(Constchq, _row).Value
            If Not IsDBNull(grd.Item(ConstChqdate, _row).Value) Then
                If chkDate(grd.Item(ConstChqdate, _row).Value) Then
                    dtrow("ChqDate") = DateValue(grd.Item(ConstChqdate, _row).Value)
                Else
                    dtrow("ChqDate") = DateValue("01/01/1950")
                End If
            End If
            dtrow("CustAcc") = paymentaccount
            dtrow("PDCAcc") = 0
        End If

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
    Private Sub setAcctrDetValue(ByVal lnkNo As Long, ByVal AccountNo As Long, ByVal reference As String, ByVal EntryRef As String, ByVal Amount As Double, ByVal ChqNo As String, ByVal ChqDate As Date, ByVal AccsetId As Integer)
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
        dtrow("PDCAcc") = Val(txtSuppName.Tag)
        dtrow("ChqDate") = ChqDate
        dtrow("ChqNo") = ChqNo
        dtrow("BankCode") = ""
        dtrow("UnqNo") = 0
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
            makeClear()
            cmbVoucherTp_SelectedIndexChanged(cmbVoucherTp, New EventArgs)
            numVchrNo.Focus()
        Else
            If isModi Then
                TabControl1.SelectedIndex = 1
                btnpayment.Enabled = False
                btnModify.Enabled = False
                btnupdate.Enabled = False
                btndelete.Enabled = False
            End If
            makeClear()
            cmbVoucherTp_SelectedIndexChanged(cmbVoucherTp, New EventArgs)
            btnModify.Text = "&Clear"
            isModi = False
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
            .ptype = 5
            .DateFrom = DateValue(dtpstart.Value)
            .DateTo = DateValue(dtpto.Value)
            .JVType = "PV"
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

            .Columns(ConstCustname).HeaderText = "Supplier Name"
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
        '_objcmnbLayer._saveDatawithOutParm("DELETE FROM AccTrCmn WHERE LinkNo=" & loadedTrId)
        '_objcmnbLayer._saveDatawithOutParm("DELETE FROM AccTrDet WHERE LinkNo=" & loadedTrId)
        _objTr.deleteAccountTransaction(loadedTrId)
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
            btnpayment.Enabled = False
        Else
            btnModify.Enabled = True
            If Not isModi Then
                cmbVoucherTp_SelectedIndexChanged(cmbVoucherTp, New EventArgs)
            End If
            btnpayment.Enabled = True
            'btnupdate.Enabled = True
        End If
    End Sub

    Private Sub grdVoucher_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdVoucher.EditingControlShowing
        Dim Col As Integer
        Col = grdVoucher.CurrentCell.ColumnIndex
        If Col = _vAssign Then
            Dim tb As TextBox = CType(e.Control, TextBox)
            RemoveHandler tb.KeyPress, AddressOf Textbox_KeyPress
            AddHandler tb.KeyPress, AddressOf Textbox_KeyPress

            RemoveHandler tb.TextChanged, AddressOf Textbox_TextChanged
            AddHandler tb.TextChanged, AddressOf Textbox_TextChanged
        End If
    End Sub
    Private Sub Textbox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            Dim col As Integer
            col = grdVoucher.CurrentCell.ColumnIndex
            If col = _vAssign Then
                If Not IsNumeric(e.KeyChar) And e.KeyChar <> "." And Not e.KeyChar = Convert.ToChar(Keys.Back) Then e.Handled = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


    Private Sub dvData_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dvData.CellDoubleClick
        loadWaite(3)
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

    Private Sub dvData_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dvData.CellContentClick

    End Sub
    Public Sub editRecord(Optional ByVal linkno As Long = 0, Optional ByVal supname As String = "")
        If dvData.RowCount = 0 And linkno = 0 Then Exit Sub
        Dim dtAccTr As DataTable
        Dim salias As String
        makeClear(True)
        numVchrNo.Text = ""
        If linkno = 0 Then
            loadedTrId = dvData.Item(ConstLinkNo, dvData.CurrentRow.Index).Value
            salias = Trim(dvData.Item(ConstCustId, dvData.CurrentRow.Index).Value & "")
            dtAccTr = _objcmnbLayer._fldDatatable("SELECT ACCID,AccDescr FROM AccMast where alias='" & salias & "'")
        Else
            loadedTrId = linkno
            salias = supname
            dtAccTr = _objcmnbLayer._fldDatatable("SELECT ACCID,AccDescr FROM AccMast where AccDescr='" & salias & "'")

        End If
        'loadedTrId = dvData.Item(ConstLinkNo, dvData.CurrentRow.Index).Value
        'salias = Trim(dvData.Item(ConstCustId, dvData.CurrentRow.Index).Value & "")
        'dtAccTr = _objcmnbLayer._fldDatatable("SELECT ACCID,AccDescr FROM AccMast where alias='" & salias & "'")
        If dtAccTr.Rows.Count > 0 Then
            chgbyprg = True
            txtSuppName.Text = dtAccTr(0)("AccDescr")
            txtSuppName.Tag = dtAccTr(0)("ACCID")
            chgbyprg = False
        End If
        btnModify.Text = "Undo"
        isModi = True
        assaignTotal()
        ldRec()
        TabControl1.SelectedIndex = 0
    End Sub


    Private Sub btnedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnedit.Click
        editRecord()
    End Sub
    Private Sub ldRec()
        Dim dtAccTrCmn As DataTable
        Dim dtAccTr As DataTable
        Dim AccTrCmn As String
        Dim i As Integer
        Dim r As Integer
        chgbyprg = True
        Dim chqStatus As Boolean
        Dim chqStatusDt As DataTable
        'AccTrCmn = "SELECT * FROM AccTrCmn WHERE LinkNo=" & loadedTrId
        'grdpayment.Rows.Clear()
        'dtAccTrCmn = _objcmnbLayer._fldDatatable(AccTrCmn)
        Dim qry As String
        Dim drqry As String = " " & getAccTrDetbyloadedTrId(loadedTrId, " AND DealAmt<0", True)
        drqry = "SELECT *,CHQ.dbcbnos trrfd FROM (" & drqry & ")TR left join (select LinkNo dbcbnos,JVNum dbcbJvno,JVDate trfrDate  " & _
                                                  "from AccTrCmn) CHQ ON TR.DBCBNo=CHQ.dbcbnos "

        qry = "SELECT * FROM AccTrCmn WHERE LinkNo=" & loadedTrId
        qry = qry & drqry
        chgbyprg = True
        grdpayment.Rows.Clear()
        Dim dtset As DataSet = _objcmnbLayer._ldDataset(qry, False)
        dtAccTrCmn = dtset.Tables(0)
        dtAccTr = dtset.Tables(1)
        If dtAccTrCmn.Rows.Count > 0 Then
            loadedTrId = dtAccTrCmn(0)("LinkNo")
            dtpdate.Value = DateValue(dtAccTrCmn(0)("JVDate"))
            chgbyprg = True
            numVchrNo.Text = dtAccTrCmn(0)("JVnum")
            numPrintVchr.Text = dtAccTrCmn(0)("JVnum")
            txtPreFix.Text = dtAccTrCmn(0)("Prefix")
            txtDescription.Text = Trim(dtAccTrCmn(0)("VrDescr") & "")
            'dtAccTr = _objcmnbLayer._fldDatatable(getAccTrDetbyloadedTrId(loadedTrId, " AND DealAmt<0"))
            For i = 0 To dtAccTr.Rows.Count - 1
                With grdpayment
                    'chqStatusDt = _objcmnbLayer._fldDatatable("select LinkNo dbcbnos,JVNum dbcbJvno,JVDate trfrDate  " & _
                    '"from AccTrCmn where Linkno=" & Val(dtAccTr(0)("DBCBNo") & ""))
                    If Val(dtAccTr(i)("trrfd") & "") > 0 Then
                        MsgBox("PDC Already Transfered! you can not modify/delete", MsgBoxStyle.Exclamation)
                        chqStatus = True
                    End If
                    .Rows.Add()
                    r = .Rows.Count - 1
                    .Item(ConstAlias, r).Value = dtAccTr(i)("Alias")
                    .Item(ConstName, r).Value = dtAccTr(i)("AccDescr")
                    .Item(ConstReference, r).Value = dtAccTr(i)("Reference")
                    .Item(ConstDescr, r).Value = dtAccTr(i)("EntryRef")
                    cmbfc.Text = dtAccTr(i)("CurrencyCode")
                    txtfcrt.Text = Format(dtAccTr(i)("CurrRate"), lnumformat)
                    If Val(dtAccTr(i)("CurrRate") & "") = 0 Then dtAccTr(i)("CurrRate") = 1
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

                    .Item(ConstBank, r).Value = dtAccTr(i)("BankCode")
                    .Item(ConstPdcCustAc, r).Value = dtAccTr(i)("pdcname")
                    .Item(ConstPdcCustAcno, r).Value = dtAccTr(i)("pdcid")
                    .Item(ConstUnq, r).Value = dtAccTr(i)("UnqNo")
                    .Item(ConstGrpSetOn, r).Value = dtAccTr(i)("GrpSetOn")
                    .Item(ConstAccountNo, r).Value = dtAccTr(i)("accountno")

                    '-------
                    If UCase(Trim(.Item(ConstGrpSetOn, r).Value)) = "BANK" Or UCase(Trim(.Item(ConstGrpSetOn, r).Value)) = "P.D.C.(I)" Or UCase(Trim(.Item(ConstGrpSetOn, r).Value)) = "P.D.C.(R)" Then
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
            If grdpayment.RowCount > 0 Then
                FindNextCell(grdpayment, grdpayment.CurrentCell.RowIndex, grdpayment.CurrentCell.ColumnIndex + 1)
            End If

            ldTrans()
            activecontrolname = ""
            If chqStatus Then
                btndelete.Enabled = False
                btnupdate.Enabled = False
            Else
                btndelete.Enabled = True
                btnupdate.Enabled = True
            End If
            btnModify.Enabled = True
            btnpayment.Enabled = True
        Else
            MsgBox("Payment Voucher Not Found", MsgBoxStyle.Exclamation)
        End If
        chgbyprg = False
    End Sub

    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        LoadReport(RptFlName, RptCaption, forPrint)
    End Sub
    Private Sub btnpayment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpayment.Click
        If Not fPpayment Is Nothing Then
            fPpayment.Close()
            fPpayment = Nothing
        End If
        fPpayment = New PreviousPaymentFrm
        With fPpayment
            .AccountNo = Val(txtSuppName.Tag)
            .dtpstart.Value = DateFrom
            .dtpto.Value = DateTo
            .jvtype = "PV"
            .ldGrid(4)
            .ShowDialog()
        End With
        fPpayment = Nothing
    End Sub
    Sub SetGridHeadPayment()
        With grdpayment
            SetEntryGridProperty(grdpayment)
            .ColumnCount = 20


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
        End With

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        setColwidth()
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

    Private Sub SupplierPayments_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Timer1.Enabled = True
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
        Valid(e.RowIndex, e.ColumnIndex)
        SrchText = ""
    End Sub
    Private Sub Valid(ByVal RowIndex As Integer, ByVal ColIndex As Integer)
        Dim chkDatatable As DataTable
        chgbyprg = True
        With grdpayment
            Select Case ColIndex
                Case ConstAlias, ConstName
                    If SrchText = "" And Not IsDBNull(grdpayment.Item(ConstAlias, grdpayment.CurrentCell.RowIndex).Value) Then
                        SrchText = grdpayment.Item(ConstAlias, grdpayment.CurrentCell.RowIndex).Value
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
                        SrchText = grdpayment.Item(ConstFCName, .CurrentCell.RowIndex).Value
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
                    If SrchText = "" And Not IsDBNull(.Item(ConstJob, grdpayment.CurrentCell.RowIndex).Value) Then
                        SrchText = grdpayment.Item(ConstJob, .CurrentCell.RowIndex).Value
                    End If
                    chkDatatable = EntriesValidation(SrchText, 5)
                    If chkDatatable.Rows.Count > 0 Then
                        .Item(ConstJob, .CurrentCell.RowIndex).Value = SrchText
                    Else
                        .Item(ConstJob, .CurrentCell.RowIndex).Value = ""
                    End If
                Case ConstPdcCustAc
                    If SrchText = "" And Not IsDBNull(.Item(ConstPdcCustAc, grdpayment.CurrentCell.RowIndex).Value) Then
                        SrchText = grdpayment.Item(ConstPdcCustAc, .CurrentCell.RowIndex).Value
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
                    '        .Item(ConstReference, .CurrentCell.RowIndex).Value = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(grdpayment.Item(ConstReference, grdpayment.CurrentCell.RowIndex).Value)
                    '    End If

            End Select
            Select Case ColIndex
                Case ConstPAmount, ConstFCAmount, ConstFCName, ConstFCRate, ConstDtype
                    assaignTotal()
            End Select
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
            With grdpayment
                For i = 0 To grdpayment.Rows.Count - 1
                    If i = .CurrentRow.Index Then
                        currentAmt = CDbl(SrchText)
                    Else
                        currentAmt = 0
                    End If
                    If IsDBNull(.Item(ConstPAmount, i).Value) Then .Item(ConstPAmount, i).Value = 0
                    If .Item(ConstDtype, i).Value = "Dr" Then
                        ttlDebit = ttlDebit + IIf(CDbl(.Item(ConstPAmount, i).Value) = 0, currentAmt, CDbl(.Item(ConstPAmount, i).Value))
                    Else
                        ttlCredit = ttlCredit + IIf(CDbl(.Item(ConstPAmount, i).Value) = 0, currentAmt, CDbl(.Item(ConstPAmount, i).Value))
                    End If

                Next
            End With
            With grdVoucher
                For i = 0 To .Rows.Count - 1
                    If .Item(_vTag, i).Value = "Y" Then
                        If IsDBNull(.Item(_vAssign, i).Value) Then .Item(_vAssign, i).Value = 0
                        If .Item(_vType, i).Value = "Dr" Then
                            ttlDebit = ttlDebit + CDbl(.Item(_vAssign, i).Value)
                        Else
                            ttlCredit = ttlCredit + CDbl(.Item(_vAssign, i).Value)
                        End If
                    End If
                Next
            End With
            SrchText = ""
            currentAmt = 0
            lblTlDebit.Text = Format(ttlDebit, lnumformat)
            lblcredit.Text = Format(ttlCredit, lnumformat)
            lbldiff.Text = Format(CDbl(lblTlDebit.Text) - CDbl(lblcredit.Text), lnumformat)
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
                        txtSuppName.Focus()
                        grdpayment.ClearSelection()
                        Exit Sub
                    End If
                    If grdpayment.CurrentRow.Index = 0 Then grdpayment.Tag = grdpayment.Item(ConstDtype, grdpayment.CurrentCell.RowIndex).Value
                    plsrch.Visible = False
                    BeginEdit()
                End With
            ElseIf e.KeyCode = Keys.Up Or e.KeyCode = Keys.PageUp Then
                If grdpayment.RowCount = 0 Then Exit Sub
                If plsrch.Visible Then
                    doSelect(0)
                    Exit Sub
                End If
            ElseIf e.KeyCode = Keys.Down Or e.KeyCode = Keys.PageDown Then
                If grdpayment.RowCount = 0 Then Exit Sub
                If plsrch.Visible Then
                    doSelect(1)
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

                    grdpayment.Item(ConstAlias, grdpayment.CurrentCell.RowIndex).Value = IIf(ItmFlds(1) IsNot Nothing, ItmFlds(1), "")
                    SrchText = IIf(ItmFlds(1) IsNot Nothing, ItmFlds(1), strGridSrchString)
                    Dim s As String = IIf(ItmFlds(1) IsNot Nothing, ItmFlds(1), "")
                    grdpayment.Item(ConstName, grdpayment.CurrentCell.RowIndex).Value = ItmFlds(0)
                    grdpayment.Item(ConstAccountNo, grdpayment.CurrentCell.RowIndex).Value = IIf(ItmFlds(3) IsNot Nothing, ItmFlds(3), 0)
                Case 3 'FC
                    grdpayment.Item(ConstFCName, grdpayment.CurrentCell.RowIndex).Value = IIf(ItmFlds(0) IsNot Nothing, ItmFlds(0), "")
                    SrchText = IIf(ItmFlds(0) IsNot Nothing, ItmFlds(0), strGridSrchString)
                    grdpayment.Item(ConstFCRate, grdpayment.CurrentCell.RowIndex).Value = IIf(ItmFlds(1) IsNot Nothing, ItmFlds(1), 0)
                    grdpayment.Item(ConstFCDec, grdpayment.CurrentCell.RowIndex).Value = IIf(ItmFlds(4) IsNot Nothing, ItmFlds(4), 2)
                    With grdpayment
                        If Not IsDBNull(.Item(ConstFCAmount, .CurrentRow.Index).Value) And Not IsDBNull(.Item(ConstFCRate, .CurrentRow.Index).Value) Then
                            .Item(ConstPAmount, .CurrentRow.Index).Value = CDbl(.Item(ConstFCAmount, .CurrentRow.Index).Value) * IIf(CDbl(.Item(ConstFCRate, .CurrentCell.RowIndex).Value) > 0, CDbl(.Item(ConstFCRate, .CurrentCell.RowIndex).Value), 1)
                        End If
                    End With
                Case 4 'Job
                    grdpayment.Item(ConstJob, grdpayment.CurrentCell.RowIndex).Value = ItmFlds(0)
                    SrchText = IIf(ItmFlds(0) IsNot Nothing, ItmFlds(0), strGridSrchString)
                Case 5 'Pdc cust name
                    grdpayment.Item(ConstPdcCustAc, grdpayment.CurrentCell.RowIndex).Value = ItmFlds(1)
                    SrchText = IIf(ItmFlds(1) IsNot Nothing, ItmFlds(1), strGridSrchString)
                    grdpayment.Item(ConstPdcCustAcno, grdpayment.CurrentCell.RowIndex).Value = IIf(ItmFlds(3) IsNot Nothing, ItmFlds(3), 0)
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
        If _NewRw Then _NewRw = False : Exit Sub
        Try
            If grdpayment.Rows.Count = 0 Then Exit Sub
            BeginEdit()
            If plsrch.Visible Then plsrch.Visible = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ldTrans()
        Dim dttable As DataTable
        If Val(txtSuppName.Tag) = 0 Then GoTo ext
        dttable = _objTr.returnldTrans(Val(txtSuppName.Tag), IIf(isModi, 1, 0), loadedTrId)
        dtSetoffTable.Clear()
        If dttable.Rows.Count > 0 Then
            Dim i As Integer
            Dim Bal As Double
            Dim Credit As Double
            Dim Debit As Double
            Dim Added As Boolean
            Dim PRef As String = ""
            Dim dtRow As DataRow
            dtRow = dtSetoffTable.NewRow
            For i = 0 To dttable.Rows.Count - 1
                Dim s As String = UCase(dttable(i)("Reference"))

                If UCase(PRef) <> UCase(dttable(i)("Reference")) Then
                    If Added Then
                        dtSetoffTable.Rows.Add(dtRow)
                        dtRow = dtSetoffTable.NewRow
                    End If
                    Added = True
                    Bal = 0 'IIf(Rs!DealType = "D", 1, -1) * Rs!DealAmt
                    Debit = 0
                    Credit = 0
                    If Val(dttable(i)("CurrRate") & "") = 0 Then dttable(i)("CurrRate") = 1
                    If Val(dttable(i)("Amt") & "") = 0 Then dttable(i)("Amt") = 0
                    PRef = dttable(i)("Reference")
                    dtRow("JVDate") = dttable(i)("JVDate")
                    dtRow("Reference") = dttable(i)("Reference")
                    dtRow("EntryRef") = dttable(i)("EntryRef")
                    dtRow("CurrencyCode") = Trim(dttable(i)("CurrencyCode") & "")
                    dtRow("Rate") = dttable(i)("CurrRate")
                    dtRow("jvnum") = dttable(i)("jvnum")
                    'dtRow("LpoNo") = dttable(i)("LpoNo")
                    'dtRow("LpoDate") = dttable(i)("LpoDate") 
                    dtRow("JobCode") = dttable(i)("JobCode")
                    If IsDBNull(dttable(i)("Fcdec")) Then
                        dtRow("Fcdec") = 2
                    Else
                        dtRow("Fcdec") = dttable(i)("Fcdec")
                    End If
                Else
                    If dttable(i)("DealType") = "C" Then
                        dtRow("JVDate") = dttable(i)("JVDate")
                        dtRow("Reference") = dttable(i)("Reference")
                        dtRow("jvnum") = dttable(i)("jvnum")
                    End If
                End If
                Bal = Bal + IIf(UCase(dttable(i)("DealType")) = "D", 1, -1) * dttable(i)("Amt")
                Debit = Debit + IIf(UCase(dttable(i)("DealType")) = "D", 1, 0) * dttable(i)("Amt")
                Credit = Credit + IIf(UCase(dttable(i)("DealType")) = "C", 1, 0) * dttable(i)("Amt")
                dtRow("Type") = IIf(Bal < 0, "D", "C") & "r"
                dtRow("Tag") = ""
                If Val(dttable(i)("CurrRate") & "") = 0 Then dttable(i)("CurrRate") = 1
                dtRow("Balance") = Format(Bal / CDbl(dttable(i)("CurrRate")), "#,##" & lnumformat)
                dtRow("InvAmt") = Format(IIf(Credit > Debit, Credit, Credit) / CDbl(dttable(i)("CurrRate")), "#,##" & lnumformat)
                dtRow("SetOffAmt") = Format(IIf(Credit > Debit, Debit, Debit) / CDbl(dttable(i)("CurrRate")), "#,##" & lnumformat)
            Next
            If Added Then dtSetoffTable.Rows.Add(dtRow)
        End If
        dtSetoffTable.DefaultView.Sort = "JVDate ASC"
        grdVoucher.DataSource = dtSetoffTable
        SetGridTr()
        If isModi Then
            ldAssgned()
        End If

        assaignTotal()
ext:
    End Sub

    Private Sub ldAssgned()
        Dim i As Integer
        Dim s As Double
        With grdVoucher
            For i = 0 To .Rows.Count - 1
                If i > .RowCount - 1 Then Exit Sub
                'If Mid(.Item(_vRef, i).Value, 1, 5) = "I3788" Then
                '    Dim a As String = .Item(_vRef, i).Value
                '    MsgBox("")
                'End If
                s = getSum(.Item(_vRef, i).Value)
                If s = 0 Then GoTo nxt
                grdVoucher.Item(_vAssign, i).Value = CDbl(Math.Abs(s))
                .Item(_vTag, i).Value = IIf(s, "Y", "")
                'If Val(.Item(_vSetOffAmt, i).Value) > 0 Then
                '    .Item(_vSetOffAmt, i).Value = CDbl(.Item(_vSetOffAmt, i).Value) - Math.Abs(s)
                'End If

                s = CDbl(.Item(_vInvAmt, i).Value) - CDbl(.Item(_vSetOffAmt, i).Value) 'IIf(s = 0, CDbl(.Item(_vSetOffAmt, i).Value), s)
                .Item(_vBalance, i).Value = s * -1
                '.Item(_vType, i).Value = IIf(s < 0, "Dr", "Cr")
nxt:
                If CDbl(.Item(_vBalance, i).Value) = 0 Then
                    .Rows.RemoveAt(i)
                    i = i - 1
                End If
            Next i
        End With
    End Sub
    Private Function getSum(ByVal Res As String) As Double
        Dim i As Integer
        getSum = 0
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable(getAccTrDetbyloadedTrId(loadedTrId, " AND DealAmt>0"))
        For i = 0 To dt.Rows.Count - 1
            If UCase(Trim(dt(i)("Reference") & "")) = UCase(Trim(Res)) Then
                If Val(dt(i)("CurrRate") & "") = 0 Then dt(i)("CurrRate") = 1
                getSum = getSum + CDbl(dt(i)("dealAmt")) * IIf(dt(i)("dealAmt") > 0, 1, -1) / dt(i)("CurrRate")
            End If
        Next i
    End Function
    Private Sub SetGridTr()
        With grdVoucher
            .ReadOnly = False
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeColumns = True
            .AllowUserToResizeRows = False
            .ColumnHeadersHeight = 100
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersDefaultCellStyle.BackColor = Color.Olive
            .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.5!, System.Drawing.FontStyle.Regular)
            .RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 9.0!)

            .Columns(_vJVDate).HeaderText = "Inv.Date"
            .Columns(ConstAlias).Width = 100
            .Columns(ConstAlias).SortMode = DataGridViewColumnSortMode.NotSortable

            .Columns(_vRef).HeaderText = "Reference"
            .Columns(_vRef).Width = 100
            .Columns(_vRef).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(_vRef).ReadOnly = True

            .Columns(_vTag).HeaderText = "Tag"
            .Columns(_vTag).Width = 30
            .Columns(_vTag).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(_vTag).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(_vTag).DefaultCellStyle.BackColor = Color.LightYellow
            .Columns(_vTag).ReadOnly = True

            .Columns(_vType).HeaderText = "Type"
            .Columns(_vType).Width = 45
            .Columns(_vType).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(_vType).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(_vType).ReadOnly = True

            .Columns(_vAssign).HeaderText = "Assign.Amt(+*)"
            .Columns(_vAssign).Width = 120
            .Columns(_vAssign).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(_vAssign).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(_vAssign).DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns(_vBalance).HeaderText = "Balance"
            .Columns(_vBalance).Width = 120
            .Columns(_vBalance).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(_vBalance).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(_vBalance).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(_vBalance).DefaultCellStyle.BackColor = Color.LightGreen
            .Columns(_vBalance).ReadOnly = True

            .Columns(_vInvAmt).HeaderText = "Inv.Amount"
            .Columns(_vInvAmt).Width = 120
            .Columns(_vInvAmt).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(_vInvAmt).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(_vInvAmt).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(_vInvAmt).ReadOnly = True

            .Columns(_vSetOffAmt).HeaderText = "Set Off Amt."
            .Columns(_vSetOffAmt).Width = 100
            .Columns(_vSetOffAmt).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(_vSetOffAmt).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(_vSetOffAmt).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(_vSetOffAmt).ReadOnly = True

            .Columns(_vFCamt).HeaderText = "FC Amount"
            .Columns(_vFCamt).Width = 80
            .Columns(_vFCamt).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(_vFCamt).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(_vFCamt).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(_vFCamt).Visible = False

            .Columns(_vCurrencyCode).HeaderText = "FC"
            .Columns(_vCurrencyCode).Width = 40
            .Columns(_vCurrencyCode).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(_vCurrencyCode).DefaultCellStyle.BackColor = Color.Yellow
            .Columns(_vCurrencyCode).ReadOnly = True
            .Columns(_vCurrencyCode).Visible = True

            .Columns(_vRate).HeaderText = "FC Rate"
            .Columns(_vRate).Width = 80
            .Columns(_vRate).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(_vRate).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(_vRate).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(_vRate).ReadOnly = True
            .Columns(_vRate).Visible = False

            .Columns(_vEntryRef).HeaderText = "Description"
            .Columns(_vEntryRef).Width = 150
            .Columns(_vEntryRef).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(_vEntryRef).ReadOnly = True

            .Columns(_vFcdec).HeaderText = "Description"
            .Columns(_vFcdec).Width = 100
            .Columns(_vFcdec).Visible = False

            .Columns(_vFcdec).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(_vFcdec).Visible = False
            .Columns("Jvnum").HeaderText = "PInv NO"
            .Columns("Jvnum").ReadOnly = True

            .Columns(_vLpo).HeaderText = "LPO No"
            .Columns(_vLpo).ReadOnly = True
            .Columns(_vLpo).Visible = False

            .Columns("Lpodate").HeaderText = "LPO Date"
            .Columns("Lpodate").ReadOnly = True
            .Columns("Lpodate").Visible = False
        End With
    End Sub

    Private Sub txtCr_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCr.Validated

    End Sub

    Private Sub grdVoucher_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdVoucher.CellFormatting
        If e.ColumnIndex = _vRate Then
            If IsDBNull(grdVoucher.Item(_vFcdec, e.RowIndex).Value) Then grdVoucher.Item(_vFcdec, e.RowIndex).Value = 2
            e.Value = String.Format("{0:F" & Val(grdVoucher.Item(_vFcdec, e.RowIndex).Value) & "}", e.Value)
        End If
    End Sub

    Private Sub grdVoucher_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellValidated
        If chgbyprg Then Exit Sub
        Select Case e.ColumnIndex
            Case _vAssign, _vFCamt
                assaignTotal()
        End Select
        SrchText = ""
    End Sub
    Private Sub Textbox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim col As Integer
        If chgbyprg Then Exit Sub
        Dim MyCtrl As TextBox = sender
        ChgId = True
        col = grdVoucher.CurrentCell.ColumnIndex
        If chgbyprg Then Exit Sub
        chgbyprg = True
        _srchOnce = False
        If col = _vAssign Then
            With grdVoucher
                If .Rows.Count > 0 Then
                    If Not IsDBNull(.Item(_vRate, .CurrentRow.Index).Value) And MyCtrl.Text <> "" Then
                        .Item(_vFCamt, .CurrentRow.Index).Value = CDbl(MyCtrl.Text) / IIf(CDbl(.Item(_vRate, .CurrentRow.Index).Value) > 0, CDbl(.Item(_vRate, .CurrentRow.Index).Value), 1)
                    Else
                        If MyCtrl.Text <> "" Then .Item(_vFCamt, .CurrentRow.Index).Value = CDbl(MyCtrl.Text)
                    End If
                End If
            End With
        ElseIf col = _vFCamt Then
            With grdVoucher
                If .Rows.Count > 0 Then
                    If Not IsDBNull(.Item(_vRate, .CurrentRow.Index).Value) And MyCtrl.Text <> "" Then
                        .Item(_vAssign, .CurrentRow.Index).Value = CDbl(MyCtrl.Text) * IIf(CDbl(.Item(_vRate, .CurrentRow.Index).Value) > 0, CDbl(.Item(_vRate, .CurrentRow.Index).Value), 1)
                    Else
                        If MyCtrl.Text <> "" Then .Item(_vAssign, .CurrentRow.Index).Value = CDbl(MyCtrl.Text)
                    End If
                End If
            End With
        End If
        chgbyprg = False
    End Sub

    Private Sub grdpayment_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdpayment.CellContentClick

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
                    txtSuppName.Focus()
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

    Private Sub cmbfc_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbfc.SelectedIndexChanged
        returnFcrt()
    End Sub
    Private Sub returnFcrt()
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
            txtSuppName.Focus()
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
    Public Sub loadFromJob()
        chgsup = True
        txtSuppName_Validated(txtSuppName, New System.EventArgs)
    End Sub
End Class