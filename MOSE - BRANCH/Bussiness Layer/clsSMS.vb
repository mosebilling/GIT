Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DataAccessLayer
Imports System.Data
Public Class clsSMS
    Private _cmnDlink As New cl_CmnDataLink()
    Private _cmnMthds As New CmnMethods()
    Public Function returnCustomerlistForSMS(ByVal _Type As Integer, ByVal category As String) As DataTable
        Dim strParam As String(,) = New String(1, 2) { _
                                            {"@tp", _Type, ""}, _
                                            {"@category", category, ""}}
        Return _cmnMthds.fLoadDatatable(strParam, "returnCustomerlistForSMS", 2)
    End Function
End Class
