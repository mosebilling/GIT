Public Class ClinicAppointmentFrm
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
    Private rpttable As DataTable
    Private WithEvents fselectBooking As SelectClinicBookingFrm
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fRptFormat As RptFormatfrm
    Public visittype As String
    Private max_cvn_visitnoteid As Long
    Private WithEvents fInvoice As SalesInvoice
    Private WithEvents fCrtAcc As PatientInfoFrm

    Private Sub txtRec0_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRec0.KeyDown, txtRec1.KeyDown
        lstKey = e.KeyCode
        Dim MyCtrl As TextBox = sender
        If e.KeyCode = Keys.F2 Then
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
            If MyCtrl.Name = "txtRec0" Then
                txtremarks.Focus()
            Else
                SendKeys.Send("{TAB}")
            End If

        End If
    End Sub
    Private Sub txtRec0_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRec0.TextChanged
        'If chgbyprg = True Then Exit Sub
        '_srchTxtId = 1
        '_srchOnce = False
        'ShowFmlist(sender)
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
            Dim x As Integer = Me.Width - fMList.Width - 100
            Dim y As Integer = Me.Height - fMList.Height - 100
            PopupLoc = New Point(x, y)
            If fMList.Visible = False Then
                Select Case _srchTxtId
                    Case 1, 2
                        SetFmlist(fMList, 34)
                End Select
                fMList.loc = PopupLoc
                fMList.Show(fMainForm)
                fMList.Visible = True
            End If
        End If
        Select Case _srchTxtId
            Case 1   'Customer Code
                fMList.SearchIndex = 1
                fMList.SearchIndexDescr = 0
                fMList_doFocus()
                fMList.Search(txtRec0.Text)
                txtRec1.Text = fMList.AssignList(txtRec0, lstKey, chgbyprg)
            Case 2   'Customer name
                fMList.SearchIndex = 0
                fMList.SearchIndexDescr = 1
                fMList_doFocus()
                fMList.Search(txtRec1.Text)
                txtRec0.Text = fMList.AssignList(txtRec1, lstKey, chgbyprg)
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
                txtRec0.Text = ItmFlds(1)
                txtRec1.Text = ItmFlds(0)
                txtRec0.Tag = ItmFlds(3)
        End Select
        chgbyprg = False
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
                                        "CurrencyCode,CountryCode,GSTIN,Remarks,GrpSetOn,gender,dateofbirth,isnull(customerage,0)customerage " & _
                                        " from AccMast LEFT JOIN AccMastAddr ON AccMast.accid=AccMastAddr.AccountNo " & _
                                        "LEFT JOIN S1AccHd ON AccMast.S1AccId=S1AccHd.S1AccId " & _
                                        condition)
        If dt.Rows.Count > 0 Then
            txtRec0.Tag = dt(0)("accid")
            chgbyprg = True
            txtRec0.Text = Trim("" & dt(0)("Alias"))
            txtRec1.Text = Trim("" & dt(0)("AccDescr"))
            txtcustAddress.Text = Trim(dt(0)("Address1") & "")
            If Trim(dt(0)("Address2") & "") <> "" Then
                txtcustAddress.Text = txtcustAddress.Text & vbCrLf & Trim(dt(0)("Address2") & "")
            End If
            If Trim(dt(0)("Address3") & "") <> "" Then
                txtcustAddress.Text = txtcustAddress.Text & vbCrLf & Trim(dt(0)("Address3") & "")
            End If
            If Trim(dt(0)("Address4") & "") <> "" Then
                txtcustAddress.Text = txtcustAddress.Text & vbCrLf & Trim(dt(0)("Address4") & "")
            End If
            If Trim(dt(0)("Phone") & "") <> "" Then
                txtcustAddress.Text = txtcustAddress.Text & vbCrLf & "Ph: " & Trim(dt(0)("Phone") & "")
            End If
            lblgender.Text = IIf(Val(dt(0)("gender") & "") = 0, "Male", "Female")
            If Not IsDBNull(dt(0)("dateofbirth")) Then
                If DateDiff(DateInterval.Year, DateValue(dt(0)("dateofbirth")), DateValue(Date.Now)) > 0 And Val(dt(0)("customerage") & "") = 0 Then
                    lbldob.Text = DateValue(dt(0)("dateofbirth")) & " / Age : " & DateDiff(DateInterval.Year, DateValue(dt(0)("dateofbirth")), DateValue(Date.Now))
                Else
                    lbldob.Text = "Age : " & Val(dt(0)("customerage") & "")
                End If

            End If

            If Not skiploadHistory And visittype = "OP" Then ldVisitHistory()
        Else
els:
            txtRec0.Text = ""
            txtRec0.Tag = ""
            txtRec1.Text = ""
            txtcustAddress.Text = ""
        End If
        chgbyprg = False
    End Sub

    Private Sub txtRec0_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRec0.Validated
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
        If txtRec0.Text = "" Then Exit Sub
        setCustomer()
    End Sub
    Private Sub NextNumber()
        Dim vrPrefix As String = ""
        Dim vrVoucherNo As Long
        Dim vrAccountNo1 As Long
        Dim vrAccountNo2 As Long
        If visittype = "IP" Then
            getVrsDet(0, "IPA", vrPrefix, vrVoucherNo, vrAccountNo1, vrAccountNo2)
        Else
            getVrsDet(0, "APP", vrPrefix, vrVoucherNo, vrAccountNo1, vrAccountNo2)
        End If

        If Not isModi Then
            numVchrNo.Text = vrVoucherNo
        End If
    End Sub

    Private Sub ClinicAppointmentFrm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtRec0.Focus()
    End Sub

    Private Sub ClinicAppointmentFrm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        NextNumber()
        ldDoctor()

        loadrefernce()
        Dim ObjLocationlist As New List(Of String)
        ObjLocationlist = toLoadAutoFillListItems("Alias", "AccMast left join S1AccHd on S1AccHd.S1AccId=AccMast.S1AccId where GrpSetOn='customer'")
        Call toAssignDownListToText(txtRec0, ObjLocationlist) '
        If Not userType Then
            btnupdate.Tag = 1
            btndelete.Tag = 1
        Else
            btnupdate.Tag = IIf(getRight(239, CurrentUser), 1, 0)
            btndelete.Tag = IIf(getRight(240, CurrentUser), 1, 0)
        End If
        Timer1.Enabled = True
        If visittype = "OP" Then
            Label1.Text = "OP Appointment"
            Label6.Text = "Previous Visits"
            ldVisitHistory()
            Panel1.BackColor = Color.LightGreen
        Else
            Label1.Text = "IP Appointment"
            Label6.Text = "Appoinment History"
            Panel1.BackColor = Color.LightPink

        End If
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
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

    Private Sub txtRec1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRec1.TextChanged

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

    Private Sub txtremarks_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtremarks.TextChanged

    End Sub

    Private Sub cmbsalesman_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbsalesman.KeyDown
        If e.KeyCode = Keys.Return Then
            btnupdate.Focus()
        End If
    End Sub
    Private Sub ldVisitHistory()
        Dim dt As DataTable
        'dt = _objcmnbLayer._fldDatatable("SELECT visitno [No],visitdate [Date],Doctor," & _
        '                                 "Comment,Observation,Treatement,visitid from ClinicVistTb where " & _
        '                                 "customerid=" & Val(txtRec0.Tag) & " AND visitid<>" & Val(numVchrNo.Tag) & " ORDER BY visitdate Desc")
        dt = _objcmnbLayer._fldDatatable("SELECT visittype [Type], visitno [No],visitdate [Date],cvn_doctor Doctor," & _
                                         "cvn_notecomment Comment,cvn_visitnoteobservation Observation,cvn_treatmentnote Treatement,visitid,isnull(max_cvn_visitnoteid,0)visitnoteid from ClinicVistTb " & _
                                         "LEFT JOIN (Select max(cvn_visitnoteid) max_cvn_visitnoteid,cvn_visitid from ClinicVisitNoteTb group by cvn_visitid)lastvisitnote ON " & _
                                         "ClinicVistTb.visitid=lastvisitnote.cvn_visitid " & _
                                         "LEFT JOIN ClinicVisitNoteTb ON lastvisitnote.max_cvn_visitnoteid=ClinicVisitNoteTb.cvn_visitnoteid " & _
                                         "where customerid=" & Val(txtRec0.Tag) & " ORDER BY visitdate Desc")
        grdVoucher.DataSource = dt
        SetGridHead()
        resizeGridColumn(grdVoucher, 5)
        Label6.Text = "Previous Visits"
        Dim i As Integer
        For i = 0 To grdVoucher.RowCount - 1
            With grdVoucher
                If Trim(.Item("Type", i).Value & "") = "IP" Then
                    .Rows.Item(i).DefaultCellStyle.BackColor = Color.LightGray
                End If
            End With
        Next

    End Sub
    Private Sub ldVisits()
        dttable = _objcmnbLayer._fldDatatable(stringForReport())
        grdlist.DataSource = dttable
        SetGridHeadList()
        resizeGridColumn(grdlist, 3)
    End Sub
    Public Function stringForReport()
        Dim str As String = "SELECT visitno [No],visitdate [Date],Alias [OP Number],AccDescr [Patient Name],Address1+Address2 Address,Phone,Doctor,referencedBy Reference," & _
                                         "Comment,Observation,Treatement,visitid,'" & _
                                          Format(DateValue(dtpfrom.Value), "dd/MM/yyyy") & "' datfrom,'" & Format(DateValue(dtpto.Value), "dd/MM/yyyy") & "' dateto,1 lnk " & _
                                         "from ClinicVistTb " & _
                                         "Left join Accmast on ClinicVistTb.customerid=Accmast.accid " & _
                                         "Left join AccMastAddr on ClinicVistTb.customerid=AccMastAddr.accountno " & _
                                         "where " & _
                                         "visitdate>='" & Format(DateValue(dtpfrom.Value), "yyyy/MM/dd") & "' AND visitdate<='" & _
                                         Format(DateValue(dtpto.Value), "yyyy/MM/dd") & "' and visittype='" & visittype & "' ORDER BY visitdate Desc"
        Return str
    End Function
    Sub SetGridHead()
        With grdVoucher
            SetGridProperty(grdVoucher)
            .Columns("No").Width = 50
            .Columns("Date").Width = 100
            .Columns("Doctor").Width = 100
            .Columns("Comment").Width = 150
            .Columns("Observation").Width = 150
            .Columns("visitid").Visible = False
            .Columns("visitnoteid").Visible = False
        End With
    End Sub
    Sub SetGridHeadList()
        With grdlist
            SetGridProperty(grdlist)
            .Columns("No").Width = 50
            .Columns("Date").Width = 100
            .Columns("OP Number").Width = 100
            .Columns("Address").Width = 100
            .Columns("Doctor").Width = 100
            .Columns("Reference").Width = 100
            .Columns("Comment").Width = 150
            .Columns("Observation").Width = 150
            .Columns("visitid").Visible = False
            .Columns("datfrom").Visible = False
            .Columns("dateto").Visible = False
            .Columns("lnk").Visible = False
        End With
        setComboGrid()
    End Sub
    Private Sub ldDoctor()
        Dim _slsManTable As DataTable = _objcmnbLayer._fldDatatable("SELECT SManCode FROM SalesmanTb WHERE isnull(isdoctor,0)=1")
        Dim i As Integer
        'cmbsalesman.Items.Add("")
        For i = 0 To _slsManTable.Rows.Count - 1
            cmbsalesman.Items.Add(_slsManTable(i)("SManCode"))
        Next
        If cmbsalesman.Items.Count > 0 Then cmbsalesman.SelectedIndex = 0
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        If Val(btndelete.Tag) = 0 Then
            MsgBox("This user do not have rights to update", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If txtRec0.Text = "" Then
            MsgBox("Invalid Patient", MsgBoxStyle.Exclamation)
            txtRec0.Focus()
            Exit Sub
        End If
chkagain:
        If Val(numVchrNo.Tag) = 0 Then
            If Not CheckNoExists("", Val(numVchrNo.Text), visittype, "ClinicVisit") Then
                If MsgBox("Document No alreary exist! Do you want to fill next?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Exit Sub
                Else
                    numVchrNo.Text = Val(numVchrNo.Text) + 1
                    GoTo chkagain
                End If
            End If
        End If
        Dim id As Long
        With _objclinic
            .visitid = Val(numVchrNo.Tag)
            .visitdate = cldrdate.Value
            .doctor = cmbsalesman.Text
            .comment = Trim(txtremarks.Text & "")
            .observation = ""
            .treatement = ""
            .customerid = Val(txtRec0.Tag)
            .visitno = numVchrNo.Text
            .crtby = CurrentUser
            .referencedBy = cmbreference.Text
            .bookingid = Val(txtbooking.Tag)
            .visittype = visittype
            id = Val(.saveClinicVisitTb())

        End With
        If Val(numVchrNo.Tag & "") = 0 Then
            Dim t As String
            If visittype = "IP" Then
                t = "IPA"
            Else
                t = "APP"
            End If
            SetNextVrNo(numVchrNo, 0, "APP", "visitno = ", False, False, True, 0, "")
            numVchrNo.Tag = id
        End If
        returnClinicVisit()
        'makeClear()
        MsgBox("Updated", MsgBoxStyle.Information)
        btnclear.Text = "New"
    End Sub
    Public Sub returnClinicVisit()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select ClinicVistTb.*,bookingno,isnull(max_cvn_visitnoteid,0)max_cvn_visitnoteid,TrId,ItmInvCmnTb.PreFix,ItmInvCmnTb.InvNo from ClinicVistTb " & _
                                         "left join ClinicVisitBookingTb on ClinicVisitBookingTb.bookingid=ClinicVistTb.bookingid " & _
                                         "LEFT JOIN (Select max(cvn_visitnoteid) max_cvn_visitnoteid,cvn_visitid from ClinicVisitNoteTb group by cvn_visitid)lastvisitnote ON " & _
                                         "ClinicVistTb.visitid=lastvisitnote.cvn_visitid " & _
                                         "left join ItmInvCmnTb  on ItmInvCmnTb.clinicvisitid=ClinicVistTb.visitid " & _
                                         "where visitid=" & Val(numVchrNo.Tag))
        chgbyprg = True
        If dt.Rows.Count > 0 Then
            txttoken.Text = Val(dt(0)("tockenno") & "")
            numVchrNo.Text = Val(dt(0)("visitno") & "")
            cldrdate.Value = DateValue(dt(0)("visitdate"))
            txtRec0.Tag = dt(0)("customerid")
            txtremarks.Text = Trim(dt(0)("comment") & "")
            txtbooking.Text = Val(dt(0)("bookingno") & "")
            txtbooking.Tag = Val(dt(0)("bookingid") & "")
            cmbsalesman.Text = Trim(dt(0)("doctor") & "")
            cmbreference.Text = Trim(dt(0)("referencedBy") & "")
            max_cvn_visitnoteid = Val(dt(0)("max_cvn_visitnoteid") & "")
            If Val(dt(0)("TrId") & "") > 0 Then
                lblinvoice.Text = "Invoiced : " & Trim(dt(0)("PreFix") & "") & "/" & Trim(dt(0)("InvNo") & "")
                lblinvoice.Tag = Val(dt(0)("TrId") & "")
                lblinvoice.Visible = True
                lblinvoice.ForeColor = Color.Green
                lblinvoice.Cursor = Cursors.Hand
            Else
                lblinvoice.Text = "Invoice Not Found"
                lblinvoice.Tag = ""
                lblinvoice.Visible = True
                lblinvoice.ForeColor = Color.Red
                lblinvoice.Cursor = Cursors.Arrow
            End If
        End If
        If visittype = "IP" Then
            ldVisitNoteHistory(Val(numVchrNo.Tag))
        Else
            ldVisitHistory()
        End If
        chgbyprg = False
        setCustomer(Val(txtRec0.Tag), True)
        txtRec0.Focus()
        btninvoice.Visible = True
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        makeClear()
    End Sub
    Private Sub makeClear()
        chgbyprg = True
        cldrdate.Value = DateValue(Now.Date)
        txtRec0.Text = ""
        txtRec0.Tag = ""
        txtRec1.Text = ""
        txtcustAddress.Text = ""
        numVchrNo.Tag = ""
        txtremarks.Text = ""
        cmbsalesman.Text = ""
        btnclear.Text = "Clear"
        txtbooking.Text = ""
        txtbooking.Tag = ""
        txttoken.Text = ""
        cmbreference.Text = ""
        ldVisitHistory()
        NextNumber()
        txtRec0.Focus()
        max_cvn_visitnoteid = 0
        btninvoice.Visible = False
        chgbyprg = False
    End Sub

    Private Sub btnaddnote_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddnote.Click
        If Val(numVchrNo.Tag) > 0 Then
            AddVisitNote()
        Else
            MsgBox("Invalid Appointment Number", MsgBoxStyle.Exclamation)
        End If
    End Sub
    Private Sub AddVisitNote()
        Dim frm As New VisitNoteFrm
        With frm
            .lblvisitnumber.Text = "Visit Number: " & numVchrNo.Text
            .txtCashCustomer.Text = txtRec1.Text
            .txtCashCustomer.Tag = Val(txtRec0.Tag)
            .visitid = Val(numVchrNo.Tag)
            .cmbdoctor.Text = cmbsalesman.Text
            If visittype = "OP" Then
                .visitnoteid = max_cvn_visitnoteid
            End If
            .ShowDialog()
            If visittype = "IP" Then
                ldVisitNoteHistory(Val(numVchrNo.Tag))
            End If
        End With
    End Sub

    Private Sub grdVoucher_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.DoubleClick
        If Val(txtRec0.Tag) > 0 Then
            Dim frm As New VisitNoteFrm
            With frm
                .txtCashCustomer.Text = txtRec1.Text
                .txtCashCustomer.Tag = Val(txtRec0.Tag)
                .cmbdoctor.Text = cmbsalesman.Text
                .visitnoteid = Val(grdVoucher.Item("visitnoteid", grdVoucher.CurrentRow.Index).Value)
                If visittype = "IP" Then
                    .visitid = Val(numVchrNo.Tag)
                    .lblvisitnumber.Text = "Visit Number: " & numVchrNo.Text
                    .ShowDialog()
                    ldVisitNoteHistory(Val(numVchrNo.Tag))
                Else
                    If grdVoucher.Item("Type", grdVoucher.CurrentRow.Index).Value <> "IP" Then
                        .lblvisitnumber.Text = "Visit Number: " & Val(grdVoucher.Item("No", grdVoucher.CurrentRow.Index).Value)
                        .visitid = Val(grdVoucher.Item("visitid", grdVoucher.CurrentRow.Index).Value)
                        If Val(grdVoucher.Item("visitid", grdVoucher.CurrentRow.Index).Value) <> Val(numVchrNo.Tag) Then
                            .txtcomment.ReadOnly = True
                            .txtobservation.ReadOnly = True
                            .txtdoctornote.ReadOnly = True
                            .txtremark.ReadOnly = True
                        End If
                        .ShowDialog()
                        ldVisitHistory()
                    Else
                        fMainForm.loadIPAppointment(Val(grdVoucher.Item("visitid", grdVoucher.CurrentRow.Index).Value))
                    End If
                End If
                
            End With
        Else
            MsgBox("Invalid OP Number", MsgBoxStyle.Exclamation)
            txtRec0.Focus()
        End If

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        resizeGridColumn(grdVoucher, 5)
    End Sub

    Private Sub grdlist_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdlist.DoubleClick
        With grdlist
            If .RowCount = 0 Then Exit Sub
            numVchrNo.Tag = Val(.Item("visitid", .CurrentRow.Index).Value)
            TabControl1.SelectedIndex = 0
            txtRec0.Focus()
            btnclear.Text = "Undo"
        End With
        returnClinicVisit()
        'ldVisitHistory()
    End Sub

    Private Sub TabControl1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.Click
        If TabControl1.SelectedIndex = 1 Then
            If btnclear.Text = "Undo" Then
                MsgBox("Please Undo to clear selected record", MsgBoxStyle.Exclamation)
                TabControl1.SelectedIndex = 0
                Exit Sub
            End If
            ldVisits()
            txtSeq.Focus()
        End If
    End Sub

    Private Sub btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnload.Click
        ldVisits()
    End Sub

    Private Sub txtSeq_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeq.TextChanged
        grdlist.DataSource = SearchGrid(dttable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
        rpttable = SearchGrid(dttable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
    End Sub
    Private Sub setComboGrid()
        ChgByPrg = True
        cmbOrder.Items.Clear()
        Dim i As Integer = 0
        For i = 0 To grdlist.ColumnCount - 2
            cmbOrder.Items.Add(grdlist.Columns(i).HeaderText)
        Next
        If cmbOrder.Items.Count > 0 Then cmbOrder.SelectedIndex = 2
        ChgByPrg = False
    End Sub

    Private Sub fSelect_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fSelect.FormClosed
        fSelect = Nothing
    End Sub

    Private Sub btnSlct_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSlct.Click
        ldSelectBooking()
    End Sub
    Private Sub ldSelectBooking()
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
        If Not fSelect Is Nothing Then fSelect.Close() : fSelect = Nothing
        fselectBooking = New SelectClinicBookingFrm
        fselectBooking.Width = 900
        fselectBooking.currentdate = DateValue(cldrdate.Value)
        fselectBooking.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
        fselectBooking.ShowDialog()
    End Sub

    Private Sub fselectBooking_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fselectBooking.FormClosed
        fselectBooking = Nothing
    End Sub

    Private Sub fselectBooking_retrunBooking(ByVal bookingid As Long) Handles fselectBooking.retrunBooking
        txtbooking.Tag = bookingid
        returnBooking()
    End Sub
    Private Sub returnBooking()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select * from ClinicVisitBookingTb " & _
                                         "where bookingid=" & Val(txtbooking.Tag))
        chgbyprg = True
        If dt.Rows.Count > 0 Then
            txtbooking.Text = Val(dt(0)("bookingno") & "")
            cldrdate.Value = DateValue(dt(0)("bookingFor"))
            txtRec0.Tag = dt(0)("customerid")
            txtremarks.Text = Trim(dt(0)("comment") & "")
            cmbsalesman.Text = Trim(dt(0)("doctor") & "")
            cmbreference.Text = Trim(dt(0)("referenceBy") & "")
            If Val(txtRec0.Tag) = 0 Then
                If MsgBox("OP Number not found Do you want to create?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Dim frm As New QuickPatientCreateFrm
                    With frm
                        .txtRec1.Text = Trim(dt(0)("patientName") & "")
                        .txtadd1.Text = Trim(dt(0)("add1") & "")
                        .txtadd2.Text = Trim(dt(0)("add2") & "")
                        .txtadd3.Text = Trim(dt(0)("add3") & "")
                        .txtphone.Text = Trim(dt(0)("phone") & "")
                        .doctor = Trim(dt(0)("doctor") & "")
                        .ShowDialog()
                        txtRec0.Text = .txtRec0.Text
                    End With
                End If
            End If
        End If
        chgbyprg = False
        setCustomer(Val(txtRec0.Tag))
        txtRec0.Focus()
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


    Private Sub btnpreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpreview.Click
        Dim RptType As String = ""
        If TabControl1.SelectedIndex = 0 Then
            RptType = "CVSC"
        Else
            RptType = "CVSL"
        End If

        If chkFormat.Checked Then
            fRptFormat = New RptFormatfrm
            fRptFormat.RptType = RptType
            fRptFormat.ShowDialog()
            fRptFormat = Nothing
        Else
            Dim RptName As String
            RptName = getRptDefFlName(RptType, "")
            If RptName <> "" Then
                PrepareReport(RptName, "", False)
            End If
        End If
    End Sub
    Public Sub PrepareReport(ByVal FileName As String, ByVal RptCaption As String, ByVal forpint As Boolean)
        If Not frm Is Nothing Then
            frm.Close()
            frm = New ReprtviewNEWfrm
        Else
            frm = New ReprtviewNEWfrm
        End If
        Dim ds As New DataSet
        If TabControl1.SelectedIndex = 0 Then
            ds = _objclinic.returnVisitCard(dtpfrom.Value, dtpto.Value, Val(numVchrNo.Tag), 0)
        Else
            If rpttable Is Nothing Then
                ds = _objcmnbLayer._ldDataset(stringForReport(), False)
            Else
                ds.Tables.Add(rpttable)
            End If
            'ds = _objclinic.returnVisitCard(dtpfrom.Value, dtpto.Value, 0, 1)
        End If
        'ds = _objTr.returnStatementReport(dtpfrom.Value, dtpto.Value, accid, 6, 0, "customer")
        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = fMainForm
        frm.Text = IIf(RptCaption = "", "Reports", RptCaption)
        frm.Show()
    End Sub

    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        PrepareReport(RptFlName, RptCaption, False)
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        If Val(btndelete.Tag) = 0 Then
            MsgBox("This user do not have rights to delete", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If MsgBox("Do you want to remove Visit?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        _objcmnbLayer._saveDatawithOutParm("DELETE FROM ClinicVistTb WHERE visitid=" & Val(numVchrNo.Tag))
        makeClear()
        btnclear.Text = "New"
    End Sub

    Private Sub btnattachment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnattachment.Click

    End Sub
    Private Sub ldVisitNoteHistory(ByVal visitid As Long)
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT  cvn_notedate [Date],cvn_doctor Doctor," & _
                                         "cvn_notecomment Comment,cvn_visitnoteobservation Observation,cvn_treatmentnote Treatement,cvn_visitnoteid visitnoteid from" & _
                                         " ClinicVisitNoteTb where cvn_visitid=" & visitid & " ORDER BY cvn_notedate Desc")
        grdVoucher.DataSource = Nothing
        grdVoucher.DataSource = dt
        SetGridHeadVisitnote(False)

    End Sub
    Sub SetGridHeadVisitnote(ByVal ismedicine As Boolean)
        With grdVoucher
            SetGridProperty(grdVoucher)
            '.Columns("vnm_medicinename").Width = 200
            '.Columns("vnm_medicinename").HeaderText = "Medicine"
            '.Columns("vnm_qty").Width = 50
            '.Columns("vnm_qty").HeaderText = "QTY"
            '.Columns("vnm_qty").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            '.Columns("vnm_noofdays").Width = 50
            '.Columns("vnm_noofdays").HeaderText = "Days"
            '.Columns("vnm_noofdays").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '.Columns("vnm_use").Width = 150
            '.Columns("vnm_use").HeaderText = "Use of Medicine"
            'resizeGridColumn(grdVoucher, 3)
            .Columns("Date").Width = 100
            .Columns("Doctor").Width = 100
            .Columns("Comment").Width = 150
            .Columns("Observation").Width = 150
            .Columns("visitnoteid").Visible = False
            resizeGridColumn(grdVoucher, 4)
        End With
    End Sub

    Private Sub grdVoucher_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellContentClick

    End Sub

    Private Sub btninvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btninvoice.Click
        If Val(lblinvoice.Tag) > 0 Then
            If MsgBox("Invoice already fount! Do you want to create new?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        End If
        If (Not fInvoice Is Nothing) Then
            fInvoice = Nothing
        End If
        fInvoice = New SalesInvoice
        fInvoice.MdiParent = fMainForm
        fInvoice.Show()
        If chkmedicine.Checked Then
            fInvoice.loadfromClinic(Val(txtRec0.Tag), "", "", "", True, Val(numVchrNo.Tag))
        Else
            fInvoice.loadfromClinic(Val(txtRec0.Tag), "")
        End If

    End Sub

    Private Sub fInvoice_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fInvoice.FormClosed
        fInvoice = Nothing
    End Sub

    Private Sub lblinvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblinvoice.Click
        If Val(lblinvoice.Tag) = 0 Then Exit Sub
        fMainForm.LoadIS(Val(lblinvoice.Tag))
    End Sub

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        QuickCust(True, "Customer")
    End Sub
    Public Sub QuickCust(Optional ByRef bOnlyOne As Boolean = False, Optional ByVal Grp As String = "Customer")
        fCrtAcc = New PatientInfoFrm
        With fCrtAcc
            .Condition = "GrpSetOn In ('" & Grp & "')"
            .WindowState = FormWindowState.Normal
            '.Width = 420
            '.Height = 724
            .TabControl2.Visible = False
            FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
            .StartPosition = FormStartPosition.CenterScreen
            If Grp = "Customer" Then
                .iscust = True
            Else
                .iscust = False
            End If
            .bOnlyOne = bOnlyOne
            .ShowDialog()
            If Val(txtRec0.Tag) > 0 Then
                txtremarks.Focus()
            Else
                txtRec0.Focus()
            End If
        End With
    End Sub

    Private Sub fCrtAcc_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fCrtAcc.FormClosed
        fCrtAcc = Nothing
    End Sub

    Private Sub fCrtAcc_OpenAccMaster(ByRef AccountNo As Long) Handles fCrtAcc.OpenAccMaster
        setCustomer(AccountNo)

    End Sub
End Class