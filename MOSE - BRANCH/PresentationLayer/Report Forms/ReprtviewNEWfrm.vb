Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports System.Xml
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO
Imports System.Drawing.Printing
Public Class ReprtviewNEWfrm
    Public Event doClose()
    Public Event doBack()
    Private myDocumentToPrint As PrintDocument
    Private myPageAlreadySetUp As Boolean = False
    Dim _objcmnbLayer As New clsCommon_BL
    Private rpt As ReportDocument = New ReportDocument()
    Public tmpDt As DataTable ' only for add data table after adding company table
    Private PrinterName As String

    Public Sub SetReport(ByVal DS As DataSet, ByVal FileName As String, ByVal rsCount As Integer, Optional ByVal DisplayGroupTree As Boolean = False, Optional ByVal vrtype As Integer = 0, Optional ByVal ToPrint As Boolean = False)
        If FileName = "" Then Exit Sub
        Dim sqlstmt As String
        Dim varXml As String
        Dim dtCmp As DataTable
        Dim Rptfls As New DataTable
        Dim printername As String = ""
        dtCmp = _objcmnbLayer.returnCompanayDetailsForReport(vrtype)
        DS.Tables.Add(dtCmp)

        If Not tmpDt Is Nothing Then
            DS.Tables.Add(tmpDt)
        End If
        Dim i As Integer
        varXml = Mid(FileName, FileName.LastIndexOf("\") + 2)
        Rptfls = _objcmnbLayer._fldDatatable("SELECT Printername FROM RptCmnTb LEFT JOIN RptFls ON RptFls.RptType = RptCmnTb.RptType WHERE RptName = '" & varXml & "'")
        If Rptfls.Rows.Count > 0 Then
            printername = Trim(Rptfls(0)("Printername") & "")
        End If
        varXml = Mid(varXml, 1, varXml.IndexOf("."))
        If Not FileExists(Path.GetTempPath & varXml & ".xml") Then
            DS.WriteXmlSchema(Path.GetTempPath & varXml & ".xml")
        End If
        rpt.Load(FileName)
        For i = 0 To DS.Tables.Count - 1
            On Error Resume Next
            rpt.Database.Tables(i).SetDataSource(DS.Tables(i))
        Next
        crView.ActiveViewIndex = 0
        crView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        crView.DisplayGroupTree = DisplayGroupTree
        crView.Location = New System.Drawing.Point(0, 0)
        crView.ReportSource = rpt
        If printername <> "" Then
            rpt.PrintOptions.PrinterName = printername
            'rpt.PrintOptions.PaperOrientation = PaperOrientation.Portrait
        End If
        If ToPrint Then
            rpt.PrintToPrinter(1, False, 0, 0)
            'crView.PrintReport()
        Else
            crView.RefreshReport()
        End If

        Exit Sub
ErrEXIT:
        MsgBox(Err.Description)
        Exit Sub
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        RaiseEvent doBack()
    End Sub

    Private Sub btnSetup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetup.Click
        'Dim psd As New PageSetupDialog
        'With psd
        '    .AllowMargins = True
        '    .AllowOrientation = True
        '    .AllowPaper = True
        '    .AllowPrinter = True
        '    .ShowHelp = True
        '    .ShowNetwork = True
        '    .Document = myDocumentToPrint
        'End With
        'psd.ShowDialog()
        If PrintDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            PrinterName = PrintDialog1.PrinterSettings.PrinterName
            If PrinterName <> "" Then
                rpt.PrintOptions.PrinterName = PrinterName
            End If
            rpt.PrintToPrinter(1, False, 0, 0)
        End If
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        rpt.Refresh()
        If (PrinterName <> "") Then
            rpt.PrintOptions.PrinterName = PrinterName
        End If
        rpt.PrintToPrinter(1, False, 0, 0)
        If Val(btnprint.Tag) > 0 Then
            _objcmnbLayer._saveDatawithOutParm("Update ItmInvCmnTb set Printed=1 where trid=" & Val(btnprint.Tag))
        End If
    End Sub
    
    Private Sub btnwhatsapp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnwhatsapp.Click
        Dim frm As New WhatsAppFrm
        frm.ShowDialog()
    End Sub

    Private Sub ReprtviewNEWfrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class