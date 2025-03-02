
Public Class QtyReport
    'object declarations
    Private _objcmnbLayer As New clsCommon_BL
    Private _objReport As New clsReport_BL
    Private srchTxtEdtd As Boolean
    Private srchTxtId As Single
    Private forSingle As Boolean
    Private chgbyprg As Boolean
    Private srchOnce As Boolean
    Private strSearch As String
    Private opt As Single
    Dim Type_rd As String = ""

    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fRptFormat As RptFormatfrm
    Private WithEvents fwait As WaitMessageFrm
    Private WithEvents fshowlocationqty As ShowLocationQtyFrm
    Private StrRptQry As String
    Private DtNo As Long

    Private Const TAGITEM = 0
    Private Const ItemCode = 1
    Private Const Barcode = 2
    Private Const Description = 3
    Private Const Unit = 4
    Private Const itemCategory = 5
    Private Const rack = 6
    Private Const opQty = 8
    Private Const QIH = 7
    Private Const RcvdQty = 9
    Private Const IssdQty = 10
    Private Const opcost = 11
    Private Const CostAvg = 12
    Private Const LastPurchCost = 13
    Private Const LastPurchaseWithoutOthercost = 14
    Private Const LastPurchaseOthercost = 15

    Private Const UnitPrice = 15
    Private Const P1Qty = 16
    Private Const P2Qty = 17
    Private Const CreatedBy = 18
    Private Const ModiBy = 19
    Private Const Tval = 20
    Private Const ItemId = 21

    Private dtTable As DataTable
    Private cmnCondition As String
    Private lvl As String
    Private Sub FillGrid()
        If Me.IsInitializing Then Exit Sub
        Dim strStockCondition As String = ""
        Select Case cmbQIH.SelectedIndex
            Case 1 '+ve
                strStockCondition = " Where AsOnQty >0 "
            Case 2  ' -ve
                strStockCondition = " Where AsOnQty <0 "
            Case 3  ' 0
                strStockCondition = " Where AsOnQty =0 "
            Case 4 ' 0+ve
                strStockCondition = " Where AsOnQty >=0 "
            Case 5  '0-ve
                strStockCondition = " Where AsOnQty <=0 "
            Case 6  ' Non zero
                strStockCondition = " Where AsOnQty <> 0 "
            Case Else
                strStockCondition = ""
        End Select

        If strStockCondition = "" Then
            strStockCondition = " WHERE isnull(ishide,0)=0 AND "
        Else
            strStockCondition = strStockCondition & " AND isnull(ishide,0)=0  AND "
        End If
        If rdStock.Checked Then
            strStockCondition = strStockCondition & " isnull(itemCategory,'Stock')= 'Stock'"
        ElseIf rdService.Checked Then
            strStockCondition = strStockCondition & " isnull(itemCategory,'Stock')= 'Service'"
        ElseIf rdBoth.Checked Then
            strStockCondition = strStockCondition & " isnull(itemCategory,'Stock')= 'Service' or isnull(itemCategory,'')= 'Stock' or isnull(itemCategory,'')= 'Menu Item'"
        ElseIf rdomenu.Checked Then
            strStockCondition = strStockCondition & " isnull(itemCategory,'Stock')= 'Menu Item'"
        End If
        If cmbitemtype.SelectedIndex = 0 Then
            strStockCondition = strStockCondition & " And isnull(ismanufacturing,0)=1"
        ElseIf cmbitemtype.SelectedIndex = 1 Then
            strStockCondition = strStockCondition & " And isnull(ismanufacturing,0)<>1"
        End If
        Dim lvl As String = ""
        If chkLevelWise.Checked Then
            If chkLevelWise.Checked Then
                Dim i As Integer
                For i = 0 To grdLevel.RowCount - 1
                    If grdLevel.Item(1, i).Value <> "" Then
                        lvl = lvl & IIf(lvl = "", "", " AND ") & " Lvl" & i + 1 & "='" & grdLevel.Item(1, i).Value & "'"
                    End If
                Next
            End If
            strStockCondition = strStockCondition & IIf(lvl = "", "", " AND ") & lvl
        End If
        'strStockCondition = strStockCondition & IIf(lvl = "", "", " AND " & lvl)
        grdvoucher.VirtualMode = True
        grdvoucher.DataSource = Nothing
        Dim flds As String
        'SELECT 'OUT' InvType,qty,0 TrDateNo,Itemid,FromLoc FROM DocTranTb 
        'LEFT JOIN DocCmnTb ON  DocTranTb.DocId=DocCmnTb.DocId
        '        union(all)
        'SELECT 'OUT' InvType,qty,0 TrDateNo,Itemid,DocDefLoc FROM DocTranTb 
        'LEFT JOIN DocCmnTb ON  DocTranTb.DocId=DocCmnTb.DocId

        flds = "'' as Tag,[Item Code] as ItemCode ,mechineItemcode,InvItm.Description,Unit,itemCategory,Rack," & _
               IIf(UsrBr = "", "isnull(AsOnQty,0)+isnull(opQty,0) AsOnQty ,opQty", "isnull(Locqty,0) AsOnQty,isnull(locopnQty,0) opQty") & _
               ",isnull(RQty,0) RcvdQty,isnull(IQty,0) IssdQty,opcost," & IIf(UsrBr = "", "CostAvg", "isnull(locationCost,0)") & " CostAvg," & _
               IIf(UsrBr = "", "LastPurchCost", "isnull(loclastcost,0)") & " LastPurchCost," & _
                "LastPurchaseWithoutOthercost,LastPurchaseOthercost," & _
                "UnitPrice,secondPrice,UnitPriceWS," & _
                IIf(UsrBr = "", "(isnull(AsOnQty,0)+opQty)", "isnull(Locqty,0)") & "/case when P1Fra=0 then 1 else P1Fra end P1Qty," & _
                IIf(UsrBr = "", "(isnull(AsOnQty,0)+opQty)", "isnull(Locqty,0)") & "/case when P2Fra=0 then 1 else P2Fra end P2Qty," & _
                "CreatedBy,ModiBy," & _
                IIf(UsrBr = "", "(isnull(AsOnQty,0)+opQty)", "isnull(Locqty,0)") & "*CostAvg tValue,invitm.itemid,  " & _
                "Lvl1,Lvl2,Lvl3,Lvl5,Lvl6,Lvl7,Lvl8,Lvl9,Lvl10,ismanufacturing,isnull(ishide,0) ishide " & _
                IIf(UsrBr = "", "", ",Locqty,loc,loclastcost,locopnQty,locationCost ")
        dtTable = _objReport.returnItemQuantityReport(strStockCondition, flds, "returnItemQuantityReport", _
                                                      getDateNo(dtpto.Value), DateValue(dtpto.Value), 0, IIf(chklocationwise.Checked, 1, 0)).Tables(0)
        grdvoucher.DataSource = dtTable
        'Dim Ttl As Double
        'If dtTable.Rows.Count > 0 Then
        '    Ttl = CDbl(dtTable.Compute("sum(AsOnQty)", String.Empty))
        '    lbltotalValue.Text = ("QIH : " & Strings.Format(Ttl, numFormat))
        '    Ttl = CDbl(dtTable.Compute("sum(tValue)", String.Empty))
        '    lbltotalValue.Text = lbltotalValue.Text & " / Total Value : " & Format(Ttl, numFormat)
        'End If
        totalValue(dtTable)

        SetGridHead()
        setComboGrid()
        'totalValue()
        Me.Cursor = Cursors.Default
        Timer2.Enabled = True
    End Sub

    Private Function getLevelId(ByVal levelname As String) As Integer
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT UnqGrpId FROM GrpItmTb WHERE GrpItmCode='" & levelname & "'")
        If dt.Rows.Count > 0 Then
            Return (Val(dt(0)(0)))
        End If
    End Function

    Private Sub SetGridHead()
        If Me.IsInitializing Then Exit Sub
        With grdvoucher
            SetGridProperty(grdvoucher)

            .Columns(TAGITEM).ReadOnly = False
            .Columns(TAGITEM).Width = 35
            .Columns(TAGITEM).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns(ItemCode).HeaderText = "Item Code"
            .Columns(ItemCode).Width = 100
            .Columns(ItemCode).ReadOnly = True

            .Columns(Barcode).HeaderText = "Barcode"
            .Columns(Barcode).Width = 100
            .Columns(Barcode).ReadOnly = True

            .Columns(Description).HeaderText = "Description"
            .Columns(Description).Width = 200
            .Columns(Description).ReadOnly = True
            .Columns(Description).Frozen = True

            .Columns(Unit).HeaderText = "Unit"
            .Columns(Unit).Width = 40
            .Columns(Unit).ReadOnly = True
            .Columns(Unit).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(Unit).Visible = False

            .Columns(itemCategory).HeaderText = "Item Category"
            .Columns(itemCategory).Width = 50
            .Columns(itemCategory).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(itemCategory).ReadOnly = True
            .Columns(itemCategory).Visible = False

            .Columns(rack).HeaderText = "Rack"
            .Columns(rack).Width = 70
            .Columns(rack).ReadOnly = True
            
            .Columns(opQty).HeaderText = "Open Qty "
            .Columns(opQty).Width = 70
            .Columns(opQty).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(opQty).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(opQty).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(opQty).ReadOnly = True

            .Columns(opcost).HeaderText = "Open Cost"
            .Columns(opcost).Width = 100
            .Columns(opcost).ReadOnly = True
            .Columns(opcost).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(opcost).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(opcost).Visible = getRight(200, CurrentUser)


            .Columns(CostAvg).HeaderText = "Cost Average"
            .Columns(CostAvg).Width = 120
            .Columns(CostAvg).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(CostAvg).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(CostAvg).ReadOnly = True
            .Columns(CostAvg).Visible = getRight(200, CurrentUser)


            .Columns(LastPurchaseWithoutOthercost).HeaderText = "Last Purch Price"
            .Columns(LastPurchaseWithoutOthercost).Width = 120
            .Columns(LastPurchaseWithoutOthercost).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(LastPurchaseWithoutOthercost).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(LastPurchaseWithoutOthercost).ReadOnly = True
            .Columns(LastPurchaseWithoutOthercost).Visible = getRight(200, CurrentUser)


            .Columns(LastPurchaseOthercost).HeaderText = "Last Purchase OthCost"
            .Columns(LastPurchaseOthercost).Width = 120
            .Columns(LastPurchaseOthercost).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(LastPurchaseOthercost).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(LastPurchaseOthercost).ReadOnly = True
            .Columns(LastPurchaseOthercost).Visible = getRight(200, CurrentUser)

            .Columns(LastPurchCost).HeaderText = "Last PurchCost"
            .Columns(LastPurchCost).Width = 120
            .Columns(LastPurchCost).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(LastPurchCost).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(LastPurchCost).ReadOnly = True
            .Columns(LastPurchCost).Visible = getRight(200, CurrentUser)

            .Columns(UnitPrice).HeaderText = "Unit Price"
            .Columns(UnitPrice).Width = 100
            .Columns(UnitPrice).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(UnitPrice).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(UnitPrice).ReadOnly = True

            .Columns(RcvdQty).HeaderText = "Rcvd Qty"
            .Columns(RcvdQty).Width = 100
            .Columns(RcvdQty).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(RcvdQty).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(RcvdQty).ReadOnly = True

            .Columns(IssdQty).HeaderText = "Issd Qty"
            .Columns(IssdQty).Width = 100
            .Columns(IssdQty).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(IssdQty).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(IssdQty).ReadOnly = True

            .Columns(QIH).HeaderText = "QIH"
            .Columns(QIH).Width = 70
            .Columns(QIH).ReadOnly = True
            .Columns(QIH).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(QIH).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(P1Qty).HeaderText = "P1 Qty"
            .Columns(P1Qty).Width = 70
            .Columns(P1Qty).ReadOnly = True
            .Columns(P1Qty).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(P1Qty).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(P2Qty).HeaderText = "P2 Qty"
            .Columns(P2Qty).Width = 70
            .Columns(P2Qty).ReadOnly = True
            .Columns(P2Qty).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(P2Qty).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(CreatedBy).HeaderText = "CreatedBy"
            .Columns(CreatedBy).Width = 70
            .Columns(CreatedBy).ReadOnly = True
            .Columns(CreatedBy).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(CreatedBy).Visible = False

            .Columns(ModiBy).HeaderText = "ModiBy"
            .Columns(ModiBy).Width = 70
            .Columns(ModiBy).ReadOnly = True
            .Columns(ModiBy).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ModiBy).Visible = False

            .Columns(Tval).Visible = False
            .Columns(ItemId).HeaderText = "ItemId"
            .Columns(ItemId).Visible = False
            Dim i As Integer
            For i = 19 To .ColumnCount - 1
                .Columns(i).Visible = False
            Next
            If getRight(200, CurrentUser) = False Then
                resizeGridColumn(grdvoucher, Description)
            End If
        End With
    End Sub

    Private Sub SetStockLedgerGridHead()
        If Me.IsInitializing Then Exit Sub
        With grdstockLedger
            SetGridProperty(grdstockLedger)

            .Columns("ItemCode").HeaderText = "Item Code"
            .Columns("ItemCode").Width = 100
            .Columns("ItemCode").ReadOnly = True
            .Columns("ItemCode").Visible = False

            .Columns("Description").HeaderText = "Description"
            .Columns("Description").Width = 200
            .Columns("Description").ReadOnly = True
            .Columns("Description").Visible = False

            .Columns("AccDescr").HeaderText = "Party Name"
            .Columns("AccDescr").Width = 150
            .Columns("AccDescr").ReadOnly = True
            '.Columns("AccDescr").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("AccDescr").Visible = True

            .Columns("TrType").HeaderText = "TrType"
            .Columns("TrType").Width = 50
            .Columns("TrType").ReadOnly = True
            .Columns("TrType").Visible = True

            .Columns("InvNo").HeaderText = "Inv. No"
            .Columns("InvNo").Width = 70
            .Columns("InvNo").ReadOnly = True

            .Columns("TrDate").HeaderText = "Tr Date"
            .Columns("TrDate").Width = 100
            .Columns("TrDate").ReadOnly = True

            .Columns("TrDescription").HeaderText = "TrDescription"
            '.Columns("TrDescription").Width = .Width - 900
            .Columns("TrDescription").ReadOnly = True

            .Columns("InTr").HeaderText = "Qty In"
            .Columns("InTr").Width = 100
            .Columns("InTr").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("InTr").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("InTr").ReadOnly = True

            .Columns("OutTr").HeaderText = "Qty Out"
            .Columns("OutTr").Width = 100
            .Columns("OutTr").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("OutTr").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("OutTr").ReadOnly = True

            .Columns("FOC").HeaderText = "FOC"
            .Columns("FOC").Width = 100
            .Columns("FOC").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("FOC").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("FOC").ReadOnly = True

            .Columns("UnitCost").HeaderText = "Unit Cost"
            .Columns("UnitCost").Width = 100
            .Columns("UnitCost").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("UnitCost").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("UnitCost").ReadOnly = True

            .Columns("TrCost").HeaderText = "Tr Cost"
            .Columns("TrCost").Width = 100
            .Columns("TrCost").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("TrCost").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("TrCost").ReadOnly = True

            .Columns("CostAvg").HeaderText = "Cost Average"
            .Columns("CostAvg").Width = 100
            .Columns("CostAvg").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("CostAvg").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("CostAvg").Visible = False

            .Columns("Bal").HeaderText = "Cost Average"
            .Columns("Bal").Width = 100
            .Columns("Bal").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Bal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Bal").ReadOnly = True
            .Columns("Bal").Visible = False

            .Columns("ItemId").HeaderText = "ItemId"
            .Columns("ItemId").Visible = False
            .Columns("lnk").Visible = False
            .Columns("DateFrom").Visible = False
            .Columns("DateTo").Visible = False

        End With
        resizeGridColumn(grdstockLedger, 7)
    End Sub
    Private Sub txtSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
        Select Case e.KeyCode
            Case Keys.Up, Keys.PageUp
                MoveUp()
            Case Keys.Down, Keys.PageDown
                MoveDown()
        End Select
    End Sub
    Public Sub MoveUp()
        Dim r As Integer
        With grdvoucher
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
        With grdvoucher
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
        Timer2.Enabled = False
        Dim dt As DataTable = SearchGrid(dtTable, Trim(txtSearch.Text), cmbSearch.SelectedIndex, Not chkSearch.Checked)
        grdvoucher.DataSource = dt
        Timer2.Enabled = True
        totalValue(dt)
        'totalValue()
    End Sub
    Private Sub SetLevelGrid()
        If Me.IsInitializing Then Exit Sub
        chgbyprg = True
        grdLevel.Columns.Clear()
        Dim headert(2) As String
        headert(0) = "GrpItmCode"
        headert(1) = "Description"
        headert(2) = "UnqGrpId"
        With grdLevel
            '.ReadOnly = True
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = True
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = True
            .AutoResizeColumns()
            .StandardTab = False
            '.Location = New Point(1, 1)
            .ColumnCount = 1
            '.Width = tbPanelLevel.Width - 2
            '.Height = tbPanelLevel.Height - 2
            .ColumnHeadersHeight = 100
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersDefaultCellStyle.BackColor = Color.Olive
            .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("arial", 8.0!, FontStyle.Regular)

            .Columns(0).HeaderText = "Level"
            .Columns(0).Width = 150
            .Columns(0).ReadOnly = True

            Dim cmb As New DataGridViewComboBoxColumn
            cmb.HeaderText = "Item Group"
            cmb.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
            cmb.DropDownWidth = 150
            .Columns.Add(cmb)
            .Columns(1).ReadOnly = False
            '.Columns(1).ReadOnly = True
            'ldLevel()
        End With
        chgbyprg = False
    End Sub

    Private Sub ldLevel(ByVal LevelTb As DataTable, ByVal LevelGrpTb As DataTable)
        Dim dtGroup As DataTable
        Dim cmb As New DataGridViewComboBoxCell
        Dim itmlevel As New DataTable
        'grdLevel.Rows.Clear()
        
        chgbyprg = True
        Dim found As Boolean
        With grdLevel
            .Rows.Clear()
            Dim i As Integer
            '.RowCount = 0
            If LevelTb.Rows.Count > 0 Then
                For i = 0 To LevelTb.Rows.Count - 1
                    If LevelTb.Rows.Count > .RowCount Then .Rows.Add()
                    .Item(0, i).Value = LevelTb(i)("LName")
                    cmb = .Rows(i).Cells(1)
                    If cmb.Items.Count = 0 Then
                        cmb.Items.Clear()
                        cmb.Items.Add("")
                        dtGroup = SearchGrid(LevelGrpTb, LevelTb(i)("LName"), 1)
                        found = False
                        For j = 0 To dtGroup.Rows.Count - 1
                            cmb.Items.Add(dtGroup(j)("GrpItmCode"))
                            If itmlevel.Rows.Count > 0 Then
                                If Trim(itmlevel(0)(0) & "") = Trim(dtGroup(j)("GrpItmCode") & "") Then found = True
                            End If
                        Next
                    End If
                Next
            End If
            .Refresh()
        End With
        chgbyprg = False
    End Sub

    Private Sub QtyReport_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not fRptFormat Is Nothing Then fRptFormat.Close() : fRptFormat = Nothing
        'fMainForm.plHome.Visible = True
        If Not frm Is Nothing Then frm.Close() : frm = Nothing
    End Sub
    Private Sub formLoad()
        Dim strqry As String
        strqry = "Select LocCode from LocationTb"
        strqry = strqry & " SELECT LName,LCode from LevelTb Order by LCode"
        strqry = strqry & " SELECT LevelTb.LCode, LName, GrpItmCode, UnqGrpId FROM LevelTb LEFT JOIN " & _
          "(SELECT GrpItmCode, LCode, UnqGrpId FROM GrpItmTb) Q ON Q.LCode = LevelTb.LCode ORDER BY LevelTb.LCode"
        Dim dtset As DataSet = _objcmnbLayer._ldDataset(strqry, False)
        loadLocation(dtset.Tables(0))
        ldLevel(dtset.Tables(1), dtset.Tables(2))
    End Sub
    Private Sub QtyReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        chgbyprg = True
        SetLevelGrid()
        cldrStartDate.Value = Format(DateAdd(DateInterval.Day, firstDateFromToday * -1, DateValue(Date.Now)), DtFormat)
        formLoad()
        cmbQIH.SelectedIndex = 0
        chkLevelWise.Checked = False
        'dtFrom.CtlText = Format(getServerDate, DtFormat)
        'cldrStartDate.Value = DateValue("01/" & Date.Now.Month & "/" & Date.Now.Year)
        txtSearch.Select()
        chkLevelWise.Checked = False
        grdLevel.ReadOnly = Not chkLevelWise.Checked
        If enableRestuarent Then rdomenu.Visible = True
        chkbatchwise.Visible = enableBatchwiseTr
        Timer1.Enabled = True
        chgbyprg = False
        'lblbranch.Visible = enableBranch
        'grdLocation.Visible = enableBranch
        If UsrBr <> "" Then
            chklocationwise.Checked = True
            chklocationwise.Enabled = False
        Else
            chklocationwise.Checked = False
        End If
    End Sub
    Private Sub setComboGrid()
        If Me.IsInitializing Then Exit Sub
        cmbSearch.Items.Clear()
        Dim i As Integer = 0
        For i = 0 To grdvoucher.ColumnCount - 1
            cmbSearch.Items.Add(grdvoucher.Columns(i).HeaderText)

        Next
        If cmbSearch.Items.Count > 0 Then
            cmbSearch.SelectedIndex = 1
        End If

    End Sub


    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        loadWaite(1)
    End Sub

    Private Sub rdOpeningQty_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Me.IsInitializing Then Exit Sub
        'If chgbyprg Then Exit Sub
        Dim MyCtrl As RadioButton = sender
        If MyCtrl.Checked = True Then
            opt = Val(MyCtrl.Tag)
        End If
    End Sub
    Private Function getWhereInv() As String
        Dim strWhere As String = ""
        'If forGrid Then GoTo forGrid
        Dim i As Integer
        If forSingle Then
            Dim SelectedItmID As Long
            SelectedItmID = Val(grdvoucher.Item(ItemId, grdvoucher.CurrentRow.Index).Value)
            getWhereInv = " Where ItemId = " & SelectedItmID
            Exit Function
        End If
        If rdTag.Checked Then
            For i = 0 To grdvoucher.RowCount - 1
                If Not IsDBNull(grdvoucher.Item(TAGITEM, i).Value) Then
                    If grdvoucher.Item(TAGITEM, i).Value = True Then
                        strWhere = strWhere & IIf(strWhere <> "", ",", "") & Val(grdvoucher.Item(ItemId, i).Value)
                    End If
                End If
            Next
            If strWhere <> "" Then
                strWhere = " Where ItemId In (" & strWhere & ")"
            Else
                strWhere = " Where ItemId In (0)"
            End If
            getWhereInv = strWhere
            Exit Function
        ElseIf rdGridlist.Checked Then
            getWhereInv = strWhere
            Exit Function
        ElseIf rdAll.Checked Then
            getWhereInv = " Where ItemCategory= 'Service' or ItemCategory= 'Stock'"
            Exit Function
        Else
            getWhereInv = ""
            Exit Function
        End If
    End Function
    Private Function getWhereAsOn() As String
        Dim strWhere As String = ""
        getWhereAsOn = ""
        'If forGrid Then GoTo forGrid
        Dim i As Integer
        If forSingle Then
            Dim SelectedItmID As Long
            SelectedItmID = Val(grdvoucher.Item(ItemId, grdvoucher.CurrentRow.Index).Value)
            getWhereAsOn = " Where ProductList.ItemId = " & SelectedItmID
            Exit Function
        End If
        If rdTag.Checked Then
        ElseIf rdGridlist.Checked Then
            For i = 0 To grdvoucher.RowCount - 1
                strWhere = strWhere & IIf(strWhere <> "", ",", "") & Val(grdvoucher.Item(ItemId, i).Value)
            Next
            If strWhere <> "" Then
                strWhere = "  Where ProductList.ItemId In (" & strWhere & ")"
            End If
            getWhereAsOn = strWhere
            Exit Function
        ElseIf rdAll.Checked Then
            getWhereAsOn = " Where ItemCategory= 'Service' or ItemCategory= 'Stock'"
            Exit Function
        Else
            getWhereAsOn = ""
            Exit Function
        End If

    End Function
    Private Sub PrepareReport(ByVal FileName As String, ByVal RptCaption As String, ByVal forpint As Boolean)
        Dim strStockCondition As String = ""
        Dim lvlFlds As String = ""
        Dim ds As DataSet
        If Not frm Is Nothing Then
            frm.Close()
            frm = New ReprtviewNEWfrm
        Else
            frm = New ReprtviewNEWfrm
        End If
        If pnlstockledger.Visible Then
            ds = _objReport.returnStockLedger(Val(grdvoucher.Item("Itemid", grdvoucher.CurrentCell.RowIndex).Value), DateValue(cldrStartDate.Value), DateValue(dtpto.Value))
        Else
            Select Case cmbQIH.SelectedIndex
                Case 1 '+ve
                    strStockCondition = " Where QIH >0 "
                Case 2  ' -ve
                    strStockCondition = " Where QIH <0 "
                Case 3  ' 0
                    strStockCondition = " Where QIH =0 "
                Case 4 ' 0+ve
                    strStockCondition = " Where QIH >=0 "
                Case 5  '0-ve
                    strStockCondition = " Where QIH <=0 "
                Case 6  ' Non zero
                    strStockCondition = " Where QIH <> 0 "
                Case Else
                    strStockCondition = ""
            End Select
            If strStockCondition = "" Then
                strStockCondition = " WHERE isnull(ishide,0)=0 AND "
            Else
                strStockCondition = strStockCondition & " AND "
            End If
            If rdStock.Checked Then
                strStockCondition = strStockCondition & " itemCategory= 'Stock'"
            ElseIf rdService.Checked Then
                strStockCondition = strStockCondition & " itemCategory= 'Service'"
            ElseIf rdBoth.Checked Then
                strStockCondition = strStockCondition & " itemCategory= 'Service' or itemCategory= 'Stock'"
            ElseIf rdomenu.Checked Then
                strStockCondition = strStockCondition & " itemCategory= 'Menu Item'"
            End If
            If cmbitemtype.SelectedIndex = 0 Then
                strStockCondition = strStockCondition & " And isnull(ismanufacturing,0)=1"
            ElseIf cmbitemtype.SelectedIndex = 1 Then
                strStockCondition = strStockCondition & " And isnull(ismanufacturing,0)<>1"
            End If
            Dim strW As String = ""
            Dim It_Param As String = ""
            If (rdTag.Checked) Then
                With grdvoucher
                    'strW = .Item(0, 0).Value
                    For i = 0 To .RowCount - 1
                        If .Item(0, i).Value = "Y" Then
                            If strW <> "" Then strW = strW & ","
                            strW = strW & .Item(ItemId, i).Value

                        End If
                    Next i
                End With
                If strW <> "" Then It_Param = " AND tr.ItemId in (" & strW & ")"
            ElseIf (rdAll.Checked) Then
                It_Param = ""
            ElseIf (rdoselect.Checked) Then
                It_Param = " AND tr.ItemId=" & Val(grdvoucher.Item(ItemId, grdvoucher.CurrentCell.RowIndex).Value)
            End If
            
            Dim flds As String = ""
            If chkLevelWise.Checked Then
                Dim lvl As String = ""
                If chkLevelWise.Checked Then
                    Dim i As Integer
                    For i = 0 To grdLevel.RowCount - 1
                        If grdLevel.Item(1, i).Value <> "" Then
                            'lvl = lvl & IIf(lvl = "", "", " AND ") & " Level" & i + 1 & "=" & getLevelId(grdLevel.Item(1, i).Value)
                            lvl = lvl & IIf(lvl = "", "", " AND ") & " Lvl" & i + 1 & "='" & grdLevel.Item(1, i).Value & "'"
                            lvlFlds = lvlFlds & IIf(lvlFlds = "", "", " - ") & grdLevel.Item(1, i).Value
                        End If
                    Next
                End If
                strStockCondition = strStockCondition & IIf(lvl = "", "", " AND ") & lvl
            End If
            If txtSearch.Text <> "" And Not rdAll.Checked And Not rdTag.Checked Then
                If strStockCondition = "" Then
                    strStockCondition = " WHERE [" & cmbSearch.Text & "] LIKE '" & txtSearch.Text & "%'"
                Else
                    Dim srch As String
                    If cmbSearch.Text = "Item Code" Then
                        srch = "ItemCode"
                    Else
                        srch = cmbSearch.Text
                    End If
                    strStockCondition = strStockCondition & " AND [" & srch & "] LIKE '" & IIf(chkSearch.Checked, "", "%") & txtSearch.Text & "%'"
                End If
            End If
            If opt = 2 Then
                strStockCondition = strStockCondition & " AND isnull(QIH,0)<=isnull(MinQty,0)"
            End If
            If chkbatchwise.Checked Then
                strStockCondition = strStockCondition & " AND isnull(BatchQty,0)>0"
            End If
            flds = "1 lnk,[Item Code] as ItemCode ,InvItm.Description,Unit,itemCategory," & _
                    IIf(UsrBr = "", "opQty", "isnull(locopnQty,0)") & " opQty,opcost,isnull(TCST,opcost) CostAvg," & _
                    IIf(UsrBr = "", "LastPurchCost", "isnull(loclastcost,0)") & " LastPurchCost," & _
                    "LastPurchaseOthercost,LastPurchaseWithoutOthercost," & _
                    "UnitPrice,(((isnull(IGST,0)+ISNULL(vat,0)+isnull(rgcess,0))*UnitPrice)/100)+UnitPrice+isnull(additionalcess,0) [Tax Price],ISNULL(RQty,0)RcvdQty,isnull(IQty,0)IssdQty," & _
                    IIf(UsrBr = "", "isnull(AsOnQty,0)+opQty", "isnull(Locqty,0)") & " QIH ," & _
                    IIf(UsrBr = "", "(isnull(AsOnQty,0)+opQty)", "isnull(Locqty,0)") & "/case when P1Fra=0 then 1 else P1Fra end P1Qty," & _
                    IIf(UsrBr = "", "(isnull(AsOnQty,0)+opQty)", "isnull(Locqty,0)") & "/case when P2Fra=0 then 1 else P2Fra end P2Qty," & _
                    "CreatedBy,ModiBy,invitm.itemid,isnull(AsOnQty,0)+opQty AsOnQty, " & _
                    "Lvl1,Lvl2,Lvl3,Lvl5,Lvl6,Lvl7,Lvl8,Lvl9,Lvl10,'" & DateValue(dtpto.Value) & "' DateFrom,'" & lvlFlds & "' LevelFlds,FraCount,P1Unit,P2Unit,MinQty,MRP,InvItm.HSNCode,isnull(IGST,0) GST,Rack,UnitPriceWS,secondPrice,ismanufacturing,isnull(ishide,0) ishide,mechineItemcode " & _
                    IIf(chklocationwise.Checked, ",Locqty,loc,loclastcost,locopnQty ", "")
            ds = _objReport.returnItemQuantityReport(strStockCondition & It_Param, flds, "returnItemQuantityReport", getDateNo(dtpto.Value), DateValue(dtpto.Value), IIf(chkbatchwise.Checked, 1, 0), IIf(chklocationwise.Checked, 1, 0))
        End If
        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = Me.MdiParent
        frm.Text = RptCaption
        frm.Show()
    End Sub


    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        PrepareReport(RptFlName, RptCaption, forPrint)
    End Sub

    Private Sub btnFP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFP.Click
        cldrStartDate.Value = Format(DateFrom, DtFormat)
    End Sub

    Private Sub btnTo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTo.Click
        cldrStartDate.Value = Format(DateTo, DtFormat)
    End Sub

    Private Sub rdStock_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdStock.CheckedChanged, rdService.CheckedChanged, rdBoth.CheckedChanged, rdomenu.CheckedChanged
       
    End Sub

    Private Sub grdvoucher_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdvoucher.CellClick
        If e.ColumnIndex = 0 And e.RowIndex >= 0 Then
            With grdvoucher
                If .Item(e.ColumnIndex, e.RowIndex).Value = "Y" Then
                    .Item(e.ColumnIndex, e.RowIndex).Value = ""
                Else
                    .Item(e.ColumnIndex, e.RowIndex).Value = "Y"
                End If
            End With
        End If
        Timer2.Enabled = True
    End Sub

    Private Sub grdvoucher_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdvoucher.CellDoubleClick
        'If btnApply.Enabled = False Then Exit Sub
        'forSingle = True
        'btnPreview_Click(sender, e)
    End Sub

    Private Sub check_Type()
        If (rdStock.Checked) Then
            Type_rd = "Stock"
        ElseIf (rdService.Checked) Then
            Type_rd = "Service"
        ElseIf (rdBoth.Checked) Then

        End If


    End Sub
    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'PrepareGrid()
        'setComboGrid()
        'lstVouchers.Enabled = False
        'btnApply.Enabled = False
    End Sub
    Private Function getWhereCostAsOn() As String
        Dim strWhere As String = ""
        Dim i As Integer
        If forSingle Then
            Dim SelectedItmID As Long
            SelectedItmID = Val(grdvoucher.Item(ItemId, grdvoucher.CurrentRow.Index).Value)
            getWhereCostAsOn = " Where BaseItemId = " & SelectedItmID
            StrRptQry = " Where ProductList.ItemId = " & SelectedItmID
            Exit Function
        End If
        If rdTag.Checked Then
            For i = 0 To grdvoucher.RowCount - 1
                If Not IsDBNull(grdvoucher.Item(TAGITEM, i).Value) Then
                    If grdvoucher.Item(TAGITEM, i).Value = True Then
                        strWhere = strWhere & IIf(strWhere <> "", ",", "") & Val(grdvoucher.Item(ItemId, i).Value)
                    End If
                End If
            Next
            If strWhere <> "" Then
                StrRptQry = " Where ProductList.ItemId in ( " & strWhere & ")"
                strWhere = " Where BaseItemId In (" & strWhere & ")"
            Else
                strWhere = " Where BaseItemId In (0)"
                StrRptQry = " Where ProductList.ItemId in (0)"
            End If
            getWhereCostAsOn = strWhere
            Exit Function
        ElseIf rdGridlist.Checked Then
            For i = 0 To grdvoucher.RowCount - 1
                strWhere = strWhere & IIf(strWhere <> "", ",", "") & Val(grdvoucher.Item(ItemId, i).Value)
            Next
            If strWhere <> "" Then
                StrRptQry = " Where ProductList.ItemId in ( " & strWhere & ")"
                strWhere = "  Where BaseItemId In (" & strWhere & ")"
            End If
            getWhereCostAsOn = strWhere
            Exit Function
        ElseIf rdAll.Checked Then
            getWhereCostAsOn = " Where ItemCategory= 'Service' or ItemCategory= 'Stock'"
            StrRptQry = " Where ItemCategory= 'Service' or ItemCategory= 'Stock'"
            Exit Function
        Else
            getWhereCostAsOn = ""
            Exit Function
        End If
    End Function

    Private Sub cmbQIH_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbQIH.SelectedIndexChanged
        'FillGrid()
    End Sub

    Private Sub cldrStartDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cldrStartDate.ValueChanged
        cldrStartDate.Value = Format(DateValue(cldrStartDate.Value), DtFormat)
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
        Dim strStockCondition As String = ""
        If rdStock.Checked Then
            strStockCondition = " Where ItemCategory= 'Stock'"
        ElseIf rdService.Checked Then
            strStockCondition = " Where ItemCategory= 'Service'"
        ElseIf rdBoth.Checked Then
            'strStockCondition = " Where ItemCategory= 'Stock'"
            strStockCondition = " Where ItemCategory= 'Service' or ItemCategory= 'Stock'"
        End If

        If strCondition = "" Then
            strCondition = " WHERE"
        Else
            strCondition = strCondition & " AND"
        End If
        If chkSearch.Checked Then
            strCondition = strCondition & "( [Item Code] Like '" & txtSearch.Text & "%' OR Barcode Like  '" & txtSearch.Text & "%' or Description Like '" & txtSearch.Text & "%' )"
        Else
            strCondition = strCondition & "( [Item Code] Like '%" & txtSearch.Text & "%' OR Barcode Like  '%" & txtSearch.Text & "%' or Description Like '%" & txtSearch.Text & "%')"
        End If
        strQry = "select convert(bit,0) as Tag,[Item Code] as ItemCode ,Barcode,Description,Model,UType,Unit,[Sales Price] as SalesPrice, [WS Price] as WholeSalesPrice," & _
                 "  QtyInHand as QIH,CostAverage, [Opn Cost] as OpnCost, [Opn Qty] as OpnQty, ItemId,BaseID from  ( Select [Item Code],Barcode,Description,Model,'B' as UType," & _
                 "Unit,UnitPrice AS [Sales Price],UnitPriceWS as [WS Price], (RcvdQty+QtyOPn - IssdQty) as  QtyInHand,CostAverage,CostOpen as [Opn Cost],QtyOpn as [Opn Qty], " & _
                 "ItemId,BaseID  from BaseItmDet  INNER JOIN InvItm ON BaseItmDet.BaseItemID=InvItm.ItemId " & strStockCondition & "  union all  Select  [Item Code],Barcode,TrDescription,Model," & _
                 "'P' as UType,Unit,UnitPrice AS [Sales Price],UnitPriceWS as [WS Price],  (RcvdQty+QtyOPn - IssdQty)/(Vdown/Vup),CostAverage * (Vdown/Vup),CostOpen *(Vdown/Vup) as [Opn Cost]," & _
                 "QtyOpn/(Vdown/Vup) as [Opn Qty], ItemId,BaseID  from InvItm  INNER JOIN BaseItmDet ON InvItm.BaseId =BaseItmDet.BaseItemID and ItemId<>BaseID  " & strStockCondition & " ) as q  " & strCondition & "  order by baseid,itemid "
        bDatatable = _objcmnbLayer._fldDatatable(strQry)
nxt:
        Return bDatatable
    End Function

    Private Sub ldTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ldTimer.Tick
        ldTimer.Enabled = False

        'If chkGlobalsearch.Checked Then
        '    grdvoucher.DataSource = GlobalSearch()
        'Else
        '    grdvoucher.DataSource = SearchGrid(dtTable, Trim(txtSearch.Text), cmbSearch.SelectedIndex, Not chkSearch.Checked)
        'End If
        'totalValue()
    End Sub
    Private Sub setOthPara()

        'Dim i As Short
        'With chklstlocation
        '    .Tag = ""
        '    For i = 0 To .Items.Count - 1
        '        If .GetItemChecked(i) Then .Tag = .Tag & IIf(.Tag = "", "", ",") & "'" & chklstlocation.Items.Item(i).ToString & "'"
        '    Next i

        '    'chkOth.Tag = ""
        'End With
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub lstVouchers_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstVouchers.SelectedIndexChanged
        If lstVouchers.SelectedIndex < 0 Then Exit Sub
        opt = lstVouchers.SelectedIndex
    End Sub

    Private Sub btnR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If chgbyprg = True Then Exit Sub
        loadWaite(1)
    End Sub


    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        loadWaite(3)
    End Sub
    Private Sub preview()
        Dim RptType As String = ""
        Dim RptCaption As String = ""
        If pnlstockledger.Visible Then
            RptType = "STK1"
        Else
            Select Case opt
                Case 0
                    If chkLevelWise.Checked Then
                        RptType = "QR2"
                    ElseIf chkbatchwise.Checked Then
                        RptType = "QTYBT"
                    ElseIf chklocationwise.Checked Then
                        RptType = "QRLOC"
                    Else
                        RptType = "QR1"
                    End If
                Case 1
                    If chkLevelWise.Checked Then
                        RptType = "QR4"
                    Else
                        RptType = "QR3"
                    End If
                Case 2
                    RptType = "QRM"
                Case 3
                    RptType = "IPL"
            End Select
        End If
        If chkFormat.Checked Then
            fRptFormat = New RptFormatfrm
            fRptFormat.RptType = RptType
            fRptFormat.ShowDialog()
            fRptFormat = Nothing
        Else
            Dim RptName As String
            RptName = getRptDefFlName(RptType, RptCaption)
            If RptName <> "" Then
                PrepareReport(RptName, RptCaption, False)
            End If
        End If
    End Sub

    Private Sub totalValue(ByVal dt As DataTable)
        Dim Ttl As Double
        If dt.Rows.Count > 0 Then
            Ttl = CDbl(dt.Compute("sum(AsOnQty)", String.Empty))
            lbltotalValue.Text = ("QIH : " & Strings.Format(Ttl, numFormat))
            Ttl = CDbl(dt.Compute("sum(tValue)", String.Empty))
            lbltotalValue.Text = lbltotalValue.Text & " / Total Value : " & Format(Ttl, numFormat)
            lbltotalValue.Text = lbltotalValue.Text & " / Total Items : " & dt.Rows.Count
        End If
    End Sub

    Private Sub grdLevel_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdLevel.Enter
        If grdLevel.CurrentCell.ColumnIndex = 1 Then
            grdLevel.BeginEdit(True)
        End If
    End Sub

    Private Sub grdLevel_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdLevel.CellClick
        If grdLevel.CurrentCell.ColumnIndex = 1 Then
            grdLevel.BeginEdit(True)
        End If
    End Sub

    Private Sub chkLevelWise_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkLevelWise.CheckedChanged
        grdLevel.ReadOnly = Not chkLevelWise.Checked
        panellevel.Visible = chkLevelWise.Checked
    End Sub
    Private Sub ldStockLedger()
        Dim dt As DataTable
        dt = _objReport.returnStockLedger(Val(grdvoucher.Item("Itemid", grdvoucher.CurrentCell.RowIndex).Value), DateValue(cldrStartDate.Value), DateValue(dtpto.Value)).Tables(0)
        If UsrBr <> "" And Dloc <> "" Then
            If dt.Rows.Count = 0 Then GoTo nxt
            Dim _qurey As EnumerableRowCollection(Of DataRow)
            _qurey = From data In dt.AsEnumerable() Where data("DocDefLoc") = Dloc Select data
            If _qurey.Count > 0 Then
                dt = _qurey.CopyToDataTable()
            End If
nxt:
        End If
        grdstockLedger.DataSource = dt
        pnlstockledger.Height = Panel1.Top - Panel1.Height
        pnlstockledger.Left = 0
        pnlstockledger.Width = Me.Width - 10
        pnlstockledger.Visible = True
        SetStockLedgerGridHead()
        Dim sum, sum2 As Double
        sum = Convert.ToInt32(dt.Compute("SUM(InTr)", String.Empty))
        lblIN.Text = sum
        sum2 = Convert.ToInt32(dt.Compute("SUM(OutTr)", String.Empty))
        lblIN.Text = "Total    [IN] : " & Format(sum, numFormat)
        lblIN.Text = lblIN.Text & "  /  [Out] : " & Format(sum2, numFormat)
        lblIN.Text = lblIN.Text & "  /  [QIH] : " & Format(CDbl(sum) - CDbl(sum2), numFormat)
        lblIN.Text = lblIN.Text & "/   Item : " & dt(0)("Description")
    End Sub

    Private Sub btnLedger_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLedger.Click
        loadWaite(2)
        
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        pnlstockledger.Visible = False
        btnLedger.Enabled = True
        lbltotalValue.Visible = True
    End Sub

    Private Sub picCloseProd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picCloseProd.Click
        'panellevel.Visible = False
        chkLevelWise.Checked = False
    End Sub

    Private Sub rdBoth_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdBoth.Click, rdomenu.Click, rdService.Click, rdStock.Click
        If Me.IsInitializing Then Exit Sub
        loadWaite(1)
    End Sub
    

    Private Sub btnexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexport.Click
        'GridExport(grdvoucher)
        Dim a As String = ""
        Dim filename As String = ""
        FolderBrowserDialog1.ShowDialog()
        If FolderBrowserDialog1.SelectedPath <> "" Then
            filename = FolderBrowserDialog1.SelectedPath
        Else
            Exit Sub
        End If
        'If ExportToExcel(filename & "/excelfile.xls", "", a) Then
        '    MsgBox("Export Completed", MsgBoxStyle.Information)
        'End If
        Dim dt As DataTable
        Dim strStockCondition As String = ""
        If chkLevelWise.Checked Then
            If chkLevelWise.Checked Then
                Dim i As Integer
                For i = 0 To grdLevel.RowCount - 1
                    If grdLevel.Item(1, i).Value <> "" Then
                        lvl = lvl & IIf(lvl = "", "", " WHERE ") & " Lvl" & i + 1 & "='" & grdLevel.Item(1, i).Value & "'"
                    End If
                Next
            End If
            strStockCondition = IIf(lvl = "", "", " WHERE ") & lvl
        End If
        If chkbillingmech.Checked Then
            dt = _objcmnbLayer._fldDatatable("Select [Item Code] as ItemCode ,GSTName HsnCode,InvItm.Description ItemName,Unit,UnitPrice Rate,QIH OpStock " & _
                                 "from invitm left join GSTTb on invitm.hsncode=gsttb.HSNCode")
            DataTableToExcel(filename & "/excelfile.xls", dt)
        ElseIf chkcsv.Checked Then
            dt = _objcmnbLayer._fldDatatable("Select [Item Code] as ItemCode ,mechineItemcode SLNO,InvItm.Description ItemName,Unit,ROUND(UnitPrice,2) Rate " & _
                                "from invitm LEFT JOIN (SELECT GrpItmCode Lvl1,UnqGrpId LvlId1 from GrpItmTb) GrpItmTb1 ON INVITM.Level1=GrpItmTb1.LvlId1 " & strStockCondition)
            exportDataTableToCsv(filename & "/productlist.csv", dt)
        Else
            DataTableToExcel(filename & "/Stock.xls", dtTable)
        End If
        MsgBox("Export Completed", MsgBoxStyle.Information)
    End Sub
    
    Private Sub PictureBox4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox4.Click
        chklocationwise.Checked = False
    End Sub

    Private Sub chklocationwise_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chklocationwise.CheckedChanged
        'pllocation.Visible = chklocationwise.Checked
        'If chklocationwise.Checked Then
        '    If grdvoucher.CurrentRow Is Nothing Then Exit Sub
        '    showlocationwise(grdvoucher.CurrentRow.Index)
        'Else
        '    If Not fshowlocationqty Is Nothing Then fshowlocationqty.Close() : fshowlocationqty = Nothing
        'End If

    End Sub
    Private Sub loadLocation(ByVal dt As DataTable)

        Dim i As Integer
        For i = 0 To dt.Rows.Count - 1
            chklocations.Items.Add(dt(i)(0))
        Next
    End Sub

    Private Sub cmbitemtype_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbitemtype.SelectedIndexChanged
        loadWaite(1)
    End Sub

    Private Sub chkbillingmech_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkbillingmech.Click
        If chkbillingmech.Checked Then
            chkcsv.Checked = False
        End If
    End Sub

    Private Sub chkcsv_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkcsv.CheckedChanged
        If chkcsv.Checked Then
            chkbillingmech.Checked = False
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        loadWaite(1)
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
            Case 2
                ldStockLedger()
                btnLedger.Enabled = False
                lbltotalValue.Visible = False
            Case 3
                preview()
        End Select
        If Not fwait Is Nothing Then
            fwait.Close()
            fwait = Nothing
        End If
    End Sub
    Private Sub grdvoucher_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdvoucher.RowEnter
        'showlocationwise(e.RowIndex)
        Timer2.Enabled = True
    End Sub
    Private Sub showlocationwise(ByVal rowindex As Integer)
        If chklocationwise.Checked = False Then Exit Sub
        If chgbyprg Then Exit Sub
        If grdvoucher.CurrentRow Is Nothing Then Exit Sub
        If fshowlocationqty Is Nothing Then fshowlocationqty = New ShowLocationQtyFrm
        With fshowlocationqty
            '.MdiParent = fMainForm
            .loadLOCQty(Val(grdvoucher.Item("Itemid", rowindex).Value))
            .Show()
            .Top = Me.Top + Screen.PrimaryScreen.WorkingArea.Height - .Height - 40
            .Left = Me.Left + Screen.PrimaryScreen.WorkingArea.Width - .Width - 10
        End With
    End Sub

    Private Sub fshowlocationqty_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fshowlocationqty.FormClosed
        chklocationwise.Checked = False
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Timer2.Enabled = False
        Dim str As String
        If grdvoucher.CurrentRow Is Nothing Then Exit Sub
        Dim preitemid As Long = Val(grdvoucher.Item("ItemId", grdvoucher.CurrentRow.Index).Value)
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

        str = str & " select [item code],[description],isnull(loc,'')Location,isnull(Locqty,0)QTY,isnull(opnQty,0)OpnQty,locationCost from invitm " & _
            "left join  (SELECT SUM(CASE WHEN InvType in ('IN') then 1 else -1 end  * isnull(qty,0)) Locqty ,Itemid ,FromLoc loc,sum(isnull(opnQty,0))opnQty,sum(isnull(lastcost,0))locationCost from " & _
            "(SELECT 'IN' InvType,isnull(LocOpnQtyTb.qty,0)qty,0 TrDateNo,Itemid,LocCode FromLoc,Qty opnQty,lastcost FROM  LocOpnQtyTb " & _
            "LEFT JOIN LocationTb ON LocationTb.locationid=LocOpnQtyTb.locationid " & _
            "UNION ALL " & _
            "SELECT 'OUT' InvType,case when doctype='DOC' AND 0='0' then 0 else qty end qty,0 TrDateNo,Itemid,FromLoc,0,0 " & _
            "FROM DocTranTb LEFT JOIN DocCmnTb ON  DocTranTb.DocId=DocCmnTb.DocId where doctype in ('DOC','MTN') " & _
            "UNION ALL " & _
           "SELECT 'IN' InvType,case when doctype='DOC' AND 0='0' then 0 else qty end qty,0 TrDateNo,Itemid,DocDefLoc,0,0 FROM DocTranTb " & _
           "LEFT JOIN DocCmnTb ON  DocTranTb.DocId=DocCmnTb.DocId where doctype in ('DOS','MTN') " & _
           " UNION ALL " & _
           "SELECT InvType,trqty+isnull(focqty,0) trqty,TrDateNo,Itemid,DocDefLoc,0,0 FROM ItmInvTrtb LEFT JOIN VoucherTypeNoTb ON ItmInvTrTb.TrTypeNo=VoucherTypeNoTb.vrno " & _
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
            .Columns(4).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("locationCost").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("locationCost").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(3).Width = 75
            .Columns(4).Width = 75
            .Columns("locationCost").HeaderText = "Cost"
            .Columns("locationCost").Width = 75
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

    Private Sub grdvoucher_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdvoucher.CellContentClick

    End Sub
End Class