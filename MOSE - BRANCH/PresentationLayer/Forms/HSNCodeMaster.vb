Public Class HSNCodeMaster
    Private WithEvents fSelect As Selectfrm
    Private srchIndex As Single
    Dim _objcmnbLayer As New clsCommon_BL
    Private _objGst As New clsGSTMaster
    Private chgbyprg As Boolean
    Private Hsncode As String
    Public isfromexternal As Boolean
    Public Event returnHsn(ByVal hsncode As String)
    Private Sub ldSelect(ByVal BVal As Single)
        fSelect = New Selectfrm
        fSelect.Location = New Point(650, 150)
        fSelect.StartPosition = FormStartPosition.CenterScreen
        SetForm(fSelect, BVal)
        fSelect.Show()
    End Sub

    Private Sub fSelect_doSelect(ByVal KeyId As String, ByVal strFld1 As String, ByVal strFld2 As String) Handles fSelect.doSelect
        Select Case srchIndex
            Case 1
                txtcollectionAcSGST.Text = strFld1
                txtcollectionAcSGST.Tag = KeyId
                txtcollectionAcSGST.Focus()
            Case 2
                txtpaymetacSGST.Text = strFld1
                txtpaymetacSGST.Tag = KeyId
                txtpaymetacSGST.Focus()
            Case 3
                txtCollectionAcCSGT.Text = strFld1
                txtCollectionAcCSGT.Tag = KeyId
                txtCollectionAcCSGT.Focus()
            Case 4
                txtPaymentAcCSGT.Text = strFld1
                txtPaymentAcCSGT.Tag = KeyId
                txtPaymentAcCSGT.Focus()
            Case 5
                txtCollectionAcIGST.Text = strFld1
                txtCollectionAcIGST.Tag = KeyId
                txtCollectionAcIGST.Focus()
            Case 6
                txtpaymentacIGST.Text = strFld1
                txtpaymentacIGST.Tag = KeyId
                txtpaymentacIGST.Focus()
        End Select

       

    End Sub

    Private Sub txtcollectionAc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtcollectionAcSGST.KeyDown, txtpaymetacSGST.KeyDown, _
                                                                                                                        txtcode.KeyDown, txtname.KeyDown, txtSGST.KeyDown, _
                                                                                                                        txtCGST.KeyDown, txtIGST.KeyDown, _
                                                                                                                        txtCollectionAcCSGT.KeyDown, txtPaymentAcCSGT.KeyDown, _
                                                                                                                        txtCollectionAcIGST.KeyDown, txtpaymentacIGST.KeyDown

        Dim myctrl As TextBox
        myctrl = sender
        If e.KeyCode = Keys.F2 Then
            If Val(myctrl.AccessibleDescription & "") > 0 Then
                srchIndex = Val(myctrl.AccessibleDescription & "")
                ldSelect(2)
            End If
        ElseIf e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub fSelect_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fSelect.FormClosed
        fSelect = Nothing
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        If txtcode.Text = "" Then
            MsgBox("Invalid Tax Code", MsgBoxStyle.Exclamation)
            txtcode.Focus()
            Exit Sub
            'ElseIf txtname.Text = "" Then
            '    MsgBox("Invalid GST Name", MsgBoxStyle.Exclamation)
            '    txtname.Focus()
            '    Exit Sub
            'ElseIf Val(txtSGST.Text) = 0 Then
            '    MsgBox("Invalid SGST %", MsgBoxStyle.Exclamation)
            '    txtSGST.Focus()
            '    Exit Sub
            'ElseIf Val(txtCGST.Text) = 0 Then
            '    MsgBox("Invalid CGST %", MsgBoxStyle.Exclamation)
            '    txtSGST.Focus()
            '    Exit Sub
            'ElseIf Val(txtIGST.Text) = 0 Then
            '    MsgBox("Invalid IGST %", MsgBoxStyle.Exclamation)
            '    txtIGST.Focus()
            '    Exit Sub
        End If
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("select gstid from GSTTb where HSNCode='" & txtcode.Text & "' and gstid<>" & Val(txtcode.Tag))
        If dt.Rows.Count > 0 Then
            MsgBox("Tax Code already exist", MsgBoxStyle.Exclamation)
            txtcode.Focus()
            Exit Sub
        End If

        If Hsncode <> "" And Hsncode <> txtcode.Text Then
            dt = _objcmnbLayer._fldDatatable("select HSNCode from ItmInvTrTb where HSNCode='" & Hsncode & "'")
            If dt.Rows.Count > 0 Then
                MsgBox("TAX Code already used! Cannot modify tax code", MsgBoxStyle.Exclamation)
                txtcode.Focus()
                Exit Sub
            End If
            dt = _objcmnbLayer._fldDatatable("select HSNCode from InvItm where HSNCode='" & Hsncode & "'")
            If dt.Rows.Count > 0 Then
                MsgBox("TAX Code already used! Cannot modify tax code", MsgBoxStyle.Exclamation)
                txtcode.Focus()
                Exit Sub
            End If
        End If
        If Val(txtcode.Tag) > 0 Then
            dt = _objcmnbLayer._fldDatatable("select HSNCode from ItmInvTrTb where Trgstid=" & Val(txtcode.Tag))
            If dt.Rows.Count > 0 Then
                If dt(0)("HSNCode") <> txtcode.Text Then
                    MsgBox("TAX Code already used! Cannot modify tax code", MsgBoxStyle.Exclamation)
                    txtcode.Focus()
                    Exit Sub
                End If
            End If
        End If
        
        saveRec()
    End Sub
    Private Sub saveRec()
        _objGst = New clsGSTMaster
        With _objGst
            .gstid = Val(txtcode.Tag)
            .HSNCode = txtcode.Text ' changed as TaxCode
            .CGST = CDbl(txtCGST.Text)
            .SGST = CDbl(txtSGST.Text)
            .IGST = CDbl(txtIGST.Text)
            .CGSTCAc = Val(txtCollectionAcCSGT.Tag)
            .CGSTPAc = Val(txtPaymentAcCSGT.Tag)
            .SGSTCAc = Val(txtcollectionAcSGST.Tag)
            .SGSTPAc = Val(txtpaymetacSGST.Tag)
            .IGSTCAc = Val(txtCollectionAcIGST.Tag)
            .IGSTPAc = Val(txtpaymentacIGST.Tag)
            .GSTName = txtname.Text ' changed as HSNCode
            .saveGSTMaster()
        End With
        loadGSTTb()
        MsgBox("GST Master updated successfully", MsgBoxStyle.Information)
        If isfromexternal Then
            RaiseEvent returnHsn(txtcode.Text)
            Me.Close()
        Else
            makeClear()
            loadGSTmaster()
        End If
    End Sub
    Private Sub loadGSTTb()
        Dim qry As String = " select GSTTb.*,CGSTCAname,CGSTPAname,SGSTCAname,SGSTPAname,IGSTCAname,IGSTPAname from GSTTb " & _
              "left join (select accid CGSTCAcId,AccDescr CGSTCAname  from AccMast) CGSTC on CGSTC.CGSTCAcId =GSTTb.CGSTCAc " & _
              "left join (select accid CGSTPAcId,AccDescr CGSTPAname  from AccMast) CGSTP on CGSTP.CGSTPAcId =GSTTb.CGSTPAc " & _
              "left join (select accid SGSTCAcId,AccDescr SGSTCAname  from AccMast) SGSTC on SGSTC.SGSTCAcId =GSTTb.SGSTCAc " & _
              "left join (select accid SGSTPAcId,AccDescr SGSTPAname  from AccMast) SGSTP on SGSTP.SGSTPAcId =GSTTb.SGSTPAc " & _
              "left join (select accid IGSTCAcId,AccDescr IGSTCAname  from AccMast) IGSTC on IGSTC.IGSTCAcId =GSTTb.IGSTCAc " & _
              "left join (select accid IGSTPAcId,AccDescr IGSTPAname  from AccMast) IGSTP on IGSTP.IGSTPAcId =GSTTb.IGSTPAc "
        dtGST = _objcmnbLayer._fldDatatable(qry)
    End Sub
    Private Sub makeClear()
        chgbyprg = True
        txtcode.Text = ""
        txtcode.Tag = ""
        txtname.Text = ""
        txtcollectionAcSGST.Text = ""
        txtcollectionAcSGST.Tag = ""
        txtpaymetacSGST.Tag = ""
        txtpaymetacSGST.Text = ""
        txtSGST.Text = "0.00"
        txtCGST.Text = "0.00"
        txtIGST.Text = "0.00"

        txtCollectionAcCSGT.Text = ""
        txtCollectionAcCSGT.Tag = ""
        txtPaymentAcCSGT.Tag = ""
        txtPaymentAcCSGT.Text = ""

        txtCollectionAcIGST.Text = ""
        txtCollectionAcIGST.Tag = ""
        txtpaymentacIGST.Tag = ""
        txtpaymentacIGST.Text = ""
        btndelete.Enabled = False

        chgbyprg = False
    End Sub
    Private Sub loadGSTmaster()
        Dim ListTb As DataTable
        ListTb = _objGst.returnGstMaster(0)
        With lstContent
            .Items.Clear()
            If ListTb.Rows.Count > 0 Then
                For i = 0 To ListTb.Rows.Count - 1
                    .Items.Add(ListTb(i)("HSNCode"))
                    If .Items.Item(i).SubItems.Count > 1 Then
                        .Items.Item(i).SubItems(1).Text = .Items.Add(ListTb(i)("GSTName"))
                    Else
                        .Items.Item(i).SubItems.Insert(1, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, ListTb(i)("GSTName")))
                    End If
                    If .Items.Item(i).SubItems.Count > 2 Then
                        .Items.Item(i).SubItems(2).Text = .Items.Add(ListTb(i)("gstid"))
                    Else
                        .Items.Item(i).SubItems.Insert(2, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, ListTb(i)("gstid")))
                    End If
                Next
            End If
        End With

    End Sub

    Private Sub lstContent_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstContent.DoubleClick
        chgbyprg = True
        txtcode.Focus()
        btndelete.Enabled = True
        txtcode.Text = lstContent.SelectedItems(0).SubItems(0).Text
        ldRec()
        chgbyprg = False
    End Sub
    Private Sub ldRec()
        Dim dt As DataTable
        _objGst = New clsGSTMaster
        _objGst.HSNCode = txtcode.Text
        dt = _objGst.returnGstMaster(1)
        If dt.Rows.Count > 0 Then
            txtcode.Text = dt(0)("HSNCode")
            Hsncode = dt(0)("HSNCode")
            txtcode.Tag = dt(0)("gstid")
            txtname.Text = dt(0)("GSTName")
            txtSGST.Text = Format(CDbl(dt(0)("SGST")), numFormat)
            txtCGST.Text = Format(CDbl(dt(0)("CGST")), numFormat)
            txtIGST.Text = Format(CDbl(dt(0)("IGST")), numFormat)

            txtcollectionAcSGST.Text = Trim(dt(0)("SGSTCAname") & "")
            txtcollectionAcSGST.Tag = Val(dt(0)("SGSTCAc") & "")
            txtCollectionAcCSGT.Text = Trim(dt(0)("CGSTCAname") & "")
            txtCollectionAcCSGT.Tag = dt(0)("CGSTCAc")
            txtCollectionAcIGST.Text = Trim(dt(0)("IGSTCAname") & "")
            txtCollectionAcIGST.Tag = dt(0)("IGSTCAc")

            txtpaymetacSGST.Text = Trim(dt(0)("SGSTPAname") & "")
            txtpaymetacSGST.Tag = dt(0)("SGSTPAc")
            txtPaymentAcCSGT.Text = Trim(dt(0)("CGSTPAname") & "")
            txtPaymentAcCSGT.Tag = dt(0)("CGSTPAc")
            txtpaymentacIGST.Text = Trim(dt(0)("IGSTPAname") & "")
            txtpaymentacIGST.Tag = dt(0)("IGSTPAc")

        End If
    End Sub
    Private Sub txtvat_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSGST.KeyPress, txtCGST.KeyPress, txtIGST.KeyPress
        'NumericTextOnKeypress(sender, e, chgbyprg, numFormat)
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        makeClear()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        If MsgBox("Do you want to remove the Record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select itemid from invitm where HSNCode='" & txtcode.Text & "'")
        If dt.Rows.Count > 0 Then
            MsgBox("HSN Code found in product master! You cannot remove", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        _objcmnbLayer._saveDatawithOutParm("DELETE FROM GSTTb WHERE gstid=" & Val(txtcode.Tag))
        txtcode.Focus()
        makeClear()
        loadGSTmaster()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub HSNCodeMaster_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtname.Focus()
    End Sub

    Private Sub HSNCodeMaster_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If Not fSelect Is Nothing Then fSelect.Close() : fSelect = Nothing
    End Sub

    Private Sub HSNCodeMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        makeClear()
        loadGSTmaster()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select gst from GstDefaultSetTb")
        If dt.Rows.Count > 0 Then
            cmbgstslab.Items.Clear()
            Dim i As Integer
            cmbgstslab.Items.Add(0)
            For i = 0 To dt.Rows.Count - 1
                cmbgstslab.Items.Add(dt(i)("gst"))
            Next
        End If
    End Sub

    Private Sub txtcollectionAcSGST_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcollectionAcSGST.TextChanged

    End Sub

    Private Sub lstContent_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstContent.SelectedIndexChanged

    End Sub

    Private Sub cmbgstslab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbgstslab.SelectedIndexChanged
        txtSGST.Text = Format(Val(cmbgstslab.Text) / 2, numFormat)
        txtCGST.Text = Format(Val(cmbgstslab.Text) / 2, numFormat)
        txtIGST.Text = Format(Val(cmbgstslab.Text), numFormat)
        txtcode.Text = txtname.Text & " - " & cmbgstslab.Text & "%"
        loadgstslab()
    End Sub
    Private Sub loadgstslab()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select * from GstDefaultSetTb" & _
                                         " left join (select accid cid,accdescr cgstname from accmast)cacd on GstDefaultSetTb.cac=cacd.cid" & _
                                         " left join (select accid pid,accdescr sgstname from accmast)pacd on GstDefaultSetTb.pac=pacd.pid" & _
                                         " left join (select accid igtid,accdescr igstname from accmast)igstacd on GstDefaultSetTb.igstac=igstacd.igtid" & _
                                         " left join (select accid cgstpid,accdescr cgstpname from accmast)cgstpacd on GstDefaultSetTb.cgstpac=cgstpacd.cgstpid" & _
                                         " left join (select accid sgstpid,accdescr sgstpname from accmast)sgstpacd on GstDefaultSetTb.sgstpac=sgstpacd.sgstpid" & _
                                         " left join (select accid igstpid,accdescr isgtpname from accmast)igstpacd on GstDefaultSetTb.igstpac=igstpacd.igstpid" & _
                                         " where gst=" & Val(cmbgstslab.Text))
        If dt.Rows.Count > 0 Then
            txtcollectionAcSGST.Text = Trim(dt(0)("sgstname") & "")
            txtpaymetacSGST.Text = Trim(dt(0)("sgstpname") & "")
            txtcollectionAcSGST.Tag = dt(0)("pid")
            txtpaymetacSGST.Tag = dt(0)("sgstpid")

            txtCollectionAcCSGT.Text = Trim(dt(0)("cgstname") & "")
            txtPaymentAcCSGT.Text = Trim(dt(0)("cgstpname") & "")
            txtCollectionAcCSGT.Tag = dt(0)("cid")
            txtPaymentAcCSGT.Tag = dt(0)("cgstpid")

            txtCollectionAcIGST.Text = Trim(dt(0)("igstname") & "")
            txtpaymentacIGST.Text = Trim(dt(0)("isgtpname") & "")
            txtCollectionAcIGST.Tag = dt(0)("igtid")
            txtpaymentacIGST.Tag = dt(0)("igstpid")
        Else
            txtcollectionAcSGST.Text = ""
            txtpaymetacSGST.Text = ""
            txtcollectionAcSGST.Tag = ""
            txtpaymetacSGST.Tag = ""

            txtCollectionAcCSGT.Text = ""
            txtPaymentAcCSGT.Text = ""
            txtCollectionAcCSGT.Tag = ""
            txtPaymentAcCSGT.Tag = ""

            txtCollectionAcIGST.Text = ""
            txtpaymentacIGST.Text = ""
            txtCollectionAcIGST.Tag = ""
            txtpaymentacIGST.Tag = ""
        End If
    End Sub

    Private Sub txtSGST_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSGST.TextChanged

    End Sub
End Class