Public Class LastRenewedDateFrm
    Public Ldate As Date
    Private openAmt As Double
    Private paidMonth As Integer
    Public rvAmt As Double

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        btnExit.Tag = 1
        Me.Close()
    End Sub

    Private Sub clrrenewed_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles clrrenewed.KeyDown
        If e.KeyCode = Keys.Return Then
            btnExit.Focus()
        End If
    End Sub

    Private Sub clrrenewed_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles clrrenewed.ValueChanged
        calculateAmt()
    End Sub
    Private Sub calculateAmt()
        paidMonth = DateDiff(DateInterval.Month, DateValue(Ldate), DateValue(clrrenewed.Value))
        If Val(lblamount.Tag) = 0 Then lblamount.Tag = 0
        openAmt = Val(lblamount.Tag)
        'openAmt = CDbl(lblamount.Tag) - (paidMonth * Val(clrrenewed.Tag))
        'lblamount.Text = "Amount : " & (paidMonth * Val(clrrenewed.Tag)) ' + openAmt
        lblamt.Text = Format((paidMonth * Val(clrrenewed.Tag)) + openAmt - rvAmt, numFormat)
    End Sub

    Private Sub LastRenewedDateFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        calculateAmt()
    End Sub
End Class