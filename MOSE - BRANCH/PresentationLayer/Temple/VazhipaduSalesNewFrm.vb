Public Class VazhipaduSalesNewFrm
#Region "Private Variables"
    Private chgbyprg As Boolean
    Private chgItm As Boolean
    Private isModi As Boolean
    Private lstKey As Keys
    Private activecontrolname As String
    Private SrchText As String
    Private chgbyprgN As Boolean
    Private chgNumByPgm As Boolean
    Private _srchIndexId As Byte
    Private strGridSrchString As String
    Private _srchOnce As Boolean
    Private _srchTxtId As Byte
    Private chgPost As Boolean
    Private chgAmt As Boolean
    Private loadedTrId As Long
    Private StrAccMastSrch As String
    Private MyActiveControl As New Object
    Private ChgId As Boolean
    Private varProtectedByRights As Boolean
    Private vazhipaduDetId As Long
#End Region
#Region "Class Objet declarations"
    Private _objcmnbLayer As New clsCommon_BL
    Private _objTr As New clsAccountTransaction
    Private _objTempInv As New clsTempleInv
    Private _objInv As New clsInvoice
#End Region
#Region "Constant Variables"
    Private Const ConstSlNo = 0
    Private Const ConstItemCode = 1
    Private Const ConstDescr = 2
    Private Const ConstUnit = 3
    Private Const ConstQty = 4
    Private Const ConstUPrice = 5
    Private Const ConstLTotal = 6
    Private Const ConstItemid = 7
    Private Const ConstPFraction = 8
    Private Const ConstDetid = 9
    Private Const ConstIsacc = 10

    Private Const ConstCSlNo = 0
    Private Const ConstCustomername = 1
    Private Const ConstStar = 2
    Private Const ConstStarid = 3
    Private Const ConstNameid = 4

#End Region
#Region "Form Object Declaration"
    Private WithEvents fMList As Mlistfrm
    Private WithEvents fSelect As Selectfrm
    Private WithEvents fProductEnquiryT As ItmEnqrytemple
    Private WithEvents fProductEnquiry As ItmEnqry
    Private WithEvents fSlctDoc As SelectInvTr
    Private WithEvents fRptFormat As RptFormatfrm
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fCrtAcc As CreateAccNew
    Private WithEvents fSerialno As AddSerialnoFrm
    Private WithEvents fCashCust As CreateCashCustomerFrm
#End Region
    Private Structure JobAccTp
        Dim Amt As Double
        Dim Job As String
        Dim Acc As Long
    End Structure
    Private JobAcc() As JobAccTp
    Private Sub picCloseProd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picCloseProd.Click
        plsrch.Visible = False
    End Sub

    Private Sub VazhipaduSalesNewFrm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If templeVazhipaduCodeSearch Then
            txtvazhipaduCode.Focus()
        Else
            txtvazhipadu.Focus()
        End If

    End Sub

    Private Sub VazhipaduSalesFrm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If Not fProductEnquiryT Is Nothing Then fProductEnquiryT.Close() : fProductEnquiryT = Nothing
    End Sub


    Private Sub VazhipaduSalesFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        StrAccMastSrch = "SELECT AccDescr, Alias, ClosingBal As Balance, OpnBal, AccountNo, S1AccHd.S1AccId, AccSetId, GrpSetOn,accid FROM" & _
                            " AccMast INNER JOIN S1AccHd ON S1AccHd.S1AccId = AccMast.S1AccId "
        SetGridHead()
        SetGridHeadCustomer()
        crtSubVrs(cmbVoucherTp, 10, True)
        cldrdate.Value = Format(Date.Now, DtFormat)
        If isModi Then
            btnModify_Click(btnModify, New System.EventArgs)
            If userType Then
                btnupdate.Tag = IIf(getRight(145, CurrentUser), 1, 0)
                btndelete.Tag = IIf(getRight(147, CurrentUser), 1, 0)
            Else
                btnupdate.Tag = 1
                btndelete.Tag = 1
            End If

            btndelete.Text = "Delete"
        Else
            AddNewClick()
            If userType Then
                btnupdate.Tag = IIf(getRight(146, CurrentUser), 1, 0)
            Else
                btnupdate.Tag = 1
            End If
            btndelete.Text = "Clear"
            btndelete.Tag = 1
        End If
        chksearchwith.Checked = templeStarCodeSearch
        chkItemcode.Checked = templeVazhipaduCodeSearch
    End Sub
    Private Sub SetGridHeadCustomer()
        chgbyprg = True
        With grdCustomernames

            SetEntryGridProperty(grdCustomernames)
            .RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 11)
            .ColumnCount = 5
            .Columns(ConstSlNo).HeaderText = "SlNo"
            .Columns(ConstCSlNo).Width = 40
            '.Columns(ConstSlNo).ReadOnly = False
            .Columns(ConstCSlNo).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(ConstSlNo).Resizable = DataGridViewTriState.False
            .Columns(ConstCSlNo).Frozen = True
            .Columns(ConstCSlNo).DefaultCellStyle.Format = "N0"
            .Columns(ConstCSlNo).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstCSlNo).ReadOnly = True
            .Columns(ConstCSlNo).DefaultCellStyle.BackColor = Color.AliceBlue


            .Columns(ConstCustomername).HeaderText = "Name"
            '.Columns(ConstDescr).Width = Me.Width * 45 / 100
            .Columns(ConstCustomername).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstCustomername).ReadOnly = False
            .Columns(ConstCustomername).Width = 150
            '.Columns(ConstCustomername).DefaultCellStyle.Font = New System.Drawing.Font("Arial", 20)

            .Columns(ConstStar).HeaderText = "Star"
            .Columns(ConstStar).Width = 100
            .Columns(ConstStar).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstStar).ReadOnly = False
            '.Columns(ConstStar).DefaultCellStyle.Font = New System.Drawing.Font("Arial", 20)

            .Columns(ConstStarid).Visible = False
            .Columns(ConstNameid).Visible = False

        End With
        chgbyprg = False
    End Sub
    Private Sub SetGridHead()
        chgbyprg = True
        With grdVoucher

            SetEntryGridProperty(grdVoucher)
            .ColumnCount = 11
            .Columns(ConstSlNo).HeaderText = "SlNo"
            .Columns(ConstSlNo).Width = 40
            '.Columns(ConstSlNo).ReadOnly = False
            .Columns(ConstSlNo).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(ConstSlNo).Resizable = DataGridViewTriState.False
            .Columns(ConstSlNo).Frozen = True
            .Columns(ConstSlNo).DefaultCellStyle.Format = "N0"
            .Columns(ConstSlNo).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstSlNo).ReadOnly = True
            .Columns(ConstSlNo).DefaultCellStyle.BackColor = Color.AliceBlue

            .Columns(ConstItemCode).HeaderText = "ItemCode"
            .Columns(ConstItemCode).Width = 100
            .Columns(ConstItemCode).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstItemCode).ReadOnly = False

            .Columns(ConstDescr).HeaderText = "Description"
            '.Columns(ConstDescr).Width = Me.Width * 45 / 100
            .Columns(ConstDescr).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstDescr).ReadOnly = False
            .Columns(ConstDescr).Width = 150

            .Columns(ConstUnit).HeaderText = "Unit"
            .Columns(ConstUnit).Width = 40
            .Columns(ConstUnit).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstUnit).ReadOnly = True
            .Columns(ConstUnit).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(ConstUnit).DefaultCellStyle.BackColor = Color.Red
            .Columns(ConstUnit).Visible = False

            .Columns(ConstQty).HeaderText = "Qty"
            .Columns(ConstQty).Width = 50
            .Columns(ConstQty).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(ConstQty).Resizable = DataGridViewTriState.False
            .Columns(ConstQty).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstQty).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstQty).ReadOnly = False

            .Columns(ConstUPrice).HeaderText = "Unit Price"
            .Columns(ConstUPrice).Width = 70
            .Columns(ConstUPrice).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(ConstUPrice).Resizable = DataGridViewTriState.False
            '.Columns(ConstUPrice).ValueType=
            .Columns(ConstUPrice).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstUPrice).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstUPrice).ReadOnly = False

            .Columns(ConstLTotal).HeaderText = "Line Total"
            .Columns(ConstLTotal).Width = 80
            .Columns(ConstLTotal).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(ConstLTotal).Resizable = DataGridViewTriState.False
            .Columns(ConstLTotal).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstLTotal).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstLTotal).ReadOnly = True
            .Columns(ConstLTotal).DefaultCellStyle.BackColor = Color.GreenYellow

            .Columns(ConstItemid).Visible = False
            .Columns(ConstPFraction).Visible = False
            .Columns(ConstDetid).Visible = False
            .Columns(ConstIsacc).Visible = False

        End With
        chgbyprg = False
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        If Timer1.Tag = "focus" Then
            grdVoucher.CurrentCell = grdVoucher.Item(5, grdVoucher.CurrentRow.Index)
            grdBeginEdit()
            Timer1.Tag = ""
        Else
            resizeGridColumn(grdVoucher, ConstDescr)
            resizeGridColumn(grdCustomernames, ConstCustomername)
        End If

    End Sub

    Private Sub VazhipaduSalesFrm_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Timer1.Enabled = True
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        If chgPost Then
            If MsgBox("Changes Found ! Do You want to Exit?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        End If
        Me.Close()
    End Sub

    Private Sub cmbVoucherTp_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVoucherTp.SelectedIndexChanged
        If chgbyprg = True Then Exit Sub
        chgbyprg = True
        NextNumber()
        chgbyprg = False
    End Sub
    Private Sub NextNumber()
        'Try
        '    Dim dtInv As DataTable
        '    _objcmnbLayer = New clsCommon_BL
        '    '//next number to Admission
        '    dtInv = _objcmnbLayer._fldDatatable("SELECT Prefix,InvNo FROM InvNos WHERE InvType='IS'")
        '    If dtInv.Rows.Count > 0 Then
        '        numVchrNo.Text = Val(dtInv(0)("InvNo"))
        '    Else
        '        numVchrNo.Text = 1
        '    End If
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
        Try
            Dim vrPrefix As String = ""
            Dim vrVoucherNo As Long
            Dim vrAccountNo1 As Long
            Dim vrAccountNo2 As Long
            With cmbVoucherTp
                If .Items.Count > 0 Then
                    If .SelectedIndex < 0 Then .SelectedIndex = 0
                    cmbVoucherTp.Tag = getvrsId(cmbVoucherTp.Text, 10)
                    getVrsDet(Val(cmbVoucherTp.Tag), "TIS", vrPrefix, vrVoucherNo, vrAccountNo1, vrAccountNo2)
                Else
                    getVrsDet(0, "TIS", vrPrefix, vrVoucherNo, vrAccountNo1, vrAccountNo2)
                End If

            End With
            If Not isModi Then
                numVchrNo.Text = vrVoucherNo
                txtprefix.Text = vrPrefix
            End If
            If Val(txtSuppAlias.Tag) = 0 Then
                txtSuppAlias.Tag = vrAccountNo2
            End If
            If Val(txtPurchAlias.Tag) = 0 Then
                txtPurchAlias.Tag = vrAccountNo1
            End If
            Dim dtAcc As DataTable
            dtAcc = _objcmnbLayer._fldDatatable("SELECT Alias,AccDescr,GrpSetOn,GrpSetOn,accid FROM AccMast " & _
                                                "LEFT JOIN S1AccHd ON AccMast.S1AccId=S1AccHd.S1AccId WHERE accid=" & vrAccountNo1)
            If dtAcc.Rows.Count > 0 Then
                txtPurchaseName.Text = dtAcc(0)("AccDescr")
                txtPurchAlias.Text = dtAcc(0)("Alias")
                txtPurchAlias.Tag = vrAccountNo1
            Else
                txtPurchaseName.Text = ""
                txtPurchAlias.Tag = ""
                txtPurchAlias.Text = ""
            End If
            dtAcc = _objcmnbLayer._fldDatatable("SELECT Alias,AccDescr,GrpSetOn,GrpSetOn,accid FROM AccMast " & _
                                               "LEFT JOIN S1AccHd ON AccMast.S1AccId=S1AccHd.S1AccId WHERE accid=" & vrAccountNo2)
            If dtAcc.Rows.Count > 0 Then
                txtSuppName.Text = dtAcc(0)("AccDescr")
                txtSuppAlias.Tag = vrAccountNo2
                txtSuppAlias.Text = dtAcc(0)("Alias")
                setCustomer(Val(txtSuppAlias.Tag))
            Else
                txtSuppName.Text = ""
                txtSuppAlias.Tag = ""
                txtSuppAlias.Text = ""
            End If
            txtvazhipaduCode.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub setCustomer(Optional ByVal accid As Long = 0)
        Dim dt As DataTable
        If accid > 0 Then
            dt = _objcmnbLayer._fldDatatable("SELECT AccMast.accid,Alias,AccDescr,Address1,Address2,Address3,Address4," & _
                                         "Phone,Fax,EMail,PT1,PT2,PT3,PT4,TrdLcno,TrdDate,CreditLimit,DueDays,SlsmanId,CurrencyCode,CountryCode,GSTIN " & _
                                         " from AccMast LEFT JOIN AccMastAddr ON AccMast.accid=AccMastAddr.AccountNo where accid=" & accid)
        Else
            dt = _objcmnbLayer._fldDatatable("SELECT AccMast.accid,Alias,AccDescr,Address1,Address2,Address3,Address4," & _
                                         "Phone,Fax,EMail,PT1,PT2,PT3,PT4,TrdLcno,TrdDate,CreditLimit,DueDays,SlsmanId,CurrencyCode,CountryCode,GSTIN " & _
                                         " from AccMast LEFT JOIN AccMastAddr ON AccMast.accid=AccMastAddr.AccountNo where Alias='" & txtSuppName.Text & "'")
        End If

        If dt.Rows.Count > 0 Then
            txtSuppAlias.Tag = dt(0)("accid")
            If accid > 0 Then
                txtSuppName.Text = Trim("" & dt(0)("AccDescr"))
                txtSuppAlias.Text = Trim("" & dt(0)("Alias"))
            End If
            txtcustAddress.Text = Trim(dt(0)("Address1") & "")
            If Trim(dt(0)("Address2") & "") <> "" Then
                txtcustAddress.Text = txtcustAddress.Text & vbCrLf & Trim(dt(0)("Address2") & "")
            End If
            If Trim(dt(0)("Address3") & "") <> "" Then
                txtcustAddress.Text = txtcustAddress.Text & vbCrLf & Trim(dt(0)("Address3") & "")
            End If
            If Trim(dt(0)("Address4") & "") <> "" Then
                txtcustAddress.Text = txtcustAddress.Text & vbCrLf & Trim(dt(0)("Address4") & "")
                'txtcustAddress.Text = txtcustAddress.Text & "  GSTIN: " & Trim(dt(0)("GSTIN") & "")
            End If
            'If Trim(dt(0)("GSTIN") & "") <> "" Then
            '    txtcustAddress.Text = txtcustAddress.Text & vbCrLf & "  GSTIN: " & Trim(dt(0)("GSTIN") & "")
            'End If
            If Trim(dt(0)("Phone") & "") <> "" Then
                txtcustAddress.Text = txtcustAddress.Text & vbCrLf & "Ph: " & Trim(dt(0)("Phone") & "")
            End If

        End If
    End Sub

    Private Sub txtReference_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtReference.KeyDown, txtSuppName.KeyDown, txtstar.KeyDown, txtCashCustomer.KeyDown
        lstKey = e.KeyCode
        Dim MyCtrl As TextBox = sender
        If e.KeyCode = Keys.Return Then
            'If MyCtrl.Name = "txtDescr" Then
            '    If grdVoucher.Rows.Count > 0 Then
            '        activecontrolname = "grdVoucher"
            '        grdVoucher.CurrentCell = grdVoucher.Item(1, grdVoucher.CurrentRow.Index)
            '        grdBeginEdit()
            '    Else
            '        AddRow()
            '    End If

            'Else

            'End If
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.F2 Then
            If MyCtrl.Name = "txtCashCustomer" Then
                fCashCust = New CreateCashCustomerFrm
                fCashCust.ShowDialog()
                fCashCust = Nothing
            End If
        ElseIf e.KeyCode = Keys.Up Or e.KeyCode = Keys.PageUp Then
            If fMList.Visible Then
                fMList.MoveUp(MyCtrl.Text)
                Exit Sub
            End If
        ElseIf e.KeyCode = Keys.Down Or e.KeyCode = Keys.PageDown Then
            If Not fMList Is Nothing Then
                If fMList.Visible Then
                    fMList.MoveDown(MyCtrl.Text)
                    Exit Sub
                End If
            End If
        End If
    End Sub

    Private Sub fCashCust_selectcust(ByVal custname As String, ByVal custaddress As String, ByVal Cashcustid As Long, ByVal cardnumber As String, ByVal phone As String, ByVal gstn As String) Handles fCashCust.selectcust

    End Sub
    Private Sub grdBeginEdit()
        chgbyprg = True
        grdVoucher.BeginEdit(True)
        chgbyprg = False
    End Sub
    Private Sub AddRow(Optional ByVal tocheck As Boolean = False)
        Dim i As Integer
        'ChgByPrg = True
        If grdVoucher.RowCount > 0 And Not tocheck Then
            If Val(grdVoucher.Item(ConstItemid, grdVoucher.CurrentRow.Index).Value) = 0 Then Exit Sub
        End If
        With grdVoucher

            i = .RowCount '- 1
            .Rows.Add(1)
            .Item(ConstSlNo, i).Value = i + 1
            .Item(ConstItemCode, i).Value = ""
            .Item(ConstDescr, i).Value = ""
            .Item(ConstUnit, i).Value = ""
            .Item(ConstQty, i).Value = Format(0, numFormat)
            .Item(ConstUPrice, i).Value = Format(0, numFormat)
            .Item(ConstLTotal, i).Value = Format(0, numFormat)
            .CurrentCell = .Item(ConstItemCode, i)
            chgbyprg = True
            .BeginEdit(True)
            chgbyprg = False
            chgItm = False
        End With
        activecontrolname = "grdVoucher"
        calculate()
        reArrangeNo()
        'fProductEnquiry = New ItmEnqrytemple
        'fProductEnquiry.isVazhipadusales = True
        'fProductEnquiry.ShowDialog()
        'ChgByPrg = False

    End Sub
    Private Sub AddCustomerRow()
        Dim i As Integer
        If grdCustomernames.RowCount > 0 Then
            If grdCustomernames.CurrentCell.ColumnIndex = ConstStar Then getStarId()
            If Val(grdCustomernames.Item(ConstStarid, grdCustomernames.CurrentRow.Index).Value) = 0 Then
                grdCustomernames.CurrentCell = grdCustomernames.Item(ConstCustomername, grdCustomernames.RowCount - 1)
                chgbyprg = True
                grdCustomernames.BeginEdit(True)
                chgbyprg = False
                GoTo lst
            End If

        End If
        With grdCustomernames

            i = .RowCount '- 1
            .Rows.Add(1)
            .Item(ConstCSlNo, i).Value = i + 1
            .Item(ConstCustomername, i).Value = ""
            .Item(ConstStar, i).Value = ""
            .CurrentCell = .Item(ConstCustomername, i)
            chgbyprg = True
            .BeginEdit(True)
            chgbyprg = False
        End With
lst:
        activecontrolname = "grdCustomernames"
        calculate()
        reArrangeNoCustomer()
    End Sub
    Private Sub calculate(Optional ByVal bFrmCalcOthCost As Boolean = False)
        Dim totQty As Double
        Dim totItm As Double
        Dim i As Integer

        With grdVoucher
            For i = 0 To .Rows.Count - 1
                If Val(.Item(ConstItemid, i).Value) = 0 Then GoTo nxt
                .Item(ConstSlNo, i).Value = i + 1
                totQty = totQty + IIf(Val(.Item(ConstQty, i).Value) > 0, Val(.Item(ConstQty, i).Value), 0)

                If Val(.Item(ConstUPrice, i).Value & "") = 0 Then
                    .Item(ConstUPrice, i).Value = 0
                End If
                If Val(.Item(ConstQty, i).Value & "") = 0 Then
                    .Item(ConstQty, i).Value = 0
                End If

                totItm = totItm + (.Item(ConstQty, i).Value * CDbl(.Item(ConstUPrice, i).Value))
                .Item(ConstLTotal, i).Value = Format((.Item(ConstQty, i).Value * CDbl(.Item(ConstUPrice, i).Value)), numFormat)
nxt:
            Next
            Dim vazhipaduTotal As Double
            For i = 0 To grdCustomernames.RowCount - 1
                With grdCustomernames
                    If .Item(ConstCustomername, i).Value <> "" Then
                        vazhipaduTotal = vazhipaduTotal + CDbl(txtrate.Text)
                    End If
                End With
            Next
            lblitemtotal.Text = Format(totItm, numFormat)
            lblTotAmt.Text = Format(totItm + vazhipaduTotal, numFormat)
            lblNetAmt.Text = Format(totItm + vazhipaduTotal, numFormat)
            lblvazhipaduTotal.Text = Format(vazhipaduTotal, numFormat)
            If Val(txtroundOff.Text) > 0 Then
                lblNetAmt.Text = Format(CDbl(lblNetAmt.Text) - (IIf(cmbsign.SelectedIndex = 1, 1, -1) * CDbl(txtroundOff.Text)), numFormat)
            End If

        End With
    End Sub
    Private Sub RemoveRow()
        If grdVoucher.RowCount > 0 Then
            If MsgBox("You Are Going to Remove the Row." & vbCrLf & "Are You Sure ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then
                Exit Sub
            End If
            With grdVoucher
                .Rows.RemoveAt(.CurrentRow.Index)
                .ClearSelection()
                calculate()
            End With
            reArrangeNo()
        End If

    End Sub
    Private Sub RemoveCustomerRow()
        If grdCustomernames.RowCount > 0 Then
            If MsgBox("You Are Going to Remove the Row." & vbCrLf & "Are You Sure ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then
                Exit Sub
            End If
            With grdCustomernames
                .Rows.RemoveAt(.CurrentRow.Index)
                .ClearSelection()
            End With
            reArrangeNoCustomer()
            calculate()
        End If

    End Sub
    Private Sub reArrangeNo()
        Dim r As Integer
        Dim i As Integer
        chgbyprg = True
        i = 0
        With grdVoucher
            For r = 0 To .Rows.Count - 1 '- 1
                If CStr(.Item(ConstSlNo, r).Value) <> "M" And CStr(.Item(ConstSlNo, r).Value) <> "L" Then
                    i = i + 1
                    .Item(ConstSlNo, r).Value = i
                End If
            Next r
        End With
        chgbyprg = False
    End Sub
    Private Sub reArrangeNoCustomer()
        Dim r As Integer
        Dim i As Integer
        chgbyprg = True
        i = 0
        With grdCustomernames
            For r = 0 To .Rows.Count - 1 '- 1
                If CStr(.Item(ConstCSlNo, r).Value) <> "M" And CStr(.Item(ConstCSlNo, r).Value) <> "L" Then
                    i = i + 1
                    .Item(ConstCSlNo, r).Value = i
                End If
            Next r
        End With
        chgbyprg = False
    End Sub

    Private Sub cldrdate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cldrdate.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            Const WM_KEYDOWN As Integer = &H100
            Const WM_KEYUP As Integer = &H101
            'msg.ToString()
            If msg.Msg = WM_KEYDOWN Or msg.Msg = WM_KEYUP Then
                If msg.WParam.ToInt32() = CInt(Keys.F8) Then
                    UpdateClick()
                ElseIf msg.WParam.ToInt32() = CInt(Keys.F1) Then
                    If chkItemcode.Checked Then
                        txtvazhipaduCode.Focus()
                    Else
                        txtvazhipadu.Focus()
                    End If
                ElseIf msg.WParam.ToInt32() = CInt(Keys.F5) Then
                    ClearClick()
                ElseIf msg.WParam.ToInt32() = CInt(Keys.F3) Then
                    AddRow()
                ElseIf msg.WParam.ToInt32() = CInt(Keys.F4) Then
                    RemoveRow()
                ElseIf msg.WParam.ToInt32() = CInt(Keys.F6) Then
                        AddCustomerRow()
                ElseIf msg.WParam.ToInt32() = CInt(Keys.F7) Then
                    RemoveCustomerRow()
                ElseIf activecontrolname = "grdVoucher" Then
                    If msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up) Or msg.WParam.ToInt32() = CInt(Keys.Enter) Or msg.WParam.ToInt32() = CInt(Keys.F2) Or msg.WParam.ToInt32() = CInt(Keys.F3) Or msg.WParam.ToInt32() = CInt(Keys.F4) Then
                        If (msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up)) And plsrch.Visible = False Then GoTo ctn
                        grdVoucher_KeyDown(Nothing, New KeyEventArgs(keyData))
                        Return True
                    End If
                ElseIf activecontrolname = "grdCustomernames" Then
                    If msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up) Or msg.WParam.ToInt32() = CInt(Keys.Enter) Or msg.WParam.ToInt32() = CInt(Keys.F2) Or msg.WParam.ToInt32() = CInt(Keys.F6) Or msg.WParam.ToInt32() = CInt(Keys.F7) Then
                        If (msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up)) And plsrch.Visible = False Then GoTo ctn
                        grdCustomernames_KeyDown(Nothing, New KeyEventArgs(keyData))
                        Return True
                    End If
                ElseIf msg.WParam.ToInt32() = CInt(Keys.Escape) Then
                    If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
                    If Not fSelect Is Nothing Then fSelect.Close() : fSelect = Nothing
                    If Not fProductEnquiryT Is Nothing Then fProductEnquiryT.Close() : fProductEnquiryT = Nothing
                    If Not fProductEnquiry Is Nothing Then fProductEnquiry.Close() : fProductEnquiry = Nothing
                    plsrch.Visible = False
                End If
            End If
ctn:
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub ClearControls(Optional ByVal loadNextNumber As Boolean = False)
        On Error Resume Next
        Dim i As Integer
        grdVoucher.RowCount = 0
        grdVoucher.CurrentCell = grdVoucher.Item(1, 0)
        _objcmnbLayer.dtSerialNo.Rows.Clear()
        activecontrolname = ""
        'lstRow = 0
        chgbyprg = True
        txtstar.Text = ""
        txtstar.Tag = ""
        txtReference.Text = ""
        txtDescr.Text = ""
        txtSuppName.Text = ""
        txtSuppAlias.Tag = ""
        txtSuppAlias.Text = ""
        txtPurchAlias.Text = ""
        txtPurchAlias.Text = ""
        txtcustAddress.Text = ""
        chgNumByPgm = True
        txtCashCustomer.Text = ""
        chgNumByPgm = False
        lblvazhipadu.Text = ""
        txtroundOff.Text = Format(0, numFormat)
        dtpvazhipaduDate.Value = DateValue(Date.Now)

        numVchrNo.ReadOnly = True
        numVchrNo.Focus()
        setCustomer()
        txtvazhipadu.Text = ""
        txtvazhipaduCode.Text = ""
        txtrate.Text = Format(0, numFormat)
        txtvazhipaduCode.Tag = 0
        vazhipaduDetId = 0
        grdCustomernames.RowCount = 0
        calculate()
        If loadNextNumber Then NextNumber()
        chgbyprg = False
        chgPost = False

    End Sub
    Private Function AddDetails() As Boolean
        Dim dt As DataTable
        Dim strMyQry As String
        Dim i As Integer
        strMyQry = "SELECT * FROM (SELECT [Item Code],InvItm.Description,UnitPrice as Price,QIH, CostAvg Cost,ItemId,0 isacc,FraCount,Unit FROM InvItm LEFT JOIN UnitsTb ON UnitsTb.Units = InvItm.Unit UNION ALL " & _
                    "SELECT Alias,AccDescr,VazhipaduRate,0,0,AccId,1,0,'NOS' FROM AccMast left join S1AccHd on S1AccHd.S1AccId=AccMast.S1AccId where GrpSetOn='Vazhipadu') Itm " & _
                    "where [Item Code] ='" & SrchText & "'  ORDER BY [Item Code]"
        dt = _objcmnbLayer._fldDatatable(strMyQry)
        If dt.Rows.Count > 0 Then
            With grdVoucher
                chgbyprg = True
                i = .CurrentRow.Index
                .Item(ConstSlNo, i).Value = i + 1
                .Item(ConstItemCode, i).Value = IIf(IsDBNull(dt(0)("Item Code")), Trim(dt(0)("Item Code") & ""), dt(0)("Item Code"))
                .Item(ConstDescr, i).Value = dt(0)("Description")
                .Item(ConstPFraction, i).Value = IIf(IsDBNull(dt(0)("FraCount")), "2", dt(0)("FraCount"))
                .Item(ConstQty, i).Value = Format(1, "#,##0" & IIf(Val(.Item(ConstPFraction, i).Value) = 0, "", "." & Strings.StrDup(CByte(.Item(ConstPFraction, i).Value), "0")))
                .Item(ConstItemid, i).Value = dt(0)("ItemId")
                .Item(ConstIsacc, i).Value = dt(0)("isacc")

                .Item(ConstUnit, i).Value = dt(0)("Unit")
                .Item(ConstUPrice, i).Value = Format(dt(0)("Price"), numFormat)
                .Item(ConstLTotal, i).Value = Format(.Item(ConstQty, i).Value * CDbl(.Item(ConstUPrice, i).Value), numFormat)
                chgAmt = True
                chgItm = False
                .ClearSelection()
                calculate()
                chgbyprg = False
            End With
        Else
            Return False
        End If
        Return True
    End Function
    Private Sub Valid(ByVal RowIndex As Integer, ByVal ColIndex As Integer)
        With grdVoucher
            Select Case ColIndex
                Case ConstItemCode
                    If Not chgItm Then Exit Sub
                    If Trim(.Item(ColIndex, RowIndex).Value) = "" Then Exit Sub
                    Dim found As Boolean
                    If AddDetails() Then
                        found = True
                        chgPost = True
                    End If
                    chgItm = False
                    If Not found Then
                        '.Item(ConstBarcode, RowIndex).Value = ""
                        .Item(ConstItemCode, RowIndex).Value = ""
                        '.Item(ConstBaseID, RowIndex).Value = ""
                        .Item(ConstItemid, RowIndex).Value = ""
                        .Item(ConstUnit, RowIndex).Value = ""
                        '.Item(ConstSerialNo, RowIndex).Value = ""
                        '.Item(ConstPMult, RowIndex).Value = "1"
                        .Item(ConstPFraction, RowIndex).Value = "2"
                        '.Item(ConstImpDocId, RowIndex).Value = ""
                        '.Item(ConstImpLnId, RowIndex).Value = ""
                        chgItm = False
                    End If

                Case ConstQty, ConstUPrice
                    If chgAmt Then
                        calculate()
                        chgPost = True
                    End If
            End Select
        End With
    End Sub
    Private Sub doSelect(ByVal Mup As Integer)
        Try
            chgbyprg = True
            Dim ItmFlds() As String
            If plsrch.Visible = False Then Exit Sub
            If Mup = 0 Then 'UP
                ItmFlds = MoveUpPl(grdSrch, _srchIndexId, strGridSrchString)
            ElseIf Mup = 1 Then 'Down
                ItmFlds = MoveDownPl(grdSrch, _srchIndexId, strGridSrchString)
            Else
                ItmFlds = SelectItmPanel(grdSrch)
            End If
            If strGridSrchString = "" And Mup = 2 Then SrchText = "" : GoTo Nxt
            Select Case _srchTxtId
                Case 1
                    grdVoucher.Item(ConstItemCode, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(0) IsNot Nothing, ItmFlds(0), "")
                    'grdVoucher.Item(ConstBarcode, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(1) IsNot Nothing, ItmFlds(1), "")
                    grdVoucher.Item(ConstDescr, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(2) IsNot Nothing, ItmFlds(1), "")
                    'grdVoucher.Item(ConstIsacc, grdVoucher.CurrentCell.RowIndex).Value = IIf(ItmFlds(6) IsNot Nothing, Val(ItmFlds(6)), "")
                    SrchText = IIf(ItmFlds(0) IsNot Nothing, ItmFlds(0), strGridSrchString)
                Case 2
                    If chksearchwith.Checked Then
                        grdCustomernames.Item(ConstStar, grdCustomernames.CurrentCell.RowIndex).Value = IIf(ItmFlds(0) IsNot Nothing, ItmFlds(0), "")
                    Else
                        grdCustomernames.Item(ConstStar, grdCustomernames.CurrentCell.RowIndex).Value = IIf(ItmFlds(1) IsNot Nothing, ItmFlds(0), "")
                    End If
                    SrchText = IIf(ItmFlds(0) IsNot Nothing, ItmFlds(0), strGridSrchString)
            End Select
nxt:
            chgbyprg = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub grdVoucher_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellClick
        chgbyprg = True
        With grdVoucher
            If Val(grdVoucher.Item(0, grdVoucher.CurrentCell.RowIndex).Value) = 0 And e.ColumnIndex <> 3 Then
                grdVoucher.CurrentCell.ReadOnly = True
            ElseIf e.ColumnIndex <> ConstSlNo And e.ColumnIndex <> ConstUnit And e.ColumnIndex <> ConstLTotal Then
                'If e.ColumnIndex = ConstTaxP And EnableGST Then
                '    grdVoucher.CurrentCell.ReadOnly = True
                'Else
                '    grdVoucher.CurrentCell.ReadOnly = False
                'End If
                grdVoucher.CurrentCell.ReadOnly = False
            Else
                grdVoucher.CurrentCell.ReadOnly = True
            End If
        End With
        grdBeginEdit()
    End Sub


    Private Sub grdVoucher_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellEndEdit
        Dim col As Integer = e.ColumnIndex
        Dim ndec1 As Integer
        If col = ConstQty Or col = ConstUPrice Then
            If col = ConstQty Then
                ndec1 = grdVoucher.Item(ConstPFraction, grdVoucher.CurrentRow.Index).Value
            Else
                ndec1 = 2
            End If
            If Val(grdVoucher.Item(col, e.RowIndex).Value) = 0 Then grdVoucher.Item(col, e.RowIndex).Value = 0
            grdVoucher.Item(col, e.RowIndex).Value = Format(Convert.ToDecimal(grdVoucher.Item(col, e.RowIndex).Value), "#,##0" & IIf(ndec1 = 0, "", "." & Strings.StrDup(ndec1, "0")))
        End If
    End Sub

    Private Sub grdVoucher_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellValidated

        Valid(e.RowIndex, e.ColumnIndex)
        'MsgBox(grdVoucher.Item(ConstItemCode, grdVoucher.CurrentRow.Index).Value)
    End Sub

    Private Sub grdVoucher_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellValueChanged
        If chgbyprg Then Exit Sub
        'ChgByPrg = True
        With grdVoucher
            Dim i As Integer = e.RowIndex
            'btnUpdate.Enabled = True
            chgPost = True
            Select Case e.ColumnIndex
                Case ConstItemCode, ConstBarcode
                    chgItm = True
                Case ConstQty, ConstTaxP, ConstDisAmt, ConstDisP
                    chgAmt = True
                Case ConstDisAmt
                    chgAmt = True
                Case ConstUPrice
                    chgAmt = True
            End Select
        End With
    End Sub

    Private Sub grdVoucher_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdVoucher.EditingControlShowing
        Dim Col As Integer
        Col = grdVoucher.CurrentCell.ColumnIndex
        If Col = ConstItemCode Or Col = ConstQty Then
            Dim tb As TextBox = CType(e.Control, TextBox)
            RemoveHandler tb.TextChanged, AddressOf Textbox_TextChanged
            AddHandler tb.TextChanged, AddressOf Textbox_TextChanged
        End If
        If Col = ConstItemCode Or Col = ConstQty Then
            Dim tb As TextBox = CType(e.Control, TextBox)
            RemoveHandler tb.KeyPress, AddressOf Textbox_KeyPress
            AddHandler tb.KeyPress, AddressOf Textbox_KeyPress
        End If
    End Sub

    Private Sub grdVoucher_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.GotFocus
        If grdVoucher.RowCount = 0 Then
            AddRow()
        End If
        activecontrolname = "grdVoucher"
    End Sub
    Private Sub grdVoucher_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdVoucher.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If grdVoucher.RowCount = 0 Then Exit Sub
                If SrchText = "" And grdVoucher.CurrentCell.ColumnIndex = ConstItemCode Then
                    SrchText = grdVoucher.Item(ConstItemCode, grdVoucher.CurrentRow.Index).Value
                    If SrchText = "" Then GoTo nxt
                End If
                If FindNextCell(grdVoucher, grdVoucher.CurrentCell.RowIndex, grdVoucher.CurrentCell.ColumnIndex + 1) Then
                    AddRow()
                End If
nxt:
                plsrch.Visible = False
                grdBeginEdit()

            ElseIf e.KeyCode = Keys.Up Or e.KeyCode = Keys.PageUp Then
                If grdVoucher.RowCount = 0 Then Exit Sub
                If plsrch.Visible Then
                    doSelect(0)
                    Exit Sub
                End If
            ElseIf e.KeyCode = Keys.Down Or e.KeyCode = Keys.PageDown Then
                If grdVoucher.RowCount = 0 Then Exit Sub
                If plsrch.Visible Then
                    doSelect(1)
                    Exit Sub
                End If
            ElseIf e.KeyCode = Keys.F2 Then
                If plsrch.Visible Then plsrch.Visible = False
                If grdVoucher.RowCount = 0 Then Exit Sub
                Select Case grdVoucher.CurrentCell.ColumnIndex
                    Case ConstItemCode
                        If Not fMList Is Nothing Then fMList.Visible = False
                        If plsrch.Visible Then plsrch.Visible = False
                        fProductEnquiry = New ItmEnqry
                        fProductEnquiry.isVazhipadusales = True
                        fProductEnquiry.ShowDialog()
                End Select
            ElseIf e.KeyCode = Keys.Escape Then
                plsrch.Visible = False
            ElseIf e.KeyCode = Keys.F3 Then
                AddRow()
            ElseIf e.KeyCode = Keys.F4 Then
                RemoveRow()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub txtCashCustomer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtReference.TextChanged, txtcustAddress.TextChanged, txtDescr.TextChanged, txtstar.TextChanged, txtCashCustomer.TextChanged
        If chgbyprg = True Then Exit Sub
        Dim myCtrl As Control = sender
        Select Case myCtrl.Name
            Case "txtstar"
                _srchTxtId = 1
                _srchOnce = False
                ShowFmlist(sender)
        End Select
        chgPost = True
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
            'Dim x As Integer = Me.Width - fMList.Width - 100
            Dim x As Integer = Me.Left + 110
            Dim y As Integer = Me.Height - fMList.Height - 50
            PopupLoc = New Point(x, y)
            If fMList.Visible = False Then
                Select Case _srchTxtId
                    Case 1
                        SetFmlist(fMList, 24)
                    Case 2, 3
                        SetFmlist(fMList, 27)
                End Select
                fMList.loc = PopupLoc
                fMList.Show(fMainForm)
                fMList.Visible = True
            End If
        End If
        Select Case _srchTxtId
            Case 1   'Star
                fMList.SearchIndex = 0
                fMList.SearchIndexDescr = 1
                fMList_doFocus()
                fMList.Search(txtstar.Text)
                fMList.AssignList(txtstar, lstKey, chgbyprg)
            Case 2   'Vazhipadu
                fMList.SearchIndex = 1
                fMList.SearchIndexDescr = 0
                fMList_doFocus()
                fMList.Search(txtvazhipaduCode.Text)
                fMList.AssignList(txtvazhipaduCode, lstKey, chgbyprg)
            Case 3   'Vazhipadu
                fMList.SearchIndex = 0
                fMList.SearchIndexDescr = 1
                fMList_doFocus()
                fMList.Search(txtvazhipadu.Text)
                txtvazhipaduCode.Text = fMList.AssignList(txtvazhipadu, lstKey, chgbyprg)
        End Select
        _srchOnce = True
        chgbyprg = False
    End Sub

    Private Sub btnrem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrem.Click
        RemoveRow()
        chgPost = True
    End Sub
    Private Sub ShowPanel(Optional ByVal isStar As Boolean = False)
        If Not _srchOnce Then
            chgbyprg = True
            Dim PopupLoc As Point
            Dim x As Integer
            Dim y As Integer
            If isStar Then
                x = grdVoucher.Left
                y = grdVoucher.Top
            Else
                x = Me.Width - plsrch.Width - 100
                y = Me.Height - plsrch.Height - 100
            End If

            PopupLoc = New Point(x, y)
            If plsrch.Visible = False Then
                plsrch.Location = PopupLoc
                plsrch.Visible = True
            End If
        End If
        If isStar Then
            SearchStarPanel(grdSrch, IIf(chksearchwith.Checked, "[starid]", "[starname]"), strGridSrchString)
        Else
            SearchItmAccPanel(grdSrch, "[Item Code]", strGridSrchString, True)
        End If

        doSelect(2)
        _srchOnce = True
        chgbyprg = False
    End Sub
    Private Sub Textbox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            Dim col As Integer
            col = grdVoucher.CurrentCell.ColumnIndex
            If col = ConstQty Or col = ConstUPrice Or col = ConstLTotal Then
                If Not IsNumeric(e.KeyChar) And e.KeyChar <> "." And Not e.KeyChar = Convert.ToChar(Keys.Back) Then e.Handled = True
            ElseIf col = ConstSerialNo Then
                e.Handled = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub Textbox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Try
            Dim col As Integer
            col = grdVoucher.CurrentCell.ColumnIndex
            Dim MyCtrl As TextBox = sender
            If col = ConstItemCode Or ConstDescr Then strGridSrchString = MyCtrl.Text
            If chgbyprg Then Exit Sub
            _srchOnce = False
            If col = ConstItemCode Then
                _srchTxtId = 1
                chgbyprg = True
                strGridSrchString = MyCtrl.Text
                ShowPanel()
                chgItm = True
                chgbyprg = False
                'grdVoucher.Item(ConstqtyChg, grdVoucher.CurrentRow.Index).Value = "Chg"
            ElseIf col = ConstBarcode Then
                _srchTxtId = 2
                chgbyprg = True
                strGridSrchString = MyCtrl.Text
                ShowPanel()
                chgbyprg = False
            ElseIf col = ConstQty Then
                'grdVoucher.Item(ConstqtyChg, grdVoucher.CurrentRow.Index).Value = "Chg"
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub NameTextbox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim col As Integer
            col = grdCustomernames.CurrentCell.ColumnIndex
            Dim MyCtrl As TextBox = sender
            If chgbyprg Then Exit Sub
            SrchText = MyCtrl.Text
            If chgbyprg Then Exit Sub
            _srchOnce = False
            If col = ConstStar Then
                _srchTxtId = 2
                chgbyprg = True
                strGridSrchString = MyCtrl.Text
                ShowPanel(True)
                chgbyprg = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub grdVoucher_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.Leave
        activecontrolname = ""
    End Sub

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        AddRow()
        grdVoucher.CurrentCell = grdVoucher.Item(ConstItemCode, grdVoucher.CurrentRow.Index)
        'doCommandStat(True)
        chgPost = True
    End Sub

    Private Sub fProductEnquiryT_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fProductEnquiryT.FormClosed
        fProductEnquiryT = Nothing
    End Sub


    Private Sub Verify()
        Dim _vAcMaster As DataTable
        clsreader()
        clsCnnection()
        If isModi Then
            'numVchrNo.Text = numVchrNo.Tag
            If chgPost = False Then
                'MsgBox("Changes not found !!", vbExclamation)
                'numVchrNo.Focus()
                'Exit Sub
            Else
                If loadedTrId = 0 Then
                    MsgBox("Voucher not yet loaded !!", vbExclamation)
                    numVchrNo.Focus()
                    Exit Sub
                End If
            End If
        End If
        calculate()
        If Val(numVchrNo.Text) < 1 Then
            MsgBox("Invalid Voucher Number !!", vbExclamation)
            numVchrNo.Focus()
            Exit Sub
        End If
        If Not IsDate(cldrdate.Value) Then
            MsgBox("Invalid Voucher Date !!", vbExclamation)
            cldrdate.Focus()
            Exit Sub
        End If
        If DateValue(cldrdate.Value) <= ProtectUntil Then
            MsgBox("Voucher Date comes within Protected Date Range !!", MsgBoxStyle.Information)
            cldrdate.Focus()
            Exit Sub
        End If
        _vAcMaster = _objcmnbLayer._fldDatatable(StrAccMastSrch & " WHERE Alias = '" & txtSuppAlias.Text & "' ORDER BY AccDescr")
        If _vAcMaster.Rows.Count = 0 Then
            MsgBox("Enter a valid  Customer Account !!", vbExclamation)
            txtSuppName.Focus()
            'txtSuppAlias.Focus()
            Exit Sub
        Else
            txtSuppAlias.Tag = _vAcMaster(0)("accid")
        End If
        If Val(txtPurchAlias.Tag) = 0 Then
            MsgBox("Sales A/C could not found!", MsgBoxStyle.Exclamation)
            cmbVoucherTp.Focus()
            Exit Sub
        End If
        'If Not isModi Then
        '    If chekDuplicate() Then Exit Sub
        'End If
        'Dim dt As DataTable
        'If txtReference.Text <> "" Then
        '    dt = _objcmnbLayer._fldDatatable("SELECT Trid from TempleSalesCmnTb where Reference='" & txtReference.Text & "' and Trid<>" & loadedTrId)
        '    If dt.Rows.Count > 0 Then
        '        MsgBox("Reference already exist!", MsgBoxStyle.Exclamation)
        '        txtReference.Focus()
        '        Exit Sub
        '    End If
        'End If

        'If CheckRestrictImport() = False Then Exit Sub
        If Not chkGridvalue() Then Exit Sub
        If CDbl(lblNetAmt.Text) < 0 Then
            MsgBox("Net Amount below Zero is not allowed !!?", vbExclamation)
            numVchrNo.Focus()
            Exit Sub
        End If
        saveTrans()
    End Sub
    Private Sub setInvCmnValue(ByVal trid As Long)
        Dim Dt As Date
        With _objInv
            Dt = DateValue(Date.Now)
            .TrId = trid
            .TrDate = DateValue(cldrdate.Value)
            .TrType = "TIS"
            .DocLstTxt = ""
            .InvTypeNo = Val(cmbVoucherTp.Tag)
            .SlsManId = ""
            .Prefix = Trim(txtprefix.Text)
            .InvNo = Val(numVchrNo.Text)
            .TrRefNo = Trim(txtprefix.Text) & IIf(txtprefix.Text = "", "", "/") & numVchrNo.Text ' Trim(txtReference.Text)
            .CSCode = Val(txtSuppAlias.Tag)
            .PSAcc = Val(txtPurchAlias.Tag)
            .JobCode = ""
            .ImpDoc = 0
            .UserId = CurrentUser
            .IsFC = False
            .FCRate = 1
            .NFraction = 2
            .FC = ""
            .Discount = 0
            .TrDescription = Trim(txtDescr.Text)
            .TypeNo = getVouchernumber("TIS")
            .EnaJob = False
            .DocDefLoc = ""
            .BrId = ""
            .OthCost = 0
            .Discount1 = 0
            .NetAmt = CDbl(lblNetAmt.Text)
            .LPO = txtReference.Text
            'Dim dt As Date = getServerDate()
            .CrtDt = Dt
            .DelDate = Dt
            .DueDate = Dt
            .DocDate = Dt
            .SuppInvDate = Dt
            .TermsId = ""
            .MchName = MACHINENAME
            .isModi = IIf(trid > 0, True, False)
            .lpoclass = ""
            .rndoff = txtroundOff.Text * IIf(cmbsign.SelectedIndex = 1, -1, 1)
            'If TaxType is 1 then invoice is interstate invoice otherwise intrastate invoice
            .TaxType = 0
            .OthrCust = txtcustAddress.Text
        End With
    End Sub
    Private Sub setInvDetValue(ByVal trid As Long, ByVal i As Integer)
        With grdVoucher

            _objInv.dtTrId = trid
            _objInv.ItemId = IIf(.Item(ConstSlNo, i).Value.ToString <> "M", Val(.Item(ConstItemid, i).Value), 0)
            _objInv.Unit = .Item(ConstUnit, i).Value
            _objInv.TrQty = CDbl(.Item(ConstQty, i).Value)
            _objInv.UnitCost = CDbl(.Item(ConstUPrice, i).Value)
            _objInv.taxP = 0
            _objInv.taxAmt = 0
            _objInv.PFraction = 1
            _objInv.UnitOthCost = 0
            _objInv.Method = 1
            _objInv.UnitDiscount = 0
            _objInv.ItemDiscount = 0
            _objInv.DisP = 0
            If Not IsDBNull(.Item(ConstDescr, i).Value) Then
                _objInv.IDescription = IIf(.Item(ConstSlNo, i).Value.ToString = "M", .Item(ConstDescr, i).Value, Trim(.Item(ConstDescr, i).Value))
            End If
            _objInv.SlNo = i + 1
            _objInv.TrTypeNo = getVouchernumber("TIS")
            _objInv.TrDateNo = getDateNo(CDate(cldrdate.Value))
            _objInv.TrType = "TIS"
            _objInv.id = Val(.Item(ConstDetid, i).Value)
            _objInv.WarrentyName = ""
            _objInv.SerialNo = ""
            _objInv.WarrentyExpDate = DateValue("01/01/1950")
            _objInv.HSNCode = Trim(.Item(ConstBarcode, i).Value & "")
            _objInv.IGSTP = 0
            _objInv.IGSTAmt = 0
            _objInv.CSGTP = 0
            _objInv.CGSTAMT = 0
            _objInv.SGSTP = 0
            _objInv.SGSTAmt = 0
            _objInv.Smancode = ""
            If _objInv.ItemId = 0 Then
                _objInv.TrQty = 1
            End If
        End With
    End Sub
    Private Sub setTMPCmnValue(ByVal trid As Long)
        With _objTempInv
            .Trid = trid
            .Trdate = DateValue(cldrdate.Value)
            .Prefix = txtprefix.Text
            .InvNo = Val(numVchrNo.Text)
            .Reference = txtReference.Text
            .salesAc = Val(txtPurchAlias.Tag)
            .CustomerAc = Val(txtSuppAlias.Tag)
            .TrDescription = txtDescr.Text
            .rndoff = Val(txtroundOff.Text)
            .VoucherTypeid = Val(cmbVoucherTp.Tag)
            .NetTot = CDbl(lblNetAmt.Text)
            .UserId = CurrentUser
            .CashCustAddr = txtcustAddress.Text
            .vazhipaduTotal = CDbl(lblvazhipaduTotal.Text)
            .ItemTotal = CDbl(lblitemtotal.Text)
            If grdCustomernames.RowCount > 0 Then
                .CashCustName = UCase(grdCustomernames.Item(ConstCustomername, 0).Value)
                .StarId = Val(grdCustomernames.Item(ConstStarid, 0).Value)
            Else
                .CashCustName = ""
                .StarId = 0
            End If
            
        End With
    End Sub
    Private Sub saveTMPDetValueForVazhipadu(ByVal trid As Long)
        If Val(txtvazhipaduCode.Tag) = 0 Then Exit Sub
        _objTempInv.Detid = vazhipaduDetId
        _objTempInv.Trid = trid
        _objTempInv.SlNo = 1
        _objTempInv.Itemid = 0
        _objTempInv.Acid = Val(txtvazhipaduCode.Tag)
        _objTempInv.ItemDescription = txtvazhipadu.Text
        _objTempInv.VazhipaduDate = DateValue(dtpvazhipaduDate.Value)
        _objTempInv.Unit = "PCS"
        If Val(txtrate.Text) > 0 Then
            _objTempInv.Qty = CDbl(lblvazhipaduTotal.Text) / CDbl(txtrate.Text)
        Else
            _objTempInv.Qty = 0
        End If

        _objTempInv.Rate = CDbl(txtrate.Text)
        _objTempInv.Isacc = True
        _objTempInv.isTempleNonVazhipaduItem = 0
        _objTempInv.TempleSalesDetTbSaveModify()
    End Sub
    Private Sub setTMPDetValue(ByVal trid As Long, ByVal i As Integer)
        With grdVoucher
            _objTempInv.Detid = Val(.Item(ConstDetid, i).Value)
            _objTempInv.Trid = trid
            _objTempInv.SlNo = i + 1 + IIf(Val(txtvazhipaduCode.Tag) > 0, 1, 0)
            _objTempInv.Itemid = 0
            _objTempInv.Acid = 0
            If Val(.Item(ConstIsacc, i).Value) = 0 Then
                _objTempInv.Itemid = Val(.Item(ConstItemid, i).Value)
            Else
                _objTempInv.Acid = Val(.Item(ConstItemid, i).Value)
            End If
            _objTempInv.ItemDescription = .Item(ConstDescr, i).Value
            _objTempInv.VazhipaduDate = DateValue(dtpvazhipaduDate.Value)
            _objTempInv.Unit = .Item(ConstUnit, i).Value
            _objTempInv.Qty = CDbl(.Item(ConstQty, i).Value)
            _objTempInv.Rate = CDbl(.Item(ConstUPrice, i).Value)
            _objTempInv.Isacc = IIf(Val(.Item(ConstIsacc, i).Value) > 0, True, False)
            _objTempInv.isTempleNonVazhipaduItem = 1
        End With
    End Sub
    Private Function saveInventory(ByVal loadedInvTrId As Long, ByVal TsalesTrid As Long) As Long
        Dim trid As Long
        Dim i As Integer
        Dim dateChanged As Boolean
        Dim qtychanged As Boolean
        Dim dtTable As DataTable

        clsreader()
        clsCnnection()
        If isModi Then
            dtTable = _objcmnbLayer._fldDatatable("SELECT TOP 1 * FROM ItmInvCmnTb WHERE TrId = " & loadedInvTrId)
            If dtTable.Rows.Count = 0 Then
                trid = 0
            Else
                _objcmnbLayer._saveDatawithOutParm("Update ItmInvTrTb set setRemove=1 WHERE trid=" & loadedInvTrId)
                trid = loadedInvTrId
            End If
        End If
        setInvCmnValue(trid)
        trid = Val(_objInv._saveCmn())
        _objcmnbLayer._saveDatawithOutParm("Update TempleSalesCmnTb set InvTrid=" & trid & " WHERE trid=" & TsalesTrid)
        'to check whether date has been changed or not
        'if changed there should be calculeted cost average for all items
        dtTable = _objcmnbLayer._fldDatatable("SELECT TOP 1 * FROM ItmInvCmnTb LEFT JOIN VoucherTypeNoTb ON ItmInvCmnTb.TypeNo=VoucherTypeNoTb.vrno " & _
                                                      "WHERE InvType='OUT' AND Trdate >='" & Format(DateValue(cldrdate.Value), "yyyy/MM/dd") & "'")
        If dtTable.Rows.Count > 0 Then
            dateChanged = True
        Else
            dateChanged = False
        End If
        Dim itemidsdatatable As New DataTable
        itemidsdatatable.Columns.Add(New DataColumn("Itemid", GetType(Long)))
        With grdVoucher
            For i = 0 To .Rows.Count - 1 '- 1
                If CDbl(.Item(ConstQty, i).Value) <> 0 And Val(.Item(ConstItemid, i).Value) <> 0 Then
                    setInvDetValue(trid, i)
                    _objInv._saveDetails()
                    qtychanged = True
                    If (dateChanged Or (qtychanged And Val(.Item(ConstDetid, i).Value) > 0)) And enableRealtimeCosting Then
                        _objInv.ItemId = Val(.Item(ConstItemid, i).Value)
                        _objInv.TrDate = DateValue(cldrdate.Value)
                        _objInv.setcostAverageOnModification(UsrBr)
                    End If
                End If
            Next
            If isModi Then
                _objInv.deleteInventoryRelatedItemDetails(loadedInvTrId)
                itemidsdatatable = _objcmnbLayer._fldDatatable("select itemid from ItmInvTrTb where setRemove=1 and trid=" & loadedInvTrId)
                _objcmnbLayer._saveDatawithOutParm("delete from ItmInvTrTb where setRemove=1 and trid=" & loadedInvTrId)
                If itemidsdatatable.Rows.Count > 0 And enableRealtimeCosting Then
                    For i = 0 To itemidsdatatable.Rows.Count - 1
                        _objInv.ItemId = itemidsdatatable(i)("Itemid")
                        _objInv.TrDate = DateValue(cldrdate.Value)
                        _objInv.setcostAverageOnModification(UsrBr)
                    Next
                End If
            End If
        End With
        Return trid
    End Function
    Private Sub saveTrans()
        Dim TrId As Long
        Dim i As Integer
        Dim DiscAcc As Long
        Dim PPerU As Single
        Dim TDrAmt As Double
        Dim dtTable As DataTable
        Dim invTid As Long
        clsreader()
        clsCnnection()
        If Not isModi Then
chkagain:
            If Not CheckNoExists(txtprefix.Text, Val(numVchrNo.Text), "TIS", "TemapleSales") Then
                If MsgBox("Document No alreary exist! Do you want to fill next?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Exit Sub
                Else
                    numVchrNo.Text = Val(numVchrNo.Text) + 1
                    GoTo chkagain
                End If
            End If
        End If
        calculate()
        If isModi Then
            dtTable = _objcmnbLayer._fldDatatable("SELECT TOP 1 * FROM TempleSalesCmnTb WHERE TrId = " & loadedTrId)
            If dtTable.Rows.Count = 0 Then
                MsgBox("The loaded voucher not found.  It may removed externally.", vbExclamation)
                Exit Sub
            Else
                invTid = Val(dtTable(0)("InvTrid") & "")
            End If
            _objcmnbLayer._saveDatawithOutParm("Update TempleSalesDetTb set setRemove=1 WHERE trid=" & loadedTrId)
            _objcmnbLayer._saveDatawithOutParm("Update DevoteeNameTb set setRemove=1 WHERE trid=" & loadedTrId)
            TrId = loadedTrId
        End If
        setTMPCmnValue(TrId)
        TrId = Val(_objTempInv.TempleSalesCmnTbSaveAndModify())
        Dim itemidsdatatable As New DataTable
        itemidsdatatable.Columns.Add(New DataColumn("Itemid", GetType(Long)))
        ReDim JobAcc(1)
        JobAcc(0).Acc = Val(txtPurchAlias.Tag)
        JobAcc(0).Job = ""

        JobAcc(1).Acc = Val(txtvazhipaduCode.Tag)
        JobAcc(1).Job = ""
        JobAcc(1).Amt = CDbl(lblvazhipaduTotal.Text)
        saveTMPDetValueForVazhipadu(TrId)
        With grdVoucher
            For i = 0 To .Rows.Count - 1 '- 1
                If CDbl(.Item(ConstQty, i).Value) <> 0 Or .Item(ConstSlNo, i).Value.ToString = "M" Then
                    PPerU = 1 'IIf(PPerU = 0, 1, PPerU)
                    TDrAmt = TDrAmt + CDbl(.Item(ConstQty, i).Value) * CDbl(.Item(ConstUPrice, i).Value)
                    setTMPDetValue(TrId, i)
                    _objTempInv.TempleSalesDetTbSaveModify()
                End If
            Next
            saveDevotee(TrId)
            If isModi Then
                _objcmnbLayer._saveDatawithOutParm("delete from TempleSalesDetTb where setRemove=1 and trid=" & loadedTrId)
                _objcmnbLayer._saveDatawithOutParm("delete from DevoteeNameTb where setRemove=1 and trid=" & loadedTrId)
            End If
        End With
        'If TDrAmt > 0 Then
        '    invTid = saveInventory(invTid, TrId)
        'End If
        JobAcc(0).Amt = TDrAmt
        TDrAmt = TDrAmt + CDbl(lblvazhipaduTotal.Text)
        UpdateAccounts(TrId, TDrAmt, DiscAcc, invTid)
        If isModi = False Then
            numPrintVchr.Text = numVchrNo.Text
            txtPPrefix.Text = txtprefix.Text
            numVchrNo.Tag = Val(numVchrNo.Text) 'Trim(CMNREC(12).FormattedText)
            txtprefix.Tag = txtprefix.Text
            txtprefix.Text = SetNextVrNo(numVchrNo, Val(cmbVoucherTp.Tag), "TIS", "TrType = 'TIS' AND InvNo = ", False, True, True, 2)
        End If
        If isModi Then
            btnModify_Click(btnModify, New System.EventArgs)
        Else
            AddNewClick()
        End If
        If MsgBox("Sales Invoice is succesfully posted with Vr. # " & numPrintVchr.Text & "." & vbCrLf & "Do you want print?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            PrepareRpt("VISM", True)
        End If
        numVchrNo.Tag = ""
    End Sub
    Private Sub AddNewClick()
        If chgbyprg Then Exit Sub
        If ChgId Then
            Select Case MsgBox("Changes found !!  Do you want hold changes ?", vbYesNoCancel + vbQuestion)
                Case vbYes
                    'Hold procedure
                Case vbNo
                Case Else
                    Exit Sub
            End Select
        End If
        ClearControls()
        isModi = False
        btnModify.Text = "&Modify"
        btndelete.Text = "Clear"
        isModi = False
        btnSlct.Visible = False
        numVchrNo.ReadOnly = True
        If userType Then
            btnupdate.Tag = IIf(getRight(145, CurrentUser), 1, 0)
        Else
            btnupdate.Tag = 1
        End If
        btndelete.Text = "Clear"
        btndelete.Tag = 1
        cmbVoucherTp_SelectedIndexChanged(cmbVoucherTp, New System.EventArgs)
        'btnNext.Visible = True
    End Sub

    Private Sub ClearClick()
        If MsgBox("Do You Want Clear Etry", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        ClearControls(True)
    End Sub
    Private Sub setAcctrCmnValue(ByVal InvTrid As Long, ByVal LinkNo As Long)
        _objTr.JVType = "TIS"
        _objTr.JVDate = DateValue(cldrdate.Value)
        _objTr.PreFix = Trim(txtprefix.Text)
        _objTr.JVNum = Val(numVchrNo.Text)
        _objTr.JVTypeNo = getVouchernumber("TIS")
        _objTr.UserId = Trim(CurrentUser & "")
        _objTr.MchName = MACHINENAME
        _objTr.CrtDtTm = Date.Now
        _objTr.TypeNo = getVouchernumber("TIS")
        '_objTr.VrDescr = Trim(txtpayee.Text)
        _objTr.IsModi = IIf(LinkNo > 0, 2, 0)
        _objTr.LinkNo = LinkNo
    End Sub
    Private Sub setAcctrDetValue(ByVal lnkNo As Long, ByVal jbIndex As Integer)
        With _objTr
            .trLinkNo = lnkNo
            .AccountNo = JobAcc(jbIndex).Acc
            .Reference = Trim(txtprefix.Text) & IIf(txtprefix.Text = "", "", "/") & numVchrNo.Text
            .EntryRef = Strings.Left(Trim(txtDescr.Text) & IIf(JobAcc(jbIndex).Job = "", "", " / " & JobAcc(jbIndex).Job) & " / " & txtSuppName.Text, 249)
            .DealAmt = -1 * JobAcc(jbIndex).Amt
            .FCAmt = -1 * JobAcc(jbIndex).Amt
            .JobCode = JobAcc(jbIndex).Job
            .JobStr = ""
            .CurrRate = 1
            .CurrencyCode = ""
            .TrInf = 0
            .OthCost = 0
            .TermsId = ""
            .CustAcc = Val(txtSuppAlias.Tag)
            .AccWithRef = JobAcc(jbIndex).Acc & txtReference.Text
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
            .Reference = Trim(txtprefix.Text) & IIf(txtprefix.Text = "", "", "/") & numVchrNo.Text
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
            Dim dtLPO As Date = IIf(chkDate(cldrdate.Value), cldrdate.Value, DateValue(cldrdate.Value))
            Dim dtDue As Date = DateValue(cldrdate.Value)
            Dim dtSup As Date = DateValue(cldrdate.Value)
            .DocDate = dtLPO
            .SuppInvDate = dtSup
            .DueDate = dtDue
        End With
    End Sub
    Private Sub updateStockTransaction(ByVal trid As Long, ByVal LinkNo As Long)
        If Not enableCostAccounting Then Exit Sub
        Dim stockAc As Long
        Dim costOfSalesAc As Long
        Dim dt As DataTable
        Dim costAmt As Double
        stockAc = getConstantAccounts(1)
        costOfSalesAc = getConstantAccounts(9)
        If stockAc = 0 Or costOfSalesAc = 0 Then Exit Sub
        dt = _objcmnbLayer._fldDatatable("select sum(CostAvg*TrQty) costAmt from ItmInvTrTb  where trid=" & trid)
        If dt.Rows.Count > 0 Then
            If IsDBNull(dt(0)("costAmt")) Then dt(0)("costAmt") = 0
            costAmt = dt(0)("costAmt")
        End If
        If costAmt <> 0 Then
            'debit entry [cost of sales]
            Dim entryref As String = "COST OF SALES FOR INVOICE : " & txtSuppName.Text & " # " & Trim(txtprefix.Text) & numVchrNo.Text
            setAcctrDetValue(LinkNo, costOfSalesAc, Trim(txtReference.Text), entryref, costAmt, "", "", 3, 0, "", _
                           "", Val(txtSuppAlias.Tag), costOfSalesAc & txtReference.Text, "", 1)
            _objTr.saveAccTrans()
            'UpdtClosBal(costOfSalesAc, costAmt)
            'credit entry [stock in hand]
            costAmt = costAmt * -1
            setAcctrDetValue(LinkNo, stockAc, Trim(txtReference.Text), entryref, costAmt, "", "", 3, 0, "", _
                           "", Val(txtSuppAlias.Tag), stockAc & txtReference.Text, "", 1)
            _objTr.saveAccTrans()
            'UpdtClosBal(stockAc, costAmt)
        End If
    End Sub
    Private Sub UpdateAccounts(ByVal TrId As Long, ByVal TDrAmt As Double, ByVal DiscAcc As Integer, ByVal InvTrid As Long)
        Dim LinkNo As Long
        Dim dtTable As DataTable
        If isModi And TrId > 0 Then
            dtTable = _objcmnbLayer._fldDatatable("SELECT LinkNo FROM AccTrCmn WHERE JVType='TIS' AND JVNum=" & numVchrNo.Text & " AND PreFix='" & txtPPrefix.Text & "'")
            If dtTable.Rows.Count > 0 Then
                LinkNo = dtTable(0)("LinkNo")
                _objcmnbLayer._saveDatawithOutParm("DELETE FROM AccTrDet WHERE LinkNo=" & LinkNo)
            End If
        End If

        setAcctrCmnValue(TrId, LinkNo)
        LinkNo = 0
        LinkNo = Val(_objTr.SaveAccTrCmn())
        Dim EntRef As String = Strings.Left(Trim(txtDescr.Text) & IIf(Trim(txtDescr.Text) = "", "", " / ") & txtPurchaseName.Text, 249)
        Dim dlAmt As Double = TDrAmt
        'Debit Entry
        If Val(txtroundOff.Text) > 0 Then
            dlAmt = dlAmt - (IIf(cmbsign.SelectedIndex = 1, 1, -1) * CDbl(txtroundOff.Text))
        End If
        setAcctrDetValue(LinkNo, Val(txtSuppAlias.Tag), Trim(txtReference.Text), EntRef, dlAmt, "", "", 0, 0, "", _
                         "", Val(txtSuppAlias.Tag), Val(txtSuppAlias.Tag) & txtReference.Text, "", 1)
        _objTr.saveAccTrans()

        'Credit Entry
        For j = 0 To JobAcc.Count - 1
            If JobAcc(j).Amt > 0 Then
                setAcctrDetValue(LinkNo, j)
                _objTr.saveAccTrans()
            End If
        Next
        updateStockTransaction(InvTrid, LinkNo)
        updateClosingBalanceForInvoice(LinkNo, True)
    End Sub
    Private Function chkGridvalue() As Boolean
        Dim Exsts As Boolean
        chkGridvalue = True
        With grdVoucher
            MyActiveControl = grdVoucher
            'Dim dtSrlNo As DataTable
            'Dim dtExstSrlno As DataTable
            'Dim itemidStr As String
            Dim r As Integer
            'itemidStr = ""
            'For r = 0 To grdVoucher.RowCount - 1
            '    itemidStr = itemidStr & IIf(itemidStr = "", "", ",") & Val(.Item(ConstItemID, r).Value)
            'Next
            For r = 0 To .RowCount - 1 '- 1
                If Val(.Item(ConstQty, r).Value) = 0 Then .Item(ConstQty, r).Value = 0
                If CDbl(.Item(ConstQty, r).Value) = 0 Or .Item(ConstSlNo, r).Value.ToString = "M" Then
                    GoTo NextR
                End If
                Exsts = True
                If CDbl(.Item(ConstQty, r).Value) = 0 Then
                    .Rows(r).Selected = True
                    .CurrentCell = .Item(ConstQty, r)
                    MsgBox("Zero Quantity !!", vbExclamation)
                    .FirstDisplayedScrollingRowIndex = r
                    GoTo Ter
                End If
NextR:
            Next r
        End With
        For r = 0 To grdCustomernames.RowCount - 1
            If grdCustomernames.Item(ConstCustomername, r).Value <> "" Then
                Exsts = True
            End If
        Next
        If Exsts = False Then
            If MsgBox("Valid Entries not exists, Do you want to continue?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                txtvazhipaduCode.Focus()
                GoTo Ter
            End If
        End If
        clsreader()
        clsCnnection()
        Exit Function
ter:
        Return False
    End Function
    Private Function chekDuplicate() As Boolean
        Dim dtTable As DataTable
        Dim varNextFoundBool As Boolean
ChkAgain:
        dtTable = _objcmnbLayer._fldDatatable("SELECT * FROM TempleSalesCmnTb WHERE InvNo = " & Val(numVchrNo.Text) & " AND PreFix = '" & MkDbSrchStr(Trim(txtprefix.Text), True) & "'")
        If dtTable.Rows.Count > 0 Then
            If MsgBox("Entered Voucher number already exists. Fill next ?", vbQuestion + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                If Not varNextFoundBool Then
                    NextNumber()
                    varNextFoundBool = True
                Else
                    numVchrNo.Text = Val(numVchrNo.Text) + 1
                End If
                GoTo ChkAgain
            Else
                Return True
            End If
        End If
        varNextFoundBool = False
    End Function

    Private Sub UpdateClick()
        If Val(btnupdate.Tag) = 0 Then
            MsgBox("This user do not have permission to Update", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        plsrch.Visible = False
        Verify()
        chgPost = False
    End Sub

    Private Sub btnModify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModify.Click
        If btnModify.Text = "&Modify" Then
            If chgbyprg Then Exit Sub
            If ChgId Then
                Select Case MsgBox("Changes found !!  Do you want hold changes ?", vbYesNoCancel + vbQuestion)
                    Case vbYes
                        'Hold procedure
                    Case vbNo
                    Case Else
                        Exit Sub
                End Select
            End If
            isModi = True
            ClearControls()
            numVchrNo.ReadOnly = False
            numVchrNo.Focus()
            btnSlct.Visible = True
            btnModify.Text = "&Undo"
            btndelete.Text = "Delete"
            If userType Then
                btnupdate.Tag = IIf(getRight(146, CurrentUser), 1, 0)
                btndelete.Tag = IIf(getRight(147, CurrentUser), 1, 0)
            Else
                btnupdate.Tag = 1
                btndelete.Tag = 1
            End If

        Else
            AddNewClick()

        End If
    End Sub
    Private Sub ldPostedInv()
        Dim i As Integer
        If ChgId And loadedTrId <> 0 Then
            If MsgBox("Changes found on loaded Voucher.  Continue with loading ?", vbQuestion + vbOKCancel) = vbCancel Then
                Exit Sub
                numVchrNo.Focus()
            End If
        End If
        Dim ItmInvCmnTb As DataTable
        Dim sRs As DataTable
        'Dim Discount As Double
        Dim UPerPack As Double
        Dim Protect As Boolean
        Dim CrossBr As Boolean
        ItmInvCmnTb = _objcmnbLayer._fldDatatable("SELECT TempleSalesCmnTb.*,Starname FROM TempleSalesCmnTb LEFT JOIN StarTb on TempleSalesCmnTb.starid=StarTb.starid  WHERE TrId = " & loadedTrId)
        chgbyprg = True
        If ItmInvCmnTb.Rows.Count = 0 Then GoTo Quit
        'getProtectUntil()
        cmbVoucherTp.Text = getVoucherName(Val(ItmInvCmnTb(0)("VoucherTypeid") & ""))
        cldrdate.Value = Format(ItmInvCmnTb(0)("TrDate"), DtFormat) ' "MM/dd/yyyy")
        txtprefix.Text = ItmInvCmnTb(0)("PreFix")
        txtPPrefix.Text = txtprefix.Text
        txtprefix.Tag = ItmInvCmnTb(0)("PreFix")
        numVchrNo.Text = ItmInvCmnTb(0)("InvNo") 'Val(Mid(!InvNo, 3)) '!InvNo '
        numPrintVchr.Text = ItmInvCmnTb(0)("InvNo")  'CMNREC(12).FormattedTex
        txtCashCustomer.Text = Trim(ItmInvCmnTb(0)("CashCustName") & "")
        txtcustAddress.Text = Trim(ItmInvCmnTb(0)("CashCustAddr") & "")

        If Not IsDBNull(ItmInvCmnTb(0)("rndoff")) Then
            If Val(ItmInvCmnTb(0)("rndoff")) > 0 Then
                cmbsign.Text = "+"
            Else
                cmbsign.Text = "-"
                ItmInvCmnTb(0)("rndoff") = ItmInvCmnTb(0)("rndoff") * -1
            End If
            txtroundOff.Text = Format(CDbl(ItmInvCmnTb(0)("rndoff")), numFormat)
        Else
            txtroundOff.Text = Format(0, numFormat)
        End If
        setCustomer(ItmInvCmnTb(0)("CustomerAc"))
        sRs = _objcmnbLayer._fldDatatable("SELECT * FROM AccMast WHERE accid = " & ItmInvCmnTb(0)("salesAc"))

        If sRs.Rows.Count > 0 Then
            txtPurchAlias.Tag = ItmInvCmnTb(0)("salesAc")
            txtPurchAlias.Text = sRs(0)("Alias")
            txtPurchaseName.Text = sRs(0)("AccDescr")
        ElseIf txtPurchAlias.Text <> "" Then
            sRs = _objcmnbLayer._fldDatatable("SELECT * FROM AccMast WHERE alias = '" & txtPurchAlias.Text & "'")
            If sRs.Rows.Count > 0 Then
                txtPurchAlias.Tag = sRs(0)("accid")
                txtPurchAlias.Text = sRs(0)("Alias")
                txtPurchaseName.Text = sRs(0)("AccDescr")
            End If
        End If

        txtReference.Text = Trim("" & ItmInvCmnTb(0)("Reference"))
        txtDescr.Text = Trim("" & ItmInvCmnTb(0)("TrDescription"))
        txtstar.Text = Trim("" & ItmInvCmnTb(0)("Starname"))

        Protect = (ItmInvCmnTb(0)("TrDate") <= ProtectUntil)
        If sRs.Rows.Count > 0 Then sRs.Clear()
        sRs = _objcmnbLayer._fldDatatable("SELECT TempleSalesDetTb.*, [Item Code],Alias,isnull(nameMalayalam,'')nameMalayalam,TempleSalesDetTb.isTempleNonVazhipaduItem FROM TempleSalesDetTb " & _
                                          "LEFT JOIN InvItm ON InvItm.ItemId = TempleSalesDetTb.ItemId " & _
                                          " LEFT JOIN AccMast ON AccMast.Accid=TempleSalesDetTb.Acid " & _
                                          " WHERE TrId = " & loadedTrId & " ORDER BY SlNo")
        grdVoucher.RowCount = 0
        Dim findVazhipadu As Boolean
        Dim r As Integer
        With grdVoucher
            .Rows.Clear()
            If sRs.Rows.Count > 0 Then
                For r = 0 To sRs.Rows.Count - 1
                    If Val(sRs(r)("ItemId")) + Val(sRs(r)("Acid")) > 0 Then
                        If Val("" & sRs(r)("isTempleNonVazhipaduItem")) = 1 Then
                            grdVoucher.Rows.Add(1)
                            UPerPack = 1
                            'grdVoucher.Item(ConstSlNo, i).Value = IIf(Val(sRs(r)("ItemId")) = 0, "M", "")
                            grdVoucher.Item(ConstItemCode, i).Value = IIf(sRs(r)("Isacc") = "True", Trim("" & sRs(r)("Alias")), Trim("" & sRs(r)("Item Code")))
                            grdVoucher.Item(ConstItemid, i).Value = IIf(sRs(r)("Isacc") = "True", sRs(r)("Acid"), sRs(r)("ItemId"))
                            grdVoucher.Item(ConstDescr, i).Value = IIf(IsDBNull(sRs(r)("ItemDescription")), "", sRs(r)("ItemDescription"))
                            grdVoucher.Item(ConstUnit, i).Value = Trim("" & sRs(r)("Unit"))
                            grdVoucher.Item(ConstQty, i).Value = Format(sRs(r)("Qty"), numFormat)
                            grdVoucher.Item(ConstUPrice, i).Value = Format(sRs(r)("Rate"), numFormat)
                            grdVoucher.Item(ConstLTotal, i).Value = Format((sRs(r)("Qty") * sRs(i)("Rate")), numFormat)
                            grdVoucher.Item(ConstDetid, i).Value = sRs(r)("Detid")
                            grdVoucher.Item(ConstIsacc, i).Value = IIf(sRs(r)("Isacc") = "True", 1, 0)
                            i = i + 1
                        Else
                            txtvazhipadu.Text = IIf(IsDBNull(sRs(r)("ItemDescription")), "", sRs(r)("ItemDescription"))
                            txtvazhipaduCode.Text = Trim("" & sRs(r)("Alias"))
                            txtvazhipaduCode.Tag = Val(sRs(r)("Acid"))
                            vazhipaduDetId = sRs(r)("Detid")
                            lblvazhipadu.Text = sRs(i)("nameMalayalam")
                            txtrate.Text = Format(sRs(r)("Rate"), numFormat)
                            If Not IsDBNull(sRs(i)("VazhipaduDate")) Then
                                If DateValue(sRs(i)("VazhipaduDate")) > DateValue("01/01/1950") Then
                                    dtpvazhipaduDate.Value = sRs(i)("VazhipaduDate")
                                End If
                            End If
                            findVazhipadu = True

                        End If
                    End If


                Next
            End If
        End With
        If sRs.Rows.Count > 0 Then sRs.Clear()
        loadDevotee(loadedTrId)
        calculate()
        reArrangeNo()
        chgNumByPgm = False
        If Protect Then
            MsgBox("Voucher comes under Protected Range.  You can't Post modifications.", vbInformation)
            varProtectedByRights = True
        ElseIf CrossBr Then
            MsgBox("Found multi-branches or branches other than you loged.  Can't Post modifications.", vbInformation)
            varProtectedByRights = True
        Else
            'btnUpdate.Enabled = (Val(btnUpdate.Tag) > 0)
            'btnRemoveRec.Enabled = (Val(btnRemoveRec.Tag) > 0)
        End If
        'For i = 0 To 10 - grdVoucher.RowCount
        '    AddRow(True)
        'Next
        numVchrNo.Focus()
        chgbyprg = False
        ChgId = False
        chgPost = False
        GoTo Quit
Err:
        If MsgBox(Err.Description & Chr(13) & "Retry?", vbRetryCancel + vbQuestion, "Warning During Opening Data File.") = vbRetry Then Resume
Quit:
        chgbyprg = False
    End Sub
    Private Sub loadDevotee(ByVal trid As Long)
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select devoteename,case when isnull(StarNameMal,'')='' then Starname else StarNameMal end star," & _
                                         "starid from DevoteeNameTb Left join StarTb on DevoteeNameTb.star=StarTb.starid where trid=" & trid)
        Dim i As Integer
        grdCustomernames.Rows.Clear()
        For i = 0 To dt.Rows.Count - 1
            With grdCustomernames

                .Rows.Add()
                .Item(ConstCustomername, i).Value = Trim(dt(i)("devoteename") & "")
                .Item(ConstStar, i).Value = Trim("" & dt(i)("star"))
                .Item(ConstStarid, i).Value = Val(dt(i)("starid"))
            End With
        Next
        reArrangeNoCustomer()
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        UpdateClick()
    End Sub

    Private Sub btnSlct_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSlct.Click
        fSlctDoc = New SelectInvTr
        With fSlctDoc
            .strType = "TIS"
            .Text = "Select Sales Invoice"
            .ShowDialog()
        End With
        If Not fSlctDoc Is Nothing Then fSlctDoc = Nothing
    End Sub

    Private Sub fSlctDoc_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fSlctDoc.FormClosed
        fSlctDoc = Nothing
    End Sub

    Private Sub fSlctDoc_selectTr(ByVal trid As Long, ByVal TrType As String) Handles fSlctDoc.selectTr
        CheckNLoad(trid)
        fSlctDoc.Close()
        fSlctDoc = Nothing
    End Sub
    Public Sub CheckNLoad(Optional ByVal FromTrId As Long = 0)
        Dim InvList As DataTable
        If FromTrId <> 0 Then
            InvList = _objcmnbLayer._fldDatatable("SELECT TrId FROM TempleSalesCmnTb WHERE  TrId = " & FromTrId)
        Else
            InvList = _objcmnbLayer._fldDatatable("SELECT TrId FROM TempleSalesCmnTb WHERE InvNo = " & Val(numVchrNo.Text) & " AND PreFix = '" & MkDbSrchStr(Trim(txtprefix.Text), True) & "'")
        End If
        If InvList.Rows.Count > 0 Then
            loadedTrId = InvList(0)("TrId")
            InvList = Nothing
            ldPostedInv()
            isModi = True
        Else
            MsgBox("Voucher with # [" & numVchrNo.Text & "] not exits !!", vbInformation)
            numVchrNo.Focus()
        End If
        'If InvList.State Then InvList.Close()
    End Sub

    Private Sub numVchrNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles numVchrNo.KeyDown
        If e.KeyCode = Keys.Enter Then
            If isModi Then
                CheckNLoad()
            Else
                SendKeys.Send("{TAB}")
            End If
        End If
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
                txtstar.Text = ItmFlds(0)
            Case 2, 3
                txtvazhipadu.Text = ItmFlds(0)
                txtvazhipaduCode.Text = ItmFlds(1)
        End Select
        chgbyprg = False
    End Sub

    Private Sub fSelect_doSelect(ByVal KeyId As String, ByVal strFld1 As String, ByVal strFld2 As String) Handles fSelect.doSelect
        chgbyprg = True
        If _srchTxtId = 1 Or _srchTxtId = 2 Then
            txtSuppAlias.Text = strFld2
            txtSuppName.Text = strFld1
            txtSuppAlias.Tag = KeyId
        End If
        chgbyprg = False
    End Sub
    Private Sub ldSelect(ByVal BVal As Single)
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
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

    Private Sub fSelect_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fSelect.FormClosed
        fSelect = Nothing
    End Sub

    Private Sub frm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles frm.FormClosed
        frm = Nothing
    End Sub

    Private Sub fRptFormat_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fRptFormat.FormClosed
        fRptFormat = Nothing
    End Sub

    Private Sub fCashCust_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fCashCust.FormClosed
        fCashCust = Nothing
    End Sub

    Private Sub txtstar_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtstar.Validated
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT starid FROM StarTb WHERE Starname='" & txtstar.Text & "'")
        If dt.Rows.Count = 0 Then
            chgbyprg = True
            txtstar.Text = ""
            chgbyprg = False
        Else
            txtstar.Tag = dt(0)("starid")
        End If
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        If btndelete.Text = "Clear" Then
            ClearClick()
        Else
            If Val(btndelete.Tag) = 0 Then
                MsgBox("This user do not have permission to Delete", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            If MsgBox("You are going to REMOVE the Sales Invoice # " & numVchrNo.Text & Chr(13) & "Are you sure ?", vbOKCancel + vbQuestion + vbDefaultButton2) = vbOK Then

                Dim itemidsdatatable As New DataTable
                Dim trdate As Date
                Dim InvTrid As Long
                itemidsdatatable = _objcmnbLayer._fldDatatable("SELECT InvTrid from TempleSalesCmnTb WHERE TrId =" & loadedTrId)
                If itemidsdatatable.Rows.Count > 0 Then
                    InvTrid = Val(itemidsdatatable(0)("InvTrid") & "")
                End If
                If InvTrid > 0 Then
                    itemidsdatatable = _objcmnbLayer._fldDatatable("SELECT Itemid,TrDate,SerialNo from ItmInvTrTb LEFT JOIN ItmInvCmnTb ON ItmInvTrTb.Trid=ItmInvCmnTb.Trid  WHERE ItmInvTrTb.TrId =" & InvTrid)
                    If itemidsdatatable.Rows.Count > 0 Then
                        trdate = DateValue(itemidsdatatable(0)("TrDate"))
                    End If

                    _objInv = New clsInvoice
                    _objInv.TrId = InvTrid
                    _objInv.TrType = "OUT"
                    _objInv.deleteInventoryTransactions()
                    For i = 0 To itemidsdatatable.Rows.Count - 1
                        _objInv.ItemId = itemidsdatatable(i)("Itemid")
                        _objInv.TrDate = DateValue(itemidsdatatable(i)("TrDate"))
                        _objInv.setcostAverageOnModification(UsrBr)
                    Next
                End If

                _objTempInv = New clsTempleInv
                _objTempInv.Trid = loadedTrId
                _objTempInv.deleteVazhipaduSales()
            End If
            btnModify_Click(btnModify, New System.EventArgs)
        End If
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        If chkFormat.Checked Then
            fRptFormat = New RptFormatfrm
            fRptFormat.RptType = "VISM"
            fRptFormat.ShowDialog()
            fRptFormat = Nothing
        Else
            PrepareRpt("VISM")
        End If
    End Sub
    Private Sub PrepareRpt(ByVal RptType As String, Optional ByVal Forprint As Boolean = False, Optional ByVal pno As Integer = 0)

        If Val(numVchrNo.Text) = 0 Then
            MsgBox("Enter a valid Voucher Number !!", vbInformation)
            numVchrNo.Focus()
            Exit Sub
        End If
        Dim RptName As String
        Dim RptCaption As String = ""
        RptName = getRptDefFlName(RptType, RptCaption)
        If Trim(RptName) <> "" Then
            LoadReport(RptName, RptCaption, Forprint, pno)
        End If
    End Sub
    Private Sub LoadReport(ByVal FileName As String, ByVal RptCaption As String, Optional ByVal ToPrint As Boolean = False, Optional ByVal pno As Integer = 0)
        If Not frm Is Nothing Then
            frm.Close()
            frm = New ReprtviewNEWfrm
        Else
            frm = New ReprtviewNEWfrm
        End If
        Dim ds As DataSet
        With _objTempInv
            .Prefix = txtPPrefix.Text
            .InvNo = Val(numPrintVchr.Text)
            ds = .ldInvoice("rturnVazhipaduSalesForInvoicePrint", pno, 1)
        End With
        'If ToPrint Then
        '    _objcmnbLayer._saveDatawithOutParm("Update ItmInvCmnTb set Printed=1 where trid=" & Val(ds.Tables(0)(0)("trid")))
        'End If
        frm.SetReport(ds, FileName, 0, False, , ToPrint)
        frm.MdiParent = Me.MdiParent
        frm.Text = RptCaption
        If Not ToPrint Then frm.Show()
    End Sub

    Private Sub fProductEnquiryT_getSelected(ByVal ItemcODE As String, ByVal isacc As Integer) Handles fProductEnquiryT.getSelected
        chgbyprg = True
        fProductEnquiryT.Close()
        SrchText = ItemcODE
        grdVoucher.Item(ConstItemCode, grdVoucher.CurrentRow.Index).Value = ItemcODE
        chgItm = True
        Valid(grdVoucher.CurrentRow.Index, ConstItemCode)
        grdVoucher.CurrentCell = grdVoucher.Item(2, grdVoucher.CurrentRow.Index)

        'grdVoucher.CurrentCell = grdVoucher.Item(5, grdVoucher.CurrentRow.Index)
        grdBeginEdit()
        chgbyprg = False
        'Timer1.Tag = "focus"
        'Timer1.Enabled = True
    End Sub

    Private Sub fProductEnquiry_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fProductEnquiry.FormClosed
        fProductEnquiry = Nothing
    End Sub

    Private Sub fProductEnquiry_getSelected(ByVal ItemcODE As String, ByVal isacc As Integer) Handles fProductEnquiry.getSelected
        chgbyprg = True
        fProductEnquiry.Close()
        SrchText = ItemcODE
        grdVoucher.Item(ConstItemCode, grdVoucher.CurrentRow.Index).Value = ItemcODE
        chgItm = True
        Valid(grdVoucher.CurrentRow.Index, ConstItemCode)
        grdVoucher.CurrentCell = grdVoucher.Item(2, grdVoucher.CurrentRow.Index)

        'grdVoucher.CurrentCell = grdVoucher.Item(5, grdVoucher.CurrentRow.Index)
        grdBeginEdit()
        chgbyprg = False
        'Timer1.Tag = "focus"
        'Timer1.Enabled = True
    End Sub

    Private Sub btncustomeradd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncustomeradd.Click
        AddCustomerRow()
    End Sub

    Private Sub grdCustomernames_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCustomernames.CellClick
        If grdCustomernames.CurrentCell Is Nothing Then Exit Sub
        chgbyprg = True
        grdCustomernames.BeginEdit(True)
        chgbyprg = False
    End Sub

    Private Sub grdCustomernames_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCustomernames.CellEnter
        If e.ColumnIndex = ConstStar Then
            SrchText = grdCustomernames.Item(ConstStarid, e.RowIndex).Value
            grdCustomernames.Item(ConstStar, e.RowIndex).Value = SrchText
        Else
            SrchText = grdCustomernames.Item(e.ColumnIndex, e.RowIndex).Value
        End If

    End Sub

    Private Sub grdCustomernames_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCustomernames.CellValidated
        If e.ColumnIndex = ConstStar Then
            getStarId()
            plsrch.Visible = False
        End If
    End Sub
    Private Sub getStarId()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT case when isnull(StarNameMal,'')='' then Starname else StarNameMal end star,starid FROM StarTb where starid=" & Val(SrchText))
        If dt.Rows.Count > 0 Then
            With grdCustomernames
                .Item(ConstStar, .CurrentRow.Index).Value = dt(0)("star")
                .Item(ConstStarid, .CurrentRow.Index).Value = dt(0)("starid")
            End With
        End If
    End Sub

    Private Sub grdCustomernames_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCustomernames.CellValueChanged
        If chgbyprg Then Exit Sub
        chgPost = True
    End Sub

    Private Sub grdCustomernames_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdCustomernames.EditingControlShowing
        Dim Col As Integer
        Col = grdCustomernames.CurrentCell.ColumnIndex
        If Col = ConstCustomername Then
            Dim tb As TextBox = CType(e.Control, TextBox)
            RemoveHandler tb.TextChanged, AddressOf NameTextbox_TextChanged
            AddHandler tb.TextChanged, AddressOf NameTextbox_TextChanged
        End If
    End Sub

    Private Sub grdCustomernames_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdCustomernames.GotFocus
        activecontrolname = "grdCustomernames"
    End Sub

    Private Sub grdCustomernames_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdCustomernames.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If grdCustomernames.RowCount = 0 Then Exit Sub
                If SrchText = "" And grdCustomernames.CurrentCell.ColumnIndex = ConstCustomername Then
                    GoTo nxt
                End If
                If FindNextCell(grdCustomernames, grdCustomernames.CurrentCell.RowIndex, grdCustomernames.CurrentCell.ColumnIndex + 1) Then
                    AddCustomerRow()
                End If
nxt:
                plsrch.Visible = False
                chgbyprg = True
                grdCustomernames.BeginEdit(True)
                chgbyprg = False
            ElseIf e.KeyCode = Keys.F6 Then
                AddCustomerRow()
            ElseIf e.KeyCode = Keys.F7 Then
                RemoveCustomerRow()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub grdCustomernames_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdCustomernames.Leave
        activecontrolname = ""
    End Sub

    Private Sub txtvazhipadu_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtvazhipadu.KeyDown, txtvazhipaduCode.KeyDown
        lstKey = e.KeyCode
        Dim MyCtrl As TextBox = sender
        If e.KeyCode = Keys.Return Then
            txtrate.Focus()
            If Not fMList Is Nothing Then fMList.Visible = False
        ElseIf e.KeyCode = Keys.Up Or e.KeyCode = Keys.PageUp Then
            If Not fMList Is Nothing Then
                If fMList.Visible Then
                    fMList.MoveUp(MyCtrl.Text)
                    Exit Sub
                End If
            End If
        ElseIf e.KeyCode = Keys.Down Or e.KeyCode = Keys.PageDown Then
            If Not fMList Is Nothing Then
                If fMList.Visible Then
                    fMList.MoveDown(MyCtrl.Text)
                    Exit Sub
                End If
            End If
        End If
    End Sub

    Private Sub txtvazhipaduCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtvazhipaduCode.TextChanged, txtvazhipadu.TextChanged
        If chgbyprg = True Then Exit Sub
        Dim myCtrl As Control = sender
        Select Case myCtrl.Name
            Case "txtvazhipaduCode"
                _srchTxtId = 2
            Case "txtvazhipadu"
                _srchTxtId = 3
        End Select
        _srchOnce = False
        ShowFmlist(sender)
        'btnUpdate.Enabled = Val(btnUpdate.Tag) > 0
        chgPost = True
    End Sub

    Private Sub txtvazhipadu_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtvazhipadu.Validated, txtvazhipaduCode.Validated
        chgbyprg = True
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("select AccId,Alias,AccDescr,isnull(VazhipaduRate,0)VazhipaduRate,isnull(nameMalayalam,'')nameMalayalam, isnull(VazhipaduNada,'')VazhipaduNada from accmast where alias='" & txtvazhipaduCode.Text & "'")
        If dt.Rows.Count > 0 Then
            txtvazhipadu.Text = dt(0)("AccDescr")
            txtvazhipaduCode.Text = dt(0)("Alias")
            txtvazhipaduCode.Tag = dt(0)("AccId")
            txtrate.Text = Format(CDbl(dt(0)("VazhipaduRate")), numFormat)
            lblvazhipadu.Text = dt(0)("nameMalayalam") & " " & dt(0)("VazhipaduNada")
        Else
            txtvazhipadu.Text = ""
            txtvazhipaduCode.Text = ""
            txtvazhipaduCode.Tag = ""
            txtrate.Text = Format(0, numFormat)
            lblvazhipadu.Text = ""
        End If
        chgbyprg = False
    End Sub

    Private Sub dtpvazhipaduDate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtpvazhipaduDate.KeyDown
        If e.KeyCode = Keys.Return Then
            txtcustAddress.Focus()
        End If
    End Sub

    Private Sub btnvazhipaduRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnvazhipaduRemove.Click
        RemoveCustomerRow()
        chgPost = True
    End Sub
    Private Sub saveDevotee(ByVal trid As Long)
        Dim i As Integer
        With grdCustomernames
            For i = 0 To .RowCount - 1
                If .Item(ConstCustomername, i).Value = "" Then GoTo nxt
                If Val(.Item(ConstNameid, i).Value) > 0 Then
                    _objcmnbLayer._saveDatawithOutParm("Update DevoteeNameTb set " & _
                                                       "setremove=0," & _
                                                        "trid=" & trid & "," & _
                                                       "devoteename='" & .Item(ConstCustomername, i).Value & "'," & _
                                                       "star=" & .Item(ConstStarid, i).Value & " where devoteid=" & Val(.Item(ConstNameid, i).Value))
                Else
                    _objcmnbLayer._saveDatawithOutParm("INSERT INTO DevoteeNameTb(devoteename,Star,setremove,trid) values(" & _
                                                       "'" & .Item(ConstCustomername, i).Value & "'," & Val(.Item(ConstStarid, i).Value) & ",0," & trid & ")")
                End If
nxt:
            Next
        End With
    End Sub

    Private Sub txtrate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtrate.KeyDown, txtDescr.KeyDown
        If e.KeyCode = Keys.Return Then
            If grdCustomernames.RowCount = 0 Then
                AddCustomerRow()
            Else
                grdCustomernames.CurrentCell = grdCustomernames.Item(ConstCustomername, grdCustomernames.RowCount - 1)
                chgbyprg = True
                grdCustomernames.BeginEdit(True)
                chgbyprg = False
                activecontrolname = "grdCustomernames"
            End If
        End If
    End Sub

    Private Sub txtrate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtrate.KeyPress
        NumericTextOnKeypress(txtrate, e, chgbyprg, numFormat)
    End Sub

    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        LoadReport(RptFlName, RptCaption)
    End Sub

    Private Sub grdVoucher_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellContentClick

    End Sub

    Private Sub dtpvazhipaduDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpvazhipaduDate.ValueChanged

    End Sub

    Private Sub txtrate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtrate.TextChanged

    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        PrepareRpt("VISM", True)
    End Sub

    Private Sub grdCustomernames_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCustomernames.CellContentClick

    End Sub

    Private Sub chkItemcode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkItemcode.CheckedChanged

    End Sub

    Private Sub chkItemcode_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkItemcode.Click
        templeVazhipaduCodeSearch = chkItemcode.Checked
    End Sub

    Private Sub chksearchwith_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chksearchwith.CheckedChanged

    End Sub

    Private Sub chksearchwith_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chksearchwith.Click
        templeStarCodeSearch = chksearchwith.Checked
    End Sub

    Private Sub fCashCust_selectcust1(ByVal custname As String, ByVal custaddress As String, ByVal Cashcustid As Long, ByVal cardnumber As String, ByVal phone As String, ByVal gstn As String) Handles fCashCust.selectcust
        txtCashCustomer.Text = custname
        txtcustAddress.Text = custaddress
        txtCashCustomer.Tag = Cashcustid
    End Sub
End Class