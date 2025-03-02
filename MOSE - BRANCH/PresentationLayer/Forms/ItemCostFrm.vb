
Public Class ItemCostFrm
    Public isSupp As Boolean
    Private chgpgm As Boolean
    'object variable
    Dim _objcmnbLayer As New clsCommon_BL
    Private Sub ItemCostFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'If isSupp Then
        '    ldSupp()
        'Else
        '    ldItem()
        'End If
        'chgpgm = True
        chkitemcost.Checked = True
        'chgpgm = False
    End Sub
    Private Sub setGrdHead()
        With grdItem
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = True
            '.ReadOnly = True

            .ColumnHeadersHeight = 100
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke
            .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular)
            .RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 9.0!)

            .ColumnCount = 5

            .Columns(0).HeaderText = "Supplier"
            .Columns(0).Width = 250
            .Columns(0).ReadOnly = True

            .Columns(1).HeaderText = "Item Cost"
            .Columns(1).Width = 100
            .Columns(1).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(2).HeaderText = "Accountno"
            .Columns(2).Visible = False

            .Columns(3).HeaderText = "id"
            .Columns(3).Visible = False

            .Columns(4).HeaderText = "Last Cost"
            .Columns(4).Width = 100
            .Columns(4).ReadOnly = True
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End With
    End Sub
    Private Sub setCustGrdHead()
        With grdItem
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = True
            '.ReadOnly = True

            .ColumnHeadersHeight = 100
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke
            .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular)
            .RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 9.0!)
            .ColumnCount = 5

            .Columns(0).HeaderText = "Item Name"
            .Columns(0).Width = 250
            .Columns(0).ReadOnly = True


            .Columns(1).HeaderText = "Item Cost"
            .Columns(1).Width = 100
            .Columns(1).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(2).HeaderText = "itemid"
            .Columns(2).Visible = False

            .Columns(3).HeaderText = "id"
            .Columns(3).Visible = False

            .Columns(4).HeaderText = "Last Cost"
            .Columns(4).Width = 100
            .Columns(4).ReadOnly = True
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End With
    End Sub
    Private Sub ldSupp()
        Dim dt As DataTable
        setGrdHead()
        dt = _objcmnbLayer._fldDatatable("SELECT AccDescr,Opcost,AccMast.AccountNo,id,LastCost FROM AccMast LEFT JOIN S1AccHd ON AccMast.S1AccId=S1AccHd.S1AccId LEFT JOIN (SELECT Accountno,itemid,LastCost,Opcost,id FROM SuppCostTb WHERE  Itemid=" & Val(lblName.Tag) & ") SuppCostTb ON AccMast.AccountNo=SuppCostTb.AccountNo where GrpSetOn='Supplier'" & IIf(chkitemcost.Checked, " AND isnull(Opcost,0)+isnull(LastCost,0)>0", ""))
        Dim i As Integer
        grdItem.Rows.Clear()
        For i = 0 To dt.Rows.Count - 1
            With grdItem
                .Rows.Add(1)
                .Item(0, .Rows.Count - 1).Value = dt(i)("AccDescr")
                If IsDBNull(dt(i)("Opcost")) Then
                    .Item(1, .Rows.Count - 1).Value = "0.00"
                Else
                    .Item(1, .Rows.Count - 1).Value = Format(dt(i)("Opcost"), "0.00")
                End If
                .Item(2, .Rows.Count - 1).Value = dt(i)("AccountNo")
                If IsDBNull(dt(i)("id")) Then
                    .Item(3, .Rows.Count - 1).Value = 0
                Else
                    .Item(3, .Rows.Count - 1).Value = dt(i)("id")
                End If
                If IsDBNull(dt(i)("LastCost")) Then
                    .Item(4, .Rows.Count - 1).Value = "0.00"
                Else
                    .Item(4, .Rows.Count - 1).Value = dt(i)("LastCost")
                End If

            End With
        Next
    End Sub
    Private Sub ldItem()
        Dim dt As DataTable
        setCustGrdHead()
        dt = _objcmnbLayer._fldDatatable("SELECT Description,SuppCostTb.Opcost,InvItm.itemid,id,LastCost FROM InvItm LEFT JOIN (SELECT Accountno,itemid,LastCost,Opcost,id FROM SuppCostTb WHERE  AccountNo=" & Val(lblName.Tag) & ") SuppCostTb ON InvItm.itemid=SuppCostTb.itemid " & IIf(chkitemcost.Checked, " WHERE isnull(SuppCostTb.Opcost,0)+isnull(LastCost,0)>0", ""))
        Dim i As Integer
        grdItem.Rows.Clear()
        For i = 0 To dt.Rows.Count - 1
            With grdItem
                .Rows.Add(1)
                .Item(0, .Rows.Count - 1).Value = dt(i)("Description")
                If IsDBNull(dt(i)("Opcost")) Then
                    .Item(1, .Rows.Count - 1).Value = "0.00"
                Else
                    .Item(1, .Rows.Count - 1).Value = dt(i)("Opcost")
                End If
                .Item(2, .Rows.Count - 1).Value = dt(i)("ItemId")
                If IsDBNull(dt(i)("id")) Then
                    .Item(3, .Rows.Count - 1).Value = 0
                Else
                    .Item(3, .Rows.Count - 1).Value = dt(i)("id")
                End If
                If IsDBNull(dt(i)("LastCost")) Then
                    .Item(4, .Rows.Count - 1).Value = "0.00"
                Else
                    .Item(4, .Rows.Count - 1).Value = dt(i)("LastCost")
                End If
            End With
        Next
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub grdItem_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItem.CellClick
        If grdItem.RowCount > 0 Then
            grdItem.BeginEdit(True)
        End If
    End Sub

    Private Sub grdItem_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItem.CellEnter
        If grdItem.RowCount > 0 Then
            grdItem.BeginEdit(True)
        End If
    End Sub
    Private Sub Textbox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            With grdItem
                Dim col As Integer
                col = .CurrentCell.ColumnIndex
                If col = 1 Then
                    If Not IsNumeric(e.KeyChar) And e.KeyChar <> "." And Not e.KeyChar = Convert.ToChar(Keys.Back) Then e.Handled = True
                End If
            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub grdItem_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdItem.EditingControlShowing
        If grdItem.CurrentCell.ColumnIndex = 1 Then
            Dim tb As TextBox = CType(e.Control, TextBox)
            RemoveHandler tb.KeyPress, AddressOf Textbox_KeyPress
            AddHandler tb.KeyPress, AddressOf Textbox_KeyPress
        End If
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Dim i As Integer
        If isSupp Then
            For i = 0 To grdItem.RowCount - 1
                If Val(grdItem.Item(1, i).Value) = 0 Then grdItem.Item(1, i).Value = 0
                _objcmnbLayer.itemid = Val(lblName.Tag)
                _objcmnbLayer.Accountno = Val(grdItem.Item(2, i).Value)
                _objcmnbLayer.Opcost = CDbl(grdItem.Item(1, i).Value)
                _objcmnbLayer.id = Val(grdItem.Item(3, i).Value)
                _objcmnbLayer.saveOrEditSuppCostTb()
            Next
        Else
            For i = 0 To grdItem.RowCount - 1
                If Val(grdItem.Item(1, i).Value) = 0 Then grdItem.Item(1, i).Value = 0
                _objcmnbLayer.itemid = Val(grdItem.Item(2, i).Value)
                _objcmnbLayer.Accountno = Val(lblName.Tag)
                _objcmnbLayer.Opcost = CDbl(grdItem.Item(1, i).Value)
                _objcmnbLayer.id = Val(grdItem.Item(3, i).Value)
                _objcmnbLayer.saveOrEditSuppCostTb()
            Next
        End If
        MsgBox("Cost successfully Posted", MsgBoxStyle.Information)
        If isSupp Then
            ldSupp()
        Else
            ldItem()
        End If
    End Sub

    Private Sub chkitemcost_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkitemcost.CheckedChanged
        If chgpgm Then Exit Sub
        If isSupp Then
            ldSupp()
        Else
            ldItem()
        End If
    End Sub

    Private Sub btnsearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsearch.Click
        Dim i As Integer
        Dim txt As String
        For i = Val(txtSeq.Tag) To grdItem.RowCount - 1
            txt = UCase(grdItem.Item(0, i).Value)
            If txt.Contains(UCase(txtSeq.Text)) Then
                grdItem.CurrentCell = grdItem.Item(1, i)
                If i = grdItem.RowCount - 1 Then
                    txtSeq.Tag = 0
                Else
                    txtSeq.Tag = i + 1
                End If

                Exit For
            End If
        Next
    End Sub
End Class