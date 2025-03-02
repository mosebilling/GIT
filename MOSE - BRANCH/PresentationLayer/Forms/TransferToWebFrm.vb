Public Class TransferToWebFrm
    Delegate Sub GenericDelegate(ByVal Mname As String, ByVal RecName As String, ByVal rec As Integer, ByVal count As Integer)
    Public typeofTransfer As Integer
    Public updateAllInvoice As Boolean
    Public DeliveredBy As String
    'Public dtTable As DataTable
    Public dtTable As DataTable
    Public isremove As Boolean
    Public dbookdate1 As Date
    Public dbookdate2 As Date
    Private _objTr As New clsAccountTransaction
    Public isdaybook, isAccountbalance, isprofitloss, isfinancialStatus As Boolean
    Public updateDaybookUptodate As Boolean
    Private Sub TransferToWebFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Worker.WorkerReportsProgress = True
        Worker.WorkerSupportsCancellation = True
        Worker.RunWorkerAsync()
    End Sub

    Public Sub status(ByVal Mname As String, ByVal RecName As String, ByVal rec As Integer, ByVal count As Integer)
        If Me.InvokeRequired Then
            Dim d As New GenericDelegate(AddressOf status)
            Me.Invoke(d, Mname, RecName, rec, count)
        Else
            lblmodule.Text = Mname
            'lblrec.Text = cnt1 + cnt2 + cnt3 + cnt4 & " / " & count
            'If rec = 19 Then
            '    MsgBox("")
            'End If
            lblrec.Text = rec & " / " & count
            'lblmodule.Refresh()
            'lblrec.Refresh()
            If rec = 0 Then
                pb.Value = 0
            Else
                'If pb.Value + (100 / count) > 100 Then
                '    pb.Value = 100
                'Else
                '    pb.Value = pb.Value + (rec * 100 / count)
                'End If
                pb.Value = rec * 100 / count
                'pb.Value = (cnt1 + cnt2 + cnt3 + cnt4) * 100 / count
            End If

        End If
    End Sub
    Private _objweb As New webDatalayer
    Private Sub Worker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles Worker.DoWork
        'typeofTransfer=0 : all products
        'typeofTransfer=1 : only changed products
        'typeofTransfer=2 : level and group
        'typeofTransfer=3 : GST Master
        'typeofTransfer=4 : Refresh sales order
        'typeofTransfer=5 : Invoioce Update for collection
        'typeofTransfer=6 : Daybook
        Select Case typeofTransfer
            Case 0, 1
                updateItemdetailstoOnline(typeofTransfer)
            Case 2
                updateLeveAndGrouptoOnline()
            Case 3
                updateGSTtoOnline()
            Case 4
                refreshSalesOrder()
            Case 5
                updateInvoicesForCollection()
            Case 6
                uploadDayilyReports()
            Case 7
                updateCustomerBalanceOnlyTOweb()
        End Select
    End Sub
    Private Sub updateCustomerBalanceOnlyTOweb()
        If dtTable.Rows.Count = 0 Then Exit Sub
        Dim dt As DataTable
        Dim i As Integer
        Dim strquery As String
        Dim custid As Integer
        strquery = "update CashCustomerTb set balance=0 where companyid=" & webIntegrationid
        _objweb.saveDataToOnline(strquery)
        For i = 0 To dtTable.Rows.Count - 1
            dt = _objweb.returnDatatable("Select custid from CashCustomerTb where companyid=" & webIntegrationid & " AND moseCustomerid=" & dtTable(i)("AccId"))
            If dt.Rows.Count = 0 Then
                strquery = "Insert into CashCustomerTb (CustName,Phone,moseCustomerid,companyid,grpseton,balance,salesman) values(" & _
                            "'" & dtTable(i)("Accdescr") & "'," & _
                            "'" & dtTable(i)("Phone") & "'," & _
                            dtTable(i)("accid") & "," & _
                            webIntegrationid & ",'customer'," & dtTable(i)("balance") & "," & _
                            "'" & Trim(dtTable(i)("collectedOrDeliveredBy") & "") & "')"
                _objweb.saveDataToOnline(strquery)
            Else
                custid = dt(0)("custid")
                strquery = "update CashCustomerTb set balance=" & dtTable(i)("balance") & _
                ",salesman='" & Trim(dtTable(i)("collectedOrDeliveredBy") & "") & "' where custid=" & custid
                _objweb.saveDataToOnline(strquery)
            End If
            status("Updating Outstanding", dtTable(i)("Accdescr"), i + 1, dtTable.Rows.Count)
        Next
    End Sub
    Public Sub updateInvoicesForCollection()
        If dtTable.Rows.Count = 0 Then Exit Sub
        Dim i As Integer
        Dim strquery As String
        Dim custid As Integer
        If Not updateAllInvoice Then
            Dim _qurey As EnumerableRowCollection(Of DataRow)
            _qurey = From data In dtTable.AsEnumerable() Where data("Updated") = "N" Select data
            If _qurey.Count > 0 Then
                dtTable = _qurey.CopyToDataTable()
            Else
                dtTable = dtTable.Clone
            End If
        End If

        Dim dt As DataTable
        For i = 0 To dtTable.Rows.Count - 1
            dt = _objweb.returnDatatable("Select custid from CashCustomerTb where companyid=" & webIntegrationid & " AND moseCustomerid=" & dtTable(i)("AccId"))

            If dt.Rows.Count = 0 Then
                strquery = "Insert into CashCustomerTb (CustName,Phone,moseCustomerid,companyid,grpseton) values(" & _
                            "'" & dtTable(i)("Account Name") & "'," & _
                            "'" & dtTable(i)("Phone") & "'," & _
                            dtTable(i)("accid") & "," & _
                            webIntegrationid & ",'customer') select scope_identity()"
                custid = _objweb.saveDataToOnlineExecuteScalar(strquery)
            Else
                custid = dt(0)("custid")
            End If
            Dim dcTrid As Long = 0
            If Val(dtTable(i)("Trid") & "") = 0 Then
                dt = _objweb.returnDatatable("Select trid from DeliveryOrCollectionTb " & _
                                      "LEFT JOIN CashCustomerTb ON DeliveryOrCollectionTb.customerid=CashCustomerTb.custid" & _
                                      " where DeliveryOrCollectionTb.companyid =" & webIntegrationid & " and moseTrid=0 AND moseCustomerid=" & Val(dtTable(i)("accid") & ""))

                If dt.Rows.Count > 0 Then
                    dcTrid = dt(0)("trid")
                End If
            Else
                dt = _objweb.returnDatatable("Select trid from DeliveryOrCollectionTb where companyid=" & webIntegrationid & " and moseTrid=" & Val(dtTable(i)("Trid") & ""))
                If dt.Rows.Count > 0 Then
                    dcTrid = dt(0)("trid")
                End If
            End If
            If dcTrid = 0 Then
                strquery = "Insert into DeliveryOrCollectionTb (moseTrid,trdate,customerid,customerName,amount,trType," & _
                            "deliveredOrCollectedBy,invNo,prefix,companyid) values (" & _
                                Val(dtTable(i)("Trid") & "") & "," & _
                                "'" & Format(DateValue(dtTable(i)("trdate")), "yyyy/MM/dd") & "'," & _
                                custid & "," & _
                                "'" & dtTable(i)("Account Name") & "'," & _
                                dtTable(i)("balance") & "," & _
                                "'IS'," & _
                                "'" & Trim(dtTable(i)("deliveredby") & "") & "'," & _
                                dtTable(i)("JVNum") & "," & _
                                "'" & dtTable(i)("Prefix") & "'," & _
                                webIntegrationid & _
                                ")"
            Else
                strquery = "Update DeliveryOrCollectionTb set trdate='" & Format(DateValue(dtTable(i)("trdate")), "yyyy/MM/dd") & "'," & _
                            "customerid=" & custid & "," & _
                            "customerName='" & dtTable(i)("Account Name") & "'," & _
                            "deliveredOrCollectedBy='" & Trim(dtTable(i)("deliveredby") & "") & "'," & _
                            "amount=" & dtTable(i)("balance") & " where companyid=" & webIntegrationid & " and trid=" & dcTrid


            End If
            _objweb.saveDataToOnline(strquery)
            status("Sales Invoices", dtTable(i)("InvPrefix"), i + 1, dtTable.Rows.Count)
            Dim _objcmn As New clsCommon_BL
            If Val(dtTable(i)("trid") & "") > 0 Then
                dt = _objweb.returnDatatable("Select amount from DeliveryOrCollectionTb where companyid=" & webIntegrationid & " and moseTrid=" & Val(dtTable(i)("Trid") & ""))
                If dt.Rows.Count > 0 Then
                    If Val(dt(0)("amount")) = 0 Then
                        _objcmn._saveDatawithOutParm("UPDATE ItmInvCmnTb SET isupdatedTowebForCollection=1 where trid=" & Val(dtTable(i)("trid")))
                    Else
                        _objcmn._saveDatawithOutParm("UPDATE ItmInvCmnTb SET isupdatedTowebForCollection=0 where trid=" & Val(dtTable(i)("trid")))
                    End If
                End If
            End If
nxt:
        Next
    End Sub
    Public Sub updateItemdetailstoOnline(ByVal tp As Integer)
        'Dim dt As DataTable
        Dim _objcmn As New clsCommon_BL
        If tp = 1 Then
            dtTable = _objcmn._fldDatatable("Select * from InvItm where isnull(ischange,0)=1")
        Else
            dtTable = _objcmn._fldDatatable("Select * from InvItm")
        End If
        Dim i As Integer
        Dim strquery As String
        If isremove Then
            _objweb.saveDataToOnline("Delete from invitm")
        End If
        For i = 0 To dtTable.Rows.Count - 1
            'If dt(i)("itemid") = 250 Then
            '    MsgBox("")
            'End If
            If Not _objweb.checkexist("Select itemid from InvItm where itemid=" & dtTable(i)("itemid")) Then
                strquery = "insert into invitm (itemid,[Item Code],Unit,Description,UnitPrice,QIH,HSNCode,MRP,shortDscr,longDescr," & _
                "P1Unit,P2Unit,P1Fra,P2Fra,Level1,Level2,Level3,Level4,Level5,Level6,Level7,Level8,Level9,Level10,image1,webname) values( " & _
                            dtTable(i)("itemid") & "," & _
                            "'" & dtTable(i)("Item Code") & "'," & _
                            "'" & dtTable(i)("Unit") & "'," & _
                            "'" & MkDbSrchStr(dtTable(i)("Description")) & "'," & _
                            dtTable(i)("UnitPrice") & "," & _
                            dtTable(i)("QIH") & "," & _
                            "'" & dtTable(i)("HSNCode") & "'," & _
                            dtTable(i)("MRP") & "," & _
                            "'" & MkDbSrchStr(Trim(dtTable(i)("shortDescr") & "")) & "'," & _
                            "'" & MkDbSrchStr(Trim(dtTable(i)("longDescr") & "")) & "'," & _
                            "'" & dtTable(i)("P1Unit") & "'," & _
                            "'" & dtTable(i)("P2Unit") & "'," & _
                            Val(dtTable(i)("P1Fra") & "") & "," & _
                            Val(dtTable(i)("P2Fra") & "") & "," & _
                            Val(dtTable(i)("Level1") & "") & "," & _
                            Val(dtTable(i)("Level2") & "") & "," & _
                            Val(dtTable(i)("Level3") & "") & "," & _
                            Val(dtTable(i)("Level4") & "") & "," & _
                            Val(dtTable(i)("Level5") & "") & "," & _
                            Val(dtTable(i)("Level6") & "") & "," & _
                            Val(dtTable(i)("Level7") & "") & "," & _
                            Val(dtTable(i)("Level8") & "") & "," & _
                            Val(dtTable(i)("Level9") & "") & "," & _
                            Val(dtTable(i)("Level10") & "") & ",'" & _
                            Trim(dtTable(i)("image1") & "") & "','" & _
                            MkDbSrchStr(Trim(dtTable(i)("webname") & "")) & "'" & _
                            ")"
                _objweb.saveDataToOnline(strquery)
                status("Item Master", dtTable(i)("Item Code"), i + 1, dtTable.Rows.Count)

            Else
                strquery = "Update InvItm set Description='" & dtTable(i)("Description") & _
                            "',webname='" & MkDbSrchStr(Trim(dtTable(i)("webname") & "")) & _
                            "',Level1=" & Val(dtTable(i)("Level1") & "") & "," & _
                            "Level2=" & Val(dtTable(i)("Level2") & "") & "," & _
                            "Level3=" & Val(dtTable(i)("Level3") & "") & "," & _
                            "Level4=" & Val(dtTable(i)("Level4") & "") & "," & _
                            "Level5=" & Val(dtTable(i)("Level5") & "") & "," & _
                            "Level6=" & Val(dtTable(i)("Level6") & "") & "," & _
                            "Level7=" & Val(dtTable(i)("Level7") & "") & "," & _
                            "Level8=" & Val(dtTable(i)("Level8") & "") & "," & _
                            "Level9=" & Val(dtTable(i)("Level9") & "") & "," & _
                            "Level10=" & Val(dtTable(i)("Level10") & "") & "," & _
                            "UnitPrice=" & dtTable(i)("UnitPrice") & "," & _
                            "MRP=" & dtTable(i)("MRP") & "," & _
                           IIf((lblwithimage.Tag) = 1, "image1='" & Trim(dtTable(i)("image1") & "") & "',", "") & _
                            "HSNCode='" & Trim(dtTable(i)("HSNCode") & "") & "'," & _
                            "shortDscr='" & Trim(dtTable(i)("shortDescr") & "") & "'," & _
                            "longDescr='" & Trim(dtTable(i)("longDescr") & "") & "'" & _
                            " WHERE Itemid=" & Val(dtTable(i)("itemid"))
                _objweb.saveDataToOnline(strquery)
                status("Item Master", dtTable(i)("Item Code"), i + 1, dtTable.Rows.Count)
            End If
            _objcmn._saveDatawithOutParm("UPDATE InvItm SET ischange=0 where itemid=" & Val(dtTable(i)("itemid")))
        Next
    End Sub
    Private Sub updateLeveAndGrouptoOnline()
        Dim dt As DataTable
        Dim _objcmn As New clsCommon_BL
        dt = _objcmn._fldDatatable("Select * from LevelTb")
        Dim i As Integer
        Dim strquery As String
        If isremove Then
            _objweb.saveDataToOnline("Delete from LevelTb  Delete from GrpItmTb")
        End If
        For i = 0 To dt.Rows.Count - 1
            If Not _objweb.checkexist("Select LCode from LevelTb where LCode=" & dt(i)("LCode")) Then
                strquery = "insert into LevelTb (LCode,LName,lorder) values( " & _
                            dt(i)("LCode") & "," & _
                            "'" & dt(i)("LName") & "'," & _
                            dt(i)("lorder") & ")"
                _objweb.saveDataToOnline(strquery)
                status("Level Master", dt(i)("LName"), i + 1, dt.Rows.Count)
            End If
        Next
        dt = _objcmn._fldDatatable("Select * from GrpItmTb")
        For i = 0 To dt.Rows.Count - 1
            If Not _objweb.checkexist("Select UnqGrpId from GrpItmTb where UnqGrpId=" & dt(i)("UnqGrpId")) Then
                strquery = "insert into GrpItmTb (GrpItmCode,LCode,UnqGrpId,GrpName) values( " & _
                            "'" & dt(i)("GrpItmCode") & "'," & _
                            dt(i)("LCode") & "," & _
                            dt(i)("UnqGrpId") & "," & _
                            "'" & dt(i)("GrpName") & "')"
                _objweb.saveDataToOnline(strquery)
                status("Group Master", dt(i)("GrpItmCode"), i + 1, dt.Rows.Count)
            End If
        Next
    End Sub
    Private Sub updateGSTtoOnline()
        Dim dt As DataTable
        Dim _objcmn As New clsCommon_BL
        Dim i As Integer
        Dim strquery As String
        dt = _objcmn._fldDatatable("Select * from GSTTb")
        If isremove Then
            _objweb.saveDataToOnline("Delete from GSTTb")
        End If
        For i = 0 To dt.Rows.Count - 1
            If Not _objweb.checkexist("Select gstid from GSTTb where gstid=" & dt(i)("gstid")) Then
                strquery = "insert into GSTTb (gstid,HSNCode,CGST,SGST,IGST,GSTName) values( " & _
                            dt(i)("gstid") & "," & _
                            "'" & dt(i)("HSNCode") & "'," & _
                            dt(i)("CGST") & "," & _
                            dt(i)("SGST") & "," & _
                            dt(i)("IGST") & "," & _
                            "'" & dt(i)("GSTName") & "')"
                _objweb.saveDataToOnline(strquery)
                status("GST Master", dt(i)("GSTName"), i + 1, dt.Rows.Count)
            End If
        Next
    End Sub
    Private Sub refreshSalesOrder()
        Dim dtDoc As DataTable
        Dim dtcust As DataTable
        Dim _objcmn As New clsCommon_BL
        Dim i As Integer
        Dim strquery As String
        Dim custid As Long
        Dim dtTable As DataTable
        Dim cashAcc As Long
        dtDoc = _objweb.returnDatatable("Select soid DocId ,ordernumbercount DNo,'' CSCode, " & _
                                        "trdate DDate,CustName,CashCustomerTb.Add1,CashCustomerTb.Add2,CashCustomerTb.Add3," & _
                                        "CashCustomerTb.Phone,CashCustomerTb.email,salesPoints,CashCustomerTb.custid Cwebid from  " & _
                                        "salesorderTb LEFT JOIN CashCustomerTb ON CashCustomerTb.custid=salesorderTb.customerid" & _
                                        " WHERE ISNULL(sostatus,0)=0 ")
        If dtDoc.Rows.Count = 0 Then
            MsgBox("Not found new Orders", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        For i = 0 To dtDoc.Rows.Count - 1
            If Not _objcmn.checkexist("Select custid from CashCustomerTb where webid=" & Val(dtDoc(i)("Cwebid") & "")) Then
                strquery = "Insert into CashCustomerTb(CustName,Add1,Add2,Add3,Phone,email,salesPoints,webid) values(" & _
                            "'" & dtDoc(i)("CustName") & "'," & _
                            "'" & dtDoc(i)("Add1") & "'," & _
                            "'" & dtDoc(i)("Add2") & "'," & _
                            "'" & dtDoc(i)("Add3") & "'," & _
                            "'" & dtDoc(i)("Phone") & "'," & _
                            "'" & dtDoc(i)("email") & "'," & _
                            Val(dtDoc(i)("salesPoints") & "") & "," & _
                            Val(dtDoc(i)("Cwebid") & "") & ")"
                _objcmn._saveDatawithOutParm(strquery)

            End If
            dtcust = _objcmn._fldDatatable("Select custid from CashCustomerTb where webid=" & Val(dtDoc(i)("Cwebid") & ""))
            If dtcust.Rows.Count > 0 Then
                custid = dtcust(0)("custid")
            End If

            dtTable = _objcmn._fldDatatable("SELECT accid FROM AccMast WHERE AccSetId Like '%05%'")
            If dtTable.Rows.Count > 0 Then cashAcc = dtTable(0)("accid")
            strquery = "insert into DocCmnTb(DocType,DNo,CSCode,DDate,webid,cashcustid) values(" & _
                        "'SOW'," & _
                        dtDoc(i)("DNo") & "," & _
                        cashAcc & "," & _
                        "'" & Format(dtDoc(i)("DDate"), "yyyy/MM/dd") & "'," & _
                         dtDoc(i)("DocId") & "," & custid & ")"


            '_objweb.saveDataToOnline(strquery)
            _objcmn._saveDatawithOutParm(strquery)
            getDocDetails(dtDoc(i)("DocId"))
            strquery = "Update salesorderTb set sostatus=1 where soid=" & dtDoc(i)("DocId")
            _objweb.saveDataToOnline(strquery)
            status("Sales Order", dtDoc(i)("DNo"), i + 1, dtDoc.Rows.Count)
        Next
    End Sub
    Private Sub getDocDetails(ByVal Wdocid As Long)
        Dim dtDoc As DataTable
        Dim docid As Long
        Dim _objcmn As New clsCommon_BL
        Dim i As Integer
        Dim strquery As String
        dtDoc = _objcmn._fldDatatable("select docid from DocCmnTb where webid=" & Wdocid)
        If dtDoc.Rows.Count > 0 Then
            docid = dtDoc(0)("docid")
        End If
        If docid > 0 Then
            dtDoc = _objweb.returnDatatable("SELECT InvItm.Description TrDetail,addToCartTb.ItemId,Unit,Qty,InvItm.UnitPrice CostPUnit,1 PFraction " & _
                                            "FROM addToCartTb LEFT JOIN InvItm on addToCartTb.itemid=InvItm.ItemId WHERE SOID=" & Wdocid)
            For i = 0 To dtDoc.Rows.Count - 1
                strquery = "Insert into DocTranTb(docid,SlNo,TrDetail,ItemId,Unit,Qty,CostPUnit,PFraction) values(" & _
                            docid & "," & _
                            i + 1 & "," & _
                            "'" & dtDoc(i)("TrDetail") & "'," & _
                            dtDoc(i)("ItemId") & "," & _
                            "'" & dtDoc(i)("Unit") & "'," & _
                            dtDoc(i)("Qty") & "," & _
                            dtDoc(i)("CostPUnit") & "," & _
                            dtDoc(i)("PFraction") & ")"
                '_objweb.saveDataToOnline(strquery)
                _objcmn._saveDatawithOutParm(strquery)
            Next
        End If

    End Sub
    Private Sub uploadDayilyReports()

        If isdaybook = False Then GoTo nxt
        Dim dt As DataTable
        Dim dt1 As DataTable
        Dim amt As Double
        dt = _objTr.returnTrialbalance(dbookdate1, dbookdate2, 6).Tables(0)
        Dim strquery As String
        Dim invno As String
        Dim AccDescr As String = ""
        Dim trtype As String = ""
        strquery = "update DaybookTb set setremove=1  where cid=" & webIntegrationid & " AND " & _
                                                            "dbdate>='" & Format(DateValue(dbookdate1), "yyyy/MM/dd") & "' AND " & _
                                                            "dbdate<='" & Format(DateValue(dbookdate2), "yyyy/MM/dd") & "'"
        _objweb.saveDataToOnline(strquery)
        For i = 0 To dt.Rows.Count - 1
            invno = dt(i)("prefix") & IIf(dt(i)("prefix") = "", "", "/") & dt(i)("JVNum")
            strquery = "Select did from DaybookTb where cid=" & webIntegrationid & " AND " & _
                                      "dbdate='" & Format(DateValue(dt(i)("JVDate")), "yyyy/MM/dd") & "' AND " & _
                                      "dbtype='" & dt(i)("tp") & "' AND " & _
                                      "accid =" & dt(i)("accid") & " AND " & _
                                      "invno='" & invno & "'"

            dt1 = _objweb.returnDatatable(strquery)
            Select Case dt(i)("trtype")
                Case 1
                    trtype = "IS"
                Case 2, 4
                    trtype = dt(i)("tp")
                Case 3
                    trtype = "IP"
                Case 5, 6
                    trtype = "RV"
                Case 7, 8
                    trtype = "PV"
                Case 9
                    trtype = "DB"
                Case 10
                    trtype = "CB"
                Case 11
                    trtype = "EX"
            End Select

            If dt1.Rows.Count > 0 Then
                If Val(dt(i)("Debit")) > 0 Then
                    amt = Val(dt(i)("Debit"))
                Else
                    amt = Val(dt(i)("credit"))
                End If
                strquery = "update DaybookTb set setremove=0," & IIf(Val(dt(i)("Debit")) > 0, "debit", "credit") & "=" & amt & ",trtype='" & trtype & "' where did=" & dt1(0)("did")
            Else
                AccDescr = dt(i)("AccDescr") & "[ " & dt(i)("tpname") & " ]"
                strquery = "insert into DaybookTb (cid,invno,dbdate,dbtype,dbDescription,debit,credit,isNotdaybookItem,accid,trtype,setremove) values" & _
                            "(" & webIntegrationid & ",'" & _
                            invno & "','" & _
                            Format(DateValue(dt(i)("jvdate")), "yyyy/MM/dd") & "','" & _
                            dt(i)("tp") & "','" & _
                            AccDescr & "'," & _
                            dt(i)("debit") & "," & _
                            dt(i)("credit") & "," & _
                            dt(i)("isNotdaybookItem") & "," & _
                            dt(i)("accid") & ",'" & _
                            trtype & "',0)"
            End If
            _objweb.saveDataToOnline(strquery)
            status("Day Book Updating..", AccDescr, i + 1, dt.Rows.Count)
        Next
        strquery = "DELETE FROM DaybookTb where setremove=1 AND  cid=" & webIntegrationid & " AND " & _
                                                            " dbdate>='" & Format(DateValue(dbookdate1), "yyyy/MM/dd") & "' AND " & _
                                                            "dbdate<='" & Format(DateValue(dbookdate2), "yyyy/MM/dd") & "'"
        _objweb.saveDataToOnline(strquery)
nxt:
        If isfinancialStatus Then ldFinancialStatusSummary()
        If isAccountbalance Then returnAccountBalanceForWeb()
        If isprofitloss Then updateProfitLoss()
        If isdaybook Or isfinancialStatus Or isAccountbalance Or isprofitloss Then
            _objweb.saveDataToOnline("update companysettings set lastupdatedFromMose='" & Date.Now & "' where cid=" & webIntegrationid)
        End If
    End Sub
    Private Sub ldFinancialStatusSummary()
        Dim dtsummary As DataTable
        Dim _objcmn As New clsCommon_BL
        dtsummary = _objcmn._fldDatatable("returnFinancialStatusForWeb", True)
        Dim i As Integer
        Dim dt As DataTable
        For i = 0 To dtsummary.Rows.Count - 1
            dt = _objweb.returnDatatable("Select cid from FinancialStatusTb where cid=" & webIntegrationid & " AND caption='" & dtsummary(i)("CAP") & "'")
            If dt.Rows.Count > 0 Then
                _objweb.saveDataToOnline("update FinancialStatusTb set amt=" & dtsummary(i)("amt") & " where cid=" & webIntegrationid & " and caption='" & dtsummary(i)("CAP") & "'")
            Else
                _objweb.saveDataToOnline("Insert into FinancialStatusTb (cid,caption,amt) values(" & webIntegrationid & ",'" & _
                                                                        dtsummary(i)("cap") & "'," & dtsummary(i)("amt") & ")")
            End If
            status("Financial Status Updating..", dtsummary(i)("cap"), i + 1, dtsummary.Rows.Count)
        Next

    End Sub
    Private Sub returnAccountBalanceForWeb()
        Dim dtsummary As DataTable
        Dim _objcmn As New clsCommon_BL
        dtsummary = _objcmn._fldDatatable("returnAccountBalanceForWeb", True)
        Dim i As Integer
        Dim dt As DataTable
        _objweb.saveDataToOnline("update CashCustomerTb set  balance=0 where companyid=" & webIntegrationid)
        For i = 0 To dtsummary.Rows.Count - 1
            dt = _objweb.returnDatatable("Select custid from CashCustomerTb where companyid=" & webIntegrationid & " AND moseCustomerid=" & dtsummary(i)("AccId"))
            If dt.Rows.Count > 0 Then

                _objweb.saveDataToOnline("update CashCustomerTb set " & _
                                         "CustName='" & dtsummary(i)("accdescr") & "'," & _
                                         "Phone='" & dtsummary(i)("Phone") & "'," & _
                                         "email='" & dtsummary(i)("email") & "'," & _
                                         "grpseton='" & dtsummary(i)("grpseton") & "'," & _
                                         "companyid='" & webIntegrationid & "'," & _
                                         "balance=" & dtsummary(i)("debit") - dtsummary(i)("credit") & _
                                         "where custid=" & dt(0)("custid"))
            Else
                _objweb.saveDataToOnline("insert into CashCustomerTb (CustName,Phone,email,moseCustomerid,companyid,grpseton,balance) values(" & _
                                         "'" & dtsummary(i)("accdescr") & "'," & _
                                         "'" & dtsummary(i)("phone") & "'," & _
                                         "'" & dtsummary(i)("email") & "'," & _
                                         dtsummary(i)("accid") & "," & _
                                         webIntegrationid & "," & _
                                         "'" & dtsummary(i)("grpseton") & "'," & _
                                         dtsummary(i)("debit") - dtsummary(i)("credit") & ")")
            End If
            status("Outstanding Updating..", dtsummary(i)("accdescr"), i + 1, dtsummary.Rows.Count)
        Next
    End Sub
    Private Sub updateProfitLoss()
        Dim dtsummary As DataTable
        Dim _objcmn As New clsCommon_BL
        Dim d1, d2 As Date
        d1 = DateValue("01/" & Month(dbookdate1) & "/" & Year(dbookdate1))
        d2 = DateValue("01/" & Month(dbookdate2) & "/" & Year(dbookdate2))
        d2 = DateAdd(DateInterval.Month, 1, d2)
        d2 = DateAdd(DateInterval.Day, -1, d2)
        dtsummary = _objcmn.returnProfitLossForWeb(d1, d2)
        Dim i As Integer
        Dim dt As DataTable
        Dim query As String
        query = "update ProfitLossTb set setremove=1  where cid=" & webIntegrationid & " AND " & _
                                                            "pldate>='" & Format(DateValue(dbookdate1), "yyyy/MM/dd") & "' AND " & _
                                                            "pldate<='" & Format(DateValue(dbookdate2), "yyyy/MM/dd") & "'"
        _objweb.saveDataToOnline(query)
        For i = 0 To dtsummary.Rows.Count - 1
            Dim pldate As Date
            pldate = DateValue("01/" & dtsummary(i)("mnth") & "/" & dtsummary(i)("yrth"))
            pldate = DateAdd(DateInterval.Month, 1, pldate)
            pldate = DateAdd(DateInterval.Day, -1, pldate)
            dt = _objweb.returnDatatable("Select plid from ProfitLossTb where pldate='" & Format(DateValue(pldate), "yyyy/MM/dd") & "' and " & _
                                         "cid=" & webIntegrationid & " AND accid=" & dtsummary(i)("AccId"))
            If dt.Rows.Count > 0 Then
                query = "update ProfitLossTb set " & _
                                         "grp='" & dtsummary(i)("grp") & "'," & _
                                         "description='" & dtsummary(i)("AccDescr") & "'," & _
                                         "amount=" & dtsummary(i)("DealAmt") & "," & _
                                         "grpid=" & dtsummary(i)("MAccId") & "," & _
                                         "setremove=0 " & _
                                         "where plid=" & dt(0)("plid")

            Else
                query = "INSERT INTO ProfitLossTb(cid,grp,accid,description,amount,grpid,pldate,setremove) values(" & _
                                        webIntegrationid & "," & _
                                        "'" & dtsummary(i)("grp") & "'," & _
                                        dtsummary(i)("accid") & "," & _
                                        "'" & dtsummary(i)("AccDescr") & "'," & _
                                        dtsummary(i)("DealAmt") & "," & _
                                        dtsummary(i)("MAccId") & "," & _
                                        "'" & Format(DateValue(pldate), "yyyy/MM/dd") & "',0)"


            End If
            _objweb.saveDataToOnline(query)
            status("Profit & Loss Updating..", dtsummary(i)("accdescr"), i + 1, dtsummary.Rows.Count)
        Next
        query = "delete from ProfitLossTb where setremove=1 AND cid=" & webIntegrationid & " AND " & _
                                                            "pldate>='" & Format(DateValue(dbookdate1), "yyyy/MM/dd") & "' AND " & _
                                                            "pldate<='" & Format(DateValue(dbookdate2), "yyyy/MM/dd") & "'"
        _objweb.saveDataToOnline(query)
    End Sub

    Private Sub Worker_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Worker.RunWorkerCompleted
        MsgBox("Completed", MsgBoxStyle.Information)
        Me.Close()
    End Sub

End Class