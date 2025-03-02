Imports MoseActivationDll
Imports System.Net

Public Class Homefrm
#Region "Object Variable"
    Private _objInv As clsInvoice
    Private _objcmnbLayer As New clsCommon_BL
    Private objactivation As New activationdll
#End Region
#Region "Form Objects"
    Private WithEvents fJob As ServiceJob
    Private WithEvents fAddwty As AddtoWarrenty
    Private WithEvents fcustomer As FindCustomerSerialNumberFrm
    Private WithEvents fwait As WaitMessageFrm
    Private WithEvents fTdashboard As TailorDashboardFrm
    Private WithEvents fis As MFSalesInvoice
    Private inOnline As Boolean
    Private licenseBlocked As Boolean
    Private isactive As Integer
#End Region

    Private CompanyTb As DataTable
    Private Sub Homefrm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated

    End Sub

    Private Sub Homefrm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If Not fTdashboard Is Nothing Then fTdashboard.Close() : fTdashboard = Nothing
    End Sub

    Private Sub Homefrm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave

    End Sub

    Private Sub Homefrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim result As Integer
        'result = productvalidation()
        If CurrentUser = "SFMODE" Then result = 1 : GoTo ext
        If Environment.MachineName <> "DEVELOPER" And Environment.MachineName <> "LAPTOP-5BG0P5ON" And Environment.MachineName <> "DESKTOP-FV7R24S" And _
        Environment.MachineName <> "MOSE-PC" And Environment.MachineName <> "DEVI" And CurrentUser <> "PROGRAMMER" Then
            result = productvalidation()
        Else
            result = 1
        End If
        If result = 0 Then
            End
        End If
ext:
        Timer2.Enabled = True
        'If enableAutoSync Then openTransfer()
    End Sub
    Private Function productvalidation()
checkagin:
        Dim entityId As String = ""
        Dim targetID As String = ""
        Dim entityKEY As String = ""
        Dim version As String = My.Application.Info.Version.ToString
        Dim productkey As String = ""
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select CompName,LICENSEKEY from companyTb")
        If dt.Rows.Count > 0 Then
            entityId = dt(0)("CompName")
            productkey = Trim(dt(0)("LICENSEKEY") & "")
        End If
        targetID = Environment.MachineName
        If productkey = "" Then productkey = "mosedemo"
        version = version.Replace(".", "")
        Dim dtstring As String = Format(DateValue(Now.Date), "yyyyMMdd")
        entityKEY = objactivation.generateKEY(dtstring, entityId, targetID, version, "007", productkey)
        Dim objen As New encriptTex
        entityKEY = objen.Encrypt(entityKEY)
        Dim result As Integer = objactivation.checkActivated(entityId, entityKEY, targetID)
        If result = 0 And (entityId = "MOSE DEMO VERSION" Or entityId = "MOSE DEMO VAT") Then
            objactivation.saveActivation(entityKEY, entityId, targetID, "", "mosedemo")
            result = 2
        End If
        If result = 0 Then
            If Not fwait Is Nothing Then fwait.Close() : fwait = Nothing
            Timer2.Enabled = False
            Dim frm As New ActivateFrm
            frm.ShowDialog()
            frm = Nothing
            GoTo checkagin
        End If
        Return result
    End Function

    Private Sub setPermission()
        If (getRight(51, CurrentUser)) Then
            btninvoicelist.Enabled = True
        Else
            btninvoicelist.Enabled = False
        End If
        If (getRight(71, CurrentUser)) Then
            btnfinStatus.Enabled = True
        Else
            btnfinStatus.Enabled = False
        End If
        If (getRight(49, CurrentUser)) Then
            btnquantitylist.Enabled = True
        Else
            btnquantitylist.Enabled = False
        End If
        If (getRight(201, CurrentUser)) Then
            btnvoucherlist.Enabled = True
        Else
            btnvoucherlist.Enabled = False
        End If
        If (getRight(257, CurrentUser)) Then
            enableuserwisetransactionlist = False
        End If

    End Sub
    Private Sub LdCompDet()

        _objcmnbLayer = New clsCommon_BL
        Dim qry As String
        Dim dtset As DataSet
        Dim defAccstring As String = ""
        Dim i As Integer
        If UsrBr = "" Then
            For i = 1 To 27
                defAccstring = defAccstring & IIf(defAccstring = "", "", " UNION ALL ") & " Select accid," & i & " setno from accmast where AccSetId like '%" & Format(i, "00") & "%'"
            Next
        Else
            defAccstring = " SELECT accid,setno FROM BranchAccSet WHERE branchcode='" & UsrBr & "'"
        End If
        qry = "SELECT * FROM CompanyTb"
        qry = qry & " select GSTTb.*,CGSTCAname,CGSTPAname,SGSTCAname,SGSTPAname,IGSTCAname,IGSTPAname from GSTTb " & _
              "left join (select accid CGSTCAcId,AccDescr CGSTCAname  from AccMast) CGSTC on CGSTC.CGSTCAcId =GSTTb.CGSTCAc " & _
              "left join (select accid CGSTPAcId,AccDescr CGSTPAname  from AccMast) CGSTP on CGSTP.CGSTPAcId =GSTTb.CGSTPAc " & _
              "left join (select accid SGSTCAcId,AccDescr SGSTCAname  from AccMast) SGSTC on SGSTC.SGSTCAcId =GSTTb.SGSTCAc " & _
              "left join (select accid SGSTPAcId,AccDescr SGSTPAname  from AccMast) SGSTP on SGSTP.SGSTPAcId =GSTTb.SGSTPAc " & _
              "left join (select accid IGSTCAcId,AccDescr IGSTCAname  from AccMast) IGSTC on IGSTC.IGSTCAcId =GSTTb.IGSTCAc " & _
              "left join (select accid IGSTPAcId,AccDescr IGSTPAname  from AccMast) IGSTP on IGSTP.IGSTPAcId =GSTTb.IGSTPAc " & defAccstring
        'qry = qry & " Select top 1 LinkNo,AccountNo,DueDate,Reference,EntryRef,DealAmt,FCAmt,CurrencyCode,CurrRate, " & _
        '                                          "JobCode,PDCAcc,BankCode,LPONo,OthCost,TrInf,TermsId,CustAcc,AccWithRef,ChqDate,ChqNo,SuppInvDate," & _
        '                                          "vatid,isvatEntry,UnqNo,setoffCount from AccTrDet"
        qry = qry & " Select top 1 * from AccTrDet"
        qry = qry & " Select top 1 * from ItmInvTrTb"
        qry = qry & " SELECT * FROM VoucherTypeNoTb "
        qry = qry & " Select dpath from SystemDocPathTb where systemname='" & MACHINENAME & "'" & IIf(BranchId > 0, " and brid=" & BranchId, "")
        dtset = _objcmnbLayer._ldDataset(qry, False)
        CompanyTb = dtset.Tables(0)
        dtGST = dtset.Tables(1)
        dtDefAcc = dtset.Tables(2)
        dtAccTb = dtset.Tables(3)
        dtInvTb = dtset.Tables(4)
        dtvoucherTypes = dtset.Tables(5)
        If dtset.Tables(6).Rows.Count > 0 Then
            DPath = Trim(dtset.Tables(6)(0)("dpath") & "")
        End If

    End Sub
    Private Sub setCompanyDet()
        lblcompanyname.Text = ""
        lbladdress1.Text = ""
        lbladdress2.Text = ""
        lbladdress3.Text = ""
        'lbladdress4.Text = ""
        lblphone.Text = ""
        lblfax.Text = ""
        If CompanyTb.Rows.Count > 0 Then
            lblcompanyname.Text = UCase(Trim("" & CompanyTb(0)("compName")))
            lbladdress1.Text = Trim("" & CompanyTb(0)("Addr1"))
            lbladdress2.Text = Trim("" & CompanyTb(0)("Addr2"))
            lbladdress3.Text = Trim("" & CompanyTb(0)("Addr3"))
            'lbladdress4.Text = Trim("" & CompanyTb(0)("Addr4"))
            lblphone.Text = Trim("Phone :" & CompanyTb(0)("Tel1"))
            lblfax.Text = Trim("Fax :" & CompanyTb(0)("Tel2"))
            lblemail.Text = Trim("Email :" & CompanyTb(0)("Email"))
            defaultState = Trim("" & CompanyTb(0)("statecode"))
            If Not IsDBNull(CompanyTb(0)("calcluatetaxFrompriceInv")) Then
                calcluatetaxFrompriceInv = CompanyTb(0)("calcluatetaxFrompriceInv")
            End If
            If Not IsDBNull(CompanyTb(0)("calcluatetaxFrompricedoc")) Then
                calcluatetaxFrompricedoc = CompanyTb(0)("calcluatetaxFrompricedoc")
            End If
            If Not IsDBNull(CompanyTb(0)("searchStartOnly")) Then
                searchStartOnly = CompanyTb(0)("searchStartOnly")
            End If
            If Not IsDBNull(CompanyTb(0)("AccPeriodFrm")) Then
                DateFrom = DateValue(CompanyTb(0)("AccPeriodFrm"))
            Else
                DateFrom = DateValue("01/01/1950")
            End If
            If Not IsDBNull(CompanyTb(0)("AccPeriodTo")) Then
                DateTo = DateValue(CompanyTb(0)("AccPeriodTo"))
            Else
                DateTo = DateValue("01/01/1950")
            End If
            If Not IsDBNull(CompanyTb(0)("priceInSales")) Then
                priceInSales = CompanyTb(0)("priceInSales")
            End If
            If Not IsDBNull(CompanyTb(0)("searchByCodeInInventory")) Then
                searchByCodeInInventory = CompanyTb(0)("searchByCodeInInventory")
            End If
            If Not IsDBNull(CompanyTb(0)("searchfulltext")) Then
                searchfulltext = CompanyTb(0)("searchfulltext")
            End If
            If DPath = "" Then
                If Not IsDBNull(CompanyTb(0)("DataPath")) Then
                    DPath = CompanyTb(0)("DataPath")
                End If
            End If
            If Not IsDBNull(CompanyTb(0)("cessenddate")) Then
                cessenddate = CompanyTb(0)("cessenddate")
            End If
            If Not IsDBNull(CompanyTb(0)("calcluatetaxFromSpriceInIP")) Then
                calcluatetaxFromSpriceInIP = CompanyTb(0)("calcluatetaxFromSpriceInIP")
            End If
            If IsDBNull(CompanyTb(0)("ProtectUntil")) Then
                ProtectUntil = DateSerial(1950, 1, 1)
            Else
                ProtectUntil = CompanyTb(0)("ProtectUntil")
            End If
            firstDateFromToday = Val(CompanyTb(0)("firstDateFromToday") & "")

            ftpurl = Trim("" & CompanyTb(0)("ftpurl"))
            ftpusername = Trim("" & CompanyTb(0)("ftpusername"))
            ftppassword = Trim(CompanyTb(0)("ftppassword") & "")

            lblaccountp.Text = DateFrom & " : TO : " & DateTo
            setExtraPara(CompanyTb, True)
            SetSystemProperties()
            With fMainForm
                .mnujob.Visible = enableServiceJob
                .btnjob.Visible = enableServiceJob
                .btnTracking.Visible = enableServiceJob
                .lblcompany.Text = lblcompanyname.Text & IIf(enableBranch, " ( " & UsrBr & " )", "")
                .lblcompany.Left = (.Width / 2) - (.lblcompany.Width / 2)
                .setPermissionOnLoad()
            End With
            If Not IsDBNull(CompanyTb(0)("enableitBin")) Then
                plitbin.Visible = CompanyTb(0)("enableitBin")
            End If
        End If
        If DPath <> "" Then
            If Mid(DPath, Len(DPath)) <> "\" Then
                DPath = DPath & "\"
            End If
            If Not FileExists(Application.StartupPath & "\Logo.vin") Then
                If FileExists(DPath & "Logo.vin") Then
                    FileCopy(DPath & "Logo.vin", Application.StartupPath & "\Logo.vin")
                Else
                    picLogo.Visible = False
                End If
            End If
            lblpath.Text = DPath
        End If
        fwait.Close()


    End Sub
    Private Sub loadSerialNoDetails()
        Dim ds As DataSet
        _objInv = New clsInvoice
        Dim itmnotFound As Boolean
        With _objInv
            .SerialNo = IIf(txtserialno.Text = "", " ", txtserialno.Text)
            'MsgBox(Len(txtserialno.Text))
            ds = .returnSerialNoDetails(IIf(chkdualsim.Checked, 1, 0))
            'If txtserialno.Text = "" Then
            '    ds.Tables.Clear()
            'End If
        End With
        If ds.Tables(0).Rows.Count > 0 Then
            txtserialno.Text = ds.Tables(0)(0)("serialno")
            lblitem.Text = ": " & Trim(ds.Tables(0)(0)("Description") & "") '& "[" & ds.Tables(0)(0)("Item Code") & "]"
            lblsupplier.Text = ": " & Trim(ds.Tables(0)(0)("SupplierName") & "")
            lblpurno.Text = ": " & Trim(ds.Tables(0)(0)("PurchaseNo") & "")
            lblsupno.Text = ": " & Trim(ds.Tables(0)(0)("TrRefNo") & "")
            If Not IsDBNull(ds.Tables(0)(0)("PurDate")) Then
                lblpurdate.Text = ": " & ds.Tables(0)(0)("PurDate")
            Else
                lblpurdate.Text = ":"
            End If
            lblcustomer.Text = ": " & Trim(ds.Tables(0)(0)("CustomerName") & "")
            lblcustomer.Tag = Val(ds.Tables(0)(0)("Custid") & "")
            'If Not IsDBNull(ds.Tables(0)(0)("SDate")) Then
            '    lblsalesDate.Text = ": " & ds.Tables(0)(0)("SDate")
            'Else
            '    lblsalesDate.Text = ": "
            '    lblWstatus.Text = ": Sales Not Found"
            '    lblWstatus.ForeColor = Color.Blue
            '    GoTo sup
            'End If

            If Not IsDBNull(ds.Tables(0)(0)("ExpDate")) Then
                Dim dt1 As Date = Format(DateValue(ds.Tables(0)(0)("ExpDate")), DtFormat)
                Dim dt2 As Date = Format(DateValue(Date.Now), DtFormat)
                If dt1 >= dt2 Then
                    If ds.Tables(0)(0)("Warrenty") = 1 Then
                        lblWstatus.Text = ": On Warranty"
                        lblWstatus.ForeColor = Color.Green
                    Else
                        lblWstatus.Text = ": No Warranty"
                        lblWstatus.ForeColor = Color.Red
                    End If
                Else
                    lblWstatus.Text = ": No Warranty"
                    lblWstatus.ForeColor = Color.Red
                End If
            Else
                lblWstatus.Text = ": No Warranty"
                lblWstatus.ForeColor = Color.Red
            End If
            If Val(ds.Tables(0)(0)("Warrenty") & "") = 2 Then
                lblWstatus.Text = ": Cancelled"
                lblWstatus.ForeColor = Color.Blue
                GoTo sup
            End If
            If Not IsDBNull(ds.Tables(0)(0)("SDate")) Then
                lblsalesDate.Text = ": " & ds.Tables(0)(0)("SDate")
            Else
                lblsalesDate.Text = ": "
                lblWstatus.Text = ": Sales Not Found"
                lblWstatus.ForeColor = Color.Blue
            End If
sup:
            If Not IsDBNull(ds.Tables(0)(0)("ExpDate")) Then
                lblexpiry.Text = ": " & ds.Tables(0)(0)("ExpDate")
            Else
                lblexpiry.Text = ": "
            End If
            lblSalesNo.Text = ": " & Trim(ds.Tables(0)(0)("SalesNo") & "")
            If ds.Tables(1).Rows.Count > 0 Then
                lblwarrenty.Text = ": " & Trim(ds.Tables(1)(0)("WarrentyName") & "")
            End If
            If Not IsDBNull(ds.Tables(0)(0)("SupWarrentyDt")) Then
                If DateValue(ds.Tables(0)(0)("SupWarrentyDt")) >= DateValue(Date.Now) Then
                    lblisupwarrentySatatus.Text = "[On Warranty]"
                    lblisupwarrentySatatus.ForeColor = Color.Green
                ElseIf DateValue(ds.Tables(0)(0)("SupWarrentyDt")) = DateValue("01/01/1950") Then
                    lblisupwarrentySatatus.Text = "[No warranty]"
                    lblisupwarrentySatatus.ForeColor = Color.Red
                Else
                    lblisupwarrentySatatus.Text = "[Warranty Expired]"
                    lblisupwarrentySatatus.ForeColor = Color.Red
                End If
                If DateValue(ds.Tables(0)(0)("SupWarrentyDt")) = DateValue("01/01/1950") Then
                    lblSupExdate.Text = ": N/A"
                Else
                    lblSupExdate.Text = ": " & ds.Tables(0)(0)("SupWarrentyDt")
                End If

            End If
        Else
            lblWstatus.Text = ": No Warranty"
            lblWstatus.ForeColor = Color.Red
            itmnotFound = True
            lblitem.Text = ": "
            lblsupplier.Text = ": "
            lblpurno.Text = ": "
            lblpurdate.Text = ":"
            lblcustomer.Text = ": "
            lblsalesDate.Text = ": "
            lblSalesNo.Text = ": "
            lblwarrenty.Text = ": "
            lblisupwarrentySatatus.Text = ": "
            lblsupno.Text = ": "
            lblSupExdate.Text = ": "
            lblWstatus.Text = ": "
            lblexpiry.Text = ": "
            'txtserialno.Text = ""
        End If
        'job details
        If ds.Tables(2).Rows.Count = 0 And itmnotFound = True Then
            MsgBox("Invalid SerialNo.", MsgBoxStyle.Exclamation)
            lstContent.Items.Clear()
            Exit Sub
        End If
        lstContent.Items.Clear()
        For i = 0 To ds.Tables(2).Rows.Count - 1
            With lstContent
                lblcustomer.Text = ": " & Trim(ds.Tables(2)(i)("AccDescr") & "")
                .Items.Add(ds.Tables(2)(i)("Jobcode"))
                If .Items.Item(i).SubItems.Count > 1 Then
                    .Items.Item(i).SubItems(1).Text = .Items.Add(ds.Tables(2)(i)("jobdate"))
                Else
                    .Items.Item(i).SubItems.Insert(1, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, ds.Tables(2)(i)("jobdate")))
                End If
                If .Items.Item(i).SubItems.Count > 2 Then
                    .Items.Item(i).SubItems(2).Text = ds.Tables(2)(i)("Complaints")
                Else
                    .Items.Item(i).SubItems.Insert(2, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, ds.Tables(2)(i)("Complaints")))
                End If
                If .Items.Item(i).SubItems.Count > 3 Then
                    .Items.Item(i).SubItems(3).Text = ds.Tables(2)(i)("Onwarrenty")
                Else
                    .Items.Item(i).SubItems.Insert(3, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, ds.Tables(2)(i)("Onwarrenty")))
                End If
                If .Items.Item(i).SubItems.Count > 4 Then
                    .Items.Item(i).SubItems(4).Text = ds.Tables(2)(i)("Observation")
                Else
                    .Items.Item(i).SubItems.Insert(4, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, ds.Tables(2)(i)("Observation")))
                End If
                If .Items.Item(i).SubItems.Count > 5 Then
                    .Items.Item(i).SubItems(5).Text = ds.Tables(2)(i)("TechRemarks")
                Else
                    .Items.Item(i).SubItems.Insert(5, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, ds.Tables(2)(i)("TechRemarks")))
                End If
                If .Items.Item(i).SubItems.Count > 6 Then
                    .Items.Item(i).SubItems(6).Text = ds.Tables(2)(i)("jobid")
                Else
                    .Items.Item(i).SubItems.Insert(6, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, ds.Tables(2)(i)("jobid")))
                End If
            End With
        Next
    End Sub

    Private Sub txtserialno_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtserialno.KeyDown
        If e.KeyCode = Keys.Enter Then
            'If Len(txtserialno.Text) < 13 Then
            '    MsgBox("Invalid Serial No", MsgBoxStyle.Exclamation)
            '    Exit Sub
            'End If
            lstContent.Items.Clear()
            loadSerialNoDetails()
        ElseIf e.KeyCode = Keys.F2 Then
            fcustomer = New FindCustomerSerialNumberFrm
            fcustomer.ShowDialog()
            fcustomer = Nothing
        End If
    End Sub


    Private Sub btnaddwarrenty_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddwarrenty.Click
        If txtserialno.Text = "" Then
            MsgBox("Invalid IMEI Number", MsgBoxStyle.Exclamation)
            txtserialno.Focus()
            Exit Sub
        End If
        If Not fAddwty Is Nothing Then fAddwty.Close() : fAddwty = Nothing
        fAddwty = New AddtoWarrenty
        With fAddwty
            .lblserialno.Text = txtserialno.Text
            .ldWarrentyTransactionDetails()
            .ShowDialog()
            loadSerialNoDetails()
        End With
    End Sub

    Private Sub cmdAddnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddnew.Click
        If txtserialno.Text = "" Then
            MsgBox("Invalid IMEI Number", MsgBoxStyle.Exclamation)
            txtserialno.Focus()
            Exit Sub
        End If
        fJob = New ServiceJob
        With fJob
            .AddRowImei(txtserialno.Text, IIf(chkwarrenty.Checked, "Y", ""), Val(lblcustomer.Tag))
            .MdiParent = fMainForm
            .Show()
        End With

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        lbltime.Text = Date.Now
    End Sub

    Private Sub btnwaarentyCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnwaarentyCancel.Click
        If txtserialno.Text = "" Then
            MsgBox("Invalid IMEI Number", MsgBoxStyle.Exclamation)
            txtserialno.Focus()
            Exit Sub
        End If
        With WarrentyCancel
            .cldrdate.Tag = txtserialno.Text
            .ShowDialog()
        End With

    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        lblitem.Text = ": "
        lblsupplier.Text = ": "
        lblpurno.Text = ": "
        lblpurdate.Text = ":"
        lblcustomer.Text = ": "
        lblsalesDate.Text = ": "
        lblSalesNo.Text = ": "
        lblwarrenty.Text = ": "
        lblisupwarrentySatatus.Text = ": "
        lblsupno.Text = ": "
        lblSupExdate.Text = ": "
        lblWstatus.Text = ": "
        lblexpiry.Text = ": "
        lblcustomer.Tag = ""
        lstContent.Items.Clear()
        txtserialno.Text = ""
    End Sub

    Private Sub fAddwty_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fAddwty.FormClosed
        fAddwty = Nothing
    End Sub

    Private Sub btnTracking_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTracking.Click
        grptracking.Height = Me.Height - 40
        grptracking.Visible = Not grptracking.Visible
        txtserialno.Focus()
    End Sub

    Private Sub btninvoicelist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btninvoicelist.Click
        fMainForm.ldViewPrint("IS")

    End Sub

    Private Sub btnquantitylist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnquantitylist.Click
        fMainForm.ldQuantity()
    End Sub

    Private Sub btnvoucherlist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnvoucherlist.Click
        fMainForm.LdVoucherlist()
    End Sub

    Private Sub txtserialno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtserialno.TextChanged

    End Sub

    Private Sub btnfinStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnfinStatus.Click
        fMainForm.loadFinancialStatus()
    End Sub

    Private Sub fcustomer_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fcustomer.FormClosed
        fcustomer = Nothing
    End Sub

    Private Sub fcustomer_selectcust(ByVal custname As String, ByVal Cashcustid As Long, ByVal serialNumber As String) Handles fcustomer.selectcust
        lblcustomer.Text = ": " & custname
        lblcustomer.Tag = Cashcustid
        txtserialno.Text = serialNumber
        txtserialno.Focus()
        lstContent.Items.Clear()
        loadSerialNoDetails()
    End Sub

    Private Sub lstContent_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstContent.DoubleClick
        If lstContent.Items.Count = 0 Then Exit Sub

        With lstContent
            If Val(.SelectedItems(0).SubItems(6).Text) = 0 Then Exit Sub
            fMainForm.loadJob(Val(.SelectedItems(0).SubItems(6).Text))
        End With

    End Sub

    Private Sub worker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles worker.DoWork
        LdCompDet()
        inOnline = CheckForInternetConnection()
        On Error GoTo err
        If inOnline And isactive = 0 Then
            If Environment.MachineName <> "DEVELOPER" And _
            Environment.MachineName <> "VINVIS-PC" And Environment.MachineName <> "MOSE-PC" And CurrentUser <> "PROGRAMMER" Then
                isactive = productRegularvalidation()
            Else
                isactive = 1
            End If
            If isactive = 0 Then
                MsgBox("Invalid License! please contact vendor", MsgBoxStyle.Critical)
                licenseBlocked = True
            End If


            'ftransferData.Hide()
            'Else
            '    If Not ftransferData Is Nothing Then ftransferData.Close() : ftransferData = Nothing
        End If
        'If inOnline And enableAutoSync Then
        '    updateItemdetailstoOnline(1)
        '    ExportImportAccount()
        '    ExportData()
        '    ImportData()
        'End If
        'Exit Sub
err:
        Timer2.Tag = 1
    End Sub

    Private Sub worker_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles worker.RunWorkerCompleted
        setCompanyDet()

    End Sub

    Private Sub fwait_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fwait.FormClosed
        fwait = Nothing
        Timer3.Enabled = True
    End Sub

    Private Sub fwait_triggerEvent() Handles fwait.triggerEvent
        worker.RunWorkerAsync()
    End Sub

    Private Sub Timer2_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Timer2.Enabled = False
        Dim constring, sbstring, MyServer As String
        constring = readXml()
        Dim _dtgetpsswrd As New DataTable
        '**server
        sbstring = Mid(constring, 1, constring.IndexOf(";"))
        sbstring = Mid(sbstring, sbstring.IndexOf("=") + 2)
        If sbstring.IndexOf("\") > 0 Then
            MyServer = Mid(sbstring, 1, sbstring.IndexOf("\"))
        Else
            MyServer = sbstring

        End If


        lbluser.Text = CurrentUser
        lblserver.Text = MyServer
        lbldatabase.Text = MyDatabase
        'LdCompDet()
        fwait = New WaitMessageFrm
        fwait.ShowDialog()
        btnTracking.Visible = enableSerialnumber And Not enableClinic
        If userType Then
            setPermission()
        End If
        fMainForm.loadRefreshcost()
        If enableWebIntegration Then
            fMainForm.loadAutoUpdate()
        End If

    End Sub


    Private Sub fTdashboard_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fTdashboard.FormClosed
        fTdashboard = Nothing
    End Sub


    Private Sub fis_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fis.FormClosed
        fis = Nothing
    End Sub

    Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        Timer3.Enabled = False
        With fMainForm
            .ToolStrip1.Visible = True
            Timer4.Enabled = True
            .MenuStrip.Visible = True
        End With
    End Sub

    Private Sub Timer4_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer4.Tick
        Timer4.Enabled = False
        'With fMainForm
        '    .MenuStrip.Visible = True
        'End With
    End Sub
    Public Shared Function CheckForInternetConnection() As Boolean
        Try
            Using client = New WebClient()
                Using stream = client.OpenRead("http://www.google.com")
                    Return True
                End Using
            End Using
        Catch
            Return False
        End Try
    End Function
    Private Function productRegularvalidation() As Integer
        Dim entityId As String = ""
        Dim targetID As String = ""
        Dim entityKEY As String = ""
        Dim version As String = My.Application.Info.Version.ToString
        Dim productkey As String = ""
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select CompName,LICENSEKEY from companyTb")
        If dt.Rows.Count > 0 Then
            entityId = dt(0)("CompName")
            productkey = Trim(dt(0)("LICENSEKEY") & "")
        End If
        targetID = Environment.MachineName
        If productkey = "" Then productkey = "mosedemo"
        version = version.Replace(".", "")
        Dim dtstring As String = Format(DateValue(Now.Date), "yyyyMMdd")
        entityKEY = objactivation.generateKEY(dtstring, entityId, targetID, version, "007", productkey)
        Dim objen As New encriptTex
        entityKEY = objen.Encrypt(entityKEY)
        Dim result As Integer = objactivation.checkProductKeyStatus(productkey, entityId, targetID, entityKEY, version, "Ultimate")
        If result > 0 Then
        Else
            _objcmnbLayer._saveDatawithOutParm("update companyTb set LICENSEKEY='' delete from EntityTb where targetID='" + targetID + "'")
        End If
        Return result
    End Function
End Class