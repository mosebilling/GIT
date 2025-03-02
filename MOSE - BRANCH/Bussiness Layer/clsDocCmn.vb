Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DataAccessLayer
Imports System.Data
Public Class clsDocCmn
    Private _cmnDlink As New cl_CmnDataLink()
    Private _cmnMthds As New CmnMethods()
    Private _ldlayer As New Dlayer
#Region "Properties"
    Private _Attn As String
    Private _BaseId As Long
    Private _BrId As String
    Private _Comment As String
    Private _Completion As String
    Private _CostPUnit As Single
    Private _CreatedDt As DateTime
    Private _CSCode As Long
    Private _DDate As DateTime
    Private _DelvDt As DateTime
    Private _DeptId As String
    Private _Discount As Single
    Private _Discount1 As Single
    Private _DNo As Integer
    Private _DocAmt As Single
    Private _DocDefLoc As String
    Private _DocId As Long
    Private _DocMId As Long
    Private _DocType As String
    Private _DueDt As DateTime
    Private _EnqId As Long
    Private _FC As String
    Private _FCRate As Single
    Private _Fld1 As String
    Private _Fld2 As String
    Private _Fld3 As String
    Private _Fld4 As String
    Private _Fld5 As String
    Private _Footer As Integer
    Private _Header As Integer
    Private _id As Long
    Private _ImpDoc As Integer
    Private _ImpDocId As Integer
    Private _ImpDocLnNo As Integer
    Private _Imported As Boolean
    Private _IsFC As Boolean
    Private _isModi As Boolean
    Private _ItemId As Long
    Private _JbDescription As String
    Private _JobCode As String
    Private _LMsgNo As Integer
    Private _Location As String
    Private _LocRow As Integer
    Private _lpoclass As String
    Private _lstdate As DateTime
    Private _MchName As String
    Private _MsgId As Short
    Private _Mthd As String
    Private _MTrPQty As Single
    Private _NFraction As Integer
    Private _Pack As String
    Private _PFraction As Short
    Private _Qty As Single
    Private _Reference As String
    Private _rndoff As Double
    Private _SlManId As String
    Private _SlNo As Short
    Private _StkUpdtd As Boolean
    Private _Subject As String
    Private _TermsId As String
    Private _TrDetail As String
    Private _Unit As String
    Private _UserId As String
    Private _Vessel As String
    Private _FromLoc As String
    Private _FromJob As String
    Public Property FromLoc() As String
        Get
            Return _FromLoc
        End Get
        Set(ByVal value As String)
            _FromLoc = value
        End Set
    End Property
    Public Property FromJob() As String
        Get
            Return _FromJob
        End Get
        Set(ByVal value As String)
            _FromJob = value
        End Set
    End Property
    Public Property Attn() As String
        Get
            Return _Attn
        End Get
        Set(ByVal value As String)
            _Attn = value
        End Set
    End Property
    Public Property BaseId() As Long
        Get
            Return _BaseId
        End Get
        Set(ByVal value As Long)
            _BaseId = value
        End Set
    End Property
    Public Property BrId() As String
        Get
            Return _BrId
        End Get
        Set(ByVal value As String)
            _BrId = value
        End Set
    End Property

    Public Property Comment() As String
        Get
            Return _Comment
        End Get
        Set(ByVal value As String)
            _Comment = value
        End Set
    End Property

    Public Property Completion() As String
        Get
            Return _Completion
        End Get
        Set(ByVal value As String)
            _Completion = value
        End Set
    End Property

    Public Property CostPUnit() As Single
        Get
            Return _CostPUnit
        End Get
        Set(ByVal value As Single)
            _CostPUnit = value
        End Set
    End Property

    Public Property CreatedDt() As DateTime
        Get
            Return _CreatedDt
        End Get
        Set(ByVal value As DateTime)
            _CreatedDt = value
        End Set
    End Property

    Public Property CSCode() As Long
        Get
            Return _CSCode
        End Get
        Set(ByVal value As Long)
            _CSCode = value
        End Set
    End Property

    Public Property DDate() As DateTime
        Get
            Return _DDate
        End Get
        Set(ByVal value As DateTime)
            _DDate = value
        End Set
    End Property

    Public Property DelvDt() As DateTime
        Get
            Return _DelvDt
        End Get
        Set(ByVal value As DateTime)
            _DelvDt = value
        End Set
    End Property

    Public Property DeptId() As String
        Get
            Return _DeptId
        End Get
        Set(ByVal value As String)
            _DeptId = value
        End Set
    End Property

    Public Property Discount() As Single
        Get
            Return _Discount
        End Get
        Set(ByVal value As Single)
            _Discount = value
        End Set
    End Property

    Public Property Discount1() As Single
        Get
            Return _Discount1
        End Get
        Set(ByVal value As Single)
            _Discount1 = value
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

    Public Property DNo() As Integer
        Get
            Return _DNo
        End Get
        Set(ByVal value As Integer)
            _DNo = value
        End Set
    End Property

    Public Property DocAmt() As Single
        Get
            Return _DocAmt
        End Get
        Set(ByVal value As Single)
            _DocAmt = value
        End Set
    End Property

    Public Property DocDefLoc() As String
        Get
            Return _DocDefLoc
        End Get
        Set(ByVal value As String)
            _DocDefLoc = value
        End Set
    End Property

    Public Property DocId() As Long
        Get
            Return _DocId
        End Get
        Set(ByVal value As Long)
            _DocId = value
        End Set
    End Property

    Public Property DocMId() As Long
        Get
            Return _DocMId
        End Get
        Set(ByVal value As Long)
            _DocMId = value
        End Set
    End Property

    Public Property DocType() As String
        Get
            Return _DocType
        End Get
        Set(ByVal value As String)
            _DocType = value
        End Set
    End Property

    Public Property DueDt() As DateTime
        Get
            Return _DueDt
        End Get
        Set(ByVal value As DateTime)
            _DueDt = value
        End Set
    End Property

    Public Property EnqId() As Long
        Get
            Return _EnqId
        End Get
        Set(ByVal value As Long)
            _EnqId = value
        End Set
    End Property

    Public Property FC() As String
        Get
            Return _FC
        End Get
        Set(ByVal value As String)
            _FC = value
        End Set
    End Property

    Public Property FCRate() As Single
        Get
            Return _FCRate
        End Get
        Set(ByVal value As Single)
            _FCRate = value
        End Set
    End Property

    Public Property Footer() As Integer
        Get
            Return _Footer
        End Get
        Set(ByVal value As Integer)
            _Footer = value
        End Set
    End Property

    Public Property Header() As Integer
        Get
            Return _Header
        End Get
        Set(ByVal value As Integer)
            _Header = value
        End Set
    End Property

    Public Property id() As Long
        Get
            Return _id
        End Get
        Set(ByVal value As Long)
            _id = value
        End Set
    End Property

    Public Property ImpDoc() As Integer
        Get
            Return _ImpDoc
        End Get
        Set(ByVal value As Integer)
            _ImpDoc = value
        End Set
    End Property

    Public Property ImpDocId() As Integer
        Get
            Return _ImpDocId
        End Get
        Set(ByVal value As Integer)
            _ImpDocId = value
        End Set
    End Property

    Public Property ImpDocLnNo() As Integer
        Get
            Return _ImpDocLnNo
        End Get
        Set(ByVal value As Integer)
            _ImpDocLnNo = value
        End Set
    End Property

    Public Property Imported() As Boolean
        Get
            Return _Imported
        End Get
        Set(ByVal value As Boolean)
            _Imported = value
        End Set
    End Property

    Public Property IsFC() As Boolean
        Get
            Return _IsFC
        End Get
        Set(ByVal value As Boolean)
            _IsFC = value
        End Set
    End Property

    Public Property isModi() As Boolean
        Get
            Return _isModi
        End Get
        Set(ByVal value As Boolean)
            _isModi = value
        End Set
    End Property

    Public Property ItemId() As Long
        Get
            Return _ItemId
        End Get
        Set(ByVal value As Long)
            _ItemId = value
        End Set
    End Property

    Public Property JbDescription() As String
        Get
            Return _JbDescription
        End Get
        Set(ByVal value As String)
            _JbDescription = value
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

    Public Property LMsgNo() As Integer
        Get
            Return _LMsgNo
        End Get
        Set(ByVal value As Integer)
            _LMsgNo = value
        End Set
    End Property

    Public Property Location() As String
        Get
            Return _Location
        End Get
        Set(ByVal value As String)
            _Location = value
        End Set
    End Property

    Public Property LocRow() As Integer
        Get
            Return _LocRow
        End Get
        Set(ByVal value As Integer)
            _LocRow = value
        End Set
    End Property

    Public Property lpoclass() As String
        Get
            Return _lpoclass
        End Get
        Set(ByVal value As String)
            _lpoclass = value
        End Set
    End Property

    Public Property lstdate() As DateTime
        Get
            Return _lstdate
        End Get
        Set(ByVal value As DateTime)
            _lstdate = value
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

    Public Property MsgId() As Short
        Get
            Return _MsgId
        End Get
        Set(ByVal value As Short)
            _MsgId = value
        End Set
    End Property

    Public Property Mthd() As String
        Get
            Return _Mthd
        End Get
        Set(ByVal value As String)
            _Mthd = value
        End Set
    End Property

    Public Property MTrPQty() As Single
        Get
            Return _MTrPQty
        End Get
        Set(ByVal value As Single)
            _MTrPQty = value
        End Set
    End Property

    Public Property NFraction() As Integer
        Get
            Return _NFraction
        End Get
        Set(ByVal value As Integer)
            _NFraction = value
        End Set
    End Property

    Public Property Pack() As String
        Get
            Return _Pack
        End Get
        Set(ByVal value As String)
            _Pack = value
        End Set
    End Property

    Public Property Penalty() As String
        Get
            Return _Fld5
        End Get
        Set(ByVal value As String)
            _Fld5 = value
        End Set
    End Property

    Public Property PFraction() As Short
        Get
            Return _PFraction
        End Get
        Set(ByVal value As Short)
            _PFraction = value
        End Set
    End Property

    Public Property PT1() As String
        Get
            Return _Fld1
        End Get
        Set(ByVal value As String)
            _Fld1 = value
        End Set
    End Property

    Public Property PT2() As String
        Get
            Return _Fld2
        End Get
        Set(ByVal value As String)
            _Fld2 = value
        End Set
    End Property

    Public Property PT3() As String
        Get
            Return _Fld3
        End Get
        Set(ByVal value As String)
            _Fld3 = value
        End Set
    End Property

    Public Property PT4() As String
        Get
            Return _Fld4
        End Get
        Set(ByVal value As String)
            _Fld4 = value
        End Set
    End Property

    Public Property Qty() As Single
        Get
            Return _Qty
        End Get
        Set(ByVal value As Single)
            _Qty = value
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

    Public Property rndoff() As Double
        Get
            Return _rndoff
        End Get
        Set(ByVal value As Double)
            _rndoff = value
        End Set
    End Property

    Public Property SlManId() As String
        Get
            Return _SlManId
        End Get
        Set(ByVal value As String)
            _SlManId = value
        End Set
    End Property

    Public Property SlNo() As Short
        Get
            Return _SlNo
        End Get
        Set(ByVal value As Short)
            _SlNo = value
        End Set
    End Property

    Public Property StkUpdtd() As Boolean
        Get
            Return _StkUpdtd
        End Get
        Set(ByVal value As Boolean)
            _StkUpdtd = value
        End Set
    End Property

    Public Property Subject() As String
        Get
            Return _Subject
        End Get
        Set(ByVal value As String)
            _Subject = value
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

    Public Property TrDetail() As String
        Get
            Return _TrDetail
        End Get
        Set(ByVal value As String)
            _TrDetail = value
        End Set
    End Property
    Private _custAddress As String
    Public Property custAddress() As String
        Get
            Return _custAddress
        End Get
        Set(ByVal value As String)
            _custAddress = value
        End Set
    End Property

    Public Property Unit() As String
        Get
            Return _Unit
        End Get
        Set(ByVal value As String)
            _Unit = value
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

    Public Property Vessel() As String
        Get
            Return _Vessel
        End Get
        Set(ByVal value As String)
            _Vessel = value
        End Set
    End Property
    Private _taxP As Double
    Public Property taxP() As Double
        Get
            Return _taxP
        End Get
        Set(ByVal value As Double)
            _taxP = value
        End Set
    End Property
    Private _taxAmt As Double
    Public Property taxAmt() As Double
        Get
            Return _taxAmt
        End Get
        Set(ByVal value As Double)
            _taxAmt = value
        End Set
    End Property
    Private _HSNCode As String
    Public Property HSNCode() As String
        Get
            Return _HSNCode
        End Get
        Set(ByVal value As String)
            _HSNCode = value
        End Set
    End Property
    Private _CSGTP As Double
    Public Property CSGTP() As Double
        Get
            Return _CSGTP
        End Get
        Set(ByVal value As Double)
            _CSGTP = value
        End Set
    End Property
    Private _CGSTAMT As Double
    Public Property CGSTAMT() As Double
        Get
            Return _CGSTAMT
        End Get
        Set(ByVal value As Double)
            _CGSTAMT = value
        End Set
    End Property
    Private _SGSTP As Double
    Public Property SGSTP() As Double
        Get
            Return _SGSTP
        End Get
        Set(ByVal value As Double)
            _SGSTP = value
        End Set
    End Property
    Private _SGSTAmt As Double
    Public Property SGSTAmt()
        Get
            Return _SGSTAmt
        End Get
        Set(ByVal value)
            _SGSTAmt = value
        End Set
    End Property
    Private _IGSTP As Double
    Public Property IGSTP()
        Get
            Return _IGSTP
        End Get
        Set(ByVal value)
            _IGSTP = value
        End Set
    End Property
    Private _IGSTAmt As Double
    Public Property IGSTAmt() As Double
        Get
            Return _IGSTAmt
        End Get
        Set(ByVal value As Double)
            _IGSTAmt = value
        End Set
    End Property
    Private _ItemDiscount As Double
    Public Property ItemDiscount() As Double
        Get
            Return _ItemDiscount
        End Get
        Set(ByVal value As Double)
            _ItemDiscount = value
        End Set
    End Property
    Private _DisP As Double
    Public Property DisP() As Double
        Get
            Return _DisP
        End Get
        Set(ByVal value As Double)
            _DisP = value
        End Set
    End Property
    Private _CessAmt As Double
    Public Property CessAmt() As Double
        Get
            Return _CessAmt
        End Get
        Set(ByVal value As Double)
            _CessAmt = value
        End Set
    End Property
    Private _withTax As Boolean
    Public Property withTax() As Boolean
        Get
            Return _withTax
        End Get
        Set(ByVal value As Boolean)
            _withTax = value
        End Set
    End Property
    Private _TaxType As Integer
    Public Property TaxType() As Integer
        Get
            Return _TaxType
        End Get
        Set(ByVal value As Integer)
            _TaxType = value
        End Set
    End Property
    Private _DocLstTxt As String
    Public Property DocLstTxt() As String
        Get
            Return _DocLstTxt
        End Get
        Set(ByVal value As String)
            _DocLstTxt = value
        End Set
    End Property
    Private _regularcessAmt As Double
    Public Property regularcessAmt() As Double
        Get
            Return _regularcessAmt
        End Get
        Set(ByVal value As Double)
            _regularcessAmt = value
        End Set
    End Property
    Private _FloodcessAmt As Double
    Public Property FloodcessAmt() As Double
        Get
            Return _FloodcessAmt
        End Get
        Set(ByVal value As Double)
            _FloodcessAmt = value
        End Set
    End Property
    Private _AdditionalcessAmt As Double
    Public Property AdditionalcessAmt() As Double
        Get
            Return _AdditionalcessAmt
        End Get
        Set(ByVal value As Double)
            _AdditionalcessAmt = value
        End Set
    End Property
    ' Fields
    Private _UnitDiscount As Double
    Public Property UnitDiscount() As Double
        Get
            Return _UnitDiscount
        End Get
        Set(ByVal value As Double)
            _UnitDiscount = value
        End Set
    End Property
    Private _lineCost As Double
    Public Property lineCost() As Double
        Get
            Return _lineCost
        End Get
        Set(ByVal value As Double)
            _lineCost = value
        End Set
    End Property
    Private _NetAmt As Double
    Public Property NetAmt() As Double
        Get
            Return _NetAmt
        End Get
        Set(ByVal value As Double)
            _NetAmt = value
        End Set
    End Property
#End Region
    Public Function _saveCmn() As String
        Dim param As String(,) = New String(27, 2) {{"@DocId", DocId, ""}, _
                                                {"@DocType", DocType, ""}, _
                                                {"@prefix", Prefix, ""}, _
                                                {"@DNo", DNo, ""}, _
                                                {"@Reference", (Trim(Reference) & ""), ""}, _
                                                {"@SlManId", (Trim(SlManId) & ""), ""}, _
                                                {"@CSCode", CSCode, ""}, _
                                                {"@DDate", DDate, "d"}, _
                                                {"@Comment", (Trim(Comment) & ""), ""}, _
                                                {"@UserId", (Trim(UserId) & ""), ""}, _
                                                {"@Discount", Discount, ""}, _
                                                {"@JobCode", JobCode, ""}, _
                                                {"@DocDefLoc", Trim(DocDefLoc & ""), ""}, _
                                                {"@MchName", Trim(MchName & ""), ""}, _
                                                {"@BrId", Trim(UsrBr & ""), ""}, _
                                                {"@FCRate", FCRate, ""}, _
                                                {"@NFraction", NFraction, ""}, _
                                                {"@FC", Trim(FC & ""), ""}, _
                                                {"@DueDt", DueDt, "d"}, _
                                                {"@Attn", Trim(Attn & ""), ""}, _
                                                {"@Subject", Trim(Subject & ""), ""}, _
                                                {"@FromJob", Trim(FromJob & ""), ""}, _
                                                {"@FromLoc", Trim(FromLoc & ""), ""}, _
                                                {"@withTax", withTax, ""}, _
                                                {"@rndoff", rndoff, ""}, _
                                                {"@DocLstTxt", Trim(DocLstTxt & ""), ""}, _
                                                {"@NetAmt", NetAmt, ""}, _
                                                {"@custAddress", Trim(custAddress & ""), ""}}
        'Return _cmnMthds._ExecuteScalar(param, "saveDocumentCmn", 28).ToString

        _ldlayer.saveDocTrTbBulk(dtDocTb, param, 28)
        Return 0
    End Function

    Public Sub _saveDetails()
        Dim param As String(,) = New String(29, 2) {{"@DocId", DocMId, ""}, _
                                                {"@ItemId", ItemId, ""}, _
                                                {"@Unit", Trim(Unit & ""), ""}, _
                                                {"@TrDetail", Trim(TrDetail & ""), ""}, _
                                                {"@Qty", Qty, ""}, _
                                                {"@CostPUnit", CostPUnit, ""}, _
                                                {"@SlNo", CInt(SlNo), ""}, _
                                                {"@PFraction", CInt(PFraction), ""}, _
                                                {"@Remark", Trim(Pack & ""), ""}, _
                                                {"@id", id, ""}, _
                                                {"@Mthd", Trim(Mthd & ""), ""}, _
                                                {"@ImpDocId", ImpDocId, ""}, _
                                                {"@ImpDocLnNo", ImpDocLnNo, ""}, _
                                                {"@taxP", taxP, ""}, _
                                                {"@taxAmt", taxAmt, ""}, _
                                                {"@HSNCode", Trim(HSNCode & ""), ""}, _
                                                {"@CSGTP", CSGTP, ""}, _
                                                {"@CGSTAMT", CGSTAMT, ""}, _
                                                {"@SGSTP", SGSTP, ""}, _
                                                {"@SGSTAmt", SGSTAmt, ""}, _
                                                {"@IGSTP", IGSTP, ""}, _
                                                {"@IGSTAmt", IGSTAmt, ""}, _
                                                {"@DisP", DisP, ""}, _
                                                {"@ItemDiscount", ItemDiscount, ""}, _
                                                {"@CessAmt", CessAmt, ""}, _
                                                {"@regularcessAmt", regularcessAmt, ""}, _
                                                {"@FloodcessAmt", FloodcessAmt, ""}, _
                                                {"@AdditionalcessAmt", AdditionalcessAmt, ""}, _
                                                {"@UnitDiscount", UnitDiscount, ""}, _
                                                {"@lineCost", lineCost, ""}}
        _cmnMthds._ExecuteNonQuery(param, "saveDocumentDetails", 30)
    End Sub

    Public Sub DocDelete()
        Dim param As String(,) = New String(0, 2) {{"@DocId", DocMId, ""}}
        _cmnMthds._ExecuteNonQuery(param, "DocCmnTbDelete", 1)
    End Sub

    Public Function returnDocumentItemsToListForm(ByVal cmdText As String, ByVal tp As Integer) As DataSet
        Dim param As String(,)
        If ((tp = 0) Or (tp = 1)) Then
            param = New String(1, 2) {{"@DocId", DocId, ""}, _
                                                   {"@tp", tp, ""}}
            Return _cmnMthds._ldDataset(param, cmdText, 2)
        End If
        param = New String(0, 2) {{"@DocId", DocId, ""}}
        Return _cmnMthds._ldDataset(param, cmdText, 1)
    End Function
    Public Function ldDoc(ByVal cmdText As String, Optional ByVal printno As Integer = 0) As DataSet
        Dim param As String(,) = New String(2, 2) {{"@prefix", Prefix, ""}, _
                                                   {"@invno", DNo, ""}, _
                                                   {"@TrType", DocType, ""}}
        Return _cmnMthds._ldDataset(param, cmdText, 3)
    End Function
End Class
