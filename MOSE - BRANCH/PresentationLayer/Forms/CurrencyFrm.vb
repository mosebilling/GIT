
Public Class CurrencyFrm
#Region "Local Variables"
    Private chgbyprg As Boolean
    'numeric text
    Private idx As Integer
    Private str1 As String
    Private str2 As String
    Private str3 As String
    Private SelStart As Integer
    Private numCtrl As TextBox
    Private nSelect As Integer
    Private LddImpDocs As String
    Private OthCost As Double
    Private FCRt As Double
    Private chgCurr As Boolean
    Private NDec As Byte
    Private chgDoc As Boolean
    Private indexHead As Short
#End Region
#Region "Class Objects"
    Private _objCurrency As clsCurrencyBL
    Private _objcmnbLayer As clsCommon_BL
#End Region

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub txtPhone_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles numrate.KeyPress, numdecimal.KeyPress
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
    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        If txtcurrencyCode.Text = "" Then
            MsgBox("Invalid Currency Code!")
            txtcurrencyCode.Focus()
            Exit Sub
        End If
        Dim dt As DataTable
        _objcmnbLayer = New clsCommon_BL
        dt = _objcmnbLayer._fldDatatable("Select CurrencyId from CurrencyTb where CurrencyCode='" & txtcurrencyCode.Text & "'")
        If dt.Rows.Count > 0 Then
            If dt(0)("CurrencyId") <> Val(txtcurrencyCode.Tag) Then
                MsgBox("Currency Code already exist", MsgBoxStyle.Exclamation)
                txtcurrencyCode.Focus()
                Exit Sub
            End If
        End If
        dt = _objcmnbLayer._fldDatatable("Select CurrencyId from CurrencyTb where Description='" & txtname.Text & "'")
        If dt.Rows.Count > 0 Then
            If dt(0)("CurrencyId") <> Val(txtcurrencyCode.Tag) Then
                MsgBox("Currency name already exist", MsgBoxStyle.Exclamation)
                txtname.Focus()
                Exit Sub
            End If
        End If
        _objCurrency = New clsCurrencyBL
        With _objCurrency
            .CurrencyId = Val(txtcurrencyCode.Tag)
            .CurrencyCode = txtcurrencyCode.Text
            .Description = txtname.Text
            .FractionCode = txtfraction.Text
            If Val(numrate.Text) = 0 Then numrate.Text = 0
            .CurrencyRate = CDbl(numrate.Text)
            .DecimalPlaces = Val(numdecimal.Text)
            .saveCurrency()
            makeClear()
            isnewCurrencyAdded = True
            LOADCurrency()
        End With
    End Sub
    Sub LOADCurrency()
        Dim ListTb As DataTable
        Dim i As Integer
        _objCurrency = New clsCurrencyBL
        _objcmnbLayer = New clsCommon_BL
        With _objCurrency
            .CurrencyId = 0
            .TP = 1
            ListTb = .retrunCurrency()
        End With
        dtcurrentyTb = _objcmnbLayer._fldDatatable("SELECT CurrencyCode FROM CurrencyTb")
        With lstContent
            .Items.Clear()
            If ListTb.Rows.Count > 0 Then
                For i = 0 To ListTb.Rows.Count - 1
                    .Items.Add(ListTb(i)("CurrencyCode"))
                    If .Items.Item(i).SubItems.Count > 1 Then
                        .Items.Item(i).SubItems(1).Text = .Items.Add(ListTb(i)("Description"))
                    Else
                        .Items.Item(i).SubItems.Insert(1, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, ListTb(i)("Description")))
                    End If
                    If .Items.Item(i).SubItems.Count > 2 Then
                        .Items.Item(i).SubItems(2).Text = ListTb(i)("Fraction Code")
                    Else
                        .Items.Item(i).SubItems.Insert(2, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, ListTb(i)("Fraction Code")))
                    End If
                    If .Items.Item(i).SubItems.Count > 3 Then
                        .Items.Item(i).SubItems(3).Text = ListTb(i)("CurrencyId")
                    Else
                        .Items.Item(i).SubItems.Insert(3, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, ListTb(i)("CurrencyId")))
                    End If
                Next
            End If
        End With
    End Sub

    Private Sub lstContent_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstContent.DoubleClick
        Dim dt As DataTable
        _objCurrency = New clsCurrencyBL
        With _objCurrency
            .CurrencyId = Val(lstContent.SelectedItems(0).SubItems(3).Text)
            .TP = 0
            dt = .retrunCurrency
        End With
        If dt.Rows.Count > 0 Then
            txtcurrencyCode.Text = dt(0)("CurrencyCode")
            txtcurrencyCode.Tag = dt(0)("CurrencyId")
            txtname.Text = dt(0)("Description")
            numrate.Text = dt(0)("CurrencyRate")
            txtfraction.Text = dt(0)("Fraction Code")
            numdecimal.Text = dt(0)("Decimal Places")
        End If
    End Sub


    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        If lstContent.SelectedItems.Count = 0 Then Exit Sub
        If MsgBox("Do you want to remove " & lstContent.SelectedItems(0).SubItems(1).Text, MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.No Then Exit Sub
        _objcmnbLayer = New clsCommon_BL
        _objcmnbLayer._saveDatawithOutParm("DELETE FROM CurrencyTb WHERE CurrencyId=" & Val(txtcurrencyCode.Tag))
        makeClear()
        LOADCurrency()
    End Sub

    Private Sub txtcurrencyCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtcurrencyCode.KeyDown, txtfraction.KeyDown, txtname.KeyDown, numdecimal.KeyDown, numrate.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub
    Private Sub makeClear()
        txtcurrencyCode.Text = ""
        txtcurrencyCode.Tag = ""
        txtname.Text = ""
        txtfraction.Text = ""
        numrate.Text = "0.00"
        numdecimal.Text = "0.00"
        txtcurrencyCode.Focus()
    End Sub

    Private Sub CurrencyFrm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtcurrencyCode.Focus()
    End Sub

    Private Sub CurrencyFrm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LOADCurrency()
    End Sub

    Private Sub lstContent_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstContent.SelectedIndexChanged

    End Sub

    Private Sub txtcurrencyCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcurrencyCode.TextChanged

    End Sub
End Class