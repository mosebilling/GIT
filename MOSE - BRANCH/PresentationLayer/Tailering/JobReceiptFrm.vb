Public Class JobReceiptFrm
    Private _objcmnbLayer As New clsCommon_BL
    Private Sub JobReceiptFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loadDebits()
    End Sub
    Private Sub loadUPICard(ByVal isupi As Boolean)
        Dim dtTable As DataTable
        If isupi Then
            dtTable = _objcmnbLayer._fldDatatable("SELECT accid,AccDescr FROM AccMast WHERE AccSetId Like '%29%'")
        Else
            dtTable = _objcmnbLayer._fldDatatable("SELECT accid,AccDescr FROM AccMast WHERE AccSetId Like '%22%'")
        End If

        If dtTable.Rows.Count > 0 Then
            txtPCard.Tag = dtTable(0)("accid")
            txtPCard.Text = dtTable(0)("AccDescr")
        Else
            If isupi Then
                MsgBox("Set UPI Payment account in Settings", MsgBoxStyle.Exclamation)
            Else
                MsgBox("Set CARD Payment account in Settings", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub
    Private Sub loadDebits()
        Dim dtTable As DataTable
        Dim ds As DataSet
        Dim qry As String
        qry = "SELECT accid,AccDescr FROM AccMast WHERE AccSetId Like '%22%'"
        qry = qry & " SELECT accid,AccDescr FROM AccMast WHERE AccSetId Like '%11%'"
        ds = _objcmnbLayer._ldDataset(qry, False)
        dtTable = ds.Tables(0)
        If dtTable.Rows.Count > 0 Then
            txtPCard.Tag = dtTable(0)("accid")
            txtPCard.Text = dtTable(0)("AccDescr")
        End If
        dtTable = Nothing
        dtTable = ds.Tables(1)
        If dtTable.Rows.Count > 0 Then
            txtPcash.Tag = dtTable(0)("accid")
            txtPcash.Text = dtTable(0)("AccDescr")
        End If
    End Sub

    Private Sub btnupi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupi.Click
        loadUPICard(True)
    End Sub

    Private Sub btnsetcard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsetcard.Click
        loadUPICard(False)
    End Sub
End Class