
Public Class SendSMSFrm
    Private _objTr As New clsSMS
    Private _objcmnbLayer As New clsCommon_BL
    Private _objweb As New webDatalayer
    Private dtTable As DataTable
    
    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        loadAccounts()
        loadformats()
    End Sub
    Private Sub loadAccounts()
        dtTable = _objTr.returnCustomerlistForSMS(0, cmbcategory.Text)
        grdvoucher.DataSource = dtTable
        SetGridHead()
    End Sub
    Private Sub SetGridHead()
        SetGridProperty(grdvoucher)
        With grdvoucher
            .Columns(0).HeaderText = "Tag"
            .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(0).Width = 50

            .Columns(2).Width = 100
        End With
        Dim i As Integer
        For i = 0 To grdvoucher.Columns.Count - 1
            cmbitmOrder.Items.Add(grdvoucher.Columns(i).HeaderText)
        Next
        cmbitmOrder.SelectedIndex = 1
        resizeGridColumn(grdvoucher, 1)
    End Sub
    Private Function sendSMS(ByVal smsNumber As String) As String
        Dim sms As New sendSMS
        'smsNumber = "919526794529,918156932691"
        Dim result As String = sms.sendSMSFn(smsNumber, txtcontent.Text)
        result = Mid(result, result.IndexOf("status"))
        result = Mid(result, result.IndexOf(":") + 3)
        result = Mid(result, result.IndexOf(":") + 2)
        result = Mid(result, 1, result.IndexOf(""""))
        Return result
    End Function
    Private Sub saveSmsTransaction(ByVal smsNumber As String, ByVal receivername As String)
        _objcmnbLayer._saveDatawithOutParm("INSERT INTO smsTransactionTb (smsNumber,formatid,smsDateTime,receivername) VALUES(" & _
                                           "'" & smsNumber & "'," & cmbformat.Tag & ",GETDATE(),'" & receivername & "')")

    End Sub

    Private Sub SendSMSFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbcategory.SelectedIndex = 0
        loadformats()
        getSMSKey()
        getSMSCount()
    End Sub

    Private Sub btnaddtemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddtemplate.Click
        If Val(cmbformat.Tag) = 0 Then
            Dim name As String = InputBox("Enter Format Name", , "")
            If name <> "" Then
                _objcmnbLayer._saveDatawithOutParm("INSERT INTO SMSFormatTb (formatname,smscontent) VALUES(" & _
                                              "'" & name & "','" & txtcontent.Text & "')")
            End If
        Else
            If MsgBox("Do you want edit selected format?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                _objcmnbLayer._saveDatawithOutParm("Update SMSFormatTb set formatname='" & cmbformat.Text & "'," & _
                                                   "smscontent='" & txtcontent.Text & "' where id=" & Val(cmbformat.Tag))
            End If
        End If
        MsgBox("Updated", MsgBoxStyle.Information)
        loadformats()
    End Sub
    Private Sub loadformats()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("select formatname from SMSFormatTb")
        cmbformat.Items.Clear()
        cmbformat.Items.Add("")
        Dim i As Integer
        For i = 0 To dt.Rows.Count - 1
            cmbformat.Items.Add(dt(0)(0))
        Next
    End Sub

    Private Sub cmbformat_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbformat.SelectedIndexChanged
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("select * from SMSFormatTb where formatname='" & cmbformat.Text & "'")
        If dt.Rows.Count > 0 Then
            txtcontent.Text = dt(0)("smscontent")
            cmbformat.Tag = dt(0)("id")
        Else
            txtcontent.Text = ""
            cmbformat.Tag = ""
        End If
    End Sub

    Private Sub btnexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexit.Click
        Me.Close()
    End Sub

    Private Sub btnsend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsend.Click
        If Val(lblsmsremaining.Tag) <= Val(lblsmsTobesend.Tag) Then
            MsgBox("SMS Count Remaining only : " & Val(lblsmsremaining.Tag), MsgBoxStyle.Information)
            Exit Sub
        End If
        If txtcontent.Text = "" Then
            MsgBox("Invalid Message", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Dim i As Integer

        Dim phno As String = ""
        Dim smscount As Integer
        For i = 0 To grdvoucher.RowCount - 1
            If grdvoucher.Item(0, i).Value = "Y" Then
                If Trim(grdvoucher.Item(2, i).Value & "") <> "" Then
                    'isFound = True
                    'Threading.Thread.Sleep(1000)
                    If phno = "" Then
                        phno = Trim(grdvoucher.Item(2, i).Value & "")
                    Else
                        phno = phno & "," & Trim(grdvoucher.Item(2, i).Value & "")
                    End If
                    smscount = smscount + 1
                End If
            End If
        Next
        Dim result As String = ""
        If smscount > 0 Then
            Me.Cursor = Cursors.WaitCursor
            result = sendSMS(phno)
        Else
            MsgBox("Select atleast one recipient", MsgBoxStyle.Exclamation)
            GoTo ext
        End If
        Dim msgstatus As Boolean
        If Trim(result & "") <> "failure" And Trim(result & "") <> "" Then
            For i = 0 To grdvoucher.RowCount - 1
                If grdvoucher.Item(0, i).Value = "Y" Then
                    If Trim(grdvoucher.Item(2, i).Value & "") <> "" Then
                        saveSmsTransaction(Trim(grdvoucher.Item(2, i).Value & ""), Trim(grdvoucher.Item(1, i).Value & ""))
                    End If
                End If
            Next
            msgstatus = True
        End If
        If msgstatus Then
            MsgBox("Message sent (" & result & ")", MsgBoxStyle.Information)
        Else
            MsgBox("Failure to send message", MsgBoxStyle.Exclamation)
        End If
        _objweb.saveDataToOnline("Update SMSCntTb set smssent=isnull(smssent,0)+" & smscount & " where id=" & Val(btnsend.Tag))
        getSMSCount()
        chkselectall.Checked = False
        selectAll()
ext:
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub grdvoucher_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdvoucher.CellClick
        With grdvoucher
            If e.ColumnIndex = 0 Then
                If e.RowIndex < 0 Then
                    'selectAll()
                Else
                    If Trim(.Item(2, e.RowIndex).Value & "") = "" Or Len(Trim(.Item(2, e.RowIndex).Value & "")) <> 10 Or Val(.Item(2, e.RowIndex).Value & "") = 0 Then
                        MsgBox("Invalid Phone Number", MsgBoxStyle.Exclamation)
                        .Item(0, e.RowIndex).Value = ""
                    Else
                        .Item(0, e.RowIndex).Value = IIf(.Item(0, e.RowIndex).Value = "Y", "", "Y")
                    End If
                End If
            End If
        End With
        countSelected()
    End Sub
    Private Sub countSelected()
        lblsmsTobesend.Tag = 0
        Dim i As Integer
        For i = 0 To grdvoucher.RowCount - 1
            With grdvoucher
                If .Item(0, i).Value = "Y" Then
                    lblsmsTobesend.Tag = Val(lblsmsTobesend.Tag) + 1
                End If
            End With
        Next
        lblsmsTobesend.Text = "Selected : " & lblsmsTobesend.Tag
    End Sub
    Private Sub selectAll()
        lblsmsTobesend.Tag = 0
        With grdvoucher
            Dim i As Integer
            For i = 0 To .RowCount - 1
                If chkselectall.Checked Then
                    If Trim(dtTable(i)(2) & "") <> "" And Len(Trim(dtTable(i)(2) & "")) = 10 And Val(dtTable(i)(2) & "") > 0 Then
                        dtTable(i)(0) = "Y"
                    Else
                        dtTable(i)(0) = ""
                    End If
                Else
                    '.Item(0, i).Value = ""
                    dtTable(i)(0) = ""
                End If
                If .Item(0, i).Value = "Y" Then
                    lblsmsTobesend.Tag = Val(lblsmsTobesend.Tag) + 1
                End If
            Next
        End With
        lblsmsTobesend.Text = "Selected : " & lblsmsTobesend.Tag
    End Sub


    Private Sub chkselectall_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkselectall.Click
        selectAll()
    End Sub
    Private Sub getSMSKey()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("select isnull(smsActiveId,0) from CompanyTb")
        If dt.Rows.Count > 0 Then
            btnsend.Tag = dt(0)(0)
        End If
    End Sub
    Private Sub getSMSCount()
        Dim dt As DataTable
        _objweb.isSms = True
        dt = _objweb.returnDatatable("select isnull(smscount,0)smscount,isnull(smssent,0)smssent,isnull(status,0)status from SMSCntTb where id=" & Val(btnsend.Tag))
        Dim smsBal As Integer
        If dt.Rows.Count > 0 Then
            smsBal = dt(0)(0) - dt(0)(1)
            If dt(0)(2) = "True" Then
                lblstaus.Text = "Active"
                lblstaus.ForeColor = Color.Green
                btnsend.Enabled = True
            Else
                GoTo els
            End If
        Else
els:
            lblstaus.Text = "Inactive"
            lblstaus.ForeColor = Color.Red
            btnsend.Enabled = False
        End If
        lblsmsremaining.Text = "Remaining : " & smsBal
        lblsmsremaining.Tag = smsBal
    End Sub

    Private Sub txtcontent_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcontent.TextChanged
        lblcharector.Text = "Charector Remaining : " & 120 - Len(txtcontent.Text) & " / 120"
    End Sub

    Private Sub bntloadtr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bntloadtr.Click
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("select * from (select smsDateTime [Date],smsNumber [Phone Number],receivername [Receiver],formatname [Message Format],CAST(smsDateTime AS date) smsDateTime from smsTransactionTb " & _
                                         "LEFT JOIN SMSFormatTb ON smsTransactionTb.formatid=SMSFormatTb.Id)tr " & _
                                         "where smsDateTime>='" & Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd") & "' and smsDateTime<='" & Format(DateValue(cldrEnddate.Value), "yyyy/MM/dd") & "'")
        grdlist.DataSource = dt
        SetGridProperty(grdlist)
        resizeGridColumn(grdlist, 2)
    End Sub

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        Dim dr As DataRow
        If dtTable Is Nothing Then
            dtTable = _objcmnbLayer._fldDatatable("select top 1 '' Tag, '' [Party Name],'' Phone from AccMast")
            dtTable.Rows.Clear()
            grdvoucher.DataSource = dtTable
            SetGridHead()
        End If
        dr = dtTable.NewRow
        dr("tag") = ""
        dr("Party Name") = "Unknown"
        dr("Phone") = txtphone.Text
        dtTable.Rows.Add(dr)
    End Sub

    Private Sub txtitemSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtitemSearch.TextChanged
        grdvoucher.DataSource = SearchGrid(dtTable, Trim(txtitemSearch.Text), cmbitmOrder.SelectedIndex, Not chkitemSearchOnly.Checked)
        SetGridHead()
    End Sub

    Private Sub chkselectall_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkselectall.CheckedChanged

    End Sub

    Private Sub grdvoucher_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdvoucher.CellContentClick

    End Sub
End Class