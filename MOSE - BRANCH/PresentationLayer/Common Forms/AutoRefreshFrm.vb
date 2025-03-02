Public Class AutoRefreshFrm
    Delegate Sub GenericDelegate(ByVal Mname As String, ByVal RecName As String, ByVal rec As Integer, ByVal count As Integer)
    Private _objcmnbLayer As New Dlayer
    Private dtAccTbTemp As DataTable
    Private _objTr As New clsAccountTransaction
    Public processtype As Integer
    Public tridFromExternal As Long
    Private Sub AutoRefreshFrm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If Worker.IsBusy Then Worker.CancelAsync()
    End Sub
    Private Sub AutoRefreshFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Left = fMainForm.Left + fMainForm.Width - Me.Width - 10
        Me.Top = fMainForm.Top + 45
        Timer1.Enabled = True
        Me.TopMost = True
    End Sub

    Private Sub Worker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles Worker.DoWork
        loadrefresh()
    End Sub
    Private Sub updateQuantityAndLastPurchaseCost(ByVal locationid As Long, ByVal trid As Long)
        Dim qry As String
        Dim dt As DataTable
        Dim trtype As String
        qry = "select id,itminvtrtb.itemid,trqty,trtype from itminvtrtb " & _
        "left join itminvcmntb on  itminvtrtb.trid=itminvcmntb.trid where itminvcmntb.trid=" & trid
        dt = _objcmnbLayer._fldDatatable(qry)
        For j = 0 To dt.Rows.Count - 1
            trtype = dt(j)("trtype")
            'QTY UPDATE************************
            qry = "update invitm set issdqty=isnull(iqty,0), rcvdqty = isnull(rqty,0), qih = uqih from invitm inner join (" & _
                "select itemid,isnull(rqty,0)rqty,isnull(iqty,0)iqty,opqty,(isnull(opqty,0) + isnull(rqty,0)) - isnull(iqty,0) uqih " & _
                "from invitm inner join (select itemid tritid, " & _
                "sum(case when invtype='in' then trqty +isnull(focqty,0) else 0 end) as rqty, " & _
                "sum(case when invtype='out' then trqty +isnull(focqty,0) else 0 end) as iqty from " & _
                "itminvtrtb left join vouchertypenotb on itminvtrtb.trtypeno=vouchertypenotb.vrno " & _
                "left join itminvcmntb on  itminvtrtb.trid=itminvcmntb.trid where isnull(invstatus,0)=0 and isnull(setremove,0)=0 " & _
                "and id=" & dt(j)("id") & " group by itemid)tr on invitm.itemid=tr.tritid) q on q.itemid = invitm.itemid "
            _objcmnbLayer.savewithoutparam(qry)
            '************************************
            'LOCATION QTY UPDATE ****************
            If locationid > 0 Then
                qry = "UPDATE locopnqtytb SET locQIH=isnull(trqty,0) from locopnqtytb inner join (select sum(trqty)trqty,itemid,LocationID from (" & _
                        "select case when invtype='IN' then 1 else -1 end *qty trqty,itemid,LocationID FROM (" & _
                        "select 'in' invtype,isnull(locopnqtytb.qty,0)qty,itemid,LocationID from  locopnqtytb " & _
                        "where itemid= " & Val(dt(j)("itemid") & "") & " and LocationID=" & locationid & _
                        "union all " & _
                        "select invtype,trqty+isnull(focqty,0) trqty,itemid,LocationID from itminvtrtb " & _
                        "left join vouchertypenotb on itminvtrtb.trtypeno=vouchertypenotb.vrno  " & _
                        "left join itminvcmntb on  itminvtrtb.trid=itminvcmntb.trid " & _
                        "left join locationtb on itminvcmntb.DocDefLoc=locationtb.loccode where isnull(invstatus,0)=0 and isnull(setremove,0)=0)tr )tr " & _
                        "where itemid= " & Val(dt(j)("itemid") & "") & " and LocationID=" & locationid & _
                        "group by itemid,LocationID)tr on locopnqtytb.itemid=tr.itemid and locopnqtytb.LocationID=tr.LocationID"
                _objcmnbLayer.savewithoutparam(qry)
            End If
            If trtype = "IP" Or trtype = "TI" Then
                'LAST PURCHASE COST *****************
                qry = "update invitm set lastpurchcost=lastcost FROM InvItm inner join  (" & _
                      "select  round(isnull(unitcost,0)+isnull(unitothcost,0)-(isnull(itemdiscount,0)/trqty),2)lastcost,itminvtrtb.ItemId from itminvtrtb inner join (" & _
                      "select max(trdateno) maxtrdateno,itemid from itminvtrtb " & _
                      "where ItemId=" & Val(dt(j)("itemid") & "") & " group by itemid)tr on ItmInvTrTb.ItemId=tr.ItemId and ItmInvTrTb.TrDateNo=tr.maxtrdateno " & _
                      "left join ItmInvCmnTb on ItmInvCmnTb.TrId=ItmInvTrTb.TrId " & _
                      "where itminvtrtb.ItemId=" & Val(dt(j)("itemid") & "") & " and TrType in ('IP','TI') and isnull(invstatus,0)=0  and isnull( setremove,0)=0)TR " & _
                      "ON InvItm.ItemId=tr.ItemId "
                _objcmnbLayer.savewithoutparam(qry)
                If locationid > 0 Then
                    'LOCATION LAST PURCHASE COST *****************
                    qry = "update locopnqtytb set lastcost=trlastcost FROM locopnqtytb inner join  (" & _
                          "select LocationID, round(isnull(unitcost,0)+isnull(unitothcost,0)-(isnull(itemdiscount,0)/trqty),2)trlastcost,itminvtrtb.ItemId from itminvtrtb inner join (" & _
                          "select max(trdateno) maxtrdateno,itemid from itminvtrtb " & _
                          "left join ItmInvCmnTb on ItmInvCmnTb.TrId=ItmInvTrTb.TrId " & _
                          "left join locationtb on itminvcmntb.DocDefLoc=locationtb.loccode " & _
                          "where ItemId=" & Val(dt(j)("itemid") & "") & " and LocationID=" & locationid & _
                          " group by itemid)tr on ItmInvTrTb.ItemId=tr.ItemId and ItmInvTrTb.TrDateNo=tr.maxtrdateno " & _
                          "left join ItmInvCmnTb on ItmInvCmnTb.TrId=ItmInvTrTb.TrId " & _
                          "left join locationtb on itminvcmntb.DocDefLoc=locationtb.loccode " & _
                          "where itminvtrtb.ItemId=" & Val(dt(j)("itemid") & "") & " and LocationID=" & locationid & _
                          " and TrType in ('IP','TI') and isnull(invstatus,0)=0  and isnull( setremove,0)=0)TR " & _
                          "ON locopnqtytb.ItemId=tr.ItemId AND locopnqtytb.LocationID=TR.LocationID"
                    _objcmnbLayer.savewithoutparam(qry)
                End If
            End If
        Next
    End Sub
    Private Sub updateCost(ByVal trid As Long)
        Dim qry As String
        Dim dtout As DataTable
        Dim dtbatch As DataTable
        Dim dtitm As DataTable
        Dim qryitem As String = "select itemid,invtype from itminvtrtb left join itminvcmntb on itminvcmntb.trid=itminvtrtb.trid  " & _
                                 "left join vouchertypenotb on itminvtrtb.trtypeno=vouchertypenotb.vrno  " & _
                                "where itminvtrtb.trid=" & trid
        dtitm = _objcmnbLayer._fldDatatable(qryitem)
        Dim itm As Integer
        For itm = 0 To dtitm.Rows.Count - 1
            If dtitm(itm)("invtype") = "IN" Then
                qry = "select id,itminvtrtb.itemid,trqty,docdefloc,trdate,brid,MchName,[item code],Description itemname from itminvtrtb " & _
                           "left join itminvcmntb on itminvcmntb.trid=itminvtrtb.trid " & _
                           "left join invitm on itminvtrtb.itemid=invitm.itemid " & _
                           "left join (select strid,sum(qty)salesbatchqty from salesbatchtb group by strid)salesbatchtb on salesbatchtb.strid=itminvtrtb.id " & _
                           "left join vouchertypenotb on itminvtrtb.trtypeno=vouchertypenotb.vrno  " & _
                           "where " & IIf(UsrBr = "", "", " isnull(brid,'')='" & UsrBr & "' and") & " isnull(invstatus,0)=0 and invtype='out' " & _
                           "and (isnull(strid,0)=0 or isnull(itminvtrtb.costavg,0)=0 or trqty<>isnull(salesbatchqty,0)) " & _
                           "and itminvtrtb.itemid =" & Val(dtitm(itm)("itemid"))
            Else
                qry = "select id,itminvtrtb.itemid,trqty,docdefloc,trdate,brid,MchName,[item code],Description itemname from itminvtrtb " & _
                           "left join itminvcmntb on itminvcmntb.trid=itminvtrtb.trid " & _
                           "left join invitm on itminvtrtb.itemid=invitm.itemid " & _
                           "left join (select strid,sum(qty)salesbatchqty from salesbatchtb group by strid)salesbatchtb on salesbatchtb.strid=itminvtrtb.id " & _
                           "left join vouchertypenotb on itminvtrtb.trtypeno=vouchertypenotb.vrno  " & _
                           "where " & IIf(UsrBr = "", "", " isnull(brid,'')='" & UsrBr & "' and") & " isnull(invstatus,0)=0 and invtype='out' " & _
                           "and (isnull(strid,0)=0 or isnull(itminvtrtb.costavg,0)=0 or trqty<>isnull(salesbatchqty,0)) " & _
                           "and itminvtrtb.itemid =" & Val(dtitm(itm)("itemid")) & " AND itminvtrtb.TRID=" & trid
            End If

            'qry = "select id,itminvtrtb.itemid,trqty,docdefloc,trdate,brid,MchName from itminvtrtb " & _
            '               "left join itminvcmntb on itminvcmntb.trid=itminvtrtb.trid " & _
            '               "left join (select strid,sum(qty)salesbatchqty from salesbatchtb group by strid)salesbatchtb on salesbatchtb.strid=itminvtrtb.id " & _
            '               "left join vouchertypenotb on itminvtrtb.trtypeno=vouchertypenotb.vrno  " & _
            '               "where " & IIf(UsrBr = "", "", " isnull(brid,'')='" & UsrBr & "' and") & " isnull(invstatus,0)=0 and invtype='out' " & _
            '               "and (isnull(strid,0)=0 or isnull(costavg,0)=0 or trqty<>isnull(salesbatchqty,0)) " & _
            '               "and itminvtrtb.itemid in (select itemid from itminvtrtb where trid=" & trid & ")"
            dtout = _objcmnbLayer._fldDatatable(qry)
            Dim i As Integer
            Dim j As Integer
            Dim itemid As Long
            Dim docdefloc As String
            Dim batchid As Long
            '***********************
            Dim trqty As Double
            Dim trdate As Date
            Dim brid As String
            Dim batchqty As Double
            Dim id As Long
            For i = 0 To dtout.Rows.Count - 1
                docdefloc = Trim(dtout(i)("docdefloc") & "")
                itemid = Val(dtout(i)("itemid") & "")
                trqty = CDbl(dtout(i)("trqty"))
                id = Val(dtout(i)("id") & "")
                brid = Trim(dtout(i)("brid") & "")
                trdate = dtout(i)("trdate")
                qry = "delete from SalesBatchTb where sTrid=" & id & " select * from (select batchtb.batchid,inqty,isnull(salesqty,0) salesqty,inqty-isnull(salesqty,0) bal,itemid,batchtrid,batchdate,TrType,docdefloc from batchtb " & _
                "left join (select sum(qty) salesqty,batchid from salesbatchtb group by batchid)sls on batchtb.batchid=sls.batchid " & _
                "inner join (select  docdefloc,id,TrType from itminvtrtb left join itminvcmntb on itminvcmntb.trid=itminvtrtb.trid) inv on batchtb.batchtrid =inv.id " & _
                "where itemid=" & itemid & IIf(docdefloc = "", "", " and isnull(docdefloc,'')='" & docdefloc & "'") & ")tr where bal>0 order by batchtrid " & _
                " UPDATE itminvtrtb set costavg=0 where id=" & id
                dtbatch = _objcmnbLayer._fldDatatable(qry)

                For j = 0 To dtbatch.Rows.Count - 1
                    batchqty = CDbl(dtbatch(j)("bal"))
                    batchid = Val(dtbatch(j)("batchid"))
                    If batchqty = 0 Then Exit Sub
                    If batchqty >= trqty Then
                        qry = "insert into salesbatchtb (strid,qty,batchid,trid,trdate,itid,brid) values " & _
                                    "(" & id & "," & trqty & "," & batchid & "," & trid & "," & Format(DateValue(trdate), "yyyy/MM/dd") & "," & itemid & ",'" & brid & "' )"
                        _objcmnbLayer.savewithoutparam(qry)
                        Exit For
                    Else
                        qry = "insert into salesbatchtb (strid,qty,batchid,trid,trdate,itid,brid) values " & _
                                    "(" & id & "," & batchqty & "," & batchid & "," & trid & "," & Format(DateValue(trdate), "yyyy/MM/dd") & "," & itemid & ",'" & brid & "' )"
                        _objcmnbLayer.savewithoutparam(qry)
                        batchqty = 0
                        trqty = trqty - batchqty
                    End If
                Next
                qry = "UPDATE itminvtrtb set costavg=isnull(TrCost,0) FROM itminvtrtb INNER JOIN " & _
                      "(select Isnull((sum(qty*cost)/min(TrQty)),0)TrCost,strid from salesbatchtb " & _
                      "inner join ItmInvTrTb on ItmInvTrTb.id=salesbatchtb.strid " & _
                      "inner join batchtb on salesbatchtb.batchid=batchtb.batchid " & _
                      "where strid=" & id & "group by strid) tr on itminvtrtb.id=tr.strid"
                _objcmnbLayer.savewithoutparam(qry)
            Next
        Next
    End Sub
    Private Sub loadrefresh()
        If dtAccTb Is Nothing Then Exit Sub
        Dim i As Integer
        Dim j As Integer
        Dim dt As DataTable
        Dim dtout As DataTable
        Dim trid As Long
        'dtAccTbTemp = _objcmnbLayer._fldDatatable("Select top 1 LinkNo,AccountNo,DueDate,Reference,EntryRef,DealAmt,FCAmt,CurrencyCode,CurrRate, " & _
        '                                          "JobCode,PDCAcc,BankCode,LPONo,OthCost,TrInf,TermsId,CustAcc,AccWithRef,ChqDate,ChqNo,SuppInvDate," & _
        '                                          "vatid,isvatEntry,UnqNo from AccTrDet")
        dtAccTbTemp = dtAccTb.Clone
        dtAccTbTemp.Rows.Clear()
        Dim mchname As String = ""
        If processtype = 0 Then
            mchname = MACHINENAME
        Else
            mchname = "RESET"
        End If
        Dim qry As String
        If trid = 0 Then
            qry = "select TobeRefreshItemTb.trid,trtype,locationid,isnull(LocCode,'')LocCode from TobeRefreshItemTb " & _
                                 "left join itminvcmntb on TobeRefreshItemTb.trid=itminvcmntb.trid  " & _
                                 "left join locationtb on itminvcmntb.docdefloc=locationtb.loccode " & _
                                 "where isnull(mchnname,'')='' and isnull(createdby,'')='" & mchname & "' " & IIf(UsrBr = "", "", " and brid='" & UsrBr & "'")
        Else
            qry = "select TobeRefreshItemTb.trid,trtype,locationid,isnull(LocCode,'')LocCode from TobeRefreshItemTb " & _
                                 "left join itminvcmntb on TobeRefreshItemTb.trid=itminvcmntb.trid  " & _
                                 "left join locationtb on itminvcmntb.docdefloc=locationtb.loccode " & _
                                 "where TobeRefreshItemTb.trid=" & trid

        End If

        dt = _objcmnbLayer._fldDatatable(qry)


        Dim trtype As String
        Dim trref As String = ""
        Dim dtISs As DataTable
        Dim stockAc As Long
        Dim costOfSalesAc As Long
        Dim costDiffAc As Long
        Dim locationid As Long
        Dim LocCode As String
        If dt.Rows.Count > 0 Then
            fMainForm.tsrefresh.Text = "Data Found.."
            'fMainForm.tsrefresh.BackColor = Color.Blue
        Else
            fMainForm.tsrefresh.Text = "Data not found"
            Dim fl As System.IO.StreamWriter
            fl = My.Computer.FileSystem.OpenTextFileWriter(Application.StartupPath & "\log.txt", False)
            fl.WriteLine(qry)
            fl.Close()
        End If
        qry = ""
        stockAc = getConstantAccounts(1)
        costOfSalesAc = getConstantAccounts(9)
        costDiffAc = getConstantAccounts(10)

        For i = 0 To dt.Rows.Count - 1
            fMainForm.tsrefresh.Text = "Cost Refresh is running.."
            'fMainForm.tsrefresh.BackColor = Color.Yellow
            trtype = Trim(dt(i)("trtype") & "")
            locationid = Val(dt(i)("locationid") & "")
            trid = Val(dt(i)("trid") & "")
            LocCode = Trim(dt(i)("LocCode") & "")
            '_objcmnbLayer.updateQTYontr(dt(i)("trid"), MACHINENAME)
            updateQuantityAndLastPurchaseCost(locationid, trid)
            updateCost(trid)
            updateCostOnMasterTbs(trid, locationid)
            If _vInvItmtable.Rows.Count > 0 Then

                dtout = Nothing
                Dim branchstr As String = ""
                If UsrBr <> "" Then
                    branchstr = " LEFT JOIN (SELECT locQIH,itemid,lastcost,locationCost from LocOpnQtyTb " & _
                                "left join LocationTb on LocationTb.LocationID=LocOpnQtyTb.LocationID " & _
                                "where LocCode='" & Dloc & "')LocationTb on InvItm.itemid=LocationTb.itemid "
                End If
                qry = "select itminvtrtb.itemid," & IIf(UsrBr = "", "QIH", "locQIH") & " QIH from itminvtrtb " & _
                "left join invitm on invitm.itemid=itminvtrtb.itemid " & branchstr & " where trid=" & dt(i)("trid")
                dtout = _objcmnbLayer._fldDatatable(qry)
                For j = 0 To dtout.Rows.Count - 1
                    Dim dttemp As DataTable
                    Dim _qurey As EnumerableRowCollection(Of DataRow)
                    _qurey = From data In _vInvItmtable.AsEnumerable() Where data("itemid") = dtout(j)("itemid") Select data
                    If _qurey.Count > 0 Then
                        dttemp = _qurey.CopyToDataTable()
                    Else
                        dttemp = _vInvItmtable.Clone
                    End If
                    Dim rownum As Long
                    If dttemp.Rows.Count > 0 Then
                        rownum = Val(dttemp(0)("rownum") & "")
                        If rownum > 0 Then
                            _vInvItmtable(rownum - 1)("QIH") = dtout(j)("QIH")
                        End If
                    End If
                Next
            End If

            If trtype = "IS" Or trtype = "SR" Then
                dtISs = Nothing
                Dim strCost As String = "left join (select sum(CostAvg*TrQty) costAmt,trid from ItmInvTrTb group by trid)ItmInvTrTb on ItmInvTrTb.trid=ItmInvCmnTb.trid "
                Dim strAccounts As String = " left join AccTrCmn on ItmInvCmnTb.trid=AccTrCmn.lnkno"
                dtISs = _objcmnbLayer._fldDatatable("select ItmInvCmnTb.trid,ItmInvCmnTb.Prefix,invno,[job code] jbcode,TRtype,isnull(costAmt,0)costAmt,isnull(linkno,0)linkno from " & _
                                                    "ItmInvCmnTb " & strCost & strAccounts & _
                                                    " WHERE ItmInvCmnTb.trid=" & trid)
                If dtISs.Rows.Count > 0 Then
                    trref = dtISs(0)("Prefix") & IIf(dtISs(0)("Prefix") = "", "", "\") & dtISs(0)("invno")
                    If trtype = "IS" Then
                        updateStockTransactionOnLive(dtISs(0)("trid"), 9, dtISs(0)("jbcode"), trref, Val(dtISs(0)("linkno")), CDbl(dtISs(0)("costAmt")), stockAc, costOfSalesAc)
                    Else
                        updateStockTransactionOnLive(dtISs(0)("trid"), 9, dtISs(0)("jbcode"), trref, Val(dtISs(0)("linkno")), CDbl(dtISs(0)("costAmt")), costOfSalesAc, stockAc)
                    End If
                    _objTr.saveItmAccTrTbCostingBulk(dtAccTbTemp)
                End If
            ElseIf trtype = "PR" Or trtype = "TO" Or trtype = "STO" Then
                dtISs = Nothing
                Dim strCost As String = "left join (select sum(CostAvg*TrQty) costAmt,trid from ItmInvTrTb group by trid)ItmInvTrTb on ItmInvTrTb.trid=ItmInvCmnTb.trid "
                If trtype = "PR" Then
                    strCost = "left join (select sum((CostAvg-(UnitCost - isnull(UnitDiscount, 0)))*TrQty) costAmt,trid from ItmInvTrTb group by trid)ItmInvTrTb on ItmInvTrTb.trid=ItmInvCmnTb.trid "
                End If
                Dim strAccounts As String = " left join AccTrCmn on ItmInvCmnTb.trid=AccTrCmn.lnkno"
                dtISs = _objcmnbLayer._fldDatatable("select ItmInvCmnTb.trid,ItmInvCmnTb.Prefix,invno,[job code] jbcode,TRtype,isnull(costAmt,0)costAmt,isnull(linkno,0)linkno from " & _
                                                    "ItmInvCmnTb " & strCost & strAccounts & _
                                                    " WHERE ItmInvCmnTb.trid=" & trid)
                If dtISs.Rows.Count > 0 Then
                    trref = dtISs(0)("Prefix") & IIf(dtISs(0)("Prefix") = "", "", "\") & dtISs(0)("invno")
                    updateStockTransactionOnLive(dtISs(0)("trid"), IIf(trtype = "STO", 18, 10), dtISs(0)("jbcode"), trref, Val(dtISs(0)("linkno")), CDbl(dtISs(0)("costAmt")), stockAc, costOfSalesAc)
                    _objTr.saveItmAccTrTbCostingBulk(dtAccTbTemp)
                End If
            End If
            status("", "", i + 1, dt.Rows.Count - 1)
            _objcmnbLayer.savewithoutparam("delete from TobeRefreshItemTb where trid=" & dt(i)("trid"))
        Next

        status("", "", 0, 0)
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'Exit Sub
        Timer1.Enabled = False
        Worker.RunWorkerAsync()
        Worker.WorkerSupportsCancellation = True
    End Sub
    Public Sub status(ByVal Mname As String, ByVal RecName As String, ByVal rec As Integer, ByVal count As Integer)
        On Error Resume Next
        If Me.InvokeRequired Then
            Dim d As New GenericDelegate(AddressOf status)
            Me.Invoke(d, Mname, RecName, rec, count)
        Else
            If rec = 0 Then
                pb.Value = 0
            Else
                If (rec * 100) / count > 100 Then
                    pb.Value = 100
                Else
                    pb.Value = (rec * 100) / count
                End If
                If processtype = 1 Then
                    If Me.Visible = False Then Me.Visible = True
                End If
                Me.Text = "Auto Refresh processing.. (" & rec & "/" & count & ")"
            End If
        End If
    End Sub

    Private Sub Worker_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Worker.RunWorkerCompleted
        If processtype = 1 Then
            MsgBox("Update Finished ! Please Restart software", MsgBoxStyle.Information)
            Me.Close()
            Exit Sub
        End If
        Timer1.Enabled = True
        Me.Text = "Auto Refresh processing.. "
        fMainForm.tsrefresh.Text = "Cost Refresh Completed!"
        fMainForm.tsrefresh.BackColor = fMainForm.StatusStrip.BackColor
    End Sub
    Private Sub updateStockTransactionOnLive(ByVal trid As Long, ByVal costType As Integer, ByVal jobcode As String, _
                                         ByVal Trref As String, ByVal LinkNo As Long, ByVal costAmt As Double, _
                                         ByVal creditac As Long, ByVal debitac As Long)

        Dim entryref As String = ""

        _objcmnbLayer.savewithoutparam("DELETE FROM AccTrDet WHERE trinf=3 and LinkNo=" & LinkNo)
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
    Private Sub setAcctrDetValueBulk(ByVal lnkNo As Long, ByVal AccountNo As Long, ByVal Reference As String, ByVal EntryRef As String, ByVal DealAmt As Double, ByVal JobCode As String, ByVal JobStr As String, _
                          ByVal TrInf As Short, ByVal OthCost As Short, ByVal LPO As String, ByVal TermsId As String, ByVal CustAcc As Integer, ByVal AccWithRef As String, _
                          ByVal CurrencyCode As String, ByVal CurrRate As Double)

        Dim dtrow As DataRow
        Dim dtLPO As Date = Date.Now
        dtrow = dtAccTbTemp.NewRow
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
        dtAccTbTemp.Rows.Add(dtrow)
        Dim j As Integer
        Dim dtype As String
        For j = 0 To dtAccTbTemp.Columns.Count - 1
            dtype = dtAccTbTemp.Columns(j).DataType.Name
            If Trim(dtAccTbTemp(dtAccTbTemp.Rows.Count - 1)(j) & "") = "" Then
                Select Case dtype
                    Case "String"
                        dtAccTbTemp(dtAccTbTemp.Rows.Count - 1)(j) = ""
                    Case "Int64", "Int32", "Double", "Decimal", "Boolean"
                        dtAccTbTemp(dtAccTbTemp.Rows.Count - 1)(j) = 0
                End Select
            End If
nxt:
        Next
    End Sub

    Private Sub updateCostOnMasterTbs(ByVal trid As Long, ByVal locationid As Integer)
        'UPDATE COST IN INVITM*************
        Dim dtitm As DataTable
        Dim qryitem As String = "select itemid,invtype from itminvtrtb left join itminvcmntb on itminvcmntb.trid=itminvtrtb.trid  " & _
                                 "left join vouchertypenotb on itminvtrtb.trtypeno=vouchertypenotb.vrno  " & _
                                "where itminvtrtb.trid=" & trid
        dtitm = _objcmnbLayer._fldDatatable(qryitem)
        Dim itm As Integer
        For itm = 0 To dtitm.Rows.Count - 1


            Dim qry As String
            qry = "update invitm set costavg =isnull(cost,0) from invitm inner join (" & _
               "select isnull(sum((bal*cost))/sum(bal),0) cost,itemid from ( " & _
               "select inqty-outqty bal,cost,itemid from (select batchtb.batchid,inqty,cost,isnull(qty,0) outqty,batchdate,batchtrid,itemid  from batchtb " & _
               "left join (select sum(qty) qty,batchid from salesbatchtb group by batchid) salesbatchtb on salesbatchtb.batchid=batchtb.batchid " & _
               "where itemid =" & dtitm(itm)("itemid") & ") bth where inqty-outqty>0 )av group by itemid)tr " & _
               "on tr.itemid=invitm.itemid "
            If locationid > 0 Then
                'UPDATE COST IN LOCOPNQTYTB
                qry = qry & vbCrLf & " select *," & locationid & " locid into #loccosttb from (select totalcost/case when bal=0 then 1 else bal end cost,Itemid,costavg from (" & _
                            "select sum(bal*cost) totalcost ,sum( bal) bal,Itemid  from (" & _
                            "select inqty-outqty bal,cost,itemid from (select batchtb.batchid,inqty,cost,isnull(qty,0) outqty,batchdate,batchtrid,itemid," & _
                            "isnull(brid,'')brid   from batchtb " & _
                            "inner join (select  brid,id from itminvtrtb " & _
                            "left join itminvcmntb on itminvcmntb.trid=itminvtrtb.trid where itminvcmntb.brid='" & UsrBr & "' " & _
                            "and itemid =" & dtitm(itm)("itemid") & ") inv on batchtb.batchtrid =inv.id " & _
                            "left join (select sum(qty) qty,batchid from salesbatchtb left join itminvcmntb on itminvcmntb.trid=salesbatchtb.trid " & _
                            "where itminvcmntb.brid='" & UsrBr & "' and itid =" & dtitm(itm)("itemid") & _
                            " group by batchid) salesbatchtb on salesbatchtb.batchid=batchtb.batchid)tr)tr group by Itemid)tr " & _
                            "left join (select costavg,itemid itid from invitm)invitm on tr.itemid=invitm.itid)tr "
                qry = qry & "merge into locopnqtytb t1 " & _
                           "using #loccosttb t2 " & _
                           "on t1.itemid=t2.itemid and isnull(t1.locationid,0)=isnull(t2.locid,0) " & _
                           "when matched then " & _
                           "update set t1.locationcost=case when isnull(t2.cost,0)>0 then isnull(t2.cost,0) else isnull(t2.costavg,0) end " & _
                           "when not matched then " & _
                          "insert (itemid,locationid,qty,lastcost,locqih,locationcost) values " & _
                          "(t2.itemid,t2.locid,0,0,0,case when isnull(t2.cost,0)>0 then isnull(t2.cost,0) else isnull(t2.costavg,0) end);"
            End If
            _objcmnbLayer.savewithoutparam(qry)
            '********************************
        Next
    End Sub
End Class