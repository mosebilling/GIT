
Public Class DeliverywiseOutstandingFrm
#Region "Class Objet declarations"
    Private _objcmnbLayer As New clsCommon_BL
    Private _objTr As New clsAccountTransaction
#End Region
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fRptFormat As RptFormatfrm
    Private WithEvents ftransfer As TransferToWebFrm
    Private dtOutstanding As DataTable
    Private dtRports As DataTable
    Public iscollection As Boolean
    Private WithEvents fSelect As Selectfrm
    Private chgbyprg As Boolean
    Private dtSetoffTable As DataTable
    Private Sub DeliverywiseOutstandingFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        grdvoucher.DataSource = Nothing
        AddtoCombo(cmbdeliveredBy, "SELECT SManCode FROM SalesmanTb", True, False)
        'If cmbdeliveredBy.Items.Count > 0 Then
        '    cmbdeliveredBy.SelectedIndex = 0
        'End If
        If iscollection Then
            rdocollectionlist.Visible = True
            rdocollectionlist.Top = rdosummary.Top
            rdoinvoicewise.Visible = False
            rdosummary.Visible = False
            rdocollectionlist.Checked = True
            btnupdate.Visible = True
            'fillGrid()
            grpReceipt.Visible = True
            lblName.Text = "Collection"
            Me.Text = "Collection   "
            crtSubVrs(cmbVoucherTp, 2, True)
        Else
            rdocollectionlist.Visible = False
            rdoinvoicewise.Visible = True
            rdosummary.Visible = True
            rdosummary.Checked = True
            grpReceipt.Visible = False
        End If
        Timer1.Enabled = True
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        fillGrid()
    End Sub
    Private Sub loadCollection()
        Try
            Dim _objweb As New webDatalayer
            dtOutstanding = Nothing
            Dim str As String
            str = "select  invNo RVNO,convert(varchar,trdate,100)trdate,customerName,isnull([Inv No],0)[Inv No],[Due Amount],amount,paymentmode [P Mode],deliveredorcollectedby,moseCustomerid customerid,trid," & _
                                                    "'" & DateValue(cldrStartDate.Value) & "' dtFrom,'" & DateValue(cldrEnddate.Value) & "' todate,1 lnk " & _
                                                    " from DeliveryOrCollectionTb " & _
                                                    "left join (select invno [Inv No],amount [Due Amount],trid Ttrid from DeliveryOrCollectionTb ) invTr On DeliveryOrCollectionTb.collectionInvTrid=invTr.Ttrid " & _
                                                    "left join CashCustomerTb on CashCustomerTb.custid=DeliveryOrCollectionTb.customerid " & _
                                                    "where DeliveryOrCollectionTb.companyid=" & webIntegrationid & " and trtype='RV' " & _
                                                   IIf(cmbdeliveredBy.Text = "", "", "AND deliveredOrCollectedBy='" & cmbdeliveredBy.Text & "'") & _
                                                    " and isnull(isupdated,0)=0 and " & _
                                                    "convert(date,trdate)>='" & Format(CDate(cldrStartDate.Value), "yyyy/MM/dd") & "'" & _
                                                    "and convert(date,trdate)<='" & Format(CDate(cldrEnddate.Value), "yyyy/MM/dd") & "'"
            dtOutstanding = _objweb.returnDatatable(str)
            grdvoucher.DataSource = Nothing
            If Not dtOutstanding Is Nothing Then
                grdvoucher.DataSource = SearchGrid(dtOutstanding, Trim(cmbVoucherTp.Text), 5, False)
                dtRports = SearchGrid(dtOutstanding, Trim(cmbVoucherTp.Text), 5, False)
            End If
            'grdvoucher.DataSource = dtOutstanding
            If Not dtOutstanding Is Nothing Then SetGridHeadRV()

            'If DateValue(cldrStartDate.Value) = DateValue(cldrEnddate.Value) Then
            '    grpReceipt.Visible = True
            '    btnupdate.Visible = True
            'Else
            '    grpReceipt.Visible = False
            '    btnupdate.Visible = False
            'End If
            If grdvoucher.RowCount > 0 Then
                Dim result As Object
                result = dtRports.Compute("SUM(amount)", "")
                Dim total As Double
                total = Val(result.ToString)
                lblTotal.Text = "Total : " & Format(total, numFormat)
                lblTotal.Visible = True
                grpReceipt.Visible = True
                btnupdate.Visible = True
            Else
                'grpReceipt.Visible = False
                'btnupdate.Visible = False
                'lblTotal.Visible = False
            End If
            

        Catch ex As Exception
            MsgBox(ex.Message & vbCrLf & "Server not responding!", MsgBoxStyle.Exclamation)
            btnupdate.Visible = False
        End Try
        
    End Sub
    Private Sub fillGrid()
        If rdocollectionlist.Checked Then
            loadCollection()
        Else
            Dim tp As Integer
            If rdoinvoicewise.Checked Then
                If chknotupdated.Checked Then
                    tp = 4
                ElseIf cmbdeliveredBy.Text = "" Then
                    tp = 5
                Else
                    tp = 1
                End If
            ElseIf rdosummary.Checked Then
                If cmbdeliveredBy.Text = "" Then
                    tp = 6
                Else
                    tp = 3
                End If

            End If
            dtOutstanding = Nothing
            dtOutstanding = _objTr.returnOutstandingForAll(cldrStartDate.Value, cldrEnddate.Value, 0, tp, 0, "Customer", cmbdeliveredBy.Text).Tables(0)
            If rdoinvoicewise.Checked Then
                If Not chkob.Checked Then
                    Dim strquery As String = ""
                    Dim _qurey As EnumerableRowCollection(Of DataRow)
                    _qurey = From data In dtOutstanding.AsEnumerable() Where data("InvPrefix") <> "OB-PRIOR" Select data
                    If _qurey.Count > 0 Then
                        dtOutstanding = _qurey.CopyToDataTable()
                    Else
                        dtOutstanding = dtOutstanding.Clone
                    End If
                End If
            End If
            grdvoucher.DataSource = Nothing
            grdvoucher.DataSource = dtOutstanding
            If rdoinvoicewise.Checked Then
                SetGridHeadInoce()
            Else
                SetGridHead()
            End If
        End If


    End Sub
    Private Sub SetGridHead(Optional ByVal disablecombolisting As Boolean = False)


        With grdvoucher
            SetGridProperty(grdvoucher)
            .Columns("AccDescr").HeaderText = "Customer Name"
            .Columns("opbal").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("opbal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("opbal").Width = 150
            .Columns("opbal").HeaderText = "Opening Balance"

            .Columns("totalInv").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("totalInv").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("totalInv").Width = 150
            .Columns("totalInv").HeaderText = "Invoice Amount"

            .Columns("TotalAmt").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("TotalAmt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("TotalAmt").Width = 150
            .Columns("TotalAmt").HeaderText = "Total Amount"

            .Columns("TotalRV").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("TotalRV").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("TotalRV").Width = 150
            .Columns("TotalRV").HeaderText = "Total RV"

            .Columns("Balance").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Balance").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Balance").Width = 150
            .Columns("Balance").HeaderText = "Balance"

            .Columns("collectedOrDeliveredBy").Visible = True
            .Columns("collectedOrDeliveredBy").HeaderText = "Collection By"
            .Columns("accid").Visible = False
            .Columns("GrpSetOn").Visible = False
            .Columns("lnk").Visible = False
            .Columns("dtFrom").Visible = False
            .Columns("todate").Visible = False

        End With
        resizeGridColumn(grdvoucher, 0)
        If Not disablecombolisting Then setComboGrid()
    End Sub
    Private Sub SetGridHeadInoce(Optional ByVal disablecombolisting As Boolean = False)


        With grdvoucher
            SetGridProperty(grdvoucher)
            .Columns("Account Name").HeaderText = "Customer Name"
            .Columns("InvPrefix").HeaderText = "Invoice"
            .Columns("trdate").HeaderText = "Date"

            .Columns("DEBIT").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("DEBIT").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("DEBIT").Width = 150
            .Columns("DEBIT").HeaderText = "Invoice Amount"

            .Columns("CREDIT").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("CREDIT").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("CREDIT").Width = 150
            .Columns("CREDIT").HeaderText = "Rceived"

            .Columns("Balance").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Balance").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Balance").Width = 150
            .Columns("Balance").HeaderText = "Balance"
            .Columns("Updated").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("lnk").Visible = False
            .Columns("dtFrom").Visible = False
            .Columns("todate").Visible = False
            .Columns("trid").Visible = False
            .Columns("accid").Visible = False
            .Columns("JVNum").Visible = False
            .Columns("Prefix").Visible = False

        End With
        resizeGridColumn(grdvoucher, 2)
        If Not disablecombolisting Then setComboGrid()
    End Sub
    Private Sub SetGridHeadRV(Optional ByVal disablecombolisting As Boolean = False)


        With grdvoucher
            SetGridProperty(grdvoucher)
            .Columns("customerName").HeaderText = "Customer Name"
            .Columns("RVNo").HeaderText = "RV#"
            .Columns("trdate").HeaderText = "Date"
            .Columns("trdate").Width = 120
            .Columns("deliveredorcollectedby").HeaderText = "Collected By"
            .Columns("deliveredorcollectedby").Width = 100
            .Columns("P Mode").Width = 75

            .Columns("amount").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("amount").Width = 150
            .Columns("amount").HeaderText = "Amount"

            .Columns("Due Amount").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Due Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Due Amount").Width = 150

            .Columns("customerid").Visible = False
            .Columns("trid").Visible = False
            .Columns("lnk").Visible = False
            .Columns("dtFrom").Visible = False
            .Columns("todate").Visible = False

        End With
        resizeGridColumn(grdvoucher, 2)
        If Not disablecombolisting Then setComboGrid()
    End Sub
    Private Sub setComboGrid()
        chgbyprg = True
        Dim i As Integer = 0
        cmbSearch.Items.Clear()
        For i = 0 To 4
            cmbSearch.Items.Add(grdvoucher.Columns(i).HeaderText)

        Next
        If cmbSearch.Items.Count > 1 Then cmbSearch.SelectedIndex = 2
        txtSearch.Focus()
        'If cmbSearch.Items.Count >= cmbShowIndex Then cmbSearch.SelectedIndex = cmbShowIndex
        chgbyprg = False
    End Sub
    Private Sub cmbdeliveredBy_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbdeliveredBy.SelectedIndexChanged
        fillGrid()

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        rdosummary_Click(rdocollectionlist, New System.EventArgs)
        If rdoinvoicewise.Checked Or rdocollectionlist.Checked Then
            resizeGridColumn(grdvoucher, 2)
        Else
            resizeGridColumn(grdvoucher, 0)
        End If
    End Sub

    Private Sub rdosummary_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdosummary.CheckedChanged
        'btnupdate.Visible = Not rdosummary.Checked
    End Sub


    Private Sub rdosummary_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdosummary.Click, rdoinvoicewise.Click, rdocollectionlist.Click

        If rdocollectionlist.Checked Then
            Label28.Text = "Collected By"
            btnupdate.Text = "Create RV"
            getRVNumber()
            btnApply.Enabled = False
        Else
            Label28.Text = "Delivered By"
            btnupdate.Text = "Update For Collection"

            grpReceipt.Visible = False
            'btnupdate.Visible = True
            btnApply.Enabled = True
        End If
        fillGrid()
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Dim RptType As String = ""
        Dim RptCaption As String = ""
        If rdosummary.Checked Then
            RptType = "DSUM"
        ElseIf rdoinvoicewise.Checked Then
            RptType = "DINV"
        End If
        fRptFormat = New RptFormatfrm
        fRptFormat.RptType = RptType
        fRptFormat.ShowDialog()
        fRptFormat = Nothing
    End Sub

    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        PrepareReport(RptFlName, RptCaption, forPrint)
    End Sub
    Public Sub PrepareReport(ByVal FileName As String, ByVal RptCaption As String, ByVal forpint As Boolean)
        If Not frm Is Nothing Then
            frm.Close()
            frm = New ReprtviewNEWfrm
        Else
            frm = New ReprtviewNEWfrm
        End If
        Dim ds As New DataSet
        If dtRports Is Nothing Then
            If rdosummary.Checked Then
                ds = _objTr.returnOutstandingForAll(cldrStartDate.Value, cldrEnddate.Value, 0, 3, 0, "Customer", cmbdeliveredBy.Text)
            ElseIf rdoinvoicewise.Checked Then
                ds = _objTr.returnOutstandingForAll(cldrStartDate.Value, cldrEnddate.Value, 0, 1, 0, "Customer", cmbdeliveredBy.Text)
            End If
        Else
            ds.Tables.Add(dtRports)
        End If

        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = fMainForm
        frm.Text = RptCaption
        frm.Show()
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        grdvoucher.DataSource = SearchGrid(dtOutstanding, Trim(txtSearch.Text), cmbSearch.SelectedIndex, Not chkSearch.Checked)
        dtRports = SearchGrid(dtOutstanding, Trim(txtSearch.Text), cmbSearch.SelectedIndex, Not chkSearch.Checked)
        If rdoinvoicewise.Checked Then
            SetGridHeadInoce(True)
        ElseIf rdosummary.Checked Then
            SetGridHead(True)
        Else
            SetGridHeadRV(True)
        End If
    End Sub

    Private Sub chknotupdated_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chknotupdated.CheckedChanged
        If enableWebIntegration Then
            btnupdate.Visible = chknotupdated.Checked
        Else
            btnupdate.Visible = False
        End If
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        If rdocollectionlist.Checked Then
            If cmbdeliveredBy.Text = "" Then
                MsgBox("You cannot create RV Without DeliveryBy ", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            If grdvoucher.RowCount > 0 Then
                UpdateReceipt()
            Else
                MsgBox("Collection not found", MsgBoxStyle.Exclamation)
            End If

        Else
            ftransfer = New TransferToWebFrm
            ftransfer.DeliveredBy = cmbdeliveredBy.Text
            ftransfer.dtTable = dtOutstanding
            ftransfer.updateAllInvoice = chkall.Checked
            ftransfer.typeofTransfer = IIf(rdoinvoicewise.Checked, 5, 7)
            ftransfer.Show(fMainForm)
        End If
    End Sub

    Private Sub ftransfer_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles ftransfer.FormClosed
        ftransfer = Nothing
        fillGrid()
    End Sub
    Private Sub getRVNumber(Optional ByVal pmode As Integer = 0)
        Dim vrPrefix As String = ""
        Dim vrVoucherNo As Long
        Dim vrAccountNo1 As Long
        Dim vrAccountNo2 As Long
        Dim dtTable As DataTable
        getVrsDet(0, "RV", vrPrefix, vrVoucherNo, vrAccountNo1, vrAccountNo2)
        txtrvprefix.Text = vrPrefix
        txtrvnumber.Text = vrVoucherNo
        If pmode = 0 Then 'cash
            dtTable = _objcmnbLayer._fldDatatable("SELECT accid,AccDescr FROM AccMast WHERE AccSetId Like '%05%'")
        ElseIf pmode = 1 Then 'bank
            dtTable = _objcmnbLayer._fldDatatable("SELECT accid,AccDescr FROM AccMast WHERE AccSetId Like '%06%'")
        Else 'pdc
            dtTable = _objcmnbLayer._fldDatatable("SELECT accid,AccDescr FROM AccMast WHERE AccSetId Like '%07%'")
        End If
        If dtTable.Rows.Count > 0 Then
            txtPcash.Tag = dtTable(0)("accid")
            txtPcash.Text = dtTable(0)("AccDescr")
        End If
    End Sub
    Private Sub UpdateReceipt()
        
        Dim LinkNo As Long
        If MsgBox("Do you want post Receipt?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
chkagain:
        If Not CheckNoExists(txtrvprefix.Text, Val(txtrvnumber.Text), "RV", "Accounts") Then
            If MsgBox("Payment No alreary exist! Do you want to fill next?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                txtrvnumber.Text = Val(txtrvnumber.Text) + 1
                GoTo chkagain
            Else
                Exit Sub
            End If
        End If
        'LinkNo = Val(txtrvnumber.Tag)
        '_objcmnbLayer._saveDatawithOutParm("DELETE FROM AccTrDet WHERE LinkNo=" & LinkNo)
        setAcctrCmnRVValue(0, LinkNo)
        LinkNo = Val(_objTr.SaveAccTrCmn())
        _objcmnbLayer._saveDatawithOutParm("Update AccTrCmn set collectedOrDeliveredBy='" & cmbdeliveredBy.Text & "' where LinkNo=" & LinkNo)
        Dim drAmt As Double
        Dim cramt As Double
        Dim i As Integer
        'debit entry
        With grdvoucher
            If Not chksinglerv.Checked Then
                If grdvoucher.CurrentRow Is Nothing Then
                    MsgBox("Please select transaction", MsgBoxStyle.Exclamation)
                    GoTo ex
                End If
                i = grdvoucher.CurrentRow.Index
                drAmt = CDbl(.Item("amount", i).Value)
            Else
                For i = 0 To .RowCount - 1
                    cramt = CDbl(.Item("amount", i).Value)
                    drAmt = drAmt + cramt
                Next
            End If

        End With
        setAcctrRVDetValue(LinkNo, Val(txtPcash.Tag), "", drAmt, 0)
        _objTr.saveAccTrans()

        'credit entry
        Dim _objweb As New webDatalayer
        With grdvoucher
            If Not chksinglerv.Checked Then
                If grdvoucher.CurrentRow Is Nothing Then
                    MsgBox("Please select transaction", MsgBoxStyle.Exclamation)
                    GoTo ex
                End If
                i = grdvoucher.CurrentRow.Index
                cramt = CDbl(.Item("amount", i).Value)
                If Trim(.Item("Inv No", i).Value & "") = "" Then
                    ldTrans(Val(.Item("customerid", i).Value))
                    setoFFBal(cramt)
                    updateRVCreditAccount(Val(.Item("customerid", i).Value), Val(txtPcash.Tag), LinkNo, cramt * -1)
                Else
                    setAcctrRVDetValue(LinkNo, Val(.Item("customerid", i).Value), Trim(.Item("Inv No", i).Value & ""), cramt * -1, Val(txtPcash.Tag))
                End If
                _objweb.saveDataToOnline("Update DeliveryOrCollectionTb set isupdated=1,moseRvNumber='" & txtrvprefix.Text & txtrvnumber.Text & "' where trid=" & Val(.Item("trid", i).Value))
            Else
                For i = 0 To .RowCount - 1
                    cramt = CDbl(.Item("amount", i).Value)
                    'ldTrans(Val(.Item("customerid", i).Value))
                    'setoFFBal(cramt)
                    'updateRVCreditAccount(Val(.Item("customerid", i).Value), Val(txtPcash.Tag), LinkNo, cramt * -1)
                    If Trim(.Item("Inv No", i).Value & "") = "" Then
                        ldTrans(Val(.Item("customerid", i).Value))
                        setoFFBal(cramt)
                        updateRVCreditAccount(Val(.Item("customerid", i).Value), Val(txtPcash.Tag), LinkNo, cramt * -1)
                    Else
                        setAcctrRVDetValue(LinkNo, Val(.Item("customerid", i).Value), Trim(.Item("Inv No", i).Value & ""), cramt * -1, Val(txtPcash.Tag))
                        _objTr.saveAccTrans()
                    End If
                    _objweb.saveDataToOnline("Update DeliveryOrCollectionTb set isupdated=1,moseRvNumber='" & txtrvprefix.Text & txtrvnumber.Text & "' where trid=" & Val(.Item("trid", i).Value))
                Next
            End If

        End With
       
        txtrvprefix.Text = SetNextVrNo(txtrvnumber, 0, "RV", "JvType = 'RV' AND JvNum = ", True, True, True)

        MsgBox("Receipt Updated", MsgBoxStyle.Information)
        MsgBox("Balance on web will not be updated until perform 'Update to Online' on Financial Status", MsgBoxStyle.Information)
        fillGrid()
ex:
    End Sub
    Private Sub setAcctrCmnRVValue(ByVal InvTrid As Long, ByVal LinkNo As Long)
        _objTr.JVType = "RV"
        _objTr.JVDate = DateValue(cldrdate.Value)
        _objTr.PreFix = txtrvprefix.Text
        _objTr.JVNum = Val(txtrvnumber.Text)
        _objTr.JVTypeNo = getVouchernumber("RVO")
        _objTr.UserId = CurrentUser
        _objTr.MchName = MACHINENAME
        _objTr.CrtDtTm = DateValue(Date.Now)
        _objTr.TypeNo = cmbVoucherTp.Tag
        _objTr.VrDescr = ""
        _objTr.VrDescr = ""
        _objTr.UserId = ""
        _objTr.IsModi = IIf(Val(txtrvnumber.Tag) > 0, 2, 0)
        _objTr.LinkNo = LinkNo

    End Sub
    Private Sub setAcctrRVDetValue(ByVal lnkNo As Long, ByVal AccountNo As Long, ByVal Reference As String, ByVal amount As Double, ByVal CustAcc As Integer)
        Dim Dt As Date = DateValue(Date.Now)
        With _objTr
            .trLinkNo = lnkNo
            .AccountNo = AccountNo
            .Reference = Reference
            .EntryRef = "Amount Received By " & cmbdeliveredBy.Text & " agianst " & Reference
            .DealAmt = amount
            .FCAmt = amount
            .JobCode = ""
            .JobStr = ""
            .CurrRate = 1
            .CurrencyCode = ""
            .TrInf = 0
            .OthCost = 0
            .TermsId = ""
            .CustAcc = CustAcc
            .AccWithRef = ""
            .DueDate = Dt
            .DocDate = Dt
            .SuppInvDate = Dt
        End With
    End Sub

    Private Sub rdocollectionlist_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdocollectionlist.CheckedChanged

    End Sub

    Private Sub rdoinvoicewise_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoinvoicewise.CheckedChanged
        chknotupdated.Visible = rdoinvoicewise.Checked
        chkob.Visible = rdoinvoicewise.Checked
    End Sub

    Private Sub txtPcash_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPcash.KeyDown
        If e.KeyCode = Keys.F2 Then
            ldSelect(15)
        End If
    End Sub
    Private Sub ldSelect(ByVal BVal As Single)
        fSelect = New Selectfrm
        'nSelect = BVal
        SetForm(fSelect, BVal)
        If BVal = 1 Or BVal = 0 Then
            fSelect.Width = 742
            fSelect.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
        Else
            fSelect.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedToolWindow
            fSelect.Width = 425
        End If
        fSelect.Show()
    End Sub
    Private Sub fSelect_doSelect(ByVal KeyId As String, ByVal strFld1 As String, ByVal strFld2 As String) Handles fSelect.doSelect
        txtPcash.Text = strFld1
        txtPcash.Tag = KeyId
    End Sub

    Private Sub fSelect_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fSelect.FormClosed
        fSelect = Nothing
    End Sub

    Private Sub grdvoucher_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdvoucher.CellClick
        setRv()
    End Sub
    Private Sub setRv()
        If chksinglerv.Checked Then Exit Sub
        cldrdate.Value = DateValue(grdvoucher.Item("trdate", grdvoucher.CurrentRow.Index).Value)
        Dim pmode As Integer
        Dim grdstring As String = UCase(Trim(grdvoucher.Item("p mode", grdvoucher.CurrentRow.Index).Value & ""))
        If grdstring = "CASH" Or grdstring = "" Then
            pmode = 0
        ElseIf grdstring = "BANK" Then
            pmode = 1
        End If
        getRVNumber(pmode)
    End Sub

    Private Sub cmbSearch_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSearch.SelectedIndexChanged
        txtSearch.Focus()
    End Sub

    Private Sub cmbVoucherTp_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVoucherTp.SelectedIndexChanged
        Dim vrPrefix As String = ""
        Dim vrVoucherNo As Long
        Dim vrAccountNo1 As Long
        Dim vrAccountNo2 As Long
        With cmbVoucherTp
            If .Items.Count > 0 Then
                If .SelectedIndex < 0 Then .SelectedIndex = 0
                cmbVoucherTp.Tag = getvrsId(cmbVoucherTp.Text, 2)
                getVrsDet(Val(cmbVoucherTp.Tag), "RV", vrPrefix, vrVoucherNo, vrAccountNo1, vrAccountNo2)
            Else
                getVrsDet(0, "RV", vrPrefix, vrVoucherNo, vrAccountNo1, vrAccountNo2)
            End If

        End With
        txtrvnumber.Text = vrVoucherNo
        txtrvprefix.Text = vrPrefix
        Dim dtAcc As DataTable
        dtAcc = _objcmnbLayer._fldDatatable("SELECT Alias,AccDescr,GrpSetOn,GrpSetOn,accid FROM AccMast " & _
                                            "LEFT JOIN S1AccHd ON AccMast.S1AccId=S1AccHd.S1AccId WHERE accid=" & vrAccountNo1)
        If dtAcc.Rows.Count > 0 Then
            txtPcash.Text = dtAcc(0)("AccDescr")
            txtPcash.Tag = dtAcc(0)("accid")
        End If
        If Not dtOutstanding Is Nothing Then
            grdvoucher.DataSource = SearchGrid(dtOutstanding, Trim(cmbVoucherTp.Text), 4, False)
            dtRports = SearchGrid(dtOutstanding, Trim(cmbVoucherTp.Text), 4, False)
            SetGridHeadRV(True)
            Dim result As Object
            result = dtRports.Compute("SUM(amount)", "")
            Dim total As Double
            total = Val(result.ToString)
            lblTotal.Text = "Total : " & Format(total, numFormat)
            lblTotal.Visible = True
        End If
    End Sub
    Private Sub ldTrans(ByVal accid As Long)
        Dim dttable As DataTable
        dttable = _objTr.returnldTrans(accid, 0, 0)
        If Not dtSetoffTable Is Nothing Then
            If dtSetoffTable.Rows.Count > 0 Then dtSetoffTable.Clear()
        Else
            dtSetoffTable = New DataTable
            CreateSetoffTable(dtSetoffTable)
        End If
        If dttable.Rows.Count > 0 Then
            Dim i As Integer
            Dim Bal As Double
            Dim Credit As Double
            Dim Debit As Double
            Dim Added As Boolean
            Dim PRef As String = ""
            Dim dtRow As DataRow
            dtRow = dtSetoffTable.NewRow
            For i = 0 To dttable.Rows.Count - 1
                If dttable(i)("DealType") = "C" Then GoTo NXT
                Dim s As String = UCase(dttable(i)("Reference"))

                If UCase(PRef) <> UCase(dttable(i)("Reference")) Then
                    If Added Then
                        dtSetoffTable.Rows.Add(dtRow)
                        dtRow = dtSetoffTable.NewRow
                    End If
                    Added = True
                    Bal = 0 'IIf(Rs!DealType = "D", 1, -1) * Rs!DealAmt
                    Debit = 0
                    Credit = 0
                    If Val(dttable(i)("CurrRate") & "") = 0 Then dttable(i)("CurrRate") = 1
                    If Val(dttable(i)("Amt") & "") = 0 Then dttable(i)("Amt") = 0
                    PRef = dttable(i)("Reference")
                    dtRow("JVDate") = dttable(i)("JVDate")
                    dtRow("Reference") = dttable(i)("Reference")
                    dtRow("EntryRef") = dttable(i)("EntryRef")
                    dtRow("CurrencyCode") = Trim(dttable(i)("CurrencyCode") & "")
                    dtRow("Rate") = dttable(i)("CurrRate")
                    dtRow("jvnum") = dttable(i)("jvnum")
                    dtRow("LpoNo") = dttable(i)("LpoNo")
                    'dtRow("LpoDate") = dttable(i)("LpoDate") 
                    dtRow("JobCode") = dttable(i)("JobCode")
                    If IsDBNull(dttable(i)("Fcdec")) Then
                        dtRow("Fcdec") = 2
                    Else
                        dtRow("Fcdec") = dttable(i)("Fcdec")
                    End If
                End If
                Bal = Bal + IIf(dttable(i)("DealType") = "D", 1, -1) * dttable(i)("Amt")
                Debit = Debit + IIf(dttable(i)("DealType") = "D", 1, 0) * dttable(i)("Amt")
                Credit = Credit + IIf(dttable(i)("DealType") = "C", 1, 0) * dttable(i)("Amt")
                dtRow("Type") = IIf(Bal < 0, "D", "C") & "r"
                dtRow("Tag") = ""
                If Val(dttable(i)("CurrRate") & "") = 0 Then dttable(i)("CurrRate") = 1
                dtRow("Balance") = Format(Bal / CDbl(dttable(i)("CurrRate")), "#,##" & numFormat)
                dtRow("InvAmt") = Format(IIf(Credit > Debit, Debit, Debit) / CDbl(dttable(i)("CurrRate")), "#,##" & numFormat)
                dtRow("SetOffAmt") = Format(IIf(Credit > Debit, Credit, Credit) / CDbl(dttable(i)("CurrRate")), "#,##" & numFormat)
NXT:
            Next
            If Added Then dtSetoffTable.Rows.Add(dtRow)
        End If
        dtSetoffTable.DefaultView.Sort = "JVDate ASC"
ext:
    End Sub
    Private Sub setoFFBal(ByVal paidAmt As Double)
        Dim i As Integer
        For i = 0 To dtSetoffTable.Rows.Count - 1
            If paidAmt > dtSetoffTable(i)("Balance") Then
                dtSetoffTable(i)("Assign") = Format(CDbl(dtSetoffTable(i)("Balance")), numFormat)
                paidAmt = Math.Round(paidAmt - CDbl(dtSetoffTable(i)("Balance")), 2)
            Else
                dtSetoffTable(i)("Assign") = Format(paidAmt, numFormat)
                paidAmt = 0
                GoTo ext
            End If
        Next
ext:
        If paidAmt > 0 Then
            Dim dtRow As DataRow
            dtRow = dtSetoffTable.NewRow
            dtRow("Reference") = "ON/AC"
            dtRow("Assign") = paidAmt
            dtSetoffTable.Rows.Add(dtRow)
        End If
    End Sub

    Private Sub updateRVCreditAccount(ByVal CreditAcc As Integer, ByVal debitAcc As Integer, ByVal LinkNo As Long, ByVal paidAmt As Double)
        Dim i As Integer
        If paidAmt > 0 And dtSetoffTable.Rows.Count = 0 Then
            'setAcctrDetValue(LinkNo, CreditAcc, "ON/AC", "Advance Collection", paidAmt * -1, "", "", DateValue("01/01/1950"), debitAcc)
            '_objTr.saveAccTrans()
        Else
            For i = 0 To dtSetoffTable.Rows.Count - 1
                If Val(dtSetoffTable(i)("Assign") & "") > 0 Then
                    setAcctrRVDetValue(LinkNo, CreditAcc, Trim(dtSetoffTable(i)("Reference")), CDbl(dtSetoffTable(i)("Assign")) * -1, debitAcc)
                    _objTr.saveAccTrans()
                End If
            Next
        End If

    End Sub
End Class