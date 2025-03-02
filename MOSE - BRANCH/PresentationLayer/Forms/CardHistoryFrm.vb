Public Class CardHistoryFrm
    Private _objcmnbLayer As New clsCommon_BL
    Private _objvehicle As New clsVechicle
    Private _srchOnce As Boolean
    Private _srchTxtId As Byte
    Private _srchIndexId As Byte
    Private chgbyprg As Boolean
    Private WithEvents fMList As Mlistfrm
    Private MyActiveControl As New Object
    Private lstKey As Keys
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub CardHistoryFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Timer1.Enabled = True
    End Sub
    Private Sub returnRenewalhistory()
        Dim dt As DataTable
        dt = _objvehicle.returncardhistory(0, txtsearch.Text)
        grdrenewal.DataSource = dt
        With grdrenewal
            SetGridProperty(grdrenewal)
            .Columns(.ColumnCount - 1).Visible = False
        End With
        resizeGridColumn(grdrenewal, 2)
    End Sub

    Private Sub returnServicehistory()
        Dim dt As DataTable
        dt = _objvehicle.returncardhistory(1, txtsearch.Text)
        grdservice.DataSource = dt
        With grdservice
            SetGridProperty(grdservice)
            .Columns(3).Width = 125
            .Columns(4).Width = 150
            .Columns(.ColumnCount - 1).Visible = False
        End With
        resizeGridColumn(grdservice, 2)
    End Sub

    Private Sub btnsearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsearch.Click
        loadrec()
    End Sub

    Private Sub txtsearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtsearch.KeyDown
        lstKey = e.KeyCode
        If e.KeyCode = Keys.Return Then
            loadrec()
            If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
        ElseIf e.KeyCode = Keys.Up Or e.KeyCode = Keys.PageUp Then
            If fMList.Visible Then
                fMList.MoveUp(txtsearch.Text)
                Exit Sub
            End If
        ElseIf e.KeyCode = Keys.Down Or e.KeyCode = Keys.PageDown Then
            If Not fMList Is Nothing Then
                If fMList.Visible Then
                    fMList.MoveDown(txtsearch.Text)
                    Exit Sub
                End If
            End If
        End If
    End Sub
    Private Sub loadrec()
        returnRenewalhistory()
        returnServicehistory()
        returncustomer()
        loadcarddetails()
    End Sub
    Private Sub returncustomer()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT * FROM CashCustomerTb " & _
                                         "LEFT JOIN CardmasterTb ON CashCustomerTb.custid=CardmasterTb.customerid WHERE cardnumber='" & txtsearch.Text & "'")
        If dt.Rows.Count > 0 Then
            txtcustomer.Text = dt(0)("CustName")
            txtcustomer.Tag = dt(0)("customeraccount")
            txtaddress.Text = dt(0)("Add1") & vbCrLf & dt(0)("Add2") & vbCrLf & dt(0)("Add3") & vbCrLf & "PH: " & dt(0)("Phone")
        End If
        Dim iNBal As Double = getAccBal(Val(txtcustomer.Tag), 0)
        lblbalance.Text = Strings.Format(iNBal, numFormat)
    End Sub
    Private Sub loadcarddetails()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT cardtypename FROM CardmasterTb LEFT JOIN CardtypemasterTb ON CardmasterTb.cardtypeid=CardtypemasterTb.cardtypeid")
        If dt.Rows.Count > 0 Then
            lblcardtype.Text = dt(0)("cardtypename")
        End If
        Dim _objvehicle As New clsVechicle
        dt = _objvehicle.returnWSCardHistory(2, txtsearch.Text, 0, 0)
        If dt.Rows.Count > 0 Then
            lblservice.Text = dt(0)("totalrenewal") - dt(0)("totalservices")
            lbllastplatenumber.Text = Trim(dt(0)("platenumber") & "")
            If Not IsDBNull(dt(0)("trdate")) Then
                lbllastservicedate.Text = DateValue(dt(0)("trdate"))
            Else
                lbllastservicedate.Text = ""
            End If
        End If
    End Sub
    
    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        txtsearch.Text = ""
        txtcustomer.Text = ""
        txtaddress.Text = ""
        lblcardtype.Text = ""
        lblservice.Text = ""
        lbllastplatenumber.Text = ""
        lbllastservicedate.Text = ""
        grdrenewal.DataSource = Nothing
        grdservice.DataSource = Nothing
    End Sub

    Private Sub grdrenewal_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdrenewal.DoubleClick
        fMainForm.LoadDIS(Val(grdrenewal.Item(grdrenewal.Columns.Count - 1, grdrenewal.CurrentRow.Index).Value))
    End Sub

    Private Sub grdservice_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdservice.DoubleClick
        fMainForm.LoadWSServiceIS(Val(grdservice.Item(grdservice.Columns.Count - 1, grdservice.CurrentRow.Index).Value))
    End Sub

    Private Sub txtsearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtsearch.TextChanged
        If chgbyprg = True Then Exit Sub
        Dim myCtrl As Control = sender
        _srchTxtId = 1
        _srchOnce = False
        ShowFmlist(sender)
    End Sub
    Private Sub ShowFmlist(ByVal myCtrl As Control)
        If chgbyprg Then Exit Sub
        MyActiveControl = myCtrl
        If Not _srchOnce Then
            chgbyprg = True
            If fMList Is Nothing Then
                fMList = New Mlistfrm
            End If
            Dim PopupLoc As Point
            Dim x As Integer = Me.Width - fMList.Width - 100
            Dim y As Integer = Me.Height - fMList.Height - 100
            PopupLoc = New Point(x, y)
            If fMList.Visible = False Then
                Select Case _srchTxtId
                    Case 1
                        SetFmlist(fMList, 25)

                End Select
                fMList.loc = PopupLoc
                fMList.Show(fMainForm)
                fMList.Visible = True
            End If
        End If
        Select Case _srchTxtId
            Case 1
                fMList.SearchIndex = 0
                fMList_doFocus()
                fMList.Search(txtsearch.Text)
                fMList.AssignList(txtsearch, lstKey, chgbyprg)
        End Select
        _srchOnce = True
        chgbyprg = False
    End Sub

    Private Sub fMList_doClose() Handles fMList.doClose
        fMList = Nothing
    End Sub

    Private Sub fMList_doFocus() Handles fMList.doFocus
        If TypeOf (MyActiveControl) Is TextBox Then
            MyActiveControl.focus()
        End If
    End Sub

    Private Sub fMList_doSelect(ByVal ItmFlds() As String) Handles fMList.doSelect
        chgbyprg = True
        Select Case _srchTxtId
            Case 1
                txtsearch.Text = ItmFlds(0)
        End Select
        chgbyprg = False
    End Sub
End Class