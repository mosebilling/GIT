Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DataAccessLayer
Imports System.Data
Public Class clsCarWash
    Private _cmnDlink As New cl_CmnDataLink()
    Private _cmnMthds As New CmnMethods()
    Public Function returnCardPackages(ByVal cardnumber As String) As DataSet
        Dim param As String(,) = New String(0, 2) {{"@cardnumber", cardnumber, ""}}
        Return _cmnMthds._ldDataset(param, "returnCardPackages", 1)
    End Function
End Class
