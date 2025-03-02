Public Class FinancialStatus
#Region "Form Objects"
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fRptFormat As RptFormatfrm
    Private WithEvents ftransfer As TransferToWebFrm
    Private WithEvents fwait As WaitMessageFrm
#End Region
#Region "Class Objet declarations"
    Private _objcmnbLayer As New clsCommon_BL
    Private _objTr As New clsAccountTransaction
    Private _objcmndLayer As New Dlayer
#End Region
#Region "Local Variables"
    Private PLSummTb As DataTable
    Private PLDetTb As DataTable
    Private BalShtSmmTb As DataTable
    Private BalShtDetTb As DataTable
    Private dtsummary As DataTable
    Private dsSummary As New DataSet
#End Region
    Delegate Sub GenericDelegate(ByVal Mname As String, ByVal RecName As String, ByVal rec As Integer, ByVal count As Integer)
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Dim RptType As String = ""
        Dim RptCaption As String = ""
        If _optRpt_1.Checked Then
            RptType = "OPD"
        ElseIf _optRpt_2.Checked Then
            RptType = "OPS"
        ElseIf _optRpt_4.Checked Then
            RptType = "TBS"
        ElseIf _optRpt_7.Checked Then
            RptType = "TBD"
        ElseIf _optReport_5.Checked Then
            RptType = "PLS"
        ElseIf _optReport_6.Checked Then
            RptType = "PLD"
        ElseIf _optReport_7.Checked Then
            RptType = "BSS"
        ElseIf _optReport_8.Checked Then
            RptType = "BSD"
        ElseIf rdoDaybook.Checked Then
            RptType = "DBK"

        End If
        fRptFormat = New RptFormatfrm
        fRptFormat.RptType = RptType
        fRptFormat.ShowDialog()
        fRptFormat = Nothing
    End Sub

    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        PrepareReport(RptFlName, RptCaption, forPrint)
    End Sub
    Public Sub PrepareReport(ByVal FileName As String, ByVal RptCaption As String, ByVal forpint As Boolean)
        If Not frm Is Nothing Then
            frm.Close()
            frm = New ReprtviewNEWfrm
        Else
            frm = New ReprtviewNEWfrm
        End If

        Dim ds As New DataSet
        Dim dt As DataTable = Nothing
        frm.tmpDt = Nothing
        Select Case True
            Case _optRpt_1.Checked
                ds = _objTr.returnTrialbalance(cldateto.Value, cldrstartdate.Value, 2)
                RptCaption = "Trial Balance"
            Case _optRpt_2.Checked
                ds = _objTr.returnTrialbalance(cldateto.Value, cldrstartdate.Value, 1)
                RptCaption = "Trial Balance"
            Case _optRpt_7.Checked
                ds = _objTr.returnTrialbalance(cldateto.Value, cldrstartdate.Value, 3)
                dt = _objcmnbLayer._fldDatatable("Select  1 as Lnk,'Group Wise Trial Balance As On " & DateValue(cldrstartdate.Value) & "' As AsOn From CompanyTb")
                frm.tmpDt = dt
                RptCaption = "Trial Balance"
            Case _optRpt_4.Checked
                ds = _objTr.returnTrialbalance(cldateto.Value, cldrstartdate.Value, 4)
                dt = _objcmnbLayer._fldDatatable("Select  1 as Lnk,'Summary Of Group Wise Trial Balance As On " & DateValue(cldrstartdate.Value) & "' As AsOn From CompanyTb")
                frm.tmpDt = dt
                RptCaption = "Trial Balance"
            Case _optReport_5.Checked
                setPLSummTb()
                ds.Tables.Add(PLSummTb)
                dt = _objcmnbLayer._fldDatatable("Select  1 as Lnk, '" & DateValue(cldrstartdate.Value) & "' As DateFrom,'" & DateValue(cldateto.Value) & "' DateTo From CompanyTb")
                RptCaption = "Profit & Loss"
                ds.Tables.Add(dt)
            Case _optReport_6.Checked
                setPLDetTb()
                ds.Tables.Add(PLDetTb)
                dt = _objcmnbLayer._fldDatatable("Select  1 as Lnk, '" & DateValue(cldrstartdate.Value) & "' As DateFrom,'" & DateValue(cldateto.Value) & "' DateTo From CompanyTb")
                ds.Tables.Add(dt)
                RptCaption = "Profit & Loss"
            Case _optReport_7.Checked
                setBSSummTb()
                ds.Tables.Add(BalShtSmmTb)
                dt = _objcmnbLayer._fldDatatable("Select  1 as Lnk, '" & DateValue(cldrstartdate.Value) & "' As DateFrom From CompanyTb")
                ds.Tables.Add(dt)
                RptCaption = "Balance Sheet"
            Case _optReport_8.Checked
                setBSDetTb()
                ds.Tables.Add(BalShtDetTb)
                dt = _objcmnbLayer._fldDatatable("Select  1 as Lnk, '" & DateValue(cldrstartdate.Value) & "' As DateFrom From CompanyTb")
                ds.Tables.Add(dt)
                RptCaption = "Balance Sheet"
            Case rdoDaybook.Checked
                ds = _objTr.returnTrialbalance(cldrstartdate.Value, cldateto.Value, 5)
                dt = _objcmnbLayer._fldDatatable("Select  1 as Lnk,'Day book from " & DateValue(cldrstartdate.Value) & " To " & DateValue(cldateto.Value) & "' As AsOn From CompanyTb")
                frm.tmpDt = dt
                RptCaption = "Day Book"
        End Select

        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = fMainForm
        frm.Text = RptCaption
        frm.Show()
    End Sub
    Private Function setPLSummTb() As Boolean
        'On Error GoTo FErr
        Dim titleTb1 As DataTable
        Dim titleTb2 As DataTable
        Dim LAmtTb As DataTable
        Dim AAmtTb As DataTable
        Dim WiseTb As DataTable
        Dim eofTb1 As Boolean
        Dim eofTb2 As Boolean
        Dim stSrch1 As Byte
        Dim stSrch2 As Byte
        Dim TL As Double
        Dim TA As Double
        Dim deptWHERE As String
        Dim deptSQL As String
        Dim branchWHERE As String
        Dim trBranchWhere As String
        Dim isOpnStock As Boolean
        Dim isClosingStock As Boolean
        Dim isDirExp As Boolean
        Dim i As Integer
        Dim j As Integer
        Dim LAmtTbPos As Integer
        Dim titleTb1Pos As Integer
        Dim AAmtTbPos As Integer
        Dim titleTb2Pos As Integer
        Dim a1 As String
        Dim a2 As String
        Dim a3 As String
        Dim quryString As String
        Dim dtRow As DataRow
        PLSummTb = _objcmnbLayer._fldDatatable("SELECT * FROM PLSummTb")
        WiseTb = _objcmnbLayer._fldDatatable("SELECT Top 1 '','' FROM CompanyTb")
        Dim WiseTbCnt As Integer = WiseTb.Rows.Count - 1
        Dim dateRange As String
        dateRange = "JVDate >= '" & Format(DateValue(cldrstartdate.Value), "yyyy/MM/dd") & "' AND JVDate <= '" & Format(DateValue(cldateto.Value), "yyyy/MM/dd") & "'"
        branchWHERE = IIf(UsrBr = "", "", " AND Branchid in ('','" & UsrBr & "')")
        trBranchWhere = IIf(UsrBr = "", "", " AND cmnbrid ='" & UsrBr & "'")
        For i = 0 To WiseTbCnt
            eofTb1 = False
            eofTb2 = False
            stSrch1 = 0
            stSrch2 = 0
            TL = 0
            TA = 0
            titleTb1Pos = 0
            titleTb2Pos = 0
            LAmtTbPos = 0
            AAmtTbPos = 0
            deptWHERE = ""
            deptSQL = ""

            'Income
            quryString = "SELECT * FROM (SELECT MAccHd.MAccId,Descr,isNull(A1,0)+isNull(A2,0) " & _
                                        "As GTtl FROM MAccHd LEFT JOIN (SELECT  (S1AccId/100)*100 As MAccId," & IIf(_chkOpt_0.CheckState, 0, "Sum(OpnBal)") & _
                                        " As A1 FROM AccMast WHERE S1AccId Between 4000 AND 4999 " & branchWHERE & " GROUP BY  (S1AccId/100)*100) As Q1 ON MAccHd.MAccId=Q1.MAccId " & _
                                       "LEFT JOIN (SELECT (accmast.AccountNo/1000000)*100 As MAccId,Sum(DealAmt) As A2 FROM  AccTrCmn " & _
                                        "LEFT JOIN AccTrDet ON AccTrCmn.LinkNo=AccTrDet.LinkNo left join accmast on AccTrDet.AccountNo=accmast.accid  WHERE isnull(approved,0)=0 and JVNum<>0 " & trBranchWhere & " AND  (accmast.AccountNo/10000) Between 4000 AND 4999  AND " & _
                                        dateRange & " GROUP BY (accmast.AccountNo/1000000)*100) As Q2 ON MAccHd.MAccId=Q2.MAccId)t WHERE " & _
                                        "MAccId Between 4000 AND 4999" & IIf(_chkOpt_1.CheckState = 0, " AND GTtl<>0", "") & _
                                        " ORDER BY MAccId,Descr"

            titleTb2 = _objcmnbLayer._fldDatatable(quryString)

            quryString = "SELECT * FROM (SELECT MAccHd.MAccId,Descr,isNull(A1,0)+isNull(A2,0) As GTtl FROM MAccHd " & _
                                                   "LEFT JOIN (SELECT (S1AccId/100)*100 As MAccId," & IIf(_chkOpt_0.CheckState, 0, "Sum(OpnBal)") & _
                                                   " As A1 FROM AccMast WHERE S1AccId Between 6000 AND 6999 " & branchWHERE & "GROUP BY (S1AccId/100)*100) As Q1 ON MAccHd.MAccId=Q1.MAccId " & _
                                                   "LEFT JOIN (SELECT (accmast.AccountNo/1000000)*100 As MAccId," & _
                                                   "Sum(DealAmt) As A2 FROM AccTrCmn LEFT JOIN AccTrDet ON AccTrCmn.LinkNo=AccTrDet.LinkNo left join accmast on AccTrDet.AccountNo=accmast.accid " & _
                                                   "WHERE isnull(approved,0)=0 and JVNum<>0 " & trBranchWhere & " AND (accmast.AccountNo/10000) Between 6000 AND 6999  AND " & _
                                                   dateRange & " GROUP BY " & "(accmast.AccountNo/1000000)*100) As Q2 ON MAccHd.MAccId=Q2.MAccId)T WHERE " & _
                                                   "MAccId Between 6000 AND 6999" & IIf(_chkOpt_1.CheckState = 0, " AND GTtl<>0", "") & _
                                                   " ORDER BY MAccId,Descr"
            titleTb1 = _objcmnbLayer._fldDatatable(quryString)

            quryString = "SELECT * FROM (SELECT MAccId,S1AccHd.S1AccId,Descr,isNull(Bal,0)+isNull(OB,0) AS Balance FROM S1AccHd " & _
                                                 "LEFT JOIN (SELECT S1AccId," & IIf(_chkOpt_0.CheckState, 0, "Sum(OpnBal)") & " As OB " & _
                                                 "FROM AccMast WHERE S1AccId Between 4000 AND 4999 " & branchWHERE & " GROUP BY S1AccId) AS A ON S1AccHd.S1AccId=A.S1AccId " & _
                                                 "LEFT JOIN (SELECT (accmast.AccountNo/10000) As S1AccId," & _
                                                 "Sum(DealAmt) AS Bal FROM AccTrCmn LEFT JOIN AccTrDet ON AccTrCmn.LinkNo=AccTrDet.LinkNo  " & _
                                                 "left join accmast on AccTrDet.AccountNo=accmast.accid " & _
                                                 "WHERE isnull(approved,0)=0 and JVNum<>0 " & trBranchWhere & " AND " & dateRange & " AND (accmast.AccountNo/10000)Between 4000 AND 4999  " & _
                                                 "GROUP BY (accmast.AccountNo/10000)) AS Q1 ON S1AccHd.S1AccId = Q1.S1AccId)T" & IIf(_chkOpt_1.CheckState = CheckState.Checked, " WHERE Balance<>0", "") & " ORDER BY MAccId,Descr"
            AAmtTb = _objcmnbLayer._fldDatatable(quryString)

            quryString = "SELECT * FROM (SELECT MAccId,S1AccHd.S1AccId,Descr," & "isNull(Bal,0)+isNull(OB,0) AS Balance FROM S1AccHd " & _
                                                 " LEFT JOIN (SELECT S1AccId," & IIf(_chkOpt_0.CheckState, 0, "Sum(OpnBal)") & " As OB FROM AccMast WHERE S1AccId Between 6000 AND 6999 " & branchWHERE & " GROUP BY S1AccId) AS A ON S1AccHd.S1AccId=A.S1AccId " & _
                                                 "LEFT JOIN (SELECT (accmast.AccountNo/10000) As S1AccId,Sum(DealAmt) AS Bal FROM AccTrCmn LEFT JOIN AccTrDet ON AccTrCmn.LinkNo=AccTrDet.LinkNo " & _
                                                 "left join accmast on AccTrDet.AccountNo=accmast.accid " & _
                                                 deptSQL & " WHERE isnull(approved,0)=0 and JVNum<>0 " & trBranchWhere & " AND " & dateRange & " AND (accmast.AccountNo/10000) Between 6000 AND  6999 GROUP BY (accmast.AccountNo/10000)) " & _
                                                 "AS Q1 ON S1AccHd.S1AccId = Q1.S1AccId)T" & IIf(_chkOpt_1.CheckState = CheckState.Checked, " WHERE Balance<>0", "") & " ORDER BY MAccId,Descr"
            LAmtTb = _objcmnbLayer._fldDatatable(quryString)

            
            If titleTb1.Rows.Count = 0 Then
                eofTb1 = True
            End If
            If titleTb2.Rows.Count = 0 Then
                eofTb2 = True
            End If
            'If numOCBal_0.Text = "" Then numOCBal_0.Text = 0
            'If numOCBal_1.Text = "" Then numOCBal_1.Text = 0
            'isOpnStock = plOS.Enabled And CDbl(numOCBal_0.Text) <> 0
            'isClosingStock = plOS.Enabled And CDbl(numOCBal_1.Text) <> 0
            isDirExp = (isOpnStock Or isClosingStock)
            stSrch1 = 0
            stSrch2 = 0
            TL = 0
            TA = 0
            With PLSummTb
                Do Until False
                    If eofTb1 And eofTb2 And Not isOpnStock And Not isClosingStock And Not isDirExp Then Exit Do
                    'ChkBreak()
                    dtRow = .NewRow
                    a1 = WiseTb(i)(0)
                    a2 = WiseTb(i)(1)
                    dtRow("DeptId") = WiseTb(i)(0) ' WiseTb!WiseId
                    dtRow("DeptDescr") = WiseTb(i)(1) 'WiseTb!Descr
                    If isOpnStock Then
                        isOpnStock = False
                        dtRow("CapLM") = "OPENING STOCK"
                        dtRow("AmtLM") = 0 'CDbl(numOCBal_0.Text)
                        a3 = 0 'CDbl(numOCBal_0.Text)
                        TL = TL + 0 'CDbl(numOCBal_0.Text)
                    ElseIf Not eofTb1 Then
                        If stSrch1 = 0 Then titleTb1Pos = 0 : GoTo LoopL
                        'LAmtTbPos = LAmtTbFindMAccId(LAmtTbPos, "MAccId", titleTb1(0)("MAccId"))
                        If stSrch1 = 1 Then
                            stSrch1 = 2
                        Else
                            LAmtTbPos = LAmtTbPos + 1
                        End If
                        If Not LAmtTbFindMAccId(LAmtTbPos, LAmtTb, "MAccId", titleTb1(titleTb1Pos)("MAccId")) Then
                            titleTb1Pos = titleTb1Pos + 1

                            If titleTb1.Rows.Count = titleTb1Pos Then
                                eofTb1 = True
                                If isClosingStock Then
                                    'isClosingStock = False
                                    'dtRow("CapLM") = "CLOSING STOCK"
                                    'dtRow("AmtLM") = -1 * CDbl(numOCBal_1.Text)
                                    'a1 = -1 * CDbl(numOCBal_1.Text)
                                    TL = TL - 0 'CDbl(numOCBal_1.Text)
                                End If
                            Else
LoopL:

                                'dtRow("MaccId") = titleTb1(titleTb1Pos)("MaccId")
                                'dtRow("MaccDescr") = titleTb1(titleTb1Pos)("Descr")
                                a2 = titleTb1(titleTb1Pos)("GTtl")
                                a3 = titleTb1(titleTb1Pos)("Descr")
                                dtRow("AmtLM") = titleTb1(titleTb1Pos)("GTtl")
                                dtRow("CapLM") = titleTb1(titleTb1Pos)("Descr")
                                stSrch1 = 1
                            End If
                        Else
                            a1 = LAmtTb(LAmtTbPos)("Descr")
                            a2 = LAmtTb(LAmtTbPos)("Balance")
                            dtRow("CapL") = LAmtTb(LAmtTbPos)("Descr")
                            dtRow("AmtL") = LAmtTb(LAmtTbPos)("Balance")
                            TL = TL + LAmtTb(LAmtTbPos)("Balance")
                        End If
                    ElseIf isClosingStock Then
                        isClosingStock = False
                        dtRow("CapLM") = "CLOSING STOCK"
                        dtRow("AmtLM").Value = -1 * 0 'CDbl(numOCBal_1.Text)
                        'a3 = -1 * 1 'CDbl(numOCBal_1.Text)
                        TL = TL - 0 'CDbl(numOCBal_1.Text)
                    ElseIf isDirExp Then
                        isDirExp = False
                        dtRow("CapLM") = "Total Direct Expense"
                        dtRow("AmtLM") = TL
                        a1 = TL
                    End If
                    If Not eofTb2 Then
                        If stSrch2 = 0 Then titleTb2Pos = 0 : GoTo LoopA
                        If stSrch2 = 1 Then
                            stSrch2 = 2
                        Else
                            AAmtTbPos = AAmtTbPos + 1
                        End If
                        If Not AAmtTbFindMAccId(AAmtTbPos, AAmtTb, "MAccId", titleTb2(titleTb2Pos)("MAccId")) Then
                            titleTb2Pos = titleTb2Pos + 1
                            If titleTb2.Rows.Count = titleTb2Pos Then
                                eofTb2 = True
                            Else
LoopA:
                                'dtRow("MaccId") = titleTb2(titleTb2Pos)("MaccId")
                                'dtRow("MaccDescr") = titleTb2(titleTb2Pos)("Descr")

                                a1 = titleTb2(titleTb2Pos)("Descr")
                                a2 = -1 * titleTb2(titleTb2Pos)("GTtl")
                                dtRow("CapAM") = titleTb2(titleTb2Pos)("Descr")
                                dtRow("AmtAM") = -1 * titleTb2(titleTb2Pos)("GTtl")
                                stSrch2 = 1
                            End If
                        Else
                            a1 = AAmtTb(AAmtTbPos)("Descr")
                            a2 = -1 * AAmtTb(AAmtTbPos)("Balance")
                            'dtRow("S1AccId") = AAmtTb(AAmtTbPos)("S1AccId")
                            dtRow("CapA") = AAmtTb(AAmtTbPos)("Descr")
                            dtRow("AmtA") = -1 * AAmtTb(AAmtTbPos)("Balance")
                            TA = TA + AAmtTb(AAmtTbPos)("Balance") * (-1)
                        End If
                    End If
                    PLSummTb.Rows.Add(dtRow)
                Loop
                If TL <> TA Then
                    dtRow = .NewRow
                    a1 = WiseTb(i)(0)
                    a2 = WiseTb(i)(1)
                    dtRow("DeptId") = WiseTb(i)(0) 'WiseTb!WiseId
                    dtRow("DeptDescr") = WiseTb(i)(1)
                    If TL - TA > 0 Then
                        dtRow("CapAM") = "Gross Loss c/f"
                        dtRow("AmtAM") = TL - TA
                        a3 = TL - TA
                    Else
                        dtRow("CapLM") = "Gross Profit c/f"
                        dtRow("AmtLM") = TA - TL
                        a3 = TA - TL
                    End If
                    PLSummTb.Rows.Add(dtRow)
                End If
                dtRow = .NewRow
                dtRow("DeptId") = WiseTb(i)(0) 'WiseTb!WiseId
                dtRow("DeptDescr") = WiseTb(i)(1) 'WiseTb!Descr
                dtRow("CapLM") = "Sub Total"
                dtRow("CapAM") = "Sub Total"
                dtRow("AmtS") = IIf(TL < TA, TA, TL)
                PLSummTb.Rows.Add(dtRow)
                If TL <> TA Then
                    dtRow = .NewRow
                    a1 = WiseTb(i)(0)
                    a2 = WiseTb(i)(1)
                    dtRow("DeptId") = WiseTb(i)(0) 'WiseTb!WiseId
                    dtRow("DeptDescr") = WiseTb(i)(1) 'WiseTb!Descr
                    If TL - TA > 0 Then
                        dtRow("CapLM") = "Gross Loss b/f"
                        dtRow("AmtLG") = TL - TA
                        TL = TL - TA
                        TA = 0
                    Else
                        dtRow("CapAM") = "Gross Profit b/f"
                        dtRow("AmtAG") = TA - TL
                        TA = TA - TL
                        TL = 0
                    End If
                    PLSummTb.Rows.Add(dtRow)
                Else
                    TA = 0
                    TL = 0
                End If
            End With
            If Not titleTb1 Is Nothing Then titleTb1.Clear() ': Set titleTb1 = Nothing
            If Not titleTb2 Is Nothing Then titleTb2.Clear() ': Set titleTb2 = Nothing
            If Not LAmtTb Is Nothing Then LAmtTb.Clear() ': Set LAmtTb = Nothing
            If Not AAmtTb Is Nothing Then AAmtTb.Clear() ': Set AAmtTb = Nothing
            quryString = "SELECT * FROM (SELECT MAccHd.MAccId,Descr,isNull(A1,0)+isNull(A2,0) " & _
                                                   "As GTtl FROM MAccHd LEFT JOIN (SELECT (S1AccId/100)*100 As MAccId," & IIf(_chkOpt_0.CheckState, 0, "Sum(OpnBal)") & _
                                                   " As A1 FROM AccMast WHERE S1AccId Between 5000 AND 5999 " & branchWHERE & "GROUP BY (S1AccId/100)*100) As Q1 ON MAccHd.MAccId=Q1.MAccId " & _
                                                   "LEFT JOIN (SELECT (accmast.AccountNo/1000000)*100 As MAccId," & _
                                                   "Sum(DealAmt) As A2 FROM  AccTrCmn LEFT JOIN AccTrDet ON AccTrCmn.LinkNo=AccTrDet.LinkNo  " & _
                                                   "left join accmast on AccTrDet.AccountNo=accmast.accid " & _
                                                   deptSQL & "WHERE isnull(approved,0)=0 and JVNum<>0 " & trBranchWhere & " AND (accmast.AccountNo/10000) Between 5000 AND 5999 AND " & dateRange & " GROUP BY " & _
                                                   "(accmast.AccountNo/1000000)*100) As Q2 ON MAccHd.MAccId=Q2.MAccId)T WHERE " & _
                                                   "MAccId Between 5000 AND 5999" & IIf(_chkOpt_1.CheckState = 0, " AND GTtl<>0", "") & _
                                                   " ORDER BY MAccId,Descr"

            titleTb2 = _objcmnbLayer._fldDatatable(quryString)

            quryString = "SELECT * FROM (SELECT MAccHd.MAccId,Descr,isNull(A1,0)+isNull(A2,0) " & _
                                                 "As GTtl FROM MAccHd LEFT JOIN (SELECT (S1AccId/100)*100 As MAccId," & IIf(_chkOpt_0.CheckState, 0, "Sum(OpnBal)") & _
                                                 " As A1 FROM AccMast WHERE S1AccId Between 7000 AND 7999 " & branchWHERE & "GROUP BY (S1AccId/100)*100) As Q1 ON MAccHd.MAccId=Q1.MAccId " & _
                                                 "LEFT JOIN (SELECT (accmast.AccountNo/1000000)*100 As MAccId," & _
                                                 "Sum(DealAmt) As A2 FROM AccTrCmn LEFT JOIN AccTrDet ON AccTrCmn.LinkNo=AccTrDet.LinkNo " & _
                                                 "left join accmast on AccTrDet.AccountNo=accmast.accid " & _
                                                 deptSQL & "WHERE isnull(approved,0)=0 and JVNum<>0 " & trBranchWhere & " AND (accmast.AccountNo/10000) Between 7000 AND 7999 AND " & dateRange & " GROUP BY " & _
                                                 "(accmast.AccountNo/1000000)*100) As Q2 ON MAccHd.MAccId=Q2.MAccId)T WHERE " & _
                                                 "MAccId Between 7000 AND 7999" & IIf(_chkOpt_1.CheckState = CheckState.Checked, " AND GTtl<>0", "") & _
                                                 " ORDER BY MAccId,Descr"

            titleTb1 = _objcmnbLayer._fldDatatable(quryString)

            quryString = "SELECT * FROM (SELECT MAccId,S1AccHd.S1AccId,Descr," & _
                                                 "isNull(Bal,0)+isNull(OB,0) AS Balance " & _
                                                 "FROM S1AccHd LEFT JOIN (SELECT S1AccId," & IIf(_chkOpt_0.CheckState, 0, "Sum(OpnBal)") & " As OB " & _
                                                 "FROM AccMast WHERE S1AccId Between 5000 AND 5999 " & branchWHERE & " GROUP BY S1AccId) AS A ON S1AccHd.S1AccId=A.S1AccId " & _
                                                 "LEFT JOIN (SELECT (accmast.AccountNo/10000) As S1AccId," & _
                                                 "Sum(DealAmt) AS Bal FROM AccTrCmn LEFT JOIN AccTrDet ON AccTrCmn.LinkNo=AccTrDet.LinkNo " & _
                                                 "left join accmast on AccTrDet.AccountNo=accmast.accid " & _
                                                 deptSQL & "WHERE isnull(approved,0)=0 and JVNum<>0 " & trBranchWhere & " AND " & dateRange & " AND (accmast.AccountNo/10000)Between 5000 AND 5999 GROUP BY (accmast.AccountNo/10000)) " & _
                                                 "AS Q1 ON S1AccHd.S1AccId = Q1.S1AccId)T" & IIf(_chkOpt_1.CheckState = 0, " WHERE Balance<>0", "") & " ORDER BY MAccId,Descr"

            AAmtTb = _objcmnbLayer._fldDatatable(quryString)

            quryString = "SELECT * FROM (SELECT MAccId,S1AccHd.S1AccId,Descr," & _
                                               "isNull(Bal,0)+isNull(OB,0) AS Balance " & _
                                               "FROM S1AccHd LEFT JOIN (SELECT S1AccId," & IIf(_chkOpt_0.CheckState, 0, "Sum(OpnBal)") & " As OB " & _
                                               "FROM AccMast WHERE S1AccId Between 5000 AND 7999 " & branchWHERE & " GROUP BY S1AccId) AS A ON S1AccHd.S1AccId=A.S1AccId " & _
                                               "LEFT JOIN (SELECT (accmast.AccountNo/10000) As S1AccId," & _
                                               "Sum(DealAmt) AS Bal FROM AccTrCmn LEFT JOIN AccTrDet ON AccTrCmn.LinkNo=AccTrDet.LinkNo " & _
                                               "left join accmast on AccTrDet.AccountNo=accmast.accid " & _
                                               deptSQL & "WHERE isnull(approved,0)=0 and JVNum<>0 " & trBranchWhere & " AND " & dateRange & " AND (accmast.AccountNo/10000) Between 7000 AND 7999 GROUP BY (accmast.AccountNo/10000)) " & _
                                               "AS Q1 ON S1AccHd.S1AccId = Q1.S1AccId)T" & IIf(_chkOpt_1.CheckState = 0, " WHERE Balance<>0", "") & " ORDER BY MAccId,Descr"

            LAmtTb = _objcmnbLayer._fldDatatable(quryString)
            eofTb1 = False
            eofTb2 = False
            titleTb1Pos = 0
            titleTb2Pos = 0
            LAmtTbPos = 0
            AAmtTbPos = 0
            If titleTb1.Rows.Count = 0 Then
                eofTb1 = True
            Else
                'titleTb1.MoveFirst()
            End If
            If titleTb2.Rows.Count = 0 Then
                eofTb2 = True
            Else
                'titleTb2.MoveFirst()
            End If
            stSrch1 = 0
            stSrch2 = 0
            With PLSummTb
                Do Until False
                    'ChkBreak()
                    dtRow = .NewRow
                    dtRow("DeptId") = WiseTb(i)(0) 'WiseTb!WiseId
                    dtRow("DeptDescr") = WiseTb(i)(1) 'WiseTb!Descr
                    If Not eofTb1 Then
                        If stSrch1 = 0 Then GoTo LoopL1
                        If stSrch1 = 1 Then
                            'LAmtTbFindMAccId("MAccId = " & titleTb1.Fields("MAccId").Value)
                            stSrch1 = 2
                            LAmtTbPos = 0
                        Else
                            'LAmtTb.FindNext("MaccId = " & titleTb1.Fields("MAccId").Value)
                            LAmtTbPos = LAmtTbPos + 1
                        End If
                        If Not LAmtTbFindMAccId(LAmtTbPos, LAmtTb, "MAccId", titleTb1(titleTb1Pos)("MAccId")) Then

                            titleTb1Pos = titleTb1Pos + 1
                            If titleTb1.Rows.Count = titleTb1Pos Then
                                eofTb1 = True
                            Else
LoopL1:
                                'dtRow("MaccId") = titleTb1(titleTb1Pos)("MaccId")
                                'dtRow("MaccDescr") = titleTb1(titleTb1Pos)("Descr")
                                dtRow("AmtLG") = titleTb1(titleTb1Pos)("GTtl")
                                dtRow("CapLM") = titleTb1(titleTb1Pos)("Descr")
                                stSrch1 = 1
                            End If
                        Else
                            dtRow("CapL") = LAmtTb(LAmtTbPos)("Descr")
                            dtRow("AmtL") = LAmtTb(LAmtTbPos)("Balance")
                            TL = TL + LAmtTb(LAmtTbPos)("Balance")
                        End If
                    End If
                    If Not eofTb2 Then
                        If stSrch2 = 0 Then GoTo LoopA1
                        If stSrch2 = 1 Then
                            stSrch2 = 2
                            AAmtTbPos = 0
                        Else
                            AAmtTbPos = AAmtTbPos + 1
                        End If
                        If Not AAmtTbFindMAccId(AAmtTbPos, AAmtTb, "MAccId", titleTb2(titleTb2Pos)("MAccId")) Then
                            titleTb2Pos = titleTb2Pos + 1
                            If titleTb2.Rows.Count = titleTb2Pos Then
                                eofTb2 = True
                            Else
LoopA1:
                                'dtRow("MaccId") = titleTb2(titleTb2Pos)("MaccId")
                                'dtRow("MaccDescr") = titleTb2(titleTb2Pos)("Descr")
                                dtRow("CapAM") = titleTb2(titleTb2Pos)("Descr")
                                dtRow("AmtAG") = -1 * titleTb2(titleTb2Pos)("GTtl")
                                stSrch2 = 1
                            End If
                        Else
                            'dtRow("S1AccId") = AAmtTb(AAmtTbPos)("S1AccId")
                            dtRow("CapA") = AAmtTb(AAmtTbPos)("Descr")
                            dtRow("AmtA") = -1 * AAmtTb(AAmtTbPos)("Balance")
                            TA = TA + AAmtTb(AAmtTbPos)("Balance") * (-1)
                        End If
                    End If
                    If eofTb1 And eofTb2 Then Exit Do
                    PLSummTb.Rows.Add(dtRow)
                Loop
                If TL <> TA Then
                    dtRow = .NewRow
                    dtRow("DeptId") = WiseTb(i)(0) 'WiseTb!WiseId
                    dtRow("DeptDescr") = WiseTb(i)(1) 'WiseTb!Descr
                    If TL - TA > 0 Then
                        dtRow("CapAM") = "Net Loss"
                        dtRow("AmtAG") = TL - TA
                    Else
                        dtRow("CapLM") = "Net Profit"
                        dtRow("AmtLG") = TA - TL
                    End If
                    PLSummTb.Rows.Add(dtRow)
                End If
            End With
SkpWise1:
            j = j + 1
        Next
        For i = 0 To PLSummTb.Rows.Count - 1
            PLSummTb(i)("Lnk") = 1
        Next
        setPLSummTb = True
        GoTo Ter
        'FErr:
        '        If MsgBox(Err.Description, MsgBoxStyle.RetryCancel) = MsgBoxResult.Retry Then Resume
Ter:

    End Function
    Private Function LAmtTbFindMAccId(ByRef pos As Integer, ByVal LAmtTb As DataTable, ByVal col As String, ByVal value As Integer) As Boolean
        Dim i As Integer
        LAmtTbFindMAccId = False
        For i = pos To LAmtTb.Rows.Count - 1
            If LAmtTb(i)(col) = value Then
                pos = i
                Return True

            End If
        Next
    End Function
    Private Function AAmtTbFindMAccId(ByRef pos As Integer, ByVal Tb As DataTable, ByVal col As String, ByVal value As Integer) As Boolean
        Dim i As Integer
        AAmtTbFindMAccId = False
        For i = pos To Tb.Rows.Count - 1
            If Tb(i)(col) = value Then
                pos = i
                Return True

            End If
        Next
    End Function


    Private Sub _optReport_5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _optReport_5.Click, _optReport_6.Click, _optReport_7.Click, _optReport_8.Click
        For Each itm As RadioButton In Frame2.Controls
            itm.Checked = False
        Next
        Dim myctrl As RadioButton
        myctrl = sender
        Select Case myctrl.Name
            Case "_optReport_7", "_optReport_8"
                Label2.Visible = False
                cldateto.Visible = False
                _lblAcc_1.Text = "As on Date"
                cldrstartdate.Value = DateValue(Date.Now)
            Case Else
                Label2.Visible = True
                cldateto.Visible = True
                _lblAcc_1.Text = "Date From"
                cldrstartdate.Value = Format(DateAdd(DateInterval.Day, firstDateFromToday * -1, DateValue(Date.Now)), DtFormat)
                cldateto.Value = DateValue(Date.Now)
        End Select
    End Sub


    Private Sub _optRpt_1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _optRpt_1.Click, _optRpt_2.Click, _optRpt_4.Click, _optRpt_7.Click, rdoDaybook.Click
        For Each itm As RadioButton In GroupBox1.Controls
            itm.Checked = False
        Next
        If rdoDaybook.Checked Then
            Label2.Visible = True
            cldateto.Visible = True
            _lblAcc_1.Text = "Date From"
        Else
            Label2.Visible = False
            cldateto.Visible = False
            cldrstartdate.Value = DateValue(Date.Now)
            _lblAcc_1.Text = "As on Date"
        End If

    End Sub
    Private Function setPLDetTb() As Boolean
        Try
            Dim titleTb1 As DataTable
            Dim titleTb2 As DataTable
            Dim LAmtTb As DataTable
            Dim AAmtTb As DataTable
            Dim WiseTb As DataTable
            Dim eofTb1 As Boolean
            Dim eofTb2 As Boolean
            Dim stSrch1 As Byte
            Dim stSrch2 As Byte
            Dim TL As Double
            Dim TA As Double
            Dim deptWHERE As String
            Dim deptSQL As String
            Dim isOpnStock As Boolean
            Dim isClosingStock As Boolean
            Dim isDirExp As Boolean
            Dim dtRow As DataRow
            Dim WiseTbPos As Integer
            Dim LAmtTbPos As Integer
            Dim titleTb1Pos As Integer
            Dim titleTb2Pos As Integer
            Dim AAmtTbPos As Integer
            WiseTb = _objcmnbLayer._fldDatatable("SELECT Top 1 '','' FROM CompanyTb")
            PLDetTb = _objcmnbLayer._fldDatatable("SELECT * FROM PLDetTb")
            Dim dateRange As String
            dateRange = "JVDate >= '" & Format(DateValue(cldrstartdate.Value), "yyyy/MM/dd") & "' AND JVDate <= '" & Format(DateValue(cldateto.Value), "yyyy/MM/dd") & "'"
            Dim branchWHERE As String = IIf(UsrBr = "", "", " AND Branchid ='" & UsrBr & "'")
            Dim trBranchWhere As String = IIf(UsrBr = "", "", " AND cmnbrid ='" & UsrBr & "'")
            For WiseTbPos = 0 To WiseTb.Rows.Count - 1
                eofTb1 = False
                eofTb2 = False
                stSrch1 = 0
                stSrch2 = 0
                TL = 0
                TA = 0
                LAmtTbPos = 0
                titleTb1Pos = 0
                titleTb2Pos = 0
                AAmtTbPos = 0
                deptWHERE = ""
                Dim QueryString As String
                deptSQL = ""

                QueryString = "SELECT * FROM (SELECT S1AccHd.S1AccId,Descr,isNull(A1,0)+isNull(A2,0) As GTtl FROM (S1AccHd " & _
                                                       " LEFT JOIN (SELECT S1AccId," & IIf(_chkOpt_0.CheckState, 0, "Sum(OpnBal)") & " As A1 FROM AccMast WHERE S1AccId Between 4000 AND 4999 " & branchWHERE & "GROUP BY S1AccId) As Q1 " & _
                                                       "ON S1AccHd.S1AccId=Q1.S1AccId) LEFT JOIN (SELECT (accmast.AccountNo/10000) As S1AccId," & _
                                                       "Sum(DealAmt) As A2 FROM AccTrCmn LEFT JOIN AccTrDet ON AccTrCmn.LinkNo=AccTrDet.LinkNo " & _
                                                       "left join accmast on AccTrDet.AccountNo=accmast.accid " & _
                                                       deptSQL & " WHERE isnull(approved,0)=0 and  JVNum<>0 " & trBranchWhere & " AND " & _
                                                       "(accmast.AccountNo/10000) Between 4000 AND 4999 AND " & dateRange & _
                                                       " GROUP BY (accmast.AccountNo/10000)) As Q2 ON S1AccHd.S1AccId=Q2.S1AccId) tr " & _
                                                       "WHERE S1AccId Between 4000 AND 4999" & IIf(_chkOpt_1.CheckState = 0, " AND GTtl<>0", "") & " ORDER BY S1AccId,Descr"
                titleTb2 = _objcmnbLayer._fldDatatable(QueryString)

                QueryString = "SELECT * FROM (SELECT S1AccHd.S1AccId,Descr,isNull(A1,0)+isNull(A2,0) As GTtl" & _
                                                      " FROM (S1AccHd LEFT JOIN (SELECT S1AccId,Sum(OpnBal) As A1 FROM AccMast WHERE S1AccId Between 6000 AND 6999 " & branchWHERE & _
                                                      "GROUP BY S1AccId) As Q1 ON S1AccHd.S1AccId=Q1.S1AccId) LEFT JOIN " & _
                                                      "(SELECT (accmast.AccountNo/10000) As S1AccId,Sum(DealAmt) As A2 FROM AccTrCmn LEFT JOIN AccTrDet ON AccTrCmn.LinkNo=AccTrDet.LinkNo " & _
                                                      "left join accmast on AccTrDet.AccountNo=accmast.accid " & _
                                                      deptSQL & _
                                                      "WHERE isnull(approved,0)=0 and JVNum<>0 " & trBranchWhere & " AND (accmast.AccountNo/10000) Between 6000 AND 6999 " & _
                                                      "AND " & dateRange & " GROUP BY (accmast.AccountNo/10000)) As Q2 ON " & _
                                                      "S1AccHd.S1AccId=Q2.S1AccId)tr WHERE S1AccId Between 6000 AND 6999 AND GTtl<>0 ORDER BY S1AccId,Descr"
                titleTb1 = _objcmnbLayer._fldDatatable(QueryString)


                QueryString = "SELECT * FROM (SELECT BranchId, AccMast.S1AccId,AccDescr," & IIf(_chkOpt_0.CheckState, 0, "isNull(OpnBal,0)") & "+isNull(Bal,0) As Balance FROM AccMast " & _
                                                     "LEFT JOIN (SELECT (accmast.AccountNo/10000) As S1AccId,accmast.AccountNo,Sum(DealAmt) As Bal FROM AccTrCmn LEFT JOIN AccTrDet ON AccTrCmn.LinkNo=AccTrDet.LinkNo  " & _
                                                     "left join accmast on AccTrDet.AccountNo=accmast.accid " & _
                                                     deptSQL & _
                                                     "WHERE isnull(approved,0)=0 and JVNum<>0 " & trBranchWhere & " AND (accmast.AccountNo/10000) Between 4000 AND 4999 AND " & dateRange & _
                                                     "GROUP BY accmast.AccountNo) As Q ON AccMast.AccountNo=Q.AccountNo)tr WHERE S1AccId Between 4000 AND 4999" & IIf(_chkOpt_1.CheckState = 0, " AND Balance<>0", "") & branchWHERE & " ORDER BY S1AccId,AccDescr"

                AAmtTb = _objcmnbLayer._fldDatatable(QueryString)

                QueryString = "SELECT * FROM (SELECT BranchId, AccMast.S1AccId,AccDescr," & IIf(_chkOpt_0.CheckState, 0, "isNull(OpnBal,0)") & "+isNull(Bal,0) As Balance FROM AccMast" & _
                                                     " LEFT JOIN (SELECT (accmast.AccountNo/10000) As S1AccId,accmast.AccountNo, Sum(DealAmt) As Bal FROM AccTrCmn LEFT JOIN AccTrDet ON AccTrCmn.LinkNo=AccTrDet.LinkNo " & _
                                                     "left join accmast on AccTrDet.AccountNo=accmast.accid " & _
                                                     deptSQL & _
                                                     " WHERE isnull(approved,0)=0 and JVNum<>0 " & trBranchWhere & " AND (accmast.AccountNo/10000) Between 6000 AND 6999 AND " & dateRange & _
                                                     "GROUP BY accmast.AccountNo) As Q ON AccMast.AccountNo=Q.AccountNo)tr WHERE S1AccId Between 6000 AND 6999" & IIf(_chkOpt_1.CheckState = 0, " AND Balance<>0", "") & branchWHERE & " ORDER BY S1AccId,AccDescr"
                LAmtTb = _objcmnbLayer._fldDatatable(QueryString)

                If titleTb1.Rows.Count < 1 Then
                    eofTb1 = True
                End If
                If titleTb2.Rows.Count = 0 Then
                    eofTb2 = True
                End If
                'If numOCBal_0.Text = "" Then numOCBal_0.Text = 0
                'If numOCBal_1.Text = "" Then numOCBal_1.Text = 0
                'isOpnStock = plOS.Enabled And CDbl(numOCBal_0.Text & "") <> 0
                'isClosingStock = plOS.Enabled And CDbl(numOCBal_1.Text) <> 0
                'isDirExp = (isOpnStock Or isClosingStock)
                stSrch1 = 0
                stSrch2 = 0
                TL = 0
                TA = 0
                Do Until False
                    If eofTb1 And eofTb2 And Not isOpnStock And Not isClosingStock And Not isDirExp Then Exit Do
                    ' ChkBreak()
                    dtRow = PLDetTb.NewRow
                    dtRow("DeptId") = WiseTb(WiseTbPos)(0)
                    dtRow("DeptDescr") = WiseTb(WiseTbPos)(1)
                    If isOpnStock Then
                        isOpnStock = False
                        dtRow("CapLM") = "OPENING STOCK"
                        dtRow("AmtLM") = 0 ' CDbl(numOCBal_0.Text)
                        TL = TL + 0 'CDbl(numOCBal_0.Text)
                    ElseIf Not eofTb1 Then
                        If stSrch1 = 0 Then GoTo LoopL
                        If stSrch1 = 1 Then
                            LAmtTbPos = LAmtTbFindFirst(0, LAmtTb, "S1AccId", titleTb1(titleTb1Pos)("S1AccId"))
                            stSrch1 = 2
                        Else
                            LAmtTbPos = LAmtTbFindFirst(LAmtTbPos + 1, LAmtTb, "S1AccId", titleTb1(titleTb1Pos)("S1AccId"))
                        End If
                        If LAmtTbPos = LAmtTb.Rows.Count Then
                            titleTb1Pos = titleTb1Pos + 1
                            If titleTb1Pos >= titleTb1.Rows.Count Then
                                eofTb1 = True
                                If isClosingStock Then
                                    isClosingStock = False
                                    dtRow("CapLM") = "CLOSING STOCK"
                                    dtRow("AmtLM") = -1 * 0 'CDbl(numOCBal_1.Text)
                                    TL = TL - 0 ' CDbl(numOCBal_1.Text)
                                End If
                            Else
LoopL:
                                dtRow("CapLM") = titleTb1(titleTb1Pos)("Descr")
                                dtRow("AmtLM") = titleTb1(titleTb1Pos)("GTtl")
                                stSrch1 = 1
                            End If
                        Else
                            'LAmtTbPos = LAmtTbPos - 1
                            dtRow("CapL") = LAmtTb(LAmtTbPos)("AccDescr")
                            dtRow("AmtL") = LAmtTb(LAmtTbPos)("Balance")
                            TL = TL + LAmtTb(LAmtTbPos)("Balance")
                        End If
                    ElseIf isClosingStock Then
                        isClosingStock = False
                        dtRow("CapLM") = "CLOSING STOCK"
                        dtRow("AmtLM") = -1 * 0 'CDbl(numOCBal_1.Text)
                        TL = TL - 0 'CDbl(numOCBal_1.Text)
                    ElseIf isDirExp Then
                        isDirExp = False
                        dtRow("CapLM") = "Total Direct Expense"
                        dtRow("AmtLM") = TL
                    End If
                    If Not eofTb2 Then
                        If stSrch2 = 0 Then GoTo LoopA
                        If stSrch2 = 1 Then
                            AAmtTbPos = AAmtTbFindFirst(0, AAmtTb, "S1AccId", titleTb2(titleTb2Pos)("S1AccId"))
                            stSrch2 = 2
                        Else
                            AAmtTbPos = AAmtTbFindFirst(AAmtTbPos + 1, AAmtTb, "S1AccId", titleTb2(titleTb2Pos)("S1AccId"))
                        End If
                        If AAmtTbPos = AAmtTb.Rows.Count Then
                            titleTb2Pos = titleTb2Pos + 1
                            If titleTb2Pos >= titleTb2.Rows.Count Then
                                eofTb2 = True
                            Else
LoopA:
                                dtRow("CapAM") = titleTb2(titleTb2Pos)("Descr")
                                'dtRow("MaccId") = titleTb2(titleTb2Pos)("MaccId")
                                'dtRow("MaccDescr") = titleTb2(titleTb2Pos)("Descr")
                                dtRow("AmtAM") = -1 * titleTb2(titleTb2Pos)("GTtl")
                                stSrch2 = 1
                            End If
                        Else
                            'AAmtTbPos = AAmtTbPos - 1
                            dtRow("CapA") = AAmtTb(AAmtTbPos)("AccDescr")
                            dtRow("AmtA") = -1 * AAmtTb(AAmtTbPos)("Balance")
                            TA = TA + AAmtTb(AAmtTbPos)("Balance") * (-1)
                        End If
                    End If
                    PLDetTb.Rows.Add(dtRow)
                Loop
                If TL <> TA Then
                    dtRow = PLDetTb.NewRow
                    dtRow("DeptId") = WiseTb(WiseTbPos)(0)
                    dtRow("DeptDescr") = WiseTb(WiseTbPos)(1)
                    If TL - TA > 0 Then
                        dtRow("CapAM") = "Gross Loss c/f"
                        dtRow("AmtAM") = TL - TA
                    Else
                        dtRow("CapLM") = "Gross Profit c/f"
                        dtRow("AmtLM") = TA - TL
                    End If
                    PLDetTb.Rows.Add(dtRow)
                End If
                dtRow = PLDetTb.NewRow
                dtRow("DeptId") = WiseTb(WiseTbPos)(0)
                dtRow("DeptDescr") = WiseTb(WiseTbPos)(1)
                dtRow("CapLM") = "Sub Total"
                dtRow("CapAM") = "Sub Total"
                dtRow("AmtS") = IIf(TL < TA, TA, TL)
                PLDetTb.Rows.Add(dtRow)
                If TL <> TA Then
                    dtRow = PLDetTb.NewRow
                    dtRow("DeptId") = WiseTb(WiseTbPos)(0)
                    dtRow("DeptDescr") = WiseTb(WiseTbPos)(1)
                    If TL - TA > 0 Then
                        dtRow("CapLM") = "Gross Loss b/f"
                        dtRow("AmtLG") = TL - TA
                        TL = TL - TA
                        TA = 0
                    Else
                        dtRow("CapAM") = "Gross Profit b/f"
                        dtRow("AmtAG") = TA - TL
                        TA = TA - TL
                        TL = 0
                    End If
                    PLDetTb.Rows.Add(dtRow)
                Else
                    TA = 0
                    TL = 0
                End If
                QueryString = "select * from (SELECT * FROM (SELECT S1AccHd.S1AccId,Descr,isNull(A1,0)+isNull(A2,0) As GTtl,MaccId FROM (S1AccHd " & _
                                                                     "LEFT JOIN (SELECT S1AccId," & IIf(_chkOpt_0.CheckState, 0, "Sum(OpnBal)") & " As A1 FROM AccMast WHERE S1AccId Between 5000 AND 5999 " & branchWHERE & "GROUP BY S1AccId) As Q1 ON " & _
                                                                     "S1AccHd.S1AccId=Q1.S1AccId) LEFT JOIN (SELECT (accmast.AccountNo/10000) As S1AccId,Sum(DealAmt)" & _
                                                                     " As A2 FROM AccTrCmn LEFT JOIN AccTrDet ON AccTrCmn.LinkNo=AccTrDet.LinkNo " & _
                                                                     "left join accmast on AccTrDet.AccountNo=accmast.accid " & _
                                                                     deptSQL & " WHERE isnull(approved,0)=0 and JVNum<>0 " & trBranchWhere & " AND (accmast.AccountNo/10000) Between 5000 " & _
                                                                     "AND 5999 AND " & dateRange & " GROUP BY (accmast.AccountNo/10000)) As Q2 ON S1AccHd.S1AccId=Q2.S1AccId)Tr " & _
                                                                     "WHERE S1AccId Between 5000 AND 5999 " & IIf(_chkOpt_1.CheckState = 0, " AND GTtl<>0", "") & " )A left Join MAccHd On A.MAccId =MAccHd .MAccId  ORDER BY S1AccId,A.Descr"
                titleTb2 = _objcmnbLayer._fldDatatable(QueryString)

                QueryString = "SELECT * FROM (SELECT S1AccHd.S1AccId,Descr,isNull(A1,0)+isNull(A2,0) As GTtl FROM (S1AccHd " & _
                                                      "LEFT JOIN (SELECT S1AccId," & IIf(_chkOpt_0.CheckState, 0, "Sum(OpnBal)") & " As A1 FROM AccMast WHERE S1AccId Between 7000 AND 7999 " & branchWHERE & " GROUP BY S1AccId)" & _
                                                      " As Q1 ON S1AccHd.S1AccId=Q1.S1AccId) LEFT JOIN (SELECT (accmast.AccountNo/10000) As S1AccId," & _
                                                      "Sum(DealAmt) As A2 FROM AccTrCmn LEFT JOIN AccTrDet ON AccTrCmn.LinkNo=AccTrDet.LinkNo " & _
                                                      "left join accmast on AccTrDet.AccountNo=accmast.accid " & _
                                                      deptSQL & " WHERE isnull(approved,0)=0 and JVNum<>0 " & trBranchWhere & " AND (accmast.AccountNo/10000) " & _
                                                      "Between 7000 AND 7999 AND " & dateRange & _
                                                      " GROUP BY (accmast.AccountNo/10000)) As Q2 ON S1AccHd.S1AccId=Q2.S1AccId) Tr " & _
                                                      "WHERE S1AccId Between 7000 AND 7999 " & IIf(_chkOpt_1.CheckState = 0, " AND GTtl<>0", "") & " ORDER BY S1AccId,Descr"
                titleTb1 = _objcmnbLayer._fldDatatable(QueryString)

                QueryString = "SELECT * FROM (SELECT AccMast.S1AccId,AccDescr," & IIf(_chkOpt_0.CheckState, 0, "isNull(OpnBal,0)") & "+isNull(Bal,0) As Balance,Branchid FROM AccMast " & _
                                                                   "LEFT JOIN (SELECT (accmast.AccountNo/10000) As S1AccId,accmast.AccountNo,Sum(DealAmt) As Bal FROM AccTrCmn LEFT JOIN AccTrDet ON AccTrCmn.LinkNo=AccTrDet.LinkNo " & _
                                                                   "left join accmast on AccTrDet.AccountNo=accmast.accid " & _
                                                                   deptSQL & _
                                                                   "WHERE isnull(approved,0)=0 and JVNum<>0 " & trBranchWhere & " AND (accmast.AccountNo/10000) Between 5000 AND 5999 AND  " & dateRange & _
                                                                   "GROUP BY accmast.AccountNo) As Q ON AccMast.AccountNo=Q.AccountNo)Tr WHERE S1AccId Between 5000 AND 5999 " & IIf(_chkOpt_1.CheckState = 0, " AND Balance<>0", "") & branchWHERE & " ORDER BY S1AccId,AccDescr"
                AAmtTb = _objcmnbLayer._fldDatatable(QueryString)

                QueryString = "SELECT * FROM (SELECT AccMast.S1AccId,AccDescr," & IIf(_chkOpt_0.CheckState, 0, "isNull(OpnBal,0)") & "+isNull(Bal,0) As Balance,Branchid FROM AccMast " & _
                                                    " LEFT JOIN (SELECT (accmast.AccountNo/10000) As S1AccId,accmast.AccountNo, Sum(DealAmt) As Bal " & _
                                                    "FROM AccTrCmn LEFT JOIN AccTrDet ON AccTrCmn.LinkNo=AccTrDet.LinkNo " & _
                                                    "left join accmast on AccTrDet.AccountNo=accmast.accid " & _
                                                    deptSQL & " WHERE isnull(approved,0)=0 and JVNum<>0 " & trBranchWhere & " AND (accmast.AccountNo/10000) Between 7000 AND 7999 AND  " & dateRange & _
                                                    "GROUP BY accmast.AccountNo) As Q ON AccMast.AccountNo=Q.AccountNo)Tr WHERE S1AccId Between 7000 AND 7999 " & IIf(_chkOpt_1.CheckState = 0, " AND Balance<>0", "") & branchWHERE & " ORDER BY S1AccId,AccDescr"
                LAmtTb = _objcmnbLayer._fldDatatable(QueryString)
                eofTb1 = False
                eofTb2 = False
                titleTb1Pos = 0
                titleTb2Pos = 0
                AAmtTbPos = 0
                LAmtTbPos = 0
                If titleTb1.Rows.Count = 0 Then
                    eofTb1 = True
                End If
                If titleTb2.Rows.Count = 0 Then
                    eofTb2 = True
                End If
                stSrch1 = 0
                stSrch2 = 0
                Do Until False
                    'ChkBreak()
                    dtRow = PLDetTb.NewRow
                    dtRow("DeptId") = WiseTb(WiseTbPos)(0)
                    dtRow("DeptDescr") = WiseTb(WiseTbPos)(1)
                    If Not eofTb1 Then
                        If stSrch1 = 0 Then GoTo LoopL1
                        If stSrch1 = 1 Then
                            LAmtTbPos = 0 ' LAmtTbFindFirst(0, LAmtTb, "S1AccId", titleTb1(titleTb1Pos)("S1AccId"))
                            stSrch1 = 2
                        Else
                            LAmtTbPos = LAmtTbPos + 1 'LAmtTbFindFirst(LAmtTbPos + 1, LAmtTb, "S1AccId", titleTb1(titleTb1Pos)("S1AccId"))
                        End If
                        If Not AAmtTbFindMAccId(LAmtTbPos, LAmtTb, "S1AccId", titleTb1(titleTb1Pos)("S1AccId")) Then
                            'If LAmtTbPos = LAmtTb.Rows.Count Then
                            titleTb1Pos = titleTb1Pos + 1
                            If titleTb1Pos >= titleTb1.Rows.Count Then
                                eofTb1 = True
                            Else
LoopL1:
                                dtRow("CapLM") = titleTb1(titleTb1Pos)("Descr")
                                dtRow("AmtLG") = titleTb1(titleTb1Pos)("GTtl")
                                stSrch1 = 1
                            End If
                        Else
                            dtRow("CapL") = LAmtTb(LAmtTbPos)("AccDescr")
                            dtRow("AmtL") = LAmtTb(LAmtTbPos)("Balance")
                            TL = TL + LAmtTb(LAmtTbPos)("Balance")
                        End If
                    End If
                    If Not eofTb2 Then
                        If stSrch2 = 0 Then GoTo LoopA1
                        If stSrch2 = 1 Then
                            AAmtTbPos = 0 'AAmtTbFindFirst(0, AAmtTb, "S1AccId", titleTb2(titleTb2Pos)("S1AccId"))
                            stSrch2 = 2
                        Else
                            AAmtTbPos = AAmtTbPos + 1 'AAmtTbFindFirst(AAmtTbPos, AAmtTb, "S1AccId", titleTb2(titleTb2Pos)("S1AccId"))
                        End If
                        If Not AAmtTbFindMAccId(AAmtTbPos, AAmtTb, "S1AccId", titleTb2(titleTb2Pos)("S1AccId")) Then
                            'If AAmtTbPos >= AAmtTb.Rows.Count Then
                            titleTb2Pos = titleTb2Pos + 1
                            If titleTb2Pos >= titleTb2.Rows.Count Then
                                eofTb2 = True
                            Else
LoopA1:
                                ' dtRow("MaccId") = titleTb2(titleTb2Pos)("MaccId")
                                ' dtRow("MaccDescr") = titleTb2(titleTb2Pos)("Descr")
                                dtRow("CapAM") = titleTb2(titleTb2Pos)("Descr")
                                dtRow("AmtAG") = -1 * titleTb2(titleTb2Pos)("GTtl")
                                stSrch2 = 1
                            End If
                        Else
                            dtRow("CapA") = AAmtTb(AAmtTbPos)("AccDescr")
                            dtRow("AmtA") = -1 * AAmtTb(AAmtTbPos)("Balance")
                            TA = TA + AAmtTb(AAmtTbPos)("Balance") * (-1)
                        End If
                    End If
                    If eofTb1 And eofTb2 Then Exit Do
                    PLDetTb.Rows.Add(dtRow)
                Loop
                If TL <> TA Then
                    dtRow = PLDetTb.NewRow
                    dtRow("DeptId") = WiseTb(WiseTbPos)(0)
                    dtRow("DeptDescr") = WiseTb(WiseTbPos)(1)
                    If TL - TA > 0 Then
                        dtRow("CapAM") = "Net Loss"
                        dtRow("AmtAG") = TL - TA
                    Else
                        dtRow("CapLM") = "Net Profit"
                        dtRow("AmtLG") = TA - TL
                    End If
                    PLDetTb.Rows.Add(dtRow)
                End If
            Next
            For i = 0 To PLDetTb.Rows.Count - 1
                PLDetTb(i)("Lnk") = 1
            Next
            setPLDetTb = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function
    Private Function LAmtTbFindFirst(ByVal pos As Integer, ByVal dt As DataTable, ByVal fld As String, ByVal varValue As String) As Integer
        LAmtTbFindFirst = dt.Rows.Count
        For i = pos To dt.Rows.Count - 1
            If dt(i)(fld) = varValue Then
                LAmtTbFindFirst = i
                Exit For
            Else
                LAmtTbFindFirst = dt.Rows.Count
            End If
        Next
    End Function
    Private Function AAmtTbFindFirst(ByVal pos As Integer, ByVal dt As DataTable, ByVal fld As String, ByVal varValue As String) As Integer
        AAmtTbFindFirst = dt.Rows.Count
        For i = pos To dt.Rows.Count - 1
            If dt(i)(fld) = varValue Then
                AAmtTbFindFirst = i
                Exit For
            Else
                AAmtTbFindFirst = dt.Rows.Count
            End If
        Next
    End Function
    Private Function setBSSummTb() As Boolean
        'Dim fRptProgrss As Object
        On Error GoTo FErr
        Dim titleTb1 As DataTable
        Dim titleTb2 As DataTable
        Dim LAmtTb As DataTable
        Dim AAmtTb As DataTable
        Dim WiseTb As DataTable
        Dim eofTb1 As Boolean
        Dim eofTb2 As Boolean
        Dim stSrch1 As Byte
        Dim stSrch2 As Byte
        Dim TL As Double
        Dim TA As Double
        Dim deptWHERE As String
        Dim deptSQL As String
        Dim WiseTbPos As Integer
        Dim titleTb1Pos As Integer
        Dim titleTb2Pos As Integer
        Dim LAmtTbPos As Integer
        Dim AAmtTbPos As Integer
        Dim dtRow As DataRow = Nothing
        WiseTb = _objcmnbLayer._fldDatatable("SELECT Top 1 '','' FROM CompanyTb")
        Dim dateRange As String
        dateRange = "JVDate <= '" & Format(DateValue(cldrstartdate.Value), "yyyy/MM/dd") & "'"
        Dim branchWHERE As String = IIf(UsrBr = "", "", " AND Branchid ='" & UsrBr & "'")
        Dim trBranchWhere As String = IIf(UsrBr = "", "", " AND cmnbrid ='" & UsrBr & "'")
        For WiseTbPos = 0 To WiseTb.Rows.Count - 1
            eofTb1 = False
            eofTb2 = False
            stSrch1 = 0
            stSrch2 = 0
            TL = 0
            TA = 0
            deptWHERE = ""
            Dim _Next_Q As String
            deptSQL = ""
            _Next_Q = "SELECT * FROM (SELECT MAccHd.MAccId,Descr,isNull(A1,0)+isNull(A2,0) As GTtl FROM (MAccHd LEFT JOIN " & _
                                                   "(SELECT (S1AccId/100)*100 As MAccId,Sum(OpnBal) As A1 FROM AccMast WHERE S1AccId Between 2000 AND 3999 " & branchWHERE & "GROUP BY (S1AccId/100)*100) As Q1 ON " & _
                                                   "MAccHd.MAccId=Q1.MAccId) LEFT JOIN (SELECT (accmast.AccountNo/1000000)*100 As MAccId,Sum(DealAmt) As A2 " & _
                                                   "FROM AccTrCmn LEFT JOIN AccTrDet ON AccTrCmn.LinkNo=AccTrDet.LinkNo " & _
                                                   "left join accmast on AccTrDet.AccountNo=accmast.accid " & _
                                                   deptSQL & " WHERE isnull(approved,0)=0 and JVNum<>0 " & trBranchWhere & " AND (accmast.AccountNo/10000) Between 2000 AND 3999 AND " & dateRange & _
                                                   "GROUP BY (accmast.AccountNo/1000000)*100) As Q2 ON MAccHd.MAccId=Q2.MAccId)Tr WHERE MAccId Between 2000 AND 3999 " & IIf(_chkOpt_1.CheckState = 0, " AND GTtl<>0", "") & " ORDER BY MAccId,Descr"

            titleTb1 = _objcmnbLayer._fldDatatable(_Next_Q)

            _Next_Q = "SELECT * FROM (SELECT MAccHd.MAccId,Descr,isNull(A1,0)+isNull(A2,0) As GTtl FROM (MAccHd LEFT JOIN " & _
                                                   "(SELECT (S1AccId/100)*100 As MAccId,Sum(OpnBal) As A1 FROM AccMast WHERE S1AccId<2000 " & branchWHERE & " GROUP BY (S1AccId/100)*100) As Q1" & _
                                                   " ON MAccHd.MAccId=Q1.MAccId) LEFT JOIN (SELECT (accmast.AccountNo/1000000)*100 As MAccId,Sum(DealAmt) As A2 " & _
                                                   "FROM AccTrCmn LEFT JOIN AccTrDet ON AccTrCmn.LinkNo=AccTrDet.LinkNo " & _
                                                   "left join accmast on AccTrDet.AccountNo=accmast.accid " & _
                                                   deptSQL & " WHERE isnull(approved,0)=0 and JVNum<>0 " & trBranchWhere & " AND " & _
                                                   "(accmast.AccountNo/10000)<2000 AND " & dateRange & " GROUP BY (accmast.AccountNo/1000000)*100) As Q2" & _
                                                   " ON MAccHd.MAccId=Q2.MAccId)Tr WHERE MAccId<2000 AND GTtl<>0 ORDER BY MAccId,Descr"

            titleTb2 = _objcmnbLayer._fldDatatable(_Next_Q)

            
            BalShtSmmTb = _objcmnbLayer._fldDatatable("SELECT * FROM BalShtSmmTb")

            _Next_Q = "SELECT * FROM (SELECT MAccId,S1AccHd.S1AccId,Descr,isNull(Bal,0)+isNull(OB,0) AS Balance FROM " & _
                                                 "(S1AccHd LEFT JOIN (SELECT S1AccId,Sum(OpnBal) As OB FROM AccMast WHERE S1AccId Between 2000 AND 3999 " & branchWHERE & " GROUP BY S1AccId) AS A " & _
                                                 "ON S1AccHd.S1AccId=A.S1AccId) LEFT JOIN (SELECT (accmast.AccountNo/10000) As S1AccId,Sum(DealAmt) AS Bal " & _
                                                 "FROM AccTrCmn LEFT JOIN AccTrDet ON AccTrCmn.LinkNo=AccTrDet.LinkNo  " & _
                                                 "left join accmast on AccTrDet.AccountNo=accmast.accid " & _
                                                 deptSQL & " WHERE isnull(approved,0)=0 and JVNum<>0 " & trBranchWhere & " AND  " & dateRange & " AND " & _
                                                 "(accmast.AccountNo/10000) Between 2000 AND 3999 GROUP BY " & _
                                                 "(accmast.AccountNo/10000)) AS Q1 ON S1AccHd.S1AccId = Q1.S1AccId)Tr WHERE Balance<>0 ORDER BY MAccId,Descr"

            LAmtTb = _objcmnbLayer._fldDatatable(_Next_Q)


            _Next_Q = "SELECT * FROM (SELECT MAccId,S1AccHd.S1AccId,Descr,isNull(Bal,0)+isNull(OB,0) AS Balance FROM (S1AccHd LEFT JOIN " & _
                                                 "(SELECT S1AccId,Sum(OpnBal) As OB FROM AccMast WHERE S1AccId<=1999 " & branchWHERE & " GROUP BY S1AccId) AS A ON S1AccHd.S1AccId=A.S1AccId) LEFT JOIN " & _
                                                 "(SELECT (accmast.AccountNo/10000) As S1AccId,Sum(DealAmt) AS Bal FROM AccTrCmn LEFT JOIN AccTrDet ON AccTrCmn.LinkNo=AccTrDet.LinkNo  " & _
                                                 "left join accmast on AccTrDet.AccountNo=accmast.accid " & _
                                                 deptSQL & " WHERE isnull(approved,0)=0 and JVNum<>0 " & trBranchWhere & " AND " & _
                                                 dateRange & "  AND (accmast.AccountNo/10000)<=1999 GROUP BY (accmast.AccountNo/10000)) AS Q1 ON S1AccHd.S1AccId = Q1.S1AccId)Tr " & _
                                                 IIf(_chkOpt_1.CheckState = 0, " WHERE Balance<>0", "") & " ORDER BY MAccId,Descr"
            AAmtTb = _objcmnbLayer._fldDatatable(_Next_Q)


            If titleTb1.Rows.Count < 1 Then
                eofTb1 = True
            Else
                titleTb1Pos = 0
            End If
            If titleTb2.Rows.Count < 1 Then
                eofTb2 = True
            Else
                titleTb2Pos = 0
            End If
            stSrch1 = 0
            stSrch2 = 0
            TL = 0
            TA = 0
            With BalShtSmmTb
                Do Until False
                    'ChkBreak()
                    dtRow = .NewRow
                    dtRow("DeptId") = WiseTb(WiseTbPos)(0)
                    dtRow("DeptDescr") = WiseTb(WiseTbPos)(1)
                    If Not eofTb1 Then
                        If stSrch1 = 0 Then GoTo LoopL
                        If stSrch1 = 1 Then
                            LAmtTbPos = 0
                            LAmtTbPos = LAmtTbFindFirst(0, LAmtTb, "MAccId", titleTb1(titleTb1Pos)("MAccId"))
                            stSrch1 = 2
                        Else
                            LAmtTbPos = LAmtTbFindFirst(LAmtTbPos + 1, LAmtTb, "MAccId", titleTb1(titleTb1Pos)("MAccId"))
                        End If
                        If LAmtTbPos >= LAmtTb.Rows.Count Then
                            titleTb1Pos = titleTb1Pos + 1
                            If titleTb1Pos >= titleTb1.Rows.Count Then
                                eofTb1 = True
                            Else
LoopL:
                                'dtRow("MaccId") = titleTb1(titleTb2Pos)("MaccId")
                                'dtRow("MaccDescr") = titleTb1(titleTb2Pos)("Descr")
                                dtRow("CapLM") = titleTb1(titleTb1Pos)("Descr")
                                dtRow("AmtLM") = -1 * titleTb1(titleTb1Pos)("GTtl")
                                stSrch1 = 1
                            End If
                        Else
                            dtRow("CapL") = LAmtTb(LAmtTbPos)("Descr")
                            dtRow("AmtL") = -1 * LAmtTb(LAmtTbPos)("Balance")
                            TL = TL + LAmtTb(LAmtTbPos)("Balance") * (-1)
                        End If
                    End If
                    If Not eofTb2 Then
                        If stSrch2 = 0 Then GoTo LoopA
                        If stSrch2 = 1 Then
                            AAmtTbPos = 0
                            AAmtTbPos = AAmtTbFindFirst(0, AAmtTb, "MAccId", titleTb2(titleTb2Pos)("MAccId"))
                            stSrch2 = 2
                        Else
                            AAmtTbPos = AAmtTbFindFirst(AAmtTbPos + 1, AAmtTb, "MAccId", titleTb2(titleTb2Pos)("MAccId"))
                        End If
                        If AAmtTbPos >= AAmtTb.Rows.Count Then
                            titleTb2Pos = titleTb2Pos + 1
                            If titleTb2Pos >= titleTb2.Rows.Count Then
                                eofTb2 = True
                            Else
LoopA:
                                dtRow("CapAM") = titleTb2(titleTb2Pos)("Descr")
                                dtRow("AmtAM") = titleTb2(titleTb2Pos)("GTtl")
                                stSrch2 = 1
                            End If
                        Else
                            dtRow("CapA") = AAmtTb(AAmtTbPos)("Descr")
                            dtRow("AmtA") = AAmtTb(AAmtTbPos)("Balance")
                            TA = TA + AAmtTb(AAmtTbPos)("Balance")
                        End If
                    End If
                    If eofTb1 And eofTb2 Then Exit Do
                    .Rows.Add(dtRow)
                Loop

                _Next_Q = "SELECT isNull(A.OB,0)+isNull(B.Bal,0) AS Balance FROM (SELECT Sum(OpnBal) As OB FROM AccMast WHERE S1AccId>=4000 " & branchWHERE & " ) AS A," & _
                                                       "(SELECT Sum(DealAmt) AS Bal FROM AccTrCmn LEFT JOIN AccTrDet ON AccTrCmn.LinkNo=AccTrDet.LinkNo " & _
                                                       "left join accmast on AccTrDet.AccountNo=accmast.accid " & _
                                                       deptSQL & "WHERE isnull(approved,0)=0 and JVNum<>0 " & trBranchWhere & " AND " & dateRange & _
                                                       "AND (accmast.AccountNo/10000)>=4000) AS B"
                LAmtTb = _objcmnbLayer._fldDatatable(_Next_Q)
                If LAmtTb.Rows.Count > 0 Then
                    LAmtTbPos = 0
                Else
                    GoTo skp
                End If
                If LAmtTb(LAmtTbPos)("Balance") <= 0 Then
                    dtRow("CapLM") = "Net Profit"
                    dtRow("AmtLM") = System.Math.Abs(LAmtTb(LAmtTbPos)("Balance"))
                    TL = TL + System.Math.Abs(LAmtTb(LAmtTbPos)("Balance"))
                Else
                    dtRow("CapAM") = "Net Loss"
                    dtRow("AmtAM") = LAmtTb(LAmtTbPos)("Balance")
                    TA = TA + LAmtTb(LAmtTbPos)("Balance")
                End If
                .Rows.Add(dtRow)
skp:
                dtRow = .NewRow
                dtRow("DeptId") = WiseTb(WiseTbPos)(0)
                dtRow("DeptDescr") = WiseTb(WiseTbPos)(1)
                If TL - TA > 0 Then
                    dtRow("CapAM") = "Difference"
                    dtRow("AmtAM") = TL - TA
                Else
                    dtRow("CapLM") = "Difference"
                    dtRow("AmtLM") = System.Math.Abs(TL - TA)
                End If
                .Rows.Add(dtRow)
            End With
            WiseTbPos = WiseTbPos + 1
        Next
        setBSSummTb = True
        For i = 0 To BalShtSmmTb.Rows.Count - 1
            BalShtSmmTb(i)("Lnk") = 1
        Next
        GoTo Ter
FErr:
        'If MsgBox(Err.Description, vbRetryCancel) = vbRetry Then Resume
Ter:
    End Function

    Private Function setBSDetTb() As Boolean
        On Error GoTo FErr
        Dim titleTb1 As DataTable
        Dim titleTb2 As DataTable
        Dim LAmtTb As DataTable
        Dim AAmtTb As DataTable
        Dim WiseTb As DataTable
        Dim eofTb1 As Boolean
        Dim eofTb2 As Boolean
        Dim stSrch1 As Byte
        Dim stSrch2 As Byte
        Dim TL As Double
        Dim TA As Double
        Dim branchWHERE As String
        Dim trBranchWhere As String
        Dim deptSQL As String
        Dim WiseTbPos As Integer
        Dim titleTb1Pos As Integer
        Dim titleTb2Pos As Integer
        Dim LAmtTbPos As Integer
        Dim AAmtTbPos As Integer
        Dim dtRow As DataRow = Nothing
        WiseTb = _objcmnbLayer._fldDatatable("SELECT Top 1 '','' FROM CompanyTb")
        Dim dateRange As String
        dateRange = "JVDate <= '" & Format(DateValue(cldrstartdate.Value), "yyyy/MM/dd") & "'"
        branchWHERE = IIf(UsrBr = "", "", " AND Branchid in ('','" & UsrBr & "')")
        trBranchWhere = IIf(UsrBr = "", "", " AND cmnbrid='" & UsrBr & "'")
        Dim opbalqyery As String = " LEFT JOIN (Select Sum(DealAmt) OpBal,AccountNo FROM AccTrCmn " & _
        "LEFT JOIN AccTrDet ON AccTrCmn.LinkNo=AccTrDet.LinkNo where JVNum=0 " & trBranchWhere & " group by AccountNo)OpBalTr On AccMast.accid=OpBalTr.AccountNo "
        For WiseTbPos = 0 To WiseTb.Rows.Count - 1
            eofTb1 = False
            eofTb2 = False
            stSrch1 = 0
            stSrch2 = 0
            TL = 0
            TA = 0
            deptSQL = ""
            Dim queryString As String

            queryString = "SELECT * FROM (SELECT S1AccHd.S1AccId,Descr,isNull(A1,0)+isNull(A2,0) As GTtl FROM (S1AccHd LEFT JOIN " & _
                                                   "(SELECT S1AccId,Sum(OpnBal) As A1 FROM AccMast WHERE S1AccId Between 2000 AND 3999  " & branchWHERE & "GROUP BY S1AccId) As Q1 ON S1AccHd.S1AccId=Q1.S1AccId)" & _
                                                   "LEFT JOIN (SELECT (accmast.AccountNo/10000) As S1AccId,Sum(DealAmt) As A2 FROM AccTrCmn LEFT JOIN AccTrDet ON AccTrCmn.LinkNo=AccTrDet.LinkNo  " & _
                                                   "left join accmast on AccTrDet.AccountNo=accmast.accid " & _
                                                   deptSQL & " WHERE isnull(approved,0)=0 and JVNum <>0 AND " & _
                                                   "(accmast.AccountNo/10000) Between 2000 AND 3999 AND " & dateRange & trBranchWhere & " GROUP BY (accmast.AccountNo/10000)) As Q2 ON " & _
                                                   "S1AccHd.S1AccId=Q2.S1AccId)Tr WHERE S1AccId Between 2000 AND 3999" & IIf(_chkOpt_1.CheckState = 0, " AND GTtl<>0", "") & " ORDER BY S1AccId,Descr"


            titleTb1 = _objcmnbLayer._fldDatatable(queryString)
            queryString = "SELECT * FROM (SELECT S1AccHd.S1AccId,Descr,isNull(A1,0)+isNull(A2,0) As GTtl FROM (S1AccHd " & _
                                                               "LEFT JOIN (SELECT S1AccId,Sum(OpnBal) As A1 FROM AccMast WHERE S1AccId<2000 " & branchWHERE & " GROUP BY S1AccId) As Q1 ON S1AccHd.S1AccId=Q1.S1AccId) " & _
                                                               "LEFT JOIN (SELECT (accmast.AccountNo/10000) As S1AccId,Sum(DealAmt) As A2 FROM AccTrCmn LEFT JOIN AccTrDet ON AccTrCmn.LinkNo=AccTrDet.LinkNo " & _
                                                               "left join accmast on AccTrDet.AccountNo=accmast.accid " & _
                                                               deptSQL & " WHERE isnull(approved,0)=0 and JVNum<>0 " & trBranchWhere & _
                                                               " AND (accmast.AccountNo/10000)<2000 AND  " & dateRange & " GROUP BY (accmast.AccountNo/10000)) As Q2 ON S1AccHd.S1AccId=Q2.S1AccId)Tr" & _
                                                               " WHERE S1AccId<2000 " & IIf(_chkOpt_1.CheckState = 0, " AND GTtl<>0", "") & " ORDER BY S1AccId,Descr"

            titleTb2 = _objcmnbLayer._fldDatatable(queryString)

            BalShtDetTb = _objcmnbLayer._fldDatatable("SELECT * FROM BalShtDetTb")

            queryString = "SELECT * FROM (SELECT BranchId, AccMast.S1AccId,AccDescr,isNull(OpnBal,0)+isNull(Bal,0) As Balance FROM AccMast " & _
                                                  "LEFT JOIN (SELECT (accmast.AccountNo/10000) As S1AccId,accmast.AccountNo,Sum(DealAmt) As Bal FROM AccTrCmn LEFT JOIN AccTrDet ON AccTrCmn.LinkNo=AccTrDet.LinkNo   " & _
                                                  "left join accmast on AccTrDet.AccountNo=accmast.accid " & _
                                                  deptSQL & _
                                                  "WHERE isnull(approved,0)=0 and JVNum<>0 " & trBranchWhere & " AND (accmast.AccountNo/10000) Between 2000 AND 3999 AND " & dateRange & " GROUP BY accmast.AccountNo) As Q " & _
                                                  "ON AccMast.AccountNo=Q.AccountNo)Tr WHERE S1AccId Between 2000 AND 3999" & IIf(_chkOpt_1.CheckState = 0, " AND round(Balance,2)<>0", "") & branchWHERE & " ORDER BY S1AccId,AccDescr"
            LAmtTb = _objcmnbLayer._fldDatatable(queryString)

            queryString = "SELECT * FROM (SELECT BranchId, AccMast.S1AccId,AccDescr,case when isnull(isobdet,0)=1 then isNull(OpBal,0) else isnull(OpnBal,0) end +isNull(Bal,0) As Balance FROM AccMast " & opbalqyery & _
                                                  "LEFT JOIN (SELECT (accmast.AccountNo/10000) As S1AccId,accmast.AccountNo, Sum(DealAmt) As Bal FROM AccTrCmn LEFT JOIN AccTrDet ON AccTrCmn.LinkNo=AccTrDet.LinkNo  " & _
                                                  "left join accmast on AccTrDet.AccountNo=accmast.accid " & _
                                                  deptSQL & _
                                                  "WHERE isnull(approved,0)=0 and JVNum<>0 " & trBranchWhere & " AND (accmast.AccountNo/10000)<2000 AND " & dateRange & " GROUP BY accmast.AccountNo) As Q ON AccMast.AccountNo=Q.AccountNo)Tr " & _
                                                  "WHERE S1AccId < 2000 " & IIf(_chkOpt_1.CheckState = 0, " AND round(Balance,2)<>0", "") & branchWHERE & " ORDER BY S1AccId,AccDescr"

            AAmtTb = _objcmnbLayer._fldDatatable(queryString)


            If titleTb1.Rows.Count < 1 Then
                eofTb1 = True
            Else
                titleTb1Pos = 0
            End If
            If titleTb2.Rows.Count < 1 Then
                eofTb2 = True
            Else
                titleTb2Pos = 0
            End If
            '  isOpnStock = fmeOS.Enabled And CDbl(numOCBal(0).Text) <> 0
            '  isClosingStock = fmeOS.Enabled And CDbl(numOCBal(1).Text) <> 0
            stSrch1 = 0
            stSrch2 = 0
            TL = 0
            TA = 0
            Dim a1 As String

            With BalShtDetTb
                Do Until False
                    'ChkBreak()
                    dtRow = .NewRow
                    dtRow("DeptId") = WiseTb(WiseTbPos)(0)
                    a1 = WiseTb(WiseTbPos)(0)
                    dtRow("DeptDescr") = WiseTb(WiseTbPos)(1)
                    a1 = WiseTb(WiseTbPos)(1)
                    If Not eofTb1 Then
                        If stSrch1 = 0 Then GoTo LoopL
                        If stSrch1 = 1 Then
                            LAmtTbPos = LAmtTbFindFirst(0, LAmtTb, "S1AccId", titleTb1(titleTb1Pos)("S1AccId"))
                            stSrch1 = 2
                        Else
                            LAmtTbPos = LAmtTbFindFirst(LAmtTbPos + 1, LAmtTb, "S1AccId", titleTb1(titleTb1Pos)("S1AccId"))
                        End If
                        If LAmtTbPos = LAmtTb.Rows.Count Then
                            titleTb1Pos = titleTb1Pos + 1
                            If titleTb1Pos >= titleTb1.Rows.Count Then
                                eofTb1 = True
                            Else
LoopL:
                                dtRow("CapLM") = titleTb1(titleTb1Pos)("Descr")
                                a1 = titleTb1(titleTb1Pos)("Descr")
                                dtRow("AmtLM") = -1 * titleTb1(titleTb1Pos)("GTtl")
                                a1 = -1 * titleTb1(titleTb1Pos)("GTtl")
                                stSrch1 = 1
                            End If
                        Else
                            dtRow("CapL") = LAmtTb(LAmtTbPos)("AccDescr")
                            a1 = LAmtTb(LAmtTbPos)("AccDescr")
                            dtRow("AmtL") = -1 * LAmtTb(LAmtTbPos)("Balance")
                            a1 = -1 * LAmtTb(LAmtTbPos)("Balance")
                            TL = TL + LAmtTb(LAmtTbPos)("Balance") * (-1)
                            a1 = TL
                        End If
                    End If
                    If Not eofTb2 Then
                        If stSrch2 = 0 Then GoTo LoopA
                        If stSrch2 = 1 Then
                            AAmtTbPos = AAmtTbFindFirst(0, AAmtTb, "S1AccId", titleTb2(titleTb2Pos)("S1AccId"))
                            stSrch2 = 2
                        Else
                            AAmtTbPos = AAmtTbFindFirst(AAmtTbPos + 1, AAmtTb, "S1AccId", titleTb2(titleTb2Pos)("S1AccId"))
                        End If
                        If AAmtTbPos = AAmtTb.Rows.Count Then
                            titleTb2Pos = titleTb2Pos + 1
                            If titleTb2Pos = titleTb2.Rows.Count Then
                                eofTb2 = True
                            Else
LoopA:
                                dtRow("CapAM") = titleTb2(titleTb2Pos)("Descr")
                                a1 = titleTb2(titleTb2Pos)("Descr")
                                dtRow("AmtAM") = titleTb2(titleTb2Pos)("GTtl")
                                a1 = titleTb2(titleTb2Pos)("GTtl")
                                stSrch2 = 1
                            End If
                        Else
                            dtRow("CapA") = AAmtTb(AAmtTbPos)("AccDescr")
                            a1 = AAmtTb(AAmtTbPos)("AccDescr")
                            dtRow("AmtA") = AAmtTb(AAmtTbPos)("Balance")
                            a1 = AAmtTb(AAmtTbPos)("Balance")
                            TA = TA + AAmtTb(AAmtTbPos)("Balance") '* (-1)
                            a1 = TA
                        End If
                    End If
                    If eofTb1 And eofTb2 Then Exit Do
                    .Rows.Add(dtRow)
                Loop
                LAmtTb.Clear()
                queryString = "SELECT isNull(A.OB,0)+isNull(B.Bal,0) AS Balance FROM (SELECT Sum(OpnBal) As OB FROM AccMast WHERE S1AccId>=4000 " & branchWHERE & " ) AS A," & _
                                                     "(SELECT Sum(DealAmt) AS Bal FROM AccTrCmn LEFT JOIN AccTrDet ON AccTrCmn.LinkNo=AccTrDet.LinkNo " & _
                                                     "left join accmast on AccTrDet.AccountNo=accmast.accid " & _
                                                     deptSQL & "WHERE isnull(approved,0)=0 and JVNum<>0 " & trBranchWhere & " AND " & dateRange & _
                                                     "AND (accmast.AccountNo/10000)>=4000) AS B"
                LAmtTb = _objcmnbLayer._fldDatatable(queryString)

                If LAmtTb.Rows.Count > 0 Then
                    LAmtTbPos = 0
                Else
                    GoTo skp
                End If
                'If LAmtTb!Balance + IIf(fmeOS.Enabled, CDbl(numOCBal(1).Text) - CDbl(numOCBal(0).Text), 0) <= 0 Then
                If LAmtTb(LAmtTbPos)("Balance") <= 0 Then
                    dtRow("CapLM") = "Net Profit"
                    dtRow("AmtLM") = System.Math.Abs(LAmtTb(LAmtTbPos)("Balance")) '+ IIf(fmeOS.Enabled, CDbl(numOCBal(1).Text) - CDbl(numOCBal(0).Text), 0)
                    a1 = System.Math.Abs(LAmtTb(LAmtTbPos)("Balance"))
                    TL = TL + System.Math.Abs(LAmtTb(LAmtTbPos)("Balance"))
                    a1 = TL '+ IIf(fmeOS.Enabled, CDbl(numOCBal(1).Text) - CDbl(numOCBal(0).Text), 0)
                Else
                    dtRow("CapAM") = "Net Loss"
                    dtRow("AmtAM") = LAmtTb(LAmtTbPos)("Balance") '+ IIf(fmeOS.Enabled, CDbl(numOCBal(1).Text) - CDbl(numOCBal(0).Text), 0)
                    a1 = LAmtTb(LAmtTbPos)("Balance")
                    TA = TA + LAmtTb(LAmtTbPos)("Balance") '+ IIf(fmeOS.Enabled, CDbl(numOCBal(1).Text) - CDbl(numOCBal(0).Text), 0)
                    a1 = TA
                End If
                .Rows.Add(dtRow)
skp:
                If TL <> TA Then
                    dtRow = .NewRow
                    dtRow("DeptId") = WiseTb(WiseTbPos)(0)
                    a1 = WiseTb(WiseTbPos)(0)
                    dtRow("DeptDescr") = WiseTb(WiseTbPos)(1)
                    a1 = WiseTb(WiseTbPos)(1)
                    If TL - TA > 0 Then
                        dtRow("CapAM") = "Difference"
                        dtRow("AmtAM") = TL - TA
                    Else
                        dtRow("CapLM") = "Difference"
                        dtRow("AmtLM") = TA - TL
                    End If
                    .Rows.Add(dtRow)
                End If
            End With
            WiseTbPos = WiseTbPos + 1
        Next
        setBSDetTb = True
        For i = 0 To BalShtDetTb.Rows.Count - 1
            BalShtDetTb(i)("Lnk") = 1
        Next
        GoTo Ter
FErr:
        'If MsgBox(Err.Description, vbRetryCancel) = vbRetry Then Resume
Ter:

    End Function

    Private Sub FinancialStatus_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If userType = 0 Or userType = 2 Then
        Else
            Frame2.Enabled = getRight(72, CurrentUser)
            GroupBox1.Enabled = getRight(73, CurrentUser)
            If Frame2.Enabled = False And GroupBox1.Enabled = False Then
                btnApply.Enabled = False
            End If
        End If
        cldrstartdate.Value = Format(DateAdd(DateInterval.Day, firstDateFromToday * -1, DateValue(Date.Now)), DtFormat)
        dtsummary = _objcmnbLayer._fldDatatable("Select '' D, convert(money,0) amt from CompanyTb")
        'ldSummary()
        rdoDaybook.Checked = True
        Timer1.Enabled = True
        Timer2.Enabled = True
        
        Panel1.Visible = enableWebIntegration
    End Sub
    Private Sub ldSummaryNew(ByVal dt1 As Date, ByVal dt2 As Date)
        Dim i As Integer
        Dim dt As DataTable
        dtsummary.Rows.Clear()
        dsSummary = New DataSet
        Try
            For i = 1 To 17
                dt = Nothing
                dt = loadSummaryByText(dt1, dt2, i)
                dsSummary.Tables.Add(dt)
            Next
        Catch ex As Exception
            MsgBox(i)
        End Try

    End Sub
    Private Sub ldSummary()
        Dim ds As DataSet
        Dim dt1, dt2 As Date
        dt1 = DateValue("01/" & Month(dtmonth.Value) & "/" & Year(dtmonth.Value))
        dt2 = DateAdd(DateInterval.Month, 1, DateValue(dtmonth.Value))
        dt2 = DateValue("01/" & Month(dt2) & "/" & Year(dt2))
        dt2 = DateAdd(DateInterval.Day, -1, dt2)
        ds = _objTr.loadHomeSummary(dt1, dt2)

        Dim dt As DataTable
        Dim i As Integer
        dtsummary.Rows.Clear()
        Dim dtr As DataRow
        For i = 0 To ds.Tables.Count - 1
            dtr = dtsummary.NewRow
            dtr("D") = ds.Tables(i).Columns(0).Caption
            dtr("amt") = ds.Tables(i)(0)(0)
            dtsummary.Rows.Add(dtr)
        Next
        grdvoucher.DataSource = dtsummary
        SetGridHead()
    End Sub
    Private Function loadSummaryByText(ByVal _FromDate As Date, ByVal _ToDate As Date, ByVal tp As Integer) As DataTable
        Dim str As String = ""
        'UsrBr = ""
        Select Case tp
            Case 1
                str = "select null [as on date]"
            Case 2
                str = "	select isnull(sum(isnull(bal,0)+opnbal),0) [pdc received amount] from accmast left join balanceqr on balanceqr.accountno=accmast.accid " & _
                "left join s1acchd on s1acchd.s1accid=accmast.s1accid left join macchd on s1acchd.maccid=macchd.maccid " & _
                "where s1acchd.grpseton = 'p.d.c.(r)'" & IIf(UsrBr = "", "", " and isnull(branchid,'') ='" & UsrBr & "'")
            Case 3
                str = "select isnull(sum(isnull(bal,0)+opnbal),0) [pdc issued amount] from accmast  " & _
                "left join balanceqr on balanceqr.accountno=accmast.accid " & _
                "left join s1acchd on s1acchd.s1accid=accmast.s1accid left join macchd on s1acchd.maccid=macchd.maccid " & _
                "where s1acchd.grpseton = 'p.d.c.(i)'" & IIf(UsrBr = "", "", " and isnull(branchid,'') ='" & UsrBr & "'")
            Case 4
                str = "select isnull(sum(isnull(bal,0)+opnbal),0) [cash in hand] from accmast left join " & _
                "balanceqr on balanceqr.accountno=accmast.accid  " & _
                "left join s1acchd on s1acchd.s1accid=accmast.s1accid left join macchd on s1acchd.maccid=macchd.maccid " & _
                "where s1acchd.grpseton = 'cash' " & IIf(UsrBr = "", "", " and isnull(branchid,'') ='" & UsrBr & "'")
            Case 5
                str = "select isnull(sum(isnull(bal,0)+opnbal),0) [amount in banks] from accmast left join balanceqr  on balanceqr.accountno=accmast.accid " & _
                "left join s1acchd on s1acchd.s1accid=accmast.s1accid left join macchd on s1acchd.maccid=macchd.maccid " & _
                "where s1acchd.grpseton = 'bank'" & IIf(UsrBr = "", "", " and isnull(branchid,'') ='" & UsrBr & "'")
            Case 6
                str = "select isnull(sum(isnull(bal,0)+opnbal),0) [amount receivable (customer)] from accmast left join balanceqr on balanceqr.accountno=accmast.accid " & _
                "left join s1acchd on s1acchd.s1accid=accmast.s1accid left join macchd on s1acchd.maccid=macchd.maccid " & _
                "where s1acchd.grpseton = 'customer'" & IIf(UsrBr = "", "", " and isnull(branchid,'') ='" & UsrBr & "'")
            Case 7
                str = "select isnull(sum(isnull(bal,0)+opnbal),0) [amount payable (supplier)] from accmast left join balanceqr on balanceqr.accountno=accmast.accid " & _
             "left join s1acchd on s1acchd.s1accid=accmast.s1accid left join macchd on s1acchd.maccid=macchd.maccid " & _
             "where s1acchd.grpseton = 'supplier'" & IIf(UsrBr = "", "", " and isnull(branchid,'') ='" & UsrBr & "'")
            Case 8
                str = "select isnull(sum(isnull(bal,0)+opnbal),0) [amount payable (other)] from accmast left join balanceqr on balanceqr.accountno=accmast.accid " & _
               "left join s1acchd on s1acchd.s1accid=accmast.s1accid left join macchd on s1acchd.maccid=macchd.maccid " & _
               "where(macchd.maccid <= 2300 And macchd.maccid > 1000 And isnull(bal, 0) + opnbal < 0) " & _
               "and s1acchd.grpseton not in( 'cash' , 'bank' ,'customer' ,'supplier' ,'p.d.c.(i)', 'p.d.c.(r)','stock') " & _
                IIf(UsrBr = "", "", " and isnull(branchid,'') ='" & UsrBr & "'")
            Case 9
                str = "select isnull(sum(isnull(bal,0)+opnbal),0) [amount receivable (other)] from accmast left join balanceqr on balanceqr.accountno=accmast.accid " & _
                 "left join s1acchd on s1acchd.s1accid=accmast.s1accid left join macchd on s1acchd.maccid=macchd.maccid " & _
                 "where macchd.maccid<=2300 and macchd.maccid>1000  and isnull(bal,0)+opnbal>0 and s1acchd.grpseton not in( 'cash' , 'bank' ,'customer' ,'supplier','p.d.c.(i)', 'p.d.c.(r)','stock'  ) " & _
                 IIf(UsrBr = "", "", " and isnull(branchid,'') ='" & UsrBr & "'")
            Case 10
                str = "select null [for selected month]"
            Case 11
                str = "select isnull(sum(case when(dealamt > 0) then dealamt else 0 end),0) [expense paid] from " & _
             "acctrdet left join accmast on acctrdet.accountno = accmast.accid left join acctrcmn on acctrcmn.linkno = acctrdet.linkno " & _
             "where isnull(approved,0)=0 and " & _
             "jvdate >='" & Format(_FromDate, "yyyy/MM/dd") & "' and jvdate >='" & Format(_ToDate, "yyyy/MM/dd") & "' and  (accmast.accountno/10000) between 7000 and 7999 " & _
             IIf(UsrBr = "", "", " and isnull(branchid,'') ='" & UsrBr & "'")
            Case 12
                str = "select isnull(sum(netamt),0) [total purchase] from  itminvcmntb " & _
             " where trdate>='" & Format(_FromDate, "yyyy/MM/dd") & "' and trdate<='" & Format(_ToDate, "yyyy/MM/dd") & "' and trtype='ip' " & IIf(UsrBr = "", "", " and isnull(brid,'') ='" & UsrBr & "'")
            Case 13
                str = "select isnull(total,0) [total sales] from (select  sum(netamt)  total from  itminvcmntb " & _
                 "where trdate>='" & Format(_FromDate, "yyyy/MM/dd") & "' and trdate<='" & Format(_ToDate, "yyyy/MM/dd") & "' and trtype in('is','dis','sis') and invno>0  " & _
                IIf(UsrBr = "", "", " and isnull(brid,'') ='" & UsrBr & "'") & ")tr"
            Case 14
                str = "select isnull(sum(trqty*(unitcost-unitdiscount+unitothcost)),0) [total sales return] from itminvtrtb " & _
             "left join itminvcmntb on itminvtrtb.trid=itminvcmntb.trid " & _
             "where trdate>='" & Format(_FromDate, "yyyy/MM/dd") & "' and trdate<='" & Format(_ToDate, "yyyy/MM/dd") & "' and trtype='sr' " & IIf(UsrBr = "", "", " and isnull(brid,'') ='" & UsrBr & "'")
            Case 15
                str = "select isnull(sum(trqty*(unitcost-unitdiscount+unitothcost)),0) [total purchase return] from  itminvtrtb " & _
             "left join itminvcmntb on itminvtrtb.trid=itminvcmntb.trid " & _
             "where trdate>='" & Format(_FromDate, "yyyy/MM/dd") & "' and trdate<='" & Format(_ToDate, "yyyy/MM/dd") & "' and trtype='pr' " & IIf(UsrBr = "", "", " and isnull(brid,'') ='" & UsrBr & "'")
            Case 16
                str = "select isnull(sum(tamt),0) [due on purchase] from( " & _
             "select sum(dealamt) tamt,reference,min(jvtype) jvtype,min(ddate) ddate,min(cmnbrid)cmnbrid from ( " & _
             "select  acctrcmn.linkno,jvnum,jvtype,reference, dealamt, " & _
             "isnull(duedate, dateadd(d,isnull(accmast.duedays,0),jvdate)) ddate,cmnbrid from acctrdet " & _
             "left join acctrcmn on acctrdet.linkno=acctrcmn.linkno " & _
             "left join accmast on acctrdet.accountno=accmast.accid " & _
             "left join s1acchd on s1acchd.s1accid=accmast.s1accid " & _
             "where isnull(approved,0)=0 and jvtype in('ip','pv','jv') and s1acchd.grpseton = 'supplier' " & _
             IIf(UsrBr = "", "", " and isnull(cmnbrid,'') ='" & UsrBr & "'") & ") tr group by reference) tr1 " & _
             "where ddate>='" & Format(_FromDate, "yyyy/MM/dd") & "' and ddate<='" & Format(_ToDate, "yyyy/MM/dd") & "' and jvtype='ip' " & IIf(UsrBr = "", "", " and isnull(cmnbrid,'') ='" & UsrBr & "'")
            Case 17
                str = "select isnull(sum(tamt),0) [due on sales] from( " & _
             "select sum(dealamt) tamt,reference,min(jvtype) jvtype,min(ddate) ddate,min(cmnbrid)cmnbrid from ( " & _
             "select  acctrcmn.linkno,jvnum,jvtype,reference, dealamt, " & _
             "isnull(duedate, dateadd(d,isnull(accmast.duedays,0),jvdate)) ddate,cmnbrid from acctrdet left join acctrcmn on acctrdet.linkno=acctrcmn.linkno " & _
             "left join accmast on acctrdet.accountno=accmast.accid " & _
             "left join s1acchd on s1acchd.s1accid=accmast.s1accid " & _
             "where  isnull(approved,0)=0 and  jvtype in('is','rv','jv','jis') and s1acchd.grpseton = 'customer' " & _
             IIf(UsrBr = "", "", " and isnull(cmnbrid,'') ='" & UsrBr & "'") & ") tr group by reference) tr1  " & _
             "where ddate>='" & Format(_FromDate, "yyyy/MM/dd") & "' and ddate<='" & Format(_ToDate, "yyyy/MM/dd") & "' and jvtype in('is','jis') " & IIf(UsrBr = "", "", " and isnull(cmnbrid,'') ='" & UsrBr & "'")

        End Select
        If str <> "" Then
            Return _objcmndLayer._fldDatatable(str)
        End If
    End Function
    Private Sub SetGridHead()
        'Dim i As Integer
        With grdvoucher
            If .RowCount = 0 Then Exit Sub
            SetGridProperty(grdvoucher)
            .RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("ARIAL", 9.0!, FontStyle.Bold)
            .Columns("D").HeaderText = "Description"
            .Columns("amt").HeaderText = "Amount"
            .Columns("amt").Width = (Me.Width / 2) * 18 / 100 '4
            .Columns("amt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("amt").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Rows.Item(0).DefaultCellStyle.BackColor = Color.SteelBlue
            .Rows.Item(9).DefaultCellStyle.BackColor = Color.Brown
            .Rows.Item(0).DefaultCellStyle.ForeColor = Color.White
            .Rows.Item(9).DefaultCellStyle.ForeColor = Color.White
            .Rows.Item(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Rows.Item(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        End With
        resizeGridColumn(grdvoucher, 0)
    End Sub
    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        'ldSummary()
        'fwait = New WaitMessageFrm
        'fwait.ShowDialog()
        dtsummary.Rows.Clear()
        Timer2.Enabled = True
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        SetGridHead()
        resizeGridColumn(grdvoucher, 0)
    End Sub

    Private Sub grdvoucher_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdvoucher.CellDoubleClick
        Dim tp As String = ""
        Select Case grdvoucher.CurrentRow.Index
            Case 1
                tp = "PDCR"
            Case 2
                tp = "PDCI"
            Case 3
                tp = "CASH"
            Case 4
                tp = "BANK"
            Case 5
                tp = "CUSTOMER"
            Case 6
                tp = "SUPPLIER"
            Case 7
                tp = "POTH"
            Case 8
                tp = "ROTH"
        End Select
        If grdvoucher.CurrentRow.Index < 9 Then
            fMainForm.loadAccDetFromFstatus(tp)
        ElseIf grdvoucher.CurrentRow.Index = 10 Then
            fMainForm.LdVoucherlist(True)
        End If
    End Sub

    Private Sub btnweb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnweb.Click
        If Not HaveInternetConnection() Then
            MsgBox("There is no internet connection", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If MsgBox("You are going to upload data to online! Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        ftransfer = New TransferToWebFrm
        With ftransfer
            .typeofTransfer = 6
            If chkupdatedate.Checked Then
                .dbookdate1 = DateValue(dtstart.Value)
            Else
                .dbookdate1 = DateValue("01/01/1950")
            End If
            .dbookdate2 = DateValue(dtend.Value)
            .isdaybook = chkdailyreports.Checked
            .isfinancialStatus = chkfsatatus.Checked
            .isAccountbalance = chkbalance.Checked
            .isprofitloss = chkprofitloss.Checked
            '.updateDaybookUptodate = chkupdatedate.Checked
            .Show(fMainForm)
        End With

    End Sub

    Private Sub ftransfer_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles ftransfer.FormClosed
        ftransfer = Nothing
    End Sub

    Private Sub chkupdatedate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkupdatedate.CheckedChanged
        Label5.Enabled = chkupdatedate.Checked
        dtstart.Enabled = chkupdatedate.Checked
    End Sub

    Private Sub Worker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles Worker.DoWork
        Dim dt1, dt2 As Date
        dt1 = DateValue("01/" & Month(dtmonth.Value) & "/" & Year(dtmonth.Value))
        dt2 = DateAdd(DateInterval.Month, 1, DateValue(dtmonth.Value))
        dt2 = DateValue("01/" & Month(dt2) & "/" & Year(dt2))
        dt2 = DateAdd(DateInterval.Day, -1, dt2)
        ldSummaryNew(dt1, dt2)
    End Sub

    Private Sub Worker_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Worker.RunWorkerCompleted

        Dim dtr As DataRow
        For i = 0 To dsSummary.Tables.Count - 1
            dtr = dtsummary.NewRow
            dtr("D") = UCase(dsSummary.Tables(i).Columns(0).Caption)
            dtr("amt") = dsSummary.Tables(i)(0)(0)
            dtsummary.Rows.Add(dtr)
        Next
        'grdvoucher.DataSource = Nothing
        grdvoucher.DataSource = dtsummary
        SetGridHead()
        If Worker.IsBusy Then Worker.CancelAsync()
        fwait.Close()
    End Sub

    Private Sub Timer2_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Timer2.Enabled = False
        fwait = New WaitMessageFrm
        fwait.ShowDialog()
    End Sub
    Private Sub fwait_triggerEvent() Handles fwait.triggerEvent
        If Worker.IsBusy Then Worker.CancelAsync()
        Worker.RunWorkerAsync()
        Worker.WorkerSupportsCancellation = True
    End Sub
    Private Sub fwait_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fwait.FormClosed
        fwait = Nothing
    End Sub

End Class