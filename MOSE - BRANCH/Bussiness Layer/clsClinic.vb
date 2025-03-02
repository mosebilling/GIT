Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DataAccessLayer
Imports System.Data
Public Class clsClinic
    Private _cmnDlink As New cl_CmnDataLink()
    Private _cmnMthds As New CmnMethods()
    Private _cvn_visitnoteid As Long
    Public Property cvn_visitnoteid() As Long
        Get
            Return _cvn_visitnoteid
        End Get
        Set(ByVal value As Long)
            _cvn_visitnoteid = value
        End Set
    End Property
    Private _visitid As Long
    Public Property visitid() As Long
        Get
            Return _visitid
        End Get
        Set(ByVal value As Long)
            _visitid = value
        End Set
    End Property
    Private _visitno As Integer
    Public Property visitno() As Integer
        Get
            Return _visitno
        End Get
        Set(ByVal value As Integer)
            _visitno = value
        End Set
    End Property
    Private _customerid As Long
    Public Property customerid() As Long
        Get
            Return _customerid
        End Get
        Set(ByVal value As Long)
            _customerid = value
        End Set
    End Property
    Private _visitdate As Date
    Public Property visitdate() As Date
        Get
            Return _visitdate
        End Get
        Set(ByVal value As Date)
            _visitdate = value
        End Set
    End Property
    Private _doctor As String
    Public Property doctor() As String
        Get
            Return _doctor
        End Get
        Set(ByVal value As String)
            _doctor = value
        End Set
    End Property
    Private _bookingid As Long
    Public Property bookingid() As Long
        Get
            Return _bookingid
        End Get
        Set(ByVal value As Long)
            _bookingid = value
        End Set
    End Property
    Private _comment As String
    Public Property comment() As String
        Get
            Return _comment
        End Get
        Set(ByVal value As String)
            _comment = value
        End Set
    End Property
    Private _observation As String
    Public Property observation() As String
        Get
            Return _observation
        End Get
        Set(ByVal value As String)
            _observation = value

        End Set
    End Property
    Private _doctornote As String
    Public Property doctornote() As String
        Get
            Return _doctornote
        End Get
        Set(ByVal value As String)
            _doctornote = value
        End Set
    End Property
    Private _treatement As String
    Public Property treatement() As String
        Get
            Return _treatement
        End Get
        Set(ByVal value As String)

            _treatement = value
        End Set
    End Property
    Private _cvn_otherremarks As String
    Public Property cvn_otherremarks() As String
        Get
            Return _cvn_otherremarks
        End Get
        Set(ByVal value As String)
            _cvn_otherremarks = value
        End Set
    End Property
    Private _crtby As String
    Public Property crtby() As String
        Get
            Return _crtby
        End Get
        Set(ByVal value As String)
            _crtby = value
        End Set
    End Property
    Private _prefix As String
    Public Property prefix() As String
        Get
            Return _prefix
        End Get
        Set(ByVal value As String)
            _prefix = value
        End Set
    End Property
    Private _referencedBy As String
    Public Property referencedBy() As String
        Get
            Return _referencedBy
        End Get
        Set(ByVal value As String)
            _referencedBy = value
        End Set
    End Property
    Private _isfollowup As Integer
    Public Property isfollowup() As Integer
        Get
            Return _isfollowup
        End Get
        Set(ByVal value As Integer)
            _isfollowup = value
        End Set
    End Property
    Private _followupdate As Date
    Public Property followupdate() As Date
        Get
            Return _followupdate
        End Get
        Set(ByVal value As Date)
            _followupdate = value
        End Set
    End Property
    Private _followupText As String
    Public Property followupText() As String
        Get
            Return _followupText
        End Get
        Set(ByVal value As String)
            _followupText = value
        End Set
    End Property
    Private _visittype As String
    Public Property visittype() As String
        Get
            Return _visittype
        End Get
        Set(ByVal value As String)
            _visittype = value
        End Set
    End Property
#Region "clinicService"
    Private _id As Long
    Public Property id() As Long
        Get
            Return _id
        End Get
        Set(ByVal value As Long)
            _id = value
        End Set
    End Property
    Private _servicedate As Date
    Public Property servicedate() As Date
        Get
            Return _servicedate
        End Get
        Set(ByVal value As Date)
            _servicedate = value
        End Set
    End Property
    Private _serviceNo As Integer
    Public Property serviceNo() As Integer
        Get
            Return _serviceNo
        End Get
        Set(ByVal value As Integer)
            _serviceNo = value
        End Set
    End Property
    Private _remark As String
    Public Property remark() As String
        Get
            Return _remark
        End Get
        Set(ByVal value As String)
            _remark = value
        End Set
    End Property
    Private _detid As Long
    Public Property detid() As Integer
        Get
            Return _detid
        End Get
        Set(ByVal value As Integer)
            _detid = value
        End Set
    End Property
    Private _model As String
    Public Property model() As String
        Get
            Return _model
        End Get
        Set(ByVal value As String)
            _model = value
        End Set
    End Property
    Private _itemname As String
    Public Property itemname() As String
        Get
            Return _itemname
        End Get
        Set(ByVal value As String)
            _itemname = value
        End Set
    End Property
    Private _warrenty As String
    Public Property warrenty() As Integer
        Get
            Return _warrenty
        End Get
        Set(ByVal value As Integer)
            _warrenty = value
        End Set
    End Property
    Private _serialno As String
    Public Property serialno() As String
        Get
            Return _serialno
        End Get
        Set(ByVal value As String)
            _serialno = value
        End Set
    End Property
    Private _warrentydate As Date
    Public Property warrentydate() As Date
        Get
            Return _warrentydate
        End Get
        Set(ByVal value As Date)
            _warrentydate = value
        End Set
    End Property
    Private _warrentyInvNo As String
    Public Property warrentyInvNo() As String
        Get
            Return _warrentyInvNo
        End Get
        Set(ByVal value As String)
            _warrentyInvNo = value
        End Set
    End Property

#End Region

    Public Function saveClinicVisitTb() As String
        Dim _id As String
        Dim param As String(,) = New String(16, 2) {{"@visitid", visitid, ""}, _
                                                    {"@visitno", visitno, ""}, _
                                                    {"@Prefix", Trim(prefix & ""), ""}, _
                                                    {"@customerid", customerid, ""}, _
                                                    {"@visitdate", Format(visitdate, "yyyy/MM/dd"), "d"}, _
                                                    {"@doctor", Trim(doctor & ""), ""}, _
                                                    {"@bookingid", bookingid, ""}, _
                                                    {"@comment", Trim(comment & ""), ""}, _
                                                    {"@observation", Trim(observation & ""), ""}, _
                                                    {"@treatement", Trim(treatement & ""), ""}, _
                                                    {"@crtby", crtby & "", ""}, _
                                                    {"@referencedBy", Trim(referencedBy & ""), ""}, _
                                                    {"@isfollowup", isfollowup, ""}, _
                                                    {"@followupdate", followupdate & "", "d"}, _
                                                    {"@followupText", Trim(followupText & ""), ""}, _
                                                    {"@visittype", Trim(visittype & ""), ""}, _
                                                    {"@brid", Trim(UsrBr & ""), ""}}
        Dim recordNo As Object
        recordNo = _cmnMthds._ExecuteScalar(param, "saveClinicVisitTb", 17)
        _id = recordNo.ToString()
        Return _id
    End Function
    Public Function saveClinicVisitNoteTb() As String
        Dim _id As String
        Dim param As String(,) = New String(9, 2) {{"@cvn_visitnoteid", cvn_visitnoteid, ""}, _
                                                    {"@cvn_visitid", visitid, ""}, _
                                                    {"@cvn_notedate", visitdate, "d"}, _
                                                    {"@cvn_doctor", Trim(doctor & ""), ""}, _
                                                    {"@cvn_notecomment", Trim(comment & ""), ""}, _
                                                    {"@cvn_visitnoteobservation", Trim(observation & ""), ""}, _
                                                    {"@cvn_doctornote", Trim(doctornote & ""), ""}, _
                                                    {"@cvn_treatmentnote", Trim(treatement & ""), ""}, _
                                                    {"@cvn_otherremarks", Trim(cvn_otherremarks & ""), ""}, _
                                                    {"@crtby", crtby & "", ""}}
        Dim recordNo As Object
        recordNo = _cmnMthds._ExecuteScalar(param, "saveClinicVisitNoteTb", 10)
        _id = recordNo.ToString()
        Return _id
    End Function
    Public Function returnVisitCard(ByVal DateFrom As DateTime, ByVal DateTo As DateTime, ByVal visitid As Long, ByVal tp As Integer) As DataSet
        Dim param As String(,) = New String(3, 2) {{"@datefrom", Format(DateFrom, "yyyy/MM/dd"), "d"}, _
                                                     {"@dateto", Format(DateTo, "yyyy/MM/dd"), "d"}, _
                                                     {"@visitid", visitid, ""}, _
                                                     {"@tp", tp, ""}}
        Return _cmnMthds._ldDataset(param, "returnVisitCard", 4)
    End Function
    Public Function saveClinicServiceCmnTb() As String
        Dim _id As String
        Dim param As String(,) = New String(4, 2) {{"@id", id, ""}, _
                                                    {"@servicedate", Format(servicedate, "yyyy/MM/dd"), "d"}, _
                                                    {"@customerid", customerid, ""}, _
                                                    {"@serviceNo", serviceNo, ""}, _
                                                    {"@remark", Trim(remark & ""), ""}}
        Dim recordNo As Object
        recordNo = _cmnMthds._ExecuteScalar(param, "saveClinicServiceCmnTb", 5)
        _id = recordNo.ToString()
        Return _id
    End Function
    Public Sub saveClinicServiceDetailsTb()
        Dim param As String(,) = New String(8, 2) {{"@detid", detid, ""}, _
                                                   {"@serviceid", id, ""}, _
                                                    {"@model", model, ""}, _
                                                    {"@itemname", itemname, ""}, _
                                                    {"@comment", Trim(comment & ""), ""}, _
                                                    {"@warrenty", warrenty, ""}, _
                                                    {"@serialno", serialno, ""}, _
                                                    {"@warrentydate", warrentydate, "d"}, _
                                                    {"@warrentyInvNo", warrentyInvNo, ""}}
        _cmnMthds._ExecuteScalar(param, "saveClinicServiceDetailsTb", 9)
    End Sub
    Public Function loadClinicService(ByVal DateFrom As DateTime, ByVal DateTo As DateTime, ByVal status As Integer, ByVal tp As Integer) As DataSet
        Dim param As String(,) = New String(3, 2) {{"@datefrom", Format(DateFrom, "yyyy/MM/dd"), "d"}, _
                                                     {"@dateto", Format(DateTo, "yyyy/MM/dd"), "d"}, _
                                                     {"@status", status, ""}, _
                                                     {"@tp", tp, ""}}
        Return _cmnMthds._ldDataset(param, "loadClinicService", 4)
    End Function
End Class
