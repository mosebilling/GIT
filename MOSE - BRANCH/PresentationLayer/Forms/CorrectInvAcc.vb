Public Class CorrectInvAcc
    Private _objcmn As New clsCommon_BL
    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        Dim i As Integer
        Dim dt As DataTable
        Dim dtTable As DataTable
        Dim dtacc As DataTable
        'Dim LinkNo As Long
        'Dim salesAmt As Double
        Me.Cursor = Cursors.WaitCursor
        dtacc = _objcmn._fldDatatable("Select Accid from accmast where alias='" & txtaccount.Text & "'")
        If dtacc.Rows.Count > 0 Then
            txtaccount.Tag = dtacc(0)("Accid")
        Else
            MsgBox("Invalid Account")
            txtaccount.Focus()
            GoTo ext
        End If
        If cmbvoucher.Text = "" Then
            MsgBox("Invalid Voucher type")
            cmbvoucher.Focus()
            GoTo ext
        End If
        Dim NetAmt As Double
        Dim r As Integer
        'dt = _objcmn._fldDatatable("Select ItmInvCmnTb.trid,ttl,rndoff,discount,taxAmt,NetAmt from ItmInvCmnTb " & _
        '                           "LEFT JOIN (Select trid,sum(TrQty*(UnitCost-ItemDiscount)) ttl,sum(taxAmt) taxAmt from ItmInvTrTb group by trid) tr on ItmInvCmnTb.trid=tr.trid where trtype='" & cmbvoucher.Text & "'")
        dt = _objcmn._fldDatatable("Select AccTrDet.LinkNo,UnqNo,jvnum,jvdate from AccTrDet left join AccTrCmn on AccTrDet.linkno=AccTrCmn.linkno where jvtype='" & cmbvoucher.Text & "' and dealamt=0")
        For i = 0 To dt.Rows.Count - 1
            NetAmt = 0
            dtTable = _objcmn._fldDatatable("SELECT  dealamt netamt FROM AccTrDet WHERE linkno  = " & dt(i)("LinkNo") & " order by dealamt desc")
            For r = 0 To dtTable.Rows.Count - 1
                NetAmt = NetAmt + CDbl(dtTable(r)("NetAmt"))
            Next
            _objcmn._saveDatawithOutParm("Update AccTrDet set dealamt=" & (NetAmt * -1) & " where UnqNo=" & Val(dt(i)("UnqNo")))
        Next
        MsgBox("Updated", MsgBoxStyle.Information)
ext:
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub CorrectInvAcc_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
    Private Sub updateacc()
        Dim dtacc As DataTable
        dtacc = _objcmn._fldDatatable("Select Accid from accmast where alias='" & txtaccount.Text & "'")
        If dtacc.Rows.Count > 0 Then
            txtaccount.Tag = dtacc(0)("Accid")
        Else
            MsgBox("Invalid Account")
            txtaccount.Focus()
            Exit Sub
        End If
        _objcmn._saveDatawithOutParm("update AccTrDet set dealamt=dealamt*-1 WHERE dealamt>0 and accountno=" & Val(txtaccount.Tag))
        MsgBox("Done")
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        updateacc()
    End Sub
End Class