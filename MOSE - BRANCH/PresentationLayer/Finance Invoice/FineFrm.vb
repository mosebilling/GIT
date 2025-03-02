Imports DataAccessLayer


Public Class FineFrm
    Private _objReport As New clsReport_BL
#Region "Properties"
    Private _instamonth As DateTime
    Public Property instamonth() As DateTime
        Get
            Return _instamonth
        End Get
        Set(ByVal value As DateTime)
            _instamonth = value
        End Set
    End Property
#End Region
    Private _cmnDlink As New cl_CmnDataLink()
    Private _cmnMthds As New CmnMethods()
    Private _objcmnbLayer As New clsCommon_BL
    Private _gridItems As DataTable
    Private dtTable As DataTable

    Private _objTr As New clsAccountTransaction
    Private TrTypeNo As Integer
    Private Dt As Date

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub FineFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loadserviceincome()
    End Sub
    Private Sub loadserviceincome()

        Dim qry As String

        If UsrBr = "" Then
            qry = "SELECT accid,AccDescr FROM AccMast WHERE AccSetId Like '%4%'"
        Else
            qry = " SELECT accid,AccDescr FROM BranchAccSet WHERE branchcode='" & UsrBr & "' and setno=" & 4
        End If
        dtTable = _objcmnbLayer._fldDatatable(qry)
        If dtTable.Rows.Count > 0 Then
            txtotherincome.Text = dtTable(0)("AccDescr")
            txtotherincome.Tag = dtTable(0)("accid")
        End If
       

    End Sub
    Private Sub loadFineCalculation()
        Dim dt As DataTable
        dt = _objReport.ProcessfineTb(DateValue(cldrdate.Value)).Tables(0)
        grdVoucher.DataSource = dt
        SetGridHead()
        If (dt.Rows.Count > 0) Then
            lblemidate1.Text = dt(0)("emidate")
            lblemidate2.Text = dt(0)("emidate2")
        End If
       
        lbltotallatefee.Text = Format(0, numFormat)
        Dim amt As String
        Dim dblAmount As Double
        amt = Trim(dt.Compute("SUM(latefee)", "") & "")
        If Val(amt) > 0 Then
            dblAmount = Convert.ToDouble(amt)
            lbltotallatefee.Text = Format(dblAmount, numFormat)
        End If
        nextVoucher()
    End Sub
    Private Sub SetGridHead()
        With grdVoucher
            SetGridProperty(grdVoucher)

           
            .Columns("TrRefNo").HeaderText = "Inv No"
            .Columns("TrRefNo").Width = 75
            .Columns("CashCustName").HeaderText = "Customer Name"
            '.Columns("CashCustName").Width = 150
            .Columns("NetAmt").HeaderText = "Net Amount"
            .Columns("NetAmt").Width = 125
            .Columns("NetAmt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("NetAmt").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("amount").HeaderText = "Installment Amount"
            .Columns("amount").Width = 125
            .Columns("amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("amount").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("installmentdate").HeaderText = "Installment Date"
            .Columns("installmentdate").Width = 125
            .Columns("latefee1").HeaderText = "Late Fee1"
            .Columns("latefee1").Width = 125
            .Columns("latefee1").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("latefee1").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("latefee2").HeaderText = "Late Fee2"
            .Columns("latefee2").Width = 125
            .Columns("latefee2").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("latefee2").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("latefee").HeaderText = "Total Late Fee"
            .Columns("latefee").Width = 125
            .Columns("latefee").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("latefee").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("emidate").HeaderText = "EMI Date"
            .Columns("emidate").Width = 125
            .Columns("emidate").Visible = False
            .Columns("emidate2").HeaderText = "Next EMI"
            .Columns("emidate2").Width = 125
            .Columns("emidate2").Visible = False
            .Columns("CSCode").HeaderText = "CSCode"
            .Columns("CSCode").Width = 125
            .Columns("CSCode").Visible = False
            .Columns("trid").HeaderText = "trid"
            .Columns("trid").Visible = False
            .Columns("id").Visible = False
            .Columns("firstemi").Visible = False
            .Columns("secondemi").Visible = False
            resizeGridColumn(grdVoucher, 1)
        End With
    End Sub
    Private Sub btnProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        Dim month As Date
        month = cldrdate.Value
        loadFineCalculation()
    End Sub
   
    Private Sub nextVoucher()
        Try
            Dim vrInvoice As String = ""
            Dim vrPrefix As String = ""
            Dim a, b As String
            a = ""
            b = ""
           
           

            getVrsDet(0, "JV", vrPrefix, vrInvoice, a, b)


            numVchrNo.Text = Val(vrInvoice)
            txtprefix.Text = Trim(vrPrefix)



        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
 

    Private Sub setAcctrCmnValue()
        _objTr.JVType = "JV"
        _objTr.JVDate = DateValue(cldrdate.Value)
        _objTr.PreFix = txtprefix.Text
        _objTr.JVNum = Val(numVchrNo.Text)
        _objTr.JVTypeNo = getVouchernumber("JV") 'TrTypeNo '' getVouchernumber("IS")
        _objTr.UserId = Trim(CurrentUser & "")
        _objTr.MchName = MACHINENAME
        _objTr.CrtDtTm = DateValue(Date.Now)
        _objTr.TypeNo = 0 ' id number from prefixtb
        _objTr.VrDescr = ""
        _objTr.IsModi = 0
        _objTr.LinkNo = 0
        _objTr.isLinkNo = True
    End Sub

    Private Sub setAcctrDetValue(ByVal lnkNo As Long, ByVal _row As Integer)
        Dim dtrow As DataRow
        Dim monthName As String
        monthName = Month(cldrdate.Text)
        dtrow = dtAccTb.NewRow
        dtrow("LinkNo") = lnkNo
        dtrow("AccountNo") = Trim(grdVoucher.Item("CSCode", _row).Value & "")
        dtrow("Reference") = Trim(grdVoucher.Item("TrRefNo", _row).Value & "")
        dtrow("EntryRef") = "Installment fine booking entry for the month"
        dtrow("DealAmt") = CDbl(grdVoucher.Item("latefee", _row).Value)
        dtrow("FCAmt") = CDbl(grdVoucher.Item("latefee", _row).Value)
        dtrow("CurrencyCode") = ""
        dtrow("CurrRate") = 1
        dtrow("TrInf") = 0
        dtrow("PDCAcc") = 0

        dtrow("ChqDate") = DateValue(Date.Now)

        dtrow("ChqNo") = 0
        dtrow("BankCode") = 0
        dtrow("UnqNo") = 0
        dtAccTb.Rows.Add(dtrow)
        Dim j As Integer
        Dim dtype As String
        For j = 0 To dtAccTb.Columns.Count - 1
            dtype = dtAccTb.Columns(j).DataType.Name
            If Trim(dtAccTb(dtAccTb.Rows.Count - 1)(j) & "") = "" Then
                Select Case dtype
                    Case "String"
                        dtAccTb(dtAccTb.Rows.Count - 1)(j) = ""
                    Case "Int64", "Int32", "Double", "Decimal", "Boolean"
                        dtAccTb(dtAccTb.Rows.Count - 1)(j) = 0
                End Select
            End If
nxt:
        Next
    End Sub
    Private Sub setAcctrotherIncmValue(ByVal lnkNo As Long, ByVal _row As Integer)
        Dim dtrow As DataRow
        Dim monthName As String
        monthName = Month(cldrdate.Text)
        dtrow = dtAccTb.NewRow
        dtrow("LinkNo") = lnkNo
        dtrow("AccountNo") = Val(txtotherincome.Tag)
        dtrow("Reference") = "On Account"
        dtrow("EntryRef") = "Installment fine booking entry for the month"
        dtrow("DealAmt") = CDbl(lbltotallatefee.Text) * -1
        dtrow("FCAmt") = CDbl(lbltotallatefee.Text) * -1
        dtrow("CurrencyCode") = ""
        dtrow("CurrRate") = 1
        dtrow("TrInf") = 0
        dtrow("PDCAcc") = 0

        dtrow("ChqDate") = DateValue(Date.Now)

        dtrow("ChqNo") = 0
        dtrow("BankCode") = 0
        dtrow("UnqNo") = 0
        dtAccTb.Rows.Add(dtrow)
        Dim j As Integer
        Dim dtype As String
        For j = 0 To dtAccTb.Columns.Count - 1
            dtype = dtAccTb.Columns(j).DataType.Name
            If Trim(dtAccTb(dtAccTb.Rows.Count - 1)(j) & "") = "" Then
                Select Case dtype
                    Case "String"
                        dtAccTb(dtAccTb.Rows.Count - 1)(j) = ""
                    Case "Int64", "Int32", "Double", "Decimal", "Boolean"
                        dtAccTb(dtAccTb.Rows.Count - 1)(j) = 0
                End Select
            End If
nxt:
        Next
    End Sub
    Private Sub saveTransaction()
        Try
chkagain:

            If Not CheckNoExists(txtprefix.Text, Val(numVchrNo.Text), "JV", "Accounts") Then
                If MsgBox("Voucher Number alreary exist! Do you want to fill next number?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    numVchrNo.Text = Val(numVchrNo.Text) + 1
                    GoTo chkagain
                Else
                    numVchrNo.Focus()
                    Exit Sub
                End If
            End If

            If dtAccTb.Rows.Count > 0 Then dtAccTb.Rows.Clear()
            Dim LinkNo As Long
            setAcctrCmnValue()

            'LinkNo = Val(_objTr.SaveAccTrCmn())
            '_objcmnbLayer._saveDatawithOutParm("Update AccTrDet set setremove=1 where LinkNo=" & LinkNo)
            Dim i As Integer
            With grdVoucher

                For i = 0 To .RowCount - 1
                    If Val(grdVoucher.Item("latefee", i).Value & "") = 0 Or Val(grdVoucher.Item("CSCode", i).Value & "") = 0 Then GoTo nxt
                    setAcctrDetValue(LinkNo, i)
                    '_objTr.saveAccTrans()
nxt:
                Next
            End With

            setAcctrotherIncmValue(LinkNo, i)

            '_objcmnbLayer._saveDatawithOutParm("Delete from AccTrDet  where setremove=1 and LinkNo=" & LinkNo)
            Dim stringLnkNo As String
            Dim qry As String
            stringLnkNo = _objTr.SaveAccTrWithDt(dtAccTb)
            With grdVoucher
                For i = 0 To .RowCount - 1

                    qry = qry + "insert into FineTb(trid,finedate,installmentid,firstemi,secondemi,fineamount,LnkNo)values(" & _
                                       Val(.Item("trid", i).Value & "") & ",'" & Format(DateValue(cldrdate.Text), "yyyy/MM/dd") & "'," & _
                                        Val(.Item("id", i).Value & "") & "," & Val(.Item("latefee1", i).Value & "") & _
                                        "," & Val(.Item("latefee2", i).Value & "") & "," & Val(.Item("latefee", i).Value & "") & "," & Val(stringLnkNo) & ")" & vbCrLf
                Next
            End With
            _objcmnbLayer._saveDatawithOutParm(qry)


            SetNextVrNo(numVchrNo, 0, "JV", "JvType = 'JV' AND JvNum = ", True, True, True)
            


            MsgBox("Fine Data is succesfully posted with JV. # " & numVchrNo.Text & ".", MsgBoxStyle.Information)
            loadFineCalculation()
        Catch ex As Exception
            MsgBox("Proc: saveTransaction" & vbCrLf & ex.Message, , "Postinng Falid")

        End Try
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        verify()
    End Sub
    Private Sub verify()
        saveTransaction()
    End Sub
End Class