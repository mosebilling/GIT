Imports System.Data.OleDb
Imports Microsoft.VisualBasic.FileIO

Public Class TransferiItemsFromExcel
    'object declarations
    Private _objcmnbLayer As New clsCommon_BL
    Dim _objItmMast As New clsItemMast_BL
    Private conn As OleDbConnection
    Private dta As OleDbDataAdapter
    Private _objTr As New clsAccountTransaction
    Private _objInv As New clsInvoice
    Private dts As DataSet
    Public dtMose As DataTable
    Private lstTable As DataTable
    Private _objGst As New clsGSTMaster
    Private tp As Integer
    Delegate Sub GenericDelegate(ByVal Mname As String, ByVal RecName As String, ByVal rec As Integer, ByVal count As Integer)
    Public Event loadtoPurchase(ByVal dt As DataTable)
    Public isFromPurchase As Boolean
    Public isPosTransfer As Boolean
    Private dtitemtransaction As DataTable
    Private dtTax As DataTable
    Private dtMultipleDebits As DataTable
    Private dtSetoffTable As DataTable
    Private salesman As String
    Dim locationcode As String

    Private Structure JobAccTp
        Dim Amt As Double
        Dim Job As String
        Dim Acc As Long
    End Structure
    Private JobAcc() As JobAccTp


    Private Sub btnbrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnbrowse.Click
        loadFromFile(False)
    End Sub
    Private Sub loadFromFile(ByVal skipfolderopen As Boolean)
        Try
            If Not skipfolderopen Then
                opfSelectFile.Title = "Please Select a File"
                opfSelectFile.InitialDirectory = "c:"
                opfSelectFile.ShowDialog()
            End If
            If txtpath.Text = "" Then Exit Sub
            If chkmdb.Checked = False Then
                conn = New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + txtpath.Text + ";Extended Properties=Excel 12.0;")
                dta = New OleDbDataAdapter("Select * From [Sheet1$]", conn)
                dts = New DataSet
                dta.Fill(dts, "[Sheet1$]")
                lstexcel.Items.Clear()
                For Each column As DataColumn In dts.Tables(0).Columns
                    lstexcel.Items.Add(column.ColumnName)
                Next
            Else
                refreshMechineData()
            End If
            plformat.Visible = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        End Try

    End Sub
    Private Sub refreshMechineData()
        Dim dt As DataTable
        If rdosales.Checked Then
            dt = _objcmnbLayer._fldDatatable("Select max(billingmechineid) billingmechineid from ItmInvCmnTb where trtype='IS' AND billingmechinenumber='01'")
        Else
            dt = _objcmnbLayer._fldDatatable("Select max(mechinervnumber) billingmechineid from AccTrCmn where jvtype='RV' AND mechinenumber='01'")
        End If
        Dim billid As Long
        If dt.Rows.Count > 0 Then
            billid = Val(dt(0)(0) & "")
        End If
        dt = copyFromMDB(billid) 'copyFromCSV()
        lstexcel.Items.Clear()
        For Each column As DataColumn In dt.Columns
            lstexcel.Items.Add(column.ColumnName)
        Next
    End Sub

    Private Function copyFromMDB(ByVal billingmechineid As Long) As DataTable
        Try

            Dim cstring As String = "Provider=Microsoft.Jet.OLEDB.4.0;Jet OLEDB:Database Password=sil123;Data Source=" + txtpath.Text + ";persist security info=false"
            Dim dt As DataTable
            If rdosales.Checked Then
                dt = _objcmnbLayer._fldDatatable("Select trid from ItmInvCmnTb where trtype='IS' AND billingmechinenumber='01' and trdate>='" & Format(DateValue(cldrstart.Value), "yyyy/MM/dd") & "' AND trdate<='" & Format(DateValue(clrend.Value), "yyyy/MM/dd") & "'")
                If dt.Rows.Count > 0 Then
                    dta = New OleDbDataAdapter("Select * From BILL_DET left join CREDITORS on BILL_DET.CredCode=CREDITORS.CustCode where bill_id>" & _
                       billingmechineid & " and Palm_ID='01' and " & _
                       "CollectionDate>=#" & Format(DateValue(cldrstart.Value), "dd/MMM/yyyy") & "# and CollectionDate<=#" & Format(DateValue(clrend.Value), "dd/MMM/yyyy") & "# order by billno", cstring)
                Else
                    dta = New OleDbDataAdapter("Select * From BILL_DET left join CREDITORS on BILL_DET.CredCode=CREDITORS.CustCode where" & _
                       " Palm_ID='01' and " & _
                       "CollectionDate>=#" & Format(DateValue(cldrstart.Value), "dd/MMM/yyyy") & "# and CollectionDate<=#" & Format(DateValue(clrend.Value), "dd/MMM/yyyy") & "# order by billno", cstring)
                End If
            Else
                dt = _objcmnbLayer._fldDatatable("Select linkno from AccTrCmn where jvtype='RV' AND mechinenumber='01' and jvdate>='" & Format(DateValue(cldrstart.Value), "yyyy/MM/dd") & "' AND jvdate<='" & Format(DateValue(clrend.Value), "yyyy/MM/dd") & "'")
                If dt.Rows.Count > 0 Then
                    dta = New OleDbDataAdapter("Select * From CREDPAYMENT where status='A' AND billno>" & _
                                           billingmechineid & " and Palm_ID='01' and " & _
                                           "Date>=#" & Format(DateValue(cldrstart.Value), "dd/MMM/yyyy") & "# and Date<=#" & Format(DateValue(clrend.Value), "dd/MMM/yyyy") & "# order by date,billno ", cstring)
                Else
                    dta = New OleDbDataAdapter("Select * From CREDPAYMENT where status='A' " & _
                       " and Palm_ID='01' and " & _
                       "Date>=#" & Format(DateValue(cldrstart.Value), "dd/MMM/yyyy") & "# and Date<=#" & Format(DateValue(clrend.Value), "dd/MMM/yyyy") & "# order by date,billno ", cstring)
                End If
            End If

            dts = New DataSet
            dta.Fill(dts)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        'conn = New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + txtpath.Text + ";Jet OLDB:Database Password=sil123;")

        Return dts.Tables(0)
    End Function
    Private Function copyFromCSV() As DataTable
        Using myReader As New TextFieldParser(txtpath.Text)
            myReader.TextFieldType = FieldType.Delimited
            myReader.SetDelimiters(",")
            myReader.HasFieldsEnclosedInQuotes = True
            Dim dt As New DataTable
            Dim i As Integer
            Dim dr As DataRow
            Do While Not myReader.EndOfData
                Dim myData() As String = myReader.ReadFields
                If dt.Columns.Count = 0 Then
                    For i = 0 To myData.Length - 1
                        dt.Columns.Add(New DataColumn("c" & i + 1, GetType(String)))
                    Next
                End If
                dr = dt.NewRow
                For i = 0 To myData.Length - 1
                    dr(i) = myData(i)
                Next
                dt.Rows.Add(dr)
            Loop
            Return dt
        End Using
    End Function
    Private Sub ldMoseColumn()
        Select Case tp
            Case 0 'PDC
                dtMose = _objcmnbLayer._fldDatatable("SELECT ChqNo,ChqDate,BankCode,DealAmt,'' Party,Reference,ChqDate TrDate,EntryRef from AccTrDet")
            Case 1 'ITEM
                dtMose = _objcmnbLayer._fldDatatable("SELECT '' [Item Code],'' [Item Name],'' as Unit,0.00  OpQty,0.00 OpCost,0.00 UnitPrice,0.00 UnitPriceWS,'' itemCategory,'' HSNCode,0 [GST%],'' MRP,'' shortDescr,'' longDescr,'' webname,0.00 SECONDPRICE,0.00 Cess,0.00 KLFC,'' GrpName,'' Location from invitm")
            Case 2 'CASH CUSTOMER
                dtMose = _objcmnbLayer._fldDatatable("SELECT  CustName,Add1,Add2,Add3,Phone,email,gender from CashCustomerTb where 1=2")
            Case 3 'PURCHASE
                dtMose = _objcmnbLayer._fldDatatable("SELECT  '' [Item Code],'' [Item Name],'' Unit,'' HSNCODE, 0.00 [GST%],0 QTY, 0.00 [Purchase Cost],0.00 [Sales Price],0.00 MRP,0.00 TotalItemDiscount,0.00 ItemUnitDiscount,0 Iscess  from INVITM where 1=2")
            Case 4 'SUPPLIER
                dtMose = _objcmnbLayer._fldDatatable("SELECT  '' Code,'' [Account Name],'' Add1,'' Add2,'' Add3,'' Phone,'' email, '' [State code],'' GSTN, '' [Contact Name],0.00 [Opn Bal],0.00 CreditLimit,0.00 DueDays from AccMast where 1=2")
            Case 5 'CUSTOMER
                dtMose = _objcmnbLayer._fldDatatable("SELECT '' Code, '' [Account Name],'' Add1,'' Add2,'' Add3,'' Phone,'' email, '' [State code],'' GSTN, '' [Contact Name],0.00 [Opn Bal],0.00 CreditLimit,0.00 DueDays from AccMast where 1=2")
            Case 6 'Sales From POS Mechine
                dtMose = _objcmnbLayer._fldDatatable("SELECT  '' Trdate,'' InvNo,'' RefNo,'' isB2B,'' CustCode,'' CustName,'' Address,'' phone,'' GSTNum," & _
                                                     " '' [Item Code],'' [Item Name],'' Unit,'' HSNCODE, 0.00 [GST%],0.00 [Sales Price],0.00 MRP,0 Iscess,0 QTY, 0.00 [Item Dis],0.00 BillDiscount," & _
                                                     "0.00 RoundOff,0.00 BillAmount,0.00 [CGST%],0.00 [SGST%],0.00 [IGST%],'' status,'' GSTN,0 BillId,0 Palm_ID,0.00 KFC  from INVITM where 1=2")
            Case 7 'collection From POS Mechine
                dtMose = _objcmnbLayer._fldDatatable("SELECT  '' Trdate,'' InvNo,'' CustCode," & _
                                                    " 0.00 [PaidAmount],'' PayMode,'' BankCode,'' ChequeNO,'' Chequedate,'' CreditCardNo,'' Palm_ID" & _
                                                    "  from accmast where 1=2")
        End Select

        dtMose.Rows.Clear()
        lstmose.Items.Clear()
        For Each column As DataColumn In dtMose.Columns
            lstmose.Items.Add(column.ColumnName)
        Next
    End Sub
    Private Sub opfSelectFile_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles opfSelectFile.FileOk
        txtpath.Text = opfSelectFile.FileName
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnok.Click
        If MsgBox("Transfering data from excel cannot be undo. Please take backup before continue" & vbCrLf & "Proceed?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        If txtpath.Text = "" Then
            MsgBox("Invalid File", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If Not FileExists(txtpath.Text) Then
            MsgBox("File not found", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If rdosalesTransferFromMchn.Checked Then refreshMechineData()
        salesman = cmbsalesman.Text

        addtoLstTable()
        Worker.RunWorkerAsync()
    End Sub
    Private Sub addtoLstTable()
        Dim i As Integer
        Dim dtr As DataRow
        lstTable.Rows.Clear()
        For i = 0 To lstvw.Items.Count - 1
            dtr = lstTable.NewRow
            dtr(0) = lstvw.Items(i).SubItems(0).Text
            dtr(1) = lstvw.Items(i).SubItems(1).Text
            lstTable.Rows.Add(dtr)
        Next
    End Sub
    Private Sub createLstTable()
        lstTable = New DataTable
        lstTable.Columns.Add(New DataColumn("C1", GetType(String)))
        lstTable.Columns.Add(New DataColumn("C2", GetType(String)))
    End Sub

    Private Sub Worker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles Worker.DoWork
        setExternalData()
        If Val(btnok.Tag) > 0 Then
            Exit Sub
        End If
        If rdoitemlistExcel.Checked Then
            Dim _qry = From job In dtMose.AsEnumerable() Where Trim(job("item code") & "") <> "" Select job
            If _qry.Count > 0 Then
                dtMose = _qry.CopyToDataTable
            End If
            updateItemdetails()
        ElseIf rdocashcustomerEx.Checked Then
            updateCashCustomerList()
        ElseIf rdopurchase.Checked Then
            checkAndCreateItemList()
        ElseIf rdosupplier.Checked Or rdocreditcustomer.Checked Then
            createCustomerSupplier()
        ElseIf rdosalesTransferFromMchn.Checked Then
            If rdosales.Checked Then
                transferBulkSalesInvoices(0)
            Else
                UpdateCollection()
            End If

        End If
    End Sub
    Private Function createSingleAccountHead(ByVal i As Integer, ByVal GrpSetOn As String) As Integer
        Dim dt As DataTable
        Dim dt1 As DataTable
        Dim gname As String = ""
        Dim gId As Integer
        dt1 = _objcmnbLayer._fldDatatable("SELECT Descr, S1AccId FROM S1AccHd WHERE GrpSetOn In ('" & GrpSetOn & "') ORDER BY Descr")
        If dt1.Rows.Count > 0 Then
            gname = dt1(0)("Descr")
            gId = dt1(0)("S1AccId")
        End If
        Dim accountNo As Integer
        Dim Acode As String
        Dim accid As Long
        dt = _objcmnbLayer._fldDatatable("SELECT Alias,AccDescr,AccountNo FROM AccMast Where [Alias]='" & Trim(dtMose(i)("custcode") & "") & "' or AccDescr ='" & Trim(dtMose(i)("custName") & "") & "'")
        'Dim _qry = From job In dt.AsEnumerable() Where job![Alias] = Trim(dtMose(i)("custcode") & "") Or job!AccDescr = Trim(dtMose(i)("custName") & "") Select New With _
        '                {.Name = job!AccountNo}
        If dt.Rows.Count = 0 Then
            Acode = GenerateNext(accountNo, gname, gId)
            If Trim(dtMose(i)("custcode")) <> "" Then
                Acode = Trim(dtMose(i)("custcode"))
            End If
            'Trim(stateCode & "")
            _objcmnbLayer._saveDatawithOutParm("INSERT INTO AccMast (AccountNo, Alias, AccDescr, S1AccId,OpnBal,isTaxRegistered,CountryCode) VALUES (" & _
                                               Val(accountNo) & ", '" & MkDbSrchStr(Trim(Acode)) & "', '" & _
                                               MkDbSrchStr(Trim(dtMose(i)("custName") & "")) & "', " & gId & ",0 " & _
                                               "," & IIf(Trim(dtMose(i)("GSTN") & "") <> "", 1, 0) & ",'" & Trim(stateCode & "") & "')")
            dt1 = _objcmnbLayer._fldDatatable("SELECT AccId FROM AccMast WHERE AccountNo=" & Val(accountNo))
            If dt1.Rows.Count > 0 Then
                accid = dt1(0)("AccId")
            End If
            _objcmnbLayer._saveDatawithOutParm("INSERT INTO AccMastAddr (AccountNo,Address1,Address2,Address3,Phone,ContactName,EMail," & _
                                                              "GSTIN) VALUES(" & _
                                                                 Val(accid) & ",'" & MkDbSrchStr(Trim(dtMose(i)("address") & "")) & "','','','" & MkDbSrchStr(Trim(dtMose(i)("phone") & "")) & "','','','')")
        Else
            dt1 = _objcmnbLayer._fldDatatable("SELECT AccId FROM AccMast WHERE AccountNo=" & Val(dt(0)("accountNo")))
            If dt1.Rows.Count > 0 Then
                accid = dt1(0)("AccId")
            End If
        End If
        Return accid
    End Function
    Private Function updateSingleCashCustomer(ByVal i As Integer) As Integer
        Dim dt = _objcmnbLayer._fldDatatable("select custid from CashCustomerTb where custcode='" & dtMose(i)("custcode") & "'")
        If dt.Rows.Count = 0 Then
            _objcmnbLayer._saveDatawithOutParm("Insert into CashCustomerTb(custcode,CustName,Add1,Add2,Add3,Phone,email,gender) values(" & _
                                             "'" & dtMose(i)("custcode") & "'," & _
                                             "'" & dtMose(i)("CustName") & "'," & _
                                             "''," & _
                                             "''," & _
                                             "''," & _
                                             "''," & _
                                             "''," & _
                                             "''" & _
                                             ")")
            dt = _objcmnbLayer._fldDatatable("select custid from CashCustomerTb where custcode='" & dtMose(i)("custcode") & "'")
            If dt.Rows.Count > 0 Then
                Return dt(0)(0)
            End If
        Else
            Return dt(0)(0)
        End If

    End Function
    Private Sub calculateINV(ByRef TDrAmt As Double, Optional ByVal BillDiscount As Double = 0, Optional ByVal discACC As Integer = 0)
        Dim billAmount As Double
        Dim i As Integer
        For i = 0 To dtitemtransaction.Rows.Count - 1
            dtitemtransaction(i)("itemtotal") = CDbl(dtitemtransaction(i)("Trqty")) * CDbl(dtitemtransaction(i)("UnitCost"))
        Next
        billAmount = Convert.ToDouble(dtitemtransaction.Compute("SUM(itemtotal)", ""))
        Dim actualprice As Double
        Dim actualdiscount As Double

        For i = 0 To dtitemtransaction.Rows.Count - 1
            actualdiscount = (BillDiscount * CDbl(dtitemtransaction(i)("UnitCost"))) / billAmount
            dtitemtransaction(i)("UnitDiscount") = actualdiscount
            actualdiscount = actualdiscount * CDbl(dtitemtransaction(i)("Trqty"))
            If Val(dtitemtransaction(i)("ItemDiscount") & "") = 0 Then dtitemtransaction(i)("ItemDiscount") = 0
            actualdiscount = actualdiscount + CDbl(dtitemtransaction(i)("ItemDiscount"))
            actualprice = CDbl(dtitemtransaction(i)("UnitCost")) * CDbl(dtitemtransaction(i)("Trqty"))
            actualprice = actualprice - actualdiscount
            TDrAmt = TDrAmt + (CDbl(dtitemtransaction(i)("Trqty")) * CDbl(dtitemtransaction(i)("UnitCost")))
            TDrAmt = TDrAmt - CDbl(dtitemtransaction(i)("ItemDiscount"))
            dtitemtransaction(i)("CGSTAMT") = Format((actualprice * CDbl(dtitemtransaction(i)("CSGTP"))) / 100, numFormat)
            dtitemtransaction(i)("SGSTAmt") = Format((actualprice * CDbl(dtitemtransaction(i)("SGSTP"))) / 100, numFormat)
            dtitemtransaction(i)("IGSTAmt") = (actualprice * CDbl(dtitemtransaction(i)("IGSTP"))) / 100
            dtitemtransaction(i)("taxAmt") = Format(CDbl(dtitemtransaction(i)("CGSTAMT")) + CDbl(dtitemtransaction(i)("SGSTAmt")), numFormat)
            'actualprice = CDbl(dtitemtransaction(i)("Trqty")) * (CDbl(dtitemtransaction(i)("UnitCost")) - CDbl(dtitemtransaction(i)("ItemDiscount")))
            dtitemtransaction(i)("linetotal") = actualprice + CDbl(dtitemtransaction(i)("taxAmt")) + CDbl(dtitemtransaction(i)("CessAmt")) '- CDbl(dtitemtransaction(i)("UnitDiscount"))
        Next

    End Sub
    Private Sub CalculateGST(Optional ByVal isAddcess As Boolean = False, Optional ByVal statecode As Integer = 0)
        Dim i As Integer
        Dim dtHSN As DataTable
        Dim dtrow As DataRow
        Dim slno As Integer

        If dtTax Is Nothing Then Exit Sub
        If dtTax.Rows.Count > 0 Then
            dtTax.Rows.Clear()
        End If
        If isAddcess Then
            If enablecess Or enableFloodCess Then
                Dim dt As DataTable = _objcmnbLayer._fldDatatable("SELECT vatcode,convert(money, 0) Amount,collectionAC,vat,AccDescr From VatMasterTb Left join accmast on VatMasterTb.collectionAC=AccMast.accid", False)
                For i = 0 To dt.Rows.Count - 1
                    dtrow = dtTax.NewRow
                    dtrow("slno") = dtTax.Rows.Count + 1
                    dtrow("AccountName") = dt(0)("AccDescr")
                    dtrow("ACid") = dt(0)("collectionAC")
                    dtrow("Amount") = 0
                    dtTax.Rows.Add(dtrow)
                Next
            End If
        End If
        Dim _qurey As EnumerableRowCollection(Of DataRow)
        For i = 0 To dtitemtransaction.Rows.Count - 1
            slno = 0
            _qurey = From data In dtGST.AsEnumerable() Where data("HSNCode") = Trim(dtitemtransaction(i)("HSNCode") & "") Select data
            If _qurey.Count > 0 Then
                dtHSN = _qurey.CopyToDataTable
                If statecode = 0 Then
                    'adding CSGT Amount****
                    Dim a = (From data In dtTax.AsEnumerable() Where data("ACid") = dtHSN(0)("CGSTCAc") Select data("slno"))
                    For Each itm In a
                        Try
                            slno = itm
                        Catch ex As Exception
                            MsgBox(ex.Message)
                        End Try
                    Next
                    If slno = 0 Then
                        dtrow = dtTax.NewRow
                        dtrow("slno") = dtTax.Rows.Count + 1
                        dtrow("AccountName") = dtHSN(0)("CGSTCAname")
                        dtrow("ACid") = dtHSN(0)("CGSTCAc")
                        dtrow("Amount") = CDbl(dtitemtransaction(i)("CGSTAMT"))
                        dtTax.Rows.Add(dtrow)
                    Else
                        dtTax(slno - 1)("Amount") = CDbl(dtTax(slno - 1)("Amount")) + CDbl(dtitemtransaction(i)("CGSTAMT"))
                    End If
                    'adding SSGT Amount****
                    a = (From data In dtTax.AsEnumerable() Where data("ACid") = dtHSN(0)("SGSTCAc") Select data("slno"))
                    slno = 0
                    For Each itm In a
                        Try
                            slno = itm
                        Catch ex As Exception
                            MsgBox(ex.Message)
                        End Try

                    Next
                    If slno = 0 Then
                        dtrow = dtTax.NewRow
                        dtrow("slno") = dtTax.Rows.Count + 1
                        dtrow("AccountName") = dtHSN(0)("SGSTCAname")
                        dtrow("ACid") = dtHSN(0)("SGSTCAc")
                        dtrow("Amount") = CDbl(dtitemtransaction(i)("SGSTAmt"))
                        dtTax.Rows.Add(dtrow)
                    Else
                        dtTax(slno - 1)("Amount") = CDbl(dtTax(slno - 1)("Amount")) + CDbl(dtitemtransaction(i)("SGSTAmt"))
                    End If
                Else
                    'adding ISGT Amount****
                    Dim a = (From data In dtTax.AsEnumerable() Where data("ACid") = dtHSN(0)("IGSTCAc") Select data("slno"))
                    slno = 0
                    For Each itm In a
                        slno = itm
                    Next
                    If slno = 0 Then
                        dtrow = dtTax.NewRow
                        dtrow("slno") = dtTax.Rows.Count + 1
                        dtrow("AccountName") = dtHSN(0)("IGSTCAname")
                        dtrow("ACid") = dtHSN(0)("IGSTCAc")
                        dtrow("Amount") = CDbl(dtitemtransaction(i)("IGSTAmt"))
                        dtTax.Rows.Add(dtrow)
                    Else
                        dtTax(slno - 1)("Amount") = CDbl(dtTax(slno - 1)("Amount")) + CDbl(dtitemtransaction(i)("IGSTAmt"))
                    End If
                End If

            End If
            If enablecess Or enableFloodCess Then
                Dim b = (From data In dtTax.AsEnumerable() Where data("ACid") = Val(dtitemtransaction(i)("cessacc") & "") Select data("slno"))
                slno = 0
                For Each itm In b
                    slno = itm
                Next
                If slno > 0 Then
                    dtTax(slno - 1)("Amount") = CDbl(dtTax(slno - 1)("Amount")) + CDbl(dtitemtransaction(i)("CessAmt"))
                End If
            End If

        Next

    End Sub
    Private Sub transferBulkSalesInvoices(ByVal iscredit As Integer)
        Dim debitAcc, CreditAcc As Integer
        dtitemtransaction = _objcmnbLayer._fldDatatable("Select 0 dtTrId,0 ItemId,'' unit,0.00 TrQty,0.00 UnitCost," & _
                                                "0.00 taxP,0.00 taxAmt,0 PFraction,0.00 taxAmt,0.00 UnitOthCost,'' Method,0.00 UnitDiscount," & _
                                                "0.00 ItemDiscount,0.00 DisP,'' IDescription,0 SlNo,'' HSNCode,0.00 IGSTP,0.00 IGSTAmt," & _
                                                "0.00 CSGTP,0.00 CGSTAMT,0.00 SGSTP,0.00 SGSTAmt,0.00 CessAmt,0 cessacc,0.00 itemtotal,0.00 lineTotal" & _
                                                " from itminvtrtb WHERE trid=0")

        Dim i As Integer
        Dim cnt As Integer = dtMose.Rows.Count - 1

        If dtMose.Rows.Count = 0 Then
            MsgBox("Record not found", MsgBoxStyle.Exclamation)
            btnok.Tag = 1
            Exit Sub
        End If
        Dim PreFixTb As DataTable
        Dim vrPrefix As String = ""
        Dim vrVoucherNo As Long
        Dim prefixId As Integer
        Dim cashcustid As Integer
        Dim dtTable As DataTable
        Dim DiscAcc As Integer
        If EnableGST And dtTax Is Nothing Then CreateTaxTable(dtTax)
        If dtTax.Rows.Count > 0 Then dtTax.Rows.Clear()
        dtTable = _objcmnbLayer._fldDatatable("SELECT accid FROM AccMast WHERE AccSetId Like '%03%'")
        If dtTable.Rows.Count > 0 Then DiscAcc = dtTable(0)("accid")
        Dim vatId As Integer
        dtTable = _objcmnbLayer._fldDatatable("Select vatid from VatMasterTb")
        If dtTable.Rows.Count > 0 Then
            vatId = Val(dtTable(0)("vatid") & "")
        End If
        Dim dtinvitm As DataTable
        Dim billno As Integer
        Dim trid As Long
        Dim dr As DataRow
        Dim SlNo As Integer
        Dim BillDiscount As Double
        Dim BillAmount As Double
        Dim cess As Double
        Dim linetotal As Double
        Dim billdate As Date
        Dim TDrAmt As Double
        Dim customername As String = ""
        Dim billingmechineid As Long
        Dim Palm_ID As String
        Dim rndoff As Double
        'cnt = 1
        Dim rindex As Integer
        Dim r As Integer
        Dim isfound As Boolean
        Dim refno As String
        For i = 0 To cnt
            If billno <> Val(dtMose(i)("InvNo")) Then
                If isfound = True Then
                    calculateINV(TDrAmt, BillDiscount, DiscAcc)
                    CalculateGST(True)
                    linetotal = Convert.ToDouble(dtitemtransaction.Compute("SUM(linetotal)", ""))
                    trid = setInvCmnValue(i, prefixId, vrPrefix, vrVoucherNo, billdate, debitAcc, CreditAcc, BillAmount, BillDiscount, cashcustid, billno, customername)
                    If Math.Round(linetotal, NoOfDecimal) <> Math.Round(BillAmount, NoOfDecimal) Then
                        linetotal = Math.Round(BillAmount, NoOfDecimal) - Math.Round(linetotal, NoOfDecimal)
                        rndoff = Math.Round(linetotal, NoOfDecimal)
                        _objcmnbLayer._saveDatawithOutParm("UPDATE ItmInvCmnTb SET billingmechinenumber='" & Palm_ID & _
                                                           "', billingmechineid=" & billingmechineid & ", rndoff=" & rndoff & _
                                                           " WHERE TRID=" & trid)
                    Else
                        _objcmnbLayer._saveDatawithOutParm("UPDATE ItmInvCmnTb SET billingmechinenumber='" & Palm_ID & _
                                                          "', billingmechineid=" & billingmechineid & _
                                                          " WHERE TRID=" & trid)

                    End If
                    If DiscAcc = 0 Then
                        TDrAmt = TDrAmt - BillDiscount + rndoff
                    End If
                    UpdateAccounts(trid, BillAmount, TDrAmt, DiscAcc, billdate, vrPrefix, vrVoucherNo, BillDiscount, debitAcc, CreditAcc, _
                                   customername, False, linetotal, billno)
                    For r = 0 To dtitemtransaction.Rows.Count - 1
                        setInvDetValue(trid, r, 1, billdate)
                    Next
                    'vrVoucherNo = vrVoucherNo + 1
                End If
                If dtMose(i)("status") = "Credit" Then
                    PreFixTb = _objcmnbLayer._fldDatatable("SELECT  Id FROM PreFixTb WHERE VrTypeNo=4 AND ctgry = 3", False)
                    If PreFixTb.Rows.Count > 0 Then
                        prefixId = Val(PreFixTb(0)(0))
                        getVrsDet(prefixId, "IS", vrPrefix, vrVoucherNo, CreditAcc, debitAcc)
                    Else
                        getVrsDet(0, "IS", vrPrefix, vrVoucherNo, CreditAcc, debitAcc)
                    End If
                    debitAcc = createSingleAccountHead(i, "customer")
                ElseIf dtMose(i)("status") = "Cash" Then
                    PreFixTb = _objcmnbLayer._fldDatatable("SELECT  Id FROM PreFixTb WHERE VrTypeNo=4 AND ctgry = 1", False)
                    If PreFixTb.Rows.Count > 0 Then
                        prefixId = Val(PreFixTb(0)(0))
                        getVrsDet(prefixId, "IS", vrPrefix, vrVoucherNo, CreditAcc, debitAcc)
                    Else
                        getVrsDet(0, "IS", vrPrefix, vrVoucherNo, CreditAcc, debitAcc)
                    End If
                    cashcustid = updateSingleCashCustomer(i)
                Else
                    isfound = False
                    GoTo nxt
                End If
                rndoff = 0
                TDrAmt = 0
                rindex = 0
                billingmechineid = 0
                Palm_ID = 0
                Palm_ID = Val(dtMose(i)("Palm_ID"))
                billingmechineid = Val(dtMose(i)("BillId"))
                billno = Val(dtMose(i)("InvNo"))
                refno = Trim(dtMose(i)("refno") & "")
                BillDiscount = CDbl(dtMose(i)("BillDiscount"))
                BillAmount = CDbl(dtMose(i)("BillAmount"))
                billdate = CDate(dtMose(i)("Trdate"))
                customername = MkDbSrchStr(Trim(dtMose(i)("custName") & ""))
                SlNo = 0
                trid = 0
                isfound = True
                If dtitemtransaction.Rows.Count > 0 Then
                    dtitemtransaction.Rows.Clear()
                End If
            End If
chkagin:
            dtinvitm = getItmDtls(4, dtMose(i)("Item Code"), True)
            If dtinvitm.Rows.Count > 0 Then
                dr = dtitemtransaction.NewRow
                dr("dtTrId") = trid
                dr("SlNo") = SlNo + 1
                dr("ItemId") = dtinvitm(0)("ItemId")
                dr("unit") = dtinvitm(0)("unit")
                dr("cessacc") = Val(dtinvitm(0)("collectionAC") & "")
                dr("TrQty") = dtMose(i)("QTY")
                dr("UnitCost") = dtMose(i)("Sales Price")
                dr("ItemDiscount") = Val(dtMose(i)("Item Dis") & "")
                dr("PFraction") = IIf(IsDBNull(dtinvitm(0)("FraCount")), "2", dtinvitm(0)("FraCount"))
                If Not IsDBNull(dtMose(i)("CGST%")) Then
                    dr("taxP") = CDbl(dtMose(i)("CGST%")) + CDbl(dtMose(i)("SGST%"))
                    dr("CSGTP") = dtMose(i)("CGST%")
                    dr("SGSTP") = dtMose(i)("SGST%")
                    dr("IGSTP") = CDbl(dtMose(i)("CGST%")) + CDbl(dtMose(i)("SGST%"))
                End If
                dr("UnitOthCost") = 0
                dr("DisP") = 0
                dr("Method") = "B"
                dr("IDescription") = dtinvitm(0)("Description")
                dr("HSNCode") = dtinvitm(0)("HSNCode")
                cess = IIf(IsDBNull(dtinvitm(0)("vat")), 0, CDbl(dtinvitm(0)("vat")))
                If Val(dtMose(i)("KFC") & "") = 0 Then dtMose(i)("KFC") = 0
                dr("CessAmt") = CDbl(dtMose(i)("KFC")) '((CDbl(dtMose(i)("Sales Price")) * cess) / 100) * CDbl(dtMose(i)("QTY"))
                'KFC
                dtitemtransaction.Rows.Add(dr)
                If IsDBNull(dtMose(i)("CGST%")) Then
                    getGSTDetails(rindex)
                End If

                rindex = rindex + 1
                'linetotal = Format(linetotal + actualprice + CDbl(dr("taxAmt")) + CDbl(dr("CessAmt")), numFormat)
                status("Transfering Sales Invoice..", "", i, cnt)
            Else
                checkAndCreateItem(vatId, i)
                GoTo chkagin
            End If
nxt:
        Next

        If isfound Then
            calculateINV(TDrAmt, BillDiscount, DiscAcc)
            CalculateGST(True)
            linetotal = Convert.ToDouble(dtitemtransaction.Compute("SUM(linetotal)", ""))
            trid = setInvCmnValue(i, prefixId, vrPrefix, vrVoucherNo, billdate, debitAcc, CreditAcc, BillAmount, BillDiscount, cashcustid, billno, customername)
            If linetotal <> BillAmount Then
                linetotal = Math.Round(BillAmount - linetotal, 3)
                rndoff = Math.Round(linetotal, 2)
                _objcmnbLayer._saveDatawithOutParm("UPDATE ItmInvCmnTb SET billingmechinenumber='" & Palm_ID & _
                                                           "', billingmechineid=" & billingmechineid & ", rndoff=" & rndoff & _
                                                           " WHERE TRID=" & trid)
            Else
                _objcmnbLayer._saveDatawithOutParm("UPDATE ItmInvCmnTb SET billingmechinenumber='" & Palm_ID & _
                                                          "', billingmechineid=" & billingmechineid & _
                                                          " WHERE TRID=" & trid)
            End If
            If DiscAcc = 0 Then
                TDrAmt = TDrAmt - BillDiscount + rndoff
            End If
            UpdateAccounts(trid, BillAmount, TDrAmt, DiscAcc, billdate, vrPrefix, vrVoucherNo, BillDiscount, debitAcc, CreditAcc, _
                           customername, False, linetotal, billno)
            For i = 0 To dtitemtransaction.Rows.Count - 1
                setInvDetValue(trid, i, 1, billdate)
            Next

        End If
    End Sub
    Private Sub getGSTDetails(ByVal i As Integer, Optional ByVal statecode As Integer = 0)
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT * FROM GSTTb WHERE HSNCODE='" & dtitemtransaction(i)("HSNCode") & "'")
        If dt.Rows.Count > 0 Then
            dtitemtransaction(i)("CSGTP") = IIf(IsDBNull(dt(0)("CGST")), 0, CDbl(dt(0)("CGST")))
            dtitemtransaction(i)("SGSTP") = IIf(IsDBNull(dt(0)("SGST")), 0, CDbl(dt(0)("SGST")))
            dtitemtransaction(i)("IGSTP") = IIf(IsDBNull(dt(0)("IGST")), 0, CDbl(dt(0)("IGST")))
        Else
            dtitemtransaction(i)("CSGTP") = 0
            dtitemtransaction(i)("SGSTP") = 0
            dtitemtransaction(i)("IGSTP") = 0
        End If
        'Dim actualPrice As Double
        'Dim discountOther As Double
        'discountOther = dtitemtransaction(i)("UnitDiscount") * dtitemtransaction(i)("TrQty")
        'actualPrice = (CDbl(dtitemtransaction(i)("UnitCost")) * CDbl(dtitemtransaction(i)("TrQty"))) - CDbl(dtitemtransaction(i)("ItemDiscount")) - discountOther
        'actualPrice = Format(actualPrice, numFormat)
        'dtitemtransaction(i)("CGSTAMT") = (actualPrice * dtitemtransaction(i)("CSGTP")) / 100
        'dtitemtransaction(i)("SGSTAmt") = (actualPrice * dtitemtransaction(i)("SGSTP")) / 100
        'dtitemtransaction(i)("IGSTAmt") = (actualPrice * dtitemtransaction(i)("IGSTP")) / 100
        If statecode = 1 Then
            dtitemtransaction(i)("taxP") = dtitemtransaction(i)("IGSTP")
        Else
            dtitemtransaction(i)("taxP") = dtitemtransaction(i)("CSGTP") + dtitemtransaction(i)("SGSTP")
        End If
    End Sub
    Private Function setInvCmnValue(ByVal i As Integer, ByVal prefixId As Integer, ByVal prefix As String, _
                                    ByVal invno As Integer, ByVal TrDate As Date, ByVal custid As Integer, ByVal psacc As Integer, _
                                    ByVal netamount As Double, ByVal BillDiscount As Double, ByVal cashcustid As Integer, ByVal RefNo As String, ByVal CustName As String) As Long
        Dim TrId As Long
chkagain:
        If Not CheckNoExists(prefix, invno, "IS", "Inventory") Then
            invno = invno + 1
            GoTo chkagain
        End If
        With _objInv
            Dim Dt As Date = DateValue(Date.Now)
            .TrId = 0
            .TrDate = TrDate
            .TrType = "IS"
            .DocLstTxt = ""
            .InvTypeNo = prefixId
            .SlsManId = salesman
            .Prefix = prefix
            .InvNo = invno
            .TrRefNo = prefix & IIf(prefix = "", "", "/") & invno ' Trim(txtReference.Text)
            .CSCode = custid
            .PSAcc = psacc
            .JobCode = ""
            .ImpDoc = 0
            .UserId = CurrentUser
            .IsFC = False
            .FCRate = 1
            .NFraction = 2
            .FC = ""
            .Discount = BillDiscount
            .TrDescription = ""
            .TypeNo = getVouchernumber("IS")
            .EnaJob = False
            .DocDefLoc = ""
            .BrId = ""
            .OthCost = 0
            .Discount1 = 0
            .NetAmt = netamount
            .LPO = RefNo
            'Dim dt As Date = getServerDate()
            .CrtDt = Dt
            .DelDate = Dt
            .DueDate = Dt
            .DocDate = Dt
            .SuppInvDate = Dt
            .TermsId = ""
            .MchName = MACHINENAME
            .isModi = False
            .lpoclass = ""
            .rndoff = 0
            'If TaxType is 0 then invoice is interstate invoice otherwise intrastate invoice
            .TaxType = 0
            .OthrCust = ""
            .DocLstTxt = ""
            .isTaxInvoice = 1
            '.CustName = dtMose(i)("CustName")
            '.custid = cashcustid
        End With
        TrId = Val(_objInv._saveCmn())
        _objcmnbLayer._saveDatawithOutParm("UPDATE ItmInvCmnTb SET CashCustName='" & CustName & _
                                           "',CashCustid=" & cashcustid & " WHERE TRID=" & TrId)
        SetNextVrNoFromVariable(invno + 1, prefixId, "IS", "TrType = 'IS' AND InvNo = ", False, True, True, , prefix)
        Return TrId
    End Function
    Private Sub setInvDetValue(ByVal Invid As Long, ByVal i As Integer, ByVal PPerU As Single, ByVal billdate As Date)
        With _objInv
            .dtTrId = Invid
            .ItemId = dtitemtransaction(i)("ItemId")
            .Unit = dtitemtransaction(i)("Unit")
            .TrQty = CDbl(dtitemtransaction(i)("TrQty")) * PPerU
            .UnitCost = CDbl(dtitemtransaction(i)("UnitCost")) / PPerU
            .taxP = CDbl(dtitemtransaction(i)("taxP"))
            .taxAmt = (CDbl(dtitemtransaction(i)("taxAmt"))) / CDbl(PPerU)
            .PFraction = PPerU
            .UnitOthCost = CDbl(dtitemtransaction(i)("UnitOthCost")) / PPerU
            .Method = dtitemtransaction(i)("Method")
            .UnitDiscount = Val(dtitemtransaction(i)("UnitDiscount")) / PPerU
            .ItemDiscount = CDbl(dtitemtransaction(i)("ItemDiscount")) / PPerU
            If IsDBNull(dtitemtransaction(i)("DisP")) Then
                dtitemtransaction(i)("DisP") = 0
            End If
            .DisP = CDbl(dtitemtransaction(i)("DisP")) / PPerU
            .IDescription = dtitemtransaction(i)("IDescription")
            .SlNo = dtitemtransaction(i)("SlNo")
            .TrTypeNo = getVouchernumber("IS")
            .TrDateNo = getDateNo(CDate(billdate))
            .TrType = "IS"
            .id = 0
            .WarrentyName = ""
            .SerialNo = ""
            .WarrentyExpDate = DateValue("01/01/1950")
            .HSNCode = Trim(dtitemtransaction(i)("HSNCode") & "")
            .IGSTP = CDbl(dtitemtransaction(i)("IGSTP"))
            .IGSTAmt = CDbl(dtitemtransaction(i)("IGSTAmt"))
            .CSGTP = CDbl(dtitemtransaction(i)("CSGTP"))
            .CGSTAMT = CDbl(dtitemtransaction(i)("CGSTAMT"))
            .SGSTP = CDbl(dtitemtransaction(i)("SGSTP"))
            .SGSTAmt = CDbl(dtitemtransaction(i)("SGSTAmt"))
            .Smancode = ""
            .StartingReading = 0
            .EndingReading = 0
            .MeterCode = ""
            .impDocid = 0
            .impDocSlno = 0
            .WoodNetQty = 0
            .WoodDiscountQty = 0
            .FloodcessAmt = (CDbl(dtitemtransaction(i)("CessAmt"))) / PPerU
            .CessAmt = (CDbl(dtitemtransaction(i)("CessAmt"))) / PPerU
            .manufacturingdate = DateValue("01/01/1950")
            ._saveDetails()
        End With
    End Sub
    Private Sub UpdateCollection()
        Dim LinkNo As Long
        Dim FCRt As Integer = 1
        Dim PreFixTb As DataTable
        Dim vrPrefix As String = ""
        Dim vrVoucherNo As Long
        Dim prefixId As Integer
        Dim i As Integer
        Dim billno As Integer
        Dim isfound As Boolean
        Dim debitAcc As Integer
        Dim CreditAcc As Integer
        Dim billingmechineid As Long
        Dim Palm_ID As String = ""
        Dim BillAmount As Double
        Dim billdate As Date
        Dim customercode As String = ""
        Dim customername As String = ""
        Dim bankcode As String = ""
        Dim chqno As String = ""
        Dim chqdate As Date
        Dim dt1 As DataTable
        If dtMose.Rows.Count = 0 Then
            MsgBox("Record not found", MsgBoxStyle.Exclamation)
            btnok.Tag = 1
            Exit Sub
        End If
        Dim cnt As Integer = dtMose.Rows.Count - 1
        For i = 0 To cnt
            'If dtMose(i)("custcode") = "00113" Then
            '    MsgBox("")
            'End If
            If billno = Val(dtMose(i)("InvNo")) And customercode = dtMose(i)("custcode") And billdate = CDate(dtMose(i)("TRDate")) Then
                BillAmount = Math.Round(BillAmount + CDbl(dtMose(i)("PaidAmount")), 2)
            Else
                If isfound = True Then
                    If CreditAcc = 0 Or debitAcc = 0 Then GoTo nxt
chkagain:
                    If Not CheckNoExists(vrPrefix, vrVoucherNo, "RV", "Accounts") Then
                        vrVoucherNo = vrVoucherNo + 1
                        GoTo chkagain
                    End If
                    setAcctrCmnValue(0, 0, DateValue(billdate), vrPrefix, vrVoucherNo, "RV", "")
                    LinkNo = 0
                    LinkNo = Val(_objTr.SaveAccTrCmn())
                    _objcmnbLayer._saveDatawithOutParm("UPDATE AccTrCmn SET mechinenumber='" & Palm_ID & _
                                                         "', mechinervnumber=" & billno & _
                                                         ",collectedOrDeliveredBy='" & salesman & "'" & _
                                                         " WHERE LinkNo=" & LinkNo)
                    setoFFBal(Math.Round(BillAmount, 2), billno)
                    'debit entry
                    setAcctrDetValue(LinkNo, debitAcc, "ON/AC", "Received from " & customername, BillAmount, bankcode, chqno, chqdate, CreditAcc)
                    _objTr.saveAccTrans()
                    'credit entry
                    updateRVCreditAccount(CreditAcc, debitAcc, LinkNo, BillAmount)
                    SetNextVrNoFromVariable(vrVoucherNo + 1, prefixId, "RV", "JvType = 'RV' AND JVNum = ", False, True, True)
                End If
nxt:
                PreFixTb = _objcmnbLayer._fldDatatable("SELECT  Id FROM PreFixTb WHERE VrTypeNo=2 AND [Voucher Name]='" & dtMose(i)("PayMode") & "'", False)
                If PreFixTb.Rows.Count > 0 Then
                    prefixId = Val(PreFixTb(0)(0))
                    getVrsDet(prefixId, "RV", vrPrefix, vrVoucherNo, debitAcc, CreditAcc)
                Else
                    getVrsDet(0, "RV", vrPrefix, vrVoucherNo, CreditAcc, debitAcc)
                End If
                billingmechineid = 0
                Palm_ID = ""
                customername = ""
                Palm_ID = Trim(dtMose(i)("Palm_ID") & "")
                billno = Val(dtMose(i)("InvNo"))
                billdate = CDate(dtMose(i)("TRDate"))
                BillAmount = CDbl(dtMose(i)("PaidAmount"))
                bankcode = Trim(dtMose(i)("BankCode") & "")
                chqno = IIf(Trim(dtMose(i)("ChequeNO") & "") = "", Trim(dtMose(i)("CreditCardNo") & ""), Trim(dtMose(i)("ChequeNO") & ""))
                If Not IsDBNull(dtMose(i)("Chequedate")) Then
                    chqdate = CDate(dtMose(i)("Chequedate"))
                Else
                    chqdate = DateValue("01/01/1950")
                End If

                customercode = MkDbSrchStr(Trim(dtMose(i)("custcode") & ""))
                dt1 = _objcmnbLayer._fldDatatable("SELECT AccId,AccDescr FROM AccMast WHERE [Alias]='" & Trim(dtMose(i)("custcode") & "'"))
                If dt1.Rows.Count > 0 Then
                    CreditAcc = dt1(0)("AccId")
                    customername = dt1(0)("AccDescr")
                    ldTrans(CreditAcc)
                End If
                isfound = True
            End If
            status("Transfering Collection..", "", i, cnt)
        Next
        If isfound = True Then
            If CreditAcc = 0 Or debitAcc = 0 Then GoTo ext
chkagain1:
            If Not CheckNoExists(vrPrefix, vrVoucherNo, "RV", "Accounts") Then
                vrVoucherNo = vrVoucherNo + 1
                GoTo chkagain1
            End If
            setAcctrCmnValue(0, 0, DateValue(billdate), vrPrefix, vrVoucherNo, "RV", "")
            LinkNo = 0
            LinkNo = Val(_objTr.SaveAccTrCmn())
            _objcmnbLayer._saveDatawithOutParm("UPDATE AccTrCmn SET mechinenumber='" & Palm_ID & _
                                                 "', mechinervnumber=" & billno & _
                                                 " WHERE LinkNo=" & LinkNo)
            setoFFBal(Math.Round(BillAmount, 2), billno)
            'debit entry
            setAcctrDetValue(LinkNo, debitAcc, "ON/AC", "Received from " & customername, BillAmount, bankcode, chqno, chqdate, CreditAcc)
            _objTr.saveAccTrans()
            'credit entry
            updateRVCreditAccount(CreditAcc, debitAcc, LinkNo, BillAmount)
            SetNextVrNoFromVariable(vrVoucherNo + 1, prefixId, "RV", "JvType = 'RV' AND JVNum = ", False, True, True)
        End If
ext:
    End Sub
    Private Sub updateRVCreditAccount(ByVal CreditAcc As Integer, ByVal debitAcc As Integer, ByVal LinkNo As Long, ByVal paidAmt As Double)
        Dim i As Integer
        If paidAmt > 0 And dtSetoffTable.Rows.Count = 0 Then
            'setAcctrDetValue(LinkNo, CreditAcc, "ON/AC", "Advance Collection", paidAmt * -1, "", "", DateValue("01/01/1950"), debitAcc)
            '_objTr.saveAccTrans()
        Else
            For i = 0 To dtSetoffTable.Rows.Count - 1
                If Val(dtSetoffTable(i)("Assign") & "") > 0 Then
                    setAcctrDetValue(LinkNo, CreditAcc, Trim(dtSetoffTable(i)("Reference")), "Collection against " & dtSetoffTable(i)("Reference"), CDbl(dtSetoffTable(i)("Assign")) * -1, "", "", DateValue("01/01/1950"), debitAcc)
                    _objTr.saveAccTrans()
                End If
            Next
        End If

    End Sub
    Private Sub setAcctrDetValue(ByVal lnkNo As Long, ByVal AccountNo As Integer, ByVal Reference As String, ByVal EntryRef As String, _
                                 ByVal DealAmt As Double, ByVal BankCode As String, ByVal ChqNo As String, ByVal ChqDate As Date, ByVal PDCAcc As Integer)

        With _objTr
            .trLinkNo = lnkNo
            .AccountNo = AccountNo
            .Reference = Reference
            .EntryRef = EntryRef
            .FCAmt = DealAmt
            .JobCode = ""
            .JobStr = ""
            .CurrRate = 1
            .CurrencyCode = ""
            .DealAmt = DealAmt * .CurrRate

            .TrInf = 0
            .OthCost = 0
            .TermsId = ""
            .CustAcc = 0
            .AccWithRef = 0
            .UnqNo = 0 'Val(grd.Item(ConstUnq, _row).Value)
            .BankCode = BankCode
            .ChqNo = ChqNo
            .ChqDate = ChqDate
            .PDCAcc = PDCAcc
            .LPONo = ""
            .DocDate = Date.Now
            .SuppInvDate = Date.Now
            .DueDate = Date.Now
            .VesselId = ""
        End With
    End Sub
    Private Sub setoFFBal(ByVal paidAmt As Double, ByVal lpo As String)
        Dim i As Integer
        For i = 0 To dtSetoffTable.Rows.Count - 1
            If lpo = dtSetoffTable(i)("LPONO") Then
                If paidAmt > dtSetoffTable(i)("Balance") Then
                    dtSetoffTable(i)("Assign") = Format(CDbl(dtSetoffTable(i)("Balance")), numFormat)
                    paidAmt = Math.Round(paidAmt - CDbl(dtSetoffTable(i)("Balance")), 2)
                Else
                    dtSetoffTable(i)("Assign") = Format(paidAmt, numFormat)
                    paidAmt = 0
                    GoTo ext
                End If
                'dtSetoffTable(i)("Assign") = Format(paidAmt, numFormat)
                'paidAmt = 0
            End If
        Next
ext:
        If paidAmt > 0 Then
            Dim dtRow As DataRow
            dtRow = dtSetoffTable.NewRow
            dtRow("Reference") = "ON/AC"
            dtRow("Assign") = paidAmt
            dtSetoffTable.Rows.Add(dtRow)
        End If
    End Sub
    Private Sub ldTrans(ByVal accid As Long)
        Dim dttable As DataTable
        dttable = _objTr.returnldTrans(accid, 0, 0)
        If Not dtSetoffTable Is Nothing Then
            If dtSetoffTable.Rows.Count > 0 Then dtSetoffTable.Clear()
        Else
            dtSetoffTable = New DataTable
            CreateSetoffTable(dtSetoffTable)
        End If
        If dttable.Rows.Count > 0 Then
            Dim i As Integer
            Dim Bal As Double
            Dim Credit As Double
            Dim Debit As Double
            Dim Added As Boolean
            Dim PRef As String = ""
            Dim dtRow As DataRow
            dtRow = dtSetoffTable.NewRow
            For i = 0 To dttable.Rows.Count - 1
                If dttable(i)("DealType") = "C" Then GoTo NXT
                Dim s As String = UCase(dttable(i)("Reference"))

                If UCase(PRef) <> UCase(dttable(i)("Reference")) Then
                    If Added Then
                        dtSetoffTable.Rows.Add(dtRow)
                        dtRow = dtSetoffTable.NewRow
                    End If
                    Added = True
                    Bal = 0 'IIf(Rs!DealType = "D", 1, -1) * Rs!DealAmt
                    Debit = 0
                    Credit = 0
                    If Val(dttable(i)("CurrRate") & "") = 0 Then dttable(i)("CurrRate") = 1
                    If Val(dttable(i)("Amt") & "") = 0 Then dttable(i)("Amt") = 0
                    PRef = dttable(i)("Reference")
                    dtRow("JVDate") = dttable(i)("JVDate")
                    dtRow("Reference") = dttable(i)("Reference")
                    dtRow("EntryRef") = dttable(i)("EntryRef")
                    dtRow("CurrencyCode") = Trim(dttable(i)("CurrencyCode") & "")
                    dtRow("Rate") = dttable(i)("CurrRate")
                    dtRow("jvnum") = dttable(i)("jvnum")
                    dtRow("LpoNo") = dttable(i)("LpoNo")
                    'dtRow("LpoDate") = dttable(i)("LpoDate") 
                    dtRow("JobCode") = dttable(i)("JobCode")
                    If IsDBNull(dttable(i)("Fcdec")) Then
                        dtRow("Fcdec") = 2
                    Else
                        dtRow("Fcdec") = dttable(i)("Fcdec")
                    End If
                End If
                Bal = Bal + IIf(dttable(i)("DealType") = "D", 1, -1) * dttable(i)("Amt")
                Debit = Debit + IIf(dttable(i)("DealType") = "D", 1, 0) * dttable(i)("Amt")
                Credit = Credit + IIf(dttable(i)("DealType") = "C", 1, 0) * dttable(i)("Amt")
                dtRow("Type") = IIf(Bal < 0, "D", "C") & "r"
                dtRow("Tag") = ""
                If Val(dttable(i)("CurrRate") & "") = 0 Then dttable(i)("CurrRate") = 1
                dtRow("Balance") = Format(Bal / CDbl(dttable(i)("CurrRate")), "#,##" & numFormat)
                dtRow("InvAmt") = Format(IIf(Credit > Debit, Debit, Debit) / CDbl(dttable(i)("CurrRate")), "#,##" & numFormat)
                dtRow("SetOffAmt") = Format(IIf(Credit > Debit, Credit, Credit) / CDbl(dttable(i)("CurrRate")), "#,##" & numFormat)
NXT:
            Next
            If Added Then dtSetoffTable.Rows.Add(dtRow)
        End If
        dtSetoffTable.DefaultView.Sort = "JVDate ASC"
ext:
    End Sub
    Private Sub UpdateAccounts(ByVal TrId As Long, ByVal TDrAmt As Double, ByVal CrAmt As Double, ByVal DiscAcc As Integer, _
                               ByVal JVDate As Date, ByVal PreFix As String, ByVal JVNum As Integer, _
                               ByVal Discount As Double, ByVal customeracc As Integer, ByVal CrAcc As Integer, ByVal customername As String, _
                               ByVal tempenableMultipleDebitInInvoice As Boolean, ByVal rndOffAmt As Double, ByVal lpo As String)
        Dim LinkNo As Long
        Dim dtTable As DataTable
        Dim Reference As String
        Dim FCRt As Integer = 1

        If TrId > 0 Then
            dtTable = _objcmnbLayer._fldDatatable("SELECT LinkNo FROM AccTrCmn WHERE lnkno  = " & TrId)
            If dtTable.Rows.Count > 0 Then
                LinkNo = dtTable(0)("LinkNo")
                _objcmnbLayer._saveDatawithOutParm("DELETE FROM AccTrDet WHERE LinkNo=" & LinkNo)
            End If
        End If
        Reference = Trim(PreFix) & IIf(PreFix = "", "", "/") & JVNum
        setAcctrCmnValue(TrId, LinkNo, JVDate, PreFix, JVNum, "IS", lpo)
        LinkNo = 0
        LinkNo = Val(_objTr.SaveAccTrCmn())

        Dim EntRef As String = Strings.Left("POS TRANSACTION", 249)
        Dim dlAmt As Double = TDrAmt ' (TDrAmt - IIf(DiscAcc > 0, CDbl(Discount), 0)) * FCRt

        'Tax Entry Credit
        Dim i As Integer = 0
        If EnableGST Then
            For i = 0 To dtTax.Rows.Count - 1
                If Val(dtTax(i)("ACid")) > 0 And Val(dtTax(i)("Amount")) > 0 Then
                    Dim TxAmount As Double = CDbl(dtTax(i)("Amount"))
                    'dlAmt = dlAmt + (TxAmount * FCRt)
                    setAcctrDetValue(LinkNo, Val(dtTax(i)("ACid")), Reference, dtTax(i)("AccountName") & " Tax Collected", _
                                     TxAmount * -1 * FCRt, "", "", 0, 0, "", "", Val(customeracc), "", "", 1)
                    _objTr.saveAccTrans()
                End If
            Next
        End If
        'Debit Entry
        Dim mdebit As Double
        If tempenableMultipleDebitInInvoice Then
            If dtMultipleDebits.Rows.Count > 0 Then
                'Multiple Entry
                For j = 0 To dtMultipleDebits.Rows.Count - 1
                    setAcctrDetValue(LinkNo, Val(dtMultipleDebits(j)("accid")), Reference, _
                                     EntRef & IIf(dtMultipleDebits(j)("reference") <> "", " " & customername & " Ref: " & dtMultipleDebits(j)("reference"), ""), _
                                     CDbl(dtMultipleDebits(j)("accAmt")), "", "", 0, 0, "", _
                                    "", Val(customeracc), "", "", 1)
                    _objTr.saveAccTrans()
                    mdebit = mdebit + CDbl(dtMultipleDebits(j)("accAmt"))
                Next
                dlAmt = dlAmt - mdebit
                If dlAmt > 0 Then
                    GoTo deft
                End If
            Else
                GoTo deft
            End If
        Else
deft:
            'If Val(rndOffAmt) > 0 Then
            '    dlAmt = dlAmt - rndOffAmt
            'End If
            'dlAmt = Math.Round(dlAmt, 2)
            setAcctrDetValue(LinkNo, Val(customeracc), Trim(Reference), EntRef, dlAmt, "", "", 0, 0, lpo, _
                             "", CrAcc, "", "", FCRt)
            _objTr.saveAccTrans()
        End If
        'Credit Entry
        setAcctrDetValue(LinkNo, Val(CrAcc), Trim(Reference), EntRef, CrAmt * -1, "", "", 0, 0, "", _
                             "", customeracc, "", "", FCRt)
        _objTr.saveAccTrans()

        'DiscountEntry
        If Discount > 0 And DiscAcc > 0 Then
            dlAmt = Discount
            setAcctrDetValue(LinkNo, DiscAcc, Reference, EntRef, dlAmt, "", "", 2, 0, "", _
                            "", customeracc, "", "", 1)
            _objTr.saveAccTrans()
        End If
        If enableMultipleDebitInInvoice Then saveMultipleDebits(TrId)
        updateStockTransaction(TrId, LinkNo, customername, Reference, customeracc)
        updateClosingBalanceForInvoice(TrId)
    End Sub
    Private Sub updateStockTransaction(ByVal trid As Long, ByVal LinkNo As Long, ByVal customername As String, _
                                       ByVal reference As String, ByVal customeracc As Integer)
        If Not enableCostAccounting Then Exit Sub
        Dim stockAc As Long
        Dim costOfSalesAc As Long
        Dim dt As DataTable
        Dim costAmt As Double
        stockAc = getConstantAccounts(1)
        costOfSalesAc = getConstantAccounts(9)
        If stockAc = 0 Or costOfSalesAc = 0 Then Exit Sub
        dt = _objcmnbLayer._fldDatatable("select sum(CostAvg*TrQty) costAmt from ItmInvTrTb  where trid=" & trid)
        If dt.Rows.Count > 0 Then
            costAmt = Val(dt(0)("costAmt") & "")
        End If
        If costAmt <> 0 Then
            'debit entry [cost of sales]
            Dim entryref As String = "COST OF SALES FOR INVOICE : " & customername & " # " & reference
            setAcctrDetValue(LinkNo, costOfSalesAc, reference, entryref, costAmt, "", "", 3, 0, "", _
                           "", customeracc, "", "", 1)
            _objTr.saveAccTrans()
            'credit entry [stock in hand]
            costAmt = costAmt * -1
            setAcctrDetValue(LinkNo, stockAc, reference, entryref, costAmt, "", "", 3, 0, "", _
                           "", customeracc, "", "", 1)
            _objTr.saveAccTrans()
            'UpdtClosBal(stockAc, costAmt)
        End If
    End Sub
    Private Sub saveMultipleDebits(ByVal trid As Long)
        Dim i As Integer
        If dtMultipleDebits Is Nothing Then Exit Sub
        For i = 0 To dtMultipleDebits.Rows.Count - 1
            If Val(dtMultipleDebits(i)("dbtid")) = 0 Then
                _objcmnbLayer._saveDatawithOutParm("INSERT INTO SalesMultipleDebitsTb (dbtrid,dbaccid,accAmt,reference) VALUES" & _
                                                   "(" & trid & "," & Val(dtMultipleDebits(i)("accid")) & "," & CDbl(dtMultipleDebits(i)("accAmt")) & ",'" & Trim(dtMultipleDebits(i)("reference") & "") & "')")
            Else
                _objcmnbLayer._saveDatawithOutParm("UPDATE SalesMultipleDebitsTb SET setremove=0 " & _
                                                   " WHERE dbtid=" & dtMultipleDebits(i)("dbtid"))
            End If
        Next
        _objcmnbLayer._saveDatawithOutParm("DELETE FROM SalesMultipleDebitsTb " & _
                                                 " WHERE setremove=1 AND dbtrid=" & trid)
    End Sub
    Private Sub setAcctrCmnValue(ByVal InvTrid As Long, ByVal LinkNo As Long, ByVal JVDate As Date, ByVal PreFix As String, ByVal JVNum As Integer, ByVal JVType As String, ByVal LpoNo As String)
        _objTr.JVType = JVType
        _objTr.JVDate = DateValue(JVDate)
        _objTr.PreFix = Trim(PreFix)
        _objTr.JVNum = Val(JVNum)
        _objTr.JVTypeNo = getVouchernumber(JVType)
        _objTr.UserId = Trim(CurrentUser & "")
        _objTr.MchName = MACHINENAME
        _objTr.CrtDtTm = Date.Now
        _objTr.TypeNo = getVouchernumber(JVType)
        '_objTr.VrDescr = Trim(txtpayee.Text)
        _objTr.IsModi = 0
        _objTr.LPONo = LpoNo
        _objTr.LinkNo = InvTrid
    End Sub
    Private Sub setAcctrDetValue(ByVal lnkNo As Long, ByVal AccountNo As Long, ByVal Reference As String, ByVal EntryRef As String, ByVal DealAmt As Double, ByVal JobCode As String, ByVal JobStr As String, _
                                  ByVal TrInf As Short, ByVal OthCost As Short, ByVal LPO As String, ByVal TermsId As String, ByVal CustAcc As Integer, ByVal AccWithRef As String, _
                                  ByVal CurrencyCode As String, ByVal CurrRate As Double)
        With _objTr
            .trLinkNo = lnkNo
            .AccountNo = AccountNo
            .Reference = Reference
            .EntryRef = EntryRef
            .DealAmt = DealAmt
            .JobCode = JobCode
            .JobStr = JobStr
            .CurrRate = CurrRate
            .CurrencyCode = CurrencyCode ' IIf(chkFC.Checked = True, txtFC.Text, "")
            .TrInf = TrInf
            .OthCost = OthCost
            .TermsId = TermsId
            .CustAcc = CustAcc
            .AccWithRef = AccWithRef
            .LPONo = LPO
            Dim dtLPO As Date = Date.Now
            Dim dtDue As Date = Date.Now
            Dim dtSup As Date = Date.Now
            .DocDate = dtLPO
            .SuppInvDate = dtSup
            .DueDate = dtDue
        End With
    End Sub
    Private Sub createCustomerSupplier()
        Dim dt As DataTable
        Dim dt1 As DataTable
        Dim gname As String = ""
        Dim gId As Integer
        Dim i As Integer
        dt = _objcmnbLayer._fldDatatable("SELECT Alias,AccDescr FROM AccMast ")
        If rdosupplier.Checked Then
            dt1 = _objcmnbLayer._fldDatatable("SELECT Descr, S1AccId FROM S1AccHd WHERE GrpSetOn In ('Supplier') ORDER BY Descr")
        Else
            dt1 = _objcmnbLayer._fldDatatable("SELECT Descr, S1AccId FROM S1AccHd WHERE GrpSetOn In ('Customer') ORDER BY Descr")
        End If
        If dt1.Rows.Count > 0 Then
            gname = dt1(0)("Descr")
            gId = dt1(0)("S1AccId")
        End If
        Dim accountNo As Integer
        Dim Acode As String
        Dim accid As Long
        Dim Ctn As Integer = dtMose.Rows.Count - 1
        For i = 0 To Ctn
            Dim _qry = From job In dt.AsEnumerable() Where job![Alias] = Trim(dtMose(i)("Code") & "") Or job!AccDescr = Trim(dtMose(i)("Account Name") & "") Select New With _
                        {.Name = job!AccountNo}
            If _qry.Count = 0 Then
                Acode = GenerateNext(accountNo, gname, gId)
                If Trim(dtMose(i)("Code") & "") <> "" Then
                    Acode = Trim(dtMose(i)("Code") & "")
                End If
                If Val(dtMose(i)("Opn Bal") & "") = 0 Then dtMose(i)("Opn Bal") = 0
                If Val(dtMose(i)("CreditLimit") & "") = 0 Then dtMose(i)("CreditLimit") = 0
                If Val(dtMose(i)("DueDays") & "") = 0 Then dtMose(i)("DueDays") = 0
                _objcmnbLayer._saveDatawithOutParm("INSERT INTO AccMast (AccountNo, Alias, AccDescr, S1AccId,OpnBal,CreditLimit,DueDays,CountryCode,isTaxRegistered) VALUES (" & _
                                               Val(accountNo) & ", '" & MkDbSrchStr(Trim(Acode)) & "', '" & _
                                               MkDbSrchStr(Trim(dtMose(i)("Account Name") & "")) & "', " & gId & "," & _
                                               CDbl(dtMose(i)("Opn Bal")) & "," & CDbl(dtMose(i)("CreditLimit")) & "," & CDbl(dtMose(i)("DueDays")) & ",'" & _
                                               Trim(dtMose(i)("State code") & "") & "'," & IIf(Trim(dtMose(i)("GSTN") & "") <> "", 1, 0) & ")")

                dt1 = _objcmnbLayer._fldDatatable("SELECT AccId FROM AccMast WHERE AccountNo=" & Val(accountNo))
                If dt1.Rows.Count > 0 Then
                    accid = dt1(0)("AccId")
                End If
                _objcmnbLayer._saveDatawithOutParm("INSERT INTO AccMastAddr (AccountNo,Address1,Address2,Address3,Phone,ContactName,EMail," & _
                                                               "GSTIN) VALUES(" & _
                                                                  Val(accid) & ",'" & _
                                                                  MkDbSrchStr(Trim(dtMose(i)("Add1") & "")) & "','" & _
                                                                  MkDbSrchStr(Trim(dtMose(i)("Add2") & "")) & "','" & _
                                                                  MkDbSrchStr(Trim(dtMose(i)("Add3") & "")) & "','" & _
                                                                  MkDbSrchStr(Trim(dtMose(i)("Phone") & "")) & "','" & _
                                                                  MkDbSrchStr(Trim(dtMose(i)("Contact Name") & "")) & "','" & _
                                                                  MkDbSrchStr(Trim(dtMose(i)("email") & "")) & "','" & _
                                                                  MkDbSrchStr(Trim(dtMose(i)("GSTN") & "")) & "')")
            End If
        Next

    End Sub
    Private Function GenerateNext(ByRef AccountNo As Integer, ByVal Grpname As String, ByVal newVal As Integer) As String
        Dim N As Double
        Dim NewCode As String = ""
        GenerateNext = ""
        Dim tmp As String
        Dim _vdatatableAcc As DataTable
        _vdatatableAcc = _objcmnbLayer._fldDatatable("SELECT MAX(AccountNo)AccountNo FROM AccMast WHERE S1AccId =" & newVal)
        If _vdatatableAcc.Rows.Count > 0 Then
            AccountNo = Val(_vdatatableAcc(0)("AccountNo") & "")
        End If
        If Val(AccountNo & "") = 0 Then
            AccountNo = Val(newVal & "0000")
        End If
        If Val(AccountNo) >= Val(newVal & "9999") Then MsgBox("Maximum number of Ledgers reached in this Group.", MsgBoxStyle.Critical) : Exit Function
        AccountNo = Val(AccountNo) + 1
        _vdatatableAcc = _objcmnbLayer._fldDatatable("SELECT * FROM AccMast")
        Try
            Do Until False
                N = N + 1
                tmp = Strings.Left(Grpname, 4) & Format(N, StrDup(4, "0"))
                Dim _qry = From job In _vdatatableAcc.AsEnumerable() Where job![Alias] = tmp Select New With _
                       {.Name = job!AccountNo}
                If _qry.Count = 0 Then
                    NewCode = Strings.Left(Grpname, 4) & Format(N, StrDup(4, "0"))
                    NewCode = Mid(NewCode, 1, 30)
                    Exit Do
                End If
                For Each itm In _qry
                    If Val(itm.Name) = 0 Then
                        NewCode = Strings.Left(Grpname, 4) & Format(N, StrDup(4, "0"))
                        NewCode = Mid(NewCode, 1, 30)
                        Exit Do
                    End If
                Next
            Loop
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return NewCode
    End Function
    Private Sub checkAndCreateItem(ByVal vatId As Integer, ByVal i As Integer, Optional ByVal onlyInsert As Boolean = False)
        Dim hsncode As String
        Dim itemcode As String = ""
        Dim isCess As Integer
        Dim isNewItemcode As Boolean
        Dim dt As DataTable
        isNewItemcode = False
        '0.00 [CGST%],0.00 [SGST%]
        hsncode = ""
        If EnableGST Then
            If Val(dtMose(i)("GST%") & "") = 0 And rdosalesTransferFromMchn.Checked Then
                dtMose(i)("GST%") = Val(dtMose(i)("CGST%")) + Val(dtMose(i)("SGST%"))
            End If
            If Trim(dtMose(i)("hsncode") & "") = "" Then
                dtMose(i)("hsncode") = Trim(dtMose(i)("Item Code") & "")
            End If
            If Val(dtMose(i)("GST%") & "") > 0 Then
                hsncode = createHSN(Trim(dtMose(i)("hsncode") & ""), Val(dtMose(i)("GST%") & ""))
            Else
                hsncode = ""
            End If
            isCess = Val(dtMose(i)("Iscess") & "")
        ElseIf enableGCC Then
            isCess = vatId
        End If
        If Trim(dtMose(i)("Item Code") & "") = "" Then
            dt = _objcmnbLayer._fldDatatable("select isnull(LastAutomatedItemCodeFromPurchase,'') lastItemcode from CompanyTb ")
            If dt.Rows.Count > 0 Then
                itemcode = dt(0)("lastItemcode")
            End If
            itemcode = GenerateNextItemCode(itemcode)
            isNewItemcode = True
        Else
            itemcode = Trim(dtMose(i)("Item Code") & "")
            isNewItemcode = False
        End If
        dtMose(i)("Item Code") = itemcode

        Dim itemid As Long
        dt = _objcmnbLayer._fldDatatable("SELECT itemid from invitm where [Item Code]='" & itemcode & "'")
        If dt.Rows.Count = 0 Then
            If Trim(dtMose(i)("Item Name") & "") = "" Then
                Exit Sub
            End If
            dt = _objcmnbLayer._fldDatatable("SELECT itemid,[Item Code] from invitm where Description='" & MkDbSrchStr(Trim(dtMose(i)("Item Name") & "")) & "'")
            If dt.Rows.Count = 0 Then
                _objcmnbLayer._saveDatawithOutParm("INSERT INTO INVITM ([Item Code],Description,Unit,UnitPrice,itemCategory,HSNCode,MRP,vatId) VALUES(" & _
                                "'" & itemcode & "'," & _
                                "'" & MkDbSrchStr(Trim(dtMose(i)("Item Name") & "")) & "'," & _
                                "'" & IIf(Trim(dtMose(i)("Unit") & "") = "", "PCS", Trim(dtMose(i)("Unit") & "")) & "'," & _
                                Val(dtMose(i)("Sales Price") & "") & "," & _
                                "'Stock'," & _
                                "'" & hsncode & "'," & _
                                Val(dtMose(i)("MRP") & "") & "," & _
                                IIf(isCess = 1, vatId, 0) & ")")
                If isNewItemcode Then
                    _objcmnbLayer._saveDatawithOutParm("update CompanyTb set LastAutomatedItemCodeFromPurchase='" & itemcode & "'")
                End If
            Else
                dtMose(i)("Item Code") = dt(0)("Item Code")
                itemid = dt(0)("itemid")
                GoTo update
            End If
        Else
            itemid = Val(dt(0)("itemid"))
            If onlyInsert = False Then
update:
                _objcmnbLayer._saveDatawithOutParm("UPDATE INVITM SET UnitPrice= " & _
                                                  IIf(Val(dtMose(i)("Sales Price") & "") > 0, Val(dtMose(i)("Sales Price") & ""), "UnitPrice") & "," & _
                                                  "MRP=" & IIf(Val(dtMose(i)("MRP") & "") > 0, Val(dtMose(i)("MRP") & ""), "MRP") & "," & _
                                                  "HSNCode=case when isnull(HSNCode,'')='' then '" & hsncode & "' else HSNCode end, " & _
                                                  "vatId=" & IIf(isCess = 1, vatId, 0) & ",mechineItemcode='" & itemcode & "'" & _
                                                  " where itemid='" & itemid & "'")
            End If


        End If
    End Sub
    Private Sub checkAndCreateItemList()
        Try
            Dim i As Integer
            Dim Ctn As Integer = dtMose.Rows.Count - 1
            Dim dt As DataTable
            Dim vatId As Integer
            dt = _objcmnbLayer._fldDatatable("Select vatid from VatMasterTb")
            If dt.Rows.Count > 0 Then
                vatId = Val(dt(0)("vatid") & "")
            End If
            For i = 0 To Ctn
                checkAndCreateItem(vatId, i)
nxt:
                status("Creating Items", "", i, Ctn)
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        End Try


    End Sub
    Private Function createHSN(ByVal Hsncode As String, ByVal gst As Double) As String
        Dim dt As DataTable
        Dim collectionAcSGST As Integer
        Dim paymetacSGST As Integer
        Dim CollectionAcCSGT As Integer
        Dim PaymentAcCSGT As Integer
        Dim CollectionAcIGST As Integer
        Dim paymentacIGST As Integer
        Dim SGSTCGST As Double
        Dim GSTcode As String
        If Hsncode = "" Then Return ""
        GSTcode = Hsncode & " - " & gst & "%"
        dt = _objcmnbLayer._fldDatatable("select hsncode from GSTTb where HSNCode='" & GSTcode & "'")
        If dt.Rows.Count > 0 Then
            Return dt(0)("hsncode")
        End If
        dt = _objcmnbLayer._fldDatatable("Select * from GstDefaultSetTb" & _
                                         " left join (select accid cid,accdescr cgstname from accmast)cacd on GstDefaultSetTb.cac=cacd.cid" & _
                                         " left join (select accid pid,accdescr sgstname from accmast)pacd on GstDefaultSetTb.pac=pacd.pid" & _
                                         " left join (select accid igtid,accdescr igstname from accmast)igstacd on GstDefaultSetTb.igstac=igstacd.igtid" & _
                                         " left join (select accid cgstpid,accdescr cgstpname from accmast)cgstpacd on GstDefaultSetTb.cgstpac=cgstpacd.cgstpid" & _
                                         " left join (select accid sgstpid,accdescr sgstpname from accmast)sgstpacd on GstDefaultSetTb.sgstpac=sgstpacd.sgstpid" & _
                                         " left join (select accid igstpid,accdescr isgtpname from accmast)igstpacd on GstDefaultSetTb.igstpac=igstpacd.igstpid" & _
                                         " where gst=" & gst)
        If dt.Rows.Count > 0 Then
            collectionAcSGST = dt(0)("pid")
            paymetacSGST = dt(0)("sgstpid")
            CollectionAcCSGT = dt(0)("cid")
            PaymentAcCSGT = dt(0)("cgstpid")
            CollectionAcIGST = dt(0)("igtid")
            paymentacIGST = dt(0)("igstpid")
        End If
        SGSTCGST = Format(gst / 2, numFormat)
        _objGst = New clsGSTMaster
        With _objGst
            .gstid = 0
            .HSNCode = GSTcode
            .CGST = SGSTCGST
            .SGST = SGSTCGST
            .IGST = gst
            .CGSTCAc = CollectionAcCSGT
            .CGSTPAc = PaymentAcCSGT
            .SGSTCAc = collectionAcSGST
            .SGSTPAc = paymetacSGST
            .IGSTCAc = CollectionAcIGST
            .IGSTPAc = paymentacIGST
            .GSTName = Hsncode
            .saveGSTMaster()
        End With
        dtGST = _objGst.returnGstMaster(0)
        Return GSTcode
    End Function
    Private Function createVatCode(ByVal vatpercentage As Double) As Integer
        If vatpercentage = 0 Then Return 0
        Dim vatcode As String = vatpercentage & "%"
        Dim dt As DataTable
chkagain:
        dt = _objcmnbLayer._fldDatatable("select VATID from VatMasterTb where VAT=" & vatpercentage)
        If dt.Rows.Count > 0 Then
            Return dt(0)("vatid")
        End If
        _objcmnbLayer._saveDatawithOutParm("INSERT INTO VatMasterTb(VATCODE,VATNAME,VAT) Values('" & vatcode & "','" & vatcode & "'," & vatpercentage & ")")
        GoTo chkagain
    End Function
    
    Private Sub updateItemdetails()
        Dim i As Integer
        Dim dt As DataTable
        Dim ds As DataSet
        If chkskipzero.Checked Then
            Dim _qurey As EnumerableRowCollection(Of DataRow)
            _qurey = From data In dtMose.AsEnumerable() Where Val(data("OpQty") & "") > 0 Select data
            If _qurey.Count > 0 Then
                dtMose = _qurey.CopyToDataTable()
            Else
                dtMose = dtMose.Clone
            End If
        End If
        If chkappend.Checked = False Then
            _objcmnbLayer._saveDatawithOutParm("delete from InvItm")
            _objcmnbLayer._saveDatawithOutParm("delete from InvItmPropertiesTb")
        End If
        Dim Ctn As Integer = dtMose.Rows.Count - 1
        Dim hsncode As String
        Dim vatid As Integer
        Dim KLFC As Integer
        Dim itemcode As String = ""
        Dim itemgroup As Integer
        Dim levelid As Integer
        Dim multipleItems As String = ""
        Dim itemid As Long
        Dim lid As Integer

        dt = _objcmnbLayer._fldDatatable("Select locationid from LocationTb where LocCode='" & locationcode & "'")
        If dt.Rows.Count > 0 Then
            lid = dt(0)("locationid")
        End If
        For i = 0 To Ctn
            If Trim(dtMose(i)("Item Code") & "") <> "" Then
                itemcode = dtMose(i)("Item Code")
            Else
                itemcode = GenerateNextItemCode(itemcode)
            End If
            hsncode = createHSN(Trim(dtMose(i)("hsncode") & ""), Val(dtMose(i)("GST%") & ""))
            vatid = createVatCode(Val(dtMose(i)("Cess") & ""))
            KLFC = createVatCode(Val(dtMose(i)("KLFC") & ""))
            If Trim(dtMose(i)("GrpName") & "") <> "" Then
chlevel:
                If itemgroup = 0 Then
                    dt = _objcmnbLayer._fldDatatable("SELECT top 1 LCode from LevelTb")
                    If dt.Rows.Count = 0 Then
                        _objcmnbLayer._saveDatawithOutParm("Insert into LevelTb (LName,lorder) values('ItemGroup',1)")
                        GoTo chlevel
                    Else
                        itemgroup = dt(0)("LCode")
                    End If
                End If
checkgrp:
                dt = _objcmnbLayer._fldDatatable("SELECT UnqGrpId from GrpItmTb where GrpItmCode='" & Trim(dtMose(i)("GrpName") & "") & "'")
                If dt.Rows.Count = 0 Then
                    _objcmnbLayer._saveDatawithOutParm("Insert into GrpItmTb (GrpItmCode,LCode,GrpName) values('" & Trim(dtMose(i)("GrpName") & "") & "'," & itemgroup & ",'" & Trim(dtMose(i)("GrpName") & "") & "')")
                    GoTo checkgrp
                Else
                    levelid = dt(0)("UnqGrpId")
                End If
            End If

            dt = _objcmnbLayer._fldDatatable("SELECT itemid from invitm where [Item Code]='" & Trim(itemcode) & "'")
            If dt.Rows.Count = 0 Then
                _objcmnbLayer._saveDatawithOutParm("INSERT INTO INVITM ([Item Code],Description,Unit,OpQty,OpCost,UnitPrice,UnitPriceWS,itemCategory,HSNCode," & _
                                                   "MRP,shortDescr,longDescr,webname,SECONDPRICE,vatid,REGULARCESSID,Level1) VALUES(" & _
                                               "'" & MkDbSrchStr(Trim(itemcode & "")) & "'," & _
                                               "'" & MkDbSrchStr(Trim(dtMose(i)("Item Name") & "")) & "'," & _
                                               "'" & IIf(Trim(dtMose(i)("Unit") & "") = "", "PCS", Trim(dtMose(i)("Unit") & "")) & "'," & _
                                               Val(dtMose(i)("OpQty") & "") & "," & _
                                               Val(dtMose(i)("OpCost") & "") & "," & _
                                               Val(dtMose(i)("UnitPrice") & "") & "," & _
                                               Val(dtMose(i)("UnitPriceWS") & "") & "," & _
                                               "'" & IIf(Trim(dtMose(i)("itemCategory") & "") = "", "stock", Trim(dtMose(i)("itemCategory") & "")) & "'," & _
                                               "'" & Trim(hsncode & "") & "'," & _
                                               Val(dtMose(i)("MRP") & "") & "," & _
                                               "'" & MkDbSrchStr(Trim(dtMose(i)("shortDescr") & "")) & "'," & _
                                               "'" & MkDbSrchStr(Trim(dtMose(i)("longDescr") & "")) & "'," & _
                                               "'" & MkDbSrchStr(Trim(dtMose(i)("webname") & "")) & "'," & _
                                               Val(dtMose(i)("SECONDPRICE") & "") & "," & _
                                               Val(dtMose(i)("Cess") & "") & "," & _
                                               Val(dtMose(i)("KLFC") & "") & "," & _
                                               levelid & ")")
                status("Inserting Data", "", i, Ctn)
            ElseIf chkopeningOnly.Checked Then
                If Not chkappend.Checked Then
                    _objcmnbLayer._saveDatawithOutParm("UPDATE INVITM SET OpQty=" & Val(dtMose(i)("OpQty") & "") & "," & _
                                  "OpCost=" & Val(dtMose(i)("OpCost") & "") & _
                                  " WHERE ITEMID=" & Val(dt(0)("itemid")))
                Else
                    _objcmnbLayer._saveDatawithOutParm("UPDATE INVITM SET OpQty=isnull(OpQty,0)+" & Val(dtMose(i)("OpQty") & "") & _
                                  " WHERE ITEMID=" & Val(dt(0)("itemid")))
                    multipleItems = multipleItems & itemcode & " /QTY : " & Val(dtMose(i)("OpQty") & "") & _
                                    "/Location : " & Val(dtMose(i)("Location") & "") & vbCrLf

                End If
                itemid = dt(0)("itemid")
                status("Updating Data", "", i, Ctn)
            Else
                _objcmnbLayer._saveDatawithOutParm("UPDATE INVITM SET OpQty=" & Val(dtMose(i)("OpQty") & "") & "," & _
                                                   "OpCost=" & Val(dtMose(i)("OpCost") & "") & "," & _
                                                   "UnitPrice=" & Val(dtMose(i)("UnitPrice") & "") & "," & _
                                                   "UnitPriceWS=" & Val(dtMose(i)("UnitPriceWS") & "") & "," & _
                                                   "MRP=" & Val(dtMose(i)("MRP") & "") & _
                                                   " WHERE ITEMID=" & Val(dt(0)("itemid")))
                status("Updating Data", "", i, Ctn)
            End If
            If (Trim(dtMose(i)("Location") & "") <> "" Or locationcode <> "") And Val(dtMose(i)("OpQty") & "") > 0 Then
                If itemid = 0 Then
                    dt = _objcmnbLayer._fldDatatable("SELECT itemid from invitm where [Item Code]='" & itemcode & "'")
                    If dt.Rows.Count > 0 Then
                        itemid = dt(0)("itemid")
                    End If
                End If
                If itemid = 0 Then GoTo nxt
                ds = _objcmnbLayer._ldDataset("SELECT LocOpnQtyTb.itemid,LocOpnQtyTb.LocationID from LocOpnQtyTb " & _
                                                 "inner join invitm on LocOpnQtyTb.itemid=invitm.itemid " & _
                                                 "inner join LocationTb on LocationTb.locationid=LocOpnQtyTb.locationid " & _
                                                 "where LocOpnQtyTb.itemid=" & itemid & " AND LocCode ='" & dtMose(0)("Location") & "'" & _
                                                 " Select locationid from LocationTb where LocCode='" & dtMose(i)("Location") & "'", False)


                If ds.Tables(1).Rows.Count > 0 And locationcode = "" Then
                    lid = ds.Tables(1)(0)("locationid")
                End If
                If ds.Tables(0).Rows.Count > 0 Then
                    _objcmnbLayer._saveDatawithOutParm("Update LocOpnQtyTb set qty=" & IIf(chkappend.Checked, "Qty+", "") & Val(dtMose(i)("OpQty") & "") & _
                                                       " where itemid=" & Val(dt(0)("itemid")) & " and locationid=" & lid)
                Else
                    _objcmnbLayer._saveDatawithOutParm("Insert into LocOpnQtyTb (itemid,LocationID,qty) " & _
                                                       "values(" & itemid & "," & lid & "," & Val(dtMose(i)("OpQty") & "") & ")")
                End If

            End If
            itemid = 0
nxt:
        Next
        If multipleItems <> "" Then
            Dim fl As System.IO.StreamWriter
            fl = My.Computer.FileSystem.OpenTextFileWriter(Application.StartupPath & "/duplicateitems.txt", False)
            fl.WriteLine(multipleItems)
            fl.Close()
        End If
    End Sub
    Private Sub savePDCList()
        Dim dt As DataTable
        Dim cnt As Integer = dts.Tables(0).Rows.Count - 1
        'Dim dtunit As DataTable
        dt = dts.Tables(0)
        For i = 0 To cnt
            status("Item Transfer", dt(i)(0), i, cnt)

        Next
    End Sub
    Private Sub setExternalData()
        Dim i As Integer
        Dim j As Integer
        Dim dtr As DataRow
        Dim dt As DataTable
        If dts Is Nothing Then
            MsgBox("Invalid Transfer", MsgBoxStyle.Exclamation)
            btnok.Tag = 1
            Exit Sub
        End If
        Dim ctn As Integer = dts.Tables(0).Rows.Count
        dt = dts.Tables(0)
        dtMose.Rows.Clear()
        For i = 0 To ctn - 1
            If dt(i)(0) = "" Then GoTo nxt
            dtr = dtMose.NewRow
            status("Transfering from External", "", i + 1, ctn)
            For j = 0 To lstvw.Items.Count - 1
                If i = 234 Then
                    MsgBox("")
                End If
                Dim dtype As String
                dtype = dt.Columns(j).DataType.Name
                If Trim(dt(i)(j) & "") = "" Then
                    Select Case dtype
                        Case "String"
                            dt(i)(j) = ""
                        Case "Int64", "Int32", "Double", "Decimal", "Boolean"
                            dt(i)(j) = 0
                    End Select
                End If
                dtr(lstTable(j)(0)) = dt(i)(lstTable(j)(1))
            Next
            dtMose.Rows.Add(dtr)
nxt:
        Next
    End Sub
    Private Sub updateCashCustomerList()
        Dim i As Integer
        If chkappend.Checked = False Then
            _objcmnbLayer._saveDatawithOutParm("delete from CashCustomerTb")
        End If
        Dim ctn = dtMose.Rows.Count - 1
        For i = 0 To ctn
            _objcmnbLayer._saveDatawithOutParm("Insert into CashCustomerTb(CustName,Add1,Add2,Add3,Phone,email,gender) values(" & _
                                               "'" & dtMose(i)("CustName") & "'," & _
                                               "'" & dtMose(i)("Add1") & "'," & _
                                               "'" & dtMose(i)("Add2") & "'," & _
                                               "'" & dtMose(i)("Add3") & "'," & _
                                               "'" & dtMose(i)("Phone") & "'," & _
                                               "'" & dtMose(i)("email") & "'," & _
                                               "'" & dtMose(i)("gender") & "'" & _
                                               ")")
            status("Inserting Data", "", i, ctn)
        Next
    End Sub
 


    Public Sub status(ByVal Mname As String, ByVal RecName As String, ByVal rec As Integer, ByVal count As Integer)
        If Me.InvokeRequired Then
            Dim d As New GenericDelegate(AddressOf status)
            Me.Invoke(d, Mname, RecName, rec, count)
        Else
            lblmodule.Text = Mname
            lblrec.Text = rec & " / " & count
            If rec = 0 Then
                pb.Value = 0
            Else
                pb.Value = rec * 100 / count
            End If
        End If
    End Sub

    Private Sub TransferiItemsFromExcel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If isFromPurchase Then
            rdoitemlistExcel.Visible = False
            rdocashcustomerEx.Visible = False
            rdocreditcustomer.Visible = False
            rdoPdcIssdEx.Visible = False
            rdosupplier.Visible = False
            rdopurchase.Left = rdoitemlistExcel.Left
            rdopurchase.Top = rdoitemlistExcel.Top
            rdopurchase.Visible = True
            rdopurchase.Checked = True
        ElseIf isPosTransfer Then
            rdoitemlistExcel.Visible = False
            rdocashcustomerEx.Visible = False
            rdocreditcustomer.Visible = False
            rdoPdcIssdEx.Visible = False
            rdosupplier.Visible = False
            rdopurchase.Visible = False
            rdosalesTransferFromMchn.Left = rdoitemlistExcel.Left
            rdosalesTransferFromMchn.Top = rdoitemlistExcel.Top
            rdosalesTransferFromMchn.Visible = True
            rdosalesTransferFromMchn.Checked = True
            Panel2.Visible = True
            Panel2.Left = rdosalesTransferFromMchn.Left
            chkmdb.Checked = True
            AddtoCombo(cmbsalesman, "SELECT SManCode FROM SalesmanTb", True, False)

        End If
        setType()
        ldMoseColumn()
        createLstTable()
        loadExcelTrasferDefaultFormatNames()
    End Sub
    Private Sub setType()
        chkappend.Visible = False
        If rdoitemlistExcel.Checked Then
            tp = 1
            chkappend.Visible = True
        ElseIf rdocashcustomerEx.Checked Then
            tp = 2
        ElseIf rdoPdcIssdEx.Checked Then
            tp = 0
        ElseIf rdopurchase.Checked Then
            tp = 3
        ElseIf rdosupplier.Checked Then
            tp = 4
        ElseIf rdocreditcustomer.Checked Then
            tp = 5
        ElseIf rdosalesTransferFromMchn.Checked Then
            If rdosales.Checked Then
                tp = 6
            Else
                tp = 7
            End If

        End If
    End Sub
    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        With lstvw
            lstvw.Items.Add(lstmose.SelectedItem)
            If .Items.Item(lstvw.Items.Count - 1).SubItems.Count > 1 Then
                .Items.Item(lstvw.Items.Count - 1).SubItems(1).Text = .Items.Add(lstexcel.SelectedItem)
            Else
                .Items.Item(lstvw.Items.Count - 1).SubItems.Insert(1, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, lstexcel.SelectedItem))
            End If
        End With
    End Sub
    Private Sub saveAcctr()
        Dim UpdateSql As String
        UpdateSql = "Insert into AccTrCmn(JVTypeNo,JVType,JVNum,PreFix,JVDate,UserId,TypeNo,LnkBkgNo,LnkContNo," & _
            "ContractTran,SMan,MchName,PDCLinkNo,CrtdtTm) values(" & _
            "@JVTypeNo,@JVType,@JVNum,@PreFix,@JVDate,@UserId,@TypeNo,@LnkBkgNo,@LnkContNo,@ContractTran," & _
            "@SMan,@MchName,@PDCLinkNo,@CrtdtTm)"

    End Sub


    Private Sub rdoPdcIssd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoPdcIssdEx.Click, rdocashcustomerEx.Click, _
                                                                                                rdoitemlistExcel.Click, rdopurchase.Click, _
                                                                                                rdocreditcustomer.Click, rdosupplier.Click, _
                                                                                                rdosales.Click, rdoreceipts.Click
        Dim myctrl As RadioButton
        myctrl = sender
        setType()
        ldMoseColumn()
        loadFromFile(True)
        lstvw.Items.Clear()
        lstexcel.Items.Clear()
        cmbformat.Text = ""
        chkopeningOnly.Visible = False
        chkskip.Visible = False
        loadExcelTrasferDefaultFormatNames()
        If myctrl.Name = "rdoitemlistExcel" Then
            chkopeningOnly.Visible = True
            chkopeningOnly.Enabled = True
            chkskip.Visible = True
            chklocqty.Visible = True
            chkskipzero.Visible = True
        Else
            chkopeningOnly.Enabled = False
            chkopeningOnly.Visible = True
            chklocqty.Visible = False
            chkskipzero.Visible = False
        End If
    End Sub

    Private Sub btnremove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnremove.Click
        If lstvw.SelectedItems.Count = 0 Then Exit Sub
        lstvw.Items.RemoveAt(lstvw.SelectedItems(0).Index)
    End Sub

    Private Sub Worker_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Worker.RunWorkerCompleted
        If rdopurchase.Checked Then
            Dim _qurey As EnumerableRowCollection(Of DataRow)
            _qurey = From data In dtMose.AsEnumerable() Where data(1).ToString() <> "" Select data
            If _qurey.Count > 0 Then
                dtMose = _qurey.CopyToDataTable
            Else
                dtMose.Rows.Clear()
            End If
            RaiseEvent loadtoPurchase(dtMose)
        End If
        If Val(btnok.Tag) = 0 Then
            MsgBox("Transfer Completed", MsgBoxStyle.Information)
        End If
        btnok.Tag = ""
        lblrec.Text = ""
        lblmodule.Text = ""
        pb.Value = 0
        If Not rdosalesTransferFromMchn.Checked Then
            Me.Close()
        End If
    End Sub
    Private Sub loadExcelTrasferDefaultFormat()
        Dim dt As DataTable
        Dim i As Integer
        If cmbformat.Text = "" Then Exit Sub
        dt = _objcmnbLayer._fldDatatable("Select id,isnull(filename,'')filename from ExcelTranferFormatCmnTb WHERE excelformatname='" & cmbformat.Text & "'")
        If dt.Rows.Count > 0 Then
            If dt(0)("filename") <> "" Then
                txtpath.Text = dt(0)("filename")
                If FileExists(txtpath.Text) Then
                    loadFromFile(True)
                Else
                    MsgBox("File not Found", MsgBoxStyle.Exclamation)
                End If
            End If
            dt = _objcmnbLayer._fldDatatable("Select * from ExcelTransferFormatDetTb WHERE cmnid=" & dt(0)(0))

            lstvw.Items.Clear()
            For i = 0 To dt.Rows.Count - 1
                With lstvw
                    lstvw.Items.Add(dt(i)("mosecolumn"))
                    If .Items.Item(lstvw.Items.Count - 1).SubItems.Count > 1 Then
                        .Items.Item(lstvw.Items.Count - 1).SubItems(1).Text = .Items.Add(dt(i)("excelcolumn"))
                    Else
                        .Items.Item(lstvw.Items.Count - 1).SubItems.Insert(1, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, dt(i)("excelcolumn")))
                    End If
                End With
            Next
        End If

    End Sub

    Private Sub loadExcelTrasferDefaultFormatNames()
        Dim i As Integer
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select excelformatname from ExcelTranferFormatCmnTb where transfertype=" & tp)
        cmbformat.Items.Clear()
        cmbformat.Items.Add("")
        For i = 0 To dt.Rows.Count - 1
            cmbformat.Items.Add(dt(i)("excelformatname"))
        Next
    End Sub
    Private Sub btnsetformat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsetformat.Click
        Dim formatname As String
        If cmbformat.Text = "" Then
            formatname = InputBox("Please Enter Format name:")
        Else
            formatname = cmbformat.Text
        End If
        If formatname = "" Then
            MsgBox("Please enter format name", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If lstvw.Items.Count = 0 Then
            MsgBox("Format not found", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Dim i As Integer
        Dim dt As DataTable
        'lstvw.Items(i).SubItems(0).Text
        dt = _objcmnbLayer._fldDatatable("Select id from ExcelTranferFormatCmnTb where excelformatname='" & formatname & "'")
        If dt.Rows.Count = 0 Then
            _objcmnbLayer._saveDatawithOutParm("INSERT INTO ExcelTranferFormatCmnTb (excelformatname,transfertype,filename) values" & _
                                         "('" & formatname & "'," & tp & ",'" & txtpath.Text & "')")
            dt = _objcmnbLayer._fldDatatable("Select id from ExcelTranferFormatCmnTb where excelformatname='" & formatname & "'")
            If dt.Rows.Count > 0 Then
                cmbformat.Tag = dt(0)(0)
            End If
        Else
            _objcmnbLayer._saveDatawithOutParm("UPDATE ExcelTranferFormatCmnTb SET excelformatname='" & formatname & "',filename='" & txtpath.Text & "' WHERE id=" & dt(0)(0))
            cmbformat.Tag = dt(0)(0)
        End If
        _objcmnbLayer._saveDatawithOutParm("delete from ExcelTransferFormatDetTb where cmnid=" & Val(cmbformat.Tag))
        For i = 0 To lstvw.Items.Count - 1
            _objcmnbLayer._saveDatawithOutParm("INSERT INTO ExcelTransferFormatDetTb (cmnid,mosecolumn,excelcolumn) values(" & _
                                               Val(cmbformat.Tag) & ",'" & lstvw.Items(i).SubItems(0).Text & "','" & lstvw.Items(i).SubItems(1).Text & "')")

        Next
        MsgBox("Format set", MsgBoxStyle.Information)
        loadExcelTrasferDefaultFormatNames()
    End Sub

    Private Sub cmbformat_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbformat.SelectedIndexChanged
        loadExcelTrasferDefaultFormat()
    End Sub
    Private Function GenerateNextItemCode(ByVal strCode As String) As String
        Dim N As Double
        Dim NewCode As String
        Dim i As Byte
        Dim f As Byte
        Dim tmp As String
        'If strCode = "" Then
        '    strCode = _objItmMast.returnLastItemCode
        'End If
        If strCode = "" Then
            strCode = "ITM0"
        End If
        Dim dr As IDataReader = Nothing
        If strCode = "" Then Return strCode
        For i = 1 To 11
            If IsNumeric(Mid(strCode, Len(strCode) - i + 1, 1)) = False Then Exit For
            tmp = Val(Mid(strCode, Len(strCode) - i + 1))
            If Val(tmp) <> 0 Then
                N = Val(tmp)
            Else
                If N <> 0 Then Exit For
            End If
            If i = Len(strCode) Then i = i + 1 : Exit For
        Next i
        i = i - 1
        f = i
        If i <= 0 Then
            'i = txtCode.MaxLength - Len(strCode)
            i = 3
        End If
        tmp = ""
        NewCode = ""
        Try
            Do Until False
                N = N + 1
                tmp = Mid(strCode, 1, IIf(Len(strCode) <> 0, Len(strCode) - f, 0)) & Format(N, StrDup(i, "0"))
                dr = _objItmMast.ldmst("SELECT [Item Code] FROM InvItm WHERE [Item Code] = '" & tmp & "'")
                If dr.Read = False Then
                    NewCode = Mid(strCode, 1, IIf(Len(strCode) <> 0, Len(strCode) - f, 0)) & Format(N, StrDup(i, "0"))
                    NewCode = Mid(NewCode, 1, 30)
                    Exit Do
                End If
            Loop
            If Not dr Is Nothing Then dr.Close()
            _objItmMast.clsreader()
            _objItmMast.clsCnnection()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return NewCode
    End Function
    Private Sub btnrefreshmechinedata_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefreshmechinedata.Click
        If txtpath.Text = "" Then
            MsgBox("Invalid File", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If Not FileExists(txtpath.Text) Then
            MsgBox("File not found", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        refreshMechineData()
        MsgBox("Done", MsgBoxStyle.Information)
        btnok.Enabled = True
    End Sub


    Private Sub chklocqty_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chklocqty.CheckedChanged
        pllocation.Visible = chklocqty.Checked
        AddtoCombo(cmblocation, "SELECT LocCode FROM LocationTb", True, False)
    End Sub

    Private Sub cmblocation_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmblocation.SelectedIndexChanged
        locationcode = cmblocation.Text
    End Sub

    Private Sub rdoitemlistExcel_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoitemlistExcel.CheckedChanged

    End Sub
End Class