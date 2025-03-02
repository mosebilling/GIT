Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DataAccessLayer
Imports System.Data

Public Class clsInvoice
#Region "MasterTable Properties"
    Private _TrId As Long
    Private _TrType As String
    Private _Prefix As String
    Private _InvNo As Integer
    Private _PSAcc As Integer
    Private _TrRefNo As String
    Private _SlsManId As String
    Private _CSCode As Long
    Private _TrDate As DateTime
    Private _TrDescription As String
    Private _UserId As String
    Private _Discount As Single
    Private _OthCost As Single
    Private _Footer As Integer
    Private _DocLstTxt As String
    Private _AreaCode As String
    Private _EnaJob As Boolean
    Private _ImpDoc As Integer
    Private _IsFC As Boolean
    Private _FCRate As Single
    Private _NFraction As Integer
    Private _FC As String
    Private _BrId As String
    Private _TypeNo As Integer
    Private _JobCode As String
    Private _DocDefLoc As String
    Private _Discount1 As Single
    Private _CrtDt As DateTime
    Private _MchName As String
    Private _LPO As String
    Private _DocDate As DateTime
    Private _DelDate As DateTime
    Private _DueDate As DateTime
    Private _TermsId As String
    Private _isModi As Boolean
    Private _NetAmt As Double
    Private _SuppInvDate As DateTime
    Private _RetInvIds As String
    Private _lpoclass As String
    Private _rndoff As Double
    Private _OthrCust As String
    Public Property TrId() As Long
        Get
            Return _TrId
        End Get
        Set(ByVal value As Long)
            _TrId = value
        End Set
    End Property
    Public Property TrType() As String
        Get
            Return _TrType
        End Get
        Set(ByVal value As String)
            _TrType = value
        End Set
    End Property
    Public Property Prefix() As String
        Get
            Return _Prefix
        End Get
        Set(ByVal value As String)
            _Prefix = value
        End Set
    End Property
    Public Property InvNo() As Integer
        Get
            Return _InvNo
        End Get
        Set(ByVal value As Integer)
            _InvNo = value
        End Set
    End Property
    Public Property PSAcc() As Integer
        Get
            Return _PSAcc
        End Get
        Set(ByVal value As Integer)
            _PSAcc = value
        End Set
    End Property
    Public Property TrRefNo() As String
        Get
            Return _TrRefNo
        End Get
        Set(ByVal value As String)
            _TrRefNo = value
        End Set
    End Property
    Public Property SlsManId() As String
        Get
            Return _SlsManId
        End Get
        Set(ByVal value As String)
            _SlsManId = value
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
    Public Property TrDate() As DateTime
        Get
            Return _TrDate
        End Get
        Set(ByVal value As DateTime)
            _TrDate = value
        End Set
    End Property
    Public Property TrDescription() As String
        Get
            Return _TrDescription
        End Get
        Set(ByVal value As String)
            _TrDescription = value
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
    Public Property Discount() As Single
        Get
            Return _Discount
        End Get
        Set(ByVal value As Single)
            _Discount = value
        End Set
    End Property
    Public Property OthCost() As Single
        Get
            Return _OthCost
        End Get
        Set(ByVal value As Single)
            _OthCost = value
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
    Public Property DocLstTxt() As String
        Get
            Return _DocLstTxt
        End Get
        Set(ByVal value As String)
            _DocLstTxt = value
        End Set
    End Property
    Public Property AreaCode() As String
        Get
            Return _AreaCode
        End Get
        Set(ByVal value As String)
            _AreaCode = value
        End Set
    End Property
    Public Property EnaJob() As Boolean
        Get
            Return _EnaJob
        End Get
        Set(ByVal value As Boolean)
            _EnaJob = value
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
    Public Property IsFC() As Boolean
        Get
            Return _IsFC
        End Get
        Set(ByVal value As Boolean)
            _IsFC = value
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
    Public Property NFraction() As Integer
        Get
            Return _NFraction
        End Get
        Set(ByVal value As Integer)
            _NFraction = value
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
    Public Property BrId() As String
        Get
            Return _BrId
        End Get
        Set(ByVal value As String)
            _BrId = value
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
    Public Property JobCode() As String
        Get
            Return _JobCode
        End Get
        Set(ByVal value As String)
            _JobCode = value
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
    Public Property Discount1() As Single
        Get
            Return _Discount1
        End Get
        Set(ByVal value As Single)
            _Discount1 = value
        End Set
    End Property
    Public Property CrtDt() As DateTime
        Get
            Return _CrtDt
        End Get
        Set(ByVal value As DateTime)
            _CrtDt = value
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
    Public Property LPO() As String
        Get
            Return _LPO
        End Get
        Set(ByVal value As String)
            _LPO = value
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
    Public Property DelDate() As DateTime
        Get
            Return _DelDate
        End Get
        Set(ByVal value As DateTime)
            _DelDate = value
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
    Public Property TermsId() As String
        Get
            Return _TermsId
        End Get
        Set(ByVal value As String)
            _TermsId = value
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
    Public Property NetAmt() As Double
        Get
            Return _NetAmt
        End Get
        Set(ByVal value As Double)
            _NetAmt = value
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
    Public Property RetInvIds() As String
        Get
            Return _RetInvIds
        End Get
        Set(ByVal value As String)
            _RetInvIds = value
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
    Public Property rndoff() As Double
        Get
            Return _rndoff
        End Get
        Set(ByVal value As Double)
            _rndoff = value
        End Set
    End Property
    Public Property OthrCust() As String
        Get
            Return _OthrCust
        End Get
        Set(ByVal value As String)
            _OthrCust = value
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
    Private _disccardid As Integer
    Public Property disccardid() As Integer
        Get
            Return _disccardid
        End Get
        Set(ByVal value As Integer)
            _disccardid = value
        End Set
    End Property
    Private _TotOut As Double
    Public Property TotOut() As Double
        Get
            Return _TotOut
        End Get
        Set(ByVal value As Double)
            _TotOut = value
        End Set
    End Property
    Private _TotProduction As Double
    Public Property TotProduction() As Double
        Get
            Return _TotProduction
        End Get
        Set(ByVal value As Double)
            _TotProduction = value
        End Set
    End Property
    Private _isTaxInvoice As Boolean
    Public Property isTaxInvoice() As Boolean
        Get
            Return _isTaxInvoice
        End Get
        Set(ByVal value As Boolean)
            _isTaxInvoice = value
        End Set
    End Property
    Private _vhclDetails As String
    Public Property vhclDetails() As String
        Get
            Return _vhclDetails
        End Get
        Set(ByVal value As String)
            _vhclDetails = value
        End Set
    End Property
    Private _isImportOrExport As Integer
    Public Property isImportOrExport() As Integer
        Get
            Return _isImportOrExport
        End Get
        Set(ByVal value As Integer)
            _isImportOrExport = value
        End Set
    End Property
    Private _isthroughcustoms As Integer
    Public Property isthroughcustoms() As Integer
        Get
            Return _isthroughcustoms
        End Get
        Set(ByVal value As Integer)
            _isthroughcustoms = value
        End Set
    End Property
    Private _cap1 As String
    Public Property cap1() As String
        Get
            Return _cap1
        End Get
        Set(ByVal value As String)
            _cap1 = value
        End Set
    End Property
    Private _cap2 As String
    Public Property cap2() As String
        Get
            Return _cap2
        End Get
        Set(ByVal value As String)
            _cap2 = value
        End Set
    End Property
    Private _cap3 As String
    Public Property cap3() As String
        Get
            Return _cap3
        End Get
        Set(ByVal value As String)
            _cap3 = value
        End Set
    End Property
    Private _cap4 As String
    Public Property cap4() As String
        Get
            Return _cap4
        End Get
        Set(ByVal value As String)
            _cap4 = value
        End Set
    End Property
    Private _cap5 As String
    Public Property cap5() As String
        Get
            Return _cap5
        End Get
        Set(ByVal value As String)
            _cap5 = value
        End Set
    End Property
    Private _info1 As String
    Public Property info1() As String
        Get
            Return _info1
        End Get
        Set(ByVal value As String)
            _info1 = value
        End Set
    End Property
    Private _info2 As String
    Public Property info2() As String
        Get
            Return _info2
        End Get
        Set(ByVal value As String)
            _info2 = value
        End Set
    End Property
    Private _info3 As String
    Public Property info3() As String
        Get
            Return _info3
        End Get
        Set(ByVal value As String)
            _info3 = value
        End Set
    End Property
    Private _info4 As String
    Public Property info4() As String
        Get
            Return _info4
        End Get
        Set(ByVal value As String)
            _info4 = value
        End Set
    End Property
    Private _info5 As String
    Public Property info5() As String
        Get
            Return _info5
        End Get
        Set(ByVal value As String)
            _info5 = value
        End Set
    End Property
    Private _priceType As Integer
    Public Property priceType() As Integer
        Get
            Return _priceType
        End Get
        Set(ByVal value As Integer)
            _priceType = value
        End Set
    End Property
    Private _GSTN As String
    Public Property GSTN() As String
        Get
            Return _GSTN
        End Get
        Set(ByVal value As String)
            _GSTN = value
        End Set
    End Property
    Private _taxwithoutLineDiscount As Integer
    Public Property taxwithoutLineDiscount() As Integer
        Get
            Return _taxwithoutLineDiscount
        End Get
        Set(ByVal value As Integer)
            _taxwithoutLineDiscount = value
        End Set
    End Property
    Private _CashCustName As String
    Public Property CashCustName() As String
        Get
            Return _CashCustName
        End Get
        Set(ByVal value As String)
            _CashCustName = value
        End Set
    End Property
    Private _deliveredBy As String
    Public Property deliveredBy() As String
        Get
            Return _deliveredBy
        End Get
        Set(ByVal value As String)
            _deliveredBy = value
        End Set
    End Property
    Private _customerPhone As String
    Public Property customerPhone() As String
        Get
            Return _customerPhone
        End Get
        Set(ByVal value As String)
            _customerPhone = value
        End Set
    End Property
    Private _tenderd As Double
    Public Property tenderd() As Double
        Get
            Return _tenderd
        End Get
        Set(ByVal value As Double)
            _tenderd = value
        End Set
    End Property
    Private _ccustid As Long
    Public Property ccustid() As Long
        Get
            Return _ccustid
        End Get
        Set(ByVal value As Long)
            _ccustid = value
        End Set
    End Property
    Private _CashCustid As Long
    Public Property CashCustid() As Long
        Get
            Return _CashCustid
        End Get
        Set(ByVal value As Long)
            _CashCustid = value
        End Set
    End Property


    Private _turnurid As Integer
    Public Property turnurid() As Integer
        Get
            Return _turnurid
        End Get
        Set(ByVal value As Integer)
            _turnurid = value
        End Set
    End Property
    Private _isB2B As Integer
    Public Property isB2B() As Integer
        Get
            Return _isB2B
        End Get
        Set(ByVal value As Integer)
            _isB2B = value
        End Set
    End Property
    Private _tunername As String
    Public Property tunername() As String
        Get
            Return _tunername
        End Get
        Set(ByVal value As String)
            _tunername = value
        End Set
    End Property
    Private _MembershipRenewalUpdate As String
    Public Property MembershipRenewalUpdate() As String
        Get
            Return _MembershipRenewalUpdate
        End Get
        Set(ByVal value As String)
            _MembershipRenewalUpdate = value
        End Set
    End Property
    Private _prdebit As String
    Public Property prdebit() As String
        Get
            Return _prdebit
        End Get
        Set(ByVal value As String)
            _prdebit = value
        End Set
    End Property
    Private _ModiDt As Date
    Public Property ModiDt() As Date
        Get
            Return _ModiDt
        End Get
        Set(ByVal value As Date)
            _ModiDt = value
        End Set
    End Property
    Private _cashAmount As Double
    Public Property cashAmount() As Double
        Get
            Return _cashAmount
        End Get
        Set(ByVal value As Double)
            _cashAmount = value
        End Set
    End Property
    Private _CardAmount As Double
    Public Property CardAmount() As Double
        Get
            Return _CardAmount
        End Get
        Set(ByVal value As Double)
            _CardAmount = value
        End Set
    End Property
    Private _poschange As Double
    Public Property poschange() As Double
        Get
            Return _poschange
        End Get
        Set(ByVal value As Double)
            _poschange = value
        End Set
    End Property
    Private _grossTotal As Double
    Public Property grossTotal() As Double
        Get
            Return _grossTotal
        End Get
        Set(ByVal value As Double)
            _grossTotal = value
        End Set
    End Property
    Private _gstamt As Double
    Public Property gstamt() As Double
        Get
            Return _gstamt
        End Get
        Set(ByVal value As Double)
            _gstamt = value
        End Set
    End Property
#End Region
#Region "DetailTable Properties"
    Private _dtTrId As Long
    Private _ItemId As Long
    Private _SlNo As Integer
    Private _TrQty As Double
    Private _Unit As String
    Private _UnitCost As Double
    Private _IDescription As String
    Private _UnitOthCost As Double
    Private _Method As String
    Private _UnitDiscount As Double
    Private _PFraction As Integer
    Private _TrTypeNo As Single
    Private _TrDateNo As Integer
    Private _id As Long
    Private _isdateChanged As Boolean
    Private _WarrentyName As String
    Private _SerialNo As String
    Private _WarrentyExpDate As Date
    Private _taxP As Double
    Private _taxAmt As Double
    Private _DisP As Double
    Private _ItemDiscount As Double
    Public _Smancode As String
    Public Property Smancode() As String
        Get
            Return _Smancode
        End Get
        Set(ByVal value As String)
            _Smancode = value
        End Set
    End Property
    Public Property ItemDiscount() As Double
        Get
            Return _ItemDiscount
        End Get
        Set(ByVal value As Double)
            _ItemDiscount = value
        End Set
    End Property
    Public Property DisP() As Double
        Get
            Return _DisP
        End Get
        Set(ByVal value As Double)
            _DisP = value
        End Set
    End Property
    Public Property dtTrId() As Long
        Get
            Return _dtTrId
        End Get
        Set(ByVal value As Long)
            _dtTrId = value
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
    Public Property ItemId() As Long
        Get
            Return _ItemId
        End Get
        Set(ByVal value As Long)
            _ItemId = value
        End Set
    End Property
    Public Property SlNo() As Integer
        Get
            Return _SlNo
        End Get
        Set(ByVal value As Integer)
            _SlNo = value
        End Set
    End Property
    Public Property TrQty() As Double
        Get
            Return _TrQty
        End Get
        Set(ByVal value As Double)
            _TrQty = value
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
    Public Property UnitCost() As Double
        Get
            Return _UnitCost
        End Get
        Set(ByVal value As Double)
            _UnitCost = value
        End Set
    End Property
    Private _Mrp As Double
    Public Property MRP() As Double
        Get
            Return _Mrp
        End Get
        Set(ByVal value As Double)
            _Mrp = value
        End Set
    End Property
    Public Property PFraction() As Integer
        Get
            Return _PFraction
        End Get
        Set(ByVal value As Integer)
            _PFraction = value
        End Set
    End Property
    Public Property IDescription() As String
        Get
            Return _IDescription
        End Get
        Set(ByVal value As String)
            _IDescription = value
        End Set
    End Property
    Public Property UnitOthCost() As Double
        Get
            Return _UnitOthCost
        End Get
        Set(ByVal value As Double)
            _UnitOthCost = value
        End Set
    End Property

    Public Property Method() As String
        Get
            Return _Method
        End Get
        Set(ByVal value As String)
            _Method = value
        End Set
    End Property

    Public Property UnitDiscount() As Double
        Get
            Return _UnitDiscount
        End Get
        Set(ByVal value As Double)
            _UnitDiscount = value
        End Set
    End Property

    Public Property TrTypeNo() As Single
        Get
            Return _TrTypeNo
        End Get
        Set(ByVal value As Single)
            _TrTypeNo = value
        End Set
    End Property

    Public Property WarrentyExpDate() As Date
        Get
            Return _WarrentyExpDate
        End Get
        Set(ByVal value As Date)
            _WarrentyExpDate = value
        End Set
    End Property

    Public Property TrDateNo() As Integer
        Get
            Return _TrDateNo
        End Get
        Set(ByVal value As Integer)
            _TrDateNo = value
        End Set
    End Property
    Public Property isdateChanged() As Boolean
        Get
            Return _isdateChanged
        End Get
        Set(ByVal value As Boolean)
            _isdateChanged = value
        End Set
    End Property
    Public Property WarrentyName() As String
        Get
            Return _WarrentyName
        End Get
        Set(ByVal value As String)
            _WarrentyName = value
        End Set
    End Property
    Public Property SerialNo() As String
        Get
            Return _SerialNo
        End Get
        Set(ByVal value As String)
            _SerialNo = value
        End Set
    End Property
    Public Property taxP() As Double
        Get
            Return _taxP
        End Get
        Set(ByVal value As Double)
            _taxP = value
        End Set
    End Property
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
    Private _InvTypeNo As Integer
    Public Property InvTypeNo() As Integer
        Get
            Return _InvTypeNo
        End Get
        Set(ByVal value As Integer)
            _InvTypeNo = value
        End Set
    End Property
    Private _StartingReading As Double
    Public Property StartingReading() As Double
        Get
            Return _StartingReading
        End Get
        Set(ByVal value As Double)
            _StartingReading = value
        End Set
    End Property
    Private _EndingReading As Double
    Public Property EndingReading() As Double
        Get
            Return _EndingReading
        End Get
        Set(ByVal value As Double)
            _EndingReading = value
        End Set
    End Property
    Private _MeterCode As String
    Public Property MeterCode() As String
        Get
            Return _MeterCode
        End Get
        Set(ByVal value As String)
            _MeterCode = value
        End Set
    End Property
    Private _MINslno As Integer
    Public Property MINslno()
        Get
            Return _MINslno
        End Get
        Set(ByVal value)
            _MINslno = value
        End Set
    End Property
    Private _impDocSlno As Integer
    Public Property impDocSlno() As Integer
        Get
            Return _impDocSlno
        End Get
        Set(ByVal value As Integer)
            _impDocSlno = value
        End Set
    End Property
    Private _impDocid As Long
    Public Property impDocid() As Long
        Get
            Return _impDocid
        End Get
        Set(ByVal value As Long)
            _impDocid = value
        End Set
    End Property
    Private _WoodNetQty As Double
    Public Property WoodNetQty() As Double
        Get
            Return _WoodNetQty
        End Get
        Set(ByVal value As Double)
            _WoodNetQty = value
        End Set
    End Property
    Private _WoodDiscountQty As Double
    Public Property WoodDiscountQty() As Double
        Get
            Return _WoodDiscountQty
        End Get
        Set(ByVal value As Double)
            _WoodDiscountQty = value
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
    Private _manufacturingdate As Date
    Public Property manufacturingdate() As Date
        Get
            Return _manufacturingdate
        End Get
        Set(ByVal value As Date)
            _manufacturingdate = value
        End Set
    End Property
    Private _SP1 As Double
    Public Property SP1() As Double
        Get
            Return _SP1
        End Get
        Set(ByVal value As Double)
            _SP1 = value
        End Set
    End Property
    Private _SP2 As Double
    Public Property SP2() As Double
        Get
            Return _SP2
        End Get
        Set(ByVal value As Double)
            _SP2 = value
        End Set
    End Property
    Private _SP3 As Double 'WS PRICE
    Public Property SP3() As Double
        Get
            Return _SP3
        End Get
        Set(ByVal value As Double)
            _SP3 = value
        End Set
    End Property
    Private _itemcost As Double
    Public Property itemcost() As Double
        Get
            Return _itemcost
        End Get
        Set(ByVal value As Double)
            _itemcost = value
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
    Private _focqty As Double
    Public Property focqty() As Double
        Get
            Return _focqty
        End Get
        Set(ByVal value As Double)
            _focqty = value
        End Set
    End Property
#End Region
#Region "Warrenty Transaction Properties"
    Private _WTrid As Long
    Private _ExpDate As Date
    Private _BillNo As String
    Private _Cust As String
    Public Property WTrid() As Long
        Get
            Return _WTrid
        End Get
        Set(ByVal value As Long)
            _WTrid = value
        End Set
    End Property
    Public Property ExpDate() As Date
        Get
            Return _ExpDate
        End Get
        Set(ByVal value As Date)
            _ExpDate = value
        End Set
    End Property
    Public Property BillNo() As String
        Get
            Return _BillNo
        End Get
        Set(ByVal value As String)
            _BillNo = value
        End Set
    End Property
    Public Property Cust() As String
        Get
            Return _Cust
        End Get
        Set(ByVal value As String)
            _Cust = value
        End Set
    End Property

#End Region
    Private _cmnDlink As New cl_CmnDataLink()
    Private _cmnMthds As New CmnMethods()
    Private _ldlayer As New Dlayer
    Public Function _saveCmn() As String
        Dim _id As String
        Dim param As String(,) = New String(67, 2) {{"@TrId", TrId, ""}, _
                                            {"@TrDate", Format(TrDate, "yyyy/MM/dd"), "d"}, _
                                            {"@TrType", TrType, ""}, _
                                            {"@PreFix", Trim(Prefix & ""), ""}, _
                                            {"@InvNo", InvNo, ""}, _
                                            {"@CSCode", CSCode, ""}, _
                                            {"@PSAcc", PSAcc, ""}, _
                                            {"@TrRefNo", Trim(TrRefNo & ""), ""}, _
                                            {"@TrDescription", Convert.ToString(TrDescription) & "", ""}, _
                                            {"@UserId", Convert.ToString(UserId) & "", ""}, _
                                            {"@Discount", Discount, ""}, _
                                            {"@OthCost", OthCost, ""}, _
                                            {"@Footer", Convert.ToString(Footer) & "", ""}, _
                                            {"@DocLstTxt", Convert.ToString(DocLstTxt) & "", ""}, _
                                            {"@SlsManId", Convert.ToString(SlsManId) & "", ""}, _
                                            {"@AreaCode", Convert.ToString(AreaCode) & "", ""}, _
                                            {"@JobCode", Convert.ToString(JobCode) & "", ""}, _
                                            {"@EnaJob", EnaJob, ""}, _
                                            {"@ImpDoc", ImpDoc, ""}, _
                                            {"@IsFC", IsFC, ""}, _
                                            {"@FCRate", FCRate, ""}, _
                                            {"@NFraction", NFraction, ""}, _
                                            {"@FC", Convert.ToString(FC) & "", ""}, _
                                            {"@BrId", Convert.ToString(UsrBr) & "", ""}, _
                                            {"@TypeNo", TypeNo, ""}, _
                                            {"@DocDefLoc", Convert.ToString(DocDefLoc) & "", ""}, _
                                            {"@Discount1", Discount1, ""}, _
                                            {"@MchName", Convert.ToString(MchName) & "", ""}, _
                                            {"@LPO", Convert.ToString(LPO) & "", ""}, _
                                            {"@DocDate", Format(DateValue(DocDate), "yyyy/MM/dd"), "d"}, _
                                            {"@DelDate", Format(DateValue(DelDate), "yyyy/MM/dd"), "d"}, _
                                            {"@DueDate", Format(DateValue(DueDate), "yyyy/MM/dd"), "d"}, _
                                            {"@SuppInvDate", Format(DateValue(SuppInvDate), "yyyy/MM/dd"), "d"}, _
                                            {"@CrtDt", Format(DateValue(CrtDt), "yyyy/MM/dd"), "d"}, _
                                            {"@TermsId", Convert.ToString(TermsId) & "", ""}, _
                                            {"@isModi", isModi, ""}, _
                                            {"@NetAmt", NetAmt, ""}, _
                                            {"@RetInvIds", Val(RetInvIds & ""), ""}, _
                                            {"@lpoclass", Convert.ToString(lpoclass) & "", ""}, _
                                            {"@rndoff", rndoff, ""}, _
                                            {"@OthrCust", Trim(OthrCust & ""), ""}, _
                                            {"@InvTypeNo", Val(InvTypeNo & ""), ""}, _
                                            {"@TaxType", Val(TaxType & ""), ""}, _
                                            {"@isTaxInvoice", isTaxInvoice, ""}, _
                                            {"@isImportOrExport", Val(isImportOrExport & ""), ""}, _
                                            {"@isthroughcustoms", Val(isthroughcustoms & ""), ""}, _
                                            {"@cap1", Trim(cap1 & ""), ""}, _
                                            {"@cap2", Trim(cap2 & ""), ""}, _
                                            {"@cap3", Trim(cap3 & ""), ""}, _
                                            {"@cap4", Trim(cap4 & ""), ""}, _
                                            {"@cap5", Trim(cap5 & ""), ""}, _
                                            {"@info1", Trim(info1 & ""), ""}, _
                                            {"@info2", Trim(info2 & ""), ""}, _
                                            {"@info3", Trim(info3 & ""), ""}, _
                                            {"@info4", Trim(info4 & ""), ""}, _
                                            {"@info5", Trim(info5 & ""), ""}, _
                                            {"@priceType", Val(priceType & ""), ""}, _
                                            {"@GSTN", Trim(GSTN & ""), ""}, _
                                            {"@taxwithoutLineDiscount", taxwithoutLineDiscount, ""}, _
                                            {"@CashCustName", Trim(CashCustName & ""), ""}, _
                                            {"@deliveredBy", Trim(deliveredBy & ""), ""}, _
                                            {"@customerPhone", Trim(customerPhone & ""), ""}, _
                                            {"@tenderd", tenderd, ""}, _
                                            {"@ccustid", ccustid, ""}, _
                                            {"@CashCustid", CashCustid, ""}, _
                                            {"@isB2B", isB2B, ""}, _
                                            {"@prdebit", Val(prdebit & ""), ""}, _
                                            {"@tunername", Trim(tunername & ""), ""}}
        Dim recordNo As Object
        recordNo = _cmnMthds._ExecuteScalar(param, "ItmInvCmnTb_SAVEorEdit", 68)
        'recordNo = _ldlayer.ExecuteScalar(param, "ItmInvCmnTb_SAVEorEdit", 57)
        _id = recordNo.ToString()
        Return _id
    End Function
#Region "Pos Properties"
    Private _TeTrid As Long
    Public Property TeTrid() As Long
        Get
            Return _TeTrid
        End Get
        Set(ByVal value As Long)
            _TeTrid = value
        End Set
    End Property
    Private _Tid As Long
    Public Property Tid() As Long
        Get
            Return _Tid
        End Get
        Set(ByVal value As Long)
            _Tid = value
        End Set
    End Property
    Private _TenderedAmt As Double
    Public Property TenderedAmt() As Double
        Get
            Return _TenderedAmt
        End Get
        Set(ByVal value As Double)
            _TenderedAmt = value
        End Set
    End Property
    Private _ChangeAmt As Double
    Public Property ChangeAmt() As Double
        Get
            Return _ChangeAmt
        End Get
        Set(ByVal value As Double)
            _ChangeAmt = value
        End Set
    End Property

#End Region
    Public Sub saveTenderedAmt()
        Dim param As String(,) = New String(3, 2) {{"@Tid", Tid, ""}, _
                                            {"@TeTrid", TeTrid, ""}, _
                                            {"TenderedAmt", TenderedAmt, ""}, _
                                            {"ChangeAmt", ChangeAmt, ""}}
        _cmnMthds._ExecuteNonQuery(param, "saveTenderedAmt", 31)
    End Sub
#Region "CASH Customer"
    Private _custid As Long
    Public Property custid() As Long
        Get
            Return _custid
        End Get
        Set(ByVal value As Long)
            _custid = value
        End Set
    End Property
    Private _CustName As String
    Public Property CustName() As String
        Get
            Return _CustName
        End Get
        Set(ByVal value As String)
            _CustName = value
        End Set
    End Property
    Private _Add1 As String
    Public Property Add1() As String
        Get
            Return _Add1
        End Get
        Set(ByVal value As String)
            _Add1 = value
        End Set
    End Property
    Private _Add2 As String
    Public Property Add2() As String
        Get
            Return _Add2
        End Get
        Set(ByVal value As String)
            _Add2 = value
        End Set
    End Property
    Private _Add3 As String
    Public Property Add3() As String
        Get
            Return _Add3
        End Get
        Set(ByVal value As String)
            _Add3 = value
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
    Private _email As String
    Public Property email() As String
        Get
            Return _email
        End Get
        Set(ByVal value As String)
            _email = value
        End Set
    End Property
    Private _salesPoints As Integer
    Public Property salesPoints() As Integer
        Get
            Return _salesPoints
        End Get
        Set(ByVal value As Integer)
            _salesPoints = value
        End Set
    End Property
    Private _GiftVrNo As String
    Public Property GiftVrNo() As String
        Get
            Return _GiftVrNo
        End Get
        Set(ByVal value As String)
            _GiftVrNo = value
        End Set
    End Property
    Private _Memberid As String
    Public Property Memberid() As String
        Get
            Return _Memberid
        End Get
        Set(ByVal value As String)
            _Memberid = value
        End Set
    End Property
    Private _Remarks As String
    Public Property Remarks() As String
        Get
            Return _Remarks
        End Get
        Set(ByVal value As String)
            _Remarks = value
        End Set
    End Property
    Private _customeraccount As Long
    Public Property customeraccount() As Long
        Get
            Return _customeraccount
        End Get
        Set(ByVal value As Long)
            _customeraccount = value
        End Set
    End Property
    Private _issupp As Boolean
    Public Property issupp() As Boolean
        Get
            Return _issupp
        End Get
        Set(ByVal value As Boolean)
            _issupp = value
        End Set
    End Property



    Public Function saveCashCustomer() As Long
        Dim param As String(,) = New String(13, 2) {{"@custid", custid, ""}, _
                                                    {"@CustName", CustName, ""}, _
                                                    {"@Add1", Add1, ""}, _
                                                    {"@Add2", Add2, ""}, _
                                                    {"@Add3", Add3, ""}, _
                                                    {"@Phone", Phone, ""}, _
                                                    {"@email", Trim(email & ""), ""}, _
                                                    {"@salesPoints", salesPoints, ""}, _
                                                    {"@GiftVrNo", Trim(GiftVrNo & ""), ""}, _
                                                    {"@Memberid", Memberid, ""}, _
                                                    {"@Remarks", Remarks, ""}, _
                                                    {"@customeraccount", customeraccount, ""}, _
                                                    {"@issupp", issupp, ""}, _
                                                    {"@GSTN", GSTN, ""}}
        Return _cmnMthds._ExecuteScalar(param, "saveCashCustomer", 14)
    End Function
#End Region


    Public Function _saveDetails(Optional ByVal ispos As Boolean = False) As Long
        Dim param As String(,) = New String(53, 2) {{"@TrId", dtTrId, ""}, _
                                                    {"@ItemId", ItemId, ""}, _
                                                    {"@SlNo", Convert.ToString(SlNo) & "", ""}, _
                                                    {"@TrQty", TrQty, ""}, _
                                                    {"@Unit", Convert.ToString(Unit) & "", ""}, _
                                                    {"@UnitCost", UnitCost, ""}, _
                                                    {"@IDescription", Convert.ToString(IDescription) & "", ""}, _
                                                    {"@UnitOthCost", UnitOthCost, ""}, _
                                                    {"@Method", Trim(Method & ""), ""}, _
                                                    {"@UnitDiscount", UnitDiscount, ""}, _
                                                    {"@PFraction", PFraction, ""}, _
                                                    {"@TrTypeNo", TrTypeNo, ""}, _
                                                    {"@TrDateNo", TrDateNo, ""}, _
                                                    {"@TrType", TrType, ""}, _
                                                    {"@id", id, ""}, _
                                                    {"@isdateChanged", isdateChanged, ""}, _
                                                    {"@WarrentyName", Trim(WarrentyName & ""), ""}, _
                                                    {"@SerialNo", SerialNo, ""}, _
                                                    {"@WarrentyExpDate", WarrentyExpDate, "d"}, _
                                                    {"@jbitmDetid", 0, ""}, _
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
                                                    {"@Smancode", Trim(Smancode & ""), ""}, _
                                                    {"@StartingReading", StartingReading, ""}, _
                                                    {"@EndingReading", EndingReading, ""}, _
                                                    {"@MeterCode", Trim(MeterCode & ""), ""}, _
                                                    {"@MINslno", Trim(MINslno & ""), ""}, _
                                                    {"@disccardid", Trim(disccardid & ""), ""}, _
                                                    {"@TotProduction", TotProduction, ""}, _
                                                    {"@TotOut", TotOut, ""}, _
                                                    {"@impDocid", impDocid, ""}, _
                                                    {"@impDocSlno", impDocSlno, ""}, _
                                                    {"@WoodNetQty", WoodNetQty, ""}, _
                                                    {"@WoodDiscountQty", WoodDiscountQty, ""}, _
                                                    {"@CessAmt", CessAmt, ""}, _
                                                    {"@manufacturingdate", manufacturingdate, "d"}, _
                                                    {"@SP1", SP1, ""}, _
                                                    {"@SP2", SP2, ""}, _
                                                    {"@SP3", SP3, ""}, _
                                                    {"@mrp", MRP, ""}, _
                                                    {"@itemcost", itemcost, ""}, _
                                                    {"@regularcessAmt", regularcessAmt, ""}, _
                                                    {"@FloodcessAmt", FloodcessAmt, ""}, _
                                                    {"@AdditionalcessAmt", AdditionalcessAmt, ""}, _
                                                    {"@focqty", focqty, ""}}
        Dim idDet As Long
        If ispos Then
            Dim recordNo As Object = Nothing
            recordNo = _cmnMthds._ExecuteScalar(param, "POSItemDetails_SAVE", 31)
            idDet = recordNo.ToString()
        Else
            '_cmnMthds._ExecuteNonQuery(param, "ItmInvTrTb_SAVE", 54)
            _ldlayer.ExecuteNonQuery(param, "ItmInvTrTb_SAVE", 54)
            idDet = 0
        End If
        Return idDet
    End Function
    Public Sub closeDlayerConnection()
        _ldlayer.closeCon()
    End Sub
    Public Function _savePOSDetails(Optional ByVal ispos As Boolean = False) As Long
        Dim param As String(,) = New String(36, 2) {{"@TrId", dtTrId, ""}, _
                                                    {"@ItemId", ItemId, ""}, _
                                                    {"@SlNo", Convert.ToString(SlNo) & "", ""}, _
                                                    {"@TrQty", TrQty, ""}, _
                                                    {"@Unit", Convert.ToString(Unit) & "", ""}, _
                                                    {"@UnitCost", UnitCost, ""}, _
                                                    {"@IDescription", Convert.ToString(IDescription) & "", ""}, _
                                                    {"@UnitOthCost", UnitOthCost, ""}, _
                                                    {"@Method", Trim(Method & ""), ""}, _
                                                    {"@UnitDiscount", UnitDiscount, ""}, _
                                                    {"@PFraction", PFraction, ""}, _
                                                    {"@TrTypeNo", TrTypeNo, ""}, _
                                                    {"@TrDateNo", TrDateNo, ""}, _
                                                    {"@TrType", TrType, ""}, _
                                                    {"@id", id, ""}, _
                                                    {"@isdateChanged", isdateChanged, ""}, _
                                                    {"@WarrentyName", Trim(WarrentyName & ""), ""}, _
                                                    {"@SerialNo", SerialNo, ""}, _
                                                    {"@WarrentyExpDate", WarrentyExpDate, "d"}, _
                                                    {"@jbitmDetid", 0, ""}, _
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
                                                    {"@Mrp", MRP, ""}, _
                                                    {"@CessAmt", CessAmt, ""}, _
                                                    {"@itemcost", itemcost, ""}, _
                                                    {"@regularcessAmt", regularcessAmt, ""}, _
                                                    {"@FloodcessAmt", FloodcessAmt, ""}, _
                                                    {"@AdditionalcessAmt", AdditionalcessAmt, ""}}
        Dim idDet As Long
        Dim recordNo As Object = Nothing
        recordNo = _cmnMthds._ExecuteScalar(param, "POSItemDetails_SAVE", 37)
        idDet = recordNo.ToString()
        Return idDet
    End Function
    Public Function ldInvoice(ByVal cmdText As String, Optional ByVal printno As Integer = 0) As DataSet
        Dim param As String(,) = New String(3, 2) {{"@prefix", Prefix, ""}, _
                                                   {"@invno", InvNo, ""}, _
                                                   {"@TrType", TrType, ""}, _
                                                   {"@printno", printno, ""}}
        Return _cmnMthds._ldDataset(param, cmdText, 4)
    End Function

    Public Sub setcostAverageOnModification(ByVal brid As String)
        Dim param As String(,) = New String(2, 2) {{"@Itemid", ItemId, ""}, _
                                                  {"@Trdate", TrDate, "d"}, _
                                                  {"@brid", BrId, ""}}
        _cmnMthds._ExecuteNonQuery(param, "setcostAverageOnModification", 3)
    End Sub
    Public Sub deleteInventoryTransactions()
        Dim param As String(,) = New String(2, 2) {{"@TrId", TrId, ""}, _
                                                  {"@TrType", TrType, ""}, _
                                                  {"@Userid", CurrentUser, ""}}
        _cmnMthds._ExecuteNonQuery(param, "deleteInventoryTransactions", 3)
    End Sub
    Public Function returnSerialNoDetails(ByVal isdual As Integer) As DataSet
        Dim param As String(,) = New String(1, 2) {{"@SerialNo", SerialNo, ""}, _
                                                   {"@isdual", isdual, ""}}
        Return _cmnMthds._ldDataset(param, "returnSerialNoDetails", 2)
    End Function
    Public Sub saveWarrenty()
        Dim param As String(,) = New String(7, 2) {{"@WTrid", WTrid, ""}, _
                                                  {"@ExpDate", ExpDate, "d"}, _
                                                  {"@Cust", Cust, ""}, _
                                                  {"@SaleDate", TrDate, "d"}, _
                                                  {"@BillNo", BillNo, ""}, _
                                                  {"@SerialNo", SerialNo, ""}, _
                                                  {"@WarrentyName", WarrentyName, ""}, _
                                                  {"@custid", custid, ""}}
        _cmnMthds._ExecuteNonQuery(param, "saveWarrenty", 8)
    End Sub
    Public Function returnWarrentyTransaction() As DataTable
        Dim param As String(,) = New String(0, 2) {{"@SerialNo", SerialNo, ""}}
        Return _cmnMthds._ldDataset(param, "returnWarrentyTransaction", 1).Tables(0)
    End Function
    Public Sub deleteInventoryRelatedItemDetails(ByVal TrId As Long)
        Dim param As String(,) = New String(0, 2) {{"@trid", TrId, ""}}
        _cmnMthds._ExecuteNonQuery(param, "deleteInventoryRelatedItemDetails", 1)
    End Sub
    Public Function returnProfitanalysisGrid(ByVal dateFrom As Date, ByVal dateTo As Date, ByVal tp As Integer) As DataSet
        Dim param As String(,) = New String(3, 2) {{"@Trdate1", dateFrom, "d"}, _
                                                   {"@Trdate2", dateTo, "d"}, _
                                                   {"@tp", tp, ""}, _
                                                   {"@brid", UsrBr, ""}}
        Return _cmnMthds._ldDataset(param, "returnProfitanalysisGrid", 4)
    End Function

    Public Function saveJobsalesInvoice() As String
        Dim param As String(,) = New String(17, 2) {{"@jbInvid", TrId, ""}, _
                                                   {"@TrType", TrType, ""}, {"@jbInvNo", InvNo, ""}, _
                                                   {"@PreFix", Trim(Prefix & ""), ""}, _
                                                   {"@JobCode", JobCode, ""}, _
                                                   {"@TrDate", Format(TrDate, "yyyy/MM/dd"), "d"}, _
                                                   {"@RefNo", Trim(TrRefNo & ""), ""}, _
                                                   {"@TrDescription", Trim(TrDescription & ""), ""}, _
                                                   {"@custid", CSCode, ""}, _
                                                   {"@Discount", Discount, ""}, _
                                                   {"@UserId", Trim(UserId & ""), ""}, _
                                                   {"@MchName", Trim(MchName & ""), ""}, _
                                                   {"@PSAcc", PSAcc, ""}, _
                                                   {"@OthrCust", Trim(OthrCust & ""), ""}, _
                                                   {"@TaxType", Val(TaxType & ""), ""}, _
                                                   {"@isTaxInv", isTaxInvoice, ""}, _
                                                   {"@vhclDetails", vhclDetails, ""}, _
                                                   {"@InvTypeNo", InvTypeNo, ""}}
        Return _cmnMthds._ExecuteScalar(param, "saveJobsalesInvoice", 18)
    End Function
    Public Sub savejobInvTrTb()
        Dim param As String(,) = New String(26, 2) {{"@TrId", dtTrId, ""}, _
                                                    {"@ItemId", ItemId, ""}, _
                                                    {"@SlNo", SlNo, ""}, _
                                                    {"@TrQty", TrQty, ""}, _
                                                    {"@Unit", Trim(Unit & ""), ""}, _
                                                    {"@UnitCost", UnitCost, ""}, _
                                                    {"@IDescription", Trim(IDescription & ""), ""}, _
                                                    {"@UnitOthCost", UnitOthCost, ""}, _
                                                    {"@Method", Trim(Method & ""), ""}, _
                                                    {"@UnitDiscount", UnitDiscount, ""}, _
                                                    {"@PFraction", PFraction, ""}, _
                                                    {"@TrTypeNo", TrTypeNo, ""}, _
                                                    {"@TrDateNo", TrDateNo, ""}, _
                                                    {"@TrType", TrType, ""}, _
                                                    {"@id", id, ""}, _
                                                    {"@taxP", taxP, ""}, _
                                                    {"@taxAmt", taxAmt, ""}, _
                                                    {"@HSNCode", Trim(HSNCode & ""), ""}, _
                                                    {"@CSGTP", CSGTP, ""}, _
                                                    {"@CGSTAMT", CGSTAMT, ""}, _
                                                    {"@SGSTP", SGSTP, ""}, _
                                                    {"@SGSTAmt", SGSTAmt, ""}, _
                                                    {"@IGSTP", IGSTP, ""}, _
                                                    {"@IGSTAmt", IGSTAmt, ""}, _
                                                    {"@impDocid", impDocid, ""}, _
                                                    {"@impDocSlno", impDocSlno, ""}, _
                                                    {"@CessAmt", CessAmt, ""}}
        _cmnMthds._ExecuteNonQuery(param, "savejobInvTrTb", 27)
    End Sub

    Public Function returnTaxReport(ByVal dateFrom As Date, ByVal dateTo As Date, ByVal tp As Integer, ByVal TrType As String, ByVal iswithgstn As Integer, _
                                    Optional ByVal taxP As Double = 0, Optional ByVal isb2b As Integer = 0) As DataSet
        Dim param As String(,) = New String(6, 2) {{"@Trdate1", dateFrom, "d"}, _
                                                   {"@Trdate2", dateTo, "d"}, _
                                                   {"@trtype", TrType, ""}, _
                                                   {"@iswithgstn", iswithgstn, ""}, _
                                                   {"@taxP", taxP, ""}, _
                                                   {"@tp", tp, ""}, _
                                                   {"@isb2b", isb2b, ""}}
        Return _cmnMthds._ldDataset(param, "returnTaxReport", 7)
    End Function
    Public Function returnBatchQty(ByVal itemid As Long, ByVal batchno As String) As Double
        Dim param As String(,) = New String(1, 2) {{"@itemid", itemid, ""}, _
                                                   {"@batchno", Trim(batchno & ""), ""}}
        Dim dt As DataTable = _cmnMthds._ldDataset(param, "returnBatchQty", 2).Tables(0)
        If dt.Rows.Count > 0 Then
            Return CDbl(dt(0)(0))
        End If
    End Function
    Public Function returnUAEVatReport(ByVal dateFrom As Date, ByVal dateTo As Date) As DataTable
        Dim param As String(,) = New String(2, 2) {{"@datefrom", dateFrom, "d"}, _
                                                   {"@dateto", dateTo, "d"}, _
                                                   {"@brid", UsrBr, ""}}
        Return _cmnMthds._ldDataset(param, "returnvat201", 3).Tables(0)
    End Function
    Public Function returnOutputTax(ByVal dateFrom As Date, ByVal dateTo As Date, ByVal tp As Integer) As DataSet
        Dim param As String(,) = New String(2, 2) {{"@dateFrom", dateFrom, "d"}, _
                                                   {"@dateTo", dateTo, "d"}, _
                                                   {"@tp", tp, ""}}
        Return _cmnMthds._ldDataset(param, "returnOutputTax", 3)
    End Function
    Public Sub savebulktoInvTr(ByVal dt As DataTable)
        '_ldlayer.saveBulk(dt)
        _ldlayer.saveItmInvTrTbBulk(dt)
    End Sub
    Public Sub returnTrCostForRefresh(ByVal id As Long)
        Dim param As String(,) = New String(0, 2) {{"@id", id, ""}}
        _cmnMthds._ExecuteNonQuery(param, "returnTrCostForRefresh", 1)
    End Sub
End Class
