

Public Class CourseItem
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
    Private chgamt As Boolean
    Private Sub loadItemdetails()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT [Item Code],Description,totalfees [Fees Amount],totalfees+((totalfees*isnull(vat,0))/100) [Fees + Tax],convert(varchar,courseduration)+ case when courseduration>1 then ' Months' else ' Month' end Duration,ItemID FROM InvItm " & _
                                         " LEFT JOIN VatMasterTb ON VatMasterTb.vatid=InvItm.vatId where itemCategory='course' ")

        grdItem.DataSource = dt
        With grdItem
            SetGridProperty(grdItem)
            .Columns("ItemID").Visible = False
            .Columns("Fees Amount").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Fees Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Fees + Tax").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Fees + Tax").DefaultCellStyle.Format = "N" & NoOfDecimal
        End With
    End Sub
    Private Sub ldRec(ByVal Id As Long)
        chgbyprg = True
        Dim found As Boolean
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT [Item Code],ItemID,Description,UnitPrice,invitm.vatid,isnull(vat,0)vat," & _
                                         "UnitPrice+((UnitPrice*isnull(vat,0))/100) taxprice," & _
                                         "isnull(courseduration,0)courseduration,isnull(totalfees,0)totalfees,vatcode,ishide,isnull(sylabus,'')sylabus FROM InvItm " & _
                                         "LEFT JOIN VatMasterTb ON VatMasterTb.vatid=InvItm.vatid " & _
                                         "WHERE ItemId = " & Id)
        For i = 0 To dt.Rows.Count - 1
            found = True
            txtCode.Text = dt(0)("Item Code")
            ItemId = dt(0)("ItemID")
            txtTrDescr.Text = Trim(dt(0)("Description") & "")
            numunitprice.Text = Format(dt(0)("UnitPrice"), numFormat)
            txtpriceWtax.Text = Format(dt(0)("taxprice"), numFormat)
            txtduration.Text = dt(0)("courseduration")
            txttotalfees.Text = Format(dt(0)("totalfees"), numFormat)
            cmbtax.Text = Trim(dt(0)("vatcode") & "")
            cmbtax.Tag = Val(dt(0)("vat") & "")
            txttotalTaxfees.Text = Format(CDbl(txttotalfees.Text) + (CDbl(txttotalfees.Text) * Val(cmbtax.Tag) / 100), numFormat)
            If Not IsDBNull(dt(0)("ishide")) Then
                chkhide.Checked = dt(0)("ishide")
            Else
                chkhide.Checked = False
            End If
            txtsylabus.Text = dt(0)("sylabus")
            txtCode.Focus()
        Next
        If Not found Then
            ItemId = 0
        End If
        chgbyprg = False
    End Sub
    Private Sub QuickItem_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ldvat()
        Timer1.Enabled = True
    End Sub
    Private Sub clearControls()
        For Each Control In Me.Controls
            If TypeOf (Control) Is TextBox Then
                Control.text = ""
                Control.tag = ""
            End If
        Next
        ItemId = 0
        chkhide.Checked = False
    End Sub
    Private Sub ldvat()
        Dim strqry As String = ""
        strqry = "SELECT vatcode FROM VatMasterTb"
        Dim dtTable As DataTable
        dtTable = _objcmnbLayer._fldDatatable(strqry)
        cmbtax.Items.Clear()
        If dtTable.Rows.Count > 0 Then
            For i = 0 To dtTable.Rows.Count - 1
                cmbtax.Items.Add(dtTable(i)("vatcode"))
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

    Private Sub txtCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCode.KeyDown, txtTrDescr.KeyDown, _
                                                                                                               numunitprice.KeyDown, _
                                                                                                              txtpriceWtax.KeyDown, txtduration.KeyDown, _
                                                                                                              txttotalfees.KeyDown, txttotalTaxfees.KeyDown
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
        If Val(txttotalfees.Text) = 0 Then txttotalfees.Text = 0
        _objcmnbLayer._saveDatawithOutParm("update invitm set courseduration=" & Val(txtduration.Text) & _
                                           ",totalfees=" & CDbl(txttotalfees.Text) & _
                                           ",ishide=" & IIf(chkhide.Checked, 1, 0) & ",sylabus='" & txtsylabus.Text & "' where itemid=" & BaseId)
        MsgBox("Item Created Successfully", MsgBoxStyle.Information)
        clearControls()
        loadItemdetails()
    End Sub
    Private Function chkFields() As Boolean
        Dim _vdtTable As DataTable
        If Trim(txtCode.Text) = "" Then
            MsgBox("'Item Code' Cannot be Blank !", MsgBoxStyle.Information)
            txtCode.Focus()
        End If
        _vdtTable = _objcmnbLayer._fldDatatable("Select ItemId from InvItm Where [Item Code] ='" & MkDbSrchStr(txtCode.Text) & "' and ItemId <>" & ItemId)
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
        _objItmMast.Unit = "PCS"
        If Val(numunitprice.Text) = 0 Then numunitprice.Text = 0
        _objItmMast.salesPrice = numunitprice.Text
        _objItmMast.Category = "Course"
        _objItmMast.WSalesPrice = 0
        _objItmMast.MRP = 0
        _objItmMast.hsncode = ""
        _objItmMast.vat = cmbtax.Text
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

        _objItmMast.Ismodi = IIf(ItemId = 0, False, True)
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
                loadItemdetails()
                resizeGridColumn(grdItem, 1)
            Case 1
                txtpriceWtax.Focus()
        End Select
    End Sub

    Private Sub calculateTaxFromUnitPrice(ByVal fromUnitpice As Boolean)
        chgbyprgAmt = True
        If Val(cmbtax.Tag & "") = 0 Then cmbtax.Tag = 0
        If Val(txttotalfees.Text) = 0 Then txttotalfees.Text = 0
        If Not fromUnitpice Then
            txttotalfees.Text = (CDbl(txtpriceWtax.Text) * 100) / (CDbl(cmbtax.Tag) + 100)
            txttotalfees.Text = Format(CDbl(numunitprice.Text), numFormat)
        Else
            txttotalTaxfees.Text = Format(CDbl(txttotalfees.Text) + ((CDbl(txttotalfees.Text) * CDbl(cmbtax.Tag)) / 100), numFormat)
        End If
        If Val(txtduration.Text) > 0 Then
            numunitprice.Text = Format(CDbl(txttotalfees.Text) / Val(txtduration.Text), numFormat)
            txtpriceWtax.Text = Format(CDbl(txttotalTaxfees.Text) / Val(txtduration.Text), numFormat)
        End If
        
        chgbyprgAmt = False
    End Sub

    Private Sub numunitprice_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numunitprice.TextChanged
        'numunitprice.Tag = CDbl(numunitprice.Text)
    End Sub

    Private Sub numunitprice_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles numunitprice.KeyPress, txtpriceWtax.KeyPress, _
        txttotalfees.KeyPress, txttotalTaxfees.KeyPress
        Dim myctr As TextBox
        myctr = sender
        If myctr.ReadOnly Then Exit Sub
        NumericTextOnKeypress(sender, e, chgbyprgAmt, numFormat)
    End Sub

    Private Sub cmbtax_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbtax.SelectedIndexChanged
        If chgbyprg Then Exit Sub
        Dim strqry As String = ""
        strqry = "SELECT vat FROM VatMasterTb where vatcode='" & cmbtax.Text & "'"
        Dim dtTable As DataTable
        dtTable = _objcmnbLayer._fldDatatable(strqry)
        If dtTable.Rows.Count > 0 Then
            cmbtax.Tag = dtTable(0)("vat")
        End If
        If Val(numunitprice.Text) > 0 Then calculateTaxFromUnitPrice(True)
    End Sub

    Private Sub txtpriceWtax_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpriceWtax.TextChanged

    End Sub

    Private Sub grdItem_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdItem.DoubleClick
        Dim itemid As Long
        itemid = grdItem.Item("itemid", grdItem.CurrentRow.Index).Value
        chgamt = False
        ldRec(itemid)
    End Sub

    Private Sub txttotalfees_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txttotalfees.Validated
        If Val(txttotalfees.Text) > 0 And chgamt Then calculateTaxFromUnitPrice(True)
        chgamt = False
    End Sub

    Private Sub txttotalTaxfees_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txttotalTaxfees.Validated
        If Val(txttotalTaxfees.Text) > 0 And chgamt Then calculateTaxFromUnitPrice(False)
        chgamt = False
    End Sub

    Private Sub txttotalfees_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txttotalfees.TextChanged
        If chgbyprg Then Exit Sub
        chgamt = True
    End Sub

    Private Sub txttotalTaxfees_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txttotalTaxfees.TextChanged
        If chgbyprg Then Exit Sub
        chgamt = True
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        clearControls()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        Dim dttable As DataTable
        dttable = _objcmnbLayer._fldDatatable("SELECT itemid FROM INVITM WHERE opQty >0 AND itemid =" & Val(ItemId))
        If dttable.Rows.Count > 0 Then
            MsgBox("OPENING QTY Exist !", MsgBoxStyle.Information, "Cannot Delete !")
            Exit Sub
        End If
        dttable.Clear()
       
        dttable = _objcmnbLayer._fldDatatable("SELECT ITEMID FROM ITMINVTRTB WHERE itemid =" & Val(ItemId) & " UNION ALL SELECT ITEMID FROM JobitemTb WHERE itemid =" & Val(ItemId))
        If dttable.Rows.Count > 0 Then
            MsgBox("Transaction Exist !", MsgBoxStyle.Information, "Cannot Delete !")
        Else
            If MsgBox("Are You Sure to Remove The Item!", MsgBoxStyle.YesNo + MsgBoxStyle.Information) = MsgBoxResult.No Then Exit Sub
            _objcmnbLayer._saveDatawithOutParm("DELETE FROM INVITM WHERE itemid=" & Val(ItemId))
            _objcmnbLayer._saveDatawithOutParm("DELETE FROM ItemRawMeterialTb WHERE itemid=" & Val(ItemId))
            _objcmnbLayer._saveDatawithOutParm("DELETE FROM InvItmPropertiesTb WHERE itemid=" & Val(ItemId))
            _objcmnbLayer._saveDatawithOutParm("delete from BatchTb where Itemid=" & Val(ItemId) & " and batchTrid=0")
            clearControls()
        End If
    End Sub
End Class