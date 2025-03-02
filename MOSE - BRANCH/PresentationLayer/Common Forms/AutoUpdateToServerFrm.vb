Public Class AutoUpdateToServerFrm
    Private _objcmnbLayer As New clsCommon_BL
    Private _objinv As clsInvoice
    Private _objdlalyer As New Dlayer
    Private recordcount As Integer
    Delegate Sub GenericDelegate(ByVal Mname As String, ByVal RecName As String, ByVal rec As Integer, ByVal count As Integer)
    Private dtInvTb As DataTable
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        dtInvTb = _objcmnbLayer._fldDatatable("Select top 1 * from ItmInvTrTb")
        Timer1.Enabled = False
        Worker.RunWorkerAsync()
        Worker.WorkerSupportsCancellation = True
    End Sub

    Private Sub AutoUpdateToServerFrm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Timer1.Enabled = True
    End Sub

    Private Sub Worker_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles Worker.DoWork
        Dim isconnected As Boolean
        If enableWebIntegration Then
            isconnected = getWebSettings()
        End If
        'MsgBox(isconnected)
        If isconnected Then
            Dim dt As DataTable
            Dim i As Integer
            dt = _objcmnbLayer._fldDatatable("select ItmInvCmnTb.trid,isnull(InvTypeNo,0)InvTypeNo,TrDate,TrType,isnull(PreFix,'')PreFix,InvNo,CSCode,TrRefNo,TrDescription,UserId," & _
                "Discount,OthCost,ItmInvCmnTb.AreaCode,[Job Code],LPO,IsFC,FCRate,NFraction,FC,CrtDt,ModiDt,NetAmt,MchName,rndoff,OthrCust," & _
                "TaxType,case when isnull(CashCustName,'')='' then accdescr else CashCustName end CashCustName,isnull(cashAmount,0)cashAmount,isnull(CardAmount,0)CardAmount,isTaxInvoice," & _
                "isB2B,isnull(tenderd,0)tenderd,isnull(poschange,0)poschange,customerPhone,GSTN ,lntotal,taxAmt from ItmInvCmnTb " & _
                "left join accmast on ItmInvCmnTb.cscode=accmast.accid " & _
                "left join (select sum(lntotal)lntotal,sum(taxAmt)taxAmt,trid from (" & _
                "select trid,(UnitCost-(isnull(ItemDiscount,0)/TrQty))*TrQty lntotal,taxAmt+isnull(CessAmt,0) taxAmt from ItmInvTrTb)tr group by trid ) tr on ItmInvCmnTb.trid=tr.trid " & _
                "where trtype in ('IS','SR') and isnull(ItmInvCmnTb.isupdatedToweb,0)=0 and isnull(invstatus,0)=0")

            recordcount = dt.Rows.Count
            For i = 0 To dt.Rows.Count - 1
                _objinv = New clsInvoice
                With _objinv
                    .TrId = dt(i)("trid")
                    .InvTypeNo = dt(i)("InvTypeNo")
                    .TrDate = dt(i)("TrDate")
                    .TrType = dt(i)("TrType")
                    .Prefix = dt(i)("PreFix")
                    .InvNo = dt(i)("InvNo")
                    .CSCode = dt(i)("CSCode")
                    .TrRefNo = dt(i)("TrRefNo")
                    .TrDescription = dt(i)("TrDescription")
                    .UserId = dt(i)("UserId")
                    .Discount = dt(i)("Discount")
                    .OthCost = dt(i)("OthCost")
                    .AreaCode = dt(i)("AreaCode")
                    .JobCode = dt(i)("Job Code")
                    .LPO = dt(i)("LPO")
                    .IsFC = dt(i)("IsFC")
                    .FCRate = dt(i)("FCRate")
                    .NFraction = dt(i)("NFraction")
                    .FC = dt(i)("FC")
                    .CrtDt = dt(i)("CrtDt")
                    If Not IsDBNull(dt(i)("ModiDt")) Then
                        .ModiDt = dt(i)("ModiDt")
                    Else
                        .ModiDt = DateValue("01/01/1950")
                    End If
                    .NetAmt = dt(i)("NetAmt")
                    .MchName = dt(i)("MchName")
                    .rndoff = dt(i)("rndoff")
                    .OthrCust = dt(i)("OthrCust")
                    .TaxType = dt(i)("TaxType")
                    .CashCustName = dt(i)("CashCustName")
                    .cashAmount = dt(i)("cashAmount")
                    .CardAmount = dt(i)("CardAmount")
                    .isTaxInvoice = dt(i)("isTaxInvoice")
                    .isB2B = IIf(Val(dt(i)("isB2B") & "") = 0, 0, 1)
                    .tenderd = dt(i)("tenderd")
                    .poschange = dt(i)("poschange")
                    .customerPhone = dt(i)("customerPhone")
                    .GSTN = dt(i)("GSTN")
                    .UserId = dt(i)("UserId")
                    .grossTotal = dt(i)("lntotal")
                    .taxAmt = dt(i)("taxAmt")
                    _objdlalyer.ExportItmInvCmnTbToWebserver(_objinv)
                    transferInventoryDet(dt(i)("trid"))
                    _objinv = Nothing
                    _objcmnbLayer._saveDatawithOutParm("update ItmInvCmnTb set isupdatedToweb=1 where trid=" & dt(i)("trid"))
                    status("", "", i + 1, dt.Rows.Count - 1)
                End With
            Next
        End If

    End Sub
    Private Sub transferInventoryDet(ByVal trid As Long)
        Dim dt As DataTable
        dtInvTb.Rows.Clear()
        Dim str As String
        str = "select * from ItmInvTrTb where trid=" & trid
        dt = _objcmnbLayer._fldDatatable(str)
        Dim dtrow As DataRow
        Dim i As Integer
        For i = 0 To dt.Rows.Count - 1
            dtrow = dtInvTb.NewRow
            dtrow("TrId") = trid
            dtrow("ItemId") = dtInvTb(i)("itemid")
            dtrow("Unit") = dtInvTb(i)("Unit")
            dtrow("TrQty") = dtInvTb(i)("TrQty")
            If Val(dtrow("ItemId")) = 0 Then
                dtrow("TrQty") = 1
                dtrow("UnitCost") = 0
                dtrow("taxP") = 0
                dtrow("taxAmt") = 0
                dtrow("UnitDiscount") = 0
            End If
            dtInvTb.Rows.Add(dtrow)
            Dim j As Integer
            Dim dtype As String
            For j = 0 To dtInvTb.Columns.Count - 1
                If dtInvTb.Columns(j).ColumnName = "id" Then GoTo nxt
                dtype = dtInvTb.Columns(j).DataType.Name
                If Trim(dtInvTb(i)(j) & "") = "" Then
                    Select Case dtype
                        Case "String"
                            dtInvTb(i)(j) = ""
                        Case "Int64", "Int32", "Double", "Decimal", "Boolean"
                            dtInvTb(i)(j) = 0
                    End Select
                End If
nxt:
            Next
        Next
        _objdlalyer.saveItmInvTrTbBulkToWeb(dtInvTb)
    End Sub

    Private Sub Worker_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Worker.RunWorkerCompleted
        Timer1.Enabled = True
        If recordcount > 0 Then
            Me.Text = "Update Completed"
        Else
            Me.Text = "Record not found"
        End If
        recordcount = 0
    End Sub
    Public Sub status(ByVal Mname As String, ByVal RecName As String, ByVal rec As Integer, ByVal count As Integer)
        On Error Resume Next
        If Me.InvokeRequired Then
            Dim d As New GenericDelegate(AddressOf status)
            Me.Invoke(d, Mname, RecName, rec, count)
        Else
            If count = 0 Then
                Me.Text = "Record not found"
            End If
            If rec = 0 Then
                pb.Value = 0
            Else
                If (rec * 100) / count > 100 Then
                    pb.Value = 100
                Else
                    pb.Value = (rec * 100) / count
                End If
                Me.Text = "Auto Update processing.. (" & rec & "/" & count & ")"
            End If
        End If
    End Sub

End Class