Imports System.IO

Public Class SelectBarcodeFormat
    Private _objcmnbLayer As New clsCommon_BL
    Public onlyOneItem As Boolean

    Private Sub SelectBarcodeFormat_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtcopy.Focus()
    End Sub
    Private Sub SelectBarcodeFormat_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If onlyOneItem Then
            Dim fileReader As String
            Dim bartenderpath As String = DPath & "\barcode.txt"
            If Not FileExists(bartenderpath) Then
                fileReader = My.Computer.FileSystem.ReadAllText(bartenderpath)
            End If
        End If
        loadFormats()
    End Sub
    Private Sub loadFormats()
        Dim dt As DataTable
        cmbformat.Items.Clear()
        dt = _objcmnbLayer._fldDatatable("SELECT barname FROM BarcodeFormatTb order by isdefalult desc")
        Dim i As Integer
        For i = 0 To dt.Rows.Count - 1
            cmbformat.Items.Add(dt(i)("barname"))
        Next
        cmbformat.Items.Add("")
        If cmbformat.Items.Count > 0 Then cmbformat.SelectedIndex = 0
    End Sub
    Private Sub addFiles()
        Dim flag As Boolean
        dlgOpen.Filter = "All Picture Files|*.bmp;*.dib;*.gif;*.wmf;*.emf;*.jpg;*.ico;*.cur|Bitmaps(*.bmp,*.dib)|*.bmp;*.dib|Gif Images(*.gif)|*.gif|JPEG Images(*.jpg)|*.jpg|Matafiles(*.wmf,*.emf)|*.wmf;*.emf|Icons(*.ico,*.cur)|*.ico;*.cur|All Files(*.*)|*.*"
        dlgOpen.Title = "Select an Image file"
        dlgOpen.FileName = ""
        dlgOpen.ShowDialog()
        Dim fileName As String = dlgOpen.FileName
        fileName = Strings.Mid(fileName, (fileName.LastIndexOf("\") + 2))
        If (dlgOpen.FileName <> "") Then
            If CmnVeriablesAndFunctions.FileExists((DPath & fileName)) Then
                File.Delete((DPath & fileName))
                flag = True
            End If
            File.Copy(dlgOpen.FileName, (DPath & fileName))
        End If
        dlgOpen = Nothing
        Dim formatName As String
        fileName = DPath & fileName
        If Not flag Then
            formatName = InputBox("Enter Format Name", "Adding Barcode Format", "", -1, -1)
            If formatName = "" Then Exit Sub
            Dim dt As DataTable
            dt = _objcmnbLayer._fldDatatable("SELECT barid FROM BarcodeFormatTb WHERE barname='" & formatName & "'")
            If dt.Rows.Count > 0 Then
                MsgBox("Format name already exist", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            _objcmnbLayer._saveDatawithOutParm("Insert Into BarcodeFormatTb( barname, path, isdefalult) values('" & formatName & "','" & fileName & "'," & 0 & ")")
        End If
        loadFormats()
    End Sub

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        addFiles()
    End Sub

    Private Sub btndefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndefault.Click
        If Val(cmbformat.Tag & "") > 0 Then
            _objcmnbLayer._saveDatawithOutParm("update BarcodeFormatTb set isdefalult=1 where barid=" & Val(cmbformat.Tag & ""))
            MsgBox("Updated", MsgBoxStyle.Information)
        Else
            MsgBox("Invalid Format", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub btnapply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnapply.Click
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT path FROM BarcodeFormatTb WHERE barname='" & cmbformat.Text & "'")
        If dt.Rows.Count > 0 Then
            cmbformat.Tag = dt(0)("path")
        End If
        Me.Close()
    End Sub

    Private Sub txtcopy_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcopy.KeyPress
        If Not IsNumeric(e.KeyChar) Then e.Handled = False
    End Sub

End Class