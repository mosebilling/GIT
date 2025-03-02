Imports DataAccessLayer
Public Class clsGSTMaster
#Region "Properties"
    Private _gstid As Integer
    Private _HSNCode As String
    Private _CGST As Double
    Private _SGST As Double
    Private _IGST As Double
    Private _CGSTCAc As Long
    Private _CGSTPAc As Long
    Private _SGSTCAc As Long
    Private _SGSTPAc As Long
    Private _IGSTCAc As Long
    Private _IGSTPAc As Long
    Private _GSTName As String
    Public Property gstid() As Integer
        Get
            Return _gstid
        End Get
        Set(ByVal value As Integer)
            _gstid = value
        End Set
    End Property
    Public Property HSNCode() As String
        Get
            Return _HSNCode
        End Get
        Set(ByVal value As String)
            _HSNCode = value
        End Set
    End Property
    Public Property CGST() As Double
        Get
            Return _CGST
        End Get
        Set(ByVal value As Double)
            _CGST = value
        End Set
    End Property
    Public Property SGST() As Double
        Get
            Return _SGST
        End Get
        Set(ByVal value As Double)
            _SGST = value
        End Set
    End Property
    Public Property IGST() As Double
        Get
            Return _IGST
        End Get
        Set(ByVal value As Double)
            _IGST = value
        End Set
    End Property
    Public Property CGSTCAc() As Long
        Get
            Return _CGSTCAc
        End Get
        Set(ByVal value As Long)
            _CGSTCAc = value
        End Set
    End Property
    Public Property CGSTPAc() As Long
        Get
            Return _CGSTPAc
        End Get
        Set(ByVal value As Long)
            _CGSTPAc = value
        End Set
    End Property
    Public Property SGSTCAc() As Long
        Get
            Return _SGSTCAc
        End Get
        Set(ByVal value As Long)
            _SGSTCAc = value
        End Set
    End Property
    Public Property SGSTPAc() As Long
        Get
            Return _SGSTPAc
        End Get
        Set(ByVal value As Long)
            _SGSTPAc = value
        End Set
    End Property
    Public Property IGSTCAc() As Long
        Get
            Return _IGSTCAc
        End Get
        Set(ByVal value As Long)
            _IGSTCAc = value
        End Set
    End Property
    Public Property IGSTPAc() As Long
        Get
            Return _IGSTPAc
        End Get
        Set(ByVal value As Long)
            _IGSTPAc = value
        End Set
    End Property
    Public Property GSTName() As String
        Get
            Return _GSTName
        End Get
        Set(ByVal value As String)
            _GSTName = value
        End Set
    End Property
#End Region
    Private _cmnDlink As New cl_CmnDataLink()
    Private _cmnMthds As New CmnMethods()
    Public Sub saveGSTMaster()
        Dim param As String(,) = New String(11, 2) {{"@gstid", gstid, ""}, _
                                                    {"@HSNCode", HSNCode, ""}, _
                                                    {"@CGST", CGST, ""}, _
                                                    {"@SGST", SGST, ""}, _
                                                    {"@IGST", IGST, ""}, _
                                                    {"@CGSTCAc", CGSTCAc, ""}, _
                                                    {"@CGSTPAc", CGSTPAc, ""}, _
                                                    {"@SGSTCAc", SGSTCAc, ""}, _
                                                    {"@SGSTPAc", SGSTPAc, ""}, _
                                                    {"@IGSTCAc", IGSTCAc, ""}, _
                                                    {"@IGSTPAc", IGSTPAc, ""}, _
                                                    {"@GSTName", GSTName, ""}}
        _cmnMthds._ExecuteNonQuery(param, "saveGSTMaster", 12)
    End Sub
    Public Function returnGstMaster(ByVal tp As Integer) As DataTable
        Dim param As String(,) = New String(2, 2) {{"@hsncode", Trim(HSNCode & ""), ""}, _
                                                   {"@gstid", gstid, ""}, _
                                                   {"@TP", tp, ""}}
        Return _cmnMthds._ldDataset(param, "returnGstMaster", 3).Tables(0)
    End Function
End Class
