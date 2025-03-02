Public Class IncomeExpenseFrm
    Private _obreport As New clsReport_BL
    Private _objcmnbLayer As New clsCommon_BL
    Private dttable As DataTable
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fRptFormat As RptFormatfrm
    Private Sub IncomeExpenseFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'cldrstartdate.Value = DateValue("01/01/2020")
        Timer1.Enabled = True
    End Sub
    Private Sub ldincomeExpense()
        'create table columns
        dttable = _objcmnbLayer._fldDatatable("select '' AccDescrE,convert(money, 0) DealAmtE,'' AccDescrI,convert(money, 0) DealAmtI," & _
                                              "1 lnk,'' dtFrom,'' dtTo,0 accidE,'' GrpSetOnE,0 accidI,'' GrpSetOnI " & _
                                              "from companytb")
        Dim dtrow As DataRow
        grdvoucher.DataSource = dttable
        SetGridHead()
        Dim ds As DataSet
        Dim ttlExpense, ttlIncome As Double
        '
        ds = _obreport.returnTempleIncomeAndExpese(DateValue(cldrstartdate.Value), DateValue(cldateto.Value))
        Dim i As Integer

        For i = 0 To ds.Tables(0).Rows.Count - 1
            If i = 0 Then dttable.Rows.Clear()
            dtrow = dttable.NewRow
            dtrow("AccDescrE") = ds.Tables(0)(i)("AccDescr")
            dtrow("DealAmtE") = ds.Tables(0)(i)("DealAmt")
            dtrow("accidE") = ds.Tables(0)(i)("accid")
            dtrow("GrpSetOnE") = ds.Tables(0)(i)("GrpSetOn")
            dtrow("lnk") = 1
            dtrow("dtFrom") = Mid(DateValue(cldrstartdate.Value.Date), 1, 11)
            dtrow("dtTo") = Mid(DateValue(cldateto.Value), 1, 11)
            dttable.Rows.Add(dtrow)
            ttlExpense = ttlExpense + ds.Tables(0)(i)("DealAmt")
        Next
        For i = 0 To ds.Tables(1).Rows.Count - 1
            If dttable.Rows.Count = i Then
                dtrow = dttable.NewRow
                dtrow("AccDescrI") = ds.Tables(1)(i)("AccDescr")
                dtrow("DealAmtI") = ds.Tables(1)(i)("DealAmt")
                dtrow("accidI") = ds.Tables(1)(i)("accid")
                dtrow("GrpSetOnI") = ds.Tables(1)(i)("GrpSetOn")
                dtrow("lnk") = 1
                dtrow("dtFrom") = Mid(DateValue(cldrstartdate.Value.Date), 1, 11)
                dtrow("dtTo") = Mid(DateValue(cldateto.Value), 1, 11)
                dttable.Rows.Add(dtrow)
            Else
                dttable(i)("AccDescrI") = ds.Tables(1)(i)("AccDescr")
                dttable(i)("DealAmtI") = ds.Tables(1)(i)("DealAmt")
                dttable(i)("accidI") = ds.Tables(1)(i)("accid")
                dttable(i)("GrpSetOnI") = ds.Tables(1)(i)("GrpSetOn")
            End If
            ttlIncome = ttlIncome + ds.Tables(1)(i)("DealAmt")
        Next
        If dttable.Rows.Count > 0 Then grdvoucher.DataSource = dttable


        lblbalance.Text = Format(ttlIncome - ttlExpense, numFormat)
        lbletotal.Text = Format(ttlExpense, numFormat)
        lblincome1.Text = Format(ttlIncome, numFormat)
        SetGridHead()
    End Sub
    Private Sub SetGridHead()
        Try
            With grdvoucher
                .ColumnHeadersVisible = True
                .RowHeadersVisible = False
                .AllowUserToAddRows = False
                .AllowUserToDeleteRows = False
                .AllowUserToResizeRows = False
                .AllowUserToResizeColumns = True
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .ReadOnly = True
                .EditMode = DataGridViewEditMode.EditProgrammatically
                .ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold)
                .ColumnHeadersDefaultCellStyle.ForeColor = Color.White
                .RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 11.0!, FontStyle.Regular)
                '.AllowUserToResizeColumns = False
                '.ColumnHeadersVisible = False
                .Columns("accidI").Visible = False
                .Columns("GrpSetOnI").Visible = False
                .Columns("accidE").Visible = False
                .Columns("GrpSetOnE").Visible = False
                .Columns("lnk").Visible = False
                .Columns("dtFrom").Visible = False
                .Columns("dtTo").Visible = False



                .Columns("DealAmtI").Width = 150
                .Columns("DealAmtI").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("DealAmtI").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("DealAmtI").SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns("DealAmtI").HeaderText = ""

                .Columns("DealAmtE").Width = 150
                .Columns("DealAmtE").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("DealAmtE").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("DealAmtE").SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns("DealAmtE").HeaderText = "Amt"
                '.Columns("DealAmtE").DefaultCellStyle.ForeColor = Color.White


                .Columns("AccDescrE").HeaderText = ""
                .Columns("AccDescrE").SortMode = DataGridViewColumnSortMode.NotSortable

                .Columns("AccDescrI").SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns("AccDescrI").HeaderText = ""
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        'resizecolumn()
    End Sub
    Private Sub resizecolumn()
        Dim colwidth As Integer = 300
        With grdvoucher
            If .Columns.Count = 0 Then Exit Sub
            colwidth = .Width - colwidth - 40
            .Columns("AccDescrE").Width = colwidth / 2
            .Columns("AccDescrI").Width = colwidth / 2
        End With
    End Sub
    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        ldincomeExpense()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub grdvoucher_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles grdvoucher.Paint
        'Offsets to adjust the position of the merged Header.
        If grdvoucher.Columns.Count = 0 Or grdvoucher.Rows.Count = 0 Then Exit Sub
        Dim heightOffset As Integer = -1
        Dim widthOffset As Integer = -2
        Dim xOffset As Integer = 0
        Dim yOffset As Integer = 0
        'Index of Header column from where the merging will start.

        Dim headerCellRectangle As Rectangle
        Dim xCord As Integer
        Dim yCord As Integer
        Dim mergedHeaderWidth As Integer
        Dim mergedHeaderRect As Rectangle
        Dim hdText As String
        Dim txtLocation As Integer
        'Number of Header columns to be merged.
        Dim columnCount As Integer = 2
        Dim columnIndex As Integer = 0
        With grdvoucher
            headerCellRectangle = .GetCellDisplayRectangle(columnIndex, 0, True)
            'X coordinate of the merged Header Column.
            xCord = (headerCellRectangle.Location.X + xOffset)
            'Y coordinate of the merged Header Column.
            yCord = ((headerCellRectangle.Location.Y - headerCellRectangle.Height) + yOffset)
            'Calculate Width of merged Header Column by adding the widths of all Columns to be merged.
            mergedHeaderWidth = (.Columns(columnIndex).Width + (.Columns((columnIndex + (columnCount - 1))).Width + widthOffset))

            'Generate the merged Header Column Rectangle.
            mergedHeaderRect = New Rectangle(xCord, yCord, mergedHeaderWidth, (headerCellRectangle.Height + heightOffset))

            'Draw the merged Header Column Rectangle.
            e.Graphics.FillRectangle(New SolidBrush(Color.White), mergedHeaderRect)
            'Draw the merged Header Column Text.
            hdText = "EXPENSE"
            txtLocation = (mergedHeaderWidth / 2) - (Len(hdText) / 2)
            e.Graphics.DrawString(hdText, .ColumnHeadersDefaultCellStyle.Font, Brushes.Black, (xCord + 2) + txtLocation - 50, (yCord + 3))
        End With
        columnIndex = 2
        With grdvoucher
            headerCellRectangle = .GetCellDisplayRectangle(columnIndex, 0, True)
            'X coordinate of the merged Header Column.
            xCord = (headerCellRectangle.Location.X + xOffset)
            'Y coordinate of the merged Header Column.
            yCord = ((headerCellRectangle.Location.Y - headerCellRectangle.Height) + yOffset)
            'Calculate Width of merged Header Column by adding the widths of all Columns to be merged.
            mergedHeaderWidth = (.Columns(columnIndex).Width + (.Columns((columnIndex + (columnCount - 1))).Width + widthOffset))

            'Generate the merged Header Column Rectangle.
            mergedHeaderRect = New Rectangle(xCord, yCord, mergedHeaderWidth, (headerCellRectangle.Height + heightOffset))

            'Draw the merged Header Column Rectangle.
            e.Graphics.FillRectangle(New SolidBrush(Color.White), mergedHeaderRect)
            'Draw the merged Header Column Text.
            hdText = "INCOME"
            txtLocation = (mergedHeaderWidth / 2) - (Len(hdText) / 2)
            e.Graphics.DrawString(hdText, .ColumnHeadersDefaultCellStyle.Font, Brushes.Black, (xCord + 2) + txtLocation - 50, (yCord + 3))
        End With

    End Sub

    Private Sub IncomeExpenseFrm_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        resizecolumn()
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Dim RptType As String = ""
        Dim RptCaption As String = ""
        RptType = "INEX"
        fRptFormat = New RptFormatfrm
        fRptFormat.RptType = RptType
        fRptFormat.ShowDialog()
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
        Dim dtRpt = returnToReportTable(dttable)
        ds.Tables.Add(dtRpt)
        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = fMainForm
        frm.Text = RptCaption
        frm.Show()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        ldincomeExpense()
        resizecolumn()
    End Sub
End Class