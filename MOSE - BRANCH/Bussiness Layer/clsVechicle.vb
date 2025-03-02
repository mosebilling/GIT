Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DataAccessLayer
Imports System.Data
Public Class clsVechicle
    Private _cmnDlink As New cl_CmnDataLink()
    Private _cmnMthds As New CmnMethods()
#Region "Properties"
    Private _carid As Integer
    Public Property carid() As Integer
        Get
            Return _carid
        End Get
        Set(ByVal value As Integer)
            _carid = value
        End Set
    End Property
    Private _cartype As String
    Public Property cartype() As String
        Get
            Return _cartype
        End Get
        Set(ByVal value As String)
            _cartype = value
        End Set
    End Property
    Private _platenumber As String
    Public Property platenumber() As String
        Get
            Return _platenumber
        End Get
        Set(ByVal value As String)
            _platenumber = value
        End Set
    End Property
    Private _regyear As String
    Public Property regyear() As String
        Get
            Return _regyear
        End Get
        Set(ByVal value As String)
            _regyear = value
        End Set
    End Property
    Private _chassisnumber As String
    Public Property chassisnumber() As String
        Get
            Return _chassisnumber
        End Get
        Set(ByVal value As String)
            _chassisnumber = value
        End Set
    End Property
    Private _bodynumber As String
    Public Property bodynumber() As String
        Get
            Return _bodynumber
        End Get
        Set(ByVal value As String)
            _bodynumber = value
        End Set
    End Property
    Private _customerid As Integer
    Public Property customerid() As Integer
        Get
            Return _customerid
        End Get
        Set(ByVal value As Integer)
            _customerid = value
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
    Private _engineNo As String
    Public Property engineNo() As String
        Get
            Return _engineNo
        End Get
        Set(ByVal value As String)
            _engineNo = value
        End Set
    End Property
    Private _isParkAndSale As Boolean
    Public Property isParkAndSale() As Boolean
        Get
            Return _isParkAndSale
        End Get
        Set(ByVal value As Boolean)
            _isParkAndSale = value
        End Set
    End Property
    Private _isuedcar As Boolean
    Public Property isuedcar() As Boolean
        Get
            Return _isuedcar
        End Get
        Set(ByVal value As Boolean)
            _isuedcar = value
        End Set
    End Property
    Private _itemid As Long
    Public Property itemid() As Long
        Get
            Return _itemid
        End Get
        Set(ByVal value As Long)
            _itemid = value
        End Set
    End Property

#End Region
    Public Function savecarmaster() As Integer
        Dim param As String(,) = New String(10, 2) { _
                                                    {"@carid", carid, ""}, _
                                                    {"@cartype", cartype, ""}, _
                                                    {"@platenumber", platenumber, ""}, _
                                                    {"@year", regyear, ""}, _
                                                    {"@chaisenumber", Trim(chassisnumber & ""), ""}, _
                                                    {"@bodynumber", Trim(bodynumber & ""), ""}, _
                                                    {"@remarks", Trim(remarks & ""), ""}, _
                                                    {"@customerid", customerid, ""}, _
                                                    {"@engineNo", Trim(engineNo & ""), ""}, _
                                                    {"@isuedcar", isuedcar, ""}, _
                                                    {"@itemid", itemid, ""}}

        Dim recordNo As Object
        recordNo = _cmnMthds._ExecuteScalar(param, "savecarmaster", 11)
        Dim id As Integer
        id = Val(recordNo.ToString())
        Return id
    End Function
    Public Function returncarmaster(ByVal tp As Integer) As DataSet
        Dim param As String(,) = New String(2, 2) { _
                                                    {"@isParkAndSale", isParkAndSale, ""}, _
                                                    {"@tp", tp, ""}, _
                                                    {"@customerid", customerid, ""}}
        Return _cmnMthds._ldDataset(param, "returncarmaster", 3)
    End Function
    Public Function returnusedcarmaster(ByVal isParkAndSale As Boolean, ByVal soldstatus As Integer, ByVal DateFrom As Date, ByVal Dateto As Date, ByVal tp As Integer) As DataSet
        Dim param As String(,) = New String(4, 2) { _
                                                    {"@isParkAndSale", isParkAndSale, ""}, _
                                                    {"@soldstatus", soldstatus, ""}, _
                                                    {"@datefrom", DateFrom, "d"}, _
                                                   {"@dateTo", Dateto, "d"}, _
                                                    {"@tp", tp, ""}}
        Return _cmnMthds._ldDataset(param, "returnusedcarmaster", 5)
    End Function

    Public Function returnWSCardHistory(ByVal tp As Integer, ByVal cardnumber As String, ByVal trid As Long, ByVal packageid As Integer)
        Dim param As String(,) = New String(3, 2) { _
                                                    {"@tp", tp, ""}, _
                                                    {"@savecarmastercardnumber", cardnumber, ""}, _
                                                    {"@packageid", packageid, ""}, _
                                                    {"@trid", trid, ""}}
        Return _cmnMthds._ldDataset(param, "returnWSCardHistory", 4).Tables(0)
    End Function
    Public Function returncardhistory(ByVal tp As Integer, ByVal cardnumber As String) As DataTable
        Dim param As String(,) = New String(1, 2) { _
                                                    {"@tp", tp, ""}, _
                                                    {"@cardnumber", cardnumber, ""}}
        Return _cmnMthds._ldDataset(param, "returncardhistory", 2).Tables(0)
    End Function
    Public Function retunGarrageJobList(ByVal tp As Integer, ByVal datefrom As Date, ByVal DateTo As Date, ByVal status As Integer) As DataSet
        Try
            Dim param As String(,) = New String(3, 2) {{"@dateFrom", datefrom, "d"}, _
                                                                {"@dateTo", DateTo, "d"}, _
                                                                {"@status", status, ""}, _
                                                                {"@tp", tp, ""}}
            Return _cmnMthds._ldDataset(param, "retunGarrageJobList", 4)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return Nothing
    End Function
    Public Function returnusedcardata(ByVal jobid As Integer) As DataSet
        Dim param As String(,) = New String(0, 2) {{"@jobid", jobid, ""}}
        Return _cmnMthds._ldDataset(param, "returnusedcardata", 1)
    End Function
    Public Function returnCarjobHistory(ByVal carid As Integer) As DataSet
        Dim param As String(,) = New String(0, 2) { _
                                                    {"@carid", carid, ""}}
        Return _cmnMthds._ldDataset(param, "returnCarjobHistory", 1)
    End Function
    Public Function returnUsedCarInvPrint(ByVal jobcode As String) As DataSet
        Dim param As String(,) = New String(0, 2) { _
                                                     {"@jobcode", jobcode, ""}}
        Return _cmnMthds._ldDataset(param, "returnUsedCarInvPrint", 1)
    End Function

End Class
