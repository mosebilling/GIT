

Public Class QuickItem
    'object variable
    Dim _objcmnbLayer As New clsCommon_BL
    Dim _objItmMast As New clsItemMast_BL

    Public NewItemCode As String
    Public IsModi As Boolean
    Public BaseId As Long
    Public ItemId As Long

    Public Event PassData(ByVal ItemCode As String, ByVal ismode As Boolean)
    Public dontClose As Boolean
    'numeric text
    Dim idx As Integer
    Dim str1 As String
    Dim str2 As String
    Dim str3 As String
    Dim SelStart As Integer
    Dim numCtrl As TextBox
    Private nSelect As Integer
    Private chgbyprg As Boolean
    Private chgbyprgAmt As Boolean

    Public Sub ldRec(ByVal Id As Long)
        chgbyprg = True
        Dim found As Boolean
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT * FROM InvItm " & _
                                         " LEFT JOIN GSTTb ON GSTTb.HSNCode=InvItm.HSNCode " & _
                                         "  WHERE ItemId = " & Id)
        For i = 0 To dt.Rows.Count - 1
            found = True
            txtCode.Text = dt(0)("Item Code")
            ItemId = dt(0)("ItemID")
            txtTrDescr.Text = Trim(dt(0)("Description") & "")
            cmbUnit.Text = Trim(dt(0)("Unit") & "")
            numunitprice.Text = Format(dt(0)("UnitPrice"), numFormat)
            numunitprice.Tag = dt(0)("UnitPrice")
            txthsncode.Text = Trim(dt(0)("HSNCode") & "")
            txtws.Text = Format(IIf(IsDBNull(dt(0)("UnitPriceWS")), 0, dt(0)("UnitPriceWS")), numFormat)
            txtmrp.Text = Format(IIf(IsDBNull(dt(0)("mrp")), 0, dt(0)("mrp")), numFormat)
            cmbCategory.Text = IIf(IsDBNull(dt(0)("itemCategory")), "Stock", dt(0)("itemCategory"))
            If Val(dt(0)("IGST") & "") > 0 Then
                txtpriceWtax.Text = Format(CDbl(numunitprice.Text) + ((CDbl(numunitprice.Text) * CDbl(dt(0)("IGST"))) / 100), numFormat)
                txtpriceWtax.Tag = CDbl(dt(0)("IGST"))
            Else
                txtpriceWtax.Tag = 0
            End If
            If txthsncode.Text = "" Then
                lblgstp.Text = ""
            Else
                lblgstp.Text = "GST : " & Val(txtpriceWtax.Tag) & "%"
            End If
            txtCode.Focus()
        Next
        If Not found Then
            ItemId = 0
            BaseId = 0
        End If
        chgbyprg = False

    End Sub
    Private Sub ldHSN()
        Dim ObjLocationlist As New List(Of String)
        ObjLocationlist = toLoadAutoFillListItems("HSNCode", "GSTTb")
        Call toAssignDownListToText(txthsncode, ObjLocationlist) '
    End Sub
    Private Sub QuickItem_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'numFormat = "0." & Microsoft.VisualBasic.StrDup(NoOfDecimal, "0")
        ldUnits()
        ldHSN()
        If cmbUnit.Items.Count > 1 Then cmbUnit.SelectedIndex = 1
        'clearControls()
        If IsModi Then ldRec(ItemId)
        Timer1.Enabled = True
    End Sub
    Private Sub clearControls()
        For Each Control In Me.Controls
            If TypeOf (Control) Is TextBox Then
                Control.text = ""
                Control.tag = ""
            End If
        Next
    End Sub
    Private Sub ldUnits()
        Dim dtTable As DataTable
        Dim i As Integer
        dtTable = _objcmnbLayer._fldDatatable("Select Units From UnitsTb Order by IsDefault desc,Units")
        cmbUnit.Items.Clear()
        If dtTable.Rows.Count > 0 Then
            For i = 0 To dtTable.Rows.Count - 1
                cmbUnit.Items.Add(dtTable(i)("Units"))
            Next
        End If
    End Sub

  

    Private Sub numBaseSalesprice_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
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

    Private Sub txtCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCode.KeyDown, txtTrDescr.KeyDown, cmbCategory.KeyDown, cmbUnit.KeyDown, _
                                                                                                              txthsncode.KeyDown, numunitprice.KeyDown, _
                                                                                                              txtpriceWtax.KeyDown, txtws.KeyDown, txtmrp.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged, txtTrDescr.TextChanged
        'If chgbyprg Then Exit Sub
        BtnUpdate.Enabled = True
    End Sub

    Private Sub BtnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUpdate.Click
        If chkFields() Then Exit Sub
        setValue()
        BaseId = _objItmMast._saveItemMast()
        MsgBox("Item Created Successfully", MsgBoxStyle.Information)
        RaiseEvent PassData(txtCode.Text, IsModi)
        If Not dontClose Then
            Me.Dispose()
            Close()
        Else
            Me.Hide()
        End If
    End Sub
    Private Function chkFields() As Boolean
        Dim _vdtTable As DataTable
        If Trim(txtCode.Text) = "" Then
            MsgBox("'Item Code' Cannot be Blank !", MsgBoxStyle.Information)
            txtCode.Focus()
        End If
        If IsModi Then
            _vdtTable = _objcmnbLayer._fldDatatable("Select ItemId from InvItm Where ItemId= " & ItemId)
            If _vdtTable.Rows.Count = 0 Then
                MsgBox("Selected Base Item Not Found !", MsgBoxStyle.Information)
                GoTo err
            End If
        End If
        _vdtTable = _objcmnbLayer._fldDatatable("Select ItemId from InvItm Where [Item Code] ='" & MkDbSrchStr(txtCode.Text) & "'" & IIf(IsModi, " and ItemId <>" & ItemId, ""))
        If _vdtTable.Rows.Count > 0 Then
            MsgBox("Item Code Already Exist as [Item Code] !", MsgBoxStyle.Information)
            txtCode.Focus()
            GoTo err
        End If
        
        
        chkFields = False
        Exit Function
Err:
        chkFields = True
    End Function
    Private Sub setValue()
        _objItmMast.ItemId = ItemId
        _objItmMast.ItemCode = Trim(txtCode.Text)
        _objItmMast.Descr = Trim(txtTrDescr.Text)
        _objItmMast.Unit = cmbUnit.Text
        If Val(numunitprice.Text) = 0 Then numunitprice.Text = 0
        If Val(txtws.Text) = 0 Then txtws.Text = 0
        If Val(txtmrp.Text) = 0 Then txtmrp.Text = 0
        _objItmMast.salesPrice = numunitprice.Tag
        _objItmMast.Category = cmbCategory.Text
        _objItmMast.WSalesPrice = CDbl(txtws.Text)
        _objItmMast.MRP = CDbl(txtmrp.Text)
        _objItmMast.hsncode = txthsncode.Text
        If IsModi Then
            _objItmMast.LstModiBy = CurrentUser
            _objItmMast.LstModiDt = Date.Now
            _objItmMast.CreatedDt = Date.Now
            _objItmMast.Createdby = Trim(CurrentUser)
        Else
            _objItmMast.LstModiBy = ""
            _objItmMast.Createdby = Trim(CurrentUser)
            _objItmMast.CreatedDt = Date.Now
            _objItmMast.LstModiDt = Date.Now
        End If
        _objItmMast.Ismodi = IsModi
    End Sub

    Private Sub BtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExit.Click
        Me.Dispose()
        Close()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        Select Case Val(Timer1.Tag)
            Case 0
                txtCode.Focus()
            Case 1
                txtpriceWtax.Focus()
        End Select
    End Sub

    Private Sub numunitprice_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles numunitprice.Validated
        If Val(numunitprice.Text) > 0 Then calculateTaxFromUnitPrice(True)
    End Sub
    Private Sub calculateTaxFromUnitPrice(ByVal fromUnitpice As Boolean)
        chgbyprgAmt = True
        If Val(txtpriceWtax.Tag & "") = 0 Then txtpriceWtax.Tag = 0
        If Not fromUnitpice Then
            numunitprice.Text = (CDbl(txtpriceWtax.Text) * 100) / (CDbl(txtpriceWtax.Tag) + 100)
            numunitprice.Tag = CDbl(numunitprice.Text)
            numunitprice.Text = Format(CDbl(numunitprice.Text), numFormat)
        Else
            txtpriceWtax.Text = Format(CDbl(numunitprice.Tag) + ((CDbl(numunitprice.Tag) * CDbl(txtpriceWtax.Tag)) / 100), numFormat)
        End If
        chgbyprgAmt = False
    End Sub

    Private Sub numunitprice_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numunitprice.TextChanged
        'numunitprice.Tag = CDbl(numunitprice.Text)
    End Sub

    Private Sub numunitprice_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles numunitprice.KeyPress, txtpriceWtax.KeyPress, txtmrp.KeyPress, txtws.KeyPress, txtamount.KeyPress, txtpercentage.KeyPress
        Dim myctr As TextBox
        myctr = sender
        If myctr.ReadOnly Then Exit Sub
        NumericTextOnKeypress(sender, e, chgbyprgAmt, numFormat)
    End Sub

    Private Sub txtpriceWtax_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtpriceWtax.Validated
        If Val(txtpriceWtax.Text) > 0 Then calculateTaxFromUnitPrice(False)
    End Sub

    Private Sub btnset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnset.Click
        If Val(txtamount.Text) = 0 Or Val(txtpercentage.Text) = 0 Then Exit Sub
        If rdowithouttax.Checked Then
            numunitprice.Text = Format(CDbl(txtamount.Text) + (CDbl(txtamount.Text) * CDbl(txtpercentage.Text) / 100), numFormat)
            If Val(numunitprice.Text) > 0 Then calculateTaxFromUnitPrice(True)
        Else
            txtpriceWtax.Text = Format(CDbl(txtamount.Text) + (CDbl(txtamount.Text) * CDbl(txtpercentage.Text) / 100), numFormat)
            If Val(txtpriceWtax.Text) > 0 Then calculateTaxFromUnitPrice(False)
        End If
    End Sub


    Private Sub txthsncode_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txthsncode.Validated
        If chgbyprg Then Exit Sub
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT IGST FROM GSTTB WHERE HSNCode='" & txthsncode.Text & "'")
        If dt.Rows.Count > 0 Then
            txtpriceWtax.Tag = Format(dt(0)("IGST"), numFormat)
            lblgstp.Text = "GST : " & Val(txtpriceWtax.Tag) & "%"
            If Val(numunitprice.Text) > 0 Then
                calculateTaxFromUnitPrice(True)
            ElseIf Val(txtpriceWtax.Text) > 0 Then
                calculateTaxFromUnitPrice(False)
            End If
        Else
            txtpriceWtax.Tag = 0
            lblgstp.Text = ""
        End If
    End Sub
End Class