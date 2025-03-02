Public Class MembershipAttendanceFrm
    Private chgbyprg As Boolean
    Private chgnumbyprg As Boolean
    Private _srchTxtId As Integer
    Private _srchOnce As Boolean
    Private WithEvents fMList As Mlistfrm
    Private MyActiveControl As New Object
    Private lstKey As Integer
    Private invdetId As Long
    Private units As Integer
    Private attendanceid As Long
    Private cntattendance As Integer
    Private jobid As Long
    Private _vtable As DataTable
    Private _objcmnbLayer As New clsCommon_BL
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        If MsgBox("Do you want to close?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        Me.Close()
    End Sub

    Private Sub txtcustomer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtcustomer.KeyDown
        lstKey = e.KeyCode
        Dim MyCtrl As TextBox = sender
        If e.KeyCode = Keys.Return Then
            dtpcheckin.Focus()
            If Not fMList Is Nothing Then fMList.Visible = False
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
        End If
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
            Dim x As Integer
            Dim y As Integer
            If _srchTxtId = 4 Then
                x = Me.Left + 480
                y = Me.Top + 465
            ElseIf _srchTxtId = 2 Then
                x = Me.Left + 100
                y = Me.Top + 300
            Else
                x = Me.Left + 480
                y = Me.Top + 320
            End If

            PopupLoc = New Point(x, y)
            If fMList.Visible = False Then
                Select Case _srchTxtId
                    Case 1
                        SetFmlist(fMList, 37)
                End Select
                If _srchTxtId = 4 Then
                    fMList.Height = fMList.Height - 50
                End If
                fMList.loc = PopupLoc
                fMList.Show(fMainForm)
                fMList.Visible = True
            End If
        End If
        Select Case _srchTxtId
            Case 1   'Customer name
                fMList.SearchIndex = 3
                fMList.SearchIndexDescr = 4
                fMList_doFocus()
                fMList.Search(txtcustomer.Text)
                fMList.returnid(txtcustomer)
                'fMList.AssignList(txtcustomer, lstKey, chgbyprg)
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
            Case 1
                txtcustomer.Text = ItmFlds(0)
                txtcustomer.Tag = ItmFlds(4)
        End Select
        chgbyprg = False
    End Sub

    Private Sub MembershipAttendanceFrm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtcustomer.Focus()
    End Sub

    Private Sub txtcustomer_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcustomer.Validated
        getpackages()
    End Sub
    Private Sub getpackages()
        Dim dt As DataTable
        Dim qry As String
        qry = "select * from (select accdescr, description,AccId,units,invdetid,isnull(cntattendance,0)cntattendance " & _
        "from jobtb left join accmast on jobtb.custcode=accmast.accid " & _
        "left join accmastaddr on accmast.accid =accmastaddr.accountno   " & _
        "inner join (select trdate,TrRefNo,[Job Code] invjobcode,[Item Code],InvItm.Description ,WarrentyExpDate enddate, " & _
        "UnitCost+isnull(taxamt,0)price,units,id invdetid from ItmInvTrTb " & _
        "inner join ItmInvCmnTb on ItmInvCmnTb.TrId=ItmInvTrTb.TrId " & _
        "left join InvItm on invitm.ItemId=ItmInvTrTb.ItemId) tr on JobTb.jobcode=tr.invjobcode " & _
        "left join (select count(attendanceid)cntattendance,invdetId attendanceid from " & _
        "AttendanceTb group by invdetId) attendance on tr.invdetid=attendance.attendanceid " & _
        "where  enddate>=convert(date,getdate())  and AccId=" & Val(txtcustomer.Tag) & ")tr where isnull(units,0)-isnull(cntattendance,0)>0"
        dt = _objcmnbLayer._fldDatatable(qry)
        Dim i As Integer
        If dt.Rows.Count > 0 Then
            chgbyprg = True
            txtcustomer.Text = dt(0)("accdescr")
            chgbyprg = False
        End If
        For i = 0 To dt.Rows.Count - 1
            cmbpackage.Items.Add(dt(i)("description"))
        Next
        If cmbpackage.Items.Count > 0 Then cmbpackage.SelectedIndex = 0
    End Sub
    Private Sub getpackagedetails()
        Dim dt As DataTable
        Dim qry As String
        qry = "select * from (select  description,AccId,units,invdetid,isnull(cntattendance,0)cntattendance,jobid,WarrentyExpDate " & _
        "from jobtb left join accmast on jobtb.custcode=accmast.accid " & _
        "left join accmastaddr on accmast.accid =accmastaddr.accountno   " & _
        "inner join (select trdate,TrRefNo,[Job Code] invjobcode,[Item Code],InvItm.Description ,WarrentyExpDate enddate, " & _
        "UnitCost+isnull(taxamt,0)price,units,id invdetid,WarrentyExpDate from ItmInvTrTb " & _
        "inner join ItmInvCmnTb on ItmInvCmnTb.TrId=ItmInvTrTb.TrId " & _
        "left join InvItm on invitm.ItemId=ItmInvTrTb.ItemId) tr on JobTb.jobcode=tr.invjobcode " & _
        "left join (select count(attendanceid)cntattendance,invdetId attendanceid from " & _
        "AttendanceTb group by invdetId) attendance on tr.invdetid=attendance.attendanceid " & _
        "where  enddate>=convert(date,getdate())  and AccId=" & Val(txtcustomer.Tag) & ")tr where isnull(description,'')='" & cmbpackage.Text & "'"
        dt = _objcmnbLayer._fldDatatable(qry)
        If dt.Rows.Count > 0 Then
            invdetId = dt(0)("invdetid")
            cntattendance = dt(0)("cntattendance")
            units = dt(0)("units")
            jobid = dt(0)("jobid")
            lblattendance.Text = cntattendance
            lblclasses.Text = units
            If Not IsDBNull(dt(0)("WarrentyExpDate")) Then
                lblexpirydate.Text = dt(0)("WarrentyExpDate")
            Else
                lblexpirydate.Text = ""
            End If

        End If
        
    End Sub

    Private Sub cmbpackage_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbpackage.SelectedIndexChanged
        getpackagedetails()
    End Sub

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        'If DateValue(cldrdate.Value) <> DateValue(Date.Now) Then
        '    MsgBox("Inavalid Date" & vbCrLf & "Change to Current date")
        '    cldrdate.Focus()
        '    Exit Sub
        'End If
        If Val(txtcustomer.Tag) = 0 Then
            MsgBox("Invalid Customer", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If cmbpackage.Text = "" Then
            MsgBox("Invalid Package", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("select attendanceid from AttendanceTb WHERE customerid=" & Val(txtcustomer.Tag) & _
                                         " and invdetId=" & invdetId & " AND attendanceid<>" & attendanceid & " and attdate='" & Format(DateValue(cldrdate.Value), "yyyy/MM/dd") & "'")
        If dt.Rows.Count > 0 Then
            MsgBox("Attendance already added", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If lblexpirydate.Text <> "" Then
            If DateValue(lblexpirydate.Text) < DateValue(cldrdate.Value) Then
                MsgBox("Package already expired", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
        End If
        Dim qry As String
        If attendanceid = 0 Then
            qry = "insert into AttendanceTb (customerid,invdetId,attdate,checkintime,checkouttime,remarks,noofpersons,jobid) values " & _
                    "(" & Val(txtcustomer.Tag) & "," & invdetId & ",'" & Format(DateValue(cldrdate.Value), "yyyy/MM/dd") & "','" & _
                    Format(dtpcheckin.Value, "yyyy/MM/dd hh:mm:ss tt") & "','" & Format(dtpcheckout.Value, "yyyy/MM/dd hh:mm:ss tt") & "','" & txtremarks.Text & "'," & Val(txtnos.Text) & "," & jobid & ")"
        Else
            qry = "Update AttendanceTb set customerid=" & Val(txtcustomer.Tag) & "," & _
                "invdetId=" & invdetId & "," & _
                "attdate='" & Format(DateValue(cldrdate.Value), "yyyy/MM/dd") & "'," & _
                "checkintime='" & Format(dtpcheckin.Value, "yyyy/MM/dd hh:mm:ss tt") & "'," & _
                "checkouttime='" & Format(dtpcheckout.Value, "yyyy/MM/dd hh:mm:ss tt") & "'," & _
                "remarks='" & MkDbSrchStr(txtremarks.Text) & "'," & _
                "jobid=" & jobid & "," & _
                "noofpersons =" & Val(txtnos.Text) & " where attendanceid=" & attendanceid
        End If

        _objcmnbLayer._saveDatawithOutParm(qry)
        MsgBox("Attendance Updated", MsgBoxStyle.Information)
        cleartext()
        Timer1.Enabled = True
    End Sub
    Private Sub loadAttendance()
        Dim qry As String
        qry = "select row_number() over(order by attendanceid asc) as Slno,AccDescr,jobcode,Description,attdate,checkintime,checkouttime,AttendanceTb.remarks," & _
        "noofpersons,isnull(cntattendance,0)cntattendance,WarrentyExpDate,attendanceid from AttendanceTb " & _
        "left join AccMast on AccMast.AccId=AttendanceTb.customerid " & _
        "left join ItmInvTrTb on ItmInvTrTb.id=AttendanceTb.invdetId " & _
        "left join InvItm on ItmInvTrTb.ItemId=InvItm.ItemId " & _
        "left join jobtb on AttendanceTb.jobid=jobtb.jobid " & _
        "left join (select count(attendanceid)cntattendance,invdetId from " & _
        "AttendanceTb group by invdetId) attendance on ItmInvTrTb.id=attendance.invdetId " & _
        "where attdate='" & Format(DateValue(cldrdate.Value), "yyyy/MM/dd") & "'"
        _vtable = _objcmnbLayer._fldDatatable(qry)
        grdattendancelist.DataSource = _vtable
        With grdattendancelist
            If .ColumnCount = 0 Then Exit Sub
            SetGridProperty(grdattendancelist)
            .Columns("Slno").Width = 70
            .Columns("AccDescr").HeaderText = "Customer Name"
            .Columns("AccDescr").Width = 200
            .Columns("AccDescr").DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
            .Columns("jobcode").HeaderText = "Admission No"
            .Columns("jobcode").Width = 150
            .Columns("Description").HeaderText = "Package Name"
            .Columns("Description").Width = 200
            .Columns("checkintime").HeaderText = "Check IN"
            .Columns("checkintime").Width = 150
            .Columns("checkouttime").HeaderText = "Check Out"
            .Columns("checkouttime").Width = 150
            .Columns("remarks").HeaderText = "Remarks"
            .Columns("noofpersons").HeaderText = "Persons"
            .Columns("attdate").HeaderText = "Date"
            .Columns("attdate").Width = 100

            .Columns("cntattendance").HeaderText = "Attendance"
            .Columns("cntattendance").Width = 100

            .Columns("WarrentyExpDate").HeaderText = "Expiry Date"
            .Columns("WarrentyExpDate").Width = 100

            .Columns("attendanceid").Visible = False

            cmbOrder.Items.Clear()
            Dim i As Integer = 0
            For i = 0 To .ColumnCount - 2
                cmbOrder.Items.Add(.Columns(i).HeaderText)
            Next
            If cmbOrder.Items.Count > 0 Then cmbOrder.SelectedIndex = 0
        End With
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        loadAttendance()
        resizeGridColumn(grdattendancelist, 6)
    End Sub

    Private Sub MembershipAttendanceFrm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Timer1.Enabled = True
        If userType Then
            If getRight(265, CurrentUser) Then
                cldrdate.Enabled = False
            Else
                cldrdate.Enabled = True
            End If
        End If
        
    End Sub

    Private Sub dtpcheckin_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtpcheckin.KeyDown, dtpcheckout.KeyDown, _
    txtremarks.KeyDown, txtnos.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub btnremove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnremove.Click
        If MsgBox("Do you want to remove Attendance?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        Dim aid As Long
        If grdattendancelist.RowCount = 0 Then Exit Sub
        If grdattendancelist.CurrentRow Is Nothing Then Exit Sub
        aid = grdattendancelist.Item("attendanceid", grdattendancelist.CurrentRow.Index).Value
        _objcmnbLayer._saveDatawithOutParm("Delete from AttendanceTb where attendanceid=" & aid)
        loadAttendance()
        cleartext()
    End Sub
    Private Sub cleartext()
        chgbyprg = True
        txtcustomer.Text = ""
        txtcustomer.Tag = ""
        cmbpackage.Items.Clear()
        txtremarks.Text = ""
        txtnos.Text = 0
        attendanceid = 0
        units = 0
        invdetId = 0
        cntattendance = 0
        dtpcheckin.Value = Date.Now
        dtpcheckout.Value = Date.Now
        lblattendance.Text = 0
        lblclasses.Text = 0
        chgbyprg = False
    End Sub
    Private Sub returnAttendance(ByVal attid As Long)
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("select AttendanceTb.*,Description,units,cntattendance,Accdescr from AttendanceTb " & _
                                         "left join AccMast on AccMast.accid=AttendanceTb.customerid " & _
                                         "left join (select  InvItm.Description,units,id invdetid from ItmInvTrTb " & _
                                         "inner join ItmInvCmnTb on ItmInvCmnTb.TrId=ItmInvTrTb.TrId " & _
                                         "left join InvItm on invitm.ItemId=ItmInvTrTb.ItemId)tr on AttendanceTb.invdetId=tr.invdetid " & _
                                         "left join (select count(attendanceid)cntattendance,invdetId from " & _
                                         "AttendanceTb group by invdetId) attendance on tr.invdetid=attendance.invdetId " & _
                                         "where attendanceid=" & attid)
        If dt.Rows.Count > 0 Then
            chgbyprg = True
            txtcustomer.Text = Trim(dt(0)("Accdescr") & "")
            txtcustomer.Tag = Val(dt(0)("customerid") & "")
            getpackages()
            cmbpackage.Text = Trim(dt(0)("Description") & "")
            dtpcheckin.Value = dt(0)("checkintime")
            dtpcheckout.Value = dt(0)("checkouttime")
            txtremarks.Text = Trim(dt(0)("remarks") & "")
            txtnos.Text = Val(dt(0)("noofpersons") & "")
            units = Val(dt(0)("units") & "")
            cntattendance = Val(dt(0)("cntattendance") & "")
            invdetId = Val(dt(0)("invdetId") & "")
            attendanceid = Val(dt(0)("attendanceid") & "")
            jobid = Val(dt(0)("jobid") & "")
            chgbyprg = False
        End If
    End Sub
    Private Sub grdattendancelist_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdattendancelist.DoubleClick
        If grdattendancelist.RowCount = 0 Then Exit Sub
        If grdattendancelist.CurrentRow Is Nothing Then Exit Sub
        returnAttendance(Val(grdattendancelist.Item("attendanceid", grdattendancelist.CurrentRow.Index).Value))
    End Sub

    Private Sub txtcustomer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcustomer.TextChanged
        If chgbyprg = True Then Exit Sub
        Dim myCtrl As Control = sender
        Select Case myCtrl.Name
            Case "txtcustomer"
                _srchTxtId = 1
        End Select
        txtcustomer.Tag = ""
        _srchOnce = False
        ShowFmlist(sender)
    End Sub

    Private Sub txtSeq_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeq.TextChanged
        grdattendancelist.DataSource = SearchGrid(_vtable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
    End Sub

    Private Sub cldrdate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cldrdate.ValueChanged
        dtpcheckin.Value = cldrdate.Value
        dtpcheckout.Value = cldrdate.Value

    End Sub

    Private Sub btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnload.Click
        loadAttendance()
    End Sub

    Private Sub txthour_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txthour.KeyPress
        NumericTextOnKeypress(sender, e, chgbyprg, "0")
    End Sub

    Private Sub txthour_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txthour.TextChanged
        If chgnumbyprg Then Exit Sub
        chgbyprg = True
        dtpcheckout.Value = DateAdd(DateInterval.Hour, Val(txthour.Text), dtpcheckin.Value)
        chgbyprg = False
    End Sub

    Private Sub dtpcheckout_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpcheckout.ValueChanged
        If chgbyprg Then Exit Sub
        Dim df As Double
        df = DateDiff(DateInterval.Hour, dtpcheckin.Value, dtpcheckout.Value)
        chgnumbyprg = True
        txthour.Text = df
        chgnumbyprg = False
    End Sub
End Class