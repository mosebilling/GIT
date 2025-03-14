﻿Public Class MilksalesFrm
    Private _objcmnbLayer As New Dlayer
    Private chgbyprg As Boolean
    Private activecontrolname As String
    Private dtitem As DataTable
    Private dtcustomersales As DataTable
    Private chgamt As Boolean
    Private WithEvents fwait As WaitMessageFrm
    Private _objTr As New clsAccountTransaction
    Private _objInv As New clsInvoice
    Private prefixid As Integer
    Private WithEvents fRptFormat As RptFormatfrm
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents frmMilkCustomerPriceFrm As New MilkCustomerPriceFrm
    Dim STRptType As String
    Private chngfound As Boolean
    Private dtsum As DataTable
    Private dtcustomerprice As DataTable


#Region "grdvoucher"
    Private Const constslno = 0
    Private Const constcustomername = 1
    Private Const constInvno = 2
    Private colstsalesamt As Integer
    Private colstlastdue As Integer
    Private colstcash As Integer
    Private colsttotaldue As Integer
    Private colpricegroup As Integer
    Private colcustomerid As Integer
    Private colmc_id As Integer
    Private colmc_invid As Integer
    Private colitemorder As Integer

#End Region
#Region "grdsummary"
    Private Const constItems = 0
    Private Const constOrd = 1
    Private Const constXta = 2
    Private Const constRtn = 3
    Private Const constFree = 4
    Private Const constleak = 5
    Private Const constsold = 6
    Private Const constDiff = 7
    Private Const constTray = 8
    Private Const constgrpid = 9
#End Region
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub MilksalesFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbsalesman.Items.Clear()
        AddtoCombo(cmbsalesman, "SELECT SManCode FROM SalesmanTb", True, False)
        cmbroute.Items.Clear()
        AddtoCombo(cmbroute, "SELECT areacode FROM areatb", True, False)
        Timer1.Enabled = True
        loaddefaultAccounts()
    End Sub
    'Private Sub setGridHeadSummary()
    '    With grdsummary
    '        SetGridProperty(grdsummary)
    '        '.ColumnCount = 2
    '        .Columns(0).HeaderText = "Item Name"
    '        .Columns(0).Width = 200
    '        .Columns(0).ReadOnly = True

    '        .Columns(1).HeaderText = "Total"
    '        .Columns(1).Width = 80
    '        .Columns(1).ReadOnly = True
    '        .Columns(1).DefaultCellStyle.Format = "N0"
    '        .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    '    End With
    'End Sub
    Private Sub setGridHeadOther()
        With grdother
            SetEntryGridProperty(grdother)
            .ColumnCount = 10

            .Columns(constItems).HeaderText = "Items"
            .Columns(constItems).Width = 100
            .Columns(constItems).ReadOnly = True

            .Columns(constOrd).HeaderText = "ORD"
            .Columns(constOrd).Width = 50
            .Columns(constOrd).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns(constXta).HeaderText = "XTA"
            .Columns(constXta).Width = 50
            .Columns(constXta).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns(constRtn).HeaderText = "RTN"
            .Columns(constRtn).Width = 50
            .Columns(constRtn).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns(constFree).HeaderText = "FREE"
            .Columns(constFree).Width = 50
            .Columns(constFree).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns(constleak).HeaderText = "LEAK"
            .Columns(constleak).Width = 50
            .Columns(constleak).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns(constsold).HeaderText = "SOLD"
            .Columns(constsold).Width = 50
            .Columns(constsold).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(constsold).ReadOnly = True

            .Columns(constDiff).HeaderText = "DIFF"
            .Columns(constDiff).Width = 50
            .Columns(constDiff).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(constDiff).ReadOnly = True

            .Columns(constTray).HeaderText = "TRAY"
            .Columns(constTray).Width = 50
            .Columns(constTray).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns(constgrpid).Visible = False
            '.Rows.Add()
            '.Item(0, .RowCount - 1).Value = "LITRE"

            '.Rows.Add()
            '.Item(0, .RowCount - 1).Value = "500ML"

            '.Rows.Add()
            '.Item(0, .RowCount - 1).Value = "CURD"

            '.Rows.Add()
            '.Item(0, .RowCount - 1).Value = "CDKG/CUP"
            loadGroup()
        End With
        resizeGridColumn(grdother, 0)
    End Sub
    Private Sub loadItems()
        dtitem = _objcmnbLayer._fldDatatable("select [Item Code] itemname,0 Total,isnull(UnitPrice,0)UnitPrice,isnull(UnitPriceWS,0)UnitPriceWS,isnull(secondPrice,0)secondPrice," & _
                                             "isnull(price1,0)price1,isnull(price2,0)price2,isnull(price3,0)price3,isnull(price4,0)price4,isnull(price5,0)price5,isnull(price6,0)price6,isnull(price7,0)price7,isnull(price8,0)price8,isnull(price9,0)price9,isnull(price10,0)price10,isnull(IGST,0)IGST,Level1,invitm.itemid,itemlistorder from invitm " & _
                                             "left join GSTTb on GSTTb.HSNCode=invitm.HSNCode " & _
                                             "left join InvItmPropertiesTb on invitm.itemid = InvItmPropertiesTb.itemid   where isnull(ishidefrommilksales,0) = 0 and isnull([Item Code],'')<>'' order by isnull(itemlistorder,0)")
        chgbyprg = True
        With grdVoucher
            SetEntryGridProperty(grdVoucher)

            .ColumnCount = dtitem.Rows.Count + 3 + 8
            .Columns(0).HeaderText = "SL"
            .Columns(0).Width = 50
            .Columns(0).ReadOnly = True
            .Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable

            .Columns(1).HeaderText = "Customer Name"
            .Columns(1).Width = 200
            .Columns(1).ReadOnly = True
            .Columns(1).Name = "customername"
            .Columns(1).DataPropertyName = "customername"
            .Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
           
            .Columns(2).HeaderText = "InvNo"
            .Columns(2).Width = 80
            .Columns(2).ReadOnly = True
            .Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(2).DefaultCellStyle.BackColor = Color.Pink

            Dim i As Integer
            For i = 0 To dtitem.Rows.Count - 1
                .Columns(i + 3).HeaderText = dtitem(i)("itemname")
                .Columns(i + 3).Width = 75
                .Columns(i + 3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(i + 3).ReadOnly = False
                .Columns(i + 3).SortMode = DataGridViewColumnSortMode.NotSortable
            Next
            Dim cnt As Integer = i + 3
            colstsalesamt = cnt
            '.ColumnCount - 7
            .Columns(colstsalesamt).HeaderText = "Sale AMT"
            .Columns(colstsalesamt).Width = 100
            .Columns(colstsalesamt).DefaultCellStyle.Format = "N0"
            .Columns(colstsalesamt).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(colstsalesamt).ReadOnly = True
            .Columns(colstsalesamt).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(colstsalesamt).DefaultCellStyle.BackColor = Color.LightPink

            '.ColumnCount-6
            cnt = cnt + 1
            colstlastdue = cnt
            .Columns(colstlastdue).HeaderText = "Last Due"
            .Columns(colstlastdue).Width = 100
            .Columns(colstlastdue).DefaultCellStyle.Format = "N0"
            .Columns(colstlastdue).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(colstlastdue).ReadOnly = True
            .Columns(colstlastdue).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(colstlastdue).DefaultCellStyle.BackColor = Color.LightPink

            '.ColumnCount-5
            cnt = cnt + 1
            colstcash = cnt
            .Columns(colstcash).HeaderText = "Cash"
            .Columns(colstcash).Width = 100
            .Columns(colstcash).DefaultCellStyle.Format = "N0"
            .Columns(colstcash).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(colstcash).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(colstcash).DefaultCellStyle.BackColor = Color.Tan

            '.ColumnCount-4
            cnt = cnt + 1
            colsttotaldue = cnt
            .Columns(colsttotaldue).HeaderText = "Total Due"
            .Columns(colsttotaldue).Width = 100
            .Columns(colsttotaldue).DefaultCellStyle.Format = "N0"
            .Columns(colsttotaldue).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(colsttotaldue).ReadOnly = True
            .Columns(colsttotaldue).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(colsttotaldue).DefaultCellStyle.BackColor = Color.LightBlue


            '.ColumnCount-3
            cnt = cnt + 1
            colpricegroup = cnt
            .Columns(colpricegroup).HeaderText = "pricegroup"
            .Columns(colpricegroup).DataPropertyName = "pricegroup"
            .Columns(colpricegroup).Visible = False
            .Columns(colpricegroup).SortMode = DataGridViewColumnSortMode.NotSortable

            '.ColumnCount-2
            cnt = cnt + 1
            colcustomerid = cnt
            .Columns(colcustomerid).HeaderText = "customerid"
            .Columns(colcustomerid).DataPropertyName = "customerid"
            .Columns(colcustomerid).Visible = False
            .Columns(colcustomerid).SortMode = DataGridViewColumnSortMode.NotSortable

            '.ColumnCount-1
            cnt = cnt + 1
            colmc_id = cnt
            .Columns(colmc_id).HeaderText = "customerid"
            .Columns(colmc_id).DataPropertyName = "customerid"
            .Columns(colmc_id).Visible = False
            .Columns(colmc_id).SortMode = DataGridViewColumnSortMode.NotSortable

            '.ColumnCount-1
            cnt = cnt + 1
            colmc_invid = cnt
            .Columns(colmc_invid).HeaderText = "invid"
            .Columns(colmc_invid).DataPropertyName = "invid"
            .Columns(colmc_invid).Visible = False
            .Columns(colmc_invid).SortMode = DataGridViewColumnSortMode.NotSortable

            '.Columns(colitemorder).HeaderText = "itemlistorder"

            '.Columns(colitemorder).Visible = False

        End With

        chgbyprg = False
        'grdsummary.DataSource = dtitem
        'setGridHeadSummary()
        'resizeGridColumn(grdsummary, 0)
    End Sub

    Private Sub grdVoucher_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellClick
        If e.RowIndex < 0 Then
            Exit Sub
        End If
        With grdVoucher
            If e.RowIndex = 0 Then
                .CurrentCell.ReadOnly = True
                Exit Sub
            End If
            If e.ColumnIndex < 3 Or e.ColumnIndex = colsttotaldue Or e.ColumnIndex = colstlastdue Or e.ColumnIndex = colstsalesamt Then
                grdVoucher.CurrentCell.ReadOnly = True
                If e.ColumnIndex = 1 Then
                    If Not frmMilkCustomerPriceFrm Is Nothing Then
                        If frmMilkCustomerPriceFrm.Visible Then loadcustomerprice()
                    End If

                End If
                Exit Sub
            Else
                grdVoucher.CurrentCell.ReadOnly = False
            End If
        End With
        grdBeginEdit()
        
    End Sub
    Private Sub grdBeginEdit()
        chgbyprg = True
        grdVoucher.BeginEdit(True)
        chgbyprg = False
    End Sub
    Private Sub grdBeginEditSummary()
        chgbyprg = True
        grdother.BeginEdit(True)
        chgbyprg = False
    End Sub

    'Private Sub grdVoucher_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.DoubleClick
    '    Dim str As String = grdVoucher.Columns(1).Name
    '    MsgBox(str)
    '    str = grdother.Item(str, grdVoucher.CurrentRow.Index).Value
    '    MsgBox(str)
    'End Sub

    Private Sub grdVoucher_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdVoucher.EditingControlShowing
        Dim Col As Integer
        Col = grdVoucher.CurrentCell.ColumnIndex
        If Col > 2 Then
            Dim tb As TextBox = CType(e.Control, TextBox)
            RemoveHandler tb.KeyPress, AddressOf Textbox_KeyPress
            AddHandler tb.KeyPress, AddressOf Textbox_KeyPress
        End If
    End Sub
    Private Sub Textbox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            Dim col As Integer
            col = grdVoucher.CurrentCell.ColumnIndex
            If col > 2 Then
                If Not IsNumeric(e.KeyChar) And e.KeyChar <> "." And Not e.KeyChar = Convert.ToChar(Keys.Back) Then e.Handled = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub loadCustomer()
        If cmbroute.Text = "" Then

            grdVoucher.Rows.Clear()
            Exit Sub
        End If
        Dim str As String
        str = "select accid,AccDescr,isnull(OpnBal,0)+isnull(bal,0)bal,isnull(pricegroup,0)pricegroup from AccMast " & _
              "left join S1AccHd on AccMast.S1AccId=S1AccHd.S1AccId " & _
              "left join (select accountno,sum(dealamt) bal from AccTrDet left join acctrcmn on acctrdet.linkno=acctrcmn.linkno " & _
              "where jvdate <'" & Format(DateValue(cldrdate.Value), "yyyy/MM/dd") & "' group by AccountNo)tr on AccMast.AccId=tr.AccountNo " & _
              "where GrpSetOn='customer'  and AreaCode='" & cmbroute.Text & "'"


        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable(str)
        grdVoucher.Rows.Clear()
        With grdVoucher
            .Rows.Add()
            .Item(0, 0).Value = ""
            .Item(1, 0).Value = "TOTAL"
            .Item(2, 0).Value = ""
            Dim i As Integer
            Dim cnt As Integer
            For i = 0 To dt.Rows.Count - 1
                .Rows.Add()
                cnt = .Rows.Count - 1
                .Item(0, cnt).Value = i + 1
                .Item(1, cnt).Value = dt(i)("AccDescr")
                .Item(2, cnt).Value = ""
                .Item(colstlastdue, cnt).Value = Format(dt(i)("bal"), numFormat)
                .Item(colpricegroup, cnt).Value = dt(i)("pricegroup")
                .Item(colcustomerid, cnt).Value = dt(i)("accid")
            Next
            'For i = 0 To 50
            '    .Rows.Add()
            '    cnt = .Rows.Count - 1
            '    .Item(0, cnt).Value = i + 1
            '    .Item(1, cnt).Value = "TEST " & i + 1
            '    .Item(2, cnt).Value = ""
            '    .Item(.Columns.Count - 5, cnt).Value = 0
            '    .Item(.Columns.Count - 2, cnt).Value = 0
            '    .Item(.Columns.Count - 1, cnt).Value = 0

            'Next
            freezeRow(grdVoucher.Rows(0))
            grdVoucher.Rows(0).Height = 32
            
            '.Rows.Add()
            'i = .RowCount - 1
            '.Item(0, i).Value = ""
            '.Item(1, i).Value = "TOTAL"
            '.Item(2, i).Value = ""
            '.Item(.Columns.Count - 4, i).Value = Format(0, numFormat)
            '.Rows(i).DefaultCellStyle.BackColor = Color.LightPink
        End With

        Dim qry As String
        qry = "select [Item Code] itemcode,isnull(customerprice,0)customerprice,isnull(IGST,0)IGST,isnull(CustomerWisePriceDiscountTb.itemid,0)itemid,isnull(customerid,0)customerid from InvItm " & _
               "left join CustomerWisePriceDiscountTb on InvItm.ItemId=CustomerWisePriceDiscountTb.itemid " & _
               "left join GSTTb on GSTTb.HSNCode=invitm.HSNCode "
        dtcustomerprice = _objcmnbLayer._fldDatatable(qry)



    End Sub
    Private Sub freezeRow(ByVal band As DataGridViewBand)
        band.Frozen = True
        band.DefaultCellStyle.BackColor = Color.GreenYellow
        band.DefaultCellStyle.ForeColor = Color.Red
        band.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 10.5!, System.Drawing.FontStyle.Bold)

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        loadItems()
        'MsgBox(Me.Width)
        'If Me.Width > 1400 Then
        '    'resizeGridColumn(grdVoucher, 1)
        'End If

        setGridHeadOther()
        addnew()
    End Sub

    Private Sub btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnload.Click
        If chngfound = True Then
            If MsgBox("changes found do u want to continue ?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation) = MsgBoxResult.No Then Exit Sub
        End If

        loadWaite(2)
        chngfound = False
    End Sub

    Private Sub grdVoucher_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.GotFocus
        activecontrolname = "grdVoucher"
    End Sub

    Private Sub grdVoucher_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdVoucher.KeyDown
        If e.KeyCode = Keys.Enter Then
            If grdVoucher.RowCount = 0 Then Exit Sub

            If FindNextCell(grdVoucher, grdVoucher.CurrentCell.RowIndex, grdVoucher.CurrentCell.ColumnIndex + 1) Then
                grdVoucher.CurrentCell = grdVoucher.Item(3, grdVoucher.RowCount - 1)
            End If

            If grdVoucher.CurrentCell.RowIndex > 0 Then grdBeginEdit()
            
        End If
    End Sub

    Private Sub grdVoucher_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.Leave
        activecontrolname = ""
    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            Const WM_KEYDOWN As Integer = &H100
            Const WM_KEYUP As Integer = &H101
            'msg.ToString()
            If msg.Msg = WM_KEYDOWN Or msg.Msg = WM_KEYUP Then
                If activecontrolname = "grdVoucher" Then
                    If msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up) Or msg.WParam.ToInt32() = CInt(Keys.Enter) Or msg.WParam.ToInt32() = CInt(Keys.F2) Or msg.WParam.ToInt32() = CInt(Keys.F3) Or msg.WParam.ToInt32() = CInt(Keys.F4) Or msg.WParam.ToInt32() = CInt(Keys.F6) Then
                        grdVoucher_KeyDown(Nothing, New KeyEventArgs(keyData))
                        Return True
                    End If
                End If
                If activecontrolname = "grdother" Then
                    If msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up) Or msg.WParam.ToInt32() = CInt(Keys.Enter) Or msg.WParam.ToInt32() = CInt(Keys.F2) Or msg.WParam.ToInt32() = CInt(Keys.F3) Or msg.WParam.ToInt32() = CInt(Keys.F4) Or msg.WParam.ToInt32() = CInt(Keys.F6) Then
                        grdother_KeyDown(Nothing, New KeyEventArgs(keyData))
                        Return True
                    End If
                End If
            End If
ctn:
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function



    Private Sub grdVoucher_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellValidated
        Try
            If e.RowIndex <= 0 Then Exit Sub
            If e.ColumnIndex > constInvno And e.ColumnIndex < colstsalesamt Then
                If chgamt = False Then Exit Sub
                Dim i As Integer
                grdVoucher.Item(colstsalesamt, e.RowIndex).Value = 0
                For i = constInvno + 1 To colstsalesamt - 1
                    getItemPrice(e.RowIndex, i)
                Next
                calculate()
                chgamt = False
            ElseIf e.ColumnIndex = colstcash Then
                Dim totaldue As Double
                With grdVoucher
                    Dim rindex As Integer = e.RowIndex
                    totaldue = CDbl(.Item(colstsalesamt, rindex).Value) + CDbl(.Item(colstlastdue, rindex).Value) - CDbl(.Item(colstcash, rindex).Value)
                    .Item(colsttotaldue, rindex).Value = Format(totaldue, numFormat)
                End With

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
        chngfound = True
        btnUpdateEnable()

    End Sub
    Private Sub getItemPrice(ByVal rindex As Integer, ByVal cindex As Integer)

        Dim pricedt As DataTable
        If dtcustomerprice.Rows.Count = 0 Then pricedt = dtcustomerprice.Clone : GoTo nxt
        Dim itemcode As String
        Dim pricegroup As Integer
        Dim qty As Integer
        Dim customerid As Long
        Dim price As Double
        Dim gst As Double
        itemcode = grdVoucher.Columns(cindex).HeaderText
        pricegroup = Val(grdVoucher.Item(colpricegroup, rindex).Value)
        qty = Val(grdVoucher.Item(cindex, rindex).Value)
        customerid = Val(grdVoucher.Item(colcustomerid, rindex).Value)
        Dim _qurey As EnumerableRowCollection(Of DataRow)
        If Not dtcustomersales Is Nothing Then
            If dtcustomersales.Rows.Count > 0 Then
                _qurey = From data In dtcustomersales.AsEnumerable() Where data("itemcode") = itemcode And data("mc_customerid") = customerid Select data
                If _qurey.Count > 0 Then
                    pricedt = _qurey.CopyToDataTable()
                Else
                    pricedt = dtcustomerprice.Clone
                End If
                If pricedt.Rows.Count > 0 Then
                    price = pricedt(0)("mi_price")
                    gst = pricedt(0)("mi_taxp")
                End If
            End If
        End If
        If price = 0 Then
            _qurey = From data In dtcustomerprice.AsEnumerable() Where data(0) = itemcode And data("customerid") = customerid Select data
            If _qurey.Count > 0 Then
                pricedt = _qurey.CopyToDataTable()
            Else
                pricedt = dtcustomerprice.Clone
            End If

            If pricedt.Rows.Count > 0 Then
                'If pricegroup = 0 Then
                '    price = pricedt(0)("UnitPrice")
                'ElseIf pricegroup = 1 Then
                '    price = pricedt(0)("UnitPriceWS")
                'ElseIf pricegroup = 2 Then
                '    price = pricedt(0)("secondPrice")
                'Else
                '    price = pricedt(0)("price" & pricegroup - 2)
                'End If
                price = pricedt(0)("customerprice")
                gst = pricedt(0)("IGST")
            End If
        End If
        If gst > 0 Then
            price = price + (price * gst / 100)
        End If
        price = price * qty
        With grdVoucher
            price = price + CDbl(.Item(colstsalesamt, rindex).Value)
            .Item(colstsalesamt, rindex).Value = Format(price, numFormat)
            Dim totaldue As Double
            totaldue = CDbl(.Item(colstsalesamt, rindex).Value) + CDbl(.Item(colstlastdue, rindex).Value) - CDbl(.Item(colstcash, rindex).Value)
            .Item(colsttotaldue, rindex).Value = Format(totaldue, numFormat)
        End With

nxt:
    End Sub

    Private Sub grdVoucher_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellValueChanged
        If chgbyprg Then Exit Sub
        If e.ColumnIndex > 2 And e.ColumnIndex < colstsalesamt Then
            chgamt = True
            chngfound = True
        End If
        btnUpdateEnable()
    End Sub
Private Sub loadGroup()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("select * from (select GrpItmCode,UnqGrpId from InvItm " & _
            "inner join GrpItmTb on InvItm.Level1=GrpItmTb.UnqGrpId group by GrpItmCode,UnqGrpId)tr order by UnqGrpId")
        Dim i As Integer
        grdother.Rows.Clear()
        With grdother
            For i = 0 To dt.Rows.Count - 1
                .Rows.Add()
                .Item(constItems, i).Value = dt(i)("GrpItmCode")
                .Item(constgrpid, i).Value = dt(i)("UnqGrpId")
            Next
        End With

    End Sub

    Private Sub grdother_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdother.CellClick
        If e.RowIndex < 0 Then Exit Sub
        With grdother
            'If e.RowIndex = 0 Then
            '    .CurrentCell.ReadOnly = True
            '    Exit Sub
            'End If
            If e.ColumnIndex = 0 Or e.ColumnIndex = constsold Then
                .CurrentCell.ReadOnly = True
                Exit Sub
            ElseIf e.ColumnIndex = 0 Or e.ColumnIndex = constDiff Then
                .CurrentCell.ReadOnly = True
            Else
                .CurrentCell.ReadOnly = False
            End If
        End With
        grdBeginEditSummary()
        btnUpdateEnable()
    End Sub

    Private Sub grdother_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdother.CellValidated
        If e.ColumnIndex > 0 Then
            calculateSummary()
            chngfound = True
        Else
            chngfound = False
        End If

        btnUpdateEnable()
    End Sub

    Private Sub grdother_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdother.CellValueChanged

    End Sub

    Private Sub grdother_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdother.EditingControlShowing
        Dim Col As Integer
        Col = grdother.CurrentCell.ColumnIndex
        If Col > 2 Then
            Dim tb As TextBox = CType(e.Control, TextBox)
            RemoveHandler tb.KeyPress, AddressOf TextboxOther_KeyPress
            AddHandler tb.KeyPress, AddressOf TextboxOther_KeyPress
        End If
    End Sub
    Private Sub TextboxOther_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            Dim col As Integer
            col = grdother.CurrentCell.ColumnIndex
            If col > 0 Then
                If Not IsNumeric(e.KeyChar) And e.KeyChar <> "." And Not e.KeyChar = Convert.ToChar(Keys.Back) Then e.Handled = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub grdother_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdother.GotFocus
        activecontrolname = "grdother"
    End Sub

    Private Sub grdother_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdother.KeyDown
        If e.KeyCode = Keys.Enter Then
            If grdother.RowCount = 0 Then Exit Sub

            If FindNextCell(grdother, grdother.CurrentCell.RowIndex, grdother.CurrentCell.ColumnIndex + 1) Then
                grdother.CurrentCell = grdother.Item(1, grdother.RowCount - 1)
            End If

            grdBeginEditSummary()
        End If
    End Sub

    Private Sub grdother_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdother.Leave
        activecontrolname = ""
    End Sub
    Private Sub calculate()
        Dim i As Integer
        Dim j As Integer
        With grdVoucher
            For j = constInvno + 1 To colsttotaldue
                .Item(j, 0).Value = 0
            Next
            For i = 1 To grdVoucher.Rows.Count - 1
                For j = constInvno + 1 To colsttotaldue
                    If Val(.Item(j, 0).Value) = 0 Then .Item(j, 0).Value = 0
                    If Val(.Item(j, i).Value) = 0 Then .Item(j, i).Value = 0
                    .Item(j, 0).Value = CDbl(.Item(j, 0).Value) + CDbl(.Item(j, i).Value)
                    If j >= colstsalesamt Then
                        .Item(j, 0).Value = Format(.Item(j, 0).Value, numFormat)
                    End If
                Next
            Next
        End With
        If grdother.RowCount > 0 Then calculatesold()
    End Sub
    Private Sub calculatesold()
        Dim i As Integer
        Dim j As Integer
        Dim x As Integer
        Dim ttl As Integer
        Dim dt As DataTable
        Dim _qurey As EnumerableRowCollection(Of DataRow)
        For i = 0 To grdother.Rows.Count - 1
            _qurey = (From data In dtitem.AsEnumerable() Where Not IsDBNull(data("Level1")) AndAlso Not IsDBNull(grdother.Item(constgrpid, i).Value) AndAlso Data("Level1").ToString() = grdother.Item(constgrpid, i).Value.ToString()Select data)
            If _qurey.Count > 0 Then
                dt = _qurey.CopyToDataTable()
            Else
                dt = dtitem.Clone
            End If
            If dt.Rows.Count > 0 Then
                For j = 0 To dt.Rows.Count - 1
                    For x = constInvno + 1 To colstsalesamt - 1
                        If grdVoucher.Columns(x).HeaderText = dt(j)("itemname") Then
                            ttl = ttl + Val(grdVoucher.Item(x, 0).Value)
                            Exit For
                        End If
                    Next
                Next
            End If
            grdother.Item(constsold, i).Value = ttl
            ttl = 0
        Next
        calculateSummary()
    End Sub
    Private Sub calculateSummary()
        Dim i As Integer
        For i = 0 To grdother.Rows.Count - 1
            With grdother
                .Item(constDiff, i).Value = Val(.Item(constsold, i).Value) - (Val(.Item(constOrd, i).Value) + Val(.Item(constXta, i).Value) + Val(.Item(constFree, i).Value) - Val(.Item(constleak, i).Value) - Val(.Item(constRtn, i).Value))
            End With
        Next
    End Sub
    Private Sub saveMilkSales()
        Dim milkcmnid As Long
        Dim qry As String
        With grdVoucher
            If .Item(colstsalesamt, 0).Value = 0 Then
                MsgBox("Transactions not found", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            Dim mc_id As Long
            Dim i As Integer
            Dim j As Integer
            Dim pricegroup As Integer
            Dim pricedt As DataTable
            Dim dt As DataTable
            If Val(txtbillno.Tag) = 0 Then
                qry = "insert into MilkCmnTb(billno,milk_date,milk_salesmancode,miilk_areacode,remark) values(" & Val(txtbillno.Text) & _
                    ",'" & Format(DateValue(cldrdate.Value), "yyyy/MM/dd") & "','" & cmbsalesman.Text & "','" & cmbroute.Text & "','" & txtrmk.Text & "')" & _
                    "select scope_identity() maxno"
                dt = _objcmnbLayer._fldDatatable(qry)
                If dt.Rows.Count > 0 Then
                    milkcmnid = dt(0)("maxno")
                End If
            Else
                milkcmnid = Val(txtbillno.Tag)
            End If
            qry = "update MilkCustomerTb set mc_setremove=1 where MC_cmnid=" & milkcmnid
            _objcmnbLayer.savewithoutparam(qry)
            For i = 1 To .RowCount - 1
                mc_id = 0
                If Val(.Item(colstsalesamt, i).Value) = 0 Then GoTo nxtcustomer
                If Val(.Item(colmc_id, i).Value) = 0 Then
                    qry = "insert into MilkCustomerTb (MC_cmnid,mc_saleamt,mc_lastdueamt,mc_cashamt,mc_totaldueamt,mc_customerid) values(" & _
                    milkcmnid & "," & CDbl(.Item(colstsalesamt, i).Value) & "," & CDbl(.Item(colstlastdue, i).Value) & "," & _
                    CDbl(.Item(colstcash, i).Value) & "," & CDbl(.Item(colsttotaldue, i).Value) & "," & CDbl(.Item(colcustomerid, i).Value) & ")" & _
                    "select scope_identity() maxno"
                    dt = _objcmnbLayer._fldDatatable(qry)
                    If dt.Rows.Count > 0 Then
                        mc_id = dt(0)("maxno")
                    End If
                Else
                    mc_id = Val(.Item(colmc_id, i).Value)
                    qry = "update MilkCustomerTb set mc_setremove=0, mc_saleamt=" & CDbl(.Item(colstsalesamt, i).Value) & "," & _
                            "mc_lastdueamt=" & CDbl(.Item(colstlastdue, i).Value) & "," & _
                            "mc_cashamt=" & CDbl(.Item(colstcash, i).Value) & "," & _
                            "mc_totaldueamt=" & CDbl(.Item(colsttotaldue, i).Value) & "," & _
                            "mc_customerid=" & Val(.Item(colcustomerid, i).Value) & " where mc_id=" & mc_id
                    _objcmnbLayer.savewithoutparam(qry)
                End If
                qry = "delete from MilkItemTb where mi_mcid=" & mc_id
                _objcmnbLayer.savewithoutparam(qry)
                For j = constInvno + 1 To colstsalesamt - 1
                    If Val(.Item(j, i).Value) = 0 Then GoTo nxtitem
                    pricegroup = Val(grdVoucher.Item(colpricegroup, i).Value)

                    'Dim _qurey As EnumerableRowCollection(Of DataRow)
                    '_qurey = From data In dtitem.AsEnumerable() Where data(0) = grdVoucher.Columns(j).HeaderText Select data
                    'If _qurey.Count > 0 Then
                    '    pricedt = _qurey.CopyToDataTable()
                    'Else
                    '    pricedt = dtitem.Clone
                    'End If
                    Dim price As Double = 0
                    Dim gst As Double = 0
                    Dim taxamt As Double = 0
                    Dim itemid As Long = 0
                    returnItemPrice(grdVoucher.Columns(j).HeaderText, Val(grdVoucher.Item(colcustomerid, i).Value), pricegroup, price, gst, itemid)
                    If itemid > 0 Then
                        'If pricegroup = 0 Then
                        '    price = pricedt(0)("UnitPrice")
                        'ElseIf pricegroup = 1 Then
                        '    price = pricedt(0)("UnitPriceWS")
                        'ElseIf pricegroup = 2 Then
                        '    price = pricedt(0)("secondPrice")
                        'Else
                        '    price = pricedt(0)("price" & pricegroup - 2)
                        'End If

                        'gst = pricedt(0)("IGST")
                        taxamt = (price * gst / 100) * Val(.Item(j, i).Value)

                        qry = "insert into MilkItemTb (mi_cmnid,mi_mcid,mi_itemid,mi_trqty,mi_price,mi_taxp,mi_taxamt) values(" & _
                          milkcmnid & "," & mc_id & "," & itemid & "," & Val(.Item(j, i).Value) & "," & price & "," & gst & "," & taxamt & ")"
                        _objcmnbLayer.savewithoutparam(qry)
                    End If
nxtitem:
                Next
nxtcustomer:
            Next
            qry = "delete  MilkCustomerTb where  isnull(mc_setremove,0)=1 and  MC_cmnid=" & milkcmnid
            _objcmnbLayer.savewithoutparam(qry)
        End With
        With grdother
            Dim x As Integer
            qry = "delete from MilkSummaryTb where ms_cmnid=" & milkcmnid
            _objcmnbLayer.savewithoutparam(qry)
            For x = 0 To .RowCount - 1
                qry = "insert into MilkSummaryTb (ms_cmnid,ms_grpid,ms_ord,ms_xta,ms_rtn,ms_free,ms_leak,ms_sold,ms_diff,ms_tray) values (" & _
                milkcmnid & "," & Val(.Item(constgrpid, x).Value) & "," & Val(.Item(constOrd, x).Value) & "," & Val(.Item(constXta, x).Value) & _
                "," & Val(.Item(constRtn, x).Value) & "," & Val(.Item(constFree, x).Value) & "," & Val(.Item(constleak, x).Value) & "," & _
                Val(.Item(constsold, x).Value) & "," & Val(.Item(constDiff, x).Value) & "," & Val(.Item(constTray, x).Value) & ")"

                _objcmnbLayer.savewithoutparam(qry)
            Next
        End With
        saveTrans(milkcmnid)
        loadsales()
        load_todaysalessum()
        If Not fwait Is Nothing Then fwait.Close() : fwait = Nothing
        MsgBox("Sales Invoice is succesfully posted with Vr. # " & txtbillno.Text & ".", MsgBoxStyle.Information)
    End Sub
    Public Sub returnItemPrice(ByVal itemcode As String, ByVal customerid As Long, ByVal pricegroup As String, _
                               ByRef price As Double, ByRef gst As Double, ByRef itemid As Long)
        Dim _qurey As EnumerableRowCollection(Of DataRow)
        Dim pricedt As DataTable
        If Not dtcustomersales Is Nothing Then
            If dtcustomersales.Rows.Count > 0 Then
                _qurey = From data In dtcustomersales.AsEnumerable() Where data("itemcode") = itemcode And data("mc_customerid") = customerid Select data
                If _qurey.Count > 0 Then
                    pricedt = _qurey.CopyToDataTable()
                Else
                    pricedt = dtitem.Clone
                End If
                If pricedt.Rows.Count > 0 Then
                    price = pricedt(0)("mi_price")
                    gst = pricedt(0)("mi_taxp")
                    itemid = pricedt(0)("mi_itemid")
                Else
                    price = 0
                    gst = 0
                    itemid = 0
                End If
            End If
        End If
        'If itemcode = "0001502580" Then
        '    MsgBox("")
        'End If
        If price = 0 Then
            _qurey = From data In dtcustomerprice.AsEnumerable() Where data(0) = itemcode And data("customerid") = customerid Select data
            If _qurey.Count > 0 Then
                pricedt = _qurey.CopyToDataTable()
            Else
                pricedt = dtcustomerprice.Clone
            End If

            If pricedt.Rows.Count > 0 Then

                price = pricedt(0)("customerprice")
                gst = pricedt(0)("IGST")
                itemid = pricedt(0)("itemid")
            End If
        End If
        'If gst > 0 Then
        '    price = price + (price * gst / 100)
        'End If
    End Sub

    Private Sub saveTrans(ByVal milkcmnid As Long)
        Dim dt As DataTable
        Dim dtinvtr As DataTable
        Dim invno As Integer
        Dim prefix As String = ""
        Dim trid As Long
        Dim vrAccountNo1 As Long
        Dim vrAccountNo2 As Long
        'dt = _objcmnbLayer._fldDatatable("select invno,prefix  from itminvcmntb inner join (select max(trid)maxtrid from itminvcmntb where trtype='IS')TR ON itminvcmntb.TRID=TR.maxtrid " & _
        '                                 " where  trtype='IS'")
        'If dt.Rows.Count > 0 Then
        '    invno = dt(0)("invno")
        '    prefix = dt(0)("prefix")
        'End If

        dt = _objcmnbLayer._fldDatatable("select MilkItemTb.*,InvItm.Description,Unit,HSNCode,mc_customerid from MilkItemTb " & _
                                         "left join InvItm on invitm.itemid=MilkItemTb.mi_itemid " & _
                                         "left join MilkCustomerTb on MilkCustomerTb.mc_id=MilkItemTb.mi_mcid " & _
                                         "where mi_cmnid=" & milkcmnid)
        Dim i As Integer
        Dim j As Integer
        Dim x As Integer
        Dim dtype As String
        Dim customerid As Long
        Dim salesamt As Double
        Dim cashamt As Double
        Dim TDrAmt As Double
        Dim pricegroup As Double
        Dim customername As String
        Dim mi_mcid As Long
        With grdVoucher
            For i = 1 To .Rows.Count - 1
                trid = Val(.Item(colmc_invid, i).Value & "")
                customerid = Val(.Item(colcustomerid, i).Value & "")
                
                customername = .Item(constcustomername, i).Value
                If Val(.Item(colstsalesamt, i).Value) = 0 Then .Item(colstsalesamt, i).Value = 0
                If Val(.Item(colstcash, i).Value) = 0 Then .Item(colstcash, i).Value = 0
                salesamt = CDbl(.Item(colstsalesamt, i).Value)
                cashamt = CDbl(.Item(colstcash, i).Value)
                pricegroup = Val(.Item(colpricegroup, i).Value)
                Dim _qurey As EnumerableRowCollection(Of DataRow)
                _qurey = From data In dt.AsEnumerable() Where Val(data("mc_customerid") & "") = customerid Select data
                If _qurey.Count > 0 Then
                    dtinvtr = _qurey.CopyToDataTable()
                Else
                    dtinvtr = dtitem.Clone
                End If
                If dtinvtr.Rows.Count > 0 Then
                    If trid = 0 Then
                        getVrsDet(0, "IS", prefix, invno, vrAccountNo1, vrAccountNo2)
                    End If
                    setInvCmnValue(trid, cashamt, invno, prefix, customerid, salesamt, pricegroup, customername)
                    trid = Val(_objInv._saveCmn())
                    _objcmnbLayer.savewithoutparam("update ItmInvCmnTb set isfromexternal=1 where trid=" & trid)
                    mi_mcid = dtinvtr(0)("mi_mcid")
                    If dtInvTb Is Nothing Then
                        dtInvTb = _objcmnbLayer._fldDatatable("Select top 1 * from ItmInvTrTb")
                    End If
                    Dim dtrow As DataRow
                    dtInvTb.Rows.Clear()
                    TDrAmt = 0
                    For j = 0 To dtinvtr.Rows.Count - 1
                        dtrow = dtInvTb.NewRow
                        dtrow("TrId") = trid
                        dtrow("ItemId") = dtinvtr(j)("mi_itemid")
                        dtrow("IDescription") = dtinvtr(j)("Description")
                        dtrow("Unit") = dtinvtr(j)("Unit")
                        dtrow("TrQty") = CDbl(dtinvtr(j)("mi_trqty"))
                        dtrow("Focqty") = 0
                        dtrow("UnitCost") = CDbl(dtinvtr(j)("mi_price"))
                        TDrAmt = TDrAmt + CDbl(dtinvtr(j)("mi_price"))
                        dtrow("taxP") = CDbl(dtinvtr(j)("mi_taxp"))
                        dtrow("taxAmt") = CDbl(dtinvtr(j)("mi_taxamt"))
                        dtrow("PFraction") = 1
                        dtrow("Method") = "B"
                        dtrow("SlNo") = j + 1
                        dtrow("TrTypeNo") = getVouchernumber("IS")
                        dtrow("TrDateNo") = getDateNo(CDate(cldrdate.Value))
                        dtrow("id") = 0
                        dtrow("HSNCode") = dtinvtr(j)("HSNCode")
                        dtrow("CSGTP") = CDbl(dtinvtr(j)("mi_taxp")) / 2
                        dtrow("SGSTP") = CDbl(dtinvtr(j)("mi_taxp")) / 2
                        dtrow("IGSTP") = CDbl(dtinvtr(j)("mi_taxp"))
                        dtrow("IGSTAmt") = CDbl(dtinvtr(j)("mi_taxamt"))
                        dtrow("CGSTAMT") = CDbl(dtinvtr(j)("mi_taxamt")) / 2
                        dtrow("SGSTAmt") = CDbl(dtinvtr(j)("mi_taxamt")) / 2
                        dtInvTb.Rows.Add(dtrow)
                        dtype = ""
                        For x = 0 To dtInvTb.Columns.Count - 1
                            If dtInvTb.Columns(x).ColumnName = "id" Then GoTo nxt
                            dtype = dtInvTb.Columns(x).DataType.Name
                            If Trim(dtInvTb(j)(x) & "") = "" Then
                                Select Case dtype
                                    Case "String"
                                        dtInvTb(j)(x) = ""
                                    Case "Int64", "Int32", "Double", "Decimal", "Boolean"
                                        dtInvTb(j)(x) = 0
                                End Select
                            End If
nxt:
                        Next
                    Next
                    _objInv.savebulktoInvTr(dtInvTb)
                    updateAccounts(trid, prefix, invno, salesamt, customerid, cashamt, customername)
                    _objcmnbLayer.savewithoutparam("update MilkCustomerTb set mc_invid=" & trid & " where mc_id=" & mi_mcid)
                End If
            Next
        End With
    End Sub
    Private Sub updateAccounts(ByVal trid As Long, ByVal prefix As String, ByVal invno As Integer, _
                               ByVal netsale As Double, ByVal Cusomerid As Long, ByVal cashamt As Double, ByVal Customername As String)
        Dim dtTable As DataTable
        Dim LinkNo As Long
        dtTable = _objcmnbLayer._fldDatatable("SELECT LinkNo FROM AccTrCmn WHERE lnkno  = " & trid)
        If dtTable.Rows.Count > 0 Then
            LinkNo = dtTable(0)("LinkNo")
            _objcmnbLayer.savewithoutparam("DELETE FROM AccTrDet WHERE LinkNo=" & LinkNo)
        End If
        dtTable = _objcmnbLayer._fldDatatable("select invno,prefix from itminvcmntb where trid=" & trid)
        If dtTable.Rows.Count > 0 Then
            invno = dtTable(0)(0)
            prefix = dtTable(0)(1)
        End If
        _objTr.JVType = "IS"
        _objTr.JVDate = DateValue(cldrdate.Value)
        _objTr.PreFix = prefix
        _objTr.JVNum = invno
        _objTr.JVTypeNo = getVouchernumber("IS")
        _objTr.UserId = Trim(CurrentUser & "")
        _objTr.MchName = MACHINENAME
        _objTr.CrtDtTm = Date.Now
        _objTr.TypeNo = prefixid
        '_objTr.VrDescr = Trim(txtpayee.Text)
        _objTr.IsModi = IIf(LinkNo > 0, 1, 0)
        _objTr.LinkNo = trid
        _objTr.taxablevalue = 0
        _objTr.taxvalue = 0
        _objTr.isLinkNo = False
        If dtAccTb.Rows.Count > 0 Then
            dtAccTb.Rows.Clear()
        End If
        'SALES
        'DEBIT
        Dim reference As String = prefix & IIf(prefix = "", "", "/") & invno
        setAcctrDetValue(LinkNo, Cusomerid, reference, "SALES", netsale, "", "", 0, 0, "", 0, Val(txtsalesac.Tag), "", "", 1, "", 0)
        'CREDIT
        setAcctrDetValue(LinkNo, Val(txtsalesac.Tag), reference, "SALES", netsale * -1, "", "", 0, 0, "", 0, Cusomerid, "", "", 1, "", 0)
        'PAYMENT
        If cashamt > 0 And Val(txtcash.Tag) > 0 Then
            'DEBIT
            setAcctrDetValue(LinkNo, Val(txtcash.Tag), reference, "AMOUNT RECEIVED FROM " & Customername, cashamt, "", "", 0, 0, "", 0, Cusomerid, "", "", 1, "", 0)
            'CREDIT
            setAcctrDetValue(LinkNo, Cusomerid, reference, "AMOUNT RECEIVED BY CASH", cashamt * -1, "", "", 0, 0, "", 0, Val(txtcash.Tag), "", "", 1, "", 0)

            _objcmnbLayer.savewithoutparam("DELETE FROM SalesMultipleDebitsTb " & _
                                                 " WHERE setremove=1 AND dbtrid=" & trid & _
                                                 " INSERT INTO SalesMultipleDebitsTb (dbtrid,dbaccid,accAmt,reference) VALUES" & _
                                                  "(" & trid & "," & Val(txtcash.Tag) & "," & cashamt & ",'" & reference & "')")
        End If
        _objTr.SaveAccTrWithDt(dtAccTb)
    End Sub
    Private Sub setAcctrDetValue(ByVal lnkNo As Long, ByVal AccountNo As Long, ByVal Reference As String, ByVal EntryRef As String, ByVal DealAmt As Double, ByVal JobCode As String, ByVal JobStr As String, _
                              ByVal TrInf As Short, ByVal OthCost As Short, ByVal LPO As String, ByVal TermsId As String, ByVal CustAcc As Integer, ByVal AccWithRef As String, _
                              ByVal CurrencyCode As String, ByVal CurrRate As Double, Optional ByVal vatcode As String = "", Optional ByVal UnqNo As Long = 0)

        Dim dtrow As DataRow
        Dim dtLPO As Date = IIf(chkDate(cldrdate.Value), cldrdate.Value, DateValue(cldrdate.Value))
        Dim dtDue As Date = DateValue(cldrdate.Value)
        Dim dtSup As Date = DateValue(cldrdate.Value)
        dtrow = dtAccTb.NewRow
        dtrow("LinkNo") = lnkNo
        dtrow("AccountNo") = AccountNo
        dtrow("Reference") = Reference ' Trim(txtprefix.Text) & IIf(txtprefix.Text = "", "", "/") & numVchrNo.Text
        dtrow("DueDate") = Format(DateValue(dtDue), "yyyy/MM/dd")
        dtrow("EntryRef") = EntryRef
        dtrow("DealAmt") = DealAmt
        dtrow("FCAmt") = DealAmt * CurrRate
        dtrow("CurrencyCode") = CurrencyCode
        dtrow("CurrRate") = CurrRate
        dtrow("TrInf") = TrInf
        dtrow("OthCost") = OthCost
        dtrow("TermsId") = TermsId
        dtrow("CustAcc") = CustAcc
        dtrow("AccWithRef") = AccWithRef
        dtrow("LPONo") = LPO
        dtrow("UnqNo") = UnqNo
        dtrow("JobCode") = ""

        'dtrow("vatcode") = vatcode
        'dtrow("SuppInvDate") = Format(DateValue(cldrdate.Value), "yyyy/MM/dd")
        dtAccTb.Rows.Add(dtrow)
        Dim j As Integer
        Dim dtype As String
        For j = 0 To dtAccTb.Columns.Count - 1
            dtype = dtAccTb.Columns(j).DataType.Name
            If Trim(dtAccTb(dtAccTb.Rows.Count - 1)(j) & "") = "" Then
                Select Case dtype
                    Case "String"
                        dtAccTb(dtAccTb.Rows.Count - 1)(j) = ""
                    Case "Int64", "Int32", "Double", "Decimal", "Boolean"
                        dtAccTb(dtAccTb.Rows.Count - 1)(j) = 0
                End Select
            End If
nxt:
        Next
    End Sub
    Private Sub setInvCmnValue(ByVal InvTrid As Long, ByVal paidamt As Double, ByVal invno As Integer, _
                                 ByVal prefix As String, ByVal customerid As Long, ByVal netamt As Double, ByVal pricegroup As Integer, ByVal CashCustName As String)
        With _objInv
            .TrId = InvTrid
            .TrDate = DateValue(cldrdate.Value)
            .TrType = "IS"
            .DocLstTxt = ""
            .CashCustName = CashCustName
            .InvTypeNo = prefixid 'prefixid
            .SlsManId = cmbsalesman.Text
            .Prefix = prefix
            .InvNo = invno
            .TrRefNo = prefix & IIf(prefix = "", "", "/") & invno ' Trim(txtReference.Text)
            .CSCode = customerid
            .PSAcc = Val(txtsalesac.Tag)
            .JobCode = ""
            .ImpDoc = 0
            .UserId = CurrentUser
            .IsFC = False
            .FCRate = 1
            .NFraction = NoOfDecimal
            .FC = ""
            .Discount = 0
            .TrDescription = ""
            .TypeNo = getVouchernumber("IS")
            .EnaJob = False
            .DocDefLoc = 0
            .BrId = UsrBr
            .OthCost = 0
            .Discount1 = 0
            .NetAmt = netamt
            .LPO = ""
            'Dim dt As Date = getServerDate()
            .CrtDt = Date.Now
            .DelDate = DateValue(cldrdate.Value)
            .DueDate = DateValue(cldrdate.Value)
            .DocDate = DateValue(cldrdate.Value)
            .SuppInvDate = DateValue(cldrdate.Value)
            .TermsId = ""
            .MchName = MACHINENAME
            .isModi = IIf(InvTrid > 0, True, False)
            .lpoclass = ""
            .rndoff = 0
            'If TaxType is 0 then invoice is interstate invoice otherwise intrastate invoice
            .TaxType = 0
            .OthrCust = ""
            .DocLstTxt = ""
            .isTaxInvoice = False
            .isImportOrExport = False
            .priceType = pricegroup
            .GSTN = ""
        End With

    End Sub
    Private Sub fwait_triggerEvent() Handles fwait.triggerEvent
        Select Case fwait.triggerType
            Case 1
                saveMilkSales()
            Case 2
                loadCustomer()
                loadsales()
                load_todaysalessum()
            Case 3
                PrepareRpt(STRptType)
        End Select
        If Not fwait Is Nothing Then fwait.Close() : fwait = Nothing
    End Sub
    Private Sub loadWaite(ByVal triggerType As Integer)
        If Not fwait Is Nothing Then fwait.Close() : fwait = Nothing
        fwait = New WaitMessageFrm
        With fwait
            .triggerType = triggerType
            .Show()
        End With
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        If grdVoucher.Rows.Count = 0 Then
            MsgBox("Customers not found in list", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If Val(txtcash.Tag) = 0 Or Val(txtsalesac.Tag) = 0 Then
            MsgBox("Sales Invoice Vouchertype not found", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If MsgBox("Do you like to file it ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = vbYes Then
            loadWaite(1)
        End If
        chngfound = False
        btnUpdateEnable()
    End Sub
    Private Sub loadsales()
        If Not dtcustomersales Is Nothing Then
            If dtcustomersales.Rows.Count > 0 Then dtcustomersales.Rows.Clear()
        End If

        Dim qry As String
        Dim dt As DataTable
        qry = "select * from MilkCmnTb where milk_date='" & Format(cldrdate.Value, "yyyy/MM/dd") & "' and miilk_areacode='" & cmbroute.Text & "'"
        dt = _objcmnbLayer._fldDatatable(qry)
        If dt.Rows.Count > 0 Then
            txtbillno.Tag = dt(0)("milkcmnid")
            txtrmk.Text = Trim(dt(0)("remark") & "")
            txtbillno.Text = Val(dt(0)("billno") & "")
            cldrdate.Enabled = False
        Else
            txtbillno.Tag = 0
            txtrmk.Text = ""
            addnew(True)
            Exit Sub
        End If
        qry = "select MilkCustomerTb.*,mi_itemid,mi_trqty,mi_price,mi_taxp,mi_taxamt,isnull([item code],'') itemcode,accdescr,isnull(trid,0)trid,isnull(trrefno,'')trrefno from MilkCustomerTb " & _
            "left join MilkItemTb on MilkItemTb.mi_mcid=MilkCustomerTb.mc_id " & _
            "left join invitm on invitm.itemid=MilkItemTb.mi_itemid " & _
            "left join accmast on accmast.accid=MilkCustomerTb.mc_customerid " & _
            "left join itminvcmntb on itminvcmntb.trid=MilkCustomerTb.mc_invid " & _
            "where MC_cmnid=" & Val(txtbillno.Tag)
        dtcustomersales = _objcmnbLayer._fldDatatable(qry)
        Dim i As Integer
        Dim j As Integer
        Dim itemcode As String
        Dim customerid As Long
        Dim pricedt As DataTable
        chgbyprg = True
        With grdVoucher
            For i = 1 To .RowCount - 1
                'If i = 4 Then
                '    MsgBox(i)
                'End If

                customerid = Val(.Item(colcustomerid, i).Value)
                If customerid = 0 Then GoTo nxt
                '.Item(colstsalesamt, i).Value = Format(0, numFormat)
                '.Item(colstlastdue, i).Value = Format(0, numFormat)
                '.Item(colstcash, i).Value = Format(0, numFormat)
                '.Item(colsttotaldue, i).Value = Format(0, numFormat)
                For j = constInvno + 1 To colstsalesamt - 1
                    itemcode = .Columns(j).HeaderText
                    Dim _qurey As EnumerableRowCollection(Of DataRow)
                    _qurey = From data In dtcustomersales.AsEnumerable() Where data("itemcode") = itemcode And data("mc_customerid") = customerid Select data
                    If _qurey.Count > 0 Then
                        pricedt = _qurey.CopyToDataTable()
                    Else
                        pricedt = dtcustomersales.Clone
                    End If
                    If pricedt.Rows.Count > 0 Then
                        .Item(j, i).Value = pricedt(0)("mi_trqty")
                        '.Item(constcustomername, i).Value = pricedt(0)("accdescr")
                        .Item(colmc_id, i).Value = pricedt(0)("mc_id")
                        .Item(constInvno, i).Value = pricedt(0)("trrefno")
                        .Item(colmc_invid, i).Value = Val(pricedt(0)("trid") & "")
                        .Item(colstsalesamt, i).Value = Format(pricedt(0)("mc_saleamt"), numFormat)
                        If Val(pricedt(0)("mc_lastdueamt")) <> 0 Then
                            .Item(colstlastdue, i).Value = Format(pricedt(0)("mc_lastdueamt"), numFormat)
                        End If
                        .Item(colstcash, i).Value = Format(pricedt(0)("mc_cashamt"), numFormat)
                        If Val(pricedt(0)("mc_totaldueamt")) <> 0 Then
                            .Item(colsttotaldue, i).Value = Format(pricedt(0)("mc_totaldueamt"), numFormat)
                        End If
                    Else
                        .Item(j, i).Value = 0
                    End If

                    If pricedt.Rows.Count > 0 Then pricedt.Rows.Clear()
                    'grdVoucher.Item(colstsalesamt, i).Value = 0
                    'getItemPrice(i, j)
                Next
nxt:
            Next
        End With
        calculate()
        qry = "select MilkSummaryTb.*,GrpItmCode from MilkSummaryTb left join GrpItmTb on GrpItmTb.UnqGrpId=MilkSummaryTb.ms_grpid where ms_cmnid=" & Val(txtbillno.Tag)
        dt = _objcmnbLayer._fldDatatable(qry)
        If dt.Rows.Count > 0 Then
            With grdother
                .Rows.Clear()
                For i = 0 To dt.Rows.Count - 1
                    .Rows.Add()
                    .Item(constItems, i).Value = dt(i)("GrpItmCode")
                    .Item(constgrpid, i).Value = dt(i)("ms_grpid")
                    .Item(constOrd, i).Value = dt(i)("ms_ord")
                    .Item(constXta, i).Value = dt(i)("ms_xta")
                    .Item(constRtn, i).Value = dt(i)("ms_rtn")
                    .Item(constFree, i).Value = dt(i)("ms_free")
                    .Item(constleak, i).Value = dt(i)("ms_leak")
                    .Item(constsold, i).Value = dt(i)("ms_sold")
                    .Item(constDiff, i).Value = dt(i)("ms_diff")
                    .Item(constTray, i).Value = dt(i)("ms_tray")
                Next
            End With
        Else
            loadGroup()
        End If
        chgbyprg = False
        btnupdate.Enabled = False
    End Sub
    Private Sub loaddefaultAccounts()
        Dim dt As DataTable
        Dim qry As String
        qry = "SELECT * FROM PreFixTb " & _
              "left join (select accdescr accname1,accid accid1 from accmast)acc1 on prefixtb.ANo=acc1.accid1 " & _
              "left join (select accdescr accname2,accid accid2 from accmast)acc2 on prefixtb.ANo2=acc2.accid2 " & _
              "where VrTypeNo=4 and ordNo=1 and isnull(accid1,0)>0 and isnull(accid2,0)>0 " & IIf(UsrBr = "", "", " AND BrId In ('', '" & UsrBr & "')")
        dt = _objcmnbLayer._fldDatatable(qry)
        If dt.Rows.Count > 0 Then
            txtsalesac.Tag = dt(0)("ANo")
            txtsalesac.Text = dt(0)("accname1")
            txtcash.Tag = dt(0)("ANo2")
            txtcash.Text = dt(0)("accname2")
            prefixid = dt(0)("Id")
        End If

    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        If chngfound = True Then
            If MsgBox("changes found do u want to continue ?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation) = MsgBoxResult.No Then Exit Sub
        End If
        addnew()
        chngfound = False
    End Sub
    Private Sub addnew(Optional ByVal skipgridclear As Boolean = False)
        'loadCustomer()
        If skipgridclear = False Then
            grdVoucher.Rows.Clear()
            cmbroute.Text = ""
            cldrdate.Value = DateValue(Date.Now)
        End If
        cmbsalesman.Text = ""
        txtrmk.Text = ""
        txtbillno.Tag = 0
        loadGroup()
        cldrdate.Enabled = True
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("select max(billno)billno from MilkCmnTb")
        If dt.Rows.Count > 0 Then
            txtbillno.Text = Val(dt(0)("billno") & "") + 1
        Else
            txtbillno.Text = 1
        End If
    End Sub

    Private Sub btnstatement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnstatement.Click
        STRptType = "ST1"
        If chkFormat.Checked Then
            fRptFormat = New RptFormatfrm
            fRptFormat.RptType = STRptType
            fRptFormat.ShowDialog()
            fRptFormat = Nothing
        Else
            loadWaite(3)
        End If
    End Sub
    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        LoadReport(RptFlName, RptCaption, forPrint)
    End Sub
    Private Sub LoadReport(ByVal FileName As String, ByVal RptCaption As String, Optional ByVal ToPrint As Boolean = False)
        If Not frm Is Nothing Then
            frm.Close()
            frm = New ReprtviewNEWfrm
        Else
            frm = New ReprtviewNEWfrm
        End If
        Dim ds As New DataSet
        Dim accid As Long
        Dim trid As Long
        If grdVoucher.CurrentRow Is Nothing Then Exit Sub
        If grdVoucher.CurrentRow.Index = 0 Then
            MsgBox("Please Select Customer", MsgBoxStyle.Information)
            Exit Sub
        End If
        accid = Val(grdVoucher.Item(colcustomerid, grdVoucher.CurrentRow.Index).Value)
        trid = Val(grdVoucher.Item(colmc_invid, grdVoucher.CurrentRow.Index).Value)
        If STRptType = "ST1" Then
            If accid = 0 Then Exit Sub
            Dim frmdate As New DateRangeFrm
            fwait.Hide()
            frmdate.TopMost = True
            frmdate.ShowDialog()
            If Val(frmdate.btnapply.Tag) = 0 Then
                fwait.Visible = True
                fwait.Focus()
                fwait.TopMost = True
                Exit Sub
            End If
            Dim dt1 As Date = DateValue(frmdate.cldrStartDate.Value)
            Dim dt2 As Date = DateValue(frmdate.cldrEnddate.Value)
            frmdate = Nothing
            ds = _objTr.returnLEDGERstatementreport(dt1, dt2, accid, 0, 0, "customer", 0)
            fwait.Visible = True
        ElseIf STRptType = "STO" Then
            If accid = 0 Then Exit Sub
            ds = _objTr.returnStatementReport(DateValue(Date.Now), DateValue(Date.Now), accid, 2, 0, "customer", 0)
            Dim parmDt As DataTable
            parmDt = _objcmnbLayer._fldDatatable(" select 1 Lnk, '" & Format(DateValue(Date.Now), "dd/MM/yyyy") & "' As dtAsOn, 30" & _
                                                                    " As Ag1, 60 As Ag2, 90 As Ag3, " & _
                                                                    "120 As Ag4, '0' As SepPage, ''" & _
                                                                    " As WiseFld, 'Ageing based on  Invoice Date.'" & _
                                                                    " As Msg , '" & Format(DateValue(Date.Now), "dd/MM/yyyy") & "' As FDate,0 as IsPeriod,'' as Isadv From CompanyTb")
            ds.Tables.Add(parmDt)
        ElseIf STRptType = "IS" Then
            Dim dt As DataTable
            If trid = 0 Then Exit Sub
            dt = _objcmnbLayer._fldDatatable("select invno,prefix from itminvcmntb where trid=" & trid)
            If dt.Rows.Count > 0 Then
                _objInv.Prefix = dt(0)("prefix")
                _objInv.InvNo = dt(0)("invno")
                _objInv.TrType = "IS"
                If _objInv.InvNo = 0 Then Exit Sub
                ds = _objInv.ldInvoice("rturnInventoryDetailsForInvoicePrint", 0)
            End If
       
        ElseIf STRptType = "MCSS" Then
            Dim dt As DataTable
            dt = prepareCustomerOutstandingList()
            ds.Tables.Add(dt)
            Dim dtmilk As DataTable
            dtmilk = itemgrouplist()
            ds.Tables.Add(dtmilk)

        End If
        frm.SetReport(ds, FileName, 0, False, , ToPrint)
        frm.MdiParent = Me.MdiParent
        frm.Text = RptCaption
        If Not ToPrint Then frm.Show()
    End Sub
    Private Sub PrepareRpt(ByVal RptType As String, Optional ByVal Forprint As Boolean = False, Optional ByVal isF As Boolean = False)
        Dim RptName As String
        Dim RptCaption As String = ""
        RptName = getRptDefFlName(RptType, RptCaption)
        If Trim(RptName) <> "" Then
            LoadReport(RptName, RptCaption, Forprint)
        End If
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        If grdVoucher.RowCount <= 1 Then Exit Sub
        STRptType = "IS"
        If chkFormat.Checked Then
            fRptFormat = New RptFormatfrm
            fRptFormat.RptType = STRptType
            fRptFormat.ShowDialog()
            fRptFormat = Nothing
        Else
            loadWaite(3)
        End If
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        If MsgBox("Do you want to delete?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        Dim qry As String
        qry = "delete from MilkCmnTb where milkcmnid=" & Val(txtbillno.Tag)
        qry = qry & "delete from MilkCustomerTb where MC_cmnid=" & Val(txtbillno.Tag)
        qry = qry & "delete from MilkItemTb where mi_cmnid=" & Val(txtbillno.Tag)
        qry = qry & "delete from MilkSummaryTb where ms_cmnid=" & Val(txtbillno.Tag)
        Dim i As Integer
        With grdVoucher
            For i = 0 To .RowCount - 1
                _objInv.TrId = Val(.Item(colmc_invid, i).Value)
                _objInv.TrType = "OUT"
                _objInv.deleteInventoryTransactions()
            Next
        End With
        _objcmnbLayer.savewithoutparam(qry)
        addnew()
        chngfound= False
    End Sub

    Private Sub pvdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pvdate.Click
        If chngfound = True Then
            If MsgBox("changes found do u want to continue ?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation) = MsgBoxResult.No Then Exit Sub
        End If
        cldrdate.Value = DateAdd(DateInterval.Day, -1, DateValue(cldrdate.Value))
        loadbtn(True)

    End Sub

    Private Sub nxdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nxdate.Click
        If chngfound = True Then
            If MsgBox("changes found do u want to continue ?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation) = MsgBoxResult.No Then Exit Sub
        End If
        cldrdate.Value = DateAdd(DateInterval.Day, 1, DateValue(cldrdate.Value))
        loadbtn(True)
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub grdVoucher_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellDoubleClick
        loadcustomerprice()
    End Sub
    Private Sub loadcustomerprice()
        If grdVoucher.CurrentRow Is Nothing Then Exit Sub
        If grdVoucher.CurrentCell.ColumnIndex = constcustomername Then
            If frmMilkCustomerPriceFrm Is Nothing Then
                frmMilkCustomerPriceFrm = New MilkCustomerPriceFrm
            End If
            With frmMilkCustomerPriceFrm
                Dim dt As DataTable = _objcmnbLayer._fldDatatable("select '' itemcode,0.00 price from invitm where itemid=0")
                Dim drow As DataRow
                Dim price As Double
                Dim gst As Double
                Dim itemid As Long
                For i = constInvno + 1 To colstsalesamt - 1
                    price = 0
                    gst = 0
                    itemid = 0
                    returnItemPrice(grdVoucher.Columns(i).HeaderText, Val(grdVoucher.Item(colcustomerid, grdVoucher.CurrentRow.Index).Value), Val(grdVoucher.Item(colpricegroup, grdVoucher.CurrentRow.Index).Value), price, gst, itemid)
                    price = price + (price * gst / 100)
                    drow = dt.NewRow
                    drow("itemcode") = grdVoucher.Columns(i).HeaderText
                    drow("price") = price
                    dt.Rows.Add(drow)
                Next
                .dt = dt
                Dim pricegroup As String
                If grdVoucher.Item(colpricegroup, grdVoucher.CurrentRow.Index).Value = 0 Then
                    pricegroup = "Unit Prce"
                ElseIf grdVoucher.Item(colpricegroup, grdVoucher.CurrentRow.Index).Value = 1 Then
                    pricegroup = "WS Price"
                ElseIf grdVoucher.Item(colpricegroup, grdVoucher.CurrentRow.Index).Value = 2 Then
                    pricegroup = "Second Price"
                Else
                    pricegroup = "Price" & (grdVoucher.Item(colpricegroup, grdVoucher.CurrentRow.Index).Value - 2)
                End If
                .lblcustomer.Text = grdVoucher.Item(constcustomername, grdVoucher.CurrentRow.Index).Value & " Price Group : [" & pricegroup & "]"
                '.MdiParent = fMainForm
                If frmMilkCustomerPriceFrm.Visible = False Then .Show()
                .Focus()
                .loaditems()
            End With
        End If
    End Sub


    Private Sub load_todaysalessum()

        Dim qry As String
        qry = "select miilk_areacode,billno, sum(mc_saleamt)mc_saleamt,SUM(mc_lastdueamt)mc_lastdueamt,SUM(mc_cashamt)mc_cashamt,SUM(mc_totaldueamt)mc_totaldueamt from MilkCmnTb " & _
                                            "left join MilkCustomerTb on MilkCustomerTb.MC_cmnid  = milkcmntb.milkcmnid WHERE milk_date='" & Format(DateValue(cldrdate.Value), "yyyy/MM/dd") & "' GROUP BY	miilk_areacode,billno " & _
                                            "union all select 'TOTAL' miilk_areacode,0 billno, sum(mc_saleamt)mc_saleamt,SUM(mc_lastdueamt)mc_lastdueamt,SUM(mc_cashamt)mc_cashamt,SUM(mc_totaldueamt)mc_totaldueamt from MilkCmnTb " & _
                                            "left join MilkCustomerTb on MilkCustomerTb.MC_cmnid  = milkcmntb.milkcmnid WHERE milk_date='" & Format(DateValue(cldrdate.Value), "yyyy/MM/dd") & "' "

        Dim dtsum As DataTable
        dtsum = _objcmnbLayer._fldDatatable(qry)
        grddatesale.DataSource = dtsum
        With grddatesale
            SetGridProperty(grddatesale)
            .Rows(.RowCount - 1).DefaultCellStyle.ForeColor = Color.Red
            .Rows(.RowCount - 1).DefaultCellStyle.BackColor = Color.GreenYellow
            .Rows(.RowCount - 1).DefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.5!, System.Drawing.FontStyle.Bold)
         
            .Columns("miilk_areacode").HeaderText = "Sale Root"
            .Columns("miilk_areacode").Width = 100
            .Columns("miilk_areacode").ReadOnly = True

            .Columns("billno").HeaderText = "Bill No"
            .Columns("billno").Width = 70
            .Columns("billno").ReadOnly = True

            .Columns("mc_saleamt").HeaderText = "Sale AMT"
            .Columns("mc_saleamt").Width = 100
            .Columns("mc_saleamt").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("mc_saleamt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("mc_saleamt").ReadOnly = True


            .Columns("mc_lastdueamt").HeaderText = "Last Due"
            .Columns("mc_lastdueamt").Width = 100
            .Columns("mc_lastdueamt").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("mc_lastdueamt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("mc_lastdueamt").ReadOnly = True


            .Columns("mc_cashamt").HeaderText = "Cash"
            .Columns("mc_cashamt").Width = 100
            .Columns("mc_cashamt").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("mc_cashamt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


            .Columns("mc_totaldueamt").HeaderText = "Total Due"
            .Columns("mc_totaldueamt").Width = 100
            .Columns("mc_totaldueamt").DefaultCellStyle.Format = "N0" & NoOfDecimal
            .Columns("mc_totaldueamt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("mc_totaldueamt").ReadOnly = True

            Dim i As Integer = Val(.Tag)
            .CurrentCell = .Item(0, i)
        End With



    End Sub

    'Private Sub cmbroute_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbroute.SelectedIndexChanged
    '    If chngfound = True Then
    '        If MsgBox("changes found do u want to continue ?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation) = MsgBoxResult.No Then Exit Sub
    '    End If
    '    load_todaysalessum()
    '    loadWaite(2)
    '    chngfound = False
    'End Sub



    Private Sub cmbroute_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbroute.SelectedIndexChanged
        loadbtn()
    End Sub
    Private Sub loadbtn(Optional ByVal skipmessage As Boolean = False)
        If chngfound = True And skipmessage = False Then
            If MsgBox("changes found do u want to continue ?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation) = MsgBoxResult.No Then Exit Sub
            btnupdate.Enabled = True
        End If
        cldrdate.Enabled = True
        'load_todaysalessum()
        loadWaite(2)
        chngfound = False
        btnUpdateEnable()
    End Sub

    Private Sub btnotsd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnotsd.Click
        STRptType = "STO"
        If chkFormat.Checked Then
            fRptFormat = New RptFormatfrm
            fRptFormat.RptType = STRptType
            fRptFormat.ShowDialog()
            fRptFormat = Nothing
        Else
            loadWaite(3)
        End If
    End Sub
    Private Sub btnUpdateEnable()
        If chngfound Then
            btnupdate.Enabled = True
        Else
            btnupdate.Enabled = False
        End If
    End Sub

    Private Sub frmMilkCustomerPriceFrm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles frmMilkCustomerPriceFrm.FormClosed
        frmMilkCustomerPriceFrm = Nothing
    End Sub


    Private Sub grddatesale_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grddatesale.DoubleClick
        If grddatesale.RowCount = 0 Then Exit Sub
        If grddatesale.CurrentRow Is Nothing Then Exit Sub
        cmbroute.Text = grddatesale.Item(0, grddatesale.CurrentRow.Index).Value
        If chngfound = True Then
            If MsgBox("changes found do u want to continue ?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation) = MsgBoxResult.No Then Exit Sub
        End If
        grddatesale.Tag = grddatesale.CurrentRow.Index
        loadWaite(2)
        chngfound = False
    End Sub
    Private Function prepareCustomerOutstandingList() As DataTable
        Dim i As Integer
        Dim j As Integer
        Dim dt As DataTable
        Dim dr As DataRow
        'Creating datatable structur
        dt = _objcmnbLayer._fldDatatable("select '' cname,'' items,convert(money,0)salesamt,convert(money,0) lastdue,convert(money,0) cashamt,convert(money,0) totaldue,'' routename, getdate()t_date,'' salesman,1 lnk")
        Dim items As String
        dt.Rows.Clear()
        With grdVoucher
            For i = 1 To .RowCount - 1
                dr = dt.NewRow

                dr("cname") = .Item(constcustomername, i).Value
                dr("salesamt") = Format(CDbl(.Item(colstsalesamt, i).Value))
                dr("lastdue") = Format(CDbl(.Item(colstlastdue, i).Value))
                dr("cashamt") = Format(CDbl(.Item(colstcash, i).Value))
                dr("totaldue") = Format(CDbl(.Item(colsttotaldue, i).Value))
                items = ""
                For j = constInvno + 1 To colstsalesamt - 1
                    If Val(.Item(j, i).Value & "") > 0 Then
                        items = items & IIf(items = "", "", ",") & .Columns(j).HeaderText & " : " & Val(.Item(j, i).Value & "")
                    End If

                Next
                dr("items") = items
                dr("lnk") = 1
                dr("routename") = cmbroute.Text
                dr("t_date") = cldrdate.Value
                dr("salesman") = cmbsalesman.Text
                dt.Rows.Add(dr)
            Next
        End With
        Return dt
    End Function
  

    Private Sub btnprw_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprw.Click
        STRptType = "MCSS"
        If chkFormat.Checked Then
            fRptFormat = New RptFormatfrm
            fRptFormat.RptType = STRptType
            fRptFormat.ShowDialog()
            fRptFormat = Nothing
        Else
            loadWaite(3)
        End If
    End Sub
    Private Function itemgrouplist() As DataTable
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("select MilkSummaryTb.*,GrpItmCode from MilkSummaryTb left join GrpItmTb on GrpItmTb.UnqGrpId=MilkSummaryTb.ms_grpid where ms_cmnid=" & Val(txtbillno.Tag))
        Dim items As String
        Dim dr As DataRow
        dt.Rows.Clear()
        With grdother
            For i = 1 To .RowCount - 1
                dr = dt.NewRow
                dr("GrpItmCode") = .Item(constItems, i).Value
                dr("ms_ord") = Format(CDbl(.Item(constOrd, i).Value))
                dr("ms_xta") = Format(CDbl(.Item(constXta, i).Value))
                dr("ms_rtn") = Format(CDbl(.Item(constRtn, i).Value))
                dr("ms_free") = Format(CDbl(.Item(constFree, i).Value))
                dr("ms_leak") = Format(CDbl(.Item(constleak, i).Value))
                dr("ms_sold") = Format(CDbl(.Item(constsold, i).Value))
                dr("ms_diff") = Format(CDbl(.Item(constDiff, i).Value))
                dr("ms_tray") = Format(CDbl(.Item(constTray, i).Value))
                items = ""
                dt.Rows.Add(dr)
            Next
        End With
        Return dt
    End Function


End Class