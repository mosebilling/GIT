Imports Microsoft.Office.Interop
Imports System.IO
Imports System.Web.Script.Serialization

Public Class GSTR1
    Private _objreport As clsReport_BL
    Private _objcmn As clsCommon_BL
    Private ds As DataSet
    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        returnGSTR1()
       
    End Sub
    Private Sub returnGSTR1()
        _objreport = New clsReport_BL
        ds = _objreport.returnGSTR1(DateValue(cldrStartDate.Value), DateValue(cldrEnddate.Value), IIf(chkb2b.Checked, 1, 0))
        setGridHead()
        _objreport = Nothing
    End Sub
    Private Sub loadB2B()

    End Sub
    'xlsx
    Private Sub ExportGSTR1ToExcel(ByVal ds As DataSet)
        Try
            Dim dtTable As DataTable = ds.Tables(0)
            Dim filename As String = ""
            FolderBrowserDialog1.ShowDialog()
            If FolderBrowserDialog1.SelectedPath <> "" Then
                filename = FolderBrowserDialog1.SelectedPath
            Else
                Exit Sub
            End If
            filename = filename & "/GSTR1.xlsx"
            Dim app As New Excel.Application
            Dim wb As Excel.Workbook = app.Workbooks.Add()
            Dim ws As Excel.Worksheet
            'B2B
            addsheet(app, wb, ws, True, "B2B", ds.Tables(0))
            'B2C
            addsheet(app, wb, ws, False, "B2C", ds.Tables(1))
            'HSN Wise
            addsheet(app, wb, ws, False, "HSNWise", ds.Tables(2))
            'B2B Sales Return
            addsheet(app, wb, ws, False, "B2B Sales Return", ds.Tables(3))
            'EXMPTED
            addsheet(app, wb, ws, False, "EXMPTED", ds.Tables(4))
            'Document issued (IS)
            addsheet(app, wb, ws, False, "Document issued (IS)", ds.Tables(5))
            'Document issued (SR)
            addsheet(app, wb, ws, False, "Document issued (SR)", ds.Tables(6))
            If FileExists(filename) Then
                File.Delete(filename)
            End If
            wb.SaveAs(filename)    'save and close the WorkBook
            wb.Close()

            MsgBox("Export completed", MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        End Try

    End Sub

    Private Sub btnexcell_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexcell.Click
        ExportGSTR1ToExcel(ds)
    End Sub
    Private Function addQts() As String
        Return """"
    End Function
    Private Function additm_det(ByVal dt As DataTable, ByVal i As Integer)
        Dim jsonstring As String
        Dim numf As String = "###0.00"
        jsonstring = "{" & addQts() & "txval" & addQts() & ":" & Format(dt(i)("Taxable Value"), numf) & ","
        jsonstring = jsonstring & addQts() & "rt" & addQts() & ":" & Format(dt(i)("TaxP"), numf) & ","
        jsonstring = jsonstring & addQts() & "camt" & addQts() & ":" & Format(dt(i)("CGST AMT"), numf) & ","
        jsonstring = jsonstring & addQts() & "samt" & addQts() & ":" & Format(dt(i)("SGST AMT"), numf) & ","
        jsonstring = jsonstring & addQts() & "csamt" & addQts() & ":" & Format(dt(i)("cessAmt"), numf) & "}"
        Return jsonstring
    End Function
    Private Sub gstrToJson(ByVal ds As DataSet)
        Dim dt As DataTable
        Dim jsonstring As String
        Dim gstn As String = ""
        Dim monthyear As String = Format(Month(cldrStartDate.Value), "00") & Format(Year(cldrStartDate.Value), "0000")
        _objcmn = New clsCommon_BL()
        dt = _objcmn._fldDatatable("Select GSTN from CompanyTb")
        If dt.Rows.Count > 0 Then
            gstn = dt(0)("GSTN")
        End If
        'dt.Rows.Clear()
        dt = ds.Tables(0)
        'begin
        jsonstring = "{" & addQts() & "gstin" & addQts() & ":" & addQts() & gstn & addQts() & ","
        jsonstring = jsonstring & addQts() & "fp" & addQts() & ":" & addQts() & monthyear & addQts() & ","
        jsonstring = jsonstring & addQts() & "version" & addQts() & ":" & addQts() & "GST3.0.3" & addQts() & ","
        jsonstring = jsonstring & addQts() & "hash" & addQts() & ":" & addQts() & "hash" & addQts() & ","
        'start b2b
        jsonstring = jsonstring & addQts() & "b2b" & addQts() & ":" & "["
        Dim i As Integer
        Dim cnt As Integer = dt.Rows.Count
        Dim pGSTN As String = ""
        Dim prate As String = ""
        Dim pInvno As String = ""
        Dim netAmt As Double
        Dim slno As Integer
        Dim numf As String = "###0.00"
        If dt.Rows.Count > 0 Then
            For i = 0 To cnt - 1
                If pInvno = dt(i)("InvNo") Then
                    jsonstring = jsonstring & ","
                Else
                    If pInvno <> "" Then jsonstring = jsonstring & "]}"
                    If pGSTN = dt(i)("GSTN") Then jsonstring = jsonstring & ","
                    'end inv
                End If
                If pGSTN <> dt(i)("GSTN") Then
                    If pInvno <> "" Then jsonstring = jsonstring & "]"
                    If pGSTN <> "" Then jsonstring = jsonstring & "},"
                    'end inv
                    'start ctin
                    jsonstring = jsonstring & "{"
                    jsonstring = jsonstring & addQts() & "ctin" & addQts() & ":" & addQts() & dt(i)("GSTN") & addQts() & ","
                    'start inv
                    jsonstring = jsonstring & addQts() & "inv" & addQts() & ":["
                End If
                If pInvno <> dt(i)("InvNo") Then
                    'If pInvno <> "" Then jsonstring = jsonstring & "},"
                    netAmt = CDbl(dt.Compute("sum([Taxable Value])", "InvNo='" & dt(i)("InvNo") & "'"))
                    netAmt = netAmt + CDbl(dt.Compute("sum([CGST AMT])", "InvNo='" & dt(i)("InvNo") & "'"))
                    netAmt = netAmt + CDbl(dt.Compute("sum([SGST AMT])", "InvNo='" & dt(i)("InvNo") & "'"))
                    netAmt = netAmt + CDbl(dt.Compute("sum([cessAmt])", "InvNo='" & dt(i)("InvNo") & "'"))
                    netAmt = Math.Round(netAmt, NoOfDecimal)
                    Dim invno As String = dt(i)("InvNo")
                    invno = invno.Replace("\", "/")
                    jsonstring = jsonstring & "{"
                    jsonstring = jsonstring & addQts() & "inum" & addQts() & ":" & addQts() & invno & addQts() & ","
                    jsonstring = jsonstring & addQts() & "idt" & addQts() & ":" & addQts() & Format(dt(i)("Inv Date"), "dd-MM-yyyy") & addQts() & ","
                    jsonstring = jsonstring & addQts() & "val" & addQts() & ":" & Format(netAmt, numf) & ","
                    jsonstring = jsonstring & addQts() & "pos" & addQts() & ":" & addQts() & dt(i)("POS") & addQts() & ","
                    jsonstring = jsonstring & addQts() & "rchrg" & addQts() & ":" & addQts() & "N" & addQts() & ","
                    jsonstring = jsonstring & addQts() & "inv_typ" & addQts() & ":" & addQts() & "R" & addQts() & ","
                    slno = 0
                    'start itms
                    jsonstring = jsonstring & addQts() & "itms" & addQts() & ":["
                End If
                'If pInvno <> dt(i)("InvNo") Then
                '    'start itms
                '    jsonstring = jsonstring & addQts() & "itms" & addQts() & ":["
                'End If
                slno = slno + 1
                jsonstring = jsonstring & "{"
                jsonstring = jsonstring & addQts() & "num" & addQts() & ":" & slno & ","
                'start itm_det
                jsonstring = jsonstring & addQts() & "itm_det" & addQts() & ":" & additm_det(dt, i) & "}"
                'end itms

                If cnt - 1 = i Then
                    jsonstring = jsonstring & "]"
                    'end itms
                    jsonstring = jsonstring & "}]"
                    'end inv
                End If
                pGSTN = dt(i)("GSTN")
                pInvno = dt(i)("InvNo")
                prate = dt(i)("TaxP")
            Next
            jsonstring = jsonstring & "}]"
            'end b2b
        End If
        'start b2c
        'If dt.Rows.Count > 0 Then dt.Rows.Clear()
        dt = ds.Tables(1)
        If dt.Rows.Count > 0 Then
            If cnt > 0 Then jsonstring = jsonstring & ","
            jsonstring = jsonstring & addQts() & "b2cs" & addQts() & ":" & "["
            cnt = dt.Rows.Count
            For i = 0 To cnt - 1
                'start b2csdata
                jsonstring = jsonstring & "{"
                jsonstring = jsonstring & addQts() & "sply_ty" & addQts() & ":" & addQts() & "INTRA" & addQts() & ","
                jsonstring = jsonstring & addQts() & "rt" & addQts() & ":" & Format(dt(i)("TaxP"), numf) & ","
                jsonstring = jsonstring & addQts() & "typ" & addQts() & ":" & addQts() & "OE" & addQts() & ","
                jsonstring = jsonstring & addQts() & "pos" & addQts() & ":" & addQts() & dt(i)("POS") & addQts() & ","
                jsonstring = jsonstring & addQts() & "txval" & addQts() & ":" & Format(dt(i)("Taxable Value"), numf) & ","
                jsonstring = jsonstring & addQts() & "camt" & addQts() & ":" & Format(dt(i)("CGST AMT"), numf) & ","
                jsonstring = jsonstring & addQts() & "samt" & addQts() & ":" & Format(dt(i)("SGST AMT"), numf) & ","
                jsonstring = jsonstring & addQts() & "csamt" & addQts() & ":" & Format(dt(i)("cessAmt"), numf) & "}"
                'end b2csdata
                If cnt - 1 > i Then
                    jsonstring = jsonstring & ","
                End If
            Next
            jsonstring = jsonstring & "]"
            'end b2c
        End If
        'If dt.Rows.Count > 0 Then dt.Rows.Clear()

        'Sales Return
        dt = ds.Tables(3)
        pInvno = ""
        pGSTN = ""
        'start cdnr
        If dt.Rows.Count > 0 Then
            If cnt > 0 Then jsonstring = jsonstring & ","
            jsonstring = jsonstring & addQts() & "cdnr" & addQts() & ":" & "["
            cnt = dt.Rows.Count
            For i = 0 To cnt - 1
                If pInvno = dt(i)("InvNo") Then
                    jsonstring = jsonstring & ","
                Else
                    If pInvno <> "" Then jsonstring = jsonstring & "]}"
                    If pGSTN = dt(i)("GSTN") Then jsonstring = jsonstring & ","
                    'end inv
                End If
                If pGSTN <> dt(i)("GSTN") Then
                    If pInvno <> "" Then jsonstring = jsonstring & "]"
                    If pGSTN <> "" Then jsonstring = jsonstring & "},"
                    'end inv
                    'start ctin
                    jsonstring = jsonstring & "{"
                    jsonstring = jsonstring & addQts() & "ctin" & addQts() & ":" & addQts() & dt(i)("GSTN") & addQts() & ","
                    'start inv
                    jsonstring = jsonstring & addQts() & "nt" & addQts() & ":["
                End If
                If pInvno <> dt(i)("InvNo") Then
                    'If pInvno <> "" Then jsonstring = jsonstring & "},"
                    netAmt = CDbl(dt.Compute("sum([Taxable Value])", "InvNo='" & dt(i)("InvNo") & "'"))
                    netAmt = netAmt + CDbl(dt.Compute("sum([CGST AMT])", "InvNo='" & dt(i)("InvNo") & "'"))
                    netAmt = netAmt + CDbl(dt.Compute("sum([SGST AMT])", "InvNo='" & dt(i)("InvNo") & "'"))
                    netAmt = netAmt + CDbl(dt.Compute("sum([cessAmt])", "InvNo='" & dt(i)("InvNo") & "'"))
                    netAmt = Math.Round(netAmt, NoOfDecimal)
                    Dim invno As String = dt(i)("InvNo")
                    invno = invno.Replace("\", "/")
                    jsonstring = jsonstring & "{"
                    jsonstring = jsonstring & addQts() & "ntty" & addQts() & ":" & addQts() & "C" & addQts() & ","
                    jsonstring = jsonstring & addQts() & "nt_num" & addQts() & ":" & addQts() & invno & addQts() & ","
                    jsonstring = jsonstring & addQts() & "nt_dt" & addQts() & ":" & addQts() & Format(dt(i)("Inv Date"), "dd-MM-yyyy") & addQts() & ","
                    jsonstring = jsonstring & addQts() & "val" & addQts() & ":" & Format(netAmt, numf) & ","
                    jsonstring = jsonstring & addQts() & "pos" & addQts() & ":" & addQts() & dt(i)("POS") & addQts() & ","
                    jsonstring = jsonstring & addQts() & "rchrg" & addQts() & ":" & addQts() & "N" & addQts() & ","
                    jsonstring = jsonstring & addQts() & "inv_typ" & addQts() & ":" & addQts() & "R" & addQts() & ","
                    slno = 0
                    'start itms
                    jsonstring = jsonstring & addQts() & "itms" & addQts() & ":["
                End If
                'If pInvno <> dt(i)("InvNo") Then
                '    'start itms
                '    jsonstring = jsonstring & addQts() & "itms" & addQts() & ":["
                'End If
                slno = slno + 1
                jsonstring = jsonstring & "{"
                jsonstring = jsonstring & addQts() & "num" & addQts() & ":" & slno & ","
                'start itm_det
                jsonstring = jsonstring & addQts() & "itm_det" & addQts() & ":" & additm_det(dt, i) & "}"
                'end itms

                If cnt - 1 = i Then
                    jsonstring = jsonstring & "]"
                    'end itms
                    jsonstring = jsonstring & "}]"
                    'end inv
                End If
                pGSTN = dt(i)("GSTN")
                pInvno = dt(i)("InvNo")
                prate = dt(i)("TaxP")
            Next
            jsonstring = jsonstring & "}]"
            'end cdnr
        End If
        'Document issued [IS]
        dt = ds.Tables(5)
        Dim srisExist As Integer
        If dt.Rows.Count > 0 Then
            If cnt > 0 Then jsonstring = jsonstring & ","
            cnt = dt.Rows.Count
            'start doc_issue
            jsonstring = jsonstring & addQts() & "doc_issue" & addQts() & ":" & "{"
            'start doc_det
sr:
            If srisExist = 1 Then
                dt = ds.Tables(6)
                If cnt > 0 Then jsonstring = jsonstring & ",{"
                cnt = dt.Rows.Count
            Else
                jsonstring = jsonstring & addQts() & "doc_det" & addQts() & ":" & "[{"
            End If
            jsonstring = jsonstring & addQts() & "doc_num" & addQts() & ":" & cnt + 1 & ","
            If srisExist = 1 Then
                jsonstring = jsonstring & addQts() & "doc_typ" & addQts() & ":" & addQts() & "Credit Note" & addQts() & ","
            Else
                jsonstring = jsonstring & addQts() & "doc_typ" & addQts() & ":" & addQts() & "Invoices for outward supply" & addQts() & ","
            End If

            'start docs
            jsonstring = jsonstring & addQts() & "docs" & addQts() & ":" & "["
            For i = 0 To cnt - 1
                'start docsdata
                jsonstring = jsonstring & "{"
                jsonstring = jsonstring & addQts() & "num" & addQts() & ":" & i + 1 & ","
                jsonstring = jsonstring & addQts() & "from" & addQts() & ":" & addQts() & dt(i)("InvFrom") & addQts() & ","
                jsonstring = jsonstring & addQts() & "to" & addQts() & ":" & addQts() & dt(i)("InvTo") & addQts() & ","
                jsonstring = jsonstring & addQts() & "totnum" & addQts() & ":" & dt(i)("Issued Count") & ","
                jsonstring = jsonstring & addQts() & "cancel" & addQts() & ":" & dt(i)("Cancelled") & ","
                jsonstring = jsonstring & addQts() & "net_issue" & addQts() & ":" & dt(i)("Total") & "}"
                'end docsdata
                If cnt - 1 > i Then
                    jsonstring = jsonstring & ","
                End If
            Next
            'Document issued [SR]
            If srisExist = 0 Then
                jsonstring = jsonstring & "]"
                'end docs
                jsonstring = jsonstring & "}"
                dt = ds.Tables(6)
                If dt.Rows.Count > 0 Then
                    srisExist = srisExist + 1
                    GoTo sr
                End If
            End If
            
           
            jsonstring = jsonstring & "]"
            'end docs
            jsonstring = jsonstring & "}]"
            'end doc_det
            jsonstring = jsonstring & "}"
            'end doc_issue
        End If
        'If dt.Rows.Count > 0 Then dt.Rows.Clear()
        'HSN wise 
        dt = ds.Tables(2)
        If dt.Rows.Count > 0 Then
            If cnt > 0 Then jsonstring = jsonstring & ","
            'start hsn
            jsonstring = jsonstring & addQts() & "hsn" & addQts() & ":" & "{"
            'start data
            jsonstring = jsonstring & addQts() & "data" & addQts() & ":" & "["
            cnt = dt.Rows.Count
            For i = 0 To cnt - 1
                'start hsndata
                jsonstring = jsonstring & "{"
                jsonstring = jsonstring & addQts() & "num" & addQts() & ":" & i + 1 & ","
                jsonstring = jsonstring & addQts() & "hsn_sc" & addQts() & ":" & addQts() & dt(i)("HSNCode") & addQts() & ","
                jsonstring = jsonstring & addQts() & "desc" & addQts() & ":" & addQts() & "GENERAL" & addQts() & ","
                jsonstring = jsonstring & addQts() & "uqc" & addQts() & ":" & addQts() & dt(i)("Unit") & addQts() & ","
                jsonstring = jsonstring & addQts() & "qty" & addQts() & ":" & Format(dt(i)("QTY"), numf) & ","
                jsonstring = jsonstring & addQts() & "rt" & addQts() & ":" & Format(dt(i)("TaxP"), numf) & ","
                jsonstring = jsonstring & addQts() & "txval" & addQts() & ":" & Format(dt(i)("Taxable Value"), numf) & ","
                jsonstring = jsonstring & addQts() & "iamt" & addQts() & ":" & Format(dt(i)("IGST Amt"), numf) & ","
                jsonstring = jsonstring & addQts() & "samt" & addQts() & ":" & Format(dt(i)("SGST AMT"), numf) & ","
                jsonstring = jsonstring & addQts() & "camt" & addQts() & ":" & Format(dt(i)("CGST AMT"), numf) & ","
                jsonstring = jsonstring & addQts() & "csamt" & addQts() & ":" & Format(dt(i)("cessAmt"), numf) & "}"
                'end hsndata
                If cnt - 1 > i Then
                    jsonstring = jsonstring & ","
                End If
            Next
            jsonstring = jsonstring & "]"
            'end data
            jsonstring = jsonstring & "}"
            'end hsn
        End If
        jsonstring = jsonstring & "}"
        'end json string
        Dim filename As String = ""
        monthyear = MonthName(Month(cldrStartDate.Value)) & "_" & Year(cldrStartDate.Value)
        FolderBrowserDialog1.ShowDialog()
        If FolderBrowserDialog1.SelectedPath <> "" Then
            filename = FolderBrowserDialog1.SelectedPath
        Else
            Exit Sub
        End If
        
        filename = filename & "\" & gstn & "_" & monthyear & "_GSTR1" & ".json"
        If FileExists(filename) Then
            File.Delete(filename)
        End If
        If System.IO.File.Exists(filename) = False Then
            System.IO.File.Create(filename).Dispose()
        End If
        Dim objWriter As New System.IO.StreamWriter(filename, True)
        objWriter.WriteLine(jsonstring)
        objWriter.Close()
        MsgBox("Export Completed", MsgBoxStyle.Information)
    End Sub
    Private Sub addsheet(ByVal app As Excel.Application, ByVal wb As Excel.Workbook, ByVal ws As Excel.Worksheet, ByVal isfirstSheet As Boolean, ByVal sheetName As String, ByVal dtTable As DataTable)
        'copy the datatable to an array
        Dim rcount As Integer = dtTable.Rows.Count
        If rcount = 0 Then rcount = 1
        Dim arr(rcount, dtTable.Columns.Count) As Object
        Dim r As Int32, c As Int32
        If dtTable.Rows.Count > 0 Then
            For r = 0 To dtTable.Rows.Count - 1
                For c = 0 To dtTable.Columns.Count - 1
                    arr(r, c) = dtTable.Rows(r).Item(c)
                Next
            Next
        Else
            For r = 0 To 0
                For c = 0 To dtTable.Columns.Count - 1
                    arr(r, c) = ""
                Next
            Next
        End If
        If isfirstSheet Then
            ws = DirectCast(app.ActiveWorkbook.ActiveSheet, Excel.Worksheet)
        Else
            ws = wb.Sheets.Add(After:=wb.Sheets(wb.Sheets.Count))
        End If
        c = 0
        For Each column As DataColumn In dtTable.Columns
            ws.Cells(1, c + 1) = column.ColumnName
            ws.Cells(1, c + 1).Font.Bold = True
            'Dim a As String = ws.Cells(1, c + 1)
            'ws.Range(ws.Cells(1, c + 1)).EntireColumn.AutoFit()
            c += 1
        Next
        'ws.UsedRange.AutoFit()
        ws.Name = sheetName
        'add the data starting in cell A2
        ws.Range(ws.Cells(2, 1), ws.Cells(rcount + 1, dtTable.Columns.Count)).Value = arr

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        If TabControl1.SelectedIndex = 0 Then
            SetGridProperty(grdb2b)
            resizeGridColumn(grdb2b, 1)
        End If
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub TabControl1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.Click
        setGridHead()
    End Sub

    Private Sub GSTR1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        returnGSTR1()
        Timer1.Enabled = True
    End Sub
    Private Sub setGridHead()
        Select Case TabControl1.SelectedIndex
            Case 0 'b2b
                grdb2b.DataSource = ds.Tables(0)
                SetGridProperty(grdb2b)
                With grdb2b
                    .Columns(0).Width = 125
                    .Columns("TaxP").DefaultCellStyle.Format = "N" & NoOfDecimal
                    .Columns("TaxP").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns("Taxable Value").DefaultCellStyle.Format = "N" & NoOfDecimal
                    .Columns("Taxable Value").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns("CGST AMT").DefaultCellStyle.Format = "N" & NoOfDecimal
                    .Columns("CGST AMT").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns("SGST AMT").DefaultCellStyle.Format = "N" & NoOfDecimal
                    .Columns("SGST AMT").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns("IGST AMT").DefaultCellStyle.Format = "N" & NoOfDecimal
                    .Columns("IGST AMT").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns("qty").DefaultCellStyle.Format = "N" & NoOfDecimal
                    .Columns("qty").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End With
                resizeGridColumn(grdb2b, 1)
            Case 1 'b2c
                grdb2c.DataSource = ds.Tables(1)
                SetGridProperty(grdb2c)
                With grdb2c
                    .Columns("TaxP").DefaultCellStyle.Format = "N" & NoOfDecimal
                    .Columns("TaxP").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns("qty").DefaultCellStyle.Format = "N" & NoOfDecimal
                    .Columns("qty").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns("Taxable Value").DefaultCellStyle.Format = "N" & NoOfDecimal
                    .Columns("Taxable Value").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns("CGST AMT").DefaultCellStyle.Format = "N" & NoOfDecimal
                    .Columns("CGST AMT").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns("SGST AMT").DefaultCellStyle.Format = "N" & NoOfDecimal
                    .Columns("SGST AMT").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns("IGST AMT").DefaultCellStyle.Format = "N" & NoOfDecimal
                    .Columns("IGST AMT").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End With
                resizeGridColumn(grdb2c, 0)
            Case 2 'hsn
                grdHSN.DataSource = ds.Tables(2)
                SetGridProperty(grdHSN)
                With grdHSN
                    .Columns("TaxP").DefaultCellStyle.Format = "N" & NoOfDecimal
                    .Columns("TaxP").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns("Taxable Value").DefaultCellStyle.Format = "N" & NoOfDecimal
                    .Columns("Taxable Value").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns("CGST AMT").DefaultCellStyle.Format = "N" & NoOfDecimal
                    .Columns("CGST AMT").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns("SGST AMT").DefaultCellStyle.Format = "N" & NoOfDecimal
                    .Columns("SGST AMT").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns("IGST AMT").DefaultCellStyle.Format = "N" & NoOfDecimal
                    .Columns("IGST AMT").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns("Total Value").DefaultCellStyle.Format = "N" & NoOfDecimal
                    .Columns("Total Value").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns("qty").DefaultCellStyle.Format = "N" & NoOfDecimal
                    .Columns("qty").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End With
                resizeGridColumn(grdHSN, 0)
            Case 3 'SR
                grdb2bSR.DataSource = ds.Tables(3)
                SetGridProperty(grdb2bSR)
                With grdb2bSR
                    .Columns(0).Width = 125
                    .Columns("TaxP").DefaultCellStyle.Format = "N" & NoOfDecimal
                    .Columns("TaxP").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns("Taxable Value").DefaultCellStyle.Format = "N" & NoOfDecimal
                    .Columns("Taxable Value").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns("CGST AMT").DefaultCellStyle.Format = "N" & NoOfDecimal
                    .Columns("CGST AMT").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns("SGST AMT").DefaultCellStyle.Format = "N" & NoOfDecimal
                    .Columns("SGST AMT").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns("IGST AMT").DefaultCellStyle.Format = "N" & NoOfDecimal
                    .Columns("IGST AMT").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns("qty").DefaultCellStyle.Format = "N" & NoOfDecimal
                    .Columns("qty").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End With
                resizeGridColumn(grdb2bSR, 1)
            Case 4 'EXMPTED
                grdexempted.DataSource = ds.Tables(4)
                SetGridProperty(grdexempted)
                With grdexempted
                    .Columns("Taxable Value").DefaultCellStyle.Format = "N" & NoOfDecimal
                    .Columns("Taxable Value").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End With
                resizeGridColumn(grdexempted, 0)
            Case 5 'docIS
                grddocIS.DataSource = ds.Tables(5)
                SetGridProperty(grddocIS)
                resizeGridColumn(grddocIS, 0)
            Case 6 'docSR
                grddocSR.DataSource = ds.Tables(6)
                SetGridProperty(grddocSR)
                resizeGridColumn(grddocSR, 0)
        End Select
    End Sub

    Private Sub btnjson_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnjson.Click
        gstrToJson(ds)
    End Sub

    Private Sub chkb2b_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkb2b.Click
        returnGSTR1()
    End Sub

    Private Sub grdb2b_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdb2b.CellContentClick

    End Sub
End Class