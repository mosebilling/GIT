Imports System.Net
Imports System.IO

Public Class StudentAdmissionFrm

#Region "Class Objects"
    Private _objJob As New clsJob
    Private _objcmnbLayer As New clsCommon_BL
    Private _objInv As New clsInvoice
    Private _objTr As New clsAccountTransaction
#End Region
    Private chgbyprg As Boolean
    Private chgamtbyprg As Boolean
    Private dttable As DataTable
    Private rpttable As DataTable
    Private RptType As String
    Private rdCount As Integer
    Private dt1 As Date
    Private dt2 As Date
    Private accid As Long
    Private S1Accid As Long
    Private _objcommonlayer As New clsCommon_BL
    Private WithEvents fRptFormat As RptFormatfrm
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fwait As WaitMessageFrm
    Private WithEvents fsales As SalesInvoice
#Region "constantvariables"
    Private Const constfeesname = 0
    Private Const constfeesamount = 1
    Private Const constRVAmnt = 2
    Private Const constBalance = 3
    Private Const constFeesRef = 4
    Private Const constfeeslastpaid = 5
    Private Const consttype = 6
    Private Const constfeesid = 7 'fees master id
    Private Const constfeesTrid = 8 'studentfeestb id
#End Region
#Region "Pirvate Variables"
    Private MyActiveControl As New Object
    Private lstKey As Integer
    Private activecontrolname As String
    Private jobid As Long

#End Region
    Delegate Sub GenericDelegate(ByVal Mname As String, ByVal RecName As String, ByVal rec As Integer, ByVal count As Integer)
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub AddNew()
        txtcode.Text = GenerateNext(txtcode.Text)
        chgbyprg = True
        chgamtbyprg = True
        For Each Control In GroupBox1.Controls
            If TypeOf (Control) Is TextBox Then
                If Control.name <> "txtcode" Then
                    Control.text = ""
                    Control.tag = ""
                End If
            End If
        Next
        For Each Control In Panel5.Controls
            If TypeOf (Control) Is TextBox Then
                Control.text = ""
                Control.tag = ""
            End If
        Next
        For Each Control In GroupBox2.Controls
            If TypeOf (Control) Is TextBox Then
                Control.text = ""
                Control.tag = ""
            End If
        Next
        For Each Control In GroupBox4.Controls
            If TypeOf (Control) Is TextBox Then
                Control.text = ""
                Control.tag = ""
            End If
        Next
        cldrdate.Value = DateValue(Date.Now)
        dtpdob.Value = DateValue(Date.Now)
        rdomale.Checked = True
        rdoactive.Checked = True
        cmbclassteacher.Text = ""
        cmbclassteacher.Tag = 0
        lblteacherphone.Text = "Phone : "
        lblsidate.Text = ""
        lblsino.Text = ""
        lblsino.Tag = ""
        lblrvdate.Text = ""
        lblrvno.Text = ""
        lblrvno.Tag = ""
        picstudent.Image = Nothing
        jobid = 0
        lbloutstanding.Text = "0.00"
        picstudent.BackgroundImage = Nothing
        lblpicpath.Text = ""
        btnnew.Text = "Clear"
        txtadmissionfees.Enabled = True
        txtadmissionfees.Tag = 0
        btnrv.Visible = False
        chgbyprg = False
        chgamtbyprg = False
        btnsales.Enabled = False
        accid = 0
        txtcode.Tag = ""
        txtopening.Text = Format(0, numFormat)
        'loadReceipt()
        grdreceipt.DataSource = Nothing
        grdinvList.DataSource = Nothing
        txtName.Focus()
        lbltotal.Text = Format(0, numFormat)
        lbldue.Text = Format(0, numFormat)
        lbloutstanding.Text = Format(0, numFormat)
        loadFees()
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
            dt = _objcmnbLayer._fldDatatable("SELECT top 1 jobcode from JobTb order by Jobid desc")
            If dt.Rows.Count > 0 Then
                strCode = dt(0)(0)
            End If
        End If
        If strCode = "" Then
            strCode = "ADM"
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

                tmp = Mid(strCode, 1, IIf(Len(strCode) <> 0, Len(strCode) - f, 0)) & Format(N, StrDup(i, "0"))
                dr = _objcmnbLayer._fldDatatable("SELECT jobcode from JobTb WHERE jobcode = '" & tmp & "'")
                If dr.Rows.Count = 0 Then
                    NewCode = Mid(strCode, 1, IIf(Len(strCode) <> 0, Len(strCode) - f, 0)) & Format(N, StrDup(i, "0"))
                    NewCode = Mid(NewCode, 1, 30)
                    Exit Do
                End If
                N = N + 1
            Loop
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return NewCode
    End Function
    Private Sub GenerateNextAccountno(ByRef Accountno As Long)
        Dim newVal As Long
        Dim _vdatatableAcc As DataTable
        _vdatatableAcc = _objcmnbLayer._fldDatatable("declare @s1accid bigint select @s1accid=s1accid from S1AccHd where GrpSetOn='customer' " & _
                                                     "SELECT MAX(AccountNo)AccountNo,@s1accid Groupid FROM AccMast WHERE S1AccId =isnull(@s1accid,0)")
        If _vdatatableAcc.Rows.Count > 0 Then
            Accountno = Val(_vdatatableAcc(0)("AccountNo") & "")
            newVal = _vdatatableAcc(0)("Groupid")
        End If
        If Val(Accountno) = 0 Then
            Accountno = Val(newVal & "0000")
        End If
        If Val(Accountno) >= Val(newVal & "9999") Then MsgBox("Maximum number of Ledgers reached in this Group.", MsgBoxStyle.Critical) : Exit Sub
        Accountno = Val(Accountno) + 1
        S1Accid = newVal
    End Sub

    Private Sub StudentAdmissionFrm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtcode.Focus()
    End Sub

    Private Sub StudentAdmissionFrm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtcode.Text = GenerateNext(txtcode.Text)
        SetGridHead()
        loadTeachers()
        loadFees()
        Timer1.Enabled = True
    End Sub

    Private Sub txtadmissionfees_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtadmissionfees.KeyDown
        If e.KeyCode = Keys.Return Then
            txtopening.Focus()
        End If
    End Sub

    Private Sub txtcode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtcode.KeyDown, cldrdate.KeyDown, txtName.KeyDown, txtAddr0.KeyDown, _
    txtAddr1.KeyDown, txtAddr2.KeyDown, txtfathername.KeyDown, txtphone.KeyDown, txtFOccupation.KeyDown, txtmother.KeyDown, txtmotherphone.KeyDown, txtMoccupation.KeyDown, txtemail.KeyDown, _
    txtstandered.KeyDown, txtsection.KeyDown, txtenrollnumber.KeyDown, txtbgroup.KeyDown, txtreligion.KeyDown, txtcast.KeyDown, cmbclassteacher.KeyDown, dtpdob.KeyDown
        Dim myctrl As Object
        myctrl = sender
        If e.KeyCode = Keys.Return Then
            If myctrl.name = "txtenrollnumber" Then
                txtbgroup.Focus()
            Else
                SendKeys.Send("{TAB}")
            End If

        End If
    End Sub

    Private Sub txtadmissionfees_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtadmissionfees.KeyPress
        NumericTextOnKeypress(txtadmissionfees, e, chgbyprg, numFormat)
    End Sub
    Private Sub SetGridHead()
        chgbyprg = True
        With grdfees
            SetEntryGridProperty(grdfees)
            .ColumnCount = 9
            .ColumnHeadersHeight = 100
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersDefaultCellStyle.BackColor = Color.Olive
            .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.5!, System.Drawing.FontStyle.Regular)
            .RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.0!)

            .Columns(constfeesname).HeaderText = "Fees"
            .Columns(constfeesname).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(constfeesname).ReadOnly = True
            .Columns(constfeesname).Width = 150

            .Columns(constFeesRef).HeaderText = "Reference"
            .Columns(constFeesRef).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(constFeesRef).ReadOnly = True
            .Columns(constfeesname).Width = 100


            .Columns(constRVAmnt).HeaderText = "Received"
            .Columns(constRVAmnt).Width = 80
            .Columns(constRVAmnt).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(constRVAmnt).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(constRVAmnt).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(constRVAmnt).DefaultCellStyle.BackColor = Color.LightGreen

            .Columns(constBalance).HeaderText = "Balance"
            .Columns(constBalance).Width = 80
            .Columns(constBalance).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(constBalance).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(constBalance).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(constBalance).DefaultCellStyle.BackColor = Color.LightPink


            .Columns(constfeesamount).HeaderText = "Amount"
            .Columns(constfeesamount).Width = 80
            .Columns(constfeesamount).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(constfeesamount).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(constfeesamount).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(constfeesamount).DefaultCellStyle.BackColor = Color.LightYellow

           
            .Columns(constfeeslastpaid).HeaderText = "Paid On"
            .Columns(constfeeslastpaid).Width = 80
            .Columns(constfeeslastpaid).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(constfeeslastpaid).ReadOnly = True
            .Columns(constfeeslastpaid).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns(consttype).HeaderText = "Yearly"
            .Columns(consttype).Width = 60
            .Columns(consttype).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(consttype).ReadOnly = True

            .Columns(constfeesid).Visible = False
            .Columns(constfeesTrid).Visible = False
        End With
        chgbyprg = False
    End Sub
    Private Sub grdBeginEdit()
        chgbyprg = True
        grdfees.BeginEdit(True)
        chgbyprg = False
    End Sub

    Private Sub grdfees_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdfees.CellClick
        If grdfees.RowCount = 0 Then Exit Sub
        With grdfees
            If e.ColumnIndex <> constfeesamount Or .Item(consttype, e.RowIndex).Value = "YES" Then
                .CurrentCell.ReadOnly = True
            Else
                .CurrentCell.ReadOnly = False
                grdBeginEdit()
            End If
        End With

    End Sub

    Private Sub grdfees_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdfees.CellEndEdit
        Dim col As Integer = e.ColumnIndex
        If col = constfeesamount Then
            With grdfees
                If Val(.Item(col, e.RowIndex).Value) = 0 Then .Item(col, e.RowIndex).Value = 0
                .Item(col, e.RowIndex).Value = Format(Convert.ToDecimal(.Item(col, e.RowIndex).Value), "#,##0" & IIf(NoOfDecimal = 0, "", "." & Strings.StrDup(NoOfDecimal, "0")))
            End With
        End If
    End Sub

    Private Sub grdfees_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdfees.EditingControlShowing
        Dim Col As Integer
        Col = grdfees.CurrentCell.ColumnIndex
        If Col = ConstQty Then
            Dim tb As TextBox = CType(e.Control, TextBox)
            RemoveHandler tb.TextChanged, AddressOf Textbox_TextChanged
            AddHandler tb.TextChanged, AddressOf Textbox_TextChanged
        End If
        If Col = ConstQty Then
            Dim tb As TextBox = CType(e.Control, TextBox)
            RemoveHandler tb.KeyPress, AddressOf Textbox_KeyPress
            AddHandler tb.KeyPress, AddressOf Textbox_KeyPress
        End If
    End Sub
    Private Sub Textbox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            Dim col As Integer
            col = grdfees.CurrentCell.ColumnIndex
            If col = constfeesamount Then
                If Not IsNumeric(e.KeyChar) And e.KeyChar <> "." And Not e.KeyChar = Convert.ToChar(Keys.Back) Then e.Handled = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub Textbox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub grdfees_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdfees.GotFocus
        activecontrolname = "grdfees"
    End Sub

    Private Sub grdfees_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdfees.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                With grdfees
                    If .RowCount = 0 Then Exit Sub
                    If .CurrentRow.Index = .RowCount - 1 Then btnupdate.Focus() : Exit Sub

                    FindNextCell(grdfees, .CurrentCell.RowIndex, .CurrentCell.ColumnIndex + 1)
                    grdBeginEdit()

                End With
            ElseIf e.KeyCode = Keys.Up Then
                With grdfees
                    If .CurrentCell.RowIndex > 0 Then
                        .CurrentCell = .Item(constfeesamount, .CurrentCell.RowIndex - 1)
                        chgbyprg = True
                        .BeginEdit(True)
                        chgbyprg = False
                    End If
                End With

            ElseIf e.KeyCode = Keys.Down Then
                With grdfees
                    If .CurrentCell.RowIndex < .RowCount - 1 Then
                        .CurrentCell = .Item(constfeesamount, .CurrentCell.RowIndex + 1)
                        chgbyprg = True
                        .BeginEdit(True)
                        chgbyprg = False
                    End If
                End With
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub grdfees_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdfees.Leave
        activecontrolname = ""
    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            Const WM_KEYDOWN As Integer = &H100
            Const WM_KEYUP As Integer = &H101
            If msg.Msg = WM_KEYDOWN Or msg.Msg = WM_KEYUP Then
                If activecontrolname = "grdfees" Then
                    If msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up) Or msg.WParam.ToInt32() = CInt(Keys.Enter) Or msg.WParam.ToInt32() = CInt(Keys.F2) Or msg.WParam.ToInt32() = CInt(Keys.F3) Or msg.WParam.ToInt32() = CInt(Keys.F4) Then
                        grdfees_KeyDown(Nothing, New KeyEventArgs(keyData))
                        Return True
                    End If
                End If
            End If
ctn:
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'Timer1.Enabled = False
        'resizeGridColumn(grdfees, constfeesname)
        'If grdfees.Columns(constfeesname).Width < 30 Then
        '    grdfees.Columns(constfeesname).Width = 150
        'End If
        '
    End Sub
    Private Sub loadTeachers()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("select SManName from SalesmanTb where isnull(isschoolteacher,0)=1")
        Dim i As Integer
        cmbclassteacher.Items.Clear()
        cmbclassteacher.Items.Add("")
        For i = 0 To dt.Rows.Count - 1
            cmbclassteacher.Items.Add(dt(i)("SManName"))
        Next
    End Sub
    Private Sub loadFees()
        Dim dt As DataTable
        Dim str As String
        str = "Select feesname,feesid,case when isnull(isyearly,0)=0 then 'NO' else 'YES' end Yearly,isnull(amount,0)amount  from SchoolFeesTb where isnull(feesIsactive,0)=0"
        dt = _objcmnbLayer._fldDatatable(str)
        Dim i As Integer
        With grdfees
            If .RowCount > 0 Then .Rows.Clear()
            For i = 0 To dt.Rows.Count - 1
                .Rows.Add()
                .Item(constfeesname, i).Value = dt(i)("feesname")
                .Item(constfeesamount, i).Value = Format(dt(i)("amount"), numFormat)
                .Item(constfeesTrid, i).Value = 0
                .Item(constfeeslastpaid, i).Value = ""
                .Item(constfeesid, i).Value = dt(i)("feesid")
                .Item(consttype, i).Value = dt(i)("Yearly")
                'If Val(txtcode.Tag) > 0 Then
                '    .Item(constfeesTrid, i).Value = dt(i)("feesid")
                '    .Item(constfeesamount, i).Value = dt(i)("amount")
                '    .Item(constfeeslastpaid, i).Value = dt(i)("lastpaid")
                'End If
            Next
        End With
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        verify()
    End Sub
    Private Sub verify()
        If txtcode.Text = "" Then
            MsgBox("Invalid Admission NO", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If txtName.Text = "" Then
            MsgBox("Invalid Student Name", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
       
        If Val(txtadmissionfees.Text) = 0 And Val(lblsino.Tag) > 0 Then
            If MsgBox("Admission Fees found! Do you want to remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        End If
        If MsgBox("Verification is succeeded. Do you like to file it ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = vbYes Then
            'saveAdmission()
            loadWaite(2)
        End If
    End Sub
    Private Sub saveAdmission()
        With _objJob
            .studentid = Val(txtcode.Tag)
            .Jobid = jobid
            .studentname = txtName.Text
            .admissionno = txtcode.Text
            .admissiondate = DateValue(cldrdate.Value)
            .standered = txtstandered.Text
            .stadd1 = txtAddr0.Text
            .stadd2 = txtAddr1.Text
            .stadd3 = txtAddr2.Text
            .section = txtsection.Text
            .rollnumber = txtenrollnumber.Text
            If rdomale.Checked Then
                .gender = 0
            Else
                .gender = 1
            End If
            .dateofbirth = DateValue(dtpdob.Value)
            .stphone = txtphone.Text
            .studentemail = txtemail.Text
            .religion = txtreligion.Text
            .studentcast = txtcast.Text
            .bloodgroup = txtbgroup.Text
            .fathername = txtfathername.Text
            .mothername = txtmother.Text
            .fatheroccupation = txtFOccupation.Text
            .motheroccupation = txtMoccupation.Text
            .motherphonenumber = txtmotherphone.Text
            If Val(txtopening.Text) = 0 Then txtopening.Text = Format(0, numFormat)
            .OpnBal = CDbl(txtopening.Text)
            If rdoactive.Checked Then
                .studentstatus = 0
            ElseIf rdosuspend.Checked Then
                .studentstatus = 1
            Else
                .studentstatus = 2
            End If
            .classteacherid = Val(cmbclassteacher.Tag)
            If Val(txtadmissionfees.Text) = 0 Then
                txtadmissionfees.Text = 0
            End If
            .admissionfees = CDbl(txtadmissionfees.Text)
            Dim AccountNo As Long
            Dim dt As DataTable
            dt = _objcmnbLayer._fldDatatable("select accountno from accmast where accid=" & accid)
            If dt.Rows.Count > 0 Then
                AccountNo = Val(dt(0)("accountno") & "")
            Else
                AccountNo = 0
            End If
            If AccountNo = 0 Then
                GenerateNextAccountno(AccountNo)
            End If
            .AccountNo = AccountNo
            'Dim s1accno As String = AccountNo
            's1accno = Mid(s1accno, 1, 4)
            .S1Accid = S1Accid
            txtcode.Tag = .saveSchoolStudentAdmissionTb()
        End With
        Dim i As Integer
        Dim qry As String = ""
        With grdfees
            For i = 0 To .RowCount - 1
                If Val(.Item(constfeesamount, i).Value) > 0 And .Item(consttype, i).Value = "NO" Then
                    If qry <> "" Then
                        qry = qry & vbCrLf
                    End If
                    If Val(.Item(constfeesTrid, i).Value) > 0 Then
                        qry = qry & "UPDATE SchoolStudentFeesTb set setremove=0,studentadmissionid=" & Val(txtcode.Tag) & ",feesmasterid=" & Val(.Item(constfeesid, i).Value) & ",Amount=" & CDbl(.Item(constfeesamount, i).Value) & _
                                "where schoolfeesid=" & Val(.Item(constfeesTrid, i).Value)
                    Else
                        qry = qry & "Insert into SchoolStudentFeesTb(studentadmissionid,feesmasterid,Amount) values(" & Val(txtcode.Tag) & "," & Val(.Item(constfeesid, i).Value) & "," & CDbl(.Item(constfeesamount, i).Value) & ")"
                    End If
                End If
            Next
        End With
        saveImage(_objJob.studentid)


        qry = qry & "delete from SchoolStudentFeesTb where isnull(setremove,0)=1 and studentadmissionid=" & Val(txtcode.Tag)
        _objcmnbLayer._saveDatawithOutParm(qry)
        If Val(lblrvno.Tag & "") = 0 And Val(txtadmissionfees.Tag) = 1 Then
            If Val(txtadmissionfees.Text) > 0 Then
                createADMFeesSI()
            Else
                If Val(lblsino.Tag) > 0 Then
                    qry = "Delete from acctrcmn where linkno=" & Val(lblsino.Tag)
                    qry = "Delete from acctrdet where linkno=" & Val(lblsino.Tag)
                    _objcmnbLayer._saveDatawithOutParm(qry)
                End If
            End If
        End If
        MsgBox("Student Admission saved successfully", MsgBoxStyle.Information)
        'AddNew()
        loadrec()
    End Sub
    Private Function profileQry(ByVal isreport As Boolean) As String
        Dim str As String
        Dim month As String
        month = Format(Date.Now, "MMMM/yyyy")


        month = Mid(month, 1, 3) & Year(DateValue(Date.Now))
        str = " declare @JobCode varchar(50) select @JobCode=admissionno from SchoolStudentAdmissionTb where studentid=" & Val(txtcode.Tag) & _
        " select SchoolStudentAdmissionTb.*,isnull(SManName,'')Classteacher,SalesmanTb.phone teacherphone, isnull(accmast.AccountNo,0) AccountNo,isnull(outstanding,0)outstanding,1 lnk from SchoolStudentAdmissionTb " & _
        "left join SalesmanTb on SalesmanTb.salesmanid=SchoolStudentAdmissionTb.classteacherid " & _
        "left join accmast on accmast.accid=SchoolStudentAdmissionTb.customerid " & _
        "left join (select sum(dealamt)outstanding,AccountNo from AccTrDet group by AccountNo)baltr on SchoolStudentAdmissionTb.customerid=baltr.AccountNo " & _
        "where studentid=" & Val(txtcode.Tag)

        str = str & " select feesname,Amount,monthref,convert(varchar,jvdate,103) lastpaid,feesid,schoolfeesid,studentadmissionid,isnull(dealamt,0)RVAmt,Amount-isnull(dealamt,0) Balance,Yearly from(  select feesname," & _
        "case when isnull(isyearly,0)=0 then isnull(SchoolStudentFeesTb.Amount,0) else isnull(SchoolFeesTb.Amount,0) end Amount,feesid,isnull(schoolfeesid,0)schoolfeesid,admissionno+'/'+feescode+'/'+'" & month & "' monthref,customerid,studentadmissionid, " & _
        "case when isnull(isyearly,0)=0 then 'NO' else 'YES' end Yearly from SchoolFeesTb left join " & _
        "(select Amount,feesmasterid,schoolfeesid,studentadmissionid from SchoolStudentFeesTb where studentadmissionid=" & Val(txtcode.Tag) & ")SchoolStudentFeesTb on SchoolFeesTb.feesid=SchoolStudentFeesTb.feesmasterid " & _
        "left join SchoolStudentAdmissionTb on SchoolStudentAdmissionTb.studentid=SchoolStudentFeesTb.studentadmissionid)tr " & _
        "LEFT JOIN (select reference,sum(dealamt*-1)dealamt,AccountNo,max(jvdate) jvdate from AccTrDet  " & _
        "inner join AccTrCmn on AccTrCmn.LinkNo=AccTrDet.LinkNo where DealAmt<0 and JVType='RV' group by reference,AccountNo)acctr on tr.monthref=acctr.Reference and tr.customerid=acctr.AccountNo "
        If isreport = False Then
            str = str & " select PreFix,JVNum,AccTrCmn.LinkNo,JVDate from AccTrCmn  " & _
              "inner join AccTrDet on AccTrCmn.LinkNo=AccTrDet.LinkNo where JVTYPE='SI' and JobCode=@JobCode " & _
              " and Reference='ADM/'+@JobCode" & _
              " and accountno=(select customerid from SchoolStudentAdmissionTb where studentid=" & Val(txtcode.Tag) & ")"

            str = str & " select PreFix,JVNum,AccTrCmn.LinkNo,JVDate from AccTrCmn  " & _
                  "inner join AccTrDet on AccTrCmn.LinkNo=AccTrDet.LinkNo where JVTYPE='RV' and JobCode=@JobCode " & _
                  " and Reference='ADM/'+@JobCode" & _
                  " and accountno=(select customerid from SchoolStudentAdmissionTb where studentid=" & Val(txtcode.Tag) & ")"

        End If



        str = str & " "
        Return str
    End Function
    Private Sub loadrec()
        Dim dt As DataTable
        Dim outstanding As Double
        Dim opening As Double
        Dim str As String
        str = profileQry(False)
        Dim ds As DataSet
        ds = _objcmnbLayer._ldDataset(str, False)
        dt = ds.Tables(0)
        chgbyprg = True
        chgamtbyprg = True
        If dt.Rows.Count > 0 Then
            txtcode.Tag = dt(0)("studentid")
            jobid = dt(0)("jobid")
            accid = dt(0)("customerid")
            txtName.Text = dt(0)("studentname")
            txtcode.Text = dt(0)("admissionno")
            cldrdate.Value = DateValue(dt(0)("admissiondate"))
            txtstandered.Text = dt(0)("standered")
            txtAddr0.Text = dt(0)("stadd1")
            txtAddr1.Text = dt(0)("stadd2")
            txtAddr2.Text = dt(0)("stadd3")
            txtsection.Text = dt(0)("section")
            txtenrollnumber.Text = dt(0)("rollnumber")
            opening = Val(dt(0)("openingAmt") & "")
            txtopening.Text = Format(opening, numFormat)
            outstanding = dt(0)("outstanding")
            If Val(dt(0)("gender")) = 0 Then
                rdomale.Checked = True
            Else
                rdofemale.Checked = True
            End If
            dtpdob.Value = DateValue(dt(0)("dateofbirth"))
            txtphone.Text = dt(0)("stphone")
            txtemail.Text = dt(0)("studentemail")
            txtreligion.Text = dt(0)("religion")
            txtcast.Text = dt(0)("studentcast")
            txtbgroup.Text = dt(0)("bloodgroup")
            txtfathername.Text = dt(0)("fathername")
            txtmother.Text = dt(0)("mothername")
            txtFOccupation.Text = dt(0)("fatheroccupation")
            txtMoccupation.Text = dt(0)("motheroccupation")
            txtmotherphone.Text = dt(0)("motherphonenumber")
            If Val(dt(0)("studentstatus")) = 0 Then
                rdoactive.Checked = True
            ElseIf Val(dt(0)("studentstatus")) = 1 Then
                rdosuspend.Checked = True
            Else
                rdoMtran.Checked = True
            End If
            cmbclassteacher.Text = Trim(dt(0)("Classteacher") & "")
            cmbclassteacher.Tag = Trim(dt(0)("classteacherid") & "")
            lblteacherphone.Text = Trim(dt(0)("teacherphone") & "")
            txtadmissionfees.Text = Format(CDbl(dt(0)("admissionfees")), numFormat)
            txtcode.Focus()
            dt = Nothing
            dt = ds.Tables(1)
            Dim i As Integer
            Dim amount As Double
            Dim dueamount As Double
            With grdfees
                If .RowCount > 0 Then .Rows.Clear()
                For i = 0 To dt.Rows.Count - 1
                    .Rows.Add()
                    .Item(constfeesname, i).Value = dt(i)("feesname")
                    .Item(constfeesamount, i).Value = Format(CDbl(dt(i)("amount")), numFormat)
                    amount = amount + CDbl(dt(i)("amount"))
                    .Item(constFeesRef, i).Value = dt(i)("monthref")
                    .Item(constfeesid, i).Value = dt(i)("feesid")
                    .Item(constfeesTrid, i).Value = dt(i)("schoolfeesid")
                    .Item(consttype, i).Value = dt(i)("Yearly")
                    If Not IsDBNull(dt(i)("lastpaid")) Then
                        grdfees.Item(constfeeslastpaid, i).Value = Trim(dt(i)("lastpaid"))
                    Else
                        .Item(constfeeslastpaid, i).Value = ""

                    End If
                    .Item(constRVAmnt, i).Value = dt(i)("RVAmt")
                    .Item(constBalance, i).Value = dt(i)("Balance")
                    dueamount = dueamount + CDbl(dt(i)("Balance"))
                Next
            End With
            lbltotal.Text = Format(amount, numFormat)
            lbldue.Text = Format(dueamount, numFormat)
            lbloutstanding.Text = Format(outstanding + opening, numFormat)
            dt = Nothing
            dt = ds.Tables(2)
            If dt.Rows.Count > 0 Then
                Dim invno As String
                invno = dt(0)("PreFix")
                If invno <> "" Then invno = invno & "/"
                invno = invno & dt(0)("JVNum")
                lblsidate.Text = dt(0)("JVDate")
                lblsino.Text = invno
                lblsino.Tag = dt(0)("linkno")
            Else
                lblsino.Tag = 0
                lblsidate.Text = ""
                lblsino.Text = ""
            End If
            dt = Nothing
            dt = ds.Tables(3)
            If dt.Rows.Count > 0 Then
                Dim invno As String
                invno = dt(0)("PreFix")
                If invno <> "" Then invno = invno & "/"
                invno = invno & dt(0)("JVNum")
                lblrvdate.Text = dt(0)("JVDate")
                lblrvno.Text = invno
                lblrvno.Tag = dt(0)("linkno")
            Else
                lblrvno.Tag = 0
                lblrvdate.Text = ""
                lblrvno.Text = ""
            End If
            loadReceipt()
            loadInvs()
            loadimage()
            If Val(lblrvno.Text) > 0 Then
                txtadmissionfees.Enabled = False
            Else
                txtadmissionfees.Enabled = True
            End If
            TabControl2.SelectedIndex = 0
            txtcode.Focus()
            GroupBox4.Enabled = True
            btnDelete.Enabled = True
            btnsales.Enabled = True
            'grdfees.Item(constfeeslastpaid, 1).Value = ""
            'btnrv.Visible = True
        End If
        btnnew.Text = "New"
        chgamtbyprg = False
        chgbyprg = False
    End Sub

    Private Sub grdList_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdList.DoubleClick
        If grdList.RowCount = 0 Then Exit Sub
        txtcode.Tag = Val(grdList.Item("studentid", grdList.CurrentRow.Index).Value)
        'loadrec()
        loadWaite(3)
    End Sub

    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        loadrec()
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        AddNew()
    End Sub
    Private Function qry() As String
        Dim str As String
        Dim condition As String
        condition = "WHERE isnull(studentstatus,0)=" & cmbstatus.SelectedIndex
        If chkdate.Checked Then
            condition = condition & " and admissiondate>='" & Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd") & "' and admissiondate<='" & Format(DateValue(cldrEnddate.Value), "yyyy/MM/dd") & "'"
        End If
        If cmbclass.Text <> "" Then
            condition = condition & " and standered='" & cmbclass.Text & "'"
        End If
        If cmbgender.SelectedIndex > 0 Then
            condition = condition & " and gender=" & cmbgender.SelectedIndex - 1
        End If
        If chkoutstanding.Checked Then
            condition = condition & " and isnull(bal,0)>0"
        End If
        str = "select admissionno,rollnumber,admissiondate,studentname,isnull(bal,0)+isnull(openingAmt,0)bal,fathername,mothername,stphone,bloodgroup,standered,section," & _
        "isnull(SManName,'')Teacher,SalesmanTb.phone teacherphone,case when isnull(gender,0)=0 then 'Male' else 'Female' end gender," & _
        "dateofbirth,religion,studentcast,studentid,1 lnk " & _
        " from SchoolStudentAdmissionTb " & _
        "left join (select sum(dealamt)bal,accountno from acctrdet group by accountno)baltr on SchoolStudentAdmissionTb.customerid=baltr.accountno " & _
        "left join SalesmanTb on SalesmanTb.salesmanid=SchoolStudentAdmissionTb.classteacherid " & condition
        Return str
    End Function

    Private Sub loadAdmissiondetails()
        Dim str As String = qry()
        dttable = _objcmnbLayer._fldDatatable(str)
        grdList.DataSource = dttable
        With grdList
            SetGridProperty(grdList)

            .Columns("admissionno").HeaderText = "AdmissionNo"
            .Columns("admissionno").Width = 150

            .Columns("rollnumber").HeaderText = "Rollnumber"
            .Columns("rollnumber").Width = 150

            .Columns("admissiondate").HeaderText = "Adm.Date"
            .Columns("admissiondate").Width = 100

            .Columns("studentname").HeaderText = "Student Name"
            .Columns("studentname").Width = 150

            .Columns("bal").HeaderText = "Outstanding"
            .Columns("bal").Width = 100
            .Columns("bal").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("bal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("bal").DefaultCellStyle.BackColor = Color.LightPink

            .Columns("fathername").HeaderText = "Father Name"
            .Columns("fathername").Width = 150

            .Columns("stphone").HeaderText = "Phone"
            .Columns("stphone").Width = 150

            .Columns("mothername").HeaderText = "Mother Name"
            .Columns("mothername").Width = 150

            .Columns("bloodgroup").HeaderText = "B.Group"
            .Columns("bloodgroup").Width = 75

            .Columns("standered").HeaderText = "Standered"
            .Columns("standered").Width = 150

            .Columns("section").HeaderText = "Section"
            .Columns("section").Width = 100

            .Columns("Teacher").HeaderText = "Teacher"
            .Columns("Teacher").Width = 150

            .Columns("teacherphone").HeaderText = "Teacher Phone"
            .Columns("teacherphone").Width = 150

            .Columns("gender").HeaderText = "Gender"
            .Columns("gender").Width = 100

            .Columns("dateofbirth").HeaderText = "DOB"
            .Columns("dateofbirth").Width = 100

            .Columns("religion").HeaderText = "Religion"
            .Columns("religion").Width = 100

            .Columns("studentcast").HeaderText = "Cast"
            .Columns("studentcast").Width = 100

           

            .Columns("studentid").Visible = False
            .Columns("lnk").Visible = False
        End With
        Dim i As Integer = 0
        For i = 0 To grdList.ColumnCount - 2
            cmbOrder.Items.Add(grdList.Columns(i).HeaderText)
        Next
        If cmbOrder.Items.Count > 0 Then cmbOrder.SelectedIndex = 3
    End Sub

    Private Sub TabControl2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl2.Click
        If TabControl2.SelectedIndex = 1 Then
            loadFilters()
            cmbclass.SelectedIndex = 0
            cmbgender.SelectedIndex = 0
            cmbstatus.SelectedIndex = 0
            loadWaite(1)
            'loadAdmissiondetails()
        End If
    End Sub

    Private Sub btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnload.Click
        loadAdmissiondetails()
    End Sub

    Private Sub txtSeq_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeq.TextChanged

        grdList.DataSource = SearchGrid(dttable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
        rpttable = grdList.DataSource


    End Sub
    Private Sub createADMFeesSI()
        Dim QRY As String
        If UsrBr = "" Then
            QRY = " Select accid from accmast where AccSetId like '%" & Format(4, "00") & "%'"
            QRY = QRY & " select Prefix,InvNo from InvNos where InvType='SI'"
        Else
            QRY = " SELECT accid FROM BranchAccSet WHERE branchcode='" & UsrBr & "' and setno=" & 4
            QRY = QRY & " select Prefix,InvNo from InvNosBrTb where InvType='SI' AND Brcode='" & UsrBr & "'"
        End If
        QRY = QRY & " select customerid from SchoolStudentAdmissionTb where studentid=" & Val(txtcode.Tag)
        Dim ds As DataSet
        ds = _objcmnbLayer._ldDataset(QRY, False)
        Dim prefix As String = ""
        Dim invno As Integer
        Dim incomeaccid As Long
        Dim customerid As Long
        If ds.Tables(0).Rows.Count > 0 Then
            incomeaccid = ds.Tables(0)(0)("accid")
        End If
        If ds.Tables(1).Rows.Count > 0 Then
            prefix = ds.Tables(1)(0)("Prefix")
            invno = ds.Tables(1)(0)("InvNo")
        End If
        If ds.Tables(2).Rows.Count > 0 Then
            customerid = ds.Tables(2)(0)("customerid")
        End If

        dtAccTb.Rows.Clear()

        _objTr.JVType = "SI"
        _objTr.JVDate = DateValue(cldrdate.Value)
        _objTr.PreFix = prefix
        _objTr.JVNum = invno
        _objTr.JVTypeNo = getVouchernumber("SI")
        _objTr.UserId = CurrentUser
        _objTr.MchName = MACHINENAME
        _objTr.CrtDtTm = DateValue(Date.Now)
        _objTr.TypeNo = 0 ' id number from prefixtb
        _objTr.VrDescr = "ADMISSION FEES/" & txtcode.Text & "/" & txtName.Text
        _objTr.IsModi = IIf(Val(lblsino.Tag) > 0, 2, 0)
        _objTr.LinkNo = Val(lblsino.Tag)
        _objTr.isLinkNo = True
        _objTr.isdeleteTr = 1

        setAcctrDetValue(True, customerid, "ADMISSION FEES BOOKING/" & txtName.Text & "/" & txtcode.Text, "ADM/" & txtcode.Text, CDbl(txtadmissionfees.Text))
        setAcctrDetValue(False, incomeaccid, "ADMISSION FEES BOOKING/" & txtName.Text & "/" & txtcode.Text, "ADM/" & txtcode.Text, CDbl(txtadmissionfees.Text))
        _objTr.SaveAccTrWithDt(dtAccTb)
    End Sub
    Private Sub setAcctrDetValue(ByVal isdb As Boolean, ByVal AccountNo As Long, ByVal EntryRef As String, ByVal Reference As String, ByVal amount As Double)
        Dim dtrow As DataRow
        dtrow = dtAccTb.NewRow
        dtrow("LinkNo") = 0
        dtrow("AccountNo") = AccountNo
        dtrow("Reference") = Reference
        dtrow("EntryRef") = EntryRef
        If isdb Then
            dtrow("DealAmt") = amount
        Else
            dtrow("DealAmt") = amount * -1
        End If
        dtrow("FCAmt") = dtrow("DealAmt")
        dtrow("CurrencyCode") = ""
        dtrow("CurrRate") = 1
        dtrow("TrInf") = 0
        dtrow("PDCAcc") = 0
        dtrow("ChqDate") = DateValue(Date.Now)
        dtrow("ChqNo") = ""
        dtrow("BankCode") = ""
        dtrow("UnqNo") = 0
        dtrow("JobCode") = txtcode.Text
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

    Private Sub cmbclassteacher_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbclassteacher.SelectedIndexChanged
        If chgbyprg Then Exit Sub
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("select salesmanid,phone from SalesmanTb where SManName='" & cmbclassteacher.Text & "'")
        If dt.Rows.Count > 0 Then
            cmbclassteacher.Tag = dt(0)("salesmanid")
            lblteacherphone.Text = dt(0)("phone")
        Else
            cmbclassteacher.Tag = ""
            lblteacherphone.Text = "Phone :"
        End If
    End Sub

    Private Sub txtadmissionfees_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtadmissionfees.TextChanged
        If chgamtbyprg Then Exit Sub
        txtadmissionfees.Tag = 1
    End Sub
    Private Sub loadFilters()
        Dim str As String
        str = " Select standered from SchoolStudentAdmissionTb group by standered"
        Dim dt As DataTable
        Dim ds As DataSet
        ds = _objcmnbLayer._ldDataset(str, False)
        dt = ds.Tables(0)
        Dim i As Integer
        cmbclass.Items.Clear()
        cmbclass.Items.Add("")
        For i = 0 To dt.Rows.Count - 1
            cmbclass.Items.Add(dt(i)("standered"))
        Next
        cmbclass.SelectedIndex = 0
    End Sub

    Private Sub chkdate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkdate.CheckedChanged

    End Sub

    Private Sub chkdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkdate.Click
        If chkdate.Checked Then
            pldate.Enabled = True
        Else
            pldate.Enabled = False
        End If
    End Sub

    Private Sub btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrv.Click
        If txtName.Text = "" Or accid = 0 Then
            MsgBox("Invalid Customer", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If Val(lblrvno.Tag) = 0 Then
            fMainForm.LoadRV(0, txtName.Text)
        Else
            fMainForm.LoadRV(Val(lblrvno.Tag), txtName.Text)
        End If

    End Sub

    Private Sub btnaddreceipt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddreceipt.Click
        'createFeesSI(DateValue(Date.Now))
        fMainForm.LoadRV(0, txtName.Text)
    End Sub

    Private Sub btneditreceipt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btneditreceipt.Click
        If grdreceipt.RowCount = 0 Then Exit Sub
        fMainForm.LoadRV(Val(grdreceipt.Item("linkno", grdreceipt.CurrentRow.Index).Value), txtName.Text)
    End Sub
    Private Sub loadReceipt()
        If txtcode.Text = "" Then Exit Sub
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT PreFix + convert(varchar,JVNum) [RV No],JVDate [RV Date],EntryRef,DealAmt*-1 Amount,AccTrCmn.LinkNo from AccTrCmn " & _
                                         "LEFT JOIN AccTrDet ON AccTrCmn.LinkNo=AccTrDet.Linkno " & _
                                         "WHERE jvtype='RV' AND (jobcode='" & txtcode.Text & "' or accountno=" & accid & ")")
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
    Private Sub loadInvs()
        If txtcode.Text = "" Then Exit Sub
        Dim strSql As String = ("Select  TrRefNo [Inv No] , TrDate [Tr.Date] ," & _
                               "isnull(InvAmt,0) [Amount]," & _
                               "TrDescription  [Tr. Description],UserId [Created By],TrId from " & _
                               "( select  prefix, invNo ,TrDate ,netamt InvAmt,Discount,AccDescr ,TrRefNo,TrDescription,Alias ,ItmInvCmnTb.UserId,[Job Code],ItmInvCmnTb.TrId from " & _
                               "ItmInvCmnTb LEFT JOIN (SELECT Trid,SUM((TrQty*(UnitCost+UnitOthCost))+taxamt)InvAmt FROM ItmInvTrTb GROUP BY Trid) Tr " & _
                               "ON  ItmInvCmnTb.Trid=Tr.Trid left join Accmast on ItmInvCmnTb.CSCode=Accmast.accid where ItmInvCmnTb.trtype='IS' and CSCode =" & accid & ") qq  order by TrDate ,InvNo")
        grdinvList.DataSource = Nothing
        _objcmnbLayer = New clsCommon_BL
        Dim source As DataTable = Me._objcmnbLayer._fldDatatable(strSql, False)
        grdinvList.DataSource = source
        Dim num3 As Integer = (source.Rows.Count - 1)
        Dim i As Integer = 0
        Dim num2 As Double
        Do While (i <= num3)
            num2 = num2 + source(i)("Amount")
            i += 1
        Loop
        lblinvamt.Text = Format(num2, numFormat)

        With grdinvList
            SetGridProperty(grdinvList)
            .Columns("Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Amount").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("trid").Visible = False
        End With
        resizeGridColumn(grdinvList, 3)
    End Sub

    Private Sub TabControl1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.Click
        If TabControl1.SelectedIndex = 2 Then
            resizeGridColumn(grdreceipt, 2)
        ElseIf TabControl1.SelectedIndex = 1 Then
            resizeGridColumn(grdfees, constfeesname)
            If grdfees.Columns(constfeesname).Width < 30 Then
                grdfees.Columns(constfeesname).Width = 150
            End If
        Else
            resizeGridColumn(grdinvList, 3)
        End If
    End Sub
    Private Sub createFeesSI(ByVal JVDate As Date)
        Dim QRY As String
        Dim month As String
        month = Format(JVDate, "MMMM/yyyy")
        month = Mid(month, 1, 3) & Year(DateValue(JVDate))
        QRY = "SELECT monthref,Reference,studentadmissionid,feesname,Amount,tr.AccountNo FROM (SELECT admissionno+'/'+feescode+'/'+'" & month & "' monthref,customerid,studentadmissionid,feesname,SchoolStudentFeesTb.Amount,isnull(AccountNo,0)AccountNo FROM SchoolFeesTb " & _
        "LEFT JOIN (select Amount,feesmasterid,schoolfeesid,studentadmissionid from SchoolStudentFeesTb )SchoolStudentFeesTb on SchoolFeesTb.feesid=SchoolStudentFeesTb.feesmasterid " & _
        "left join SchoolStudentAdmissionTb on SchoolStudentAdmissionTb.studentid=SchoolStudentFeesTb.studentadmissionid where  isnull(isyearly,0)=0)Tr left join (" & _
        "select reference,AccountNo  from AccTrDet  " & _
        "inner join AccTrCmn on AccTrCmn.LinkNo=AccTrDet.LinkNo where DealAmt>0 and JVType='SI' group by reference,AccountNo)acctr on tr.customerid=acctr.AccountNo and tr.monthref=acctr.Reference " & _
        "where isnull(Reference,'') ='' and   studentadmissionid=" & Val(txtcode.Tag)
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable(QRY)
        Dim amount As Double
        Dim accid As Long
        Dim incomeacc As Long
        If dt.Rows.Count > 0 Then
           
            If UsrBr = "" Then
                QRY = " Select accid from accmast where AccSetId like '%" & Format(4, "00") & "%'"
                QRY = QRY & " select Prefix,InvNo from InvNos where InvType='SI'"
            Else
                QRY = " SELECT accid FROM BranchAccSet WHERE branchcode='" & UsrBr & "' and setno=" & 4
                QRY = QRY & " select Prefix,InvNo from InvNosBrTb where InvType='SI' AND Brcode='" & UsrBr & "'"
            End If
            QRY = QRY & " select customerid from SchoolStudentAdmissionTb where studentid=" & Val(txtcode.Tag)
            Dim ds As DataSet
            ds = _objcmnbLayer._ldDataset(QRY, False)
            Dim prefix As String = ""
            Dim invno As Integer

            Dim customerid As Long
            If ds.Tables(0).Rows.Count > 0 Then
                incomeacc = ds.Tables(0)(0)("accid")
            End If
            If ds.Tables(1).Rows.Count > 0 Then
                prefix = ds.Tables(1)(0)("Prefix")
                invno = ds.Tables(1)(0)("InvNo")
            End If
            If ds.Tables(2).Rows.Count > 0 Then
                customerid = ds.Tables(2)(0)("customerid")
            End If
            dtAccTb.Rows.Clear()

            _objTr.JVType = "SI"
            _objTr.JVDate = DateValue(JVDate)
            _objTr.PreFix = prefix
            _objTr.JVNum = invno
            _objTr.JVTypeNo = getVouchernumber("SI")
            _objTr.UserId = CurrentUser
            _objTr.MchName = MACHINENAME
            _objTr.CrtDtTm = DateValue(Date.Now)
            _objTr.TypeNo = 0 ' id number from prefixtb
            _objTr.VrDescr = "FEES/" & txtcode.Text & "/" & txtName.Text
            _objTr.IsModi = 0
            _objTr.LinkNo = 0
            _objTr.isLinkNo = True
            _objTr.isdeleteTr = 1
            Dim i As Integer
            For i = 0 To dt.Rows.Count - 1
                If Val(dt(i)("Amount")) > 0 Then
                    setAcctrDetValue(True, customerid, dt(i)("feesname") & "/" & month, dt(i)("monthref"), dt(i)("Amount"))
                    amount = amount + Val(dt(i)("Amount"))
                    accid = 0
                    accid = dt(i)("AccountNo")
                    If accid = 0 Then accid = incomeacc
                    setAcctrDetValue(False, accid, "FEES/" & txtcode.Text & "/" & txtName.Text, "ON/AC", Val(dt(i)("Amount")))
                End If
            Next
            If amount > 0 Then
                _objTr.SaveAccTrWithDt(dtAccTb)
            End If

        End If

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
                loadAdmissiondetails()
            Case 2
                saveAdmission()
            Case 3
                loadrec()
            Case 4
                PrepareRpt(RptType)
        End Select
        If Not fwait Is Nothing Then
            fwait.Close()
            fwait = Nothing
        End If
    End Sub
    Private Sub PrepareRpt(ByVal RptType As String, Optional ByVal Forprint As Boolean = False, Optional ByVal isF As Boolean = False)
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
        Dim str As String
        'Dim accid As Long
        Dim dt As DataTable
        Dim dt1 As Date
        Dim dt2 As Date
        Dim frmdate As DateRangeFrm
        If TabControl2.SelectedIndex = 0 Then
            If RptType = "ST1" Then
                dt = _objcmnbLayer._fldDatatable("select accid from accmast where accid=" & accid)
                If dt.Rows.Count > 0 Then
                    accid = dt(0)("accid")
                    frmdate = New DateRangeFrm
                    fwait.Hide()
                    frmdate.TopMost = True
                    frmdate.ShowDialog()
                    If Val(frmdate.btnapply.Tag) = 0 Then
                        fwait.Visible = True
                        fwait.Focus()
                        fwait.TopMost = True
                        Exit Sub
                    End If
                    dt1 = DateValue(frmdate.cldrStartDate.Value)
                    dt2 = DateValue(frmdate.cldrEnddate.Value)
                    frmdate = Nothing
                    ds = _objTr.returnLEDGERstatementreport(dt1, dt2, accid, 0, 0, "customer", 0)
                    fwait.Visible = True
                End If
            ElseIf RptType = "STO" Then
                dt = _objcmnbLayer._fldDatatable("select accid from accmast where accid=" & accid)
                If dt.Rows.Count > 0 Then
                    accid = dt(0)("accid")
                    ds = _objTr.returnStatementReport(DateValue(Date.Now), DateValue(Date.Now), accid, 2, 0, "customer", 0)
                    Dim parmDt As DataTable
                    parmDt = _objcmnbLayer._fldDatatable(" select 1 Lnk, '" & Format(DateValue(cldrEnddate.Value), "dd/MM/yyyy") & "' As dtAsOn, 30" & _
                                                                            " As Ag1, 60 As Ag2, 90 As Ag3, " & _
                                                                            "120 As Ag4, '0' As SepPage, ''" & _
                                                                            " As WiseFld, 'Ageing based on  Invoice Date.'" & _
                                                                            " As Msg , '" & cldrStartDate.Value & "' As FDate,0 as IsPeriod,'' as Isadv From CompanyTb")
                    ds.Tables.Add(parmDt)
                End If
            Else
                str = profileQry(True)
                ds = _objcommonlayer._ldDataset(str, False)
            End If
        Else
            If rpttable Is Nothing Then
                str = qry()
                ds = _objcommonlayer._ldDataset(str, False)
            Else
rpt:
                ds.Tables.Add(rpttable)
            End If
        End If
        frm.SetReport(ds, FileName, 0, False, , ToPrint)
        frm.MdiParent = Me.MdiParent
        frm.Text = RptCaption
        If Not ToPrint Then frm.Show()

    End Sub

    Private Sub chkFormat_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFormat.CheckedChanged

    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        If TabControl2.SelectedIndex = 0 Then
            RptType = "SAF"
        Else
            RptType = "SAL"

        End If

        If RptType = "" Then Exit Sub
        If chkFormat.Checked Then
            fRptFormat = New RptFormatfrm
            fRptFormat.RptType = RptType
            fRptFormat.ShowDialog()
            fRptFormat = Nothing
        Else
            loadWaite(4)
        End If
    End Sub

    Private Sub btnbrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnbrowse.Click
        With DlgOpen
            .Filter = "All Picture Files|*.bmp;*.dib;*.gif;*.wmf;*.emf;*.jpg;*.ico;*.cur|" & _
                     "Bitmaps(*.bmp,*.dib)|*.bmp;*.dib|" & _
                     "Gif Images(*.gif)|*.gif|" & _
                     "JPEG Images(*.jpg)|*.jpg|" & _
                     "Matafiles(*.wmf,*.emf)|*.wmf;*.emf|" & _
                     "Icons(*.ico,*.cur)|*.ico;*.cur|" & _
                     "All Files(*.*)|*.*"
            .Title = "Select an Image file"
            .FileName = ""
            .ShowDialog()
            If .FileName <> "" Then
                Err.Clear()
                On Error Resume Next
                Dim bm As New Bitmap(.FileName)

                picstudent.SizeMode = PictureBoxSizeMode.StretchImage
                picstudent.Image = bm
                If Err.Number Then
                    MsgBox(Err.Description)
                Else
                    picstudent.Tag = .FileName
                    'lblpicpath.Text = .FileName
                    btnupdate.Enabled = True
                End If
            End If
            ChDir(Application.StartupPath)
        End With
    End Sub
    Private Sub saveImage(ByVal studentid As Long)
        If picstudent.Tag <> "" Then
            'On Error Resume Next
            If Directory.Exists(DPath & "\Photos") = False Then
                Directory.CreateDirectory(DPath & "\Photos")
            End If
            Dim imagename As String = "STUD-" & studentid & ".png"
            If DPath <> "" Then
                imagename = DPath & "Photos\" & imagename
                If FileExists(imagename) Then
                    System.IO.File.Delete(imagename)
                End If
                FileCopy(picstudent.Tag, imagename)
                If Err.Number Then
                    If MsgBox(Err.Description, vbRetryCancel + vbExclamation) = vbRetry Then Exit Sub
                End If
            End If
            'uploadtoServer(studentid)
        End If
    End Sub
    Private Sub loadimage()
        Try
            GoTo loadlocalimage
            If ftpurl <> "" And ftpusername <> "" And ftppassword <> "" Then
                'btnupload.Enabled = False
                picstudent.SizeMode = PictureBoxSizeMode.CenterImage
                picstudent.ImageLocation = Application.StartupPath & "\loader.gif"
                'LdPic(picImge, Application.StartupPath & "\loader.gif", Me)
                Dim fname As String = ftpurl & "/STUD-" & Val(txtcode.Tag) & ".png"
                Dim MyWebClient As New System.Net.WebClient
                AddHandler MyWebClient.DownloadDataCompleted, AddressOf DownloadDataCompleted
                MyWebClient.Credentials = New NetworkCredential(ftpusername, ftppassword)
                MyWebClient.DownloadDataAsync(New Uri(fname))
                'Dim ImageInBytes() As Byte = MyWebClient.DownloadData(fname)
                'Dim ImageStream As New IO.MemoryStream(ImageInBytes)
                'picImge.Image = New System.Drawing.Bitmap(ImageStream)
                'lblpicpath.Text = "ITM-" & PreitemId & ".png"
                'lblpicpath.Tag = DPath & "Photos\ITM-" & PreitemId & ".png"
            Else
                GoTo loadlocalimage
            End If
        Catch ex As Exception
            GoTo loadlocalimage
        End Try
        Exit Sub
loadlocalimage:
      
        If FileExists(DPath & "Photos\STUD-" & Val(txtcode.Tag) & ".png") Then
            LdPic(picstudent, DPath & "Photos\STUD-" & Val(txtcode.Tag) & ".png", Me)
            lblpicpath.Text = "STUD-" & Val(txtcode.Tag) & ".png"
            lblpicpath.Tag = DPath & "Photos\STUD-" & Val(txtcode.Tag) & ".png"
        Else
            picstudent.Image = Nothing
            lblpicpath.Text = ""
        End If
      
        'btnupload.Enabled = True
    End Sub
    Sub DownloadDataCompleted(ByVal sender As Object, ByVal e As DownloadDataCompletedEventArgs)
        If e.Cancelled = False AndAlso e.Error Is Nothing Then
            picstudent.SizeMode = PictureBoxSizeMode.StretchImage
            picstudent.Image = New Bitmap(New IO.MemoryStream(e.Result))
            lblpicpath.Text = "STUD-" & Val(txtcode.Tag) & ".png"
            lblpicpath.Tag = DPath & "Photos\STUD-" & Val(txtcode.Tag) & ".png"
        Else
            picstudent.Image = Nothing
            picstudent.ImageLocation = ""
            LdPic(picstudent, DPath & "Photos\STUD-" & Val(txtcode.Tag) & ".png", Me, True)
            lblpicpath.Text = "STUD-" & Val(txtcode.Tag) & ".png"
            lblpicpath.Tag = DPath & "Photos\STUD-" & Val(txtcode.Tag) & ".png"
            'btnupload.Enabled = True
        End If
    End Sub
    'Private Sub uploadtoServer(ByVal studentid As Long)
    '    If ftpurl <> "" And ftpusername <> "" And ftppassword <> "" Then
    '        Dim imagename As String = DPath & "Photos\STUDENT-" & studentid & ".png"
    '        If FileExists(imagename) Then
    '            If MsgBox("Do you want update image to webserver?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.No Then Exit Sub

    '            Dim request As FtpWebRequest = DirectCast(WebRequest.Create(New Uri(ftpurl & "/STUDENT-" & studentid & ".png")), System.Net.FtpWebRequest)
    '            request.Method = WebRequestMethods.Ftp.UploadFile
    '            request.Credentials = New NetworkCredential(ftpusername, ftppassword)
    '            request.UseBinary = True
    '            request.UsePassive = True

    '            Dim buffer(1023) As Byte
    '            Dim bytesIn As Long = 1
    '            Dim totalBytesIn As Long = 0

    '            Dim filepath As System.IO.FileInfo = New System.IO.FileInfo(imagename)
    '            Dim ftpstream As System.IO.FileStream = filepath.OpenRead()
    '            Dim flLength As Long = ftpstream.Length
    '            Dim reqfile As System.IO.Stream = request.GetRequestStream()

    '            Do Until bytesIn < 1
    '                bytesIn = ftpstream.Read(buffer, 0, 1024)
    '                If bytesIn > 0 Then
    '                    reqfile.Write(buffer, 0, bytesIn)
    '                    totalBytesIn += bytesIn
    '                End If
    '            Loop
    '            reqfile.Close()
    '            ftpstream.Close()
    '        End If
    '    End If
    'End Sub

    Private Sub grdList_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdList.CellContentClick

    End Sub

    Private Sub grdfees_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdfees.CellContentClick

    End Sub



    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("select linkno from AccTrDet left join AccMast on AccTrDet.AccountNo=AccMast.AccId where accmast.accid ='" & accid & "'")

        If dt.Rows.Count > 0 Then
            MsgBox("Transaction found ", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If MsgBox("You are going to REMOVE the student # " & txtName.Text & Chr(13) & "Are you sure ?", vbYesNo + vbQuestion + vbDefaultButton2) = MsgBoxResult.Yes Then

            _objcmnbLayer._saveDatawithOutParm("delete from SchoolStudentAdmissionTb where studentid=" & Val(txtcode.Tag) & _
                                               "delete from SchoolStudentFeesTb where studentadmissionid=" & Val(txtcode.Tag))
            AddNew()

        End If
    End Sub

    Private Sub btnstatement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnstatement.Click
        'Dim RptType As String
        RptType = "ST1"
        If chkFormat.Checked Then
            fRptFormat = New RptFormatfrm
            fRptFormat.RptType = RptType
            fRptFormat.ShowDialog()
            fRptFormat = Nothing
        Else
            loadWaite(4)
        End If
    End Sub

    Private Sub btnoutstanding_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnoutstanding.Click
        RptType = "STO"
        If chkFormat.Checked Then
            fRptFormat = New RptFormatfrm
            fRptFormat.RptType = RptType
            fRptFormat.ShowDialog()
            fRptFormat = Nothing
        Else
            loadWaite(4)
        End If
    End Sub

    Private Sub btndownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndownload.Click
        Dim filename As String = ""
        Dim imagename As String = DPath & "Photos\STUD-" & Val(txtcode.Tag) & ".png"
        If FileExists(imagename) Then
            FolderBrowserDialog1.ShowDialog()
            If FolderBrowserDialog1.SelectedPath <> "" Then
                filename = FolderBrowserDialog1.SelectedPath
            Else
                Exit Sub
            End If
            filename = filename & "/" & txtcode.Text & ".png"
            FileCopy(imagename, filename)
            MsgBox("Done", MsgBoxStyle.Information)
        Else
            MsgBox("Image not found", MsgBoxStyle.Critical)
        End If
        
    End Sub
    Public Sub status(ByVal Mname As String, ByVal RecName As String, ByVal rec As Integer, ByVal count As Integer)
        If Me.InvokeRequired Then
            Dim d As New GenericDelegate(AddressOf status)
            Me.Invoke(d, Mname, RecName, rec, count)
        Else
            lblitem.Text = Mname
            If rec = 0 Then
                pb.Value = 0
            Else
                pb.Value = rec * 100 / count
            End If
        End If
    End Sub

    Private Sub Worker_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles Worker.DoWork
        Dim i As Integer
        'Dim accid As Long
        Dim dt As DataTable
        rdCount = DateDiff(DateInterval.Month, dt1, dt2) + 1
        'dt = _objcmnbLayer._fldDatatable("select accid from accmast where accid=" & AccountNo)
        'If dt.Rows.Count > 0 Then
        '    accid = dt(0)("accid")
        'End If
        If rdCount > 0 Then
            For i = 0 To rdCount - 1
                dt1 = DateValue("01/" & Month(dt1) & "/" & Year(dt1))
                dt1 = DateAdd(DateInterval.Month, 1, dt1)
                dt1 = DateAdd(DateInterval.Day, -1, dt1)
                createFeesSI(dt1)
                dt1 = DateAdd(DateInterval.Month, 1, dt1)
                status("Updating Fees Booking :- " & dt1, "", i, rdCount)
            Next
            status("Updating Fees Booking :- Updated", "", rdCount, rdCount)
        Else
            MsgBox("Please select students", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub Worker_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Worker.RunWorkerCompleted
        MsgBox("Updated " & rdCount & " Records", MsgBoxStyle.Information)
        pb.Value = 0
        pb.Visible = False
        lblitem.Visible = False
    End Sub

    Private Sub btnmultiplefees_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnmultiplefees.Click
        If MsgBox("Do you want to generate Fees Booking for Multiple Month", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        pb.Visible = True
        lblitem.Visible = True
        Dim frmdate As New DateRangeFrm
        frmdate.ShowDialog()
        dt1 = DateValue(frmdate.cldrStartDate.Value)
        dt2 = DateValue(frmdate.cldrEnddate.Value)
        frmdate = Nothing
        Worker.RunWorkerAsync()
    End Sub

    Private Sub picwhatsapp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picwhatsapp.Click
        Dim f As New WhatsaapFrm
        f.txtphone.Text = txtphone.Text
        f.txtparty.Text = txtName.Text
        f.TopMost = True
        f.txtjobcode.Text = txtcode.Text
        f.txtamount.Text = lbloutstanding.Text
        f.txtreceived.Text = 0
        f.txtreceived.Visible = False
        f.chkoutstanding.Checked = True
        f.chkoutstanding.Enabled = False
        f.Label6.Visible = False
        f.Jobtype = "Admission No"
        f.ShowDialog()
    End Sub

    Private Sub txtopening_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtopening.KeyDown
        If e.KeyCode = Keys.Return Then
            btnupdate.Focus()
        End If
    End Sub

    Private Sub txtopening_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtopening.KeyPress
        NumericTextOnKeypress(txtopening, e, chgbyprg, numFormat)
    End Sub

    Private Sub btnsales_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsales.Click
        If accid = 0 Then
            MsgBox("Invalid Student", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If fsales Is Nothing Then
            fsales = New SalesInvoice
            With fsales
                .MdiParent = fMainForm
                .Show()
                .loadfromClinic(accid, "", txtcode.Text, txtName.Text)
            End With
        End If
    End Sub

    Private Sub fsales_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fsales.FormClosed
        fsales = Nothing
    End Sub

    Private Sub txtopening_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtopening.TextChanged

    End Sub

    Private Sub btneditinvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btneditinvoice.Click
        If grdinvList.RowCount = 0 Then Exit Sub
        If grdinvList.CurrentRow Is Nothing Then Exit Sub
        fMainForm.LoadIS(Val(grdinvList.Item("trid", grdinvList.CurrentRow.Index).Value))
    End Sub

    Private Sub grdinvList_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdinvList.DoubleClick
        If grdinvList.RowCount = 0 Then Exit Sub
        If grdinvList.CurrentRow Is Nothing Then Exit Sub
        fMainForm.LoadIS(Val(grdinvList.Item("trid", grdinvList.CurrentRow.Index).Value))
    End Sub
End Class