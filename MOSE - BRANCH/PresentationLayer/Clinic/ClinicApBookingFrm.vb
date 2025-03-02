Public Class ClinicApBookingFrm
    Private chgbyprg As Boolean
    Private _srchOnce As Boolean
    Private _srchTxtId As Byte
    Private _srchIndexId As Byte
    Private WithEvents fMList As Mlistfrm
    Private WithEvents fSelect As Selectfrm
    Private MyActiveControl As New Object
    Private lstKey As Keys
    Private _objcmnbLayer As New clsCommon_BL
    Public isModi As Boolean
    Private _objclinic As New clsClinic
    Private dttable As DataTable
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub txtRec0_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRec0.KeyDown
        lstKey = e.KeyCode
        Dim MyCtrl As TextBox = sender
        If e.KeyCode = Keys.F2 Then
            If txtRec0.ReadOnly Then Exit Sub
            If Not fMList Is Nothing Then fMList.Visible = False
            _srchTxtId = IIf(MyCtrl.Name = "txtRec0", 1, 2)
            ldSelect(12)
        ElseIf e.KeyCode = Keys.Up Or e.KeyCode = Keys.PageUp Then
            If Not fMList Is Nothing Then
                If fMList.Visible Then
                    fMList.MoveUp(MyCtrl.Text)
                    Exit Sub
                End If
            End If

        ElseIf e.KeyCode = Keys.Down Or e.KeyCode = Keys.PageDown Then
            If Not fMList Is Nothing Then
                If fMList.Visible Then
                    fMList.MoveDown(MyCtrl.Text)
                    Exit Sub
                End If
            End If
        ElseIf e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
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

    Private Sub fSelect_doSelect(ByVal KeyId As String, ByVal strFld1 As String, ByVal strFld2 As String) Handles fSelect.doSelect
        chgbyprg = True
        txtRec1.Text = strFld1
        txtRec0.Text = strFld2
        txtRec0.Tag = KeyId
        setCustomer()
        txtRec1.Focus()
        chgbyprg = False
    End Sub
    Private Sub setCustomer(Optional ByVal accid As Long = 0, Optional ByVal skiploadHistory As Boolean = False)
        Dim dt As DataTable
        Dim condition As String
        If txtRec0.Text = "" And accid = 0 Then GoTo els
        If accid > 0 Then
            condition = "where accid=" & accid
        Else
            condition = "where Alias='" & txtRec0.Text & "'"
        End If
        dt = _objcmnbLayer._fldDatatable("SELECT AccMast.accid,Alias,AccDescr,Address1,Address2,Address3,Address4," & _
                                        "Phone,Fax,EMail,PT1,PT2,PT3,PT4,TrdLcno,TrdDate,CreditLimit,DueDays,SlsmanId," & _
                                        "CurrencyCode,CountryCode,GSTIN,Remarks,GrpSetOn " & _
                                        " from AccMast LEFT JOIN AccMastAddr ON AccMast.accid=AccMastAddr.AccountNo " & _
                                        "LEFT JOIN S1AccHd ON AccMast.S1AccId=S1AccHd.S1AccId " & _
                                        condition)
        If dt.Rows.Count > 0 Then
            txtRec0.Tag = dt(0)("accid")
            chgbyprg = True
            txtRec0.Text = Trim("" & dt(0)("Alias"))
            txtRec1.Text = Trim("" & dt(0)("AccDescr"))
            txtadd1.Text = Trim(dt(0)("Address1") & "")
            txtadd2.Text = Trim(dt(0)("Address2") & "")
            txtadd3.Text = Trim(dt(0)("Address3") & "")
            txtphone.Text = Trim(dt(0)("Phone") & "")
            btnclear.Text = "Undo"
        Else
els:
            txtRec0.Text = ""
            txtRec0.Tag = ""
            txtRec1.Text = ""
            txtadd1.Text = ""
            txtadd2.Text = ""
            txtadd3.Text = ""
            txtphone.Text = ""
        End If
        chgbyprg = False
    End Sub

    Private Sub fSelect_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fSelect.FormClosed
        fSelect = Nothing
    End Sub

    Private Sub ClinicApBookingFrm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        cmbsalesman.Focus()
    End Sub

    
    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        clear()
    End Sub
    Private Sub clear()
        chgbyprg = True
        txtRec0.Text = ""
        txtRec0.Tag = ""
        txtRec1.Text = ""
        numVchrNo.Tag = ""
        txtremarks.Text = ""
        cmbsalesman.Text = ""
        txtadd1.Text = ""
        txtadd2.Text = ""
        txtadd3.Text = ""
        txtphone.Text = ""
        cmbtype.Text = "OP"
        cmbreference.Text = ""
        dtptime.Value = Date.Now
        dtpbookingfor.Value = DateValue(Date.Now)
        btnclear.Text = "Clear"
        NextNumber()
        txtRec0.Focus()
        chgbyprg = False
    End Sub
    Private Sub NextNumber()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("select max(bookingno) maxno from ClinicVisitBookingTb")
        If dt.Rows.Count > 0 Then
            Dim maxno As Integer = Val(dt(0)("maxno") & "")
            numVchrNo.Text = maxno + 1
            Exit Sub
        End If
        numVchrNo.Text = 1
    End Sub

    Private Sub ClinicApBookingFrm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        NextNumber()
        ldDoctor()
        loadrefernce()
        Dim ObjLocationlist As New List(Of String)
        ObjLocationlist = toLoadAutoFillListItems("Alias", "AccMast left join S1AccHd on S1AccHd.S1AccId=AccMast.S1AccId where GrpSetOn='customer'")
        Call toAssignDownListToText(txtRec0, ObjLocationlist) '
        If Not userType Then
            btndelete.Tag = 1
        Else
            btnupdate.Tag = 1
            btndelete.Tag = IIf(getRight(242, CurrentUser), 1, 0)
        End If
    End Sub
    Private Sub ldBookings()
        Dim condition As String
        If rdobooking.Checked Then
            condition = "bookingdate>='" & Format(DateValue(dtpfrom.Value), "yyyy/MM/dd") & "' AND bookingdate<='" & Format(DateValue(dtpto.Value), "yyyy/MM/dd") & "' ORDER BY bookingdate "
        Else
            condition = "bookingFor>='" & Format(DateValue(dtpfrom.Value), "yyyy/MM/dd") & "' AND bookingFor<='" & Format(DateValue(dtpto.Value), "yyyy/MM/dd") & "' ORDER BY bookingFor "
        End If

        dttable = _objcmnbLayer._fldDatatable("SELECT bookingno [No],visitno [App No],bookingFor [Date], ltrim(right (convert(varchar,bokkingtime,100),7))BookTime,Alias [OP Number]," & _
                                              "patientName [Patient Name],add1+add2 Address,ClinicVisitBookingTb.Phone,Doctor,referenceBy Reference," & _
                                              "bookingid from ClinicVisitBookingTb " & _
                                             "Left join Accmast on ClinicVisitBookingTb.customerid=Accmast.accid " & _
                                             "Left join AccMastAddr on ClinicVisitBookingTb.customerid=AccMastAddr.accountno " & _
                                             "Left join (Select visitno,bookingid bid from ClinicVistTb)ClinicVistTb on ClinicVisitBookingTb.bookingid=ClinicVistTb.bid " & _
                                             " where " & condition)
        grdlist.DataSource = dttable
        SetGridHeadList()
        resizeGridColumn(grdlist, 5)
    End Sub
    Sub SetGridHeadList()
        With grdlist
            SetGridProperty(grdlist)
            .Columns("No").Width = 50
            .Columns("App No").Width = 75
            .Columns("Date").Width = 100
            .Columns("BookTime").Width = 100
            .Columns("OP Number").Width = 100
            .Columns("Address").Width = 100
            .Columns("Doctor").Width = 100
            .Columns("Reference").Width = 100
            .Columns("bookingid").Visible = False
            '.Columns("bid").Visible = False
        End With
        setComboGrid()
    End Sub
    Private Sub setComboGrid()
        ChgByPrg = True
        cmbOrder.Items.Clear()
        Dim i As Integer = 0
        For i = 0 To grdlist.ColumnCount - 2
            cmbOrder.Items.Add(grdlist.Columns(i).HeaderText)
        Next
        If cmbOrder.Items.Count > 0 Then cmbOrder.SelectedIndex = 0
        ChgByPrg = False
    End Sub
    Private Sub ldDoctor()
        Dim _slsManTable As DataTable = _objcmnbLayer._fldDatatable("SELECT SManCode FROM SalesmanTb WHERE isnull(isdoctor,0)=1")
        Dim i As Integer
        cmbsalesman.Items.Add("")
        For i = 0 To _slsManTable.Rows.Count - 1
            cmbsalesman.Items.Add(_slsManTable(i)("SManCode"))
        Next
    End Sub

    Private Sub txtSeq_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSeq.TextChanged
        grdlist.DataSource = SearchGrid(dttable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
    End Sub

    Private Sub rdobooking_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdobooking.Click, rdobookingfor.Click
        ldBookings()
    End Sub

    Private Sub TabControl1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.Click
        If TabControl1.SelectedIndex = 1 Then
            If btnclear.Text = "Undo" Then
                MsgBox("Please Undo to clear selected record", MsgBoxStyle.Exclamation)
                TabControl1.SelectedIndex = 0
                Exit Sub
            End If
            ldBookings()
            txtSeq.Focus()
        End If
    End Sub
    Private Sub returnBooking()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select * from ClinicVisitBookingTb " & _
                                         "where bookingid=" & Val(numVchrNo.Tag))
        chgbyprg = True
        If dt.Rows.Count > 0 Then
            numVchrNo.Text = Val(dt(0)("bookingno") & "")
            dtpbookingfor.Value = DateValue(dt(0)("bookingFor"))
            dtptime.Value = dt(0)("bokkingtime")
            txtRec0.Tag = dt(0)("customerid")
            txtremarks.Text = Trim(dt(0)("comment") & "")
            cmbsalesman.Text = Trim(dt(0)("doctor") & "")
            cmbtype.Text = Trim(dt(0)("bookingtype") & "")
            txtadd1.Text = Trim(dt(0)("add1") & "")
            txtadd2.Text = Trim(dt(0)("add2") & "")
            txtadd3.Text = Trim(dt(0)("add3") & "")
            txtphone.Text = Trim(dt(0)("phone") & "")
            txtRec1.Text = Trim(dt(0)("patientName") & "")
            cmbreference.Text = Trim(dt(0)("referenceBy") & "")
        End If
        chgbyprg = False
        If cmbtype.Text = "OP" And Val(txtRec0.Tag) > 0 Then
            setCustomer(Val(txtRec0.Tag), True)
        End If

        txtRec0.Focus()
    End Sub

    Private Sub grdlist_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdlist.DoubleClick
        With grdlist
            If .RowCount = 0 Then Exit Sub
            numVchrNo.Tag = Val(.Item("bookingid", .CurrentRow.Index).Value)
            TabControl1.SelectedIndex = 0
            txtRec0.Focus()
            btnclear.Text = "Undo"
        End With
        returnBooking()
    End Sub
    Private Sub updateBooking()
        'If Val(txtRec0.Tag) = 0 Then
        '    MsgBox("Invalid OP Number", MsgBoxStyle.Exclamation)
        '    txtRec0.Focus()
        '    Exit Sub
        'End If
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("select * from ClinicVisitBookingTb where doctor='" & cmbsalesman.Text & _
                                         "' and bookingFor='" & Format(DateValue(dtpbookingfor.Value), "yyyy/MM/dd") & "' Order by bokkingtime Desc")
        If dt.Rows.Count > 0 Then
            Dim bookedtime As String
            Dim timetoupdate As String = Format(TimeValue(dtptime.Value), "HH:mm")
            Dim i As Integer
            For i = 0 To dt.Rows.Count - 1
                bookedtime = Format(TimeValue(dt(i)("bokkingtime")), "HH:mm")
                If timetoupdate = bookedtime And Val(numVchrNo.Tag) <> dt(i)("bookingid") Then
                    MsgBox("Time not avaible for the current date", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If
            Next
            
        End If
        Dim str As String
        Dim remark As String
        If txtremarks.Text <> "" Then
            If Mid(txtremarks.Text, Len(txtremarks.Text) - 1) = vbCrLf Then
                remark = Mid(txtremarks.Text, 1, Len(txtremarks.Text) - 1)
            End If
        End If
        Dim bookingtime As String = Format(dtptime.Value, "yyyy/MM/dd HH:mm")
        If Val(numVchrNo.Tag) = 0 Then
            str = "Insert into ClinicVisitBookingTb(bookingno,bookingdate,bookingFor,customerid,doctor,comment,bookingtype,bokkingtime,phone," & _
                    "add1,add2,add3,patientName,referenceBy) values(" & _
                    Val(numVchrNo.Text) & ",'" & Format(DateValue(Date.Now), "yyyy/MM/dd") & "','" & Format(DateValue(dtpbookingfor.Value), "yyyy/MM/dd") & "'," & _
                    Val(txtRec0.Tag) & ",'" & cmbsalesman.Text & "','" & txtremarks.Text & "','" & cmbtype.Text & "','" & _
                    bookingtime & "','" & txtphone.Text & "','" & _
                    txtadd1.Text & "','" & txtadd2.Text & "','" & txtadd3.Text & "','" & txtRec1.Text & "','" & cmbreference.Text & "')"
        Else
            str = "Update ClinicVisitBookingTb set bookingdate='" & Format(DateValue(Date.Now), "yyyy/MM/dd") & "'," & _
                                                   "bookingFor='" & Format(DateValue(dtpbookingfor.Value), "yyyy/MM/dd") & "'," & _
                                                   "customerid=" & Val(txtRec0.Tag) & "," & _
                                                   "comment='" & MkDbSrchStr(txtremarks.Text) & "'," & _
                                                   "doctor='" & cmbsalesman.Text & "', " & _
                                                   "phone='" & txtphone.Text & "', " & _
                                                   "add1='" & MkDbSrchStr(txtadd1.Text) & "', " & _
                                                   "add2='" & MkDbSrchStr(txtadd2.Text) & "', " & _
                                                   "add3='" & MkDbSrchStr(txtadd3.Text) & "', " & _
                                                   "bokkingtime='" & bookingtime & "', " & _
                                                   "bookingtype='" & cmbtype.Text & "', " & _
                                                   "patientName='" & MkDbSrchStr(txtRec1.Text) & "'," & _
                                                   "referenceBy='" & cmbreference.Text & "' " & _
                                                   "where bookingid=" & Val(numVchrNo.Tag)


        End If
        _objcmnbLayer._saveDatawithOutParm(str)
        MsgBox("Updated", MsgBoxStyle.Information)
        clear()
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        updateBooking()
    End Sub

    Private Sub cldrdate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Return Then
            dtpbookingfor.Focus()
        End If
    End Sub

    Private Sub dtpbookingfor_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtpbookingfor.KeyDown
        If e.KeyCode = Keys.Return Then
            dtptime.Focus()
        End If
    End Sub

    Private Sub txtRec1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRec1.KeyDown
        If e.KeyCode = Keys.Return Then
            If cmbtype.Text = "OP" Then
                txtremarks.Focus()
            Else
                SendKeys.Send("{TAB}")
            End If
        End If
    End Sub

    Private Sub txtremarks_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtremarks.KeyDown
        If e.KeyCode = Keys.Return Then
            If txtremarks.Text <> "" Then
                If Mid(txtremarks.Text, Len(txtremarks.Text) - 1) = vbCrLf Then
                    cmbsalesman.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub cmbsalesman_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbsalesman.KeyDown
        If e.KeyCode = Keys.Return Then
            dtpbookingfor.Focus()
        End If
    End Sub

    Private Sub dtptime_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtptime.KeyDown
        If e.KeyCode = Keys.Return Then
            cmbtype.Focus()
        End If
    End Sub

    Private Sub cmbtype_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbtype.KeyDown
        If e.KeyCode = Keys.Return Then
            txtRec0.Focus()
        End If
    End Sub
    Private Sub cmbtype_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbtype.SelectedIndexChanged
        If cmbtype.Text = "OP" Then
            txtRec1.ReadOnly = True
            txtadd1.ReadOnly = True
            txtadd2.ReadOnly = True
            txtadd3.ReadOnly = True
            txtphone.ReadOnly = True
            txtRec0.ReadOnly = False
            txtRec0.Focus()
        Else
            txtRec1.ReadOnly = False
            txtadd1.ReadOnly = False
            txtadd2.ReadOnly = False
            txtadd3.ReadOnly = False
            txtphone.ReadOnly = False
            txtRec0.ReadOnly = True
            txtRec1.Focus()
        End If
    End Sub


    Private Sub cmbsalesman_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbsalesman.SelectedIndexChanged
        getlasttime()
    End Sub
    Private Sub getlasttime()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("select top 1 * from ClinicVisitBookingTb where doctor='" & cmbsalesman.Text & _
                                         "' and bookingFor='" & Format(DateValue(dtpbookingfor.Value), "yyyy/MM/dd") & "' Order by bokkingtime Desc")
        If dt.Rows.Count > 0 Then
            lbltime.Text = "Last OP : " & Format(TimeValue(dt(0)("bokkingtime")), "hh:mm:ss tt")
        Else
            lbltime.Text = "Last OP : "
        End If
    End Sub
    Private Sub loadrefernce()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("select * from RefernceDoctorTb ")
        Dim i As Integer
        cmbreference.Items.Clear()
        cmbreference.Items.Add("")
        For i = 0 To dt.Rows.Count - 1
            cmbreference.Items.Add(dt(i)("refDoctorname"))
        Next
    End Sub

    Private Sub btnaddref_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddref.Click
        Dim freference As New ReferedByDoctorsFrm
        freference.isreturnname = True
        freference.ShowDialog()
        loadrefernce()
        cmbreference.Text = freference.txtname.Text
        freference = Nothing
    End Sub
    Private Sub txtRec0_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRec0.Validated
        setCustomer()
    End Sub

    Private Sub txtadd1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtadd1.KeyDown, txtadd2.KeyDown, txtadd3.KeyDown, txtphone.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub dtpbookingfor_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpbookingfor.Validated
        getlasttime()
    End Sub

    Private Sub dtpbookingfor_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpbookingfor.ValueChanged
        dtptime.Value = dtpbookingfor.Value
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        If Val(btndelete.Tag) = 0 Then
            MsgBox("This user do not have rights to delete", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If MsgBox("Do you want to remove Booking?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        _objcmnbLayer._saveDatawithOutParm("DELETE FROM ClinicVisitBookingTb WHERE bookingid=" & Val(numVchrNo.Tag))
        clear()
    End Sub

    Private Sub txtRec0_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRec0.TextChanged

    End Sub

    Private Sub txtRec1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRec1.TextChanged

    End Sub
End Class