Public Class AddFollowupFrm
    Private _objcmnbLayer As New clsCommon_BL
    Private Sub AddFollowupFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ldDoctor()
        Dim ObjLocationlist As New List(Of String)
        ObjLocationlist = toLoadAutoFillListItems("Alias", "AccMast left join S1AccHd on S1AccHd.S1AccId=AccMast.S1AccId where GrpSetOn='customer'")
        Call toAssignDownListToText(txtopnumber, ObjLocationlist) '
        If Val(dtpcalldate.Tag) > 0 Then
            loadCreated()
        ElseIf Val(txtopnumber.Tag) > 0 Then
            setCustomer(Val(txtopnumber.Tag))
        End If
    End Sub
    Private Sub setCustomer(Optional ByVal accid As Long = 0, Optional ByVal skiploadHistory As Boolean = False)
        Dim dt As DataTable
        Dim condition As String
        If txtopnumber.Text = "" And accid = 0 Then GoTo els
        If accid > 0 Then
            condition = "where accid=" & accid
        Else
            condition = "where Alias='" & txtopnumber.Text & "'"
        End If
        dt = _objcmnbLayer._fldDatatable("SELECT AccMast.accid,Alias,AccDescr,Address1,Address2,Address3,Address4," & _
                                        "Phone,Fax,EMail,PT1,PT2,PT3,PT4,TrdLcno,TrdDate,CreditLimit,DueDays,SlsmanId," & _
                                        "CurrencyCode,CountryCode,GSTIN,Remarks,GrpSetOn,gender,dateofbirth " & _
                                        " from AccMast LEFT JOIN AccMastAddr ON AccMast.accid=AccMastAddr.AccountNo " & _
                                        "LEFT JOIN S1AccHd ON AccMast.S1AccId=S1AccHd.S1AccId " & _
                                        condition)
        If dt.Rows.Count > 0 Then
            txtopnumber.Tag = dt(0)("accid")
            txtopnumber.Text = Trim("" & dt(0)("Alias"))
            txtcompanyname.Text = Trim("" & dt(0)("AccDescr"))
            txtcustAddress.Text = Trim(dt(0)("Address1") & "")
            If Trim(dt(0)("Address2") & "") <> "" Then
                txtcustAddress.Text = txtcustAddress.Text & vbCrLf & Trim(dt(0)("Address2") & "")
            End If
            If Trim(dt(0)("Address3") & "") <> "" Then
                txtcustAddress.Text = txtcustAddress.Text & vbCrLf & Trim(dt(0)("Address3") & "")
            End If
            If Trim(dt(0)("Address4") & "") <> "" Then
                txtcustAddress.Text = txtcustAddress.Text & vbCrLf & Trim(dt(0)("Address4") & "")
            End If
            If Trim(dt(0)("Phone") & "") <> "" Then
                txtphonenumber.Text = Trim(dt(0)("Phone") & "")
            End If
            If Val(dtpcalldate.Tag) = 0 Then
                If Trim(dt(0)("SlsmanId") & "") <> "" Then
                    cmbsalesman.Text = Trim(dt(0)("SlsmanId") & "")
                End If
            End If
            
        Else
els:
            txtopnumber.Text = ""
            txtopnumber.Tag = ""
            txtcompanyname.Text = ""
            txtcustAddress.Text = ""
        End If
    End Sub

    Private Sub txtopnumber_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtopnumber.KeyDown
        If e.KeyCode = Keys.Return Then
            cmbsalesman.Focus()
        End If
    End Sub

    Private Sub txtopnumber_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtopnumber.Validated
        setCustomer()
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        If Val(txtopnumber.Tag) = 0 Then
            MsgBox("Invalid Patient", MsgBoxStyle.Information)
            txtopnumber.Focus()
            Exit Sub
        End If
        If Val(btnupdate.Tag) > 0 Then
            MsgBox("You cannot edit closed call", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If Val(dtpcalldate.Tag) > 0 Then
            _objcmnbLayer._saveDatawithOutParm("update CustomerFollowupTb set calldate='" & Format(DateValue(dtpcalldate.Value), "yyyy/MM/dd") & "'," & _
                                               "pupose='" & MkDbSrchStr(txtpurpose.Text) & "',comment='" & MkDbSrchStr(txtremark.Text) & _
                                               "',salesman='" & cmbsalesman.Text & "',isclosed=" & IIf(chkcreatenew.Checked, 1, 0) & _
                                               ",closeddate='" & Format(DateValue(Date.Now), "yyyy/MM/dd") & "'" & _
                                               " where recallid=" & Val(dtpcalldate.Tag))
        End If
        If Val(dtpcalldate.Tag) = 0 Or chkcreatenew.Checked Then
            Dim calldate As Date
            If chkcreatenew.Checked Then
                calldate = DateValue(dtpcallnextdate.Value)
            Else
                calldate = DateValue(dtpcalldate.Value)
            End If
            _objcmnbLayer._saveDatawithOutParm("insert into CustomerFollowupTb(customerid,calldate,pupose,salesman) values(" & Val(txtopnumber.Tag) & _
                                               ",'" & Format(DateValue(calldate), "yyyy/MM/dd") & "','" & MkDbSrchStr(txtpurpose.Text) & "','" & cmbsalesman.Text & "')")
        End If
        MsgBox("Updated", MsgBoxStyle.Information)
        Me.Close()
    End Sub

    Private Sub chkcreatenew_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkcreatenew.CheckedChanged
        'grpcallupdate.Enabled = chkcreatenew.Checked
    End Sub
    Private Sub loadCreated()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select * from CustomerFollowupTb where recallid=" & Val(dtpcalldate.Tag))
        If dt.Rows.Count > 0 Then
            txtopnumber.Tag = dt(0)("customerid")
            setCustomer(Val(txtopnumber.Tag))
            dtpcalldate.Value = dt(0)("calldate")
            txtpurpose.Text = Trim(dt(0)("pupose") & "")
            txtremark.Text = Trim(dt(0)("comment") & "")
            cmbsalesman.Text = Trim(dt(0)("salesman") & "")
            btnupdate.Tag = Val(dt(0)("isclosed") & "")
            grpcallupdate.Enabled = True
        End If
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub ldDoctor()
        Dim _slsManTable As DataTable = _objcmnbLayer._fldDatatable("SELECT SManCode FROM SalesmanTb WHERE isnull(isdoctor,0)=1")
        Dim i As Integer
        cmbsalesman.Items.Add("")
        For i = 0 To _slsManTable.Rows.Count - 1
            cmbsalesman.Items.Add(_slsManTable(i)("SManCode"))
        Next
    End Sub

    Private Sub cmbsalesman_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbsalesman.KeyDown
        If e.KeyCode = Keys.Return Then
            txtpurpose.Focus()
        End If
    End Sub

    Private Sub cmbsalesman_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbsalesman.SelectedIndexChanged

    End Sub
End Class