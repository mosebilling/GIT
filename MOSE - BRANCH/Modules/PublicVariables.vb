Module PublicVariables
#Region "Public Variables"
    Public CostMethod As Integer
    Public numFormat As String
    Public NoOfDecimal As Integer
    Public dtEmpty As String
    Public APath As String
    Public password As String
    Public ProtectUntil As Date
    Public DontWarnAny As Boolean
    Public EnaCostAcc As Boolean
    Public dtPhotopath As String
    Public vrVersion As String
    Public stateCode As String
    Public setTaxAsIncomeExpense As Boolean
    Public firstDateFromToday As Integer

    Public DateFrom As Date
    Public DateTo As Date
    Public shortDtFmt As String
    Public DtFormat As String
    Public dtMask As String
    Public DtFormatTime As String
    Public SerialAlertDays As Integer
    Public withNonTaxBill As Boolean
    Public bartenderpath As String
    Public EntrygridFontSize As Double
    Public ISNextline As Integer
    Public roundoffGtrThn50 As Integer
    Public roundoffLessThn50 As Integer

    Public userType As Boolean
    Public CurrentUser As String
    Public UsrBr As String
    Public Usrlocation As String
    Public fMainForm As New MainFrm
    Public DPath As String
    Public MyServer As String
    Public MyDatabase As String
    Public MACHINENAME As String
    Public defaultState As String
    Public dtProductBatch As DataTable
    Public templeVazhipaduCodeSearch As Boolean
    Public templeStarCodeSearch As Boolean
    Public dtrights As DataTable
    Public dtSysPropVal As DataTable
    Public dtSysRestVal As DataTable
    Public userDiscount As Double
    Public searchByCodeInInventory As Boolean
    Public searchfulltext As Boolean
    Public BranchId As Integer
    Public DBranchId As Integer
    Public cessenddate As Date

    Public ftpurl As String
    Public ftpusername As String
    Public ftppassword As String

#End Region
#Region "Public datatable for formload"
    Public dtwarrenty As DataTable
    Public dtsalesman As DataTable
    Public CaptionTb As DataTable
    Public dtcurrentyTb As DataTable
    Public dtlocationTb As DataTable
    Public PreFixTb As DataTable
    Public dtInvNos As DataTable
    Public dtAcc As DataTable
    Public dtItmTable As DataTable
#End Region
#Region "System Parameter Variables"
    Public connectionstring As String
    Public enableRealtimeCosting As Boolean
    Public enableServiceJob As Boolean
    Public enableJobMaster As Boolean
    Public enableAccounts As Boolean
    Public enableInventory As Boolean
    Public enableContractJob As Boolean
    Public enableDocuments As Boolean
    Public enableSerialnumber As Boolean
    Public ShowTaxOnInventory As Boolean
    Public EnableGST As Boolean
    Public EnableWarranty As Boolean
    Public enableDuplicateBill As Boolean
    'Public ShowSerialnumberAlert As Boolean
    Public AllowUnitDiscountEntryOnInventory As Boolean
    Public enableItemwiseSalesman As Boolean
    Public enableFuleBankInvoice As Boolean
    Public enableMultipleDebitInInvoice As Boolean
    Public EnableBarcode As Boolean
    Public enableTemple As Boolean
    Public enableProduction As Boolean
    Public enableWebIntegration As Boolean
    Public enableNextlineonItemcode As Boolean
    Public enablePrintOnSave As Boolean
    Public enableWorkshop As Boolean
    Public enableCarWash As Boolean
    Public webserver As String
    Public webusername As String
    Public webpassword As String
    Public webdbname As String
    Public webIntegrationid As Integer
    Public enableRestuarent As Boolean
    Public enableLodge As Boolean
    Public enableWoodSale As Boolean
    Public enableUserCodeOnPosLogin As Boolean
    Public enablePOS As Boolean
    Public enableNegativeQtyAlert As Boolean
    Public enablecess As Boolean
    Public enableBatchwiseTr As Boolean
    Public enableDeliverywiseOutstanding As Boolean
    Public enableVazhipaduSalesWithMutipleNamesAndItems As Boolean
    Public enableSerialnumberWithoutPurchase As Boolean
    Public enableSMS As Boolean
    Public enablePayroll As Boolean
    Public enableAutoRoundOff As Boolean
    Public enableSingleUserKOT As Boolean
    Public enableSalesmancompulsory As Boolean
    Public enableCostAccounting As Boolean
    Public enableCreditPrice As Boolean
    Public enableMRPInStockIn As Boolean
    Public enableSP1InStockIn As Boolean
    Public enableSP2InStockIn As Boolean
    Public enableSP3InStockIn As Boolean
    Public enableGCC As Boolean
    Public enableMRPinDocument As Boolean
    Public enableTaxinDocument As Boolean
    Public enablefetchLastPrice As Boolean
    Public enableFloodCess As Boolean
    Public enableItemAutoPopulate As Boolean
    Public calcluatetaxFrompriceInv As Boolean
    Public calcluatetaxFrompricedoc As Boolean
    Public searchStartOnly As Boolean
    'Public enableWSPriceAsDefault As Boolean
    Public disableShowAlert As Boolean
    Public enableFOCQty As Boolean
    Public enablePhoneNumberMandatory As Boolean
    Public enableMultipleDebitAutoPopulate As Boolean
    Public enableAdvanceEntryInMultipleDebit As Boolean
    Public enableMultipleDebitAsCreditCollection As Boolean
    Public priceInSales As Integer
    Public enableMembership As Boolean
    Public enableB2BAsDefault As Boolean
    Public enableInvoiceTotalFromHistory As Boolean
    Public enableClinic As Boolean
    Public enableFocusOnQTYinPOS As Boolean
    Public enableMRPinSales As Boolean
    Public enableExpiryDateInPOS As Boolean
    Public LinkB2BWithWSPrice As Boolean
    Public enableChurchModule As Boolean
    Public enablelaundry As Boolean
    Public disablePriceEditInPos As Boolean
    Public Dloc As String
    Public Dbranch As String
    Public JLoc As String
    Public enableBranch As Boolean
    Public stockEffectInDeliveryNote As Boolean
    Public disableEditProdectDescription As Boolean
    Public disableMRPFromProductSearch As Boolean
    Public disableWSFromProductSearch As Boolean
    Public setSalespriceFromMRPinPruchase As Boolean
    Public duplicatebillinPOS As Boolean
    Public calcluatetaxFromSpriceInIP As Boolean
    Public EnableAlertBelowcost As Boolean
    Public enablePrintOnRVSave As Boolean
    Public enableAdjustDiscountOnTaxTotal As Boolean
    Public EnableFinancialSales As Boolean
    Public EnableUsedCar As Boolean
    Public EnableFruitsSales As Boolean
    Public DisbleRepeateRv As Boolean
    Public enableuserwisetransactionlist As Boolean
    Public enableTailoring As Boolean
    Public enableMicroFinace As Boolean
    Public enableMultiplePointsOnLineItem As Boolean
    Public enableInstallmentInRV As Boolean
    Public enableRVApproval As Boolean
    Public enableschoolmanagement As Boolean
    Public enableprofitanalysiswithreturn As Boolean
    Public enableChooseInstallmentinRV As Boolean
    Public enableCoursemangementDXB As Boolean
    Public enableSwimmingPool As Boolean
    Public enableGYM As Boolean
    Public enableRouteBulkSale As Boolean
#End Region
    Public Sub setConstants()
        dtEmpty = "  /  /    "
        shortDtFmt = "dd/MM/yyyy"
        DtFormat = "dd/MM/yyyy"
        EnaCostAcc = True
        dtMask = ""
        vrVersion = "20.0.5"
        'AccTrCmn : moduleId=0 [Job Invoice],moduleId=1 [Vazhipadu Sales]
    End Sub
End Module
