Public Class LodgeCheckInFrm
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
    Private WithEvents fSelectBooking As SelectBookingFrm
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
    Private Const ConstBookingRawid = 17
#End Region
    Private Sub LodgeCheckInFrm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtcustomer.Focus()
    End Sub

    Private Sub LodgeCheckInFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AddNew()
        SetRoomGridHead()
        setRoomsHead()
        SetGridHead()
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
            btnremoveservice.Tag = IIf(getRight(192, CurrentUser), 1, 0)
        Else
            btnupdate.Tag = 1
            btndelete.Tag = 1
            btnroomedit.Tag = 1
            btnrem.Tag = 1
            btnremoveservice.Tag = 1
        End If
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
                                             " where dtype='CHI' order by JobTb.Jobid desc")
            If dt.Rows.Count > 0 Then
                strCode = dt(0)(0)
            End If
        End If
        If strCode = "" Then
            strCode = "CHI"
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
        grdroomAdded.Rows.Clear()
        grdVoucher.Rows.Clear()
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
        fillGrid()
        txtprintjob.Text = ""
        txtcustomer.Focus()
        lblgstn.Text = ""
        lblgstn.Tag = ""
        txtbookRef.Text = ""
        txtbookRef.Tag = ""
        chgPost = False
        If userType Then
            btnupdate.Tag = IIf(getRight(188, CurrentUser), 1, 0)
        Else
            btnupdate.Tag = 1
        End If
        btncheckout.Visible = False
        lblcreatedinfo.Text = ""
        btnSlct.Enabled = True
    End Sub
    Public Sub fillGrid()
        Dim num2 As Double
        Dim strSql As String = ("Select  convert(varchar(12), case when prefix=''  then '' else '-' end +  invNo ) as [Inv No] , TrDate [Tr.Date] ," & _
                                "Alias [Cust. Code],AccDescr [Customer Name],(InvAmt) [Amount],TrRefNo [Ref. No]," & _
                                "TrDescription  [Tr. Description],[Job Code],UserId [Created By],TrId from " & _
                                "( select  prefix, invNo ,TrDate ,netamt InvAmt,Discount,AccDescr ,TrRefNo,TrDescription,Alias ,ItmInvCmnTb.UserId,[Job Code],ItmInvCmnTb.TrId from " & _
                                "ItmInvCmnTb LEFT JOIN (SELECT Trid,SUM((TrQty*(UnitCost+UnitOthCost))+taxamt)InvAmt FROM ItmInvTrTb GROUP BY Trid) Tr " & _
                                "ON  ItmInvCmnTb.Trid=Tr.Trid left join Accmast on ItmInvCmnTb.CSCode=Accmast.accid where ItmInvCmnTb.trtype='IS' and [Job Code] ='" & txtjobcode.Text & "') qq  order by TrDate ,InvNo")
        grdinvList.DataSource = Nothing
        _objcmnbLayer = New clsCommon_BL
        Dim source As DataTable = Me._objcmnbLayer._fldDatatable(strSql, False)
        grdinvList.DataSource = source
        Dim num3 As Integer = (source.Rows.Count - 1)
        Dim i As Integer = 0
        Do While (i <= num3)
            num2 = num2 + source(i)("Amount")
            i += 1
        Loop
        lblinvamt.Text = Format(num2, numFormat)
        source = Nothing
        strSql = "SELECT JVType [Type],PreFix,JVNum [RV No],JVDate [RV Date],Reference,DealAmt*-1 Amount,Accdescr [Paid By],dbtr.ChqNo [Chq No],dbtr.ChqDate [Chq Date],dbtr.BankCode [Bank Code],JVTypeNo,AccTrCmn.Linkno From AccTrCmn " & _
                    "LEFT JOIN AccTrDet ON AccTrDet.Linkno=AccTrCmn.Linkno " & _
                    "LEFT JOIN (SELECT Linkno,Accdescr,ChqNo,ChqDate,BankCode FROM AccTrDet " & _
                    "LEFT JOIN AccMast On AccTrDet.accountno=AccMast.Accid where DealAmt>0) dbtr ON AccTrDet.Linkno=dbtr.Linkno " & _
                    "where JobCode in('" & txtjobcode.Text & "'" & ",'" & txtbookRef.Text & "')  and JobCode<>'' and JVType='RV' and DealAmt<0"
        grdrvlist.DataSource = Nothing
        _objcmnbLayer = New clsCommon_BL
        source = _objcmnbLayer._fldDatatable(strSql, False)
        grdrvlist.DataSource = source
        SetGridHeadInv()
        Dim totalRv As Double
        With grdrvlist
            For i = 0 To .RowCount - 1
                If .Item("type", i).Value = "RV" Then
                    totalRv = totalRv + CDbl(.Item("Amount", i).Value)
                End If
            Next
        End With
        lblRv.Text = Format(totalRv, numFormat)
        lblbalance.Text = Format(CDbl(lblinvamt.Text) - totalRv, numFormat)
        chgbyprg = False
    End Sub

    Private Sub SetGridHeadInv()

        If grdinvList.ColumnCount = 0 Then Exit Sub
        SetGridProperty(grdinvList)
        With grdinvList
            .Columns.Item((.ColumnCount - 1)).Visible = False
            .Columns.Item("Inv No").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns.Item("Inv No").Width = &H4B
            .Columns.Item("Customer Name").Width = 200
            .Columns.Item("Tr. Description").Width = 200
            .Columns.Item("Amount").DefaultCellStyle.Format = ("N" & NoOfDecimal)
            .Columns.Item("Amount").Frozen = True
            .Columns.Item("Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End With
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
    Private Sub setRoomsHead()
        chgbyprg = True
        With grdroomAdded
            SetGridProperty(grdroomAdded)
            .ColumnCount = 2
            .Columns(0).HeaderText = "Room NO."
            .Columns(0).Width = 180
            .Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(1).Visible = False
        End With
        chgbyprg = False
    End Sub
    Private Sub SetRoomGridHead()
        chgbyprg = True
        With grdRooms

            SetGridProperty(grdRooms)
            .ColumnCount = 18
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
            .Columns(ConstBarcode).Visible = EnableGST

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

            .Columns(ConstCheckIn).HeaderText = "Check In"
            .Columns(ConstCheckIn).Width = 125
            .Columns(ConstCheckIn).SortMode = DataGridViewColumnSortMode.NotSortable

            .Columns(ConstCheckEstOut).HeaderText = "Check Out Est"
            .Columns(ConstCheckEstOut).Width = 125
            .Columns(ConstCheckEstOut).SortMode = DataGridViewColumnSortMode.NotSortable

            .Columns(ConstCheckOut).HeaderText = "Check Out"
            .Columns(ConstCheckOut).Width = 125
            .Columns(ConstCheckOut).SortMode = DataGridViewColumnSortMode.NotSortable

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

            .Columns(ConstTag).HeaderText = "Invoiced?"
            .Columns(ConstTag).Width = 70
            .Columns(ConstTag).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstTag).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns(ConstRItemid).Visible = False
            .Columns(ConstRoomid).Visible = False
            .Columns(ConstBookingRawid).Visible = False
        End With
        chgbyprg = False
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        resizeGridColumn(grdRooms, ConstJobDescr)
        'resizeGridColumn(grdItem, 5)
        resizeGridColumn(grdroomAdded, 0)
        resizeGridColumn(grdVoucher, ConstDescr)
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
            '.jobid = Val(txtbookRef.Tag)
            .isgstCustomer = IIf(Val(lblgstn.Tag) = 0, False, True)
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
            .rent = Format(CDbl(grdRooms.Item(ConstRent, grdRooms.CurrentRow.Index).Value), numFormat)
            .rdoall.Checked = True
            If grdRooms.Item(Constcategory, grdRooms.CurrentRow.Index).Value = "AC" Then
                .rdoac.Tag = "AC"
            Else
                .rdoac.Tag = "nAC"
            End If
            .btnupdate.Tag = grdRooms.CurrentRow.Index + 1
            .jobid = IIf(Val(txtjobcode.Tag) = 0, Val(txtbookRef.Tag), Val(txtjobcode.Tag))
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
            If grdRooms.Item(ConstTag, e.RowIndex).Value <> "YES" Then checkOrUncheckTag(grdRooms, e, ConstTag)
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
            .Dtype = "CHI"
            .BookingRef = txtbookRef.Text
            .saveLoadgeTb()
        End With
    End Sub
    Private Sub saveRooms()
        _objcmnbLayer._saveDatawithOutParm("update LodgeRoomTb set Setremove=1 WHERE roomstatus=1 and roomLdgid=" & Val(txtjobcode.Tag))
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
                    _objJob.roomstatus = 1
                    _objJob.bookingRawid = Val(.Item(ConstBookingRawid, i).Value & "")
                    _objJob.hsnCode = Trim(.Item(ConstBarcode, i).Value & "")
                    _objJob.saveLodgeRoomTb()
                End If
            Next
            _objcmnbLayer._saveDatawithOutParm("DELETE FROM LodgeRoomTb WHERE isnull(Setremove,0)=1 and roomstatus=1 and roomLdgid=" & Val(txtjobcode.Tag))
        End With
    End Sub
    Private Sub saveService()
        Dim i As Integer
        _objcmnbLayer._saveDatawithOutParm("update LodgeServiceTb set Setremove=1 WHERE  ldgroomid=" & Val(grdroomAdded.Item(1, grdroomAdded.CurrentRow.Index).Value))
        With grdVoucher
            For i = 0 To .Rows.Count - 1
                If Val(.Item(ConstItemID, i).Value) > 0 Then
                    _objJob.ldgServiceId = Val(.Item(ConstId, i).Value)
                    _objJob.ldgServiceItemid = Val(.Item(ConstItemID, i).Value)
                    _objJob.Jobid = Val(txtjobcode.Tag)
                    _objJob.roomItemid = Val(grdroomAdded.Item(1, grdroomAdded.CurrentRow.Index).Value)
                    _objJob.Qty = CDbl(.Item(ConstQty, i).Value)
                    If Trim(.Item(ConstWarrentyExpiry, i).Value & "") <> "" Then
                        _objJob.serviceDateTime = DateValue(.Item(ConstWarrentyExpiry, i).Value)
                    End If
                    _objJob.unitprice = CDbl(.Item(ConstUPrice, i).Value)
                    _objJob.remarks = .Item(ConstDescr, i).Value
                    _objJob.saveLodgeService()
                End If
            Next
        End With
        _objcmnbLayer._saveDatawithOutParm("DELETE FROM LodgeServiceTb WHERE isnull(Setremove,0)=1 and ldgroomid=" & Val(grdroomAdded.Item(1, grdroomAdded.CurrentRow.Index).Value))
        MsgBox("Updated", MsgBoxStyle.Information)
        chgservice = False
        chgServicevalidate = False
    End Sub
    Private Function chkGrid()
        With grdVoucher
            For i = 0 To .Rows.Count - 1
                If CDbl(.Item(ConstQty, i).Value) = 0 Then
                    .Rows(i).Selected = True
                    .CurrentCell = .Item(ConstQty, i)
                    MsgBox("Zero Quantity !!", vbExclamation)
                    .FirstDisplayedScrollingRowIndex = i
                    Return False
                End If
                If CDbl(.Item(ConstUPrice, i).Value) = 0 Then
                    .Rows(i).Selected = True
                    .CurrentCell = .Item(ConstQty, i)
                    If MsgBox("Zero Price ! Do you want to proceed?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                        .FirstDisplayedScrollingRowIndex = i
                        Return False
                    End If
                End If
            Next
        End With
        Return True
    End Function
    Private Sub loadServices(ByVal rIndex As Integer, ByVal donotcalculate As Boolean)
        Dim dt As DataTable
        Dim UPerPack As Integer
        Dim FCRt As Integer = 1
        If grdroomAdded.Rows.Count = 0 Then Exit Sub
        dt = _objcmnbLayer._fldDatatable("SELECT LodgeServiceTb.*, [Item Code],unit,HSNCode FROM LodgeServiceTb " & _
                                         "LEFT JOIN InvItm ON InvItm.itemid = LodgeServiceTb.ldgServiceItemid where ldgroomid=" & Val(grdroomAdded.Item(1, rIndex).Value))
        chgbyprg = True
        Dim actualPrice As Double
        Dim taxamt As Double
        With grdVoucher
            .Rows.Clear()
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    .Rows.Add(1)
                    .Item(ConstSlNo, i).Value = IIf(Val(dt(i)("ldgServiceItemid")) = 0, "M", "")
                    If .Item(ConstSlNo, i).Value <> "M" Then
                        UPerPack = 1
                        .Item(ConstItemCode, i).Value = Trim("" & dt(i)("Item Code"))
                        .Item(ConstItemID, i).Value = dt(i)("ldgServiceItemid")
                        .Item(ConstPFraction, i).Value = 2
                        .Item(ConstPMult, i).Value = UPerPack 'IIf(IsNull(dt!PFraCount), "2", dt!PFraCount)
                        .Item(ConstBarcode, i).Value = dt(i)("HSNCode")
                        Dim dt1 As DataTable = _objcmnbLayer._fldDatatable("SELECT * FROM GSTTb WHERE HSNCODE='" & dt(i)("HSNCode") & "'")
                        If dt1.Rows.Count > 0 Then
                            .Item(ConstCGSTP, i).Value = Format(IIf(IsDBNull(dt1(0)("CGST")), 0, CDbl(dt1(0)("CGST"))), numFormat)
                            .Item(ConstSGSTP, i).Value = Format(IIf(IsDBNull(dt1(0)("SGST")), 0, CDbl(dt1(0)("SGST"))), numFormat)
                            .Item(ConstIGSTP, i).Value = Format(IIf(IsDBNull(dt1(0)("IGST")), 0, CDbl(dt1(0)("IGST"))), numFormat)
                        Else
                            .Item(ConstCGSTP, i).Value = 0
                            .Item(ConstSGSTP, i).Value = 0
                            .Item(ConstIGSTP, i).Value = 0
                        End If
                    Else
                        .Item(ConstPFraction, i).Value = "2"
                        .Item(ConstPMult, i).Value = "1"
                        .Item(ConstItemID, i).Value = 0
                    End If
                    .Item(ConstDescr, i).Value = IIf(IsDBNull(dt(i)("remarks")), "", dt(i)("remarks"))
                    .Item(ConstB, i).Value = "B"
                    .Item(ConstUnit, i).Value = Trim("" & dt(i)("Unit"))
                    actualPrice = dt(i)("unitprice") * dt(i)("Qty")
                    taxamt = (actualPrice * .Item(ConstIGSTP, i).Value) / 100
                    If Val(.Item(ConstIGSTP, i).Value & "") = 0 Then .Item(ConstIGSTP, i).Value = Format(0, numFormat)
                    .Item(ConstTaxP, i).Value = Format(CDbl(.Item(ConstIGSTP, i).Value), numFormat)
                    'If Val(dt(i)("taxamt") & "") = 0 Then dt(i)("taxamt") = 0
                    .Item(ConstTaxAmt, i).Value = Format(taxamt / FCRt, numFormat)
                    .Item(ConstQty, i).Value = Format(dt(i)("Qty") / UPerPack, "#,##0" & IIf(Val(.Item(ConstPFraction, i).Value & "") = 0, "", "." & Strings.StrDup(CByte(Val(.Item(ConstPFraction, i).Value & "")), "0")))
                    .Item(ConstUPrice, i).Value = Format(dt(i)("unitprice") * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & numFormat)
                    .Item(ConstActualPrice, i).Value = dt(i)("unitprice") * UPerPack / FCRt
                    .Item(ConstLTotal, i).Value = Format((dt(i)("Qty") * dt(i)("unitprice")) * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & numFormat)
                    .Item(constItmTot, i).Value = Format((.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value), numFormat)

                    .Item(ConstId, i).Value = dt(i)("ldgServiceId")
                    If Not IsDBNull(dt(i)("serviceDateTime")) Then
                        If DateValue(dt(i)("serviceDateTime")) > DateValue("01/01/1950") Then
                            .Item(ConstWarrentyExpiry, i).Value = dt(i)("serviceDateTime")
                        End If
                    End If
                Next

            End If
        End With
        If Not donotcalculate Then calculateJobValue(2)
        calculate()
        reArrangeNo()
        chgbyprg = False
    End Sub
    Private Sub loadRooms(ByVal donotcalculate As Boolean)
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT LodgeRoomTb.*,[Item Code],Description," & _
                                         "case when isnull(LodgeRoomTb.HSNCode,'')='' then INVITM.HSNCode else isnull(LodgeRoomTb.HSNCode,'') end HSN," & _
                                         "((isnull(rent,0)*(isnull(gst,0)+isnull(cess,0)))/100)+rent TaxPrice,isnull(invitemid,0)invitemid FROM LodgeRoomTb " & _
                                         "INNER JOIN INVITM ON LodgeRoomTb.roomItemid=invitm.itemid " & _
                                         "LEFT JOIN GSTTb ON GSTTb.HSNCode=InvItm.HSNCode " & _
                                         "left join (select ItemId invitemid from ItmInvCmnTb " & _
                                         "left join ItmInvTrTb on ItmInvCmnTb.trid=ItmInvTrTb.TrId " & _
                                         "where [Job Code]='" & txtjobcode.Text & "' and itemid>0) tr on  LodgeRoomTb.roomItemid=tr.invitemid " & _
                                         "where roomLdgid=" & Val(txtjobcode.Tag))
        grdroomAdded.Rows.Clear()
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
                .Item(ConstBarcode, i).Value = dt(i)("HSN")
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
                .Item(ConstBookingRawid, i).Value = Val(dt(i)("bookingRawid") & "")
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
                If Val(dt(i)("invitemid") & "") = 0 Then
                    .Item(ConstTag, i).Value = ""
                Else
                    .Item(ConstTag, i).Value = "YES"
                End If

                If grdroomAdded.ColumnCount > 0 Then
                    grdroomAdded.Rows.Add()
                    grdroomAdded.Item(0, i).Value = dt(i)("Item Code")
                    grdroomAdded.Item(1, i).Value = dt(i)("ldgroomid")
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
                        If (msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up)) And plsrch.Visible = False Then GoTo ctn
                        grdVoucher_KeyDown(Nothing, New KeyEventArgs(keyData))
                        Return True
                    End If
                ElseIf msg.WParam.ToInt32() = CInt(Keys.Escape) Then
                    If Not fSelect Is Nothing Then fSelect.Close() : fSelect = Nothing
                    If Not fProductEnquiry Is Nothing Then fProductEnquiry.Close() : fProductEnquiry = Nothing
                    plsrch.Visible = False
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
                .Dtype = "CHI"
                If rdoactive.Checked Then
                    If rdocheckin.Checked Then
                        .Tp = 4
                    ElseIf rdocheckout.Checked Then
                        .Tp = 7
                    Else
                        .Tp = 0
                    End If

                ElseIf rdoclosed.Checked Then
                    If rdocheckin.Checked Then
                        .Tp = 5
                    ElseIf rdocheckout.Checked Then
                        .Tp = 8
                    Else
                        .Tp = 2
                    End If
                ElseIf rdoall.Checked Then
                    If rdocheckin.Checked Then
                        .Tp = 6
                    ElseIf rdocheckout.Checked Then
                        .Tp = 8
                    Else
                        .Tp = 3
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
                .Dtype = "CHI"
                .Tp = 1
                dt = .returnLodge.Tables(0)
            End With
            If dt.Rows.Count > 0 Then
                txtbookRef.Text = Trim(dt(0)("BookingRef") & "")
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
            loadServices(0, True)
            fillGrid()
            calculateJobValue(3)
            Label27.Enabled = True
            txtprintjob.Enabled = True
            btncheckout.Visible = True
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
        TabControl2.SelectedIndex = 0
        btnundo.Text = "Undo"
        btnSlct.Enabled = False
    End Sub

    Private Sub grdItem_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdItem.DoubleClick
        If grdItem.Rows.Count = 0 Then Exit Sub
        If grdItem.CurrentRow Is Nothing Then Exit Sub
        txtjobcode.Tag = Val(grdItem.Item("jobid", grdItem.CurrentRow.Index).Value)
        loadLodgeForEdit()

    End Sub
    Private Sub SetGridHead()
        chgbyprg = True
        SetGridHeadEntryProperty(grdVoucher)
        With grdVoucher
            .Columns(ConstWarrentyExpiry).Visible = True
            .Columns(ConstWarrentyExpiry).HeaderText = "Date"
            .Columns(ConstUnit).Visible = False
        End With
        chgbyprg = False
    End Sub

    Private Sub TabControl1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.Click
        If TabControl1.SelectedIndex = 1 Then
            resizeGridColumn(grdVoucher, ConstDescr)
            btnundo.Enabled = False
            btnupdate.Enabled = False
            btndelete.Enabled = False
            If Val(txtjobcode.Tag) > 0 Then
                btnaddservice.Enabled = True
                btnremoveservice.Enabled = True
                btnsaveService.Enabled = True
            Else
                btnaddservice.Enabled = False
                btnremoveservice.Enabled = False
                btnsaveService.Enabled = False
            End If
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
    Private Sub addServiceRow()
        If Val(txtjobcode.Tag) = 0 Then
            MsgBox("Voucher has not been loaded", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        AddRow()
        grdVoucher.CurrentCell = grdVoucher.Item(ConstItemCode, grdVoucher.CurrentRow.Index)
    End Sub
    Private Sub AddRow(Optional ByVal tocheck As Boolean = False)
        Dim i As Integer
        'ChgByPrg = True
        If grdVoucher.RowCount > 0 And Not tocheck Then
            If Val(grdVoucher.Item(ConstItemID, i).Value) = 0 Then Exit Sub
        End If
        If Not grdroomAdded.CurrentRow Is Nothing Then
            If grdRooms.Item(Conststatus, grdroomAdded.CurrentRow.Index).Value <> "Active" Then
                MsgBox("Room not active! You cannot add services", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
        End If
        With grdVoucher
            activecontrolname = "grdVoucher"
            i = .RowCount '- 1
            .Rows.Add(1)
            .Item(ConstSlNo, i).Value = i + 1
            .Item(ConstBarcode, i).Value = ""
            .Item(ConstItemCode, i).Value = ""
            .Item(ConstDescr, i).Value = ""
            .Item(ConstUnit, i).Value = ""
            .Item(ConstQty, i).Value = Format(0, numFormat)
            .Item(ConstUPrice, i).Value = Format(0, numFormat)
            .Item(ConstDisP, i).Value = Format(0, numFormat)
            .Item(ConstDisAmt, i).Value = Format(0, numFormat)
            .Item(ConstTaxP, i).Value = Format(0, numFormat)
            .Item(ConstTaxAmt, i).Value = Format(0, numFormat)
            .Item(ConstLTotal, i).Value = Format(0, numFormat) '(.Item(ConstQty, i).Value * DR!unitPrice) - (.Item(ConstQty, i).Value * Val(DR!DiscountVal)) + (.Item(ConstQty, i).Value * Val(DR!TaxVal))
            .Item(ConstNUPrice, i).Value = Format(0, numFormat) 'Val(DR!unitPrice) - Val(DR!DiscountVal) + Val(DR!TaxVal)
            .Item(ConstItemID, i).Value = 0
            .Item(ConstImpJobChildTbID, i).Value = 0
            .Item(ConstSerialNo, i).Value = ""
            .Item(ConstPMult, i).Value = "1"
            .Item(ConstPFraction, i).Value = "2"
            .Item(ConstImpDocId, i).Value = ""
            .Item(ConstImpLnId, i).Value = ""
            .Item(ConstLrow, i).Value = i + 1
            '.Item(ConstActualPrice, i).Value = 0 ' DR!unitPrice
            '.ClearSelection()
            '.Select()
            '.Rows(i).Selected = True
            .CurrentCell = .Item(ConstItemCode, i)
            chgbyprg = True
            .BeginEdit(True)
            chgbyprg = False
            chgItm = False
        End With
        calculate()
        reArrangeNo()
    End Sub
    Private Sub calculate(Optional ByVal bFrmCalcOthCost As Boolean = False)
        Dim totQty As Double
        Dim totItm As Double
        Dim totTax As Double
        Dim totLnDis As Double
        Dim totAmt As Double
        Dim i As Integer
        If Not dtTax Is Nothing Then
            For j = 0 To dtTax.Rows.Count - 1
                dtTax(j)("Amount") = 0
            Next
        End If

        With grdVoucher
            For i = 0 To .Rows.Count - 1
                If Val(.Item(ConstItemID, i).Value) = 0 Then GoTo nxt
                .Item(ConstSlNo, i).Value = i + 1
                totQty = totQty + IIf(Val(.Item(ConstQty, i).Value) > 0, Val(.Item(ConstQty, i).Value), 0)
                If Val(.Item(ConstTaxP, i).Value & "") = 0 Then
                    .Item(ConstTaxP, i).Value = 0
                End If
                If Val(.Item(ConstActualPrice, i).Value & "") = 0 Then
                    .Item(ConstActualPrice, i).Value = 0
                End If
                If Val(.Item(ConstQty, i).Value & "") = 0 Then
                    .Item(ConstQty, i).Value = 0
                End If

                .Item(constItmTot, i).Value = Format((.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value), numFormat)
                .Item(ConstLTotal, i).Value = Format((.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value) + CDbl(.Item(ConstTaxAmt, i).Value), numFormat)
                .Item(ConstNUPrice, i).Value = CDbl(.Item(ConstActualPrice, i).Value) - (CDbl(.Item(ConstDisAmt, i).Value) / CDbl(.Item(ConstQty, i).Value)) + (CDbl(.Item(ConstTaxAmt, i).Value) / CDbl(.Item(ConstQty, i).Value)) - Val(.Item(ConstDiscOther, i).Value)
                If EnableGST Then
                    getGSTDetails(Trim(.Item(ConstBarcode, i).Value & ""), i, True)
                    If Val(.Item(ConstSGSTAmt, i).Value & "") = 0 Then .Item(ConstSGSTAmt, i).Value = 0
                    If Val(.Item(ConstCGSTAmt, i).Value & "") = 0 Then .Item(ConstCGSTAmt, i).Value = 0
                    .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstCGSTAmt, i).Value) + CDbl(.Item(ConstSGSTAmt, i).Value), numFormat)
                    totTax = totTax + CDbl(.Item(ConstCGSTAmt, i).Value) + CDbl(.Item(ConstSGSTAmt, i).Value)
                Else
                    .Item(ConstTaxAmt, i).Value = Format(((CDbl(.Item(ConstActualPrice, i).Value) * .Item(ConstTaxP, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value), numFormat)
                    totTax = totTax + .Item(ConstTaxAmt, i).Value
                End If

                totLnDis = totLnDis + .Item(ConstDisAmt, i).Value
                totAmt = totAmt + (.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value)
                totItm = totItm + (.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value)
                If ShowTaxOnInventory Then
                    For j = 0 To dtTax.Rows.Count - 1
                        If Val(.Item(ConstTaxP, i).Value) = dtTax(j)("vat") Then
                            dtTax(j)("Amount") = CDbl(dtTax(j)("Amount")) + CDbl(grdVoucher.Item(ConstTaxAmt, i).Value)
                        End If
                    Next
                End If
nxt:
            Next
            If EnableGST Then
                CalculateGST()
            End If
            totAmt = totAmt + totTax
            lblserviceGridTotal.Text = Format(totAmt, numFormat)
            chgAmt = False
        End With
    End Sub

    Private Sub getGSTDetails(ByVal hsncode As String, ByVal i As Integer, ByVal calculatefromGrid As Boolean)
        Dim dt As DataTable
        With grdVoucher
            If Not calculatefromGrid Then
                dt = _objcmnbLayer._fldDatatable("SELECT * FROM GSTTb WHERE HSNCODE='" & hsncode & "'")
                If dt.Rows.Count > 0 Then
                    .Item(ConstCGSTP, i).Value = Format(IIf(IsDBNull(dt(0)("CGST")), 0, CDbl(dt(0)("CGST"))), numFormat)
                    .Item(ConstSGSTP, i).Value = Format(IIf(IsDBNull(dt(0)("SGST")), 0, CDbl(dt(0)("SGST"))), numFormat)
                    .Item(ConstIGSTP, i).Value = Format(IIf(IsDBNull(dt(0)("IGST")), 0, CDbl(dt(0)("IGST"))), numFormat)
                Else
                    .Item(ConstCGSTP, i).Value = Format(0, numFormat)
                    .Item(ConstSGSTP, i).Value = Format(0, numFormat)
                    .Item(ConstIGSTP, i).Value = Format(0, numFormat)
                End If
            End If
            Dim actualPrice As Double
            Dim discountOther As Double
            discountOther = CDbl(.Item(ConstDiscOther, i).Value) * CDbl(.Item(ConstQty, i).Value)
            actualPrice = (CDbl(.Item(ConstActualPrice, i).Value) * CDbl(.Item(ConstQty, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value) - discountOther
            actualPrice = Format(actualPrice, numFormat)
            .Item(ConstCGSTAmt, i).Value = (actualPrice * .Item(ConstCGSTP, i).Value) / 100
            .Item(ConstSGSTAmt, i).Value = (actualPrice * .Item(ConstSGSTP, i).Value) / 100
            .Item(ConstIGSTAmt, i).Value = (actualPrice * .Item(ConstIGSTP, i).Value) / 100
            .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstIGSTAmt, i).Value), numFormat)
            .Item(ConstTaxP, i).Value = .Item(ConstIGSTP, i).Value
        End With
    End Sub
    Private Sub CalculateGST()
        Dim i As Integer
        Dim dtHSN As DataTable
        Dim dtrow As DataRow
        Dim slno As Integer
        dtTax.Rows.Clear()
        With grdVoucher
            Dim _qurey As EnumerableRowCollection(Of DataRow)
            For i = 0 To .RowCount - 1
                slno = 0
                _qurey = From data In dtGST.AsEnumerable() Where data("HSNCode") = Trim(.Item(ConstBarcode, i).Value & "") Select data
                If _qurey.Count > 0 Then
                    dtHSN = _qurey.CopyToDataTable
                    If Val(txtaddress.Tag) = 0 Then
                        'adding CSGT Amount****
                        Dim a = (From data In dtTax.AsEnumerable() Where data("ACid") = dtHSN(0)("CGSTCAc") Select data("slno"))
                        For Each itm In a
                            Try
                                slno = itm
                            Catch ex As Exception
                                MsgBox(ex.Message)
                            End Try
                        Next
                        If slno = 0 Then
                            dtrow = dtTax.NewRow
                            dtrow("slno") = dtTax.Rows.Count + 1
                            dtrow("AccountName") = dtHSN(0)("CGSTCAname")
                            dtrow("ACid") = dtHSN(0)("CGSTCAc")
                            dtrow("Amount") = CDbl(.Item(ConstCGSTAmt, i).Value)
                            dtTax.Rows.Add(dtrow)
                        Else
                            dtTax(slno - 1)("Amount") = CDbl(dtTax(slno - 1)("Amount")) + CDbl(.Item(ConstCGSTAmt, i).Value)
                        End If
                        'adding SSGT Amount****
                        a = (From data In dtTax.AsEnumerable() Where data("ACid") = dtHSN(0)("SGSTCAc") Select data("slno"))
                        slno = 0
                        For Each itm In a
                            Try
                                slno = itm
                            Catch ex As Exception
                                MsgBox(ex.Message)
                            End Try

                        Next
                        If slno = 0 Then
                            dtrow = dtTax.NewRow
                            dtrow("slno") = dtTax.Rows.Count + 1
                            dtrow("AccountName") = dtHSN(0)("SGSTCAname")
                            dtrow("ACid") = dtHSN(0)("SGSTCAc")
                            dtrow("Amount") = CDbl(.Item(ConstSGSTAmt, i).Value)
                            dtTax.Rows.Add(dtrow)
                        Else
                            dtTax(slno - 1)("Amount") = CDbl(dtTax(slno - 1)("Amount")) + CDbl(.Item(ConstSGSTAmt, i).Value)
                        End If
                    Else
                        'adding ISGT Amount****
                        Dim a = (From data In dtTax.AsEnumerable() Where data("ACid") = dtHSN(0)("IGSTCAc") Select data("slno"))
                        slno = 0
                        For Each itm In a
                            slno = itm
                        Next
                        If slno = 0 Then
                            dtrow = dtTax.NewRow
                            dtrow("slno") = dtTax.Rows.Count + 1
                            dtrow("AccountName") = dtHSN(0)("IGSTCAname")
                            dtrow("ACid") = dtHSN(0)("IGSTCAc")
                            dtrow("Amount") = CDbl(.Item(ConstIGSTAmt, i).Value)
                            dtTax.Rows.Add(dtrow)
                        Else
                            dtTax(slno - 1)("Amount") = CDbl(dtTax(slno - 1)("Amount")) + CDbl(.Item(ConstIGSTAmt, i).Value)
                        End If
                    End If
                End If
            Next
        End With
    End Sub
    Private Sub reArrangeNo()
        Dim r As Integer
        Dim i As Integer
        chgbyprg = True
        i = 0
        With grdVoucher
            For r = 0 To .Rows.Count - 1 '- 1
                If CStr(.Item(ConstSlNo, r).Value) <> "M" And CStr(.Item(ConstSlNo, r).Value) <> "L" Then
                    i = i + 1
                    .Item(ConstSlNo, r).Value = i
                End If
            Next r
        End With
        chgbyprg = False
    End Sub

    Private Sub grdVoucher_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellClick
        chgbyprg = True
        With grdVoucher
            If Val(grdVoucher.Item(0, grdVoucher.CurrentCell.RowIndex).Value) = 0 And e.ColumnIndex <> 3 Then
                grdVoucher.CurrentCell.ReadOnly = True
            ElseIf e.ColumnIndex <> ConstSlNo And e.ColumnIndex <> constItmTot And e.ColumnIndex <> ConstUnit And e.ColumnIndex <> ConstLTotal And e.ColumnIndex <> ConstBarcode And e.ColumnIndex <> ConstTaxAmt And e.ColumnIndex <> ConstStartReading Then
                If e.ColumnIndex = ConstTaxP And EnableGST Then
                    grdVoucher.CurrentCell.ReadOnly = True
                ElseIf e.ColumnIndex = ConstQty And grdVoucher.Item(ConstMeterCode, grdVoucher.CurrentCell.RowIndex).Value <> "" Then
                    grdVoucher.CurrentCell.ReadOnly = True
                ElseIf e.ColumnIndex = ConstQty And enableWoodSale Then
                    grdVoucher.CurrentCell.ReadOnly = True
                Else
                    grdVoucher.CurrentCell.ReadOnly = False
                End If
            Else
                grdVoucher.CurrentCell.ReadOnly = True
            End If
        End With
        grdBeginEdit()
    End Sub

    Private Sub grdVoucher_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdVoucher.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If grdVoucher.RowCount = 0 Then Exit Sub
                If SrchText = "" And grdVoucher.CurrentCell.ColumnIndex = ConstItemCode Then
                    GoTo nxt
                End If
                If FindNextCell(grdVoucher, grdVoucher.CurrentCell.RowIndex, grdVoucher.CurrentCell.ColumnIndex + 1) Or enableNextlineonItemcode Then
                    AddRow()
                End If
nxt:
                plsrch.Visible = False
                grdBeginEdit()

            ElseIf e.KeyCode = Keys.Up Or e.KeyCode = Keys.PageUp Then
                If grdVoucher.RowCount = 0 Then Exit Sub
                If plsrch.Visible Then
                    doSelect(0)
                    Exit Sub
                End If
            ElseIf e.KeyCode = Keys.Down Or e.KeyCode = Keys.PageDown Then
                If grdVoucher.RowCount = 0 Then Exit Sub
                If plsrch.Visible Then
                    doSelect(1)
                    Exit Sub
                End If
            ElseIf e.KeyCode = Keys.F2 Then
                If plsrch.Visible Then plsrch.Visible = False
                If grdVoucher.RowCount = 0 Then Exit Sub
                Select Case grdVoucher.CurrentCell.ColumnIndex
                    Case ConstItemCode
                        If plsrch.Visible Then plsrch.Visible = False
                        fProductEnquiry = New ItmEnqry
                        fProductEnquiry.hideRoom = True
                        fProductEnquiry.ShowDialog()
                End Select
            ElseIf e.KeyCode = Keys.Escape Then
                plsrch.Visible = False
            ElseIf e.KeyCode = Keys.F3 Then
                AddRow()
            ElseIf e.KeyCode = Keys.F4 Then
                RemoveRow()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub RemoveRow()
        If grdVoucher.RowCount > 0 Then
            If MsgBox("You Are Going to Remove the Row." & vbCrLf & "Are You Sure ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then
                Exit Sub
            End If
            With grdVoucher
                .Rows.RemoveAt(.CurrentRow.Index)
                .ClearSelection()
                calculate()
            End With
            reArrangeNo()
        End If
        chgservice = True
    End Sub

    Private Sub fProductEnquiry_getSelected(ByVal ItemcODE As String, ByVal isacc As Integer) Handles fProductEnquiry.getSelected
        chgbyprg = True
        fProductEnquiry.Close()
        SrchText = ItemcODE
        grdVoucher.Item(ConstItemCode, grdVoucher.CurrentRow.Index).Value = ItemcODE
        chgItm = True
        Valid(grdVoucher.CurrentRow.Index, ConstItemCode)
        grdVoucher.CurrentCell = grdVoucher.Item(3, grdVoucher.CurrentRow.Index)
        grdBeginEdit()
        chgbyprg = False
    End Sub
    Private Sub Valid(ByVal RowIndex As Integer, ByVal ColIndex As Integer)
        With grdVoucher
            Select Case ColIndex
                Case ConstBarcode, ConstItemCode
                    If Not chgItm Then Exit Sub
                    If Trim(.Item(ColIndex, RowIndex).Value) = "" Then Exit Sub
                    Dim dtItms As DataTable
                    Dim found As Boolean
                    dtItms = getItmDtls(3, SrchText, True)
                    If dtItms.Rows.Count > 0 Then
                        found = True
                        AddDetails(dtItms)
                    End If
                    chgItm = False
                    If Not found Then
                        .Item(ConstBarcode, RowIndex).Value = ""
                        .Item(ConstItemCode, RowIndex).Value = ""
                        .Item(ConstImpJobChildTbID, RowIndex).Value = ""
                        .Item(ConstItemID, RowIndex).Value = ""
                        .Item(ConstUnit, RowIndex).Value = ""
                        .Item(ConstSerialNo, RowIndex).Value = ""
                        .Item(ConstPMult, RowIndex).Value = "1"
                        .Item(ConstPFraction, RowIndex).Value = "2"
                        .Item(ConstImpDocId, RowIndex).Value = ""
                        .Item(ConstImpLnId, RowIndex).Value = ""
                        chgItm = False
                    End If

                Case ConstQty

                    If chgAmt Then
                        If AllowUnitDiscountEntryOnInventory Then
                            .Item(ConstDisAmt, RowIndex).Value = Format(((CDbl(.Item(ConstActualPrice, RowIndex).Value) * Val(.Item(ConstDisP, RowIndex).Value)) / 100) * CDbl(.Item(ConstQty, RowIndex).Value), numFormat)
                        End If

                        If EnableGST Then getGSTDetails(Trim(.Item(ConstBarcode, RowIndex).Value & ""), RowIndex, True)
                        calculate()
                    End If
                Case ConstUPrice
                    If chgAmt Then
                        If Format(.Item(ConstActualPrice, RowIndex).Value, numFormat) <> Format(CDbl(.Item(ConstUPrice, RowIndex).Value), numFormat) Then
                            .Item(ConstActualPrice, RowIndex).Value = CDbl(.Item(ConstUPrice, RowIndex).Value)
                        End If
                        If AllowUnitDiscountEntryOnInventory Then
                            .Item(ConstDisAmt, RowIndex).Value = Format(((CDbl(.Item(ConstActualPrice, RowIndex).Value) * Val(.Item(ConstDisP, RowIndex).Value)) / 100) * CDbl(.Item(ConstQty, RowIndex).Value), numFormat)
                        End If
                        If EnableGST Then getGSTDetails(Trim(.Item(ConstBarcode, RowIndex).Value & ""), RowIndex, True)
                        calculate()
                    End If
                Case ConstDisP
                    If chgAmt Then
                        .Item(ConstDisAmt, RowIndex).Value = Format(((CDbl(.Item(ConstActualPrice, RowIndex).Value) * Val(.Item(ConstDisP, RowIndex).Value)) / 100) * CDbl(.Item(ConstQty, RowIndex).Value), numFormat)
                        If EnableGST Then getGSTDetails(Trim(.Item(ConstBarcode, RowIndex).Value & ""), RowIndex, True)
                        calculate()
                    End If
                Case ConstDisAmt
                    If chgAmt Then
                        Dim unitDiscount As Double
                        unitDiscount = CDbl(.Item(ConstDisAmt, RowIndex).Value) / CDbl(.Item(ConstQty, RowIndex).Value)
                        .Item(ConstDisP, RowIndex).Value = Format((unitDiscount * 100) / CDbl(.Item(ConstActualPrice, RowIndex).Value), numFormat)
                        If EnableGST Then getGSTDetails(Trim(.Item(ConstBarcode, RowIndex).Value & ""), RowIndex, True)
                        calculate()
                    End If
                Case ConstTaxAmt
                    If chgAmt Then
                        calculate()
                    End If
                Case ConstTaxP
                    If chgAmt Then
                        calculate()
                    End If
                Case Else
            End Select
        End With
        chgAmt = False
    End Sub
    Private Sub AddDetails(ByVal DR As DataTable)
        Dim i As Integer
        Dim PMult As Double
        With grdVoucher
            chgbyprg = True
            PMult = 1
            i = .CurrentRow.Index ' .RowCount - 1
            .Item(ConstSlNo, i).Value = i + 1
            .Item(ConstItemCode, i).Value = IIf(IsDBNull(DR(0)("Item Code")), Trim(DR(0)("Item Code") & ""), DR(0)("Item Code"))
            .Item(ConstDescr, i).Value = DR(0)("Description")
            If EnableGST Then
                .Item(ConstBarcode, i).Value = DR(0)("HSNCode") 'hsncode
            Else
                .Item(ConstBarcode, i).Value = ""
            End If
            .Item(ConstQty, i).Value = Format(1, numFormat) 'IIf(IsReturn, -1, 1)
            .Item(ConstItemID, i).Value = DR(0)("ItemId")
            .Item(ConstPMult, i).Value = 1 'IIf(IsNull(sRs(0)("PFraCount), "2", sRs(0)("PFraCount)

            If DR(0)("ItemCategory") = "Comment" Then
                .Item(ConstSlNo, i).Value = "M"
                .Item(ConstB, i).Value = 0
                .Item(ConstUnit, i).Value = ""
                .Item(ConstSerialNo, i).Value = ""
                .Item(ConstPMult, i).Value = "1"
                .Item(ConstPFraction, i).Value = "2"
                .Item(ConstImpDocId, i).Value = ""
                .Item(ConstImpLnId, i).Value = ""
            Else
                onceChgFld = (CStr(.Item(ConstSlNo, i).Value) = "M" Or CStr(.Item(ConstSlNo, i).Value) = "L")
                If onceChgFld Then
                    .Item(ConstSlNo, i).Value = ""
                    .Item(ConstBarcode, i).Value = ""
                    .Item(ConstItemCode, i).Value = ""
                End If
            End If
            .Item(ConstB, .CurrentCell.RowIndex).Value = "B"
            .Item(ConstPFraction, i).Value = IIf(IsDBNull(DR(0)("FraCount")), "2", DR(0)("FraCount"))
            .Item(ConstUnit, i).Value = DR(0)("Unit")
            If CDbl(.Item(ConstUPrice, i).Value) = 0 Or chgItm Then
                If chkwS.Checked Then
                    .Item(ConstActualPrice, i).Value = DR(0)("UnitPriceWS")
                Else
                    .Item(ConstActualPrice, i).Value = DR(0)("UnitPrice")
                End If
                .Item(ConstUPrice, i).Value = Format(CDbl(.Item(ConstActualPrice, i).Value), numFormat)
            End If
            If Not IsDBNull(DR(0)("isSerialNo")) Then
                .Item(ConstIsSerial, i).Value = IIf(DR(0)("isSerialNo"), 1, 0)
            Else
                .Item(ConstIsSerial, i).Value = 0
            End If
            'If Val(DR(0)("vat") & "") = 0 Then DR(0)("vat") = 0
            '.Item(ConstTaxP, i).Value = Format(IIf(IsDBNull(DR(0)("vat")), 0, CDbl(DR(0)("vat"))), numFormat)
            '.Item(ConstTaxAmt, i).Value = ((CDbl(.Item(ConstActualPrice, i).Value) * .Item(ConstTaxP, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value)
            If ShowTaxOnInventory Then
                If Val(DR(0)("vat") & "") = 0 Then
                    DR(0)("vat") = 0
                End If
                .Item(ConstTaxP, i).Value = Format(IIf(IsDBNull(DR(0)("vat")), 0, CDbl(DR(0)("vat"))), numFormat)
                .Item(ConstTaxAmt, i).Value = ((CDbl(.Item(ConstActualPrice, i).Value) * .Item(ConstTaxP, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value)
            ElseIf EnableGST Then
                getGSTDetails(Trim(.Item(ConstBarcode, i).Value & ""), i, False)
            End If
            .Item(ConstLTotal, i).Value = Format(.Item(ConstQty, i).Value * CDbl(.Item(ConstUPrice, i).Value), numFormat)
            .Item(ConstNUPrice, i).Value = Format(CDbl(.Item(ConstUPrice, i).Value), numFormat)
            .Item(ConstQty, i).Value = Format(CDbl(.Item(ConstQty, i).Value), "#,##0" & IIf(Val(.Item(ConstPFraction, i).Value) = 0, "", "." & Strings.StrDup(CByte(.Item(ConstPFraction, i).Value), "0")))
            .Item(ConstImpDocId, i).Value = ""
            .Item(ConstImpLnId, i).Value = ""
            chgAmt = True
            chgItm = False
            .ClearSelection()
        End With
        calculate()
        chgbyprg = False
    End Sub
    Private Sub grdBeginEdit()
        chgbyprg = True
        grdVoucher.BeginEdit(True)
        chgbyprg = False
    End Sub
    Private Sub doSelect(ByVal Mup As Integer)
        Try
            chgbyprg = True
            Dim ItmFlds() As String
            If plsrch.Visible = False Then Exit Sub
            If Mup = 0 Then 'UP
                ItmFlds = MoveUpPl(grdSrch, _srchIndexId, strGridSrchString)
            ElseIf Mup = 1 Then 'Down
                ItmFlds = MoveDownPl(grdSrch, _srchIndexId, strGridSrchString)
            Else
                ItmFlds = SelectItmPanel(grdSrch)
            End If
            If strGridSrchString = "" And Mup = 2 Then SrchText = "" : GoTo Nxt
            Select Case _srchTxtId
                Case 1
                    grdVoucher.Item(ConstItemCode, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(0) IsNot Nothing, ItmFlds(0), "")
                    'grdVoucher.Item(ConstBarcode, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(1) IsNot Nothing, ItmFlds(1), "")
                    grdVoucher.Item(ConstDescr, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(2) IsNot Nothing, ItmFlds(1), "")
                    SrchText = IIf(ItmFlds(0) IsNot Nothing, ItmFlds(0), strGridSrchString)
            End Select
nxt:
            chgbyprg = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnaddservice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddservice.Click
        AddRow()
        chgservice = True
        chgPost = True

    End Sub

    Private Sub btnremoveservice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnremoveservice.Click
        If Val(btnremoveservice.Tag) = 0 Then
            MsgBox("This user do not have permission to perform this action", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If Not grdroomAdded.CurrentRow Is Nothing Then
            If grdRooms.Item(Conststatus, grdroomAdded.CurrentRow.Index).Value <> "Active" Then
                MsgBox("Room not active! You cannot remove services", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
        End If
        RemoveRow()
        chgPost = True
    End Sub

    Private Sub grdVoucher_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellDoubleClick
        Dim dt As DataTable = Nothing
        With grdVoucher
            If e.ColumnIndex = ConstSlNo Then
                .Item(ConstSlNo, .CurrentCell.RowIndex).Value = IIf(Val(.Item(ConstSlNo, .CurrentCell.RowIndex).Value) = 0, "", "M")
                reArrangeNo()
            End If
        End With
    End Sub

    Private Sub grdVoucher_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellEndEdit
        Dim col As Integer = e.ColumnIndex
        Dim ndec1 As Integer
        chgbyprg = True
        If col = ConstQty Or col = ConstUPrice Or col = ConstDisAmt Or col = ConstLTotal Or col = ConstTaxP Or col = ConstDisP Or col = ConstEndReading Or col = ConstWoodDiscQty Or col = ConstWoodQty Then
            If col = ConstQty Or col = ConstEndReading Or col = ConstWoodDiscQty Or col = ConstWoodQty Then
                ndec1 = grdVoucher.Item(ConstPFraction, grdVoucher.CurrentRow.Index).Value
            Else
                ndec1 = 2
            End If
            If Val(grdVoucher.Item(col, e.RowIndex).Value) = 0 Then grdVoucher.Item(col, e.RowIndex).Value = 0
            grdVoucher.Item(col, e.RowIndex).Value = Format(Convert.ToDecimal(grdVoucher.Item(col, e.RowIndex).Value), "#,##0" & IIf(ndec1 = 0, "", "." & Strings.StrDup(ndec1, "0")))
        End If
        chgbyprg = False
    End Sub

    Private Sub grdVoucher_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellValidated
        Valid(e.RowIndex, e.ColumnIndex)
    End Sub

    Private Sub grdVoucher_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellValueChanged
        If chgbyprg Then Exit Sub
        'ChgByPrg = True
        With grdVoucher
            Dim i As Integer = e.RowIndex
            Select Case e.ColumnIndex
                Case ConstItemCode, ConstBarcode
                    chgItm = True
                Case ConstQty, ConstTaxP, ConstDisAmt, ConstDisP, ConstWoodDiscQty, ConstWoodQty
                    chgAmt = True
                Case ConstLTotal
                    If Val(grdVoucher.Item(ConstQty, i).Value) > 0 Then
                        If Val(grdVoucher.Item(ConstDisAmt, i).Value) = 0 And CDbl(grdVoucher.Item(ConstTaxAmt, i).Value) = 0 Then 'Not AllowUnitDiscountEntryOnInventory And Not ShowTaxOnInventory And
                            chgbyprg = True
                            grdVoucher.Item(ConstUPrice, i).Value = Format(CDbl(grdVoucher.Item(ConstLTotal, i).Value) / grdVoucher.Item(ConstQty, i).Value, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) 'IIf(IsReturn, -1, 1)
                            grdVoucher.Item(ConstActualPrice, i).Value = CDbl(grdVoucher.Item(ConstLTotal, i).Value) / grdVoucher.Item(ConstQty, i).Value
                            calculate()
                            chgbyprg = False
                        End If
                    End If
                    chgAmt = True
                Case ConstDisAmt
                    chgAmt = True
                Case ConstUPrice
                    chgUprice = True
                    chgAmt = True
                    calculateTaxFromUnitPrice(e.RowIndex)
            End Select
        End With
        chgservice = True
        chgPost = True
    End Sub
    Private Sub calculateTaxFromUnitPrice(ByVal i As Integer)
        If chgUprice And chkcal.Checked Then
            chgbyprg = True
            With grdVoucher
                .Item(ConstActualPrice, i).Value = (CDbl(.Item(ConstUPrice, i).Value) * 100) / (CDbl(.Item(ConstTaxP, i).Value) + 100)
                .Item(ConstUPrice, i).Value = Format(.Item(ConstActualPrice, i).Value, numFormat)
            End With
        End If
        chgUprice = False
        chgbyprg = False
    End Sub

    Private Sub grdVoucher_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdVoucher.EditingControlShowing
        Dim Col As Integer
        Col = grdVoucher.CurrentCell.ColumnIndex
        If Col = ConstItemCode Or Col = ConstQty Then
            Dim tb As TextBox = CType(e.Control, TextBox)
            RemoveHandler tb.TextChanged, AddressOf Textbox_TextChanged
            AddHandler tb.TextChanged, AddressOf Textbox_TextChanged
        End If
        If Col = ConstItemCode Or Col = ConstSerialNo Or Col = ConstQty Then
            Dim tb As TextBox = CType(e.Control, TextBox)
            RemoveHandler tb.KeyPress, AddressOf Textbox_KeyPress
            AddHandler tb.KeyPress, AddressOf Textbox_KeyPress
        End If
    End Sub
    Private Sub Textbox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            Dim col As Integer
            col = grdVoucher.CurrentCell.ColumnIndex
            If col = ConstQty Or col = ConstNUPrice Or col = ConstLTotal Then
                If Not IsNumeric(e.KeyChar) And e.KeyChar <> "." And Not e.KeyChar = Convert.ToChar(Keys.Back) Then e.Handled = True
                If col = ConstQty Then
                    If grdVoucher.Item(ConstSerialNo, grdVoucher.CurrentRow.Index).Value <> "" Then
                        e.Handled = True
                    End If
                End If
            ElseIf col = ConstSerialNo Then
                e.Handled = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub Textbox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Try
            Dim col As Integer
            col = grdVoucher.CurrentCell.ColumnIndex
            Dim MyCtrl As TextBox = sender
            If col = ConstItemCode Or ConstDescr Then strGridSrchString = MyCtrl.Text
            If chgbyprg Then Exit Sub
            _srchOnce = False
            If col = ConstItemCode Then
                _srchTxtId = 1
                chgbyprg = True
                strGridSrchString = MyCtrl.Text
                ShowPanel()
                chgItm = True
                chgbyprg = False
                grdVoucher.Item(ConstqtyChg, grdVoucher.CurrentRow.Index).Value = "Chg"
            ElseIf col = ConstBarcode Then
                _srchTxtId = 2
                chgbyprg = True
                strGridSrchString = MyCtrl.Text
                ShowPanel()
                chgbyprg = False
            ElseIf col = ConstQty Then
                grdVoucher.Item(ConstqtyChg, grdVoucher.CurrentRow.Index).Value = "Chg"
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub ShowPanel()
        If Not _srchOnce Then
            chgbyprg = True
            Dim PopupLoc As Point
            Dim x As Integer = Me.Width - plsrch.Width - 100
            Dim y As Integer = Me.Height - plsrch.Height - 100
            PopupLoc = New Point(x, y)
            If plsrch.Visible = False Then
                plsrch.Location = PopupLoc
                plsrch.Visible = True
            End If
        End If
        SearchProductPanel(grdSrch, "[Item Code]", strGridSrchString, True, False, True)
        doSelect(2)
        _srchOnce = True
        chgbyprg = False
    End Sub

    Private Sub grdVoucher_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.GotFocus
        'If grdVoucher.RowCount = 0 Then
        '    AddRow()
        'End If
        activecontrolname = "grdVoucher"
    End Sub

    Private Sub grdVoucher_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.Leave
        activecontrolname = ""
    End Sub

    Private Sub picCloseProd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picCloseProd.Click
        plsrch.Visible = False
    End Sub

    Private Sub btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnload.Click
        ldJobdetails()
    End Sub

    Private Sub btnsaveService_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsaveService.Click
        If chkGrid() Then
            saveService()
            loadServices(grdroomAdded.CurrentRow.Index, False)
            calculateToalServiceValue()
        End If
    End Sub

    Private Sub grdroomAdded_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdroomAdded.CellValidated
        If chgServicevalidate Then Exit Sub
        If chgservice Then
            If MsgBox("Changes found in service! do you want change row?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                grdroomAdded.Tag = 1
            Else
                chgservice = False
            End If
        End If

    End Sub

    Private Sub grdroomAdded_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdroomAdded.RowEnter
        If Val(grdroomAdded.Tag) = 1 Then
            Timer2.Enabled = True
        Else
            If serviceRow <> e.RowIndex Then
                loadServices(e.RowIndex, True)
                serviceRow = e.RowIndex
            End If
        End If
        grdroomAdded.Tag = ""
        chgServicevalidate = False
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
        fillGrid()
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
                grpinvoice.Enabled = True
                btncheckout.Text = "Undo Check Out"
                btncheckout.BackColor = Color.IndianRed
                If userType Then
                    btncheckout.Tag = IIf(getRight(194, CurrentUser), 1, 0)
                Else
                    btncheckout.Tag = 1
                End If

            ElseIf .Item(Conststatus, RowIndex).Value = "Active" Then
                'rdoinvoice.Enabled = False
                'chkwS.Enabled = False
                grpinvoice.Enabled = False
                btncheckout.Text = "Check &Out"
                btncheckout.BackColor = Color.MediumSeaGreen
                If userType Then
                    btncheckout.Tag = IIf(getRight(193, CurrentUser), 1, 0)
                Else
                    btncheckout.Tag = 1
                End If

            End If
        End With
        calculateSingleRoomRent(RowIndex)
    End Sub

    Private Sub btninvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btninvoice.Click
        If userType Then
            btninvoice.Tag = IIf(getRight(86, CurrentUser), 1, 0)
        Else
            btninvoice.Tag = 1
        End If
        If Val(btninvoice.Tag) = 0 Then
            MsgBox("This user do not have permission to Create New Invoice", MsgBoxStyle.Exclamation, Nothing)
        ElseIf Val(txtjobcode.Tag) = 0 Then
            MsgBox("Invalid Job", MsgBoxStyle.Exclamation, Nothing)
        Else
            If grdinvList.RowCount > 0 Then
                If MsgBox("Invoice Found against this job, Do you want to create new Invoice?", MsgBoxStyle.Question & MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            End If

            Dim roomids As String = ""
            For i = 0 To grdRooms.RowCount - 1
                If grdRooms.Item(ConstTag, i).Value = "Y" Then
                    roomids = roomids & IIf(roomids = "", "", ",") & grdRooms.Item(ConstRoomid, i).Value
                End If
            Next
            If roomids = "" Then
                MsgBox("Tag atleast one room", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            If (Not fInvoice Is Nothing) Then
                fInvoice = Nothing
            End If
            fInvoice = New MFSalesInvoice
            fInvoice.MdiParent = fMainForm
            fInvoice.Show()
            'Dim amt As Double
            'If rdoserviceinv.Checked Then
            '    If chktotal.Checked Then
            '        amt = CDbl(lblserviceCharge.Text)
            '    Else
            '        amt = 0
            '    End If
            'Else
            '    If chkwS.Checked Then
            '        amt = CDbl(txtserviceCharge.Text)
            '    Else
            '        amt = 0
            '    End If
            'End If
            fInvoice.returnFromLodge(Val(txtjobcode.Tag))
            For i = 0 To grdRooms.RowCount - 1
                If grdRooms.Item(ConstTag, i).Value = "Y" Then
                    If rdoserviceinv.Checked Then
                        fInvoice.returnLodgeServices(Val(grdRooms.Item(ConstRoomid, i).Value), chktotal.Checked)
                    Else
                        fInvoice.returnLodgeRoomAndServices(Val(grdRooms.Item(ConstRoomid, i).Value), chkconsolidateservice.Checked, Not chkwS.Checked)
                    End If

                End If
            Next
            fInvoice.calculate()
            With fInvoice
                If CDbl(.lbltax.Text) = 0 Then .chktaxInv.Checked = False
            End With
        End If
    End Sub
    Private Sub fInvoice_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fInvoice.FormClosed
        fInvoice = Nothing
    End Sub

    Private Sub btncheckout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncheckout.Click
        If Val(btncheckout.Tag) = 0 Then
            MsgBox("This user do not have permission to perform this action", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If grdRooms.CurrentRow Is Nothing Then Exit Sub
        If btncheckout.Text = "Check &Out" Then
            Dim frm As New CheckOutFrm
            With grdRooms
                frm.lblroom.Tag = .Item(ConstRoomid, .CurrentRow.Index).Value
                frm.lblroom.Text = "ROOM : " & .Item(ConstItemCode, .CurrentRow.Index).Value
                frm.ShowDialog()
                .Item(Conststatus, .CurrentRow.Index).Value = "Closed"
                .Item(ConstCheckOut, .CurrentRow.Index).Value = frm.dtcheckout.Value
            End With
            btncheckout.Text = "Undo Check &Out"
        Else
            If MsgBox("Do you want undo checkout?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            _objcmnbLayer._saveDatawithOutParm("UPDATE LodgeRoomTb SET roomstatus=1 WHERE ldgroomid=" & Val(grdRooms.Item(ConstRoomid, grdRooms.CurrentRow.Index).Value))
            With grdRooms
                .Item(Conststatus, .CurrentRow.Index).Value = "Active"
                .Item(ConstCheckOut, .CurrentRow.Index).Value = ""
            End With
            btncheckout.Text = "Check &Out"
        End If
        calculateJobValue(1)
        roomRowEnter(grdRooms.CurrentRow.Index)
        chgPost = True
    End Sub

    Private Sub grdinvList_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdinvList.CellContentClick

    End Sub

    Private Sub grdinvList_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdinvList.DoubleClick
        If grdinvList.Rows.Count = 0 Then Exit Sub
        fMainForm.LoadIS(Val(grdinvList.Item(grdinvList.ColumnCount - 1, grdinvList.CurrentRow.Index).Value))
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
        'dt = _objcmnbLayer._fldDatatable("select * from LodgeSettingsTb")
        'Dim nonGstHsn As String = ""
        'If dt.Rows.Count > 0 Then
        '    nonGstHsn = Trim(dt(0)("taxablenonHsnCode") & "")
        'End If
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
            RptType = "LDLST"
        ElseIf TabControl2.SelectedIndex = 0 Then
            If txtprintjob.Text = "" Then
                MsgBox("Invalid Check In Number", MsgBoxStyle.Information)
                Exit Sub
            End If
            RptType = "LDG"
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
                sts = 2
            ElseIf rdoactive.Checked Then
                sts = 1
            Else
                sts = 0
            End If
            If rdonone.Checked Then
                tp = 0
            ElseIf rdocheckin.Checked Then
                tp = 1
            Else
                tp = 2
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
        If grdinvList.RowCount > 0 Then
            MsgBox("Invoice found! cannot remove the document", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If grdrvlist.RowCount > 0 Then
            MsgBox("RV found! cannot remove the document", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If MsgBox("You are going remove the document! are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        _objcmnbLayer._saveDatawithOutParm("DELETE FROM LodgeTb where jobid=" & Val(txtjobcode.Tag))
        _objcmnbLayer._saveDatawithOutParm("DELETE FROM JobTb where jobid=" & Val(txtjobcode.Tag))
        _objcmnbLayer._saveDatawithOutParm("update LodgeRoomTb set roomstatus=2 " & _
                                           "where ldgroomid in (select bookingRawid from LodgeRoomTb where roomLdgid=" & Val(txtjobcode.Tag) & ")")
        _objcmnbLayer._saveDatawithOutParm("DELETE FROM LodgeRoomTb where roomLdgid=" & Val(txtjobcode.Tag))
        _objcmnbLayer._saveDatawithOutParm("DELETE FROM LodgeServiceTb where lodgeid=" & Val(txtjobcode.Tag))
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

    Private Sub grdVoucher_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellContentClick

    End Sub

    Private Sub grdroomAdded_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdroomAdded.CellContentClick

    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Timer2.Enabled = False
        With grdroomAdded
            chgServicevalidate = True
            .CurrentCell = .Item(0, serviceRow)
            .Tag = ""
        End With
    End Sub

    Private Sub txtmaleGust_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtmaleGust.TextChanged, txtfemaleGust.TextChanged, txtkids.TextChanged
        If chgbyprgAmt Then Exit Sub
        chgPost = True
        txtnumberofGust.Text = Val(txtmaleGust.Text) + Val(txtfemaleGust.Text) + Val(txtkids.Text)
        btnupdate.Enabled = True
    End Sub

    Private Sub btnSlct_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSlct.Click
        If fSelectBooking Is Nothing Then
            fSelectBooking = New SelectBookingFrm
            fSelectBooking.ShowDialog()
        End If
    End Sub

    Private Sub fSelectBooking_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fSelectBooking.FormClosed
        fSelectBooking = Nothing
    End Sub

    Private Sub fSelectBooking_transferBooking(ByVal jobid As Long, ByVal roomids As String) Handles fSelectBooking.transferBooking
        chgbyprg = True
        Try
            Dim dt As DataTable
            _objJob = New clsJob
            With _objJob
                .Jobid = jobid
                .DateFrom = DateValue(cldrStartDate.Value)
                .DateTo = DateValue(cldrEnddate.Value)
                .custcode = 0
                .Dtype = "BKN"
                .Tp = 1
                dt = .returnLodge.Tables(0)
            End With
            If dt.Rows.Count > 0 Then
                txtbookRef.Text = dt(0)("Jobcode")
                txtbookRef.Tag = jobid
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
            loadRoomsFromBooking(True, jobid, roomids)
            fillGrid()
            calculateJobValue(3)
            Label27.Enabled = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
        chgbyprg = False
    End Sub
    Private Sub loadRoomsFromBooking(ByVal donotcalculate As Boolean, ByVal jobid As Long, ByVal roomids As String)
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT LodgeRoomTb.*,[Item Code],Description,INVITM.HSNCode,((isnull(rent,0)*(isnull(gst,0)+isnull(cess,0)))/100)+rent TaxPrice FROM LodgeRoomTb " & _
                                         "INNER JOIN INVITM ON LodgeRoomTb.roomItemid=invitm.itemid " & _
                                         "LEFT JOIN GSTTb ON GSTTb.HSNCode=InvItm.HSNCode " & _
                                         "where roomLdgid=" & jobid & " and ldgroomid in (" & roomids & ")")
        grdroomAdded.Rows.Clear()
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
                .Item(ConstRoomid, i).Value = 0
                .Item(ConstBookingRawid, i).Value = dt(i)("ldgroomid")
                If Val(dt(i)("roomstatus") & "") = 1 Then
                    .Item(Conststatus, i).Value = "Active"
                ElseIf Val(dt(i)("roomstatus") & "") = 0 Then
                    .Item(Conststatus, i).Value = "Closed"
                Else
                    .Item(Conststatus, i).Value = "Booked"
                End If

                If grdroomAdded.ColumnCount > 0 Then
                    grdroomAdded.Rows.Add()
                    grdroomAdded.Item(0, i).Value = dt(i)("Item Code")
                    grdroomAdded.Item(1, i).Value = dt(i)("ldgroomid")
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
End Class