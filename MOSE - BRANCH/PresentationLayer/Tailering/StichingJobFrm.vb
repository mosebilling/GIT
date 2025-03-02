Imports System.IO

Public Class StichingJobFrm
#Region "Const Variables"
    Private Const lConstSlNo = 0
    Private Const lConstItemCode = 1
    Public Const lConstHsnCode = 2 'HSN Code
    Private Const lConstDescr = 3
    Private Const lConstJobDescr = 4
    Private Const lConstUnit = 5
    Private Const lConstQty = 6
    Private Const lConstUPrice = 7
    Private Const lconstItmTot = 8
    Private Const lConstTaxP = 9
    Private Const lConstTaxAmt = 10
    Private Const lConstLTotal = 11
    Private Const lConstItemID = 12
    Private Const lConstId = 13
    Private Const lConstCGSTP = 14
    Private Const lConstCGSTAmt = 15
    Private Const lConstSGSTP = 16
    Private Const lConstSGSTAmt = 17
    Private Const lConstIGSTP = 18
    Private Const lConstIGSTAmt = 19
    Private Const lConstAttend = 20
    Private Const lConstFinished = 21
    Private Const lConstPFraction = 22
    Private Const lConstActualPrice = 23
    Private Const lConstunitDiscount = 24

    Private Const mSlno = 0
    Private Const mMeasurement = 1
    Private Const mValue = 2
    Private Const mMeasurementid = 3
#End Region
#Region "Local Variables"
    Private chgbyprg As Boolean
    Private _srchTxtId As Integer
    Private _srchOnce As Boolean
    Private _srchIndexId As Byte
    Private dtm As DataTable
    Private MyActiveControl As New Object
    Private lstKey As Integer
    Private activecontrolname As String
    Private ischgItm As Boolean
    Private strGridSrchString As String
    Private SrchText As String
    Private chgAmt As Boolean
    Private chgUprice As Boolean
    Private _vtable As DataTable
    Private TrTypeNo As Integer
    Private stockdebit As Integer
    Private stockcredit As Integer
    Private printjobcard As Boolean
    Private dtTax As DataTable
    Private chgNumByPgm As Boolean
    Private _dtRptTable As DataTable
    Private hidecost As Integer
#End Region
#Region "Class Objects"
    Private _objJob As clsJob
    Private _objcmnbLayer As clsCommon_BL
    Private _objInv As New clsInvoice
    Private _objTr As New clsAccountTransaction
#End Region
#Region "Form Objects"
    Private WithEvents fMList As Mlistfrm
    Private WithEvents fRptFormat As RptFormatfrm
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fDelivery As New JobDelivery
    Private WithEvents fInvoice As JobSalesInvoice
    Private WithEvents fProductEnquiry As ItmEnqry
    Private WithEvents fcustomer As CreateAccNew
#End Region
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub SetGridHead()
        chgbyprg = True
        With grditems

            SetEntryGridProperty(grditems)
            .ColumnCount = 25
            .ColumnHeadersHeight = 100
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersDefaultCellStyle.BackColor = Color.Olive
            .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.5!, System.Drawing.FontStyle.Regular)
            .RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.0!)
            '.ReadOnly = True

            .Columns(lConstSlNo).HeaderText = "SlNo"
            .Columns(lConstSlNo).Width = 40
            .Columns(lConstSlNo).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(lConstSlNo).Frozen = True
            .Columns(lConstSlNo).DefaultCellStyle.Format = "N0"
            .Columns(lConstSlNo).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '.Columns(ConstSlNo).ReadOnly = True

            .Columns(lConstItemCode).HeaderText = "ItemCode"
            .Columns(lConstItemCode).Width = 100
            .Columns(lConstItemCode).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(ConstItemCode).ReadOnly = True

            .Columns(lConstHsnCode).HeaderText = "HSN Code"
            .Columns(lConstHsnCode).Width = 100
            .Columns(lConstHsnCode).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(lConstHsnCode).ReadOnly = True
            .Columns(lConstHsnCode).Visible = EnableGST

            .Columns(lConstDescr).HeaderText = "Item Name"
            .Columns(lConstDescr).Width = 220
            .Columns(lConstDescr).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(lConstHsnCode).ReadOnly = True

            .Columns(lConstJobDescr).HeaderText = "Job Description"
            .Columns(lConstJobDescr).Width = 220
            .Columns(lConstJobDescr).SortMode = DataGridViewColumnSortMode.NotSortable

            .Columns(lConstUnit).HeaderText = "Unit"
            .Columns(lConstUnit).Width = 40
            .Columns(lConstUnit).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(lConstUnit).Visible = False
            .Columns(lConstUnit).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns(lConstQty).HeaderText = "Qty"
            .Columns(lConstQty).Width = 50
            .Columns(lConstQty).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(ConstQty).Resizable = DataGridViewTriState.False
            .Columns(lConstQty).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(lConstQty).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstQty).DefaultCellStyle.BackColor = Color.LightGreen
            '.Columns(ConstQty).ReadOnly = False

            .Columns(lConstUPrice).HeaderText = "Unit Price"
            .Columns(lConstUPrice).Width = 70
            .Columns(lConstUPrice).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(lConstUPrice).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(lConstUPrice).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(lConstUPrice).DefaultCellStyle.BackColor = Color.LightCyan
            '.Columns(ConstUPrice).ReadOnly = False

            .Columns(lconstItmTot).HeaderText = "Item Total"
            .Columns(lconstItmTot).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(lconstItmTot).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(lconstItmTot).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(lconstItmTot).ReadOnly = True
            .Columns(lconstItmTot).Visible = True


            .Columns(lConstTaxP).HeaderText = "Tax%"
            .Columns(lConstTaxP).Width = 50
            .Columns(lConstTaxP).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(lConstTaxP).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(lConstTaxP).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(lConstTaxP).ReadOnly = True

            .Columns(lConstTaxAmt).HeaderText = "Tax Amt"
            .Columns(lConstTaxAmt).Width = 70
            .Columns(lConstTaxAmt).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(lConstTaxAmt).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(lConstTaxAmt).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(lConstTaxAmt).ReadOnly = True

            .Columns(lConstLTotal).HeaderText = "Line Total"
            .Columns(lConstLTotal).Width = 80
            .Columns(lConstLTotal).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(ConstLTotal).Resizable = DataGridViewTriState.False
            .Columns(lConstLTotal).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(lConstLTotal).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(lConstLTotal).DefaultCellStyle.BackColor = Color.LightGreen
            .Columns(lConstLTotal).ReadOnly = True

            .Columns(lConstItemID).HeaderText = "ItemID"
            .Columns(lConstItemID).Visible = False
            .Columns(lConstItemID).ReadOnly = True


            .Columns(lConstId).HeaderText = "id"
            .Columns(lConstId).Visible = False
            .Columns(lConstId).ReadOnly = True

            .Columns(lConstCGSTP).HeaderText = "CGST %"
            .Columns(lConstCGSTP).Width = 50
            .Columns(lConstCGSTP).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(lConstCGSTP).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(lConstCGSTP).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(lConstCGSTP).ReadOnly = True
            .Columns(lConstCGSTP).Visible = False

            .Columns(lConstCGSTAmt).HeaderText = "CGST Amt"
            .Columns(lConstCGSTAmt).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(lConstCGSTAmt).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(lConstCGSTAmt).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(lConstCGSTAmt).ReadOnly = True
            .Columns(lConstCGSTAmt).Visible = False

            .Columns(lConstSGSTP).HeaderText = "SGST %"
            .Columns(lConstSGSTP).Width = 50
            .Columns(lConstSGSTP).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(lConstSGSTP).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(lConstSGSTP).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(lConstSGSTP).ReadOnly = True
            .Columns(lConstSGSTP).Visible = False

            .Columns(lConstSGSTAmt).HeaderText = "SGST Amt"
            .Columns(lConstSGSTAmt).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(lConstSGSTAmt).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(lConstSGSTAmt).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(lConstSGSTAmt).ReadOnly = True
            .Columns(lConstSGSTAmt).Visible = False

            .Columns(lConstIGSTP).HeaderText = "IGST %"
            .Columns(lConstIGSTP).Width = 50
            .Columns(lConstIGSTP).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(lConstIGSTP).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(lConstIGSTP).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(lConstIGSTP).ReadOnly = True
            .Columns(lConstIGSTP).Visible = False

            .Columns(lConstIGSTAmt).HeaderText = "IGST Amt"
            .Columns(lConstIGSTAmt).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(lConstIGSTAmt).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(lConstIGSTAmt).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(lConstIGSTAmt).ReadOnly = True
            .Columns(lConstIGSTAmt).Visible = False

            .Columns(lConstAttend).HeaderText = "Attended By"
            .Columns(lConstAttend).Width = 120
            .Columns(lConstAttend).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(lConstAttend).ReadOnly = True
            .Columns(lConstAttend).Visible = False

            .Columns(lConstFinished).HeaderText = "Finished"
            .Columns(lConstFinished).Width = 100
            .Columns(lConstFinished).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(lConstFinished).ReadOnly = True
            .Columns(lConstFinished).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(lConstFinished).Visible = False

            .Columns(lConstPFraction).Visible = False
            .Columns(lConstActualPrice).Visible = False
            .Columns(lConstunitDiscount).Visible = False

        End With
        chgbyprg = False
    End Sub
    Private Sub setGridHeadMeasurement()
        chgbyprg = True
        With grdmeasurement
            SetEntryGridProperty(grdmeasurement)
            .ColumnCount = 4
            .Columns(mSlno).HeaderText = "SlNo"
            .Columns(mSlno).Width = 40
            .Columns(mSlno).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(mSlno).DefaultCellStyle.Format = "N0"
            .Columns(mSlno).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(mSlno).ReadOnly = True

            .Columns(mMeasurement).HeaderText = "Measurement"
            .Columns(mMeasurement).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(mMeasurement).ReadOnly = True

            .Columns(mValue).HeaderText = "Value"
            .Columns(mValue).SortMode = DataGridViewColumnSortMode.NotSortable

            .Columns(mMeasurementid).Visible = False
        End With
        chgbyprg = False
    End Sub

    Private Sub StichingJobFrm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtcustomer.Focus()
    End Sub
    Private Sub StichingJobFrm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If userType Then
            hidecost = IIf(getRight(253, CurrentUser), 1, 0)
            If hidecost = 0 Then
                Label19.Visible = False
                lblitemcost.Visible = False
                Label30.Visible = False
                lblprofit.Visible = False
                Label5.Visible = False
                lblserviceGridTotal.Visible = False
                Label15.Visible = False
                lblJobvalue.Visible = False
                GroupBox2.Height = 66
                Label16.Top = Label19.Top
                txtserviceCharge.Top = lblitemcost.Top
            End If
        Else
            hidecost = 1
        End If
        btnupdate.Tag = 1
        SetGridHead()
        setGridHeadMeasurement()
        SetGridHeadItems()
        Timer1.Enabled = True
        AddNew()
        TrTypeNo = getVouchernumber("STO")
        stockdebit = getConstantAccounts(18)
        stockcredit = getConstantAccounts(1)
        cmbreceipttype.SelectedIndex = 0
        cmbtype.SelectedIndex = 3
        CreateTaxTable(dtTax)

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
    Private Sub SetGridHeadItems()
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
            .Columns(ConstUPrice).Visible = IIf(hidecost = 1, True, False)

            .Columns(constItmTot).HeaderText = "Item Total"
            .Columns(constItmTot).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(constItmTot).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(constItmTot).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(constItmTot).ReadOnly = True
            .Columns(constItmTot).Visible = IIf(hidecost = 1, True, False)


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
            .Columns(ConstLTotal).Visible = IIf(hidecost = 1, True, False)

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

        End With
        chgbyprg = False
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        resizeGridColumn(grdVoucher, ConstDescr)
        'resizeGridColumn(grditems, lConstDescr)
        resizeGridColumn(grdmeasurement, mMeasurement)
    End Sub

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        'fMainForm.QuickCust(, "Customer")
        fcustomer = New CreateAccNew
        With fcustomer
            .Condition = "GrpSetOn In ('Customer')"
            .iscust = True
            .bOnlyOne = True
            .ShowDialog()
        End With
        fcustomer = Nothing
    End Sub

    Private Sub txtcustomer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtcustomer.KeyDown
        If e.KeyCode = Keys.Return Then
            txtphone.Focus()
        End If
    End Sub

    Private Sub txtcustomer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcustomer.TextChanged
        If chgbyprg = True Then Exit Sub
        Dim myCtrl As Control = sender
        Select Case myCtrl.Name
            Case "txtcustomer"
                _srchTxtId = 1
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

    Private Sub txtcustomer_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcustomer.Validated
        setcustomer(0)
    End Sub
    Private Sub setcustomer(ByVal accid As Long)
        If txtcustomer.Text = "" And accid = 0 Then Exit Sub
        Dim dt As DataTable
        _objcmnbLayer = New clsCommon_BL
        If accid = 0 Then
            dt = _objcmnbLayer._fldDatatable("SELECT accid,Alias,AccDescr,Address1,Address2,Address3,Address4,Phone,Fax,EMail,PT1,PT2,PT3,PT4,TrdLcno,TrdDate,ContactName,CountryCode from AccMast LEFT JOIN AccMastAddr ON AccMast.ACCID=AccMastAddr.accountno where AccDescr='" & txtcustomer.Text & "'")
        Else
            dt = _objcmnbLayer._fldDatatable("SELECT accid,Alias,AccDescr,Address1,Address2,Address3,Address4,Phone,Fax,EMail,PT1,PT2,PT3,PT4,TrdLcno,TrdDate,ContactName,CountryCode from AccMast LEFT JOIN AccMastAddr ON AccMast.ACCID=AccMastAddr.accountno where accid=" & accid)
        End If
        If dt.Rows.Count > 0 Then
            chgbyprg = True
            txtcustomer.Tag = dt(0)("accid")
            txtcustomer.Text = dt(0)("AccDescr")
            txtaddress.Text = dt(0)("Address1") & vbCrLf & dt(0)("Address2") & vbCrLf & dt(0)("Address3") & vbCrLf & "Phone : " & dt(0)("Phone") & vbCrLf & dt(0)("ContactName")
            txtphone.Text = dt(0)("Phone")
            chgbyprg = False
            'lblstatecode.Text = ("State Code : " & dt(0)("CountryCode"))
            'If ("" & dt(0)("CountryCode")) = "" Then
            '    lblstatecode.Tag = ""
            'Else
            '    If Trim(dt(0)("CountryCode") & "") <> Trim(stateCode & "") Then
            '        lblstatecode.Tag = 1
            '    Else
            '        lblstatecode.Tag = ""
            '    End If

            'End If
        Else
            txtcustomer.Text = ""
        End If
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
    End Sub

    Private Sub btnitmadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnitmadd.Click
        AddRow()
    End Sub
    Private Sub filtermeasurementDT(ByVal rindex As Integer, ByVal skipadd As Boolean)
        If dtm Is Nothing Then
            CreatemMeasurementTable()
        End If
        If rindex < 0 Then Exit Sub
        Dim bDatatable As DataTable
        If dtm.Rows.Count = 0 Then bDatatable = dtm.Clone : GoTo nxt
        Dim _qurey As EnumerableRowCollection(Of DataRow)
        _qurey = From data In dtm.AsEnumerable() Where data("slno") <> rindex Select data
        If _qurey.Count > 0 Then
            dtm = _qurey.CopyToDataTable()
        Else
            If grdmeasurement.Rows.Count > 0 Then
                dtm = dtm.Clone
            End If

        End If
        If skipadd Then Exit Sub
nxt:
        Dim i As Integer
        With grdmeasurement
            'If .CurrentRow Is Nothing Then Exit Sub
            'If .RowCount > 0 And .CurrentRow.Index = rindex Then
            '    Exit Sub
            'End If
            Dim dtrow As DataRow
            For i = 0 To .RowCount - 1
                dtrow = dtm.NewRow
                dtrow("slno") = rindex
                dtrow("measurementname") = .Item(mMeasurement, i).Value
                dtrow("mvalue") = .Item(mValue, i).Value
                dtrow("measurementid") = Val(.Item(mMeasurementid, i).Value)
                dtm.Rows.Add(dtrow)
            Next
        End With
    End Sub
    Public Sub AddRow(Optional ByVal tocheck As Boolean = False)
        'chgbyprg = True
        If dtm Is Nothing Then CreatemMeasurementTable()
        Dim i As Integer
        With grditems
            filtermeasurementDT(Val(grditems.Tag), False)
            activecontrolname = "grditems"
            i = .RowCount '- 1
            .Rows.Add(1)
            .Item(lConstSlNo, i).Value = i + 1
            .Item(lConstItemCode, i).Value = ""
            .Item(lConstDescr, i).Value = ""
            .Item(lConstJobDescr, i).Value = ""
            .Item(lConstUnit, i).Value = ""
            .Item(lConstQty, i).Value = Format(0, numFormat)
            .Item(lConstUPrice, i).Value = Format(0, numFormat)
            .Item(lConstTaxP, i).Value = Format(0, numFormat)
            .Item(lConstTaxAmt, i).Value = Format(0, numFormat)
            .Item(lConstLTotal, i).Value = Format(0, numFormat)
            .Item(lConstItemID, i).Value = 0
            .Item(lConstPFraction, i).Value = "2"
            .CurrentCell = .Item(lConstItemCode, i)
            chgbyprg = True
            .BeginEdit(True)
            chgbyprg = False
            If grdmeasurement.Rows.Count > 0 Then grdmeasurement.Rows.Clear()
            picImage.Image = Nothing
            grditems.Tag = i
        End With
        calculate()
        reArrangeNo()
    End Sub
    Private Sub RemoveRow()
        If grditems.RowCount > 0 Then
            If MsgBox("You Are Going to Remove the Row." & vbCrLf & "Are You Sure ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then
                Exit Sub
            End If
            Dim r As Integer
            With grditems
                r = .CurrentRow.Index
                .Rows.RemoveAt(.CurrentRow.Index)
                .ClearSelection()
                'calculate()
            End With
            filtermeasurementDT(Val(grditems.Tag), True)
            Dim i As Integer
            For i = 0 To dtm.Rows.Count - 1
                If dtm(i)("slno") > r Then
                    dtm(i)("slno") = Val(dtm(i)("slno")) - 1
                End If
            Next
            reArrangeNo()
        End If
        calculate()
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
    Private Sub reArrangeMeasurement()
        Dim r As Integer
        Dim i As Integer
        chgbyprg = True
        i = 0
        With grdmeasurement
            For r = 0 To .Rows.Count - 1 '- 1
                i = i + 1
                .Item(mSlno, r).Value = i
            Next r
        End With
        chgbyprg = False
    End Sub
    Public Sub CreatemMeasurementTable()
        dtm = New DataTable
        dtm.Columns.Add(New DataColumn("slno", GetType(Integer)))
        dtm.Columns.Add(New DataColumn("measurementname", GetType(String)))
        dtm.Columns.Add(New DataColumn("mvalue", GetType(String)))
        dtm.Columns.Add(New DataColumn("measurementid", GetType(Integer)))
    End Sub

    Private Sub grditems_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grditems.CellClick
        'grditems.Tag = e.RowIndex
        chgbyprg = True
        With grditems
            If Val(.Item(0, .CurrentCell.RowIndex).Value) = 0 And e.ColumnIndex <> 3 And e.ColumnIndex <> 21 Then
                .CurrentCell.ReadOnly = True
            ElseIf e.ColumnIndex <> lConstSlNo And e.ColumnIndex <> lconstItmTot And e.ColumnIndex <> lConstUnit And e.ColumnIndex <> lConstLTotal And e.ColumnIndex <> lConstHsnCode And e.ColumnIndex <> lConstTaxAmt And e.ColumnIndex <> lConstFinished Then
                If e.ColumnIndex = lConstTaxP And EnableGST Then
                    .CurrentCell.ReadOnly = True
                Else
                    .CurrentCell.ReadOnly = False
                End If
            Else
                .CurrentCell.ReadOnly = True
            End If
            chgbyprg = True
            .BeginEdit(True)
            chgbyprg = False
        End With

    End Sub

    Private Sub grditems_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grditems.RowEnter
        If chgbyprg Then Exit Sub
        filtermeasurementDT(Val(grditems.Tag), False)
        If grditems.CurrentRow Is Nothing Then Exit Sub
        grditems.Tag = e.RowIndex
        getmeasurementFromDatatable(Val(grditems.Tag))
        ldImage(grditems.Item(lConstItemID, e.RowIndex).Value)
    End Sub

    Private Sub btnrem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrem.Click
        RemoveRow()
    End Sub

    Private Sub grditems_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grditems.CellContentClick

    End Sub

    Private Sub grditems_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grditems.CellEndEdit
        Dim col As Integer = e.ColumnIndex
        Dim ndec1 As Integer
        With grditems
            If col = lConstQty Or col = lConstUPrice Or col = lConstLTotal Or col = lConstTaxP Then
                ndec1 = 2
                If Val(.Item(col, e.RowIndex).Value) = 0 Then .Item(col, e.RowIndex).Value = 0
                .Item(col, e.RowIndex).Value = Format(Convert.ToDecimal(.Item(col, e.RowIndex).Value), "#,##0" & IIf(ndec1 = 0, "", "." & Strings.StrDup(ndec1, "0")))
            End If
        End With

    End Sub

    Private Sub grditems_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grditems.CellValidated
        Valid(e.RowIndex, e.ColumnIndex)
    End Sub
    Private Sub Valid(ByVal RowIndex As Integer, ByVal ColIndex As Integer)
        With grditems
            Select Case ColIndex
                Case lConstHsnCode, lConstItemCode
                    If Not ischgItm Then Exit Sub
                    If Trim(.Item(ColIndex, RowIndex).Value) = "" Then Exit Sub
                    Dim dtItms As DataTable
                    Dim found As Boolean
                    If SrchText = "" Then Exit Sub
                    dtItms = getItmDtls(3, SrchText, True)
                    If dtItms.Rows.Count > 0 Then
                        found = True
                        filtermeasurementDT(RowIndex, True)
                        AddDetails(dtItms)
                    End If
                    ischgItm = False
                    If Not found Then
                        .Item(lConstHsnCode, RowIndex).Value = ""
                        .Item(lConstItemCode, RowIndex).Value = ""
                        '.Item(lConstBaseID, RowIndex).Value = ""
                        .Item(lConstItemID, RowIndex).Value = ""
                        .Item(lConstUnit, RowIndex).Value = ""
                        '.Item(lConstSerialNo, RowIndex).Value = ""
                        '.Item(lConstPMult, RowIndex).Value = "1"
                        .Item(lConstPFraction, RowIndex).Value = "2"
                        '.Item(lConstImpDocId, RowIndex).Value = ""
                        '.Item(lConstImpLnId, RowIndex).Value = ""
                        ischgItm = False
                    End If
                Case lConstQty
                    If chgAmt Then
                        If EnableGST Then getGSTDetails(Trim(.Item(lConstHsnCode, RowIndex).Value & ""), RowIndex, True, False)
                        calculate()
                    End If
                Case lConstUPrice
                    If chgAmt Then
                        .Item(lConstActualPrice, RowIndex).Value = CDbl(.Item(lConstUPrice, RowIndex).Value)
                        If EnableGST Then getGSTDetails(Trim(.Item(lConstHsnCode, RowIndex).Value & ""), RowIndex, True, False)
                        'chgbyprg = False
                        calculate()
                    End If
                Case lConstTaxAmt
                    If chgAmt Then
                        calculate()
                    End If
                Case lConstTaxP
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
        With grditems
            chgbyprg = True
            PMult = 1
            i = .CurrentRow.Index ' .RowCount - 1
            .Item(lConstSlNo, i).Value = i + 1
            .Item(lConstItemCode, i).Value = IIf(IsDBNull(DR(0)("Item Code")), Trim(DR(0)("Item Code") & ""), DR(0)("Item Code"))
            .Item(lConstDescr, i).Value = DR(0)("Description")
            If EnableGST Then
                .Item(lConstHsnCode, i).Value = DR(0)("HSNCode") 'hsncode
            Else
                .Item(lConstHsnCode, i).Value = ""
            End If
            .Item(lConstQty, i).Value = Format(1, numFormat) 'IIf(IsReturn, -1, 1)
            .Item(lConstItemID, i).Value = DR(0)("ItemId")
            .Item(lConstPFraction, i).Value = IIf(IsDBNull(DR(0)("FraCount")), "2", DR(0)("FraCount"))
            .Item(lConstUnit, i).Value = DR(0)("Unit")
            If CDbl(.Item(lConstUPrice, i).Value) = 0 Or ischgItm Then
                .Item(lConstActualPrice, i).Value = DR(0)("UnitPrice")
                .Item(lConstUPrice, i).Value = Format(CDbl(.Item(lConstActualPrice, i).Value), numFormat)
            End If
            If EnableGST Then
                getGSTDetails(Trim(.Item(lConstHsnCode, i).Value & ""), i, False, False)
            End If
            .Item(lConstLTotal, i).Value = Format(.Item(lConstQty, i).Value * CDbl(.Item(lConstUPrice, i).Value), numFormat)
            .Item(lConstQty, i).Value = Format(CDbl(.Item(lConstQty, i).Value), "#,##0" & IIf(Val(.Item(lConstPFraction, i).Value) = 0, "", "." & Strings.StrDup(CByte(.Item(lConstPFraction, i).Value), "0")))
            chgAmt = True
            ischgItm = False
            .ClearSelection()
            If grdmeasurement.Rows.Count > 0 Then grdmeasurement.Rows.Clear()
            loadItemMeasurement(i)
            ldImage(.Item(lConstItemID, i).Value)
        End With
        chgbyprg = False
        calculate()

    End Sub
    Private Sub loadItemMeasurement(ByVal rindex As Integer)
        Dim dt As DataTable = _objcmnbLayer._fldDatatable("select measurementname,measurementid from StichingMeasurementTb where mitemid=" & Val(grditems.Item(lConstItemID, rindex).Value))
        Dim i As Integer
        'If grdmeasurement.Rows.Count > 0 Then grdmeasurement.Rows.Clear()
        Dim r As Integer
        Dim found As Boolean = False
        With grdmeasurement
            For i = 0 To dt.Rows.Count - 1
                found = False
                For r = 0 To grdmeasurement.Rows.Count - 1
                    If dt(i)("measurementname") = grdmeasurement.Item(mMeasurement, r).Value Then
                        found = True
                        Exit For
                    End If
                Next
                If Not found Then
                    .Rows.Add()
                    .Item(mMeasurement, .Rows.Count - 1).Value = dt(i)("measurementname")
                    .Item(mMeasurementid, .Rows.Count - 1).Value = dt(i)("measurementid")
                End If
            Next

        End With
        reArrangeMeasurement()
    End Sub
    Private Sub getmeasurementFromDatatable(ByVal rindex As Integer)
        If rindex < 0 Then Exit Sub
        Dim bDatatable As DataTable
        If dtm.Rows.Count = 0 Then bDatatable = dtm.Clone : GoTo nxt
        Dim _qurey As EnumerableRowCollection(Of DataRow)
        _qurey = From data In dtm.AsEnumerable() Where data("slno") = rindex Select data
        If _qurey.Count > 0 Then
            bDatatable = _qurey.CopyToDataTable()
        Else
            bDatatable = dtm.Clone
        End If
nxt:
        If grdmeasurement.Rows.Count > 0 Then grdmeasurement.Rows.Clear()
        If bDatatable.Rows.Count > 0 Then
            For i = 0 To bDatatable.Rows.Count - 1
                With grdmeasurement
                    .Rows.Add()
                    .Item(mSlno, .Rows.Count - 1).Value = bDatatable(i)("slno")
                    .Item(mMeasurement, .Rows.Count - 1).Value = bDatatable(i)("measurementname")
                    .Item(mValue, .Rows.Count - 1).Value = bDatatable(i)("mvalue")
                    .Item(mMeasurementid, .Rows.Count - 1).Value = bDatatable(i)("measurementid")
                End With
            Next
            loadItemMeasurement(rindex)
            reArrangeMeasurement()
        Else
            loadItemMeasurement(rindex)
        End If

    End Sub
    Private Sub getGSTDetails(ByVal hsncode As String, ByVal i As Integer, ByVal calculatefromGrid As Boolean, ByVal istock As Boolean)
        Dim dt As DataTable
        If istock Then
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
                .Item(ConstTaxAmt, i).Value = Format(CDbl(.Item(ConstCGSTAmt, i).Value) + CDbl(.Item(ConstSGSTAmt, i).Value), numFormat)
                .Item(ConstTaxP, i).Value = CDbl(.Item(ConstCGSTP, i).Value) + CDbl(.Item(ConstSGSTP, i).Value)
            End With
        Else
            With grditems
                If Not calculatefromGrid Then
                    dt = _objcmnbLayer._fldDatatable("SELECT * FROM GSTTb WHERE HSNCODE='" & hsncode & "'")
                    If dt.Rows.Count > 0 Then
                        .Item(lConstCGSTP, i).Value = Format(IIf(IsDBNull(dt(0)("CGST")), 0, CDbl(dt(0)("CGST"))), numFormat)
                        .Item(lConstSGSTP, i).Value = Format(IIf(IsDBNull(dt(0)("SGST")), 0, CDbl(dt(0)("SGST"))), numFormat)
                        .Item(lConstIGSTP, i).Value = Format(IIf(IsDBNull(dt(0)("IGST")), 0, CDbl(dt(0)("IGST"))), numFormat)
                    Else
                        .Item(lConstCGSTP, i).Value = Format(0, numFormat)
                        .Item(lConstSGSTP, i).Value = Format(0, numFormat)
                        .Item(lConstIGSTP, i).Value = Format(0, numFormat)
                    End If
                End If
                Dim actualPrice As Double
                actualPrice = ((CDbl(.Item(lConstActualPrice, i).Value) - CDbl(.Item(lConstunitDiscount, i).Value)) * CDbl(.Item(lConstQty, i).Value))
                .Item(lConstCGSTAmt, i).Value = (actualPrice * .Item(lConstCGSTP, i).Value) / 100
                .Item(lConstSGSTAmt, i).Value = (actualPrice * .Item(lConstSGSTP, i).Value) / 100
                .Item(lConstIGSTAmt, i).Value = (actualPrice * .Item(lConstIGSTP, i).Value) / 100
                .Item(lConstTaxAmt, i).Value = Format(CDbl(.Item(lConstCGSTAmt, i).Value) + CDbl(.Item(lConstSGSTAmt, i).Value), numFormat)
                .Item(lConstTaxP, i).Value = CDbl(.Item(lConstCGSTP, i).Value) + CDbl(.Item(lConstSGSTP, i).Value)
            End With

        End If

    End Sub
    Private Sub calculateunitdiscount()
        Dim i As Integer
        Dim subtotal As Double
        If Val(numDisc.Text) = 0 Then numDisc.Text = 0
        Dim tDAmt As Double = CDbl(numDisc.Text)
        With grditems
            For i = 0 To .Rows.Count - 1
                If IsDBNull(.Item(lConstQty, i).Value) Then .Item(lConstQty, i).Value = 0
                If CStr(.Item(lConstQty, i).Value) = "" Then .Item(lConstQty, i).Value = 0
                subtotal = subtotal + (Val(.Item(lConstActualPrice, i).Value) * CDbl(.Item(lConstQty, i).Value))
            Next
            For i = 0 To .Rows.Count - 1
                Dim actualPrice As Double = .Item(lConstActualPrice, i).Value
                If subtotal = 0 Then
                    .Item(lConstunitDiscount, i).Value = 0
                Else
                    If enableAdjustDiscountOnTaxTotal Then
                        If tDAmt = 0 Then GoTo els
                        Dim discountOther As Double
                        discountOther = (tDAmt * actualPrice) / subtotal
                        Dim discountWithoutTax As Double
                        Dim ttax As Double
                        ttax = CDbl(.Item(lConstTaxP, i).Value)
                        discountWithoutTax = (discountOther * 100) / (ttax + 100)
                        .Item(lConstunitDiscount, i).Value = discountWithoutTax
                    Else
els:
                        .Item(lConstunitDiscount, i).Value = (tDAmt * actualPrice) / subtotal
                    End If

                End If
            Next
            
        End With
    End Sub

    Private Sub calculate()
        If chgbyprg Then Exit Sub
        Dim totQty As Double
        Dim totTax As Double
        Dim totAmt As Double
        Dim actualprice As Double
        Dim totItm As Double
        calculateunitdiscount()
        With grditems
            For i = 0 To .Rows.Count - 1
                If Val(.Item(lConstItemID, i).Value) = 0 Then GoTo nxt
                .Item(lConstSlNo, i).Value = i + 1
                totQty = totQty + IIf(Val(.Item(lConstQty, i).Value) > 0, Val(.Item(lConstQty, i).Value), 0)
                If Val(.Item(lConstTaxP, i).Value & "") = 0 Then
                    .Item(lConstTaxP, i).Value = 0
                End If
                If Val(.Item(lConstActualPrice, i).Value & "") = 0 Then
                    .Item(lConstActualPrice, i).Value = 0
                End If
                If Val(.Item(lConstunitDiscount, i).Value & "") = 0 Then
                    .Item(lConstunitDiscount, i).Value = 0
                End If
                actualprice = CDbl(.Item(lConstActualPrice, i).Value) - CDbl(.Item(lConstunitDiscount, i).Value)
                If Val(.Item(lConstQty, i).Value & "") = 0 Then
                    .Item(lConstQty, i).Value = 0
                End If
                .Item(lConstTaxAmt, i).Value = Format(((actualprice * .Item(lConstTaxP, i).Value) / 100) * CDbl(.Item(lConstQty, i).Value), numFormat)
                If EnableGST Then
                    If Val(.Item(lConstSGSTAmt, i).Value & "") = 0 Then .Item(lConstSGSTAmt, i).Value = 0
                    If Val(.Item(lConstCGSTAmt, i).Value & "") = 0 Then .Item(lConstCGSTAmt, i).Value = 0
                    .Item(lConstSGSTAmt, i).Value = Format(CDbl(.Item(lConstTaxAmt, i).Value) / 2, numFormat)
                    .Item(lConstCGSTAmt, i).Value = Format(CDbl(.Item(lConstTaxAmt, i).Value) / 2, numFormat)
                End If
                totTax = totTax + .Item(lConstTaxAmt, i).Value
                totItm = totItm + (CDbl(.Item(lConstQty, i).Value) * CDbl(.Item(lConstActualPrice, i).Value))
                totAmt = totAmt + (.Item(lConstQty, i).Value * (CDbl(.Item(lConstActualPrice, i).Value) - IIf(enableAdjustDiscountOnTaxTotal, CDbl(.Item(lConstunitDiscount, i).Value), 0)))
                .Item(lconstItmTot, i).Value = Format((.Item(lConstQty, i).Value * CDbl(.Item(lConstActualPrice, i).Value)), numFormat)
                .Item(lConstLTotal, i).Value = Format((.Item(lConstQty, i).Value * CDbl(.Item(lConstActualPrice, i).Value)) + CDbl(.Item(lConstTaxAmt, i).Value), numFormat)
nxt:
            Next
        End With
        lblTotAmt.Text = Format(totItm, numFormat)
        totAmt = totAmt + totTax
        'lbltotal.Text = Format(totAmt - CDbl(numDisc.Text) + totTax, numFormat)
        If enableAdjustDiscountOnTaxTotal Then
            lbltotal.Text = Format(totAmt, numFormat)
        Else
            lbltotal.Text = Format(totAmt - CDbl(numDisc.Text), numFormat)
        End If
        chgNumByPgm = True
        Dim retrnAmt As Double
        cmbsign.SelectedIndex = getroundoffAMT(lbltotal.Text, retrnAmt)
        txtroundOff.Text = Format(retrnAmt, numFormat)
        chgNumByPgm = False
        If Val(txtroundOff.Text) > 0 Then
            lbltotal.Text = Format(CDbl(lbltotal.Text) - (IIf(cmbsign.SelectedIndex = 1, 1, -1) * CDbl(txtroundOff.Text)), numFormat)
        End If
        lbltax.Text = Format(totTax, numFormat)
        lbljbamount.Text = lbltotal.Text
        lblqty.Text = Format(totQty, 0)
        If Val(txtserviceCharge.Text) = 0 Then txtserviceCharge.Text = 0
        If Val(lblitemcost.Text) = 0 Then lblitemcost.Text = 0
        lblJobvalue.Text = Format(CDbl(txtserviceCharge.Text) + CDbl(lblitemcost.Text), numFormat)
        If Val(lbljbamount.Text) = 0 Then lbljbamount.Text = Format(0, numFormat)
        If Val(lblRv.Text) = 0 Then lblRv.Text = Format(0, numFormat)
        lblbalance.Text = Format(CDbl(lbljbamount.Text) - CDbl(lblRv.Text), numFormat)

        lblprofit.Text = Format(CDbl(lbljbamount.Text) - CDbl(lblJobvalue.Text), numFormat)
    End Sub

    Private Sub grditems_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grditems.CellValueChanged
        If chgbyprg Then Exit Sub
        'ChgByPrg = True
        With grditems
            Dim i As Integer = e.RowIndex
            Select Case e.ColumnIndex
                Case lConstItemCode
                    ischgItm = True
                Case lConstQty, lConstTaxP
                    chgAmt = True
                Case lConstUPrice
                    chgUprice = True
                    chgAmt = True
                    calculateTaxFromUnitPrice(e.RowIndex)
            End Select
        End With
    End Sub
    Private Sub calculateTaxFromUnitPrice(ByVal i As Integer)
        If chgUprice And chkcal.Checked Then
            chgbyprg = True
            With grditems
                .Item(lConstActualPrice, i).Value = (CDbl(.Item(lConstUPrice, i).Value) * 100) / (CDbl(.Item(lConstTaxP, i).Value) + 100)
                .Item(lConstUPrice, i).Value = Format(.Item(lConstActualPrice, i).Value, numFormat)
            End With
        End If
        chgUprice = False
        chgbyprg = False
    End Sub

    Private Sub grditems_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grditems.EditingControlShowing
        Dim Col As Integer
        Col = grditems.CurrentCell.ColumnIndex
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
            col = grditems.CurrentCell.ColumnIndex
            If col = lConstQty Or col = lConstLTotal Then
                If Not IsNumeric(e.KeyChar) And e.KeyChar <> "." And Not e.KeyChar = Convert.ToChar(Keys.Back) Then e.Handled = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub Textbox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Try
            Dim col As Integer
            col = grditems.CurrentCell.ColumnIndex
            Dim MyCtrl As TextBox = sender
            If col = lConstItemCode Or lConstDescr Then strGridSrchString = MyCtrl.Text
            If chgbyprg Then Exit Sub
            _srchOnce = False
            If col = lConstItemCode Then
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
                    grditems.Item(ConstItemCode, grditems.CurrentCell.RowIndex).Value = IIf(ItmFlds(0) IsNot Nothing, ItmFlds(0), "")
                    'grdVoucher.Item(ConstBarcode, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(1) IsNot Nothing, ItmFlds(1), "")
                    grditems.Item(ConstDescr, grditems.CurrentCell.RowIndex).Value = IIf(ItmFlds(2) IsNot Nothing, ItmFlds(1), "")
                    SrchText = IIf(ItmFlds(0) IsNot Nothing, ItmFlds(0), strGridSrchString)
                Case 2
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

    Private Sub picCloseProd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picCloseProd.Click
        plsrch.Visible = False
    End Sub

    Private Sub grditems_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grditems.GotFocus
        activecontrolname = "grditems"
    End Sub

    Private Sub grditems_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grditems.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If grditems.RowCount = 0 Then Exit Sub
                If SrchText = "" And grditems.CurrentCell.ColumnIndex = ConstItemCode Then
                    GoTo nxt
                End If
                If FindNextCell(grditems, grditems.CurrentCell.RowIndex, grditems.CurrentCell.ColumnIndex + 1) Then
                    AddRow()
                End If
nxt:
                plsrch.Visible = False
                chgbyprg = True
                grditems.BeginEdit(True)
                chgbyprg = False

            ElseIf e.KeyCode = Keys.Up Or e.KeyCode = Keys.PageUp Then
                If grditems.RowCount = 0 Then Exit Sub
                If plsrch.Visible Then
                    doSelect(0)
                    Exit Sub
                End If
            ElseIf e.KeyCode = Keys.Down Or e.KeyCode = Keys.PageDown Then
                If grditems.RowCount = 0 Then Exit Sub
                If plsrch.Visible Then
                    doSelect(1)
                    Exit Sub
                End If
            ElseIf e.KeyCode = Keys.F2 Then
                If plsrch.Visible Then plsrch.Visible = False
                If grditems.RowCount = 0 Then Exit Sub
                _srchTxtId = 1
                Select Case grditems.CurrentCell.ColumnIndex
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

    Private Sub grditems_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles grditems.Leave
        activecontrolname = ""
    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            Const WM_KEYDOWN As Integer = &H100
            Const WM_KEYUP As Integer = &H101
            If msg.Msg = WM_KEYDOWN Or msg.Msg = WM_KEYUP Then
                If activecontrolname = "grditems" Then
                    If msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up) Or msg.WParam.ToInt32() = CInt(Keys.Enter) Or msg.WParam.ToInt32() = CInt(Keys.F2) Or msg.WParam.ToInt32() = CInt(Keys.F3) Or msg.WParam.ToInt32() = CInt(Keys.F4) Then
                        grditems_KeyDown(Nothing, New KeyEventArgs(keyData))
                        Return True
                    End If
                ElseIf activecontrolname = "grdVoucher" Then
                    If msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up) Or msg.WParam.ToInt32() = CInt(Keys.Enter) Or msg.WParam.ToInt32() = CInt(Keys.F2) Or msg.WParam.ToInt32() = CInt(Keys.F3) Or msg.WParam.ToInt32() = CInt(Keys.F4) Then
                        grdVoucher_KeyDown(Nothing, New KeyEventArgs(keyData))
                        Return True
                    End If
                ElseIf activecontrolname = "grdmeasurement" Then
                    If msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up) Or msg.WParam.ToInt32() = CInt(Keys.Enter) Or msg.WParam.ToInt32() = CInt(Keys.F2) Or msg.WParam.ToInt32() = CInt(Keys.F3) Or msg.WParam.ToInt32() = CInt(Keys.F4) Then
                        grdmeasurement_KeyDown(Nothing, New KeyEventArgs(keyData))
                        Return True
                    End If
                End If
            End If
ctn:
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function



    Private Sub fcustomer_OpenAccMaster(ByRef AccountNo As Long) Handles fcustomer.OpenAccMaster
        setcustomer(AccountNo)
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
                    dtItms = ItmValidation(3, SrchText, True, "IP", 0) 'getItmDtls(3, SrchText, True)
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
                        calculateStockTotal()
                    End If
                Case ConstUPrice
                    If chgAmt Then
                        .Item(ConstActualPrice, RowIndex).Value = CDbl(.Item(ConstUPrice, RowIndex).Value)
                        'If EnableGST Then getGSTDetails(Trim(.Item(ConstBarcode, RowIndex).Value & ""), RowIndex, True, True)
                        calculateStockTotal()
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
                If Val(DR(0)("costAvg")) > 0 Then
                    .Item(ConstActualPrice, i).Value = Val(DR(0)("costAvg"))
                Else
                    .Item(ConstActualPrice, i).Value = IIf(Val(DR(0)("lastPrice")) = 0, DR(0)("opcost"), DR(0)("lastPrice"))
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
        calculateStockTotal()

    End Sub
    Private Sub calculateStockTotal()
        If chgbyprg Then Exit Sub
        Dim totQty As Double
        Dim totTax As Double
        Dim totAmt As Double
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
        lblserviceGridTotal.Text = Format(totAmt, numFormat)
        lblitemcost.Text = Format(totAmt, numFormat)
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
                _srchTxtId = 2
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

    Private Sub grdVoucher_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.GotFocus
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
                    AddRowStock()
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
                AddRowStock()
            ElseIf e.KeyCode = Keys.F4 Then
                RemoveRowStock()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnaddservice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddservice.Click
        AddRowStock()
    End Sub
    Public Sub AddRowStock(Optional ByVal tocheck As Boolean = False)
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
        calculateStockTotal()
        reArrangeNoStock()
    End Sub
    Private Sub RemoveRowStock()
        If grdVoucher.RowCount > 0 Then
            If MsgBox("You Are Going to Remove the Row." & vbCrLf & "Are You Sure ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then
                Exit Sub
            End If
            With grdVoucher
                .Rows.RemoveAt(.CurrentRow.Index)
                .ClearSelection()
                calculateStockTotal()
            End With
            reArrangeNoStock()
        End If
        ischgItm = True
    End Sub
    Private Sub reArrangeNoStock()
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

    Private Sub btnremoveservice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnremoveservice.Click
        RemoveRowStock()
    End Sub

    Private Sub grdVoucher_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.Leave
        activecontrolname = ""
    End Sub
    Private Sub fProductEnquiry_getSelected(ByVal ItemcODE As String, ByVal isacc As Integer) Handles fProductEnquiry.getSelected
        chgbyprg = True
        fProductEnquiry.Close()
        SrchText = ItemcODE
        If _srchTxtId = 1 Then
            grditems.Item(lConstItemCode, grditems.CurrentRow.Index).Value = ItemcODE
            ischgItm = True
            Valid(grditems.CurrentRow.Index, ConstItemCode)
            grditems.CurrentCell = grditems.Item(3, grditems.CurrentRow.Index)
            grditems.BeginEdit(True)
        ElseIf _srchTxtId = 2 Then
            grdVoucher.Item(ConstItemCode, grdVoucher.CurrentRow.Index).Value = ItemcODE
            ischgItm = True
            ValidStockEntry(grdVoucher.CurrentRow.Index, ConstItemCode)
            grdVoucher.CurrentCell = grdVoucher.Item(3, grdVoucher.CurrentRow.Index)
            grdBeginEdit()

        End If
        chgbyprg = False
    End Sub

    Private Sub TabControl1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.Click
        If TabControl1.SelectedIndex = 1 Then
            resizeGridColumn(grdVoucher, ConstDescr)
        End If
    End Sub

    Private Sub grdmeasurement_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdmeasurement.CellClick
        With grdmeasurement
            chgbyprg = True
            .BeginEdit(True)
            chgbyprg = False
        End With
    End Sub

    Private Sub grdmeasurement_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdmeasurement.CellContentClick

    End Sub

    Private Sub grdmeasurement_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdmeasurement.GotFocus
        activecontrolname = "grdmeasurement"
    End Sub

    Private Sub grdmeasurement_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdmeasurement.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If grdmeasurement.RowCount = 0 Then Exit Sub
                If SrchText = "" And grdmeasurement.CurrentCell.ColumnIndex = ConstItemCode Then
                    GoTo nxt
                End If
                If FindNextCell(grdmeasurement, grdmeasurement.CurrentCell.RowIndex, grdmeasurement.CurrentCell.ColumnIndex + 1) Then
                    'AddRow()
                End If
nxt:
                chgbyprg = True
                grdmeasurement.BeginEdit(True)
                chgbyprg = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub grdmeasurement_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdmeasurement.Leave
        activecontrolname = ""
    End Sub
    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        verify()
    End Sub
    Private Sub verify()
        If Val(btnupdate.Tag) = 0 Then
            MsgBox("Current User does not have permission to update", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If btndelivery.Text = "Undo Delivery" Then
            MsgBox("Delivered job cannot be saved", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
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
        If txtphone.Text = "" Then
            MsgBox("Invalid Phone Number", MsgBoxStyle.Exclamation)
            txtphone.Focus()
            Exit Sub
        End If
        Dim i As Integer
        If Not grditems.CurrentRow Is Nothing And grdmeasurement.Rows.Count > 0 Then
            filtermeasurementDT(grditems.CurrentRow.Index, 0)
        End If
        For i = 0 To dtm.Rows.Count - 1
            If Trim(dtm(i)("mvalue") & "") = "" Then
                MsgBox("All measurement values required for row : " & Val(dtm(i)("slno") & "") + 1, MsgBoxStyle.Exclamation)
                Exit Sub
            End If
        Next
        If MsgBox("Verification is succeeded. Do you like to file it?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub


        saveJob()
        txtprintjob.Text = txtjobcode.Text
        MsgBox("Order and Invoice Saved Successfully", MsgBoxStyle.Information)
        grdmeasurement.Rows.Clear()
        ldRec(Val(txtjobcode.Tag))
        'AddNew()
        'makeClear()
        'If isModi Then Me.Close()
    End Sub
    Private Sub makeClear()
        chgbyprg = True
        txtcustomer.Text = ""
        txtcustomer.Tag = ""
        txtaddress.Text = ""
        lbljobstatus.Text = ""
        txtserviceCharge.Text = Format(0, numFormat)
        lblitemcost.Text = Format(0, numFormat)
        lblJobvalue.Text = Format(0, numFormat)
        lblitemcost.Text = Format(0, numFormat)
        lblJobvalue.Text = Format(0, numFormat)
        btndelivery.Text = "Delivery"
        numDisc.Text = Format(0, numFormat)
        txtdp.Text = Format(0, numFormat)
        grdVoucher.Rows.Clear()
        grditems.Rows.Clear()
        grdmeasurement.Rows.Clear()
        If dtm.Rows.Count > 0 Then dtm.Rows.Clear()
        'grdinvList.Rows.Clear()
        'grdrvlist.Rows.Clear()
        numsto.Text = ""
        numsto.Tag = ""
        txtjobcode.Tag = ""
        chgbyprg = False
        ischgItm = False
        btndelete.Visible = False
        'btndelete.Text = "Clear"
        fillGrid("")
        lblinvno.Text = ""
        lblinvno.Tag = ""
        txtphone.Text = ""
        calculate()
    End Sub
    Private Sub saveJob()
        _objJob = New clsJob
        With _objJob
            .Jobid = Val(txtjobcode.Tag)
            .jobcode = txtjobcode.Text
            .jobdate = DateValue(dtpdate.Value)
            .jobname = txtjobcode.Text
            .JobDescription = ""
            .custcode = Val(txtcustomer.Tag)
            .EstimatedDate = DateValue(dtpdeliverydate.Value)
            .SIID = 0
            .RvId = 0
            If Val(lblitemcost.Text) = 0 Then lblitemcost.Text = 0
            .ItemCost = CDbl(lblitemcost.Text)
            .Userid = CurrentUser
            If Val(txtserviceCharge.Text) = 0 Then txtserviceCharge.Text = 0
            .LabourCost = CDbl(txtserviceCharge.Text)
            If Val(numDisc.Text) = 0 Then numDisc.Text = 0
            .jobdiscount = CDbl(numDisc.Text)
            If Val(lbljbamount.Text) = 0 Then lbljbamount.Text = Format(0, numFormat)
            .NetAmt = CDbl(lbljbamount.Text)
            txtjobcode.Tag = .saveJob()
        End With
        saveJobItems()
        If txtphone.Text <> "" Then
            _objcmnbLayer._saveDatawithOutParm("Update AccMastAddr set Phone='" & txtphone.Text & "' where AccountNo=" & Val(txtcustomer.Tag))
        End If
        verifyInvoice()
        'If Val(lblinvno.Tag) > 0 Then
        '    verifyInvoice()
        'Else
        '    If MsgBox("Do you want to create Invoice?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        '    verifyInvoice()
        'End If
    End Sub
    Private Sub saveJobItems()
        _objJob = New clsJob
        Dim i As Integer
        _objcmnbLayer._saveDatawithOutParm("Update JobitemTb set setRemove=1 WHERE jbid=" & Val(txtjobcode.Tag))
        With grditems
            For i = 0 To .RowCount - 1
                If Val(.Item(lConstItemID, i).Value) = 0 Then GoTo nxt
                _objJob.Itemid = Val(.Item(lConstItemID, i).Value)
                _objJob.Jobid = Val(txtjobcode.Tag)
                If Val(.Item(lConstQty, i).Value) = 0 Then .Item(lConstQty, i).Value = 0
                _objJob.Qty = CDbl(.Item(lConstQty, i).Value)
                If Val(.Item(lConstUPrice, i).Value) = 0 Then .Item(lConstUPrice, i).Value = 0
                _objJob.Uprice = CDbl(.Item(lConstUPrice, i).Value)
                _objJob.jbitmId = Val(.Item(lConstId, i).Value)
                _objJob.trDtno = getDateNo(CDate(dtpdate.Value))
                _objJob.TaxP = CDbl(.Item(lConstTaxP, i).Value)
                _objJob.TaxAmt = CDbl(.Item(lConstTaxAmt, i).Value)
                _objJob.cgstPer = CDbl(.Item(lConstCGSTP, i).Value)
                _objJob.sgstPer = CDbl(.Item(lConstSGSTP, i).Value)
                _objJob.itmDescription = .Item(lConstDescr, i).Value
                _objJob.jbDescription = .Item(lConstJobDescr, i).Value
                _objJob.hsnCode = .Item(lConstHsnCode, i).Value
                _objJob.pFraction = Val(.Item(lConstPFraction, i).Value)
                _objJob.unitdiscount = Val(.Item(lConstunitDiscount, i).Value)
                _objJob.SlNo = i + 1
                _objJob.Trid = 0
                _objJob.saveJobItemTb()
nxt:
            Next
        End With
        _objcmnbLayer._saveDatawithOutParm("delete from JobitemTb where setRemove=1 and jbid=" & Val(txtjobcode.Tag))
        ischgItm = False
        saveMeasurement()
    End Sub
    Private Sub saveMeasurement()
        Dim i As Integer
        _objcmnbLayer._saveDatawithOutParm("delete from StichingJobMeasurementTb where  jobid=" & Val(txtjobcode.Tag))
        For i = 0 To dtm.Rows.Count - 1
            With grdmeasurement
                If dtm(i)("slno") >= 0 And Trim(dtm(i)("mvalue") & "") <> "" Then
                    _objcmnbLayer._saveDatawithOutParm("insert into StichingJobMeasurementTb (slno,jobid,measuermentid,measurementvalue,measurementremark) values(" & _
                                                   dtm(i)("slno") + 1 & "," & Val(txtjobcode.Tag) & "," & dtm(i)("measurementid") & ",'" & dtm(i)("mvalue") & "','')")
                End If

            End With
        Next
    End Sub
    Private Sub ldJobdetails()
        _objJob = New clsJob
        'Select Case cmbtype.SelectedIndex
        '    Case 0
        '        cmbdatewise.Items.Add("Job Date")
        '        cmbdatewise.Items.Add("Estimated Date")
        '    Case 1
        '        cmbdatewise.Items.Add("Job Date")
        '        cmbdatewise.Items.Add("Estimated Date")
        '        cmbdatewise.Items.Add("Delivery Date")
        '    Case 2
        '        cmbdatewise.Items.Add("Job Date")
        '        cmbdatewise.Items.Add("Delivery Date")
        '    Case Else
        '        cmbdatewise.Items.Add("Job Date")
        '        cmbdatewise.Items.Add("Estimated Date")
        '        cmbdatewise.Items.Add("Delivery Date")
        'End Select

        'With _objJob
        '    .Jobid = 0
        '    .DateFrom = DateValue(cldrStartDate.Value)
        '    .DateTo = DateValue(cldrEnddate.Value)
        '    .custcode = 0 'Val(txtsearch.Tag)
        '    '.IMEINo = IIf(rptCategory = 8, cmbtech.Text, txtsearch.Text)
        '    'rptCategory= 1-estimated datewise, 2-Job datewise list,3-Job datewise Closed,4-Job datewise Opened,5-Closed datewise Closed
        '    If cmbtype.SelectedIndex = 0 Then
        '        If cmbdatewise.SelectedIndex = 0 Then
        '            .Tp = 4
        '        ElseIf cmbdatewise.SelectedIndex = 1 Then
        '            .Tp = 1
        '        ElseIf cmbdatewise.SelectedIndex = 2 Then
        '            .Tp = 5
        '        End If

        '    ElseIf cmbtype.SelectedIndex = 1 Then
        '        If cmbdatewise.SelectedIndex = 1 Then
        '            .Tp = 5
        '        Else
        '            .Tp = 3
        '        End If

        '    Else
        '        .Tp = 2
        '    End If
        '    _vtable = .returnJob.Tables(0)
        'End With
        Dim qry As String
        Dim condition As String = ""
        Select Case cmbtype.SelectedIndex
            Case 0
                If cmbdatewise.SelectedIndex = 0 Then
                    condition = "where jobdate>='" & Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd") & "' and jobdate<='" & Format(DateValue(cldrEnddate.Value), "yyyy/MM/dd") & "'"
                Else
                    condition = "where estimateddate>='" & Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd") & "' and estimateddate<='" & Format(DateValue(cldrEnddate.Value), "yyyy/MM/dd") & "'"
                End If
                condition = condition & " and isnull([status],0)=0"
            Case 2
                If cmbdatewise.SelectedIndex = 0 Then
                    condition = "where jobdate>='" & Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd") & "' and jobdate<='" & Format(DateValue(cldrEnddate.Value), "yyyy/MM/dd") & "'"
                ElseIf cmbdatewise.SelectedIndex = 1 Then
                    condition = "where estimateddate>='" & Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd") & "' and estimateddate<='" & Format(DateValue(cldrEnddate.Value), "yyyy/MM/dd") & "'"
                ElseIf cmbdatewise.SelectedIndex = 2 Then
                    condition = "where convert(date,Jdeliverydate,103)>='" & Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd") & "' and convert(date,Jdeliverydate,103)<='" & Format(DateValue(cldrEnddate.Value), "yyyy/MM/dd") & "'"
                Else
                    condition = "where jobdate>='" & Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd") & "' and jobdate<='" & Format(DateValue(cldrEnddate.Value), "yyyy/MM/dd") & "'"
                End If
            Case 1
                If cmbdatewise.SelectedIndex = 0 Then
                    condition = "where jobdate>='" & Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd") & "' and jobdate<='" & Format(DateValue(cldrEnddate.Value), "yyyy/MM/dd") & "'"
                Else
                    condition = "where convert(date,Jdeliverydate,103)>='" & Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd") & "' and convert(date,Jdeliverydate,103)<='" & Format(DateValue(cldrEnddate.Value), "yyyy/MM/dd") & "'"
                End If
                condition = condition & " and isnull([status],0)=1"
            Case Else
                condition = condition & " where isnull([status],0)=0"
        End Select
        qry = "select jobcode,jobdate,jobname,case when status=1 then 'YES' else 'NO' end Closed,accdescr,Phone,estimateddate,Jdeliverydate," & _
              "isnull(NetAmt,0)jobvalue,isnull(servicecost,0)+isnull(itemcost,0)+isnull(LabourCost,0) Cost,isnull(NetAmt,0)-(isnull(servicecost,0)+isnull(itemcost,0)+isnull(LabourCost,0)) Profit," & _
              "userid,crdtdate,'" & Format(DateValue(cldrStartDate.Value), "dd/MM/yyyy") & "' datefrom,'" & Format(DateValue(cldrEnddate.Value), "dd/MM/yyyy") & "' dateto,1 lnk,jobid,servicecost,itemcost,jobclosedate " & _
              "from jobtb left join accmast on jobtb.custcode=accmast.accid " & _
              "left join accmastaddr on accmast.accid =accmastaddr.accountno " & condition

        grdItem.DataSource = Nothing
        _vtable = _objcmnbLayer._fldDatatable(qry)
        grdItem.DataSource = _vtable
        If _vtable.Rows.Count = 0 Then loadjobitemlist(0)
        SetGrid()

    End Sub
    Private Sub loadjobitemlist(ByVal jobid As Long)
        Dim dt As DataTable
        Dim qry As String
        qry = "select  [Item Code],itmdescription [Item Name],jbdescription [Description],QTY,uprice [Rate],((uprice-isnull(unitdiscount,0))*QTY)+taxamt [Line Total]," & _
              "case when isnull(isclosed,0) =1 then 'YES' else 'NO' end Closed " & _
              "from jobitemtb left join invitm on jobitemtb.itemid=invitm.itemid where jbid=" & jobid
        dt = _objcmnbLayer._fldDatatable(qry)
        With grdlistjobitems
            .DataSource = dt
            SetGridProperty(grdlistjobitems)
            .Columns("Item Name").Width = 150
            .Columns("Closed").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("QTY").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("QTY").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Rate").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Rate").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Rate").Visible = False
            .Columns("Line Total").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Line Total").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Line Total").Visible = False
            .Columns("Closed").Visible = False

        End With
        resizeGridColumn(grdlistjobitems, 2)
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
            '.Columns("JobDescription").HeaderText = "Job Description"
            .Columns("AccDescr").HeaderText = "Customer Name"
            '.Columns("ContactName").HeaderText = "Contact Name"
            .Columns("EstimatedDate").HeaderText = "Estimated Date"
            .Columns("EstimatedDate").Width = 110
            .Columns("Jdeliverydate").HeaderText = "Delivered Date"
            .Columns("Jdeliverydate").Width = 135
            '.Columns("EstimatedAmt").HeaderText = "Estimated Amount"
            .Columns("JobValue").HeaderText = "Job Value"
            .Columns("Userid").HeaderText = "Created By"
            .Columns("CrdtDate").HeaderText = "Created Date"
            '.Columns("EstimatedAmt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '.Columns("EstimatedAmt").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("JobValue").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("JobValue").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Datefrom").Visible = False
            .Columns("Dateto").Visible = False
            .Columns("lnk").Visible = False
            .Columns("jobid").Visible = False
            .Columns("ServiceCost").Visible = False
            .Columns("ItemCost").Visible = False
            .Columns("JobCloseDate").Visible = False
            '.Columns("JobDescription").Width = 200
            .Columns("AccDescr").Width = 200
            '.Columns("ContactName").Width = 200
            '.Columns("EstimatedDate").Width = 150
            '.Columns("EstimatedAmt").Width = 150
            .Columns("CrdtDate").Width = 150
            .Columns("Jobname").Visible = False
            .Columns("Cost").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Cost").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Cost").Visible = IIf(hidecost = 1, True, False)
            .Columns("Profit").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Profit").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Profit").Visible = IIf(hidecost = 1, True, False)
            setComboGrid()
            If hidecost = 0 Then
                resizeGridColumn(grdItem, 4)
            End If
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

    Private Sub TabControl2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl2.Click
        If TabControl2.SelectedIndex = 1 And grdItem.Rows.Count = 0 Then
            ldJobdetails()
        End If
    End Sub

    Private Sub btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnload.Click
        ldJobdetails()
    End Sub
    Public Sub ldRec(ByVal jbid As Long)
        If jbid = 0 Then Exit Sub
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
            txtjobcode.Text = ds.Tables(0)(0)("jobcode")
            txtprintjob.Text = ds.Tables(0)(0)("jobcode")
            txtjobcode.Tag = ds.Tables(0)(0)("Jobid")
            dtpdate.Value = ds.Tables(0)(0)("jobdate")
            'txtJobname.Text = Trim(ds.Tables(0)(0)("jobname") & "")
            'txtDescription.Text = ds.Tables(0)(0)("JobDescription")
            If Not IsDBNull(ds.Tables(0)(0)("EstimatedDate")) Then
                dtpdeliverydate.Value = ds.Tables(0)(0)("EstimatedDate")
            End If
            txtcustomer.Text = ds.Tables(0)(0)("AccDescr")
            txtcustomer.Tag = ds.Tables(0)(0)("custcode")
            txtaddress.Text = ds.Tables(0)(0)("Address1") & vbCrLf & ds.Tables(0)(0)("Address2") & vbCrLf & _
            ds.Tables(0)(0)("Address3") & vbCrLf & "Phone : " & ds.Tables(0)(0)("Phone") & vbCrLf & _
            ds.Tables(0)(0)("ContactName")
            txtphone.Text = ds.Tables(0)(0)("Phone")

            'txtobservedBy.Text = Trim(ds.Tables(0)(0)("OBTech") & "")
            'txttechnician.Text = Trim(ds.Tables(0)(0)("SManCode") & "")

            If Not IsDBNull(ds.Tables(0)(0)("LabourCost")) Then
                txtserviceCharge.Text = Format(CDbl(ds.Tables(0)(0)("LabourCost")), numFormat)
            Else
                txtserviceCharge.Text = Format(0, numFormat)
            End If
            numsto.Text = Val(ds.Tables(0)(0)("invno") & "")
            numsto.Tag = Val(ds.Tables(0)(0)("trid") & "")
            numDisc.Text = Format(CDbl(ds.Tables(0)(0)("jobdiscount") & ""), numFormat)

            lblinvno.Text = ds.Tables(0)(0)("TrRefNo")
            lblinvno.Tag = ds.Tables(0)(0)("ISTrid")
            lblnextnumber.Text = ds.Tables(0)(0)("ISNO")
            lblinvprefix.Text = ds.Tables(0)(0)("ISPrefix")

            If ds.Tables(0)(0)("jobstatus") = 0 Then
                lbljobstatus.Text = "ACTIVE"
                lbljobstatus.ForeColor = Color.Green
                btndelivery.Text = "Delivery"
            Else
                lbljobstatus.Text = "Delivered on " & ds.Tables(0)(0)("Jdeliverydate")
                lbljobstatus.ForeColor = Color.Red
                btndelivery.Text = "Undo Delivery"
            End If


            chgbyprg = False
        End If
        Dim i As Integer
        Dim j As Integer
        SetGridHead()
        grditems.Rows.Clear()
        If Not dtm Is Nothing Then
            If dtm.Rows.Count > 0 Then dtm.Rows.Clear()
        End If

        With grditems
            For j = 0 To ds.Tables(1).Rows.Count - 1
                .Rows.Add(1)
                i = .RowCount - 1
                .Item(lConstSlNo, i).Value = i + 1
                .Item(lConstItemCode, i).Value = ds.Tables(1)(j)("item code")
                '.Item(lConstDescr, i).Value = ds.Tables(1)(j)("Description")
                .Item(lConstUnit, i).Value = ds.Tables(1)(j)("Unit")
                .Item(lConstQty, i).Value = Format(Val(ds.Tables(1)(j)("Qty")), numFormat)
                .Item(lConstActualPrice, i).Value = CDbl(ds.Tables(1)(j)("Uprice"))
                .Item(lConstUPrice, i).Value = Format(CDbl(ds.Tables(1)(j)("Uprice")), numFormat)
                .Item(lConstTaxP, i).Value = Format(CDbl(ds.Tables(1)(j)("Taxp")), numFormat)
                .Item(lConstTaxAmt, i).Value = Format(CDbl(ds.Tables(1)(j)("TaxAmt")), numFormat)
                '.Item(lConstLTotal, i).Value = Format((CDbl(ds.Tables(1)(j)("Qty")) * CDbl(ds.Tables(1)(j)("Uprice"))) + CDbl(ds.Tables(1)(j)("TaxAmt")), numFormat)
                .Item(lConstItemID, i).Value = Val(ds.Tables(1)(j)("ItemId"))
                .Item(lConstId, i).Value = Val(ds.Tables(1)(j)("jbitmId"))
                .Item(lConstCGSTP, i).Value = CDbl(ds.Tables(1)(j)("cgstPer"))
                .Item(lConstSGSTP, i).Value = CDbl(ds.Tables(1)(j)("sgstPer"))
                .Item(lConstIGSTP, i).Value = CDbl(ds.Tables(1)(j)("sgstPer")) + CDbl(ds.Tables(1)(j)("cgstPer"))
                .Item(lConstJobDescr, i).Value = Trim(ds.Tables(1)(j)("jbDescription") & "")
                .Item(lConstDescr, i).Value = Trim(ds.Tables(1)(j)("itmDescription") & "")
                .Item(lConstHsnCode, i).Value = Trim(ds.Tables(1)(j)("hsncode") & "")
                .Item(lConstPFraction, i).Value = Val(ds.Tables(1)(j)("pFraction") & "")
                .Item(lConstAttend, i).Value = Trim(ds.Tables(1)(j)("attendedBy") & "")
                .Item(lConstFinished, i).Value = UCase(Trim(ds.Tables(1)(j)("isclosed") & ""))
                .Item(lConstunitDiscount, i).Value = ds.Tables(1)(j)("unitdiscount")
                getGSTDetails("", i, True, False)
            Next
        End With
        dtm.Rows.Clear()
        If ds.Tables(2).Rows.Count > 0 Then
            dtm = ds.Tables(2)
            grditems.Tag = 0
            getmeasurementFromDatatable(0)
        End If
        ldPostedInv()
        'ldItemCost()
        fillGrid(txtjobcode.Text)
        reArrangeNo()
        calculate()
        calculateStockTotal()
        reArrangeNoStock()
        TabControl2.SelectedIndex = 0
        btnundo.Visible = True
        btndelete.Text = "Delete"
        If userType Then
            btnupdate.Tag = IIf(getRight(251, CurrentUser), 1, 0)
            btndelete.Tag = IIf(getRight(252, CurrentUser), 1, 0)
        Else
            btnupdate.Tag = 1
            btndelete.Tag = 1
        End If
        btndelete.Visible = True
        btndelete.Enabled = True

    End Sub

    Private Sub grdItem_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdItem.DoubleClick
        Dim jobid As Long
        jobid = grdItem.Item("jobid", grdItem.CurrentRow.Index).Value
        ldRec(jobid)
    End Sub

    'STOCK OUT TRANSACTIONS
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
        'dtTable = _objcmnbLayer._fldDatatable("SELECT accid FROM AccMast WHERE AccSetId Like '%02%'")
        'If dtTable.Rows.Count > 0 Then DiscAcc = dtTable(0)("accid")
        'If dtTable.Rows.Count > 0 Then dtTable.Clear()
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
        _objcmnbLayer._saveDatawithOutParm("Update JobTb set ItemCost=" & CDbl(lblserviceGridTotal.Text) & " where jobid=" & Val(txtjobcode.Tag))
        'If Trim(LddImpDocs) <> "" Then RfrshPrssdQty(LddImpDocs)
        'If Not fwait Is Nothing Then fwait.Close() : fwait = Nothing
        MsgBox("Stock Out succesfully posted", MsgBoxStyle.Information)
        ldPostedInv()
        btnupdate.Enabled = True
        numsto.Tag = ""
    End Sub

#Region "STO"

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
            .NetAmt = CDbl(lblserviceGridTotal.Text)
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

        setAcctrDetValue(LinkNo, stockdebit, numsto.Text, numsto.Text, CDbl(lblserviceGridTotal.Text), txtjobcode.Text, "", 0, 0, "", _
                             "", stockcredit, "", "", 1)
        Dim dlAmt As Double = CDbl(lblserviceGridTotal.Text)
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
    Private Sub btnsaveService_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsaveService.Click
        If Val(txtjobcode.Tag) = 0 Then
            MsgBox("Invalid Job", MsgBoxStyle.Information)
            Exit Sub
        End If
        If grdVoucher.Rows.Count = 0 Then
            MsgBox("Consumption not found", MsgBoxStyle.Information)
            Exit Sub
        End If
        saveTrans()
    End Sub
    Private Sub ldPostedInv()
        Dim i As Integer

        Dim ItmInvCmnTb As DataTable
        Dim sRs As DataTable
        'Dim Discount As Double
        Dim UPerPack As Double
        Dim loadedTrId As Long
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select trid from ItmInvCmnTb where [job code]='" & txtjobcode.Text & "' and trtype='STO'")
        If dt.Rows.Count > 0 Then
            loadedTrId = dt(0)("trid")
        End If
        Dim itemquery As String = "SELECT ItmInvTrTb.*, [Item Code],Warrentyname,isSerialNo,FraCount," & _
                                          "isnull(itemCategory,'')itemCategory,paymentAC,vat,rgcess,rgcaccount,additionalcess FROM ItmInvTrTb LEFT JOIN InvItm ON InvItm.ItemId = ItmInvTrTb.ItemId " & _
                                          " LEFT JOIN InvItmPropertiesTb ON InvItmPropertiesTb.Itemid=invitm.itemid " & _
                                          "LEFT JOIN WarrentyMastertb ON WarrentyMastertb.Wid=ItmInvTrTb.WarrentyId " & _
                                          "LEFT JOIN UnitsTb ON UnitsTb.Units=ItmInvTrTb.Unit " & _
                                          "LEFT JOIN VatMasterTb ON InvItm.vatId=VatMasterTb.vatId " & _
                                          "LEFT JOIN (select vatcode rgccode,vat rgcess, paymentAC rgcaccount,vatid rgcid from  VatMasterTb)regularcess ON InvItm.regularcessid=regularcess.rgcid " & _
                                          "WHERE TrId = " & loadedTrId & " ORDER BY SlNo"

        'ItmInvCmnTb = _objcmnbLayer._fldDatatable("SELECT ItmInvCmnTb.*,jobname,[Voucher Name],Ctgry FROM ItmInvCmnTb " & _
        '                                          "LEFT JOIN PreFixTb ON PreFixTb.id=ItmInvCmnTb.invtypeno " & _
        '                                          "LEFT JOIN JobTb ON ItmInvCmnTb.[Job Code]=JobTb.jobcode WHERE TrId = " & loadedTrId & " AND TrType = 'IP'")
        Dim dtset As DataSet = _objcmnbLayer._ldDataset("SELECT ItmInvCmnTb.*,jobname,[Voucher Name],Ctgry,isnull(SalesmanTb.accountno,0) Smanacc,Alias,AccDescr,isnull(linkno,0)linkno " & _
                                                  "FROM ItmInvCmnTb " & _
                                                  "LEFT JOIN PreFixTb ON PreFixTb.id=ItmInvCmnTb.invtypeno " & _
                                                  "LEFT JOIN JobTb ON ItmInvCmnTb.[Job Code]=JobTb.jobcode " & _
                                                  "LEFT JOIN SalesmanTb ON SalesmanTb.SManCode=ItmInvCmnTb.SlsManId " & _
                                                  "LEFT JOIN AccMast ON AccMast.accid=ItmInvCmnTb.PSAcc " & _
                                                  "LEFT JOIN AccTrCmn on ItmInvCmnTb.trid=AccTrCmn.lnkno " & _
                                                  "WHERE TrId = " & loadedTrId & itemquery, False)
        chgbyprg = True
        ItmInvCmnTb = dtset.Tables(0)
        If ItmInvCmnTb.Rows.Count = 0 Then GoTo Quit
        Dim FCRt As Integer = 1
        Dim NDec As Integer = NoOfDecimal
        sRs = dtset.Tables(1)
        chgbyprg = True
        With grdVoucher
            .Rows.Clear()
            If sRs.Rows.Count > 0 Then
                For i = 0 To sRs.Rows.Count - 1
                    grdVoucher.Rows.Add(1)
                    chgbyprg = True
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
                    grdVoucher.Item(ConstLocation, i).Value = Trim("" & sRs(i)("Warrentyname")) ' !Colour
                    If Val(grdVoucher.Item(ConstPFraction, i).Value & "") = 0 Then grdVoucher.Item(ConstPFraction, i).Value = 0
                    grdVoucher.Item(ConstQty, i).Value = Format(sRs(i)("TrQty") / UPerPack, "#,##0" & IIf(Val(grdVoucher.Item(ConstPFraction, i).Value) = 0, "", "." & Strings.StrDup(CByte(grdVoucher.Item(ConstPFraction, i).Value), "0")))
                    'If Val(sRs(i)("Focqty") & "") = 0 Then sRs(i)("Focqty") = 0
                    'grdVoucher.Item(ConstFocQty, i).Value = Format(sRs(i)("Focqty") / UPerPack, "#,##0" & IIf(Val(grdVoucher.Item(ConstPFraction, i).Value) = 0, "", "." & Strings.StrDup(CByte(grdVoucher.Item(ConstPFraction, i).Value), "0")))
                    grdVoucher.Item(ConstUPrice, i).Value = Format(sRs(i)("UnitCost") * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & lnumformat)
                    grdVoucher.Item(ConstActualPrice, i).Value = sRs(i)("UnitCost") * UPerPack / FCRt
                    grdVoucher.Item(ConstLTotal, i).Value = Format((sRs(i)("TrQty") * sRs(i)("UnitCost")) * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & lnumformat)
                    grdVoucher.Item(ConstUnitOthCost, i).Value = Format(sRs(i)("UnitOthCost") * UPerPack / FCRt, "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))) ', "#,##" & lnumformat)
                    'grdVoucher.Item(ConstActualOthCost, i).Value = sRs(i)("UnitOthCost") * UPerPack / FCRt
                    'grdVoucher.Item(ConstSerialNo, i).Value = sRs(i)("SerialNo")
                    'If Val(sRs(i)("DisP") & "") = 0 Then sRs(i)("DisP") = 0
                    'grdVoucher.Item(ConstDisP, i).Value = Format(sRs(i)("DisP"), numFormat)
                    'If Val(sRs(i)("ItemDiscount") & "") = 0 Then sRs(i)("ItemDiscount") = 0
                    'grdVoucher.Item(ConstDisAmt, i).Value = Format(sRs(i)("ItemDiscount") * UPerPack / FCRt, numFormat)

                    grdVoucher.Item(ConstBarcode, i).Value = sRs(i)("HSNCode")
                    'grdVoucher.Item(ConstCGSTP, i).Value = sRs(i)("CSGTP")
                    'grdVoucher.Item(ConstCGSTAmt, i).Value = sRs(i)("CGSTAMT") / FCRt

                    'grdVoucher.Item(ConstSGSTP, i).Value = sRs(i)("SGSTP")
                    'grdVoucher.Item(ConstSGSTAmt, i).Value = sRs(i)("SGSTAmt") / FCRt

                    'grdVoucher.Item(ConstIGSTP, i).Value = sRs(i)("IGSTP")
                    'grdVoucher.Item(ConstIGSTAmt, i).Value = sRs(i)("IGSTAmt") / FCRt
                    'If Not IsDBNull(sRs(i)("isSerialNo")) Then
                    '    grdVoucher.Item(ConstIsSerial, i).Value = IIf(sRs(i)("isSerialNo"), 1, 0)
                    'Else
                    '    grdVoucher.Item(ConstIsSerial, i).Value = 0
                    'End If


                    If Val(sRs(i)("taxAmt") & "") = 0 Then
                        sRs(i)("taxAmt") = 0
                    End If
                    grdVoucher.Item(ConstTaxAmt, i).Value = sRs(i)("taxAmt") / FCRt
                    grdVoucher.Item(ConstTaxP, i).Value = sRs(i)("taxP")

                    'If Val(sRs(i)("vat") & "") = 0 Then sRs(i)("vat") = 0
                    'If Val(sRs(i)("rgcess") & "") = 0 Then sRs(i)("rgcess") = 0
                    'If Not enableGCC Then
                    '    grdVoucher.Item(Constcess, i).Value = Format(sRs(i)("vat"), numFormat)
                    '    grdVoucher.Item(ConstRegcess, i).Value = Format(sRs(i)("rgcess"), lnumformat)
                    'Else
                    '    grdVoucher.Item(Constcess, i).Value = 0
                    '    grdVoucher.Item(ConstRegcess, i).Value = 0
                    'End If

                    'If Val(sRs(i)("CessAmt") & "") = 0 Then sRs(i)("CessAmt") = 0
                    'grdVoucher.Item(ConstcessAmt, i).Value = Format(sRs(i)("CessAmt") / FCRt, lnumformat)
                    'If Val(sRs(i)("regularcessAmt") & "") = 0 Then sRs(i)("regularcessAmt") = 0
                    'grdVoucher.Item(ConstregularCessAmt, i).Value = Format(sRs(i)("regularcessAmt") / FCRt, lnumformat)
                    'If Val(sRs(i)("FloodcessAmt") & "") = 0 Then sRs(i)("FloodcessAmt") = 0
                    'grdVoucher.Item(ConstFloodCessAmt, i).Value = Format(sRs(i)("FloodcessAmt") / FCRt, lnumformat)
                    'If Val(sRs(i)("additionalcess") & "") = 0 Then sRs(i)("additionalcess") = 0
                    'grdVoucher.Item(ConstAdditionalcess, i).Value = sRs(i)("additionalcess") / FCRt
                    'If Val(sRs(i)("paymentAC") & "") = 0 Then sRs(i)("paymentAC") = 0
                    'grdVoucher.Item(ConstcessAc, i).Value = Val(sRs(i)("paymentAC"))
                    'If Val(sRs(i)("rgcaccount") & "") = 0 Then sRs(i)("rgcaccount") = 0
                    'grdVoucher.Item(ConstRegcessAc, i).Value = Val(sRs(i)("rgcaccount"))


                    '.Item(constItmTot, i).Value = Format((.Item(ConstQty, i).Value * CDbl(.Item(ConstActualPrice, i).Value)) - CDbl(.Item(ConstDisAmt, i).Value), lnumformat)
                    'grdVoucher.Item(ConstMthd, i).Value = sRs(i)("Method")
                    'grdVoucher.Item(ConstUnitVal, i).Value = sRs(i)("UnitValue") * UPerPack / FCRt
                    'grdVoucher.Item(ConstDiscOther, i).Value = sRs(i)("DiscOther") * UPerPack / FCRt
                    'grdVoucher.Item(ConstDiscOther, i).Value = sRs(i)("UnitDiscount") * UPerPack / FCRt
                    'grdVoucher.Item(ConstImpDocId, i).Value = Val(sRs(i)("impDocid") & "")
                    'grdVoucher.Item(ConstImpLnId, i).Value = Val(sRs(i)("impDocSlno") & "")
                    'grdVoucher.Item(ConstId, i).Value = sRs(i)("id")
                    'If Not IsDBNull(sRs(i)("WarrentyExpDate")) Then
                    '    If DateValue(sRs(i)("WarrentyExpDate")) > DateValue("01/01/1950") Then
                    '        grdVoucher.Item(ConstWarrentyExpiry, i).Value = sRs(i)("WarrentyExpDate")
                    '    End If
                    'End If
                    'If Not IsDBNull(sRs(i)("manufacturingdate")) Then
                    '    If DateValue(sRs(i)("manufacturingdate")) > DateValue("01/01/1950") Then
                    '        grdVoucher.Item(ConstManufacturingdate, i).Value = sRs(i)("manufacturingdate")
                    '    End If
                    'End If
                    'If .Item(ConstSerialNo, i).Value <> "" And enableSerialnumber Then
                    '    AddTodtSerialNo(.Item(ConstSerialNo, i).Value, Val(.Item(ConstItemID, i).Value), i, DateValue(.Item(ConstWarrentyExpiry, i).Value), Val(.Item(ConstId, i).Value))
                    'End If
                    'If Val(sRs(i)("SP1") & "") = 0 Then sRs(i)("SP1") = 0
                    'grdVoucher.Item(ConstSP1, i).Value = Format(sRs(i)("SP1"), numFormat)
                    'If Val(sRs(i)("SP2") & "") = 0 Then sRs(i)("SP2") = 0
                    'grdVoucher.Item(ConstSP2, i).Value = Format(sRs(i)("SP2"), numFormat)
                    'If Val(sRs(i)("SP3") & "") = 0 Then sRs(i)("SP3") = 0
                    'grdVoucher.Item(ConstSP3, i).Value = Format(sRs(i)("SP3"), numFormat)
                    'If Val(sRs(i)("MRP") & "") = 0 Then sRs(i)("MRP") = 0
                    'grdVoucher.Item(ConstMRP, i).Value = Format(sRs(i)("MRP"), numFormat)
                    'If enableWoodSale Then
                    '    tNumformat = "#,##0" & IIf(NDec = 0, "", "." & Strings.StrDup(NDec, "0"))
                    '    grdVoucher.Item(ConstWoodQty, i).Value = Format(sRs(i)("WoodNetQty") / UPerPack, tNumformat)
                    '    grdVoucher.Item(ConstWoodDiscQty, i).Value = Format(sRs(i)("WoodDiscountQty") / UPerPack, tNumformat)
                    'End If
                    'If enableGCC Then
                    '    If Val(sRs(i)("paymentAC") & "") = 0 Then sRs(i)("paymentAC") = 0
                    '    grdVoucher.Item(ConstcessAc, i).Value = Val(sRs(i)("paymentAC"))
                    'End If
                Next
                reArrangeNoStock()
            End If
        End With
        If sRs.Rows.Count > 0 Then sRs.Clear()
        calculateStockTotal()
Quit:
        chgbyprg = False
    End Sub

    Private Sub grditems_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grditems.CellDoubleClick
        If e.ColumnIndex = lConstFinished Then
            With grditems
                .Item(lConstFinished, e.RowIndex).Value = IIf(.Item(lConstFinished, e.RowIndex).Value = "YES", "NO", "YES")
            End With
        End If
    End Sub
    Public Sub ldImage(ByVal itemid As Long)
        dtPhotopath = DPath & "Photos"
        Dim objImage As Image
        picImage.Image = Nothing
        Dim dtImage As DataTable = _objcmnbLayer._fldDatatable("SELECT image1 FROM InvItm WHERE itemid=" & itemid)
        If dtImage.Rows.Count > 0 Then
            picImage.Tag = dtPhotopath & "/" & dtImage(0)("image1")
            If File.Exists(picImage.Tag) Then
                Try
                    Using str As FileStream = File.OpenRead(picImage.Tag)
                        objImage = Image.FromStream(str)
                    End Using
                    picImage.Image = objImage
                Catch ex As Exception
                End Try
            End If
        End If
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        printjobcard = False
        If chkFormat.Checked Then
            If TabControl2.SelectedIndex = 0 Then
                fRptFormat = New RptFormatfrm
                fRptFormat.RptType = "JBTC"
                fRptFormat.ShowDialog()
            Else
                'fRptFormat = New RptFormatfrm
                'fRptFormat.RptType = IIf(rdoactive.Checked, "JBS1", "JBS3")
                'fRptFormat.ShowDialog()
            End If
        Else
            If TabControl2.SelectedIndex = 0 Then
                PrepareRpt("JBTC")
            Else
                'fRptFormat = New RptFormatfrm
                'fRptFormat.RptType = IIf(rdoactive.Checked, "JBS1", "JBS3")
                'fRptFormat.ShowDialog()
            End If

        End If
    End Sub
    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        If TabControl2.SelectedIndex = 0 Then
            If fRptFormat.RptType = "IS" Then
                LoadReportIS(RptFlName, RptCaption, forPrint)
            Else
                LoadReport(RptFlName, RptCaption)
            End If
        Else
            'PrepareReportList(RptFlName, "", forPrint)
        End If
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
            If RptType = "IS" Then
                LoadReportIS(RptName, RptCaption, Forprint)
            Else
                LoadReport(RptName, RptCaption, Forprint)
            End If

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
        If printjobcard Then
            _objJob.jbitmId = grditems.Item(lConstId, grditems.CurrentRow.Index).Value
        End If
        '_objJob.jobcode = txtprintjob.Text
        Dim ds As DataSet = _objJob.returnJobForInvPrint(3)
        ds.Tables(1).Columns.Add("Image", GetType(Byte()))
        Dim i As Integer
        For i = 0 To ds.Tables(1).Rows.Count - 1
            Dim ppath As String = Trim(ds.Tables(1)(i)("ppath") & "")
            If File.Exists(ppath) Then
                Dim img As Image = Image.FromFile(ppath)
                Dim bytes As Byte() = CType((New ImageConverter()).ConvertTo(img, GetType(Byte())), Byte())
                ds.Tables(1)(i)("Image") = bytes
            End If
        Next
        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = fMainForm
        frm.Text = RptCaption
        frm.Show()

    End Sub
    Private Sub LoadReportIS(ByVal FileName As String, ByVal RptCaption As String, Optional ByVal ToPrint As Boolean = False, Optional ByVal pno As Integer = 0, Optional ByVal printername As String = "")
        If Not frm Is Nothing Then
            frm.Close()
            frm = New ReprtviewNEWfrm
        Else
            frm = New ReprtviewNEWfrm
        End If
        _objInv.Prefix = lblinvprefix.Text
        _objInv.InvNo = Val(lblnextnumber.Text)
        _objInv.TrType = "IS"
        If _objInv.InvNo = 0 Then Exit Sub
        Dim ds As DataSet
        ds = _objInv.ldInvoice("rturnInventoryDetailsForInvoicePrint", pno)
        If ToPrint Then
            _objcmnbLayer._saveDatawithOutParm("Update ItmInvCmnTb set Printed=1 where trid=" & Val(ds.Tables(0)(0)("trid")))
        End If
        frm.SetReport(ds, FileName, 0, False, , ToPrint)
        frm.btnprint.Tag = Val(ds.Tables(0)(0)("trid"))
        frm.MdiParent = Me.MdiParent
        frm.Text = RptCaption
        If Not ToPrint Then frm.Show()
    End Sub

    Private Sub btnprintjobcard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprintjobcard.Click
        If grditems.Rows.Count = 0 Then Exit Sub
        If grditems.CurrentRow Is Nothing Then Exit Sub
        If grdmeasurement.RowCount = 0 Then
            MsgBox("Select item from the list", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        printjobcard = True
        If chkFormat.Checked Then
            fRptFormat = New RptFormatfrm
            fRptFormat.RptType = "JBMC"
            fRptFormat.ShowDialog()
        Else
            PrepareRpt("JBMC")
        End If
        printjobcard = False
    End Sub

    Private Sub btnundo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnundo.Click
        makeClear()
        AddNew()
    End Sub

    Private Sub btninvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btninvoice.Click
        If Val(lblinvno.Tag) = 0 Then
            verifyInvoice()
        Else
            If chkFormat.Checked Then
                fRptFormat = New RptFormatfrm
                fRptFormat.RptType = "IS"
                fRptFormat.ShowDialog()
                fRptFormat = Nothing
            Else
                PrepareRpt("IS")
            End If
        End If

    End Sub
    Private Sub verifyInvoice()
        If EnableGST Then calculateGST()
        If Val(txtjobcode.Tag) = 0 Then
            MsgBox("Invalid Job", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        'If Val(lbljbamount.Text) = 0 Then
        '    MsgBox("Job Amount is zero is not allowed to invoice", MsgBoxStyle.Exclamation)
        '    Exit Sub
        'End If
        If Val(txtcustomer.Tag) = 0 Then
            MsgBox("Invalid customer", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        getNextInvoiceNumber()
        saveInvoice()
    End Sub


    Private Sub btncreateRV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncreateRV.Click
        If Val(lblbalance.Text) = 0 Then
            MsgBox("Receipt already done!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If cmbreceipttype.SelectedIndex = 0 Then
            Dim freceipt As New OtherReceiptsFrm
            If Val(txtjobcode.Tag) = 0 Then Exit Sub
            If Val(txtcustomer.Tag) = 0 Then Exit Sub
            With freceipt
                .chgbyprg = True
                .WindowState = FormWindowState.Normal
                .StartPosition = FormStartPosition.CenterParent
                .txtjobcode.Text = txtjobcode.Text
                .jobCustomer = Val(txtcustomer.Tag)
                .reference = lblinvno.Text
                If Val(lblbalance.Text) > 0 Then
                    .rvamt = CDbl(lblbalance.Text)
                End If
                .closeonSave = True

                '.ldjbname()
                .chgbyprg = False
                .ShowDialog()
                fillGrid(txtjobcode.Text)
                calculate()
            End With
        Else
            If txtcustomer.Text = "" Then
                MsgBox("Invalid Customer", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            fMainForm.LoadRV(0, txtcustomer.Text)
        End If
    End Sub
    Private Sub editRV(ByVal rvid As Long)
        Dim freceipt As New OtherReceiptsFrm
        If Val(txtjobcode.Tag) = 0 Then Exit Sub
        If Val(txtcustomer.Tag) = 0 Then Exit Sub
        With freceipt
            .chgbyprg = True
            .WindowState = FormWindowState.Normal
            .StartPosition = FormStartPosition.CenterParent
            .txtjobcode.Text = txtjobcode.Text
            .jobCustomer = Val(txtcustomer.Tag)
            .isModi = True
            .editRecord(rvid, True)
            .chgbyprg = False
            .ShowDialog()
            fillGrid(txtjobcode.Text)
        End With
    End Sub
    Public Sub fillGrid(ByVal jobcode As String)
        Dim strSql As String = ("Select  convert(varchar(12), case when prefix=''  then '' else '-' end +  invNo ) as [Inv No] , TrDate [Tr.Date] ," & _
                                "Alias [Cust. Code],AccDescr [Customer Name],(InvAmt-Discount) [Amount],TrRefNo [Ref. No],TrDescription  [Tr. Description]," & _
                                "jobcode,UserId [Created By],TrId from ( select  prefix, invNo ,TrDate ,InvAmt,Discount,AccDescr ,TrRefNo,TrDescription,Alias ," & _
                                "JobInvCmntb.UserId,JOBCODE,JobInvCmntb.TrId from JOBInvCmntb LEFT JOIN JobTb ON JobInvCmntb.jobid=JobTb.jobid  " & _
                                "LEFT JOIN (SELECT Trid,SUM((TrQty*(UnitCost+UnitOthCost))+taxamt)InvAmt FROM JobInvTrTb GROUP BY Trid) Tr ON  JobInvCmntb.Trid=Tr.Trid " & _
                                "left join Accmast on JobInvCmnTb.custid=Accmast.accid where JobInvCmntb.trtype='JIS' " & _
                                "and JobInvCmntb.jobid=" & Val(txtjobcode.Tag) & ") as qq  order by TrDate ,InvNo")
        grdinvList.DataSource = Nothing
        _objcmnbLayer = New clsCommon_BL
        Dim source As DataTable = Me._objcmnbLayer._fldDatatable(strSql, False)
        grdinvList.DataSource = source
        'Dim num3 As Integer = (source.Rows.Count - 1)
        'Dim i As Integer = 0
        'Do While (i <= num3)
        '    num2 = num2 + source(i)("Amount")
        '    i += 1
        'Loop
        'lblinvamt.Text = Format(num2, numFormat)
        source = Nothing
        strSql = "SELECT JVType [Type],PreFix,JVNum [RV No],JVDate [RV Date],Reference,DealAmt*-1 Amount,Accdescr [Paid By],dbtr.ChqNo [Chq No]," & _
                "dbtr.ChqDate [Chq Date],dbtr.BankCode [Bank Code],AccTrCmn.Linkno From AccTrCmn " & _
                "LEFT JOIN AccTrDet ON AccTrDet.Linkno=AccTrCmn.Linkno " & _
                "LEFT JOIN (SELECT Linkno,Accdescr,ChqNo,ChqDate,BankCode FROM AccTrDet " & _
                "LEFT JOIN AccMast On AccTrDet.accountno=AccMast.Accid where DealAmt>0) dbtr ON AccTrDet.Linkno=dbtr.Linkno " & _
                "where JobCode='" & jobcode & "' and JobCode<>'' and JVType='RV' and (DealAmt*-1)>0"
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

    Private Sub grdrvlist_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdrvlist.DoubleClick
        Dim id As Integer
        id = grdrvlist.Item("linkno", grdrvlist.CurrentRow.Index).Value
        editRV(id)
    End Sub
    Private Sub calculateGST()
        Dim i As Integer
        Dim dtHSN As DataTable
        Dim dtrow As DataRow
        Dim slno As Integer
        If dtTax Is Nothing Then Exit Sub
        If dtTax.Rows.Count > 0 Then
            dtTax.Rows.Clear()
        End If
        With grditems
            Dim _qurey As EnumerableRowCollection(Of DataRow)
            For i = 0 To .RowCount - 1
                slno = 0
                _qurey = From data In dtGST.AsEnumerable() Where data("HSNCode") = Trim(.Item(lConstHsnCode, i).Value & "") Select data
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
                            dtrow("Amount") = CDbl(.Item(lConstCGSTAmt, i).Value)
                            dtTax.Rows.Add(dtrow)
                        Else
                            dtTax(slno - 1)("Amount") = CDbl(dtTax(slno - 1)("Amount")) + CDbl(.Item(lConstCGSTAmt, i).Value)
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
                            dtrow("Amount") = CDbl(.Item(lConstSGSTAmt, i).Value)
                            dtTax.Rows.Add(dtrow)
                        Else
                            dtTax(slno - 1)("Amount") = CDbl(dtTax(slno - 1)("Amount")) + CDbl(.Item(lConstSGSTAmt, i).Value)
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
                            dtrow("Amount") = CDbl(.Item(lConstIGSTAmt, i).Value)
                            dtTax.Rows.Add(dtrow)
                        Else
                            dtTax(slno - 1)("Amount") = CDbl(dtTax(slno - 1)("Amount")) + CDbl(.Item(lConstIGSTAmt, i).Value)
                        End If
                    End If

                End If
                'If enableFloodCess And Val(lblgstn.Tag & "") = 0 And cessenddate >= DateValue(cldrdate.Value) Then
                '    Dim b = (From data In dtTax.AsEnumerable() Where data("ACid") = Val(.Item(ConstcessAc, i).Value & "") Select data("slno"))
                '    slno = 0
                '    For Each itm In b
                '        slno = itm
                '    Next
                '    If slno > 0 Then
                '        dtTax(slno - 1)("Amount") = CDbl(dtTax(slno - 1)("Amount")) + CDbl(.Item(ConstFloodCessAmt, i).Value)
                '    End If
                'End If
                'If enablecess Then
                '    Dim b = (From data In dtTax.AsEnumerable() Where data("ACid") = Val(.Item(ConstRegcessAc, i).Value & "") Select data("slno"))
                '    slno = 0
                '    For Each itm In b
                '        slno = itm
                '    Next
                '    If slno > 0 Then
                '        dtTax(slno - 1)("Amount") = CDbl(dtTax(slno - 1)("Amount")) + CDbl(.Item(ConstregularCessAmt, i).Value)
                '    End If
                'End If
            Next
        End With
    End Sub

    Private Sub txtdp_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtdp.KeyDown
        If e.KeyCode = Keys.Return Then
            btnupdate.Focus()
        End If
    End Sub

    Private Sub txtdp_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtdp.Validated
        If Val(lblTotAmt.Text) > 0 Then
            chgNumByPgm = True
            If Val(txtdp.Text) = 0 Then txtdp.Text = 0
            numDisc.Text = Format((CDbl(lblTotAmt.Text) * CDbl(txtdp.Text)) / 100, numFormat)
            chgNumByPgm = False
            calculate()
        End If
    End Sub

    Private Sub numDisc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles numDisc.KeyDown
        If e.KeyCode = Keys.Return Then
            btnupdate.Focus()
        End If
    End Sub

    Private Sub numDisc_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles numDisc.KeyPress
        NumericTextOnKeypress(sender, e, chgNumByPgm, numFormat)
    End Sub

    Private Sub numDisc_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles numDisc.Validated
        chgNumByPgm = True
        If Val(numDisc.Text) = 0 Then
            numDisc.Text = Format(0, numFormat)
        End If
        If Val(lblTotAmt.Text) = 0 Then lblTotAmt.Text = Format(0, numFormat)
        txtdp.Text = Format((CDbl(numDisc.Text) * 100) / CDbl(lblTotAmt.Text), numFormat)
        chgNumByPgm = False
        calculate()
    End Sub
    Private Sub getNextInvoiceNumber()
        Dim ds As DataSet
        ds = _objcmnbLayer._ldDataset("select top 1 Id,PreFix,VrNo,ANo,ANo2 from PreFixTb where isnull(Ctgry,0)=3 and  VrTypeNo = 4" & _
                                      " SELECT * FROM InvNos WHERE InvType='IS'", False)
        If ds.Tables(0).Rows.Count > 0 Then
            txtsalesac.Text = ds.Tables(0)(0)("ANo")
            lblinvprefix.Tag = ds.Tables(0)(0)("id")
        End If
        If ds.Tables(1).Rows.Count > 0 And Val(lblinvno.Tag) = 0 Then
            lblinvprefix.Text = Trim(ds.Tables(1)(0)("PreFix") & "")
            lblnextnumber.Text = ds.Tables(1)(0)("Invno")

        End If
    End Sub
#Region "SALES INVOICE"
    Private Sub saveInvoice()
        If Val(lblinvno.Tag) = 0 Then
chkagain:
            If Not CheckNoExists(lblinvprefix.Text, Val(lblnextnumber.Text), "IS", "Inventory") Then
                lblnextnumber.Text = Val(lblnextnumber.Text) + 1
                GoTo chkagain
            End If
        End If
        Dim invtypeno As Integer = getVouchernumber("IS")
        Dim TrId As Long = Val(lblinvno.Tag)
        setISCmnValue(TrId, invtypeno)
        TrId = Val(_objInv._saveCmn())
        Dim TDrAmt As Double = saveISTr(TrId, invtypeno)
        UpdateAccountsIS(TrId, 0, 0, invtypeno)
        lblinvno.Tag = TrId
        lblinvno.Text = lblinvprefix.Text & IIf(lblinvprefix.Text = "", "", "/") & lblnextnumber.Text
    End Sub
    Private Sub setISCmnValue(ByVal InvTrid As Long, ByVal invtypeno As Integer)
        With _objInv
            Dim Dt As Date = DateValue(Date.Now)
            .TrId = InvTrid
            .TrDate = DateValue(dtpdate.Value)
            .TrType = "IS"
            .DocLstTxt = ""
            .Prefix = lblinvprefix.Text
            .InvNo = Val(lblnextnumber.Text)
            .TrRefNo = lblinvprefix.Text & IIf(lblinvprefix.Text = "", "", "/") & lblnextnumber.Text
            .CSCode = Val(txtcustomer.Tag)
            .PSAcc = Val(txtsalesac.Text)
            .JobCode = txtjobcode.Text
            .ImpDoc = 0
            .UserId = CurrentUser
            .CashCustName = txtcustomer.Text
            .OthrCust = txtaddress.Text
            .IsFC = 0
            .FCRate = 1
            .NFraction = 2
            .FC = ""
            If Val(numDisc.Text) = 0 Then
                .Discount = 0
            Else
                .Discount = CDbl(numDisc.Text)
            End If
            .TrDescription = ""
            .TypeNo = invtypeno 'trtype no
            .InvTypeNo = Val(lblinvprefix.Tag)
            .EnaJob = False
            .DocDefLoc = ""
            .BrId = UsrBr
            .OthCost = 0
            .Discount1 = 0
            .NetAmt = CDbl(lbltotal.Text)
            .LPO = ""
            'Dim dt As Date = getServerDate()
            .CrtDt = Dt
            .DelDate = Dt
            .DueDate = Dt
            .DocDate = Dt
            .SuppInvDate = Dt
            .TermsId = ""
            .MchName = MACHINENAME
            .isModi = IIf(Val(lblinvno.Tag) > 0, True, False)
            .lpoclass = ""
            If Val(txtroundOff.Text) = 0 Then txtroundOff.Text = Format(0, numFormat)
            .rndoff = CDbl(txtroundOff.Text) * IIf(cmbsign.SelectedIndex = 1, -1, 1)
            .isTaxInvoice = EnableGST
        End With

    End Sub
    Private Function saveISTr(ByVal Invid As Long, ByVal TrTypeNo As Integer) As Double
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
        With grditems
            For i = 0 To .Rows.Count - 1
                If .Item(lConstSlNo, i).Value.ToString <> "M" And Val(.Item(lConstItemID, i).Value) = 0 Then GoTo nxtM
                PPerU = 1 'Switch(.TextMatrix(i, 4) = "B", 1, .TextMatrix(i, 4) = "P1", CSng(grdVoucher.TextMatrix(i, 18)), .TextMatrix(i, 4) = "P2", CSng(grdVoucher.TextMatrix(i, 19)))
                PPerU = IIf(PPerU = 0, 1, PPerU)

                dtrow = dtInvTb.NewRow
                dtrow("TrId") = Invid
                dtrow("ItemId") = IIf(.Item(lConstSlNo, i).Value.ToString <> "M", Val(.Item(lConstItemID, i).Value), 0)
                dtrow("Unit") = .Item(lConstUnit, i).Value
                dtrow("TrQty") = CDbl(.Item(lConstQty, i).Value) * PPerU
                dtrow("Focqty") = 0
                dtrow("UnitCost") = CDbl(.Item(lConstActualPrice, i).Value) * FCRt / PPerU 'CDbl(.TextMatrix(i, 10)) / PPerU

                TDrAmt = TDrAmt + CDbl(.Item(lConstQty, i).Value) * (CDbl(.Item(lConstActualPrice, i).Value) - CDbl(.Item(lConstunitDiscount, i).Value))

                dtrow("taxP") = CDbl(.Item(lConstTaxP, i).Value)
                dtrow("taxAmt") = (CDbl(.Item(lConstTaxAmt, i).Value) * FCRt)
                dtrow("PFraction") = PPerU
                dtrow("UnitOthCost") = 0
                dtrow("Method") = "B"
                dtrow("UnitDiscount") = Val(.Item(lConstunitDiscount, i).Value) * FCRt / PPerU
                If Not IsDBNull(.Item(lConstDescr, i).Value) Then
                    dtrow("IDescription") = Trim(.Item(lConstDescr, i).Value)
                End If
                dtrow("SlNo") = i + 1
                dtrow("TrTypeNo") = TrTypeNo
                dtrow("TrDateNo") = getDateNo(CDate(dtpdate.Value))
                dtrow("id") = Val(.Item(lConstId, i).Value)
                dtrow("SerialNo") = ""
                dtrow("WarrentyExpDate") = DateValue("01/01/1950")
                dtrow("HSNCode") = Trim(.Item(lConstHsnCode, i).Value & "")
                If Val(.Item(lConstCGSTP, i).Value & "") = 0 Then .Item(lConstCGSTP, i).Value = 0
                dtrow("CSGTP") = CDbl(.Item(lConstCGSTP, i).Value)
                If Val(.Item(lConstSGSTP, i).Value & "") = 0 Then .Item(lConstSGSTP, i).Value = 0
                dtrow("SGSTP") = CDbl(.Item(lConstSGSTP, i).Value)
                If Val(.Item(lConstIGSTP, i).Value & "") = 0 Then .Item(lConstIGSTP, i).Value = 0
                dtrow("IGSTP") = CDbl(.Item(lConstIGSTP, i).Value)
                If Val(.Item(lConstIGSTAmt, i).Value) = 0 Then .Item(lConstCGSTAmt, i).Value = 0
                If Val(.Item(lConstSGSTAmt, i).Value) = 0 Then .Item(lConstSGSTAmt, i).Value = 0
                If Val(.Item(lConstIGSTAmt, i).Value) = 0 Then .Item(lConstIGSTAmt, i).Value = 0
                dtrow("IGSTAmt") = CDbl(.Item(lConstIGSTAmt, i).Value) * FCRt
                dtrow("CGSTAMT") = CDbl(.Item(lConstCGSTAmt, i).Value) * FCRt
                dtrow("SGSTAmt") = CDbl(.Item(lConstSGSTAmt, i).Value) * FCRt
                dtrow("FloodcessAmt") = 0
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
    Private Sub UpdateAccountsIS(ByVal TrId As Long, ByVal TDrAmt As Double, ByVal DiscAcc As Integer, ByVal invtypeno As Integer)
        Dim LinkNo As Long
        Dim dtTable As DataTable
        If Val(lblinvno.Tag) > 0 And TrId > 0 Then
            dtTable = _objcmnbLayer._fldDatatable("SELECT LinkNo FROM AccTrCmn WHERE LnkNo = " & TrId)
            If dtTable.Rows.Count > 0 Then
                LinkNo = dtTable(0)("LinkNo")
                _objcmnbLayer._saveDatawithOutParm("DELETE FROM AccTrDet WHERE LinkNo=" & LinkNo)
            End If
        End If

        setAcctrCmnValue(TrId, LinkNo, "IS", lblinvprefix.Text, Val(lblnextnumber.Text), invtypeno)
        If dtAccTb Is Nothing Then
            dtAccTb = _objcmnbLayer._fldDatatable("Select top 1 LinkNo,AccountNo,DueDate,Reference,EntryRef,DealAmt,FCAmt,CurrencyCode,CurrRate, " & _
                                                  "JobCode,PDCAcc,BankCode,LPONo,OthCost,TrInf,TermsId,CustAcc,AccWithRef,ChqDate,ChqNo,SuppInvDate," & _
                                                  "vatid,isvatEntry,UnqNo from AccTrDet")
        End If
        If dtAccTb.Rows.Count > 0 Then
            dtAccTb.Rows.Clear()
        End If
        Dim EntRef As String = "SALES INVOICE FROM TAILORING ORDER - " & txtjobcode.Text
        Dim reference As String = lblinvprefix.Text & IIf(lblinvprefix.Text = "", "", "/") & Val(lblnextnumber.Text)
        setAcctrDetValue(LinkNo, Val(txtcustomer.Tag), reference, EntRef, CDbl(lbltotal.Text), txtjobcode.Text, "", 0, 0, "", _
                             "", Val(txtsalesac.Text), "", "", 1)
        'Dim dlAmt As Double = CDbl(lblserviceGridTotal.Text)
        'Credit Entry
        If dtTax Is Nothing Then
            GoTo nxt
        End If
        Dim i As Integer = 0
        Dim ttlTxAmount As Double
        Dim TxAmount As Double
        For i = 0 To dtTax.Rows.Count - 1
            If Val(dtTax(i)("ACid")) > 0 And Val(dtTax(i)("Amount")) > 0 Then
                TxAmount = Format(CDbl(dtTax(i)("Amount")), numFormat)
                ttlTxAmount = Format(CDbl(ttlTxAmount) + TxAmount, numFormat)
                setAcctrDetValue(LinkNo, Val(dtTax(i)("ACid")), reference, EntRef, TxAmount * -1, "", "", 0, 0, "", _
                         "", Val(txtcustomer.Tag), "", "", 1)
            End If
        Next
nxt:
        Dim cramt As Double
        cramt = CDbl(lblTotAmt.Text)
        cramt = CDbl(lbltotal.Text) - CDbl(lbltax.Text)
        Dim total As Double = cramt + ttlTxAmount
        total = CDbl(lbltotal.Text) - total
        cramt = (cramt + total)
        setAcctrDetValue(LinkNo, Val(txtsalesac.Text), reference, EntRef, cramt * -1, "", "", 0, 0, "", _
                         "", Val(txtcustomer.Tag), "", "", 1)
        updateStockTransaction(TrId, LinkNo, lblinvprefix.Text, Val(lblnextnumber.Text), "IS")
        _objTr.SaveAccTrWithDt(dtAccTb)
    End Sub
    Private Sub updateStockTransaction(ByVal trid As Long, ByVal LinkNo As Long, ByVal prefix As String, ByVal invno As Integer, ByVal trtype As String)
        If Not enableCostAccounting Then Exit Sub
        Dim stockAc As Long
        Dim costOfSalesAc As Long
        Dim dt As DataTable
        Dim costAmt As Double
        stockAc = getConstantAccounts(1)
        costOfSalesAc = getConstantAccounts(9)
        If stockAc = 0 Or costOfSalesAc = 0 Then Exit Sub
        dt = _objcmnbLayer._fldDatatable("select sum(CostAvg*TrQty) costAmt from ItmInvTrTb  where trid=" & trid)
        If dt.Rows.Count > 0 Then
            costAmt = Val(dt(0)("costAmt") & "")
        End If
        If trtype = "IS" Then
            trtype = "INVOICE"
        Else
            trtype = "STOCK OUT"
        End If
        If costAmt <> 0 Then
            'debit entry [cost of sales]
            Dim entryref As String = "COST OF SALES FOR " & trtype & " : " & txtcustomer.Text & " # " & prefix & invno
            setAcctrDetValue(LinkNo, costOfSalesAc, Trim(prefix & IIf(prefix = "", "", "/") & invno), entryref, costAmt, "", "", 3, 0, "", _
                           "", Val(txtcustomer.Tag), "", "", 1)
            'credit entry [stock in hand]
            costAmt = costAmt * -1
            setAcctrDetValue(LinkNo, stockAc, Trim(prefix & IIf(prefix = "", "", "/") & invno), entryref, costAmt, "", "", 3, 0, "", _
                           "", Val(txtcustomer.Tag), "", "", 1)
        End If
    End Sub
#End Region

    Private Sub cmbtype_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbtype.SelectedIndexChanged
        cmbdatewise.Items.Clear()
        Select Case cmbtype.SelectedIndex
            Case 0
                cmbdatewise.Items.Add("Job Date")
                cmbdatewise.Items.Add("Estimated Date")
                'Case 1
                '    cmbdatewise.Items.Add("Job Date")
                '    cmbdatewise.Items.Add("Estimated Date")
                '    cmbdatewise.Items.Add("Delivery Date")
            Case 1
                cmbdatewise.Items.Add("Job Date")
                cmbdatewise.Items.Add("Delivery Date")
            Case Else
                cmbdatewise.Items.Add("Job Date")
                cmbdatewise.Items.Add("Estimated Date")
                cmbdatewise.Items.Add("Delivery Date")
        End Select
    End Sub

    Private Sub grdItem_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItem.RowEnter
        If grdItem.Rows.Count = 0 Then loadjobitemlist(0) : Exit Sub
        'If grdItem.CurrentRow Is Nothing Then Exit Sub
        Dim jobid As Long
        jobid = grdItem.Item("Jobid", e.RowIndex).Value
        loadjobitemlist(jobid)
    End Sub

    Private Sub btndelivery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelivery.Click
        If btndelivery.Text = "Delivery" Then
            If MsgBox("Do you want set Delivery?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            Dim f As New JobClosing
            f.lbljjob.Tag = Val(txtjobcode.Tag)
            f.lbljjob.Text = txtjobcode.Text
            f.ShowDialog()
            ldRec(Val(txtjobcode.Tag))
            f = Nothing
            'lbljobstatus.ForeColor = Color.Red
            'btndelivery.Text = "Undo Delivery"
        Else
            If MsgBox("Do you want to Undo Delivery?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            Dim qry As String
            qry = "Update JobTb set Status=0, JobCloseDate=null,Jdeliverydate=null where Jobid=" & Val(txtjobcode.Tag)
            qry = qry & "UPDATE JobItemtb SET isClosed =0,closedDate=null WHERE jbid=" & Val(txtjobcode.Tag)
            _objcmnbLayer._saveDatawithOutParm(qry)
            'lbljobstatus.Text = ""
            'btndelivery.Text = "Delivery"
        End If

    End Sub

    Private Sub txtSeq_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeq.TextChanged
        grdItem.DataSource = SearchGrid(_vtable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
        _dtRptTable = SearchGrid(_vtable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
    End Sub

    Private Sub btnrefreshVoucher_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefreshVoucher.Click
        ldRec(Val(txtjobcode.Tag))
    End Sub

    Private Sub txtphone_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtphone.KeyDown
        If e.KeyCode = Keys.Return Then
            btnitmadd.Focus()
        End If
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        If Val(btndelete.Tag) = 0 Then
            MsgBox("This user does not have permission to delete Job", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If MsgBox("Do you want delete Job?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        Dim qry As String
        Dim dt As DataTable
        Dim linknos As String = ""
        Dim i As Integer
        dt = _objcmnbLayer._fldDatatable("select linkno from acctrdet where JobCode='" & txtjobcode.Text & "' group by linkno")
        For i = 0 To dt.Rows.Count - 1
            linknos = IIf(linknos = "", "", ",") & dt(i)("linkno")
        Next
        qry = "delete from jobtb where jobid=" & Val(txtjobcode.Tag)
        qry = qry & "delete from JobitemTb where jbid=" & Val(txtjobcode.Tag)
        qry = qry & "delete from  StichingJobMeasurementTb where jobid = " & Val(txtjobcode.Tag)
        qry = qry & "delete from  StichingJobMeasurementTb where jobid = " & Val(txtjobcode.Tag)
        If linknos <> "" Then
            qry = qry & "delete from acctrdet where linkno in (" & linknos & ")"
            qry = qry & "delete from acctrcmn where linkno in (" & linknos & ")"
        End If
        
        _objcmnbLayer._fldDatatable(qry)
        deleteInvoice()
        makeClear()
        AddNew()
    End Sub
    Private Sub deleteInvoice()
        _objInv.TrId = Val(lblinvno.Tag)
        _objInv.TrType = "OUT"
        _objInv.deleteInventoryTransactions()

        '*****************************
        _objInv.TrId = Val(numsto.Tag)
        _objInv.TrType = "OUT"
        _objInv.deleteInventoryTransactions()
    End Sub

    Private Sub picwhatsapp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picwhatsapp.Click
        Dim f As New WhatsaapFrm
        f.txtphone.Text = txtphone.Text
        f.txtparty.Text = txtcustomer.Text
        f.TopMost = True
        f.txtjobcode.Text = txtjobcode.Text
        f.txtamount.Text = lbltotal.Text
        f.txtreceived.Text = lblRv.Text
        f.Jobtype = "Job Code"
        f.ShowDialog()
        'fMainForm.TopMost = False
    End Sub

    Private Sub grdSrch_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSrch.CellContentClick

    End Sub

    Private Sub grdSrch_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSrch.DoubleClick
        If _srchTxtId = 1 Then
            activecontrolname = "grditems"
            doSelect(2)
            'chgbyprg = True
            With grditems
                If .CurrentCell.ColumnIndex = ConstItemCode Then
                    .CurrentCell = .Item(1, .CurrentRow.Index)
                    ischgItm = True
                    Valid(.CurrentCell.RowIndex, .CurrentCell.ColumnIndex)
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

    Private Sub txtdp_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtdp.TextChanged

    End Sub
End Class