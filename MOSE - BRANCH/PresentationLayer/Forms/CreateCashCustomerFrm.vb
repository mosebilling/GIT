Public Class CreateCashCustomerFrm
    Private _objcmnbLayer As New clsCommon_BL
    Private _objInv As New clsInvoice
    Private WithEvents fSelect As Selectfrm
    Private chgbyprg As Boolean
    Private MyActiveControl As New Object
    Private lstKey As Integer
    Private _srchTxtId As Integer
    Private _srchOnce As Boolean
    Private _srchIndexId As Byte
    Private _vDtable As DataTable
    Public isaddnew As Boolean
    Public Event selectcust(ByVal custname As String, ByVal custaddress As String, ByVal Cashcustid As Long, _
                            ByVal cardnumber As String, ByVal phone As String, ByVal gstn As String)
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        saveCustomer()
    End Sub
    Private Function checkduplicateaccountsettings() As Boolean
        Dim dt As DataTable
        Dim custid As Integer
        dt = _objcmnbLayer._fldDatatable("Select custid  from CashCustomerTb where customeraccount=" & Val(txtaccount.Tag))
        If dt.Rows.Count > 0 Then
            custid = dt(0)("custid")
            If custid <> Val(txtname.Tag) Then
                Return False
            End If
        End If
        Return True
    End Function
    Private Sub saveCustomer()
        If Val(txtaccount.Tag) > 0 Then
            If checkduplicateaccountsettings() = False Then
                MsgBox("Account ledger found in another customer details! cannot update the record", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
        End If
        If txtname.Text = "" Then
            MsgBox("Invalid Name", MsgBoxStyle.Exclamation)
            txtname.Focus()
            Exit Sub
        End If
        If txtphone.Text = "" Then
            MsgBox("Invalid Phone", MsgBoxStyle.Exclamation)
            txtphone.Focus()
            Exit Sub
        End If
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select custid  from CashCustomerTb where Phone='" & txtphone.Text & "'")
        If dt.Rows.Count > 0 Then
            If Val(txtname.Tag) <> dt(0)("custid") Then
                If MsgBox("Customer Name found with phone number! Do you want to load?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                dt = _objcmnbLayer._fldDatatable("SELECT CashCustomerTb.*,AccDescr,cardnumber FROM CashCustomerTb " & _
                                         "LEFT JOIN Accmast on CashCustomerTb.customeraccount=accmast.accid  " & _
                                          "LEFT JOIN CardmasterTb ON CardmasterTb.customerid=CashCustomerTb.custid " & _
                                          "where Phone='" & txtphone.Text & "'")
                ldRec(dt)
                Exit Sub
            End If
        End If
        With _objInv
            .custid = Val(txtname.Tag)
            .CustName = txtname.Text
            .Add1 = txtAddr0.Text
            .Add2 = txtAddr1.Text
            .Add3 = txtAddr2.Text
            .Phone = txtphone.Text
            .email = txtemail.Text
            .Memberid = txtmemberid.Text
            .GiftVrNo = txtgifvr.Text
            .Remarks = txtremarks.Text
            .customeraccount = Val(txtaccount.Tag)
            .GSTN = txtgstn.Text
            txtname.Tag = .saveCashCustomer()
        End With
        If Val(txtaccount.Tag) > 0 Then
            _objcmnbLayer._saveDatawithOutParm("Update AccMastAddr set Address1='" & MkDbSrchStr(txtAddr0.Text) & "',Address2='" & MkDbSrchStr(txtAddr1.Text) & "'," & _
                                                           "Address3='" & MkDbSrchStr(txtAddr2.Text) & "',Phone='" & MkDbSrchStr(txtphone.Text) & "',EMail='" & MkDbSrchStr(txtemail.Text) & "',Remarks='" & MkDbSrchStr(txtremarks.Text) & "'" & _
                                                           "WHERE AccountNo=" & Val(txtaccount.Tag))
        End If

        MsgBox("Updated Successfully", MsgBoxStyle.Information)
        sendCust()
    End Sub

    Private Sub clearControls(Optional ByVal donotclearPhone As Boolean = False)
        txtAddr0.Text = ""
        txtAddr1.Text = ""
        txtAddr2.Text = ""
        txtemail.Text = ""
        If Not donotclearPhone Then
            txtphone.Text = ""
        End If
        txtgifvr.Text = ""
        txtname.Text = ""
        txtremarks.Text = ""
        txtname.Tag = ""
        txtaccount.Text = ""
        txtaccount.Tag = ""

    End Sub
    Private Sub ldCashCustomer()

        Dim query As String
        'query = "SELECT CustName [Customer Name],Add1 Address1,Phone,salesPoints Points," & IIf(Not enableCarWash, " Memberid [Member Id]", "cardnumber [Discount Card]") & ",custid " & _
        '                                 "FROM CashCustomerTb " & _
        '                                 "LEFT JOIN CardmasterTb ON CardmasterTb.customerid=CashCustomerTb.custid " & _
        '                                 "WHERE CustName like '%" & MkDbSrchStr(txtsearch.Text) & "%' OR Phone='" & MkDbSrchStr(txtsearch.Text) & _
        '                                 "' OR Memberid='" & MkDbSrchStr(txtsearch.Text) & "' OR GiftVrNo='" & MkDbSrchStr(txtsearch.Text) & _
        '                                 IIf(rdocard.Checked, "' OR cardnumber='" & MkDbSrchStr(txtsearch.Text), "") & "'"
        query = "SELECT CustName [Customer Name],Add1 Address1,Phone,Email,GSTN,salesPoints Points," & IIf(Not enableCarWash, " Memberid [Member Id]", "cardnumber [Discount Card]") & ",custid " & _
                                         "FROM CashCustomerTb " & _
                                         "LEFT JOIN CardmasterTb ON CardmasterTb.customerid=CashCustomerTb.custid "
        _vDtable = _objcmnbLayer._fldDatatable(query)
        If _vDtable.Rows.Count > 0 Then
            grdlist.DataSource = _vDtable
            setGridHead()
            btnselect.Enabled = True
            btnedit.Enabled = True

        ElseIf _vDtable.Rows.Count = 0 Then
            MsgBox("Record Not Found", MsgBoxStyle.Exclamation)
            txtsearch.Focus()
        End If
    End Sub
    Private Sub setGridHead()
        SetGridProperty(grdlist)
        With grdlist
            .Columns("custid").Visible = False
            .Columns("Customer Name").Width = 150
            .Columns("Points").Width = 60
            .Columns("Address1").Width = 150
            If enableCarWash Then
                .Columns("Discount Card").Width = 110
            End If
            .Columns("custid").Visible = False
            '.Columns("Add3").Visible = False
            '.Columns("Remarks").Visible = False
        End With
    End Sub
    Private Sub ldRec(ByVal dt As DataTable)
        txtname.Tag = dt(0)("custid")
        txtname.Text = Trim(dt(0)("CustName") & "")
        txtAddr0.Text = Trim(dt(0)("Add1") & "")
        txtAddr1.Text = Trim(dt(0)("Add2") & "")
        txtAddr2.Text = Trim(dt(0)("Add3") & "")
        txtphone.Text = Trim(dt(0)("Phone") & "")
        txtemail.Text = Trim(dt(0)("email") & "")
        txtgifvr.Text = Trim(dt(0)("GiftVrNo") & "")
        txtgstn.Text = Trim(dt(0)("GSTN") & "")
        txtmemberid.Text = Trim(dt(0)("Memberid") & "")
        txtremarks.Text = Trim(dt(0)("Remarks") & "")
        txtaccount.Tag = Val(dt(0)("customeraccount") & "")
        txtaccount.Text = Trim(dt(0)("AccDescr") & "")
        lblcard.Text = Trim(dt(0)("cardnumber") & "")
    End Sub
    Private Sub ldfromGrid(ByVal crow As Integer)
        Dim dt As DataTable
        Dim additionalcondition As String = ""
        If enableCarWash Then
            If Trim(grdlist.Item("Discount Card", crow).Value & "") <> "" Then
                additionalcondition = " AND cardnumber='" & grdlist.Item("Discount Card", crow).Value & "'"
            End If
        End If
        dt = _objcmnbLayer._fldDatatable("SELECT CashCustomerTb.*,AccDescr,cardnumber FROM CashCustomerTb " & _
                                         "LEFT JOIN Accmast on CashCustomerTb.customeraccount=accmast.accid  " & _
                                          "LEFT JOIN CardmasterTb ON CardmasterTb.customerid=CashCustomerTb.custid " & _
                                         "WHERE Custid=" & grdlist.Item("custid", crow).Value & additionalcondition)
        If dt.Rows.Count > 0 Then
            ldRec(dt)
        End If
    End Sub

    Private Sub grdlist_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdlist.CellClick
        ldfromGrid(e.RowIndex)
    End Sub
    Private Sub grdlist_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdlist.CellDoubleClick

    End Sub

    Private Sub btnselect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnselect.Click
        sendCust()
    End Sub
    Private Sub sendCust()
        Dim address As String = ""
        address = addAddress(address, txtAddr0.Text)
        address = addAddress(address, txtAddr1.Text)
        address = addAddress(address, txtAddr2.Text)
        If txtphone.Text <> "" Then address = addAddress(address, "PH: " & txtphone.Text)
        If txtmemberid.Text <> "" Then address = addAddress(address, "Member ID: " & txtmemberid.Text)
        RaiseEvent selectcust(txtname.Text, Trim(address), Val(txtname.Tag), lblcard.Text, txtphone.Text, txtgstn.Text)
        Me.Close()
    End Sub
    Private Function addAddress(ByVal address As String, ByVal addclm As String) As String
        If address <> "" Then
            address = address & vbCrLf
        End If
        Return address & addclm
    End Function

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        clearControls()
    End Sub

    Private Sub CreateCashCustomerFrm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If grpedit.Enabled Then
            txtphone.Focus()
        Else
            txtsearch.Focus()
        End If
    End Sub

    Private Sub txtsearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtsearch.KeyDown
        Select Case e.KeyCode
            Case Keys.Up, Keys.PageUp
                MoveUp()
            Case Keys.Down, Keys.PageDown
                MoveDown()
            Case Keys.Return
                btnselect_Click(sender, e)
        End Select
        'If e.KeyCode = Keys.Return Then
        '    ldCashCustomer()
        '    txtsearch.Focus()
        'End If
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            Const WM_KEYDOWN As Integer = &H100
            Const WM_KEYUP As Integer = &H101
            'msg.ToString()
            If msg.Msg = WM_KEYDOWN Or msg.Msg = WM_KEYUP Then
                If msg.WParam.ToInt32() = CInt(Keys.F9) Then
                    If txtname.Text = "" Then MsgBox("Invalid Customer", MsgBoxStyle.Exclamation) : txtname.Focus() : Exit Function
                    sendCust()
                End If
                If msg.WParam.ToInt32() = CInt(Keys.F8) Then
                    If txtname.Text = "" Then MsgBox("Invalid Customer", MsgBoxStyle.Exclamation) : txtname.Focus() : Exit Function
                    saveCustomer()
                End If
                If msg.WParam.ToInt32() = CInt(Keys.F6) Then
                    rdocustomer.Checked = True
                    txtsearch.Focus()
                ElseIf msg.WParam.ToInt32() = CInt(Keys.F7) Then
                    rdocard.Checked = True
                    txtsearch.Focus()
                End If
            End If
ctn:
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub txtsearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtsearch.TextChanged
        grdlist.DataSource = SearchCustomerGrid()
        setGridHead()
    End Sub


    Private Sub txtname_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtname.KeyDown, txtAddr0.KeyDown, txtAddr1.KeyDown, _
                                                                                                               txtAddr2.KeyDown, txtphone.KeyDown, txtemail.KeyDown, _
                                                                                                               txtgifvr.KeyDown, txtmemberid.KeyDown, txtgstn.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub CreateCashCustomerFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        plws.Visible = True
        ldCashCustomer()
        If isaddnew Then addnew(True)
    End Sub
    Private Sub addnew(Optional ByVal donotclearPhone As Boolean = False)
        clearControls(donotclearPhone)
        grpedit.Enabled = True
        btnnew.Enabled = False
        txtname.Focus()
    End Sub

    Private Sub txtaccount_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtaccount.KeyDown
        If e.KeyCode = Keys.F2 Then
            _srchTxtId = 2
            ldSelect(1)
        ElseIf e.KeyCode = Keys.Delete Or e.KeyCode = Keys.Back Then
            txtaccount.Text = ""
            txtaccount.Tag = ""
        End If

    End Sub

    Private Sub ldSelect(ByVal BVal As Single)
        fSelect = New Selectfrm
        'nSelect = BVal
        SetForm(fSelect, BVal)
        If BVal = 1 Or BVal = 0 Then
            fSelect.Width = 742
            fSelect.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
        Else
            fSelect.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedToolWindow
            fSelect.Width = 425
        End If
        fSelect.Show()
    End Sub

    Private Sub fSelect_doSelect(ByVal KeyId As String, ByVal strFld1 As String, ByVal strFld2 As String) Handles fSelect.doSelect
        chgbyprg = True
        If _srchTxtId = 2 Then
            txtaccount.Text = strFld1
            txtaccount.Tag = KeyId
        End If
        chgbyprg = False
    End Sub
   

    Private Sub fSelect_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fSelect.FormClosed
        fSelect = Nothing
    End Sub


    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
       addnew
    End Sub

    Private Sub btnedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnedit.Click
        grpedit.Enabled = True
        btnedit.Enabled = False
        txtname.Focus()
    End Sub


    Private Sub rdocard_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdocard.Click, rdocustomer.Click
        txtsearch.Focus()
    End Sub

    Private Sub grdlist_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdlist.DoubleClick
        sendCust()
    End Sub
    Private Function SearchCustomerGrid() As DataTable
        Dim bDatatable As DataTable
        If _vDtable.Rows.Count = 0 Then bDatatable = _vDtable.Clone : GoTo nxt
        Dim srchText As String = txtsearch.Text
        Dim _qurey As EnumerableRowCollection(Of DataRow)
        If rdocard.Checked Then
            _qurey = From data In _vDtable.AsEnumerable() Where data(6).ToString.ToUpper.Contains(UCase(srchText)) Select data
        Else
            _qurey = From data In _vDtable.AsEnumerable() Where data(0).ToString.ToUpper.Contains(UCase(srchText)) Or _
                data(1).ToString.ToUpper.Contains(UCase(srchText)) Or _
                data(2).ToString.ToUpper.Contains(UCase(srchText)) Select data
        End If
       

        If _qurey.Count > 0 Then
            bDatatable = _qurey.CopyToDataTable()
        Else
            bDatatable = _vDtable.Clone
        End If
nxt:
        Return bDatatable
    End Function
    Private Sub MoveUp()
        Dim r As Integer
        With grdlist
            If .RowCount < 1 Then Exit Sub
            If .CurrentRow.Index < 0 Then Exit Sub
            r = .CurrentRow.Index
            If r <> 0 Then
                r = r - 1
                If Not r < 0 Then
                    .Rows(r).Selected = True
                    .CurrentCell = .Item(0, r)
                    .FirstDisplayedScrollingRowIndex() = r
                End If
            End If
        End With
    End Sub
    Private Sub MoveDown()
        On Error Resume Next
        Dim r As Integer
        With grdlist
            If .RowCount < 1 Then Exit Sub
            If .CurrentRow.Index = .RowCount - 1 Then GoTo Slct
            r = .CurrentRow.Index
            If .CurrentRow.Index < .RowCount - 1 Then
                r = r + 1
                .Rows(r).Selected = True
                .CurrentCell = .Item(0, r)
                .FirstDisplayedScrollingRowIndex() = r
            Else
                Exit Sub
            End If
Slct:
            'MsgBox(dvData.CurrentRow.Index)
        End With
    End Sub

    Private Sub btnexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexport.Click
        Dim a As String = ""
        Dim filename As String = ""
        FolderBrowserDialog1.ShowDialog()
        If FolderBrowserDialog1.SelectedPath <> "" Then
            filename = FolderBrowserDialog1.SelectedPath
        Else
            Exit Sub
        End If
        DataTableToExcel(filename & "/CashCustomer.xls", _vDtable)
        MsgBox("Export Completed", MsgBoxStyle.Information)
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        If Val(txtname.Tag) > 0 Then
            If MsgBox("Do you want to delete?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            _objcmnbLayer._saveDatawithOutParm("delete from CashCustomerTb where custid=" & Val(txtname.Tag))
            clearControls()
            ldCashCustomer()
        End If
    End Sub
End Class