
Public Class CreatePackList
    'object variable
    Dim _objcmnbLayer As New clsCommon_BL
    Dim _objItmMast As New clsItemMast_BL
    '****************
    Public Event meUpdate()
    Public baseId As Long
    Public IsModi As Boolean
    'numeric text
    Dim idx As Integer
    Dim str1 As String
    Dim str2 As String
    Dim str3 As String
    Dim SelStart As Integer
    Dim numCtrl As TextBox
    '*************
    Private chgbyprg As Boolean

    Private Sub CreatePackList_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        cmbUnit.Focus()
    End Sub
    Private Sub CreatePackList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ldUnits()
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
        If cmbUnit.Items.Count > 0 Then cmbUnit.SelectedIndex = 0
        If lblunit.Tag <> "" Then cmbUnit.Text = lblunit.Tag
    End Sub

    Private Sub BtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub

    Private Sub BtnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUpdate.Click
        'If Not chkFields() Then Exit Sub
        If lblbu.Tag = "Medium Unit" Then
            _objcmnbLayer._saveDatawithOutParm("UPDATE invitm set P1Unit='" & cmbUnit.Text & "',P1Fra=" & CDbl(txtbu.Text) & " where ItemId=" & Val(cmbUnit.Tag))
        Else
            _objcmnbLayer._saveDatawithOutParm("UPDATE InvItm set P2Unit='" & IIf(Val(txtbu.Text) = 0, "", cmbUnit.Text) & "',P2Fra=" & CDbl(txtbu.Text) & " where ItemId=" & Val(cmbUnit.Tag))
        End If
        MsgBox("Item PackCode saved successfully", MsgBoxStyle.Information)
        RaiseEvent meUpdate()
        Me.Close()
    End Sub
    Private Function chkFields() As Boolean

        If Val(txtbu.Text) = 0 Then
            MsgBox("Invalid Unit Fraction", MsgBoxStyle.Critical)
            Exit Function
        End If
        
        chkFields = True
    End Function
    Private Sub makeClear()
        For Each Control In GroupBox2.Controls
            If TypeOf (Control) Is TextBox Then
                Control.text = ""
                Control.tag = ""
            End If
            If TypeOf (Control) Is CheckBox Then
                Control.checked = False
                Control.tag = ""
            End If
        Next
    End Sub

    Private Sub txtCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles _
                                                                                                                 txtbu.KeyDown, _
                                                                                                                txtpu.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub
    

    Private Sub numPrice_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles numPrice.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub numPrice_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles numPrice.KeyPress, numWprice.KeyPress
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
            If Len(Trim(str1)) > 12 And Len(numCtrl.Text) > 0 Then
                If Mid(Mid(Mid(numCtrl.Text, 1, idx), Len(Trim(str1)) + 1 - 12), 1, 1) = "," Then
                    str3 = Mid(Mid(numCtrl.Text, 1, idx), Len(Trim(str1)) + 2 - 12)
                End If
                numCtrl.Text = Mid(str1, Len(Trim(str1)) + 1 - 12) & str2
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


    Private Sub cmbUnit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbUnit.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtbu_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtbu.KeyPress
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
            If Len(Trim(str1)) > 12 And Len(numCtrl.Text) > 0 Then
                If Mid(Mid(Mid(numCtrl.Text, 1, idx), Len(Trim(str1)) + 1 - 12), 1, 1) = "," Then
                    str3 = Mid(Mid(numCtrl.Text, 1, idx), Len(Trim(str1)) + 2 - 12)
                End If
                numCtrl.Text = Mid(str1, Len(Trim(str1)) + 1 - 12) & str2
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
            numCtrl.Text = Format(Val(numCtrl.Text), "#,##0")
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

    Private Sub txtbu_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtbu.TextChanged

    End Sub
End Class