Public Class AmcList
    Public btnchng As Boolean
    Private chgbyprg As Boolean
    Private MyActiveControl As New Object
    Private _srchOnce As Boolean
    Private _srchTxtId As Byte
    Private lstKey As Keys
    Private _objcmnbLayer As New clsCommon_BL
    Private customeralias As String
    Private WithEvents fRptFormat As RptFormatfrm
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fCrtAcc As CreateAccNew

    Private WithEvents fMList As Mlistfrm
    Private isShowItems As Boolean
    Private chgPost As Boolean
    Private activecontrolname As String
    Private WithEvents fwait As WaitMessageFrm
    Public strMyCaption As String
    Public isSingle As Boolean
    Public isItemdata As Boolean
    Public istemple As Boolean
    Public resizecolum As Integer
    Private itemid As Long
    Private dt As DataTable
    Private gridd As Long
    Private gridtrid As Long
    Private dtRports As DataTable

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub txtcustomer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtcustomer.KeyDown

        lstKey = e.KeyCode
        Dim MyCtrl As TextBox = sender
        If e.KeyCode = Keys.Return Then
            txtitem.Focus()
            If Not fMList Is Nothing Then fMList.Visible = False
        ElseIf e.KeyCode = Keys.Up Or e.KeyCode = Keys.PageUp Then
            isShowItems = False
            If Not fMList Is Nothing Then
                If fMList.Visible Then
                    fMList.MoveUp(txtitem.Text)
                    txtitem.SelectionStart = Len(txtitem.Text) + 1
                    'txtitemcode.SelectionLength = Len(txtitemcode.Text)
                    'txtitemcode.SelectAll()
                    Exit Sub
                End If
            End If

        ElseIf e.KeyCode = Keys.Down Or e.KeyCode = Keys.PageDown Then
            isShowItems = False
            If Not fMList Is Nothing Then
                If fMList.Visible Then
                    fMList.MoveDown(txtitem.Text)
                    'txtitemcode.SelectionStart = Len(txtitemcode.Text)
                    'txtitemcode.SelectionLength = Len(txtitemcode.Text)
                    Exit Sub
                End If
            End If
        End If
    End Sub

    Private Sub txtcustomer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcustomer.TextChanged
        If chgbyprg = True Then Exit Sub
        _srchOnce = False
        _srchTxtId = 1
        ShowFmlist(sender)

    End Sub
    Private Sub ShowFmlist(ByVal myCtrl As Control)
        If chgbyprg Then Exit Sub
        MyActiveControl = myCtrl
        If Not _srchOnce Then
            chgbyprg = True
            If fMList Is Nothing Then
                fMList = New Mlistfrm
            End If
            Dim PopupLoc As Point
            Dim x As Integer = Me.Width - fMList.Width - 100
            Dim y As Integer = Me.Height - fMList.Height - 10
            PopupLoc = New Point(x, y)
            If fMList.Visible = False Then
                Select Case _srchTxtId
                    Case 1 'Customer name
                        SetFmlist(fMList, 3)
                    Case 2 'Item Master
                        SetFmlist(fMList, 1)

                End Select
                fMList.loc = PopupLoc
                fMList.Show(fMainForm)
                fMList.Visible = True
            End If
        End If
        fMList.dvData.BackgroundColor = Color.White
        Select Case _srchTxtId
            Case 1   'Customer name
                fMList.SearchIndex = 0
                fMList.SearchIndexDescr = 1
                fMList_doFocus()
                fMList.Search(txtcustomer.Text)
                customeralias = fMList.AssignList(txtcustomer, lstKey, chgbyprg)
            Case 2   'Item Master
                fMList.SearchIndex = 0
                fMList_doFocus()
                'fMList.Search(txtitemcode.Text, True, chkSearch.Checked)
                fMList.Search(txtitem.Text, "", True)
                fMList.AssignList(txtitem, lstKey, chgbyprg)
                txtitem.SelectionStart = Len(txtitem.Text)
        End Select
        _srchOnce = True
        chgbyprg = False
    End Sub
    Private Sub fMList_doFocus() Handles fMList.doFocus
        If TypeOf (MyActiveControl) Is TextBox Then
            MyActiveControl.focus()
        End If
    End Sub

    Private Sub txtcustomer_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcustomer.Validated
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
        If txtcustomer.Text = "" Then Exit Sub
        setCustomer(, , , False)
    End Sub
    Private Sub setCustomer(Optional ByVal accid As Long = 0, Optional ByVal skipCalculate As Boolean = False, Optional ByVal GSTN As String = "", Optional ByVal skipcustomerdiscount As Boolean = True, Optional ByVal varlinkno As Long = 0)
        Dim dt As DataTable
        Dim condition As String
        condition = "where Alias='" & customeralias & "'"
        dt = _objcmnbLayer._fldDatatable("SELECT AccMast.accid,AccDescr" & _
                                        " from AccMast LEFT JOIN AccMastAddr ON AccMast.accid=AccMastAddr.AccountNo " & _
                                        "LEFT JOIN S1AccHd ON AccMast.S1AccId=S1AccHd.S1AccId " & _
                                        "LEFT JOIN (select custid,customeraccount from CashCustomerTb) CashCustomerTb ON customeraccount=ACCMAST.ACCID " & _
                                        condition)
        If dt.Rows.Count > 0 Then
            chgbyprg = True
            txtcustomer.Tag = dt(0)("accid")
            txtcustomer.Text = Trim("" & dt(0)("AccDescr"))
            chgbyprg = False
        End If

    End Sub


    Private Sub txtitem_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtitem.KeyDown

        lstKey = e.KeyCode
        If e.KeyCode <> Keys.Escape Then
            isShowItems = True
        Else
            isShowItems = False
        End If
        If e.KeyCode = Keys.Return Then
            txtdescr.Focus()
        ElseIf e.KeyCode = Keys.Up Or e.KeyCode = Keys.PageUp Then
            isShowItems = False
            If Not fMList Is Nothing Then
                If fMList.Visible Then
                    fMList.MoveUp(txtitem.Text)
                    txtitem.SelectionStart = Len(txtitem.Text) + 1
                    'txtitemcode.SelectionLength = Len(txtitemcode.Text)
                    'txtitemcode.SelectAll()
                    Exit Sub
                End If
            End If

        ElseIf e.KeyCode = Keys.Down Or e.KeyCode = Keys.PageDown Then
            isShowItems = False
            If Not fMList Is Nothing Then
                If fMList.Visible Then
                    fMList.MoveDown(txtitem.Text)
                    'txtitemcode.SelectionStart = Len(txtitemcode.Text)
                    'txtitemcode.SelectionLength = Len(txtitemcode.Text)
                    Exit Sub
                End If
            End If
        End If
    End Sub
    Private Sub txtitem_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtitem.TextChanged
        If chgbyprg = True Then Exit Sub
        _srchOnce = False
        _srchTxtId = 2
        ShowFmlist(sender)
    End Sub
    Private Sub loadWaite(ByVal triggerType As Integer)

        If Not fwait Is Nothing Then fwait.Close() : fwait = Nothing
        fwait = New WaitMessageFrm
        With fwait
            .triggerType = triggerType
            '.Visible = True
            .ShowDialog()
        End With
        If triggerType = 2 Then
            Timer1.Enabled = True
        Else
            txtitem.Focus()
        End If

    End Sub

    Private Sub txtitem_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtitem.Validated
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
        If txtitem.Text = "" Then Exit Sub
        Dim dt As DataTable = Nothing

        Dim qtyqry As String
        qtyqry = "SELECT Itemid,Description FROM InvItm WHERE [item code]='" & txtitem.Tag & "'"
        dt = _objcmnbLayer._fldDatatable(qtyqry)
        If dt.Rows.Count = 0 Then
            MsgBox("Item not found", MsgBoxStyle.Exclamation)
            chgbyprg = True
            txtitem.Text = ""
            chgbyprg = False
            Exit Sub
        Else
            itemid = dt(0)("Itemid")
            chgbyprg = True
            txtitem.Text = dt(0)("Description")
            chgbyprg = False

        End If


    End Sub

    Private Sub fMList_doSelect(ByVal ItmFlds() As String) Handles fMList.doSelect
        chgbyprg = True
        Select Case _srchTxtId
            Case 1
                txtcustomer.Text = ItmFlds(0)
                customeralias = ItmFlds(1)
            Case 2
                itemid = ItmFlds(3)
                txtitem.Text = ItmFlds(1)
                txtitem.Tag = ItmFlds(0)
        End Select
        chgbyprg = False
    End Sub

    Private Sub AmcList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Timer1.Enabled = True
        btnamcremove.Visible = False
        AddNew()
    End Sub

    Private Sub btnamcadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnamcadd.Click
        If txtinvoicenmr.Text = "" Then
            MsgBox("Please Eneter Invoice Number", MsgBoxStyle.Exclamation)
            txtinvoicenmr.Focus()
            Exit Sub
        ElseIf txtstmpddate.Text = "" Then
            MsgBox("Please Enter Stamped Date ", MsgBoxStyle.Exclamation)
            Exit Sub

        ElseIf txtnextstmpddate.Text = "" Then
            MsgBox("Please Enter Next Stamping Date ", MsgBoxStyle.Exclamation)
            txtnextstmpddate.Focus()
            Exit Sub

        ElseIf txtcustomer.Text = "" Then
            MsgBox("Please Select Customer ", MsgBoxStyle.Exclamation)
            txtcustomer.Focus()
            Exit Sub

        ElseIf txtitem.Text = "" Then
            MsgBox("Please Select Item ", MsgBoxStyle.Exclamation)
            txtitem.Focus()
            Exit Sub
        

        End If
        Dim qry As String
        Dim invoctag As Double
        invoctag = txtinvoicenmr.Tag
        If invoctag > 0 Then
            qry = "Update AmcCustomerTb set invoicedate='" & Format(cldrdate.Value, "yyyy/MM/dd") & "',customerid='" & _
            Val(txtcustomer.Tag) & "',itemid=" & itemid & ",stamped_date='" & _
            txtstmpddate.Text & "',nextstamped_date='" & txtnextstmpddate.Text & _
            "',amount='" & txtamt.Text & "',invoicenumber='" & txtinvoicenmr.Text & "',descr='" & txtdescr.Text & "' where id=" & Val(txtinvoicenmr.Tag)
            _objcmnbLayer._saveDatawithOutParm(qry)
        Else
            qry = "insert into AmcCustomerTb(invoicenumber,invoicedate,customerid,itemid,stamped_date,nextstamped_date,amount,descr) values('" & txtinvoicenmr.Text & "','" & Format(cldrdate.Value, "yyyy/MM/dd") & "','" & Val(txtcustomer.Tag) & "'," & itemid & ",'" & txtstmpddate.Text & "','" & txtnextstmpddate.Text & "','" & txtamt.Text & "','" & txtdescr.Text & "')"
            _objcmnbLayer._saveDatawithOutParm(qry)
        End If


        MsgBox("Updated", MsgBoxStyle.Information)
        loaddata()
        clearcontrol()
    End Sub

    Private Sub clearcontrol()
        chgbyprg = True
        txtcustomer.Text = ""
        txtitem.Text = ""
        txtstmpddate.Text = ""
        txtnextstmpddate.Text = ""
        txtamt.Text = ""
        txtdescr.Text = ""
        cldrdate.Value = DateValue(Date.Now)
        cldrdate.Focus()
        txtcustomer.Tag = ""
        txtitem.Tag = ""
        itemid = 0
        btnamcadd.Text = "Add"
        txtinvoicenmr.Tag = 0
        btnamcremove.Visible = False
        gridd = 0
        gridtrid = 0
        dtRports = Nothing
        chgbyprg = False
        AddNew()


    End Sub

    Private Sub drgv_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles drgv.CellClick
        gridd = drgv.Item("id", drgv.CurrentRow.Index).Value
        gridtrid = drgv.Item("trid", drgv.CurrentRow.Index).Value
    End Sub

    Private Sub drgv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles drgv.DoubleClick
        chgbyprg = True
        Dim gridd As Long
        gridd = drgv.Item("id", drgv.CurrentRow.Index).Value
        Dim gridtrid As Long
        gridtrid = drgv.Item("trid", drgv.CurrentRow.Index).Value
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable(" select AmcCustomerTb.*,AccDescr,Description,[item code] itemcode from AmcCustomerTb left join accmast on accmast.accid=AmcCustomerTb.customerid " & _
                                         "left join InvItm on  InvItm.ItemId=AmcCustomerTb.ItemId " & _
                                         " where id=" & gridd)

        If dt.Rows.Count > 0 Then
            txtinvoicenmr.Text = Trim(dt(0)("invoicenumber") & "")
            txtcustomer.Text = Trim(dt(0)("AccDescr") & "")
            txtcustomer.Tag = Val(dt(0)("customerid") & "")
            txtitem.Text = Trim(dt(0)("Description") & "")
            txtitem.Tag = Trim(dt(0)("itemcode") & "")
            txtstmpddate.Text = Trim(dt(0)("stamped_date") & "")
            txtnextstmpddate.Text = Trim(dt(0)("nextstamped_date") & "")
            cldrdate.Value = Trim(dt(0)("invoicedate") & "")
            txtinvoicenmr.Tag = dt(0)("id")
            itemid = dt(0)("itemid")
            txtdescr.Text = Trim(dt(0)("descr") & "")
            If Not IsDBNull(dt(0)("amount")) Then
                txtamt.Text = Format(CDbl(dt(0)("amount")), numFormat)
            Else
                txtamt.Text = Format(0, numFormat)
            End If

            cldrdate.Focus()
            btnamcadd.Text = "Update"

        End If

        chgbyprg = False
        If gridd = 0 And gridtrid > 0 Then
            MsgBox("Invoice can't be edit or remove", MsgBoxStyle.Exclamation)
        End If
        btnamcremove.Visible = True
    End Sub

    Private Sub btnamcremove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnamcremove.Click
        If txtinvoicenmr.Tag > 0 Then
            If MsgBox("Are you sure", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            _objcmnbLayer._saveDatawithOutParm(" delete AmcCustomerTb where id=" & Val(txtinvoicenmr.Tag))
            loaddata()
            clearcontrol()
        Else
            Exit Sub
        End If
    End Sub

    Private Sub loaddata()
        Dim Condition As String
        If rdoactive.Checked = False Then
            Condition = " where ishide =" & 1

        Else
            Condition = " where ISNULL(ishide, 0)=" & 0

        End If
        Dim qry As String
        qry = " select invoicenumber,invoicedate,AccDescr,InvItm.itemid,Description,descr,isnull(address1,'')address1,Phone," & _
                                         "stamped_date,nextstamped_date,amount,id,0 trid ,AmcCustomerTb.ishide from AmcCustomerTb left join accmast on accmast.accid=AmcCustomerTb.customerid " & _
                                         "left join InvItm on  InvItm.ItemId=AmcCustomerTb.ItemId " & _
                                         "left join AccMastAddr on AccMast.AccId=AccMastAddr.AccountNo"
        qry = "select *,1 lnk from (" & qry & " union all select TrRefNo,TrDate,CashCustName,InvItm.itemid,Description,info3," & _
                    "case when isnull(OthrCust,'')<>'' then  OthrCust else ISNULL(address1,'') end address1," & _
                    "case when isnull(customerphone,'')<>'' then  customerphone else ISNULL(Phone,'') end Phone, info1,info2,NetAmt,0,ItmInvCmnTb.trid,ItmInvCmnTb.ishide from ItmInvCmnTb left join ItmInvTrTb on ItmInvCmnTb.TrId=ItmInvTrTb.TrId " & _
                    "left join InvItm on ItmInvTrTb.ItemId=InvItm.ItemId " & _
                    "left join AccMast on ItmInvCmnTb.CSCode=AccMast.AccId " & _
                    "left join AccMastAddr on AccMast.AccId=AccMastAddr.AccountNo where isnull(info1,'')<>'' or ISNULL(info2,'')<>'') tr  " & Condition & " and tr.itemid > 0"
        dt = _objcmnbLayer._fldDatatable(qry)
        drgv.DataSource = dt
        setGridHead()
        With drgv
            Dim i As Integer = 0
            cmbSearch.Items.Clear()
            For i = 0 To .ColumnCount - 3
                cmbSearch.Items.Add(.Columns(i).HeaderText)
            Next
            If cmbSearch.Items.Count > 1 Then cmbSearch.SelectedIndex = 9
        End With
        clearcontrol()
    End Sub
    Private Sub setGridHead()
        SetGridProperty(drgv)
        With drgv
            .Columns("invoicenumber").Width = 100
            .Columns("invoicenumber").HeaderText = "Invoice "

            .Columns("invoicedate").Width = 75
            .Columns("invoicedate").HeaderText = "Date"

            .Columns("AccDescr").Width = 200
            .Columns("AccDescr").HeaderText = "Customer Name"

            .Columns("Description").Width = 220
            .Columns("Description").HeaderText = "Item"

            .Columns("stamped_date").Width = 120
            .Columns("stamped_date").HeaderText = "Stamped Date"

            .Columns("nextstamped_date").Width = 140
            .Columns("nextstamped_date").HeaderText = "Next Stamping Date"

            .Columns("descr").Width = 150
            .Columns("descr").HeaderText = "Description"

            .Columns("address1").Width = 250
            .Columns("address1").HeaderText = "Address"

            .Columns("Phone").Width = 120
            .Columns("Phone").HeaderText = "Phone"

            .Columns("id").Visible = False
            .Columns("trid").Visible = False
            .Columns("ishide").Visible = False
            .Columns("lnk").Visible = False
            .Columns("itemid").Visible = False

            '.Columns("amount").Width = 100
            '.Columns("amount").HeaderText = "Amounut"
            '.Columns("amount").DefaultCellStyle.Format = "N" & NoOfDecimal
            '.Columns("amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("amount").Visible = False


        End With
        resizeGridColumn(drgv, 3)
    End Sub

    Private Sub txtamt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtamt.KeyPress
        NumericTextOnKeypress(txtamt, e, chgbyprg, numFormat)
    End Sub
    Private Sub btnld_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnld.Click
        loaddata()
        dtRports = Nothing
    End Sub
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        loaddata()
    End Sub

    Private Sub cldrdate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cldrdate.KeyDown, txtstmpddate.KeyDown, txtnextstmpddate.KeyDown, txtamt.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        If chgbyprg Then Exit Sub
        Dim dtsearch As DataTable = SearchGrid(dt, Trim(txtSearch.Text), cmbSearch.SelectedIndex, Not chkSearch.Checked)
        dtRports = SearchGrid(dt, Trim(txtSearch.Text), cmbSearch.SelectedIndex, Not chkSearch.Checked)
        drgv.DataSource = dtsearch
        setGridHead()
    End Sub

    Private Sub btnclr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclr.Click
        clearcontrol()
    End Sub


    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        QuickCust(True, "Customer")
        'Dim Cname As String
        'Dim qry As String
        'qry = "SELECT TOP 1 AccDescr FROM AccMast ORDER BY AccId DESC"
        'Dim dtdd As DataTable = _objcmnbLayer._fldDatatable(qry)
        'Cname = dtdd.Rows(0)("AccDescr").ToString()
        'txtcustomer.Text = Cname
        'txtitem.Focus()

    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        If chkFormat.Checked Then
            fRptFormat = New RptFormatfrm
            fRptFormat.RptType = "AMCL"
            fRptFormat.ShowDialog()
            fRptFormat = Nothing
        Else
            PrepareRpt("AMCL")
        End If
    End Sub
    Private Sub PrepareRpt(ByVal RptType As String, Optional ByVal Forprint As Boolean = False, Optional ByVal pno As Integer = 0)

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
        If dtRports Is Nothing Then
            dtRports = SearchGrid(dt, "", cmbSearch.SelectedIndex, Not chkSearch.Checked)
        Else
            dtRports = Nothing
            If Not frm Is Nothing Then
                frm.Close()
                frm = New ReprtviewNEWfrm
            Else
                frm = New ReprtviewNEWfrm
            End If

            If dtRports Is Nothing Then
                dtRports = SearchGrid(dt, "", cmbSearch.SelectedIndex, Not chkSearch.Checked)
            End If
        End If
        ds.Tables.Add(dtRports)
        '_objInv.Prefix = txtPPrefix.Text
        '_objInv.InvNo = Val(numPrintVchr.Text)
        '_objInv.TrType = "AMC"
        'If _objInv.InvNo = 0 Then Exit Sub
        'Dim ds As DataSet
        'ds = _objInv.ldInvoice("rturnInventoryDetailsForInvoicePrint", pno)
        'If ToPrint Then
        '    _objcmnbLayer._saveDatawithOutParm("Update ItmInvCmnTb set Printed=1 where trid=" & Val(ds.Tables(0)(0)("trid")))
        'End If
        frm.SetReport(ds, FileName, 0, False, , ToPrint)
        frm.btnprint.Tag = Val(ds.Tables(0)(0)("trid"))
        frm.MdiParent = Me.MdiParent
        frm.Text = RptCaption
        If Not ToPrint Then frm.Show()
    End Sub
    Private Sub btnhide_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnhide.Click
        Dim hqy As String
        Dim hqry As String
        Dim Condition1 As String
        If rdoactive.Checked = True Then
            Condition1 = 1
            If gridd Or gridtrid > 0 Then
                If MsgBox(" Do you want to hide this customer ", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            End If
        Else
            Condition1 = 0
            If gridd Or gridtrid > 0 Then
                If MsgBox(" Do you want to unhide this customer ", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            End If
        End If
        If gridtrid > 0 Then
            hqry = " update ItmInvCmnTb set ishide =" & Condition1 & " where TrId =" & gridtrid & ""
            _objcmnbLayer._saveDatawithOutParm(hqry)
            loaddata()
            clearcontrol()
        ElseIf gridd > 0 Then
            hqy = " update AmcCustomerTb set ishide =" & Condition1 & " where id=" & gridd & ""
            _objcmnbLayer._saveDatawithOutParm(hqy)
            loaddata()
            clearcontrol()
        Else
            Exit Sub
        End If
    End Sub

    Private Sub rdohide_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdohide.CheckedChanged
        btnhide.Text = "UnHide"
        loaddata()
        clearcontrol()

    End Sub

    Private Sub rdoactive_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoactive.CheckedChanged
        clearcontrol()
        btnhide.Text = "Hide"
    End Sub
    Public Sub QuickCust(Optional ByRef bOnlyOne As Boolean = False, Optional ByVal Grp As String = "Customer")
        fCrtAcc = New CreateAccNew
        With fCrtAcc
            .Condition = "GrpSetOn In ('" & Grp & "')"
            If Grp = "Customer" Then
                .iscust = True
            Else
                .iscust = False
            End If
            .bOnlyOne = bOnlyOne
            .ShowDialog()
            txtitem.Focus()
            fCrtAcc = Nothing
        End With
    End Sub
    Private Sub fCrtAcc_OpenAccMaster(ByRef AccountNo As Long) Handles fCrtAcc.OpenAccMaster
        loadCustomerDetails(AccountNo)
    End Sub
    Private Sub loadCustomerDetails(ByVal accid As Long)
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
        If txtcustomer.Text = "" And accid = 0 Then Exit Sub
        Dim dt As DataTable
        _objcmnbLayer = New clsCommon_BL
        Dim str As String
        If accid > 0 Then
            str = "SELECT accid,Alias,AccDescr,Address1,Address2,Address3,Address4,Phone,Fax,EMail,PT1,PT2,PT3,PT4," & _
                  "TrdLcno,TrdDate,ContactName,CountryCode from AccMast LEFT JOIN AccMastAddr ON AccMast.ACCID=AccMastAddr.accountno where accid=" & accid
        Else
            str = "SELECT accid,Alias,AccDescr,Address1,Address2,Address3,Address4,Phone,Fax,EMail,PT1,PT2,PT3,PT4," & _
                  "TrdLcno,TrdDate,ContactName,CountryCode from AccMast LEFT JOIN AccMastAddr ON AccMast.ACCID=AccMastAddr.accountno where AccDescr='" & txtcustomer.Text & "'"
        End If

        dt = _objcmnbLayer._fldDatatable(str)
        chgbyprg = True
        If dt.Rows.Count > 0 Then

            txtcustomer.Text = UCase(dt(0)("AccDescr"))
            txtcustomer.Tag = dt(0)("accid")

        Else
            txtcustomer.Text = ""
        End If
        chgbyprg = False
    End Sub

    Private Sub txtdescr_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtdescr.KeyDown
        lstKey = e.KeyCode
        If e.KeyCode <> Keys.Escape Then
            isShowItems = True
        Else
            isShowItems = False
        End If
        If e.KeyCode = Keys.Return Then
            btnamcadd.Focus()
        End If
    End Sub
    Private Sub AddNew()
        chgbyprg = True
        txtinvoicenmr.Text = GenerateNext("")
        chgbyprg = False
    End Sub
    Private Function GenerateNext(ByVal strCode As String) As String
        Dim N As Double
        Dim NewCode As String
        Dim i As Byte
        Dim f As Byte
        Dim tmp As String
        Dim dt As DataTable
        If strCode = "" Then
            dt = _objcmnbLayer._fldDatatable("SELECT top 1 invoicenumber from AmcCustomerTb " & _
                                             " order by AmcCustomerTb.id desc")
            If dt.Rows.Count > 0 Then
                strCode = dt(0)(0)
            End If
        End If
        If strCode = "" Then
            strCode = "AMC"
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
                dr = _objcmnbLayer._fldDatatable("SELECT invoicenumber from AmcCustomerTb WHERE invoicenumber= '" & tmp & "'")
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

            Private Sub txtinvoicenmr_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtinvoicenmr.KeyDown
        If e.KeyCode = Keys.Return Then
            cldrdate.Focus()
        End If
    End Sub
 
    Private Sub txtdescr_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtdescr.TextChanged

    End Sub

    Private Sub txtamt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtamt.TextChanged

    End Sub
End Class