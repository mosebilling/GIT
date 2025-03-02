Imports DataAccessLayer
Public Class clsBranch
    Private _cmnDlink As New cl_CmnDataLink()
    Private _cmnMthds As New CmnMethods()
    Private _BranchId As Integer
    Public Property BranchId() As Integer
        Get
            Return _BranchId
        End Get
        Set(ByVal value As Integer)
            _BranchId = value
        End Set
    End Property
    Private _Branchcode As String
    Public Property Branchcode() As String
        Get
            Return _Branchcode
        End Get
        Set(ByVal value As String)
            _Branchcode = value
        End Set
    End Property
    Private _BranchName As String
    Public Property BranchName() As String
        Get
            Return _BranchName
        End Get
        Set(ByVal value As String)
            _BranchName = value
        End Set
    End Property
    Private _CostOfSlHd As Integer
    Public Property CostOfSlHd() As Integer
        Get
            Return _CostOfSlHd
        End Get
        Set(ByVal value As Integer)
            _CostOfSlHd = value
        End Set
    End Property
    Private _CostDiff As Integer
    Public Property CostDiff() As Integer
        Get
            Return _CostDiff
        End Get
        Set(ByVal value As Integer)
            _CostDiff = value
        End Set
    End Property
    Private _IsDefault As Boolean
    Public Property IsDefault() As Boolean
        Get
            Return _IsDefault
        End Get
        Set(ByVal value As Boolean)
            _IsDefault = value
        End Set
    End Property
    Private _StockHd As Integer
    Public Property StockHd() As Integer
        Get
            Return _StockHd
        End Get
        Set(ByVal value As Integer)
            _StockHd = value
        End Set
    End Property
    Private _BrAdd1 As String
    Public Property BrAdd1() As String
        Get
            Return _BrAdd1
        End Get
        Set(ByVal value As String)
            _BrAdd1 = value
        End Set
    End Property
    Private _BrAdd2 As String
    Public Property BrAdd2() As String
        Get
            Return _BrAdd2
        End Get
        Set(ByVal value As String)
            _BrAdd2 = value
        End Set
    End Property
    Private _BrAdd3 As String
    Public Property BrAdd3() As String
        Get
            Return _BrAdd3
        End Get
        Set(ByVal value As String)
            _BrAdd3 = value
        End Set
    End Property
    Private _BrAdd4 As String
    Public Property BrAdd4() As String
        Get
            Return _BrAdd4
        End Get
        Set(ByVal value As String)
            _BrAdd4 = value
        End Set
    End Property
    Private _BrPhone As String
    Public Property BrPhone() As String
        Get
            Return _BrPhone
        End Get
        Set(ByVal value As String)
            _BrPhone = value
        End Set
    End Property
    Private _BrLocation As String
    Public Property BrLocation() As String
        Get
            Return _BrLocation
        End Get
        Set(ByVal value As String)
            _BrLocation = value
        End Set
    End Property
    Public Sub saveBranch()
        Dim param As String(,) = New String(12, 2) {{"@BranchId", BranchId, ""}, _
                                                    {"@Branchcode", Branchcode, ""}, _
                                                    {"@BranchName", BranchName, ""}, _
                                                    {"@CostOfSlHd", CostOfSlHd, ""}, _
                                                    {"@CostDiff", CostDiff, ""}, _
                                                    {"@IsDefault", IsDefault, ""}, _
                                                    {"@StockHd", StockHd, ""}, _
                                                    {"@BrAdd1", BrAdd1, ""}, _
                                                    {"@BrAdd2", BrAdd2, ""}, _
                                                    {"@BrAdd3", BrAdd3, ""}, _
                                                    {"@BrAdd4", BrAdd4, ""}, _
                                                    {"@BrPhone", BrPhone, ""}, _
                                                    {"@BrLocation", BrLocation, ""}}
        _cmnMthds._ExecuteNonQuery(param, "saveBranchTb", 13)
    End Sub
    Public Function loadBranch() As DataTable
        Dim param As String(,) = New String(0, 2) {{"@branchcode", Branchcode, ""}}
        Return _cmnMthds._ldDataset(param, "loadBranch", 1).Tables(0)
    End Function
End Class
