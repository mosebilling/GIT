Module LoadMastersOnFormLoad
    Private _objcmnbLayer As New clsCommon_BL
    Public Function loadInventoryFormLoadMasters(ByVal isloadcaption As Boolean, ByVal vrTypeNo As Integer, _
                                            ByVal CmnVrFldName As String, ByVal DiscAccSetId As Integer, _
                                            ByRef discountAcc As Long, ByRef TrTypeNo As Integer) As DataSet
        Dim qry As String
        Dim userInvnos As String
        Dim prefixacc As String
        Dim discount As String
        If UsrBr = "" Then
            discount = " Select accid from accmast where AccSetId like '%" & Format(DiscAccSetId, "00") & "%'"
        Else
            discount = " SELECT accid FROM BranchAccSet WHERE branchcode='" & UsrBr & "' and setno=" & DiscAccSetId
        End If
        prefixacc = " select Alias,AccDescr,GrpSetOn,accid from (select ANo,VrTypeNo from PreFixTb group by VrTypeNo,ANo)PreFixTb inner join (SELECT Alias,AccDescr,GrpSetOn,accid FROM AccMast " & _
                                                "LEFT JOIN S1AccHd ON AccMast.S1AccId=S1AccHd.S1AccId) acc1 on PreFixTb.ANo=acc1.accid WHERE VrTypeNo = " & vrTypeNo

        prefixacc = prefixacc & " Union all select Alias,AccDescr,GrpSetOn,accid from (select ANo2,VrTypeNo from PreFixTb group by VrTypeNo,ANo2)PreFixTb inner join (SELECT Alias,AccDescr,GrpSetOn,accid FROM AccMast " & _
                                                "LEFT JOIN S1AccHd ON AccMast.S1AccId=S1AccHd.S1AccId) acc2 on PreFixTb.ANo2=acc2.accid WHERE VrTypeNo = " & vrTypeNo

        userInvnos = IIf(UsrBr = "", " SELECT * FROM InvNos WHERE InvType='" & CmnVrFldName & "'", " SELECT * FROM InvNosBrTb WHERE Brcode='" & UsrBr & "' AND InvType='" & CmnVrFldName & "'")

        qry = "SELECT WarrentyName,Wid FROM WarrentyMasterTb"
        qry = qry & " SELECT SManCode FROM SalesmanTb " & _
                "left join BranchTb on BranchTb.branchid=SalesmanTb.branchid " & IIf(UsrBr = "", "", "where Branchcode='" & UsrBr & "'")
        qry = qry & " Select * from AdditionalInfoCaptionTb"
        qry = qry & " SELECT CurrencyCode FROM CurrencyTb"
        qry = qry & " SELECT LocCode FROM LocationTb"
        qry = qry & " SELECT * FROM PreFixTb WHERE VrTypeNo = " & vrTypeNo & IIf(UsrBr = "", "", " AND BrId In ('', '" & UsrBr & "')") & " Order by ordNo"
        qry = qry & userInvnos & prefixacc & discount
        qry = qry & " SELECT * FROM VoucherTypeNoTb WHERE VoucherType='" & CmnVrFldName & "'"
        Dim dtset As DataSet
        dtset = _objcmnbLayer._ldDataset(qry, False)
        If EnableWarranty Then
            dtwarrenty = dtset.Tables(0)
        End If
        dtsalesman = dtset.Tables(1)
        If isloadcaption Then
            CaptionTb = dtset.Tables(2)
        End If
        dtcurrentyTb = dtset.Tables(3)
        dtlocationTb = dtset.Tables(4)
        PreFixTb = dtset.Tables(5)
        dtInvNos = dtset.Tables(6)
        dtAcc = dtset.Tables(7)
        If dtset.Tables(8).Rows.Count > 0 Then
            discountAcc = Val(dtset.Tables(8)(0)(0) & "")
        End If
        If dtset.Tables(9).Rows.Count > 0 Then
            TrTypeNo = Val(dtset.Tables(9)(0)(0) & "")
        End If
        Return dtset
    End Function
    Public Sub loadDocumentMasters(ByVal CmnVrFldName As String, Optional ByRef quoteTerms As String = "")
        Dim qry As String
        qry = IIf(UsrBr = "", " SELECT * FROM InvNos WHERE InvType='" & CmnVrFldName & "'", " SELECT * FROM InvNosBrTb WHERE Brcode='" & UsrBr & "' AND InvType='" & CmnVrFldName & "'")
        qry = qry & " SELECT SManCode FROM SalesmanTb"
        qry = qry & " SELECT CurrencyCode FROM CurrencyTb"
        qry = qry & " SELECT LocCode FROM LocationTb"
        qry = qry & " SELECT quoteTerms FROM CompanyTb"
        Dim dtset As DataSet
        dtset = _objcmnbLayer._ldDataset(qry, False)
        dtInvNos = dtset.Tables(0)
        dtsalesman = dtset.Tables(1)
        dtcurrentyTb = dtset.Tables(2)
        dtlocationTb = dtset.Tables(3)
        If dtset.Tables(4).Rows.Count > 0 Then
            quoteTerms = Trim(dtset.Tables(4)(0)("quoteTerms") & "")
        End If
    End Sub
End Module
