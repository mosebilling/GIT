Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DataAccessLayer
Imports System.Data

'........... BL Is Created By Ashok On 08/102013

Public Class clsCommon_BL
    Private _cmnDlink As New cl_CmnDataLink()
    Private _cmnMthds As New CmnMethods()
    Private ds As New DataSet
    Public dtSerialNo As DataTable
#Region "Property Settings"
    Private _uid As String
    Private _pwd As String
    Private _uname As String
    Private _userGroup As String
    Private _server As String
    Private _dataPath As String
    Private _appPath As String
    Private _database As String
    Private _Connected As Boolean
    Private _attatched As Boolean
    Private _Exprs As Boolean
    Private _serverUid As String
    Private _Accountno As Integer
    Private _itemid As Integer
    Private _Opcost As Double
    Private _id As Integer


    Public Property P_uid() As String
        Get
            Return _uid
        End Get
        Set(ByVal value As String)
            _uid = value
        End Set
    End Property
    Public Property P_pwd() As String
        Get
            Return _pwd
        End Get
        Set(ByVal value As String)
            _pwd = value
        End Set
    End Property
    Public Property P_username() As String
        Get
            Return _uname
        End Get
        Set(ByVal value As String)
            _uname = value
        End Set
    End Property
    Public Property P_userGroup() As String
        Get
            Return _userGroup
        End Get
        Set(ByVal value As String)
            _userGroup = value
        End Set
    End Property
    Public Property serverUid() As String
        Get
            Return _serverUid
        End Get
        Set(ByVal value As String)
            _serverUid = value
        End Set
    End Property
    Public Property P_server() As String
        Get
            Return _server
        End Get
        Set(ByVal value As String)
            _server = value
        End Set
    End Property
    Public Property P_dataPath() As String
        Get
            Return _dataPath
        End Get
        Set(ByVal value As String)
            _dataPath = value
        End Set
    End Property
    Public Property P_appPath() As String
        Get
            Return _appPath
        End Get
        Set(ByVal value As String)
            _appPath = value
        End Set
    End Property
    Public Property P_database() As String
        Get
            Return _database
        End Get
        Set(ByVal value As String)
            _database = value
        End Set
    End Property
    Public Property _Connectedp() As Boolean
        Get
            Return _Connected
        End Get
        Set(ByVal value As Boolean)
            _Connected = value
        End Set
    End Property
    Public Property _pAttatched() As Boolean
        Get
            Return _attatched
        End Get
        Set(ByVal value As Boolean)
            _attatched = value
        End Set
    End Property
    Public Property _pExprs() As Boolean
        Get
            Return _Exprs
        End Get
        Set(ByVal value As Boolean)
            _Exprs = value
        End Set
    End Property
    Public Property Accountno() As Integer
        Get
            Return _Accountno
        End Get
        Set(ByVal value As Integer)
            _Accountno = value
        End Set
    End Property
    Public Property itemid() As Integer
        Get
            Return _itemid
        End Get
        Set(ByVal value As Integer)
            _itemid = value
        End Set
    End Property
    Public Property Opcost() As Double
        Get
            Return _Opcost
        End Get
        Set(ByVal value As Double)
            _Opcost = value
        End Set
    End Property
    Public Property id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property


    'VOUCHER FOOTER SETTINGS
    Private _vrType As Integer
    Private _fld1 As String
    Private _fld2 As String
    Private _fld3 As String
    Private _fld4 As String
    Private _tp As Integer

    Public Property vrType() As Integer
        Get
            Return _vrType
        End Get
        Set(ByVal value As Integer)
            _vrType = value
        End Set
    End Property
    Public Property fld1() As String
        Get
            Return _fld1
        End Get
        Set(ByVal value As String)
            _fld1 = value
        End Set
    End Property
    Public Property fld2() As String
        Get
            Return _fld2
        End Get
        Set(ByVal value As String)
            _fld2 = value
        End Set
    End Property
    Public Property fld3() As String
        Get
            Return _fld3
        End Get
        Set(ByVal value As String)
            _fld3 = value
        End Set
    End Property
    Public Property fld4() As String
        Get
            Return _fld4
        End Get
        Set(ByVal value As String)
            _fld4 = value
        End Set
    End Property
    Public Property tp() As Integer
        Get
            Return _tp
        End Get
        Set(ByVal value As Integer)
            _tp = value
        End Set
    End Property
#End Region

    Public Function ldDataReader(ByVal cmdText As String) As IDataReader
        Return _cmnMthds.LoadData(cmdText, False)
    End Function

    Public Function fldDatabases(ByVal _obj As clsCommon_BL) As DataTable
        Return _cmnDlink.fLoadDatabases(_obj.P_server, _obj.serverUid, _obj.P_pwd, _obj.P_database, "Select * from sys.databases where database_id>4")
    End Function
    Public Function fcheckConnection(ByVal _obj As clsCommon_BL) As Boolean
        Return _cmnDlink.CheckConnection(_obj._pExprs, _obj._Connectedp, _obj.P_server, _obj.serverUid, _obj.P_dataPath, _obj.P_pwd, _
         _obj.P_database)
    End Function
    Public Function fFilelocation(ByVal _obj As clsCommon_BL) As String
        If _obj.P_database IsNot Nothing Then
            Return _cmnDlink.fldLocation(_obj.P_server, _obj.serverUid, _obj.P_pwd, _obj.P_database, "Select physical_name,name from sys.master_files where name='" + _obj.P_database & "'")
        Else
            Return ""
        End If
    End Function
    Public Function _ldDataset(ByVal CmdTex As String, ByVal Isproc As Boolean) As DataSet
        Return _cmnMthds._ldDataset(CmdTex, Isproc)
    End Function
    Public Sub __saveDataset(ByVal CmdText As String, ByVal dtSet As DataSet)
        _cmnMthds._saveDataset(CmdText, dtSet)
    End Sub
    Public Sub __saveDataset(ByVal CmdText1 As String, ByVal CmdText2 As String, ByVal dtSet As DataSet, ByVal tableName1 As String, ByVal tableName2 As String)
        _cmnMthds._saveDataset(CmdText1, CmdText2, dtSet, tableName1, tableName2)
    End Sub
    Public Sub __saveDataTable(ByVal CmdText As String, ByVal dtTable As DataTable)
        _cmnMthds._saveDataTable(CmdText, dtTable)
    End Sub
    Public Function _fldDatatable(ByVal strSql As String, Optional ByVal isproc As Boolean = False) As DataTable
        Return _cmnMthds.fLoadDatatable(strSql, isproc)
    End Function
    Public Function returnCompanayDetailsForReport(ByVal vrtype As Integer) As DataTable
        Dim param As String(,) = New String(1, 2) {{"@vrtype", vrtype, ""}, _
                                                   {"@brid", UsrBr, ""}}
        Return _cmnMthds.fLoadDatatable(param, "returnCompanayDetailsForReport", 2)
        'Dim param As String(,) = New String(0, 2) {{"@vrtype", vrtype, ""}}
        'Return _cmnMthds.fLoadDatatable(param, "returnCompanayDetailsForReport", 1)
    End Function
    Public Sub _saveDatawithOutParm(ByVal Cmdtext As String)
        _cmnMthds._ExecuteNonQuery(Cmdtext)
    End Sub
    Public Function ExecuteScalar(ByVal Cmdtext As String) As Long
        Dim id As String
        Dim obj = _cmnMthds._ExecuteScalar(Cmdtext)
        id = obj.ToString()
        Return Val(id)
    End Function
    Public Sub updateCostAvarage(ByVal loc As String, ByVal itemid As Long)
        Dim param As String(,) = New String(1, 2) {{"@loc", loc, ""}, _
                                                   {"@itemid", itemid, ""}}
        _cmnMthds._ExecuteNonQuery(param, "updateCostAvarage", 2)
    End Sub
    Public Function _executeDataSetMultTable(ByVal commandText As String, ByVal Tablename As String, ByVal dtSet As DataSet) As DataSet
        Return _cmnMthds._executeDataSetMultTable(commandText, Tablename, dtSet)
    End Function

    Public Function fLdDatapath(ByVal _cmmPrts As clsCommon_BL, ByVal cmdText As String) As String
        Dim str As Object
        str = _cmnMthds.fldLocation(P_server, P_uid, P_pwd, P_database, cmdText)
        Return str.ToString()
    End Function
    Public Sub clsCnnection()
        _cmnMthds.clsConnection()
    End Sub
    Public Sub clsreader()
        _cmnMthds.CloseReader()
    End Sub
    Public Function fldCheckDatabaseName(ByVal _obj As clsCommon_BL, ByVal databasename As String) As Boolean
        Dim dt As DataTable
        dt = _cmnDlink.fLoadDatabases(_obj.P_server, _obj.serverUid, _obj.P_pwd, _obj.P_database, "Select * from sys.databases where database_id>4 and name='" & databasename & "'")
        If dt.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function returnDetailsByStringCondition(ByVal Condition As String, ByVal dateFlds As String, ByVal cmdText As String) As DataSet
        Dim param As String(,) = New String(0, 2) {{"@Condition", Condition, ""}}
        Return _cmnMthds._ldDataset(param, cmdText, 2)
    End Function
    Public Function validAccountName(ByVal conditionText As String, ByVal conditionIndex As Integer) As DataTable
        Dim param As String(,) = New String(1, 2) {{"@conditionText", conditionText, ""}, _
                                       {"@conditionIndex", conditionIndex, ""}}
        Return _cmnMthds._ldDataset(param, "checkvalidAccountName", 2).Tables(0)
    End Function
    Public Function IsjobExist(ByVal TextToValidate As String) As DataTable
        Return _cmnMthds.fLoadDatatable("SELECT * from JobTb where jobcode='" & TextToValidate & "'", False)
    End Function
    Public Sub saveOrEditSuppCostTb()
        Dim param As String(,) = New String(3, 2) { _
                                                   {"@Accountno", Accountno, ""}, _
                                                       {"@itemid", itemid, ""}, _
                                                       {"@Opcost", Opcost, ""}, _
                                                       {"@id", id, ""}}
        _cmnMthds._ExecuteNonQuery(param, "saveOrEditSuppCostTb", 4)
    End Sub
    Public Function isValidEntry(ByVal TextToValidate As String, ByVal TbNo As Integer) As String
        Dim dt As DataTable
        isValidEntry = ""
        Select Case TbNo
            Case 0
                '"txtLocation"
                dt = _cmnMthds.fLoadDatatable("SELECT LocationId from LocationTb where LocationId='" & TextToValidate & "'", False)
                If dt.Rows.Count > 0 Then
                    Return dt(0)(0)
                End If
            Case 1
                '"txtSuppAlias"
            Case 2
                '"txtSuppName"
            Case 3
                'txtJob
                dt = _cmnMthds.fLoadDatatable("SELECT jobname from JobTb where jobcode='" & TextToValidate & "'", False)
                If dt.Rows.Count > 0 Then
                    Return Trim(dt(0)(0) & "")
                End If
            Case 4
                'txtFC
                'dt = _cmnMthds.fLoadDatatable("SELECT CountryCode from CountryTb where CountryCode='" & TextToValidate & "'", False)
                'If dt.Rows.Count > 0 Then
                '    Return dt(0)(0)
                'End If
            Case 5
                'txtTerms
                dt = _cmnMthds.fLoadDatatable("SELECT TermsId from TermsTb where TermsId='" & TextToValidate & "'", False)
                If dt.Rows.Count > 0 Then
                    Return dt(0)(0)
                End If
            Case 6
                'txtPurchAlias
            Case 7
                'txtPurchaseName

            Case 9
                'txtVessel
                dt = _cmnMthds.fLoadDatatable("SELECT VesselId from VesselesTb where VesselId='" & TextToValidate & "'", False)
                If dt.Rows.Count > 0 Then
                    Return dt(0)(0)
                End If
            Case 10
                'txtarea
                dt = _cmnMthds.fLoadDatatable("SELECT AreaCode from AreaTb where AreaCode='" & TextToValidate & "'", False)
                If dt.Rows.Count > 0 Then
                    Return dt(0)(0)
                End If
            Case 11
                'txtsman
                dt = _cmnMthds.fLoadDatatable("SELECT SManCode from SalesmanTb where SManCode='" & TextToValidate & "'", False)
                If dt.Rows.Count > 0 Then
                    Return dt(0)(0)
                End If
            Case 12
                'Department
                dt = _cmnMthds.fLoadDatatable("SELECT DeptId FROM DepartmentTb where DeptId='" & TextToValidate & "'", False)
                If dt.Rows.Count > 0 Then
                    Return dt(0)(0)
                End If
            Case 13
                'Temple Admission 
                dt = _cmnMthds.fLoadDatatable("SELECT MemberName FROM TempleMembershipTb where MemberCode='" & TextToValidate & "'", False)
                If dt.Rows.Count > 0 Then
                    Return dt(0)(0)
                End If
        End Select

    End Function
    Public Function getApprvlStr() As String
        Select Case Now.Day Mod 6
            Case 1 'dmy
                getApprvlStr = Now.Day * 5 & Month(Date.Now) * 24 & Year(Date.Now) * 36
            Case 2 'dym
                getApprvlStr = Now.Day * 5 & Year(Date.Now) * 24 & Month(Date.Now) * 36
            Case 3 'mdy
                getApprvlStr = Month(Date.Now) * 5 & Now.Day * 24 & Year(Date.Now) * 36
            Case 4 'myd
                getApprvlStr = Month(Date.Now) * 5 & Year(Date.Now) * 24 & Now.Day * 36
            Case 5 'ydm
                getApprvlStr = Year(Date.Now) * 5 & Now.Day * 24 & Month(Date.Now) * 36
            Case Else 'ymd
                getApprvlStr = Year(Date.Now) * 5 & Month(Date.Now) * 24 & Now.Day * 36
        End Select
    End Function
    Public Sub setRefreshQty(ByVal itemid As Long)
        Dim param As String(,) = New String(1, 2) { _
                                                 {"@itemid", itemid, ""}, _
                                                 {"@loc", IIf(UsrBr = "", "", Dloc), ""}}
        _cmnMthds._ExecuteNonQuery(param, "setRefreshQty", 2)
    End Sub
    Public Sub updateClosingBalance(Optional ByVal varAccountno As Long = 0)
        Dim param As String(,) = New String(0, 2) { _
                                                  {"@Accountno", varAccountno, ""}}
        _cmnMthds._ExecuteNonQuery(param, "updateClosingBalance", 1)
    End Sub

    Public Sub updatetoLog(ByVal vrmodule As String, ByVal eventTp As String, ByVal eventdt As DateTime, ByVal eventdet As String)
        Dim param As String(,) = New String(4, 2) {{"@module", vrmodule, ""}, _
                                                   {"@eventTp", eventTp, ""}, _
                                                   {"@eventdt", eventdt, "d"}, _
                                                   {"@eventuser", CurrentUser, ""}, _
                                                   {"@eventdet", eventdet, ""}}
        _cmnMthds._ExecuteNonQuery(param, "updatetoLog", 5)
    End Sub

    Public Function returnShowAlert(ByVal AlertType As Integer, ByVal dateFrom As Date, ByVal dateTo As Date, ByVal isDatewise As Integer) As DataTable
        Dim param As String(,) = New String(4, 2) {{"@AlertType", AlertType, ""}, _
                                                   {"@dateFrom", dateFrom, "d"}, _
                                                   {"@dateTo", dateTo, "d"}, _
                                                   {"@isDatewise", isDatewise, ""}, _
                                                   {"@brid", UsrBr, ""}}
        Return _cmnMthds._ldDataset(param, "returnShowAlert", 5).Tables(0)
    End Function
    Public Function returnAvailableSerialNumber(ByVal tp As Integer, ByVal serialNo As String, ByVal trtype As String) As DataTable
        Dim param As String(,) = New String(2, 2) {{"@tp", tp, ""}, _
                                                   {"@Serialno", serialNo, ""}, _
                                                    {"@trtype", trtype, ""}}
        Return _cmnMthds._ldDataset(param, "returnAvailableSerialNumber", 3).Tables(0)
    End Function
    Public Sub updateMeterReadingQty(ByVal MeterCode As String)
        Dim param As String(,) = New String(0, 2) {{"@MeterCode", MeterCode, ""}}
        _cmnMthds._ExecuteNonQuery(param, "updateMeterReadingQty", 1)
    End Sub
    Public Function checkexist(ByVal str As String) As Boolean
        Dim dt As DataTable
        dt = _cmnMthds.fLoadDatatable(str, False)
        If dt.Rows.Count > 0 Then
            Return True
        End If
        Return False
    End Function
    Public Function returnProfitLossForWeb(ByVal datefrom As Date, ByVal dateto As Date) As DataTable
        Dim param As String(,) = New String(1, 2) {{"@datefrom", datefrom, "d"}, _
                                                   {"@dateto", dateto, "d"}}
        Return _cmnMthds.fLoadDatatable(param, "returnProfitLossForWeb", 2)
    End Function
    Public Sub resetBatch(ByVal tp As Integer)
        Dim param As String(,) = New String(0, 2) {{"@tp", tp, ""}}
        _cmnMthds._ExecuteNonQuery(param, "resetBatch", 1)
    End Sub
    Public Sub updateLastPurchaseCost(ByVal itemid As Long)
        Dim param As String(,) = New String(1, 2) {{"@itemid", itemid, ""}, _
                                                   {"@branch", UsrBr, ""}}
        _cmnMthds._ExecuteNonQuery(param, "updateLastPurchaseCost", 2)
    End Sub
    Public Sub updatecostavarageontr(ByVal trid As Long, ByVal mchnname As String)
        Dim param As String(,) = New String(1, 2) {{"@trid", trid, ""}, _
                                                   {"@mchnname", mchnname, ""}}
        _cmnMthds._ExecuteNonQuery(param, "updatecostavarageontr", 2)
    End Sub
    Public Function returnEventLog(ByVal datefrom As Date, ByVal dateto As Date, ByVal tp As Integer, ByVal modulename As String, ByVal user As String, ByVal eventname As String) As DataTable
        Dim param As String(,) = New String(5, 2) {{"@tp", tp, ""}, _
                                                   {"@datefrom", datefrom, "d"}, _
                                                   {"@dateto", dateto, "d"}, _
                                                   {"@module", modulename, ""}, _
                                                   {"@user", user, ""}, _
                                                   {"@eventname", eventname, ""}}
        Return _cmnMthds.fLoadDatatable(param, "returnEventLog", 6)
    End Function
End Class

