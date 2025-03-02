Imports System.IO
Imports System.Data.SqlClient
Public Class ServerFrm
    Public ChangeServer As Boolean
    Public cancelFrm As Boolean
    'Dim drCompany As SqlClient.SqlDataReader
    Dim _vDrdatabases As DataTable
    Dim _objcmnbLayer As New clsCommon_BL
    Public chgpgm As Boolean
    Private isSelectedDatabase As Boolean
    Private con As SqlConnection

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        If txtServer.Text = "" Then MsgBox("You are not able to continue without server name", MsgBoxStyle.Critical) : Exit Sub
        If cmbdatabases.Text = "" Then MsgBox("You are not able to continue without Database name", MsgBoxStyle.Critical) : Exit Sub
        If txtpath.Text = "" And cmbservertype.SelectedIndex < 3 Then MsgBox("You are not able to continue without Datapath", MsgBoxStyle.Critical) : Exit Sub
        Dim DPATH As String = ""
        If cmbservertype.SelectedIndex = 2 Then
            _objcmnbLayer.P_server = txtServer.Text
            _objcmnbLayer.P_database = cmbdatabases.Text
            'cmn.RemoveDatabase()
        ElseIf cmbservertype.SelectedIndex = 1 Then
            If Val(lbldata.Tag) = 0 Or ChangeServer Then
                If Not isSelectedDatabase Then
                    Dim FolderName As String
                    If Not ChangeServer Then
                        Dim i As Integer
                        DPATH = txtpath.Text
                        If File.Exists(DPATH & "\" & cmbdatabases.Text & ".mdf") = False Then
                            For i = 2 To Len(DPATH)
                                FolderName = Trim(Mid(DPATH, Len(DPATH) + 1))
                                If InStr(FolderName, "\") > 0 Then
                                    FolderName = Trim(Mid(FolderName, 1, FolderName.IndexOf("\")))
                                Else
                                    i = Len(DPATH)
                                End If
                                If Directory.Exists(DPATH & FolderName) = False Then
                                    Directory.CreateDirectory(DPATH & FolderName)
                                End If
                                DPATH = DPATH & FolderName & "\"
                            Next
                            If Directory.Exists(DPATH & "\Photos") = False Then
                                Directory.CreateDirectory(DPATH & "\Photos")
                            End If
                            File.Copy(Application.StartupPath & "\ACCOUNTS.mdf", DPATH & "\" & cmbdatabases.Text & ".mdf")
                        End If
                    End If

                End If
                If Val(lbldata.Tag) = 0 Or ChangeServer Then
                    _objcmnbLayer.P_server = txtServer.Text
                    _objcmnbLayer.P_database = cmbdatabases.Text
                    _objcmnbLayer.P_uid = txtusername.Text
                    _objcmnbLayer.P_pwd = txtpassword.Text
                    _objcmnbLayer.P_dataPath = DPATH
                    _objcmnbLayer.clsCnnection()
                    If File.Exists(Application.StartupPath & "\ConString.xml") Then
                        File.Delete(Application.StartupPath & "\ConString.xml")
                    End If
                    _objcmnbLayer.fcheckConnection(_objcmnbLayer)
                    Dim NtPath As String
                    NtPath = "\\" & txtServer.Text & "\" & Trim(Mid(txtpath.Text, 1, 1)) & Trim(Mid(txtpath.Text, txtpath.Text.IndexOf(":") + 2))
                    _objcmnbLayer._saveDatawithOutParm("Update CompanyTb set NDataPath='" & NtPath & "',DataPath='" & txtpath.Text & "',databaseName='" & cmbdatabases.Text & "',Servername='" & txtServer.Text & "'")
                    lbldata.Tag = 1
                End If
            End If
        ElseIf cmbservertype.SelectedIndex = 3 Then
            Dim dt As DataTable
            _objcmnbLayer.P_server = txtServer.Text
            _objcmnbLayer.P_database = cmbdatabases.Text
            dt = _objcmnbLayer._fldDatatable("SELECT Servername,databaseName FROM CompanyTb")
            If dt.Rows.Count > 0 Then
                If Trim(dt(0)("Servername") & "") = "" Or Trim(dt(0)("databaseName") & "") = "" Then
                    Dim NtPath As String
                    NtPath = "\\" & txtServer.Text & "\" & Trim(Mid(txtpath.Text, 1, 1)) & Trim(Mid(txtpath.Text, txtpath.Text.IndexOf(":") + 2))
                    _objcmnbLayer._saveDatawithOutParm("Update CompanyTb set databaseName='" & cmbdatabases.Text & "',Servername='" & txtServer.Text & "'")
                End If
            End If
            lbldata.Tag = 1
        End If
        'CmnClass.CheckDatabase = False
        If ChangeServer Then
            End
        Else
            Me.Close()
        End If

    End Sub

    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcancel.Click
        If ChangeServer = False Then
            If File.Exists(Application.StartupPath & "\Server.txt") = False Then
                If File.Exists(Application.StartupPath & "\ServerChanged.txt") Then
                    File.Copy(Application.StartupPath & "\ServerChanged.txt", Application.StartupPath & "\Server.txt")
                    File.Delete(Application.StartupPath & "\ServerChanged.txt")
                End If
            End If
            End
        Else
            cancelFrm = True
            Me.Close()
        End If
    End Sub

    Private Sub cmbservertype_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbservertype.SelectedIndexChanged
        If chgpgm = True Then Exit Sub
        cmbdatabases.DataSource = Nothing
        cmbdatabases.Items.Clear()
        If chgpgm = True Then Exit Sub
        If cmbservertype.SelectedIndex = 0 Then
            txtServer.Enabled = False
            txtServer.Text = ""
            txtpath.Enabled = True
            cmdBrowse.Enabled = True
        ElseIf cmbservertype.SelectedIndex = 1 Then
            txtServer.Enabled = True
            txtServer.Text = Environment.MachineName
            txtpath.Enabled = True
            cmdBrowse.Enabled = True
        ElseIf cmbservertype.SelectedIndex = 2 Then
            txtServer.Enabled = True
            txtServer.Text = ".\SQLEXPRESS"
            txtpath.Enabled = True
            cmdBrowse.Enabled = True
        Else
            txtServer.Enabled = True
            txtServer.Text = ""
            txtpath.Enabled = False
            cmdBrowse.Enabled = False
        End If
        _objcmnbLayer.P_server = txtServer.Text
        btnsearchDatabase.Enabled = True
    End Sub

    Private Sub ServerFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If chgpgm = True Then chgpgm = False : Exit Sub
        chgpgm = True
        lblstatus.Text = ""
        lblstatus.Visible = True
        txtServer.Text = Environment.MachineName & "\SQL2014"
        cmbservertype.SelectedIndex = 1
        chgpgm = False


    End Sub

    Private Sub cmdBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowse.Click
        FolderBrowserDialog1.ShowDialog()
        If FolderBrowserDialog1.SelectedPath <> "" Then
            txtpath.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub
    Sub LoadDatabases()
        Dim a As String
        a = _objcmnbLayer.P_server
        cmbdatabases.DataSource = Nothing
        _vDrdatabases = _objcmnbLayer.fldDatabases(_objcmnbLayer)
        cmbdatabases.Items.Clear()
        cmbdatabases.Items.Add("")
        Dim i As Integer
        For i = 0 To _vDrdatabases.Rows.Count - 1
            cmbdatabases.Items.Add(_vDrdatabases(i)("name"))
        Next
        'cmbdatabases.DataSource = _vDrdatabases
        'cmbdatabases.DisplayMember = "name"
        'cmbdatabases.ValueMember = "name"
    End Sub



    Private Sub cmbdatabases_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbdatabases.Validated
        If _objcmnbLayer.fldCheckDatabaseName(_objcmnbLayer, cmbdatabases.Text) Then
            isSelectedDatabase = True
        Else
            isSelectedDatabase = False
        End If
    End Sub

    Private Sub valid()
        Try
            If txtServer.Text = "" Or txtusername.Text = "" Or txtpassword.Text = "" Then
                MsgBox("Credential could not be null", MsgBoxStyle.Information)
                txtServer.Focus()
                Exit Sub
            End If
            _objcmnbLayer.P_server = txtServer.Text
            _objcmnbLayer.serverUid = txtusername.Text
            _objcmnbLayer.P_pwd = txtpassword.Text
            LoadDatabases()
            btnsearchDatabase.Enabled = False
            cmbdatabases.Enabled = True
            cmbdatabases.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub btnsearchDatabase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsearchDatabase.Click
        Cursor = Cursors.WaitCursor
        lblstatus.Text = "Searching Existing databases. Please Wait.."
        lblstatus.Refresh()
        valid()
        Cursor = Cursors.Default
        lblstatus.Text = ""
    End Sub

    Private Sub btntestconnection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btntestconnection.Click
        If cmbdatabases.Text = "" Then
            MsgBox("Invalid Database Name", MsgBoxStyle.Exclamation)
            cmbdatabases.Focus()
            Exit Sub
        End If
        If chgpgm = True Then Exit Sub
        If txtusername.Text = "" Then Exit Sub
        If txtServer.Text = "" Then Exit Sub
        If cmbdatabases.Text = "" Then Exit Sub
        If txtpassword.Text = "" Then Exit Sub
        Try
            _objcmnbLayer.P_server = txtServer.Text
            _objcmnbLayer.P_database = cmbdatabases.Text
            _objcmnbLayer.P_uid = txtusername.Text
            _objcmnbLayer.P_pwd = txtpassword.Text
            isSelectedDatabase = True
            lbldata.Tag = ""
            If cmbdatabases.Text <> "System.Data.DataRowView" Then
                If cmbservertype.SelectedIndex <> 3 Then
                    lbldata.Text = _objcmnbLayer.fFilelocation(_objcmnbLayer)
                Else
                    _objcmnbLayer._Connectedp = True
                End If
                If txtpath.Text = "" Then
                    lbldata.Tag = ""
                    Dim dt As DataTable
                    _objcmnbLayer._Connectedp = True
                    If Not _objcmnbLayer.fcheckConnection(_objcmnbLayer) Then
                        MsgBox("Unable to connect Sql Server! Try again", MsgBoxStyle.Critical)
                    End If
                    If cmbservertype.SelectedIndex = 3 Then
                        dt = _objcmnbLayer._fldDatatable("SELECT NDataPath FROM CompanyTb")
                    Else
                        dt = _objcmnbLayer._fldDatatable("SELECT DataPath FROM CompanyTb")
                    End If
                    If dt.Rows.Count > 0 Then
                        lbldata.Text = "Data file: " & Trim(dt(0)(0) & "")
                        txtpath.Text = Trim(dt(0)(0) & "")
                        If txtpath.Text = "" Then
                            lbldata.Tag = ""
                        Else
                            lbldata.Tag = 1
                        End If
                    Else
                        lbldata.Text = "Path not found"
                    End If
                Else
                    lbldata.Text = "Data file: " & lbldata.Text
                    lbldata.Tag = 1
                End If
            Else
                lbldata.Tag = 1
            End If
            MsgBox("Test Connection Success", MsgBoxStyle.Information)
        Catch ex As Exception
            'CmnClass.Connected = False
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub txtServer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtServer.TextChanged, txtpassword.TextChanged, txtusername.TextChanged
        btnsearchDatabase.Enabled = True
    End Sub

    Private Sub cmbdatabases_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbdatabases.SelectedIndexChanged
        btntestconnection.Enabled = True
    End Sub

    Private Sub btnSql_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSql.Click
        If FileExists(Application.StartupPath & "\SQLEXPR_x86_ENU.exe") Then
            If MsgBox("You are going to install SQL SERVER 2014 Database Engine! are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            Shell((Application.StartupPath & "\SQLEXPR_x86_ENU.exe"), AppWinStyle.MinimizedFocus, False, -1)
        Else
            MsgBox("File Not Found", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub cmdcreateDb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcreateDb.Click
        Try
            If cmbdatabases.Text = "" Then
                MsgBox("Invalid Database Name", MsgBoxStyle.Exclamation)
                cmbdatabases.Focus()
                Exit Sub
            End If
            Cursor = Cursors.WaitCursor
            lblstatus.Text = "Database settings is in progress. Please Wait.."
            lblstatus.Refresh()
            createDatabase()
            restoreData(cmbdatabases.Text)
            Cursor = Cursors.Default
            lblstatus.Text = ""
            btntestconnection.Enabled = True
        Catch exception1 As Exception
            Dim exception As Exception = exception1
            MsgBox(exception.Message, MsgBoxStyle.ApplicationModal, Nothing)
            End
        End Try

    End Sub
    Private Sub restoreData(ByVal MyDatabase As String)
        Dim filename As String = (Application.StartupPath & "\data.bak")
        If restoreDb(MyDatabase, filename) Then
            MsgBox("Database restore is successfully completed", MsgBoxStyle.Information, Nothing)
        End If
    End Sub


    Private Function restoreDb(ByVal dbname As String, ByVal filename As String) As Boolean
        Dim flag As Boolean
        Dim str As String = ""
        Dim str2 As String = ""
        Dim str3 As String = ""
        Dim str4 As String = ""
        Dim table As New DataTable
        Dim reader As SqlDataReader = New SqlCommand("RESTORE FILELISTONLY FROM DISK='" & filename & "'", ConnectionMasterTable(True)) With { _
            .CommandType = CommandType.Text}.ExecuteReader
        table.Load(reader)
        If (table.Rows.Count > 0) Then
            str = table.Rows.Item(0).Item(0)
            str2 = table.Rows.Item(1).Item(0)
        End If
        Dim dtset As DataSet = returnDataWithDataAdapter("select name, physical_name from sys.database_files", False)
        If (dtset.Tables.Item(0).Rows.Count > 0) Then
            str3 = dtset.Tables.Item(0).Rows.Item(0).Item(1)
            str4 = dtset.Tables.Item(0).Rows.Item(1).Item(1)
        End If
        Dim command As New SqlCommand("ALTER DATABASE " & dbname & " SET SINGLE_USER With ROLLBACK IMMEDIATE  RESTORE DATABASE " & dbname & _
                                      " FROM DISK='" & filename & "'WITH MOVE '" & str & "' TO '" & str3 & "',MOVE '" & str2 & "' TO '" & str4 & "',REPLACE ALTER DATABASE " & dbname & _
                                      " SET MULTI_USER", ConnectionMasterTable(True))
        Try
            command.ExecuteNonQuery()
            flag = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.ApplicationModal, Nothing)
        End Try
        Return flag
    End Function

    Private Function ConnectionMasterTable(ByVal fromMaster As String) As SqlConnection
        Try
            If ((Not con Is Nothing) AndAlso (con.State = ConnectionState.Open)) Then
                con.Close()
                con = Nothing
            End If
            If fromMaster Then
                con = New SqlConnection("Server=" & txtServer.Text & ";uid=sa;pwd=" & txtpassword.Text & ";database=master;")
            Else
                con = New SqlConnection("Server=" & txtServer.Text & ";uid=sa;pwd=" & txtpassword.Text & ";database=" & cmbdatabases.Text & ";")
            End If
            con.Open()
        Catch ex As Exception
            Interaction.MsgBox((ex.Message & ChrW(13) & ChrW(10) & "Failed to connect the server"), MsgBoxStyle.Critical, Nothing)
        End Try
        Return con
    End Function
    Private Function returnDataWithDataAdapter(ByVal cmdText As String, ByVal isProc As Boolean) As DataSet
        Dim dataSet As New DataSet
        Dim sqa As New SqlDataAdapter
        Try
            Dim selectCommand As New SqlCommand(cmdText, ConnectionMasterTable(False))
            If isProc Then
                selectCommand.CommandType = CommandType.StoredProcedure
            Else
                selectCommand.CommandType = CommandType.Text
            End If
            sqa = New SqlDataAdapter(selectCommand)
            sqa.Fill(dataSet)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return dataSet
    End Function
    Private Sub createDatabase()
        Try
            Dim cmd As SqlCommand
            cmd = New SqlCommand(("Create database " & cmbdatabases.Text), ConnectionMasterTable(True))
            cmd.CommandType = CommandType.Text
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub







End Class