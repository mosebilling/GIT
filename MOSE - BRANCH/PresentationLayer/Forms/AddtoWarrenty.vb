Public Class AddtoWarrenty
#Region "Class Objects"
    Private _objInv As clsInvoice
    Private _objcmnbLayer As clsCommon_BL
#End Region
    Private Sub AddtoWarrenty_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtpExpiryDate.Value = DateAdd(DateInterval.Year, 1, DateValue(cldrdate.Value))
    End Sub
    Public Sub ldWarrentyTransactionDetails()
        _objInv = New clsInvoice
        Dim dt As DataTable
        If cmbwarrenty.Items.Count = 0 Then ldWarrenty()
        With _objInv
            .SerialNo = lblserialno.Text
            dt = .returnWarrentyTransaction
        End With
        If dt.Rows.Count > 0 Then
            lblserialno.Tag = dt(0)("WTrid")
            txtSuppName.Text = dt(0)("Cust")
            txtSuppName.Tag = dt(0)("Custid")
            numVchrNo.Text = dt(0)("BillNo")
            cmbwarrenty.Text = Trim(dt(0)("WarrentyName") & "")
            If Not IsDBNull(dt(0)("SaleDate")) Then
                cldrdate.Value = dt(0)("SaleDate")
            Else
                cldrdate.Value = Date.Now
            End If
            If Not IsDBNull(dt(0)("ExpDate")) Then
                dtpExpiryDate.Value = dt(0)("ExpDate")
            Else
                dtpExpiryDate.Value = Date.Now
            End If

        End If
    End Sub
    Private Sub saveWarrentyTransaction()
        Try
            _objInv = New clsInvoice
            With _objInv
                .WTrid = Val(lblserialno.Tag)
                .ExpDate = DateValue(dtpExpiryDate.Value)
                .Cust = txtSuppName.Text
                .TrDate = DateValue(cldrdate.Value)
                .BillNo = numVchrNo.Text
                .SerialNo = lblserialno.Text
                .WarrentyName = cmbwarrenty.Text
                .CSCode = Val(txtSuppName.Tag)
                .saveWarrenty()
                MsgBox("Warrenty Saved", MsgBoxStyle.Information)
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        saveWarrentyTransaction()
        Me.Close()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub cldrdate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cldrdate.KeyDown
        If e.KeyCode = Keys.Enter Then txtSuppName.Focus()
    End Sub

    Private Sub cldrdate_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles cldrdate.Validated
        dtpExpiryDate.Value = DateAdd(DateInterval.Year, 1, DateValue(cldrdate.Value))
    End Sub

    Private Sub numVchrNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles numVchrNo.KeyDown
        If e.KeyCode = Keys.Enter Then cldrdate.Focus()
    End Sub

    Private Sub numVchrNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numVchrNo.TextChanged

    End Sub

    Private Sub cldrdate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cldrdate.ValueChanged

    End Sub

    Private Sub txtSuppName_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSuppName.KeyDown
        If e.KeyCode = Keys.Enter Then dtpExpiryDate.Focus()
    End Sub

    Private Sub txtSuppName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSuppName.TextChanged

    End Sub

    Private Sub dtpExpiryDate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtpExpiryDate.KeyDown
        If e.KeyCode = Keys.Enter Then btnupdate.Focus()
    End Sub
    Private Sub ldWarrenty()
        Dim dt As DataTable
        _objcmnbLayer = New clsCommon_BL
        dt = _objcmnbLayer._fldDatatable("SELECT WarrentyName FROM WarrentyMasterTb")
        Dim i As Integer
        cmbwarrenty.Items.Clear()
        For i = 0 To dt.Rows.Count - 1
            cmbwarrenty.Items.Add(dt(i)(0))
        Next
        If dt.Rows.Count > 0 Then cmbwarrenty.SelectedIndex = 0
    End Sub
End Class