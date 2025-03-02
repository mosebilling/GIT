Public Class UsedcarFrm
    Private chgbyprg As Boolean
    Private chcar As Boolean
    Private carid As Long
    Private SrchText As String
    Private _srchIndexId As Byte
    Private _srchTxtId As Integer
    Private _srchOnce As Boolean
    Private MyActiveControl As New Object
    Private lstKey As Integer
    Private isset As Boolean
    Private dtTable As DataTable
    Private dtTableJob As DataTable
    Private rptTable As DataTable
    Private WithEvents fpInvoice As PurchaseInvoiceFrm
    Private Const ConstInvNo = ConstBatchQty + 1
    Public isModi As Boolean
    Private ChgId As Boolean
    Private chgPost As Boolean
    Private itemid As Long


#Region "Class Objects"
    Private _objJob As clsJob
    Private _objcmnbLayer As New clsCommon_BL
    Private _objInv As New clsInvoice
    Private _objTr As New clsAccountTransaction
    Private _objvh As New clsVechicle
#End Region
#Region "Form Objects"
    Private WithEvents fMList As Mlistfrm
    Private WithEvents fRptFormat As RptFormatfrm
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fDelivery As New JobDelivery
    Private WithEvents fInvoice As SalesInvoice
    Private WithEvents fCrtAcc As CreateAccNew
    Private WithEvents fProductEnquiry As ItmEnqry
    Private WithEvents fselectJob As JobEnqryFrm
    Private WithEvents festmate As CustomerQuotation
    Private WithEvents fproductMast As ItemMastFrm
    Private WithEvents fwait As WaitMessageFrm
    Private WithEvents fDoc As DocumentView
    Private WithEvents fPpayment As PreviousPaymentFrm
#End Region

    Private Sub txtcustomer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtsuppler.KeyDown, txtplateno.KeyDown, txtcustomer.KeyDown
        lstKey = e.KeyCode
        Dim myctrl As TextBox
        myctrl = sender
        If e.KeyCode = Keys.Return Then
            If myctrl.Name = "txtcustomer" Then
                btnupdate.Focus()
            Else
                SendKeys.Send("{TAB}")
            End If
            Exit Sub
        End If
        If myctrl.Name = "txtcustomer" Or myctrl.Name = "txtplateno" Or myctrl.Name = "txtsuppler" Then
            If e.KeyCode = Keys.Up Or e.KeyCode = Keys.PageUp Then
                If fMList.Visible Then
                    fMList.MoveUp(myctrl.Text)
                    Exit Sub
                End If
            ElseIf e.KeyCode = Keys.Down Or e.KeyCode = Keys.PageDown Then
                If Not fMList Is Nothing Then
                    If fMList.Visible Then
                        fMList.MoveDown(myctrl.Text)
                        Exit Sub
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub txtcarname_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcarname.TextChanged, txtmodel.TextChanged, txtchasisno.TextChanged, txtengineno.TextChanged
        If chgbyprg Then Exit Sub
        chgPost = True
        chcar = True
    End Sub
    Private Sub txtcarname_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtcarname.KeyDown, txtmodel.KeyDown, txtchasisno.KeyDown, txtengineno.KeyDown, txtlastKM.KeyDown
        Dim myctrl As TextBox
        myctrl = sender
        If e.KeyCode = Keys.Return Then
            If myctrl.Name = "txtlastKM" Then
                dtpdate.Focus()
            Else
                SendKeys.Send("{TAB}")
            End If
        End If
    End Sub
    Private Sub txtcustomer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtsuppler.TextChanged, txtplateno.TextChanged, txtcustomer.TextChanged
        If chgbyprg = True Then Exit Sub
        chgPost = True
        Dim myCtrl As Control = sender
        Select Case myCtrl.Name
            Case "txtsuppler"
                _srchTxtId = 1
            Case "txtplateno"
                _srchTxtId = 2
            Case "txtcustomer"
                _srchTxtId = 3
        End Select
        _srchOnce = False
        ShowFmlist(sender)
    End Sub
    Private Sub txtcustomer_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtsuppler.Validated, txtcustomer.Validated

        Dim myctrl As TextBox
        myctrl = sender
        If myctrl.Text = "" Then
            If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
            Exit Sub
        End If
        If myctrl.Name = "txtcustomer" Then
            loadCustomerDetails(0, False)
        Else
            loadCustomerDetails(0, True)
        End If
        chgbyprg = False
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
    End Sub
    Private Sub txtplateno_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtplateno.Validated
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing

        'searchCar()
        loadWaite(1)

    End Sub

    Private Sub ShowFmlist(ByVal myCtrl As Control)
        If chgbyprg Then Exit Sub
        MyActiveControl = myCtrl
        If fMList Is Nothing Then
            fMList = New Mlistfrm
        End If
        If Not _srchOnce Then
            chgbyprg = True
            Dim PopupLoc As Point
            Dim x As Integer
            Dim y As Integer
            If _srchTxtId = 4 Then
                x = Me.Left + 480
                y = Me.Top + 465
            ElseIf _srchTxtId = 2 Then
                x = Me.Left + 100
                y = Me.Top + 300
            ElseIf _srchTxtId = 3 Then
                x = Me.Left + 800
                y = Me.Top + 500
            Else
                x = Me.Left + 480
                y = Me.Top + 320
            End If

            PopupLoc = New Point(x, y)
            If fMList.Visible = False Then
                Select Case _srchTxtId
                    Case 1, 3
                        SetFmlist(fMList, 3)
                    Case 2
                        SetFmlist(fMList, 29)
                        'Case 3
                        '    SetFmlist(fMList, 2)
                    Case 4
                        SetFmlist(fMList, 12)
                End Select
                If _srchTxtId = 4 Then
                    fMList.Height = fMList.Height - 50
                End If
                fMList.loc = PopupLoc
                fMList.Show(fMainForm)
                fMList.Visible = True
            End If
        End If
        Select Case _srchTxtId
            Case 1   'Supplier name
                fMList.SearchIndex = 0
                fMList.SearchIndexDescr = 1
                fMList_doFocus()
                fMList.Search(txtsuppler.Text)
                fMList.AssignList(txtsuppler, lstKey, chgbyprg)
            Case 2   'Car name
                fMList.SearchIndex = 0
                fMList.SearchIndexDescr = 1
                fMList_doFocus()
                fMList.Search(txtplateno.Text)
                fMList.AssignList(txtplateno, lstKey, chgbyprg)
            Case 3   'Customer name
                fMList.SearchIndex = 0
                fMList.SearchIndexDescr = 1
                fMList_doFocus()
                fMList.Search(txtcustomer.Text)
                fMList.AssignList(txtcustomer, lstKey, chgbyprg)
            Case 4   'Technician
                fMList.SearchIndex = 0
                fMList.SearchIndexDescr = 1
                fMList_doFocus()
                'fMList.Search(txttechnician.Text)
                'fMList.AssignList(txttechnician, lstKey, chgbyprg)
        End Select
        _srchOnce = True
        chgbyprg = False
    End Sub
    Private Sub fMList_doFocus() Handles fMList.doFocus
        If TypeOf (MyActiveControl) Is TextBox Then
            MyActiveControl.focus()
        End If
    End Sub

    Private Sub fMList_doSelect(ByVal ItmFlds() As String) Handles fMList.doSelect
        chgbyprg = True
        Select Case _srchTxtId
            Case 1
                txtsuppler.Text = ItmFlds(0)
                txtsuppler.Tag = ItmFlds(3)
            Case 2
                txtplateno.Text = ItmFlds(0)
                'txtplateno.Tag = ItmFlds(2)
            Case 3
                txtcustomer.Text = ItmFlds(0)
                txtcustomer.Tag = ItmFlds(3)
        End Select
        chgbyprg = False
    End Sub

    Private Sub loadCustomerDetails(ByVal accid As Long, ByVal issupp As Boolean)
        Dim dt As DataTable
        chgbyprg = True
        _objcmnbLayer = New clsCommon_BL

        If Not issupp Then
            dt = _objcmnbLayer._fldDatatable("SELECT accid,Alias,AccDescr,Address1,Address2,Address3,Address4,Phone,Fax,EMail,PT1,PT2,PT3,PT4," & _
                                        "TrdLcno,TrdDate,ContactName,GrpSetOn from AccMast LEFT JOIN AccMastAddr ON AccMast.ACCID=AccMastAddr.accountno " & _
                                        "left join S1AccHd on S1AccHd.S1AccId=AccMast.S1AccId " & _
                                        IIf(accid > 0, "where accid=" & accid, "where AccDescr='" & txtcustomer.Text & "'"))
            If dt.Rows.Count > 0 Then
                txtcustomer.Tag = dt(0)("accid")
                txtcustomer.Text = dt(0)("AccDescr")
                txtcustomeraddress.Text = dt(0)("Address1") & vbCrLf & dt(0)("Address2") & vbCrLf & dt(0)("Address3") & vbCrLf & dt(0)("Phone") & vbCrLf & dt(0)("ContactName")
                'Dim iNBal As Double = getAccBal(Val(txtsuppler.Tag))
                btnupdate.Focus()

            Else
                txtcustomer.Text = ""
                txtcustomer.Tag = ""
                txtcustomeraddress.Text = ""
            End If
        Else
            dt = _objcmnbLayer._fldDatatable("SELECT accid,Alias,AccDescr,Address1,Address2,Address3,Address4,Phone,Fax,EMail,PT1,PT2,PT3,PT4," & _
                                        "TrdLcno,TrdDate,ContactName,GrpSetOn from AccMast LEFT JOIN AccMastAddr ON AccMast.ACCID=AccMastAddr.accountno " & _
                                        "left join S1AccHd on S1AccHd.S1AccId=AccMast.S1AccId " & _
                                        IIf(accid > 0, "where accid=" & accid, "where AccDescr='" & txtsuppler.Text & "'"))
            If dt.Rows.Count > 0 Then
                txtsuppler.Tag = dt(0)("accid")
                txtsuppler.Text = dt(0)("AccDescr")
                txtaddress.Text = dt(0)("Address1") & vbCrLf & dt(0)("Address2") & vbCrLf & dt(0)("Address3") & vbCrLf & dt(0)("Phone") & vbCrLf & dt(0)("ContactName")
                'Dim iNBal As Double = getAccBal(Val(txtsuppler.Tag))
                txtDescription.Focus()
            Else
                txtsuppler.Text = ""
                txtaddress.Text = ""
                txtaddress.Text = ""
            End If
        End If

        chgbyprg = False
        btncustomer.Tag = 0

    End Sub
    Private Sub searchCar()
        chgbyprg = True
        Dim dt As DataTable
        Dim query As String
        query = "select * from JobTb left join UsedcarTb on UsedcarTb.jobid=jobtb.Jobid " & _
                "left join CarMasterTb on UsedcarTb.carid=CarMasterTb.carid " & _
                "left join (select itemid, [item code] itemcode from invitm) invitm on CarMasterTb.platenumber=invitm.itemcode " & _
                "left join AccMast on AccMast.AccId=JobTb.custcode  " & _
                "left join AccMastAddr on AccMastAddr.AccountNo=AccMast.AccountNo  " & _
                "left join (select AccId supid,AccDescr supname,Address1 supAdd1,Address2 supAdd2,Address3 supAdd3 from AccMast " & _
                "left join AccMastAddr on AccMastAddr.AccountNo=AccMast.AccountNo )supAcc on supAcc.supid=JobTb.supcode " & _
                "left join (select [Job Code] IPjobcode,InvNo IPInvNO,TrDate IPDate,PreFix IPPrifix,NetAmt IPNetamnt,TrId  IPId,TrRefNo IPREF,CSCode supcode from ItmInvCmnTb where TrType='IP' and isnull([Job Code],'')<>'')IP on IP.IPjobcode =JobTb.jobcode " & _
                "left join (select [Job Code] ISjobcode,InvNo ISInvNO,TrDate ISDate,PreFix ISPrifix,NetAmt ISNetamnt,AccDescr custName,TrId ISId,TrRefNo ISREF,CSCode custcode from ItmInvCmnTb left join AccMast on ItmInvCmnTb.CSCode=AccMast.AccId where TrType='IS' and isnull([Job Code],'')<>'')ISTR on ISTR.ISjobcode =JobTb.jobcode " & _
                "left join (select Reference,AccountNo,sum(dealamt)ipbalance from AccTrCmn inner join AccTrDet on AccTrCmn.LinkNo=AccTrDet.LinkNo group by Reference,AccountNo)IPBAL " & _
                "on ip.supcode=IPBAL.AccountNo and ip.IPREF=IPBAL.Reference " & _
                "left join (select Reference isreference,AccountNo isaccountno,sum(dealamt)isbalance from AccTrCmn inner join AccTrDet on AccTrCmn.LinkNo=AccTrDet.LinkNo group by Reference,AccountNo)ISBAL " & _
                "on ISTR.custcode=ISBAL.isaccountno and ISTR.ISREF=ISBAL.isreference " & _
                "where jobcode<>''and platenumber='" & txtplateno.Text & "'"
        dt = _objcmnbLayer._fldDatatable(query)

        If dt.Rows.Count > 0 Then
            makeClear()
            chgbyprg = True
            txtplateno.Text = Trim(dt(0)("platenumber") & "")
            itemid = Val(dt(0)("itemid") & "")
            txtplateno.Tag = dt(0)("carid")
            dtpdate.Value = DateValue(dt(0)("jobdate"))
            txtcarname.Text = Trim(dt(0)("cartype") & "")
            txtmodel.Text = Trim(dt(0)("regyear") & "")
            txtchasisno.Text = Trim(dt(0)("chaisenumber") & "")
            txtengineno.Text = Trim(dt(0)("engineNo") & "")
            'txtlastKM.Text = Trim(dt(0)("lastKm") & "")
            loadCustomerDetails(Val(dt(0)("supcode") & ""), True)
            txtjobcode.Tag = dt(0)("Jobid")
            txtjobcode.Text = dt(0)("jobcode")
            txtDescription.Text = dt(0)("JobDescription")
            If Not IsDBNull(dt(0)("modidate")) Then
                lblmodidate.Text = dt(0)("modidate")
            End If
            If Val(dt(0)("ipbalance") & "") < 0 Then
                lblipbal.Text = Format(dt(0)("ipbalance") * -1, numFormat)
                lblipbal.ForeColor = Color.Red
                btnpv.Enabled = True
            Else
                lblipbal.Text = "No Outstanding"
                lblipbal.ForeColor = Color.Green
                btnpv.Enabled = False
            End If
            If Val(dt(0)("isbalance") & "") > 0 Then
                lblisbal.Text = Format(dt(0)("isbalance"), numFormat)
                lblisbal.ForeColor = Color.Red
                btnrv.Enabled = True
            Else
                lblisbal.Text = "No Outstanding"
                lblisbal.ForeColor = Color.Green
                btnrv.Enabled = False
            End If
            chgbyprg = True
            'txtsuppler.Text = dt(0)("AccDescr")
            chkParkSale.Checked = dt(0)("isParkAndSale")
            txtPurchaseInvNo.Text = Trim(dt(0)("IPPrifix") & "") & Val(dt(0)("IPInvNO") & "")
            txtsupinvno.Text = Trim(dt(0)("IPREF") & "")
            txtPurchaseInvNo.Tag = dt(0)("IPId")
            If Val(txtPurchaseInvNo.Tag & "") > 0 Or chkParkSale.Checked Then
                btnprchase.Enabled = False
                txtsuppler.ReadOnly = True
                btnadd.Enabled = False
            Else
                btnprchase.Enabled = True
                txtsuppler.ReadOnly = False
                btnadd.Enabled = True
            End If
            'txtSalesInvNo.Text = Trim(dt(0)("ISPrifix") & "") & Val(dt(0)("ISInvNO") & "")
            txtSalesInvNo.Text = Trim(dt(0)("ISREF") & "")
            txtSalesInvNo.Tag = dt(0)("ISId")
            If Val(txtSalesInvNo.Tag & "") > 0 Then
                btninvoice.Enabled = False
                btncustomer.Enabled = False
                txtcustomer.ReadOnly = True
            Else
                btninvoice.Enabled = True
                btncustomer.Enabled = True
                txtcustomer.ReadOnly = False
            End If
            txtlastKM.Text = Trim(dt(0)("lastKM") & "")
            loadCustomerDetails(dt(0)("custcode"), False)
            fillGrid()

            If Val(dt(0)("IPNetamnt") & "") > 0 Then
                lblPurchaseCost.Text = Format(dt(0)("IPNetamnt"), numFormat)
            Else
                lblPurchaseCost.Text = 0

            End If

            If Val(dt(0)("ISNetamnt") & "") > 0 Then
                txtsalescost.Text = Format(dt(0)("ISNetamnt"), numFormat)
            Else

                txtsalescost.Text = 0

            End If

            calculate()
            btnnew.Tag = 1
            Dim fname As String = "Uc_" & txtjobcode.Tag & ".png"
            LdPic(picUsedcar, DPath & "\" & fname, Me, False)
            btnmore.Enabled = True
            'btnsearchPlateno.Text = "Clear"
            txtplateno.Enabled = True
            'btneditplatenumber.Enabled = True
            If Val(txtPurchaseInvNo.Tag & "") > 0 Then
                btnprchase.Enabled = False
            Else
                btnprchase.Enabled = True
            End If
            'btnprchase.Enabled = True
            If Val(txtSalesInvNo.Tag & "") > 0 Or Val(txtcustomer.Tag & "") = 0 Then
                btninvoice.Enabled = False
            Else
                btninvoice.Enabled = True
            End If
            btnexpence.Enabled = True
        Else
            txtplateno.Enabled = True
            txtplateno.Tag = ""
            txtlastKM.Text = ""
            txtcarname.Text = ""
            txtmodel.Text = ""
            txtchasisno.Text = ""
            txtengineno.Text = ""
            txtlastKM.Text = ""

        End If
ext:
        chgbyprg = False
        btnnew.Text = "Undo"
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        If chgPost = True Then
            If MsgBox("Changes found! Do you want to close?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        End If
        Me.Close()
    End Sub
    'Private Sub btneditplatenumber_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If MsgBox("Do you want change Reg.Number?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then carid = 0 : Exit Sub
    '    txtplateno.Enabled = True
    '    btneditplatenumber.Enabled = False
    '    carid = txtplateno.Tag
    '    txtplateno.Focus()
    'End Sub

    'Private Sub btnsearchPlateno_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsearchPlateno.Click
    '    If btnsearchPlateno.Text = "Clear" Then
    '        If Val(txtjobcode.Tag) > 0 Then
    '            If MsgBox("Job opened! Do you want clear data?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
    '            undo()
    '        End If
    '        clearCarDetails()
    '    End If
    'End Sub
    Private Sub undo()
        btnnew.Text = "New"
        makeClear()
        enableDisableControl(True)
        txtjobcode.Text = ""
        txtjobcode.Tag = ""
    End Sub
    Private Sub makeClear()
        chgbyprg = True
        txtDescription.Text = ""
        txtplateno.Text = ""
        txtplateno.Tag = ""
        txtlastKM.Text = ""
        txtcarname.Text = ""
        txtmodel.Text = ""
        txtchasisno.Text = ""
        txtengineno.Text = ""
        txtlastKM.Text = ""
        txtjobcode.Text = ""
        txtjobcode.Tag = ""
        txtjobcode.Text = ""
        txtjobcode.Tag = ""
        txtsupinvno.Text = ""
        btnnew.Tag = 0
        itemid = 0
        txtsalescost.Text = Format(0, numFormat)
        lblPurchaseCost.Text = Format(0, numFormat)
        lbljobcost.Text = Format(0, numFormat)
        picUsedcar.BackgroundImage = Nothing
        txtscost.Text = Format(0, numFormat)
        txtsuppler.Text = ""
        txtsuppler.Tag = ""
        txtaddress.Text = ""
        txtcustomer.Text = ""
        txtcustomer.Tag = ""
        txtcustomeraddress.Text = ""
        txtPurchaseInvNo.Text = ""
        txtPurchaseInvNo.Tag = ""
        txtSalesInvNo.Text = ""
        txtSalesInvNo.Tag = ""
        'txtplateno.ReadOnly = False
        fillGrid()
        calculate()
        chgbyprg = False
        btnnew.Text = "New"
        'btnsearchPlateno.Enabled = True
    End Sub
    Private Sub clearCarDetails()
        chgbyprg = True
        txtplateno.Text = ""
        txtplateno.Tag = ""
        txtplateno.ReadOnly = False
        txtmodel.Text = ""
        txtchasisno.Text = ""
        txtengineno.Text = ""
        txtlastKM.Text = ""
        'btnsearchPlateno.Text = "Search"
        txtcarname.Text = ""
        txtplateno.Enabled = True
        txtplateno.Focus()
        carid = 0
        txtsuppler.Text = ""
        txtsuppler.Tag = ""
        txtaddress.Text = ""
        chgbyprg = False
        chcar = False
        'jobhistory()
    End Sub
    Private Sub enableDisableControl(ByVal EnDs As Boolean)
        For Each Control In gbjob.Controls
            If TypeOf (Control) Is TextBox Then
                Control.readonly = EnDs
            End If
            If TypeOf (Control) Is DateTimePicker Then
                Control.enabled = Not EnDs
            End If
        Next
        txtaddress.ReadOnly = True

        txtDescription.ReadOnly = EnDs
        'txtserviceCharge.ReadOnly = EnDs
        txtscost.ReadOnly = EnDs

        btndelete.Enabled = Not EnDs

        btnclosejob.Visible = False
        'gbinvoice.Enabled = Not EnDs
        'btnupdate.Enabled = Not EnDs

    End Sub
    Public Sub fillGrid()

        Dim strSql As String = "select JVType,PREFIX + case when PREFIX ='' then '' else '/' end +convert(varchar,JVNum)JVNum,JVDate,AccDescr," & _
        "DealAmt,EntryRef,AccTrDet.LinkNo   from AccTrCmn left join AccTrDet on AccTrCmn.LinkNo= AccTrDet.LinkNo " & _
        "left join AccMast on AccMast.AccId=AccTrDet.AccountNo where lnkno=0 and isnull(AccTrDet.JobCode,'') = '" & txtjobcode.Text & "' and DealAmt>0 and isnull(AccTrDet.JobCode,'')<>'' "
        grdinvList.DataSource = Nothing
        _objcmnbLayer = New clsCommon_BL
        Dim source As DataTable = Me._objcmnbLayer._fldDatatable(strSql, False)
        grdinvList.DataSource = source
        gridTotal(grdinvList.DataSource)
        SetGridHeadInv()
        chgbyprg = False
        Dim num3 As Integer = (source.Rows.Count - 1)

    End Sub
    Private Sub SetGridHeadInv()

        SetGridProperty(grdinvList)
        With grdinvList
            .Columns.Item(.ColumnCount - 1).Visible = False
            .Columns.Item("JVType").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("JVType").HeaderText = "Type"
            .Columns.Item("JVType").Width = 150
            .Columns.Item("JVType").Width = &H4B
            .Columns.Item("JVNum").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("JVNum").HeaderText = "Number"
            .Columns.Item("JVNum").Width = 150
            .Columns.Item("JVNum").Width = &H4B
            .Columns.Item("JVDate").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("JVDate").HeaderText = "Date"
            .Columns.Item("JVDate").Width = &H4B
            .Columns.Item("JVDate").Width = 150
            .Columns.Item("AccDescr").Width = 200
            .Columns("AccDescr").HeaderText = "Account Name"
            .Columns.Item("DealAmt").DefaultCellStyle.Format = ("N" & NoOfDecimal)
            .Columns.Item("DealAmt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("DealAmt").HeaderText = "Amount"
            .Columns.Item("DealAmt").Width = 100
            .Columns.Item("EntryRef").Width = 250
            .Columns("EntryRef").HeaderText = "Description"
            '.Columns("EntryRef").Visible = False
        End With
        'resizeGridColumn(grdinvList, 5)
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click

        If btnnew.Text = "New" Then

            makenew()
        Else
            If btnnew.Text = "Undo" Then
                If MsgBox("You are going to undo the modify! Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Exit Sub
                End If
            End If
            undo()
        End If

    End Sub

    Private Sub makenew()
        btnnew.Text = "Clear"

        makeClear()
        AddNew()
        enableDisableControl(False)
        clearCarDetails()

    End Sub
    Private Sub AddNew()
        txtjobcode.Text = GenerateNext(txtjobcode.Text)
        txtsuppler.Focus()
    End Sub
    Private Function GenerateNext(ByVal strCode As String) As String
        Dim N As Double
        Dim NewCode As String
        Dim i As Byte
        Dim f As Byte
        Dim tmp As String
        Dim dt As DataTable
        _objcmnbLayer = New clsCommon_BL
        If strCode = "" Then
            dt = _objcmnbLayer._fldDatatable("SELECT top 1 jobcode from jobtb order by jobid desc")
            If dt.Rows.Count > 0 Then
                strCode = dt(0)(0)
            End If
        End If
        If strCode = "" Then
            strCode = "JB"
        End If
        Dim dr As DataTable
        If strCode = "" Then Return strCode
        For i = 1 To 11
            If IsNumeric(Mid(strCode, Len(strCode) - i + 1, 1)) = False Then Exit For
            tmp = Val(Mid(strCode, Len(strCode) - i + 1))
            If Val(tmp) <> 0 Then
                N = Val(tmp)
            Else
                If N <> 0 Then Exit For
            End If
            If i = Len(strCode) Then i = i + 1 : Exit For
        Next i
        i = i - 1
        f = i
        If i <= 0 Then
            'i = txtCode.MaxLength - Len(strCode)
            i = 3
        End If
        tmp = ""
        NewCode = ""
        Try
            Do Until False
                N = N + 1
                tmp = Mid(strCode, 1, IIf(Len(strCode) <> 0, Len(strCode) - f, 0)) & Format(N, StrDup(i, "0"))
                dr = _objcmnbLayer._fldDatatable("SELECT jobcode from jobtb WHERE jobcode = '" & tmp & "'")
                If dr.Rows.Count = 0 Then
                    NewCode = Mid(strCode, 1, IIf(Len(strCode) <> 0, Len(strCode) - f, 0)) & Format(N, StrDup(i, "0"))
                    NewCode = Mid(NewCode, 1, 30)
                    Exit Do
                End If
            Loop
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return NewCode
    End Function


    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        updateClick()
    End Sub
    Private Sub updateClick()
        If Val(btnupdate.Tag) = 0 Then
            MsgBox("This user do not have permission to Update", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        verify()

    End Sub
    Private Sub verify()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select jobid from jobtb where jobcode ='" & txtjobcode.Text & "' and jobid<>" & Val(txtjobcode.Tag))
        If dt.Rows.Count > 0 Then
            MsgBox("Job code already exist", MsgBoxStyle.Exclamation)
            txtjobcode.Focus()
            Exit Sub
        End If
        If Val(txtsuppler.Tag) = 0 Then
            MsgBox("Invalid supplier", MsgBoxStyle.Exclamation)
            txtsuppler.Focus()
            Exit Sub
        End If
        If txtplateno.Text = "" Then
            MsgBox("Invalid Car Registration Number", MsgBoxStyle.Exclamation)
            txtplateno.Focus()
            Exit Sub
        End If
        If MsgBox("Verification is succeeded. Do you like to file it?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        'SaveRecord()
        loadWaite(2)
        'txtprintjob.Text = txtjobcode.Text

    End Sub
    Private Sub SaveRecord()
        saveCar()
        saveJob()
        searchCar()

        'makeClear()
        'txtjobcode.Text = ""
        'txtjobcode.Tag = ""
        enableDisableControl(True)
        btnnew.Text = "New"
        txtplateno.Focus()
        chcar = False
    End Sub
    Private Sub saveCar()
        With _objvh
            .carid = Val(txtplateno.Tag)
            .platenumber = txtplateno.Text
            .cartype = txtcarname.Text
            .regyear = txtmodel.Text
            .chassisnumber = txtchasisno.Text
            .engineNo = txtengineno.Text
            .customerid = Val(txtsuppler.Tag)
            .isuedcar = True
            .itemid = itemid
            txtplateno.Tag = .savecarmaster()

        End With

    End Sub
    Private Sub saveJob()
        _objJob = New clsJob
        With _objJob
            .Jobid = Val(txtjobcode.Tag)
            .jobcode = txtjobcode.Text
            .jobdate = DateValue(dtpdate.Value)
            .JobDescription = txtDescription.Text
            .custcode = Val(txtcustomer.Tag)
            .carid = Val(txtplateno.Tag)
            .isParkAndSale = IIf(chkParkSale.Checked, 1, 0)
            .supcode = Val(txtsuppler.Tag)
            .lastKM = txtlastKM.Text

            txtjobcode.Tag = .saveUsedcarJob()
            Dim jobid As String
            jobid = Val(txtjobcode.Tag)
            If picUsedcar.Tag <> "" Then
                Dim fname As String
                fname = "Uc_" & jobid & ".png"
                'On Error Resume Next
                If DPath = "" Or DPath = "\" Then Exit Sub

                picUsedcar.BackgroundImage.Dispose()
                If FileExists(DPath & fname) Then
                    System.IO.File.Delete(DPath & fname)
                    'My.Computer.FileSystem.DeleteFile(DPath & fname)
                End If
                FileCopy(picUsedcar.Tag, DPath & fname)
                'If Not Err.Number Then
                '    Dim bm As Bitmap = picLogo.Image
                '    bm.Save(DPath & "Logo.vin", System.Drawing.Imaging.ImageFormat.Jpeg)
                'End If
                If Err.Number Then
                    If MsgBox(Err.Description, vbRetryCancel + vbExclamation) = vbRetry Then Resume
                End If
            End If
        End With



    End Sub




    Private Sub UsedcarFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        makenew()

        'enableDisableControl(True)


        If userType Then
            btnupdate.Tag = IIf(getRight(181, CurrentUser), 1, 0)
            btndelete.Tag = IIf(getRight(182, CurrentUser), 1, 0)
        Else
            btnupdate.Tag = 1
            btndelete.Tag = 1
        End If
        'loadCarDetails()

        Timer2.Enabled = True


    End Sub
    Private Sub loadCarDetails()
        Dim tp As Integer
        Dim rptType As Integer
        If RdbAll.Checked Then
            tp = 0
        ElseIf Rdbavailable.Checked Then
            tp = 1
        Else
            tp = 2

        End If
        If chkbydate.Checked Then
            If rdojobdate.Checked Then
                rptType = 1
            ElseIf rdopurchasedate.Checked Then
                rptType = 2
            Else
                rptType = 3
            End If
        End If

        If chkParkndSale.Checked Then
            dtTable = _objvh.returnusedcarmaster(True, tp, DateValue(cldrStartDate.Value), DateValue(cldrEnddate.Value), rptType).Tables(0)
        Else
            dtTable = _objvh.returnusedcarmaster(False, tp, DateValue(cldrStartDate.Value), DateValue(cldrEnddate.Value), rptType).Tables(0)
        End If


        'dtTable = _objvh.returncarmaster(1).Tables(0)
        dgvCarmaster.DataSource = dtTable
        SetGridProperty(dgvCarmaster)
        With dgvCarmaster
            .Columns("Vehicle Name").Width = 150
            .Columns("Vehicle Name").HeaderText = "Vehicle Name"
            .Columns("Reg Number").Width = 100
            .Columns("Reg Number").HeaderText = "Reg Number"
            .Columns("Chasis Number").Width = 150
            .Columns("Chasis Number").HeaderText = "Chasis Number"
            .Columns("Engine Number").Width = 150
            .Columns("Engine Number").HeaderText = "Engine Number"
            .Columns("Model").Width = 150
            .Columns("Model").HeaderText = "Model"
            .Columns("customer").Width = 150
            .Columns("customer").HeaderText = "Customer"
            .Columns("suppname").Width = 150
            .Columns("suppname").HeaderText = "Supplier"

            .Columns("Phone").Width = 180
            .Columns("Phone").HeaderText = "Customer Phone"
            .Columns("Job Code").Width = 150
            .Columns("Job Code").HeaderText = "Job Code"
            .Columns("Job Date").Width = 100
            .Columns("Job Date").HeaderText = "Job Date"
            .Columns("IPInvNO").Width = 100
            .Columns("IPInvNO").HeaderText = "Purchase"
            .Columns("ISInvNO").Width = 100
            .Columns("ISInvNO").HeaderText = "Sales"

            .Columns("IPNetamnt").Width = 100
            .Columns("IPNetamnt").HeaderText = "IP Amount"
            .Columns("IPNetamnt").DefaultCellStyle.Format = ("N" & NoOfDecimal)
            .Columns("IPNetamnt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns("ISNetamnt").Width = 100
            .Columns("ISNetamnt").HeaderText = "IS Amount"
            .Columns("ISNetamnt").DefaultCellStyle.Format = ("N" & NoOfDecimal)
            .Columns("ISNetamnt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns("Expamnt").Width = 100
            .Columns("Expamnt").HeaderText = "Expense"
            .Columns("Expamnt").DefaultCellStyle.Format = ("N" & NoOfDecimal)
            .Columns("Expamnt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns("Profit").Width = 100
            .Columns("Profit").DefaultCellStyle.Format = ("N" & NoOfDecimal)
            .Columns("Profit").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns("carid").Visible = False
            .Columns("jobid").Visible = False
            .Columns("lnk").Visible = False
            .Columns("soldstatus").Visible = False
            .Columns("rpttype").Visible = False
            .Columns("dateto").Visible = False
            .Columns("datefrom").Visible = False
            Dim i As Integer
            cmbOrder.Items.Clear()
            For i = 0 To .Columns.Count - 1
                cmbOrder.Items.Add(.Columns(i).HeaderText)
            Next
            cmbOrder.SelectedIndex = 1

        End With

    End Sub
    Private Sub changecolor()
        With dgvCarmaster
            For i = 0 To .RowCount - 1
                If Val(.Item("soldstatus", i).Value) = 2 Then
                    .Rows(i).DefaultCellStyle.BackColor = Color.LightPink
                Else
                    .Rows(i).DefaultCellStyle.BackColor = Color.LightGreen
                End If
            Next
        End With
    End Sub




    Private Sub dgvCarmaster_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvCarmaster.DoubleClick
        If dgvCarmaster.RowCount = 0 Then Exit Sub
        chgbyprg = True
        txtplateno.Text = dgvCarmaster.Item("Reg Number", dgvCarmaster.CurrentRow.Index).Value
        chgbyprg = False
        'searchCar()
        loadWaite(1)
        TabControl2.SelectedIndex = 0
    End Sub
    Private Sub grdinvList_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdinvList.DoubleClick
        If grdinvList.RowCount = 0 Then Exit Sub
        Dim lnkno As Long
        lnkno = grdinvList.Item("LinkNo", grdinvList.CurrentRow.Index).Value
        fMainForm.LoadPVO(lnkno, txtjobcode.Text)
    End Sub
    Private Sub btninvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btninvoice.Click
        If Val(txtcustomer.Tag) > 0 Then
            fInvoice = New SalesInvoice
            fInvoice.MdiParent = fMainForm
            fInvoice.Show()
            fInvoice.returnFromJobCarWash(txtjobcode.Text, Val(txtjobcode.Tag), IIf(chkParkndSale.Checked, "JOB", txtplateno.Text), IIf(chkParkndSale.Checked, "SERVICE", txtcarname.Text), Val(txtcustomer.Tag))
        Else

            MsgBox("Please select valid Customer", MsgBoxStyle.Exclamation)
            txtcustomer.Focus()
        End If


    End Sub



    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        btncustomer.Tag = 0
        QuickCust(True, "Supplier")
        'loadWaite(4)
    End Sub
    Public Sub QuickCust(Optional ByRef bOnlyOne As Boolean = False, Optional ByVal Grp As String = "Supplier")
        fCrtAcc = New CreateAccNew
        With fCrtAcc
            .Condition = "GrpSetOn In ('" & Grp & "')"
            If Grp = "Supplier" Then
                .iscust = False
            Else
                .iscust = True
            End If
            .bOnlyOne = bOnlyOne
            .ShowDialog()
            txtDescription.Focus()

        End With
    End Sub

    Private Sub fCrtAcc_OpenAccMaster(ByRef AccountNo As Long) Handles fCrtAcc.OpenAccMaster
        loadCustomerDetails(AccountNo, IIf(Val(btncustomer.Tag) = 1, False, True))
    End Sub


    Private Sub btnPicUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPicUpload.Click
        With DlgOpen
            .Filter = "All Picture Files|*.bmp;*.dib;*.gif;*.wmf;*.emf;*.jpg;*.ico;*.cur|" & _
                     "Bitmaps(*.bmp,*.dib)|*.bmp;*.dib|" & _
                     "Gif Images(*.gif)|*.gif|" & _
                     "JPEG Images(*.jpg)|*.jpg|" & _
                     "Matafiles(*.wmf,*.emf)|*.wmf;*.emf|" & _
                     "Icons(*.ico,*.cur)|*.ico;*.cur|" & _
                     "All Files(*.*)|*.*"
            .Title = "Select an Image file"
            .FileName = ""
            .ShowDialog()
            If .FileName <> "" Then
                Err.Clear()
                On Error Resume Next
                'Dim bm As New Bitmap(.FileName)
                'picLogo.Image = bm
                LdPic(picUsedcar, .FileName, Me, False)
                If Err.Number Then
                    MsgBox(Err.Description)
                Else
                    picUsedcar.Tag = .FileName
                End If
            End If
            ChDir(Application.StartupPath)
        End With
    End Sub





    Private Sub btnexpence_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexpence.Click
        fMainForm.LoadPVO(0, txtjobcode.Text)
    End Sub

    Private Sub btncarRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'loadCarDetails()
        loadWaite(3)

    End Sub

    Private Sub chkParkndSale_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkParkndSale.CheckedChanged
        'loadCarDetails()
        loadWaite(3)
    End Sub
    'Private Sub loadjobdetails()
    '    Dim tp As Integer
    '    If chkParkndSale.Checked Then
    'End Sub


    Private Sub btnprchase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprchase.Click
        If Val(txtsuppler.Tag & "") = 0 Then
            Exit Sub
        End If
        fpInvoice = New PurchaseInvoiceFrm
        fpInvoice.MdiParent = fMainForm

        fpInvoice.Show()
        fpInvoice.returnFromJob(txtjobcode.Text, Val(txtjobcode.Tag), txtplateno.Text, Val(txtsuppler.Tag), txtcarname.Text)

    End Sub

   
    Private Sub gridTotal(ByVal RptdtTable As DataTable)
        txtscost.Text = Format(0, numFormat)
        If RptdtTable.Rows.Count = 0 Then Exit Sub
        Dim drSum As Double
        Dim amt As String
        amt = Trim(RptdtTable.Compute("SUM(DealAmt)", "") & "")
        If Val(amt) > 0 Then
            drSum = Convert.ToDouble(amt)
        End If

        txtscost.Text = Format(drSum, numFormat)

    End Sub
    Private Sub calculate()

        lbljobcost.Text = Format(CDbl(txtscost.Text) + CDbl(lblPurchaseCost.Text), numFormat)
        lblitemvalue.Text = Format(CDbl(txtsalescost.Text) - CDbl(lbljobcost.Text), numFormat)
    End Sub
    
  
    Private Sub btnPurchaseInvEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPurchaseInvEdit.Click
        If Val(txtPurchaseInvNo.Tag & "") > 0 Then
            fMainForm.LoadIP(txtPurchaseInvNo.Tag)
        End If

    End Sub

    Private Sub btnSalesInvEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalesInvEdit.Click
        fMainForm.LoadIS(txtSalesInvNo.Tag)
    End Sub

    Private Sub grdinvList_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdinvList.CellContentClick

    End Sub

    Private Sub btnrfsh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrfsh.Click
        'searchCar()
        loadWaite(1)
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        If chkFormat.Checked Then
            If TabControl2.SelectedIndex = 0 Then
                fRptFormat = New RptFormatfrm
                fRptFormat.RptType = "UCJC"
                fRptFormat.ShowDialog()
            Else
                fRptFormat = New RptFormatfrm
                fRptFormat.RptType = "USL"
                fRptFormat.ShowDialog()
            End If
            fRptFormat = Nothing

        Else
            If TabControl2.SelectedIndex = 0 Then
                PrepareRpt("UCJC")
            Else

                PrepareRpt("USL")
            End If

        End If
    End Sub
    Private Sub PrepareRpt(ByVal RptType As String, Optional ByVal Forprint As Boolean = False, Optional ByVal pno As Integer = 0)
        'If chgPost Then
        '    MsgBox("Changes Found! Update the voucher", MsgBoxStyle.Exclamation)
        '    btnupdate.Focus()
        '    Exit Sub
        'End If
        'If Val(txtplateno.Text) = 0 Then
        '    MsgBox("Enter a valid Voucher Number !!", vbInformation)
        '    txtplateno.Focus()
        '    Exit Sub
        'End If
        Dim RptName As String
        Dim RptCaption As String = ""
        Dim printername As String = ""
        RptName = getRptDefFlName(RptType, RptCaption, printername)
        If Trim(RptName) <> "" Then
            LoadReport(RptName, RptCaption, Forprint, pno, printername)
        End If
    End Sub
    Private Sub LoadReport(ByVal FileName As String, ByVal RptCaption As String, Optional ByVal ToPrint As Boolean = False, Optional ByVal pno As Integer = 0, Optional ByVal printername As String = "")
        If Not frm Is Nothing Then
            frm.Close()
            frm = New ReprtviewNEWfrm
        Else
            frm = New ReprtviewNEWfrm
        End If
        Dim ds As New DataSet
        If TabControl2.SelectedIndex = 0 Then
            If rptTable Is Nothing Then
                Dim jobcode As String
                jobcode = txtjobcode.Text
                ds = _objvh.returnUsedCarInvPrint(jobcode)

            Else
                ds.Tables.Add(rptTable)
            End If
        Else
            
            If rptTable Is Nothing Then
                Dim tp As Integer
                Dim rptType As Integer
                If RdbAll.Checked Then
                    tp = 0
                ElseIf Rdbavailable.Checked Then
                    tp = 1
                Else
                    tp = 2

                End If
                If chkbydate.Checked Then
                    If rdojobdate.Checked Then
                        rptType = 1
                    ElseIf rdopurchasedate.Checked Then
                        rptType = 2
                    Else
                        rptType = 3
                    End If
                End If
                'If chkParkndSale.Checked Then
                '    ds = _objvh.returnusedcarmaster(True, tp, DateValue(cldrStartDate.Value), DateValue(cldrEnddate.Value), IIf(chkbydate.Checked, 1, 0))
                'Else
                '    ds = _objvh.returnusedcarmaster(False, tp, DateValue(cldrStartDate.Value), DateValue(cldrEnddate.Value), IIf(chkbydate.Checked, 1, 0))
                'End If
                If chkParkndSale.Checked Then
                    ds = _objvh.returnusedcarmaster(True, tp, DateValue(cldrStartDate.Value), DateValue(cldrEnddate.Value), rptType)
                Else
                    ds = _objvh.returnusedcarmaster(False, tp, DateValue(cldrStartDate.Value), DateValue(cldrEnddate.Value), rptType)
                End If

            Else
                ds.Tables.Add(rptTable)
            End If
        End If




        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = fMainForm
        frm.Text = RptCaption
        frm.Show()
        rptTable = Nothing
    End Sub

    Private Sub txtSeq_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeq.TextChanged
        dgvCarmaster.DataSource = SearchGrid(dtTable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
        rptTable = SearchGrid(dtTable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
    End Sub

    Private Sub chkFormat_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFormat.CheckedChanged

    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click

        If Val(btndelete.Tag) = 0 Then
            MsgBox("This user do not have permission to Remove", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If grdinvList.RowCount > 0 Then
            MsgBox("You cannot delete invoiced job", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If Val(txtPurchaseInvNo.Tag & "") > 0 Then
            MsgBox("You cannot delete Purchased job", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If Val(txtSalesInvNo.Tag & "") > 0 Then
            MsgBox("You cannot delete Sales job", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If MsgBox("You are going to REMOVE the JOB # " & txtjobcode.Text & Chr(13) & "Are you sure ?", vbOKCancel + vbQuestion + vbDefaultButton2) = vbOK Then

            _objcmnbLayer._saveDatawithOutParm("delete from JobTb where jobid=" & Val(txtjobcode.Tag) & _
                                               " delete from UsedcarTb where jobid=" & Val(txtjobcode.Tag) & _
                                               " delete from CarMasterTb where carid=" & Val(txtplateno.Tag))
            undo()

        End If

    End Sub

    Private Sub btncustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncustomer.Click
        btncustomer.Tag = 1
        'loadWaite(5)
        QuickCust(True, "Customer")
    End Sub

    Private Sub fMList_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fMList.FormClosed
        fMList = Nothing
    End Sub


    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Timer2.Enabled = False
        'resizeGridColumn(grdinvList, 5)
        changecolor()
    End Sub

    Private Sub fwait_triggerEvent() Handles fwait.triggerEvent
        Select Case fwait.triggerType
            Case 1
                searchCar()
            Case 2
                SaveRecord()
                If Not fwait Is Nothing Then
                    fwait.Close()
                    fwait = Nothing
                End If
                MsgBox("Job Card saved successfully", MsgBoxStyle.Information)
                Exit Sub
            Case 3
                loadCarDetails()
                changecolor()
            Case 4
                QuickCust(True, "Supplier")
            Case 5
                QuickCust(True, "Customer")
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

    Private Sub btnmore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnmore.Click
        Try
            If Not fDoc Is Nothing Then fDoc = Nothing
            Dim Jobid As Integer
            Jobid = Val(txtjobcode.Tag)
            fDoc = New DocumentView
            If Val(Jobid) Then
                fDoc.KeyId = "UC-" & Val(Jobid)
                fDoc.moduleid = 7
                fDoc.isDoc = True
                fDoc.itemid = 0
                fDoc.ldImage()
                fDoc.ShowDialog()
            Else
                fDoc.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnpdoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpdoc.Click
        Try
            If Not fDoc Is Nothing Then fDoc = Nothing
            Dim Jobid As Integer
            Jobid = Val(txtjobcode.Tag)
            fDoc = New DocumentView
            If Val(Jobid) Then
                fDoc.KeyId = "UC-Supplier" & Val(Jobid)
                fDoc.moduleid = 7
                fDoc.isDoc = True
                fDoc.itemid = 0
                fDoc.ldImage()
                fDoc.ShowDialog()
            Else
                fDoc.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnsdoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsdoc.Click
        Try
            If Not fDoc Is Nothing Then fDoc = Nothing
            Dim Jobid As Integer
            Jobid = Val(txtjobcode.Tag)
            fDoc = New DocumentView
            If Val(Jobid) Then
                fDoc.KeyId = "UC-Customer" & Val(Jobid)
                fDoc.moduleid = 7
                fDoc.isDoc = True
                fDoc.itemid = 0
                fDoc.ldImage()
                fDoc.ShowDialog()
            Else
                fDoc.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub dgvCarmaster_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvCarmaster.CellContentClick

    End Sub
    Private Sub TabControl2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl2.Click
        If TabControl2.SelectedIndex = 1 And btnnew.Text = "Undo" Then
            MsgBox("Do Undo/Update before moving to Enquiry", MsgBoxStyle.Exclamation)
            TabControl2.SelectedIndex = 0
            Exit Sub
        End If
        loadWaite(3)
    End Sub

    Private Sub txtPurchaseInvNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPurchaseInvNo.TextChanged

    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub txtaddress_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtaddress.TextChanged
        chgPost = True
    End Sub

    Private Sub txtcustomeraddress_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcustomeraddress.TextChanged
        chgPost = True
    End Sub

    Private Sub txtDescription_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDescription.TextChanged
        chgPost = True
    End Sub

    Private Sub txtjobcode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtjobcode.TextChanged
        chgPost = True
    End Sub

    Private Sub txtlastKM_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtlastKM.TextChanged
        chgPost = True
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdbAll.CheckedChanged

    End Sub

    Private Sub btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnload.Click
        loadWaite(3)
    End Sub

    Private Sub btnpv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpv.Click
        fMainForm.LoadPV(0, txtsuppler.Text)
    End Sub

    Private Sub btnrv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrv.Click
        fMainForm.LoadRV(0, txtcustomer.Text)
    End Sub

    Private Sub chkParkSale_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkParkSale.Click
      
        If chkParkSale.Checked Then
            If Val(txtPurchaseInvNo.Tag & "") > 0 Then
                MsgBox("Purchased Job cannot converted to Park & Sale", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            btnPurchaseInvEdit.Enabled = False
            btnprchase.Enabled = False
            txtsuppler.Enabled = False
            btnadd.Enabled = False
        Else
            btnPurchaseInvEdit.Enabled = True
            btnprchase.Enabled = True
            txtsuppler.Enabled = True
            btnadd.Enabled = True
        End If

    End Sub

    Private Sub btnrvlist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrvlist.Click
        If Not fPpayment Is Nothing Then
            fPpayment.Close()
            fPpayment = Nothing
        End If
        fPpayment = New PreviousPaymentFrm
        With fPpayment
            .AccountNo = Val(txtcustomer.Tag)
            .accountname = txtcustomer.Text
            .dtpstart.Value = DateFrom
            .dtpto.Value = DateTo
            .jvtype = "RV"
            .reference = txtSalesInvNo.Text
            .ldGrid(10)
            .ShowDialog()
        End With
        fPpayment = Nothing
    End Sub

    Private Sub fPpayment_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fPpayment.FormClosed
        fPpayment = Nothing
    End Sub

    Private Sub btnpvlist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpvlist.Click
        If Not fPpayment Is Nothing Then
            fPpayment.Close()
            fPpayment = Nothing
        End If
        fPpayment = New PreviousPaymentFrm
        With fPpayment
            .AccountNo = Val(txtsuppler.Tag)
            .accountname = txtsuppler.Text
            .dtpstart.Value = DateFrom
            .dtpto.Value = DateTo
            .jvtype = "PV"
            .reference = txtsupinvno.Text
            .ldGrid(13)
            .ShowDialog()
        End With
        fPpayment = Nothing
    End Sub
End Class