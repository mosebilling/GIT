Public Class LodgeBookingFrm
#Region "Form Objects"
    Private WithEvents fDoc As DocumentView
    Private WithEvents fSelect As Selectfrm
    Private WithEvents fCrtAcc As CreateAccNew
    Private WithEvents fAddrooms As AddRoomsFrm
    Private WithEvents fProductEnquiry As ItmEnqry
    Private WithEvents fInvoice As MFSalesInvoice
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fRptFormat As RptFormatfrm
    Private WithEvents fMList As Mlistfrm
#End Region
#Region "Class Objects"
    Private _objJob As New clsJob
    Private _objcmnbLayer As New clsCommon_BL
    Private _objInv As New clsInvoice
    Private _objTr As New clsAccountTransaction
    Private _objReport As New clsReport_BL
#End Region
#Region "Local Variable"
    Private chgbyprg As Boolean
    Private chgbyprgAmt As Boolean
    Private _srchTxtId As Byte
    Private activecontrolname As String
    Private _vtable As DataTable
    Private _dtRptTable As DataTable
    Private chgItm As Boolean
    Private dtTax As DataTable
    Private chgAmt As Boolean
    Private chgUprice As Boolean
    Private lstKey As Integer

    Private SrchText As String
    Private textIndex As Integer
    Private _srchOnce As Boolean
    Private _srchIndexId As Byte
    Private strGridSrchString As String
    Private onceChgFld As Boolean
    Private MyActiveControl As New Object
    Private NDec As Integer = 2
    Private chgPost As Boolean
    Private chgservice As Boolean
    Private serviceRow As Integer
    Private chgServicevalidate As Boolean
#End Region
#Region "Const Variables"
    Private Const ConstJobDescr = 4
    Private Const Constcategory = 5
    Private Const ConstCheckIn = 6
    Private Const ConstCheckEstOut = 7
    Private Const ConstCheckOut = 8
    Private Const ConstTag = 9
    Private Const Conststatus = 10
    Private Const ConstRent = 11
    Private Const ConstGst = 12
    Private Const ConstCess = 13
    Private Const ConstTaxPrice = 14
    Private Const ConstRItemid = 15
    Private Const ConstRoomid = 16
#End Region
    Public isCheckIn As Boolean

    Private Sub LodgeBookingFrm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtcustomer.Focus()
    End Sub
    

    Private Sub AddNew()
        chgbyprg = True
        txtjobcode.Text = GenerateNext("")
        chgbyprg = False
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
            dt = _objcmnbLayer._fldDatatable("SELECT top 1 jobcode from JobTb " & _
                                             "inner join LodgeTb on LodgeTb.jobid=JobTb.jobid" & _
                                             " where dtype='BKN' order by JobTb.Jobid desc")
            If dt.Rows.Count > 0 Then
                strCode = dt(0)(0)
            End If
        End If
        If strCode = "" Then
            strCode = "BKN"
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
                dr = _objcmnbLayer._fldDatatable("SELECT jobcode from JobTb WHERE jobcode = '" & tmp & "'")
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

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        If chgPost Then
            If MsgBox("Do you want to Exit?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        End If
        Me.Close()
    End Sub

    Private Sub btnattach_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnattach.Click
        Try
            Dim jobid As Integer
            If Not fDoc Is Nothing Then fDoc = Nothing
            jobid = Val(txtjobcode.Tag)
            fDoc = New DocumentView
            If jobid > 0 Then
                fDoc.KeyId = "hp-" & Val(jobid)
                fDoc.moduleid = 5
                fDoc.isDoc = True
                fDoc.itemid = 0
                fDoc.ldImage()
                fDoc.ShowDialog()
            Else
                fDoc.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub fDoc_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fDoc.FormClosed
        fDoc = Nothing
    End Sub

    Private Sub btnundo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnundo.Click
        If btnundo.Text = "Undo" Then
            If MsgBox("Do you want to undo the process?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            TabControl2.SelectedIndex = 1
            Label27.Enabled = False
            txtprintjob.Enabled = False
            btnundo.Text = "Clear"
        Else
            If MsgBox("Do you want clear all fields?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            TabControl2.SelectedIndex = 0
            Label27.Enabled = True
            txtprintjob.Enabled = True
            AddNew()
        End If
        makeClear()
        txtcustomer.Focus()
    End Sub
    Private Sub makeClear()
        chgbyprg = True
        txtDescription.Text = ""
        txtcustomer.Text = ""
        txtcustomer.Tag = ""
        txtaddress.Text = ""
        lbltotalRent.Text = Format(0, numFormat)
        lblJobvalue.Text = Format(0, numFormat)
        lblserviceCharge.Text = Format(0, numFormat)
        lblJobvalue.Text = Format(0, numFormat)
        grdRooms.Rows.Clear()
        txtnumberofGust.Text = 0
        txtmaleGust.Text = 0
        txtfemaleGust.Text = 0
        txtkids.Text = 0
        cmbidentityproof.Text = ""
        txtidentityProofNumber.Text = ""
        txtjobcode.Tag = ""
        txtvechicledetails.Text = ""
        chgbyprg = False
        btndelete.Enabled = False
        'btnundo.Text = "&New / Clear"
        lbldays.Text = ""
        lblrent.Text = ""
        fillGrid("")
        txtprintjob.Text = ""
        txtcustomer.Focus()
        chgPost = False
        lblgstn.Text = ""
        lblgstn.Tag = ""
        If userType Then
            btnupdate.Tag = IIf(getRight(188, CurrentUser), 1, 0)
        Else
            btnupdate.Tag = 1
        End If
        'btncheckout.Visible = False
        lblcreatedinfo.Text = ""
    End Sub
    Public Sub fillGrid(ByVal jobcode As String)
        Dim source As DataTable = Nothing

        Dim strSql As String = "SELECT JVType [Type],PreFix,JVNum [RV No],JVDate [RV Date],Reference,DealAmt*-1 Amount,Accdescr [Paid By],dbtr.ChqNo [Chq No],dbtr.ChqDate [Chq Date],dbtr.BankCode [Bank Code],JVTypeNo,AccTrCmn.Linkno From AccTrCmn " & _
                    "LEFT JOIN AccTrDet ON AccTrDet.Linkno=AccTrCmn.Linkno " & _
                    "LEFT JOIN (SELECT Linkno,Accdescr,ChqNo,ChqDate,BankCode FROM AccTrDet " & _
                    "LEFT JOIN AccMast On AccTrDet.accountno=AccMast.Accid where DealAmt>0) dbtr ON AccTrDet.Linkno=dbtr.Linkno " & _
                    "where JobCode='" & jobcode & "' and JobCode<>'' and JVType='RV' and DealAmt<0"
        grdrvlist.DataSource = Nothing
        _objcmnbLayer = New clsCommon_BL
        source = _objcmnbLayer._fldDatatable(strSql, False)
        grdrvlist.DataSource = source
        Dim totalRv As Double
        With grdrvlist
            For i = 0 To .RowCount - 1
                If .Item("type", i).Value = "RV" Then
                    totalRv = totalRv + CDbl(.Item("Amount", i).Value)
                End If
            Next
        End With
        lblRv.Text = Format(totalRv, numFormat)
        'lblbalance.Text = Format(CDbl(lblinvamt.Text) - totalRv, numFormat)
        chgbyprg = False
        SetGridHeadInv()
    End Sub

    Private Sub txtjobcode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtjobcode.KeyDown, dtpdate.KeyDown, _
                                                                                                                    txtnumberofGust.KeyDown, txtmaleGust.KeyDown, _
                                                                                                                    txtfemaleGust.KeyDown, txtidentityProofNumber.KeyDown, _
                                                                                                                    txtvechicledetails.KeyDown, txtkids.KeyDown
        Dim myctrl As Control
        myctrl = sender
        If e.KeyCode = Keys.Return Then
            If myctrl.Name = "txtvechicledetails" Then
                btnitmadd.Focus()
            Else
                SendKeys.Send("{TAB}")
            End If

        End If


    End Sub

    Private Sub txtcustomer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtcustomer.KeyDown
        lstKey = e.KeyCode
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.F2 Then
            _srchTxtId = 1
            ldSelect(1)
        ElseIf e.KeyCode = Keys.Up Or e.KeyCode = Keys.PageUp Then
            If fMList.Visible Then
                fMList.MoveUp(txtcustomer.Text)
                Exit Sub
            End If
        ElseIf e.KeyCode = Keys.Down Or e.KeyCode = Keys.PageDown Then
            If Not fMList Is Nothing Then
                If fMList.Visible Then
                    fMList.MoveDown(txtcustomer.Text)
                    Exit Sub
                End If
            End If
        End If

    End Sub

    Private Sub ldSelect(ByVal BVal As Single)
        fSelect = New Selectfrm
        'nSelect = BVal
        SetForm(fSelect, BVal)
        If BVal = 1 Or BVal = 0 Then
            fSelect.Width = 742
            fSelect.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
        Else
            fSelect.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedToolWindow
            fSelect.Width = 425
        End If
        fSelect.cmbShowIndex = 2
        fSelect.Show()
    End Sub
    Private Sub fSelect_doSelect(ByVal KeyId As String, ByVal strFld1 As String, ByVal strFld2 As String) Handles fSelect.doSelect
        chgbyprg = True
        If _srchTxtId = 1 Then
            txtcustomer.Text = strFld1
            txtcustomer.Tag = KeyId
        End If
        chgbyprg = False
    End Sub

    Private Sub fSelect_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fSelect.FormClosed
        fSelect = Nothing
    End Sub

    Private Sub txtcustomer_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcustomer.Validated
        If txtcustomer.Text = "" Then Exit Sub
        loadCustomerDet(Val(txtcustomer.Tag))
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
    End Sub
    Private Sub loadCustomerDet(ByVal accid As Integer)
        Dim dt As DataTable
        _objcmnbLayer = New clsCommon_BL
        chgbyprg = True
        dt = _objcmnbLayer._fldDatatable("SELECT accid,Alias,AccDescr,Address1,Address2,Address3,Address4,Phone,Fax,EMail,PT1,PT2,PT3,PT4," & _
                                          "TrdLcno,TrdDate,ContactName,GSTIN from AccMast LEFT JOIN AccMastAddr ON AccMast.ACCID=AccMastAddr.accountno " & _
                                          IIf(accid > 0, "where accid=" & accid, "where AccDescr='" & txtcustomer.Text & "'"))
        If dt.Rows.Count > 0 Then
            txtcustomer.Text = dt(0)("AccDescr")
            txtcustomer.Tag = dt(0)("accid")
            txtaddress.Text = dt(0)("Address1") & vbCrLf & dt(0)("Address2") & vbCrLf & dt(0)("Address3") & vbCrLf & "Phone : " & dt(0)("Phone") & vbCrLf & dt(0)("ContactName")
            If Trim(dt(0)("GSTIN") & "") <> "" Then
                lblgstn.Text = "GSTN: " & Trim(dt(0)("GSTIN") & "")
                lblgstn.Tag = 1
                lblgstn.Visible = True
            Else
                'chktaxInv.Checked = False
                lblgstn.Text = "GSTN: Nill"
                lblgstn.Tag = 0
            End If
        Else
            txtcustomer.Text = ""
        End If
        chgbyprg = False
    End Sub

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        QuickCust("Customer")
    End Sub
    Private Sub QuickCust(Optional ByVal Grp As String = "Customer")
        fCrtAcc = New CreateAccNew
        With fCrtAcc
            .Condition = "GrpSetOn In ('" & Grp & "')"
            If Grp = "Customer" Then
                .iscust = True
            Else
                .iscust = False
            End If
            .bOnlyOne = True
            .ShowDialog()
        End With
        txtDescription.Focus()
    End Sub

    Private Sub fCrtAcc_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fCrtAcc.FormClosed
        fCrtAcc = Nothing
    End Sub

    Private Sub fCrtAcc_OpenAccMaster(ByRef AccountNo As Long) Handles fCrtAcc.OpenAccMaster
        txtcustomer.Tag = AccountNo
        loadCustomerDet(AccountNo)
    End Sub
    
    Private Sub SetRoomGridHead()
        chgbyprg = True
        With grdRooms

            SetGridProperty(grdRooms)
            .ColumnCount = 17
            .ColumnHeadersHeight = 100
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersDefaultCellStyle.BackColor = Color.Olive
            .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.5!, System.Drawing.FontStyle.Regular)
            .RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.0!)
            '.ReadOnly = True

            .Columns(ConstSlNo).HeaderText = "SlNo"
            .Columns(ConstSlNo).Width = 40
            .Columns(ConstSlNo).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstSlNo).Frozen = True
            .Columns(ConstSlNo).DefaultCellStyle.Format = "N0"
            .Columns(ConstSlNo).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '.Columns(ConstSlNo).ReadOnly = True

            .Columns(ConstItemCode).HeaderText = "Room"
            .Columns(ConstItemCode).Width = 50
            .Columns(ConstItemCode).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(ConstItemCode).ReadOnly = False

            .Columns(ConstBarcode).HeaderText = "HSN Code"
            .Columns(ConstBarcode).Width = 100
            .Columns(ConstBarcode).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstBarcode).ReadOnly = True
            .Columns(ConstBarcode).Visible = False

            .Columns(ConstDescr).HeaderText = "Room Name"
            .Columns(ConstDescr).Width = 220
            .Columns(ConstDescr).SortMode = DataGridViewColumnSortMode.NotSortable

            .Columns(Constcategory).HeaderText = "Category"
            .Columns(Constcategory).Width = 75
            .Columns(Constcategory).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(Constcategory).ReadOnly = True

            .Columns(ConstJobDescr).HeaderText = "Description"
            .Columns(ConstJobDescr).Width = 220
            .Columns(ConstJobDescr).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(ConstDescr).ReadOnly = False

            .Columns(ConstCheckIn).HeaderText = "Booking"
            .Columns(ConstCheckIn).Width = 125
            .Columns(ConstCheckIn).SortMode = DataGridViewColumnSortMode.NotSortable

            .Columns(ConstCheckEstOut).HeaderText = "Check Out Est"
            .Columns(ConstCheckEstOut).Width = 125
            .Columns(ConstCheckEstOut).SortMode = DataGridViewColumnSortMode.NotSortable

            .Columns(ConstCheckOut).HeaderText = "Check Out"
            .Columns(ConstCheckOut).Width = 125
            .Columns(ConstCheckOut).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstCheckOut).Visible = False

            .Columns(Conststatus).HeaderText = "Status"
            .Columns(Conststatus).Width = 50
            .Columns(Conststatus).SortMode = DataGridViewColumnSortMode.NotSortable

            .Columns(ConstRent).HeaderText = "Price"
            .Columns(ConstRent).Width = 75
            .Columns(ConstRent).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstRent).DefaultCellStyle.Format = "N" & 2
            .Columns(ConstRent).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstRent).Visible = False

            .Columns(ConstGst).HeaderText = "GST"
            .Columns(ConstGst).Width = 75
            .Columns(ConstGst).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstGst).DefaultCellStyle.Format = "N" & 2
            .Columns(ConstGst).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstGst).Visible = False

            .Columns(ConstCess).HeaderText = "CESS"
            .Columns(ConstCess).Width = 75
            .Columns(ConstCess).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstCess).DefaultCellStyle.Format = "N" & 2
            .Columns(ConstCess).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstCess).Visible = False

            .Columns(ConstTaxPrice).HeaderText = "Tax Price"
            .Columns(ConstTaxPrice).Width = 75
            .Columns(ConstTaxPrice).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstTaxPrice).DefaultCellStyle.Format = "N" & 2
            .Columns(ConstTaxPrice).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstTaxPrice).ReadOnly = True

            .Columns(ConstTag).HeaderText = "Tag"
            .Columns(ConstTag).Width = 50
            .Columns(ConstTag).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstTag).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(ConstTag).Visible = False

            .Columns(ConstRItemid).Visible = False
            .Columns(ConstRoomid).Visible = False
        End With
        chgbyprg = False
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        resizeGridColumn(grdRooms, ConstJobDescr)
        'resizeGridColumn(grdItem, 5)
    End Sub

    Private Sub btnitmAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnitmadd.Click
        fAddrooms = New AddRoomsFrm
        Dim selectedrooms As String = ""
        With grdRooms
            For i = 0 To .Rows.Count - 1
                selectedrooms = selectedrooms & IIf(selectedrooms = "", "", ",") & .Item(ConstRItemid, i).Value
            Next
        End With
        If selectedrooms <> "" Then
            selectedrooms = "and itemid not in(" & selectedrooms & ")"
        End If
        With fAddrooms
            .selectedRooms = selectedrooms
            .isgstCustomer = IIf(Val(lblgstn.Tag) = 0, False, True)
            .isBooking = True
            .Label5.Text = "Booking Date"
            .ShowDialog()
        End With
        fAddrooms = Nothing
        chgPost = True
        btnupdate.Enabled = True
    End Sub
    Private Sub addRows()
        With grdRooms
            .Rows.Add()
        End With
    End Sub


    Private Sub btnroomedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnroomedit.Click
        If Val(btnroomedit.Tag) = 0 Then
            MsgBox("This user do not have permission to perform this action", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        editRoom()
    End Sub
    Private Sub editRoom()
        If grdRooms.RowCount = 0 Then Exit Sub
        If grdRooms.Item(Conststatus, grdRooms.CurrentRow.Index).Value = "Closed" Then
            MsgBox("You cannot edit closed rooms", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        fAddrooms = New AddRoomsFrm
        Dim selectedrooms As String = ""
        With grdRooms
            For i = 0 To .Rows.Count - 1
                If i <> grdRooms.CurrentRow.Index Then
                    selectedrooms = selectedrooms & IIf(selectedrooms = "", "", ",") & .Item(ConstRItemid, i).Value
                End If
            Next
        End With
        If selectedrooms <> "" Then
            selectedrooms = "and itemid not in(" & selectedrooms & ")"
        End If
        With fAddrooms
            .selectedRooms = selectedrooms
            .ItemId = Val(grdRooms.Item(ConstRItemid, grdRooms.CurrentRow.Index).Value)
            .txtdescription.Text = grdRooms.Item(ConstJobDescr, grdRooms.CurrentRow.Index).Value
            .dtpcheckin.Text = grdRooms.Item(ConstCheckIn, grdRooms.CurrentRow.Index).Value
            .txtestNumber.Text = DateDiff(DateInterval.Day, DateValue(grdRooms.Item(ConstCheckIn, grdRooms.CurrentRow.Index).Value), DateValue(grdRooms.Item(ConstCheckEstOut, grdRooms.CurrentRow.Index).Value))
            .lblestimated.Text = grdRooms.Item(ConstCheckEstOut, grdRooms.CurrentRow.Index).Value
            If grdRooms.Item(Constcategory, grdRooms.CurrentRow.Index).Value = "AC" Then
                .rdoac.Tag = "AC"
            Else
                .rdoac.Tag = "nAC"
            End If
            .jobid = Val(txtjobcode.Tag)
            .btnupdate.Tag = grdRooms.CurrentRow.Index + 1
            .ShowDialog()
            calculateSingleRoomRent(grdRooms.CurrentRow.Index)
            calculateTotalRent()
        End With
        fAddrooms = Nothing
        chgPost = True
        btnupdate.Enabled = True
    End Sub


    Private Sub grdRooms_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdRooms.CellClick
        If grdRooms.RowCount = 0 Then Exit Sub
        If e.ColumnIndex = ConstTag Then
            If grdRooms.Item(Conststatus, grdRooms.CurrentRow.Index).Value = "Active" Then
                MsgBox("You cannot Invoice to Active Rooms, Please checkout before invoicing", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            checkOrUncheckTag(grdRooms, e, ConstTag)
        End If
    End Sub

    Private Sub grdRooms_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdRooms.DoubleClick
        If grdRooms.Rows.Count > 0 Then
            editRoom()
        End If
    End Sub

    Private Sub btnrem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrem.Click
        If Val(btnrem.Tag) = 0 Then
            MsgBox("This user do not have permission to perform this action", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If MsgBox("Do you want remove the row?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        If grdRooms.Rows.Count = 0 Then Exit Sub
        If grdRooms.CurrentRow Is Nothing Then Exit Sub
        grdRooms.Rows.RemoveAt(grdRooms.CurrentRow.Index)
        chgPost = True
        btnupdate.Enabled = True
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        Try
            verify()
        Catch ex As Exception
            MsgBox(ex.Message & vbCrLf & "Try Again!", MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub verify()
        If Val(btnupdate.Tag) = 0 Then
            MsgBox("This user do not have permission to Update", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If btnupdate.Enabled = False Then Exit Sub
        Dim dt As DataTable
        _objcmnbLayer = New clsCommon_BL
        dt = _objcmnbLayer._fldDatatable("Select Jobid from JobTb where jobcode ='" & txtjobcode.Text & "' and jobid<>" & Val(txtjobcode.Tag))
        If dt.Rows.Count > 0 Then
            MsgBox("Job code already exist", MsgBoxStyle.Exclamation)
            txtjobcode.Focus()
            Exit Sub
        End If
        If Val(txtcustomer.Tag) = 0 Then
            MsgBox("Invalid Customer", MsgBoxStyle.Exclamation)
            txtcustomer.Focus()
            Exit Sub
        End If
        If grdRooms.Rows.Count = 0 Then
            MsgBox("Rooms not Found", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If MsgBox("Verification is succeeded. Do you like to file it?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        saveJob()
        txtprintjob.Text = txtjobcode.Text
        MsgBox("Booking saved successfully", MsgBoxStyle.Information)
        If btnundo.Text = "Undo" Then
            makeClear()
            AddNew()
            btnundo.Text = "Clear"
        Else
            btnattach.Enabled = True
        End If
    End Sub
    Private Sub saveJob()
        With _objJob
            .Jobid = Val(txtjobcode.Tag)
            .jobcode = txtjobcode.Text
            .jobdate = DateValue(dtpdate.Value)
            .jobname = ""
            .JobDescription = txtDescription.Text
            .custcode = Val(txtcustomer.Tag)
            .EstimatedDate = DateValue(Date.Now)
            .SIID = 0
            .RvId = 0
            .Userid = CurrentUser
            txtjobcode.Tag = .saveJob()
        End With
        saveLodge()
        If grdRooms.RowCount > 0 Then
            saveRooms()
        End If
        loadRooms(False)
    End Sub
    Private Sub saveLodge()
        With _objJob
            .Jobid = Val(txtjobcode.Tag)
            .custcode = Val(txtcustomer.Tag)
            .ldgdescription = txtDescription.Text
            .noOfGust = Val(txtnumberofGust.Text)
            .malegusts = Val(txtmaleGust.Text)
            .femalegusts = Val(txtfemaleGust.Text)
            .noofKids = Val(txtkids.Text)
            .identityProof = cmbidentityproof.Text
            .identityProofNumber = txtidentityProofNumber.Text
            .vehicleDetails = txtvechicledetails.Text
            .Dtype = "BKN"
            .BookingRef = ""
            .saveLoadgeTb()
        End With
    End Sub
    Private Sub saveRooms()
        _objcmnbLayer._saveDatawithOutParm("update LodgeRoomTb set Setremove=1 WHERE roomstatus=2 and roomLdgid=" & Val(txtjobcode.Tag))
        With grdRooms
            For i = 0 To .Rows.Count - 1
                If .Item(Conststatus, i).Value <> "Closed" Then
                    _objJob.ldgroomid = Val(.Item(ConstRoomid, i).Value)
                    _objJob.roomItemid = Val(.Item(ConstRItemid, i).Value)
                    _objJob.Jobid = Val(txtjobcode.Tag)
                    _objJob.Remark = .Item(ConstJobDescr, i).Value
                    _objJob.checkinDateTime = .Item(ConstCheckIn, i).Value
                    _objJob.checkoutEstimateDateTime = .Item(ConstCheckEstOut, i).Value
                    _objJob.roomcategory = .Item(Constcategory, i).Value
                    _objJob.rent = CDbl(.Item(ConstRent, i).Value)
                    _objJob.gst = CDbl(.Item(ConstGst, i).Value)
                    _objJob.cess = .Item(ConstCess, i).Value
                    _objJob.roomstatus = 2
                    _objJob.saveLodgeRoomTb()
                End If
            Next
            _objcmnbLayer._saveDatawithOutParm("DELETE FROM LodgeRoomTb WHERE isnull(Setremove,0)=1 and roomstatus=2 and roomLdgid=" & Val(txtjobcode.Tag))
        End With
    End Sub

    Private Sub loadRooms(ByVal donotcalculate As Boolean)
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT LodgeRoomTb.*,[Item Code],Description,INVITM.HSNCode,((isnull(rent,0)*(isnull(gst,0)+isnull(cess,0)))/100)+rent TaxPrice FROM LodgeRoomTb " & _
                                         "INNER JOIN INVITM ON LodgeRoomTb.roomItemid=invitm.itemid " & _
                                         "LEFT JOIN GSTTb ON GSTTb.HSNCode=InvItm.HSNCode " & _
                                         "where roomLdgid=" & Val(txtjobcode.Tag))
        Dim i As Integer
        chgbyprg = True
        Dim tax As Double
        With grdRooms
            .Rows.Clear()
            For i = 0 To dt.Rows.Count - 1
                .Rows.Add()
                i = .Rows.Count - 1
                .Item(ConstSlNo, i).Value = i + 1
                .Item(ConstItemCode, i).Value = dt(i)("Item Code")
                .Item(ConstBarcode, i).Value = dt(i)("HSNCode")
                .Item(ConstDescr, i).Value = dt(i)("Description")
                If Val(dt(i)("rent") & "") = 0 Then dt(i)("rent") = 0
                .Item(ConstRent, i).Value = dt(i)("rent")
                If Val(lblgstn.Tag) = 0 Then
                    .Item(ConstTaxPrice, i).Value = Format(CDbl(dt(i)("TaxPrice")), numFormat)
                Else
                    tax = dt(i)("gst")
                    .Item(ConstTaxPrice, i).Value = Format(CDbl(.Item(ConstRent, i).Value) + (CDbl(.Item(ConstRent, i).Value) * tax / 100), numFormat)
                End If
                If Val(dt(i)("gst") & "") = 0 Then dt(i)("gst") = 0
                .Item(ConstGst, i).Value = dt(i)("gst")
                If Val(dt(i)("cess") & "") = 0 Then dt(i)("cess") = 0
                .Item(ConstCess, i).Value = dt(i)("cess")

                .Item(ConstRItemid, i).Value = dt(i)("roomItemid")
                .Item(ConstJobDescr, i).Value = dt(i)("remarks")
                .Item(ConstCheckIn, i).Value = dt(i)("checkinDateTime")
                .Item(ConstCheckEstOut, i).Value = dt(i)("checkoutEstimateDateTime")
                .Item(ConstCheckOut, i).Value = IIf(Val(dt(i)("roomstatus") & "") = 0, dt(i)("checkoutDateTime"), "")
                .Item(ConstRoomid, i).Value = dt(i)("ldgroomid")
                If Val(dt(i)("roomstatus") & "") = 1 Then
                    .Item(Conststatus, i).Value = "Active"
                ElseIf Val(dt(i)("roomstatus") & "") = 0 Then
                    .Item(Conststatus, i).Value = "Closed"
                Else
                    .Item(Conststatus, i).Value = "Booked"
                End If

               
                .Item(Constcategory, i).Value = Trim(dt(i)("roomcategory") & "")
            Next
        End With
        chgbyprg = False
        If Not donotcalculate Then calculateJobValue(1)
        If grdRooms.RowCount > 0 Then
            roomRowEnter(0)
        End If

    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            Const WM_KEYDOWN As Integer = &H100
            Const WM_KEYUP As Integer = &H101
            If msg.Msg = WM_KEYDOWN Or msg.Msg = WM_KEYUP Then
                If msg.WParam.ToInt32() = CInt(Keys.F8) Then
                    verify()
                ElseIf activecontrolname = "grdVoucher" Then
                    If msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up) Or msg.WParam.ToInt32() = CInt(Keys.Enter) Or msg.WParam.ToInt32() = CInt(Keys.F2) Or msg.WParam.ToInt32() = CInt(Keys.F3) Or msg.WParam.ToInt32() = CInt(Keys.F4) Then
                        If (msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up)) Then GoTo ctn
                        Return True
                    End If
                ElseIf msg.WParam.ToInt32() = CInt(Keys.Escape) Then
                    If Not fSelect Is Nothing Then fSelect.Close() : fSelect = Nothing
                    If Not fProductEnquiry Is Nothing Then fProductEnquiry.Close() : fProductEnquiry = Nothing
                End If
            End If
ctn:
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function
    Private Sub ldJobdetails()
        Try
            _objJob = New clsJob
            With _objJob
                .Jobid = 0
                .DateFrom = DateValue(cldrStartDate.Value)
                .DateTo = DateValue(cldrEnddate.Value)
                .custcode = 0
                .Dtype = "BKN"
                If rdoactive.Checked Then
                    If rdocheckin.Checked Then
                        .Tp = 10
                    Else
                        .Tp = 0
                    End If
                ElseIf rdoclosed.Checked Then
                    If rdocheckin.Checked Then
                        .Tp = 5
                    Else
                        .Tp = 2
                    End If
                ElseIf rdoall.Checked Then
                    If rdocheckin.Checked Then
                        .Tp = 6
                    Else
                        .Tp = 11
                    End If
                End If
                _vtable = .returnLodge.Tables(0)
            End With
            grdItem.DataSource = Nothing
            grdItem.DataSource = _vtable
            SetGrid()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try


    End Sub
    Private Sub SetGrid()
        With grdItem
            If .ColumnCount = 0 Then Exit Sub
            SetGridProperty(grdItem)
            .Columns("SLNO").Width = 50
            .Columns("Jobcode").HeaderText = "Code"
            .Columns("Item Code").HeaderText = "Room"
            .Columns("jobdate").HeaderText = "Doc Date"
            .Columns("checkinDateTime").HeaderText = "Check In"
            .Columns("checkoutEstimateDateTime").HeaderText = "Check Out Est"
            .Columns("checkoutDateTime").HeaderText = "Check Out"
            .Columns("ldgdescription").HeaderText = "Description"
            .Columns("AccDescr").HeaderText = "Customer Name"
            .Columns("Userid").HeaderText = "Created By"
            .Columns("CrdtDate").HeaderText = "Created Date"
            .Columns("closed").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("checkinDateTime").DefaultCellStyle.Format = "dd/MM/yyyy hh:mm tt"
            .Columns("checkoutEstimateDateTime").DefaultCellStyle.Format = "dd/MM/yyyy hh:mm tt"
            .Columns("checkoutDateTime").DefaultCellStyle.Format = "dd/MM/yyyy hh:mm tt"
            .Columns("Datefrom").Visible = False
            .Columns("Dateto").Visible = False
            .Columns("lnk").Visible = False
            .Columns("jobid").Visible = False
            .Columns("Description").Visible = False
            '.Columns("ServiceCost").Visible = False
            '.Columns("ItemCost").Visible = False
            '.Columns("JobCloseDate").Visible = False
            .Columns("ldgdescription").Width = 200
            .Columns("AccDescr").Width = 200
            .Columns("jobdate").Width = 80
            .Columns("checkinDateTime").Width = 150
            .Columns("checkoutEstimateDateTime").Width = 150
            .Columns("checkoutDateTime").Width = 150
            .Columns("CrdtDate").Width = 150
            .Columns("Jobcode").Width = 100
            setComboGrid()
            'resizeGridColumn(grdItem, 5)
        End With
    End Sub
    Private Sub setComboGrid()
        cmbOrder.Items.Clear()
        Dim i As Integer = 0
        For i = 0 To grdItem.ColumnCount - 2
            cmbOrder.Items.Add(grdItem.Columns(i).HeaderText)
        Next
        If cmbOrder.Items.Count > 0 Then cmbOrder.SelectedIndex = 0
    End Sub

    Private Sub TabControl2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl2.Click
        If btnundo.Text = "Undo" Then
            MsgBox("Do Undo/Update before moving to List", MsgBoxStyle.Exclamation)
            TabControl2.SelectedIndex = 0
            Exit Sub
        End If
        If TabControl2.SelectedIndex = 1 Then
            'resizeGridColumn(grdItem, 5)
            Label27.Enabled = False
            txtprintjob.Enabled = False
        Else
            Label27.Enabled = True
            txtprintjob.Enabled = True
        End If
    End Sub

    Private Sub txtSeq_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeq.TextChanged
        grdItem.DataSource = SearchGrid(_vtable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
        _dtRptTable = SearchGrid(_vtable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
    End Sub
    Public Sub loadLodgeForEdit()
        chgbyprg = True
        Try
            Dim dt As DataTable
            _objJob = New clsJob
            With _objJob
                .Jobid = Val(txtjobcode.Tag)
                .DateFrom = DateValue(cldrStartDate.Value)
                .DateTo = DateValue(cldrEnddate.Value)
                .custcode = 0
                .Dtype = "BKN"
                .Tp = 1
                dt = .returnLodge.Tables(0)
            End With
            If dt.Rows.Count > 0 Then
                txtjobcode.Text = dt(0)("Jobcode")
                txtprintjob.Text = dt(0)("Jobcode")
                dtpdate.Value = DateValue(dt(0)("jobdate"))
                txtjobcode.Tag = dt(0)("jobid")
                txtcustomer.Tag = dt(0)("custcode")
                loadCustomerDet(Val(txtcustomer.Tag))
                txtDescription.Text = dt(0)("ldgdescription")
                txtnumberofGust.Text = dt(0)("noOfGust")
                txtkids.Text = dt(0)("noofKids")
                txtmaleGust.Text = dt(0)("malegusts")
                txtfemaleGust.Text = dt(0)("femalegusts")
                cmbidentityproof.Text = dt(0)("identityProof")
                txtidentityProofNumber.Text = dt(0)("identityProofNumber")
                txtvechicledetails.Text = dt(0)("vehicleDetails")
            End If
            loadRooms(True)
            fillGrid(txtjobcode.Text)
            calculateJobValue(3)
            Label27.Enabled = True
            txtprintjob.Enabled = True
            'btncheckout.Visible = True
            If Not IsDBNull(dt(0)("CrdtDate")) Then
                lblcreatedinfo.Text = "Created on: " & dt(0)("CrdtDate")
            End If
            If Not IsDBNull(dt(0)("ModiDate")) Then
                lblcreatedinfo.Text = lblcreatedinfo.Text & vbCrLf & "Modified on: " & dt(0)("ModiDate")
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
        btndelete.Enabled = True
        If userType Then
            btnupdate.Tag = IIf(getRight(189, CurrentUser), 1, 0)
            btndelete.Tag = IIf(getRight(190, CurrentUser), 1, 0)
        Else
            btnupdate.Tag = 1
            btndelete.Tag = 1
        End If
        btnattach.Enabled = True
        chgbyprg = False
    End Sub

    Private Sub grdItem_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdItem.DoubleClick
        If grdItem.Rows.Count = 0 Then Exit Sub
        If grdItem.CurrentRow Is Nothing Then Exit Sub
        txtjobcode.Tag = Val(grdItem.Item("jobid", grdItem.CurrentRow.Index).Value)
        loadLodgeForEdit()
        TabControl2.SelectedIndex = 0
        btnundo.Text = "Undo"
    End Sub

    Private Sub TabControl1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.Click
        If TabControl1.SelectedIndex = 1 Then

            btnundo.Enabled = False
            btnupdate.Enabled = False
            btndelete.Enabled = False
        Else
            If chgservice Then
                If MsgBox("Changes found in service? do you want navigate?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    TabControl1.SelectedIndex = 1
                    Exit Sub
                End If
            End If
            btnundo.Enabled = True
            btnupdate.Enabled = True
            btndelete.Enabled = True
            chgservice = False
        End If
    End Sub

    Private Sub btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnload.Click
        ldJobdetails()
    End Sub

    Private Sub btncreateRV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncreateRV.Click
        If txtcustomer.Text = "" Then
            MsgBox("Invalid Customer", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If cmbreceipttype.SelectedIndex = 0 Then
            fMainForm.LoadRVO(0, txtjobcode.Text, Val(txtcustomer.Tag))
        ElseIf cmbreceipttype.SelectedIndex = 1 Then
            fMainForm.LoadRV(0, txtcustomer.Text)
        End If

    End Sub

    Private Sub btnrefreshVoucher_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefreshVoucher.Click
        fillGrid(txtjobcode.Text)
    End Sub

    Private Sub grdrvlist_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdrvlist.DoubleClick
        If grdrvlist.Rows.Count = 0 Then Exit Sub
        If Val(grdrvlist.Item("Linkno", grdrvlist.CurrentRow.Index).Value) = 3 Then
            fMainForm.LoadRV(Val(grdrvlist.Item("Linkno", grdrvlist.CurrentRow.Index).Value), txtcustomer.Text)
        Else
            fMainForm.LoadRVO(Val(grdrvlist.Item("Linkno", grdrvlist.CurrentRow.Index).Value), txtjobcode.Text, 0)
        End If
    End Sub
    Private Sub calculateTotalRent()
        Dim checkindate As Date
        Dim checkoutdate As Date
        Dim roomrent As Double
        Dim rentduration As Integer
        Dim checkinTime As Date
        Dim checkouttime As Date
        Dim ttlrent As Double
        Dim i As Integer
        With grdRooms
            For i = 0 To .Rows.Count - 1
                checkindate = Format(DateValue(.Item(ConstCheckIn, i).Value), "dd/MM/yyyy")
                checkinTime = TimeValue(.Item(ConstCheckIn, i).Value)
                If Trim(.Item(ConstCheckOut, i).Value & "") = "" Then
                    If Trim(.Item(ConstCheckEstOut, i).Value & "") = "" Then
                        checkoutdate = DateValue(Date.Now)
                        checkouttime = TimeValue(Date.Now)
                    Else
                        checkoutdate = DateValue(.Item(ConstCheckEstOut, i).Value)
                        checkouttime = TimeValue(.Item(ConstCheckEstOut, i).Value)
                        If DateValue(checkoutdate) < DateValue(Date.Now) Then
                            checkoutdate = DateValue(Date.Now)
                            checkouttime = TimeValue(Date.Now)
                        End If
                    End If
                Else
                    checkoutdate = DateValue(.Item(ConstCheckOut, i).Value)
                    checkouttime = TimeValue(.Item(ConstCheckOut, i).Value)
                End If

                rentduration = DateDiff(DateInterval.Day, checkindate, checkoutdate) + 1
                If checkouttime <= checkinTime And rentduration > 1 Then
                    rentduration = rentduration - 1
                End If

                If Val(.Item(ConstTaxPrice, i).Value) = 0 Then .Item(ConstTaxPrice, i).Value = Format(0, numFormat)
                roomrent = CDbl(.Item(ConstTaxPrice, i).Value)
                roomrent = roomrent * rentduration
                ttlrent = ttlrent + roomrent
            Next
        End With
        lbltotalRent.Text = Format(ttlrent, numFormat)
    End Sub
    Private Sub calculateSingleRoomRent(ByVal i As Integer)
        Dim checkindate As Date
        Dim checkoutdate As Date
        Dim roomrent As Double
        Dim rentduration As Integer
        Dim ttlrent As Double
        Dim checkinTime As Date
        Dim checkouttime As Date
        With grdRooms
            If Trim(.Item(ConstCheckIn, i).Value & "") <> "" Then
                checkindate = DateValue(.Item(ConstCheckIn, i).Value)
                checkinTime = TimeValue(.Item(ConstCheckIn, i).Value)
                If Trim(.Item(ConstCheckOut, i).Value & "") = "" Then
                    If Trim(.Item(ConstCheckEstOut, i).Value & "") = "" Then
                        checkoutdate = DateValue(Date.Now)
                        checkouttime = TimeValue(Date.Now)
                    Else
                        checkoutdate = DateValue(.Item(ConstCheckEstOut, i).Value)
                        checkouttime = TimeValue(.Item(ConstCheckEstOut, i).Value)
                        If DateValue(checkoutdate) < DateValue(Date.Now) Then
                            checkoutdate = DateValue(Date.Now)
                            checkouttime = TimeValue(Date.Now)
                        End If
                    End If
                Else
                    checkoutdate = DateValue(.Item(ConstCheckOut, i).Value)
                    checkouttime = TimeValue(.Item(ConstCheckOut, i).Value)
                End If
                rentduration = DateDiff(DateInterval.Day, checkindate, checkoutdate) + 1
                If checkouttime <= checkinTime And rentduration > 1 Then
                    rentduration = rentduration - 1
                End If
                If Val(.Item(ConstTaxPrice, i).Value) = 0 Then .Item(ConstTaxPrice, i).Value = Format(0, numFormat)
                roomrent = CDbl(.Item(ConstTaxPrice, i).Value)
                roomrent = roomrent * rentduration
                ttlrent = ttlrent + roomrent
            End If
        End With
        lblrent.Text = Format(ttlrent, numFormat)
        lbldays.Text = rentduration
    End Sub
    Private Sub calculateJobValue(ByVal calculateindex As Integer)
        If calculateindex = 1 Or calculateindex = 3 Then calculateTotalRent()
        If calculateindex = 2 Or calculateindex = 3 Then calculateToalServiceValue()
        If Val(lbltotalRent.Text) = 0 Then lbltotalRent.Text = Format(0, numFormat)
        If Val(lblserviceCharge.Text) = 0 Then lblserviceCharge.Text = Format(0, numFormat)
        lblJobvalue.Text = CDbl(lbltotalRent.Text) + CDbl(lblserviceCharge.Text)
        lblJobvalue.Text = Format(CDbl(lblJobvalue.Text), numFormat)
    End Sub
    Private Sub calculateToalServiceValue()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT SUM(TaxPrice*Qty) TaxPrice from (SELECT ((isnull(IGST,0)*LodgeServiceTb.UnitPrice)/100)+LodgeServiceTb.UnitPrice TaxPrice,Qty FROM LodgeServiceTb " & _
                                        "LEFT JOIN InvItm ON InvItm.itemid = LodgeServiceTb.ldgServiceItemid " & _
                                        "LEFT JOIN GSTTb ON GSTTb.HSNCode=InvItm.HSNCode " & _
                                        "where lodgeid=" & Val(txtjobcode.Tag) & ")tr")
        If dt.Rows.Count > 0 Then
            lblserviceCharge.Text = Format(Val(dt(0)("TaxPrice") & ""), numFormat)
        Else
            lblserviceCharge.Text = Format(0, numFormat)
        End If
    End Sub

    Private Sub grdRooms_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdRooms.RowEnter
        roomRowEnter(e.RowIndex)
    End Sub
    Private Sub roomRowEnter(ByVal RowIndex As Integer)
        With grdRooms
            If .Item(Conststatus, RowIndex).Value = "Closed" Then
                'rdoinvoice.Enabled = True
                'chkwS.Enabled = True
                'btncheckout.Text = "Undo Check Out"
                'btncheckout.BackColor = Color.IndianRed
                'If userType Then
                '    btncheckout.Tag = IIf(getRight(194, CurrentUser), 1, 0)
                'Else
                '    btncheckout.Tag = 1
                'End If

            ElseIf .Item(Conststatus, RowIndex).Value = "Active" Then
                'rdoinvoice.Enabled = False
                'chkwS.Enabled = False
                'btncheckout.Text = "Check &Out"
                'btncheckout.BackColor = Color.MediumSeaGreen
                'If userType Then
                '    btncheckout.Tag = IIf(getRight(193, CurrentUser), 1, 0)
                'Else
                '    btncheckout.Tag = 1
                'End If

            End If
        End With
        calculateSingleRoomRent(RowIndex)
    End Sub


    Private Sub rdoactive_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoactive.Click, rdoall.Click, rdoclosed.Click, rdocheckin.Click, rdocheckout.Click, rdonone.Click
        Dim myctrl As RadioButton
        myctrl = sender
        If myctrl.Name = "rdoactive" Then
            rdonone.Checked = True
            rdocheckout.Checked = False
            rdocheckout.Enabled = False
        Else
            If rdoactive.Checked = False Then rdocheckout.Enabled = True
        End If

        ldJobdetails()
    End Sub


    Private Sub cmbidentityproof_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbidentityproof.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtfemaleGust_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtfemaleGust.TextChanged

    End Sub

    Private Sub rdoactive_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoactive.CheckedChanged

    End Sub

    Private Sub fAddrooms_addOrEditRoom(ByVal itemid As Long, ByVal roomDescr As String, ByVal chkdate As Date, _
                                        ByVal estDays As Integer, ByVal rindex As Integer, ByVal category As String, _
                                        ByVal rent As Double, ByVal gst As Double, ByVal cess As Double, ByVal hsncode As String) Handles fAddrooms.addOrEditRoom
        Dim i As Integer
        If rindex = 0 Then
            addRows()
            i = grdRooms.Rows.Count - 1
        Else
            i = rindex - 1
        End If

        Dim dt As DataTable
        With grdRooms

            .Item(ConstSlNo, i).Value = i + 1
            dt = _objcmnbLayer._fldDatatable("SELECT [Item Code],Description,((isnull(IGST,0)*UnitPrice)/100)+UnitPrice TaxPrice," & _
                                             "((isnull(IGST,0)*UnitPriceWS)/100)+UnitPriceWS NonAcPrice, itemid,InvItm.HSNCode,Make FROM InvItm " & _
                                             " LEFT JOIN GSTTb ON GSTTb.HSNCode=InvItm.HSNCode WHERE Itemid=" & itemid)
            If dt.Rows.Count > 0 Then
                .Item(ConstItemCode, i).Value = dt(0)("Item Code")
                If hsncode <> "" Then
                    .Item(ConstBarcode, i).Value = hsncode
                Else
                    .Item(ConstBarcode, i).Value = dt(0)("HSNCode")
                End If

                .Item(ConstDescr, i).Value = dt(0)("Description")
                .Item(ConstRent, i).Value = Format(CDbl(rent), numFormat)
                .Item(ConstGst, i).Value = Format(CDbl(gst), numFormat)
                .Item(ConstCess, i).Value = Format(CDbl(cess), numFormat)
                Dim tax As Double = (rent * (gst + IIf(Val(lblgstn.Tag) = 0, cess, 0))) / 100
                .Item(ConstTaxPrice, i).Value = Format(rent + tax, numFormat)
                'If category <> "AC" And Val(dt(0)("Make")) = 1 Then
                '    .Item(ConstTaxPrice, i).Value = dt(0)("NonAcPrice")
                'Else
                '    .Item(ConstTaxPrice, i).Value = dt(0)("TaxPrice")
                'End If

                .Item(ConstRItemid, i).Value = dt(0)("itemid")
            End If
            .Item(ConstCheckIn, i).Value = chkdate
            .Item(ConstJobDescr, i).Value = roomDescr
            .Item(Constcategory, i).Value = category
            Dim chkoutdt As String
            chkoutdt = DateAdd(DateInterval.Day, estDays, chkdate)
            .Item(ConstCheckEstOut, i).Value = chkoutdt
        End With
        fAddrooms.Close()
        calculateJobValue(3)
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Dim RptType As String = ""
        Dim RptCaption As String = ""
        If TabControl2.SelectedIndex = 1 Then
            RptType = "LDBLST"
        ElseIf TabControl2.SelectedIndex = 0 Then
            If txtprintjob.Text = "" Then
                MsgBox("Invalid Booking Number", MsgBoxStyle.Information)
                Exit Sub
            End If
            RptType = "LDGB"
        End If
        If chkFormat.Checked Then
            fRptFormat = New RptFormatfrm
            fRptFormat.RptType = RptType
            fRptFormat.ShowDialog()
            fRptFormat = Nothing
        Else
            PrepareReport(RptType)
        End If

    End Sub
    Private Sub loadReport(ByVal FileName As String, ByVal RptCaption As String, ByVal forpint As Boolean)
        If Not frm Is Nothing Then
            frm.Close()
            frm = New ReprtviewNEWfrm
        Else
            frm = New ReprtviewNEWfrm
        End If
        Dim sts As Integer
        Dim tp As Integer
        Dim ds As DataSet
        If TabControl2.SelectedIndex = 0 Then
            ds = _objReport.returnLodgeCheckinForPrint(txtprintjob.Text)
        Else
            If rdoall.Checked Then
                sts = 3
            ElseIf rdoactive.Checked Then
                sts = 2
            Else
                sts = 0
            End If
            If rdonone.Checked Then
                tp = 3
            ElseIf rdocheckin.Checked Then
                tp = 4
            Else
                tp = 5
            End If
            ds = _objReport.returnLodgeForReports(tp, cldrStartDate.Value, cldrEnddate.Value, sts, "")
        End If
        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = fMainForm
        frm.Text = RptCaption
        frm.Show()
    End Sub
    Public Sub PrepareReport(ByVal RptType As String)
        Dim RptName As String
        Dim RptCaption As String = ""
        Dim printername As String = ""
        RptName = getRptDefFlName(RptType, RptCaption, printername)
        If Trim(RptName) <> "" Then
            loadReport(RptName, RptCaption, False)
        End If
    End Sub

    Private Sub fRptFormat_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fRptFormat.FormClosed
        fRptFormat = Nothing
    End Sub
    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        loadReport(RptFlName, RptCaption, False)
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
        chgPost = True
        btnupdate.Enabled = True
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
                        SetFmlist(fMList, 3)
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
                fMList.SearchIndex = 0
                fMList.SearchIndexDescr = 1
                fMList_doFocus()
                fMList.Search(txtcustomer.Text)
                fMList.AssignList(txtcustomer, lstKey, chgbyprg)
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
                txtcustomer.Tag = ItmFlds(3)
        End Select
        chgbyprg = False
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        If Val(btndelete.Tag) = 0 Then
            MsgBox("This user do not have permission to Remove", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If grdrvlist.RowCount > 0 Then
            MsgBox("RV found! cannot remove the document", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If MsgBox("You are going remove the document! are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        _objcmnbLayer._saveDatawithOutParm("DELETE FROM LodgeTb where jobid=" & Val(txtjobcode.Tag))
        _objcmnbLayer._saveDatawithOutParm("DELETE FROM LodgeRoomTb where roomLdgid=" & Val(txtjobcode.Tag))
        _objcmnbLayer._saveDatawithOutParm("DELETE FROM LodgeServiceTb where lodgeid=" & Val(txtjobcode.Tag))
        _objcmnbLayer._saveDatawithOutParm("DELETE FROM JobTb where jobid=" & Val(txtjobcode.Tag))
        btnundo.Text = "Clear"
        AddNew()
        makeClear()
    End Sub

    Private Sub txtjobcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtjobcode.TextChanged
        If chgbyprg Then Exit Sub
        chgPost = True
        btnupdate.Enabled = True
    End Sub

    Private Sub dtpdate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpdate.ValueChanged
        If chgbyprg Then Exit Sub
        chgPost = True
    End Sub

    Private Sub txtmaleGust_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtmaleGust.KeyPress, txtfemaleGust.KeyPress, txtkids.KeyPress
        NumericTextOnKeypress(sender, e, chgbyprg, "0")
    End Sub

    Private Sub txtDescription_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDescription.KeyDown
        If e.KeyCode = Keys.Return Then
            If txtDescription.Text <> "" Then
                If Mid(txtDescription.Text, Len(txtDescription.Text) - 1) = vbCrLf Then
                    txtmaleGust.Focus()
                End If
            End If
        End If
    End Sub



    Private Sub txtDescription_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDescription.TextChanged, txtidentityProofNumber.TextChanged, txtvechicledetails.TextChanged
        If chgbyprg Then Exit Sub
        chgPost = True
        btnupdate.Enabled = True
    End Sub

    Private Sub cmbidentityproof_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbidentityproof.SelectedIndexChanged
        If chgbyprg Then Exit Sub
        chgPost = True
        btnupdate.Enabled = True
    End Sub



    Private Sub txtmaleGust_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtmaleGust.TextChanged, txtfemaleGust.TextChanged, txtkids.TextChanged
        If chgbyprgAmt Then Exit Sub
        chgPost = True
        txtnumberofGust.Text = Val(txtmaleGust.Text) + Val(txtfemaleGust.Text) + Val(txtkids.Text)
        btnupdate.Enabled = True
    End Sub

    Private Sub LodgeBookingFrm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddNew()
        SetRoomGridHead()
       
        ldJobdetails()
        If ShowTaxOnInventory Then
            dtTax = _objcmnbLayer._fldDatatable("SELECT vatcode,convert(money, 0) Amount,collectionAC,paymentAC,vat From VatMasterTb", False)
        ElseIf EnableGST Then
            CreateTaxTable(dtTax)
        End If
        Timer1.Enabled = True
        cmbreceipttype.SelectedIndex = 0
        If userType Then
            btnupdate.Tag = IIf(getRight(188, CurrentUser), 1, 0)
            btndelete.Tag = IIf(getRight(190, CurrentUser), 1, 0)
            btnroomedit.Tag = IIf(getRight(191, CurrentUser), 1, 0)
            btnrem.Tag = IIf(getRight(199, CurrentUser), 1, 0)
        Else
            btnupdate.Tag = 1
            btndelete.Tag = 1
            btnroomedit.Tag = 1
            btnrem.Tag = 1
        End If
    End Sub
    Private Sub SetGridHeadInv()
        If grdrvlist.ColumnCount = 0 Then Exit Sub
        SetGridProperty(grdrvlist)
        With grdrvlist
            .Columns.Item((.ColumnCount - 1)).Visible = False
            .Columns.Item("PreFix").Width = 45
            .Columns.Item("Paid By").Width = 150
            .Columns.Item("Bank Code").Width = 100
            .Columns.Item("Amount").DefaultCellStyle.Format = ("N" & NoOfDecimal)
            .Columns.Item("Amount").Frozen = True
            .Columns.Item("Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns.Item("JVTypeNo").Visible = False
        End With
    End Sub
End Class