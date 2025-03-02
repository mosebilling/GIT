Public Class VehiclemasterFrm
#Region "Constant variables"
    Private Const constcartype = 0
    Private Const constplateno = 1
    Private Const constYear = 2
    Private Const constchassis = 3
    Private Const constbodyno = 4
    Private Const constCardType = 5
    Private Const constdiscountvchr = 6
    Private Const constKm = 7
    Private Const constservicedate = 8
    Private Const constCarid = 9
#End Region
    Private chgbyprg As Boolean
    Private _objcmnbLayer As New clsCommon_BL
    Private activecontrolname As String
    Private WithEvents fCashCust As CreateCashCustomerFrm
    Private WithEvents fSelect As Selectfrm
    Private WithEvents fwscardsale As WSCardRenew

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub setGridHead()

        Try
            chgbyprg = True
            With grdVoucher
                SetEntryGridProperty(grdVoucher)
                .ColumnCount = 10

                .Columns(constcartype).HeaderText = "Car Type"
                .Columns(constcartype).Width = 250
                .Columns(constcartype).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(constcartype).DefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.5!, System.Drawing.FontStyle.Regular)

                .Columns(constplateno).HeaderText = "Plate Number"
                .Columns(constplateno).Width = 100 '100
                .Columns(constplateno).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(constplateno).DefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.5!, System.Drawing.FontStyle.Regular)

                .Columns(constYear).HeaderText = "Reg. Year"
                .Columns(constYear).Width = 75
                .Columns(constYear).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(constYear).DefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.5!, System.Drawing.FontStyle.Regular)

                .Columns(constchassis).HeaderText = "Chassis Number"
                .Columns(constchassis).Width = 140
                .Columns(constchassis).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(constchassis).DefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.5!, System.Drawing.FontStyle.Regular)

                .Columns(constbodyno).HeaderText = "Body Number"
                .Columns(constbodyno).Width = 140
                .Columns(constbodyno).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(constbodyno).DefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.5!, System.Drawing.FontStyle.Regular)
                .Columns(constbodyno).Frozen = True

                .Columns(constCardType).HeaderText = "Card Type"
                .Columns(constCardType).Width = 150
                .Columns(constCardType).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(constCardType).DefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.5!, System.Drawing.FontStyle.Regular)
                .Columns(constCardType).ReadOnly = True
                .Columns(constCardType).DefaultCellStyle.BackColor = Color.LightSkyBlue

                .Columns(constdiscountvchr).HeaderText = "Card Number"
                .Columns(constdiscountvchr).Width = 150
                .Columns(constdiscountvchr).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(constdiscountvchr).DefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.5!, System.Drawing.FontStyle.Regular)
                .Columns(constdiscountvchr).ReadOnly = True
                .Columns(constdiscountvchr).DefaultCellStyle.BackColor = Color.LightSkyBlue


                .Columns(constKm).HeaderText = "Last Service On (KM)"
                .Columns(constKm).Width = 140
                .Columns(constKm).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(constKm).DefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.5!, System.Drawing.FontStyle.Regular)
                .Columns(constKm).ReadOnly = True
                .Columns(constKm).DefaultCellStyle.BackColor = Color.LightSkyBlue

                .Columns(constservicedate).HeaderText = "Last Service On (Date)"
                .Columns(constservicedate).Width = 140
                .Columns(constservicedate).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(constservicedate).DefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.5!, System.Drawing.FontStyle.Regular)
                .Columns(constservicedate).ReadOnly = True
                .Columns(constservicedate).DefaultCellStyle.BackColor = Color.LightSkyBlue

                .Columns(constCarid).Visible = False

            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        chgbyprg = False
    End Sub

    Private Sub VehiclemasterFrm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        setGridHead()
    End Sub
    Public Sub loadVehiclemaster(ByVal customerid As Integer)

    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            Const WM_KEYDOWN As Integer = &H100
            Const WM_KEYUP As Integer = &H101
            msg.ToString()
            If msg.Msg = WM_KEYDOWN Or msg.Msg = WM_KEYUP Then
                If msg.WParam.ToInt32() = CInt(Keys.F8) Then

                ElseIf activecontrolname = "grdVoucher" Or msg.WParam.ToInt32() = CInt(Keys.Enter) Then
                    If msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up) Or msg.WParam.ToInt32() = CInt(Keys.Enter) Or msg.WParam.ToInt32() = CInt(Keys.F2) Or msg.WParam.ToInt32() = CInt(Keys.F3) Or msg.WParam.ToInt32() = CInt(Keys.F4) Then
                        grdVoucher_KeyDown(Nothing, New KeyEventArgs(keyData))
                        Return True
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function

    Private Sub grdVoucher_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellClick
        If grdVoucher.RowCount = 0 Then Exit Sub
        grdVoucher.BeginEdit(True)
    End Sub

    Private Sub grdVoucher_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.GotFocus
        activecontrolname = "grdVoucher"
    End Sub

    Private Sub grdVoucher_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdVoucher.KeyDown
        Try
            If e.KeyCode = Keys.F3 Then
                AddRow()
            ElseIf e.KeyCode = Keys.F4 Then
                removeRow()
            ElseIf e.KeyCode = Keys.Enter Then
                If FindNextCell(grdVoucher, grdVoucher.CurrentCell.RowIndex, grdVoucher.CurrentCell.ColumnIndex + 1) Then
                    AddRow()
                End If
                grdVoucher.BeginEdit(True)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub AddRow()
        Dim i As Integer
        With grdVoucher
            activecontrolname = "grdVoucher"
            .Rows.Add(1)
            i = .RowCount - 1
            .CurrentCell = .Item(0, i)
        End With
        grdVoucher.BeginEdit(True)
    End Sub
    Private Sub removeRow()
        If MsgBox("Do you want remove the row?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        With grdVoucher
            .Rows.RemoveAt(.CurrentRow.Index)
            .ClearSelection()
        End With
    End Sub

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        AddRow()
    End Sub

    Private Sub btnrem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrem.Click
        removeRow()
    End Sub

    Private Sub grdVoucher_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.Leave
        activecontrolname = ""
    End Sub
    Private _srchTxtId As Integer
    Private Sub txtcustomer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtcustomer.KeyDown
        If e.KeyCode = Keys.F2 Then
            fCashCust = New CreateCashCustomerFrm
            fCashCust.ShowDialog()
            fCashCust = Nothing
        End If
        'If e.KeyCode = Keys.F2 Then
        '    _srchTxtId = 1
        '    ldSelect(1)
        'ElseIf e.KeyCode = Keys.Delete Or e.KeyCode = Keys.Back Then
        '    txtcustomer.Text = ""
        '    txtcustomer.Tag = ""
        'End If
    End Sub
    Private Sub ldSelect(ByVal BVal As Single)
        fSelect = New Selectfrm
        'nSelect = BVal
        SetForm(fSelect, BVal)
        If BVal = 1 Or BVal = 0 Then
            fSelect.Width = 742
            fSelect.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
        Else
            fSelect.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedToolWindow
            fSelect.Width = 425
        End If
        fSelect.Show()
    End Sub

    Private Sub fCashCust_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fCashCust.FormClosed
        fCashCust = Nothing
    End Sub


    Private Sub savecarmaster()
        Dim i As Integer
        Dim _objvechicle As New clsVechicle
        For i = 0 To grdVoucher.RowCount - 1
            With grdVoucher
                If .Item(constcartype, i).Value <> "" Then
                    _objvechicle.cartype = .Item(constcartype, i).Value
                    _objvechicle.platenumber = .Item(constplateno, i).Value
                    _objvechicle.regyear = .Item(constYear, i).Value
                    _objvechicle.chassisnumber = .Item(constchassis, i).Value
                    _objvechicle.bodynumber = .Item(constbodyno, i).Value
                    _objvechicle.carid = .Item(constCarid, i).Value
                    _objvechicle.customerid = Val(txtcustomer.Tag)
                    _objvechicle.savecarmaster()
                End If
            End With
        Next
        loadcarmaster()
    End Sub
    Private Sub loadcarmaster()
        Dim dt As DataTable
        Dim _objvechicle As New clsVechicle
        _objvechicle.customerid = Val(txtcustomer.Tag)
        dt = _objvechicle.returncarmaster(0).Tables(0)
        Dim i As Integer
        'Dim r As Integer
        For i = 0 To dt.Rows.Count - 1
            With grdVoucher
                .Rows.Add()
                .Item(constcartype, i).Value = dt(i)("cartype")
                .Item(constplateno, i).Value = dt(i)("platenumber")
                .Item(constYear, i).Value = dt(i)("regyear")
                .Item(constchassis, i).Value = dt(i)("chaisenumber")
                .Item(constbodyno, i).Value = dt(i)("bodynumber")
                .Item(constdiscountvchr, i).Value = dt(i)("discountvouchernumber")
                .Item(constCardType, i).Value = dt(i)("cardtypename")
                .Item(constCarid, i).Value = dt(i)("carid")
            End With
        Next
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        savecarmaster()
        MsgBox("Car master updated", MsgBoxStyle.Information)
    End Sub

    Private Sub btncard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncard.Click
        If grdVoucher.RowCount = 0 Then Exit Sub
        fwscardsale = New WSCardRenew
        With fwscardsale
            '.loadcustomerANDcardetails(Val(grdVoucher.Item(constCarid, grdVoucher.CurrentRow.Index).Value))
            '.txtvehicletype.Tag = Val(grdVoucher.Item(constCarid, grdVoucher.CurrentRow.Index).Value)
            .ShowDialog()
        End With
    End Sub

    Private Sub txtcustomer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcustomer.TextChanged

    End Sub

    Private Sub fSelect_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fSelect.FormClosed
        fSelect = Nothing
    End Sub

    Private Sub fSelect_doSelect(ByVal KeyId As String, ByVal strFld1 As String, ByVal strFld2 As String) Handles fSelect.doSelect
        chgbyprg = True
        If _srchTxtId = 1 Then
            txtcustomer.Text = strFld1
            txtcustomer.Tag = KeyId
        End If
        chgbyprg = False
    End Sub

    Private Sub fCashCust_selectcust(ByVal custname As String, ByVal custaddress As String, ByVal Cashcustid As Long, ByVal cardnumber As String, ByVal phone As String, ByVal gstn As String) Handles fCashCust.selectcust
        txtcustomer.Text = custname
        txtaddress.Text = custaddress
        txtcustomer.Tag = Cashcustid
        loadcarmaster()
    End Sub
End Class