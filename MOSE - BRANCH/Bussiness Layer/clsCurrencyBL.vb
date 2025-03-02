Imports DataAccessLayer
Public Class clsCurrencyBL
#Region "Property variables"
    Private _CurrencyId As Integer
    Private _CurrencyCode As String
    Private _CurrencyRate As Double
    Private _FractionCode As String
    Private _Description As String
    Private _DecimalPlaces As Integer
    Private _tp As Integer

    Public Property CurrencyId() As Integer
        Get
            Return _CurrencyId
        End Get
        Set(ByVal value As Integer)
            _CurrencyId = value
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
    Public Property CurrencyRate() As Double
        Get
            Return _CurrencyRate
        End Get
        Set(ByVal value As Double)
            _CurrencyRate = value
        End Set
    End Property
    Public Property FractionCode() As String
        Get
            Return _FractionCode
        End Get
        Set(ByVal value As String)
            _FractionCode = value
        End Set
    End Property
    Public Property Description() As String
        Get
            Return _Description
        End Get
        Set(ByVal value As String)
            _Description = value
        End Set
    End Property
    Public Property DecimalPlaces() As Integer
        Get
            Return _DecimalPlaces
        End Get
        Set(ByVal value As Integer)
            _DecimalPlaces = value
        End Set
    End Property
    Public Property TP() As Integer
        Get
            Return _tp
        End Get
        Set(ByVal value As Integer)
            _tp = value
        End Set
    End Property


   

#End Region
    Private _cmnDlink As New cl_CmnDataLink()
    Private _cmnMthds As New CmnMethods()
    Public Function retrunCurrency() As DataTable
        Dim param As String(,) = New String(1, 2) {{"@CurrencyId", CurrencyId, ""}, _
                                                   {"@TP", TP, ""}}
        Return _cmnMthds._ldDataset(param, "retrunCurrency", 2).Tables(0)
    End Function
    Public Sub saveCurrency()
        Dim param As String(,) = New String(5, 2) {{"@CurrencyId", CurrencyId, ""}, _
                                                    {"@CurrencyCode", CurrencyCode, ""}, _
                                                    {"@CurrencyRate", CurrencyRate, ""}, _
                                                    {"@Description", Description, ""}, _
                                                    {"@FractionCode", FractionCode, ""}, _
                                                    {"@DecimalPlaces", DecimalPlaces, ""}}
        _cmnMthds._ExecuteNonQuery(param, "saveCurrency", 6)
    End Sub
   
End Class
