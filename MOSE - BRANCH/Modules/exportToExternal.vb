Imports Microsoft
Imports Microsoft.Office.Interop
Imports System.IO
Module exportToExternal
    'datagridview to excell
    Public Function GridExport(ByVal DGV As DataGridView, ByVal filename As String) As Boolean
        Try
            If DGV.RowCount > 0 Then
                filename = filename & "\Export_Qty.xls"
                DGV.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
                DGV.SelectAll()
                IO.File.WriteAllText(filename, DGV.GetClipboardContent().GetText.TrimEnd)
                DGV.ClearSelection()
                Process.Start(filename)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        End Try

    End Function
    'datattable to excell .xlsx
    Public Sub DataTableToExcel(ByVal filename As String, ByVal dtTable As DataTable)
        Try
            Dim app As New Excel.Application
            Dim wb As Excel.Workbook = app.Workbooks.Add()
            Dim ws As Excel.Worksheet
            Dim arr(dtTable.Rows.Count, dtTable.Columns.Count) As Object
            Dim r As Int32, c As Int32
            'copy the datatable to an array
            For r = 0 To dtTable.Rows.Count - 1
                For c = 0 To dtTable.Columns.Count - 1
                    arr(r, c) = dtTable.Rows(r).Item(c)
                Next
            Next
            ws = DirectCast(app.ActiveWorkbook.ActiveSheet, Excel.Worksheet)
            'to add new worksheet
            'ws = wb.Sheets.Add(After:=wb.Sheets(wb.Sheets.Count))
            'ws.Name = "Sheet1"   'name the worksheet
            'add the column headers starting in A1
            c = 0
            For Each column As DataColumn In dtTable.Columns
                ws.Cells(1, c + 1) = column.ColumnName
                ws.Cells(1, c + 1).Font.Bold = True
                c += 1
            Next
            'add the data starting in cell A2
            ws.Range(ws.Cells(2, 1), ws.Cells(dtTable.Rows.Count + 1, dtTable.Columns.Count)).Value = arr
            If FileExists(filename) Then
                File.Delete(filename)
            End If
            wb.SaveAs(filename)    'save and close the WorkBook
            wb.Close()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        End Try

    End Sub
    'datattable to excell .xls
    Public Function ExportToExcel(ByVal a_sFilename As String, ByVal a_sFileTitle As String, ByRef a_sErrorMessage As String, ByVal dtTable As DataTable) As Boolean
        a_sErrorMessage = String.Empty
        Dim bRetVal As Boolean = False
        Dim dsDataSet As New DataSet

        Try
            Dim dt As DataTable = dtTable.Copy
            dsDataSet.Tables.Add(dt)

            Dim xlObject As Excel.Application = Nothing
            Dim xlWB As Excel.Workbook = Nothing
            Dim xlSh As Excel.Worksheet = Nothing
            Dim rg As Excel.Range = Nothing
            Try
                xlObject = New Excel.Application()
                xlObject.AlertBeforeOverwriting = False
                xlObject.DisplayAlerts = False

                ''This Adds a new woorkbook, you could open the workbook from file also
                xlWB = xlObject.Workbooks.Add(Type.Missing)
                xlWB.SaveAs(a_sFilename, 56)

                xlSh = DirectCast(xlObject.ActiveWorkbook.ActiveSheet, Excel.Worksheet)

                'Dim sUpperRange As String = "A1"
                'Dim sLastCol As String = "AQ"
                'Dim sLowerRange As String = sLastCol + (dsDataSet.Tables(0).Rows.Count + 1).ToString()

                For j = 0 To dsDataSet.Tables(0).Columns.Count - 1
                    xlSh.Cells(1, j + 1) = _
                        dsDataSet.Tables(0).Columns(j).ToString()
                    xlSh.Cells(1, j + 1).Font.Bold = True
                Next

                For i = 1 To dsDataSet.Tables(0).Rows.Count
                    For j = 0 To dsDataSet.Tables(0).Columns.Count - 1
                        xlSh.Cells(i + 1, j + 1) = _
                            dsDataSet.Tables(0).Rows(i - 1)(j).ToString()
                    Next
                Next
                xlSh.Columns.AutoFit()
                'rg = xlSh.Range(sUpperRange, sLowerRange)
                'rg.Value2 = GetData(dsDataSet.Tables(0))

                'xlSh.Range("A1", sLastCol & "1").Font.Bold = True
                'xlSh.Range("A1", sLastCol & "1").HorizontalAlignment = XlHAlign.xlHAlignCenter
                'xlSh.Range(sUpperRange, sLowerRange).EntireColumn.AutoFit()

                If String.IsNullOrEmpty(a_sFileTitle) Then
                    xlObject.Caption = "untitled"
                Else
                    xlObject.Caption = a_sFileTitle
                End If

                xlWB.Save()
                bRetVal = True
            Catch ex As System.Runtime.InteropServices.COMException
                If ex.ErrorCode = -2147221164 Then
                    a_sErrorMessage = "Error in export: Please install Microsoft Office (Excel) to use the Export to Excel feature."
                ElseIf ex.ErrorCode = -2146827284 Then
                    a_sErrorMessage = "Error in export: Excel allows only 65,536 maximum rows in a sheet."
                Else
                    a_sErrorMessage = (("Error in export: " & ex.Message) + Environment.NewLine & " Error: ") + ex.ErrorCode
                End If
            Catch ex As Exception
                a_sErrorMessage = "Error in export: " & ex.Message
            Finally
                Try
                    If xlWB IsNot Nothing Then
                        xlWB.Close(Nothing, Nothing, Nothing)
                    End If
                    xlObject.Workbooks.Close()
                    xlObject.Quit()
                Catch
                End Try
                xlSh = Nothing
                xlWB = Nothing
                xlObject = Nothing
                ' force final cleanup!
                GC.Collect()
                GC.WaitForPendingFinalizers()
            End Try
        Catch ex As Exception
            a_sErrorMessage = "Error in export: " & ex.Message
        End Try

        Return bRetVal
    End Function
    Public Function exportDataTableToCsv(ByVal folderPath As String, ByVal dtTable As DataTable)
        'Build the CSV file data as a Comma separated string.
        Dim csv As String = ""

        'Add the Header row for CSV file.
        For Each column As DataColumn In dtTable.Columns
            csv += column.ColumnName & ","c
        Next
        'Add new line.
        csv += vbCr & vbLf
        'Adding the Rows
        Dim c As Integer
        Dim r As Integer
        For r = 0 To dtTable.Rows.Count - 1
            For c = 0 To dtTable.Columns.Count - 1
                csv += dtTable.Rows(r).Item(c) & ","c
            Next
            csv += vbCr & vbLf
        Next
        'Exporting to Excel
        File.WriteAllText(folderPath, csv)
        Return True
    End Function
End Module
