Public Class ViewImageFrm
    Public listitemid As Long
    Public Sub loadimage()
        LdPic(picimagefromlist, DPath & "Photos\ITM-" & listitemid & ".png", Me)
    End Sub
End Class