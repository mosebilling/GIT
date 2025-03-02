Public Class DoctorDeskFrm
    Private _objcmnbLayer As New clsCommon_BL
    Private dttable As DataTable
    Private Sub btnexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexit.Click
        If userType Then
            If getRight(244, CurrentUser) Then
                If MsgBox("Do you want to exit?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                End
            Else
                Me.Close()
            End If
        Else
            Me.Close()
        End If
    End Sub

    Private Sub DoctorDeskFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Timer1.Enabled = True
        LdCompDet()
        makeClear()
    End Sub
    Private Sub ldVisitHistory()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT visitno [No],visitdate [Date],Doctor," & _
                                         "Comment,Observation,Treatement,visitid from ClinicVistTb where " & _
                                         "customerid=" & Val(txtRec0.Tag) & " AND visitid<>" & Val(numVchrNo.Tag) & " ORDER BY visitdate Desc")
        grdVoucher.DataSource = dt
        SetGridHead()
        resizeGridColumn(grdVoucher, 5)
    End Sub
    Private Sub ldVisits()
        Dim dt As DataTable = _objcmnbLayer._fldDatatable("SELECT tockenno [Tocken],Alias [OP Number],AccDescr [Patient Name],visitid from ClinicVistTb " & _
                                         "Left join Accmast on ClinicVistTb.customerid=Accmast.accid " & _
                                         " where isnull(isclosed,0)=0 AND doctor='" & CurrentUser & "' AND " & _
                                         "visitdate>='" & Format(DateValue(Date.Now), "yyyy/MM/dd") & "' ORDER BY tockenno")
        grdlist.DataSource = dt
        SetGridHeadList()
        resizeGridColumn(grdlist, 2)
    End Sub
    Private Sub ldClosedVisits()
        dttable = _objcmnbLayer._fldDatatable("SELECT tockenno [Tocken],Alias [OP Number],AccDescr [Patient Name],visitdate [Date],visitid from ClinicVistTb " & _
                                         "Left join Accmast on ClinicVistTb.customerid=Accmast.accid " & _
                                         " where isnull(isclosed,0)=1 AND doctor='" & CurrentUser & "' AND " & _
                                         "visitdate>='" & Format(DateValue(dtpfrom.Value), "yyyy/MM/dd") & "' AND visitdate<='" & Format(DateValue(dtpto.Value), "yyyy/MM/dd") & "' ORDER BY visitdate desc")
        grdclosedlist.DataSource = dttable
        SetGridHeadClosedList()
        'resizeGridColumn(grdclosedlist, 2)
    End Sub
    Sub SetGridHead()
        With grdVoucher
            SetGridProperty(grdVoucher)
            .Columns("No").Width = 50
            .Columns("Date").Width = 100
            .Columns("Doctor").Width = 100
            .Columns("Comment").Width = 150
            .Columns("Observation").Width = 150
            .Columns("visitid").Visible = False
        End With
    End Sub
    Sub SetGridHeadList()
        With grdlist
            SetGridProperty(grdlist)
            .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular)
            .RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 12.0!)
            .Columns("Tocken").Width = 75
            .Columns("OP Number").Width = 150
            .Columns("visitid").Visible = False
        End With
    End Sub
    Sub SetGridHeadClosedList()
        With grdclosedlist
            SetGridProperty(grdclosedlist)
            .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular)
            .RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 12.0!)
            .Columns("Tocken").Width = 75
            .Columns("OP Number").Width = 120
            .Columns("Patient Name").Width = 120
            .Columns("visitid").Visible = False
        End With
    End Sub

    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        ldVisits()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        ldVisits()
    End Sub

    Private Sub grdlist_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdlist.CellClick
        If Val(numVchrNo.Tag) > 0 Then
            MsgBox("Please Close Opened Visit File", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        With grdlist
            If .RowCount = 0 Then Exit Sub
            numVchrNo.Tag = Val(.Item("visitid", .CurrentRow.Index).Value)
            txtRec0.Focus()
        End With
        returnClinicVisit()
        ldVisitHistory()
    End Sub
    Private Sub returnClinicVisit()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select ClinicVistTb.*,AccDescr,Alias,gender,dateofbirth from ClinicVistTb " & _
                                         "left join AccMast on AccMast.accid=ClinicVistTb.customerid " & _
                                         "LEFT JOIN AccMastAddr ON AccMast.accid=AccMastAddr.AccountNo " & _
                                         " where visitid=" & Val(numVchrNo.Tag))
        If dt.Rows.Count > 0 Then
            txttoken.Text = Val(dt(0)("tockenno") & "")
            numVchrNo.Text = Val(dt(0)("visitno") & "")
            txtRec0.Tag = dt(0)("customerid")
            txtremarks.Text = Trim(dt(0)("comment") & "")
            txtRec1.Text = Trim(dt(0)("AccDescr") & "")
            txtRec0.Text = Trim(dt(0)("Alias") & "")
            btnclosevisit.Enabled = True
            btncancel.Enabled = True
            lblgender.Text = IIf(Val(dt(0)("gender") & "") = 0, "Male", "Female")
            If Not IsDBNull(dt(0)("dateofbirth")) Then
                lbldob.Text = DateValue(dt(0)("dateofbirth")) & " / Age : " & DateDiff(DateInterval.Year, DateValue(dt(0)("dateofbirth")), DateValue(Date.Now))
            End If
        End If
        txtRec0.Focus()
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
            .cmbdoctor.Text = CurrentUser
            .lblvisitnumber.Tag = numVchrNo.Text
            .cmbdoctor.Enabled = False
            .ShowDialog()
        End With
    End Sub

    Private Sub btnclosevisit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclosevisit.Click
        If MsgBox("Do you want to close the Visit?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        _objcmnbLayer._saveDatawithOutParm("Update ClinicVistTb set isclosed=1 where visitid=" & Val(numVchrNo.Tag))
        ldVisits()
        makeClear()
    End Sub
    Private Sub makeClear()
        txtRec0.Text = ""
        txtRec0.Tag = ""
        txtRec1.Text = ""
        numVchrNo.Tag = ""
        numVchrNo.Text = ""
        txtremarks.Text = ""
        txttoken.Text = ""
        lbldob.Text = ""
        lblgender.Text = ""
        ldVisitHistory()
        txtRec0.Focus()
        btnclosevisit.Enabled = False
        btncancel.Enabled = False
    End Sub

    Private Sub TabControl1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.Click
        If TabControl1.SelectedIndex = 1 Then
            ldClosedVisits()
        End If
    End Sub

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        makeClear()
    End Sub

    Private Sub grdVoucher_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.DoubleClick
        If Val(txtRec0.Tag) > 0 Then
            Dim frm As New VisitNoteFrm
            With frm
                .lblvisitnumber.Text = "Visit Number: " & Val(grdVoucher.Item("No", grdVoucher.CurrentRow.Index).Value)
                .txtCashCustomer.Text = txtRec1.Text
                .txtCashCustomer.Tag = Val(txtRec0.Tag)
                .visitid = Val(grdVoucher.Item("visitid", grdVoucher.CurrentRow.Index).Value)
                .cmbdoctor.Enabled = False
                If Val(grdVoucher.Item("visitid", grdVoucher.CurrentRow.Index).Value) <> Val(numVchrNo.Tag) Then
                    .txtcomment.ReadOnly = True
                    .txtobservation.ReadOnly = True
                    .txtdoctornote.ReadOnly = True
                End If
                .ShowDialog()
                ldVisitHistory()
            End With
        Else
            MsgBox("Invalid OP Number", MsgBoxStyle.Exclamation)
            txtRec0.Focus()
        End If
    End Sub
    Private Sub LdCompDet()
        Dim CompanyTb As DataTable
        _objcmnbLayer = New clsCommon_BL
        CompanyTb = _objcmnbLayer._fldDatatable("SELECT * FROM CompanyTb")
        If CompanyTb.Rows.Count > 0 Then
            Me.Cursor = Cursors.WaitCursor
            setExtraPara(CompanyTb, False)
            Me.Cursor = Cursors.Default
            With fMainForm
                .mnujob.Visible = enableServiceJob
                .btnjob.Visible = enableServiceJob
                .btnTracking.Visible = enableServiceJob
                .lblcompany.Text = UCase(Trim("" & CompanyTb(0)("compName")))
                .lblcompany.Left = (.Width / 2) - (.lblcompany.Width / 2)
            End With
            defaultState = Trim("" & CompanyTb(0)("statecode"))
            If Not IsDBNull(CompanyTb(0)("calcluatetaxFrompriceInv")) Then
                calcluatetaxFrompriceInv = CompanyTb(0)("calcluatetaxFrompriceInv")
            End If
            If Not IsDBNull(CompanyTb(0)("calcluatetaxFrompricedoc")) Then
                calcluatetaxFrompricedoc = CompanyTb(0)("calcluatetaxFrompricedoc")
            End If
            If Not IsDBNull(CompanyTb(0)("searchStartOnly")) Then
                searchStartOnly = CompanyTb(0)("searchStartOnly")
            End If
            If Not IsDBNull(CompanyTb(0)("AccPeriodFrm")) Then
                DateFrom = DateValue(CompanyTb(0)("AccPeriodFrm"))
            Else
                DateFrom = DateValue("01/01/1950")
            End If
            If Not IsDBNull(CompanyTb(0)("AccPeriodTo")) Then
                DateTo = DateValue(CompanyTb(0)("AccPeriodTo"))
            Else
                DateTo = DateValue("01/01/1950")
            End If
            If Not IsDBNull(CompanyTb(0)("priceInSales")) Then
                priceInSales = CompanyTb(0)("priceInSales")
            End If
        End If
        If DPath <> "" Then
            If Mid(DPath, Len(DPath)) <> "\" Then
                DPath = DPath & "\"
            End If
            
        End If
    End Sub

    Private Sub btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnload.Click
        ldClosedVisits()
    End Sub

    Private Sub grdlist_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdlist.CellContentClick

    End Sub

    Private Sub grdclosedlist_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdclosedlist.CellClick
        If Val(numVchrNo.Tag) > 0 Then
            MsgBox("Please Close Opened Visit File", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        With grdclosedlist
            If .RowCount = 0 Then Exit Sub
            numVchrNo.Tag = Val(.Item("visitid", .CurrentRow.Index).Value)
            txtRec0.Focus()
        End With
        returnClinicVisit()
        ldVisitHistory()
    End Sub

    Private Sub btnloadcalls_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnloadcalls.Click
        ldFollowup()
    End Sub
    Private Sub ldFollowup()
        Dim dtfollowup As DataTable = _objcmnbLayer._fldDatatable("SELECT calldate [Date],Alias [OP Number],AccDescr [Patient Name],Address1+Address2 Address,Phone,Pupose Purpose,salesman Doctor," & _
                                         "recallid from CustomerFollowupTb " & _
                                         "Left join Accmast on CustomerFollowupTb.customerid=Accmast.accid " & _
                                         "Left join AccMastAddr on CustomerFollowupTb.customerid=AccMastAddr.accountno " & _
                                         " where isnull(isclosed,0)=0 AND " & _
                                         "calldate>='" & Format(DateValue(dtpfollowupdate1.Value), "yyyy/MM/dd") & "' AND calldate<='" & Format(DateValue(dtpfollowupdate2.Value), "yyyy/MM/dd") & "' ORDER BY calldate")
        grdfollowup.DataSource = dtfollowup
        SetGridHeadFollowupList()
        'resizeGridColumn(grdlist, 5)
    End Sub
    Sub SetGridHeadFollowupList()
        With grdfollowup
            SetGridProperty(grdfollowup)
            .Columns("Date").Width = 75
            .Columns("OP Number").Width = 100
            .Columns("Patient Name").Width = 150
            .Columns("Address").Visible = False
            .Columns("phone").Visible = False
            .Columns("Doctor").Visible = False
            .Columns("purpose").Width = 150
            .Columns("recallid").Visible = False
        End With
    End Sub

    Private Sub grdfollowup_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdfollowup.CellContentClick

    End Sub

    Private Sub grdfollowup_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdfollowup.DoubleClick
        If grdfollowup.RowCount = 0 Then Exit Sub
        Dim frm As New AddFollowupFrm
        frm.dtpcalldate.Tag = Val(grdfollowup.Item("recallid", grdfollowup.CurrentRow.Index).Value)
        frm.grpcallupdate.Visible = False
        frm.btnupdate.Visible = False
        frm.txtpurpose.Enabled = False
        frm.Text = "Followup"
        frm.ShowDialog()
    End Sub
End Class