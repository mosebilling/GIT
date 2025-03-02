Public Class WeeklyCollectionFrm
    Private WithEvents fwait As WaitMessageFrm
    Private _objcmnbLayer As New clsCommon_BL
    Private dttable As DataTable
    Private rpttable As DataTable
    Private chgbyprg As Boolean
    Private wdate, wdate1, wdate2, wdate3, wdate4, wdate5, wdate6, wdate7 As Date
    Private WithEvents frm As New ReprtviewNEWfrm
    Private _objcommonlayer As New clsCommon_BL
    Private RptType As String
    Private WithEvents fRptFormat As RptFormatfrm
    Private Sub WeeklyCollectionFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        createDatatable()
    End Sub

    Private Sub fwait_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fwait.FormClosed
        fwait = Nothing
    End Sub
    Private Sub fwait_triggerEvent() Handles fwait.triggerEvent
        Select Case fwait.triggerType
            Case 1
                'fillGrid(opt)
                loadWeeklyCollection()
        End Select
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
    Private Sub loadWeeklyCollection()
        Dim dt As DataTable
        If dttable.Rows.Count > 0 Then dttable.Rows.Clear()
        wdate = DateValue(cldrStartDate.Value)
        wdate1 = DateValue(cldrStartDate.Value)
        wdate2 = DateAdd(DateInterval.Day, 1, wdate1)
        wdate3 = DateAdd(DateInterval.Day, 1, wdate2)
        wdate4 = DateAdd(DateInterval.Day, 1, wdate3)
        wdate5 = DateAdd(DateInterval.Day, 1, wdate4)
        wdate6 = DateAdd(DateInterval.Day, 1, wdate5)
        wdate7 = DateAdd(DateInterval.Day, 1, wdate6)
        'Dim str As String = "select AccDescr,isnull(amt,0)amt,isnull(JVDate,'" & Format(DateValue(wdate1), "yyyy/MM/dd") & "')JVDate,Reference,convert(varchar,accid)+'/'+Reference Referencegroup from " & _
        '                    "AccMast left join S1AccHd on AccMast.S1AccId=S1AccHd.S1AccId " & _
        '                    "left join (select DealAmt*-1 amt,JVDate,Reference,accountno from AccTrCmn " & _
        '                    "left join AccTrDet on AccTrCmn.LinkNo=AccTrDet.LinkNo " & _
        '                    "where isnull(approved,0)=0 and JVType='RV' AND JVDate>='" & Format(DateValue(wdate1), "yyyy/MM/dd") & _
        '                    "' AND JVDate<='" & Format(DateValue(wdate6), "yyyy/MM/dd") & "')tr " & _
        '                    "on AccMast.accid=tr.accountno " & _
        '                    "left join (select sum(dealamt)otamt, accountno from acctrdet left join AccTrCmn on AccTrCmn.LinkNo=AccTrDet.LinkNo " & _
        '                    "where JVDate<'" & Format(DateValue(wdate1), "yyyy/MM/dd") & "' group by accountno)ot on AccMast.accid=ot.accountno " & _
        '                    "where GrpSetOn='customer' and isnull(otamt,0)>0"
        Dim str As String = "select AccDescr,isnull(amt,0)amt,isnull(JVDate,'" & Format(DateValue(wdate1), "yyyy/MM/dd") & "')JVDate,Reference,convert(varchar,accid)+'/'+Reference Referencegroup from " & _
                            "AccMast left join S1AccHd on AccMast.S1AccId=S1AccHd.S1AccId " & _
                            "left join (select DealAmt*-1 amt,JVDate,Reference,accountno from AccTrCmn " & _
                            "left join AccTrDet on AccTrCmn.LinkNo=AccTrDet.LinkNo " & _
                            "where isnull(approved,0)=0 and JVType='RV' AND JVDate>='" & Format(DateValue(wdate1), "yyyy/MM/dd") & _
                            "' AND JVDate<='" & Format(DateValue(wdate6), "yyyy/MM/dd") & "')tr " & _
                            "on AccMast.accid=tr.accountno " & _
                            "inner join (select accountno from ItmInvCmnTb " & _
                            "left join (select sum(dealamt) invbal,Reference,accountno from AccTrDet inner join AccTrCmn on acctrdet.LinkNo=AccTrCmn.LinkNo " & _
                            "where isnull(approved,0)=0 and  JVDate<'" & Format(DateValue(wdate1), "yyyy/MM/dd") & "' group by Reference,accountno)acctr " & _
                            "on ItmInvCmnTb.trrefno=acctr.reference and ItmInvCmnTb.cscode=acctr.accountno where isnull(invbal,0)>0 and isnull(ishideFromRVCollection,0)=0 " & _
                            "and trdate<='" & Format(DateValue(wdate1), "yyyy/MM/dd") & "' group by accountno)ot on AccMast.accid=ot.accountno where GrpSetOn='customer'"
        str = "select AccDescr,sum(amt)amt,JVDate from( " & str & ")tr group by AccDescr,JVDate"
        dt = _objcmnbLayer._fldDatatable(str)
        Dim drow As DataRow

        
        Dim i As Integer
        Dim dtweekly As DataTable
        Dim accname As String
        Dim reference As String
        Dim Referencegroup As String
fetchagain:
        Dim _qurey As EnumerableRowCollection(Of DataRow)
        If dt.Rows.Count > 0 Then
            accname = dt(0)("AccDescr")
            'reference = dt(0)("reference")
            'Referencegroup = dt(0)("Referencegroup")
            drow = dttable.NewRow
            wdate = wdate1
            For i = 1 To 6
                '_qurey = From data In dt.AsEnumerable() Where data("AccDescr") = accname _
                '         And data("reference") = reference And DateValue(data("JVDate")) = DateValue(wdate) Select data
                _qurey = From data In dt.AsEnumerable() Where data("AccDescr") = accname _
                         And DateValue(data("JVDate")) = DateValue(wdate) Select data
                If _qurey.Count > 0 Then
                    dtweekly = _qurey.CopyToDataTable()
                Else
                    dtweekly = dt.Clone
                End If
                If dtweekly.Rows.Count > 0 Then
                    drow("d" & i) = dtweekly(0)("amt")
                Else
                    drow("d" & i) = 0
                End If
                wdate = DateAdd(DateInterval.Day, 1, wdate)
            Next
            drow("accname") = accname
            'drow("reference") = reference
            drow("accid") = 0
            drow("lnk") = 1
            dttable.Rows.Add(drow)
            _qurey = From data In dt.AsEnumerable() Where data("AccDescr") <> accname Select data
            If _qurey.Count > 0 Then
                dt = _qurey.CopyToDataTable()
            Else
                dt = dt.Clone
            End If
            If dt.Rows.Count > 0 Then
                GoTo fetchagain
            End If
        End If
        grdvoucher.DataSource = dttable

        SetGridHead()
    End Sub
    Private Sub createDatatable()
        dttable = New DataTable
        With dttable
            .Columns.Add(New DataColumn("accname", GetType(String)))
            .Columns.Add(New DataColumn("reference", GetType(String)))
            .Columns.Add(New DataColumn("d1", GetType(Double)))
            .Columns.Add(New DataColumn("d2", GetType(Double)))
            .Columns.Add(New DataColumn("d3", GetType(Double)))
            .Columns.Add(New DataColumn("d4", GetType(Double)))
            .Columns.Add(New DataColumn("d5", GetType(Double)))
            .Columns.Add(New DataColumn("d6", GetType(Double)))
            .Columns.Add(New DataColumn("d7", GetType(Double)))
            .Columns.Add(New DataColumn("accid", GetType(Long)))
            .Columns.Add(New DataColumn("lnk", GetType(Integer)))

        End With
        dttable.Rows.Clear()
    End Sub
    Private Sub SetGridHead()
        chgbyprg = True
        With grdvoucher
            SetGridProperty(grdvoucher)
            .ColumnHeadersHeight = 100
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersDefaultCellStyle.BackColor = Color.Olive
            .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.5!, System.Drawing.FontStyle.Regular)
            .RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.0!)

            .Columns("accname").HeaderText = "Customer"
            .Columns("accname").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("accname").ReadOnly = True
            .Columns("accname").Width = 150

            .Columns("reference").HeaderText = "Reference"
            .Columns("reference").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("reference").Visible = False

            .Columns("d1").HeaderText = wdate1
            .Columns("d1").Width = 100
            .Columns("d1").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("d1").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("d1").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


            .Columns("d2").HeaderText = wdate2
            .Columns("d2").Width = 100
            .Columns("d2").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("d2").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("d2").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


            .Columns("d3").HeaderText = wdate3
            .Columns("d3").Width = 100
            .Columns("d3").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("d3").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("d3").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns("d4").HeaderText = wdate4
            .Columns("d4").Width = 100
            .Columns("d4").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("d4").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("d4").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


            .Columns("d5").HeaderText = wdate5
            .Columns("d5").Width = 100
            .Columns("d5").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("d5").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("d5").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


            .Columns("d6").HeaderText = wdate6
            .Columns("d6").Width = 100
            .Columns("d6").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("d6").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("d6").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns("d7").HeaderText = wdate7
            .Columns("d7").Width = 100
            .Columns("d7").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("d7").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("d7").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("d7").Visible = False

            .Columns("accid").Visible = False
            .Columns("lnk").Visible = False

        End With
        resizeGridColumn(grdvoucher, 0)

        chgbyprg = False
        Dim i As Integer = 0
        For i = 0 To grdvoucher.ColumnCount - 2
            cmbOrder.Items.Add(grdvoucher.Columns(i).HeaderText)
        Next
        If cmbOrder.Items.Count > 0 Then cmbOrder.SelectedIndex = 0
    End Sub
    Private Sub txtSeq_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeq.TextChanged

        grdvoucher.DataSource = SearchGrid(dttable, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
        rpttable = grdvoucher.DataSource


    End Sub
    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        loadWaite(1)
    End Sub

    Private Sub PrepareRpt(ByVal RptType As String, Optional ByVal Forprint As Boolean = False, Optional ByVal isF As Boolean = False)
        Dim RptName As String
        Dim RptCaption As String = ""
        RptName = getRptDefFlName(RptType, RptCaption)
        If Trim(RptName) <> "" Then
            LoadReport(RptName, RptCaption, Forprint)
        End If
    End Sub
    Private Sub LoadReport(ByVal FileName As String, ByVal RptCaption As String, Optional ByVal ToPrint As Boolean = False)
        If Not frm Is Nothing Then
            frm.Close()
            frm = New ReprtviewNEWfrm
        Else
            frm = New ReprtviewNEWfrm
        End If
        Dim ds As New DataSet
        Dim str As String
        Dim dt As DataTable
        If rpttable Is Nothing Then
            Dim _qurey As EnumerableRowCollection(Of DataRow)
            'linq
            _qurey = From data In dttable.AsEnumerable() Select data
            If _qurey.Count > 0 Then
                dt = _qurey.CopyToDataTable()
            Else
                dt = dttable.Clone
            End If
            ds.Tables.Add(dt)
        Else
            ds.Tables.Add(rpttable)
        End If

        Dim parmDt As DataTable
        str = " select 1 Lnk,'" & Format(DateValue(cldrStartDate.Value), "dd/MM/yyyy") & "' As FDate, '" & Format(DateValue(cldrEnddate.Value), "dd/MM/yyyy") & "' As Sdate,'" & _
                                              Format(DateValue(wdate1), "yyyy/MM/dd") & "' wdate1,'" & _
                                              Format(DateValue(wdate2), "yyyy/MM/dd") & "' wdate2,'" & _
                                              Format(DateValue(wdate3), "yyyy/MM/dd") & "' wdate3,'" & _
                                              Format(DateValue(wdate4), "yyyy/MM/dd") & "' wdate4,'" & _
                                              Format(DateValue(wdate5), "yyyy/MM/dd") & "' wdate5,'" & _
                                              Format(DateValue(wdate6), "yyyy/MM/dd") & "' wdate6,'" & _
                                              Format(DateValue(wdate7), "yyyy/MM/dd") & "' wdate7  From CompanyTb"
        parmDt = _objcmnbLayer._fldDatatable(str)
        
        ds.Tables.Add(parmDt)
        frm.SetReport(ds, FileName, 0, False, , ToPrint)
        frm.MdiParent = Me.MdiParent
        frm.Text = RptCaption
        If Not ToPrint Then frm.Show()

    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        RptType = "WCR"
        If RptType = "" Then Exit Sub
        If chkFormat.Checked Then
            fRptFormat = New RptFormatfrm
            fRptFormat.RptType = RptType
            fRptFormat.ShowDialog()
            fRptFormat = Nothing
        Else
            PrepareRpt(RptType)
        End If
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub chkSearch_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSearch.CheckedChanged

    End Sub
End Class