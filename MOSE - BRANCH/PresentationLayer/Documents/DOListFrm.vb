Public Class DOListFrm
    Dim dtTable As DataTable
    Private _objcmnbLayer As New clsCommon_BL
    Public Event transferItems(ByVal docids As String, ByVal isallitem As Boolean)
    Private Sub btnfind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnfind.Click
        loadDocs()
    End Sub
    Public Sub loadDocs()
        Dim strquery As String
        Dim condition As String
        If Val(txtSuppName.Tag) > 0 Then
            condition = " AND CSCode=" & Val(txtSuppName.Tag)
        Else
            condition = ""
        End If
        If chkdate.Checked Then
            condition = condition & " AND DDate>='" & Format(DateValue(dtpdatefrom.Value), "yyyy/MM/dd") & " AND DDate<='" & Format(DateValue(dtpdateto.Value), "yyyy/MM/dd")
        End If
        strquery = "SELECT '' Tag,* FROM (SELECT DNo, DDate,AccDescr [Customer Name], TAmt [Amount],ISNULL(PIAmt,0)+ISNULL(PDAmt,0) [Processed]," & _
                "case when TDQty - (isNull(TPQtyInv,0)+ isnull(TPQtyDoc,0))> 0 then 'Balance' else 'Yes' end Ordered, Reference, Comment,DocCmnTb.Docid,CSCode FROM DocCmnTb " & _
                "LEFT JOIN ACCMAST ON DocCmnTb.CSCode=AccMast.accid " & _
                "LEFT JOIN (SELECT DocId, Sum(CostPUnit * Qty) TAmt, Sum(Qty) As TDQty FROM DocTranTb GROUP BY DocId)As QAmt ON DocCmnTb.DocId = QAmt.DocId " & _
                "LEFT JOIN (SELECT impDocid, Sum(TrQty) As TPQtyInv,Sum(UnitCost * TrQty) PIAmt FROM ItmInvTrTb  GROUP BY impDocid) As PIQ ON PIQ.impDocid = DocCmnTb.DocId " & _
                "LEFT JOIN (SELECT impDocid, Sum(Qty) As TPQtyDoc,Sum(CostPUnit * Qty) As PDAmt FROM DocTranTb  GROUP BY impDocid) As PIQD ON PIQD.impDocid = DocCmnTb.DocId " & _
                "WHERE " & IIf(UsrBr = "", "", " Brid='" & UsrBr & "' and ") & "  DocType = '" & lbldoc.Text & "') Tr WHERE Ordered = 'Balance' " & condition & " order by DDate,[Customer Name]"
        dtTable = _objcmnbLayer._fldDatatable(strquery)
        grdSrch.DataSource = dtTable
        setGridHead()
    End Sub
    Private Sub setGridHead()
        Dim i As Integer
        With grdSrch
            SetGridProperty(grdSrch)
            .Columns("Tag").Width = 32
            .Columns("Tag").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("DNo").Width = 75
            .Columns("DDate").Width = 75
            .Columns("Ordered").Width = 75
            .Columns("Customer Name").Width = 200
            .Columns("Amount").DefaultCellStyle.Format = "N" & 2
            .Columns("Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            'resizeGridColumn(grdSrch, 2)
            .Columns("Amount").Width = 90
            .Columns("Processed").DefaultCellStyle.Format = "N" & 2
            .Columns("Processed").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            'resizeGridColumn(grdSrch, 2)
            .Columns("Processed").Width = 90
            .Columns("Docid").Visible = False
            .Columns("CSCode").Visible = False
            cmbcolms.Items.Clear()
            For i = 0 To .Columns.Count - 1
                cmbcolms.Items.Add(.Columns(i).HeaderText)
            Next
            cmbcolms.SelectedIndex = 1
        End With
    End Sub

    Private Sub grdSrch_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSrch.CellClick
        If e.ColumnIndex = 0 Then
            With grdSrch
                .Item(0, e.RowIndex).Value = IIf(.Item(0, e.RowIndex).Value = "Y", "", "Y")
            End With
        End If
    End Sub

    Private Sub btnexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexit.Click
        Me.Close()
    End Sub

    Private Sub txtSeq_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeq.TextChanged
        grdSrch.DataSource = SearchGrid(dtTable, txtSeq.Text, cmbcolms.SelectedIndex)
        setGridHead()
    End Sub

    Private Sub cmbcolms_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbcolms.SelectedIndexChanged
        txtSeq.Focus()
    End Sub

    Private Sub btntransfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btntransfer.Click
        Dim ids As String = ""
        Dim i As Integer
        With grdSrch
            For i = 0 To .Rows.Count - 1
                If .Item(0, i).Value = "Y" Then
                    ids = ids & IIf(ids = "", "", ",") & .Item("Docid", i).Value
                End If
            Next
        End With
        If ids = "" Then
            MsgBox("Select Document to transfer", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        RaiseEvent transferItems(ids, chkallitems.Checked)
        Me.Close()
    End Sub

    Private Sub grdSrch_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSrch.DoubleClick
        Dim ids As String = ""
        Dim i As Integer
        If Not grdSrch.CurrentRow.Index = Nothing Then
            i = grdSrch.CurrentRow.Index
        End If
        ids = grdSrch.Item("Docid", i).Value
        RaiseEvent transferItems(ids, chkallitems.Checked)
        Me.Close()
    End Sub

    Private Sub DOListFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class