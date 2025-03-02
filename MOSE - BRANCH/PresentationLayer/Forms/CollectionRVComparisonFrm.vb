Public Class CollectionRVComparisonFrm
    Private _objTr As New clsAccountTransaction
    Dim _vdatatable As DataTable
    Public AccountNo As Long
    Public reference As String
    Dim RptdtTable As DataTable
    Public dateFrom As Date

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnload.Click
        ldGrid(12)
    End Sub
    Public Sub ldGrid(ByVal ptype As Integer)
        Dim dt As DataTable
        Dim dt1 As DataTable
        Dim ds As DataSet

        With _objTr
            .ptype = ptype
            .DateFrom = DateValue(dateFrom)
            .DateTo = DateValue(Date.Now)
            .AccountNo = AccountNo
            .JVType = "RV"
            .Reference = reference
            ds = .returnPaymentDetails
            dt = ds.Tables(0)
            dt1 = ds.Tables(1)
        End With
        dvData.DataSource = dt
        dgvCollection.DataSource = dt1
        SetmodiGrid()
        lblcount.Text = "Count : " & dt.Rows.Count
        lblcollectioncount.Text = "Count : " & dt1.Rows.Count
        lblcollection.Text = Format(0, numFormat)
        lbltotal.Text = Format(0, numFormat)

        Dim amt As String
        Dim dblAmount As Double
        amt = Trim(dt.Compute("SUM(amount)", "") & "")
        If Val(amt) > 0 Then
            dblAmount = Convert.ToDouble(amt)
            lbltotal.Text = Format(dblAmount, numFormat)
        End If
        amt = ""
        dblAmount = 0
        amt = Trim(dt1.Compute("SUM(amount)", "") & "")
        If Val(amt) > 0 Then
            dblAmount = Convert.ToDouble(amt)
            lblcollection.Text = Format(dblAmount, numFormat)
        End If
    End Sub
    Sub SetmodiGrid()
        With dvData
            SetGridProperty(dvData)
            
            .Columns("DateFrom").Visible = False
            .Columns("DateTo").Visible = False
            .Columns("Lnk").Visible = False
            .Columns("AccountNo").Visible = False
            .Columns("AccDescr").HeaderText = "Account Name"
            '.Columns("AccDescr").Visible = False
            '.Columns("Linkno").Visible = False

            .Columns("inv no").Width = 75
            .Columns("inv no").HeaderText = "Invoice"

            .Columns("tr date").Width = 75
            .Columns("tr date").HeaderText = "Date"

            .Columns("amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("amount").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("amount").Width = 100
            .Columns("amount").HeaderText = "Amount"

            .Columns("description").Visible = False
            .Columns("linkno").Visible = False
            .Columns("reference").Visible = False

            .Columns("chqno").Visible = False
            .Columns("chqdate").Visible = False

        End With
        With dgvCollection
            SetGridProperty(dgvCollection)
            .Columns("RvNo").Width = 50
            .Columns("rvdate").Width = 75
            .Columns("rvdate").HeaderText = "Date"

            .Columns("referenceNo").Width = 75
            .Columns("referenceNo").HeaderText = "Invoice"

            .Columns("AccDescr").Width = 150
            .Columns("AccDescr").HeaderText = "Account Name"

            .Columns("amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("amount").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("amount").Width = 100
            .Columns("amount").HeaderText = "Amount"

            resizeGridColumn(dgvCollection, 3)
            resizeGridColumn(dvData, 2)
        End With
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        resizeGridColumn(dgvCollection, 3)
        resizeGridColumn(dvData, 2)
    End Sub

    Private Sub CollectionRVComparisonFrm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Timer1.Enabled = True
    End Sub

    Private Sub dvData_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dvData.CellContentClick

    End Sub
End Class