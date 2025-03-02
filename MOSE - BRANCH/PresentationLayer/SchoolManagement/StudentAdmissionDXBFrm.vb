Imports System.Net
Imports System.IO

Public Class StudentAdmissionDXBFrm

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
    Private WithEvents fsales As FeesSalesInvoice
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
       
        cldrdate.Value = DateValue(Date.Now)
        dtpdob.Value = DateValue(Date.Now)
        rdomale.Checked = True
        rdoactive.Checked = True
        cmbclassteacher.Text = ""
        cmbclassteacher.Tag = 0
        lblteacherphone.Text = "Phone : "
       
        picstudent.Image = Nothing
        jobid = 0
        lbloutstanding.Text = "0.00"
        picstudent.BackgroundImage = Nothing
        lblpicpath.Text = ""
        picID.Image = Nothing
        picID.BackgroundImage = Nothing
        lblID.Text = ""
        btnnew.Text = "Clear"
        btnPreview.Text = "Print ID"
        txtremark.Text = ""
        chgbyprg = False
        chgamtbyprg = False
        btnsales.Enabled = False
        accid = 0
        txtcode.Tag = ""
        txtopening.Text = Format(0, numFormat)
        'loadReceipt()
        grdreceipt.DataSource = Nothing
        grdinvList.DataSource = Nothing
        grdinstallments.DataSource = Nothing
        txtName.Focus()
    End Sub
    Private Function GenerateNext(ByVal strCode As String) As String
        Dim N As Double
        Dim NewCode As String
        Dim i As Byte
        Dim f As Byte
        Dim tmp As String
        Dim dt As DataTable
        _objcmnbLayer = New clsCommon_BL
        If strCode = "" Or Val(txtcode.Tag) > 0 Then
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
        loadTeachers()
        Timer1.Enabled = True
    End Sub

    Private Sub txtadmissionfees_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Return Then
            txtopening.Focus()
        End If
    End Sub

    Private Sub txtcode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtcode.KeyDown, cldrdate.KeyDown, txtName.KeyDown, txtAddr0.KeyDown, _
    txtAddr1.KeyDown, txtAddr2.KeyDown, txtfathername.KeyDown, txtphone.KeyDown, txtpassport.KeyDown, txtemiratesid.KeyDown, txtnationality.KeyDown, txtemail.KeyDown, _
       txtbgroup.KeyDown, cmbclassteacher.KeyDown, dtpdob.KeyDown
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
        Dim AccountNo As Long
        If AccountNo = 0 Then
            GenerateNextAccountno(AccountNo)
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
            '.standered = txtstandered.Text
            .stadd1 = txtAddr0.Text
            .stadd2 = txtAddr1.Text
            .stadd3 = txtAddr2.Text
            '.section = txtsection.Text
            '.rollnumber = txtenrollnumber.Text
            If rdomale.Checked Then
                .gender = 0
            Else
                .gender = 1
            End If
            .dateofbirth = DateValue(dtpdob.Value)
            .stphone = txtphone.Text
            .studentemail = txtemail.Text
            .religion = ""
            .studentcast = ""
            .bloodgroup = txtbgroup.Text
            .fathername = txtfathername.Text
            .emiratesid = txtemiratesid.Text
            .passportno = txtpassport.Text
            '.motheroccupation = txtMoccupation.Text
            .nationality = txtnationality.Text
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

            .admissionfees = 0
            .AccountNo = accid
            'Dim s1accno As String = AccountNo
            's1accno = Mid(s1accno, 1, 4)
            .S1Accid = S1Accid
            .remark = txtremark.Text

            txtcode.Tag = .saveSchoolStudentAdmissionTb()
        End With
        saveImage(_objJob.studentid)
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

        str = str & " select feesname,Amount,monthref,convert(varchar,jvdate,103) lastpaid,feesid,schoolfeesid,studentadmissionid,isnull(dealamt,0)RVAmt,Amount-isnull(dealamt,0) Balance,Yearly from(  select feesname,isnull(SchoolStudentFeesTb.Amount,0)Amount,feesid,isnull(schoolfeesid,0)schoolfeesid,admissionno+'/'+feescode+'/'+'" & month & "' monthref,customerid,studentadmissionid, " & _
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
    Public Sub loadrec()
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
            'txtstandered.Text = dt(0)("standered")
            txtAddr0.Text = dt(0)("stadd1")
            txtAddr1.Text = dt(0)("stadd2")
            txtAddr2.Text = dt(0)("stadd3")
            'txtsection.Text = dt(0)("section")
            'txtenrollnumber.Text = dt(0)("rollnumber")
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

            txtbgroup.Text = dt(0)("bloodgroup")
            txtfathername.Text = dt(0)("fathername")
            txtemiratesid.Text = Trim(dt(0)("emiratesid") & "")
            txtpassport.Text = Trim(dt(0)("passportno") & "")
            txtnationality.Text = Trim(dt(0)("nationality") & "")
            txtremark.Text = Trim(dt(0)("remark") & "")
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
            txtcode.Focus()
            dt = Nothing

            lbloutstanding.Text = Format(outstanding + opening, numFormat)
            dt = Nothing
            dt = ds.Tables(3)

            loadReceipt()
            loadInvs()
            loadimage()
            TabControl2.SelectedIndex = 0
            txtcode.Focus()
            btnDelete.Enabled = True
            btnsales.Enabled = True
            'grdfees.Item(constfeeslastpaid, 1).Value = ""
            'btnrv.Visible = True
        End If
        btnnew.Text = "New"
        btnPreview.Text = "Print ID"
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
            .Columns("fathername").Visible = False

            .Columns("stphone").HeaderText = "Phone"
            .Columns("stphone").Width = 150

            .Columns("mothername").HeaderText = "Mother Name"
            .Columns("mothername").Visible = False

            .Columns("bloodgroup").HeaderText = "B.Group"
            .Columns("bloodgroup").Width = 75

            .Columns("standered").HeaderText = "Standered"
            .Columns("standered").Visible = False

            .Columns("section").HeaderText = "Section"
            .Columns("section").Visible = False

            .Columns("Teacher").HeaderText = "Teacher"
            .Columns("Teacher").Width = 150

            .Columns("teacherphone").HeaderText = "Teacher Phone"
            .Columns("teacherphone").Visible = False

            .Columns("gender").HeaderText = "Gender"
            .Columns("gender").Width = 100

            .Columns("dateofbirth").HeaderText = "DOB"
            .Columns("dateofbirth").Width = 100

            .Columns("religion").HeaderText = "Religion"
            .Columns("religion").Visible = False

            .Columns("studentcast").HeaderText = "Cast"
            .Columns("studentcast").Visible = False



            .Columns("studentid").Visible = False
            .Columns("lnk").Visible = False
        End With
        Dim i As Integer = 0
        For i = 0 To grdList.ColumnCount - 2
            cmbOrder.Items.Add(grdList.Columns(i).HeaderText)
        Next
        If cmbOrder.Items.Count > 0 Then cmbOrder.SelectedIndex = 0
    End Sub

    Private Sub TabControl2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl2.Click
        If TabControl2.SelectedIndex = 1 Then
            loadFilters()
            cmbclass.SelectedIndex = 0
            cmbgender.SelectedIndex = 0
            cmbstatus.SelectedIndex = 0
            loadWaite(1)
            btnPreview.Text = "Preview"
            'loadAdmissiondetails()
        Else
            btnPreview.Text = "Print ID"
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
        _objTr.IsModi = 0
        _objTr.LinkNo = 0
        _objTr.isLinkNo = True
        _objTr.isdeleteTr = 1

        setAcctrDetValue(True, customerid, "ADMISSION FEES BOOKING/" & txtName.Text & "/" & txtcode.Text, "ADM/" & txtcode.Text, 0)
        setAcctrDetValue(False, incomeaccid, "ADMISSION FEES BOOKING/" & txtName.Text & "/" & txtcode.Text, "ADM/" & txtcode.Text, 0)
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
        'Monthly()
        'Quartarly()
        'Half(Yearly)
        'Yearly()
        Dim strSql As String = ("Select Itemname [Course Name],InvAmt Amount,rvamt,InvAmt-rvamt Balance, TrRefNo [Inv No] , TrDate [Tr.Date],courseduration [Duration]," & _
                                "case when isnull(turnurid,0)=0 then 'Monthly' when isnull(turnurid,0)=1 then 'Quartarly' " & _
                                "when isnull(turnurid,0)=2 then 'Half Yearly' else 'Yearly' end [Mode]  ,TrId from " & _
                               "( select  TrRefNo,TrDate ,netamt InvAmt,prefix, invNo,ItmInvCmnTb.TrId,Itemname,isnull(rvamt,0)rvamt,courseduration,turnurid from ItmInvCmnTb " & _
                               "inner JOIN (SELECT Trid,invitm.Description Itemname,isnull(courseduration,0)courseduration FROM ItmInvTrTb left join invitm on ItmInvTrTb.itemid=invitm.itemid " & _
                               "where itemCategory='course') Tr ON  ItmInvCmnTb.Trid=Tr.Trid  " & _
                               "left join (select sum(dealamt*-1) rvamt,Reference,AccountNo from AccTrDet " & _
                               "inner join AccTrCmn on AccTrCmn.LinkNo=AccTrDet.LinkNo where DealAmt<0 group by Reference,AccountNo)rv on ItmInvCmnTb.CSCode=rv.AccountNo and rv.Reference=ItmInvCmnTb.TrRefNo " & _
                               "left join Accmast on ItmInvCmnTb.CSCode=Accmast.accid where ItmInvCmnTb.trtype='IS' and CSCode =" & accid & ") qq  order by TrDate ,InvNo")
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
            .Columns("Amount").Width = 80

            .Columns("rvamt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("rvamt").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("rvamt").HeaderText = "Received"
            .Columns("rvamt").Width = 80
            .Columns("rvamt").DefaultCellStyle.BackColor = Color.LightGreen

            .Columns("Balance").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Balance").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Balance").DefaultCellStyle.BackColor = Color.LightPink

            .Columns("Inv No").Width = 70
            .Columns("Tr.Date").Width = 70

            .Columns("Duration").Width = 80
            .Columns("Mode").Width = 80
            .Columns("Course Name").Width = 150


            .Columns("trid").Visible = False
        End With
        'resizeGridColumn(grdinvList, 0)
    End Sub

    Private Sub TabControl1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.Click
        If TabControl1.SelectedIndex = 1 Then
            resizeGridColumn(grdreceipt, 2)
        Else
            resizeGridColumn(grdinvList, 0)
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
                resizeGridColumn(grdList, 3)
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
                ds.Tables(0).Columns.Add("Image", GetType(Byte()))
                Dim ppath As String = Trim(lblpicpath.Tag)
                If File.Exists(ppath) Then
                    Dim img As Image = Image.FromFile(ppath)
                    Dim bytes As Byte() = CType((New ImageConverter()).ConvertTo(img, GetType(Byte())), Byte())
                    ds.Tables(0)(0)("Image") = bytes
                End If
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
            RptType = "SAID"
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
            'uploadtoServer(studentid, "STUD")
        End If
        If picID.Tag <> "" Then
            'On Error Resume Next
            If Directory.Exists(DPath & "\Photos") = False Then
                Directory.CreateDirectory(DPath & "\Photos")
            End If
            Dim imagename As String = "STUDID-" & studentid & ".png"
            If DPath <> "" Then
                imagename = DPath & "Photos\" & imagename
                If FileExists(imagename) Then
                    System.IO.File.Delete(imagename)
                End If
                FileCopy(picID.Tag, imagename)
                If Err.Number Then
                    If MsgBox(Err.Description, vbRetryCancel + vbExclamation) = vbRetry Then Exit Sub
                End If
            End If
            'uploadtoServer(studentid, "STUDID")
        End If
    End Sub
    Private Sub loadimage()
        Try
            GoTo loadlocalimage
            If ftpurl <> "" And ftpusername <> "" And ftppassword <> "" Then
                picstudent.SizeMode = PictureBoxSizeMode.CenterImage
                picstudent.ImageLocation = Application.StartupPath & "\loader.gif"
                Dim fname As String = ftpurl & "/STUD-" & Val(txtcode.Tag) & ".png"
                Dim MyWebClient As New System.Net.WebClient
                AddHandler MyWebClient.DownloadDataCompleted, AddressOf DownloadDataCompleted
                MyWebClient.Credentials = New NetworkCredential(ftpusername, ftppassword)
                MyWebClient.DownloadDataAsync(New Uri(fname))
                'ID PROOF
                picID.SizeMode = PictureBoxSizeMode.CenterImage
                picID.ImageLocation = Application.StartupPath & "\loader.gif"
                fname = ftpurl & "/STUDID-" & Val(txtcode.Tag) & ".png"
                Dim MyWebClient1 As New System.Net.WebClient
                AddHandler MyWebClient1.DownloadDataCompleted, AddressOf DownloadDataIDCompleted
                MyWebClient1.Credentials = New NetworkCredential(ftpusername, ftppassword)
                MyWebClient1.DownloadDataAsync(New Uri(fname))
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
        If FileExists(DPath & "Photos\STUDID-" & Val(txtcode.Tag) & ".png") Then
            LdPic(picID, DPath & "Photos\STUDID-" & Val(txtcode.Tag) & ".png", Me)
            lblID.Text = "STUD-" & Val(txtcode.Tag) & ".png"
            lblID.Tag = DPath & "Photos\STUDID-" & Val(txtcode.Tag) & ".png"
        Else
            picID.Image = Nothing
            lblID.Text = ""
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
    Private Sub uploadtoServer(ByVal studentid As Long, ByVal imgtp As String)
        If ftpurl <> "" And ftpusername <> "" And ftppassword <> "" Then
            Dim imagename As String = DPath & "Photos\" & imgtp & "-" & studentid & ".png"
            If FileExists(imagename) Then
                If MsgBox("Do you want update image to webserver?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.No Then Exit Sub

                Dim request As FtpWebRequest = DirectCast(WebRequest.Create(New Uri(ftpurl & "/" & imgtp & "-" & studentid & ".png")), System.Net.FtpWebRequest)
                request.Method = WebRequestMethods.Ftp.UploadFile
                request.Credentials = New NetworkCredential(ftpusername, ftppassword)
                request.UseBinary = True
                request.UsePassive = True

                Dim buffer(1023) As Byte
                Dim bytesIn As Long = 1
                Dim totalBytesIn As Long = 0

                Dim filepath As System.IO.FileInfo = New System.IO.FileInfo(imagename)
                Dim ftpstream As System.IO.FileStream = filepath.OpenRead()
                Dim flLength As Long = ftpstream.Length
                Dim reqfile As System.IO.Stream = request.GetRequestStream()

                Do Until bytesIn < 1
                    bytesIn = ftpstream.Read(buffer, 0, 1024)
                    If bytesIn > 0 Then
                        reqfile.Write(buffer, 0, bytesIn)
                        totalBytesIn += bytesIn
                    End If
                Loop
                reqfile.Close()
                ftpstream.Close()
            End If
        End If
    End Sub
    Sub DownloadDataIDCompleted(ByVal sender As Object, ByVal e As DownloadDataCompletedEventArgs)
        If e.Cancelled = False AndAlso e.Error Is Nothing Then
            picID.SizeMode = PictureBoxSizeMode.StretchImage
            picID.Image = New Bitmap(New IO.MemoryStream(e.Result))
            lblID.Text = "STUDID-" & Val(txtcode.Tag) & ".png"
            lblID.Tag = DPath & "Photos\STUDID-" & Val(txtcode.Tag) & ".png"
        Else
            picID.Image = Nothing
            picID.ImageLocation = ""
            LdPic(picID, DPath & "Photos\STUDID-" & Val(txtcode.Tag) & ".png", Me, True)
            lblpicpath.Text = "STUDID-" & Val(txtcode.Tag) & ".png"
            lblpicpath.Tag = DPath & "Photos\STUDID-" & Val(txtcode.Tag) & ".png"
        End If
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

    Private Sub btnmultiplefees_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
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
            fsales = New FeesSalesInvoice
            With fsales
                .MdiParent = fMainForm
                .Show()
                .loadfromClinic(accid, "")
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
        fMainForm.editFeesInvoice(Val(grdinvList.Item("trid", grdinvList.CurrentRow.Index).Value))
    End Sub
    

    Private Sub grdinvList_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdinvList.DoubleClick
        If grdinvList.RowCount = 0 Then Exit Sub
        If grdinvList.CurrentRow Is Nothing Then Exit Sub
        fMainForm.editFeesInvoice(Val(grdinvList.Item("trid", grdinvList.CurrentRow.Index).Value))
    End Sub
    Private Sub loadInstallments(ByVal trid As Long)
        Dim dt As DataTable
        Dim str As String = "select ROW_NUMBER() OVER(ORDER BY installmentdate ASC) AS Slno, installmentdate,Amount,isnull(rvamt,0)rvamt,amount-isnull(rvamt,0) Balance from InstallmentTb " & _
        "left join (select sum(DealAmt*-1)rvamt,insmntid from AccTrDet " & _
        "left join AccTrCmn ON AccTrDet.LinkNo=AccTrCmn.LinkNo where JVType='RV' group by insmntid)rv on InstallmentTb.id=rv.insmntid where trid=" & trid
        dt = _objcmnbLayer._fldDatatable(str)
        grdinstallments.DataSource = dt
        With grdinstallments
            SetGridProperty(grdinstallments)
            .Columns("Slno").HeaderText = "SLNO"
            .Columns("Slno").Width = 80

            .Columns("installmentdate").HeaderText = "Date"
            .Columns("installmentdate").Width = 80

            .Columns("Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Amount").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Amount").Width = 80

            .Columns("rvamt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("rvamt").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("rvamt").HeaderText = "Received"
            .Columns("rvamt").DefaultCellStyle.BackColor = Color.LightGreen
            .Columns("rvamt").Width = 80

            .Columns("Balance").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Balance").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Balance").Width = 80
            .Columns("Balance").DefaultCellStyle.BackColor = Color.LightPink

            '.Columns("Inv No").Width = 70
            '.Columns("Tr.Date").Width = 70
        End With
        resizeGridColumn(grdinstallments, 1)
    End Sub

    Private Sub grdinvList_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdinvList.RowEnter
        Dim trid As Long
        If grdinvList.RowCount = 0 Then Exit Sub
        trid = grdinvList.Item("trid", e.RowIndex).Value
        loadInstallments(trid)
    End Sub

    Private Sub btnbrowseid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnbrowseid.Click
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

                picID.SizeMode = PictureBoxSizeMode.StretchImage
                picID.Image = bm
                If Err.Number Then
                    MsgBox(Err.Description)
                Else
                    picID.Tag = .FileName
                    'lblpicpath.Text = .FileName
                    btnupdate.Enabled = True
                End If
            End If
            ChDir(Application.StartupPath)
        End With
    End Sub

    Private Sub btndownloadid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndownloadid.Click
        Dim filename As String = ""
        Dim imagename As String = DPath & "Photos\STUDID-" & Val(txtcode.Tag) & ".png"
        If FileExists(imagename) Then
            FolderBrowserDialog1.ShowDialog()
            If FolderBrowserDialog1.SelectedPath <> "" Then
                filename = FolderBrowserDialog1.SelectedPath
            Else
                Exit Sub
            End If
            filename = filename & "/" & txtcode.Text & "-ID.png"
            FileCopy(imagename, filename)
            MsgBox("Done", MsgBoxStyle.Information)
        Else
            MsgBox("Image not found", MsgBoxStyle.Critical)
        End If
    End Sub
    
    
End Class