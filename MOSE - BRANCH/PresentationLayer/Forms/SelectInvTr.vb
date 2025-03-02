
Public Class SelectInvTr
    Private tmpMyQry As String
    Private strMyQry As String
    Private strMyCaption As String = ""
    Private cmbShowIndex As Integer
    Private dtTable As DataTable
    Public jobcode As String
    Public isImportForTo As Boolean
    Public isForLocationTransfer As Boolean
    Dim chgbypgm As Boolean
    Public branch As String
    Public strType As String
    Public Itemid As Long
    Public ItemCode As String
    Public PartyId As Long
    Public Event selectTr(ByVal trid As Long, ByVal TrType As String)
    Public Event closetr()
    'object declarations
    Dim _objcmnbLayer As New clsCommon_BL
    'const declaration
    Const ConstInvNo = 0
    Const ConstTrDate = 1
    Const ConstTrRefNo = 2
    Const ConstLPO = 3
    Const ConstAccDescr = 4
    Const ConstItemCode = 5
    Const ConstIDescription = 6
    Const ConstTrQty = 7
    Const ConstUnitCost = 8
    Const ConstItemid = 9
    Const ConstTrid = 10
    Private Sub SelectInvTr_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        setEnquiryLoad()
        'txtSearch.Select()
    End Sub
    Public Sub setEnquiryLoad()
        Try
            If isForLocationTransfer Then
                AddtoCombo(cmbbranch, "SELECT Branchcode FROM BranchTb", True, False)
                Dim i As Integer
                If cmbbranch.Items.Count > 0 Then
                    For i = 0 To cmbbranch.Items.Count - 1
                        If cmbbranch.Items.Item(i).ToString <> UsrBr And cmbbranch.Items.Item(i).ToString <> "" Then
                            cmbbranch.SelectedIndex = i
                        End If
                    Next
                End If
            End If
            If branch <> "" Then
                cmbbranch.Text = branch
            End If
            FillGrid()

            'Me.Text = strMyCaption
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub setComboGrid()
        Dim i As Integer = 0
        Dim grdcount As Integer = IIf(grdVoucher.ColumnCount - 1 >= cmbShowIndex, cmbShowIndex, grdVoucher.ColumnCount - 1)
        For i = 0 To grdcount
            cmbSearch.Items.Add(grdVoucher.Columns(i).HeaderText)
        Next
        If cmbSearch.Items.Count > 0 Then cmbSearch.SelectedIndex = 0
    End Sub
    Private Sub FillGrid()
        If chgbypgm Then Exit Sub
        Dim branchwhere As String
        Dim strImportWhere As String = ""
        If cmbbranch.Visible And Val(cmbbranch.Tag) = 1 Then
            branchwhere = IIf(cmbbranch.Text = "", "", " AND Brid='" & cmbbranch.Text & "'")
        Else
            branchwhere = IIf(UsrBr = "", "", " AND Brid='" & UsrBr & "'")
        End If
        Dim usercondition As String = IIf(enableuserwisetransactionlist And userType, " AND userid='" & CurrentUser & "'", "")
        Select Case strType
            Case "IP", "IS", "DIS", "SIS", "MIS"
                strMyCaption = "Inventory " & IIf(strType = "IS" Or strType = "DIS", "Sales", "Purchase")
                strMyQry = "SELECT * FROM (SELECT InvNo, TrDate,TrRefNo As Reference, Alias As PartyId,case when isnull(CashCustName,'')='' then AccDescr else CashCustName + " & _
                            "case when AccDescr='CASH IN HAND' THEN  ' ['+AccDescr+']' ELSE '' END end [Party Name],NetAmt As [Net Amount]," & _
                            "Disc As Discount,rndoff as [Round Off]," & _
                            " TrDescription As Description,UserId, " & _
                            "[Job Code], ItmInvCmnTb.TermsId, LPO, PreFix + case when PreFix= '' then '' else '/' end + " & _
                            "convert(varchar,InvNo) [No With PF], ItmInvCmnTb.TrId FROM ItmInvCmnTb LEFT JOIN " & _
                            "(SELECT TrId, Sum((UnitCost*TrQty)+isnull(taxAmt,0)+isnull(CessAmt,0)) As [Inv Amount], Sum((UnitDiscount*TrQty)+isnull(ItemDiscount,0)) As Disc FROM ItmInvTrTb " & _
                            "GROUP BY TrId) Q1 ON Q1.TrId = ItmInvCmnTb.TrId " & _
                            "LEFT JOIN AccMast ON AccMast.accid = ItmInvCmnTb.CSCode WHERE isnull(invStatus,0)=0 and TrType = '" & strType & "'" & branchwhere & usercondition & " ) Q ORDER BY trdate desc ,invno desc"
                cmbShowIndex = 14
            Case "MI"
                strMyCaption = "Inventory " & IIf(strType = "IS", "Sales", "Purchase")
                strMyQry = "SELECT * FROM (SELECT InvNo, TrDate,TrRefNo As Reference, Alias As PartyId, AccDescr As [Party Name],[Inv Amount]- Disc+rndoff As [Net Amount]," & _
                            "Disc As Discount,rndoff as [Round Off]," & _
                            " TrDescription As Description,UserId, " & _
                            "[Job Code], ItmInvCmnTb.TermsId, LPO, PreFix + case when PreFix= '' then '' else '/' end + " & _
                            "convert(varchar,InvNo) [No With PF], ItmInvCmnTb.TrId FROM ItmInvCmnTb LEFT JOIN " & _
                            "(SELECT TrId, Sum((UnitCost*TrQty)+isnull(taxAmt,0)) As [Inv Amount], Sum((UnitDiscount*TrQty)+isnull(ItemDiscount,0)) As Disc FROM ItmInvTrTb " & _
                            "GROUP BY TrId) Q1 ON Q1.TrId = ItmInvCmnTb.TrId " & _
                            "LEFT JOIN AccMast ON AccMast.accid = ItmInvCmnTb.CSCode WHERE TrType = '" & strType & "'" & branchwhere & ") Q ORDER BY trdate desc ,invno desc"
                cmbShowIndex = 14
            Case "TIS"
                strMyCaption = "Temple Sales"
                strMyQry = "SELECT * FROM (SELECT InvNo, TrDate,Reference, Alias As PartyId, AccDescr As [Party Name],[Inv Amount]+isnull(rndoff,0) As [Net Amount]," & _
                            "rndoff as [Round Off]," & _
                            " TrDescription As Description,UserId, " & _
                            " TempleSalesCmnTb.TrId FROM TempleSalesCmnTb LEFT JOIN " & _
                            "(SELECT TrId, Sum((Rate*Qty)) As [Inv Amount] FROM TempleSalesDetTb " & _
                            "GROUP BY TrId) Q1 ON Q1.TrId = TempleSalesCmnTb.TrId " & _
                            "LEFT JOIN AccMast ON AccMast.accid = TempleSalesCmnTb.CustomerAc ) Q ORDER BY trdate desc ,invno desc"
                cmbShowIndex = 5
            Case "SR", "PR"
                strMyCaption = "Inventory " & IIf(strType = "SR", "Sales Return ", "Purchase Return ") & " History of [ " & ItemCode & " ]"
                strMyQry = "SELECT * FROM (SELECT InvNo, TrDate,TrRefNo As Reference,Alias As PartyId, AccDescr As [Party Name],[Inv Amount]- Disc+rndoff As [Net Amount]," & _
                           "Disc As Discount,rndoff as [Round Off]," & _
                           " TrDescription As Description,UserId, " & _
                           "[Job Code], ItmInvCmnTb.TermsId, LPO, PreFix + case when PreFix= '' then '' else '/' end + " & _
                           "convert(varchar,InvNo) [No With PF], ItmInvCmnTb.TrId FROM ItmInvCmnTb LEFT JOIN " & _
                           "(SELECT TrId, Sum((UnitCost*TrQty)+isnull(taxAmt,0)+isnull(CessAmt,0)) As [Inv Amount], Sum((UnitDiscount*TrQty)+isnull(ItemDiscount,0)) As Disc FROM ItmInvTrTb " & _
                           "GROUP BY TrId) Q1 ON Q1.TrId = ItmInvCmnTb.TrId " & _
                           "LEFT JOIN AccMast ON AccMast.accid = ItmInvCmnTb.CSCode WHERE TrType = '" & strType & "'" & branchwhere & usercondition & ") Q ORDER BY trdate desc ,invno desc"
                cmbShowIndex = 14
            Case "QTR", "DOS", "PO"
                strMyQry = "Select DNo [Doc No] , Ddate [Doc.Date] ,Alias [Cust. Code],AccDescr [Supplier Name],InvAmt [Inv. Amount],Reference [Ref. No], JobCode,  Comment  [Tr. Description],UserId [Created By],Docid TrId" & _
                        " from ( select  DNo ,DDate ,NetAmt InvAmt,Discount,AccDescr ,Reference,Comment,Alias ,UserId,FromJob,JobCode,DocCmnTb.DocId from DocCmnTb" & _
                          " LEFT JOIN (SELECT DocId,SUM((Qty*CostPUnit)+isnull(taxAmt,0)+isnull(CessAmt,0))InvAmt, Sum(isnull(ItemDiscount,0)) As ItemDiscount FROM DocTranTb GROUP BY DocId) Tr ON  DocCmnTb.Docid=Tr.Docid" & _
                          " left join Accmast on DocCmnTb.CSCode=Accmast.accid where DocCmnTb.DocType='" & strType & "'" & branchwhere & ") as qq  order by Ddate desc ,Dno desc"
            Case "ENQ", "DOC", "QTI", "SO"
                strMyQry = "Select DNo [Doc No] , Ddate [Doc.Date] ,Alias [Cust. Code],AccDescr [Customer Name],InvAmt [Inv. Amount],Reference [Ref. No], JobCode,  Comment  [Tr. Description],UserId [Created By],Docid Trid" & _
                        " from ( select  DNo ,DDate ,NetAmt InvAmt,Discount,AccDescr ,Reference,Comment,Alias ,UserId,FromJob,JobCode,DocCmnTb.DocId from DocCmnTb" & _
                          " LEFT JOIN (SELECT DocId,SUM((Qty*CostPUnit)+isnull(taxAmt,0)+isnull(CessAmt,0))InvAmt, Sum(isnull(ItemDiscount,0)) As ItemDiscount FROM DocTranTb GROUP BY DocId) Tr ON  DocCmnTb.Docid=Tr.Docid" & _
                          " left join Accmast on DocCmnTb.CSCode=Accmast.accid where isnull(invStatus,0)=0 and DocCmnTb.DocType='" & strType & "'" & branchwhere & ") as qq  order by Ddate desc,Dno desc"
            Case "MTN"
                strMyQry = "Select DNo [Doc No] , Ddate [Doc.Date] ,Alias [Cust. Code],AccDescr [Customer Name],InvAmt [Inv. Amount],Reference [Ref. No]," & _
                        " case when isnull(FromJob,'')='' then FromLoc else FromJob end  [From Job]," & _
                        "case when isnull(JobCode,'')='' then DocDefLoc else JobCode end [To Job],Comment  [Tr. Description],UserId [Created By],Docid TrId" & _
                        " from ( select  DNo ,DDate ,InvAmt,Discount,AccDescr ,Reference,Comment,Alias ,UserId,FromJob,JobCode,DocCmnTb.DocId,DocDefLoc,FromLoc from DocCmnTb" & _
                          " LEFT JOIN (SELECT DocId,SUM(Qty*CostPUnit)InvAmt FROM DocTranTb GROUP BY DocId) Tr ON  DocCmnTb.Docid=Tr.Docid" & _
                          " left join Accmast on DocCmnTb.CSCode=Accmast.accid where DocCmnTb.DocType='" & strType & "'" & branchwhere & " ) as qq  order by Ddate desc ,Dno desc"
            Case "TI", "TO", "STO"
                Dim netAmtStr As String
                If jobcode <> "" And strType = "STO" Then
                    jobcode = " AND [Job Code]='" & jobcode & "'"
                    netAmtStr = " sum(CostAvg*TrQty)"
                Else
                    netAmtStr = "Sum((UnitCost*TrQty)+isnull(taxAmt,0)+isnull(CessAmt,0))"
                End If

                If isForLocationTransfer And strType = "TO" Then
                    strImportWhere = " left join (select trid TIidInTO from ItmInvCmnTb where trtype='TI') ImportedTI ON Q.isimportedtoTI=ImportedTI.TIidInTO " & _
                                     "WHERE isnull(TIidInTO,0)=0 AND ISNULL(locTransferInTOTI,0)=1"
                    branchwhere = branchwhere & "  and isnull(DefStkOutBr,'')='" & Dloc & "'"
                ElseIf isForLocationTransfer And strType = "TI" Then
                    strImportWhere = " left join (select isimportedtoTI TiId  from ItmInvCmnTb where trtype='TO') ImportedTO ON Q.Trid=ImportedTO.TiId " & _
                                     "WHERE isnull(TiId,0)=0 AND ISNULL(locTransferInTOTI,0)=1"
                End If
                strMyQry = "SELECT * FROM (SELECT Prefix,InvNo, TrDate,[Inv Amount]- Disc As [Net Amount]," & _
                            "Disc As Discount, AccDescr As [Account Name]," & _
                            "TrRefNo As Reference, Alias As PartyId, TrDescription As Description,UserId, " & _
                            "[Job Code], ItmInvCmnTb.TermsId, LPO, PreFix + case when PreFix= '' then '' else '/' end + " & _
                            "convert(varchar,InvNo) [No With PF], ItmInvCmnTb.TrId,isimportedtoTI,locTransferInTOTI FROM ItmInvCmnTb LEFT JOIN " & _
                            "(SELECT TrId," & netAmtStr & " As [Inv Amount], Sum(UnitDiscount*TrQty) As Disc FROM ItmInvTrTb " & _
                            "GROUP BY TrId) Q1 ON Q1.TrId = ItmInvCmnTb.TrId " & _
                            "LEFT JOIN AccMast ON AccMast.accid = ItmInvCmnTb.CSCode " & _
                            "WHERE TrType = '" & strType & "'" & branchwhere & jobcode & " ) Q " & _
                            strImportWhere & " ORDER BY trdate desc ,invno desc"
                cmbShowIndex = 13
            Case "JIS"
                strMyCaption = "Job Invoice"
                strMyQry = "Select prefix + case when isnull(prefix,'')=''  then '' else  '/' end +  convert(varchar,invNo) as [Inv No] , TrDate [Tr.Date] ,TrRefNo [Ref. No],AccDescr [Customer Name],(InvAmt-Discount)+(case when isnull(isTaxInv,0)=0 then 0 else 1 end *TaxAmt) [Amount],TrDescription  [Tr. Description],jobcode [Job Code],UserId [Created By],TrId from ( select  trtype,prefix, invNo ,TrDate ,InvAmt,Discount,AccDescr ,TrRefNo,TrDescription,Alias ,jobInvCmntb.UserId,jobcode,jobInvCmntb.TrId,TaxAmt,isTaxInv from jobInvCmntb " & _
                        "LEFT JOIN (SELECT Trid,SUM(TrQty*(UnitCost))InvAmt,sum(TaxAmt+isnull(cessAmt,0)) TaxAmt FROM jobInvTrTb GROUP BY Trid) Tr ON  jobInvCmntb.Trid=Tr.Trid left join jobtb on jobInvCmntb.jobid=jobtb.jobid  left join Accmast on jobInvCmnTb.custid=Accmast.accid where jobInvCmntb.trtype='JIS') as qq  order by TrDate ,InvNo"
        End Select
        grdVoucher.DataSource = Nothing
        If strMyQry = "" Then GoTo SetHeadOnly
        tmpMyQry = strMyQry
        dtTable = _objcmnbLayer._fldDatatable(strMyQry)
        grdVoucher.DataSource = dtTable
        If strImportWhere <> "" Then
            grdVoucher.Columns("isimportedtoTI").Visible = False
        End If
        grdVoucher.Columns("TrId").Visible = False
SetHeadOnly:
        SetGridHead()
    End Sub
    Private Sub SetGridHead()
        With grdVoucher
            SetGridProperty(grdVoucher)
            Select Case strType
                Case "IS", "IP", "SR", "PR", "OD", "MI", "DIS", "SIS", "MIS"
                    .Columns(3).Visible = False
                    .Columns(4).Width = 250
                    .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns(5).DefaultCellStyle.Format = "N" & NoOfDecimal
                    .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns(6).DefaultCellStyle.Format = "N" & NoOfDecimal
                    .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns(7).DefaultCellStyle.Format = "N" & NoOfDecimal
                    'grdVoucher.Columns("TrId").Visible = False
                Case "STR", "ODD"
                    .Columns(3).Visible = False
                    .Columns(5).Width = 250
                    .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns(6).DefaultCellStyle.Format = "N" & NoOfDecimal
                    .Columns(7).Width = 250
                    'grdVoucher.Columns("TrId").Visible = False
                Case "ODN"
                    .Columns(3).Width = 250
                    .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns(4).DefaultCellStyle.Format = "N" & NoOfDecimal
                Case "TI", "TO", "STO"
                    .Columns.Item((.ColumnCount - 1)).Visible = False
                    .Columns.Item("InvNo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Columns.Item("InvNo").Width = 100
                    .Columns.Item("Account Name").Width = 200
                    .Columns.Item("Net Amount").DefaultCellStyle.Format = "N" & NoOfDecimal
                    .Columns.Item("Net Amount").Frozen = True
                    .Columns.Item("Net Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns.Item("partyid").Visible = False
                    .Columns.Item("Description").Visible = False
                    .Columns.Item("Discount").Visible = False
                    .Columns.Item("No With PF").Visible = False
                    'grdVoucher.Columns("TrId").Visible = False
                Case "MTN"
                    .Columns(4).Visible = False
                    .Columns("Customer Name").Visible = False
                    .Columns("Inv. Amount").Visible = False
                    .Columns(2).Visible = False
                    .Columns("Tr. Description").Width = 170
                Case "TIS"
                    .Columns.Item((.ColumnCount - 1)).Visible = False
                    .Columns.Item("InvNo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Columns.Item("InvNo").Width = 100
                    .Columns.Item("UserId").Visible = False
                    .Columns.Item("Party Name").Width = 200
                    .Columns.Item("Net Amount").DefaultCellStyle.Format = "N" & NoOfDecimal
                    .Columns.Item("Net Amount").Frozen = True
                    .Columns.Item("Net Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    'grdVoucher.Columns("TrId").Visible = False
                Case "QTI"
                    .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns(4).DefaultCellStyle.Format = "N" & NoOfDecimal
                Case "JIS"
                    .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns(4).DefaultCellStyle.Format = "N" & NoOfDecimal
                    .Columns("Tr. Description").Visible = False
                    .Columns("Customer Name").Width = 300
                Case Else
                    .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns(4).DefaultCellStyle.Format = "N" & NoOfDecimal
                    .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns(5).DefaultCellStyle.Format = "N" & NoOfDecimal
                    .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns(6).DefaultCellStyle.Format = "N" & NoOfDecimal
                    .Columns(2).Visible = False
                    .Columns("Tr. Description").Width = 300
            End Select
        End With
        setComboGrid()
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        If dtTable.Rows.Count = 0 Then Exit Sub
        grdVoucher.DataSource = SearchGrid(dtTable, Trim(txtSearch.Text), cmbSearch.SelectedIndex)
    End Sub

    Private Sub grdVoucher_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellDoubleClick
        RaiseEvent selectTr(Val(grdVoucher.Item("Trid", grdVoucher.CurrentRow.Index).Value & ""), strType)
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        RaiseEvent closetr()
        Me.Close()
    End Sub

    Private Sub cmbbranch_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbbranch.SelectedIndexChanged
        FillGrid()
    End Sub
End Class