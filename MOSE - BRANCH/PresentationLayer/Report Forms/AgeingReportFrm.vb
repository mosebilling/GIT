Public Class AgeingReportFrm
#Region "Class Objet declarations"
    Private _objcmnbLayer As New clsCommon_BL
    Private _objTr As New clsAccountTransaction
#End Region
#Region "Local Variables"
    Private dtTable As DataTable
    Private chgbyprg As Boolean
#End Region
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fRptFormat As RptFormatfrm
    Private Sub AgeingReportFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        cmbcategory.SelectedIndex = 0
        Timer2.Enabled = True
    End Sub
    Private Sub loadAgeing()
        dtTable = _objTr.returnAgeingReport(DateValue(cldrStartDate.Value), DateValue(cldrEnddate.Value), 0, _
                                           0, cmbcategory.Text, Val(txta1.Text), Val(txta2.Text), _
                                            Val(txta3.Text), Val(txta4.Text)).Tables(0)
        grdvoucher.DataSource = dtTable
        setGridHead()
    End Sub
    Private Sub setGridHead()
        With grdvoucher
            SetGridProperty(grdvoucher)
            .Columns("less30").HeaderText = "Less " & Val(txta1.Text)
            .Columns("less30").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("less30").Width = 150
            .Columns("less30").DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns("less60").HeaderText = Val(txta1.Text) & " - " & Val(txta2.Text) - 1
            .Columns("less60").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("less60").Width = 150
            .Columns("less60").DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns("less90").HeaderText = Val(txta2.Text) & " - " & Val(txta3.Text) - 1
            .Columns("less90").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("less90").Width = 150
            .Columns("less90").DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns("less120").HeaderText = Val(txta3.Text) & " - " & Val(txta4.Text) - 1
            .Columns("less120").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("less120").Width = 150
            .Columns("less120").DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns("above120").HeaderText = Val(txta4.Text) & " And Above "
            .Columns("above120").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("above120").Width = 150
            .Columns("above120").DefaultCellStyle.Format = "N" & NoOfDecimal
            Dim i As Integer
            For i = 6 To .Columns.Count - 1
                .Columns(i).Visible = False
            Next
            '.Columns("AccId").Visible = False
            Timer1.Enabled = True
        End With
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        resizeGridColumn(grdvoucher, 0)
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Timer2.Enabled = False
        loadAgeing()
    End Sub

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        loadAgeing()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub loadOutstanding(ByVal accid As Long)
        Dim dt As DataTable
        dt = _objTr.returnStatementReport(DateValue(cldrStartDate.Value), DateValue(cldrEnddate.Value), accid, 2, 0, cmbcategory.Text, 0).Tables(0)
        grdtr.DataSource = dt
        setGridTr()
    End Sub
    Private Sub setGridTr()
        With grdtr
            SetGridProperty(grdtr)
            .Columns("reference").HeaderText = "Reference"
            .Columns("reference").Width = 100

            .Columns("EntryRef").HeaderText = "Description"
            .Columns("trdate").HeaderText = "Inv Date"
            .Columns("reference").Width = 100

            .Columns("ddate").HeaderText = "Due Date"

            .Columns("debit").HeaderText = "Debit"
            .Columns("debit").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("debit").Width = 150
            .Columns("debit").DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns("credit").HeaderText = "Credit"
            .Columns("credit").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("credit").Width = 150
            .Columns("credit").DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns("bal").HeaderText = "Balance"
            .Columns("bal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("bal").Width = 150
            .Columns("bal").DefaultCellStyle.Format = "N" & NoOfDecimal


            .Columns("DueDate").Visible = False
            .Columns("Agdt").Visible = False
            .Columns("lnk").Visible = False
            .Columns("accid").Visible = False

            Dim i As Integer
            For i = 10 To .Columns.Count - 2
                .Columns(i).Visible = False
            Next
        End With
        resizeGridColumn(grdtr, 3)
    End Sub

    Private Sub grdvoucher_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdvoucher.RowEnter
        Dim accid As Long
        accid = grdvoucher.Item("accid", e.RowIndex).Value
        loadOutstanding(accid)
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Dim RptType As String = ""
        Dim RptCaption As String = ""
        RptType = "AGR"
        If chkFormat.Checked Then
            fRptFormat = New RptFormatfrm
            fRptFormat.RptType = RptType
            fRptFormat.ShowDialog()
            fRptFormat = Nothing
        Else
            Dim RptName As String
            RptName = getRptDefFlName(RptType, RptCaption)
            If RptName <> "" Then
                PrepareReport(RptName, RptCaption, False)
            End If
        End If
    End Sub
    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        PrepareReport(RptFlName, "", forPrint)
    End Sub
    Public Sub PrepareReport(ByVal FileName As String, ByVal RptCaption As String, ByVal forpint As Boolean)
        If Not frm Is Nothing Then
            frm.Close()
            frm = New ReprtviewNEWfrm
        Else
            frm = New ReprtviewNEWfrm
        End If
        Dim ds As DataSet
        ds = _objTr.returnAgeingReport(DateValue(cldrStartDate.Value), DateValue(cldrEnddate.Value), 0, _
                                           0, cmbcategory.Text, Val(txta1.Text), Val(txta2.Text), _
                                            Val(txta3.Text), Val(txta4.Text))
        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = fMainForm
        frm.Text = IIf(RptCaption = "", "Ageing Reports", RptCaption)
        frm.Show()

    End Sub
End Class