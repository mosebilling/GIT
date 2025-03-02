Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DataAccessLayer
Imports System.Data
Public Class clsTempleInv
    Private _cmnDlink As New cl_CmnDataLink()
    Private _cmnMthds As New CmnMethods()
#Region "TempleSalesCmn"
    Private _Trid As Long
    Public Property Trid() As Long
        Get
            Return _Trid
        End Get
        Set(ByVal value As Long)
            _Trid = value
        End Set
    End Property
    Private _Trdate As Date
    Public Property Trdate() As Date
        Get
            Return _Trdate
        End Get
        Set(ByVal value As Date)
            _Trdate = value
        End Set
    End Property
    Private _Prefix As String
    Public Property Prefix() As String
        Get
            Return _Prefix
        End Get
        Set(ByVal value As String)
            _Prefix = value
        End Set
    End Property
    Private _InvNo As Integer
    Public Property InvNo() As Integer
        Get
            Return _InvNo
        End Get
        Set(ByVal value As Integer)
            _InvNo = value
        End Set
    End Property
    Private _Reference As String
    Public Property Reference() As String
        Get
            Return _Reference
        End Get
        Set(ByVal value As String)
            _Reference = value
        End Set
    End Property
    Private _salesAc As Integer
    Public Property salesAc() As Integer
        Get
            Return _salesAc
        End Get
        Set(ByVal value As Integer)
            _salesAc = value
        End Set
    End Property
    Private _CustomerAc As Integer
    Public Property CustomerAc() As Integer
        Get
            Return _CustomerAc
        End Get
        Set(ByVal value As Integer)
            _CustomerAc = value
        End Set
    End Property
    Private _CsCustomeriD As Integer
    Public Property CsCustomeriD() As Integer
        Get
            Return _CsCustomeriD
        End Get
        Set(ByVal value As Integer)
            _CsCustomeriD = value
        End Set
    End Property
    Private _StarId As Integer
    Public Property StarId() As Integer
        Get
            Return _StarId
        End Get
        Set(ByVal value As Integer)
            _StarId = value
        End Set
    End Property
    Private _TrDescription As String
    Public Property TrDescription() As String
        Get
            Return _TrDescription
        End Get
        Set(ByVal value As String)
            _TrDescription = value
        End Set
    End Property
    Private _rndoff As Double
    Public Property rndoff() As Double
        Get
            Return _rndoff
        End Get
        Set(ByVal value As Double)
            _rndoff = value
        End Set
    End Property
    Private _VoucherTypeid As Integer
    Public Property VoucherTypeid() As Integer
        Get
            Return _VoucherTypeid
        End Get
        Set(ByVal value As Integer)
            _VoucherTypeid = value
        End Set
    End Property
    Private _NetTot As Double
    Public Property NetTot() As Double
        Get
            Return _NetTot
        End Get
        Set(ByVal value As Double)
            _NetTot = value
        End Set
    End Property
    Private _UserId As String
    Public Property UserId() As String
        Get
            Return _UserId
        End Get
        Set(ByVal value As String)
            _UserId = value
        End Set
    End Property
    Private _CashCustName As String
    Public Property CashCustName()
        Get
            Return _CashCustName
        End Get
        Set(ByVal value)
            _CashCustName = value
        End Set
    End Property
    Private _CashCustAddr As String
    Public Property CashCustAddr()
        Get
            Return _CashCustAddr
        End Get
        Set(ByVal value)
            _CashCustAddr = value
        End Set
    End Property
    Private _vazhipaduTotal As Double
    Public Property vazhipaduTotal()
        Get
            Return _vazhipaduTotal
        End Get
        Set(ByVal value)
            _vazhipaduTotal = value
        End Set
    End Property
    Private _ItemTotal As Double
    Public Property ItemTotal()
        Get
            Return _ItemTotal
        End Get
        Set(ByVal value)
            _ItemTotal = value
        End Set
    End Property
    Private _membershipId As Integer
    Public Property membershipId() As Integer
        Get
            Return _membershipId
        End Get
        Set(ByVal value As Integer)
            _membershipId = value
        End Set
    End Property
    
    Public Function TempleSalesCmnTbSaveAndModify() As String
        Dim _id As String
        Dim param As String(,) = New String(17, 2) {{"@TrId", Trid, ""}, _
                                            {"@TrDate", Format(Trdate, "yyyy/MM/dd"), "d"}, _
                                            {"@PreFix", Convert.ToString(Prefix) & "", ""}, _
                                            {"@InvNo", InvNo, ""}, _
                                            {"@Reference", Reference, ""}, _
                                            {"@salesAc", salesAc, ""}, _
                                            {"@CustomerAc", CustomerAc, ""}, _
                                            {"@CashCustName", CashCustName, ""}, _
                                            {"@CashCustAddr", CashCustAddr, ""}, _
                                            {"@StarId", StarId, ""}, _
                                            {"@TrDescription", Convert.ToString(TrDescription) & "", ""}, _
                                            {"@rndoff", rndoff, ""}, _
                                            {"@VoucherTypeid", VoucherTypeid, ""}, _
                                            {"@NetTot", NetTot, ""}, _
                                            {"@UserId", Convert.ToString(UserId) & "", ""}, _
                                            {"@vazhipaduTotal", vazhipaduTotal, ""}, _
                                            {"@ItemTotal", ItemTotal, ""}, _
                                            {"@membershipId", memberid, ""}}
        Dim recordNo As Object
        recordNo = _cmnMthds._ExecuteScalar(param, "TempleSalesCmnTbSaveAndModify", 18)
        _id = recordNo.ToString()
        Return _id
    End Function
    Public Sub deleteVazhipaduSales()
        Dim param As String(,) = New String(1, 2) {{"@TrId", Trid, ""}, _
                                                   {"@userid", CurrentUser, ""}}
        _cmnMthds._ExecuteNonQuery(param, "deleteVazhipaduSales", 2)
    End Sub
#End Region
#Region "TempleSaleDet"
    Private _Detid As Long
    Public Property Detid() As Long
        Get
            Return _Detid
        End Get
        Set(ByVal value As Long)
            _Detid = value
        End Set
    End Property
    Private _Itemid As Long
    Public Property Itemid() As Long
        Get
            Return _Itemid
        End Get
        Set(ByVal value As Long)
            _Itemid = value
        End Set
    End Property
    Private _Acid As Long
    Public Property Acid() As Long
        Get
            Return _Acid
        End Get
        Set(ByVal value As Long)
            _Acid = value
        End Set
    End Property
    Private _ItemDescription As String
    Public Property ItemDescription() As String
        Get
            Return _ItemDescription
        End Get
        Set(ByVal value As String)
            _ItemDescription = value
        End Set
    End Property
    Private _VazhipaduDate As Date
    Public Property VazhipaduDate() As Date
        Get
            Return _VazhipaduDate
        End Get
        Set(ByVal value As Date)
            _VazhipaduDate = value
        End Set
    End Property
    Private _Unit As String
    Public Property Unit() As String
        Get
            Return _Unit
        End Get
        Set(ByVal value As String)
            _Unit = value
        End Set
    End Property
    Private _Qty As Double
    Public Property Qty() As Double
        Get
            Return _Qty
        End Get
        Set(ByVal value As Double)
            _Qty = value
        End Set
    End Property
    Private _Rate As Double
    Public Property Rate() As Double
        Get
            Return _Rate
        End Get
        Set(ByVal value As Double)
            _Rate = value
        End Set
    End Property
    Private _Isacc As Boolean
    Public Property Isacc() As Boolean
        Get
            Return _Isacc
        End Get
        Set(ByVal value As Boolean)
            _Isacc = value
        End Set
    End Property
    Private _SlNo As Integer
    Public Property SlNo() As Integer
        Get
            Return _SlNo
        End Get
        Set(ByVal value As Integer)
            _SlNo = value
        End Set
    End Property
    Private _isTempleNonVazhipaduItem As Integer
    Public Property isTempleNonVazhipaduItem() As Integer
        Get
            Return _isTempleNonVazhipaduItem
        End Get
        Set(ByVal value As Integer)
            _isTempleNonVazhipaduItem = value
        End Set
    End Property
    Public Sub TempleSalesDetTbSaveModify()
        Dim param As String(,) = New String(11, 2) {{"@Detid", Detid, ""}, _
                                            {"@TrId", Trid, ""}, _
                                            {"@SlNo", SlNo, ""}, _
                                            {"@Itemid", Itemid, ""}, _
                                            {"@Acid", Acid, ""}, _
                                            {"@ItemDescription", Trim(ItemDescription & ""), ""}, _
                                            {"@VazhipaduDate", Format(VazhipaduDate, "yyyy/MM/dd"), "d"}, _
                                            {"@Unit", Unit, ""}, _
                                            {"@Qty", Qty, ""}, _
                                            {"@Rate", Rate, ""}, _
                                            {"@Isacc", Isacc, ""}, _
                                            {"@isTempleNonVazhipaduItem", isTempleNonVazhipaduItem, ""}}
        _cmnMthds._ExecuteNonQuery(param, "TempleSalesDetTbSaveModify", 12)
    End Sub
#End Region
#Region "TempleAdmission"
    Private _memberid As Long
    Public Property memberid() As Long
        Get
            Return _memberid
        End Get
        Set(ByVal value As Long)
            _memberid = value
        End Set
    End Property
    Private _Mdate As Date
    Public Property Mdate() As Date
        Get
            Return _Mdate
        End Get
        Set(ByVal value As Date)
            _Mdate = value
        End Set
    End Property
    Private _MemberCode As String
    Public Property MemberCode() As String
        Get
            Return _MemberCode
        End Get
        Set(ByVal value As String)
            _MemberCode = value
        End Set
    End Property
    Private _MemberName As String
    Public Property MemberName() As String
        Get
            Return _MemberName
        End Get
        Set(ByVal value As String)
            _MemberName = value
        End Set
    End Property
    Private _Addr1 As String
    Public Property Addr1() As String
        Get
            Return _Addr1
        End Get
        Set(ByVal value As String)
            _Addr1 = value
        End Set
    End Property
    Private _Addr2 As String
    Public Property Addr2() As String
        Get
            Return _Addr2
        End Get
        Set(ByVal value As String)
            _Addr2 = value
        End Set
    End Property
    Private _Addr3 As String
    Public Property Addr3() As String
        Get
            Return _Addr3
        End Get
        Set(ByVal value As String)
            _Addr3 = value
        End Set
    End Property
    Private _Addr4 As String
    Public Property Addr4() As String
        Get
            Return _Addr4
        End Get
        Set(ByVal value As String)
            _Addr4 = value
        End Set
    End Property
    Private _FamilyName As String
    Public Property FamilyName() As String
        Get
            Return _FamilyName
        End Get
        Set(ByVal value As String)
            _FamilyName = value
        End Set
    End Property
    Private _Phone As String
    Public Property Phone() As String
        Get
            Return _Phone
        End Get
        Set(ByVal value As String)
            _Phone = value
        End Set
    End Property
    Private _Email As String
    Public Property Email() As String
        Get
            Return _Email
        End Get
        Set(ByVal value As String)
            _Email = value
        End Set
    End Property
    Private _BloodGrp As String
    Public Property BloodGrp() As String
        Get
            Return _BloodGrp
        End Get
        Set(ByVal value As String)
            _BloodGrp = value
        End Set
    End Property
    Private _Occupation As String
    Public Property Occupation() As String
        Get
            Return _Occupation
        End Get
        Set(ByVal value As String)
            _Occupation = value
        End Set
    End Property
    Private _MStatus As Integer
    Public Property MStatus() As Integer
        Get
            Return _MStatus
        End Get
        Set(ByVal value As Integer)
            _MStatus = value
        End Set
    End Property
    Private _Gender As Integer
    Public Property Gender() As String
        Get
            Return _Gender
        End Get
        Set(ByVal value As String)
            _Gender = value
        End Set
    End Property
    Private _Mgroup As Integer
    Public Property Mgroup() As Integer
        Get
            Return _Mgroup
        End Get
        Set(ByVal value As Integer)
            _Mgroup = value
        End Set
    End Property
    Private _Designation As String
    Public Property Designation() As String
        Get
            Return _Designation
        End Get
        Set(ByVal value As String)
            _Designation = value
        End Set
    End Property
    Private _OpeningDueAmt As Double
    Public Property OpeningDueAmt() As Double
        Get
            Return _OpeningDueAmt
        End Get
        Set(ByVal value As Double)
            _OpeningDueAmt = value
        End Set
    End Property
    Private _LivesIn As Integer
    Public Property LivesIn() As Integer
        Get
            Return _LivesIn
        End Get
        Set(ByVal value As Integer)
            _LivesIn = value
        End Set
    End Property
    Private _LastPaidDate As Date
    Public Property LastPaidDate() As Date
        Get
            Return _LastPaidDate
        End Get
        Set(ByVal value As Date)
            _LastPaidDate = value
        End Set
    End Property
    Public Sub TempleMembershipTbSaveModify()
        'Dim _id As String
        Dim param As String(,) = New String(33, 2) {{"@memberid", memberid, ""}, _
                                            {"@Mdate", Format(Mdate, "yyyy/MM/dd"), "d"}, _
                                            {"@MemberCode", Convert.ToString(MemberCode) & "", ""}, _
                                            {"@MemberName", MemberName, ""}, _
                                            {"@Addr1", Addr1, ""}, _
                                            {"@Addr2", Addr2, ""}, _
                                            {"@Addr3", Addr3, ""}, _
                                            {"@Addr4", Addr4, ""}, _
                                            {"@FamilyName", FamilyName, ""}, _
                                            {"@Phone", Phone, ""}, _
                                            {"@Email", Convert.ToString(Email) & "", ""}, _
                                            {"@BloodGrp", BloodGrp, ""}, _
                                            {"@Occupation", Occupation, ""}, _
                                            {"@MStatus", MStatus, ""}, _
                                            {"@Gender", Gender, ""}, _
                                            {"@Mgroup", Mgroup & "", ""}, _
                                            {"@Designation", Designation, ""}, _
                                            {"@OpeningDueAmt", OpeningDueAmt, ""}, _
                                            {"@LivesIn", LivesIn, ""}, _
                                            {"@UsrId", Convert.ToString(UserId) & "", ""}, _
                                            {"@LastPaidDate", Format(LastPaidDate, "yyyy/MM/dd"), "d"}, _
                                            {"@isWu", IsWU, ""}, _
                                            {"@housename", housename, ""}, _
                                            {"@houseno", houseno, ""}, _
                                            {"@ward", ward, ""}, _
                                            {"@familiunitname", familyunitname, ""}, _
                                            {"@kallarano", kallarano & "", ""}, _
                                            {"@subscription", subscription, ""}, _
                                            {"@dob", dob, "d"}, _
                                            {"@marriagedate", marriagedate, "d"}, _
                                            {"@adharno", Trim(adharno & ""), ""}, _
                                            {"@qualification", Trim(qualification & ""), ""}, _
                                            {"@dateofdeath", dateofdeath, "d"}, _
                                            {"@ismarriage", ismarriage, ""}}
        _cmnMthds._ExecuteNonQuery(param, "TempleMembershipTbSaveModify", 34)
    End Sub
#End Region
#Region "TempleFamilyDet"
    Private _familiid As Long
    Public Property familiid() As Long
        Get
            Return _familiid
        End Get
        Set(ByVal value As Long)
            _familiid = value
        End Set
    End Property
    Private _fmembername As String
    Public Property fmembername() As String
        Get
            Return _fmembername
        End Get
        Set(ByVal value As String)
            _fmembername = value
        End Set
    End Property
    Private _relation As String
    Public Property relation() As String

        Get
            Return _relation
        End Get
        Set(ByVal value As String)
            _relation = value
        End Set
    End Property
    Private _category As String
    Public Property category() As String
        Get
            Return _category
        End Get
        Set(ByVal value As String)
            _category = value
        End Set
    End Property
    Private _IsWU As Boolean
    Public Property IsWU() As Boolean
        Get
            Return _IsWU
        End Get
        Set(ByVal value As Boolean)
            _IsWU = value
        End Set
    End Property
    Private _WURoll As String
    Public Property WURoll() As String
        Get
            Return _WURoll
        End Get
        Set(ByVal value As String)
            _WURoll = value
        End Set
    End Property
    Private _StudentStandard As String
    Public Property StudentStandard() As String
        Get
            Return _StudentStandard
        End Get
        Set(ByVal value As String)
            _StudentStandard = value
        End Set
    End Property
    Private _StudentSchool As String
    Public Property StudentSchool() As String
        Get
            Return _StudentSchool
        End Get
        Set(ByVal value As String)
            _StudentSchool = value
        End Set
    End Property
    Private _housename As String
    Public Property housename() As String
        Get
            Return _housename
        End Get
        Set(ByVal value As String)
            _housename = value
        End Set
    End Property
    Private _houseno As String
    Public Property houseno() As String
        Get
            Return _houseno
        End Get
        Set(ByVal value As String)
            _houseno = value
        End Set
    End Property
    Private _ward As String
    Public Property ward() As String
        Get
            Return _ward
        End Get
        Set(ByVal value As String)
            _ward = value
        End Set
    End Property
    Private _familyunitname As String
    Public Property familyunitname() As String
        Get
            Return _familyunitname
        End Get
        Set(ByVal value As String)
            _familyunitname = value
        End Set
    End Property
    Private _kallarano As String
    Public Property kallarano() As String
        Get
            Return _kallarano
        End Get
        Set(ByVal value As String)
            _kallarano = value
        End Set
    End Property
    Private _subscription As String
    Public Property subscription() As String
        Get
            Return _subscription
        End Get
        Set(ByVal value As String)
            _subscription = value
        End Set
    End Property
    Private _dob As Date
    Public Property dob() As Date
        Get
            Return _dob
        End Get
        Set(ByVal value As Date)
            _dob = value
        End Set
    End Property
    Private _marriagedate As Date
    Public Property marriagedate() As Date
        Get
            Return _marriagedate
        End Get
        Set(ByVal value As Date)
            _marriagedate = value
        End Set
    End Property
    Private _adharno As String
    Public Property adharno() As String
        Get
            Return _adharno
        End Get
        Set(ByVal value As String)
            _adharno = value
        End Set
    End Property
    Private _qualification As String
    Public Property qualification() As String
        Get
            Return _qualification
        End Get
        Set(ByVal value As String)
            _qualification = value
        End Set
    End Property
    Private _baptismdate As Date
    Public Property baptismdate() As Date
        Get
            Return _baptismdate
        End Get
        Set(ByVal value As Date)
            _baptismdate = value
        End Set
    End Property
    Private _dateofdeath As Date
    Public Property dateofdeath() As Date
        Get
            Return _dateofdeath
        End Get
        Set(ByVal value As Date)
            _dateofdeath = value
        End Set
    End Property
    Private _ismarriage As Boolean
    Public Property ismarriage() As Boolean
        Get
            Return _ismarriage
        End Get
        Set(ByVal value As Boolean)
            _ismarriage = value
        End Set
    End Property
    Private _isbaptism As Boolean
    Public Property isbaptism() As Boolean
        Get
            Return _isbaptism
        End Get
        Set(ByVal value As Boolean)
            _isbaptism = value
        End Set
    End Property

    Public Sub TempleFamilyTbSaveModify()
        Dim param As String(,) = New String(22, 2) {{"@familiid", familiid, ""}, _
                                            {"@fkMembershipId", memberid, ""}, _
                                            {"@fmembername", fmembername, ""}, _
                                            {"@relation", relation, ""}, _
                                            {"@gender", Gender, ""}, _
                                            {"@category", category, ""}, _
                                            {"@occupation", Occupation, ""}, _
                                            {"@IsWU", IsWU, ""}, _
                                            {"@WURoll", WURoll, ""}, _
                                            {"@StudentStandard", StudentStandard & "", ""}, _
                                            {"@StudentSchool", StudentSchool, ""}, _
                                            {"@bloodgroup", BloodGrp, ""}, _
                                            {"@LivesIn", LivesIn, ""}, _
                                            {"@dob", dob, "d"}, _
                                            {"@marriagedate", marriagedate, "d"}, _
                                            {"@phone", Phone, ""}, _
                                            {"@adharno", adharno & "", ""}, _
                                            {"@qualification", qualification, ""}, _
                                            {"@dateofdeath", dateofdeath, "d"}, _
                                            {"@baptismdate", baptismdate, "d"}, _
                                            {"@mstatus", MStatus, ""}, _
                                            {"ismarried", ismarriage, ""}, _
                                            {"@isbaptism", isbaptism, ""}}
        _cmnMthds._ExecuteNonQuery(param, "TempleFamilyTbSaveModify", 23)
    End Sub
#End Region
    Public Function ldInvoice(ByVal cmdText As String, Optional ByVal printno As Integer = 0, Optional ByVal withdevotee As Integer = 0) As DataSet
        Dim param As String(,) = New String(2, 2) {{"@prefix", Prefix, ""}, _
                                                   {"@invno", InvNo, ""}, _
                                                    {"@withdevotee", withdevotee, ""}}
        Return _cmnMthds._ldDataset(param, cmdText, 3)
    End Function
    Public Function returnMemberAdmissionDetails(Optional ByVal tp As Integer = 0) As DataSet
        Dim param As String(,) = New String(1, 2) {{"@memberid", memberid, ""}, _
                                                   {"@tp", tp, ""}}
        Return _cmnMthds._ldDataset(param, "returnMemberAdmissionDetails", 2)
    End Function
    Public Function returnTempleMemberShipPendingAmt() As Double
        Dim param As String(,) = New String(0, 2) {{"@memberid", memberid, ""}}
        Dim dt As DataTable = _cmnMthds._ldDataset(param, "returnTempleMemberShipPendingAmt", 1).Tables(0)
        Return dt(0)(0)
    End Function

    Public Function returnMembershipsales(ByVal datefrom As Date, ByVal dateto As Date, ByVal memberid As Integer) As DataSet
        Dim param As String(,) = New String(2, 2) {{"@datefrom", datefrom, "d"}, _
                                                   {"@dateto", dateto, "d"}, _
                                                   {"@membershipid", memberid, ""}}
        Return _cmnMthds._ldDataset(param, "returnMembershipsales", 3)
    End Function

End Class
