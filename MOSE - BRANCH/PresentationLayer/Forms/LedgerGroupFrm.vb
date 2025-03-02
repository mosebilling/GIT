Public Class LedgerGroupFrm
    Private _objcmnbLayer As New clsCommon_BL
    Private chgByPgm As Boolean
    Private Sub ldTitle()
        Dim dtTable As DataTable
        dtTable = _objcmnbLayer._fldDatatable("select * from (SELECT 'Nature'=case " & _
                                                      "when MAccId>=1000 and MAccId<2000 then 'Assets'" & _
                                                      "when MAccId>=2000 and MAccId<3000 then 'Liability'" & _
                                                      "when MAccId>=3000 and MAccId<4000 then 'Equity'" & _
                                                      "when MAccId>=4000 and MAccId<5000 then 'Income'" & _
                                                      "when MAccId>=5000 and MAccId<6000 then 'Indirect Income'" & _
                                                      "when MAccId>=6000 and MAccId<7000 then 'Exp.Direct'" & _
                                                      "when MAccId>=7000 and MAccId<8000 then 'Exp.Indirect'" & _
                                                      "End ,Descr,MAccId FROM MAccHd) tr where nature='" & cmbnature.Text & "'")
        Dim i As Integer
        cmbtitle.Items.Clear()
        cmbtitle.Items.Add("")
        For i = 0 To dtTable.Rows.Count - 1
            cmbtitle.Items.Add(dtTable(i)("Descr"))
        Next

    End Sub
    Private Sub ldGroup()
        Dim dtTable As DataTable
        cmbtitle.Tag = ""
        dtTable = _objcmnbLayer._fldDatatable("select MAccId from MAccHd where Descr='" & cmbtitle.Text & "'")
        If dtTable.Rows.Count > 0 Then
            cmbtitle.Tag = dtTable(0)("MAccId")
            dtTable = _objcmnbLayer._fldDatatable("SELECT Descr,GrpSetOn,MAccId,S1AccId FROM S1AccHd where MAccId=" & Val(cmbtitle.Tag))
            With lstContent
                .Items.Clear()
                If dtTable.Rows.Count > 0 Then
                    For i = 0 To dtTable.Rows.Count - 1
                        .Items.Add(dtTable(i)("Descr"))
                        If .Items.Item(i).SubItems.Count > 1 Then
                            .Items.Item(i).SubItems(1).Text = .Items.Add(dtTable(i)("GrpSetOn"))
                        Else
                            .Items.Item(i).SubItems.Insert(1, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, dtTable(i)("GrpSetOn")))
                        End If
                        If .Items.Item(i).SubItems.Count > 2 Then
                            .Items.Item(i).SubItems(2).Text = dtTable(i)("S1AccId")
                        Else
                            .Items.Item(i).SubItems.Insert(2, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, dtTable(i)("S1AccId")))
                        End If

                    Next
                End If
            End With

        End If

    End Sub

    Private Sub lstContent_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstContent.DoubleClick
        With lstContent
            txtname.Tag = Val(lstContent.SelectedItems(0).SubItems(2).Text)
            txtname.Text = lstContent.SelectedItems(0).SubItems(0).Text
            cmbgrpset.Text = lstContent.SelectedItems(0).SubItems(1).Text
            cmbnature.Enabled = False
            cmbtitle.Enabled = False
        End With
    End Sub


    Private Sub cmbnature_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbnature.SelectedIndexChanged
        ldTitle()
        lstContent.Items.Clear()
        cmbgrpset.Items.Clear()
        cmbgrpset.Items.Add("")
        If cmbnature.Text = "Assets" Then
            cmbgrpset.Items.Add("")
            cmbgrpset.Items.Add("CASH")
            cmbgrpset.Items.Add("CUSTOMER")
            cmbgrpset.Items.Add("P.D.C.(R)")
            cmbgrpset.Items.Add("BANK")
            cmbgrpset.Items.Add("F.Asset")
            cmbgrpset.Items.Add("Stock")
            cmbgrpset.Items.Add("Staff")
            cmbgrpset.Enabled = True
        ElseIf cmbnature.Text = "Liability" Then
            cmbgrpset.Items.Add("")
            cmbgrpset.Items.Add("SUPPLIER")
            cmbgrpset.Items.Add("P.D.C.(I)")
            cmbgrpset.Enabled = True
        ElseIf cmbnature.Text = "Income" Then
            If enableTemple Or enableChurchModule Then
                cmbgrpset.Items.Add("")
                cmbgrpset.Items.Add("Vazhipadu")
                cmbgrpset.Enabled = True
            ElseIf enableschoolmanagement Then
                cmbgrpset.Items.Add("")
                cmbgrpset.Items.Add("Fees")
                cmbgrpset.Enabled = True
            Else
                cmbgrpset.Enabled = False
            End If
        Else
            cmbgrpset.Enabled = False
        End If
    End Sub

    Private Sub cmbtitle_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbtitle.SelectedIndexChanged
        If chgByPgm Then Exit Sub
        ldGroup()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        cmbnature.Enabled = True
        cmbtitle.Enabled = True
        Dim _vdatatableTtls As DataTable
        _vdatatableTtls = _objcmnbLayer._fldDatatable("SELECT Descr,GrpSetOn,MAccId,S1AccId FROM S1AccHd")
        If ValidGroup() Then
            If Not Val(txtname.Tag) > 0 Then
                Dim bDatatable As New DataTable
                Dim _qurey = From data In _vdatatableTtls.AsEnumerable() Where data(2) = Val(cmbtitle.Tag) Select data
                If _qurey.Count > 0 Then
                    bDatatable = _qurey.CopyToDataTable()
                    Dim num = bDatatable.AsEnumerable()
                    txtname.Tag = (From n In num Select n.Field(Of Int16)("S1AccId")).Max()
                Else
                    txtname.Tag = Val(cmbtitle.Tag)
                End If
                txtname.Tag = txtname.Tag + 1
                _objcmnbLayer._saveDatawithOutParm("INSERT INTO S1AccHd (MAccId,Descr,GrpSetOn,S1AccId) VALUES(" & Val(cmbtitle.Tag) & ",'" & MkDbSrchStr(txtname.Text) & "','" & cmbgrpset.Text & "'," & Val(txtname.Tag) & ")")
            Else
                _objcmnbLayer._saveDatawithOutParm("UPDATE S1AccHd SET Descr='" & MkDbSrchStr(txtname.Text) & "', GrpSetOn='" & cmbgrpset.Text & "' WHERE S1AccId=" & Val(txtname.Tag))
            End If
        End If
        clearData()
        ldGroup()

    End Sub
    Private Sub clearData()
        txtname.Text = ""
        txtname.Tag = ""
        cmbgrpset.Text = ""
        cmbtitle.Enabled = True
        cmbnature.Enabled = True
        'cmbtitle.Text = ""
        'cmbtitle.Tag = ""
        'cmbnature.Text = ""
    End Sub
    Private Function ValidGroup() As Boolean
        Dim dt As DataTable
        ValidGroup = True
        If txtname.Text = "" Then GoTo invalid
        dt = _objcmnbLayer._fldDatatable("SELECT Descr,GrpSetOn,MAccId,S1AccId FROM S1AccHd where Descr='" & MkDbSrchStr(txtname.Text) & "'")
        If dt.Rows.Count > 0 Then
            If dt(0)("S1AccId") <> Val(txtname.Tag) Then
invalid:
                MsgBox("Invalid / Duplicate Entry Found", MsgBoxStyle.Critical)
                ValidGroup = False
            End If
        End If
    End Function

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        If Val(txtname.Tag) = 0 Then
            MsgBox("Please select group from the list", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If chkIsAccounts() Then Exit Sub
        If MsgBox("Do you want to remove the group " & txtname.Text, MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.No Then Exit Sub
        _objcmnbLayer._saveDatawithOutParm("Delete from S1AccHd where  S1AccId=" & Val(txtname.Tag))
        clearData()
        ldGroup()
    End Sub
    Private Function chkIsAccounts() As Boolean
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT Accountno from accmast where S1AccId=" & Val(txtname.Tag))
        If dt.Rows.Count > 0 Then
            MsgBox("Accounts Ledger found under this group! you cannot remove", MsgBoxStyle.Exclamation)
            Return True
        End If
    End Function

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        Dim MAccId As Integer
        MAccId = Val(cmbtitle.Tag)
        EditTitle.txtname.Tag = Val(cmbtitle.Tag)
        EditTitle.txtname.Text = cmbtitle.Text
        EditTitle.ShowDialog()
        chgByPgm = True
        ldTitle()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("select Descr,MAccId FROM MAccHd where MAccId=" & MAccId)
        If dt.Rows.Count > 0 Then
            cmbtitle.Text = dt(0)("Descr")
            cmbtitle.Tag = dt(0)("MAccId")
        End If
        chgByPgm = False
    End Sub

    Private Sub lstContent_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstContent.SelectedIndexChanged

    End Sub

    Private Sub LedgerGroupFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class