
Public Class InvReportFrm
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fRptFormat As RptFormatfrm
    Private WithEvents fProductEnquiry As ItmEnqry
    Private WithEvents fwait As WaitMessageFrm
    Dim strsql As String
    Public ldType As String
    Public opt As Single
    Public isDoc As Boolean
    'WithEvents bsCustomers As New BindingSource
    Private cmbShowIndex As Integer
    Private chgbyprg As Boolean
    Private forSingle As Boolean
    Private dtTable As DataTable
    Dim MyCtrl As RadioButton
    Dim strSearch As String
    'object declarations
    Dim _objcmnbLayer As New clsCommon_BL
    Private itmDatatable As DataTable
    Private _objReport As New clsReport_BL
    Private lvlGrp As String
    Public cboxroute As Boolean

    Private Sub setComboGrid()
        chgbyprg = True
        Dim i As Integer = 0
        cmbSearch.Items.Clear()
        For i = 0 To grdvoucher.ColumnCount - 1
            cmbSearch.Items.Add(grdvoucher.Columns(i).HeaderText)

        Next
        If cmbSearch.Items.Count > 1 Then cmbSearch.SelectedIndex = 5
        txtSearch.Focus()
        'If cmbSearch.Items.Count >= cmbShowIndex Then cmbSearch.SelectedIndex = cmbShowIndex
        chgbyprg = False
    End Sub
    Private Function getWhereInv(Optional ByVal forGrid As Boolean = False) As String
        Dim strWhere As String = ""
        If forGrid Then GoTo forGrid
        If forSingle Then
            Dim SelectedTrID As Long
            SelectedTrID = Val(grdvoucher.Item(grdvoucher.ColumnCount - 1, grdvoucher.CurrentRow.Index).Value)
            getWhereInv = " and ItmInvCmntb.TrId= " & SelectedTrID
            Exit Function
        End If
forGrid:
        Select Case ldType
            Case "IP", "IS", "SR", "PR", "JIS", "TIS", "TI", "TO"
                strWhere = " Trdate>='" & Format(CDate(cldrStartDate.Value), "yyyy-MM-dd") & "' and  Trdate<='" & Format(CDate(cldrEnddate.Value), "yyyy-MM-dd") & "' "
            Case "PO", "MTN"
                strWhere = " Ddate>='" & Format(CDate(cldrStartDate.Value), "yyyy-MM-dd") & "' and  Ddate<='" & Format(CDate(cldrEnddate.Value), "yyyy-MM-dd") & "'"
        End Select
        If strWhere <> "" Then
            getWhereInv = " and " & strWhere
        Else
            getWhereInv = ""
        End If

        'End If
    End Function
    Public Sub fillGrid(ByVal optInv As Single)
        chgbyprg = True
        Dim vsummary As String
        Dim dtsum As DataTable
        If ldType = "IP" Then
            lblName.Text = "Purchase Invoice Report"
        ElseIf ldType = "IS" Then
            lblName.Text = "Sales Invoice Report"
        ElseIf ldType = "SR" Then
            lblName.Text = "Sales Return Report"
        ElseIf ldType = "PR" Then
            lblName.Text = "Purchase Return Report"
        ElseIf ldType = "JIS" Then
            Me.lblName.Text = "Job Sales Invoice Report"
        ElseIf ldType = "TI" Then
            Me.lblName.Text = "Stock Transfer [IN] Report"
        ElseIf ldType = "TO" Then
            lblName.Text = "Stock Transfer [Out] Report"
        ElseIf ldType = "MTN" Then
            lblName.Text = "Material Transfer Note"
        Else
            lblName.Text = "Local Purchase Order"
        End If

        txtSearch.Text = ""
        If IsDate(cldrStartDate.Value) And IsDate(cldrEnddate.Value) Then
            If CDate(cldrStartDate.Value) > CDate(cldrEnddate.Value) Then GoTo Invaliddate
        Else
Invaliddate:
            MsgBox("Invalid Date", MsgBoxStyle.Information)
            Exit Sub
        End If
        Dim shPending As String
        
        'ldType = Choose(optInv + 1, "IS", "IP", "SR", "PR", "DOC", "DOS", "SO", "PO", "QTI", "QTR", "MR", "GIN", "GRC", "TI", "TO", "LTR")
        If chkPending.Checked Then
            shPending = "INNER JOIN (SELECT DocCmnTb.DocId FROM DocCmnTb LEFT JOIN (SELECT DocId, Sum(Qty) TDQty FROM DocTranTb GROUP BY DocId) QAmt ON DocCmnTb.DocId = QAmt.DocId" & _
                        " LEFT JOIN (SELECT DocId, Sum(TrQty) As TPQty FROM DocAssgnTb GROUP BY DocId) PIQ ON PIQ.DocId = DocCmnTb.DocId WHERE DocType = '" & ldType & "'" & getWhereInv(True) & " AND TDQty - isNull(TPQty,0)> 0) PndQ ON PndQ.DocId = DocCmnTb.DocId"
        Else
            shPending = ""
        End If
        Dim tpcondition As String = ""
        Dim usercondition As String = ""
        If enableuserwisetransactionlist And userType And ldType <> "IP" Then
            usercondition = " and UserId='" & CurrentUser & "'"
        End If
        Select Case ldType
            Case "IS", "SR"
                If chkcardsale.Checked Or chkservicesale.Checked Then
                    tpcondition = ""
                    If chksale.Checked Then
                        tpcondition = "'IS'"
                    End If
                    If chkcardsale.Checked Then
                        tpcondition = tpcondition & IIf(tpcondition = "", "", ",") & "'DIS'"
                    End If
                    If chkservicesale.Checked Then
                        tpcondition = tpcondition & IIf(tpcondition = "", "", ",") & "'SIS'"
                    End If
                    tpcondition = " and ItmInvCmntb.trtype IN(" & tpcondition & ")"
                Else
                    tpcondition = " and ItmInvCmntb.trtype='" & ldType & "'"
                End If
                
                strsql = "Select case when isnull(isB2b,0)=1 then 'B2B' else 'B2C' end Invoice ,trtype [Type],[P Mode],Ctype, convert(varchar(12), case when prefix=''  then '' else '-' end +  invNo ) as [Inv No] , TrDate [Tr.Date] ,Alias [Cust. Code]," & _
                        "CASE WHEN ISNULL(CashCustName,'')='' THEN AccDescr ELSE CashCustName END [Customer Name],InvAmt [Amount],Discount,rndoff [Round Off],taxAmt [Tax Amount]," & _
                        "NetAmt [Net Total], OthCost [Sales Exp.],TrRefNo [Ref. No],SlsManId," & _
                        "TrDescription  [Tr. Description],[Job Code],LPO,UserId [Created By],convert(varchar,CrtDt,100) [Created On],Brid Branch,TrId from ( select trtype, ItmInvCmntb.prefix, invNo ,TrDate ,InvAmt,Discount,Discount1," & _
                        "OthCost,AccDescr ,TrRefNo,TrDescription,Alias ,UserId,[Job Code],ItmInvCmntb.Termsid,LPO,ItmInvCmntb.TrId,rndoff,taxAmt,CashCustName,ItmInvCmntb.isTaxInvoice,ItmInvCmntb.SlsManId,CrtDt,ItmInvCmntb.isB2B,FCRate,NetAmt,[Voucher Name][P Mode],ItmInvCmntb.brid,case when isnull(iscashinvoiceonly,0)=1 then 'Cash' else 'Credit'end Ctype  from ItmInvCmntb " & _
                        "LEFT JOIN PreFixTb ON ItmInvCmntb.InvTypeNo=PreFixTb.ID " & _
                         " LEFT JOIN (SELECT Trid,SUM((TrQty*(UnitCost+UnitOthCost))-ItemDiscount)InvAmt,sum(taxAmt+isnull(cessAmt,0)) taxAmt FROM ItmInvTrTb GROUP BY Trid) Tr ON  ItmInvCmntb.Trid=Tr.Trid" & _
                         " left join Accmast on ItmInvCmnTb.CSCode=Accmast.accid where  isnull(invStatus,0)=0   " & _
                        IIf(UsrBr = "", "", " and ItmInvCmntb.Brid='" & UsrBr & "'") & tpcondition & usercondition & getWhereInv(True) & " ) as qq  order by isnull(Brid,''), TrDate ,InvNo"
                Dim dtsr As DataTable
                dtsr = _objcmnbLayer._fldDatatable("select sum(netamt)sramt from ItmInvCmntb where trtype ='SR' " & IIf(UsrBr = "", "", " and ItmInvCmntb.Brid='" & UsrBr & "'") & usercondition & _
                                                   " AND Trdate>='" & Format(CDate(cldrStartDate.Value), "yyyy-MM-dd") & "' and  Trdate<='" & Format(CDate(cldrEnddate.Value), "yyyy-MM-dd") & "'")
                If dtsr.Rows.Count > 0 Then
                    If Val(dtsr(0)("sramt") & "") > 0 Then
                        lblreturn.Text = Format(dtsr(0)("sramt"), numFormat)
                    Else
                        lblreturn.Text = Format(0, numFormat)
                    End If
                Else
                    lblreturn.Text = Format(0, numFormat)
                End If

                cmbShowIndex = 14
            Case "IP", "PR"
                strsql = "Select  trtype [Type], convert(varchar(12), case when prefix=''  then '' else '-' end +  invNo ) as [Inv No] ,TrDate [Tr.Date] ,Alias [Sup. Code]," & _
                 "CASE WHEN ISNULL(CashCustName,'')='' THEN AccDescr ELSE CashCustName END as [Supplier Name],(InvAmt-Discount)+rndoff [Amount],taxAmt [Tax Amount],NetAmt [Net Total],OthCost [Other Exp.],TrRefNo [Ref. No],TrDescription [Tr. Description],[Job Code],LPO,UserId [Created By],TrId from " & _
                 "( select  isTaxInvoice,trtype,prefix, invNo ,TrDate ,InvAmt,Discount,Discount1,OthCost,AccDescr,TrRefNo,TrDescription,Alias ,UserId,[Job Code],ItmInvCmntb.Termsid,LPO,ItmInvCmntb.TrId,rndoff,taxAmt,CashCustName,NetAmt,Brid from ItmInvCmntb " & _
                         " LEFT JOIN (SELECT Trid,SUM((TrQty*(UnitCost+UnitOthCost))-ItemDiscount) InvAmt,sum(taxAmt+isnull(cessAmt,0)) taxAmt FROM ItmInvTrTb GROUP BY Trid) Tr ON  ItmInvCmntb.Trid=Tr.Trid" & _
                         " left join Accmast on ItmInvCmnTb.CSCode=Accmast.accid where " & IIf(UsrBr = "", "", " ItmInvCmntb.Brid='" & UsrBr & "' and") & " ItmInvCmntb.trtype='" & ldType & "'" & _
                         usercondition & getWhereInv(True) & " ) as qq  order by isnull(Brid,''),TrDate ,InvNo"
                cmbShowIndex = 14
            Case "JIS"
                strsql = "Select   trtype [Type],prefix + case when isnull(prefix,'')=''  then '' else  '/' end +  convert(varchar,invNo) as [Inv No] , TrDate [Tr.Date] ,Alias [Cust. Code],AccDescr [Customer Name],(InvAmt-Discount)+(case when isnull(isTaxInv,0)=0 then 0 else 1 end *TaxAmt) [Amount],TrRefNo [Ref. No],TrDescription  [Tr. Description],jobcode [Job Code],UserId [Created By],TrId from ( select  trtype,prefix, invNo ,TrDate ,InvAmt,Discount,AccDescr ,TrRefNo,TrDescription,Alias ,jobInvCmntb.UserId,jobcode,jobInvCmntb.TrId,TaxAmt,isTaxInv from jobInvCmntb " & _
                        "LEFT JOIN (SELECT Trid,SUM(TrQty*(UnitCost))InvAmt,sum(TaxAmt+isnull(cessAmt,0)) TaxAmt FROM jobInvTrTb GROUP BY Trid) Tr ON  jobInvCmntb.Trid=Tr.Trid left join jobtb on jobInvCmntb.jobid=jobtb.jobid  left join Accmast on jobInvCmnTb.custid=Accmast.accid where jobInvCmntb.trtype='" & ldType & "'" & getWhereInv(True) & " ) as qq  order by TrDate ,InvNo"
            Case "TI", "TO"
                strsql = "Select   prefix + case when isnull(prefix,'')=''  then '' else  '/' end +  convert(varchar,invNo) as [Inv No] ,TrDate [Tr.Date] ,Alias [ACC. Code],AccDescr as [Account Name],NetAmt [Net Total],OthCost [Other Exp.],TrRefNo [Ref. No],TrDescription [Tr. Description],[Job Code],LPO,UserId [Created By],TrId from" & _
                " ( select  isTaxInvoice,prefix, invNo ,TrDate ,InvAmt,Discount,Discount1,OthCost,AccDescr,TrRefNo,TrDescription,Alias ,UserId,[Job Code],ItmInvCmntb.Termsid,LPO,ItmInvCmntb.TrId,rndoff,isnull(TaxAmt,0)TaxAmt,NetAmt from ItmInvCmntb " & _
                        " LEFT JOIN (SELECT Trid,SUM(TrQty*(UnitCost+UnitOthCost))InvAmt,sum(TaxAmt) TaxAmt FROM ItmInvTrTb GROUP BY Trid) Tr ON  ItmInvCmntb.Trid=Tr.Trid left join Accmast on ItmInvCmnTb.CSCode=Accmast.accid where " & IIf(UsrBr = "", "", " ItmInvCmntb.Brid='" & UsrBr & "' and") & " ItmInvCmntb.trtype='" & ldType & "'" & getWhereInv(True) & " ) as qq  order by TrDate ,InvNo"
            Case "MTN"
                strsql = "Select  DNo [Doc No] , Ddate [Doc.Date] ," & _
                        "AccDescr [Customer Name],Reference [Ref. No],Comment  [Tr. Description]," & _
                        "isnull(fcode,FromLoc) + ' / ' +  isnull(fjname,'') [Tr. From]," & _
                        "case when isnull(JobCode,'')='' then DocDefLoc else JobCode end +' / ' + isnull(JobName,'') Job,UserId [Created By],Docid " & _
                       " from ( select  DNo ,DDate ,InvAmt,Discount,AccDescr ,Reference,Comment,Alias ,DocCmnTb.UserId,DocCmnTb.jobcode,DocCmnTb.DocId,jobname,fjname,DocDefLoc,fcode,FromLoc from DocCmnTb" & _
                       " LEFT JOIN JobTb ON JobTb.jobcode=DocCmnTb.jobcode " & _
                       " LEFT JOIN (select jobcode fcode,jobname fjname from JobTb)fjb ON fjb.fcode=DocCmnTb.FromJob" & _
                         " LEFT JOIN (SELECT DocId,SUM((Qty/PFraction)*CostPUnit)InvAmt FROM DocTranTb GROUP BY DocId) Tr ON  DocCmnTb.Docid=Tr.Docid " & shPending & _
                         " left join Accmast on DocCmnTb.CSCode=Accmast.accid where " & IIf(UsrBr = "", "", " ItmInvCmntb.Brid='" & UsrBr & "' and") & " DocCmnTb.DocType='" & ldType & "'" & getWhereInv(True) & " ) as qq  order by Ddate ,Dno"
            Case "PO"
                strsql = "Select  DNo [Doc No] , Ddate [Doc.Date] ,AccDescr [Supplier Name],(InvAmt-Discount)+rndoff [Amount],Reference [Ref. No],Comment  [Tr. Description],[Job Code],UserId [Created By],Docid " & _
                        " from ( select  DNo ,DDate ,InvAmt,Discount,Discount1,AccDescr ,Reference,Comment,Alias ,UserId,[Job Code],DocCmnTb.Termsid,DocCmnTb.DocId,rndoff,lpoclass from DocCmnTb" & _
                          " LEFT JOIN (SELECT DocId,SUM((Qty/PFraction)*CostPUnit)InvAmt FROM DocTranTb GROUP BY DocId) Tr ON  DocCmnTb.Docid=Tr.Docid " & shPending & _
                          " left join Accmast on DocCmnTb.CSCode=Accmast.accid where " & IIf(UsrBr = "", "", " ItmInvCmntb.Brid='" & UsrBr & "' and") & " DocCmnTb.DocType='" & ldType & "'" & getWhereInv(True) & " ) as qq  order by Ddate ,Dno"
            Case "TIS"
                strsql = "Select Prefix, InvNo [Inv No] , Trdate [Tr.Date] ,CashCustName [Party Name],Starname [Star Name],InvAmt+rndoff [Amount],Reference [Ref. No],TrDescription  [Tr. Description],UserId [Created By],TempleSalesCmnTb.Trid " & _
                        " from TempleSalesCmnTb " & _
                          " LEFT JOIN (SELECT Trid,SUM(Qty*Rate)InvAmt FROM TempleSalesDetTb GROUP BY trid) Tr ON  TempleSalesCmnTb.Trid=Tr.trid " & _
                          " left join StarTb on TempleSalesCmnTb.starid=StarTb.starid where 1=1 " & getWhereInv(True) & " order by Trdate ,Invno"

        End Select

        'vsummary = "select * from (select [Voucher Name][P Mode],sum(netamt)Amount from ItmInvCmntb LEFT JOIN PreFixTb ON ItmInvCmntb.InvTypeNo=PreFixTb.ID  where " & IIf(UsrBr = "", "", " ItmInvCmntb.Brid='" & UsrBr & "' and") & " trtype ='" & ldType & "' and " & _
        '"Trdate>='" & Format(CDate(cldrStartDate.Value), "yyyy-MM-dd") & "' and  Trdate<='" & Format(CDate(cldrEnddate.Value), "yyyy-MM-dd") & "'" & _
        '" GROUP BY  [Voucher Name])tr where isnull(Amount,0)>0"
        vsummary = "select [P Mode], sum(DealAmt)Amount from (select case when GrpSetOn in ('customer','supplier') then  'CREDIT' else AccDescr end [P Mode],dealamt+isnull(rvamt,0) dealamt from ( select AccountNo,sum(dealamt)dealamt from ( select DealAmt,JVNum,AccTrDet.AccountNo from AccTrCmn left join AccTrDet on AccTrDet.linkno=AccTrCmn.linkno" & _
        " where " & IIf(UsrBr = "", "", " AccTrCmn.CmnBrId='" & UsrBr & "' and") & " jvtype ='" & ldType & "'  and JVDate>='" & Format(CDate(cldrStartDate.Value), "yyyy-MM-dd") & "'  and  JVDate<='" & Format(CDate(cldrEnddate.Value), "yyyy-MM-dd") & "' and isnull(DealAmt,0)>0 and isnull(trinf,0) in (0,10) " & usercondition & " )tr group by AccountNo)tr " & _
        " left join (select sum(dealamt)rvamt,AccountNo from AccTrCmn left join AccTrDet on AccTrDet.linkno=AccTrCmn.linkno where " & IIf(UsrBr = "", "", " AccTrCmn.CmnBrId='" & UsrBr & "' and") & " jvtype ='" & ldType & "' and JVDate>='" & Format(CDate(cldrStartDate.Value), "yyyy-MM-dd") & "' and  JVDate<='" & Format(CDate(cldrEnddate.Value), "yyyy-MM-dd") & "' " & _
        " and isnull(DealAmt,0)<0 and isnull(trinf,0)=10 group by AccountNo)rv on tr.AccountNo=rv.AccountNo   " & _
        "LEFT JOIN AccMast ON tr.accountno=AccMast.accid  left join S1AccHd on AccMast.S1AccId=S1AccHd.S1AccId) tr where isnull(DealAmt,0)>0 group by [P Mode]union all select 'DISCOUNT',  sum(discount)discount from ItmInvCmnTb where   " & _
        " trdate>='" & Format(CDate(cldrStartDate.Value), "yyyy-MM-dd") & "' and  trdate<='" & Format(CDate(cldrEnddate.Value), "yyyy-MM-dd") & "' "

        dtsum = _objcmnbLayer._fldDatatable(vsummary)
        grdsum.DataSource = dtsum

        SetGridProperty(grdsum)
        With grdsum
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(1).DefaultCellStyle.Format = "N" & NoOfDecimal
        End With
        grdsum.Rows(grdsum.RowCount - 1).DefaultCellStyle.BackColor = Color.Cyan
        resizeGridColumn(grdsum, 0)
        grdvoucher.DataSource = Nothing
        If strsql = "" Then
            cmbSearch.Items.Clear()
            GoTo SetHeadOnly
        End If
        dtTable = _objcmnbLayer._fldDatatable(strsql)
        grdvoucher.DataSource = dtTable
        setComboGrid()

        gridTotal(dtTable)
SetHeadOnly:
        SetGridHead()
        chgbyprg = False
    End Sub
    Private Sub gridTotal(ByVal dt As DataTable)
        If dt.Rows.Count = 0 Then Exit Sub
        Dim drSum As Double
        Dim amt As String
        Select Case ldType
            Case "IS", "SR", "IP", "PR", "TI", "TO"
                amt = Trim(dt.Compute("SUM([Net Total])", "") & "")
            Case "MTN"
                lbltotal.Visible = False
                Label3.Visible = False
                amt = 0
            Case Else
                amt = Trim(dt.Compute("SUM([Amount])", "") & "")
        End Select
        If Val(amt) > 0 Then
            drSum = Convert.ToDouble(amt)
        End If
        lbltotal.Text = Format(drSum, numFormat)
        If ldType = "IS" Then
            If Val(lbltotal.Text) = 0 Then lbltotal.Text = Format(0, numFormat)
            If Val(lblreturn.Text) = 0 Then lblreturn.Text = Format(0, numFormat)
            lblnetsales.Text = Format(CDbl(lbltotal.Text) - CDbl(lblreturn.Text), numFormat)
        End If
    End Sub
    Private Sub SetGridHead()
        For i = 0 To cmbSearch.Items.Count - 1
            With grdvoucher
                grdvoucher.Columns(i).ReadOnly = True
                .Columns(i).HeaderText = cmbSearch.Items(i).ToString
            End With
            grdvoucher.Columns(0).ReadOnly = False
        Next

        With grdvoucher
            SetGridProperty(grdvoucher)
            If ldType <> "MTN" And ldType <> "TIS" And ldType <> "IS" And ldType <> "SR" Then
                .Columns(0).Width = 50
                .Columns(.ColumnCount - 1).Visible = False
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Columns(1).Width = 75
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Columns(5).DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns(5).Frozen = True
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(6).DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(6).DefaultCellStyle.BackColor = Color.LightBlue
                .Columns(7).DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(7).DefaultCellStyle.BackColor = Color.LawnGreen
                .Columns(8).DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End If
            If ldType = "IS" Or ldType = "SR" Then
                .Columns(0).Width = 50
                .Columns(1).Width = 50
                .Columns(.ColumnCount - 1).Visible = False
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Columns(3).Width = 75
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                .Columns("Amount").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("Amount").Frozen = True
                .Columns("Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                .Columns("Discount").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("Discount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                '.Columns("Discount").DefaultCellStyle.BackColor = Color.LightBlue

                .Columns("Round Off").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("Round Off").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                '.Columns("Round Off").DefaultCellStyle.BackColor = Color.LawnGreen

                .Columns("Tax Amount").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("Tax Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("Tax Amount").DefaultCellStyle.BackColor = Color.LightBlue

                .Columns("Net Total").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("Net Total").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("Net Total").DefaultCellStyle.BackColor = Color.LawnGreen
            End If
            If ldType = "PO" Then
                .Columns(5).Width = 200
                .Columns(8).Width = 300
            ElseIf ldType = "MTN" Then
                .Columns("Customer Name").Width = 250
                .Columns("Created By").Width = 100
                .Columns("Doc No").Width = 80
                .Columns("Doc.Date").Width = 70
                .Columns("Docid").Visible = False
                .Columns(0).Visible = False
                resizeGridColumn(grdvoucher, 5)
            ElseIf ldType = "TIS" Then
                .Columns("Party Name").Width = 250
                .Columns("Prefix").Width = 50
                .Columns("Tr.Date").Width = 75
                .Columns(5).DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns(5).Frozen = True
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("Trid").Visible = False
                resizeGridColumn(grdvoucher, 7)
            Else
                .Columns("Tr. Description").Width = 250
                If ldType = "IS" Or ldType = "SR" Then
                    .Columns.Item("Created On").Width = 120
                    .Columns(7).Width = 200
                    'Else
                    '    .Columns.Item("Created On").Visible = False
                Else
                    .Columns(4).Width = 200
                End If
            End If
            If ldType = "JIS" Then
                .Columns.Item("JOB CODE").Width = 100
                .Columns.Item("Tr. Description").Width = 300
            End If
        End With
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Dim RptType As String = ldType
        Dim RptCaption As String = ""
        If rdosummary.Checked Then
            RptType = ldType & "S"
            If ldType = "IP" And enableWoodSale Then
                If chkpaymentdet.Checked Then
                    RptType = "IPSWR"
                Else
                    RptType = "IPSW"
                End If
            End If

            Select Case True
                Case enableWoodSale
                    If ldType = "IS" Then
                        If chkpaymentdet.Checked Then
                            RptType = "ISSWR"
                        Else
                            RptType = "ISSW"
                        End If
                    End If

                    'Case chkkottype.Checked
                    '    RptType = "ISSKOT"
                Case chkwaiter.Checked
                    RptType = "ISSSC"
                Case chkserviceby.Checked, chksection.Checked, chkkottype.Checked
                    RptType = "ISSLM"
                    If enableRestuarent Then
                        RptType = "ISSCH"
                        'If chkkottype.Checked Then
                        '    RptType = "ISSKOTCH"
                        'End If
                    End If
                    If chkwithsr.Checked Then
                        RptType = "ISSRSM"
                    End If
                Case chkwithsr.Checked And Not chkmultipledebit.Checked
                    RptType = "ISSR"
                Case chkmultipledebit.Checked
                    RptType = "ISMD"
                Case enableRouteBulkSale And cmbroute.Text <> ""
                    RptType = "ISDS"
            End Select

            'If ldType = "IS" And enableWoodSale Then
            '    If chkpaymentdet.Checked Then
            '        RptType = "ISSWR"
            '    Else
            '        RptType = "ISSW"
            '    End If
            'End If
            'If ldType = "IS" And chkkottype.Checked Then
            '    RptType = "ISSKOT"
            'End If
            'If ldType = "IS" And chkserviceby.Checked Then
            '    RptType = "ISSLM"
            '    If enableRestuarent Then
            '        RptType = "ISSCH"
            '        If chkkottype.Checked Then
            '            RptType = "ISSKOTCH"
            '        End If
            '    End If
            'End If
            'If ldType = "IS" And chkwithsr.Checked Then
            '    ldType = "ISSR"
            'End If
            Dim cnt As Integer
            If chksale.Checked Then cnt = 1
            If chkcardsale.Checked Then cnt = cnt + 1
            If chkservicesale.Checked Then cnt = cnt + 1
            If ldType = "IS" And cnt > 1 Then
                RptType = "WSIS"
            End If
            If chkcommission.Checked Then
                RptType = "SMC" 'Salesman commission
            End If
        Else
            RptType = ldType & "D"
            If ldType = "IP" And enableWoodSale Then
                RptType = "IPDW"
            End If
            If ldType = "IS" And enableWoodSale Then
                RptType = "ISDW"
                
            End If
            If ldType <> "TIS" Then
                If ldType = "IS" And chkkottype.Checked Then
                    RptType = "ISDKOT"
                End If
                If chkItemtotal.Checked Then
                    RptType = "PIQL"
                ElseIf chkserviceby.Checked Or chkkottype.Checked Or chksection.Checked Or chkwaiter.Checked Then
                    RptType = "ISSMN"
                    If enableRestuarent Then
                        RptType = "ISDSCH"
                        'If chkkottype.Checked Then
                        '    RptType = "ISDKOTSCH"
                        'End If
                    End If
                End If
                If chkLevelWise.Checked Then
                    RptType = "ISDL"
                End If
            Else
                If chkvazhipaducomm.Checked Then
                    RptType = "TISC"
                End If
            End If
            Dim cnt As Integer
            If chksale.Checked Then cnt = 1
            If chkcardsale.Checked Then cnt = cnt + 1
            If chkservicesale.Checked Then cnt = cnt + 1
            If ldType = "IS" And cnt > 1 Then
                RptType = "WSISD"
            End If
            If ldType = "IS" And chkwithsr.Checked Then
                RptType = "ISDSR"
            End If
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
        'fRptFormat = New RptFormatfrm
        'fRptFormat.RptType = RptType
        'fRptFormat.ShowDialog()
        'fRptFormat = Nothing
    End Sub
    Private Function getwhere() As String
        Dim txtcondition As String
        Dim condition As String
        If Not isDoc Then
            Dim tpcondition As String = ""
            If ldType = "IS" Then
                If chkcardsale.Checked Or chkservicesale.Checked Then
                    tpcondition = ""
                    If chksale.Checked Then
                        tpcondition = "'IS'"
                    End If
                    If chkcardsale.Checked Then
                        tpcondition = tpcondition & IIf(tpcondition = "", "", ",") & "'DIS'"
                    End If
                    If chkservicesale.Checked Then
                        tpcondition = tpcondition & IIf(tpcondition = "", "", ",") & "'SIS'"
                    End If
                    tpcondition = " trtype IN(" & tpcondition & ")"
                Else
                    tpcondition = " trtype='" & ldType & "'"
                    If chkwithsr.Checked Then
                        tpcondition = "(" & tpcondition & " OR trtype='SR')"
                    End If
                End If
            ElseIf ldType <> "TIS" Then
                tpcondition = " trtype='" & ldType & "'"
            End If
            If ldType = "TIS" And rdoitemwise.Checked And chkvazhipadudate.Checked = True Then
                condition = " WHERE VazhipaduDate>='" & Format(CDate(cldrStartDate.Value), "yyyy/MM/dd") & "' and VazhipaduDate<='" & Format(CDate(cldrEnddate.Value), "yyyy/MM/dd") & "'"
            Else
                condition = " WHERE " & IIf(ldType = "IS", " isnull(invStatus,0)=0 and ", "") & "Trdate>='" & Format(CDate(cldrStartDate.Value), "yyyy/MM/dd") & "' and Trdate<='" & Format(CDate(cldrEnddate.Value), "yyyy/MM/dd") & IIf(tpcondition = "", "'", "' AND " & tpcondition)
            End If

            If cmbinvtype.Text <> "" Then
                condition = condition & " AND vouchername='" & cmbinvtype.Text & "'"
            End If
            If cmbinvcategory.Text <> "" Then
                If cmbinvcategory.Text = "B2B" Then
                    condition = condition & " AND isnull(isB2B,0)=1"
                ElseIf cmbinvcategory.Text = "B2C" Then
                    condition = condition & " AND isnull(isB2B,0)=0"
                End If
            End If
            Select Case ldType
                Case "TIS"
                    txtcondition = cmbSearch.Text
                Case Else
                    If cmbSearch.Text = "Customer Name" Then
                        txtcondition = "Supplier Name"
                    ElseIf cmbSearch.Text = "Cust. Code" Then
                        txtcondition = "Sup. Code"
                    ElseIf cmbSearch.Text = "Invoice" Then
                        txtcondition = ""
                    Else
                        txtcondition = cmbSearch.Text
                    End If
            End Select
        Else
            condition = " WHERE [Doc.Date]>='" & Format(CDate(cldrStartDate.Value), "yyyy/MM/dd") & "' and [Doc.Date]<='" & Format(CDate(cldrEnddate.Value), "yyyy/MM/dd") & "' AND DocType='" & ldType & "'"

            If cmbSearch.Text = "Customer Name" Or cmbSearch.Text = "Supplier Name" Then
                txtcondition = "AccDescr"
           
            Else
                txtcondition = cmbSearch.Text
            End If

        End If
        If txtSearch.Text <> "" And txtcondition <> "" Then
            If chkSearch.Checked Then
                condition = condition & " AND [" & txtcondition & "] like '" & txtSearch.Text & "%'"
            Else
                condition = condition & " AND [" & txtcondition & "] like '%" & txtSearch.Text & "%'"
            End If
        End If
        If cmbsalesman.Text <> "" Then
            If chkserviceby.Checked Then
                condition = condition & " AND Smancode='" & cmbsalesman.Text & "'"
            End If
        End If
        If cmbwaiter.Text <> "" Then
            If chkwaiter.Checked Then
                condition = condition & " AND Smancode='" & cmbwaiter.Text & "'"
            End If
        End If
        If cmbschedule.Text <> "" Then
            If chkserviceby.Checked Then
                condition = condition & " AND Schedule='" & cmbschedule.Text & "'"
            End If
        End If

        If cmbkottype.Text <> "" And ldType <> "TIS" Then
            If chkkottype.Checked Then
                condition = condition & " AND Kotsts='" & cmbkottype.Text & "'"
            End If
        End If
        If Val(txtitem.Tag) > 0 Then
            condition = condition & " AND itemid=" & Val(txtitem.Tag)
            'ElseIf txtitem.Tag <> "" Then
            '    condition = condition & " AND " & txtitem.Tag
        End If
        If cmbsection.Text <> "" Then
            condition = condition & " AND sectionname='" & cmbsection.Text & "'"
        End If
        Select Case ldType
            Case "TIS"
                If rdoitemwise.Checked Then
                    Dim isacc As String = ""
                    If chkItemtotal.Checked Then
                        isacc = "Isacc=1"
                    End If
                    If chkserviceby.Checked Then
                        isacc = isacc & IIf(isacc = "", "", " OR ") & "Isacc=0"
                    End If
                    condition = condition & IIf(isacc <> "", " AND (" & isacc & ")", "")
                    If chkvazhipaducomm.Checked Then
                        condition = condition & " AND ISNULL(vazhipadurateThirumeni,0)+ISNULL(vazhipadurateKazhakam,0)>0"
                    End If
                End If
            Case "IS", "IP", "SR", "PR", "TI", "TO"
                Dim lvl As String = ""
                If chkLevelWise.Checked Then
                    If chkLevelWise.Checked Then
                        Dim i As Integer
                        For i = 0 To grdLevel.RowCount - 1
                            If grdLevel.Item(1, i).Value <> "" Then
                                lvl = lvl & IIf(lvl = "", "", " AND ") & " Lvl" & i + 1 & "='" & grdLevel.Item(1, i).Value & "'"
                                lvlGrp = lvlGrp & IIf(lvlGrp = "", "", "-") & grdLevel.Item(1, i).Value
                            End If
                        Next
                    End If
                    condition = condition & IIf(lvl = "", "", " AND ") & lvl
                End If
        End Select
        Select Case ldType
            Case "IS", "IP", "SR", "PR", "TI", "TO", "SO", "DOC", "PO", "QTI"
                condition = condition & IIf(UsrBr = "", "", " AND Brid='" & UsrBr & "'")
                condition = condition & IIf(enableuserwisetransactionlist And userType, " AND [created by]='" & CurrentUser & "'", "")
        End Select
        Return condition
    End Function
    Public Sub PrepareReport(ByVal FileName As String, ByVal RptCaption As String, ByVal forpint As Boolean)
        If Not frm Is Nothing Then
            frm.Close()
            frm = New ReprtviewNEWfrm
        Else
            frm = New ReprtviewNEWfrm
        End If
        Dim condition As String = getwhere()
        Dim AccDate1 = Format(CDate(cldrStartDate.Value), "yyyy/MM/dd")
        Dim AccDate2 = Format(CDate(cldrEnddate.Value), "yyyy/MM/dd")
        Dim dateFlds As String = "'" & Format(CDate(cldrStartDate.Value), "dd-MM-yyyy") & "' DateFrom ,'" & Format(CDate(cldrEnddate.Value), "dd-MM-yyyy") & "' DateTo,'" & lvlGrp & "' levelGrp "
        Dim ds As DataSet = Nothing
        Dim restuarentgrpBy As String = ""
        If isDoc Then
            If rdosummary.Checked Then
                ds = _objReport.returnDetailsForReport(condition, dateFlds, "returnDocDetailsForReport", 0, "")
            ElseIf rdoitemwise.Checked Then
                ds = _objReport.returnDetailsForReport(condition, dateFlds, "returnDOCItemDetailsForReport", 0, "")
            End If
        Else
            Dim rptType As Integer
            Select Case ldType
                Case "JIS" ' job sales 
                    rptType = 2
                Case "TIS" 'vazhipadu sales
                    rptType = 4
                Case Else 'inventory transaction
                    rptType = 1
                    If enableRestuarent Then
                        If chkwaiter.Checked Then
                            rptType = 6
                            restuarentgrpBy = ",Smancode"
                        End If
                        If chksection.Checked Then
                            rptType = 6
                            If restuarentgrpBy <> "" Then
                                restuarentgrpBy = restuarentgrpBy & "+'-'+sectionname"
                            Else
                                restuarentgrpBy = ",sectionname"
                            End If

                        End If
                        If chkkottype.Checked Then
                            rptType = 7
                            If restuarentgrpBy <> "" Then
                                restuarentgrpBy = restuarentgrpBy & "+'-'+Kotsts"
                            Else
                                restuarentgrpBy = ",Kotsts"
                            End If

                        End If
                        If chkserviceby.Checked Then
                            rptType = 6
                            If restuarentgrpBy <> "" Then
                                restuarentgrpBy = restuarentgrpBy & "+'-'+Schedule"
                            Else
                                restuarentgrpBy = ",Schedule"
                            End If
                        End If
                        If restuarentgrpBy <> "" Then
                            restuarentgrpBy = restuarentgrpBy & " grpBy"
                        End If
                        If chkcommission.Checked Then
                            restuarentgrpBy = ""
                        End If
                    End If
            End Select
            If rdosummary.Checked Then
                If chkpaymentdet.Checked Then
                    rptType = 8
                End If
                If chkcommission.Checked Then
                    Dim tp As Integer
                    If enableRestuarent Then
                        If cmbwaiter.Text <> "" Then
                            tp = 1
                        Else
                            tp = 0
                        End If
                    Else
                        If cmbsalesman.Text <> "" Then
                            tp = 1
                        Else
                            tp = 0
                        End If
                    End If
                    ds = _objReport.retrunSalesManCommissionForPrint(DateValue(cldrStartDate.Value), DateValue(cldrEnddate.Value), IIf(enableRestuarent, cmbwaiter.Text, cmbsalesman.Text), tp)
                ElseIf enableRouteBulkSale And cmbroute.Text <> "" Then
                    Dim qry As String
                    qry = "select AccDescr cname,'' items,NetAmt salesamt,OpnBal+isnull(balamt,0)lastdue,isnull(rvamt,0)cashamt,OpnBal+isnull(balamt,0)+isnull(NetAmt,0)totaldue," & _
                    " AreaCode routename,TrDate t_date,'' salesman,1 lnk," & dateFlds & " from ( select sum(netamt)netamt,cscode,trdate from ItmInvCmnTb " & _
                    " where TrType='IS' and trdate>='" & Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd") & _
                    "' and trdate<='" & Format(DateValue(cldrEnddate.Value), "yyyy/MM/dd") & _
                    "'  group by cscode,trdate)itmInvCmntb" & _
                    " left join AccMast on ItmInvCmnTb.CSCode=AccMast.AccId" & _
                    " left join S1AccHd on S1AccHd.S1AccId=AccMast.S1AccId" & _
                    " left join (select sum(dealamt)balamt,AccountNo from AccTrCmn" & _
                    " left join AccTrDet on AccTrCmn.LinkNo=AccTrDet.LinkNo where JVDate<'" & Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd") & "' group by AccountNo)bal on AccMast.AccId=bal.AccountNo" & _
                    " left join (select sum(dealamt*-1)rvamt,AccountNo from AccTrCmn " & _
                    " left join AccTrDet on AccTrCmn.LinkNo=AccTrDet.LinkNo where JVDate>='" & Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd") & "' and JVDate<='" & Format(DateValue(cldrEnddate.Value), "yyyy/MM/dd") & "' and DealAmt<0 group by AccountNo)rv on AccMast.AccId=rv.AccountNo" & _
                    " where  GrpSetOn ='customer' and AreaCode ='" & cmbroute.Text & "' "
                    ds = _objcmnbLayer._ldDataset(qry, False)
                Else
                    If chkmultipledebit.Checked Then rptType = 9
                    ds = _objReport.returnDetailsForReport(condition, dateFlds & restuarentgrpBy, "returnInventoryDetailsForReport", rptType, AccDate1, AccDate2, 5)
                End If
            ElseIf rdoitemwise.Checked Then

                If chkLevelWise.Checked Then
                    rptType = 8
                End If
                ds = _objReport.returnDetailsForReport(condition, dateFlds & restuarentgrpBy, "returnInventoryItemwiseDetailsForReport", rptType, "", "")

            End If
        End If

        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = fMainForm
        frm.Text = RptCaption
        frm.Show()
    End Sub
   
    'WRITEOFF OPENING VALUS AS EXPENSE RELATED TO OFFICE
    Private Sub InvReportFrm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        'fillGrid(0)
        If ldType = "PO" Then
            grpDocument.Visible = True
        Else
            grpDocument.Visible = False
        End If
        txtSearch.Focus()
    End Sub

    Private Sub InvReportFrm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not fRptFormat Is Nothing Then fRptFormat.Close() : fRptFormat = Nothing
        If Not frm Is Nothing Then frm.Close() : frm = Nothing
        'fMainForm.plHome.Visible = True
    End Sub

    Private Sub InvReportFrm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If cboxroute Then
            cmbroute.Visible = True
            lroute.Visible = True
        Else
            cmbroute.Visible = False
            lroute.Visible = False
        End If

        AddtoCombo(cmbroute, "SELECT areacode FROM areatb", True, False)
        cldrStartDate.Value = Format(DateValue(Date.Now), DtFormat)
        cldrEnddate.Value = Format(Date.Now, DtFormat)
        'setUpGrid()
        ldBranch()
        plsr.Visible = False
        ldsalesman()
        crtSubVrs(cmbinvtype, 4, True, True)
        btnLoad_Click(btnLoad, New System.EventArgs)
        'chkwithsr.Top = chkItemtotal.Top
        'chkwithsr.Left = chkItemtotal.Left
        'cldrStartDate.Value = DateAdd(DateInterval.Day, -30, DateValue(Date.Now))
        If ldType <> "IS" Then
            cmbsalesman.Visible = False
            chkserviceby.Visible = False
            cmbschedule.Visible = False
            If enableWoodSale Then
                chkpaymentdet.Visible = True
                chkpaymentdet.Text = "With Payment"
            End If
            chkwithsr.Visible = False
            If ldType = "TIS" Then
                chkvazhipaducomm.Top = cmbsalesman.Top
                chkvazhipaducomm.Left = chkserviceby.Left
                chkvazhipadudate.Top = chkkottype.Top
                chkvazhipadudate.Left = chkkottype.Left
                chkLevelWise.Visible = False
                'chkvazhipaducomm.Visible = True
            End If
            If enableCarWash And ldType = "IP" Then
                'plsale.Left = grpDocument.Left
                'plsale.Top = grpDocument.Top
                plsale.Visible = True
            End If
            chkmultipledebit.Visible = False
            plsr.Visible = False
        Else
            If enableRestuarent Then
                chkserviceby.Text = "Schedule Wise"
                cmbschedule.Top = cmbsalesman.Top
                cmbsalesman.Visible = False
                cmbschedule.Visible = True
                chkkottype.Visible = True
                cmbkottype.Visible = True
                grpIntype.Visible = True
                chkmultipledebit.Left = btnexport.Left - chkmultipledebit.Width
                chkmultipledebit.Top = btnexport.Top
            Else
                cmbschedule.Visible = False
                chkmultipledebit.Left = chkkottype.Left
                chkmultipledebit.Top = chkkottype.Top
            End If
            If enableWoodSale Then
                chkpaymentdet.Visible = True
            End If
            If enableMultipleDebitInInvoice Or enablePOS Then chkmultipledebit.Visible = True
            plsr.Visible = True

        End If
        lbltotal.Visible = Not enableInvoiceTotalFromHistory
        Label3.Visible = Not enableInvoiceTotalFromHistory
        Timer1.Enabled = True
        chgbyprg = False
        rdoLoad()
    End Sub
    Private Sub loadSections()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select sectionname,sectionid from RestSectionTb")
        Dim i As Integer
        cmbsection.Items.Clear()
        cmbsection.Items.Add("")
        For i = 0 To dt.Rows.Count - 1
            cmbsection.Items.Add(dt(i)("sectionname"))
        Next
        If cmbsection.Items.Count > 0 Then cmbsection.SelectedIndex = 0
    End Sub
    Private Sub ldsalesman()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT SManCode FROM SalesmanTb")
        cmbsalesman.Items.Clear()
        cmbsalesman.Items.Add("")
        Dim i As Integer
        For i = 0 To dt.Rows.Count - 1
            cmbsalesman.Items.Add(dt(i)("SManCode"))
        Next
    End Sub
    'Private Sub setUpGrid()
    '    With grdvoucher
    '        '.ReadOnly = True
    '        .ColumnHeadersVisible = True
    '        .RowHeadersVisible = False
    '        .SelectionMode = DataGridViewSelectionMode.FullRowSelect
    '        .AllowUserToAddRows = False
    '        .AllowUserToDeleteRows = False
    '        .AllowUserToResizeRows = False
    '        .AllowUserToResizeColumns = True
    '        .ScrollBars = ScrollBars.Both
    '        .ColumnHeadersHeight = 250
    '        .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    '        .ColumnHeadersDefaultCellStyle.BackColor = Color.Olive
    '        .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.5!, System.Drawing.FontStyle.Regular)
    '        .RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.5!)
    '    End With
    'End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        If chgbyprg Then Exit Sub
        Dim dt As DataTable = SearchGrid(dtTable, Trim(txtSearch.Text), cmbSearch.SelectedIndex, Not chkSearch.Checked)
        grdvoucher.DataSource = dt
        SetGridHead()
        gridTotal(dt)
    End Sub

    Private Sub cmbSearch_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSearch.SelectedIndexChanged
        If chgbyprg Then Exit Sub
        txtSearch.Text = ""
        SetGridHead()
        txtSearch.Focus()
    End Sub

    Private Sub rdIS_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Me.IsInitializing Then Exit Sub
        If chgbyprg Then Exit Sub
        Dim MyCtrl As RadioButton = sender
        If MyCtrl.Checked = True Then
            opt = Val(MyCtrl.Tag)
            loadWaite(1)
        End If
    End Sub

    Private Sub chkDate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Me.IsInitializing Then Exit Sub
        loadWaite(1)
    End Sub

    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        PrepareReport(RptFlName, RptCaption, forPrint)
    End Sub

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        loadWaite(1)
    End Sub

    Private Sub ftnFP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        cldrStartDate.Value = Format(CDate(DateFrom), DtFormat)
        cldrEnddate.Value = Format(CDate(DateTo), DtFormat)
        btnLoad_Click(sender, e)
    End Sub

    Private Sub btnToday_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        cldrStartDate.Value = Format(CDate(Today), DtFormat)
        cldrEnddate.Value = Format(CDate(Today), DtFormat)
        btnLoad_Click(sender, e)
    End Sub

  


    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub ldBranch()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT * FROM BranchTb")
        cmbbranch.Items.Clear()
        cmbbranch.Items.Add("")
        Dim i As Integer
        For i = 0 To dt.Rows.Count - 1
            cmbbranch.Items.Add(dt(i)("BranchId"))
        Next
    End Sub


    Private Sub btnedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnedit.Click
        If grdvoucher.RowCount = 0 Then Exit Sub
        If grdvoucher.CurrentRow Is Nothing Then Exit Sub
        If ldType = "IS" Then 'IS
            fMainForm.LoadIS(Val(grdvoucher.Item(grdvoucher.ColumnCount - 1, grdvoucher.CurrentRow.Index).Value))
        ElseIf ldType = "IP" Then 'IP
            fMainForm.LoadIP(Val(grdvoucher.Item(grdvoucher.ColumnCount - 1, grdvoucher.CurrentRow.Index).Value))
        ElseIf ldType = "SR" Then 'SR
            fMainForm.LoadSR(Val(grdvoucher.Item(grdvoucher.ColumnCount - 1, grdvoucher.CurrentRow.Index).Value))
        ElseIf ldType = "PR" Then 'PR
            fMainForm.LoadPR(Val(grdvoucher.Item(grdvoucher.ColumnCount - 1, grdvoucher.CurrentRow.Index).Value))
        ElseIf ldType = "JIS" Then 'PR
            fMainForm.LoadJIS(Val(grdvoucher.Item(grdvoucher.ColumnCount - 1, grdvoucher.CurrentRow.Index).Value))
        ElseIf ldType = "MTN" Then 'MTN
            fMainForm.LoadGIN(Val(grdvoucher.Item(grdvoucher.ColumnCount - 1, grdvoucher.CurrentRow.Index).Value))
        ElseIf ldType = "TIS" And enableChurchModule Then 'MTN
            fMainForm.ldNerchaSales(Val(grdvoucher.Item(grdvoucher.ColumnCount - 1, grdvoucher.CurrentRow.Index).Value))
        Else
            Exit Sub
        End If
    End Sub

    Private Sub grdvoucher_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdvoucher.DoubleClick
        btnedit_Click(btnedit, New System.EventArgs)
    End Sub
    Private Sub ldDocumentItems(ByVal rIndex As Integer)
        Try
            If chkPending.Checked Then
            Else
                'If grdvoucher.RowCount = 0 Then Exit Sub
                Dim qryTp As Integer
                If ldType = "JIS" Then
                    qryTp = 2
                ElseIf ldType = "MTN" Then
                    qryTp = 3
                ElseIf ldType = "TIS" Then
                    qryTp = 4
                Else
                    qryTp = 1
                End If
                itmDatatable = _objReport.returnItemsToListForm("returnInventoryItemsToListForm", Val(grdvoucher.Item(grdvoucher.ColumnCount - 1, rIndex).Value), qryTp).Tables(0)
                grdItemView.DataSource = itmDatatable
                setItemGridHead()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub grdvoucher_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdvoucher.RowEnter
        ldDocumentItems(e.RowIndex)
    End Sub
    Private Sub setItemGridHead()
        With grdItemView
            SetGridProperty(grdItemView)
            .Columns("Item Code").Width = 100
            .Columns("IDescription").Width = 200
            .Columns("Unit").Width = 50
            .Columns("TrQty").Width = 70
            .Columns("TrQty").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("TrQty").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("UnitCost").Width = 100
            .Columns("UnitCost").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("UnitCost").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            
            .Columns("LineTotal").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("LineTotal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            If Not ldType = "TIS" Then
                .Columns("Taxp").Width = 100
                .Columns("Taxp").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("Taxp").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("TaxAmt").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("TaxAmt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("Cess").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("Cess").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                If Not ldType = "JIS" Then
                    .Columns("FOC").DefaultCellStyle.Format = "N" & NoOfDecimal
                    .Columns("FOC").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns("FOC").Visible = enableFOCQty
                    .Columns("FOC").Width = 70
                    .Columns("SerialNo").Visible = enableSerialnumber Or enableBatchwiseTr
                End If
                

                If enablecess Then
                    .Columns("Cess").Visible = True
                Else
                    .Columns("Cess").Visible = False
                End If

            End If

            '.Columns(7).Visible = False
            .Columns("itemid").Visible = False
            If ldType = "MTN" Then
                .Columns("UnitCost").Visible = False
                .Columns("Taxp").Visible = False
                .Columns("TaxAmt").Visible = False
                .Columns("LineTotal").Visible = False
                .Columns("TrQty").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("TrQty").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                resizeGridColumn(grdItemView, 1)
            ElseIf ldType = "TIS" Then
                .Columns("detid").Visible = False
                
            End If
            Dim i As Integer
            If cmbitmOrder.Items.Count = 0 Then
                For i = 0 To .ColumnCount - 2
                    cmbitmOrder.Items.Add(.Columns(i).HeaderText)
                Next
            End If
            If Val(.Tag & "") = 0 Then
                Timer1.Enabled = True
                .Tag = 1
            Else
                resizeGridColumn(grdItemView, 1)
            End If
        End With
    End Sub

    Private Sub txtitemSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtitemSearch.TextChanged
        grdItemView.DataSource = SearchGrid(itmDatatable, Trim(txtitemSearch.Text), cmbitmOrder.SelectedIndex, Not chkitemSearchOnly.Checked)
        setItemGridHead()
    End Sub

    Private Sub cldrStartDate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cldrStartDate.KeyDown
        If e.KeyCode = Keys.Return Then
            cldrEnddate.Focus()
        End If
    End Sub
    Private Sub cldrEnddate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cldrEnddate.KeyDown
        If e.KeyCode = Keys.Return Then
            btnLoad_Click(btnLoad, New System.EventArgs)
        End If
    End Sub

    Private Sub rdosummary_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdosummary.Click, rdoitemwise.Click
        rdoLoad()
    End Sub
    Private Sub rdoLoad()
        plitem.Visible = False
        txtitem.Text = ""
        txtitem.Tag = ""
        chkItemtotal.Visible = False
        chkserviceby.Visible = False
        cmbsalesman.Visible = False
        grpIntype.Visible = False
        cmbinvtype.Text = ""
        chkwithsr.Visible = False
        chkItemtotal.Checked = False
        chkserviceby.Checked = False
        chkvazhipaducomm.Visible = False
        chkvazhipadudate.Visible = False
        chkcommission.Checked = False
        chkcommission.Visible = False
        chkmultipledebit.Visible = False
        'chkmultipledebit.Left = btnexport.Left - chkmultipledebit.Width
        If rdosummary.Checked Then
            chkserviceby.Text = "Sales Man wise"
            If ldType = "IS" Then
                If enableMultipleDebitInInvoice Or enablePOS Then chkmultipledebit.Visible = True
                cmbsalesman.Visible = True
                chkserviceby.Visible = True
                grpIntype.Visible = True
                chkwithsr.Visible = True
            End If
            If enableRestuarent Then
                chkserviceby.Text = "Schedule Wise"
                cmbschedule.Top = cmbsalesman.Top
                cmbsalesman.Visible = False
                grpIntype.Visible = True
            End If
            If enableWoodSale And (ldType = "IS" Or ldType = "IP") Then
                chkpaymentdet.Visible = True
            End If
            If chkwaiter.Checked Or chkserviceby.Checked Then chkcommission.Visible = True
            'If ldType = "TIS" Then chkkottype.Visible = False
        ElseIf rdoitemwise.Checked Then
            chkItemtotal.Visible = True
            chkpaymentdet.Visible = False
            chkwithsr.Visible = True
            plitem.Visible = True
            If enableItemwiseSalesman Then
                chkserviceby.Visible = False
                chkserviceby.Text = "Serviced By wise"
                If ldType = "IS" Then cmbsalesman.Visible = True
                If ldType = "IS" Then chkserviceby.Visible = True
            Else
                cmbsalesman.Visible = False
                chkserviceby.Visible = False
            End If
            If enableRestuarent Then
                chkserviceby.Text = "Schedule Wise"
                cmbschedule.Top = cmbsalesman.Top
                cmbsalesman.Visible = False
                chkserviceby.Visible = True
                grpIntype.Visible = False
            End If
            If ldType = "TIS" Then
                If enableChurchModule Then
                    chkItemtotal.Text = "Nercha Items"
                    chkvazhipaducomm.Visible = False
                    chkvazhipadudate.Visible = False
                Else
                    chkItemtotal.Text = "Vazhipadu Items"
                    chkvazhipaducomm.Visible = True
                    chkvazhipadudate.Visible = True
                End If

                chkserviceby.Text = "Inventory Items"
                'chkkottype.Text = "By Vazhipadu Date"
                chgbyprg = True
                chkItemtotal.Checked = True
                chkserviceby.Checked = True
                chkserviceby.Visible = True

                'chkkottype.Visible = True
                'chkkottype.Top = cmbsalesman.Top
                chgbyprg = False
            End If
        End If
    End Sub

    Private Sub chkItemtotal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkItemtotal.Click
        If ldType = "TIS" Then Exit Sub
        If chgbyprg Then Exit Sub
        chgbyprg = True
        If chkSearch.Visible Then chkserviceby.Checked = False
        chgbyprg = False
    End Sub

    Private Sub chkserviceby_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkserviceby.CheckedChanged
        If ldType = "TIS" Then Exit Sub
        If chgbyprg Then Exit Sub
        chgbyprg = True
        chkItemtotal.Checked = False
        If Not enableRestuarent And rdosummary.Checked Then chkcommission.Visible = chkserviceby.Checked
        chgbyprg = False
    End Sub


    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        Select Case ldType
            Case "TIS"
                rdoitemwise.Checked = True
                rdosummary_Click(rdoitemwise, New System.EventArgs)
                resizeGridColumn(grdvoucher, 7)
                resizeGridColumn(grdItemView, 1)
            Case "IS", "JIS"
                'grpIntype.Visible = Not enableRestuarent
                grpIntype.Top = grpDocument.Top - 2
                grpIntype.Left = grpDocument.Left - 5
                chkcommission.Top = cmbsalesman.Top + cmbsalesman.Height
                chkcommission.Left = cmbsalesman.Left
                resizeGridColumn(grdItemView, 1)
                If enableRestuarent Then
                    grpsection.Left = grpIntype.Left - 40
                    grpsection.Top = grpIntype.Top + grpIntype.Height + 1
                    grpsection.Visible = True
                    loadSections()
                    plwaiter.Visible = True
                    plwaiter.Top = grpIntype.Top - plwaiter.Height - 2
                    plwaiter.Left = grpIntype.Left - 70
                    chkcommission.Top = plwaiter.Top + 8
                    chkcommission.Left = plwaiter.Left + chkwaiter.Width
                    loadwaiter()
                End If
        End Select
       
    End Sub
    Private Sub loadwaiter()
        Dim dt As DataTable
        If enableSingleUserKOT Then
            dt = _objcmnbLayer._fldDatatable("SELECT SManCode FROM SalesmanTb")
            cmbwaiter.Items.Clear()
            cmbwaiter.Items.Add("")
            Dim i As Integer
            For i = 0 To dt.Rows.Count - 1
                cmbwaiter.Items.Add(dt(i)("SManCode"))
            Next
        Else
            dt = _objcmnbLayer._fldDatatable("SELECT UserId FROM UserTb")
            cmbwaiter.Items.Clear()
            cmbwaiter.Items.Add("")
            Dim i As Integer
            For i = 0 To dt.Rows.Count - 1
                cmbwaiter.Items.Add(dt(i)("UserId"))
            Next
        End If

    End Sub


    Private Sub txtitem_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtitem.KeyDown
        If e.KeyCode = Keys.F2 Then
            fProductEnquiry = New ItmEnqry
            fProductEnquiry.isVazhipadusales = IIf(ldType = "TIS", True, False)
            fProductEnquiry.ShowDialog()
        ElseIf e.KeyCode = Keys.Delete Then
            txtitem.Text = ""
            txtitem.Tag = ""
        End If
    End Sub


    Private Sub fProductEnquiry_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fProductEnquiry.FormClosed
        fProductEnquiry = Nothing
    End Sub

    Private Sub fProductEnquiry_getSelected(ByVal ItemcODE As String, ByVal isacc As Integer) Handles fProductEnquiry.getSelected
        Dim dt As DataTable
        Dim strMyQry As String
        If ldType = "TIS" Then
            strMyQry = "SELECT * FROM (SELECT [Item Code], InvItm.Description,ItemId,0 isacc FROM InvItm UNION ALL " & _
                                "SELECT Alias,AccDescr,AccId,1 FROM AccMast left join S1AccHd on S1AccHd.S1AccId=AccMast.S1AccId where GrpSetOn='Vazhipadu') Itm " & _
                                "where [Item Code] ='" & ItemcODE & "'"
        Else
            strMyQry = "SELECT [Item Code], InvItm.Description,ItemId FROM InvItm " & _
                                "where [Item Code] ='" & ItemcODE & "'"
        End If

        dt = _objcmnbLayer._fldDatatable(strMyQry)
        If dt.Rows.Count > 0 Then
            txtitem.Text = dt(0)("Description")
            txtitem.Tag = dt(0)("ItemId")
            If ldType = "TIS" Then
                If dt(0)("isacc") = 0 Then
                    chkItemtotal.Checked = False
                    chkItemtotal.Enabled = False
                    chkserviceby.Checked = True
                    chkserviceby.Enabled = True
                Else
                    chkItemtotal.Checked = True
                    chkItemtotal.Enabled = True
                    chkserviceby.Checked = False
                    chkserviceby.Enabled = False
                End If
            End If
        End If
        fProductEnquiry.Close()
        'txtitem.Text = ItemcODE

    End Sub


    Private Sub cmbkottype_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbkottype.SelectedIndexChanged
        If cmbkottype.Text = "" Then
            chkkottype.Checked = False
        Else
            chkkottype.Checked = True
        End If
    End Sub

    Private Sub cmbschedule_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbschedule.SelectedIndexChanged
        If cmbschedule.Text <> "" Then
            chkserviceby.Checked = True
        Else
            chkserviceby.Checked = False
        End If

    End Sub

    Private Sub cmbsalesman_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbsalesman.SelectedIndexChanged
        If cmbsalesman.Text <> "" Then
            chkserviceby.Checked = True
        Else
            chkserviceby.Checked = False
        End If

    End Sub

    Private Sub rdosummary_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdosummary.CheckedChanged

    End Sub

    Private Sub rdoitemwise_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoitemwise.CheckedChanged

    End Sub

    Private Sub cmbwaiter_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbwaiter.SelectedIndexChanged
        If cmbwaiter.Text <> "" Then
            chkwaiter.Checked = True
        Else
            chkwaiter.Checked = False
        End If
    End Sub

    Private Sub chkwaiter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkwaiter.Click
        If rdosummary.Checked Then chkcommission.Visible = chkwaiter.Checked
    End Sub

    Private Sub chkcommission_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkcommission.CheckedChanged

    End Sub

    Private Sub chkwaiter_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkwaiter.CheckedChanged

    End Sub

    Private Sub chkLevelWise_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkLevelWise.CheckedChanged
        grdLevel.ReadOnly = Not chkLevelWise.Checked
        panellevel.Visible = chkLevelWise.Checked
        If chkLevelWise.Checked Then
            SetLevelGrid()
            ldLevel()
        End If
    End Sub

    Private Sub picCloseProd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picCloseProd.Click
        chkLevelWise.Checked = False
    End Sub

    Private Sub grdLevel_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdLevel.CellClick
        If grdLevel.CurrentCell.ColumnIndex = 1 Then
            grdLevel.BeginEdit(True)
        End If
    End Sub

    Private Sub grdLevel_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdLevel.Enter
        If grdLevel.CurrentCell.ColumnIndex = 1 Then
            grdLevel.BeginEdit(True)
        End If
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

    Private Sub ldLevel()
        Dim LevelTb As DataTable
        Dim LevelGrpTb As DataTable
        Dim dtGroup As DataTable
        Dim cmb As New DataGridViewComboBoxCell
        Dim itmlevel As New DataTable
        'grdLevel.Rows.Clear()
        LevelTb = _objcmnbLayer._fldDatatable("SELECT LName,LCode from LevelTb Order by LCode")
        LevelGrpTb = _objcmnbLayer._fldDatatable("SELECT LevelTb.LCode, LName, GrpItmCode, UnqGrpId FROM LevelTb LEFT JOIN " & _
          "(SELECT GrpItmCode, LCode, UnqGrpId FROM GrpItmTb) Q ON Q.LCode = LevelTb.LCode ORDER BY LevelTb.LCode")
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

    Private Sub txtitem_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtitem.TextChanged

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
        DataTableToExcel(filename & "/" & ldType & ".xls", dtTable)
        MsgBox("Export Completed", MsgBoxStyle.Information)
    End Sub
    Private Sub fwait_triggerEvent() Handles fwait.triggerEvent
        Select Case fwait.triggerType
            Case 1
                fillGrid(opt)
            
        End Select
        If Not fwait Is Nothing Then
            fwait.Close()
            fwait = Nothing
        End If

    End Sub
    Private Sub loadWaite(ByVal triggerType As Integer)
        If Not fwait Is Nothing Then fwait.Close() : fwait = Nothing
        fwait = New WaitMessageFrm
        With fwait
            .triggerType = triggerType
            .Show()
        End With
    End Sub


    Private Sub cmbroute_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbroute.SelectedIndexChanged

    End Sub

End Class