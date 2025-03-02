Public Class EmployeeMasterFrm
    Private WithEvents fCrtAcc As CreateAcc
    Private _objcmnbLayer As New clsCommon_BL
    Private _objpayroll As New clsPayroll
    Private chgBypgm As Boolean
    Private _dtTable As DataTable
    Private _rptTable As DataTable
    Private WithEvents fMList As Mlistfrm
    Private WithEvents fSelect As Selectfrm
    Private lstKey As Integer
    Private _srchTxtId As Integer
    Private _srchOnce As Boolean
    Private _srchIndexId As Byte
    Private MyActiveControl As New Object
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fRptFormat As RptFormatfrm
    Private chgbyprg As Boolean
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        If MsgBox("Do you want to exit?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        Me.Close()
    End Sub

    Private Sub EmployeeMasterFrm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtname.Focus()
    End Sub

    Private Sub EmployeeMasterFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ldDesignations()
        ldDepartments()
        txtCode.Text = GenerateNext("")
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
            dt = _objcmnbLayer._fldDatatable("SELECT top 1 empcode from EmpMasterTb order by empid desc")
            If dt.Rows.Count > 0 Then
                strCode = dt(0)(0)
            End If
        End If
        If strCode = "" Then
            strCode = "EMP"
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
                dr = _objcmnbLayer._fldDatatable("SELECT empid from EmpMasterTb WHERE empcode = '" & tmp & "'")
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
    Private Sub AddNew()
        Dim code As String = ""
        If txtCode.Text <> "" Then
            code = txtCode.Text
        End If
        clearControl()
        txtCode.Text = GenerateNext(code)
        txtname.Focus()
    End Sub
    Private Sub clearControl()
        chgBypgm = True
        For Each Control In GroupBox1.Controls
            If TypeOf (Control) Is TextBox Then
                Control.text = ""
                Control.tag = ""
            End If
            If TypeOf (Control) Is CheckBox Then
                Control.checked = False
                Control.tag = ""
            End If
        Next
        txtvisa.Text = ""
        dtpvisaexpiry.Value = DateValue(Date.Now)
        txtidno.Text = ""
        dtpidexpiry.Value = DateValue(Date.Now)
        txtlicense.Text = ""
        dtplicenseexpiry.Value = DateValue(Date.Now)
        txtpassport.Text = ""
        dtppassportexpiry.Value = DateValue(Date.Now)
        rdomale.Checked = True
        rdolabour.Checked = True
        cmbdesignation.Text = ""
        txtaccount.Text = ""
        txtaccount.Tag = ""
        rdowages.Checked = True
        rdodaily.Checked = True
        txtdailyPay.Text = Format(0, numFormat)
        txtsalary.Text = Format(0, numFormat)
        btnresign.Enabled = False
        btnRemove.Enabled = False
        btnclear.Text = "&New"
        rdodaily.Enabled = True
        rdounit.Enabled = True
        rdoMonthly.Enabled = False
        cmbdepartment.Text = ""
        cmbdesignation.Text = ""
        txtbank.Text = ""
        txtlabourid.Text = ""
        dtplabourexpiry.Value = DateValue(Date.Now)
        chgBypgm = False
    End Sub
    Private Sub ldDesignations()
        AddtoCombo(cmbdesignation, "Select DesignationName from DesignationTb", True)
    End Sub
    Private Sub ldDepartments()
        AddtoCombo(cmbdepartment, "Select departmentname from DepartmentTb", True)
    End Sub

    Private Sub btnrefreshDesignation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefreshDesignation.Click
        Dim frm As New DesignationCreation
        frm.isClose = True
        frm.ShowDialog()
        ldDesignations()
    End Sub
    Public Sub QuickStaff()
        fCrtAcc = New CreateAcc
        With fCrtAcc
            .Condition = "GrpSetOn In ('Staff')"
            .bOnlyOne = True
            .addNew()
            .txtRec1.Text = txtname.Text
            .cmdOk.Enabled = True
            .txtphone.Text = txtphone.Text
            .txtemail.Text = txtemail.Text
            .ShowDialog()
        End With
    End Sub

    Private Sub btngenerateAcc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btngenerateAcc.Click
        If txtname.Text = "" Then
            MsgBox("Invalid Staff Name", MsgBoxStyle.Exclamation)
            txtname.Focus()
            Exit Sub
        End If
        If Val(txtaccount.Tag) > 0 Then
            MsgBox("Account already created", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        QuickStaff()
    End Sub

    Private Sub fCrtAcc_OpenAccMaster(ByRef AccountNo As Long) Handles fCrtAcc.OpenAccMaster
        Dim dt As DataTable
        chgBypgm = True
        dt = _objcmnbLayer._fldDatatable("SELECT accid,AccDescr from AccMast where accid=" & AccountNo)
        If dt.Rows.Count > 0 Then
            txtaccount.Text = dt(0)("AccDescr")
            txtaccount.Tag = dt(0)("accid")
        End If
        txtdailyPay.Focus()
        chgBypgm = False
    End Sub

    Private Sub txtCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCode.KeyDown, txtname.KeyDown, txtemail.KeyDown, _
                                                                                                              cmbdesignation.KeyDown, txtdailyPay.KeyDown, txtsalary.KeyDown, _
                                                                                                              txtphone.KeyDown
        Dim myctrl As Control
        myctrl = sender
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
            If myctrl.Name = "txtdailyPay" Or myctrl.Name = "txtsalary" Then
                txtbank.Focus()
            Else
                SendKeys.Send("{TAB}")
            End If
        End If
        
        
    End Sub


    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        AddNew()
    End Sub

    Private Sub dtpjdate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtpjdate.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtdailyPay_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtdailyPay.KeyPress
        NumericTextOnKeypress(txtdailyPay, e, chgBypgm, numFormat)
    End Sub

    Private Sub txtsalary_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtsalary.KeyPress
        NumericTextOnKeypress(txtsalary, e, chgBypgm, numFormat)
    End Sub




    Private Sub txtCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged, txtname.TextChanged, _
                                                                                                        txtTrDescr.TextChanged, txtemail.TextChanged, txtphone.TextChanged, _
                                                                                                        txtdailyPay.TextChanged, txtsalary.TextChanged, txtbank.TextChanged
        If chgBypgm Then Exit Sub
        BtnUpdate.Enabled = True
    End Sub

    Private Sub rdowages_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdowages.Click, rdodaily.Click, rdofemale.Click, rdolabour.Click, rdomale.Click, _
                                                                                            rdoMonthly.Click, rdosalary.Click, rdostaff.Click, rdounit.Click

        If sender.name <> "rdounit" Then
            If rdowages.Checked Then
                lbldpay.Enabled = True
                txtdailyPay.Enabled = True
                lblmpay.Enabled = False
                txtsalary.Enabled = False
                rdodaily.Enabled = True
                rdounit.Enabled = True
                rdoMonthly.Enabled = False
                rdodaily.Checked = True
            Else
                lbldpay.Enabled = False
                txtdailyPay.Enabled = False
                lblmpay.Enabled = True
                txtsalary.Enabled = True
                rdodaily.Enabled = False
                rdounit.Enabled = False
                rdoMonthly.Enabled = True
                rdoMonthly.Checked = True
            End If
        End If


        If rdounit.Checked Then
            lbldpay.Text = "Rate/Unit"
        Else
            lbldpay.Text = "Daily Pay"
        End If
        BtnUpdate.Enabled = True
    End Sub

    Private Sub BtnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUpdate.Click
        verify()
    End Sub
    Private Sub verify()
        If txtCode.Text = "" Then
            MsgBox("Inavalid Employee Code", MsgBoxStyle.Exclamation)
            txtCode.Focus()
            Exit Sub
        End If
        If txtname.Text = "" Then
            MsgBox("Inavalid Employee Name", MsgBoxStyle.Exclamation)
            txtname.Focus()
            Exit Sub
        End If
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select empid from EmpMasterTb where empcode='" & txtCode.Text & "' and empid<>" & Val(txtCode.Tag))
        If dt.Rows.Count > 0 Then
            MsgBox("Employee already exist", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        saveEmp()
        returnMaster()
    End Sub
    Private Sub saveEmp()
        With _objpayroll
            .empid = Val(txtCode.Tag)
            .empcode = txtCode.Text
            .empname = txtname.Text
            .empaddress = txtTrDescr.Text
            .emptype = IIf(rdolabour.Checked, 0, 1)
            .gender = IIf(rdomale.Checked, 0, 1)
            .phone = txtphone.Text
            .emailid = txtemail.Text
            .joindate = DateValue(dtpjdate.Value)
            .ledgerAcc = Val(txtaccount.Tag)
            .salaryType = IIf(rdowages.Checked, 0, 1)
            Dim scategory As Integer
            If rdodaily.Checked Then
                scategory = 0
            ElseIf rdounit.Checked Then
                scategory = 1
            Else
                scategory = 2
            End If
            .salarycategory = scategory
            If Val(txtdailyPay.Text) = 0 Then txtdailyPay.Text = 0
            .dailyPay = CDbl(txtdailyPay.Text)
            If Val(txtsalary.Text) = 0 Then txtsalary.Text = 0
            .monthlyPay = CDbl(txtsalary.Text)
            .Designation = cmbdesignation.Text
            .departmentname = cmbdepartment.Text
            .bankdetails = txtbank.Text
            .visanumber = txtvisa.Text
            .visaexpiry = DateValue(dtpvisaexpiry.Value)
            .idcard = txtidno.Text
            .idcarexpiry = DateValue(dtpidexpiry.Value)
            .licensenumber = txtlicense.Text
            .licenseexpiry = DateValue(dtplicenseexpiry.Value)
            .passportnumber = txtpassport.Text
            .passportexpiry = DateValue(dtppassportexpiry.Value)
            .labourid = txtlabourid.Text
            .labourexpiry = DateValue(dtplabourexpiry.Value)
            ._saveEmp()
        End With
        MsgBox("Employee Details updated successfully", MsgBoxStyle.Information)
        AddNew()
    End Sub
    Private Function loadEmployeeMaster() As DataTable
        Dim condition As String = ""
        If rdoActive.Checked Then
            condition = " isnull(empstatus,0)=0 "
        ElseIf rdoresigined.Checked Then
            condition = " isnull(empstatus,0)=1 "
        End If
        If rdovisaexpiry.Checked Then
            condition = condition & IIf(condition <> "", " and ", "") & " isnull(visanumber,'')<>'' and visaexpiry>='" & Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd") & "'" & _
            " and visaexpiry<='" & Format(DateValue(cldrEnddate.Value), "yyyy/MM/dd") & "'"
        End If
        If rdoidexpiry.Checked Then
            condition = condition & IIf(condition <> "", " and ", "") & " isnull(idcard,'')<>'' and idcarexpiry>='" & Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd") & "'" & _
            " and idcarexpiry<='" & Format(DateValue(cldrEnddate.Value), "yyyy/MM/dd") & "'"
        End If
        If rdolicense.Checked Then
            condition = condition & IIf(condition <> "", " and ", "") & " isnull(licensenumber,'')<>'' and licenseexpiry>='" & Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd") & "'" & _
            " and licenseexpiry<='" & Format(DateValue(cldrEnddate.Value), "yyyy/MM/dd") & "'"
        End If
        If rdopassportExpiry.Checked Then
            condition = condition & IIf(condition <> "", " and ", "") & " isnull(passportnumber,'')<>'' and passportexpiry>='" & Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd") & "'" & _
            " and passportexpiry<='" & Format(DateValue(cldrEnddate.Value), "yyyy/MM/dd") & "'"
        End If
        If condition <> "" Then
            condition = " where " & condition
        End If
        
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select empcode [Code],empname [Emp Name],case when isnull(emptype,0)=0 then 'Labour' else 'Staff' end [Emp Type]," & _
                                              "isnull(DesignationName,'')Designation,isnull(departmentname,'')Department," & _
                                               "case when isnull(gender,0)=0 then 'Male' else 'Female' end Gender,Phone,joindate [Join Date]," & _
                                               "case when isnull(empstatus,0)=0 then 'Active' else 'Resigned' end [Status],empid,1 lnk," & _
                                               "visanumber [Visa Number],visaexpiry [Visa Expiry],idcard [ID Card No],idcarexpiry [ID Expiry]" & _
                                               ",licensenumber [License Number],licenseexpiry [lic. Expiry],passportnumber [Passport Number],passportexpiry [Pass. Expiry]  from EmpMasterTb " & _
                                               "left join DesignationTb on EmpMasterTb.DesignationId=DesignationTb.DesignationId " & _
                                               "left join DepartmentTb on EmpMasterTb.departmentid=DepartmentTb.departmentid" & _
                                               condition)
        Return dt
    End Function
    Private Sub returnMaster()
        _dtTable = loadEmployeeMaster()
        grdItem.DataSource = _dtTable
        setGridHead()
    End Sub
    Private Sub setGridHead()
        SetGridProperty(grdItem)
        With grdItem
            .Columns("empid").Visible = False
            .Columns("Designation").Width = 200
            .Columns("Department").Width = 150
            .Columns("Gender").Width = 50
            .Columns("lnk").Visible = False
        End With
        Dim i As Integer
        cmbSearch.Items.Clear()
        For i = 0 To grdItem.ColumnCount - 1
            cmbSearch.Items.Add(grdItem.Columns(i).HeaderText)
        Next
        If cmbSearch.Items.Count > 0 Then cmbSearch.SelectedIndex = 1

    End Sub

    Private Sub TabControl1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.Click
        If TabControl1.SelectedIndex = 1 Then
            If btnclear.Text = "Undo" Then
                MsgBox("Do Undo/Update before moving to List", MsgBoxStyle.Exclamation)
                TabControl1.SelectedIndex = 0
                txtname.Focus()
                Exit Sub
            End If
            If grdItem.RowCount = 0 Then
                returnMaster()
            End If
        End If
        'resizeGridColumn(grdItem, 1)
    End Sub

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        returnMaster()
    End Sub

    Private Sub rdoActive_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoActive.Click, rdoresigined.Click, rdonone.Click
        returnMaster()
    End Sub
    Private Sub ldEmpRec(ByVal empid As Integer)
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select EmpMasterTb.*,isnull(AccDescr,'')AccDescr,isnull(DesignationName,'')DesignationName," & _
                                         "isnull(departmentname,'')departmentname  from EmpMasterTb " & _
                                        "left join Accmast on EmpMasterTb.ledgerAcc=Accmast.accid " & _
                                        "left join DesignationTb on EmpMasterTb.DesignationId=DesignationTb.DesignationId " & _
                                        "left join DepartmentTb on EmpMasterTb.departmentid=DepartmentTb.departmentid " & _
                                        "where empid=" & empid)
        chgBypgm = True
        If dt.Rows.Count > 0 Then
            txtCode.Text = dt(0)("empcode")
            txtCode.Tag = dt(0)("empid")
            txtname.Text = dt(0)("empname")
            If dt(0)("emptype") = 0 Then
                rdolabour.Checked = True
            Else
                rdostaff.Checked = True
            End If
            If dt(0)("gender") = 0 Then
                rdomale.Checked = True
            Else
                rdofemale.Checked = True
            End If
            txtTrDescr.Text = dt(0)("empaddress")
            txtphone.Text = dt(0)("phone")
            txtemail.Text = dt(0)("emailid")
            txtbank.Text = Trim(dt(0)("bankdetails") & "")
            dtpjdate.Value = dt(0)("joindate")
            If Trim(dt(0)("AccDescr") & "") = "" Then
                txtaccount.Tag = 0
                txtaccount.Text = ""
            Else
                txtaccount.Tag = Val(dt(0)("ledgerAcc") & "")
                txtaccount.Text = Trim(dt(0)("AccDescr") & "")
            End If
            
            If dt(0)("salaryType") = 0 Then
                rdowages.Checked = True
            Else
                rdosalary.Checked = True
            End If
            rdoMonthly.Enabled = False
            rdounit.Enabled = False
            rdodaily.Enabled = False
            txtdailyPay.Enabled = False
            txtsalary.Enabled = False
            lblmpay.Enabled = False
            lbldpay.Enabled = False
            If dt(0)("salarycategory") = 0 Then
                rdodaily.Checked = True
                rdounit.Enabled = True
                rdodaily.Enabled = True
                txtdailyPay.Enabled = True
                lbldpay.Enabled = True
            ElseIf dt(0)("salarycategory") = 1 Then
                rdounit.Checked = True
                rdounit.Enabled = True
                rdodaily.Enabled = True
                txtdailyPay.Enabled = True
                lbldpay.Enabled = True
            Else
                rdoMonthly.Checked = True
                rdoMonthly.Enabled = True
                txtsalary.Enabled = True
                lblmpay.Enabled = True
            End If
            If Val(dt(0)("dailyPay")) > 0 Then
                txtdailyPay.Text = Format(CDbl(dt(0)("dailyPay")), numFormat)
            Else
                txtdailyPay.Text = Format(0, numFormat)
            End If
            If Val(dt(0)("monthlyPay")) > 0 Then
                txtsalary.Text = Format(CDbl(dt(0)("monthlyPay")), numFormat)
            Else
                txtsalary.Text = Format(0, numFormat)
            End If
            If dt(0)("empstatus") = "False" Then
                lblstatus.Text = "Active"
            Else
                lblstatus.Text = "Resigned at " & dt(0)("resignedDate")
            End If
            lblstatus.Tag = dt(0)("empstatus")
            cmbdesignation.Text = dt(0)("DesignationName")
            cmbdepartment.Text = dt(0)("departmentname")
            txtvisa.Text = Trim(dt(0)("visanumber") & "")
            If Not IsDBNull(dt(0)("visaexpiry")) Then
                dtpvisaexpiry.Value = DateValue(dt(0)("visaexpiry"))
            End If
            txtidno.Text = Trim(dt(0)("idcard") & "")
            If Not IsDBNull(dt(0)("idcarexpiry")) Then
                dtpidexpiry.Value = DateValue(dt(0)("idcarexpiry"))
            End If
            txtlicense.Text = Trim(dt(0)("licensenumber") & "")
            If Not IsDBNull(dt(0)("licenseexpiry")) Then
                dtplicenseexpiry.Value = DateValue(dt(0)("licenseexpiry"))
            End If
            txtpassport.Text = Trim(dt(0)("passportnumber") & "")
            If Not IsDBNull(dt(0)("passportexpiry")) Then
                dtppassportexpiry.Value = DateValue(dt(0)("passportexpiry"))
            End If
            txtlabourid.Text = Trim(dt(0)("labourid") & "")
            If Not IsDBNull(dt(0)("labourexpiry")) Then
                dtplabourexpiry.Value = DateValue(dt(0)("labourexpiry"))
            End If
            TabControl1.SelectedIndex = 0
            btnRemove.Enabled = True
            btnresign.Visible = True
            txtname.Focus()
            btnclear.Text = "Undo"
        End If
        chgBypgm = False
    End Sub

    Private Sub grdItem_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItem.CellDoubleClick
        If grdItem.RowCount = 0 Then Exit Sub
        ldEmpRec(Val(grdItem.Item("empid", e.RowIndex).Value))
    End Sub

    Private Sub btnedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnedit.Click
        If grdItem.RowCount = 0 Then Exit Sub
        ldEmpRec(Val(grdItem.Item("empid", grdItem.CurrentRow.Index).Value))
    End Sub

    Private Sub txtaccount_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtaccount.KeyDown
        lstKey = e.KeyCode
        Dim myctrl As TextBox
        myctrl = sender
        If e.KeyCode = Keys.Enter Then
            txtdailyPay.Focus()
        End If
        If e.KeyCode = Keys.Up Or e.KeyCode = Keys.PageUp Then
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
        ElseIf e.KeyCode = Keys.F2 Then
            If txtname.Text = "" Then Exit Sub
            ldSelect(2)
        End If
    End Sub
    Private Sub ldSelect(ByVal BVal As Single)
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
        fSelect = New Selectfrm
        'nSelect = BVal
        SetForm(fSelect, BVal)
        fSelect.Width = 742
        fSelect.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
        fSelect.Show()
    End Sub

    Private Sub txtaccount_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtaccount.TextChanged
        If txtname.Text = "" Then Exit Sub
        If chgBypgm = True Then Exit Sub
        Dim myCtrl As Control = sender
        Select Case myCtrl.Name
            Case "txtaccount"
                _srchTxtId = 1
        End Select
        _srchOnce = False
        ShowFmlist(sender)
    End Sub
    Private Sub ShowFmlist(ByVal myCtrl As Control)
        If chgBypgm Then Exit Sub
        MyActiveControl = myCtrl
        If Not _srchOnce Then
            chgbyprg = True
            If fMList Is Nothing Then
                fMList = New Mlistfrm
            End If
            Dim PopupLoc As Point
            Dim x As Integer = Me.Width - fMList.Width
            Dim y As Integer = Me.Height - fMList.Height - 10
            PopupLoc = New Point(x, y)
            If fMList.Visible = False Then
                Select Case _srchTxtId
                    Case 1
                        SetFmlist(fMList, 28)
                End Select
                fMList.loc = PopupLoc
                fMList.Show(fMainForm)
                fMList.Visible = True
            End If
        End If
        Select Case _srchTxtId
            Case 1   'Staff
                fMList.SearchIndex = 0
                fMList.SearchIndexDescr = 1
                fMList_doFocus()
                fMList.Search(txtaccount.Text)
                fMList.AssignList(txtaccount, lstKey, chgBypgm)
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
        chgBypgm = True
        Select Case _srchTxtId
            Case 1
                txtaccount.Text = ItmFlds(0)
                txtaccount.Tag = ItmFlds(3)
        End Select
        chgBypgm = False
    End Sub


    Private Sub txtaccount_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtaccount.Validated
        If txtname.Text = "" Then GoTo invalid
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select accid,accdescr from accmast where accdescr='" & txtaccount.Text & "'")
        chgBypgm = True
        If dt.Rows.Count = 0 Then
invalid:
            txtaccount.Text = ""
            txtaccount.Tag = ""
        Else
            txtaccount.Text = dt(0)("accdescr")
            txtaccount.Tag = dt(0)("accid")
        End If
        chgBypgm = False
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        grdItem.DataSource = SearchGrid(_dtTable, Trim(txtSearch.Text), cmbSearch.SelectedIndex, Not chkSearch.Checked)
        _rptTable = SearchGrid(_dtTable, Trim(txtSearch.Text), cmbSearch.SelectedIndex, Not chkSearch.Checked)
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Dim RptType As String = ""
        RptType = "EMPL"
        fRptFormat = New RptFormatfrm
        fRptFormat.RptType = RptType
        fRptFormat.ShowDialog()
        fRptFormat = Nothing
    End Sub
    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        PrepareReport(RptFlName, "", forPrint)
    End Sub
    Public Sub PrepareReport(ByVal FileName As String, ByVal RptCaption As String, ByVal forpint As Boolean)
        If Not frm Is Nothing Then
            frm.Close()
            frm = New ReprtviewNEWfrm
        Else
            frm = New ReprtviewNEWfrm
        End If
        Dim ds As New DataSet
        If _rptTable Is Nothing Then
            Dim dt As DataTable
            dt = loadEmployeeMaster()
            ds.Tables.Add(dt)
        Else
            ds.Tables.Add(_rptTable)
        End If
        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = fMainForm
        frm.Text = "Employee Report"
        frm.Show()
    End Sub

    Private Sub fSelect_doSelect(ByVal KeyId As String, ByVal strFld1 As String, ByVal strFld2 As String) Handles fSelect.doSelect
        chgBypgm = True
        txtaccount.Text = strFld1
        txtaccount.Tag = KeyId
        chgBypgm = False
    End Sub

    Private Sub btnadddepartment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadddepartment.Click
        Dim frm As New DepartmentCreation
        frm.isClose = True
        frm.ShowDialog()
        ldDepartments()
    End Sub

    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        If MsgBox("Do you want to remove selected employee?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        _objcmnbLayer._saveDatawithOutParm("Delete from EmpMasterTb where empid=" & Val(txtCode.Tag))
        MsgBox("Employee removed", MsgBoxStyle.Information)
        AddNew()
        TabControl1.SelectedIndex = 1
        loadEmployeeMaster()
    End Sub

    Private Sub txtvisa_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtvisa.KeyDown, dtpvisaexpiry.KeyDown, _
    txtidno.KeyDown, dtpidexpiry.KeyDown, txtlicense.KeyDown, dtplicenseexpiry.KeyDown, txtpassport.KeyDown, dtppassportexpiry.KeyDown
        Dim myctrl As Control
        myctrl = sender
        If e.KeyCode = Keys.Return Then
            If myctrl.Name = "dtppassportexpiry" Then
                BtnUpdate.Focus()
            Else
                SendKeys.Send("{TAB}")
            End If

        End If
    End Sub

    Private Sub txtvisa_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtvisa.TextChanged

    End Sub
End Class