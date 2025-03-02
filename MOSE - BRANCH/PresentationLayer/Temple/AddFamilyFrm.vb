Public Class AddFamilyFrm
    Public MemberId As Long
#Region "Class Objects"
    Private _objcmnbLayer As New clsCommon_BL
    Private _objTempInv As New clsTempleInv
#End Region
    Private Sub txtname_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtname.KeyDown, txtrelation.KeyDown, txtintute.KeyDown, _
                                                                                                                txtblood.KeyDown, txtoccupation.KeyDown, txtroll.KeyDown, txtstandared.KeyDown, cmbgroup.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub


    Private Sub cmblives_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmblives.KeyDown
        If e.KeyCode = Keys.Return Then
            cmdOk.Focus()
        End If
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        setAndSaveValues()
        If Val(txtname.Tag) > 0 Then
            Me.Close()
        Else
            clearControls()
            txtname.Focus()
        End If
    End Sub

    Private Sub AddFamilyFrm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtname.Focus()
    End Sub
    Private Sub setAndSaveValues()
        With _objTempInv
            .familiid = Val(txtname.Tag)
            .memberid = MemberId
            .fmembername = txtname.Text
            .relation = txtrelation.Text
            .Gender = IIf(rdomale.Checked, 0, 1)
            .category = cmbgroup.SelectedIndex
            .Occupation = txtoccupation.Text
            .IsWU = chkwu.Checked
            .WURoll = txtroll.Text
            .StudentStandard = txtstandared.Text
            .StudentSchool = txtintute.Text
            .BloodGrp = txtblood.Text
            .LivesIn = cmblives.SelectedIndex
            .TempleFamilyTbSaveModify()
            MsgBox("Record Successfully Saved", MsgBoxStyle.Information)
        End With
    End Sub
    Private Sub clearControls()
        txtname.Text = ""
        txtname.Tag = ""
        txtrelation.Text = ""
        rdomale.Checked = True
        cmbgroup.SelectedIndex = 0
        txtstandared.Text = ""
        txtintute.Text = ""
        txtoccupation.Text = ""
        chkwu.Checked = False
        txtroll.Text = ""
        txtblood.Text = ""
        cmblives.SelectedIndex = 0
    End Sub
    Private Sub ldFamilyMemberDetails()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT * FROM TempleFamilyTb WHERE familiid=" & Val(txtname.Tag))
        If dt.Rows.Count > 0 Then
            txtname.Text = dt(0)("fmembername")
            txtname.Tag = dt(0)("familiid")
            txtrelation.Text = dt(0)("relation")
            Select Case dt(0)("gender")
                Case 0
                    rdomale.Checked = True
                Case 1
                    rdofemale.Checked = True
            End Select
            cmbgroup.SelectedIndex = dt(0)("category")
            txtoccupation.Text = dt(0)("occupation")
            chkwu.Checked = dt(0)("IsWU")
            txtroll.Text = dt(0)("WURoll")
            txtstandared.Text = dt(0)("StudentStandard")
            txtintute.Text = dt(0)("StudentSchool")
            txtblood.Text = dt(0)("bloodgroup")
            cmblives.SelectedIndex = dt(0)("LivesIn")
        End If
    End Sub

    Private Sub AddFamilyFrm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ldFamilyMemberDetails()
    End Sub
End Class