Public Class InvoicePercentage
#Region "NumericText"
    'numeric text
    Private idx As Integer
    Private str1 As String
    Private str2 As String
    Private str3 As String
    Private SelStart As Integer
    Private numCtrl As TextBox
#End Region
    Private chgbyprg As Boolean
    Public Event retunAmt(ByVal amt As Double)

    Private Sub numAmt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles numAmt.KeyDown
        If e.KeyCode = Keys.Return Then
            closeAmt()
        End If
    End Sub
    Private Sub numAmt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles numAmt.KeyPress
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

    Private Sub btnok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnok.Click
        closeAmt()
    End Sub
    Private Sub closeAmt()
        If Val(lbltobeinvoiced.Text) = 0 Then lbltobeinvoiced.Text = 0
        If Val(lblInvoiced.Text) = 0 Then lblInvoiced.Text = 0
        If CDbl(lbltobeinvoiced.Text) < CDbl(lblInvoiced.Text) Then
            MsgBox("Amount Greater than QTN Amount is not allowed", MsgBoxStyle.Exclamation)
            numAmt.Focus()
            Exit Sub
        End If
        If Val(lblInvoiced.Text) = 0 Then lblInvoiced.Text = 0
        RaiseEvent retunAmt(CDbl(lblInvoiced.Text))
        Me.Close()
    End Sub

    Private Sub numAmt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numAmt.TextChanged
        If Val(lbltobeinvoiced.Text) = 0 Then lbltobeinvoiced.Text = 0
        If rdoPer.Checked Then
            lblInvoiced.Text = Format(CDbl(lblinvoice.Text) * CDbl(numAmt.Text) / 100, numFormat)
        Else
            lblInvoiced.Text = Format(CDbl(numAmt.Text), numFormat)
        End If
    End Sub
End Class