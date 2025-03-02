
Public Class ItmEnqry
    Private chgbyprg As Boolean
    Dim srchTxtEdtd As Boolean
    Dim srchTxtId As Single
    Dim srchOnce As Boolean
    Public fromItemMast As Boolean
    Public Ititemid As Boolean
    Public isproduction As Boolean
    Private WithEvents fproductMast As ItemMastFrm
    Private Const ItemCode = 0
    Private Const TrDescr = 1
    Private Const Rack = 2
    Private Const QtyInHand = 3
    Private Const UnitPrice = 4
    Private Const mrp = 5
    Private Const taxprice = 6
    Private Const wsp = 7
    Private Const costtax = 8
    Private Const ItemId = 9
    Private Const isacc = 10
    Public Event CreateItem()
    Public isVazhipadusales As Boolean
    Public isFromPurchase As Boolean
    Public hideRoom As Boolean
    '0 MRP,0 [Tax Price],0 WSP,0 [Cost+Tax]
    'object declarations
    Dim _objcmnbLayer As New clsCommon_BL
    Private WithEvents fwait As WaitMessageFrm
    Private WithEvents fQuickProd As New QuickItem
    Public Event getSelected(ByVal ItemcODE As String, ByVal isacc As Integer)

    Private Sub ItmEnqry_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        chgbyprg = True
        cmbQIH.SelectedIndex = 0
        'FillGrid()
        txtSearch.Select()
        chkSearch.Checked = searchStartOnly
        chgbyprg = True
        chkfulltext.Checked = searchfulltext
        chgbyprg = False
        Timer1.Enabled = True
    End Sub
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
        If isFromPurchase Then
            strCondition = " WHERE itemCategory<>'Menu Item'"
        End If
        If strCondition = "" Then
            strCondition = " WHERE"
        Else
            strCondition = strCondition & " AND"
        End If
        If chkSearch.Checked Then
            strCondition = strCondition & " ([Item Code] Like '" & txtSearch.Text & "%' OR Barcode Like  '" & txtSearch.Text & "%' or Description Like '" & txtSearch.Text & "%') "
        Else
            strCondition = strCondition & " ([Item Code] Like '%" & txtSearch.Text & "%' OR Barcode Like  '%" & txtSearch.Text & "%' or Description Like '%" & txtSearch.Text & "%') "
        End If
        If hideRoom Then
            strCondition = strCondition & " AND itemCategory<>'room'"
        End If
        strQry = "select  [Item Code] as ItemCode ,Barcode,Description,Model,UType,Unit,[Sales Price] as SalesPrice, [WS Price] as WholeSalesPrice,  QtyInHand as QIH,CostAverage, " & _
                 "[Opn Cost] as OpnCost, [Opn Qty] as OpnQty, ItemId,BaseID from  ( Select [Item Code],Barcode,Description,Model,'B' as UType,Unit,UnitPrice AS [Sales Price]," & _
                 "UnitPriceWS as [WS Price], (RcvdQty+QtyOPn - IssdQty) as  QtyInHand,CostAverage,CostOpen as [Opn Cost],QtyOpn as [Opn Qty], ItemId,BaseID  from BaseItmDet  " & _
                 "INNER JOIN InvItm ON BaseItmDet.BaseItemID=InvItm.ItemId   union all  Select  [Item Code],Barcode,TrDescription,Model ,'P' as UType,Unit,UnitPrice AS " & _
                 "[Sales Price],UnitPriceWS as [WS Price],  (RcvdQty+QtyOPn - IssdQty)/(Vdown/Vup),CostAverage * (Vdown/Vup),CostOpen *(Vdown/Vup) as [Opn Cost]," & _
                 "QtyOpn/(Vdown/Vup) as [Opn Qty], ItemId,BaseID  from InvItm  INNER JOIN BaseItmDet ON InvItm.BaseId =BaseItmDet.BaseItemID and ItemId<>BaseID) as q  " & strCondition & "  order by baseid,itemid "
        bDatatable = _objcmnbLayer._fldDatatable(strQry)
nxt:
        Return bDatatable
    End Function
    Private Sub FillGrid()
        grdPack.VirtualMode = True
        Dim strQry As String
        grdPack.DataSource = Nothing
        If isVazhipadusales Then
            strQry = "SELECT * FROM (select [Item Code] as ItemCode ,Description," & _
            "'' Rack,QIH,UnitPrice," & _
            "0 MRP,0 [Tax Price],0 WSP,0 [Cost+Tax]," & _
            "ItemId,0 isacc from  InvItm UNION ALL " & _
                "SELECT Alias,AccDescr,'',0,VazhipaduRate,0 MRP,0 [Tax Price],0 WSP,0 [Cost+Tax],AccId,1 " & _
                "FROM AccMast left join S1AccHd on S1AccHd.S1AccId=AccMast.S1AccId where GrpSetOn='Vazhipadu')ITM   order by isacc Desc,Description "
        ElseIf isproduction Then
            strQry = "select  [Item Code] as ItemCode ,Description,Rack, QIH,UnitPrice,0 MRP,0 [Tax Price],0 WSP,0 [Cost+Tax], InvItm.ItemId from  InvItm " & _
                    "LEFT JOIN InvItmPropertiesTb ON InvItmPropertiesTb.Itemid=InvItm.Itemid where isnull(ishide,0)=0 AND ismanufacturing=1  order by itemid "
            'ElseIf isFromPurchase Then
            '    strQry = "select  [Item Code] as ItemCode ,Description,Rack, QIH,UnitPrice,  MRP,0 [Tax Price],0 WSP,0 [Cost+Tax], ItemId from  InvItm where itemCategory<>'Menu Item' order by itemid "
            'ElseIf hideRoom Then
            '    strQry = "select  [Item Code] as ItemCode ,Description, QIH,UnitPrice, ItemId from  InvItm where itemCategory<>'room'  order by itemid "
        Else
            strQry = "select  [Item Code] as ItemCode ,Description,Rack, QIH,UnitPrice,MRP,(((isnull(IGST,1)+ISNULL(vat,0))*UnitPrice)/100)+UnitPrice [Tax Price]," & _
            "UnitPriceWS WSP," & _
             "((isnull(IGST,1)*CostAvg)/100)+CostAvg [Cost+Tax]," & _
            " ItemId from  InvItm" & _
             " LEFT JOIN GSTTb ON GSTTb.HSNCode=InvItm.HSNCode " & _
             " LEFT JOIN VatMasterTb ON VatMasterTb.vatid=InvItm.vatid WHERE isnull(ishide,0)=0 " & _
            IIf(hideRoom, " AND itemCategory<>'room'", "") & _
            "  order by itemid "
        End If
        If dtItmTable Is Nothing Then
            dtItmTable = _objcmnbLayer._fldDatatable(strQry)
        End If
        grdPack.DataSource = dtItmTable
        SetGridHead()
        Timer2.Enabled = True
        chkSearchBycode.Checked = searchByCodeInInventory
    End Sub

    Private Sub SetGridHead()
        With grdPack
            SetGridProperty(grdPack)

            .Columns(ItemCode).HeaderText = "Item Code"
            .Columns(ItemCode).Width = 150

            .Columns(TrDescr).HeaderText = "Description"
            .Columns(TrDescr).Width = 400
            .Columns(TrDescr).SortMode = DataGridViewColumnSortMode.NotSortable

            .Columns(Rack).HeaderText = "Rack"
            .Columns(Rack).Width = 70

            .Columns(QtyInHand).HeaderText = "QIH"
            .Columns(QtyInHand).Width = 70
            .Columns(QtyInHand).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(QtyInHand).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(UnitPrice).HeaderText = "Price"
            .Columns(UnitPrice).Width = 70
            .Columns(UnitPrice).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(UnitPrice).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(mrp).HeaderText = "MRP"
            .Columns(mrp).Width = 70
            .Columns(mrp).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(mrp).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(taxprice).HeaderText = "Tax Price"
            .Columns(taxprice).Width = 70
            .Columns(taxprice).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(taxprice).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(wsp).HeaderText = "WS. Price"
            .Columns(wsp).Width = 70
            .Columns(wsp).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(wsp).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(costtax).HeaderText = "Cost + Tax"
            .Columns(costtax).Width = 70
            .Columns(costtax).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(costtax).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


            .Columns(ItemId).HeaderText = "ItemId"
            .Columns(ItemId).Visible = False
            If isVazhipadusales Then
                .Columns(isacc).Visible = False
                .Columns(costtax).Visible = False
                .Columns(wsp).Visible = False
                .Columns(taxprice).Visible = False
                .Columns(mrp).Visible = False
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
                Timer2.Enabled = True
            Case Keys.Down, Keys.PageDown
                MoveDown()
                Timer2.Enabled = True
            Case Keys.Return
                If enableItemAutoPopulate And txtSearch.Text = "" Then
                    Me.Close()
                ElseIf chkfulltext.Checked And Val(txtSearch.Tag) = 0 Then
                    ldTimer.Enabled = True
                    txtSearch.Tag = 1
                ElseIf grdPack.Rows.Count > 0 And Val(txtSearch.Tag) = 1 Then
                    BtnSelect_Click(sender, e)
                End If

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
        If chkfulltext.Checked = False Then
            ldTimer.Enabled = False
            ldTimer.Enabled = True
        End If
        If chkfulltext.Checked Then
            txtSearch.Tag = 0
        Else
            txtSearch.Tag = 1
        End If
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
        'Timer1.Enabled = False
        'If srchOnce = False Then
        '    chgbyprg = True
        'End If
        'Select Case srchTxtId
        '    Case 5  ' 
        '        FillGrid()
        'End Select
        'srchOnce = True
        'chgbyprg = False
        Timer1.Enabled = False
        loadWaite(1)
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
       
    End Sub


    Private Sub BtnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSelect.Click
        If grdPack.RowCount < 1 Then Exit Sub
        Dim TMPiTMcODE As String
        Dim isaccToselect As Integer
        Dim rindex As Integer
        If Not grdPack.CurrentRow Is Nothing Then
            rindex = grdPack.CurrentRow.Index
        Else
            rindex = 0
        End If

        With grdPack
            TMPiTMcODE = .Item(ItemCode, rindex).Value()
            If isVazhipadusales Then
                isaccToselect = Val(grdPack.Item(isacc, rindex).Value)
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

    Private Sub grdPack_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPack.CellClick
        Timer2.Enabled = True
    End Sub

    Private Sub grdPack_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPack.CellDoubleClick
        BtnSelect_Click(BtnSelect, New System.EventArgs())
    End Sub

    Private Sub ldTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ldTimer.Tick
        ldTimer.Enabled = False
        If chkfulltext.Checked Then
            grdPack.DataSource = SearchGrid(dtItmTable, Trim(txtSearch.Text), cmbSearch.SelectedIndex, Not chkSearch.Checked, , True)
        Else
            grdPack.DataSource = SearchGrid(dtItmTable, Trim(txtSearch.Text), cmbSearch.SelectedIndex, Not chkSearch.Checked)
        End If
        Timer2.Enabled = True
    End Sub
    Private Sub grdPack_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdPack.KeyDown
        If e.KeyCode = Keys.Enter Then BtnSelect_Click(BtnSelect, New System.EventArgs)
    End Sub

    Private Sub btnCreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        'RaiseEvent CreateItem()
        'Me.Close()
        Try
            fQuickProd.dontClose = True
            fQuickProd.Show()
            Me.Hide()
        Catch ex As Exception
            'MsgBox(clsmn.ReturnErrorMsg("btnNew_Click [frmItmEnq] ", ex.Message), MsgBoxStyle.Critical)
        End Try

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
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            Const WM_KEYDOWN As Integer = &H100
            Const WM_KEYUP As Integer = &H101
            'msg.ToString()
            If msg.Msg = WM_KEYDOWN Or msg.Msg = WM_KEYUP Then
                If msg.WParam.ToInt32() = CInt(Keys.F3) Then
                   
                    RaiseEvent CreateItem()
                    Me.Close()
                ElseIf msg.WParam.ToInt32() = CInt(Keys.Escape) Then
                    Me.Close()
                End If
            End If
ctn:
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function


    Private Sub chkSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSearch.Click
        _objcmnbLayer._saveDatawithOutParm("Update CompanyTb set searchStartOnly =" & IIf(chkSearch.Checked, 1, 0))
        searchStartOnly = chkSearch.Checked
    End Sub
    Private Sub loadWaite(ByVal triggerType As Integer)
        If Not fwait Is Nothing Then fwait.Close() : fwait = Nothing
        fwait = New WaitMessageFrm
        With fwait
            .triggerType = triggerType
            .Show()
        End With
    End Sub

    Private Sub fwait_triggerEvent() Handles fwait.triggerEvent
        Select Case fwait.triggerType
            Case 1
                FillGrid()
        End Select
        If Not fwait Is Nothing Then
            fwait.Close()
            fwait = Nothing
        End If
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Timer2.Enabled = False
        Dim str As String
        If Val(grdPack.RowCount) = 0 Then Exit Sub
        If grdPack.CurrentRow Is Nothing Then Exit Sub
        Dim preitemid As Long = Val(grdPack.Item("ItemId", grdPack.CurrentRow.Index).Value)
        str = "Select itm.*,isnull(Locqty,0) Qty from (Select " & _
             "case when " & preitemid & "=Parentitemid then childcode else Parentcode end Code, " & _
              "case when " & preitemid & "=Parentitemid then childname else Parentname end Name," & _
              "case when " & preitemid & "=Parentitemid then ChildItemid else Parentitemid end sitemid," & _
             "supersedid  from SupersedItemsTb " & _
             "left join (select [Item Code] childcode,Description childname,itemid childid from invitm)child on child.childid=SupersedItemsTb.ChildItemid " & _
             "left join (select [Item Code] Parentcode,Description Parentname,itemid Parentid from invitm)Parent on Parent.Parentid=SupersedItemsTb.Parentitemid where Parentitemid=" & preitemid & " OR childid=" & preitemid & ")itm " & _
            "left join  (SELECT SUM(CASE WHEN InvType in ('IN') then 1 else -1 end  * isnull(qty,0)) Locqty ,Itemid from " & _
            "(SELECT 'OUT' InvType,case when doctype='DOC' AND 0='0' then 0 else qty end qty,0 TrDateNo,Itemid " & _
            "FROM DocTranTb LEFT JOIN DocCmnTb ON  DocTranTb.DocId=DocCmnTb.DocId where doctype in ('DOC','MTN') " & _
            "UNION ALL " & _
           "SELECT 'IN' InvType,case when doctype='DOC' AND 0='0' then 0 else qty end qty,0 TrDateNo,Itemid FROM DocTranTb " & _
           "LEFT JOIN DocCmnTb ON  DocTranTb.DocId=DocCmnTb.DocId where doctype in ('DOS','MTN') " & _
           " UNION ALL " & _
           "SELECT InvType,trqty+isnull(focqty,0) trqty,TrDateNo,Itemid FROM ItmInvTrtb LEFT JOIN VoucherTypeNoTb ON ItmInvTrTb.TrTypeNo=VoucherTypeNoTb.vrno " & _
           "LEFT JOIN ItmInvCmnTb ON  ItmInvTrTb.TRID=ItmInvCmnTb.TRID  WHERE isnull(invStatus,0)=0)Loc group by Itemid)tr " & _
           "on itm.sitemid=tr.itemid "

        str = str & " select [item code],[description],isnull(loc,'')Location,isnull(Locqty,0)QTY from invitm " & _
            "left join  (SELECT SUM(CASE WHEN InvType in ('IN') then 1 else -1 end  * isnull(qty,0)) Locqty ,Itemid ,FromLoc loc from " & _
            "(SELECT 'IN' InvType,isnull(LocOpnQtyTb.qty,0)qty,0 TrDateNo,Itemid,LocCode FromLoc FROM  LocOpnQtyTb " & _
            "LEFT JOIN LocationTb ON LocationTb.locationid=LocOpnQtyTb.locationid " & _
            "UNION ALL " & _
            "SELECT 'OUT' InvType,case when doctype='DOC' AND 0='0' then 0 else qty end qty,0 TrDateNo,Itemid,FromLoc " & _
            "FROM DocTranTb LEFT JOIN DocCmnTb ON  DocTranTb.DocId=DocCmnTb.DocId where doctype in ('DOC','MTN') " & _
            "UNION ALL " & _
           "SELECT 'IN' InvType,case when doctype='DOC' AND 0='0' then 0 else qty end qty,0 TrDateNo,Itemid,DocDefLoc FROM DocTranTb " & _
           "LEFT JOIN DocCmnTb ON  DocTranTb.DocId=DocCmnTb.DocId where doctype in ('DOS','MTN') " & _
           " UNION ALL " & _
           "SELECT InvType,trqty+isnull(focqty,0) trqty,TrDateNo,Itemid,DocDefLoc FROM ItmInvTrtb LEFT JOIN VoucherTypeNoTb ON ItmInvTrTb.TrTypeNo=VoucherTypeNoTb.vrno " & _
           "LEFT JOIN ItmInvCmnTb ON  ItmInvTrTb.TRID=ItmInvCmnTb.TRID  WHERE isnull(invStatus,0)=0)Loc where isnull(FromLoc,'')<>'' group by Itemid,FromLoc)tr " & _
           "on invitm.itemid=tr.itemid	where invitm.Itemid=" & preitemid
        Dim dtset As DataSet
        dtset = _objcmnbLayer._ldDataset(str, False)
        grdsupersed.DataSource = dtset.Tables(0)
        grdLocation.DataSource = dtset.Tables(1)
        With grdLocation
            SetGridProperty(grdLocation)
            .Columns(0).Visible = False
            .Columns(1).Visible = False
            .Columns(3).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            resizeGridColumn(grdLocation, 2)
        End With
        SetGridProperty(grdsupersed)
        With grdsupersed
            .Columns("supersedid").Visible = False
            .Columns("sitemid").Visible = False
            .Columns("Qty").Width = 50
            .Columns("Qty").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Qty").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        resizeGridColumn(grdsupersed, 1)
    End Sub

    Private Sub chkfulltext_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkfulltext.CheckedChanged
        If chgbyprg Then Exit Sub
        If chkfulltext.Checked Then
            _objcmnbLayer._saveDatawithOutParm("Update CompanyTb set searchfulltext=1")
            searchfulltext = True
        Else
            _objcmnbLayer._saveDatawithOutParm("Update CompanyTb set searchfulltext=0")
            searchfulltext = False
        End If
    End Sub

    Private Sub chkSearchBycode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSearchBycode.CheckedChanged
        If chkSearchBycode.Checked Then
            cmbSearch.SelectedIndex = 0
        Else
            cmbSearch.SelectedIndex = 1
        End If
        If chgbyprg Then Exit Sub
        If chkSearchBycode.Checked Then
            _objcmnbLayer._saveDatawithOutParm("Update CompanyTb set searchByCodeInInventory=1")
            searchByCodeInInventory = True
        Else
            _objcmnbLayer._saveDatawithOutParm("Update CompanyTb set searchByCodeInInventory=0")
            searchByCodeInInventory = False
        End If
    End Sub

    Private Sub grdsupersed_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdsupersed.DoubleClick
        If grdsupersed.RowCount < 1 Then Exit Sub
        Dim TMPiTMcODE As String
        Dim rindex As Integer
        If Not grdsupersed.CurrentRow Is Nothing Then
            rindex = grdsupersed.CurrentRow.Index
        Else
            rindex = 0
        End If

        With grdsupersed
            TMPiTMcODE = .Item("Code", rindex).Value()
            If TMPiTMcODE = "" Then Exit Sub
        End With
        'Me.Close()
        RaiseEvent getSelected(TMPiTMcODE, 0)
    End Sub

    Private Sub grdPack_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPack.CellContentClick

    End Sub
End Class
