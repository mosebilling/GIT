Public Class ChurchAdmissionFrm
#Region "Private Variabls"
    Private ismodi As Boolean
    Private chgbyprg As Boolean
    Private vdata As DataTable
    Private _dtRptTable As DataTable
    Private strQry As String
    Private yearFees As Double
#End Region
#Region "Class Objects"
    Private _objcmnbLayer As New clsCommon_BL
    Private _objTempInv As New clsTempleInv
#End Region
#Region "Form Objects"
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fRptFormat As RptFormatfrm
    Private WithEvents fsales As ChurchSalesFrm
#End Region
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub AddNew()
        clearControls()
        'btnupdate.Tag = IIf(getRight(141, CurrentUser), 1, 0)
        If userType Then
            btnupdate.Tag = IIf(getRight(141, CurrentUser), 1, 0)
        Else
            btnupdate.Tag = 1
        End If
        txtcode.Text = GenerateNext(txtcode.Text)
    End Sub
    Private Function GenerateNext(ByVal strCode As String) As String
        Dim N As Double
        Dim NewCode As String
        Dim i As Byte
        Dim f As Byte
        Dim tmp As String
        Dim dt As DataTable
        _objcmnbLayer = New clsCommon_BL
        If strCode = "" Then
            dt = _objcmnbLayer._fldDatatable("SELECT top 1 MemberCode from TempleMembershipTb order by memberid desc")
            If dt.Rows.Count > 0 Then
                strCode = dt(0)(0)
            End If
        End If
        If strCode = "" Then
            strCode = "TM"
        End If
        Dim dr As DataTable
        If strCode = "" Then Return strCode
        For i = 1 To 11
            If IsNumeric(Mid(strCode, Len(strCode) - i + 1, 1)) = False Then Exit For
            tmp = Val(Mid(strCode, Len(strCode) - i + 1))
            If Val(tmp) <> 0 Then
                N = Val(tmp)
            Else
                If N <> 0 Then Exit For
            End If
            If i = Len(strCode) Then i = i + 1 : Exit For
        Next i
        i = i - 1
        f = i
        If i <= 0 Then
            'i = txtCode.MaxLength - Len(strCode)
            i = 3
        End If
        tmp = ""
        NewCode = ""
        Try
            Do Until False
                N = N + 1
                tmp = Mid(strCode, 1, IIf(Len(strCode) <> 0, Len(strCode) - f, 0)) & Format(N, StrDup(i, "0"))
                dr = _objcmnbLayer._fldDatatable("SELECT MemberCode from TempleMembershipTb WHERE MemberCode = '" & tmp & "'")
                If dr.Rows.Count = 0 Then
                    NewCode = Mid(strCode, 1, IIf(Len(strCode) <> 0, Len(strCode) - f, 0)) & Format(N, StrDup(i, "0"))
                    NewCode = Mid(NewCode, 1, 30)
                    Exit Do
                End If
            Loop
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return NewCode
    End Function

    Private Sub TempleAdmissionFrm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        
        cldrdate.Focus()
    End Sub
    Private Sub getReceipts()
        If Val(btnaddreceipt.Tag) = 1 Then
            loadReceipt()
            getDueAmt()
            Dim dt As DataTable
            dt = _objcmnbLayer._fldDatatable("SELECT LastPaidDate FROM TempleMembershipTb WHERE memberid=" & Val(txtcode.Tag))
            If dt.Rows.Count > 0 Then
                If Not IsDBNull(dt(0)("LastPaidDate")) Then
                    lbllastdate.Text = DateValue(dt(0)("LastPaidDate"))
                End If
            End If
            btnaddreceipt.Tag = ""
        End If
    End Sub
    Private Sub TempleAdmissionFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim dt As DataTable

        dt = _objcmnbLayer._fldDatatable("SELECT YearFeesTemple FROM CompanyTb")
        If dt.Rows.Count > 0 Then
            If Not IsDBNull(dt(0)(0)) Then
                yearFees = dt(0)(0)
            End If
        End If
        'strQry = "SELECT MemberCode [M Code],Mdate [Join Date],MemberName [Member Name],FamilyName [Family Name],Addr1 [House Name],Phone,BloodGrp [Blood Group]," & _
        '                                    "Addr2,Addr3,Addr4,Email,Occupation," & _
        '                                    "case when MStatus=0 then 'Live' " & _
        '                                    "when MStatus=1 then 'Expired' " & _
        '                                    "when MStatus=2 then 'Transferd' end MstsText " & _
        '                                    ",case when Gender=0 then 'Male' " & _
        '                                    "when Gender=1 then 'Female' end Gender " & _
        '                                    ",case when Mgroup=0 then 'Member' " & _
        '                                    "when Mgroup=1 then 'Committee Member' end Mgroup,Designation,OpeningDueAmt,LivesIn,(DATEDIFF(m,isnull(LastPaidDate,Mdate),GETDATE()) *" & yearFees & ")+isnull(OpeningDueAmt,0) [Due Amt],memberid,1 lnk, " & _
        '                                    IIf(chkdate.Checked = 1, 1, 0) & " isdatewise,'" & Format(cldrStartDate.Value, "yyyy/MMM/dd") & "' fDate,'" & Format(cldrEnddate.Value, "yyyy/MMM/dd") & "' tDate FROM TempleMembershipTb " & _
        '                                    "left join (select sum(dealamt*-1) amt,Reference  from AccTrDet group by Reference ) rv on TempleMembershipTb.MemberCode=rv.Reference"
        setQuery()
        getReceipts()
        AddNew()
        btndelete.Text = "Clear"
        If userType Then
            btnupdate.Tag = IIf(getRight(141, CurrentUser), 1, 0)
        Else
            btnupdate.Tag = 1
        End If
        loadList()
        loadFamily()
        loadReceipt()
        loadprayargroup()
        Timer1.Enabled = True
        dtpinvoicedatefrom.Value = DateFrom
        dtpinvoicedateto.Value = DateTo
    End Sub
    Private Sub loadprayargroup()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select familiunitname from TempleMembershipTb group by familiunitname")
        Dim i As Integer
        cmbfamilyunit.Items.Clear()
        cmbfamilyunit.Items.Add("")
        For i = 0 To dt.Rows.Count - 1
            If Trim(dt(i)("familiunitname") & "") <> "" Then
                cmbfamilyunit.Items.Add(dt(i)("familiunitname"))
            End If

        Next
    End Sub

    Private Sub txtcode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtcode.KeyDown, txtName.KeyDown, txtAddr0.KeyDown, txtAddr1.KeyDown, _
                                                                                                              txtAddr2.KeyDown, txtAddr3.KeyDown, txtphone.KeyDown, txtemail.KeyDown, _
                                                                                                              txtbgroup.KeyDown, txtkallarano.KeyDown, txtDueAmt.KeyDown, txtfamilyname.KeyDown, _
                                                                                                              cmblives.KeyDown, txtoccupation.KeyDown, cmbgroup.KeyDown, txthousename.KeyDown, _
                                                                                                              txtward.KeyDown, txthouseno.KeyDown, cmbfamilyunit.KeyDown, txtadhar.KeyDown, _
                                                                                                              txtqualification.KeyDown, txtsubscription.KeyDown
        Dim myctr As Object
        myctr = sender
        If e.KeyCode = Keys.Return Then
            If myctr.name = "txtDueAmt" Then
                btnupdate.Focus()
            ElseIf myctr.name = "cmbfamilyunit" Then
                txtbgroup.Focus()
            Else
                SendKeys.Send("{TAB}")
            End If
        End If
    End Sub

    Private Sub txtDueAmt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDueAmt.KeyPress, txtsubscription.KeyPress
        NumericTextOnKeypress(sender, e, chgbyprg, numFormat)
    End Sub
    Private Sub loadList()
        setQuery()
        vdata = _objcmnbLayer._fldDatatable(strQry & getWhere())
        grdList.DataSource = vdata
        setGridHead()
    End Sub

    Private Sub setGridHead()
        With grdList
            If .ColumnCount = 0 Then Exit Sub
            SetGridProperty(grdList)

            .Columns("memberid").Visible = False
            .Columns("Family Name").Width = 250
            .Columns("Qualification").Width = 250
            .Columns("Member Name").Width = 250
            .Columns("House Name").Width = 250
            .Columns("Phone").Width = 150
            .Columns("Gender").Width = 100
            .Columns("DOB").Width = 100
            .Columns("Join Date").Visible = False
            cmbOrder.Items.Clear()
            Dim i As Integer = 0
            For i = 0 To .ColumnCount - 2
                cmbOrder.Items.Add(.Columns(i).HeaderText)
            Next
            For i = 13 To .ColumnCount - 1
                .Columns(i).Visible = False
            Next
            .Columns("Due Amt").Visible = True
            .Columns("Due Amt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Due Amt").DefaultCellStyle.Format = "N" & NoOfDecimal
            If cmbOrder.Items.Count > 0 Then cmbOrder.SelectedIndex = 2

        End With
        'resizeGridColumn(grdList, 2)
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        'resizeGridColumn(grdList, 2)
        resizeGridColumn(grdallMembers, 0)
        resizeGridColumn(grdWu, 0)
        resizeGridColumn(grdreceipt, 2)
        resizeGridColumn(dvinvoicelist, 3)
    End Sub

    Private Sub TempleAdmissionFrm_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Timer1.Enabled = True
    End Sub
    Private Sub loadFamily()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT fmembername,relation,occupation,gender,category,IsWU,WURoll,StudentStandard,StudentSchool,bloodgroup,LivesIn,familiid FROM TempleFamilyTb WHERE fkMembershipId=" & Val(txtcode.Tag))
        grdallMembers.DataSource = dt
        Dim i As Integer
        With grdallMembers
            SetGridProperty(grdallMembers)
            .Columns("fmembername").HeaderText = "Name"
            .Columns("relation").HeaderText = "Relation"
            .Columns("occupation").HeaderText = "Occupation"
            For i = 3 To .ColumnCount - 1
                .Columns(i).Visible = False
            Next
        End With
        Dim dtWU As DataTable
        dtWU = SearchGrid(dt, "True", 5, False)
        With grdWu
            .DataSource = dtWU
            SetGridProperty(grdWu)
            .Columns("fmembername").HeaderText = "Name"
            .Columns("WURoll").HeaderText = "Responsibility"
            .Columns("WURoll").Width = 100
            For i = 1 To .ColumnCount - 1
                If .Columns(i).HeaderText <> "Responsibility" Then
                    .Columns(i).Visible = False
                End If
            Next
        End With
        Dim dtStudent As DataTable
        dtStudent = SearchGrid(dt, 1, 4, False)
        With grdstudent
            .DataSource = dtStudent
            SetGridProperty(grdstudent)
            .Columns("fmembername").HeaderText = "Name"
            .Columns("StudentStandard").HeaderText = "Standared"
            .Columns("StudentStandard").Width = 100
            For i = 1 To .ColumnCount - 1
                If .Columns(i).HeaderText <> "Standared" Then
                    .Columns(i).Visible = False
                End If
            Next
        End With
        resizeGridColumn(grdallMembers, 0)
        resizeGridColumn(grdWu, 0)
        resizeGridColumn(grdstudent, 0)
    End Sub

    Private Sub TabControl2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl2.Click
        If TabControl2.SelectedIndex = 1 Then
            If btnModify.Text = "Undo" Then
                MsgBox("Do Undo/Update before moving to Enquiry", MsgBoxStyle.Exclamation)
                TabControl2.SelectedIndex = 0
                Exit Sub
            End If
            'resizeGridColumn(grdList, 2)
            btnupdate.Enabled = False
            btndelete.Enabled = False
            txtSeq.Focus()
        Else
            btnupdate.Enabled = True
            btndelete.Enabled = True
        End If
    End Sub

    Private Sub btnaddFamily_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddFamily.Click
        If Val(txtcode.Tag) = 0 Then MsgBox("Create Member First", MsgBoxStyle.Exclamation) : Exit Sub
        showFamily(0)
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        If Val(btnupdate.Tag) = 0 Then
            MsgBox("This user do not have permission to Update", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If txtName.Text = "" Then
            MsgBox("Invalid Name", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        setAndSaveValues()
    End Sub
    Private Function chkDuplication() As Boolean
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select memberid from TempleMembershipTb where MemberCode='" & txtcode.Text & "' and memberid<>" & Val(txtcode.Tag))
        If dt.Rows.Count > 0 Then
            MsgBox("Member Code Already Exist", MsgBoxStyle.Exclamation)
            txtcode.Focus()
            Return True
        End If
    End Function
    Private Sub setAndSaveValues()
        With _objTempInv
            .memberid = Val(txtcode.Tag)
            .Mdate = DateValue(cldrdate.Value)
            .MemberCode = txtcode.Text
            .MemberName = txtName.Text
            .Addr1 = txtAddr0.Text
            .Addr2 = txtAddr1.Text
            .Addr3 = txtAddr2.Text
            .Addr4 = txtAddr3.Text
            .FamilyName = txtfamilyname.Text
            .Phone = txtphone.Text
            .Email = txtemail.Text
            .BloodGrp = txtbgroup.Text
            .Occupation = txtoccupation.Text
            If rdoMlive.Checked Then
                .MStatus = 0
            ElseIf rdoMexpired.Checked Then
                .MStatus = 1
            Else
                .MStatus = 2
            End If
            .Gender = IIf(rdomale.Checked, 0, 1)
            .Mgroup = cmbgroup.SelectedIndex
            .Designation = txtkallarano.Text
            .OpeningDueAmt = CDbl(txtDueAmt.Text)
            .LivesIn = cmblives.SelectedIndex
            .UserId = CurrentUser
            .LastPaidDate = DateValue(clrrenewed.Value)
            .IsWU = chkwu.Checked
            .housename = txthousename.Text
            .houseno = txthouseno.Text
            .ward = txtward.Text
            .familyunitname = cmbfamilyunit.Text
            .kallarano = txtkallarano.Text
            .subscription = CDbl(txtsubscription.Text)
            .dob = DateValue(dtpdob.Value)
            .marriagedate = DateValue(dtpmarriage.Value)
            .adharno = txtadhar.Text
            .qualification = txtqualification.Text
            .dateofdeath = DateValue(dtpdateofdeath.Value)
            .ismarriage = chkmarriage.Checked
            .TempleMembershipTbSaveModify()
        End With
        MsgBox("Record Successfully Saved", MsgBoxStyle.Information)
        AddNew()
        loadList()
    End Sub
    Private Sub clearControls()
        txtName.Text = ""
        txtcode.Tag = ""
        txtAddr0.Text = ""
        txtAddr1.Text = ""
        txtAddr2.Text = ""
        txtAddr3.Text = ""
        txtfamilyname.Text = ""
        txtphone.Text = ""
        txtemail.Text = ""
        txtbgroup.Text = ""
        txtoccupation.Text = ""
        txtkallarano.Text = ""
        lblPendingAmt.Text = "0.00"
        txthousename.Text = ""
        txthouseno.Text = ""
        txtward.Text = ""
        cmbfamilyunit.Text = ""
        txtkallarano.Text = ""
        txtsubscription.Text = "0.00"
        txtadhar.Text = ""
        txtqualification.Text = ""
        txtDueAmt.Text = Format(0, numFormat)
        rdoMlive.Checked = True
        rdomale.Checked = True
        cmbgroup.SelectedIndex = 0
        cmblives.SelectedIndex = 0
        btnModify.Visible = False
        btndelete.Text = "Clear"
        btnModify.Text = "Modify"
        clrrenewed.Value = Date.Now
        cldrdate.Value = Date.Now
        dtpdob.Value = Date.Now
        dtpmarriage.Value = Date.Now
        chkmarriage.Checked = False

        lbllastdate.Text = ""
        chkwu.Checked = False
        loadFamily()
        loadReceipt(True)
        getDueAmt()
    End Sub
    Private Sub loadForEdit()
        clearControls()
        Dim dt As DataTable
        txtcode.Tag = Val(grdList.Item("memberid", grdList.CurrentRow.Index).Value)
        dt = _objcmnbLayer._fldDatatable("SELECT * FROM TempleMembershipTb WHERE memberid=" & Val(txtcode.Tag))
        If dt.Rows.Count > 0 Then
            txtcode.Text = dt(0)("MemberCode")
            txtcode.Tag = dt(0)("memberid")
            cldrdate.Value = dt(0)("Mdate")
            If Not IsDBNull(dt(0)("PaymentStartDt")) Then
                clrrenewed.Value = dt(0)("PaymentStartDt")
            Else
                clrrenewed.Value = dt(0)("Mdate")
            End If
            txtName.Text = dt(0)("MemberName")
            txtAddr0.Text = dt(0)("Addr1")
            txtAddr1.Text = dt(0)("Addr2")
            txtAddr2.Text = dt(0)("Addr3")
            txtAddr3.Text = dt(0)("Addr4")
            txtfamilyname.Text = dt(0)("FamilyName")
            txtphone.Text = dt(0)("Phone")
            txtemail.Text = dt(0)("Email")
            txtbgroup.Text = dt(0)("BloodGrp")
            txtoccupation.Text = dt(0)("Occupation")

            txthousename.Text = Trim(dt(0)("housename") & "")
            txthouseno.Text = dt(0)("houseno")
            txtward.Text = dt(0)("ward")
            cmbfamilyunit.Text = dt(0)("familiunitname")
            txtkallarano.Text = dt(0)("kallarano")
            If Val(dt(0)("subscription") & "") = 0 Then dt(0)("subscription") = 0
            txtsubscription.Text = Format(CDbl(dt(0)("subscription")), numFormat)
            dtpdob.Value = dt(0)("dob")
            dtpmarriage.Value = dt(0)("marriagedate")
            txtqualification.Text = dt(0)("qualification")
            txtadhar.Text = dt(0)("adharno")
            If Not IsDBNull(dt(0)("isWu")) Then
                chkwu.Checked = dt(0)("isWu")
            End If
            Select Case dt(0)("MStatus")
                Case 0
                    rdoMlive.Checked = True
                Case 1
                    rdoMexpired.Checked = True
                Case 2
                    rdoMtran.Checked = True
            End Select
            Select Case dt(0)("Gender")
                Case 0
                    rdomale.Checked = True
                Case 1
                    rdofemale.Checked = True
            End Select
            If Not IsDBNull(dt(0)("ismarriage")) Then
                chkmarriage.Checked = dt(0)("ismarriage")
            End If
            cmbgroup.SelectedIndex = dt(0)("Mgroup")
            txtkallarano.Text = dt(0)("Designation")
            If IsDBNull(dt(0)("OpeningDueAmt")) Then dt(0)("OpeningDueAmt") = 0
            txtDueAmt.Text = Format(CDbl(dt(0)("OpeningDueAmt")), numFormat)
            If Not IsDBNull(dt(0)("LastPaidDate")) Then
                lbllastdate.Text = dt(0)("LastPaidDate")
            Else
                lbllastdate.Text = clrrenewed.Value
            End If

            cmblives.SelectedIndex = dt(0)("LivesIn")
            loadFamily()
            loadReceipt()

            loadSales()
            getDueAmt()
            TabControl2.SelectedIndex = 0
            btnModify.Text = "Undo"
            btndelete.Text = "Delete"
            If userType Then
                btnupdate.Tag = IIf(getRight(142, CurrentUser), 1, 0)
                btndelete.Tag = IIf(getRight(143, CurrentUser), 1, 0)
            Else
                btnupdate.Tag = 1
                btndelete.Tag = 1
            End If
            btnModify.Visible = True
            btnupdate.Enabled = True
            btndelete.Enabled = True
            'calculatePending()
        End If
    End Sub
    Private Sub loadReceipt(Optional ByVal isclear As Boolean = False)
        If txtcode.Text = "" Then Exit Sub
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT PreFix + convert(varchar,JVNum) [RV No],JVDate [RV Date],EntryRef,DealAmt*-1 Amount,AccTrCmn.LinkNo from AccTrCmn " & _
                                         "LEFT JOIN AccTrDet ON AccTrCmn.LinkNo=AccTrDet.Linkno WHERE Reference='" & IIf(isclear, "000", txtcode.Text) & "'")
        grdreceipt.DataSource = dt
        With grdreceipt
            SetGridProperty(grdreceipt)
            .Columns("Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Amount").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("LinkNo").Visible = False
        End With
        Dim i As Integer
        Dim rvtotal As Double
        For i = 0 To grdreceipt.RowCount - 1
            rvtotal = rvtotal + CDbl(grdreceipt.Item("Amount", i).Value)
        Next
        lblrvtotal.Text = Format(rvtotal, numFormat)
        resizeGridColumn(grdreceipt, 2)
    End Sub

    Private Sub grdList_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdList.DoubleClick
        If grdList.RowCount > 0 Then
            loadForEdit()
        End If
    End Sub

    Private Sub btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnload.Click
        loadList()
    End Sub

    Private Sub btnModify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModify.Click
        If btnModify.Text = "Undo" Then
            AddNew()
            TabControl2.SelectedIndex = 1
            btnupdate.Enabled = False
            btndelete.Enabled = False
        Else
            TabControl2.SelectedIndex = 1
        End If
    End Sub

    Private Sub grdallMembers_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdallMembers.DoubleClick
        If grdallMembers.RowCount = 0 Then Exit Sub
        showFamily(Val(grdallMembers.Item(grdallMembers.ColumnCount - 1, grdallMembers.CurrentRow.Index).Value))
    End Sub
    Private Sub showFamily(ByVal memberid As Long)
        Dim frm As New ParshAddFamilyFrm
        With frm
            .MemberId = Val(txtcode.Tag)
            .txtname.Tag = memberid
            .ShowDialog()
        End With
        frm = Nothing
        loadFamily()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        If btndelete.Text = "Delete" Then
            If Val(btndelete.Tag) = 0 Then
                MsgBox("This user do not have permission to Delete", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            If MsgBox("Do you want Delete Member?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.No Then Exit Sub
            _objcmnbLayer._saveDatawithOutParm("Delete from TempleFamilyTb where fkMembershipId=" & Val(txtcode.Tag))
            _objcmnbLayer._saveDatawithOutParm("Delete from TempleMembershipTb where memberid=" & Val(txtcode.Tag))
            Dim dt As DataTable
            Dim linkno As Long
            dt = _objcmnbLayer._fldDatatable("SELECT Linkno from AccTrDet where Reference='" & txtcode.Text & "'")
            If dt.Rows.Count > 0 Then
                linkno = dt(0)("Linkno")
                _objcmnbLayer._saveDatawithOutParm("Delete from AccTrDet where linkno=" & linkno)
                _objcmnbLayer._saveDatawithOutParm("Delete from AccTrCmn where linkno=" & linkno)
            End If
        End If
        AddNew()
    End Sub

    Private Sub btnRemFamily_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemFamily.Click
        If MsgBox("Do you want Delete Family Member?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.No Then Exit Sub
        _objcmnbLayer._saveDatawithOutParm("Delete from TempleFamilyTb where familiid=" & Val(grdallMembers.Item(grdallMembers.ColumnCount - 1, grdallMembers.CurrentRow.Index).Value))
        loadFamily()
    End Sub

    Private Sub txtSeq_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeq.TextChanged
        grdList.DataSource = SearchGrid(vdata, txtSeq.Text, cmbOrder.SelectedIndex, Not chkSearch.Checked)
        _dtRptTable = SearchGrid(vdata, txtSeq.Text, cmbOrder.SelectedIndex, Not chkSearch.Checked)
    End Sub

    Private Sub rdoLive_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoLive.Click, rdoExpired.Click, rdoTransferd.Click, rdodue.Click
        loadList()
    End Sub


    Private Sub chkdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkdate.Click
        pldate.Enabled = chkdate.Checked
        Panel4.Enabled = chkdate.Checked
    End Sub

    Private Sub btnpreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpreview.Click
        Dim RptType As String
        Dim RptCaption As String = ""
        If TabControl2.SelectedIndex = 1 Then
            If rdodue.Checked Then
                RptType = "TALD"
            Else
                RptType = "CHL"
            End If
        Else
            RptType = "PRF"
        End If
        If chkFormat.Checked Then
            fRptFormat = New RptFormatfrm
            fRptFormat.RptType = RptType
            fRptFormat.ShowDialog()
            fRptFormat = Nothing
        Else
            Dim RptName As String
            RptName = getRptDefFlName(RptType, RptCaption)
            PrepareReport(RptName, "", False)
        End If

        
    End Sub

    Private Sub frm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles frm.FormClosed
        frm = Nothing
    End Sub

    Private Sub fRptFormat_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fRptFormat.FormClosed
        fRptFormat = Nothing
    End Sub
    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        PrepareReport(RptFlName, "", forPrint)
    End Sub
    Private Sub PrepareReport(ByVal FileName As String, ByVal RptCaption As String, ByVal forpint As Boolean)
        If Not frm Is Nothing Then
            frm.Close()
            frm = New ReprtviewNEWfrm
        Else
            frm = New ReprtviewNEWfrm
        End If
        Dim ds As New DataSet
        If TabControl2.SelectedIndex = 1 Then
            If _dtRptTable Is Nothing Then
                setQuery()
                'vdata = _objcmnbLayer._fldDatatable(strQry & getWhere() & " ORDER BY Mdate,MemberName")
                ds = _objcmnbLayer._ldDataset(strQry & getWhere(), False)
            Else
                ds.Tables.Add(_dtRptTable)
            End If
        Else
            _objTempInv = New clsTempleInv
            _objTempInv.memberid = Val(txtcode.Tag)
            ds = _objTempInv.returnMemberAdmissionDetails(1)
        End If
        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = fMainForm
        frm.Text = "Admission List"
        frm.Show()
    End Sub
    Private Function getWhere() As String
        Dim condition As String = ""
        If rdoLive.Checked Then
            condition = "MStatus=0"
        ElseIf rdoExpired.Checked Then
            condition = "MStatus=1"
        ElseIf rdoTransferd.Checked Then
            condition = "MStatus=2"
        ElseIf rdodue.Checked Then
            condition = "[Due Amt]>0"
        End If
        condition = " WHERE " & condition
        Dim dtdate As String = ""
        If rdobirth.Checked Then
            'dtdate = " AND [dob]>='" & Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd") & "' AND [dob]<='" & Format(DateValue(cldrEnddate.Value), "yyyy/MM/dd") & "'"
            dtdate = " AND [birthday]>='" & Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd") & "' AND [birthday]<='" & Format(DateValue(cldrEnddate.Value), "yyyy/MM/dd") & "' ORDER BY [birthday],[Member Name]"
        ElseIf rdomarriragedate.Checked Then
            dtdate = " AND isnull(ismarriage,0)>0 AND Marriage>='" & Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd") & "' AND Marriage<='" & Format(DateValue(cldrEnddate.Value), "yyyy/MM/dd") & "' ORDER BY [Marriage],[Member Name]"
        ElseIf rdodeathdate.Checked Then
            dtdate = " AND [Death Date]>='" & Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd") & "' AND [Death Date]<='" & Format(DateValue(cldrEnddate.Value), "yyyy/MM/dd") & "' ORDER BY [Death Date],[Member Name]"
        End If
        If chkdate.Checked Then
            'If rdodue.Checked Then
            'Else
            '    condition = condition & " AND Mdate>='" & Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd") & "' AND Mdate<='" & Format(DateValue(cldrEnddate.Value), "yyyy/MM/dd") & "'"
            'End If
            condition = condition & dtdate '" AND [Join Date]>='" & Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd") & "' AND [Join Date]<='" & Format(DateValue(cldrEnddate.Value), "yyyy/MM/dd") & "'"

        End If
        condition = condition
        Return condition
    End Function

    Private Sub btnaddreceipt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddreceipt.Click
        Dim paidDATE As Date
        Dim paidMonth As Integer
        Dim PaidAmt As Double
        Dim frm As New LastRenewedDateFrm
        With frm
            frm.Ldate = DateValue(clrrenewed.Value)
            frm.clrrenewed.Tag = CDbl(txtsubscription.Text)
            frm.lblamount.Tag = CDbl(txtDueAmt.Text)
            .rvAmt = CDbl(lblrvtotal.Text)
            .ShowDialog()
            paidDATE = .clrrenewed.Value
            If Val(.btnExit.Tag) = 0 Or Val(frm.lblamt.Text) = 0 Then Exit Sub
        End With
        paidMonth = DateDiff(DateInterval.Month, DateValue(clrrenewed.Value), paidDATE)
        If Val(frm.lblamt.Text) = 0 Then frm.lblamt.Text = 0
        PaidAmt = CDbl(frm.lblamt.Text)
        fMainForm.LoadRVO(0, txtcode.Text, , True, PaidAmt, Format(DateValue(paidDATE), "dd/MM/yyyy"))
        btnaddreceipt.Tag = 1
    End Sub
    Private Sub getDueAmt()
        If Val(txtcode.Tag) = 0 Then Exit Sub
        _objTempInv = New clsTempleInv
        _objTempInv.memberid = Val(txtcode.Tag)
        lblPendingAmt.Text = Format(_objTempInv.returnTempleMemberShipPendingAmt(), numFormat)
    End Sub

    Private Sub btneditreceipt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btneditreceipt.Click
        If grdreceipt.RowCount = 0 Then Exit Sub
        fMainForm.LoadRVO(Val(grdreceipt.Item("linkno", grdreceipt.CurrentRow.Index).Value), txtcode.Text, True)
        btnaddreceipt.Tag = 1
    End Sub

    Private Sub clrrenewed_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles clrrenewed.KeyDown
        If e.KeyCode = Keys.Return Then
            txtDueAmt.Focus()
        End If
    End Sub

    Private Sub cldrdate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cldrdate.KeyDown
        If e.KeyCode = Keys.Return Then
            txtName.Focus()
        End If
    End Sub

    Private Sub clrrenewed_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles clrrenewed.Validated
        If clrrenewed.Tag = "" Then Exit Sub
        calculatePending()
        clrrenewed.Tag = ""
        If lbllastdate.Text = "" Then
            lbllastdate.Text = Format(clrrenewed.Value, DtFormat)
        End If
    End Sub
    Private Sub calculatePending()
        Dim months As Integer
        months = DateDiff(DateInterval.Month, DateValue(clrrenewed.Value), Date.Now)
        lblPendingAmt.Text = Format((months * yearFees) + CDbl(txtDueAmt.Text) - CDbl(lblrvtotal.Text), numFormat)
    End Sub

    Private Sub clrrenewed_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles clrrenewed.ValueChanged
        clrrenewed.Tag = "chg"
    End Sub

    Private Sub txtDueAmt_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDueAmt.Validated
        calculatePending()
    End Sub
    Private Sub setQuery()

        strQry = "SELECT MemberCode [M Code],Mdate [Join Date],MemberName [Member Name],case when Gender=0 then 'Male' when Gender=1 then 'Female' end Gender ,'Self' Relation," & _
                                            "DOB,datediff(year,dob,getdate())Age,marriagedate [Marriage], Phone,adharno [Adhaar],Qualification,Occupation Profession," & _
                                            "case when MStatus=1  then dateofdeath else null end [Death Date]," & _
                                            "FamilyName [Family Name],Addr1 [House Name],BloodGrp [Blood Group],Addr2,Addr3,Addr4,Email," & _
                                            "case when MStatus=0 then 'Live' when MStatus=1 then 'Expired' when MStatus=2 then 'Transferd' end MstsText ," & _
                                            "case when Mgroup=0 then 'Member' when Mgroup=1 then 'Committee Member' end Mgroup," & _
                                            "Designation,OpeningDueAmt,LivesIn,CASE WHEN MStatus<>0 OR ISNULL(isWu,0)=1 THEN 0 ELSE (DATEDIFF(m,isnull(PaymentStartDt,Mdate),GETDATE()) *ISNULL(subscription,0))+isnull(OpeningDueAmt,0)-isnull(amt,0)End [Due Amt], " & _
                                            "memberid,1 lnk,ismarriage," & _
                                            IIf(chkdate.Checked, 1, 0) & " isdatewise,'" & Format(cldrStartDate.Value, "dd/MMM/yyyy") & "' fDate,'" & Format(cldrEnddate.Value, "dd/MMM/yyyy") & "' tDate,MStatus " & _
                                            " FROM TempleMembershipTb " & _
                                            "left join (select sum(dealamt*-1) amt,Reference  from AccTrDet group by Reference ) rv on TempleMembershipTb.MemberCode=rv.Reference"

        strQry = strQry & " UNION ALL SELECT MemberCode [M Code],Mdate [Join Date],fmembername [Member Name],case when TempleFamilyTb.Gender=0 then 'Male' when TempleFamilyTb.Gender=1 then 'Female' end Gender ,relation,TempleFamilyTb.dob,datediff(year,TempleFamilyTb.dob,getdate())Age," & _
                                            "TempleFamilyTb.marriagedate,TempleFamilyTb.phone,TempleFamilyTb.adharno,TempleFamilyTb.qualification, TempleFamilyTb.Occupation,case when TempleFamilyTb.MStatus=1 then TempleFamilyTb.dateofdeath else null end ,FamilyName [Family Name],Addr1 [House Name]," & _
                                            "bloodgroup [Blood Group],Addr2,Addr3,Addr4,Email," & _
                                            "case when TempleFamilyTb.MStatus=0 then 'Live' when TempleFamilyTb.MStatus=1 then 'Expired' when TempleFamilyTb.MStatus=2 then 'Transferd' end MstsText ," & _
                                            "case when Mgroup=0 then 'Member' when Mgroup=1 then 'Committee Member' end Mgroup," & _
                                            "Designation,OpeningDueAmt,case when TempleFamilyTb.LivesIn<0 then 0 else TempleFamilyTb.LivesIn end,CASE WHEN TempleFamilyTb.MStatus<>0 OR ISNULL(TempleFamilyTb.isWu,0)=1 THEN 0 " & _
                                            "ELSE (DATEDIFF(m,isnull(PaymentStartDt,Mdate),GETDATE()) *ISNULL(subscription,0))+isnull(OpeningDueAmt,0)-isnull(amt,0)End [Due Amt]," & _
                                            "memberid,1 lnk,ismarried," & _
                                            IIf(chkdate.Checked, 1, 0) & " isdatewise,'" & Format(cldrStartDate.Value, "dd/MMM/yyyy") & "' fDate,'" & Format(cldrEnddate.Value, "dd/MMM/yyyy") & "' tDate,TempleFamilyTb.MStatus " & _
                                            " FROM TempleMembershipTb left join TempleFamilyTb on TempleFamilyTb.fkMembershipId=TempleMembershipTb.memberid " & _
                                            "left join (select sum(dealamt*-1) amt,Reference  from AccTrDet group by Reference ) rv on TempleMembershipTb.MemberCode=rv.Reference"

        'If rdodue.Checked Then

        'End If
        strQry = "Select * from (Select *,DATEFROMPARTS(year(GETDATE()),month(DOB),day(DOB)) birthday from (" & strQry & ") t)tr "
    End Sub

    Private Sub rdoMexpired_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoMexpired.Click, rdoMlive.Click, rdoMtran.Click
        pldeath.Enabled = rdoMexpired.Checked
    End Sub

    Private Sub btnaddinvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddinvoice.Click
        ldNerchaSales()
    End Sub
    Private Sub ldNerchaSales(Optional ByVal keyid As Long = 0)
        If Val(txtcode.Tag) = 0 Then
            MsgBox("Invalid Member", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If Not fsales Is Nothing Then
            fsales.Close()
            fsales = Nothing
        End If
        fsales = New ChurchSalesFrm
        With fsales
            .MdiParent = fMainForm

            .Show()
            .txtCashCustomer.Tag = Val(txtcode.Tag)
            .txtDescr.Text = txtName.Text
            .loadFromMember()
            If keyid > 0 Then
                .isModi = True
                .CheckNLoad(keyid)
            End If
        End With
    End Sub
    Private Sub loadSales()
        Dim dt As DataTable
        dt = _objTempInv.returnMembershipsales(DateValue(dtpinvoicedatefrom.Value), DateValue(dtpinvoicedateto.Value), Val(txtcode.Tag)).Tables(0)
        dvinvoicelist.DataSource = dt
        SetGridProperty(dvinvoicelist)
        With dvinvoicelist
            .Columns("trid").Visible = False
            .Columns("total").DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
            .Columns("total").DefaultCellStyle.Format = "N" & NoOfDecimal
            resizeGridColumn(dvinvoicelist, 3)
        End With
    End Sub

    Private Sub btnloadinvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnloadinvoice.Click
        loadSales()
    End Sub

    Private Sub fsales_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fsales.FormClosed
        fsales = Nothing
        loadSales()
    End Sub

    Private Sub TabControl1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.Click
        If TabControl1.SelectedIndex = 1 Then
            If dvinvoicelist.Columns.Count > 0 Then
                resizeGridColumn(dvinvoicelist, 3)

            End If
            If grdreceipt.Columns.Count > 0 Then
                resizeGridColumn(grdreceipt, 2)
            End If
        End If
    End Sub

    Private Sub btneditbill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btneditbill.Click
        If dvinvoicelist.Rows.Count > 0 Then
            If Not dvinvoicelist.CurrentRow Is Nothing Then
                Dim mid As Integer = Val(dvinvoicelist.Item("Trid", dvinvoicelist.CurrentRow.Index).Value)
                ldNerchaSales(mid)
            End If
        End If
    End Sub

    Private Sub dvinvoicelist_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dvinvoicelist.DoubleClick
        If dvinvoicelist.Rows.Count > 0 Then
            If Not dvinvoicelist.CurrentRow Is Nothing Then
                Dim mid As Integer = Val(dvinvoicelist.Item("trid", dvinvoicelist.CurrentRow.Index).Value)
                ldNerchaSales(mid)
            End If
        End If
    End Sub

    Private Sub chkdate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkdate.CheckedChanged

    End Sub

    Private Sub grdList_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdList.CellContentClick

    End Sub

    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        loadFamily()
        loadReceipt()
        loadSales()
        getDueAmt()
    End Sub

    Private Sub chkmarriage_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkmarriage.CheckedChanged
        Label29.Enabled = chkmarriage.Checked
        dtpmarriage.Enabled = chkmarriage.Checked
    End Sub
End Class