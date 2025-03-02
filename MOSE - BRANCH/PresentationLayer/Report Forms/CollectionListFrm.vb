Public Class CollectionListFrm
    Private _objcommonlayer As New clsCommon_BL
    Private _objreport As New clsReport_BL
    Private dttable As DataTable
    Private rpttable As DataTable
    Private WithEvents fRptFormat As RptFormatfrm
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fwait As WaitMessageFrm
    Private RptType As String
    Private Sub CollectionListFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loadusersalesman()
        cmbstatus.SelectedIndex = 0
        cmbsalesman.SelectedIndex = 0
        cmbuser.SelectedIndex = 0
    End Sub

    Private Sub btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnload.Click
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
                loadCollectionList()
            Case 2
                PrepareRpt(RptType)
        End Select
        If Not fwait Is Nothing Then
            fwait.Close()
            fwait = Nothing
        End If
    End Sub
    Private Sub loadusersalesman()
        Dim ds As DataSet
        Dim Str As String
        Str = " select SManCode from SalesmanTb select UserId from UserTb"
        ds = _objcommonlayer._ldDataset(Str, False)
        Dim i As Integer
        cmbsalesman.Items.Clear()
        cmbsalesman.Items.Add("")
        If ds.Tables(0).Rows.Count > 0 Then
            For i = 0 To ds.Tables(0).Rows.Count - 1
                cmbsalesman.Items.Add(ds.Tables(0)(i)("SManCode"))
            Next
        End If
        cmbuser.Items.Clear()
        cmbuser.Items.Add("")
        If ds.Tables(1).Rows.Count > 0 Then
            For i = 0 To ds.Tables(1).Rows.Count - 1
                cmbuser.Items.Add(ds.Tables(1)(i)("UserId"))
            Next
        End If
    End Sub
    Private Function setQyery(ByVal forreport As Boolean) As String
        Dim str As String
        Dim condition As String
        If chkcollectionwise.Checked Then
            Select Case cmbstatus.SelectedIndex
                Case 1 'paid
                    condition = " isnull(cashamount,0)+isnull(onlineamount,0)>0 "
                Case 2 'not paid
                    condition = " isnull(cashamount,0)+isnull(onlineamount,0)=0 "
                Case 3 'pending
                    condition = " isnull(pending,0)>0 "
                Case Else
                    condition = ""
            End Select
        Else
            Select Case cmbstatus.SelectedIndex
                Case 1
                    condition = " isnull(RV,0)>0 "
                Case 2
                    condition = " isnull(RV,0)=0 "
                Case 3
                    condition = " isnull(instaamt,0)-isnull(RV,0)>0 "
                Case Else
                    condition = ""
            End Select
        End If
        
        If cmbuser.Text <> "" Then
            If condition <> "" Then condition = condition & " and "
            condition = condition & " isnull(Createuser,'')='" & cmbuser.Text & "' "
        End If
        If cmbsalesman.Text <> "" Then
            If condition <> "" Then condition = condition & " and "
            condition = condition & " isnull(SlsmanId,'')='" & cmbsalesman.Text & "' "
        End If
        If condition <> "" Then
            condition = " and " & condition
        End If
        If rdoall.Checked Then
            
            str = "declare @FINANCEINSTALLMENT money"
            str = str & " select @FINANCEINSTALLMENT =FINANCEINSTALLMENT from CompanyDefaultSettingsTb if isnull(@FINANCEINSTALLMENT,0)=0 begin set @FINANCEINSTALLMENT=1 end "
            str = str & "select *," & IIf(chkcollectionwise.Checked, 1, 0) & " collectionwise from ( select AccDescr,isnull(jvnum,0) [RV No],collectionno,collectiondate,instaamt,isnull(cashamount,0)cashamount,isnull(onlineamount,0)onlineamount, " & _
                         "isnull(pending,0)pending,isnull(RV,0)RV,Createuser,customerid,1 lnk,0 tp,'" & Format(DateValue(dtpdate.Value), "dd/MM/yyyy") & "' datefrom,case when isnull(jvnum,0)=0 then 1 else 0 end norv,SlsmanId from AccMast  " & _
                         "inner join (select cscode,sum(netamt/@FINANCEINSTALLMENT) instaamt from ItmInvCmnTb " & _
                         "left join (select sum(dealamt) invbal,Reference,accountno from AccTrDet inner join AccTrCmn on acctrdet.LinkNo=AccTrCmn.LinkNo " & _
                         "where isnull(approved,0)=0 and JVDate< CASE WHEN JVTYPE in('IS','MIS')  THEN '" & Format(DateAdd(DateInterval.Day, 1, DateValue(dtpdate.Value)), "yyyy/MM/dd") & "' ELSE '" & Format(DateValue(dtpdate.Value), "yyyy/MM/dd") & "' END " & _
                         "group by Reference,accountno ) invrv on ItmInvCmnTb.TrRefNo=invrv.Reference and invrv.accountno=ItmInvCmnTb.cscode " & _
                         "where isnull(ishideFromRVCollection,0)=0 and TrType in('IS','MIS') and isnull(invbal,0)>0 and trdate <='" & Format(DateValue(dtpdate.Value), "yyyy/MM/dd") & "' group by CSCode)ItmInvCmnTb on ItmInvCmnTb.CSCode=AccMast.AccId " & _
                         "left join (select * from  FinanceCollectionTb where collectiondate='" & Format(DateValue(dtpdate.Value), "yyyy/MM/dd") & "')FinanceCollectionTb  on FinanceCollectionTb.customerid=AccMast.AccId  " & _
                         "left join (select sum(dealamt*-1) RV,AccountNo,min(jvnum) jvnum from AccTrDet inner join AccTrCmn on acctrdet.LinkNo=AccTrCmn.LinkNo " & _
                         "WHERE isnull(approved,0)=0 and JVType='RV' AND JVDate='" & Format(DateValue(dtpdate.Value), "yyyy/MM/dd") & "' GROUP BY AccountNo) tr on AccMast.AccId=tr.AccountNo " & _
                         "left join s1acchd on accmast.s1accid=s1acchd.s1accid " & _
                         "left join (select sum(DealAmt) bal,AccountNo from AccTrDet left join acctrcmn on acctrcmn.linkno=acctrdet.linkno where isnull(approved,0)=0 group by AccountNo)acctr on AccMast.AccId=acctr.AccountNo " & _
                         "where GrpSetOn='customer' " & condition
            If forreport Then
                Dim pendingcolleciton As String
                If chkcollectionwise.Checked Then
                    pendingcolleciton = "isnull(instaamt,0)-(isnull(cashamount,0)+isnull(onlineamount,0))>0 and isnull(cashamount,0)+isnull(onlineamount,0)>0 "
                Else
                    pendingcolleciton = "isnull(instaamt,0)-isnull(RV,0)>0 and isnull(RV,0)>0 "
                End If
                str = str & " union all select AccDescr,isnull(jvnum,0) [RV No],collectionno,collectiondate,instaamt,isnull(cashamount,0)cashamount,isnull(onlineamount,0)onlineamount, " & _
                         "isnull(pending,0)pending,isnull(RV,0)RV,Createuser,customerid,1 lnk,1 tp,'" & Format(DateValue(dtpdate.Value), "dd/MM/yyyy") & "' datefrom,case when isnull(jvnum,0)=0 then 1 else 0 end norv,SlsmanId from AccMast  " & _
                         "inner join (select cscode,sum(netamt/@FINANCEINSTALLMENT) instaamt from ItmInvCmnTb " & _
                         "left join (select sum(dealamt) invbal,Reference,accountno from AccTrDet inner join AccTrCmn on acctrdet.LinkNo=AccTrCmn.LinkNo " & _
                         "where isnull(approved,0)=0 and JVDate< CASE WHEN JVTYPE='IS' THEN '" & Format(DateAdd(DateInterval.Day, 1, DateValue(dtpdate.Value)), "yyyy/MM/dd") & "' ELSE '" & Format(DateValue(dtpdate.Value), "yyyy/MM/dd") & "' END " & _
                         "group by Reference,accountno ) invrv on ItmInvCmnTb.TrRefNo=invrv.Reference and invrv.accountno=ItmInvCmnTb.cscode " & _
                         "where isnull(ishideFromRVCollection,0)=0 and TrType='IS' and isnull(invbal,0)>0 and trdate <='" & Format(DateValue(dtpdate.Value), "yyyy/MM/dd") & "' group by CSCode)ItmInvCmnTb on ItmInvCmnTb.CSCode=AccMast.AccId " & _
                         "left join (select * from  FinanceCollectionTb where collectiondate='" & Format(DateValue(dtpdate.Value), "yyyy/MM/dd") & "')FinanceCollectionTb  on FinanceCollectionTb.customerid=AccMast.AccId  " & _
                         "left join (select sum(dealamt*-1) RV,AccountNo,min(jvnum) jvnum from AccTrDet inner join AccTrCmn on acctrdet.LinkNo=AccTrCmn.LinkNo " & _
                         "WHERE isnull(approved,0)=0 and JVType='RV' AND JVDate='" & Format(DateValue(dtpdate.Value), "yyyy/MM/dd") & "' GROUP BY AccountNo) tr on AccMast.AccId=tr.AccountNo " & _
                         "left join s1acchd on accmast.s1accid=s1acchd.s1accid " & _
                         "left join (select sum(DealAmt) bal,AccountNo from AccTrDet left join acctrcmn on acctrcmn.linkno=acctrdet.linkno where isnull(approved,0)=0 group by AccountNo)acctr on AccMast.AccId=acctr.AccountNo " & _
                         "where GrpSetOn='customer' and " & pendingcolleciton & condition
            End If
            str = str & ")tr order by [RV No]"
        ElseIf rdoinvoice.Checked Then
            str = "declare @FINANCEINSTALLMENT money"
            str = str & " select @FINANCEINSTALLMENT =FINANCEINSTALLMENT from CompanyDefaultSettingsTb if isnull(@FINANCEINSTALLMENT,0)=0 begin set @FINANCEINSTALLMENT=1 end "
            str = str & "select AccDescr,[Inv No],[Inv Date],instaamt,Createuser,SlsmanId Salesman, customerid,trid from ( select AccDescr,TrRefNo [Inv No],trdate [Inv Date],isnull(jvnum,0) [RV No],collectionno,collectiondate,instaamt,isnull(cashamount,0)cashamount,isnull(onlineamount,0)onlineamount, " & _
                         "isnull(pending,0)pending,isnull(RV,0)RV,Createuser,accid customerid,1 lnk,0 tp,'" & Format(DateValue(dtpdate.Value), "dd/MM/yyyy") & "' datefrom,case when isnull(jvnum,0)=0 then 1 else 0 end norv,TrId,SlsmanId from AccMast  " & _
                         "inner join (select cscode,sum(netamt/@FINANCEINSTALLMENT) instaamt,TrRefNo,TrId,min(trdate)trdate from ItmInvCmnTb " & _
                         "left join (select sum(dealamt) invbal,Reference,accountno from AccTrDet inner join AccTrCmn on acctrdet.LinkNo=AccTrCmn.LinkNo " & _
                         "where isnull(approved,0)=0 and JVDate< CASE WHEN JVTYPE='IS' THEN '" & Format(DateAdd(DateInterval.Day, 1, DateValue(dtpdate.Value)), "yyyy/MM/dd") & "' ELSE '" & Format(DateValue(dtpdate.Value), "yyyy/MM/dd") & "' END " & _
                         "group by Reference,accountno ) invrv on ItmInvCmnTb.TrRefNo=invrv.Reference and invrv.accountno=ItmInvCmnTb.cscode " & _
                         "where isnull(ishideFromRVCollection,0)=0 and TrType='IS' and isnull(invbal,0)>0 and trdate <='" & Format(DateValue(dtpdate.Value), "yyyy/MM/dd") & "' group by CSCode,TrRefNo,TrId)ItmInvCmnTb on ItmInvCmnTb.CSCode=AccMast.AccId " & _
                         "left join (select * from  FinanceCollectionTb where collectiondate='" & Format(DateValue(dtpdate.Value), "yyyy/MM/dd") & "')FinanceCollectionTb  on FinanceCollectionTb.customerid=AccMast.AccId  " & _
                         "left join (select sum(dealamt*-1) RV,AccountNo,min(jvnum) jvnum from AccTrDet inner join AccTrCmn on acctrdet.LinkNo=AccTrCmn.LinkNo " & _
                         "WHERE isnull(approved,0)=0 and JVType='RV' AND JVDate='" & Format(DateValue(dtpdate.Value), "yyyy/MM/dd") & "' GROUP BY AccountNo) tr on AccMast.AccId=tr.AccountNo " & _
                         "left join s1acchd on accmast.s1accid=s1acchd.s1accid " & _
                         "left join (select sum(DealAmt) bal,AccountNo from AccTrDet left join acctrcmn on acctrdet.linkno=acctrcmn.linkno where isnull(approved,0)=0  group by AccountNo)acctr on AccMast.AccId=acctr.AccountNo " & _
                         "where GrpSetOn='customer'" & condition
            str = str & ")tr order by norv, [RV No]"
        Else
            str = "declare @FINANCEINSTALLMENT money"
            str = str & " select @FINANCEINSTALLMENT =FINANCEINSTALLMENT from CompanyDefaultSettingsTb if isnull(@FINANCEINSTALLMENT,0)=0 begin set @FINANCEINSTALLMENT=1 end "
            str = str & "select AccDescr,isnull(jvnum,0) [RV No],collectionno,collectiondate,isnull(instaamt,0)instaamt,cashamount,onlineamount,pending,RV,Createuser,customerid,1 lnk,0 tp,'" & Format(DateValue(dtpdate.Value), "dd/MM/yyyy") & "' datefrom,0 norv,SlsmanId  from FinanceCollectionTb " & _
                  "inner join AccMast on FinanceCollectionTb.customerid=AccMast.AccId " & _
                  "inner join (select cscode,sum(netamt/@FINANCEINSTALLMENT) instaamt from ItmInvCmnTb " & _
                  "left join (select sum(dealamt) invbal,Reference,accountno from AccTrDet " & _
                  "inner join AccTrCmn on acctrdet.LinkNo=AccTrCmn.LinkNo " & _
                  "where isnull(approved,0)=0 and JVDate< CASE WHEN JVTYPE='IS' THEN '" & Format(DateAdd(DateInterval.Day, 1, DateValue(dtpdate.Value)), "yyyy/MM/dd") & "' ELSE '" & Format(DateValue(dtpdate.Value), "yyyy/MM/dd") & "' END " & _
                  " group by Reference,accountno ) invrv on ItmInvCmnTb.TrRefNo=invrv.Reference and invrv.accountno=ItmInvCmnTb.cscode " & _
                  "where isnull(ishideFromRVCollection,0)=0 and TrType='IS' and isnull(invbal,0)>0 group by CSCode)ItmInvCmnTb on ItmInvCmnTb.CSCode=AccMast.AccId " & _
                  "left join (select sum(dealamt*-1) RV,AccountNo,min(jvnum) jvnum  from AccTrDet " & _
                  "inner join AccTrCmn on acctrdet.LinkNo=AccTrCmn.LinkNo WHERE isnull(approved,0)=0 and JVType='RV' " & _
                  "AND JVDate='" & Format(DateValue(dtpdate.Value), "yyyy/MM/dd") & "' GROUP BY AccountNo) tr on FinanceCollectionTb.customerid=tr.AccountNo " & _
                  "where collectiondate='" & Format(DateValue(dtpdate.Value), "yyyy/MM/dd") & "'" & IIf(chkrvnot.Checked, " and isnull(jvnum,0) =0", "") & condition & " order by AccDescr"
        End If
        Return str
    End Function
    Private Sub loadCollectionList()
        Dim str As String = setQyery(False)
        dttable = _objcommonlayer._fldDatatable(str)
        dvData.DataSource = dttable
        setGridHead()
        With dvData
            Dim i As Integer = 0
            cmbOrder.Items.Clear()
            For i = 0 To .ColumnCount - 1
                cmbOrder.Items.Add(.Columns(i).HeaderText)

            Next
            If cmbOrder.Items.Count > 1 Then cmbOrder.SelectedIndex = 0
            txtSeq.Focus()
        End With
    End Sub
    Private Sub setGridHead()
        With dvData
            SetGridProperty(dvData)
            If rdoinvoice.Checked = False Then
                .Columns("collectionno").HeaderText = "CollectionNo"
                .Columns("collectionno").Width = 90
                .Columns("collectiondate").HeaderText = "Date"
                .Columns("collectiondate").Width = 75

                .Columns("instaamt").HeaderText = "Installment"
                .Columns("instaamt").Width = 100
                .Columns("instaamt").SortMode = DataGridViewColumnSortMode.Automatic
                .Columns("instaamt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("instaamt").DefaultCellStyle.Format = "N" & NoOfDecimal

                .Columns("cashamount").HeaderText = "Cash"
                .Columns("cashamount").Width = 100
                .Columns("cashamount").SortMode = DataGridViewColumnSortMode.Automatic
                .Columns("cashamount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("cashamount").DefaultCellStyle.Format = "N" & NoOfDecimal

                .Columns("onlineamount").HeaderText = "Online"
                .Columns("onlineamount").Width = 100
                .Columns("onlineamount").SortMode = DataGridViewColumnSortMode.Automatic
                .Columns("onlineamount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("onlineamount").DefaultCellStyle.Format = "N" & NoOfDecimal

                .Columns("pending").HeaderText = "Balance"
                .Columns("pending").Width = 100
                .Columns("pending").SortMode = DataGridViewColumnSortMode.Automatic
                .Columns("pending").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("pending").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("pending").DefaultCellStyle.BackColor = Color.LightCyan

                .Columns("RV").HeaderText = "RV"
                .Columns("RV").Width = 100
                .Columns("RV").SortMode = DataGridViewColumnSortMode.Automatic
                .Columns("RV").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("RV").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("RV").DefaultCellStyle.BackColor = Color.LightGreen

                .Columns("lnk").Visible = False
                .Columns("tp").Visible = False
                .Columns("norv").Visible = False
                .Columns("datefrom").Visible = False
            End If
           
            .Columns("AccDescr").HeaderText = "Customer Name"
            .Columns("AccDescr").Width = 150
            .Columns("Createuser").HeaderText = "Created By"
            .Columns("Createuser").Width = 150
            .Columns("customerid").Visible = False
            
            If rdoinvoice.Checked Then
                .Columns("Inv Date").Width = 75
                .Columns("Inv No").Width = 85

                .Columns("instaamt").HeaderText = "Installment"
                .Columns("instaamt").Width = 100
                .Columns("instaamt").SortMode = DataGridViewColumnSortMode.Automatic
                .Columns("instaamt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("instaamt").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("trid").Visible = False
            End If
            resizeGridColumn(dvData, 0)
            If rdoinvoice.Checked = False Then
                gridTotal(dttable)
            End If

            If dvData.Rows.Count > 0 Then
                btnPreview.Enabled = True
            Else
                btnPreview.Enabled = False
            End If
            
        End With
    End Sub

    Private Sub btnexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexit.Click
        Me.Close()
    End Sub

    Private Sub btnrv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrv.Click
        If dvData.CurrentRow Is Nothing Then Exit Sub
        Dim i As Integer
        i = dvData.CurrentRow.Index
        Dim customerid As Long
        customerid = Val(dvData.Item("customerid", i).Value & "")
        Dim dt As DataTable
        If customerid > 0 Then
            dt = _objcommonlayer._fldDatatable("select acctrcmn.linkno from acctrcmn left join acctrdet on acctrcmn.linkno=acctrdet.linkno " & _
                                               "where jvdate='" & Format(DateValue(dtpdate.Value), "yyyy/MM/dd") & "' and accountno=" & customerid)
            If dt.Rows.Count > 0 Then
                'If MsgBox("RV Found in selected date!Do you want to continue? ", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                MsgBox("RV Found in selected date!" & vbCrLf & "You cannot post RV", MsgBoxStyle.Information)
                Exit Sub
            End If
        End If
        fMainForm.LoadRV(0, dvData.Item("AccDescr", i).Value)
    End Sub
    Private Sub gridTotal(ByVal dt As DataTable)
        lbltotalInvoice.Text = Format(0, numFormat)
        lblreceived.Text = Format(0, numFormat)
        lbltotalbalance.Text = Format(0, numFormat)
        lblcollection.Text = Format(0, numFormat)

        Dim amt As String
        Dim dblAmount As Double
        amt = Trim(dt.Compute("SUM(cashamount)", "") & "")
        If Val(amt) > 0 Then
            dblAmount = Convert.ToDouble(amt)
            lbltotalInvoice.Text = Format(dblAmount, numFormat)
        End If
        amt = ""
        dblAmount = 0
        amt = Trim(dt.Compute("SUM(onlineamount)", "") & "")
        If Val(amt) > 0 Then
            dblAmount = Convert.ToDouble(amt)
            lblreceived.Text = Format(dblAmount, numFormat)
        End If
        amt = ""
        dblAmount = 0
        amt = Trim(dt.Compute("SUM(pending)", "") & "")
        If Val(amt) > 0 Then
            dblAmount = Convert.ToDouble(amt)
            lbltotalbalance.Text = Format(dblAmount, numFormat)
        End If
        
        amt = ""
        dblAmount = 0
        amt = Trim(dt.Compute("SUM(RV)", "") & "")
        If Val(amt) > 0 Then
            dblAmount = Convert.ToDouble(amt)
            lblcollection.Text = Format(dblAmount, numFormat)
        End If
        lbltodaycollection.Text = Format(CDbl(lbltotalInvoice.Text) + CDbl(lblreceived.Text), numFormat)

    End Sub

    Private Sub txtSeq_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeq.TextChanged
        dvData.DataSource = SearchGrid(dttable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
        rpttable = SearchGrid(dttable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
        setGridHead()
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        'Dim RptType As String
        If rdocollection.Checked Then
            RptType = "OCL"
        ElseIf rdoall.Checked Then
            RptType = "OCLA"
            If chkcollectionwise.Checked Then
                RptType = "ACSL"
            End If
        Else
            If chkinstallment.Checked Then
                RptType = "FINS"
            Else
                RptType = ""
            End If
        End If
        If RptType = "" Then Exit Sub
        If chkFormat.Checked Then
            fRptFormat = New RptFormatfrm
            fRptFormat.RptType = RptType
            fRptFormat.ShowDialog()
            fRptFormat = Nothing
        Else
            loadWaite(2)
        End If
    End Sub
    Private Sub PrepareRpt(ByVal RptType As String, Optional ByVal Forprint As Boolean = False, Optional ByVal isF As Boolean = False)
        Dim RptName As String
        Dim RptCaption As String = ""
        RptName = getRptDefFlName(RptType, RptCaption)
        If Trim(RptName) <> "" Then
            LoadReport(RptName, RptCaption, Forprint)
        End If
    End Sub
    Private Sub LoadReport(ByVal FileName As String, ByVal RptCaption As String, Optional ByVal ToPrint As Boolean = False)
        If Not frm Is Nothing Then
            frm.Close()
            frm = New ReprtviewNEWfrm
        Else
            frm = New ReprtviewNEWfrm
        End If
        Dim ds As New DataSet
        If chkinstallment.Checked And rdoinvoice.Checked Then
            ds = loadinstallmenthistory()
            'ds.Tables.Add(dt)
        Else
            If rpttable Is Nothing Then
                Dim str As String = setQyery(True)
                ds = _objcommonlayer._ldDataset(str, False)
            Else
rpt:
                ds.Tables.Add(rpttable)
            End If
        End If
        
        frm.SetReport(ds, FileName, 0, False, , ToPrint)
        frm.MdiParent = Me.MdiParent
        frm.Text = RptCaption
        If Not ToPrint Then frm.Show()
    End Sub
    Private Function loadinstallmenthistory() As DataSet
        If dvData.Rows.Count = 0 Then Return Nothing
        If dvData.CurrentRow Is Nothing Then Return Nothing
        Dim ds As DataSet
        Dim str As String = ""
        Dim trdate As Date
        Dim refno As String
        Dim instaamt As Double
        Dim customerid As String
        Dim invdate As String
        Dim invamt As Double
        Dim custname As String
        Dim FINANCEINSTALLMENT As Integer
        With dvData
            trdate = DateValue(.Item("inv date", .CurrentRow.Index).Value)
            refno = .Item("inv no", .CurrentRow.Index).Value
            customerid = .Item("customerid", .CurrentRow.Index).Value
            custname = .Item("AccDescr", .CurrentRow.Index).Value
            If CDbl(.Item("instaamt", .CurrentRow.Index).Value) > 0 Then
                instaamt = CDbl(.Item("instaamt", .CurrentRow.Index).Value)
            Else
                instaamt = 0
            End If
            invdate = DateValue(.Item("inv date", .CurrentRow.Index).Value)

        End With
        str = "declare @FINANCEINSTALLMENT int"
        str = str & " select isnull(FINANCEINSTALLMENT,0)FINANCEINSTALLMENT from CompanyDefaultSettingsTb "
        str = str & " select DealAmt*-1 DealAmt,Reference,accountno,jvdate from AccTrDet   inner join AccTrCmn on acctrdet.LinkNo=AccTrCmn.LinkNo  " & _
                    "where AccountNo=" & customerid & " and Reference='" & refno & "' and DealAmt<0 "
        ds = _objcommonlayer._ldDataset(str, False)


        If ds.Tables(0).Rows.Count > 0 Then
            FINANCEINSTALLMENT = Val(ds.Tables(0)(0)("FINANCEINSTALLMENT") & "")
            invamt = instaamt * FINANCEINSTALLMENT
        End If
        Dim dtrv As DataTable
        Dim dtinst As DataTable
        dtinst = _objcommonlayer._fldDatatable(" select getdate() Instdate,convert(money,0) instamt,convert(money,0) paid,1 lnk,'' invno,'' invdate, convert(money,0) invamt,'' custname,'' mname")
        dtrv = ds.Tables(1)
        Dim i As Integer

        Dim rvamt As Double
        Dim bDatatable As DataTable
        bDatatable = Nothing
        Dim dr As DataRow
        dtinst.Rows.Clear()
        FINANCEINSTALLMENT = DateDiff(DateInterval.Day, trdate, DateValue(Date.Now))
        For i = 1 To 234
            trdate = DateAdd(DateInterval.Day, 1, trdate)
            If Not bDatatable Is Nothing Then
                If bDatatable.Rows.Count > 0 Then bDatatable.Rows.Clear()
            End If

            If dtrv.Rows.Count = 0 Then bDatatable = dtrv.Clone : GoTo nxt
            'If bDatatable.Rows.Count > 0 Then bDatatable.Rows.Clear()
            Dim _qurey As EnumerableRowCollection(Of DataRow)
            _qurey = From data In dtrv.AsEnumerable() Where data("jvdate") = trdate Select data
            If _qurey.Count > 0 Then
                bDatatable = _qurey.CopyToDataTable()
            Else
                bDatatable = dtrv.Clone
            End If
            If bDatatable.Rows.Count > 0 Then
                If Val(bDatatable(0)("DealAmt")) = 0 Then bDatatable(0)("DealAmt") = 0
                rvamt = bDatatable(0)("DealAmt")
            End If
nxt:
            Dim dayname As String
            dayname = trdate.ToString("dddd")
            dr = dtinst.NewRow
            dr("Instdate") = trdate
            dr("instamt") = instaamt
            dr("paid") = rvamt
            dr("invno") = refno
            dr("invdate") = invdate
            dr("invamt") = invamt
            dr("custname") = custname
            dr("mname") = dayname
            dr("lnk") = 1
            dtinst.Rows.Add(dr)
            'If dayname = "Sunday" Then
            '    i = i - 1
            'End If
            rvamt = 0
        Next
        Dim ds1 As New DataSet
        ds1.Tables.Add(dtinst)
        Return ds1
    End Function

    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        If Trim(RptFlName) <> "" Then
            LoadReport(RptFlName, RptCaption, forPrint)
        End If
    End Sub


    Private Sub rdocollection_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdocollection.Click, rdoall.Click, rdoinvoice.Click
        If rdocollection.Checked Then
            chkrvnot.Visible = True
        Else
            chkrvnot.Visible = False
            chkrvnot.Checked = False
        End If
        If rdoinvoice.Checked Then
            chkinstallment.Visible = True
            chkinstallment.Checked = True
        Else
            chkinstallment.Visible = False
            chkinstallment.Checked = False
        End If
        loadWaite(1)
    End Sub


    Private Sub rdoinvoice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoinvoice.Click
        chkrvnot.Checked = False
    End Sub

    Private Sub rdocollection_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdocollection.CheckedChanged

    End Sub

    Private Sub chkFormat_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFormat.CheckedChanged

    End Sub
End Class