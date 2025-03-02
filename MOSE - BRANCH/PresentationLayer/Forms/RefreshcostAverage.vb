Public Class RefreshcostAverage
    Private _objcmnbLayer As New clsCommon_BL
    Private _objInv As New clsInvoice
    Private _objTr As New clsAccountTransaction
    Delegate Sub GenericDelegate(ByVal Mname As String, ByVal RecName As String, ByVal rec As Integer, ByVal count As Integer)
    Public itemid As Integer
    Private Sub RefreshcostAverage_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'If itemid > 0 Then
        '    btnTransactionItems.Enabled = False
        '    Worker.RunWorkerAsync()
        '    Worker.WorkerSupportsCancellation = True
        'End If
    End Sub
    Public Sub status(ByVal Mname As String, ByVal RecName As String, ByVal rec As Integer, ByVal count As Integer)
        If Me.InvokeRequired Then
            Dim d As New GenericDelegate(AddressOf status)
            Me.Invoke(d, Mname, RecName, rec, count)
        Else
            lblitem.Text = Mname
            lblVal.Text = rec & " / " & count
            If rec = 0 Then
                pb.Value = 0
            Else
                pb.Value = rec * 100 / count
            End If
        End If
    End Sub

    Private Sub Worker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles Worker.DoWork
        refreshCost()
        'refreshWeightedAverage()
    End Sub
    Private Sub refreshWeightedAverage()
        Dim rdCount As Integer
        Dim i As Integer
        Dim dtISs As DataTable
        'GoTo update
        If itemid = 0 Then
            dtISs = _objcmnbLayer._fldDatatable("UPDATE ItmInvTrTb SET CostAvg=0 " & IIf(UsrBr = "", "", "WHERE Trid in (Select trid from ItmInvCmnTb WHERE DocDefLoc='" & Dloc & "')") & _
                                            " Select id,[item code],trdate,trtype from ItmInvTrTb LEFT JOIN ItmInvCmnTb ON  " & _
                                           "ItmInvTrTb.TRID=ItmInvCmnTb.TRID " & _
                                           "left join invitm on invitm.itemid=ItmInvTrTb.itemid " & _
                                           "LEFT JOIN VoucherTypeNoTb ON ItmInvTrTb.TrTypeNo=VoucherTypeNoTb.vrno " & _
                                           "where isnull(invStatus,0)=0 and (InvType='OUT' OR Trtype='SR')" & _
                                           IIf(UsrBr = "", "", " AND DocDefLoc='" & Dloc & "'") & " Order by [item code],trdate,id")
        Else
            dtISs = _objcmnbLayer._fldDatatable("UPDATE ItmInvTrTb SET CostAvg=0 WHERE ItemId=" & itemid & IIf(UsrBr = "", "", " AND Trid in (Select trid from ItmInvCmnTb WHERE DocDefLoc='" & Dloc & "')") & _
                                                " Select id,[item code],trdate,trtype from ItmInvTrTb LEFT JOIN ItmInvCmnTb ON  " & _
                                                "ItmInvTrTb.TRID=ItmInvCmnTb.TRID " & _
                                               "left join invitm on invitm.itemid=ItmInvTrTb.itemid " & _
                                               "LEFT JOIN VoucherTypeNoTb ON ItmInvTrTb.TrTypeNo=VoucherTypeNoTb.vrno " & _
                                               "where isnull(invStatus,0)=0 and (InvType='OUT' OR Trtype='SR') AND ItmInvTrTb.itemid=" & itemid & _
                                               IIf(UsrBr = "", "", " AND DocDefLoc='" & Dloc & "'") & " order by trdate,id")
        End If
        rdCount = dtISs.Rows.Count
        For i = 0 To rdCount - 1
            'MsgBox(dtISs(i)("trdate"))
            _objInv.returnTrCostForRefresh(dtISs(i)("id"))
            status("Calculating Transaction CostAsOn  " & Format(CDate(Now.Date), DtFormat) & "||" & dtISs(i)("item code"), "", i, rdCount)
        Next
        If MsgBox("Do you want to post cost in Accounts?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.No Then
            GoTo update
        End If
        If Not enableCostAccounting Or itemid > 0 Then GoTo update
        status("Updating Accounts", "", 0, 0)
        'SALES INVOICE costType=9
        dtISs = Nothing
        Dim strCost As String = "left join (select sum(CostAvg*TrQty) costAmt,trid from ItmInvTrTb group by trid)ItmInvTrTb on ItmInvTrTb.trid=ItmInvCmnTb.trid "
        Dim strAccounts As String = " left join AccTrCmn on ItmInvCmnTb.trid=AccTrCmn.lnkno"
        dtISs = _objcmnbLayer._fldDatatable("select ItmInvCmnTb.trid,ItmInvCmnTb.Prefix,invno,[job code] jbcode,TRtype,isnull(costAmt,0)costAmt,isnull(linkno,0)linkno from " & _
                                            "ItmInvCmnTb " & strCost & strAccounts & " WHERE TRtype ='IS'" & IIf(UsrBr = "", "", " AND Brid='" & UsrBr & "'"))
        rdCount = dtISs.Rows.Count - 1
        Dim stockAc As Long
        Dim costOfSalesAc As Long
        Dim costDiffAc As Long
        stockAc = getConstantAccounts(1)
        'costType=18 [MATERIAL CONSUMPTION], 'costType=9 [COST OF SALES],'costType=10 [COST DIFFERENCE]
        costOfSalesAc = getConstantAccounts(9)
        If dtAccTb.Rows.Count > 0 Then dtAccTb.Rows.Clear()
        For i = 0 To rdCount
            Dim trref As String = dtISs(i)("Prefix") & IIf(dtISs(i)("Prefix") = "", "", "\") & dtISs(i)("invno")
            status("Updating Accounts", "Stock Valuation on Sales Invoice \ " & trref, i, rdCount)
            updateStockTransactionOnLive(dtISs(i)("trid"), 9, dtISs(i)("jbcode"), trref, Val(dtISs(i)("linkno")), CDbl(dtISs(i)("costAmt")), stockAc, costOfSalesAc)
        Next
        _objTr.saveItmAccTrTbCostingBulk(dtAccTb)
update:
        If itemid = 0 Then
            dtISs = _objcmnbLayer._fldDatatable("Select ItmInvTrTb.itemid,[item code] from ItmInvTrTb LEFT JOIN ItmInvCmnTb ON  " & _
                                                          "ItmInvTrTb.TRID=ItmInvCmnTb.TRID " & _
                                                           "left join invitm on invitm.itemid=ItmInvTrTb.itemid " & _
                                                          "where isnull(invStatus,0)=0 " & _
                                                          IIf(UsrBr = "", "", " AND DocDefLoc='" & Dloc & "'") & " group by ItmInvTrTb.itemid,[item code]")
            rdCount = dtISs.Rows.Count
            For i = 0 To rdCount - 1
                'MsgBox(dtISs(i)("trdate"))
                _objcmnbLayer.updateCostAvarage(IIf(UsrBr = "", "", Dloc), Val(dtISs(i)("itemid")))
                status("Calculating CostAsOn  " & Format(CDate(Now.Date), DtFormat) & "||" & dtISs(i)("item code"), "", i, rdCount)
            Next
        Else
            _objcmnbLayer.updateCostAvarage(IIf(UsrBr = "", "", Dloc), itemid)
        End If
        status("Calculating CostAsOn Non Transaction items ", "", 0, 0)
        _objcmnbLayer._fldDatatable("update InvItm set CostAvg=isnull(opcost,0) " & _
                                    "where itemid not in (select itemid from ItmInvTrTb group by itemid)")
        _objcmnbLayer._fldDatatable("update LocOpnQtyTb set locationCost=isnull(opcost,0) from (select  isnull(opcost,0) opcost,ItemId from invitm " & _
                                   "where itemid not in (select itemid from ItmInvTrTb group by itemid) )cost where LocOpnQtyTb.ItemId=cost.ItemId ")
    End Sub
    Private Sub updateStockTransactionOnLive(ByVal trid As Long, ByVal costType As Integer, ByVal jobcode As String, _
                                             ByVal Trref As String, ByVal LinkNo As Long, ByVal costAmt As Double, _
                                             ByVal creditac As Long, ByVal debitac As Long)

        Dim entryref As String = ""

        _objcmnbLayer._saveDatawithOutParm("DELETE FROM AccTrDet WHERE trinf=3 and LinkNo=" & LinkNo)
        If costAmt <= 0 Then Exit Sub
        If LinkNo = 0 Then Exit Sub
        Select Case costType
            Case 9
                entryref = "COST OF SALES : Refernce#" & Trref
            Case 10
                entryref = "COST DIFFERENCE : Refernce#" & Trref
            Case 18
                entryref = "SERVICE STOCK OUT : JOB#" & jobcode
        End Select
        If creditac = 0 Or debitac = 0 Then Exit Sub
        If costAmt <> 0 Then
            'debit entry [cost of sales]/cost diff/material consumption
            setAcctrDetValueBulk(LinkNo, debitac, Trref, entryref, costAmt, "", "", 3, 0, "", _
                           "", creditac, debitac & Trref, "", 1)
            'UpdtClosBal(costOfSalesAc, costAmt)
            'credit entry [stock in hand]
            costAmt = costAmt * -1
            setAcctrDetValueBulk(LinkNo, creditac, Trref, entryref, costAmt, "", "", 3, 0, "", _
                           "", debitac, creditac & Trref, "", 1)
            'UpdtClosBal(stockAc, costAmt)
        End If
    End Sub
    Private Sub refreshCost()
        '22885388
        Dim dtItem As DataTable
        Dim rdCount As Integer
        Dim i As Integer
        If itemid = 0 Then
            If chkremove.Checked Then
                If MsgBox("Do you want to remove Cost?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                    _objcmnbLayer._saveDatawithOutParm("update ItmInvTrTb set CostAvg=0 from ItmInvTrTb " & _
                                                       "inner join ItmInvCmnTb on ItmInvCmnTb.trid=ItmInvTrTb.trid " & _
                                                       "where trtype in ('IS','TO','STO','PR') and brid='" & UsrBr & "' " & _
                                                       "Delete from SalesBatchTb where brid='" & UsrBr & "'")
                End If
            End If
            Dim str As String
            str = "inner join (select Itemid from (select Itemid,InQty-isnull(salesqty,0) bal from BatchTb " & _
            "left join (select sum(qty) salesqty,batchid from SalesBatchTb group by batchid)sls on BatchTb.batchid=sls.batchid " & _
            "left join (select  DocDefLoc,id from ItmInvTrTb LEFT JOIN ItmInvCmnTb ON ItmInvCmnTb.Trid=ItmInvTrTb.Trid) inv on batchtb.batchTrid =inv.id " & _
            "where case when isnull(DocDefLoc,'')='' then isnull(lcode,'') else DocDefLoc end='" & Dloc & "')tr where bal>0 group by Itemid)bth on invitm.itemid=bth.itemid"

            dtItem = _objcmnbLayer._fldDatatable("Select invitm.itemid,CostAvg,QIH,[item code] From invitm " & _
                                  "inner join (select itemid tritid from ItmInvTrTb LEFT JOIN ItmInvCmnTb ON  ItmInvTrTb.TRID=ItmInvCmnTb.TRID " & _
                                  "LEFT JOIN VoucherTypeNoTb ON ItmInvTrTb.TrTypeNo=VoucherTypeNoTb.vrno " & _
                                  " where isnull(invStatus,0)=0 and isnull(CostAvg,0)=0 AND InvType='OUT' " & _
                                  IIf(UsrBr = "", "", "and brid='" & UsrBr & "'") & " group by itemid)tr on invitm.itemid=tr.tritid " & str & _
                                  " where ItemCategory in ('stock','Menu Item')")

        Else
            If chkremove.Checked Then
                If MsgBox("Do you want to remove Cost?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                    _objcmnbLayer._saveDatawithOutParm("update ItmInvTrTb set CostAvg=0 from ItmInvTrTb " & _
                                                       "inner join ItmInvCmnTb on ItmInvCmnTb.trid=ItmInvTrTb.trid " & _
                                                       "where itemid=" & itemid & " and trtype in ('IS','TO','STO','PR') and brid='" & UsrBr & "' " & _
                                                       "Delete from SalesBatchTb where  brid='" & UsrBr & "' and Itid=" & itemid)
                End If
            End If

            dtItem = _objcmnbLayer._fldDatatable("Select invitm.itemid,CostAvg,QIH,[item code] From invitm " & _
                                  "inner join (select itemid tritid from ItmInvTrTb LEFT JOIN ItmInvCmnTb ON  ItmInvTrTb.TRID=ItmInvCmnTb.TRID " & _
                                  "LEFT JOIN VoucherTypeNoTb ON ItmInvTrTb.TrTypeNo=VoucherTypeNoTb.vrno " & _
                                  " where isnull(invStatus,0)=0 and isnull(CostAvg,0)=0 AND InvType='OUT' " & _
                                  IIf(UsrBr = "", "", "and brid='" & UsrBr & "'") & " group by itemid)tr on invitm.itemid=tr.tritid " & _
                                  " where ItemCategory in ('stock','Menu Item') and itemid=" & itemid)
            '_objcmnbLayer._saveDatawithOutParm("Delete from SalesBatchTb where Itid=" & itemid)
        End If
        rdCount = dtItem.Rows.Count - 1
        _objcmnbLayer._saveDatawithOutParm("delete from refreshTb")
        For i = 0 To rdCount
            Try
                status("Calculating CostAsOn  " & Format(CDate(Now.Date), DtFormat) & "||" & dtItem(i)("item code"), "", i, rdCount)
                'If dtItem(i)("itemid") = 47262 Then
                '    GoTo nxt
                'End If
                _objInv.ItemId = dtItem(i)("Itemid")
                _objInv.TrDate = DateValue(DateFrom)
                _objInv.setcostAverageOnModification(UsrBr)
            Catch ex As Exception
                Dim fl As System.IO.StreamWriter
                fl = My.Computer.FileSystem.OpenTextFileWriter(Application.StartupPath & "\refreshErrorLog.txt", False)
                fl.WriteLine("Itemid,branch,Rate,MRP,PDate,Edate,Nos")
                Dim query As String = dtItem(i)("Itemid") & "," & UsrBr
                fl.WriteLine(query)
                fl.Close()
            End Try

nxt:
        Next
        'Exit Sub
        rdCount = 0
        i = 0
        If Not enableCostAccounting Or itemid > 0 Then Exit Sub
        status("Updating Accounts", "", 0, 0)
        'Dim trid As Long
        'trid = 1332397
        'SALES INVOICE
        Dim dtISs As DataTable
        Dim stockAc As Long
        Dim costOfSalesAc As Long
        Dim costDiffAc As Long
        stockAc = getConstantAccounts(1)
        costOfSalesAc = getConstantAccounts(9)
        costDiffAc = getConstantAccounts(10)
        'costType=18 [MATERIAL CONSUMPTION], 'costType=9 [COST OF SALES],'costType=10 [COST DIFFERENCE]
        dtAccTb.Rows.Clear()
        Dim strCost As String = "left join (select sum(CostAvg*TrQty) costAmt,trid from ItmInvTrTb group by trid)ItmInvTrTb on ItmInvTrTb.trid=ItmInvCmnTb.trid "
        Dim strAccounts As String = " left join AccTrCmn on ItmInvCmnTb.trid=AccTrCmn.lnkno"
        dtISs = _objcmnbLayer._fldDatatable("select ItmInvCmnTb.trid,ItmInvCmnTb.Prefix,invno,[job code] jbcode,TRtype,isnull(costAmt,0)costAmt,isnull(linkno,0)linkno from " & _
                                            "ItmInvCmnTb " & strCost & strAccounts & _
                                            " left join (select trid from refreshTb group by trid)refreshTb on itminvcmntb.trid=refreshTb.trid WHERE TRtype ='IS'" & IIf(UsrBr = "", "", " AND Brid='" & UsrBr & "'"))
        rdCount = dtISs.Rows.Count - 1
        Dim trref As String = ""
        For i = 0 To rdCount
            status("Updating Accounts", "Stock Valuation on Sales Invoice \ " & dtISs(i)("Prefix") & IIf(dtISs(i)("Prefix") = "", "", "\") & dtISs(i)("invno"), i, rdCount)
            trref = dtISs(i)("Prefix") & IIf(dtISs(i)("Prefix") = "", "", "\") & dtISs(i)("invno")
            updateStockTransactionOnLive(dtISs(i)("trid"), 9, dtISs(i)("jbcode"), trref, Val(dtISs(i)("linkno")), CDbl(dtISs(i)("costAmt")), stockAc, costOfSalesAc)
        Next
        'SALES RETURN
        status("Updating Accounts", "", 0, 0)
        dtISs = Nothing
        dtISs = _objcmnbLayer._fldDatatable("select ItmInvCmnTb.trid,ItmInvCmnTb.Prefix,invno,[job code] jbcode,TRtype,isnull(costAmt,0)costAmt,isnull(linkno,0)linkno from " & _
                                            "ItmInvCmnTb " & strCost & strAccounts & _
                                            " inner join (select trid from refreshTb group by trid)refreshTb on itminvcmntb.trid=refreshTb.trid WHERE isnull(costAmt,0)>0 and TRtype ='SR'" & IIf(UsrBr = "", "", " AND Brid='" & UsrBr & "'"))

        rdCount = dtISs.Rows.Count - 1
        For i = 0 To rdCount
            trref = dtISs(i)("Prefix") & IIf(dtISs(i)("Prefix") = "", "", "\") & dtISs(i)("invno")
            status("Updating Accounts", "Stock Valuation on Sales Return Invoice \ " & dtISs(i)("Prefix") & IIf(dtISs(i)("Prefix") = "", "", "\") & dtISs(i)("invno"), i, rdCount)
            updateStockTransactionOnLive(dtISs(i)("trid"), 9, dtISs(i)("jbcode"), trref, Val(dtISs(i)("linkno")), CDbl(dtISs(i)("costAmt")), costOfSalesAc, stockAc)
        Next
        'PURCHASE RETURN
        status("Updating Accounts", "", 0, 0)
        strCost = "left join (select sum((CostAvg-(UnitCost - isnull(UnitDiscount, 0)))*TrQty) costAmt,trid from ItmInvTrTb group by trid)ItmInvTrTb on ItmInvTrTb.trid=ItmInvCmnTb.trid "
        dtISs = _objcmnbLayer._fldDatatable("select ItmInvCmnTb.trid,ItmInvCmnTb.Prefix,invno,[job code] jbcode,TRtype,isnull(costAmt,0)costAmt,isnull(linkno,0)linkno from " & _
                                            "ItmInvCmnTb " & strCost & strAccounts & _
                                            " inner join (select trid from refreshTb group by trid)refreshTb on itminvcmntb.trid=refreshTb.trid WHERE isnull(costAmt,0)>0 and TRtype ='PR'" & IIf(UsrBr = "", "", " AND Brid='" & UsrBr & "'"))

        rdCount = dtISs.Rows.Count - 1
        For i = 0 To rdCount
            trref = dtISs(i)("Prefix") & IIf(dtISs(i)("Prefix") = "", "", "\") & dtISs(i)("invno")
            status("Updating Accounts", "Stock Valuation on Purchase Return Invoice \ " & dtISs(i)("Prefix") & IIf(dtISs(i)("Prefix") = "", "", "\") & dtISs(i)("invno"), i, rdCount)
            updateStockTransactionOnLive(dtISs(i)("trid"), 10, dtISs(i)("jbcode"), trref, Val(dtISs(i)("linkno")), CDbl(dtISs(i)("costAmt")), stockAc, costDiffAc)
        Next
        'TRANSACTION OUT
        status("Updating Accounts", "", 0, 0)
        dtISs = _objcmnbLayer._fldDatatable("select ItmInvCmnTb.trid,ItmInvCmnTb.Prefix,invno,[job code] jbcode,TRtype,isnull(costAmt,0)costAmt,isnull(linkno,0)linkno from " & _
                                            "ItmInvCmnTb " & strCost & strAccounts & _
                                            " inner join (select trid from refreshTb group by trid)refreshTb on itminvcmntb.trid=refreshTb.trid WHERE isnull(costAmt,0)>0 and TRtype in ('TO','STO') " & IIf(UsrBr = "", "", " AND Brid='" & UsrBr & "'"))

        rdCount = dtISs.Rows.Count - 1
        For i = 0 To rdCount
            trref = dtISs(i)("Prefix") & IIf(dtISs(i)("Prefix") = "", "", "\") & dtISs(i)("invno")
            status("Updating Accounts", "Stock Valuation on Transaction OUT Invoice \ " & dtISs(i)("Prefix") & IIf(dtISs(i)("Prefix") = "", "", "\") & dtISs(i)("invno"), i, rdCount)
            updateStockTransactionOnLive(dtISs(i)("trid"), IIf(dtISs(i)("TRtype") = "TO", 10, 18), dtISs(i)("jbcode"), trref, Val(dtISs(i)("linkno")), CDbl(dtISs(i)("costAmt")), stockAc, costDiffAc)

        Next
        rdCount = dtAccTb.Rows.Count
        If dtAccTb.Rows.Count > 0 Then
            Dim j As Integer
            Dim dtype As String
            For i = 0 To dtAccTb.Rows.Count - 1
                For j = 0 To dtAccTb.Columns.Count - 1
                    dtype = dtAccTb.Columns(j).DataType.Name
                    If Trim(dtAccTb(i)(j) & "") = "" Then
                        Select Case dtype
                            Case "String"
                                dtAccTb(i)(j) = ""
                            Case "Int64", "Int32", "Double", "Decimal", "Boolean"
                                dtAccTb(i)(j) = 0
                            Case "date", "DateTime"
                                dtAccTb(i)(j) = DateValue(Date.Now)
                        End Select
                    End If
                Next
                status("Updating Accounts (Removing null values)", "Removing null values \ ", i, rdCount)
            Next

            _objTr.saveItmAccTrTbCostingBulk(dtAccTb)
        End If
        dtAccTb.Rows.Clear()
    End Sub
    
    Private Sub updateStockTransaction(ByVal trid As Long, ByVal costType As Integer, ByVal jobcode As String)
        Dim stockAc As Long
        Dim costOfSalesAc As Long
        Dim dt As DataTable
        Dim costAmt As Double
        Dim dtTable As DataTable
        Dim LinkNo As Long
        stockAc = getConstantAccounts(1)
        Dim Trref As String = ""
        Dim entryref As String = ""
        'costType=18 [MATERIAL CONSUMPTION], 'costType=9 [COST OF SALES],'costType=10 [COST DIFFERENCE]
        costOfSalesAc = getConstantAccounts(IIf(costType = 18, 9, costType))
        If trid > 0 Then
            dtTable = _objcmnbLayer._fldDatatable("SELECT AccTrCmn.LinkNo,reference FROM AccTrCmn LEFT JOIN AccTrDet ON AccTrDet.Linkno=AccTrCmn.linkno WHERE LnkNo = " & trid)
            If dtTable.Rows.Count > 0 Then
                LinkNo = dtTable(0)("LinkNo")
                Trref = Trim(dtTable(0)("reference") & "")
                _objcmnbLayer._saveDatawithOutParm("DELETE FROM AccTrDet WHERE trinf=3 and LinkNo=" & LinkNo)
            End If
        Else
            Exit Sub
        End If
        If LinkNo = 0 Then Exit Sub
        If costType = 10 Then
            dt = _objcmnbLayer._fldDatatable("select sum((CostAvg-(UnitCost - isnull(UnitDiscount, 0)))*TrQty) costAmt from ItmInvTrTb  where trid=" & trid)
            If dt.Rows.Count > 0 Then
                costAmt = dt(0)("costAmt")
            End If
        Else
            dt = _objcmnbLayer._fldDatatable("select sum(CostAvg*TrQty) costAmt from ItmInvTrTb  where trid=" & trid)
            If dt.Rows.Count > 0 Then
                costAmt = Val(dt(0)("costAmt") & "")
            End If
        End If
        
        Select Case costType
            Case 9
                entryref = "COST OF SALES : Refernce#" & Trref
            Case 10
                entryref = "COST DIFFERENCE : Refernce#" & Trref
            Case 18
                entryref = "SERVICE STOCK OUT : JOB#" & jobcode

        End Select
        If stockAc = 0 Or costOfSalesAc = 0 Then Exit Sub
        If costAmt <> 0 Then
            'debit entry [cost of sales]/cost diff/material consumption
            setAcctrDetValue(LinkNo, costOfSalesAc, Trref, entryref, costAmt, "", "", 3, 0, "", _
                           "", stockAc, costOfSalesAc & Trref, "", 1)
            _objTr.saveAccTrans()
            UpdtClosBal(costOfSalesAc, costAmt)
            'credit entry [stock in hand]
            costAmt = costAmt * -1
            setAcctrDetValue(LinkNo, stockAc, Trref, entryref, costAmt, "", "", 3, 0, "", _
                           "", costOfSalesAc, stockAc & Trref, "", 1)
            _objTr.saveAccTrans()
            UpdtClosBal(stockAc, costAmt)
        End If
    End Sub
    
    Private Sub setAcctrDetValue(ByVal lnkNo As Long, ByVal AccountNo As Long, ByVal Reference As String, ByVal EntryRef As String, ByVal DealAmt As Double, ByVal JobCode As String, ByVal JobStr As String, _
                                  ByVal TrInf As Short, ByVal OthCost As Short, ByVal LPO As String, ByVal TermsId As String, ByVal CustAcc As Integer, ByVal AccWithRef As String, _
                                  ByVal CurrencyCode As String, ByVal CurrRate As Double)
        With _objTr
            .trLinkNo = lnkNo
            .AccountNo = AccountNo
            .Reference = Reference
            .EntryRef = EntryRef
            .DealAmt = DealAmt
            .JobCode = JobCode
            .JobStr = JobStr
            .CurrRate = CurrRate
            .CurrencyCode = CurrencyCode ' IIf(chkFC.Checked = True, txtFC.Text, "")
            .TrInf = TrInf
            .OthCost = OthCost
            .TermsId = TermsId
            .CustAcc = CustAcc
            .AccWithRef = AccWithRef
            .LPONo = LPO
            Dim dtLPO As Date = Date.Now
            .DocDate = dtLPO
            .SuppInvDate = dtLPO
            .DueDate = dtLPO
        End With
    End Sub
    Private Sub setAcctrDetValueBulk(ByVal lnkNo As Long, ByVal AccountNo As Long, ByVal Reference As String, ByVal EntryRef As String, ByVal DealAmt As Double, ByVal JobCode As String, ByVal JobStr As String, _
                              ByVal TrInf As Short, ByVal OthCost As Short, ByVal LPO As String, ByVal TermsId As String, ByVal CustAcc As Integer, ByVal AccWithRef As String, _
                              ByVal CurrencyCode As String, ByVal CurrRate As Double)

        Dim dtrow As DataRow
        Dim dtLPO As Date = Date.Now
        dtrow = dtAccTb.NewRow
        dtrow("LinkNo") = lnkNo
        dtrow("AccountNo") = AccountNo
        dtrow("Reference") = Reference ' Trim(txtprefix.Text) & IIf(txtprefix.Text = "", "", "/") & numVchrNo.Text
        'dtrow("DueDate") = Format(DateValue(cldrdate.Value), "yyyy/MM/dd")
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
        dtrow("UnqNo") = 0
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
    Private Sub btnTransactionItems_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTransactionItems.Click
        btnTransactionItems.Enabled = False
        Worker.RunWorkerAsync()
        Worker.WorkerSupportsCancellation = True
    End Sub

    Private Sub Worker_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Worker.RunWorkerCompleted
        status("Calculating CostAsOn  " & Format(CDate(Now.Date), DtFormat) & "|| Completed", "", 0, 0)
        MsgBox("Completed", MsgBoxStyle.Information)
        Me.Close()
    End Sub

    Private Sub btnexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexit.Click
        Me.Close()
    End Sub
End Class