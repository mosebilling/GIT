Public Class VisitNoteFrm
    Public visitid As Long
    Public visitnoteid As Long
    Private _objclinic As New clsClinic
    Private _objcmnbLayer As New clsCommon_BL
    Private chgbyprg As Boolean
    Private _srchTxtId As Integer
    Private _srchOnce As Boolean
    Private MyActiveControl As New Object
    Private lstKey As Keys
    Private WithEvents fMList As Mlistfrm
    Private WithEvents fwait As WaitMessageFrm
    Private itemid As Long
    Private medicinetrid As Long
    Private selectedrow As Integer
    Private chgitem As Boolean
    Private jvtype As String
#Region "constVariables"
    Private Const constslno = 0
    Private Const constitemcode = 1
    Private Const constitemname = 2
    Private Const constqty = 3
    Private Const constnoofdays = 4
    Private Const constuse = 5
    Private Const constmedicinetrid = 6
    Private Const constitemid = 7
    Private Const constedit = 8
#End Region

    Private Sub setGridHead()
        SetGridProperty(grdmedicine)
        With grdmedicine
            .ColumnCount = 9

            .Columns(constslno).HeaderText = "SLNO"
            .Columns(constslno).Width = 50

            .Columns(constitemcode).HeaderText = "Medicine Code"
            .Columns(constitemcode).Width = 100
            .Columns(constitemcode).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(constitemcode).ReadOnly = True

            .Columns(constitemname).HeaderText = "Medicine Name"
            .Columns(constitemname).Width = 100
            .Columns(constitemname).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(constitemname).ReadOnly = True

            .Columns(constqty).HeaderText = "Qty"
            .Columns(constqty).Width = 75
            .Columns(constqty).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(constqty).DefaultCellStyle.Format = "N0"
            .Columns(constqty).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(constnoofdays).HeaderText = "Days"
            .Columns(constnoofdays).Width = 75
            .Columns(constnoofdays).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(constnoofdays).DefaultCellStyle.Format = "N0"
            .Columns(constnoofdays).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(constuse).HeaderText = "Use of Medicine"
            .Columns(constuse).Width = 200
            .Columns(constuse).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(constuse).ReadOnly = True

            .Columns(constmedicinetrid).Visible = False
            .Columns(constitemid).Visible = False
            .Columns(constedit).Visible = False

        End With
    End Sub
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Private Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        If txtcomment.Text = "" And txtobservation.Text = "" And txtdoctornote.Text = "" Then
            MsgBox("Please add the data", MsgBoxStyle.Exclamation)
            txtcomment.Focus()
            Exit Sub
        End If
        If visitid = 0 Then
            With _objclinic
                .visitid = visitid
                .visitdate = cldrdate.Value
                .doctor = cmbdoctor.Text
                '.comment = txtcomment.Text
                '.observation = txtobservation.Text
                '.treatement = txtdoctornote.Text
                .customerid = Val(txtCashCustomer.Tag)
                .visitno = Val(lblvisitnumber.Tag)
                .crtby = CurrentUser
                .visittype = "OE"
                If chkfollowup.Checked Then
                    .isfollowup = 1
                    .followupdate = DateValue(dtpfollowup.Value)
                    .followupText = txtfollowup.Text
                Else
                    .followupdate = DateValue("01/01/1950")
                    .isfollowup = 0
                    .followupText = ""
                End If
                visitid = .saveClinicVisitTb()
            End With
        End If
        With _objclinic
            .cvn_visitnoteid = visitnoteid
            .visitid = visitid
            .visitdate = cldrdate.Value
            .doctor = cmbdoctor.Text
            .comment = txtcomment.Text
            .observation = txtobservation.Text
            .treatement = txttreatmentdescription.Text
            .doctornote = txtdoctornote.Text
            .cvn_otherremarks = txtremark.Text
            .crtby = CurrentUser
            visitnoteid = .saveClinicVisitNoteTb()
        End With
        saveviistmedicine(visitnoteid)
        'If chkfollowup.Checked Then createFollowup()
        'closeFollowup()
        MsgBox("Updated", MsgBoxStyle.Information)
        Me.Close()
    End Sub
    Private Sub closeFollowup()
        _objcmnbLayer._saveDatawithOutParm("Update CustomerFollowupTb set isclosed=1 where customerid=" & Val(txtCashCustomer.Tag) & " AND calldate='" & Format(DateValue(cldrdate.Value), "yyyy/MM/dd") & "'")
    End Sub
    Private Sub createFollowup()
        Dim callid As Long
        Dim dtfollowup As DataTable = _objcmnbLayer._fldDatatable("Select recallid from CustomerFollowupTb where visitid=" & visitid)
        If dtfollowup.Rows.Count > 0 Then
            callid = Val(dtfollowup(0)("recallid") & "")
        End If
        If callid > 0 Then
            _objcmnbLayer._saveDatawithOutParm("update CustomerFollowupTb set calldate='" & Format(DateValue(dtpfollowup.Value), "yyyy/MM/dd") & "'," & _
                                               "pupose='" & MkDbSrchStr(txtfollowup.Text) & _
                                               "',salesman='" & cmbdoctor.Text & "'" & _
                                               " where recallid=" & callid)
        Else
            Dim calldate As Date = Format(DateValue(dtpfollowup.Value), "yyyy/MM/dd")
            _objcmnbLayer._saveDatawithOutParm("insert into CustomerFollowupTb(customerid,calldate,pupose,salesman,visitid) values(" & Val(txtCashCustomer.Tag) & _
                                               ",'" & Format(DateValue(calldate), "yyyy/MM/dd") & "','" & MkDbSrchStr(txtfollowup.Text) & "','" & cmbdoctor.Text & "'," & visitid & ")")
        End If

    End Sub



    Private Sub VisitNoteFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim _slsManTable As DataTable = _objcmnbLayer._fldDatatable("SELECT SManCode FROM SalesmanTb WHERE isnull(isdoctor,0)=1")
        Dim i As Integer
        cmbdoctor.Items.Add("")
        For i = 0 To _slsManTable.Rows.Count - 1
            cmbdoctor.Items.Add(_slsManTable(i)("SManCode"))
        Next
        setGridHead()
        If visitnoteid > 0 Then ldVist()
    End Sub
    Private Sub ldVist()
        Dim ds As DataSet
        Dim dt As DataTable
        Dim str As String
        str = "SELECT ClinicVisitNoteTb.*,visitno,customerid,AccDescr from ClinicVisitNoteTb " & _
        "left join ClinicVistTb on ClinicVistTb.visitid=ClinicVisitNoteTb.cvn_visitid  " & _
        "left join accmast on accmast.accid=ClinicVistTb.customerid " & _
        "where cvn_visitnoteid=" & visitnoteid

        str = str & " update ClinicVisitMedicineTb set setremove=0 where vnm_visitnoteid=" & visitnoteid & _
        " select ClinicVisitMedicineTb.*,[item code] from ClinicVisitMedicineTb " & _
        "left join invitm on ClinicVisitMedicineTb.vnm_itemid=invitm.itemid  where vnm_visitnoteid=" & visitnoteid
        ds = _objcmnbLayer._ldDataset(str, False)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            cldrdate.Value = Format(dt(0)("cvn_notedate"), "dd/MM/yyyy")
            cmbdoctor.Text = Trim(dt(0)("cvn_doctor") & "")
            txtcomment.Text = Trim(dt(0)("cvn_notecomment") & "")
            txtobservation.Text = Trim(dt(0)("cvn_visitnoteobservation") & "")
            txtdoctornote.Text = Trim(dt(0)("cvn_doctornote") & "")
            txttreatmentdescription.Text = Trim(dt(0)("cvn_treatmentnote") & "")
            txtremark.Text = Trim(dt(0)("cvn_otherremarks") & "")
            visitid = Val(dt(0)("cvn_visitid") & "")
            txtCashCustomer.Text = Trim(dt(0)("AccDescr") & "")
            txtCashCustomer.Tag = Val(dt(0)("customerid") & "")
        End If
        dt = Nothing
        dt = ds.Tables(1)
        Dim i As Integer
        For i = 0 To dt.Rows.Count - 1
            With grdmedicine
                .Rows.Add()
                i = .Rows.Count - 1
                .Item(constslno, i).Value = i + 1
                .Item(constitemcode, i).Value = Trim(dt(i)("item code") & "")
                .Item(constitemname, i).Value = Trim(dt(i)("vnm_medicinename") & "")
                .Item(constqty, i).Value = Val(dt(i)("vnm_qty"))
                .Item(constuse, i).Value = Trim(dt(i)("vnm_use") & "")
                .Item(constnoofdays, i).Value = Val(dt(i)("vnm_noofdays"))
                .Item(constmedicinetrid, i).Value = dt(i)("vnm_medicinetrid")
                .Item(constitemid, i).Value = dt(i)("vnm_itemid")
            End With
        Next
    End Sub

    Private Sub txtitemname_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtitemname.KeyDown
        lstKey = e.KeyCode
        If e.KeyCode = Keys.Return Then
            If txtitemname.Text <> "" And chgitem = True Then
                loadWaite(0)
            End If
            txtqty.Focus()
            chgitem = False
        ElseIf e.KeyCode = Keys.Up Or e.KeyCode = Keys.PageUp Then
            If Not fMList Is Nothing Then
                If fMList.Visible Then
                    fMList.MoveUp(txtitemname.Text)
                    txtitemname.SelectionStart = Len(txtitemname.Text) + 1
                    'txtitemcode.SelectionLength = Len(txtitemcode.Text)
                    'txtitemcode.SelectAll()
                    Exit Sub
                End If
            End If

        ElseIf e.KeyCode = Keys.Down Or e.KeyCode = Keys.PageDown Then
            If Not fMList Is Nothing Then
                If fMList.Visible Then
                    fMList.MoveDown(txtitemname.Text)
                    'txtitemcode.SelectionStart = Len(txtitemcode.Text)
                    'txtitemcode.SelectionLength = Len(txtitemcode.Text)
                    Exit Sub
                End If
            End If
        End If
    End Sub

    Private Sub txtitemname_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtitemname.TextChanged
        If chgbyprg Then Exit Sub
        Timer1.Enabled = False
        Timer1.Enabled = True
        chgitem = True
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        If chgbyprg = True Then Exit Sub
        _srchTxtId = 1
        _srchOnce = False
        ShowFmlist(txtitemname, True)
    End Sub
    Private Sub ShowFmlist(ByVal myCtrl As Control, Optional ByVal isFromTexbox As Boolean = False)
        If chgbyprg Then Exit Sub
        MyActiveControl = myCtrl
        Dim alreadyOpened As Boolean
        If Not _srchOnce Then
            chgbyprg = True
            If fMList Is Nothing Then
                fMList = New Mlistfrm
            Else
                alreadyOpened = True
            End If
            Dim PopupLoc As Point
            Dim x As Integer
            Dim y As Integer
            If Not isFromTexbox Then

                x = Me.Width - fMList.Width - 100
                y = Me.Height - fMList.Height - 100
            Else
                If Not alreadyOpened Then
                    fMList.Width = fMList.Width + 100
                    fMList.resizecolum = 1
                End If
                x = Me.Left + 100
                y = Me.Top + 230
            End If
            PopupLoc = New Point(x, y)
            If fMList.Visible = False Then
                Select Case _srchTxtId
                    Case 1 'item master 
                        SetFmlist(fMList, 1)
                End Select
                fMList.loc = PopupLoc
                fMList.Show(Me)
                fMList.Visible = True
            End If
        End If
        Select Case _srchTxtId
            Case 1    'Item Master
                fMList.SearchIndex = 0
                fMList_doFocus()
                'fMList.Search(txtitemcode.Text, True, chkSearch.Checked)
                fMList.Search(txtitemname.Text, "", True)
                fMList.AssignList(txtitemname, lstKey, chgbyprg)
                txtitemname.SelectionStart = Len(txtitemname.Text)
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
                txtitemname.Text = ItmFlds(1)
                txtitemname.Tag = ItmFlds(0)
        End Select
        chgbyprg = False
    End Sub

    Private Sub TabControl1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.Click
        If TabControl1.SelectedIndex = 1 Then
            txtitemname.Focus()
            resizeGridColumn(grdmedicine, constitemname)
        End If
    End Sub
    Private Sub loadWaite(ByVal triggerType As Integer)

        If Not fwait Is Nothing Then fwait.Close() : fwait = Nothing
        fwait = New WaitMessageFrm
        With fwait
            .triggerType = triggerType
            '.Visible = True
            .ShowDialog()
        End With

    End Sub
    Private Sub fwait_triggerEvent() Handles fwait.triggerEvent
        Select Case fwait.triggerType
            Case 0
                itemCodeKeyEnter()
        End Select
        If Not fwait Is Nothing Then fwait.Close() : fwait = Nothing
    End Sub

    Private Sub fwait_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fwait.FormClosed
        fwait = Nothing
    End Sub
    Private Sub itemCodeKeyEnter()
        If Not fMList Is Nothing Then fMList.Visible = False
        Dim dt As DataTable = Nothing
        If Trim(txtitemname.Tag & "") = "" Then
            txtitemname.Tag = txtitemname.Text
        End If
        Dim qtyqry As String
        Dim skipLoad As Boolean
        qtyqry = "SELECT Itemid,Description,isnull(wmcalculation,0)wmcalculation,removebatchwise FROM InvItm " & _
                                            "where [item code]='" & txtitemname.Tag & "'"
        If skipLoad = False Then dt = _objcmnbLayer._fldDatatable(qtyqry)
        skipLoad = False
        If dt.Rows.Count = 0 Then
            MsgBox("Item not found", MsgBoxStyle.Exclamation)
            chgbyprg = True
            txtitemname.Text = ""
            chgbyprg = False
            Exit Sub
        Else
            chgbyprg = True
            itemid = dt(0)("Itemid")
            txtitemname.Text = dt(0)("Description")
            chgbyprg = False
            txtdescription.Focus()
        End If
        chgitem = False
    End Sub

    Private Sub txtdescription_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtdescription.KeyDown, _
    txtqty.KeyDown, txtdays.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtusage_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtusage.KeyDown
        If e.KeyCode = Keys.Return Then
            If txtusage.Text <> "" Then
                If Mid(txtusage.Text, Len(txtusage.Text) - 1) = vbCrLf Then
                    btnitmAdd.Focus()
                End If
            Else
                btnitmAdd.Focus()
            End If
        End If
    End Sub

    Private Sub txtqty_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtqty.KeyPress, txtdays.KeyPress
        NumericTextOnKeypress(sender, e, chgbyprg, numFormat)
    End Sub
    Private Sub addtogrid()
        If itemid = 0 Then Exit Sub
        With grdmedicine
            Dim i As Integer
            If selectedrow - 1 < 0 Then
                .Rows.Add()
                i = .Rows.Count - 1
            Else
                i = selectedrow - 1
            End If
            .Item(constslno, i).Value = i + 1
            .Item(constitemcode, i).Value = txtitemname.Tag
            .Item(constitemname, i).Value = txtitemname.Text
            .Item(constqty, i).Value = Val(txtqty.Text)
            .Item(constnoofdays, i).Value = Val(txtdays.Text)
            .Item(constuse, i).Value = txtusage.Text
            .Item(constmedicinetrid, i).Value = medicinetrid
            .Item(constitemid, i).Value = itemid
            .Item(constedit, i).Value = "Y"
        End With
        clearMedicinedetails()
    End Sub
    Private Sub clearMedicinedetails()
        chgbyprg = True
        txtitemname.Text = ""
        txtitemname.Tag = ""
        txtdescription.Text = ""
        txtqty.Text = 0
        txtdays.Text = 0
        txtusage.Text = ""
        medicinetrid = 0
        itemid = 0
        chgbyprg = False
        chgitem = False
        txtitemname.Focus()
    End Sub

    Private Sub btnitmAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnitmAdd.Click
        addtogrid()
    End Sub

    Private Sub btnclearmedicine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclearmedicine.Click
        clearMedicinedetails()
    End Sub

    Private Sub grdmedicine_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdmedicine.DoubleClick
        With grdmedicine
            chgbyprg = True
            If .RowCount = 0 Then Exit Sub
            If .CurrentRow Is Nothing Then Exit Sub
            If .CurrentRow.Index < 0 Then Exit Sub
            Dim i As Integer = .CurrentRow.Index
            txtitemname.Tag = .Item(constitemcode, i).Value
            txtitemname.Text = .Item(constitemname, i).Value
            txtqty.Text = Val(.Item(constqty, i).Value)
            txtdays.Text = Val(.Item(constnoofdays, i).Value)
            txtusage.Text = .Item(constuse, i).Value
            medicinetrid = .Item(constmedicinetrid, i).Value
            selectedrow = i + 1
            itemid = .Item(constitemid, i).Value
            chgbyprg = False
        End With
    End Sub
    Private Sub removeRow()
        With grdmedicine
            If .RowCount = 0 Then Exit Sub
            If .CurrentRow Is Nothing Then Exit Sub
            If .CurrentRow.Index < 0 Then Exit Sub
            Dim i As Integer = .CurrentRow.Index
            If MsgBox("Do you want to remove row?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            .Rows.RemoveAt(i)
            .ClearSelection()
        End With
    End Sub
    Private Sub saveviistmedicine(ByVal visitnotid As Long)
        Dim str As String
        Dim i As Integer
        For i = 0 To grdmedicine.Rows.Count - 1
            With grdmedicine
                If .Item(constedit, i).Value = "Y" Then
                    medicinetrid = .Item(constmedicinetrid, i).Value
                    If medicinetrid > 0 Then
                        str = "update ClinicVisitMedicineTb set vnm_itemid=" & Val(.Item(constitemid, i).Value) & ",vnm_medicinename=N'" & .Item(constitemname, i).Value & "',vnm_use=N'" & .Item(constuse, i).Value & _
                                "',vnm_qty=" & Val(.Item(constqty, i).Value) & ",vnm_noofdays=" & Val(.Item(constnoofdays, i).Value) & " where vnm_medicinetrid=" & medicinetrid
                    Else
                        str = "insert into ClinicVisitMedicineTb(vnm_visitnoteid,vnm_itemid,vnm_medicinename,vnm_use,vnm_qty,vnm_noofdays) values(" & _
                              visitnotid & "," & Val(.Item(constitemid, i).Value) & ",N'" & MkDbSrchStr(.Item(constitemname, i).Value) & _
                              "',N'" & MkDbSrchStr(.Item(constuse, i).Value) & "'," & Val(.Item(constqty, i).Value) & "," & Val(.Item(constnoofdays, i).Value) & ")"
                    End If
                    _objcmnbLayer._saveDatawithOutParm(str)
                End If
            End With
        Next
        str = "delete from ClinicVisitMedicineTb  where isnull(setremove,0)=1 AND vnm_visitnoteid=" & visitnoteid
        _objcmnbLayer._saveDatawithOutParm(str)
    End Sub

    Private Sub btnrem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrem.Click
        If grdmedicine.Rows.Count = 0 Then Exit Sub
        If grdmedicine.CurrentRow Is Nothing Then Exit Sub
        If grdmedicine.CurrentRow.Index < 0 Then Exit Sub
        If MsgBox("Do you want to remove row?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
            With grdmedicine
                Dim i As Integer = .CurrentRow.Index
                Dim str As String = "update ClinicVisitMedicineTb set setremove=1 where isnull(setremove,0)=1 where vnm_medicinetrid=" & Val(.Item(constmedicinetrid, i).Value)
                _objcmnbLayer._saveDatawithOutParm(str)
            End With
        End If
    End Sub
End Class