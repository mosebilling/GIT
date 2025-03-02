Public Class SearchText
#Region "Form Objects"
    Private WithEvents fMList As Mlistfrm
#End Region
#Region "Private Variable"
    Private chgbyprg As Boolean
    Private _srchTxtId As Integer
    Private _srchOnce As Boolean
    Private MyActiveControl As New Object
    Private lstKey As Integer
#End Region
#Region "Class Object"
    Private _objcmnbLayer As clsCommon_BL
#End Region
    Public srchType As Integer
    Private Sub txtname_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtsearch.TextChanged
        If chgbyprg = True Then Exit Sub
        If srchType = 0 Or srchType = 3 Then Exit Sub
        Select Case srchType
            Case 1
                _srchTxtId = 1
            Case 2
                _srchTxtId = 2
        End Select
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
            Dim x As Integer = Me.Width - fMList.Width
            Dim y As Integer = Me.Height - fMList.Height - 10
            PopupLoc = New Point(x, y)
            If fMList.Visible = False Then
                Select Case _srchTxtId
                    Case 1
                        SetFmlist(fMList, 3)
                    Case 2
                        SetFmlist(fMList, 2)
                End Select
                fMList.loc = PopupLoc
                fMList.Show(fMainForm)
                fMList.Visible = True
            End If
        End If
        Select Case _srchTxtId
            Case 1   'Customer name
                fMList.SearchIndex = 0
                fMList.SearchIndexDescr = 1
                fMList_doFocus()
                fMList.Search(txtsearch.Text)
                fMList.AssignList(txtsearch, lstKey, chgbyprg)
            Case 2   'Item name
                fMList.SearchIndex = 1
                fMList.SearchIndexDescr = 0
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
                txtsearch.Tag = ItmFlds(3)
            Case 2
                txtsearch.Text = ItmFlds(1)
                txtsearch.Tag = ItmFlds(2)
        End Select
        chgbyprg = False
    End Sub
   

    Private Sub SearchText_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If srchType = 3 Then
            loadTech() : cmbtech.Visible = True
        Else
            cmbtech.Visible = False
        End If
    End Sub
    Private Sub loadTech()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select Empname from EmployeeTb")
        cmbtech.Items.Clear()
        Dim i As Integer
        For i = 0 To dt.Rows.Count - 1
            cmbtech.Items.Add(dt(i)(0))
        Next
    End Sub

    Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView.Click
        btnView.Tag = 1
        Me.Close()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        btnView.Tag = 0
    End Sub
End Class