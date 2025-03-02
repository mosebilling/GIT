Public Class eventLogFrm
    Private objcmnlayer As New clsCommon_BL
    Private dtTable As DataTable
    Private _rptTable As DataTable
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fRptFormat As RptFormatfrm
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub eventLogFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loadMasters()
        loadEvents()
        Timer1.Enabled = True
    End Sub
    Private Sub loadMasters()
        Dim dt As DataTable
        Dim ds As DataSet
        ds = objcmnlayer._ldDataset("select module from EventLogTb group by module select eventuser from EventLogTb group by eventuser", False)
        Dim i As Integer
        cmbmodule.Items.Clear()
        cmbmodule.Items.Add("")
        dt = ds.Tables(0)
        For i = 0 To dt.Rows.Count - 1
            cmbmodule.Items.Add(dt(i)("module"))
        Next
        dt = ds.Tables(1)
        cmbuser.Items.Clear()
        cmbuser.Items.Add("")
        For i = 0 To dt.Rows.Count - 1
            cmbuser.Items.Add(dt(i)("eventuser"))
        Next
    End Sub
    Private Sub loadEvents()

        dtTable = objcmnlayer.returnEventLog(DateValue(cldrStartDate.Value), DateValue(cldrEnddate.Value), 0, cmbmodule.Text, cmbuser.Text, Mid(cmbevent.Text, 1, 1))
        grdvoucher.DataSource = dtTable
        setgridhead()
        Dim i As Integer
        For i = 0 To grdvoucher.ColumnCount - 1
            cmbSearch.Items.Add(grdvoucher.Columns(i).HeaderText)
        Next
        If cmbSearch.Items.Count > 1 Then cmbSearch.SelectedIndex = 0
        txtSearch.Focus()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        resizeGridColumn(grdvoucher, 4)
    End Sub

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        loadEvents()
       
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Dim dt As DataTable = SearchGrid(dtTable, Trim(txtSearch.Text), cmbSearch.SelectedIndex, Not chkSearch.Checked)
        grdvoucher.DataSource = dt
        _rptTable = dt
        SetGridHead()
    End Sub
    Private Sub setgridhead()
        With grdvoucher
            SetGridProperty(grdvoucher)
            .Columns(0).Width = 150
            .Columns(0).HeaderText = "Module"
            .Columns(1).Width = 150
            .Columns(1).HeaderText = "User"
            .Columns(2).HeaderText = "Date & Time"
            .Columns(2).Width = 150
            .Columns(3).HeaderText = "Event Name"
            .Columns(3).Width = 150
            .Columns(4).Width = 50
            .Columns(4).HeaderText = "Description"
            .Columns(5).Visible = False
            .Columns("lnk").Visible = False
            .Columns("datefrom").Visible = False
            .Columns("dateto").Visible = False
        End With
        
        resizeGridColumn(grdvoucher, 4)
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Dim RptType As String = ""
        Dim RptCaption As String = ""
        RptType = "ELOG"
        If chkFormat.Checked Then
            fRptFormat = New RptFormatfrm
            fRptFormat.RptType = RptType
            fRptFormat.ShowDialog()
            fRptFormat = Nothing
        Else
            Dim RptName As String
            RptName = getRptDefFlName(RptType, RptCaption)
            If RptName <> "" Then
                PrepareReport(RptName, "EVENT LOG", False)
            End If
        End If
    End Sub

    Private Sub fRptFormat_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fRptFormat.FormClosed
        fRptFormat = Nothing
    End Sub
    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        PrepareReport(RptFlName, RptCaption, forPrint)
    End Sub

    Private Sub frm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles frm.FormClosed
        frm = Nothing
    End Sub
    Public Sub PrepareReport(ByVal FileName As String, ByVal RptCaption As String, ByVal forpint As Boolean)
        If Not frm Is Nothing Then
            frm.Close()
            frm = New ReprtviewNEWfrm
        Else
            frm = New ReprtviewNEWfrm
        End If
        Dim ds As New DataSet
        If Not _rptTable Is Nothing Then
            ds.Tables.Add(_rptTable)
        Else
            Dim dtRpt = returnToReportTable(dtTable)
            ds.Tables.Add(dtRpt)
        End If
        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = fMainForm
        frm.Text = RptCaption
        frm.Show()
    End Sub
End Class