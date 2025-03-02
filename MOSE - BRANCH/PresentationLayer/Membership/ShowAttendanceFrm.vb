Public Class ShowAttendanceFrm
    Private _objcmnbLayer As New clsCommon_BL
    Public invtrid As Long
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Public Sub loadAttendance()
        Dim _vtable As DataTable
        Dim qry As String
        qry = "select row_number() over(order by attendanceid asc) as Slno,AccDescr,jobcode,Description," & _
        "attdate,checkintime,checkouttime,AttendanceTb.remarks," & _
        "noofpersons,attendanceid,id from AttendanceTb " & _
        "left join AccMast on AccMast.AccId=AttendanceTb.customerid " & _
        "left join ItmInvTrTb on ItmInvTrTb.id=AttendanceTb.invdetId " & _
        "left join InvItm on ItmInvTrTb.ItemId=InvItm.ItemId " & _
        "left join jobtb on AttendanceTb.jobid=jobtb.jobid " & _
        "left join (select count(attendanceid)cntattendance,invdetId from " & _
        "AttendanceTb group by invdetId) attendance on ItmInvTrTb.id=attendance.invdetId " & _
        "where id=" & invtrid & " and attdate>='" & Format(DateValue(cldrdate.Value), "yyyy/MM/dd") & _
        "' and  attdate<='" & Format(DateValue(cldrEnddate.Value), "yyyy/MM/dd") & "'"
        _vtable = _objcmnbLayer._fldDatatable(qry)
        grdattendancelist.DataSource = _vtable
        With grdattendancelist
            If .ColumnCount = 0 Then Exit Sub
            SetGridProperty(grdattendancelist)
            .Columns("Slno").Width = 70
            .Columns("AccDescr").HeaderText = "Customer Name"
            .Columns("AccDescr").Visible = False
            .Columns("AccDescr").DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
            .Columns("jobcode").HeaderText = "Admission No"
            .Columns("jobcode").Visible = False
            .Columns("Description").HeaderText = "Package Name"
            .Columns("Description").Visible = False
            .Columns("checkintime").HeaderText = "Check IN"
            .Columns("checkintime").Width = 150
            .Columns("checkouttime").HeaderText = "Check Out"
            .Columns("checkouttime").Width = 150
            .Columns("remarks").HeaderText = "Remarks"
            .Columns("noofpersons").HeaderText = "Persons"
            .Columns("attdate").HeaderText = "Date"
            .Columns("attdate").Width = 100

            .Columns("attendanceid").Visible = False
            .Columns("id").Visible = False
            .Columns("noofpersons").Visible = False

        End With
    End Sub

    Private Sub ShowAttendanceFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub chkdate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkdate.CheckedChanged

    End Sub

    Private Sub chkdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkdate.Click
        If chkdate.Checked Then
            pldate.Enabled = True
        Else
            pldate.Enabled = False
        End If
    End Sub

    Private Sub btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnload.Click
        loadAttendance()
    End Sub
End Class