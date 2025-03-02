
Public Class ItmEnqrytemple
    Private chgbyprg As Boolean
    Dim dtTable As DataTable
    Dim srchTxtEdtd As Boolean
    Dim srchTxtId As Single
    Dim srchOnce As Boolean
    Public fromItemMast As Boolean
    Public Ititemid As Boolean
    Public isproduction As Boolean
    Private WithEvents fproductMast As ItemMastFrm
    Private Const ItemCode = 0
    Private Const TrDescr = 1
    Private Const QtyInHand = 2
    Private Const UnitPrice = 3
    Private Const ItemId = 4
    Private Const isacc = 5
    Public Event CreateItem()
    Public isVazhipadusales As Boolean
    Public isFromPurchase As Boolean
    Public hideRoom As Boolean
    '0 MRP,0 [Tax Price],0 WSP,0 [Cost+Tax]
    'object declarations
    Dim _objcmnbLayer As New clsCommon_BL

    Private WithEvents fQuickProd As New QuickItem
    Public Event getSelected(ByVal ItemcODE As String, ByVal isacc As Integer)


    Public Function GlobalSearch() As DataTable
        Dim bDatatable As DataTable

        Dim strQry As String
        Dim strCondition As String
        Select Case cmbQIH.SelectedIndex
            Case 0
                strCondition = ""
            Case 1 '+ve
                strCondition = " Where QtyInHand >0 "
            Case 2  ' -ve
                strCondition = " Where QtyInHand <0 "
            Case 3  ' 0
                strCondition = " Where QtyInHand =0 "
            Case 4 ' 0+ve
                strCondition = " Where QtyInHand >=0 "
            Case 5  '0-ve
                strCondition = " Where QtyInHand <=0 "
            Case 6  ' Non zero
                strCondition = " Where QtyInHand <> 0 "
            Case Else
                strCondition = ""
        End Select
        
        If strCondition = "" Then
            strCondition = " WHERE"
        Else
            strCondition = strCondition & " AND"
        End If
        If chkSearch.Checked Then
            strCondition = strCondition & " ([Item Code] Like '" & txtSearch.Text & "%' or Description Like '" & txtSearch.Text & "%') "
        Else
            strCondition = strCondition & " ([Item Code] Like '%" & txtSearch.Text & "%' OR Description Like '%" & txtSearch.Text & "%') "
        End If
        If hideRoom Then
            strCondition = strCondition & " AND itemCategory<>'room'"
        End If
        'strQry = "select  [Item Code] as ItemCode ,Barcode,Description,Model,UType,Unit,[Sales Price] as SalesPrice, [WS Price] as WholeSalesPrice,  QtyInHand as QIH,CostAverage, " & _
        '         "[Opn Cost] as OpnCost, [Opn Qty] as OpnQty, ItemId,BaseID from  ( Select [Item Code],Barcode,Description,Model,'B' as UType,Unit,UnitPrice AS [Sales Price]," & _
        '         "UnitPriceWS as [WS Price], (RcvdQty+QtyOPn - IssdQty) as  QtyInHand,CostAverage,CostOpen as [Opn Cost],QtyOpn as [Opn Qty], ItemId,BaseID  from BaseItmDet  " & _
        '         "INNER JOIN InvItm ON BaseItmDet.BaseItemID=InvItm.ItemId   union all  Select  [Item Code],Barcode,TrDescription,Model ,'P' as UType,Unit,UnitPrice AS " & _
        '         "[Sales Price],UnitPriceWS as [WS Price],  (RcvdQty+QtyOPn - IssdQty)/(Vdown/Vup),CostAverage * (Vdown/Vup),CostOpen *(Vdown/Vup) as [Opn Cost]," & _
        '         "QtyOpn/(Vdown/Vup) as [Opn Qty], ItemId,BaseID  from InvItm  INNER JOIN BaseItmDet ON InvItm.BaseId =BaseItmDet.BaseItemID and ItemId<>BaseID) as q  " & strCondition & "  order by baseid,itemid "

        strQry = "SELECT * FROM (select [Item Code] as ItemCode ,Description, QIH,UnitPrice,ItemId,0 isacc from  InvItm UNION ALL " & _
                "SELECT Alias,AccDescr,0,VazhipaduRate,AccId,1 FROM AccMast left join S1AccHd on S1AccHd.S1AccId=AccMast.S1AccId where GrpSetOn='Vazhipadu')ITM  " & strCondition & " order by isacc Desc,[Description] "

        bDatatable = _objcmnbLayer._fldDatatable(strQry)
nxt:
        Return bDatatable
    End Function
    Private Sub FillGrid()
        grdPack.VirtualMode = True
        Dim strQry As String
        grdPack.DataSource = Nothing
        strQry = "SELECT * FROM (select [Item Code] as ItemCode ,Description, QIH,UnitPrice,ItemId,0 isacc from  InvItm UNION ALL " & _
                "SELECT Alias,AccDescr,0,VazhipaduRate,AccId,1 FROM AccMast left join S1AccHd on S1AccHd.S1AccId=AccMast.S1AccId where GrpSetOn='Vazhipadu')ITM   order by isacc Desc,[Description] "

        dtTable = _objcmnbLayer._fldDatatable(strQry)
        grdPack.DataSource = dtTable
        SetGridHead()
    End Sub
    Private Sub SetGridHead()
        With grdPack
            SetGridProperty(grdPack)

            .Columns(ItemCode).HeaderText = "Item Code"
            .Columns(ItemCode).Width = 150

            .Columns(TrDescr).HeaderText = "Description"
            .Columns(TrDescr).Width = 400
            .Columns(TrDescr).SortMode = DataGridViewColumnSortMode.NotSortable

            .Columns(QtyInHand).HeaderText = "QIH"
            .Columns(QtyInHand).Width = 70
            .Columns(QtyInHand).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(QtyInHand).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(UnitPrice).HeaderText = "Price"
            .Columns(UnitPrice).Width = 70
            .Columns(UnitPrice).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(UnitPrice).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(ItemId).HeaderText = "ItemId"
            .Columns(ItemId).Visible = False
            If isVazhipadusales Then
                .Columns(isacc).Visible = False
            End If


            cmbSearch.Items.Clear()
            Dim i As Integer = 0
            For i = 0 To grdPack.ColumnCount - 1
                cmbSearch.Items.Add(grdPack.Columns(i).HeaderText)
            Next
            resizeGridColumn(grdPack, TrDescr)
            cmbSearch.SelectedIndex = 1
        End With
    End Sub

    Private Sub txtSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
        Select Case e.KeyCode
            Case Keys.Up, Keys.PageUp
                MoveUp()
            Case Keys.Down, Keys.PageDown
                MoveDown()
            Case Keys.Return
                BtnSelect_Click(sender, e)
        End Select
    End Sub
    Public Sub MoveUp()
        Dim r As Integer
        With grdPack
            If .RowCount < 1 Then Exit Sub
            If .CurrentRow.Index < 0 Then Exit Sub
            r = .CurrentRow.Index
            If r <> 0 Then
                r = r - 1
                If Not r < 0 Then
                    .Rows(r).Selected = True
                    .CurrentCell = .Item(cmbSearch.SelectedIndex, r)
                    .FirstDisplayedScrollingRowIndex() = r
                End If
            End If
        End With
    End Sub
    Public Sub MoveDown()
        On Error Resume Next
        Dim r As Integer
        With grdPack
            If .RowCount < 1 Then Exit Sub
            If .CurrentRow.Index = .RowCount - 1 Then GoTo Slct
            r = .CurrentRow.Index
            If .CurrentRow.Index < .RowCount - 1 Then
                r = r + 1
                .Rows(r).Selected = True
                .CurrentCell = .Item(cmbSearch.SelectedIndex, r)
                .FirstDisplayedScrollingRowIndex() = r
            Else
                Exit Sub
            End If
Slct:
            'MsgBox(dvData.CurrentRow.Index)
        End With
    End Sub
    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        'grdPack.DataSource = SearchGrid(dtTable, Trim(txtSearch.Text), cmbSearch.SelectedIndex)
        ldTimer.Enabled = False
        ldTimer.Enabled = True
    End Sub

    Private Sub cmbQIH_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbQIH.SelectedIndexChanged
        If chgbyprg Then Exit Sub
        srchTxtEdtd = True
        srchTxtId = 5
        Timer1.Enabled = False
        Timer1.Enabled = True
        srchOnce = False
        'doCommandStat(True)
        chgbyprg = True
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        If srchOnce = False Then
            chgbyprg = True
        End If
        Select Case srchTxtId
            Case 5  ' 
                FillGrid()
        End Select
        srchOnce = True
        chgbyprg = False
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            fQuickProd.dontClose = True
            fQuickProd.Show()
            Me.Hide()
        Catch ex As Exception
            'MsgBox(clsmn.ReturnErrorMsg("btnNew_Click [frmItmEnq] ", ex.Message), MsgBoxStyle.Critical)
        End Try

    End Sub


    Private Sub BtnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSelect.Click
        If grdPack.RowCount < 1 Then Exit Sub
        Dim TMPiTMcODE As String
        Dim isaccToselect As Integer
        With grdPack
            TMPiTMcODE = .Item(ItemCode, .CurrentRow.Index).Value()
            If isVazhipadusales Then
                isaccToselect = Val(grdPack.Item(isacc, grdPack.CurrentRow.Index).Value)
            Else
                isaccToselect = 0
            End If
            If TMPiTMcODE = "" Then Exit Sub
        End With
        'Me.Close()
        RaiseEvent getSelected(TMPiTMcODE, isaccToselect)
    End Sub

    Private Sub BtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRefresh.Click
        FillGrid()
    End Sub

    Private Sub BtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub

    Private Sub grdPack_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPack.CellContentClick

    End Sub

    Private Sub grdPack_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPack.CellDoubleClick
        BtnSelect_Click(BtnSelect, New System.EventArgs())
    End Sub

    Private Sub ldTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ldTimer.Tick
        ldTimer.Enabled = False
        If chkGlobalsearch.Checked Then
            grdPack.DataSource = GlobalSearch()
        Else
            grdPack.DataSource = SearchGrid(dtTable, Trim(txtSearch.Text), cmbSearch.SelectedIndex, Not chkSearch.Checked)
        End If
    End Sub

    Private Sub grdPack_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdPack.KeyDown
        If e.KeyCode = Keys.Enter Then BtnSelect_Click(BtnSelect, New System.EventArgs)
    End Sub

    Private Sub btnCreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        RaiseEvent CreateItem()
        Me.Close()
    End Sub

    Private Sub fQuickProd_PassData(ByVal ItemCode As String, ByVal ismode As Boolean) Handles fQuickProd.PassData
        Try
            RaiseEvent getSelected(fQuickProd.txtCode.Text, 0)
            fQuickProd.Close()
            fQuickProd = Nothing
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ItmEnqrytemple_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        chgbyprg = True
        cmbQIH.SelectedIndex = 0
        FillGrid()
        txtSearch.Select()
    End Sub
End Class