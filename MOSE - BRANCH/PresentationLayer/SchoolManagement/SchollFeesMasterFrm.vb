Public Class SchollFeesMasterFrm
    Private _objcmnbLayer As New clsCommon_BL
    Private accid As Long
    Private chgbyprg As Boolean
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select feesid from  SchoolFeesTb where feesname='" & MkDbSrchStr(txtfeesname.Text) & "' and feesid<>" & Val(txtfeesname.Tag))
        If dt.Rows.Count > 0 Then
            MsgBox("Fees name already exist", MsgBoxStyle.Information)
            Exit Sub
        End If
        dt = _objcmnbLayer._fldDatatable("Select feesid from  SchoolFeesTb where feescode='" & MkDbSrchStr(txtcode.Text) & "' and feesid<>" & Val(txtfeesname.Tag))
        If dt.Rows.Count > 0 Then
            MsgBox("Fees Code already exist", MsgBoxStyle.Information)
            Exit Sub
        End If
        If Val(txtledgergroup.Tag) = 0 Then
            MsgBox("Please set Fess Income group in Ledger Group Master ", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Dim stracc As String = ""
        Dim str As String = "declare @accid bigint set @accid= " & accid
        Dim AccountNo As Long
        If accid = 0 Then
            Dim S1AccId As Long = GenerateNextAccountno(AccountNo)
            stracc = " if isnull(@accid,0)=0 begin insert into AccMast(AccountNo,Alias,AccDescr,OpnBal,S1AccId) values(" & AccountNo & ",'" & txtcode.Text & "','" & txtfeesname.Text & "',0," & S1AccId & ")" & _
            "Set @accid=SCOPE_IDENTITY() end "
        End If
        If Val(txtamount.Text) = 0 Then txtamount.Text = 0
        If Val(txtfeesname.Tag) > 0 Then
            If stracc = "" Then
                stracc = " update AccMast set Alias='" & txtcode.Text & "',AccDescr='" & txtfeesname.Text & "' where accid=" & accid
            End If
            str = str & stracc & "update SchoolFeesTb set AccountNo=@accid,feescode='" & txtcode.Text & "', feesname='" & MkDbSrchStr(txtfeesname.Text) & _
            "',feesIsactive='" & chkhide.Checked & "',amount=" & CDbl(txtamount.Text) & ",isyearly='" & chkyearly.Checked & "' where feesid=" & Val(txtfeesname.Tag)

        Else
            str = str & stracc & "Insert into SchoolFeesTb (feescode,feesname,feesIsactive,AccountNo,amount,isyearly) values('" & MkDbSrchStr(txtcode.Text) & "','" & _
            MkDbSrchStr(txtfeesname.Text) & "','" & chkhide.Checked & "',@accid," & CDbl(txtamount.Text) & ",'" & chkyearly.Checked & "')"
        End If
        _objcmnbLayer._saveDatawithOutParm(str)
        MsgBox("Updated", MsgBoxStyle.Information)
        txtfeesname.Text = ""
        txtfeesname.Tag = ""
        txtcode.Text = ""
        txtamount.Text = ""
        chkyearly.Checked = False
        chkhide.Checked = False
        loadfees()
        txtfeesname.Focus()
    End Sub
    Private Sub loadfees()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select feescode,feesname,case when isnull(feesIsactive,0)=0 then 'NO' else 'YES' end Hide," & _
                                         "case when isnull(isyearly,0)=0 then 'NO' else 'YES' end Yearly,Amount, feesid,AccountNo from  SchoolFeesTb")
        grdfees.DataSource = dt
        With grdfees
            SetGridProperty(grdfees)
            .Columns("feescode").HeaderText = "Fees Code"
            .Columns("feescode").Width = 100
            .Columns("feesname").HeaderText = "Fees Name"
            .Columns("Hide").HeaderText = "Hide"
            .Columns("Hide").Width = 50
            .Columns("Hide").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("feesid").Visible = False
            .Columns("AccountNo").Visible = False
            .Columns("Yearly").Width = 70
            .Columns("Yearly").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Amount").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        resizeGridColumn(grdfees, 1)
    End Sub

    Private Sub SchollFeesMasterFrm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtfeesname.Focus()
    End Sub

    Private Sub SchollFeesMasterFrm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Timer1.Enabled = True
    End Sub

    Private Sub grdfees_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdfees.DoubleClick
        With grdfees
            If .RowCount = 0 Then Exit Sub
            txtfeesname.Tag = Val(.Item("feesid", .CurrentRow.Index).Value)
            txtcode.Text = Trim(.Item("feescode", .CurrentRow.Index).Value & "")
            txtfeesname.Text = .Item("feesname", .CurrentRow.Index).Value
            If Val(.Item("amount", .CurrentRow.Index).Value & "") > 0 Then
                txtamount.Text = Format(CDbl(.Item("amount", .CurrentRow.Index).Value), numFormat)
            Else
                txtamount.Text = Format(0, numFormat)
            End If
            accid = Val(.Item("AccountNo", .CurrentRow.Index).Value & "")
            If .Item("Hide", .CurrentRow.Index).Value = "YES" Then
                chkhide.Checked = True
            Else
                chkhide.Checked = False
            End If
            If .Item("Yearly", .CurrentRow.Index).Value = "YES" Then
                chkyearly.Checked = True
            Else
                chkyearly.Checked = False
            End If
            txtcode.Focus()
        End With
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select top 1 schoolfeesid from  SchoolStudentFeesTb where feesmasterid=" & Val(txtfeesname.Tag))
        If dt.Rows.Count > 0 Then
            MsgBox("Fees name already added in admission master", MsgBoxStyle.Information)
            Exit Sub
        End If
       
        If MsgBox("Do you want to remove Fees?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        _objcmnbLayer._saveDatawithOutParm("delete from SchoolFeesTb where feesid=" & Val(txtfeesname.Tag))
        txtfeesname.Text = ""
        txtfeesname.Tag = ""
        txtfeesname.Focus()
        loadfees()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        loadfees()
        loadaccountGroup()
    End Sub

    Private Sub txtfeesname_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtfeesname.KeyDown
        If e.KeyCode = Keys.Return Then
            btnupdate.Focus()
        End If
    End Sub

    Private Sub txtfeesname_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtfeesname.TextChanged

    End Sub

    Private Sub txtcode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtcode.KeyDown
        If e.KeyCode = Keys.Return Then
            txtcode.Focus()
        End If
    End Sub
    Private Function GenerateNextAccountno(ByRef AccountNo As Long) As Long
        Dim newVal As Long
        Dim _vdatatableAcc As DataTable
        _vdatatableAcc = _objcmnbLayer._fldDatatable("declare @s1accid bigint select @s1accid=s1accid from S1AccHd where GrpSetOn='Fees' " & _
                                                     "SELECT MAX(AccountNo)AccountNo,@s1accid Groupid FROM AccMast WHERE S1AccId =isnull(@s1accid,0)")
        If _vdatatableAcc.Rows.Count > 0 Then
            AccountNo = Val(_vdatatableAcc(0)("AccountNo") & "")
            newVal = _vdatatableAcc(0)("Groupid")
        End If
        If Val(AccountNo) = 0 Then
            AccountNo = Val(newVal & "0000")
        End If
        If Val(AccountNo) >= Val(newVal & "9999") Then MsgBox("Maximum number of Ledgers reached in this Group.", MsgBoxStyle.Critical) : Exit Function
        AccountNo = Val(AccountNo) + 1
        Return newVal
    End Function
    Private Sub loadaccountGroup()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("select s1accid,Descr from S1AccHd where GrpSetOn='Fees'")
        If dt.Rows.Count > 0 Then
            txtledgergroup.Text = dt(0)("Descr")
            txtledgergroup.Tag = dt(0)("s1accid")
        End If
    End Sub

    Private Sub txtamount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtamount.KeyPress
        NumericTextOnKeypress(txtamount, e, chgbyprg, numFormat)
    End Sub

    Private Sub grdfees_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdfees.CellContentClick

    End Sub
End Class