Imports System.IO
Public NotInheritable Class SplashScreen
    Dim _TimeToEnter As Integer = 0
    Private NetServer As Boolean
    'Private WithEvents Frmmain As frmMain
    Private _objCmnblayer As New clsCommon_BL
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'Timer1.Enabled = False
        'If enableBranch Then
        '    BrLogin.Show()
        'Else
        '    Login.Show()
        'End If
        'Me.Close()
        configure()
    End Sub
    Private Sub configure()
        Try
            Dim a(1) As String
            If _TimeToEnter = 1 Then
                Timer1.Enabled = False
                If MainFrm Is Nothing Then
chkagain:
                    If File.Exists(Application.StartupPath & "\ConString.xml") = False Then
                        NetServer = False
                        Dim frmServer As New ServerFrm
                        If _objCmnblayer.P_server <> "" Then
                            frmServer.chgpgm = True
                            frmServer.txtServer.Text = _objCmnblayer.P_server
                            If frmServer.txtServer.Text = ".\SQLEXPRESS" Then
                                frmServer.cmbservertype.SelectedIndex = 2
                            ElseIf frmServer.txtServer.Text = "." Then
                                frmServer.cmbservertype.SelectedIndex = 1
                                frmServer.txtServer.Enabled = False
                            Else
                                frmServer.cmbservertype.SelectedIndex = 3
                                NetServer = True
                            End If
                            If _objCmnblayer.P_database <> "" Then
                                frmServer.cmbdatabases.Text = _objCmnblayer.P_database
                            End If
                            If _objCmnblayer.P_dataPath <> "" Then
                                frmServer.txtpath.Text = _objCmnblayer.P_dataPath
                            End If
                        End If
                        _objCmnblayer.P_server = ""
                        _objCmnblayer.P_dataPath = ""
                        _objCmnblayer.P_database = ""
                        frmServer.ShowDialog()
                        If Val(frmServer.lbldata.Tag) = 1 Then
                            _objCmnblayer._pAttatched = True
                            _objCmnblayer._Connectedp = True
                        Else
                            _objCmnblayer._pAttatched = False
                        End If
                        If frmServer.cancelFrm = True Then End
                        GoTo chkagain
                        frmServer = Nothing
                    Else
                        If FileExists(Application.StartupPath & "\Logo.vin") Then
                            System.IO.File.Delete(DPath & "Logo.vin")
                        End If
                        Dim dt As DataTable
                        _objCmnblayer._Connectedp = True
                        'Dim con As Boolean
                        'con = _objCmnblayer.fcheckConnection(_objCmnblayer)
                        'If bwInitialize.IsBusy Then
                        '    If bwInitialize.WorkerSupportsCancellation Then
                        '        bwInitialize.CancelAsync()
                        '        bwInitialize.Dispose()
                        '    End If
                        'End If
                        Dim dbTb As New DataSet
                        Dim qury As String = "SELECT DB_NAME() AS CurrDB,HOST_NAME() ServerName "
                        qury = qury & " SELECT Servername,username,password,databaseName,DataPath,Ndatapath FROM CompanyTb"
                        qury = qury & " SELECT ProcessCode,isnull(isEnable,0) isEnable FROM SysPara"
                        qury = qury & " SELECT ProcessCode,isnull(isEnable,0) isEnable FROM RestSettingsTb"
                        dbTb = _objCmnblayer._ldDataset(qury, False)
                        If dbTb.Tables(0).Rows.Count > 0 Then
                            MyServer = dbTb.Tables(0)(0)("ServerName")  '
                            MyDatabase = dbTb.Tables(0)(0)("CurrDB")
                        End If
                        dt = dbTb.Tables(1)
                        dtSysPropVal = dbTb.Tables(2)
                        dtSysRestVal = dbTb.Tables(3)
                        If dt.Rows.Count > 0 Then
                            _objCmnblayer.P_server = Trim(dt(0)("Servername") & "")
                            _objCmnblayer.P_dataPath = Trim(dt(0)("DataPath") & "")
                            _objCmnblayer.P_database = Trim(dt(0)("databaseName") & "")
                            MACHINENAME = Environment.MachineName
                            If DPath = "" Then
                                If MACHINENAME = MyServer Then
                                    DPath = _objCmnblayer.P_dataPath
                                Else
                                    If Trim(dt(0)("Ndatapath") & "") <> "" Then
                                        DPath = dt(0)("Ndatapath")
                                    End If
                                End If
                            End If
                        End If
                    End If
                    If _objCmnblayer.P_server = ".\SQLEXPRESS" Then _objCmnblayer._Connectedp = False : _objCmnblayer._pExprs = True

                    DPath = DPath
                    enableBranch = getSysPropVal("ENBR", False)
                    Timer2.Enabled = False

                    If enableBranch Then
                        BrLogin.Show()
                    Else
                        Login.Show()
                    End If
                    Me.Close()
                End If
            End If
            _TimeToEnter = _TimeToEnter + 1
        Catch ex As Exception
            MsgBox(ex.Message & vbCrLf & "Failed to connect the server! Pleas Try Again!")
            End
        End Try
    End Sub
    Private Sub cheEnableBranch()
        Dim dt As DataTable
        dt = _objCmnblayer._fldDatatable("Select ")
    End Sub

    Private Sub SplashScreen_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _TimeToEnter = 1
        'configure()
        Timer1.Enabled = True
        'bwInitialize.RunWorkerAsync()
        'bwInitialize.WorkerSupportsCancellation = True
        Timer2.Enabled = True
        Label3.Text = "Version : " & My.Application.Info.Version.ToString
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        If pbr1.Value < 10 Then
            pbr1.Value = pbr1.Value + 10
        Else
            pbr1.Value = 0
        End If
    End Sub

    Private Sub bwInitialize_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwInitialize.DoWork
        Dim i As Integer
        For i = 1 To 1
            configure()
        Next

    End Sub

    Private Sub bwInitialize_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwInitialize.RunWorkerCompleted
        If enableBranch Then
            BrLogin.Show()
        Else
            Login.Show()
        End If
        Me.Close()
        'Timer2.Enabled = False
        '
        'Timer1.Enabled = True
    End Sub
End Class
