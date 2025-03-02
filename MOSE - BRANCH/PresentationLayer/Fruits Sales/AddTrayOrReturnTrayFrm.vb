Public Class AddTrayOrReturnTrayFrm
    Private Const constCarriername = 0
    Private Const constCarrierCurrentVoucher = 2
    Private Const constoldbalance = 1
    Private Const constcarrierReturn = 3
    Private Const constcarrierbal = 4
    Private Const constcarrierid = 5
    Private Const constcarriertrid = 6
    Private Const constcustomerid = 7
    Private Const constchg = 8
    Public loadedTrId As Long
    Public trtype As String
    Public accType As Integer
    Public supplierid As Long
    Private _objcmnbLayer As New clsCommon_BL
    Private chgbypgm As Boolean
    Private Sub AddTrayOrReturnTrayFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'SetGridHeadCarrier()
        Timer1.Enabled = True
    End Sub

    Private Sub btnexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexit.Click
        Me.Close()
    End Sub
    Public Sub loadCarriers()
        Dim dt As DataTable
        '"Select carriername,carrierid  from CarrierTb Order By carriername"
        If grdcarrier.Columns.Count = 0 Then SetGridHeadCarrier()
        Dim str As String = ""
        
        If trtype <> "OB" Then
            If loadedTrId > 0 Then
                str = "select carriername,isnull(obQtyIn,0)obQtyIn,isnull(obQtyOut,0)obQtyOut,isnull(QtyIn,0)QtyIn,isnull(QtyOut,0)QtyOut,CarrierTb.carrierid,isnull(carriertrid,0)carriertrid from CarrierTb " & _
                  "left join ( SELECT QtyIn,QtyOut,carrierid,carriertrid FROM CarrierTrTb where id=" & loadedTrId & ")CarrierTrTb on CarrierTrTb.carrierid=CarrierTb.carrierid " & _
                  "left join(select sum(QtyIn)obQtyIn,sum(QtyOut)obQtyOut,carrierid OBcarrierid from  CarrierTrTb where customerid=" & supplierid & _
                  " and id<>" & loadedTrId & " group by carrierid)OBCarrierTrTb on OBCarrierTrTb.OBcarrierid=CarrierTb.carrierid"
            Else
                str = "select carriername,isnull(obQtyIn,0)obQtyIn,isnull(obQtyOut,0)obQtyOut,0 QtyIn,0 QtyOut,CarrierTb.carrierid,0 carriertrid from CarrierTb " & _
                 "left join(select sum(QtyIn)obQtyIn,sum(QtyOut)obQtyOut,carrierid OBcarrierid from  CarrierTrTb where customerid=" & supplierid & _
                 " group by carrierid)OBCarrierTrTb on OBCarrierTrTb.OBcarrierid=CarrierTb.carrierid"

            End If
        Else
            str = "select carriername,isnull(obQtyIn,0)obQtyIn,isnull(obQtyOut,0)obQtyOut,0 QtyIn,0 QtyOut,CarrierTb.carrierid,0 carriertrid from CarrierTb " & _
                 "left join(select sum(QtyIn)obQtyIn,sum(QtyOut)obQtyOut,carrierid OBcarrierid from  CarrierTrTb where trtype='OB' AND customerid=" & supplierid & _
                 " group by carrierid)OBCarrierTrTb on OBCarrierTrTb.OBcarrierid=CarrierTb.carrierid"

        End If
        
       
        dt = _objcmnbLayer._fldDatatable(str)
        Dim i As Integer
        grdcarrier.Rows.Clear()
        For i = 0 To dt.Rows.Count - 1
            With grdcarrier
                .Rows.Add(1)
                .Item(constCarriername, i).Value = dt(i)("carriername")
                If accType = 0 And trtype = "OB" Then
                    .Item(constCarrierCurrentVoucher, i).Value = dt(i)("obQtyOut")
                ElseIf accType = 0 And trtype = "RT" Then
                    .Item(constCarrierCurrentVoucher, i).Value = dt(i)("QtyIn")
                ElseIf accType = 1 And trtype = "RT" Then
                    .Item(constCarrierCurrentVoucher, i).Value = dt(i)("QtyOut")
                ElseIf accType = 1 And trtype = "OB" Then
                    .Item(constCarrierCurrentVoucher, i).Value = dt(i)("obQtyIn")
                End If
                If accType = 0 And trtype = "RT" Then
                    .Item(constoldbalance, i).Value = dt(i)("obQtyOut") - dt(i)("obQtyIn")
                ElseIf accType = 1 And trtype = "RT" Then
                    .Item(constoldbalance, i).Value = dt(i)("obQtyIn") - dt(i)("obQtyOut")
                End If

                'If Val(.Item(constcarrierReturn, i).Value) = 0 Then
                '    .Item(constcarrierReturn, i).Value = dt(i)("QtyOut")
                'End If
                .Item(constcarrierid, i).Value = dt(i)("carrierid")
                .Item(constcarriertrid, i).Value = dt(i)("carriertrid")
                .Item(constcustomerid, i).Value = supplierid
                .Item(constcarrierbal, i).Value = Val(.Item(constoldbalance, i).Value) - Val(.Item(constCarrierCurrentVoucher, i).Value)
                .Item(constchg, i).Value = ""
            End With
        Next
    End Sub
    Private Sub saveCarrier()
        'accType=0 CUSTOMER
        'accttype=1 supplier
        Dim i As Integer
        Dim qry As String = ""
        Dim trRemarks As String
        If trtype = "OB" Then
            trRemarks = "OPENING TRANSACTION"
        Else
            trRemarks = "RETURN TRANSACTION"
        End If
        Dim fnd As Boolean
        Dim maxid As Long
        With grdcarrier
            maxid = loadedTrId
            For i = 0 To .RowCount - 1
                If .Item(constchg, i).Value = "chg" Then
                    fnd = True
                    Exit For
                End If
            Next
            If maxid = 0 Then
                Dim dt As DataTable
                dt = _objcmnbLayer._fldDatatable("Insert into CarrierTrCmnTb (trtype,trdate,customerid) values('" & trtype & "','" & _
                                        Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd") & "'," & supplierid & ") select Scope_identity() maxid")
                If dt.Rows.Count > 0 Then
                    maxid = Val(dt(0)("maxid") & "")
                End If
            End If
            For i = 0 To .RowCount - 1
                If .Item(constchg, i).Value = "" Then GoTo nxt
                If Val(.Item(constcarriertrid, i).Value) = 0 Then
                    If (accType = 0 And trtype = "OB") Or (accType = 1 And trtype = "RT") Then
                        qry = "Insert into CarrierTrTb(trid,customerid,carrierid,QtyOut,QtyIn,trdate,trRemarks,id,trType) values(" & _
                                loadedTrId & "," & Val(.Item(constcustomerid, i).Value) & "," & Val(.Item(constcarrierid, i).Value) & "," & _
                                Val(.Item(constCarrierCurrentVoucher, i).Value) & ",0," & _
                                "'" & Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd") & "','" & trRemarks & "'," & maxid & ",'" & trtype & "')"
                    ElseIf (accType = 0 And trtype = "RT") Or (accType = 1 And trtype = "OB") Then
                        qry = "Insert into CarrierTrTb(trid,customerid,carrierid,QtyIn,QtyOut,trdate,trRemarks,id,trType) values(" & _
                                loadedTrId & "," & Val(.Item(constcustomerid, i).Value) & "," & Val(.Item(constcarrierid, i).Value) & "," & _
                                Val(.Item(constCarrierCurrentVoucher, i).Value) & ",0," & _
                                "'" & Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd") & "','" & trRemarks & "'," & maxid & ",'" & trtype & "')"

                    End If

                Else
                    If (accType = 0 And trtype = "OB") Or (accType = 1 And trtype = "RT") Then
                        qry = "Update CarrierTrTb set trid=" & loadedTrId & "," & _
                                                 "customerid=" & Val(.Item(constcustomerid, i).Value) & "," & _
                                                 "carrierid=" & Val(.Item(constcarrierid, i).Value) & "," & _
                                                 "QtyOut=" & Val(.Item(constCarrierCurrentVoucher, i).Value) & "," & _
                                                 "QtyIn=0," & _
                                                 "trdate='" & Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd") & "'," & _
                                                 "trRemarks='" & trRemarks & "'" & _
                                                 " where carriertrid=" & Val(.Item(constcarriertrid, i).Value)
                    ElseIf (accType = 0 And trtype = "RT") Or (accType = 1 And trtype = "OB") Then
                        qry = "Update CarrierTrTb set trid=" & loadedTrId & "," & _
                                                 "customerid=" & Val(.Item(constcustomerid, i).Value) & "," & _
                                                 "carrierid=" & Val(.Item(constcarrierid, i).Value) & "," & _
                                                 "QtyIn=" & Val(.Item(constCarrierCurrentVoucher, i).Value) & "," & _
                                                 "QtyOut=0," & _
                                                 "trdate='" & Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd") & "'," & _
                                                 "trRemarks='" & trRemarks & "'" & _
                                                 " where carriertrid=" & Val(.Item(constcarriertrid, i).Value)

                    End If


                End If
                _objcmnbLayer._saveDatawithOutParm(qry)
nxt:
            Next
        End With

    End Sub
    Private Sub SetGridHeadCarrier()
        With grdcarrier
            chgbypgm = True
            SetEntryGridProperty(grdcarrier)
            .RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 11)
            .ColumnCount = 9

            .Columns(constCarriername).HeaderText = "Carrier"
            .Columns(constCarriername).Width = 40
            .Columns(constCarriername).SortMode = DataGridViewColumnSortMode.NotSortable

            .Columns(constoldbalance).HeaderText = "Opening"
            .Columns(constoldbalance).Width = 75
            .Columns(constoldbalance).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(constoldbalance).DefaultCellStyle.Format = "N0"
            .Columns(constoldbalance).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(constoldbalance).Visible = IIf(trtype = "RT", True, False)

            .Columns(constCarrierCurrentVoucher).HeaderText = IIf(trtype = "OB", "Opening", "Returned")
            .Columns(constCarrierCurrentVoucher).Width = 75
            .Columns(constCarrierCurrentVoucher).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(constCarrierCurrentVoucher).DefaultCellStyle.Format = "N0"
            .Columns(constCarrierCurrentVoucher).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


            .Columns(constcarrierReturn).HeaderText = "Returned"
            .Columns(constcarrierReturn).Width = 75
            .Columns(constcarrierReturn).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(constcarrierReturn).DefaultCellStyle.Format = "N0"
            .Columns(constcarrierReturn).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(constcarrierReturn).ReadOnly = False
            .Columns(constcarrierReturn).Visible = False

            .Columns(constcarrierbal).HeaderText = "Balance"
            .Columns(constcarrierbal).Width = 75
            .Columns(constcarrierbal).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(constcarrierbal).DefaultCellStyle.Format = "N0"
            .Columns(constcarrierbal).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(constcarrierbal).Visible = False

            .Columns(constcarrierid).Visible = False
            .Columns(constcarriertrid).Visible = False
            .Columns(constcustomerid).Visible = False
            .Columns(constchg).Visible = False
            chgbypgm = False
        End With
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        saveCarrier()
        MsgBox("Updated", MsgBoxStyle.Information)
        Me.Close()
    End Sub

    Private Sub grdcarrier_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdcarrier.CellClick
        With grdcarrier
            If e.ColumnIndex = constCarrierCurrentVoucher Then
                .Columns(e.ColumnIndex).ReadOnly = False
                .BeginEdit(True)
            Else
                .Columns(e.ColumnIndex).ReadOnly = True
            End If
        End With
    End Sub

    Private Sub grdcarrier_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdcarrier.CellEnter
        If e.ColumnIndex = constCarrierCurrentVoucher Then
            With grdcarrier
                .BeginEdit(True)
            End With
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        resizeGridColumn(grdcarrier, 0)
    End Sub

    Private Sub grdcarrier_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdcarrier.CellValueChanged
        If chgbypgm Then Exit Sub
        If grdcarrier.RowCount > 0 Then
            If e.ColumnIndex = constCarrierCurrentVoucher Then
                grdcarrier.Item(constchg, e.RowIndex).Value = "chg"
            End If

        End If

    End Sub
End Class