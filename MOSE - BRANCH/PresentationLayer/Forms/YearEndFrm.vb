Public Class YearEndFrm
    Private dttable As DataTable
    Private _objcmnbLayer As New clsCommon_BL
    Private _objTr As New clsAccountTransaction
    Private seconddb As String
    Private acctb As DataTable
    Delegate Sub GenericDelegate(ByVal Mname As String, ByVal RecName As String, ByVal rec As Integer, ByVal count As Integer)
    Private Sub YearEndFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadDatabases()
    End Sub
    Sub LoadDatabases()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select * from sys.databases where database_id>4")
        Dim i As Integer
        cmbseconddb.Items.Clear()
        cmbseconddb.Items.Add("")
        For i = 0 To dt.Rows.Count - 1
            cmbseconddb.Items.Add(dt(i)(0))
        Next
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        'If acctb.Rows.Count > 0 Then acctb.Rows.Clear()
        'loadCustomerbalance()
        getPdc()
        MsgBox("Loaded", MsgBoxStyle.Information)
    End Sub
    Private Sub getPdc()
        dttable = Nothing
        dttable = _objTr.returnPDCTransfer(0, 0).Tables(0)
        Dim str As String
        str = " Select top 1 LinkNo,AccountNo,DueDate,Reference,EntryRef,DealAmt,FCAmt,CurrencyCode,CurrRate, " & _
                                                  "JobCode,PDCAcc,BankCode,LPONo,OthCost,TrInf,TermsId,CustAcc,AccWithRef,ChqDate,ChqNo,SuppInvDate," & _
                                                  "vatid,isvatEntry,UnqNo,setoffCount,'' Jvdate from AccTrDet"
        acctb = _objcmnbLayer._fldDatatable(str)
        acctb.Rows.Clear()
        Dim i As Integer
        For i = 0 To dttable.Rows.Count - 1
            setAcctrDetValue(0, i)
        Next
        dttable = Nothing
        dttable = _objTr.returnPDCTransfer(1, 0).Tables(0)
        For i = 0 To dttable.Rows.Count - 1
            setAcctrDetValue(0, i)
        Next
    End Sub
    Private Sub loadCustomerbalance()
        Dim str As String
        str = "select tr.*,AccDescr,Debit-Credit DealAmt,'' ChqDate,''ChqNo,'' BankCode,jvdate from (" & _
        "select accountno,reference,sum(case when dealamt>0 then dealamt else 0 end) Debit," & _
        "sum(case when dealamt<0 then dealamt*-1 else 0 end) Credit,min(EntryRef)EntryRef,min(jvdate)jvdate " & _
        "from acctrdet left join AccTrCmn on AccTrDet.LinkNo=AccTrCmn.LinkNo  group by accountno,reference)tr " & _
        "left join AccMast on tr.AccountNo=AccMast.AccId " & _
        "left join s1acchd on accmast.s1accid=S1AccHd.S1AccId " & _
        "where Debit-Credit<>0 and GrpSetOn in ('customer','supplier') order by tr.AccountNo"
        dttable = _objcmnbLayer._fldDatatable(str)
        Dim i As Integer
        For i = 0 To dttable.Rows.Count - 1
            setAcctrDetValue(0, i)
        Next
    End Sub
    Private Sub setAcctrDetValue(ByVal lnkNo As Long, ByVal _row As Integer)
        Dim dtrow As DataRow
        dtrow = acctb.NewRow
        dtrow("LinkNo") = lnkNo
        dtrow("AccountNo") = dttable(_row)("accountno")
        dtrow("Reference") = dttable(_row)("reference")
        dtrow("EntryRef") = dttable(_row)("EntryRef")
        dtrow("DealAmt") = dttable(_row)("DealAmt")
        dtrow("FCAmt") = dttable(_row)("DealAmt")
        dtrow("CurrencyCode") = ""
        dtrow("CurrRate") = 1
        dtrow("TrInf") = 0
        dtrow("PDCAcc") = 0
        dtrow("ChqDate") = dttable(_row)("ChqDate")
        dtrow("ChqNo") = dttable(_row)("ChqNo")
        dtrow("BankCode") = dttable(_row)("BankCode")
        dtrow("jvdate") = dttable(_row)("jvdate")
        acctb.Rows.Add(dtrow)
        Dim j As Integer
        Dim dtype As String
        For j = 0 To acctb.Columns.Count - 1
            dtype = acctb.Columns(j).DataType.Name
            If Trim(acctb(acctb.Rows.Count - 1)(j) & "") = "" Then
                Select Case dtype
                    Case "String"
                        acctb(acctb.Rows.Count - 1)(j) = ""
                    Case "Int64", "Int32", "Double", "Decimal", "Boolean"
                        acctb(acctb.Rows.Count - 1)(j) = 0
                End Select
            End If
nxt:
        Next
    End Sub

    Private Sub btntransfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btntransfer.Click
        If acctb.Rows.Count = 0 Then
            MsgBox("Record not found", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If cmbseconddb.Text <> "" Then
            seconddb = cmbseconddb.Text
        Else
            Exit Sub
        End If
        Worker.WorkerReportsProgress = True
        Worker.WorkerSupportsCancellation = True
        Worker.RunWorkerAsync()
    End Sub
    Private Sub saveOB(ByVal tp As Integer)
        Dim str As String
        Dim i As Integer
        Dim id As Long
        For i = 0 To acctb.Rows.Count - 1
            If Val(acctb(i)("DealAmt") & "") <> 0 Then
                id = 0
                str = "insert into " & seconddb & ".dbo.AccTrCmn(jvtype,jvdate,PreFix,JVNum,JVTypeNo,cmnbrid) values('OB','" & Format(DateValue(acctb(i)("jvdate")), "yyyy/MM/dd") & "','',0,0,'ZUBARA')"
                str = str & "declare @id bigint set @id=SCOPE_IDENTITY() select @id id"
                id = _objcmnbLayer.ExecuteScalar(str)
                'dtAcc(i)("LinkNo") = id
                str = "insert into " & seconddb & ".dbo.AccTrDet (linkno,AccountNo,Reference,EntryRef,DealAmt,CurrencyCode,CurrRate,ChqNo,ChqDate,BankCode) values (" & _
                        id & "," & acctb(i)("AccountNo") & ",'" & acctb(i)("Reference") & "','" & _
                        acctb(i)("EntryRef") & "'," & CDbl(acctb(i)("DealAmt")) * IIf(tp = 0, 1, -1) & ",'',1,'" & acctb(i)("ChqNo") & "','" & _
                        Format(DateValue(acctb(i)("ChqDate")), "yyyy/MM/dd") & "','" & acctb(i)("ChqNo") & "')"
                '_objcmnbLayer._saveDatawithOutParm(str)
                str = str & " declare @opbal money select @opbal =sum(dealamt) from " & seconddb & ".dbo.acctrdet " & _
                "left join " & seconddb & ".dbo.acctrcmn on " & seconddb & ".dbo.acctrdet.linkno=" & seconddb & ".dbo.acctrcmn.linkno where jvtype='OB' AND Accountno=" & acctb(i)("AccountNo") & _
                " update " & seconddb & ".dbo.AccMast set opnbal=isnull(@opbal,0),IsOBDet=1 where accid= " & acctb(i)("AccountNo")
                _objcmnbLayer._saveDatawithOutParm(str)

            End If
            status("Year Ending..", acctb(i)("reference"), i, acctb.Rows.Count)
        Next

    End Sub

    Private Sub Worker_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles Worker.DoWork
        saveOB(1)
    End Sub

    Private Sub Worker_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Worker.RunWorkerCompleted
        MsgBox("Transfer Completed", MsgBoxStyle.Information)
    End Sub
    Public Sub status(ByVal Mname As String, ByVal RecName As String, ByVal rec As Integer, ByVal count As Integer)
        If Me.InvokeRequired Then
            Dim d As New GenericDelegate(AddressOf status)
            Me.Invoke(d, Mname, RecName, rec, count)
        Else
            lblmodule.Text = Mname
            lblrec.Text = rec & " / " & count
            If rec = 0 Then
                pb.Value = 0
            Else
                pb.Value = rec * 100 / count
            End If

        End If
    End Sub
End Class