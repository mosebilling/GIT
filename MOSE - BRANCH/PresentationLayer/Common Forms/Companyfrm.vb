Imports System.Net
Imports System.IO

Public Class Companyfrm
    'object declarations
    Dim _objcmnbLayer As New clsCommon_BL
    Dim _objdataCorrection As New clsDataCorrections
    Private _objTr As New clsAccountTransaction
    'Local variable
    Dim chgByPrg As Boolean
    Dim _vdatatableBranch As DataTable
    Dim PreFixTb As New DataTable
    Dim activecontrolname As String
    Private nSelect As Integer
    Private ChgID As Boolean
    Private CompanyTb As New DataTable
    Private dtAccount As DataTable
    Dim dtVoucher As New DataTable
    Dim dtVhr As DataTable
    Private srchIndex As Single
    Private Structure JobAccTp
        Dim Amt As Double
        Dim Job As String
        Dim Acc As Long
    End Structure
    Private JobAcc() As JobAccTp

    'const declaration
    Private Const Constbranch = 0
    Private Const ConstName = 1
    Private Const ConstPrefix = 2
    Private Const ConstVno = 3
    Private Const ConstEanble = 4
    Private Const ConstAc1 = 5
    Private Const ConstAcname1 = 6
    Private Const ConstAc2 = 7
    Private Const ConstAc2name = 8
    Private Const constCtry = 9
    Private Const constOrdno = 10
    Private Const ConstAno1 = 11
    Private Const ConstAno = 12
    Private Const Constid = 13
    Private Const ConsvrTypeNo = 14
    Private Const ConstCategory = 15
    '*******************************
    Private Const ConstOtherCost = 0
    Private Const ConstOtherPayable = 1
    Private Const ConstStock = 2
    Private Const ConstCurrencyReval = 3
    Private Const ConstPurDisc = 4
    Private Const ConstSlsDisc = 5
    Private Const ConstServiceIncome = 6
    Private Const ConstSCash = 7
    Private Const ConstSBank = 8
    Private Const ConstSPDC = 9
    Private Const constCPay = 10
    Private Const ConstBank = 11
    Private Const ConstCostOfSale = 12
    Private Const ConstCostDiff = 13
    Private Const ConstPosCash = 14
    Private Const ConstPosSale = 15
    Private Const ConstPosRtn = 16
    Private Const ConstStockExcess = 17
    Private Const constStockShrtg = 18
    Private Const ConstWage = 19
    Private Const ConstDeduction1 = 20
    Private Const ConstDeduction2 = 21
    Private Const ConstDeduction3 = 22
    Private Const ConstDepreciation = 23
    Private Const ConstChequeBounce = 24
    Private Const ConstMeterialCons = 25
    '*********
    Private hrd(30) As String

    Private Const constOtherInfoCap = 0
    Private Const constOtherInfoVal = 1
    Private Const constOtherInfoId = 2

    Private WithEvents fSelect As Selectfrm
    Dim chgVhr As Boolean
    Dim chgOthrVhr As Boolean
    Dim chgVhrNo As Boolean

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            Const WM_KEYDOWN As Integer = &H100
            Const WM_KEYUP As Integer = &H101
            msg.ToString()
            If msg.Msg = WM_KEYDOWN Or msg.Msg = WM_KEYUP Then
                If msg.WParam.ToInt32() = CInt(Keys.F8) Then
                ElseIf activecontrolname = "grdVchr" Then
                    If msg.WParam.ToInt32() = CInt(Keys.F2) Or msg.WParam.ToInt32() = CInt(Keys.Enter) Then
                        grdVchr_KeyDown(Nothing, New KeyEventArgs(keyData))
                        Return True
                    ElseIf msg.WParam.ToInt32() = CInt(Keys.Space) Then
                        grdVchr.BeginEdit(True)
                    End If
                ElseIf activecontrolname = "grdVoucher" Then
                    If msg.WParam.ToInt32() = CInt(Keys.F2) Or msg.WParam.ToInt32() = CInt(Keys.Enter) Then
                        grdVoucher_KeyDown(Nothing, New KeyEventArgs(keyData))
                        Return True
                    End If
                    chgOthrVhr = True
                ElseIf activecontrolname = "grdOtherInfo" Then
                    If msg.WParam.ToInt32() = CInt(Keys.Enter) Then
                        grdOtherInfo_KeyDown(Nothing, New KeyEventArgs(keyData))
                        Return True
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function
    Private Sub ldPreFix()
        Try
            Dim qry As String
            qry = "Select BrId,[Voucher Name],PreFix,vrNo,Enable,Alias,AccDescr,AL2,ACD2," & _
                  "'Ctry'=CASE WHEN Ctgry=0 THEN 'None'" & _
                  "WHEN Ctgry=1 THEN 'Cash'" & _
                  "WHEN Ctgry=2 THEN 'Card'" & _
                  "WHEN Ctgry=3 THEN 'Credit'" & _
                  "WHEN Ctgry=4 THEN 'Gift'" & _
                  "WHEN Ctgry=5 THEN 'Disc vchr' " & _
                  "WHEN Ctgry=6 THEN 'UPI' END," & _
                  "ordNo," & _
                  " ANo2,ANo,Id,vrTypeNo,Ctgry from PreFixTb" & _
                  " LEFT JOIN AccMast ON AccMast.accid=PreFixTb.ANo" & _
                  " LEFT JOIN (SELECT accid A2,Alias AL2,AccDescr ACD2 FROM AccMast)ANO2 ON ANO2.A2=PreFixTb.ANo2"
            PreFixTb = _objcmnbLayer._fldDatatable(qry & " Where " & IIf(UsrBr = "", "", " BrId IN('" & UsrBr & "','') and") & " VrTypeNo = " & lstVouchers.SelectedIndex + 1)
            If grdVchr.Tag = "Loading" Then Exit Sub
            grdVchr.Tag = "Loading"
            If lstVouchers.SelectedIndex < 0 Then lstVouchers.SelectedIndex = 0
            If PreFixTb.Rows.Count > 0 Then
                grdVchr.DataSource = PreFixTb
                setGridHead()
            Else
                PreFixTb.Rows.Clear()
                grdVchr.DataSource = PreFixTb
                setGridHead()
            End If
            lstVouchers.Enabled = True
            grdVchr.Tag = ""
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub setGridHeadOtherInfo()
        Try
            chgByPrg = True
            With grdOtherInfo
                SetGridEditProperty(grdOtherInfo)
                .ColumnCount = 3

                .Columns(constOtherInfoCap).HeaderText = "Caption"
                .Columns(constOtherInfoCap).Width = 150
                .Columns(constOtherInfoVal).HeaderText = "Value"
                .Columns(constOtherInfoVal).Width = 150
                .Columns(constOtherInfoId).Visible = False
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub
    Private Sub setGridHead()
        Try
            chgByPrg = True
            With grdVchr
                .ColumnHeadersVisible = True
                .RowHeadersVisible = False
                .AllowUserToAddRows = False
                .AllowUserToDeleteRows = False
                .AllowUserToResizeRows = False
                .AllowUserToResizeColumns = True
                .EditMode = DataGridViewEditMode.EditProgrammatically
                .ColumnHeadersHeight = 30
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                '.ColumnHeadersDefaultCellStyle.BackColor = Color.Olive
                .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.5!, System.Drawing.FontStyle.Regular)
                .RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.5!)

                '.ColumnCount = 15
                Dim cmb As New DataGridViewComboBoxColumn
                cmb.Items.Add("")
                For i = 0 To _vdatatableBranch.Rows.Count - 1
                    cmb.Items.Add(_vdatatableBranch(i)(0))
                Next
                cmb.HeaderText = "Branch"
                cmb.DataPropertyName = "BrId"
                .Columns.RemoveAt(Constbranch)
                .Columns.Insert(Constbranch, cmb)
                .Columns(Constbranch).Width = 100
                '.Columns(Constbranch).Frozen = True
                .Columns(Constbranch).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(Constbranch).Visible = enableBranch


                .Columns(ConstName).HeaderText = "Voucher Name"
                .Columns(ConstName).Width = 100 '100
                .Columns(ConstName).SortMode = DataGridViewColumnSortMode.NotSortable
                '.Columns(ConstName).Frozen = True

                .Columns(ConstPrefix).HeaderText = "Pre-Fix"
                .Columns(ConstPrefix).Width = 50
                .Columns(ConstPrefix).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(ConstName).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft


                .Columns(ConstVno).HeaderText = "Vr. No."
                .Columns(ConstVno).Width = 50
                .Columns(ConstVno).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(ConstVno).DefaultCellStyle.Format = "N0"

                .Columns(ConstEanble).HeaderText = "Enable"
                .Columns(ConstEanble).Width = 50
                .Columns(ConstEanble).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(ConstEanble).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                'AddCheckBoxColumns(grdVchr, 4, "E")

                .Columns(ConstAc1).HeaderText = "First A/c"
                .Columns(ConstAc1).Width = 50
                .Columns(ConstAc1).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(ConstAc1).Visible = False

                .Columns(ConstAcname1).HeaderText = "First A/c. Name"
                .Columns(ConstAcname1).Width = 150
                .Columns(ConstAcname1).SortMode = DataGridViewColumnSortMode.NotSortable

                .Columns(ConstAc2).HeaderText = "II A/c. Id"
                .Columns(ConstAc2).Width = 70
                .Columns(ConstAc2).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(ConstAc2).Visible = False

                .Columns(ConstAc2name).HeaderText = "Second A/C Name"
                .Columns(ConstAc2name).Width = 150
                .Columns(ConstAc2name).SortMode = DataGridViewColumnSortMode.NotSortable


                cmb = New DataGridViewComboBoxColumn
                cmb.Items.Add("None")
                cmb.Items.Add("Cash")
                cmb.Items.Add("Card")
                cmb.Items.Add("Credit")
                cmb.Items.Add("Gift")
                cmb.Items.Add("Disc vchr")
                cmb.Items.Add("UPI")
                cmb.DataPropertyName = "Ctry"
                '.Columns(ConstCategory).HeaderText = "Category"
                cmb.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
                .Columns.RemoveAt(constCtry)
                .Columns.Insert(constCtry, cmb)
                .Columns(constCtry).HeaderText = "Category"
                .Columns(constCtry).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(constCtry).Width = 60 '40

                .Columns(constOrdno).HeaderText = "OrdNo"
                .Columns(constOrdno).Width = 50

                .Columns(ConstAno1).HeaderText = "ANo1"
                .Columns(ConstAno1).Visible = False

                .Columns(ConstAno).HeaderText = "ANo"
                .Columns(ConstAno).Visible = False

                .Columns(Constid).HeaderText = "ID"
                .Columns(Constid).Visible = False

                .Columns(ConsvrTypeNo).HeaderText = "vrTypeNo"
                .Columns(ConsvrTypeNo).Visible = False

                .Columns(ConstCategory).HeaderText = "Cate"
                .Columns(ConstCategory).Visible = False

            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        chgByPrg = False
    End Sub
    Private Sub setGridHeadAccounts()

        Try

            chgByPrg = True
            With grdVoucher
                .ColumnHeadersVisible = True
                .RowHeadersVisible = False
                .AllowUserToAddRows = False
                .AllowUserToDeleteRows = False
                .AllowUserToResizeRows = False
                .AllowUserToResizeColumns = True
                .EditMode = DataGridViewEditMode.EditProgrammatically
                .ColumnHeadersHeight = 28
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .ColumnHeadersDefaultCellStyle.BackColor = Color.Olive
                .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.5!, System.Drawing.FontStyle.Regular)
                '.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.0!)
                .ColumnCount = 2

                .Columns(ConstOtherCost).HeaderText = "Parameter"
                .Columns(ConstOtherCost).Width = 200 '100
                .Columns(ConstOtherCost).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(ConstOtherCost).DefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.5!, System.Drawing.FontStyle.Regular)
                .Columns(ConstOtherCost).ReadOnly = True

                .Columns(ConstOtherPayable).HeaderText = "Accounts"
                .Columns(ConstOtherPayable).Width = 150
                .Columns(ConstOtherPayable).SortMode = DataGridViewColumnSortMode.NotSortable


            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        chgByPrg = False
    End Sub
    Private Sub LodMaster()
        _vdatatableBranch = _objcmnbLayer._fldDatatable("SELECT Branchcode FROM BranchTb")
        Dim i As Integer
        cmbBr.Items.Clear()
        If UsrBr = "" Then
            cmbBr.Items.Add("")
            For i = 0 To _vdatatableBranch.Rows.Count - 1
                cmbBr.Items.Add(_vdatatableBranch(i)("Branchcode"))
            Next
        Else
            Dim dt As DataTable
            dt = _objcmnbLayer._fldDatatable("SELECT Branchcode FROM UsrBr " & _
                                             "LEFT JOIN BranchTb ON UsrBr.BrId=BranchTb.Branchcode " & _
                                             "LEFT JOIN UserTb ON UsrBr.UsrId=UserTb.Id " & _
                                             "WHERE UserId='" & CurrentUser & "'")
            For i = 0 To dt.Rows.Count - 1
                cmbBr.Items.Add(dt(i)("Branchcode"))
            Next
        End If

    End Sub
    Private Sub addAccounts()
        hrd(0) = "Stock"
        hrd(1) = "PurDisc(Cr)"
        hrd(2) = "SlsDisc(Dr)"
        hrd(3) = "Service Income"
        hrd(4) = "Service Cash"
        hrd(5) = "Service Bank"
        hrd(6) = "Service PDC"
        hrd(7) = "Bank(PDC Tr.)"
        hrd(8) = "Cost of Sale"
        hrd(9) = "Cost Diff."
        hrd(10) = "POS Cash"
        hrd(11) = "POS Sales"
        hrd(12) = "POS SReturn"
        hrd(13) = "Stock Excess"
        hrd(14) = "Stock Shrtg"
        hrd(15) = "Depreciation"
        hrd(16) = "Chq Bounce A/C"
        hrd(17) = "Meterial Consumptions"
        hrd(18) = "Commission A/C"
        hrd(19) = "Tax Income A/C"
        hrd(20) = "Tax Expense A/C"
        hrd(21) = "Card Payment A/C"
        hrd(22) = "Salary A/C"
        hrd(23) = "Vat Output"
        hrd(24) = "Vat Input"
        hrd(25) = "Input On Import"
        hrd(26) = "Output On Import"
        hrd(27) = "SR Deduction A/C"
        hrd(28) = "UPI Payment A/C"
        dtAccount = _objcmnbLayer._fldDatatable("SELECT Alias,AccSetId FROM AccMast where isnull(AccSetId,'')<>''")
        Dim i As Short
        With grdVoucher
            .Rows.Clear()
            For i = 0 To 28
                .Rows.Add()
                .Item(0, .Rows.Count - 1).Value = hrd(i)
                .Item(1, .Rows.Count - 1).Value = ldAlias(i + 1)
            Next
        End With
    End Sub
    Private Function ldAlias(ByVal _row As Short) As String
        Dim _qry = From job In dtAccount.AsEnumerable() Where job("AccSetId").ToString.ToUpper.Contains(Format(_row, "00")) Select New With _
                      {.Name = job!Alias}
        For Each itm In _qry
            Return itm.Name
            Exit For
        Next
        Return ""
        'dtAccount.Select("AccSetId like %" & Format(_row, "00") & "%")
    End Function

    Private Sub Companyfrm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        picLogo.Image = Nothing
        picLogo.Dispose()
        If Not fSelect Is Nothing Then fSelect.Close() : fSelect = Nothing
    End Sub

    Private Sub Companyfrm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Try
        LodMaster()
        ldVoucherno()
        lstVouchers.SelectedIndex = 0
        loadLocation()
        LodCurrency()
        ldState()
        LdCompDet()
        setGridHeadAccounts()
        setGridHeadOtherInfo()
        addAccounts()
        loadAddInfoCap()
        loadCompanyOtherInfo()
        loadfinancedata()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select gst from GstDefaultSetTb order by gst")
        If dt.Rows.Count > 0 Then
            cmbgstslab.Items.Clear()
            Dim i As Integer
            For i = 0 To dt.Rows.Count - 1
                cmbgstslab.Items.Add(dt(i)("gst"))
            Next
        End If
        If (PublicVariables.CurrentUser = "PROGRAMMAR") Then
            txtname.ReadOnly = False
            btnmodule.Visible = True
            btnRptToxml.Visible = True
            Label13.Visible = True
            'chkparameter.Visible = True
        Else
            txtname.ReadOnly = True
            btnmodule.Visible = False
            btnRptToxml.Visible = False
            Label13.Visible = False
            'chkparameter.Visible = False
        End If
        Dim lgnotFound As Boolean
        If Not FileExists(Application.StartupPath & "\Logo.vin") Then
            If FileExists(DPath & "Logo.vin") Then
                FileCopy(DPath & "Logo.vin", Application.StartupPath & "\Logo.vin")
            Else
                lgnotFound = True
            End If
        End If
        If lgnotFound Then
            LdPic(picLogo, Application.StartupPath & "\mLogo.vin", Me)
        Else
            LdPic(picLogo, Application.StartupPath & "\Logo.vin", Me)
        End If
        If CurrentUser = "PROGRAMMAR" Then
            txtname.ReadOnly = False
            chkisDXB.Visible = True
        Else
            txtname.ReadOnly = True
            chkisDXB.Visible = False
        End If
        ldSysPara()
        'fMainForm.Panel1.Visible = False
        plgstset.Visible = EnableGST
        pltemple.Visible = enableTemple
        plgst.Visible = EnableGST
        cmbgstslab.SelectedIndex = 0
        tables()
        If enablecess Then
            plcess.Visible = True
        Else
            plcess.Visible = False
        End If
        If enableGCC Then
            Label22.Text = "Emirate"
        End If
        btnsms.Visible = enableSMS
        lblCap8.Visible = enableBranch
        cmbBr.Visible = enableBranch
        btnsetBranchAcc.Visible = enableBranch
        chgByPrg = True
        cmbBr.Text = UsrBr
        chgByPrg = False
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try

    End Sub
    Private Sub loadLocation()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT LocCode from LocationTb")
        Dim i As Integer
        cmbDloc.Items.Add("")
        cmbJLoc.Items.Add("")
        For i = 0 To dt.Rows.Count - 1
            cmbDloc.Items.Add(dt(i)(0))
            cmbJLoc.Items.Add(dt(i)(0))
        Next
    End Sub
    Private Sub UpdateVouchers()
        Try
            If chgVhr Then
                Dim qry As String
                Dim ctry As Integer
                qry = "SELECT BrId,[Voucher Name],PreFix,vrNo,Enable,Ctgry,ANo2,ANo,Id,vrTypeNo,ordNo FROM PreFixTb"
                Dim i As Integer
                For i = 0 To grdVchr.Rows.Count - 1
                    Select Case grdVchr.Item(9, i).Value
                        Case "None"
                            ctry = 0
                        Case "Cash"
                            ctry = 1
                        Case "Card"
                            ctry = 2
                        Case "Credit"
                            ctry = 3
                        Case "Gift"
                            ctry = 4
                        Case "Disc Vchr"
                            ctry = 5
                        Case "UPI"
                            ctry = 6
                        Case Else
                            ctry = 0
                    End Select
                    PreFixTb(i)("vrTypeNo") = lstVouchers.SelectedIndex + 1
                    PreFixTb(i)("Ctgry") = ctry
                    If IsDBNull(PreFixTb(i)("Brid")) Then
                        PreFixTb(i)("Brid") = ""
                    End If
                    If IsDBNull(PreFixTb(i)("PreFix")) Then
                        PreFixTb(i)("PreFix") = ""
                    End If
                    If IsDBNull(PreFixTb(i)("ANo")) Or Trim(grdVchr.Item(ConstAcname1, i).Value & "") = "" Then
                        PreFixTb(i)("ANo") = 0
                    End If
                    If IsDBNull(PreFixTb(i)("ANo2")) Or Trim(grdVchr.Item(ConstAc2name, i).Value & "") = "" Then
                        PreFixTb(i)("ANo2") = 0
                    End If
                    If IsDBNull(PreFixTb(i)(ConstName)) Then
                        MsgBox("Invalid Voucher Name!", MsgBoxStyle.Critical)
                        grdVchr.Select()
                        grdVchr.CurrentCell = grdVchr.Item(i, ConstName)
                        Exit Sub
                    End If
                Next
                _objcmnbLayer.__saveDataTable(qry, PreFixTb)
                ldPreFix()
            End If
            If chgOthrVhr Then
                saveAccset()
            End If
            'btnset.Enabled = False
            chgOthrVhr = False
            chgVhr = False
            chgVhrNo = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRemove.Click
        If BtnRemove.Enabled = False Then Exit Sub
        With grdVchr
            If .Rows.Count = 0 Then Exit Sub
            If MsgBox("You are going to REMOVE one record. Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Warning...") = MsgBoxResult.No Then Exit Sub
            If Val(.Item("ID", .CurrentRow.Index).Value & "") > 0 Then
                _objcmnbLayer._saveDatawithOutParm("DELETE FROM PreFixTb WHERE ID=" & Val(.Item("ID", .CurrentRow.Index).Value))
            End If
            .Rows.RemoveAt(.CurrentRow.Index)
        End With
    End Sub

    Private Sub BtnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAdd.Click
        AddRowgrdVchr()
    End Sub
    Sub AddRowgrdVchr()
        Dim _vRw As DataRow
        _vRw = PreFixTb.NewRow()
        _vRw(constCtry) = "None"
        PreFixTb.Rows.Add(_vRw)
        With grdVchr
            .DataSource = Nothing
            .DataSource = PreFixTb
            setGridHead()
            .Select()
            .CurrentCell = .Item(1, .Rows.Count - 1)
            activecontrolname = "grdVchr"
            .BeginEdit(True)
        End With
    End Sub
    Private Sub addOtherInfoRow()
        With grdOtherInfo
            .Rows.Add()
            .Select()
            .CurrentCell = .Item(0, .Rows.Count - 1)
            activecontrolname = "grdOtherInfo"
            .BeginEdit(True)
        End With
    End Sub

    Private Sub grdVchr_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVchr.CellClick
        'If e.ColumnIndex = Constbranch Or e.ColumnIndex = constCtry Then grdVchr.BeginEdit(True)
        If grdVchr.RowCount = 0 Then Exit Sub
        grdVchr.BeginEdit(True)

    End Sub

    Private Sub grdVchr_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVchr.CellDoubleClick
        grdVchr.BeginEdit(True)
    End Sub

    Private Sub grdVchr_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVchr.GotFocus
        activecontrolname = "grdVchr"
    End Sub

    Private Sub grdVchr_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdVchr.KeyDown
        With grdVchr
            If e.KeyCode = Keys.Enter Then
                If FindNextCell(grdVchr, .CurrentCell.RowIndex, .CurrentCell.ColumnIndex + 1) Then
                    AddRowgrdVchr()
                End If
                chgByPrg = True
                .BeginEdit(True)
                chgByPrg = False
            ElseIf e.KeyCode = Keys.F2 Then
                Select Case .CurrentCell.ColumnIndex
                    Case ConstAcname1, ConstAc1, ConstAc2, ConstAc2name
                        Select Case lstVouchers.SelectedIndex
                            Case 0
                                ldSelect(16)
                            Case 1
                                ldSelect(15)
                            Case 3, 4
                                ldSelect(2)
                            Case 6
                                ldSelect(7)
                            Case Else
                                ldSelect(1000)
                        End Select

                End Select
            End If
        End With
    End Sub

    Private Sub fSelect_doSelect(ByVal KeyId As String, ByVal strFld1 As String, ByVal strFld2 As String) Handles fSelect.doSelect
        chgByPrg = True
        If srchIndex > 0 Then
            Select Case srchIndex
                Case 1
                    txtCGSTac.Text = strFld1
                    txtCGSTac.Tag = KeyId
                    txtCGSTac.Focus()
                Case 2
                    txtpaymentac.Text = strFld1
                    txtpaymentac.Tag = KeyId
                    txtpaymentac.Focus()
                Case 3
                    txtIgst.Text = strFld1
                    txtIgst.Tag = KeyId
                    txtIgst.Focus()
                Case 4
                    txtcgstPac.Text = strFld1
                    txtcgstPac.Tag = KeyId
                    txtcgstPac.Focus()
                Case 5
                    txtSGSTPac.Text = strFld1
                    txtSGSTPac.Tag = KeyId
                    txtSGSTPac.Focus()
                Case 6
                    txtIGSTPac.Text = strFld1
                    txtIGSTPac.Tag = KeyId
                    txtIGSTPac.Focus()
            End Select
            srchIndex = 0
        Else
            If activecontrolname = "grdVchr" Then
                With grdVchr
                    Select Case .CurrentCell.ColumnIndex
                        Case ConstAc1, ConstAcname1
                            .Item(ConstAc1, .CurrentRow.Index).Value = strFld2
                            .Item(ConstAcname1, .CurrentRow.Index).Value = strFld1
                            .Item(ConstAno, .CurrentRow.Index).Value = KeyId
                        Case ConstAc2, ConstAc2name
                            .Item(ConstAc2, .CurrentRow.Index).Value = strFld2
                            .Item(ConstAc2name, .CurrentRow.Index).Value = strFld1
                            .Item(ConstAno1, .CurrentRow.Index).Value = KeyId
                    End Select
                End With
                chgVhr = True
            ElseIf activecontrolname = "grdVoucher" Then
                With grdVoucher
                    .Item(1, .CurrentRow.Index).Value = strFld2
                    chgOthrVhr = True
                End With
            End If
        End If
        chgByPrg = False
    End Sub

    Private Sub lstVouchers_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstVouchers.SelectedIndexChanged
        ldPreFix()
    End Sub

    Private Sub grdVchr_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVchr.Leave
        activecontrolname = ""
    End Sub


    Private Sub grdVchr_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdVchr.EditingControlShowing
        chgVhr = True
    End Sub


    Public Sub AddCheckBoxColumns(ByRef dvGrid As DataGridView, ByVal colIndex As Integer, ByVal HeaderText As String)
        Dim CheckBoxColumn As New DataGridViewCheckBoxColumn
        dvGrid.Columns.RemoveAt(colIndex)
        With CheckBoxColumn
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            .FlatStyle = FlatStyle.Standard
            .HeaderText = HeaderText
            .CellTemplate = New DataGridViewCheckBoxCell()
            .CellTemplate.Style.BackColor = Color.Beige
        End With
        dvGrid.Columns.Insert(colIndex, CheckBoxColumn)
    End Sub
    Private Sub BtnCancelPara_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If ChgID Then
            If MsgBox("Changes Found ! Do You Want to Close ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = vbNo Then Exit Sub
        End If
        Close()
    End Sub

    Private Sub BtnCancelVchr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancelVchr.Click
        If ChgID Then
            If MsgBox("Changes Found ! Do You Want to Close ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = vbNo Then Exit Sub
        End If
        Close()
    End Sub

    Private Sub BtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        Try
            If ChgID Then
                If MsgBox("Changes Found ! Do You Want to Close ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = vbNo Then Exit Sub
            End If
            Me.Close()
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub txtaddr1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtaddr1.KeyDown, txtaddr2.KeyDown, txtaddr3.KeyDown, _
                                                                                                                txtaddr4.KeyDown, txtphone.KeyDown, txtFax.KeyDown, txtEmail.KeyDown, txtWeb.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")

        End If
    End Sub

    Private Sub txtname_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtname.TextChanged, txtaddr1.TextChanged, txtaddr2.TextChanged, txtaddr3.TextChanged, txtaddr4.TextChanged, txtEmail.TextChanged, txtFax.TextChanged, txtname.TextChanged, txtphone.TextChanged, txtphone.TextChanged, txtWeb.TextChanged
        If chgByPrg Then Exit Sub
        btnUpdate.Enabled = True
        ChgID = True
    End Sub
    Private Sub LdCompDet()

        CompanyTb = _objcmnbLayer._fldDatatable("SELECT * FROM CompanyTb")
        chgByPrg = True
        If CompanyTb.Rows.Count > 0 Then
            txtname.Text = Trim("" & CompanyTb(0)("compName"))
            txtaddr1.Text = Trim("" & CompanyTb(0)("Addr1"))
            txtaddr2.Text = Trim("" & CompanyTb(0)("Addr2"))
            txtaddr3.Text = Trim("" & CompanyTb(0)("Addr3"))
            txtaddr4.Text = Trim("" & CompanyTb(0)("Addr4"))
            txtphone.Text = Trim("" & CompanyTb(0)("Tel1"))
            txtFax.Text = Trim("" & CompanyTb(0)("Tel2"))
            txtEmail.Text = Trim("" & CompanyTb(0)("Email"))
            txtTerms.Text = Trim(CompanyTb(0)("quoteTerms") & "")
            cmbseconddb.Text = Trim(CompanyTb(0)("secondDB") & "")
            txtWeb.Text = Trim("" & CompanyTb(0)("WWW"))
            cmbcosting.SelectedIndex = Val("" & CompanyTb(0)("CostingMethod"))
            If Trim("" & CompanyTb(0)("bankDetails")) <> "" Then
                txtbank.Text = Trim("" & CompanyTb(0)("bankDetails"))
            End If
            txtbartender.Text = Trim("" & CompanyTb(0)("bartenderpath"))
            txtlastitemcode.Text = Trim("" & CompanyTb(0)("LastAutomatedItemCodeFromPurchase"))
            firstDateFromToday = Val(CompanyTb(0)("firstDateFromToday") & "")
            txtfirstdateBefore.Text = firstDateFromToday
            If Not IsDBNull(CompanyTb(0)("AccPeriodFrm")) Then
                cldrdate.Value = CompanyTb(0)("AccPeriodFrm")
            Else
                cldrdate.Value = Date.Now
            End If

            If Not IsDBNull(CompanyTb(0)("AccPeriodTo")) Then
                cldrdate1.Value = CompanyTb(0)("AccPeriodTo")
            Else
                cldrdate1.Value = Date.Now
            End If

            If Not IsDBNull(CompanyTb(0)("cessdate")) Then
                dtpcessdate.Value = CompanyTb(0)("cessdate")
            Else
                dtpcessdate.Value = DateValue("01/01/1950")
            End If
            txtnetworkpath.Text = Trim("" & CompanyTb(0)("NDataPath"))
            txtdocumentpath.Text = DPath
            txtTin.Text = Trim(CompanyTb(0)("TINNO") & "")
            txtcst.Text = Trim(CompanyTb(0)("CST") & "")
            cmbcurrency.Text = Trim(CompanyTb(0)("BasicCurrencyId") & "")
            cmbDloc.Text = Trim(CompanyTb(0)("DefLoc") & "")
            cmbJLoc.Text = Trim(CompanyTb(0)("JobLoc") & "")
            txtserialAlert.Text = Val(CompanyTb(0)("SerialAlertDays") & "")
            txtgstnumber.Text = Trim(CompanyTb(0)("GSTN") & "")
            cmbstate.Text = Trim(CompanyTb(0)("statecode") & "")
            txtdecimal.Text = Val(CompanyTb(0)("NoOfDecimal") & "")
            cmbISnextline.SelectedIndex = Val(CompanyTb(0)("ISNextLineOn") & "")
            If Not IsDBNull(CompanyTb(0)("LastBkdDt")) Then
                lblbkdate.Text = "Last Backup Date : " & CompanyTb(0)("LastBkdDt")
            Else
                lblbkdate.Text = "Last Backup Date :"
            End If
            If Val(CompanyTb(0)("YearFeesTemple") & "") = 0 Then CompanyTb(0)("YearFeesTemple") = 0
            txtmonthlyfees.Text = Format(CDbl(CompanyTb(0)("YearFeesTemple")), numFormat)
            If Not IsDBNull(CompanyTb(0)("withNonTaxBill")) Then
                chknontax.Checked = CompanyTb(0)("withNonTaxBill")
            End If
            If Val(CompanyTb(0)("EntrygridFontSize") & "") = 0 Then CompanyTb(0)("EntrygridFontSize") = 0
            txtfontsize.Text = Format(CDbl(CompanyTb(0)("EntrygridFontSize")), numFormat)
            cmbgrtr.SelectedIndex = Val(CompanyTb(0)("roundoffGtrThn50") & "")
            cmbless.SelectedIndex = Val(CompanyTb(0)("roundoffLessThn50") & "")
            If Not IsDBNull(CompanyTb(0)("isdxb")) Then
                chkisDXB.Checked = CompanyTb(0)("isdxb")
            End If
            If Not IsDBNull(CompanyTb(0)("cessenddate")) Then
                dtpcessenddate.Value = DateValue(CompanyTb(0)("cessenddate"))
            Else
                dtpcessenddate.Value = DateValue("01/01/1950")
            End If
            txtftpurl.Text = Trim("" & CompanyTb(0)("ftpurl"))
            txtftpusername.Text = Trim("" & CompanyTb(0)("ftpusername"))
            txtftppassword.Text = Trim(CompanyTb(0)("ftppassword") & "")
        End If
        chgByPrg = False
        'btnUpdate.Enabled = False
    End Sub
    'Private Sub ldpic()
    '    If .FileName <> "" Then
    '        Err.Clear()
    '        On Error Resume Next
    '        Dim bm As New Bitmap(.FileName)
    '        picLogo.Image = bm
    '        If Err.Number Then
    '            MsgBox(Err.Description)
    '        Else
    '            picLogo.Tag = .FileName
    '            btnUpdate.Enabled = True
    '            ChgID = True
    '        End If
    '    End If
    'End Sub
    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        With DlgOpen
            .Filter = "All Picture Files|*.bmp;*.dib;*.gif;*.wmf;*.emf;*.jpg;*.ico;*.cur|" & _
                     "Bitmaps(*.bmp,*.dib)|*.bmp;*.dib|" & _
                     "Gif Images(*.gif)|*.gif|" & _
                     "JPEG Images(*.jpg)|*.jpg|" & _
                     "Matafiles(*.wmf,*.emf)|*.wmf;*.emf|" & _
                     "Icons(*.ico,*.cur)|*.ico;*.cur|" & _
                     "All Files(*.*)|*.*"
            .Title = "Select an Image file"
            .FileName = ""
            .ShowDialog()
            If .FileName <> "" Then
                Err.Clear()
                On Error Resume Next
                Dim bm As New Bitmap(.FileName)
                picLogo.Image = bm
                If Err.Number Then
                    MsgBox(Err.Description)
                Else
                    picLogo.Tag = .FileName
                    btnUpdate.Enabled = True
                    ChgID = True
                End If
            End If
            ChDir(Application.StartupPath)
        End With
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        SaveModi()
    End Sub
    Private Sub SaveModi()
        If ChgID Then
            CompanyTb(0)("compName") = Trim(txtname.Text)
            CompanyTb(0)("Addr1") = Trim(txtaddr1.Text)
            CompanyTb(0)("Addr2") = Trim(txtaddr2.Text)
            CompanyTb(0)("Addr3") = Trim(txtaddr3.Text)
            CompanyTb(0)("Addr4") = Trim(txtaddr4.Text)
            CompanyTb(0)("Tel1") = Trim(txtphone.Text)
            CompanyTb(0)("Tel2") = Trim(txtFax.Text)
            CompanyTb(0)("Email") = Trim(txtEmail.Text)
            CompanyTb(0)("WWW") = Trim(txtWeb.Text)
            CompanyTb(0)("AccPeriodFrm") = DateValue(cldrdate.Value)
            CompanyTb(0)("AccPeriodTo") = DateValue(cldrdate1.Value)
            CompanyTb(0)("DataPath") = Trim(txtdocumentpath.Text)
            CompanyTb(0)("NDataPath") = Trim(txtnetworkpath.Text)
            CompanyTb(0)("TINNO") = Trim(txtTin.Text)
            CompanyTb(0)("CST") = Trim(txtcst.Text)
            CompanyTb(0)("BasicCurrencyId") = Trim(cmbcurrency.Text)
            CompanyTb(0)("DefLoc") = Trim(cmbDloc.Text)
            CompanyTb(0)("JobLoc") = Trim(cmbJLoc.Text)
            CompanyTb(0)("SerialAlertDays") = Val(txtserialAlert.Text)
            CompanyTb(0)("statecode") = Trim(cmbstate.Text)
            CompanyTb(0)("GSTN") = Trim(txtgstnumber.Text)
            CompanyTb(0)("NoOfDecimal") = Val(txtdecimal.Text)
            If Val(txtmonthlyfees.Text) = 0 Then txtmonthlyfees.Text = 0
            CompanyTb(0)("YearFeesTemple") = CDbl(txtmonthlyfees.Text)
            CompanyTb(0)("ISNextLineOn") = cmbISnextline.SelectedIndex
            CompanyTb(0)("cessdate") = DateValue(dtpcessdate.Value)
            CompanyTb(0)("withNonTaxBill") = chknontax.Checked
            CompanyTb(0)("bartenderpath") = txtbartender.Text
            CompanyTb(0)("EntrygridFontSize") = Val(txtfontsize.Text)
            CompanyTb(0)("LastAutomatedItemCodeFromPurchase") = txtlastitemcode.Text
            CompanyTb(0)("roundoffGtrThn50") = cmbgrtr.SelectedIndex
            CompanyTb(0)("roundoffLessThn50") = cmbless.SelectedIndex
            CompanyTb(0)("bankDetails") = txtbank.Text
            CompanyTb(0)("quoteTerms") = txtTerms.Text
            CompanyTb(0)("secondDB") = cmbseconddb.Text
            CompanyTb(0)("CostingMethod") = cmbcosting.SelectedIndex
            CompanyTb(0)("cessenddate") = DateValue(dtpcessenddate.Value)
            CompanyTb(0)("payrollalertDays") = Val(txtpayrollalert.Text)
            CompanyTb(0)("firstDateFromToday") = Val(txtfirstdateBefore.Text)
            CompanyTb(0)("ftpurl") = Trim(txtftpurl.Text & "")
            CompanyTb(0)("ftpusername") = Trim(txtftpusername.Text & "")
            CompanyTb(0)("ftppassword") = Trim(txtftppassword.Text & "")

            _objcmnbLayer.__saveDataTable("SELECT cid,compName,Addr1,Addr2,Addr3,Addr4,Tel1,Tel2,Email,WWW,AccPeriodFrm,AccPeriodTo,BasicCurrencyId," & _
                                          "NoOfDecimal,DataPath,NDataPath,TINNO,CST,DefLoc,JobLoc,SerialAlertDays,statecode,GSTN,YearFeesTemple,ISNextLineOn," & _
                                          "cessdate,withNonTaxBill,bartenderpath,EntrygridFontSize,LastAutomatedItemCodeFromPurchase," & _
                                          "roundoffGtrThn50,roundoffLessThn50,bankDetails,quoteTerms,secondDB,CostingMethod,cessenddate,payrollalertDays," & _
                                          "firstDateFromToday,ftpurl,ftpusername,ftppassword FROM CompanyTb", CompanyTb)
            'If MACHINENAME = MyServer Then
            '    _objcmnbLayer._saveDatawithOutParm("Update CompanyTb set DataPath='" & MkDbSrchStr(txtdocumentpath.Text) & "',NDataPath='" & MkDbSrchStr(txtdocumentpath.Tag) & "'")
            'Else
            '    _objcmnbLayer._saveDatawithOutParm("Update CompanyTb set DataPath='" & MkDbSrchStr(txtdocumentpath.Tag) & "',NDataPath='" & MkDbSrchStr(txtdocumentpath.Text) & "'")
            'End If
            If picLogo.Tag <> "" Then
                'On Error Resume Next
                If DPath = "" Or DPath = "\" Then DPath = txtdocumentpath.Text & "\"
                If FileExists(DPath & "Logo.vin") Then
                    System.IO.File.Delete(DPath & "Logo.vin")
                End If
                FileCopy(picLogo.Tag, DPath & "Logo.vin")
                'If Not Err.Number Then
                '    Dim bm As Bitmap = picLogo.Image
                '    bm.Save(DPath & "Logo.vin", System.Drawing.Imaging.ImageFormat.Jpeg)
                'End If
                If Err.Number Then
                    If MsgBox(Err.Description, vbRetryCancel + vbExclamation) = vbRetry Then Resume
                End If
            End If
        End If
        ChgID = False
        UpdateVouchers()
        saveVhrNo()
        If chkparameter.Tag <> "" Then updateParameter()
        dtSysPropVal = _objcmnbLayer._fldDatatable("SELECT ProcessCode,isnull(isEnable,0) isEnable FROM SysPara")
        SetSystemProperties()
        'SetCompPara()

        _objcmnbLayer._saveDatawithOutParm("UPDATE companytb set isDXB=" & IIf(chkisDXB.Checked, 1, 0))
        setExtraPara(CompanyTb, False)
        MsgBox("You should Restart Software to effect the changes of System Parameters.", vbInformation)
        If cmbDloc.Text <> "" Then
            Dim strquery As String = "UPDATE ItmInvCmnTb set DocDefLoc='" & cmbDloc.Text & "' where isnull(DocDefLoc,'')=''"
            strquery = strquery & vbCrLf & "UPDATE DocCmnTb set DocDefLoc='" & cmbDloc.Text & "' where isnull(DocDefLoc,'')=''"
            strquery = strquery & vbCrLf & "UPDATE DocCmnTb set FromLoc='" & cmbDloc.Text & "' where isnull(FromLoc,'')=''"
            _objcmnbLayer._saveDatawithOutParm(strquery)

        End If
        If Val(txtcap1.Tag) > 0 Then
            _objcmnbLayer._saveDatawithOutParm("UPDATE AdditionalInfoCaptionTb set cap1='" & txtcap1.Text & "'," & _
                                               "cap2='" & txtcap2.Text & "'," & _
                                               "cap3='" & txtcap3.Text & "'," & _
                                               "cap4='" & txtcap4.Text & "'," & _
                                               "cap5='" & txtcap5.Text & "'")
        Else
            _objcmnbLayer._saveDatawithOutParm("Insert into AdditionalInfoCaptionTb values('" & txtcap1.Text & "','" & _
                                               txtcap2.Text & "','" & _
                                               txtcap3.Text & "','" & _
                                               txtcap4.Text & "','" & _
                                               txtcap5.Text & "')")
        End If

        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT * FROM CompanyDefaultSettingsTb")
        If Val(txtrestructure.Text & "") = 0 Then txtrestructure.Text = 0
        If Val(txtlatefeupto4.Text & "") = 0 Then txtlatefeupto4.Text = 0
        If Val(txtlatefeaftr4.Text & "") = 0 Then txtlatefeaftr4.Text = 0
        If dt.Rows.Count > 0 Then
            _objcmnbLayer._saveDatawithOutParm("Update CompanyDefaultSettingsTb set financeEMIDate=" & Val(txtemidate.Text) & "," & _
                                               "financeSecondEMIDate=" & Val(txtemidate2.Text) & "," & _
                                               "financeLateFeeAmountUpto4Times=" & CDbl(txtlatefeupto4.Text) & "," & _
                                               "financeLateFeeAmountAfter4Times=" & CDbl(txtlatefeaftr4.Text) & "," & _
                                               "financeFreeSettlementDuration=" & Val(txtsettelmntduration.Text) & "," & _
                                               "financeSettlementAfterDuration=" & Val(txtsettlmntaftrduration.Text) & "," & _
                                               "financeRestructureFee=" & CDbl(txtrestructure.Text))
        Else
            _objcmnbLayer._saveDatawithOutParm("Insert into CompanyDefaultSettingsTb (financeEMIDate,financeSecondEMIDate,financeLateFeeAmountUpto4Times,financeLateFeeAmountAfter4Times,financeFreeSettlementDuration,financeSettlementAfterDuration,financeRestructureFee) values(" & _
                                               Val(txtemidate.Text & "") & "," & _
                                               Val(txtemidate2.Text & "") & "," & _
                                                CDbl(txtlatefeupto4.Text) & "," & _
                                               CDbl(txtlatefeaftr4.Text) & "," & _
                                               Val(txtsettelmntduration.Text & "") & "," & _
                                               Val(txtsettlmntaftrduration.Text & "") & "," & _
                                              CDbl(txtrestructure.Text) & ")")
        End If
        saveCompanyOtherInfo()
        'Me.Close()
    End Sub
    Private Sub loadfinancedata()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("select * from CompanyDefaultSettingsTb")
        If dt.Rows.Count > 0 Then
            txtemidate.Text = Trim(dt(0)("financeEMIDate") & "")
            txtemidate2.Text = Trim(dt(0)("financeSecondEMIDate") & "")
            txtlatefeupto4.Text = Trim(dt(0)("financeLateFeeAmountUpto4Times") & "")
            txtlatefeaftr4.Text = Trim(dt(0)("financeLateFeeAmountAfter4Times") & "")
            txtsettelmntduration.Text = Trim(dt(0)("financeFreeSettlementDuration") & "")
            txtsettlmntaftrduration.Text = Trim(dt(0)("financeSettlementAfterDuration") & "")
            txtrestructure.Text = Trim(dt(0)("financeRestructureFee") & "")
        End If
    End Sub
    Private Sub cldrdate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        btnUpdate.Enabled = True
    End Sub

    Private Sub cldrdate1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        btnUpdate.Enabled = True
    End Sub

    Private Sub grdVoucher_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellDoubleClick
        'grdVoucherNo.BeginEdit(True)
    End Sub

    Private Sub grdVoucher_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.DoubleClick
        grdVoucher.BeginEdit(True)
    End Sub

    Private Sub grdVoucher_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.GotFocus
        activecontrolname = "grdVoucher"
    End Sub


    Private Sub grdVoucher_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdVoucher.KeyDown
        If e.KeyCode = Keys.F2 Then
            ldSelect(grdVoucher.CurrentRow.Index, True)
        End If
    End Sub
    Private Sub ldSelect(ByVal BVal As Single, Optional ByVal isOther As Boolean = False)
        fSelect = New Selectfrm
        If isOther Then
            getGLSQL(fSelect, BVal)
        Else
            fSelect.Location = New Point(650, 150)
            fSelect.StartPosition = FormStartPosition.CenterScreen
            SetForm(fSelect, BVal)
        End If
        fSelect.Show()
    End Sub

    Private Sub grdVoucher_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.Leave
        activecontrolname = ""
    End Sub
    Private Sub saveAccset()
        If cmbBr.Text <> "" Then Exit Sub
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT AccountNo,Alias FROM AccMast")
        Dim i As Integer
        For i = 0 To grdVoucher.RowCount - 1
            If grdVoucher.Item(1, i).Value <> "" Then
                Dim _qry = From job In dt.AsEnumerable() Where job!Alias = grdVoucher.Item(1, i).Value Select New With _
                                       {.Name = job!AccountNo}
                If Not _qry.Any Then
                    MsgBox("Invalid Account Name!", MsgBoxStyle.Critical)
                End If
            End If
        Next
        _objcmnbLayer._saveDatawithOutParm("UPDATE AccMast SET AccSetId = ''")
        For i = 0 To grdVoucher.RowCount - 1
            If grdVoucher.Item(1, i).Value <> "" And grdVoucher.Item(0, i).Value <> "" Then
                _objcmnbLayer._saveDatawithOutParm("UPDATE AccMast SET AccSetId = AccSetId + ' " & FormatStringLft(i + 1, "00") & "' WHERE Alias='" & grdVoucher.Item(1, i).Value & "'")
            End If
        Next
    End Sub
    Private Sub getGLSQL(ByVal frm As Selectfrm, ByVal col As Byte)
        Dim getGLSQLstr As String = ""
        Try
            getGLSQLstr = "SELECT AccMast.AccDescr As [Account Name],AccMast.Alias,AccMast.accid FROM " & _
                                   "S1AccHd INNER JOIN AccMast ON S1AccHd.S1AccId = AccMast.S1AccId " & _
                                   "LEFT JOIN CurrencyTb ON AccMast.CurrencyCode = CurrencyTb.CurrencyCode "
            Select Case col + 1
                Case 1
                    getGLSQLstr = getGLSQLstr & " WHERE AccMast.S1AccId Between 1000 And 1999"
                Case 6, 8
                    getGLSQLstr = getGLSQLstr & " WHERE S1AccHd.GrpSetOn='Bank'"
                Case 7
                    getGLSQLstr = getGLSQLstr & " WHERE S1AccHd.GrpSetOn='P.D.C.(R)'"
                Case 6
                    getGLSQLstr = AssignAccSQLStr(3)

                Case 5
                    getGLSQLstr = getGLSQLstr & " WHERE S1AccHd.GrpSetOn='Cash'"
                    'Case 2
                    '    getGLSQLstr = getGLSQLstr '& " WHERE AccMast.S1AccId Between 2000 And 2999"
                Case 2, 3, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18
                    getGLSQLstr = AssignAccSQLStr(8)
                Case 4
                    getGLSQLstr = AssignAccSQLStr(3)

            End Select
            frm.Location = New Point(650, 150)
            frm.pnlRadios.Visible = False
            frm.Width = 516
            frm.strMyQry = getGLSQLstr
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ldVoucherno()
        Try
            If UsrBr = "" Then
df:
                dtVhr = _objcmnbLayer._fldDatatable("SELECT * FROM INVNOS")
            Else
                dtVhr = _objcmnbLayer._fldDatatable("SELECT InvNosBrTb.InvType,TypeName,InvNosBrTb.Prefix,InvNosBrTb.InvNo FROM InvNosBrTb " & _
                                                    "left join INVNOS on InvNosBrTb.InvType=INVNOS.InvType where Brcode='" & UsrBr & "'")
                If dtVhr.Rows.Count = 0 Then GoTo df
            End If

            dtVoucher.Rows.Clear()
            grdVoucherNo.DataSource = dtVhr
            setGridHeadVhrNo()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub CreateVoucherTb()
        With dtVoucher
            If .Columns.Count > 0 Then Exit Sub
            .Columns.Add(New DataColumn("Voucher", GetType(String)))
            .Columns.Add(New DataColumn("Prefix", GetType(String)))
            .Columns.Add(New DataColumn("Voucher No", GetType(Integer)))
        End With
    End Sub
    Private Sub setGridHeadVhrNo()
        With grdVoucherNo
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = True
            .EditMode = DataGridViewEditMode.EditProgrammatically
            .ColumnHeadersHeight = 30
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            '.ColumnHeadersDefaultCellStyle.BackColor = Color.Olive
            .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.5!, System.Drawing.FontStyle.Regular)
            .RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.5!)

            .Columns(1).HeaderText = "Inv Type"
            .Columns(1).ReadOnly = True
            .Columns(1).Width = 200
            '.Columns(2).Visible = False
            .Columns(2).Width = 80
            .Columns(3).HeaderText = "Inv No"
            .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
            .Columns(3).DefaultCellStyle.Format = "N0"
            '.Columns(4).Visible = False
            .Columns(0).Visible = False
        End With

    End Sub

    Private Sub grdVoucherNo_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucherNo.CellDoubleClick
        grdVoucherNo.BeginEdit(True)
    End Sub

    Private Sub grdVoucherNo_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdVoucherNo.EditingControlShowing
        'btnset.Enabled = True
    End Sub

    Private Sub grdVoucherNo_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucherNo.Enter
        grdVoucherNo.BeginEdit(True)
    End Sub
    Private Sub saveVhrNo()
        If UsrBr = "" Then
            _objcmnbLayer.__saveDataTable("SELECT * FROM INVNOS", dtVhr)
        Else
            Dim i As Integer
            _objcmnbLayer._saveDatawithOutParm("Delete from InvNosBrTb where Brcode='" & UsrBr & "'")
            For i = 0 To dtVhr.Rows.Count - 1
                _objcmnbLayer._saveDatawithOutParm("insert into InvNosBrTb (InvType,Brcode,Prefix,InvNo) values('" & _
                                                   dtVhr(i)("InvType") & "','" & _
                                                   UsrBr & "','" & _
                                                   dtVhr(i)("Prefix") & "'," & _
                                                   dtVhr(i)("InvNo") & ")")
            Next
        End If

    End Sub

    Private Sub btnset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        saveVhrNo()
        'btnset.Enabled = False
        'BtnUpdateVchr.Enabled = False
    End Sub

    Private Sub btnTrfrItms_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTrfrItms.Click
        'TransferiItemsFromExcel.ShowDialog()
    End Sub

    Private Sub grdVchr_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVchr.CellContentClick

    End Sub

    Private Sub btndocpath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndocpath.Click
        FolderBrowserDialog1.ShowDialog()
        If FolderBrowserDialog1.SelectedPath <> "" Then
            txtdocumentpath.Text = FolderBrowserDialog1.SelectedPath
        End If
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select systemname from SystemDocPathTb where systemname='" & MACHINENAME & "'")
        If dt.Rows.Count > 0 Then
            _objcmnbLayer._saveDatawithOutParm("Update SystemDocPathTb set dpath='" & txtdocumentpath.Text & "', brid=" & BranchId & " where systemname='" & MACHINENAME & "'")
        Else
            _objcmnbLayer._saveDatawithOutParm("Insert into SystemDocPathTb (systemname,dpath,brid) values('" & MACHINENAME & "','" & txtdocumentpath.Text & "'," & BranchId & ")")
        End If
        MsgBox("Document path has been set", MsgBoxStyle.Information)
    End Sub

    Private Sub txtdocumentpath_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtdocumentpath.TextChanged
        ChgID = True
    End Sub
    Private Sub ldSysPara()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select ProcessCode,Description,OrdNo,isnull(isEnable,0) isEnable from SysPara where isnull(tp,0)=0 and isnull(isvisible,0)=0 order by OrdNo")
        chkparameter.Items.Clear()
        Dim i As Integer
        For i = 0 To dt.Rows.Count - 1
            chkparameter.Items.Add(dt(i)("Description"), dt(i)("isEnable"))
        Next
    End Sub
    Private Sub updateParameter()
        Dim i As Integer
        For i = 0 To chkparameter.Items.Count - 1
            _objcmnbLayer._saveDatawithOutParm("Update SysPara set isEnable=" & IIf(chkparameter.GetItemChecked(i), 1, 0) & " where Description='" & chkparameter.Items.Item(i).ToString & "'")
        Next
        SetSystemProperties()
        pltemple.Visible = enableTemple
    End Sub

    Private Sub chkparameter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkparameter.Click
        chkparameter.Tag = "chg"
    End Sub

    Private Sub LodCurrency()
        Dim dt As DataTable = _objcmnbLayer._fldDatatable("SELECT CurrencyCode FROM CurrencyTb", False)
        cmbcurrency.Items.Clear()
        cmbcurrency.Items.Add("")
        Dim i As Integer
        For i = 0 To dt.Rows.Count - 1
            cmbcurrency.Items.Add(dt(i)("CurrencyCode"))
        Next
    End Sub


    Private Sub btnRptToxml_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRptToxml.Click
        RptToXmlFrm.ShowDialog()
    End Sub

    Private Sub btnmodule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnmodule.Click
        If btnmodule.Text = "Modules" Then
            ldModules()
            btnUpdate.Enabled = False
            btnmodule.Text = "Set/Undo"
        Else
            If MsgBox("Do You Want to Update?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                updateModules()
                MsgBox("Module Updated", MsgBoxStyle.Information)
            End If
            btnUpdate.Enabled = True
            btnmodule.Text = "Modules"
            ldSysPara()
        End If
    End Sub
    Private Sub ldModules()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select ProcessCode,Description,OrdNo,isnull(isEnable,0) isEnable from SysPara where isnull(tp,0)=1 and isnull(isvisible,0)=0 order by OrdNo")
        Dim i As Integer
        chkparameter.Items.Clear()
        For i = 0 To dt.Rows.Count - 1
            chkparameter.Items.Add(dt(i)("Description"), dt(i)("isEnable"))
        Next
    End Sub
    Private Sub updateModules()
        Dim i As Integer
        For i = 0 To chkparameter.Items.Count - 1
            _objcmnbLayer._saveDatawithOutParm("Update SysPara set isEnable=" & IIf(chkparameter.GetItemChecked(i), 1, 0) & " where Description='" & chkparameter.Items.Item(i).ToString & "'")
        Next
        SetSystemProperties()
    End Sub
    Private Sub ldState()
        Dim dt As DataTable = _objcmnbLayer._fldDatatable("SELECT statecode FROM StateMasterTb", False)
        cmbstate.Items.Clear()
        cmbstate.Items.Add("")
        Dim i As Integer
        For i = 0 To dt.Rows.Count - 1
            cmbstate.Items.Add(dt(i)("statecode"))
        Next
    End Sub

    Private Sub txtserialAlert_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtserialAlert.KeyPress, txtdecimal.KeyPress
        NumericTextOnKeypress(sender, e, chgByPrg, "0")
    End Sub

    Private Sub grdVoucher_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellContentClick

    End Sub

    Private Sub btnremovebtch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnremovebtch.Click
        If MsgBox("You are going to remove Un-Matching Batch Entries! Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        _objcmnbLayer._saveDatawithOutParm("delete from BatchTb where batchTrid not in(select id from ItmInvTrTb) and batchTrid>0")
        MsgBox("Completed", MsgBoxStyle.Information)
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpaymentac.TextChanged

    End Sub

    Private Sub txtcollectionac_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCGSTac.KeyDown, _
                                                                                                                      txtpaymentac.KeyDown, txtIgst.KeyDown, txtcgstPac.KeyDown, _
                                                                                                                      txtSGSTPac.KeyDown, txtIGSTPac.KeyDown
        If e.KeyCode = Keys.F2 Then
            Dim myctrl As TextBox
            myctrl = sender
            If e.KeyCode = Keys.F2 Then
                If Val(myctrl.AccessibleDescription & "") > 0 Then
                    srchIndex = Val(myctrl.AccessibleDescription & "")
                    ldSelect(2)
                End If
            ElseIf e.KeyCode = Keys.Return Then
                SendKeys.Send("{TAB}")
            End If
        End If
    End Sub

    Private Sub btngstset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btngstset.Click
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select * from GstDefaultSetTb where gst=" & Val(cmbgstslab.Text))
        If Val(txtCGSTac.Tag & "") = 0 Then txtCGSTac.Tag = 0
        If Val(txtpaymentac.Tag & "") = 0 Then txtpaymentac.Tag = 0
        If Val(txtIgst.Tag & "") = 0 Then txtIgst.Tag = 0
        If Val(txtcgstPac.Tag & "") = 0 Then txtcgstPac.Tag = 0
        If Val(txtSGSTPac.Tag & "") = 0 Then txtSGSTPac.Tag = 0
        If Val(txtIGSTPac.Tag & "") = 0 Then txtIGSTPac.Tag = 0
        If dt.Rows.Count > 0 Then
            _objcmnbLayer._saveDatawithOutParm("Update GstDefaultSetTb set cac=" & Val(txtCGSTac.Tag) & ",pac=" & Val(txtpaymentac.Tag) & ",igstac=" & Val(txtIgst.Tag) & _
                                               ",cgstpac=" & Val(txtcgstPac.Tag) & ",sgstpac=" & Val(txtSGSTPac.Tag) & ",igstpac=" & Val(txtIGSTPac.Tag) & _
                                               " where gst=" & Val(cmbgstslab.Text))
        Else
            _objcmnbLayer._saveDatawithOutParm("Insert into GstDefaultSetTb (gst,cac,pac,igstac,cgstpac,sgstpac,igstpac) values(" & Val(cmbgstslab.Text) & "," & _
                                               Val(txtCGSTac.Tag) & "," & Val(txtpaymentac.Tag) & "," & Val(txtIgst.Tag) & _
                                               "," & Val(txtcgstPac.Tag) & "," & Val(txtSGSTPac.Tag) & "," & Val(txtIGSTPac.Tag) & ")")
        End If
        MsgBox("GST Defaults has been set", MsgBoxStyle.Information)
    End Sub
    Private Sub loadgstslab()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select * from GstDefaultSetTb" & _
                                         " left join (select accid cid,accdescr cname from accmast)cacd on GstDefaultSetTb.cac=cacd.cid" & _
                                         " left join (select accid pid,accdescr pname from accmast)pacd on GstDefaultSetTb.pac=pacd.pid" & _
                                         " left join (select accid igid,accdescr isgtname from accmast)igstacd on GstDefaultSetTb.igstac=igstacd.igid" & _
                                         " left join (select accid cgstpid,accdescr cgstpname from accmast)cgstpacd on GstDefaultSetTb.cgstpac=cgstpacd.cgstpid" & _
                                         " left join (select accid sgstpid,accdescr sgstpname from accmast)sgstpacd on GstDefaultSetTb.sgstpac=sgstpacd.sgstpid" & _
                                         " left join (select accid igstpid,accdescr isgtpname from accmast)igstpacd on GstDefaultSetTb.igstpac=igstpacd.igstpid" & _
                                         " where gst=" & Val(cmbgstslab.Text))
        If dt.Rows.Count > 0 Then
            txtCGSTac.Text = Trim(dt(0)("cname") & "")
            txtpaymentac.Text = Trim(dt(0)("pname") & "")
            txtIgst.Text = Trim(dt(0)("isgtname") & "")
            txtIgst.Tag = dt(0)("igid")
            txtCGSTac.Tag = dt(0)("cid")
            txtpaymentac.Tag = dt(0)("pid")

            txtcgstPac.Text = Trim(dt(0)("cgstpname") & "")
            txtcgstPac.Tag = dt(0)("cgstpid")
            txtSGSTPac.Text = Trim(dt(0)("sgstpname") & "")
            txtSGSTPac.Tag = dt(0)("sgstpid")
            txtIGSTPac.Text = Trim(dt(0)("isgtpname") & "")
            txtIGSTPac.Tag = dt(0)("igstpid")
        Else
            txtCGSTac.Text = ""
            txtpaymentac.Text = ""
            txtIgst.Text = ""
            txtIgst.Tag = ""
            txtCGSTac.Tag = ""
            txtpaymentac.Tag = ""
            txtcgstPac.Text = ""
            txtcgstPac.Tag = ""
            txtSGSTPac.Text = ""
            txtSGSTPac.Tag = ""
            txtIGSTPac.Text = ""
            txtIGSTPac.Tag = ""
        End If
    End Sub

    Private Sub cmbgstslab_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbgstslab.SelectedIndexChanged
        loadgstslab()
    End Sub

    Private Sub btnhscode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnhscode.Click
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT HSNCode,IGST,gstid FROM GSTTb")
        Dim i As Integer
        Dim taxcode As String
        For i = 0 To dt.Rows.Count - 1
            taxcode = dt(i)("HSNCode") & " - " & dt(i)("IGST") & "%"
            _objcmnbLayer._saveDatawithOutParm("Update ItmInvTrTb set HSNCode='" & taxcode & "',Trgstid=" & dt(i)("gstid") & " where HSNCode='" & dt(i)("HSNCode") & "'")
            _objcmnbLayer._saveDatawithOutParm("Update InvItm set HSNCode='" & taxcode & "' where HSNCode='" & dt(i)("HSNCode") & "'")
            _objcmnbLayer._saveDatawithOutParm("Update GSTTb set HSNCode='" & taxcode & "',GSTName='" & dt(i)("HSNCode") & "' where HSNCode='" & dt(i)("HSNCode") & "'")
        Next
        MsgBox("Updated", MsgBoxStyle.Information)
    End Sub

    Private Sub btnupdateTaxIndex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdateTaxIndex.Click
        Dim dt As DataTable
        Dim dt1 As DataTable
        Dim i As Integer
        dt = _objcmnbLayer._fldDatatable("Select HSNCode from ItmInvTrTb group by HSNCode")
        For i = 0 To dt.Rows.Count - 1
            dt1 = _objcmnbLayer._fldDatatable("SELECT gstid FROM GSTTb where HSNCode='" & dt(i)("HSNCode") & "'")
            _objcmnbLayer._saveDatawithOutParm("Update ItmInvTrTb set Trgstid=" & dt1(i)("gstid") & " where HSNCode='" & dt(i)("HSNCode") & "'")
        Next
        MsgBox("Updated", MsgBoxStyle.Information)
    End Sub

    Private Sub txtmonthlyfees_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtmonthlyfees.KeyPress
        NumericTextOnKeypress(sender, e, chgByPrg, numFormat)
    End Sub

    Private Sub txtmonthlyfees_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtmonthlyfees.TextChanged
        If chgByPrg Then Exit Sub
        ChgID = True
    End Sub

    Private Sub btnCorrectFmeter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCorrectFmeter.Click
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT fcode FROM FuelMeterReadingTb")
        Dim i As Integer
        For i = 0 To dt.Rows.Count - 1
            _objcmnbLayer.updateMeterReadingQty(dt(i)("fcode"))
        Next
        MsgBox("Updated", MsgBoxStyle.Information)
    End Sub

    Private Sub btnnetworkpath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnetworkpath.Click
        FolderBrowserDialog1.ShowDialog()
        If FolderBrowserDialog1.SelectedPath <> "" Then
            txtnetworkpath.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub btntaxpricetounitprice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btntaxpricetounitprice.Click
        _objcmnbLayer._saveDatawithOutParm("update itm set itm.UnitPrice=isnull(tx.TaxPrice,0) from InvItm itm Inner Join " & _
                                           "(SELECT itemid,((isnull(IGST,0)*UnitPrice)/100)+UnitPrice TaxPrice FROM InvItm " & _
                                           "LEFT JOIN GSTTb ON GSTTb.HSNCode=InvItm.HSNCode) tx on itm.ItemId=tx.ItemId")
        MsgBox("Updated", MsgBoxStyle.Information)
    End Sub

    Private Sub btnremoveHsn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnremoveHsn.Click
        _objcmnbLayer._saveDatawithOutParm("update InvItm set HSNCode=''")
        MsgBox("Updated", MsgBoxStyle.Information)
    End Sub

    Private Sub btnitemtowebname_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnitemtowebname.Click
        _objcmnbLayer._saveDatawithOutParm("update InvItm set ischange=1, webname=UPPER(LEFT(Description,1))+LOWER(SUBSTRING(Description,2,LEN(Description)))")
        MsgBox("Updated", MsgBoxStyle.Information)
    End Sub
    Private Sub updateOrSelectQuery(ByVal strQry As String)

        Try
            Dim qryTp As String
            Dim dt As New DataTable
            qryTp = Mid(strQry, 1, 6)
            dt = _objcmnbLayer._fldDatatable(strQry)
            dvdata.DataSource = dt
            tbtroubleshooting.SelectedIndex = 2
            'If LCase(qryTp) = "select" Then
            '    dt = _objcmnbLayer._fldDatatable(strQry)
            '    dvdata.DataSource = dt
            '    tbtroubleshooting.SelectedIndex = 2
            'Else
            '    _objcmnbLayer._saveDatawithOutParm(strQry)
            '    MsgBox("Updated", MsgBoxStyle.Information)
            'End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        End Try


    End Sub

    Private Sub btnexecute_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexecute.Click
        updateOrSelectQuery(txtquery.Text)
    End Sub
    Private Sub tables()
        Dim str As String
        str = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_CATALOG='" & MyDatabase & "' order by TABLE_NAME"
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable(str)
        For i = 0 To dt.Rows.Count - 1
            cmbtables.Items.Add(dt(i)("TABLE_NAME"))
        Next
    End Sub
    Private Sub loadTableData()
        Me.Cursor = Cursors.WaitCursor
        Dim dt As New DataTable
        dt = _objcmnbLayer._fldDatatable("Select * from " & cmbtables.Text)
        dvdata.DataSource = dt
        SetGridProperty(dvdata)
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub cmbtables_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbtables.SelectedIndexChanged
        loadTableData()
    End Sub

    Private Sub btnrvfrominv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrvfrominv.Click
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("select * from (select dealamt,RestRVid,rvamt,JVNum,JVType,rvnum,rvtype from AccTrCmn " & _
                                        "left join AccTrDet on AccTrCmn.LinkNo=AccTrDet.LinkNo " & _
                                        "left join ItmInvCmnTb on AccTrCmn.LnkNo=ItmInvCmnTb.trid " & _
                                        "left join (select dealamt rvamt,AccTrDet.LinkNo,JVNum rvnum,JVType rvtype from AccTrDet " & _
                                        "left join AccTrCmn on AccTrCmn.LinkNo=AccTrDet.LinkNo where DealAmt>0 )tr on ItmInvCmnTb.RestRVid=tr.LinkNo " & _
                                        "where TrType='IS' and DealAmt>0 ) tr where DealAmt<>rvamt")
        Dim i As Integer
        For i = 0 To dt.Rows.Count - 1
            _objcmnbLayer._saveDatawithOutParm("Update AccTrDet set dealamt=case when dealamt<0 then -1 else 1 end *" & dt(i)("dealamt") & " where linkno=" & dt(i)("RestRVid"))
        Next
        MsgBox("Updated", MsgBoxStyle.Information)
    End Sub

    Private Sub btnhsnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnhsnupdate.Click
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT HSNCode,IGST,gstid FROM GSTTb")
        If dt.Rows.Count > 0 Then
            _objcmnbLayer._saveDatawithOutParm("Update InvItm set HSNCode='" & dt(0)("HSNCode") & "'")
        End If
        MsgBox("Updated", MsgBoxStyle.Information)
    End Sub

    Private Sub btnCorrectInvAcc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCorrectInvAcc.Click
        CorrectInvAcc.ShowDialog()
    End Sub

    Private Sub txtfontsize_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtfontsize.KeyPress, txtfirstdateBefore.KeyPress
        NumericTextOnKeypress(sender, e, chgByPrg, "0")
    End Sub

    Private Sub btnsms_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsms.Click
        addSmsFrm.ShowDialog()
    End Sub

    Private Sub btnUpdateCollation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateCollation.Click
        Me.Cursor = Cursors.WaitCursor
        Dim cnt As Integer = _objdataCorrection.UpdateCollation()
        Me.Cursor = Cursors.Default
        MsgBox(cnt & " Records Updated", MsgBoxStyle.Information)
    End Sub

    Private Sub btntemplesales_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btntemplesales.Click
        Me.Cursor = Cursors.WaitCursor
        updateTempleSales()
        Me.Cursor = Cursors.Default
        MsgBox("Done", MsgBoxStyle.Information)
    End Sub
    Private Sub updateTempleSales()
        Dim dtcmn As DataTable
        Dim dtdet As DataTable
        Dim vrPrefix As String = ""
        Dim vrVoucherNo As Long
        Dim vrAccountNo1 As Long
        Dim vrAccountNo2 As Long
        Dim voucherid As Integer
        Dim LinkNo As Long
        Dim j As Integer
        Dim itmAmt, TDrAmt As Double
        _objcmnbLayer._saveDatawithOutParm("Delete from acctrcmn  delete from acctrdet")
        dtcmn = _objcmnbLayer._fldDatatable("Select trid,VoucherTypeid,prefix,invno,trdate from TempleSalesCmnTb")
        Dim i As Integer
        Dim reference As String
        For i = 0 To dtcmn.Rows.Count - 1
            voucherid = Val(dtcmn(i)("VoucherTypeid"))
            getVrsDet(voucherid, "TIS", vrPrefix, vrVoucherNo, vrAccountNo1, vrAccountNo2)
            ReDim JobAcc(0)
            JobAcc(0).Acc = vrAccountNo1
            JobAcc(0).Job = ""
            dtdet = _objcmnbLayer._fldDatatable("SELECT ItemId,accid,Qty,rate,isnull(Isacc,0)Isacc FROM TempleSalesDetTb " & _
                                          " WHERE trid=" & Val(dtcmn(1)("trid")) & " ORDER BY SlNo")
            For j = 0 To dtdet.Rows.Count - 1
                If Val(dtdet(j)("Isacc")) = 0 Then
                    itmAmt = itmAmt + CDbl(dtdet(j)("Qty")) * CDbl(dtdet(j)("rate"))
                Else
                    j = j + 1
                    ReDim Preserve JobAcc(j)
                    JobAcc(j).Acc = Val(dtdet(j)("accid"))
                    JobAcc(j).Amt = CDbl(dtdet(j)("Qty")) * CDbl(dtdet(j)("rate"))
                End If
                TDrAmt = TDrAmt + (CDbl(dtdet(j)("Qty")) * CDbl(dtdet(j)("rate")))
            Next
            JobAcc(0).Amt = itmAmt
            reference = Trim(dtcmn(i)("prefix")) & IIf(dtcmn(i)("prefix") = "", "", "/") & dtcmn(i)("invno")
            LinkNo = setAcctrCmnValue(dtcmn(i)("prefix"), Val(dtcmn(i)("invno")), DateValue(dtcmn(i)("trdate")))
            'debit
            setAcctrDetValue(LinkNo, vrAccountNo2, reference, "SALES", TDrAmt, "", "", 0, 0, "", _
                         "", Val(vrAccountNo1), Val(vrAccountNo1) & reference, "", 1)
            _objTr.saveAccTrans()
            'Credit Entry
            For j = 0 To JobAcc.Count - 1
                If JobAcc(j).Amt > 0 Then
                    setAcctrDetValue(LinkNo, j, reference)
                    _objTr.saveAccTrans()
                End If
            Next
            updateClosingBalanceForInvoice(LinkNo, True)
        Next
    End Sub
    Private Function setAcctrCmnValue(ByVal prefix As String, ByVal jvnum As Integer, ByVal jvdate As Date) As Long
        _objTr.JVType = "TIS"
        _objTr.JVDate = DateValue(jvdate)
        _objTr.PreFix = prefix
        _objTr.JVNum = jvnum
        _objTr.JVTypeNo = getVouchernumber("TIS")
        _objTr.UserId = Trim(CurrentUser & "")
        _objTr.MchName = MACHINENAME
        _objTr.CrtDtTm = Date.Now
        _objTr.TypeNo = getVouchernumber("TIS")
        _objTr.LinkNo = 0
        _objTr.IsModi = 1
        _objTr.isLinkNo = True
        Return Val(_objTr.SaveAccTrCmn())
    End Function
    Private Sub setAcctrDetValue(ByVal lnkNo As Long, ByVal jbIndex As Integer, ByVal ref As String)
        With _objTr
            .trLinkNo = lnkNo
            .AccountNo = JobAcc(jbIndex).Acc
            .Reference = ref
            .EntryRef = "SALES"
            .DealAmt = -1 * JobAcc(jbIndex).Amt
            .FCAmt = -1 * JobAcc(jbIndex).Amt
            .JobCode = JobAcc(jbIndex).Job
            .JobStr = ""
            .CurrRate = 1
            .CurrencyCode = ""
            .TrInf = 0
            .OthCost = 0
            .TermsId = ""
            .CustAcc = 0
            .AccWithRef = ""
            .DueDate = Date.Now
            .DocDate = Date.Now
            .SuppInvDate = Date.Now
        End With
    End Sub
    Private Sub setAcctrDetValue(ByVal lnkNo As Long, ByVal AccountNo As Long, ByVal Reference As String, ByVal EntryRef As String, ByVal DealAmt As Double, ByVal JobCode As String, ByVal JobStr As String, _
                                  ByVal TrInf As Short, ByVal OthCost As Short, ByVal LPO As String, ByVal TermsId As String, ByVal CustAcc As Integer, ByVal AccWithRef As String, _
                                  ByVal CurrencyCode As String, ByVal CurrRate As Double)
        With _objTr
            .trLinkNo = lnkNo
            .AccountNo = AccountNo
            .Reference = Reference
            .EntryRef = EntryRef
            .DealAmt = DealAmt
            .JobCode = JobCode
            .JobStr = JobStr
            .CurrRate = CurrRate
            .CurrencyCode = CurrencyCode ' IIf(chkFC.Checked = True, txtFC.Text, "")
            .TrInf = TrInf
            .OthCost = OthCost
            .TermsId = TermsId
            .CustAcc = CustAcc
            .AccWithRef = AccWithRef
            .LPONo = LPO
            Dim dtLPO As Date = Date.Now
            Dim dtDue As Date = DateValue(Date.Now)
            Dim dtSup As Date = DateValue(Date.Now)
            .DocDate = dtLPO
            .SuppInvDate = dtSup
            .DueDate = dtDue
        End With
    End Sub

    Private Sub btnremoveUnmatchSalesbatch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnremoveUnmatchSalesbatch.Click
        If MsgBox("You are going to remove Un-Matching Batch Entries! Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        _objcmnbLayer._saveDatawithOutParm("delete from SalesBatchTb where sTrid not in(select id from ItmInvTrTb)")
        _objcmnbLayer._saveDatawithOutParm("delete SalesBatchTb from SalesBatchTb left join  ItmInvTrTb on SalesBatchTb.sTrid=ItmInvTrTb.id  where itid<>itemid")
        Dim strqry As String
        strqry = "delete from SalesBatchTb where sTrid in (select id from ItmInvTrTb left join (select sum(qty)qty,sTrid from SalesBatchTb group by sTrid) tr on ItmInvTrTb.id=tr.sTrid where TrQty<>qty)"
        strqry = strqry & " update ItmInvTrTb set CostAvg=0 from ItmInvTrTb inner join (select id from ItmInvTrTb inner join ItmInvCmnTb on ItmInvTrTb.TrId=ItmInvCmnTb.TrId " & _
        "left join SalesBatchTb on ItmInvTrTb.id=SalesBatchTb.sTrid " & _
        "LEFT JOIN VoucherTypeNoTb on ItmInvCmnTb.TypeNo=VoucherTypeNoTb.vrno " & _
        "where isnull(sTrid,0)=0 and InvType='OUT') TR ON ItmInvTrTb.id=tr.id"
        strqry = strqry & " insert into TobeRefreshItemTb (trid,createdby) select ItmInvCmnTb.TrId,'RESET' from ItmInvTrTb inner join ItmInvCmnTb on ItmInvTrTb.TrId=ItmInvCmnTb.TrId " & _
        "left join SalesBatchTb on ItmInvTrTb.id=SalesBatchTb.sTrid " & _
        "LEFT JOIN VoucherTypeNoTb on ItmInvCmnTb.TypeNo=VoucherTypeNoTb.vrno " & _
        "where isnull(sTrid,0)=0 and InvType='OUT'"
        _objcmnbLayer._saveDatawithOutParm(strqry)

        MsgBox("Completed", MsgBoxStyle.Information)
        If MsgBox("Do you want to Run Reset Refresh cost?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        If Not fMainForm.fRFCOST Is Nothing Then
            fMainForm.fRFCOST.Close()
            fMainForm.fRFCOST = Nothing
            fMainForm.loadRefreshcost(1)
        End If
    End Sub

    Private Sub btnFindUnmatchingBatch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFindUnmatchingBatch.Click
        updateOrSelectQuery("Select * from BatchTb where batchTrid not in(select id from ItmInvTrTb) and batchTrid>0")
    End Sub

    Private Sub btnFindUnmatchingSalesBatch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFindUnmatchingSalesBatch.Click
        updateOrSelectQuery("Select * from SalesBatchTb where sTrid not in(select id from ItmInvTrTb) UNION ALL Select SalesBatchTb.* from SalesBatchTb left join  ItmInvTrTb on SalesBatchTb.sTrid=ItmInvTrTb.id  where itid<>itemid")
    End Sub

    Private Sub btnduplicateitemcodeData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnduplicateitemcodeData.Click
        updateOrSelectQuery("select [Item Code],Description,cnt,ItemId from invitm " & _
                            "left join (select count([Item Code]) cnt, [Item Code] icode from invitm group by [Item Code])itm " & _
                            "on InvItm.[Item Code]=itm.icode where cnt>1")
    End Sub

    Private Sub btnduplicateitemnamedata_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnduplicateitemnamedata.Click
        updateOrSelectQuery("select [Item Code],Description,cnt,ItemId from invitm " & _
                           "left join (select count(Description) cnt, Description icode from invitm group by Description)itm " & _
                           "on InvItm.Description=itm.icode where cnt>1")
    End Sub

    Private Sub btnduplicateitemcode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnduplicateitemcode.Click
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("select [Item Code],Description,cnt,ItemId from invitm " & _
                           "left join (select count([Item Code]) cnt, [Item Code] icode from invitm group by [Item Code])itm " & _
                           "on InvItm.[Item Code]=itm.icode where cnt>1 order by [Item Code],ItemId")
        Dim i As Integer
        Dim itemname As String = ""
        Dim ItemId As Integer
        For i = 0 To dt.Rows.Count - 1
            If ItemId > 0 And ItemId <> dt(i)("ItemId") And UCase(itemname) = UCase(dt(i)("[Item Code]")) Then
                _objcmnbLayer._saveDatawithOutParm("update ItmInvTrTb set itemid=" & ItemId & " where itemid=" & dt(i)("ItemId") & _
                                                   " update DocTranTb set itemid=" & ItemId & " where itemid=" & dt(i)("ItemId") & _
                                                   " update BatchTb set itemid=" & ItemId & " where itemid=" & dt(i)("ItemId") & _
                                                   " update SalesBatchTb set Itid=" & ItemId & " where Itid=" & dt(i)("ItemId") & _
                                                   " update SerialNoTrTb set itemid=" & ItemId & " where itemid=" & dt(i)("ItemId") & _
                                                   " update KOTItemsTb set itemid=" & ItemId & " where itemid=" & dt(i)("ItemId") & _
                                                   " update KOTCancelledItemsTb set itemid=" & ItemId & " where itemid=" & dt(i)("ItemId") & _
                                                   " update JobInvTrTb set itemid=" & ItemId & " where itemid=" & dt(i)("ItemId") & _
                                                   " update JobitemTb set itemid=" & ItemId & " where itemid=" & dt(i)("ItemId") & _
                                                   " update LodgeServiceTb set ldgServiceItemid=" & ItemId & " where ldgServiceItemid=" & dt(i)("ItemId") & _
                                                   " update LodgeRoomTb set roomItemid=" & ItemId & " where roomItemid=" & dt(i)("ItemId") & _
                                                   " update ItemRawMeterialTb set Ritemid=" & ItemId & " where Ritemid=" & dt(i)("ItemId"))
                _objcmnbLayer._saveDatawithOutParm("delete from invitm where itemid=" & dt(i)("ItemId") & _
                                                   "delete from InvItmPropertiesTb where itemid=" & dt(i)("ItemId"))
                ItemId = 0
                itemname = ""
            Else
                itemname = dt(i)("[Item Code]")
                ItemId = dt(i)("ItemId")
            End If
        Next
        MsgBox("Completed", MsgBoxStyle.Information)
    End Sub

    Private Sub btnduplicateitemnames_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnduplicateitemnames.Click
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("select [Item Code],Description,cnt,ItemId from invitm " & _
                           "left join (select count(Description) cnt, Description icode from invitm group by Description)itm " & _
                           "on InvItm.Description=itm.icode where cnt>1 order by Description,ItemId")
        Dim i As Integer
        Dim itemname As String = ""
        Dim ItemId As Integer
        For i = 0 To dt.Rows.Count - 1
            If ItemId > 0 And ItemId <> dt(i)("ItemId") And UCase(itemname) = UCase(dt(i)("Description")) Then
                _objcmnbLayer._saveDatawithOutParm("update ItmInvTrTb set itemid=" & ItemId & " where itemid=" & dt(i)("ItemId") & _
                                                   " update DocTranTb set itemid=" & ItemId & " where itemid=" & dt(i)("ItemId") & _
                                                   " update BatchTb set itemid=" & ItemId & " where itemid=" & dt(i)("ItemId") & _
                                                   " update SalesBatchTb set Itid=" & ItemId & " where Itid=" & dt(i)("ItemId") & _
                                                   " update SerialNoTrTb set itemid=" & ItemId & " where itemid=" & dt(i)("ItemId") & _
                                                   " update KOTItemsTb set itemid=" & ItemId & " where itemid=" & dt(i)("ItemId") & _
                                                   " update KOTCancelledItemsTb set itemid=" & ItemId & " where itemid=" & dt(i)("ItemId") & _
                                                   " update JobInvTrTb set itemid=" & ItemId & " where itemid=" & dt(i)("ItemId") & _
                                                   " update JobitemTb set itemid=" & ItemId & " where itemid=" & dt(i)("ItemId") & _
                                                   " update LodgeServiceTb set ldgServiceItemid=" & ItemId & " where ldgServiceItemid=" & dt(i)("ItemId") & _
                                                   " update LodgeRoomTb set roomItemid=" & ItemId & " where roomItemid=" & dt(i)("ItemId") & _
                                                   " update ItemRawMeterialTb set Ritemid=" & ItemId & " where Ritemid=" & dt(i)("ItemId"))
                _objcmnbLayer._saveDatawithOutParm("delete from invitm where itemid=" & dt(i)("ItemId") & _
                                                   "delete from InvItmPropertiesTb where itemid=" & dt(i)("ItemId"))
                ItemId = 0
                itemname = ""
            Else
                itemname = dt(i)("Description")
                ItemId = dt(i)("ItemId")
            End If
        Next
        MsgBox("Completed", MsgBoxStyle.Information)
    End Sub
    Private Sub loadAddInfoCap()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select * from AdditionalInfoCaptionTb")
        If dt.Rows.Count > 0 Then
            txtcap1.Text = dt(0)("cap1")
            txtcap2.Text = dt(0)("cap2")
            txtcap3.Text = dt(0)("cap3")
            txtcap4.Text = dt(0)("cap4")
            txtcap5.Text = dt(0)("cap5")
            txtcap1.Tag = 1
        Else
            txtcap1.Tag = 0
            txtcap1.Text = ""
            txtcap2.Text = ""
            txtcap3.Text = ""
            txtcap4.Text = ""
            txtcap5.Text = ""
        End If
    End Sub

    Private Sub txtcap1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtcap1.KeyDown, txtcap2.KeyDown, txtcap3.KeyDown, txtcap4.KeyDown, txtcap5.KeyDown
        If e.KeyCode = Keys.Return Then
            Dim ctrl As TextBox = sender
            If ctrl.Name = "txtcap5" Then
                btnUpdate.Focus()
            Else
                SendKeys.Send("{TAB}")
            End If
        End If
    End Sub
    Sub LoadDatabases()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select * from sys.databases where database_id>4")
        Dim i As Integer
        cmbseconddb.Items.Clear()
        cmbseconddb.Items.Add("")
        For i = 0 To dt.Rows.Count - 1
            cmbseconddb.Items.Add(dt(i)(0))
        Next
    End Sub

    Private Sub btnloaddbs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnloaddbs.Click
        LoadDatabases()
    End Sub

    Private Sub btnposdiscount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnposdiscount.Click
        calOthCost()
    End Sub
    Private Sub calOthCost()
        Dim i As Integer
        Dim r As Integer
        Dim tBAmt As Double
        Dim tBDAmt As Double
        Dim tDAmt As Double
        Dim actualPrice As Double
        Dim qty As Double
        Dim lineDiscountAmt As Double
        Dim dt As DataTable
        Dim dtItems As DataTable
        Dim dtInvs As DataTable
        Dim discamt As Double
        dtInvs = _objcmnbLayer._fldDatatable("Select * from itminvcmntb Left join " & _
                                             "(select sum(ISNULL(unitdiscount,0)*trqty) unitDiscount,trid from itminvtrtb group by trid) tr on tr.trid=itminvcmntb.trid " & _
                                         "where isnull(IsPOS,0)<>0 and trtype='IS' AND ISNULL(Discount,0)>0 AND round(ISNULL(unitdiscount,0),2)<>Discount")
        dt = _objcmnbLayer._fldDatatable("Select * from itminvcmntb left join itminvtrtb on itminvtrtb.trid=itminvcmntb.trid " & _
                                         "where isnull(IsPOS,0)<>0 and trtype='IS' AND ISNULL(Discount,0)>0")
        For i = 0 To dtInvs.Rows.Count - 1
            lineDiscountAmt = 0
            qty = 0
            discamt = 0
            actualPrice = 0
            tBAmt = 0
            tBDAmt = 0
            dtItems = SearchGrid(dt, dtInvs(i)("trid"), 0, False, , True)
            For r = 0 To dtItems.Rows.Count - 1
                qty = dtItems(r)("trqty")
                actualPrice = dtItems(r)("UnitCost")
                lineDiscountAmt = dtItems(r)("ItemDiscount")
                tBAmt = tBAmt + (actualPrice * qty) - lineDiscountAmt
                tBDAmt = tBDAmt + (actualPrice * qty) - lineDiscountAmt
            Next
            tDAmt = Val(dtInvs(i)("Discount") & "")
            For r = 0 To dtItems.Rows.Count - 1
                lineDiscountAmt = 0
                qty = 0
                discamt = 0
                actualPrice = 0
                lineDiscountAmt = dtItems(r)("ItemDiscount")
                qty = dtItems(r)("trqty")
                discamt = lineDiscountAmt / qty
                actualPrice = dtItems(r)("UnitCost") - discamt
                If tBDAmt = 0 Then
                    discamt = 0
                Else
                    discamt = (tDAmt * actualPrice) / tBDAmt
                End If
                _objcmnbLayer._saveDatawithOutParm("Update itminvtrtb set unitdiscount=" & discamt & " where id=" & dtItems(r)("Id"))
            Next
        Next
        MsgBox("Done", MsgBoxStyle.Information)
    End Sub

    Private Sub btncorrectbachcostasperTr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncorrectbachcostasperTr.Click
        Dim unitcost As String
        If setTaxAsIncomeExpense Then
            '@UnitCost-@UnitDiscount+@UnitOthCost +((@UnitCost*@taxP)/100)
            unitcost = "(TR.UnitCost-TR.UnitDiscount+TR.UnitOthCost+((TR.UnitCost*taxp)/100))"
        Else
            unitcost = "(isnull(TR.UnitCost,0)-isnull(TR.UnitDiscount,0)+isnull(TR.UnitOthCost,0))-(isnull(TR.ItemDiscount,0)/TR.trqty)"
        End If
        Dim str As String = "Update BatchTb set BatchTb.Cost=" & unitcost & " from BatchTb B INNER JOIN ItmInvTrTb TR ON B.batchTrid=TR.id"
        _objcmnbLayer._saveDatawithOutParm(str)
        MsgBox("Done", MsgBoxStyle.Information)
    End Sub

    Private Sub btnsetBranchAcc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsetBranchAcc.Click
        If cmbBr.Text = "" Then
            MsgBox("Invalid Branch", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Dim dt As DataTable
        Dim accid As Long
        dt = _objcmnbLayer._fldDatatable("SELECT accid,Alias FROM AccMast")
        Dim i As Integer
        For i = 0 To grdVoucher.RowCount - 1
            If grdVoucher.Item(1, i).Value <> "" Then
                Dim _qry = From job In dt.AsEnumerable() Where job!Alias = grdVoucher.Item(1, i).Value Select New With _
                                       {.Name = job!accid}
                If Not _qry.Any Then
                    MsgBox("Invalid Account Name!", MsgBoxStyle.Critical)
                End If
            End If
        Next
        _objcmnbLayer._saveDatawithOutParm("Delete from BranchAccSet where branchcode='" & cmbBr.Text & "'")
        For i = 0 To grdVoucher.RowCount - 1
            If grdVoucher.Item(1, i).Value <> "" And grdVoucher.Item(0, i).Value <> "" Then
                If grdVoucher.Item(1, i).Value <> "" Then
                    Dim _qry = From job In dt.AsEnumerable() Where job!Alias = grdVoucher.Item(1, i).Value Select New With _
                                           {.Name = job!accid}
                    For Each itm In _qry
                        accid = itm.Name
                    Next
                    If accid > 0 Then
                        _objcmnbLayer._saveDatawithOutParm("INSERT INTO BranchAccSet(branchcode,setNo,accid) VALUES('" & cmbBr.Text & "'," & i + 1 & "," & accid & ")")
                    End If
                End If
            End If
        Next
        MsgBox("Updated", MsgBoxStyle.Information)
    End Sub
    Private Sub BranchAccounts()
        If cmbBr.Text = "" Then
            addAccounts()
            Exit Sub
        End If

        hrd(0) = "Stock"
        hrd(1) = "PurDisc(Cr)"
        hrd(2) = "SlsDisc(Dr)"
        hrd(3) = "Service Income"
        hrd(4) = "Service Cash"
        hrd(5) = "Service Bank"
        hrd(6) = "Service PDC"
        hrd(7) = "Bank(PDC Tr.)"
        hrd(8) = "Cost of Sale"
        hrd(9) = "Cost Diff."
        hrd(10) = "POS Cash"
        hrd(11) = "POS Sales"
        hrd(12) = "POS SReturn"
        hrd(13) = "Stock Excess"
        hrd(14) = "Stock Shrtg"
        hrd(15) = "Depreciation"
        hrd(16) = "Chq Bounce A/C"
        hrd(17) = "Meterial Consumptions"
        hrd(18) = "Commission A/C"
        hrd(19) = "Tax Income A/C"
        hrd(20) = "Tax Expense A/C"
        hrd(21) = "Card Payment A/C"
        hrd(22) = "Salary A/C"
        hrd(23) = "Vat Output"
        hrd(24) = "Vat Input"
        hrd(25) = "Input On Import"
        hrd(26) = "Output On Import"
        Dim dt As DataTable = _objcmnbLayer._fldDatatable("SELECT Alias,AccMast.accid,setNo FROM BranchAccSet " & _
                                                          "left join AccMast on BranchAccSet.accid=AccMast.accid where branchcode='" & cmbBr.Text & "'")
        If dt.Rows.Count = 0 Then
            If chgByPrg Then GoTo ld
            If MsgBox("Account Settings not found! Do you want to load Defaults?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                addAccounts()
                Exit Sub
            End If
        End If
ld:
        Dim i As Short
        Dim alisas As String = ""
        With grdVoucher
            .Rows.Clear()
            For i = 0 To 26
                .Rows.Add()
                .Item(0, .Rows.Count - 1).Value = hrd(i)
                alisas = ""
                Dim _qry = From job In dt.AsEnumerable() Where job("setNo") = i + 1 Select New With _
                      {.Name = job!Alias}
                For Each itm In _qry
                    alisas = Trim(itm.Name & "")
                Next
                .Item(1, .Rows.Count - 1).Value = alisas
            Next
        End With
    End Sub

    Private Sub cmbBr_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbBr.SelectedIndexChanged
        If cmbBr.Text = "" Then chgOthrVhr = True
        BranchAccounts()
    End Sub

    Private Sub btnlocqtyupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnlocqtyupdate.Click
        Dim str As String
        Dim dt As DataTable = _objcmnbLayer._fldDatatable("Select LocationID from LocationTb where LocCode='" & Dloc & "'")
        If dt.Rows.Count > 0 Then
            str = "update ItmInvCmnTb set DocDefLoc ='" & Dloc & "' delete from LocOpnQtyTb " & _
             "INSERT INTO LocOpnQtyTb (itemid,LocationID,qty,lastcost)  SELECT itemid," & dt(0)("LocationID") & ",opQty,LastPurchCost from InvItm"
            _objcmnbLayer._saveDatawithOutParm(str)
        End If
        dt = _objcmnbLayer._fldDatatable("Select LocationID from LocationTb where LocCode<>'" & Dloc & "'")
        If dt.Rows.Count > 0 Then
            Dim i As Integer
            For i = 0 To dt.Rows.Count - 1
                str = "INSERT INTO LocOpnQtyTb (itemid,LocationID,qty,lastcost)  SELECT itemid," & dt(i)("LocationID") & ",0,LastPurchCost from InvItm"
                _objcmnbLayer._saveDatawithOutParm(str)
            Next
        End If
        MsgBox("Updated", MsgBoxStyle.Information)
    End Sub

    Private Sub btncorrectOpLocTb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncorrectOpLocTb.Click
        Dim dt As DataTable
        Dim dt1 As DataTable
        dt = _objcmnbLayer._fldDatatable("select * from (select itemid,LocationID,count(itemid) cnt from LocOpnQtyTb " & _
                                         "group by ItemId,LocationID)tr where cnt>1")
        Dim cnt As Integer = dt.Rows.Count
        If dt.Rows.Count > 0 Then
            Dim i As Integer
            For i = 0 To dt.Rows.Count - 1
                dt1 = _objcmnbLayer._fldDatatable("Select top 1 * from LocOpnQtyTb where itemid=" & _
                                             Val(dt(i)("itemid")) & " and LocationID=" & Val(dt(i)("LocationID")))
                _objcmnbLayer._saveDatawithOutParm("Delete from LocOpnQtyTb where itemid=" & _
                                                 Val(dt(i)("itemid")) & " and LocationID=" & Val(dt(i)("LocationID")))
                _objcmnbLayer._saveDatawithOutParm("insert into LocOpnQtyTb (itemid,LocationID,qty,lastcost,locQIH,locationCost) values(" & _
                                                   Val(dt1(0)("itemid")) & "," & Val(dt1(0)("LocationID")) & "," & Val(dt1(0)("qty")) & "," & _
                                                   Val(dt1(0)("lastcost")) & "," & Val(dt1(0)("locQIH")) & "," & Val(dt1(0)("locationCost")) & ")")
            Next

        End If
        MsgBox(cnt & " Records Updated", MsgBoxStyle.Information)
    End Sub

    Private Sub btncorrectOpeningBatch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncorrectOpeningBatch.Click
        _objcmnbLayer.resetBatch(IIf(enableBranch, 0, 1))
        MsgBox("Updated", MsgBoxStyle.Information)
    End Sub

    Private Sub btnupdatelastpurchasecost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdatelastpurchasecost.Click
        Dim frm As New ProcessFormFrm
        With frm
            .typeofProcess = 1
            .ShowDialog()
        End With
    End Sub

    Private Sub grdVchr_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVchr.CellValueChanged
        If chgByPrg Then Exit Sub
        ChgID = True
    End Sub

    Private Sub txtpayrollalert_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpayrollalert.TextChanged

    End Sub

    Private Sub txtfontsize_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtfontsize.TextChanged

    End Sub

    Private Sub grdOtherInfo_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdOtherInfo.CellClick
        If grdOtherInfo.RowCount = 0 Then Exit Sub
        activecontrolname = "grdOtherInfo"
        grdOtherInfo.BeginEdit(True)
    End Sub

    Private Sub grdOtherInfo_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdOtherInfo.CellValueChanged
        If chgByPrg Then Exit Sub
        ChgID = True
    End Sub

    Private Sub grdOtherInfo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdOtherInfo.GotFocus
        activecontrolname = "grdOtherInfo"
    End Sub

    Private Sub grdOtherInfo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdOtherInfo.KeyDown
        With grdOtherInfo
            If e.KeyCode = Keys.Enter Then
                If FindNextCell(grdOtherInfo, .CurrentCell.RowIndex, .CurrentCell.ColumnIndex + 1) Then
                    addOtherInfoRow()
                End If
                chgByPrg = True
                .BeginEdit(True)
                chgByPrg = False
            End If
        End With
    End Sub

    Private Sub grdOtherInfo_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdOtherInfo.Leave
        activecontrolname = ""
    End Sub

    Private Sub btnaddOtherInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddOtherInfo.Click
        addOtherInfoRow()
    End Sub

    Private Sub btnotherinfoRem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnotherinfoRem.Click
        If MsgBox("Do you want to remove the row?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        With grdOtherInfo
            Dim i As Integer
            i = .CurrentRow.Index
            .Rows.RemoveAt(i)
        End With
    End Sub
    Private Sub saveCompanyOtherInfo()
        Dim i As Integer
        Dim str As String
        For i = 0 To grdOtherInfo.Rows.Count - 1
            With grdOtherInfo
                If Val(.Item(constOtherInfoId, i).Value & "") > 0 Then
                    str = "update CompanyOtherInfoTb set otherinfoCap='" & .Item(constOtherInfoCap, i).Value & _
                    "',otherInfoValue='" & .Item(constOtherInfoVal, i).Value & _
                    "' where companyOtherInfoid=" & Val(.Item(constOtherInfoId, i).Value & "")
                Else
                    str = "insert into CompanyOtherInfoTb(otherinfoCap,otherInfoValue) values('" & .Item(constOtherInfoCap, i).Value & "','" & _
                            .Item(constOtherInfoVal, i).Value & "')"
                End If
                _objcmnbLayer._saveDatawithOutParm(str)
            End With
        Next
        loadCompanyOtherInfo()
    End Sub
    Private Sub loadCompanyOtherInfo()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("select * from CompanyOtherInfoTb")
        grdOtherInfo.Rows.Clear()
        For i = 0 To dt.Rows.Count - 1
            With grdOtherInfo
                .Rows.Add()
                .Item(constOtherInfoCap, i).Value = dt(i)("otherinfoCap")
                .Item(constOtherInfoVal, i).Value = dt(i)("otherInfoValue")
                .Item(constOtherInfoId, i).Value = dt(i)("companyOtherInfoid")
            End With
        Next
    End Sub

    Private Sub GroupBox5_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox5.Enter

    End Sub

    Private Sub txtlatefeupto4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtlatefeupto4.KeyPress, txtlatefeaftr4.KeyPress, txtrestructure.KeyPress
        NumericTextOnKeypress(sender, e, chgByPrg, numFormat)
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtlatefeupto4.TextChanged

    End Sub

    Private Sub txtsettlmntaftrduration_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtsettlmntaftrduration.KeyPress, txtsettelmntduration.KeyPress, txtemidate.KeyPress, txtemidate2.KeyPress
        NumericTextOnKeypress(sender, e, chgByPrg, "0")
    End Sub


    Private Sub chkITBIN_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkITBIN.Click
        If chkITBIN.Checked Then
            _objcmnbLayer._saveDatawithOutParm("update CompanyTb set enableitBin=1")
        Else
            _objcmnbLayer._saveDatawithOutParm("update CompanyTb set enableitBin=0")
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        With DlgOpen
            .Filter = "All Picture Files|*.bmp;*.dib;*.gif;*.wmf;*.emf;*.jpg;*.ico;*.cur|" & _
                     "Bitmaps(*.bmp,*.dib)|*.bmp;*.dib|" & _
                     "Gif Images(*.gif)|*.gif|" & _
                     "JPEG Images(*.jpg)|*.jpg|" & _
                     "Matafiles(*.wmf,*.emf)|*.wmf;*.emf|" & _
                     "Icons(*.ico,*.cur)|*.ico;*.cur|" & _
                     "All Files(*.*)|*.*"
            .Title = "Select an Image file"
            .FileName = ""
            .ShowDialog()
            If .FileName <> "" Then
                Err.Clear()
                'On Error Resume Next
                Upload(.FileName)
            End If
            ChDir(Application.StartupPath)
        End With
    End Sub
    Private Sub Upload(ByVal fileName As String)
        'Dim client = New WebClient()
        ''Dim uri = New Uri("http://localhost:65004/Default.aspx")
        'Dim uri = New Uri("https://upload.mosebilling.com")
        ''client.Credentials = New NetworkCredential("uploaduser", "x8f0W%3i4")
        ''client.UploadFile(uri, System.IO.Path.GetFileName(fileName))
        ''MsgBox("")
        'System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
        'client.Headers.Add(HttpRequestHeader.UserAgent, "/")
        ''client.Headers.Add("fileName", System.IO.Path.GetFileName(fileName))
        ''client.UseDefaultCredentials = True
        'client.UploadFile(uri, "PUT", fileName)
        'MsgBox("")

        Dim request As FtpWebRequest = DirectCast(WebRequest.Create(New Uri("ftp://ftp.upload.mosebilling.com/pImages/test1.jpg")), System.Net.FtpWebRequest)
        request.Method = WebRequestMethods.Ftp.UploadFile
        request.Credentials = New NetworkCredential("uploaduser", "x8f0W%3i4")
        request.UseBinary = True
        request.UsePassive = True

        Dim buffer(1023) As Byte
        Dim bytesIn As Long = 1
        Dim totalBytesIn As Long = 0

        Dim filepath As System.IO.FileInfo = New System.IO.FileInfo(fileName)
        Dim ftpstream As System.IO.FileStream = filepath.OpenRead()
        Dim flLength As Long = ftpstream.Length
        Dim reqfile As System.IO.Stream = request.GetRequestStream()

        Do Until bytesIn < 1
            bytesIn = ftpstream.Read(buffer, 0, 1024)
            If bytesIn > 0 Then
                reqfile.Write(buffer, 0, bytesIn)
                totalBytesIn += bytesIn
            End If
        Loop

        reqfile.Close()
        ftpstream.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim fname As String = "ftp://ftp.upload.mosebilling.com/pImages/test1.jpg"
        Dim MyWebClient As New System.Net.WebClient
        MyWebClient.Credentials = New NetworkCredential("uploaduser", "x8f0W%3i4")
        'BYTE ARRAY HOLDS THE DATA
        Dim ImageInBytes() As Byte = MyWebClient.DownloadData(fname)

        'CREATE A MEMORY STREAM USING THE BYTES
        Dim ImageStream As New IO.MemoryStream(ImageInBytes)

        'CREATE A BITMAP FROM THE MEMORY STREAM
        picLogo.Image = New System.Drawing.Bitmap(ImageStream)


        ''FTP Server URL.
        'Dim ftp As String = "ftp://yourserver.com/"
        ''FTP Folder name. Leave blank if you want to list files from root folder.
        'Dim ftpFolder As String = "Uploads/"
        'Try
        '    Dim fileName As String = "Desert.jpg"
        '    'Create FTP Request.
        '    Dim request As FtpWebRequest = CType(WebRequest.Create(ftp & ftpFolder & fileName), FtpWebRequest)
        '    request.Method = WebRequestMethods.Ftp.DownloadFile
        '    'Enter FTP Server credentials.
        '    request.Credentials = New NetworkCredential("Username", "Password")
        '    request.UsePassive = True
        '    request.UseBinary = True
        '    request.EnableSsl = False
        '    'Fetch the Response and read it into a MemoryStream object.
        '    Dim response As FtpWebResponse = CType(request.GetResponse(), FtpWebResponse)
        '    Using stream As MemoryStream = New MemoryStream()
        '        response.GetResponseStream.Write(
        '        Dim base64String As String = Convert.ToBase64String(stream.ToArray(), 0, stream.ToArray().Length)
        '        'Image1.ImageUrl = "data:image/png;base64," & base64String
        '    End Using
        'Catch ex As WebException
        '    Throw New Exception((TryCast(ex.Response, FtpWebResponse)).StatusDescription)
        'End Try
    End Sub

    Private Sub txtftppassword_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtftppassword.KeyDown
        If e.KeyCode = Keys.Return Then
            btnUpdate.Focus()
        End If
    End Sub

    Private Sub txtftpurl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtftpurl.KeyDown, txtftpusername.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub btntestconnection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btntestconnection.Click
        testWebServer()
    End Sub
    Private Sub testWebServer()
        Try

            If ftpurl <> "" And ftpusername <> "" And ftppassword <> "" Then
                Dim imagename As String = Application.StartupPath & "\servertest.png"
                If FileExists(imagename) Then
                    'If MsgBox("Do you want update image to webserver?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.No Then Exit Sub
                    'Dim request As FtpWebRequest = DirectCast(WebRequest.Create(New Uri("ftp://173.214.170.234/servertest.png")), System.Net.FtpWebRequest)
                    Dim request As FtpWebRequest = DirectCast(WebRequest.Create(New Uri(ftpurl & "/servertest.png")), System.Net.FtpWebRequest)
                    request.Method = WebRequestMethods.Ftp.UploadFile
                    request.Credentials = New NetworkCredential(ftpusername, ftppassword)
                    request.UseBinary = True
                    request.UsePassive = True

                    Dim buffer(1023) As Byte
                    Dim bytesIn As Long = 1
                    Dim totalBytesIn As Long = 0

                    Dim filepath As System.IO.FileInfo = New System.IO.FileInfo(imagename)
                    Dim ftpstream As System.IO.FileStream = filepath.OpenRead()
                    Dim flLength As Long = ftpstream.Length
                    Dim reqfile As System.IO.Stream = request.GetRequestStream()

                    Do Until bytesIn < 1
                        bytesIn = ftpstream.Read(buffer, 0, 1024)
                        If bytesIn > 0 Then
                            reqfile.Write(buffer, 0, bytesIn)
                            totalBytesIn += bytesIn
                        End If
                    Loop
                    reqfile.Close()
                    ftpstream.Close()
                End If
                MsgBox("Test connection was successfull", MsgBoxStyle.Information)
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
        Exit Sub
err:
        MsgBox("Test connection failed", MsgBoxStyle.Critical)
    End Sub
End Class