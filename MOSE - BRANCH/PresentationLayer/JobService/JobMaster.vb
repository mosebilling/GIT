Public Class JobMaster
#Region "Public variables"
    Public isModi As Boolean
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
    Private WithEvents fProductEnquiry As ItmEnqry
#End Region
#Region "Private Variable"
    Private chgbyprg As Boolean
    Private _srchTxtId As Integer
    Private _srchOnce As Boolean
    Private _srchIndexId As Byte

    Private MyActiveControl As New Object
    Private lstKey As Integer
    Private activecontrolname As String
    Private isset As Boolean
    Private PSAcc As Long
    Private ischgItm As Boolean
    Private ischgGrid As Boolean
    Private dtTax As DataTable
    Private chgAmt As Boolean
    Private chgUprice As Boolean
    Private chgbyprgN As Boolean
    Private _vtable As DataTable
    Private _dtRptTable As DataTable

    Private strGridSrchString As String
    Private SrchText As String
#End Region
#Region "Const Variables"
    Private Const ConstSlNo = 0
    Private Const ConstItemCode = 1
    Public Const ConstHsnCode = 2 'HSN Code
    Private Const ConstDescr = 3
    Private Const ConstJobDescr = 4
    Private Const ConstUnit = 5
    Private Const ConstQty = 6
    Private Const ConstUPrice = 7
    Private Const constItmTot = 8
    Private Const ConstTaxP = 9
    Private Const ConstTaxAmt = 10
    Private Const ConstLTotal = 11
    Private Const ConstItemID = 12
    Private Const ConstId = 13
    Private Const ConstCGSTP = 14
    Private Const ConstCGSTAmt = 15
    Private Const ConstSGSTP = 16
    Private Const ConstSGSTAmt = 17
    Private Const ConstIGSTP = 18
    Private Const ConstIGSTAmt = 19
    Private Const ConstAttend = 20
    Private Const ConstFinished = 21
    Private Const ConstPFraction = 22
    Private Const ConstActualPrice = 23
#End Region
    Private Sub SetGridHead()
        chgbyprg = True
        With grdVoucher

            SetEntryGridProperty(grdVoucher)
            .ColumnCount = 24
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

            .Columns(ConstItemCode).HeaderText = "ItemCode"
            .Columns(ConstItemCode).Width = 100
            .Columns(ConstItemCode).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(ConstItemCode).ReadOnly = False

            .Columns(ConstHsnCode).HeaderText = "HSN Code"
            .Columns(ConstHsnCode).Width = 100
            .Columns(ConstHsnCode).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstHsnCode).ReadOnly = True
            .Columns(ConstHsnCode).Visible = EnableGST

            .Columns(ConstDescr).HeaderText = "Description"
            .Columns(ConstDescr).Width = 220
            .Columns(ConstDescr).SortMode = DataGridViewColumnSortMode.NotSortable

            .Columns(ConstJobDescr).HeaderText = "Job Description"
            .Columns(ConstJobDescr).Width = 220
            .Columns(ConstJobDescr).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(ConstDescr).ReadOnly = False

            .Columns(ConstUnit).HeaderText = "Unit"
            .Columns(ConstUnit).Width = 40
            .Columns(ConstUnit).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstUnit).Visible = False
            .Columns(ConstUnit).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns(ConstQty).HeaderText = "Qty"
            .Columns(ConstQty).Width = 50
            .Columns(ConstQty).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(ConstQty).Resizable = DataGridViewTriState.False
            .Columns(ConstQty).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstQty).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '.Columns(ConstQty).ReadOnly = False

            .Columns(ConstUPrice).HeaderText = "Unit Price"
            .Columns(ConstUPrice).Width = 70
            .Columns(ConstUPrice).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstUPrice).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstUPrice).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '.Columns(ConstUPrice).ReadOnly = False

            .Columns(constItmTot).HeaderText = "Item Total"
            .Columns(constItmTot).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(constItmTot).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(constItmTot).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(constItmTot).ReadOnly = True
            .Columns(constItmTot).Visible = True


            .Columns(ConstTaxP).HeaderText = "Tax%"
            .Columns(ConstTaxP).Width = 50
            .Columns(ConstTaxP).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstTaxP).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstTaxP).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstTaxP).ReadOnly = True

            .Columns(ConstTaxAmt).HeaderText = "Tax Amt"
            .Columns(ConstTaxAmt).Width = 70
            .Columns(ConstTaxAmt).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstTaxAmt).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstTaxAmt).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstTaxAmt).ReadOnly = True
            
            .Columns(ConstLTotal).HeaderText = "Line Total"
            .Columns(ConstLTotal).Width = 80
            .Columns(ConstLTotal).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(ConstLTotal).Resizable = DataGridViewTriState.False
            .Columns(ConstLTotal).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstLTotal).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstLTotal).ReadOnly = True

            .Columns(ConstItemID).HeaderText = "ItemID"
            .Columns(ConstItemID).Visible = False
            .Columns(ConstItemID).ReadOnly = True


            .Columns(ConstId).HeaderText = "id"
            .Columns(ConstId).Visible = False
            .Columns(ConstId).ReadOnly = True

            .Columns(ConstCGSTP).HeaderText = "CGST %"
            .Columns(ConstCGSTP).Width = 50
            .Columns(ConstCGSTP).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstCGSTP).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstCGSTP).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstCGSTP).ReadOnly = True
            .Columns(ConstCGSTP).Visible = False

            .Columns(ConstCGSTAmt).HeaderText = "CGST Amt"
            .Columns(ConstCGSTAmt).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstCGSTAmt).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstCGSTAmt).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstCGSTAmt).ReadOnly = True
            .Columns(ConstCGSTAmt).Visible = False

            .Columns(ConstSGSTP).HeaderText = "SGST %"
            .Columns(ConstSGSTP).Width = 50
            .Columns(ConstSGSTP).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstSGSTP).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstSGSTP).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstSGSTP).ReadOnly = True
            .Columns(ConstSGSTP).Visible = False

            .Columns(ConstSGSTAmt).HeaderText = "SGST Amt"
            .Columns(ConstSGSTAmt).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstSGSTAmt).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstSGSTAmt).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstSGSTAmt).ReadOnly = True
            .Columns(ConstSGSTAmt).Visible = False

            .Columns(ConstIGSTP).HeaderText = "IGST %"
            .Columns(ConstIGSTP).Width = 50
            .Columns(ConstIGSTP).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstIGSTP).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstIGSTP).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstIGSTP).ReadOnly = True
            .Columns(ConstIGSTP).Visible = False

            .Columns(ConstIGSTAmt).HeaderText = "IGST Amt"
            .Columns(ConstIGSTAmt).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstIGSTAmt).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstIGSTAmt).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstIGSTAmt).ReadOnly = True
            .Columns(ConstIGSTAmt).Visible = False

            .Columns(ConstAttend).HeaderText = "Attended By"
            .Columns(ConstAttend).Width = 120
            .Columns(ConstAttend).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstAttend).ReadOnly = True

            .Columns(ConstFinished).HeaderText = "Finished"
            .Columns(ConstFinished).Width = 100
            .Columns(ConstFinished).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstFinished).ReadOnly = True

            .Columns(ConstPFraction).Visible = False
            .Columns(ConstActualPrice).Visible = False

        End With
        chgbyprg = False
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
            Else
                btnupdate.Tag = 1
                btndelete.Tag = 1
            End If
        End If
        ldJobdetails()
        CreateTaxTable(dtTax)
        isset = False
        SetGridHead()
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
            Dim x As Integer = Me.Width - fMList.Width
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
        fMainForm.QuickCust(, "Customer")
    End Sub

    Private Sub txtcustomer_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcustomer.Validated
        If txtcustomer.Text = "" Then Exit Sub
        Dim dt As DataTable
        _objcmnbLayer = New clsCommon_BL
        dt = _objcmnbLayer._fldDatatable("SELECT accid,Alias,AccDescr,Address1,Address2,Address3,Address4,Phone,Fax,EMail,PT1,PT2,PT3,PT4,TrdLcno,TrdDate,ContactName,CountryCode from AccMast LEFT JOIN AccMastAddr ON AccMast.ACCID=AccMastAddr.accountno where AccDescr='" & txtcustomer.Text & "'")
        If dt.Rows.Count > 0 Then
            txtcustomer.Tag = dt(0)("accid")
            txtaddress.Text = dt(0)("Address1") & vbCrLf & dt(0)("Address2") & vbCrLf & dt(0)("Address3") & vbCrLf & "Phone : " & dt(0)("Phone") & vbCrLf & dt(0)("ContactName")
            lblstatecode.Text = ("State Code : " & dt(0)("CountryCode"))
            If ("" & dt(0)("CountryCode")) = "" Then
                lblstatecode.Tag = ""
            Else
                If Trim(dt(0)("CountryCode") & "") <> Trim(stateCode & "") Then
                    lblstatecode.Tag = 1
                Else
                    lblstatecode.Tag = ""
                End If

            End If
        Else
            txtcustomer.Text = ""
        End If
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
        If txtDescription.Text = "" Then
            MsgBox("Invalid Description", MsgBoxStyle.Exclamation)
            txtDescription.Focus()
            Exit Sub
        End If
        If Val(txtcustomer.Tag) = 0 Then
            MsgBox("Invalid Customer", MsgBoxStyle.Exclamation)
            txtcustomer.Focus()
            Exit Sub
        End If
        If MsgBox("Verification is succeeded. Do you like to file it?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        saveJob()
        txtprintjob.Text = numVchrNo.Text
        MsgBox("Service Invoice saved successfully", MsgBoxStyle.Information)
        AddNew()
        makeClear()
        'If isModi Then Me.Close()
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
            .Technician = txttechnician.Text
            .SIID = 0
            .RvId = 0
            If Val(txtserviceCharge.Text) = 0 Then txtserviceCharge.Text = 0
            .ServiceCost = CDbl(txtserviceCharge.Text)
            If Val(lblitemcost.Text) = 0 Then lblitemcost.Text = 0
            .ItemCost = CDbl(lblitemcost.Text)
            .Userid = CurrentUser
            If Val(txtscost.Text) = 0 Then txtscost.Text = 0
            .LabourCost = CDbl(txtscost.Text)
            numVchrNo.Tag = .saveJob()
        End With
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

    Private Sub saveJobItems()
        _objJob = New clsJob
        Dim itemidsdatatable As DataTable
        Dim dtTable As DataTable
        Dim dateChanged As Boolean
        Dim i As Integer
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
        With grdVoucher
            For i = 0 To grdVoucher.RowCount - 1
                If Val(.Item(ConstItemID, i).Value) = 0 Then GoTo nxt
                _objJob.Itemid = Val(.Item(ConstItemID, i).Value)
                _objJob.Jobid = Val(numVchrNo.Tag)
                If Val(.Item(ConstQty, i).Value) = 0 Then .Item(ConstQty, i).Value = 0
                _objJob.Qty = CDbl(.Item(ConstQty, i).Value)
                If Val(.Item(ConstUPrice, i).Value) = 0 Then .Item(ConstUPrice, i).Value = 0
                _objJob.Uprice = CDbl(.Item(ConstUPrice, i).Value)
                _objJob.jbitmId = Val(.Item(ConstId, i).Value)
                _objJob.trDtno = getDateNo(CDate(dtpdate.Value))
                _objJob.Trid = Val(numsto.Tag)
                _objJob.TaxP = CDbl(.Item(ConstTaxP, i).Value)
                _objJob.TaxAmt = CDbl(.Item(ConstTaxAmt, i).Value)
                _objJob.cgstPer = CDbl(.Item(ConstCGSTP, i).Value)
                _objJob.sgstPer = CDbl(.Item(ConstSGSTP, i).Value)
                _objJob.jbDescription = .Item(ConstJobDescr, i).Value
                _objJob.itmDescription = .Item(ConstDescr, i).Value
                _objJob.hsnCode = .Item(ConstHsnCode, i).Value
                _objJob.pFraction = Val(.Item(ConstPFraction, i).Value)
                _objJob.SlNo = i + 1
                _objJob.saveJobItemTb()
                If dateChanged And enableRealtimeCosting Then
                    _objInv.ItemId = Val(.Item(ConstItemID, i).Value)
                    _objInv.TrDate = DateValue(dtpdate.Value)
                    _objInv.setcostAverageOnModification(UsrBr)
                End If
nxt:
            Next
        End With
        If isModi Then
            itemidsdatatable = _objcmnbLayer._fldDatatable("select itemid from ItmInvTrTb where setRemove=1 and trid=" & Val(numsto.Tag))
            _objcmnbLayer._saveDatawithOutParm("delete from ItmInvTrTb where setRemove=1 and trid=" & Val(numsto.Tag))
            If itemidsdatatable.Rows.Count > 0 And enableRealtimeCosting Then
                For i = 0 To itemidsdatatable.Rows.Count - 1
                    _objInv.ItemId = itemidsdatatable(i)("Itemid")
                    _objInv.TrDate = DateValue(dtpdate.Value)
                    _objInv.setcostAverageOnModification(UsrBr)
                Next
            End If
            _objcmnbLayer._saveDatawithOutParm("delete from JobitemTb where setRemove=1 and jbid=" & Val(numVchrNo.Tag))
        End If
        updateStockTransaction()
        ischgItm = False
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
        _objTr.JVDate = DateValue(dtpdate.Value)
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
    Private Sub setAcctrDetValue(ByVal lnkNo As Long, ByVal AccountNo As Long, ByVal Reference As String, ByVal EntryRef As String, ByVal DealAmt As Double, ByVal JobCode As String, ByVal JobStr As String, _
                                  ByVal TrInf As Short, ByVal OthCost As Short, ByVal LPO As String, ByVal TermsId As String, ByVal CustAcc As Integer, ByVal AccWithRef As String, _
                                  ByVal CurrencyCode As String, ByVal CurrRate As Double)
        With _objTr
            .trLinkNo = lnkNo
            .AccountNo = AccountNo
            .Reference = numsto.Text
            .EntryRef = EntryRef
            .DealAmt = DealAmt
            .JobCode = JobCode
            .JobStr = JobStr
            .CurrRate = CurrRate
            .CurrencyCode = CurrencyCode ' IIf(chkFC.Checked = True, txtFC.Text, "")
            .TrInf = TrInf
            .OthCost = OthCost
            .TermsId = TermsId
            .CustAcc = CustAcc
            .AccWithRef = AccWithRef
            .LPONo = LPO
            Dim dtLPO As Date = IIf(chkDate(dtpdate.Value), dtpdate.Value, DateValue(dtpdate.Value))
            .DocDate = dtLPO
            .SuppInvDate = dtLPO
            .DueDate = dtLPO
        End With
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
        _objcmnbLayer._saveDatawithOutParm("delete from ItmInvTrTb where trid=" & Val(numsto.Tag))
        _objcmnbLayer._saveDatawithOutParm("UPDATE InvNos SET Prefix = '',InvNo=" & Val(numsto.Text) + 1 & " WHERE InvType='STO'")
    End Sub
    Private Sub setInvCmnValue()
        Dim Dt As Date
        With _objInv
            Dt = DateValue(Date.Now)
            .TrId = Val(numsto.Tag & "")
            .TrDate = DateValue(dtpdate.Value)
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
            .NetAmt = CDbl(lblitemcost.Text)
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
            numsto.Tag = Val(_objInv._saveCmn())
        End With
    End Sub
    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
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
        txttechnician.Text = ""
        txtserviceCharge.Text = Format(0, numFormat)
        lblitemcost.Text = Format(0, numFormat)
        lblJobvalue.Text = Format(0, numFormat)
        lblitmcost.Text = Format(0, numFormat)
        lbljobcost.Text = Format(0, numFormat)
        txttotalItemAmt.Text = Format(0, numFormat)
        grdVoucher.Rows.Clear()
        numsto.Text = ""
        numsto.Tag = ""
        numVchrNo.Tag = ""
        lblstatecode.Tag = ""
        lblstatecode.Text = ""
        chgbyprg = False
        ischgItm = False
        btnundo.Visible = False
        btndelete.Text = "Clear"
        isModi = False
        fillGrid("")
    End Sub

    Private Sub calculate()
        If chgbyprg Then Exit Sub
        Dim totQty As Double
        Dim totTax As Double
        Dim totAmt As Double
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
                If EnableGST Then
                    If Val(.Item(ConstSGSTAmt, i).Value & "") = 0 Then .Item(ConstSGSTAmt, i).Value = 0
                    If Val(.Item(ConstCGSTAmt, i).Value & "") = 0 Then .Item(ConstCGSTAmt, i).Value = 0
                    .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstCGSTAmt, i).Value) + CDbl(.Item(ConstSGSTAmt, i).Value), numFormat)
                Else
                    .Item(ConstTaxAmt, i).Value = Format(((CDbl(.Item(ConstActualPrice, i).Value) * .Item(ConstTaxP, i).Value) / 100) * CDbl(.Item(ConstQty, i).Value), numFormat)
                End If
                totTax = totTax + .Item(ConstTaxAmt, i).Value
                totAmt = totAmt + (.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) + CDbl(.Item(ConstTaxAmt, i).Value)
                .Item(constItmTot, i).Value = Format((.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)), numFormat)
                .Item(ConstLTotal, i).Value = Format((.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) + CDbl(.Item(ConstTaxAmt, i).Value), numFormat)
                If ShowTaxOnInventory Then
                    For j = 0 To dtTax.Rows.Count - 1
                        If Val(.Item(ConstTaxP, i).Value) = dtTax(j)("vat") Then
                            dtTax(j)("Amount") = CDbl(dtTax(j)("Amount")) + CDbl(grdVoucher.Item(ConstTaxAmt, i).Value)
                        End If
                    Next
                ElseIf EnableGST Then
                    CalculateGST()
                End If
nxt:
            Next
        End With
        lblitemcost.Text = Format(totAmt, numFormat)
        'txttotalItemAmt.Text = Format(totAmt, numFormat)
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
            If grdinvList.RowCount > 0 Or grdrvlist.RowCount > 0 Then
                MsgBox("Transactions found! you cannot remove this job", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            If MsgBox("Do you want to remove the current Job?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            If Val(numVchrNo.Tag) = 0 Then Exit Sub
            _objcmnbLayer = New clsCommon_BL
            With _objcmnbLayer
                ._saveDatawithOutParm("DELETE FROM JobTb WHERE Jobid=" & Val(numVchrNo.Tag))
                ._saveDatawithOutParm("DELETE FROM JobItemtb WHERE Jbid=" & Val(numVchrNo.Tag))
            End With
            deleteInventory()
            'Me.Close()

        Else
            If MsgBox("Do you want to clear the current Job?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

        End If
        btnundo_Click(btnundo, New System.EventArgs)
    End Sub
    Public Sub ldRec(ByVal jbid As Long)
        Dim ds As DataSet
        _objJob = New clsJob
        With _objJob
            .Jobid = jbid
            .DateFrom = DateValue(Date.Now)
            .DateTo = DateValue(Date.Now)
            .Tp = 21
            ds = .returnJob
        End With
        If ds.Tables(0).Rows.Count > 0 Then
            chgbyprg = True
            numVchrNo.Text = ds.Tables(0)(0)("jobcode")
            txtprintjob.Text = ds.Tables(0)(0)("jobcode")
            numVchrNo.Tag = ds.Tables(0)(0)("Jobid")
            dtpdate.Value = ds.Tables(0)(0)("jobdate")
            txtJobname.Text = Trim(ds.Tables(0)(0)("jobname") & "")
            txtDescription.Text = ds.Tables(0)(0)("JobDescription")

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
            txtaddress.Text = ds.Tables(0)(0)("Address1") & vbCrLf & ds.Tables(0)(0)("Address2") & vbCrLf & ds.Tables(0)(0)("Address3") & vbCrLf & "Phone : " & ds.Tables(0)(0)("Phone") & vbCrLf & ds.Tables(0)(0)("ContactName")


            'txtobservedBy.Text = Trim(ds.Tables(0)(0)("OBTech") & "")
            txttechnician.Text = Trim(ds.Tables(0)(0)("SManCode") & "")

            If Not IsDBNull(ds.Tables(0)(0)("ServiceCost")) Then
                txtserviceCharge.Text = Format(CDbl(ds.Tables(0)(0)("ServiceCost")), numFormat)
            Else
                txtserviceCharge.Text = Format(0, numFormat)
            End If
            If Not IsDBNull(ds.Tables(0)(0)("LabourCost")) Then
                txtscost.Text = Format(CDbl(ds.Tables(0)(0)("LabourCost")), numFormat)
            Else
                txtscost.Text = Format(0, numFormat)
            End If
            If Not IsDBNull(ds.Tables(0)(0)("ItemCost")) Then
                lblitemcost.Text = Format(CDbl(ds.Tables(0)(0)("ItemCost")), numFormat)
            Else
                lblitemcost.Text = Format(0, numFormat)
            End If



            numsto.Text = Val(ds.Tables(0)(0)("invno") & "")
            numsto.Tag = Val(ds.Tables(0)(0)("trid") & "")
            chgbyprg = False
        End If
        Dim i As Integer
        Dim j As Integer
        SetGridHead()
        grdVoucher.Rows.Clear()
        With grdVoucher
            For j = 0 To ds.Tables(1).Rows.Count - 1
                .Rows.Add(1)
                i = .RowCount - 1
                .Item(ConstSlNo, i).Value = i + 1
                .Item(ConstItemCode, i).Value = ds.Tables(1)(j)("item code")
                '.Item(ConstDescr, i).Value = ds.Tables(1)(j)("Description")
                .Item(ConstUnit, i).Value = ds.Tables(1)(j)("Unit")
                .Item(ConstQty, i).Value = Format(Val(ds.Tables(1)(j)("Qty")), numFormat)
                .Item(ConstActualPrice, i).Value = CDbl(ds.Tables(1)(j)("Uprice"))
                .Item(ConstUPrice, i).Value = Format(CDbl(ds.Tables(1)(j)("Uprice")), numFormat)
                .Item(ConstTaxP, i).Value = Format(CDbl(ds.Tables(1)(j)("Taxp")), numFormat)
                .Item(ConstTaxAmt, i).Value = Format(CDbl(ds.Tables(1)(j)("TaxAmt")), numFormat)
                '.Item(ConstLTotal, i).Value = Format((CDbl(ds.Tables(1)(j)("Qty")) * CDbl(ds.Tables(1)(j)("Uprice"))) + CDbl(ds.Tables(1)(j)("TaxAmt")), numFormat)
                .Item(ConstItemID, i).Value = Val(ds.Tables(1)(j)("ItemId"))
                .Item(ConstId, i).Value = Val(ds.Tables(1)(j)("jbitmId"))
                .Item(ConstCGSTP, i).Value = CDbl(ds.Tables(1)(j)("cgstPer"))
                .Item(ConstSGSTP, i).Value = CDbl(ds.Tables(1)(j)("sgstPer"))
                .Item(ConstIGSTP, i).Value = CDbl(ds.Tables(1)(j)("sgstPer")) + CDbl(ds.Tables(1)(j)("cgstPer"))
                .Item(ConstJobDescr, i).Value = Trim(ds.Tables(1)(j)("jbDescription") & "")
                .Item(ConstDescr, i).Value = Trim(ds.Tables(1)(j)("itmDescription") & "")
                .Item(ConstHsnCode, i).Value = Trim(ds.Tables(1)(j)("hsncode") & "")
                .Item(ConstPFraction, i).Value = Val(ds.Tables(1)(j)("pFraction") & "")
                .Item(ConstAttend, i).Value = Trim(ds.Tables(1)(j)("attendedBy") & "")
                .Item(ConstFinished, i).Value = Trim(ds.Tables(1)(j)("isclosed") & "")
                getGSTDetails("", i, True)
            Next
        End With
        If userType Then
            btnupdate.Tag = IIf(getRight(30, CurrentUser), 1, 0)
            btndelete.Tag = IIf(getRight(31, CurrentUser), 1, 0)
        Else
            btnupdate.Tag = 1
            btndelete.Tag = 1
        End If
        ldItemCost()
        fillGrid(numVchrNo.Text)
        reArrangeNo()
        calculate()
        TabControl2.SelectedIndex = 0
        btnundo.Visible = True
        btndelete.Text = "Delete"
        isModi = True
    End Sub

    Private Sub btnitmAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnitmAdd.Click
        AddRow()
    End Sub
    Public Sub AddRow(Optional ByVal tocheck As Boolean = False)
        chgbyprg = True
        Dim i As Integer
        With grdVoucher
            activecontrolname = "grdVoucher"
            i = .RowCount '- 1
            .Rows.Add(1)
            .Item(ConstSlNo, i).Value = i + 1
            .Item(ConstItemCode, i).Value = ""
            .Item(ConstDescr, i).Value = ""
            .Item(ConstJobDescr, i).Value = ""
            .Item(ConstUnit, i).Value = ""
            .Item(ConstQty, i).Value = Format(0, numFormat)
            .Item(ConstUPrice, i).Value = Format(0, numFormat)
            .Item(ConstTaxP, i).Value = Format(0, numFormat)
            .Item(ConstTaxAmt, i).Value = Format(0, numFormat)
            .Item(ConstLTotal, i).Value = Format(0, numFormat)
            .Item(ConstItemID, i).Value = 0
            .Item(ConstPFraction, i).Value = "2"
            .CurrentCell = .Item(ConstItemCode, i)
            chgbyprg = True
            .BeginEdit(True)
            chgbyprg = False
        End With
        calculate()
        reArrangeNo()
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


    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        If chkFormat.Checked Then
            If TabControl2.SelectedIndex = 0 Then
                fRptFormat = New RptFormatfrm
                fRptFormat.RptType = "JBM"
                fRptFormat.ShowDialog()
            Else
                fRptFormat = New RptFormatfrm
                fRptFormat.RptType = IIf(rdoactive.Checked, "JBS1", "JBS3")
                fRptFormat.ShowDialog()
            End If

        Else
            If TabControl2.SelectedIndex = 0 Then
                PrepareRpt("JBM")
            Else
                fRptFormat = New RptFormatfrm
                fRptFormat.RptType = IIf(rdoactive.Checked, "JBS1", "JBS3")
                fRptFormat.ShowDialog()
            End If

        End If
    End Sub
    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        If TabControl2.SelectedIndex = 0 Then
            LoadReport(RptFlName, RptCaption)
        Else
            PrepareReportList(RptFlName, "", forPrint)
        End If
    End Sub
    Private Sub fRptFormat_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fRptFormat.FormClosed
        fRptFormat = Nothing
    End Sub
    Public Sub PrepareReportList(ByVal FileName As String, ByVal RptCaption As String, ByVal forpint As Boolean)
        If Not frm Is Nothing Then
            frm.Close()
            frm = New ReprtviewNEWfrm
        Else
            frm = New ReprtviewNEWfrm
        End If
        Dim ds As New DataSet
        If _dtRptTable Is Nothing Then
            With _objJob
                .Jobid = 0
                .DateFrom = DateValue(cldrStartDate.Value)
                .DateTo = DateValue(cldrEnddate.Value)
                .custcode = 0 'Val(txtsearch.Tag)
                '.IMEI = IIf(rptCategory = 8, cmbtech.Text, txtsearch.Text)
                'rptCategory= 1-estimated datewise, 2-Job datewise list,3-Job datewise Closed,4-Job datewise Opened,5-Closed datewise Closed
                .Tp = IIf(rdoactive.Checked, 4, 3)
                ds = .returnJob
            End With
        Else
            ds.Tables.Add(_dtRptTable)
        End If
        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = fMainForm
        frm.Text = RptCaption
        frm.Show()
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

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            Const WM_KEYDOWN As Integer = &H100
            Const WM_KEYUP As Integer = &H101
            If msg.Msg = WM_KEYDOWN Or msg.Msg = WM_KEYUP Then
                If activecontrolname = "grdVoucher" Then
                    If msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up) Or msg.WParam.ToInt32() = CInt(Keys.Enter) Or msg.WParam.ToInt32() = CInt(Keys.F2) Or msg.WParam.ToInt32() = CInt(Keys.F3) Or msg.WParam.ToInt32() = CInt(Keys.F4) Then
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

    Private Sub txtserviceCost_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtserviceCharge.TextChanged
        calculate()
    End Sub

    Private Sub txttechnician_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txttechnician.Validated
        If chgbyprg Then Exit Sub
        If txttechnician.Text = "" Then Exit Sub
        Dim dt As DataTable
        _objcmnbLayer = New clsCommon_BL
        dt = _objcmnbLayer._fldDatatable("SELECT salesmanid from SalesmanTb  where SManCode='" & txttechnician.Text & "'")
        If dt.Rows.Count > 0 Then
            txttechnician.Tag = dt(0)("salesmanid")
        Else
            txttechnician.Text = ""
        End If
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
    End Sub

    Private Sub dtpdate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtpdate.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
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

    Public Sub fillGrid(ByVal jobcode As String)
        Dim num2 As Double
        Dim strSql As String = ("Select  convert(varchar(12), case when prefix=''  then '' else '-' end +  invNo ) as [Inv No] , TrDate [Tr.Date] ,Alias [Cust. Code],AccDescr [Customer Name],(InvAmt-Discount) [Amount],TrRefNo [Ref. No],TrDescription  [Tr. Description],jobcode,UserId [Created By],TrId from ( select  prefix, invNo ,TrDate ,InvAmt,Discount,AccDescr ,TrRefNo,TrDescription,Alias ,JobInvCmntb.UserId,JOBCODE,JobInvCmntb.TrId from JOBInvCmntb LEFT JOIN JobTb ON JobInvCmntb.jobid=JobTb.jobid  LEFT JOIN (SELECT Trid,SUM((TrQty*(UnitCost+UnitOthCost))+taxamt)InvAmt FROM JobInvTrTb GROUP BY Trid) Tr ON  JobInvCmntb.Trid=Tr.Trid left join Accmast on JobInvCmnTb.custid=Accmast.accid where JobInvCmntb.trtype='JIS' and JobInvCmntb.jobid=" & Val(numVchrNo.Tag) & ") as qq  order by TrDate ,InvNo")
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
        strSql = "SELECT JVType [Type],PreFix,JVNum [RV No],JVDate [RV Date],Reference,DealAmt*-1 Amount,Accdescr [Paid By],dbtr.ChqNo [Chq No],dbtr.ChqDate [Chq Date],dbtr.BankCode [Bank Code],AccTrCmn.Linkno From AccTrCmn " & _
                    "LEFT JOIN AccTrDet ON AccTrDet.Linkno=AccTrCmn.Linkno " & _
                    "LEFT JOIN (SELECT Linkno,Accdescr,ChqNo,ChqDate,BankCode FROM AccTrDet " & _
                    "LEFT JOIN AccMast On AccTrDet.accountno=AccMast.Accid where DealAmt>0) dbtr ON AccTrDet.Linkno=dbtr.Linkno " & _
                    "where JobCode='" & jobcode & "' and JobCode<>'' and JVType='RV'"
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
        End With
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
            fInvoice.Show()
            Dim amt As Double
            If rdoserviceinv.Checked Then
                If chktotal.Checked Then
                    amt = CDbl(lblJobvalue.Text)
                Else
                    amt = CDbl(txtserviceCharge.Text)
                End If
            Else
                If chkwS.Checked Then
                    amt = CDbl(txtserviceCharge.Text)
                Else
                    amt = 0
                End If
            End If
            fInvoice.returnFromJob(Val(numVchrNo.Tag), amt, rdoserviceinv.Checked, Val(numsto.Tag))
        End If
    End Sub

    Private Sub fInvoice_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fInvoice.FormClosed
        fInvoice = Nothing
    End Sub

    Private Sub grdinvList_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdinvList.DoubleClick
        If grdinvList.Rows.Count = 0 Then Exit Sub
        fMainForm.LoadJIS(Val(grdinvList.Item(grdinvList.ColumnCount - 1, grdinvList.CurrentRow.Index).Value))
    End Sub
    Private Sub grdBeginEdit()
        chgbyprg = True
        grdVoucher.BeginEdit(True)
        chgbyprg = False
    End Sub

    Private Sub grdVoucher_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellClick
        chgbyprg = True
        With grdVoucher
            If Val(grdVoucher.Item(0, grdVoucher.CurrentCell.RowIndex).Value) = 0 And e.ColumnIndex <> 3 Then
                grdVoucher.CurrentCell.ReadOnly = True
            ElseIf e.ColumnIndex <> ConstSlNo And e.ColumnIndex <> constItmTot And e.ColumnIndex <> ConstUnit And e.ColumnIndex <> ConstLTotal And e.ColumnIndex <> ConstHsnCode And e.ColumnIndex <> ConstTaxAmt And e.ColumnIndex <> ConstFinished Then
                If e.ColumnIndex = ConstTaxP And EnableGST Then
                    grdVoucher.CurrentCell.ReadOnly = True
                ElseIf e.ColumnIndex = ConstQty And grdVoucher.Item(ConstMeterCode, grdVoucher.CurrentCell.RowIndex).Value <> "" Then
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
                _qurey = From data In dtGST.AsEnumerable() Where data("HSNCode") = Trim(.Item(ConstHsnCode, i).Value & "") Select data
                If _qurey.Count > 0 Then
                    dtHSN = _qurey.CopyToDataTable
                    If Val(lblstatecode.Tag) = 0 Then
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
            actualPrice = (CDbl(.Item(ConstActualPrice, i).Value) * CDbl(.Item(ConstQty, i).Value))
            .Item(ConstCGSTAmt, i).Value = (actualPrice * .Item(ConstCGSTP, i).Value) / 100
            .Item(ConstSGSTAmt, i).Value = (actualPrice * .Item(ConstSGSTP, i).Value) / 100
            .Item(ConstIGSTAmt, i).Value = (actualPrice * .Item(ConstIGSTP, i).Value) / 100
            If Val(lblstatecode.Tag) = 1 Then
                .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstIGSTAmt, i).Value), numFormat)
                .Item(ConstTaxP, i).Value = .Item(ConstIGSTP, i).Value
            Else
                .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstCGSTAmt, i).Value) + CDbl(.Item(ConstSGSTAmt, i).Value), numFormat)
                .Item(ConstTaxP, i).Value = CDbl(.Item(ConstCGSTP, i).Value) + CDbl(.Item(ConstSGSTP, i).Value)
            End If
        End With
    End Sub

    Private Sub grdVoucher_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellEndEdit
        Dim col As Integer = e.ColumnIndex
        Dim ndec1 As Integer
        If col = ConstQty Or col = ConstUPrice Or col = ConstLTotal Or col = ConstTaxP Then
            If col = ConstQty Or col = ConstEndReading Then
                ndec1 = grdVoucher.Item(ConstPFraction, grdVoucher.CurrentRow.Index).Value
            Else
                ndec1 = 2
            End If
            If Val(grdVoucher.Item(col, e.RowIndex).Value) = 0 Then grdVoucher.Item(col, e.RowIndex).Value = 0
            grdVoucher.Item(col, e.RowIndex).Value = Format(Convert.ToDecimal(grdVoucher.Item(col, e.RowIndex).Value), "#,##0" & IIf(ndec1 = 0, "", "." & Strings.StrDup(ndec1, "0")))
        End If
    End Sub
    Private Sub Valid(ByVal RowIndex As Integer, ByVal ColIndex As Integer)
        With grdVoucher
            Select Case ColIndex
                Case ConstHsnCode, ConstItemCode
                    If Not ischgItm Then Exit Sub
                    If Trim(.Item(ColIndex, RowIndex).Value) = "" Then Exit Sub
                    Dim dtItms As DataTable
                    Dim found As Boolean
                    dtItms = getItmDtls(3, SrchText, True)
                    If dtItms.Rows.Count > 0 Then
                        found = True
                        AddDetails(dtItms)
                    End If
                    ischgItm = False
                    If Not found Then
                        .Item(ConstHsnCode, RowIndex).Value = ""
                        .Item(ConstItemCode, RowIndex).Value = ""
                        '.Item(ConstBaseID, RowIndex).Value = ""
                        .Item(ConstItemID, RowIndex).Value = ""
                        .Item(ConstUnit, RowIndex).Value = ""
                        .Item(ConstSerialNo, RowIndex).Value = ""
                        '.Item(ConstPMult, RowIndex).Value = "1"
                        .Item(ConstPFraction, RowIndex).Value = "2"
                        '.Item(ConstImpDocId, RowIndex).Value = ""
                        '.Item(ConstImpLnId, RowIndex).Value = ""
                        ischgItm = False
                    End If
                Case ConstQty
                    If chgAmt Then
                        If EnableGST Then getGSTDetails(Trim(.Item(ConstHsnCode, RowIndex).Value & ""), RowIndex, True)
                        calculate()
                    End If
                Case ConstUPrice
                    If chgAmt Then
                        .Item(ConstActualPrice, RowIndex).Value = CDbl(.Item(ConstUPrice, RowIndex).Value)
                        If EnableGST Then getGSTDetails(Trim(.Item(ConstHsnCode, RowIndex).Value & ""), RowIndex, True)
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
                .Item(ConstHsnCode, i).Value = DR(0)("HSNCode") 'hsncode
            Else
                .Item(ConstHsnCode, i).Value = ""
            End If
            .Item(ConstQty, i).Value = Format(1, numFormat) 'IIf(IsReturn, -1, 1)
            .Item(ConstItemID, i).Value = DR(0)("ItemId")
            .Item(ConstPFraction, i).Value = IIf(IsDBNull(DR(0)("FraCount")), "2", DR(0)("FraCount"))
            .Item(ConstUnit, i).Value = DR(0)("Unit")
            If CDbl(.Item(ConstUPrice, i).Value) = 0 Or ischgItm Then
                .Item(ConstActualPrice, i).Value = DR(0)("UnitPrice")
                .Item(ConstUPrice, i).Value = Format(CDbl(.Item(ConstActualPrice, i).Value), numFormat)
            End If
           If EnableGST Then
                getGSTDetails(Trim(.Item(ConstHsnCode, i).Value & ""), i, False)
            End If
            .Item(ConstLTotal, i).Value = Format(.Item(ConstQty, i).Value * CDbl(.Item(ConstUPrice, i).Value), numFormat)
            .Item(ConstQty, i).Value = Format(CDbl(.Item(ConstQty, i).Value), "#,##0" & IIf(Val(.Item(ConstPFraction, i).Value) = 0, "", "." & Strings.StrDup(CByte(.Item(ConstPFraction, i).Value), "0")))
            chgAmt = True
            ischgItm = False
            .ClearSelection()
        End With
        chgbyprg = False
        calculate()

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
                    ischgItm = True
                Case ConstQty, ConstTaxP, ConstDisAmt, ConstDisP
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
        If Col = ConstItemCode Or Col = ConstQty Then
            Dim tb As TextBox = CType(e.Control, TextBox)
            RemoveHandler tb.KeyPress, AddressOf Textbox_KeyPress
            AddHandler tb.KeyPress, AddressOf Textbox_KeyPress
        End If
    End Sub
    Private Sub Textbox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            Dim col As Integer
            col = grdVoucher.CurrentCell.ColumnIndex
            If col = ConstQty Or col = ConstLTotal Then
                If Not IsNumeric(e.KeyChar) And e.KeyChar <> "." And Not e.KeyChar = Convert.ToChar(Keys.Back) Then e.Handled = True
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
                ischgItm = True
                chgbyprg = False
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
        SearchProductPanel(grdSrch, "[Item Code]", strGridSrchString, True)
        doSelect(2)
        _srchOnce = True
        chgbyprg = False
    End Sub

    Private Sub picCloseProd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picCloseProd.Click
        plsrch.Visible = False
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

    Private Sub grdVoucher_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.GotFocus
        If grdVoucher.RowCount = 0 Then
            AddRow()
        End If
        activecontrolname = "grdVoucher"
    End Sub

    Private Sub grdVoucher_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdVoucher.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If grdVoucher.RowCount = 0 Then Exit Sub
                If SrchText = "" And grdVoucher.CurrentCell.ColumnIndex = ConstItemCode Then
                    GoTo nxt
                End If
                If FindNextCell(grdVoucher, grdVoucher.CurrentCell.RowIndex, grdVoucher.CurrentCell.ColumnIndex + 1) Then
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
                        If Not fMList Is Nothing Then fMList.Visible = False
                        If plsrch.Visible Then plsrch.Visible = False
                        fProductEnquiry = New ItmEnqry
                        fProductEnquiry.ShowDialog()
                    
                    Case ConstTaxAmt, ConstTaxP
                        If EnableGST Then
                            tbgst.Visible = True
                            showItemGst(True, grdVoucher.CurrentRow.Index)
                        End If
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
    Private Sub showItemGst(ByVal isfocus As Boolean, ByVal i As Integer)
        With grdVoucher
            'Dim i As Integer = grdVoucher.CurrentRow.Index
            If Val(.Item(ConstActualPrice, i).Value) = 0 Then .Item(ConstActualPrice, i).Value = 0
            If Val(.Item(ConstQty, i).Value) = 0 Then .Item(ConstQty, i).Value = 0
            txtCgst.Tag = CDbl(.Item(ConstActualPrice, i).Value) * CDbl(.Item(ConstQty, i).Value)
            chgbyprgN = True
            If Val(lblstatecode.Tag) > 0 Then
                txtIgst.Enabled = True
                txtIgstAmt.Enabled = True
                txtIgst.Text = Format(CDbl(.Item(ConstIGSTP, i).Value), numFormat)
                txtIgstAmt.Text = Format(CDbl(.Item(ConstIGSTAmt, i).Value), numFormat)

                txtCgst.Text = Format(0, numFormat)
                txtCgstAmt.Text = Format(0, numFormat)
                txtSgst.Text = Format(0, numFormat)
                txtSgstAmt.Text = Format(0, numFormat)
                txtCgst.Enabled = False
                txtCgstAmt.Enabled = False
                txtSgst.Enabled = False
                txtSgstAmt.Enabled = False
            Else
                txtCgst.Enabled = True
                txtCgstAmt.Enabled = True
                txtSgst.Enabled = True
                txtSgstAmt.Enabled = True
                txtCgst.Text = Format(CDbl(.Item(ConstCGSTP, i).Value), numFormat)
                txtCgstAmt.Text = Format(CDbl(.Item(ConstCGSTAmt, i).Value), numFormat)
                txtSgst.Text = Format(CDbl(.Item(ConstSGSTP, i).Value), numFormat)
                txtSgstAmt.Text = Format(CDbl(.Item(ConstSGSTAmt, i).Value), numFormat)

                txtIgst.Text = Format(0, numFormat)


                txtIgstAmt.Text = Format(0, numFormat)
                txtIgst.Enabled = False
                txtIgstAmt.Enabled = False
            End If

        End With
        chgbyprgN = False
        If isfocus Then txtCgst.Focus()
    End Sub
    Private Sub setGstToGrdFromTabC()
        Dim i As Integer = grdVoucher.CurrentRow.Index
        With grdVoucher
            If Val(lblstatecode.Tag) > 0 Then
                .Item(ConstIGSTP, i).Value = txtIgst.Text
                .Item(ConstIGSTAmt, i).Value = txtIgstAmt.Text
            Else
                .Item(ConstCGSTP, i).Value = txtCgst.Text
                .Item(ConstCGSTAmt, i).Value = txtCgstAmt.Text
                .Item(ConstSGSTP, i).Value = txtSgst.Text
                .Item(ConstSGSTAmt, i).Value = txtSgstAmt.Text
            End If
            If Val(lblstatecode.Tag) = 1 Then
                .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstIGSTAmt, i).Value), numFormat)
                .Item(ConstTaxP, i).Value = .Item(ConstIGSTP, i).Value
            Else
                .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstCGSTAmt, i).Value) + CDbl(.Item(ConstSGSTAmt, i).Value), numFormat)
                .Item(ConstTaxP, i).Value = CDbl(.Item(ConstCGSTP, i).Value) + CDbl(.Item(ConstSGSTP, i).Value)
            End If
        End With
        txtIgst.Text = 0
        txtIgstAmt.Text = 0
        txtCgst.Text = 0
        txtCgstAmt.Text = 0
        txtSgst.Text = 0
        txtSgstAmt.Text = 0
        txtCgst.Tag = 0
        tbgst.Visible = False
        calculate()
    End Sub

    Private Sub fProductEnquiry_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fProductEnquiry.FormClosed
        fProductEnquiry = Nothing
    End Sub

    Private Sub grdVoucher_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.Leave
        activecontrolname = ""
    End Sub

    Private Sub grdVoucher_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.RowEnter
        If chgbyprg Then Exit Sub
        If tbgst.Visible Then
            showItemGst(False, e.RowIndex)
        End If
    End Sub
    Private Sub fProductEnquiry_getSelected(ByVal ItemcODE As String, ByVal isacc As Integer) Handles fProductEnquiry.getSelected
        chgbyprg = True
        fProductEnquiry.Close()
        SrchText = ItemcODE
        grdVoucher.Item(ConstItemCode, grdVoucher.CurrentRow.Index).Value = ItemcODE
        ischgItm = True
        Valid(grdVoucher.CurrentRow.Index, ConstItemCode)
        grdVoucher.CurrentCell = grdVoucher.Item(3, grdVoucher.CurrentRow.Index)
        grdBeginEdit()
        chgbyprg = False
    End Sub

    Private Sub btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnload.Click
        ldJobdetails()
    End Sub
    Private Sub ldJobdetails()

        _objJob = New clsJob
        With _objJob
            .Jobid = 0
            .DateFrom = DateValue(cldrStartDate.Value)
            .DateTo = DateValue(cldrEnddate.Value)
            .custcode = 0 'Val(txtsearch.Tag)
            '.IMEINo = IIf(rptCategory = 8, cmbtech.Text, txtsearch.Text)
            'rptCategory= 1-estimated datewise, 2-Job datewise list,3-Job datewise Closed,4-Job datewise Opened,5-Closed datewise Closed
            If rdoactive.Checked Then
                If chkestidate.Checked Then
                    .Tp = 1
                Else
                    .Tp = 4
                End If

            ElseIf rdoclosed.Checked Then
                .Tp = 3
            Else
                .Tp = 2
            End If
            _vtable = .returnJob.Tables(0)
        End With
        grdItem.DataSource = Nothing
        grdItem.DataSource = _vtable
        SetGrid()

    End Sub
    Private Sub SetGrid()
        With grdItem
            If .ColumnCount = 0 Then Exit Sub
            SetGridProperty(grdItem)

            .Columns("Jobcode").HeaderText = "Job Code"
            .Columns("jobdate").HeaderText = "Job Date"
            .Columns("Jobname").HeaderText = "Job Name"
            .Columns("JobDescription").HeaderText = "Job Description"
            .Columns("AccDescr").HeaderText = "Customer Name"
            .Columns("ContactName").HeaderText = "Contact Name"
            .Columns("EstimatedDate").HeaderText = "Estimated Date"
            .Columns("EstimatedAmt").HeaderText = "Estimated Amount"
            .Columns("JobValue").HeaderText = "Job Value"
            .Columns("Userid").HeaderText = "Created By"
            .Columns("CrdtDate").HeaderText = "Created Date"
            .Columns("EstimatedAmt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("EstimatedAmt").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("JobValue").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("JobValue").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Datefrom").Visible = False
            .Columns("Dateto").Visible = False
            .Columns("lnk").Visible = False
            .Columns("jobid").Visible = False
            .Columns("ServiceCost").Visible = False
            .Columns("ItemCost").Visible = False
            .Columns("JobCloseDate").Visible = False
            .Columns("JobDescription").Width = 200
            .Columns("AccDescr").Width = 200
            .Columns("ContactName").Width = 200
            .Columns("EstimatedDate").Width = 150
            .Columns("EstimatedAmt").Width = 150
            .Columns("CrdtDate").Width = 150
            .Columns("Jobname").Frozen = True
            setComboGrid()
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

    Private Sub rdoall_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoall.Click, rdoactive.Click, rdoclosed.Click
        Dim myctrl As RadioButton
        myctrl = sender
        If myctrl.Name = "rdoactive" Then
            chkestidate.Enabled = True
        Else
            chkestidate.Enabled = False
        End If
        ldJobdetails()
    End Sub

    Private Sub grdItem_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItem.CellContentClick

    End Sub

    Private Sub grdItem_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdItem.DoubleClick
        If grdItem.Rows.Count = 0 Then Exit Sub
        ldRec(Val(grdItem.Item("jobid", grdItem.CurrentRow.Index).Value))
    End Sub

    Private Sub btnundo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnundo.Click
        makeClear()
        AddNew()
    End Sub

    Private Sub TabControl2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl2.Click
        If TabControl2.SelectedIndex = 1 And btnundo.Visible = True Then
            MsgBox("Do Undo/Update before moving to Job List", MsgBoxStyle.Exclamation)
            TabControl2.SelectedIndex = 0
        End If
    End Sub

    Private Sub btncreateRV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncreateRV.Click
        If txtcustomer.Text = "" Then
            MsgBox("Invalid Customer", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        fMainForm.LoadRV(0, txtcustomer.Text)
    End Sub

    Private Sub btnrefreshVoucher_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefreshVoucher.Click
        fillGrid(numVchrNo.Text)
    End Sub

    Private Sub grdrvlist_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdrvlist.DoubleClick
        If grdrvlist.Rows.Count = 0 Then Exit Sub
        fMainForm.LoadRV(Val(grdrvlist.Item("Linkno", grdrvlist.CurrentRow.Index).Value), txtcustomer.Text)
    End Sub

    Private Sub txtSeq_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeq.TextChanged
        grdItem.DataSource = SearchGrid(_vtable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
        _dtRptTable = SearchGrid(_vtable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
    End Sub


    Private Sub rdoinvoice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoinvoice.Click, rdoserviceinv.Click
        Dim myctrl As RadioButton
        myctrl = sender
        If myctrl.Name = "rdoinvoice" Then
            chkwS.Enabled = True
            chktotal.Enabled = False
        Else
            chktotal.Enabled = True
            chkwS.Enabled = False
        End If
    End Sub

    Private Sub rdoinvoice_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoinvoice.CheckedChanged

    End Sub

    Private Sub grdinvList_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdinvList.CellContentClick

    End Sub

    Private Sub TabPage3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage3.Click

    End Sub

    Private Sub txtprintjob_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtprintjob.TextChanged

    End Sub


End Class