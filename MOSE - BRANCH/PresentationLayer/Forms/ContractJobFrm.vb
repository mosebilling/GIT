Public Class ContractJobFrm
#Region "Constant Varibales"
    '*******Item constants
    'Private Const ConstSlNo = 0
    'Private Const ConstItemCode = 1
    'Private Const ConstDescr = 2
    'Private Const ConstUnit = 3
    'Private Const ConstQty = 4
    'Private Const ConstUPrice = 5
    'Private Const ConstTaxP = 6
    'Private Const ConstTaxAmt = 7
    'Private Const ConstLTotal = 8
    'Private Const ConstItemID = 9
    'Private Const ConstBaseID = 10
    'Private Const ConstId = 11
    'Private Const ConstPper = 12
    '************workprogress
    Private Const ConstSdate = 1
    Private Const ConstEstdate = 2
    Private Const ConstEdate = 3
    Private Const ConstWorkDetail = 4
    Private Const ConstEstAmount = 5
    Private Const ConstActualAmt = 6
    Private Const Constdays = 7
    Private Const ConstAmtDiff = 8
    Private Const Constsw = 9
    Private Const ConstRemark = 10
    Private Const ConstConId = 11

    'RV GRID CONSTANT variables
    Private Const ConstInvNo = 0
    Private Const ConstReference = 1
    Private Const ConstTrdate = 2
    Private Const ConstInvAmount = 3
    Private Const ConstCustId = 4
    Private Const ConstCustname = 5
    Private Const ConstTrRef = 6
    Private Const Consttype = 7
    Private Const ConstLinkNo = 8

#End Region
#Region "Private Variables"
    Private chgbyprg As Boolean
    Private _srchTxtId As Integer
    Private _srchOnce As Boolean
    Private MyActiveControl As New Object
    Private lstKey As Integer
    Private activecontrolname As String
    Private SrchText As String
    Private ischgItm As Boolean
    Private PSAcc As Long
    Private dtchg As Boolean
    Private amtchg As Boolean
    Private invoiceAmount As Double
    Private qtyNumFormat As String
    Private _vtable As DataTable
    Private chgAmt As Boolean
    Private chgUprice As Boolean
    Private strGridSrchString As String
    Private _srchIndexId As Integer
    Private stockdebit As Integer
    Private stockcredit As Integer
    Private TrTypeNo As Integer
#End Region
#Region "Class Objects"
    Private _objJob As clsJob
    Private _objcmnbLayer As New clsCommon_BL
    Private _objInv As New clsInvoice
    Private _objTr As New clsAccountTransaction
    Private _objReport As New clsReport_BL
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
    Private WithEvents fInvoice As MFSalesInvoice
    Private WithEvents fSlctDoc As SelectInvTr
    Private WithEvents fAmount As InvoicePercentage
    Private WithEvents fHistory As New SelectHistory
    Private WithEvents fcustomer As CreateAccNew
    Private WithEvents fqti As CustomerQuotation
    Private WithEvents fProductEnquiry As ItmEnqry

#End Region
#Region "Public Variables"
    Public isModi As Boolean
    Public isModiItm As Boolean
    Dim strsql As String
    Public ldType As String
    Private forSingle As Boolean
    Private dtTable As DataTable
#End Region

    Private Sub ContractJobFrm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        dtpdate.Focus()
    End Sub

    Private Sub ContractJobFrm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If Not fSlctDoc Is Nothing Then fSlctDoc.Close() : fSlctDoc = Nothing
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
        _objInv = Nothing
        _objJob = Nothing
        _objTr = Nothing
    End Sub
    Private Sub ContractJobFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not isModi Then
            AddNew()
            btndelete.Text = "Clear"
            If userType Then
                btnupdate.Tag = IIf(getRight(113, CurrentUser), 1, 0)
                btnupdateConsum.Tag = IIf(getRight(119, CurrentUser), 1, 0)
            Else
                btnupdate.Tag = 1
            End If
            btninvoice.Enabled = False
        Else
            btndelete.Text = "Delete"
            If userType Then
                btnupdate.Tag = IIf(getRight(114, CurrentUser), 1, 0)
                btndelete.Tag = IIf(getRight(115, CurrentUser), 1, 0)
                btncloseJob.Tag = IIf(getRight(130, CurrentUser), 1, 0)
                btnupdateConsum.Tag = IIf(getRight(120, CurrentUser), 1, 0)
                btninvoice.Tag = IIf(getRight(116, CurrentUser), 1, 0)
            Else
                btnupdate.Tag = 1
                btndelete.Tag = 1
                btncloseJob.Tag = 1
                btnupdateConsum.Tag = 1
            End If
        End If
        SetGridHead()
        Timer1.Enabled = True
        cmbjbType.SelectedIndex = 0
        cmbrvtype.SelectedIndex = 0
        cmbtype.SelectedIndex = 0
        stockdebit = getConstantAccounts(18)
        stockcredit = getConstantAccounts(1)
        TrTypeNo = getVouchernumber("STO")
    End Sub
    Private Sub AddNew()
        txtjobcode.Text = GenerateNext(txtjobcode.Text)
    End Sub
    Private Function GenerateNext(ByVal strCode As String) As String
        Dim N As Double
        Dim NewCode As String
        Dim i As Byte
        Dim f As Byte
        Dim tmp As String
        Dim dt As DataTable
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

    Private Sub SetGridHeadInv(ByVal grd As DataGridView, ByVal tp As Integer)

        SetGridProperty(grd)
        With grd
            .Columns((.ColumnCount - 1)).Visible = False
            .Columns("Inv No").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Inv No").Width = &H4B
            If tp = 0 Then
                .Columns("AccDescr").Width = 200
                .Columns("AccDescr").HeaderText = "Customer"
                .Columns("RV Amount").HeaderText = "Received"
            Else
                .Columns("Supplier Name").Width = 200
                .Columns("RV Amount").HeaderText = "Paid"
            End If
            .Columns("Tr. Description").Visible = False
            .Columns("Amount").DefaultCellStyle.Format = ("N" & NoOfDecimal)
            .Columns("Amount").Frozen = True
            .Columns("Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("RV Amount").DefaultCellStyle.Format = ("N" & NoOfDecimal)
            .Columns("RV Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Balance").DefaultCellStyle.Format = ("N" & NoOfDecimal)
            .Columns("Balance").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("jobcode").Visible = False
        End With
        If tp = 0 Then
            resizeGridColumn(grdinvList, 3)
        Else
            resizeGridColumn(grdpurchase, 3)
        End If

    End Sub
    Private Sub SetGridHead()
        chgbyprg = True
        With grdVoucher

            SetGridHeadEntryProperty(grdVoucher)


            .Columns(ConstSlNo).HeaderText = "SlNo"
            .Columns(ConstSlNo).Width = 40
            .Columns(ConstSlNo).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstSlNo).DefaultCellStyle.Format = "N0"
            .Columns(ConstSlNo).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '.Columns(ConstSlNo).ReadOnly = True

            .Columns(ConstItemCode).HeaderText = "ItemCode"
            .Columns(ConstItemCode).Width = 100
            .Columns(ConstItemCode).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(ConstItemCode).ReadOnly = False

            .Columns(ConstBarcode).HeaderText = "HSN Code"
            .Columns(ConstBarcode).Width = 100
            .Columns(ConstBarcode).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstBarcode).ReadOnly = True
            .Columns(ConstBarcode).Visible = False

            .Columns(ConstDescr).HeaderText = "Description"
            .Columns(ConstDescr).Width = 220
            .Columns(ConstDescr).SortMode = DataGridViewColumnSortMode.NotSortable

            .Columns(ConstUnit).HeaderText = "Unit"
            .Columns(ConstUnit).Width = 40
            .Columns(ConstUnit).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstUnit).Visible = False
            .Columns(ConstUnit).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns(ConstQty).HeaderText = "Qty"
            .Columns(ConstQty).Width = 50
            .Columns(ConstQty).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstQty).DefaultCellStyle.BackColor = Color.LightGreen
            '.Columns(ConstQty).Resizable = DataGridViewTriState.False
            .Columns(ConstQty).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstQty).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '.Columns(ConstQty).ReadOnly = False

            .Columns(ConstUPrice).HeaderText = "Unit Price"
            .Columns(ConstUPrice).Width = 70
            .Columns(ConstUPrice).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstUPrice).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstUPrice).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '.Columns(ConstUPrice).Visible = IIf(hidecost = 1, True, False)

            .Columns(constItmTot).HeaderText = "Item Total"
            .Columns(constItmTot).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(constItmTot).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(constItmTot).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(constItmTot).ReadOnly = True
            '.Columns(constItmTot).Visible = IIf(hidecost = 1, True, False)


            .Columns(ConstTaxP).HeaderText = "Tax%"
            .Columns(ConstTaxP).Width = 50
            .Columns(ConstTaxP).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstTaxP).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstTaxP).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstTaxP).Visible = False

            .Columns(ConstTaxAmt).HeaderText = "Tax Amt"
            .Columns(ConstTaxAmt).Width = 70
            .Columns(ConstTaxAmt).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstTaxAmt).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstTaxAmt).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstTaxAmt).Visible = False

            .Columns(ConstLTotal).HeaderText = "Line Total"
            .Columns(ConstLTotal).Width = 80
            .Columns(ConstLTotal).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(ConstLTotal).Resizable = DataGridViewTriState.False
            .Columns(ConstLTotal).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstLTotal).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstLTotal).DefaultCellStyle.BackColor = Color.LightBlue
            .Columns(ConstLTotal).ReadOnly = True
            '.Columns(ConstLTotal).Visible = IIf(hidecost = 1, True, False)

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

            .Columns(ConstPFraction).Visible = False
            .Columns(ConstActualPrice).Visible = False
            .Columns(ConstMRP).Visible = False

        End With
        chgbyprg = False
    End Sub
    Public Sub loadInvoices()
        Dim num2 As Double
        _objJob = New clsJob
        grdinvList.DataSource = Nothing
        Dim source As DataTable = _objJob.returnVoucherList(Val(txtjobcode.Tag), 2)
        grdinvList.DataSource = source
        SetGridHeadInv(grdinvList, 0)
        chgbyprg = False
        Dim num3 As Integer = (source.Rows.Count - 1)
        Dim i As Integer = 0
        Do While (i <= num3)
            num2 = num2 + source(i)("Amount")
            i += 1
        Loop
        'lblinvamt.Text = Format(num2, numFormat)
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        resizeGridColumn(grdVoucher, ConstDescr)

    End Sub

    Private Sub ContractJobFrm_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Timer1.Enabled = True
    End Sub

    Private Sub TabControl2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl2.Click
        If TabControl2.SelectedIndex = 1 Then
            If grdItem.RowCount = 0 Then ldJobdetails()
            'If grdinvList.RowCount = 0 Then fillGrid()
            'If grdRVList.RowCount = 0 Then ldrvGrid()
            'If grdpayments.RowCount = 0 Then ldPVGrid()
            'If grdpurchase.RowCount = 0 Then ldPurchaseGrid()
        End If
    End Sub

    'Private Sub dtpdate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtpdate.KeyDown, txtJobname.KeyDown, _
    '                                                                                                        txtcustomer.KeyDown, cmbjbType.KeyDown, dtpstart.KeyDown, _
    '                                                                                                        dtpestimatedDt.KeyDown, numOpnAmt.KeyDown, numOPNIncome.KeyDown, numQtnAmt.KeyDown, _
    '                                                                                                        txtEstAmt.KeyDown, txtsitelocation.KeyDown, txtPjob.KeyDown, txtplotno.KeyDown, txtitemname.KeyDown, txtitm.KeyDown, numVchrNo.KeyDown

    'End Sub

    Private Sub txtEstAmt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles numQtnAmt.KeyPress, numOpnAmt.KeyPress, numOPNIncome.KeyPress, txtscost.KeyPress
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
            numCtrl.Text = Format(Val(numCtrl.Text), numFormat)
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

    Private Sub dtpstart_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtpstart.KeyDown, dtpestimatedDt.KeyDown, dtpdate.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub numVchrNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtjobcode.KeyDown, txtJobname.KeyDown, _
                                                                                                            txtcustomer.KeyDown, _
                                                                                                             numOpnAmt.KeyDown, numOPNIncome.KeyDown, numQtnAmt.KeyDown, _
                                                                                                             txtsitelocation.KeyDown
        lstKey = e.KeyCode
        Dim myctrl As TextBox
        myctrl = sender
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
        If myctrl.Name = "txtcustomer" Or myctrl.Name = "txtitm" Or myctrl.Name = "txtitemname" Or myctrl.Name = "txtPjob" Or myctrl.Name = "numVchrNo" Then
            If e.KeyCode = Keys.Up Or e.KeyCode = Keys.PageUp Then
                If Not fMList Is Nothing Then
                    If fMList.Visible Then
                        fMList.MoveUp(myctrl.Text)
                        Exit Sub
                    End If
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



    Private Sub txtcustomer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcustomer.TextChanged, txtjobcode.TextChanged
        If chgbyprg = True Then Exit Sub
        Dim myCtrl As Control = sender
        Select Case myCtrl.Name
            Case "txtcustomer"
                _srchTxtId = 1
            Case "txtitm"
                _srchTxtId = 2
            Case "txtitemname"
                _srchTxtId = 3
            Case "txtjobcode"
                If isModi Then
                    _srchTxtId = 5
                    txtjobcode.Tag = 0
                Else
                    Exit Sub
                End If
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
                        JobidForConstruct = Val(txtjobcode.Tag)
                        SetFmlist(fMList, 22)
                        JobidForConstruct = 0
                    Case 5
                        SetFmlist(fMList, 8)
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
                'Case 2   'Item Code
                '    fMList.SearchIndex = 0
                '    fMList.SearchIndexDescr = 1
                '    fMList_doFocus()
                '    fMList.Search(txtitm.Text)
                '    txtitemname.Text = fMList.AssignList(txtitm, lstKey, chgbyprg)
                'Case 3   'Item name
                '    fMList.SearchIndex = 1
                '    fMList.SearchIndexDescr = 0
                '    fMList_doFocus()
                '    fMList.Search(txtitemname.Text)
                '    txtitm.Text = fMList.AssignList(txtitemname, lstKey, chgbyprg)
            Case 5 'Job for modification

                fMList.SearchIndex = 0
                fMList.SearchIndexDescr = 1
                fMList_doFocus()
                fMList.Search(txtjobcode.Text)
                fMList.AssignList(txtjobcode, lstKey, chgbyprg)
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
            Case 5
                txtjobcode.Text = ItmFlds(0)
        End Select
        chgbyprg = False
    End Sub

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        'fMainForm.QuickCust(True, "Customer")
        fcustomer = New CreateAccNew
        With fcustomer
            .Condition = "GrpSetOn In ('Customer')"
            .iscust = True
            .bOnlyOne = True
            .ShowDialog()
        End With
        fcustomer = Nothing
    End Sub

    Private Sub txtcustomer_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcustomer.Validated
        setCustomer(0)
    End Sub
    Private Sub setCustomer(ByVal accid As Long)
        If txtcustomer.Text = "" And accid = 0 Then Exit Sub
        Dim dt As DataTable
        If accid = 0 Then
            dt = _objcmnbLayer._fldDatatable("SELECT accid,Alias,AccDescr,Address1,Address2,Address3,Address4,Phone,Fax,EMail,PT1,PT2,PT3,PT4,TrdLcno,TrdDate,ContactName,CountryCode from AccMast LEFT JOIN AccMastAddr ON AccMast.ACCID=AccMastAddr.accountno where AccDescr='" & txtcustomer.Text & "'")
        Else
            dt = _objcmnbLayer._fldDatatable("SELECT accid,Alias,AccDescr,Address1,Address2,Address3,Address4,Phone,Fax,EMail,PT1,PT2,PT3,PT4,TrdLcno,TrdDate,ContactName,CountryCode from AccMast LEFT JOIN AccMastAddr ON AccMast.ACCID=AccMastAddr.accountno where accid=" & accid)
        End If
        'dt = _objcmnbLayer._fldDatatable("SELECT accid,Alias,AccDescr,Address1,Address2,Address3,Address4,Phone,Fax,EMail,PT1,PT2,PT3,PT4,TrdLcno,TrdDate,ContactName from AccMast LEFT JOIN AccMastAddr ON AccMast.ACCID=AccMastAddr.accountno where AccDescr='" & txtcustomer.Text & "'")
        chgbyprg = True
        If dt.Rows.Count > 0 Then
            txtcustomer.Text = dt(0)("AccDescr")
            txtcustomer.Tag = dt(0)("accid")
            txtaddress.Text = dt(0)("Address1") & vbCrLf & dt(0)("Address2") & vbCrLf & dt(0)("Address3") & vbCrLf & dt(0)("Phone") & vbCrLf & dt(0)("ContactName")

        Else
            txtcustomer.Text = ""
            txtcustomer.Tag = ""
            txtaddress.Text = ""
        End If
        chgbyprg = False
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        If Val(btnupdate.Tag) = 0 Then
            MsgBox("This user do not have permission to Update", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        verify()
    End Sub
    Private Sub verify()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select Jobid from JobTb where jobcode ='" & txtjobcode.Text & "' and jobid<>" & Val(txtjobcode.Tag))
        If dt.Rows.Count > 0 Then
            MsgBox("Job code already exist", MsgBoxStyle.Exclamation)
            txtjobcode.Focus()
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
        If txtJobname.Text = "" Then
            txtJobname.Text = txtcustomer.Text
        End If
        If MsgBox("Verification is succeeded. Do you like to file it?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        saveJob()
        txtprintjob.Text = txtjobcode.Text
        MsgBox("Service Invoice saved successfully", MsgBoxStyle.Information)
        ldRec(Val(txtjobcode.Tag))
        'If isModi Then
        '    btnmodify_Click(btnmodify, New System.EventArgs)
        'Else
        '    AddNew()
        '    makeClear()
        'End If

    End Sub
    Private Sub makeClear()
        chgbyprg = True
        txtJobname.Text = ""
        txtDescription.Text = ""
        txtcustomer.Text = ""
        txtcustomer.Tag = ""
        txtaddress.Text = ""
        txtsiteaddress.Text = ""
        txtsitelocation.Text = ""
        txtscost.Text = Format(0, numFormat)
        lblitmcost.Text = Format(0, numFormat)
        lbljobcost.Text = Format(0, numFormat)
        lblitmcost.Text = Format(0, numFormat)
        lbljobcost.Text = Format(0, numFormat)
        numOpnAmt.Text = Format(0, numFormat)
        numOPNIncome.Text = Format(0, numFormat)
        numQtnAmt.Text = Format(0, numFormat)
        lblprofit.Text = Format(0, numFormat)
        lblprofitcap.Text = "Profit"
        cmbjbType.SelectedIndex = 0
        cmbjbType.Tag = 0
        grdVoucher.Rows.Clear()
        txtjobcode.Tag = ""
        loadInvoices()
        txtjobcode.Tag = 0
        numsto.Text = ""
        numsto.Tag = ""
        chgbyprg = False
        ischgItm = False
        dtpstart.Value = Date.Now
        dtpestimatedDt.Value = Date.Now
        grdRVList.DataSource = Nothing
        grdpayments.DataSource = Nothing
        btndelete.Visible = False
        'loadVoucherDetails()
    End Sub


    Private Sub saveJob()
        _objJob = New clsJob
        With _objJob
            .Jobid = Val(txtjobcode.Tag)
            .jobcode = txtjobcode.Text
            .jobdate = DateValue(dtpdate.Value)
            .jobname = txtJobname.Text
            .JobDescription = txtDescription.Text
            .custcode = Val(txtcustomer.Tag)
            .EstimatedDate = DateValue(dtpestimatedDt.Value)
            .Startdate = DateValue(dtpstart.Value)
            .EstimatedAmt = 0
            .Technician = ""
            .SIID = 0
            .RvId = 0
            If Val(txtscost.Text) = 0 Then txtscost.Text = 0
            .ServiceCost = CDbl(txtscost.Text)
            If Val(lblitmcost.Text) = 0 Then lblitmcost.Text = 0
            .ItemCost = CDbl(lblitmcost.Text)
            .Userid = CurrentUser
            If Val(txtscost.Text) = 0 Then txtscost.Text = 0
            .LabourCost = CDbl(txtscost.Text)
            txtjobcode.Tag = .saveJob()
        End With
        'saveConstruction()
        'If grdVoucher.RowCount > 0 Then
        '    If ischgItm Then saveJobItems()
        'Else
        '    deleteInventory()
        'End If
    End Sub
#Region "STO"
    Private Sub saveTrans()
        Dim TrId As Long
        Dim DiscAcc As Long
        Dim TDrAmt As Double
        Dim dtTable As DataTable
        clsreader()
        clsCnnection()
        Dim FCRt As Integer = 1
        Dim isModi As Boolean
        If Val(numsto.Tag) = 0 Then
            dtTable = _objcmnbLayer._fldDatatable("SELECT InvNo FROM InvNos WHERE InvType = 'STO'")
            If dtTable.Rows.Count > 0 Then
                numsto.Text = dtTable(0)("InvNo")
            End If
chkagain:
            If Not CheckNoExists("", Val(numsto.Text), "STO", "Inventory") Then
                If MsgBox("Document No alreary exist! Do you want to fill next?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Exit Sub
                Else
                    numsto.Text = Val(numsto.Text) + 1
                    GoTo chkagain
                End If
            End If
        Else
            isModi = True
        End If
        If isModi Then
            dtTable = _objcmnbLayer._fldDatatable("SELECT TOP 1 * FROM ItmInvCmnTb WHERE TrId = " & Val(numsto.Tag))
            If dtTable.Rows.Count = 0 Then
                MsgBox("The loaded voucher not found.  It may removed externally.", vbExclamation)
                Exit Sub
            End If
            TrId = Val(numsto.Tag)
        End If
        setInvCmnValue(TrId)
        TrId = Val(_objInv._saveCmn())
        TDrAmt = saveInvTr(TrId)
        UpdateAccounts(TrId, TDrAmt, DiscAcc)
        MsgBox("Stock Out succesfully posted", MsgBoxStyle.Information)
        ldPostedInv()
        btnupdate.Enabled = True
        numsto.Tag = ""
    End Sub
    Private Sub setInvCmnValue(ByVal InvTrid As Long)

        With _objInv
            Dim Dt As Date = DateValue(Date.Now)
            .TrId = InvTrid
            .TrDate = DateValue(dtpdate.Value)
            .TrType = "STO"
            .DocLstTxt = ""
            .Prefix = ""
            .InvNo = Val(numsto.Text)
            .TrRefNo = Val(numsto.Text)
            .CSCode = stockcredit
            .PSAcc = stockdebit
            .JobCode = txtjobcode.Text
            .ImpDoc = 0
            .UserId = CurrentUser
            .IsFC = 0
            .FCRate = 1
            .NFraction = 2
            .FC = ""
            .Discount = 0
            .TrDescription = ""
            .TypeNo = TrTypeNo ' getVouchernumber(cmbVoucherTp.Text)
            .EnaJob = False
            .DocDefLoc = ""
            .BrId = UsrBr
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
            .isModi = IIf(Val(numsto.Tag) > 0, True, False)
            .lpoclass = ""
            .rndoff = 0
            .isTaxInvoice = False
        End With

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

        Dim FCRt As Integer = 1
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
                dtrow("TrTypeNo") = TrTypeNo ' getVouchernumber("IS")
                dtrow("TrDateNo") = getDateNo(CDate(dtpdate.Value))
                dtrow("id") = Val(.Item(ConstId, i).Value)
                'dtrow("WarrentyName") = .Item(ConstLocation, i).Value
                dtrow("SerialNo") = ""

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
        If Val(numsto.Tag) > 0 And TrId > 0 Then
            dtTable = _objcmnbLayer._fldDatatable("SELECT LinkNo FROM AccTrCmn WHERE LnkNo = " & TrId)
            If dtTable.Rows.Count > 0 Then
                LinkNo = dtTable(0)("LinkNo")
                _objcmnbLayer._saveDatawithOutParm("DELETE FROM AccTrDet WHERE LinkNo=" & LinkNo)
            End If
        End If
        setAcctrCmnValue(TrId, LinkNo, "STO", "", Val(numsto.Text), TrTypeNo)
        If dtAccTb Is Nothing Then
            dtAccTb = _objcmnbLayer._fldDatatable("Select top 1 LinkNo,AccountNo,DueDate,Reference,EntryRef,DealAmt,FCAmt,CurrencyCode,CurrRate, " & _
                                                  "JobCode,PDCAcc,BankCode,LPONo,OthCost,TrInf,TermsId,CustAcc,AccWithRef,ChqDate,ChqNo,SuppInvDate," & _
                                                  "vatid,isvatEntry,UnqNo from AccTrDet")
        End If
        If dtAccTb.Rows.Count > 0 Then
            dtAccTb.Rows.Clear()
        End If
        Dim EntRef As String = "STOCK OUT FROM TAILORING ORDER - " & txtjobcode.Text

        setAcctrDetValue(LinkNo, stockdebit, numsto.Text, numsto.Text, CDbl(txttotalItemAmt.Text), txtjobcode.Text, "", 0, 0, "", _
                             "", stockcredit, "", "", 1)
        Dim dlAmt As Double = CDbl(txttotalItemAmt.Text)
        'Credit Entry
        dlAmt = dlAmt * -1
        setAcctrDetValue(LinkNo, stockcredit, Trim(numsto.Text), EntRef, dlAmt, "", "", 0, 0, "", _
                         "", stockdebit, "", "", 1)
        'updateStockTransaction(TrId, LinkNo, "", numsto.Text, "STO")
        _objTr.SaveAccTrWithDt(dtAccTb)
    End Sub
    Private Sub setAcctrCmnValue(ByVal InvTrid As Long, ByVal LinkNo As Long, ByVal JVType As String, ByVal PreFix As String, _
                                 ByVal JVNum As Integer, ByVal TrTypeNo As Integer)
        _objTr.JVType = JVType
        _objTr.JVDate = DateValue(dtpdate.Value)
        _objTr.PreFix = PreFix
        _objTr.JVNum = JVNum
        _objTr.JVTypeNo = TrTypeNo ' getVouchernumber(cmbVoucherTp.Text)
        _objTr.UserId = Trim(CurrentUser & "")
        _objTr.MchName = MACHINENAME
        _objTr.CrtDtTm = DateValue(Date.Now)
        _objTr.TypeNo = 0
        '_objTr.VrDescr = Trim(txtpayee.Text)
        _objTr.IsModi = IIf(LinkNo > 0, 1, 0)
        _objTr.LinkNo = InvTrid
    End Sub
    Private Sub setAcctrDetValue(ByVal lnkNo As Long, ByVal AccountNo As Long, ByVal Reference As String, ByVal EntryRef As String, ByVal DealAmt As Double, ByVal JobCode As String, ByVal JobStr As String, _
                                  ByVal TrInf As Short, ByVal OthCost As Short, ByVal LPO As String, ByVal TermsId As String, ByVal CustAcc As Integer, ByVal AccWithRef As String, _
                                  ByVal CurrencyCode As String, ByVal CurrRate As Double)
        Dim dtrow As DataRow
        Dim dtLPO As Date = DateValue(Date.Now)
        Dim dtSup As Date = DateValue(Date.Now)
        dtrow = dtAccTb.NewRow
        dtrow("LinkNo") = lnkNo
        dtrow("AccountNo") = AccountNo
        dtrow("Reference") = IIf(Reference = "", "ON/AC", Reference)
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
#End Region

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        If isModi Then
            If Val(btndelete.Tag) = 0 Then
                MsgBox("This user do not have permission to Remove", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            If MsgBox("Do you want to remove the current Job?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            If Val(txtjobcode.Tag) = 0 Then Exit Sub
            With _objcmnbLayer
                ._saveDatawithOutParm("DELETE FROM JobTb WHERE Jobid=" & Val(txtjobcode.Tag))
                ._saveDatawithOutParm("DELETE FROM JobReceivedAccessoriesTb WHERE Jobid=" & Val(txtjobcode.Tag))
            End With
            deleteInventory()
            btnmodify_Click(btnmodify, New System.EventArgs)
        End If
    End Sub
    Private Sub Modify()
        chgbyprg = True
        isModi = True
        btnmodify.Text = "New"
        txtjobcode.Text = ""
        txtjobcode.Focus()
        btndelete.Text = "Delete"
        btninvoice.Enabled = True
        btnpurchase.Enabled = True
        btncloseJob.Enabled = True
        btnreceipt.Enabled = True
        btnpayment.Enabled = True
        If userType Then
            btnupdate.Tag = IIf(getRight(114, CurrentUser), 1, 0)
            btndelete.Tag = IIf(getRight(115, CurrentUser), 1, 0)
            btncloseJob.Tag = IIf(getRight(130, CurrentUser), 1, 0)
        Else
            btnupdate.Tag = 1
            btndelete.Tag = 1
            btncloseJob.Tag = 1
        End If
        chgbyprg = False
    End Sub
    Private Sub btnmodify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnmodify.Click
        TabControl2.SelectedIndex = 0
        chgbyprg = True
        isModi = False
        AddNew()
        makeClear()
        btndelete.Text = "New"
        btninvoice.Enabled = False
        btncloseJob.Enabled = False
        If userType Then
            btnupdate.Tag = IIf(getRight(113, CurrentUser), 1, 0)
            btndelete.Tag = IIf(getRight(115, CurrentUser), 1, 0)
            btncloseJob.Tag = IIf(getRight(130, CurrentUser), 1, 0)
        Else
            btnupdate.Tag = 1
            btndelete.Tag = 1
            btncloseJob.Tag = 1
        End If
        chgbyprg = False
    End Sub

    Public Sub ldRec(ByVal jbid As Long)
        If jbid = 0 Then
            Dim dt As DataTable
            dt = _objcmnbLayer._fldDatatable("Select jobid from jobtb where jobcode='" & MkDbSrchStr(txtjobcode.Text) & "'")
            If dt.Rows.Count > 0 Then
                jbid = dt(0)(0)
            End If
        Else
            Modify()
        End If
        If jbid = 0 Then MsgBox("Invalid Job", MsgBoxStyle.Exclamation) : Exit Sub
        Dim ds As DataSet
        _objJob = New clsJob
        With _objJob
            .Jobid = jbid
            .DateFrom = DateValue(Date.Now)
            .DateTo = DateValue(Date.Now)
            .Tp = 21
            ds = .returnJob
        End With
        chgbyprg = True
        grdinvList.DataSource = Nothing
        grdpayments.DataSource = Nothing
        grdRVList.DataSource = Nothing
        If ds.Tables(0).Rows.Count > 0 Then

            txtjobcode.Text = ds.Tables(0)(0)("jobcode")
            txtprintjob.Text = ds.Tables(0)(0)("jobcode")
            txtjobcode.Tag = ds.Tables(0)(0)("Jobid")
            dtpdate.Value = ds.Tables(0)(0)("jobdate")
            txtJobname.Text = ds.Tables(0)(0)("jobname")
            txtDescription.Text = ds.Tables(0)(0)("JobDescription")
            dtpestimatedDt.Value = ds.Tables(0)(0)("EstimatedDate")
            If Not IsDBNull(ds.Tables(0)(0)("JobCloseDate")) Then
                lbldtClosed.Text = Format(DateValue(ds.Tables(0)(0)("JobCloseDate")), DtFormat)
            Else
                lbldtClosed.Text = dtEmpty
            End If

            If Not IsDBNull(ds.Tables(0)(0)("startdate")) Then
                dtpstart.Value = Format(DateValue(ds.Tables(0)(0)("startdate")), DtFormat)
            End If


            txtcustomer.Text = ds.Tables(0)(0)("AccDescr")
            txtcustomer.Tag = ds.Tables(0)(0)("custcode")
            txtaddress.Text = ds.Tables(0)(0)("Address1") & vbCrLf & ds.Tables(0)(0)("Address2") & vbCrLf & ds.Tables(0)(0)("Address3") & vbCrLf & ds.Tables(0)(0)("Phone") & vbCrLf & ds.Tables(0)(0)("ContactName")

            txtscost.Text = Format(CDbl(ds.Tables(0)(0)("LabourCost")), numFormat)
            lblitmcost.Text = Format(CDbl(ds.Tables(0)(0)("ItemCost")), numFormat)
            numsto.Text = Val(ds.Tables(0)(0)("invno") & "")
            numsto.Tag = Val(ds.Tables(0)(0)("trid") & "")

            txtqtnno.Text = Val(ds.Tables(0)(0)("dno") & "")
            txtqtnno.Tag = Val(ds.Tables(0)(0)("docid") & "")
            If Val(ds.Tables(0)(0)("docnetamt") & "") > 0 Then
                numQtnAmt.Text = Format(CDbl(ds.Tables(0)(0)("docnetamt")), numFormat)
            Else
                numQtnAmt.Text = Format(0, numFormat)
            End If
        End If
        getstatus()
        calculate()
        ldItemCost()
        chgbyprg = False
        TabControl2.SelectedIndex = 0
        txtjobcode.Focus()
        btndelete.Visible = True
    End Sub
    Private Sub calculateCost()
        If Not chgbyprg Then
            If Val(txtscost.Text) = 0 Then
                txtscost.Text = 0
            End If
            If Val(lblitmcost.Text) = 0 Then
                lblitmcost.Text = 0
            End If
            If Val(lblexpense.Text) = 0 Then
                lblexpense.Text = 0
            End If
            lbljobcost.Text = Format(CDbl(txtscost.Text) + CDbl(lblexpense.Text) + CDbl(lblitmcost.Text), numFormat)
            If Val(numQtnAmt.Text) = 0 Then numQtnAmt.Text = 0
            lblprofit.Text = Format(CDbl(lblinvamt.Text) - CDbl(lbljobcost.Text), numFormat)
            If Val(lblprofit.Text) = 0 Then lblprofit.Text = Format(0, numFormat)
            If CDbl(lblprofit.Text) < 0 Then
                lblprofitcap.Text = "Loss"
            Else
                lblprofitcap.Text = "Profit"
            End If
        End If

    End Sub
    Private Sub ldItemCost()
        _objJob = New clsJob
        'Dim dt As DataTable = _objcmnbLayer._fldDatatable("select sum(CostAvg*TrQty) costAmt from ItmInvTrTb" & _
        '                                                  " LEFT JOIN ItmInvCmnTb ON ItmInvCmnTb.Trid=ItmInvTrTb.trid  where trtype='STO' AND [Job Code]='" & MkDbSrchStr(numVchrNo.Text) & "'", False)
        Dim dt As DataTable
        _objJob.jobcode = txtjobcode.Text
        dt = _objJob.returnJobcost
        If (dt.Rows.Count > 0) Then
            If Val(dt(0)("ItmCost") & "") = 0 Then dt(0)("ItmCost") = 0
            lblitmcost.Text = Format(CDbl(dt(0)("ItmCost")), numFormat)
            If Val(dt(0)("OthrCost") & "") = 0 Then dt(0)("OthrCost") = 0
            lblexpense.Text = Format(CDbl(dt(0)("OthrCost")), numFormat)
            If Val(dt(0)("InvAmt") & "") = 0 Then dt(0)("InvAmt") = 0
            lblinvamt.Text = Format(CDbl(dt(0)("InvAmt")), numFormat)

            If Val(dt(0)("invsubtotal") & "") = 0 Then dt(0)("invsubtotal") = 0
            lblinvsubtotal.Text = Format(CDbl(dt(0)("invsubtotal")), numFormat)

            If Val(dt(0)("Received") & "") = 0 Then dt(0)("Received") = 0
            lblrv.Text = Format(CDbl(dt(0)("Received")), numFormat)
            lblbalance.Text = Format(CDbl(lblinvamt.Text) - CDbl(lblrv.Text), numFormat)
            If Val(dt(0)("IPAmt") & "") = 0 Then dt(0)("IPAmt") = 0
            lblactualpurchase.Text = "Net.:" & Format(CDbl(dt(0)("IPAmt")), numFormat)

            If Val(dt(0)("ipsubtotal") & "") = 0 Then dt(0)("ipsubtotal") = 0
            lblpurchase.Text = Format(CDbl(dt(0)("ipsubtotal")), numFormat)

            lbljobcost.Text = Format(CDbl(lblitmcost.Text) + CDbl(lblpurchase.Text) + CDbl(lblexpense.Text), numFormat)
            lblprofit.Text = Format(CDbl(lblinvsubtotal.Text) - CDbl(lbljobcost.Text), numFormat)
        End If

        calculateCost()
    End Sub
    Public Sub BeginEdit()
        chgbyprg = True
        With grdVoucher
            If .RowCount = 0 Then Exit Sub
            activecontrolname = "grdVoucher"
            .BeginEdit(True)

        End With
        chgbyprg = False
    End Sub
    Private Sub Textbox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            Dim col As Integer
            col = grdVoucher.CurrentCell.ColumnIndex
            If col = ConstEstAmount Or col = ConstActualAmt Then
                If Not IsNumeric(e.KeyChar) And e.KeyChar <> "." And Not e.KeyChar = Convert.ToChar(Keys.Back) Then e.Handled = True
            ElseIf col = ConstAmtDiff Or col = Constdays Then
                e.Handled = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub btnrem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrem.Click
        RemoveRow()
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

    Private Sub btnupdateConsum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdateConsum.Click
        ''If Val(btnupdateConsum.Tag) = 0 Then
        ''    MsgBox("This user do not have permission to Update Material Consumption", MsgBoxStyle.Exclamation)
        ''    Exit Sub
        ''End If
        'If Val(txtjobcode.Tag) = 0 Then
        '    MsgBox("Invalid Job! Please Select Valid Job", MsgBoxStyle.Exclamation)
        '    Exit Sub
        'End If
        ''If MsgBox("Do you want to file it?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        'saveJobItems()
        'ldPostedInv()
        ''ldItemCost()
        'calculate()
        'MsgBox("Material Consumption Saved Successfully", MsgBoxStyle.Information)
        'btnupdate.Enabled = True
        If Val(txtjobcode.Tag) = 0 Then
            MsgBox("Invalid Job", MsgBoxStyle.Information)
            Exit Sub
        End If
        If grdVoucher.Rows.Count = 0 Then
            MsgBox("Consumption not found", MsgBoxStyle.Information)
            Exit Sub
        End If
        saveTrans()
        btnupdate.Enabled = True
    End Sub

    Private Sub btnldConsum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        fSlctDoc = New SelectInvTr
        With fSlctDoc
            .jobcode = txtjobcode.Text
            .strType = "STO"
            .Text = "Select STOCK OUT Invoice"
            .ShowDialog()
        End With
        If Not fSlctDoc Is Nothing Then fSlctDoc = Nothing
    End Sub

    Private Sub fSlctDoc_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fSlctDoc.FormClosed
        fSlctDoc = Nothing
    End Sub
    Public Sub CheckNLoad(Optional ByVal FromTrId As Long = 0)
        Dim InvList As New DataTable
        If FromTrId <> 0 Then
            InvList = _objcmnbLayer._fldDatatable("SELECT TrId FROM ItmInvCmnTb WHERE  TrId = " & FromTrId)
        Else
            'InvList = _objcmnbLayer._fldDatatable("SELECT TrId FROM ItmInvCmnTb WHERE TrType = 'STO' AND InvNo = " & Val(numsto.Text) & " AND PreFix = '" & MkDbSrchStr(Trim(txtprefix.Text), True) & "'")
        End If
        If InvList.Rows.Count > 0 Then
            numsto.Tag = InvList(0)("TrId")
            InvList = Nothing
            ldPostedInv()
            isModiItm = True
        Else
            MsgBox("Voucher with # [" & txtjobcode.Text & "] not exits !!", vbInformation)
            txtjobcode.Focus()
        End If
        'If InvList.State Then InvList.Close()
    End Sub
    Private Sub ldPostedInv()
        Dim i As Integer
        Dim ItmInvCmnTb As DataTable
        'Dim DocAssgnTb As DataTable
        Dim sRs As DataTable
        'Dim Discount As Double
        Dim UPerPack As Double
        ItmInvCmnTb = _objcmnbLayer._fldDatatable("SELECT ItmInvCmnTb.*,jobname FROM ItmInvCmnTb LEFT JOIN JobTb ON " & _
                                                  "ItmInvCmnTb.[Job Code]=JobTb.jobcode WHERE TrId = " & Val(numsto.Tag) & " AND TrType = 'STO'")
        chgbyprg = True
        If ItmInvCmnTb.Rows.Count = 0 Then GoTo Quit
        getProtectUntil()

        numsto.Text = ItmInvCmnTb(0)("InvNo") 'Val(Mid(!InvNo, 3)) '!InvNo '
        'numsto.Tag = ItmInvCmnTb(0)("Trid")

        '############################################################

        sRs = _objcmnbLayer._fldDatatable("SELECT ItmInvTrTb.*, [Item Code],Warrentyname,FraCount FROM ItmInvTrTb LEFT JOIN InvItm ON InvItm.ItemId = ItmInvTrTb.ItemId " & _
                                          " LEFT JOIN UnitsTb ON UnitsTb.Units=ItmInvTrTb.UNIT " & _
                                          "LEFT JOIN WarrentyMastertb ON WarrentyMastertb.Wid=ItmInvTrTb.WarrentyId WHERE TrId = " & Val(numsto.Tag) & " ORDER BY SlNo")

        Dim j As Integer
        'SetGridHead()
        Dim NDec As Integer
        grdVoucher.Rows.Clear()
        With grdVoucher
            For j = 0 To sRs.Rows.Count - 1
                .Rows.Add(1)
                UPerPack = IIf(sRs(j)("PFraction") = 0 Or IsDBNull(sRs(j)("PFraction")), 1, sRs(j)("PFraction"))
                NDec = Val(sRs(j)("FraCount") & "")
                qtyNumFormat = "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))
                i = .RowCount - 1
                .Item(ConstSlNo, i).Value = i + 1
                .Item(ConstItemCode, i).Value = sRs(i)("item code")
                .Item(ConstDescr, i).Value = sRs(i)("IDescription")
                .Item(ConstUnit, i).Value = sRs(i)("Unit")
                .Item(ConstQty, i).Value = Format(CDbl(sRs(i)("TrQty")) / UPerPack, qtyNumFormat)
                .Item(ConstActualPrice, i).Value = CDbl(sRs(i)("UnitCost")) * UPerPack
                .Item(ConstUPrice, i).Value = Format(CDbl(sRs(i)("UnitCost") * UPerPack), numFormat)
                .Item(ConstTaxP, i).Value = Format(CDbl(sRs(i)("Taxp")), numFormat)
                .Item(ConstTaxAmt, i).Value = Format(CDbl(sRs(i)("TaxAmt")), numFormat)
                .Item(ConstLTotal, i).Value = Format((CDbl(sRs(i)("TrQty")) * CDbl(sRs(i)("CostAvg"))), numFormat)
                .Item(ConstItemID, i).Value = Val(sRs(i)("ItemId"))
                .Item(ConstId, i).Value = Val(sRs(i)("id"))
                .Item(ConstPMult, i).Value = UPerPack
            Next
        End With
        If sRs.Rows.Count > 0 Then sRs.Clear()
        reArrangeNo()
        calculate()
        Exit Sub
Err:
        If MsgBox(Err.Description & Chr(13) & "Retry?", vbRetryCancel + vbQuestion, "Warning During Opening Data File.") = vbRetry Then Resume
Quit:
        chgbyprg = False
    End Sub

    Private Sub fSlctDoc_selectTr(ByVal trid As Long, ByVal TrType As String) Handles fSlctDoc.selectTr
        CheckNLoad(trid)
        fSlctDoc.Close()
        fSlctDoc = Nothing
    End Sub

    Private Sub btninvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btninvoice.Click
        If userType Then
            btninvoice.Tag = IIf(getRight(116, CurrentUser), 1, 0)
        Else
            btninvoice.Tag = 1
        End If
        Dim qtnno As Long
        If Val(txtqtnno.Tag) > 0 Then
            If MsgBox("Do you want Transfer Quotation to Invoice?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                qtnno = Val(txtqtnno.Tag)
            End If
        End If
        If Val(btninvoice.Tag) = 0 Then
            MsgBox("This user do not have permission to Create New Invoice", MsgBoxStyle.Exclamation, Nothing)
        ElseIf Val(txtjobcode.Tag) = 0 Then
            MsgBox("Invalid Job", MsgBoxStyle.Exclamation, Nothing)
        Else
            If (Not fInvoice Is Nothing) Then
                fInvoice = Nothing
            End If
            fInvoice = New MFSalesInvoice
            fInvoice.MdiParent = fMainForm
            fInvoice.Show()
            fInvoice.returnFromJob(Val(txtjobcode.Tag), qtnno)
        End If
    End Sub

    Private Sub fAmount_retunAmt(ByVal amt As Double) Handles fAmount.retunAmt
        invoiceAmount = amt
    End Sub

    Private Sub grdinvList_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdinvList.DoubleClick
        Dim isvalid As Integer
        If userType Then
            isvalid = IIf(getRight(117, CurrentUser), 1, 0)
        Else
            isvalid = 1
        End If
        If isvalid = 0 Then
            MsgBox("This user do not have permission to Edit Invoice", MsgBoxStyle.Exclamation, Nothing)
            Exit Sub
        End If
        'fMainForm.LoadJIS(Val(grdinvList.Item(grdinvList.ColumnCount - 1, grdinvList.CurrentRow.Index).Value))
        If (Not fInvoice Is Nothing) Then
            fInvoice = Nothing
        End If
        fInvoice = New MFSalesInvoice
        fInvoice.MdiParent = fMainForm
        fInvoice.Show()
        fInvoice.CheckNLoad(Val(grdinvList.Item(grdinvList.ColumnCount - 1, grdinvList.CurrentRow.Index).Value))
    End Sub

    Private Sub fInvoice_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fInvoice.FormClosed
        fInvoice = Nothing
    End Sub

    'Private Sub fInvoice_refhreshList() Handles fInvoice.refhreshList
    '    fillGrid()
    'End Sub

    Private Sub btndeleteConsum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Val(btnupdateConsum.Tag) = 0 Then
            MsgBox("This user do not have permission to Remove", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If MsgBox("Do you want to remove the current Consumption Entry?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        deleteInventory()
        ldItemCost()
        grdVoucher.Rows.Clear()
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
        'For i = 0 To itemidsdatatable.Rows.Count - 1
        '    _objInv.ItemId = itemidsdatatable(i)("Itemid")
        '    _objInv.TrDate = DateValue(itemidsdatatable(i)("TrDate"))
        '    _objInv.setcostAverageOnModification()
        'Next
    End Sub

    Private Sub numVchrNo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtjobcode.Validated
        If isModi And Val(txtjobcode.Tag) = 0 Then
            ldRec(0)
        End If
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
    End Sub
    Private Sub ldrvGrid()
        Dim dtTable As DataTable
        With _objTr
            .ptype = 7
            .DateFrom = DateValue(Date.Now)
            .DateTo = DateValue(Date.Now)
            .JVType = "RV"
            .JobCode = txtjobcode.Text
            dtTable = .returnPaymentDetails.Tables(0)
        End With
        grdRVList.DataSource = dtTable
        SetmodiGrid()
        'setComboGrid()
    End Sub
    Private Sub ldPVGrid()
        Dim dtTable As DataTable
        With _objTr
            .ptype = 9
            .DateFrom = DateValue(Date.Now)
            .DateTo = DateValue(Date.Now)
            .JVType = "PV"
            .JobCode = txtjobcode.Text
            dtTable = .returnPaymentDetails.Tables(0)
        End With
        grdpayments.DataSource = dtTable
        SetmodiPVGrid()
        'setComboGrid()
    End Sub
    Private Sub ldPurchaseGrid()
        _objJob = New clsJob
        grdpurchase.DataSource = Nothing
        Dim source As DataTable = _objJob.returnVoucherList(Val(txtjobcode.Tag), 1)
        grdpurchase.DataSource = source
        SetGridHeadInv(grdpurchase, 1)

    End Sub
    Sub SetmodiPVGrid()
        With grdpayments
            SetGridProperty(grdpayments)


            .Columns(ConstInvNo).HeaderText = "PV No"
            .Columns(ConstInvNo).Width = 75
            '.Columns(ConstInvNo).SortMode = DataGridViewColumnSortMode.Automatic

            .Columns(ConstReference).HeaderText = "Reference"
            .Columns(ConstReference).Width = 75

            .Columns(ConstTrdate).HeaderText = "Tr Date"
            .Columns(ConstTrdate).Width = 90
            .Columns(ConstTrdate).SortMode = DataGridViewColumnSortMode.Automatic
            .Columns(ConstTrdate).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

            .Columns(ConstInvAmount).HeaderText = "Amount"
            .Columns(ConstInvAmount).Width = 120
            .Columns(ConstInvAmount).SortMode = DataGridViewColumnSortMode.Automatic
            .Columns(ConstInvAmount).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstInvAmount).DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns(ConstCustId).HeaderText = "Cust.Code"
            .Columns(ConstCustId).Width = 75
            .Columns(ConstCustId).SortMode = DataGridViewColumnSortMode.Automatic
            .Columns(ConstCustId).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(ConstCustId).Visible = False

            .Columns(ConstCustname).HeaderText = "Account Name"
            .Columns(ConstCustname).Width = 150
            .Columns(ConstCustname).SortMode = DataGridViewColumnSortMode.Automatic


            .Columns(ConstTrRef).HeaderText = "Description"
            .Columns(ConstTrRef).Width = 300
            .Columns(ConstTrRef).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstTrRef).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            '.Columns(ConstTrRef).Visible = False

            .Columns(Consttype).HeaderText = "Type"
            .Columns(Consttype).Width = 7.5 '100
            .Columns(Consttype).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(Consttype).Visible = False

            .Columns(ConstLinkNo).HeaderText = "Link No"
            .Columns(ConstLinkNo).Width = 250 '100
            .Columns(ConstLinkNo).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstLinkNo).Visible = False

            .Columns("ChqDate").HeaderText = "Chq Date"
            .Columns("ChqDate").Width = 80 '100
            '.Columns("ChqDate").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("ChqDate").Visible = False

            .Columns("ChqNo").HeaderText = "Cheque#"
            .Columns("ChqNo").Width = 80 '100
            '.Columns("ChqNo").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("ChqNo").Visible = False


            .Columns("DateFrom").Visible = False
            .Columns("DateTo").Visible = False
            .Columns("Lnk").Visible = False
            resizeGridColumn(grdpayments, ConstTrRef)

        End With

    End Sub
    Sub SetmodiGrid()
        With grdRVList
            SetGridProperty(grdRVList)


            .Columns(ConstInvNo).HeaderText = "RV No"
            .Columns(ConstInvNo).Width = 75

            .Columns(ConstReference).HeaderText = "Reference"
            .Columns(ConstReference).Width = 75
            '.Columns(ConstInvNo).SortMode = DataGridViewColumnSortMode.Automatic

            .Columns(ConstTrdate).HeaderText = "Tr Date"
            .Columns(ConstTrdate).Width = 90
            .Columns(ConstTrdate).SortMode = DataGridViewColumnSortMode.Automatic
            .Columns(ConstTrdate).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

            .Columns(ConstInvAmount).HeaderText = "Amount"
            .Columns(ConstInvAmount).Width = 120
            .Columns(ConstInvAmount).SortMode = DataGridViewColumnSortMode.Automatic
            .Columns(ConstInvAmount).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstInvAmount).DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns(ConstCustId).HeaderText = "Cust.Code"
            .Columns(ConstCustId).Width = 75
            .Columns(ConstCustId).SortMode = DataGridViewColumnSortMode.Automatic
            .Columns(ConstCustId).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(ConstCustId).Visible = False

            .Columns(ConstCustname).HeaderText = "Payment Mode"
            .Columns(ConstCustname).Width = 150
            .Columns(ConstCustname).SortMode = DataGridViewColumnSortMode.Automatic


            .Columns(ConstTrRef).HeaderText = "Description"
            .Columns(ConstTrRef).Width = 300
            .Columns(ConstTrRef).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstTrRef).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

            .Columns(Consttype).HeaderText = "Type"
            .Columns(Consttype).Width = 7.5 '100
            .Columns(Consttype).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(Consttype).Visible = False

            .Columns(ConstLinkNo).HeaderText = "Link No"
            .Columns(ConstLinkNo).Width = 250 '100
            .Columns(ConstLinkNo).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstLinkNo).Visible = False

            .Columns("ChqDate").HeaderText = "Chq Date"
            .Columns("ChqDate").Width = 80 '100
            '.Columns("ChqDate").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("ChqDate").Visible = False

            .Columns("ChqNo").HeaderText = "Cheque#"
            .Columns("ChqNo").Width = 80 '100
            '.Columns("ChqNo").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("ChqNo").Visible = False


            .Columns("DateFrom").Visible = False
            .Columns("DateTo").Visible = False
            .Columns("Lnk").Visible = False
            .Columns("JVTypeNo").Visible = False
            resizeGridColumn(grdRVList, ConstTrRef)

        End With

    End Sub
    'Private Sub loadVoucherDetails()
    '    Dim dt As DataTable
    '    _objJob = New clsJob
    '    Dim linkno As Long

    '    If Not grdRVList.CurrentRow Is Nothing Then
    '        linkno = Val(grdRVList.Item(ConstLinkNo, grdRVList.CurrentRow.Index).Value)
    '    Else
    '        linkno = 0
    '    End If
    '    dt = _objJob.returnJVDetails(linkno, numVchrNo.Text, 0)
    '    grdrvDetails.DataSource = dt
    '    setRvListGrdHead(grdrvDetails)
    'End Sub
    Private Sub setRvListGrdHead(ByVal grd As DataGridView)
        With grd
            SetGridProperty(grd)

            .Columns("Amount").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End With

    End Sub


    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        If isModi And Val(txtjobcode.Tag) > 0 Then
            ldRec(Val(txtjobcode.Tag))
            ldPVGrid()
            ldrvGrid()
            loadInvoices()
            ldPurchaseGrid()
        End If
    End Sub
    Private Sub setHistory(ByVal ItemId As Long, ByVal strCode As String, Optional ByVal PartyId As Long = 0, Optional ByVal strType As String = "")
        If fHistory Is Nothing Then
            fHistory = New SelectHistory
        End If
        With fHistory
            If strType <> "" Then
                .strType = strType
            Else
                Exit Sub
            End If
            .Itemid = ItemId
            .ItemCode = strCode
            .PartyId = PartyId
            .dt_From.Value = DateFrom
            .jobcode = txtjobcode.Text
            If fHistory.Visible Then
                .setEnquiryLoad()
            Else
                .Show()
                .Top = Me.Top + Screen.PrimaryScreen.WorkingArea.Height - .Height + 20
            End If
        End With
    End Sub

    Private Sub fHistory_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fHistory.FormClosed
        fHistory = Nothing
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Dim myctrl As Button
        myctrl = sender
        Dim rptype As String = ""
        Select Case myctrl.Name
            Case "btnPreview"
                rptype = "JBC"
        End Select
        If chkFormat.Checked Then
            fRptFormat = New RptFormatfrm
            fRptFormat.RptType = rptype
            fRptFormat.ShowDialog()
        Else
            PrepareRpt(rptype)
        End If
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
            LoadReport(RptName, RptCaption, RptType, Forprint)
        End If
    End Sub
    Private Sub LoadReport(ByVal FileName As String, ByVal RptCaption As String, ByVal RptType As String, Optional ByVal ToPrint As Boolean = False)
        If Not frm Is Nothing Then
            frm.Close()
            frm = New ReprtviewNEWfrm
        Else
            frm = New ReprtviewNEWfrm
        End If
        _objJob = New clsJob
        Dim ds As New DataSet
        If RptType = "JBC" Then
            _objJob.jobcode = txtprintjob.Text
            ds = _objJob.returnJobForInvPrint(1)
            'ElseIf RptType = "MC" Then
            '    _objInv.Prefix = ""
            '    _objInv.InvNo = Val(txtPsto.Text)
            '    _objInv.TrType = "STO"
            '    ds = _objInv.ldInvoice("rturnInventoryDetailsForInvoicePrint")
        ElseIf RptType = "JQT" Then
            _objJob.jobcode = txtjobcode.Text
            ds = _objJob.returnJobwiseQty
        End If
        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = fMainForm
        frm.Text = RptCaption
        frm.Show()

    End Sub

    Private Sub frm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles frm.FormClosed
        frm = Nothing
    End Sub

    Private Sub btncloseJob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncloseJob.Click
        If Val(txtjobcode.Tag) = 0 Then
            MsgBox("Select Valid Job", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If btncloseJob.Text = "Job Closing" Then
            If Val(btncloseJob.Tag) = 0 Then
                MsgBox("This user do not have permission to Close Job", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            Dim frm As New JobClosing
            frm.cldrdate.Tag = Val(txtjobcode.Tag)
            frm.lbljjob.Text = "Job Code : " & txtjobcode.Text
            frm.ShowDialog()
        Else
            If MsgBox("Do you want activate Job?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            _objcmnbLayer._saveDatawithOutParm("Update JobTb set Status=0,JobCloseDate=null where Jobid=" & Val(txtjobcode.Tag))
            MsgBox("Job Activated", MsgBoxStyle.Information)
        End If
        getstatus()
    End Sub
    Private Sub getstatus()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT Status,JobCloseDate FROM JobTb WHERE Jobid=" & Val(txtjobcode.Tag))
        If dt.Rows.Count > 0 Then
            If Val(dt(0)("Status") & "") > 0 Then
                lbldtClosed.Text = Format(DateValue(dt(0)("JobCloseDate")), DtFormat)
                btncloseJob.Text = "Undo Closing"
                lblstatus.Text = "Closed Job"
                lblstatus.ForeColor = Color.Red
                lblstatus.Visible = True
            Else
                btncloseJob.Text = "Job Closing"
                lbldtClosed.Text = dtEmpty
                lblstatus.Text = "Active Job"
                lblstatus.ForeColor = Color.Green
                lblstatus.Visible = True
            End If
        End If
    End Sub

    Private Sub cmbjbType_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbjbType.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub btnpayment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpayment.Click
        Dim isvalid As Integer
        If userType Then
            isvalid = IIf(getRight(66, CurrentUser), 1, 0)
        Else
            isvalid = 1
        End If
        If isvalid = 0 Then
            MsgBox("This user do not have permission to Create Payment", MsgBoxStyle.Exclamation, Nothing)
            Exit Sub
        End If
        If txtjobcode.Text = "" Then Exit Sub
        'fMainForm.LoadPVO(, txtjobcode.Text)
        Dim fPV As New ExpensePayments
        With fPV
            .chgbyprg = True
            .WindowState = FormWindowState.Normal
            .StartPosition = FormStartPosition.CenterParent
            .txtjobcode.Text = txtjobcode.Text
            '.jobSupplier = Val(txtcustomer.Tag)
            '.ldjbname()
            .chgbyprg = False
            .ShowDialog()
        End With
    End Sub

    Private Sub btnreceipt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreceipt.Click
        Dim isvalid As Integer
        If cmbrvtype.SelectedIndex = 0 Then
            If userType Then
                isvalid = IIf(getRight(62, CurrentUser), 1, 0)
            Else
                isvalid = 1
            End If
            If isvalid = 0 Then
                MsgBox("This user do not have permission to Create Receipt", MsgBoxStyle.Exclamation, Nothing)
                Exit Sub
            End If
            If Val(txtjobcode.Tag) = 0 Then Exit Sub
            If txtcustomer.Text = "" Then Exit Sub
            'fMainForm.LoadRV(, txtcustomer.Text)
            Dim fRV As New MFCustomerReceipt
            With fRV
                .chgbyprg = True
                .txtSuppName.Text = txtcustomer.Text
                .WindowState = FormWindowState.Normal
                .StartPosition = FormStartPosition.CenterParent
                .chgbyprg = False
                'fRV.loadFromJob()
                .ShowDialog()
            End With

        Else
            If userType Then
                isvalid = IIf(getRight(62, CurrentUser), 1, 0)
            Else
                isvalid = 1
            End If
            If isvalid = 0 Then
                MsgBox("This user do not have permission to Create Receipt", MsgBoxStyle.Exclamation, Nothing)
                Exit Sub
            End If
            If txtjobcode.Text = "" Then Exit Sub
            'fMainForm.LoadRVO(, txtjobcode.Text)
            Dim freceipt As New OtherReceiptsFrm
            If Val(txtjobcode.Tag) = 0 Then Exit Sub
            If Val(txtcustomer.Tag) = 0 Then Exit Sub
            With freceipt
                .chgbyprg = True
                .WindowState = FormWindowState.Normal
                .StartPosition = FormStartPosition.CenterParent
                .txtjobcode.Text = txtjobcode.Text
                .jobCustomer = Val(txtcustomer.Tag)
                '.ldjbname()
                .chgbyprg = False
                .ShowDialog()
            End With
        End If

    End Sub

    Private Sub btneditpayment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btneditpayment.Click
        Dim isvalid As Integer
        If userType Then
            isvalid = IIf(getRight(67, CurrentUser), 1, 0)
        Else
            isvalid = 1
        End If
        If isvalid = 0 Then
            MsgBox("This user do not have permission to Create Receipt", MsgBoxStyle.Exclamation, Nothing)
            Exit Sub
        End If
        If txtjobcode.Text = "" Then Exit Sub
        If grdpayments.CurrentRow Is Nothing Then Exit Sub
        fMainForm.LoadPVO(Val(grdpayments.Item(ConstLinkNo, grdpayments.CurrentRow.Index).Value), txtjobcode.Text)
    End Sub

    Private Sub btneditreceipt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim isvalid As Integer
        If userType Then
            isvalid = IIf(getRight(63, CurrentUser), 1, 0)
        Else
            isvalid = 1
        End If
        If isvalid = 0 Then
            MsgBox("This user do not have permission to Edit Receipt", MsgBoxStyle.Exclamation, Nothing)
            Exit Sub
        End If
        If txtjobcode.Text = "" Then Exit Sub
        If grdRVList.CurrentRow Is Nothing Then Exit Sub
        If grdRVList.Item("JVTypeNo", grdRVList.CurrentRow.Index).Value = 103 Then
            fMainForm.LoadRVO(Val(grdRVList.Item(ConstLinkNo, grdRVList.CurrentRow.Index).Value), txtjobcode.Text)
        Else
            fMainForm.LoadRV(Val(grdRVList.Item(ConstLinkNo, grdRVList.CurrentRow.Index).Value), txtcustomer.Text)
        End If
    End Sub

    Private Sub btnOutstanding_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim isvalid As Integer
        If userType Then
            isvalid = IIf(getRight(62, CurrentUser), 1, 0)
        Else
            isvalid = 1
        End If
        If isvalid = 0 Then
            MsgBox("This user do not have permission to Create Receipt", MsgBoxStyle.Exclamation, Nothing)
            Exit Sub
        End If
        If txtjobcode.Text = "" Then Exit Sub
        If txtcustomer.Text = "" Then Exit Sub
        fMainForm.LoadRV(, txtcustomer.Text)
    End Sub

    Private Sub btnpurchase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpurchase.Click
        Dim isvalid As Integer
        If userType Then
            isvalid = IIf(getRight(40, CurrentUser), 1, 0)
        Else
            isvalid = 1
        End If
        If isvalid = 0 Then
            MsgBox("This user do not have permission to Create Purchase", MsgBoxStyle.Exclamation, Nothing)
            Exit Sub
        End If
        If txtjobcode.Text = "" Then Exit Sub
        fMainForm.LoadIP(, txtjobcode.Text)
    End Sub

    Private Sub grdpurchase_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdpurchase.DoubleClick
        Dim trid As Long
        If grdpurchase.RowCount > 0 Then
            trid = Val(grdpurchase.Item("trid", grdpurchase.CurrentRow.Index).Value)
        End If
        If trid > 0 Then
            fMainForm.LoadIP(trid)
        End If

    End Sub
    Private Sub fcustomer_OpenAccMaster(ByRef AccountNo As Long) Handles fcustomer.OpenAccMaster
        setCustomer(AccountNo)
        txtsitelocation.Focus()
    End Sub

    Private Sub btnqtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnqtn.Click
        If Val(txtjobcode.Tag) = 0 Then Exit Sub
        If Val(txtcustomer.Tag) = 0 Then
            MsgBox("Invalid Customer", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If fqti Is Nothing Then
            fqti = New CustomerQuotation
        End If
        With fqti
            .MdiParent = fMainForm
            .Show()
            If Val(txtqtnno.Tag) = 0 Then
                .loadFroJob(Val(txtcustomer.Tag), txtjobcode.Text, txtJobname.Text, False)
            Else
                .CheckNLoad(Val(txtqtnno.Tag))
            End If

        End With
    End Sub

    Private Sub fqti_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fqti.FormClosed
        fqti = Nothing
    End Sub

    Private Sub btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnload.Click
        ldJobdetails()
    End Sub
    Private Sub ldJobdetails()
        _objJob = New clsJob
        Dim qry As String
        Dim condition As String = ""
        Select Case cmbtype.SelectedIndex
            Case 0
                If cmbdatewise.SelectedIndex = 1 Then
                    condition = "where jobdate>='" & Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd") & "' and jobdate<='" & Format(DateValue(cldrEnddate.Value), "yyyy/MM/dd") & "'"
                Else
                    condition = " where 1=1"
                End If
                condition = condition & " and isnull([status],0)=0"
            Case 2
                If cmbdatewise.SelectedIndex = 0 Then
                    condition = "where jobdate>='" & Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd") & "' and jobdate<='" & Format(DateValue(cldrEnddate.Value), "yyyy/MM/dd") & "'"
                ElseIf cmbdatewise.SelectedIndex = 2 Then
                    condition = "where convert(date,JobCloseDate,103)>='" & Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd") & "' and convert(date,JobCloseDate,103)<='" & Format(DateValue(cldrEnddate.Value), "yyyy/MM/dd") & "'"

                End If
            Case 1
                If cmbdatewise.SelectedIndex = 0 Then
                    condition = "where jobdate>='" & Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd") & "' and jobdate<='" & Format(DateValue(cldrEnddate.Value), "yyyy/MM/dd") & "'"
                Else
                    condition = "where convert(date,JobCloseDate,103)>='" & Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd") & "' and convert(date,JobCloseDate,103)<='" & Format(DateValue(cldrEnddate.Value), "yyyy/MM/dd") & "'"
                End If
                condition = condition & " and isnull([status],0)=1"
        End Select
        qry = "select jobcode,jobdate,jobname,case when status=1 then 'YES' else 'NO' end Closed,accdescr,Phone,JobCloseDate," & _
            "isnull(jobtb.NetAmt,0)InvAmt,isnull(subtotal,0)invsubtotal,isnull(ipsubtotal,0) ipsubtotal, isnull(costAmt,0)+isnull(ipsubtotal,0)+isnull(OthrCost,0) TotalCost," & _
            "isnull(subtotal,0)-(isnull(costAmt,0)+isnull(ipsubtotal,0)+isnull(OthrCost,0)) Profit," & _
            "userid,crdtdate,'" & Format(DateValue(cldrStartDate.Value), "dd/MM/yyyy") & "' datefrom,'" & Format(DateValue(cldrEnddate.Value), "dd/MM/yyyy") & "' dateto,1 lnk,jobid " & _
            "from jobtb left join accmast on jobtb.custcode=accmast.accid " & _
            "left join accmastaddr on accmast.accid =accmastaddr.accountno " & _
            "left join (select sum(netamt)netamt,[Job Code],sum(subtotal)subtotal from ItmInvCmnTb " & _
            "left join (select trid,sum(isnull(price,0)) subtotal from  (select trid,((UnitCost-UnitDiscount)-(ItemDiscount/TrQty))*TrQty price,TrQty " & _
            "from ItmInvTrTb)tr group by trid)invtr on ItmInvCmnTb.trid=invtr.TrId " & _
            "where TrType='IS' group by [Job Code])inv on JobTb.jobcode=inv.[Job Code] " & _
            "left join (select SUM(NetAmt) costAmt,[Job Code] from  ItmInvCmnTb " & _
            "where trtype='STO' group by [Job Code]) tr on JobTb.jobcode=tr.[Job Code] " & _
            "left join (select [Job Code],sum(NetAmt) IPAmt,sum(subtotal)ipsubtotal from ItmInvCmnTb " & _
            "left join (select trid,sum(isnull(price,0)) subtotal from  (select trid,((UnitCost-UnitDiscount)-(ItemDiscount/TrQty))*TrQty price,TrQty " & _
            "from ItmInvTrTb)tr group by trid)invtr on ItmInvCmnTb.trid=invtr.TrId " & _
            "where trtype='IP' group by [Job Code] ) IPtr on IPtr.[Job Code]=JobTb.jobcode " & _
            "left join (select sum(dealamt) OthrCost,JobCode aJcode from AccTrDet " & _
            "left join AccTrCmn on AccTrDet.LinkNo=AccTrCmn.LinkNo " & _
            "where  JVType in('pv','jv') and DealAmt>0 group by JobCode ) acctr on acctr.aJcode =JobTb.jobcode " & _
            condition & " order by JobCode"

        grdItem.DataSource = Nothing
        _vtable = _objcmnbLayer._fldDatatable(qry)
        grdItem.DataSource = _vtable
        SetGrid()
        'resizeGridColumn(grdItem, 3)
        gridTotal(_vtable)
    End Sub
    Private Sub gridTotal(ByVal dt As DataTable)
        If dt.Rows.Count = 0 Then Exit Sub
        Dim amt As String
        amt = Trim(dt.Compute("SUM([InvAmt])", "") & "")
        lblInvoiceSummary.Text = Format(CDbl(amt), numFormat)
        amt = Trim(dt.Compute("SUM([TotalCost])", "") & "")
        lbltotalcost.Text = Format(CDbl(amt), numFormat)
        amt = Trim(dt.Compute("SUM([Profit])", "") & "")
        lbltotalprofit.Text = Format(CDbl(amt), numFormat)
    End Sub
    Private Sub SetGrid()
        With grdItem
            If .ColumnCount = 0 Then Exit Sub
            SetGridProperty(grdItem)

            .Columns("Jobcode").HeaderText = "Job Code"
            .Columns("jobdate").HeaderText = "Job Date"
            .Columns("jobdate").Width = 80

            .Columns("Jobname").HeaderText = "Job Name"
            .Columns("Closed").Width = 50
            .Columns("Closed").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("AccDescr").HeaderText = "Customer Name"
            .Columns("JobCloseDate").HeaderText = "Closed Date"
            .Columns("JobCloseDate").Width = 135

            .Columns("InvAmt").HeaderText = "Job Value"
            .Columns("InvAmt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("InvAmt").DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns("invsubtotal").HeaderText = "Inv Subtotal"
            .Columns("invsubtotal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("invsubtotal").DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns("ipsubtotal").HeaderText = "IP Cost"
            .Columns("ipsubtotal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("ipsubtotal").DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns("TotalCost").HeaderText = "Job Cost"
            .Columns("TotalCost").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("TotalCost").DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns("Profit").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Profit").DefaultCellStyle.Format = "N" & NoOfDecimal


            .Columns("Userid").HeaderText = "Created By"
            .Columns("CrdtDate").HeaderText = "Created Date"

            .Columns("Datefrom").Visible = False
            .Columns("Dateto").Visible = False
            .Columns("lnk").Visible = False
            .Columns("jobid").Visible = False
            .Columns("AccDescr").Width = 200
            .Columns("CrdtDate").Width = 150
            .Columns("Jobname").Visible = False

            setComboGrid()
            '
        End With
    End Sub
    Private Sub setComboGrid()
        cmbOrder.Items.Clear()
        Dim i As Integer = 0
        For i = 0 To 11
            cmbOrder.Items.Add(grdItem.Columns(i).HeaderText)
        Next
        If cmbOrder.Items.Count > 0 Then cmbOrder.SelectedIndex = 4
    End Sub

    Private Sub grdItem_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdItem.DoubleClick
        Dim jobid As Long
        If grdItem.RowCount = 0 Then Exit Sub
        jobid = grdItem.Item("jobid", grdItem.CurrentRow.Index).Value
        ldRec(jobid)
    End Sub

    Private Sub fqti_returnqtidetails(ByVal trid As Long) Handles fqti.returnqtidetails
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("select netamt,DNO from DocCmnTb where docid=" & trid)
        If dt.Rows.Count > 0 Then
            txtqtnno.Text = dt(0)("DNO")
            txtqtnno.Tag = trid
            numQtnAmt.Text = Format(CDbl(dt(0)("netamt")), numFormat)
        End If
    End Sub
    Private Sub loadEstimatedItems(ByVal Trid As Long)
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT [Item Code],TrDetail,Qty,CostPUnit-UnitDiscount-(ItemDiscount/Qty)CostPUnit," & _
                                         "((CostPUnit-UnitDiscount)*Qty)-isnull(ItemDiscount,0)+isnull(taxAmt,0) LineTotal FROM DocTranTb " & _
                                          "LEFT JOIN InvItm ON InvItm.ItemId = DocTranTb.ItemId  " & _
                                          "LEFT JOIN VatMasterTb ON InvItm.vatId=VatMasterTb.vatId " & _
                                          "LEFT JOIN (SELECT foundImport FROM (SELECT impDocSlno foundImport FROM ItmInvTrTb " & _
                                          "UNION ALL SELECT ImpDocLnNo FROM DocTranTb)tr  GROUP BY foundImport) As PIQ ON PIQ.foundImport = DocTranTb.id " & _
                                          "LEFT JOIN (select vatcode rgccode,vat rgcess, collectionAC rgcaccount,vatid rgcid from  VatMasterTb)regularcess ON InvItm.regularcessid=regularcess.rgcid " & _
                                          "WHERE Docid = " & Trid & " ORDER BY SlNo")
        grdestimateditems.DataSource = dt
        With grdestimateditems
            SetGridProperty(grdestimateditems)
            .Columns("Qty").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Qty").DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns("CostPUnit").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("CostPUnit").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("CostPUnit").HeaderText = "Price"

            .Columns("LineTotal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("LineTotal").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("LineTotal").HeaderText = "Line Total"

            resizeGridColumn(grdestimateditems, 1)

        End With
    End Sub

    Private Sub TabControl3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl3.Click
        If TabControl3.SelectedIndex = 1 Then
            loadEstimatedItems(Val(txtqtnno.Tag))
        ElseIf TabControl3.SelectedIndex = 2 Then
            loadInvoices()
        ElseIf TabControl3.SelectedIndex = 3 Then
            ldPurchaseGrid()
        ElseIf TabControl3.SelectedIndex = 4 Then
            ldrvGrid()
        ElseIf TabControl3.SelectedIndex = 5 Then
            ldPVGrid()
        ElseIf TabControl3.SelectedIndex = 6 Then
            resizeGridColumn(grdVoucher, ConstDescr)
            If Val(numsto.Tag) > 0 Then ldPostedInv()
        End If
    End Sub
    Private Sub ldDocumentItems(ByVal trid As Long, ByVal grd As DataGridView)
        Try
            Dim itmDatatable As DataTable
            itmDatatable = _objReport.returnItemsToListForm("returnInventoryItemsToListForm", trid, 1).Tables(0)
            grd.DataSource = itmDatatable
            setItemGridHead(grd)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub setItemGridHead(ByVal grdItemView As DataGridView)
        With grdItemView
            SetGridProperty(grdItemView)
            .Columns("Item Code").Width = 100
            .Columns("IDescription").Width = 200
            .Columns("Unit").Width = 50
            .Columns("TrQty").Width = 70
            .Columns("TrQty").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("TrQty").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("UnitCost").Width = 100
            .Columns("UnitCost").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("UnitCost").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns("LineTotal").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("LineTotal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            If Not ldType = "TIS" Then
                .Columns("Taxp").Width = 100
                .Columns("Taxp").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("Taxp").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("TaxAmt").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("TaxAmt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("Cess").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("Cess").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                If Not ldType = "JIS" Then
                    .Columns("FOC").DefaultCellStyle.Format = "N" & NoOfDecimal
                    .Columns("FOC").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns("FOC").Visible = enableFOCQty
                    .Columns("FOC").Width = 70
                    .Columns("SerialNo").Visible = enableSerialnumber Or enableBatchwiseTr
                End If

                If enablecess Then
                    .Columns("Cess").Visible = True
                Else
                    .Columns("Cess").Visible = False
                End If

            End If

            resizeGridColumn(grdItemView, 1)
        End With
    End Sub

    Private Sub grdinvList_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdinvList.RowEnter
        If grdinvList.RowCount = 0 Then Exit Sub
        ldDocumentItems(Val(grdinvList.Item("trid", e.RowIndex).Value), grdinvitems)
    End Sub

    Private Sub grdpurchase_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdpurchase.CellContentClick

    End Sub

    Private Sub grdpurchase_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdpurchase.RowEnter
        If grdpurchase.RowCount = 0 Then Exit Sub
        ldDocumentItems(Val(grdpurchase.Item("trid", e.RowIndex).Value), grdpurchaseitems)
    End Sub

    Private Sub grdRVList_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdRVList.CellContentClick

    End Sub

    Private Sub grdRVList_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdRVList.DoubleClick
        If grdRVList.Item("JVTypeNo", grdRVList.CurrentRow.Index).Value = 103 Then
            Dim freceipt As New OtherReceiptsFrm
            If Val(txtjobcode.Tag) = 0 Then Exit Sub
            If Val(txtcustomer.Tag) = 0 Then Exit Sub
            With freceipt
                .chgbyprg = True
                .WindowState = FormWindowState.Normal
                .StartPosition = FormStartPosition.CenterParent
                .txtjobcode.Text = txtjobcode.Text
                .isModi = True
                .editlinkno = Val(grdRVList.Item("Linkno", grdRVList.CurrentRow.Index).Value)
                '.ldjbname()
                .chgbyprg = False
                .ShowDialog()
            End With
        Else
            Dim fRV As New MFCustomerReceipt
            With fRV
                .chgbyprg = True
                .txtSuppName.Text = txtcustomer.Text
                .WindowState = FormWindowState.Normal
                .StartPosition = FormStartPosition.CenterParent
                .chgbyprg = False
                .isModi = True
                .editlinkno = Val(grdRVList.Item("Linkno", grdRVList.CurrentRow.Index).Value)
                '.editRecord(Val(grdRVList.Item("Linkno", grdRVList.CurrentRow.Index).Value), txtcustomer.Text)
                .ShowDialog()
            End With
        End If
    End Sub

    Private Sub grdpayments_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdpayments.CellContentClick

    End Sub

    Private Sub grdpayments_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdpayments.DoubleClick
        If grdpayments.RowCount = 0 Then Exit Sub
        Dim fPV As New ExpensePayments
        With fPV
            .chgbyprg = True
            .WindowState = FormWindowState.Normal
            .StartPosition = FormStartPosition.CenterParent
            .txtjobcode.Text = txtjobcode.Text
            .isModi = True
            .editlinkno = Val(grdpayments.Item("Linkno", grdpayments.CurrentRow.Index).Value)
            '.jobSupplier = Val(txtcustomer.Tag)
            '.ldjbname()
            .chgbyprg = False
            .ShowDialog()
        End With
    End Sub

    Private Sub btnitmAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnitmAdd.Click
        AddRow()
    End Sub
    Public Sub AddRow(Optional ByVal tocheck As Boolean = False)
        If Val(txtjobcode.Tag) = 0 Then
            MsgBox("Invalid Job", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        chgbyprg = True
        Dim i As Integer
        With grdVoucher
            activecontrolname = "grdVoucher"
            i = .RowCount '- 1
            .Rows.Add(1)
            .Item(ConstSlNo, i).Value = i + 1
            .Item(ConstItemCode, i).Value = ""
            .Item(ConstDescr, i).Value = ""
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

    Private Sub grdVoucher_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellClick
        chgbyprg = True
        With grdVoucher
            If Val(grdVoucher.Item(0, grdVoucher.CurrentCell.RowIndex).Value) = 0 And e.ColumnIndex <> 3 Then
                grdVoucher.CurrentCell.ReadOnly = True
            ElseIf e.ColumnIndex <> ConstSlNo And e.ColumnIndex <> constItmTot And e.ColumnIndex <> ConstUnit And e.ColumnIndex <> ConstLTotal And e.ColumnIndex <> ConstBarcode And e.ColumnIndex <> ConstTaxAmt Then
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
    Private Sub grdBeginEdit()
        chgbyprg = True
        grdVoucher.BeginEdit(True)
        chgbyprg = False
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

    Private Sub grdVoucher_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellValidated
        ValidStockEntry(e.RowIndex, e.ColumnIndex)
    End Sub
    Private Sub ValidStockEntry(ByVal RowIndex As Integer, ByVal ColIndex As Integer)
        With grdVoucher
            Select Case ColIndex
                Case ConstBarcode, ConstItemCode
                    If Not ischgItm Then Exit Sub
                    If Trim(.Item(ColIndex, RowIndex).Value) = "" Then Exit Sub
                    Dim dtItms As DataTable
                    Dim found As Boolean
                    'dtItms = getItmDtls(3, SrchText, True, Trim(.Item(ColIndex, RowIndex).Value))
                    dtItms = ItmValidation(3, SrchText, True, "IP", 0)
                    If dtItms.Rows.Count > 0 Then
                        found = True
                        AddStockDetails(dtItms)
                    End If
                    ischgItm = False
                    If Not found Then
                        .Item(ConstBarcode, RowIndex).Value = ""
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
                        'If EnableGST Then getGSTDetails(Trim(.Item(ConstBarcode, RowIndex).Value & ""), RowIndex, True, True)
                        calculate()
                    End If
                Case ConstUPrice
                    If chgAmt Then
                        .Item(ConstActualPrice, RowIndex).Value = CDbl(.Item(ConstUPrice, RowIndex).Value)
                        'If EnableGST Then getGSTDetails(Trim(.Item(ConstBarcode, RowIndex).Value & ""), RowIndex, True, True)
                        calculate()
                    End If
                    'Case ConstTaxAmt
                    '    If chgAmt Then
                    '        calculateStockTotal()
                    '    End If
                    'Case ConstTaxP
                    '    If chgAmt Then
                    '        calculateStockTotal()
                    '    End If
                Case Else
            End Select
        End With
    End Sub
    Private Sub calculate()
        If chgbyprg Then Exit Sub
        Dim totQty As Double
        Dim totTax As Double
        Dim totAmt As Double
        chgbyprg = True
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
nxt:
            Next
        End With
        txttotalItemAmt.Text = Format(totAmt, numFormat)
        lblitmcost.Text = Format(totAmt, numFormat)
        chgbyprg = False
    End Sub
    Private Sub AddStockDetails(ByVal DR As DataTable)
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
            .Item(ConstPFraction, i).Value = IIf(IsDBNull(DR(0)("FraCount")), "2", DR(0)("FraCount"))
            .Item(ConstUnit, i).Value = DR(0)("Unit")
            If CDbl(.Item(ConstUPrice, i).Value) = 0 Or ischgItm Then
                If Val(DR(0)("costAvg") & "") > 0 Then
                    .Item(ConstActualPrice, i).Value = Val(DR(0)("costAvg"))
                Else
                    .Item(ConstActualPrice, i).Value = IIf(Val(DR(0)("lastpurchcost") & "") = 0, DR(0)("opcost"), DR(0)("lastpurchcost"))
                End If

                .Item(ConstUPrice, i).Value = Format(CDbl(.Item(ConstActualPrice, i).Value), numFormat)
            End If
            'If EnableGST Then
            '    getGSTDetails(Trim(.Item(ConstBarcode, i).Value & ""), i, False, True)
            'End If
            .Item(ConstLTotal, i).Value = Format(.Item(ConstQty, i).Value * CDbl(.Item(ConstUPrice, i).Value), numFormat)
            .Item(ConstQty, i).Value = Format(CDbl(.Item(ConstQty, i).Value), "#,##0" & IIf(Val(.Item(ConstPFraction, i).Value) = 0, "", "." & Strings.StrDup(CByte(.Item(ConstPFraction, i).Value), "0")))
            chgAmt = True
            ischgItm = False
            .ClearSelection()
        End With
        chgbyprg = False
        calculate()

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
                    'calculateTaxFromUnitPrice(e.RowIndex)
            End Select
        End With
        btnupdate.Enabled = False
    End Sub

    Private Sub grdVoucher_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdVoucher.EditingControlShowing
        Dim Col As Integer
        Col = grdVoucher.CurrentCell.ColumnIndex
        If Col = ConstItemCode Or Col = ConstQty Then
            Dim tb As TextBox = CType(e.Control, TextBox)
            RemoveHandler tb.TextChanged, AddressOf TextboxStock_TextChanged
            AddHandler tb.TextChanged, AddressOf TextboxStock_TextChanged
        End If
        If Col = ConstItemCode Or Col = ConstQty Then
            Dim tb As TextBox = CType(e.Control, TextBox)
            RemoveHandler tb.KeyPress, AddressOf TextboxStock_KeyPress
            AddHandler tb.KeyPress, AddressOf TextboxStock_KeyPress
        End If
    End Sub
    Private Sub TextboxStock_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            Dim col As Integer
            col = grdVoucher.CurrentCell.ColumnIndex
            If col = ConstQty Or col = ConstLTotal Then
                If Not IsNumeric(e.KeyChar) And e.KeyChar <> "." And Not e.KeyChar = Convert.ToChar(Keys.Back) Then e.Handled = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub TextboxStock_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)

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
        SearchProductPanel(grdSrch, "Item Code", strGridSrchString, True)
        doSelect(2)
        _srchOnce = True
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

    Private Sub grdVoucher_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.GotFocus
        activecontrolname = "grdVoucher"
    End Sub

    Private Sub grdVoucher_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdVoucher.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If grdVoucher.RowCount = 0 Then Exit Sub
                If SrchText = "" And grdVoucher.CurrentCell.ColumnIndex = ConstItemCode Then
                    SrchText = grdVoucher.Item(ConstItemCode, grdVoucher.CurrentRow.Index).Value
                    If SrchText = "" Then GoTo nxt
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
                _srchTxtId = 2
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
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub grdVoucher_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.Leave
        activecontrolname = ""
    End Sub
    Private Sub fProductEnquiry_getSelected(ByVal ItemcODE As String, ByVal isacc As Integer) Handles fProductEnquiry.getSelected
        chgbyprg = True
        fProductEnquiry.Close()
        SrchText = ItemcODE
        If _srchTxtId = 1 Then
            grdVoucher.Item(ConstItemCode, grdVoucher.CurrentRow.Index).Value = ItemcODE
            ischgItm = True
            ValidStockEntry(grdVoucher.CurrentRow.Index, ConstItemCode)
            grdVoucher.CurrentCell = grdVoucher.Item(3, grdVoucher.CurrentRow.Index)
            grdBeginEdit()

        End If
        chgbyprg = False
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


    Private Sub cmbtype_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbtype.SelectedIndexChanged
        cmbdatewise.Items.Clear()
        Select Case cmbtype.SelectedIndex
            Case 0
                cmbdatewise.Items.Add("None")
                cmbdatewise.Items.Add("Job Date")
            Case Else
                cmbdatewise.Items.Add("Job Date")
                cmbdatewise.Items.Add("Closed Date")
        End Select
    End Sub

    Private Sub txtSeq_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeq.TextChanged
        Dim dt As DataTable = SearchGrid(_vtable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
        grdItem.DataSource = dt
        gridTotal(dt)
    End Sub


    Private Sub grdSrch_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSrch.CellDoubleClick
        If _srchTxtId = 1 Then
            activecontrolname = "grdVoucher"
            doSelect(2)
            'chgbyprg = True
            With grdVoucher
                If .CurrentCell.ColumnIndex = ConstItemCode Then
                    .CurrentCell = .Item(1, .CurrentRow.Index)
                    ischgItm = True
                    ValidStockEntry(.CurrentCell.RowIndex, .CurrentCell.ColumnIndex)
                    chgbyprg = True
                    ischgItm = False
                    .BeginEdit(True)
                    chgbyprg = False
                End If
            End With
            plsrch.Visible = False
            chgbyprg = False
        End If
    End Sub

    Private Sub TabPage10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage10.Click

    End Sub

    Private Sub picCloseProd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picCloseProd.Click
        plsrch.Visible = False
    End Sub

    Private Sub lblprofit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblprofit.Click

    End Sub

    Private Sub GroupBox2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub GroupBox4_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox4.Enter

    End Sub
End Class