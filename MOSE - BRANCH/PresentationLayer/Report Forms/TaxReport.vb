Public Class TaxReportFrm
#Region "Class Objects"
    Private _objInv As New clsInvoice
    Private _objcmnbLayer As New clsCommon_BL
#End Region
#Region "Local Variables"
    Private dtTable As DataTable
    Private RptdtTable As DataTable
#End Region
#Region "Form Objects"
    Private WithEvents frm As New ReprtviewNEWfrm
    Private WithEvents fRptFormat As RptFormatfrm
    Private WithEvents fwait As WaitMessageFrm
#End Region

    Private Sub ldGrid()
        Dim trtype As String = ""
        Select Case True
            Case rdosales.Checked
                trtype = "IS"
            Case rdopurchase.Checked
                trtype = "IP"
            Case rdosalesreturn.Checked
                trtype = "SR"
            Case rdopurchasereturn.Checked
                trtype = "PR"

        End Select
        Dim iswithgstn As Integer
        Dim isB2b As Integer
        If chkwithgst.Checked And chkwithoutgst.Checked Then
            iswithgstn = 2
        ElseIf chkwithgst.Checked Then
            iswithgstn = 1
        ElseIf chkwithoutgst.Checked Then
            iswithgstn = 0
        Else
            chkwithgst.Checked = True
            chkwithoutgst.Checked = True
            iswithgstn = 2
        End If
        If chkb2c.Checked And chkb2b.Checked Then
            isB2b = 2
        ElseIf chkb2b.Checked Then
            isB2b = 1
        ElseIf chkb2c.Checked Then
            isB2b = 0
        Else
            chkb2b.Checked = True
            chkb2c.Checked = True
            isB2b = 2
        End If
        If rdooutputtax.Checked Then
            'Dim tp As Integer
            'If chkb2b.Checked Then
            '    tp = 1
            'Else
            '    tp = 0
            'End If
            'dtTable = _objInv.returnOutputTax(Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd"), Format(DateValue(cldrEnddate.Value), "yyyy/MM/dd"), tp).Tables(0)
        Else
        End If
        dtTable = _objInv.returnTaxReport(Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd"), Format(DateValue(cldrEnddate.Value), "yyyy/MM/dd"), 1, trtype, iswithgstn, 0, isB2b).Tables(0)
        grdvoucher.DataSource = dtTable
        SetGridHead()
    End Sub

    Private Sub SetGridHead()
        With grdvoucher
            SetGridProperty(grdvoucher)
            .Columns("Inv No").Width = 75
            .Columns("Tr.Date").Width = 75
            .Columns("Customer Name").Width = 200

            If rdosales.Checked Then
                .Columns("Customer Name").HeaderText = "Customer Name"
            Else
                .Columns("Customer Name").HeaderText = "Supplier Name"
            End If

            .Columns("Total Sale").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Total Sale").Width = 100
            .Columns("Total Sale").DefaultCellStyle.Format = "N" & NoOfDecimal
            If rdosales.Checked Then
                .Columns("Total Sale").HeaderText = "Total Sales"
            Else
                .Columns("Total Sale").HeaderText = "Gross Total"
            End If

            .Columns("Discount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Discount").Width = 100
            .Columns("Discount").DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns("Tax Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Tax Amount").Width = 100
            .Columns("Tax Amount").DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns("CGST").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("CGST").Width = 100
            .Columns("CGST").DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns("SGST").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("SGST").Width = 100
            .Columns("SGST").DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns("IGST").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("IGST").Width = 100
            .Columns("IGST").DefaultCellStyle.Format = "N" & NoOfDecimal

            .Columns("CessAmt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("CessAmt").Width = 100
            .Columns("CessAmt").DefaultCellStyle.Format = "N" & NoOfDecimal


            .Columns("Net Sale").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Net Sale").Width = 100
            .Columns("Net Sale").DefaultCellStyle.Format = "N" & NoOfDecimal

            If rdosales.Checked Then
                .Columns("Net Sale").HeaderText = "Net Sales"
            Else
                .Columns("Net Sale").HeaderText = "Net Total"
            End If

            .Columns("Sales Cost").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Sales Cost").Width = 100
            .Columns("Sales Cost").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Sales Cost").Visible = False


            If rdosales.Checked Then
                .Columns("Sales Cost").HeaderText = "Sales Cost"
            Else
                .Columns("Sales Cost").HeaderText = "Other Cost"
            End If
            .Columns("TrId").Visible = False
            .Columns("LPO").Visible = False
            .Columns("DateFrom").Visible = False
            .Columns("DateTo").Visible = False
            .Columns("lnk").Visible = False
            'If Not rdooutputtax.Checked Then

            'Else
            '    setGridHeadOutputTax()

            'End If
            
            '.Columns("Tr. Description").Visible = False
        End With
        setComboGrid(grdvoucher)
    End Sub
    Private Sub setGridHeadOutputTax()
        With grdvoucher
            If chkb2c.Checked Then
                .Columns("trdate").Width = 75
                .Columns("trdate").HeaderText = "Bill Date"

                .Columns("billamount").Width = 100
                .Columns("billamount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("billamount").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("billamount").HeaderText = "Bill Amount"

                .Columns("tbamt0").Width = 75
                .Columns("tbamt0").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("tbamt0").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("tbamt0").HeaderText = "0%"

                .Columns("taxamt0").Width = 100
                .Columns("taxamt0").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("taxamt0").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("taxamt0").HeaderText = "Tax Amount"

                .Columns("tbamt5").Width = 75
                .Columns("tbamt5").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("tbamt5").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("tbamt5").HeaderText = "5%"

                .Columns("taxamt5").Width = 100
                .Columns("taxamt5").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("taxamt5").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("taxamt5").HeaderText = "Tax Amount"

                .Columns("tbamt12").Width = 75
                .Columns("tbamt12").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("tbamt12").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("tbamt12").HeaderText = "12%"

                .Columns("taxamt12").Width = 100
                .Columns("taxamt12").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("taxamt12").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("taxamt12").HeaderText = "Tax Amount"

                .Columns("tbamt18").Width = 75
                .Columns("tbamt18").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("tbamt18").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("tbamt18").HeaderText = "18%"

                .Columns("taxamt18").Width = 100
                .Columns("taxamt18").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("taxamt18").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("taxamt18").HeaderText = "Tax Amount"

                .Columns("tbamt28").Width = 75
                .Columns("tbamt28").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("tbamt28").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("tbamt28").HeaderText = "28%"

                .Columns("taxamt28").Width = 100
                .Columns("taxamt28").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("taxamt28").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("taxamt28").HeaderText = "Tax Amount"

                .Columns("taxamt").Width = 100
                .Columns("taxamt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("taxamt").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("taxamt").HeaderText = "Total Tax"

                .Columns("FloodcessAmt").Width = 100
                .Columns("FloodcessAmt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("FloodcessAmt").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("FloodcessAmt").HeaderText = "Flood Cess"

                .Columns("Billno").Width = 150
            Else
                .Columns("invno").Width = 75
                .Columns("invno").HeaderText = "Bill No"
                .Columns("GSTIN").Width = 75
                .Columns("Dealername").Width = 150
                .Columns("trdate").Width = 75
                .Columns("trdate").HeaderText = "Bill Date"

                .Columns("BillAmt").Width = 100
                .Columns("BillAmt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("BillAmt").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("BillAmt").HeaderText = "Bill Amount"

                .Columns("igst").Width = 75
                .Columns("igst").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("igst").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("igst").HeaderText = "GST%"

                .Columns("taxableamt").Width = 150
                .Columns("taxableamt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("taxableamt").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("taxableamt").HeaderText = "Taxable Amount"

                .Columns("CGSTAMT").Width = 100
                .Columns("CGSTAMT").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("CGSTAMT").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("CGSTAMT").HeaderText = "CGST"

                .Columns("SGSTAmt").Width = 100
                .Columns("SGSTAmt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("SGSTAmt").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("SGSTAmt").HeaderText = "SGST"
                resizeGridColumn(grdvoucher, 2)
            End If
            .Columns("DateFrom").Visible = False
            .Columns("DateTo").Visible = False
            .Columns("lnk").Visible = False
        End With
       
    End Sub
    Private Sub setComboGrid(ByVal grd As DataGridView)
        cmbcolms.Items.Clear()
        Dim i As Integer = 0
        With grd
            For i = 0 To grd.ColumnCount - 1
                cmbcolms.Items.Add(.Columns(i).HeaderText)
            Next
            If cmbcolms.Items.Count > 0 Then cmbcolms.SelectedIndex = 1
        End With
    End Sub
    Private Sub setColwidth()
        If grdvoucher.Columns.Count = 0 Then Exit Sub
        Dim colwidth As Integer
        Dim colwidth1 As Integer
        Dim i As Integer
        For i = 4 To grdvoucher.ColumnCount - 1
            If grdvoucher.Columns(i).Visible = True Then
                colwidth = colwidth + grdvoucher.Columns(i).Width
            End If
        Next
        For i = 0 To 2
            If grdvoucher.Columns(i).Visible = True Then
                colwidth1 = colwidth1 + grdvoucher.Columns(i).Width
            End If
        Next
        colwidth = colwidth + colwidth1
        grdvoucher.Columns("Customer Name").Width = grdvoucher.Width - colwidth - 130
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        'setColwidth()
    End Sub


    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        loadWaite(1)
    End Sub

    Private Sub txtSeq_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeq.TextChanged
        grdvoucher.DataSource = SearchGrid(dtTable, Trim(txtSeq.Text), cmbcolms.SelectedIndex, Not chkSearch.Checked)
        RptdtTable = SearchGrid(dtTable, Trim(txtSeq.Text), cmbcolms.SelectedIndex, Not chkSearch.Checked)
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Dim RptType As String = ""
        Dim RptCaption As String = ""
        If rdooutputtax.Checked Then
            If chkb2b.Checked Then
                RptType = "OPTB"
            Else
                RptType = "OPTC"
            End If
        Else
            If rdoInvoicewise.Checked Then
                RptType = "TXS"
            ElseIf rdohsncode.Checked Then
                RptType = "TXH"
            ElseIf rdoItemwise.Checked Then
                RptType = "TXI"
            ElseIf rdopercentagewise.Checked Then
                RptType = "TXP"
            End If
        End If

        If chkFormat.Checked Then
            fRptFormat = New RptFormatfrm
            fRptFormat.RptType = RptType
            fRptFormat.ShowDialog()
            fRptFormat = Nothing
        Else
            Dim RptName As String
            RptName = getRptDefFlName(RptType, RptCaption)
            PrepareReport(RptName, RptCaption, False)
        End If

    End Sub

    Private Sub frm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles frm.FormClosed
        frm = Nothing
    End Sub

    Private Sub fRptFormat_PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean) Handles fRptFormat.PrvPrnRpt
        PrepareReport(RptFlName, RptCaption, forPrint)
    End Sub
    Public Sub PrepareReport(ByVal FileName As String, ByVal RptCaption As String, ByVal forpint As Boolean)
        Dim ds As New DataSet
        If Not frm Is Nothing Then
            frm.Close()
            frm = New ReprtviewNEWfrm
        Else
            frm = New ReprtviewNEWfrm
        End If
        Dim tp As Integer
        Dim trtype As String = ""
        Select Case True
            Case rdosales.Checked
                trtype = "IS"
            Case rdopurchase.Checked
                trtype = "IP"
            Case rdopurchasereturn.Checked
                trtype = "PR"
            Case rdosalesreturn.Checked
                trtype = "SR"
            Case rdooutputtax.Checked
                If chkb2b.Checked Then
                    tp = 1
                Else
                    tp = 0
                End If
                ds = _objInv.returnOutputTax(Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd"), Format(DateValue(cldrEnddate.Value), "yyyy/MM/dd"), tp)
                GoTo setrpt
        End Select
        Dim iswithgstn As Integer
        Dim isB2b As Integer
        If chkwithgst.Checked And chkwithoutgst.Checked Then
            iswithgstn = 2
        ElseIf chkwithgst.Checked Then
            iswithgstn = 1
        ElseIf chkwithoutgst.Checked Then
            iswithgstn = 0
        Else
            iswithgstn = 2
            chkwithgst.Checked = True
            chkwithoutgst.Checked = True
        End If
        If chkb2c.Checked And chkb2b.Checked Then
            isB2b = 2
        ElseIf chkb2b.Checked Then
            isB2b = 1
        ElseIf chkb2c.Checked Then
            isB2b = 0
        Else
            chkb2b.Checked = True
            chkb2c.Checked = True
            isB2b = 2
        End If
        Select Case True
            Case rdoInvoicewise.Checked
                If trtype = "" Then
                    tp = 2
                Else
                    tp = 1
                End If
                If RptdtTable Is Nothing Then
                    ds = _objInv.returnTaxReport(Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd"), Format(DateValue(cldrEnddate.Value), "yyyy/MM/dd"), tp, trtype, iswithgstn, 0, isB2b)
                Else
                    ds.Tables.Add(RptdtTable)
                End If
                RptdtTable = Nothing
            Case rdohsncode.Checked
                If trtype = "" Then
                    tp = 4
                Else
                    tp = 3
                End If
                ds = _objInv.returnTaxReport(Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd"), Format(DateValue(cldrEnddate.Value), "yyyy/MM/dd"), tp, trtype, iswithgstn, 0, isB2b)
            Case rdoItemwise.Checked
                If trtype = "" Then
                    tp = 8
                    Exit Sub
                Else
                    tp = 5
                End If
                ds = _objInv.returnTaxReport(Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd"), Format(DateValue(cldrEnddate.Value), "yyyy/MM/dd"), tp, trtype, iswithgstn, 0, isB2b)
            Case rdopercentagewise.Checked
                If trtype = "" Then
                    tp = 8
                    Exit Sub
                Else
                    If cmbgstslab.Text = "" Then
                        tp = 6
                    Else
                        tp = 7
                    End If
                End If
                ds = _objInv.returnTaxReport(Format(DateValue(cldrStartDate.Value), "yyyy/MM/dd"), Format(DateValue(cldrEnddate.Value), "yyyy/MM/dd"), tp, trtype, iswithgstn, Val(cmbgstslab.Text), isB2b)
        End Select
setrpt:
        frm.SetReport(ds, FileName, 0, False)
        frm.MdiParent = Me.MdiParent
        frm.Text = "Tax Report " & RptCaption
        frm.Show()
    End Sub

    Private Sub TaxReportFrm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cldrStartDate.Value = Format(DateAdd(DateInterval.Day, firstDateFromToday * -1, DateValue(Date.Now)), DtFormat)
        loadWaite(1)
        loadGSTSlab()
        chkb2b.Checked = True
        chkb2c.Checked = True
        chkwithgst.Checked = True
        chkwithoutgst.Checked = True
        'Timer1.Enabled = True
    End Sub

    Private Sub rdoboth_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoboth.Click
        btnLoad.Enabled = False
        For Each ctrl In Me.Controls
            If TypeOf (ctrl) Is RadioButton Then
                ctrl.checked = False
            End If
        Next
    End Sub

    Private Sub rdosales_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdosales.Click, rdopurchase.Click, rdooutputtax.Click
        btnLoad.Enabled = True
        Dim myctrl As RadioButton
        myctrl = sender
        
        If myctrl.Name = "rdooutputtax" Then
            cmbcolms.Visible = False
            txtSeq.Visible = False
            chkSearch.Visible = False
            chkb2b.Checked = False 
            chkb2c.Checked = True
            GroupBox1.Enabled = False
            btnLoad.Enabled = False
            For Each ctrl In Me.Controls
                If TypeOf (ctrl) Is RadioButton Then
                    ctrl.checked = False
                End If
            Next
            chkwithgst.Enabled = False
            chkwithoutgst.Enabled = False
        ElseIf myctrl.Name = "rdosales" Then
            GroupBox1.Enabled = True
            For Each ctrl In Me.GroupBox3.Controls
                If TypeOf (ctrl) Is RadioButton Then
                    ctrl.checked = False
                End If
            Next
            chkwithgst.Enabled = True
            chkwithoutgst.Enabled = True
        Else
            cmbcolms.Visible = True
            txtSeq.Visible = True
            chkSearch.Visible = True
            chkwithgst.Enabled = True
            chkwithoutgst.Enabled = True
            For Each ctrl In Me.GroupBox3.Controls
                If TypeOf (ctrl) Is RadioButton Then
                    ctrl.checked = False
                End If
            Next
        End If
    End Sub
    Private Sub loadGSTSlab()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select gst from GstDefaultSetTb")
        If dt.Rows.Count > 0 Then
            cmbgstslab.Items.Clear()
            Dim i As Integer
            cmbgstslab.Items.Add(0)
            For i = 0 To dt.Rows.Count - 1
                cmbgstslab.Items.Add(dt(i)("gst"))
            Next
        End If
    End Sub

    Private Sub rdopercentagewise_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdopercentagewise.CheckedChanged
        cmbgstslab.Visible = rdopercentagewise.Checked
    End Sub


    Private Sub chkb2b_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkb2b.Click
        If rdooutputtax.Checked Then
            chkb2c.Checked = False
        End If
    End Sub
    Private Sub chkb2c_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkb2c.Click
        If rdooutputtax.Checked Then
            chkb2b.Checked = False
        End If
    End Sub
    Private Sub fwait_triggerEvent() Handles fwait.triggerEvent
        Select Case fwait.triggerType
            Case 1
                ldGrid()

        End Select
        If Not fwait Is Nothing Then
            fwait.Close()
            fwait = Nothing
        End If

    End Sub
    Private Sub loadWaite(ByVal triggerType As Integer)
        If Not fwait Is Nothing Then fwait.Close() : fwait = Nothing
        fwait = New WaitMessageFrm
        With fwait
            .triggerType = triggerType
            .Show()
        End With
    End Sub
End Class