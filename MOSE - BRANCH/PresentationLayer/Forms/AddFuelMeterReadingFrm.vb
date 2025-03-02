Public Class AddFuelMeterReadingFrm
    Private _objcmnbLayer As New clsCommon_BL
    Private chgbyprg As Boolean
    Private lnumformat As String = "#,##0.000"

    Private Sub AddFuelMeterReadingFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtcode.Text = GenerateNext(txtcode.Text)
        loadFuelMeter()
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
            dt = _objcmnbLayer._fldDatatable("SELECT top 1 fcode from FuelMeterReadingTb order by fmeterid desc")
            If dt.Rows.Count > 0 Then
                strCode = dt(0)(0)
            End If
        End If
        If strCode = "" Then
            strCode = "FM"
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
                dr = _objcmnbLayer._fldDatatable("SELECT fcode from FuelMeterReadingTb WHERE fcode = '" & tmp & "'")
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

    Private Sub txtcode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtcode.KeyDown, txtfname.KeyDown, txtreading.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub


    Private Sub txtreading_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtreading.KeyPress
        NumericTextOnKeypress(sender, e, chgbyprg, lnumformat)
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        'If Val(btnupdate.Tag) = 0 Then
        '    MsgBox("This user do not have rights to update", MsgBoxStyle.Exclamation)
        '    Exit Sub
        'End If
        If chkDuplicate() Then Exit Sub
        saveFuelMeter()
    End Sub
    Private Function chkDuplicate() As Boolean
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT fcode from FuelMeterReadingTb WHERE fcode = '" & txtcode.Text & "' and fmeterid<>" & Val(txtcode.Tag))
        If dt.Rows.Count > 0 Then
            MsgBox("Fuel Meter Code Already Exist", MsgBoxStyle.Exclamation)
            Return True
        Else
            Return False
        End If
    End Function
    Private Sub loadFuelMeter()
        Dim ListTb As DataTable
        ListTb = _objcmnbLayer._fldDatatable("SELECT fcode Code,fmetername [Fuel Name],fstarting Opening,ISNULL(fending,0) Ending,fmeterid FROM FuelMeterReadingTb")
        With lstContent
            .Items.Clear()
            If ListTb.Rows.Count > 0 Then
                For i = 0 To ListTb.Rows.Count - 1
                    .Items.Add(ListTb(i)("Code"))
                    If .Items.Item(i).SubItems.Count > 1 Then
                        .Items.Item(i).SubItems(1).Text = .Items.Add(ListTb(i)("Fuel Name"))
                    Else
                        .Items.Item(i).SubItems.Insert(1, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, ListTb(i)("Fuel Name")))
                    End If

                    If .Items.Item(i).SubItems.Count > 2 Then
                        .Items.Item(i).SubItems(2).Text = .Items.Add(ListTb(i)("Opening"))
                    Else
                        .Items.Item(i).SubItems.Insert(2, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, ListTb(i)("Opening")))
                    End If

                    If .Items.Item(i).SubItems.Count > 3 Then
                        .Items.Item(i).SubItems(3).Text = .Items.Add(ListTb(i)("Ending"))
                    Else
                        .Items.Item(i).SubItems.Insert(3, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, ListTb(i)("Ending")))
                    End If

                    If .Items.Item(i).SubItems.Count > 4 Then
                        .Items.Item(i).SubItems(4).Text = .Items.Add(ListTb(i)("fmeterid"))
                    Else
                        .Items.Item(i).SubItems.Insert(4, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, ListTb(i)("fmeterid")))
                    End If

                Next
            End If
        End With
    End Sub
    Private Sub saveFuelMeter()
        If Val(txtreading.Text) = 0 Then txtreading.Text = 0
        If Val(txtcode.Tag) = 0 Then
            _objcmnbLayer._saveDatawithOutParm("Insert into FuelMeterReadingTb (fcode,fmetername,fstarting) values('" & txtcode.Text & "','" & txtfname.Text & "'," & CDbl(txtreading.Text) & ")")
        Else
            _objcmnbLayer._saveDatawithOutParm("Update FuelMeterReadingTb set fcode='" & txtcode.Text & "',fmetername='" & txtfname.Text & "',fstarting=" & CDbl(txtreading.Text) & " WHERE fmeterid=" & Val(txtcode.Tag))
        End If
        MsgBox("Record Updated Successfully", MsgBoxStyle.Information)
        makeclear()
        loadFuelMeter()
    End Sub

    Private Sub makeclear()
        txtcode.Tag = ""
        txtfname.Text = ""
        txtreading.Text = "0.000"
        txtcode.Text = GenerateNext(txtcode.Text)
        btndelete.Text = "Clear"
        btnundo.Visible = False
    End Sub
    Private Sub loadtoEdit()
        chgbyprg = True
        txtcode.Text = lstContent.SelectedItems(0).SubItems(0).Text
        txtfname.Text = lstContent.SelectedItems(0).SubItems(1).Text
        txtreading.Text = Format(CDbl(lstContent.SelectedItems(0).SubItems(2).Text), lnumformat)
        txtcode.Tag = Val(lstContent.SelectedItems(0).SubItems(4).Text)
        chgbyprg = False
        btndelete.Text = "Delete"
        btnundo.Visible = True
    End Sub

    Private Sub lstContent_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstContent.DoubleClick
        loadtoEdit()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        If btndelete.Text <> "Delete" Then
            makeclear()
        Else
            If MsgBox("Do you want Remove Selected Meter Code?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            _objcmnbLayer._saveDatawithOutParm("Delete from FuelMeterReadingTb WHERE fmeterid=" & Val(txtcode.Tag))
            makeclear()
            loadFuelMeter()
        End If
    End Sub

    Private Sub btnundo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnundo.Click
        makeclear()
    End Sub
End Class