Imports DataAccessLayer

Module LoadMaster
    Private _objcmnbLayer As New clsCommon_BL
    Private _cmnMthds As New CmnMethods()
    Public Function _ldMasterDataset(ByVal tp As Integer) As DataSet
        Dim param As String(,) = New String(1, 2) {{"@username", CurrentUser, ""}, _
                                                   {"@tp", tp, ""}}
        Return _cmnMthds._ldDataset(param, "loadMasters", 2)
    End Function
End Module
