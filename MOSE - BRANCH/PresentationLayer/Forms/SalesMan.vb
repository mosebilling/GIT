Public Class SalesMan
#Region "Class Objects"
    Private _objcmnbLayer As New clsCommon_BL
#End Region
    Private chgbyprg As Boolean
    Private _srchTxtId As Integer
    Private _srchOnce As Boolean
    Private MyActiveControl As Object
    Private lstKey As Integer
    Private idx As Integer
    Private numCtrl As TextBox
    Private SelStart As Integer
    Private str1 As String
    Private str2 As String
    Private str3 As String
    Private WithEvents fMList As Mlistfrm
    Dim activecontrolname As String

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        clearCtrl()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        If txtcode.Text = "" Then
            MsgBox("Invalid Salesman Code!", MsgBoxStyle.Exclamation)
            txtcode.Focus()
            Exit Sub
        End If
        If txtname.Text = "" Then
            MsgBox("Invalid Salesman Name!", MsgBoxStyle.Exclamation)
            txtname.Focus()
            Exit Sub
        End If
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("select salesmanid from  SalesmanTb where SManCode='" & txtcode.Text & "'")
        If dt.Rows.Count > 0 Then
            If dt(0)("salesmanid") <> Val(txtcode.Tag) Then
                MsgBox("Salesman Code already exist", MsgBoxStyle.Exclamation)
                txtcode.Focus()
                Exit Sub
            End If
        End If
        dt = _objcmnbLayer._fldDatatable("select salesmanid from  SalesmanTb where SManName='" & txtname.Text & "'")
        If dt.Rows.Count > 0 Then
            If dt(0)("salesmanid") <> Val(txtcode.Tag) Then
                MsgBox("Salesman Name already exist", MsgBoxStyle.Exclamation)
                txtname.Focus()
                Exit Sub
            End If
        End If

        If Val(numdis.Text) = 0 Then numdis.Text = 0
        If Val(numcom.Text) = 0 Then numcom.Text = 0
        If Val(txttargetAmt.Text) = 0 Then txttargetAmt.Text = 0
        Dim branchid As Integer
        dt = _objcmnbLayer._fldDatatable("select BranchId from BranchTb where Branchcode='" & UsrBr & "'")
        If dt.Rows.Count > 0 Then
            branchid = dt(0)("BranchId")
        End If
        If Val(txtcode.Tag) > 0 Then
            _objcmnbLayer._saveDatawithOutParm("Update SalesmanTb set SManCode ='" & txtcode.Text & "', SManName='" & txtname.Text & _
                                               "',accountno=" & Val(txtac.Tag) & ",comP=" & CDbl(numcom.Text) & _
                                               ",DisP=" & CDbl(numdis.Text) & ",targetAmt=" & CDbl(txttargetAmt.Text) & _
                                               ",empcode ='" & txtempcode.Text & "',branchid=" & branchid & " where salesmanid=" & Val(txtcode.Tag))
        Else
            _objcmnbLayer._saveDatawithOutParm("Insert into SalesmanTb (SManCode,SManName,accountno,comP,DisP,targetAmt,empcode,branchid) values('" & _
                                               txtcode.Text & "','" & txtname.Text & "'," & Val(txtac.Tag) & "," & CDbl(numcom.Text) & "," & _
                                               CDbl(numdis.Text) & "," & CDbl(txttargetAmt.Text) & ",'" & txtempcode.Text & "'," & branchid & ")")
        End If
        If chkslab.Checked Then
            dt = _objcmnbLayer._fldDatatable("select salesmanid from  SalesmanTb where SManCode='" & txtcode.Text & "'")
            If dt.Rows.Count > 0 Then
                Dim i As Integer
                _objcmnbLayer._saveDatawithOutParm("Delete from SalesmanCommissionSlabTb where salesmanid=" & dt(0)("salesmanid"))
                With grdSlab
                    For i = 0 To .RowCount - 1
                        If Val(.Item(0, i).Value) > 0 Then
                            _objcmnbLayer._saveDatawithOutParm("Insert into SalesmanCommissionSlabTb (salesmanid,targetamt,commissionamt,ispercentage) values(" & _
                                                                           dt(0)("salesmanid") & "," & CDbl(.Item(0, i).Value) & "," & CDbl(.Item(1, i).Value) & "," & IIf(.Item(2, i).Value = "Y", 1, 0) & ")")
                        End If
                    Next
                End With

            End If
        End If
        MsgBox("Sales Man Updated", MsgBoxStyle.Information)
        isnewSalesmanAdded = True
        loadSalesman()
        clearCtrl()
    End Sub
    Private Sub loadSlab()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select * from SalesmanCommissionSlabTb where salesmanid=" & Val(txtcode.Tag))
        Dim i As Integer
        chkslab.Checked = dt.Rows.Count > 0
        grdSlab.Rows.Clear()
        For i = 0 To dt.Rows.Count - 1
            With grdSlab
                .Rows.Add()
                .Item(0, i).Value = Format(CDbl(dt(i)("targetamt")), numFormat)
                .Item(1, i).Value = Format(CDbl(dt(i)("commissionamt")), numFormat)
                If Val(dt(i)("ispercentage")) = 0 Then
                    .Item(2, i).Value = ""
                Else
                    .Item(2, i).Value = "Y"
                End If
            End With
        Next
    End Sub
    Private Sub clearCtrl()
        chgbyprg = True
        txtname.Text = ""
        txtcode.Text = ""
        txtcode.Tag = 0
        txtac.Text = ""
        txtac.Tag = ""
        txtempcode.Text = ""
        txttargetAmt.Text = Format(0, numFormat)
        numcom.Text = Format(0, numFormat)
        numdis.Text = Format(0, numFormat)
        txtcode.Focus()
        grdSlab.Rows.Clear()
        chkslab.Checked = False
        chgbyprg = False
    End Sub
    Private Sub loadSalesman()
        Try
            Dim dtset As DataSet
            Dim ListTb As DataTable
            dtset = _objcmnbLayer._ldDataset("SELECT SManCode,SManName,ISNULL(AccDescr,'') AccDescr,SalesmanTb.comP,SalesmanTb.DisP,SalesmanTb.accountno," & _
                                                 "salesmanid,isnull(targetAmt,0)targetAmt,empcode FROM SalesmanTb left join BranchTb on BranchTb.branchid=SalesmanTb.branchid " & _
                                                 "LEFT JOIN AccMast ON SalesmanTb.accountno=AccMast.Accid " & IIf(UsrBr = "", "", "where Branchcode='" & UsrBr & "'") & _
                                                 " SELECT SManCode FROM SalesmanTb left join BranchTb on BranchTb.branchid=SalesmanTb.branchid " & IIf(UsrBr = "", "", "where Branchcode='" & UsrBr & "'"), False)
            dtsalesman = dtset.Tables(1)
            ListTb = dtset.Tables(0)
            With lstContent
                .Items.Clear()
                If ListTb.Rows.Count > 0 Then
                    For i = 0 To ListTb.Rows.Count - 1
                        .Items.Add(ListTb(i)("SManCode"))
                        If .Items.Item(i).SubItems.Count > 1 Then
                            .Items.Item(i).SubItems(1).Text = .Items.Add(ListTb(i)("SManName"))
                        Else
                            .Items.Item(i).SubItems.Insert(1, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, ListTb(i)("SManName")))
                        End If

                        If .Items.Item(i).SubItems.Count > 2 Then
                            .Items.Item(i).SubItems(2).Text = .Items.Add(ListTb(i)("AccDescr"))
                        Else
                            .Items.Item(i).SubItems.Insert(2, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, ListTb(i)("AccDescr")))
                        End If

                        If .Items.Item(i).SubItems.Count > 3 Then
                            .Items.Item(i).SubItems(3).Text = .Items.Add(ListTb(i)("comP"))
                        Else
                            .Items.Item(i).SubItems.Insert(3, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, ListTb(i)("comP")))
                        End If

                        If .Items.Item(i).SubItems.Count > 4 Then
                            .Items.Item(i).SubItems(4).Text = .Items.Add(ListTb(i)("DisP"))
                        Else
                            .Items.Item(i).SubItems.Insert(4, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, ListTb(i)("DisP")))
                        End If

                        If .Items.Item(i).SubItems.Count > 5 Then
                            .Items.Item(i).SubItems(5).Text = .Items.Add(ListTb(i)("accountno"))
                        Else
                            .Items.Item(i).SubItems.Insert(5, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, ListTb(i)("accountno")))
                        End If

                        If .Items.Item(i).SubItems.Count > 6 Then
                            .Items.Item(i).SubItems(6).Text = .Items.Add(ListTb(i)("targetAmt"))
                        Else
                            .Items.Item(i).SubItems.Insert(6, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, Format(CDbl(ListTb(i)("targetAmt")), numFormat)))
                        End If

                        If .Items.Item(i).SubItems.Count > 7 Then
                            .Items.Item(i).SubItems(7).Text = .Items.Add(ListTb(i)("empcode"))
                        Else
                            .Items.Item(i).SubItems.Insert(7, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, Trim(ListTb(i)("empcode") & "")))
                        End If

                        If .Items.Item(i).SubItems.Count > 8 Then
                            .Items.Item(i).SubItems(8).Text = .Items.Add(ListTb(i)("salesmanid"))
                        Else
                            .Items.Item(i).SubItems.Insert(8, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, ListTb(i)("salesmanid")))
                        End If
                    Next
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        End Try

    End Sub

    Private Sub lstContent_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstContent.DoubleClick
        chgbyprg = True
        txtcode.Text = lstContent.SelectedItems(0).SubItems(0).Text
        txtname.Text = lstContent.SelectedItems(0).SubItems(1).Text
        txtac.Text = lstContent.SelectedItems(0).SubItems(2).Text
        txtac.Tag = lstContent.SelectedItems(0).SubItems(5).Text
        numcom.Text = Format(CDbl(lstContent.SelectedItems(0).SubItems(3).Text), numFormat)
        numdis.Text = Format(CDbl(lstContent.SelectedItems(0).SubItems(4).Text), numFormat)
        txttargetAmt.Text = Format(CDbl(lstContent.SelectedItems(0).SubItems(6).Text), numFormat)
        txtempcode.Text = lstContent.SelectedItems(0).SubItems(7).Text
        txtcode.Tag = Val(lstContent.SelectedItems(0).SubItems(8).Text)
        loadSlab()
        chgbyprg = False
    End Sub

    Private Sub lstContent_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstContent.SelectedIndexChanged

    End Sub

    Private Sub SalesMan_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtcode.Focus()
    End Sub

    Private Sub SalesMan_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
    End Sub

    Private Sub SalesMan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        setCommssionGrid()
        loadSalesman()
        numcom.Text = Format(0, numFormat)
        numdis.Text = Format(0, numFormat)
        Dim ObjLocationlist As New List(Of String)
        ObjLocationlist = toLoadAutoFillListItems("empcode", "EmpMasterTb")
        Call toAssignDownListToText(txtempcode, ObjLocationlist) '
        txtbranch.Text = UsrBr
    End Sub

    Private Sub txtcode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtcode.KeyDown, txtname.KeyDown, txtempcode.KeyDown
        If e.KeyCode = Keys.Return Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        Dim dt As DataTable
        Dim strquery As String
        strquery = "SELECT TOP 1 Trid FROM (select trid from  ItmInvCmnTb where SlsManId='" & txtcode.Text & "'"
        strquery = strquery & " UNION ALL select trid from  ItmInvTrTb where Smancode='" & txtcode.Text & "'"
        strquery = strquery & " UNION ALL select LINKNO from  AccTrCmn where collectedOrDeliveredBy='" & txtcode.Text & "') tr"
        dt = _objcmnbLayer._fldDatatable(strquery)
        If dt.Rows.Count > 0 Then
            If Val(dt(0)("trid") & "") <> 0 Then
                MsgBox("Salesman Name found in transactions! you cannot delete", MsgBoxStyle.Exclamation)
                txtname.Focus()
                Exit Sub
            End If
        End If
        If MsgBox("Do you want to remove the Sales man?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        _objcmnbLayer._saveDatawithOutParm("DELETE FROM SalesmanTb WHERE salesmanid=" & Val(txtcode.Tag))
        _objcmnbLayer._saveDatawithOutParm("Delete from SalesmanCommissionSlabTb where salesmanid=" & Val(txtcode.Tag))
        txtcode.Focus()
        loadSalesman()
        txtname.Text = ""
        txtcode.Text = ""
        txtcode.Tag = 0
    End Sub

    Private Sub txtac_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtac.KeyDown
        lstKey = e.KeyCode
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            If Not fMList Is Nothing Then
                If fMList.Visible Then fMList.Hide()
            End If
        End If

    End Sub

    Private Sub txtac_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtac.TextChanged
        If Not chgbyprg Then
            Dim control As Control = DirectCast(sender, Control)
            If (control.Name = "txtac") Then
                _srchTxtId = 1
            End If
            _srchOnce = False
            ShowFmlist(DirectCast(sender, Control))
        End If

    End Sub

    Private Sub ShowFmlist(ByVal myCtrl As Control)
        If Not chgbyprg Then
            MyActiveControl = myCtrl
            If Not _srchOnce Then
                chgbyprg = True
                If (fMList Is Nothing) Then
                    fMList = New Mlistfrm
                End If
                Dim x As Integer = ((Left + Width) + 10)
                Dim y As Integer = CInt(Math.Round(CDbl((Top + (CDbl(Height) / 2)))))
                Dim point As New Point(x, y)
                If Not fMList.Visible Then
                    If (_srchTxtId = 1) Then
                        CmnVeriablesAndFunctions.SetFmlist(fMList, 13, 0)
                    End If
                    fMList.loc = point
                    fMList.Show(PublicVariables.fMainForm)
                    fMList.Visible = True
                End If
            End If
            If (_srchTxtId = 1) Then
                fMList.SearchIndex = 0
                fMList.SearchIndexDescr = 1
                fMList_doFocus()
                fMList.Search(txtac.Text)
                fMList.AssignList(txtac, DirectCast(lstKey, Keys), chgbyprg, False)
                txtac = DirectCast(txtac, TextBox)
            End If
            _srchOnce = True
            chgbyprg = False
        End If
    End Sub

    Private Sub numcom_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles numcom.KeyDown, numdis.KeyDown, txttargetAmt.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub numcom_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles numcom.KeyPress, numdis.KeyPress
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
            numCtrl.Text = Format(Val(numCtrl.Text), numFormat)
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

    Private Sub numcom_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numcom.TextChanged

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
        If (_srchTxtId = 1) Then
            txtac.Text = ItmFlds(0)
            txtac.Tag = ItmFlds(3)
        End If
        chgbyprg = False
    End Sub

    Private Sub txtac_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtac.Validated
        If ((txtac.Text <> "") AndAlso Not chgbyprg) Then
            _objcmnbLayer = New clsCommon_BL
            Dim dt As DataTable = _objcmnbLayer._fldDatatable(("SELECT accid,Alias,AccDescr from AccMast where AccDescr='" & txtac.Text & "'"), False)
            If (dt.Rows.Count > 0) Then
                txtac.Tag = dt(0)("accid")
            Else
                txtac.Text = ""
            End If
            If (Not fMList Is Nothing) Then
                fMList.Close()
                fMList = Nothing
            End If
        End If

    End Sub

    Private Sub txttargetAmt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txttargetAmt.KeyPress
        NumericTextOnKeypress(txttargetAmt, e, chgbyprg, numFormat)
    End Sub
    Private Sub setCommssionGrid()
        chgbyprg = True
        With grdSlab
            SetGridEditProperty(grdSlab)
            .ColumnCount = 3
            .Columns(0).HeaderText = "Target Upto"
            .Columns(0).Width = 100
            .Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(0).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(1).HeaderText = "Commission"
            .Columns(1).Width = 100
            .Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(1).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(2).HeaderText = "%?"
            .Columns(2).Width = 50
            .Columns(2).ReadOnly = True
            .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


        End With
        chgbyprg = False
    End Sub

    Private Sub chkslab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkslab.CheckedChanged
        plslab.Enabled = chkslab.Checked
    End Sub
    Private Sub AddSlabRow()
        Dim i As Integer
        With grdSlab
            activecontrolname = "grdSlab"
            i = .RowCount '- 1
            .Rows.Add(1)
            .CurrentCell = .Item(0, i)
            chgbyprg = True
            .BeginEdit(True)
            chgbyprg = False
        End With
        btnupdate.Enabled = True
    End Sub
    Private Sub RemoveSlabRow()
        If grdSlab.RowCount > 0 Then
            If MsgBox("You Are Going to Remove the Row." & vbCrLf & "Are You Sure ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then
                Exit Sub
            End If
            With grdSlab
                .Rows.RemoveAt(.CurrentRow.Index)
                .ClearSelection()
            End With
        End If
        btnupdate.Enabled = True
    End Sub

    Private Sub btnaddslab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddslab.Click
        AddSlabRow()
    End Sub

    Private Sub btnRemslab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemslab.Click
        RemoveSlabRow()
    End Sub

    Private Sub grdSlab_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSlab.CellClick
        If e.ColumnIndex = 2 Then
            With grdSlab
                .Item(e.ColumnIndex, e.RowIndex).Value = IIf(.Item(e.ColumnIndex, e.RowIndex).Value = "Y", "", "Y")
            End With
        End If
        chgbyprg = True
        grdSlab.BeginEdit(True)
        chgbyprg = False
    End Sub

    Private Sub grdSlab_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSlab.CellEndEdit
        With grdSlab
            Dim col As Integer = e.ColumnIndex
            Dim ndec1 As Integer
            If col = 0 Or col = 1 Then
                ndec1 = 2
                .Item(col, e.RowIndex).Value = Format(Convert.ToDecimal(.Item(col, e.RowIndex).Value), "#,##0" & IIf(ndec1 = 0, "", "." & Strings.StrDup(ndec1, "0")))
            End If
        End With
    End Sub

    Private Sub grdSlab_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdSlab.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If grdSlab.RowCount = 0 Then Exit Sub
                If FindNextCell(grdSlab, grdSlab.CurrentCell.RowIndex, grdSlab.CurrentCell.ColumnIndex + 1) Then
                    AddSlabRow()
                End If
nxt:
                chgbyprg = True
                grdSlab.BeginEdit(True)
                chgbyprg = False

            ElseIf e.KeyCode = Keys.F3 Then
                AddSlabRow()
            ElseIf e.KeyCode = Keys.F4 Then
                RemoveSlabRow()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub grdSlab_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSlab.Leave
        activecontrolname = ""
    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            Const WM_KEYDOWN As Integer = &H100
            Const WM_KEYUP As Integer = &H101
            'msg.ToString()
            If msg.Msg = WM_KEYDOWN Or msg.Msg = WM_KEYUP Then
                If activecontrolname = "grdSlab" Then
                    If msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up) Or msg.WParam.ToInt32() = CInt(Keys.Enter) Or msg.WParam.ToInt32() = CInt(Keys.F2) Or msg.WParam.ToInt32() = CInt(Keys.F3) Or msg.WParam.ToInt32() = CInt(Keys.F4) Then
                        If (msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up)) Then GoTo ctn
                        grdSlab_KeyDown(Nothing, New KeyEventArgs(keyData))
                        Return True
                    End If
                End If
            End If
ctn:
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub txtcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcode.TextChanged

    End Sub
End Class