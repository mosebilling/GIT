Public Class AddSerialnoFrm
#Region "Public Variables"
    Public Event AddSerialNos()
    Public isSales As Boolean
    Public isReturn As Boolean
    Public Trid As Long
    Public detId As Long
    Public rowIndex As Integer
#End Region
#Region "Class Objects"
    Private _objcmnbLayer As clsCommon_BL
    Private _objItmMast As clsItemMast_BL
    Public _objbLayer As clsCommon_BL
#End Region
#Region "Private Variables"
    Private isDual As Boolean
    Private makeIsreturn As Boolean
    Private dtCurrentItems As DataTable
#End Region

    Private Sub AddSerialnoFrm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtserialno.Focus()
    End Sub
    Private Sub AddSerialnoFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        getItemstatus()
        SetHead()
        dtCurrentItems = createdtSerialNo()
    End Sub
    Private Sub getItemstatus()
        Dim dt As DataTable
        _objcmnbLayer = New clsCommon_BL
        dt = _objcmnbLayer._fldDatatable("select isDuealSerialNo from InvItmPropertiesTb where itemid=" & Val(txtserialno.Tag))
        If dt.Rows.Count > 0 Then
            isDual = dt(0)("isDuealSerialNo")
            If Not isSales And Not isReturn Then
                If isDual Then
                    Label2.Visible = True
                    txtserialNoII.Visible = True
                    lblserialType.Text = "Type : Dual IMEI Number"
                Else
                    Label2.Visible = False
                    txtserialNoII.Visible = False
                    lblserialType.Text = "Type : Single IMEI Number"
                End If
            Else
                If isDual Then
                    lblserialType.Text = "Type : Dual IMEI Number"
                Else
                    lblserialType.Text = "Type : Single IMEI Number"
                End If
                Label2.Visible = False
                txtserialNoII.Visible = False
            End If
        End If
    End Sub
    Private Sub SetHead()
        SetGridProperty(grdVoucher)
        With grdVoucher
            .ColumnCount = 2
            .Columns(0).HeaderText = "IMEI No"
            .Columns(0).Width = 300
            .Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable

            .Columns(1).HeaderText = "Warranty Expiry Date"
            .Columns(1).Width = 150
            .Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
        End With
    End Sub

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        RaiseEvent AddSerialNos()
    End Sub

    Private Sub btnrem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrem.Click
        If MsgBox("Do you want to remove the row?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        deleteDtSerialNo(dtCurrentItems, grdVoucher.Item(0, grdVoucher.CurrentRow.Index).Value, Val(txtserialno.Tag))
        grdVoucher.Rows.RemoveAt(grdVoucher.CurrentRow.Index)
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub txtserialno_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtserialno.KeyDown, txtserialNoII.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim dtExstSrlno As DataTable
            _objcmnbLayer = New clsCommon_BL
            _objItmMast = New clsItemMast_BL

            Dim myctrl As TextBox
            myctrl = sender
            Dim serialNo As String = ""
            Dim errSno As String = ""
            If isReturn Then
                serialNo = txtserialno.Text
                dtExstSrlno = _objItmMast.checkIsSerialnumberAvailable(serialNo, isDual, Val(txtserialno.Tag))
                If dtExstSrlno.Rows.Count > 0 Then
                    serialNo = dtExstSrlno(0)("SerialNo")
                ElseIf isSales And Not enableSerialnumberWithoutPurchase Then
                    MsgBox("IMEI No. not found!", MsgBoxStyle.Exclamation)
                    txtserialno.Focus()
                    Exit Sub
                Else
                    If isDual Then
                        If txtserialNoII.Visible = False Then
                            MsgBox("IMEI No. not found! You should enter Second IMEI Number", MsgBoxStyle.Exclamation)
                        End If
                        isReturn = False
                        makeIsreturn = True
                        Label2.Visible = True
                        txtserialNoII.Visible = True
                        txtserialNoII.Focus()
                        Exit Sub
                    End If

                End If
            End If
            If isSales Then
                If Not isReturn Then serialNo = txtserialno.Text
                dtExstSrlno = _objItmMast.checkIsSerialnumberAvailable(serialNo, isDual, Val(txtserialno.Tag))
                If dtExstSrlno.Rows.Count = 0 Then
slserr:
                    If Not enableSerialnumberWithoutPurchase Then
                        MsgBox("IMEI No. not found!", MsgBoxStyle.Exclamation)
                        txtserialno.Focus()
                        Exit Sub
                    End If
                Else
                    If Val(dtExstSrlno(0)("Cnt") & "") > 0 Then
                        If Val(txtserialno.Tag) <> Val(dtExstSrlno(0)("itemid") & "") Then
                            Dim dtitm As DataTable
                            dtitm = _objcmnbLayer._fldDatatable("Select [Item Code],Description from InvItm where itemid=" & Val(dtExstSrlno(0)("itemid")))
                            If dtitm.Rows.Count > 0 Then
                                MsgBox("IMEI number found for another item " & dtitm(0)("Description"), MsgBoxStyle.Exclamation)
                                Exit Sub
                            End If
                        End If
                        serialNo = dtExstSrlno(0)("SerialNo")
                    Else
                        GoTo slserr
                    End If

                End If
            Else
                If isReturn Then GoTo rtnExt
                If isDual Then
                    If myctrl.Name = "txtserialno" Then
                        txtserialNoII.Focus()
                        Exit Sub
                    Else
                        If txtserialno.Text = "" Then
                            MsgBox("IMEI Number-I should not be blank", MsgBoxStyle.Exclamation)
                            txtserialno.Focus()
                            Exit Sub
                        End If
                        If txtserialNoII.Text = "" Then
                            MsgBox("IMEI Number-II should not be blank", MsgBoxStyle.Exclamation)
                            txtserialNoII.Focus()
                            Exit Sub
                        End If
                    End If
                    serialNo = txtserialno.Text & "," & txtserialNoII.Text
                Else
                    serialNo = txtserialno.Text
                End If

                If Not isDual Then
rtnExt:
                    dtExstSrlno = _objItmMast.checkIsSerialnumberAvailable(serialNo, False, Val(txtserialno.Tag))
                    errSno = txtserialno.Text
                Else
                    errSno = txtserialno.Text
                    'dtExstSrlno = _objcmnbLayer._fldDatatable("SELECT SerialNo,ItemId,PreFix,InvNo,TrType FROM serialnotb " & _
                    '                                          "left join ItmInvCmnTb on SerialNoTb.PurchaseId=ItmInvCmnTb.TrId where SerialNo like '%" & errSno & "%' and ItemId =" & Val(txtserialno.Tag))
                    dtExstSrlno = _objItmMast.checkIsSerialnumberAvailable(errSno, isDual, Val(txtserialno.Tag))
                    If dtExstSrlno.Rows.Count > 0 Then GoTo blk
                    errSno = txtserialNoII.Text
                    'dtExstSrlno = _objcmnbLayer._fldDatatable("SELECT SerialNo,ItemId,PreFix,InvNo,TrType FROM serialnotb " & _
                    '                                          "left join ItmInvCmnTb on SerialNoTb.PurchaseId=ItmInvCmnTb.TrId where SerialNo like '%" & errSno & "%' and ItemId =" & Val(txtserialno.Tag))
                    dtExstSrlno = _objItmMast.checkIsSerialnumberAvailable(errSno, isDual, Val(txtserialno.Tag))

                End If
blk:
                If isReturn Then errSno = serialNo
                If dtExstSrlno.Rows.Count > 0 Then
                    If Val(dtExstSrlno(0)("Cnt") & "") > 0 Then
                        MsgBox("IMEI No ( " & errSno & " ) already exist in [" & Trim(dtExstSrlno(0)("TrType") & "") & " - " & Trim(dtExstSrlno(0)("PreFix") & "") & IIf(Trim(dtExstSrlno(0)("PreFix") & "") = "", "", "/") & Val(dtExstSrlno(0)("InvNo") & "") & "] !", MsgBoxStyle.Exclamation)
                        txtserialno.Focus()
                        Exit Sub
                    ElseIf Val(dtExstSrlno(0)("itemid")) <> Val(txtserialno.Tag) Then
                        MsgBox("IMEI No ( " & errSno & " ) already exist for another item !", MsgBoxStyle.Exclamation)
                        txtserialno.Focus()
                        Exit Sub
                    End If
                End If
            End If
            If serialNo <> "" Then
                If isSerialNoExistInList(txtserialno.Text) Then
                    MsgBox("IMEI Number [" & txtserialno.Text & "] already added for this item", MsgBoxStyle.Exclamation)
                    txtserialno.Focus()
                    Exit Sub
                End If
                If isSerialNoExistInList(txtserialNoII.Text) Then
                    MsgBox("IMEI Number [" & txtserialNoII.Text & "] already added for this item", MsgBoxStyle.Exclamation)
                    txtserialno.Focus()
                    Exit Sub
                End If
                'to check current list
                If isSerialNoExistCurrentInList(txtserialno.Text) Then
                    MsgBox("IMEI Number [" & txtserialno.Text & "] already added for this item", MsgBoxStyle.Exclamation)
                    txtserialno.Focus()
                    Exit Sub
                End If
                If isSerialNoExistCurrentInList(txtserialNoII.Text) Then
                    MsgBox("IMEI Number [" & txtserialNoII.Text & "] already added for this item", MsgBoxStyle.Exclamation)
                    txtserialno.Focus()
                    Exit Sub
                End If
                With grdVoucher
                    .Rows.Add()
                    .Item(0, .Rows.Count - 1).Value = serialNo
                    .Item(1, .Rows.Count - 1).Value = Format(DateValue(cldrdate.Value), DtFormat)
                End With
                AddTodtSerialNo(serialNo)
            End If
            serialNo = ""
            txtserialno.Text = ""
            txtserialNoII.Text = ""
            txtserialno.Focus()
            If makeIsreturn Then isReturn = True
            If isReturn Then
                Label2.Visible = False
                txtserialNoII.Visible = False
                makeIsreturn = False
            End If

        End If
    End Sub

    Private Sub cldrdate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cldrdate.KeyDown
        If e.KeyCode = Keys.Return Then
            txtserialno.Focus()
        End If
    End Sub


    Private Sub chknowarrenty_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chknowarrenty.CheckedChanged
        If chknowarrenty.Checked Then
            cldrdate.Tag = cldrdate.Value
            cldrdate.Value = DateValue("01/01/1950")
            cldrdate.Enabled = False
        Else
            cldrdate.Value = cldrdate.Tag
            cldrdate.Enabled = True
        End If
        txtserialno.Focus()
    End Sub
    Private Sub AddTodtSerialNo(ByVal serialno As String)
        Dim dtrow As DataRow
        dtrow = dtCurrentItems.NewRow
        dtrow("ItmSerialNo") = serialno
        dtrow("Wdate") = Format(DateValue(cldrdate.Value), DtFormat)
        dtrow("Trid") = Trid
        dtrow("Detid") = detId
        dtrow("itemid") = Val(txtserialno.Tag)
        dtrow("RowIndex") = rowIndex
        dtrow("dtTbIndex") = dtCurrentItems.Rows.Count
        dtCurrentItems.Rows.Add(dtrow)

    End Sub
    Private Function isSerialNoExistInList(ByVal srchText As String) As Boolean
        If srchText = "" Then Return False
        Dim _qurey As EnumerableRowCollection(Of DataRow)
        _qurey = From data In _objbLayer.dtSerialNo.AsEnumerable() Where data("ItmSerialNo").ToString.ToUpper.Contains(UCase(srchText)) And data("itemid") = Val(txtserialno.Tag) Select data
        If _qurey.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Function isSerialNoExistCurrentInList(ByVal srchText As String) As Boolean
        If srchText = "" Then Return False
        Dim _qurey As EnumerableRowCollection(Of DataRow)
        _qurey = From data In dtCurrentItems.AsEnumerable() Where data("ItmSerialNo").ToString.ToUpper.Contains(UCase(srchText)) And data("itemid") = Val(txtserialno.Tag) Select data
        If _qurey.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub txtserialno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtserialno.TextChanged

    End Sub
End Class