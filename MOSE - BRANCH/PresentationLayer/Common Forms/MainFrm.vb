Imports System.Windows.Forms

Public Class MainFrm
#Region "Form Objects"
    Private WithEvents fwait As WaitMessageFrm
    Private WithEvents fhome As Homefrm
    Private WithEvents fparameter As Companyfrm
    Private WithEvents fView As InvReportFrm
    Private WithEvents fViewDOC As DocReportFrm
    Private WithEvents fViewWS As InvReportFrm
    Private WithEvents fProductionView As ProductionInvReport
    Private WithEvents fFuelView As FuelInvReportFrm
    Private WithEvents fproductMast As ItemMastFrm
    Private WithEvents fPI As PurchaseInvoiceFrm
    Private WithEvents fPIFruit As FruitPurchaseInvoiceFrm
    Private WithEvents fPR As PurchaseReturnInvoice
    Private WithEvents fFruitPR As FruitPurchaseReturnInvoice
    Private WithEvents fSR As SalesReturnInvoice
    Private WithEvents fFruitSR As FruitSalesReturnInvoice
    Private WithEvents fJV As JournalVoucher
    Private WithEvents fPV As SupplierPayments
    Private WithEvents fRV As CustomerReceipt
    Private WithEvents fPVO As ExpensePayments
    Private WithEvents fRVO As OtherReceiptsFrm
    Private WithEvents fCrtAcc As CreateAccNew
    Private WithEvents fQtyReport As QtyReport
    Private WithEvents fFinancialRep As FinancialStatements
    Private WithEvents fFinancialStat As FinancialStatus
    Private WithEvents fLevel As LevelMasterFrm
    Private WithEvents fJOB As ServiceJob
    Private WithEvents fEdtJOB As ServiceJob
    Private WithEvents fJOBMST As JobMaster
    Private WithEvents fJOBLst As JobList
    Private WithEvents fJOBLst1 As JobList
    Private WithEvents fJOBLst2 As JobList
    Private WithEvents fJOBLst3 As JobList
    Private WithEvents fJOBLst4 As JobList
    Private WithEvents fshowAlert As ShowAlert
    Private WithEvents fserialNo As AvailableSerialNumberListFrm
    Private WithEvents fTempleAdmission As TempleAdmissionFrm
    Private WithEvents fParishAdmission As New ChurchAdmissionFrm
    Private WithEvents fVazhipaduSales As VazhipaduSalesFrm
    Private WithEvents fVazhipaduSalesN As VazhipaduSalesNewFrm
    Private WithEvents fJOBPendingLst As JobPendingAssign
    Private WithEvents fchurchSales As ChurchSalesFrm

    Private WithEvents fJOBLst5 As JobList
    Private WithEvents fJOBLst6 As JobList
    Private WithEvents fJOBLst7 As JobList
    Private WithEvents fJOBLst8 As JobList
    Private WithEvents fJOProfitanalysis As JobList
    Private WithEvents fJOBLstDelivey As JobList

    Private WithEvents fCJOBLst As ContractJobList
    Private WithEvents fCJOBPLst As ContractJobList

    Private WithEvents fImeiNo As IMEINos
    Private WithEvents fImeiNo1 As IMEINos
    Private WithEvents fImeiNo2 As IMEINos
    Private WithEvents fImeiNo3 As IMEINos
    Private WithEvents fIS As SalesInvoice
    Private WithEvents fFruitIS As FruitSalesInvoice
    Private WithEvents efIS As FinanceSalesInvoice
    Private WithEvents fDocIS As MFSalesInvoice
    Private WithEvents fDIS As WSCardSalesInvoice
    Private WithEvents fPOS As POSInvoice
    Private WithEvents fVoucher As VoucherWiseReport
    Private WithEvents fpanalysis As Profitanalysis
    Private WithEvents freconciliation As FinancialStatements
    Private WithEvents fAccdet As FinancialStatements
    Private WithEvents fInvoice As JobSalesInvoice
    Private WithEvents fpricemanagement As PricemanagementFrm
    Private WithEvents fStockAdjTI As StockAdjustmentFrm
    Private WithEvents fStockAdjTO As StockAdjustmentFrm
    Private WithEvents fCJOB As ContractJobFrm
    Private WithEvents fECJOB As ContractJobFrm
    Private WithEvents FGin As GoodsTransferFrm
    Private WithEvents FQti As CustomerQuotation
    Private WithEvents FEQti As QuotationFrm
    Private WithEvents fTaxReport As TaxReportFrm
    Private WithEvents fPDCTransfer As PDCTransfer
    Private WithEvents fManufacturing As ManufacturingFrm
    Private WithEvents fwebInvReport As WebInvReport
    Private WithEvents fcardType As CardTypeMasterFrm
    Private WithEvents fVehiclemaster As VehiclemasterFrm
    Private WithEvents fSIS As WSServiceSalesInvoice
    Private WithEvents fDCR As WSCardRenew
    Private WithEvents fSTSh As StockShortageFrm
    Private WithEvents fCheckin As LodgeCheckInFrm
    Private WithEvents fBooking As LodgeBookingFrm
    Private WithEvents fRoom As LodgeRoomFrm
    Private WithEvents fPosLogin As POSLogin
    Private WithEvents fstockmovement As StockmovementFrm
    Private WithEvents fdeliverywiseoutstanding As DeliverywiseOutstandingFrm
    Private WithEvents fcollection As DeliverywiseOutstandingFrm
    Private WithEvents fEmployee As EmployeeMasterFrm
    Private WithEvents fworksheet As DailyWorkSheetFrm
    Private WithEvents fEWiseworksheet As EmpworksheetFrm
    Private WithEvents fpaymentbooking As PaymentBookingFrm
    Private WithEvents fjobcard As GarrageJobCardFrm

    Private WithEvents fusedcar As UsedcarFrm
    Private WithEvents fsalesanalysis As SalesanalysisFrm
    Private WithEvents ftempleIncomeExpense As IncomeExpenseFrm
    Private WithEvents fVATReport As UAETaxReport
    Private WithEvents FSO As SalesOrderFrm
    Private WithEvents FSOL As LaundryOrderFrm
    Private WithEvents FDOC As CustomerDeliverOrderFrm
    Private WithEvents FPO As PurchaseOrderFrm
    Private WithEvents fsalemansummary As SalesmanwiseSummaryfrm
    Private WithEvents fmembership As ContractMemberShipFrm
    Private WithEvents fAuditReport As AuditReportLodgeFrm
    Private WithEvents fpatienet As PatientInfoFrm
    Private WithEvents fappointment As ClinicAppointmentFrm
    Private WithEvents fDoctordesk As DoctorDeskFrm
    Private WithEvents fpatientfollowup As PatientFollowupFrm
    Private WithEvents flaundryList As LaundryReportFrm
    Private WithEvents fClinicService As ServiceItemCollectionFrm
    Private WithEvents fadvancesetoff As AdvanceSetoff
    Private WithEvents frvcollection As RVCollectionListFrm
    Private WithEvents ffineform As FineFrm
    Private WithEvents fLoanBook As LoanBook
    Private WithEvents fonlinecollection As CollectionListFrm
    Private WithEvents fTrayOutstandingFruits As FruitsTrayAnalysisFrm
    Private WithEvents floanrestructure As RestructureFrm
    Private WithEvents fbranchreconcil As BranchReconciliation
    Private WithEvents frmDayclosing As DaycloseReportFrm
    Private WithEvents FGSTR1 As GSTR1
    Private WithEvents fstichingservicemaster As StichingServiceMasterFrm
    Private WithEvents fstichingjob As StichingJobFrm
    Private WithEvents fageing As AgeingReportFrm
    Private WithEvents fMFIS As MFSalesInvoice
    Private WithEvents fMFRV As MFCustomerReceipt
    Private WithEvents fMFIR As MFISReportFrm
    Public WithEvents fRFCOST As AutoRefreshFrm
    Private WithEvents fStudentAdmission As StudentAdmissionFrm
    Private WithEvents fmonthlyfees As MonthlyFeesInvoiceFrm
    Private WithEvents fweeklycollection As WeeklyCollectionFrm
    Private WithEvents fdebitNote As Debitnote
    Private WithEvents fyearlyfees As FeesInvoiceFrm
    Private WithEvents fstudentappication As StudentAdmissionDXBFrm
    Private WithEvents ffeesSales As FeesSalesInvoice
    Private WithEvents fcourseSummary As SchoolDXBDashBoardFrm
    Private WithEvents fIPappointment As ClinicAppointmentFrm
    Private WithEvents feventlog As eventLogFrm
    Private WithEvents fautoupdateToserver As AutoUpdateToServerFrm
    Private WithEvents fpoolmembership As SwimmingPoolMemberShipFrm
    Private WithEvents fmembershipattendance As MembershipAttendanceFrm
    Private WithEvents fgymmembership As GYMMemberShipFrm
    Private WithEvents famccustomerlist As AmcList
    Private WithEvents froutesales As MilksalesFrm
    Private WithEvents fRefreshCostManual As RefreshCostManualFrm

#End Region
#Region "Drag"
    Dim posX As Integer
    Dim posY As Integer
    Dim drag As Boolean
    Dim maximized As Boolean
#End Region
#Region "Class objects"
    Private _objcmnbLayer As clsCommon_BL
#End Region
    '    //https://cloud.webaapsservers.net:8443/
    '//Username : vinvis
    '//paswword: 08I#8hax8

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        End
    End Sub
    Public Sub GetMenues(ByVal Current As ToolStripItem, ByRef menues As List(Of ToolStripItem))
        menues.Add(Current)
        If TypeOf (Current) Is ToolStripMenuItem Then
            For Each menu As ToolStripItem In DirectCast(Current, ToolStripMenuItem).DropDownItems
                GetMenues(menu, menues)
            Next
        End If
    End Sub
    Public Sub setRight()
        'Dim dt As DataTable
        _objcmnbLayer = New clsCommon_BL
        'dt = _objcmnbLayer._fldDatatable("SELECT * FROM Rights INNER JOIN UserTb ON Rights.Uid=UserTb.id where userid='" & CurrentUser & "'")
        Dim menues As New List(Of ToolStripItem)
        For Each t As ToolStripItem In MenuStrip.Items
            GetMenues(t, menues)
        Next
        Dim msg As String = ""
        'Dim itm As New ToolStripItem
        For Each itm As ToolStripItem In menues
            If Val(itm.Tag) <> 0 And itm.Tag <> "y" And userType Then
                Dim _qurey = From data In dtrights.AsEnumerable() Where data("NodeId") = Val(itm.Tag) And UCase(data("UserId")) = UCase(CurrentUser) Select data
                If _qurey.Count > 0 Or itm.Tag = "y" Or Not userType Then
                    itm.Visible = True
                Else
                    itm.Visible = False
                End If
            Else
                itm.Visible = True
            End If
        Next
    End Sub
    Private Sub Set_Permission()
        If (getRight(5, CurrentUser)) Then
            btnsupp.Visible = True
        Else
            btnsupp.Visible = False
        End If

        If (getRight(9, CurrentUser)) Then
            btncust.Visible = True
        Else
            btncust.Visible = False
        End If

        If (getRight(1, CurrentUser)) Then
            btnproduct.Visible = True
        Else
            btnproduct.Visible = False
        End If

        If (getRight(22, CurrentUser)) And enableServiceJob Then
            btnjob.Visible = True
        Else
            btnjob.Visible = False
        End If

        If (getRight(21, CurrentUser)) Then
            btnhsnmaster.Visible = True
        Else
            btnhsnmaster.Visible = False

        End If
        If (getRight(41, CurrentUser)) Then
            btnpurchase.Visible = True
        Else
            btnpurchase.Visible = False
        End If

        If (getRight(45, CurrentUser)) Then
            btnstockout.Visible = True
        Else
            btnstockout.Visible = False
        End If

        If (getRight(53, CurrentUser)) Then
            btnuser.Visible = True
        Else
            btnuser.Visible = False
        End If
        If (getRight(32, CurrentUser)) And enableServiceJob Then
            btnTracking.Visible = True
        Else
            btnTracking.Visible = False
        End If
        If (getRight(62, CurrentUser)) Then
            btnRVCust.Visible = True
        Else
            btnRVCust.Visible = False
        End If
        If (getRight(66, CurrentUser)) Then
            btnPVSupplier.Visible = True
        Else
            btnPVSupplier.Visible = False
        End If
        If (getRight(66, CurrentUser)) Then
            btnpvother.Visible = True
        Else
            btnpvother.Visible = False
        End If
        If (getRight(62, CurrentUser)) Then
            btnadvanceRv.Visible = True
        Else
            btnadvanceRv.Visible = False
        End If
    End Sub
    Private Sub MainFrm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        'Exit Sub


    End Sub
    Public Sub setPermissionOnLoad()
        ToolStrip1.Visible = False
        MenuStrip.Visible = False
        'MenuStrip1.Visible = False
        Dim menues As New List(Of ToolStripItem)
        mnuws.Tag = IIf(enableWorkshop, "179", mnuws.Tag)
        mnujob.Tag = IIf(enableServiceJob Or enableJobMaster, "", "n")
        mnuaccounts.Tag = IIf(enableAccounts, "", "n")
        mnustock.Tag = IIf(enableInventory, "", "n")
        mnuContractJob.Tag = IIf(enableContractJob, "", "n")
        ToolStripMenuItem9.Tag = IIf(enableServiceJob, "", "n") 'warrenty
        'mnuvat.Tag = IIf(ShowTaxOnInventory, mnuvat.Tag, "n")
        mnugstmaster.Tag = IIf(EnableGST, mnugstmaster.Tag, "n")
        mnutemple.Tag = IIf(enableTemple, mnutemple.Tag, "n")
        mnuproduction.Tag = IIf(enableProduction, mnuproduction.Tag, "n")
        mnuFuleBank.Tag = IIf(enableFuleBankInvoice, mnuFuleBank.Tag, "n")
        'JobEnquiryToolStripMenuItem.Tag = IIf(enableJobMaster, JobEnquiryToolStripMenuItem.Tag, "n")
        'DeliveredListToolStripMenuItem.Tag = IIf(enableJobMaster, DeliveredListToolStripMenuItem.Tag, "n")
        mnuweb.Tag = IIf(enableWebIntegration, mnuweb.Tag, "n")
        mnuws.Tag = IIf(enableCarWash Or enableWorkshop, mnuws.Tag, "n")
        mnulodge.Tag = IIf(enableLodge, "", "n")
        mnudeliverywiseoutstanding.Tag = IIf(enableDeliverywiseOutstanding, mnudeliverywiseoutstanding.Tag, "n")
        mnudocument.Tag = IIf(enableDocuments, "", "n")
        mnusms.Tag = IIf(enableSMS, mnusms.Tag, "n")
        mnupayroll.Tag = IIf(enablePayroll, mnupayroll.Tag, "n")
        mnupendingJob.Tag = IIf(enableJobMaster, mnupendingJob.Tag, "n")
        mnudiscountcardws.Tag = IIf(enableWorkshop, "n", mnudiscountcardws.Tag)
        mnuvehicle.Tag = IIf(enableWorkshop, "n", mnuvehicle.Tag)
        mnuserviceinvoicews.Tag = IIf(enableWorkshop, "n", mnuserviceinvoicews.Tag)
        mnuservicelist.Tag = IIf(enableWorkshop, "n", mnuservicelist.Tag)
        mnumembership.Tag = IIf(enableMembership Or enableSwimmingPool Or enableGYM, mnumembership.Tag, "n")
        mnuclinic.Tag = IIf(enableClinic, mnuclinic.Tag, "n")
        mnuparish.Tag = IIf(enableChurchModule, mnuparish.Tag, "n")
        mnulaundry.Tag = IIf(enablelaundry, mnulaundry.Tag, "n")
        mnubranch.Tag = IIf(enableBranch, "", "n")
        FinanceServiceMenuItem.Tag = IIf(EnableFinancialSales, "", "n")
        mnuusedcar.Tag = IIf(EnableUsedCar, "", "n")
        mnucarrier.Tag = IIf(EnableFruitsSales, "", "n")
        mnustiching.Tag = IIf(enableTailoring, "", "n")
        GSTR1ToolStripMenuItem.Tag = IIf(EnableGST, "", "n")
        mnutaxreport.Tag = IIf(EnableGST Or enableGCC, "", "n")
        mnumf.Tag = IIf(enableMicroFinace, "", "n")
        tspschoolmangement.Tag = IIf(enableschoolmanagement, "", "n")
        mnucoursemangement.Tag = IIf(enableCoursemangementDXB, "", "n")
        mnuroutesales.Tag = IIf(enableRouteBulkSale, "", "n")


        btnjob.Visible = enableServiceJob
        btnTracking.Visible = enableServiceJob
        btnhsnmaster.Visible = EnableGST
        SerialNumberExpiryToolStripMenuItem1.Visible = enableSerialnumber
        btncardsales.Visible = enableCarWash
        btnwsservice.Visible = enableCarWash
        btnposInv.Visible = enablePOS
        btncust.Visible = Not enableClinic


        If userType Then
            setRight()
            Set_Permission()
            For Each t As ToolStripItem In MenuStrip.Items
                GetMenues(t, menues)
            Next
            For Each itm As ToolStripItem In menues
                If itm.Name = "mnujob" Then
                    Dim a = itm.Name
                End If
                If itm.Tag = "n" Then
                    itm.Visible = False
                End If
            Next
            If getRight(93, CurrentUser) Or getRight(97, CurrentUser) Then
                mnustockadjustment.Visible = True
            Else
                mnustockadjustment.Visible = False
            End If
            If getRight(185, CurrentUser) Then
                mnusalesanalysis.Visible = True
            Else
                mnusalesanalysis.Visible = False
            End If
        Else
            Try
                For Each t As ToolStripItem In MenuStrip.Items
                    GetMenues(t, menues)
                Next
                For Each itm As ToolStripItem In menues
                    If itm.Tag = "n" Then
                        itm.Visible = False
                    Else
                        itm.Visible = True
                    End If
                Next
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End If

        If EnableFinancialSales Then
            btnposInv.Visible = False
            btnpurchase.Visible = False
            btnsupp.Visible = False
            btnPVSupplier.Visible = False
            mnustock.Visible = False

        End If

nxt:

        'If Not enableRealtimeCosting Then
        '    RefreshCostValuesToolStripMenuItem.Enabled = False
        'End If
        If Not enableRealtimeCosting Then
            RefreshCostValuesToolStripMenuItem.Enabled = False
        Else
            RefreshCostValuesToolStripMenuItem.Enabled = True
        End If
        mnuvat.Text = IIf(enableGCC, "Vat Master", "Other Tax")
        mnustatemaster.Text = IIf(enableGCC, "Emirate Master", "Sate Master")

        Try
            mnutaxreport.Text = IIf(enableGCC, "VAT Report", "GST Report")
        Catch ex As Exception
            MsgBox("")
        End Try
        'ToolStrip1.Visible = True
        'MenuStrip.Visible = True
        'MenuStrip1.Visible = True
    End Sub

    Private Sub MainFrm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If Not fRFCOST Is Nothing Then fRFCOST.Close() : fRFCOST = Nothing
        If Not fautoupdateToserver Is Nothing Then fautoupdateToserver.Close() : fautoupdateToserver = Nothing
    End Sub

    Private Sub MainFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If userType Then
            If getRight(244, CurrentUser) Then
                ToolStrip1.Visible = False
                MenuStrip.Visible = False
                fDoctordesk = New DoctorDeskFrm
                fDoctordesk.MdiParent = Me
                fDoctordesk.Show()
            Else
                fhome = New Homefrm
                fhome.MdiParent = Me
                fhome.Show()
            End If
        Else
            fhome = New Homefrm
            fhome.MdiParent = Me
            fhome.Show()

        End If

        setConstants()
        'setPermissionOnLoad()
        Timer1.Enabled = True
        lblstatus.Text = "Version : " & My.Application.Info.Version.ToString

        'btnRVCust.Image.Save("E:\rv.jpg")
        'btnPVSupplier.Image.Save("E:\pv.jpg")
        'btnpvother.Image.Save("E:\pv.jpg")
    End Sub
    Public Sub loadRefreshcost(Optional ByVal processtype As Integer = 0)
        'Exit Sub
        If fRFCOST Is Nothing Then
N:
            fRFCOST = New AutoRefreshFrm
            fRFCOST.processtype = processtype
            'fRFCOST.MdiParent = Me
            fRFCOST.Show()
            fRFCOST.Hide()
            tsrefresh.Text = "Cost Refresh is running.."
            tsrefresh.BackColor = StatusStrip.BackColor
        End If
    End Sub
    Private Sub fhome_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fhome.FormClosed
        Dim dt As DataTable
        If _objcmnbLayer Is Nothing Then _objcmnbLayer = New clsCommon_BL
        dt = _objcmnbLayer._fldDatatable("SELECT LastBkdDt FROM CompanyTb")
        Dim dtbk As Date
        If dt.Rows.Count > 0 Then
            If Not IsDBNull(dt(0)(0)) Then
                dtbk = DateValue(dt(0)(0))
            Else
                dtbk = DateValue("01/01/1950")
            End If
        End If
        If dtbk < Now.Date And MACHINENAME = fhome.lblserver.Text Then
            If MsgBox("Do you want to take Backup?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then GoTo ed
            Backupfrm.ShowDialog()
        End If
ed:
        'End
    End Sub

    Private Sub PrintSetupToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintSetupToolStripMenuItem.Click
        fparameter = New Companyfrm
        'fparameter.MdiParent = Me
        fparameter.ShowDialog()
        fparameter = Nothing
    End Sub

    Private Sub mnuusermaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuusermaster.Click
        UserSettingsFrm.ShowDialog()
    End Sub

    Private Sub mnusecurity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnusecurity.Click
        ChangeSecurityFrm.ShowDialog()
    End Sub

    Private Sub mnuaccountLedger_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuaccountLedger.Click
        Dim fCreateAcc As New CreateAcc
        fCreateAcc.ShowDialog()
        fCreateAcc = Nothing
    End Sub

    Private Sub mnucurrency_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnucurrency.Click
        CurrencyFrm.ShowDialog()
    End Sub

    Private Sub mnubackup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnubackup.Click
        Backupfrm.ShowDialog()
    End Sub

    Private Sub mnurestore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnurestore.Click
        'Restorefrm.ShowDialog()
        Dim fpath As String = APath & "Restore.exe"
        Process.Start(fpath)
    End Sub

    Private Sub mnuunits_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuunits.Click
        UnitMasterFrm.StrCaption = "Unit Master"
        UnitMasterFrm.ShowDialog()
    End Sub
    Public Sub ldViewPrint(ByVal tp As String, Optional ByVal isdoc As Boolean = False, Optional ByVal isfromroutesale As Boolean = False)
        If fView Is Nothing Then
            fView = New InvReportFrm
            fView.MdiParent = Me
            fView.ldType = tp
            fView.isDoc = isdoc
            fView.cboxroute = isfromroutesale
            fView.Show()
            With MenuStrip1.Items.Add("Reports - " & tp)
                .Tag = "INR"
                AddHandler .Click, AddressOf MenuStripClick
            End With
        Else
            fView.ldType = tp
            fView.cboxroute = isfromroutesale
            fView.Focus()
        End If

    End Sub
    Public Sub ldViewWSPrint(ByVal tp As String, ByVal wsSales As Integer)
        If fViewWS Is Nothing Then
            fViewWS = New InvReportFrm
            fViewWS.MdiParent = Me
            fViewWS.ldType = tp
            fViewWS.chkcardsale.Checked = 1
            fViewWS.chkservicesale.Checked = 1
            fViewWS.chksale.Checked = 0
            fViewWS.Show()
        Else
            fViewWS.chkcardsale.Checked = 1
            fViewWS.chkservicesale.Checked = 1
            fViewWS.chksale.Checked = 0
            fViewWS.ldType = tp
            fViewWS.Focus()
        End If

    End Sub
    Public Sub ldFuelViewPrint(ByVal tp As String, Optional ByVal isdoc As Boolean = False)
        If fFuelView Is Nothing Then
            fFuelView = New FuelInvReportFrm
            fFuelView.MdiParent = Me
            fFuelView.ldType = tp
            fFuelView.isDoc = isdoc
            fFuelView.Show()
        Else
            fFuelView.ldType = tp
            fFuelView.Focus()
        End If

    End Sub


    Private Sub btnproduct_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnproduct.Click
        loadProduct()
    End Sub
    Private Sub loadProduct()
        If fproductMast Is Nothing Then
            fproductMast = New ItemMastFrm
            fproductMast.MdiParent = fMainForm
            'fproductMast.Top = Me.Top + 500
            fproductMast.Show()
            With MenuStrip1.Items.Add("Product Master")
                .Tag = "ITM"
                AddHandler .Click, AddressOf MenuStripClick
            End With
        Else
            fproductMast.Focus()
        End If
    End Sub

    Private Sub fproductMast_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fproductMast.FormClosed
        fproductMast = Nothing
        removeMenuItem("ITM")
    End Sub

    Private Sub btnpurchase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpurchase.Click
        LoadIP()
    End Sub
    Public Sub loadJob(Optional ByVal KeyId As Long = 0)
        If Not fEdtJOB Is Nothing Then fEdtJOB = Nothing
        fEdtJOB = New ServiceJob
        With fEdtJOB
            .isModi = True
            .MdiParent = Me
            .Show()
            .btnSlct.Visible = True
            .btnmodify.Text = "Undo"
            .isfromExternal = True
            .ldRec(KeyId)
        End With
    End Sub
    Public Sub ldECJob(Optional ByVal KeyId As Long = 0)
        If Not fECJOB Is Nothing Then fECJOB = Nothing
        fECJOB = New ContractJobFrm
        With fECJOB
            .isModi = True
            .MdiParent = Me
            .Show()
            .ldRec(KeyId)
        End With
    End Sub
    Public Sub LoadIP(Optional ByVal KeyId As Long = 0, Optional ByVal jobcode As String = "")
        If EnableFruitsSales Then
            If fPIFruit Is Nothing Then
                fPIFruit = New FruitPurchaseInvoiceFrm
                If KeyId <> 0 Then
                    fPIFruit.isModi = True
                End If
                fPIFruit.MdiParent = Me
                fPIFruit.Show()
                With MenuStrip1.Items.Add("Purchase")
                    .Tag = "IP"
                    AddHandler .Click, AddressOf MenuStripClick
                End With
            Else
                fPIFruit.Focus()
            End If
            If KeyId <> 0 Then
                fPIFruit.isModi = True
                fPIFruit.CheckNLoad(KeyId)
            End If

        Else
            If fPI Is Nothing Then
                fPI = New PurchaseInvoiceFrm
                If KeyId <> 0 Then
                    fPI.isModi = True
                End If
                fPI.MdiParent = Me
                fPI.Show()
                With MenuStrip1.Items.Add("Purchase")
                    .Tag = "IP"
                    AddHandler .Click, AddressOf MenuStripClick
                End With
            Else
                fPI.Focus()
            End If
            If KeyId <> 0 Then
                fPI.isModi = True
                fPI.CheckNLoad(KeyId)
            End If
            If jobcode <> "" Then
                fPI.chgbyprg = True
                fPI.txtJob.Text = jobcode
                fPI.ldjbname()
                fPI.chgbyprg = False
            End If
        End If

    End Sub
    Public Sub LoadJV(Optional ByVal KeyId As Long = 0)
        If fJV Is Nothing Then
N:
            fJV = New JournalVoucher
            If KeyId <> 0 Then
                fJV.isModi = True
            End If
            fJV.MdiParent = Me
            fJV.Show()
            With MenuStrip1.Items.Add("Journal")
                .Tag = "JV"
                AddHandler .Click, AddressOf MenuStripClick
            End With
        Else
            fJV.Focus()
        End If
        If KeyId <> 0 Then
            fJV.isModi = True
            fJV.CheckNLoad(KeyId)
        End If
    End Sub
    Public Sub LoadIS(Optional ByVal KeyId As Long = 0, Optional ByVal docid As Long = 0)
        If EnableFruitsSales Then
            If fFruitIS Is Nothing Then
                fFruitIS = New FruitSalesInvoice
                If KeyId <> 0 Then
                    fFruitIS.isModi = True
                End If
                fFruitIS.MdiParent = Me
                fFruitIS.Show()
                With MenuStrip1.Items.Add("Sales")
                    .Tag = "IS"
                    AddHandler .Click, AddressOf MenuStripClick
                End With
            Else
                fFruitIS.Focus()
            End If
            If KeyId <> 0 Then
                fFruitIS.isModi = True
                fFruitIS.CheckNLoad(KeyId)
            End If
            If docid <> 0 Then
                fFruitIS.ImportDOs(docid, True, True)
            End If
        ElseIf EnableFinancialSales Then
            If efIS Is Nothing Then
N:
                efIS = New FinanceSalesInvoice
                If KeyId <> 0 Then
                    efIS.isModi = True
                End If
                efIS.MdiParent = Me
                efIS.Show()
                With MenuStrip1.Items.Add("Sales")
                    .Tag = "IS"
                    AddHandler .Click, AddressOf MenuStripClick
                End With
            Else
                efIS.Focus()
            End If
            If KeyId <> 0 Then
                efIS.isModi = True
                efIS.CheckNLoad(KeyId)
            End If
            If docid <> 0 Then
                efIS.ImportDOs(docid, True, True)
            End If
        Else
            If fIS Is Nothing Then
                fIS = New SalesInvoice
                If KeyId <> 0 Then
                    fIS.isModi = True
                End If
                fIS.MdiParent = Me
                fIS.Show()
                With MenuStrip1.Items.Add("Sales")
                    .Tag = "IS"
                    AddHandler .Click, AddressOf MenuStripClick
                End With
            Else
                fIS.Focus()
            End If
            If KeyId <> 0 Then
                fIS.isModi = True
                fIS.CheckNLoad(KeyId)
            End If
            If docid <> 0 Then
                fIS.ImportDOs(docid, True, True)
            End If
        End If

    End Sub

    Public Sub editFeesInvoice(ByVal KeyId As Long)
        If ffeesSales Is Nothing Then
            ffeesSales = New FeesSalesInvoice
            If KeyId <> 0 Then
                ffeesSales.isModi = True
            End If
            ffeesSales.MdiParent = Me
            ffeesSales.Show()
            With fMainForm.MenuStrip1.Items.Add("Sales")
                .Tag = "ISF"
                AddHandler .Click, AddressOf MenuStripClick
            End With
        Else
            ffeesSales.Focus()
        End If
        If KeyId <> 0 Then
            ffeesSales.isModi = True
            ffeesSales.CheckNLoad(KeyId)
        End If
    End Sub

    Public Sub MenuStripClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim myctrl As Object
        myctrl = sender
        Select Case myctrl.tag
            Case "QTY"
                ldQuantity()
            Case "IS"
                LoadIS()
            Case "INR"
                Dim mname As String = myctrl.Text
                mname = Mid(mname, mname.IndexOf("-") + 2)
                ldViewPrint(Trim(mname))
            Case "ITM"
                loadProduct()
            Case "PM"
                loadPricemanagement()
            Case "SM"
                showSerialNumbers()
            Case "IP"
                LoadIP()
            Case "PR"
                LoadPR()
            Case "CO"
                loadCarrierOutstanding()
            Case "SR"
                LoadSR()
            Case "TI"
                LoadSTKADJTI()
            Case "TO"
                LoadSTKADJTO()
            Case "LT"
                LoadGIN()
            Case "DOCR"
                Dim mname As String = myctrl.Text
                mname = Mid(mname, mname.IndexOf("-") + 2)
                ldViewDOCPrint(Trim(mname))
            Case "WR"
                loadWarrentyReport()
            Case "DC"
                loadDayClosing()
            Case "PA"
                loadProfitAnalisis()
            Case "SA"
                loadSalesAnalysis()
            Case "SM"
                loadStockMovement()
            Case "VR", "GR"
                loadTaxReport()
            Case "JV"
                LoadJV()
            Case "RVC"
                LoadRV()
            Case "RVO"
                LoadRVO()
            Case "CL"
                loadRVCollection()
            Case "OCL"
                loadOnlineCollectionlist()
            Case "PVS"
                LoadPV()
            Case "PVO"
                LoadPVO()
            Case "ADS"
                loadAdvanceSetoff()
            Case "FS"
                ldFinancialstatement()
            Case "FSS"
                loadFinancialStatus()
            Case "VAS"
                LdVoucherlist()
            Case "REC"
                loadReconciliation()
            Case "RECB"
                loadbranchReconciliation()
            Case "PDCL"
                loadPDCList()
            Case "QTI"
                LoadQTI()
            Case "SO"
                LoadSO()
            Case "DOC"
                LoadDOC()
            Case "PO"
                LoadPO()
            Case "ISF"
                editFeesInvoice(0)
            Case "CSUM"
                loadCSummary()
        End Select
    End Sub
    Public Sub LoadISDoc(Optional ByVal Docid As Long = 0)
        If fDocIS Is Nothing Then
N:
            fDocIS = New MFSalesInvoice
            fDocIS.MdiParent = Me
            fDocIS.Show()
        Else
            fDocIS.Focus()
        End If
    End Sub
    Public Sub LoadPOSIS(Optional ByVal KeyId As Long = 0, Optional ByVal counter As String = "")

        If fPOS Is Nothing Then
N:
            fPOS = New POSInvoice
            If KeyId <> 0 Then
                fPOS.isModi = True
            End If
            fPOS.MdiParent = Me
            fPOS.lblcompany.Text = "POS INVOICE [ Counter : " & counter & " ]"
            fPOS.POScounter = counter
            fPOS.Show()
        Else
            fPOS.Focus()
        End If
        If KeyId <> 0 Then
            fPOS.isModi = True
            fPOS.CheckNLoad(KeyId)
        End If
    End Sub

    Private Sub btnsupp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsupp.Click
        QuickCust(, "Supplier")
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
            fCrtAcc = Nothing
        End With
    End Sub

    Private Sub btncust_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncust.Click
        QuickCust(, "Customer")
    End Sub

    Private Sub btnuser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnuser.Click
        UserSettingsFrm.ShowDialog()
    End Sub

    Private Sub ToolsMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolsMenu.Click

    End Sub

    Private Sub btnbackup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnbackup.Click
        Backupfrm.ShowDialog()
    End Sub

    Private Sub btnwarrenty_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnhsnmaster.Click
        Dim frm As New HSNCodeMaster
        frm.ShowDialog()
        frm = Nothing
    End Sub

    Private Sub btnjob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnjob.Click
        ldJob()
    End Sub
    Private Sub ldJob()
        If enableJobMaster Then
            If Not fJOBMST Is Nothing Then fJOBMST.Close() : fJOBMST = Nothing
            fJOBMST = New JobMaster
            With fJOBMST
                .MdiParent = fMainForm
                .Show()
                With MenuStrip1.Items.Add("JOBMST")
                    .Tag = "JBM"
                    AddHandler .Click, AddressOf MenuStripClick
                End With
            End With
        Else
            If Not fJOB Is Nothing Then fJOB.Close() : fJOB = Nothing
            fJOB = New ServiceJob
            With fJOB
                .MdiParent = fMainForm
                .Show()
                With MenuStrip1.Items.Add("JOB")
                    .Tag = "SJB"
                    AddHandler .Click, AddressOf MenuStripClick
                End With
            End With
        End If

    End Sub
    Private Sub ldTempleAdmission()
        If Not fTempleAdmission Is Nothing Then fTempleAdmission.Close() : fTempleAdmission = Nothing
        fTempleAdmission = New TempleAdmissionFrm
        With fTempleAdmission
            .MdiParent = fMainForm
            .Show()
        End With
    End Sub
    Private Sub JobEntryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JobEntryToolStripMenuItem.Click
        ldJob()
    End Sub

    Private Sub fJOBLst_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fJOBLst.FormClosed
        fJOBLst = Nothing
        removeMenuItem("JBH")
    End Sub

    Private Sub JobDateWiseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JobDateWiseToolStripMenuItem.Click
        If fJOBLst Is Nothing Then
            fJOBLst = New JobList
            With fJOBLst
                .MdiParent = fMainForm
                .rptCategory = 4
                .Show()
                With MenuStrip1.Items.Add("JOB History")
                    .Tag = "JBH"
                    AddHandler .Click, AddressOf MenuStripClick
                End With
            End With
        Else
            fJOBLst.Focus()
        End If

    End Sub

    Private Sub btnTracking_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTracking.Click
        If fJOBLst Is Nothing Then
            fJOBLst = New JobList
            With fJOBLst
                .MdiParent = fMainForm
                .rptCategory = 2
                .Show()
            End With
        Else
            fJOBLst.Focus()
        End If


    End Sub

    Private Sub btnminimize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnminimize.Click
        btnminimize.Cursor = Cursors.WaitCursor
        Me.WindowState = FormWindowState.Minimized
        btnminimize.Cursor = Cursors.Arrow
    End Sub

    Private Sub btnrestore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrestore.Click
        If Me.WindowState = FormWindowState.Normal Then
            Me.WindowState = FormWindowState.Maximized
        Else
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If MsgBox("Do you want to Close?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        Me.Close()
    End Sub

    Private Sub Panel1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel1.DoubleClick

    End Sub

    Private Sub Panel1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel1.MouseDoubleClick
        If e.Button = MouseButtons.Left Then
            If maximized Then
                Me.WindowState = FormWindowState.Normal
                maximized = False
            Else
                Me.WindowState = FormWindowState.Maximized
                maximized = True
            End If
        End If
    End Sub

    Private Sub Panel1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel1.MouseDown
        If e.Button = MouseButtons.Left Then
            drag = True
            posX = Cursor.Position.X - Me.Left
            posY = Cursor.Position.Y - Me.Top
        End If
    End Sub
    Private Sub panel1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel1.MouseUp
        drag = False
    End Sub

    Private Sub panel1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel1.MouseMove
        If drag Then
            Me.Top = Cursor.Position.Y - posY
            Me.Left = Cursor.Position.X - posX
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub btnhome_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnhome.Click
        If Not fhome Is Nothing Then
            fhome.Focus()
        End If
    End Sub

    Private Sub fPI_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fPI.FormClosed
        fPI = Nothing
        removeMenuItem("IP")
    End Sub

    Private Sub CreateEditProductToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateEditProductToolStripMenuItem.Click
        loadProduct()
    End Sub

    Private Sub VouToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VouToolStripMenuItem.Click
        ldViewPrint("IP")
    End Sub

    Private Sub CreateEditToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateEditToolStripMenuItem.Click
        LoadIP()
    End Sub

    Private Sub fView_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fView.FormClosed
        fView = Nothing
        removeMenuItem("INR")
    End Sub

    Private Sub EstimatedDateWiseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EstimatedDateWiseToolStripMenuItem.Click
        If fJOBLst1 Is Nothing Then
            fJOBLst1 = New JobList
            With fJOBLst1
                .MdiParent = fMainForm
                .rptCategory = 1
                .lblcap.Text = "JOB HISTORY [ESTIMATED DATE WISE]"
                .Show()
                With MenuStrip1.Items.Add("JOB Estimated")
                    .Tag = "JBE"
                    AddHandler .Click, AddressOf MenuStripClick
                End With
            End With
        Else
            fJOBLst1.Focus()
        End If
    End Sub

    Private Sub fJOBLst1_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fJOBLst1.FormClosed
        fJOBLst1 = Nothing
        removeMenuItem("JBE")
    End Sub

    Private Sub fJOBLst2_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fJOBLst2.FormClosed
        fJOBLst2 = Nothing
    End Sub

    Private Sub fJOBLst3_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fJOBLst3.FormClosed
        fJOBLst3 = Nothing
    End Sub

    Private Sub fJOBLst4_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fJOBLst4.FormClosed
        fJOBLst4 = Nothing
    End Sub


    Private Sub mnuactiveJoblist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuactiveJoblist.Click
        If fJOBLst3 Is Nothing Then
            fJOBLst3 = New JobList
            With fJOBLst3
                .MdiParent = fMainForm
                .rptCategory = 4
                .lblcap.Text = "ACTIVE JOB HISTORY [JOB DATE WISE]"
                .Show()
            End With
        Else
            fJOBLst3.Focus()
        End If
    End Sub

    Private Sub ClosedDateWiseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClosedDateWiseToolStripMenuItem.Click
        If fJOBLst4 Is Nothing Then
            fJOBLst4 = New JobList
            With fJOBLst4
                .MdiParent = fMainForm
                .rptCategory = 5
                .lblcap.Text = "CLOSED JOB HISTORY [CLOSED DATE WISE]"
                .Show()
            End With
        Else
            fJOBLst4.Focus()
        End If
    End Sub

    Private Sub JobDateWiseToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JobDateWiseToolStripMenuItem1.Click
        If fJOBLst2 Is Nothing Then
            fJOBLst2 = New JobList
            With fJOBLst2
                .MdiParent = fMainForm
                .rptCategory = 3
                .lblcap.Text = "CLOSED JOB HISTORY [JOB DATE WISE]"
                .Show()
            End With
        Else
            fJOBLst2.Focus()
        End If
    End Sub

    Private Sub CustomerWiseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CustomerWiseToolStripMenuItem.Click
        If fJOBLst5 Is Nothing Then
            fJOBLst5 = New JobList
            With fJOBLst5
                .rptCategory = 6
                .MdiParent = fMainForm
                .lblcap.Text = "JOB HISTORY [CUSTOMER WISE]"
                .Show()
            End With
        Else
            fJOBLst5.Focus()
        End If
    End Sub

    Private Sub fJOBLst5_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fJOBLst5.FormClosed
        fJOBLst5 = Nothing
    End Sub

    Private Sub fJOBLst6_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fJOBLst6.FormClosed
        fJOBLst6 = Nothing
        removeMenuItem("JBS")
    End Sub

    Private Sub fJOBLst7_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fJOBLst7.FormClosed
        fJOBLst7 = Nothing
    End Sub

    Private Sub fJOBLst8_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fJOBLst8.FormClosed
        fJOBLst8 = Nothing
        removeMenuItem("JBI")
    End Sub

    Private Sub IMEIJobHistoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IMEIJobHistoryToolStripMenuItem.Click
        If fJOBLst6 Is Nothing Then
            fJOBLst6 = New JobList
            With fJOBLst6
                .MdiParent = fMainForm
                .rptCategory = 7
                .lblcap.Text = "JOB HISTORY [SERIAL NUMBER WISE]"
                .Show()
                With MenuStrip1.Items.Add("JOB Serial Number List")
                    .Tag = "JBS"
                    AddHandler .Click, AddressOf MenuStripClick
                End With
            End With
        Else
            fJOBLst6.Focus()
        End If
    End Sub

    Private Sub TechnicianWiseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TechnicianWiseToolStripMenuItem.Click
        If fJOBLst7 Is Nothing Then
            fJOBLst7 = New JobList
            With fJOBLst7
                .MdiParent = fMainForm
                .rptCategory = 8
                .lblcap.Text = "JOB HISTORY [TECHNICIAN WISE]"
                .Show()
            End With
        Else
            fJOBLst7.Focus()
        End If
    End Sub

    Private Sub ItemWiseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemWiseToolStripMenuItem.Click
        If fJOBLst8 Is Nothing Then
            fJOBLst8 = New JobList
            With fJOBLst8
                .MdiParent = fMainForm
                .rptCategory = 9
                .lblcap.Text = "JOB HISTORY [ITEMWISE WISE]"
                .Show()
                With MenuStrip1.Items.Add("JOB Itemwise")
                    .Tag = "JBI"
                    AddHandler .Click, AddressOf MenuStripClick
                End With
            End With
        Else
            fJOBLst8.Focus()
        End If
    End Sub

    Private Sub QuantityReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QuantityReportToolStripMenuItem.Click
        ldQuantity()
    End Sub
    Public Sub ldQuantity()
        If fQtyReport Is Nothing Then
            fQtyReport = New QtyReport
            fQtyReport.MdiParent = fMainForm
            'fproductMast.Top = Me.Top + 500
            fQtyReport.Show()
            With MenuStrip1.Items.Add("Quantity Reports")
                .Tag = "QTY"
                AddHandler .Click, AddressOf MenuStripClick
            End With
        Else
            fQtyReport.Focus()
        End If
    End Sub

    Private Sub fQtyReport_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fQtyReport.FormClosed
        fQtyReport = Nothing
        removeMenuItem("QTY")
    End Sub
    Private Sub removeMenuItem(ByVal tag As String)
        Dim i As Integer
        For i = MenuStrip1.Items.Count - 1 To 0 Step -1
            If MenuStrip1.Items(i).Tag = tag Then
                MenuStrip1.Items.RemoveAt(i)
            End If
        Next
    End Sub

    Private Sub fImeiNo_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fImeiNo.FormClosed
        fImeiNo = Nothing
    End Sub

    Private Sub fImeiNo1_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fImeiNo1.FormClosed
        fImeiNo1 = Nothing
        removeMenuItem("WR")
    End Sub

    Private Sub CustomerWarrentyExpiredListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CustomerWarrentyExpiredListToolStripMenuItem.Click
        loadWarrentyReport()
    End Sub
    Private Sub loadWarrentyReport()
        If fImeiNo1 Is Nothing Then
            fImeiNo1 = New IMEINos
            fImeiNo1.MdiParent = fMainForm
            fImeiNo1.SearchType = 1
            fImeiNo1.Show()
            With MenuStrip1.Items.Add("Warrenty Report")
                .Tag = "WR"
                AddHandler .Click, AddressOf MenuStripClick
            End With
        Else
            fImeiNo1.Focus()
        End If
    End Sub

    Private Sub WarrentyCancelledIMEINosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WarrentyCancelledIMEINosToolStripMenuItem.Click
        If fImeiNo2 Is Nothing Then
            fImeiNo2 = New IMEINos
            fImeiNo2.MdiParent = fMainForm
            fImeiNo2.SearchType = 2
            fImeiNo2.Show()
        Else
            fImeiNo2.Focus()
        End If
    End Sub

    Private Sub fImeiNo2_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fImeiNo2.FormClosed
        fImeiNo2 = Nothing
    End Sub

    Private Sub WarrentyIMEINosListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WarrentyIMEINosListToolStripMenuItem.Click
        If fImeiNo3 Is Nothing Then
            fImeiNo3 = New IMEINos
            fImeiNo3.MdiParent = fMainForm
            fImeiNo3.SearchType = 3
            fImeiNo3.Show()
        Else
            fImeiNo3.Focus()
        End If
    End Sub

    Private Sub fImeiNo3_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fImeiNo3.FormClosed
        fImeiNo3 = Nothing
    End Sub

    Private Sub UserPermissionsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UserPermissionsToolStripMenuItem.Click
        UserSettingsFrm.ShowDialog()
    End Sub

    Private Sub btnstockout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnstockout.Click
        LoadIS()
    End Sub


    Private Sub CreateEditToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateEditToolStripMenuItem1.Click
        LoadIS()
    End Sub

    Private Sub VoucherHistoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VoucherHistoryToolStripMenuItem.Click
        ldViewPrint("IS")
    End Sub

    Private Sub fIS_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fIS.FormClosed
        fIS = Nothing
        removeMenuItem("IS")
    End Sub

    Private Sub fJOB_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fJOB.FormClosed
        fJOB = Nothing
        removeMenuItem("SJB")
    End Sub

    Private Sub fEdtJOB_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fEdtJOB.FormClosed
        fEdtJOB = Nothing
    End Sub

    Private Sub JobEnquiryEsitmatedDateWiseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JobEnquiryEsitmatedDateWiseToolStripMenuItem.Click

    End Sub

    Private Sub ProductKeyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProductKeyToolStripMenuItem.Click
        KeyForm.ShowDialog()
    End Sub

    Private Sub ChangeServerConnectionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangeServerConnectionToolStripMenuItem.Click
        ServerFrm.ChangeServer = True
        ServerFrm.ShowDialog()
    End Sub

    Private Sub ToolStripMenuItem9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem9.Click
        WarrentyMaster.ShowDialog()
    End Sub

    Private Sub mnuledgergroup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuledgergroup.Click
        LedgerGroupFrm.ShowDialog()
    End Sub

    Private Sub tsmBank_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmBank.Click
        Dim frm As New BankCodeCreation
        frm.ShowDialog()
        frm = Nothing
    End Sub

    Private Sub tspJV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tspJV.Click
        LoadJV()
    End Sub

    Private Sub fJV_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fJV.FormClosed
        fJV = Nothing
        removeMenuItem("JV")
    End Sub
    Public Sub LoadPV(Optional ByVal KeyId As Long = 0, Optional ByVal suppName As String = "")
        If fPV Is Nothing Then
N:
            fPV = New SupplierPayments
            If KeyId <> 0 Then
                fPV.isModi = True
            End If
            fPV.MdiParent = Me
            fPV.Show()
            With MenuStrip1.Items.Add("Supplier PV")
                .Tag = "PVS"
                AddHandler .Click, AddressOf MenuStripClick
            End With
        Else
            fPV.Focus()
        End If
        If suppName <> "" Then
            fPV.chgbyprg = True
            fPV.txtSuppName.Text = suppName
            fPV.chgbyprg = False
            If Not fPV.isModi Then fPV.loadFromJob()
        End If
        If KeyId <> 0 Then
            fPV.isModi = True
            fPV.editRecord(KeyId, suppName)
            'fPV.CheckNLoad(KeyId)
        End If
    End Sub
    Public Sub LoadRV(Optional ByVal KeyId As Long = 0, Optional ByVal custName As String = "")
        Me.Cursor = Cursors.WaitCursor
        If fRV Is Nothing Then
N:
            fRV = New CustomerReceipt
            If KeyId <> 0 Then
                fRV.isModi = True
            End If
            fRV.MdiParent = Me
            fRV.Show()
            With MenuStrip1.Items.Add("RV Customer")
                .Tag = "RVC"
                AddHandler .Click, AddressOf MenuStripClick
            End With
        Else
            fRV.Focus()
        End If
        If custName <> "" Then
            fRV.chgbyprg = True
            fRV.txtSuppName.Text = custName
            fRV.chgbyprg = False
            If Not fRV.isModi Then fRV.loadFromJob()
        End If
        If KeyId <> 0 Then
            fRV.isModi = True
            fRV.editRecord(KeyId, custName)
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub fPV_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fPV.FormClosed
        fPV = Nothing
        removeMenuItem("PVS")
    End Sub
    Public Sub LoadPVO(Optional ByVal KeyId As Long = 0, Optional ByVal jobcode As String = "")
        If fPVO Is Nothing Then
N:
            fPVO = New ExpensePayments
            If KeyId <> 0 Then
                fPVO.isModi = True
            End If
            fPVO.MdiParent = Me
            fPVO.Show()
            With MenuStrip1.Items.Add("Expense")
                .Tag = "PVO"
                AddHandler .Click, AddressOf MenuStripClick
            End With
        Else
            fPVO.Focus()
        End If

        If KeyId <> 0 Then
            fPVO.isModi = True
            fPVO.editRecord(KeyId)
        End If
        If jobcode <> "" Then
            fPVO.chgbyprg = True
            fPVO.txtjobcode.Text = jobcode
            fPVO.ldjbname()
            fPVO.chgbyprg = False
        End If
    End Sub
    Public Sub LoadRVO(Optional ByVal KeyId As Long = 0, Optional ByVal jobcode As String = "", Optional ByVal jobcustid As Long = 0, Optional ByVal isTemple As Boolean = False, Optional ByVal templeAmt As Double = 0, Optional ByVal TempleDate As String = "")
        If fRVO Is Nothing Then
N:
            fRVO = New OtherReceiptsFrm
            If KeyId <> 0 Then
                fRVO.isModi = True
            End If
            fRVO.MdiParent = Me
            fRVO.Show()
            With MenuStrip1.Items.Add("Advance RV")
                .Tag = "RVO"
                AddHandler .Click, AddressOf MenuStripClick
            End With
        Else
            fRVO.Focus()
        End If
        If KeyId <> 0 Then
            fRVO.isModi = True
            fRVO.editRecord(KeyId)
        End If
        If jobcode <> "" Then
            fRVO.chgbyprg = True
            fRVO.txtjobcode.Text = jobcode
            fRVO.isTemple = isTemple
            fRVO.jobCustomer = jobcustid
            If TempleDate <> "" Then
                fRVO.TmpleDate = DateValue(TempleDate)
            End If

            fRVO.TmpleAmt = templeAmt
            fRVO.ldjbname()
            fRVO.chgbyprg = False
        End If

    End Sub

    Private Sub SupplierPVToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SupplierPVToolStripMenuItem.Click
        LoadPV()
    End Sub

    Private Sub fPVO_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fPVO.FormClosed
        fPVO = Nothing
        removeMenuItem("PVO")
    End Sub

    Private Sub OtherPVToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OtherPVToolStripMenuItem.Click
        LoadPVO()
    End Sub

    Private Sub fRV_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fRV.FormClosed
        fRV = Nothing
        removeMenuItem("RVC")
    End Sub

    Private Sub CustomerRVToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CustomerRVToolStripMenuItem.Click
        LoadRV()
    End Sub

    Private Sub SupplierWarrentyExpiredListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SupplierWarrentyExpiredListToolStripMenuItem.Click
        If fImeiNo Is Nothing Then
            fImeiNo = New IMEINos
            fImeiNo.MdiParent = fMainForm
            fImeiNo.SearchType = 0
            fImeiNo.Show()
        Else
            fImeiNo.Focus()
        End If
    End Sub

    Private Sub mnuPRCE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPRCE.Click
        LoadPR()
    End Sub
    Public Sub LoadPR(Optional ByVal KeyId As Long = 0)
        If EnableFruitsSales Then
            If fFruitPR Is Nothing Then
                fFruitPR = New FruitPurchaseReturnInvoice
                If KeyId <> 0 Then
                    fFruitPR.isModi = True
                End If
                fFruitPR.MdiParent = Me
                fFruitPR.Show()
                With MenuStrip1.Items.Add("Purchase Return")
                    .Tag = "PR"
                    AddHandler .Click, AddressOf MenuStripClick
                End With
            Else
                fFruitPR.Focus()
            End If
            If KeyId <> 0 Then
                fFruitPR.isModi = True
                fFruitPR.CheckNLoad(KeyId)
            End If
        Else
            If fPR Is Nothing Then
                fPR = New PurchaseReturnInvoice
                If KeyId <> 0 Then
                    fPR.isModi = True
                End If
                fPR.MdiParent = Me
                fPR.Show()
                With MenuStrip1.Items.Add("Purchase Return")
                    .Tag = "PR"
                    AddHandler .Click, AddressOf MenuStripClick
                End With
            Else
                fPR.Focus()
            End If
            If KeyId <> 0 Then
                fPR.isModi = True
                fPR.CheckNLoad(KeyId)
            End If
        End If

    End Sub

    Private Sub fPR_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fPR.FormClosed
        fPR = Nothing
        removeMenuItem("PR")
    End Sub

    Private Sub mnuSRC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSRC.Click
        LoadSR()
    End Sub
    Public Sub LoadSR(Optional ByVal KeyId As Long = 0)
        If EnableFruitsSales Then
            If fFruitSR Is Nothing Then
                fFruitSR = New FruitSalesReturnInvoice
                If KeyId <> 0 Then
                    fFruitSR.isModi = True
                End If
                fFruitSR.MdiParent = Me
                fFruitSR.Show()
                With MenuStrip1.Items.Add("Sales Return")
                    .Tag = "SR"
                    AddHandler .Click, AddressOf MenuStripClick
                End With
            Else
                fFruitSR.Focus()
            End If
            If KeyId <> 0 Then
                fFruitSR.isModi = True
                fFruitSR.CheckNLoad(KeyId)
            End If
        Else
            If fSR Is Nothing Then
                fSR = New SalesReturnInvoice
                If KeyId <> 0 Then
                    fSR.isModi = True
                End If
                fSR.MdiParent = Me
                fSR.Show()
                With MenuStrip1.Items.Add("Sales Return")
                    .Tag = "SR"
                    AddHandler .Click, AddressOf MenuStripClick
                End With
            Else
                fSR.Focus()
            End If
            If KeyId <> 0 Then
                fSR.isModi = True
                fSR.CheckNLoad(KeyId)
            End If
        End If

    End Sub

    Private Sub fSR_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fSR.FormClosed
        fSR = Nothing
        removeMenuItem("SR")
    End Sub

    Private Sub LedgerStatementsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LedgerStatementsToolStripMenuItem.Click
        ldFinancialstatement()
    End Sub
    Private Sub ldFinancialstatement()
        If fFinancialRep Is Nothing Then
            fFinancialRep = New FinancialStatements
            fFinancialRep.MdiParent = fMainForm
            'fproductMast.Top = Me.Top + 500
            fFinancialRep.Show()
            With MenuStrip1.Items.Add("Financial Statement")
                .Tag = "FS"
                AddHandler .Click, AddressOf MenuStripClick
            End With
        Else
            fFinancialRep.Focus()
        End If
    End Sub

    Private Sub fFinancialRep_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fFinancialRep.FormClosed
        fFinancialRep = Nothing
        removeMenuItem("FS")
    End Sub

    Private Sub ProfitLossToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProfitLossToolStripMenuItem.Click
        loadFinancialStatus()
    End Sub
    Public Sub loadFinancialStatus()
        If fFinancialStat Is Nothing Then
            fFinancialStat = New FinancialStatus
            fFinancialStat.MdiParent = fMainForm
            'fproductMast.Top = Me.Top + 500
            fFinancialStat.Show()
            With MenuStrip1.Items.Add("Financial Status")
                .Tag = "FSS"
                AddHandler .Click, AddressOf MenuStripClick
            End With
        Else
            fFinancialStat.Focus()
        End If
    End Sub

    Private Sub fFinancialStat_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fFinancialStat.FormClosed
        fFinancialStat = Nothing
        removeMenuItem("FSS")
    End Sub

    Private Sub VoucherHistoryToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VoucherHistoryToolStripMenuItem2.Click
        ldViewPrint("SR")
    End Sub

    Private Sub VoucherHistoryToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VoucherHistoryToolStripMenuItem1.Click
        ldViewPrint("PR")
    End Sub

    Private Sub mnulevel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnulevel.Click
        fLevel = New LevelMasterFrm
        fLevel.ShowDialog()
        fLevel = Nothing
    End Sub

    Private Sub RefreshDataToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshDataToolStripMenuItem.Click
        'Dim frsh As RefreshDatabase
        'frsh = New RefreshDatabase
        'frsh.ShowDialog()
        'frsh = Nothing
        _objcmnbLayer = New clsCommon_BL
        Me.Cursor = Cursors.WaitCursor
        _objcmnbLayer.setRefreshQty(0)
        Me.Cursor = Cursors.Default
        MsgBox("Quantity updated", MsgBoxStyle.Information)
    End Sub

    Private Sub RefreshAccountBalanceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshAccountBalanceToolStripMenuItem.Click
        _objcmnbLayer = New clsCommon_BL
        Me.Cursor = Cursors.WaitCursor
        _objcmnbLayer.updateClosingBalance()
        Me.Cursor = Cursors.Default
        MsgBox("Account Balance updated", MsgBoxStyle.Information)
    End Sub

    Private Sub RefreshCostValuesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshCostValuesToolStripMenuItem.Click
        'Dim frm As New RefreshcostAverage
        'frm.ShowDialog()
        'frm = Nothing
        If fRefreshCostManual Is Nothing Then
            fRefreshCostManual = New RefreshCostManualFrm
            fRefreshCostManual.Show()
        Else
            fRefreshCostManual.Focus()
        End If
    End Sub

    Private Sub VoucherAnalysisToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VoucherAnalysisToolStripMenuItem.Click
        LdVoucherlist()
    End Sub
    Public Sub LdVoucherlist(Optional ByVal isexpense As Boolean = False)
        If fVoucher Is Nothing Then
            fVoucher = New VoucherWiseReport
            With fVoucher
                .MdiParent = fMainForm
                If isexpense Then
                    Dim df As Date
                    Dim dt As Date
                    df = DateValue("01/" & Month(Date.Now) & "/" & Year(Date.Now))
                    dt = DateAdd(DateInterval.Month, 1, DateValue("01/" & Month(Date.Now) & "/" & Year(Date.Now)))
                    dt = DateAdd(DateInterval.Day, -1, DateValue("01/" & Month(dt) & "/" & Year(dt)))
                    .cldrStartDate.Value = Format(DateValue(df), DtFormat)
                    .cldrEnddate.Value = Format(DateValue(dt), DtFormat)
                    .rdoExpense.Checked = True
                    .GroupBox3.Visible = False
                End If
                .Show()
                With MenuStrip1.Items.Add("Voucher Analysis")
                    .Tag = "VAS"
                    AddHandler .Click, AddressOf MenuStripClick
                End With
            End With
        Else
            fVoucher.Focus()
        End If

    End Sub

    Private Sub fVoucher_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fVoucher.FormClosed
        fVoucher = Nothing
        removeMenuItem("VAS")
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        About.ShowDialog()
    End Sub

    Private Sub ProfitAnalysisToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProfitAnalysisToolStripMenuItem.Click
        loadProfitAnalisis()
    End Sub
    Private Sub loadProfitAnalisis()
        If fpanalysis Is Nothing Then
            fpanalysis = New Profitanalysis
            fpanalysis.MdiParent = fMainForm
            fpanalysis.Show()
            With MenuStrip1.Items.Add("Profit Analysis")
                .Tag = "PA"
                AddHandler .Click, AddressOf MenuStripClick
            End With
        Else
            fpanalysis.Focus()
        End If

    End Sub

    Private Sub fpanalysis_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fpanalysis.FormClosed
        fpanalysis = Nothing
        removeMenuItem("PA")
    End Sub


    Private Sub mnusalesman_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnusalesman.Click
        Dim frm As New SalesMan
        frm.ShowDialog()
        frm = Nothing
    End Sub

    Private Sub mnuvat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuvat.Click
        If enableGCC Then
            Dim frm As New GCCVatMaster
            frm.ShowDialog()
            frm = Nothing
        Else
            Dim frm As New VatMaster
            frm.ShowDialog()
            frm = Nothing
        End If

    End Sub

    Private Sub mnureconciliation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnureconciliation.Click
        loadReconciliation()
    End Sub
    Private Sub loadReconciliation()
        If freconciliation Is Nothing Then
            freconciliation = New FinancialStatements
            freconciliation.isreconcil = True
            freconciliation.MdiParent = fMainForm
            freconciliation.cmbcategory.Text = "Bank"
            freconciliation.Show()
            With MenuStrip1.Items.Add("Reconciliation")
                .Tag = "REC"
                AddHandler .Click, AddressOf MenuStripClick
            End With
        Else
            freconciliation.Focus()
        End If
    End Sub
    Public Sub loadAccDetFromFstatus(ByVal tp As String)

        If fAccdet Is Nothing Then
            fAccdet = New FinancialStatements
            fAccdet.isfromFstatus = True
            fAccdet.MdiParent = fMainForm
            With fAccdet
                Select Case tp
                    Case "PDCR"
                        .cmbcategory.Text = "P.D.C.(R)"
                    Case "PDCI"
                        .cmbcategory.Text = "P.D.C.(I)"
                    Case "CASH"
                        .cmbcategory.Text = "Cash"
                    Case "BANK"
                        .cmbcategory.Text = "Bank"
                    Case "CUSTOMER"
                        .cmbcategory.Text = "Customer"
                    Case "SUPPLIER"
                        .cmbcategory.Text = "Supplier"
                    Case "POTH"
                        .optpoth.Checked = True
                    Case "ROTH"
                        .optroth.Checked = True
                End Select
                .rdoAccountBalance.Checked = True
            End With
            fAccdet.Show()
        Else
            fAccdet.Focus()
        End If

    End Sub

    Private Sub freconciliation_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles freconciliation.FormClosed
        freconciliation = Nothing
        removeMenuItem("REC")
    End Sub

    Private Sub mnuRptCustom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRptCustom.Click
        Dim frm As New RptFormatfrm
        frm.isFromMenu = True
        frm.ShowDialog()
        frm = Nothing
    End Sub
    Public Sub LoadJIS(Optional ByVal KeyId As Long = 0)
        If (fInvoice Is Nothing) Then
            fInvoice = New JobSalesInvoice
            If (KeyId <> 0) Then
                fInvoice.isModi = True
            End If
            fInvoice.MdiParent = fMainForm
            fInvoice.Show()
        Else
            fInvoice.Focus()
        End If
        If (KeyId <> 0) Then
            fInvoice.isModi = True
            fInvoice.CheckNLoad(KeyId)
        End If
    End Sub



    Private Sub fInvoice_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fInvoice.FormClosed
        fInvoice = Nothing
    End Sub

    Private Sub mnujobinvoicelist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnujobinvoicelist.Click
        ldViewPrint("JIS")
    End Sub

    Private Sub mnujjobprofit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnujjobprofit.Click


        If fJOProfitanalysis Is Nothing Then
            fJOProfitanalysis = New JobList
            With fJOProfitanalysis
                .MdiParent = fMainForm
                .rptCategory = 10
                .lblcap.Text = "JOB LIST"
                .Show()
            End With
        Else
            fJOProfitanalysis.Focus()
        End If
    End Sub

    Private Sub fJOProfitanalysis_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fJOProfitanalysis.FormClosed
        fJOProfitanalysis = Nothing
    End Sub

    Private Sub DeliveredListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeliveredListToolStripMenuItem.Click
        If fJOBLstDelivey Is Nothing Then
            fJOBLstDelivey = New JobList
            fJOBLstDelivey.MdiParent = fMainForm
            fJOBLstDelivey.rptCategory = 12
            fJOBLstDelivey.Show()
            With MenuStrip1.Items.Add("JOB Delivery")
                .Tag = "JBD"
                AddHandler .Click, AddressOf MenuStripClick
            End With
        Else
            fJOBLstDelivey.rptCategory = 12
            fJOBLstDelivey.Focus()
        End If

    End Sub

    Private Sub PriceManagementToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PriceManagementToolStripMenuItem.Click
        loadPricemanagement()

    End Sub
    Private Sub loadPricemanagement()
        If fpricemanagement Is Nothing Then
            fpricemanagement = New PricemanagementFrm
            fpricemanagement.MdiParent = fMainForm
            fpricemanagement.Show()
            With MenuStrip1.Items.Add("Price Management")
                .Tag = "PM"
                AddHandler .Click, AddressOf MenuStripClick
            End With
        Else
            fpricemanagement.Focus()
        End If
    End Sub

    Private Sub fpricemanagement_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fpricemanagement.FormClosed
        fpricemanagement = Nothing
        removeMenuItem("PM")
    End Sub


    Private Sub mnustockadjustment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnustockadjustment.Click
        LoadSTKADJTI()
    End Sub
    Public Sub LoadSTKADJTI(Optional ByVal KeyId As Long = 0)
        If fStockAdjTI Is Nothing Then
N:
            fStockAdjTI = New StockAdjustmentFrm
            If KeyId <> 0 Then
                fStockAdjTI.isModi = True
            End If
            fStockAdjTI.MdiParent = Me
            fStockAdjTI.cmbVoucherTp.SelectedIndex = 0
            fStockAdjTI.Show()
            With MenuStrip1.Items.Add("Stock IN")
                .Tag = "TI"
                AddHandler .Click, AddressOf MenuStripClick
            End With
        Else
            fStockAdjTI.Focus()
        End If
        If KeyId <> 0 Then
            fStockAdjTI.isModi = True
            fStockAdjTI.CheckNLoad(KeyId)
        End If
    End Sub
    Public Sub LoadSTKADJTO(Optional ByVal KeyId As Long = 0)
        If fStockAdjTO Is Nothing Then
N:
            fStockAdjTO = New StockAdjustmentFrm
            If KeyId <> 0 Then
                fStockAdjTO.isModi = True
            End If
            fStockAdjTO.MdiParent = Me
            fStockAdjTO.Show()
            fStockAdjTO.cmbVoucherTp.SelectedIndex = 1
            With MenuStrip1.Items.Add("Stock Out")
                .Tag = "TO"
                AddHandler .Click, AddressOf MenuStripClick
            End With
        Else
            fStockAdjTO.Focus()
        End If
        If KeyId <> 0 Then
            fStockAdjTO.isModi = True
            fStockAdjTO.CheckNLoad(KeyId)
        End If
    End Sub

    Private Sub VoucherHistoryTIToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VoucherHistoryTIToolStripMenuItem.Click
        ldViewPrint("TI")
    End Sub

    Private Sub VoucherHistoryTOToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VoucherHistoryTOToolStripMenuItem.Click
        ldViewPrint("TO")
    End Sub

    Private Sub ProjectMasterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProjectMasterToolStripMenuItem.Click
        ldCJob()
    End Sub
    Private Sub ldCJob()
        If Not fCJOB Is Nothing Then fCJOB.Close() : fCJOB = Nothing
        fCJOB = New ContractJobFrm
        With fCJOB
            .MdiParent = fMainForm
            .Show()
        End With
    End Sub

    Private Sub OtherRVToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OtherRVToolStripMenuItem.Click
        LoadRVO()
    End Sub

    Private Sub fRVO_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fRVO.FormClosed
        fRVO = Nothing
        removeMenuItem("RVO")
    End Sub

    Public Sub LoadGIN(Optional ByVal KeyId As Long = 0)
        If FGin Is Nothing Then
N:
            FGin = New GoodsTransferFrm
            If KeyId <> 0 Then
                FGin.isModi = True
            End If
            FGin.MdiParent = Me
            FGin.Show()
            With MenuStrip1.Items.Add("Location Transfer")
                .Tag = "LT"
                AddHandler .Click, AddressOf MenuStripClick
            End With
        Else
            FGin.Focus()
        End If
        If KeyId <> 0 Then
            FGin.isModi = True
            FGin.CheckNLoad(KeyId)
        End If
    End Sub
    Public Sub LoadQTI(Optional ByVal KeyId As Long = 0)
        If FQti Is Nothing Then
N:
            FQti = New CustomerQuotation
            If KeyId <> 0 Then
                FQti.isModi = True
            End If
            FQti.MdiParent = Me
            FQti.Show()
            With MenuStrip1.Items.Add("Quotation")
                .Tag = "QTI"
                AddHandler .Click, AddressOf MenuStripClick
            End With
        Else
            FQti.Focus()
        End If
        If KeyId <> 0 Then
            FQti.isModi = True
            FQti.CheckNLoad(KeyId)
        End If
    End Sub
    Public Sub LoadEQTI(Optional ByVal KeyId As Long = 0)
        If FEQti Is Nothing Then
N:
            FEQti = New QuotationFrm
            If KeyId <> 0 Then
                FEQti.isModi = True
            End If
            FEQti.MdiParent = Me
            FEQti.Show()
        Else
            FEQti.Focus()
        End If
        If KeyId <> 0 Then
            FEQti.isModi = True
            FEQti.CheckNLoad(KeyId)
        End If
    End Sub

    Private Sub FGin_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles FGin.FormClosed
        FGin = Nothing
        removeMenuItem("LT")
    End Sub

    Private Sub JobListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JobListToolStripMenuItem.Click
        If fCJOBLst Is Nothing Then
            fCJOBLst = New ContractJobList
            With fCJOBLst
                .MdiParent = fMainForm
                .rptCategory = 15
                .Show()
            End With
        Else
            fCJOBLst.Focus()
        End If
    End Sub

    Private Sub fCJOBLst_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fCJOBLst.FormClosed
        fCJOBLst = Nothing
    End Sub

    Private Sub fECJOB_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fECJOB.FormClosed
        fECJOB = Nothing
    End Sub

    Private Sub mnuMTNCreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMTNCreate.Click
        LoadGIN()
    End Sub

    Private Sub mnuMTNHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMTNHistory.Click
        ldViewPrint("MTN", True)
    End Sub

    Private Sub JobProfitAnalysisToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JobProfitAnalysisToolStripMenuItem.Click
        If fCJOBPLst Is Nothing Then
            fCJOBPLst = New ContractJobList
            With fCJOBPLst
                .MdiParent = fMainForm
                .rptCategory = 19
                .Panel3.Visible = False
                .FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
                .Show()
            End With
        Else
            fCJOBPLst.Focus()
        End If
    End Sub

    Private Sub fCJOBPLst_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fCJOBPLst.FormClosed
        fCJOBPLst = Nothing
    End Sub


    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        If disableShowAlert Then Exit Sub
        Dim rt As Boolean
        rt = IIf(userType, getRight(133, CurrentUser), True)
        If enableSerialnumber And rt Then
            showAlerts(0)
        End If
        'PDC ALERT********
        rt = IIf(userType, getRight(132, CurrentUser), True)
        If rt Then
            showAlerts(1)
        End If
        rt = IIf(userType, getRight(131, CurrentUser), True)
        If rt Then
            showAlerts(2)
        End If
        '************************
        'VISA EXPIRY
        rt = IIf(userType, getRight(247, CurrentUser), True)
        If rt Then
            showAlerts(3)
        End If
        'ID EXPIRY
        rt = IIf(userType, getRight(247, CurrentUser), True)
        If rt Then
            showAlerts(4)
        End If
        'LIECENSE EXPIRY
        rt = IIf(userType, getRight(247, CurrentUser), True)
        If rt Then
            showAlerts(5)
        End If
        'PASSPORT EXPIRY
        rt = IIf(userType, getRight(247, CurrentUser), True)
        If rt Then
            showAlerts(6)
        End If
        'ToolStrip1.Visible = True
        'MenuStrip.Visible = True
    End Sub
    Public Sub showAlerts(ByVal alerttype As Integer, Optional ByVal isFromMenu As Boolean = False)
        Try
            Exit Sub
            _objcmnbLayer = New clsCommon_BL
            Dim dtShowAlert As DataTable = Nothing
            dtShowAlert = _objcmnbLayer._fldDatatable("select Id from lastalertdate UserTb where " & _
                                                      "UserId='" & CurrentUser & "' and lastalertdate<>'" & Format(DateValue(Date.Now), "yyyy/MM/dd") & "'")
            If dtShowAlert.Rows.Count = 0 Then Exit Sub
            If dtShowAlert.Rows.Count > 0 Then
                If Val(dtShowAlert(0)("id") & "") = 0 Then Exit Sub
            End If
            dtShowAlert = _objcmnbLayer.returnShowAlert(alerttype, DateValue(Date.Now), DateValue(Date.Now), alerttype)
            If dtShowAlert.Rows.Count > 0 Or isFromMenu Then
                If isFromMenu Then
                    fshowAlert = New ShowAlert
                    With fshowAlert
                        Select Case alerttype
                            Case 0
                                .Text = "[Available Serial Number List]"
                        End Select
                        .AlertType = alerttype
                        .grdVoucher.DataSource = dtShowAlert
                        .setGridHead()
                        .btnothcancel.Text = "E&xit"
                        .MdiParent = fMainForm
                        .dtShowAlert = dtShowAlert
                        .plmain.Visible = True
                        .Show()
                    End With
                Else
                    Dim frm As New ShowAlert
                    With frm
                        .AlertType = alerttype
                        .grdVoucher.DataSource = dtShowAlert
                        .setGridHead()
                        Select Case alerttype
                            Case 0
                                .Text = .Text & " [Serial Number Expiry List]"
                            Case 1
                                .Text = .Text & " [PDC Issued Expiry List]"
                            Case 2
                                .Text = .Text & " [PDC Received Expiry List]"
                        End Select
                        .WindowState = FormWindowState.Normal
                        .StartPosition = FormStartPosition.CenterScreen
                        .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
                        .ShowDialog()
                    End With
                    frm = Nothing
                End If
                'Else
                '    MsgBox("Expired Serial Numbers Not Found", MsgBoxStyle.Exclamation)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub fshowAlert_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fshowAlert.FormClosed
        fshowAlert = Nothing
    End Sub

    Private Sub SerialNumberExpiryToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SerialNumberExpiryToolStripMenuItem1.Click
        'showAlerts(0, True)
        showSerialNumbers()
    End Sub
    Private Sub showSerialNumbers()
        If fserialNo Is Nothing Then
            fserialNo = New AvailableSerialNumberListFrm
        End If
        fserialNo.MdiParent = fMainForm
        fserialNo.Show()
        With MenuStrip1.Items.Add("Serial Numbers")
            .Tag = "SM"
            AddHandler .Click, AddressOf MenuStripClick
        End With
    End Sub

    Private Sub mnustatemaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnustatemaster.Click
        Dim frm As New StateCodeCreation
        frm.ShowDialog()
        frm = Nothing
    End Sub

    Private Sub mnugstmaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugstmaster.Click
        Dim frm As New HSNCodeMaster
        frm.ShowDialog()
        frm = Nothing
    End Sub

    Private Sub FQti_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles FQti.FormClosed
        FQti = Nothing
        removeMenuItem("QTI")
    End Sub

    Private Sub mnuCQTI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCQTI.Click
        LoadQTI()
    End Sub

    Private Sub FEQti_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles FEQti.FormClosed
        FEQti = Nothing
    End Sub

    Private Sub mnutaxreport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnutaxreport.Click
        loadTaxReport()

    End Sub
    Private Sub loadTaxReport()
        If enableGCC Then
            If fVATReport Is Nothing Then
                fVATReport = New UAETaxReport
                fVATReport.MdiParent = fMainForm
                fVATReport.Show()
                With MenuStrip1.Items.Add("VAT Report")
                    .Tag = "VR"
                    AddHandler .Click, AddressOf MenuStripClick
                End With
            Else
                fVATReport.Focus()
            End If

        Else
            If fTaxReport Is Nothing Then
                fTaxReport = New TaxReportFrm
                fTaxReport.MdiParent = fMainForm
                fTaxReport.Show()
                With MenuStrip1.Items.Add("GST Report")
                    .Tag = "GR"
                    AddHandler .Click, AddressOf MenuStripClick
                End With
            Else
                fTaxReport.Focus()
            End If

        End If
    End Sub

    Private Sub fTaxReport_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fTaxReport.FormClosed
        fTaxReport = Nothing
        removeMenuItem("GR")
    End Sub

    Private Sub mnubarcode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnubarcode.Click
        Try
            Dim constring, sbstring, MyServer As String
            constring = readXml()
            Dim _dtpasswrd As String
            Dim _dtgetpsswrd As New DataTable
            '**server
            sbstring = Mid(constring, 1, constring.IndexOf(";"))
            sbstring = Mid(sbstring, sbstring.IndexOf("=") + 2)
            MyServer = sbstring
            ''**database
            'sbstring = Mid(constring, constring.LastIndexOf(";") + 2)
            'sbstring = Mid(sbstring, sbstring.IndexOf("=") + 2)
            'txtdbto.Text = sbstring
            ''**password
            'sbstring = Mid(constring, constring.IndexOf(";") + 2)
            'sbstring = Mid(sbstring, sbstring.IndexOf(";") + 2)
            'sbstring = Mid(sbstring, 1, sbstring.IndexOf(";"))
            'sbstring = Mid(sbstring, sbstring.IndexOf("=") + 2)
            'lblpwd.Text = sbstring
            ''**user
            'sbstring = Mid(constring, constring.IndexOf(";") + 2)
            'sbstring = Mid(sbstring, 1, sbstring.IndexOf(";"))
            'sbstring = Mid(sbstring, sbstring.IndexOf("=") + 2)
            'lbluser.Text = sbstring
            '**password
            sbstring = Mid(constring, constring.IndexOf(";") + 2)
            sbstring = Mid(sbstring, sbstring.IndexOf(";") + 2)
            sbstring = Mid(sbstring, 1, sbstring.IndexOf(";"))
            _dtpasswrd = Mid(sbstring, sbstring.IndexOf("=") + 2)


            Dim fpath As String = APath & "ProfitBarcode.exe"
            Process.Start(fpath, "1,0," & MyServer & "," & MyDatabase & ",sa," & _dtpasswrd & ",1")
            'Shell(fpath & "2,0,server,profitacc,sa,clickclick")
        Catch ex As Exception
            MsgBox("Path not Found", MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Private Sub mnupdctransfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnupdctransfer.Click
        loadPDCList()
    End Sub
    Private Sub loadPDCList()
        If fPDCTransfer Is Nothing Then
            fPDCTransfer = New PDCTransfer
            fPDCTransfer.MdiParent = fMainForm
            'fproductMast.Top = Me.Top + 500
            fPDCTransfer.Show()
            With MenuStrip1.Items.Add("PDC List")
                .Tag = "PDCL"
                AddHandler .Click, AddressOf MenuStripClick
            End With
        Else
            fPDCTransfer.Focus()
        End If
    End Sub

    Private Sub fPDCTransfer_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fPDCTransfer.FormClosed
        fPDCTransfer = Nothing
        removeMenuItem("PDCL")
    End Sub

    Private Sub fserialNo_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fserialNo.FormClosed
        fserialNo = Nothing
        removeMenuItem("SM")
    End Sub

    Private Sub fAccdet_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fAccdet.FormClosed
        fAccdet = Nothing
    End Sub

    Private Sub fPOS_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fPOS.FormClosed
        fPOS = Nothing
        ToolStrip1.Visible = True
        MenuStrip.Visible = True
        If Not fhome Is Nothing Then fhome.Show()
    End Sub

    Private Sub btnposInv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnposInv.Click
        If enableUserCodeOnPosLogin Then
            fPosLogin = New POSLogin
            fPosLogin.ShowDialog()
        Else
            Dim dt As DataTable
            Dim str, userid, mchname As String
            str = "declare @userCounter varchar(20) declare @islogined int declare @mchname varchar(50) declare @userid varchar(20) Select @userCounter=userCounter from UserTb where UserId='" & CurrentUser & "'"
            str = str & "select @userid=userid,@mchname=mchname from POSLoginLogTb where counter=case when isnull(@userCounter,'')='' then 'C1' else @userCounter end select @userCounter userCounter,@userid userid,@mchname mchname"
            _objcmnbLayer = New clsCommon_BL
            dt = _objcmnbLayer._fldDatatable(str)
            Dim counter As String = ""
            If dt.Rows.Count > 0 Then
                counter = Trim(dt(0)("userCounter") & "")
                userid = Trim(dt(0)("userid") & "")
                mchname = Trim(dt(0)("mchname") & "")
            Else
                counter = ""
                userid = ""
                mchname = ""
            End If
            If counter = "" Then counter = "C1"
            If enableUserCodeOnPosLogin Then
                fPosLogin = New POSLogin
                fPosLogin.MdiParent = fMainForm
                fPosLogin.ShowDialog()
            Else
                If counter = "" Then
                    MsgBox("Please set counter in Uuser Permission!", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If
                If userid <> "" Then
                    MsgBox("Counter " & counter & " already logged in by " & userid & " from " & mchname, MsgBoxStyle.Exclamation)
                    Exit Sub
                End If
                If Not fhome Is Nothing Then fhome.Hide()
                ToolStrip1.Visible = False
                MenuStrip.Visible = False
                LoadPOSIS(, counter)
            End If
        End If
    End Sub

    Private Sub mnumemberAdmission_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnumemberAdmission.Click
        ldTempleAdmission()
    End Sub

    Private Sub mnuvazhipaduSales_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuvazhipaduSales.Click
        If enableVazhipaduSalesWithMutipleNamesAndItems Then
            ldVazhipaduSalesWithMutipleNames()
        Else
            ldVazhipaduSales()
        End If

    End Sub
    Private Sub ldVazhipaduSales()
        If Not fVazhipaduSales Is Nothing Then fVazhipaduSales.Close() : fVazhipaduSales = Nothing
        fVazhipaduSales = New VazhipaduSalesFrm
        With fVazhipaduSales
            .MdiParent = fMainForm
            .Show()
        End With
    End Sub
    Private Sub ldVazhipaduSalesWithMutipleNames()
        If Not fVazhipaduSalesN Is Nothing Then fVazhipaduSalesN.Close() : fVazhipaduSalesN = Nothing
        fVazhipaduSalesN = New VazhipaduSalesNewFrm
        With fVazhipaduSalesN
            .MdiParent = fMainForm
            .Show()
        End With
    End Sub


    Private Sub mnuVazhipaduMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuVazhipaduMaster.Click
        Dim frm As New VazhipaduMasterFrm
        With frm
            .Condition = "GrpSetOn In ('Vazhipadu')"
            .ShowDialog()
        End With
    End Sub

    Private Sub mnuvazhipadusalesHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuvazhipadusalesHistory.Click
        ldViewPrint("TIS")
    End Sub

    Private Sub FuelMeterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FuelMeterToolStripMenuItem.Click
        Dim ffuelmaster As New AddFuelMeterReadingFrm
        ffuelmaster.ShowDialog()
        ffuelmaster = Nothing
    End Sub

    Private Sub fFuelView_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fFuelView.FormClosed
        fFuelView = Nothing
    End Sub

    Private Sub mnuFuelbillingHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFuelbillingHistory.Click
        ldFuelViewPrint("IS")
    End Sub

    Private Sub fJOBMST_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fJOBMST.FormClosed
        fJOBMST = Nothing
    End Sub

    Private Sub mnupendingJob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnupendingJob.Click
        If fJOBPendingLst Is Nothing Then
            fJOBPendingLst = New JobPendingAssign
            With fJOBPendingLst
                .MdiParent = fMainForm
                .rptCategory = 22
                .Show()
            End With
        Else
            fJOBPendingLst.Focus()
        End If

    End Sub

    Private Sub fJOBPendingLst_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fJOBPendingLst.FormClosed
        fJOBPendingLst = Nothing
    End Sub

    Public Sub LoadMSI(Optional ByVal KeyId As Long = 0)
        If fManufacturing Is Nothing Then
N:
            fManufacturing = New ManufacturingFrm
            If KeyId <> 0 Then
                fManufacturing.isModi = True
            End If
            fManufacturing.MdiParent = Me
            fManufacturing.Show()
        Else
            fManufacturing.Focus()
        End If
        If KeyId <> 0 Then
            fManufacturing.isModi = True
            fManufacturing.CheckNLoad(KeyId)
        End If
    End Sub

    Private Sub fManufacturing_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fManufacturing.FormClosed
        fManufacturing = Nothing
    End Sub

    Private Sub munproductionvoucher_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles munproductionvoucher.Click
        LoadMSI()
    End Sub
    Public Sub ldProdictionViewPrint()
        If fProductionView Is Nothing Then
            fProductionView = New ProductionInvReport
            fProductionView.MdiParent = Me
            fProductionView.ldType = "MI"
            fProductionView.Show()
        Else
            fProductionView.ldType = "MI"
            fProductionView.Focus()
        End If

    End Sub

    Private Sub fProductionView_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fProductionView.FormClosed
        fProductionView = Nothing
    End Sub

    Private Sub mnuproductionvoucherHis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuproductionvoucherHis.Click
        ldProdictionViewPrint()
    End Sub

    Private Sub mnuwebsettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuwebsettings.Click
        Dim frm As New WebsettingsFrm
        frm.ShowDialog()
        frm = Nothing
    End Sub

    Private Sub mnuwebsalesorder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuwebsalesorder.Click
        fwebInvReport = New WebInvReport
        fwebInvReport.MdiParent = Me
        fwebInvReport.Show()
    End Sub

    Private Sub fwebInvReport_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fwebInvReport.FormClosed
        fwebInvReport = Nothing
    End Sub


    Private Sub mnucardtypews_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnucardtypews.Click
        fcardType = New CardTypeMasterFrm
        fcardType.ShowDialog()
        fcardType = Nothing
    End Sub

    Private Sub mnuvehicle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuvehicle.Click
        loadVehicleMaster()
    End Sub
    Public Sub loadVehicleMaster(Optional ByVal customerid As Integer = 0)
        fVehiclemaster = New VehiclemasterFrm
        With fVehiclemaster
            If customerid > 0 Then
                .loadVehiclemaster(customerid)
            End If
            .MdiParent = fMainForm
            .Show()
        End With
    End Sub

    Private Sub fVehiclemaster_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fVehiclemaster.FormClosed
        fVehiclemaster = Nothing
    End Sub

    Private Sub mnudcardissuews_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnudcardissuews.Click
        LoadDIS()
    End Sub
    Public Sub LoadDIS(Optional ByVal KeyId As Long = 0)
        If fDIS Is Nothing Then
N:
            fDIS = New WSCardSalesInvoice
            If KeyId <> 0 Then
                fDIS.isModi = True
            End If
            fDIS.MdiParent = Me
            fDIS.Show()
        Else
            fDIS.Focus()
        End If
        If KeyId <> 0 Then
            fDIS.isModi = True
            fDIS.CheckNLoad(KeyId)
        End If
    End Sub

    Private Sub fDIS_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fDIS.FormClosed
        fDIS = Nothing
    End Sub

    Public Sub LoadWSServiceIS(Optional ByVal KeyId As Long = 0)
        If fSIS Is Nothing Then
N:
            fSIS = New WSServiceSalesInvoice
            If KeyId <> 0 Then
                fSIS.isModi = True
            End If
            fSIS.MdiParent = Me
            fSIS.Show()
        Else
            fSIS.Focus()
        End If
        If KeyId <> 0 Then
            fSIS.isModi = True
            fSIS.CheckNLoad(KeyId)
        End If
    End Sub

    Private Sub fSIS_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fSIS.FormClosed
        fSIS = Nothing
    End Sub

    Private Sub mnuserviceinvoicews_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuserviceinvoicews.Click
        LoadWSServiceIS()
    End Sub

    Private Sub mnuwscardrenew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuwscardrenew.Click
        fDCR = New WSCardRenew
        fDCR.ShowDialog()
        fDCR = Nothing
    End Sub

    Private Sub mnudcardhistoryws_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnudcardhistoryws.Click
        Dim frm As New CardHistoryFrm
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub btncardsales_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncardsales.Click
        LoadDIS()
    End Sub

    Private Sub btnwsservice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnwsservice.Click
        LoadWSServiceIS()
    End Sub

    Private Sub ShortageEntryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShortageEntryToolStripMenuItem.Click
        LoadStockShortage()
    End Sub
    Public Sub LoadStockShortage(Optional ByVal KeyId As Long = 0)
        If fSTSh Is Nothing Then
N:
            fSTSh = New StockShortageFrm
            If KeyId <> 0 Then
                fSTSh.isModi = True
            End If
            fSTSh.MdiParent = Me
            fSTSh.Show()
        Else
            fSTSh.Focus()
        End If
        If KeyId <> 0 Then
            fSTSh.isModi = True
            fSTSh.CheckNLoad(KeyId)
        End If
    End Sub

    Private Sub fSTSh_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fSTSh.FormClosed
        fSTSh = Nothing
    End Sub

    Private Sub mnuconsumption_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuconsumption.Click
        Dim frm As New MaterialConusumptionComparisonFrm
        frm.MdiParent = fMainForm
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub btnRVCust_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRVCust.Click
        LoadRV()
    End Sub


    Private Sub btnpvother_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpvother.Click
        LoadPVO()
    End Sub

    Private Sub btnPVSupplier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPVSupplier.Click
        LoadPV()
    End Sub

    Private Sub fDocIS_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fDocIS.FormClosed
        fDocIS = Nothing
    End Sub

    Private Sub mnuservicelist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuservicelist.Click
        ldViewWSPrint("IS", 2)
    End Sub


    Private Sub fViewWS_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fViewWS.FormClosed
        fView = Nothing
    End Sub

    Private Sub mnuroom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuroom.Click
        If fRoom Is Nothing Then
            fRoom = New LodgeRoomFrm
        End If
        fRoom.MdiParent = fMainForm
        fRoom.Show()
    End Sub

    Private Sub fCheckin_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fCheckin.FormClosed
        fCheckin = Nothing
    End Sub

    Private Sub mnucheckin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnucheckin.Click
        If fCheckin Is Nothing Then
            fCheckin = New LodgeCheckInFrm
            fCheckin.MdiParent = fMainForm
            fCheckin.Show()
        End If
    End Sub

    Private Sub mnuhminvoicelist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ldViewPrint("JIS")
    End Sub

    Private Sub fRoom_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fRoom.FormClosed
        fRoom = Nothing
    End Sub

    Private Sub fPosLogin_sendUsercode(ByVal usercode As String, ByVal usertype As Integer, ByVal counter As String) Handles fPosLogin.sendUsercode

        LoadPOSIS(, counter)
    End Sub

    Private Sub mnustockmovement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnustockmovement.Click
        loadStockMovement()

    End Sub
    Private Sub loadStockMovement()
        If fstockmovement Is Nothing Then
            fstockmovement = New StockmovementFrm
            fstockmovement.MdiParent = fMainForm
            fstockmovement.Show()
            With MenuStrip1.Items.Add("Stock Movement")
                .Tag = "SM"
                AddHandler .Click, AddressOf MenuStripClick
            End With
        Else
            fstockmovement.Focus()
        End If
    End Sub

    Private Sub fstockmovement_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fstockmovement.FormClosed
        fstockmovement = Nothing
        removeMenuItem("SM")
    End Sub

    Private Sub MainFrm_MdiChildActivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MdiChildActivate

    End Sub

    Private Sub MainFrm_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        lblcompany.Left = (Me.Width / 2) - (lblcompany.Width / 2)
    End Sub

    Private Sub mnudeliverywiseoutstanding_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnudeliverywiseoutstanding.Click
        If fdeliverywiseoutstanding Is Nothing Then fdeliverywiseoutstanding = New DeliverywiseOutstandingFrm
        With fdeliverywiseoutstanding
            .MdiParent = fMainForm
            .Show()
        End With
    End Sub

    Private Sub fdeliverywiseoutstanding_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fdeliverywiseoutstanding.FormClosed
        fdeliverywiseoutstanding = Nothing
    End Sub

    Private Sub fVazhipaduSalesN_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fVazhipaduSalesN.FormClosed
        fVazhipaduSalesN = Nothing
    End Sub

    Private Sub mnuQTIHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuQTIHistory.Click
        ldViewDOCPrint("QTI")
    End Sub
    Public Sub ldViewDOCPrint(ByVal tp As String, Optional ByVal isdoc As Boolean = False)
        If fViewDOC Is Nothing Then
            fViewDOC = New DocReportFrm
            fViewDOC.MdiParent = Me
            fViewDOC.ldType = tp
            fViewDOC.isDoc = isdoc
            fViewDOC.Show()
            With MenuStrip1.Items.Add("DOC Report - " & tp)
                .Tag = "DOCR"
                AddHandler .Click, AddressOf MenuStripClick
            End With
        Else
            fViewDOC.ldType = tp
            fViewDOC.Focus()
        End If
    End Sub

    Private Sub fViewDOC_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fViewDOC.FormClosed
        fViewDOC = Nothing
        removeMenuItem("DOCR")
    End Sub

    Private Sub MessageToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnusms.Click
        SendSMSFrm.ShowDialog()
    End Sub

    Private Sub mnuempMast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuempMast.Click
        If Not fEmployee Is Nothing Then
            fEmployee.Focus()
        Else
            fEmployee = New EmployeeMasterFrm
            With fEmployee
                .MdiParent = Me
                .Show()
            End With
        End If

    End Sub

    Private Sub fEmployee_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fEmployee.FormClosed
        fEmployee = Nothing
    End Sub


    Private Sub fworksheet_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fworksheet.FormClosed
        fworksheet = Nothing
    End Sub

    Private Sub mnupaymentbooking_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnupaymentbooking.Click
        If Not fpaymentbooking Is Nothing Then
            fpaymentbooking.Focus()
        Else
            fpaymentbooking = New PaymentBookingFrm
            With fpaymentbooking
                .MdiParent = Me
                .Show()
            End With
        End If
    End Sub

    Private Sub fpaymentbooking_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fpaymentbooking.FormClosed
        fpaymentbooking = Nothing
    End Sub

    Private Sub mnujobcard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnujobcard.Click
        If Not fjobcard Is Nothing Then
            fjobcard.Focus()
        Else
            fjobcard = New GarrageJobCardFrm
            With fjobcard
                .MdiParent = Me
                .Show()
            End With
        End If
    End Sub

    Private Sub fjobcard_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fjobcard.FormClosed
        fjobcard = Nothing
    End Sub
    Private Sub fusedcar_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fusedcar.FormClosed
        fusedcar = Nothing
    End Sub

    Private Sub mnugarrageInvList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnugarrageInvList.Click
        ldViewPrint("JIS")
    End Sub

    Private Sub mnucollection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnucollection.Click
        If fcollection Is Nothing Then fcollection = New DeliverywiseOutstandingFrm
        With fcollection
            .MdiParent = fMainForm
            .iscollection = True
            .Show()
        End With
    End Sub

    Private Sub fcollection_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fcollection.FormClosed
        fcollection = Nothing
    End Sub

    Private Sub mnusalesanalysis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnusalesanalysis.Click
        loadSalesAnalysis()
    End Sub
    Private Sub loadSalesAnalysis()
        If fsalesanalysis Is Nothing Then
            fsalesanalysis = New SalesanalysisFrm
            fsalesanalysis.MdiParent = fMainForm
            fsalesanalysis.Show()
            With MenuStrip1.Items.Add("Sales Analysis")
                .Tag = "SA"
                AddHandler .Click, AddressOf MenuStripClick
            End With
        Else
            fsalesanalysis.Focus()
        End If

    End Sub

    Private Sub fsalesanalysis_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fsalesanalysis.FormClosed
        fsalesanalysis = Nothing
        removeMenuItem("SA")
    End Sub

    Private Sub mnuincomeexpense_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuincomeexpense.Click

        ftempleIncomeExpense = New IncomeExpenseFrm
        ftempleIncomeExpense.MdiParent = fMainForm
        ftempleIncomeExpense.Show()
    End Sub

    Private Sub ftempleIncomeExpense_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles ftempleIncomeExpense.FormClosed
        ftempleIncomeExpense = Nothing
    End Sub

    Private Sub mnuposexcelltransfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuposexcelltransfer.Click
        Dim fTransferFromexel As New TransferiItemsFromExcel
        With fTransferFromexel
            .isPosTransfer = True
            .ShowDialog()
        End With
    End Sub

    Private Sub mnudesignation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnudesignation.Click
        Dim frm As New DesignationCreation
        frm.ShowDialog()
        frm = Nothing
    End Sub

    Private Sub mnudepartment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnudepartment.Click
        Dim frm As New DepartmentCreation
        frm.ShowDialog()
        frm = Nothing
    End Sub

    Private Sub AttendanceWorksheetEntryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AttendanceWorksheetEntryToolStripMenuItem.Click
        If Not fworksheet Is Nothing Then
            fworksheet.Focus()
        Else
            fworksheet = New DailyWorkSheetFrm
            With fworksheet
                .MdiParent = Me
                .Show()
            End With
        End If
    End Sub

    Private Sub mnuEmployeeWIseAttendance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEmployeeWIseAttendance.Click
        If Not fEWiseworksheet Is Nothing Then
            fEWiseworksheet.Focus()
        Else
            fEWiseworksheet = New EmpworksheetFrm
            With fEWiseworksheet
                .MdiParent = Me
                .Show()
                With MenuStrip1.Items.Add("Worksheet")
                    .Tag = "EWS"
                    AddHandler .Click, AddressOf MenuStripClick
                End With
            End With
        End If

    End Sub

    Private Sub fEWiseworksheet_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fEWiseworksheet.FormClosed
        fEWiseworksheet = Nothing
        removeMenuItem("EWS")
    End Sub

    Private Sub fVATReport_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fVATReport.FormClosed
        fVATReport = Nothing
        removeMenuItem("VR")
    End Sub
    Public Sub LoadSO(Optional ByVal KeyId As Long = 0, Optional ByVal islaundry As Boolean = False)
        If FSO Is Nothing Then
N:
            FSO = New SalesOrderFrm
            If KeyId <> 0 Then
                FSO.isModi = True
            End If
            FSO.MdiParent = Me
            FSO.islaundry = islaundry
            FSO.Show()
            With MenuStrip1.Items.Add("Sales Order")
                .Tag = "SO"
                AddHandler .Click, AddressOf MenuStripClick
            End With
        Else
            FSO.Focus()
        End If
        If KeyId <> 0 Then
            FSO.isModi = True
            FSO.CheckNLoad(KeyId)
        End If
    End Sub
    Public Sub LoadSOLaundry(Optional ByVal KeyId As Long = 0, Optional ByVal islaundry As Boolean = False)
        If FSOL Is Nothing Then
N:
            FSOL = New LaundryOrderFrm
            If KeyId <> 0 Then
                FSOL.isModi = True
            End If
            FSOL.MdiParent = Me
            FSOL.islaundry = islaundry
            FSOL.Show()
        Else
            FSOL.Focus()
        End If
        If KeyId <> 0 Then
            FSOL.isModi = True
            FSOL.CheckNLoad(KeyId)
        End If
    End Sub
    Private Sub mnuSOCreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSOCreate.Click
        LoadSO()
    End Sub

    Private Sub FSO_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles FSO.FormClosed
        FSO = Nothing
        removeMenuItem("SO")
    End Sub

    Private Sub VoucherHistoryToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VoucherHistoryToolStripMenuItem3.Click
        ldViewDOCPrint("SO")
    End Sub
    Public Sub LoadDOC(Optional ByVal KeyId As Long = 0)
        If FDOC Is Nothing Then
N:
            FDOC = New CustomerDeliverOrderFrm
            If KeyId <> 0 Then
                FDOC.isModi = True
            End If
            FDOC.MdiParent = Me
            FDOC.Show()
            With MenuStrip1.Items.Add("Delivery Order")
                .Tag = "DOC"
                AddHandler .Click, AddressOf MenuStripClick
            End With
        Else
            FDOC.Focus()
        End If
        If KeyId <> 0 Then
            FDOC.isModi = True
            FDOC.CheckNLoad(KeyId)
        End If
    End Sub

    Private Sub mnuCreateDoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCreateDoc.Click
        LoadDOC()
    End Sub

    Private Sub FDOC_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles FDOC.FormClosed
        FDOC = Nothing
        removeMenuItem("DOC")
    End Sub

    Private Sub mnuDOChistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDOChistory.Click
        ldViewDOCPrint("DOC")
    End Sub

    Private Sub JobEnquiryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JobEnquiryToolStripMenuItem.Click

    End Sub

    Private Sub mnusalesmansummary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnusalesmansummary.Click
        If fsalemansummary Is Nothing Then
            fsalemansummary = New SalesmanwiseSummaryfrm
            fsalemansummary.MdiParent = Me
            fsalemansummary.Show()
        Else
            fsalemansummary.Focus()
        End If
    End Sub

    Private Sub fsalemansummary_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fsalemansummary.FormClosed
        fsalemansummary = Nothing
    End Sub

    Private Sub mnulocation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnulocation.Click
        Dim flocation As New LocationMasterFrm
        flocation.StrCaption = "Location"
        flocation.ShowDialog()
        flocation = Nothing
    End Sub

    Private Sub mnustocktransfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnustocktransfer.Click
        LoadGIN()
    End Sub

    Private Sub mnustocktransferHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnustocktransferHistory.Click
        ldViewDOCPrint("MTN")
    End Sub

    Private Sub mnucashcustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnucashcustomer.Click
        Dim fCashCust As New CreateCashCustomerFrm
        fCashCust.ShowDialog()
        fCashCust = Nothing
    End Sub
    Public Sub LoadPO(Optional ByVal KeyId As Long = 0)
        If FPO Is Nothing Then
N:
            FPO = New PurchaseOrderFrm
            If KeyId <> 0 Then
                FPO.isModi = True
            End If
            FPO.MdiParent = Me
            FPO.Show()
            With MenuStrip1.Items.Add("Purchase Order")
                .Tag = "PO"
                AddHandler .Click, AddressOf MenuStripClick
            End With
        Else
            FPO.Focus()
        End If
        If KeyId <> 0 Then
            FPO.isModi = True
            FPO.CheckNLoad(KeyId)
        End If
    End Sub

    Private Sub mnupocreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnupocreate.Click
        LoadPO()
    End Sub

    Private Sub FPO_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles FPO.FormClosed
        FPO = Nothing
        removeMenuItem("PO")
    End Sub

    Private Sub mnupohistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnupohistory.Click
        ldViewDOCPrint("PO")
    End Sub

    Private Sub mnubooking_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnubooking.Click
        If fBooking Is Nothing Then
            fBooking = New LodgeBookingFrm
            fBooking.MdiParent = fMainForm
            fBooking.Show()
        End If
    End Sub

    Private Sub fBooking_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fBooking.FormClosed
        fBooking = Nothing
    End Sub

    Private Sub mnuadmission_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuadmission.Click
        If enableSwimmingPool Then
            If fpoolmembership Is Nothing Then
                fpoolmembership = New SwimmingPoolMemberShipFrm
                fpoolmembership.MdiParent = fMainForm
                fpoolmembership.Show()
            End If
        Else
            If fgymmembership Is Nothing Then
                fgymmembership = New GYMMemberShipFrm
                fgymmembership.MdiParent = fMainForm
                fgymmembership.Show()
            End If
        End If


    End Sub

    Private Sub fmembership_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fmembership.FormClosed
        fmembership = Nothing
    End Sub

    Private Sub mnupackage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnupackage.Click
        Dim fpackage As New MembershipItemsFrm
        fpackage.ShowDialog()
        fpackage = Nothing
    End Sub

    Private Sub FromFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FromFileToolStripMenuItem.Click
        Dim frm As New TransferiItemsFromExcel
        frm.chkopeningOnly.Visible = True
        frm.ShowDialog()
    End Sub

    Private Sub FromDBToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FromDBToolStripMenuItem.Click
        Dim frm As New DataFromDbFrm
        frm.ShowDialog()
    End Sub

    Private Sub mnulodgesettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnulodgesettings.Click
        Dim frm As New LodgeSettingsFrm
        frm.ShowDialog()
    End Sub

    Private Sub mnuaudit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuaudit.Click
        If Not fAuditReport Is Nothing Then fAuditReport.Close() : fAuditReport = Nothing
        fAuditReport = New AuditReportLodgeFrm
        With fAuditReport
            .MdiParent = fMainForm
            .Show()
        End With
    End Sub

    Private Sub fAuditReport_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fAuditReport.FormClosed
        fAuditReport = Nothing
    End Sub


    Private Sub PatientMasterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PatientMasterToolStripMenuItem.Click
        PatientInfo()
    End Sub
    Public Sub PatientInfo(Optional ByRef bOnlyOne As Boolean = False, Optional ByVal Grp As String = "Customer")
        fpatienet = New PatientInfoFrm
        With fpatienet
            .Condition = "GrpSetOn In ('" & Grp & "')"
            .iscust = True
            .MdiParent = fMainForm
            .bOnlyOne = bOnlyOne
            .Show()
        End With
    End Sub

    Private Sub fpatienet_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fpatienet.FormClosed
        fpatienet = Nothing
    End Sub

    Private Sub mnudoctor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnudoctor.Click
        Dim frm As New Doctor
        frm.ShowDialog()
    End Sub

    Private Sub mnuappointment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuappointment.Click
        If fappointment Is Nothing Then
            fappointment = New ClinicAppointmentFrm
            With fappointment
                .MdiParent = fMainForm
                .visittype = "OP"
                .Show()
                With MenuStrip1.Items.Add("OP Appointment")
                    .Tag = "OPA"
                    AddHandler .Click, AddressOf MenuStripClick
                End With
            End With
        End If
    End Sub

    Private Sub fappointment_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fappointment.FormClosed
        fappointment = Nothing
        removeMenuItem("OPA")
    End Sub

    Private Sub mnuappbooking_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuappbooking.Click
        Dim frm As New ClinicApBookingFrm
        frm.ShowDialog()
    End Sub

    Private Sub mnureferedBy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnureferedBy.Click
        Dim frm As New ReferedByDoctorsFrm
        frm.ShowDialog()
    End Sub

    Private Sub mnudoctordesk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnudoctordesk.Click
        ToolStrip1.Visible = False
        MenuStrip.Visible = False
        If fDoctordesk Is Nothing Then
            fDoctordesk = New DoctorDeskFrm
            fDoctordesk.MdiParent = fMainForm
            fDoctordesk.Show()
        Else
            fDoctordesk.Focus()
        End If
    End Sub

    Private Sub fDoctordesk_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fDoctordesk.FormClosed
        ToolStrip1.Visible = True
        MenuStrip.Visible = True
        fDoctordesk = Nothing
    End Sub

    Private Sub mnucreatefollowup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnucreatefollowup.Click
        If fpatientfollowup Is Nothing Then
            fpatientfollowup = New PatientFollowupFrm
            fpatientfollowup.MdiParent = fMainForm
            fpatientfollowup.Show()
        Else
            fpatientfollowup.Focus()
        End If
    End Sub

    Private Sub fpatientfollowup_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fpatientfollowup.FormClosed
        fpatientfollowup = Nothing
    End Sub

    Private Sub mnuLaundryOderlist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLaundryOderlist.Click
        If flaundryList Is Nothing Then
            flaundryList = New LaundryReportFrm
            With flaundryList
                .MdiParent = Me
                .ldType = "SO"
                .isDoc = True
                .Show()
            End With
        Else
            flaundryList.ldType = "SO"
            flaundryList.Focus()
        End If
    End Sub

    Private Sub flaundryList_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles flaundryList.FormClosed
        flaundryList = Nothing
    End Sub

    Private Sub FSOL_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles FSOL.FormClosed
        FSOL = Nothing
    End Sub

    Private Sub mnulaundryOrderForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnulaundryOrderForm.Click
        LoadSOLaundry(0, True)
    End Sub

    Private Sub mnuservice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuservice.Click
        If fClinicService Is Nothing Then
            fClinicService = New ServiceItemCollectionFrm
            With fClinicService
                .MdiParent = Me
                .Show()
            End With
        Else
            fClinicService.Focus()
        End If

    End Sub

    Private Sub fClinicService_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fClinicService.FormClosed
        fClinicService = Nothing
    End Sub

    Private Sub ToolStripMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuparish.Click

    End Sub

    Private Sub mnuparishmembership_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuparishmembership.Click
        ldParishAdmission()
    End Sub
    Public Sub ldParishAdmission()
        If fParishAdmission Is Nothing Then
            fParishAdmission = New ChurchAdmissionFrm
show:
            With fParishAdmission
                .MdiParent = fMainForm
                .Show()
            End With
        Else
            If fParishAdmission.Visible = False Then
                GoTo show
            End If
            fParishAdmission.Focus()
        End If

    End Sub

    Private Sub fParishAdmission_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fParishAdmission.FormClosed
        fParishAdmission = Nothing
    End Sub

    Private Sub mnunercha_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnunercha.Click
        Dim frm As New NercharFrm
        With frm
            .Condition = "GrpSetOn In ('Vazhipadu')"
            .ShowDialog()
        End With
    End Sub

    Private Sub mnunerchabill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnunerchabill.Click
        ldNerchaSales()
    End Sub
    Public Sub ldNerchaSales(Optional ByVal keyid As Long = 0)
        If Not fchurchSales Is Nothing Then
            fchurchSales.Focus()
        Else
            fchurchSales = New ChurchSalesFrm
            With fchurchSales
                .MdiParent = fMainForm
                If keyid > 0 Then
                    .isModi = True
                End If
                .Show()
                If keyid > 0 Then

                    .CheckNLoad(keyid)
                End If
            End With
        End If
    End Sub

    Private Sub fchurchSales_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fchurchSales.FormClosed
        fchurchSales = Nothing
    End Sub

    Private Sub mnubilllist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnubilllist.Click
        ldViewPrint("TIS")
    End Sub

    Private Sub mnubranch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnubranch.Click
        Dim frm As New BranchFrm
        frm.ShowDialog()
    End Sub

    Private Sub mnurefreshMasters_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnurefreshMasters.Click
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
                refreshItemTable("Item Code", "")
        End Select
        If Not fwait Is Nothing Then
            fwait.Close()
            fwait = Nothing
        End If
    End Sub

    Private Sub btnadvanceRv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadvanceRv.Click
        LoadRVO()
    End Sub

    Private Sub mnuprotect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuprotect.Click
        Dim frm As New ProtectUntilFrm
        frm.ShowDialog()
        frm = Nothing
    End Sub

    Private Sub fStockAdjTO_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fStockAdjTO.FormClosed
        fStockAdjTO = Nothing
        removeMenuItem("TO")
    End Sub

    Private Sub fStockAdjTI_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fStockAdjTI.FormClosed
        fStockAdjTI = Nothing
        removeMenuItem("TI")
    End Sub

    Private Sub mnuTO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTO.Click
        LoadSTKADJTO()
    End Sub

    Private Sub mnuadvancesetoff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuadvancesetoff.Click
        loadAdvanceSetoff()
    End Sub

    Private Sub fadvancesetoff_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fadvancesetoff.FormClosed
        fadvancesetoff = Nothing
        removeMenuItem("ADS")
    End Sub
    Public Sub loadAdvanceSetoff()
        If fadvancesetoff Is Nothing Then
N:
            fadvancesetoff = New AdvanceSetoff
            With fadvancesetoff
                .MdiParent = Me
                .Show()
                With MenuStrip1.Items.Add("Advance Setoff")
                    .Tag = "ADS"
                    AddHandler .Click, AddressOf MenuStripClick
                End With
            End With
        Else
            fadvancesetoff.Focus()
        End If
    End Sub

    Private Sub mnusalesroute_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnusalesroute.Click
        Dim frm As New UnitMasterFrm
        With frm
            .StrCaption = "Sales Route"
            .ShowDialog()
        End With
    End Sub

    Private Sub mnudayclosing_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnudayclosing.Click
        loadDayClosing()
    End Sub
    Private Sub loadDayClosing()
        If frmDayclosing Is Nothing Then
            frmDayclosing = New DaycloseReportFrm
            frmDayclosing.MdiParent = fMainForm
            frmDayclosing.Show()
            With MenuStrip1.Items.Add("Day Closing")
                .Tag = "DC"
                AddHandler .Click, AddressOf MenuStripClick
            End With
        Else
            frmDayclosing.Focus()
        End If

    End Sub

    Private Sub mnuyearend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuyearend.Click
        YearEndFrm.ShowDialog()
    End Sub

    Private Sub mnurvcollection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnurvcollection.Click
        loadRVCollection()
    End Sub
    Private Sub loadRVCollection()
        If frvcollection Is Nothing Then
            frvcollection = New RVCollectionListFrm
            With frvcollection
                .MdiParent = fMainForm
                .Show()
            End With
            With MenuStrip1.Items.Add("Collection List")
                .Tag = "CL"
                AddHandler .Click, AddressOf MenuStripClick
            End With
        Else
            frvcollection.Focus()
        End If
    End Sub

    Private Sub frvcollection_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles frvcollection.FormClosed
        frvcollection = Nothing
        removeMenuItem("CL")
    End Sub

    Private Sub efIS_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles efIS.FormClosed
        efIS = Nothing
        removeMenuItem("IS")
    End Sub

    Private Sub mnufinefrm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnufinefrm.Click
        If ffineform Is Nothing Then
            ffineform = New FineFrm
            With ffineform
                .MdiParent = fMainForm
                .Show()
            End With
        Else
            ffineform.Focus()
        End If
    End Sub

    Private Sub ffineform_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles ffineform.FormClosed
        ffineform = Nothing
    End Sub




    Private Sub JobToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JobToolStripMenuItem.Click
        If Not fusedcar Is Nothing Then
            fusedcar.Focus()
        Else
            fusedcar = New UsedcarFrm
            With fusedcar
                .MdiParent = Me
                .Show()
            End With
        End If
    End Sub

    Private Sub mnustock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnustock.Click

    End Sub


    Private Sub SalesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalesToolStripMenuItem.Click
        LoadIS()
    End Sub

    Private Sub mnuLoanbook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLoanbook.Click
        If fLoanBook Is Nothing Then
            fLoanBook = New LoanBook
            With fLoanBook
                .MdiParent = fMainForm
                .Show()
            End With
        Else
            fLoanBook.Focus()
        End If
    End Sub
    Private Sub fLoanBook_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fLoanBook.FormClosed
        fLoanBook = Nothing
    End Sub

    Private Sub mnuonlinecollection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuonlinecollection.Click
        loadOnlineCollectionlist()
    End Sub
    Private Sub loadOnlineCollectionlist()
        If fonlinecollection Is Nothing Then
            fonlinecollection = New CollectionListFrm
            fonlinecollection.MdiParent = fMainForm
            fonlinecollection.Show()
            With MenuStrip1.Items.Add("Collection List")
                .Tag = "OCL"
                AddHandler .Click, AddressOf MenuStripClick
            End With
        Else
            fonlinecollection.Focus()
        End If
    End Sub

    Private Sub fonlinecollection_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fonlinecollection.FormClosed
        fonlinecollection = Nothing
        removeMenuItem("OCL")
    End Sub

    Private Sub mnucarrier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnucarrier.Click
        Dim frm As New UnitMasterFrm
        With frm
            .StrCaption = "Carrier"
            .ShowDialog()
        End With
    End Sub

    Private Sub fFruitIS_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fFruitIS.FormClosed
        fFruitIS = Nothing
        removeMenuItem("IS")
    End Sub

    Private Sub fPIFruit_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fPIFruit.FormClosed
        fPIFruit = Nothing
        removeMenuItem("IP")
    End Sub

    Private Sub fFruitSR_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fFruitSR.FormClosed
        fFruitSR = Nothing
        removeMenuItem("SR")
    End Sub

    Private Sub fFruitPR_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fFruitPR.FormClosed
        fFruitPR = Nothing
        removeMenuItem("PR")
    End Sub

    Private Sub mnucarrieroutstanding_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnucarrieroutstanding.Click
        loadCarrierOutstanding()
    End Sub
    Private Sub loadCarrierOutstanding()
        If fTrayOutstandingFruits Is Nothing Then
            fTrayOutstandingFruits = New FruitsTrayAnalysisFrm
            fTrayOutstandingFruits.MdiParent = fMainForm
            fTrayOutstandingFruits.Show()
            With MenuStrip1.Items.Add("Carrier Outstanding")
                .Tag = "CO"
                AddHandler .Click, AddressOf MenuStripClick
            End With
        Else
            fTrayOutstandingFruits.Focus()
        End If
    End Sub

    Private Sub fTrayOutstandingFruits_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fTrayOutstandingFruits.FormClosed
        fTrayOutstandingFruits = Nothing
        removeMenuItem("CO")
    End Sub

    Private Sub mnutunurmaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnutunurmaster.Click
        Dim frm As New TunurMaster
        frm.ShowDialog()
        frm = Nothing
    End Sub

    Private Sub floanrestructure_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles floanrestructure.FormClosed
        floanrestructure = Nothing
    End Sub

    Private Sub mnurestructure_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnurestructure.Click
        If floanrestructure Is Nothing Then
            floanrestructure = New RestructureFrm
            floanrestructure.MdiParent = fMainForm
            floanrestructure.Show()
        Else
            floanrestructure.Focus()
        End If
    End Sub

    Private Sub mnubranchreconcil_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnubranchreconcil.Click
        loadbranchReconciliation()
    End Sub
    Private Sub loadbranchReconciliation()
        If fbranchreconcil Is Nothing Then
            fbranchreconcil = New BranchReconciliation
            fbranchreconcil.MdiParent = fMainForm
            fbranchreconcil.Show()
            With MenuStrip1.Items.Add("Branch Reconciliation")
                .Tag = "RECB"
                AddHandler .Click, AddressOf MenuStripClick
            End With
        Else
            fbranchreconcil.Focus()
        End If
    End Sub

    Private Sub fbranchreconcil_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fbranchreconcil.FormClosed
        fbranchreconcil = Nothing
        removeMenuItem("RECB")
    End Sub

    Private Sub frmDayclosing_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles frmDayclosing.FormClosed
        frmDayclosing = Nothing
        removeMenuItem("DC")
    End Sub

    Private Sub HelpMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelpMenu.Click

    End Sub
    Private Sub MenuStrip_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles MenuStrip.ItemClicked

    End Sub
    Private Sub MenuStrip1_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked

    End Sub
    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub GSTR1ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GSTR1ToolStripMenuItem.Click
        FGSTR1 = New GSTR1
        With FGSTR1
            .MdiParent = fMainForm
            .Show()
        End With
    End Sub

    Private Sub FGSTR1_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles FGSTR1.FormClosed
        FGSTR1 = Nothing
    End Sub

    Private Sub Panel3_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)

    End Sub

    Private Sub Panel4_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)

    End Sub

    Private Sub mnustichingServiceMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnustichingServiceMaster.Click
        If fstichingservicemaster Is Nothing Then
            fstichingservicemaster = New StichingServiceMasterFrm
        End If
        'fstichingservicemaster.Top = Panel1.Top + Panel1.Height + ToolStrip1.Height + MenuStrip.Height + MenuStrip1.Height + 10
        With fstichingservicemaster
            .ShowDialog()
        End With


    End Sub

    Private Sub JobCardToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JobCardToolStripMenuItem.Click
        If fstichingjob Is Nothing Then fstichingjob = New StichingJobFrm
        With fstichingjob
            .MdiParent = fMainForm
            .Show()
        End With
    End Sub

    Private Sub fstichingjob_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fstichingjob.FormClosed
        fstichingjob = Nothing
    End Sub

    Private Sub fstichingservicemaster_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fstichingservicemaster.FormClosed
        fstichingservicemaster = Nothing
    End Sub

    Private Sub mnuageing_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuageing.Click
        If fageing Is Nothing Then
            fageing = New AgeingReportFrm
        End If
        With fageing
            .MdiParent = fMainForm
            .Show()
        End With
    End Sub

    Private Sub fageing_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fageing.FormClosed
        fageing = Nothing
    End Sub

    Private Sub mnumicroinvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnumicroinvoice.Click
        LoadMFIS()
    End Sub
    Public Sub LoadMFIS(Optional ByVal KeyId As Long = 0, Optional ByVal docid As Long = 0)
        If fMFIS Is Nothing Then
            fMFIS = New MFSalesInvoice
            If KeyId <> 0 Then
                fMFIS.isModi = True
            End If
            fMFIS.MdiParent = Me
            fMFIS.Show()
            With MenuStrip1.Items.Add("Micro Finance Invoice")
                .Tag = "MFIS"
                AddHandler .Click, AddressOf MenuStripClick
            End With
        Else
            fMFIS.Focus()
        End If
        If KeyId <> 0 Then
            fMFIS.isModi = True
            fMFIS.CheckNLoad(KeyId)
        End If
        If docid <> 0 Then
            fMFIS.ImportDOs(docid, True, True)
        End If

    End Sub

    Private Sub fMFIS_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fMFIS.FormClosed
        fMFIS = Nothing
        removeMenuItem("MFIS")
    End Sub
    Public Sub LoadMFRV(Optional ByVal KeyId As Long = 0, Optional ByVal custName As String = "")
        Me.Cursor = Cursors.WaitCursor
        If fMFRV Is Nothing Then
N:
            fMFRV = New MFCustomerReceipt
            If KeyId <> 0 Then
                fMFRV.isModi = True
            End If
            fMFRV.MdiParent = Me
            fMFRV.Show()
            With MenuStrip1.Items.Add("Micro Finance RV")
                .Tag = "RVM"
                AddHandler .Click, AddressOf MenuStripClick
            End With
        Else
            fMFRV.Focus()
        End If
        If custName <> "" Then
            fMFRV.chgbyprg = True
            fMFRV.txtSuppName.Text = custName
            fMFRV.chgbyprg = False
            If Not fMFRV.isModi Then fMFRV.loadFromJob()
        End If
        If KeyId <> 0 Then
            fMFRV.isModi = True
            fMFRV.editRecord(KeyId, custName)
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub fMFRV_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fMFRV.FormClosed
        fMFRV = Nothing
        removeMenuItem("RVM")
    End Sub

    Private Sub mnureceipt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnureceipt.Click
        LoadMFRV()
    End Sub

    Private Sub mnumfinvreport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnumfinvreport.Click
        If Not fMFIR Is Nothing Then fMFIR.Close() : fMFIR = Nothing
        fMFIR = New MFISReportFrm
        With MenuStrip1.Items.Add("MF Invoice List")
            .Tag = "fMFIR"
            AddHandler .Click, AddressOf MenuStripClick
        End With
        With fMFIR
            .MdiParent = fMainForm
            .Show()
        End With
    End Sub

    Private Sub fMFIR_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fMFIR.FormClosed
        fMFIR = Nothing
        removeMenuItem("fMFIR")
    End Sub

    Private Sub fRFCOST_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fRFCOST.FormClosed

        fRFCOST = Nothing
    End Sub

    Private Sub tsrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsrefresh.Click
        Exit Sub
        If Not fRFCOST Is Nothing Then
            If fRFCOST.Visible = False Then
                fRFCOST.Show()
            Else
                fRFCOST.Hide()
            End If

        End If
    End Sub

    Private Sub mnustudentadmission_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnustudentadmission.Click
        If fStudentAdmission Is Nothing Then
            fStudentAdmission = New StudentAdmissionFrm
            With fStudentAdmission
                .MdiParent = fMainForm
                .Show()
            End With
        Else
            With fStudentAdmission
                .Focus()
            End With
        End If

    End Sub

    Private Sub fStudentAdmission_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fStudentAdmission.FormClosed
        fStudentAdmission = Nothing
    End Sub

    Private Sub mnuteachermaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuteachermaster.Click
        Dim frm As New SchoolTeacherFrm
        frm.ShowDialog()
    End Sub

    Private Sub mnuschoolfeesmaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuschoolfeesmaster.Click
        Dim frm As New SchollFeesMasterFrm
        frm.ShowDialog()
    End Sub

    Private Sub mnuMonthlyfeesbooking_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMonthlyfeesbooking.Click
        If fmonthlyfees Is Nothing Then
            fmonthlyfees = New MonthlyFeesInvoiceFrm
            With fmonthlyfees
                .MdiParent = fMainForm
                .Show()
            End With
        Else
            With fmonthlyfees
                .Focus()
            End With
        End If

    End Sub

    Private Sub fmonthlyfees_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fmonthlyfees.FormClosed
        fmonthlyfees = Nothing
    End Sub

    Private Sub mnuweeklycollection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuweeklycollection.Click
        If fweeklycollection Is Nothing Then
            fweeklycollection = New WeeklyCollectionFrm
            With fweeklycollection
                .MdiParent = fMainForm
                .Show()
            End With
        Else
            With fweeklycollection
                .Focus()
            End With
        End If

    End Sub

    Private Sub fweeklycollection_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fweeklycollection.FormClosed
        fweeklycollection = Nothing
    End Sub


    Private Sub mnudebit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnudebit.Click

        If fdebitNote Is Nothing Then
            fdebitNote = New Debitnote
            With fdebitNote
                .MdiParent = fMainForm
                .Show()
            End With
        Else
            With fdebitNote
                .Focus()
            End With
        End If
    End Sub

    Private Sub fdebitNote_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fdebitNote.FormClosed
        fdebitNote = Nothing
    End Sub

    Private Sub fyearlyfees_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fyearlyfees.FormClosed
        fyearlyfees = Nothing
    End Sub

    Private Sub mnuyearlyfeesbooking_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuyearlyfeesbooking.Click
        If fyearlyfees Is Nothing Then
            fyearlyfees = New FeesInvoiceFrm
            With fyearlyfees
                .MdiParent = fMainForm
                .Show()
            End With
        Else
            With fyearlyfees
                .Focus()
            End With
        End If
    End Sub

    Private Sub mnustudentapplication_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnustudentapplication.Click
        loadStudentAdmissionDXB()
    End Sub
    Public Sub loadStudentAdmissionDXB(Optional ByVal keid As Long = 0)
        If fstudentappication Is Nothing Then
            fstudentappication = New StudentAdmissionDXBFrm
            With fstudentappication
                .MdiParent = fMainForm
                .Show()
                If keid > 0 Then
                    .txtcode.Tag = keid
                    .loadrec()
                End If
            End With
            With MenuStrip1.Items.Add("Admission")
                .Tag = "STADM"
                AddHandler .Click, AddressOf MenuStripClick
            End With
        Else
            With fstudentappication
                .Focus()
            End With
        End If
    End Sub

    Private Sub mnucoursemaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnucoursemaster.Click
        Dim frm As New CourseItem
        frm.ShowDialog()
        frm = Nothing
    End Sub

    Private Sub ffeesSales_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles ffeesSales.FormClosed
        ffeesSales = Nothing
        removeMenuItem("ISF")
    End Sub

    Private Sub fstudentappication_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fstudentappication.FormClosed
        fstudentappication = Nothing
        removeMenuItem("STADM")
    End Sub

    Private Sub mnucoursesummary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnucoursesummary.Click
        loadCSummary()
    End Sub
    Private Sub loadCSummary()
        If fcourseSummary Is Nothing Then
            fcourseSummary = New SchoolDXBDashBoardFrm
            fcourseSummary.MdiParent = fMainForm
            fcourseSummary.Show()
            With MenuStrip1.Items.Add("Course Summary")
                .Tag = "CSUM"
                AddHandler .Click, AddressOf MenuStripClick
            End With
        Else
            fcourseSummary.Focus()
        End If
    End Sub

    Private Sub fcourseSummary_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fcourseSummary.FormClosed
        fcourseSummary = Nothing
        removeMenuItem("CSUM")
    End Sub

    Private Sub mnututor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnututor.Click
        Dim frm As New SchoolTeacherFrm
        frm.ShowDialog()
    End Sub

    Private Sub mnutreatmentmaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnutreatmentmaster.Click
        Dim frm As New TreatmentItem
        frm.ShowDialog()
        frm = Nothing
    End Sub

    Private Sub fIPappointment_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fIPappointment.FormClosed
        fIPappointment = Nothing
        removeMenuItem("IPA")
    End Sub

    Private Sub mnuipappointment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuipappointment.Click
        loadIPAppointment()
    End Sub
    Public Sub loadIPAppointment(Optional ByVal keyid As Long = 0)
        If fIPappointment Is Nothing Then
            fIPappointment = New ClinicAppointmentFrm
            With fIPappointment
                .MdiParent = fMainForm
                .visittype = "IP"
                .Show()
                If keyid > 0 Then
                    .numVchrNo.Tag = keyid
                    .returnClinicVisit()
                End If
                With MenuStrip1.Items.Add("IP Appointment")
                    .Tag = "IPA"
                    AddHandler .Click, AddressOf MenuStripClick
                End With
            End With
        End If
    End Sub

    Private Sub mnueventlog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnueventlog.Click
        If feventlog Is Nothing Then
            feventlog = New eventLogFrm
            feventlog.MdiParent = fMainForm
            feventlog.Show()
        Else
            feventlog.Focus()
        End If
    End Sub
    Private Sub feventlog_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles feventlog.FormClosed
        feventlog = Nothing
    End Sub
    Public Sub loadAutoUpdate()
        If fautoupdateToserver Is Nothing Then
N:
            fautoupdateToserver = New AutoUpdateToServerFrm
            With fautoupdateToserver
                .Show()
                .Hide()
            End With
        End If
    End Sub

    Private Sub fautoupdateToserver_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fautoupdateToserver.FormClosed
        fautoupdateToserver = Nothing
    End Sub

    Private Sub ShowUpdateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowUpdateToolStripMenuItem.Click
        If fautoupdateToserver Is Nothing Then
N:
            fautoupdateToserver = New AutoUpdateToServerFrm

        End If
        With fautoupdateToserver
            If .Visible Then
                .Hide()
            Else
                .Show()
            End If

        End With
    End Sub

    Private Sub fpoolmembership_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fpoolmembership.FormClosed
        fpoolmembership = Nothing
    End Sub

    Private Sub mnumembershipattendance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnumembershipattendance.Click
        If fmembershipattendance Is Nothing Then
            fmembershipattendance = New MembershipAttendanceFrm
            fmembershipattendance.MdiParent = fMainForm
            fmembershipattendance.Show()
            With MenuStrip1.Items.Add("Attendance")
                .Tag = "MSA"
                AddHandler .Click, AddressOf MenuStripClick
            End With
        Else
            fmembershipattendance.Focus()
        End If
    End Sub

    Private Sub fmembershipattendance_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fmembershipattendance.FormClosed
        fmembershipattendance = Nothing
        removeMenuItem("MSA")
    End Sub

    Private Sub fgymmembership_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fgymmembership.FormClosed
        fgymmembership = Nothing
    End Sub

    Private Sub fCJOB_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fCJOB.FormClosed

    End Sub

    Private Sub fJOBLstDelivey_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fJOBLstDelivey.FormClosed
        fJOBLstDelivey = Nothing
        removeMenuItem("JBD")
    End Sub

    Private Sub mnuamclist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuamclist.Click
        If famccustomerlist Is Nothing Then
            famccustomerlist = New AmcList
            famccustomerlist.MdiParent = fMainForm
            famccustomerlist.Show()
            With MenuStrip1.Items.Add("AMC Customer List")
                .Tag = "AMC"
                AddHandler .Click, AddressOf MenuStripClick
            End With
        Else
            famccustomerlist.Focus()
        End If


    End Sub

    Private Sub famccustomerlist_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles famccustomerlist.FormClosed
        famccustomerlist = Nothing
        removeMenuItem("AMC")
    End Sub


    'Private Sub mnuroutesales_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuroutesales.Click
    '    If froutesales Is Nothing Then
    '        froutesales = New MilksalesFrm
    '        froutesales.MdiParent = fMainForm
    '        froutesales.Show()
    '        With MenuStrip1.Items.Add("Route Sales")
    '            .Tag = "RS"
    '            AddHandler .Click, AddressOf MenuStripClick
    '        End With
    '    Else
    '        froutesales.Focus()
    '    End If
    'End Sub

    Private Sub froutesales_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles froutesales.FormClosed
        froutesales = Nothing
        removeMenuItem("RS")
    End Sub


    Private Sub Bulksales_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bulksales.Click
        If froutesales Is Nothing Then
            froutesales = New MilksalesFrm
            froutesales.MdiParent = fMainForm
            froutesales.Show()
            With MenuStrip1.Items.Add("Route Sales")
                .Tag = "RS"
                AddHandler .Click, AddressOf MenuStripClick
            End With
        Else
            froutesales.Focus()
        End If
    End Sub


    Private Sub mnuroutesales_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuroutesales.Click

    End Sub

    Private Sub mnudistributedsalesreport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnudistributedsalesreport.Click
        fMainForm.ldViewPrint("IS", False, True)
    End Sub

    Private Sub fRefreshCostManual_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fRefreshCostManual.FormClosed
        fRefreshCostManual = Nothing
        loadRefreshcost()
    End Sub
End Class
