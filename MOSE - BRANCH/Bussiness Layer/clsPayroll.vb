Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DataAccessLayer
Imports System.Data
Public Class clsPayroll
    Private _cmnDlink As New cl_CmnDataLink()
    Private _cmnMthds As New CmnMethods()
#Region "Properties"
    Private _empid As Long
    Public Property empid() As Long
        Get
            Return _empid
        End Get
        Set(ByVal value As Long)
            _empid = value
        End Set
    End Property
    Private _empcode As String
    Public Property empcode() As String
        Get
            Return _empcode
        End Get
        Set(ByVal value As String)
            _empcode = value
        End Set
    End Property
    Private _empname As String
    Public Property empname() As String
        Get
            Return _empname
        End Get
        Set(ByVal value As String)
            _empname = value
        End Set
    End Property
    Private _emptype As Integer
    Public Property emptype() As Integer
        Get
            Return _emptype
        End Get
        Set(ByVal value As Integer)
            _emptype = value
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
    Private _empaddress As String
    Public Property empaddress() As String
        Get
            Return _empaddress
        End Get
        Set(ByVal value As String)
            _empaddress = value
        End Set
    End Property
    Private _phone As String
    Public Property phone() As String
        Get
            Return _phone
        End Get
        Set(ByVal value As String)
            _phone = value
        End Set
    End Property
    Private _emailid As String
    Public Property emailid() As String
        Get
            Return _emailid
        End Get
        Set(ByVal value As String)
            _emailid = value
        End Set
    End Property
    Private _joindate As Date
    Public Property joindate() As Date
        Get
            Return _joindate
        End Get
        Set(ByVal value As Date)
            _joindate = value
        End Set
    End Property
    Private _ledgerAcc As Integer
    Public Property ledgerAcc() As Integer
        Get
            Return _ledgerAcc
        End Get
        Set(ByVal value As Integer)
            _ledgerAcc = value
        End Set
    End Property
    Private _salaryType As Integer
    Public Property salaryType() As Integer
        Get
            Return _salaryType
        End Get
        Set(ByVal value As Integer)
            _salaryType = value
        End Set
    End Property
    Private _salarycategory As Integer
    Public Property salarycategory() As Integer
        Get
            Return _salarycategory
        End Get
        Set(ByVal value As Integer)
            _salarycategory = value
        End Set
    End Property
    Private _dailyPay As Double
    Public Property dailyPay() As Double
        Get
            Return _dailyPay
        End Get
        Set(ByVal value As Double)
            _dailyPay = value
        End Set
    End Property
    Private _monthlyPay As Double
    Public Property monthlyPay() As Double
        Get
            Return _monthlyPay
        End Get
        Set(ByVal value As Double)
            _monthlyPay = value
        End Set
    End Property
    Private _empstatus As Boolean
    Public Property empstatus() As Boolean
        Get
            Return _empstatus
        End Get
        Set(ByVal value As Boolean)
            _empstatus = value
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
    Private _departmentname As String
    Public Property departmentname() As String
        Get
            Return _departmentname
        End Get
        Set(ByVal value As String)
            _departmentname = value
        End Set
    End Property
    Private _bankdetails As String
    Public Property bankdetails() As String
        Get
            Return _bankdetails
        End Get
        Set(ByVal value As String)
            _bankdetails = value
        End Set
    End Property
    Private _visanumber As String
    Public Property visanumber() As String
        Get
            Return _visanumber
        End Get
        Set(ByVal value As String)
            _visanumber = value
        End Set
    End Property
    Private _visaexpiry As Date
    Public Property visaexpiry() As Date
        Get
            Return _visaexpiry
        End Get
        Set(ByVal value As Date)
            _visaexpiry = value
        End Set
    End Property
    Private _idcard As String
    Public Property idcard() As String
        Get
            Return _idcard
        End Get
        Set(ByVal value As String)
            _idcard = value
        End Set
    End Property
    Private _idcarexpiry As Date
    Public Property idcarexpiry() As Date
        Get
            Return _idcarexpiry
        End Get
        Set(ByVal value As Date)
            _idcarexpiry = value
        End Set
    End Property
    Private _licensenumber As String
    Public Property licensenumber()
        Get
            Return _licensenumber
        End Get
        Set(ByVal value)
            _licensenumber = value
        End Set
    End Property
    Private _licenseexpiry As Date
    Public Property licenseexpiry() As Date
        Get
            Return _licenseexpiry
        End Get
        Set(ByVal value As Date)
            _licenseexpiry = value
        End Set
    End Property
    Private _passportnumber As String
    Public Property passportnumber()
        Get
            Return _passportnumber
        End Get
        Set(ByVal value)
            _passportnumber = value
        End Set
    End Property
    Private _passportexpiry As Date
    Public Property passportexpiry() As Date
        Get
            Return _passportexpiry
        End Get
        Set(ByVal value As Date)
            _passportexpiry = value
        End Set
    End Property
    Private _labourid As String
    Public Property labourid() As String
        Get
            Return _labourid
        End Get
        Set(ByVal value As String)
            _labourid = value
        End Set
    End Property
    Private _labourexpiry As Date
    Public Property labourexpiry() As Date
        Get
            Return _labourexpiry
        End Get
        Set(ByVal value As Date)
            _labourexpiry = value
        End Set
    End Property

#End Region
#Region "Paroll Properties"
    Private _sheetid As Long
    Public Property sheetid() As Long
        Get
            Return _sheetid
        End Get
        Set(ByVal value As Long)
            _sheetid = value
        End Set
    End Property
    Private _sheetdate As Date
    Public Property sheetdate() As Date
        Get
            Return _sheetdate
        End Get
        Set(ByVal value As Date)
            _sheetdate = value
        End Set
    End Property
    Private _sheetdateto As Date
    Public Property sheetdateto() As Date
        Get
            Return _sheetdateto
        End Get
        Set(ByVal value As Date)
            _sheetdateto = value
        End Set
    End Property
    Private _sheetcategory As Integer
    Public Property sheetcategory() As Integer
        Get
            Return _sheetcategory
        End Get
        Set(ByVal value As Integer)
            _sheetcategory = value
        End Set
    End Property
    Private _sheetTotal As Double
    Public Property sheetTotal() As Double
        Get
            Return _sheetTotal
        End Get
        Set(ByVal value As Double)
            _sheetTotal = value
        End Set
    End Property
    Private _detId As Long
    Public Property detId() As Long
        Get
            Return _detId
        End Get
        Set(ByVal value As Long)
            _detId = value
        End Set
    End Property
    Private _sheetunits As Double
    Public Property sheetunits() As Double
        Get
            Return _sheetunits
        End Get
        Set(ByVal value As Double)
            _sheetunits = value
        End Set
    End Property
    Private _unitRate As Double
    Public Property unitRate() As Double
        Get
            Return _unitRate
        End Get
        Set(ByVal value As Double)
            _unitRate = value
        End Set
    End Property
    Private _unitTotal As Double
    Public Property unitTotal() As Double
        Get
            Return _unitTotal
        End Get
        Set(ByVal value As Double)
            _unitTotal = value
        End Set
    End Property
#End Region
#Region "Payroll Payment"
    Private _paymentid As Long
    Public Property paymentid() As Long
        Get
            Return _paymentid
        End Get
        Set(ByVal value As Long)
            _paymentid = value
        End Set
    End Property
    Private _datefrom As Date
    Public Property datefrom() As Date
        Get
            Return _datefrom
        End Get
        Set(ByVal value As Date)
            _datefrom = value
        End Set
    End Property
    Private _dateto As Date
    Public Property dateto() As Date
        Get
            Return _dateto
        End Get
        Set(ByVal value As Date)
            _dateto = value
        End Set
    End Property
    Private _bookingdate As Date
    Public Property bookingdate() As Date
        Get
            Return _bookingdate
        End Get
        Set(ByVal value As Date)
            _bookingdate = value
        End Set
    End Property
    Private _isloadedFromWorksheet As Boolean
    Public Property isloadedFromWorksheet() As Boolean
        Get
            Return _isloadedFromWorksheet
        End Get
        Set(ByVal value As Boolean)
            _isloadedFromWorksheet = value
        End Set
    End Property
    Private _paymentcategory As Integer
    Public Property paymentcategory() As Integer
        Get
            Return _paymentcategory
        End Get
        Set(ByVal value As Integer)
            _paymentcategory = value
        End Set
    End Property
    Private _JvLinkNo As Long
    Public Property JvLinkNo() As Long
        Get
            Return _JvLinkNo
        End Get
        Set(ByVal value As Long)
            _JvLinkNo = value
        End Set
    End Property
    Private _totalAmt As Double
    Public Property totalAmt() As Double
        Get
            Return _totalAmt
        End Get
        Set(ByVal value As Double)
            _totalAmt = value
        End Set
    End Property
    Private _paymentdetid As Long
    Public Property paymentdetid() As Long
        Get
            Return _paymentdetid
        End Get
        Set(ByVal value As Long)
            _paymentdetid = value
        End Set
    End Property
    Private _advance As Double
    Public Property advance() As Double
        Get
            Return _advance
        End Get
        Set(ByVal value As Double)
            _advance = value
        End Set
    End Property
    Private _absent As Integer
    Public Property absent() As Integer
        Get
            Return _absent
        End Get
        Set(ByVal value As Integer)
            _absent = value
        End Set
    End Property
    Private _holyday As Integer
    Public Property holyday() As Integer
        Get
            Return _holyday
        End Get
        Set(ByVal value As Integer)
            _holyday = value
        End Set
    End Property
    Private _units As Integer
    Public Property units() As Integer
        Get
            Return _units
        End Get
        Set(ByVal value As Integer)
            _units = value
        End Set
    End Property
    Private _allowance As Double
    Public Property allowance() As Double
        Get
            Return _allowance
        End Get
        Set(ByVal value As Double)
            _allowance = value
        End Set
    End Property
    Private _deduction As Double
    Public Property deduction() As Double
        Get
            Return _deduction
        End Get
        Set(ByVal value As Double)
            _deduction = value
        End Set
    End Property
    Private _deduction1 As Double
    Public Property deduction1() As Double
        Get
            Return _deduction1
        End Get
        Set(ByVal value As Double)
            _deduction1 = value
        End Set
    End Property
    Private _deduction2 As Double
    Public Property deduction2() As Double
        Get
            Return _deduction2
        End Get
        Set(ByVal value As Double)
            _deduction2 = value
        End Set
    End Property
    Private _netAmt As Double
    Public Property netAmt() As Double
        Get
            Return _netAmt
        End Get
        Set(ByVal value As Double)
            _netAmt = value
        End Set
    End Property
    Private _allowance2 As Double
    Public Property allowance2() As Double
        Get
            Return _allowance2
        End Get
        Set(ByVal value As Double)
            _allowance2 = value
        End Set
    End Property

#End Region
    Public Function _saveEmp() As String
        Dim param As String(,) = New String(26, 2) {{"@empid", empid, ""}, _
                                               {"@empcode", empcode, ""}, _
                                               {"@empname", empname, ""}, _
                                               {"@emptype", emptype, ""}, _
                                               {"@gender", gender, ""}, _
                                               {"@empaddress", empaddress, ""}, _
                                               {"@phone", phone, ""}, _
                                               {"@emailid", (Trim(emailid) & ""), ""}, _
                                               {"@joindate", joindate, "d"}, _
                                               {"@ledgerAcc", ledgerAcc, ""}, _
                                               {"@salaryType", salaryType, ""}, _
                                               {"@salarycategory", salarycategory, ""}, _
                                               {"@dailyPay", dailyPay, ""}, _
                                               {"@monthlyPay", monthlyPay, ""}, _
                                               {"@Designation", Designation, ""}, _
                                               {"@Department", Trim(departmentname & ""), ""}, _
                                               {"@bankdetails", Trim(bankdetails & ""), ""}, _
                                               {"@visanumber", Trim(visanumber & ""), ""}, _
                                               {"@visaexpiry", visaexpiry, "d"}, _
                                               {"@idcard", Trim(idcard & ""), ""}, _
                                               {"@idcarexpiry", idcarexpiry, "d"}, _
                                               {"@licensenumber", Trim(licensenumber & ""), ""}, _
                                               {"@licenseexpiry", licenseexpiry, "d"}, _
                                               {"@passportnumber", Trim(passportnumber & ""), ""}, _
                                               {"@passportexpiry", passportexpiry, "d"}, _
                                               {"@labourid", Trim(labourid & ""), ""}, _
                                               {"@labourexpiry", labourexpiry, "d"}}
        Return _cmnMthds._ExecuteScalar(param, "saveEmpMaster", 27).ToString
    End Function
    Public Function savePayrollWorksheet() As Long
        Dim param As String(,) = New String(4, 2) {{"@sheetid", sheetid, ""}, _
                                               {"@sheetdate", sheetdate, "d"}, _
                                               {"@sheetdateto", sheetdateto, "d"}, _
                                               {"@sheetcategory", sheetcategory, ""}, _
                                               {"@sheetTotal", sheetTotal, ""}}
        Dim mxno As String = _cmnMthds._ExecuteScalar(param, "savePayrollWorksheet", 5).ToString
        Return Val(mxno)
    End Function
    Public Sub savePayrollWorksheetDet()
        Dim param As String(,) = New String(5, 2) {{"@detId", detId, ""}, _
                                               {"@sheetid", sheetid, ""}, _
                                               {"@empid", empid, ""}, _
                                               {"@sheetunits", sheetunits, ""}, _
                                               {"@unitRate", unitRate, ""}, _
                                               {"@unitTotal", unitTotal, ""}}
        _cmnMthds._ExecuteNonQuery(param, "savePayrollWorksheetDet", 6)
    End Sub
    Public Function returnEmployeeForPaymentBooking(ByVal datefrom As Date, ByVal dateUpto As Date, ByVal salarycategory As Integer, ByVal tp As Integer) As DataSet
        Dim param As String(,) = New String(3, 2) {{"@datefrom", datefrom, "d"}, _
                                                   {"@dateUpto", dateUpto, "d"}, _
                                                   {"@salarycategory", salarycategory, ""}, _
                                                   {"@tp", tp, ""}}
        Return _cmnMthds._ldDataset(param, "returnEmployeeForPaymentBooking", 4)
    End Function
    Public Function savePayrollPaymentCmn() As String
        Dim param As String(,) = New String(6, 2) {{"@paymentid", paymentid, ""}, _
                                               {"@datefrom", datefrom, "d"}, _
                                               {"@dateto", dateto, "d"}, _
                                               {"@bookingdate", bookingdate, "d"}, _
                                               {"@isloadedFromWorksheet", isloadedFromWorksheet, ""}, _
                                               {"@paymentcategory", paymentcategory, ""}, _
                                               {"@totalAmt", totalAmt, ""}}
        Return _cmnMthds._ExecuteScalar(param, "savePayrollPaymentCmn", 7).ToString
    End Function
    Public Sub savePayrollPaymentdet()
        Dim param As String(,) = New String(13, 2) {{"@paymentdetid", paymentdetid, ""}, _
                                               {"@paymentid", paymentid, ""}, _
                                               {"@empid", empid, ""}, _
                                               {"@advance", advance, ""}, _
                                               {"@absent", absent, ""}, _
                                               {"@holyday", holyday, ""}, _
                                               {"@units", units, ""}, _
                                               {"@allowance", allowance, ""}, _
                                               {"@deduction", deduction, ""}, _
                                               {"@netAmt", netAmt, ""}, _
                                               {"@allowance2", allowance2, ""}, _
                                               {"@unitRate", unitRate, ""}, _
                                               {"@deduction1", deduction1, ""}, _
                                               {"@deduction2", deduction2, ""}}
        _cmnMthds._ExecuteNonQuery(param, "savePayrollPaymentdet", 14)
    End Sub
    Public Function returnPayrollPaymentBooking(ByVal paymentid As Long, ByVal tp As Integer) As DataSet
        Dim param As String(,) = New String(1, 2) {{"@paymentid", paymentid, ""}, _
                                                   {"@tp", tp, ""}}
        Return _cmnMthds._ldDataset(param, "returnPayrollPaymentBooking", 2)
    End Function
End Class
