Public Class FinancialStatements

#Region "Class Objet declarations"
    Private _objcmnbLayer As New clsCommon_BL
    Private _objTr As New clsAccountTransaction
#End Region
#Region "Local Variables"
    Private dtTable As DataTable
    Private chgbyprg As Boolean
    Private skipLoadAccounts As Boolean
#End Region
#Region "Constant Variables"
    Private Const constAcTag = 0
    Private Const constAccDescr = 1
    Private Const constAlias = 2
    Private Const constNature = 3
    Private Const constClosingBal = 4
    Private Const constOpnBal = 5
    Private Const constGrpSetOn = 6
    Private Const constAccountNo = 7
#End Region
#Region "Form Objects"
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fRptFormat As RptFormatfrm
    Private WithEvents fReconciliation As Reconciliation
#End Region
#Region "Public Variables"
    Public isreconcil As Boolean
    Public isfromFstatus As Boolean
#End Region

    Private Sub FinancialStatements_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtSeq.Focus()
    End Sub
    Private Sub FinancialStatements_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbcategory.Enabled = Not isreconcil
        'loadAccounts()
        cldrStartDate.Value = Format(DateAdd(DateInterval.Day, firstDateFromToday * -1, DateValue(Date.Now)), DtFormat)
        If isreconcil Then
            ldBYcategory("Bank")
            If userType Then
                btnreconciliation.Visible = getRight(103, CurrentUser)
            Else
                btnreconciliation.Visible = True
            End If
        ElseIf isfromFstatus Then
            'btnLoad_Click(btnLoad, New System.EventArgs)
            If optpoth.Checked Or optroth.Checked Then
                loadAccounts()
            End If
            'GroupBox1.Enabled = False
            cmbcategory.Enabled = False
        Else
            btnreconciliation.Visible = False
            cmbcategory.Text = "ALL"
        End If
        AddtoCombo(cmbdeliveredBy, "SELECT SManCode FROM SalesmanTb", True, False)
        cmbarea.Items.Clear()
        AddtoCombo(cmbarea, "SELECT areacode FROM areatb", True, False)
        If enableSMS Then
            btnsms.Visible = True
        End If
    End Sub
    Private Sub loadAccounts(Optional ByVal donotattach As Boolean = False)
        If optpoth.Checked Then
            dtTable = _objTr.returnStatementGrid(5)
        ElseIf optroth.Checked Then
            dtTable = _objTr.returnStatementGrid(6)
        Else
            dtTable = _objTr.returnStatementGrid(0)
        End If
        If donotattach = False Then
            grdvoucher.DataSource = dtTable
            SetGridHead()
        End If
      
    End Sub
    Private Sub setGridVazhipadu()
        SetGridProperty(grdvoucher)
        With grdvoucher
            .Columns("Code").Width = 75
            .Columns("Vazhipadu Name").Width = 200
            .Columns("Malayalam").Width = 200
            .Columns("Timing").Width = 100
            .Columns("Nada").Width = 100
            .Columns("Rate").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Rate").Width = 100
            .Columns("Rate").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Thirumeni Rate").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Thirumeni Rate").Width = 100
            .Columns("Thirumeni Rate").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Kazhakam Rate").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Kazhakam Rate").Width = 100
            .Columns("Kazhakam Rate").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("accid").Visible = False
            .Columns("lnk").Visible = False
        End With
    End Sub
    Private Sub SetGridHead()
        With grdvoucher
            SetGridProperty(grdvoucher)
            If .ColumnCount = 0 Then Exit Sub
            .Columns(constAcTag).HeaderText = "Tag"
            .Columns(constAcTag).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            '.AutoResizeColumns()
            .Columns(constAcTag).Width = 50

            .Columns(constAccDescr).HeaderText = "Account Name"
            .Columns(constAccDescr).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(constAccDescr).Width = 300

            .Columns(constAlias).HeaderText = "Acc. Id"
            .Columns(constAlias).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(constAlias).Width = 100

            .Columns(constNature).HeaderText = "Nature"
            .Columns(constNature).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(constNature).Width = 75

            .Columns(constClosingBal).HeaderText = "Closing Balance"
            .Columns(constClosingBal).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(constClosingBal).Width = 150
            .Columns(constClosingBal).DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns(constOpnBal).HeaderText = "Open.Balance"
            .Columns(constOpnBal).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(constOpnBal).Width = 100
            .Columns(constOpnBal).DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns(constGrpSetOn).HeaderText = "Category"
            .Columns(constGrpSetOn).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(constGrpSetOn).Width = 75
            .Columns("Group Name").Width = 200
            .Columns("Title Name").Visible = False
            .Columns("MaccId").Visible = False

            .Columns(constAccountNo).HeaderText = "AccountNo"
            .Columns(constAccountNo).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(constAccountNo).Visible = False
            .Columns("bal").Visible = False
            setComboGrid(grdvoucher)
        End With
    End Sub

    Private Sub txtSeq_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeq.TextChanged
        If chgbyprg Then Exit Sub
        grdvoucher.DataSource = SearchGrid(dtTable, Trim(txtSeq.Text), cmbcolms.SelectedIndex, Not chkSearch.Checked)
        'SetGridHead()
    End Sub
    Private Sub setComboGrid(ByVal grd As DataGridView)
        cmbcolms.Items.Clear()
        Dim i As Integer = 0
        With grd
            For i = 0 To grd.ColumnCount - 1
                cmbcolms.Items.Add(.Columns(i).HeaderText)
            Next
            If cmbcolms.Items.Count > 0 Then cmbcolms.SelectedIndex = 1
        End With
    End Sub
    Private Sub ldBYcategory(ByVal grptype As String)
        Dim bDatatable As DataTable
        If dtTable Is Nothing Then loadAccounts(True)
        If dtTable.Rows.Count = 0 Then bDatatable = dtTable.Clone : GoTo nxt
        Dim _qurey As EnumerableRowCollection(Of DataRow)
        If isfromFstatus Then
            If chkremoveadvance.Checked Then
                If grptype = "Customer" Then
                    If cmbarea.Text <> "" Then
                        _qurey = From data In dtTable.AsEnumerable() Where UCase(data("GrpSetOn")) = UCase(grptype) _
                                And Math.Round(data("Closing bal"), 2) > 0 And data("areacode") = cmbarea.Text Select data
                    Else
                        _qurey = From data In dtTable.AsEnumerable() Where UCase(data("GrpSetOn")) = UCase(grptype) _
                                 And Math.Round(data("Closing bal"), 2) > 0 Select data
                    End If
                ElseIf grptype = "Supplier" Then
                    _qurey = From data In dtTable.AsEnumerable() Where UCase(data("GrpSetOn")) = UCase(grptype) And Math.Round(data("Closing bal"), 2) < 0 Select data
                Else
                    _qurey = From data In dtTable.AsEnumerable() Where UCase(data("GrpSetOn")) = UCase(grptype) And Math.Round(data("Closing bal"), 2) <> 0 Select data
                End If
            Else
                _qurey = From data In dtTable.AsEnumerable() Where UCase(data("GrpSetOn")) = UCase(grptype) And Math.Round(data("Closing bal"), 2) <> 0 Select data
            End If
            'If _qurey.Count > 0 Then
            '    dtTable = _qurey.CopyToDataTable()
            'Else
            '    dtTable = dtTable.Clone
            'End If
            'grdvoucher.DataSource = dtTable
            'SetGridHead()
        Else
            If cmbarea.Text <> "" Then
                _qurey = From data In dtTable.AsEnumerable() Where UCase(data("GrpSetOn")) = UCase(grptype) And data("areacode") = cmbarea.Text Select data
            Else
                _qurey = From data In dtTable.AsEnumerable() Where UCase(data("GrpSetOn")) = UCase(grptype) Select data
            End If
        End If
        If _qurey.Count > 0 Then
            bDatatable = _qurey.CopyToDataTable()
        Else
            bDatatable = dtTable.Clone
        End If
nxt:
        grdvoucher.DataSource = bDatatable
        SetGridHead()
    End Sub


    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        If cmbcategory.Text = "All" Then
            loadAccounts()
        ElseIf cmbcategory.Text = "Vazhipadu" And rdovazhipadurate.Checked Then
            dtTable = _objTr.returnVazhipaduRates(0).Tables(0)
            grdvoucher.DataSource = dtTable
            setGridVazhipadu()
        Else
            Dim txt As String = cmbcategory.Text
            rdoAccountBalance.Tag = txt
            If Not skipLoadAccounts Then loadAccounts()
            ldBYcategory(txt)
        End If
        skipLoadAccounts = False
    End Sub
    Private Sub loadVazhipaduRate()

    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Dim RptType As String = ""
        Dim RptCaption As String = ""
        If rdostatement.Checked Then
            RptType = "ST1"
        ElseIf rdooutstanding.Checked Then
            If chkdeliveredBy.Checked Then
                RptType = "STOD"
            Else
                RptType = "STO"
            End If
        ElseIf rdoAccountBalance.Checked Then
            RptType = "ACB"
        ElseIf rdoAddress.Checked Then
            RptType = "ADS"
        ElseIf rdovazhipadurate.Checked Then
            RptType = "VZR"
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

    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        PrepareReport(RptFlName, "", forPrint)
    End Sub
    Public Sub PrepareReport(ByVal FileName As String, ByVal RptCaption As String, ByVal forpint As Boolean)
        If Not frm Is Nothing Then
            frm.Close()
            frm = New ReprtviewNEWfrm
        Else
            frm = New ReprtviewNEWfrm
        End If
        Dim tp As Integer
        Select Case True
            Case rdostatement.Checked
                tp = 0
            Case rdooutstanding.Checked
                tp = 2
            Case rdoAccountBalance.Checked
                If rdoAccountBalance.Tag = "All" Or Trim(rdoAccountBalance.Tag & "") = "" Then
                    If optpoth.Checked Then
                        tp = 4
                    ElseIf optroth.Checked Then
                        tp = 5
                    Else
                        tp = 3
                    End If

                Else
                    tp = 1
                End If
            Case rdoAddress.Checked
                If rdoAccountBalance.Tag = "All" Or Trim(rdoAccountBalance.Tag & "") = "" Then
                    MsgBox("Address List only for groupwise", MsgBoxStyle.Exclamation)
                    Exit Sub
                Else
                    tp = 6
                End If
            Case rdovazhipadurate.Checked
                GoTo cnt
        End Select
        Dim ds As New DataSet
        If tp = 2 Then
            If chkdeliveredBy.Checked Then
                ds = _objTr.returnOutstandingForAll(cldrStartDate.Value, cldrEnddate.Value, Val(grdvoucher.Item(constAccountNo, grdvoucher.CurrentRow.Index).Value), 1, IIf(chkageing.Checked, 1, 0), Trim(rdoAccountBalance.Tag & ""), cmbdeliveredBy.Text)
                GoTo cnt
            End If
        End If
        Dim accid As Integer
        If Not grdvoucher.CurrentRow Is Nothing Then
            accid = Val(grdvoucher.Item(constAccountNo, grdvoucher.CurrentRow.Index).Value)
        End If
        If tp = 0 Then
            Dim str As String
            str = "declare @fromdate date declare @todate date declare @accid bigint declare @brid varchar(50) "
            str = str & " set @fromdate ='" & Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd") & "'"
            str = str & " set @todate ='" & Format(DateValue(cldrEnddate.Value), "yyyy/MM/dd") & "'"
            str = str & " set @accid=" & accid
            str = str & " set @brid = N'" & UsrBr & "'"
            str = str & " select * from( " & _
                "select accmast.accid,areacode,accdescr [account name],alias,countrycode,deptid,branchid ,slsmanid," & _
                "jvtype,jvnum,jvdate,isnull(debit,0)debit,isnull(credit ,0)credit,s1acchd.s1accid ,macchd.maccid," & _
                "macchd.descr [title name],ref reference,s1acchd.descr [group name],grpseton,lpono," & _
                "description,[inv no],typeord ,1 as lnk,address1,address2,address3,address4,phone,fax,@fromdate fromdate,@todate todate from accmast left join " & _
                "(select  @fromdate jvdate,0 jvnum,accmast.accid," & _
                "case when isnull(dealamt,0)+opnbal>0 then isnull(dealamt,0)+opnbal else 0 end debit ," & _
                "case when isnull(dealamt,0)+opnbal<0 then isnull(dealamt,0)+opnbal else 0 end *(-1) credit," & _
                "'ob' jvtype,description,ref,[inv no],lpono,typeord,@fromdate fromdate,@todate todate from accmast " & _
                "left join (select sum(dealamt) dealamt,accountno,'opening balance' " & _
                "description,'' ref,'' [inv no],'' lpono,0 typeord  from acctrdet left join acctrcmn on acctrdet.linkno=acctrcmn.linkno " & _
                "where isnull(approved,0)=0 and jvtype<>'ob' and jvdate<  @fromdate  and case when @brid='' then '' else  isnull(cmnbrid,'') end " & _
                "in ('',@brid) group by accountno)tr	on accmast.accid=tr.accountno " & _
                "union all " & _
                "select jvdate,jvnum,accountno,case when(dealamt>0) then dealamt else 0 end debit,case when(dealamt<0) then (-1)*dealamt else 0 end credit," & _
                "jvtype,entryref description,reference ref,case when(prefix<>'') then prefix+'/'+convert(varchar,jvnum) else convert(varchar,jvnum) end [inv no]," & _
                "lpono,typeord,@fromdate fromdate,@todate todate from acctrdet left join acctrcmn on acctrdet.linkno=acctrcmn.linkno  " & _
                "left join vouchertypenotb on  vouchertypenotb.vrno=acctrcmn.jvtypeno  " & _
                "where isnull(approved,0)=0 and jvtype<>'ob' and jvdate>= @fromdate and jvdate<= @todate  and " & _
                "case when @brid='' then '' else  isnull(cmnbrid,'') end in ('',@brid))tr on accmast.accid=tr.accid " & _
                "left join s1acchd on s1acchd.s1accid=accmast.s1accid left join macchd on macchd.maccid=s1acchd.maccid " & _
                "left join accmastaddr on accmast.accid=accmastaddr.accountno)tr where accid=@accid order by jvdate,typeord,jvnum "
            ds = _objcmnbLayer._ldDataset(str, False)
            'ds = _objTr.returnLEDGERstatementreport(cldrStartDate.Value, cldrEnddate.Value, accid, tp, IIf(chkageing.Checked, 1, 0), Trim(rdoAccountBalance.Tag & ""), IIf(chkremoveadvance.Checked, 1, 0), cmbarea.Text)
        Else

            ds = _objTr.returnStatementReport(cldrStartDate.Value, cldrEnddate.Value, accid, tp, IIf(chkageing.Checked, 1, 0), Trim(rdoAccountBalance.Tag & ""), IIf(chkremoveadvance.Checked, 1, 0), cmbarea.Text)
        End If
        '

cnt:
        If rdooutstanding.Checked Then
            Dim parmDt As DataTable
            parmDt = _objcmnbLayer._fldDatatable(" select 1 Lnk, '" & Format(DateValue(cldrEnddate.Value), "dd/MM/yyyy") & "' As dtAsOn, 30" & _
                                                                    " As Ag1, 60 As Ag2, 90 As Ag3, " & _
                                                                    "120 As Ag4, '0' As SepPage, ''" & _
                                                                    " As WiseFld, 'Ageing based on  Invoice Date.'" & _
                                                                    " As Msg , '" & cldrStartDate.Value & "' As FDate,0 as IsPeriod,'' as Isadv From CompanyTb")
            ds.Tables.Add(parmDt)
        ElseIf rdovazhipadurate.Checked Then
            ds = _objTr.returnVazhipaduRates(0)
        End If
        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = fMainForm
        frm.Text = IIf(RptCaption = "", "Financial Reports", RptCaption)
        frm.Show()
    End Sub

    Private Sub btnreconciliation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreconciliation.Click
        If grdvoucher.RowCount = 0 Then Exit Sub
        fReconciliation = New Reconciliation
        fReconciliation.txtbankname.Text = grdvoucher.Item(1, grdvoucher.CurrentRow.Index).Value
        fReconciliation.txtbankname.Tag = Val(grdvoucher.Item(7, grdvoucher.CurrentRow.Index).Value)
        fReconciliation.cldrdateFrom.Value = cldrStartDate.Value
        fReconciliation.clrDateTo.Value = cldrEnddate.Value
        fReconciliation.GetBankCode()
        fReconciliation.MdiParent = fMainForm
        fReconciliation.Show()
        fReconciliation = Nothing

    End Sub

    Private Sub cmbcategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbcategory.SelectedIndexChanged
        If cmbcategory.Text = "ALL" Then
            loadAccounts()
        Else
            ldBYcategory(cmbcategory.Text)
        End If
        lblarea.Visible = False
        cmbarea.Visible = False
        skipLoadAccounts = True
        btnrv.Visible = False
        rdoAccountBalance.Tag = cmbcategory.Text
        If cmbcategory.Text = "Bank" Then
            If userType Then
                btnreconciliation.Visible = getRight(103, CurrentUser)
            Else
                btnreconciliation.Visible = True
            End If
        ElseIf cmbcategory.Text = "Customer" Then
            lblarea.Visible = True
            cmbarea.Visible = True
            btnrv.Visible = True
            btnrv.Text = "Create RV"
        ElseIf cmbcategory.Text = "Supplier" Then
            btnrv.Visible = True
            btnrv.Text = "Create PV"
        Else
            btnreconciliation.Visible = False
        End If
        If cmbcategory.Text = "P.D.C.(I)" Or cmbcategory.Text = "P.D.C.(R)" Then
            rdopdclist.Visible = True
        Else
            rdopdclist.Visible = False
        End If
        If cmbcategory.Text = "Vazhipadu" Then
            rdovazhipadurate.Visible = True
            rdovazhipadurate.Top = rdopdclist.Top
        Else
            rdovazhipadurate.Visible = False
        End If
    End Sub


    Private Sub btnsms_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsms.Click
        Dim accid As Integer
        If grdvoucher.CurrentRow Is Nothing Then Exit Sub
        accid = Val(grdvoucher.Item(constAccountNo, grdvoucher.CurrentRow.Index).Value)
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select isnull(Phone,'')Phone from AccMastAddr where AccountNo=" & accid)
        If dt.Rows.Count > 0 Then
            If dt(0)("Phone") = "" Then
                MsgBox("Phone number not Found", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            Dim frm As New SendShortSMSFrm
            With frm
                .txtparty.Text = grdvoucher.Item(constAccDescr, grdvoucher.CurrentRow.Index).Value
                .txtphone.Text = dt(0)("Phone")
                dt = _objcmnbLayer._fldDatatable("SELECT CompName FROM CompanyTb")
                Dim CompanyName As String = ""
                If dt.Rows.Count > 0 Then
                    CompanyName = dt(0)(0)
                End If
                .txtcontent.Text = "Dear " & .txtparty.Text & "," & vbCrLf & _
                                    "Your outstanding balace is " & Format(CDbl(grdvoucher.Item(constClosingBal, grdvoucher.CurrentRow.Index).Value), numFormat) & "," & vbCrLf & _
                                    CompanyName
                .ShowDialog()

            End With
        Else
            MsgBox("Phone number not Found", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
    End Sub

    
    Private Sub rdoAccountBalance_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoAccountBalance.CheckedChanged
        chgbyprg = True
        chkremoveadvance.Visible = rdoAccountBalance.Checked
        chgbyprg = False
    End Sub

    Private Sub chkremoveadvance_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkremoveadvance.Click
        If chgbyprg Then Exit Sub
        ldBYcategory(cmbcategory.Text)
    End Sub

    Private Sub cmbarea_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbarea.SelectedIndexChanged
        ldBYcategory(cmbcategory.Text)
    End Sub

    Private Sub btnrv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrv.Click
        If cmbcategory.Text = "Customer" Then
            fMainForm.LoadRV(0, grdvoucher.Item(constAccDescr, grdvoucher.CurrentRow.Index).Value)
        ElseIf cmbcategory.Text = "Supplier" Then
            fMainForm.LoadPV(0, grdvoucher.Item(constAccDescr, grdvoucher.CurrentRow.Index).Value)
        End If
    End Sub
End Class