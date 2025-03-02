Imports System.Drawing.Imaging
Imports IDAutomation.Windows.Forms.LinearBarCode
Imports System.Drawing.Printing
Imports System.Configuration
Imports GenCode128
Imports System.IO
Public Class BarCodeFrm
    Private chgbypgm As Boolean
    Private WithEvents pdPrint As PrintDocument
    Private PrintDocType As String = "Barcode"
    Private _objcmnbLayer As New clsCommon_BL

    Private Sub txtqty_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtqty.KeyDown
        If e.KeyCode = Keys.Return Then
            btnPrint.Focus()
        End If
    End Sub
    Private Sub txtqty_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtqty.KeyPress
        NumericTextOnKeypress(txtqty, e, chgbypgm, "0")
    End Sub

    Private Sub BarCodeFrm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        imgIDAutomation.Image = Nothing
        imgIDAutomation.Dispose()
    End Sub

    Private Sub BarCodeFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'loadPrinternames()
        'generateBarcode()
        loadFormats()
        isPrintTaxPrice()
    End Sub
    Private Sub loadPrinternames()
        Dim pkInstalledPrinters As String

        ' Find all printers installed
        For Each pkInstalledPrinters In _
            PrinterSettings.InstalledPrinters
            cmbprinter.Items.Add(pkInstalledPrinters)
        Next pkInstalledPrinters

        ' Set the combo to the first printer in the list
        cmbprinter.SelectedIndex = 0
    End Sub
    Private Sub pdPrint_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles pdPrint.PrintPage
        Dim h, w As Integer
        Dim rect As New Rectangle(0, 10, 280, 85)
        Dim sf As New StringFormat
        sf.LineAlignment = StringAlignment.Center
        Dim headTop As Integer = 10
        Dim printFont10_Normal As New Font("Calibri", 10, FontStyle.Regular, GraphicsUnit.Point)
        rect = New Rectangle(0, 10, 280, 15)


        Dim pbImage As New PictureBox
        pbImage.Image = Image.FromFile(Application.StartupPath & "\" & "SavedBarcode.Jpeg")
        w = Image.FromFile(Application.StartupPath & "\" & "SavedBarcode.Jpeg").Width
        h = Image.FromFile(Application.StartupPath & "\" & "SavedBarcode.Jpeg").Height
        Dim lPosition As Integer
        lPosition = (280 - w) / 2

        Dim barTop As Integer = headTop + 15

        rect = New Rectangle(0, headTop, 280, 15)
        e.Graphics.DrawString("LACHOOS COLLECTIONS", printFont10_Normal, Brushes.Black, rect, sf)
        e.Graphics.DrawRectangle(Pens.White, rect)

        'w = 140
        h = 40
        rect = New Rectangle(0, barTop, w, h)
        e.Graphics.InterpolationMode = Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        e.Graphics.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
        e.Graphics.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
        e.Graphics.DrawImage(pbImage.Image, rect)
        e.Graphics.DrawRectangle(Pens.White, rect)

        rect = New Rectangle(0, barTop + h + 1, 280, 15)
        e.Graphics.DrawString(lblbarcode.Text, printFont10_Normal, Brushes.Black, rect, sf)
        e.Graphics.DrawRectangle(Pens.White, rect)

        rect = New Rectangle(0, barTop + h + 16, 280, 15)
        e.Graphics.DrawString("RS: " & lblprice.Text, printFont10_Normal, Brushes.Black, rect, sf)
        e.Graphics.DrawRectangle(Pens.White, rect)


        rect = New Rectangle(162, headTop, 280, 15)
        e.Graphics.DrawString("LACHOOS COLLECTIONS", printFont10_Normal, Brushes.Black, rect, sf)
        e.Graphics.DrawRectangle(Pens.White, rect)

        rect = New Rectangle(162, barTop, w, h)
        e.Graphics.InterpolationMode = Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        e.Graphics.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
        e.Graphics.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
        e.Graphics.DrawImage(pbImage.Image, rect)
        e.Graphics.DrawRectangle(Pens.White, rect)

        rect = New Rectangle(162, barTop + h + 1, 280, 15)
        e.Graphics.DrawString(lblbarcode.Text, printFont10_Normal, Brushes.Black, rect, sf)
        e.Graphics.DrawRectangle(Pens.White, rect)

        rect = New Rectangle(162, barTop + h + 16, 280, 15)
        e.Graphics.DrawString("RS: " & lblprice.Text, printFont10_Normal, Brushes.Black, rect, sf)
        e.Graphics.DrawRectangle(Pens.White, rect)



    End Sub
    Private Sub generateBarcode()
        Try
            If lblbarcode.Text = "" Then Exit Sub
            Dim NewBarcode As IDAutomation.Windows.Forms.LinearBarCode.Barcode = New IDAutomation.Windows.Forms.LinearBarCode.Barcode()
            NewBarcode.DataToEncode = lblbarcode.Text.ToString() 'Input of textbox to generate barcode 
            NewBarcode.SymbologyID = Symbologies.Code39
            NewBarcode.Code128Set = Code128CharacterSets.A
            NewBarcode.RotationAngle = RotationAngles.Zero_Degrees
            NewBarcode.LeftMarginCM = 0
            NewBarcode.TopMarginCM = 0
            NewBarcode.ShowText = False
            NewBarcode.Size = New System.Drawing.Size(100, 100)
            NewBarcode.AutoSizeMode = Windows.Forms.AutoSizeMode.GrowAndShrink
            NewBarcode.RefreshImage()
            NewBarcode.Resolution = Resolutions.Screen
            NewBarcode.ResolutionCustomDPI = 96
            NewBarcode.RefreshImage()
            If FileExists(Application.StartupPath & "\" & "SavedBarcode.Jpeg") Then
                System.IO.File.Delete(Application.StartupPath & "\" & "SavedBarcode.Jpeg")
            End If
            NewBarcode.SaveImageAs("SavedBarcode.Jpeg", System.Drawing.Imaging.ImageFormat.Jpeg)
            NewBarcode.Resolution = Resolutions.Printer
            Dim bmp As Bitmap = Image.FromFile(Application.StartupPath & "\" & "SavedBarcode.Jpeg")
            imgIDAutomation.Image = New Bitmap(bmp)
            bmp.Dispose()
            GC.Collect()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        End Try
        
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If Val(cmbformat.Tag & "") = 0 Then
            MsgBox("Invalid format", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If chkprintFromExt.Checked Then
            'Dim dt As DataTable
            'Dim ppath As String = ""
            'dt = _Objcmn._fldDatatable("select bartenderpath from companydb")
            'If dt.Rows.Count > 0 Then
            '    ppath = Trim(dt(0)("bartenderpath") & "")
            'End If
            If bartenderpath = "" Then
                bartenderpath = DPath & "\barcode.txt"
            End If
            If Not FileExists(bartenderpath) Then
                File.Copy(Application.StartupPath & "\barcode.txt", DPath & "\barcode.txt")
            End If
            If bartenderpath = "" Then
                If Not FileExists(bartenderpath) Then
                    File.Copy(Application.StartupPath & "\barcode.txt", Application.StartupPath & "\barcode.txt")
                End If
                MsgBox("Please set Barcode Print Path", MsgBoxStyle.Exclamation)
            Else
                Dim fl As System.IO.StreamWriter
                fl = My.Computer.FileSystem.OpenTextFileWriter(bartenderpath, False)
                fl.WriteLine("ProductName,Code,Rate,MRP,PDate,Edate,Nos")
                Dim query As String
                Dim unitprice As Double
                If chkIsTaxPrice.Checked Then
                    unitprice = CDbl(lbltaxprice.Text)
                Else
                    unitprice = CDbl(lblprice.Text)
                End If
                Dim nformat As String = "0" & IIf(NoOfDecimal = 0, "", "." & StrDup(NoOfDecimal, "0"))
                query = lblitem.Text & "," & lblbarcode.Text & "," & Format(CDbl(unitprice), nformat) & "," & Format(CDbl(lblmrp.Text), nformat) & "," & DateValue(cldrStartDate.Value) & "," & DateValue(dtpto.Value) & "," & Val(txtqty.Text)
                fl.WriteLine(query)
                fl.Close()
            End If
            'MsgBox("Print has been set", MsgBoxStyle.Information)
            Dim appstart As String = Application.StartupPath & "\BarTender\bartend.exe"

            If FileExists(appstart) = False Then
                MsgBox("Bar Code Application not found! Please contact vendor", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            Dim dt As DataTable
            dt = _objcmnbLayer._fldDatatable("SELECT path FROM BarcodeFormatTb WHERE barid=" & Val(cmbformat.Tag & ""))
            Dim formatname As String = ""
            If dt.Rows.Count > 0 Then
                formatname = dt(0)("path")
            End If
            If formatname = "" Then
                MsgBox("Format file not found", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            Dim psi As New ProcessStartInfo(appstart, formatname)
            Process.Start(psi)
            Me.Close()
            Exit Sub
        End If
        If lblbarcode.Text = "" Then
            MsgBox("Invlaid Barcode", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If Val(txtqty.Text) = 0 Then
            MsgBox("Invalid Qty", MsgBoxStyle.Exclamation)
            txtqty.Focus()
            Exit Sub
        End If
        If cmbprinter.Text = "" Then
            MsgBox("Invalid Printer", MsgBoxStyle.Exclamation)
            cmbprinter.Focus()
            Exit Sub
        End If
        Try
            pdPrint = New PrintDocument
            pdPrint.PrinterSettings.PrinterName = cmbprinter.Text
            pdPrint.PrintController = New StandardPrintController
            Dim psz As New Printing.PaperSize
            psz.RawKind = Printing.PaperKind.Custom
            psz.Width = 320
            psz.Height = 100
            If pdPrint.PrinterSettings.IsValid Then
                pdPrint.DocumentName = PrintDocType
                pdPrint.DefaultPageSettings.PaperSize = psz
                pdPrint.PrinterSettings.Copies = Val(txtqty.Text)
                pdPrint.Print()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
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

    Private Sub cmbformat_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbformat.SelectedIndexChanged
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT barid FROM BarcodeFormatTb WHERE barname='" & cmbformat.Text & "'")
        If dt.Rows.Count > 0 Then
            cmbformat.Tag = dt(0)("barid")
        End If
    End Sub


    Private Sub btndefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndefault.Click
        If Val(cmbformat.Tag & "") > 0 Then
            _objcmnbLayer._saveDatawithOutParm("update BarcodeFormatTb set isdefalult=1 where barid=" & Val(cmbformat.Tag & ""))
            MsgBox("Updated", MsgBoxStyle.Information)
        Else
            MsgBox("Invalid Format", MsgBoxStyle.Exclamation)
        End If
       
    End Sub

    Private Sub btnremove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnremove.Click
        If MsgBox("Do you want to remove the format?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        _objcmnbLayer._saveDatawithOutParm("delete from BarcodeFormatTb  where barid=" & Val(cmbformat.Tag & ""))
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
    Private Sub isPrintTaxPrice()
        chgbypgm = True
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select isnull(isTaxItemInBarcode,0)isTaxItemInBarcode from CompanyTb")
        If dt.Rows.Count > 0 Then
            If Not IsDBNull(dt(0)("isTaxItemInBarcode")) Then
                chkIsTaxPrice.Checked = dt(0)("isTaxItemInBarcode")
            Else
                chkIsTaxPrice.Checked = False
            End If
        End If
        chgbypgm = False
    End Sub

    Private Sub chkIsTaxPrice_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIsTaxPrice.CheckedChanged
        If chgbypgm Then Exit Sub
        If chkIsTaxPrice.Checked = True Then
            If MsgBox("Do you want set Tax Price as Price?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                chgbypgm = True
                chkIsTaxPrice.Checked = False
                chgbypgm = False
                Exit Sub
            End If

        Else
            If MsgBox("Do you want set Unit Pice as Price?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        End If
        _objcmnbLayer._saveDatawithOutParm("UPDATE CompanyTb SET isTaxItemInBarcode=" & IIf(chkIsTaxPrice.Checked, 1, 0))
    End Sub
End Class