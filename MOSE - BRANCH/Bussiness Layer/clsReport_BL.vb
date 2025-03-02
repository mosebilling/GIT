Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DataAccessLayer
Imports System.Data
Public Class clsReport_BL
    Private _cmnDlink As New cl_CmnDataLink()
    Private _cmnMthds As New CmnMethods()
    Public Function returnDetailsForReport(ByVal Condition As String, ByVal dateFlds As String, ByVal cmdText As String, ByVal tp As Integer, Optional ByVal AccDate1 As String = "", Optional ByVal AccDate2 As String = "", Optional ByVal pmcnt As Integer = 3) As DataSet
        Dim param As String(,) = New String(4, 2) {{"@Condition", Condition, ""}, _
                                                   {"@DateFlds", dateFlds, ""}, _
                                                   {"@tp", tp, ""}, _
                                                   {"@AccDate1", AccDate1, ""}, _
                                                   {"@AccDate2", AccDate2, ""}}
        Return _cmnMthds._ldDataset(param, cmdText, pmcnt)
    End Function
    Public Function returnItemQuantityReport(ByVal Condition As String, ByVal Flds As String, ByVal cmdText As String, ByVal dtno As Integer, ByVal trdate As Date, ByVal setbatchwise As Integer, ByVal setlocationwise As Integer)
        Dim param As String(,) = New String(6, 2) {{"@Condition", Condition, ""}, _
                                                   {"@Flds", Flds, ""}, _
                                                   {"@Trdateno", dtno, ""}, _
                                                   {"@Trdate", trdate, "d"}, _
                                                   {"@setbatchwise", setbatchwise, ""}, _
                                                   {"@setlocationwise", setlocationwise, ""}, _
                                                   {"@Loc", IIf(UsrBr = "", "", Dloc), ""}}
        Return _cmnMthds._ldDataset(param, cmdText, 7)
    End Function
    Public Function returnStockLedger(ByVal itemid As Integer, ByVal datefrom As Date, ByVal dateTo As Date) As DataSet
        Dim param As String(,) = New String(3, 2) {{"@ITEMID", itemid, ""}, _
                                                   {"@DateFrom", datefrom, "d"}, _
                                                   {"@DateTo", dateTo, "d"}, _
                                                   {"@Loc", IIf(UsrBr = "", "", Dloc), ""}}
        Return _cmnMthds._ldDataset(param, "returnStockLedger", 4)
    End Function
    Public Function returnCustomerSupplierList(ByVal Condition As String) As DataSet
        Dim param As String(,) = New String(0, 2) {{"@GrpSet", Condition, ""}}
        Return _cmnMthds._ldDataset(param, "returnAccountDetails", 1)
    End Function
    Public Function returnItemCostbySupplier(ByVal Itemid As Long, ByVal tp As Integer) As DataSet
        Dim param As String(,) = New String(1, 2) {{"@Itemid", Itemid, ""}, _
                                                   {"@tp", tp, ""}}
        Return _cmnMthds._ldDataset(param, "returnItemCostbySupplier", 2)

    End Function
    Public Function returnItemwiseTransactionList(ByVal Itemid As Long, ByVal AccountNO As Integer, ByVal dateFrom As Date, ByVal DateTo As Date, ByVal TrType As String, ByVal Tp As Integer) As DataSet
        Dim param As String(,) = New String(6, 2) { _
                                                  {"@DateFrom", dateFrom, "d"}, _
                                                  {"@DateTo", DateTo, "d"}, _
                                                  {"@AccountNO", AccountNO, ""}, _
                                                  {"@Itemid", Itemid, ""}, _
                                                  {"@TrType", TrType, ""}, _
                                                  {"@Tp", Tp, ""}, _
                                                  {"@brid", UsrBr, ""}}
        Return _cmnMthds._ldDataset(param, "returnItemwiseTransactionList", 7)
    End Function
    Public Function returnItemPriceList(ByVal Itemid As Long, ByVal AccountNO As Long, ByVal tp As Integer) As DataSet
        Dim param As String(,) = New String(2, 2) {{"@Itemid", Itemid, ""}, _
                                                  {"@AccountNO", AccountNO, ""}, _
                                                  {"@tp", tp, ""}}
        Return _cmnMthds._ldDataset(param, "returnItemPriceList", 3)
    End Function
    Public Function returnItemsToListForm(ByVal cmdText As String, ByVal DocId As Long, ByVal tp As Integer) As DataSet
        Dim param As String(,) = New String(1, 2) {{"@DocId", DocId, ""}, _
                                                   {"@tp", tp, ""}}
        Return _cmnMthds._ldDataset(param, cmdText, 2)
    End Function
    Public Function returnFuelItemsToListForm(ByVal cmdText As String, ByVal DocId As Long, ByVal tp As Integer) As DataSet
        Dim param As String(,) = New String(1, 2) {{"@DocId", DocId, ""}, _
                                                   {"@tp", tp, ""}}
        Return _cmnMthds._ldDataset(param, cmdText, 2)
    End Function
    Public Function returnIMEINos(ByVal dateFrom As Date, ByVal dateto As Date, ByVal tp As Integer) As DataSet
        Dim param As String(,) = New String(2, 2) {{"@DateFrom", dateFrom, "d"}, _
                                                   {"@Dateto", dateto, "d"}, _
                                                   {"@tp", tp, ""}}
        Return _cmnMthds._ldDataset(param, "returnIMEINos", 3)
    End Function
    Public Function returnDaybook(ByVal tp As Integer, ByVal dateFrom As Date, ByVal dateto As Date) As DataSet
        Dim param As String(,) = New String(2, 2) {{"@datefrom", dateFrom, "d"}, _
                                                   {"@dateto", dateto, "d"}, _
                                                   {"@tp", tp, ""}}
        Return _cmnMthds._ldDataset(param, "returnDaybook", 3)
    End Function
    Public Function returnLodgeForReports(ByVal tp As Integer, ByVal dateFrom As Date, ByVal dateto As Date, ByVal status As Integer, ByVal srchtext As String) As DataSet
        Dim param As String(,) = New String(4, 2) {{"@datefrom", dateFrom, "d"}, _
                                                   {"@dateto", dateto, "d"}, _
                                                   {"@status", status, ""}, _
                                                   {"@srchtext", srchtext, ""}, _
                                                   {"@tp", tp, ""}}
        Return _cmnMthds._ldDataset(param, "returnLodgeForReports", 5)
    End Function
    Public Function returnLodgeCheckinForPrint(ByVal Jobcode As String) As DataSet
        Dim param As String(,) = New String(0, 2) {{"@Jobcode", Jobcode, ""}}
        Return _cmnMthds._ldDataset(param, "returnLodgeCheckinForPrint", 1)
    End Function
    Public Function returnStockMovement(ByVal tp As Integer, ByVal conditin As String, ByVal dtconditin As String, ByVal cscode As String, ByVal dtfrom As String, ByVal dtto As String, ByVal wise As String, ByVal Isuptodate As String) As DataSet
        Dim param As String(,) = New String(8, 2) {{"@conditin", conditin, ""}, _
                                                   {"@dtconditin", dtconditin, ""}, _
                                                   {"@cscode", cscode, ""}, _
                                                   {"@dtfrom", dtfrom, ""}, _
                                                   {"@dtto", dtto, ""}, _
                                                   {"@wise", wise, ""}, _
                                                   {"@Isuptodate", Isuptodate, ""}, _
                                                   {"@tp", tp, ""}, _
                                                    {"@brid", UsrBr, ""}}
        Return _cmnMthds._ldDataset(param, "returnStockMovement", 9)

    End Function
    Public Function retrunSalesManCommissionForPrint(ByVal DateFrom As Date, ByVal DateTo As Date, ByVal smancode As String, ByVal tp As Integer) As DataSet
        Dim param As String(,) = New String(3, 2) {{"@dateFrom", DateFrom, "d"}, _
                                                   {"@dateTo", DateTo, "d"}, _
                                                   {"@smancode", smancode, ""}, _
                                                   {"@tp", tp, ""}}
        Return _cmnMthds._ldDataset(param, "retrunSalesManCommissionForPrint", 4)
    End Function
    Public Function returnCustomerwiseSalesAnalysis(ByVal DateFrom As Date, ByVal DateTo As Date, ByVal tp As Integer) As DataTable
        Dim param As String(,) = New String(3, 2) {{"@dateFrom", DateFrom, "d"}, _
                                                   {"@dateTo", DateTo, "d"}, _
                                                   {"@tp", tp, ""}, _
                                                   {"@brid", UsrBr, ""}}
        Return _cmnMthds._ldDataset(param, "returnCustomerwiseSalesAnalysis", 4).Tables(0)
    End Function
    Public Function returnTempleIncomeAndExpese(ByVal DateFrom As Date, ByVal DateTo As Date) As DataSet
        Dim param As String(,) = New String(1, 2) {{"@dateFrom", DateFrom, "d"}, _
                                                   {"@dateTo", DateTo, "d"}}
        Return _cmnMthds._ldDataset(param, "returnTempleIncomeAndExpese", 2)
    End Function
    Public Function returnsalesmanwisesummary(ByVal smancode As String, ByVal DateFrom As Date, ByVal DateTo As Date, ByVal tp As Integer) As DataSet
        Dim param As String(,) = New String(4, 2) {{"@SlsManId", smancode, ""}, _
                                                   {"@dfrom", DateFrom, "d"}, _
                                                   {"@dto", DateTo, "d"}, _
                                                   {"@tp", tp, ""}, _
                                                   {"@brid", UsrBr, ""}}
        Return _cmnMthds._ldDataset(param, "returnsalesmanwisesummary", 5)
    End Function
    Public Function rerturnMembershipCardForPrint(ByVal Jobcode As String) As DataSet
        Dim param As String(,) = New String(0, 2) {{"@Jobcode", Jobcode, ""}}
        Return _cmnMthds._ldDataset(param, "rerturnMembershipCardForPrint", 1)
    End Function
    Public Function returnLocationwiseQty(ByVal itemid As Integer) As DataSet
        Dim param As String(,) = New String(0, 2) {{"@itemid", itemid, ""}}
        Return _cmnMthds._ldDataset(param, "returnLocationwiseQty", 1)
    End Function
    Public Function returnDayCloseReport(ByVal DateFrom As Date, ByVal DateTo As Date, ByVal tp As Integer) As DataSet
        Dim param As String(,) = New String(3, 2) {{"@datefrom", DateFrom, "d"}, _
                                                   {"@dateto", DateTo, "d"}, _
                                                   {"@tp", tp, ""}, _
                                                   {"@brid", UsrBr, ""}}
        Return _cmnMthds._ldDataset(param, "returnDayCloseReport", 4)
    End Function
    Public Function returnRVCollectionList(ByVal DateFrom As Date, ByVal tp As Integer) As DataSet
        Dim param As String(,) = New String(2, 2) {{"@date", DateFrom, "d"}, _
                                                   {"@brid", UsrBr, ""}, _
                                                   {"@tp", tp, ""}}
        Return _cmnMthds._ldDataset(param, "returnRVCollectionList", 3)
    End Function

    Public Function ProcessfineTb(ByVal instamonth As Date) As DataSet
        Dim param As String(,) = New String(0, 2) {{"@month", instamonth, "d"}}
        Return _cmnMthds._ldDataset(param, "ProcessfineTb", 1)
    End Function
    Public Function processRestructureSettlement(ByVal restructuredate As Date) As DataSet
        Dim param As String(,) = New String(0, 2) {{"@restructuredate", restructuredate, "d"}}
        Return _cmnMthds._ldDataset(param, "processRestructureSettlement", 1)
    End Function

    Public Function LoadLoanBook(ByVal invNo As String) As DataSet
        Dim param As String(,) = New String(0, 2) {{"@invNo", invNo, ""}}
        Return _cmnMthds._ldDataset(param, "LoadLoanBook", 1)
    End Function
    Public Function loadFruitsTrayOutstanding(ByVal DateFrom As Date, ByVal DateTo As Date, ByVal tp As Integer, ByVal custid As Long, ByVal carrierid As Long) As DataSet
        Dim param As String(,) = New String(4, 2) {{"@custid", custid, ""}, _
                                                   {"@carrierid", carrierid, ""}, _
                                                   {"@datefrom", DateFrom, "d"}, _
                                                   {"@dateto", DateTo, "d"}, _
                                                   {"@tp", tp, ""}}
        Return _cmnMthds._ldDataset(param, "loadFruitsTrayOutstanding", 5)
    End Function
    Public Function returnGSTR1(ByVal datefrom As Date, ByVal dateto As Date, ByVal isb2bhsn As Integer) As DataSet
        Dim param As String(,) = New String(3, 2) {{"@datefrom", datefrom, "d"}, _
                                                   {"@dateto", dateto, "d"}, _
                                                   {"@isb2bhsn", isb2bhsn, ""}, _
                                                   {"@brid", UsrBr, ""}}
        Return _cmnMthds._ldDataset(param, "returnGSTR1", 4)
    End Function
    Public Function returnGSTR1TX(ByVal datefrom As Date, ByVal dateto As Date, ByVal isb2bhsn As Integer) As DataSet
        Dim param As String(,) = New String(3, 2) {{"@datefrom", datefrom, "d"}, _
                                                   {"@dateto", dateto, "d"}, _
                                                   {"@isb2bhsn", isb2bhsn, ""}, _
                                                   {"@brid", UsrBr, ""}}
        Return _cmnMthds._ldDataset(param, "returnGSTR1TX", 4)
    End Function
    Public Function returnMemberwiseRV(ByVal datefrom As Date) As DataSet
        Dim dt As New DataTable
        dt.Columns.Add(New DataColumn("AccountName", GetType(String)))
        dt.Columns.Add(New DataColumn("NetAmt", GetType(Double)))
        dt.Columns.Add(New DataColumn("M1", GetType(String)))
        dt.Columns.Add(New DataColumn("M2", GetType(String)))
        dt.Columns.Add(New DataColumn("M3", GetType(String)))
        dt.Columns.Add(New DataColumn("M4", GetType(String)))
        dt.Columns.Add(New DataColumn("M5", GetType(String)))
        dt.Columns.Add(New DataColumn("isdara", GetType(Integer)))
        dt.Columns.Add(New DataColumn("invno", GetType(String)))
        dt.Columns.Add(New DataColumn("trdate", GetType(Date)))
        dt.Columns.Add(New DataColumn("rvdate", GetType(Date)))
        dt.Columns.Add(New DataColumn("lnk", GetType(Integer)))

        dt.Rows.Clear()
        Dim dtlist As DataTable
        Dim param As String(,) = New String(0, 2) {{"@datefrom", datefrom, "d"}}
        dtlist = _cmnMthds._ldDataset(param, "returnMemberwiseRV", 1).Tables(0)
        Dim trid As Long = 0
        Dim i As Integer
        Dim j As Integer
        Dim drow As DataRow
        Dim drowdata As DataRow
        drow = dt.NewRow
        drowdata = dt.NewRow
        For i = 0 To dtlist.Rows.Count - 1
            If trid <> dtlist(i)("TrId") Then
                If trid > 0 Then
                    dt.Rows.Add(drow)
                    dt.Rows.Add(drowdata)
                End If
                trid = dtlist(i)("TrId")
                drow = dt.NewRow
                drowdata = dt.NewRow
                drowdata("M1") = 0
                drowdata("M2") = 0
                drowdata("M3") = 0
                drowdata("M4") = 0
                drowdata("M5") = 0
                j = 0
            End If
            drow("AccountName") = dtlist(i)("AccDescr")
            drow("NetAmt") = dtlist(i)("NetAmt")
            drow("isdara") = 0
            drow("invno") = dtlist(i)("TrRefNo")
            drow("trdate") = dtlist(i)("TrDate")
            drow("rvdate") = datefrom
            drowdata("rvdate") = datefrom
            drowdata("lnk") = 1
            drow("lnk") = 1
            drowdata("isdara") = 1

            
            Select Case j
                Case 0
                    drow("M1") = dtlist(i)("membername")
                    drowdata("M1") = dtlist(i)("rvamt")
                Case 1
                    drow("M2") = dtlist(i)("membername")
                    drowdata("M2") = dtlist(i)("rvamt")
                Case 2
                    drow("M3") = dtlist(i)("membername")
                    drowdata("M3") = dtlist(i)("rvamt")
                Case 3
                    drow("M4") = dtlist(i)("membername")
                    drowdata("M4") = dtlist(i)("rvamt")
                Case 4
                    drow("M5") = dtlist(i)("membername")
                    drowdata("M5") = dtlist(i)("rvamt")
            End Select
            j = j + 1
        Next
        If trid > 0 Then
            dt.Rows.Add(drow)
            dt.Rows.Add(drowdata)
        End If
        Dim ds As New DataSet
        ds.Tables.Add(dt)
        Return ds
    End Function
End Class
