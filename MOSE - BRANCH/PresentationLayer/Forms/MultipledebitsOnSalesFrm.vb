Public Class MultipledebitsOnSalesFrm
#Region "Constant Variables"
    Private Const ConstAlias = 0
    Private Const ConstName = 1
    Private Const ConstAmount = 2
    Private Const ConstReference = 3
    Private Const ConstAccountNo = 4
    Private Const ConstUnq = 5
#End Region
#Region "Public Variables"
    Public dt As DataTable
    Public customeralias As String
    Public customername As String
    Public customeraccid As Long
    Public reference As String
    Public dtSetoffTable As New DataTable
    Public lnumformat As String
    Public skipCrEntry As Boolean
    Public trtype As String
#End Region
#Region "Local Variables"
    Private chgbyprg As Boolean
    Private _srchOnce As Boolean
    Private _srchTxtId As Byte
    Private _srchIndexId As Byte
    Private MyActiveControl As New Object
    Private lstKey As Keys
    Private dtTable As DataTable
    Private iscashinvoiceonly As Boolean
#End Region
#Region "Form Objects"
    Private WithEvents fMList As Mlistfrm
#End Region
#Region "Class Objet declarations"
    Private _objcmnbLayer As New clsCommon_BL
    Private _objTr As New clsAccountTransaction
#End Region
    Private Sub getcustomerType()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT iscashinvoiceonly FROM AccMast where AccId='" & customeraccid & "'")
        If Not IsDBNull(dt(0)("iscashinvoiceonly")) Then
            iscashinvoiceonly = dt(0)("iscashinvoiceonly")

        End If

    End Sub
    Private Sub MultipledebitsOnSalesFrm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtAmt.Focus()
    End Sub

    Private Sub MultipledebitsOnSalesFrm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
    End Sub

    Private Sub MultipledebitsOnSalesFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetGridHead()
        loadMultipleDebits()
        getcustomerType()
        If trtype = "IP" Then
            crtVrs(cmbVoucherTp, 1, True)
            Label4.Text = "Advance Paid"
        Else
            crtVrs(cmbVoucherTp, 2, True)
        End If

    End Sub
    Private Function crtVrs(ByRef cmb As System.Windows.Forms.ComboBox, ByRef vrTypeNo As Byte, Optional ByRef Slct As Boolean = False, Optional ByVal isaddnull As Boolean = False) As Boolean
        Dim i As Integer
        dtTable = _objcmnbLayer._fldDatatable(" SELECT * FROM PreFixTb " & _
                                              "LEFT JOIN (SELECT Alias,AccDescr,GrpSetOn,accid FROM AccMast " & _
                                              "LEFT JOIN S1AccHd ON AccMast.S1AccId=S1AccHd.S1AccId)ACC ON PreFixTb.ANO=ACC.ACCID " & _
                                              "WHERE VrTypeNo = " & vrTypeNo & IIf(UsrBr = "", "", " AND BrId In ('', '" & UsrBr & "')") & " Order by ordNo")
        cmb.Items.Clear()
        If isaddnull Then
            cmb.Items.Add("")
        End If
        If dtTable.Rows.Count > 0 Then
            For i = 0 To dtTable.Rows.Count - 1
                cmb.Items.Add(dtTable(i)("Voucher Name"))
            Next
        End If
        If Slct And cmb.Items.Count > 0 Then
            cmb.SelectedIndex = 0
        End If
    End Function
    Sub SetGridHead()
        With grdVoucher
            SetGridProperty(grdVoucher)
            .ColumnCount = 6

            .Columns(ConstAlias).HeaderText = "Alias"
            .Columns(ConstAlias).Width = 100
            .Columns(ConstAlias).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstAlias).Visible = False

            .Columns(ConstName).HeaderText = "Account Name"
            .Columns(ConstName).Width = 300
            .Columns(ConstName).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstName).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

            .Columns(ConstAmount).HeaderText = "Amount"
            .Columns(ConstAmount).Width = 100
            .Columns(ConstAmount).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstAmount).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstAmount).DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns(ConstReference).HeaderText = "Reference"
            .Columns(ConstReference).Width = 100
            .Columns(ConstReference).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(ConstReference).Visible = False

            .Columns(ConstAccountNo).HeaderText = "AccountNO"
            .Columns(ConstAccountNo).Width = Me.Width * 5 / 100   '100
            .Columns(ConstAccountNo).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstAccountNo).Visible = False

            .Columns(ConstUnq).HeaderText = "Unq"
            .Columns(ConstUnq).Visible = False
        End With
        resizeGridColumn(grdVoucher, ConstName)

    End Sub
    Private Sub loadMultipleDebits()
        Dim i As Integer
        If dt Is Nothing Then Exit Sub
        Dim rindex As Integer
        With grdVoucher
            For i = 0 To dt.Rows.Count - 1
                If (CDbl(dt(i)("accAmt")) > 0 And trtype = "IS") Or (CDbl(dt(i)("accAmt")) < 0 And (trtype = "IP" Or trtype = "SR")) Then
                    .Rows.Add()
                    rindex = .RowCount - 1
                    .Item(ConstAlias, rindex).Value = Trim(dt(i)("Alias") & "")
                    .Item(ConstName, rindex).Value = dt(i)("AccDescr")
                    If dt(i)("accAmt") < 0 Then
                        .Item(ConstAmount, rindex).Value = dt(i)("accAmt") * -1
                    Else
                        .Item(ConstAmount, rindex).Value = dt(i)("accAmt")
                    End If
                    .Item(ConstReference, rindex).Value = dt(i)("reference")
                    .Item(ConstAccountNo, rindex).Value = dt(i)("accid")
                    .Item(ConstUnq, rindex).Value = dt(i)("dbtid")
                End If
            Next
        End With
        calcuate()
        'SetGridHead()
    End Sub

    Private Sub txtSuppName_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSuppName.KeyDown
        lstKey = e.KeyCode
        Dim MyCtrl As TextBox
        MyCtrl = sender
        If e.KeyCode = Keys.Return Then
            If Not fMList Is Nothing Then fMList.Visible = False
            txtAmt.Focus()
        ElseIf e.KeyCode = Keys.Up Or e.KeyCode = Keys.PageUp Then
            If fMList.Visible Then
                fMList.MoveUp(MyCtrl.Text)
                Exit Sub
            End If
        ElseIf e.KeyCode = Keys.Down Or e.KeyCode = Keys.PageDown Then
            If Not fMList Is Nothing Then
                If fMList.Visible Then
                    fMList.MoveDown(MyCtrl.Text)
                    Exit Sub
                End If
            End If
        End If
    End Sub


    Private Sub txtSuppName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSuppName.TextChanged
        If chgbyprg = True Then Exit Sub
        Dim myCtrl As Control = sender
        Select Case myCtrl.Name
            Case "txtSuppAlias"
                _srchTxtId = 1
            Case "txtSuppName"
                _srchTxtId = 2
        End Select
        _srchOnce = False
        ShowFmlist(sender)
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
            Dim x As Integer = Me.Width '- fMList.Width - 100
            Dim y As Integer = Me.Height '- fMList.Height - 100
            PopupLoc = New Point(x, y)
            If fMList.Visible = False Then
                Select Case _srchTxtId
                    Case 1, 2
                        SetFmlist(fMList, 13)
                End Select
                fMList.loc = PopupLoc
                fMList.Show(fMainForm)
                fMList.Visible = True
            End If
        End If
        Select Case _srchTxtId
            Case 2   'Customer name
                fMList.SearchIndex = 0
                fMList.SearchIndexDescr = 1
                fMList_doFocus()
                fMList.Search(txtSuppName.Text)
                txtSuppAlias.Text = fMList.AssignList(txtSuppName, lstKey, chgbyprg)
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
            Case 1, 2
                txtSuppAlias.Text = ItmFlds(1)
                txtSuppName.Text = ItmFlds(0)
                txtSuppAlias.Tag = ItmFlds(3)
        End Select
        chgbyprg = False
    End Sub

    Private Sub txtAmt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAmt.KeyDown
        If e.KeyCode = Keys.Return Then
            txtreference.Focus()
        End If
    End Sub

    Private Sub txtAmt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAmt.KeyPress
        NumericTextOnKeypress(sender, e, chgbyprg, numFormat)
    End Sub
    Private Sub AddToGrid()
        Dim i As Integer
        If Val(txtSuppAlias.Tag) = 0 Or Val(txtAmt.Text) = 0 Then Exit Sub
        With grdVoucher
            .Rows.Add()
            i = .RowCount - 1
            .Item(ConstAlias, i).Value = txtSuppAlias.Text
            .Item(ConstName, i).Value = txtSuppName.Text
            If Val(txtAmt.Text) = 0 Then txtAmt.Text = 0
            .Item(ConstAmount, i).Value = CDbl(txtAmt.Text)
            .Item(ConstAccountNo, i).Value = Val(txtSuppAlias.Tag)
            .Item(ConstReference, i).Value = txtreference.Text
        End With
        calcuate()
        makeClear()
    End Sub
    Private Sub makeClear()
        chgbyprg = True
        txtAmt.Text = Format(0, numFormat)
        txtSuppName.Text = ""
        txtSuppAlias.Text = ""
        txtSuppAlias.Tag = ""
        'txtreference.Text = reference
        chgbyprg = False
        cmbVoucherTp_SelectedIndexChanged(cmbVoucherTp, New System.EventArgs)
    End Sub

    Private Sub txtSuppName_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSuppName.Validated
        chgbyprg = True
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT * FROM AccMast where alias='" & txtSuppAlias.Text & "'")
        If dt.Rows.Count > 0 Then
            txtSuppAlias.Tag = dt(0)("accid")
            txtSuppName.Text = dt(0)("AccDescr")
        Else
            txtSuppAlias.Tag = ""
            txtSuppName.Text = ""
        End If
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
        chgbyprg = False
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        AddToGrid()
    End Sub

    Private Sub txtAmt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAmt.TextChanged

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        'If dt.Rows.Count > 0 Then
        '    MsgBox("")
        'End If
        btnupdate.Tag = 0
        Me.Close()
    End Sub
    Private Sub calcuate()
        Dim i As Integer
        Dim ttl As Double
        For i = 0 To grdVoucher.RowCount - 1
            ttl = ttl + CDbl(grdVoucher.Item(ConstAmount, i).Value)
        Next
        For i = 0 To grdpaid.RowCount - 1
            ttl = ttl + CDbl(grdpaid.Item("Assign", i).Value)
        Next
        lblassigned.Text = "Assigned Amt. " & Format(ttl, numFormat)
        lblassigned.Tag = ttl
        lblbalance.Text = Format(CDbl(lblinvoiceAmt.Tag) - ttl, numFormat)
    End Sub

    Private Sub btnremove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnremove.Click
        If MsgBox("Do you want to remove the row?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        grdVoucher.Rows.RemoveAt(grdVoucher.CurrentRow.Index)
        calcuate()
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        AddToGrid()
        If enableAdvanceEntryInMultipleDebit Then
            If Val(lblassigned.Tag) <> Val(lblinvoiceAmt.Tag) Then
                If MsgBox("Assigned Amount not matched with Invoice Amount! Do you want to continue?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Exit Sub
                End If
            End If
        Else
            If Val(lblassigned.Tag) > Val(lblinvoiceAmt.Tag) Then
                MsgBox("Assigned Amount greater than Invoice Amount", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            If Val(lblassigned.Tag) <> Val(lblinvoiceAmt.Tag) Then
                If (iscashinvoiceonly = True) Then
                    MsgBox("Credit Invoice not allowed for this customer", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    If MsgBox("Assigned Amount not matched with Invoice Amount! Do you want to continue?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                End If
            End If
        End If

        AddToDatatable()
        btnupdate.Tag = 1
        Me.Close()
    End Sub
    Private Sub AddToDatatable()
        If dt Is Nothing Then Exit Sub
        Dim i As Integer
        Dim dr As DataRow
        dt.Rows.Clear()
        Dim ttl As Double
        With grdVoucher
            If .RowCount > 0 Then
                For i = 0 To .RowCount - 1
                    dr = dt.NewRow
                    dr("Alias") = .Item(ConstAlias, i).Value
                    dr("AccDescr") = .Item(ConstName, i).Value
                    If trtype = "IS" Then
                        dr("accAmt") = .Item(ConstAmount, i).Value
                    Else
                        dr("accAmt") = .Item(ConstAmount, i).Value * -1
                    End If

                    ttl = ttl + CDbl(.Item(ConstAmount, i).Value)
                    dr("reference") = .Item(ConstReference, i).Value
                    dr("accid") = .Item(ConstAccountNo, i).Value
                    dr("dbtid") = Val(.Item(ConstUnq, i).Value & "")
                    dt.Rows.Add(dr)
                Next
                If Not skipCrEntry Then
                    dr = dt.NewRow
                    dr("Alias") = customeralias
                    dr("AccDescr") = customername
                    If trtype = "IS" Then
                        dr("accAmt") = ttl * -1
                    Else
                        dr("accAmt") = ttl
                    End If
                    'dr("accAmt") = ttl * -1 'IIf(Val(lblassigned.Tag) >= Val(lblinvoiceAmt.Tag), CDbl(lblinvoiceAmt.Tag), CDbl(lblassigned.Tag)) * -1
                    dr("reference") = txtreference.Text
                    dr("accid") = customeraccid
                    dr("dbtid") = 0
                    dt.Rows.Add(dr)
                End If

                If enableAdvanceEntryInMultipleDebit Then
                    If Val(lblassigned.Tag) > Val(lblinvoiceAmt.Tag) Then
                        dr = dt.NewRow
                        dr("Alias") = customeralias
                        dr("AccDescr") = customername
                        If trtype = "IS" Then
                            dr("accAmt") = (CDbl(lblassigned.Tag) - CDbl(lblinvoiceAmt.Tag)) * -1
                        Else
                            dr("accAmt") = (CDbl(lblassigned.Tag) - CDbl(lblinvoiceAmt.Tag))
                        End If

                        dr("reference") = "ON/AC"
                        dr("accid") = customeraccid
                        dr("dbtid") = 0
                        dt.Rows.Add(dr)
                    End If
                End If
            End If


        End With
    End Sub

    Private Sub txtreference_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtreference.KeyDown
        If e.KeyCode = Keys.Return Then
            AddToGrid()
        End If
    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            Const WM_KEYDOWN As Integer = &H100
            Const WM_KEYUP As Integer = &H101
            If msg.Msg = WM_KEYDOWN Or msg.Msg = WM_KEYUP Then
                If msg.WParam.ToInt32() = CInt(Keys.F1) Then
                    cmbVoucherTp.SelectedIndex = 0
                ElseIf msg.WParam.ToInt32() = CInt(Keys.F2) Then
                    cmbVoucherTp.SelectedIndex = 1
                ElseIf msg.WParam.ToInt32() = CInt(Keys.F3) Then
                    cmbVoucherTp.SelectedIndex = 2
                ElseIf msg.WParam.ToInt32() = CInt(Keys.F8) Then
                    btnupdate_Click(btnupdate, New System.EventArgs)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub cmbVoucherTp_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVoucherTp.SelectedIndexChanged
        Dim dt As DataTable
        Dim _qurey As EnumerableRowCollection(Of DataRow)
        If trtype = "IP" Then
            _qurey = From data In dtTable.AsEnumerable() Where data("VrTypeNo") = 1 And data("Voucher Name") = cmbVoucherTp.Text Select data
        Else
            _qurey = From data In dtTable.AsEnumerable() Where data("VrTypeNo") = 2 And data("Voucher Name") = cmbVoucherTp.Text Select data
        End If
        If _qurey.Count > 0 Then
            dt = _qurey.CopyToDataTable()
        Else
            dt = dtTable.Clone
        End If
        If dt.Rows.Count > 0 Then
            chgbyprg = True
            If Not IsDBNull(dt(0)("accid")) Then
                txtSuppAlias.Tag = dt(0)("accid")
                txtSuppAlias.Text = dt(0)("Alias")
                txtSuppName.Text = dt(0)("AccDescr")
            End If
            chgbyprg = False
            txtAmt.Focus()
        Else
            MsgBox("Voucher Settings Not Found", MsgBoxStyle.Critical)
        End If
    End Sub
    Private Sub ldTrans()
        If customeraccid = 0 Then GoTo ext
        If dtSetoffTable Is Nothing Then CreateSetoffTable(dtSetoffTable)
        Dim refcondition As String = ""
        If txtparty.Text <> "" Then
            refcondition = " AND AccTrCmn.othinf='" & txtparty.Text & "'"
        End If
        If trtype = "IP" Then
            dtSetoffTable = _objcmnbLayer._fldDatatable("Select '' Tag,convert(money,0) Assign, DealAmt,JVDate," & _
                                                    "PreFix + CASE when PreFix='' then '' else '/' end +convert(varchar,JVNum) RVNO," & _
                                                    "AccTrCmn.othinf Reference,AccTrDet.LinkNo,UnqNo,accountno,CurrencyCode,CurrRate,EntryRef+' - '+AccTrCmn.othinf EntryRef from AccTrDet " & _
                                                    "left join AccTrCmn on AccTrCmn.linkno=AccTrDet.linkno " & _
                                                    "where reference in ('ON/AC','') and jvtype='PV' and DealAmt>0 and accountno=" & customeraccid & refcondition)
        Else
            dtSetoffTable = _objcmnbLayer._fldDatatable("Select '' Tag,convert(money,0) Assign, DealAmt*-1 DealAmt,JVDate," & _
                                                    "PreFix + CASE when PreFix='' then '' else '/' end +convert(varchar,JVNum) RVNO," & _
                                                    "AccTrCmn.othinf Reference,AccTrDet.LinkNo,UnqNo,accountno,CurrencyCode,CurrRate,EntryRef+' - '+AccTrCmn.othinf EntryRef from AccTrDet " & _
                                                    "left join AccTrCmn on AccTrCmn.linkno=AccTrDet.linkno " & _
                                                    "where reference in ('ON/AC','') and jvtype='RV' and DealAmt<0 and accountno=" & customeraccid & refcondition)
        End If
        
        grdpaid.DataSource = dtSetoffTable
        SetGridTr()

        'assaignTotal()
ext:
    End Sub
    Private Sub SetGridTr()
        With grdpaid
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

            .Columns("JVDate").HeaderText = "Date"
            .Columns("Tag").HeaderText = "Tag"
            .Columns("Tag").Width = 30
            .Columns("Tag").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("Tag").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Tag").DefaultCellStyle.BackColor = Color.LightYellow
            .Columns("Tag").ReadOnly = True

            .Columns("Assign").HeaderText = "Assign"
            .Columns("Assign").Width = 120
            .Columns("Assign").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("Assign").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Assign").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Assign").ReadOnly = True

            .Columns("DealAmt").HeaderText = "Balance"
            .Columns("DealAmt").Width = 120
            .Columns("DealAmt").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("DealAmt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("DealAmt").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("DealAmt").DefaultCellStyle.BackColor = Color.LightGreen
            .Columns("DealAmt").ReadOnly = True

            .Columns("linkno").Visible = False
            .Columns("UnqNo").Visible = False
            Dim i As Integer
            For i = 6 To .Columns.Count - 1
                .Columns(i).Visible = False
            Next
            resizeGridColumn(grdpaid, 4)
        End With
    End Sub

    Private Sub btnloadAdvance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnloadAdvance.Click
        ldTrans()
    End Sub

    Private Sub grdpaid_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdpaid.CellClick
        If e.ColumnIndex = 0 Then
            grdpaid.Item(e.ColumnIndex, e.RowIndex).Value = IIf(grdpaid.Item(e.ColumnIndex, e.RowIndex).Value = "", "Y", "")
            If grdpaid.Item(e.ColumnIndex, e.RowIndex).Value = "Y" Then
                If CDbl(lblbalance.Text) > 0 Then
                    If CDbl(grdpaid.Item("DealAmt", e.RowIndex).Value) > CDbl(lblbalance.Text) Then
                        grdpaid.Item("Assign", e.RowIndex).Value = Format(CDbl(lblbalance.Text), lnumformat)
                    Else
                        Dim amt As Double = CDbl(grdpaid.Item("DealAmt", e.RowIndex).Value)
                        grdpaid.Item("Assign", e.RowIndex).Value = amt ' Format(amt, lnumformat)
                    End If
                Else
                    MsgBox("Assigned Amount greater than invoice amount", MsgBoxStyle.Exclamation)
                End If
            Else
                grdpaid.Item("Assign", e.RowIndex).Value = Format(0, lnumformat)
            End If
            calcuate()
        End If
    End Sub

    Private Sub grdVoucher_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellContentClick

    End Sub
End Class