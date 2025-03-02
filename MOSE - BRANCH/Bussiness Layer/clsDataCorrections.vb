Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.IO
Imports System.Xml
Public Class clsDataCorrections
    Private con As SqlConnection
    Public Function Connection() As SqlConnection
        Try
            If Not con Is Nothing Then
                If con.State = ConnectionState.Open Then con.Close() : con = Nothing
            End If
            Dim reader As New XmlTextReader(Application.StartupPath & "\ConString.xml")
            Dim constring As String = ""
            Do While reader.Read
                If reader.Name = "ConnectionString" Then
                    constring = reader.GetAttribute("String")
                End If
            Loop
            reader.Close()
            con = New SqlConnection(constring)
            con.Open()
        Catch ex As Exception
            MsgBox(ex.Message & vbCrLf & "Failed to connect the server", MsgBoxStyle.Critical)
        End Try
        Return con
    End Function


    Public Function UpdateCollation() As Integer
        Dim cmdUpdate As SqlCommand
        Dim dr As SqlDataReader
        Dim str As String
        On Error Resume Next
        Dim defcol As String
        str = "select serverproperty('collation') defcol"
        cmdUpdate = New SqlCommand(str, Connection)
        cmdUpdate.CommandType = CommandType.Text
        Dim dt1 As New DataTable
        dr = cmdUpdate.ExecuteReader(CommandBehavior.CloseConnection)
        dt1.Load(dr)
        If dt1.Rows.Count > 0 Then
            defcol = dt1.Rows(0)(0)
        Else
            Exit Function
        End If
        str = " SELECT t.name ""Table Name""," & _
                " c.name ""Column Name""," & _
                "s.name ""datatype""," & _
                "c.collation_name ""Collation""," & _
                "COL_LENGTH(t.name,c.name) ""csize""" & _
                 " FROM sys.tables t INNER JOIN " & _
                "sys.columns c ON c.object_id=t.object_id INNER JOIN " & _
                "sys.types s ON s.user_type_id=c.user_type_id " & _
                "WHERE c.collation_name <> '" & defcol & "'" & _
                "and t.type like 'U'"

        cmdUpdate = New SqlCommand(str, Connection)
        cmdUpdate.CommandType = CommandType.Text
        Dim dt As New DataTable
        dr = cmdUpdate.ExecuteReader(CommandBehavior.CloseConnection)
        dt.Load(dr)
        Dim i As Integer
        Dim PKname As String
        For i = 0 To dt.Rows.Count - 1
            PKname = ""
            str = "select CONSTRAINT_NAME  from information_schema.KEY_COLUMN_USAGE " & _
                "where TABLE_NAME='" & dt.Rows(i)("Table Name") & "' and COLUMN_NAME ='" & dt.Rows(i)("Column Name") & "'"

            cmdUpdate = New SqlCommand(str, Connection)
            cmdUpdate.CommandType = CommandType.Text
            dr = cmdUpdate.ExecuteReader(CommandBehavior.CloseConnection)
            While dr.Read
                PKname = dr("CONSTRAINT_NAME")
            End While
            If PKname <> "" Then
                str = "alter table " & dt.Rows(i)("Table Name") & " DROP  " & PKname
                cmdUpdate = New SqlCommand(str, Connection)
                cmdUpdate.CommandType = CommandType.Text
                cmdUpdate.ExecuteNonQuery()
            End If
            str = "ALTER TABLE dbo." & dt.Rows(i)("Table Name") & _
                  " ALTER COLUMN [" & dt.Rows(i)("Column Name") & "] " & dt.Rows(i)("datatype") & "(" & IIf(Val(dt.Rows(i)("csize")) <= 0, 50, dt.Rows(i)("csize")) & ")" & _
                  " COLLATE " & defcol & " NULL"
            str = Replace(str, "nvarchar", "varchar")
            cmdUpdate = New SqlCommand(str, Connection)
            cmdUpdate.CommandType = CommandType.Text
            cmdUpdate.ExecuteNonQuery()

            If PKname <> "" Then
                str = "alter table " & dt.Rows(i)("Table Name") & " ALTER COLUMN [" & dt.Rows(i)("Column Name") & "] " & dt.Rows(i)("datatype") & "(" & IIf(Val(dt.Rows(i)("csize")) <= 0, 50, dt.Rows(i)("csize")) & ") " & _
                " COLLATE " & defcol & " not null"
                cmdUpdate = New SqlCommand(str, Connection)
                cmdUpdate.CommandType = CommandType.Text
                cmdUpdate.ExecuteNonQuery()

                str = " alter table " & dt.Rows(i)("Table Name") & " ADD CONSTRAINT  " & PKname & " PRIMARY KEY([" & dt.Rows(i)("Column Name") & "])"
                cmdUpdate = New SqlCommand(str, Connection)
                cmdUpdate.CommandType = CommandType.Text
                cmdUpdate.ExecuteNonQuery()
            End If
        Next
        Return dt.Rows.Count
    End Function
End Class
