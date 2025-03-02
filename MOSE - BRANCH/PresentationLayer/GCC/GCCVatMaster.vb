Public Class GCCVatMaster
    Private WithEvents fSelect As Selectfrm
    Private srchIndex As Single
    Dim _objcmnbLayer As New clsCommon_BL
    Private chgbyprg As Boolean
#Region "NumericText"
    'numeric text
    Private idx As Integer
    Private str1 As String
    Private str2 As String
    Private str3 As String
    Private SelStart As Integer
    Private numCtrl As TextBox
#End Region
    Private Sub VatMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loadVatmaster()
    End Sub
    Private Sub ldSelect(ByVal BVal As Single)
        fSelect = New Selectfrm
        fSelect.Location = New Point(650, 150)
        fSelect.StartPosition = FormStartPosition.CenterScreen
        SetForm(fSelect, BVal)
        fSelect.Show()
    End Sub

    Private Sub fSelect_doSelect(ByVal KeyId As String, ByVal strFld1 As String, ByVal strFld2 As String) Handles fSelect.doSelect
        If srchIndex = 0 Then
            txtcollectionAc.Text = strFld1
            txtcollectionAc.Tag = KeyId
        ElseIf srchIndex = 1 Then
            txtpaymetac.Text = strFld1
            txtpaymetac.Tag = KeyId
        ElseIf srchIndex = 2 Then
            txtoutputimport.Text = strFld1
            txtoutputimport.Tag = KeyId
        ElseIf srchIndex = 3 Then
            txtvatinputimport.Text = strFld1
            txtvatinputimport.Tag = KeyId
        End If

    End Sub

    Private Sub txtcollectionAc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtcollectionAc.KeyDown, txtpaymetac.KeyDown, _
                                                                                                                        txtcode.KeyDown, txtname.KeyDown, txtvat.KeyDown, txtoutputimport.KeyDown, txtvatinputimport.KeyDown
        Dim myctrl As TextBox
        myctrl = sender
        If e.KeyCode = Keys.F2 Then
            Select Case myctrl.Name
                Case "txtcollectionAc", "txtpaymetac", "txtoutputimport", "txtvatinputimport"
                    If myctrl.Name = "txtcollectionAc" Then
                        srchIndex = 0
                    ElseIf myctrl.Name = "txtpaymetac" Then
                        srchIndex = 1
                    ElseIf myctrl.Name = "txtoutputimport" Then
                        srchIndex = 2
                    ElseIf myctrl.Name = "txtvatinputimport" Then
                        srchIndex = 3
                    End If
                    ldSelect(2)
            End Select
        ElseIf e.KeyCode = Keys.Delete Or e.KeyCode = Keys.Back Then
            Select Case myctrl.Name
                Case "txtcollectionAc", "txtpaymetac", "txtoutputimport", "txtvatinputimport"
                    myctrl.Text = ""
                    myctrl.Tag = ""
            End Select
        ElseIf e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub fSelect_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fSelect.FormClosed
        fSelect = Nothing
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        If txtcode.Text = "" Then
            MsgBox("Invalid Vat Code", MsgBoxStyle.Exclamation)
            txtcode.Focus()
            Exit Sub
        ElseIf txtname.Text = "" Then
            MsgBox("Invalid Vat Code", MsgBoxStyle.Exclamation)
            txtname.Focus()
            Exit Sub
        ElseIf Val(txtvat.Text) = 0 Then
            MsgBox("Invalid Vat %", MsgBoxStyle.Exclamation)
            txtvat.Focus()
            Exit Sub
        End If
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("select vatid from VatMasterTb where vatcode='" & txtcode.Text & "' and vatid<>" & Val(txtcode.Tag))
        If dt.Rows.Count > 0 Then
            MsgBox("Vat Code already exist", MsgBoxStyle.Exclamation)
            txtcode.Focus()
            Exit Sub
        End If
        dt = _objcmnbLayer._fldDatatable("select vatid from VatMasterTb where vatname='" & txtname.Text & "' and vatid<>" & Val(txtcode.Tag))
        If dt.Rows.Count > 0 Then
            MsgBox("Vat  Name already exist", MsgBoxStyle.Exclamation)
            txtname.Focus()
            Exit Sub
        End If
        saveRec()
    End Sub
    Private Sub saveRec()
        If Val(txtcode.Tag) > 0 Then
            _objcmnbLayer._saveDatawithOutParm("UPDATE VatMasterTb SET vatcode='" & txtcode.Text & "',vatname ='" & txtname.Text & _
                                               "',collectionAC=" & Val(txtcollectionAc.Tag & "") & " ,paymentAC=" & Val(txtpaymetac.Tag & "") & _
                                               ",OutpurtvatImport=" & Val(txtoutputimport.Tag & "") & " ,InputvatImport=" & Val(txtvatinputimport.Tag & "") & _
                                               ",vat=" & CDbl(txtvat.Text) & _
                                               " WHERE vatid=" & Val(txtcode.Tag))
        Else
            _objcmnbLayer._saveDatawithOutParm("INSERT INTO VatMasterTb(vatcode,vatname,collectionAC,paymentAC,OutpurtvatImport,InputvatImport,vat) VALUES ('" & _
                                               txtcode.Text & "','" & _
                                               txtname.Text & "'," & _
                                               Val(txtcollectionAc.Tag & "") & "," & _
                                               Val(txtpaymetac.Tag & "") & "," & _
                                               Val(txtoutputimport.Tag & "") & "," & _
                                               Val(txtvatinputimport.Tag & "") & "," & _
                                               CDbl(txtvat.Text) & ")")
        End If
        MsgBox("Vat Master updated successfully", MsgBoxStyle.Information)
        makeClear()
        loadVatmaster()
    End Sub
    Private Sub makeClear()
        chgbyprg = True
        txtcode.Text = ""
        txtcode.Tag = ""
        txtname.Text = ""
        txtcollectionAc.Text = ""
        txtcollectionAc.Tag = ""
        txtpaymetac.Tag = ""
        txtpaymetac.Text = ""
        txtvat.Text = "0.00"
        txtvatinputimport.Text = ""
        txtvatinputimport.Tag = ""
        txtoutputimport.Text = ""
        txtoutputimport.Tag = ""
        chgbyprg = False
    End Sub
    Private Sub loadVatmaster()
        Try


            Dim ListTb As DataTable
            ListTb = _objcmnbLayer._fldDatatable("SELECT vatcode,vatname,ISNULL(clsName,'')clsName,isnull(PName,'')PName,ISNULL(ImportOutName,'')ImportOutName,isnull(ImportInName,'')ImportInName," & _
                                                 "ISNULL(collectionAC,0)collectionAC,ISNULL(paymentAC,0)paymentAC,ISNULL(IOId,0)IOId,ISNULL(IIId,0)IIId,vatid,vat,ISNULL(OutpurtvatImport,0)OutpurtvatImport,ISNULL(InputvatImport,0)InputvatImport " & _
                                                 "from VatMasterTb LEFT JOIN (SELECT ACCDESCR clsName,Accid cId FROM ACCMAST)cls " & _
                                                 " ON VatMasterTb.collectionAC=CLS.Cid " & _
                                                 "LEFT JOIN (SELECT ACCDESCR PName,Accid pId FROM ACCMAST)Pac ON VatMasterTb.paymentAC=Pac.pid " & _
                                                 "LEFT JOIN (SELECT ACCDESCR ImportOutName,Accid IOId FROM ACCMAST)IOac ON  VatMasterTb.OutpurtvatImport=IOac.IOId " & _
                                                 "LEFT JOIN (SELECT ACCDESCR ImportInName,Accid IIId FROM ACCMAST)IIac ON  VatMasterTb.InputvatImport=IIac.IIId ")
            With lstContent
                .Items.Clear()
                If ListTb.Rows.Count > 0 Then
                    For i = 0 To ListTb.Rows.Count - 1
                        .Items.Add(ListTb(i)("vatcode"))
                        If .Items.Item(i).SubItems.Count > 1 Then
                            .Items.Item(i).SubItems(1).Text = .Items.Add(ListTb(i)("vatname"))
                        Else
                            .Items.Item(i).SubItems.Insert(1, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, ListTb(i)("vatname")))
                        End If
                        If .Items.Item(i).SubItems.Count > 2 Then
                            .Items.Item(i).SubItems(2).Text = .Items.Add(ListTb(i)("vat"))
                        Else
                            .Items.Item(i).SubItems.Insert(2, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, ListTb(i)("vat")))
                        End If
                        If .Items.Item(i).SubItems.Count > 3 Then
                            .Items.Item(i).SubItems(3).Text = .Items.Add(ListTb(i)("clsName"))
                        Else
                            .Items.Item(i).SubItems.Insert(3, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, Trim(ListTb(i)("clsName") & "")))
                        End If

                        If .Items.Item(i).SubItems.Count > 4 Then
                            .Items.Item(i).SubItems(4).Text = .Items.Add(ListTb(i)("PName"))
                        Else
                            .Items.Item(i).SubItems.Insert(4, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, ListTb(i)("PName")))
                        End If

                        If .Items.Item(i).SubItems.Count > 5 Then
                            .Items.Item(i).SubItems(5).Text = .Items.Add(ListTb(i)("ImportOutName"))
                        Else
                            .Items.Item(i).SubItems.Insert(5, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, Trim(ListTb(i)("ImportOutName") & "")))
                        End If

                        If .Items.Item(i).SubItems.Count > 6 Then
                            .Items.Item(i).SubItems(6).Text = .Items.Add(ListTb(i)("ImportInName"))
                        Else
                            .Items.Item(i).SubItems.Insert(6, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, ListTb(i)("ImportInName")))
                        End If

                        If .Items.Item(i).SubItems.Count > 7 Then
                            .Items.Item(i).SubItems(7).Text = .Items.Add(ListTb(i)("collectionAC"))
                        Else
                            .Items.Item(i).SubItems.Insert(7, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, ListTb(i)("collectionAC")))
                        End If
                        If .Items.Item(i).SubItems.Count > 8 Then
                            .Items.Item(i).SubItems(8).Text = .Items.Add(ListTb(i)("paymentAC"))
                        Else
                            .Items.Item(i).SubItems.Insert(8, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, ListTb(i)("paymentAC")))
                        End If

                        If .Items.Item(i).SubItems.Count > 9 Then
                            .Items.Item(i).SubItems(9).Text = .Items.Add(ListTb(i)("OutpurtvatImport"))
                        Else
                            .Items.Item(i).SubItems.Insert(9, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, ListTb(i)("OutpurtvatImport")))
                        End If
                        If .Items.Item(i).SubItems.Count > 10 Then
                            .Items.Item(i).SubItems(10).Text = .Items.Add(ListTb(i)("InputvatImport"))
                        Else
                            .Items.Item(i).SubItems.Insert(10, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, ListTb(i)("InputvatImport")))
                        End If

                        If .Items.Item(i).SubItems.Count > 11 Then
                            .Items.Item(i).SubItems(11).Text = .Items.Add(ListTb(i)("vatid"))
                        Else
                            .Items.Item(i).SubItems.Insert(11, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, ListTb(i)("vatid")))
                        End If
                    Next
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub lstContent_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstContent.DoubleClick
        chgbyprg = True
        txtcode.Tag = Val(lstContent.SelectedItems(0).SubItems(11).Text)
        txtcode.Text = lstContent.SelectedItems(0).SubItems(0).Text
        txtname.Text = lstContent.SelectedItems(0).SubItems(1).Text
        txtvat.Text = Format(CDbl(lstContent.SelectedItems(0).SubItems(2).Text), numFormat)
        txtcollectionAc.Text = lstContent.SelectedItems(0).SubItems(3).Text
        txtcollectionAc.Tag = Val(lstContent.SelectedItems(0).SubItems(7).Text)
        txtpaymetac.Text = lstContent.SelectedItems(0).SubItems(4).Text
        txtpaymetac.Tag = Val(lstContent.SelectedItems(0).SubItems(8).Text)

        txtoutputimport.Text = lstContent.SelectedItems(0).SubItems(5).Text
        txtoutputimport.Tag = Val(lstContent.SelectedItems(0).SubItems(9).Text)
        txtvatinputimport.Text = lstContent.SelectedItems(0).SubItems(6).Text
        txtvatinputimport.Tag = Val(lstContent.SelectedItems(0).SubItems(10).Text)

        txtcode.Focus()
        btndelete.Enabled = True
        chgbyprg = False
    End Sub

    Private Sub txtvat_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtvat.KeyPress
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

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        makeClear()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        If MsgBox("Do you want to remove the VAT?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        _objcmnbLayer._saveDatawithOutParm("DELETE FROM VatMasterTb WHERE vatid=" & Val(txtcode.Tag))
        txtcode.Focus()
        makeClear()
        loadVatmaster()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub txtvat_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtvat.TextChanged

    End Sub

    Private Sub txtcollectionAc_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcollectionAc.TextChanged

    End Sub

    Private Sub lstContent_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstContent.SelectedIndexChanged

    End Sub
End Class