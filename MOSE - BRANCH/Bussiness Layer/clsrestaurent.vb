Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DataAccessLayer
Imports System.Data
Public Class clsrestaurent
    Private _cmnMthds As New CmnMethods()
    Public Function returnMenuItemsForDamageEntry(ByVal trdate As Integer) As DataTable
        Dim param As String(,) = New String(0, 2) {{"@dateno", trdate, ""}}
        Return _cmnMthds._ldDataset(param, "returnMenuItemsForDamageEntry", 1).Tables(0)
    End Function
    Public Function returnProductionMaterialConsumption(ByVal trdate As Date) As DataTable
        Dim param As String(,) = New String(0, 2) {{"@trdate", trdate, "d"}}
        Return _cmnMthds._ldDataset(param, "returnProductionMaterialConsumption", 1).Tables(0)
    End Function
    Public Function returnProductionMaterialConsumptionDS(ByVal trdate As Date) As DataSet
        Dim param As String(,) = New String(0, 2) {{"@trdate", trdate, "d"}}
        Return _cmnMthds._ldDataset(param, "returnProductionMaterialConsumption", 1)
    End Function
End Class
