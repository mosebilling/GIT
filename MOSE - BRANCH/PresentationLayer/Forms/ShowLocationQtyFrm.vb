Public Class ShowLocationQtyFrm
    Private _objReport As New clsReport_BL
    Private _objcmnbLayer As New clsCommon_BL
    Private itemid As Long
    Public Sub loadLOCQty(ByVal agritemid As Long)
        Dim ds As DataSet
        If agritemid = 0 Then Exit Sub
        ds = _objReport.returnLocationwiseQty(agritemid)
        Dim dt As DataTable = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            lbltotalValue.Text = ds.Tables(1)(0)("item code") & vbCrLf & ds.Tables(1)(0)("description")
            itemid = agritemid
        End If
        grdLocation.DataSource = dt
        With grdLocation
            SetGridProperty(grdLocation)
            .ReadOnly = False
            .Columns(0).ReadOnly = True
            .Columns(1).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(1).ReadOnly = True
            .Columns(2).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(2).ReadOnly = False
            resizeGridColumn(grdLocation, 0)
        End With
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub grdLocation_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdLocation.CellEnter
        If e.ColumnIndex = 2 Then grdLocation.BeginEdit(True)
    End Sub

    Private Sub btnupdateopening_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdateopening.Click
        Dim ds As DataSet
        Dim locationcode As String
        Dim lid As Integer
        Dim i As Integer
        Dim qty As Double
        Dim ttlqty As Double
        For i = 0 To grdLocation.RowCount - 1
            locationcode = grdLocation.Item("Location", i).Value
            If Val(grdLocation.Item("Op Qty", i).Value & "") = 0 Then grdLocation.Item("Op Qty", i).Value = 0
            qty = CDbl(grdLocation.Item("Op Qty", i).Value)
            Dim strTr As String = "select sum(trqty)trqty from( select invtype,case when invtype='IN' THEN 1 ELSE -1 END *trqty+isnull(focqty,0) trqty,itemid,docdefloc from itminvtrtb " & _
                                   "left join vouchertypenotb on itminvtrtb.trtypeno=vouchertypenotb.vrno " & _
                                   "left join itminvcmntb on  itminvtrtb.trid=itminvcmntb.trid " & _
                                   "where itemid=" & itemid & " and docdefloc='" & locationcode & "')tr"

            ds = _objcmnbLayer._ldDataset("SELECT LocOpnQtyTb.itemid,LocOpnQtyTb.LocationID from LocOpnQtyTb " & _
                                                  "inner join invitm on LocOpnQtyTb.itemid=invitm.itemid " & _
                                                  "inner join LocationTb on LocationTb.locationid=LocOpnQtyTb.locationid " & _
                                                  "where LocOpnQtyTb.itemid=" & itemid & " AND LocCode ='" & locationcode & "'" & _
                                                  " Select locationid from LocationTb where LocCode='" & locationcode & "'" & vbCrLf & strTr, False)


            If ds.Tables(1).Rows.Count > 0 Then
                lid = ds.Tables(1)(0)("locationid")
            End If
            Dim strbatch As String = "declare @opcost money select @opcost=opcost from InvItm where itemid=" & itemid & " "
            strbatch = strbatch & "set @opcost=isnull(@opcost,0) "
            strbatch = strbatch & "delete from BatchTb where itemid=" & itemid & " and batchTrid=0 and lcode='" & locationcode & "' "
            strbatch = strbatch & "insert into BatchTb(batchdate,Itemid,batchTrid,InQty,Cost,isFoc,lcode) values(" & _
            "'" & Format(DateValue(DateFrom), "yyyy/MM/dd") & "'," & itemid & ",0," & qty & ",@opcost,0,'" & locationcode & "')"
            Dim QIH As Double
            If ds.Tables(2).Rows.Count > 0 Then
                QIH = Val(ds.Tables(2)(0)("trqty") & "")
            End If
            Dim strqry As String
            strqry = "declare @opcost money select @opcost=opcost from InvItm where itemid=" & itemid & vbCrLf
            If ds.Tables(0).Rows.Count > 0 Then
                _objcmnbLayer._saveDatawithOutParm(strqry & "Update LocOpnQtyTb set locationCost=@opcost,locQIH=" & QIH + qty & ", qty=" & qty & _
                                                   " where itemid=" & itemid & " and locationid=" & lid)
            Else
                _objcmnbLayer._saveDatawithOutParm(strqry & "Insert into LocOpnQtyTb (itemid,LocationID,qty,locationCost) " & _
                                                   "values(" & itemid & "," & lid & "," & qty & ",@opcost)")
            End If
            ttlqty = ttlqty + qty
            _objcmnbLayer._saveDatawithOutParm("Update Invitm set opQty=" & ttlqty & _
                                                      " where itemid=" & itemid & " " & strbatch)
        Next
        _objcmnbLayer.setRefreshQty(itemid)
        MsgBox("Updated", MsgBoxStyle.Information)
        loadLOCQty(itemid)

    End Sub

    Private Sub ShowLocationQtyFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class