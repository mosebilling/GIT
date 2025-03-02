Imports System.Data.SqlClient

Imports DataAccessLayer

Public Class Dlayer
    Private _LinkNo As Long
    Public Property LinkNo() As Long
        Get
            Return _LinkNo
        End Get
        Set(ByVal value As Long)
            _LinkNo = value
        End Set
    End Property
    Private _cmnMthds As New CmnMethods()
    Public con As SqlConnection
    Private Sub openCon()
        Try
            If con Is Nothing Then
                'If con.State = ConnectionState.Open Then con.Close() : con = Nothing
                If connectionstring = "" Then
                    connectionstring = readXml()
                End If
                'Dim constring As String = connectionstring
                con = New SqlConnection()
                con.ConnectionString = connectionstring
                con.Open()
            End If
        Catch ex As Exception
            MsgBox("Connection Failure", MsgBoxStyle.Exclamation)
        End Try
    End Sub
    Public Sub closeCon()
        If Not con Is Nothing Then
            If con.State = ConnectionState.Open Then con.Close() : con = Nothing
        End If
    End Sub
    Public Sub ExecuteNonQuery(ByVal str As String(,), ByVal cmdtext As String, ByVal count As Integer)
        Try
            If connectionstring = "" Then
                connectionstring = readXml()
            End If
            Using con As New SqlConnection(connectionstring)
                Using cmd As New SqlCommand(cmdtext)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Connection = con
                    Dim i As Integer
                    For i = 0 To count - 1
                        If str(i, 2) = "d" Then
                            If DateValue(str(i, 1)) < DateValue("01/01/1950") Then
                                str(i, 1) = "01/01/1950"
                            End If
                            cmd.Parameters.AddWithValue(str(i, 0), DateValue(str(i, 1)))
                        Else
                            cmd.Parameters.AddWithValue(str(i, 0), str(i, 1))
                        End If
                    Next
                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Public Function ExecuteScalar(ByVal str As String(,), ByVal cmdtext As String, ByVal count As Integer) As String
        Try
            openCon()
            Dim cmd As New SqlCommand(cmdtext, con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim i As Integer
            For i = 0 To count - 1
                If str(i, 2) = "d" Then
                    cmd.Parameters.AddWithValue(str(i, 0), DateValue(str(i, 1)))
                Else
                    cmd.Parameters.AddWithValue(str(i, 0), str(i, 1))
                End If
            Next
            Dim maxno = cmd.ExecuteScalar()
            Return maxno.ToString()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return ""
    End Function
    Public Sub saveBulk(ByVal dt As DataTable)
        If dt.Rows.Count > 0 Then
            Dim constring As String = readXml()
            Using con As New SqlConnection(constring)
                Using sqlBulkCopy As New SqlBulkCopy(con)
                    'Set the database table name
                    sqlBulkCopy.DestinationTableName = "dbo.ItmInvTrTb"

                    '[OPTIONAL]: Map the DataTable columns with that of the database table
                    'sqlBulkCopy.ColumnMappings.Add("Id", "CustomerId")
                    'sqlBulkCopy.ColumnMappings.Add("Name", "Name")
                    'sqlBulkCopy.ColumnMappings.Add("Country", "Country")
                    con.Open()
                    sqlBulkCopy.WriteToServer(dt)
                    con.Close()
                End Using
            End Using
        End If
    End Sub
    Public Sub saveItmInvTrTbBulk(ByVal dt As DataTable)
        If dt.Rows.Count > 0 Then
            Dim trid As Long
            trid = dt(0)("TrId")
            Dim sqa As SqlDataAdapter
            Dim dtreturn As New DataTable
            Dim constring As String = readXml()
            Using con As New SqlConnection(constring)
                Using cmd As New SqlCommand("saveItmInvTrTbBulk")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Connection = con
                    cmd.Parameters.AddWithValue("@trid", trid)
                    cmd.Parameters.AddWithValue("@tblItmInvTrTb", dt)
                    con.Open()
                    'cmd.ExecuteNonQuery()
                    sqa = New SqlDataAdapter(cmd)
                    sqa.Fill(dtreturn)
                    con.Close()
                    updateItemMasterDataTable(dtreturn)
                End Using
            End Using
        End If
    End Sub
    Public Sub updateItemMasterDataTable(ByVal dt As DataTable)
        If dt Is Nothing Then Exit Sub
        Dim dtTbIndex As Integer
        Dim dtrow As DataRow
        If _vInvItmtable Is Nothing Then Exit Sub
        If dt.Rows.Count = 0 Then Exit Sub
        If _vInvItmtable.Rows.Count = 0 Then Exit Sub
        Dim i As Integer
        For i = 0 To dt.Rows.Count - 1
            dtrow = _vInvItmtable.Select("itemid=" & Val(dt(i)("itemid") & ""))(0)
            dtTbIndex = _vInvItmtable.Rows.IndexOf(dtrow)
            _vInvItmtable(dtTbIndex)("QIH") = Val(dt(i)("qih") & "")
            _vInvItmtable(dtTbIndex)("Cost") = Val(dt(i)("Cost") & "")
            'MsgBox(_vInvItmtable(dtTbIndex)("code"))
        Next
    End Sub
    Public Function saveItmAccTrTbBulk(ByVal dt As DataTable, ByVal str As String(,), ByVal count As Integer) As Long
        Dim recordNo As Object
        Dim _id As String = ""
        If dt.Rows.Count > 0 Then
            Dim constring As String = readXml()

            Using con As New SqlConnection(constring)



                Using cmd As New SqlCommand("AccTrCmn_SAVEorEdit")

                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Connection = con
                    Dim i As Integer
                    For i = 0 To count - 1
                        If str(i, 2) = "d" Then
                            cmd.Parameters.AddWithValue(str(i, 0), DateValue(str(i, 1)))
                        Else
                            cmd.Parameters.AddWithValue(str(i, 0), str(i, 1))
                        End If
                    Next

                    'MsgBox(dt.Columns.Count)
                    cmd.Parameters.AddWithValue("@tblAccTrDet", dt)
                    con.Open()
                    Try

                        recordNo = cmd.ExecuteScalar()
                        _id = recordNo.ToString()
                        'cmd.ExecuteNonQuery()
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try

                    con.Close()
                End Using
            End Using
        End If


        
        Return Val(_id)
    End Function
    Public Sub saveItmAccTrTbCostingBulk(ByVal dt As DataTable)
        If dt.Rows.Count > 0 Then
            Dim constring As String = readXml()
            Using con As New SqlConnection(constring)
                Using cmd As New SqlCommand("saveItmAccTrTbCostingBulk")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Connection = con
                    cmd.Parameters.AddWithValue("@tblAccTrDet", dt)
                    con.Open()
                    Try
                        cmd.ExecuteNonQuery()
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try

                    con.Close()
                End Using
            End Using
        End If
    End Sub
    Public Sub saveDocTrTbBulk(ByVal dt As DataTable, ByVal str As String(,), ByVal count As Integer)
        If dt.Rows.Count > 0 Then
            Dim constring As String = readXml()
            Using con As New SqlConnection(constring)
                Using cmd As New SqlCommand("saveDocumentCmn")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Connection = con
                    Dim i As Integer
                    For i = 0 To count - 1
                        If str(i, 2) = "d" Then
                            cmd.Parameters.AddWithValue(str(i, 0), DateValue(str(i, 1)))
                        Else
                            cmd.Parameters.AddWithValue(str(i, 0), str(i, 1))
                        End If
                    Next
                    cmd.Parameters.AddWithValue("@tblDocTrTb", dt)
                    con.Open()
                    Try
                        cmd.ExecuteNonQuery()
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try

                    con.Close()
                End Using
            End Using
        End If
    End Sub
    Public Function _fldDatatable(ByVal qry As String) As DataTable
        Dim sqa As SqlDataAdapter
        Dim dtreturn As New DataTable
        If connectionstring = "" Then
            connectionstring = readXml()
        End If
        Using con As New SqlConnection(connectionstring)
            Using cmd As New SqlCommand(qry)
                cmd.CommandType = CommandType.Text
                cmd.Connection = con
                con.Open()
                sqa = New SqlDataAdapter(cmd)
                sqa.Fill(dtreturn)
                con.Close()
            End Using
        End Using
        Return dtreturn
    End Function
    Public Sub updateQTYontr(ByVal trid As Long, ByVal mchnname As String)
        Dim param As String(,) = New String(1, 2) {{"@trid", trid, ""}, _
                                                   {"@mchnname", mchnname, ""}}
        ExecuteNonQuery(param, "updatecostavarageontr", 2)
    End Sub
    Public Sub updatecostavarageontr(ByVal itemid As Long, ByVal trqty As Double, ByVal id As Long)
        Dim param As String(,) = New String(3, 2) {{"@itemid", itemid, ""}, _
                                                  {"@isdatechanged", 0, ""}, _
                                                  {"@trqty", trqty, ""}, _
                                                  {"@id", id, ""}}
        ExecuteNonQuery(param, "setcostaveragetomodifieditems", 4)
    End Sub
    Public Sub savewithoutparam(ByVal cmdtext As String)
        Try
            If connectionstring = "" Then
                connectionstring = readXml()
            End If
            Using con As New SqlConnection(connectionstring)
                Using cmd As New SqlCommand(cmdtext)
                    cmd.CommandType = CommandType.Text
                    cmd.Connection = con
                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Public Function _ldDataset(ByVal str As String(,), ByVal cmdtext As String, ByVal count As Integer, ByVal isproc As Boolean) As DataSet
        Dim sqa As SqlDataAdapter
        Dim dtreturn As New DataSet
        If connectionstring = "" Then
            connectionstring = readXml()
        End If
        Using con As New SqlConnection(connectionstring)
            con.Open()
            Using cmd As New SqlCommand(cmdtext)
                If isproc Then
                    cmd.CommandType = CommandType.StoredProcedure
                Else
                    cmd.CommandType = CommandType.Text
                End If
                cmd.Connection = con
                Dim i As Integer
                For i = 0 To count - 1
                    If str(i, 2) = "d" Then
                        If DateValue(str(i, 1)) < DateValue("01/01/1950") Then
                            str(i, 1) = "01/01/1950"
                        End If
                        cmd.Parameters.AddWithValue(str(i, 0), DateValue(str(i, 1)))
                    Else
                        cmd.Parameters.AddWithValue(str(i, 0), str(i, 1))
                    End If
                Next
                sqa = New SqlDataAdapter(cmd)
                sqa.Fill(dtreturn)
            End Using
        End Using
        Return dtreturn
    End Function
    Public Sub ExportItmInvCmnTbToWebserver(ByVal m As clsInvoice)
        Dim param As String(,) = New String(37, 2) {{"@trid", m.TrId, ""}, _
                                                   {"@InvTypeNo", m.InvTypeNo, ""}, _
                                                    {"@TrDate", m.TrDate, "d"}, _
                                                    {"@TrType", m.TrType, ""}, _
                                                    {"@PreFix", m.Prefix, ""}, _
                                                    {"@InvNo", m.InvNo, ""}, _
                                                    {"@CSCode", m.CSCode, ""}, _
                                                    {"@TrRefNo", m.TrRefNo, ""}, _
                                                    {"@TrDescription", m.TrDescription, ""}, _
                                                    {"@UserId", m.UserId, ""}, _
                                                    {"@Discount", m.Discount, ""}, _
                                                    {"@OthCost", m.OthCost, ""}, _
                                                    {"@AreaCode", m.AreaCode, ""}, _
                                                    {"@jobcode", m.JobCode, ""}, _
                                                    {"@LPO", m.LPO, ""}, _
                                                    {"@SlsPurchRetId", 0, ""}, _
                                                    {"@IsFC", m.IsFC, ""}, _
                                                    {"@FCRate", m.FCRate, ""}, _
                                                    {"@NFraction", m.NFraction, ""}, _
                                                    {"@FC", m.FC, ""}, _
                                                    {"@CrtDt", m.CrtDt, "d"}, _
                                                    {"@ModiDt", m.ModiDt, "d"}, _
                                                    {"@NetAmt", m.NetAmt, ""}, _
                                                    {"@MchName", m.MchName, ""}, _
                                                    {"@rndoff", m.rndoff, ""}, _
                                                    {"@OthrCust", m.OthrCust, ""}, _
                                                    {"@TaxType", m.TaxType, ""}, _
                                                    {"@CashCustName", m.CashCustName, ""}, _
                                                    {"@cashAmount", m.cashAmount, ""}, _
                                                    {"@CardAmount", m.CardAmount, ""}, _
                                                    {"@isTaxInvoice", m.isTaxInvoice, ""}, _
                                                    {"@isB2B", m.isB2B, ""}, _
                                                    {"@tenderd", m.tenderd, ""}, _
                                                    {"@poschange", m.poschange, ""}, _
                                                    {"@customerPhone", m.customerPhone, ""}, _
                                                    {"@GSTN", m.GSTN, ""}, _
                                                    {"@grossTotal", m.grossTotal, ""}, _
                                                    {"@gstamt", m.taxAmt, ""}}
        ExecuteWebNonQuery(param, "saveItmInvCmnTb", 38)
    End Sub
    Public Sub saveItmInvTrTbBulkToWeb(ByVal dt As DataTable)
        If dt.Rows.Count > 0 Then
            Dim trid As Long
            trid = dt(0)("TrId")
            Dim dtreturn As New DataTable
            Dim constring As String = readXml()
            Using con As New SqlConnection(constring)
                Using cmd As New SqlCommand("saveItmInvTrTbBulk")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Connection = con
                    cmd.Parameters.AddWithValue("@trid", trid)
                    cmd.Parameters.AddWithValue("@tblItmInvTrTb", dt)
                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()
                End Using
            End Using
        End If
    End Sub
    Public Sub ExecuteWebNonQuery(ByVal str As String(,), ByVal cmdtext As String, ByVal count As Integer)
        Try
            'If connectionstring = "" Then
            '    connectionstring = readXml()
            'End If
            Dim constring As String = "Data Source=" & webserver & ";Initial Catalog=" & webdbname & _
            ";Integrated Security=False;User ID=" & webusername & ";pwd=" & webpassword & ";Encrypt=False;Packet Size=4096"
            Using con As New SqlConnection(constring)
                Using cmd As New SqlCommand(cmdtext)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Connection = con
                    Dim i As Integer
                    For i = 0 To count - 1
                        If str(i, 2) = "d" Then
                            If DateValue(str(i, 1)) < DateValue("01/01/1950") Then
                                str(i, 1) = "01/01/1950"
                            End If
                            cmd.Parameters.AddWithValue(str(i, 0), DateValue(str(i, 1)))
                        Else
                            cmd.Parameters.AddWithValue(str(i, 0), str(i, 1))
                        End If
                    Next
                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
End Class
