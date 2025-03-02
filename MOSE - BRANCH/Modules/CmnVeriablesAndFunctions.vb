Imports System.IO
Imports System.Xml
Imports System.Runtime.InteropServices
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Module CmnVeriablesAndFunctions
#Region "GridConstantVariables"
    Public Const ConstSlNo = 0
    Public Const ConstBarcode = 2 'HSN Code
    Public Const ConstItemCode = 1
    Public Const ConstDescr = 3
    Public Const ConstB = 4
    Public Const ConstUnit = 5
    Public Const ConstLocation = 6 'WARRENTY COLUMN
    Public Const Constsman = 7 'SALESMAN COLUMN
    Public Const ConstMeterCode = 8 'salsepoint
    Public Const ConstStartReading = 9
    Public Const ConstEndReading = 10
    Public Const ConstSerialNo = 11
    Public Const ConstManufacturingdate = 12
    Public Const ConstWarrentyExpiry = 13
    Public Const ConstTotalProduction = 14 ' for restaurent stock damage entry
    Public Const ConstTotalSales = 15 ' for restaurent stock damage entry
    Public Const ConstWoodQty = 16
    Public Const ConstWoodDiscQty = 17
    Public Const ConstQty = 18
    Public Const ConstFocQty = 19
    Public Const ConstMRP = 20

    Public Const ConstSP1 = 21
    Public Const ConstSP2 = 22
    Public Const ConstSP3 = 23

    Public Const ConstUPrice = 24
    Public Const ConstDisP = 25
    Public Const ConstDisAmt = 26
    Public Const constItmTot = 27
    Public Const ConstTaxP = 28
    Public Const ConstTaxAmt = 29
    Public Const ConstcessAmt = 30
    Public Const ConstLTotal = 31
    Public Const ConstUnitOthCost = 32
    Public Const ConstNUPrice = 33
    Public Const ConstActualOthCost = 34
    Public Const ConstMthd = 35
    Public Const ConstUnitVal = 36
    Public Const ConstDiscOther = 37
    Public Const ConstJob = 38
    Public Const ConstJobCostAc = 39
    Public Const ConstBcodeOrICode = 40
    Public Const ConstImpLnId = 41
    Public Const ConstImpDocId = 42
    Public Const ConstActualPrice = 43
    Public Const ConstJobAcID = 44
    Public Const ConstPMult = 45
    Public Const ConstPFraction = 46
    Public Const ConstItemID = 47
    Public Const ConstImpJobChildTbID = 48
    Public Const ConstLrow = 49
    Public Const ConstId = 50
    Public Const ConstqtyChg = 51
    Public Const ConstCGSTP = 52
    Public Const ConstCGSTAmt = 53
    Public Const ConstSGSTP = 54
    Public Const ConstSGSTAmt = 55
    Public Const ConstIGSTP = 56
    Public Const ConstIGSTAmt = 57
    Public Const ConstIsSerial = 58
    Public Const ConstIsManufacturingItem = 59
    Public Const ConstDonotAllowsaveItem = 60
    Public Const Constcess = 61
    Public Const ConstcessAc = 62
    Public Const ConstBatchQty = 63
    Public Const ConstBatchCost = 64
    Public Const ConstRegcess = 65
    Public Const ConstRegcessAc = 66
    Public Const ConstAdditionalcess = 67
    Public Const ConstregularCessAmt = 68
    Public Const ConstFloodCessAmt = 69
    Public Const ConstLineProfit = 70
    Public Const ConstUnitCount = 71


#End Region
    Private _objcmnbLayer As New clsCommon_BL
    Private _vSrchdatatable As New DataTable
    Public _vInvItmtable As New DataTable
    Private _objItmMast As clsItemMast_BL
    Private _objTr As clsAccountTransaction
    Private _objGst As clsGSTMaster
    Public JobidForConstruct As Long
    Public dtGST As DataTable
    Public dtDefAcc As DataTable
    Public dtInvTb As DataTable
    Public dtAccTb As DataTable
    Public dtDocTb As DataTable
    Public dtvoucherTypes As DataTable
    Public isnewItemcreated As Boolean
    Public isnewCurrencyAdded As Boolean
    Public isnewSalesmanAdded As Boolean
    Public iswarrentyAdded As Boolean
    Public islocationAdded As Boolean


    Private Structure SHFILEINFO
        Public hIcon As IntPtr
        Public iIcon As Int32
        Public dwAttributes As Int32
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=MAX_PATH)> _
        Public szDisplayName As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=80)> _
        Public szTypeName As String
    End Structure
    Private Enum IconSize
        SHGFI_LARGEICON = 0
        SHGFI_SMALLICON = 1
    End Enum
    Private Const MAX_PATH As Int32 = 260
    Private Const SHGFI_ICON As Int32 = &H100
    Private Const SHGFI_USEFILEATTRIBUTES As Int32 = &H10
    Private Const FILE_ATTRIBUTE_NORMAL As Int32 = &H80



    Public Sub AddNodes(ByVal Nodes As TreeNodeCollection, ByVal rows As DataRow(), ByVal dt As DataTable)
        Try
            For Each row As DataRow In rows
                Dim Node = Nodes.Add(row("Name").ToString)
                Dim SubRows = dt.Select("IDParent = " & CInt(row("ID")))
                AddNodes(Node.Nodes, SubRows, dt)
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Function FindNextCell(ByVal dgv As DataGridView, ByVal RowIndex As Integer, ByVal ColIndex As Integer) As Boolean
        On Error Resume Next
        Dim found As Boolean = False
        While dgv.RowCount > RowIndex
            While dgv.Columns.Count > ColIndex
                If Not (dgv.Rows(RowIndex).Cells(ColIndex)).ReadOnly And dgv.Rows(RowIndex).Cells(ColIndex).Visible = True Then
                    dgv.CurrentCell = dgv.Rows(RowIndex).Cells(ColIndex)
                    FindNextCell = False
                    Exit Function
                Else
                    ColIndex += 1
                    dgv.BeginEdit(True)
                End If
            End While
            RowIndex += 1
            ColIndex = 0
            FindNextCell = True
        End While
    End Function
    Public Function FileExists(ByVal strPathName As String) As Boolean
        FileExists = File.Exists(strPathName)
    End Function
    Public Function DirExist(ByVal strPathName As String) As Boolean
        Return Directory.Exists(strPathName)
    End Function
    Public Sub SetForm(ByVal frm As Selectfrm, ByVal BVal As Single)
        With frm
            Select Case BVal
                Case 0 'Supplier
                    .strMyCaption = "Select Supplier"
                    .rbSupplier.Checked = True
                    .strMyQry = AssignAccSQLStr(1, "", 2)
                    .bModify = True
                    .bAddnew = True
                    ''DoEnaRadios(False, True, True, True, True, True, True, True, True, True)
                    .rbPDCIssued.Enabled = False
                    .rbPDCRcvd.Enabled = False
                    .rbSales.Enabled = False
                    .rbPurchase.Enabled = False
                    .rbExpence.Enabled = False
                    .cmbShowIndex = 0
                    .BVal = 1
                Case 1 'Customer
                    .strMyCaption = "Select Customer"
                    .rbCustomer.Checked = True
                    .strMyQry = AssignAccSQLStr(0, "", 2)
                    .bModify = True
                    .bAddnew = True
                    ''DoEnaRadios(False, True, True, True, True, True, True, True, True, True)
                    .rbPDCIssued.Enabled = False
                    .rbPDCRcvd.Enabled = False
                    .rbSales.Enabled = False
                    .rbPurchase.Enabled = False
                    .rbExpence.Enabled = False
                    .cmbShowIndex = 3
                    .BVal = 1

                Case 2 'All Account
                    .strMyCaption = "Select Account"
                    .rbAllAc.Checked = True
                    .strMyQry = AssignAccSQLStr(8, "", 2)
                    .bModify = True
                    .bAddnew = True
                    'DoEnaRadios(False, True, True, True, True, True, True, True, True, True)
                    '.rbPDCIssued.Enabled = True
                    '.rbPDCRcvd.Enabled = True
                    '.rbSales.Enabled = True
                    '.rbPurchase.Enabled = True
                    '.rbExpence.Enabled = True
                    .cmbShowIndex = 0
                    .BVal = 2
                Case 3   ' Selelct Job 
                    .strMyCaption = "Select Job "
                    .pnlRadios.Visible = False
                    .strMyQry = "SELECT JCode [Job Code], JName [Job Name] FROM JobDetTbA"
                    .bModify = False
                    .bAddnew = False
                    .cmbShowIndex = 0
                    .BVal = 3
                Case 4  ' Sales Account
                    .strMyCaption = "Select Sales Account"
                    .rbSales.Checked = True
                    .strMyQry = AssignAccSQLStr(3, "", 2)
                    .bModify = True
                    .bAddnew = True
                    .rbPDCRcvd.Enabled = False
                    .rbPDCIssued.Enabled = False
                    'DoEnaRadios(False, True, True, True, True, True, True, True, True, True)
                    .cmbShowIndex = 0
                Case 5 'Cash
                    .strMyCaption = "Select Cash A/C"
                    .rbCustomer.Checked = True
                    .strMyQry = AssignAccSQLStr(13, "", 2)
                    .bModify = True
                    .bAddnew = True
                    ''DoEnaRadios(False, True, True, True, True, True, True, True, True, True)
                    .rbPDCIssued.Enabled = False
                    .rbPDCRcvd.Enabled = False
                    .rbSales.Enabled = False
                    .rbPurchase.Enabled = False
                    .rbExpence.Enabled = False
                    .cmbShowIndex = 0
                    .BVal = 5
                Case 6 'Bank
                    .strMyCaption = "Select Bank A/C"
                    .rbCustomer.Checked = True
                    .strMyQry = AssignAccSQLStr(14, "", 2)
                    .bModify = True
                    .bAddnew = True
                    ''DoEnaRadios(False, True, True, True, True, True, True, True, True, True)
                    .rbPDCIssued.Enabled = False
                    .rbPDCRcvd.Enabled = False
                    .rbSales.Enabled = False
                    .rbPurchase.Enabled = False
                    .rbExpence.Enabled = False
                    .cmbShowIndex = 0
                    .BVal = 6
                Case 7  ' Purchase A/c
                    .strMyCaption = "Purchase Account"
                    .rbPurchase.Checked = True
                    .strMyQry = AssignAccSQLStr(4, "", 2)
                    .bModify = True
                    .bAddnew = True
                    .rbPDCIssued.Enabled = False
                    .rbPDCRcvd.Enabled = False
                    .rbSales.Enabled = False
                    .rbPurchase.Enabled = True
                    .rbExpence.Enabled = True
                    .cmbShowIndex = 0
                Case 8  ' Sales A/c
                    .strMyCaption = "Sales Account"
                    .rbSales.Checked = True
                    .strMyQry = AssignAccSQLStr(3, "", 2)
                    .bModify = True
                    .bAddnew = True
                    .rbPDCIssued.Enabled = False
                    .rbPDCRcvd.Enabled = False
                    .rbSales.Enabled = True
                    .rbPurchase.Enabled = False
                    .rbExpence.Enabled = False
                    .cmbShowIndex = 0
                Case 9 ' Othercost Credit A/c
                    .strMyCaption = "Select Creditor"
                    .rbSupplier.Checked = True
                    .strMyQry = AssignAccSQLStr(1, "", 2)
                    .bModify = True
                    .bAddnew = True
                    .rbPDCIssued.Enabled = False
                    .rbPDCRcvd.Enabled = False
                    .rbSales.Enabled = False
                    .rbPurchase.Enabled = False
                    .rbExpence.Enabled = False
                    .cmbShowIndex = 0
                Case 10  ' Othercost Debit A/c
                    .strMyCaption = "Select Debitor"
                    .rbExpence.Checked = True
                    .strMyQry = AssignAccSQLStr(4, "", 2)
                    .bModify = True
                    .bAddnew = True
                    .rbPDCIssued.Enabled = False
                    .rbPDCRcvd.Enabled = False
                    .rbSales.Enabled = False
                    .rbPurchase.Enabled = False
                    .rbExpence.Enabled = False
                    .cmbShowIndex = 0


                Case 11  ' Select Purchase Invoices
                    .strMyCaption = "Select Sales Invoices"
                    .pnlRadios.Visible = False
                    .strMyQry = "" '"SELECT * FROM (SELECT InvNo, TrDate,[Inv Amount]- Disc + Oth As [Net Amount], Disc As Discount, Oth As [Ttl Oth Cost], AccDescr As [Party Name],  TrRefNo As Reference, Alias As PartyId, TrDescription As Description,UserId, [Job Code], ItmInvCmnTb.TermsId, LPO, Trim(PreFix) & IIf(Trim(PreFix)= '', '','/') & InvNo As [No With PF], ItmInvCmnTb.TrId FROM (ItmInvCmnTb LEFT JOIN (SELECT TrId, Sum(UnitCost*TrQty) As [Inv Amount], Sum(UnitDiscount*TrQty) As Disc, Sum(UnitOthCost*TrQty) As Oth FROM ItmInvTrTb GROUP BY TrId) As Q1 ON Q1.TrId = ItmInvCmnTb.TrId) LEFT JOIN AccMast ON AccMast.AccountNo = ItmInvCmnTb.CSCode WHERE TrType = '" & tp & "'" & Switch(optP(0).Value, " AND TrDate > #" & Format(ProtectUntil, shortDtFmt) & "#", optP(1).Value, " AND TrDate <= #" & Format(ProtectUntil, shortDtFmt) & "#", optP(2).Value, "") & ") As Q ORDER BY InvNo DESC"
                    ' AssignAccSQLStr(4, "", 2)
                    .bModify = False
                    .bAddnew = False
                    .cmbShowIndex = 0
                Case 12 'Customer
                    .strMyCaption = "Select Patient"
                    .rbCustomer.Checked = True
                    .strMyQry = AssignAccSQLStr(22, "", 2)
                    .bModify = True
                    .bAddnew = True
                    ''DoEnaRadios(False, True, True, True, True, True, True, True, True, True)
                    .rbPDCIssued.Enabled = False
                    .rbPDCRcvd.Enabled = False
                    .rbSales.Enabled = False
                    .rbPurchase.Enabled = False
                    .rbExpence.Enabled = False
                    .cmbShowIndex = 3
                    .BVal = 1

                Case 15  ' RV cash,PDC,BANK A/c
                    .strMyCaption = "Select Account Name"
                    .rbExpence.Checked = True
                    .strMyQry = AssignAccSQLStr(15, "", 2)
                    .bModify = True
                    .bAddnew = True
                    .rbPDCIssued.Enabled = False
                    .rbPDCRcvd.Enabled = True
                    .rbSales.Enabled = False
                    .rbCashBank.Enabled = True
                    .rbPurchase.Enabled = False
                    .rbExpence.Enabled = False
                    .rbCustomer.Enabled = False
                    .rbCashBank.Checked = True
                    .cmbShowIndex = 0
                Case 16  ' PV cash,PDC,BANK A/c
                    .strMyCaption = "Select Account Name"
                    .rbExpence.Checked = True
                    .strMyQry = AssignAccSQLStr(16, "", 2)
                    .bModify = True
                    .bAddnew = True
                    .rbPDCIssued.Enabled = True
                    .rbPDCRcvd.Enabled = False
                    .rbSales.Enabled = False
                    .rbCashBank.Enabled = True
                    .rbPurchase.Enabled = False
                    .rbExpence.Enabled = False
                    .rbCustomer.Enabled = False
                    .rbCashBank.Checked = True
                    .cmbShowIndex = 0
                Case 55  ' Select FC
                    .strMyCaption = "Select Currency"
                    .pnlRadios.Visible = False
                    .strMyQry = "Select CurrencyCode,CurrencyRate,[Fraction Code],Description,[Decimal Places] from CurrencyTb" '"SELECT * FROM (SELECT InvNo, TrDate,[Inv Amount]- Disc + Oth As [Net Amount], Disc As Discount, Oth As [Ttl Oth Cost], AccDescr As [Party Name],  TrRefNo As Reference, Alias As PartyId, TrDescription As Description,UserId, [Job Code], ItmInvCmnTb.TermsId, LPO, Trim(PreFix) & IIf(Trim(PreFix)= '', '','/') & InvNo As [No With PF], ItmInvCmnTb.TrId FROM (ItmInvCmnTb LEFT JOIN (SELECT TrId, Sum(UnitCost*TrQty) As [Inv Amount], Sum(UnitDiscount*TrQty) As Disc, Sum(UnitOthCost*TrQty) As Oth FROM ItmInvTrTb GROUP BY TrId) As Q1 ON Q1.TrId = ItmInvCmnTb.TrId) LEFT JOIN AccMast ON AccMast.AccountNo = ItmInvCmnTb.CSCode WHERE TrType = '" & tp & "'" & Switch(optP(0).Value, " AND TrDate > #" & Format(ProtectUntil, shortDtFmt) & "#", optP(1).Value, " AND TrDate <= #" & Format(ProtectUntil, shortDtFmt) & "#", optP(2).Value, "") & ") As Q ORDER BY InvNo DESC"
                    ' AssignAccSQLStr(4, "", 2)
                    .bModify = False
                    .bAddnew = False
                    .cmbShowIndex = 0
                Case 66   ' Selelct Location
                    .strMyCaption = "Select Location "
                    .pnlRadios.Visible = False
                    .strMyQry = "SELECT LocationId as Code, Description, IsDefault FROM  LocationTb ORDER BY LocationId"
                    .bModify = False
                    .bAddnew = False
                    .cmbShowIndex = 0
                Case 88   ' Selelct Terms 
                    .strMyCaption = "Select Terms "
                    .pnlRadios.Visible = False
                    .strMyQry = "SELECT TermsID as Code, termsDescr as Description,nDays FROM TermsTb"
                    .bModify = False
                    .bAddnew = False
                    .cmbShowIndex = 0
                Case 99   ' Selelct Area

                    .strMyCaption = "Select Area "
                    .pnlRadios.Visible = False
                    .strMyQry = "SELECT AreaCode as [Area Code], AreaDescr as Name FROM AreaTb ORDER BY AreaCode"
                    .bModify = False
                    .bAddnew = False
                    .cmbShowIndex = 0
                Case 109   ' Selelct Salesman

                    .strMyCaption = "Select Salesman "
                    .pnlRadios.Visible = False
                    .strMyQry = "SELECT '' as lnk,SManCode as [Salesman Id]," & _
                        " SManName as [Salesman Name], Commission, Address1," & _
                        " Address2, Tel as Telephone FROM " & _
                        "SalesmanTb ORDER BY SManCode" '"SELECT AreaCode as [Area Code], AreaDescr as Name FROM AreaTb ORDER BY AreaCode"
                    .bModify = False
                    .bAddnew = False
                    .cmbShowIndex = 0
                Case 110  ' Select Cash customer
                    .strMyCaption = "Select Currency"
                    .pnlRadios.Visible = False
                    .strMyQry = "Select CustName [Customer Name], Add1 + ',' + Add2 [Address],Phone,email Email,discountvouchernumber [Card Number], from CashCustomerTb LEFT JOIN CarMasterTb ON CashCustomerTb.custid=CarMasterTb.customerid ORDER BY CustName"
                    .bModify = False
                    .bAddnew = False
                    .cmbShowIndex = 0
                Case 1000
                    .strMyCaption = "Select Account Name"
                    .rbExpence.Checked = True
                    .strMyQry = AssignAccSQLStr(8, "", 2)
                    .bModify = True
                    .bAddnew = True
                    .rbPDCIssued.Enabled = True
                    .rbPDCRcvd.Enabled = True
                    .rbSales.Enabled = False
                    .rbCashBank.Enabled = True
                    .rbPurchase.Enabled = False
                    .rbExpence.Enabled = False
                    .rbCustomer.Enabled = False
                    .rbCashBank.Checked = True
                    .rbAllAc.Checked = True
                    .cmbShowIndex = 0
            End Select
        End With
    End Sub
    Public Function AssignAccSQLStr(ByVal Index As Integer, Optional ByVal BrId As String = "", Optional ByVal BrHow As Byte = 2) As String    ', SelAccount As frmSelect)
        '0 - sp. Branch & Branch with empty, 1 - only sp. branch, 2 - all branches
        Dim str As String
        If BrId = "" Then
            BrId = UsrBr
        End If
        str = "SELECT AccMast.AccDescr As [AccountName],AccMast.Alias,BranchId,accid "
        Select Case Index

            Case 0, 1, 9, 10, 22
                If Index = 22 Then
                    str = "SELECT AccMast.AccDescr [Patient Name],AccMast.Alias [OP Number],Address1+Address2+Address3 [Address],Phone,BranchId,accid "
                    str = str & "FROM S1AccHd INNER JOIN AccMast ON S1AccHd.S1AccId = AccMast.S1AccId " & _
                        "LEFT JOIN AccMastAddr ON AccMastAddr.AccountNo=AccMast.accid " & _
                     "WHERE S1AccHd.GrpSetOn='Customer'" & IIf(BrId <> "", " AND isnull(AccMast.BranchId,'')  In (" & IIf(BrHow = 1, "", "'', ") & "'" & BrId & "')", "")
                Else
                    str = "SELECT AccMast.AccDescr As [AccountName],AccMast.Alias,Phone,BranchId,accid "
                    str = str & "FROM S1AccHd INNER JOIN AccMast ON S1AccHd.S1AccId = AccMast.S1AccId " & _
                    "LEFT JOIN AccMastAddr ON AccMastAddr.AccountNo=AccMast.accid " & _
                     "WHERE S1AccHd.GrpSetOn='" & _
                     Choose(Index + 1, "Customer", "Supplier", "", "", "", "", "", "", "", "P.D.C.(R)", "P.D.C.(I)") & "'" & IIf(BrId <> "", " AND isnull(AccMast.BranchId,'')  In (" & IIf(BrHow = 1, "", "'', ") & "'" & BrId & "')", "")
                End If
            Case 2, 6, 7
                'Both (Supplier ,Customer) and ('Cash','Bank')
                str = "SELECT AccMast.AccDescr As [AccountName],AccMast.Alias,isnull(Phone,'')Phone,BranchId,accid "
                str = str & "FROM S1AccHd INNER JOIN AccMast ON S1AccHd.S1AccId = AccMast.S1AccId " & _
                        "LEFT JOIN AccMastAddr ON AccMastAddr.AccountNo=AccMast.accid " & _
                      "WHERE S1AccHd.GrpSetOn In (" & _
                      Choose(Index - 1, "'Supplier','Customer'", "''", "''", "''", "'Cash','Bank'", "'Cash'") & ")" & IIf(BrId <> "", " AND isnull(AccMast.BranchId,'')  In (" & IIf(BrHow = 1, "", "'', ") & "'" & BrId & "')", "")
            Case 13
                'Cash
                str = str & "FROM S1AccHd INNER JOIN AccMast ON S1AccHd.S1AccId = AccMast.S1AccId " & _
                      "WHERE S1AccHd.GrpSetOn = 'Cash'" & IIf(BrId <> "", " AND isnull(AccMast.BranchId,'')  In (" & IIf(BrHow = 1, "", "'', ") & "'" & BrId & "')", "")
            Case 14
                'Bank
                str = str & "FROM S1AccHd INNER JOIN AccMast ON S1AccHd.S1AccId = AccMast.S1AccId " & _
                      "WHERE S1AccHd.GrpSetOn = 'Bank'" & IIf(BrId <> "", " AND isnull(AccMast.BranchId,'')  In (" & IIf(BrHow = 1, "", "'', ") & "'" & BrId & "')", "")
            Case 3, 4, 5
                'Sales,Purchases & Expenses
                str = str & "FROM AccMast " & _
                      "WHERE AccMast.S1AccId Between " & Choose(Index - 2, "4000 And 5999", _
                      "6000 And 6999", "7000 And 7999") & IIf(BrId <> "", " AND isnull(AccMast.BranchId,'')  In (" & IIf(BrHow = 1, "", "'', ") & "'" & BrId & "')", "")
            Case 11
                'G/L A/c. changed to Cash
                str = str & "FROM AccMast " & _
                      "WHERE AccMast.AccSetId <> ''" & IIf(BrId <> "", " AND isnull(AccMast.BranchId,'')  In (" & IIf(BrHow = 1, "", "'', ") & "'" & BrId & "')", "")
            Case 8
                'All A/c.
                str = str & "FROM AccMast " & _
                      IIf(BrId <> "", " WHERE isnull(AccMast.BranchId,'')  In (" & IIf(BrHow = 1, "", "'', ") & "'" & BrId & "')", "")
            Case 12
                'All except PDC
                str = str & "FROM S1AccHd INNER JOIN AccMast ON S1AccHd.S1AccId = AccMast.S1AccId " & _
                      "WHERE Not S1AccHd.GrpSetOn Like 'P.D.C.*'" & IIf(BrId <> "", " AND isnull(AccMast.BranchId,'')  In (" & IIf(BrHow = 1, "", "'', ") & "'" & BrId & "')", "")
            Case 15
                'Cash,Bank,P.D.C.(R)
                str = str & "FROM S1AccHd INNER JOIN AccMast ON S1AccHd.S1AccId = AccMast.S1AccId " & _
                      "WHERE S1AccHd.GrpSetOn = 'Cash' or S1AccHd.GrpSetOn = 'Bank' or S1AccHd.GrpSetOn ='P.D.C.(R)'" & IIf(BrId <> "", " AND isnull(AccMast.BranchId,'')  In (" & IIf(BrHow = 1, "", "'', ") & "'" & BrId & "')", "")
            Case 16
                'Cash,Bank,P.D.C.(I)
                str = str & "FROM S1AccHd INNER JOIN AccMast ON S1AccHd.S1AccId = AccMast.S1AccId " & _
                      "WHERE S1AccHd.GrpSetOn = 'Cash' or S1AccHd.GrpSetOn = 'Bank' or S1AccHd.GrpSetOn ='P.D.C.(I)'" & IIf(BrId <> "", " AND isnull(AccMast.BranchId,'')  In (" & IIf(BrHow = 1, "", "'', ") & "'" & BrId & "')", "")
            Case 17
                'Customer
                str = str & "FROM S1AccHd INNER JOIN AccMast ON S1AccHd.S1AccId = AccMast.S1AccId " & _
                      "WHERE S1AccHd.GrpSetOn In ('Customer')" & IIf(BrId <> "", " AND isnull(AccMast.BranchId,'')  In (" & IIf(BrHow = 1, "", "'', ") & "'" & BrId & "')", "")
            Case 18
                'Supplier
                str = str & "FROM S1AccHd INNER JOIN AccMast ON S1AccHd.S1AccId = AccMast.S1AccId " & _
                      "WHERE S1AccHd.GrpSetOn In ('Supplier')" & IIf(BrId <> "", " AND isnull(AccMast.BranchId,'')  In (" & IIf(BrHow = 1, "", "'', ") & "'" & BrId & "')", "")
            Case 19
                'Supplier,Cash,Customer
                str = str & "FROM S1AccHd INNER JOIN AccMast ON S1AccHd.S1AccId = AccMast.S1AccId " & _
                      "WHERE S1AccHd.GrpSetOn In ('Supplier','Customer','Cash')" & IIf(BrId <> "", " AND isnull(AccMast.BranchId,'')  In (" & IIf(BrHow = 1, "", "'', ") & "'" & BrId & "')", "")
            Case 20
                'Supplier,Cash,Customer
                str = "SELECT AccMast.AccDescr As [Vazhipadu],AccMast.Alias Code FROM S1AccHd INNER JOIN AccMast ON S1AccHd.S1AccId = AccMast.S1AccId " & _
                      "WHERE S1AccHd.GrpSetOn In ('Vazhipadu')"
            Case 21
                'Staff
                str = str & "FROM S1AccHd INNER JOIN AccMast ON S1AccHd.S1AccId = AccMast.S1AccId " & _
                      "WHERE S1AccHd.GrpSetOn In ('Staff')" & IIf(BrId <> "", " AND isnull(AccMast.BranchId,'')  In (" & IIf(BrHow = 1, "", "'', ") & "'" & BrId & "')", "")
        End Select
        AssignAccSQLStr = str
    End Function
    Public Function FormatString(ByVal value As String, ByVal format As String) As String
        Return Trim(value) & StrDup(Len(format) - Len(value), " ")
    End Function
    Public Function FormatStringLft(ByVal value As String, ByVal format As String) As String
        Return StrDup(Len(format) - Len(value), "0") & Trim(value)
    End Function
    'Public Sub LdPic(ByRef picLogo As PictureBox, ByVal filename As String, ByVal MeFrm As Form)
    '    Try
    '        If FileExists(filename) Then
    '            Dim bm As New Bitmap(filename)
    '            picLogo.BackgroundImage = bm
    '        Else
    '            'picLogo.BackgroundImage = MeFrm.Icon.ToBitmap
    '        End If
    '        picLogo.BackgroundImageLayout = ImageLayout.Stretch
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical)
    '    End Try

    'End Sub
    Public Sub LdPic(ByRef picUsedcar As PictureBox, ByVal filename As String, ByVal MeFrm As Form, Optional ByVal isImage As Boolean = False)
        Try
            If FileExists(filename) Then
                'Dim bm As New Bitmap(filename)
                'Dim imgTable As DataTable = imageTable()
                'Dim drow As DataRow
                'drow = imageTable.NewRow
                'Dim ms As New IO.MemoryStream(CType(filename, Byte())) 'This is correct...
                'Dim returnImage As Image = Image.FromStream(ms)
                Dim img As Image = Image.FromFile(filename)
                Dim bArr As Byte() = imgToByteArray(img)
                img.Dispose()
                img = Nothing
                Dim img1 As Image = byteArrayToImage(bArr)
                If isImage Then
                    picUsedcar.Image = img1
                Else
                    picUsedcar.BackgroundImage = img1
                End If

            Else
                picUsedcar.BackgroundImage = Nothing
                picUsedcar.Image = Nothing
            End If
            picUsedcar.BackgroundImageLayout = ImageLayout.Stretch
            picUsedcar.Visible = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try

    End Sub
    'convert image to bytearray
    Public Function imgToByteArray(ByVal img As Image) As Byte()
        Using mStream As New MemoryStream()
            img.Save(mStream, img.RawFormat)
            Return mStream.ToArray()
        End Using
    End Function
    'convert bytearray to image
    Public Function byteArrayToImage(ByVal byteArrayIn As Byte()) As Image
        Using mStream As New MemoryStream(byteArrayIn)
            Return Image.FromStream(mStream)
        End Using
    End Function

    Public Sub AddtoCombo(ByRef MyCombo As ComboBox, ByVal strSQL As String, Optional ByVal AddNull As Boolean = False, Optional ByVal IsAddItemData As Boolean = False)
        Dim dt As DataTable
        MyCombo.Items.Clear()
        dt = _objcmnbLayer._fldDatatable(strSQL)
        If dt.Rows.Count = 0 Then MyCombo.Items.Add("")
        If AddNull Then MyCombo.Items.Add("")
        For i = 0 To dt.Rows.Count - 1
            If IsAddItemData Then
                MyCombo.Items.Add(New myComboData(dt(i)(0), dt(i)(1)))
            Else
                MyCombo.Items.Add(New myComboData(dt(i)(0), 0))
            End If
        Next
    End Sub
    Public Sub AddDttoCombo(ByRef MyCombo As ComboBox, ByVal dt As DataTable, Optional ByVal AddNull As Boolean = False, Optional ByVal IsAddItemData As Boolean = False)
        MyCombo.Items.Clear()
        If dt.Rows.Count = 0 Then MyCombo.Items.Add("")
        If AddNull Then MyCombo.Items.Add("")
        For i = 0 To dt.Rows.Count - 1
            If IsAddItemData Then
                MyCombo.Items.Add(New myComboData(dt(i)(0), dt(i)(1)))
            Else
                MyCombo.Items.Add(New myComboData(Trim(dt(i)(0)), 0))
            End If
        Next
    End Sub
    Public Function MkDbSrchStr(ByVal s As String, Optional ByVal bTemp As Boolean = False) As String
        Dim i As Integer
        MkDbSrchStr = ""
        Dim Ch As String
        For i = 1 To Len(s)
            Ch = Mid(s, i, 1)
            If Ch = "'" Then
                MkDbSrchStr = MkDbSrchStr & "'"
            End If
            MkDbSrchStr = MkDbSrchStr & Ch
        Next i
    End Function
    Public Class myComboData
        Private ItemData As Long
        Private strText As String
        Public Sub New( _
           ByVal strTxt As String, _
           ByVal ItmData As Long)
            ItemData = ItmData
            strText = strTxt
        End Sub
        Public Property theData() As Long
            Get
                Return ItemData
            End Get
            Set(ByVal iValue As Long)
                ItemData = iValue
            End Set
        End Property
        Public Property theText() As String
            Get
                Return strText
            End Get
            Set(ByVal iValue As String)
                strText = iValue
            End Set
        End Property
        Public Overrides Function ToString() As String
            Return strText
        End Function
    End Class
    Public Function SearchGrid(ByVal _vDtable As DataTable, ByVal srchText As String, ByVal SrchIndex As Integer, Optional ByVal isContains As Boolean = False, Optional ByVal isfindMode As Boolean = False, Optional ByVal isSingle As Boolean = False) As DataTable
        Dim bDatatable As DataTable
        If _vDtable.Rows.Count = 0 Then bDatatable = _vDtable.Clone : GoTo nxt
        Dim _qurey As EnumerableRowCollection(Of DataRow)
        If isContains Then
            _qurey = From data In _vDtable.AsEnumerable() Where data(SrchIndex).ToString.ToUpper.Contains(UCase(srchText)) Select data
        ElseIf isfindMode Then
            _qurey = From data In _vDtable.AsEnumerable() Where Math.Round(CDbl(data(SrchIndex)), 2) - Math.Truncate(Math.Round(CDbl(data(SrchIndex)), 2)) > 0 Select data
        ElseIf isSingle Then
            _qurey = From data In _vDtable.AsEnumerable() Where data(SrchIndex) = srchText Select data
        Else
            _qurey = From data In _vDtable.AsEnumerable() Where data(SrchIndex).ToString.StartsWith(srchText, StringComparison.OrdinalIgnoreCase) Select data
        End If
        If _qurey.Count > 0 Then
            bDatatable = _qurey.CopyToDataTable()
        Else
            bDatatable = _vDtable.Clone
        End If
nxt:
        Return bDatatable
    End Function
    Public Function returnToReportTable(ByVal _vDtable As DataTable)
        Dim bDatatable As DataTable
        If _vDtable.Rows.Count = 0 Then bDatatable = _vDtable.Clone : GoTo nxt
        Dim _qurey As EnumerableRowCollection(Of DataRow)
        _qurey = From data In _vDtable.AsEnumerable() Select data
        If _qurey.Count > 0 Then
            bDatatable = _qurey.CopyToDataTable()
        Else
            bDatatable = _vDtable.Clone
        End If
nxt:
        Return bDatatable
    End Function
    Public Function SearchBatchWithItemid(ByVal _vDtable As DataTable, ByVal srchText As String, ByVal SrchIndex As Integer, ByVal itemid As Long, Optional ByVal isContains As Boolean = False) As DataTable
        Dim bDatatable As DataTable
        If _vDtable.Rows.Count = 0 Then bDatatable = _vDtable.Clone : GoTo nxt
        Dim _qurey As EnumerableRowCollection(Of DataRow)
        If isContains Then
            _qurey = From data In _vDtable.AsEnumerable() Where data(SrchIndex).ToString.ToUpper.Contains(UCase(srchText)) Select data
        Else
            _qurey = From data In _vDtable.AsEnumerable() Where data(SrchIndex).ToString.StartsWith(srchText, StringComparison.OrdinalIgnoreCase) And data("itemid") = itemid Select data
        End If
        If _qurey.Count > 0 Then
            bDatatable = _qurey.CopyToDataTable()
        Else
            bDatatable = _vDtable.Clone
        End If
nxt:
        Return bDatatable
    End Function
    Public Function SearchSequenceFromGrid(ByRef grd As DataGridView, ByVal columIndex As Integer, ByVal txtSeq As String, ByVal SelectPos As Integer) As String
        If Trim(txtSeq) = "" Then Return ""
        With grd
            For i = SelectPos To .RowCount - 1
                If Not IsDBNull(.Item(columIndex, i).Value) Then
                    If UCase(.Item(columIndex, i).Value).Contains(UCase(txtSeq)) Then
                        Return i
                    End If
                End If
            Next
        End With
        MsgBox("  Finished   ", MsgBoxStyle.Information, "Validation")
        Return "N"
    End Function
    Public Function chkDate(ByVal D As String) As Boolean
        On Error Resume Next
        Dim tDate As Date
        tDate = CDate(D)
        chkDate = True
        If Err.Number > 0 Then chkDate = False
    End Function
    Public Sub SetGridProperty(ByVal dgDataGrid As DataGridView)
        With dgDataGrid
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ReadOnly = True
            .EditMode = DataGridViewEditMode.EditProgrammatically
            .ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke
            .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular)
            .RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        End With

    End Sub
    Public Sub SetGridEditProperty(ByVal dgDataGrid As DataGridView)
        With dgDataGrid
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = True
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            .ReadOnly = False
            .EditMode = DataGridViewEditMode.EditProgrammatically
            .ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke
            .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular)
            .RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 9.0!)

            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        End With

    End Sub
    Public Sub setExtraPara(ByVal dt As DataTable, ByVal isskipproperty As Boolean)
        If Not isskipproperty Then SetSystemProperties()
        SetCompPara(dt)
        ChkSysDate()
        If enableGCC Then
            EnableGST = False
            enablecess = False
        End If
        ShowTaxOnInventory = enableGCC
    End Sub
    Public Function ChkSysDate() As Boolean
        If evaluateDate(DateSerial(1999, 12, 4)) Then
            If evaluateDate(Format(DateSerial(1999, 12, 4), DtFormat)) = False Then Exit Function
        Else
            Exit Function
        End If
        ChkSysDate = True
    End Function
    Private Function evaluateDate(ByVal strDt As String) As Boolean
        'Dim strDt As String
        Dim i As Byte
        Dim Pos1 As Byte
        Dim Pos2 As Byte
        Dim Sep(1) As String ' * 1
        Dim Ch As String '* 1
        Sep(0) = ""
        Sep(1) = ""
        For i = 1 To Len(strDt)
            Ch = Mid(strDt, i, 1)
            If Ch = "/" Or Ch = "-" Then
                If Sep(0) = "" Then
                    Sep(0) = Ch
                Else
                    Sep(1) = Ch
                End If
            End If
        Next i
        If Sep(0) = "" Or Sep(1) = "" Then
            MsgBox("Please set a proper system short date format.", vbInformation)
            Exit Function
        End If
        Dim MFmt As Boolean
        Dim DFmt As Boolean ' String * 2
        Dim YFmt As Boolean
        Pos1 = 1
        DtFormat = ""
        dtMask = ""
        dtEmpty = ""
        shortDtFmt = ""
        For i = 0 To 2
            If i < 2 Then Pos2 = InStr(Pos1, strDt, Sep(i))
            Select Case UCase(Trim(Mid(strDt, Pos1, Pos2 - Pos1)))
                Case "04", "4", "SAT", "SATURDAY"
                    DFmt = True
                    DtFormat = DtFormat & "dd"
                    dtMask = dtMask & "##"
                    dtEmpty = dtEmpty & "  "
                    shortDtFmt = shortDtFmt & "dd"
                Case "12", "DEC", "DECEMBER"
                    DtFormat = DtFormat & "MM"
                    dtMask = dtMask & "##"
                    dtEmpty = dtEmpty & "  "
                    shortDtFmt = shortDtFmt & "MMM"
                    MFmt = True
                Case "1999", "99", "1"
                    DtFormat = DtFormat & "yyyy"
                    dtMask = dtMask & "####"
                    dtEmpty = dtEmpty & "    "
                    shortDtFmt = shortDtFmt & "yyyy"
                    YFmt = True
                Case Else
                    MsgBox("Please set a proper system short date format.", vbInformation)
                    Exit Function
            End Select
            If i < 2 Then
                DtFormat = DtFormat & Sep(i)
                dtMask = dtMask & Sep(i)
                dtEmpty = dtEmpty & Sep(i)
                shortDtFmt = shortDtFmt & Sep(i)
                Pos1 = Pos2 + 1
                If i = 1 Then Pos2 = Len(strDt) + 1
            End If
        Next i
        DtFormatTime = DtFormat & " hh:mm:ss tt"
        If DFmt = False Or MFmt = False Or YFmt = False Then
            MsgBox("Please set a proper system short date format.", vbInformation)
            Exit Function
        End If
        evaluateDate = True
    End Function
    Public Sub SetCompPara(ByVal sRs As DataTable)
        If sRs Is Nothing Then
            sRs = _objcmnbLayer._fldDatatable("SELECT * FROM CompanyTb")
        End If
        If sRs.Rows.Count = 0 Then
            DateFrom = Date.Today
        Else
            If Not IsDBNull(sRs(0)("AccPeriodFrm")) Then
                DateFrom = sRs(0)("AccPeriodFrm")
            Else
                'DateFrom = dtEmpty
            End If
            If Not IsDBNull(sRs(0)("AccPeriodTo")) Then
                DateTo = sRs(0)("AccPeriodTo")
            Else
                'DateTo = dtEmpty
            End If
            If Dloc = "" Then
                Dloc = Trim(sRs(0)("DefLoc") & "")
            End If
            JLoc = Trim(sRs(0)("JobLoc") & "")
            'CCurrency = sRs(0)("BasicCurrencyId")
            NoOfDecimal = Val(sRs(0)("NoOfDecimal") & "")
            stateCode = Trim(sRs(0)("statecode") & "")
            SerialAlertDays = Val(sRs(0)("SerialAlertDays") & "")
            bartenderpath = Trim(sRs(0)("bartenderpath") & "")
            ISNextline = Val(sRs(0)("ISNextLineOn") & "")
            roundoffGtrThn50 = Val(sRs(0)("roundoffGtrThn50") & "")
            roundoffLessThn50 = Val(sRs(0)("roundoffLessThn50") & "")
            'PDCWarn = sRs(0)("WarnPDC")
            CostMethod = Val(sRs(0)("CostingMethod") & "")
            If Not IsDBNull(sRs(0)("withNonTaxBill")) Then
                withNonTaxBill = sRs(0)("withNonTaxBill")
            End If
            If Not IsDBNull(sRs(0)("EntrygridFontSize")) Then
                EntrygridFontSize = sRs(0)("EntrygridFontSize")
            End If
            If Not IsDBNull(sRs(0)("isDXB")) Then
                enableGCC = sRs(0)("isDXB")
            End If
            If NoOfDecimal = 0 Then
                numFormat = "#,##0.00"

            Else
                numFormat = "#,##0" & IIf(NoOfDecimal = 0, "", "." & Strings.StrDup(NoOfDecimal, "0"))
            End If

        End If
    End Sub


    Public Sub SetSystemProperties()
        enableRealtimeCosting = getSysPropVal("RCUP", False)
        enableServiceJob = getSysPropVal("SJ", False)
        enableAccounts = getSysPropVal("AC", False)
        enableInventory = getSysPropVal("ST", False)
        enableContractJob = getSysPropVal("CJ", False)
        enableDocuments = getSysPropVal("DC", False)
        enableSerialnumber = getSysPropVal("SN", False)
        ShowTaxOnInventory = getSysPropVal("ET", False)
        'ShowSerialnumberAlert = getSysPropVal("SNA", False)
        setTaxAsIncomeExpense = getSysPropVal("TIE", False)
        EnableGST = getSysPropVal("GST", False)
        EnableBarcode = getSysPropVal("EBAR", False)
        EnableWarranty = getSysPropVal("EWR", False)
        enableDuplicateBill = getSysPropVal("EDB", False)
        AllowUnitDiscountEntryOnInventory = getSysPropVal("IDS", False)
        enableItemwiseSalesman = getSysPropVal("ISSM", False)
        enableFuleBankInvoice = getSysPropVal("FB", False)
        enableTemple = getSysPropVal("TMPL", False)
        enableJobMaster = getSysPropVal("JB", False)
        enableProduction = getSysPropVal("MN", False)
        enableWebIntegration = getSysPropVal("WEB", False)
        enableNextlineonItemcode = getSysPropVal("ENI", False)
        enablePrintOnSave = getSysPropVal("EPS", False)
        enableWorkshop = getSysPropVal("WS", False)
        enableCarWash = getSysPropVal("CW", False)
        enableRestuarent = getSysPropVal("RS", False)
        enableMultipleDebitInInvoice = getSysPropVal("EMD", False)
        enableLodge = getSysPropVal("ENLD", False)
        enableWoodSale = getSysPropVal("ENWS", False)
        enableUserCodeOnPosLogin = getSysPropVal("OPC", False)
        enablePOS = getSysPropVal("ENPOS", False)
        If enablePOS Then
            enableMultipleDebitInInvoice = True
        End If
        enableNegativeQtyAlert = getSysPropVal("ENQ", False)
        enablecess = getSysPropVal("CESS", False)
        enableBatchwiseTr = getSysPropVal("ENB", False)
        enableDeliverywiseOutstanding = getSysPropVal("EDOT", False)
        enableVazhipaduSalesWithMutipleNamesAndItems = getSysPropVal("EVSWMN", False)
        enableSerialnumberWithoutPurchase = getSysPropVal("ESWOP", False)
        enableSMS = getSysPropVal("ENSMS", False)
        enablePayroll = getSysPropVal("ENP", False)
        enableAutoRoundOff = getSysPropVal("EAR", False)
        enableSalesmancompulsory = getSysPropVal("ENSMN", False)
        enableCostAccounting = getSysPropVal("ECACC", False)
        enableCreditPrice = getSysPropVal("ECP", False)
        enableMRPInStockIn = getSysPropVal("EMST", False)
        enableSP1InStockIn = getSysPropVal("ESP1ST", False)
        enableSP2InStockIn = getSysPropVal("ESP2ST", False)
        enableSP3InStockIn = getSysPropVal("ESP3ST", False)
        enableMRPinDocument = getSysPropVal("EMDOC", False)
        enableTaxinDocument = getSysPropVal("ETDOC", False)
        enablefetchLastPrice = getSysPropVal("FLP", False)
        enableFloodCess = getSysPropVal("EFC", False)
        enableItemAutoPopulate = getSysPropVal("EIAD", False)
        enableB2BAsDefault = getSysPropVal("EB2BD", False)
        disableShowAlert = getSysPropVal("DSAL", False)
        enableFOCQty = getSysPropVal("ENFOC", False)
        enablePhoneNumberMandatory = getSysPropVal("ENPHM", False)
        enableMultipleDebitAutoPopulate = getSysPropVal("EMDAP", False)
        enableAdvanceEntryInMultipleDebit = getSysPropVal("EADVMD", False)
        enableMultipleDebitAsCreditCollection = getSysPropVal("EMDC", False)
        enableMembership = getSysPropVal("EMSP", False)
        enableInvoiceTotalFromHistory = getSysPropVal("DINVT", False)
        enableClinic = getSysPropVal("ENCL", False)
        enableFocusOnQTYinPOS = getSysPropVal("EQF", False)
        enableMRPinSales = getSysPropVal("EMRP", False)
        enableExpiryDateInPOS = getSysPropVal("EEDP", False)
        LinkB2BWithWSPrice = getSysPropVal("LB2BWS", False)
        enableChurchModule = getSysPropVal("ENCH", False)
        enablelaundry = getSysPropVal("ENLA", False)
        enableBranch = getSysPropVal("ENBR", False)
        stockEffectInDeliveryNote = getSysPropVal("SED", False)
        disableEditProdectDescription = getSysPropVal("DIDIT", False)
        disableMRPFromProductSearch = getSysPropVal("DMRPIS", False)
        disableWSFromProductSearch = getSysPropVal("DWSIS", False)
        disablePriceEditInPos = getSysPropVal("DPEP", False)
        setSalespriceFromMRPinPruchase = getSysPropVal("SSMRP", False)
        EnableAlertBelowcost = getSysPropVal("EACOST", False)
        enablePrintOnRVSave = getSysPropVal("EPRV", False)
        enableAdjustDiscountOnTaxTotal = getSysPropVal("EUPD", False)
        EnableFinancialSales = getSysPropVal("EFS", False)
        EnableFruitsSales = getSysPropVal("FSLS", False)
        DisbleRepeateRv = getSysPropVal("DRRV", False)
        enableuserwisetransactionlist = getSysPropVal("UTL", False)
        enableTailoring = getSysPropVal("TLM", False)
        EnableUsedCar = getSysPropVal("EUCI", False)
        enableMicroFinace = getSysPropVal("EMF", False)
        enableMultiplePointsOnLineItem = getSysPropVal("EMPNTS", False)
        enableInstallmentInRV = getSysPropVal("IIRV", False)
        enableRVApproval = getSysPropVal("ERA", False)
        enableschoolmanagement = getSysPropVal("SCH", False)
        enableprofitanalysiswithreturn = getSysPropVal("EPWR", False)
        enableChooseInstallmentinRV = getSysPropVal("EIIRV", False)
        enableCoursemangementDXB = getSysPropVal("ECMD", False)
        enableSwimmingPool = getSysPropVal("ESW", False)
        enableGYM = getSysPropVal("EGY", False)
        enableRouteBulkSale = getSysPropVal("ERBS", False)
        'restuarent module
        enableSingleUserKOT = getSysRestVal("ESUKOT", False)

        If EnableGST Then
            ShowTaxOnInventory = False
        End If
        ShowTaxOnInventory = enableGCC
        'If enableWebIntegration Then getWebSettings()
    End Sub

    Public Function getWebSettings() As Boolean
        If Not HaveInternetConnection() Then Exit Function
        Dim dt As DataTable
        Dim _objcmn As New clsCommon_BL
        dt = _objcmn._fldDatatable("Select * from WebserverTb")
        If dt.Rows.Count > 0 Then
            webserver = Trim(dt(0)("webserver") & "")
            webusername = Trim(dt(0)("username") & "")
            webpassword = Trim(dt(0)("password") & "")
            webdbname = Trim(dt(0)("dbname") & "")
            webIntegrationid = Val(dt(0)("webIntegrationid") & "")
        End If
        Dim _objweb As New webDatalayer
        If webIntegrationid > 0 Then
            If _objweb.testconnection() Then
                dt = _objweb.returnDatatable("select compid from companysettings where isnull(status,0)=0 and compid=" & webIntegrationid)
                If dt.Rows.Count = 0 Then
                    'MsgBox("Web Integration has been deactivated! Please contact your vendor", MsgBoxStyle.Exclamation)
                    webpassword = ""
                    Return False
                Else
                    Return True
                End If
            End If
        End If
        Return False
    End Function
    Public Function HaveInternetConnection() As Boolean

        Try
            Return My.Computer.Network.Ping("www.google.com")
        Catch
            Return False
        End Try

    End Function
    Public Function createdtSerialNo() As DataTable
        Dim dtSerialNo As New DataTable
        With dtSerialNo
            .Columns.Add(New DataColumn("ItmSerialNo", GetType(String)))
            .Columns.Add(New DataColumn("Trid", GetType(Long)))
            .Columns.Add(New DataColumn("DetId", GetType(Long)))
            .Columns.Add(New DataColumn("Itemid", GetType(Long)))
            .Columns.Add(New DataColumn("RowIndex", GetType(Integer)))
            .Columns.Add(New DataColumn("Wdate", GetType(Date)))
        End With
        'dtSerialNo = _objcmnbLayer._fldDatatable("SELECT *,0 dtTbIndex from SerialNoTrTb where 1=2")
        dtSerialNo.Rows.Clear()
        Return dtSerialNo
    End Function
    Public Sub deleteDtSerialNo(ByRef dt As DataTable, ByVal srchText As String, ByVal itemid As Long)
        If dt Is Nothing Then Exit Sub
        Dim dtTbIndex As Integer
        Dim dtrow As DataRow
        If dt.Rows.Count = 0 Then Exit Sub
        If srchText = "" Or itemid = 0 Then Exit Sub
        dtrow = dt.Select("ItmSerialNo='" & srchText & "' and itemid=" & itemid)(0)
        dtTbIndex = dt.Rows.IndexOf(dtrow)
        dt.Rows.RemoveAt(dtTbIndex)
        'MsgBox(dtTbIndex)
    End Sub
    
    Private Function getSysRestVal(ByVal Key As String, Optional ByVal Dfault As Boolean = False) As Boolean
        Dim dtTable As DataTable
        'dtTable = _objcmnbLayer._fldDatatable("SELECT ProcessCode,isnull(isEnable,0) isEnable FROM RestSettingsTb WHERE ProcessCode = '" & Key & "'")
        Dim _qurey As EnumerableRowCollection(Of DataRow)
        _qurey = From data In dtSysRestVal.AsEnumerable() Where data("ProcessCode") = Key Select data
        If _qurey.Count > 0 Then
            dtTable = _qurey.CopyToDataTable()
        Else
            dtTable = dtSysRestVal.Clone
        End If
        If dtTable.Rows.Count > 0 Then
            getSysRestVal = dtTable(0)("isEnable")
        Else
            getSysRestVal = Dfault
        End If
    End Function
    Public Function getSysPropVal(ByVal Key As String, Optional ByVal Dfault As Boolean = False) As Boolean
        Dim dtTable As DataTable
        'dtTable = _objcmnbLayer._fldDatatable("SELECT ProcessCode,isnull(isEnable,0) isEnable FROM SysPara WHERE ProcessCode = '" & Key & "'")
        Dim _qurey As EnumerableRowCollection(Of DataRow)
        _qurey = From data In dtSysPropVal.AsEnumerable() Where data("ProcessCode") = Key Select data
        If _qurey.Count > 0 Then
            dtTable = _qurey.CopyToDataTable()
        Else
            dtTable = dtSysPropVal.Clone
        End If
        If dtTable.Rows.Count > 0 Then
            getSysPropVal = dtTable(0)("isEnable")
        Else
            getSysPropVal = Dfault
        End If
    End Function
    Public Sub ldMasterTables(ByVal txt As String, ByVal cmb As ComboBox)
        Dim dtTable As DataTable
        dtTable = _objcmnbLayer._fldDatatable(txt)
        cmb.DataSource = dtTable
    End Sub
    Public Sub SetFmlist(ByVal fMList As Mlistfrm, ByVal srchTxtId As Integer, Optional ByVal AccountNo As Long = 0)
        Select Case srchTxtId
            Case 1, 2
                fMList.strMyCaption = "Select Item Master "
                'fMList.strMyQry = "SELECT [Item Code] as Code,Barcode,Description,TrDescription,Model,Case When ItemId=BaseId then  QtyInHand Else QtyInHand* Vup/VDown  end as QIH,Case When ItemId=BaseId then  CostAverage Else CostAverage* Vup/VDown  end as Cost,Case When ItemId=BaseId then  LastPurchCost Else LastPurchCost* Vup/VDown  end as LPC,ItemId,BaseID FROM InvItm LEFT JOIN BaseItmDet ON InvItm.BaseId = BaseItmDet.BaseItemId "
                'fMList.strMyQry = "SELECT " & IIf(srchTxtId = 1, "[Item Code]", IIf(srchTxtId = 2, "Description", "Barcode")) & " FROM InvItm LEFT JOIN BaseItmDet ON InvItm.BaseId = BaseItmDet.BaseItemId "
                fMList.strMyQry = "select top 70 * from (SELECT  [Item Code] as Code,Description,((isnull(IGST,0)*UnitPrice)/100)+UnitPrice TaxPrice,itemid from InvItm " & _
                                    "LEFT JOIN GSTTb ON GSTTb.HSNCode=InvItm.HSNCode where isnull(ishide,0)=0) tr "
                fMList.isItemdata = True
                fMList.FillGrid()
                'fMList.Search("", IIf(srchTxtId = 1, "[Item Code]", "Description"))
                'FillSearchGrid(IIf(srchTxtId = 1, "[Item Code]", "Barcode"), strGridSrchString, True)
            Case 3, 4 'Customer,Customer name
                fMList.strMyCaption = "Select Customer/Supplier Account"
                fMList.strMyQry = AssignAccSQLStr(2, UsrBr, 2)
                fMList.FillGrid()
            Case 5, 6  'PurchAlias,PurchName
                fMList.strMyCaption = "Select Sales Account"
                fMList.strMyQry = AssignAccSQLStr(4, UsrBr, 2)
                fMList.FillGrid()
            Case 7  'FC
                fMList.strMyCaption = "Select Currency "
                fMList.strMyQry = "SELECT CurrencyCode,CurrencyRate , Description, [Fraction Code] AS FractionCode, [Decimal Places] as NoOfDecimal FROM CurrencyTb ORDER BY CurrencyCode"
                fMList.FillGrid()
            Case 8  'JOB
                fMList.isItemdata = True
                fMList.strMyCaption = "Select Job "
                fMList.strMyQry = "SELECT jobcode as Code,jobname as Name,JobId from JobTb Order by jobId "
                fMList.FillGrid()
            Case 9 'Location
                fMList.strMyCaption = "Select Location "
                fMList.strMyQry = "SELECT LocationId as Code," & _
                                    " Description FROM " & _
                                    " LocationTb ORDER BY LocationId"
                fMList.FillGrid()
            Case 10  'Terms
                fMList.strMyCaption = "Select Terms "
                fMList.strMyQry = "SELECT TermsId as [Terms Id], TermsDescr as Description, NDays FROM TermsTb ORDER BY TermsId"
                fMList.FillGrid()
            Case 11 'Area
                fMList.strMyCaption = "Select Area "
                fMList.strMyQry = "SELECT  AreaCode as [Area Code], AreaDescr as Name FROM AreaTb ORDER BY AreaCode"
                fMList.FillGrid()
            Case 12 'Salesman
                fMList.strMyCaption = "Select Salesman "
                fMList.strMyQry = "SELECT " & _
                    " SManName as [Name],SManCode as [Code] FROM " & _
                    "SalesmanTb ORDER BY SManCode"
                fMList.FillGrid()
            Case 13 'for all account
                fMList.strMyCaption = "Select All Account"
                fMList.strMyQry = AssignAccSQLStr(8, UsrBr, 2)
                fMList.FillGrid()
            Case 14 'Cash
                fMList.strMyCaption = "Select Cash Account"
                fMList.strMyQry = AssignAccSQLStr(13, UsrBr, 2)
                fMList.FillGrid()
            Case 15 'Bank
                fMList.strMyCaption = "Select Bank Account"
                fMList.strMyQry = AssignAccSQLStr(14, UsrBr, 2)
                fMList.FillGrid()
            Case 17 'Vessel
                fMList.strMyCaption = "Select Vessel"
                fMList.strMyQry = "Select VesselId as [Vessel Code],Description as [Vessel Name] FROM VesselesTb WHERE AccountNo=" & AccountNo
                fMList.FillGrid()
            Case 18  'Department
                fMList.strMyCaption = "Select Department "
                fMList.strMyQry = "SELECT DeptId as [Dep Id], DeptDescr as Description FROM DepartmentTb ORDER BY MasterOnly"
                fMList.FillGrid()
            Case 19  'Paid to from Account Cmn
                fMList.strMyCaption = "Select Paid To.. "
                fMList.strMyQry = "SELECT   PaidTo  [PAID TO] FROM AccTrCmn WHERE JVtypeno=102 GROUP BY PaidTo"
                fMList.isSingle = True
                fMList.FillGrid()
            Case 20  'Supplier
                fMList.strMyCaption = "Select Supplier Account"
                fMList.strMyQry = AssignAccSQLStr(18, UsrBr, 2)
                fMList.FillGrid()
            Case 21  'Customer
                fMList.strMyCaption = "Select Customer Account"
                fMList.strMyQry = AssignAccSQLStr(17, UsrBr, 2)
                fMList.FillGrid()
            Case 22  'JOB for sub job
                fMList.isItemdata = True
                fMList.strMyCaption = "Select Job "
                fMList.strMyQry = "SELECT jobcode as Code,jobname as Name,JobTb.JobId from JobTb " & _
                                "left join ConstructionTb on  JobTb.jobid=ConstructionTb.jobid where isnull(Pjobid,0)=0 and JobTb.JobId<>" & JobidForConstruct & " Order by jobId "
                fMList.FillGrid()
            Case 23  'Customer,Supplier,Cash
                fMList.strMyCaption = "Select Customer Account"
                fMList.strMyQry = AssignAccSQLStr(19, UsrBr, 2)
                fMList.FillGrid()
            Case 24
                fMList.isItemdata = True
                fMList.strMyCaption = "Select Star "
                fMList.strMyQry = "SELECT Starname,StarNameMal,starid from StarTb"
                fMList.FillGrid()
            Case 25
                'fMList.isItemdata = True
                'fMList.isSingle = True
                fMList.resizecolum = 1
                fMList.strMyCaption = "Select Card "
                fMList.strMyQry = "SELECT cardnumber Card,CustName [Customer Name] from CardmasterTb LEFT JOIN CashCustomerTb ON CardmasterTb.customerid =CashCustomerTb.custid"
                fMList.FillGrid()
            Case 26, 31 ' item codes
                fMList.resizecolum = 1
                If (srchTxtId = 26) Then
                    fMList.strMyCaption = "Select Item Code "

                Else
                    fMList.strMyCaption = "Select Item Name "

                End If
                fMList.strMyQry = "SELECT [Item Code],Description from invitm"

                fMList.FillGrid()
            Case 27  'Vazhipadu
                fMList.strMyCaption = "Select Vazhipadu"
                fMList.strMyQry = AssignAccSQLStr(20, "", 2)
                fMList.istemple = True
                fMList.FillGrid()
            Case 28  'Staff
                fMList.strMyCaption = "Select Staff Account"
                fMList.strMyQry = AssignAccSQLStr(21, UsrBr, 2)
                fMList.FillGrid()
            Case 29 ' car master
                fMList.Width = fMList.Width - 50
                fMList.resizecolum = 1
                fMList.strMyCaption = "Select Car master "
                fMList.strMyQry = "SELECT platenumber [Plate Number],cartype [Car Name],carid from CarMasterTb"
                fMList.FillGrid()
                fMList.dvData.Columns(0).Width = 150
                fMList.dvData.Columns(2).Visible = False
            Case 30 ' cash customer
                fMList.Width = fMList.Width - 50
                fMList.resizecolum = 0
                fMList.strMyCaption = "Select cash customer "
                fMList.strMyQry = "SELECT CustName [Customer Name],Phone [Phone Number],custid from CashCustomerTb where isnull(issupp,0)=0"
                fMList.FillGrid()
                'fMList.dvData.Columns(0).Width = 125
                fMList.dvData.Columns(1).Width = 125
                fMList.dvData.Columns(2).Visible = False
            Case 32 'subject from document
                fMList.resizecolum = 0
                fMList.strMyCaption = "Select Subject "
                fMList.strMyQry = "SELECT Subject  from DocCmnTb group by Subject"
                fMList.FillGrid()
            Case 33 ' cash Supplier
                fMList.Width = fMList.Width - 50
                fMList.resizecolum = 1
                fMList.strMyCaption = "Select cash Supplier "
                fMList.strMyQry = "SELECT Phone [Phone Number],CustName [Supplier Name],custid from CashCustomerTb where isnull(issupp,0)=1"
                fMList.FillGrid()
                fMList.dvData.Columns(0).Width = 125
                'fMList.dvData.Columns(1).Width = 150
                fMList.dvData.Columns(2).Visible = False
            Case 34 'Patient
                fMList.strMyCaption = "Select Patient"
                fMList.strMyQry = AssignAccSQLStr(22, UsrBr, 2)
                fMList.FillGrid()
            Case 35 'Church membership
                fMList.Width = fMList.Width - 50
                fMList.resizecolum = 0
                fMList.strMyCaption = "Select Member "
                fMList.strMyQry = "SELECT MemberName [Member Name],MemberCode Code,housename [House Name], memberid from TempleMembershipTb"
                fMList.FillGrid()
                fMList.dvData.Columns(1).Width = 125
                fMList.dvData.Columns(2).Width = 125
                fMList.dvData.Columns(3).Visible = False
            Case 36  'Sales,Purchases & Expenses
                fMList.strMyCaption = "Select Customer Account"
                fMList.strMyQry = AssignAccSQLStr(5, UsrBr, 2)
                fMList.FillGrid()
            Case 37 'membership for attendance
                fMList.Width = fMList.Width - 50
                fMList.resizecolum = 0
                fMList.strMyCaption = "Select Customer "
                fMList.strMyQry = "select * from (select AccDescr, Phone,jobcode,isnull(AccDescr,'')+isnull(Phone,'')+isnull(jobcode,'') searchtext,accid " & _
                                    "from jobtb left join accmast on jobtb.custcode=accmast.accid " & _
                                    "left join accmastaddr on accmast.accid =accmastaddr.accountno   " & _
                                    "inner join (select trdate,TrRefNo,[Job Code] invjobcode,[Item Code],InvItm.Description ,WarrentyExpDate enddate, " & _
                                    "UnitCost+isnull(taxamt,0)price,units,id invdetid from ItmInvTrTb " & _
                                    "inner join ItmInvCmnTb on ItmInvCmnTb.TrId=ItmInvTrTb.TrId " & _
                                    "left join InvItm on invitm.ItemId=ItmInvTrTb.ItemId) tr on JobTb.jobcode=tr.invjobcode " & _
                                    "where  enddate>=convert(date,getdate())) tr "
                fMList.FillGrid()
                fMList.dvData.Columns(0).Width = 125
                fMList.dvData.Columns(1).Width = 125
                fMList.dvData.Columns(2).Width = 125
                fMList.dvData.Columns(3).Visible = False
                fMList.dvData.Columns(4).Visible = False
        End Select
    End Sub
    Public Function getItmDtls(ByVal FldFor As Byte, ByVal fldVal As String, Optional ByVal AttachBaseDet As Boolean = False, Optional ByVal dtItem As DataTable = Nothing, Optional ByVal returnFromLinq As Boolean = False) As DataTable
        'FldId : 0 - ItemId, 1 - Item Code, 2 - Barcode, 3 - both itemcode and barcode
        Dim Condition As String
        Select Case FldFor   '1 for itemcode 2 for barcode 3 for both and 0 for itemid
            Case 1
                Condition = " [Item Code] = '" & MkDbSrchStr(fldVal) & "'"
                'Case 2
                '    Condition = " BarCode = '" & MkDbSrchStr(fldVal) & "'"
            Case 3
                Condition = " [Item Code] = '" & MkDbSrchStr(fldVal) & "'"
            Case 4
                Condition = " mechineItemcode = '" & MkDbSrchStr(fldVal) & "'"
            Case Else
                Condition = " invitm.ItemId = " & Val(fldVal)
                'Case Else ' for whole Item
                '    Condition = " BaseId = " & Val(fldVal)
        End Select

        Dim strQry As String
        If returnFromLinq Then
            Dim dtTable As DataTable
            Dim _qurey As EnumerableRowCollection(Of DataRow)
            _qurey = From data In dtItem.AsEnumerable() Where data("Item Code") = fldVal Select data
            If _qurey.Count > 0 Then
                dtTable = _qurey.CopyToDataTable()
            Else
                dtTable = dtItem.Clone
            End If
            Return dtTable
        End If
        strQry = "SELECT InvItm.*, FraCount,isnull(vat,0)vat,isSerialNo,isDuealSerialNo,ismanufacturing," & _
                "isnull(collectionAC,0)collectionAC,isnull(paymentAC,0)paymentAC,isnull(additionalcess,0)additionalcess," & _
                "isnull(rgcess,0)rgcess,isnull(rgccollectionac,0)rgccollectionac,isnull(rgcpeymentacc,0)rgcpeymentacc," & _
                "isnull(LastPurchCost,0)LastPurchCost,CGST,SGST,IGST,isnull(POSCount,0)POSCount FROM " & _
                "InvItm LEFT JOIN UnitsTb ON UnitsTb.Units = InvItm.Unit" & _
                " LEFT JOIN VatMasterTb ON InvItm.vatid=VatMasterTb.vatid " & _
                " LEFT JOIN (select vat rgcess,isnull(collectionAC,0) rgccollectionac,isnull(paymentAC,0) rgcpeymentacc,vatid rgcessid from VatMasterTb) regularcess ON InvItm.regularcessid=regularcess.rgcessid " & _
                "LEFT JOIN InvItmPropertiesTb ON InvItmPropertiesTb.Itemid=invitm.itemid " & _
                " left join GSTTb on GSTTb.hsncode=invitm.hsncode " & _
                 " WHERE " & Condition
        Return _objcmnbLayer._fldDatatable(strQry)
    End Function
    Public Function ItmValidation(ByVal FldFor As Byte, ByVal fldVal As String, ByVal isPriceAccwise As Boolean, ByVal trtype As String, Optional ByVal accid As Long = 0) As DataTable
        'FldId : 0 - ItemId, 1 - Item Code, 2 - Barcode, 3 - both itemcode and barcode
        Dim Condition As String
        Select Case FldFor   '1 for itemcode 2 for barcode 3 for both and 0 for itemid
            Case 1
                Condition = " [Item Code] = '" & MkDbSrchStr(fldVal) & "'"
                'Case 2
                '    Condition = " BarCode = '" & MkDbSrchStr(fldVal) & "'"
            Case 3
                Condition = " [Item Code] = '" & MkDbSrchStr(fldVal) & "' or mechineItemcode = '" & MkDbSrchStr(fldVal) & "'"
            Case 4
                Condition = " mechineItemcode = '" & MkDbSrchStr(fldVal) & "'"
            Case Else
                Condition = " invitm.ItemId = " & Val(fldVal)
                'Case Else ' for whole Item
                '    Condition = " BaseId = " & Val(fldVal)
        End Select

        Dim strQry As String
        Dim lastprice As String
        Dim branchstr As String
        branchstr = " LEFT JOIN (SELECT locQIH,itemid litemid,lastcost,locationCost from LocOpnQtyTb " & _
                        "left join LocationTb on LocationTb.LocationID=LocOpnQtyTb.LocationID " & _
                        "where LocCode='" & Dloc & "')LocationTb on InvItm.itemid=LocationTb.litemid "

        lastprice = " LEFT JOIN (SELECT UnitCost lastPrice,itemid from ItmInvTrTb " & _
                    "inner join (select itemid itid,max(id) costid from ItmInvTrTb " & _
                    "left join ItmInvCmnTb on ItmInvCmnTb.trid=ItmInvTrTb.trid " & _
                    "WHERE Trtype='" & trtype & "'" & IIf(isPriceAccwise, "AND CSCode=" & accid, "") & "  group by itemid) tr on ItmInvTrTb.id=tr.costid) lastprice ON InvItm.Itemid=lastprice.itemid "

        strQry = "SELECT InvItm.*, FraCount,isnull(vat,0)vat,isSerialNo,isDuealSerialNo,ismanufacturing," & _
                "isnull(collectionAC,0)collectionAC,isnull(paymentAC,0)paymentAC,isnull(additionalcess,0)additionalcess," & _
                "isnull(rgcess,0)rgcess,isnull(rgccollectionac,0)rgccollectionac,isnull(rgcpeymentacc,0)rgcpeymentacc," & _
                "CGST,SGST,IGST,isnull(discount,0)discount,isnull(lastPrice,0)lastPrice,isnull(locationCost,0)locationCost,isnull(lastcost,0) locationLastCost FROM " & _
                "InvItm " & branchstr & " LEFT JOIN UnitsTb ON UnitsTb.Units = InvItm.Unit" & _
                " LEFT JOIN VatMasterTb ON InvItm.vatid=VatMasterTb.vatid " & _
                " LEFT JOIN (select vat rgcess,isnull(collectionAC,0) rgccollectionac,isnull(paymentAC,0) rgcpeymentacc,vatid rgcessid from VatMasterTb) regularcess ON InvItm.regularcessid=regularcess.rgcessid " & _
                "LEFT JOIN InvItmPropertiesTb ON InvItmPropertiesTb.Itemid=invitm.itemid " & _
                " left join GSTTb on GSTTb.hsncode=invitm.hsncode " & _
                lastprice & _
                " left join (Select discount,Itemid DiscItemid from CustomerWisePriceDiscountTb where customerid=" & accid & ") custDISC on Invitm.itemid=custDISC.DiscItemid " & _
                 " WHERE " & Condition

        Return _objcmnbLayer._fldDatatable(strQry)
    End Function
    Public Function returnAllItem() As DataTable
        Dim strQry As String
        strQry = "SELECT InvItm.*, FraCount,isnull(vat,0)vat,isSerialNo,isDuealSerialNo,ismanufacturing," & _
                "isnull(collectionAC,0)collectionAC,isnull(paymentAC,0)paymentAC,isnull(additionalcess,0)additionalcess," & _
                "isnull(rgcess,0)rgcess,isnull(rgccollectionac,0)rgccollectionac,isnull(rgcpeymentacc,0)rgcpeymentacc," & _
                "isnull(LastPurchCost,0)LastPurchCost,CGST,SGST,IGST FROM " & _
                "InvItm LEFT JOIN UnitsTb ON UnitsTb.Units = InvItm.Unit" & _
                " LEFT JOIN VatMasterTb ON InvItm.vatid=VatMasterTb.vatid " & _
                " LEFT JOIN (select vat rgcess,isnull(collectionAC,0) rgccollectionac,isnull(paymentAC,0) rgcpeymentacc,vatid rgcessid from VatMasterTb) regularcess ON InvItm.regularcessid=regularcess.rgcessid " & _
                "LEFT JOIN InvItmPropertiesTb ON InvItmPropertiesTb.Itemid=invitm.itemid " & _
                " left join GSTTb on GSTTb.hsncode=invitm.hsncode where isnull(ishide,0)=0"
        Return _objcmnbLayer._fldDatatable(strQry)
    End Function
    Public Sub SearchJobPanel(ByVal dvData As DataGridView, ByVal Fld As String, ByVal FldValue As String, ByVal WithPack As Boolean)
        Dim strMyQry As String
        strMyQry = ""
        strMyQry = "SELECT jobcode,jobname,Jobid FROM JobTb where " & Fld & " Like '" & FldValue & "%' ORDER BY " & Fld
        dvData.DataSource = Nothing
        _vSrchdatatable = _objcmnbLayer._fldDatatable(strMyQry, False)
        dvData.DataSource = _vSrchdatatable
        With dvData
            SetGridProperty(dvData)
            .ColumnHeadersHeight = 100
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersDefaultCellStyle.BackColor = Color.Olive
            .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Verdana", 8.5!)
            .Columns(0).HeaderText = "Job Code"
            .Columns(0).Width = 150

            .Columns(1).HeaderText = "[Job Name]"
            .Columns(1).Width = 240

            .Columns(2).HeaderText = "ID"
            .Columns(2).Visible = False
        End With
    End Sub
    Public Sub SearchStarPanel(ByVal dvData As DataGridView, ByVal Fld As String, ByVal FldValue As String)
        Dim strMyQry As String
        strMyQry = ""
        'if isacc=0  then selected item from the invetory otherwise accountmaster
        strMyQry = "SELECT starid [Code],starname Star,starnamemal Malayalam FROM startb where " & Fld & " Like '" & FldValue & "%' ORDER BY " & Fld
        dvData.DataSource = Nothing
        _vSrchdatatable = _objcmnbLayer._fldDatatable(strMyQry, False)
        dvData.DataSource = _vSrchdatatable
SetHeadOnly:
        With dvData
            SetGridProperty(dvData)

            .ColumnHeadersHeight = 100
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersDefaultCellStyle.BackColor = Color.Olive
            .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Verdana", 8.5!)
            .Columns(0).Width = 150
            .Columns(1).Width = 150
            .Columns(2).Width = 150

        End With
    End Sub
    Public Sub SearchItmAccPanel(ByVal dvData As DataGridView, ByVal Fld As String, ByVal FldValue As String, ByVal WithPack As Boolean)
        Dim strMyQry As String
        strMyQry = ""
        'if isacc=0  then selected item from the invetory otherwise accountmaster
        strMyQry = "SELECT * FROM (SELECT [Item Code],Description,'' Malayalam,UnitPrice as Price,QIH, CostAvg Cost,ItemId,0 isacc FROM InvItm UNION ALL " & _
                    "SELECT Alias, AccDescr,isnull(nameMalayalam,''),VazhipaduRate,0,0,AccId,1 FROM AccMast left join S1AccHd on S1AccHd.S1AccId=AccMast.S1AccId " & _
                    "where GrpSetOn='Vazhipadu') Itm where " & Fld & " Like '" & FldValue & "%' ORDER BY " & Fld
        dvData.DataSource = Nothing
        _vSrchdatatable = _objcmnbLayer._fldDatatable(strMyQry, False)
        dvData.DataSource = _vSrchdatatable
SetHeadOnly:
        With dvData
            SetGridProperty(dvData)

            .ColumnHeadersHeight = 100
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersDefaultCellStyle.BackColor = Color.Olive
            .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Verdana", 8.5!)
            .Columns(0).HeaderText = "Item Code"
            .Columns(0).Width = 150

            .Columns(1).HeaderText = "Description"
            .Columns(1).Width = 240

            .Columns(3).HeaderText = "Price"
            .Columns(3).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(3).Visible = True

            .Columns(4).HeaderText = "QIH"
            .Columns(4).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(4).Width = 50
            .Columns(4).Visible = False

            .Columns(5).HeaderText = "Cost"
            .Columns(5).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(5).Visible = False

            .Columns(6).HeaderText = "ItemID"
            .Columns(6).Visible = False
            .Columns(7).Visible = False

        End With
    End Sub

    Public Sub searchProductBatch(ByVal dvData As DataGridView, ByVal txtsearch As String, ByVal itemid As Long, ByVal isrefreshData As Boolean, Optional ByVal ispos As Boolean = False, Optional ByVal searchindex As Integer = 2)
        dvData.DataSource = Nothing
        _objItmMast = New clsItemMast_BL
        If dtProductBatch Is Nothing Or isrefreshData Then
            'dtProductBatch = _objcmnbLayer._fldDatatable("returnBatchItems", True)
            dtProductBatch = _objItmMast.returnBatchItems(itemid)
        End If
        dvData.DataSource = SearchBatchWithItemid(dtProductBatch, txtsearch, searchindex, itemid, False)
SetHeadOnly:
        With dvData
            SetGridProperty(dvData)

            .ColumnHeadersHeight = 100
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersDefaultCellStyle.BackColor = Color.Olive
            .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Verdana", 8.5!)
            .Columns("invno").HeaderText = "Type"
            .Columns("invno").Width = 50
            .Columns("invno").Visible = Not ISPOS

            .Columns("BatchQty").HeaderText = "Qty"
            .Columns("BatchQty").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("BatchQty").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("BatchQty").Visible = True
            .Columns("BatchQty").Width = 50

            .Columns("batchno").HeaderText = "Batch No."
            .Columns("batchno").Width = 100

            .Columns("manufacturingdate").HeaderText = "M Date"
            .Columns("manufacturingdate").Width = 100
            .Columns("manufacturingdate").Visible = Not ISPOS

            .Columns("WarrentyExpDate").HeaderText = "Ex. Date"
            .Columns("WarrentyExpDate").Width = 100
            .Columns("WarrentyExpDate").Visible = Not ISPOS

            .Columns("MRP").HeaderText = "MRP"
            .Columns("MRP").Width = 70
            .Columns("MRP").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("MRP").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns("SP1").HeaderText = "Unit Price"
            .Columns("SP1").Width = 70
            .Columns("SP1").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("SP1").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns("SP2").HeaderText = IIf(enableCreditPrice, "Cr. Price", "D. Price")
            .Columns("SP2").Width = 70
            .Columns("SP2").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("SP2").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns("SP3").HeaderText = "WS Price"
            .Columns("SP3").Width = 70
            .Columns("SP3").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("SP3").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("SP3").Visible = Not ISPOS


            .Columns("ItemId").HeaderText = "ItemID"
            .Columns("ItemId").Visible = False
            .Columns("itmcost").Visible = False
            '.Columns("invno").Visible = False

        End With
        'resizeGridColumn(dvData, 2)
    End Sub
    Public Sub refreshItemTable(ByVal Fld As String, ByVal FldValue As String, Optional ByVal WithPack As Boolean = False, Optional ByVal isproduction As Boolean = False, Optional ByVal hideroom As Boolean = False, Optional ByVal category As String = "")
        Dim strMyQry As String
        strMyQry = ""
        Dim branchstr As String = ""
        If UsrBr <> "" Then
            branchstr = " LEFT JOIN (SELECT locQIH,itemid,lastcost,locationCost from LocOpnQtyTb " & _
                        "left join LocationTb on LocationTb.LocationID=LocOpnQtyTb.LocationID " & _
                        "where LocCode='" & Dloc & "')LocationTb on InvItm.itemid=LocationTb.itemid "
        End If

        If Not isproduction Then
            If category = "" Then
                strMyQry = "SELECT [Item Code] as Code,Description,UnitPrice as Price," & _
                            "MRP,(((isnull(IGST,0)+ISNULL(vat,0))*UnitPrice)/100)+UnitPrice [Tax Price]," & _
                            "UnitPriceWS WSP," & _
                            "((isnull(IGST,1)*CostAvg)/100)+CostAvg [Cost+Tax]," & _
                            IIf(UsrBr = "", "QIH", "isnull(locQIH,0)") & " QIH," & _
                            IIf(UsrBr = "", "CostAvg", "case when isnull(locationCost,0)=0 then CostAvg else isnull(locationCost,0) end") & " Cost, " & _
                            IIf(UsrBr = "", "LastPurchCost", "case when isnull(lastcost,0)=0 then LastPurchCost else isnull(lastcost,0) end") & " LPCost,InvItm.ItemId,ROW_NUMBER() OVER(ORDER BY [" & Fld & "] ASC) AS Rownum FROM InvItm " & _
                            " LEFT JOIN GSTTb ON GSTTb.HSNCode=InvItm.HSNCode " & _
                            " LEFT JOIN VatMasterTb ON VatMasterTb.vatid=InvItm.vatid " & branchstr & _
                            "where isnull(ishide,0)=0 and [" & Fld & "] Like '" & FldValue & "%'" & IIf(hideroom, " AND itemCategory in ('stock','service') ", "") & _
                            " ORDER BY [" & Fld & "]"
            Else
                strMyQry = "SELECT  [Item Code] as Code,Description,UnitPrice as Price," & _
                            "MRP,(((isnull(IGST,0)+ISNULL(vat,0))*UnitPrice)/100)+UnitPrice [Tax Price]," & _
                             "UnitPriceWS WSP," & _
                             "((isnull(IGST,1)*CostAvg)/100)+CostAvg [Cost+Tax]," & _
                            IIf(UsrBr = "", "QIH", "isnull(locQIH,0)") & " QIH," & _
                            IIf(UsrBr = "", "CostAvg", "case when isnull(locationCost,0)=0 then CostAvg else isnull(locationCost,0) end") & " Cost, " & _
                            IIf(UsrBr = "", "LastPurchCost", "case when isnull(lastcost,0) then LastPurchCost else isnull(lastcost,0) end") & " LPCost,InvItm.ItemId,ROW_NUMBER() OVER(ORDER BY [" & Fld & "] ASC) AS Rownum FROM InvItm " & _
                             " LEFT JOIN GSTTb ON GSTTb.HSNCode=InvItm.HSNCode " & _
                             " LEFT JOIN VatMasterTb ON VatMasterTb.vatid=InvItm.vatid " & branchstr & _
                            "where isnull(ishide,0)=0 AND itemCategory ='" & category & "'" & _
                            " ORDER BY [" & Fld & "]"

                'strMyQry = "SELECT [Item Code] as Code,Description,UnitPrice as Price," & _
                '           "MRP,(((isnull(IGST,0)+ISNULL(vat,0))*UnitPrice)/100)+UnitPrice [Tax Price]," & _
                '            "UnitPriceWS WSP," & _
                '            "((isnull(IGST,1)*CostAvg)/100)+CostAvg [Cost+Tax]," & _
                '           "QIH, CostAvg Cost," & IIf(UsrBr = "", "LastPurchCost", "isnull(lastcost,0)") & " LPCost,ItemId FROM InvItm " & _
                '            " LEFT JOIN GSTTb ON GSTTb.HSNCode=InvItm.HSNCode " & _
                '            " LEFT JOIN VatMasterTb ON VatMasterTb.vatid=InvItm.vatid " & branchstr & _
                '           "where  isnull(ishide,0)=0 AND itemCategory='" & category & "' AND " & _
                '           "[" & Fld & "] Like '" & FldValue & "%' ORDER BY [" & Fld & "]"
            End If
        Else
            strMyQry = "SELECT [Item Code] as Code,Description,UnitPrice as Price,QIH, CostAvg Cost,InvItm.ItemId,ROW_NUMBER() OVER(ORDER BY [" & Fld & "] ASC) AS Rownum FROM InvItm " & _
                        "LEFT JOIN InvItmPropertiesTb ON InvItmPropertiesTb.Itemid=InvItm.Itemid where isnull(ishide,0)=0 AND ismanufacturing=1 " & _
                        " and " & Fld & " Like '" & FldValue & "%' ORDER BY " & Fld
        End If
        _vInvItmtable = _objcmnbLayer._fldDatatable(strMyQry, False)
    End Sub
    Public Function SearchProductPanel(ByVal dvData As DataGridView, ByVal Fld As String, ByVal FldValue As String, ByVal WithPack As Boolean, _
                                       Optional ByVal isproduction As Boolean = False, Optional ByVal hideroom As Boolean = False, _
                                       Optional ByVal category As String = "", Optional ByVal returnDt As Boolean = False) As DataTable

        dvData.DataSource = Nothing
        Dim dtTable As DataTable
        'isnewItemcreated = True
        If _vInvItmtable.Rows.Count = 0 Or isnewItemcreated Then
rfrsh:
            If FldValue = "" Then
                _vInvItmtable.Rows.Clear()
            Else
                refreshItemTable(Fld, FldValue, WithPack, isproduction, hideroom, category)
            End If

            isnewItemcreated = False
            dtTable = _vInvItmtable
            If returnDt Then Return dtTable
        Else
            If FldValue = "" Then
                _vInvItmtable.Rows.Clear()
                dtTable = _vInvItmtable.Clone
            Else
                If Fld = "Item Code" Then Fld = "Code"
                Dim _qurey As EnumerableRowCollection(Of DataRow)
                _qurey = From data In _vInvItmtable.AsEnumerable() Where data(Fld).ToString.StartsWith(FldValue, StringComparison.OrdinalIgnoreCase) Select data
                If _qurey.Count > 0 Then
                    dtTable = _qurey.CopyToDataTable()
                Else
                    dtTable = _vInvItmtable.Clone
                End If
            End If
            If dtTable.Rows.Count = 0 Then
                Fld = "Item Code"
                GoTo rfrsh
            End If
        End If

        dvData.DataSource = dtTable
SetHeadOnly:
        With dvData
            SetGridProperty(dvData)

            .ColumnHeadersHeight = 100
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersDefaultCellStyle.BackColor = Color.Olive
            .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Verdana", 8.5!)
            .Columns(0).HeaderText = "Item Code"
            .Columns(0).Width = 100

            .Columns(1).HeaderText = "Description"
            .Columns(1).Width = 240

            .Columns("Price").HeaderText = "Price"
            .Columns("Price").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Price").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Price").Visible = True

            If Not isproduction Then
                .Columns("MRP").HeaderText = "MRP"
                .Columns("MRP").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("MRP").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("MRP").Visible = True

                .Columns("Tax Price").HeaderText = "Tax Price"
                .Columns("Tax Price").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("Tax Price").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("Tax Price").Visible = True

                .Columns("WSP").HeaderText = "WSP"
                .Columns("WSP").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("WSP").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("WSP").Visible = True

                .Columns("Cost+Tax").HeaderText = "WSP"
                .Columns("Cost+Tax").DefaultCellStyle.Format = "N" & NoOfDecimal
                .Columns("Cost+Tax").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("Cost+Tax").Visible = False
            End If
            .Columns("MRP").Visible = Not disableMRPFromProductSearch
            .Columns("WSP").Visible = Not disableWSFromProductSearch

            .Columns("QIH").HeaderText = "QIH"
            .Columns("QIH").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("QIH").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("QIH").Width = 50

            .Columns("Cost").HeaderText = "Cost"
            .Columns("Cost").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("Cost").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Cost").Visible = True

            .Columns("LPCost").HeaderText = "LCost"
            .Columns("LPCost").DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns("LPCost").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("LPCost").Visible = False

            .Columns("ItemID").HeaderText = "ItemID"
            .Columns("ItemID").Visible = False
            .Columns("Rownum").Visible = False

        End With
        'resizeGridColumn(dvData, 1)
        Return dtTable
    End Function
    Public Function MoveDownPl(ByVal dvData As DataGridView, ByVal SearchIndex As Integer, ByVal DefTxt As String) As String()
        Dim r As Integer
        Dim itms As String()
        With dvData
            If .RowCount < 1 Then GoTo Slct
            If .CurrentRow Is Nothing Then GoTo Slct
            If .CurrentRow.Index = .RowCount - 1 Then GoTo Slct
            r = .CurrentRow.Index
            If .CurrentRow.Index < .RowCount - 1 Then
                r = r + 1
                .Rows(r).Selected = True
                .CurrentCell = .Item(SearchIndex, r)
                .FirstDisplayedScrollingRowIndex() = r
            End If
Slct:
            itms = SelectItmPanel(dvData)
            Return itms
        End With
    End Function

    Public Function MoveUpPl(ByVal dvData As DataGridView, ByVal SearchIndex As Integer, ByVal DefTxt As String) As String()
        Dim r As Integer
        Dim itms As String()
        With dvData
            If .RowCount < 1 Then GoTo Slct
            If .CurrentRow Is Nothing Then GoTo Slct
            If .CurrentRow.Index < 0 Then GoTo Slct
            r = .CurrentRow.Index
            If r = 0 And DefTxt = Trim(.Item(SearchIndex, r).Value & "") Then GoTo Slct : Exit Function
            If r <> 0 Then
                r = r - 1
                If Not r < 0 Then
                    .Rows(r).Selected = True
                    .CurrentCell = .Item(SearchIndex, r)
                    .FirstDisplayedScrollingRowIndex() = r
                End If
            End If
Slct:
            itms = SelectItmPanel(dvData)
            Return itms
        End With
    End Function
    Public Function SelectItmPanel(ByVal dvData As DataGridView) As String()
        'On Error Resume Next

        With dvData
            Dim Items() As String
            Dim rindex As Integer
            Dim str As String
            ReDim Items(.Columns.Count - 1)
            If .Rows.Count < 1 Then Return Items : Exit Function
            If Not dvData.CurrentRow Is Nothing Then
                rindex = dvData.CurrentRow.Index
            Else
                dvData.CurrentCell = dvData.Item(1, rindex)
            End If

            Dim i As Byte
            For i = 0 To .Columns.Count - 1
                str = Trim(.Item(i, rindex).Value & "")
                If IsDBNull(.Item(i, rindex).Value) Then
                    Items(i) = ""
                Else
                    Items(i) = .Item(i, rindex).Value  ' .Columns(i).Text '  
                End If
            Next i
            Return Items 'ItemSelect(Items)
        End With
    End Function
    Public Sub clsCnnection()
        _objcmnbLayer.clsCnnection()
    End Sub
    Public Sub clsreader()
        _objcmnbLayer.clsreader()
    End Sub
    Public Function getProtectUntil() As Date
        Dim _vdatatable As New DataTable
        _vdatatable = _objcmnbLayer._fldDatatable("SELECT ProtectUntil FROM CompanyTb")
        If _vdatatable.Rows.Count > 0 Then
            If IsDBNull(_vdatatable(0)("ProtectUntil")) Then
                ProtectUntil = DateSerial(1950, 1, 1)
            Else
                ProtectUntil = _vdatatable(0)("ProtectUntil")
            End If
        End If
        Return ProtectUntil
    End Function
    Public Sub UpdtQty(ByVal TrId As Long, ByVal isModi As Boolean, ByVal TrType As String, Optional ByVal NPCost As Double = 0, Optional ByVal NItemId As Long = 0, Optional ByVal NTrQty As Double = 0, Optional ByVal MPQty As Double = 0)
        If isModi = True Then
            Dim dt As DataTable
            Dim i As Integer
            dt = _objcmnbLayer._fldDatatable("Select TrQty,ItemId,MTrPQty From ItmInvTrTb Where MsgId =0 and TrId=" & TrId)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    Select Case TrType
                        Case "IP"
                            _objcmnbLayer._saveDatawithOutParm("Update InvItm Set RcvdQty=RcvdQty -" & dt(i)("TrQty") & "  Where ItemId=" & dt(i)("ItemId"))
                        Case "IS"
                            _objcmnbLayer._saveDatawithOutParm("Update InvItm Set IssdQty=IssdQty -" & dt(i)("TrQty") & " Where ItemId=" & dt(i)("ItemId"))
                    End Select
                    _objcmnbLayer._saveDatawithOutParm("Update InvItm Set QIH=opQty+RcvdQty-IssdQty Where   ITEMID=" & dt(i)("ItemId"))
                Next
            End If
        Else
            Select Case TrType
                Case "IP"
                    _objcmnbLayer._saveDatawithOutParm("Update InvItm Set RcvdQty=RcvdQty+" & NTrQty & " Where ItemId=" & NItemId)
                Case "IS"
                    _objcmnbLayer._saveDatawithOutParm("Update InvItm Set IssdQty=IssdQty+" & NTrQty & " Where ItemId=" & NItemId)
            End Select
            _objcmnbLayer._saveDatawithOutParm("Update InvItm Set QIH=opQty+RcvdQty -IssdQty Where ItemId=" & NItemId)
        End If
    End Sub
    Public Sub UpdtClosBal(ByVal AccountNo As Long, ByVal Amount As Double)
        '_objcmnbLayer._saveDatawithOutParm(" Update Accmast set ClosingBal = ClosingBal + " & Amount & " where AccountNo=" & AccountNo)
        _objcmnbLayer.updateClosingBalance(AccountNo)
    End Sub
    Public Function getVrsDet(ByVal id As Long, ByVal CmnVrFldName As String, Optional ByVal isCustmVr As Boolean = False) As String
        Dim PreFixTb As DataTable
        getVrsDet = ""
        If id <> 0 Then
            PreFixTb = _objcmnbLayer._fldDatatable("SELECT  * FROM PreFixTb WHERE Id = " & id, False)
            With PreFixTb
                If .Rows.Count > 0 Then
                    If .Rows(0)("Enable") Then
                        getVrsDet = IIf(Trim(.Rows(0)("PreFix")) = "", "      ", FormatString(.Rows(0)("PreFix"), "!@@@@@")) & Format(PreFixTb.Rows(0)("vrNo"), "00000") & Format(.Rows(0)("ANo"), "00000000") & Format(.Rows(0)("ANo2"), "00000000")
                        GoTo Ter
                    Else
                        getVrsDet = Format(.Rows(0)("ANo"), "00000000") & Format(.Rows(0)("ANo2"), "00000000")
                    End If
                End If
            End With
        End If
        If isCustmVr Then
            PreFixTb = _objcmnbLayer._fldDatatable("SELECT * FROM CustmVr WHERE [TypeId] = '" & CmnVrFldName & "'", False)
            If PreFixTb.Rows.Count > 0 Then
                If Not IsDBNull(PreFixTb.Rows(0)("PreFix")) Then
                    getVrsDet = IIf(Trim(PreFixTb.Rows(0)("PreFix")) = "", "      ", FormatString(PreFixTb.Rows(0)("PreFix"), "!@@@@@")) & PreFixTb.Rows(0)("vrNo")
                Else
                    getVrsDet = "      " & PreFixTb.Rows(0)("vrNo")
                End If
            End If
        Else
            PreFixTb = _objcmnbLayer._fldDatatable("SELECT * FROM InvNos", False)
            With PreFixTb
                If PreFixTb.Rows.Count > 0 Then
                    If IsDBNull(.Rows(0)(CmnVrFldName)) Then GoTo Nval
                    If Trim(.Rows(0)(CmnVrFldName)) = "" Or IsDBNull(.Rows(0)(CmnVrFldName)) Then
Nval:
                        Dim sVrNo As String = "      000001"
                        .Rows(0)(CmnVrFldName) = sVrNo
                        '_objcmnbLayer.__saveDataTable("SELECT " & CmnVrFldName & " FROM InvNos", PreFixTb)
                        _objcmnbLayer._saveDatawithOutParm("UPDATE InvNos SET [" & CmnVrFldName & "] = '" & sVrNo & "'")
                        PreFixTb.AcceptChanges()
                    End If
                    getVrsDet = Left(PreFixTb.Rows(0)(CmnVrFldName), 5) & FormatString(Val(Mid(.Rows(0)(CmnVrFldName), 6)), "000000") & getVrsDet
                End If
            End With
        End If
Ter:
        Return getVrsDet
    End Function
    Public Sub ModifyClosBal(ByVal TrType As String, ByVal PreFix As String, ByVal InvNo As Long, Optional ByVal UnqNo As Long = 0)
        Dim i As Integer
        Dim rsSelect As DataTable
        If Val(UnqNo) > 0 Then
            rsSelect = _objcmnbLayer._fldDatatable("Select * FROM AccTrDet WHERE LinkNo = " & UnqNo)
        Else
            rsSelect = _objcmnbLayer._fldDatatable("Select AccTrdet.* FROM AccTrdet Left join AccTrCmn on AccTrdet.LinkNo=AccTrCmn.LinkNo  WHERE JVType = '" & TrType & "' AND JVNum = " & InvNo & " AND PreFix = '" & MkDbSrchStr(PreFix) & "'")
        End If
        If rsSelect.Rows.Count > 0 Then
            For i = 0 To rsSelect.Rows.Count - 1
                _objcmnbLayer._saveDatawithOutParm("Update AccMast Set ClosingBal=ClosingBal+ " & Val(IIf(IsDBNull(rsSelect(i)("DealAmt")), 0, rsSelect(i)("DealAmt"))) * -1 & " Where AccountNo=" & rsSelect(i)("AccountNo"))
            Next
        End If
    End Sub
    Public Function SetNextVrNoFromVariable(ByVal vrVoucherNo As Long, ByVal id As Integer, ByVal CmnVrFldName As String, ByVal Condition As String, ByVal NoPreFix As String, Optional ByVal isCustmVr As Boolean = False, Optional ByVal Save As Boolean = False, Optional ByVal jtype As Integer = 0, Optional ByVal vrPrefix As String = "") As String
        Dim vrAccountNo1 As Long
        Dim vrAccountNo2 As Long
        Dim PreFixTb As DataTable
        Dim i As Integer
        If id = 0 Then
            i = vrVoucherNo
        Else
            If vrVoucherNo = 0 Then
                getVrsDet(id, CmnVrFldName, vrPrefix, vrVoucherNo, vrAccountNo1, vrAccountNo2)
            End If

            i = vrVoucherNo
        End If

        Do Until False
            If jtype = 1 Then ' job invoice
                PreFixTb = _objcmnbLayer._fldDatatable("SELECT InvNo FROM JobInvCmnTb  WHERE " & Condition & i & IIf(NoPreFix, "", " AND PreFix = '" & Trim(vrPrefix) & "'"))
            ElseIf jtype = 2 Then 'temple sales invoice
                PreFixTb = _objcmnbLayer._fldDatatable("SELECT InvNo FROM TempleSalesCmnTb  WHERE InvNo=" & i & IIf(NoPreFix, "", " AND PreFix = '" & Trim(vrPrefix) & "'"))
            ElseIf Condition Like "*DocType*" Then
                PreFixTb = _objcmnbLayer._fldDatatable("SELECT DNO FROM DocCmnTb WHERE " & Condition & i & IIf(NoPreFix, "", " AND PreFix = '" & Trim(vrPrefix) & "'"))
            ElseIf Condition Like "*TrType*" Then
                PreFixTb = _objcmnbLayer._fldDatatable("SELECT InvNo FROM ItmInvCmnTb WHERE " & Condition & i & IIf(NoPreFix, "", " AND PreFix = '" & Trim(vrPrefix) & "'"))
            Else
                PreFixTb = _objcmnbLayer._fldDatatable("SELECT JVNum FROM AccTrCmn WHERE " & Condition & i & IIf(NoPreFix, "", " AND PreFix = '" & Trim(vrPrefix) & "'"))
            End If
            If PreFixTb.Rows.Count = 0 Then GoTo sv
            i = i + 1
            If i > 999999 Then i = 1
        Loop
        If i = vrVoucherNo Then GoTo Ter 'Exit Sub
sv:
        If Save Then
            vrVoucherNo = i
            PreFixTb = _objcmnbLayer._fldDatatable("SELECT * FROM PreFixTb WHERE Id = " & id)
            With PreFixTb
                If PreFixTb.Rows.Count > 0 Then  ' .RecordCount > 0 Then
                    If .Rows(0)("Enable") Then
                        .Rows(0)("vrNo") = i
                        _objcmnbLayer.__saveDataTable("SELECT * FROM PreFixTb WHERE Id = " & id, PreFixTb)
                        GoTo Ter
                    Else
                    End If
                End If
            End With
            _objcmnbLayer._saveDatawithOutParm("UPDATE InvNos SET Prefix = '" & vrPrefix & "',InvNo=" & i & " WHERE InvType='" & CmnVrFldName & "'")

        End If
Ter:
        Return vrPrefix
    End Function
    Public Function SetNextVrNo(ByVal txtVr As TextBox, ByVal id As Integer, ByVal CmnVrFldName As String, ByVal Condition As String, ByVal NoPreFix As String, Optional ByVal isCustmVr As Boolean = False, Optional ByVal Save As Boolean = False, Optional ByVal jtype As Integer = 0, Optional ByVal vrPrefix As String = "") As String
        GoTo getno
        Dim vrVoucherNo As Long
        Dim vrAccountNo1 As Long
        Dim vrAccountNo2 As Long
        Dim PreFixTb As DataTable
        Dim VrTypeNo As Integer
        Dim i As Integer
        If id = 0 Then
            i = txtVr.Text
        Else
            getVrsDet(id, CmnVrFldName, vrPrefix, vrVoucherNo, vrAccountNo1, vrAccountNo2)
            i = vrVoucherNo
        End If

        Do Until False
            If jtype = 1 Then ' job invoice
                PreFixTb = _objcmnbLayer._fldDatatable("SELECT InvNo FROM JobInvCmnTb  WHERE " & Condition & i & IIf(NoPreFix, "", " AND PreFix = '" & Trim(vrPrefix) & "'"))
            ElseIf jtype = 2 Then 'temple sales invoice
                PreFixTb = _objcmnbLayer._fldDatatable("SELECT InvNo FROM TempleSalesCmnTb  WHERE InvNo=" & i & IIf(NoPreFix, "", " AND PreFix = '" & Trim(vrPrefix) & "'"))
            ElseIf Condition Like "*DocType*" Then
                PreFixTb = _objcmnbLayer._fldDatatable("SELECT DNO FROM DocCmnTb WHERE " & Condition & i & IIf(NoPreFix, "", " AND PreFix = '" & Trim(vrPrefix) & "'") & IIf(UsrBr = "", "", " AND BrId = '" & Trim(UsrBr) & "'"))
            ElseIf Condition Like "*TrType*" Then
                PreFixTb = _objcmnbLayer._fldDatatable("SELECT InvNo FROM ItmInvCmnTb WHERE " & Condition & i & IIf(NoPreFix, "", " AND PreFix = '" & Trim(vrPrefix) & "'") & IIf(UsrBr = "", "", " AND BrId = '" & Trim(UsrBr) & "'"))
            ElseIf Condition Like "*visitno*" Then
                PreFixTb = _objcmnbLayer._fldDatatable("SELECT visitno FROM ClinicVistTb WHERE " & Condition & i & IIf(NoPreFix, "", " AND PreFix = '" & Trim(vrPrefix) & "'"))
            Else
                PreFixTb = _objcmnbLayer._fldDatatable("SELECT JVNum FROM AccTrCmn WHERE " & Condition & i & IIf(NoPreFix, "", " AND PreFix = '" & Trim(vrPrefix) & "'") & IIf(UsrBr = "", "", " AND CmnBrId = '" & Trim(UsrBr) & "'"))
            End If
            If PreFixTb.Rows.Count = 0 Then Exit Do
            i = i + 1
            If i > 999999 Then i = 1
        Loop
        If i = Val(txtVr.Text) Then GoTo Ter 'Exit Sub
        If Save Then
            txtVr.Text = i
            PreFixTb = _objcmnbLayer._fldDatatable("SELECT * FROM PreFixTb WHERE Id = " & id)
            With PreFixTb
                If PreFixTb.Rows.Count > 0 Then  ' .RecordCount > 0 Then
                    If .Rows(0)("Enable") Then
                        .Rows(0)("vrNo") = i
                        _objcmnbLayer.__saveDataTable("SELECT * FROM PreFixTb WHERE Id = " & id, PreFixTb)
                        GoTo Ter
                    Else

                    End If
                    VrTypeNo = .Rows(0)("VrTypeNo")
                End If
            End With
            'Dim dt As DataTable

            'dt = _objcmnbLayer._fldDatatable("Select * from InvNosBrTb where InvType='" & CmnVrFldName & "' and Brcode='" & UsrBr & "'")
            'If dt.Rows.Count > 0 Then
            '    _objcmnbLayer._saveDatawithOutParm("UPDATE InvNosBrTb SET Prefix = '" & vrPrefix & "',InvNo=" & i & " WHERE InvType='" & CmnVrFldName & "' and Brcode='" & UsrBr & "'")
            'Else
            '    _objcmnbLayer._saveDatawithOutParm("UPDATE InvNos SET Prefix = '" & vrPrefix & "',InvNo=" & i & " WHERE InvType='" & CmnVrFldName & "'")

            'End If
            Dim userInvnos As String = "declare @id int "
            userInvnos = userInvnos & "Select @id=count(InvNo) from InvNosBrTb where InvType='" & CmnVrFldName & "' and Brcode='" & UsrBr & "'"
            userInvnos = userInvnos & " if isnull(@id,0)>0 begin "
            userInvnos = userInvnos & " UPDATE InvNosBrTb SET Prefix = '" & vrPrefix & "',InvNo=" & i & " WHERE InvType='" & CmnVrFldName & "' and Brcode='" & UsrBr & "' end "
            userInvnos = userInvnos & " else begin"
            userInvnos = userInvnos & " UPDATE InvNos SET Prefix = '" & vrPrefix & "',InvNo=" & i & " WHERE InvType='" & CmnVrFldName & "' end "

getno:
            Dim branch As String = IIf(UsrBr = "", Dbranch, UsrBr)
            userInvnos = IIf(branch = "", " SELECT * FROM InvNos WHERE InvType='" & CmnVrFldName & "'", " SELECT * FROM InvNosBrTb WHERE Brcode='" & branch & "' AND InvType='" & CmnVrFldName & "'")
            userInvnos = userInvnos & " SELECT * FROM PreFixTb WHERE VrTypeNo = " & VrTypeNo & IIf(branch = "", "", " AND BrId In ('', '" & branch & "')") & " Order by ordNo"
            Dim dtset As DataSet
            dtset = _objcmnbLayer._ldDataset(userInvnos, False)
            dtInvNos = dtset.Tables(0)
            PreFixTb = dtset.Tables(1)
        End If
Ter:
        SetNextVrNo = vrPrefix
    End Function
    Public Function SaveNxtVrNo(ByVal txtVr As TextBox, ByVal id As Long, ByVal CmnVrFldName As String, ByVal Condition As String, Optional ByVal isCustmVr As Boolean = False, Optional ByVal Save As Boolean = False, Optional ByVal NoPreFix As Boolean = False) As String
        Dim i As Long
        Dim sVrNo As String
        Dim PreFixTb As New DataTable
        sVrNo = getVrsDet(id, CmnVrFldName, isCustmVr)
        If Save = False Then txtVr.Text = Val(Mid(sVrNo, 6, 6))
        i = Val(txtVr.Text) ' Val(Mid(txtVr.FormattedText, 3))
        Do Until False
            If Condition Like "*TrType*" Then
                PreFixTb = _objcmnbLayer._fldDatatable("SELECT InvNo FROM ItmInvCmnTb WHERE " & Condition & i & IIf(NoPreFix, "", " AND PreFix = '" & Trim(Left(sVrNo, 5)) & "'"))
            Else
                PreFixTb = _objcmnbLayer._fldDatatable("SELECT JVNum FROM AccTrCmn WHERE " & Condition & i & IIf(NoPreFix, "", " AND PreFix = '" & Trim(Mid(sVrNo, 1, 5)) & "'"))
            End If
            If PreFixTb.Rows.Count = 0 Then Exit Do
            i = i + 1
            If i > 999999 Then i = 1
        Loop
        If i = Val(txtVr.Text) Then GoTo Ter 'Exit Sub
        If Save Then
            txtVr.Text = i
            sVrNo = Left(sVrNo, 5) & Format(i, "000000")
            PreFixTb = _objcmnbLayer._fldDatatable("SELECT * FROM PreFixTb WHERE Id = " & id)
            With PreFixTb
                If PreFixTb.Rows.Count > 0 Then  ' .RecordCount > 0 Then
                    If .Rows(0)("Enable") Then
                        .Rows(0)("vrNo") = i
                        _objcmnbLayer.__saveDataTable("SELECT * FROM PreFixTb WHERE Id = " & id, PreFixTb)
                        GoTo Ter
                    Else
                    End If
                End If
            End With
            If isCustmVr Then
                _objcmnbLayer._saveDatawithOutParm("UPDATE CustmVr SET VrNo = " & i & " WHERE TypeId = '" & CmnVrFldName & "'")
            Else
                _objcmnbLayer._saveDatawithOutParm("UPDATE InvNos SET [" & CmnVrFldName & "] = '" & sVrNo & "'")
            End If
        End If
Ter:
        SaveNxtVrNo = Left(sVrNo, 5)
    End Function
    Public Function CheckNoExists(ByRef Prefix As String, ByVal Invno As Long, ByVal Trtype As String, ByVal Tablename As String, Optional ByVal txt As TextBox = Nothing) As Boolean
        Dim _vdatatable As New DataTable
        Dim dt As DataTable
        CheckNoExists = True
        If Tablename = "Inventory" Then
            If Invno = 0 Then
                dt = _objcmnbLayer._fldDatatable("SELECT max(invno) invno FROM ItmInvCmnTb where invStatus=0 and TrType='" & Trtype & "' and Prefix = '" & Prefix & "'")
                If dt.Rows.Count > 0 Then
                    Invno = Val(dt(0)("invno") & "") + 1
                Else
                    Invno = 1
                End If
            End If
            _vdatatable = _objcmnbLayer._fldDatatable("SELECT trid FROM ItmInvCmnTb where invStatus=0 and TrType='" & Trtype & "' and Prefix = '" & Prefix & "' AND InvNo=" & Invno)
        ElseIf Tablename = "Document" Then
            If Invno = 0 Then
                dt = _objcmnbLayer._fldDatatable("SELECT max(DNo) DNo FROM DocCmnTb where DocType='" & Trtype & "' and Prefix = '" & Prefix & "'")
                If dt.Rows.Count > 0 Then
                    Invno = Val(dt(0)("DNo") & "") + 1
                Else
                    Invno = 1
                End If
            End If
            _vdatatable = _objcmnbLayer._fldDatatable("SELECT Docid from DocCmnTb where DocType='" & Trtype & "' AND DNo=" & Invno & " and Prefix = '" & Prefix & "'")
        ElseIf Tablename = "Accounts" Then
            If Invno = 0 Then
                dt = _objcmnbLayer._fldDatatable("SELECT max(JVNum) JVNum FROM AccTrCmn where JVType='" & Trtype & "' and Prefix = '" & Prefix & "'")
                If dt.Rows.Count > 0 Then
                    Invno = Val(dt(0)("JVNum") & "") + 1
                Else
                    Invno = 1
                End If
            End If
            _vdatatable = _objcmnbLayer._fldDatatable("SELECT LinkNo from AccTrCmn where JVType='" & Trtype & "' AND Prefix = '" & Prefix & "' AND JVNum=" & Invno)
        ElseIf Tablename = "TemapleSales" Then
            If Invno = 0 Then
                dt = _objcmnbLayer._fldDatatable("SELECT max(InvNo) InvNo FROM TempleSalesCmnTb where Prefix = '" & Prefix & "'")
                If dt.Rows.Count > 0 Then
                    Invno = Val(dt(0)("InvNo") & "") + 1
                Else
                    Invno = 1
                End If
            End If
            _vdatatable = _objcmnbLayer._fldDatatable("SELECT Trid from TempleSalesCmnTb where Prefix = '" & Prefix & "' AND InvNo=" & Invno)
        ElseIf Tablename = "ClinicVisit" Then
            If Invno = 0 Then
                dt = _objcmnbLayer._fldDatatable("SELECT max(visitno) InvNo FROM ClinicVistTb where Prefix = '" & Prefix & "' and visittype='" & Trtype & "'")
                If dt.Rows.Count > 0 Then
                    Invno = Val(dt(0)("InvNo") & "") + 1
                Else
                    Invno = 1
                End If
            End If
            _vdatatable = _objcmnbLayer._fldDatatable("SELECT visitid from ClinicVistTb where Prefix = '" & Prefix & "' AND visitno=" & Invno & " and visittype='" & Trtype & "'")
        End If
        If Not txt Is Nothing Then txt.Text = Invno
        If _vdatatable.Rows.Count > 0 Then
            CheckNoExists = False
        End If
    End Function
    Public Sub RfrshPrssdQty(ByVal DocStr As String)
        If Trim(DocStr) = "" Then Exit Sub
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT DocId, [LineNo], Sum(TrQty) As TQty FROM DocAssgnTb  WHERE DocId In (" & DocStr & ") GROUP BY DocId, [LineNo]")
        _objcmnbLayer._saveDatawithOutParm("UPDATE DocTranTb SET ProssdQty = 0 WHERE DocId In (" & DocStr & ")")
        If dt.Rows.Count > 0 Then
            For i = 0 To dt.Rows.Count - 1
                _objcmnbLayer._saveDatawithOutParm("UPDATE DocTranTb SET ProssdQty = " & IIf(IsDBNull(dt(i)("TQty")), 0, dt(i)("TQty")) & " WHERE DocId = " & dt(i)("DocId") & " AND SlNo = " & dt(i)("LineNo"))
            Next
        End If
    End Sub
    Public Function getDateNo(ByVal Dt As Date) As Long
        getDateNo = CLng(DateDiff(DateInterval.Day, DateSerial(2006, 1, 1), Dt))
    End Function
    Public Function correctDOStr(ByRef sDOs As String) As String
        Dim s As String
        Dim Pos As Short
        correctDOStr = ""
        Do Until False
            Pos = InStr(1, sDOs, ",")
            s = Mid(sDOs, 1, IIf(Pos, Pos - 1, Len(sDOs)))
            sDOs = Mid(sDOs, IIf(Pos, Len(sDOs) - Pos, 1))
            If Val(s) > 0 And Val(s) < 9999999 Then
                correctDOStr = correctDOStr & Val(s) & ", "
            End If
            If sDOs = "" Then Exit Do
        Loop
        If correctDOStr <> "" Then correctDOStr = Mid(correctDOStr, 1, Len(correctDOStr) - 2)
    End Function
    Public Function getDocTypeNo(ByRef DType As String) As Integer
        Select Case DType
            Case "MR"
                getDocTypeNo = 0
            Case "QTI"
                getDocTypeNo = 1
            Case "PO"
                getDocTypeNo = 2
            Case "SO"
                getDocTypeNo = 3
            Case "DOS"
                getDocTypeNo = 4
            Case "DOC"
                getDocTypeNo = 5
            Case "SQT"
                getDocTypeNo = 6
            Case "ENQ"
                getDocTypeNo = 7
        End Select
    End Function
    'Public Function getRptDefFlName(ByVal RptType As String, Optional ByRef Captn As String = "Sujis Report") As String
    '    Try
    '        getRptDefFlName = ""
    '        Dim RptCmnTb As DataTable
    '        RptCmnTb = _objcmnbLayer._fldDatatable("SELECT RptCaption, RptTypeName, DefaultRpt, OnlySaveAs, RptName FROM RptCmnTb LEFT JOIN RptFls ON RptFls.RptNo = RptCmnTb.DefaultRpt WHERE RptCmnTb.RptType = '" & RptType & "'")
    '        If RptCmnTb.Rows.Count > 0 Then
    '            If IsDBNull(RptCmnTb(0)("DefaultRpt")) Or IsDBNull(RptCmnTb(0)("RptName")) Then
    '                MsgBox("Default format is not found.  Chose 'Pring Dialog' and set Default.", vbExclamation)
    '                Exit Function
    '            Else
    '                getRptDefFlName = IIf(RptCmnTb(0)("OnlySaveAs"), APath, DPath) & RptCmnTb(0)("RptName")
    '                If Not IsDBNull(Captn) Then
    '                    Captn = RptCmnTb(0)("RptTypeName") & " - " & RptCmnTb(0)("RptCaption")
    '                End If
    '            End If
    '        Else
    '            MsgBox("Voucher type " & RptType & " not found in the format table.  Please contact your vendor.", vbCritical)
    '            Exit Function
    '        End If
    '        If Not FileExists(getRptDefFlName) Then
    '            getRptDefFlName = ""
    '            MsgBox("Selected format file not found !!", vbCritical)
    '            Exit Function
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '        getRptDefFlName = ""
    '    End Try
    'End Function
    Public Function getRptDefFlName(ByVal RptType As String, Optional ByRef Captn As String = "MOSE Report", Optional ByRef printername As String = "") As String
        Try
            getRptDefFlName = ""
            Dim RptCmnTb As DataTable
            RptCmnTb = _objcmnbLayer._fldDatatable("SELECT RptCaption, RptTypeName, DefaultRpt, OnlySaveAs, RptName,RptNo,printername FROM RptCmnTb " & _
                                                   "LEFT JOIN RptFls ON RptFls.RptNo = RptCmnTb.DefaultRpt WHERE RptCmnTb.RptType = '" & RptType & "'")
            If RptCmnTb.Rows.Count > 0 Then
                If IsDBNull(RptCmnTb(0)("DefaultRpt")) Or IsDBNull(RptCmnTb(0)("RptName")) Then
                    MsgBox("Default format is not found.  Chose 'Pring Dialog' and set Default.", vbExclamation)
                    Exit Function
                Else
                    getRptDefFlName = IIf(RptCmnTb(0)("OnlySaveAs"), APath, DPath) & RptCmnTb(0)("RptName")
                    If Not IsDBNull(Captn) Then
                        Captn = RptCmnTb(0)("RptTypeName") & " - " & RptCmnTb(0)("RptCaption")
                    End If
                End If
                Dim dt As DataTable
                printername = Trim(RptCmnTb(0)("printername") & "")
                If printername = "" Then
                    If RptCmnTb(0)("RptNo") < 8000 Then
                        dt = _objcmnbLayer._fldDatatable("SELECT printername from RptDetTb where RptNo=" & RptCmnTb(0)("RptNo"))
                        If dt.Rows.Count > 0 Then
                            printername = Trim(dt(0)("printername") & "")
                        End If
                    Else
                        dt = _objcmnbLayer._fldDatatable("SELECT printername from RptCustTb where CustRptNo=" & RptCmnTb(0)("RptNo") - 8000)
                        If dt.Rows.Count > 0 Then
                            printername = Trim(dt(0)("printername") & "")
                        End If
                    End If
                End If
            Else
                MsgBox("Voucher type " & RptType & " not found in the format table.  Please contact your vendor.", vbCritical)
                Exit Function
            End If
            If Not FileExists(getRptDefFlName) Then
                getRptDefFlName = ""
                MsgBox("Selected format file not found !!(" & getRptDefFlName & ")", vbCritical)
                Exit Function
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            getRptDefFlName = ""
        End Try
    End Function
    Public Function getPurchAmt(ByVal dMasterVal As Double, ByVal AccounNO As Long, ByVal ItemId As Long) As Double

        Dim dtSPInfTb As DataTable
        Dim PMult As Double
        If ItemId = 0 Then Exit Function
        If AccounNO = 0 Then
            'Bring Price from Item Master
BringFromMaster:
            Return dMasterVal / 1 'fcrt
        Else
            'Bring Price from Item Transaction (Last Sale Price)
            'If chkLastPrice.Checked Then
            PMult = 1
            _objcmnbLayer.clsreader()
            _objcmnbLayer.clsCnnection()
            dtSPInfTb = _objcmnbLayer._fldDatatable("SELECT isnull(LastCost,isnull(Opcost,0)) Lpc FROM AccMast " & _
                                                    "LEFT JOIN SuppCostTb ON AccMast.accid=SuppCostTb.accountno " & _
                                                    " WHERE AccMast.accid = " & AccounNO & _
                                                    " And SuppCostTb.ItemId = " & ItemId)
            If dtSPInfTb.Rows.Count = 0 And ItemId > 0 Then
                dtSPInfTb = _objcmnbLayer._fldDatatable("select case when isnull(CostAvg,0)=0 then isnull(Opcost,0) else costAvg end lpc from invitm where itemid=" & ItemId)
                If dtSPInfTb.Rows.Count = 0 Then
                    GoTo BringFromMaster
                Else
                    Return dtSPInfTb(0)("Lpc")
                End If
            Else
                Return dtSPInfTb(0)("Lpc")
            End If
        End If
    End Function
    Public Function getRight(ByVal NodId As Long, ByVal userid As String) As Boolean
        Dim dt As DataTable
        If userType Then
            'dt = _objcmnbLayer._fldDatatable("SELECT * FROM Rights LEFT JOIN UserTb ON Rights.UId=UserTb.Id where NodeId=" & NodId & " AND UserId='" & userid & "'")
            Dim _qurey As EnumerableRowCollection(Of DataRow)
            _qurey = From data In dtrights.AsEnumerable() Where data("NodeId") = NodId Select data
            If _qurey.Count > 0 Then
                dt = _qurey.CopyToDataTable()
            Else
                dt = dtrights.Clone
            End If
            If dt.Rows.Count = 0 Then
                Return False
            Else
                Return True
            End If
        Else
            Return True
        End If

    End Function
    Public Sub SetEntryGridProperty(ByVal dgDataGrid As DataGridView)
        With dgDataGrid
            If EntrygridFontSize = 0 Then EntrygridFontSize = 8
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = True
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            .ReadOnly = False
            .EditMode = DataGridViewEditMode.EditProgrammatically
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersDefaultCellStyle.BackColor = Color.Olive
            .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.5!, System.Drawing.FontStyle.Bold)
            .RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", EntrygridFontSize)
            .ColumnHeadersHeight = 100
            .RowTemplate.Height = 22 + EntrygridFontSize - 8

            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        End With
    End Sub
    Public Sub resizeGridColumn(ByVal grd As DataGridView, ByVal columnToresize As Integer)
        If grd.Columns.Count = 0 Then Exit Sub
        Dim colwidth, sizeOfbeforeColumns As Integer
        Dim i As Integer
        For i = columnToresize + 1 To grd.Columns.Count - 1
            If grd.Columns(i).Visible = True Then
                colwidth = colwidth + grd.Columns(i).Width
            End If
        Next
        For i = 0 To columnToresize - 1
            If grd.Columns(i).Visible = True Then
                sizeOfbeforeColumns = sizeOfbeforeColumns + grd.Columns(i).Width
            End If
        Next
        'With grd
        '    sizeOfbeforeColumns = .Columns(ConstSlNo).Width + .Columns(ConstItemCode).Width
        'End With
        colwidth = colwidth + sizeOfbeforeColumns
        grd.Columns(columnToresize).Width = grd.Width - colwidth - 40
    End Sub
    Public Sub SetGridHeadEntryProperty(ByVal grdVoucher As DataGridView)
        With grdVoucher
            SetEntryGridProperty(grdVoucher)
            .ColumnCount = 72

            .Columns(ConstSlNo).HeaderText = "SlNo"
            .Columns(ConstSlNo).Width = 40
            '.Columns(ConstSlNo).ReadOnly = False
            .Columns(ConstSlNo).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(ConstSlNo).Resizable = DataGridViewTriState.False

            .Columns(ConstSlNo).DefaultCellStyle.Format = "N0"
            .Columns(ConstSlNo).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstSlNo).ReadOnly = True
            .Columns(ConstSlNo).DefaultCellStyle.BackColor = Color.AliceBlue


            .Columns(ConstBarcode).HeaderText = "HSN Code"
            .Columns(ConstBarcode).Width = 100
            .Columns(ConstBarcode).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstBarcode).ReadOnly = True
            .Columns(ConstBarcode).Visible = EnableGST

            .Columns(ConstItemCode).HeaderText = "ItemCode"
            .Columns(ConstItemCode).Width = 100
            .Columns(ConstItemCode).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstItemCode).ReadOnly = False

            .Columns(ConstDescr).HeaderText = "Description"
            '.Columns(ConstDescr).Width = Me.Width * 45 / 100
            .Columns(ConstDescr).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstDescr).ReadOnly = disableEditProdectDescription
            .Columns(ConstDescr).Width = 150
            .Columns(ConstDescr).Frozen = True

            .Columns(ConstB).HeaderText = "B?"
            .Columns(ConstB).Width = 25
            .Columns(ConstB).ReadOnly = True
            .Columns(ConstB).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(ConstSlNo).Resizable = DataGridViewTriState.False
            .Columns(ConstB).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(ConstB).Visible = False

            .Columns(ConstUnit).HeaderText = "Unit"
            .Columns(ConstUnit).Width = 40
            .Columns(ConstUnit).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstUnit).ReadOnly = True
            .Columns(ConstUnit).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(ConstUnit).DefaultCellStyle.BackColor = Color.Red
            '.Columns(ConstUnit).Resizable = DataGridViewTriState.False

            Dim cmb As New DataGridViewComboBoxColumn
            Dim dt As DataTable
            'cmb.Items.Add("")
            If EnableWarranty Then
                'dt = _objcmnbLayer._fldDatatable("SELECT WarrentyName FROM WarrentyMasterTb")
                Dim i As Integer
                cmb.Items.Add("")
                For i = 0 To dtwarrenty.Rows.Count - 1
                    cmb.Items.Add(Trim(dtwarrenty(i)(0)))
                Next
            End If

            cmb.HeaderText = "Warranty"
            cmb.DataPropertyName = "Warranty"
            cmb.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
            cmb.DefaultCellStyle.Font = New System.Drawing.Font("Arial Black", 12.0!, System.Drawing.FontStyle.Bold)
            .Columns.RemoveAt(ConstLocation)
            .Columns.Insert(ConstLocation, cmb)
            .Columns(ConstLocation).HeaderText = "Warranty"
            .Columns(ConstLocation).Width = 100
            .Columns(ConstLocation).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstLocation).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(ConstLocation).ReadOnly = False ' Not AllowLocationItemwiseOnInventory
            .Columns(ConstLocation).Visible = EnableWarranty

            cmb = New DataGridViewComboBoxColumn
            'cmb.Items.Add("")
            If enableItemwiseSalesman Then
                'dt = _objcmnbLayer._fldDatatable("SELECT SManCode FROM SalesmanTb")
                cmb.Items.Add("")
                If Not dtsalesman Is Nothing Then
                    For i = 0 To dtsalesman.Rows.Count - 1
                        cmb.Items.Add(Trim(dtsalesman(i)(0)))
                    Next
                End If
              
                cmb.HeaderText = "Service By"
                cmb.DataPropertyName = "SManCode"
                cmb.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
                cmb.DefaultCellStyle.Font = New System.Drawing.Font("Arial Black", 12.0!, System.Drawing.FontStyle.Bold)
                .Columns.RemoveAt(Constsman)
                .Columns.Insert(Constsman, cmb)
                .Columns(Constsman).HeaderText = "Serviced By"
                .Columns(Constsman).Width = 100
                .Columns(Constsman).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(Constsman).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(Constsman).ReadOnly = False ' Not AllowLocationItemwiseOnInventory
                .Columns(Constsman).Visible = False
            ElseIf EnableFruitsSales Then
                dt = _objcmnbLayer._fldDatatable("SELECT carriername FROM CarrierTb")
                cmb.Items.Add("")
                For i = 0 To dt.Rows.Count - 1
                    cmb.Items.Add(Trim(dt(i)(0)))
                Next
                cmb.DataPropertyName = "carriername"
                cmb.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
                cmb.DefaultCellStyle.Font = New System.Drawing.Font("Arial Black", 12.0!, System.Drawing.FontStyle.Bold)
                .Columns.RemoveAt(Constsman)
                .Columns.Insert(Constsman, cmb)
                .Columns(Constsman).HeaderText = "Carrier"
                .Columns(Constsman).Width = 100
                .Columns(Constsman).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(Constsman).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(Constsman).ReadOnly = False ' Not AllowLocationItemwiseOnInventory
                .Columns(Constsman).Visible = True
            Else
                .Columns(Constsman).Visible = False
            End If
            cmb = New DataGridViewComboBoxColumn
            'cmb.Items.Add("")
            If enableFuleBankInvoice Then
                dt = _objcmnbLayer._fldDatatable("SELECT fcode FROM FuelMeterReadingTb")
                cmb.Items.Add("")
                For i = 0 To dt.Rows.Count - 1
                    cmb.Items.Add(Trim(dt(i)(0)))
                Next
            End If

            'cmb.HeaderText = "Fuel Meter"
            cmb.DataPropertyName = "fcode"
            cmb.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
            cmb.DefaultCellStyle.Font = New System.Drawing.Font("Arial Black", 12.0!, System.Drawing.FontStyle.Bold)
            .Columns.RemoveAt(ConstMeterCode)
            .Columns.Insert(ConstMeterCode, cmb)
            .Columns(ConstMeterCode).HeaderText = "Fuel Meter"
            .Columns(ConstMeterCode).Width = 75
            .Columns(ConstMeterCode).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstMeterCode).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(ConstMeterCode).ReadOnly = False ' Not AllowLocationItemwiseOnInventory
            .Columns(ConstMeterCode).Visible = False

            .Columns(ConstStartReading).HeaderText = "Opening Reading"
            .Columns(ConstStartReading).Width = 100
            .Columns(ConstStartReading).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstStartReading).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(ConstStartReading).ReadOnly = True ' Not AllowPackEntryOnInventory
            .Columns(ConstStartReading).Visible = False

            .Columns(ConstEndReading).HeaderText = "Ending Reading"
            .Columns(ConstEndReading).Width = 100
            .Columns(ConstEndReading).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstEndReading).DefaultCellStyle.Format = "N" & 3
            .Columns(ConstEndReading).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstEndReading).ReadOnly = False ' Not AllowPackEntryOnInventory
            .Columns(ConstEndReading).Visible = False

            If enableBatchwiseTr Then
                .Columns(ConstSerialNo).HeaderText = "Batch No"
            ElseIf enableClinic Then
                .Columns(ConstSerialNo).HeaderText = "Serial No"
            Else
                .Columns(ConstSerialNo).HeaderText = "IMEI No"
            End If

            .Columns(ConstSerialNo).Width = 100
            .Columns(ConstSerialNo).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstSerialNo).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(ConstSerialNo).ReadOnly = False ' Not AllowPackEntryOnInventory
            If enableSerialnumber Or enableBatchwiseTr Then
                .Columns(ConstSerialNo).Visible = True
            Else
                .Columns(ConstSerialNo).Visible = False
            End If

            .Columns(ConstManufacturingdate).HeaderText = "M. Date" ' warrenty Expiry date
            .Columns(ConstManufacturingdate).Width = 50
            .Columns(ConstManufacturingdate).SortMode = DataGridViewColumnSortMode.NotSortable
            Dim col As New CalendarColumn()
            .Columns.RemoveAt(ConstManufacturingdate)
            col.DataPropertyName = "manufacturing"
            .Columns.Insert(ConstManufacturingdate, col)
            .Columns(ConstManufacturingdate).HeaderText = "M. Date"
            .Columns(ConstManufacturingdate).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(ConstManufacturingdate).Visible = enableBatchwiseTr

            .Columns(ConstWarrentyExpiry).HeaderText = "Expiry Date" ' warrenty Expiry date
            .Columns(ConstWarrentyExpiry).Width = 50
            .Columns(ConstWarrentyExpiry).SortMode = DataGridViewColumnSortMode.NotSortable
            col = New CalendarColumn()
            .Columns.RemoveAt(ConstWarrentyExpiry)
            col.DataPropertyName = "Expiry"
            .Columns.Insert(ConstWarrentyExpiry, col)
            .Columns(ConstWarrentyExpiry).HeaderText = "Expiry Date"
            .Columns(ConstWarrentyExpiry).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            If enableSerialnumber Or enableBatchwiseTr Then
                .Columns(ConstWarrentyExpiry).Visible = True
            Else
                .Columns(ConstWarrentyExpiry).Visible = False
            End If

            .Columns(ConstTotalProduction).HeaderText = "IN"
            .Columns(ConstTotalProduction).Width = 50
            .Columns(ConstTotalProduction).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(ConstQty).Resizable = DataGridViewTriState.False
            .Columns(ConstTotalProduction).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstTotalProduction).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstTotalProduction).Visible = False
            .Columns(ConstTotalProduction).ReadOnly = True

            .Columns(ConstTotalSales).HeaderText = "Sales"
            .Columns(ConstTotalSales).Width = 50
            .Columns(ConstTotalSales).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(ConstQty).Resizable = DataGridViewTriState.False
            .Columns(ConstTotalSales).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstTotalSales).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstTotalSales).Visible = False
            .Columns(ConstTotalSales).ReadOnly = True

            If enableMultiplePointsOnLineItem Then
                .Columns(ConstWoodQty).HeaderText = "Points"
            Else
                .Columns(ConstWoodQty).HeaderText = "Net Weight"
            End If
            .Columns(ConstWoodQty).Width = 75
            .Columns(ConstWoodQty).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(ConstQty).Resizable = DataGridViewTriState.False
            .Columns(ConstWoodQty).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstWoodQty).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstWoodQty).ReadOnly = False
            .Columns(ConstWoodQty).Visible = (enableWoodSale Or enableMultiplePointsOnLineItem)

            .Columns(ConstWoodDiscQty).HeaderText = "Disc (KG)"
            .Columns(ConstWoodDiscQty).Width = 75
            .Columns(ConstWoodDiscQty).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(ConstQty).Resizable = DataGridViewTriState.False
            .Columns(ConstWoodDiscQty).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstWoodDiscQty).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstWoodDiscQty).ReadOnly = False
            .Columns(ConstWoodDiscQty).Visible = enableWoodSale

            .Columns(ConstQty).HeaderText = IIf(enableWoodSale, "Weight", "Qty")
            .Columns(ConstQty).Width = IIf(enableWoodSale, 75, 50)
            .Columns(ConstQty).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(ConstQty).Resizable = DataGridViewTriState.False
            .Columns(ConstQty).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstQty).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstQty).ReadOnly = False

            .Columns(ConstFocQty).HeaderText = "F.Qty"
            .Columns(ConstFocQty).Width = IIf(enableWoodSale, 75, 50)
            .Columns(ConstFocQty).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstFocQty).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstFocQty).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstFocQty).ReadOnly = False
            .Columns(ConstFocQty).Visible = enableFOCQty

            .Columns(ConstMRP).HeaderText = "MRP"
            .Columns(ConstMRP).Width = 70
            .Columns(ConstMRP).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(ConstUPrice).Resizable = DataGridViewTriState.False
            '.Columns(ConstUPrice).ValueType=
            .Columns(ConstMRP).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstMRP).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstMRP).ReadOnly = False
            .Columns(ConstMRP).Visible = False

            .Columns(ConstSP1).HeaderText = "Sales Price"
            .Columns(ConstSP1).Width = 90
            .Columns(ConstSP1).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstSP1).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstSP1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstSP1).ReadOnly = False
            .Columns(ConstSP1).Visible = False

            .Columns(ConstSP2).HeaderText = IIf(enableCreditPrice, "Cr. Price", "D. Price")
            .Columns(ConstSP2).Width = 70
            .Columns(ConstSP2).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstSP2).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstSP2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstSP2).ReadOnly = False
            .Columns(ConstSP2).Visible = False

            .Columns(ConstSP3).HeaderText = "WS Price"
            .Columns(ConstSP3).Width = 70
            .Columns(ConstSP3).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstSP3).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstSP3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstSP3).ReadOnly = False
            .Columns(ConstSP3).Visible = False

            .Columns(ConstUPrice).HeaderText = "Unit Price"
            .Columns(ConstUPrice).Width = 70
            .Columns(ConstUPrice).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(ConstUPrice).Resizable = DataGridViewTriState.False
            '.Columns(ConstUPrice).ValueType=
            .Columns(ConstUPrice).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstUPrice).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstUPrice).ReadOnly = False
            '------------------------------------Not using Now-----------------------------
            .Columns(ConstDisP).HeaderText = "Dis %"
            .Columns(ConstDisP).ReadOnly = Not AllowUnitDiscountEntryOnInventory
            .Columns(ConstDisP).Visible = AllowUnitDiscountEntryOnInventory
            .Columns(ConstDisP).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(ConstDisP).Resizable = DataGridViewTriState.False
            .Columns(ConstDisP).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstDisP).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstDisP).Width = 50
            '-----------------------------------------------------------------
            .Columns(ConstDisAmt).HeaderText = "Dis Amt"
            '.Columns(ConstDisAmt).Width = Me.Width * 7 / 100 '70
            .Columns(ConstDisAmt).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(ConstDisAmt).Resizable = DataGridViewTriState.False
            .Columns(ConstDisAmt).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstDisAmt).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstDisAmt).ReadOnly = Not AllowUnitDiscountEntryOnInventory
            .Columns(ConstDisAmt).Visible = AllowUnitDiscountEntryOnInventory
            'lblDis.Visible = AllowUnitDiscountEntryOnInventory

            .Columns(constItmTot).HeaderText = "Item Total"
            '.Columns(ConstDisAmt).Width = Me.Width * 7 / 100 '70
            .Columns(constItmTot).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(ConstDisAmt).Resizable = DataGridViewTriState.False
            .Columns(constItmTot).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(constItmTot).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(constItmTot).ReadOnly = True
            .Columns(constItmTot).Visible = (ShowTaxOnInventory Or EnableGST)

            If enableGCC Then
                .Columns(ConstTaxP).HeaderText = "Tax%"
            Else
                .Columns(ConstTaxP).HeaderText = IIf(EnableGST, "GST%", "Tax%")
            End If

            .Columns(ConstTaxP).Width = 50
            '.Columns(ConstTaxP).Width = Me.Width * 6 / 100 '60
            .Columns(ConstTaxP).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(ConstTaxP).Resizable = DataGridViewTriState.False
            .Columns(ConstTaxP).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstTaxP).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstTaxP).ReadOnly = (EnableGST Or enableGCC)
            .Columns(ConstTaxP).Visible = (enableGCC Or EnableGST)
            'lblTax.Visible = ShowTaxOnInventory

            If enableGCC Then
                .Columns(ConstTaxAmt).HeaderText = "Tax Amt"
            Else
                .Columns(ConstTaxAmt).HeaderText = IIf(EnableGST, "GST Amt", "Tax Amt")
            End If

            '.Columns(ConstTaxAmt).Width = Me.Width * 7 / 100 '70
            .Columns(ConstTaxAmt).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(ConstTaxAmt).Resizable = DataGridViewTriState.False
            .Columns(ConstTaxAmt).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstTaxAmt).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstTaxAmt).ReadOnly = True
            .Columns(ConstTaxAmt).Visible = (enableGCC Or EnableGST)
            .Columns(ConstTaxAmt).Width = 80

            .Columns(ConstcessAmt).HeaderText = "Cess Amt"
            .Columns(ConstcessAmt).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstcessAmt).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstcessAmt).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstcessAmt).ReadOnly = True
            .Columns(ConstcessAmt).Visible = enablecess
            .Columns(ConstcessAmt).Width = 70

            .Columns(ConstLTotal).HeaderText = "Line Total"
            .Columns(ConstLTotal).Width = 90
            .Columns(ConstLTotal).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(ConstLTotal).Resizable = DataGridViewTriState.False
            .Columns(ConstLTotal).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstLTotal).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstLTotal).ReadOnly = True
            .Columns(ConstLTotal).DefaultCellStyle.BackColor = Color.GreenYellow

            .Columns(ConstUnitOthCost).HeaderText = "Unit Oth Cost"
            '.Columns(ConstUnitOthCost).Width = Me.Width * 9 / 100 '80
            .Columns(ConstUnitOthCost).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(ConstLTotal).Resizable = DataGridViewTriState.False
            .Columns(ConstUnitOthCost).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstUnitOthCost).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstUnitOthCost).ReadOnly = True
            .Columns(ConstUnitOthCost).Visible = False

            .Columns(ConstNUPrice).HeaderText = "Net Unit Price"
            '.Columns(ConstNUPrice).Width = Me.Width * 8 / 100 '70
            .Columns(ConstNUPrice).ReadOnly = True
            .Columns(ConstNUPrice).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(ConstNUPrice).Resizable = DataGridViewTriState.False
            .Columns(ConstNUPrice).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstNUPrice).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstNUPrice).Visible = False

            .Columns(ConstActualOthCost).HeaderText = "Unit Actual Oth Cost"
            .Columns(ConstActualOthCost).Visible = False
            .Columns(ConstActualOthCost).ReadOnly = True
            .Columns(ConstActualOthCost).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstActualOthCost).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(ConstMthd).HeaderText = "Method"
            .Columns(ConstMthd).Visible = False
            .Columns(ConstMthd).ReadOnly = True

            .Columns(ConstUnitVal).HeaderText = "uVal"
            .Columns(ConstUnitVal).Visible = False
            .Columns(ConstUnitVal).ReadOnly = True

            .Columns(ConstDiscOther).HeaderText = "DisOther"
            .Columns(ConstDiscOther).Visible = False
            .Columns(ConstDiscOther).ReadOnly = True

            .Columns(ConstJob).HeaderText = "Job"
            .Columns(ConstJob).Visible = False
            .Columns(ConstJob).ReadOnly = True

            .Columns(ConstJobCostAc).HeaderText = "JobCostAcc"
            .Columns(ConstJobCostAc).Visible = False
            .Columns(ConstJobCostAc).ReadOnly = True

            .Columns(ConstBcodeOrICode).HeaderText = "Barcode/ItmCode"
            .Columns(ConstBcodeOrICode).Visible = False
            .Columns(ConstBcodeOrICode).ReadOnly = True

            .Columns(ConstImpLnId).HeaderText = "ImprtdDocLnNo"
            .Columns(ConstImpLnId).Visible = False
            .Columns(ConstImpLnId).ReadOnly = True

            .Columns(ConstImpDocId).HeaderText = "ImprtdDocID"
            .Columns(ConstImpDocId).Visible = False
            .Columns(ConstImpDocId).ReadOnly = True

            .Columns(ConstActualPrice).HeaderText = "Actual Price"
            .Columns(ConstActualPrice).Visible = False
            .Columns(ConstActualPrice).ReadOnly = True

            .Columns(ConstJobAcID).HeaderText = "JobAccID"
            .Columns(ConstJobAcID).Visible = False
            .Columns(ConstJobAcID).ReadOnly = True

            .Columns(ConstPFraction).HeaderText = "pFraction"
            .Columns(ConstPFraction).Visible = False
            .Columns(ConstPFraction).ReadOnly = True


            .Columns(ConstPMult).HeaderText = "Mult"
            .Columns(ConstPMult).Visible = False
            .Columns(ConstPMult).ReadOnly = True

            .Columns(ConstItemID).HeaderText = "ItemID"
            .Columns(ConstItemID).Visible = False
            .Columns(ConstItemID).ReadOnly = True

            .Columns(ConstImpJobChildTbID).HeaderText = "BaseID"
            .Columns(ConstImpJobChildTbID).Visible = False
            .Columns(ConstImpJobChildTbID).ReadOnly = True

            .Columns(ConstLrow).HeaderText = "Lrow"
            .Columns(ConstLrow).Visible = False
            .Columns(ConstLrow).ReadOnly = True

            .Columns(ConstId).HeaderText = "id"
            .Columns(ConstId).Visible = False
            .Columns(ConstId).ReadOnly = True
            .Columns(ConstqtyChg).Visible = False

            .Columns(ConstCGSTP).HeaderText = "CGST %"
            .Columns(ConstCGSTP).Width = 50
            .Columns(ConstCGSTP).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstCGSTP).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstCGSTP).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstCGSTP).ReadOnly = True
            .Columns(ConstCGSTP).Visible = False

            .Columns(ConstCGSTAmt).HeaderText = "CGST Amt"
            .Columns(ConstCGSTAmt).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstCGSTAmt).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstCGSTAmt).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstCGSTAmt).ReadOnly = True
            .Columns(ConstCGSTAmt).Visible = False

            .Columns(ConstSGSTP).HeaderText = "SGST %"
            .Columns(ConstSGSTP).Width = 50
            .Columns(ConstSGSTP).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstSGSTP).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstSGSTP).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstSGSTP).ReadOnly = True
            .Columns(ConstSGSTP).Visible = False

            .Columns(ConstSGSTAmt).HeaderText = "SGST Amt"
            .Columns(ConstSGSTAmt).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstSGSTAmt).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstSGSTAmt).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstSGSTAmt).ReadOnly = True
            .Columns(ConstSGSTAmt).Visible = False

            .Columns(ConstIGSTP).HeaderText = "IGST %"
            .Columns(ConstIGSTP).Width = 50
            .Columns(ConstIGSTP).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstIGSTP).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstIGSTP).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstIGSTP).ReadOnly = True
            .Columns(ConstIGSTP).Visible = False

            .Columns(ConstIGSTAmt).HeaderText = "IGST Amt"
            .Columns(ConstIGSTAmt).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstIGSTAmt).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstIGSTAmt).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstIGSTAmt).ReadOnly = True
            .Columns(ConstIGSTAmt).Visible = False
            .Columns(ConstIsSerial).Visible = False
            .Columns(ConstIsManufacturingItem).Visible = False
            .Columns(ConstDonotAllowsaveItem).Visible = False
            .Columns(Constcess).Visible = False
            .Columns(ConstcessAc).Visible = False
            .Columns(ConstBatchQty).Visible = False

            .Columns(ConstBatchCost).Visible = False
            .Columns(ConstBatchCost).HeaderText = "Cost"
            .Columns(ConstBatchCost).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstBatchCost).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstBatchCost).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(ConstLineProfit).Visible = False
            .Columns(ConstLineProfit).HeaderText = "Profit" ' Profit colum for customer quotation
            .Columns(ConstLineProfit).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(ConstLineProfit).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstLineProfit).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(ConstLineProfit).ReadOnly = True

            .Columns(ConstRegcess).Visible = False
            .Columns(ConstRegcessAc).Visible = False
            .Columns(ConstAdditionalcess).Visible = False
            .Columns(ConstregularCessAmt).Visible = False
            .Columns(ConstFloodCessAmt).Visible = False
            .Columns(ConstUnitCount).Visible = False
        End With
    End Sub

    Public Function getVoucherName(ByVal id As Integer) As String
        Dim PreFixTb As DataTable
        PreFixTb = _objcmnbLayer._fldDatatable("SELECT [Voucher Name] FROM PreFixTb WHERE id = " & id)
        If PreFixTb.Rows.Count > 0 Then
            Return PreFixTb(0)("Voucher Name")
        Else
            Return ""
        End If
    End Function
    Public Function crtSubVrs(ByRef cmb As System.Windows.Forms.ComboBox, ByRef vrTypeNo As Byte, Optional ByRef Slct As Boolean = False, Optional ByVal isaddnull As Boolean = False) As Boolean
        Dim branch As String
        branch = IIf(UsrBr = "", Dbranch, UsrBr)
        Dim i As Integer
        If PreFixTb Is Nothing Then
            PreFixTb = _objcmnbLayer._fldDatatable(" SELECT * FROM PreFixTb WHERE VrTypeNo = " & vrTypeNo & IIf(branch = "", "", " AND BrId In ('', '" & branch & "')") & " Order by ordNo")
        End If

        cmb.Items.Clear()
        If isaddnull Then
            cmb.Items.Add("")
        End If
        If PreFixTb.Rows.Count > 0 Then
            For i = 0 To PreFixTb.Rows.Count - 1
                cmb.Items.Add(PreFixTb(i)("Voucher Name"))
            Next
            crtSubVrs = True
        Else
            crtSubVrs = False
        End If
        If Slct And cmb.Items.Count > 0 Then
            cmb.SelectedIndex = 0
        End If
    End Function
    Public Sub getVrsDet(ByVal id As Long, ByVal CmnVrFldName As String, ByRef vrPrefix As String, ByRef vrVoucherNo As String, ByRef vrAccountNo1 As String, ByRef vrAccountNo2 As String, Optional ByVal isCustmVr As Boolean = False, Optional ByRef Branch As String = "", Optional ByRef vtype As String = "")
        Dim dtTable As New DataTable
        If id <> 0 Then
            'If PreFixTb Is Nothing Then

            'Else
            '    Dim _qurey As EnumerableRowCollection(Of DataRow)
            '    _qurey = From data In PreFixTb.AsEnumerable() Where data("id") = id Select data
            '    If _qurey.Count > 0 Then
            '        dtTable = _qurey.CopyToDataTable()
            '    Else
            '        dtTable = PreFixTb.Clone
            '    End If
            'End If
            Dim ds As DataSet
            Dim qry As String
            qry = "SELECT  * FROM PreFixTb WHERE Id = " & id
            If UsrBr = "" Then
                qry = qry & " SELECT * FROM InvNos WHERE InvType='" & CmnVrFldName & "'"
            Else
                qry = qry & " SELECT * FROM InvNosBrTb WHERE Brcode='" & UsrBr & "' AND InvType='" & CmnVrFldName & "'"
            End If
            ds = _objcmnbLayer._ldDataset(qry, False)
            PreFixTb = ds.Tables(0)
            dtTable = ds.Tables(1)
            If UsrBr <> "" And dtTable.Rows.Count = 0 Then
                dtTable = _objcmnbLayer._fldDatatable("SELECT * FROM InvNos WHERE InvType='" & CmnVrFldName & "'", False)
            End If
            'PreFixTb = _objcmnbLayer._fldDatatable(qry, False)
            With PreFixTb
                If .Rows.Count > 0 Then
                    If .Rows(0)("Enable") Then
                        'getVrsDet = IIf(Trim(.Rows(0)("PreFix")) = "", "      ", FormatString(.Rows(0)("PreFix"), "!@@@@@")) & Format(PreFixTb.Rows(0)("vrNo"), "00000") & Format(.Rows(0)("ANo"), "00000000") & Format(.Rows(0)("ANo2"), "00000000")
                        vrPrefix = Trim(.Rows(0)("PreFix"))
                        vrVoucherNo = Val(.Rows(0)("vrNo") & "")
                        vrAccountNo1 = Val(.Rows(0)("ANo") & "")
                        vrAccountNo2 = Val(.Rows(0)("ANo2") & "")
                        Branch = Trim("" & .Rows(0)("BrId"))
                        Select Case Val(Val(.Rows(0)("Ctgry") & ""))
                            Case 0
                                vtype = ""
                            Case 1
                                vtype = "Cash"
                            Case 2
                                vtype = "Card"
                            Case 3
                                vtype = "Credit"
                            Case 4
                                vtype = "Gift"
                            Case 5
                                vtype = "Disc"

                        End Select
                        GoTo Ter
                    Else
                        'getVrsDet = Format(.Rows(0)("ANo"), "00000000") & Format(.Rows(0)("ANo2"), "00000000")
                        vrPrefix = ""
                        vrVoucherNo = 0
                        vrAccountNo1 = Val(.Rows(0)("ANo") & "")
                        vrAccountNo2 = Val(.Rows(0)("ANo2") & "")
                        Branch = Trim("" & .Rows(0)("BrId"))

                    End If
                    Select Case Val(Val(.Rows(0)("Ctgry") & ""))
                        Case 0
                            vtype = ""
                        Case 1
                            vtype = "Cash"
                        Case 2
                            vtype = "Card"
                        Case 3
                            vtype = "Credit"
                        Case 4
                            vtype = "Gift"
                        Case 5
                            vtype = "Disc"

                    End Select

                End If
            End With
        Else
            If UsrBr = "" Then
df:
                dtTable = _objcmnbLayer._fldDatatable("SELECT * FROM InvNos WHERE InvType='" & CmnVrFldName & "'", False)
            Else
                dtTable = _objcmnbLayer._fldDatatable("SELECT * FROM InvNosBrTb WHERE Brcode='" & UsrBr & "' AND InvType='" & CmnVrFldName & "'", False)
                If dtTable.Rows.Count = 0 Then GoTo df
            End If
        End If
        '        If UsrBr = "" Then
        'df:
        '            dtTable = _objcmnbLayer._fldDatatable("SELECT * FROM InvNos WHERE InvType='" & CmnVrFldName & "'", False)
        '        Else
        '            dtTable = _objcmnbLayer._fldDatatable("SELECT * FROM InvNosBrTb WHERE Brcode='" & UsrBr & "' AND InvType='" & CmnVrFldName & "'", False)
        '            If dtTable.Rows.Count = 0 Then GoTo df
        '        End If
        'If dtInvNos Is Nothing Then

        'Else
        '    dtTable = dtInvNos
        'End If



        With dtTable
            If .Rows.Count > 0 Then
                vrPrefix = Trim(.Rows(0)("Prefix") & "")
                vrVoucherNo = Val(.Rows(0)("InvNo"))
            End If
        End With
Ter:
        'Return getVrsDet
    End Sub
    Public Function getvrsId(ByVal vrType As String, ByVal vrTypeNo As Integer) As Integer
        Dim dtTable As DataTable
        PreFixTb = _objcmnbLayer._fldDatatable("SELECT  Id FROM PreFixTb WHERE VrTypeNo=" & vrTypeNo & " AND [Voucher Name] = '" & vrType & "'", False)

        'Dim _qurey As EnumerableRowCollection(Of DataRow)
        '_qurey = From data In PreFixTb.AsEnumerable() Where data("VrTypeNo") = vrTypeNo And data("Voucher Name") = vrType Select data
        'If _qurey.Count > 0 Then
        '    dtTable = _qurey.CopyToDataTable()
        'Else
        '    dtTable = PreFixTb.Clone
        'End If
        If PreFixTb.Rows.Count > 0 Then
            Return PreFixTb(0)("id")
        End If
    End Function
    Public Function getvrsName(ByVal id As Integer) As String
        Dim PreFixTb As DataTable
        getvrsName = ""
        PreFixTb = _objcmnbLayer._fldDatatable("SELECT  * FROM PreFixTb WHERE id = '" & id & "'", False)
        If PreFixTb.Rows.Count > 0 Then
            Return PreFixTb(0)("Voucher Name")
        End If
    End Function
    Public Function RowOnGrid(ByVal strt As Integer, ByVal grd As DataGridView, ByVal col As Integer, ByVal SrchText As String) As Long
        Dim RowNo As Long = 0
        With grd
            For j = strt To grd.RowCount - 1
                If .Item(col, j).Value.ToString.StartsWith(SrchText, StringComparison.CurrentCultureIgnoreCase) Then
                    RowNo = j
                    Exit For
                End If
            Next
        End With
        Return RowNo
    End Function
    Public Function getServerDate() As Date
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT GETDATE() Dt")
        Return Format(dt(0)("Dt"), DtFormatTime)
    End Function
    Public Function EntriesValidation(ByVal txtSearch As String, ByVal tIndex As Integer) As DataTable
        Dim strMyQry As String = ""
        Select Case tIndex
            Case 1 'AccountMaster
                strMyQry = "Select Alias,AccDescr,CreditLimit,DeptId,TermsId,AreaCode,AccountNo,BranchId,accid from AccMast Where Alias ='" & MkDbSrchStr(txtSearch) & "'"
            Case 2 '"txtFC"
                strMyQry = "Select CurrencyCode,CurrencyRate,[Fraction Code],[Decimal Places] from CurrencyTb Where CurrencyCode='" & MkDbSrchStr(txtSearch) & "'"
            Case 3 '"txtTerms"
                strMyQry = "Select TermsId,TermsDescr,NDays from TermsTb Where TermsID='" & MkDbSrchStr(txtSearch) & "'"
            Case 4 '"txtLocation"
                strMyQry = "Select LocationId,Description from LocationTb where LocationId='" & MkDbSrchStr(txtSearch) & "'"
            Case 5 '"txtJob"
                strMyQry = "Select Jobid,jobcode,jobname from jobtb Where jobcode='" & MkDbSrchStr(txtSearch) & "'"
            Case 6 '"txtArea"
                strMyQry = "Select AreaCode,AreaDescr from AreaTb Where AreaCode='" & MkDbSrchStr(txtSearch) & "'"
            Case 7 '"txtSalesman"
                strMyQry = "Select SManCode,SManName,Tel from SalesmanTb Where SmanCode='" & MkDbSrchStr(txtSearch) & "'"
            Case 8 'AccountMaster
                strMyQry = "Select Alias,AccDescr,CreditLimit,DeptId,TermsId,AreaCode,accid from AccMast Where AccDescr ='" & MkDbSrchStr(txtSearch) & "'"
            Case 9 'AccountMaster
                strMyQry = "Select Alias,AccDescr,CreditLimit,DeptId,TermsId,AreaCode,AccountNo,BranchId,accid from AccMast Where accid ='" & Val(txtSearch) & "'"
        End Select
        Return _objcmnbLayer._fldDatatable(strMyQry, False)
    End Function
    Public Sub SetPanel(ByVal dvData As DataGridView, ByVal srchTxtId As Integer, ByVal noCol As Integer, ByVal Wcol As Integer, ByVal CWidth As Integer)
        Dim strMyQry As String
        strMyQry = ""
        Select Case srchTxtId
            Case 3, 4 'Customer,Customer name
                strMyQry = AssignAccSQLStr(0, UsrBr, 2)
            Case 5, 6  'PurchAlias,PurchName
                strMyQry = AssignAccSQLStr(4, UsrBr, 2)
            Case 7  'FC
                strMyQry = "SELECT CurrencyCode,CurrencyRate Rate, Description, [Fraction Code] AS FractionCode, [Decimal Places] as NoOfDecimal FROM CurrencyTb ORDER BY CurrencyCode"
            Case 8  'JOB
                strMyQry = "SELECT jobcode as Code,jobname,JobId from JobTb Order by jobId "
            Case 9 'Location

                strMyQry = "SELECT LocationId as Code," & _
                                    " Description FROM " & _
                                    " LocationTb ORDER BY LocationId"
            Case 10  'Terms
                strMyQry = "SELECT TermsId as [Terms Id], TermsDescr as Description, NDays FROM TermsTb ORDER BY TermsId"
            Case 11 'Area
                strMyQry = "SELECT  AreaCode as [Area Code], AreaDescr as Name FROM AreaTb ORDER BY AreaCode"
            Case 12 'Salesman
                strMyQry = "SELECT SManCode as [Salesman Id]," & _
                    " SManName as [Salesman Name], Commission, Address1," & _
                    " Address2, Tel as Telephone FROM " & _
                    "SalesmanTb ORDER BY SManCode"
            Case 13 'for all account
                strMyQry = AssignAccSQLStr(8, UsrBr, 2)
            Case 14
                strMyQry = "SELECT EntryRef as Name FROM AccTrDet GROUP BY EntryRef"
            Case 15
                strMyQry = "SELECT OthInf as Name FROM AccTrCmn WHERE JVType='RV' GROUP BY OthInf"
            Case 16
                strMyQry = "SELECT OthInf as Name FROM AccTrCmn WHERE JVType='PV' GROUP BY OthInf"
        End Select
        dvData.DataSource = Nothing
        If strMyQry = "" Then GoTo SetHeadOnly
        _vSrchdatatable = _objcmnbLayer._fldDatatable(strMyQry, False)

        dvData.DataSource = _vSrchdatatable
SetHeadOnly:
        With dvData
            .ReadOnly = True
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = True
            If .ColumnCount > 1 Then .AutoResizeColumn(1)
            .ColumnHeadersHeight = 100
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersDefaultCellStyle.BackColor = Color.Olive
            .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold)
            .RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 9.0!)
            Dim i As Integer
            For i = noCol To .ColumnCount - 1
                .Columns(i).Visible = False
            Next
            For i = 0 To noCol
                .Columns(i).Width = dvData.Width * 20 / 100
            Next
            .Columns(Wcol).Width = CWidth
        End With
    End Sub
    Public Sub SearchPanel(ByVal dvData As DataGridView, ByVal txtSearch As String, ByVal SearchIndex As Integer)
        Dim bDatatable As DataTable
        If Trim(txtSearch) <> "" Then
            If _vSrchdatatable.Rows.Count = 0 Then Exit Sub
            Dim _qurey = From data In _vSrchdatatable.AsEnumerable() Where data(SearchIndex).ToString.StartsWith(Trim(txtSearch), StringComparison.OrdinalIgnoreCase) Select data
            If _qurey.Count > 0 Then
                bDatatable = _qurey.CopyToDataTable()

                dvData.DataSource = bDatatable
            Else
                bDatatable = _vSrchdatatable.Clone
                dvData.DataSource = bDatatable
            End If
        Else
            dvData.DataSource = _vSrchdatatable
        End If
    End Sub
    Public Function GrpSetOn(ByRef Accno As Long) As String
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("select  isnull(GrpSetOn,'') GrpSetOn from AccMast inner join dbo.S1AccHd on AccMast.S1AccId=dbo.S1AccHd.S1AccId where AccountNo=" & Accno)
        If dt.Rows.Count > 0 Then
            Return (dt(0)("GrpSetOn"))
        End If
        Return ""
    End Function
    Public Function getVouchernumber(ByVal vrtype As String) As Integer
        Dim dt As DataTable
        Dim _qurey = From data In dtvoucherTypes.AsEnumerable() Where data("VoucherType") = vrtype Select data
        If _qurey.Count > 0 Then
            dt = _qurey.CopyToDataTable()
        Else
            dt = dtvoucherTypes.Clone
        End If
        'dt = _objcmnbLayer._fldDatatable(getVouchernumberStr(vrtype))
        If dt.Rows.Count > 0 Then
            Return Val(dt(0)("vrno"))
        End If
    End Function
    Public Sub CreateSetoffTable(ByRef dttable As DataTable)
        dttable.Columns.Add(New DataColumn("JVDate", GetType(Date)))
        dttable.Columns.Add(New DataColumn("Reference", GetType(String)))
        dttable.Columns.Add(New DataColumn("Tag", GetType(String)))
        dttable.Columns.Add(New DataColumn("Type", GetType(String)))
        dttable.Columns.Add(New DataColumn("Assign", GetType(Double)))
        dttable.Columns.Add(New DataColumn("Balance", GetType(Double)))
        dttable.Columns.Add(New DataColumn("InvAmt", GetType(Double)))
        dttable.Columns.Add(New DataColumn("installment", GetType(Double)))
        dttable.Columns.Add(New DataColumn("SetOffAmt", GetType(Double)))
        dttable.Columns.Add(New DataColumn("FCAmt", GetType(Double)))
        dttable.Columns.Add(New DataColumn("CurrencyCode", GetType(String)))
        dttable.Columns.Add(New DataColumn("Rate", GetType(Double)))
        dttable.Columns.Add(New DataColumn("EntryRef", GetType(String)))
        dttable.Columns.Add(New DataColumn("Fcdec", GetType(Integer)))
        dttable.Columns.Add(New DataColumn("Jvnum", GetType(Integer)))
        dttable.Columns.Add(New DataColumn("LPONo", GetType(String)))
        dttable.Columns.Add(New DataColumn("LPODate", GetType(String)))
        dttable.Columns.Add(New DataColumn("JobCode", GetType(String)))
        dttable.Columns.Add(New DataColumn("setoffcount", GetType(Integer)))

    End Sub
    Public Function getConstantAccounts(ByVal setno As Integer) As Long
        'If UsrBr = "" Then
        '    dt = _objcmnbLayer._fldDatatable("Select accid from accmast where AccSetId like '%" & Format(accid, "00") & "%'")
        'Else
        '    dt = _objcmnbLayer._fldDatatable("SELECT accid FROM BranchAccSet WHERE branchcode='" & UsrBr & "' and setno=" & accid - 1)
        'End If
        Dim dtTable As DataTable
        Dim _qurey As EnumerableRowCollection(Of DataRow)
        _qurey = From data In dtDefAcc.AsEnumerable() Where data("setno") = setno Select data
        If _qurey.Count > 0 Then
            dtTable = _qurey.CopyToDataTable()
        Else
            dtTable = dtDefAcc.Clone
        End If
        If dtTable.Rows.Count > 0 Then
            Return dtTable(0)("accid")
        End If
        Return 0
    End Function

    Public Sub createInstantItem(ByVal ItemCode As String, ByVal Descr As String, ByVal Unit As String, ByVal salesPrice As Double, ByVal Category As String)
        _objItmMast = New clsItemMast_BL
        With _objItmMast
            .ItemId = 0
            .ItemCode = ItemCode
            .Descr = Strings.Trim(Descr)
            .Unit = Unit
            .salesPrice = salesPrice
            .Category = Category
            .LstModiBy = ""
            .Createdby = Strings.Trim(PublicVariables.CurrentUser)
            .CreatedDt = DateTime.Now
            .LstModiDt = DateTime.Now
            .Ismodi = False
            ._saveItemMast()
        End With

    End Sub

    Public Sub updateClosingBalanceForInvoice(ByVal trid As Long, Optional ByVal isjob As Boolean = False)
        Dim dt As DataTable
        If isjob Then
            dt = _objcmnbLayer._fldDatatable("SELECT Accountno FROM AccTrCmn left join acctrdet on acctrcmn.linkno=acctrdet.linkno WHERE jbInvid = " & trid, False)
        Else
            dt = _objcmnbLayer._fldDatatable("SELECT Accountno FROM AccTrCmn left join acctrdet on acctrcmn.linkno=acctrdet.linkno WHERE AccTrCmn.Linkno = " & trid, False)
        End If

        Dim num2 As Integer = (dt.Rows.Count - 1)
        Dim i As Integer = 0
        Do While (i <= num2)
            _objcmnbLayer.updateClosingBalance(Val(dt(i)("Accountno") & ""))
            i += 1
        Loop
    End Sub

    Public Sub checkOrUncheckTag(ByVal grd As DataGridView, ByVal e As DataGridViewCellEventArgs, ByVal intColumnIndex As Integer)
        Try
            If e.RowIndex < 0 Then
                Dim i As Integer
                For i = 0 To grd.RowCount - 1
                    grd.Item(intColumnIndex, i).Value = IIf(Trim(grd.Tag & "") = "Y", "", "Y")
                Next
                grd.Tag = IIf(grd.Tag = "Y", "", "Y")
            Else
                If (e.ColumnIndex = intColumnIndex) Then
                    grd.Item(intColumnIndex, e.RowIndex).Value = IIf(grd.Item(intColumnIndex, e.RowIndex).Value = "Y", "", "Y")
                End If
            End If
            
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Function getAccBal(ByVal accid As Long, Optional ByVal linkno As Long = 0) As Double
        Dim dt As DataTable = _objcmnbLayer._fldDatatable("SELECT isnull(OpnBal,0)+isnull(bal,0) bal from AccMast LEFT JOIN (Select sum(DealAmt) bal,accountno from AccTrDet " & _
                                                          "left join AccTrCmn on AccTrCmn.linkno=AccTrDet.linkno where jvtype<>'OB' and " & _
                                                          IIf(linkno = 0, "AccTrCmn.linkno<>" & linkno, "AccTrCmn.linkno<" & linkno) & " group by accountno) Tr On Accmast.accid=tr.accountno where accid=" & accid, False)
        If dt.Rows.Count > 0 Then
            Return dt(0)("bal")
        End If
        Return 0
    End Function





    Public Function getAccAegBal(ByVal accid As Long, ByVal curDate As DateTime, ByVal duedays As Integer) As Double
        Dim duedate As Date = DateAdd(DateInterval.Day, duedays * -1, curDate)
        _objTr = New clsAccountTransaction
        Dim dt As DataTable = _objTr.returnldTrans(accid, 0, 0)
        If dt.Rows.Count > 0 Then
            Dim _qurey = From data In dt.AsEnumerable() Where data("JVDate") < duedate Select data
            If _qurey.Count > 0 Then
                Return _qurey.Count
            End If
        End If
        Return 0
    End Function

    Public Function readXml() As String
        If connectionstring <> "" Then Return connectionstring
        Dim con As String = ""
        If File.Exists(Application.StartupPath + "/" + "ConString.xml") Then
            Dim fs As New FileStream(Application.StartupPath + "/" + "ConString.xml", FileMode.Open)
            Dim reader As XmlReader = New XmlTextReader(fs)
            While reader.Read()
                If reader.Name = "ConnectionString" Then
                    con = reader.GetAttribute("String")
                    Exit While
                End If
            End While
            fs.Close()
            reader.Close()
            fs.Dispose()
        End If
        Return con
    End Function
    Public Sub AddGST(ByRef dtTax As DataTable, ByVal HsnCode As String, ByVal amount As Double, ByVal accountName As String, ByVal accountNo As Long, ByVal IsColleted As Boolean)
        Dim dtGst As DataTable
        If IsColleted Then
            dtGst = _objcmnbLayer._fldDatatable("SELECT HSNCode,'' AccountName ,convert(money, 0) Amount,0 ACid From GSTTb " & _
                                                " where 1=2", False)
        End If
        Dim dtrow As DataRow
        dtrow = dtTax.NewRow
        dtrow("HSNCode") = HsnCode
        dtrow("Amount") = amount
        dtrow("AccountName") = accountName
        dtrow("ACid") = accountNo
        dtTax.Rows.Add(dtrow)
    End Sub
    Public Sub CreateTaxTable(ByRef dtTax As DataTable)
        dtTax = New DataTable
        'create structure of dttax
        '_objGst = New clsGSTMaster
        'dtGST = _objGst.returnGstMaster(0)
        'dtTax = _objcmnbLayer._fldDatatable("SELECT 0 Slno,'' AccountName ,convert(money, 0) Amount,0 ACid From GSTTb where 1=2", False)
        dtTax.Columns.Add(New DataColumn("Slno", GetType(Integer)))
        dtTax.Columns.Add(New DataColumn("AccountName", GetType(String)))
        dtTax.Columns.Add(New DataColumn("Amount", GetType(Double)))
        dtTax.Columns.Add(New DataColumn("ACid", GetType(Integer)))
    End Sub
    Public Sub toAssignDownListToText(ByVal _ObjTextbox As TextBox, ByVal _objListOfData As List(Of String))

        'AutoComplete collection that will help to filter keep the records.
        Dim MySource As New AutoCompleteStringCollection()

        'Records binded to the AutocompleteStringCollection.
        MySource.AddRange(_objListOfData.ToArray)

        'this AutocompleteStringcollection binded to the textbox as custom
        'source.
        _ObjTextbox.AutoCompleteCustomSource = MySource
        ''OR you can use this for combobox
        ''ComboBox1.AutoCompleteCustomSource = MySource

        'Auto complete mode set to suggest append so that it will sugesst one
        'or more suggested completion strings it has both ‘Suggest’ and
        '‘Append’ functionality
        _ObjTextbox.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        ''OR you can use this for combobox
        ''ComboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend

        'Set to Custom source we have filled already
        _ObjTextbox.AutoCompleteSource = AutoCompleteSource.CustomSource
        ''OR you can use this for combobox
        ''ComboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource
    End Sub
    Public Function toLoadAutoFillListItems(ByVal Desc As String, ByVal table As String) As List(Of String)
        Dim dt As New DataTable
        Dim lst As New List(Of String)

        Dim SqlQry As String = "select " & Desc & " from " & table '& '" where Deleted ='False'"
        dt = _objcmnbLayer._fldDatatable(SqlQry)
        For i = 0 To dt.Rows.Count - 1
            lst.Add(dt(i)(0))
        Next
        Return lst

    End Function
    Public Function toLoadAutoFillListItemsFromDt(ByVal dt As DataTable) As List(Of String)
        Dim lst As New List(Of String)

        'Dim SqlQry As String = "select " & Desc & " from " & table '& '" where Deleted ='False'"
        'dt = _objcmnbLayer._fldDatatable(SqlQry)
        For i = 0 To dt.Rows.Count - 1
            lst.Add(dt(i)(0))
        Next
        Return lst

    End Function
    Public Function getLastSalesAmt(ByVal accid As Long, ByVal itemid As Long, ByVal iscustomerwise As Boolean, ByVal trtype As String, Optional ByVal defaultPrice As Double = 0) As Double
        Dim dt As DataTable
        If enablefetchLastPrice Then
            dt = _objcmnbLayer._fldDatatable("SELECT Top 1 UnitCost FROM ItmInvTrTb " & _
                                                    "LEFT JOIN ItmInvCmnTb ON ItmInvCmnTb.Trid=ItmInvTrTb.Trid " & _
                                                    "where " & IIf(iscustomerwise, " CSCode=" & accid & " AND ", "") & _
                                                    " Itemid=" & itemid & " AND trtype='" & trtype & "' ORDER BY TrDate Desc,id Desc")

           
            If dt.Rows.Count > 0 Then
                Return dt(0)("UnitCost")
            End If
            GoTo def
        Else
def:
            Return defaultPrice
        End If

    End Function
    Public Function getSumOfDataTable(ByVal dt As DataTable, ByVal columnIndex As String) As Double
        Return dt.Compute("SUM(" & columnIndex & ")", String.Empty)
    End Function
    ' get associated icon (as bitmap).
    Private Function GetFileIcon(ByVal fileExt As String, Optional ByVal ICOsize As IconSize = IconSize.SHGFI_SMALLICON) As Bitmap
        Dim shinfo As New SHFILEINFO
        shinfo.szDisplayName = New String(Chr(0), MAX_PATH)
        shinfo.szTypeName = New String(Chr(0), 80)
        SHGetFileInfo(fileExt, FILE_ATTRIBUTE_NORMAL, shinfo, Marshal.SizeOf(shinfo), SHGFI_ICON Or ICOsize Or SHGFI_USEFILEATTRIBUTES)
        Dim bmp As Bitmap = System.Drawing.Icon.FromHandle(shinfo.hIcon).ToBitmap
        DestroyIcon(shinfo.hIcon) ' must destroy icon to avoid GDI leak!
        Return bmp ' return icon as a bitmap
    End Function
    Public Function getWoodDiscount(ByVal itemid As Long, ByVal netQty As Double) As Double
        Dim dt As DataTable
        Dim dqty As Double
        Dim pqty As Double
        Dim aboveTenTon As Double
        Dim netqtyOnrange As Double
        If netQty > 10000 Then
            netqtyOnrange = netQty - (netQty - 10000)
        Else
            netqtyOnrange = netQty
        End If
        dt = _objcmnbLayer._fldDatatable("SELECT qty,dqty,aboveTenTon FROM WoodQtyDiscountTb LEFT JOIN INVITM ON INVITM.Itemid=WoodQtyDiscountTb.itemid WHERE WoodQtyDiscountTb.Itemid=" & itemid)
        For i = 0 To dt.Rows.Count - 1
            If netqtyOnrange > pqty And netqtyOnrange <= dt(i)("qty") Then
                dqty = dt(i)("dqty")
            End If
            pqty = dt(i)("qty")
            aboveTenTon = dt(i)("aboveTenTon")
        Next
        If netQty > 10000 Then
            netQty = netQty - 10000
            dqty = ((netQty / 1000) * aboveTenTon) + dqty
        End If
        Return dqty
    End Function
    Public Function returnBitMapForDoc(ByVal fileExt As String) As Bitmap
        Return GetFileIcon(fileExt, IconSize.SHGFI_LARGEICON)
    End Function
    Public Function getCessDate() As Date
        Dim dt As DataTable
        If enablecess Then
            dt = _objcmnbLayer._fldDatatable("Select cessdate from CompanyTb")
            If dt.Rows.Count > 0 Then
                If Not IsDBNull(dt(0)("cessdate")) Then
                    Return DateValue(dt(0)("cessdate"))
                End If

            End If
        End If

        Return DateValue("01/01/1950")
    End Function
    Public Function isPrintTaxPrice() As Boolean

        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select isnull(isTaxItemInBarcode,0)isTaxItemInBarcode from CompanyTb")
        If dt.Rows.Count > 0 Then
            Return dt(0)("isTaxItemInBarcode")
        End If
        Return False
    End Function
    Public Function getTerms() As String
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT quoteTerms FROM CompanyTb")
        If dt.Rows.Count > 0 Then
            Return Trim(dt(0)(0) & "")
        End If
        Return ""
    End Function
    Public Function getroundoffAMT(ByVal NetAmt As String, ByRef retrnAmt As Double) As Integer
        Dim ntAmt As Double
        Dim lntAmt As String
        Dim decInd As Integer = NetAmt.IndexOf(".")
        If decInd < 0 Then Return 0
        lntAmt = Mid(NetAmt, NetAmt.IndexOf(".") + 1)
        ntAmt = CDbl(lntAmt)
        If ntAmt >= CDbl("0.50") Then
            Select Case roundoffGtrThn50
                Case 0
                    retrnAmt = ntAmt
                    Return 1
                Case 1
                    retrnAmt = ntAmt - CDbl("0.50")
                    Return 1
                Case 2
                    retrnAmt = CDbl("1.00") - ntAmt
                    Return 0
            End Select
        ElseIf ntAmt = 0 Then
            retrnAmt = 0
            Return 0
        Else
            Select Case roundoffLessThn50
                Case 0
                    retrnAmt = ntAmt
                    Return 1
                Case 1
                    retrnAmt = CDbl("0.50") - ntAmt
                    Return 0
                Case 2
                    retrnAmt = CDbl("1.00") - ntAmt
                    Return 0
            End Select
        End If
        Return 0
    End Function
    Public Function chekItemImported(ByVal id As Long) As Boolean
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT foundImport FROM (SELECT foundImport FROM (SELECT impDocSlno foundImport FROM ItmInvTrTb " & _
                                  "UNION ALL SELECT ImpDocLnNo FROM DocTranTb)tr  GROUP BY foundImport) TR where foundImport=" & id)
        If dt.Rows.Count > 0 Then
            Return True
        End If
        Return False
    End Function
    Public Function checkReconciliation(ByVal grdVoucher As DataGridView, ByVal ConstAccountNo As Integer, ByVal ConstAmount As Integer, ByVal ConstName As Integer, ByVal rindex As Integer) As Boolean
        Dim accid As Long
        Dim dt As DataTable
        accid = grdVoucher.Item(ConstAccountNo, rindex).Value
        If accid > 0 Then
            dt = _objcmnbLayer._fldDatatable("select isnull(isreconcil,0)isreconcil from accmast where accid=" & accid)
            If dt.Rows.Count > 0 Then
                Dim isreconcil As Integer
                isreconcil = dt(0)("isreconcil")
                If isreconcil > 0 Then
                    Dim amt As Double
                    If Val(grdVoucher.Item(ConstAmount, rindex).Value & "") > 0 Then
                        amt = CDbl(grdVoucher.Item(ConstAmount, rindex).Value)
                        If amt > 0 Then
                            dt = _objcmnbLayer._fldDatatable("select isnull(bal,0)bal,isnull(reconbal,0)reconbal from (select ISNULL(trbal,0)+OpnBal bal,reconbal from accmast " & _
                                                             "left join (select sum(dealamt) trbal,AccountNo from AccTrDet group by AccountNo)AccTrDet on AccTrDet.AccountNo=accmast.AccId " & _
                                                             "left join (select sum(dealamt)reconbal,AccountNo from AccTrDet where isnull(RecntnTag,0)=0 and DealAmt>0 group by AccountNo)noCleared on noCleared.AccountNo=accmast.AccId " & _
                                                             "where AccId=" & accid & ")tr")
                            If dt.Rows.Count > 0 Then
                                If amt > (Val(dt(0)("bal")) - Val(dt(0)("reconbal"))) Then
                                    MsgBox("Only available reconciliation Amount is for " & grdVoucher.Item(ConstName, rindex).Value & " :- " & (Val(dt(0)("bal")) - Val(dt(0)("reconbal"))), MsgBoxStyle.Exclamation)
                                    Return False
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End If
        Return True
    End Function
    Public Sub getdefaultTaxAccounts(ByRef defaultInput As Integer, ByRef defaultOutput As Integer, ByRef defaultInputOnImport As Integer, ByRef defaultOutputOnImport As Integer)
        'defaultInput = getConstantAccounts(25)
        'defaultOutput = getConstantAccounts(24)
        defaultInputOnImport = getConstantAccounts(26)
        defaultOutputOnImport = getConstantAccounts(27)
        'dtTable = _objcmnbLayer._fldDatatable("SELECT accid FROM AccMast WHERE AccSetId Like '%25%'")
        'If dtTable.Rows.Count > 0 Then defaultInput = dtTable(0)("accid")
        'dtTable = _objcmnbLayer._fldDatatable("SELECT accid FROM AccMast WHERE AccSetId Like '%24%'")
        'If dtTable.Rows.Count > 0 Then defaultOutput = dtTable(0)("accid")
        'dtTable = _objcmnbLayer._fldDatatable("SELECT accid FROM AccMast WHERE AccSetId Like '%26%'")
        'If dtTable.Rows.Count > 0 Then defaultInputOnImport = dtTable(0)("accid")
        'dtTable = _objcmnbLayer._fldDatatable("SELECT accid FROM AccMast WHERE AccSetId Like '%27%'")
        'If dtTable.Rows.Count > 0 Then defaultOutputOnImport = dtTable(0)("accid")
    End Sub
    Public Sub createMultipleDebitTb(ByRef dtMultipleDebits As DataTable)
        dtMultipleDebits = New DataTable
        With dtMultipleDebits
            .Columns.Add(New DataColumn("Alias", GetType(String)))
            .Columns.Add(New DataColumn("AccDescr", GetType(String)))
            .Columns.Add(New DataColumn("accAmt", GetType(Double)))
            .Columns.Add(New DataColumn("reference", GetType(String)))
            .Columns.Add(New DataColumn("accid", GetType(String)))
            .Columns.Add(New DataColumn("dbtid", GetType(String)))
        End With
    End Sub
    Public Function createMultiplePointsOnSales() As DataTable
        Dim dtPoints As New DataTable
        With dtPoints
            .Columns.Add(New DataColumn("itemname", GetType(String)))
            .Columns.Add(New DataColumn("sman", GetType(String)))
            .Columns.Add(New DataColumn("points", GetType(Integer)))
            .Columns.Add(New DataColumn("Trid", GetType(Long)))
            .Columns.Add(New DataColumn("DetId", GetType(Long)))
            .Columns.Add(New DataColumn("Itemid", GetType(Long)))
            .Columns.Add(New DataColumn("RowIndex", GetType(Integer)))

        End With
        'dtSerialNo = _objcmnbLayer._fldDatatable("SELECT *,0 dtTbIndex from SerialNoTrTb where 1=2")
        dtPoints.Rows.Clear()
        Return dtPoints
    End Function
    Public Function encrypt(ByVal sPlainText As String) As String
        Dim enc As System.Text.UTF8Encoding
        Dim encryptor As ICryptoTransform
        Dim decryptor As ICryptoTransform
        Dim enText As String = ""
        Dim KEY_128 As Byte() = {42, 1, 52, 67, 231, 13, 94, 101, 123, 6, 0, 12, 32, 91, 4, 111, 31, 70, 21, 141, 123, 142, 234, 82, 95, 129, 187, 162, 12, 55, 98, 23}
        Dim IV_128 As Byte() = {234, 12, 52, 44, 214, 222, 200, 109, 2, 98, 45, 76, 88, 53, 23, 78}
        Dim symmetricKey As RijndaelManaged = New RijndaelManaged()
        symmetricKey.Mode = CipherMode.CBC

        enc = New System.Text.UTF8Encoding
        encryptor = symmetricKey.CreateEncryptor(KEY_128, IV_128)
        decryptor = symmetricKey.CreateDecryptor(KEY_128, IV_128)
        If Not String.IsNullOrEmpty(sPlainText) Then
            Dim memoryStream As MemoryStream = New MemoryStream()
            Dim cryptoStream As CryptoStream = New CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write)
            cryptoStream.Write(enc.GetBytes(sPlainText), 0, sPlainText.Length)
            cryptoStream.FlushFinalBlock()
            enText = Convert.ToBase64String(memoryStream.ToArray())
            memoryStream.Close()
            cryptoStream.Close()
        End If
        Return enText
    End Function
    Public Function returnHsnCodeDet(ByVal hsncode As String) As DataTable
        Dim dt As DataTable
        Dim _qurey As EnumerableRowCollection(Of DataRow)
        _qurey = From data In dtGST.AsEnumerable() Where data("HSNCODE") = hsncode Select data
        If _qurey.Count > 0 Then
            dt = _qurey.CopyToDataTable()
        Else
            dt = dtGST.Clone
        End If
        Return dt
    End Function
    <DllImport("shell32.dll", CharSet:=CharSet.Auto)> _
    Private Function SHGetFileInfo( _
                ByVal pszPath As String, _
                ByVal dwFileAttributes As Int32, _
                ByRef psfi As SHFILEINFO, _
                ByVal cbFileInfo As Int32, _
                ByVal uFlags As Int32) As IntPtr
    End Function

    <DllImport("user32.dll", SetLastError:=True)> _
    Private Function DestroyIcon(ByVal hIcon As IntPtr) As Boolean
    End Function
End Module
