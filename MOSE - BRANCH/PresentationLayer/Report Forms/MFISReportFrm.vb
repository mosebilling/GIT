Public Class MFISReportFrm
    Private chgbyprg As Boolean
    Dim _objcmnbLayer As New clsCommon_BL
    Private _objReport As New clsReport_BL
    Private dtTable As DataTable
    Private WithEvents fwait As WaitMessageFrm
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fRptFormat As RptFormatfrm
    Private dtMemberRv As DataTable
    Private Sub MFISReportFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Timer1.Enabled = True
    End Sub
    Public Sub fillGrid()
        chgbyprg = True
        lblName.Text = "Sales Invoice Report"
        txtSearch.Text = ""
        If IsDate(cldrStartDate.Value) And IsDate(cldrEnddate.Value) Then
            If DateValue(cldrStartDate.Value) > DateValue(cldrEnddate.Value) Then GoTo Invaliddate
        Else
Invaliddate:
            MsgBox("Invalid Date", MsgBoxStyle.Information)
            Exit Sub
        End If
        Dim strcondition As String = " and trtype='MIS' and Trdate>='" & Format(DateValue(cldrStartDate.Value), "yyyy-MM-dd") & "' and  Trdate<='" & Format(DateValue(cldrEnddate.Value), "yyyy-MM-dd") & "' "
        Dim strsql As String = "Select  convert(varchar(12), case when prefix=''  then '' else '-' end +  invNo ) as [Inv No] , TrDate [Tr.Date] ,Alias [Cust. Code]," & _
                        "CASE WHEN ISNULL(CashCustName,'')='' THEN AccDescr ELSE CashCustName END [Customer Name]," & _
                        "NetAmt [Net Total],rvamt,TrRefNo [Ref. No],SlsManId," & _
                        "TrDescription  [Tr. Description],UserId [Created By],convert(varchar,CrtDt,100) [Created On],TrId from ( select trtype, ItmInvCmntb.prefix, invNo ,TrDate ,InvAmt,Discount,Discount1," & _
                        "OthCost,AccDescr ,TrRefNo,TrDescription,Alias ,UserId,[Job Code],ItmInvCmntb.Termsid,LPO,ItmInvCmntb.TrId,rndoff,taxAmt,CashCustName,ItmInvCmntb.isTaxInvoice,ItmInvCmntb.SlsManId,CrtDt,ItmInvCmntb.isB2B,FCRate,NetAmt,[Voucher Name][P Mode],ItmInvCmntb.brid,case when isnull(iscashinvoiceonly,0)=1 then 'Cash' else 'Credit'end Ctype,isnull(rvamt,0)rvamt  from ItmInvCmntb " & _
                        "LEFT JOIN PreFixTb ON ItmInvCmntb.InvTypeNo=PreFixTb.ID " & _
                         " LEFT JOIN (SELECT Trid,SUM((TrQty*(UnitCost+UnitOthCost))-ItemDiscount)InvAmt,sum(taxAmt+isnull(cessAmt,0)) taxAmt FROM ItmInvTrTb GROUP BY Trid) Tr ON  ItmInvCmntb.Trid=Tr.Trid" & _
                         " left join Accmast on ItmInvCmnTb.CSCode=Accmast.accid " & _
                         "left join (select sum(dealamt*-1)rvamt,reference,AccountNo from acctrdet where DealAmt<0 group by reference,AccountNo )rv on rv.reference=ItmInvCmnTb.trrefno and rv.AccountNo=ItmInvCmnTb.CSCode " & _
                         "where  isnull(invStatus,0)=0   " & _
                        IIf(UsrBr = "", "", " and ItmInvCmntb.Brid='" & UsrBr & "'") & strcondition & " ) as qq  order by isnull(Brid,''), TrDate ,InvNo"

        'vsummary = "select * from (select [Voucher Name][P Mode],sum(netamt)Amount from ItmInvCmntb LEFT JOIN PreFixTb ON ItmInvCmntb.InvTypeNo=PreFixTb.ID  where " & IIf(UsrBr = "", "", " ItmInvCmntb.Brid='" & UsrBr & "' and") & " trtype ='" & ldType & "' and " & _
        '"Trdate>='" & Format(CDate(cldrStartDate.Value), "yyyy-MM-dd") & "' and  Trdate<='" & Format(CDate(cldrEnddate.Value), "yyyy-MM-dd") & "'" & _
        '" GROUP BY  [Voucher Name])tr where isnull(Amount,0)>0"

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
    Private Sub gridTotal(ByVal dt As DataTable)
        If dt.Rows.Count = 0 Then Exit Sub
        Dim drSum As Double
        Dim amt As String
        amt = Trim(dt.Compute("SUM([Net Total])", "") & "")
        If Val(amt) > 0 Then
            drSum = Convert.ToDouble(amt)
        End If
        lblnetsales.Text = Format(drSum, numFormat)
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
            .Columns("Inv No").Width = 75
            .Columns("Tr.Date").Width = 75
            .Columns("Cust. Code").Width = 100
            .Columns("Customer Name").Width = 250
            .Columns("Ref. No").Width = 100
            .Columns("SlsManId").Width = 100
            .Columns("Created By").Width = 100
            .Columns("Created On").Width = 100
            .Columns(8).Visible = False
            .Columns("Net Total").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Net Total").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns("rvamt").HeaderText = "RV Amount"
            .Columns("rvamt").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("rvamt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(.ColumnCount - 1).Visible = False
            If Me.Width >= 1450 Then resizeGridColumn(grdvoucher, 3)
        End With
    End Sub
    Private Sub fwait_triggerEvent() Handles fwait.triggerEvent
        Select Case fwait.triggerType
            Case 1
                fillGrid()
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

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        loadWaite(1)
    End Sub

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        fillGrid()
    End Sub
    Private Sub loadMember(ByVal trid As Long)
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select membername,amount,isnull(rvamt,0)rvamt from MicroFinanceMemberTb " & _
                                         "LEFT JOIN (select sum(rvamount)rvamt,invmemberid from MicroFinanceRVmemberTb group by invmemberid) rv on MicroFinanceMemberTb.memberid=rv.invmemberid " & _
                                         "where trid=" & trid)
        grdmember.DataSource = dt
        With grdmember
            SetGridProperty(grdmember)
            .Columns("membername").HeaderText = "Member Name"
            .Columns("amount").HeaderText = "Amount"
            .Columns("amount").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("amount").DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns("rvamt").HeaderText = "RV"
            .Columns("rvamt").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("rvamt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("rvamt").DefaultCellStyle.Format = "N" & NoOfDecimal
        End With
        resizeGridColumn(grdmember, 0)
    End Sub

    Private Sub grdvoucher_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdvoucher.DoubleClick
        fMainForm.LoadMFIS(Val(grdvoucher.Item(grdvoucher.ColumnCount - 1, grdvoucher.CurrentRow.Index).Value))
    End Sub

    Private Sub grdvoucher_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdvoucher.RowEnter
        Dim trid As Long
        trid = grdvoucher.Item("trid", e.RowIndex).Value
        loadMember(trid)
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Dim RptType As String
        If rdosummary.Checked Then
            RptType = "ISS"
        Else
            RptType = "MFSS"
        End If
        Dim RptCaption As String = ""
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
    Public Sub PrepareReport(ByVal FileName As String, ByVal RptCaption As String, ByVal forpint As Boolean)
        If Not frm Is Nothing Then
            frm.Close()
            frm = New ReprtviewNEWfrm
        Else
            frm = New ReprtviewNEWfrm
        End If
        Dim condition As String = " where trtype='MIS' and Trdate>='" & Format(DateValue(cldrStartDate.Value), "yyyy-MM-dd") & "' and  Trdate<='" & Format(DateValue(cldrEnddate.Value), "yyyy-MM-dd") & "' "
        Dim AccDate1 = Format(CDate(cldrStartDate.Value), "yyyy/MM/dd")
        Dim AccDate2 = Format(CDate(cldrEnddate.Value), "yyyy/MM/dd")
        Dim dateFlds As String = "'" & Format(CDate(cldrStartDate.Value), "dd-MM-yyyy") & "' DateFrom ,'" & Format(CDate(cldrEnddate.Value), "dd-MM-yyyy") & "' DateTo,'' levelGrp "
        Dim ds As DataSet = Nothing
        Dim restuarentgrpBy As String = ""
        Dim rpttype As Integer
        If rdosummary.Checked Then
            rpttype = 1
            ds = _objReport.returnDetailsForReport(condition, dateFlds, "returnInventoryDetailsForReport", rpttype, AccDate1, AccDate2, 5)
        Else
            ds = _objReport.returnMemberwiseRV(DateValue(cldrStartDate.Value))
        End If
        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = fMainForm
        frm.Text = RptCaption
        frm.Show()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        PrepareReport(RptFlName, RptCaption, forPrint)
    End Sub

    Private Sub MFISReportFrm_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Label1.Text = Me.Width
    End Sub
    
End Class