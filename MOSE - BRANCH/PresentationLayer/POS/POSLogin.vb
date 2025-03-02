Public Class POSLogin
#Region "Class Object Declaration"
    Dim _objcmnbLayer As New clsCommon_BL
#End Region
    Public Event sendUsercode(ByVal usercode As String, ByVal usertype As Integer, ByVal counter As String)
    Private Sub POSLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        getCounter()
    End Sub
    Private Sub getCounter()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select userCounter from UserTb where UserId='" & CurrentUser & "'")
        If dt.Rows.Count > 0 Then
            lblcounter.Text = "Counter : " & dt(0)("userCounter")
            lblcounter.Tag = dt(0)("userCounter")
        Else
            lblcounter.Text = "Counter : ADMIN"
            lblcounter.Tag = "ADMIN"
        End If
    End Sub
    Private Function checkLogin() As Boolean
        If Not userType Then
            GoTo ext
        End If
        Dim dt As DataTable
        Dim isreception As Integer
        If txtusercode.Text = "" Then
            MsgBox("Invalid Login", MsgBoxStyle.Exclamation)
            Return False
        End If
        dt = _objcmnbLayer._fldDatatable("Select Id,isreception,UserId,MasterYN from UserTb where usercode='" & txtusercode.Text & "'")
        If dt.Rows.Count = 0 Then
            MsgBox("Invalid Login", MsgBoxStyle.Exclamation)
            Return False
        Else
            If CurrentUser <> dt(0)("UserId") Then
                MsgBox("Counter Code and User Id does not match", MsgBoxStyle.Exclamation)
                Return False
            End If
            isreception = dt(0)("isreception")
            userType = IIf(dt(0)("MasterYN") = True, 0, 1)
        End If
ext:
        RaiseEvent sendUsercode(txtusercode.Text, isreception, lblcounter.Tag)
        Return True
    End Function

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        If checkLogin() Then
            Me.Close()
        End If
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub

    Private Sub txtusercode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtusercode.KeyDown
        If e.KeyCode = Keys.Return Then
            If checkLogin() Then
                Me.Close()
            End If
        End If
    End Sub

    Private Sub txtusercode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtusercode.TextChanged

    End Sub
End Class