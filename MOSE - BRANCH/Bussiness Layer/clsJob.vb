Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DataAccessLayer
Imports System.Data
Public Class clsJob
#Region "Accessories Properties"
    Private _Alid As Long
    Private _AccessoriesName As String
    Public Property Alid() As Long
        Get
            Return _Alid
        End Get
        Set(ByVal value As Long)
            _Alid = value
        End Set
    End Property
    Public Property AccessoriesName() As String
        Get
            Return _AccessoriesName
        End Get
        Set(ByVal value As String)
            _AccessoriesName = value
        End Set
    End Property
    
#End Region
#Region "Job Properties"
    Private _Jobid As Long
    Private _jobcode As String
    Private _jobdate As Date
    Private _jobname As String
    Private _JobDescription As String
    Private _custcode As Long
    Private _EstimatedDate As Date
    Private _EstimatedAmt As Double
    Private _TechRemarks As String
    Private _ObserverdBy As String
    Private _Technician As String
    Private _SIID As Long
    Private _RvId As Long
    Private _ServiceCost As Double
    Private _ItemCost As Double
    Private _Userid As String
    Private _CancelWarrenty As Boolean
    Private _CancelDate As Date
    Private _isParkAndSale As Boolean
    Private _ModiDate As Date
    Public Property Jobid() As Long
        Get
            Return _Jobid
        End Get
        Set(ByVal value As Long)
            _Jobid = value
        End Set
    End Property
    Public Property jobcode() As String
        Get
            Return _jobcode
        End Get
        Set(ByVal value As String)
            _jobcode = value
        End Set
    End Property
    Public Property jobdate() As Date
        Get
            Return _jobdate
        End Get
        Set(ByVal value As Date)
            _jobdate = value
        End Set
    End Property
    Public Property jobname() As String
        Get
            Return _jobname
        End Get
        Set(ByVal value As String)
            _jobname = value
        End Set
    End Property
    Public Property JobDescription() As String
        Get
            Return _JobDescription
        End Get
        Set(ByVal value As String)
            _JobDescription = value
        End Set
    End Property
    Public Property custcode() As Long
        Get
            Return _custcode
        End Get
        Set(ByVal value As Long)
            _custcode = value
        End Set
    End Property
  
    Public Property EstimatedDate() As Date
        Get
            Return _EstimatedDate
        End Get
        Set(ByVal value As Date)
            _EstimatedDate = value
        End Set
    End Property
    Public Property EstimatedAmt() As Double
        Get
            Return _EstimatedAmt
        End Get
        Set(ByVal value As Double)
            _EstimatedAmt = value
        End Set
    End Property

    Public Property Technician() As String
        Get
            Return _Technician
        End Get
        Set(ByVal value As String)
            _Technician = value
        End Set
    End Property
    Public Property SIID() As Long
        Get
            Return _SIID
        End Get
        Set(ByVal value As Long)
            _SIID = value
        End Set
    End Property
    Public Property RvId() As Long
        Get
            Return _RvId
        End Get
        Set(ByVal value As Long)
            _RvId = value
        End Set
    End Property
    Public Property ServiceCost() As Double
        Get
            Return _ServiceCost
        End Get
        Set(ByVal value As Double)
            _ServiceCost = value
        End Set
    End Property
    Public Property ItemCost() As Double
        Get
            Return _ItemCost
        End Get
        Set(ByVal value As Double)
            _ItemCost = value
        End Set
    End Property
    
    Public Property Userid() As String
        Get
            Return _Userid
        End Get
        Set(ByVal value As String)
            _Userid = value
        End Set
    End Property
    Public Property CancelWarrenty() As Boolean
        Get
            Return _CancelWarrenty
        End Get
        Set(ByVal value As Boolean)
            _CancelWarrenty = value
        End Set
    End Property
    Public Property CancelDate() As Date
        Get
            Return _CancelDate
        End Get
        Set(ByVal value As Date)
            _CancelDate = value
        End Set
    End Property
    Public Property isParkAndSale() As Boolean
        Get
            Return _isParkAndSale
        End Get
        Set(ByVal value As Boolean)
            _isParkAndSale = value
        End Set
    End Property
    Private _lastKM As String
    Public Property lastKM() As String
        Get
            Return _lastKM
        End Get
        Set(ByVal value As String)
            _lastKM = value
        End Set
    End Property
    Private _supcode As Long
    Public Property supcode() As Long
        Get
            Return _supcode
        End Get
        Set(ByVal value As Long)
            _supcode = value
        End Set
    End Property
    Public Property ModiDate() As Date
        Get
            Return _ModiDate
        End Get
        Set(ByVal value As Date)
            _ModiDate = value
        End Set
    End Property
#End Region
#Region "Search Properties"
    Private _DateFrom As Date
    Private _DateTo As Date
    Private _Tp As Integer
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
    Public Property Tp() As Integer
        Get
            Return _Tp
        End Get
        Set(ByVal value As Integer)
            _Tp = value
        End Set
    End Property
#End Region
#Region "JobItem Properties"
    Private _jbitmId As Long
    Private _Itemid As Long
    Private _Qty As Double
    Private _Uprice As Double
    Private _trDtno As Integer
    Private _SlNo As Integer
    Private _Trid As Long
    Private _Taxp As Double
    Private _taxAmt As Double
    Private _LabourCost As Double
    Private _Remark As String


    Public Property jbitmId() As Long
        Get
            Return _jbitmId
        End Get
        Set(ByVal value As Long)
            _jbitmId = value
        End Set
    End Property
    Public Property Itemid() As Long
        Get
            Return _Itemid
        End Get
        Set(ByVal value As Long)
            _Itemid = value
        End Set
    End Property
    Public Property Qty() As Double
        Get
            Return _Qty
        End Get
        Set(ByVal value As Double)
            _Qty = value
        End Set
    End Property
    Public Property Uprice() As Double
        Get
            Return _Uprice
        End Get
        Set(ByVal value As Double)
            _Uprice = value
        End Set
    End Property
    Public Property trDtno() As Integer
        Get
            Return _trDtno
        End Get
        Set(ByVal value As Integer)
            _trDtno = value
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
    Public Property Trid() As Long
        Get
            Return _Trid
        End Get
        Set(ByVal value As Long)
            _Trid = value
        End Set
    End Property
    Public Property TaxP() As Double
        Get
            Return _Taxp
        End Get
        Set(ByVal value As Double)
            _Taxp = value
        End Set
    End Property
    Public Property TaxAmt() As Double
        Get
            Return _taxAmt
        End Get
        Set(ByVal value As Double)
            _taxAmt = value
        End Set
    End Property
    Public Property LabourCost() As Double
        Get
            Return _LabourCost
        End Get
        Set(ByVal value As Double)
            _LabourCost = value
        End Set
    End Property
    Private _cgstPer As Double
    Public Property cgstPer() As Double
        Get
            Return _cgstPer
        End Get
        Set(ByVal value As Double)
            _cgstPer = value
        End Set
    End Property
    Private _sgstPer As Double
    Public Property sgstPer() As Double
        Get
            Return _sgstPer
        End Get
        Set(ByVal value As Double)
            _sgstPer = value
        End Set
    End Property
    Private _jbDescription As String
    Public Property jbDescription() As String
        Get
            Return _jbDescription
        End Get
        Set(ByVal value As String)
            _jbDescription = value
        End Set
    End Property
    Private _itmDescription As String
    Public Property itmDescription() As String
        Get
            Return _itmDescription
        End Get
        Set(ByVal value As String)
            _itmDescription = value
        End Set
    End Property
    Private _hsnCode As String
    Public Property hsnCode() As String
        Get
            Return _hsnCode
        End Get
        Set(ByVal value As String)
            _hsnCode = value
        End Set
    End Property
    Private _pFraction As Integer
    Public Property pFraction() As Integer
        Get
            Return _pFraction
        End Get
        Set(ByVal value As Integer)
            _pFraction = value
        End Set
    End Property
    Private _fuelStatus As Integer
    Public Property fuelStatus() As Integer
        Get
            Return _fuelStatus
        End Get
        Set(ByVal value As Integer)
            _fuelStatus = value
        End Set
    End Property
    Private _kilometer As String
    Public Property kilometer() As String
        Get
            Return _kilometer
        End Get
        Set(ByVal value As String)
            _kilometer = value
        End Set
    End Property
    Private _accessoriesRemark As String
    Public Property accessoriesRemark() As String
        Get
            Return _accessoriesRemark
        End Get
        Set(ByVal value As String)
            _accessoriesRemark = value
        End Set
    End Property
    Private _carid As Long
    Public Property carid() As Long
        Get
            Return _carid
        End Get
        Set(ByVal value As Long)
            _carid = value
        End Set
    End Property
    Private _noofMats As Integer
    Public Property noofMats() As Integer
        Get
            Return _noofMats
        End Get
        Set(ByVal value As Integer)
            _noofMats = value
        End Set
    End Property
    Private _noofMudFlap As Integer
    Public Property noofMudFlap() As Integer
        Get
            Return _noofMudFlap
        End Get
        Set(ByVal value As Integer)
            _noofMudFlap = value
        End Set
    End Property
    Private _noofwheelcap As Integer
    Public Property noofwheelcap() As Integer
        Get
            Return _noofwheelcap
        End Get
        Set(ByVal value As Integer)
            _noofwheelcap = value
        End Set
    End Property
    Private _noofSpeekers As Integer
    Public Property noofSpeekers() As Integer
        Get
            Return _noofSpeekers
        End Get
        Set(ByVal value As Integer)
            _noofSpeekers = value
        End Set
    End Property
    Private _jobdiscount As Double
    Public Property jobdiscount() As Double
        Get
            Return _jobdiscount
        End Get
        Set(ByVal value As Double)
            _jobdiscount = value
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
    Private _unitdiscount As Double
    Public Property unitdiscount() As Double
        Get
            Return _unitdiscount
        End Get
        Set(ByVal value As Double)
            _unitdiscount = value
        End Set
    End Property
#End Region
#Region "Custruction Properties"
    Private _constructionid As Long
    Private _jobtype As String
    Private _Startdate As Date
    Private _QtnAmt As Double
    Private _OpnCost As Double
    Private _OpnIncome As Double
    Private _SiteLocation As String
    Private _PlotNo As String
    Private _Pjobid As Long
    Private _SiteAddress As String
    Public Property constructionid() As Long
        Get
            Return _constructionid
        End Get
        Set(ByVal value As Long)
            _constructionid = value
        End Set
    End Property
    Public Property jobtype() As String
        Get
            Return _jobtype
        End Get
        Set(ByVal value As String)
            _jobtype = value
        End Set
    End Property
    Public Property Startdate() As Date
        Get
            Return _Startdate
        End Get
        Set(ByVal value As Date)
            _Startdate = value
        End Set
    End Property
    Public Property QtnAmt() As Double
        Get
            Return _QtnAmt
        End Get
        Set(ByVal value As Double)
            _QtnAmt = value
        End Set
    End Property
    Public Property OpnCost() As Double
        Get
            Return _OpnCost
        End Get
        Set(ByVal value As Double)
            _OpnCost = value
        End Set
    End Property
    Public Property OpnIncome() As Double
        Get
            Return _OpnIncome
        End Get
        Set(ByVal value As Double)
            _OpnIncome = value
        End Set
    End Property
    Public Property SiteLocation() As String
        Get
            Return _SiteLocation
        End Get
        Set(ByVal value As String)
            _SiteLocation = value
        End Set
    End Property
    Public Property PlotNo() As String
        Get
            Return _PlotNo
        End Get
        Set(ByVal value As String)
            _PlotNo = value
        End Set
    End Property
    Public Property Pjobid() As String
        Get
            Return _Pjobid
        End Get
        Set(ByVal value As String)
            _Pjobid = value
        End Set
    End Property
    Public Property SiteAddress() As String
        Get
            Return _SiteAddress
        End Get
        Set(ByVal value As String)
            _SiteAddress = value
        End Set
    End Property
#End Region
#Region "JobImeiNolisttb"
    Private _jobimeiId As Long
    Private _IMEI As String
    Private _Model As String
    Private _bt As Integer
    Private _cv As Integer
    Private _bx As Integer
    Private _mc As Integer
    Private _withWarrenty As Boolean
    Private _CWarrenty As Boolean
    Private _Observation As String
    Public Property jobimeiId() As Long
        Get
            Return _jobimeiId
        End Get
        Set(ByVal value As Long)
            _jobimeiId = value
        End Set
    End Property
    Public Property IMEI() As String
        Get
            Return _IMEI
        End Get
        Set(ByVal value As String)
            _IMEI = value
        End Set
    End Property
    Dim _Closed As String
    Public Property Closed() As String
        Get
            Return _Closed
        End Get
        Set(ByVal value As String)
            _Closed = value
        End Set
    End Property
    Public Property Model() As String
        Get
            Return _Model
        End Get
        Set(ByVal value As String)
            _Model = value
        End Set
    End Property
    Public Property bt() As Integer
        Get
            Return _bt
        End Get
        Set(ByVal value As Integer)
            _bt = value
        End Set
    End Property
    Public Property cv() As Integer
        Get
            Return _cv
        End Get
        Set(ByVal value As Integer)
            _cv = value
        End Set
    End Property
    Public Property mc() As Integer
        Get
            Return _mc
        End Get
        Set(ByVal value As Integer)
            _mc = value
        End Set
    End Property
    Public Property bx() As Integer
        Get
            Return _bx
        End Get
        Set(ByVal value As Integer)
            _bx = value
        End Set
    End Property
    Private _pen As Integer
    Public Property Pen() As Integer
        Get
            Return _pen
        End Get
        Set(ByVal value As Integer)
            _pen = value
        End Set
    End Property
    Public Property withWarrenty() As Boolean
        Get
            Return _withWarrenty
        End Get
        Set(ByVal value As Boolean)
            _withWarrenty = value
        End Set
    End Property
    Public Property CWarrenty() As Boolean
        Get
            Return _CWarrenty
        End Get
        Set(ByVal value As Boolean)
            _CWarrenty = value
        End Set
    End Property
    Public Property Observation() As String
        Get
            Return _Observation
        End Get
        Set(ByVal value As String)
            _Observation = value
        End Set
    End Property
    Public Property TechRemarks() As String
        Get
            Return _TechRemarks
        End Get
        Set(ByVal value As String)
            _TechRemarks = value
        End Set
    End Property
#End Region
#Region "WorkProgress Properties"
    Private _wprogressid As Long
    Private _endate As Date
    Private _actualAmount As Double
    Public Property wprogressid() As Long
        Get
            Return _wprogressid
        End Get
        Set(ByVal value As Long)
            _wprogressid = value
        End Set
    End Property
    Public Property endate() As Date
        Get
            Return _endate
        End Get
        Set(ByVal value As Date)
            _endate = value
        End Set
    End Property
    Public Property actualAmount() As Double
        Get
            Return _actualAmount
        End Get
        Set(ByVal value As Double)
            _actualAmount = value
        End Set
    End Property
#End Region
#Region "LodgeProperties"
    Private _ldgdescription As String
    Public Property ldgdescription() As String
        Get
            Return _ldgdescription
        End Get
        Set(ByVal value As String)
            _ldgdescription = value
        End Set
    End Property
    Private _noOfGust As Integer
    Public Property noOfGust() As Integer
        Get
            Return _noOfGust
        End Get
        Set(ByVal value As Integer)
            _noOfGust = value
        End Set
    End Property
    Private _malegusts As Integer
    Public Property malegusts() As Integer
        Get
            Return _malegusts
        End Get
        Set(ByVal value As Integer)
            _malegusts = value
        End Set
    End Property
    Private _femalegusts As Integer
    Public Property femalegusts() As Integer
        Get
            Return _femalegusts
        End Get
        Set(ByVal value As Integer)
            _femalegusts = value
        End Set
    End Property
    Private _noofKids As Integer
    Public Property noofKids() As Integer
        Get
            Return _noofKids
        End Get
        Set(ByVal value As Integer)
            _noofKids = value
        End Set
    End Property
    Private _Dtype As String
    Public Property Dtype() As String
        Get
            Return _Dtype
        End Get
        Set(ByVal value As String)
            _Dtype = value
        End Set
    End Property

    Private _identityProof As String
    Public Property identityProof() As String
        Get
            Return _identityProof
        End Get
        Set(ByVal value As String)
            _identityProof = value
        End Set
    End Property
    Private _identityProofNumber As String
    Public Property identityProofNumber() As String
        Get
            Return _identityProofNumber
        End Get
        Set(ByVal value As String)
            _identityProofNumber = value
        End Set
    End Property
    Private _vehicleDetails As String
    Public Property vehicleDetails() As String
        Get
            Return _vehicleDetails
        End Get
        Set(ByVal value As String)
            _vehicleDetails = value
        End Set
    End Property
    Private _ldgroomid As Long
    Public Property ldgroomid() As Long
        Get
            Return _ldgroomid
        End Get
        Set(ByVal value As Long)
            _ldgroomid = value
        End Set
    End Property
    Private _roomItemid As Long
    Public Property roomItemid() As Long
        Get
            Return _roomItemid
        End Get
        Set(ByVal value As Long)
            _roomItemid = value
        End Set
    End Property
    Private _remarks As String
    Public Property remarks() As String
        Get
            Return _remarks
        End Get
        Set(ByVal value As String)
            _remarks = value
        End Set
    End Property
    Private _checkinDateTime As Date
    Public Property checkinDateTime() As Date
        Get
            Return _checkinDateTime
        End Get
        Set(ByVal value As Date)
            _checkinDateTime = value
        End Set
    End Property
    Private _checkoutEstimateDateTime As Date
    Public Property checkoutEstimateDateTime() As Date
        Get
            Return _checkoutEstimateDateTime
        End Get
        Set(ByVal value As Date)
            _checkoutEstimateDateTime = value
        End Set
    End Property
    Private _ldgServiceId As Long
    Public Property ldgServiceId() As Long
        Get
            Return _ldgServiceId
        End Get
        Set(ByVal value As Long)
            _ldgServiceId = value
        End Set
    End Property
    Private _ldgServiceItemid As Long
    Public Property ldgServiceItemid() As Long
        Get
            Return _ldgServiceItemid
        End Get
        Set(ByVal value As Long)
            _ldgServiceItemid = value
        End Set
    End Property
    Private _serviceDateTime As Date
    Public Property serviceDateTime() As Date
        Get
            Return _serviceDateTime
        End Get
        Set(ByVal value As Date)
            _serviceDateTime = value
        End Set
    End Property
    Private _unitprice As Double
    Public Property unitprice() As Double
        Get
            Return _unitprice
        End Get
        Set(ByVal value As Double)
            _unitprice = value
        End Set
    End Property
    Private _checkoutDateTime As Date
    Public Property checkoutDateTime() As Date
        Get
            Return _checkoutDateTime
        End Get
        Set(ByVal value As Date)
            _checkoutDateTime = value
        End Set
    End Property
    Private _roomcategory As String
    Public Property roomcategory() As String
        Get
            Return _roomcategory
        End Get
        Set(ByVal value As String)
            _roomcategory = value
        End Set
    End Property
    Private _rent As Double
    Public Property rent() As Double
        Get
            Return _rent
        End Get
        Set(ByVal value As Double)
            _rent = value
        End Set
    End Property
    Private _gst As Double
    Public Property gst() As Double
        Get
            Return _gst
        End Get
        Set(ByVal value As Double)
            _gst = value
        End Set
    End Property
    Private _cess As Double
    Public Property cess() As Double
        Get
            Return _cess
        End Get
        Set(ByVal value As Double)
            _cess = value
        End Set
    End Property
    Private _roomstatus As Integer
    Public Property roomstatus() As Integer
        Get
            Return _roomstatus
        End Get
        Set(ByVal value As Integer)
            _roomstatus = value
        End Set
    End Property
    Private _BookingRef As String
    Public Property BookingRef() As String
        Get
            Return _BookingRef
        End Get
        Set(ByVal value As String)
            _BookingRef = value
        End Set
    End Property
    Private _bookingRawid As Long
    Public Property bookingRawid() As Long
        Get
            Return _bookingRawid
        End Get
        Set(ByVal value As Long)
            _bookingRawid = value
        End Set
    End Property
#End Region
#Region "GarrageComplaints"
    Private _repairsId As Long
    Public Property repairsId() As Long
        Get
            Return _repairsId
        End Get
        Set(ByVal value As Long)
            _repairsId = value
        End Set
    End Property
    Private _complaints As String
    Public Property complaints() As String
        Get
            Return _complaints
        End Get
        Set(ByVal value As String)
            _complaints = value
        End Set
    End Property
    Private _instrucations As String
    Public Property instrucations() As String
        Get
            Return _instrucations
        End Get
        Set(ByVal value As String)
            _instrucations = value
        End Set
    End Property
    Private _completed As Integer
    Public Property completed() As Integer
        Get
            Return _completed
        End Get
        Set(ByVal value As Integer)
            _completed = value
        End Set
    End Property
#End Region
#Region "Membership Properties"
    Private _membershipid As Long
    Public Property membershipid() As Long
        Get
            Return _membershipid
        End Get
        Set(ByVal value As Long)
            _membershipid = value
        End Set
    End Property
    Private _age As Integer
    Public Property age() As Integer
        Get
            Return _age
        End Get
        Set(ByVal value As Integer)
            _age = value
        End Set
    End Property
    Private _gender As Integer
    Public Property gender() As Integer
        Get
            Return _gender
        End Get
        Set(ByVal value As Integer)
            _gender = value
        End Set
    End Property
    Private _imagename As String
    Public Property imagename() As String
        Get
            Return _imagename
        End Get
        Set(ByVal value As String)
            _imagename = value
        End Set
    End Property
    '**********
    Private _renewid As Long
    Public Property renewid() As Long
        Get
            Return _renewid
        End Get
        Set(ByVal value As Long)
            _renewid = value
        End Set
    End Property
    Private _moccupation As String
    Public Property moccupation() As String
        Get
            Return _moccupation
        End Get
        Set(ByVal value As String)
            _moccupation = value
        End Set
    End Property
    Private _mmedicalcondition As String
    Public Property mmedicalcondition() As String
        Get
            Return _mmedicalcondition
        End Get
        Set(ByVal value As String)
            _mmedicalcondition = value
        End Set
    End Property
    Private _emergencynumber As String
    Public Property emergencynumber() As String
        Get
            Return _emergencynumber
        End Get
        Set(ByVal value As String)
            _emergencynumber = value
        End Set
    End Property
    Private _contactname As String
    Public Property contactname() As String
        Get
            Return _contactname
        End Get
        Set(ByVal value As String)
            _contactname = value
        End Set
    End Property
#End Region
#Region "SchoolManagement"
    Private _studentid As Long
    Public Property studentid() As Long
        Get
            Return _studentid
        End Get
        Set(ByVal value As Long)
            _studentid = value
        End Set
    End Property
    Private _studentname As String
    Public Property studentname() As String
        Get
            Return _studentname
        End Get
        Set(ByVal value As String)
            _studentname = value
        End Set
    End Property
    Private _admissionno As String
    Public Property admissionno() As String
        Get
            Return _admissionno
        End Get
        Set(ByVal value As String)
            _admissionno = value
        End Set
    End Property
    Private _admissiondate As Date
    Public Property admissiondate() As Date
        Get
            Return _admissiondate
        End Get
        Set(ByVal value As Date)
            _admissiondate = value
        End Set
    End Property
    Private _standered As String
    Public Property standered() As String
        Get
            Return _standered
        End Get
        Set(ByVal value As String)
            _standered = value
        End Set
    End Property
    Private _section As String
    Public Property section() As String
        Get
            Return _section
        End Get
        Set(ByVal value As String)
            _section = value
        End Set
    End Property
    Private _rollnumber As String
    Public Property rollnumber() As String
        Get
            Return _rollnumber
        End Get
        Set(ByVal value As String)
            _rollnumber = value
        End Set
    End Property
    Private _dateofbirth As Date
    Public Property dateofbirth() As Date
        Get
            Return _dateofbirth
        End Get
        Set(ByVal value As Date)
            _dateofbirth = value
        End Set
    End Property
    Private _stphone As String
    Public Property stphone() As String
        Get
            Return _stphone
        End Get
        Set(ByVal value As String)
            _stphone = value
        End Set
    End Property
    Private _studentemail As String
    Public Property studentemail() As String
        Get
            Return _studentemail
        End Get
        Set(ByVal value As String)
            _studentemail = value
        End Set
    End Property
    Private _stadd1 As String
    Public Property stadd1() As String
        Get
            Return _stadd1
        End Get
        Set(ByVal value As String)
            _stadd1 = value
        End Set
    End Property
    Private _stadd2 As String
    Public Property stadd2() As String
        Get
            Return _stadd2
        End Get
        Set(ByVal value As String)
            _stadd2 = value
        End Set
    End Property
    Private _stadd3 As String
    Public Property stadd3() As String
        Get
            Return _stadd3
        End Get
        Set(ByVal value As String)
            _stadd3 = value
        End Set
    End Property
    Private _religion As String
    Public Property religion() As String
        Get
            Return _religion
        End Get
        Set(ByVal value As String)
            _religion = value
        End Set
    End Property
    Private _studentcast As String
    Public Property studentcast() As String
        Get
            Return _studentcast
        End Get
        Set(ByVal value As String)
            _studentcast = value
        End Set
    End Property
    Private _bloodgroup As String
    Public Property bloodgroup() As String
        Get
            Return _bloodgroup
        End Get
        Set(ByVal value As String)
            _bloodgroup = value
        End Set
    End Property
    Private _fathername As String
    Public Property fathername() As String
        Get
            Return _fathername
        End Get
        Set(ByVal value As String)
            _fathername = value
        End Set
    End Property
    Private _mothername As String
    Public Property mothername() As String
        Get
            Return _mothername
        End Get
        Set(ByVal value As String)
            _mothername = value
        End Set
    End Property
    Private _fatheroccupation As String
    Public Property fatheroccupation() As String
        Get
            Return _fatheroccupation
        End Get
        Set(ByVal value As String)
            _fatheroccupation = value
        End Set
    End Property
    Private _motheroccupation As String
    Public Property motheroccupation() As String
        Get
            Return _motheroccupation
        End Get
        Set(ByVal value As String)
            _motheroccupation = value
        End Set
    End Property
    Private _motherphonenumber As String
    Public Property motherphonenumber() As String
        Get
            Return _motherphonenumber
        End Get
        Set(ByVal value As String)
            _motherphonenumber = value
        End Set
    End Property
    Private _studentstatus As String
    Public Property studentstatus() As String
        Get
            Return _studentstatus
        End Get
        Set(ByVal value As String)
            _studentstatus = value
        End Set
    End Property
    Private _classteacherid As Long
    Public Property classteacherid() As Long
        Get
            Return _classteacherid
        End Get
        Set(ByVal value As Long)
            _classteacherid = value
        End Set
    End Property
    Private _admissionfees As Double
    Public Property admissionfees() As Double
        Get
            Return _admissionfees
        End Get
        Set(ByVal value As Double)
            _admissionfees = value
        End Set
    End Property
    Private _accountno As Long
    Public Property AccountNo() As Long
        Get
            Return _accountno
        End Get
        Set(ByVal value As Long)
            _accountno = value
        End Set
    End Property
    Private _S1Accid As Long
    Public Property S1Accid() As Long
        Get
            Return _S1Accid
        End Get
        Set(ByVal value As Long)
            _S1Accid = value
        End Set
    End Property
    Private _OpnBal As Double
    Public Property OpnBal() As Double
        Get
            Return _OpnBal
        End Get
        Set(ByVal value As Double)
            _OpnBal = value
        End Set
    End Property
    Public Property remark() As String
        Get
            Return _Remark
        End Get
        Set(ByVal value As String)
            _Remark = value
        End Set
    End Property
    Private _passportno As String
    Public Property passportno() As String
        Get
            Return _passportno
        End Get
        Set(ByVal value As String)
            _passportno = value
        End Set
    End Property
    Private _emiratesid As String
    Public Property emiratesid() As String
        Get
            Return _emiratesid
        End Get
        Set(ByVal value As String)
            _emiratesid = value
        End Set
    End Property
    Private _nationality As String
    Public Property nationality() As String
        Get
            Return _nationality
        End Get
        Set(ByVal value As String)
            _nationality = value
        End Set
    End Property
#End Region


    Private _cmnMthds As New CmnMethods()
    Private _dlayer As New Dlayer
    Public Sub saveAccessoriesTb()
        Dim param As String(,) = New String(1, 2) {{"@AccId ", Alid, ""}, _
                                                  {"@AccessoriesName", AccessoriesName, ""}}
        _cmnMthds._ExecuteNonQuery(param, "saveAccessoriesTb", 2)
    End Sub

    Public Function saveJob() As Long
        Dim param As String(,) = New String(16, 2) {{"@Jobid ", Jobid, ""}, _
                                                    {"@jobcode", jobcode, ""}, _
                                                    {"@jobdate ", jobdate, "d"}, _
                                                    {"@jobname", jobname, ""}, _
                                                    {"@JobDescription ", Trim(JobDescription & ""), ""}, _
                                                    {"@custcode", custcode, ""}, _
                                                    {"@EstimatedDate", EstimatedDate, "d"}, _
                                                    {"@EstimatedAmt ", EstimatedAmt, ""}, _
                                                    {"@Technician ", Trim(Technician & ""), ""}, _
                                                    {"@SIID", SIID, ""}, _
                                                    {"@RvId ", RvId, ""}, _
                                                    {"@ServiceCost", ServiceCost, ""}, _
                                                    {"@ItemCost ", ItemCost, ""}, _
                                                    {"@Userid", Userid, ""}, _
                                                    {"@LabourCost", LabourCost, ""}, _
                                                    {"@jobdiscount", jobdiscount, ""}, _
                                                    {"@NetAmt", NetAmt, ""}}

        Return Val(_cmnMthds._ExecuteScalar(param, "saveJob", 17).ToString())
    End Function
    
    Public Sub saveJobReceivedAccessoriesTb()
        Dim param As String(,) = New String(1, 2) {{"@Jobid ", Jobid, ""}, _
                                                  {"@Accessories", AccessoriesName, ""}}
        _cmnMthds._ExecuteNonQuery(param, "saveJobReceivedAccessoriesTb", 2)
    End Sub
    Public Function returnJob() As DataSet
        If Trim(Closed & "") = "" Then Closed = "No"
        Dim param As String(,) = New String(6, 2) {{"@Jobid", Jobid, ""}, _
                                                   {"@DateFrom", DateFrom, "d"}, _
                                                   {"@DateTo", DateTo, "d"}, _
                                                   {"@custcode", custcode, ""}, _
                                                   {"@IMEINo ", Trim(IMEI & ""), ""}, _
                                                   {"@Closed ", Trim(Closed & ""), ""}, _
                                                   {"@Tp", Tp, ""}}
        Return _cmnMthds._ldDataset(param, "returnJob", 7)
    End Function
    Public Function returnLodge() As DataSet
        If Trim(Closed & "") = "" Then Closed = "No"
        Dim param As String(,) = New String(6, 2) {{"@Jobid", Jobid, ""}, _
                                                   {"@DateFrom", DateFrom, "d"}, _
                                                   {"@DateTo", DateTo, "d"}, _
                                                   {"@custcode", custcode, ""}, _
                                                   {"@Closed ", Trim(Closed & ""), ""}, _
                                                   {"@Tp", Tp, ""}, _
                                                   {"@Dtype", Dtype, ""}}
        Return _cmnMthds._ldDataset(param, "returnLodgeDetails", 7)
    End Function
    Public Function returnLodgeCheckInlistForAudit() As DataSet
        Dim param As String(,) = New String(1, 2) {{"@DateFrom", DateFrom, "d"}, _
                                                   {"@DateTo", DateTo, "d"}}
        Return _cmnMthds._ldDataset(param, "returnLodgeCheckInlistForAudit", 2)
    End Function
    
    Public Sub saveJobItemTb()
        Dim param As String(,) = New String(17, 2) {{"@jbitmId", jbitmId, ""}, _
                                                  {"@Itemid", Itemid, ""}, _
                                                  {"@jbid", Jobid, ""}, _
                                                  {"@Qty", Qty, ""}, _
                                                  {"@Uprice", Uprice, ""}, _
                                                  {"@trDtno", trDtno, ""}, _
                                                  {"@TrId", Trid, ""}, _
                                                  {"@SlNo", SlNo, ""}, _
                                                  {"@taxp", TaxP, ""}, _
                                                  {"@taxAmt", TaxAmt, ""}, _
                                                  {"@cgstPer", cgstPer, ""}, _
                                                  {"@sgstPer", sgstPer, ""}, _
                                                  {"@jbDescription", Trim(jbDescription & ""), ""}, _
                                                  {"@itmDescription", Trim(itmDescription & ""), ""}, _
                                                  {"@hsnCode", Trim(hsnCode & ""), ""}, _
                                                  {"@pFraction", pFraction, ""}, _
                                                  {"@jbimslno", SlNo, ""}, _
                                                  {"@unitdiscount", unitdiscount, ""}}
        _cmnMthds._ExecuteNonQuery(param, "saveJobItemTb", 18)
    End Sub
    Public Function returnJobForInvPrint(ByVal tp As Integer) As DataSet
        Dim param As String(,) = New String(0, 2) {{"@Jobcode", jobcode, ""}}
        If tp = 0 Then
            Return _cmnMthds._ldDataset(param, "returnJobForInvPrint", 1)
        ElseIf tp = 1 Then
            Return _cmnMthds._ldDataset(param, "returnContractJobForPrint", 1)
        ElseIf tp = 2 Then '2
            Return _cmnMthds._ldDataset(param, "returnGarrageJobCard", 1)
        Else ' 3
            Dim param1 As String(,) = New String(1, 2) {{"@Jobcode", jobcode, ""}, _
                                                        {"@jbitmId", jbitmId, ""}}
            Return _cmnMthds._ldDataset(param1, "returnTailoringJobForPrint", 2)
        End If
    End Function
    Public Sub saveJobImeiListTb()
        Dim param As String(,) = New String(14, 2) {{"@jobimeiId", jobimeiId, ""}, _
                                                  {"@jobid", Jobid, ""}, _
                                                  {"@IMEI", IMEI, ""}, _
                                                  {"@Model", Model, ""}, _
                                                  {"@bt", bt, ""}, _
                                                  {"@cv", cv, ""}, _
                                                  {"@mc", mc, ""}, _
                                                  {"@bx", bx, ""}, _
                                                  {"@Complaints", Trim(JobDescription & ""), ""}, _
                                                  {"@Observation", Trim(Observation & ""), ""}, _
                                                  {"@TechRemarks", Trim(TechRemarks & ""), ""}, _
                                                  {"@CancelWarrenty", CWarrenty, ""}, _
                                                  {"@withWarrenty", withWarrenty, ""}, _
                                                  {"@Pen", Pen, ""}, _
                                                  {"@Remark", Trim(Remark & ""), ""}}
        _cmnMthds._ExecuteNonQuery(param, "saveJobImeiListTb", 15)
    End Sub

    Public Sub saveContruction()
        Dim param As String(,) = New String(10, 2) {{"@constructionid", constructionid, ""}, _
                                                  {"@jobid", Jobid, ""}, _
                                                  {"@jobtype", jobtype, ""}, _
                                                  {"@Startdate", Startdate, "d"}, _
                                                  {"@QtnAmt", QtnAmt, ""}, _
                                                  {"@OpnCost", OpnCost, ""}, _
                                                  {"@OpnIncome", OpnIncome, ""}, _
                                                  {"@SiteLocation", SiteLocation, ""}, _
                                                  {"@PlotNo", Trim(PlotNo & ""), ""}, _
                                                  {"@Pjobid", Val(Pjobid & ""), ""}, _
                                                  {"@SiteAddress", Trim(SiteAddress & ""), ""}}
        _cmnMthds._ExecuteNonQuery(param, "saveContruction", 11)
    End Sub

    Public Function SaveWorkProgressTb() As Long
        Dim param As String(,) = New String(11, 2) {{"@wprogressid", wprogressid, ""}, _
                                                  {"@jobid", Jobid, ""}, _
                                                  {"@workdetails", Trim(JobDescription & ""), ""}, _
                                                  {"@Startdate", Startdate, "d"}, _
                                                  {"@endate", endate, "d"}, _
                                                  {"@estimateddate", EstimatedDate, "d"}, _
                                                  {"@estimatedAmount", EstimatedAmt, ""}, _
                                                  {"@actualAmount", actualAmount, ""}, _
                                                  {"@Superwisor", Trim(Technician & ""), ""}, _
                                                  {"@Remarks", Trim(Remark & ""), ""}, _
                                                  {"@Userid", CurrentUser, ""}, _
                                                  {"@crtDt", Date.Now, "d"}}
        Return Val(_cmnMthds._ExecuteScalar(param, "SaveWorkProgressTb", 12).ToString)
    End Function
    Public Function returnVoucherList(ByVal jbid As Long, ByVal tp As Integer) As DataTable
        Dim param As String(,) = New String(1, 2) {{"@jobid", jbid, ""}, _
                                                   {"@tp", tp, ""}}
        Return _cmnMthds._ldDataset(param, "returnVoucherList", 2).Tables(0)
    End Function
    Public Function returnJVDetails(ByVal Linkno As Long, ByVal jobcode As String, ByVal tp As Integer) As DataTable
        Dim param As String(,) = New String(2, 2) {{"@LINKNO", Linkno, ""}, _
                                                   {"@jobcode", jobcode, ""}, _
                                                   {"@tp", tp, ""}}
        Return _cmnMthds._ldDataset(param, "returnJVDetails", 3).Tables(0)
    End Function
    Public Function returnJobwiseQty() As DataSet
        Dim param As String(,) = New String(0, 2) {{"@jobcode", jobcode, ""}}
        Return _cmnMthds._ldDataset(param, "returnJobwiseQty", 1)
    End Function
    Public Function returnJobcost() As DataTable
        Dim param As String(,) = New String(0, 2) {{"@jobcode", jobcode, ""}}
        Return _cmnMthds._ldDataset(param, "returnJobcost", 1).Tables(0)
    End Function
    Public Sub saveLoadgeTb()
        Dim param As String(,) = New String(11, 2) {{"@jobid", Jobid, ""}, _
                                                  {"@customerid", custcode, ""}, _
                                                  {"@ldgdescription", Trim(ldgdescription & ""), ""}, _
                                                  {"@noOfGust", noOfGust, ""}, _
                                                  {"@malegusts", malegusts, ""}, _
                                                  {"@femalegusts", femalegusts, ""}, _
                                                  {"@identityProof", Trim(identityProof & ""), ""}, _
                                                  {"@identityProofNumber", Trim(identityProofNumber & ""), ""}, _
                                                  {"@vehicleDetails", Trim(vehicleDetails & ""), ""}, _
                                                  {"@noofKids", noofKids, ""}, _
                                                  {"@Dtype", Dtype, ""}, _
                                                  {"@BookingRef", BookingRef, ""}}
        _cmnMthds._ExecuteNonQuery(param, "saveLoadgeTb", 12)
    End Sub
    Public Sub saveLodgeRoomTb()
        Dim param As String(,) = New String(12, 2) {{"@ldgroomid", ldgroomid, ""}, _
                                                  {"@roomItemid", roomItemid, ""}, _
                                                  {"@roomLdgid", Jobid, ""}, _
                                                  {"@remarks", Trim(remarks & ""), ""}, _
                                                  {"@checkinDateTime", checkinDateTime, "d"}, _
                                                  {"@checkoutEstimateDateTime", checkoutEstimateDateTime, "d"}, _
                                                  {"@roomcategory", roomcategory, ""}, _
                                                  {"@rent", rent, ""}, _
                                                  {"@gst", gst, ""}, _
                                                  {"@cess", cess, ""}, _
                                                  {"@roomstatus", roomstatus, ""}, _
                                                  {"@bookingRawid", bookingRawid, ""}, _
                                                  {"@HSNCode", Trim(hsnCode & ""), ""}}
        _cmnMthds._ExecuteNonQuery(param, "saveLodgeRoomTb", 13)
    End Sub
    Public Sub saveLodgeService()
        Dim param As String(,) = New String(7, 2) {{"@ldgServiceId", ldgServiceId, ""}, _
                                                  {"@ldgServiceItemid", ldgServiceItemid, ""}, _
                                                  {"@ldgroomid", roomItemid, ""}, _
                                                  {"@lodgeid", Jobid, ""}, _
                                                  {"@qty", Qty, ""}, _
                                                  {"@serviceDateTime", serviceDateTime, "d"}, _
                                                  {"@unitprice", unitprice, ""}, _
                                                  {"@remarks", remarks, ""}}
        _cmnMthds._ExecuteNonQuery(param, "saveLodgeService", 8)
    End Sub
    Public Sub checkOutLodge()
        Dim param As String(,) = New String(1, 2) {{"@ldgroomid", ldgroomid, ""}, _
                                                  {"@checkoutDateTime", checkoutDateTime, "d"}}
        _cmnMthds._ExecuteNonQuery(param, "checkOutLodge", 2)
    End Sub
    Public Function saveGarrageJob() As Long
        Dim param As String(,) = New String(18, 2) {{"@Jobid ", Jobid, ""}, _
                                                    {"@jobcode", jobcode, ""}, _
                                                    {"@jobdate ", jobdate, "d"}, _
                                                    {"@JobDescription ", Trim(JobDescription & ""), ""}, _
                                                    {"@custcode", custcode, ""}, _
                                                    {"@EstimatedDate", EstimatedDate, "d"}, _
                                                    {"@EstimatedAmt ", EstimatedAmt, ""}, _
                                                    {"@Technician ", Trim(Technician & ""), ""}, _
                                                    {"@ServiceCost", ServiceCost, ""}, _
                                                    {"@Userid ", Userid, ""}, _
                                                    {"@LabourCost ", LabourCost, ""}, _
                                                    {"@fuelStatus ", Trim(fuelStatus & ""), ""}, _
                                                    {"@kilometer", Trim(kilometer & ""), ""}, _
                                                    {"@accessoriesRemark ", Trim(accessoriesRemark & ""), ""}, _
                                                    {"@carid ", carid, ""}, _
                                                     {"@noofMats ", noofMats, ""}, _
                                                      {"@noofMudFlap ", noofMudFlap, ""}, _
                                                       {"@noofwheelcap ", noofwheelcap, ""}, _
                                                        {"@noofSpeekers ", noofSpeekers, ""}}

        Return Val(_cmnMthds._ExecuteScalar(param, "saveGarrageJob", 19).ToString())
    End Function
    Public Sub saveGarrageDemandedRepairTb()
        Dim param As String(,) = New String(4, 2) {{"@repairsId", repairsId, ""}, _
                                                  {"@grid", Jobid, ""}, _
                                                  {"@complaints", Trim(complaints & ""), ""}, _
                                                  {"@instrucations", Trim(instrucations & ""), ""}, _
                                                  {"@completed", completed, ""}}
        _cmnMthds._ExecuteNonQuery(param, "saveGarrageDemandedRepairTb", 5)
    End Sub
    Public Function returnGarrageJob(ByVal grid As Long) As DataTable
        Dim param As String(,) = New String(0, 2) {{"@grid", grid, ""}}
        Return _cmnMthds._ldDataset(param, "returnGarrageJob", 1).Tables(0)
    End Function
    Public Sub SaveMembership()
        Dim param As String(,) = New String(9, 2) {{"@jobid", Jobid, ""}, _
                                                  {"@age", age, ""}, _
                                                  {"@gender", gender, ""}, _
                                                  {"@identityProof", identityProof, ""}, _
                                                  {"@idno", identityProofNumber, ""}, _
                                                  {"@imagename", imagename, ""}, _
                                                  {"@moccupation", moccupation, ""}, _
                                                  {"@mmedicalcondition", mmedicalcondition, ""}, _
                                                  {"@emergencynumber", emergencynumber, ""}, _
                                                  {"@contactname", contactname, ""}}
        _cmnMthds._ExecuteNonQuery(param, "SaveMembership", 10)
    End Sub
    Public Sub SaveMembershipRenewalTb()
        Dim param As String(,) = New String(5, 2) {{"@renewid", renewid, ""}, _
                                                  {"@jobid", Jobid, ""}, _
                                                  {"@itemid", Itemid, ""}, _
                                                  {"@startDate", Startdate, "d"}, _
                                                  {"@endDate", EstimatedDate, "d"}, _
                                                  {"@price", unitprice, ""}}
        _cmnMthds._ExecuteNonQuery(param, "SaveMembershipRenewalTb", 6)
    End Sub
    Public Function saveUsedcarJob() As Long
        Dim param As String(,) = New String(8, 2) {{"@carid", carid, ""}, _
                                                    {"@Jobid", Jobid, ""}, _
                                                    {"@jobcode", jobcode, ""}, _
                                                    {"@jobdate", jobdate, "d"}, _
                                                    {"@JobDescription ", Trim(JobDescription & ""), ""}, _
                                                    {"@custcode", custcode, ""}, _
                                                    {"isParkAndSale", isParkAndSale, ""}, _
                                                    {"@lastKM", lastKM, ""}, _
                                                    {"@supcode", supcode, ""}}

        Return Val(_cmnMthds._ExecuteScalar(param, "saveusedcarjob", 9).ToString())
    End Function
    Public Function returnMemberShip() As DataSet
        If Trim(Closed & "") = "" Then Closed = "No"
        Dim param As String(,) = New String(3, 2) {{"@Jobid", Jobid, ""}, _
                                                   {"@DateFrom", DateFrom, "d"}, _
                                                   {"@DateTo", DateTo, "d"}, _
                                                   {"@Tp", Tp, ""}}
        Return _cmnMthds._ldDataset(param, "returnMemberShipDetails", 4)
    End Function
    Public Function returnPoolMembershipdetails(ByVal enabledate As Integer) As DataSet
        If Trim(Closed & "") = "" Then Closed = "No"
        Dim param As String(,) = New String(4, 2) {{"@Jobid", Jobid, ""}, _
                                                   {"@DateFrom", DateFrom, "d"}, _
                                                   {"@DateTo", DateTo, "d"}, _
                                                   {"@Tp", Tp, ""}, _
                                                   {"@enabledate", enabledate, ""}}
        Return _dlayer._ldDataset(param, "returnPoolMembershipdetails", 5, True)
    End Function
    Public Function saveSchoolStudentAdmissionTb() As Long
        Dim param As String(,) = New String(33, 2) {{"@studentid ", studentid, ""}, _
                                                    {"@jobid", Jobid, ""}, _
                                                    {"@studentname ", studentname, ""}, _
                                                    {"@admissionno", admissionno, ""}, _
                                                    {"@admissiondate ", admissiondate, "d"}, _
                                                    {"@standered", Trim(standered & ""), ""}, _
                                                    {"@section", Trim(section & ""), ""}, _
                                                    {"@rollnumber", Trim(rollnumber & ""), ""}, _
                                                    {"@gender ", gender, ""}, _
                                                    {"@dateofbirth", dateofbirth, "d"}, _
                                                    {"@stphone", Trim(stphone & ""), ""}, _
                                                    {"@studentemail", Trim(studentemail & ""), ""}, _
                                                    {"@stadd1", Trim(stadd1 & ""), ""}, _
                                                    {"@stadd2", Trim(stadd2 & ""), ""}, _
                                                    {"@stadd3", Trim(stadd3 & ""), ""}, _
                                                    {"@religion", Trim(religion & ""), ""}, _
                                                    {"@studentcast", Trim(studentcast & ""), ""}, _
                                                    {"@bloodgroup", Trim(bloodgroup & ""), ""}, _
                                                    {"@fathername", Trim(fathername & ""), ""}, _
                                                    {"@mothername", Trim(mothername & ""), ""}, _
                                                    {"@fatheroccupation", Trim(fatheroccupation & ""), ""}, _
                                                    {"@motheroccupation", Trim(motheroccupation & ""), ""}, _
                                                    {"@motherphonenumber", Trim(motherphonenumber & ""), ""}, _
                                                    {"@studentstatus", Trim(studentstatus & ""), ""}, _
                                                    {"@classteacherid", classteacherid, ""}, _
                                                    {"@admissionfees", admissionfees, ""}, _
                                                    {"@currentuser", CurrentUser, ""}, _
                                                    {"@AccountNo", AccountNo, ""}, _
                                                    {"@S1Accid", S1Accid, ""}, _
                                                    {"@OpnBal", OpnBal, ""}, _
                                                    {"@remark", Trim(remark & ""), ""}, _
                                                    {"@passportno", Trim(passportno & ""), ""}, _
                                                    {"@emiratesid", Trim(emiratesid & ""), ""}, _
                                                    {"@nationality", Trim(nationality & ""), ""}}

        Return Val(_cmnMthds._ExecuteScalar(param, "saveSchoolStudentAdmissionTb", 34).ToString())
    End Function
End Class
