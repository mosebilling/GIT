Public Class ViewImportedDocsFrm
    Dim dtTable As DataTable
    Private _objcmnbLayer As New clsCommon_BL
    Private Sub btnexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexit.Click
        Me.Close()
    End Sub
    Public Sub loadImportedDocs()
        Dim strquery As String
        Dim condition As String
        If chkallitems.Checked Then
            condition = "WHERE docid=" & Val(lbldoc.Tag)
        Else
            condition = "WHERE id=" & Val(txtitemname.Tag)
        End If
        strquery = "select Trtype [Type],invno [Inv No], trdate [Date], [Customer Name],TrQty,inv.Unit,Unitcost,Trid from (select Trtype,invno, trdate,AccDescr [Customer Name]," & _
                    "TrQty/PFraction TrQty,unit,unitcost/PFraction Unitcost,impDocid,impDocSlno,ItmInvCmnTb.Trid from ItmInvCmnTb " & _
                    "left join ItmInvTrTb on ItmInvCmnTb.trid=ItmInvTrTb.TrId " & _
                    "left join AccMast on ItmInvCmnTb.CSCode=AccMast.AccId union all " & _
                    "select DocType, dno, DDate,AccDescr [Customer Name],Qty/PFraction TrQty,unit,CostPUnit/PFraction Unitcost,ImpDocId,ImpDocLnNo,DocCmnTb.Docid from DocCmnTb " & _
                    "left join DocTranTb on DocCmnTb.DocId=DocTranTb.DocId " & _
                    "left join AccMast on DocCmnTb.CSCode=AccMast.AccId) inv " & _
                    "inner join DocTranTb on inv.impDocSlno=DocTranTb.id " & condition
        dtTable = _objcmnbLayer._fldDatatable(strquery)
        grdSrch.DataSource = dtTable
        setGridHead()
    End Sub
    Private Sub setGridHead()
        Dim i As Integer
        With grdSrch
            SetGridProperty(grdSrch)
            .Columns("Type").Width = 75
            .Columns("Inv No").Width = 75
            .Columns("Date").Width = 75
            .Columns("Unit").Width = 75
            .Columns("Customer Name").Width = 200
            .Columns("TrQty").Width = 75
            .Columns("TrQty").DefaultCellStyle.Format = "N" & 2
            .Columns("TrQty").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns("Unitcost").DefaultCellStyle.Format = "N" & 2
            .Columns("Unitcost").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Unitcost").Width = 90

            .Columns("Trid").Visible = False
            cmbcolms.Items.Clear()
            For i = 0 To .Columns.Count - 1
                cmbcolms.Items.Add(.Columns(i).HeaderText)
            Next
            cmbcolms.SelectedIndex = 1
        End With
    End Sub
    Private Sub chkallitems_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkallitems.Click
        If chkallitems.Checked Then
            txtitemname.Enabled = False
        Else
            txtitemname.Enabled = True
        End If
        loadImportedDocs()
    End Sub

    Private Sub btnfind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnfind.Click
        loadImportedDocs()
    End Sub
End Class