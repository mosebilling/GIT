Public Class MonthlyFeesInvoiceFrm
#Region "Class Objects"
    Private _objcmnbLayer As New clsCommon_BL
    Private _objTr As New clsAccountTransaction
#End Region
    Private WithEvents fwait As WaitMessageFrm
    Private incomeaccount As Long
    Private rdCount As Integer
    Delegate Sub GenericDelegate(ByVal Mname As String, ByVal RecName As String, ByVal rec As Integer, ByVal count As Integer)
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub loadFees()
        Dim dt As DataTable
        Dim str As String
        str = "Select feesname,feesid  from SchoolFeesTb where isnull(feesIsactive,0)=0"
        dt = _objcmnbLayer._fldDatatable(str)
        Dim i As Integer
        cmbfees.Items.Clear()
        For i = 0 To dt.Rows.Count - 1
            cmbfees.Items.Add(dt(i)("feesname"))
        Next
        If cmbfees.SelectedIndex > 0 Then
            cmbfees.SelectedIndex = 0
        End If

    End Sub

    Private Sub MonthlyFeesInvoiceFrm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        loadFees()
    End Sub

    Private Sub btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnload.Click
        loadWaite(1)
    End Sub
    Private Sub loadStudents()
        'If cmbfees.Tag = "" Then Exit Sub
        Dim str As String
        'Dim reference As String = cmbfees.Tag & "/" & Mid(cldrdate.Text, 1, 3) & Year(cldrdate.Value)
        'str = "select 'Y' Tag, admissionno,studentname,monthref,standered,Amount,isnull(dealamt,0)dealamt,customerid from ( " & _
        '"SELECT admissionno,standered, admissionno+'/'+'" & reference & "' monthref,customerid,studentname,Amount,feesname from SchoolStudentAdmissionTb " & _
        '"left join (select studentadmissionid,feescode,Amount,feesname from SchoolStudentFeesTb  " & _
        '"inner join SchoolFeesTb on SchoolStudentFeesTb.feesmasterid=SchoolFeesTb.feesid)fees on fees.studentadmissionid=SchoolStudentAdmissionTb.studentid)tr " & _
        '"LEFT JOIN (select reference,sum(dealamt)dealamt,AccountNo from AccTrDet  " & _
        '"inner join AccTrCmn on AccTrCmn.LinkNo=AccTrDet.LinkNo where DealAmt>0 and JVType='SI' group by reference,AccountNo)acctr on tr.monthref=acctr.Reference and tr.customerid=acctr.AccountNo " & _
        '"WHERE ISNULL(dealamt,0)=0 AND feesname='" & cmbfees.Text & "'"
        str = "select 'Y' Tag, admissionno,studentname,standered,section,stphone,fathername,mothername,isnull(totalfees,0)totalfees,customerid,studentid from SchoolStudentAdmissionTb " & _
            "left join (select sum(amount)totalfees,studentadmissionid from SchoolStudentFeesTb where amount>0 group by studentadmissionid)SchoolStudentFeesTb on SchoolStudentFeesTb.studentadmissionid=SchoolStudentAdmissionTb.studentid " & _
            "where isnull(studentstatus,0)=0"
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable(str)
        grdvoucher.DataSource = dt
        With grdvoucher
            SetGridProperty(grdvoucher)
            .Columns("Tag").Width = 50
            .Columns("Tag").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns("admissionno").HeaderText = "Admission NO"
            .Columns("admissionno").Width = 150

            .Columns("studentname").HeaderText = "Student Name"
            .Columns("studentname").Width = 200

            .Columns("section").HeaderText = "Section"
            .Columns("section").Width = 150

            .Columns("stphone").HeaderText = "Phone"
            .Columns("stphone").Width = 150

            .Columns("fathername").HeaderText = "Father Name"
            .Columns("fathername").Width = 150

            .Columns("mothername").HeaderText = "Mother Name"
            .Columns("mothername").Width = 150

            .Columns("standered").HeaderText = "Standered"
            .Columns("standered").Width = 150

            .Columns("totalfees").HeaderText = "Total Fees"
            .Columns("totalfees").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("totalfees").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("totalfees").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("totalfees").Width = 150

            .Columns("customerid").Visible = False
            .Columns("studentid").Visible = False

        End With
        resizeGridColumn(grdvoucher, 2)
    End Sub

    Private Sub cmbfees_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbfees.SelectedIndexChanged
        Dim dt As DataTable
        Dim str As String
        str = "Select feescode  from SchoolFeesTb where feesname='" & cmbfees.Text & "'"
        dt = _objcmnbLayer._fldDatatable(str)
        If dt.Rows.Count > 0 Then
            cmbfees.Tag = dt(0)("feescode")
        End If
    End Sub

    Private Sub fwait_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fwait.FormClosed
        fwait = Nothing
    End Sub

    Private Sub fwait_triggerEvent() Handles fwait.triggerEvent
        If fwait.triggerType = 1 Then
            loadStudents()
        End If
        If Not fwait Is Nothing Then
            fwait.Close()
            fwait = Nothing
        End If
    End Sub
    Private Sub loadWaite(ByVal triggerType As Integer)
        If Not fwait Is Nothing Then fwait.Close() : fwait = Nothing
        fwait = New WaitMessageFrm
        With fwait
            .triggerType = triggerType
            .Show()
        End With
    End Sub
    Private Sub createFeesSI(ByVal admissionno As String, ByVal studentname As String, ByVal accountno As Long, ByVal studentid As Long)
        Dim QRY As String
        Dim month As String
        month = Format(cldrdate.Value, "MMMM/yyyy")
        month = Mid(month, 1, 3) & Year(DateValue(cldrdate.Value))
        QRY = "SELECT monthref,Reference,studentadmissionid,feesname,Amount,tr.AccountNo  FROM (SELECT admissionno+'/'+feescode+'/'+'" & month & "' monthref,customerid,studentadmissionid,feesname,SchoolStudentFeesTb.Amount,isnull(AccountNo,0)AccountNo FROM SchoolFeesTb " & _
        "LEFT JOIN (select Amount,feesmasterid,schoolfeesid,studentadmissionid from SchoolStudentFeesTb )SchoolStudentFeesTb on SchoolFeesTb.feesid=SchoolStudentFeesTb.feesmasterid " & _
        "left join SchoolStudentAdmissionTb on SchoolStudentAdmissionTb.studentid=SchoolStudentFeesTb.studentadmissionid where  isnull(isyearly,0)=0)Tr left join (" & _
        "select reference,AccountNo  from AccTrDet  " & _
        "inner join AccTrCmn on AccTrCmn.LinkNo=AccTrDet.LinkNo where DealAmt>0 and JVType='SI' group by reference,AccountNo)acctr on tr.customerid=acctr.AccountNo and tr.monthref=acctr.Reference " & _
        "where isnull(Reference,'') ='' and  studentadmissionid=" & studentid
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable(QRY)
        Dim amount As Double
        Dim accid As Long
        If dt.Rows.Count > 0 Then
            incomeaccount = 0
            incomeaccount = dt(0)("AccountNo")
            If UsrBr = "" Then
                QRY = " Select accid from accmast where AccSetId like '%" & Format(4, "00") & "%'"
                QRY = QRY & " select Prefix,InvNo from InvNos where InvType='SI'"
            Else
                QRY = " SELECT accid FROM BranchAccSet WHERE branchcode='" & UsrBr & "' and setno=" & 4
                QRY = QRY & " select Prefix,InvNo from InvNosBrTb where InvType='SI' AND Brcode='" & UsrBr & "'"
            End If
            'QRY = QRY & " select ACCID from Accmast where accountno=" & accountno
            Dim ds As DataSet
            ds = _objcmnbLayer._ldDataset(QRY, False)
            Dim prefix As String = ""
            Dim invno As Integer

            Dim customerid As Long
            If ds.Tables(0).Rows.Count > 0 And incomeaccount = 0 Then
                incomeaccount = ds.Tables(0)(0)("accid")
            End If
            If ds.Tables(1).Rows.Count > 0 Then
                prefix = ds.Tables(1)(0)("Prefix")
                invno = ds.Tables(1)(0)("InvNo")
            End If
            'If ds.Tables(2).Rows.Count > 0 Then
            '    customerid = ds.Tables(2)(0)("accid")
            'End If
            customerid = accountno
            dtAccTb.Rows.Clear()

            _objTr.JVType = "SI"
            _objTr.JVDate = DateValue(cldrtransaction.Value)
            _objTr.PreFix = prefix
            _objTr.JVNum = invno
            _objTr.JVTypeNo = getVouchernumber("SI")
            _objTr.UserId = CurrentUser
            _objTr.MchName = MACHINENAME
            _objTr.CrtDtTm = DateValue(Date.Now)
            _objTr.TypeNo = 0 ' id number from prefixtb
            _objTr.VrDescr = "FEES/" & admissionno & "/" & studentname
            _objTr.IsModi = 0
            _objTr.LinkNo = 0
            _objTr.isLinkNo = True
            _objTr.isdeleteTr = 1
            Dim i As Integer
            'For i = 0 To dt.Rows.Count - 1
            '    If Val(dt(i)("Amount")) > 0 Then
            '        setAcctrDetValue(True, customerid, dt(i)("feesname") & "/" & month, dt(i)("monthref"), dt(i)("Amount"), admissionno)
            '        amount = amount + Val(dt(i)("Amount"))
            '    End If
            'Next
            'If amount > 0 Then
            '    setAcctrDetValue(False, incomeaccount, "FEES/" & admissionno & "/" & studentname, "ON/AC", amount, admissionno)
            '    _objTr.SaveAccTrWithDt(dtAccTb)
            'End If
            For i = 0 To dt.Rows.Count - 1
                If Val(dt(i)("Amount")) > 0 Then
                    setAcctrDetValue(True, customerid, dt(i)("feesname") & "/" & month, dt(i)("monthref"), dt(i)("Amount"), admissionno)
                    amount = Val(dt(i)("Amount"))
                    accid = 0
                    accid = dt(i)("AccountNo")
                    If accid = 0 Then accid = incomeaccount
                    setAcctrDetValue(False, accid, "FEES/" & admissionno & "/" & studentname, "ON/AC", amount, admissionno)
                End If
            Next
            If amount > 0 Then
                _objTr.SaveAccTrWithDt(dtAccTb)
            End If
        End If
    End Sub
    Private Sub setAcctrDetValue(ByVal isdb As Boolean, ByVal AccountNo As Long, ByVal EntryRef As String, ByVal Reference As String, ByVal amount As Double, ByVal JobCode As String)
        Dim dtrow As DataRow
        dtrow = dtAccTb.NewRow
        dtrow("LinkNo") = 0
        dtrow("AccountNo") = AccountNo
        dtrow("Reference") = Reference
        dtrow("EntryRef") = EntryRef
        If isdb Then
            dtrow("DealAmt") = amount
        Else
            dtrow("DealAmt") = amount * -1
        End If
        dtrow("FCAmt") = dtrow("DealAmt")
        dtrow("CurrencyCode") = ""
        dtrow("CurrRate") = 1
        dtrow("TrInf") = 0
        dtrow("PDCAcc") = 0
        dtrow("ChqDate") = DateValue(Date.Now)
        dtrow("ChqNo") = ""
        dtrow("BankCode") = ""
        dtrow("UnqNo") = 0
        dtrow("JobCode") = JobCode
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

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        If grdvoucher.RowCount = 0 Then
            MsgBox("Students records not found", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        rdCount = 0
        For i = 0 To grdvoucher.RowCount - 1
            If grdvoucher.Item("tag", i).Value = "Y" Then
                rdCount = rdCount + 1
            End If
        Next
        If rdCount = 0 Then
            MsgBox("Please select students", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If MsgBox("Do you want to generate Fees Booking for the month " & cldrdate.Text, MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        Worker.RunWorkerAsync()
    End Sub
    Public Sub status(ByVal Mname As String, ByVal RecName As String, ByVal rec As Integer, ByVal count As Integer)
        If Me.InvokeRequired Then
            Dim d As New GenericDelegate(AddressOf status)
            Me.Invoke(d, Mname, RecName, rec, count)
        Else
            lblitem.Text = Mname
            If rec = 0 Then
                pb.Value = 0
            Else
                pb.Value = rec * 100 / count
            End If
        End If
    End Sub

    Private Sub Worker_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles Worker.DoWork
        Dim i As Integer
        If rdCount > 0 Then
            For i = 0 To grdvoucher.RowCount - 1
                With grdvoucher
                    If .Item("tag", i).Value = "Y" Then
                        createFeesSI(.Item("admissionno", i).Value, .Item("studentname", i).Value, .Item("customerid", i).Value, .Item("studentid", i).Value)
                        status("Updating Fees Booking :- " & .Item("admissionno", i).Value, .Item("studentname", i).Value, i, rdCount)
                    End If
                End With
            Next
            status("Updating Fees Booking :- Updated", "", rdCount, rdCount)
        Else
            MsgBox("Please select students", MsgBoxStyle.Exclamation)
        End If
       
    End Sub

    Private Sub Worker_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Worker.RunWorkerCompleted
        MsgBox("Updated " & rdCount & " Records", MsgBoxStyle.Information)
        pb.Value = 0
    End Sub

    Private Sub grdvoucher_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdvoucher.CellClick
        If e.ColumnIndex = 0 Then
            With grdvoucher
                If .Item(0, e.RowIndex).Value = "Y" Then
                    .Item(0, e.RowIndex).Value = ""
                Else
                    .Item(0, e.RowIndex).Value = "Y"
                End If
            End With
        End If
    End Sub
End Class