Public Class SchoolTeacherFrm
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
            MsgBox("Invalid Teacher Code!", MsgBoxStyle.Exclamation)
            txtcode.Focus()
            Exit Sub
        End If
        If txtname.Text = "" Then
            MsgBox("Invalid Teacher Name!", MsgBoxStyle.Exclamation)
            txtname.Focus()
            Exit Sub
        End If
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("select salesmanid from  SalesmanTb where SManCode='" & txtcode.Text & "'")
        If dt.Rows.Count > 0 Then
            If dt(0)("salesmanid") <> Val(txtcode.Tag) Then
                MsgBox("Teacher Code already exist", MsgBoxStyle.Exclamation)
                txtcode.Focus()
                Exit Sub
            End If
        End If
        dt = _objcmnbLayer._fldDatatable("select salesmanid from  SalesmanTb where SManName='" & txtname.Text & "'")
        If dt.Rows.Count > 0 Then
            If dt(0)("salesmanid") <> Val(txtcode.Tag) Then
                MsgBox("Teacher Name already exist", MsgBoxStyle.Exclamation)
                txtname.Focus()
                Exit Sub
            End If
        End If
        Dim branchid As Integer
        dt = _objcmnbLayer._fldDatatable("select BranchId from BranchTb where Branchcode='" & UsrBr & "'")
        If dt.Rows.Count > 0 Then
            branchid = dt(0)("BranchId")
        End If
        If Val(txtcode.Tag) > 0 Then
            _objcmnbLayer._saveDatawithOutParm("Update SalesmanTb set SManCode ='" & txtcode.Text & "', SManName='" & txtname.Text & _
                                               "',accountno=" & Val(txtac.Tag) & ",comP=0" & _
                                               ",DisP=0,targetAmt=0" & _
                                               ",empcode ='" & txtempcode.Text & "',branchid=" & branchid & ",phone='" & txtphone.Text & "' where salesmanid=" & Val(txtcode.Tag))
        Else
            _objcmnbLayer._saveDatawithOutParm("Insert into SalesmanTb (SManCode,SManName,accountno,comP,DisP,targetAmt,empcode,branchid,isschoolteacher,phone) values('" & _
                                               txtcode.Text & "','" & txtname.Text & "'," & Val(txtac.Tag) & ",0," & _
                                               "0,0,'" & txtempcode.Text & "'," & branchid & ",1,'" & txtphone.Text & "')")
        End If
        
        MsgBox("Teacher Name Updated", MsgBoxStyle.Information)
        isnewSalesmanAdded = True
        loadSalesman()
        clearCtrl()
    End Sub
    Private Sub clearCtrl()
        chgbyprg = True
        txtname.Text = ""
        txtcode.Text = ""
        txtcode.Tag = 0
        txtac.Text = ""
        txtac.Tag = ""
        txtempcode.Text = ""
        txtphone.Text = ""
        txtcode.Focus()
        chgbyprg = False
    End Sub
    Private Sub loadSalesman()
        Try
            Dim dtset As DataSet
            Dim ListTb As DataTable
            Dim str As String
            str = "SELECT SManCode,SManName,ISNULL(AccDescr,'') AccDescr,SalesmanTb.comP,SalesmanTb.DisP,SalesmanTb.accountno," & _
                                                 "salesmanid,isnull(targetAmt,0)targetAmt,empcode,phone FROM SalesmanTb left join BranchTb on BranchTb.branchid=SalesmanTb.branchid " & _
                                                 "LEFT JOIN AccMast ON SalesmanTb.accountno=AccMast.Accid where isnull(isschoolteacher,0)=1 " & IIf(UsrBr = "", "", "and Branchcode='" & UsrBr & "'")
            dtset = _objcmnbLayer._ldDataset(str, False)
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
                            .Items.Item(i).SubItems(2).Text = .Items.Add(ListTb(i)("Phone"))
                        Else
                            .Items.Item(i).SubItems.Insert(2, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, ListTb(i)("Phone")))
                        End If

                        If .Items.Item(i).SubItems.Count > 3 Then
                            .Items.Item(i).SubItems(3).Text = .Items.Add(ListTb(i)("Phone"))
                        Else
                            .Items.Item(i).SubItems.Insert(3, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, ListTb(i)("Phone")))
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
        txtphone.Text = lstContent.SelectedItems(0).SubItems(2).Text
        'txtac.Text = lstContent.SelectedItems(0).SubItems(2).Text
        txtac.Tag = lstContent.SelectedItems(0).SubItems(5).Text
        txtempcode.Text = lstContent.SelectedItems(0).SubItems(7).Text
        txtcode.Tag = Val(lstContent.SelectedItems(0).SubItems(8).Text)
        txtcode.Focus()
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
        loadSalesman()
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
        strquery = "SELECT TOP 1 Trid FROM (select TOP 1 trid from  ItmInvCmnTb where SlsManId='" & txtcode.Text & "'"
        strquery = strquery & " UNION ALL select TOP 1 trid from  ItmInvTrTb where Smancode='" & txtcode.Text & "'"
        strquery = strquery & " UNION ALL select TOP 1 LINKNO from  AccTrCmn where collectedOrDeliveredBy='" & txtcode.Text & "'"
        strquery = strquery & "UNION ALL select TOP 1 studentid from  SchoolStudentAdmissionTb where classteacherid=" & Val(txtcode.Tag) & ") tr"
        dt = _objcmnbLayer._fldDatatable(strquery)
        If dt.Rows.Count > 0 Then
            If Val(dt(0)("trid") & "") <> 0 Then
                MsgBox("Teacher Name found in transactions! you cannot delete", MsgBoxStyle.Exclamation)
                txtname.Focus()
                Exit Sub
            End If
        End If
        If MsgBox("Do you want to remove the Teacher?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
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

    Private Sub numcom_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub numcom_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
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

    Private Sub numcom_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

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

    Private Sub txtname_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtname.TextChanged

    End Sub

    Private Sub txtphone_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtphone.KeyDown
        If e.KeyCode = Keys.Return Then btnupdate.Focus()
    End Sub

    Private Sub txtphone_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtphone.TextChanged

    End Sub
End Class