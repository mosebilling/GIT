Imports System.Data.SqlClient
Public Class webDatalayer
    Private con As SqlConnection
    Public isSms As Boolean
    Private Sub openCon()
        Try
            If Not con Is Nothing Then
                If con.State = ConnectionState.Open Then con.Close() : con = Nothing
            End If
            Dim constring As String
            If isSms Then
                '
                'constring = "Server=VINVIS-PC\SQL2014;uid=sa;pwd=mosesft;database=MOSESMSCNT;"
                constring = "Data Source=199.79.62.22;Initial Catalog=SMSCustomerDb;Integrated Security=False;User ID=vinvisin;pwd=E18@a4L7ghi;Connect Timeout=15;Encrypt=False;Packet Size=4096"
            Else
                constring = "Server=" & webserver & ";uid=" & webusername & ";pwd=" & webpassword & ";database=" & webdbname & ";"
                'constring = "Data Source=" & webserver & ";Initial Catalog=" & webdbname & ";Integrated Security=False;User ID=" & webusername & ";pwd=" & webpassword & ";Connect Timeout=15;Encrypt=False;Packet Size=4096"
            End If

            con = New SqlConnection()
            con.ConnectionString = constring
            con.Open()
        Catch ex As Exception
            MsgBox("Online Connection Failure" & vbCrLf & "Please check your Internet connection or contact vendor", MsgBoxStyle.Exclamation)
        End Try
    End Sub
    Private Sub closeCon()
        If Not con Is Nothing Then
            If con.State = ConnectionState.Open Then con.Close() : con = Nothing
        End If
    End Sub
    Public Sub saveDataToOnline(ByVal str As String)
        Try
            openCon()
            Dim cmd As New SqlCommand(str, con)
            cmd.CommandType = CommandType.Text
            cmd.ExecuteNonQuery()
            closeCon()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Public Function saveDataToOnlineExecuteScalar(ByVal str As String) As Integer
        Dim id As String = ""
        Try
            openCon()
            Dim cmd As New SqlCommand(str, con)
            cmd.CommandType = CommandType.Text
            Dim recordNo As Object
            recordNo = cmd.ExecuteScalar()
            closeCon()
            id = recordNo.ToString()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return Val(id)
    End Function
    Public Function checkexist(ByVal str As String) As Boolean
        Dim dtcheck As New DataTable
        Dim sqa As SqlDataAdapter
        openCon()
        Dim cmd As New SqlCommand(str, con)
        cmd.CommandType = CommandType.Text
        sqa = New SqlDataAdapter(cmd)
        sqa.Fill(dtcheck)
        closeCon()
        If dtcheck.Rows.Count > 0 Then
            Return True
        End If
        Return False
    End Function
    Public Function returnDatatable(ByVal str As String) As DataTable
        Dim dt As New DataTable
        Dim sqa As SqlDataAdapter
        openCon()
        If con.State = ConnectionState.Closed Then Return Nothing
        Dim cmd As New SqlCommand(str, con)
        cmd.CommandType = CommandType.Text
        sqa = New SqlDataAdapter(cmd)
        sqa.Fill(dt)
        closeCon()
        Return dt
    End Function
    Public Function testconnection() As Boolean
        openCon()
        If con.State = ConnectionState.Closed Then
            Return False
        End If
        closeCon()
        Return True
    End Function
End Class
