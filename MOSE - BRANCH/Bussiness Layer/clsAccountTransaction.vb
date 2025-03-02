Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DataAccessLayer
Imports System.Data
Public Class clsAccountTransaction
#Region "MasterTable Properties"
    Private _JVTypeNo As Integer
    Private _JVType As String
    Private _JVNum As Long
    Private _PreFix As String
    Private _JVDate As DateTime
    Private _UserId As String
    Private _CrtDtTm As DateTime
    Private _TypeNo As Integer
    Private _LinkNo As Long
    Private _VrDescr As String
    Private _LnkBkgNo As Integer
    Private _LnkContNo As Integer
    Private _ContractTran As Boolean
    Private _MchName As String
    Private _SMan As String
    Private _PDCLinkNo As Long
    Private _IsModi As Integer
    Private _OthInf As String

    Public Property JVTypeNo() As Integer
        Get
            Return _JVTypeNo
        End Get
        Set(ByVal value As Integer)
            _JVTypeNo = value
        End Set
    End Property
    Public Property JVType() As String
        Get
            Return _JVType
        End Get
        Set(ByVal value As String)
            _JVType = value
        End Set
    End Property
    Public Property OthInf() As String
        Get
            Return _OthInf
        End Get
        Set(ByVal value As String)
            _OthInf = value
        End Set
    End Property
    Public Property JVNum() As Long
        Get
            Return _JVNum
        End Get
        Set(ByVal value As Long)
            _JVNum = value
        End Set
    End Property
    Public Property PreFix() As String
        Get
            Return _PreFix
        End Get
        Set(ByVal value As String)
            _PreFix = value
        End Set
    End Property
    Public Property JVDate() As DateTime
        Get
            Return _JVDate
        End Get
        Set(ByVal value As DateTime)
            _JVDate = value
        End Set
    End Property
    Public Property UserId() As String
        Get
            Return _UserId
        End Get
        Set(ByVal value As String)
            _UserId = value
        End Set
    End Property
    Public Property CrtDtTm() As DateTime
        Get
            Return _CrtDtTm
        End Get
        Set(ByVal value As DateTime)
            _CrtDtTm = value
        End Set
    End Property
    Public Property TypeNo() As Integer
        Get
            Return _TypeNo
        End Get
        Set(ByVal value As Integer)
            _TypeNo = value
        End Set
    End Property
    Public Property LinkNo() As Long
        Get
            Return _LinkNo
        End Get
        Set(ByVal value As Long)
            _LinkNo = value
        End Set
    End Property
    Public Property VrDescr() As String
        Get
            Return _VrDescr
        End Get
        Set(ByVal value As String)
            _VrDescr = value
        End Set
    End Property
    Public Property LnkBkgNo() As Integer
        Get
            Return _LnkBkgNo
        End Get
        Set(ByVal value As Integer)
            _LnkBkgNo = value
        End Set
    End Property
    Public Property LnkContNo() As Integer
        Get
            Return _LnkContNo
        End Get
        Set(ByVal value As Integer)
            _LnkContNo = value
        End Set
    End Property
    Public Property ContractTran() As Boolean
        Get
            Return _ContractTran
        End Get
        Set(ByVal value As Boolean)
            _ContractTran = value
        End Set
    End Property
    Public Property MchName() As String
        Get
            Return _MchName
        End Get
        Set(ByVal value As String)
            _MchName = value
        End Set
    End Property
    Public Property IsModi() As Integer
        Get
            Return _IsModi
        End Get
        Set(ByVal value As Integer)
            _IsModi = value
        End Set
    End Property
    Public Property SMan() As String
        Get
            Return _SMan
        End Get
        Set(ByVal value As String)
            _SMan = value
        End Set
    End Property
    Public Property PDCLinkNo() As Long
        Get
            Return _PDCLinkNo
        End Get
        Set(ByVal value As Long)
            _PDCLinkNo = value
        End Set
    End Property
    Private _taxablevalue As Double
    Public Property taxablevalue() As Double
        Get
            Return _taxablevalue
        End Get
        Set(ByVal value As Double)
            _taxablevalue = value
        End Set
    End Property
    Private _taxvalue As Double
    Public Property taxvalue() As Double
        Get
            Return _taxvalue
        End Get
        Set(ByVal value As Double)
            _taxvalue = value
        End Set
    End Property
    Private _collectedOrDeliveredBy As String
    Public Property collectedOrDeliveredBy() As String
        Get
            Return _collectedOrDeliveredBy
        End Get
        Set(ByVal value As String)
            _collectedOrDeliveredBy = value
        End Set
    End Property
    Private _isdeleteTr As Integer
    Public Property isdeleteTr() As Integer
        Get
            Return _isdeleteTr
        End Get
        Set(ByVal value As Integer)
            _isdeleteTr = value
        End Set
    End Property
    Private _pdcid As Long
    Public Property pdcid() As Long
        Get
            Return _pdcid
        End Get
        Set(ByVal value As Long)
            _pdcid = value
        End Set
    End Property
    Private _DailyRVNo As Integer
    Public Property DailyRVNo() As Integer
        Get
            Return _DailyRVNo
        End Get
        Set(ByVal value As Integer)
            _DailyRVNo = value
        End Set
    End Property
#End Region
#Region "DetailsTable Properties"
    Private _trLinkNo As Integer
    Private _AccountNo As Integer
    Private _DueDate As DateTime
    Private _Reference As String
    Private _EntryRef As String
    Private _DealAmt As Double
    Private _CurrencyCode As String
    Private _CurrRate As Single
    Private _FCAmt As Double
    Private _JobCode As String
    Private _LPONo As String
    Private _OthCost As Int16
    Private _TrInf As Int16
    Private _TermsId As String
    Private _CustAcc As Integer
    Private _AccWithRef As String
    Private _JobStr As String
    Private _DocDate As DateTime
    Private _SuppInvDate As DateTime
    Private _VesselId As String
    Private _isLinkNo As Boolean
    Private _Brid As Boolean
    Private _ChqNo As String
    Private _ChqDate As DateTime
    Private _UnqNo As Long
    Private _BankCode As String
    Private _PDCAcc As Integer

    Public Property trLinkNo() As Integer
        Get
            Return _trLinkNo
        End Get
        Set(ByVal value As Integer)
            _trLinkNo = value
        End Set
    End Property
    
    Public Property isLinkNo() As Boolean
        Get
            Return _isLinkNo
        End Get
        Set(ByVal value As Boolean)
            _isLinkNo = value
        End Set
    End Property
    Public Property AccountNo() As Integer
        Get
            Return _AccountNo
        End Get
        Set(ByVal value As Integer)
            _AccountNo = value
        End Set
    End Property
    Public Property DueDate() As DateTime
        Get
            Return _DueDate
        End Get
        Set(ByVal value As DateTime)
            _DueDate = value
        End Set
    End Property
    Public Property Reference() As String
        Get
            Return _Reference
        End Get
        Set(ByVal value As String)
            _Reference = value
        End Set
    End Property
    Public Property EntryRef() As String
        Get
            Return _EntryRef
        End Get
        Set(ByVal value As String)
            _EntryRef = value
        End Set
    End Property
    Public Property DealAmt() As Double
        Get
            Return _DealAmt
        End Get
        Set(ByVal value As Double)
            _DealAmt = value
        End Set
    End Property
    Public Property CurrencyCode() As String
        Get
            Return _CurrencyCode
        End Get
        Set(ByVal value As String)
            _CurrencyCode = value
        End Set
    End Property
    Public Property CurrRate() As Single
        Get
            Return _CurrRate
        End Get
        Set(ByVal value As Single)
            _CurrRate = value
        End Set
    End Property
    Public Property JobCode() As String
        Get
            Return _JobCode
        End Get
        Set(ByVal value As String)
            _JobCode = value
        End Set
    End Property
    Public Property LPONo() As String
        Get
            Return _LPONo
        End Get
        Set(ByVal value As String)
            _LPONo = value
        End Set
    End Property
    Public Property OthCost() As Int16
        Get
            Return _OthCost
        End Get
        Set(ByVal value As Int16)
            _OthCost = value
        End Set
    End Property
    Public Property TrInf() As Int16
        Get
            Return _TrInf
        End Get
        Set(ByVal value As Int16)
            _TrInf = value
        End Set
    End Property
    Public Property TermsId() As String
        Get
            Return _TermsId
        End Get
        Set(ByVal value As String)
            _TermsId = value
        End Set
    End Property
    Public Property CustAcc() As Integer
        Get
            Return _CustAcc
        End Get
        Set(ByVal value As Integer)
            _CustAcc = value
        End Set
    End Property
    Public Property AccWithRef() As String
        Get
            Return _AccWithRef
        End Get
        Set(ByVal value As String)
            _AccWithRef = value
        End Set
    End Property
    Public Property JobStr() As String
        Get
            Return _JobStr
        End Get
        Set(ByVal value As String)
            _JobStr = value
        End Set
    End Property
    Public Property DocDate() As DateTime
        Get
            Return _DocDate
        End Get
        Set(ByVal value As DateTime)
            _DocDate = value
        End Set
    End Property
    Public Property SuppInvDate() As DateTime
        Get
            Return _SuppInvDate
        End Get
        Set(ByVal value As DateTime)
            _SuppInvDate = value
        End Set
    End Property
    Public Property FCAmt() As Double
        Get
            Return _FCAmt
        End Get
        Set(ByVal value As Double)
            _FCAmt = value
        End Set
    End Property
    Public Property VesselId() As String
        Get
            Return _VesselId
        End Get
        Set(ByVal value As String)
            _VesselId = value
        End Set
    End Property
    Public Property Brid() As String
        Get
            Return _Brid
        End Get
        Set(ByVal value As String)
            _Brid = value
        End Set
    End Property
    Public Property ChqNo() As String
        Get
            Return _ChqNo
        End Get
        Set(ByVal value As String)
            _ChqNo = value
        End Set
    End Property
    Public Property ChqDate() As String
        Get
            Return _ChqDate
        End Get
        Set(ByVal value As String)
            _ChqDate = value
        End Set
    End Property
    Public Property UnqNo() As Long
        Get
            Return _UnqNo
        End Get
        Set(ByVal value As Long)
            _UnqNo = value
        End Set
    End Property
    Public Property BankCode() As String
        Get
            Return _BankCode
        End Get
        Set(ByVal value As String)
            _BankCode = value
        End Set
    End Property
    Public Property PDCAcc() As Integer
        Get
            Return _PDCAcc
        End Get
        Set(ByVal value As Integer)
            _PDCAcc = value
        End Set
    End Property
    Private _vatcode As String
    Public Property vatcode() As String
        Get
            Return _vatcode
        End Get
        Set(ByVal value As String)
            _vatcode = value
        End Set
    End Property
    Private _isvatEntry As Integer
    Public Property isvatEntry() As Integer
        Get
            Return _isvatEntry
        End Get
        Set(ByVal value As Integer)
            _isvatEntry = value
        End Set
    End Property
    Private _payrollpaymentid As Long
    Public Property payrollpaymentid() As Long
        Get
            Return _payrollpaymentid
        End Get
        Set(ByVal value As Long)
            _payrollpaymentid = value
        End Set
    End Property
#End Region
#Region "Search Parameters"
    Private _ptype As Long
    Private _DateFrom As Date
    Private _DateTo As Date
    Public Property ptype() As Integer
        Get
            Return _ptype
        End Get
        Set(ByVal value As Integer)
            _ptype = value
        End Set
    End Property
    Public Property DateFrom() As Date
        Get
            Return _DateFrom
        End Get
        Set(ByVal value As Date)
            _DateFrom = value
        End Set
    End Property
    Public Property DateTo() As Date
        Get
            Return _DateTo
        End Get
        Set(ByVal value As Date)
            _DateTo = value
        End Set
    End Property
#End Region
    Private _cmnDlink As New cl_CmnDataLink()
    Private _cmnMthds As New CmnMethods()
    Private _ldlayer As New Dlayer
    Public Sub saveItmAccTrTbCostingBulk(ByVal dt As DataTable)
        _ldlayer.saveItmAccTrTbCostingBulk(dt)
    End Sub
    Public Function SaveAccTrWithDt(ByVal dt As DataTable, Optional ByVal ismicrofinanceRv As Integer = 0, Optional ByVal approved As Integer = 0, Optional ByVal courseInstallmentid As Integer = 0) As String
        Try
            'Dim branch As String = IIf(Trim(UsrBr & "") = "", Trim(Dbranch & ""), Trim(UsrBr & ""))
            Dim param As String(,) = New String(27, 2) {{"@LnkNo", LinkNo, ""}, _
                                                        {"@JVTypeNo", JVTypeNo, ""}, _
                                                        {"@JVType", JVType, ""}, _
                                                        {"@JVNum", JVNum, ""}, _
                                                        {"@PreFix", PreFix, ""}, _
                                                        {"@JVDate", JVDate, "d"}, _
                                                         {"@UserId", UserId, ""}, _
                                                         {"@CrtDtTm", CrtDtTm, "d"}, _
                                                         {"@TypeNo", TypeNo, ""}, _
                                                         {"@LnkBkgNo", LnkBkgNo, ""}, _
                                                         {"@LnkContNo", LnkContNo, ""}, _
                                                         {"@ContractTran", ContractTran, ""}, _
                                                         {"@MchName", Trim(MchName & ""), ""}, _
                                                         {"@SMan", Trim(SMan & ""), ""}, _
                                                         {"@PDCLinkNo", PDCLinkNo, ""}, _
                                                         {"@IsModi", IsModi, ""}, _
                                                         {"@OthInf", Trim(OthInf & ""), ""}, _
                                                         {"@VrDescr", Trim(VrDescr & ""), ""}, _
                                                         {"@taxablevalue", CDbl(taxablevalue & ""), ""}, _
                                                         {"@taxvalue", CDbl(taxvalue), ""}, _
                                                         {"@CmnBrId", UsrBr, ""}, _
                                                         {"@collectedOrDeliveredBy", Trim(collectedOrDeliveredBy & ""), ""}, _
                                                         {"@isdeleteTr", Val(isdeleteTr & ""), ""}, _
                                                         {"@pdcid", Val(pdcid & ""), ""}, _
                                                         {"@DailyRVNo", Val(DailyRVNo & ""), ""}, _
                                                         {"@ismicrofinanceRv", ismicrofinanceRv, ""}, _
                                                         {"@approved", approved, ""}, _
                                                         {"@courseInstallmentid", courseInstallmentid, ""}}
            Dim _id As String = ""
            If isLinkNo Then
                If IsModi Then
                    param(15, 1) = 2
                Else
                    param(15, 1) = 0
                End If
            Else
                If IsModi Then
                    param(15, 1) = 1
                Else
                    param(15, 1) = 0
                End If
            End If
            'Dim recordNo As Object
            'recordNo = _cmnMthds._ExecuteScalar(param, "AccTrCmn_SAVEorEdit", 21)
            'recordNo = _ldlayer.ExecuteScalar(param, "AccTrCmn_SAVEorEdit", 21)
            _id = _ldlayer.saveItmAccTrTbBulk(dt, param, 28)
            '_id = recordNo.ToString()
            'If _id = "" Then
            '    _id = LinkNo
            'End If

            Return _id
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function SaveAccTrCmn() As String
        Try
            Dim param As String(,) = New String(21, 2) {{"@LnkNo", LinkNo, ""}, _
                                                        {"@JVTypeNo", JVTypeNo, ""}, _
                                                        {"@JVType", JVType, ""}, _
                                                        {"@JVNum", JVNum, ""}, _
                                                        {"@PreFix", PreFix, ""}, _
                                                        {"@JVDate", JVDate, "d"}, _
                                                         {"@UserId", UserId, ""}, _
                                                         {"@TypeNo", TypeNo, ""}, _
                                                         {"@LnkBkgNo", LnkBkgNo, ""}, _
                                                         {"@LnkContNo", LnkContNo, ""}, _
                                                         {"@ContractTran", ContractTran, ""}, _
                                                         {"@MchName", Trim(MchName & ""), ""}, _
                                                         {"@SMan", Trim(SMan & ""), ""}, _
                                                         {"@PDCLinkNo", PDCLinkNo, ""}, _
                                                         {"@CrtDtTm", CrtDtTm, "d"}, _
                                                         {"@IsModi", IsModi, ""}, _
                                                         {"@OthInf", Trim(OthInf & ""), ""}, _
                                                         {"@VrDescr", Trim(VrDescr & ""), ""}, _
                                                         {"@taxablevalue", CDbl(taxablevalue & ""), ""}, _
                                                         {"@taxvalue", CDbl(taxvalue), ""}, _
                                                         {"@CmnBrId", Trim(UsrBr & ""), ""}, _
                                                         {"@financecollectionid", 0, ""}}
            Dim _id As String
            If isLinkNo Then
                If IsModi Then
                    param(15, 1) = 2
                Else
                    param(15, 1) = 0
                End If
            Else
                If IsModi Then
                    param(15, 1) = 1
                Else
                    param(15, 1) = 0
                End If
            End If
            Dim recordNo As Object
            recordNo = _cmnMthds._ExecuteScalar(param, "acctrcmn_saveoreditFromJob", 22)
            'recordNo = _ldlayer.ExecuteScalar(param, "AccTrCmn_SAVEorEdit", 21)
            '_ldlayer.saveItmAccTrTbBulk(dt, param, 21)
            _id = recordNo.ToString()
            If _id = "" Then
                _id = LinkNo
            End If
            Return _id
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub saveAccTrans()
        Dim param As String(,) = New String(26, 2) {{"@LinkNo", trLinkNo, ""}, _
                                                    {"@AccountNo", AccountNo, ""}, _
                                                    {"@DueDate", DueDate, "d"}, _
                                                    {"@Reference", Reference, ""}, _
                                                    {"@EntryRef", EntryRef, ""}, _
                                                    {"@DealAmt", DealAmt, ""}, _
                                                    {"@CurrencyCode", CurrencyCode, ""}, _
                                                    {"@CurrRate", CurrRate, ""}, _
                                                    {"@FCAmt", FCAmt, ""}, _
                                                    {"@JobCode", Trim(JobCode & ""), ""}, _
                                                    {"@LPONo", Convert.ToString(LPONo) & "", ""}, _
                                                    {"@OthCost", OthCost, ""}, _
                                                    {"@TrInf", TrInf, ""}, _
                                                    {"@TermsId", Convert.ToString(TermsId) & "", ""}, _
                                                    {"@CustAcc", Convert.ToString(CustAcc) & "", ""}, _
                                                    {"@AccWithRef", Convert.ToString(AccWithRef) & "", ""}, _
                                                    {"@SuppInvDate", SuppInvDate, "d"}, _
                                                    {"@VesselId", Convert.ToString(VesselId) & "", ""}, _
                                                    {"@BrId", Convert.ToString(Brid) & "", ""}, _
                                                    {"@ChqNo", Convert.ToString(ChqNo) & "", ""}, _
                                                    {"@ChqDate", ChqDate, "d"}, _
                                                    {"@UnqNo", Val(UnqNo & ""), ""}, _
                                                    {"@BankCode", Trim(BankCode & ""), ""}, _
                                                    {"@PDCAcc", Val(PDCAcc & ""), ""}, _
                                                    {"@vatcode", Trim(vatcode & ""), ""}, _
                                                    {"@isvatEntry", Val(isvatEntry & ""), ""}, _
                                                    {"@setoffcount", 0, ""}}
        '_cmnMthds._ExecuteNonQuery(param, "AccTrDet_SAVE", 26)
        _ldlayer.ExecuteNonQuery(param, "AccTrDet_SAVE", 27)
    End Sub
    Public Function returnAccountDetailsWithJob() As DataSet
        Dim param As String(,) = New String(0, 2) {{"@AccountNo", AccountNo, ""}}
        Return _cmnMthds._ldDataset(param, "returnAccountDetailsWithJob", 1)
    End Function
    Public Function ldInvoice(ByVal cmdText As String) As DataSet
        Dim param As String(,) = New String(3, 2) {{"@Prefix", PreFix, ""}, _
                                                   {"@Jvnum", JVNum, ""}, _
                                                   {"@Jvtype", JVType, ""}, _
                                                   {"@TP", TypeNo, ""}}
        Return _cmnMthds._ldDataset(param, cmdText, 4)
    End Function
    Public Function returnOpeningbalanceInPDC(ByVal _Accno As Integer) As DataTable
        Dim param As String(,) = New String(0, 2) {{"@AccountNO", _Accno, ""}}
        Return _cmnMthds.fLoadDatatable(param, "returnOpeningbalanceInPDC", 1)
    End Function
    Public Function returnPaymentDetails() As DataSet
        Dim branch As String = IIf(UsrBr = "", Dbranch, UsrBr)
        Dim userid As String = IIf(enableuserwisetransactionlist And userType, CurrentUser, "")
        Dim param As String(,) = New String(8, 2) {{"@pType", ptype, ""}, _
                                                  {"@DateFrom", DateFrom, "d"}, _
                                                  {"@DateTo", DateTo, "d"}, _
                                                  {"@AccountNo", AccountNo, ""}, _
                                                  {"@JVType", JVType, ""}, _
                                                  {"@JobCode", Trim(JobCode & ""), ""}, _
                                                  {"@Reference", Trim(Reference & ""), ""}, _
                                                  {"@CmnBrId", Trim(branch & ""), ""}, _
                                                  {"@userid", Trim(userid & ""), ""}}
        Return _cmnMthds._ldDataset(param, "returnPaymentDetails", 9)
    End Function
    Public Function returnldTrans(ByVal Accid As Long, ByVal varismodi As Integer, ByVal ldtrid As Long) As DataTable
        Dim param As String(,) = New String(3, 2) {{"@Accid", Accid, ""}, _
                                                   {"@ismodi", varismodi, ""}, _
                                                   {"@ldTrid", ldtrid, ""}, _
                                                   {"@Brid", UsrBr, ""}}
        Return _cmnMthds.fLoadDatatable(param, "returnldTrans", 4)

    End Function
    Public Function returnStatementGrid(ByVal _Type As Integer) As DataTable
        Dim strParam As String(,) = New String(1, 2) { _
                                            {"@Type", _Type, ""}, _
                                            {"@brid", UsrBr, ""}}
        Return _cmnMthds.fLoadDatatable(strParam, "returnStatementGrid", 2)
    End Function

    Public Function returnVazhipaduRates(ByVal _Type As Integer) As DataSet
        Dim strParam As String(,) = New String(0, 2) { _
                                            {"@Type", _Type, ""}}
        Return _cmnMthds._ldDataset(strParam, "returnVazhipaduRates", 1)
    End Function
    Public Function returnStatementReport(ByVal JVdate_From As DateTime, ByVal JVDate_To As DateTime, ByVal accid As Long, ByVal Val As Integer, ByVal AgeingOnDueDt As Integer, ByVal grpSeton As String, ByVal isremoveadvance As Integer, Optional ByVal areacode As String = "") As DataSet
        Dim param As String(,) = New String(8, 2) {{"@FromDate", Format(JVdate_From, "yyyy/MM/dd"), "d"}, _
                                                     {"@ToDate", Format(JVDate_To, "yyyy/MM/dd"), "d"}, _
                                                     {"@accid", accid, ""}, _
                                                     {"@optReport", Val, ""}, _
                                                     {"@AgeingOnDueDt", AgeingOnDueDt, ""}, _
                                                     {"@grpSeton", Trim(grpSeton & ""), ""}, _
                                                      {"@areacode", Trim(areacode & ""), ""}, _
                                                     {"@brid", Trim(UsrBr & ""), ""}, _
                                                     {"@isremoveadvance", isremoveadvance, ""}}
        Return _cmnMthds._ldDataset(param, "returnStatementReport", 9)
    End Function
    Public Function returnOutstandingForAll(ByVal JVdate_From As DateTime, ByVal JVDate_To As DateTime, ByVal accid As Long, ByVal Val As Integer, ByVal AgeingOnDueDt As Integer, ByVal grpSeton As String, ByVal deliveredBy As String) As DataSet
        Dim param As String(,) = New String(6, 2) {{"@FromDate", Format(JVdate_From, "yyyy/MM/dd"), "d"}, _
                                                     {"@ToDate", Format(JVDate_To, "yyyy/MM/dd"), "d"}, _
                                                     {"@accid", accid, ""}, _
                                                     {"@optReport", Val, ""}, _
                                                     {"@AgeingOnDueDt", AgeingOnDueDt, ""}, _
                                                     {"@grpSeton", Trim(grpSeton & ""), ""}, _
                                                     {"@deliveredBy", Trim(deliveredBy & ""), ""}}
        Return _cmnMthds._ldDataset(param, "returnOutstandingForAll", 7)
    End Function
    Public Function returnTrialbalance(ByVal dateFrom As Date, ByVal asOnDate As Date, ByVal tp As Integer) As DataSet
        Dim param As String(,) = New String(3, 2) {{"@dateFrom", Format(dateFrom, "yyyy/MM/dd"), "d"}, _
                                                   {"@asondate", Format(asOnDate, "yyyy/MM/dd"), "d"}, _
                                                    {"@tp", tp, ""}, _
                                                    {"@brid", UsrBr, ""}}
        Return _cmnMthds._ldDataset(param, "returnTrialbalance", 4)
    End Function
    Public Function returnVoucherReport() As DataSet
        Dim param As String(,) = New String(4, 2) {{"@JVType", JVType, ""}, _
                                                   {"@JVDate1", DateFrom, "d"}, _
                                                   {"@JVDate2", DateTo, "d"}, _
                                                    {"@tp", ptype, ""}, _
                                                    {"@brid", UsrBr, ""}}
        Return _cmnMthds._ldDataset(param, "returnVoucherReport", 5)
    End Function
    Public Function returnReconciliationGrid(ByVal _FromDate As DateTime, ByVal _ToDate As DateTime, ByVal _BankCode As String, ByVal _AccountNo As Integer, ByVal _Type As Integer) As DataTable
        Dim strParam As String(,) = New String(5, 2) {{"@FromDate", Format(_FromDate, "yyyy/MM/dd"), "d"}, _
                                                     {"@ToDate", Format(_ToDate, "yyyy/MM/dd"), "d"}, _
                                            {"@bankCode", _BankCode, ""}, _
                                            {"@AccountNo", _AccountNo, ""}, _
                                            {"@Type", _Type, ""}, _
                                            {"@brid", UsrBr, ""}}
        Return _cmnMthds.fLoadDatatable(strParam, "returnReconciliationGrid", 6)
    End Function


    Public Function returnReconciliationReport(ByVal _FromDate As DateTime, ByVal _ToDate As DateTime, ByVal _BankCode As String, ByVal _AccountNo As Integer) As DataSet
        Dim strParam As String(,) = New String(3, 2) {{"@FromDate", Format(_FromDate, "yyyy/MM/dd"), "d"}, _
                                                     {"@ToDate", Format(_ToDate, "yyyy/MM/dd"), "d"}, _
                                            {"@bankCode", _BankCode, ""}, _
                                            {"@AccountNo", _AccountNo, ""}}
        Return _cmnMthds._ldDataset(strParam, "returnReconciliationReport", 4)
    End Function
    Public Function updateClearedDate(ByVal _ClearedDate As DateTime, ByVal _UnqNo As Integer) As Integer
        Dim param As String(,) = New String(1, 2) {{"@ClearedDate", Format(_ClearedDate, "yyyy/MM/dd"), "d"}, _
                                                    {"@UnqNo", _UnqNo, ""}}

        updateClearedDate = _cmnMthds._ExecuteScalar(param, "[updateClearedDate]", 2)
        'dbManager.ExecuteNonQuery(CommandType.Text, sql,""},
    End Function
    Public Function loadHomeSummary(ByVal _FromDate As Date, ByVal _ToDate As Date) As DataSet
        Dim strParam As String(,) = New String(2, 2) {{"@JVDate1", Format(_FromDate, "yyyy/MM/dd"), "d"}, _
                                                    {"@JVDate2", Format(_ToDate, "yyyy/MM/dd"), "d"}, _
                                                    {"@brid", UsrBr, ""}}
        Return _cmnMthds._ldDataset(strParam, "loadSummary", 3)
    End Function
    Public Function returnPDCTransfer(ByVal GrpSetOnTp As Integer, ByVal PDCStatus As Integer) As DataSet
        Dim param As String(,) = New String(5, 2) {{"@datefrom", DateFrom, "d"}, _
                                                   {"@dateto", DateTo, "d"}, _
                                                    {"@tp", ptype, ""}, _
                                                    {"@GrpSetOnTp", GrpSetOnTp, ""}, _
                                                    {"@PDCStatus", PDCStatus, ""}, _
                                                    {"@brid", UsrBr, ""}}
        Return _cmnMthds._ldDataset(param, "returnPDCTransfer", 6)
    End Function
    Public Function deleteAccountTransaction(ByVal linkNo As Long) As DataSet
        Dim param As String(,) = New String(1, 2) {{"@LinkNo", linkNo, ""}, _
                                                    {"@UserId", CurrentUser, ""}}
        Return _cmnMthds._ldDataset(param, "deleteAccountTransaction", 2)
    End Function
    Public Function returnTempleIncomeAndExpese(ByVal dateFrom As Date, ByVal asOnDate As Date) As DataSet
        Dim param As String(,) = New String(1, 2) {{"@dateFrom", Format(dateFrom, "yyyy/MM/dd"), "d"}, _
                                                   {"@asondate", Format(asOnDate, "yyyy/MM/dd"), "d"}}
        Return _cmnMthds._ldDataset(param, "returnTempleIncomeAndExpese", 2)
    End Function
    Public Sub closeDlayerConnection()
        _ldlayer.closeCon()
    End Sub
    Public Function returnAgeingReport(ByVal JVdate_From As DateTime, ByVal JVDate_To As DateTime, ByVal accid As Long, ByVal AgeingOnDueDt As Integer, _
                                   ByVal grpSeton As String, ByVal a1 As Integer, ByVal a2 As Integer, ByVal a3 As Integer, ByVal a4 As Integer) As DataSet
        Dim param As String(,) = New String(9, 2) {{"@FromDate", Format(JVDate_To, "yyyy/MM/dd"), "d"}, _
                                                     {"@ToDate", Format(JVDate_To, "yyyy/MM/dd"), "d"}, _
                                                     {"@accid", accid, ""}, _
                                                     {"@AgeingOnDueDt", AgeingOnDueDt, ""}, _
                                                     {"@grpSeton", Trim(grpSeton & ""), ""}, _
                                                     {"@a1", a1, ""}, _
                                                     {"@a2", a2, ""}, _
                                                     {"@a3", a3, ""}, _
                                                     {"@a4", a4, ""}, _
                                                     {"@brid", UsrBr, ""}}
        Return _cmnMthds._ldDataset(param, "returnAgeingReport", 10)
    End Function
#Region "LoadFromDatalayer"
    Public Function returnLEDGERstatementreport(ByVal JVdate_From As DateTime, ByVal JVDate_To As DateTime, ByVal accid As Long, ByVal Val As Integer, ByVal AgeingOnDueDt As Integer, ByVal grpSeton As String, ByVal isremoveadvance As Integer, Optional ByVal areacode As String = "") As DataSet
        Dim param As String(,) = New String(3, 2) {{"@fromdate", Format(JVdate_From, "yyyy/MM/dd"), "d"}, _
                                                     {"@todate", Format(JVDate_To, "yyyy/MM/dd"), "d"}, _
                                                     {"@accid", accid, ""}, _
                                                     {"@brid", Trim(UsrBr & ""), ""}}
        Return _cmnMthds._ldDataset(param, "returnLEDGERstatementreport", 4)
    End Function
#End Region
End Class
