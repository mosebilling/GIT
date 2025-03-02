Public Class ParshAddFamilyFrm
    Public MemberId As Long
#Region "Class Objects"
    Private _objcmnbLayer As New clsCommon_BL
    Private _objTempInv As New clsTempleInv
#End Region
    Private Sub txtname_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtname.KeyDown, txtrelation.KeyDown, txtintute.KeyDown, _
                                                                                                                txtblood.KeyDown, txtoccupation.KeyDown, txtroll.KeyDown, _
                                                                                                                txtstandared.KeyDown, cmbgroup.KeyDown, txtoccupation.KeyDown, _
                                                                                                                txtadhar.KeyDown, txtqualification.KeyDown, txtphone.KeyDown
        Dim mctrl As Control = sender
        If e.KeyCode = Keys.Return Then
            If mctrl.Name = "txtphone" Then
                cmdOk.Focus()
            Else
                SendKeys.Send("{TAB}")
            End If
        End If
    End Sub


    Private Sub cmblives_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmblives.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
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
            .dob = DateValue(dtpdob.Value)
            .marriagedate = DateValue(dtpmarriage.Value)
            .Phone = txtphone.Text
            .adharno = txtadhar.Text
            .qualification = txtqualification.Text
            .dateofdeath = DateValue(dtpdateofdeath.Value)
            .baptismdate = DateValue(dtpbaptism.Value)
            .ismarriage = chkmarriage.Checked
            .isbaptism = chkisbaptism.Checked
            If rdoMlive.Checked Then
                .MStatus = 0
            ElseIf rdoMexpired.Checked Then
                .MStatus = 1
            Else
                .MStatus = 2
            End If
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
        txtadhar.Text = ""
        txtqualification.Text = ""
        txtphone.Text = ""
        rdoMlive.Checked = True
        dtpdob.Value = DateValue(Date.Now)
        dtpdateofdeath.Value = DateValue(Date.Now)
        dtpbaptism.Value = DateValue(Date.Now)
        chkisbaptism.Checked = False
        chkmarriage.Checked = False
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

            dtpdob.Value = dt(0)("dob")
            dtpmarriage.Value = dt(0)("marriagedate")
            txtphone.Text = dt(0)("phone")
            txtadhar.Text = dt(0)("adharno")
            txtqualification.Text = dt(0)("qualification")
            dtpdateofdeath.Value = dt(0)("dateofdeath")
            dtpbaptism.Value = dt(0)("baptismdate")
            If Not IsDBNull(dt(0)("ismarried")) Then
                chkmarriage.Checked = dt(0)("ismarried")
            End If
            If Not IsDBNull(dt(0)("isbaptism")) Then
                chkisbaptism.Checked = dt(0)("isbaptism")
            End If
            Select Case Val(dt(0)("mstatus") & "")
                Case 0
                    rdoMlive.Checked = True
                Case 1
                    rdoMexpired.Checked = True
                Case Else
                    rdoMtran.Checked = True
            End Select
        End If
    End Sub

    Private Sub AddFamilyFrm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ldFamilyMemberDetails()
    End Sub

    Private Sub dtpdob_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtpdob.KeyDown, dtpbaptism.KeyDown, _
                                                                                                              dtpmarriage.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub rdoMexpired_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoMexpired.Click, rdoMlive.Click, rdoMtran.Click
        pldeath.Enabled = rdoMexpired.Checked
    End Sub

    Private Sub chkisbaptism_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkisbaptism.CheckedChanged
        Label3.Enabled = chkisbaptism.Checked
        dtpbaptism.Enabled = chkisbaptism.Checked
    End Sub

    Private Sub chkmarriage_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkmarriage.CheckedChanged
        Label29.Enabled = chkmarriage.Checked
        dtpmarriage.Enabled = chkmarriage.Checked
    End Sub
End Class