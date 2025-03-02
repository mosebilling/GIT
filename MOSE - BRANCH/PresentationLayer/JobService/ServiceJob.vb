Public Class ServiceJob
#Region "Public variables"
    Public isModi As Boolean
    Public isfromExternal As Boolean
#End Region
#Region "Class Objects"
    Private _objJob As clsJob
    Private _objcmnbLayer As clsCommon_BL
    Private _objInv As New clsInvoice
    Private _objTr As New clsAccountTransaction
#End Region
#Region "NumericText"
    'numeric text
    Private idx As Integer
    Private str1 As String
    Private str2 As String
    Private str3 As String
    Private SelStart As Integer
    Private numCtrl As TextBox
#End Region
#Region "Form Objects"
    Private WithEvents fMList As Mlistfrm
    Private WithEvents fRptFormat As RptFormatfrm
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fDelivery As New JobDelivery
    Private WithEvents fInvoice As JobSalesInvoice
    Private WithEvents fCrtAcc As CreateAccNew
    Private WithEvents fProductEnquiry As ItmEnqry
    Private WithEvents fselectJob As JobEnqryFrm
#End Region
#Region "Private Variable"
    Private chgbyprg As Boolean
    Private _srchTxtId As Integer
    Private _srchOnce As Boolean
    Private MyActiveControl As New Object
    Private lstKey As Integer
    Private activecontrolname As String
    Private isset As Boolean
    Private PSAcc As Long
    Private ischgItm As Boolean
    Private SrchText As String
    Private _srchIndexId As Byte
    Private strGridSrchString As String
    Private chgItm As Boolean
    Private cessdate As Date
    Private chgAmt As Boolean
    Private chgUprice As Boolean
    Private NDec As Integer = 2
    Private FCRt As Double = 1
    Private PPerU As Single
    Private ischgJobcode As Boolean
#End Region
#Region "Const Variables"

    Private Const ConstImeino = 1
    Private Const ConstModel = 2
    Private Const ConstRemark = 3
    Private Const ConstBt = 4
    Private Const ConstCv = 5
    Private Const ConstBX = 6
    Private Const ConstMc = 7
    Private Const ConstPen = 8
    Private Const ConstComplaints = 9
    Private Const ConstObservation = 10
    Private Const ConstTechRemarks = 11
    Private Const ConstWithWarrenty = 12
    Private Const ConstCancelWarrenty = 13
    Private Const ConstJbImeiid = 14
    Private Const ConstDelivered = 15
#End Region
    Private Sub btnaccessories_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        AddAccessories.ShowDialog()
        'ldAccessories()
    End Sub

    Private Sub ServiceJob_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        dtpdate.Focus()
    End Sub

    Private Sub ServiceJob_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        
        If Not isModi Then
            'ldAccessories()
            AddNew()
            btndelete.Text = "Clear"
            If userType Then
                btnupdate.Tag = IIf(getRight(29, CurrentUser), 1, 0)
            Else
                btnupdate.Tag = 1

            End If
        Else
            btndelete.Text = "Delete"
            If userType Then
                btnupdate.Tag = IIf(getRight(30, CurrentUser), 1, 0)
                btndelete.Tag = IIf(getRight(31, CurrentUser), 1, 0)
                btndelivery.Tag = IIf(getRight(35, CurrentUser), 1, 0)
                btnjobclose.Tag = IIf(getRight(54, CurrentUser), 1, 0)
            Else
                btnupdate.Tag = 1
                btndelete.Tag = 1
                btndelivery.Tag = 1
                btnjobclose.Tag = 1
            End If
        End If
        cessdate = getCessDate()
        setImeiGrid()
        isset = False
        SetGridHead()
    End Sub
    Private Sub setImeiGrid()
        With grdImei
            If isset Then Exit Sub

            SetEntryGridProperty(grdImei)

            .ColumnHeadersHeight = 100
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersDefaultCellStyle.BackColor = Color.Olive
            .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.5!, System.Drawing.FontStyle.Regular)
            .RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.0!)
            .ColumnCount = 16

            .Columns(ConstSlNo).HeaderText = "SlNo"
            .Columns(ConstSlNo).Width = 30
            .Columns(ConstSlNo).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(ConstSlNo).Frozen = True
            .Columns(ConstSlNo).DefaultCellStyle.Format = "N0"
            .Columns(ConstSlNo).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstSlNo).ReadOnly = True

            .Columns(ConstImeino).HeaderText = "IMEI No./Item Name"
            .Columns(ConstImeino).Width = 150
            .Columns(ConstImeino).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstImeino).ReadOnly = False

            .Columns(ConstModel).HeaderText = "Model"
            .Columns(ConstModel).Width = 120
            .Columns(ConstModel).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstModel).ReadOnly = False

            .Columns(ConstRemark).HeaderText = "Remark"
            .Columns(ConstRemark).Width = 150
            .Columns(ConstRemark).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstRemark).ReadOnly = False


            .Columns(ConstBt).HeaderText = "BT"
            .Columns(ConstBt).Width = 45
            .Columns(ConstBt).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstBt).ReadOnly = True
            .Columns(ConstBt).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(ConstBt).Visible = False


            .Columns(ConstCv).HeaderText = "CV"
            .Columns(ConstCv).Width = 45
            .Columns(ConstCv).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstCv).ReadOnly = True
            .Columns(ConstCv).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(ConstCv).Visible = False


            .Columns(ConstBX).HeaderText = "BX"
            .Columns(ConstBX).Width = 45
            .Columns(ConstBX).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstBX).ReadOnly = True
            .Columns(ConstBX).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(ConstBX).Visible = False


            .Columns(ConstMc).HeaderText = "MC"
            .Columns(ConstMc).Width = 45
            .Columns(ConstMc).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstMc).ReadOnly = True
            .Columns(ConstMc).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(ConstMc).Visible = False

            .Columns(ConstPen).HeaderText = "Pen"
            .Columns(ConstPen).Width = 45
            .Columns(ConstPen).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstPen).ReadOnly = True
            .Columns(ConstPen).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            '.Columns(ConstPen).Visible = False



            .Columns(ConstComplaints).HeaderText = "Customer Complaints"
            .Columns(ConstComplaints).Width = 220
            .Columns(ConstComplaints).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstComplaints).ReadOnly = False
            '.Columns(ConstComplaints).Frozen = True

            .Columns(ConstObservation).HeaderText = "Observation"
            .Columns(ConstObservation).Width = 220
            .Columns(ConstObservation).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstObservation).ReadOnly = False

            .Columns(ConstTechRemarks).HeaderText = "Technician Remarks"
            .Columns(ConstTechRemarks).Width = 220
            .Columns(ConstTechRemarks).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstTechRemarks).ReadOnly = False

            .Columns(ConstWithWarrenty).HeaderText = "W?"
            .Columns(ConstWithWarrenty).Width = 45
            .Columns(ConstWithWarrenty).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstWithWarrenty).ReadOnly = True
            .Columns(ConstWithWarrenty).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns(ConstCancelWarrenty).HeaderText = "CW?"
            .Columns(ConstCancelWarrenty).Width = 45
            .Columns(ConstCancelWarrenty).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstCancelWarrenty).ReadOnly = True
            .Columns(ConstCancelWarrenty).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns(ConstJbImeiid).HeaderText = "id"
            .Columns(ConstJbImeiid).Width = 100
            .Columns(ConstJbImeiid).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstJbImeiid).Visible = False

            .Columns(ConstDelivered).HeaderText = "Delivered"
            .Columns(ConstDelivered).Width = 70
            .Columns(ConstDelivered).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstDelivered).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(ConstDelivered).ReadOnly = True
            .Columns(ConstDelivered).Visible = True
            isset = True
        End With
    End Sub
    Private Sub SetGridHead()
        chgbyprg = True
        With grdVoucher

            SetGridHeadEntryProperty(grdVoucher)
            .Columns(Constsman).Visible = False
            .Columns(ConstMeterCode).Visible = False
            .Columns(ConstStartReading).Visible = False
            .Columns(ConstEndReading).Visible = False
            .Columns(ConstSerialNo).Visible = False
            .Columns(ConstWarrentyExpiry).Visible = False
        End With
        chgbyprg = False
    End Sub
    Private Sub AddNew()

        numVchrNo.Text = GenerateNext(numVchrNo.Text)
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
            strCode = "JB"
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

    Private Sub numVchrNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles numVchrNo.KeyDown, txtJobname.KeyDown, txtEstAmt.KeyDown, txtcustomer.KeyDown, _
                                                                                                               txttechnician.KeyDown
        lstKey = e.KeyCode
        Dim myctrl As TextBox
        myctrl = sender
        If e.KeyCode = Keys.Enter Then
            If myctrl.Name = "numVchrNo" Then
                Dim dt As DataTable
                If isModi And ischgJobcode Then
                    dt = _objcmnbLayer._fldDatatable("Select jobid from JobTb where Jobcode='" & numVchrNo.Text & "'")
                    If dt.Rows.Count > 0 Then
                        ldRec(dt(0)("jobid"))
                    End If
                    ischgJobcode = False
                    End If
            End If
            SendKeys.Send("{TAB}")
        End If
        If myctrl.Name = "txtcustomer" Or myctrl.Name = "txtitm" Or myctrl.Name = "txtitemname" Then
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
            End If
        
        End If
    End Sub

    Private Sub numVchrNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numVchrNo.TextChanged
        If chgbyprg Then Exit Sub
        ischgJobcode = True
    End Sub

    Private Sub txttaxP_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtEstAmt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEstAmt.KeyPress, txtserviceCharge.KeyPress
        On Error Resume Next
        numCtrl = sender
        chgbyprg = True
        SelStart = numCtrl.SelectionStart
        If IsNumeric(e.KeyChar) Or e.KeyChar = "." Then
            If numCtrl.SelectionLength > 0 Then
                numCtrl.Text = Mid(numCtrl.Text, 1, SelStart) & Mid(numCtrl.Text, SelStart + numCtrl.SelectionLength + 1)
            End If
            idx = numCtrl.Text.IndexOf(".")
            If e.KeyChar <> "." Then
                If SelStart > idx Then
                    numCtrl.Text = Mid(numCtrl.Text, 1, SelStart) & Val(e.KeyChar) & Mid(numCtrl.Text, SelStart + 2)
                Else
                    numCtrl.Text = Mid(numCtrl.Text, 1, SelStart) & Val(e.KeyChar) & Mid(numCtrl.Text, SelStart + 1)
                End If
            End If
            idx = numCtrl.Text.IndexOf(".")
            If idx > 0 Then
                str1 = CDbl(Mid(numCtrl.Text, 1, idx))
                str2 = Mid(numCtrl.Text, idx + 1)
            End If
            If Len(Trim(str1)) > 10 And Len(numCtrl.Text) > 0 Then
                If Mid(Mid(Mid(numCtrl.Text, 1, idx), Len(Trim(str1)) + 1 - 10), 1, 1) = "," Then
                    str3 = Mid(Mid(numCtrl.Text, 1, idx), Len(Trim(str1)) + 2 - 10)
                End If
                numCtrl.Text = Mid(str1, Len(Trim(str1)) + 1 - 10) & str2
                SelStart = SelStart - 2
            Else
                str3 = ""
            End If
            idx = numCtrl.Text.IndexOf(".")
            If idx > 0 Then
                str1 = Mid(numCtrl.Text, 1, idx)
            Else
                str1 = numCtrl.Text
            End If
            numCtrl.Text = CDbl(numCtrl.Text)
            numCtrl.Text = Format(Val(numCtrl.Text), "#,##0.00")
            idx = numCtrl.Text.IndexOf(".")
            If idx > 0 Then
                str2 = Mid(numCtrl.Text, 1, idx)
            Else
                str2 = numCtrl.Text
            End If
            numCtrl.SelectionStart = SelStart + Len(str2) - IIf(str3 = "", Len(str1), Len(str3)) + 1
            'we assaigned formatted value to textbox so we not need it write it on again
            e.Handled = True
        Else
            If CInt(AscW(e.KeyChar)) = 8 Or CInt(AscW(e.KeyChar)) = 22 Then
                If CInt(AscW(e.KeyChar)) = 22 Then
                    If Not IsNumeric(Clipboard.GetText) Then
                        e.Handled = True
                    End If
                Else
                    e.Handled = False
                End If
            Else
                e.Handled = True
            End If
        End If
        chgbyprg = False
    End Sub

    Private Sub txtEstAmt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEstAmt.TextChanged

    End Sub

    Private Sub txtcustomer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcustomer.TextChanged, txttechnician.TextChanged
        If chgbyprg = True Then Exit Sub
        Dim myCtrl As Control = sender
        Select Case myCtrl.Name
            Case "txtcustomer"
                _srchTxtId = 1
            Case "txtitm"
                _srchTxtId = 2
            Case "txtitemname"
                _srchTxtId = 3
            Case "txttechnician"
                _srchTxtId = 4
        End Select
        _srchOnce = False
        ShowFmlist(sender)
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
            If _srchTxtId = 4 Then
                x = Me.Width - fMList.Width
            Else
                x = Me.Left + 100
            End If
            Dim y As Integer = Me.Height - fMList.Height - 10
            PopupLoc = New Point(x, y)
            If fMList.Visible = False Then
                Select Case _srchTxtId
                    Case 1
                        SetFmlist(fMList, 3)
                    Case 2
                        SetFmlist(fMList, 1)
                    Case 3
                        SetFmlist(fMList, 2)
                    Case 4
                        SetFmlist(fMList, 12)
                End Select
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
           
            Case 4   'Technician
                fMList.SearchIndex = 0
                fMList.SearchIndexDescr = 1
                fMList_doFocus()
                fMList.Search(txttechnician.Text)
                fMList.AssignList(txttechnician, lstKey, chgbyprg)
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
            Case 4
                txttechnician.Text = ItmFlds(0)
        End Select
        chgbyprg = False
    End Sub

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        QuickCust(True, "Customer")
    End Sub
    Public Sub QuickCust(Optional ByRef bOnlyOne As Boolean = False, Optional ByVal Grp As String = "Customer")
        fCrtAcc = New CreateAccNew
        With fCrtAcc
            .Condition = "GrpSetOn In ('" & Grp & "')"
            If Grp = "Customer" Then
                .iscust = True
            Else
                .iscust = False
            End If
            .bOnlyOne = bOnlyOne
            .ShowDialog()
        End With
    End Sub

    Private Sub txtcustomer_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcustomer.Validated
        If txtcustomer.Text = "" Then Exit Sub
        loadCustomerDetails(0)
        chgbyprg = False
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub verify()
        Dim dt As DataTable
        _objcmnbLayer = New clsCommon_BL
        dt = _objcmnbLayer._fldDatatable("Select Jobid from JobTb where jobcode ='" & numVchrNo.Text & "' and jobid<>" & Val(numVchrNo.Tag))
        If dt.Rows.Count > 0 Then
            MsgBox("Job code already exist", MsgBoxStyle.Exclamation)
            numVchrNo.Focus()
            Exit Sub
        End If
        'If txtDescription.Text = "" Then
        '    MsgBox("Invalid Description", MsgBoxStyle.Exclamation)
        '    txtDescription.Focus()
        '    Exit Sub
        'End If
        If Val(txtcustomer.Tag) = 0 Then
            MsgBox("Invalid Customer", MsgBoxStyle.Exclamation)
            txtcustomer.Focus()
            Exit Sub
        End If
        If grdImei.RowCount = 0 Then
            MsgBox("Invalid IMEI Number", MsgBoxStyle.Exclamation)
            btnadd.Focus()
            Exit Sub
        End If
        If txttechnician.Text = "" Then
            MsgBox("Invalid Technician", MsgBoxStyle.Exclamation)
            txttechnician.Focus()
            Exit Sub
        End If
        If MsgBox("Verification is succeeded. Do you like to file it?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        saveJob()
        txtprintjob.Text = numVchrNo.Text
        MsgBox("Service Invoice saved successfully", MsgBoxStyle.Information)
        If isModi Then
            numVchrNo.Text = ""
        End If
        'If isModi And isfromExternal Then Me.Close()
        'AddNew()
        'makeClear()
        ldRec(numVchrNo.Tag)
    End Sub
    Private Sub saveJob()
        _objJob = New clsJob
        With _objJob
            .Jobid = Val(numVchrNo.Tag)
            .jobcode = numVchrNo.Text
            .jobdate = DateValue(dtpdate.Value)
            .jobname = txtJobname.Text
            .JobDescription = txtDescription.Text
            .custcode = Val(txtcustomer.Tag)
            .EstimatedDate = DateValue(dtpestimatedDt.Value)
            If Val(txtEstAmt.Text) = 0 Then txtEstAmt.Text = 0
            .EstimatedAmt = CDbl(txtEstAmt.Text)
            .Technician = txttechnician.Tag
            .SIID = 0
            .RvId = 0
            If Val(txtserviceCharge.Text) = 0 Then txtserviceCharge.Text = 0
            .ServiceCost = CDbl(txtserviceCharge.Text)
            If Val(lblitemcost.Text) = 0 Then lblitemcost.Text = 0
            .ItemCost = CDbl(txttotalItemAmt.Text)
            .Userid = CurrentUser
            If Val(txtscost.Text) = 0 Then txtscost.Text = 0
            .LabourCost = CDbl(txtscost.Text)
            numVchrNo.Tag = .saveJob()
        End With
        'saveJobReceivedAccessories()
        saveJobImei()
        If grdVoucher.RowCount > 0 Then
            saveJobItems()
        Else
            deleteInventory()
        End If
    End Sub
    Private Sub deleteInventory()
        Dim itemidsdatatable As New DataTable
        If Val(numsto.Tag) = 0 Then Exit Sub
        Dim trdate As Date
        itemidsdatatable = _objcmnbLayer._fldDatatable("SELECT Itemid,TrDate,SerialNo from ItmInvTrTb " & _
                                                       "LEFT JOIN ItmInvCmnTb ON ItmInvTrTb.Trid=ItmInvCmnTb.Trid  " & _
                                                       "WHERE ItmInvTrTb.TrId =" & Val(numsto.Tag))
        trdate = DateValue(itemidsdatatable(0)("TrDate"))
        _objInv.TrId = Val(numsto.Tag)
        _objInv.TrType = "OUT"
        _objInv.deleteInventoryTransactions()
        For i = 0 To itemidsdatatable.Rows.Count - 1
            _objInv.ItemId = itemidsdatatable(i)("Itemid")
            _objInv.TrDate = DateValue(itemidsdatatable(i)("TrDate"))
            _objInv.setcostAverageOnModification(UsrBr)
        Next
        _objcmnbLayer._saveDatawithOutParm("delete from JobitemTb where jbid=" & Val(numVchrNo.Tag))
    End Sub
    Private Sub saveJobImei()
        _objJob = New clsJob
        Dim i As Integer

        With grdImei
            For i = 0 To .RowCount - 1
                _objJob.IMEI = .Item(ConstImeino, i).Value
                _objJob.Jobid = Val(numVchrNo.Tag)
                _objJob.Model = .Item(ConstModel, i).Value
                _objJob.bt = IIf(.Item(ConstBt, i).Value = "Y", 1, 0)
                _objJob.cv = IIf(.Item(ConstCv, i).Value = "Y", 1, 0)
                _objJob.bx = IIf(.Item(ConstBX, i).Value = "Y", 1, 0)
                _objJob.mc = IIf(.Item(ConstMc, i).Value = "Y", 1, 0)
                _objJob.Pen = IIf(.Item(ConstPen, i).Value = "Y", 1, 0)
                _objJob.JobDescription = .Item(ConstComplaints, i).Value
                _objJob.Observation = .Item(ConstObservation, i).Value
                _objJob.TechRemarks = .Item(ConstTechRemarks, i).Value
                _objJob.CWarrenty = IIf(.Item(ConstCancelWarrenty, i).Value = "Y", True, False)
                _objJob.withWarrenty = IIf(.Item(ConstWithWarrenty, i).Value = "Y", True, False)
                _objJob.jobimeiId = Val(.Item(ConstJbImeiid, i).Value)
                _objJob.Remark = .Item(ConstRemark, i).Value
                _objJob.saveJobImeiListTb()
            Next
            _objcmnbLayer._saveDatawithOutParm("delete from JobImeiListTb where jobid=" & Val(numVchrNo.Tag) & " and setremove=1")
        End With

    End Sub
    'Private Sub saveJobReceivedAccessories()
    '    _objcmnbLayer = New clsCommon_BL
    '    _objcmnbLayer._saveDatawithOutParm("DELETE FROM JobReceivedAccessoriesTb WHERE Jobid=" & Val(numVchrNo.Tag))
    '    For Each itemChecked In chklistAccessories.CheckedItems
    '        _objJob = New clsJob
    '        With _objJob
    '            .Jobid = Val(numVchrNo.Tag)
    '            .AccessoriesName = itemChecked.ToString
    '            .saveJobReceivedAccessoriesTb()
    '        End With
    '    Next
    'End Sub

    Private Sub setInvDetValue(ByVal Invid As Long, ByVal PPerU As Single, ByVal i As Integer)
        With grdVoucher

            _objInv.dtTrId = Invid
            _objInv.ItemId = IIf(.Item(ConstSlNo, i).Value.ToString <> "M", Val(.Item(ConstItemID, i).Value), 0)
            _objInv.Unit = .Item(ConstUnit, i).Value
            _objInv.TrQty = CDbl(.Item(ConstQty, i).Value) * PPerU
            _objInv.UnitCost = CDbl(.Item(ConstActualPrice, i).Value) * FCRt / PPerU 'CDbl(.TextMatrix(i, 10)) / PPerU

            _objInv.taxP = CDbl(grdVoucher.Item(ConstTaxP, i).Value)
            _objInv.taxAmt = (CDbl(grdVoucher.Item(ConstTaxAmt, i).Value) * FCRt) / CDbl(PPerU)
            _objInv.PFraction = PPerU
            If Val(.Item(ConstUnitOthCost, i).Value) = 0 Then .Item(ConstUnitOthCost, i).Value = 0
            _objInv.UnitOthCost = CDbl(.Item(ConstUnitOthCost, i).Value) * FCRt / PPerU
            _objInv.Method = .Item(ConstB, i).Value
            _objInv.UnitDiscount = Val(.Item(ConstDiscOther, i).Value) * FCRt / PPerU
            If Val(.Item(ConstDisAmt, i).Value & "") = 0 Then .Item(ConstDisAmt, i).Value = 0
            _objInv.ItemDiscount = CDbl(.Item(ConstDisAmt, i).Value) * FCRt / PPerU
            If Val(.Item(ConstDisP, i).Value) = 0 Then .Item(ConstDisP, i).Value = 0
            _objInv.DisP = CDbl(.Item(ConstDisP, i).Value) * FCRt / PPerU

            If Not IsDBNull(.Item(ConstDescr, i).Value) Then
                _objInv.IDescription = IIf(.Item(ConstSlNo, i).Value.ToString = "M", .Item(ConstDescr, i).Value, Trim(.Item(ConstDescr, i).Value))
            End If
            _objInv.SlNo = i + 1
            _objInv.TrTypeNo = getVouchernumber("IS")
            _objInv.TrDateNo = getDateNo(CDate(dtpdate.Value))
            _objInv.TrType = "IS"
            _objInv.id = Val(.Item(ConstId, i).Value)
            _objInv.WarrentyName = .Item(ConstLocation, i).Value
            _objInv.SerialNo = Trim(.Item(ConstSerialNo, i).Value & "")
            If IsDate(.Item(ConstWarrentyExpiry, i).Value) Then
                _objInv.WarrentyExpDate = DateValue(.Item(ConstWarrentyExpiry, i).Value)
            Else
                _objInv.WarrentyExpDate = DateValue("01/01/1950")
            End If
            _objInv.HSNCode = Trim(.Item(ConstBarcode, i).Value & "")
            If Val(.Item(ConstIGSTP, i).Value & "") = 0 Then .Item(ConstIGSTP, i).Value = 0
            _objInv.IGSTP = CDbl(.Item(ConstIGSTP, i).Value)
            If Val(.Item(ConstIGSTAmt, i).Value & "") = 0 Then .Item(ConstIGSTAmt, i).Value = 0
            _objInv.IGSTAmt = CDbl(.Item(ConstIGSTAmt, i).Value)

            If Val(.Item(ConstCGSTP, i).Value & "") = 0 Then .Item(ConstCGSTP, i).Value = 0
            _objInv.CSGTP = CDbl(.Item(ConstCGSTP, i).Value)
            If Val(.Item(ConstCGSTAmt, i).Value & "") = 0 Then .Item(ConstCGSTAmt, i).Value = 0
            _objInv.CGSTAMT = CDbl(.Item(ConstCGSTAmt, i).Value)
            If Val(.Item(ConstSGSTP, i).Value & "") = 0 Then .Item(ConstSGSTP, i).Value = 0
            _objInv.SGSTP = CDbl(.Item(ConstSGSTP, i).Value)
            If Val(.Item(ConstSGSTAmt, i).Value & "") = 0 Then .Item(ConstSGSTAmt, i).Value = 0
            _objInv.SGSTAmt = CDbl(.Item(ConstSGSTAmt, i).Value)
            _objInv.Smancode = Trim(.Item(Constsman, i).Value & "")
            If Val(.Item(ConstStartReading, i).Value) = 0 Then .Item(ConstStartReading, i).Value = 0
            If Val(.Item(ConstEndReading, i).Value) = 0 Then .Item(ConstEndReading, i).Value = 0
            _objInv.StartingReading = CDbl(.Item(ConstStartReading, i).Value)
            _objInv.EndingReading = CDbl(.Item(ConstEndReading, i).Value)
            _objInv.MeterCode = Trim(.Item(ConstMeterCode, i).Value & "")
            _objInv.impDocid = Val(.Item(ConstImpDocId, i).Value & "")
            _objInv.impDocSlno = Val(.Item(ConstImpLnId, i).Value & "")
            If Val(.Item(ConstWoodQty, i).Value) = 0 Then .Item(ConstWoodQty, i).Value = 0
            If Val(.Item(ConstWoodDiscQty, i).Value) = 0 Then .Item(ConstWoodDiscQty, i).Value = 0
            _objInv.WoodNetQty = CDbl(.Item(ConstWoodQty, i).Value) * PPerU
            _objInv.WoodDiscountQty = CDbl(.Item(ConstWoodDiscQty, i).Value) * PPerU
            If Val(.Item(ConstcessAmt, i).Value & "") = 0 Then .Item(ConstcessAmt, i).Value = 0
            _objInv.CessAmt = (CDbl(grdVoucher.Item(ConstcessAmt, i).Value) * FCRt) / CDbl(PPerU)
            If IsDate(.Item(ConstManufacturingdate, i).Value) Then
                _objInv.manufacturingdate = DateValue(.Item(ConstManufacturingdate, i).Value)
            Else
                _objInv.manufacturingdate = DateValue("01/01/1950")
            End If
            If _objInv.ItemId = 0 Then
                _objInv.TrQty = 1
                '_objInv.UnitCost = 1
                '_objInv.taxP = 1
                '_objInv.taxAmt = 1
                '_objInv.UnitDiscount = 0
            End If
            'addtodtTb(Invid, .Item(ConstItemID, i).Value, .Item(ConstId, i).Value)

        End With
    End Sub
    Private Sub saveJobItems(Optional ByVal isSTOOnly As Boolean = False)
        _objJob = New clsJob
        Dim itemidsdatatable As DataTable
        Dim dtTable As DataTable
        Dim dateChanged As Boolean
        Dim i As Integer
        Dim TDrAmt As Double
        dtTable = _objcmnbLayer._fldDatatable("SELECT TOP 1 * FROM ItmInvCmnTb LEFT JOIN VoucherTypeNoTb ON ItmInvCmnTb.TypeNo=VoucherTypeNoTb.vrno " & _
                                                      "WHERE InvType='OUT' AND Trdate >='" & Format(DateValue(dtpdate.Value), "yyyy/MM/dd") & "'")
        If dtTable.Rows.Count > 0 Then
            dateChanged = True
        Else
            dateChanged = False
        End If
        If Val(numsto.Tag) > 0 Then
            isModi = True
            _objcmnbLayer._saveDatawithOutParm("Update JobitemTb set setRemove=1 WHERE jbid=" & Val(numVchrNo.Tag))
            _objcmnbLayer._saveDatawithOutParm("Update ItmInvTrTb set setRemove=1 WHERE trid=" & Val(numsto.Tag))
        End If
        saveInventory()
        TDrAmt = saveInvTr(numsto.Tag)

        'With grdVoucher
        'For i = 0 To .Rows.Count - 1 '- 1
        '    If CDbl(.Item(ConstQty, i).Value) <> 0 Or .Item(ConstSlNo, i).Value.ToString = "M" Then
        '        PPerU = Val(.Item(ConstPMult, i).Value) 'Switch(.TextMatrix(i, 4) = "B", 1, .TextMatrix(i, 4) = "P1", CSng(grdVoucher.TextMatrix(i, 18)), .TextMatrix(i, 4) = "P2", CSng(grdVoucher.TextMatrix(i, 19)))
        '        PPerU = IIf(PPerU = 0, 1, PPerU)
        '        setInvDetValue(Val(numsto.Tag), PPerU, i)
        '        _objInv._saveDetails()
        '        If dateChanged And enableRealtimeCosting Then
        '            _objInv.ItemId = Val(.Item(ConstItemID, i).Value)
        '            _objInv.TrDate = DateValue(dtpdate.Value)
        '            _objInv.setcostAverageOnModification(UsrBr)
        '        End If
        '    End If
        'Next
        'End With
        'If isModi Then
        '    itemidsdatatable = _objcmnbLayer._fldDatatable("select itemid from ItmInvTrTb where setRemove=1 and trid=" & Val(numsto.Tag))
        '    _objcmnbLayer._saveDatawithOutParm("delete from ItmInvTrTb where setRemove=1 and trid=" & Val(numsto.Tag))
        '    If itemidsdatatable.Rows.Count > 0 And enableRealtimeCosting Then
        '        For i = 0 To itemidsdatatable.Rows.Count - 1
        '            _objInv.ItemId = itemidsdatatable(i)("Itemid")
        '            _objInv.TrDate = DateValue(dtpdate.Value)
        '            _objInv.setcostAverageOnModification(UsrBr)
        '        Next
        '    End If
        '    _objcmnbLayer._saveDatawithOutParm("delete from JobitemTb where setRemove=1 and jbid=" & Val(numVchrNo.Tag))
        '    _objcmnbLayer._saveDatawithOutParm("update JobTb set itemcost=" & CDbl(txttotalItemAmt.Text) & " where jobid=" & Val(numVchrNo.Tag))
        'End If
        'updateStockTransaction()
        UpdateAccounts(numsto.Tag, TDrAmt, 0)
        ischgItm = False
        If isSTOOnly Then
            MsgBox("Stock Updated", MsgBoxStyle.Information)
        End If
    End Sub
    Private Sub updateStockTransaction()
        If Not enableCostAccounting Then Exit Sub
        Dim stockAc As Long
        Dim costOfSalesAc As Long
        Dim dt As DataTable
        Dim costAmt As Double
        Dim dtTable As DataTable
        Dim LinkNo As Long
        stockAc = getConstantAccounts(1)
        costOfSalesAc = getConstantAccounts(9)
        If stockAc = 0 Or costOfSalesAc = 0 Then Exit Sub
        If isModi And Val(numsto.Tag) > 0 Then
            dtTable = _objcmnbLayer._fldDatatable("SELECT LinkNo FROM AccTrCmn WHERE LnkNo = " & Val(numsto.Tag))
            If dtTable.Rows.Count > 0 Then
                LinkNo = dtTable(0)("LinkNo")
                _objcmnbLayer._saveDatawithOutParm("DELETE FROM AccTrDet WHERE LinkNo=" & LinkNo)
            End If
        End If
        setAcctrCmnValue(Val(numsto.Tag), LinkNo)
        LinkNo = 0
        LinkNo = Val(_objTr.SaveAccTrCmn())
        dt = _objcmnbLayer._fldDatatable("select sum(CostAvg*TrQty) costAmt from ItmInvTrTb  where trid=" & Val(numsto.Tag))
        If dt.Rows.Count > 0 Then
            costAmt = dt(0)("costAmt")
        End If
        If costAmt <> 0 Then
            'debit entry [cost of sales]
            Dim entryref As String = "SERVICE STOCK OUT : JOB#" & numVchrNo.Text
            setAcctrDetValue(LinkNo, costOfSalesAc, Val(numsto.Text), entryref, costAmt, "", "", 3, 0, "", _
                           "", stockAc, costOfSalesAc & Val(numsto.Text), "", 1)
            _objTr.saveAccTrans()
            UpdtClosBal(costOfSalesAc, costAmt)
            'credit entry [stock in hand]
            costAmt = costAmt * -1
            setAcctrDetValue(LinkNo, stockAc, numsto.Text, entryref, costAmt, "", "", 3, 0, "", _
                           "", costOfSalesAc, stockAc & numsto.Text, "", 1)
            _objTr.saveAccTrans()
            UpdtClosBal(stockAc, costAmt)
        End If
    End Sub
    Private Sub setAcctrCmnValue(ByVal InvTrid As Long, ByVal LinkNo As Long)
        _objTr.JVType = "STO"
        _objTr.JVDate = DateValue(dtpconsumptiondate.Value)
        _objTr.PreFix = ""
        _objTr.JVNum = Val(numsto.Text)
        _objTr.JVTypeNo = getVouchernumber("STO")
        _objTr.UserId = Trim(CurrentUser & "")
        _objTr.MchName = MACHINENAME
        _objTr.CrtDtTm = DateValue(Date.Now)
        _objTr.TypeNo = getVouchernumber("STO")
        '_objTr.VrDescr = Trim(txtpayee.Text)
        _objTr.IsModi = IIf(LinkNo > 0, 1, 0)
        _objTr.LinkNo = InvTrid
    End Sub
    'Private Sub setAcctrDetValue(ByVal lnkNo As Long, ByVal AccountNo As Long, ByVal Reference As String, ByVal EntryRef As String, ByVal DealAmt As Double, ByVal JobCode As String, ByVal JobStr As String, _
    '                              ByVal TrInf As Short, ByVal OthCost As Short, ByVal LPO As String, ByVal TermsId As String, ByVal CustAcc As Integer, ByVal AccWithRef As String, _
    '                              ByVal CurrencyCode As String, ByVal CurrRate As Double)
    '    With _objTr
    '        .trLinkNo = lnkNo
    '        .AccountNo = AccountNo
    '        .Reference = numsto.Text
    '        .EntryRef = EntryRef
    '        .DealAmt = DealAmt
    '        .JobCode = JobCode
    '        .JobStr = JobStr
    '        .CurrRate = CurrRate
    '        .CurrencyCode = CurrencyCode ' IIf(chkFC.Checked = True, txtFC.Text, "")
    '        .TrInf = TrInf
    '        .OthCost = OthCost
    '        .TermsId = TermsId
    '        .CustAcc = CustAcc
    '        .AccWithRef = AccWithRef
    '        .LPONo = LPO
    '        Dim dtLPO As Date = IIf(chkDate(dtpdate.Value), dtpdate.Value, DateValue(dtpdate.Value))
    '        .DocDate = dtLPO
    '        .SuppInvDate = dtLPO
    '        .DueDate = dtLPO
    '    End With
    'End Sub
    Private Sub setAcctrDetValue(ByVal lnkNo As Long, ByVal AccountNo As Long, ByVal Reference As String, ByVal EntryRef As String, ByVal DealAmt As Double, ByVal JobCode As String, ByVal JobStr As String, _
                                 ByVal TrInf As Short, ByVal OthCost As Short, ByVal LPO As String, ByVal TermsId As String, ByVal CustAcc As Integer, ByVal AccWithRef As String, _
                                 ByVal CurrencyCode As String, ByVal CurrRate As Double)
        Dim dtrow As DataRow
        Dim dtLPO As Date = DateValue(Date.Now)
        Dim dtSup As Date = DateValue(Date.Now)
        dtrow = dtAccTb.NewRow
        dtrow("LinkNo") = lnkNo
        dtrow("AccountNo") = AccountNo
        dtrow("Reference") = numsto.Text
        'dtrow("DueDate") = Format(DateValue(cldrdate.Value), "yyyy/MM/dd")
        dtrow("EntryRef") = EntryRef
        dtrow("DealAmt") = DealAmt
        dtrow("FCAmt") = DealAmt * CurrRate
        dtrow("CurrencyCode") = CurrencyCode
        dtrow("CurrRate") = CurrRate
        dtrow("TrInf") = TrInf
        dtrow("OthCost") = OthCost
        dtrow("TermsId") = TermsId
        dtrow("CustAcc") = CustAcc
        dtrow("AccWithRef") = AccWithRef
        dtrow("LPONo") = LPO
        'dtrow("SuppInvDate") = Format(DateValue(cldrdate.Value), "yyyy/MM/dd")
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
    Private Sub saveInventory()

        If Val(numsto.Text & "") = 0 Then
            Dim PreFixTb As DataTable
            PreFixTb = _objcmnbLayer._fldDatatable("SELECT * FROM InvNos WHERE InvType='STO'", False)
            If PreFixTb.Rows.Count > 0 Then
                numsto.Text = Val(PreFixTb.Rows(0)("InvNo"))
            Else
                numsto.Text = 1
            End If
        End If
chkagain:
        If Val(numsto.Tag) = 0 Then
            If Not CheckNoExists("", Val(numsto.Text), "STO", "Inventory") Then
                numsto.Text = Val(numsto.Text) + 1
                GoTo chkagain
            End If
        End If
        setInvCmnValue()
        _objcmnbLayer._saveDatawithOutParm("Update InvItm Set IssdQty=IssdQty-tr.tQty,QIH=QIH+tr.tQty from " & _
                                           "(SELECT Itemid,TrQty tQty from ItmInvTrTb where trid=" & Val(numsto.Tag) & ") tr" & _
                                           "  Where InvItm.ItemId=tr.Itemid")
        '_objcmnbLayer._saveDatawithOutParm("delete from ItmInvTrTb where trid=" & Val(numsto.Tag))
        _objcmnbLayer._saveDatawithOutParm("UPDATE InvNos SET Prefix = '',InvNo=" & Val(numsto.Text) + 1 & " WHERE InvType='STO'")
    End Sub
    Private Sub setInvCmnValue()
        Dim Dt As Date
        With _objInv
            Dt = DateValue(Date.Now)
            .TrId = Val(numsto.Tag & "")
            .TrDate = DateValue(dtpconsumptiondate.Value)
            .TrType = "STO"
            .DocLstTxt = ""
            .Prefix = ""
            .InvNo = Val(numsto.Text)
            .TrRefNo = numsto.Text ' Trim(txtReference.Text)
            .CSCode = Val(txtcustomer.Tag)
            .PSAcc = PSAcc
            .JobCode = numVchrNo.Text
            .ImpDoc = 0
            .UserId = CurrentUser
            .IsFC = 0
            .FCRate = 1
            .NFraction = 2
            .FC = ""
            .Discount = 0
            .TrDescription = "SERVICE STOCK OUT TRANSACTION"
            .TypeNo = getVouchernumber("STO")
            .EnaJob = False
            .DocDefLoc = ""
            .BrId = ""
            .OthCost = 0
            .Discount1 = 0
            .NetAmt = CDbl(txttotalItemAmt.Text)
            .LPO = ""
            'Dim dt As Date = getServerDate()
            .CrtDt = Dt
            .DelDate = Dt
            .DueDate = Dt
            .DocDate = Dt
            .SuppInvDate = Dt
            .TermsId = ""
            .MchName = MACHINENAME
            .isModi = IIf(Val(numsto.Tag & "") > 0, True, False)
            .lpoclass = ""
            .rndoff = 0
            .isTaxInvoice = chktaxInv.Checked
            numsto.Tag = Val(_objInv._saveCmn())
        End With
    End Sub
    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        updateClick()
    End Sub
    Private Sub updateClick()
        If Val(btnupdate.Tag) = 0 Then
            MsgBox("This user do not have permission to Update", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        verify()
    End Sub
    Private Sub makeClear()
        chgbyprg = True
        txtJobname.Text = ""
        txtDescription.Text = ""
        txtEstAmt.Text = Format(0, numFormat)
        txtcustomer.Text = ""
        txtcustomer.Tag = ""
        txtaddress.Text = ""
        txtobservedBy.Text = ""
        txtobservedBy.Text = ""
        txttechnician.Text = ""
        txtserviceCharge.Text = Format(0, numFormat)
        lblitemcost.Text = Format(0, numFormat)
        lblJobvalue.Text = Format(0, numFormat)
        lblitmcost.Text = Format(0, numFormat)
        lbljobcost.Text = Format(0, numFormat)
        txttotalItemAmt.Text = Format(0, numFormat)
        grdVoucher.Rows.Clear()
        grdImei.Rows.Clear()
        numsto.Text = ""
        numsto.Tag = ""
        numVchrNo.Tag = ""
        lblstatus.Text = ""
        btnmodify.Text = "Modify"
        chgbyprg = False
        ischgItm = False
        isModi = False
    End Sub

    Private Sub calculateJobvalue()
        If Val(txtserviceCharge.Text) = 0 Then txtserviceCharge.Text = 0
        If Val(lblitemcost.Text) = 0 Then lblitemcost.Text = 0
        lblJobvalue.Text = Format(CDbl(txtserviceCharge.Text) + CDbl(lblitemcost.Text), numFormat)
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        If isModi Then
            If Val(btndelete.Tag) = 0 Then
                MsgBox("This user do not have permission to Remove", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            If MsgBox("Do you want to remove the current Job?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            If Val(numVchrNo.Tag) = 0 Then Exit Sub
            _objcmnbLayer = New clsCommon_BL
            With _objcmnbLayer
                ._saveDatawithOutParm("DELETE FROM JobTb WHERE Jobid=" & Val(numVchrNo.Tag))
                ._saveDatawithOutParm("DELETE FROM JobReceivedAccessoriesTb WHERE Jobid=" & Val(numVchrNo.Tag))
            End With
            deleteInventory()
            Me.Close()
        Else
            If MsgBox("Do you want to clear the current Job?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            makeClear()
        End If
    End Sub
    Public Sub ldRec(ByVal jbid As Long)
        Dim ds As DataSet
        _objJob = New clsJob
        With _objJob
            .Jobid = jbid
            .DateFrom = DateValue(Date.Now)
            .DateTo = DateValue(Date.Now)
            .Tp = 0
            ds = .returnJob
        End With
        If ds.Tables(0).Rows.Count > 0 Then
            chgbyprg = True
            numVchrNo.Text = ds.Tables(0)(0)("jobcode")
            txtprintjob.Text = ds.Tables(0)(0)("jobcode")
            numVchrNo.Tag = ds.Tables(0)(0)("Jobid")
            dtpdate.Value = ds.Tables(0)(0)("jobdate")
            txtJobname.Text = Trim(ds.Tables(0)(0)("jobname") & "")
            txtDescription.Text = Trim(ds.Tables(0)(0)("JobDescription") & "")

            If Not IsDBNull(ds.Tables(0)(0)("EstimatedDate")) Then
                dtpestimatedDt.Value = ds.Tables(0)(0)("EstimatedDate")
            End If
            If Not IsDBNull(ds.Tables(0)(0)("EstimatedAmt")) Then
                txtEstAmt.Text = Format(CDbl(ds.Tables(0)(0)("EstimatedAmt")), numFormat)
            Else
                txtEstAmt.Text = Format(0, numFormat)
            End If
            txtcustomer.Text = ds.Tables(0)(0)("AccDescr")
            txtcustomer.Tag = ds.Tables(0)(0)("custcode")
            txtaddress.Text = ds.Tables(0)(0)("Address1") & vbCrLf & ds.Tables(0)(0)("Address2") & vbCrLf & ds.Tables(0)(0)("Address3") & vbCrLf & ds.Tables(0)(0)("Phone") & vbCrLf & ds.Tables(0)(0)("ContactName")


            'txtobservedBy.Text = Trim(ds.Tables(0)(0)("OBTech") & "")
            txttechnician.Tag = Trim(ds.Tables(0)(0)("SManCode") & "")
            Dim dt As DataTable
            _objcmnbLayer = New clsCommon_BL
            dt = _objcmnbLayer._fldDatatable("SELECT SManName from SalesmanTb  where SManCode='" & txttechnician.Tag & "'")
            If dt.Rows.Count > 0 Then
                txttechnician.Text = dt(0)("SManName")
            Else
                txttechnician.Text = ""
                txttechnician.Tag = ""
            End If

            _objcmnbLayer = Nothing
            If Val(ds.Tables(0)(0)("ServiceCost") & "") = 0 Then
                ds.Tables(0)(0)("ServiceCost") = 0

            End If

            txtserviceCharge.Text = Format(CDbl(ds.Tables(0)(0)("ServiceCost")), numFormat)
            If Val(ds.Tables(0)(0)("LabourCost") & "") = 0 Then
                ds.Tables(0)(0)("LabourCost") = 0

            End If
            txtscost.Text = Format(CDbl(ds.Tables(0)(0)("LabourCost")), numFormat)
            If Val(ds.Tables(0)(0)("ItemCost") & "") = 0 Then
                ds.Tables(0)(0)("ItemCost") = 0

            End If
            lblitemcost.Text = Format(CDbl(ds.Tables(0)(0)("ItemCost")), numFormat)
            numsto.Text = Val(ds.Tables(0)(0)("invno") & "")
            numsto.Tag = Val(ds.Tables(0)(0)("trid") & "")
            If Val(ds.Tables(0)(0)("Status") & "") > 0 Then
                lblstatus.Text = "CLOSED"
                lblstatus.ForeColor = Color.Red
                btnjobclose.Text = "Undo Close"
            Else
                lblstatus.Text = "ACTIVE"
                lblstatus.ForeColor = Color.Green
                btnjobclose.Text = "Job Close"
            End If
            chgbyprg = False
        End If
        '#################IMEI NO LIST ################################
        Dim i As Integer
        'If grdinvList.Columns.Count = 0 Then
        '    isset = False
        'End If
        setImeiGrid()
        With grdImei
            .Rows.Clear()
            For i = 0 To ds.Tables(1).Rows.Count - 1
                .Rows.Add(1)
                .Item(ConstImeino, i).Value = ds.Tables(1)(i)("IMEI")
                .Item(ConstModel, i).Value = ds.Tables(1)(i)("Model")
                .Item(ConstRemark, i).Value = ds.Tables(1)(i)("Remark")
                .Item(ConstBt, i).Value = IIf(ds.Tables(1)(i)("bt") = 1, "Y", "")
                .Item(ConstCv, i).Value = IIf(ds.Tables(1)(i)("cv") = 1, "Y", "")
                .Item(ConstBX, i).Value = IIf(ds.Tables(1)(i)("bx") = 1, "Y", "")
                .Item(ConstMc, i).Value = IIf(ds.Tables(1)(i)("mc") = 1, "Y", "")
                .Item(ConstPen, i).Value = IIf(ds.Tables(1)(i)("pen") = 1, "Y", "")
                .Item(ConstComplaints, i).Value = ds.Tables(1)(i)("Complaints")
                .Item(ConstObservation, i).Value = ds.Tables(1)(i)("Observation")
                .Item(ConstTechRemarks, i).Value = ds.Tables(1)(i)("TechRemarks")
                .Item(ConstCancelWarrenty, i).Value = IIf(ds.Tables(1)(i)("CancelWarrenty") = True, "Y", "")
                .Item(ConstWithWarrenty, i).Value = IIf(ds.Tables(1)(i)("withWarrenty") = True, "Y", "")
                If Trim(ds.Tables(1)(i)("Delivered") & "") = "True" Then
                    .Item(ConstDelivered, i).Value = "Y"
                Else
                    .Item(ConstDelivered, i).Value = ""
                End If

                .Item(ConstJbImeiid, i).Value = ds.Tables(1)(i)("jobimeiId")
            Next
        End With
        loadItems(Val(numsto.Tag))
        ''############################################################
        'Dim j As Integer
        'SetGridHead()
        'grdVoucher.Rows.Clear()
        'With grdVoucher
        '    For j = 0 To ds.Tables(2).Rows.Count - 1
        '        .Rows.Add(1)
        '        i = .RowCount - 1
        '        .Item(ConstSlNo, i).Value = i + 1
        '        .Item(ConstItemCode, i).Value = ds.Tables(2)(j)("item code")
        '        .Item(ConstDescr, i).Value = ds.Tables(2)(j)("Description")
        '        .Item(ConstUnit, i).Value = ds.Tables(2)(j)("Unit")
        '        .Item(ConstQty, i).Value = Format(Val(ds.Tables(2)(j)("Qty")), numFormat)
        '        .Item(ConstUPrice, i).Value = Format(CDbl(ds.Tables(2)(j)("Uprice")), numFormat)
        '        .Item(ConstTaxP, i).Value = Format(CDbl(ds.Tables(2)(j)("Taxp")), numFormat)
        '        .Item(ConstTaxAmt, i).Value = Format(CDbl(ds.Tables(2)(j)("TaxAmt")), numFormat)
        '        .Item(ConstLTotal, i).Value = Format((CDbl(ds.Tables(2)(j)("Qty")) * CDbl(ds.Tables(2)(j)("Uprice"))) + CDbl(ds.Tables(2)(j)("TaxAmt")), numFormat)
        '        .Item(ConstItemID, i).Value = Val(ds.Tables(2)(j)("ItemId"))
        '        .Item(ConstId, i).Value = Val(ds.Tables(2)(j)("jbitmId"))
        '    Next
        'End With
        ldItemCost()
        fillGrid()
        reArrangeNo()
        reArrangeNoImei()
        calculate()
        isModi = True
    End Sub
    Private Sub loadItems(ByVal loadedTrId As Long)
        Dim sRs As DataTable
        Dim UPerPack As Integer
        _objcmnbLayer = New clsCommon_BL
        sRs = _objcmnbLayer._fldDatatable("SELECT ItmInvTrTb.*, [Item Code],Warrentyname,isSerialNo,fcode,FraCount,isnull(itemCategory,'')itemCategory,collectionAC,vat,isTaxInvoice,trdate " & _
                                          "FROM ItmInvTrTb LEFT JOIN InvItm ON InvItm.ItemId = ItmInvTrTb.ItemId " & _
                                          "LEFT JOIN ItmInvCmnTb on ItmInvCmnTb.trid=ItmInvTrTb.trid " & _
                                          " LEFT JOIN InvItmPropertiesTb ON InvItmPropertiesTb.Itemid=invitm.itemid " & _
                                          "LEFT JOIN WarrentyMastertb ON WarrentyMastertb.Wid=ItmInvTrTb.WarrentyId " & _
                                          "LEFT JOIN FuelMeterReadingTb ON FuelMeterReadingTb.fmeterid=ItmInvTrTb.FuleMeterid " & _
                                          "LEFT JOIN UnitsTb ON UnitsTb.Units=ItmInvTrTb.Unit " & _
                                          "LEFT JOIN VatMasterTb ON InvItm.vatId=VatMasterTb.vatId " & _
                                          "WHERE ItmInvTrTb.TrId = " & loadedTrId & " ORDER BY SlNo")
        grdVoucher.RowCount = 0
        Dim tNumformat As String
        With grdVoucher
            .Rows.Clear()
            If sRs.Rows.Count > 0 Then
                For i = 0 To sRs.Rows.Count - 1
                    grdVoucher.Rows.Add(1)
                    If i = 0 Then
                        If IsDBNull(sRs(i)("isTaxInvoice")) Then sRs(i)("isTaxInvoice") = 0
                        chktaxInv.Checked = IIf(sRs(i)("isTaxInvoice") = "True", 1, 0)
                        dtpconsumptiondate.Value = DateValue(sRs(i)("trdate"))
                    End If
                    'grdVoucher.Item(ConstSlNo, i).Value = IIf(sRs(i)("MsgId") = 1, "M", IIf(sRs(i)("MsgId") = 2, "L", ""))
                    UPerPack = IIf(sRs(i)("PFraction") = 0, 1, sRs(i)("PFraction"))
                    grdVoucher.Item(ConstSlNo, i).Value = IIf(Val(sRs(i)("ItemId")) = 0, "M", "")
                    If grdVoucher.Item(ConstSlNo, i).Value <> "M" Then
                        UPerPack = IIf(sRs(i)("PFraction") = 0 Or IsDBNull(sRs(i)("PFraction")), 1, sRs(i)("PFraction"))
                        grdVoucher.Item(ConstItemCode, i).Value = Trim("" & sRs(i)("Item Code"))
                        grdVoucher.Item(ConstItemID, i).Value = sRs(i)("ItemId")
                        grdVoucher.Item(ConstPFraction, i).Value = sRs(i)("FraCount")
                        grdVoucher.Item(ConstPMult, i).Value = UPerPack 'IIf(IsNull(sRs!PFraCount), "2", sRs!PFraCount)
                    Else
                        grdVoucher.Item(ConstPFraction, i).Value = "2"
                        grdVoucher.Item(ConstPMult, i).Value = "1"
                        grdVoucher.Item(ConstItemID, i).Value = 0
                    End If
                    grdVoucher.Item(ConstDescr, i).Value = IIf(IsDBNull(sRs(i)("IDescription")), "", sRs(i)("IDescription"))
                    grdVoucher.Item(ConstB, i).Value = IIf(sRs(i)("Method") & "" = "", "B", Trim(sRs(i)("Method") & ""))
                    grdVoucher.Item(ConstUnit, i).Value = Trim("" & sRs(i)("Unit"))
                    If Val(sRs(i)("Taxp") & "") = 0 Then sRs(i)("Taxp") = 0
                    grdVoucher.Item(ConstTaxP, i).Value = Format(sRs(i)("Taxp"), numFormat)
                    If Val(sRs(i)("taxamt") & "") = 0 Then sRs(i)("taxamt") = 0
                    grdVoucher.Item(ConstTaxAmt, i).Value = Format(sRs(i)("taxamt") / FCRt, numFormat)

                    If Val(sRs(i)("vat") & "") = 0 Then sRs(i)("vat") = 0
                    grdVoucher.Item(Constcess, i).Value = Format(sRs(i)("vat"), numFormat)
                    If Val(sRs(i)("CessAmt") & "") = 0 Then sRs(i)("CessAmt") = 0
                    grdVoucher.Item(ConstcessAmt, i).Value = Format(sRs(i)("CessAmt") / FCRt, numFormat)
                    If Val(sRs(i)("collectionAC") & "") = 0 Then sRs(i)("collectionAC") = 0
                    grdVoucher.Item(ConstcessAc, i).Value = Val(sRs(i)("collectionAC"))

                    grdVoucher.Item(ConstLocation, i).Value = Trim("" & sRs(i)("Warrentyname")) ' !Colour
                    grdVoucher.Item(ConstQty, i).Value = Format(sRs(i)("TrQty") / UPerPack, "#,##0" & IIf(Val(grdVoucher.Item(ConstPFraction, i).Value & "") = 0, "", "." & Strings.StrDup(CByte(Val(grdVoucher.Item(ConstPFraction, i).Value & "")), "0")))
                    grdVoucher.Item(ConstUPrice, i).Value = Format(sRs(i)("UnitCost") * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & numformat)
                    grdVoucher.Item(ConstActualPrice, i).Value = sRs(i)("UnitCost") * UPerPack / FCRt
                    tNumformat = "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))
                    grdVoucher.Item(ConstLTotal, i).Value = Format((sRs(i)("TrQty") * sRs(i)("UnitCost")) * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & numformat)
                    grdVoucher.Item(ConstUnitOthCost, i).Value = Format(sRs(i)("UnitOthCost") * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & numformat)
                    grdVoucher.Item(ConstActualOthCost, i).Value = sRs(i)("UnitOthCost") * UPerPack / FCRt
                    grdVoucher.Item(ConstSerialNo, i).Value = sRs(i)("SerialNo")
                    .Item(constItmTot, i).Value = Format((.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value), numFormat)
                    'grdVoucher.Item(ConstMthd, i).Value = sRs(i)("Method")
                    'grdVoucher.Item(ConstUnitVal, i).Value = sRs(i)("UnitValue") * UPerPack / FCRt
                    'grdVoucher.Item(ConstDiscOther, i).Value = sRs(i)("DiscOther") * UPerPack / FCRt
                    grdVoucher.Item(ConstDiscOther, i).Value = sRs(i)("UnitDiscount") * UPerPack / FCRt
                    If Val(sRs(i)("DisP") & "") = 0 Then sRs(i)("DisP") = 0
                    grdVoucher.Item(ConstDisP, i).Value = Format(sRs(i)("DisP"), numFormat)
                    If Val(sRs(i)("ItemDiscount") & "") = 0 Then sRs(i)("ItemDiscount") = 0
                    grdVoucher.Item(ConstDisAmt, i).Value = Format(sRs(i)("ItemDiscount") * UPerPack / FCRt, numFormat)
                    grdVoucher.Item(ConstImpDocId, i).Value = sRs(i)("impDocid")
                    grdVoucher.Item(ConstImpLnId, i).Value = sRs(i)("impDocSlno")


                    grdVoucher.Item(ConstBarcode, i).Value = sRs(i)("HSNCode")
                    grdVoucher.Item(ConstCGSTP, i).Value = sRs(i)("CSGTP")
                    grdVoucher.Item(ConstCGSTAmt, i).Value = sRs(i)("CGSTAMT")

                    grdVoucher.Item(ConstSGSTP, i).Value = sRs(i)("SGSTP")
                    grdVoucher.Item(ConstSGSTAmt, i).Value = sRs(i)("SGSTAmt")

                    grdVoucher.Item(ConstIGSTP, i).Value = sRs(i)("IGSTP")
                    grdVoucher.Item(ConstIGSTAmt, i).Value = sRs(i)("IGSTAmt")
                    
                    grdVoucher.Item(ConstIsSerial, i).Value = 0
                    grdVoucher.Item(ConstId, i).Value = sRs(i)("id")
                    calcualteLineTotal(i)
                Next

            End If
        End With
        If sRs.Rows.Count > 0 Then sRs.Clear()
        calculate()
        reArrangeNo()
    End Sub

    Private Sub btnitmAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnitmAdd.Click
        AddRow()
    End Sub
    Public Sub AddRow(Optional ByVal tocheck As Boolean = False)
        Dim i As Integer
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
            .CurrentCell = .Item(ConstItemCode, i)
            SrchText = "" ' .Item(ConstItemCode, i).Value
            chgbyprg = True
            .BeginEdit(True)
            chgbyprg = False
            chgItm = False
        End With
        calculate()
    End Sub
    Private Sub calcualteLineTotal(ByVal RowIndex As Integer)
        chgbyprg = True
        With grdVoucher
            Dim i As Integer
            i = RowIndex
            If Val(.Item(ConstItemID, i).Value) = 0 Then Exit Sub
            .Item(ConstSlNo, i).Value = i + 1
            If Val(.Item(ConstTaxP, i).Value & "") = 0 Then
                .Item(ConstTaxP, i).Value = 0
            End If
            If Val(.Item(Constcess, i).Value & "") = 0 Then
                .Item(Constcess, i).Value = 0
            End If
            If Val(.Item(ConstActualPrice, i).Value & "") = 0 Then
                .Item(ConstActualPrice, i).Value = 0
            End If
            If Val(.Item(ConstQty, i).Value & "") = 0 Then
                .Item(ConstQty, i).Value = 0
            End If
            Dim gstamt As Double
            Dim cessTtl As Double
            If EnableGST Then
                getGSTDetails(Trim(.Item(ConstBarcode, i).Value & ""), i, True)
                If Val(.Item(ConstSGSTAmt, i).Value & "") = 0 Then .Item(ConstSGSTAmt, i).Value = 0
                If Val(.Item(ConstCGSTAmt, i).Value & "") = 0 Then .Item(ConstCGSTAmt, i).Value = 0
                gstamt = CDbl(.Item(ConstCGSTAmt, i).Value) + CDbl(.Item(ConstSGSTAmt, i).Value)
                .Item(ConstTaxAmt, i).Value = Format(gstamt, numFormat)
            ElseIf enableGCC Or ShowTaxOnInventory Then
                Dim actualPrice As Double
                Dim discountOther As Double
                discountOther = CDbl(.Item(ConstDiscOther, i).Value) * CDbl(.Item(ConstQty, i).Value)
                actualPrice = (CDbl(.Item(ConstActualPrice, i).Value) * CDbl(.Item(ConstQty, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value) - discountOther
                actualPrice = Format(actualPrice, numFormat)
                .Item(ConstIGSTAmt, i).Value = ((actualPrice * .Item(ConstIGSTP, i).Value) / 100)
                .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstIGSTAmt, i).Value), numFormat)
            End If
            If enablecess And cessdate <= DateValue(dtpdate.Value) And chktaxInv.Checked Then
                cessTtl = (((CDbl(.Item(ConstActualPrice, i).Value) - CDbl(.Item(ConstDiscOther, i).Value)) * .Item(Constcess, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value)
                .Item(ConstcessAmt, i).Value = Format(cessTtl, numFormat)
            End If
            If chktaxInv.Checked = False Then
                .Item(ConstTaxAmt, i).Value = Format(0, numFormat)
                .Item(ConstTaxP, i).Value = Format(0, numFormat)
                .Item(ConstcessAmt, i).Value = 0
            End If

            .Item(constItmTot, i).Value = Format((.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value), numFormat)
            Dim ttl As Double
            ttl = (.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value) + gstamt + cessTtl
            .Item(ConstLTotal, i).Value = Format(ttl, numFormat)
            .Item(ConstNUPrice, i).Value = CDbl(.Item(ConstActualPrice, i).Value) - (CDbl(.Item(ConstDisAmt, i).Value) / CDbl(.Item(ConstQty, i).Value)) + (CDbl(.Item(ConstTaxAmt, i).Value) / CDbl(.Item(ConstQty, i).Value)) + (CDbl(.Item(ConstcessAmt, i).Value) / CDbl(.Item(ConstQty, i).Value)) - Val(.Item(ConstDiscOther, i).Value)
        End With
        chgbyprg = False
    End Sub
    Private Sub calculate(Optional ByVal bFrmCalcOthCost As Boolean = False, Optional ByVal calculateLineTotal As Boolean = False, Optional ByVal chgDiscount As Boolean = False)
        Dim totQty As Double
        Dim totItm As Double
        Dim totTax As Double
        Dim totLnDis As Double
        Dim totAmt As Double
        Dim totCess As Double
        Dim i As Integer
        Dim gindex As Integer
        Dim lnTax As Double
        Dim lnttl As Double
        Dim totTaxableAmt As Double
        If grdVoucher.CurrentCell Is Nothing Then
            gindex = grdVoucher.RowCount - 1
        Else
            gindex = grdVoucher.CurrentCell.RowIndex
        End If
        If calculateLineTotal Then
            calcualteLineTotal(gindex)
        End If

        With grdVoucher
            For i = 0 To .Rows.Count - 1
                lnTax = 0
                If Val(.Item(ConstItemID, i).Value) = 0 Then GoTo nxt
                totQty = totQty + IIf(Val(.Item(ConstQty, i).Value) > 0, Val(.Item(ConstQty, i).Value), 0)
                If chktaxInv.Checked = False Then
                    .Item(ConstTaxAmt, i).Value = Format(0, numFormat)
                    .Item(ConstTaxP, i).Value = Format(0, numFormat)
                    .Item(ConstcessAmt, i).Value = 0
                Else
                    .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstIGSTAmt, i).Value), numFormat)
                    .Item(ConstTaxP, i).Value = Format(CDbl(.Item(ConstIGSTP, i).Value), numFormat)
                    If enablecess Then
                        lnTax = (((CDbl(.Item(ConstActualPrice, i).Value) - CDbl(.Item(ConstDiscOther, i).Value)) * .Item(Constcess, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value)
                        .Item(ConstcessAmt, i).Value = Format(lnTax, numFormat)
                    End If
                    lnTax = lnTax + CDbl(.Item(ConstIGSTAmt, i).Value)
                End If
                If EnableGST Then
                    If chktaxInv.Checked Then
                        totTax = totTax + CDbl(.Item(ConstCGSTAmt, i).Value) + CDbl(.Item(ConstSGSTAmt, i).Value)
                    End If
                Else
                    If chktaxInv.Checked Then
                        totTax = totTax + CDbl(.Item(ConstIGSTAmt, i).Value)
                    End If
                End If

                totLnDis = totLnDis + .Item(ConstDisAmt, i).Value
                totAmt = totAmt + (CDbl(.Item(ConstQty, i).Value) * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value)
                totItm = totItm + (CDbl(.Item(ConstQty, i).Value) * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value)
                If enablecess And chktaxInv.Checked Then
                    totCess = totCess + CDbl(.Item(ConstcessAmt, i).Value)
                End If
                lnttl = (CDbl(.Item(ConstQty, i).Value) * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value)
                lnttl = lnttl + lnTax
                chgbyprg = True
                .Item(ConstLTotal, i).Value = Format(lnttl, numFormat)
                chgbyprg = False
                If Val(.Item(ConstTaxAmt, i).Value) > 0 Then
                    totTaxableAmt = totTaxableAmt + ((CDbl(.Item(ConstActualPrice, i).Value) - CDbl(.Item(ConstDiscOther, i).Value)) * CDbl(.Item(ConstQty, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value)
                End If
nxt:
            Next
            totAmt = totAmt + totTax + totCess
            lblTotAmt.Text = Format(totItm, numFormat)
            txttotalItemAmt.Text = Format(totAmt, numFormat)
            lblitemcost.Text = Format(totAmt, numFormat)
            lbltax.Text = Format(totTax, numFormat)
            lblcess.Text = Format(totCess, numFormat)
            lbltaxable.Text = Format(totTaxableAmt, numFormat)
        End With
        calculateJobvalue()
    End Sub


    Private Sub btnrem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrem.Click
        RemoveRow()
    End Sub

    Private Sub RemoveRow()
        If grdVoucher.RowCount > 0 Then
            If MsgBox("You Are Going to Remove the Row." & vbCrLf & "Are You Sure ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then
                Exit Sub
            End If
            With grdVoucher
                .Rows.RemoveAt(.CurrentRow.Index)
                .ClearSelection()
                '.Rows(.CurrentRow.Index).Selected = True
                calculate()
            End With
            reArrangeNo()
        End If
        ischgItm = True
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
    Private Sub reArrangeNoImei()
        Dim r As Integer
        Dim i As Integer
        chgbyprg = True
        i = 0
        With grdImei
            For r = 0 To .Rows.Count - 1 '- 1
                If CStr(.Item(ConstSlNo, r).Value) <> "M" And CStr(.Item(ConstSlNo, r).Value) <> "L" Then
                    i = i + 1
                    .Item(ConstSlNo, r).Value = i
                End If
            Next r
        End With
        chgbyprg = False
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        If chkFormat.Checked Then
            fRptFormat = New RptFormatfrm
            fRptFormat.RptType = "JB"
            fRptFormat.ShowDialog()
        Else
            PrepareRpt("JB")
        End If
    End Sub
    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        LoadReport(RptFlName, RptCaption)
    End Sub
    Private Sub fRptFormat_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fRptFormat.FormClosed
        fRptFormat = Nothing
    End Sub
    Private Sub PrepareRpt(ByVal RptType As String, Optional ByVal Forprint As Boolean = False)

        If txtprintjob.Text = "" Then
            MsgBox("Enter a valid Job Code !!", vbInformation)
            txtprintjob.Focus()
            Exit Sub
        End If
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
        _objJob = New clsJob
        _objJob.jobcode = txtprintjob.Text
        Dim ds As DataSet = _objJob.returnJobForInvPrint(0)
        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = fMainForm
        frm.Text = RptCaption
        frm.Show()

    End Sub

    Private Sub frm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles frm.FormClosed
        frm = Nothing
    End Sub

    Public Sub AddRowImei(Optional ByVal IMEI As String = "", Optional ByVal Warrenty As String = "", Optional ByVal customerid As Long = 0)
        Dim i As Integer
        loadCustomerDetails(customerid)
        With grdImei
            If .ColumnCount = 0 Then setImeiGrid()
            activecontrolname = "grdImei"
            i = .RowCount '- 1
            .Rows.Add(1)
            .Item(ConstSlNo, i).Value = i + 1
            .Item(ConstImeino, i).Value = IMEI
            .Item(ConstModel, i).Value = ""
            .Item(ConstBt, i).Value = ""
            .Item(ConstCv, i).Value = ""
            .Item(ConstBX, i).Value = ""
            .Item(ConstMc, i).Value = ""
            .Item(ConstComplaints, i).Value = ""
            .Item(ConstObservation, i).Value = ""
            .Item(ConstTechRemarks, i).Value = ""
            .Item(ConstWithWarrenty, i).Value = Warrenty
            .Item(ConstCancelWarrenty, i).Value = ""
            .Item(ConstJbImeiid, i).Value = ""
            If IMEI <> "" Then
                .CurrentCell = .Item(2, i)
            Else
                .CurrentCell = .Item(ConstImeino, i)
            End If

            chgbyprg = True
            .BeginEdit(True)
            chgbyprg = False
        End With


    End Sub

    Private Sub btnaddimei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddimei.Click
        AddRowImei()
    End Sub
    Private Sub RemoveRowImei()
        If grdImei.CurrentRow Is Nothing Then Exit Sub
        If grdImei.RowCount > 0 Then
            If grdImei.Item(ConstDelivered, grdImei.CurrentRow.Index).Value = "Yes" Then
                MsgBox("Delivered Item cannot be removed", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            If MsgBox("You Are Going to Remove the Row." & vbCrLf & "Are You Sure ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then
                Exit Sub
            End If
            With grdImei
                .Rows.RemoveAt(.CurrentRow.Index)
                .ClearSelection()
            End With
            reArrangeNoImei()
        End If

    End Sub

    Private Sub btnremove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnremove.Click
        RemoveRowImei()
    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            Const WM_KEYDOWN As Integer = &H100
            Const WM_KEYUP As Integer = &H101
            'msg.ToString()
            If msg.Msg = WM_KEYDOWN Or msg.Msg = WM_KEYUP Then
                If msg.WParam.ToInt32() = CInt(Keys.F8) Then
                    UpdateClick()
                ElseIf activecontrolname = "grdImei" Then
                    If msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up) Or msg.WParam.ToInt32() = CInt(Keys.Enter) Or msg.WParam.ToInt32() = CInt(Keys.F2) Or msg.WParam.ToInt32() = CInt(Keys.F3) Or msg.WParam.ToInt32() = CInt(Keys.F4) Then
                        grdImei_KeyDown(Nothing, New KeyEventArgs(keyData))
                        Return True
                    End If
                ElseIf activecontrolname = "grdVoucher" Then
                    If msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up) Or msg.WParam.ToInt32() = CInt(Keys.Enter) Or msg.WParam.ToInt32() = CInt(Keys.F2) Or msg.WParam.ToInt32() = CInt(Keys.F3) Or msg.WParam.ToInt32() = CInt(Keys.F4) Or msg.WParam.ToInt32() = CInt(Keys.F6) Then
                        If (msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up)) And plsrch.Visible = False Then GoTo ctn
                        grdVoucher_KeyDown(Nothing, New KeyEventArgs(keyData))
                        Return True
                    End If
                End If
            End If
ctn:
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub grdImei_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdImei.CellClick
        If e.ColumnIndex = ConstBt Or e.ColumnIndex = ConstCv Or e.ColumnIndex = ConstBX Or e.ColumnIndex = ConstMc Or e.ColumnIndex = ConstWithWarrenty Or e.ColumnIndex = ConstCancelWarrenty Or e.ColumnIndex = ConstPen Then
            If grdImei.Item(e.ColumnIndex, e.RowIndex).Value = "Y" Then
                grdImei.Item(e.ColumnIndex, e.RowIndex).Value = ""
            Else
                grdImei.Item(e.ColumnIndex, e.RowIndex).Value = "Y"
            End If
        End If
        chgbyprg = True
        grdImei.BeginEdit(True)
        chgbyprg = False
    End Sub

    Private Sub grdImei_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdImei.GotFocus
        activecontrolname = "grdImei"
    End Sub


    Private Sub grdImei_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdImei.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If FindNextCell(grdImei, grdImei.CurrentCell.RowIndex, grdImei.CurrentCell.ColumnIndex + 1) Then
                    AddRowImei()
                End If
                chgbyprg = True
                grdImei.BeginEdit(True)
                chgbyprg = False
            ElseIf e.KeyCode = Keys.F3 Then
                AddRow()
            ElseIf e.KeyCode = Keys.F4 Then
                If grdVoucher.RowCount = 0 Then Exit Sub
                RemoveRowImei()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub grdImei_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdImei.Leave
        activecontrolname = ""
    End Sub

    Private Sub txtserviceCost_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtserviceCharge.TextChanged
        calculate()
    End Sub

    Private Sub txttechnician_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txttechnician.Validated
        If chgbyprg Then Exit Sub
        If txttechnician.Text = "" Then Exit Sub
        Dim dt As DataTable
        _objcmnbLayer = New clsCommon_BL
        dt = _objcmnbLayer._fldDatatable("SELECT SManCode,SManName from SalesmanTb  where SManName='" & txttechnician.Text & "'")
        chgbyprg = True
        If dt.Rows.Count > 0 Then
            txttechnician.Tag = dt(0)("SManCode")
            txttechnician.Text = dt(0)("SManName")
        Else
            txttechnician.Text = ""
        End If
        chgbyprg = False
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
    End Sub

    Private Sub dtpdate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtpdate.KeyDown, dtpestimatedDt.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If

    End Sub

    Private Sub txtcost_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        chkcal.Tag = ""
    End Sub



    Private Sub ldItemCost()
        Dim num As Double
        _objcmnbLayer = New clsCommon_BL
        Dim dt As DataTable = _objcmnbLayer._fldDatatable("select sum(CostAvg*TrQty) costAmt from ItmInvTrTb  where trid=" & Val(numsto.Tag), False)
        If (dt.Rows.Count > 0) Then
            If Val(dt(0)("costAmt") & "") = 0 Then dt(0)("costAmt") = 0
            num = CDbl(dt(0)("costAmt"))
        End If
        lblitmcost.Text = Format(num, numFormat)
        calculateCost()
    End Sub
    Private Sub calculateCost()
        If Not chgbyprg Then
            If Val(txtserviceCharge.Text) = 0 Then
                txtserviceCharge.Text = 0
            End If
            If Val(lblitmcost.Text) = 0 Then
                lblitmcost.Text = 0
            End If
            lbljobcost.Text = Format(CDbl(txtscost.Text) + CDbl(lblitmcost.Text), numFormat)
        End If
    End Sub

    Public Sub fillGrid()
        Dim num2 As Double
        Dim strSql As String = ("Select  convert(varchar(12), case when prefix=''  then '' else '-' end +  invNo ) as [Inv No] , TrDate [Tr.Date] ,Alias [Cust. Code],AccDescr [Customer Name],(InvAmt-Discount) [Amount],TrRefNo [Ref. No],TrDescription  [Tr. Description],jobcode,UserId [Created By],TrId from ( select  prefix, invNo ,TrDate ,InvAmt,Discount,AccDescr ,TrRefNo,TrDescription,Alias ,JobInvCmntb.UserId,JOBCODE,JobInvCmntb.TrId from JOBInvCmntb LEFT JOIN JobTb ON JobInvCmntb.jobid=JobTb.jobid  LEFT JOIN (SELECT Trid,SUM((TrQty*(UnitCost+UnitOthCost))+taxamt)InvAmt FROM JobInvTrTb GROUP BY Trid) Tr ON  JobInvCmntb.Trid=Tr.Trid left join Accmast on JobInvCmnTb.custid=Accmast.accid where JobInvCmntb.trtype='JIS' and JobInvCmntb.jobid=" & Val(numVchrNo.Tag) & ") as qq  order by TrDate ,InvNo")
        grdinvList.DataSource = Nothing
        _objcmnbLayer = New clsCommon_BL
        Dim source As DataTable = Me._objcmnbLayer._fldDatatable(strSql, False)
        grdinvList.DataSource = source
        SetGridHeadInv()
        chgbyprg = False
        Dim num3 As Integer = (source.Rows.Count - 1)
        Dim i As Integer = 0
        Do While (i <= num3)
            num2 = num2 + source(i)("Amount")
            i += 1
        Loop
        lblinvamt.Text = Format(num2, numFormat)
    End Sub

    Private Sub SetGridHeadInv()

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
    End Sub

    Private Sub btndelivery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelivery.Click
        If Val(btndelivery.Tag) = 0 Then
            MsgBox("This user do not have permission to Delivery Entry", MsgBoxStyle.Exclamation, Nothing)
        Else
            If Not fDelivery Is Nothing Then
                fDelivery = Nothing
            End If
            fDelivery = New JobDelivery
            Dim jbDescription As String = ("Job Code:" & numVchrNo.Text & "\Job Name: " & txtJobname.Text)
            fDelivery.ldRec(Val(numVchrNo.Tag), jbDescription)
            fDelivery.ShowDialog()
        End If
        ldRec(Val(numVchrNo.Tag))
    End Sub

    Private Sub btninvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btninvoice.Click
        If userType Then
            btninvoice.Tag = IIf(getRight(86, CurrentUser), 1, 0)
        Else
            btninvoice.Tag = 1
        End If
        If Val(btninvoice.Tag) = 0 Then
            MsgBox("This user do not have permission to Create New Invoice", MsgBoxStyle.Exclamation, Nothing)
        ElseIf Val(numVchrNo.Tag) = 0 Then
            MsgBox("Invalid Job", MsgBoxStyle.Exclamation, Nothing)
        Else
            If grdinvList.RowCount > 0 Then
                If MsgBox("Invoice Found against this job, Do you want to create new Invoice?", MsgBoxStyle.Question & MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            End If
            If (Not fInvoice Is Nothing) Then
                fInvoice = Nothing
            End If
            fInvoice = New JobSalesInvoice
            fInvoice.MdiParent = fMainForm
            fInvoice.plvehicle.Visible = False
            fInvoice.Show()
            fInvoice.returnFromJob(Val(numVchrNo.Tag), IIf(chkwS.Checked, 0, CDbl(txtserviceCharge.Text)), rdoserviceinv.Checked, Val(numsto.Tag))
        End If
    End Sub

    Private Sub fInvoice_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fInvoice.FormClosed
        fInvoice = Nothing
        ldRec(Val(numVchrNo.Tag))
    End Sub

    Private Sub grdinvList_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdinvList.DoubleClick
        fMainForm.LoadJIS(Val(grdinvList.Item(grdinvList.ColumnCount - 1, grdinvList.CurrentRow.Index).Value))
    End Sub

    Private Sub fCrtAcc_OpenAccMaster(ByRef AccountNo As Long) Handles fCrtAcc.OpenAccMaster
        loadCustomerDetails(AccountNo)
    End Sub
    Private Sub loadCustomerDetails(ByVal accid As Long)
        Dim dt As DataTable
        chgbyprg = True
        _objcmnbLayer = New clsCommon_BL
        dt = _objcmnbLayer._fldDatatable("SELECT accid,Alias,AccDescr,Address1,Address2,Address3,Address4,Phone,Fax,EMail,PT1,PT2,PT3,PT4," & _
                                         "TrdLcno,TrdDate,ContactName from AccMast LEFT JOIN AccMastAddr ON AccMast.ACCID=AccMastAddr.accountno " & _
                                         IIf(accid > 0, "where accid=" & accid, "where AccDescr='" & txtcustomer.Text & "'"))
        If dt.Rows.Count > 0 Then
            txtcustomer.Tag = dt(0)("accid")
            txtcustomer.Text = dt(0)("AccDescr")
            txtaddress.Text = dt(0)("Address1") & vbCrLf & dt(0)("Address2") & vbCrLf & dt(0)("Address3") & vbCrLf & dt(0)("Phone") & vbCrLf & dt(0)("ContactName")
        Else
            txtcustomer.Text = ""
        End If
        dtpestimatedDt.Focus()
    End Sub

    Private Sub grdVoucher_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellClick
        chgbyprg = True
        With grdVoucher
            If Val(grdVoucher.Item(0, grdVoucher.CurrentCell.RowIndex).Value) = 0 And e.ColumnIndex <> 3 Then
                grdVoucher.CurrentCell.ReadOnly = True
            ElseIf e.ColumnIndex <> ConstSlNo And e.ColumnIndex <> constItmTot And e.ColumnIndex <> ConstUnit And e.ColumnIndex <> ConstLTotal And e.ColumnIndex <> ConstBarcode And e.ColumnIndex <> ConstTaxAmt And e.ColumnIndex <> ConstStartReading Then
                If e.ColumnIndex = ConstTaxP And EnableGST Then
                    grdVoucher.CurrentCell.ReadOnly = True
                Else
                    grdVoucher.CurrentCell.ReadOnly = False
                End If
            ElseIf e.ColumnIndex = ConstSlNo And enableBatchwiseTr Then
                grdVoucher.CurrentCell.ReadOnly = False
            Else
                grdVoucher.CurrentCell.ReadOnly = True
            End If
        End With
        grdBeginEdit()
    End Sub

    Private Sub grdVoucher_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellDoubleClick
        Dim dt As DataTable = Nothing
        With grdVoucher
            If e.ColumnIndex = ConstSlNo Then
                .Item(ConstSlNo, .CurrentCell.RowIndex).Value = IIf(Val(.Item(ConstSlNo, .CurrentCell.RowIndex).Value) = 0, "", "M")
                reArrangeNo()
            ElseIf e.ColumnIndex = ConstUnit Then
                If .Item(ConstB, .CurrentCell.RowIndex).Value = "B" Then
                    .Item(ConstB, .CurrentCell.RowIndex).Value = "P1"
                    dt = _objcmnbLayer._fldDatatable("SELECT P1Unit U,P1Fra F,LastPurchCost FROM InvItm WHERE Itemid=" & Val(.Item(ConstItemID, .CurrentCell.RowIndex).Value))
                ElseIf .Item(ConstB, .CurrentCell.RowIndex).Value = "P1" Then
                    .Item(ConstB, .CurrentCell.RowIndex).Value = "P2"
                    dt = _objcmnbLayer._fldDatatable("SELECT P2Unit U,P2Fra F,LastPurchCost FROM InvItm WHERE Itemid=" & Val(.Item(ConstItemID, .CurrentCell.RowIndex).Value))
                ElseIf .Item(ConstB, .CurrentCell.RowIndex).Value = "P2" Then
u:
                    .Item(ConstB, .CurrentCell.RowIndex).Value = "B"
                    dt = _objcmnbLayer._fldDatatable("SELECT Unit U,1 F,LastPurchCost FROM InvItm WHERE Itemid=" & Val(.Item(ConstItemID, .CurrentCell.RowIndex).Value))
                End If
                If dt Is Nothing Then GoTo u
                If dt.Rows.Count > 0 Then
                    .Item(ConstUnit, .CurrentCell.RowIndex).Value = dt(0)("U")
                    .Item(ConstPMult, .CurrentCell.RowIndex).Value = dt(0)("F")
                    Dim cost As Double
                    cost = getPurchAmt(dt(0)("LastPurchCost"), 0, Val(.Item(ConstItemID, .CurrentCell.RowIndex).Value))
                    If Val(.Item(ConstPMult, .CurrentCell.RowIndex).Value & "") = 0 Then
                        .Item(ConstPMult, .CurrentCell.RowIndex).Value = 0
                    End If
                    cost = cost * Val(.Item(ConstPMult, .CurrentCell.RowIndex).Value)
                    .Item(ConstActualPrice, .CurrentCell.RowIndex).Value = cost
                    .Item(ConstUPrice, .CurrentCell.RowIndex).Value = Format(cost, numFormat)
                    calculate(, True)
                End If

            End If
        End With
    End Sub

    Private Sub grdVoucher_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellEndEdit
        Dim col As Integer = e.ColumnIndex
        Dim ndec1 As Integer
        chgbyprg = True
        If col = ConstQty Or col = ConstUPrice Or col = ConstDisAmt Or col = ConstLTotal Or col = ConstTaxP Or col = ConstDisP Or col = ConstEndReading Or col = ConstWoodDiscQty Or col = ConstWoodQty Then
            If col = ConstQty Or col = ConstEndReading Or col = ConstWoodDiscQty Or col = ConstWoodQty Then
                ndec1 = Val(grdVoucher.Item(ConstPFraction, grdVoucher.CurrentRow.Index).Value & "")
            Else
                ndec1 = 2
            End If
            If Val(grdVoucher.Item(col, e.RowIndex).Value) = 0 Then grdVoucher.Item(col, e.RowIndex).Value = 0
            grdVoucher.Item(col, e.RowIndex).Value = Format(Convert.ToDecimal(grdVoucher.Item(col, e.RowIndex).Value), "#,##0" & IIf(ndec1 = 0, "", "." & Strings.StrDup(ndec1, "0")))
        End If
        chgbyprg = False
    End Sub

    Private Sub grdVoucher_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellValidated
        Try
            Valid(e.RowIndex, e.ColumnIndex)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try

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


            .Item(ConstB, .CurrentCell.RowIndex).Value = "B"
            .Item(ConstPFraction, i).Value = IIf(IsDBNull(DR(0)("FraCount")), "2", DR(0)("FraCount"))
            .Item(ConstUnit, i).Value = DR(0)("Unit")
            .Item(ConstWarrentyExpiry, i).Value = Format(DateAdd(DateInterval.Year, 1, DateValue(dtpdate.Value)), DtFormat)
            If CDbl(.Item(ConstUPrice, i).Value) = 0 Or chgItm Then
                If chkws.Checked Then
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
            '.Item(ConstTaxP, i).Value = Format(IIf(IsDBNull(DR(0)("vat")), 0, CDbl(DR(0)("vat"))), numformat)
            '.Item(ConstTaxAmt, i).Value = ((CDbl(.Item(ConstActualPrice, i).Value) * .Item(ConstTaxP, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value)
            If ShowTaxOnInventory Then
                If Val(DR(0)("vat") & "") = 0 Then
                    DR(0)("vat") = 0
                End If
                .Item(ConstTaxP, i).Value = Format(IIf(IsDBNull(DR(0)("vat")), 0, CDbl(DR(0)("vat"))), numFormat)
                .Item(ConstTaxAmt, i).Value = ((CDbl(.Item(ConstActualPrice, i).Value) * .Item(ConstTaxP, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value)
                .Item(ConstIGSTP, i).Value = IIf(IsDBNull(DR(0)("vat")), 0, CDbl(DR(0)("vat")))
                .Item(ConstcessAc, i).Value = IIf(IsDBNull(DR(0)("collectionAC")), 0, Val(DR(0)("collectionAC")))
            ElseIf EnableGST Then
                getGSTDetails(Trim(.Item(ConstBarcode, i).Value & ""), i, False)
            End If
            If enablecess And cessdate <= DateValue(dtpdate.Value) Then
                .Item(Constcess, i).Value = Format(IIf(IsDBNull(DR(0)("vat")), 0, CDbl(DR(0)("vat"))), numFormat)
                .Item(ConstcessAc, i).Value = IIf(IsDBNull(DR(0)("collectionAC")), 0, Val(DR(0)("collectionAC")))
                .Item(ConstcessAmt, i).Value = Format(((CDbl(.Item(ConstActualPrice, i).Value) * .Item(Constcess, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value), numFormat)
            End If
            .Item(ConstLTotal, i).Value = Format(.Item(ConstQty, i).Value * CDbl(.Item(ConstUPrice, i).Value), numFormat)
            .Item(ConstNUPrice, i).Value = Format(CDbl(.Item(ConstUPrice, i).Value), numFormat)
            .Item(ConstQty, i).Value = Format(CDbl(.Item(ConstQty, i).Value), "#,##0" & IIf(Val(.Item(ConstPFraction, i).Value) = 0, "", "." & Strings.StrDup(CByte(.Item(ConstPFraction, i).Value), "0")))
            .Item(ConstImpDocId, i).Value = ""
            .Item(ConstImpLnId, i).Value = ""
            chgAmt = True
            chgItm = False
            .ClearSelection()
            'checkItemQty(i)
            'If diableNegativeSale Then
            '    .CurrentCell = .Item(ConstQty, i)
            'End If

        End With
        calculate(, True)
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

                    End If
                    calculate(, True)
                Case ConstUPrice
                    If chgAmt Then
                        If Format(.Item(ConstActualPrice, RowIndex).Value, numFormat) <> Format(CDbl(.Item(ConstUPrice, RowIndex).Value), numFormat) Then
                            .Item(ConstActualPrice, RowIndex).Value = CDbl(.Item(ConstUPrice, RowIndex).Value)
                        End If
                        If AllowUnitDiscountEntryOnInventory Then
                            .Item(ConstDisAmt, RowIndex).Value = Format(((CDbl(.Item(ConstActualPrice, RowIndex).Value) * Val(.Item(ConstDisP, RowIndex).Value)) / 100) * CDbl(.Item(ConstQty, RowIndex).Value), numFormat)
                        End If
                        If EnableGST Then getGSTDetails(Trim(.Item(ConstBarcode, RowIndex).Value & ""), RowIndex, True)
                        calculate(, True)
                    End If
                Case ConstDisP
                    If chgAmt Then
                        .Item(ConstDisAmt, RowIndex).Value = Format(((CDbl(.Item(ConstActualPrice, RowIndex).Value) * Val(.Item(ConstDisP, RowIndex).Value)) / 100) * CDbl(.Item(ConstQty, RowIndex).Value), numFormat)
                        If EnableGST Then getGSTDetails(Trim(.Item(ConstBarcode, RowIndex).Value & ""), RowIndex, True)
                        calculate(, True)
                    End If
                Case ConstDisAmt
                    If chgAmt Then
                        Dim unitDiscount As Double
                        unitDiscount = CDbl(.Item(ConstDisAmt, RowIndex).Value) / CDbl(.Item(ConstQty, RowIndex).Value)
                        .Item(ConstDisP, RowIndex).Value = Format((unitDiscount * 100) / CDbl(.Item(ConstActualPrice, RowIndex).Value), numFormat)
                        If EnableGST Then getGSTDetails(Trim(.Item(ConstBarcode, RowIndex).Value & ""), RowIndex, True)
                        calculate(, True)
                    End If
                Case ConstTaxAmt
                    If chgAmt Then
                        calculate(, True)
                    End If
                Case ConstTaxP
                    If chgAmt Then
                        calculate(, True)
                    End If
                Case Else
            End Select
        End With
        chgAmt = False
    End Sub

    Private Sub grdVoucher_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellValueChanged
        If chgbyprg Then Exit Sub
        'ChgByPrg = True
        With grdVoucher
            Dim i As Integer = e.RowIndex
            'btnUpdate.Enabled = True
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
                            calculate(, True)
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
    End Sub

    Private Sub calculateTaxFromUnitPrice(ByVal i As Integer)
        If chgUprice And chkcal.Checked Then
            chgbyprg = True
            With grdVoucher
                .Item(ConstActualPrice, i).Value = (CDbl(.Item(ConstUPrice, i).Value) * 100) / (CDbl(.Item(ConstTaxP, i).Value) + CDbl(.Item(Constcess, i).Value) + 100)
                .Item(ConstUPrice, i).Value = Format(.Item(ConstActualPrice, i).Value, numFormat)
            End With
        End If
        chgUprice = False
        chgbyprg = False
    End Sub
    Private Sub Textbox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            Dim col As Integer
            col = grdVoucher.CurrentCell.ColumnIndex
            If col = ConstQty Or col = ConstNUPrice Or col = ConstLTotal Then
                If Not IsNumeric(e.KeyChar) And e.KeyChar <> "." And Not e.KeyChar = Convert.ToChar(Keys.Back) Then e.Handled = True
                If col = ConstQty Then
                    If grdVoucher.Item(ConstSerialNo, grdVoucher.CurrentRow.Index).Value <> "" And Not enableBatchwiseTr Then
                        e.Handled = True
                    End If
                End If
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
                '_srchIndexId = 1
                chgbyprg = True
                strGridSrchString = MyCtrl.Text
                ShowPanel()
                chgItm = True
                chgbyprg = False
                grdVoucher.Item(ConstqtyChg, grdVoucher.CurrentRow.Index).Value = "Chg"
            ElseIf col = ConstBarcode Then
                _srchTxtId = 2
                '_srchIndexId = 2
                chgbyprg = True
                strGridSrchString = MyCtrl.Text
                ShowPanel()
                chgbyprg = False
                'ElseIf col = ConstSerialNo Then
                '    _srchTxtId = 3
                '    '_srchIndexId = 3
                '    chgbyprg = True
                '    strGridSrchString = MyCtrl.Text
                '    ShowPanel()
                '    chgbyprg = False
            ElseIf col = ConstQty Then
                grdVoucher.Item(ConstqtyChg, grdVoucher.CurrentRow.Index).Value = "Chg"
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub ShowPanel(Optional ByVal isrefreshBatchData As Boolean = False)
        If Not _srchOnce Then
            chgbyprg = True
            Dim PopupLoc As Point
            Dim x As Integer
            Dim y As Integer
            If _srchTxtId = 3 Then
                plsrch.Height = 246
                plsrch.Width = 450
            Else

                plsrch.Height = 300
                plsrch.Width = 700
                'x = Me.Width - plsrch.Width - 100
                'y = Me.Height - plsrch.Height - 100
            End If
            x = grdVoucher.Left + grdVoucher.Width - plsrch.Width
            y = grdVoucher.Top
            PopupLoc = New Point(x, y)
            If plsrch.Visible = False Then
                plsrch.Location = PopupLoc
                plsrch.Visible = True
            End If
        End If
        If _srchTxtId = 3 And enableBatchwiseTr Then
            searchProductBatch(grdSrch, strGridSrchString, Val(grdVoucher.Item(ConstItemID, grdVoucher.CurrentRow.Index).Value), isrefreshBatchData)
        Else
            SearchProductPanel(grdSrch, "Item Code", strGridSrchString, True)
        End If
        If grdSrch.RowCount > 0 And grdVoucher.CurrentCell.ColumnIndex = ConstSerialNo And enableBatchwiseTr And strGridSrchString = "" Then
            strGridSrchString = grdSrch.Item(2, 0).Value
        End If
        doSelect(2)
        _srchOnce = True
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

    Private Sub grdVoucher_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.GotFocus
        'If grdVoucher.RowCount = 0 Then
        '    AddRow()
        'End If
        activecontrolname = "grdVoucher"
    End Sub
    Private Sub grdVoucher_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdVoucher.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If grdVoucher.RowCount = 0 Then Exit Sub
                plsrch.Visible = False
                If SrchText = "" And grdVoucher.CurrentCell.ColumnIndex = ConstItemCode Then
                    SrchText = grdVoucher.Item(ConstItemCode, grdVoucher.CurrentRow.Index).Value
                    If SrchText = "" Then GoTo nxt
                End If
                If FindNextCell(grdVoucher, grdVoucher.CurrentCell.RowIndex, grdVoucher.CurrentCell.ColumnIndex + 1) Then
                    AddRow()
                End If
nxt:

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
            ElseIf e.KeyCode = Keys.F1 Then
                grdBeginEdit()
            ElseIf e.KeyCode = Keys.F2 Then
                If plsrch.Visible Then plsrch.Visible = False
                If grdVoucher.RowCount = 0 Then Exit Sub
                Select Case grdVoucher.CurrentCell.ColumnIndex
                    Case ConstItemCode
                        If Not fMList Is Nothing Then fMList.Visible = False
                        If plsrch.Visible Then plsrch.Visible = False
                        fProductEnquiry = New ItmEnqry
                        fProductEnquiry.ShowDialog()
                End Select
            ElseIf e.KeyCode = Keys.Escape Then
                plsrch.Visible = False
            ElseIf e.KeyCode = Keys.F3 Then
                AddRow()
            ElseIf e.KeyCode = Keys.F4 Then
                RemoveRow()
            ElseIf e.KeyCode = Keys.F6 Then
                'modifyItem(Val(grdVoucher.Item(ConstItemID, grdVoucher.CurrentRow.Index).Value))
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
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
                Case 3
                    grdVoucher.Item(ConstSerialNo, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(2) IsNot Nothing, ItmFlds(2), "")
                    'grdVoucher.Item(ConstBarcode, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(1) IsNot Nothing, ItmFlds(1), "")
                    grdVoucher.Item(ConstManufacturingdate, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(3) IsNot Nothing, ItmFlds(3), "")
                    grdVoucher.Item(ConstWarrentyExpiry, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(4) IsNot Nothing, ItmFlds(4), "")
                    grdVoucher.Item(ConstBatchQty, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(2) IsNot Nothing, ItmFlds(2), "")
                    SrchText = IIf(ItmFlds(2) IsNot Nothing, ItmFlds(2), strGridSrchString)
            End Select
nxt:
            chgbyprg = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
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
            'actualPrice = (CDbl(.Item(ConstActualPrice, i).Value) * CDbl(.Item(ConstQty, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value) - discountOther
            actualPrice = Format(actualPrice, numFormat)
            .Item(ConstCGSTAmt, i).Value = (actualPrice * .Item(ConstCGSTP, i).Value) / 100
            .Item(ConstSGSTAmt, i).Value = (actualPrice * .Item(ConstSGSTP, i).Value) / 100
            .Item(ConstIGSTAmt, i).Value = (actualPrice * .Item(ConstIGSTP, i).Value) / 100
            .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstCGSTAmt, i).Value) + CDbl(.Item(ConstSGSTAmt, i).Value), numFormat)
            .Item(ConstTaxP, i).Value = CDbl(.Item(ConstCGSTP, i).Value) + CDbl(.Item(ConstSGSTP, i).Value)
        End With
    End Sub

    Private Sub grdVoucher_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.Leave
        activecontrolname = ""
    End Sub

    Private Sub TabControl2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl2.Click
        If TabControl2.SelectedIndex = 1 Then
            btnnew.Enabled = False
            btnmodify.Enabled = False
            btnupdate.Enabled = False
            btndelete.Enabled = False

            resizeGridColumn(grdVoucher, ConstDescr)
        ElseIf TabControl2.SelectedIndex = 0 Then
            btnnew.Enabled = True
            btnmodify.Enabled = True
            btnupdate.Enabled = True
            btndelete.Enabled = True


        ElseIf TabControl2.SelectedIndex = 2 Then
            fillGrid()
        End If
    End Sub

    Private Sub btnsto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsto.Click
        saveJobItems(True)
    End Sub

    Private Sub chktaxInv_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chktaxInv.Click
        calculate()
    End Sub

   

    Private Sub fProductEnquiry_getSelected(ByVal ItemcODE As String, ByVal isacc As Integer) Handles fProductEnquiry.getSelected
        chgbyprg = True
        fProductEnquiry.Close()
        SrchText = ItemcODE
        grdVoucher.Item(ConstItemCode, grdVoucher.CurrentRow.Index).Value = ItemcODE
        chgItm = True
        Valid(grdVoucher.CurrentRow.Index, ConstItemCode)
        grdVoucher.CurrentCell = grdVoucher.Item(3, grdVoucher.CurrentRow.Index)
        grdVoucher.BeginEdit(True)
        chgbyprg = False
    End Sub

    Private Sub btnjobclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnjobclose.Click
        If btnjobclose.Text = "Job Close" Then
            If Val(btnjobclose.Tag) = 0 Then
                MsgBox("This user do not have permission to Close Job", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            Dim frm As New JobClosing
            frm.lbljjob.Tag = Val(numVchrNo.Tag)
            frm.lbljjob.Text = "Job Code : " & numVchrNo.Text
            frm.ShowDialog()
        Else
            If MsgBox("Do you want to close this job?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            _objcmnbLayer._saveDatawithOutParm("Update JobTb set Status=0 where Jobid=" & Val(numVchrNo.Tag))

        End If
        ldRec(Val(numVchrNo.Tag))
    End Sub

    Private Sub btnSlct_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSlct.Click
        fselectJob = New JobEnqryFrm
        fselectJob.ShowDialog()
        fselectJob = Nothing
    End Sub

    Private Sub fselectJob_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fselectJob.FormClosed
        fselectJob = Nothing
    End Sub

    Private Sub fselectJob_sendJobid(ByVal jobid As Long) Handles fselectJob.sendJobid
        ldRec(jobid)
    End Sub

    Private Sub btnmodify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnmodify.Click
        If btnmodify.Text = "Modify" Then
            makeClear()
            btnSlct.Visible = True
            btnmodify.Text = "Undo"
            isModi = True
            numVchrNo.Focus()
        Else
            makeClear()
            btnSlct.Visible = False
            btnmodify.Enabled = True
            btnmodify.Text = "Modify"
            numVchrNo.Text = ""
            AddNew()
        End If

    End Sub

    Private Sub numVchrNo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles numVchrNo.Validated
        ischgJobcode = False
    End Sub

  

    Private Sub grdSrch_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSrch.CellDoubleClick
        activecontrolname = "grdVoucher"
        doSelect(2)
        chgbyprg = True
        grdVoucher.CurrentCell = grdVoucher.Item(1, grdVoucher.CurrentRow.Index)
        chgItm = True
        Valid(grdVoucher.CurrentCell.RowIndex, grdVoucher.CurrentCell.ColumnIndex)
        chgbyprg = False
        grdVoucher.BeginEdit(True)
        plsrch.Visible = False
    End Sub


    Private Function saveInvTr(ByVal Invid As Long) As Double
        If dtInvTb Is Nothing Then
            dtInvTb = _objcmnbLayer._fldDatatable("Select top 1 * from ItmInvTrTb")
        End If
        Dim dtrow As DataRow
        dtInvTb.Rows.Clear()
        Dim i As Integer
        Dim PPerU As Single
        Dim TDrAmt As Double
        Dim ImpJobChildTbIDs As String = ""
        With grdVoucher
            For i = 0 To .Rows.Count - 1
                If .Item(ConstSlNo, i).Value.ToString <> "M" And Val(.Item(ConstItemID, i).Value) = 0 Then GoTo nxtM
                PPerU = Val(.Item(ConstPMult, i).Value) 'Switch(.TextMatrix(i, 4) = "B", 1, .TextMatrix(i, 4) = "P1", CSng(grdVoucher.TextMatrix(i, 18)), .TextMatrix(i, 4) = "P2", CSng(grdVoucher.TextMatrix(i, 19)))
                PPerU = IIf(PPerU = 0, 1, PPerU)

                dtrow = dtInvTb.NewRow
                dtrow("TrId") = Invid
                dtrow("ItemId") = IIf(.Item(ConstSlNo, i).Value.ToString <> "M", Val(.Item(ConstItemID, i).Value), 0)
                dtrow("Unit") = .Item(ConstUnit, i).Value
                dtrow("TrQty") = CDbl(.Item(ConstQty, i).Value) * PPerU
                dtrow("Focqty") = CDbl(.Item(ConstFocQty, i).Value) * PPerU
                dtrow("UnitCost") = CDbl(.Item(ConstActualPrice, i).Value) * FCRt / PPerU 'CDbl(.TextMatrix(i, 10)) / PPerU

                TDrAmt = TDrAmt + CDbl(.Item(ConstQty, i).Value) * (CDbl(.Item(ConstActualPrice, i).Value))
                TDrAmt = TDrAmt - CDbl(.Item(ConstDisAmt, i).Value)

                dtrow("taxP") = CDbl(grdVoucher.Item(ConstTaxP, i).Value)
                dtrow("taxAmt") = (CDbl(grdVoucher.Item(ConstTaxAmt, i).Value) * FCRt)
                dtrow("PFraction") = PPerU
                If Val(.Item(ConstUnitOthCost, i).Value) = 0 Then .Item(ConstUnitOthCost, i).Value = 0
                dtrow("UnitOthCost") = CDbl(.Item(ConstUnitOthCost, i).Value) * FCRt / PPerU
                dtrow("Method") = .Item(ConstB, i).Value
                dtrow("UnitDiscount") = Val(.Item(ConstDiscOther, i).Value) * FCRt / PPerU
                If Val(.Item(ConstDisAmt, i).Value & "") = 0 Then .Item(ConstDisAmt, i).Value = 0
                dtrow("ItemDiscount") = CDbl(.Item(ConstDisAmt, i).Value) * FCRt / PPerU
                If Val(.Item(ConstDisP, i).Value) = 0 Then .Item(ConstDisP, i).Value = 0
                dtrow("DisP") = CDbl(.Item(ConstDisP, i).Value) * FCRt / PPerU

                If Not IsDBNull(.Item(ConstDescr, i).Value) Then
                    dtrow("IDescription") = IIf(.Item(ConstSlNo, i).Value.ToString = "M", .Item(ConstDescr, i).Value, Trim(.Item(ConstDescr, i).Value))
                End If
                dtrow("SlNo") = i + 1
                dtrow("TrTypeNo") = getVouchernumber("IS") ' getVouchernumber("IS")
                dtrow("TrDateNo") = getDateNo(CDate(dtpdate.Value))
                dtrow("id") = Val(.Item(ConstId, i).Value)
                'dtrow("WarrentyName") = .Item(ConstLocation, i).Value
                '_objInv.SerialNo = Trim(.Item(ConstSerialNo, i).Value & "")
                'If Trim(.Item(ConstSerialNo, i).Value & "") = "" And enableBatchwiseTr Then
                '    dtrow("SerialNo") = "IP" & txtprefix.Text & numVchrNo.Text
                'Else
                '    dtrow("SerialNo") = Trim(.Item(ConstSerialNo, i).Value & "")
                'End If
                dtrow("SerialNo") = Trim(.Item(ConstSerialNo, i).Value & "")
                If IsDate(.Item(ConstWarrentyExpiry, i).Value) Then
                    dtrow("WarrentyExpDate") = DateValue(.Item(ConstWarrentyExpiry, i).Value)
                Else
                    dtrow("WarrentyExpDate") = DateValue("01/01/1950")
                End If
                dtrow("HSNCode") = Trim(.Item(ConstBarcode, i).Value & "")
                If Val(.Item(ConstCGSTP, i).Value & "") = 0 Then .Item(ConstCGSTP, i).Value = 0
                dtrow("CSGTP") = CDbl(.Item(ConstCGSTP, i).Value)
                If Val(.Item(ConstSGSTP, i).Value & "") = 0 Then .Item(ConstSGSTP, i).Value = 0
                dtrow("SGSTP") = CDbl(.Item(ConstSGSTP, i).Value)
                If Val(.Item(ConstIGSTP, i).Value & "") = 0 Then .Item(ConstIGSTP, i).Value = 0
                dtrow("IGSTP") = CDbl(.Item(ConstIGSTP, i).Value)
                .Item(ConstCGSTAmt, i).Value = 0
                .Item(ConstSGSTAmt, i).Value = 0
                .Item(ConstIGSTAmt, i).Value = 0
                .Item(ConstregularCessAmt, i).Value = 0
                .Item(ConstFloodCessAmt, i).Value = 0
                .Item(ConstAdditionalcess, i).Value = 0
                .Item(ConstcessAmt, i).Value = 0
                dtrow("IGSTAmt") = CDbl(.Item(ConstIGSTAmt, i).Value) * FCRt
                dtrow("CGSTAMT") = CDbl(.Item(ConstCGSTAmt, i).Value) * FCRt
                dtrow("SGSTAmt") = CDbl(.Item(ConstSGSTAmt, i).Value) * FCRt
                dtrow("regularcessAmt") = (CDbl(.Item(ConstregularCessAmt, i).Value) * FCRt)
                dtrow("FloodcessAmt") = 0

                dtrow("AdditionalcessAmt") = ((CDbl(.Item(ConstAdditionalcess, i).Value) * CDbl(.Item(ConstQty, i).Value)) * FCRt)
                dtrow("CessAmt") = (CDbl(grdVoucher.Item(ConstcessAmt, i).Value) * FCRt)
                dtrow("Smancode") = Trim(.Item(Constsman, i).Value & "")
                dtrow("impDocid") = Val(.Item(ConstImpDocId, i).Value & "")
                dtrow("impDocSlno") = Val(.Item(ConstImpLnId, i).Value & "")

                If IsDate(.Item(ConstManufacturingdate, i).Value) Then
                    dtrow("manufacturingdate") = DateValue(.Item(ConstManufacturingdate, i).Value)
                Else
                    dtrow("manufacturingdate") = DateValue("01/01/1950")
                End If
                If Val(.Item(ConstMRP, i).Value & "") = 0 Then .Item(ConstMRP, i).Value = 0
                dtrow("MRP") = CDbl(.Item(ConstMRP, i).Value)
                If Val(.Item(ConstSP1, i).Value & "") = 0 Then .Item(ConstSP1, i).Value = 0
                dtrow("SP1") = CDbl(.Item(ConstSP1, i).Value)
                If Val(.Item(ConstSP2, i).Value & "") = 0 Then .Item(ConstSP2, i).Value = 0
                dtrow("SP2") = CDbl(.Item(ConstSP2, i).Value)
                If Val(.Item(ConstSP3, i).Value & "") = 0 Then .Item(ConstSP3, i).Value = 0
                dtrow("SP3") = CDbl(.Item(ConstSP3, i).Value)
                If Val(.Item(ConstBatchCost, i).Value & "") = 0 Then .Item(ConstBatchCost, i).Value = 0
                dtrow("CostAvg") = CDbl(.Item(ConstBatchCost, i).Value)
                If Val(dtrow("ItemId")) = 0 Then
                    dtrow("TrQty") = 1
                    dtrow("UnitCost") = 1
                    dtrow("taxP") = 1
                    dtrow("taxAmt") = 1
                    dtrow("UnitDiscount") = 0
                End If
                dtInvTb.Rows.Add(dtrow)
                Dim j As Integer
                Dim dtype As String
                For j = 0 To dtInvTb.Columns.Count - 1
                    If dtInvTb.Columns(j).ColumnName = "id" Then GoTo nxt
                    dtype = dtInvTb.Columns(j).DataType.Name
                    If Trim(dtInvTb(i)(j) & "") = "" Then
                        Select Case dtype
                            Case "String"
                                dtInvTb(i)(j) = ""
                            Case "Int64", "Int32", "Double", "Decimal", "Boolean"
                                dtInvTb(i)(j) = 0
                        End Select
                    End If
nxt:
                Next
nxtM:
            Next
        End With
        _objInv.savebulktoInvTr(dtInvTb)
        Return TDrAmt
    End Function
    Private Sub UpdateAccounts(ByVal TrId As Long, ByVal TDrAmt As Double, ByVal DiscAcc As Integer)
        Dim LinkNo As Long
        Dim dtTable As DataTable
        Dim stockAc As Long
        Dim costOfSalesAc As Long
        Dim dt As DataTable
        Dim costAmt As Double
        
        stockAc = getConstantAccounts(1)
        costOfSalesAc = getConstantAccounts(9)

        If isModi And TrId > 0 Then
            dtTable = _objcmnbLayer._fldDatatable("SELECT LinkNo FROM AccTrCmn WHERE LnkNo = " & TrId)
            If dtTable.Rows.Count > 0 Then
                LinkNo = dtTable(0)("LinkNo")
                _objcmnbLayer._saveDatawithOutParm("DELETE FROM AccTrDet WHERE LinkNo=" & LinkNo)
            End If
        End If

        setAcctrCmnValue(TrId, LinkNo)
        If dtAccTb Is Nothing Then
            dtAccTb = _objcmnbLayer._fldDatatable("Select top 1 LinkNo,AccountNo,DueDate,Reference,EntryRef,DealAmt,FCAmt,CurrencyCode,CurrRate, " & _
                                                  "JobCode,PDCAcc,BankCode,LPONo,OthCost,TrInf,TermsId,CustAcc,AccWithRef,ChqDate,ChqNo,SuppInvDate," & _
                                                  "vatid,isvatEntry,UnqNo from AccTrDet")
        End If
        If dtAccTb.Rows.Count > 0 Then
            dtAccTb.Rows.Clear()
        End If
       
        dt = _objcmnbLayer._fldDatatable("select sum(CostAvg*TrQty) costAmt from ItmInvTrTb  where trid=" & Val(numsto.Tag))
        If dt.Rows.Count > 0 Then
            costAmt = dt(0)("costAmt")
        End If
        If costAmt <> 0 Then
            'debit entry [cost of sales]
            Dim entryref As String = "SERVICE STOCK OUT : JOB#" & numVchrNo.Text
            setAcctrDetValue(LinkNo, costOfSalesAc, Val(numsto.Text), entryref, costAmt, "", "", 3, 0, "", _
                           "", stockAc, costOfSalesAc & Val(numsto.Text), "", 1)
          
            'credit entry [stock in hand]
            costAmt = costAmt * -1
            setAcctrDetValue(LinkNo, stockAc, numsto.Text, entryref, costAmt, "", "", 3, 0, "", _
                           "", costOfSalesAc, stockAc & numsto.Text, "", 1)
           
        End If


        _objTr.SaveAccTrWithDt(dtAccTb)
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        makeClear()
        AddNew()
    End Sub

    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        ldRec(Val(numVchrNo.Tag))
    End Sub
End Class