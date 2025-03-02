Public Class WhatsaapFrm
    Private _objcmnbLayer As New clsCommon_BL
    Public Jobtype As String
    Private Sub btnsend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsend.Click
        senMessage()
    End Sub
    Private Sub senMessage()
        Try
            If txtcontent.Text = "" Then
                MsgBox("Add Content", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            If txtphone.Text = "" Then
                MsgBox("Invalid Phone", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            Dim web As New WebBrowser
            Dim messageString As String
            Dim messageText As String = txtpreview.Text

            messageString = messageText.Replace(vbCrLf, "%0A")
            messageString = messageString.Replace("&", "%26")
            messageString = messageString.Replace(" ", "+") & ""
            Dim phone As String
            If Len(txtphone.Text) = 10 Then
                phone = "91" & txtphone.Text
            Else
                phone = txtphone.Text
            End If
            web.Navigate("whatsapp://send?phone=" & phone & "&text=")
            Threading.Thread.Sleep(500)
            web.Focus()
            web.Navigate("whatsapp://send?phone=" & phone & "&text=" & messageString)
            Threading.Thread.Sleep(1000)
            'SendKeys.Send("{TAB}")
            'Threading.Thread.Sleep(5000)
            SendKeys.Send("{ENTER}")
            Me.Close()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Private Sub WhatsaapFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loadTemplate()
    End Sub
    Private Sub loadTemplate()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("select templatename from WhatAppTb")
        cmbtemplate.Items.Clear()
        Dim i As Integer
        For i = 0 To dt.Rows.Count - 1
            cmbtemplate.Items.Add(dt(i)("templatename"))
        Next
    End Sub

    Private Sub cmbtemplate_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbtemplate.SelectedIndexChanged
        Dim dt As DataTable
        cmbtemplate.Tag = ""
        dt = _objcmnbLayer._fldDatatable("select tempid,templateText from WhatAppTb where templatename='" & cmbtemplate.Text & "'")
        If dt.Rows.Count > 0 Then
            txtcontent.Text = dt(0)("templateText")
            cmbtemplate.Tag = dt(0)("tempid")
        End If
        previewmessage()
        
        'richTextBox1.AppendText("your text");
    End Sub
    Private Sub previewmessage()
        If Val(txtreceived.Text) = 0 Then txtreceived.Text = Format(0, numFormat)
        Dim messageText As String = ""
        Dim startmessage As String = "Hello " & txtparty.Text & "," & vbCrLf
        If chkisjobcode.Checked = True Then
            messageText = messageText & vbCrLf & Jobtype & " : " & txtjobcode.Text
        End If
        If chkoutstanding.Checked = False Then
            messageText = messageText & vbCrLf & "Amount   : " & txtamount.Text
            messageText = messageText & vbCrLf & "Received  : " & Format(CDbl(txtreceived.Text), numFormat)
        End If
        If chkisamount.Checked = True Then
            Dim balance As Double
            If Val(txtamount.Text) = 0 Then txtamount.Text = 0
            If Val(txtreceived.Text) = 0 Then txtreceived.Text = 0
            balance = CDbl(txtamount.Text) - CDbl(txtreceived.Text)
            messageText = messageText & vbCrLf & "*Balance    : " & Format(balance, numFormat) & "*"
        End If
       
        txtpreview.Text = startmessage & txtcontent.Text & vbCrLf & messageText
    End Sub

    Private Sub btnset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnset.Click
        If Val(cmbtemplate.Tag) > 0 Then
            _objcmnbLayer._saveDatawithOutParm("Update WhatAppTb set templateText=N'" & txtcontent.Text & "' WHERE tempid=" & Val(cmbtemplate.Tag))
        Else
            _objcmnbLayer._saveDatawithOutParm("Insert into WhatAppTb(templatename,templateText) values('" & cmbtemplate.Text & "',N'" & txtcontent.Text & "')")
        End If
        MsgBox("Saved", MsgBoxStyle.Information)
    End Sub

    Private Sub btnremove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnremove.Click
        If MsgBox("Do you want remove template?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        _objcmnbLayer._saveDatawithOutParm("delete from WhatAppTb  WHERE tempid=" & Val(cmbtemplate.Tag))
        txtcontent.Text = ""
        cmbtemplate.Text = ""
        cmbtemplate.Tag = ""
        loadTemplate()
    End Sub

    Private Sub cmbtemplate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbtemplate.TextChanged
        cmbtemplate.Tag = ""
    End Sub

    Private Sub btnexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexit.Click
        Me.Close()
    End Sub

    Private Sub txtcontent_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcontent.TextChanged
        previewmessage()
    End Sub
End Class