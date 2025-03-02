Public Class SchoolDXBDashBoardFrm
    Private _objcommonlayer As New clsCommon_BL
    Private dttable As DataTable
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub loadDueInstallments()
        Dim ds As DataSet
        Dim str As String
        Dim condition As String = ""
        If rdopending.Checked Then
            condition = " and amount-isnull(rvamt,0)>0"
        ElseIf rdopaid.Checked Then
            condition = " and amount-isnull(rvamt,0)=0"
        End If
        If chkdate.Checked Then
            condition = condition & " AND installmentdate>='" & Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd") & _
        "' and installmentdate<='" & Format(DateValue(cldrEnddate.Value), "yyyy/MM/dd") & "'"
        Else
            condition = condition & " and installmentdate<='" & Format(DateValue(cldrEnddate.Value), "yyyy/MM/dd") & "'"
        End If
        str = "select ROW_NUMBER() OVER(ORDER BY admissionno ASC) AS slno, admissionno,installmentdate,studentname,invitm.Description coursename,stphone, " & _
        "fathername,stadd1,stadd2,amount,amount-isnull(rvamt,0) balance,studentid from InstallmentTb " & _
        "left join (select sum(DealAmt*-1)rvamt,insmntid from AccTrDet  " & _
        "left join AccTrCmn ON AccTrDet.LinkNo=AccTrCmn.LinkNo where JVType='RV' group by insmntid)rv on InstallmentTb.id=rv.insmntid " & _
        "inner join ItmInvCmnTb on InstallmentTb.trid=ItmInvCmnTb.TrId  " & _
        "left join ItmInvTrTb on ItmInvCmnTb.TrId=ItmInvTrTb.TrId " & _
        "left join InvItm on ItmInvTrTb.ItemId=InvItm.ItemId " & _
        "inner join SchoolStudentAdmissionTb on ItmInvCmnTb.CSCode=SchoolStudentAdmissionTb.customerid " & _
        "where isnull(studentstatus,0)=0 " & condition

        str = str & " select coursename,count(admissionno)Students, sum(balance) Amount from(" & str & ")tr group by coursename"

        str = str & " select invitm.Description,count(admissionno)Students from ItmInvCmnTb " & _
        "left join ItmInvTrTb on ItmInvCmnTb.TrId=ItmInvTrTb.TrId " & _
        "left join InvItm on ItmInvTrTb.ItemId=InvItm.ItemId " & _
        "inner join SchoolStudentAdmissionTb on ItmInvCmnTb.CSCode=SchoolStudentAdmissionTb.customerid where isnull(studentstatus,0)=0 group by invitm.Description"

        ds = _objcommonlayer._ldDataset(str, False)
        dttable = ds.Tables(0)
        grdvoucher.DataSource = dttable
        grdcoursesummary.DataSource = ds.Tables(1)
        grdcourse.DataSource = ds.Tables(2)
        With grdvoucher
            SetGridProperty(grdvoucher)
            .Columns("slno").HeaderText = "SLNO"
            .Columns("slno").Width = 50
            .Columns("slno").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns("admissionno").HeaderText = "AdmissionNo"
            .Columns("admissionno").Width = 100

            .Columns("installmentdate").HeaderText = "Date"
            .Columns("installmentdate").Width = 80

            .Columns("studentname").HeaderText = "Student Name"
            .Columns("studentname").Width = 150

            .Columns("coursename").HeaderText = "Course Name"
            .Columns("coursename").Width = 150

            .Columns("fathername").HeaderText = "Father Name"
            .Columns("fathername").Width = 150

            .Columns("stphone").HeaderText = "Phone"
            .Columns("stphone").Width = 80

            .Columns("stadd1").HeaderText = "Address 1"
            .Columns("stadd1").Width = 150

            .Columns("stadd2").HeaderText = "Address 2"
            .Columns("stadd2").Width = 150

            .Columns("amount").HeaderText = "Amount"
            .Columns("amount").Width = 80
            .Columns("amount").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("amount").DefaultCellStyle.BackColor = Color.LightGreen

            .Columns("balance").HeaderText = "Due Amount"
            .Columns("balance").Width = 100
            .Columns("balance").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("balance").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("balance").DefaultCellStyle.BackColor = Color.LightPink

            .Columns("studentid").Visible = False
            Dim i As Integer
            Dim currentdate As Date
            currentdate = DateValue(Date.Now)
            Dim amt As Double
            For i = 0 To grdvoucher.RowCount - 1
                Dim instadate As Date
                instadate = DateValue(.Item("installmentdate", i).Value)
                If DateDiff(DateInterval.Day, instadate, currentdate) <= 10 Then
                    .Rows.Item(i).DefaultCellStyle.BackColor = Color.Yellow
                ElseIf DateDiff(DateInterval.Day, instadate, currentdate) > 10 Then
                    .Rows.Item(i).DefaultCellStyle.BackColor = Color.LightCoral
                End If
                amt = CDbl(.Item("balance", i).Value) + amt
            Next
            For i = 0 To .ColumnCount - 2
                cmbOrder.Items.Add(.Columns(i).HeaderText)
            Next
            If cmbOrder.Items.Count > 0 Then cmbOrder.SelectedIndex = 3
            lblnetsales.Text = Format(amt, numFormat)
        End With
        resizeGridColumn(grdvoucher, 3)
        With grdcoursesummary
            SetGridProperty(grdcoursesummary)
            .Columns("coursename").HeaderText = "Course Name"

            .Columns("Students").HeaderText = "Students"
            .Columns("Students").Width = 80
            .Columns("Students").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns("Amount").HeaderText = "Amount"
            .Columns("Amount").Width = 100
            .Columns("Amount").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        resizeGridColumn(grdcoursesummary, 0)
        With grdcourse
            SetGridProperty(grdcourse)
            .Columns("Description").HeaderText = "Course Name"

            .Columns("Students").HeaderText = "Students"
            .Columns("Students").Width = 80
            .Columns("Students").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        End With
        resizeGridColumn(grdcourse, 0)
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        loadDueInstallments()
    End Sub

    Private Sub SchoolDXBDashBoardFrm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Timer1.Enabled = True
    End Sub

    Private Sub btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnload.Click
        loadDueInstallments()
    End Sub

    Private Sub rdopending_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdopending.Click, rdoboth.Click, rdopaid.Click
        loadDueInstallments()
    End Sub

    Private Sub btnrv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrv.Click
        If grdvoucher.RowCount = 0 Then Exit Sub
        If grdvoucher.CurrentRow Is Nothing Then Exit Sub
        fMainForm.LoadRV(0, grdvoucher.Item("studentname", grdvoucher.CurrentRow.Index).Value)
    End Sub


    Private Sub chkdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkdate.Click
        pldate.Enabled = chkdate.Checked
    End Sub

    Private Sub grdvoucher_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdvoucher.DoubleClick
        Dim studentid As Long = grdvoucher.Item("studentid", grdvoucher.CurrentRow.Index).Value
        fMainForm.loadStudentAdmissionDXB(studentid)
    End Sub

    Private Sub txtSeq_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeq.TextChanged
        grdvoucher.DataSource = SearchGrid(dttable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
        'rpttable = grdList.DataSource
    End Sub
End Class