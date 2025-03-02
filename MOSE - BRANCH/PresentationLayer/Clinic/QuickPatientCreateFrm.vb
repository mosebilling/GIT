Public Class QuickPatientCreateFrm
    Private _objcmnbLayer As New clsCommon_BL
    Public doctor As String
    Private Sub btnexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexit.Click
        txtRec0.Text = ""
        Me.Close()
    End Sub

    Private Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        updateClick()
    End Sub
    Private Sub updateClick()
        If cmdOk.Enabled = False Then Exit Sub
        If Trim(txtRec0.Text) = "" Then
            MsgBox("Invalid Patient Code", MsgBoxStyle.Critical)
            txtRec0.Focus()
            Exit Sub
        End If
        If Trim(txtRec1.Text) = "" Then
            MsgBox("Invalid Patient Name", MsgBoxStyle.Critical)
            txtRec1.Focus()
            Exit Sub
        End If
        If Not chkDuplicate() Then Exit Sub
        saveAcc()
        MsgBox("Record Updated Successfully", MsgBoxStyle.Information)
        Me.Close()
    End Sub
    Private Function saveAcc() As Boolean
        _objcmnbLayer._saveDatawithOutParm("INSERT INTO AccMast (AccountNo, Alias, AccDescr, S1AccId,SlsmanId) VALUES (" & _
                                           Val(txtRec0.Tag) & ", '" & MkDbSrchStr(Trim(txtRec0.Text)) & "', '" & _
                                           MkDbSrchStr(Trim(txtRec1.Text)) & "', " & cmbAccGroup.Items(cmbAccGroup.SelectedIndex).thedata() & "," & _
                                           "'" & cmbdoctor.Text & "')")
        saveAddress()
    End Function
    Private Function GenerateNext(ByVal Grpname As String, ByVal newVal As Integer) As String
        Dim N As Double
        Dim NewCode As String = ""
        GenerateNext = ""
        Dim tmp As String
        Dim _vdatatableAcc As DataTable
        _vdatatableAcc = _objcmnbLayer._fldDatatable("SELECT MAX(AccountNo)AccountNo FROM AccMast WHERE S1AccId =" & newVal)
        If _vdatatableAcc.Rows.Count > 0 Then
            txtRec0.Tag = _vdatatableAcc(0)("AccountNo")
        End If
        If Val(txtRec0.Tag & "") = 0 Then
            txtRec0.Tag = Val(newVal & "0000")
        End If
        If Val(txtRec0.Tag) >= Val(newVal & "9999") Then MsgBox("Maximum number of Ledgers reached in this Group.", MsgBoxStyle.Critical) : Exit Function
        txtRec0.Tag = Val(txtRec0.Tag) + 1
        _vdatatableAcc = _objcmnbLayer._fldDatatable("SELECT * FROM AccMast")
        Try
            Do Until False
                N = N + 1
                tmp = Strings.Left(Grpname, 4) & Format(N, StrDup(4, "0"))
                Dim _qry = From job In _vdatatableAcc.AsEnumerable() Where job![Alias] = tmp Select New With _
                       {.Name = job!AccountNo}
                If _qry.Count = 0 Then
                    NewCode = Strings.Left(Grpname, 4) & Format(N, StrDup(4, "0"))
                    NewCode = Mid(NewCode, 1, 30)
                    Exit Do
                End If
                For Each itm In _qry
                    If Val(itm.Name) = 0 Then
                        NewCode = Strings.Left(Grpname, 4) & Format(N, StrDup(4, "0"))
                        NewCode = Mid(NewCode, 1, 30)
                        Exit Do
                    End If
                Next
            Loop
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return NewCode
    End Function
    Private Sub saveAddress()
        Try
            Dim AccMast As DataTable
            Dim accid As Integer
            AccMast = _objcmnbLayer._fldDatatable("SELECT AccId FROM AccMast WHERE AccountNo=" & Val(txtRec0.Tag))
            If AccMast.Rows.Count > 0 Then
                accid = AccMast(0)("AccId")
            End If
            Dim dt As Date
            dt = DateValue("01/01/1950")
            _objcmnbLayer._saveDatawithOutParm("INSERT INTO AccMastAddr (AccountNo,Address1,Address2,Address3,Phone" & _
                                                                   ") VALUES(" & _
                                                                   Val(accid) & ",'" & _
                                                                   MkDbSrchStr(txtadd1.Text) & "','" & _
                                                                   MkDbSrchStr(txtadd2.Text) & "','" & _
                                                                   MkDbSrchStr(txtadd3.Text) & "','" & _
                                                                   MkDbSrchStr(txtphone.Text) & "')")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub saveCustomer(ByVal accid As Integer)
        Dim _objInv As New clsInvoice
        Dim custid As Integer
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select custid  from CashCustomerTb where customeraccount=" & accid)
        If dt.Rows.Count > 0 Then
            custid = dt(0)("custid")
        End If
        With _objInv
            .custid = custid
            .CustName = txtRec1.Text
            .Add1 = txtadd1.Text
            .Add2 = txtadd2.Text
            .Add3 = txtadd3.Text
            .Phone = txtphone.Text
            .email = ""
            .Memberid = ""
            .GiftVrNo = ""
            .Remarks = ""
            .customeraccount = accid
            .issupp = False
            .GSTN = ""
            .saveCashCustomer()
        End With
    End Sub
    Private Function chkDuplicate() As Boolean
        Dim dt As DataTable
        chkDuplicate = True
        dt = _objcmnbLayer._fldDatatable("SELECT * FROM AccMast ")
        Dim _qry = From job In dt.AsEnumerable() Where job![Alias] = txtRec0.Text Select New With _
                        {.Name = job!AccountNo}
        If _qry.Count > 0 Then
            MsgBox("Dupicate OP Number Found!", MsgBoxStyle.Critical)
            chkDuplicate = False
            txtRec0.Focus()
            Exit Function
        End If
        _qry = From job In dt.AsEnumerable() Where job!AccDescr = txtRec1.Text Select New With _
                        {.Name = job!AccountNo}
        If _qry.Count > 0 Then
            MsgBox("Dupicate Name Found!", MsgBoxStyle.Critical)
            chkDuplicate = False
            txtRec1.Focus()
            Exit Function
        End If
    End Function

    Private Sub QuickPatientCreateFrm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtRec1.Focus()
    End Sub

    Private Sub QuickPatientCreateFrm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddtoCombo(cmbAccGroup, "SELECT Descr, S1AccId FROM S1AccHd WHERE grpseton='customer' ORDER BY Descr", False, True)
        If cmbAccGroup.Items.Count > 0 Then cmbAccGroup.SelectedIndex = 0
        txtRec0.Text = GenerateNext(cmbAccGroup.Text, cmbAccGroup.Items(cmbAccGroup.SelectedIndex).thedata)
        ldDoctor()
        cmbdoctor.Text = doctor
    End Sub

    Private Sub btnnextnumber_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnextnumber.Click
        txtRec0.Text = GenerateNext(cmbAccGroup.Text, cmbAccGroup.Items(cmbAccGroup.SelectedIndex).thedata)
    End Sub

    Private Sub txtRec0_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRec0.KeyDown, txtRec1.KeyDown, txtadd1.KeyDown, txtadd2.KeyDown, txtadd3.KeyDown, txtphone.KeyDown, cmbdoctor.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub
    Private Sub ldDoctor()
        Dim _slsManTable As DataTable = _objcmnbLayer._fldDatatable("SELECT SManCode FROM SalesmanTb WHERE isnull(isdoctor,0)=1")
        Dim i As Integer
        cmbdoctor.Items.Add("")
        For i = 0 To _slsManTable.Rows.Count - 1
            cmbdoctor.Items.Add(_slsManTable(i)("SManCode"))
        Next
    End Sub
End Class