Module SqlStatements
    Private sqlQuery As String
    Public Function tocheckDuplicatePDCEntryStr(ByVal PDCAcc As Long, ByVal ChqNo As String, ByVal BankCode As String, ByVal dType As String) As String
        sqlQuery = "Select AccTrCmn.LinkNo from AccTrCmn left join AccTrDet on AccTrCmn.linkno= AccTrDet.linkno " & _
                    "WHERE Not JVType In ('DB', 'CB', 'CR') AND PDCAcc = " & PDCAcc & _
                    " AND dealAmt " & IIf(dType = "D", ">0", "<=0") & " AND UPPER(LTRIM(RTRIM(ChqNo))) = '" & ChqNo & "' AND UPPER(BankCode) = '" & BankCode & "'"
        Return sqlQuery
    End Function
    Public Function getVouchernumberStr(ByVal vrtype As String) As String
        sqlQuery = "SELECT * FROM VoucherTypeNoTb WHERE VoucherType='" & vrtype & "'"
        Return sqlQuery
    End Function
    Public Function getAccTrDetbyloadedTrId(ByVal loadedTrId As Long, Optional ByVal othercondition As String = "", Optional ByVal removeorderby As Boolean = False) As String
        sqlQuery = "SELECT AccTrDet.*,alias,accdescr,pdcname,pdcid,GrpSetOn FROM AccTrDet " & _
                  "LEFT JOIN AccMast ON AccTrDet.Accountno=Accmast.accid " & _
                  "left join s1acchd on accmast.s1accid=s1acchd.s1accid " & _
                  "LEFT JOIN (select accid pdcid,accdescr pdcname from ACCMAST) PDC on AccTrDet.PDCAcc=pdc.pdcid " & _
                  "WHERE LinkNO=" & loadedTrId & othercondition & IIf(removeorderby, "", " ORDER BY unqno")
        Return sqlQuery
    End Function
   
End Module
