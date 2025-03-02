Public Class FindCustomerSerialNumberFrm
    Private _objcmnbLayer As New clsCommon_BL
    Private _objInv As New clsInvoice
    Private WithEvents fSelect As Selectfrm
    Private chgbyprg As Boolean
    Private MyActiveControl As New Object
    Private lstKey As Integer
    Private _srchTxtId As Integer
    Private _srchOnce As Boolean
    Private _srchIndexId As Byte
    Public Event selectcust(ByVal custname As String, ByVal Cashcustid As Long, ByVal serialNumber As String)
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub ldCashCustomer(ByVal islike As Boolean)
        Dim dt As DataTable
        Dim query As String
        If islike Then
            query = "SELECT AccDescr [Customer Name],Address1,Phone,SerialNo,accid from AccMast LEFT JOIN AccMastAddr ON AccMastAddr.Accountno=AccMast.Accid " & _
                "INNER JOIN WarrentyTrTb ON AccMast.Accid=WarrentyTrTb.custid " & _
                "WHERE AccDescr like '" & txtsearch.Text & "%' OR Phone like'" & txtsearch.Text & "%' OR SerialNo like '" & txtsearch.Text & "%'"
        Else
            query = "SELECT AccDescr [Customer Name],Address1,Phone,SerialNo,accid from AccMast LEFT JOIN AccMastAddr ON AccMastAddr.Accountno=AccMast.Accid " & _
                "INNER JOIN WarrentyTrTb ON AccMast.Accid=WarrentyTrTb.custid " & _
                "WHERE AccDescr='" & txtsearch.Text & "' OR Phone='" & txtsearch.Text & "' OR SerialNo='" & txtsearch.Text & "'"
        End If
        
        dt = _objcmnbLayer._fldDatatable(query)
        If islike Then
            grdlist.DataSource = dt
            setGridHead()
            Exit Sub
        End If
        If dt.Rows.Count > 0 Then
            grdlist.DataSource = dt
            setGridHead()
            'ElseIf dt.Rows.Count > 0 Then
            '    dt = _objcmnbLayer._fldDatatable("SELECT  CashCustomerTb.*,AccDescr FROM CashCustomerTb LEFT JOIN Accmast on CashCustomerTb.customeraccount=accmast.accid WHERE custid=" & dt(0)("custid"))
            '    ldRec(dt)
            btnselect.Enabled = True
        ElseIf dt.Rows.Count = 0 And Not islike Then
            MsgBox("Record Not Found", MsgBoxStyle.Exclamation)
            txtsearch.Focus()
        End If
    End Sub
    Private Sub setGridHead()
        SetGridProperty(grdlist)
        With grdlist
            .Columns("accid").Visible = False
            .Columns("Customer Name").Width = 150
            .Columns("Address1").Width = 150
            .Columns("Phone").Width = 100
            .Columns("SerialNo").Width = 110
        End With
    End Sub

    Private Sub btnselect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnselect.Click
        sendCust()
    End Sub
    Private Sub sendCust()
        If grdlist.RowCount > 0 Then
            If grdlist.CurrentRow Is Nothing Then Exit Sub
        End If
        Dim crow As Integer = grdlist.CurrentRow.Index
        RaiseEvent selectcust(grdlist.Item("Customer Name", crow).Value, grdlist.Item("accid", crow).Value, grdlist.Item("SerialNo", crow).Value)
        Me.Close()
    End Sub


    Private Sub CreateCashCustomerFrm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtsearch.Focus()
    End Sub

    Private Sub txtsearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtsearch.KeyDown
        If e.KeyCode = Keys.Return Then
            ldCashCustomer(False)
            txtsearch.Focus()
        End If
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            Const WM_KEYDOWN As Integer = &H100
            Const WM_KEYUP As Integer = &H101
            'msg.ToString()
            If msg.Msg = WM_KEYDOWN Or msg.Msg = WM_KEYUP Then
                If msg.WParam.ToInt32() = CInt(Keys.F9) Then
                    sendCust()
                End If
            End If
ctn:
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub txtname_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub


    Private Sub rdocard_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        txtsearch.Focus()
    End Sub

    Private Sub grdlist_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdlist.DoubleClick
        sendCust()
    End Sub

    Private Sub txtsearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtsearch.TextChanged
        ldCashCustomer(True)
    End Sub
End Class