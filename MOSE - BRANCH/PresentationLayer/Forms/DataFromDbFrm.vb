
Public Class DataFromDbFrm
    Private _objcmnbLayer As New clsCommon_BL
    Dim _objItmMast As New clsItemMast_BL
    Private _objGst As New clsGSTMaster
    Delegate Sub GenericDelegate(ByVal Mname As String, ByVal RecName As String, ByVal rec As Integer, ByVal count As Integer)
    Private Sub btnok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnok.Click
        If MsgBox("Do you want to transfer products from linked db?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        updateItemdetails()
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
            lblmodule.Refresh()
            lblrec.Refresh()
        End If
    End Sub
    Private Sub updateItemdetails()
        Dim i As Integer
        Try
            Dim dt As DataTable
            Dim dtMose As DataTable
            Dim dbname As String = ""
            dt = _objcmnbLayer._fldDatatable("select seconddb from CompanyTb")
            If dt.Rows.Count > 0 Then
                dbname = Trim(dt(0)("seconddb") & "")
            End If
            If dbname = "" Then
                MsgBox("Linked DB not found", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            If chkappend.Checked = False Then
                _objcmnbLayer._saveDatawithOutParm("delete from InvItm")
                _objcmnbLayer._saveDatawithOutParm("delete from InvItmPropertiesTb")
            End If
            Dim strQuery As String
            strQuery = "select * from  " & dbname & ".dbo.invitm Products " & _
                        "left join  " & dbname & ".dbo.GSTTb gst on Products.HSNCode=gst.HSNCode " & _
                        "left join (select vatid,vatcode,vat from " & dbname & ".dbo.VatMasterTb) vat on Products.vatId=vat.vatid " & _
                        "left join (select vatid rgcessid,vatcode rgcessCode,vat rgcess from  " & dbname & ".dbo.VatMasterTb) rgcess on Products.regularcessid=rgcess.rgcessid " & _
                        "left join (select GrpItmCode level1code,UnqGrpId level1Id,GrpName level1name from  " & dbname & ".dbo.GrpItmTb )level1 on Products.Level1=level1.level1Id " & _
                        "left join (select GrpItmCode level2code,UnqGrpId level2Id,GrpName level2name from  " & dbname & ".dbo.GrpItmTb )level2 on Products.Level2=level2.level2Id " & _
                        "left join (select GrpItmCode level3code,UnqGrpId level3Id,GrpName level3name from  " & dbname & ".dbo.GrpItmTb )level3 on Products.Level2=level3.level3Id " & _
                        "left join (select GrpItmCode level4code,UnqGrpId level4Id,GrpName level4name from  " & dbname & ".dbo.GrpItmTb )level4 on Products.Level2=level4.level4Id " & _
                        "left join (select GrpItmCode level5code,UnqGrpId level5Id,GrpName level5name from  " & dbname & ".dbo.GrpItmTb )level5 on Products.Level5=level5.level5Id "
            dtMose = _objcmnbLayer._fldDatatable(strQuery)

            Dim Ctn As Integer = dtMose.Rows.Count - 1
            Dim hsncode As String
            Dim vatid As Integer
            Dim KLFC As Integer
            Dim added As Integer
            Dim edited As Integer
            Dim level1, level2, level3, level4, level5 As Integer
            For i = 0 To Ctn
                dt = _objcmnbLayer._fldDatatable("SELECT itemid from invitm where [Item Code]='" & MkDbSrchStr(Trim(dtMose(i)("Item Code") & "")) & "'")
                If dt.Rows.Count = 0 Then
                    hsncode = createHSN(Trim(dtMose(i)("gstname") & ""), Val(dtMose(i)("IGST") & ""))
                    KLFC = createVatCode(Val(dtMose(i)("vat") & ""))
                    vatid = createVatCode(Val(dtMose(i)("rgcess") & ""))
                    level1 = getgroupId(Trim(dtMose(i)("level1code") & ""))
                    level2 = getgroupId(Trim(dtMose(i)("level2code") & ""))
                    level3 = getgroupId(Trim(dtMose(i)("level3code") & ""))
                    level4 = getgroupId(Trim(dtMose(i)("level4code") & ""))
                    level5 = getgroupId(Trim(dtMose(i)("level5code") & ""))
                    strQuery = "INSERT INTO INVITM ([Item Code],Description,Unit,OpQty,OpCost,UnitPrice,UnitPriceWS," & _
                                                  "itemCategory,HSNCode,MRP,shortDescr,longDescr,webname,SECONDPRICE,vatid,REGULARCESSID,MinQty," & _
                                                  "P1Unit,P2Unit,P1Fra,P2Fra,Level1,Level2,Level3,Level4,Level5,IscostAssign,Make,Model,MinSalesPrice," & _
                                                  "Rack,isrestuarentMenuitem,mechineItemcode,additionalcess" & _
                                                  " ) VALUES(" & _
                                                   "'" & MkDbSrchStr(Trim(dtMose(i)("Item Code") & "")) & "'," & _
                                                   "'" & MkDbSrchStr(Trim(dtMose(i)("Description") & "")) & "'," & _
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
                                                   KLFC & "," & _
                                                   vatid & "," & _
                                                   Val(dtMose(i)("MinQty") & "") & ",'" & _
                                                   Trim(dtMose(i)("P1Unit") & "") & "','" & _
                                                   Trim(dtMose(i)("P2Unit") & "") & "'," & _
                                                   Val(dtMose(i)("P1Fra") & "") & "," & _
                                                   Val(dtMose(i)("P2Fra") & "") & "," & _
                                                   level1 & "," & level2 & "," & level3 & "," & level4 & "," & level5 & "," & _
                                                   Val(dtMose(i)("IscostAssign") & "") & ",'" & _
                                                   Trim(dtMose(i)("make") & "") & "','" & _
                                                   Trim(dtMose(i)("Model") & "") & "'," & _
                                                   Val(dtMose(i)("MinSalesPrice") & "") & ",'" & _
                                                   Trim(dtMose(i)("Rack") & "") & "'," & _
                                                   Val(dtMose(i)("isrestuarentMenuitem") & "") & ",'" & _
                                                   Trim(dtMose(i)("mechineItemcode") & "") & "'," & _
                                                   Val(dtMose(i)("additionalcess") & "") & _
                                                   ")"
                    _objcmnbLayer._saveDatawithOutParm(strQuery)
                    added = added + 1
                    status("Inserting Data", "", i + 1, Ctn + 1)
                Else
                    _objcmnbLayer._saveDatawithOutParm("UPDATE INVITM SET OpQty=" & Val(dtMose(i)("OpQty") & "") & "," & _
                                                       "OpCost=" & Val(dtMose(i)("OpCost") & "") & "," & _
                                                       "UnitPrice=" & Val(dtMose(i)("UnitPrice") & "") & "," & _
                                                       "UnitPriceWS=" & Val(dtMose(i)("UnitPriceWS") & "") & "," & _
                                                       "MRP=" & Val(dtMose(i)("MRP") & "") & "," & _
                                                       "SECONDPRICE=" & Val(dtMose(i)("SECONDPRICE") & "") & _
                                                       " WHERE ITEMID=" & Val(dt(0)("itemid")))
                    status("Updating Data", "", i + 1, Ctn + 1)
                    edited = edited + 1
                End If
            Next
            MsgBox("Completed" & vbCrLf & added & " Prodects added" & vbCrLf & edited & " Prodects edited", MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox(ex.Message & " Row : " & i, MsgBoxStyle.Exclamation)
        End Try


    End Sub
    Private Function getgroupId(ByVal grpcode As String) As Integer
        Dim dt As DataTable
        If grpcode = "" Then Return 0
        dt = _objcmnbLayer._fldDatatable("select UnqGrpId from GrpItmTb where GrpItmCode='" & grpcode & "'")
        If dt.Rows.Count > 0 Then
            Return dt(0)("UnqGrpId")
        End If
        Return 0
    End Function
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

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class