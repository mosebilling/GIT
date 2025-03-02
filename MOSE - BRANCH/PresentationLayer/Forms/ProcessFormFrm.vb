Public Class ProcessFormFrm
    Public typeofProcess As Integer
    Dim _objcmnbLayer As New clsCommon_BL
    Delegate Sub GenericDelegate(ByVal Mname As String, ByVal RecName As String, ByVal rec As Integer, ByVal count As Integer)

    Private Sub ProcessFormFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Worker.WorkerReportsProgress = True
        Worker.WorkerSupportsCancellation = True
        Worker.RunWorkerAsync()
    End Sub
    Public Sub status(ByVal Mname As String, ByVal RecName As String, ByVal rec As Integer, ByVal count As Integer)
        If Me.InvokeRequired Then
            Dim d As New GenericDelegate(AddressOf status)
            Me.Invoke(d, Mname, RecName, rec, count)
        Else
            lblmodule.Text = Mname
            'lblrec.Text = cnt1 + cnt2 + cnt3 + cnt4 & " / " & count
            'If rec = 19 Then
            '    MsgBox("")
            'End If
            lblrec.Text = rec & " / " & count
            'lblmodule.Refresh()
            'lblrec.Refresh()
            If rec = 0 Then
                pb.Value = 0
            Else
                'If pb.Value + (100 / count) > 100 Then
                '    pb.Value = 100
                'Else
                '    pb.Value = pb.Value + (rec * 100 / count)
                'End If
                pb.Value = rec * 100 / count
                'pb.Value = (cnt1 + cnt2 + cnt3 + cnt4) * 100 / count
            End If

        End If
    End Sub

    Private Sub Worker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles Worker.DoWork
        Try
            Select Case typeofProcess
                Case 1
                    Dim i As Integer
                    Dim dt As DataTable
                    dt = _objcmnbLayer._fldDatatable("Select invitm.itemid,[Item Code] from invitm " & _
                                                     "inner JOIN (Select itemid from ItmInvCmnTb left join ItmInvTrTb ON ItmInvTrTb.TRID=ItmInvCmnTb.TRID " & _
                                                     "where trtype IN ('IP','TI')) tr on invitm.itemid=tr.itemid")
                    For i = 0 To dt.Rows.Count - 1
                        _objcmnbLayer.updateLastPurchaseCost(Val(dt(i)("itemid")))
                        status("Updating Last Purchase Cost", dt(i)("Item Code"), i + 1, dt.Rows.Count)
                    Next
            End Select
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
        
    End Sub

    Private Sub Worker_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Worker.RunWorkerCompleted
        MsgBox("Updated", MsgBoxStyle.Information)
    End Sub
End Class