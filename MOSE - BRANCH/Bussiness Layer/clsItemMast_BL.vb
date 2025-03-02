Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DataAccessLayer
Imports System.Data
Public Class clsItemMast_BL
    Private _cmnDlink As New cl_CmnDataLink()
    Private _cmnMthds As New CmnMethods()
#Region "Property Settings"
    Private _ItemId As Long
    Private _ItemCode As String
    Private _Unit As String
    Private _Descr As String
    Private _Category As String
    Private _opnQty As Double
    Private _OpnCost As Double
    Private _salesPrice As Double
    Private _WSalesPrice As Double
    Private _MinQty As Double
    Private _Make As String
    Private _Model As String
    Private _vat As String
    Private _MinSalesPrice As Double

    Private _P1Unit As String
    Private _P2Unit As String
    Private _P1Fra As Integer
    Private _P2Fra As Integer
    Private _Level1 As String
    Private _Level2 As String
    Private _Level3 As String
    Private _Level4 As String
    Private _Level5 As String
    Private _Level6 As String
    Private _Level7 As String
    Private _Level8 As String
    Private _Level9 As String
    Private _Level10 As String

    Private _Createdby As String
    Private _LstModiBy As String
    Private _CreatedDt As DateTime
    Private _LstModiDt As DateTime
    Private _Ismodi As Boolean
    
    Private _picture As Byte()

    ' Properties are added By Ashok For Item Display On 20/10/2013
    Private strInventoryType As String '-- This Variable holds the Sting .. >> IP,IS,SR,PR,DOC,DOS,QTI,QTR,ENQ,PHI,PHO
    Private intItemid As Integer  'This Variable holds the Item Id (Product Id) to be searched
    Private intCustomerOrSupplierId As Integer  'This Variable holds the Customer or supplier Id
    Private _datefrom As Date
    Private _dateto As Date
    Private _branch As String
    Private _rawmetid As Long
    Private _Ritemid As Long
    Private _Rawqty As Double
    Public Property rawmetid() As Long
        Get
            Return _rawmetid
        End Get
        Set(ByVal value As Long)
            _rawmetid = value
        End Set
    End Property
    Public Property Ritemid() As Long
        Get
            Return _Ritemid
        End Get
        Set(ByVal value As Long)
            _Ritemid = value
        End Set
    End Property
    Public Property Rawqty() As Double
        Get
            Return _Rawqty
        End Get
        Set(ByVal value As Double)
            _Rawqty = value
        End Set
    End Property


    Public Property picture() As Byte()
        Get
            Return _picture
        End Get
        Set(ByVal value As Byte())
            _picture = value
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
    Public Property ItemCode() As String
        Get
            Return _ItemCode
        End Get
        Set(ByVal value As String)
            _ItemCode = value
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
    Public Property Descr() As String
        Get
            Return _Descr
        End Get
        Set(ByVal value As String)
            _Descr = value
        End Set
    End Property
    Public Property Category() As String
        Get
            Return _Category
        End Get
        Set(ByVal value As String)
            _Category = value
        End Set
    End Property
    Public Property opnQty() As Double
        Get
            Return _opnQty
        End Get
        Set(ByVal value As Double)
            _opnQty = value
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
    Public Property salesPrice() As Double
        Get
            Return _salesPrice
        End Get
        Set(ByVal value As Double)
            _salesPrice = value
        End Set
    End Property
    Public Property WSalesPrice() As Double
        Get
            Return _WSalesPrice
        End Get
        Set(ByVal value As Double)
            _WSalesPrice = value
        End Set
    End Property
    Public Property MinQty() As Double
        Get
            Return _MinQty
        End Get
        Set(ByVal value As Double)
            _MinQty = value
        End Set
    End Property
    Public Property P1Unit() As String
        Get
            Return _P1Unit
        End Get
        Set(ByVal value As String)
            _P1Unit = value
        End Set
    End Property
    Public Property P2Unit() As String
        Get
            Return _P2Unit
        End Get
        Set(ByVal value As String)
            _P2Unit = value
        End Set
    End Property
    Public Property P1Fra() As Integer
        Get
            Return _P1Fra
        End Get
        Set(ByVal value As Integer)
            _P1Fra = value
        End Set
    End Property
    Public Property P2Fra() As Integer
        Get
            Return _P2Fra
        End Get
        Set(ByVal value As Integer)
            _P2Fra = value
        End Set
    End Property
    Public Property Level1() As String
        Get
            Return _Level1
        End Get
        Set(ByVal value As String)
            _Level1 = value
        End Set
    End Property
    Public Property Level2() As String
        Get
            Return _Level2
        End Get
        Set(ByVal value As String)
            _Level2 = value
        End Set
    End Property
    Public Property Level3() As String
        Get
            Return _Level3
        End Get
        Set(ByVal value As String)
            _Level3 = value
        End Set
    End Property
    Public Property Level4() As String
        Get
            Return _Level4
        End Get
        Set(ByVal value As String)
            _Level4 = value
        End Set
    End Property
    Public Property Level5() As String
        Get
            Return _Level5
        End Get
        Set(ByVal value As String)
            _Level5 = value
        End Set
    End Property
    Public Property Level6() As String
        Get
            Return _Level6
        End Get
        Set(ByVal value As String)
            _Level6 = value
        End Set
    End Property
    Public Property Level7() As String
        Get
            Return _Level7
        End Get
        Set(ByVal value As String)
            _Level7 = value
        End Set
    End Property
    Public Property Level8() As String
        Get
            Return _Level8
        End Get
        Set(ByVal value As String)
            _Level8 = value
        End Set
    End Property
    Public Property Level9() As String
        Get
            Return _Level9
        End Get
        Set(ByVal value As String)
            _Level9 = value
        End Set
    End Property
    Public Property Level10() As String
        Get
            Return _Level10
        End Get
        Set(ByVal value As String)
            _Level10 = value
        End Set
    End Property
    Public Property Createdby() As String
        Get
            Return _Createdby
        End Get
        Set(ByVal value As String)
            _Createdby = value
        End Set
    End Property
    Public Property LstModiBy() As String
        Get
            Return _LstModiBy
        End Get
        Set(ByVal value As String)
            _LstModiBy = value
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
    Public Property LstModiDt() As DateTime
        Get
            Return _LstModiDt
        End Get
        Set(ByVal value As DateTime)
            _LstModiDt = value
        End Set
    End Property
    Public Property Ismodi() As Boolean
        Get
            Return _Ismodi
        End Get
        Set(ByVal value As Boolean)
            _Ismodi = value
        End Set
    End Property

    Public Property Branch() As String
        Get
            Return _branch
        End Get
        Set(ByVal value As String)
            _branch = value
        End Set
    End Property

    Public Property InventoryType() As String
        Get
            Return strInventoryType
        End Get
        Set(ByVal value As String)
            strInventoryType = value
        End Set
    End Property
    Public Property DateFrom() As Date
        Get
            Return _datefrom
        End Get
        Set(ByVal value As Date)
            _datefrom = value
        End Set
    End Property
    Public Property DateTo() As Date
        Get
            Return _dateto
        End Get
        Set(ByVal value As Date)
            _dateto = value
        End Set
    End Property
    Public Property CustomerOrSupplierId() As Integer
        Get
            Return intCustomerOrSupplierId
        End Get
        Set(ByVal value As Integer)
            intCustomerOrSupplierId = value
        End Set
    End Property
    Public Property Make() As String
        Get
            Return _Make
        End Get
        Set(ByVal value As String)
            _Make = value
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
    Public Property vat() As String
        Get
            Return _vat
        End Get
        Set(ByVal value As String)
            _vat = value
        End Set
    End Property
    Public Property MinSalesPrice() As Double
        Get
            Return _MinSalesPrice
        End Get
        Set(ByVal value As Double)
            _MinSalesPrice = value
        End Set
    End Property
    Private _hsncode As String
    Public Property hsncode() As String
        Get
            Return _hsncode
        End Get
        Set(ByVal value As String)
            _hsncode = value
        End Set
    End Property
    Private _MRP As Double
    Public Property MRP() As Double
        Get
            Return _MRP
        End Get
        Set(ByVal value As Double)
            _MRP = value
        End Set
    End Property
    Private _webname As String
    Public Property webname() As String
        Get
            Return _webname
        End Get
        Set(ByVal value As String)
            _webname = value
        End Set
    End Property
    Private _aboveTenTon As Double
    Public Property aboveTenTon() As Double
        Get
            Return _aboveTenTon
        End Get
        Set(ByVal value As Double)
            _aboveTenTon = value
        End Set
    End Property
    Private _rack As String
    Public Property Rack() As String
        Get
            Return _rack
        End Get
        Set(ByVal value As String)
            _rack = value
        End Set
    End Property
    Private _secondPrice As Double
    Public Property secondPrice() As Double
        Get
            Return _secondPrice
        End Get
        Set(ByVal value As Double)
            _secondPrice = value
        End Set
    End Property
    Private _mechineItemcode As String
    Public Property mechineItemcode() As String
        Get
            Return _mechineItemcode
        End Get
        Set(ByVal value As String)
            _mechineItemcode = value
        End Set
    End Property
    Private _additionalcess As Double
    Public Property additionalcess() As Double
        Get
            Return _additionalcess
        End Get
        Set(ByVal value As Double)
            _additionalcess = value
        End Set
    End Property
    Private _regularcess As String
    Public Property regularcess() As String
        Get
            Return _regularcess
        End Get
        Set(ByVal value As String)
            _regularcess = value
        End Set
    End Property
    Private _wmcalculation As Double
    Public Property wmcalculation() As Double
        Get
            Return _wmcalculation
        End Get
        Set(ByVal value As Double)
            _wmcalculation = value
        End Set
    End Property
    Private _carrier As String
    Public Property carrier() As String
        Get
            Return _carrier
        End Get
        Set(ByVal value As String)
            _carrier = value
        End Set
    End Property
    Private _salescountForPoint As Integer
    Public Property salescountForPoint() As Integer
        Get
            Return _salescountForPoint
        End Get
        Set(ByVal value As Integer)
            _salescountForPoint = value
        End Set
    End Property
    Private _salesPontOncount As Integer
    Public Property salesPontOncount() As Integer
        Get
            Return _salesPontOncount
        End Get
        Set(ByVal value As Integer)
            _salesPontOncount = value
        End Set
    End Property
    Private _Price1 As Double
    Public Property Price1() As Double
        Get
            Return _Price1
        End Get
        Set(ByVal value As Double)
            _Price1 = value
        End Set
    End Property
    Private _Price2 As Double
    Public Property Price2() As Double
        Get
            Return _Price2
        End Get
        Set(ByVal value As Double)
            _Price2 = value
        End Set
    End Property
    Private _Price3 As Double
    Public Property Price3() As Double
        Get
            Return _Price3
        End Get
        Set(ByVal value As Double)
            _Price3 = value
        End Set
    End Property
    Private _Price4 As Double
    Public Property Price4() As Double
        Get
            Return _Price4
        End Get
        Set(ByVal value As Double)
            _Price4 = value
        End Set
    End Property
    Private _Price5 As Double
    Public Property Price5() As Double
        Get
            Return _Price5
        End Get
        Set(ByVal value As Double)
            _Price5 = value
        End Set
    End Property
    Private _Price6 As Double
    Public Property Price6() As Double
        Get
            Return _Price6
        End Get
        Set(ByVal value As Double)
            _Price6 = value
        End Set
    End Property
    Private _Price7 As Double
    Public Property Price7() As Double
        Get
            Return _Price7
        End Get
        Set(ByVal value As Double)
            _Price7 = value
        End Set
    End Property
    Private _Price8 As Double
    Public Property Price8() As Double
        Get
            Return _Price8
        End Get
        Set(ByVal value As Double)
            _Price8 = value
        End Set
    End Property
    Private _Price9 As Double
    Public Property Price9() As Double
        Get
            Return _Price9
        End Get
        Set(ByVal value As Double)
            _Price9 = value
        End Set
    End Property
    Private _Price10 As Double
    Public Property Price10() As Double
        Get
            Return _Price10
        End Get
        Set(ByVal value As Double)
            _Price10 = value
        End Set
    End Property
    Private _itemlistorder As Double
    Public Property itemlistorder() As Double
        Get
            Return _itemlistorder
        End Get
        Set(ByVal value As Double)
            _itemlistorder = value
        End Set
    End Property
#End Region
    Public Function _ldItmMast() As DataTable
        Return _cmnMthds.fLoadDatatable("ldItemMasterWithOutParam", True)
    End Function
    Public Function ldmst() As IDataReader
        Return _cmnMthds.LoadData("ldItemMasterWithOutParam", True)
    End Function
    Public Function ldmst(ByVal cmdText As String) As IDataReader
        Return _cmnMthds.LoadData(cmdText, False)
    End Function
    Public Function _saveItemMast() As Long
        Dim param As String(,) = New String(54, 2) { _
                                                {"@ItemId", ItemId, ""}, _
                                                {"@ItemCode", ItemCode, ""}, _
                                                {"@Unit", Unit, ""}, _
                                                {"@Descr", Descr, ""}, _
                                                 {"@itemCategory", Category, ""}, _
                                                 {"@opQty", opnQty, ""}, _
                                                 {"@OpCost", OpnCost, ""}, _
                                                 {"@salesPrice", salesPrice, ""}, _
                                                 {"@WSalesPrice", WSalesPrice, ""}, _
                                                 {"@MinQty", MinQty, ""}, _
                                                 {"@P1Unit", P1Unit + "", ""}, _
                                                 {"@P2Unit", Trim(P2Unit + ""), ""}, _
                                                 {"@P1Fra", P1Fra, ""}, _
                                                 {"@P2Fra", P2Fra, ""}, _
                                                 {"@Leveltxt1", Trim(Level1 + ""), ""}, _
                                                 {"@Leveltxt2", Trim(Level2 + ""), ""}, _
                                                 {"@Leveltxt3", Trim(Level3 + ""), ""}, _
                                                 {"@Leveltxt4", Trim(Level4 + ""), ""}, _
                                                 {"@Leveltxt5", Trim(Level5 + ""), ""}, _
                                                 {"@Leveltxt6", Trim(Level6 + ""), ""}, _
                                                 {"@Leveltxt7", Trim(Level7 + ""), ""}, _
                                                 {"@Leveltxt8", Trim(Level8 + ""), ""}, _
                                                 {"@Leveltxt9", Trim(Level9 + ""), ""}, _
                                                 {"@Leveltxt10", Trim(Level10 + ""), ""}, _
                                                 {"@Createdby", Createdby + "", ""}, _
                                                 {"@LstModiBy", LstModiBy + "", ""}, _
                                                 {"@CreatedDt", CreatedDt, "d"}, _
                                                 {"@LstModiDt", LstModiDt, "d"}, _
                                                 {"@ISModi", IIf(Ismodi, 1, 0), ""}, _
                                                 {"@Make", Trim(Make & ""), ""}, _
                                                 {"@Model", Trim(Model & ""), ""}, _
                                                 {"@vat", Trim(vat & ""), ""}, _
                                                 {"@hsncode", Trim(hsncode & ""), ""}, _
                                                 {"@mrp", CDbl(MRP & ""), ""}, _
                                                 {"@aboveTenTon", aboveTenTon, ""}, _
                                                 {"@Rack", Trim(Rack & ""), ""}, _
                                                 {"@secondPrice", secondPrice, ""}, _
                                                 {"@mechineItemcode", Trim(mechineItemcode & ""), ""}, _
                                                 {"@additionalcess", Val(additionalcess & ""), ""}, _
                                                 {"@regularcess", Trim(regularcess & ""), ""}, _
                                                 {"@wmcalculation", wmcalculation, ""}, _
                                                 {"@carrier", Trim(carrier & ""), ""}, _
                                                 {"@salescountForPoint", salescountForPoint, ""}, _
                                                 {"@salesPontOncount", salesPontOncount, ""}, _
                                                 {"@Price1", Price1, ""}, _
                                                 {"@Price2", Price2, ""}, _
                                                 {"@Price3", Price3, ""}, _
                                                 {"@Price4", Price4, ""}, _
                                                 {"@Price5", Price5, ""}, _
                                                 {"@Price6", Price6, ""}, _
                                                 {"@Price7", Price7, ""}, _
                                                 {"@Price8", Price8, ""}, _
                                                 {"@Price9", Price9, ""}, _
                                                 {"@Price10", Price10, ""}, _
                                                 {"@itemlistorder", itemlistorder, ""}}

        Dim rtnId As Object = _cmnMthds._ExecuteScalar(param, "sp_ProductSaveorModi", 55)
        Return Val(rtnId.ToString)
    End Function
#Region "ItemProperties Vlues"
    Private _isDuealSerialNo As Boolean
    Public Property isDuealSerialNo() As Boolean
        Get
            Return _isDuealSerialNo
        End Get
        Set(ByVal value As Boolean)
            _isDuealSerialNo = value
        End Set
    End Property
    Private _isSerialNo As Boolean
    Public Property isSerialNo() As Boolean
        Get
            Return _isSerialNo
        End Get
        Set(ByVal value As Boolean)
            _isSerialNo = value
        End Set
    End Property
    Private _ismanufacturing As Boolean
    Public Property ismanufacturing() As Boolean
        Get
            Return _ismanufacturing
        End Get
        Set(ByVal value As Boolean)
            _ismanufacturing = value
        End Set
    End Property

#End Region
    Public Sub saveItemProperties()
        Dim strParam As String(,) = New String(3, 2) { _
                                           {"@itemid", ItemId, ""}, _
                                           {"@isSerialNo", isSerialNo, ""}, _
                                           {"@isDuealSerialNo", isDuealSerialNo, ""}, _
                                           {"@ismanufacturing", ismanufacturing, ""}}
        _cmnMthds._ExecuteNonQuery(strParam, "saveItemProperties", 4)
    End Sub
    Public Sub clsCnnection()
        _cmnMthds.clsConnection()
    End Sub
    Public Sub clsreader()
        _cmnMthds.CloseReader()
    End Sub
    Public Function returnLastItemCode() As String
        Dim dt As DataTable
        dt = _cmnMthds.fLoadDatatable("returnLastItemCode", True)
        If dt.Rows.Count > 0 Then
            Return dt(0)("Item Code")
        Else
            Return ""
        End If
    End Function
    Public Function returnTransactionHistoryForItem(ByVal tp As Integer, ByVal jobcode As String) As DataTable
        Dim strParam As String(,) = New String(6, 2) { _
                                            {"@strInventoryType", InventoryType, ""}, _
                                            {"@intItemid", ItemId, ""}, _
                                            {"@intCustomerOrSupplierId", CustomerOrSupplierId, ""}, _
                                            {"@DateFrom", DateFrom, "d"}, _
                                            {"@Dateto", DateTo, "d"}, _
                                            {"@jobcode", jobcode, ""}, _
                                            {"@Tp", tp, ""}}

        Return _cmnMthds.fLoadDatatable(strParam, "[returnTransactionHistoryForItem]", 7)

    End Function
    Public Function returnItemdetailsForPricemanagement(ByVal tp As Integer) As DataTable
        Dim strParam As String(,) = New String(1, 2) { _
                           {"@tp", tp, ""}, _
                           {"@dloc", Trim(Dloc & ""), ""}}
        Return _cmnMthds.fLoadDatatable(strParam, "returnItemdetailsForPricemanagement", 2)
    End Function
    Public Function updatePricemanagement() As Long
        Dim param As String(,) = New String(17, 2) { _
                                                {"@ItemCode", ItemCode, ""}, _
                                                {"@Description", Descr, ""}, _
                                                {"@Unit", Unit, ""}, _
                                                 {"@QtyOpn", opnQty, ""}, _
                                                 {"@CostOpen", OpnCost, ""}, _
                                                 {"@UnitPrice", salesPrice, ""}, _
                                                 {"@UnitPriceWS", WSalesPrice, ""}, _
                                                 {"@MinSalesPrice", MinSalesPrice, ""}, _
                                                 {"@MRP", MRP, ""}, _
                                                 {"@Hsncode", hsncode, ""}, _
                                                 {"@ItemId", ItemId, ""}, _
                                                 {"@webname", webname, ""}, _
                                                 {"@vat", vat, ""}, _
                                                 {"@Rack", Rack, ""}, _
                                                 {"@secondPrice", secondPrice, ""}, _
                                                 {"@rgcess", Trim(regularcess & ""), ""}, _
                                                 {"@additionalcess", additionalcess, ""}, _
                                                 {"@dloc", Trim(Dloc & ""), ""}}
        _cmnMthds._ExecuteNonQuery(param, "updatePricemanagement", 18)
    End Function
    Public Function checkIsSerialnumberAvailable(ByVal serialno As String, ByVal isdual As Boolean, ByVal ItemId As Long) As DataTable
        Dim strParam As String(,) = New String(2, 2) { _
                                            {"@serialno", serialno, ""}, _
                                            {"@isdual", isdual, ""}, _
                                            {"@itemid", ItemId, ""}}

        Return _cmnMthds.fLoadDatatable(strParam, "checkIsSerialnumberAvailable", 3)

    End Function
    Public Function saveRawMeterial() As Long
        Dim param As String(,) = New String(3, 2) { _
                                                {"@rawmetid", rawmetid, ""}, _
                                                {"@Ritemid", Ritemid, ""}, _
                                                {"@Rawqty", Rawqty, ""}, _
                                                {"@itemid", ItemId, ""}}
        _cmnMthds._ExecuteNonQuery(param, "saveRawMeterial", 4)
    End Function
    Public Function returnRawMaterial(ByVal ItemId As Long) As DataTable
        Dim strParam As String(,) = New String(0, 2) { _
                                            {"@itemid", ItemId, ""}}
        Return _cmnMthds.fLoadDatatable(strParam, "returnRawMaterial", 1)

    End Function
    Public Function returnRawmaterialFromTransaction(ByVal trid As Long) As DataTable
        Dim strParam As String(,) = New String(0, 2) { _
                                           {"@trid", trid, ""}}

        Return _cmnMthds.fLoadDatatable(strParam, "returnRawmaterialFromTransaction", 1)
    End Function
    Public Function returnBatchItems(ByVal ItemId As Long, Optional ByVal tp As Integer = 0) As DataTable
        Dim strParam As String(,) = New String(1, 2) { _
                                            {"@itemid", ItemId, ""}, _
                                            {"@tp", tp, ""}}

        Return _cmnMthds.fLoadDatatable(strParam, "returnBatchItems", 2)

    End Function
End Class
