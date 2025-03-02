Public Class ServiceItemCollectionFrm
    Private Const ConstSlNo = 0
    Private Const Constmodel = 1
    Public Const Constitemname = 2 'HSN Code
    Private Const Constcomment = 3
    Private Const Constwarrenty = 4
    Private Const Constserialno = 5
    Private Const Constwarrentydate = 6
    Private Const ConstwarrentyInvNo = 7
    Private Const constTechRemark = 8
    Private Const Constdetid = 9
    Private _objcmnbLayer As New clsCommon_BL
    Private _objclinic As New clsClinic
    Private SrchText As String
    Private chgbyprg As Boolean
    Private activecontrolname As String
    Private dtTable As DataTable

    Private Sub ServiceItemCollectionFrm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtRec0.Focus()
    End Sub
    Private Sub ServiceItemCollectionFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        NextNumber()
        SetGridHead()
        Dim ObjLocationlist As New List(Of String)
        ObjLocationlist = toLoadAutoFillListItems("Alias", "AccMast left join S1AccHd on S1AccHd.S1AccId=AccMast.S1AccId where GrpSetOn='customer'")
        Call toAssignDownListToText(txtRec0, ObjLocationlist) '
        If Not userType Then
            btndelete.Tag = 1
        Else
            btnupdate.Tag = 1
            btndelete.Tag = IIf(getRight(242, CurrentUser), 1, 0)
        End If
    End Sub
    Private Sub NextNumber()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("select max(serviceNo) maxno from ClinicServiceCmnTb")
        If dt.Rows.Count > 0 Then
            Dim maxno As Integer = Val(dt(0)("maxno") & "")
            numVchrNo.Text = maxno + 1
            Exit Sub
        End If
        numVchrNo.Text = 1
    End Sub
    Private Sub SetGridHead()
        With grdVoucher

            SetEntryGridProperty(grdVoucher)
            .ColumnCount = 10
            .ColumnHeadersHeight = 100
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersDefaultCellStyle.BackColor = Color.Olive
            .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.5!, System.Drawing.FontStyle.Regular)
            .RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.0!)

            .Columns(ConstSlNo).HeaderText = "SlNo"
            .Columns(ConstSlNo).Width = 40
            .Columns(ConstSlNo).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstSlNo).Frozen = True
            .Columns(ConstSlNo).DefaultCellStyle.Format = "N0"
            .Columns(ConstSlNo).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(Constmodel).HeaderText = "Model"
            .Columns(Constmodel).Width = 100
            .Columns(Constmodel).SortMode = DataGridViewColumnSortMode.NotSortable

            .Columns(Constitemname).HeaderText = "Description"
            .Columns(Constitemname).Width = 200
            .Columns(Constitemname).SortMode = DataGridViewColumnSortMode.NotSortable

            .Columns(Constcomment).HeaderText = "Comment"
            .Columns(Constcomment).Width = 220
            .Columns(Constcomment).SortMode = DataGridViewColumnSortMode.NotSortable

            .Columns(Constwarrenty).HeaderText = "W"
            .Columns(Constwarrenty).Width = 40
            .Columns(Constwarrenty).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(Constwarrenty).ReadOnly = True

            .Columns(Constserialno).HeaderText = "Serial No"
            .Columns(Constserialno).Width = 100

            .Columns(Constwarrentydate).Width = 100
            .Columns(Constwarrentydate).SortMode = DataGridViewColumnSortMode.NotSortable
            Dim col As New CalendarColumn()
            .Columns.RemoveAt(Constwarrentydate)
            col.DataPropertyName = "warrentydate"
            .Columns.Insert(Constwarrentydate, col)
            .Columns(Constwarrentydate).HeaderText = "M. Date"
            .Columns(Constwarrentydate).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns(ConstwarrentyInvNo).HeaderText = "INV#"
            .Columns(ConstwarrentyInvNo).Width = 100
            .Columns(ConstwarrentyInvNo).SortMode = DataGridViewColumnSortMode.NotSortable

            .Columns(constTechRemark).HeaderText = "Tech Remark"
            .Columns(constTechRemark).Width = 200
            .Columns(constTechRemark).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(constTechRemark).ReadOnly = True

            .Columns(Constdetid).Visible = False

        End With
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            Const WM_KEYDOWN As Integer = &H100
            Const WM_KEYUP As Integer = &H101
            If msg.Msg = WM_KEYDOWN Or msg.Msg = WM_KEYUP Then
                If activecontrolname = "grdVoucher" Then
                    If msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up) Or msg.WParam.ToInt32() = CInt(Keys.Enter) Or msg.WParam.ToInt32() = CInt(Keys.F2) Or msg.WParam.ToInt32() = CInt(Keys.F3) Or msg.WParam.ToInt32() = CInt(Keys.F4) Then
                        grdVoucher_KeyDown(Nothing, New KeyEventArgs(keyData))
                        Return True
                    End If
                End If
            End If
ctn:
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function
    Private Sub grdBeginEdit()
        chgbyprg = True
        grdVoucher.BeginEdit(True)
        chgbyprg = False
    End Sub

    Private Sub grdVoucher_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellClick
        chgbyprg = True
        With grdVoucher
            If e.ColumnIndex = Constwarrenty Or e.ColumnIndex = constTechRemark Then
                grdVoucher.CurrentCell.ReadOnly = True
            Else
                grdVoucher.CurrentCell.ReadOnly = False
            End If
        End With
        grdBeginEdit()
    End Sub

    Private Sub grdVoucher_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.GotFocus
        activecontrolname = "grdVoucher"
    End Sub
    Private Sub grdVoucher_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdVoucher.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If grdVoucher.RowCount = 0 Then Exit Sub
               
                If FindNextCell(grdVoucher, grdVoucher.CurrentCell.RowIndex, grdVoucher.CurrentCell.ColumnIndex + 1) Then
                    AddRow()
                End If
nxt:
                grdBeginEdit()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub AddRow(Optional ByVal tocheck As Boolean = False)
        chgbyprg = True
        Dim i As Integer
        With grdVoucher
            .Rows.Add()
            i = .RowCount - 1
            activecontrolname = "grdVoucher"
            .CurrentCell = .Item(Constmodel, i)
            chgbyprg = True
            .BeginEdit(True)
            chgbyprg = False
        End With
        reArrangeNo()
    End Sub
    Private Sub RemoveRow()
        If grdVoucher.RowCount > 0 Then
            If MsgBox("You Are Going to Remove the Row." & vbCrLf & "Are You Sure ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then
                Exit Sub
            End If
            With grdVoucher
                .Rows.RemoveAt(.CurrentRow.Index)
                .ClearSelection()
            End With
            reArrangeNo()
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

    Private Sub grdVoucher_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVoucher.Leave
        activecontrolname = ""
    End Sub

    Private Sub btnremoveitem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnremoveitem.Click
        RemoveRow()
    End Sub

    Private Sub btnadditem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadditem.Click
        AddRow()
    End Sub

    Private Sub txtRec0_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRec0.KeyDown, txtRec1.KeyDown
        Dim MyCtrl As TextBox = sender
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub
    Private Sub setCustomer(Optional ByVal accid As Long = 0, Optional ByVal skiploadHistory As Boolean = False)
        Dim dt As DataTable
        Dim condition As String
        If txtRec0.Text = "" And accid = 0 Then GoTo els
        If accid > 0 Then
            condition = "where accid=" & accid
        Else
            condition = "where Alias='" & txtRec0.Text & "'"
        End If
        dt = _objcmnbLayer._fldDatatable("SELECT AccMast.accid,Alias,AccDescr,Address1,Address2,Address3,Address4," & _
                                        "Phone,Fax,EMail,PT1,PT2,PT3,PT4,TrdLcno,TrdDate,CreditLimit,DueDays,SlsmanId," & _
                                        "CurrencyCode,CountryCode,GSTIN,Remarks,GrpSetOn " & _
                                        " from AccMast LEFT JOIN AccMastAddr ON AccMast.accid=AccMastAddr.AccountNo " & _
                                        "LEFT JOIN S1AccHd ON AccMast.S1AccId=S1AccHd.S1AccId " & _
                                        condition)
        If dt.Rows.Count > 0 Then
            txtRec0.Tag = dt(0)("accid")
            chgbyprg = True
            txtRec0.Text = Trim("" & dt(0)("Alias"))
            txtRec1.Text = Trim("" & dt(0)("AccDescr"))
            txtadd1.Text = Trim(dt(0)("Address1") & "")
            txtadd2.Text = Trim(dt(0)("Address2") & "")
            txtadd3.Text = Trim(dt(0)("Address3") & "")
            txtphone.Text = Trim(dt(0)("Phone") & "")
            btnclear.Text = "Undo"
        Else
els:
            txtRec0.Text = ""
            txtRec0.Tag = ""
            txtRec1.Text = ""
            txtadd1.Text = ""
            txtadd2.Text = ""
            txtadd3.Text = ""
            txtphone.Text = ""
        End If
        chgbyprg = False
    End Sub

    Private Sub txtRec0_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRec0.Validated
        setCustomer()
    End Sub

    Private Sub grdVoucher_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdVoucher.CellDoubleClick
        If e.ColumnIndex = Constwarrenty Then
            With grdVoucher
                .Item(e.ColumnIndex, e.RowIndex).Value = IIf(.Item(e.ColumnIndex, e.RowIndex).Value = "", "Y", "")
            End With
        End If
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        If verify() Then
            Dim maxid As Long
            With _objclinic
                .id = Val(numVchrNo.Tag)
                .servicedate = DateValue(dtpdate.Value)
                .customerid = Val(txtRec0.Tag)
                .serviceNo = Val(numVchrNo.Text)
                .remark = txtremarks.Text
                maxid = .saveClinicServiceCmnTb()
            End With
            Dim i As Integer
            If maxid > 0 Then
                With grdVoucher
                    For i = 0 To .Rows.Count - 1
                        _objclinic.detid = Val(.Item(Constdetid, i).Value)
                        _objclinic.model = .Item(Constmodel, i).Value
                        _objclinic.itemname = .Item(Constitemname, i).Value
                        _objclinic.comment = .Item(Constcomment, i).Value
                        _objclinic.warrenty = IIf(.Item(Constwarrenty, i).Value = "", 0, 1)
                        _objclinic.serialno = .Item(Constserialno, i).Value
                        _objclinic.warrentydate = .Item(Constwarrentydate, i).Value
                        _objclinic.warrentyInvNo = .Item(ConstwarrentyInvNo, i).Value
                        _objclinic.saveClinicServiceDetailsTb()
                    Next

                End With
            End If
            
        End If
    End Sub
    Private Function verify() As Boolean
        If Val(txtRec0.Tag) = 0 Then
            MsgBox("Invalid OP Number", MsgBoxStyle.Exclamation)
            txtRec0.Focus()
            Return False
        End If
        If grdVoucher.Rows.Count = 0 Then
            MsgBox("Invalid Items", MsgBoxStyle.Exclamation)
            Return False
        End If
        Return True
    End Function
    Private Sub loadService()
        Dim tp As Integer
        If rdoActive.Checked Or rdoclosed.Checked Then
            tp = 0
        Else
            tp = 1
        End If
        Dim status As Integer
        If rdoActive.Checked Then
            status = 0
        ElseIf rdoclosed.Checked Then
            status = 2
        End If
        dtTable = _objclinic.loadClinicService(DateValue(dtpfrom.Value), DateValue(dtpto.Value), tp, status).Tables(0)
        grdlist.DataSource = dtTable
        SetGridHeadList()
    End Sub
    Sub SetGridHeadList()
        With grdlist
            SetGridProperty(grdlist)
            .Columns("Patient").Width = 200
            .Columns("Item Name").Width = 150
            .Columns("Comment").Width = 150
            .Columns("lnk").Visible = False
            .Columns("datefrom").Visible = False
            .Columns("dateto").Visible = False
        End With
        setComboGrid()
    End Sub
    Private Sub setComboGrid()
        ChgByPrg = True
        cmbOrder.Items.Clear()
        Dim i As Integer = 0
        For i = 0 To grdlist.ColumnCount - 2
            cmbOrder.Items.Add(grdlist.Columns(i).HeaderText)
        Next
        If cmbOrder.Items.Count > 0 Then cmbOrder.SelectedIndex = 0
        ChgByPrg = False
    End Sub

    Private Sub btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnload.Click
        loadService()
    End Sub
End Class