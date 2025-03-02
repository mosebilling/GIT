'Imports App_Properties
Public Class AdvanceSetoff
    Dim istext As Boolean
    Dim isCust As Boolean
    Dim chgbyprg As Boolean
    Dim _srchOnce As Boolean
    Dim _srchTxtId As Byte
    Dim _srchIndexId As Byte
    Dim strGridSrchString As String
    Dim lstKey As Keys
    Dim StrAccMastSrch As String
    Private MyActiveControl As New Object
    'object declarations
    Dim _objcmnbLayer As New clsCommon_BL
    Dim _objAccbLayer As New clsAccountTransaction
    'Private _objTr As New clsAccountTransaction
    'Tr datagrid
    Private Const _vJVDate = 0
    Private Const _vRef = 1
    Private Const _vTag = 2
    Private Const _vType = 3
    Private Const _vAssign = 4
    Private Const _vBalance = 5
    Private Const _vInvAmt = 6
    Private Const _vSetOffAmt = 7
    Private Const _vFCamt = 8
    Private Const _vCurrencyCode = 9
    Private Const _vRate = 10
    Private Const _vInv = 11
    Private Const _vEntryRef = 12
    Private Const _vFDec = 13
    'Private Const _vUnqNo = 12
    'Private Const _vLinkNo = 13
    'form objects
    Private WithEvents fMList As Mlistfrm
    Private WithEvents fSelect As Selectfrm
    Dim dtSetoffTable As New DataTable

    Private Sub AdvanceSetoff_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Keys.Escape) Then
            Me.Close()

        End If
    End Sub
  
    Private Sub AdvanceSetoff_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CreateSetoffTable(dtSetoffTable)
        grdTr.DataSource = dtSetoffTable
        SetGridTr()
        'StrAccMastSrch = "SELECT AccDescr, Alias, ClosingBal As Balance, OpnBal, DeptId, CurrencyCode, CurrencyRate, AccountNo, S1AccId, AccSetId, GS As GrpSetOn, ForJobYN, SlsmanId, JobAssgble,AreaCode, TermsId, BranchId, CreditLimit, Hide, ClosingPDCRAmt, PriceGrp, LPOOptional,AccPhoto FROM" & _
        '                   "(SELECT AccDescr,Alias, ClosingBal,OpnBal,DeptId,AccMast.CurrencyCode,CurrencyRate,AccMast.AccountNo,AccMast.S1AccId,AccSetId, isNull(GrpSetOn, '') GS,ForJobYN, SlsmanId, JobAssgble, AreaCode, TermsId, AccMast.BranchId, CreditLimit, ClosingPDCRAmt, Hide, PriceGrp, LPOOptional,AccPhoto FROM AccMast INNER JOIN S1AccHd ON S1AccHd.S1AccId = AccMast.S1AccId LEFT JOIN CurrencyTb ON AccMast.CurrencyCode = CurrencyTb.CurrencyCode) TR"
        StrAccMastSrch = "SELECT AccDescr, Alias, ClosingBal As Balance, OpnBal, accid, S1AccHd.S1AccId, AccSetId, GrpSetOn FROM" & _
                " AccMast INNER JOIN S1AccHd ON S1AccHd.S1AccId = AccMast.S1AccId "
    End Sub
    Private Sub SetGridTr()
        With grdTr
            .ReadOnly = False
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = True
            .EditMode = DataGridViewEditMode.EditProgrammatically
            '.ColumnCount = 22
            .ColumnHeadersHeight = 25
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            '.ColumnHeadersDefaultCellStyle.BackColor = Color.Olive
            .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular)
            .RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.0!)
            .Columns(_vJVDate).HeaderText = "Inv.Date"
            .Columns(_vJVDate).Width = 100
            .Columns(_vJVDate).SortMode = DataGridViewColumnSortMode.NotSortable

            .Columns(_vRef).HeaderText = "Reference"
            .Columns(_vRef).Width = 100
            .Columns(_vRef).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(_vRef).ReadOnly = True

            .Columns(_vTag).HeaderText = "Tag"
            .Columns(_vTag).Width = 30
            .Columns(_vTag).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(_vTag).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
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
            .Columns(_vBalance).ReadOnly = True

            .Columns(_vInvAmt).HeaderText = "Inv.Amount"
            .Columns(_vInvAmt).Width = 120
            .Columns(_vInvAmt).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(_vInvAmt).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(_vInvAmt).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(_vInvAmt).ReadOnly = True

            .Columns(_vSetOffAmt).HeaderText = "Set Off Amt."
            .Columns(_vSetOffAmt).Width = 120
            .Columns(_vSetOffAmt).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(_vSetOffAmt).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(_vSetOffAmt).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(_vSetOffAmt).ReadOnly = True

            .Columns(_vFCamt).HeaderText = "FC Amount"
            .Columns(_vFCamt).Width = 120
            .Columns(_vFCamt).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(_vFCamt).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(_vFCamt).DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns(_vCurrencyCode).HeaderText = "FC"
            .Columns(_vCurrencyCode).Width = 70
            .Columns(_vCurrencyCode).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(_vCurrencyCode).ReadOnly = True

            .Columns(_vRate).HeaderText = "FC Rate"
            .Columns(_vRate).Width = 100
            .Columns(_vRate).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(_vRate).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(_vRate).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(_vRate).ReadOnly = True

            .Columns(_vInv).HeaderText = "Inv.No"
            .Columns(_vInv).Width = 70
            .Columns(_vInv).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(_vInv).ReadOnly = True

            .Columns(_vEntryRef).HeaderText = "Description"
            .Columns(_vEntryRef).Width = 150
            .Columns(_vEntryRef).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(_vEntryRef).ReadOnly = True

            .Columns(_vFDec).HeaderText = "Fcdec"
            .Columns(_vFDec).Width = Me.Width * 5 / 100   '100
            .Columns(_vFDec).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(_vFDec).Visible = False

        End With
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
                    Case 0, 1
                        SetFmlist(fMList, 13)
                        'Case 2, 3
                        '    SetFmlist(fMList, 15)
                End Select
                fMList.loc = PopupLoc
                fMList.Show(fMainForm)
                fMList.Visible = True
            End If
        End If
        Select Case _srchTxtId
            Case 0   ' code
                fMList.SearchIndex = 1
                fMList.SearchIndexDescr = 0
                fMList_doFocus()
                fMList.Search(txtCode.Text)
                txtaccount.Text = fMList.AssignList(txtCode, lstKey, chgbyprg)
            Case 1   ' name
                fMList.SearchIndex = 0
                fMList.SearchIndexDescr = 1
                fMList_doFocus()
                fMList.Search(txtaccount.Text)
                txtCode.Text = fMList.AssignList(txtaccount, lstKey, chgbyprg)
        End Select
        _srchOnce = True
        chgbyprg = False
    End Sub

    Private Sub fMList_doFocus() Handles fMList.doFocus
        If TypeOf (MyActiveControl) Is TextBox Then
            MyActiveControl.focus()
        End If
    End Sub

    Private Sub fMList_doSelect(ByVal ItmFlds() As String) Handles fMList.doSelect
        chgbyprg = True
        Select Case _srchTxtId
            Case 0, 1
                txtCode.Text = ItmFlds(1)
                txtaccount.Text = ItmFlds(0)
                txtCode.Tag = ItmFlds(3)
        End Select
        chgbyprg = False
    End Sub

    Private Sub txtaccount_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtaccount.KeyDown, txtCode.KeyDown
        lstKey = e.KeyCode
        Dim MyCtrl As TextBox = sender
        If e.KeyCode = Keys.Return Then
            btnLoad.Focus()
        ElseIf e.KeyCode = Keys.F2 Then
            Select Case MyCtrl.Name
                Case "txtCode", "txtaccount"
                    ldSelect(1)
            End Select
        ElseIf e.KeyCode = Keys.Up Or e.KeyCode = Keys.PageUp Then
            If fMList.Visible Then
                fMList.MoveUp(MyCtrl.Text)
                Exit Sub
            End If
        ElseIf e.KeyCode = Keys.Down Or e.KeyCode = Keys.PageDown Then
            If fMList.Visible Then
                fMList.MoveDown(MyCtrl.Text)
                Exit Sub
            End If
        End If
    End Sub

    Private Sub txtCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged, txtaccount.TextChanged
        If chgbyprg = True Then Exit Sub
        Dim myCtrl As Control = sender
        _srchTxtId = IIf(myCtrl.Name = "txtCode", 0, 1)
        _srchOnce = False
        ShowFmlist(sender)
    End Sub
    Private Sub Valid()
        chgbyprg = True
        Dim _rData As DataTable
        If Not fMList Is Nothing Then
            If fMList.Visible Then fMList.Visible = False
        End If
        Select Case _srchTxtId
            Case 0, 1
                _rData = EntriesValidation(txtCode.Text, 1)
                If _rData.Rows.Count > 0 Then
                    txtCode.Text = _rData(0)("Alias").ToString
                    txtaccount.Text = _rData(0)("AccDescr").ToString
                    txtCode.Tag = _rData(0)("accid").ToString
                    _rData.Clear()
                Else
                    txtCode.Text = ""
                    txtaccount.Text = ""
                    txtCode.Tag = ""
                End If
        End Select
        CustomerStatus
        chgbyprg = False
    End Sub
    Private Function CustomerStatus() As Boolean
        Dim _rData As DataTable
        If Val(txtCode.Tag) > 0 Then
            _rData = _objcmnbLayer._fldDatatable(StrAccMastSrch & " WHERE accid = " & Val(txtCode.Tag) & " ORDER BY AccDescr")
            isCust = False
            If _rData.Rows.Count > 0 Then
                If UCase(Trim(_rData(0)("GrpSetOn"))) = UCase("Customer") Then
                    isCust = True
                End If
                If UCase(Trim(_rData(0)("GrpSetOn"))) = "CUSTOMER" Or UCase(Trim(_rData(0)("GrpSetOn"))) = "SUPPLIER" Then
                    CustomerStatus = True
                End If
            End If
        End If
    End Function
    Private Sub txtaccount_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtaccount.Validated, txtCode.Validated
        Valid()
        If _srchTxtId = 0 Then txtCode.SelectionLength = 0
        If _srchTxtId = 1 Then txtaccount.SelectionLength = 0
        ldTrans()
    End Sub
    Private Sub ldSelect(ByVal BVal As Single)
        fSelect = New Selectfrm
        SetForm(fSelect, BVal)
        fSelect.Show()
    End Sub

    Private Sub fSelect_doSelect(ByVal KeyId As String, ByVal strFld1 As String, ByVal strFld2 As String) Handles fSelect.doSelect
        chgbyprg = True
        If _srchTxtId = 0 Or _srchTxtId = 1 Then
            txtCode.Text = strFld2
            txtaccount.Text = strFld1
            txtCode.Tag = KeyId
            btnLoad_Click(btnLoad, New System.EventArgs)
        End If
        chgbyprg = False
    End Sub
    '    Private Sub ldTrans()
    '        Dim dttable As DataTable
    '        If Val(txtCode.Tag) = 0 Then GoTo ext
    '        dttable = _objAccbLayer.returnldTrans(Val(txtCode.Tag), 0, 0)
    '        dtSetoffTable.Clear()
    '        If dttable.Rows.Count > 0 Then
    '            Dim i As Integer
    '            Dim Bal As Double
    '            Dim Credit As Double
    '            Dim Debit As Double
    '            Dim Added As Boolean
    '            Dim PRef As String = ""
    '            Dim dtRow As DataRow
    '            dtRow = dtSetoffTable.NewRow
    '            For i = 0 To dttable.Rows.Count - 1
    '                Dim s As String = UCase(dttable(i)("Reference"))
    '                If s = "" Then dttable(i)("Reference") = "ON/AC"
    '                dttable(i)("DealType") = ucase(dttable(i)("DealType"))
    '                If UCase(PRef) <> UCase(dttable(i)("Reference")) Then
    '                    If Added Then
    '                        dtSetoffTable.Rows.Add(dtRow)
    '                        dtRow = dtSetoffTable.NewRow
    '                    End If
    '                    Added = True
    '                    Bal = 0 'IIf(Rs!DealType = "D", 1, -1) * Rs!DealAmt
    '                    Debit = 0
    '                    Credit = 0
    '                    If Val(dttable(i)("CurrRate") & "") = 0 Then dttable(i)("CurrRate") = 1
    '                    If Val(dttable(i)("Amt") & "") = 0 Then dttable(i)("Amt") = 0
    '                    PRef = dttable(i)("Reference")
    '                    dtRow("JVDate") = dttable(i)("JVDate")
    '                    dtRow("Reference") = dttable(i)("Reference")
    '                    dtRow("EntryRef") = dttable(i)("EntryRef")
    '                    dtRow("CurrencyCode") = Trim(dttable(i)("CurrencyCode") & "")
    '                    dtRow("Rate") = dttable(i)("CurrRate")
    '                    dtRow("InvNo") = dttable(i)("jvnum")
    '                    'dtRow("LpoNo") = dttable(i)("LpoNo")
    '                    'dtRow("LpoDate") = dttable(i)("LpoDate") 
    '                    'dtRow("JobCode") = dttable(i)("JobCode")
    '                    If IsDBNull(dttable(i)("Fcdec")) Then
    '                        dtRow("Fcdec") = 2
    '                    Else
    '                        dtRow("Fcdec") = dttable(i)("Fcdec")
    '                    End If
    '                Else
    '                    If isCust And dttable(i)("DealType") = "D" Then
    '                        dtRow("JVDate") = dttable(i)("JVDate")
    '                        'dtRow("Reference") = dttable(i)("Reference")
    '                        'dtRow("InvNo") = dttable(i)("jvnum")
    '                    End If
    '                    If Not isCust And dttable(i)("DealType") = "C" Then
    '                        dtRow("JVDate") = dttable(i)("JVDate")
    '                        'dtRow("Reference") = dttable(i)("Reference")
    '                        'dtRow("InvNo") = dttable(i)("jvnum")
    '                    End If
    '                End If
    '                Bal = Bal + IIf(dttable(i)("DealType") = "D", 1, -1) * dttable(i)("Amt")
    '                Debit = Debit + IIf(dttable(i)("DealType") = "D", 1, 0) * dttable(i)("Amt")
    '                Credit = Credit + IIf(dttable(i)("DealType") = "C", 1, 0) * dttable(i)("Amt")
    '                dtRow("Type") = IIf(Bal < 0, "D", "C") & "r"
    '                dtRow("Tag") = ""
    '                If Val(dttable(i)("CurrRate") & "") = 0 Then dttable(i)("CurrRate") = 1
    '                dtRow("Balance") = Format(Bal / CDbl(dttable(i)("CurrRate")), "#,##" & numFormat)
    '                dtRow("InvAmt") = Format(IIf(Credit > Debit, Debit, Debit) / CDbl(dttable(i)("CurrRate")), "#,##" & numFormat)
    '                dtRow("SetOffAmt") = Format(IIf(Credit > Debit, Credit, Credit) / CDbl(dttable(i)("CurrRate")), "#,##" & numFormat)
    '            Next
    '            If Added Then dtSetoffTable.Rows.Add(dtRow)
    '        End If
    '        If isCust Then
    '            dtSetoffTable.DefaultView.Sort = "Type desc,JVDate"
    '        End If

    '        grdTr.DataSource = dtSetoffTable
    '        SetGridTr()

    '        assaignTotal()
    'ext:
    '    End Sub
    Private Sub ldTrans()
        Dim dttable As DataTable
        Dim qry As String
        If Val(txtCode.Tag) = 0 Then Exit Sub
        'AL JAMHOUR AUTO SPARE PARTS TR.
        qry = "SELECT Tr.* FROM (SELECT Reference, EntryRef, JVDate, " & _
                                              "'DealType'=CASE WHEN DealAmt>=0 THEN 'D' WHEN DealAmt<0 THEN 'C' END," & _
                                              " abs(DealAmt) Amt, AccTrDet.CurrencyCode, CurrRate,JVType+'/'+PreFix+convert( nvarchar,JVNum) InvNo," & _
                                              " 'Ord'=CASE WHEN JVType ='IP' OR JVType='PI' THEN 1" & _
                                              " WHEN JVType ='IS' OR JVType='SI'THEN 2" & _
                                              " WHEN JVType ='PR' OR JVType= 'DN' THEN 3" & _
                                              " WHEN JVType ='SR' OR JVType='CN' THEN 4" & _
                                              " WHEN JVType ='PV' OR JVType= 'RV' THEN 6" & _
                                              " ELSE 5 END,FCDec " & _
                                              " FROM AccTrDet LEFT JOIN AccTrCmn ON AccTrDet.LinkNo=AccTrCmn.LinkNo" & _
                                              " LEFT JOIN (SELECT CurrencyCode,[Decimal Places] FCDec FROM CurrencyTb)Cur ON Cur.CurrencyCode=AccTrDet.CurrencyCode" & _
                                              " WHERE " & IIf(UsrBr = "", "", " CmnBrId IN('" & UsrBr & "','') AND") & "  AccountNo = " & Val(txtCode.Tag) & _
                                              " UNION ALL SELECT 'OB-PRIOR','Opening Balance', '" & Format(DateValue(DateAdd(DateInterval.Day, -1, DateFrom)), "yyyy/MM/dd") & "'," & _
                                              "'DealType'=CASE WHEN OpnBal>0 THEN 'D' WHEN OpnBal<0 THEN 'C' END," & _
                                              " Abs(OpnBal), AccMast.CurrencyCode, CurrencyRate,'', 0,[Decimal Places]" & _
                                              " FROM AccMast LEFT JOIN CurrencyTb ON AccMast.CurrencyCode = CurrencyTb.CurrencyCode WHERE accid = " & Val(txtCode.Tag) & " AND IsOBDet = 0 AND OpnBal <> 0) As Tr" & _
                                              " INNER JOIN (SELECT Reference FROM (SELECT Reference, 'DealType'=CASE WHEN DealAmt>0 THEN 'D' WHEN DealAmt<0 THEN 'C' END, DealAmt FROM AccTrDet LEFT JOIN AccTrCmn ON AccTrDet.LinkNo=AccTrCmn.LinkNo " & _
                                              " WHERE " & IIf(UsrBr = "", "", " CmnBrId IN('" & UsrBr & "','') AND") & " AccountNo = " & Val(txtCode.Tag) & " UNION ALL SELECT 'OB-PRIOR','DealType'=CASE WHEN OpnBal>0 THEN 'D' WHEN OpnBal<0 THEN 'C' END, OpnBal FROM AccMast " & _
                                              " WHERE accid = " & Val(txtCode.Tag) & " AND IsOBDet = 0 AND OpnBal <> 0)T GROUP BY Reference" & _
                                              " HAVING abs(Sum(DealAmt)) >=0." & IIf(NoOfDecimal = 0, 0, StrDup(NoOfDecimal - 1, "0") & "1") & ") As Q ON Q.Reference = Tr.Reference WHERE Amt>0 ORDER BY DealType " & IIf(Not isCust, "DESC", "") & ",JVDate"
        'dttable = _objcmnbLayer._fldDatatable(qry)
        dtSetoffTable.Clear()
        dttable = _objAccbLayer.returnldTrans(Val(txtCode.Tag), 0, 0)
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
                If UCase(PRef) <> UCase(dttable(i)("Reference")) Then
                    If Added Then
                        dtSetoffTable.Rows.Add(dtRow)
                        dtRow = dtSetoffTable.NewRow
                    End If
                    Added = True
                    Bal = 0 'IIf(Rs!DealType = "D", 1, -1) * Rs!DealAmt
                    Debit = 0
                    Credit = 0
                    PRef = dttable(i)("Reference")
                    dtRow("JVDate") = dttable(i)("JVDate")
                    dtRow("Reference") = dttable(i)("Reference")
                    dtRow("EntryRef") = dttable(i)("EntryRef")
                    dtRow("CurrencyCode") = dttable(i)("CurrencyCode")
                    dtRow("Rate") = dttable(i)("CurrRate")
                    dtRow("InvNo") = dttable(i)("InvNo")
                    If IsDBNull(dttable(i)("Fcdec")) Then
                        dtRow("Fcdec") = 2
                    Else
                        dtRow("Fcdec") = dttable(i)("Fcdec")
                    End If
                Else
                    If isCust And dttable(i)("DealType") = "D" Then
                        dtRow("JVDate") = dttable(i)("JVDate")
                        'dtRow("Reference") = dttable(i)("Reference")
                        'dtRow("InvNo") = dttable(i)("jvnum")
                    End If
                    If Not isCust And dttable(i)("DealType") = "C" Then
                        dtRow("JVDate") = dttable(i)("JVDate")
                        'dtRow("Reference") = dttable(i)("Reference")
                        'dtRow("InvNo") = dttable(i)("jvnum")
                    End If

                End If
                Bal = Bal + IIf(dttable(i)("DealType") = "D", 1, -1) * dttable(i)("Amt")
                Debit = Debit + IIf(dttable(i)("DealType") = "D", 1, 0) * dttable(i)("Amt")
                Credit = Credit + IIf(dttable(i)("DealType") = "C", 1, 0) * dttable(i)("Amt")
                dtRow("Type") = IIf(Bal < 0, "C", "D") & "r"
                dtRow("Tag") = ""
                dtRow("Balance") = Format(Bal, "#,##" & numFormat)
                dtRow("Assign") = Format(0, "#,##" & numFormat)
                dtRow("InvAmt") = Format(IIf(isCust, Debit, Credit), "#,##" & numFormat)
                dtRow("SetOffAmt") = Format(IIf(isCust, Credit, Debit), "#,##" & numFormat)
                dtRow("InvNo") = dttable(i)("InvNo")
            Next
            If Added Then dtSetoffTable.Rows.Add(dtRow)
        End If
        If isCust Then
            dtSetoffTable.DefaultView.Sort = "Type ASC,JVDate ASC"
        Else
            dtSetoffTable.DefaultView.Sort = "Type DESC,JVDate ASC"
        End If
        lblrecords.Text = "Total Records Found : " & dtSetoffTable.Rows.Count & " (Unset : " & dttable.Rows.Count & ")"
        grdTr.DataSource = dtSetoffTable
    End Sub
    Private Sub CreateSetoffTable(ByRef dttable As DataTable)
        dttable.Columns.Add(New DataColumn("JVDate", GetType(Date)))
        dttable.Columns.Add(New DataColumn("Reference", GetType(String)))
        dttable.Columns.Add(New DataColumn("Tag", GetType(String)))
        dttable.Columns.Add(New DataColumn("Type", GetType(String)))
        dttable.Columns.Add(New DataColumn("Assign", GetType(Double)))
        dttable.Columns.Add(New DataColumn("Balance", GetType(Double)))
        dttable.Columns.Add(New DataColumn("InvAmt", GetType(Double)))
        dttable.Columns.Add(New DataColumn("SetOffAmt", GetType(Double)))
        dttable.Columns.Add(New DataColumn("FCAmt", GetType(Double)))
        dttable.Columns.Add(New DataColumn("CurrencyCode", GetType(String)))
        dttable.Columns.Add(New DataColumn("Rate", GetType(Double)))
        dttable.Columns.Add(New DataColumn("InvNo", GetType(String)))
        dttable.Columns.Add(New DataColumn("EntryRef", GetType(String)))
        dttable.Columns.Add(New DataColumn("FcDec", GetType(String)))
        'dttable.Columns.Add(New DataColumn("UnqNo", GetType(Long)))
        'dttable.Columns.Add(New DataColumn("LinkNo", GetType(Long)))
    End Sub

    Private Sub grdTr_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdTr.CellClick
        If grdTr.RowCount = 0 Then Exit Sub
        Dim i As Integer
        Dim dfound As Boolean
        If grdTr.Item(_vTag, e.RowIndex).Value = "" Then
            If isCust Then
                For i = 0 To grdTr.RowCount - 1
                    If grdTr.Item(_vTag, i).Value = "Y" And grdTr.Item(_vType, i).Value = "Cr" And i <> e.RowIndex Then
                        dfound = True
                        Exit For
                    End If
                Next
                If dfound And grdTr.Item(_vType, e.RowIndex).Value = "Cr" Then
                    MsgBox("For the Customer Account, Taging on multiple Credit (Cr) is not allowed.", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If
            Else
                For i = 0 To grdTr.RowCount - 1
                    If grdTr.Item(_vTag, i).Value = "Y" And grdTr.Item(_vType, i).Value = "Dr" And i <> e.RowIndex Then
                        dfound = True
                        Exit For
                    End If
                Next
                If dfound And grdTr.Item(_vType, e.RowIndex).Value = "Dr" Then
                    MsgBox("For the Supplier Account, Taging on multiple Debit (Dr) is not allowed.", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If
            End If
        End If

        If e.ColumnIndex = _vTag Then
            If IsDBNull(grdTr.Item(_vTag, e.RowIndex).Value) Then grdTr.Item(_vTag, e.RowIndex).Value = ""
            If grdTr.Item(_vTag, e.RowIndex).Value = "Y" Then
                grdTr.Item(_vTag, e.RowIndex).Value = ""
                grdTr.Item(_vAssign, e.RowIndex).Value = Format(0, "0.00")
            Else
                assaignTotal()
                FillAmt()
                If CDbl(grdTr.Item(_vAssign, grdTr.CurrentCell.RowIndex).Value) = 0 Then assignFull()
            End If
            With grdTr
                If .Rows.Count > 0 Then
                    If Not IsDBNull(.Item(_vRate, .CurrentRow.Index).Value) Then
                        .Item(_vFCamt, .CurrentRow.Index).Value = CDbl(.Item(_vAssign, .CurrentRow.Index).Value) / IIf(CDbl(.Item(_vRate, .CurrentRow.Index).Value) > 0, CDbl(.Item(_vRate, .CurrentRow.Index).Value), 1)
                    Else
                        .Item(_vFCamt, .CurrentRow.Index).Value = .Item(_vAssign, .CurrentRow.Index).Value
                    End If
                End If
            End With
            grdTr.Tag = grdTr.Item(_vType, e.RowIndex).Value
        ElseIf e.ColumnIndex = _vAssign Then
            grdTr.BeginEdit(True)
        End If
        assaignTotal()
    End Sub
    Sub assaignTotal()
        Try
            Dim i As Integer
            Dim ttlDebit As Double
            Dim ttlCredit As Double
            With grdTr
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
            lblTlDebit.Text = Format(CDbl(ttlCredit), numFormat)
            lblTlCredit.Text = Format(CDbl(ttlDebit), numFormat)
            lblDiff.Text = Format(CDbl(lblTlDebit.Text) - CDbl(lblTlCredit.Text), numFormat)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub
    Private Sub FillAmt()
        With grdTr
            If .RowCount = 0 Then Exit Sub
            If (CDbl(lblDiff.Text) > 0 And CDbl(.Item(_vBalance, .CurrentCell.RowIndex).Value) > 0) Or _
                (CDbl(lblDiff.Text) < 0 And CDbl(.Item(_vBalance, .CurrentCell.RowIndex).Value) < 0) Then
                If Math.Abs(CDbl(lblDiff.Text)) >= Math.Abs(CDbl(.Item(_vBalance, .CurrentCell.RowIndex).Value)) Then
                    .Item(_vAssign, .CurrentCell.RowIndex).Value = Format(Math.Abs(CDbl(.Item(_vBalance, .CurrentCell.RowIndex).Value)), "0.0")
                Else
                    .Item(_vAssign, .CurrentCell.RowIndex).Value = Format(Math.Abs(CDbl(lblDiff.Text)), "0.00")
                End If
                assaignTotal()
            End If
            If IsDBNull(grdTr.Item(_vAssign, grdTr.CurrentCell.RowIndex).Value) Then grdTr.Item(_vAssign, grdTr.CurrentCell.RowIndex).Value = 0
            .Item(_vTag, .CurrentCell.RowIndex).Value = IIf(.Item(_vTag, .CurrentCell.RowIndex).Value = "Y", "", "Y")
            
        End With
    End Sub
    Private Sub assignFull()
        With grdTr
            .Item(_vTag, .CurrentCell.RowIndex).Value = "Y"
            .Item(_vAssign, .CurrentCell.RowIndex).Value = Math.Abs(.Item(_vBalance, .CurrentCell.RowIndex).Value) 'Format(Math.Abs(CDbl(.Item(_vInvAmt, .CurrentCell.RowIndex).Value)), "#,##" & numFormat)
            assaignTotal()
        End With
    End Sub

    Private Sub cmdUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdate.Click
        Try
            If cmdUpdate.Enabled = False Then Exit Sub
            chgbyprg = True
            cmdUpdate.Focus()
            chgbyprg = False
            assaignTotal()
            Dim i As Integer
            Dim DrFound As Boolean
            If Not fSelect Is Nothing Then fSelect.Close() : fSelect = Nothing
            With grdTr
                If .RowCount = 0 Then
                    MsgBox("Entry not Found !!", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If
                DrFound = False
                For i = 0 To .RowCount - 1
                    If IsDBNull(.Item(_vAssign, i).Value) Then .Item(_vAssign, i).Value = 0
                    If .Item(_vType, i).Value = IIf(isCust, "Cr", "Dr") And CDbl(.Item(_vAssign, i).Value) <> 0 Then
                        If DrFound Then
                            MsgBox("For the " & IIf(isCust, "Customer", "Supplier") & " Account, Taging on multiple " & IIf(Not isCust, "Debit (Dr)", "Credit (Cr)") & " is not allowed.", MsgBoxStyle.Exclamation)
                            .Select()
                            .CurrentCell = .Item(_vAssign, i)
                            Exit Sub
                        End If
                        DrFound = True
                    End If
                Next
                If (CDbl(lblDiff.Text) <> 0 Or (CDbl(lblTlCredit.Text) = 0 And CDbl(lblTlDebit.Text) = 0)) Then
                    MsgBox("Amount is not balanced or empty entry.", MsgBoxStyle.Exclamation)
                    .Select()
                    .CurrentCell = .Item(_vAssign, 0)
                    Exit Sub
                End If
                If Not CustomerStatus() Then
                    MsgBox("Choose a valid Customer/Supplier Account.", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If
                If MsgBox("Verification is succeeded. Do you like to file it ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                    AdvTran()
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub AdvTran()
        Dim i As Integer
        With grdTr
            For i = 0 To .RowCount - 1
                If .Item(_vTag, i).Value = "Y" Then
                    If CDbl(.Item(_vAssign, i).Value) <> 0 Then
                        'find execess amount from the grid
                        'in case of customer it should be Cr otherwise it Dr
                        If (isCust And .Item(_vType, i).Value = "Cr") Or (Not isCust And .Item(_vType, i).Value = "Dr") Then
                            SetAdvTran(.Item(_vRef, i).Value)
                            Exit For
                        End If
                    End If
                End If
            Next i
        End With
        MsgBox("Succesfully Adjusted.", MsgBoxStyle.Information)
        ldTrans()
    End Sub

    Private Sub SetAdvTran(ByRef Ref As String)
        Dim i As Integer
        With grdTr
            For i = 0 To .RowCount - 1
                If .Item(_vTag, i).Value = "Y" Then
                    If CDbl(.Item(_vAssign, i).Value) <> 0 Then
                        'find pending amount from the grid
                        'in the case of customer it should be Dr otherwise it Cr
                        If (isCust And .Item(_vType, i).Value = "Dr") Or (Not isCust And .Item(_vType, i).Value = "Cr") Then
                            SaveAdvTran(Ref, .Item(_vRef, i).Value, i)
                        End If
                    End If
                End If
            Next i
        End With
    End Sub
    Private Sub SaveAdvTran(ByRef Ref As String, ByRef dueRef As String, ByRef Row As Integer)
        Dim AccTrDet As String = ""
        Dim AccTrDet1 As String = ""
        Dim dtSave As DataTable
        Dim dtTrtb As DataTable
        Dim dtRow As DataRow
        Dim i As Integer
        Dim j As Integer
        'Ref-excess amount referece
        'sRef - pending amount reference
        'Row - pending amount index
Nxt:
        If isCust Then
            AccTrDet = "SELECT * FROM AccTrDet WHERE AccountNo = " & Val(txtCode.Tag) & " AND Reference = '" & Ref & "' and DealAmt<0"
        Else
            AccTrDet = "SELECT * FROM AccTrDet WHERE AccountNo = " & Val(txtCode.Tag) & " AND Reference = '" & Ref & "' and DealAmt>0"
        End If

        AccTrDet1 = "SELECT * FROM AccTrDet"
        dtTrtb = _objcmnbLayer._fldDatatable(AccTrDet)
        If dtTrtb.Rows.Count = 0 Then GoTo ter
        dtSave = dtTrtb.Clone
        dtSave.Clear()
        With grdTr
            For i = 0 To dtTrtb.Rows.Count - 1
                If CDbl(.Item(_vAssign, Row).Value) = Math.Abs(CDbl(dtTrtb(i)("DealAmt"))) Then
                    _objcmnbLayer._saveDatawithOutParm("UPDATE AccTrDet SET Reference='" & dueRef & "' WHERE UnqNo=" & Val(dtTrtb(i)("UnqNo")))
                    '.Item(_vAssign, Row).Value = 0
                    GoTo Ter
                ElseIf CDbl(.Item(_vAssign, Row).Value) < Math.Abs(CDbl(dtTrtb(i)("DealAmt"))) Then
                    dtRow = dtSave.NewRow
                    For j = 0 To dtTrtb.Columns.Count - 1
                        If dtTrtb.Columns(i).ColumnName <> "UnqNo" Then
                            dtRow(j) = dtTrtb(0)(j)
                        End If
                    Next
                    dtRow("Reference") = dueRef
                    dtRow("DealAmt") = IIf(.Item(_vType, Row).Value = "Dr", -1, 1) * CDbl(.Item(_vAssign, Row).Value)
                    dtRow("fcamt") = dtRow("DealAmt")
                    dtSave.Rows.Add(dtRow)
                    _objcmnbLayer.__saveDataTable(AccTrDet1, dtSave)
                    _objcmnbLayer._saveDatawithOutParm("UPDATE AccTrDet SET DealAmt='" & IIf(CDbl(dtTrtb(i)("DealAmt")) < 0, -1, 1) * (Math.Abs(CDbl(dtTrtb(i)("DealAmt"))) - CDbl(.Item(_vAssign, Row).Value)) & "' WHERE UnqNo=" & Val(dtTrtb(i)("UnqNo")))
                    '.Item(_vAssign, Row).Value = 0
                    GoTo Ter
                ElseIf CDbl(.Item(_vAssign, Row).Value) > Math.Abs(CDbl(dtTrtb(i)("DealAmt"))) Then
                    .Item(_vAssign, Row).Value = CDbl(.Item(_vAssign, Row).Value) - Math.Abs(CDbl(dtTrtb(i)("DealAmt")))
                    _objcmnbLayer._saveDatawithOutParm("UPDATE AccTrDet SET Reference='" & dueRef & "' WHERE UnqNo=" & Val(dtTrtb(i)("UnqNo")))
                    GoTo Nxt
                End If
            Next
        End With
ter:
    End Sub

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        ldTrans()
    End Sub

    Private Sub grdTr_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdTr.CellValidated
        assaignTotal()
    End Sub

    Private Sub cmdHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdHelp.Click
        chgbyprg = True
        'txtaccount.Text = ""
        'txtCode.Text = ""
        'txtCode.Tag = ""
        'grdTr.Rows.Clear()
        ldTrans()
        chgbyprg = False
    End Sub

    Private Sub grdTr_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdTr.CellFormatting
        With grdTr
            If e.ColumnIndex = _vRate Then
                If IsDBNull(.Item(_vFDec, e.RowIndex).Value) Then .Item(_vFDec, e.RowIndex).Value = 2
                e.Value = String.Format("{0:F" & Val(.Item(_vFDec, e.RowIndex).Value) & "}", e.Value)
            End If
        End With
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub
End Class