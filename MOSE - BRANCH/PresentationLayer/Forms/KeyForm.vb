Imports System.Net.NetworkInformation
Public Class KeyForm
    Private _objcmnbLayer As clsCommon_BL
   
    Function ChkApprvl() As Integer
        _objcmnbLayer = New clsCommon_BL
        If Len(Trim(txtCode.Text)) <= 4 Then
        Else
            Dim nics() As NetworkInterface = NetworkInterface.GetAllNetworkInterfaces()
            txtCode.Tag = _objcmnbLayer.getApprvlStr '& nics(0).GetPhysicalAddress.ToString
            txtCode.Text = Trim(txtCode.Text)
            If Microsoft.VisualBasic.Strings.Left(txtCode.Text, Len(txtCode.Text) - 2) = txtCode.Tag Then
                Select Case Val(Microsoft.VisualBasic.Strings.Right(txtCode.Text, 2))
                    Case 87
                        ChkApprvl = 15
                    Case 80
                        ChkApprvl = 30
                    Case 81
                        ChkApprvl = 10
                End Select
            End If
        End If
    End Function

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Dim i As Integer
        i = ChkApprvl()
        Select Case i
            Case 0
                MsgBox("Activation failed !!!", vbCritical)
            Case 10
                _objcmnbLayer._saveDatawithOutParm("Update CompanyTb set Generated='138" & txtCode.Text & "',Cntdt=81")
                MsgBox("Succesfully Activated.", vbInformation)
                If System.IO.File.Exists(Application.StartupPath & "\dataSet.dll") Then
                    System.IO.File.Delete(Application.StartupPath & "\dataSet.dll")
                End If
                Me.Close()
            Case 30
                _objcmnbLayer._saveDatawithOutParm("Update CompanyTb set dt=getdate(), Generated='138" & txtCode.Text & "',Cntdt=80")
                MsgBox("Conditional Activation Succesfully done.", vbInformation)
                If System.IO.File.Exists(Application.StartupPath & "\dataSet.dll") Then
                    System.IO.File.Delete(Application.StartupPath & "\dataSet.dll")
                End If
                Me.Close()
            Case 15
                _objcmnbLayer._saveDatawithOutParm("Update CompanyTb set dt=getdate(), Generated='138" & txtCode.Text & "',Cntdt=87")
                MsgBox("Conditional Activation Succesfully done.", vbInformation)
                If System.IO.File.Exists(Application.StartupPath & "\dataSet.dll") Then
                    System.IO.File.Delete(Application.StartupPath & "\dataSet.dll")
                End If
                Me.Close()
        End Select
    End Sub

    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcancel.Click
        Me.Close()
    End Sub

    Private Sub KeyForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        _objcmnbLayer = New clsCommon_BL
        If CurrentUser = "PROGRAMMAR" Then
            txtCode.Text = _objcmnbLayer.getApprvlStr
        Else
            txtCode.Text = ""
        End If

    End Sub

    Private Sub txtCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged

    End Sub
End Class